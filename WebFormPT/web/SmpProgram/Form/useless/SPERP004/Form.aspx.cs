using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
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

public partial class SmpProgram_Form_SPERP004_Form : SmpErpFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPERP004";
        AgentSchema = "WebServerProject.form.SPERP004.SmpReceiptFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPERP";
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

        //申請人
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
        OriginatorGUID.Display = false;

        //驗收人員
        AccepterId.clientEngineType = engineType;
        AccepterId.connectDBString = connectString;
        
        //驗收人員一
        Accepter1GUID.clientEngineType = engineType;
        Accepter1GUID.connectDBString = connectString;
        
        //驗收人員二
        Accepter2GUID.clientEngineType = engineType;
        Accepter2GUID.connectDBString = connectString;

        //驗收人員三
        Accepter3GUID.clientEngineType = engineType;
        Accepter3GUID.connectDBString = connectString;

        //驗收人員四
        Accepter4GUID.clientEngineType = engineType;
        Accepter4GUID.connectDBString = connectString;

        //驗收人員五
        Accepter5GUID.clientEngineType = engineType;
        Accepter5GUID.connectDBString = connectString;

        bool isAddNew = base.isNew();
        DataObjectSet detailSet = null;
        if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.SPERP004.SmpReceiptDetail");
            detailSet.setTableName("SmpReceiptDetail");
            detailSet.loadFileSchema();
            objects.setChild("SmpReceiptDetail", detailSet);
        }
        else
        {
            detailSet = objects.getChild("SmpReceiptDetail");
        }
        DataListLine.dataSource = detailSet;
        DataListLine.HiddenField = new string[] { "GUID", "ReceiptFormGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListLine.NoAdd = true;
        DataListLine.NoDelete = true;
        DataListLine.NoModify = true;
        DataListLine.updateTable();

        //唯讀欄位
        Subject.ReadOnly = true;
        OriginatorGUID.ReadOnly = true;
        ReceptNum.ReadOnly = true;
        VatCode.ReadOnly = true;
        VatRegistrationNum.ReadOnly = true;
        DueDate.ReadOnly = true;
        PaymentName.ReadOnly = true;
        PackingSlip.ReadOnly = true;
        VendorName.ReadOnly = true;
        Rate.ReadOnly = true;
        ShippedDate.ReadOnly = true;
        Comments.ReadOnly = true;
        WaybillAirbillNum.ReadOnly = true;
        CurrencyCode.ReadOnly = true;
        AccepterId.ReadOnly = true;
        Accepter1GUID.ReadOnly = true;
        Accepter2GUID.ReadOnly = true;
        Accepter3GUID.ReadOnly = true;
        Accepter4GUID.ReadOnly = true;
        Accepter5GUID.ReadOnly = true;

        //不顯示欄位
        ShipmentHeaderId.Display = false;

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
        Subject.ValueText = objects.getData("Subject");
        SetOfBookName.ValueText = objects.getData("SetOfBookName");
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
        OriginatorGUID.doGUIDValidate();
        //驗收人員
        AccepterId.ValueText = objects.getData("AccepterId");
        if (!AccepterId.ValueText.Equals(""))
        {
            AccepterId.doValidate();
        }
        //驗收人員一
        Accepter1GUID.GuidValueText = objects.getData("Accepter1GUID");
        if (!Accepter1GUID.GuidValueText.Equals(""))
        {
            Accepter1GUID.doGUIDValidate();
        }
        //驗收人員二
        Accepter2GUID.GuidValueText = objects.getData("Accepter2GUID");
        if (!Accepter2GUID.GuidValueText.Equals(""))
        {
            Accepter2GUID.doGUIDValidate();
        }
        //驗收人員三
        Accepter3GUID.GuidValueText = objects.getData("Accepter3GUID");
        if (!Accepter3GUID.GuidValueText.Equals(""))
        {
            Accepter3GUID.doGUIDValidate();
        }
        //驗收人員四
        Accepter4GUID.GuidValueText = objects.getData("Accepter4GUID");
        if (!Accepter4GUID.GuidValueText.Equals(""))
        {
            Accepter4GUID.doGUIDValidate();
        }
        //驗收人員五
        Accepter5GUID.GuidValueText = objects.getData("Accepter5GUID");
        if (!Accepter5GUID.GuidValueText.Equals(""))
        {
            Accepter5GUID.doGUIDValidate();
        }
        ReceptNum.ValueText = objects.getData("ReceptNum");
        VatCode.ValueText = objects.getData("VatCode");
        VatRegistrationNum.ValueText = objects.getData("VatRegistrationNum");
        DueDate.ValueText = objects.getData("DueDate");
        PaymentName.ValueText = objects.getData("PaymentName");
        PackingSlip.ValueText = objects.getData("PackingSlip");
        VendorName.ValueText = objects.getData("VendorName");
        Rate.ValueText = objects.getData("Rate");
        ShippedDate.ValueText = objects.getData("ShippedDate");
        Comments.ValueText = objects.getData("Comments");
        WaybillAirbillNum.ValueText = objects.getData("WaybillAirbillNum");
        CurrencyCode.ValueText = objects.getData("CurrencyCode");
        ShipmentHeaderId.ValueText = objects.getData("ShipmentHeaderId");
        FlowId.ValueText = objects.getData("FlowId");

        string actName = Convert.ToString(getSession("ACTName"));
        if (actName == "" || actName.Equals("填表人"))
        {

        }
        else if (actName.Equals("驗收人"))
        {
            Accepter1GUID.ReadOnly = false;
            Accepter2GUID.ReadOnly = false;
            Accepter3GUID.ReadOnly = false;
            Accepter4GUID.ReadOnly = false;
            Accepter5GUID.ReadOnly = false;
        }
        else
        {
            
        }
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
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
        }

        objects.setData("Accepter1GUID", Accepter1GUID.GuidValueText);
        objects.setData("Accepter2GUID", Accepter2GUID.GuidValueText);
        objects.setData("Accepter3GUID", Accepter3GUID.GuidValueText);
        objects.setData("Accepter4GUID", Accepter4GUID.GuidValueText);
        objects.setData("Accepter5GUID", Accepter5GUID.GuidValueText);
        //for MCloud
        //objects.setData("OriginatorUserName", OriginatorGUID.ReadOnlyValueText);
        //objects.setData("AccepterUserName", AccepterId.ReadOnlyValueText);
        //objects.setData("Accepter1UserName", Accepter1GUID.ReadOnlyValueText);
        //objects.setData("Accepter2UserName", Accepter2GUID.ReadOnlyValueText);
        //objects.setData("Accepter3UserName", Accepter3GUID.ReadOnlyValueText);
        //objects.setData("Accepter4UserName", Accepter4GUID.ReadOnlyValueText);
        //objects.setData("Accepter5UserName", Accepter5GUID.ReadOnlyValueText);
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;

        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = OriginatorGUID.ValueText; //表單關系人
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
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
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        string[] values = null;
        values = base.getUserInfo(engine, currentObject.getData("OriginatorGUID"));
        string originatorId = values[0];
        string accepter = currentObject.getData("AccepterId");
        values = base.getUserInfo(engine, currentObject.getData("Accepter1GUID"));
        string accepter1 = values[0];
        values = base.getUserInfo(engine, currentObject.getData("Accepter2GUID"));
        string accepter2 = values[0];
        values = base.getUserInfo(engine, currentObject.getData("Accepter3GUID"));
        string accepter3 = values[0];
        values = base.getUserInfo(engine, currentObject.getData("Accepter4GUID"));
        string accepter4 = values[0];
        values = base.getUserInfo(engine, currentObject.getData("Accepter5GUID"));
        string accepter5 = values[0];

        //string originatorGUID = OriginatorGUID.GuidValueText;
        //string[] values = base.getUserManagerInfo(engine, originatorGUID);
        //string managerGUID = values[0];
        //values = base.getUserInfo(engine, managerGUID);
        //string managerId = values[0];

        xml += "<SPERP004>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<accepter DataType=\"java.lang.String\">" + accepter + "</accepter>";
        xml += "<manager DataType=\"java.lang.String\"></manager>";
        xml += "<accepter1 DataType=\"java.lang.String\">" + accepter1 + "</accepter1>";
        xml += "<accepter2 DataType=\"java.lang.String\">" + accepter2 + "</accepter2>";
        xml += "<accepter3 DataType=\"java.lang.String\">" + accepter3 + "</accepter3>";
        xml += "<accepter4 DataType=\"java.lang.String\">" + accepter4 + "</accepter4>";
        xml += "<accepter5 DataType=\"java.lang.String\">" + accepter5 + "</accepter5>";
        xml += "</SPERP004>";

        //表單代號
        param["SPERP004"] = xml;
        return "SPERP004";
    }

    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine"></param>
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
    /// <param name="engine"></param>
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
            string backOpinion = (string)Session["tempBackOpinion"];
            try
            {
                if (backOpinion.Equals(""))
                {
                    MessageBox("請輸入退件原因!");
                }
                else
                {
                    //updateSourceStatus("Reject");
                    //base.terminateThisProcess(si.fillerID);
                    base.terminateThisProcess();
                }
            }
            catch (Exception e)
            {
                base.writeLog(e);
                throw new Exception(e.StackTrace);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }


    /// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        try
        {
            string sheetNo = currentObject.getData("SheetNo");
            string sourceId = currentObject.getData("FlowId");
            string userId = (string)Session["UserID"];
            if (userId == null || userId.Equals(""))
            {
                userId = currentObject.getData("AccepterId");
            }
            writeLog("SheetNo = " + sheetNo + ", sourceId = " + sourceId);
            if (result.Equals("Y"))
            {
                updateSourceStatus(sourceId, "Close", userId);
            }
            else
            {
                updateSourceStatus("Reject", currentObject);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        base.afterApprove(engine, currentObject, result);
    }

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string status, DataObject currentObject)
    {
        string result = "";
        string sourceId = currentObject.getData("FlowId");
        string userId = (string)Session["UserID"];
        if (userId == null || userId.Equals(""))
        {
            userId = currentObject.getData("AccepterId");
        }
        if (!sourceId.Equals(""))
        {
            result = updateSourceStatus(sourceId, status, userId);
        }
        return result;
    }

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="number"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string number, string status, string userId)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "SMP_ERP_CUSTOMIZATION.upd_erp_workflow";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_flow_id", OracleType.Number).Value = Convert.ToInt32(number);
            objCmd.Parameters.Add("p_status", OracleType.VarChar).Value = status;
            objCmd.Parameters.Add("p_emp_num", OracleType.VarChar).Value = userId;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);

            writeLog(number + "^" + status + "^" + userId);
            writeLog(result);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPERP004.log", true, System.Text.Encoding.Default);
            sw.WriteLine(DateTime.Now + " " + message);
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
}