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
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Xml;
using WebServerProject.auth;


public partial class SmpProgram_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPTS011M";
        ApplicationID = "SMPFORM";
        ModuleID = "SPTS";

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {         
        if (!IsPostBack)
        {            
            if (!IsProcessEvent)
            {                
                //權限判斷
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                AUTHAgent authagent = new AUTHAgent();
                authagent.engine = engine;
                int auth = authagent.getAuth("SPTS011M", (string)Session["UserID"], (string[])Session["usergroup"]);
                
                if (auth == 0)
                {
                    Response.Redirect("~/NoAuth.aspx");
                }
                else
                {
                    //公司別
                    string[,] ids = null;   
                    string userGUID = (string)Session["UserGUID"];
                    SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
                    ids = tsmp.getCompanyCodeName(engine, userGUID);                   
                    CompanyCode.setListItem(ids);
                   
                    CheckDept.clientEngineType = engineType;
                    CheckDept.connectDBString = connectString;
                    Actual.ReadOnly = true;
                    Plan.ReadOnly = true;
                    NewCourse.ReadOnly = true;
                    AchieveRate.ReadOnly = true;
                    ReqList.NoAdd = true;
                    ReqList.NoModify = true;
                    ReqList.NoDelete = true;
                    ReqList.isShowAll = true;
                    ReqList.showExcel = true;
                }
                engine.close();
            }
            
        }
        
    }
        
	
    protected void SearchButton_Click(object sender, EventArgs e)
    {		
        AbstractEngine engine = null;
        try
        {
            if (CompanyCode.ValueText.Equals(""))
            {
                MessageBox("請選擇公司別!");
            }
            else
            {
            
                IOFactory factory = new IOFactory();
                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                NLAgent agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPTS011.SmpTSAchievementRateVAgent");
                agent.engine = engine;

                string reqSDate = StartDate.ValueText;
                string reqEDate = EndDate.ValueText;
                string whereCondition = " CompanyCode ='" + CompanyCode.ValueText + "' ";

                //開課日期
                if (!reqSDate.Equals("") && !reqEDate.Equals(""))
                {
                    whereCondition += " and  (SchYear + '/' + SchMonth) BETWEEN '" + reqSDate + "' and '" + reqEDate + "' ";
                }
                else if (!reqSDate.Equals("") && reqEDate.Equals(""))
                {
                    whereCondition += " and  (SchYear + '/' + SchMonth) >= '" + reqSDate + "' ";
                }
                else if (reqSDate.Equals("") && !reqEDate.Equals(""))
                {
                    whereCondition += " and  (SchYear + '/' + SchMonth) <= '" + reqEDate + "' ";
                }

                //部門
                if (!CheckDept.ValueText.Equals(""))
                    whereCondition += " and deptId ='" + CheckDept.ValueText + "' ";

                agent.query(whereCondition);

                DataObjectSet dos = agent.defaultData;
                ReqList.dataSource = dos;
                //   ReqList.showSettingPages = new Boolean[] { false, false, false, false, false, false, false, false, false, false };
                ReqList.HiddenField = new string[] { "SchYear", "SchMonth"};

                double actualCount = 0;
                double planCount = 0;
                double newCount = 0;
                double achieveRate = 0;
                for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                {
                    DataObject obj = dos.getAvailableDataObject(i);
                    if (!obj.getData("StartDate").Equals("") && obj.getData("Status").Equals("Closed"))
                        actualCount++;
                    if (obj.getData("SchSource").Equals("1"))
                        planCount++;
                    else if (obj.getData("SchSource").Equals("2"))
                        newCount++;
                }

                if ((planCount + newCount) > 0)
                   achieveRate = Math.Round(actualCount / (planCount + newCount),2);

                Actual.ValueText = actualCount.ToString();
                Plan.ValueText = planCount.ToString();
                NewCourse.ValueText = newCount.ToString();            
                AchieveRate.ValueText =  String.Format("{0:P0}", achieveRate) ;
                ReqList.updateTable();
                ReqList.reSortCondition("計劃代號", DataObjectConstants.ASC);
            }    
        }
        catch (Exception ex)
        {
            
            MessageBox(ex.Message);
            writeLog(ex);
        }
        finally
        {
            if (engine !=null)
                engine.close();
        }

    }
}
