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

public partial class Help_DefaultObjectDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string objectName = Request.QueryString["objectName"];

        DataObject doo = (DataObject)com.dsc.kernal.utility.Utility.getClasses(objectName.Split(new char[] { '.' })[0], objectName);

        DataObjectFactory dof=new DataObjectFactory();
        doo.loadLanguage();
        string xml = dof.generalXML(doo);

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
                objXslT.Load(Server.MapPath("~/Help/DefaultObjectDescription.xsl"));
            }
            else
            {
                objXslT.Load(Server.MapPath("~/language/"+com.dsc.kernal.utility.Utility.getLocale()+"/helptemplate/DefaultObjectDescription.xsl"));
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
