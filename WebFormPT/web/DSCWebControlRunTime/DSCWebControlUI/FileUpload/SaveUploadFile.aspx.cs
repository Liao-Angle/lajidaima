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
using com.dsc.kernal.utility;
using System.IO;

public partial class DSCWebControlRunTime_DSCWebControlUI_FileUpload_SaveUploadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string tempPath = Server.MapPath("~/tempFolder/") + "/";
        string fileID = IDProcessor.getID("");

        string sFile = tempPath + fileID;

        byte[] data = Request.BinaryRead(Request.ContentLength);

        FileStream sw = new FileStream(sFile, FileMode.Create);
        sw.Write(data, 0, data.Length);
        sw.Close();

        Response.Write(fileID);
    }
}
