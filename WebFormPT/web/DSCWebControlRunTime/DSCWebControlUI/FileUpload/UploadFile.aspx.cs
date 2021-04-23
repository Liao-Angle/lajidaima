//20090506 Modify Eric:HtmlEncode
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

public partial class DSCWebControlRunTime_DSCWebControlUI_FileUpload_UploadFile :BaseWebUI.GeneralWebPage
{
    private string splitter = "####";

    protected override void OnPreRenderComplete(EventArgs e)
    {
        ConfirmUpload.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "FileItem", "string27", "確認上傳");
        base.OnPreRenderComplete(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                setSession("tempFolder", com.dsc.kernal.utility.Utility.UrlDecode(Request.QueryString["tempFolder"]));
                setSession("maxLength", Request.QueryString["maxLength"]);
                this.ConfirmUpload.OnClientClick = "return window.dialogArguments.FileUploadBeforeOpenWindow('" + Request.QueryString["sources"] + "',document.getElementById('" + this.FUP.ClientID + "').value);";
                Description.onKeyDownClientScript = "var event=getEvent();if(event.keyCode==13) { event.keyCode=9;};";

                DSCLabel1.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "FileItem", "string25", "選擇上傳檔案");
                DSCLabel2.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "FileItem", "string26", "檔案說明");
            }
        }
    }

    protected void ConfirmUpload_Click(object sender, EventArgs e)
    {            
        if (FUP.FileName.Length==0)
        {                    
                MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "FileItem", "string24", "請選擇要上傳的檔案"));
                return;            
        }

        //如果禁止0KB檔案上傳；可將以下程式碼開放
        //if (FUP.FileBytes.Length == 0)
        //{
        //    MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "FileItem", "string25", "檔案大小0KB為無效檔案"));
        //    return;
        //}

        string tempFolder = (string)getSession("tempFolder");
        int maxLength = int.Parse((string)getSession("maxLength"));
        
        string fileGUID = com.dsc.kernal.utility.IDProcessor.getID("");
        //20100517 FISH 客製ISO檔案上傳名稱
        if (getSession(Request.QueryString["parentPageUID"],"FileGUID") != null)
        {
            fileGUID = getSession(Request.QueryString["parentPageUID"], "FileGUID").ToString();
            setSession(Request.QueryString["parentPageUID"], "FileGUID", null);
        }
        HttpPostedFile hf = Request.Files[0];
        
        if (maxLength != -1)
        {
            if (hf.ContentLength > maxLength)
            {
                MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "FileItem", "string28", "檔案大小超過限制"));
                return;
            }
        }
        string oldFileName = com.dsc.kernal.utility.Utility.getFileName(hf.FileName);
        string fileExt = com.dsc.kernal.utility.Utility.getExtName(hf.FileName);

        hf.SaveAs(tempFolder + "\\" + fileGUID + "." + fileExt);

        string filePath = fileGUID + "." + fileExt;
        string fileName = oldFileName;
        
        string rev = fileName + splitter + fileExt + splitter + filePath.Replace("\\", "/") + splitter + Description.ValueText;

        Response.Write("<script language='javascript'>");
        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");
        Response.Write("top.window.returnValue='" + Page.Server.HtmlEncode(rev) + "';");
                break;
            default:
                string js = "";
                js += "window.parent.parent.$('#ecpDialog').attr('ret', '" + Page.Server.HtmlEncode(rev) + "');";
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        }
        Response.Write("</script>");
    }
}
