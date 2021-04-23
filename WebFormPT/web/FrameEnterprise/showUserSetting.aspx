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

<%
			// 在這裡放置使用者程式碼以初始化網頁
			Response.Clear();
			Response.Cache.SetExpires(DateTime.Now);
			Response.ContentType="text/html";


            try
            {
                //string curPath=Utility.extractPath(Server.MapPath(Request.Path))[0] + @"..\bin\";
                //System.Environment.CurrentDirectory = curPath;

                NameValueCollection ncq = Request.Form;
                NameValueCollection ncv = Request.ServerVariables;
                NameValueCollection ncs = Request.QueryString;

                //系統參數
                string workPath = Request.PhysicalApplicationPath;
                string clientIP = Request.UserHostAddress;

                //取得所有參數
                string userGUID = (string)Session["UserGUID"];

                //先取得檔案內容
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                string data = "";

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "";

                bool SMVPAAA003 = true;

                WebServerProject.auth.AUTHAgent authag = new WebServerProject.auth.AUTHAgent();
                authag.engine = engine;

                sql = "select * from SMVPAAA";
                DataSet qds = engine.getDataSet(sql, "TEMP");

                int auadmin = authag.getAuthFromAuthItem(qds.Tables[0].Rows[0]["SMVPAAA035"].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);

                if (auadmin == 0)
                {

                    sql = "select SMVPAAA003 from SMVPAAA";
                    DataSet dds = engine.getDataSet(sql, "TEMP");

                    if (dds.Tables[0].Rows[0]["SMVPAAA003"].ToString().Equals("N"))
                    {
                        SMVPAAA003 = false;
                    }
                }

                if (SMVPAAA003)
                {
                    sql = "select * from PANELLAYOUT where USERGUID='" + Utility.filter(userGUID) + "' AND LANGUAGEID = '" + Session["Locale"] + "' ";
                    DataSet ds = engine.getDataSet(sql, "TEMP");


                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        sql = "select LAYOUT from PANELLAYOUT where USERGUID='SYSTEMDEFAULT'  AND LANGUAGEID = '" + Session["Locale"] + "' ";
                        DataSet dds = engine.getDataSet(sql, "TEMP");
                        data = dds.Tables[0].Rows[0][0].ToString();

                        try
                        {
                            DataRow dr = ds.Tables[0].NewRow();
                            dr["GUID"] = IDProcessor.getID("");
                            dr["USERGUID"] = userGUID;
                            dr["LAYOUT"] = data;
                            dr["LANGUAGEID"] = Session["Locale"];
                            ds.Tables[0].Rows.Add(dr);

                            if (!engine.updateDataSet(ds))
                            {
                                engine.close();
                                throw new Exception(engine.errorString);
                            }
                        }
                        catch (Exception ue)
                        {
                            //System.IO.StreamWriter sw = new StreamWriter(@"D:\ASPNET平台\error.txt", false);
                            //sw.Write(ue.Message);
                            //sw.Close();
                        }

                    }
                    else
                    {
                        data = ds.Tables[0].Rows[0]["LAYOUT"].ToString();
                    }
                }
                else
                {
                    sql = "select LAYOUT from PANELLAYOUT where USERGUID='SYSTEMDEFAULT' AND LANGUAGEID = '" + Session["Locale"] + "' ";
                    DataSet dds = engine.getDataSet(sql, "TEMP");
                    data = dds.Tables[0].Rows[0][0].ToString();

                }
                engine.close();


                Response.Write(data);
            }
            catch (Exception xe)
            {
                Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string031", "伺服器處理發生錯誤, 請重新登入. 其他錯誤訊息: ") + xe.Message);
            }

%>
