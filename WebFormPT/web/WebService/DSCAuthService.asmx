<%@ WebService Language="C#" Class="DSCAuthService" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using com.dsc.kernal.utility;
using com.dsc.kernal.factory;
using System.IO;
using System.Data;

/// <summary>
/// DSCAuthService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DSCAuthService : System.Web.Services.WebService
{

    public DSCAuthService()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int getAuthFromProgramID(string programID, string processid)
    {
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine=null;

        try
        {
            engine = factory.getEngine(acs.engineType, acs.connectString);

            string sql = "select GROUPID from LOGINDATA where PROCESSID='" + Utility.filter(processid) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");

            if (ds.Tables[0].Rows.Count == 0)
            {
                engine.close();
                return 0;
            }
            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                engine.close();
                return 0;
            }
            string[] groupid = ds.Tables[0].Rows[0][0].ToString().Split(new char[] { ';' });
            WebServerProject.auth.AUTHAgent agent = new WebServerProject.auth.AUTHAgent();
            agent.engine = engine;

            int auth = agent.getAuth(programID, "", groupid);

            engine.close();
            return auth;
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            return 0;
        }
    }
    
    [WebMethod]
    public int getAuthFromAuthItem(string authitemID, string processid)
    {
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;

        try
        {
            engine = factory.getEngine(acs.engineType, acs.connectString);

            string sql = "select GROUPID from LOGINDATA where PROCESSID='" + Utility.filter(processid) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");

            if (ds.Tables[0].Rows.Count == 0)
            {
                engine.close();
                return 0;
            }
            if (ds.Tables[0].Rows[0][0].ToString().Equals(""))
            {
                engine.close();
                return 0;
            }
            string[] groupid = ds.Tables[0].Rows[0][0].ToString().Split(new char[] { ';' });
            WebServerProject.auth.AUTHAgent agent = new WebServerProject.auth.AUTHAgent();
            agent.engine = engine;

            int auth = agent.getAuthFromAuthItem(authitemID, "", groupid);
            
            engine.close();
            return auth;
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            return 0;
        }
    }
    
    /// <summary>
    /// 取得系統所有權限群組設定Schema
    /// </summary>
    /// <returns>Xml schema</returns>
    [WebMethod]
    public string getAllAuthSchema()
    {

        string xml = "<Root></Root>";
        XMLProcessor xp = new XMLProcessor(xml, 1);

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        string dbid = "0";
        string dbtitle = "預設資料庫";
        string dbstr = acs.connectString;
        string enginetype = acs.engineType;

        XmlNode rootnode = xp.selectSingleNode(@"/Root");
        XmlDocument doc = xp.getXmlDocument();
        queryAllAuthSchema(dbid, dbtitle, dbstr, enginetype, "", doc, ref rootnode);

        return xp.getXmlString();
    }
    
    /// <summary>
    /// 取得指定群組代號的權限設定
    /// </summary>
    /// <param name="groupID">使定的群組代號</param>
    /// <returns>Xml Schenma</returns>
    [WebMethod]
    public string getAuthSchema(string groupID)
    {
        string xml = "<Root></Root>";
        XMLProcessor xp = new XMLProcessor(xml, 1);

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        string dbid = "0";
        string dbtitle = "預設資料庫";
        string dbstr = acs.connectString;
        string enginetype = acs.engineType;

        XmlNode rootnode = xp.selectSingleNode(@"/Root");
        XmlDocument doc = xp.getXmlDocument();
        queryAllAuthSchema(dbid, dbtitle, dbstr, enginetype, groupID, doc, ref rootnode);

        return xp.getXmlString();
    }
    /// <summary>
    /// 取得系統所有權限項目
    /// </summary>
    /// <returns>Xml Schema</returns>
    [WebMethod]
    public string getAllAuthItem()
    {

        string xml = "<Root></Root>";
        XMLProcessor xp = new XMLProcessor(xml, 1);

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        string dbid = "0";
        string dbtitle = "預設資料庫";
        string dbstr = acs.connectString;
        string enginetype = acs.engineType;

        XmlNode rootnode = xp.selectSingleNode(@"/Root");
        XmlDocument doc = xp.getXmlDocument();
        queryAllAuthItem(dbid, dbtitle, dbstr, enginetype, doc, ref rootnode);

        return xp.getXmlString();
    }

    /// <summary>
    /// 設定所有資料庫的權限項目
    /// </summary>
    /// <param name="xml">要設定的內容</param>
    /// <returns>xml Schema</returns>
    [WebMethod]
    public string modifyAuthItem(string xml)
    {
        //取得所有DB index
        try
        {
            XMLProcessor xpo = new XMLProcessor(xml, 1);
            string xmls = "<Root></Root>";
            XMLProcessor xp = new XMLProcessor(xmls, 1);

            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(acs.engineType, acs.connectString);
            engine.startTransaction(IsolationLevel.ReadCommitted);

            bool result = true;
            result = processModifyAuthItem(engine, "0", xpo, ref xp);

            if (result)
            {
                engine.commit();
            }
            else
            {
                engine.rollback();
            }
            engine.close();

            return xp.getXmlString();
        }
        catch (Exception ue)
        {
            string errxml = "<Root><SystemError>";
            errxml += "所傳入xml格式有誤, 或者系統發生錯誤. 錯誤訊息為: " + ue.Message;
            errxml += "</SystemError></Root>";

            return errxml;
        }
    }


    /// <summary>
    /// 設定所有資料庫的群組權限
    /// </summary>
    /// <param name="xml">設定的內容</param>
    /// <returns>Xml Schema</returns>
    [WebMethod]
    public string modifyAuthSchema(string xml)
    {
        //取得所有DB index
        try
        {

            XMLProcessor xpo = new XMLProcessor(xml, 1);
            string xmls = "<Root></Root>";
            XMLProcessor xp = new XMLProcessor(xmls, 1);

            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(acs.engineType, acs.connectString);
            engine.startTransaction(IsolationLevel.ReadCommitted);

            bool result = true;
            result = processModifyAuthSchema(engine, "0", xpo, ref xp);

            if (result)
            {
                engine.commit();
            }
            else
            {
                engine.rollback();
            }
            engine.close();


            return xp.getXmlString();
        }
        catch (Exception ue)
        {
            string errxml = "<Root><SystemError>";
            errxml += "所傳入xml格式有誤, 或者系統發生錯誤. 錯誤訊息為: " + ue.Message;
            errxml += "</SystemError></Root>";

            return errxml;
        }
    }

    #region Private方法
    private bool processModifyAuthSchema(AbstractEngine engine, string dbIndex, XMLProcessor xpo, ref XMLProcessor xp)
    {
        bool retv = true;

        //先根據dbIndex建立DBNode
        XmlNode dbnode = xp.getXmlDocument().CreateElement("DB");
        XmlAttribute dbid = xp.getXmlDocument().CreateAttribute("ID");
        dbid.Value = dbIndex;
        dbnode.Attributes.Append(dbid);
        xp.selectSingleNode(@"/Root").AppendChild(dbnode);

        //輪詢各個Action
        XmlNodeList xnl = xpo.selectAllNodes(@"//Root/Action");
        foreach (XmlNode xno in xnl)
        {
            bool res = true;
            string errmsg = "";
            try
            {
                string acid = xno.Attributes["ID"].Value;
                if (acid.Equals("AddGroup"))
                {
                    string sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    DataSet ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        throw new Exception("此群組代號已經存在");
                    }
                    sql = "insert into SMSAABA(SMSAABA001, SMSAABA002, SMSAABA003, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values(";
                    sql += "'" + Utility.filter(IDProcessor.getID("")) + "',";
                    sql += "'" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "',";
                    sql += "'" + Utility.filter(xno.ChildNodes[0].Attributes["Title"].Value) + "',";
                    sql += "'WebService',";
                    sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                    sql += "'',";
                    sql += "'')";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("ModifyGroup"))
                {
                    string sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    DataSet ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組代號不存在");
                    }
                    sql = "update SMSAABA set SMSAABA003='" + Utility.filter(xno.ChildNodes[0].Attributes["Title"].Value) + "' where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("DeleteGroup"))
                {
                    string guid = "";
                    string sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    DataSet ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組代號不存在");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABA001"].ToString();
                    }
                    sql = "delete from SMSAABB where SMSAABB002='" + Utility.filter(guid) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }

                    sql = "delete from SMSAABC where SMSAABC002='" + Utility.filter(guid) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                    sql = "delete from SMSAABA where SMSAABA001='" + Utility.filter(guid) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("AddItem"))
                {
                    string guid = "";
                    string sql;
                    DataSet ds;

                    sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["GroupID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組代號不存在");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABA001"].ToString();
                    }

                    sql = "select * from SMSAABB where SMSAABB002='" + Utility.filter(guid) + "' and SMSAABB003='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        throw new Exception("此群組已經包含此權限項目");
                    }

                    sql = "select * from SMSAAAA where SMSAAAA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此權限項目代號不存在");
                    }

                    string auth = xno.ChildNodes[0].Attributes["Auth"].Value;
                    string[] authz = new string[10];
                    int auths = int.Parse(auth);
                    for (int z = 0; z < 10; z++)
                    {
                        int comp = (int)System.Math.Pow(2, z);
                        if ((auths & comp) > 0)
                        {
                            authz[z] = "Y";
                        }
                        else
                        {
                            authz[z] = "N";
                        }
                    }
                                       
                    sql = "insert into SMSAABB(SMSAABB001, SMSAABB002, SMSAABB003, AUTH01, AUTH02, AUTH03, AUTH04, AUTH05, AUTH06, AUTH07, AUTH08, AUTH09, AUTH10, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values(";
                    sql += "'" + Utility.filter(IDProcessor.getID("")) + "',";
                    sql += "'" + Utility.filter(guid) + "',";
                    sql += "'" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "',";
                    for (int z = 0; z < 10; z++)
                    {
                        sql += "'" + Utility.filter(authz[z]) + "', ";
                    }
                    sql += "'WebService',";
                    sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                    sql += "'',";
                    sql += "'')";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("DeleteItem"))
                {
                    string guid = "";
                    string sql;
                    DataSet ds;

                    sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["GroupID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組代號不存在");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABA001"].ToString();
                    }

                    sql = "select * from SMSAABB where SMSAABB002='" + Utility.filter(guid) + "' and SMSAABB003='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組不包含此權限項目");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABB001"].ToString();
                    }


                    sql = "delete from SMSAABB where SMSAABB001='" + Utility.filter(guid) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("ModifyItem"))
                {
                    string guid = "";
                    string sql;
                    DataSet ds;

                    sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["GroupID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組代號不存在");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABA001"].ToString();
                    }

                    sql = "select * from SMSAABB where SMSAABB002='" + Utility.filter(guid) + "' and SMSAABB003='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組不包含此權限項目");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABB001"].ToString();
                    }


                    string auth = xno.ChildNodes[0].Attributes["Auth"].Value;
                    int auths = int.Parse(auth);
                    string msql = "";
                    for (int z = 0; z < 10; z++)
                    {
                        int comp = (int)System.Math.Pow(2, z);
                        if ((auths & comp) > 0)
                        {
                            msql += "AUTH" + string.Format("{0:00}", z + 1) + "='Y', ";
                        }
                        else
                        {
                            msql += "AUTH" + string.Format("{0:00}", z + 1) + "='N', ";
                        }
                    }
                    msql = msql.Substring(0, msql.Length - 2);   
                    
                    sql = "update SMSAABB set "+msql+" where SMSAABB001='" + guid + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("AddUser"))
                {
                    string guid = "";
                    string sql;
                    DataSet ds;

                    sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["GroupID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組代號不存在");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABA001"].ToString();
                    }

                    sql = "select * from SMSAABC where SMSAABC002='" + Utility.filter(guid) + "' and SMSAABC003='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        throw new Exception("此群組已經包含此使用者");
                    }

                    sql = "select * from Users where id='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此使用者不存在");
                    }


                    sql = "insert into SMSAABC(SMSAABC001, SMSAABC002, SMSAABC003, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values(";
                    sql += "'" + Utility.filter(IDProcessor.getID("")) + "',";
                    sql += "'" + Utility.filter(guid) + "',";
                    sql += "'" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "',";
                    sql += "'WebService',";
                    sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                    sql += "'',";
                    sql += "'')";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("DeleteUser"))
                {
                    string guid = "";
                    string sql;
                    DataSet ds;

                    sql = "select * from SMSAABA where SMSAABA002='" + Utility.filter(xno.ChildNodes[0].Attributes["GroupID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組代號不存在");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABA001"].ToString();
                    }

                    sql = "select * from SMSAABC where SMSAABC002='" + Utility.filter(guid) + "' and SMSAABC003='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此群組不包含此使用者");
                    }
                    else
                    {
                        guid = ds.Tables[0].Rows[0]["SMSAABC001"].ToString();
                    }


                    sql = "delete from SMSAABC where SMSAABC001='" + Utility.filter(guid) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else
                {
                    res = false;
                    errmsg = "未知的操作動作命令";
                }
            }
            catch (Exception te)
            {
                res = false;
                errmsg = te.Message;
            }
            XmlNode xn = xp.getXmlDocument().ImportNode(xno, true);
            dbnode.AppendChild(xn);
            XmlNode resnode = xp.getXmlDocument().CreateElement("Result");
            XmlAttribute atr = xp.getXmlDocument().CreateAttribute("ReturnValue");

            if (res)
            {
                //成功
                atr.Value = "0";
                resnode.InnerText = "";
            }
            else
            {
                //失敗
                atr.Value = "1";
                resnode.InnerText = errmsg;
            }
            resnode.Attributes.Append(atr);
            xn.AppendChild(resnode);
            if (!res)
            {
                retv = false;
            }
        }

        return retv;
    }
    private bool processModifyAuthItem(AbstractEngine engine, string dbIndex, XMLProcessor xpo, ref XMLProcessor xp)
    {
        bool retv = true;

        //先根據dbIndex建立DBNode
        XmlNode dbnode = xp.getXmlDocument().CreateElement("DB");
        XmlAttribute dbid = xp.getXmlDocument().CreateAttribute("ID");
        dbid.Value = dbIndex;
        dbnode.Attributes.Append(dbid);
        xp.selectSingleNode(@"/Root").AppendChild(dbnode);

        //輪詢各個Action
        XmlNodeList xnl = xpo.selectAllNodes(@"//Root/Action");
        foreach (XmlNode xno in xnl)
        {
            bool res = true;
            string errmsg = "";
            try
            {
                string acid = xno.Attributes["ID"].Value;
                if (acid.Equals("AddItem"))
                {
                    string sql = "select * from SMSAAAA where SMSAAAA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    DataSet ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        throw new Exception("此權限項目代號已經存在");
                    }
                    sql = "insert into SMSAAAA(SMSAAAA001, SMSAAAA002, SMSAAAA003, SMSAAAA004, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values(";
                    sql += "'" + Utility.filter(IDProcessor.getID("")) + "',";
                    sql += "'" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "',";
                    sql += "'" + Utility.filter(xno.ChildNodes[0].Attributes["Title"].Value) + "',";
                    sql += "'" + Utility.filter(xno.ChildNodes[0].Attributes["Description"].Value) + "',";
                    sql += "'WebService',";
                    sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                    sql += "'',";
                    sql += "'')";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("ModifyItem"))
                {
                    string sql = "select * from SMSAAAA where SMSAAAA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    DataSet ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此權限項目代號不存在");
                    }
                    sql = "update SMSAAAA set SMSAAAA003='" + Utility.filter(xno.ChildNodes[0].Attributes["Title"].Value) + "', SMSAAAA004='" + Utility.filter(xno.ChildNodes[0].Attributes["Description"].Value) + "' where SMSAAAA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else if (acid.Equals("DeleteItem"))
                {
                    string sql = "select * from SMSAAAA where SMSAAAA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    DataSet ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        throw new Exception("此權限項目代號不存在");
                    }
                    sql = "delete from SMSAABB where SMSAABB003='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }

                    sql = "delete from SMSAAAA where SMSAAAA002='" + Utility.filter(xno.ChildNodes[0].Attributes["ID"].Value) + "'";
                    if (!engine.executeSQL(sql))
                    {
                        throw new Exception(engine.errorString);
                    }
                }
                else
                {
                    res = false;
                    errmsg = "未知的操作動作命令";
                }
            }
            catch (Exception te)
            {
                res = false;
                errmsg = te.Message;
            }
            XmlNode xn = xp.getXmlDocument().ImportNode(xno, true);
            dbnode.AppendChild(xn);
            XmlNode resnode = xp.getXmlDocument().CreateElement("Result");
            XmlAttribute atr = xp.getXmlDocument().CreateAttribute("ReturnValue");

            if (res)
            {
                //成功
                atr.Value = "0";
                resnode.InnerText = "";
            }
            else
            {
                //失敗
                atr.Value = "1";
                resnode.InnerText = errmsg;
            }
            resnode.Attributes.Append(atr);
            xn.AppendChild(resnode);
            if (!res)
            {
                retv = false;
            }
        }

        return retv;
    }
    private void queryAllAuthItem(string dbid, string dbtitle, string dbstr, string enginetype, XmlDocument doc, ref XmlNode curnode)
    {
        XmlNode nd = doc.CreateElement("DB");
        XmlAttribute atr = doc.CreateAttribute("ID");
        atr.Value = dbid;
        nd.Attributes.Append(atr);
        atr = doc.CreateAttribute("Title");
        atr.Value = dbtitle;
        nd.Attributes.Append(atr);
        curnode.AppendChild(nd);

        //建立資料庫連線
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(enginetype, dbstr);

        string sql = "";
        DataSet ds = null;

        sql = "select SMSAAAA002, SMSAAAA003, SMSAAAA004 from SMSAAAA";
        ds = engine.getDataSet(sql, "TEMP");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            XmlNode nd2 = doc.CreateElement("Item");
            atr = doc.CreateAttribute("ID");
            atr.Value = ds.Tables[0].Rows[i][0].ToString();
            nd2.Attributes.Append(atr);
            atr = doc.CreateAttribute("Title");
            atr.Value = ds.Tables[0].Rows[i][1].ToString();
            nd2.Attributes.Append(atr);
            atr = doc.CreateAttribute("Description");
            atr.Value = ds.Tables[0].Rows[i][2].ToString();
            nd2.Attributes.Append(atr);

            nd.AppendChild(nd2);
        }
        engine.close();
    }
    private void queryAllAuthSchema(string dbid, string dbtitle, string dbstr, string enginetype, string groupID, XmlDocument doc, ref XmlNode curnode)
    {
        XmlNode nd = doc.CreateElement("DB");
        XmlAttribute atr = doc.CreateAttribute("ID");
        atr.Value = dbid;
        nd.Attributes.Append(atr);
        atr = doc.CreateAttribute("Title");
        atr.Value = dbtitle;
        nd.Attributes.Append(atr);
        curnode.AppendChild(nd);

        queryAuthSchema(dbstr, enginetype, groupID, doc, ref nd);
    }
    private void queryAuthSchema(string dbstr, string enginetype, string groupID, XmlDocument doc, ref XmlNode curnode)
    {
        //建立資料庫連線
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(enginetype, dbstr);

        string sql = "";
        DataSet ds = null;

        //取得Group
        if (groupID.Equals(""))
        {
            sql = "select SMSAABA001, SMSAABA002, SMSAABA003 from SMSAABA";
        }
        else
        {
            sql = "select SMSAABA001, SMSAABA002, SMSAABA003 from SMSAABA where SMSAABA002='" + Utility.filter(groupID) + "'";
        }
        ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            XmlNode nd = doc.CreateElement("Group");
            XmlAttribute atr = doc.CreateAttribute("ID");
            atr.Value = ds.Tables[0].Rows[i][1].ToString();
            nd.Attributes.Append(atr);
            atr = doc.CreateAttribute("Title");
            atr.Value = ds.Tables[0].Rows[i][2].ToString();
            nd.Attributes.Append(atr);

            queryAuthDetail(engine, ds.Tables[0].Rows[i][0].ToString(), doc, ref nd);

            curnode.AppendChild(nd);
        }

        engine.close();
    }
    private void queryAuthDetail(AbstractEngine engine, string guid, XmlDocument doc, ref XmlNode curnode)
    {
        string sql;
        DataSet ds;

        XmlNode itemnode = doc.CreateElement("Items");
        XmlNode usernode = doc.CreateElement("Users");
        curnode.AppendChild(itemnode);
        curnode.AppendChild(usernode);

        //取得Items
        sql = "select SMSAABB003, SMSAAAA003, AUTH01, AUTH02, AUTH03, AUTH04, AUTH05, AUTH06, AUTH07, AUTH08, AUTH09, AUTH10 from SMSAABB inner join SMSAAAA on SMSAABB003=SMSAAAA002 where SMSAABB002='" + Utility.filter(guid) + "'";
        ds = engine.getDataSet(sql, "TEMP");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            XmlNode nd = doc.CreateElement("Item");
            XmlAttribute atr = doc.CreateAttribute("ID");
            atr.Value = ds.Tables[0].Rows[i][0].ToString();
            nd.Attributes.Append(atr);
            atr = doc.CreateAttribute("Title");
            atr.Value = ds.Tables[0].Rows[i][1].ToString();
            nd.Attributes.Append(atr);

            for (int z = 0; z < 10; z++)
            {
                atr = doc.CreateAttribute("AUTH"+string.Format("{0:00}",z+1));
                if (ds.Tables[0].Rows[i][2 + z].ToString().Equals("Y"))
                {
                    atr.Value = "Y";
                }
                else
                {
                    atr.Value = "N";
                }
                nd.Attributes.Append(atr);
            }
            itemnode.AppendChild(nd);
        }

        //取得Users
        sql = "select SMSAABC003, userName from SMSAABC inner join Users on SMSAABC003=Users.id where SMSAABC002='" + Utility.filter(guid) + "'";
        ds = engine.getDataSet(sql, "TEMP");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            XmlNode nd = doc.CreateElement("User");
            XmlAttribute atr = doc.CreateAttribute("ID");
            atr.Value = ds.Tables[0].Rows[i][0].ToString();
            nd.Attributes.Append(atr);
            atr = doc.CreateAttribute("Title");
            atr.Value = ds.Tables[0].Rows[i][1].ToString();
            nd.Attributes.Append(atr);
            usernode.AppendChild(nd);
        }
    }
    #endregion
}

