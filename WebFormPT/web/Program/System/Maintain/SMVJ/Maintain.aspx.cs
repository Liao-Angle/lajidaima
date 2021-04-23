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
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.auth;
using com.dsc.kernal.global;
using System.IO;
using WebServerProject.maintain.SMVJ;
using System.Xml;

public partial class Program_System_Maintain_SMVJ_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string maintainIdentity = "SMVJ";

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);


                AUTHAgent authagent = new AUTHAgent();
                authagent.engine = engine;

                int auth = authagent.getAuth(maintainIdentity, (string)Session["UserID"], (string[])Session["usergroup"]);

                engine.close();

                if (auth == 0)
                {
                    Response.Redirect("~/NoAuth.aspx");
                }

                //取得AgentFolder資料
                DataObjectSet dos = new DataObjectSet();
                dos.setAssemblyName("WebServerProject");
                dos.setChildClassString("WebServerProject.maintain.SMVJ.SMVJAAA");
                dos.setTableName("SMVJAAA");

                string[] agents = Directory.GetFiles((string)GlobalCache.getValue("AgentFolder"));
                for (int i = 0; i < agents.Length; i++)
                {
                    XMLProcessor xp = new XMLProcessor(agents[i], 0);
                    XmlNode xn = xp.selectSingleNode("/DOS");
                    try
                    {
                        SMVJAAA aa = (SMVJAAA)dos.create();
                        aa.SMVJAAA001 = IDProcessor.getID("");
                        aa.SMVJAAA002 = Utility.getFileName(agents[i]);
                        aa.SMVJAAA003 = xn.Attributes["AssemblyName"].Value;
                        aa.SMVJAAA004 = xn.Attributes["ChildClassString"].Value;
                        aa.SMVJAAA005 = xn.Attributes["TableName"].Value;

                        if (!dos.add(aa))
                        {
                            Response.Write(dos.errorString);
                        }
                    }
                    catch (Exception te)
                    {
                        Response.Write(te.Message);
                    }
                }
                ListTable.InputForm = "Detail.aspx";
                ListTable.HiddenField = new string[] { "SMVJAAA001" };
                //ListTable.ReadOnlyField = ReadOnlyField;
                ListTable.NoAdd = true;
                ListTable.NoDelete = true;
                ListTable.DialogHeight = 700;
                ListTable.DialogWidth = 700;

                ListTable.dataSource = dos;
                ListTable.updateTable();
            }
        }
    }
}
