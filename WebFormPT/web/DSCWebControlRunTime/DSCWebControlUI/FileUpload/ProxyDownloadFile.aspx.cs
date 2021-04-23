using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DSCWebControlRunTime_DSCWebControlUI_FileUpload_ProxyDownloadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["OneTimeFilePathGUID"] != null) 
        {
            string OneTimeFilePathGUID = Request.QueryString["OneTimeFilePathGUID"];
            string OneTimeFileNameGUID = Request.QueryString["OneTimeFileNameGUID"];
            string FilePath = Session[OneTimeFilePathGUID].ToString();
            string FileName = Session[OneTimeFileNameGUID].ToString();
            Response.Clear();            
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);            
            FilePath = Page.ResolveClientUrl(FilePath);
            Response.WriteFile(FilePath);
            Response.End();        
        }        
    }    
}