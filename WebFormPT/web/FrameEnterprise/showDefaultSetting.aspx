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
			// 硂柑竚ㄏノ祘Α絏﹍て呼
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

                //╰参把计
                string workPath = Request.PhysicalApplicationPath;
                string clientIP = Request.UserHostAddress;

                //眔┮Τ把计
                string userGUID = (string)Session["UserGUID"];
                
                //眔郎ず甧
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
                Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string031", "狝竟矪瞶祇ネ岿粇, 叫穝祅. ㄤ岿粇癟: ") + xe.Message);
            }

%>
