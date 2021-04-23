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

public partial class Login : LoginPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["connectString"] == null)
        {
            Response.Write("<script language=javascript>");
            Response.Write("alert('�t�Τw��s�άO�z�w���m�Ӥ[, �Э��s�n�J');");
            Response.Write("window.location.href='Default.aspx';");
            Response.Write("</script>");
            Response.End();
        }
        if (!IsPostBack)
        {
	    Session["isAD"] = true;
            if (Convert.ToBoolean(Session["isAD"]) == false)
            {
               
                Response.Write("<script language=javascript>");
                if (Request.ServerVariables["AUTH_USER"] != null || !string.IsNullOrEmpty(Convert.ToString(Request.ServerVariables["AUTH_USER"])))
                {
		    Session["isAD"] = true;
		    initPage();
		    Session["isAD"] = false;
                }
		else
		{
		    Session["isAD"] = false;
		    initPage();
		    Session["isAD"] = true;
		}
                Response.Write("</script>");                
            }
            else
            {
		if (Request.ServerVariables["AUTH_USER"] != null || !string.IsNullOrEmpty(Convert.ToString(Request.ServerVariables["AUTH_USER"])))
                {
                    Session["isAD"] = false;
		}
                initPage();
            }
            //�]chrome�Bsafari�Popera�|���䴩�[�J�̷R�y�k�A�]�������ӥ\��
            com.dsc.kernal.utility.BrowserProcessor.BrowserType type = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
            if (!type.Equals(com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE) && !type.Equals(com.dsc.kernal.utility.BrowserProcessor.BrowserType.FireFox))
            {
                Button2.Visible = false;
            }
        }
    }
    protected void ActiveX_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("ActiveX.aspx");
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        String LogonUser = UID.Text;
        String Password = PWD.Text;
        String languageType = LANGUAGE.SelectedValue;
        String layoutType = LAYOUT.SelectedValue;

        login("", LogonUser, Password, languageType, layoutType);
    }
}
