using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.document;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using System.Net;
using System.ComponentModel;


public partial class SmpProgram_Form_SPSP001_File : BaseWebUI.DataListInlineForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SmpSampleForm";
        ApplicationID = "SMPFORM";
        ModuleID = "SPSP";

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(" function closeWindow(times) {");
                sb.Append("     if (times < 5) {");
                sb.Append("         if (!(window.closed)) {");
                sb.Append("             times++;");
                sb.Append("             window.close();");
                sb.Append("             var timeID = setTimeout(\"\", 1);");
                sb.Append("             clearTimeout(timeID);");
                sb.Append("             timeID = timer = setTimeout(\"closeWindow('\" + times + \"')\", 2000);");
                sb.Append("         }");
                sb.Append("     }");
                sb.Append(" }");
                sb.Append("closeWindow(1);");
                sb.Append("</script>");
                Type ctype = this.GetType();
                ClientScriptManager cm = Page.ClientScript;
                //System.Threading.Thread.Sleep(1000);
            }
        }
    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            //LocalFileAdapter fileAdapter = new LocalFileAdapter();
            //string tempFilePath = Server.MapPath("~/tempFolder");
            //tempFilePath = tempFilePath.Replace("\\", "\\\\");
            string fileName = objects.getData("FILENAME");
            string fileext = objects.getData("FILEEXT");
            if (!fileext.Equals(""))
            {
                fileName = fileName + "." + fileext;
            }
            //string tempFile = tempFilePath + "\\\\" + fileName;
            string level1 = "EKM";
            string level2 = objects.getData("SheetNo");
            string fileGUID = objects.getData("FileItemGUID");
            string destpath = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "filepath");
            destpath += level1 + "\\" + level2 + "\\";
            destpath += fileGUID;
            destpath = destpath.Replace("\\", "\\\\");
            //fileAdapter.getFile(tempFile, level1, level2, fileGUID);

            //Response.ClearHeaders();
            //Response.Clear();
            //Response.Expires = 0;
            //Response.Buffer = true;
            if (Request.Browser.Browser == "IE")
            {
                fileName = System.Web.HttpUtility.UrlPathEncode(fileName);
            }
            string strContentDisposition = String.Format("{0}; filename=\"{1}\"", "attachment", fileName);
            Response.AddHeader("Content-Disposition", strContentDisposition);
            Response.ContentType = "Application/octet-stream";
            Response.BinaryWrite(System.IO.File.ReadAllBytes(destpath));
            Response.End(); 

            //Response.ContentType = "application/x-zip-compressed";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            //Response.TransmitFile(destpath);
            //Close();

        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
    }

    protected override void saveData(DataObject objects)
    {
    }

    protected override void saveCustomData(DataObject objects)
    {
        base.saveCustomData(objects);
    }


    public void Close()
    {
        Page p = (Page)System.Web.HttpContext.Current.Handler;
        ClientScriptManager CSM = p.ClientScript;
        String ScriptName = "close";
        String ScriptMsg = "window.opener = null;window.open('','_self');window.close();";
        Type CsType = p.GetType();
        if (!CSM.IsStartupScriptRegistered(CsType, ScriptName))
        {
            CSM.RegisterStartupScript(CsType, ScriptName, ScriptMsg, true);
        }
    }

}