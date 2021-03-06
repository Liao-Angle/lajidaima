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

public partial class smpEasyFlow_system_Signed_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        //========修改的部分==========
        maintainIdentity = "smpEFSigned";
        ApplicationID = "SYSTEM";
        ModuleID = "smpEasyFlow";
        //========修改的部分==========

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {

                ListTable.CurPanelID = CurPanelID;

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "";
                string[,] ids;


                //設定可取得流程
                SysParam sp = new SysParam(engine);
                string sp7 = sp.getParam("EF2KWebDB");

                AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7);
                DataSet sp7ds = sp7engine.getDataSet("select resca001, resca002 from resca where resca086='2'", "TEMP");
                sp7engine.close();

                ids = new string[1+sp7ds.Tables[0].Rows.Count, 2];
                ids[0, 0] = "";
                ids[0, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwl_maintain_aspx.language.ini", "message", " idsA", "不限定");
                for (int i = 0; i < sp7ds.Tables[0].Rows.Count; i++)
                {
                    ids[i + 1 , 0] = sp7ds.Tables[0].Rows[i][0].ToString();
                    ids[i + 1 , 1] = sp7ds.Tables[0].Rows[i][1].ToString();
                }
                ProcessIDList.setListItem(ids);

                setSession("HiddenFields", new ArrayList());
                engine.close();

                queryData();
            }
        }
    }

    private void queryData()
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "";

            SysParam sp = new SysParam(engine);

            //取得EasyFlow的待簽核資料
            DataSet sp7 = getSP7FormSigned(engine);

            if ((sp7.Tables[0].Rows.Count>0))
            {

                //string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME ";

                //抓取來源區域變數
                string qstr = "select GUID, FSYS, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME ";

                DataSet extData = null;
                DataSet det = null;

                qstr += " from SMWL";

                DataObjectSet dos = new DataObjectSet();
                string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
                schema += "<queryStr>" + qstr + "</queryStr>";
                schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                schema += "  <fieldDefinition>";

                schema += "    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>";

                //來源區域
                schema += "    <field dataField=\"FSYS\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"來源\" showName=\"\"/>";


                schema += "    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                schema += "    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"40\" defaultValue=\"\" displayName=\"重要性\" showName=\"0:低;1:中;2:高\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
                schema += "    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"表單名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>";
                schema += "    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                schema += "    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>";
                schema += "    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>";
                schema += "    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>";
                schema += "    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>";
                schema += "    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";

                schema += "  </fieldDefinition>";
                schema += "  <identityField>";
                schema += "    <field dataField=\"GUID\"/>";
                schema += "  </identityField>";
                schema += "  <keyField>";
                schema += "    <field dataField=\"GUID\"/>";
                schema += "  </keyField>";
                schema += "  <allowEmptyField>";

                schema += "    <field dataField=\"GUID\"/>";
                //來源區域
                schema += "    <field dataField=\"FSYS\"/>";

                schema += "    <field dataField=\"ATTACH\"/>";
                schema += "    <field dataField=\"IMPORTANT\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\"/>";
                schema += "    <field dataField=\"PROCESSNAME\"/>";
                schema += "    <field dataField=\"SHEETNO\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\"/>";
                schema += "    <field dataField=\"SUBJECT\"/>";
                schema += "    <field dataField=\"USERNAME\"/>";
                schema += "    <field dataField=\"CREATETIME\"/>";
                schema += "    <field dataField=\"WORKTYPE\"/>";
                schema += "    <field dataField=\"VIEWTIMES\"/>";
                schema += "    <field dataField=\"WORKASSIGNMENT\"/>";
                schema += "    <field dataField=\"ASSIGNMENTTYPE\"/>";
                schema += "    <field dataField=\"REASSIGNMENTTYPE\"/>";
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


                //處理SP7
                for (int i = 0; i < sp7.Tables[0].Rows.Count; i++)
                {
                    DataObject ddo = dos.create();
                    ddo.setData("GUID", sp7.Tables[0].Rows[i]["resdd001"].ToString()+"-"+sp7.Tables[0].Rows[i]["resdd002"].ToString());

                    //來源區域
                    ddo.setData("FSYS", "EasyFlow");
                    //來源區域

                    ddo.setData("CURRENTSTATE", "");
                    ddo.setData("PROCESSNAME", "\t"+sp7.Tables[0].Rows[i]["resca002"].ToString());
                    ddo.setData("SHEETNO", sp7.Tables[0].Rows[i]["resdd002"].ToString());
                    if (sp7.Tables[0].Rows[i]["resda032"].ToString().Equals("0"))
                    {
                        ddo.setData("IMPORTANT", "低");
                    }
                    else if (sp7.Tables[0].Rows[i]["resda032"].ToString().Equals("1"))
                    {
                        ddo.setData("IMPORTANT", "普通");
                    }
                    else
                    {
                        ddo.setData("IMPORTANT", "高");
                    }
                    if (sp7.Tables[0].Rows[i]["resde002"].ToString() != "")
                    {
                        ddo.setData("ATTACH", "{[font color=red]}！{[/font]}");
                    }
                    else
                    {
                        ddo.setData("ATTACH", "");
                    }
                    ddo.setData("WORKITEMNAME", "");
                    ddo.setData("SUBJECT", sp7.Tables[0].Rows[i]["resda031"].ToString());
                    ddo.setData("USERNAME", sp7.Tables[0].Rows[i]["resda_017"].ToString());
                    ddo.setData("CREATETIME", sp7.Tables[0].Rows[i]["resdd009"].ToString());
                    ddo.setData("WORKTYPE", "");
                    ddo.setData("ASSIGNMENTTYPE", "");
                    ddo.setData("REASSIGNMENTTYPE", "");
                    ddo.setData("VIEWTIMES", sp7.Tables[0].Rows[i]["resdd013"].ToString());
                    ddo.setData("WORKASSIGNMENT", "");
                    ddo.setData("D_INSERTUSER", "SYSTEM");
                    ddo.setData("D_INSERTTIME", DateTimeUtility.getSystemTime2(null));
                    ddo.setData("D_MODIFYUSER", "");
                    ddo.setData("D_MODIFYTIME", "");
                    ddo.Tag=sp7.Tables[0].Rows[i];
                    dos.add(ddo);

                }

                ListTable.IsGeneralUse = false;
                ListTable.InputForm = "Detail.aspx";
                ArrayList hFields = (ArrayList)getSession("HiddenFields");
                string[] hf = new string[hFields.Count + 4];
                hf[0] = "GUID";
                hf[1] = "WORKASSIGNMENT";
                hf[2] = "ASSIGNMENTTYPE";
                hf[3] = "REASSIGNMENTTYPE";
                for (int i = 0; i < hFields.Count; i++)
                {
                    hf[i + 4] = (string)hFields[i];
                }
                ListTable.WidthMode = 1;
                ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                ListTable.HiddenField = hf;
                ListTable.dataSource = dos;
                ListTable.updateTable();

                setSession("CURLIST", dos);
            }
            else
            {
                //string qstr = "select GUID, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from SMWL";
                string qstr = "select GUID, FSYS, ATTACH, IMPORTANT, CURRENTSTATE, PROCESSNAME, SHEETNO, WORKITEMNAME, SUBJECT, USERNAME, CREATETIME, WORKTYPE, VIEWTIMES, WORKASSIGNMENT, ASSIGNMENTTYPE, REASSIGNMENTTYPE, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from SMWL";


                DataObjectSet dos = new DataObjectSet();
                string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.flow.SMWL.SMWLAAA\" tableName=\"SMWL\">";
                schema += "<queryStr>" + qstr + "</queryStr>";
                schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
                schema += "  <fieldDefinition>";

                schema += "    <field dataField=\"GUID\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"識別碼\" showName=\"\"/>";

                //來源區域
                schema += "    <field dataField=\"FSYS\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"來源\" showName=\"\"/>";

                schema += "    <field dataField=\"ATTACH\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"附件\" showName=\"\"/>";
                schema += "    <field dataField=\"IMPORTANT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"重要性\" showName=\"\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"狀態\" showName=\"0:未開始;1:進行中;2:已暫停;3:已完成;4:已撤銷;5:已中止\"/>";
                schema += "    <field dataField=\"PROCESSNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"表單名稱\" showName=\"\"/>";
                schema += "    <field dataField=\"SHEETNO\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"單號\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"流程角色\" showName=\"\"/>";
                schema += "    <field dataField=\"SUBJECT\" typeField=\"STRING\" lengthField=\"2000\" defaultValue=\"\" displayName=\"主旨\" showName=\"\"/>";
                schema += "    <field dataField=\"USERNAME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"發起人\" showName=\"\"/>";
                schema += "    <field dataField=\"CREATETIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作性質\" showName=\"\"/>";
                schema += "    <field dataField=\"VIEWTIMES\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"讀取次數\" showName=\"\"/>";
                schema += "    <field dataField=\"WORKASSIGNMENT\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作指派識別\" showName=\"\"/>";
                schema += "    <field dataField=\"ASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"工作類型\" showName=\"\"/>";
                schema += "    <field dataField=\"REASSIGNMENTTYPE\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"轉派類型\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_INSERTTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"建立時間\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYUSER\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新者\" showName=\"\"/>";
                schema += "    <field dataField=\"D_MODIFYTIME\" typeField=\"STRING\" lengthField=\"500\" defaultValue=\"\" displayName=\"更新時間\" showName=\"\"/>";

                schema += "  </fieldDefinition>";
                schema += "  <identityField>";
                schema += "    <field dataField=\"GUID\"/>";
                schema += "  </identityField>";
                schema += "  <keyField>";
                schema += "    <field dataField=\"GUID\"/>";
                schema += "  </keyField>";
                schema += "  <allowEmptyField>";

                schema += "    <field dataField=\"GUID\"/>";

                //來源區域
                schema += "    <field dataField=\"FSYS\"/>";
                //來源區域

                schema += "    <field dataField=\"ATTACH\"/>";
                schema += "    <field dataField=\"IMPORTANT\"/>";
                schema += "    <field dataField=\"CURRENTSTATE\"/>";
                schema += "    <field dataField=\"PROCESSNAME\"/>";
                schema += "    <field dataField=\"SHEETNO\"/>";
                schema += "    <field dataField=\"WORKITEMNAME\"/>";
                schema += "    <field dataField=\"SUBJECT\"/>";
                schema += "    <field dataField=\"USERNAME\"/>";
                schema += "    <field dataField=\"CREATETIME\"/>";
                schema += "    <field dataField=\"WORKTYPE\"/>";
                schema += "    <field dataField=\"VIEWTIMES\"/>";
                schema += "    <field dataField=\"WORKASSIGNMENT\"/>";
                schema += "    <field dataField=\"ASSIGNMENTTYPE\"/>";
                schema += "    <field dataField=\"REASSIGNMENTTYPE\"/>";
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

                ListTable.IsGeneralUse = false;
                ListTable.WidthMode = 1;
                ListTable.setColumnStyle("ATTACH", 25, DSCWebControl.GridColumnStyle.CENTER);
                ListTable.InputForm = "Detail.aspx";
                ListTable.HiddenField = new string[] { "GUID", "WORKASSIGNMENT", "ASSIGNMENTTYPE", "REASSIGNMENTTYPE" };
                ListTable.dataSource = dos;
                ListTable.updateTable();

                setSession("CURLIST", dos);
            }


            engine.close();
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(te.Message);
            writeLog(te);
        }

        //MessageBox("已取得清單");

    }
    protected void FilterButton_Click(object sender, EventArgs e)
    {
        queryData();
    }
    protected DataObject ListTable_CustomDisplayTitle(DataObject objects)
    {
        /*
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
        */
        return objects;
    }
    protected DSCWebControl.FlowStatusData ListTable_GetFlowStatusString(DataObject objects, bool isAddNew)
    {

        //建立FlowStatusData物件
        FlowStatusData fd = new FlowStatusData();

        DataRow dr = (DataRow)objects.Tag;
        ListTable.FormTitle = "\t" + dr["resca002"].ToString() + "(" + dr["resdd002"].ToString() + ")";
        fd.PDID = dr["resdd001"].ToString();
        fd.ObjectGUID = "";
        fd.FlowGUID = dr["resdd002"].ToString();
        fd.WorkItemOID = dr["resdd003"].ToString();
        fd.TargetWorkItemOID = dr["resdd004"].ToString();
        fd.workAssignmentOID = dr["resdd005"].ToString();
        //base.showModalDialog("http://www.google.com.tw", "", "", "", "", "", "", "", "", "", "", "", "");
        //base.showOpenWindow("http://www.google.com.tw", "", "", "", "", "", "", "", "", "", "", "", "", "", "", false);
        return fd;
    }
    protected void ListTable_RefreshButtonClick()
    {
        queryData();
    }

    private DataSet getSP7FormSigned(AbstractEngine engine)
    {
        SysParam sp = new SysParam(engine);
        string sp7str = sp.getParam("EF2KWebDB");

        IOFactory factory = new IOFactory();
        AbstractEngine sp7engine = factory.getEngine(EngineConstants.SQL, sp7str);

        string userid = mappingUserID(engine, sp7engine);

        string strSQL = "";
        strSQL = "select distinct resdd.resdd001, resca.resca002, resdd.resdd002, resdd.resdd003, ";
        strSQL = strSQL + " resdd.resdd004, resdd.resdd005, resdd.resdd006, resdd.resdd008, resdd.resdd009, ";
        strSQL = strSQL + " resdd.resdd014, resdd.resdd013, resdd.resdd015, resda.resda031, resda.resda032, ";
        strSQL = strSQL + " resdb.resdb027, resde.resde002, ";
        strSQL = strSQL + " resak1.resak002 as resda_016, resak2.resak002 as resda_017 ";
        strSQL = strSQL + " FROM resdd LEFT OUTER JOIN ";
        strSQL = strSQL + " resde ON resdd.resdd001 = resde.resde001 AND resdd.resdd002 = resde.resde002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resda ON resdd.resdd001 = resda.resda001 AND resdd.resdd002 = resda.resda002 LEFT OUTER JOIN ";
        strSQL = strSQL + " resak as resak1 ON resda.resda016 = resak1.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resak as resak2 ON resda.resda017 = resak2.resak001 LEFT OUTER JOIN ";
        strSQL = strSQL + " resdb ON resdd.resdd001 = resdb.resdb001 AND resdd.resdd002 = resdb.resdb002 and resdb.resdb003=resdd.resdd003 and resdb.resdb004=resdd.resdd004 LEFT OUTER JOIN ";
        strSQL = strSQL + " resca ON resdd.resdd001 = resca.resca001 ";
        strSQL = strSQL + " WHERE ";
        strSQL = strSQL + " (NOT (resdd.resdd001 LIKE '%CRM_%')) and ";
        strSQL = strSQL + " (resdd.resdd007 = '" + userid + "' OR resdd.resdd020 = '" + userid + "') ";
        strSQL = strSQL + " and (resdd.resdd019 = 'Y') ";
        //strSQL = strSQL + " AND (resdd.resdd015 in ('1','5')) and (resda.resda021 = '1') ";
        strSQL = strSQL + " AND (resdd.resdd015 in ('2','3','4','9','10','11')) ";
        //1=未簽核,   2=同意,   3=不同意,
        //4=會辦完成, 5=已撤簽, 6=執行成功 ,
        //7=執行失敗, 8=已通知, 9=已抽單,
        //10=他人已簽核 11=他人已撤簽

        if (!StartTime.ValueText.Equals(""))
        {
            strSQL += " AND resdd009>='" + StartTime.ValueText + "' ";
        }
        if (!EndTime.ValueText.Equals(""))
        {
            strSQL += " AND resdd009<='" + EndTime.ValueText + "' ";
        }
        if (!ProcessIDList.ValueText.Equals(""))
        {
            strSQL += " AND resdd001='" + ProcessIDList.ValueText + "' ";
        }
        if (!Subject.ValueText.Equals(""))
        {
            strSQL += " AND resda031 like '%" + Subject.ValueText + "%' ";
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
