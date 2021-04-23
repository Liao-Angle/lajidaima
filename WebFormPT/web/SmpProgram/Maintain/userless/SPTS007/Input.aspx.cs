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

public partial class SmpProgram_maintain_SPTS007_Input : BaseWebUI.DataListSaveForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        EmployeeGUID.clientEngineType = (string)Session["engineType"];
        EmployeeGUID.connectDBString = (string)Session["connectString"];
        DeptGUID.clientEngineType = (string)Session["engineType"];
        DeptGUID.connectDBString = (string)Session["connectString"];

        //公司別
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string userGUID = (string)Session["UserGUID"];
        string[,] ids = null;       
        SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
        ids = tsmp.getCompanyCodeName(engine, userGUID);
        CompanyCode.setListItem(ids);        
        OnBoard.ReadOnly = true;
        JobTitle.ReadOnly = true;
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
        EmployeeGUID.GuidValueText = objects.getData("EmployeeGUID");
        EmployeeGUID.doGUIDValidate();
        DeptGUID.GuidValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
        JobTitle.ValueText = objects.getData("JobTitle");
        OnBoard.ValueText = objects.getData("OnBoard");
        Specialty.ValueText = objects.getData("Specialty");
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
        objects.setData("EmployeeGUID", EmployeeGUID.GuidValueText);      
        objects.setData("id", EmployeeGUID.ValueText);
        objects.setData("userName", EmployeeGUID.ReadOnlyValueText);
        objects.setData("DeptGUID", DeptGUID.GuidValueText);
        objects.setData("deptId", DeptGUID.ValueText);
        objects.setData("deptName", DeptGUID.ReadOnlyValueText);
        objects.setData("JobTitle", JobTitle.ValueText);
        objects.setData("OnBoard", OnBoard.ValueText);
        objects.setData("Specialty", Specialty.ValueText);
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
        string employeeGuid = objects.getData("EmployeeGUID");
        string deptGuid = objects.getData("DeptGUID");
        string companyCode = objects.getData("CompanyCode");      
        string startYear = objects.getData("StartYear");
        string endYear = objects.getData("EndYear");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        if (companyCode.Equals(""))
        {
            errMsg += "請選擇[" + LblCompanyCode.Text + "]!\n";
        }
        
        if (Specialty.ValueText.Equals(""))
        {
            errMsg += "請選擇[" + LblSpecialty.Text + "]!\n";
        }

        if (endYear.Equals(""))
        {
            endYear = "9999";
        }        
        
        if (JobTitle.ValueText.Equals(""))
        {
            errMsg += "請選擇[" + LblJobTitle.Text + "]!\n";
        }
        else
        {
            //在同一有效期間內員工不可重覆
            sql = "select count(*) cnt from SmpTSProfessional where EmployeeGUID = '" + employeeGuid + "' " +
               //   "and DeptGUID ='" + deptGuid + "' "+
               //   "and JobTitle ='" + JobTitle.ValueText + "' " +
                  "and CompanyCode ='" + CompanyCode.ValueText + "' " +
                  "and GUID != '" + guid + "' " +
                  "and ((StartYear <= '" + startYear + "' and (EndYear >= '" + startYear + "' or EndYear = '')) " +
                    "or (StartYear <= '" + endYear + "' and (EndYear >= '" + endYear + "' or EndYear = '')))";
            int cnt = (int)engine.executeScalar(sql);
            if (cnt > 0)
            {
                errMsg += "在同一有效期間[員工工號]不可重覆!\n";
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
    /// 帶出部門及到職日
    /// </summary>
    /// <param name="values"></param>
    protected void EmployeeGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;

        try
        {
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
            string userGUID = EmployeeGUID.GuidValueText;
            string sql = "select organizationUnitOID from Functions where occupantOID = '" + userGUID + "'";
            string orgUnitGUID = (string)engine.executeScalar(sql);
            //部門
            if (!orgUnitGUID.Equals(""))
            {
                DeptGUID.GuidValueText = orgUnitGUID;
                DeptGUID.doGUIDValidate();
            }

            //到職日
            //取得鼎新連線
            SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
            erpEngine = tsmp.getErpEngine(engine, CompanyCode.ValueText);
            sql = " SELECT convert(char,cast(MV021 as datetime),111) MV021C,P.ME002 " +
                 " FROM CMSMV " +
                 " JOIN CMSSMA C ON MV001=C.MA001 " +
                 " JOIN PALSME P ON MA013=P.ME001 " +
                 " WHERE MV001='" + EmployeeGUID.ValueText + "' ";
            DataSet ds = erpEngine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                OnBoard.ValueText = ds.Tables[0].Rows[0][0].ToString();
                JobTitle.ValueText = ds.Tables[0].Rows[0][1].ToString();
            }
        }

        catch (Exception e)
        {
            writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine !=null)
                engine.close();
            if (erpEngine != null)
                erpEngine.close();
        }

        
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
        agent.loadSchema("WebServerProject.maintain.SPTS007.SmpTSProfessionalAgent");
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