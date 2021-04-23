using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;

public partial class SmpProgram_maintain_SPTS006_Input : BaseWebUI.DataListSaveForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {  
        //公司別
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string[,] ids = null;
        string userGUID = (string)Session["UserGUID"];
        SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
        ids = tsmp.getCompanyCodeName(engine, userGUID);
        if (ids != null)
        {
            CompanyCode.setListItem(ids);           
        }

        //學歷
        ids = new string[,]{
                {"",""},
                {"1","國中(含)以下"},
                {"2","高中"},
                {"3","專科"},
                {"4","學士"},
                {"5","碩士"},
                {"6","博士"},
            };
        Educational.setListItem(ids);
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (!isNew)
        {
            CompanyCode.ReadOnly = true;
        }
        CompanyCode.ValueText = objects.getData("CompanyCode");
        Educational.ValueText = objects.getData("Educational");        
        JobFunction.ValueText = objects.getData("JobFunction");
        JobItem.ValueText = objects.getData("JobItem");
        Experience.ValueText = objects.getData("Experience");        
        Course.ValueText = objects.getData("Course");
        Skill.ValueText = objects.getData("Skill");
        Evaluation.ValueText = objects.getData("Evaluation");
        StartYear.ValueText = objects.getData("StartYear");
        EndYear.ValueText = objects.getData("EndYear");
        Remark.ValueText = objects.getData("Remark");     
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));             
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
                
        string companyCode = CompanyCode.ValueText;
        string companyName = "";

        if (companyCode.Equals("SMP"))
        {
            companyName = "新普科技";
        }
        else if (companyCode.Equals("TP"))
        {
            companyName = "中普科技";
        }
        objects.setData("CompanyCode", companyCode);
        objects.setData("CompanyName", companyName);
        objects.setData("Educational", Educational.ValueText);
        objects.setData("JobFunction", JobFunction.ValueText);
        objects.setData("JobItem", JobItem.ValueText);
        objects.setData("Experience", Experience.ValueText);       
        objects.setData("Course", Course.ValueText);
        objects.setData("Evaluation", Evaluation.ValueText);
        objects.setData("Skill", Skill.ValueText);
        objects.setData("StartYear", StartYear.ValueText);
        objects.setData("EndYear", EndYear.ValueText);
        objects.setData("Remark", Remark.ValueText);

        string errMsg = checkFieldData(objects);
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }           
    }


    public string checkFieldData(com.dsc.kernal.databean.DataObject objects)
    {
        string errMsg = "";

        string sql = null;
        string guid = objects.getData("GUID");              
        string companyCode = objects.getData("CompanyCode");      
        string startYear = objects.getData("StartYear");
        string jobFunction = objects.getData("JobFunction");
        string endYear = objects.getData("EndYear");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        if (endYear.Equals(""))
        {
            endYear = "9999";
        }   
        
        if (companyCode.Equals(""))
        {
            errMsg += "請選擇[" + LblCompanyCode.Text + "]!\n";
        }

        if (Educational.ValueText.Equals(""))
        {
            errMsg += "請選擇[" + LblEducational.Text + "]!\n";
        }

        if (JobItem.ValueText.Equals(""))
        {
            errMsg += "請選擇[" + LblJobItem.Text + "]!\n";
        }    

        if (jobFunction.Equals(""))
        {
            errMsg += "請選擇[" + LblJobFunction.Text + "]!\n";
        }
        else
        {
            //在同一有效期間內員工、部門、工作職務不可重覆
            sql = "select count(*) cnt from SmpTSExpertiseRep where JobFunction = '" + jobFunction + "' " +                 
                  "and CompanyCode ='" + CompanyCode.ValueText + "' " +
                  "and GUID != '" + guid + "' " +
                  "and ((StartYear <= '" + startYear + "' and (EndYear >= '" + startYear + "' or EndYear = '')) " +
                    "or (StartYear <= '" + endYear + "' and (EndYear >= '" + endYear + "' or EndYear = '')))";
            int cnt = (int)engine.executeScalar(sql);
            if (cnt > 0)
            {
                errMsg += "在同一有效期間[" + LblJobFunction.Text + "]不可重覆!\n";
            }
        }

        if (!startYear.Equals("") && !endYear.Equals(""))
        {
            //檢查日期
            if (startYear.CompareTo(endYear) > 0)
            {
                errMsg += "有效年度(迄)需大於等於有效年度(起)!\n";
            }
        }        
 
        
        return errMsg;
    }
        
      

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS006.SmpTSExpertiseRepAgent");
        agent.engine = engine;
        agent.query("1=2");

        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            result = agent.update();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }

    //check 有效年度(起)   
    protected void StartYear_TextChanged(object sender, EventArgs e)
    {
        int n;
        if ((!int.TryParse(StartYear.ValueText, out n)) || (StartYear.ValueText.Length !=4))
        {
            MessageBox("有效年度(起)，請輸入西元年(YYYY)");
            StartYear.ValueText = "";
            StartYear.Focus();
        }

    }

    //check 有效年度(迄)   
    protected void EndYear_TextChanged(object sender, EventArgs e)
    {
        int n;
        if ((!int.TryParse(EndYear.ValueText, out n)) || (EndYear.ValueText.Length != 4))
        {
            MessageBox("有效年度(迄)，請輸入西元年(YYYY)");
            EndYear.ValueText = "";
            EndYear.Focus();
        }
    }
}