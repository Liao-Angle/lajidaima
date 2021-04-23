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
//using WebServerProject.maintain.SMVKA;
using System.Collections.Generic;
using System.Linq;
using com.dsc.kernal.agent;
using WebServerProject.auth;

public partial class SmpProgram_Maintain_SPAD002_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                initData();
            }
        }
    }

    public void initData()
    {		
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //SMVKAAgent agent = new SMVKAAgent();
	NLAgent agent1 = new NLAgent();
        agent1.loadSchema("WebServerProject.maintain.SPAD002.SMVKAAAAgent");		
        agent1.engine = engine;
		
        //string whereClause = " SMVKAAA002 = '" + (string)Session["UserGUID"] + "' order by A.D_INSERTUSER ";
        //bool res = agent1.query(whereClause);
		//Page.Response.Write("alert('whereClause:" + whereClause + "');");		
        engine.close();

        if (!engine.errorString.Equals(""))
        {
            throw new Exception(engine.errorString);
        }
        dateList.DialogHeight = 480;
        dateList.DialogWidth = 750;
        dateList.InputForm = "Detail.aspx";
        dateList.HiddenField = new string[] { "SMVKAAA001", "SMVKAAA002", "userId", "userName", "SMVKAAA005", "SMVKAAA006" };
        dateList.dataSource = agent1.defaultData;
        dateList.updateTable();
    }

    protected void dateList_DeleteData()
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
		NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPAD002.SMVKAAAAgent");
        //agent.engine = engine;
		
        //SMVKAAgent agent = new SMVKAAgent();
        agent.engine = engine;
        agent.defaultData = dateList.dataSource;

        bool result = agent.update();
        if (!result)
        {
            throw new Exception(engine.errorString);
        }
        engine.close();
    }
}
