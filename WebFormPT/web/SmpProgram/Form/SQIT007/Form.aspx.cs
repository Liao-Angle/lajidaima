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
using System.Text;
using System.IO;
using System.Net;

public partial class Program_SCQ_Form_SQIT007_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "SQIT007";
        AgentSchema = "WebServerProject.form.SQIT007.SQIT007Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "EASYFLOW";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SheetNo.Display = false;
        Subject.Display = false;

        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.SQIT007.SQIT007B");
            dos.setTableName("SQIT007B");
            dos.loadFileSchema();
            objects.setChild("SQIT007B", dos);
            wxList.dataSource = dos;
            wxList.updateTable();

            DataObjectSet dos2 = new DataObjectSet();
            dos2.isNameLess = true;
            dos2.setAssemblyName("WebServerProject");
            dos2.setChildClassString("WebServerProject.form.SQIT007.SQIT007C");
            dos2.setTableName("SQIT007C");
            dos2.loadFileSchema();
            objects.setChild("SQIT007C", dos2);
            AttachmentList.dataSource = dos2;
            AttachmentList.updateTable();
        }

        wxList.clientEngineType = engineType;
        wxList.connectDBString = connectString;
        wxList.HiddenField = new string[] { "GUID", "HeaderGUID" };
        AttachmentList.clientEngineType = engineType;
        AttachmentList.connectDBString = connectString;
        AttachmentList.HiddenField = new string[] { "GUID", "HeaderGUID", "Photo64" };
        AttachmentList.InputForm = "Photo.aspx";
        AttachmentList.DialogHeight = 600;
        AttachmentList.DialogWidth = 1000;

        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        base.showData(engine, objects);

        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        RqNO.ValueText = objects.getData("RqNO");
        MID.ValueText = objects.getData("MID");
        Creator.ValueText = objects.getData("Creator");
        PartNo.ValueText = objects.getData("PartNo");
        RqDate.ValueText = objects.getData("RqDate");
        RqPlanDate.ValueText = objects.getData("RqPlanDate");
        RqOwner.ValueText = objects.getData("RqOwner");
        BU.ValueText = objects.getData("BU");
        RqType.ValueText = objects.getData("RqType");
        RqConent.ValueText = objects.getData("RqConent");
        wxsm.ValueText = objects.getData("wxsm");
        charge.ValueText = objects.getData("charge");
        wxEmp.ValueText = objects.getData("wxEmp");

        wxList.dataSource = objects.getChild("SQIT007B");
        wxList.updateTable();
        AttachmentList.dataSource = objects.getChild("SQIT007C");
        AttachmentList.updateTable();


        wxList.IsShowCheckBox = false;
        wxList.ReadOnly = true;
        AttachmentList.IsShowCheckBox = false;
        AttachmentList.ReadOnly = true;
        SheetNo.Display = false;
        Subject.Display = false;
        //顯示發起資料

    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("RqNO", RqNO.ValueText);
            objects.setData("MID", MID.ValueText);
            objects.setData("Creator", Creator.ValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("RqDate", RqDate.ValueText);
            objects.setData("RqPlanDate", RqPlanDate.ValueText);
            objects.setData("RqOwner", RqOwner.ValueText);
            objects.setData("BU", BU.ValueText);
            objects.setData("RqType", RqType.ValueText);
            objects.setData("RqConent", RqConent.ValueText);
            objects.setData("wxsm", wxsm.ValueText);
            objects.setData("charge", charge.ValueText);
            objects.setData("wxEmp", wxEmp.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in wxList.dataSource.getAllDataObjects())
            {
                obj.setData("HeaderGUID", objects.getData("GUID"));
            }

            objects.setChild("SQIT007B", wxList.dataSource);
            //將C表匯進來
            foreach (DataObject obj in AttachmentList.dataSource.getAllDataObjects())
            {
                obj.setData("HeaderGUID", objects.getData("GUID"));
            }

            objects.setChild("SQIT007C", AttachmentList.dataSource);


            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

        }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        //新增判斷資料

        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 維修設備領用 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }

        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        return getSubmitInfo(engine, objects, si);
    }
    
    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定自動編碼格式所需變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    /// <summary>
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        //string creator = "";


        xml += "<SQIT007>";
        //xml += "<TextBox2 DataType=\"java.lang.String\">" + creator + "</TextBox2>";//上級主管是否簽核
        xml += "</SQIT007>";

        param["SQIT007"] = xml;
        return "SQIT007";
    }


    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);
    }

    /// <summary>
    /// 若有加簽，送簽核前呼叫。
    /// 加簽時系統會設定Session("IsAddSign")，所以必需在saveData時執行 setSession("IsAddSign", "AFTER");
    /// AFTER 代表往後簽核
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterSign(engine, currentObject, result);
    }

    /// <summary>
    /// 重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("CREATOR"))
        {
            try
            {
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                base.writeLog(e);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    ///// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        //獲取資訊系統API地址
        string url = "http://10.3.11.102/SimploWebAPI/api/IMS_Maintenance/CloseECPStep2";
        bool resultBool = false;

        string sheetNo = currentObject.getData("SheetNo");

        string sql = @"select RqNO from SQIT007A where SheetNo ='" + sheetNo + "'";
        DataRow bxtb = engine.getDataSet(sql, "data").Tables["data"].Rows[0];

        string number = "";

        number = bxtb["RqNO"].ToString();

        StringBuilder Json = new StringBuilder();
        Json.AppendFormat("{0}", "{");
        Json.AppendFormat("\"number\":\"{0}\",", number);
        //"Y"代表同意
        if (result == "Y")
        {
            Json.AppendFormat("\"type\":\"{0}\"", "1");
        }
        else
        {
            Json.AppendFormat("\"type\":\"{0}\"", "0");
        }

        Json.AppendFormat("{0}", "}");
        try
        {
            resultBool = Send(url, Json);
            writeLog("result:" + result);
            writeLog("number:" + number);
            writeLog("Json:" + Json);
        }
        catch (Exception ex)
        { }
        base.showData(engine, currentObject);
    }
    /// <summary>
    /// 調用API
    /// </summary>
    /// <param name="urlString"></param>
    /// <param name="resultJson"></param>
    /// <returns></returns>
    public bool Send(string urlString, StringBuilder resultJson)
    {
            bool resultBool = false;
            //messageString = "OK";

            try
            {
                Stream dataStream = null;

                Encoding encoding = Encoding.UTF8;

                //处理HttpWebRequest访问https有安全证书的问题（ 请求被中止: 未能创建 SSL/TLS 安全通道。）
                //ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlString);
                request.Method = "POST";
                //request.Headers.Add("Authorization", string.Format("Bearer {0}", tokenString));
                request.ContentType = "application/json";
                request.KeepAlive = false; //关闭"持久连接"
                request.Timeout = 36000;
                request.ProtocolVersion = HttpVersion.Version11;

                byte[] buffer = encoding.GetBytes(resultJson.ToString());
                request.ContentLength = buffer.Length;
                using (dataStream = request.GetRequestStream())
                {
                    dataStream.Write(buffer, 0, buffer.Length);
                }
                //request.GetRequestStream().Write(buffer, 0, buffer.Length);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                //{
                //    resultString = reader.ReadToEnd();
                //}
                resultBool = true;
            }
            catch (Exception ex)
            {
                resultBool = false;
                //messageString = ex.Message;
            }

            return resultBool;
        }
    /// <summary>
    /// 日誌
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SQIT007.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }
    protected bool wxList_SaveRowData(DataObject objects, bool isNew)
    {

        return true;
    }
}
