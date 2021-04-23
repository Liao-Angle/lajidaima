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

public partial class SmpProgram_Maintain_SPTS008_Import : BaseWebUI.GeneralWebPage
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
        TEMPLATEEXP1 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", "TEMPLATEEXP1", "下載範本格式說明");
        TEMPLATEEXP2 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", "TEMPLATEEXP2", "下載範例檔案");
        PreviewButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", "TEMPLATEEXP3", "預覽");
        ImportButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", "TEMPLATEEXP4", "匯入");
        DownloadButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", "TEMPLATEEXP5", "查看錯誤資料");
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
            int auth = authagent.getAuth("SPTS008M", (string)Session["UserID"], (string[])Session["usergroup"]);
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
        mstr += "function SPTS008_Msg(){";
        mstr += "alert('" + msg + "');";
        mstr += "}";
        mstr += "window.setTimeout('SPTS008_Msg()', 100);";
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
        //System.IO.StreamWriter sw1 = null;                
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        NLAgent agent = null;
        string fName = "";
        
        //if (Request.Files.Count == 0)
        if (!FuFilePath.HasFile)
        {
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", 
                "QueryError", "請選擇匯入檔案"));
        }
        else
        {            
            try
            {
                string tempPath = "d:\\temp\\SPTS008_" + IDProcessor.getID("");   //string tempPath = Utility.G_GetTempPath() + "\\";           
                fName = tempPath + ".xls";               
                //sw1 = new StreamWriter(tempPath + ".log" , true);
                //sw1.WriteLine("PreviewButton_Click : " + System.DateTime.Now.ToString());
                //檔案上傳入TempFolder                 
                Request.Files[0].SaveAs(fName);               
                //取得匯入資料
                //ArrayList aryData = ExcelReader.getData(fName); //需使用者32位元
                SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
                ArrayList aryData = tsmp.getExcelData(fName);
                 
                //設定AgentSchema                                              
                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPTS008.SmpTSExpertiseRepAgent");
                agent.engine = engine;
                agent.query("(1=2)");               

                //取得欄位資料加入datalist
                DataListExpertise.HiddenField = new string[] { "GUID",  "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
                DataObjectSet dos = agent.defaultData;
                string fileMsg = "";               
                int errCount = 0;

                //取得公司別權限
                string userGUID = (string)Session["UserGUID"];                                
                Boolean hasPrivilege = false;
                string companyPrivilege = "";
                companyPrivilege = tsmp.getCompanyCode(engine, userGUID);

                for (int i = 0; i < aryData.Count; i++)
                {                    
                    string[] data = (string[])aryData[i];
                    if (data[0].Equals("ADD"))
                    {
                        String sqlCompanyCode = "select CompanyCode from SmpTSCompanyV where CompanyCode = ";
                        string companyCode = (string)engine.executeScalar(sqlCompanyCode + "'" + data[1] + "'");
                        if (companyCode != null)
                        {                            
                            if (companyPrivilege.Length > 0 && companyPrivilege.IndexOf(data[1]) != -1)
                                hasPrivilege = true; 
                          
                            if (!hasPrivilege)
                            {
                                errCount++;
                                fileMsg += "無此公司別匯入(" + data[1] + ")權限!";  
                            }
                            else
                            {
                                if (!data[2].Equals("") && !data[3].Equals("") && !data[4].Equals("") && !data[5].Equals("") &&
                                !data[6].Equals("") && !data[7].Equals("") && !data[8].Equals("") && !data[9].Equals("") &&
                                !data[10].Equals("") && !data[11].Equals(""))
                                {
                                    string startYear = data[9];
                                    string endYear = "9999";
                                    if (!data[10].Equals(""))
                                    {
                                        endYear = data[10];
                                    }

                                    //檢查日期
                                    if (startYear.CompareTo(endYear) > 0)
                                    {
                                        errCount++;
                                        fileMsg += "工作職務:" + data[2] + ",有效年度(迄)需大於有效年度(起)!";
                                    }
                                    else
                                    {
                                        int educationalIndexOf =   data[3].IndexOf(":");                                      

                                        if (educationalIndexOf == -1 ||
                                            String.Compare(data[3].Substring(0, educationalIndexOf), "1", true) < 0 ||
                                            String.Compare(data[3].Substring(0, educationalIndexOf), "6", true) > 0)
                                        {
                                            errCount++;
                                            fileMsg += "工作職務:" + data[2] + ",學歷代號不正確!";
                                           
                                        }
                                        else
                                        {
                                            DataObject objects = dos.create();
                                            objects.setData("GUID", IDProcessor.getID(""));
                                            objects.setData("IS_LOCK", "N");
                                            objects.setData("IS_DISPLAY", "Y");
                                            objects.setData("DATA_STATUS", "Y");
                                            objects.setData("CompanyCode", data[1]);
                                            objects.setData("JobFunction", data[2]);
                                            objects.setData("Educational", data[3].Substring(0, educationalIndexOf));
                                            objects.setData("JobItem", data[4]);
                                            objects.setData("Experience", data[5]);
                                            objects.setData("Course", data[6]);
                                            objects.setData("Skill", data[7]);
                                            objects.setData("Evaluation", data[8]);
                                            objects.setData("StartYear", data[9]);
                                            objects.setData("EndYear", data[10]);
                                            objects.setData("Remark", data[11]);
                                            //fileMsg += "第[" + i + "]列加入清單中:"+ objects.saveXML();
                                            dos.add(objects);
                                        }
                                    }
                                }
                                else
                                {
                                    errCount++;
                                    fileMsg += data[2] + ":此工作職稱資料不完整!";
                                }
                            }
                        }
                        else
                        {
                            errCount++;
                            fileMsg += "公司別不可空白或無此公司別!";  
                        }
                    }                    
                } //for              
                DataListExpertise.dataSource = dos;
                DataListExpertise.updateTable();
                 
                string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", 
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
                alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", 
                    "alertMsg", "處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: ") + te.Message);
            }
            finally
            {
                //刪除上傳檔案
                System.IO.File.Delete(fName);
                if(engine != null) 
                    engine.close();
                //if (sw1 != null)
                //    sw1.Close();
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
            DataObjectSet expertiseSet = DataListExpertise.dataSource;
            if (expertiseSet != null && expertiseSet.getAvailableDataObjectCount() > 0)
            {
                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPTS008.SmpTSExpertiseRepAgent");
                agent.engine = engine;
                agent.query("(1=2)");

                agent.defaultData = expertiseSet;
                msg = checkFieldData(expertiseSet);

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
                        string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message",
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
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts008_import_aspx.language.ini", "message", "alertMsg", 
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
    public string checkFieldData(com.dsc.kernal.databean.DataObjectSet objectSets)
    {
        string errMsg = "";        
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        try
        {
            string sql = null;
            for (int i = 0; i < objectSets.getAvailableDataObjectCount(); i++)
            {                
                DataObject obj = objectSets.getAvailableDataObject(i);
                string guid = obj.getData("GUID");
                string companyCode = obj.getData("CompanyCode");
                string jobFunction = obj.getData("JobFunction");
                string startYear = obj.getData("StartYear");
                string endYear = obj.getData("EndYear");

                if (endYear.Equals(""))
                {
                    endYear = "9999";
                }

                //在同一有效期間內員工、部門、工作職務不可重覆
                sql = "select count(*) cnt from SmpTSExpertiseRep where JobFunction = '" + jobFunction + "' " +
                        "and CompanyCode ='" + companyCode + "' " +
                        "and GUID != '" + guid + "' " +
                        "and ((StartYear <= '" + startYear + "' and (EndYear >= '" + startYear + "' or EndYear = '')) " +
                        "or (StartYear <= '" + endYear + "' and (EndYear >= '" + endYear + "' or EndYear = '')))";
                int cnt = (int)engine.executeScalar(sql);
                if (cnt > 0)
                {
                    errMsg += "工作職稱:" + jobFunction + ",在同一有效期間不可重覆!";
                }
                   
            }
        }
        catch (Exception e)
        {
            writeLog(e);
            errMsg += e.Message;
        }
        finally
        {
            if (engine!=null)
                engine.close();
            
        }
        return errMsg;
        
    }
    
}
