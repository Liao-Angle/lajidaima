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
using WebServerProject.flow.SMWR;
using WebServerProject;
using DSCWebControl;

public partial class SmpProgram_Maintain_SPAD013_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //CL_Chang 2013/7/12
        maintainIdentity = "SPAD013";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string BoxID = "";
                if (Request.QueryString["BoxID"] == null)
                {
                    setSession("BoxID","");
                    BoxID = "";
                }
                else
                {
                    setSession("BoxID", Request.QueryString["BoxID"]);
                    BoxID = Request.QueryString["BoxID"];
                }

                ListTable.CurPanelID = CurPanelID;

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                OriginatorGUID.clientEngineType = engineType;
                OriginatorGUID.connectDBString = connectString;

                string[,] ids = new string[,]{
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "ids1", "不限定")},
                    {"I",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsI", "進行中")},
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsY", "已結案")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsN", "已終止")},
                    {"W",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "idsW", "已撤銷")}
                };
                SMWYAAA020.setListItem(ids);

                ids = new string[,]{
                    {"N","一般"},
                    {"Y","已刪除"}
                };
                SMWYAAA022.setListItem(ids);

                string sql = "";

                //國昌2009/09/22:增加原稿設定
                sql = "select SMVPAAA027, SMVPAAA028, SMVPAAA029 from SMVPAAA";
                DataSet tmp = engine.getDataSet(sql, "TEMP");
                bool isDefaultReadAll = true;
                int maxDataCount = 100;
                bool isConstraints = false;
                if (tmp.Tables[0].Rows[0]["SMVPAAA027"].ToString().Equals("Y"))
                {
                    isDefaultReadAll = true;
                }
                else
                {
                    isDefaultReadAll = false;
                }
                try
                {
                    maxDataCount = int.Parse(tmp.Tables[0].Rows[0]["SMVPAAA028"].ToString());
                }
                catch { };
                if (tmp.Tables[0].Rows[0]["SMVPAAA029"].ToString().Equals("Y"))
                {
                    isConstraints = true;
                }
                else
                {
                    isConstraints = false;
                }
                setSession("isDefaultReadAll", isDefaultReadAll);
                setSession("maxDataCount", maxDataCount);
                setSession("isConstraints", isConstraints);

                if (BoxID.Equals(""))
                {
                    //CL_Chang 2013/7/12 只列出有權限的流程清單
                    sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA,Users,SmpFormQuery where id='" + (string)Session["UserID"] + "' and SMWBAAA001 = FlowGUID and OID=UserGUID and Active='Y'";
                }
                else
                {
                    SMWRAgent agent = new SMWRAgent();
                    agent.engine = engine;
                    agent.query("SMWRAAA022='" + BoxID + "'");
                    DataObjectSet dos = agent.defaultData;

                    if (dos.getAvailableDataObjectCount() == 0)
                    {
                        engine.close();
                        Response.Redirect("NoSetting.aspx");
                    }
                    SMWRAAA aa=(SMWRAAA)dos.getAvailableDataObject(0);
                    DataObjectSet child = aa.getChild("SMWRAAB");
                    string instr = "";
                    if (child.getAvailableDataObjectCount() > 0)
                    {
                        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
                        {
                            instr += "'" + child.getAvailableDataObject(i).getData("SMWRAAB003") + "',";
                        }
                        if (instr.Length > 0)
                        {
                            instr = instr.Substring(0, instr.Length - 1);
                        }
                        setSession("instr", instr);

                        //CL_Chang 2013/7/12 只列出有權限的流程清單
                        if (aa.SMWRAAA024.Equals("1"))
                        {
                            sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA,SmpFormQuery,Users where SMWBAAA003 in (" + instr + ") and id = '" + (string)Session["UserID"] + "' and Active='Y' and SMWBAAA001 = FlowGUID and OID=UserGUID";
                            setSession("ProcessMode", "1");
                        }
                        else
                        {
                            sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA,SmpFormQuery,Users where SMWBAAA003 not in (" + instr + ") and id = '" + (string)Session["UserID"] + "' and Active='Y' and SMWBAAA001 = FlowGUID and OID=UserGUID";
                            setSession("ProcessMode", "0");
                        }
                        setSession("SMWRAAA", aa);
                    }
                    else 
                    {
                        sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where 1=2";
                        MessageBox("未設定該資料夾對應之流程");
                    }                   
                }

                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0) {
                    ids = new string[ds.Tables[0].Rows.Count + 1, 2];
                    ids[0, 0] = "";
                    ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "ids1", "不限定");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ids[i + 1, 0] = ds.Tables[0].Rows[i][0].ToString();
                        ids[i + 1, 1] = ds.Tables[0].Rows[i][1].ToString();
                    }
                    SMWYAAA003.setListItem(ids);

                    engine.close();

                    ListTable.NoDelete = true;
                    ListTable.NoAdd = true;

                    //CL_Chang 2013/7/12 預設不執行查詢
                    //queryData(true);
                }                
            }
        }
    }

    private void queryData(bool isFirst)
    {
        string BoxID=(string)getSession("BoxID");

        //string whereClause = "(SMWYAAA008='" + (string)Session["UserID"] + "' or SMWYAAA012='" + (string)Session["UserID"] + "') ";
        string whereClause = "(1=1) ";
        if (!SMWYAAA020.ValueText.Equals(""))
        {
            whereClause += " and SMWYAAA020='" + SMWYAAA020.ValueText + "'";
        }

        if (!SMWYAAA003.ValueText.Equals(""))
        {
            whereClause += " and SMWYAAA003='" + SMWYAAA003.ValueText + "'";
        }
        else
        {
            if (!BoxID.Equals(""))
            {
                string instr = (string)getSession("instr");
                string ProcessMode = (string)getSession("ProcessMode");
                if (ProcessMode.Equals("0"))
                {
                    whereClause += " and SMWYAAA003 not in (" + instr + ")";
                }
                else
                {
                    whereClause += " and SMWYAAA003 in (" + instr + ")";
                }
            }
        }
        if (!SMWYAAA006.ValueText.Equals(""))
        {
            whereClause += " and SMWYAAA006 like '%" + SMWYAAA006.ValueText + "%'";
        }

        //CL_Chang 2013/7/12 增加查詢條件
        //單號
        if (!SheetNo.ValueText.Equals(""))
        {
            whereClause += " and SMWYAAA002 = '" + SheetNo.ValueText + "'";
        }

        //填表人/關系人
        if (!OriginatorGUID.ValueText.Equals(""))
        {
            whereClause += " and (SMWYAAA008='" + OriginatorGUID.ValueText + "' or SMWYAAA012='" + OriginatorGUID.ValueText + "') ";
        }

        //建立時間
        if (!StartTime.ValueText.Equals(""))
        {
            whereClause += " and D_INSERTTIME >= " + "'" + StartTime.ValueText + "' ";
        }

        if (!EndTime.ValueText.Equals(""))
        {
            whereClause += " and D_INSERTTIME >= " + "'" + EndTime.ValueText + "' ";
        }

        //只能查有開放權限的流程
        whereClause += " and exists (select 'x' from SMWBAAA,Users,SmpFormQuery where id = '" + (string)Session["UserID"] + "' and Active='Y' and SMWBAAA001 = FlowGUID and OID=UserGUID and SMWYAAA003=SMWBAAA003) ";
        
        //CL_Chang 2013/7/12 增加查詢條件 End

        whereClause += " and SMWYAAA022='" + SMWYAAA022.ValueText + "'";

        if (isFirst)
        {
            bool isDefaultReadAll = (bool)getSession("isDefaultReadAll");
            if (!isDefaultReadAll)
            {
                whereClause += " and (1=2)";
            }
        }
        whereClause += " order by D_INSERTTIME DESC";
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        SMWYAgent agent = new SMWYAgent();
        agent.engine = engine;
        int maxDataCount = (int)getSession("maxDataCount");
        if (maxDataCount == -1)
        {
            agent.query(whereClause);
        }
        else
        {
            agent.query(whereClause, maxDataCount);
        }

        DataObjectSet dos = agent.defaultData;

        SMWGAgent ag = new SMWGAgent();
        ag.engine = engine;
        ag.query("");
        DataObjectSet fdos = ag.defaultData;


        ArrayList ary = new ArrayList();

        DataSet wk = null;
        if (BoxID.Equals(""))
        {
            string sqls = "select * from SMWKAAA";
            wk = engine.getDataSet(sqls, "TEMP");

            ary.Add("SMWYAAA001");
            for (int i = 2; i <= 20; i++)
            {
                string tag = string.Format("{0:000}", i);
                if (wk.Tables[0].Rows[0]["SMWKAAA" + tag].ToString().Equals("N"))
                {
                    ary.Add("SMWYAAA" + tag);
                }
            }
            //特別處理SMWKAAA021附件
            if (wk.Tables[0].Rows[0]["SMWKAAA021"].ToString().Equals("N"))
            {
                ary.Add("ATTACHCOUNT");
            }
        }
        else
        {
            SMWRAAA aa = (SMWRAAA)getSession("SMWRAAA");
            ary.Add("SMWYAAA001");
            for (int i = 2; i <= 20; i++)
            {
                string tag = string.Format("{0:000}", i);
                if (aa.getData("SMWRAAA"+tag).Equals("N"))
                {
                    ary.Add("SMWYAAA" + tag);
                }
            }
            //特別處理SMWKAAA021附件
            if (aa.SMWRAAA021.Equals("N"))
            {
                ary.Add("ATTACHCOUNT");
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

        //處理欄位順序
        string[] orderList;
        if (BoxID.Equals(""))
        {
            orderList = wk.Tables[0].Rows[0]["SMWKAAA200"].ToString().Split(new char[] { ';' });
        }
        else
        {
            SMWRAAA aa = (SMWRAAA)getSession("SMWRAAA");
            orderList = aa.SMWRAAA200.Split(new char[] { ';' });
        }
        ListTable.reOrderField(orderList);

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
        ListTable.setColumnStyle("ATTACHCOUNT", 30, DSCWebControl.GridColumnStyle.CENTER);
        ListTable.WidthMode = 1;
        ListTable.IsGeneralUse = false;
        ListTable.IsPanelWindow = true;
        ListTable.InputForm = "Detail.aspx";
        ListTable.HiddenField = hField;
        ListTable.dataSource = dos;
        ListTable.updateTable();
        setSession("CURLIST", dos);
        engine.close();
    }
    protected void FilterButton_Click(object sender, EventArgs e)
    {
        bool isContraints = (bool)getSession("isConstraints");
        if (isContraints)
        {
            if ((SMWYAAA020.ValueText.Equals("")) && (SMWYAAA003.ValueText.Equals("")) && (SMWYAAA006.ValueText.Trim().Equals("")))
            {
                MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "NeedConstraints", "必須輸入任一條件"));
                return;
            }
        }
        queryData(false);
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
        fd.HistoryGUID = tb.Text;
        fd.ObjectGUID = wi.SMWYAAA019;
        fd.PDID = wi.SMWYAAA003;
        fd.PDVer = "";
        //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
        //目前都給他為ProcessModify
        fd.UIStatus = FlowStatusData.FormReadOnly;
        fd.WorkItemOID = "";

        Session["tempSMWK"] = wi.SMWYAAA018;

        //這裡要根據SMWDAAA判定是否有ProcessNew

        ListTable.FormTitle = wi.SMWYAAA004 + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "List", "(單號:") + wi.SMWYAAA002 + ")";
        //engine.close();
        return fd;

    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData(false);

    }
    protected void ToggleButton_Click(object sender, EventArgs e)
    {
        DataObject[] ddo=ListTable.getSelectedItem();
        if (ddo.Length > 0)
        {
            string uptag = "";
            if (SMWYAAA022.ValueText.Equals("Y"))
            {
                uptag = "N";
            }
            else
            {
                uptag = "Y";
            }

            string xtag = "'*'";
            for (int i = 0; i < ddo.Length; i++)
            {
                xtag += ",'" + ddo[i].getData("SMWYAAA001") + "'";
            }

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;

            try
            {
                engine = factory.getEngine(engineType, connectString);
                string sql = "update SMWYAAA set SMWYAAA022='" + uptag + "' where SMWYAAA001 in (" + xtag + ")";
                engine.executeSQL(sql);

                engine.close();

                queryData(false);
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
    }
    protected void SMWYAAA022_SelectChanged(string value)
    {
        queryData(false);
    }
}
