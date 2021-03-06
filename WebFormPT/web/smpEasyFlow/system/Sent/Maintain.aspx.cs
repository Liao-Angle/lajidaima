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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.flow.data;
using com.dsc.flow.server;
using com.dsc.kernal.agent;
using WebServerProject.flow.SMWY;
using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;

public partial class smpEasyFlow_system_Sent_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //========修改的部分==========
        maintainIdentity = "smpEFSent";
        ApplicationID = "SYSTEM";
        ModuleID = "smpEasyFlow";
        //========修改的部分==========

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                ListTable.CurPanelID = CurPanelID;

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string[,] ids = new string[,]{
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "ids1", "不限定")},
                    {"I",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsI", "進行中")},
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsY", "已結案")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsN", "已終止")},
                    {"W",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsW", "已撤銷")}
                };
                SMWYAAA020.setListItem(ids);

                string sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                SysParam sp = new SysParam(engine);
                string sp7 = sp.getParam("EF2KWebDB");

                AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7);
                DataSet sp7ds = sp7engine.getDataSet("select resca001,resca002 from resca where resca086='2' and resca026='Y' and resca084='1' order by resca001", "TEMP");
                sp7engine.close();

                ids = new string[ds.Tables[0].Rows.Count + 1 + sp7ds.Tables[0].Rows.Count, 2];
                ids[0, 0] = "";
                ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "ids1", "不限定");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1, 0] = ds.Tables[0].Rows[i][0].ToString();
                    ids[i + 1, 1] = ds.Tables[0].Rows[i][1].ToString();
                }
                for (int i = 0; i < sp7ds.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1 + ds.Tables[0].Rows.Count, 0] = sp7ds.Tables[0].Rows[i][0].ToString();
                    ids[i + 1 + ds.Tables[0].Rows.Count, 1] = "(SP7)" + sp7ds.Tables[0].Rows[i][1].ToString();
                }
                SMWYAAA003.setListItem(ids);

                engine.close();

                ListTable.NoDelete = true;
                ListTable.NoAdd = true;

                queryData();
            }
        }
    }

    private void queryData()
    {
        string whereClause = "(SMWYAAA008='" + (string)Session["UserID"] + "' or SMWYAAA012='" + (string)Session["UserID"] + "') ";
        if (!SMWYAAA020.ValueText.Equals(""))
        {
            whereClause += " and SMWYAAA020='" + SMWYAAA020.ValueText + "'";
        }
        if (!SMWYAAA003.ValueText.Equals(""))
        {
            whereClause += " and SMWYAAA003='" + SMWYAAA003.ValueText + "'";
        }
        if (!SMWYAAA006.ValueText.Equals(""))
        {
            whereClause += " and SMWYAAA006 like '%" + SMWYAAA006.ValueText + "%'";
        }
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //國昌: 取得SP7的原稿
        DataSet sp7 = getSP7FormSent(engine);

        SMWYAgent agent = new SMWYAgent();
        agent.engine = engine;
        agent.query(whereClause);

        DataObjectSet dos = agent.defaultData;

        //國昌: 將sp7的資料mapping到SMWYAAA
        for (int i = 0; i < sp7.Tables[0].Rows.Count; i++)
        {
            SMWYAAA ya = (SMWYAAA)dos.create();
            ya.SMWYAAA001 = IDProcessor.getID("");
            ya.ATTACHCOUNT = sp7.Tables[0].Rows[i]["resde002"].ToString();
            ya.SMWYAAA002 = sp7.Tables[0].Rows[i]["resda002"].ToString();
            ya.SMWYAAA003 = sp7.Tables[0].Rows[i]["resda001"].ToString();
            ya.SMWYAAA004 = "\t"+sp7.Tables[0].Rows[i]["resca002"].ToString();
            ya.SMWYAAA005 = sp7.Tables[0].Rows[i]["resda002"].ToString();
            ya.SMWYAAA006 = sp7.Tables[0].Rows[i]["resda031"].ToString();
            ya.SMWYAAA007 = sp7.Tables[0].Rows[i]["resda032"].ToString();
            ya.SMWYAAA008 = sp7.Tables[0].Rows[i]["resda016"].ToString();
            ya.SMWYAAA009 = sp7.Tables[0].Rows[i]["resda_016"].ToString();
            ya.SMWYAAA010 = "-";
            ya.SMWYAAA011 = "-";
            ya.SMWYAAA012 = sp7.Tables[0].Rows[i]["resda017"].ToString();
            ya.SMWYAAA013 = sp7.Tables[0].Rows[i]["resda_017"].ToString();
            ya.SMWYAAA014 = "-";
            ya.SMWYAAA015 = "-";
            ya.SMWYAAA016 = "-";
            ya.SMWYAAA017 = sp7.Tables[0].Rows[i]["resda015"].ToString();
            ya.SMWYAAA018 = "-";
            ya.SMWYAAA019 = "";
            string tempStatus = sp7.Tables[0].Rows[i]["resda021"].ToString();
            if (tempStatus.Equals("1"))
            {
                ya.SMWYAAA020 = "未完成";
            }
            else if (tempStatus.Equals("2"))
            {
                ya.SMWYAAA020 = "同意";
            }
            else if (tempStatus.Equals("3"))
            {
                ya.SMWYAAA020 = "不同意";
            }
            else
            {
                ya.SMWYAAA020 = "已抽單";
            }
            for (int z = 1; z <= 20; z++)
            {
                ya.setData("SMWYAAA1" + string.Format("{0:00}", z), "");
            }
            
            dos.addDraft(ya);
        }

        SMWGAgent ag = new SMWGAgent();
        ag.engine = engine;
        ag.query("");
        DataObjectSet fdos = ag.defaultData;

        string sqls = "select * from SMWKAAA";
        DataSet wk = engine.getDataSet(sqls, "TEMP");

        ArrayList ary = new ArrayList();
        ary.Add("SMWYAAA001");
        for (int i = 2; i <= 20; i++)
        {
            string tag = string.Format("{0:000}", i);
            if (wk.Tables[0].Rows[0]["SMWKAAA" + tag].ToString().Equals("N"))
            {
                ary.Add("SMWYAAA" + tag);
            }
        }

        Hashtable hs = new Hashtable();
        for (int i = 1; i <= 20; i++)
        {
            string tag = "SMWYAAA1" + string.Format("{0:00}", i);
            string fname="";
            bool hasF = false;
            for (int j = 0; j < fdos.getAvailableDataObjectCount(); j++)
            {
                SMWGAAA aa = (SMWGAAA)fdos.getAvailableDataObject(j);
                if (aa.SMWGAAA002.Equals(tag))
                {
                    fname=aa.SMWGAAA004;
                    hasF = true;
                    break;
                }
            }
            if (!hasF)
            {
                ary.Add(tag);
            }
            else
            {
                hs.Add(tag, fname);
            }
        }
        setSession("FieldName", hs);
        
        string[] hField = new string[ary.Count];
        for (int i = 0; i < ary.Count; i++)
        {
            hField[i] = (string)ary[i];
        }

        string[,] orderby = new string[,]{
            {"SMWYAAA017",DataObjectConstants.DESC}
        };
        dos.sort(orderby);

        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            if (!dos.getAvailableDataObject(i).getData("ATTACHCOUNT").Equals(""))
            {
                dos.getAvailableDataObject(i).setData("ATTACHCOUNT", "{[font color=red]}！{[/font]}");
            }
            else
            {
                dos.getAvailableDataObject(i).setData("ATTACHCOUNT", "");
            }
        }

        ListTable.IsGeneralUse = false;
        ListTable.IsPanelWindow = true;
        ListTable.WidthMode = 1;
        ListTable.setColumnStyle("ATTACHCOUNT", 25, DSCWebControl.GridColumnStyle.CENTER);
        ListTable.InputForm = "Detail.aspx";
        ListTable.HiddenField = hField;
        ListTable.dataSource = dos;
        ListTable.updateTable();
        
        engine.close();
    }
    protected void FilterButton_Click(object sender, EventArgs e)
    {
        queryData();
    }
    protected DataObject ListTable_CustomDisplayTitle(DataObject objects)
    {
        Hashtable hs = (Hashtable)getSession("FieldName");

        IDictionaryEnumerator ie = hs.GetEnumerator();
        while (ie.MoveNext())
        {
            string tag = (string)ie.Key;
            string fname = (string)ie.Value;

            for (int i = 0; i < objects.dataField.Length; i++)
            {
                if (tag.Equals(objects.dataField[i]))
                {
                    objects.fieldDefinition[i, 3] = fname;
                }
            }
        }
        return objects;
    }
    protected DSCWebControl.FlowStatusData ListTable_GetFlowStatusString(DataObject objects, bool isAddNew)
    {
        SMWYAAA wi = (SMWYAAA)objects;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //建立FlowStatusData物件
        FlowStatusData fd = new FlowStatusData();

        //取得資料物件代碼, 由原稿取得
        fd.ACTID = "";
        fd.ACTName = "";
        fd.FlowGUID = wi.SMWYAAA005;
        fd.HistoryGUID = "";
        fd.ObjectGUID = wi.SMWYAAA019;
        fd.PDID = wi.SMWYAAA003;
        fd.PDVer = "";
        //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
        //目前都給他為ProcessModify
        fd.UIStatus = FlowStatusData.FormReadOnly;
        fd.WorkItemOID = "";

        Session["tempSMWK"] = wi.SMWYAAA018;

        //這裡要根據SMWDAAA判定是否有ProcessNew
        if (wi.SMWYAAA019.Equals(""))
        {
            ListTable.FormTitle = "\t";
        }
        else
        {
            ListTable.FormTitle = "";
        }
        ListTable.FormTitle +=wi.SMWYAAA004 + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "List", "(單號:") + wi.SMWYAAA002 + ")";
        //engine.close();
        return fd;

    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData();

    }
    private DataSet getSP7FormSent(AbstractEngine engine)
    {
        

        SysParam sp = new SysParam(engine);
        string sp7str = sp.getParam("EF2KWebDB");

        IOFactory factory = new IOFactory();
        AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7str);

        string userid = mappingUserID(engine, sp7engine);

        string strSQL = "";
        strSQL = "select distinct resda.resda001, resda.resda002, resda.resda015, resda.resda016, ";
        strSQL = strSQL + "resda.resda018, resda.resda019, resda.resda020, resda.resda021, resca.resca002, ";
        strSQL = strSQL + " resda.resda031, resda.resda032, resda.resda033, resde.resde002, ";
        strSQL = strSQL + " resda.resda017, ";
        strSQL = strSQL + " resak1.resak002 as resda_016, resak2.resak002 as resda_017 ";
        strSQL = strSQL + " from resda left outer join ";
        strSQL = strSQL + " resde on resda.resda001 = resde.resde001 and  ";
        strSQL = strSQL + " resda.resda002 = resde.resde002 left outer join ";
        strSQL = strSQL + " resak as resak1 ON resda.resda016 = resak1.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resak as resak2 ON resda.resda017 = resak2.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resca on resda.resda001 = resca.resca001 ";
        strSQL = strSQL + " WHERE ";
        strSQL = strSQL + " (NOT (resda.resda001 LIKE '%CRM_%')) and ";
        strSQL = strSQL + " resda.resda016 = '" + userid + "' ";
        strSQL = strSQL + " and resda.resda033 = 'Y' ";
        //strSQL = strSQL + " and (resda.resda001 != 'PTV001' and (resda.resda019 > '2012/12/28' or resda.resda019 <= '2012/12/27')) ";
        //resda033 是否顯示在填表人原稿信箱 , resda019 是表單結案日期


        //if (!StartTime.ValueText.Equals(""))
        //{
        //    strSQL += " AND resdd009>='" + StartTime.ValueText + "' ";
        //}
        //if (!EndTime.ValueText.Equals(""))
        //{
        //    strSQL += " AND resdd009<='" + EndTime.ValueText + "' ";
        //}
        if (!SMWYAAA006.ValueText.Equals(""))
        {
            strSQL += " AND resda031 like '%" + SMWYAAA006.ValueText + "%' ";
        }
        if (!SMWYAAA003.ValueText.Equals(""))
        {
            strSQL += " AND resda001='" + SMWYAAA003.ValueText + "' ";
        }
        if (!SMWYAAA020.ValueText.Equals(""))
        {
            if (SMWYAAA020.ValueText.Equals("I"))
            {
                strSQL += " AND resda020='2' and resda021='1'";
            }
            else if (SMWYAAA020.ValueText.Equals("Y"))
            {
                strSQL += " AND resda020='3' and resda021='2'";
            }
            else if (SMWYAAA020.ValueText.Equals("N"))
            {
                strSQL += " AND resda020='3' and resda021='3'";
            }
            else if (SMWYAAA020.ValueText.Equals("W"))
            {
                strSQL += " AND resda020='4' and resda021='4'";
            }
        }
        DataSet ds = sp7engine.getDataSet(strSQL, "TEMP");

        sp7engine.close();
        return ds;
    }


    //========修改的部分==========
    private string mappingUserID(AbstractEngine engine, AbstractEngine sp7engine)
    {
        string ret = "";
        ret = Convert.ToString(sp7engine.executeScalar("select resak001 from resak with(nolock) where resak001='" + Convert.ToString(Session["UserID"]) + "'"));
        if (ret != "")
        {
            return ret;
        }
        else
        {
            return Convert.ToString(Session["UserID"]);
        }
    }
    //========修改的部分==========
}
