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
using System.IO;
using System.Xml;
using WebServerProject.auth;
using NPOI.HSSF.UserModel;
 


public partial class SmpProgram_Maintain_SPTS010_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        Master.SendButtonClick += new ProjectBaseWebUI_ListMaintain.SendButtonClickEvent(Master_SendButtonClick);
        Master.DeleteButtonClick += new ProjectBaseWebUI_ListMaintain.DeleteButtonClickEvent(Master_DeleteButtonClick);
        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                start();
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void start()
    {
        Master.maintainIdentity = "SPTS010M";
        Master.ApplicationID = "SMPFORM";
        Master.ModuleID = "SPTS"; //教育訓練模組

        DataObject obj = new DataObject();
        obj.loadFileSchema("WebServerProject.maintain.SPTS010.SmpTSTrainingListV");        
        Master.HiddenField = new string[] { "GUID", "CourseFormGUID","CompanyCode", "EmployeeGUID","DeptGUID","IS_LOCK", "IS_DISPLAY", "DATA_STATUS" ,"D_INSERTUSER","D_INSERTTIME","D_MODIFYUSER","D_MODIFYTIME"};
      
        Master.DialogHeight = 540;
        Master.InitData(obj);
           
    }   


    public void Master_SendButtonClick(string whereClause)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS010.SmpTSTrainingListVAgent"); 
        agent.engine = engine;

        //get CompanyCode
        string userGUID = (string)Session["UserGUID"];
        SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
        string companyCode = tsmp.getCompanyCode(engine, userGUID);
        if (companyCode != null && companyCode.Length > 0)
        {
            if (whereClause.Equals(""))
            {
                whereClause = " (CompanyCode in (" + companyCode + ")) ";
            }
            else
            {
                whereClause += " and (CompanyCode in (" + companyCode + ")) ";
            }
        }


        bool res = agent.query(whereClause);
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }

        string[,] orderby = new string[,] { { "CompanyCode", DataObjectConstants.ASC }, { "StartDate", DataObjectConstants.DESC } };
        agent.defaultData.sort(orderby);

        Master.basedos = agent.defaultData;
        Master.getListTable().NoAdd = true;
        Master.getListTable().NoDelete = true;
        Master.getListTable().showExcel = true;
    }


    public void Master_DeleteButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS010.SmpTSTrainingListVAgent"); 
        agent.engine = engine;
        agent.defaultData = Master.basedos;

        bool result = agent.update();
        engine.close();
        if (!result)
        {
            throw new Exception(engine.errorString);
        }
    }
   
}
