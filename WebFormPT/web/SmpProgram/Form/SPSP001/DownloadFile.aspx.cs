using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using com.dsc.kernal.utility;
using com.dsc.kernal.document;

public partial class SmpProgram_Form_SPSP001_DownloadFile : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataObject objects = (DataObject)getSession((string)Session["UserID"], "DownFile");
        string fileName = objects.getData("FILENAME");
        string fileext = objects.getData("FILEEXT");
        if (!fileext.Equals(""))
        {
            fileName = fileName + "." + fileext;
        }
        string level1 = "EKM";
        string level2 = objects.getData("SheetNo");
        string fileGUID = objects.getData("FileItemGUID");
        string destpath = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "filepath");
        destpath += level1 + "\\" + level2 + "\\";
        destpath += fileGUID;
        destpath = destpath.Replace("\\", "\\\\");
        if (Request.Browser.Browser == "IE")
        {
            fileName = System.Web.HttpUtility.UrlPathEncode(fileName);
        }
        string strContentDisposition = String.Format("{0}; filename=\"{1}\"", "attachment", fileName);
        Response.AddHeader("Content-Disposition", strContentDisposition);
        Response.ContentType = "Application/octet-stream";
        Response.BinaryWrite(System.IO.File.ReadAllBytes(destpath));
        Response.End(); 
    }
}