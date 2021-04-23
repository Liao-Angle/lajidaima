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
using com.dsc.kernal.global;
using System.IO;
using System.Xml;
using WebServerProject.auth;
using NPOI.HSSF.UserModel; 

using com.dsc.flow.data;
using com.dsc.flow.server;
using WebServerProject.flow.SMWM;
using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;	


public partial class SmpProgram_Maintain_SPSYS001M_Maintain : BaseWebUI.GeneralWebPage                     
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
                //取得User有權限查詢的代業畫面代號/名稱
                string[,] ids = null;                 
                string sql = null;
                DataSet ds = null;
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);    
                string userId = (string)Session["UserId"];
              
                sql = "select SMWAAAA009,SMWAAAA003 "+
                      "From Users U "+
                      "JOIN SmpFormQuery F on U.OID= F.UserGUID "+
                      "join SMWAAAA A on F.FlowGUID = A.SMWAAAA001 "+                     
                      "where U.id = '" + userId + "'" +
                      "and PrivilegeType ='Maintain' "+
                      "and Active='Y' ";                
                ds = engine.getDataSet(sql, "TEMP");                                            
                int count = ds.Tables[0].Rows.Count;
                if (count > 0)
                {                    
                    ids = new string[count, 2];                  
                    for (int i = 0; i < count; i++)
                    {
                        ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                        ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
                    }
                    AgentSchemaId.setListItem(ids);
                    AgentSchemaId.ValueText = ids[0,0];
                    start();
					
                }else{                    
                    Response.Redirect("~/NoAuth.aspx");
                }
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void start()
    {
        Master.maintainIdentity = "SPSYS001M";
        Master.ApplicationID = "SYSTEM";
        Master.ModuleID = "SMVAA";  

        DataObject obj = new DataObject();
        string doSchema = AgentSchemaId.ValueText.Replace("Agent","");
        obj.loadFileSchema(doSchema);

        //解析AgentSchema
        string[] tgs = AgentSchemaId.ValueText.Split(new char[] { '.' });
        string fp = (string)GlobalCache.getValue("DataObjectFolder");
       
        for (int i = 0; i < (tgs.Length - 1); i++)
        {
            fp += tgs[i] + "\\";            
        }
        fp += doSchema + ".xml";
				
        XMLProcessor xp = new XMLProcessor(fp, 0);
       
        //欄位清單
        XmlNodeList it = xp.selectAllNodes("//DataObject/smpQueryHideField/field");
        string[] ids = new string[it.Count];
		string[] strTest = new string[it.Count];
        int ccs = 0;
        foreach (XmlNode xts in it)
        {
            ids[ccs] = xts.Attributes["dataField"].Value;  
			ccs++;
        }
		
		Master.inputForm = "Detail.aspx";   
		
        Master.HiddenField = ids;		
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
        agent.loadSchema(AgentSchemaId.ValueText); 
        agent.engine = engine;
		
        bool res = agent.query(whereClause);		
		
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }      

        Master.basedos = agent.defaultData;
		DataObject obj = null;
        int count = Master.basedos.getAvailableDataObjectCount();
		
		//MessageBox("count : " + count);

        if (count > 0)
        {
            obj = Master.basedos.getAvailableDataObject(0);
            //MessageBox("GUID : " + obj.getData("GUID"));
        }
		
		Master.getListTable().NoAdd = true;
        Master.getListTable().NoDelete = true;
        Master.getListTable().showExcel = true;
		Master.getListTable().NoModify = true;
		
    }


    public void Master_DeleteButtonClick()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema( AgentSchemaId.ValueText ); 
        agent.engine = engine;
        agent.defaultData = Master.basedos;

        bool result = agent.update();
        engine.close();
        if (!result)
        {
            throw new Exception(engine.errorString);
        }
    }

    protected void AgentSchemaId_SelectChanged(string value)
    {      
        DSCWebControl.QueryPage qp = Master.getQueryPage();
        qp.clearQuery();       
        start();       
    }

    
}
