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
using WebServerProject.flow.SMWA;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWA_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWA";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string[,] ids = new string[,]{
            {"0","關閉畫面"},
            {"3","畫面停留"},
            {"4","畫面與工具列停留"}
        };
        SMWAAAA030.setListItem(ids);

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        SMWAAAA008.clientEngineType = engineType;
        SMWAAAA008.connectDBString = connectString;

        SMWAAAA obj = (SMWAAAA)objects;
        SMWAAAA002.ValueText = obj.SMWAAAA002;
        SMWAAAA003.ValueText = obj.SMWAAAA003;
        SMWAAAA004.ValueText = obj.SMWAAAA004;
        SMWAAAA005.ValueText = obj.SMWAAAA005;
        SMWAAAA006.ValueText = obj.SMWAAAA006;
        if (obj.SMWAAAA007.Equals("Y"))
        {
            SMWAAAA007.Checked = true;
        }
        SMWAAAA008.ValueText = obj.SMWAAAA008;
        SMWAAAA008.doValidate();

        SMWAAAA009.ValueText = obj.SMWAAAA009;
        if (obj.SMWAAAA010.Equals("Y"))
        {
            SMWAAAA010.Checked = true;
        }
        else
        {
            SMWAAAA010.Checked = false;
        }

        if (obj.SMWAAAA011.Equals("Y"))
        {
            SMWAAAA011.Checked = true;
        }
        else
        {
            SMWAAAA011.Checked = false;
        }
        if (obj.SMWAAAA012.Equals("Y"))
        {
            SMWAAAA012.Checked = true;
        }
        else
        {
            SMWAAAA012.Checked = false;
        }
        if (obj.SMWAAAA013.Equals("Y"))
        {
            SMWAAAA013.Checked = true;
        }
        else
        {
            SMWAAAA013.Checked = false;
        }
        if (obj.SMWAAAA014.Equals("Y"))
        {
            SMWAAAA014.Checked = true;
        }
        else
        {
            SMWAAAA014.Checked = false;
        }
        if (obj.SMWAAAA015.Equals("Y"))
        {
            SMWAAAA015.Checked = true;
        }
        else
        {
            SMWAAAA015.Checked = false;
        }
        if (obj.SMWAAAA016.Equals("Y"))
        {
            SMWAAAA016.Checked = true;
        }
        else
        {
            SMWAAAA016.Checked = false;
        }
        if (obj.SMWAAAA017.Equals("Y"))
        {
            SMWAAAA017.Checked = true;
        }
        else
        {
            SMWAAAA017.Checked = false;
        }
        if (obj.SMWAAAA018.Equals("Y"))
        {
            SMWAAAA018.Checked = true;
        }
        else
        {
            SMWAAAA018.Checked = false;
        }
        if (obj.SMWAAAA019.Equals("Y"))
        {
            SMWAAAA019.Checked = true;
        }
        else
        {
            SMWAAAA019.Checked = false;
        }
        SMWAAAA021.ValueText = obj.SMWAAAA021;
        SMWAAAA022.ValueText = obj.SMWAAAA022;
        SMWAAAA023.ValueText = obj.SMWAAAA023;
        SMWAAAA024.ValueText = obj.SMWAAAA024;
        SMWAAAA025.ValueText = obj.SMWAAAA025;
        SMWAAAA026.ValueText = obj.SMWAAAA026;
        SMWAAAA027.ValueText = obj.SMWAAAA027;
        SMWAAAA028.ValueText = obj.SMWAAAA028;
        SMWAAAA029.ValueText = obj.SMWAAAA029;
        SMWAAAA030.ValueText = obj.SMWAAAA030;

        bool isAddNew = (bool)getSession("isNew");
        DataObjectSet child = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.flow.SMWA.SMWAAAB");
            child.setTableName("SMWAAAB");
            obj.setChild("SMWAAAB", child);
        }
        else
        {
            child = obj.getChild("SMWAAAB");
        }
        ListTable.HiddenField = new string[] { "SMWAAAB001", "SMWAAAB002" };
        ListTable.NoAdd = true;
        ListTable.NoDelete = true;
        ListTable.dataSource = child;
        ListTable.updateTable();
    }
    protected override void saveData(DataObject objects)
    {
        //檢查主旨格式是否符合
        int countS = 0;
        for (int i = 0; i < SMWAAAA004.ValueText.Length; i++)
        {
            if (SMWAAAA004.ValueText.Substring(i, 1).Equals("#"))
            {
                countS++;
            }
        }
        if ((countS % 2) != 0)
        {
            //throw new Exception("主旨格式不合法");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwa_detail_aspx.language.ini", "message", " QueryError1", "主旨格式不合法"));
        }
        //檢查權限項目
        if (SMWAAAA008.GuidValueText.Equals(""))
        {
            //throw new Exception("必須填寫權限項目");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwa_detail_aspx.language.ini", "message", " QueryError2", "必須填寫權限項目"));
        }
        //檢查AgentSchema
        if (SMWAAAA009.ValueText.Equals(""))
        {
            //throw new Exception("必須填寫AgentSchema");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwa_detail_aspx.language.ini", "message", " QueryError3", "必須填寫AgentSchema"));
        }
        //檢查作業畫面URL
        if (SMWAAAA005.ValueText.Equals(""))
        {
            //throw new Exception("必須填寫作業畫面URL");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwa_detail_aspx.language.ini", "message", " QueryError4", "必須填寫作業畫面URL"));
        }

        SMWAAAA obj = (SMWAAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWAAAA001 = IDProcessor.getID("");
        }

        obj.SMWAAAA002 = SMWAAAA002.ValueText;
        obj.SMWAAAA003 = SMWAAAA003.ValueText;
        obj.SMWAAAA004 = SMWAAAA004.ValueText;
        obj.SMWAAAA005 = SMWAAAA005.ValueText;
        obj.SMWAAAA006 = SMWAAAA006.ValueText;
        if (SMWAAAA007.Checked)
        {
            obj.SMWAAAA007 = "Y";
        }
        else
        {
            obj.SMWAAAA007 = "N";
        }
        obj.SMWAAAA008 = SMWAAAA008.ValueText;
        obj.SMWAAAA009 = SMWAAAA009.ValueText;
        if (SMWAAAA010.Checked)
        {
            obj.SMWAAAA010 = "Y";
        }
        else
        {
            obj.SMWAAAA010 = "N";
        }
        if (SMWAAAA011.Checked)
        {
            obj.SMWAAAA011 = "Y";
        }
        else
        {
            obj.SMWAAAA011 = "N";
        }
        if (SMWAAAA012.Checked)
        {
            obj.SMWAAAA012 = "Y";
        }
        else
        {
            obj.SMWAAAA012 = "N";
        }
        if (SMWAAAA013.Checked)
        {
            obj.SMWAAAA013 = "Y";
        }
        else
        {
            obj.SMWAAAA013 = "N";
        }
        if (SMWAAAA014.Checked)
        {
            obj.SMWAAAA014 = "Y";
        }
        else
        {
            obj.SMWAAAA014 = "N";
        }
        if (SMWAAAA015.Checked)
        {
            obj.SMWAAAA015 = "Y";
        }
        else
        {
            obj.SMWAAAA015 = "N";
        }
        if (SMWAAAA016.Checked)
        {
            obj.SMWAAAA016 = "Y";
        }
        else
        {
            obj.SMWAAAA016 = "N";
        }
        if (SMWAAAA017.Checked)
        {
            obj.SMWAAAA017 = "Y";
        }
        else
        {
            obj.SMWAAAA017 = "N";
        }
        if (SMWAAAA018.Checked)
        {
            obj.SMWAAAA018 = "Y";
        }
        else
        {
            obj.SMWAAAA018 = "N";
        }
        if (SMWAAAA019.Checked)
        {
            obj.SMWAAAA019 = "Y";
        }
        else
        {
            obj.SMWAAAA019 = "N";
        }
        obj.SMWAAAA021 = SMWAAAA021.ValueText;
        obj.SMWAAAA022 = SMWAAAA022.ValueText;
        obj.SMWAAAA023 = SMWAAAA023.ValueText;
        obj.SMWAAAA024 = SMWAAAA024.ValueText;
        obj.SMWAAAA025 = SMWAAAA025.ValueText;
        obj.SMWAAAA026 = SMWAAAA026.ValueText;
        obj.SMWAAAA027 = SMWAAAA027.ValueText;
        obj.SMWAAAA028 = SMWAAAA028.ValueText;
        obj.SMWAAAA029 = SMWAAAA029.ValueText;
        obj.SMWAAAA030 = SMWAAAA030.ValueText;

        DataObjectSet child = ListTable.dataSource;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMWAAAB ab = (SMWAAAB)child.getAvailableDataObject(i);
            ab.SMWAAAB002 = obj.SMWAAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWAAgent agent = new SMWAAgent();
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
    protected void AnalyzeButton_Click(object sender, EventArgs e)
    {
        if (SMWAAAA005.ValueText.Equals(""))
        {
            //MessageBox("請填入作業畫面URL");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwa_detail_aspx.language.ini", "message", " QueryError5", "請填入作業畫面URL"));
            return;
        }
        try
        {
            string curPath = Server.MapPath("~/");
            curPath += SMWAAAA005.ValueText;

            System.IO.StreamReader sr = new System.IO.StreamReader(curPath);
            string html = sr.ReadToEnd();
            sr.Close();

            ArrayList ary = new ArrayList();

            MIL.Html.HtmlDocument doc = MIL.Html.HtmlDocument.Create(html, false);
            fetchChild(doc.Nodes, ary, "");

            DataObjectSet dos = ListTable.dataSource;
            //for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            //{
            //    dos.getAvailableDataObject(i).delete();
            //}

            for (int i = 0; i < ary.Count; i++)
            {
                string[] data = (string[])ary[i];

                bool hasFound = false;
                SMWAAAB tab = null;
                for (int j = 0; j < dos.getAvailableDataObjectCount(); j++)
                {
                    SMWAAAB tp = (SMWAAAB)dos.getAvailableDataObject(j);
                    if (tp.SMWAAAB003.Equals(data[1]))
                    {
                        tab = tp;
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    SMWAAAB ab = (SMWAAAB)dos.create();
                    ab.SMWAAAB001 = IDProcessor.getID("");
                    ab.SMWAAAB002 = "temp";
                    ab.SMWAAAB003 = data[1];
                    ab.SMWAAAB004 = data[0];
                    ab.SMWAAAB005 = "Y";
                    ab.SMWAAAB006 = "N";
                    ab.SMWAAAB007 = "N";
                    ab.SMWAAAB008 = "N";
                    ab.SMWAAAB009 = "Y";
                    ab.SMWAAAB010 = "N";
                    ab.SMWAAAB011 = "N";
                    ab.SMWAAAB012 = "N";
                    dos.add(ab);
                }
                else
                {
                    tab.SMWAAAB003 = data[1];
                    tab.SMWAAAB004 = data[0];
                }
            }

            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                SMWAAAB ab = (SMWAAAB)dos.getAvailableDataObject(i);
                bool hasFound = false;
                for (int j = 0; j < ary.Count; j++)
                {
                    string[] data = (string[])ary[j];

                    if (data[1].Equals(ab.SMWAAAB003))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    ab.delete();
                }
            }
            ListTable.dataSource = dos;
            ListTable.updateTable();
        }
        catch (Exception te)
        {
            Response.Write(te.Message);
        }
    }

    private void fetchChild(MIL.Html.HtmlNodeCollection xnl, ArrayList ary, string prefix)
    {
        MIL.Html.HtmlElement o = new MIL.Html.HtmlElement("HTML");
        foreach (MIL.Html.HtmlNode xn in xnl)
        {
            
            if (xn.GetType().Equals(o.GetType()))
            {
                MIL.Html.HtmlElement he = (MIL.Html.HtmlElement)xn;
                if (prefix.Equals(""))
                {
                    if (he.Name.Equals("%@"))
                    {
                        if (he.Attributes["Namespace"] != null)
                        {
                            if (he.Attributes["Namespace"].Value.Equals("DSCWebControl"))
                            {
                                prefix = he.Attributes["TagPrefix"].Value;
                            }
                        }
                    }
                }
                string tagname = he.Name;
                if (!prefix.Equals(""))
                {
                    if (tagname.IndexOf(prefix) == 0)
                    {
                        string elementName = tagname.Split(new char[] { ':' })[1];
                        string IDs = he.Attributes["ID"].Value;
                        ary.Add(new string[] { elementName, IDs });
                    }
                }
                fetchChild(((MIL.Html.HtmlElement)xn).Nodes, ary, prefix);
            }
            else
            {
                //sw.WriteLine("NotHTML:" + xn.HTML);
            }
        }
    }
    protected void ListTable_ShowRowData(DataObject objects)
    {
        SMWAAAB ab = (SMWAAAB)objects;

        SMWAAAB003.ValueText = ab.SMWAAAB003;
        SMWAAAB004.ValueText = ab.SMWAAAB004;

        if (ab.SMWAAAB005.Equals("Y"))
        {
            SMWAAAB005.Checked = true;
        }
        else
        {
            SMWAAAB005.Checked = false;
        }
        if (ab.SMWAAAB006.Equals("Y"))
        {
            SMWAAAB006.Checked = true;
        }
        else
        {
            SMWAAAB006.Checked = false;
        }
        if (ab.SMWAAAB007.Equals("Y"))
        {
            SMWAAAB007.Checked = true;
        }
        else
        {
            SMWAAAB007.Checked = false;
        }
        if (ab.SMWAAAB008.Equals("Y"))
        {
            SMWAAAB008.Checked = true;
        }
        else
        {
            SMWAAAB008.Checked = false;
        }
        if (ab.SMWAAAB009.Equals("Y"))
        {
            SMWAAAB009.Checked = true;
        }
        else
        {
            SMWAAAB009.Checked = false;
        }
        if (ab.SMWAAAB010.Equals("Y"))
        {
            SMWAAAB010.Checked = true;
        }
        else
        {
            SMWAAAB010.Checked = false;
        }
        if (ab.SMWAAAB011.Equals("Y"))
        {
            SMWAAAB011.Checked = true;
        }
        else
        {
            SMWAAAB011.Checked = false;
        }
        if (ab.SMWAAAB012.Equals("Y"))
        {
            SMWAAAB012.Checked = true;
        }
        else
        {
            SMWAAAB012.Checked = false;
        }
    }
    protected bool ListTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWAAAB ab = (SMWAAAB)objects;

        if (SMWAAAB005.Checked)
        {
            ab.SMWAAAB005 = "Y";
        }
        else
        {
            ab.SMWAAAB005 = "N";
        }
        if (SMWAAAB006.Checked)
        {
            ab.SMWAAAB006 = "Y";
        }
        else
        {
            ab.SMWAAAB006 = "N";
        }
        if (SMWAAAB007.Checked)
        {
            ab.SMWAAAB007 = "Y";
        }
        else
        {
            ab.SMWAAAB007 = "N";
        }
        if (SMWAAAB008.Checked)
        {
            ab.SMWAAAB008 = "Y";
        }
        else
        {
            ab.SMWAAAB008 = "N";
        }
        if (SMWAAAB009.Checked)
        {
            ab.SMWAAAB009 = "Y";
        }
        else
        {
            ab.SMWAAAB009 = "N";
        }
        if (SMWAAAB010.Checked)
        {
            ab.SMWAAAB010 = "Y";
        }
        else
        {
            ab.SMWAAAB010 = "N";
        }
        if (SMWAAAB011.Checked)
        {
            ab.SMWAAAB011 = "Y";
        }
        else
        {
            ab.SMWAAAB011 = "N";
        }
        if (SMWAAAB012.Checked)
        {
            ab.SMWAAAB012 = "Y";
        }
        else
        {
            ab.SMWAAAB012 = "N";
        }

        return true;
    }
}
