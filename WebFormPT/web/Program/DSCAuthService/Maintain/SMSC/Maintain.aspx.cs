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
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.IO;
using System.Xml;
using WebServerProject.auth;

public partial class Program_DSCAuthService_Maintain_SMSC_Maintain : BaseWebUI.GeneralWebPage
{
    protected string TEMPLATEEXP1 = "下載範本格式說明";
    protected string TEMPLATEEXP2 = "下載範例檔案";
    protected string TEMPLATEEXP3 = "匯入異動檔";
    protected string TEMPLATEEXP4 = "匯入完整檔案";
    protected string TEMPLATEEXP5 = "匯入";
    protected string TEMPLATEEXP6 = "目前資料庫設定";
    protected string TEMPLATEEXP7 = "所有資料庫設定";

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }
    protected override void OnInit(EventArgs e)
    {
        TEMPLATEEXP1 = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "TEMPLATEEXP1", "下載範本格式說明");
        TEMPLATEEXP2 = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "TEMPLATEEXP2", "下載範例檔案");
        IncreRadio.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "TEMPLATEEXP3", "匯入異動檔");
        TotalRadio.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "TEMPLATEEXP4", "匯入完整檔案");
        ImportButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "TEMPLATEEXP5", "匯入");
        DBSetting.Items[0].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "TEMPLATEEXP6", "目前資料庫設定");
        DBSetting.Items[1].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "TEMPLATEEXP7", "所有資料庫設定");
        base.OnInit(e);
    }
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

            int auth = authagent.getAuth("SMSC", (string)Session["UserID"], (string[])Session["usergroup"]);

            engine.close();

            string mstr = "";

            if ((!authagent.parse(auth, AUTHAgent.ADD)) && (!authagent.parse(auth, AUTHAgent.MODIFY)) && (!authagent.parse(auth, AUTHAgent.DELETE)))
            {
                Response.Redirect("~/NoAuth.aspx");
            }

            DBSetting.Enabled = false;
        }

    }
    private void alert(string msg)
    {
        string mstr = "<script language=javascript>";
        mstr += "function SMSC_Msg(){";
        mstr += "alert('" + msg + "');";
        mstr += "}";
        mstr += "window.setTimeout('SMSC_Msg()', 100);";
        mstr += "</script>";

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);
    }
    private void downloadFile(string url)
    {
        string mstr = "<script language=javascript>";
        mstr += "function SMSC_Msg(){";
        mstr += "window.open('"+url+"');";
        mstr += "}";
        mstr += "window.setTimeout('SMSC_Msg()', 100);";
        mstr += "</script>";

        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);

    }
    protected void ImportButton_Click(object sender, EventArgs e)
    {

        if (Request.Files.Count == 0)
        {
            alert(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "QueryError", "請選擇匯入檔案"));
        }
        else
        {
            try
            {
                string tempPath = Utility.G_GetTempPath() + "\\";
                string fName = tempPath + IDProcessor.getID("") + ".xls";


                Request.Files[0].SaveAs(fName);

                ArrayList aryData = ExcelReader.getData(fName);

                string xml = "<Root>";
                for (int i = 0; i < aryData.Count; i++)
                {
                    string[] data = (string[])aryData[i];

                    if (data[0].Equals("AG"))
                    {
                        //AddGroup
                        xml += "<Action ID='AddGroup'>";
                        xml += "<Group ID='" + data[1] + "' Title='" + data[3] + "'/>";
                        xml += "</Action>";
                    }
                    else if (data[0].Equals("MG"))
                    {
                        //ModifyGroup
                        xml += "<Action ID='ModifyGroup'>";
                        xml += "<Group ID='" + data[1] + "' Title='" + data[3] + "'/>";
                        xml += "</Action>";
                    }
                    else if (data[0].Equals("DG"))
                    {
                        //ModifyGroup
                        xml += "<Action ID='DeleteGroup'>";
                        xml += "<Group ID='" + data[1] + "' />";
                        xml += "</Action>";
                    }
                    else if (data[0].Equals("AI"))
                    {
                        double authv = 0;
                        for (int z = 4; z <= 13; z++)
                        {
                            if (data[z].Equals("Y"))
                            {
                                authv += System.Math.Pow(2, z - 4);
                            }
                        }
                        //AddItem
                        xml += "<Action ID='AddItem'>";
                        xml += "<Item GroupID='" + data[1] + "' ID='" + data[2] + "' Auth='"+authv.ToString()+"' />";
                        xml += "</Action>";
                    }
                    else if (data[0].Equals("MI"))
                    {
                        double authv = 0;
                        for (int z = 4; z <= 13; z++)
                        {
                            if (data[z].Equals("Y"))
                            {
                                authv += System.Math.Pow(2, z - 4);
                            }
                        }
                        //ModifyItem
                        xml += "<Action ID='ModifyItem'>";
                        xml += "<Item GroupID='" + data[1] + "' ID='" + data[2] + "' Auth='" + authv.ToString() + "' />";
                        xml += "</Action>";
                    }
                    else if (data[0].Equals("DI"))
                    {
                        //DeleteItem
                        xml += "<Action ID='DeleteItem'>";
                        xml += "<Item GroupID='" + data[1] + "' ID='" + data[2] + "'  />";
                        xml += "</Action>";
                    }
                    else if (data[0].Equals("AU"))
                    {
                        //AddUser
                        xml += "<Action ID='AddUser'>";
                        xml += "<User GroupID='" + data[1] + "' ID='" + data[2] + "' />";
                        xml += "</Action>";
                    }
                    else if (data[0].Equals("DU"))
                    {
                        //DeleteUser
                        xml += "<Action ID='DeleteUser'>";
                        xml += "<User GroupID='" + data[1] + "' ID='" + data[2] + "' />";
                        xml += "</Action>";
                    }
                }

                System.IO.File.Delete(fName);

                xml += "</Root>";

                string returnMsg = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "returnMsg", "匯入成功");
                bool isShowMsg = true;
                string fileMsg = "";

                string returnXml = "";

                IOFactory sfactory = new IOFactory();
                AbstractEngine sengine = sfactory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
                WebServerProject.SysParam param = new WebServerProject.SysParam(sengine);
                string ws = param.getParam("AuthWebService");
                sengine.close();

                com.dsc.kernal.webservice.WSDLClient service = new com.dsc.kernal.webservice.WSDLClient(ws);
                service.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
                service.build(false);

                if (TotalRadio.Checked)
                {
                    //這裡要根據所選擇的資料庫做刪除
                    if (true)
                    {
                        string connectString = (string)Session["connectString"];
                        string engineType = (string)Session["engineType"];
                        //目前設定
                        IOFactory factory = new IOFactory();
                        AbstractEngine engine = factory.getEngine(engineType, connectString);

                        deleteAll(engine);

                        engine.close();
                    }

                }
                returnXml = (string)service.callSync("modifyAuthSchema",xml);

                XMLProcessor xp = new XMLProcessor(returnXml, 1);
                XmlNode xn = xp.selectSingleNode(@"/Root/SystemError");
                if (xn != null)
                {
                    returnMsg = xn.InnerText;
                }
                else
                {
                    bool hasError = false;
                    XmlNodeList dbList = xp.selectAllNodes(@"/Root/DB");
                    foreach (XmlNode db in dbList)
                    {
                        XmlNodeList xnl = db.SelectNodes(@"Action");
                        string dbmsg = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg1", "在資料庫索引: ") + db.Attributes["ID"].Value + "-";

                        foreach (XmlNode x in xnl)
                        {
                            XmlNode xr = x.SelectSingleNode("Result");
                            XmlNode xc;
                            if (!xr.Attributes["ReturnValue"].Value.Equals("0"))
                            {
                                //有錯誤
                                hasError = true;
                                if (x.Attributes["ID"].Value.Equals("AddGroup"))
                                {
                                    xc = x.SelectSingleNode("Group");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg2", "新增群組代號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                                else if (x.Attributes["ID"].Value.Equals("ModifyGroup"))
                                {
                                    xc = x.SelectSingleNode("Group");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg3", "修改群組代號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                                else if (x.Attributes["ID"].Value.Equals("DeleteGroup"))
                                {
                                    xc = x.SelectSingleNode("Group");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg4", "刪除群組代號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                                else if (x.Attributes["ID"].Value.Equals("AddItem"))
                                {
                                    xc = x.SelectSingleNode("Item");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg2", "新增群組代號: ") + xc.Attributes["GroupID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg6", "下權限項目代號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                                else if (x.Attributes["ID"].Value.Equals("ModifyItem"))
                                {
                                    xc = x.SelectSingleNode("Item");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg3", "修改群組代號: ") + xc.Attributes["GroupID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg6", "下權限項目代號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                                else if (x.Attributes["ID"].Value.Equals("DeleteItem"))
                                {
                                    xc = x.SelectSingleNode("Item");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg4", "刪除群組代號: ") + xc.Attributes["GroupID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg6", "下權限項目代號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                                else if (x.Attributes["ID"].Value.Equals("AddUser"))
                                {
                                    xc = x.SelectSingleNode("User");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg2", "新增群組代號: ") + xc.Attributes["GroupID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg7", "下使用者帳號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                                else if (x.Attributes["ID"].Value.Equals("DeleteUser"))
                                {
                                    xc = x.SelectSingleNode("User");
                                    fileMsg += dbmsg + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg4", "刪除群組代號: ") + xc.Attributes["GroupID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg7", "下使用者帳號: ") + xc.Attributes["ID"].Value + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "dbmsg5", "時發生錯誤. 訊息為: ") + xr.InnerText + "\r\n";
                                }
                            }
                        }

                        if (hasError)
                        {
                            isShowMsg = false;
                        }
                    }
                }

                if (isShowMsg)
                {
                    alert(returnMsg);
                }
                else
                {
                    //這裡要下載檔案
                    string errFile = tempPath + IDProcessor.getID("")+".log";
                    StreamWriter sw = new StreamWriter(errFile, false);
                    sw.Write(fileMsg);
                    sw.Close();

                    downloadFile("GetFile.aspx?FID=" + Server.UrlEncode(errFile));
                }
            }
            catch (Exception te)
            {
                //alert("處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: " + te.Message);
                alert(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsc_maintain_aspx.language.ini", "message", "alertMsg", "處理檔案發生錯誤. 可能您上傳的檔案格式不和, 或是系統處理錯誤. 錯誤訊息為: ") + te.Message);
            }
        }
    }
    protected void deleteAll(AbstractEngine engine)
    {
        string sql = "";
        sql = "delete from SMSAABA";
        engine.executeSQL(sql);
        sql = "delete from SMSAABB";
        engine.executeSQL(sql);
        sql = "delete from SMSAABC";
        engine.executeSQL(sql);
    }
}
