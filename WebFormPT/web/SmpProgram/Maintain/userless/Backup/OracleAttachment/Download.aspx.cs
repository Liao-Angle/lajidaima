using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using WebServerProject.auth;
using System.Data;

public partial class SmpProgram_Maintain_OracleAttachment_Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty((string)Session["UserID"]))
                Response.Redirect("~/NoAuth.aspx");
            //程式權限判斷
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            //connectString = "USER=sa;PWD=ecp13adm;SERVER=192.168.2.227;DATABASE=WebFormPT";
            //engineType = EngineConstants.SQL;
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            AUTHAgent authagent = new AUTHAgent();
            authagent.engine = engine;
            int auth = authagent.getAuth("OracleAttachments", (string)Session["UserID"], (string[])Session["usergroup"]);
            //int auth = 0;
            if (auth == 0)
                Response.Redirect("~/NoAuth.aspx");
            string msg = null;
            decimal? headerID = Convert.ToDecimal(Request["Header_Id"]);
            decimal? fileID = Convert.ToDecimal(Request["FileId"]);
            decimal? key = Convert.ToDecimal(Request["Key"]);
            string kind = (string)Request["Kind"];
            string empNum = (string)Session["UserID"];

            //decimal? headerID = 602011;//588442;
            //decimal? fileID = 4521098;//4342439;
            //decimal? key = 598860002;//123;
            //string kind = "STANDARD";//"BLANKET";
            //string empNum = "3625";
            
            byte[] fileContent = null;
            string mimeType = "";
            string fileName = "";
            AbstractEngine engineOra = getErpPortalEngine(engine);
            //附件KEY判斷
            msg = chkAttachKey(engineOra, Convert.ToDecimal(key), Convert.ToDecimal(fileID));
            //附件權限判斷
            if ((kind == "BLANKET") || (kind == "STANDARD") || (kind == "RFQ") ||
                (kind == "QUOTATION") || (kind == "AP"))
            {
                if (string.IsNullOrEmpty(msg))
                    msg = chkHierarchy(engineOra, Convert.ToDecimal(headerID), kind, empNum);
                else
                    msg = msg + "  " + chkHierarchy(engineOra, Convert.ToDecimal(headerID), kind, empNum);
            }
            if (string.IsNullOrEmpty(msg))
            {
                msg = "附件下載中!...";
                string sql = "	SELECT * " +
                             "    FROM SMP_FND_LOBS " +
                             "   WHERE FILE_ID = '" + fileID + "'";
                DataSet ds = engineOra.getDataSet(sql, "TEMP");
                int rows = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rows; i++)
                {
                    mimeType = ds.Tables[0].Rows[i][2].ToString();
                    fileName = Server.UrlPathEncode(ds.Tables[0].Rows[i][1].ToString());
                    fileContent = (byte[])ds.Tables[0].Rows[i][3];
                    Response.AppendHeader("content-length", fileContent.Length.ToString());
                    Response.ContentType = mimeType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(fileContent);
                    Response.Flush();
                    Response.End();
                    msg = "附件下載完成!";
                }
            }
            engine.close();
            engineOra.close();
            this.Msg_lab.Text = msg;
        }
    }

    protected AbstractEngine getErpPortalEngine(AbstractEngine engine)
    {
        AbstractEngine engineErpPortal = null;
        try
        {
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam("ERPPortalDB");
            IOFactory factory = new IOFactory();
            engineErpPortal = factory.getEngine(EngineConstants.ORACLE, connStr);
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        return engineErpPortal;
    }

    protected string chkHierarchy(AbstractEngine engine, decimal headerId, string kind, string empNum)
    {
        string sql = "	SELECT smp_chk_pur_hierarchy(" + headerId.ToString() + ",'" + kind + "','" + empNum + "') val " +
                     "    FROM dual";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string result = null;
        for (int i = 0; i < rows; i++)
            result = ds.Tables[0].Rows[i][0].ToString();
        return result;
    }

    protected string chkAttachKey(AbstractEngine engine, decimal key, decimal fileId)
    {
        string sql = "	SELECT smp_chk_attach_key(" + key.ToString() + "," + fileId + ") val " +
                     "    FROM dual";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string result = null;
        for (int i = 0; i < rows; i++)
            result = ds.Tables[0].Rows[i][0].ToString();
        return result;
    }

}