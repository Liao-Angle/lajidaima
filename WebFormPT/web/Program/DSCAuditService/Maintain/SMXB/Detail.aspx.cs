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
using WebServerProject.audit.SMXB;

public partial class Program_DSCAuditService_Maintain_SMXB_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMXB";
        ApplicationID = "SYSTEM";
        ModuleID = "SMXAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            connectString = (string)Session["connectString"];
            engineType = (string)Session["engineType"];

            SMXBAAA002.clientEngineType = engineType;
            SMXBAAA002.connectDBString = connectString;

            OpenWin1.clientEngineType = engineType;
            OpenWin1.connectDBString = connectString;
            OpenWin2.clientEngineType = engineType;
            OpenWin2.connectDBString = connectString;

            SMXBAAA obj = (SMXBAAA)objects;
            bool isAddNew = (bool)getSession("isNew");
            DataObjectSet dos = null;
            if (isAddNew)
            {
                dos = new DataObjectSet();
                dos.setAssemblyName("WebServerProject");
                dos.setChildClassString("WebServerProject.audit.SMXB.SMXBAAB");
                dos.setTableName("SMXBAAB");

                obj.setChild("SMXBAAB", dos);
            }
            else
            {
                dos = obj.getChild("SMXBAAB");
            }

            SMXBAAA002.GuidValueText = obj.SMXBAAA002;
            SMXBAAA002.doGUIDValidate();

            SMXBAAA003.ValueText = obj.SMXBAAA003;
            SMXBAAA004.ValueText = obj.SMXBAAA004;
            SMXBAAA005.ValueText = obj.SMXBAAA005;

            if (obj.SMXBAAA006.Equals("Y"))
            {
                SMXBAAA006.Checked = true;
            }
            else
            {
                SMXBAAA006.Checked = false;
            }
            if (obj.SMXBAAA007.Equals("Y"))
            {
                SMXBAAA007.Checked = true;
            }
            else
            {
                SMXBAAA007.Checked = false;
            }
            if (obj.SMXBAAA008.Equals("Y"))
            {
                SMXBAAA008.Checked = true;
            }
            else
            {
                SMXBAAA008.Checked = false;
            }

            DetailList.HiddenField = new string[] { "SMXBAAB001", "SMXBAAB002" };
            DetailList.dataSource = dos;
            DetailList.updateTable();

            string[,] ids = new string[10, 2];
            for (int i = 0; i < 10; i++)
            {
                ids[i, 0] = i.ToString();
                ids[i, 1] = i.ToString();
            }

            SMXBAAB004.setListItem(ids);
        }
        catch (Exception ze)
        {
            Response.Write("alert('"+ze.Message+"');");
        }
    }

    protected override void saveData(DataObject objects)
    {

        bool isAddNew = (bool)getSession("isNew");
        SMXBAAA obj = (SMXBAAA)objects;
        if (isAddNew)
        {
            obj.SMXBAAA001 = IDProcessor.getID("");
        }
        obj.SMXBAAA002 = SMXBAAA002.GuidValueText;
        obj.SMXAAAA002 = SMXBAAA002.ValueText;
        obj.SMXAAAA003 = SMXBAAA002.ReadOnlyValueText;
        obj.SMXBAAA003 = SMXBAAA003.ValueText;
        obj.SMXBAAA004 = SMXBAAA004.ValueText;
        obj.SMXBAAA005 = SMXBAAA005.ValueText;
        if (SMXBAAA006.Checked)
        {
            obj.SMXBAAA006 = "Y";
        }
        else
        {
            obj.SMXBAAA006 = "N";
        }
        if (SMXBAAA007.Checked)
        {
            obj.SMXBAAA007 = "Y";
        }
        else
        {
            obj.SMXBAAA007 = "N";
        }
        if (SMXBAAA008.Checked)
        {
            obj.SMXBAAA008 = "Y";
        }
        else
        {
            obj.SMXBAAA008 = "N";
        }

        for (int i = 0; i < DetailList.dataSource.getAvailableDataObjectCount(); i++)
        {
            SMXBAAB ab = (SMXBAAB)DetailList.dataSource.getAvailableDataObject(i);
            ab.SMXBAAB002 = obj.SMXBAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMXBAgent agent = new SMXBAgent();
        agent.engine = engine;
        agent.query("1=2");

        //取得資料
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
    protected void DetailList_ShowRowData(DataObject objects)
    {

        SMXBAAB ab = (SMXBAAB)objects;
        SMXBAAB003.ValueText = ab.SMXBAAB003;
        SMXBAAB004.ValueText = ab.SMXBAAB004;
        SMXBAAB005.ValueText = ab.SMXBAAB005;

        if (ab.SMXBAAB006.Equals("Y"))
        {
            SMXBAAB006.Checked = true;
        }
        else
        {
            SMXBAAB006.Checked = false;
        }
        if (ab.SMXBAAB007.Equals("Y"))
        {
            SMXBAAB007.Checked = true;
        }
        else
        {
            SMXBAAB007.Checked = false;
        }
        if (ab.SMXBAAB008.Equals("Y"))
        {
            SMXBAAB008.Checked = true;
        }
        else
        {
            SMXBAAB008.Checked = false;
        }
    }
    protected bool DetailList_SaveRowData(DataObject objects, bool isNew)
    {
        SMXBAAB ab = (SMXBAAB)objects;
        if (isNew)
        {
            ab.SMXBAAB001 = IDProcessor.getID("");
            ab.SMXBAAB002 = "temp";
        }
        ab.SMXBAAB003 = SMXBAAB003.ValueText;
        ab.SMXBAAB004 = SMXBAAB004.ValueText;
        ab.SMXBAAB005 = SMXBAAB005.ValueText;
        if (SMXBAAB006.Checked)
        {
            ab.SMXBAAB006 = "Y";
        }
        else
        {
            ab.SMXBAAB006 = "N";
        }
        if (SMXBAAB007.Checked)
        {
            ab.SMXBAAB007 = "Y";
        }
        else
        {
            ab.SMXBAAB007 = "N";
        }
        if (SMXBAAB008.Checked)
        {
            ab.SMXBAAB008 = "Y";
        }
        else
        {
            ab.SMXBAAB008 = "N";
        }

        return true;
    }
    protected void GlassButton1_Click(object sender, EventArgs e)
    {
        OpenWin1.PageUniqueID = this.PageUniqueID;
        OpenWin1.identityID = "0001";
        OpenWin1.paramString = "id";
        OpenWin1.openWin("Users", "001", true, "0001");

    }
    protected void OpenWin1_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            string tag = "";
            for (int i = 0; i < values.GetLength(0); i++)
            {
                tag += values[i, 1] + ";";
            }
            if (tag.Length > 0)
            {
                tag = tag.Substring(0, tag.Length - 1);
            }

            if (SMXBAAA005.ValueText.Equals(""))
            {
                SMXBAAA005.ValueText = tag;
            }
            else
            {
                SMXBAAA005.ValueText += ";" + tag;
            }
        }
    }
    protected void OpenWin2_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            string tag = "";
            for (int i = 0; i < values.GetLength(0); i++)
            {
                tag += values[i, 1] + ";";
            }
            if (tag.Length > 0)
            {
                tag = tag.Substring(0, tag.Length - 1);
            }

            if (SMXBAAB005.ValueText.Equals(""))
            {
                SMXBAAB005.ValueText = tag;
            }
            else
            {
                SMXBAAB005.ValueText += ";" + tag;
            }
        }
    }
    protected void GlassButton2_Click(object sender, EventArgs e)
    {
        OpenWin2.PageUniqueID = this.PageUniqueID;
        OpenWin2.identityID = "0001";
        OpenWin2.paramString = "id";
        OpenWin2.openWin("Users", "001", true, "0001");

    }
}
