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
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPTS012_Import : BaseWebUI.GeneralWebPage
{
    protected string TEMPLATEEXP1 = "下載範本格式說明";
    protected string TEMPLATEEXP2 = "下載範例檔案";
    protected string TEMPLATEEXP3 = "預覽";
    protected string TEMPLATEEXP4 = "匯入";
    protected string TEMPLATEEXP5 = "查看錯誤資料";
    

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
        TEMPLATEEXP1 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", "TEMPLATEEXP1", "下載範本格式說明");
        TEMPLATEEXP2 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", "TEMPLATEEXP2", "下載範例檔案");
        PreviewButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", "TEMPLATEEXP3", "預覽");
        ImportButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", "TEMPLATEEXP4", "匯入");
        DownloadButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", "TEMPLATEEXP5", "查看錯誤資料");
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
            int auth = authagent.getAuth("SPPM002M", (string)Session["UserID"], (string[])Session["usergroup"]);
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
        mstr += "function SPPM002_Msg(){";
        mstr += "alert('" + msg + "');";
        mstr += "}";
        mstr += "window.setTimeout('SPPM002_Msg()', 100);";
        mstr += "</script>";

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);
    }
    
    /// <summary>
    /// 下載錯誤訊息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DownloadButton_Click(object sender, EventArgs e)
    {
        string errFile = (string)getSession(PageUniqueID, "ImportErrLog");  
        System.Net.WebClient wc = new System.Net.WebClient(); //呼叫 webclient 方式做檔案下載
        byte[] xfile = null; 
                   
        xfile = wc.DownloadData(errFile);
        string xfileName = System.IO.Path.GetFileName("Err.log");
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(xfileName));
        HttpContext.Current.Response.ContentType = "application/octet-stream"; //二進位方式
        HttpContext.Current.Response.BinaryWrite(xfile); //內容轉出作檔案下載
        HttpContext.Current.Response.End();
    }

   
    /// <summary>
    /// 點選匯入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void PreviewButton_Click(object sender, EventArgs e)
    {               
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        NLAgent agent = null;
        string fName = "";
        
        if (!FuFilePath.HasFile)
        {
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", 
                "QueryError", "請選擇匯入檔案"));
        }
        else
        {
            int errCount = 0;
            try
            {
                string tempPath = "d:\\temp\\SPPM002_" + IDProcessor.getID("");   //string tempPath = Utility.G_GetTempPath() + "\\";           
                fName = tempPath + ".xls";               
                //檔案上傳入TempFolder                 
                Request.Files[0].SaveAs(fName);               
                //取得匯入資料
                //ArrayList aryData = ExcelReader.getData(fName); //需使用32位元
                SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
                ArrayList aryData = tsmp.getExcelData(fName);
                 
                //設定AgentSchema                                              
                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPPM002.SmpPmUserAssessmentAgent");
                agent.engine = engine;
                agent.query("(1=2)");               

                //取得欄位資料加入datalist
                DataListExpertise.HiddenField = new string[] { "GUID", "UserGUID", "deptOID", "FirstAssessUserGUID", "SecondAssessUserGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
                DataObjectSet dos = agent.defaultData;
                string fileMsg = "";

                for (int i = 0; i < aryData.Count; i++)
                {
                    string result = "";
                    string[] data = (string[])aryData[i];
                    string action = data[0].Trim();
                    if (!string.IsNullOrEmpty(action))
                    {
                        if (action.Equals("ADD") || action.Equals("MOD"))
                        {
                            string userId = data[1];
                            string firstUserId = data[3];
                            string secondUserId = "";
                            if (data.Length >= 5)
                            {
                                secondUserId = data[5];
                            }
                            if (!userId.Equals("") && !firstUserId.Equals(""))
                            {
                                string[] userResult = SmpPmMaintainUtil.getUserInfoById(engine, userId);
                                string[] firstResult = new string[] { "", "" };
                                string[] secondResult = new string[] { "", "" };

                                firstResult = SmpPmMaintainUtil.getUserGUID(engine, firstUserId);
                                if (string.IsNullOrEmpty(firstResult[0]))
                                {
                                    result += data[2] + " 找不到一階人員: " + firstUserId + ", 列" + (i + 2); ;
                                }

                                if (!string.IsNullOrEmpty(secondUserId))
                                {
                                    secondResult = SmpPmMaintainUtil.getUserGUID(engine, secondUserId);
                                    if (string.IsNullOrEmpty(secondResult[0]))
                                    {
                                        result += data[2] + " 找不到二階人員: " + secondUserId + ", 列" + (i + 2); ; ;
                                    }
                                }

                                DataObject objects = dos.create();
                                if (action.Equals("ADD"))
                                {
                                    objects.setData("GUID", IDProcessor.getID(""));
                                }
                                else
                                {
                                    agent.query("UserGUID='" + userResult[0] + "'");
                                    DataObjectSet set = agent.defaultData;
                                    if (set.getAvailableDataObjectCount() > 0)
                                    {
                                        DataObject dataObject = set.getAvailableDataObject(0);
                                        objects.setData("GUID", dataObject.getData("GUID"));
                                    }
                                    else
                                    {
                                        result += data[2] + " 找不到資料更新, 列" + (i + 2);
                                    }
                                }

                                if (string.IsNullOrEmpty(result))
                                {
                                    objects.setData("IS_DISPLAY", "Y");
                                    objects.setData("IS_LOCK", "N");
                                    objects.setData("DATA_STATUS", "Y");
                                    objects.setData("orgName", userResult[8]);
                                    objects.setData("UserGUID", userResult[0]);
                                    objects.setData("empNumber", userId);
                                    objects.setData("empName", userResult[1]);
                                    //objects.setData("deptOID", );
                                    objects.setData("deptId", userResult[6]);
                                    objects.setData("deptName", userResult[7]);
                                    objects.setData("titleName", userResult[3]);
                                    objects.setData("FirstAssessUserGUID", firstResult[0]);
                                    objects.setData("FirstAssessUserId", firstUserId);
                                    objects.setData("FirstAssessUserName", firstResult[1]);
                                    objects.setData("SecondAssessUserGUID", secondResult[0]);
                                    objects.setData("SecondAssessUserId", secondUserId);
                                    objects.setData("SecondAssessUserName", secondResult[1]);
                                    dos.add(objects);
                                }
                            }
                            else
                            {
                                result += data[1] + " 此筆資料不完整, 列" + (i + 2);
                            }
                        }
                        else
                        {
                            result += data[1] + " 此筆資料指令不正確, 列" + (i + 2);
                        }

                        if (!string.IsNullOrEmpty(result))
                        {
                            errCount++;
                            fileMsg += result;
                        }
                    }
                }
                DataListExpertise.dataSource = dos;
                DataListExpertise.updateTable();
                 
                string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm004_import_aspx.language.ini", 
                    "message", "returnMsg", "Excel讀取完成");
                //returnMsg += "! 總筆數:" + aryData.Count ;
                if (errCount > 0)
                {
                    returnMsg += "! " + errCount + " 筆資料異常，請點選【查看錯誤資料】按鈕下載錯誤資料!";
                    DownloadButton.Enabled = true;
                     
                    string errFile = tempPath + "_err" + ".log";
                    setSession("ImportErrLog", errFile);
                    StreamWriter sw = new StreamWriter(errFile, false);
                    sw.Write(fileMsg);
                    sw.Close();

                }
                else
                {
                    DownloadButton.Enabled = false;
                }
                alert(returnMsg);
            }
            catch (Exception te)
            {
                writeLog(te);
                alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", 
                    "alertMsg", errCount + "處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: ") + te.Message);
            }
            finally
            {
                //刪除上傳檔案
                System.IO.File.Delete(fName);
                if (engine != null)
                {
                    engine.close();
                }
            }
        }
    }

    /// <summary>
    /// 儲入DB
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImportButton_Click(object sender, EventArgs e)
    {       
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        NLAgent agent = null;
        String msg = "";
       
        try
        {
            DataObjectSet dos = DataListExpertise.dataSource;
            if (dos != null && dos.getAvailableDataObjectCount() > 0)
            {
                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPPM002.SmpPmUserAssessmentAgent");
                agent.engine = engine;
                agent.query("(1=2)");
                agent.defaultData = dos;
                msg = checkFieldData(dos);

                if (msg.Equals(""))
                {
                    msg = "資料更新至資料庫!";
                    if (!agent.update())  //僅更新未被刪除的資料
                    {
                        msg += engine.errorString;
                        throw new Exception(msg);
                    }
                    else
                    {
                        string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message",
                            "returnMsg", "匯入成功");
                        returnMsg += ",  匯入筆數:" + DataListExpertise.dataSource.getAvailableDataObjectCount();
                        alert(returnMsg);
                    }
                }
                else
                {
                    alert(msg);
                }
            }
            else 
            {
                alert("無資料可執行匯入!");

            } 
        }
        catch (Exception ex)
        {
            writeLog(ex);
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_sppm002_import_aspx.language.ini", "message", "alertMsg", 
                "儲存失敗，錯誤訊息為: ") + ex.Message);
        }
        finally
        {
            if (engine != null) engine.close();
        }
       
    }    

    /// <summary>
    //  檢查資料正確性
    /// </summary>
    /// <param name="fName"></param>
    /// <returns></returns>
    public string checkFieldData(com.dsc.kernal.databean.DataObjectSet ods)
    {
        string result = "";        
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        try
        {
            engine = factory.getEngine(engineType, connectString);
            for (int i = 0; i < ods.getAvailableDataObjectCount(); i++)
            {
                string errMsg = "";
                DataObject objects = ods.getAvailableDataObject(i);
                string guid = objects.getData("GUID");
                string firstAssessUserGUID = objects.getData("FirstAssessUserGUID");
                if (string.IsNullOrEmpty(firstAssessUserGUID))
                {
                    errMsg += "一階評核人員必需有值!";
                }

                if (!errMsg.Equals(""))
                {
                    result += "列" + (i+1) + ": " + errMsg + "\\n";
                }
            }
        }
        catch (Exception e)
        {
            writeLog(e);
            result += e.Message;
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            
        }
        return result;
        
    }
    
}
