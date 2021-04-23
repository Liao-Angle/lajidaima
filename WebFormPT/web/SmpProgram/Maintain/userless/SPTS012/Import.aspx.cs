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
        TEMPLATEEXP1 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", "TEMPLATEEXP1", "下載範本格式說明");
        TEMPLATEEXP2 = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", "TEMPLATEEXP2", "下載範例檔案");
        PreviewButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", "TEMPLATEEXP3", "預覽");
        ImportButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", "TEMPLATEEXP4", "匯入");
        DownloadButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", "TEMPLATEEXP5", "查看錯誤資料");
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
            int auth = authagent.getAuth("SPTS012M", (string)Session["UserID"], (string[])Session["usergroup"]);
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
        mstr += "function SPTS012_Msg(){";
        mstr += "alert('" + msg + "');";
        mstr += "}";
        mstr += "window.setTimeout('SPTS012_Msg()', 100);";
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
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", 
                "QueryError", "請選擇匯入檔案"));
        }
        else
        {            
            try
            {
                string tempPath = "d:\\temp\\SPTS012_" + IDProcessor.getID("");   //string tempPath = Utility.G_GetTempPath() + "\\";           
                fName = tempPath + ".xls";               
                //sw1 = new StreamWriter(tempPath + ".log" , true);
                //sw1.WriteLine("PreviewButton_Click : " + System.DateTime.Now.ToString());
                //檔案上傳入TempFolder                 
                Request.Files[0].SaveAs(fName);               
                //取得匯入資料
                //ArrayList aryData = ExcelReader.getData(fName); //需使用32位元
                SmpTSMaintainPage tsmp = new SmpTSMaintainPage();
                ArrayList aryData = tsmp.getExcelData(fName);
                 
                //設定AgentSchema                                              
                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPTS004.SmpTSLecturerAgent");
                agent.engine = engine;
                agent.query("(1=2)");               

                //取得欄位資料加入datalist
                DataListExpertise.HiddenField = new string[] { "GUID", "CompanyCode", "LecturerSource", "InLecturerGUID", "InLecturerDeptGUID", "ExtCompany", "SpecialSkill", "Experience", "Orientation", "Professional", "Management", "Quality", "EHS", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
                DataObjectSet dos = agent.defaultData;
                string fileMsg = "";               
                int errCount = 0;

                //取得公司別權限
                string userGUID = (string)Session["UserGUID"];                                
                bool hasPrivilege = false;
                string companyPrivilege = "";
                companyPrivilege = tsmp.getCompanyCode(engine, userGUID);

                for (int i = 0; i < aryData.Count; i++)
                {                    
                    string[] data = (string[])aryData[i];
                    if (data[0].Equals("ADD"))
                    {
                        string companyCode = data[1];
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
                                if (!data[1].Equals("") && !data[2].Equals(""))
                                {
                                    string sql = "select CompanyName from SmpTSCompanyV where CompanyCode = '" + companyCode + "'";
                                    string companyName = (string)engine.executeScalar(sql);
                                    string lecturerSourceCode = data[2];
                                    sql = "select LecturerSourceValue from SmpTSLecturerSourceV where LecturerSourceCode = '" + lecturerSourceCode + "'";
                                    string lecturerSourceValue = (string)engine.executeScalar(sql);
                                    string userId = data[3];
                                    sql = "select u.OID, u.userName, u.mailAddress, f.organizationUnitOID from Users u join Functions f on f.occupantOID = u.OID where id='" + userId + "' and isMain=1";
                                    DataSet ds = engine.getDataSet(sql, "TEMP");
                                    string inLecturerGUID = "";
                                    string userName = "";
                                    string email = data[6];
                                    string inLecturerDeptGUID = "";
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        inLecturerGUID = ds.Tables[0].Rows[0][0].ToString();
                                        userName = ds.Tables[0].Rows[0][1].ToString();
                                        email = ds.Tables[0].Rows[0][2].ToString();
                                        inLecturerDeptGUID = ds.Tables[0].Rows[0][3].ToString();
                                    }

                                    DataObject objects = dos.create();
                                    objects.setData("GUID", IDProcessor.getID(""));
                                    objects.setData("CompanyCode", data[1]);
                                    objects.setData("CompanyName", companyName);
                                    objects.setData("LecturerSource", lecturerSourceCode);
                                    objects.setData("LecturerSourceValue", lecturerSourceValue);
                                    objects.setData("InLecturerGUID", inLecturerGUID);
                                    objects.setData("id", userId);
                                    objects.setData("userName", userName);
                                    objects.setData("InLecturerDeptGUID", inLecturerDeptGUID);
                                    objects.setData("ExtLecturer", data[4]);
                                    objects.setData("ExtCompany", data[5]);
                                    objects.setData("Email", email);
                                    objects.setData("StartDate", data[7]);
                                    objects.setData("EndDate", data[8]);
                                    objects.setData("Telephone", data[9]);
                                    objects.setData("SpecialSkill", data[10]);
                                    objects.setData("Experience", data[11]);
                                    objects.setData("Orientation", data[12]);
                                    objects.setData("Professional", data[13]);
                                    objects.setData("Management", data[14]);
                                    objects.setData("Quality", data[15]);
                                    objects.setData("EHS", data[16]);
                                    objects.setData("IS_DISPLAY", "Y");
                                    objects.setData("IS_LOCK", "N");
                                    objects.setData("DATA_STATUS", "Y");
                                    dos.add(objects);
                                }
                                else
                                {
                                    errCount++;
                                    fileMsg += data[3] + data[4] + ":此筆資料不完整!";
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
                 
                string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", 
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
                alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", 
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
            DataObjectSet dos = DataListExpertise.dataSource;
            if (dos != null && dos.getAvailableDataObjectCount() > 0)
            {
                engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                agent = new NLAgent();
                agent.loadSchema("WebServerProject.maintain.SPTS004.SmpTSLecturerAgent");
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
                        string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message",
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
            alert(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts012_import_aspx.language.ini", "message", "alertMsg", 
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
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        try
        {
            string sql = null;
            for (int i = 0; i < ods.getAvailableDataObjectCount(); i++)
            {
                string errMsg = "";
                DataObject objects = ods.getAvailableDataObject(i);
                string guid = objects.getData("GUID");
                string companyCode = objects.getData("CompanyCode");
                string lecturerSource = objects.getData("LecturerSource");
                string userId = objects.getData("id");
                string extLecturer = objects.getData("ExtLecturer");
                string extCompany = objects.getData("ExtCompany");
                string telephone = objects.getData("Telephone");
                string startDate = objects.getData("StartDate");
                string endDate = objects.getData("EndDate");
                string orientation = objects.getData("Orientation");
                string professional = objects.getData("Professional");
                string management = objects.getData("Management");
                string quality = objects.getData("Quality");
                string ehs = objects.getData("EHS");

                if (endDate.Equals(""))
                {
                    endDate = "9999/12/31";
                }

                if (companyCode.Equals(""))
                {
                    errMsg += "請選擇[公司別]!";
                }

                if (lecturerSource.Equals(""))
                {
                    errMsg += "請選擇[講師來源]!";
                }

                if (!startDate.Equals("") && !endDate.Equals(""))
                {
                    DateTime startTime = Convert.ToDateTime(startDate);
                    DateTime endTime = Convert.ToDateTime(endDate);
                    //檢查日期
                    if (startTime.CompareTo(endTime) >= 0)
                    {
                        errMsg += "有效期間(迄)需大於有效期間(起)!";
                    }
                }

                //內部-需選擇內部講師
                if (lecturerSource.Equals("I"))
                {
                    if (startDate.Equals(""))
                    {
                        errMsg += "請選擇[有效時間(起)]!";
                    }
                    sql = "select OID from Users where id='" + userId + "'";
                    string inLecturerGUID = (string)engine.executeScalar(sql);
                    if (inLecturerGUID == null || inLecturerGUID.Equals(""))
                    {
                        errMsg += "請輸入[內部講師]!";
                    }

                    //在同一有效期間內內部講師(員工工號)不可重覆
                    sql = "select count(*) cnt from SmpTSLecturer where InLecturerGUID='" + inLecturerGUID + "' " +
                            "and GUID != '" + guid + "' " +
                            "and ((StartDate <= '" + startDate + "' and (EndDate >= '" + startDate + "' or EndDate = '')) " +
                                "or (StartDate <= '" + endDate + "' and (EndDate >= '" + endDate + "' or EndDate = '')))";
                    int cnt = (int)engine.executeScalar(sql);
                    if (cnt > 0)
                    {
                        errMsg += "在同一有效期間[內部講師]不可重覆!";
                    }
                }
                //外部-需輸入外部講師、公司/機構、信箱、電話
                else if (lecturerSource.Equals("O"))
                {
                    if (extLecturer.Equals(""))
                    {
                        errMsg += "請輸入[外部講師]!";
                    }
                    if (extCompany.Equals(""))
                    {
                        errMsg += "請輸入[公司/機構]!";
                    }
                    if (telephone.Equals(""))
                    {
                        errMsg += "請輸入[電話]!";
                    }

                    //在同一有效期間內外部講師姓名不可重覆
                    sql = "select count(*) cnt from SmpTSLecturer where ExtLecturer='" + extLecturer + "' and ExtCompany='" + extCompany + "' " +
                            "and GUID != '" + guid + "'";
                    int cnt = (int)engine.executeScalar(sql);
                    if (cnt > 0)
                    {
                        errMsg += "同一公司外部講師[" + extLecturer + "]不可重覆!";
                    }
                }

                //教授課程類至少選一項
                if (orientation.Equals("") && professional.Equals("") && !management.Equals("") && quality.Equals("") && ehs.Equals(""))
                {
                    errMsg += "請選擇[教授課程類別]!";
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
            if (engine!=null)
                engine.close();
            
        }
        return result;
        
    }
    
}
