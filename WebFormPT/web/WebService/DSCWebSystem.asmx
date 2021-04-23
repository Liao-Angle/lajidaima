<%@ WebService Language="C#" Class="DSCWebSystem" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections;
using System.Data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject;
using com.dsc.flow.data;
using com.dsc.flow.server;
using WebServerProject.system.login;
using com.dsc.kernal.logon;
using System.Text;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DSCWebSystem  : System.Web.Services.WebService {

    private string composeLoginResult(string result, string description, string processid, string userguid, string userid, string username, string locale, string[] groupid)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<ECPLogin>");
        sb.Append("<result>" + result + "</result>");
        sb.Append("<description>" + description + "</description>");
        sb.Append("<processid>" + processid + "</processid>");
        sb.Append("<userguid>" + userguid + "</userguid>");
        sb.Append("<userid>" + userid + "</userid>");
        sb.Append("<username>" + username + "</username>");
        sb.Append("<locale>" + locale + "</locale>");
        sb.Append("<usergroup>");
        for (int i = 0; i < groupid.Length; i++)
        {
            sb.Append(groupid[i]);
            if (i != (groupid.Length - 1))
            {
                sb.Append(";");
            }
        }
        sb.Append("</usergroup>");
        sb.Append("</ECPLogin>");
        return sb.ToString();
    }

    /// <summary>
    /// 提供遠端系統向ECP做Login動作. 此方法會回傳XML, 描述如下
    /// <ECPLogin>
    /// <result>Y/N</result>
    /// <description>若有錯誤描述內容</description>
    /// <processid>登入的processid</processid>
    /// <userguid>登入者的GUID</userguid>
    /// <userid>登入者的UserID</userid>
    /// <username>登入者的姓名</username>
    /// <locale>登入者的預設語系</locale>
    /// <usergroup>使用者所屬的權限群組代號，以;分隔多群組</usergroup>
    /// </ECPLogin>
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="userid"></param>
    /// <param name="password"></param>
    /// <param name="languageType"></param>
    /// <returns></returns>
    [WebMethod]
    public string login(string domain, string userid, string password, string languageType)
    {
        try
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine;

            engine = factory.getEngine(acs.engineType, acs.connectString);

            String Domain = domain;
            String LogonUser = userid;
            String Password = password;

            LogonFactory lfac = new LogonFactory();
            //string loginStr = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "LogonClass");
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string loginStr = sp.getParam("LogonClass");
            if (loginStr.Equals(""))
            {
                loginStr = "WebServerProject.system.login.GPUserLogin";
            }
            string loginAsm = loginStr.Split(new char[] { '.' })[0].Trim();
            AbstractLogon log = lfac.getLogonObject(loginAsm, loginStr);

            string retlog = log.startLogin(engine, Domain, LogonUser, Password);

            if (retlog.Equals(AbstractLogon.NOSUCHUSER))
            {
                engine.close();
                return composeLoginResult("N", "登入錯誤！資料庫中沒有您的身分資料，請洽詢系統管理員。", "", "", "", "", "", new string[0]);

            }
            else if (retlog.Equals(AbstractLogon.USEREMPTY))
            {
                engine.close();
                return composeLoginResult("N", "登入失敗！請確認所有資訊皆已輸入!!", "", "", "", "", "", new string[0]);
            }
            else if (retlog.Equals(AbstractLogon.NOAUTH))
            {
                engine.close();
                return composeLoginResult("N", "登入失敗！無登入使用權限!!", "", "", "", "", "", new string[0]);
            }
            else if (retlog.Equals(AbstractLogon.SUCCESS))
            {

                string sql = "select SMVPAAA015 from SMVPAAA";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                string locale = "";
                if (ds.Tables[0].Rows[0][0].ToString().Equals("N"))
                {
                    locale = "zh_TW";
                }
                else
                {
                    if (languageType.Equals("DEFAULT"))
                    {
                        locale = log.LanguageType;
                    }
                    else
                    {
                        locale = languageType;
                    }

                }

                string xml = composeLoginResult("Y", "", log.ProcessID, log.UserID, log.UserCode, log.UserName, locale, log.UserAuthGroup);

                //紀錄Log
                string gpid = "";
                for (int i = 0; i < log.UserAuthGroup.Length; i++)
                {
                    gpid += log.UserAuthGroup[i] + ";";
                }
                if (gpid.Length > 0)
                {
                    gpid = gpid.Substring(0, gpid.Length - 1);
                }
                sql = "insert into LOGINDATA(GUID, SESSIONID, USERID, USERNAME, LOGINIP, LOGINTIME, LOGOUTTIME, PROCESSID, GROUPID) values(";
                sql += "'" + IDProcessor.getID("") + "',";
                sql += "'" + "" + "',";
                sql += "'" + log.UserCode + "',";
                sql += "'" + log.UserName + "',";
                sql += "'" + "" + "',";
                sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                sql += "'',";
                sql += "'" + log.ProcessID + "',";
                sql += "'" + gpid + "')";
                engine.executeSQL(sql);

                engine.close();
                return xml;
            }
            else
            {
                engine.close();
                return composeLoginResult("N", "登入失敗！" + retlog, "", "", "", "", "", new string[0]);
            }
        }
        catch (Exception e)
        {
            return composeLoginResult("N", e.StackTrace+";"+e.Message, "", "", "", "", "", new string[0]);
        }
    }

    [WebMethod]
    public string getClientNotifyMessage(string userID)
    {
        string retv = "";

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine;

        engine = factory.getEngine(acs.engineType, acs.connectString);

        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        string sss = "select SMVPAAA009 from SMVPAAA";
        DataSet dds = engine.getDataSet(sss, "TEMP");
        bool isD = false;
        if (dds.Tables[0].Rows[0][0].ToString().Equals("Y"))
        {
            isD = true;
        }
        else
        {
            isD = false;
        }
        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, isD);
        WorkItem[] wi = null;
        //這裡要等學長改完才改
        wi = adp.fetchPerformableWorkItems(userID, "1000000", "1", "", "", "", "", "", "0", "");

        //取得系統所有資料夾定義
        string sql = "select SMWIAAA001, SMWIAAA003, SMWIAAA004, SMWIAAA005 from SMWIAAA where SMWIAAA007='W'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            //流程
            string processIDS = "";
            if (ds.Tables[0].Rows[i]["SMWIAAA004"].ToString().Equals("0"))
            {
                sql = "select SMWBAAA003 from SMWBAAA where SMWBAAA003 not in (select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[i]["SMWIAAA001"].ToString()) + "')";
            }
            else
            {
                sql = "select SMWIAAB003 from SMWIAAB where SMWIAAB002='" + Utility.filter(ds.Tables[0].Rows[i]["SMWIAAA001"].ToString()) + "'";
            }
            DataSet temp = engine.getDataSet(sql, "TEMP");
            for (int j = 0; j < temp.Tables[0].Rows.Count; j++)
            {
                processIDS += temp.Tables[0].Rows[j][0].ToString() + ";";
            }
            processIDS = processIDS.Substring(0, processIDS.Length - 1);

            string[] lists = processIDS.Split(new char[] { ';' });
            string ztag = "'*'";
            for (int x = 0; x < lists.Length; x++)
            {
                ztag += ",'" + Utility.filter(lists[x]) + "'";
            }
            //設定不可取得流程
            sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA where SMWBAAA003 not in (" + ztag + ")";
            DataSet ddd = engine.getDataSet(sql, "TEMP");
            string denyProcessIDs = "";
            for (int x = 0; x < ddd.Tables[0].Rows.Count; x++)
            {
                denyProcessIDs += ddd.Tables[0].Rows[x][0].ToString() + ";";
            }
            if (denyProcessIDs.Length > 0)
            {
                denyProcessIDs = denyProcessIDs.Substring(0, denyProcessIDs.Length - 1);
            }

            //取得不可流程角色（工作名稱)
            string denyWorkItemss = "";
            if (ds.Tables[0].Rows[i]["SMWIAAA005"].ToString().Equals("1"))
            {
                sql = "select SMWCAAA002 from SMWCAAA where SMWCAAA002 not in (select SMWIAAC003 from SMWIAAC where SMWIAAC002='" + Utility.filter(ds.Tables[0].Rows[i]["SMWIAAA001"].ToString()) + "')";
            }
            else
            {
                sql = "select SMWIAAC003 from SMWIAAC where SMWIAAC002='" + Utility.filter(ds.Tables[0].Rows[i]["SMWIAAA001"].ToString()) + "'";
            }
            temp = engine.getDataSet(sql, "TEMP");
            for (int x = 0; x < temp.Tables[0].Rows.Count; x++)
            {
                denyWorkItemss += temp.Tables[0].Rows[x][0].ToString() + ";";
            }
            if (denyWorkItemss.Length > 0)
            {
                denyWorkItemss = denyWorkItemss.Substring(0, denyWorkItemss.Length - 1);
            }


            string processList = processIDS;
            //string WorkItems = workItems; //這裡會因為;分隔導致問題


            //開始根據denyProcessID過濾以及denyWorkItems
            string[] denyProcessID = denyProcessIDs.Split(new char[] { ';' });
            string[] denyWorkItems = denyWorkItemss.Split(new char[] { ';' });

            ArrayList tempWI = new ArrayList();
            for (int x = 0; x < wi.Length; x++)
            {
                if (true)
                {
                    //所有流程, 這時候要過濾denyProcess
                    bool hasF = false;
                    for (int z = 0; z < denyProcessID.Length; z++)
                    {
                        if (wi[i].processId.Equals(denyProcessID[z]))
                        {
                            hasF = true;
                            break;
                        }
                    }
                    if (hasF)
                    {
                        continue;
                    }
                }
                //過濾denyWorkItems
                bool hasFound = false;
                for (int z = 0; z < denyWorkItems.Length; z++)
                {
                    if (wi[x].workItemName.Equals(denyWorkItems[z]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (hasFound)
                {
                    continue;
                }

                tempWI.Add(wi[x]);
            }

            if (tempWI.Count > 0)
            {
                retv += ds.Tables[0].Rows[i]["SMWIAAA003"].ToString() + ": 共有 " + tempWI.Count.ToString() + " 筆資料待處理\r\n";
            }
        }

        engine.close();

        return retv;
    }

    [WebMethod]
    public string addAlertMessage(string userid, string messageType, string alertTime, string title, string content, string url)
    {
        if (messageType == null)
        {
            return "訊息類型(messageType)僅能為0~2三種";
        }
        if ((!messageType.Equals("0")) && (!messageType.Equals("1")) && (!messageType.Equals("2")))
        {
            return "訊息類型(messageType)僅能為0~2三種";
        }
        DateTime dt = DateTime.MinValue;
        if ((alertTime != null) && (!alertTime.Equals("")))
        {
            try
            {
                dt = DateTime.Parse(alertTime);
            }
            catch
            {
                return "通知時間(alertTime)設定錯誤";
            }
        }
        if ((title == null) || (title.Trim().Length == 0))
        {
            return "標題(title)必須填寫";
        }
        if ((content == null) || (content.Trim().Length == 0))
        {
            return "內容(content)必須填寫";
        }

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine;

        engine = factory.getEngine(acs.engineType, acs.connectString);

        string sql = "select * from Users where id='" + Utility.filter(userid) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            sql = "select * from SMVCAAA where (1=2)";
            ds = engine.getDataSet(sql, "TEMP");
            DataRow dr = ds.Tables[0].NewRow();
            dr["SMVCAAA001"] = IDProcessor.getID("");
            dr["SMVCAAA002"] = userid;
            dr["SMVCAAA003"] = messageType;
            if (dt != DateTime.MinValue)
            {
                dr["SMVCAAA004"] = DateTimeUtility.convertDateTimeToString(dt);
            }
            else
            {
                dr["SMVCAAA004"] = DateTimeUtility.getSystemTime2(null);
            }
            dr["SMVCAAA005"] = "N";
            dr["SMVCAAA006"] = "N";
            dr["SMVCAAA007"] = title;
            dr["SMVCAAA008"] = content;
            if (url != null)
            {
                dr["SMVCAAA009"] = url;
            }
            else
            {
                dr["SMVCAAA009"] = "";
            }
            dr["D_INSERTUSER"] = "WebService";
            dr["D_INSERTTIME"] = DateTimeUtility.getSystemTime2(null);
            dr["D_MODIFYUSER"] = "";
            dr["D_MODIFYTIME"] = "";

            ds.Tables[0].Rows.Add(dr);

            if (!engine.updateDataSet(ds))
            {
                engine.close();
                return engine.errorString;
            }
        }
        engine.close();
        return "";
    }
}

