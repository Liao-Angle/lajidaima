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
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class MainFrame_MainFrame : System.Web.UI.Page
{
    public string LoginInfo = "";
    public string WindowTitle = "";


    public bool SMVPAAA002 = true; //是否允許設定個人化選單
    public bool SMVPAAA003 = true; //事否允許自定面板
    public bool SMVPAAA004 = true; //是否允許使用者自訂新視窗開啟功能
    public bool SMVPAAA005 = true; //
    public bool SMVPAAA006 = true; //是否預設顯示標題
    public bool SMVPAAA007 = true; //是否允許切換標題
    public bool SMVPAAA008 = true; //是否顯示功能列
    public bool SMVPAAA009 = true; //
    public bool SMVPAAA016 = true; //系統/個人選單視為一般視窗
    public bool SMVPAAA017 = true; //是否下載元件
    public bool SMVPAAA041 = true; //是否啟用主版面切換語言功能
    public bool isUseSetting = true; //判定是否顯示使用者儲存的狀態
    public bool isSaveDefault = true; //是否儲存預設版面
    public string SMVPAAA016Text = "";
    public string SMVPAAA005Text = ""; //滑鼠右鍵script 20130604

    protected override void OnInit(EventArgs e)
    {

        if (Session["UserID"] == null)
        {
            Response.Redirect("../default.aspx");
        }

        LoginInfo = ": " + (string)Session["UserID"] + " " + (string)Session["UserName"];

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "";
        DataSet ds = null;

        int isProtectedPage = -1;
        string sql1 = "select SMVPAAA005 from SMVPAAA";
        DataSet ds1 = engine.getDataSet(sql1, "TEMP1");
        isProtectedPage = int.Parse(ds1.Tables[0].Rows[0][0].ToString());
        SMVPAAA005Text = "<script language='javascript'>ScreenProtectPage(" + isProtectedPage.ToString() + ");</script>";

        if (!IsPostBack)
        {
            ChangeLanguageInit(engine);
        }
        if (Request.Form["mtd"] != null)
        {
            sql = "select SMVAAAB003, SMVAAAB004 from SMVAAAB where SMVAAAB002='" + Request.Form["mtd"] + "'";
            ds = engine.getDataSet(sql, "TEMP");

            string oStr = "";
            if (ds.Tables[0].Rows.Count > 0)
            {
                oStr = ds.Tables[0].Rows[0][0].ToString() + "$$$" + ds.Tables[0].Rows[0][1].ToString();
            }
            engine.close();

            Response.Write(oStr);
            Response.End();
            return;
        }
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        WindowTitle = (string)Session["UserName"] + "(" + (string)Session["UserID"] + ")-" + sp.getParam("SystemName");

        WebServerProject.auth.AUTHAgent authag = new WebServerProject.auth.AUTHAgent();
        authag.engine = engine;

        sql = "select * from SMVPAAA";
        ds = engine.getDataSet(sql, "TEMP");

        int auadmin = authag.getAuthFromAuthItem(ds.Tables[0].Rows[0]["SMVPAAA035"].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);

        //if (!((string)Session["UserID"]).ToUpper().Equals("ADMINISTRATOR"))
        if (auadmin == 0)
        {

            if (ds.Tables[0].Rows[0]["SMVPAAA002"].ToString().Equals("N"))
            {
                SMVPAAA002 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA003"].ToString().Equals("N"))
            {
                SMVPAAA003 = false;
            }
            else
            {
                //這裡要根據SMVPAAA025的權限決定
                int au = authag.getAuthFromAuthItem(ds.Tables[0].Rows[0]["SMVPAAA025"].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);
                if (au == 0)
                {
                    SMVPAAA003 = false;
                }
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA004"].ToString().Equals("N"))
            {
                SMVPAAA004 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA005"].ToString().Equals("N"))
            {
                SMVPAAA005 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA006"].ToString().Equals("N"))
            {
                SMVPAAA006 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA007"].ToString().Equals("N"))
            {
                SMVPAAA007 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA008"].ToString().Equals("N"))
            {
                SMVPAAA008 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA009"].ToString().Equals("N"))
            {
                SMVPAAA009 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA016"].ToString().Equals("N"))
            {
                SMVPAAA016 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA017"].ToString().Equals("N"))
            {
                SMVPAAA017 = false;
            }
            if (ds.Tables[0].Rows[0]["SMVPAAA041"].ToString().Equals("N"))
            {
                SMVPAAA041 = false;
            }
            isSaveDefault = false;
        }
        if ((Request.QueryString["CloseTitle"] != null) && (Request.QueryString["CloseTitle"].Equals("1")))
        {
            SMVPAAA006 = false;
        }
        if ((Request.QueryString["CloseToolBar"] != null) && (Request.QueryString["CloseToolBar"].Equals("1")))
        {
            SMVPAAA008 = false;
        }
        if ((Request.QueryString["CloseSetting"] != null) && (Request.QueryString["CloseSetting"].Equals("1")))
        {
            isUseSetting = false;
        }
        if (!SMVPAAA016)
        {
            string zx = "";
            zx += "<script language=javascript>";
            zx += "changeMenuFix(true);";
            zx += "</script>";

            SMVPAAA016Text = zx;
        }
        //這裡要處理各種外部連結的URL
        if (Request.QueryString["runMethod"]!=null)
        {
            string sc = "<script language=javascript>";
            
            sc += "function openPortalLink(){";

            string runMethod = Request.QueryString["runMethod"];
            if (runMethod.Equals("showReadOnlyForm"))
            {
                string processSerialNumber = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["processSerialNumber"]);
                //取得作業畫面URL
                string url = "";
                sql = "select * from SMWYAAA where SMWYAAA005='" + Utility.filter(processSerialNumber) + "'";
                ds = engine.getDataSet(sql, "TEMP");

                string formTitle = ds.Tables[0].Rows[0]["SMWYAAA004"].ToString() + "(單號:" + ds.Tables[0].Rows[0]["SMWYAAA002"].ToString() + ")";

                sql = "select SMWAAAA005 from SMWAAAA where SMWAAAA001='" + Utility.filter(ds.Tables[0].Rows[0]["SMWYAAA018"].ToString()) + "'";
                DataSet tmp = engine.getDataSet(sql, "TEMP");
                url = tmp.Tables[0].Rows[0][0].ToString();
                url = "" + url + "?";
                url += "ACTID=";
                url += "&ACTName=";
                url += "&FlowGUID=" + processSerialNumber;
                url += "&HistoryGUID=";
                url += "&ObjectGUID=" + ds.Tables[0].Rows[0]["SMWYAAA019"].ToString();
                url += "&PDID=" + ds.Tables[0].Rows[0]["SMWYAAA003"].ToString();
                url += "&PDVer=";
                url += "&UIStatus=3";
                url += "&WorkItemOID=";

                sc += "openWindowGeneral('"+formTitle+"', '" + url + "', 0, 0, '', true, true);";
            }
            else if (runMethod.Equals("showSignForm"))
            {
                string processSerialNumber = Request.QueryString["processSerialNumber"];
                string workItemOID = Request.QueryString["workItemOID"];
                string workItemName = Request.QueryString["workItemName"];

                //檢查是否允許簽核, 若不允許（可能已經簽核過了, 轉為原稿顯示)
                string flowType = (String)Session["FlowAdapter"];
                string con1 = (String)Session["NaNaWebService"];
                string con2 = (String)Session["DotJWebService"];
                string account = (String)Session["FlowAccount"];
                string password = (String)Session["FlowPassword"];

                com.dsc.flow.server.FlowFactory ff = new com.dsc.flow.server.FlowFactory();
                com.dsc.flow.server.AbstractFlowAdapter adp = ff.getAdapter(flowType);
                adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
                adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

                string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
                fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

                adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

                com.dsc.flow.data.WorkItem[] wi = null;

                wi = adp.fetchPerformableWorkItems((string)Session["UserID"], "1000000", "1", "", "", processSerialNumber, "","", "","");

                //國昌20100623-mantis:0017645
                sql = "select SMVPAAA036 from SMVPAAA";
                string isSubstitute = (string)engine.executeScalar(sql);

                sql = "select SMVPAAA026 from SMVPAAA";
                string isDB = (string)engine.executeScalar(sql);

                string fakePageUniqueID=IDProcessor.getID("");

                //代理人處理機制
                if (isSubstitute.Equals("Y"))
                {
                    //取出並宣告系統參數定義之存取組件
                    Session[fakePageUniqueID+"_SUBSLIST"]=new System.Collections.Specialized.StringCollection();
                    string subsComponent = sp.getParam("SubstituteComponent");
                    com.dsc.flow.client.SubstituteFactory fac = new com.dsc.flow.client.SubstituteFactory();
                    com.dsc.flow.client.AbstractSubstitute asub = fac.getSubstituteUtility(subsComponent.Split(new char[] { '.' })[0], subsComponent);
                    //取出被代理人清單
                    string[,] substituteList = asub.getSubstituteList((string)Session["UserID"], engine);

                    //依據List取出對應的WorkItem並合併
                    if (substituteList.Length > 0)
                    {
                        string substituteUser = string.Empty;
                        com.dsc.flow.data.WorkItem[] wiNew = null;
                        ArrayList arrayWI = new ArrayList();
                        for (int i = 0; i < substituteList.Length / 3; i++)
                        {
                            //一個ID只抓一次WorkItem
                            if (!substituteUser.Equals(substituteList[i, 0]))
                            {
                                //更新UserID
                                substituteUser = substituteList[i, 0];
                                //依據被代理人ID抓取WorkItem
                                if (isDB.Equals("N"))
                                {
                                    wiNew = adp.fetchPerformableWorkItems(substituteUser, "1000000", "1", "", "", processSerialNumber, "", "", "", "");
                                }
                                else
                                {
                                    wiNew = adp.fetchPerformableWorkItems(substituteUser, "1000000", "1", "", "", processSerialNumber, "", "", "", "", engine);
                                }
                            }
                            //篩選ProcessID與發起部門
                            string substituteProcID = substituteList[i, 1];
                            string substituteOrg = substituteList[i, 2];
                            arrayWI = new ArrayList();
                            for (int j = 0; j < wiNew.Length; j++)
                            {
                                //filter the ProcID
                                if (wiNew[j].processId.Equals(substituteProcID))
                                {
                                    if ((string.IsNullOrEmpty(substituteOrg)) || (substituteOrg == ""))
                                    {
                                        if (!arrayWI.Contains(wiNew[j]))
                                            arrayWI.Add(wiNew[j]);
                                    }
                                    else
                                    {
                                        //filter the Org-2010/05/10國昌修正, SMWYAAA0005->SMWYAAA005, 並將wiNew[j].processInstanceOID->processSerialNumber
                                        sql = "select SMWYAAA016 from SMWYAAA where SMWYAAA005 = '{0}'";
                                        string org = (string)engine.executeScalar(string.Format(sql, wiNew[j].processSerialNumber));
                                        if (substituteOrg.Equals(org))
                                        {
                                            if (!arrayWI.Contains(wiNew[j]))
                                                arrayWI.Add(wiNew[j]);
                                        }
                                    }
                                }
                            }
                            //合併
                            wi = mergeWorkItem(wi, arrayWI, fakePageUniqueID);
                        }
                    }
                }
                //國昌20100623-mantis:0017645-結束

                string url = "";
                string formTitle = "";
                if (wi.Length > 0)
                {
                    //取得作業畫面URL

                    sql = "select * from SMWYAAA where SMWYAAA005='" + Utility.filter(processSerialNumber) + "'";
                    ds = engine.getDataSet(sql, "TEMP");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        formTitle = ds.Tables[0].Rows[0]["SMWYAAA004"].ToString() + "(單號:" + ds.Tables[0].Rows[0]["SMWYAAA002"].ToString() + ")";
                    }
                    else
                    {
                        formTitle = wi[0].processName + "(流程實例序號:" + wi[0].processSerialNumber + ")";
                    }

                    sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(ds.Tables[0].Rows[0]["SMWYAAA003"].ToString()) + "' and SMWDAAA004='" + Utility.filter(workItemName) + "'";
                    DataSet dsx = engine.getDataSet(sql, "TEMP");
                    if (dsx.Tables[0].Rows.Count > 0)
                    {
                        url = dsx.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        sql = "select SMWAAAA005 from SMWAAAA inner join SMWDAAA on SMWDAAA005=SMWAAAA001 inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(ds.Tables[0].Rows[0]["SMWYAAA003"].ToString()) + "' and SMWDAAA006='Default'";
                        dsx = engine.getDataSet(sql, "TEMP");
                        url = dsx.Tables[0].Rows[0][0].ToString();
                    }

                    url = "" + url + "?";
                    url += "ACTID=";

                    sql = "select SMWDAAA006, SMWDAAA005 from SMWDAAA inner join SMWBAAA on SMWDAAA003=SMWBAAA004 where SMWBAAA003='" + Utility.filter(ds.Tables[0].Rows[0]["SMWYAAA003"].ToString()) + "' and SMWDAAA004='" + Utility.filter(workItemName) + "'";
                    DataSet ds2 = engine.getDataSet(sql, "TEMP");
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        if (ds2.Tables[0].Rows[0][0].ToString().Equals("New"))
                        {
                            url += "&UIStatus=4"; //ProcessNew
                            url += "&ObjectGUID=" + ds.Tables[0].Rows[0]["SMWYAAA019"].ToString();
                        }
                        else
                        {
                            url += "&UIStatus=5"; //ProcessModify
                            //有可能是ProcessNew之後的ProcessModify, 必須考慮替換fd.ObjectGUID
                            if (!ds.Tables[0].Rows[0]["SMWYAAA018"].ToString().Equals(ds2.Tables[0].Rows[0][1].ToString()))
                            {
                                //若原稿的SMWAAAA001跟目前關卡的SMWAAAA001不同, 表示要替換
                                sql = "select CURGUID from FORMRELATION where ORIGUID='" + Utility.filter(ds.Tables[0].Rows[0]["SMWYAAA019"].ToString()) + "'";
                                ds2 = engine.getDataSet(sql, "TEMP");
                                url += "&ObjectGUID=" + ds2.Tables[0].Rows[0][0].ToString();
                            }
                            else
                            {
                                url += "&ObjectGUID=" + ds.Tables[0].Rows[0]["SMWYAAA019"].ToString();
                            }
                        }
                    }
                    else
                    {
                        url += "&UIStatus=5"; //ProcessModify
                        url += "&ObjectGUID=" + ds.Tables[0].Rows[0]["SMWYAAA019"].ToString();
                    }


                    url += "&ACTName=" + Server.UrlEncode(workItemName);
                    url += "&FlowGUID=" + processSerialNumber;
                    url += "&HistoryGUID=";
                    url += "&PDID=" + ds.Tables[0].Rows[0]["SMWYAAA003"].ToString();
                    url += "&PDVer=";
                    url += "&WorkItemOID=" + workItemOID;
                    url += "&ParentPageUID=" + fakePageUniqueID;
                    //國昌:2009/10/07 0014130取得reassignmentType
                    string reassignmentType="";
                    if (wi[0].assignmentType.Length == 1)
                    {
                        reassignmentType = "";
                    }
                    else
                    {
                        string ast = wi[0].assignmentType.Substring(1, 1);
                        reassignmentType = ast;
                    }
                    url += "&reassignmentType="+reassignmentType;
                    url += "&mailflowPerformers=" + Convert.ToString(Session["UserID"]) + ";";
                    
                }
                else
                {
		    bool isNotice = false;
                    com.dsc.flow.data.WorkItem[] wiNot = adp.fetchNoticeWorkItems(Convert.ToString(Session["UserID"]), "1000000", "1", "", "", processSerialNumber, "", "", "", "", engine);
                    for (int j = 0; j < wiNot.Length; j++)
                    {
                        if (wiNot[j].processSerialNumber == processSerialNumber)
                        {
                            isNotice = true;
                        }
                    }

                    //取得作業畫面URL, 相同於runMethod=showReadOnlyForm
                    sql = "select * from SMWYAAA where SMWYAAA005='" + Utility.filter(processSerialNumber) + "'";
                    ds = engine.getDataSet(sql, "TEMP");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        formTitle = ds.Tables[0].Rows[0]["SMWYAAA004"].ToString() + "(單號:" + ds.Tables[0].Rows[0]["SMWYAAA002"].ToString() + ")";
                    }
                    else
                    {
                        formTitle = wi[0].processName + "(流程實例序號:" + wi[0].processSerialNumber + ")";
                    }

                    sql = "select SMWAAAA005 from SMWAAAA where SMWAAAA001='" + Utility.filter(ds.Tables[0].Rows[0]["SMWYAAA018"].ToString()) + "'";
                    DataSet tmp = engine.getDataSet(sql, "TEMP");
                    url = tmp.Tables[0].Rows[0][0].ToString();
                    url = "" + url + "?";
                    url += "ACTID=";
                    url += "&ACTName=";
                    url += "&FlowGUID=" + processSerialNumber;
                    url += "&HistoryGUID=";
                    url += "&ObjectGUID=" + ds.Tables[0].Rows[0]["SMWYAAA019"].ToString();
                    url += "&PDID=" + ds.Tables[0].Rows[0]["SMWYAAA003"].ToString();
                    url += "&PDVer=";
                    url += "&UIStatus=3";
                    url += "&WorkItemOID=";
                    if (isNotice)
                    {
                        url += "&mailflowPerformers=" + Convert.ToString(Session["UserID"]) + ";";
                    }
                    else
                    {
                        url += "&mailflowPerformers=;";
                    }
                    //國昌20100617 mantis0017613
                    sc += "alert('此單據已被簽核過');";
                }
                sc += "openWindowGeneral('" + formTitle + "', '" + url + "', 0, 0, '', true, true);";
            }
            else if (runMethod.Equals("executeProgram"))
            {
                string programID = Request.QueryString["programID"];
                sql = "select SMVAAAB003, SMVAAAB004 from SMVAAAB where SMVAAAB002='" + Utility.filter(programID) + "'";
                DataSet dsx = engine.getDataSet(sql, "TEMP");
                if (dsx.Tables[0].Rows.Count > 0)
                {
                    sc += "openWindowGeneral('" + dsx.Tables[0].Rows[0][0].ToString() + "','" + dsx.Tables[0].Rows[0][1].ToString() + "',0,0,'',true,true);";
                }
            }

            sc += "}";
            sc += "</script>";

            //ClientScriptManager cm = Page.ClientScript;
            //Type ctype = Page.GetType();
            //cm.RegisterStartupScript(ctype, this.GetType().Name + "_PortalLink", sc);
            Response.Write(sc);
        }
        engine.close(); 
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
    }

    private com.dsc.flow.data.WorkItem[] mergeWorkItem(com.dsc.flow.data.WorkItem[] wiOriginal, ArrayList wiNew, string fakePageUniqueID)
    {
        System.Collections.Specialized.StringCollection subsWorkOIDList = (System.Collections.Specialized.StringCollection)Session[fakePageUniqueID + "_SUBSLIST"];
        Array.Resize(ref wiOriginal, wiOriginal.Length + wiNew.Count);
        for (int i = 0; i < wiNew.Count; i++)
        {
            wiOriginal[wiOriginal.Length - wiNew.Count + i] = (com.dsc.flow.data.WorkItem)wiNew[i];
            subsWorkOIDList.Add(((com.dsc.flow.data.WorkItem)wiNew[i]).workItemOID);
        }
        Session[fakePageUniqueID + "_SUBSLIST"] = subsWorkOIDList;
        return wiOriginal;
    }
    protected void ChangeLanguageInit(AbstractEngine engine)
    {
        //比照變換使用者模式 ;  跳過onBeforeUnLoad事件
        ddlCL.Attributes.Add("onChange", "top.window.isChangeUser=true;");
        ddlCL.SelectedValue = Session["locale"].ToString();
        string sql = "Select LANGUAGEID, LANGUAGENAME from SYSLANGUAGE ";
        DataSet dsLan = engine.getDataSet(sql, "LANGUAGE");
        if (dsLan.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsLan.Tables[0].Rows.Count; i++)
            {
                ListItem tmp = new ListItem();
                tmp.Text = dsLan.Tables[0].Rows[i][1].ToString();
                tmp.Value = dsLan.Tables[0].Rows[i][0].ToString();
                ddlCL.Items.Add(tmp);                
            }
            ddlCL.SelectedValue = Session["locale"].ToString();
        }        
        dsLan.Dispose();        
    }
    protected void ddlCL_SelectedIndexChanged(object sender, EventArgs e)
    {
        string js = "";
        js += "<script>";
        js += "top.window.isChangeUser=true;";
        js += "document.URL=location.href;";
        js += "</script>";
        Session["SessionCount"] = null;
        Session["locale"] = ddlCL.SelectedValue;
        Response.Write(js);
    }
}
