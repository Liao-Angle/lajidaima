<%@ Page language="c#" validateRequest="false" %>
<%@ Import namespace="System.IO"%>
<%@ Import namespace="System.Text"%>
<%@ Import namespace="System.Web"%>
<%@ Import namespace="com.dsc.kernal.utility"%>
<%@ Import namespace="com.dsc.kernal.agent"%>
<%@ Import namespace="com.dsc.kernal.factory"%>
<%@ Import namespace="com.dsc.kernal.global"%>
<%@ Import namespace="com.dsc.kernal.databean"%>
<%@ Import namespace="System.Data"%>
<%@ Import namespace="System.Collections"%>
<%@ Import namespace="System.Runtime.Remoting"%>
<%@ Import Namespace="WebServerProject.auth" %>

<%
			// 在這裡放置使用者程式碼以初始化網頁
			Response.Clear();
			Response.Cache.SetExpires(DateTime.Now);
			Response.ContentType="text/xml";

            try
            {
                //string curPath=Utility.extractPath(Server.MapPath(Request.Path))[0] + @"..\bin\";
                //System.Environment.CurrentDirectory = curPath;
                
                NameValueCollection ncq = Request.Form;
                NameValueCollection ncv = Request.ServerVariables;
                NameValueCollection ncs = Request.QueryString;

                //StreamWriter writer = new StreamWriter(Server.MapPath("~\\Utility\\log_" + DateTime.Now.ToLongDateString() + ".txt"), true);
                //writer.WriteLine("");
                //writer.WriteLine("------------------" + DateTime.Now.ToString());
                //for (int i = 0; i < ncq.AllKeys.Length; i++)
                //{
                //    writer.WriteLine(ncq.GetKey(i) + " :: " + ncq[i]);
                //}
                //writer.WriteLine("");
                //writer.WriteLine("------------------");
                //writer.Close();
                
                //系統參數
                string workPath = Request.PhysicalApplicationPath;
                string clientIP = Request.UserHostAddress;

                //取得所有參數
                string method = ncq["method"];
                if (method == null)
                {
                    method = ncs["method"];
                }
                string TransactionKey = ncq["TransactionKey"];
                if (TransactionKey == null)
                {
                    TransactionKey = ncs["TransactionKey"];
                }
                if(TransactionKey==null)
                {
                    TransactionKey="";
                }

                if (method.Equals("exec"))
                {
                    string sql = ncq["sql"]; //processID
                    AbstractEngine engine = null;
                    if (TransactionKey.Equals(""))
                    {
                        String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                        string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                        Session["userdbstr"] = connectString;
                        IOFactory factory = new IOFactory();
                        engine = (AbstractEngine)factory.getEngine(engineType, connectString);
                        engine.startTransaction(IsolationLevel.ReadCommitted);
                    }
                    else
                    {
                        if (GlobalCache.getValue("ENGINE" + TransactionKey) == null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                        else
                        {
                            engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        }
                    }
                    
                    try
                    {
                        DataSet ds = engine.exec(sql);
                        object returnValue = engine.spReturnValue;
                        Hashtable hs = engine.spOutput;

                        //engine.close();

                        string retStr = "";

                        if (ds != null)
                        {
                            string tpf = IDProcessor.getID("");
                            StringWriter sr = new StringWriter();
                            
                            ds.WriteXml(sr, System.Data.XmlWriteMode.WriteSchema);

                            retStr = sr.ToString();
                            sr.Close();

                            retStr = com.dsc.kernal.utility.Utility.replaceSpecialString(retStr);
                        }
                        else
                        {
                            retStr = "*";
                        }
                        retStr += ";;;;;;;;;;";

                        retStr += returnValue + ";;;;;;;;;;";
                        IDictionaryEnumerator ie = hs.GetEnumerator();
                        bool hasOutput = false;
                        while (ie.MoveNext())
                        {
                            hasOutput = true;
                            retStr += ie.Key.ToString() + ";;;;;;;;" + ie.Value.ToString() + ";;;;;;;;;";
                        }
                        if (hasOutput) retStr = retStr.Substring(0, retStr.Length - 9);

                        Response.Write(retStr);

                        if (TransactionKey.Equals(""))
                        {
                            engine.commit();
                            engine.close();
                        }
                    }
                    catch (Exception te)
                    {
                        if (TransactionKey.Equals(""))
                        {
                            engine.rollback();
                            engine.close();
                        }
                        throw new Exception("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + te.Message);
                    }
                }
                else if (method.Equals("getDataSet"))
                {
                    string sql = ncq["sql"]; 
                    string tablename = ncq["tablename"]; 

                    AbstractEngine engine = null;
                    if (TransactionKey.Equals(""))
                    {
                        String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                        string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                        Session["userdbstr"] = connectString;
                        IOFactory factory = new IOFactory();
                        engine = (AbstractEngine)factory.getEngine(engineType, connectString);
                        if (engine==null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                    }
                    else
                    {
                        if (GlobalCache.getValue("ENGINE" + TransactionKey) == null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                        else
                        {
                            engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        }
                    }

                    try
                    {
                        string tpf = IDProcessor.getID("");
                        DataSet ds = null;

                        //20121004 hjlin modify start
                        string rowCount = ncq["rowCount"];
                        string paramtag = ncq["param"];
                        string parseMode = ncq["parseMode"];

                        if ((rowCount != null) && (!rowCount.Equals("")))
                        {
                            ds = engine.getDataSetLimit(sql, tablename, int.Parse(rowCount));
                        }
                        else if ((paramtag != null) && (!paramtag.Equals("")) && (parseMode != null) && (!parseMode.Equals("")))
                        {
                            String[] sptag1 = new String[] { "^|$*%)" };
                            String[] sptag2 = new String[] { "(|%($|" };
                            string[] paramList1 = paramtag.Split(sptag2, StringSplitOptions.None);
                            String[,] param = new String[paramList1.Length,3];
                            for (int i = 0; i < paramList1.Length; i++)
                            {
                                string[] paramList2 = paramList1[i].Split(sptag1, StringSplitOptions.None);
                                if (bool.Parse(parseMode))
                                {
                                    param[i, 0] = paramList2[0];
                                }
                                else
                                {
                                    param[i, 0] = paramList2[0];
                                    param[i, 1] = paramList2[1];
                                    param[i, 2] = paramList2[2];
                                }
                            }
                            ds = engine.getDataSet(sql, tablename, param, bool.Parse(parseMode));
                        }
                        else
                        {
                            ds = engine.getDataSet(sql, tablename);
                        }
                        //20121004 hjlin modify end
                        
                        System.IO.StringWriter sw = new StringWriter();
                        ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                        string retStr = sw.ToString();
                        sw.Close();
                        
                        retStr = com.dsc.kernal.utility.Utility.replaceSpecialString(retStr);

                        Response.Write(retStr);
                        if (TransactionKey.Equals(""))
                        {
                            engine.close();
                        }
                    }
                    catch (Exception tte)
                    {
                        Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: "+tte.Message);
                        if (TransactionKey.Equals(""))
                        {
                            engine.close();
                        }
                    }
                    
                    
                }
                else if (method.Equals("updateDataSet"))
                {
                    string strs = ncq["ds"]; //processID
                    string schema=ncq["schema"];

                    DataSet ds = new DataSet();

                    StringReader sr = new StringReader(Utility.recoverSpecialString(schema));
                    ds.ReadXmlSchema(sr);
                    sr.Close();

                    sr = new StringReader(Utility.recoverSpecialString(strs));
                    ds.ReadXml(sr, XmlReadMode.DiffGram);
                    sr.Close();
                   
                    AbstractEngine engine = null;
                    if (TransactionKey.Equals(""))
                    {
                        String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                        string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                        Session["userdbstr"] = connectString;
                        IOFactory factory = new IOFactory();
                        engine = (AbstractEngine)factory.getEngine(engineType, connectString);
                        engine.startTransaction(IsolationLevel.ReadCommitted);
                    }
                    else
                    {
                        if (GlobalCache.getValue("ENGINE" + TransactionKey) == null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                        else
                        {
                            engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        }
                    }

                    bool result = true;
                    result=engine.updateDataSet(ds);
                    
                    if (result)
                    {
                        Response.Write("");
                        if (TransactionKey.Equals(""))
                        {
                            engine.commit();
                            engine.close();
                        }
                    }
                    else
                    {
                        Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + engine.errorString);
                        if (TransactionKey.Equals(""))
                        {
                            engine.rollback();
                            engine.close();
                        }
                    }
                }
                    
                else if (method.Equals("startTransaction"))
                {
                    string sql = ncq["sql"]; //processID
                    string tablename = ncq["tablename"]; //APName
                    String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                    string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                    Session["userdbstr"] = connectString;
                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = (AbstractEngine)factory.getEngine(engineType, connectString);

                    string level = ncq["level"];
                    switch (level)
                    {
                        case "ReadCommitted":
                            engine.startTransaction(IsolationLevel.ReadCommitted);
                            break;
                        case "Chaos":
                            engine.startTransaction(IsolationLevel.Chaos);
                            break;
                        case "ReadUncommitted":
                            engine.startTransaction(IsolationLevel.ReadUncommitted);
                            break;
                        case "RepeatableRead":
                            engine.startTransaction(IsolationLevel.RepeatableRead);
                            break;
                        case "Serializable":
                            engine.startTransaction(IsolationLevel.Serializable);
                            break;
                        case "Snapshot":
                            engine.startTransaction(IsolationLevel.Snapshot);
                            break;
                        case "Unspecified":
                            engine.startTransaction(IsolationLevel.Unspecified);
                            break;
                        default:
                            engine.startTransaction(IsolationLevel.ReadCommitted);
                            break;
                    }
                    string keyname = IDProcessor.getID("");
                    keyname = keyname.Replace("-", "");
                    GlobalCache.setValue("ENGINE" + keyname, engine);
                    Response.Write(keyname);
                }
                else if (method.Equals("commit"))
                {
                    try
                    {
                        AbstractEngine engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        engine.commit();
                    }
                    catch (Exception te) { };
                }
                else if (method.Equals("rollback"))
                {
                    try
                    {
                        AbstractEngine engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        engine.rollback();
                    }
                    catch (Exception te) { };
                }
                else if (method.Equals("close"))
                {
                    try
                    {
                        AbstractEngine engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        engine.close();
                        GlobalCache.setValue("ENGINE" + TransactionKey, null);
                    }
                    catch (Exception te) { };
                }
                else if (method.Equals("SendMail"))
                {
                    string from = ncq["from"];
                    string to = ncq["to"];
                    string cc = ncq["cc"];
                    string subject = ncq["subject"];
                    string body = ncq["body"];

                    MailProcessor.sendMailHTML("127.0.0.1", to, cc, from, subject, body);
                }
                else if (method.Equals("getAuthGroup"))
                {
                    string userid = ncq["userid"];
                    String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                    string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                    Session["userdbstr"] = connectString;
                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = (AbstractEngine)factory.getEngine(engineType, connectString);

                    AUTHAgent ags = new AUTHAgent();
                    ags.engine = engine;
                    string[] ret = ags.getServerAuthGroup(userid);
                    string rv = "";
                    for (int i = 0; i < ret.Length; i++)
                    {
                        rv += ret[i] + ";";
                    }
                    rv = rv.Substring(0, rv.Length - 1);
                    engine.close();
                    Response.Write(rv);


                }
                else if (method.Equals("getAllAuth"))
                {
                    string userid = ncq["userid"];
                    string[] groupid = ncq["groupid"].Split(new char[] { ';' });
                    String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                    string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                    Session["userdbstr"] = connectString;
                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = (AbstractEngine)factory.getEngine(engineType, connectString);

                    AUTHAgent ags = new AUTHAgent();
                    ags.engine = engine;
                    Hashtable hs = ags.getAllAuth(userid, groupid);

                    string rets = "";
                    IDictionaryEnumerator myEnumerator = hs.GetEnumerator();
                    while (myEnumerator.MoveNext())
                    {
                        string key = (string)myEnumerator.Key;
                        int auth = (int)myEnumerator.Value;

                        rets += key + ":" + auth.ToString() + ";";
                    }
                    if (rets.Length > 0)
                    {
                        rets = rets.Substring(0, rets.Length - 1);
                    }
                    Response.Write(rets);
                }
                else if (method.Equals("updateDataObjectSet"))
                {
                    string xmlset = ncq["xmlset"]; 
                    string agentname = ncq["agentname"]; 
                    string childname = ncq["childname"];

                    AbstractEngine engine = null;
                    if (TransactionKey.Equals(""))
                    {
                        String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                        string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                        Session["userdbstr"] = connectString;
                        IOFactory factory = new IOFactory();
                        engine = (AbstractEngine)factory.getEngine(engineType, connectString);
                        engine.startTransaction(IsolationLevel.ReadCommitted);
                    }
                    else
                    {
                        if (GlobalCache.getValue("ENGINE" + TransactionKey) == null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                        else
                        {
                            engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        }
                    }
                    DataObjectSet dos = new DataObjectSet();
                    dos.loadXML3(xmlset);

                    string assemblyName = agentname;
                    string childClassString = childname;

                    //20121005 hjlin modify start
                    AbstractAgent oGet;
                    if (childClassString.IndexOf("NLAgent") >= 0)
                    {//com.dsc.kernal.agent.NLAgent
                        NLAgent agent = new NLAgent(engine);
                        agent.loadSchema(dos.getChildClassString()+"Agent");
                        oGet = (AbstractAgent)agent;
                    }
                    else
                    {
                        oGet = (AbstractAgent)Utility.getClasses(assemblyName, childClassString);
                        oGet.engine = engine;
                    }
                    //20121005 hjlin modify end
                    
                    oGet.defaultData = dos;
                    bool result = oGet.update();
                    if (result)
                    {
                        Response.Write("");
                        if (TransactionKey.Equals(""))
                        {
                            engine.commit();
                            engine.close();
                        }
                    }
                    else
                    {
                        Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + engine.errorString);
                        if (TransactionKey.Equals(""))
                        {
                            engine.rollback();
                            engine.close();
                        }
                    }
                }
                else if (method.Equals("updateDataObjectSetFormMode"))
                {
                    string xmlset = ncq["xmlset"]; //processID
                    string agentname = ncq["agentname"]; //APName
                    string childname = ncq["childname"];

                    AbstractEngine engine = null;
                    if (TransactionKey.Equals(""))
                    {
                        String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                        string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                        Session["userdbstr"] = connectString;
                        IOFactory factory = new IOFactory();
                        engine = (AbstractEngine)factory.getEngine(engineType, connectString);
                        engine.startTransaction(IsolationLevel.ReadCommitted);
                    }
                    else
                    {
                        if (GlobalCache.getValue("ENGINE" + TransactionKey) == null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                        else
                        {
                            engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        }
                    }

                    DataObjectSet dos = new DataObjectSet();
                    dos.loadXML3(xmlset);

                    string assemblyName = agentname;
                    string childClassString = childname;
                    /*
                    string assemblyNameStr = assemblyName + "," + "Version=1.0.0.1,Culture=Neutral,PublicKeyToken=affe844ceef8e1bc";
                    ObjectHandle objHandleGet = Activator.CreateInstance(assemblyNameStr, childname);
                    AbstractAgent oGet = (AbstractAgent)objHandleGet.Unwrap();
                    */

                    //20121005 hjlin modify start
                    AbstractAgent oGet;
                    if (childClassString.IndexOf("NLAgent") >= 0)
                    {//com.dsc.kernal.agent.NLAgent
                        NLAgent agent = new NLAgent(engine);
                        agent.loadSchema(dos.getChildClassString() + "Agent");
                        oGet = (AbstractAgent)agent;
                    }
                    else
                    {
                        oGet = (AbstractAgent)Utility.getClasses(assemblyName, childClassString);
                        oGet.engine = engine;
                    }
                    //20121005 hjlin modify end

                    oGet.defaultData = dos;
                    bool result = oGet.updateFormMode();
                    if (result)
                    {
                        Response.Write("");
                        if (TransactionKey.Equals(""))
                        {
                            engine.commit();
                            engine.close();
                        }
                    }
                    else
                    {
                        Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + engine.errorString);
                        if (TransactionKey.Equals(""))
                        {
                            engine.rollback();
                            engine.close();
                        }
                    }

                }
                else if (method.Equals("executeSQL"))
                {
                    string sql = ncq["sql"]; //processID
                    string tablename = ncq["tablename"]; //APName

                    AbstractEngine engine = null;
                    if (TransactionKey.Equals(""))
                    {
                        String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                        string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                        Session["userdbstr"] = connectString;
                        IOFactory factory = new IOFactory();
                        engine = (AbstractEngine)factory.getEngine(engineType, connectString);
                        engine.startTransaction(IsolationLevel.ReadCommitted);
                    }
                    else
                    {
                        if (GlobalCache.getValue("ENGINE" + TransactionKey) == null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                        else
                        {
                            engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        }
                    }

                    try
                    {
                        if (engine.executeSQL(sql))
                        {
                            if (TransactionKey.Equals(""))
                            {
                                engine.commit();
                                engine.close();
                            }
                            Response.Write("0");
                        }
                        else
                        {
                            if (TransactionKey.Equals(""))
                            {
                                engine.rollback();
                                engine.close();
                            }
                            Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + engine.errorString);
                        }
                    }
                    catch (Exception te)
                    {
                        if (TransactionKey.Equals(""))
                        {
                            engine.rollback();
                            engine.close();
                        }
                        Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + engine.errorString);
                    }
                }
                else if (method.Equals("executeScalar"))
                {
                    string sql = ncq["sql"]; //processID

                    AbstractEngine engine = null;
                    if (TransactionKey.Equals(""))
                    {
                        String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                        string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                        Session["userdbstr"] = connectString;
                        IOFactory factory = new IOFactory();
                        engine = (AbstractEngine)factory.getEngine(engineType, connectString);
                        engine.startTransaction(IsolationLevel.ReadCommitted);
                    }
                    else
                    {
                        if (GlobalCache.getValue("ENGINE" + TransactionKey) == null)
                        {
                            throw new Exception("資料庫連線已經關閉, 請重新登入");
                        }
                        else
                        {
                            engine = (AbstractEngine)GlobalCache.getValue("ENGINE" + TransactionKey);
                        }
                    }

                    try
                    {
                        object rets=engine.executeScalar(sql);
                        
                        if ((rets==null) && (!engine.errorString.Equals("")))
                        {
                            if (TransactionKey.Equals(""))
                            {
                                engine.rollback();
                                engine.close();
                            }
                            Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + engine.errorString);
                        }
                        else
                        {
                            if (TransactionKey.Equals(""))
                            {
                                engine.commit();
                                engine.close();
                            }
                            if (rets == null)
                            {
                                rets = "";
                            }
                            Response.Write("successvalue: "+rets.ToString());
                        }
                    }
                    catch (Exception te)
                    {
                        if (TransactionKey.Equals(""))
                        {
                            engine.rollback();
                            engine.close();
                        }
                        Response.Write("資料庫連線發生錯誤, 請重新登入. 其他錯誤訊息: " + engine.errorString);
                    }
                }
                else if (method.Equals("OpenWin"))
                {
                    string tableID = ncq["tableID"]; //processID
                    string serialNum = ncq["serialNum"]; //APName
                    string pm = ncq["param"];
                    string whereClause = ncq["whereClause"];
                    string pmode = ncq["parseMode"];
                    int page = int.Parse(ncq["page"]);
                    int pagesize = int.Parse(ncq["pagesize"]);
                    bool parseMode = true;
                    if (pmode.Equals("Y"))
                        parseMode = true;
                    else
                        parseMode = false;
                    String[,] param;
                    String[] paramtag;

                    if ((pm != null) && (!pm.Equals("")))
                    {
                        paramtag = pm.Split(new char[] { ',' });

                        param = new String[paramtag.Length, 3];
                        for (int q = 0; q < paramtag.Length; q++)
                        {
                            param[q, 0] = "@" + paramtag[q].Trim();
                        }
                    }
                    else
                    {
                        paramtag = null;
                        param = null;
                    }
                    AbstractOpenWindowAgent owoagent;
                    string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                    if (engineType.Equals(EngineConstants.SQL))
                    {
                        owoagent = new OpenWindowAgent();
                    }
                    else
                    {
                        owoagent = new OpenWindowOracleAgent();
                    }
                    String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                    Session["userdbstr"] = connectString;

                    ArrayList ary = owoagent.queryData("", connectString, "", connectString, tableID, serialNum, param, whereClause, parseMode, page, pagesize);
                    string retStr = "<ArrayList>";

                    DataObjectSet dos = (DataObjectSet)ary[0];
                    retStr += "<ITEM0>" + dos.saveXML() + "</ITEM0>";

                    string displayName = "";
                    string[] tag = (string[])ary[1];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM1>" + displayName + "</ITEM1>";

                    displayName = "";
                    tag = (string[])ary[2];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM2>" + displayName + "</ITEM2>";

                    displayName = "";
                    tag = (string[])ary[3];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM3>" + displayName + "</ITEM3>";

                    displayName = "";
                    tag = (string[])ary[4];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM4>" + displayName + "</ITEM4>";

                    retStr += "</ArrayList>";
                    Response.Write(retStr);
                }
                else if (method.Equals("OpenValidate"))
                {
                    string tableID = ncq["tableID"]; //processID
                    string serialNum = ncq["serialNum"]; //APName
                    string pm = ncq["param"];
                    string whereClause = ncq["whereClause"];
                    string pmode = ncq["parseMode"];
                    int page = int.Parse(ncq["page"]);
                    int pagesize = int.Parse(ncq["pagesize"]);
                    bool parseMode = true;
                    if (pmode.Equals("Y"))
                        parseMode = true;
                    else
                        parseMode = false;
                    String[,] param;
                    String[] paramtag;

                    if ((pm != null) && (!pm.Equals("")))
                    {
                        paramtag = pm.Split(new char[] { '~' });

                        param = new String[paramtag.Length, 3];
                        for (int q = 0; q < paramtag.Length; q++)
                        {
                            string[] tgs = paramtag[q].Split(new char[] { '|' });
                            param[q, 0] = tgs[0].Trim();
                            param[q, 1] = tgs[1].Trim();
                            param[q, 2] = tgs[2].Trim();
                        }
                    }
                    else
                    {
                        paramtag = null;
                        param = null;
                    }
                    AbstractOpenWindowAgent owoagent;
                    string engineType = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "engineType");
                    if (engineType.Equals(EngineConstants.SQL))
                    {
                        owoagent = new OpenWindowAgent();
                    }
                    else
                    {
                        owoagent = new OpenWindowOracleAgent();
                    }
                    String connectString = com.dsc.kernal.utility.GlobalProperty.getProperty("connect", "dbstr");
                    Session["userdbstr"] = connectString;

                    ArrayList ary = owoagent.queryData("", connectString, "", connectString, tableID, serialNum, param, whereClause, parseMode, page, pagesize);

                    string retStr = "<ArrayList>";

                    DataObjectSet dos = (DataObjectSet)ary[0];
                    retStr += "<ITEM0>" + dos.saveXML() + "</ITEM0>";

                    string displayName = "";
                    string[] tag = (string[])ary[1];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM1>" + displayName + "</ITEM1>";

                    displayName = "";
                    tag = (string[])ary[2];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM2>" + displayName + "</ITEM2>";

                    displayName = "";
                    tag = (string[])ary[3];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM3>" + displayName + "</ITEM3>";

                    displayName = "";
                    tag = (string[])ary[4];
                    for (int i = 0; i < tag.Length; i++)
                    {
                        displayName += tag[i] + "#";
                    }
                    displayName = displayName.Substring(0, displayName.Length - 1);
                    retStr += "<ITEM4>" + displayName + "</ITEM4>";

                    retStr += "</ArrayList>";
                    Response.Write(retStr);
                }
                //此方法為上傳檔案處理完成後續動作
                else if (method.Equals("confirmSave"))
                {
                    string guid = ncq["guid"]; //processID
                    string level1 = ncq["level1"];
                    string level2 = ncq["level2"];
                    //String connectString = (String)Session["userdbstr"];
                    string folderName = com.dsc.kernal.utility.Utility.G_GetTempPath(); //暫存存放目錄
                    string oriPath = folderName + guid;

                    string retStr = "";
                    try
                    {
                        string assemblyName = "";
                        string childClassString = GlobalProperty.getProperty("global", "fileStorageAdapter");
                        assemblyName = childClassString.Split(new char[] { '.' })[0].Trim();
                        
                        /*
                        string assemblyNameStr = assemblyName + "," + "Version=1.0.0.1,Culture=Neutral,PublicKeyToken=affe844ceef8e1bc";

                        ObjectHandle objHandle = Activator.CreateInstance(assemblyNameStr, childClassString);
                        com.dsc.kernal.document.AbstractDocumentAdapter o = (com.dsc.kernal.document.AbstractDocumentAdapter)objHandle.Unwrap();
                        */
                        com.dsc.kernal.document.AbstractDocumentAdapter o = (com.dsc.kernal.document.AbstractDocumentAdapter)Utility.getClasses(assemblyName, childClassString);

                        o.saveFile(oriPath, level1, level2, guid);

                    }
                    catch (Exception ze)
                    {
                        retStr = ze.Message;
                    }
                    Response.Write(com.dsc.kernal.utility.Utility.composeResultXML(retStr));
                    //Response.Write("0");
                }
                //此方法為下載檔案動作
                else if (method.Equals("DownloadFileBrowser"))
                {
                    string realname = ncs["realname"];
                    string level1 = ncs["level1"];
                    string level2 = ncs["level2"];
                    string IdentityID = ncs["IdentityID"];

                    string folderName = com.dsc.kernal.utility.Utility.G_GetTempPath(); //暫存存放目錄
                    string oriPath = folderName + IdentityID;

                    string retStr = "";
                    try
                    {
                        string assemblyName = "";
                        string childClassString = GlobalProperty.getProperty("global", "fileStorageAdapter");
                        assemblyName = childClassString.Split(new char[] { '.' })[0].Trim();

                        /*
                        string assemblyNameStr = assemblyName + "," + "Version=1.0.0.1,Culture=Neutral,PublicKeyToken=affe844ceef8e1bc";

                        ObjectHandle objHandle = Activator.CreateInstance(assemblyNameStr, childClassString);
                        com.dsc.kernal.document.AbstractDocumentAdapter o = (com.dsc.kernal.document.AbstractDocumentAdapter)objHandle.Unwrap();
                        */
                        com.dsc.kernal.document.AbstractDocumentAdapter o = (com.dsc.kernal.document.AbstractDocumentAdapter)Utility.getClasses(assemblyName, childClassString);
                       
                        o.getFile(oriPath, level1, level2, IdentityID);

                    }
                    catch (Exception ze)
                    {
                        retStr = ze.Message;
                    }
                    if (!File.Exists(oriPath))
                    {
                        Response.Status = "404 找不到檔案";
                        Response.Write(Response.Status+retStr);
                        //Response.End();
                    }
                    else
                    {
                        Response.Clear();
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(realname));
                        Response.WriteFile(oriPath);
                    }
                }
                //上傳檔案動作
                else if (method.Equals("UploadFileBlock"))
                {
                    string guid = ncq["guid"];
                    string hash = ncq["content"]; //區塊內容
                    string folderName = com.dsc.kernal.utility.Utility.G_GetTempPath(); //暫存存放目錄
                    //string folderName="C:\\";
                    string tempName = folderName + guid;
                    if (!File.Exists(tempName))
                    {
                        //File.Create(tempName);
                    }
                    FileStream fs = new FileStream(tempName, FileMode.Append);
                    try
                    {

                        byte[] content = com.dsc.kernal.utility.Base64.decode(hash);
                        fs.Write(content, 0, content.Length);
                        fs.Close();
                    }
                    catch (Exception ue)
                    {
                        Response.Write(ue.Message);

                        fs.Close();
                    }
                }

                //此方法已經不使用
                else if (method.Equals("DownloadFile"))
                {
                    string filepath = ncq["filepath"];
                    long block = long.Parse(ncq["block"]);
                    if (!File.Exists(filepath))
                    {
                        Response.Write("");
                    }
                    else
                    {
                        FileStream fs = new FileStream(filepath, FileMode.Open);
                        long offset = block * 2048;
                        if (offset >= fs.Length)
                        {
                            fs.Close();
                            Response.Write("");
                        }
                        else
                        {
                            byte[] rets;
                            long curlength;
                            if ((offset + 2048) > fs.Length)
                            {
                                curlength = fs.Length - offset;
                            }
                            else
                            {
                                curlength = 2048;
                            }
                            rets = new byte[curlength];
                            fs.Seek(offset, SeekOrigin.Begin);
                            fs.Read(rets, 0, (int)curlength);
                            Response.Write(com.dsc.kernal.utility.Base64.encode(rets));
                            fs.Close();
                        }
                    }
                }
                //此方法已經不使用
                else if (method.Equals("FileUpload"))
                {
                    string folderName = com.dsc.kernal.utility.Utility.G_GetTempPath(); //暫存存放目錄
                    //string folderName=@"C:\";
                    //string realName=Utility.extractPath(Request.Files[0].FileName)[1];		

                    string tempName = IDProcessor.getID("");
                    //儲存實體檔案
                    Request.Files[0].SaveAs(folderName + tempName);
                    Response.Write(Utility.composeResultXML(tempName));
                }
                else
                {
                    Response.Write("找不到方法");
                }
            }
            catch (Exception xe)
            {
                Response.Write("伺服器處理發生錯誤, 請重新登入. 其他錯誤訊息: "+xe.Message);
            }

%>
