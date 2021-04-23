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
//using App_Code.SMWYQ;
using WebServerProject.flow.SMWG;
using WebServerProject.flow.SMWR;
using WebServerProject;
using DSCWebControl;

public partial class ECPEF_SMWK_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //WriteLOGLine("********************程式開始");
        maintainIdentity = "SMWK";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWK";

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string BoxID = "";
                if (Request.QueryString["BoxID"] == null)
                {
                    setSession("BoxID", "");
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
                AbstractEngine engine = null;
                AbstractEngine sp8engine = null;
                try
                {
                    engine = factory.getEngine(engineType, connectString);

                    string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);//EF DB connection String
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
                    {"Y","已隱藏"}
                };
                    SMWYAAA022.setListItem(ids);

                    string sql = "";

                    //WriteLOGLine("基本畫面定義完畢");

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
                        sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA";
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
                        SMWRAAA aa = (SMWRAAA)dos.getAvailableDataObject(0);
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

                            if (aa.SMWRAAA024.Equals("1"))
                            {
                                sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where SMWBAAA003 in (" + instr + ")";
                                setSession("ProcessMode", "1");
                            }
                            else
                            {
                                sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where SMWBAAA003 not in (" + instr + ")";
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
                    //WriteLOGLine("ECP可選擇表單選項定義完畢");

                    EndsTime.ValueText = DateTimeUtility.getSystemTime2(null);
        		
		    StartTime.ValueText = DateTime.Parse(DateTimeUtility.getSystemTime2(null).ToString()).AddMonths(-3).ToString();

                    DataSet ddd = engine.getDataSet(sql, "TEMP");
                    if (ddd.Tables[0].Rows.Count > 0)
                    {
                        //ids = new string[ds.Tables[0].Rows.Count + 1, 2];
                        //ids[0, 0] = "";
                        //ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "ids1", "不限定");
                        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //{
                        //    ids[i + 1, 0] = ds.Tables[0].Rows[i][0].ToString();
                        //    ids[i + 1, 1] = ds.Tables[0].Rows[i][1].ToString();
                        //}

                        //Amos
                        sp8engine = factory.getEngine(EngineConstants.SQL, SP8str);
                        DataSet sp8ds = sp8engine.getDataSet("select resca001, resca002 from resca where resca086='2' and resca001<>'PER013' and resca001<>'PER006OVT'", "TEMP");
                        sp8engine.close();

                        ids = new string[ddd.Tables[0].Rows.Count + 1 + sp8ds.Tables[0].Rows.Count, 2];
                        ids[0, 0] = "";
                        ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " idsA", "不限定");
                        for (int i = 0; i < ddd.Tables[0].Rows.Count; i++)
                        {
                            ids[i + 1, 0] = ddd.Tables[0].Rows[i][0].ToString();
                            ids[i + 1, 1] = ddd.Tables[0].Rows[i][1].ToString();
                        }
                        for (int i = 0; i < sp8ds.Tables[0].Rows.Count; i++)
                        {
                            ids[i + 1 + ddd.Tables[0].Rows.Count, 0] = sp8ds.Tables[0].Rows[i][0].ToString();
                            ids[i + 1 + ddd.Tables[0].Rows.Count, 1] = "(EF)" + sp8ds.Tables[0].Rows[i][1].ToString()+"("+sp8ds.Tables[0].Rows[i][0].ToString()+")";
                        }

                        SMWYAAA003.setListItem(ids);

                        engine.close();

                        ListTable.NoDelete = true;
                        ListTable.NoAdd = true;
                        //WriteLOGLine("EF可選擇表單選項定義完畢");
                        queryData(true);
                    }
                }
                catch (Exception ex)
                {
                    try { engine.close();
                    sp8engine.close(); }
                    catch { }
                    throw ex;
                }
            }
        }
    }

    private void queryData(bool isFirst)
    {
        bool boflag = true;
        if (!StartTime.ValueText.Equals("") && !EndsTime.ValueText.Equals(""))
        {
            DateTime stDate = Convert.ToDateTime(StartTime.ValueText);
            DateTime edDate = Convert.ToDateTime(EndsTime.ValueText);
            if (edDate.CompareTo(stDate) < 0)
            {
                MessageBox("工作建立時間迄時需大於起時");
                boflag = false;
            }
        }

        if (boflag)
        {


            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            string userid = (string)Session["UserID"];

            AbstractEngine SP8engine = null;
            try
            {
                //WriteLOGLine("開始撈ECP原稿資料");
                string BoxID = (string)getSession("BoxID");
                //string whereClause = "(SMWYAAA008='" + (string)Session["UserID"] + "' or SMWYAAA012='" + (string)Session["UserID"] + "') ";
                //原稿只有發起人可以看 所以排除代填者
                string whereClause = "(SMWYAAA008='" + (string)Session["UserID"] + "') ";

                if (!SMWYAAA020.ValueText.Equals(""))
                {
                    whereClause += " and SMWYAAA020='" + SMWYAAA020.ValueText + "'";
                }

                if (!StartTime.ValueText.Equals(""))
                {
                    whereClause += " and left(SMWYAAA017,10)>='" + StartTime.ValueText + "'";
                }

                if (!EndsTime.ValueText.Equals(""))
                {
                    whereClause += " and left(SMWYAAA017,10)<='" + EndsTime.ValueText + "'";
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
                engine = factory.getEngine(engineType, connectString);
                string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);
                //SMWYQAgent agent = new SMWYQAgent();
                //agent.engine = engine;
                //int maxDataCount = (int)getSession("maxDataCount");
                //if (maxDataCount == -1)
                //{
                //    agent.query(whereClause);
                //}
                //else
                //{
                //    agent.query(whereClause, maxDataCount);
                //}
                //DataObjectSet dos = agent.defaultData;

                string qstr = "select SMWYAAA.SMWYAAA001,ATTACHCOUNT,SMWYAAA.SMWYAAA020,SMWYAAA.SMWYAAA002,SMWYAAA.SMWYAAA003,SMWYAAA.SMWYAAA004,SMWYAAA.SMWYAAA005,SMWYAAA.SMWYAAA006,SMWYAAA.SMWYAAA007,SMWYAAA.SMWYAAA008,SMWYAAA.SMWYAAA009,SMWYAAA.SMWYAAA010,SMWYAAA.SMWYAAA011,SMWYAAA.SMWYAAA012,SMWYAAA.SMWYAAA013,SMWYAAA.SMWYAAA014,SMWYAAA.SMWYAAA015,SMWYAAA.SMWYAAA016,SMWYAAA.SMWYAAA017,SMWYAAA.SMWYAAA018,SMWYAAA.SMWYAAA019,SMWYAAA.SMWYAAA101,SMWYAAA.SMWYAAA102,SMWYAAA.SMWYAAA103,SMWYAAA.SMWYAAA104,SMWYAAA.SMWYAAA105,SMWYAAA.SMWYAAA106,SMWYAAA.SMWYAAA107,SMWYAAA.SMWYAAA108,SMWYAAA.SMWYAAA109,SMWYAAA.SMWYAAA110,SMWYAAA.SMWYAAA111,SMWYAAA.SMWYAAA112,SMWYAAA.SMWYAAA113,SMWYAAA.SMWYAAA114,SMWYAAA.SMWYAAA115,SMWYAAA.SMWYAAA116,SMWYAAA.SMWYAAA117,SMWYAAA.SMWYAAA118,SMWYAAA.SMWYAAA119,SMWYAAA.SMWYAAA120,SMWYAAA.D_INSERTUSER,SMWYAAA.D_INSERTTIME,SMWYAAA.D_MODIFYUSER,SMWYAAA.D_MODIFYTIME from SMWYAAA left outer join (select SMWYAAA005, count(*) as ATTACHCOUNT from SMWYAAA inner join FILEITEM on SMWYAAA019=JOBID group by SMWYAAA005) Z on SMWYAAA.SMWYAAA005=Z.SMWYAAA005 where " + whereClause;
                //MessageBox(qstr);
                DataObjectSet dos = new DataObjectSet();
                string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWY.SMWYAAA\" tableName=\"SMWY\">";
                schema += "<queryStr><![CDATA[" + qstr + "]]></queryStr>";
                schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                schema += "  <fieldDefinition>";
                schema += "    <field dataField=\"SMWYAAA001\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"唯一識別碼\" showName=\"\"/>";
                schema += "    <field dataField=\"ATTACHCOUNT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA020\" typeField=\"STRING\" lengthField=\"1\" defaultValue=\"\" displayName=\"流程狀態\" showName=\"I:進行中;Y:已結案;N:已終止;W:已撤銷\"/>";
                schema += "    <field dataField=\"SMWYAAA002\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA003\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"流程代號\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA004\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"流程名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA005\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"流程實例序號\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA006\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA007\" typeField=\"STRING\" lengthField=\"1\" defaultValue=\"\" displayName=\"重要性\" showName=\"0:低;1:中;2:高\"/>";
                schema += "    <field dataField=\"SMWYAAA008\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"填表人代號\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA009\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"填表人姓名\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA010\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"填表單位代號\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA011\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"填表單位名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA012\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"關係人代號\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA013\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"關係人姓名\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA014\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"關係人單位代號\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA015\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"關係人單位名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA016\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"流程發起單位\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA017\" typeField=\"STRING\" lengthField=\"20\" defaultValue=\"\" displayName=\"填表日期\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA018\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"作業畫面識別碼\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA019\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"資料單頭識別碼\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA101\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位101\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA102\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位102\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA103\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位103\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA104\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位104\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA105\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位105\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA106\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位106\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA107\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位107\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA108\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位108\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA109\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位109\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA110\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位110\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA111\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位111\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA112\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位112\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA113\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位113\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA114\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位114\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA115\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位115\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA116\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位116\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA117\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位117\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA118\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位118\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA119\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位119\" showName=\"\"/>";
                schema += "    <field dataField=\"SMWYAAA120\" typeField=\"STRING\" lengthField=\"255\" defaultValue=\"\" displayName=\"延伸欄位120\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"50\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";
                schema += "  </fieldDefinition>";
                schema += "  <identityField>";
                schema += "    <field dataField=\"SMWYAAA001\"/>";
                schema += "  </identityField>";
                schema += "  <keyField>";
                schema += "    <field dataField=\"SMWYAAA001\"/>";
                schema += "  </keyField>";
                schema += "  <allowEmptyField>";

                schema += "    <field dataField=\"SMWYAAA010\"/>";
                schema += "    <field dataField=\"SMWYAAA011\"/>";
                schema += "    <field dataField=\"SMWYAAA012\"/>";
                schema += "    <field dataField=\"SMWYAAA013\"/>";
                schema += "    <field dataField=\"SMWYAAA014\"/>";
                schema += "    <field dataField=\"SMWYAAA015\"/>";
                schema += "    <field dataField=\"SMWYAAA016\"/>";
                schema += "    <field dataField=\"SMWYAAA017\"/>";
                schema += "    <field dataField=\"SMWYAAA018\"/>";
                schema += "    <field dataField=\"SMWYAAA019\"/>";
                schema += "    <field dataField=\"SMWYAAA020\"/>";
                schema += "    <field dataField=\"SMWYAAA101\"/>";
                schema += "    <field dataField=\"SMWYAAA102\"/>";
                schema += "    <field dataField=\"SMWYAAA103\"/>";
                schema += "    <field dataField=\"SMWYAAA104\"/>";
                schema += "    <field dataField=\"SMWYAAA105\"/>";
                schema += "    <field dataField=\"SMWYAAA106\"/>";
                schema += "    <field dataField=\"SMWYAAA107\"/>";
                schema += "    <field dataField=\"SMWYAAA108\"/>";
                schema += "    <field dataField=\"SMWYAAA110\"/>";
                schema += "    <field dataField=\"SMWYAAA111\"/>";
                schema += "    <field dataField=\"SMWYAAA112\"/>";
                schema += "    <field dataField=\"SMWYAAA113\"/>";
                schema += "    <field dataField=\"SMWYAAA114\"/>";
                schema += "    <field dataField=\"SMWYAAA115\"/>";
                schema += "    <field dataField=\"SMWYAAA116\"/>";
                schema += "    <field dataField=\"SMWYAAA117\"/>";
                schema += "    <field dataField=\"SMWYAAA118\"/>";
                schema += "    <field dataField=\"SMWYAAA119\"/>";
                schema += "    <field dataField=\"SMWYAAA120\"/>";
                schema += "    <field dataField=\"D_INSERTUSER\"/>";
                schema += "    <field dataField=\"D_INSERTTIME\"/>";
                schema += "    <field dataField=\"D_MODIFYUSER\"/>";
                schema += "    <field dataField=\"D_MODIFYTIME\"/>";

                schema += "  </allowEmptyField>";
                schema += "  <nonUpdateField>";
                schema += "  </nonUpdateField>";
                schema += "</DataObject>";
                dos.dataObjectSchema = schema;
                dos.isNameLess = true;

                //WriteLOGLine("撈出ECP原稿資料");

                ArrayList arys = new ArrayList();

                DataSet dsSMWY = engine.getDataSet(qstr, "TEMP");
                for (int i = 0; i < dsSMWY.Tables[0].Rows.Count; i++)
                {
                    arys.Add(dsSMWY.Tables[0].Rows[i]);
                }
                //for (int i = 0; i < dsSMWY.Tables[0].Columns.Count; i++)
                //{
                //    MessageBox(dsSMWY.Tables[0].Columns[i].ColumnName);
                //}

                DataTable dtSMWY = dsSMWY.Tables[0];
                //if (dtSMWY.Rows.Count > 0)
                #region 此段不執行了
                if (false)
                {
                    for (int i = 0; i < dtSMWY.Rows.Count; i++)
                    {
                        DataObject doo = dos.create();
                        doo.setData("SMWYAAA001", IDProcessor.getID(""));
                        doo.setData("ATTACHCOUNT", dsSMWY.Tables[0].Rows[i]["ATTACHCOUNT"].ToString());
                        doo.setData("SMWYAAA002", dsSMWY.Tables[0].Rows[i]["SMWYAAA002"].ToString());
                        doo.setData("SMWYAAA003", dsSMWY.Tables[0].Rows[i]["SMWYAAA003"].ToString());
                        doo.setData("SMWYAAA004", dsSMWY.Tables[0].Rows[i]["SMWYAAA004"].ToString());
                        doo.setData("SMWYAAA005", dsSMWY.Tables[0].Rows[i]["SMWYAAA005"].ToString());
                        doo.setData("SMWYAAA006", dsSMWY.Tables[0].Rows[i]["SMWYAAA006"].ToString());
                        doo.setData("SMWYAAA007", dsSMWY.Tables[0].Rows[i]["SMWYAAA007"].ToString());
                        doo.setData("SMWYAAA008", dsSMWY.Tables[0].Rows[i]["SMWYAAA008"].ToString());
                        doo.setData("SMWYAAA009", dsSMWY.Tables[0].Rows[i]["SMWYAAA009"].ToString());
                        doo.setData("SMWYAAA010", dsSMWY.Tables[0].Rows[i]["SMWYAAA010"].ToString());
                        doo.setData("SMWYAAA011", dsSMWY.Tables[0].Rows[i]["SMWYAAA011"].ToString());
                        doo.setData("SMWYAAA012", dsSMWY.Tables[0].Rows[i]["SMWYAAA012"].ToString());
                        doo.setData("SMWYAAA013", dsSMWY.Tables[0].Rows[i]["SMWYAAA013"].ToString());
                        doo.setData("SMWYAAA014", dsSMWY.Tables[0].Rows[i]["SMWYAAA014"].ToString());
                        doo.setData("SMWYAAA015", dsSMWY.Tables[0].Rows[i]["SMWYAAA015"].ToString());
                        doo.setData("SMWYAAA016", dsSMWY.Tables[0].Rows[i]["SMWYAAA016"].ToString());
                        doo.setData("SMWYAAA017", dsSMWY.Tables[0].Rows[i]["SMWYAAA017"].ToString());
                        doo.setData("SMWYAAA018", dsSMWY.Tables[0].Rows[i]["SMWYAAA018"].ToString());
                        doo.setData("SMWYAAA019", dsSMWY.Tables[0].Rows[i]["SMWYAAA019"].ToString());
                        doo.setData("SMWYAAA020", dsSMWY.Tables[0].Rows[i]["SMWYAAA020"].ToString());
                        doo.setData("SMWYAAA101", dsSMWY.Tables[0].Rows[i]["SMWYAAA101"].ToString());
                        doo.setData("SMWYAAA102", dsSMWY.Tables[0].Rows[i]["SMWYAAA102"].ToString());
                        doo.setData("SMWYAAA103", dsSMWY.Tables[0].Rows[i]["SMWYAAA103"].ToString());
                        doo.setData("SMWYAAA104", dsSMWY.Tables[0].Rows[i]["SMWYAAA104"].ToString());
                        doo.setData("SMWYAAA105", dsSMWY.Tables[0].Rows[i]["SMWYAAA105"].ToString());
                        doo.setData("SMWYAAA106", dsSMWY.Tables[0].Rows[i]["SMWYAAA106"].ToString());
                        doo.setData("SMWYAAA107", dsSMWY.Tables[0].Rows[i]["SMWYAAA107"].ToString());
                        doo.setData("SMWYAAA108", dsSMWY.Tables[0].Rows[i]["SMWYAAA108"].ToString());
                        doo.setData("SMWYAAA109", dsSMWY.Tables[0].Rows[i]["SMWYAAA109"].ToString());
                        doo.setData("SMWYAAA110", dsSMWY.Tables[0].Rows[i]["SMWYAAA110"].ToString());
                        doo.setData("SMWYAAA111", dsSMWY.Tables[0].Rows[i]["SMWYAAA111"].ToString());
                        doo.setData("SMWYAAA112", dsSMWY.Tables[0].Rows[i]["SMWYAAA112"].ToString());
                        doo.setData("SMWYAAA113", dsSMWY.Tables[0].Rows[i]["SMWYAAA113"].ToString());
                        doo.setData("SMWYAAA114", dsSMWY.Tables[0].Rows[i]["SMWYAAA114"].ToString());
                        doo.setData("SMWYAAA115", dsSMWY.Tables[0].Rows[i]["SMWYAAA115"].ToString());
                        doo.setData("SMWYAAA116", dsSMWY.Tables[0].Rows[i]["SMWYAAA116"].ToString());
                        doo.setData("SMWYAAA117", dsSMWY.Tables[0].Rows[i]["SMWYAAA117"].ToString());
                        doo.setData("SMWYAAA118", dsSMWY.Tables[0].Rows[i]["SMWYAAA118"].ToString());
                        doo.setData("SMWYAAA119", dsSMWY.Tables[0].Rows[i]["SMWYAAA119"].ToString());
                        doo.setData("SMWYAAA120", dsSMWY.Tables[0].Rows[i]["SMWYAAA120"].ToString());
                        dos.addDraft(doo);
                    }
                }
                #endregion

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
                        if (aa.getData("SMWRAAA" + tag).Equals("N"))
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
                    string fname = "";
                    bool hasF = false;
                    for (int j = 0; j < fdos.getAvailableDataObjectCount(); j++)
                    {
                        SMWGAAA aa = (SMWGAAA)fdos.getAvailableDataObject(j);
                        if (aa.SMWGAAA002.Equals(tag))
                        {
                            fname = aa.SMWGAAA004;
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
                //WriteLOGLine("開始處理欄位順序");
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
                //WriteLOGLine("開始取EF的原稿DB資料");
                #region 取EF 的原稿
                SP8engine = factory.getEngine(EngineConstants.SQL, SP8str);

                string strSQL = "";
                strSQL += "SELECT DISTINCT de.resde002,da.resda015, da.resda016, ak.resak002 as resak002,da.resda017,ak1.resak002 as resak002t, da.resda020, da.resda021, ";
                strSQL += "da.resda001, da.resda002, ca.resca002, da.resda031, da.resda015, ";
                strSQL += "isnull(da.resda032,'1')as resda032,da.resda033 ";
                strSQL += "FROM resda da ";
                strSQL += "LEFT JOIN resca ca ON da.resda001 = ca.resca001 ";
                strSQL += "LEFT JOIN resak ak ON da.resda016 = ak.resak001 ";
                strSQL += "LEFT JOIN resak ak1 ON da.resda017 = ak1.resak001 ";
                strSQL += "LEFT JOIN resde de ON da.resda001 = de.resde001 AND da.resda002 = de.resde002 ";
                //strSQL += "WHERE da.resda016 = N'" + userid + "' AND da.resda033 = N'Y' ";
                strSQL += "WHERE da.resda016 = N'" + userid + "' ";
                if (SMWYAAA020.ValueText.Equals("I"))
                {
                    strSQL += " AND da.resda020=N'2' ";
                }
                if (SMWYAAA020.ValueText.Equals("Y"))
                {
                    strSQL += " AND (da.resda020=N'3' AND da.resda021=N'2') ";
                }
                if (SMWYAAA020.ValueText.Equals("N"))
                {
                    strSQL += " AND (da.resda020=N'3' AND da.resda021=N'3') ";
                }
                if (SMWYAAA020.ValueText.Equals("W"))
                {
                    strSQL += " AND (da.resda020=N'4' AND da.resda021=N'4') ";
                }
                if (!SMWYAAA003.ValueText.Equals(""))
                {
                    strSQL += " AND da.resda001='" + SMWYAAA003.ValueText + "'";
                }
                if (!SMWYAAA006.ValueText.Equals(""))
                {
                    strSQL += " and da.resda031 like '%" + SMWYAAA006.ValueText + "%'";
                }
                if (!StartTime.ValueText.Equals(""))
                {
                    strSQL += " AND left(resda015,10)>='" + StartTime.ValueText + "' ";
                }
                if (!EndsTime.ValueText.Equals(""))
                {
                    strSQL += " AND left(resda015,10)<='" + EndsTime.ValueText + "' ";
                }
                if (SMWYAAA022.ValueText.Equals("Y"))//刪除
                {
                    //strSQL += " and 1=0";
                    strSQL += " AND da.resda033 = N'N'";//不顯示
                }
                else//一般
                {
                    strSQL += " AND da.resda033 = N'Y'";//顯示
                }
                DataSet ds = SP8engine.getDataSet(strSQL, "TEMP");
                //MessageBox(strSQL );
                SP8engine.close();
                //WriteLOGLine("開始取EF的原稿DB資料結束");
                #endregion
                //WriteLOGLine("開始將EF的資料 塞進dos");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    arys.Add(ds.Tables[0].Rows[i]);
                }

                for (int i = 0; i < arys.Count; i++)
                {
                    for (int j = i + 1; j < arys.Count; j++)
                    {

                        DataRow ob1 = (DataRow)arys[i];
                        DataRow ob2 = (DataRow)arys[j];
                        string ct1 = "";
                        string ct2 = "";


                        if (ob1.Table.Columns[0].ColumnName.Equals("SMWYAAA001"))
                        {
                            ct1 = ob1["SMWYAAA017"].ToString();
                        }
                        else
                        {
                            ct1 = ob1["resda015"].ToString();
                        }
                        if (ob2.Table.Columns[0].ColumnName.Equals("SMWYAAA001"))
                        {
                            ct2 = ob2["SMWYAAA017"].ToString();
                        }
                        else
                        {
                            ct2 = ob2["resda015"].ToString();
                        }
                        if (ct1.CompareTo(ct2) < 0)
                        {
                            //arys.Remove(ob1);
                            //arys.Insert(i, ob1);
                            object ptr = arys[i];
                            arys[i] = arys[j];
                            arys[j] = ptr;

                        }

                    }
                }

                int EFCount = ds.Tables[0].Rows.Count;

                setSession("init_SMWK", arys);
                TotalRows.Text = "共" + arys.Count.ToString() + "筆資料";
                //int totalpages = (int)com.dsc.kernal.utility.Utility.Round((decimal)(arys.Count / ListTable.getPageSize()), 0);
                //if (totalpages * ListTable.getPageSize() < arys.Count)
                //{
                //    totalpages++;
                //}
                //setSession("TOTALPAGES", totalpages);
                //TotalPages.Text = "/共" + totalpages.ToString() + "頁";

                #region 此段不執行了
                if (false)
                //if (EFCount > 0)
                {
                    for (int i = 0; i < EFCount; i++)
                    {
                        DataObject doo = dos.create();
                        doo.setData("SMWYAAA001", IDProcessor.getID(""));
                        if (ds.Tables[0].Rows[i]["resde002"].ToString() == "")
                            doo.setData("ATTACHCOUNT", "");
                        else
                            doo.setData("ATTACHCOUNT", "{[font color=red]}！{[/font]}");
                        doo.setData("SMWYAAA002", ds.Tables[0].Rows[i]["resda002"].ToString());
                        doo.setData("SMWYAAA003", ds.Tables[0].Rows[i]["resda001"].ToString());
                        doo.setData("SMWYAAA004", ds.Tables[0].Rows[i]["resca002"].ToString());
                        doo.setData("SMWYAAA005", ds.Tables[0].Rows[i]["resda002"].ToString());

                        doo.setData("SMWYAAA006", ds.Tables[0].Rows[i]["resda031"].ToString());
                        doo.setData("SMWYAAA007", ds.Tables[0].Rows[i]["resda032"].ToString());
                        doo.setData("SMWYAAA008", ds.Tables[0].Rows[i]["resda016"].ToString());
                        doo.setData("SMWYAAA009", ds.Tables[0].Rows[i]["resak002"].ToString());

                        doo.setData("SMWYAAA012", ds.Tables[0].Rows[i]["resda017"].ToString());
                        doo.setData("SMWYAAA013", ds.Tables[0].Rows[i]["resak002t"].ToString());

                        doo.setData("SMWYAAA017", ds.Tables[0].Rows[i]["resda015"].ToString());
                        doo.setData("SMWYAAA018", "QQ18");
                        doo.setData("SMWYAAA019", "QQ19");

                        string sSMWYAAA020 = "";
                        if (ds.Tables[0].Rows[i]["resda020"].ToString() == "2")
                            sSMWYAAA020 = "I";
                        if ((ds.Tables[0].Rows[i]["resda020"].ToString() == "3") && (ds.Tables[0].Rows[i]["resda021"].ToString() == "2"))
                            sSMWYAAA020 = "Y";
                        if ((ds.Tables[0].Rows[i]["resda020"].ToString() == "3") && (ds.Tables[0].Rows[i]["resda021"].ToString() == "3"))
                            sSMWYAAA020 = "N";
                        if ((ds.Tables[0].Rows[i]["resda020"].ToString() == "4") && (ds.Tables[0].Rows[i]["resda021"].ToString() == "4"))
                            sSMWYAAA020 = "W";

                        doo.setData("SMWYAAA020", sSMWYAAA020);

                        doo.setData("SMWYAAA021", "");
                        //doo.setData("SMWYAAA022", "N");
                        string sSMWYAAA022 = "";
                        if (ds.Tables[0].Rows[i]["resda033"].ToString().Equals("Y"))
                            sSMWYAAA022 = "N";
                        else
                            sSMWYAAA022 = "Y";
                        doo.setData("SMWYAAA022", sSMWYAAA022);

                        dos.addDraft(doo);
                    }
                }
                #endregion
                //WriteLOGLine("開始將EF的資料 塞進dos結束");

                //WriteLOGLine("dos排序完成");

                ListTable.setColumnStyle("ATTACHCOUNT", 30, DSCWebControl.GridColumnStyle.CENTER);
                ListTable.WidthMode = 1;
                ListTable.IsGeneralUse = false;
                ListTable.IsPanelWindow = true;
                ListTable.InputForm = "Detail.aspx";
                ListTable.HiddenField = hField;
                ListTable.dataSource = dos;
                //WriteLOGLine("ListTable開始updateTable");
                //ListTable.updateTable();
	

	
                //WriteLOGLine("ListTable開始updateTable完成");
                engine.close();
                SP8engine.close();

                setSession("CURRENTPAGE", 1);

		//ListTable.isShowAll=false;
		//ListTable.PageSize = 10;

                //changePage(1);
		
		ListTable_ShowPagingClick();
            }
            catch (Exception ex)
            {
                try
                {
                    engine.close();
                    SP8engine.close();
                }
                catch { }
                throw ex;
            }
        }
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
		ListTable.isShowAll=false;
		ListTable.PageSize=10;
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
        try
        {
            DataObject wi = (DataObject)objects;
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            //建立FlowStatusData物件
            FlowStatusData fd = new FlowStatusData();

            //取得資料物件代碼, 由原稿取得
            fd.ACTID = "";
            fd.ACTName = "";
            //fd.FlowGUID = wi["SMWYAAA001"].ToString();
            fd.FlowGUID = wi.getData("SMWYAAA005").ToString();
            fd.HistoryGUID = "";
            fd.ObjectGUID = wi.getData("SMWYAAA019").ToString();
            fd.PDID = wi.getData("SMWYAAA003").ToString();
            fd.PDVer = "";
            //這裡要判斷是否為ProcessNew或者ProcessModify或者為ProcessDelete或者為FormReadOnly
            //目前都給他為ProcessModify
            fd.UIStatus = FlowStatusData.FormReadOnly;
            fd.WorkItemOID = "";

            Session["tempSMWK"] = wi.getData("SMWYAAA018").ToString();

            //這裡要根據SMWDAAA判定是否有ProcessNew

	    if(!Session["tempSMWK"].ToString().Equals("QQ18")){
            ListTable.FormTitle = wi.getData("SMWYAAA004").ToString() + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwk_maintain_aspx.language.ini", "message", "List", "(單號:") + wi.getData("SMWYAAA002").ToString() + ")";
		}else{
            ListTable.FormTitle="EasyFlow 電子表單";
		}
            //engine.close();
            return fd;
        }
        catch (Exception ze)
        {
            FlowStatusData fd = new FlowStatusData();
            return fd;
        }
    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData(false);

    }
    protected void ToggleButton_Click(object sender, EventArgs e)
    {
        //Modify by 20120229: ECP & EF 分別要更新 Delete Flag
        //ECP SMWYAAA022=Y --> Update Y
        //EF  SMWYAAA022=Y --> Update N 
        DataObject[] ddo = ListTable.getSelectedItem();
        if (ddo.Length > 0)
        {
            string uptag = "";
            string EFuptag = "";
            if (SMWYAAA022.ValueText.Equals("Y"))
            {
                uptag = "N";
            }
            else
            {
                uptag = "Y";
            }
            EFuptag = SMWYAAA022.ValueText;

            string xtag = "'*'";//ECP
            string EFxtag = "'*'";//EF
            for (int i = 0; i < ddo.Length; i++)
            {
                if (ddo[i].getData("SMWYAAA018").Equals("QQ18"))//EF Data
                {
                    EFxtag += ",'" + ddo[i].getData("SMWYAAA003") + ddo[i].getData("SMWYAAA002") + "'";//代號+單號
                }
                else
                {
                    xtag += ",'" + ddo[i].getData("SMWYAAA001") + "'";
                }
            }

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            AbstractEngine SP8engine = null;

            try
            {
                engine = factory.getEngine(engineType, connectString);
                string SP8str = GetINDUSConnSTR("EF2KWebDB", engine);
                string sql = "update SMWYAAA set SMWYAAA022='" + uptag + "' where SMWYAAA001 in (" + xtag + ")";
                engine.executeSQL(sql);
                engine.close();

                SP8engine = factory.getEngine(EngineConstants.SQL, SP8str);
                string EFsql = "Update resda set resda033 = '" + EFuptag + "' where resda001+resda002 in (" + EFxtag + ")  ";
                SP8engine.executeSQL(EFsql);
                SP8engine.close();

                queryData(false);
            }
            catch (Exception ue)
            {
                try
                {
                    engine.close();
                    SP8engine.close();
                }
                catch { };
                throw ue;
            }
        }
    }
    protected void SMWYAAA022_SelectChanged(string value)
    {
        queryData(false);
    }

    protected void WriteLOGLine(string value)
    {
        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
        fname = Server.MapPath("~/LogFolder/" + fname + "_SMWY_Log.log");
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
        sw.WriteLine((string)Session["UserID"] + " " + System.DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss.fff") + ":" + value);
        sw.Close();
    }


    protected bool ListTable_NextPageClick()
    {
	if(((int)getSession("TOTALPAGES"))==0) return false;
        if (ListTable.isShowAll) return false;
        int cup = (int)getSession("CURRENTPAGE");
        int pageNum = cup + 1;
        int total = (int)getSession("TOTALPAGES");
        if (pageNum > total) pageNum = total;
        changePage(pageNum);
        setSession("CURRENTPAGE", pageNum);
        CurrentPage.ValueText = pageNum.ToString();
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(1);
        ListTable.setCurrentPage(1);

        return true;
    }
    protected bool ListTable_PrevPageClick()
    {
	if(((int)getSession("TOTALPAGES"))==0) return false;
        if (ListTable.isShowAll) return false;
        int cup = (int)getSession("CURRENTPAGE");
        int pageNum = cup - 1;
        if (pageNum == 0) pageNum = 1;
        changePage(pageNum);
        setSession("CURRENTPAGE", pageNum);
        CurrentPage.ValueText = pageNum.ToString();
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(1);
        ListTable.setCurrentPage(1);

        return true;
    }
    protected bool ListTable_FirstPageClick()
    {
	if(((int)getSession("TOTALPAGES"))==0) return false;
        if (ListTable.isShowAll) return false;
        changePage(1);
        setSession("CURRENTPAGE", 1);
        CurrentPage.ValueText = "1";
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(1);
        ListTable.setCurrentPage(1);

        return true;
    }
    protected bool ListTable_LastPageClick()
    {
	if(((int)getSession("TOTALPAGES"))==0) return false;
        if (ListTable.isShowAll) return false;
        int tp = (int)getSession("TOTALPAGES");
        changePage(tp);
        setSession("CURRENTPAGE", tp);
        CurrentPage.ValueText = tp.ToString();
        DataObjectSet dos = ListTable.dataSource;
        dos.setCurrentPageNum(tp);
        ListTable.setCurrentPage(tp);

        return true;
    }
    protected void CurrentPage_TextChanged(object sender, EventArgs e)
    {
	if(((int)getSession("TOTALPAGES"))==0) return;
        int cp = 1;
        try
        {
            cp = int.Parse(CurrentPage.ValueText);
        }
        catch
        {
            cp = 1;
        }
        int tp = (int)getSession("TOTALPAGES");
        if (cp > tp) cp = tp;
        changePage(cp);
    }
    protected bool ListTable_ChangePageSizeClick(int pagesize)
    {
	if(((int)getSession("TOTALPAGES"))==0) return false;
        ArrayList ary = (ArrayList)getSession("init_SMWK");
        int tp = (int)com.dsc.kernal.utility.Utility.Round((decimal)(ary.Count / pagesize), 0);
        if (tp * pagesize < ary.Count)
        {
            tp++;
        }
        setSession("TOTALPAGES", tp);
        TotalPages.Text = "/共" + tp.ToString() + "頁";
        setSession("CURRENTPAGE", 1);
        changePage(1);

        return true;
    }
    private void changePage(int pageNum)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        string userid = (string)Session["UserID"];

        setSession("CURRENTPAGE", pageNum);
        CurrentPage.ValueText = ((int)getSession("CURRENTPAGE")).ToString();

        try
        {
            string BoxID = (string)getSession("BoxID");


            //WriteLOGLine("撈出ECP原稿資料");
            ArrayList arys = (ArrayList)getSession("init_SMWK");
            int start = ListTable.getPageSize() * (pageNum - 1);
            int end = start + ListTable.getPageSize();
            if (end > arys.Count) end = arys.Count;

            //DataSet dsSMWY = engine.getDataSet(qstr, "TEMP");
            DataObjectSet dos = ListTable.dataSource;
            dos.clear();
            if(true)
            {
                for (int i = start; i < end; i++)
                {
                    bool isECP = false;
                    DataRow drs = (DataRow)arys[i];
                    if (drs.Table.Columns[0].ColumnName.Equals("SMWYAAA001"))
                    {
                        isECP = true;
                    }
                    else
                    {
                        isECP = false;
                    }
                    if (isECP)
                    {
                        DataObject doo = dos.create();
                        //doo.setData("SMWYAAA001", IDProcessor.getID(""));
                        doo.setData("SMWYAAA001", drs["SMWYAAA001"].ToString());
                        doo.setData("ATTACHCOUNT", drs["ATTACHCOUNT"].ToString());
                        doo.setData("SMWYAAA002", drs["SMWYAAA002"].ToString());
                        doo.setData("SMWYAAA003", drs["SMWYAAA003"].ToString());
                        doo.setData("SMWYAAA004", drs["SMWYAAA004"].ToString());
                        doo.setData("SMWYAAA005", drs["SMWYAAA005"].ToString());
                        doo.setData("SMWYAAA006", drs["SMWYAAA006"].ToString());
                        doo.setData("SMWYAAA007", drs["SMWYAAA007"].ToString());
                        doo.setData("SMWYAAA008", drs["SMWYAAA008"].ToString());
                        doo.setData("SMWYAAA009", drs["SMWYAAA009"].ToString());
                        doo.setData("SMWYAAA010", drs["SMWYAAA010"].ToString());
                        doo.setData("SMWYAAA011", drs["SMWYAAA011"].ToString());
                        doo.setData("SMWYAAA012", drs["SMWYAAA012"].ToString());
                        doo.setData("SMWYAAA013", drs["SMWYAAA013"].ToString());
                        doo.setData("SMWYAAA014", drs["SMWYAAA014"].ToString());
                        doo.setData("SMWYAAA015", drs["SMWYAAA015"].ToString());
                        doo.setData("SMWYAAA016", drs["SMWYAAA016"].ToString());
                        doo.setData("SMWYAAA017", drs["SMWYAAA017"].ToString());
                        doo.setData("SMWYAAA018", drs["SMWYAAA018"].ToString());
                        doo.setData("SMWYAAA019", drs["SMWYAAA019"].ToString());
                        doo.setData("SMWYAAA020", drs["SMWYAAA020"].ToString());
                        doo.setData("SMWYAAA101", drs["SMWYAAA101"].ToString());
                        doo.setData("SMWYAAA102", drs["SMWYAAA102"].ToString());
                        doo.setData("SMWYAAA103", drs["SMWYAAA103"].ToString());
                        doo.setData("SMWYAAA104", drs["SMWYAAA104"].ToString());
                        doo.setData("SMWYAAA105", drs["SMWYAAA105"].ToString());
                        doo.setData("SMWYAAA106", drs["SMWYAAA106"].ToString());
                        doo.setData("SMWYAAA107", drs["SMWYAAA107"].ToString());
                        doo.setData("SMWYAAA108", drs["SMWYAAA108"].ToString());
                        doo.setData("SMWYAAA109", drs["SMWYAAA109"].ToString());
                        doo.setData("SMWYAAA110", drs["SMWYAAA110"].ToString());
                        doo.setData("SMWYAAA111", drs["SMWYAAA111"].ToString());
                        doo.setData("SMWYAAA112", drs["SMWYAAA112"].ToString());
                        doo.setData("SMWYAAA113", drs["SMWYAAA113"].ToString());
                        doo.setData("SMWYAAA114", drs["SMWYAAA114"].ToString());
                        doo.setData("SMWYAAA115", drs["SMWYAAA115"].ToString());
                        doo.setData("SMWYAAA116", drs["SMWYAAA116"].ToString());
                        doo.setData("SMWYAAA117", drs["SMWYAAA117"].ToString());
                        doo.setData("SMWYAAA118", drs["SMWYAAA118"].ToString());
                        doo.setData("SMWYAAA119", drs["SMWYAAA119"].ToString());
                        doo.setData("SMWYAAA120", drs["SMWYAAA120"].ToString());
                        dos.addDraft(doo);
                    }
                    else
                    {
                        DataObject doo = dos.create();
                        doo.setData("SMWYAAA001", IDProcessor.getID(""));
                        if (drs["resde002"].ToString() == "")
                            doo.setData("ATTACHCOUNT", "");
                        else
                            doo.setData("ATTACHCOUNT", "{[font color=red]}！{[/font]}");
                        doo.setData("SMWYAAA002", drs["resda002"].ToString());
                        doo.setData("SMWYAAA003", drs["resda001"].ToString());
                        doo.setData("SMWYAAA004", drs["resca002"].ToString());
                        doo.setData("SMWYAAA005", drs["resda002"].ToString());

                        doo.setData("SMWYAAA006", drs["resda031"].ToString());
                        doo.setData("SMWYAAA007", drs["resda032"].ToString());
                        doo.setData("SMWYAAA008", drs["resda016"].ToString());
                        doo.setData("SMWYAAA009", drs["resak002"].ToString());
                        //doo.setData("SMWYAAA010", "QQ10");

                        //doo.setData("SMWYAAA011", "QQ11");
                        doo.setData("SMWYAAA012", drs["resda017"].ToString());
                        doo.setData("SMWYAAA013", drs["resak002t"].ToString());
                        //doo.setData("SMWYAAA014", "QQ14");
                        //doo.setData("SMWYAAA015", "QQ15");

                        //doo.setData("SMWYAAA016", "QQ16");
                        doo.setData("SMWYAAA017", drs["resda015"].ToString());
                        doo.setData("SMWYAAA018", "QQ18");
                        doo.setData("SMWYAAA019", "QQ19");

                        string sSMWYAAA020 = "";
                        if (drs["resda020"].ToString() == "2")
                            sSMWYAAA020 = "I";
                        if ((drs["resda020"].ToString() == "3") && (drs["resda021"].ToString() == "2"))
                            sSMWYAAA020 = "Y";
                        if ((drs["resda020"].ToString() == "3") && (drs["resda021"].ToString() == "3"))
                            sSMWYAAA020 = "N";
                        if ((drs["resda020"].ToString() == "4") && (drs["resda021"].ToString() == "4"))
                            sSMWYAAA020 = "W";

                        doo.setData("SMWYAAA020", sSMWYAAA020);

                        doo.setData("SMWYAAA021", "");
                        //doo.setData("SMWYAAA022", "N");
                        string sSMWYAAA022 = "";
                        if (drs["resda033"].ToString().Equals("Y"))
                            sSMWYAAA022 = "N";
                        else
                            sSMWYAAA022 = "Y";
                        doo.setData("SMWYAAA022", sSMWYAAA022);


                        dos.addDraft(doo);
                    }
                }
            }

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

            ListTable.dataSource = dos;
            //WriteLOGLine("ListTable開始updateTable");
            ListTable.updateTable();
            //WriteLOGLine("ListTable開始updateTable完成");
            //engine.close();
        }
        catch (Exception ex)
        {
            try
            {
                engine.close();
            }
            catch { }
            throw ex;
        }
    }
    protected void ListTable_ShowAllPageClick()
    {
        ListTable.isShowAll = false;
        ArrayList ary = (ArrayList)getSession("init_SMWK");
        ListTable.setCurrentPage(1);
        ListTable.dataSource.setCurrentPageNum(1);
        ListTable.PageSize = ary.Count;
        ListTable.dataSource.setPageSize(ary.Count);
        setSession("TOTALPAGES", 1);
        TotalPages.Text = "/共1頁";
        setSession("CURRENTPAGE", 1);
        changePage(1);
        ListTable.isShowAll = true;
    }
    protected void ListTable_ShowPagingClick()
    {
        ArrayList ary = (ArrayList)getSession("init_SMWK");
        //ListTable.setCurrentPage(1);
        ListTable.dataSource.setCurrentPageNum(1);
        int pagesize = 10;
        ListTable.PageSize = pagesize;
        ListTable.isShowAll=false;
        ListTable.dataSource.setPageSize(pagesize);
        int tp = (int)com.dsc.kernal.utility.Utility.Round((decimal)(ary.Count / pagesize), 0);
        if (tp * pagesize < ary.Count)
        {
            tp++;
        }
	if(ary.Count==0){
		CurrentPage.ValueText="0";
	}else{
		CurrentPage.ValueText="1";
	}
        setSession("TOTALPAGES", tp);
        TotalPages.Text = "/共" + tp.ToString() + "頁";
        setSession("CURRENTPAGE", 1);
        changePage(1);
    }
    public string GetINDUSConnSTR(string strConn, AbstractEngine engine)
    {

        SysParam sp = new SysParam(engine);
        string connectString1 = sp.getParam(strConn);

        return connectString1;
    }

}
