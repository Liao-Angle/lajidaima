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

public partial class SampleProgram_ProjectMaintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                AbstractEngine engine = null;
                try
                {
                    IOFactory factory = new IOFactory();
                    engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);

                    NLAgent agent = new NLAgent();
                    agent.loadSchema("WebServerProject.Sample.ProjectAgent");
                    agent.engine = engine;
                    agent.query("1=2");
                    DataObjectSet dos = agent.defaultData;


                    engine.close();

                    ProjectList.dataSource = dos;
                    ProjectList.HiddenField = new string[] { "GUID", "LeaderGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
                    ProjectList.InputForm = "Input.aspx";
                    ProjectList.updateTable();
                }
                catch (Exception ue)
                {
                    try
                    {
                        engine.close();
                    }
                    catch { };
                }
            }
        }
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        try
        {
            engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);

            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.Sample.ProjectAgent");
            agent.engine = engine;
            agent.query("");
            DataObjectSet dos = agent.defaultData;

            engine.close();

            ProjectList.dataSource = dos;
            ProjectList.updateTable();
        }
        catch (Exception ue)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(ue.Message);
        }

    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;

        try
        {
            engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);

            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.Sample.ProjectAgent");
            agent.engine = engine;
            agent.defaultData = ProjectList.dataSource;
            agent.update();

            engine.close();
            MessageBox("儲存成功");
        }
        catch (Exception ue)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(ue.Message);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}
