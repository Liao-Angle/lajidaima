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
			// �b�o�̩�m�ϥΪ̵{���X�H��l�ƺ���
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

                //�t�ΰѼ�
                string workPath = Request.PhysicalApplicationPath;
                string clientIP = Request.UserHostAddress;

                //���o�Ҧ��Ѽ�
                string data = ncq["DATA"];
                if (data == null)
                {
                    data = ncs["DATA"];
                }
                string userGUID = (string)Session["UserGUID"];

                //���s���ɮ�
                string errMsg="";
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "select * from PANELLAYOUT where USERGUID='SYSTEMDEFAULT' AND LANGUAGEID = '" + Session["Locale"] + "' ";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["GUID"] = IDProcessor.getID("");
                    dr["USERGUID"] = "SYSTEMDEFAULT";
                    dr["LAYOUT"] = data;
                    dr["LANGUAGEID"] = Session["Locale"];
                    ds.Tables[0].Rows.Add(dr);

                    if (!engine.updateDataSet(ds))
                    {
                        errMsg = engine.errorString;
                    }
                }
                else
                {
                    ds.Tables[0].Rows[0]["LAYOUT"] = data;
                    if (!engine.updateDataSet(ds))
                    {
                        errMsg = engine.errorString;
                    }
                }
                engine.close();

                if (errMsg.Equals(""))
                {
                    Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string029", "�x�s���\"));
                }
                else
                {
                    Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string030", "�x�s�ɵo�Ϳ��~. ���~�T����: ") + errMsg);
                }
            }
            catch (Exception xe)
            {
                Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string031", "���A���B�z�o�Ϳ��~, �Э��s�n�J. ��L���~�T��: ") + xe.Message);
            }

%>
