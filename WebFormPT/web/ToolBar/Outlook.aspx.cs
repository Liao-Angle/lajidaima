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
using System.Web.Services;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Xml;
using System.Text;

public partial class ToolBar_Outlook : System.Web.UI.Page
{
    private string lsplitter = "#!#!#";
    private string splitter = "#*#*#";

    public string outlookStyle = "Outlook.css";
    public string isShowSearch = "Y";
	//CL_Chang
	public WebServerProject.SysParam spx = null;
    public com.dsc.kernal.webservice.WSDLClient WC = null;
    public string inboxcount = "0";
    public string efinboxcount = "0";
    public int totalcount = 0;
    public bool getCount = false;
    public com.dsc.kernal.factory.AbstractEngine sp7engine = null;
	
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        for (int i = 0; i < Page.Header.Controls.Count; i++)
        {
            if (Page.Header.Controls[i].GetType().Name.Equals("HtmlLink"))
            {
                System.Web.UI.HtmlControls.HtmlLink hl = (System.Web.UI.HtmlControls.HtmlLink)Page.Header.Controls[i];
                //hl.Href = Page.ResolveClientUrl("~/Outlook/" + css);
                hl.Href = GlobalProperty.getProperty("layout", (string)Session["layoutType"]).Split(new char[] { ';' })[3];
            }
        }

        LiteralControl lc = new LiteralControl();
        lc.Text = "<script language=javascript src='" + Page.ResolveUrl("~/DSCWebControlRunTime/DSCWebControlUI/PageSecure.js") + "'></script>";
        Page.Form.Controls.Add(lc);

        string connectString = Convert.ToString(Session["connectString"]);
        string engineType = Convert.ToString(Session["engineType"]);
        int isProtectedPage = -1;
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select SMVPAAA005 from SMVPAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        isProtectedPage = int.Parse(ds.Tables[0].Rows[0][0].ToString());

        engine.close();

        System.Web.UI.LiteralControl lc1 = new System.Web.UI.LiteralControl();
        lc1.Text = "<script language='javascript'>ScreenProtectPage(" + Convert.ToString(isProtectedPage) + ");</script>";
        this.Controls.AddAt(this.Controls.Count, lc1);
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
        if (!IsPostBack)
        {
            string Method = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["Method"]);
            if (Method == null)
            {
                Method = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["Method"]);
            }

            string isSystem = "";
            isSystem = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["ShowSys"]);
            if (isSystem == null)
            {
                isSystem = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["ShowSys"]);
            }
            if (isSystem == null)
            {
                if (Session["ShowSys"] != null)
                {
                    isSystem = (string)Session["ShowSys"];
                }
            }
            else
            {
                Session["ShowSys"] = isSystem;
            }

            if (Method != null)
            {
                string data = "";
                StringBuilder sbData = new StringBuilder();
                if (Method.Equals("GetToolBar"))
                {
                    string connectString = (string)Session["connectString"];
                    string engineType = (string)Session["engineType"];

                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = factory.getEngine(engineType, connectString);

                    string sql = "select * from SMVAAAA where SMVAAAA002=''";
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

                    sql = "select * from SMVAAAA ";
                    if (isSystem.Equals("1"))
                    {
                        sql += " where SMVAAAA007='SYSTEMDEFAULT'";
                    }
                    else
                    {
                        sql += " where SMVAAAA007='" + Utility.filter((string)Session["UserGUID"]) + "'";
                    }
                    sql += " order by SMVAAAA008 asc";
                    DataSet dsa = engine.getDataSet(sql, "TEMP");

                    sql = "select SMVAAAB003, SMVAAAB004, SMVAAAB005, SMVAAAB006, SMVAAAB007, SMVAAAB008, SMVAAAB002, SMVAAAA002,SMVAAAB011 from SMVAAAA inner join SMVAAAB on SMVAAAA006=SMVAAAB001  order by SMVAAAA008 asc";
                    DataSet dsb = engine.getDataSet(sql, "TEMP");

                    ArrayList tempAry = new ArrayList();
                    for (int x = 0; x < dsb.Tables[0].Rows.Count; x++)
                    {
                        if (!tempAry.Contains(dsb.Tables[0].Rows[x]["SMVAAAB002"].ToString()))
                        {
                            tempAry.Add(dsb.Tables[0].Rows[x]["SMVAAAB002"].ToString());
                        }
                    }
                    string[] programids = (string[])tempAry.ToArray(typeof(string));

                    WebServerProject.auth.AUTHAgent authagent = new WebServerProject.auth.AUTHAgent();
                    authagent.engine = engine;

                    Hashtable allauth = authagent.getAllProgramAuth(programids, (string)Session["UserID"], (string[])Session["usergroup"]);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string GUID = ds.Tables[0].Rows[i]["SMVAAAA001"].ToString();

                            string zd = "";
                            StringBuilder sbZD = new StringBuilder();
                            sbZD.Append("<Root>");

                            queryXml(engine, GUID, authagent, dsa, dsb, allauth, ref sbZD);

                            sbZD.Append("</Root>");

                            //這裡要由底層計算各Folder節點的Item數量
                            XMLProcessor xp = new XMLProcessor(sbZD.ToString(), 1);
                            int count = sumChild(xp.selectSingleNode(@"Root"));

                            if (count > 0)
                            {
                                count = sumFOpen(xp.selectSingleNode(@"Root"));

                                sbData.Append(ds.Tables[0].Rows[i]["SMVAAAA001"].ToString() + splitter);
                                sbData.Append(com.dsc.locale.LocaleString.getMenuLocaleString(ds.Tables[0].Rows[i]["SMVAAAA003"].ToString(), ds.Tables[0].Rows[i]["SMVAAAA004"].ToString(), true) + splitter);
                                sbData.Append(ds.Tables[0].Rows[i]["SMVAAAA005"].ToString() + splitter);
                                if (count > 0)
                                {
                                    sbData.Append("Y" + lsplitter);
                                }
                                else
                                {
                                    sbData.Append("N" + lsplitter);
                                }
                            }
                        }
                        if (sbData.Length > 0)
                        {
                            string orgString = sbData.ToString();
                            int endPoint = orgString.Length - lsplitter.Length;                            
                            sbData.Length = 0;
                            sbData.Append(orgString.Substring(0, endPoint));
                        }
                    }
                    else
                    {
                        sbData.Append( "x" + splitter);
                        sbData.Append( com.dsc.locale.LocaleString.getMainFrameLocaleString("Outlook.aspx.language.ini", "global", "string004", "無設定選單內容") + splitter);
                        sbData.Append( "");
                    }
                    engine.close();
                    Response.Write(sbData.ToString());
                    Response.End();
                }
                else if (Method.Equals("GetSearchBar"))
                {
                    sbData.Append( "" + splitter);
                    sbData.Append( com.dsc.locale.LocaleString.getMainFrameLocaleString("Outlook.aspx.language.ini", "global", "string005", "搜尋結果") + splitter);
                    sbData.Append( "" + splitter + "Y");
                    Response.Write(sbData.ToString());
                    Response.End();
                }
                else if (Method.Equals("GetToolSearchTree"))
                {
                    string connectString = (string)Session["connectString"];
                    string engineType = (string)Session["engineType"];

                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = factory.getEngine(engineType, connectString);

                    WebServerProject.auth.AUTHAgent authagent = new WebServerProject.auth.AUTHAgent();
                    authagent.engine = engine;


                    sbData.Append( "<Root>");

                    string sql = "select SMVAAAB003, SMVAAAB004, SMVAAAB005, SMVAAAB006, SMVAAAB007, SMVAAAB008, SMVAAAB002,SMVAAAB011 from SMVAAAA inner join SMVAAAB on SMVAAAA006=SMVAAAB001 where SMVAAAB003 like '%" + Utility.filter(Request.Form["SV"]) + "%'";
                    if (isSystem.Equals("1"))
                    {
                        sql += " and SMVAAAA007='SYSTEMDEFAULT'";
                    }
                    else
                    {
                        sql += " and SMVAAAA007='" + Utility.filter((string)Session["UserGUID"]) + "'";
                    }
                    DataSet ds = engine.getDataSet(sql, "TEP");

                    ArrayList tempAry = new ArrayList();
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        if (!tempAry.Contains(ds.Tables[0].Rows[x]["SMVAAAB002"].ToString()))
                        {
                            tempAry.Add(ds.Tables[0].Rows[x]["SMVAAAB002"].ToString());
                        }
                    }
                    string[] programids = (string[])tempAry.ToArray(typeof(string));
                    Hashtable allauth = authagent.getAllProgramAuth(programids, (string)Session["UserID"], (string[])Session["usergroup"]);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //int auth = authagent.getAuth(ds.Tables[0].Rows[i][6].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);
                        int auth = getAuth(allauth, ds.Tables[0].Rows[i][6].ToString());

                        if (auth > 0)
                        {
                            sbData.Append( "<Item title='" + filterXMLChar(com.dsc.locale.LocaleString.getMenuLocaleString(ds.Tables[0].Rows[i][6].ToString(), ds.Tables[0].Rows[i][0].ToString(), false)) + "' ");
                            sbData.Append( "url='" + HttpUtility.HtmlEncode(ds.Tables[0].Rows[i][1].ToString()) + "' ");
                            sbData.Append( "width='" + ds.Tables[0].Rows[i][2].ToString() + "' ");
                            sbData.Append( "height='" + ds.Tables[0].Rows[i][3].ToString() + "' ");
                            if (ds.Tables[0].Rows[i][4].ToString().Equals("Y"))
                            {
                                sbData.Append( "isMax='1' ");
                            }
                            else
                            {
                                sbData.Append( "isMax='0' ");
                            }
                            if (ds.Tables[0].Rows[i][5].ToString().Equals("N"))
                            {
                                //sbData.Append( "frameType='scrolling=no' ");
                                sbData.Append( "frameType='0' ");
                            }
                            else
                            {
                                //sbData.Append( "frameType='' ");
                                sbData.Append( "frameType='1' ");
                            }
                            sbData.Append( "explain='" + filterXMLChar(ds.Tables[0].Rows[i]["SMVAAAB011"].ToString()) + "' ");
                            sbData.Append( "></Item>");
                        }
                    }
                    sbData.Append( "</Root>");

                    engine.close();
                    Response.Write(sbData.ToString());
                    Response.End();
                }
                else if (Method.Equals("GetToolTree"))
                {                    

                    string GUID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.Form["GUID"]);

                    string connectString = (string)Session["connectString"];
                    string engineType = (string)Session["engineType"];

                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = factory.getEngine(engineType, connectString);

                    WebServerProject.auth.AUTHAgent authagent = new WebServerProject.auth.AUTHAgent();
                    authagent.engine = engine;

                    string sql = "select * from SMVAAAA";
                    sql += " order by SMVAAAA008 asc";
                    DataSet ds = engine.getDataSet(sql, "TEMP");

                    sql = "select SMVAAAB003, SMVAAAB004, SMVAAAB005, SMVAAAB006, SMVAAAB007, SMVAAAB008, SMVAAAB002, SMVAAAA002,SMVAAAB011 from SMVAAAA inner join SMVAAAB on SMVAAAA006=SMVAAAB001  order by SMVAAAA008 asc";
                    DataSet dsb = engine.getDataSet(sql, "TEMP");

                    ArrayList tempAry = new ArrayList();
                    for (int x = 0; x < dsb.Tables[0].Rows.Count; x++)
                    {
                        if (!tempAry.Contains(dsb.Tables[0].Rows[x]["SMVAAAB002"].ToString()))
                        {
                            tempAry.Add(dsb.Tables[0].Rows[x]["SMVAAAB002"].ToString());
                        }
                    }
                    string[] programids = (string[])tempAry.ToArray(typeof(string));
                    Hashtable allauth = authagent.getAllProgramAuth(programids, (string)Session["UserID"], (string[])Session["usergroup"]);

                    sbData.Append( "<Root>");

                    queryXml(engine, GUID, authagent, ds, dsb, allauth, ref sbData);

                    sbData.Append( "</Root>");

                    engine.close();

                    //這裡要由底層計算各Folder節點的Item數量
                    XMLProcessor xp = new XMLProcessor(sbData.ToString(), 1);
                    int count = sumChild(xp.selectSingleNode(@"Root"));

                    //這裡要由底層計算各Folder是否需要展開
                    count = sumFOpen(xp.selectSingleNode(@"Root"));



                    Response.Write(xp.getXmlString());
                    Response.End();
                }
            }
            else
            {
                //國昌20100614-mantis0017434
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "select SMVPAAA040 from SMVPAAA";
                DataSet tds = engine.getDataSet(sql, "TEMP");
                if (tds.Tables[0].Rows[0][0].ToString().Equals("Y"))
                {
                    isShowSearch = "Y";
                }
                else
                {
                    isShowSearch = "N";
                }
                engine.close();
            }
        }
    }
    private int sumChild(XmlNode xn)
    {
        int sum = 0;
        if ((xn.Name.Equals("Folder")) || (xn.Name.Equals("Root")))
        {
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                sum += sumChild(xn.ChildNodes[i]);
            }
            XmlAttribute dbsum = xn.OwnerDocument.CreateAttribute("SUM");
            dbsum.Value = sum.ToString();
            xn.Attributes.Append(dbsum);
        }
        else
        {
            sum = 1;
        }
        return sum;
    }
    private int sumFOpen(XmlNode xn)
    {
        int sum = 0;
        int xsum = 0;
        if ((xn.Name.Equals("Folder")) || (xn.Name.Equals("Root")))
        {
            for (int i = 0; i < xn.ChildNodes.Count; i++)
            {
                sum += sumFOpen(xn.ChildNodes[i]);
            }
            if (xn.Name.Equals("Folder"))
            {
                xsum = int.Parse(xn.Attributes["FOPEN"].Value);
                xsum += sum;
                xn.Attributes["FOPEN"].Value = xsum.ToString();
            }
            else
            {
                xsum = sum;
            }
        }
        return xsum;
    }

    private void queryXml(AbstractEngine engine, string parentGUID, WebServerProject.auth.AUTHAgent authagent, DataSet dsa, DataSet dsb, Hashtable hs, ref StringBuilder sbData)
    {
        string sql;
        //sql = "select SMVAAAA001, SMVAAAA004, SMVAAAA003, SMVAAAA009 from SMVAAAA where SMVAAAA002='" + Utility.filter(parentGUID) + "' and SMVAAAA006='' order by SMVAAAA008 asc";
        //DataSet ds = engine.getDataSet(sql, "TEMP");
        DataRow[] dra = dsa.Tables[0].Select(" SMVAAAA002='" + Utility.filter(parentGUID) + "' and SMVAAAA006='' ");

        for (int i = 0; i < dra.Length; i++)
        {
            string DIS = "0";
            if (dra[i]["SMVAAAA009"].ToString().Equals("N"))
            {
                DIS = "0";
            }
            else
            {
                DIS = "1";
            }
            sbData.Append( "<Folder title='" + filterXMLChar(com.dsc.locale.LocaleString.getMenuLocaleString(dra[i]["SMVAAAA003"].ToString(), dra[i]["SMVAAAA004"].ToString(), true)) + "' FOPEN='"+DIS+"'>");

            queryXml(engine, dra[i]["SMVAAAA001"].ToString(), authagent, dsa, dsb, hs, ref sbData);

            sbData.Append( "</Folder>");
        }

        //sql = "select SMVAAAB003, SMVAAAB004, SMVAAAB005, SMVAAAB006, SMVAAAB007, SMVAAAB008, SMVAAAB002 from SMVAAAA inner join SMVAAAB on SMVAAAA006=SMVAAAB001 where SMVAAAA002='" + Utility.filter(parentGUID) + "' order by SMVAAAA008 asc";
        //ds = engine.getDataSet(sql, "TEP");
        DataRow[] drb = dsb.Tables[0].Select("SMVAAAA002='" + Utility.filter(parentGUID) + "'");
        for (int i = 0; i < drb.Length; i++)
        {
            //int auth = authagent.getAuth(drb[i][6].ToString(), (string)Session["UserID"], (string[])Session["usergroup"]);
            int auth = getAuth(hs, drb[i][6].ToString());
            if (auth > 0)
            {
                if (filterXMLChar(com.dsc.locale.LocaleString.getMenuLocaleString(drb[i][6].ToString(), drb[i][0].ToString(), false)) == "收件資料匣")
                {
                    try
                    {
                        //sql = "select count(*) from UserWorkAssignment where userId='" + Convert.ToString(Session["UserID"]) + "'";
                        sql = "select count(*) from UserWorkAssignment, SMWBAAA where userId='" + Convert.ToString(Session["UserID"]) + "' and processDefinitionId=SMWBAAA003 and ISNULL(SMWBAAA901,'') = ''";
                        sql += " and not exists (select 'x' from QUEUEWAITING where KEYS=workItemOID)";
                        inboxcount = Convert.ToString(engine.executeScalar(sql));
                        getCount = true;
                    }
                    catch (Exception e)
                    {

                    }
                    sbData.Append("<Item title='" + filterXMLChar(com.dsc.locale.LocaleString.getMenuLocaleString(drb[i][6].ToString(), drb[i][0].ToString(), false)) + " (");
                    if (getCount)
                    {
                        sbData.Append(inboxcount + ")' ");
                    }
                    else
                    {
                        sbData.Append("0)' ");
                    }
                } 
				else if (filterXMLChar(com.dsc.locale.LocaleString.getMenuLocaleString(drb[i][6].ToString(), drb[i][0].ToString(), false)) == "收件資料匣(ERP)")
                {
					try
                    {
                        sql = "select count(*) from UserWorkAssignment, SMWBAAA where userId='" + Convert.ToString(Session["UserID"]) + "' and processDefinitionId=SMWBAAA003 and ISNULL(SMWBAAA901,'') = 'Y'";
                        sql += " and not exists (select 'x' from QUEUEWAITING where KEYS=workItemOID)";
						inboxcount = Convert.ToString(engine.executeScalar(sql));
                        getCount = true;
                    }
                    catch (Exception e)
                    {

                    }
                    sbData.Append("<Item title='" + filterXMLChar(com.dsc.locale.LocaleString.getMenuLocaleString(drb[i][6].ToString(), drb[i][0].ToString(), false)) + " (");
                    if (getCount)
                    {
                        sbData.Append(inboxcount + ")' ");
                    }
                    else
                    {
                        sbData.Append("0)' ");
                    }
				} 
				else {
					sbData.Append( "<Item title='" + filterXMLChar(com.dsc.locale.LocaleString.getMenuLocaleString(drb[i][6].ToString(), drb[i][0].ToString(), false)) + "' ");
                }
				sbData.Append( "url='" + HttpUtility.HtmlEncode(drb[i][1].ToString()) + "' ");                
                sbData.Append( "width='" + drb[i][2].ToString() + "' ");
                sbData.Append( "height='" + drb[i][3].ToString() + "' ");
                if (drb[i][4].ToString().Equals("Y"))
                {
                    sbData.Append( "isMax='1' ");
                }
                else
                {
                    sbData.Append( "isMax='0' ");
                }
                if (drb[i][5].ToString().Equals("N"))
                {
                    sbData.Append( "frameType='0' ");
                    //sbData.Append( "frameType='scrolling=no' ");
                }
                else
                {
                    //sbData.Append( "frameType='' ");
                    sbData.Append( "frameType='1' ");
                }
                sbData.Append( "explain='" + filterXMLChar(drb[i]["SMVAAAB011"].ToString()) +"' ");
                sbData.Append( "></Item>");
            }
        }
    }
    private int getAuth(Hashtable hs, string programid)
    {
        object obj = hs[programid];
        if (obj == null)
        {
            return 0;
        }
        else
        {
            return (int)obj;
        }
    }
	
	[WebMethod]
    public static string ProcessInboxEfCount()
    {
        string efinboxcount = "?";
        com.dsc.kernal.factory.AbstractEngine sp7engine = null;
        AbstractEngine engine1 = null;
        try
        {
            string connectString = (string)HttpContext.Current.Session["connectString"];
            string engineType = (string)HttpContext.Current.Session["engineType"];

            IOFactory factory = new IOFactory();
            engine1 = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine1);//
            sp7engine = factory.getEngine(engineType, sp.getParam("EF2KWebDB"));//
            efinboxcount = Convert.ToString(sp7engine.executeScalar("select count(resdd001) from resdd where resdd019='Y' and resdd015=1 and (resdd007=(select resak001 from resak where resak001 ='" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "') or resdd020=(select resak001 from resak where resak001 = '" + Convert.ToString(HttpContext.Current.Session["UserID"]) + "'))"));
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (engine1 != null) engine1.close();
            if (sp7engine != null) sp7engine.close();
        }
        return efinboxcount;
    }

    [WebMethod]
    public static string ProcessInboxCount()
    {
        string inboxcount = "?";
        //com.dsc.kernal.webservice.WSDLClient WC = null;
        //WebServerProject.SysParam spx = null;
        AbstractEngine engine1 = null;
        try
        {
            string connectString = (string)HttpContext.Current.Session["connectString"];
            string engineType = (string)HttpContext.Current.Session["engineType"];
            string userId = Convert.ToString(HttpContext.Current.Session["UserID"]);
            IOFactory factory = new IOFactory();
            engine1 = factory.getEngine(engineType, connectString);
            //string sql = "select count(*) from UserWorkAssignment where userId='" + userId + "'";
			string sql = "select count(*) from UserWorkAssignment, SMWBAAA where userId='" + userId + "' and processDefinitionId=SMWBAAA003 and ISNULL(SMWBAAA901,'') = ''";
            sql += " and not exists (select 'x' from QUEUEWAITING where KEYS=workItemOID)";
            inboxcount = Convert.ToString(engine1.executeScalar(sql));

            //spx = new WebServerProject.SysParam(engine1);
            //string WorkflowService = spx.getParam("NaNaWebService");
            //WC = new com.dsc.kernal.webservice.WSDLClient(WorkflowService);
            //WC.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
            //WC.build(true);
            //inboxcount = Convert.ToString(WC.callSync("fetchWorkItemCount", Convert.ToString(HttpContext.Current.Session["UserID"]), 0, "ALL"));
        }
        catch (Exception e)
        {

        }
        finally
        {
            if (engine1 != null) engine1.close();
        }
        return inboxcount;
    }
	
	[WebMethod]
    public static string ProcessInboxErpCount()
    {
        string inboxcount = "?";
        AbstractEngine engine1 = null;
        try
        {
            string connectString = (string)HttpContext.Current.Session["connectString"];
            string engineType = (string)HttpContext.Current.Session["engineType"];
            string userId = Convert.ToString(HttpContext.Current.Session["UserID"]);
            IOFactory factory = new IOFactory();
            engine1 = factory.getEngine(engineType, connectString);
			string sql = "select count(*) from UserWorkAssignment, SMWBAAA where userId='" + userId + "' and processDefinitionId=SMWBAAA003 and ISNULL(SMWBAAA901,'') = 'Y'";
            sql += " and not exists (select 'x' from QUEUEWAITING where KEYS=workItemOID)";
            inboxcount = Convert.ToString(engine1.executeScalar(sql));
		}
        catch (Exception e)
        {

        }
        finally
        {
            if (engine1 != null) engine1.close();
        }
        return inboxcount;
    }
}
