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
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using com.dsc.kernal.utility;
using com.dsc.kernal.document;
using System.IO;
using System.Xml;
using WebServerProject.auth;
using NPOI.HSSF.UserModel;

public partial class SmpProgram_Maintain_SPTS006_Import : BaseWebUI.GeneralWebPage
{
    protected string TEMPLATEEXP1 = "下載範本格式說明";
    protected string TEMPLATEEXP2 = "下載範例檔案";
    protected string TEMPLATEEXP5 = "匯入";

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    /// <summary>
    /// init 設定表單元件Text
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
        TEMPLATEEXP1 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts006_import_aspx.language.ini", "message", "TEMPLATEEXP1", "下載範本格式說明");
        TEMPLATEEXP2 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts006_import_aspx.language.ini", "message", "TEMPLATEEXP2", "下載範例檔案");
        ImportButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts006_import_aspx.language.ini", "message", "TEMPLATEEXP5", "匯入");
        base.OnInit(e);
    }

    /// <summary>
    /// page load
    /// 1. 權限判斷
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //權限判斷
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            AUTHAgent authagent = new AUTHAgent();
            authagent.engine = engine;
            int auth = authagent.getAuth("SPTS006M", (string)Session["UserID"], (string[])Session["usergroup"]);
            engine.close();
            if ((!authagent.parse(auth, AUTHAgent.ADD)) && (!authagent.parse(auth, AUTHAgent.MODIFY)) && (!authagent.parse(auth, AUTHAgent.DELETE)))
            {
                Response.Redirect("~/NoAuth.aspx");
            }
        }

    }

    /// <summary>
    /// script alert message
    /// </summary>
    /// <param name="msg"></param>
    private void alert(string msg)
    {
        string mstr = "<script language=javascript>";
        mstr += "function SPTS006_Msg(){";
        mstr += "alert('" + msg + "');";
        mstr += "}";
        mstr += "window.setTimeout('SPTS006_Msg()', 100);";
        mstr += "</script>";

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);
    }

    /// <summary>
    /// Script open window, 開窗下載檔案
    /// </summary>
    /// <param name="url"></param>
    private void downloadFile(string url)
    {
        string mstr = "<script language=javascript>";
        mstr += "function SPTS006_Msg(){";
        mstr += "window.open('"+url+"');";
        mstr += "}";
        mstr += "window.setTimeout('SPTS006_Msg()', 100);";
        mstr += "</script>";

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);

    }

    /// <summary>
    /// 點選匯入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImportButton_Click(object sender, EventArgs e)
    {

        if (Request.Files.Count == 0)
        {
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts006_import_aspx.language.ini", "message", "QueryError", "請選擇匯入檔案"));
        }
        else
        {
            IOFactory factory = new IOFactory();
            AbstractEngine engine = null;
            NLAgent agent = null;
            try
            {
                //檔案上傳入TempFolder
                string tempPath = Utility.G_GetTempPath() + "\\";
                string fName = tempPath + IDProcessor.getID("") + ".xls";
                Request.Files[0].SaveAs(fName);

                //取得匯入資料
                //ArrayList aryData = ExcelReader.getData(fName); //需使用者32位元
                ArrayList aryData = getData(fName);
                //設定AgentSchema

                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPTS006.SmpTSExpertiseRepAgent");
                agent.engine = engine;
                agent.query("(1=2)");

                //取得欄位資料加入datalist
                DataObjectSet dos = agent.defaultData;
                string fileMsg = "";
                bool isShowMsg = true;
                for (int i = 0; i < aryData.Count; i++)
                {
                    DataObject objects = dos.create();
                    string[] data = (string[])aryData[i];
                    if (data[0].Equals("ADD"))
                    {
                        objects.setData("GUID", IDProcessor.getID(""));
                        objects.setData("IS_LOCK", "N");
                        objects.setData("IS_DISPLAY", "Y");
                        objects.setData("DATA_STATUS", "Y");
                        objects.setData("JobTitle", data[1]);
                        objects.setData("JobFunction", data[2]);
                        objects.setData("Educational", data[3]);
                        objects.setData("Experience", data[4]);
                        objects.setData("Course", data[5]);
                        objects.setData("Skill", data[6]);
                        objects.setData("Evaluation", data[7]);
                        objects.setData("StartYear", data[8]);
                        objects.setData("EndYear", data[9]);
                        objects.setData("Remark", data[10]);
                    }
                    fileMsg += "第[" + i + "]列加入清單中:" + objects.saveXML();
                    dos.add(objects);
                }
                agent.defaultData = dos;
                fileMsg += "資料更新至資料庫!";
                if (!agent.update())
                {
                    fileMsg += engine.errorString;
                    isShowMsg = false;
                    throw new Exception(engine.errorString);
                }
                DataListExpertiseRep.HiddenField = new string[] { "GUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
                DataListExpertiseRep.dataSource = dos;
                DataListExpertiseRep.updateTable();

                //刪除上傳檔案
                System.IO.File.Delete(fName);

                string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts006_import_aspx.language.ini", "message", "returnMsg", "匯入成功");
                returnMsg += ", 匯入筆數:" + aryData.Count;

                if (isShowMsg)
                {
                    alert(returnMsg);

                    string errFile = tempPath + IDProcessor.getID("") + ".log";
                    StreamWriter sw = new StreamWriter(errFile, false);
                    sw.Write(fileMsg);
                    sw.Close();
                }
                else
                {
                    //這裡要下載檔案
                    string errFile = tempPath + IDProcessor.getID("") + ".log";
                    StreamWriter sw = new StreamWriter(errFile, false);
                    sw.Write(fileMsg);
                    sw.Close();

                    downloadFile("GetFile.aspx?FID=" + Server.UrlEncode(errFile));
                }
            }
            catch (Exception te)
            {
                writeLog(te);
                alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts006_import_aspx.language.ini", "message", "alertMsg", "處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: ") + te.Message);
            }
            finally
            {
                if(engine != null) engine.close();
            }
        }
    }

    /// <summary>
    /// 取得Excel檔案內容
    /// </summary>
    /// <param name="fName"></param>
    /// <returns></returns>
    private ArrayList getData(string fName) {
        ArrayList aryData = new ArrayList();
        string filePath = string.Format(fName);
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        HSSFWorkbook wk = new HSSFWorkbook(fs); 
        HSSFSheet hst = (HSSFSheet)wk.GetSheetAt(0);
        for (int i = 0; i<=hst.LastRowNum; i++)
        {
            if (i == 0)
            {
                continue;
            }
            HSSFRow row = (HSSFRow)hst.GetRow(i);
            int cellNum = row.LastCellNum;
            string[] values = new string[cellNum];
            for (int j = 0; j<cellNum; j++)
            {
                string cellValue = row.GetCell(j) == null ? "0" : row.GetCell(j).ToString();
                values[j] = cellValue;
                MessageBox(cellValue);
            }
            aryData.Add(values);
        }
        fs.Close();
        return aryData;
    }
}
