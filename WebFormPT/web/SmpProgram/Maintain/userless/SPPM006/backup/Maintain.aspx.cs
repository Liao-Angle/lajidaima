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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;
using com.dsc.kernal.agent;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM006_Input : BaseWebUI.GeneralWebPage
{
	private string connectString = "";
	private string engineType = "";
	
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPPM006M";
        ApplicationID = "SYSTEM";
        ModuleID = "SPPM";

        base.OnInit(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {		
		if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {                
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                string[,] ids = null;
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定考核計畫
                ids = SmpPmMaintainUtil.getAssessment(engine);               
                AssessmentPlanGUID.setListItem(ids);
                //設定等級
                ids = SmpPmMaintainUtil.getAchievementLevel(engine);
                AchievementLevel.setListItem(ids);
                //AchievementLevel.ValueText = "NA";

                string userGUID = (string)Session["UserGUID"];
                string whereClause = " 1=2 ";
                queryData(whereClause);

                //部門
                CheckDept.clientEngineType = engineType;
                CheckDept.connectDBString = connectString;

                //統計欄位readonly
                Total.ValueText = "0";
                Complete.ValueText = "0";
                UnComplete.ValueText = "0";
                Total.ReadOnly = true;
                Complete.ReadOnly = true;
                UnComplete.ReadOnly = true;              
                
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSearch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText))
        {
            //query 
            Total.ValueText = "0";
            Complete.ValueText = "0";
            UnComplete.ValueText = "0";
            string userGUID = (string)Session["UserGUID"];            
            string whereClause = " AssessUserGUID='" + userGUID + "' ";
            whereClause += " and AssessmentPlanGUID='" + AssessmentPlanGUID.ValueText + "'";
            whereClause += " and AssessmentPlanGUID='" + AssessmentPlanGUID.ValueText + "'";
            
            if (!string.IsNullOrEmpty(AchievementLevel.ValueText))
            {
                if (AchievementLevel.ValueText.Equals("NA"))
                    whereClause += " and AchievementLevel='' ";                 
                else
                    whereClause += " and AchievementLevel='" + AchievementLevel.ValueText + "' ";
            }
            if (!string.IsNullOrEmpty(CheckDept.ValueText))
            {
                whereClause += " and deptId='" + CheckDept.ValueText + "'";
            }            
            queryData(whereClause);

            //統計人數   
            string connectString = (string)Session["connectString"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);        
            string sql = "select count(AchievementLevel),isnull(sum(case when  AchievementLevel <>'' then 1 else 0 end ),0) complete," +
                         "isnull(sum(case when  AchievementLevel <>'' then 0  else 1  end ),0) uncomplete "+
                         "from SmpPmUserAchievement a "+
                         "join SmpHrEmployeeInfoV b on b.empGUID=a.UserGUID " +
                         "where " + whereClause ;
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Total.ValueText = ds.Tables[0].Rows[0][0].ToString();
                Complete.ValueText = ds.Tables[0].Rows[0][1].ToString();
                UnComplete.ValueText = ds.Tables[0].Rows[0][2].ToString();
            }
        }
        else
        {
            MessageBox("請先選擇考核名稱!");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="whereClause"></param>
    protected void queryData(string whereClause)
    {
	    AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM006.SmpPmUserAchievementAgent");
            agent.engine = engine;
            agent.query(whereClause);
            DataObjectSet set = agent.defaultData;

            AchievementList.InputForm = "Input.aspx";
            AchievementList.DialogHeight = 360;
            AchievementList.DialogWidth = 840;
            AchievementList.HiddenField = new string[] { "GUID", "deptId", "UserGUID", "AssessmentPlanGUID", "AssessmentName", "AssessUserGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS", "D_INSERTUSER","D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME"  };
            AchievementList.dataSource = set;
            AchievementList.updateTable();

        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
            writeLog(ex);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
    }

    //部門開窗前先取得公司名
    protected void CheckDept_BeforeClickButton()
    {
        string connectString = (string)Session["connectString"];
      
        string companyCode = "";
        string companyName = "";
        string[,] ids = null;      
       
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);        

        string sql = "select CompanyCode from SmpPmAssessmentPlan Where GUID = '" + AssessmentPlanGUID.ValueText + "' ";
        DataSet ds = engine.getDataSet(sql, "TEMP");       
        if (ds.Tables[0].Rows.Count > 0)
        {
          companyCode= ds.Tables[0].Rows[0][0].ToString();          

          //依companyCode取得公司名稱
          ids = SmpPmMaintainUtil.getCompanyIds(engine);
          for (int i = 0; i < ids.Length; i++)
          {
              //if (ids[i, 0].Equals(companyCode))
              //    companyName = ids[i, 1];
          }
          companyName = "SMP";
          //CheckDept.whereClause = " (organizationName ='" + companyName + "' )";
        }
                
    }
}
