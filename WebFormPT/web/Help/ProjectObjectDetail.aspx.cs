﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.factory;
using com.dsc.kernal.databean;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.Remoting;
using WebServerProject;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

public partial class Help_ProjectObjectDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string objectName = Request.QueryString["objectName"];
        string[] pts = objectName.Split(new char[] { '.' });
        string orifp = "~/settingFiles/DataObjectFolder/";
        for (int i = 0; i < (pts.Length - 1); i++)
        {
            orifp += pts[i] + "/";
        }
        orifp += objectName + ".xml";
        string fp = Server.MapPath(orifp);
        StreamReader sr2 = new StreamReader(fp);
        string xml = sr2.ReadToEnd();
        sr2.Close();


        DataObject doo = new DataObject();
        doo.loadSchema(xml);

        DataObjectFactory dof = new DataObjectFactory();
        doo.loadLanguage();
        xml = dof.generalXML(doo);

        //Response.Write(xml);
        StringReader sr = new StringReader(xml);
        StringWriter sw = new StringWriter();

        XPathDocument objDocument = new XPathDocument(sr);
        XPathNavigator objNav = objDocument.CreateNavigator();
        XslTransform objXslT = new XslTransform();

        try
        {
            string lFolder = "~/language/" + com.dsc.kernal.utility.Utility.getLocale();
            if (!System.IO.Directory.Exists(Server.MapPath(lFolder)))
            {
                objXslT.Load(Server.MapPath("~/Help/ProjectObjectDescription.xsl"));
            }
            else
            {
                objXslT.Load(Server.MapPath("~/language/" + com.dsc.kernal.utility.Utility.getLocale() + "/helptemplate/ProjectObjectDescription.xsl"));
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
}
