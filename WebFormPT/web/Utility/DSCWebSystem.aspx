<%@ Page Language="C#" %>
<%@ Import namespace="System.IO"%>
<%@ Import namespace="System.Text"%>
<%@ Import namespace="System.Web"%>
<%@ Import namespace="com.dsc.kernal.utility"%>
<%@ Import namespace="com.dsc.flow.data"%>
<%@ Import namespace="com.dsc.kernal.factory"%>
<%@ Import namespace="com.dsc.flow.server"%>
<%@ Import namespace="com.dsc.kernal.databean"%>
<%@ Import namespace="System.Data"%>
<%@ Import namespace="System.Collections"%>
<%@ Import namespace="WebServerProject"%>
<%
    string method = Request.QueryString["method"];
    if (method.Equals("getClientNotifyMessage"))
    {
        string userID = Request.QueryString["userID"];
        string PWD = Request.QueryString["PWD"];
        string retv = "";

        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine;

        engine = factory.getEngine(acs.engineType, acs.connectString);

        //先確認userID是否允許使用此系統
        System.Security.Cryptography.SHA1 md = System.Security.Cryptography.SHA1.Create();

        string oriPwd = PWD;
        String tSalt = "abcedefghijklmnopqrstuvwxyz";


        byte[] oriData = System.Text.Encoding.Default.GetBytes(tSalt + oriPwd);


        byte[] data = md.ComputeHash(oriData);


        string hashPwd = "";
        hashPwd = com.dsc.kernal.utility.Base64.encode(data);

        string sx = "select * from SMVTAAA where SMVTAAA002='" + userID + "' and SMVTAAA003='" + hashPwd + "'";
        DataSet ddt = engine.getDataSet(sx, "TEMP");
        if (ddt.Tables[0].Rows.Count > 0)
        {

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

            if (retv.Length > 0)
            {
                retv = "查詢時間: " + DateTimeUtility.getSystemTime2(null) + "\r\n" + retv;
            }
        }
        else
        {
            retv = "您沒有使用權限, 或是帳號密碼錯誤";
        }
        engine.close();

        // Create two different encodings.
        Encoding ascii = Encoding.ASCII;
        Encoding unicode = Encoding.Default;

        //Convert the string into a byte[].
        byte[] unicodeBytes = unicode.GetBytes(retv);

        // Perform the conversion from one encoding to the other.
        byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

        // Convert the byte[] into a string
        char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
        ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
        string asciiString = new string(asciiChars);

        Response.ContentEncoding = Encoding.Default;
        Response.Write(retv);
    }
%>