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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using com.dsc.kernal.factory;
using System.IO;
using com.dsc.kernal.utility;

public partial class Help_showProgramDescription : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string programID = Request.QueryString["ProgramID"];
        string engineType = (string)Session["engineType"];
        string connectString = (string)Session["connectString"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "select * from SMVAAAB left outer join SMSAAAA on SMVAAAB009=SMSAAAA002 where SMVAAAB002='" + Utility.filter(programID) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        engine.close();

        string programid = ds.Tables[0].Rows[0]["SMVAAAB002"].ToString();
        string authid = ds.Tables[0].Rows[0]["SMSAAAA002"].ToString();

        string xml = "<Program>";
        xml += "<ProgramID>" + splitHTML(ds.Tables[0].Rows[0]["SMVAAAB002"].ToString()) + "</ProgramID>";
        xml += "<ProgramName>" + splitHTML(com.dsc.locale.LocaleString.getMenuLocaleString(programid, ds.Tables[0].Rows[0]["SMVAAAB003"].ToString(), false)) + "</ProgramName>";
        xml += "<ProgramURL>" + splitHTML(ds.Tables[0].Rows[0]["SMVAAAB004"].ToString().Replace("&","&amp;")) + "</ProgramURL>";
        xml += "<ProgramTarget>" + splitHTML(com.dsc.locale.LocaleString.getOnlineHelpString(programid, "VB011", ds.Tables[0].Rows[0]["SMVAAAB011"].ToString())) + "</ProgramTarget>";
        xml += "<ProgramTime>" + splitHTML(com.dsc.locale.LocaleString.getOnlineHelpString(programid, "VB012", ds.Tables[0].Rows[0]["SMVAAAB012"].ToString())) + "</ProgramTime>";
        xml += "<ProgramOperate>" + splitHTML(com.dsc.locale.LocaleString.getOnlineHelpString(programid, "VB013", ds.Tables[0].Rows[0]["SMVAAAB013"].ToString())) + "</ProgramOperate>";
        xml += "<ProgramNotice>" + splitHTML(com.dsc.locale.LocaleString.getOnlineHelpString(programid, "VB014", ds.Tables[0].Rows[0]["SMVAAAB014"].ToString())) + "</ProgramNotice>";
        xml += "<ProgramFile>" + splitHTML(com.dsc.locale.LocaleString.getOnlineHelpString(programid, "VB010", ds.Tables[0].Rows[0]["SMVAAAB010"].ToString())) + "</ProgramFile>";
        xml += "<ProgramAuth>" + splitHTML(com.dsc.locale.LocaleString.getAuthItemString(authid, ds.Tables[0].Rows[0]["SMSAAAA003"].ToString())) + "(" + splitHTML(ds.Tables[0].Rows[0]["SMVAAAB009"].ToString()) + ")</ProgramAuth>";
        xml += "</Program>";

        StringReader sr = new StringReader(xml);
        StringWriter sw=new StringWriter();

        XPathDocument objDocument = new XPathDocument(sr);
        XPathNavigator objNav = objDocument.CreateNavigator();
        XslTransform objXslT = new XslTransform();

        try
        {
            string lFolder = "~/language/" + com.dsc.kernal.utility.Utility.getLocale();
            if (!System.IO.Directory.Exists(Server.MapPath(lFolder)))
            {
                objXslT.Load(Server.MapPath("~/Help/ProgramDescription.xsl"));
            }
            else
            {
                objXslT.Load(Server.MapPath("~/language/" + com.dsc.kernal.utility.Utility.getLocale() + "/helptemplate/ProgramDescription.xsl"));
            }
            objXslT.Transform(objNav, null, sw);

            Response.Write(sw.ToString());
        }
        catch (Exception te)
        {
            Response.Write("<script language=javascript>");
            Response.Write("alert('" + te.Message + "');");
            Response.Write("</script>");
        }
        sr.Close();
    }

    //移除HTML標籤 2009/5/18
    public static string splitHTML(string html) 
    {
        string split = html;
        //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"<[^>]+>|]+>");       
        //split = regex.Replace(split, "");
        split = split.Replace("<", "&lt;");
        split = split.Replace(">", "&gt;");
        split = split.Replace("\\n", System.Environment.NewLine);
        return split; 
    } 
}
