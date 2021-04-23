<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // 應用程式啟動時執行的程式碼
        com.dsc.kernal.utility.GlobalProperty.Filename = Server.MapPath("~/SettingFiles/setting.ini");
        com.dsc.kernal.global.GlobalCache.setValue("DataObjectFolder",Server.MapPath("~/SettingFiles/DataObjectFolder/"));
        com.dsc.kernal.global.GlobalCache.setValue("LanguageFolder", Server.MapPath("~/language/"));
		
		if (Application["OnlineUsers"] == null)
        {
            ArrayList arylUsers = new ArrayList();
            Application.Set("OnlineUsers", arylUsers);
		}
		
		if (Application["SessionLivingGroup"] == null) 
        {
            Hashtable htSeCollection = new Hashtable();
            Application["SessionLivingGroup"] = htSeCollection;
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        if (com.dsc.kernal.utility.GlobalProperty.getProperty("global", "isDebugSession").Equals("1"))
        {
            //  應用程式關閉時執行的程式碼
            HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.GetField, null, null, null);
            if (runtime == null)
                return;

            string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField, null, runtime, null);

            string shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField, null, runtime, null);

            if (!System.Diagnostics.EventLog.SourceExists("ECP"))
            {
                System.Diagnostics.EventLog.CreateEventSource("ECP", "Application");
            }

            System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
            log.Source = "ECP";

            log.WriteEntry(String.Format("\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}", shutDownMessage, shutDownStack), System.Diagnostics.EventLogEntryType.Error);
        }
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 發生未處理錯誤時執行的程式碼

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 啟動新工作階段時執行的程式碼
        com.dsc.kernal.utility.GlobalProperty.Filename = Server.MapPath("~/SettingFiles/setting.ini");
        String clientconnectstr = "";
        clientconnectstr = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "clientconnectstr");
        com.dsc.kernal.global.GlobalCache.setValue("CONNECTSTR", clientconnectstr);
		
		//跨站整合客製
        if (Application["SessionLivingGroup"] != null)
        {
            Hashtable htSeCollection = (Hashtable)Application["SessionLivingGroup"];
            if (!htSeCollection.ContainsKey(Session.SessionID))
            {
                htSeCollection.Add(Session.SessionID, com.dsc.kernal.utility.IDProcessor.getID(""));
            }
            else
            {
                htSeCollection[Session.SessionID] = com.dsc.kernal.utility.IDProcessor.getID("");
            }
        }
    }

    void Session_End(object sender, EventArgs e) 
    {
        ////跨站整合客製
        if (Application["SessionLivingGroup"] != null)
        {
            Hashtable htSeCollection = (Hashtable)Application["SessionLivingGroup"];
            if (htSeCollection.ContainsKey(Session.SessionID))
            {
                htSeCollection.Remove(Session.SessionID);
            }
        }
		
		// 工作階段結束時執行的程式碼。 
        // 注意: 只有在 Web.config 檔將 sessionstate 模式設定為 InProc 時，
        // 才會引發 Session_End 事件。如果將工作階段模式設定為 StateServer 
        // 或 SQLServer，就不會引發這個事件。
        string sClass = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();
        string connectString = acs.connectString;
        string engineType = acs.engineType;
        com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
        com.dsc.kernal.factory.AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select LOGOUTTIME from LOGINDATA where SESSIONID='" + com.dsc.kernal.utility.Utility.filter(Session.SessionID) + "'";
        System.Data.DataSet ds = engine.getDataSet(sql, "TEMP");
        if ((ds.Tables[0].Rows.Count > 0) && (ds.Tables[0].Rows[0][0].ToString().Equals("")))
        {
            sql = "update LOGINDATA set LOGOUTTIME='" + com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null) + "' where SESSIONID='" + com.dsc.kernal.utility.Utility.filter(Session.SessionID) + "'";
            engine.executeSQL(sql);
        }
        engine.close();


    }
       
</script>
