using System;
using System.Collections.Generic;
using System.Data;
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

public partial class SmpProgram_Form_SPKM001_DownloadFile : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataObject objects = (DataObject)getSession((string)Session["UserID"], "DownFile");
        string fileName = Request.QueryString["FILENAME"];
        string fileExt = Request.QueryString["FILEEXT"];
        string level1 = "EKM";
        string level2 = Request.QueryString["SheetNo"];
        string fileGUID = Request.QueryString["FileItemGUID"];
        //if (objects != null)
        //{
        //    fileName = objects.getData("FILENAME");
        //    fileExt = objects.getData("FILEEXT");
        //    if (!fileExt.Equals(""))
        //    {
        //        fileName = fileName + "." + fileExt;
        //    }
        //    level2 = objects.getData("SheetNo");
        //    fileGUID = objects.getData("FileItemGUID");
        //}
        //else
        //{
            fileName = fileName + "." + fileExt;
        //}
        string destpath = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "filepath");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "SELECT LEVEL1, LEVEL2 FROM FILEITEM where GUID='" + fileGUID + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            level1 = ds.Tables[0].Rows[0][0].ToString();
            level2 = ds.Tables[0].Rows[0][1].ToString();
            destpath += level1 + "\\" + level2 + "\\";
            destpath += fileGUID;
            destpath = destpath.Replace("\\", "\\\\");
            if (Request.Browser.Browser == "IE")
            {
                fileName = System.Web.HttpUtility.UrlPathEncode(fileName);
            }
            if (File.Exists(destpath))
            {
                string strContentDisposition = String.Format("{0}; filename=\"{1}\"", "attachment", fileName);
                Response.AddHeader("Content-Disposition", strContentDisposition);
                Response.ContentType = "Application/octet-stream";
                Response.BinaryWrite(System.IO.File.ReadAllBytes(destpath));
                Response.End();
            }
            else
            {
                MessageBox("檔案不存在!");
            }
        }
        engine.close();
    }
}