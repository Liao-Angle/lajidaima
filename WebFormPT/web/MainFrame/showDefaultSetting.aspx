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

                string sql = "select LAYOUT from PANELLAYOUT where USERGUID='SYSTEMDEFAULT' and LANGUAGEID = '" + Session["Locale"] + "' ";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    data = "";
                }
                else
                {
                    data = ds.Tables[0].Rows[0][0].ToString();
                }
                engine.close();
                

                Response.Write(data);
            }
            catch (Exception xe)
            {
                Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string031", "伺服器處理發生錯誤, 請重新登入. 其他錯誤訊息: ") + xe.Message);
            }

%>
