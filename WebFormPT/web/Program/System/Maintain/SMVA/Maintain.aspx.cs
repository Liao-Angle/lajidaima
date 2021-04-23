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
using System.Xml;
using WebServerProject.auth;
using System.IO;

public partial class Program_System_Maintain_SMVA_Maintain : BaseWebUI.GeneralWebPage
{
    protected string msg1 = "不可加入重覆項目";
    protected string msg2 = "你確定要刪除嗎?";
    protected string msg3 = "你確定要儲存設定嗎?";
    protected string msg4 = "如果您沒有儲存設定, 當您按下確認後所匯出的資料僅為儲存前資料. 您確定要繼續匯出嗎?";
    protected string msg5 = "儲存設定";
    protected string msg6 = "匯出設定";
    protected string msg7 = "搜尋可使用作業: ";
    protected string msg8 = "搜尋";
    protected string msg9 = "新項目";
    protected string msg10 = "全部顯示";
    protected string msg11 = "將項目拉至適當位置以改變選項內容, 或是拖拉至垃圾桶刪除";
    protected string msg12 = "將項目拖拉至右方樹狀清單以增加選項內容";
    protected string msg13 = "新增模組";
    protected string msg14 = "修改模組";
    protected string msg15 = "往上";
    protected string msg16 = "往下";
    protected string msg17 = "切換預設開啟";
    protected string msg18 = "往上";
    protected string msg19 = "往下";
    protected string msg20 = "新增模組";

    protected override void OnPreRender(EventArgs e)
    {
        msg1 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg1", "不可加入重覆項目");
        msg2 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg2", "你確定要刪除嗎?");
        msg3 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg3", "你確定要儲存設定嗎?");
        msg4 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg4", "如果您沒有儲存設定, 當您按下確認後所匯出的資料僅為儲存前資料. 您確定要繼續匯出嗎?");
        msg5 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg5", "儲存設定");
        msg6 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg6", "匯出設定");
        msg7 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg7", "搜尋可使用作業: ");
        msg8 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg8", "搜尋");
        msg9 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg9", "新項目");
        msg10 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg10", "全部顯示");
        msg11 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg11", "將項目拉至適當位置以改變選項內容, 或是拖拉至垃圾桶刪除");
        msg12 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg12", "將項目拖拉至右方樹狀清單以增加選項內容");
        msg13 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg13", "新增模組");
        msg14 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg14", "修改模組");
        msg15 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg15", "往上");
        msg16 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg16", "往下");
        msg17 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg17", "切換預設開啟");
        msg18 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg18", "往上");
        msg19 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg19", "往下");
        msg20 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg20", "新增模組");
        base.OnPreRender(e);
    }
    protected override void OnInit(EventArgs e)
    {
        setProtectedLevel(15);
        disablePreparePanel = true;
        base.OnInit(e);
    }
    private string filterXMLChar(string ori)
    {
        //return ori;
        string outs = ori;
        outs = outs.Replace("&", "&amp;");
        outs = outs.Replace("'", "&apos;");
        outs = outs.Replace("\"", "&quot;");
        outs = outs.Replace("<", "&lt;");
        outs = outs.Replace(">", "&gt;");
        return outs;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = false;
        string method = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["Method"]);
        if ((method != null) && (!method.Equals("")))
        {
            if (method.Equals("GetToolTree"))
            {
                string isSystem = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["ShowSys"]);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "";

                WebServerProject.auth.AUTHAgent authagent = new WebServerProject.auth.AUTHAgent();
                authagent.engine = engine;

                string data = "<Root>";

                queryXml(engine, "", isSystem, authagent, ref data);

                data += "</Root>";

                engine.close();

                Response.Clear();
                Response.Write(data);
                Response.End();
            }
            else if (method.Equals("GetProgramList"))
            {
                string lsplitter = "$!$!$";
                string splitter = "$*$*$";

                string isSystem = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["ShowSys"]);
                string keyValue = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["keyvalue"]);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                WebServerProject.auth.AUTHAgent authagent = new WebServerProject.auth.AUTHAgent();
                authagent.engine = engine;

                string sql = "";

                string data = "";
                sql = "select SMVAAAB001, SMVAAAB003, SMVAAAB002 from SMVAAAB where SMVAAAB003 like '%" + Utility.filter(keyValue) + "%'";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bool isS = false;
                    if (isSystem.Equals("1"))
                    {
                        isS = true;
                    }
                    else
                    {
                        int auth = authagent.getAuth(ds.Tables[0].Rows[i]["SMVAAAB002"].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);
                        if (auth > 0)
                        {
                            isS = true;
                        }
                    }
                    if (isS)
                    {
                        data += ds.Tables[0].Rows[i][0].ToString() + splitter + ds.Tables[0].Rows[i][1].ToString();
                        data += lsplitter;
                    }
                }
                if (data.Length > 0)
                {
                    data = data.Substring(0, data.Length - lsplitter.Length);
                }
                engine.close();

                Response.Clear();
                Response.Write(data);
                Response.End();
            }
            else if (method.Equals("GetNewProgramList"))
            {
                string lsplitter = "$!$!$";
                string splitter = "$*$*$";

                string isSystem = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["ShowSys"]);
                string keyValue = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["keyvalue"]);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                WebServerProject.auth.AUTHAgent authagent = new WebServerProject.auth.AUTHAgent();
                authagent.engine = engine;

                string sql = "";

                string data = "";
                sql = "select SMVAAAB001, SMVAAAB003, SMVAAAB002 from SMVAAAB where SMVAAAB003 like '%" + Utility.filter(keyValue) + "%' and SMVAAAB001 not in (select SMVAAAA006 from SMVAAAA  ";
                if (isSystem.Equals("1"))
                {
                    sql += " where SMVAAAA007='SYSTEMDEFAULT')";
                }
                else
                {
                    sql += " where SMVAAAA007='" + Utility.filter((string)Session["UserGUID"]) + "')";
                }
                DataSet ds = engine.getDataSet(sql, "TEMP");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bool isS = false;
                    if (isSystem.Equals("1"))
                    {
                        isS = true;
                    }
                    else
                    {
                        int auth = authagent.getAuth(ds.Tables[0].Rows[i]["SMVAAAB002"].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);
                        if (auth > 0)
                        {
                            isS = true;
                        }
                    }
                    if (isS)
                    {
                        data += ds.Tables[0].Rows[i][0].ToString() + splitter + ds.Tables[0].Rows[i][1].ToString();
                        data += lsplitter;
                    }
                }
                if (data.Length > 0)
                {
                    data = data.Substring(0, data.Length - lsplitter.Length);
                }
                engine.close();

                Response.Clear();
                Response.Write(data);
                Response.End();
            }
            else if (method.Equals("saveData"))
            {
                string isSystem = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["ShowSys"]);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                engine.startTransaction(IsolationLevel.ReadCommitted);

                bool result = false;
                try
                {

                    string sql = "";

                    string data = "";

                    string xml = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["Data"]);
                    xml = xml.Replace("#lt;", "<");
                    xml = xml.Replace("#gt;", ">");
                    xml = xml.Replace("#AND;", "&");

                    XMLProcessor p = new XMLProcessor(xml, 1);

                    sql = "delete from SMVAAAA ";
                    if (isSystem.Equals("1"))
                    {
                        sql += " where SMVAAAA007='SYSTEMDEFAULT'";
                    }
                    else
                    {
                        sql += " where SMVAAAA007='" + Utility.filter((string)Session["UserGUID"]) + "'";
                    }
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }

                    insertSMVA(engine, p.doc.ChildNodes[0], "", isSystem);
                    result = true;
                    engine.commit();
                }
                catch (Exception ze)
                {
                    engine.rollback();
                    Response.Write(ze.Message);
                    result = false;
                }

                engine.close();
                if (result)
                {
                    Response.Write(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_maintain_aspx.language.ini", "message", "msg21", "儲存成功"));
                }
                Response.End();
            }
            else if (method.Equals("exportData"))
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                WebServerProject.auth.AUTHAgent authagent = new WebServerProject.auth.AUTHAgent();
                authagent.engine = engine;

                string data = "<Root>";

                queryExportXml(engine, "", ref data);

                data += "</Root>";
                
                engine.close();

                string tempPath = Utility.G_GetTempPath() + "\\";
                string dataFile = tempPath + IDProcessor.getID("") + ".xml";
                StreamWriter sw = new StreamWriter(dataFile, false);
                sw.Write(data);
                sw.Close();

                Response.Write("window.open('GetFile.aspx?FID=" + Server.UrlEncode(dataFile)+"');");
                Response.End();
            }
        }
        else
        {
            //新進入頁面
            //權限判斷
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            AUTHAgent authagent = new AUTHAgent();
            authagent.engine = engine;

            string isSystem = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["ShowSys"]);
            //string isSystem = "1";
            int auth = 0;

            if (isSystem.Equals("1"))
            {
                auth = authagent.getAuth("SMVA", (string)Session["UserID"], (string[])Session["usergroup"]);
            }
            else
            {
                string sql = "select SMVPAAA024 from SMVPAAA";
                
                string auths=(string)engine.executeScalar(sql);
                auth = authagent.getAuthFromAuthItem(auths, (string)Session["UserID"], (string[])Session["usergroup"]);
            }
            engine.close();

            string mstr = "";
            if ((!authagent.parse(auth, AUTHAgent.READ)) && (!authagent.parse(auth, AUTHAgent.ADD)) && (!authagent.parse(auth, AUTHAgent.MODIFY)) && (!authagent.parse(auth, AUTHAgent.DELETE)))
            {
                Response.Redirect("~/NoAuth.aspx");
            }
            else if ((!authagent.parse(auth, AUTHAgent.ADD)) && (!authagent.parse(auth, AUTHAgent.MODIFY)) && (!authagent.parse(auth, AUTHAgent.DELETE)))
            {
                mstr += "<script language=javascript>";
                mstr += "document.getElementById('SaveButton').disabled=true;";
                mstr += "</script>";
                ClientScriptManager cm = Page.ClientScript;
                Type ctype = Page.GetType();
                cm.RegisterStartupScript(ctype, "AuthScript", mstr);
            }

        }
    }
    private void downloadFile(string url)
    {
        string mstr = "<script language=javascript>";
        mstr += "function SMSC_Msg(){";
        mstr += "window.open('" + url + "');";
        mstr += "}";
        mstr += "window.setTimeout('SMSC_Msg()', 100);";
        mstr += "</script>";

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);

    }
    private void insertSMVA(AbstractEngine engine, XmlNode xn, string parentGUID, string isSystem)
    {
        string sql = "";
        for (int i = 0; i < xn.ChildNodes.Count; i++)
        {
            if (xn.ChildNodes[i].Name.Equals("Folder"))
            {
                string thisGUID=IDProcessor.getID("");
                sql = "insert into SMVAAAA(SMVAAAA001, SMVAAAA002, SMVAAAA003, SMVAAAA004, SMVAAAA005, SMVAAAA006, SMVAAAA007, SMVAAAA008, SMVAAAA009, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values(";
                sql += "'" + thisGUID + "',";
                sql += "'" + parentGUID + "',";
                sql += "'" + xn.ChildNodes[i].Attributes["ID"].Value + "',";
                sql += "'" + xn.ChildNodes[i].Attributes["title"].Value + "',";
                sql += "'" + xn.ChildNodes[i].Attributes["ImageURL"].Value + "',";
                sql += "'',";
                if (isSystem.Equals("1"))
                {
                    sql += "'SYSTEMDEFAULT',";
                }
                else
                {
                    sql += "'" + (string)Session["UserGUID"] + "',";
                }
                sql += i.ToString() + ",";
                sql += "'" + xn.ChildNodes[i].Attributes["FOPEN"].Value + "',";
                sql += "'',";
                sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                sql += "'',";
                sql += "'" + DateTimeUtility.getSystemTime2(null) + "')";
                if (!engine.executeSQL(sql))
                {
                    throw new Exception(engine.errorString);
                }

                insertSMVA(engine, xn.ChildNodes[i], thisGUID, isSystem);
            }
            else
            {
                sql = "insert into SMVAAAA(SMVAAAA001, SMVAAAA002, SMVAAAA003, SMVAAAA004, SMVAAAA005, SMVAAAA006, SMVAAAA007, SMVAAAA008, SMVAAAA009, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values(";
                sql += "'" + IDProcessor.getID("") + "',";
                sql += "'" + parentGUID + "',";
                sql += "'*',";
                sql += "'*',";
                sql += "'*',";
                sql += "'" + xn.ChildNodes[i].Attributes["ItemGUID"].Value + "',";
                if (isSystem.Equals("1"))
                {
                    sql += "'SYSTEMDEFAULT',";
                }
                else
                {
                    sql += "'" + (string)Session["UserGUID"] + "',";
                }
                sql += i.ToString() + ",";
                sql += "'N',";
                sql += "'',";
                sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                sql += "'',";
                sql += "'" + DateTimeUtility.getSystemTime2(null) + "')";
                if (!engine.executeSQL(sql))
                {
                    throw new Exception(engine.errorString);
                }
            }
        }
    }
    private void queryXml(AbstractEngine engine, string parentGUID, string isSystem, WebServerProject.auth.AUTHAgent authagent, ref string data)
    {
        string sql = "";
        sql = "select SMVAAAA.*, SMVAAAB.SMVAAAB003, SMVAAAB.SMVAAAB002 from SMVAAAA left outer join SMVAAAB on SMVAAAA006=SMVAAAB001 where SMVAAAA002='" + Utility.filter(parentGUID) + "' ";
        if (isSystem.Equals("1"))
        {
            sql += " and SMVAAAA007='SYSTEMDEFAULT'";
        }
        else
        {
            sql += " and SMVAAAA007='" + Utility.filter((string)Session["UserGUID"]) + "'";
        }
        sql += " order by SMVAAAA008 asc";

        DataSet ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (!ds.Tables[0].Rows[i]["SMVAAAA006"].ToString().Equals(""))
            {
                //程式
                bool isS=false;
                if (isSystem.Equals("1"))
                {
                    isS = true;
                }
                else
                {
                    int auth = authagent.getAuth(ds.Tables[0].Rows[i]["SMVAAAB002"].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);
                    if (auth > 0)
                    {
                        isS = true;
                    }
                }
                if (isS)
                {
                    data += "<Item GUID='" + ds.Tables[0].Rows[i]["SMVAAAA001"].ToString() + "' ItemGUID='" + ds.Tables[0].Rows[i]["SMVAAAA006"].ToString() + "' title='" + filterXMLChar(ds.Tables[0].Rows[i]["SMVAAAB003"].ToString()) + "'></Item>";
                }
            }
            else
            {
                //模組
                string DIS = "0";
                DIS=ds.Tables[0].Rows[i]["SMVAAAA009"].ToString();
                data += "<Folder GUID='" + ds.Tables[0].Rows[i]["SMVAAAA001"].ToString() + "' ID='" + ds.Tables[0].Rows[i]["SMVAAAA003"].ToString() + "' title='" + filterXMLChar(ds.Tables[0].Rows[i]["SMVAAAA004"].ToString()) + "' ImageURL='" + ds.Tables[0].Rows[i]["SMVAAAA005"].ToString() + "' FOPEN='"+DIS+"' >";

                queryXml(engine, ds.Tables[0].Rows[i]["SMVAAAA001"].ToString(), isSystem, authagent, ref data);

                data += "</Folder>";
            }
        }
    }
    private void queryExportXml(AbstractEngine engine, string parentGUID, ref string data)
    {
        string sql = "";
        sql = "select SMVAAAA.*, SMVAAAB.SMVAAAB003, SMVAAAB.SMVAAAB002 from SMVAAAA left outer join SMVAAAB on SMVAAAA006=SMVAAAB001 where SMVAAAA002='" + Utility.filter(parentGUID) + "'";
        sql += " and SMVAAAA007='SYSTEMDEFAULT'";
        sql += " order by SMVAAAA008 asc";

        DataSet ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (!ds.Tables[0].Rows[i]["SMVAAAA006"].ToString().Equals(""))
            {
                //程式
                data += "<Item ProgID='" + ds.Tables[0].Rows[i]["SMVAAAB002"].ToString() + "' title='" + filterXMLChar(ds.Tables[0].Rows[i]["SMVAAAB003"].ToString()) + "'></Item>";
                
            }
            else
            {
                //模組
                data += "<Folder ID='" + ds.Tables[0].Rows[i]["SMVAAAA003"].ToString() + "' title='" + filterXMLChar(ds.Tables[0].Rows[i]["SMVAAAA004"].ToString()) + "' ImageURL='" + ds.Tables[0].Rows[i]["SMVAAAA005"].ToString() + "' >";

                queryExportXml(engine, ds.Tables[0].Rows[i]["SMVAAAA001"].ToString(), ref data);

                data += "</Folder>";
            }
        }
    }
}
