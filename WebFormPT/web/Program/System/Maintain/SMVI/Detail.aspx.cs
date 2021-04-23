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
using WebServerProject.maintain.SMVI;
using System.Xml;

public partial class Program_System_Maintain_SMVI_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVI";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];


        SMVIAAA obj = (SMVIAAA)objects;
        SMVIAAA002.ValueText = obj.SMVIAAA002;
        SMVIAAA003.ValueText = obj.SMVIAAA003;
        SMVIAAA004.ValueText = obj.SMVIAAA004;

        bool isAddNew = (bool)getSession("isNew");
        DataObjectSet child = null;
        DataObjectSet cc = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.maintain.SMVI.SMVIAAB");
            child.setTableName("SMVIAAB");
            obj.setChild("SMVIAAB", child);

            cc = new DataObjectSet();
            cc.setAssemblyName("WebServerProject");
            cc.setChildClassString("WebServerProject.maintain.SMVI.SMVIAAC");
            cc.setTableName("SMVIAAC");
            obj.setChild("SMVIAAC", cc);
        }
        else
        {
            child = obj.getChild("SMVIAAB");
            cc = obj.getChild("SMVIAAC");
        }

        ListTable.HiddenField = new string[] { "SMVIAAB001", "SMVIAAB002","SMVIAAB007" };
        ListTable.dataSource = child;
        ListTable.updateTable();
        reOrder();

        string[,] ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "ids0", "字軌")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "ids1", "西元年")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "ids2", "民國年")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "ids3", "月")},
            {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "ids4", "日")},
            {"5",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "ids5", "流水號")},
            {"6","SQL"},
            {"7",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "ids7", "字串替換")}
        };
        SMVIAAB004.setListItem(ids);
    }
    protected override void saveData(DataObject objects)
    {
        //要檢查不可以有兩組流水號
        DataObjectSet child = ListTable.dataSource;
        int sCount = 0;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMVIAAB ab = (SMVIAAB)child.getAvailableDataObject(i);
            if (ab.SMVIAAB004.Equals("5"))
            {
                sCount++;
            }
        }
        if (sCount != 1)
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "QueryError1", "必須要有一組流水號, 且僅能為一組"));
        }

        //要檢查流水號必須為最後一個
        if (child.getAvailableDataObjectCount() < 2)
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "QueryError2", "必須要有一組流水號, 一組非流水號"));
        }
        if (!child.getAvailableDataObject(child.getAvailableDataObjectCount() - 1).getData("SMVIAAB004").Equals("5"))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvi_detail_aspx.language.ini", "message", "QueryError3", "最後一組必須為流水號"));
        }


        SMVIAAA obj = (SMVIAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVIAAA001 = IDProcessor.getID("");
        }

        obj.SMVIAAA002 = SMVIAAA002.ValueText;
        obj.SMVIAAA003 = SMVIAAA003.ValueText;
        obj.SMVIAAA004 = SMVIAAA004.ValueText;

        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMVIAAB ab = (SMVIAAB)child.getAvailableDataObject(i);
            ab.SMVIAAB002 = obj.SMVIAAA001;
        }
        reOrder();

        //解析參數
        ArrayList param = new ArrayList();
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMVIAAB ab = (SMVIAAB)child.getAvailableDataObject(i);
            if ((ab.SMVIAAB004.Equals("6")) || (ab.SMVIAAB004.Equals("7")))
            {
                anayParam(param, ab.SMVIAAB007);
            }
        }

        DataObjectSet cc = objects.getChild("SMVIAAC");
        for (int i = 0; i < cc.getAvailableDataObjectCount(); i++)
        {
            cc.getAvailableDataObject(i).setData("SMVIAAC002", obj.SMVIAAA001);
            cc.getAvailableDataObject(i).delete();
        }

        for (int i = 0; i < param.Count; i++)
        {
            
            string tag = (string)param[i];
            SMVIAAC ac = (SMVIAAC)cc.create();
            ac.SMVIAAC001 = IDProcessor.getID("");
            ac.SMVIAAC002 = obj.SMVIAAA001;
            ac.SMVIAAC003 = tag;
            ac.INSERTUSER = (string)Session["UserID"];
            cc.add(ac);
        }

        
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVIAgent agent = new SMVIAgent();
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
    protected void ListTable_ShowRowData(DataObject objects)
    {
        SMVIAAB ab = (SMVIAAB)objects;

        SMVIAAB003.ValueText = ab.SMVIAAB003;
        SMVIAAB004.ValueText = ab.SMVIAAB004;
        SMVIAAB005.ValueText = ab.SMVIAAB005;
        if (ab.SMVIAAB006.Equals("Y"))
        {
            SMVIAAB006.Checked = true;
        }
        else
        {
            SMVIAAB006.Checked = false;
        }
        SMVIAAB007.ValueText = ab.SMVIAAB007;
    }
    protected bool ListTable_SaveRowData(DataObject objects, bool isNew)
    {
        string tv=SMVIAAB003.ValueText;
        if ((tv.Equals("1")) || (tv.Equals("2")) || (tv.Equals("3")) || (tv.Equals("4")) || (tv.Equals("5")))
        {
            if (SMVIAAB007.ValueText.Equals(""))
            {
                MessageBox("必須填寫內容");
                return false;
            }
        }
        if (int.Parse(SMVIAAB005.ValueText) < 1)
        {
            MessageBox("字軌長度必須大於等於1");
            return false;
        }
        SMVIAAB ab = (SMVIAAB)objects;

        if (isNew)
        {
            ab.SMVIAAB001 = IDProcessor.getID("");
            ab.SMVIAAB002 = "TEMP";
        }
        ab.SMVIAAB003 = SMVIAAB003.ValueText;
        ab.SMVIAAB004 = SMVIAAB004.ValueText;
        ab.SMVIAAB005 = SMVIAAB005.ValueText;
        if (SMVIAAB006.Checked)
        {
            ab.SMVIAAB006 = "Y";
        }
        else
        {
            ab.SMVIAAB006 = "N";
        }
        ab.SMVIAAB007 = SMVIAAB007.ValueText;

        return true;
    }

    private void reOrder()
    {
        DataObjectSet dos = ListTable.dataSource;
        string[,] orderby = new string[,]{
            {"SMVIAAB003",DataObjectConstants.ASC}
        };
        dos.sort(orderby);

        int ss = 1;
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            SMVIAAB ab = (SMVIAAB)dos.getAvailableDataObject(i);
            ab.SMVIAAB003 = string.Format("{0:00}", ss);

            ss++;
        }

        ListTable.updateTable();
    }
    protected void RefreshOrderButton_Click(object sender, EventArgs e)
    {
        reOrder();
    }
    protected void ListTable_AddOutline(DataObject objects, bool isNew)
    {
        reOrder();
    }
    protected void anayParam(ArrayList ary, string cont)
    {
        string tag = "";
        bool isIn = false;
        for (int i = 0; i < cont.Length; i++)
        {
            string z = cont.Substring(i, 1);
            if (z.Equals("#"))
            {
                if (isIn)
                {
                    ary.Add(tag);
                    isIn = false;
                    tag = "";
                }
                else
                {
                    isIn = true;
                }
            }
            else
            {
                if (isIn)
                {
                    tag += z;
                }
            }
        }
    }
}
