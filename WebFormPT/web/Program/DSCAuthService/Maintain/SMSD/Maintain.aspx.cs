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

public partial class Program_DSCAuthService_Maintain_SMSD_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        ALABEL1.Text=com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "ALABEL1", "選取資料庫：");
        ALABEL2.Text= com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "ALABEL2", "匯出項目 ：");
        ALABEL3.Text=com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "ALABEL3", "檔案格式 ：");
        ExportButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "ExportButton.Text", "匯出");
        DBSetting.Items[0].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "DBSetting1", "目前資料庫設定");
        DBSetting.Items[1].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "DBSetting2", "所有資料庫設定");
        ItemType.Items[0].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "ItemType1", "群組權限設定");
        ItemType.Items[1].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "ItemType2", "權限項目設定");
        FileFormat.Items[0].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "FileFormat1", "標準Xml");
        FileFormat.Items[1].Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "global", "FileFormat2", "Excel Xml試算表");
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
    protected void ExportButton_Click(object sender, EventArgs e)
    {
        string xml = "";

        IOFactory sfactory = new IOFactory();
        AbstractEngine sengine = sfactory.getEngine((string)Session["engineType"], (string)Session["connectString"]);
        WebServerProject.SysParam param = new WebServerProject.SysParam(sengine);
        string ws = param.getParam("AuthWebService");
        sengine.close();

        com.dsc.kernal.webservice.WSDLClient service = new com.dsc.kernal.webservice.WSDLClient(ws);
        service.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
        service.build(false);

        if (ItemType.SelectedValue.Equals("AuthItem"))
        {
            xml = (string)service.Sync("getAllAuthItem", new object[0]);
        }
        else
        {
            xml = (string)service.Sync("getAllAuthSchema", new object[0]);
        }
        //檔案轉換
        string tempPath = Utility.G_GetTempPath() + "\\";
        if (FileFormat.SelectedValue.Equals("Xml"))
        {
            string dataFile = tempPath + IDProcessor.getID("") + ".xml";
            StreamWriter sw = new StreamWriter(dataFile, false);
            sw.Write(xml);
            sw.Close();

            downloadFile("GetFile.aspx?FID=" + Server.UrlEncode(dataFile));

        }
        else
        {
            if (ItemType.SelectedValue.Equals("AuthItem"))
            {
                ArrayList data = new ArrayList();
                XMLProcessor xp = new XMLProcessor(xml, 1);
                XmlNodeList itemList = xp.selectAllNodes(@"//Root/DB/Item");
                foreach (XmlNode item in itemList)
                {
                    string[] tag = new string[3];
                    tag[0] = item.Attributes["ID"].Value;
                    tag[1] = item.Attributes["Title"].Value;
                    tag[2] = item.Attributes["Description"].Value;

                    data.Add(tag);
                }
                string[] headers = new string[] { com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME1", "權限項目代號"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME2", "權限項目名稱"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME3", "備註") };
                string dataFile = tempPath + IDProcessor.getID("") + ".xml";
                setData(dataFile, headers, data);

                downloadFile("GetFile.aspx?FID=" + Server.UrlEncode(dataFile));
            }
            else
            {
                ArrayList data = new ArrayList();
                XMLProcessor xp = new XMLProcessor(xml, 1);
                XmlNodeList groupList = xp.selectAllNodes(@"//Root/DB/Group");
                foreach (XmlNode group in groupList)
                {
                    string[] tag = new string[15];
                    tag[0] = "AG";
                    tag[1] = group.Attributes["ID"].Value;
                    tag[2] = "";
                    tag[3] = group.Attributes["Title"].Value;
                    tag[4] = "";
                    tag[5] = "";
                    tag[6] = "";
                    tag[7] = "";
                    tag[8] = "";
                    tag[9] = "";
                    tag[10] = "";
                    tag[11] = "";
                    tag[12] = "";
                    tag[13] = "";
                    tag[14] = "";
                    data.Add(tag);

                    //處理AuthItem
                    XmlNodeList itemList = group.SelectNodes("Items/Item");
                    foreach (XmlNode item in itemList)
                    {
                        string[] tags = new string[15];

                        tags[0] = "AI";
                        tags[1] = group.Attributes["ID"].Value;
                        tags[2] = item.Attributes["ID"].Value;
                        tags[3] = "";
                        for (int q = 0; q < 10; q++)
                        {
                            string tgs = "AUTH" + string.Format("{0:00}", q + 1);
                            if (item.Attributes[tgs].Value.Equals("Y"))
                            {
                                tags[4 + q] = "Y";
                            }
                            else
                            {
                                tags[4 + q] = "";
                            }
                        }
                        data.Add(tags);
                    }

                    //處理Users
                    XmlNodeList userList = group.SelectNodes("Users/User");
                    foreach (XmlNode user in userList)
                    {
                        string[] tags = new string[15];

                        tags[0] = "AU";
                        tags[1] = group.Attributes["ID"].Value;
                        tags[2] = user.Attributes["ID"].Value;
                        tags[3] = "";
                        tags[4] = "";
                        tags[5] = "";
                        tags[6] = "";
                        tags[7] = "";
                        tags[8] = "";
                        tags[9] = "";
                        tags[10] = "";
                        tags[11] = "";
                        tags[12] = "";
                        tags[13] = "";
                        tags[14] = "";
                        data.Add(tags);
                    }

                }

                string[] headers = new string[] { com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME4", "動作"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME5", "群組代號"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME6", "項目/使用者代號"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME7", "群組名稱"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH01", "讀取"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH02", "新增"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH03", "修改"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH04", "刪除"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH05", "列印"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH06", "報表"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH07", "擁有"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH08", "權限08"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH09", "權限09"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "AUTH10", "權限10"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsd_maintain_aspx.language.ini", "message", "NAME3", "備註") };
                string dataFile = tempPath + IDProcessor.getID("") + ".xml";
                setData(dataFile, headers, data);

                downloadFile("GetFile.aspx?FID=" + Server.UrlEncode(dataFile));

            }
        }


    }



    /// <summary>
    /// 將資料寫入檔案(Excel檔，以XML的方式寫入)
    /// </summary>
    /// <param name="fileName">檔案名稱(Excel檔)</param>
    /// <param name="header">欄位標題</param>
    /// <param name="datas">資料內容</param>
    public void setData(string fileName, string[] header, ArrayList datas)
    {
        string outputStr = "";
        //檔頭
        outputStr += "<?xml version=\"1.0\"?>\n";
        outputStr += "<?mso-application progid=\"Excel.Sheet\"?>\n";
        outputStr += "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\n";
        outputStr += " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\n";
        outputStr += " xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\n";
        outputStr += " xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\n";
        outputStr += " xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n";
        outputStr += " <DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">\n";
        outputStr += "  <Author>dsc</Author>\n";
        outputStr += "  <LastAuthor>dsc</LastAuthor>\n";
        outputStr += "  <Created>2006-05-24T05:18:50Z</Created>\n";
        outputStr += "  <Company>dsc</Company>\n";
        outputStr += "  <Version>11.5606</Version>\n";
        outputStr += " </DocumentProperties>\n";
        outputStr += " <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n";
        outputStr += "  <WindowHeight>9225</WindowHeight>\n";
        outputStr += "  <WindowWidth>11700</WindowWidth>\n";
        outputStr += "  <WindowTopX>240</WindowTopX>\n";
        outputStr += "  <WindowTopY>75</WindowTopY>\n";
        outputStr += "  <ProtectStructure>False</ProtectStructure>\n";
        outputStr += "  <ProtectWindows>False</ProtectWindows>\n";
        outputStr += " </ExcelWorkbook>\n";
        outputStr += " <Styles>\n";
        outputStr += "  <Style ss:ID=\"Default\" ss:Name=\"Normal\">\n";
        outputStr += "   <Alignment ss:Vertical=\"Center\"/>\n";
        outputStr += "   <Borders/>\n";
        outputStr += "   <Font ss:FontName=\"新細明體\" x:CharSet=\"136\" x:Family=\"Roman\" ss:Size=\"12\"/>\n";
        outputStr += "   <Interior/>\n";
        outputStr += "   <NumberFormat ss:Format=\"@\"/>\n";
        outputStr += "   <Protection/>\n";
        outputStr += "  </Style>\n";
        outputStr += " </Styles>\n";

        //加入一個Sheet
        outputStr += " <Worksheet ss:Name=\"Sheet1\">\n";
        outputStr += "  <Table ss:ExpandedColumnCount=\"" + header.Length.ToString() + "\" ss:ExpandedRowCount=\"" + string.Format("{0}", datas.Count + 1) + "\" x:FullColumns=\"1\"\n";
        outputStr += "   x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"16.5\">\n";

        //標題
        outputStr += "   <Row ss:AutoFitHeight=\"0\">\n";
        for (int i = 0; i < header.Length; i++)
        {

            outputStr += "    <Cell><Data ss:Type=\"String\">" + header[i] + "</Data></Cell>\n";

        }
        outputStr += "   </Row>\n";

        for (int i = 0; i < datas.Count; i++)
        {
            string[] line = (string[])datas[i];
            outputStr += "   <Row ss:AutoFitHeight=\"0\">\n";
            for (int j = 0; j < line.Length; j++)
            {
                outputStr += "    <Cell><Data ss:Type=\"String\">" + line[j] + "</Data></Cell>\n";
            }
            outputStr += "   </Row>\n";
        }
        outputStr += "  </Table>\n";
        outputStr += "  <WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n";
        outputStr += "   <Unsynced/>\n";
        outputStr += "   <Selected/>\n";
        outputStr += "   <ProtectObjects>False</ProtectObjects>\n";
        outputStr += "   <ProtectScenarios>False</ProtectScenarios>\n";
        outputStr += "  </WorksheetOptions>\n";
        outputStr += " </Worksheet>\n";

        //檔尾
        outputStr += "</Workbook>\n";

        StreamWriter fs = new StreamWriter(fileName, false, System.Text.Encoding.Unicode);
        fs.Write(outputStr);
        fs.Close();

    }

    protected void FileFormat_SelectedIndexChanged(object sender, EventArgs e)
    {
        DBSetting.Enabled = false;
    }
}
