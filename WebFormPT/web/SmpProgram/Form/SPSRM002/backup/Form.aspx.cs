using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using com.dsc.flow.server;

public partial class SmpProgram_System_Form_SPSRM002_Form : SmpErpFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPSRM002"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPSRM002.SmpSupplierIntoPlantAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPSRM";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo"); //填表人的一些基本資訊
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string[,] ids = null;

        //申請人員
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
		
		//申請單位
	    DeptGUID.clientEngineType = engineType;
	    DeptGUID.connectDBString = connectString;
	    DeptGUID.ValueText = si.submitOrgID;
	    DeptGUID.doValidate();
		
		//審核人員
        CheckByGUID.clientEngineType = engineType;
        CheckByGUID.connectDBString = connectString;
		
		//會簽人員
		Countersign1GUID.clientEngineType = engineType;
        Countersign1GUID.connectDBString = connectString;
		Countersign2GUID.clientEngineType = engineType;
        Countersign2GUID.connectDBString = connectString;
		Countersign3GUID.clientEngineType = engineType;
        Countersign3GUID.connectDBString = connectString;
		

        if (HtmlContentCode.InnerHtml.Equals(""))
        {
            string innerHtml = "<font size=2>Content is null, this data must been created from eSRM. As below is an sample.</font><table cellSpacing='1' cellPadding='2' ><tr><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Doc Number</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>1013000001</td><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Status</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>OPEN</td></tr><tr><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Vendor Name</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>TEST</td><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Vendor Type</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>機構類</td></tr><tr><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Contract Kind</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>新合約</td><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Assess Kind</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>實地評鑑</td></tr><tr><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Workflow Status</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/></td><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Workflow Number</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/></td></tr><tr><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Description</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>TEST</td><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Creator</td> <td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>Tony_Huang, 黃英傑 SMP.資訊部</td></tr></table><p><table cellSpacing='1' cellPadding='2' ><tr><td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Line Number</td> <td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Request Name</td> <td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Description</td> <td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Qty</td> <td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Days</td> <td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Kind</td> <td align='center' bgcolor='#cccc99'><font face='arial' color='#336699' size='2'/><b/>Attachment</td> </tr><tr><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>1</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>test1111</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>test1</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>1</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>1</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>Internal</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/><a href='javascript:onViewAttach(\"SMP_REQUEST_DOC_LINES_T\",1,\"BeoALu2UGAI1\",\"BatchNumber(1000000001)\");'>Yes</a></td></tr><tr><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>2</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>test2</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>test2</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>1</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>1</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/>External</td><td align='left' bgcolor='#f7f7e7'><font face='arial' size='2'/><a href='javascript:onViewAttach(\"SMP_REQUEST_DOC_LINES_T\",2,\"BeoALu2UGAI1\",\"BatchNumber(1000000001)\");'>Yes</a></td></tr></table>";
            HtmlContentCode.InnerHtml = innerHtml;
        }

        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
		//表單欄位

        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo");
        //SourceId
        SourceId.Value = objects.getData("SourceId");
		//eSRM Number
		//SrmNumber.Value = objects.getData("SrmNumber");
		//SrmNumber.Display = false;
		//SrmNumber.ValueText = objects.getData("SrmNumber");
		//string headerId = Convert.ToString(objects.getData("SourceId")); //Oracle eSRM Number
		//hlSrmNumber.Text = SrmNumber.ValueText;
		//Document Request URL
        //string srmUrl = sp.getParam("PortalSrmHrUrl");
        //ViewSrmUrl.Value = srmUrl;
        //hlSrmNumber.NavigateUrl = "javascript:onViewSrm(" + headerId + ")";		
		
		

        //SRM此需求的附件Url        
		WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string url = sp.getParam("SMPReqDocUrl");
        ViewAttachUrl.Value = url;
        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		//申請單位
        DeptGUID.ValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
        //審核人
        CheckByGUID.GuidValueText = objects.getData("CheckByGUID");
        if (!Convert.ToString(CheckByGUID.GuidValueText).Equals(""))
        {
            CheckByGUID.doGUIDValidate();
        }
		//會簽人員
        Countersign1GUID.GuidValueText = objects.getData("Countersign1GUID");
        if (!Convert.ToString(Countersign1GUID.GuidValueText).Equals(""))
        {
            Countersign1GUID.doGUIDValidate();
        }
		Countersign2GUID.GuidValueText = objects.getData("Countersign2GUID");
        if (!Convert.ToString(Countersign2GUID.GuidValueText).Equals(""))
        {
            Countersign2GUID.doGUIDValidate();
        }
		Countersign3GUID.GuidValueText = objects.getData("Countersign3GUID");
        if (!Convert.ToString(Countersign3GUID.GuidValueText).Equals(""))
        {
            Countersign3GUID.doGUIDValidate();
        }
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //內容
        string htmlContent = objects.getData("HtmlContent");
        if (!htmlContent.Equals(""))
        {
            HtmlContentCode.InnerHtml = htmlContent;
        }        

        //發起時
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName == "" || actName.Equals("填表人"))
        {
            Subject.ReadOnly = false;
            OriginatorGUID.ReadOnly = false;
        }
        else
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
			SheetNo.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
			DeptGUID.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            Countersign1GUID.ReadOnly = true;
			Countersign2GUID.ReadOnly = true;
			Countersign3GUID.ReadOnly = true;
        }

        if (actName.Equals("申請人"))
        {
            CheckByGUID.ReadOnly = false;
        }
		if (actName.Equals("工安審核"))
        {
			Countersign1GUID.ReadOnly = false;
			Countersign2GUID.ReadOnly = false;
			Countersign3GUID.ReadOnly = false;
        }
		
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        bool isAddNew = base.isNew(); //base 父類別
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SourceId", SourceId.Value);
            objects.setData("Subject", Subject.ValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
			objects.setData("DeptGUID", DeptGUID.GuidValueText);
			//objects.setData("SrmNumber", SrmNumber.Value);
            base.saveData(engine, objects);
        }
        objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
		objects.setData("Countersign1GUID", Countersign1GUID.GuidValueText);
		objects.setData("Countersign2GUID", Countersign2GUID.GuidValueText);
		objects.setData("Countersign3GUID", Countersign3GUID.GuidValueText);
        //beforeSetFlow
        setSession("IsSetFlow", "Y");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));

        if (actName.Equals(""))
        {
            if (!strErrMsg.Equals(""))
            {
                pushErrorMessage(strErrMsg);
                result = false;
            }
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人, 登入者
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 
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
		si.ownerID = OriginatorGUID.ValueText;
		si.ownerName = OriginatorGUID.ReadOnlyValueText;
		depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID); //自動編號設定作業
        return hs;
    }

    /// <summary>
    /// 設定表單參數
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //填表人
        string creatorId = si.fillerID;
        
        //申請人員
        string originatorId = si.ownerID;
        
        //主管
        string originatorGUID = OriginatorGUID.GuidValueText;
        string[] values = base.getUserManagerInfo(engine, originatorGUID);
        string managerGUID = values[0];
        values = base.getUserInfo(engine, managerGUID);
        string managerId = values[0];        
        string checkbyId = CheckByGUID.ValueText;
		string countersign1 = Countersign1GUID.ValueText;
		string countersign2 = Countersign2GUID.ValueText;
		string countersign3 = Countersign3GUID.ValueText; //通知
		        
        xml += "<SPSRM002>";
        xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
        xml += "<safetyStaff DataType=\"java.lang.Integer\">SPSRM002-Safety</safetyStaff>";
        xml += "<checkby DataType=\"java.lang.String\">" + checkbyId + "</checkby>";
		xml += "<countersign1 DataType=\"java.lang.String\">" + countersign1 + "</countersign1>";
        xml += "<countersign2 DataType=\"java.lang.String\">" + countersign2 + "</countersign2>";
        xml += "<countersign3 DataType=\"java.lang.String\">" + countersign3 + "</countersign3>";
		xml += "<notify DataType=\"java.lang.String\">SPSRM002-NOTI</notify>";
        xml += "</SPSRM002>";

        //表單代號
        param["SPSRM002"] = xml;
        return "SPSRM002";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="setFlowXml"></param>
    /// <returns></returns>
    protected override string beforeSetFlow(AbstractEngine engine, string setFlowXml)
    {
        return setFlowXml;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void beforeSend(AbstractEngine engine, DataObject currentObject)
    {
        
        base.beforeSend(engine, currentObject);
    }

    /// <summary>
    /// 簽核前程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        return addSignXml;
    }

    /// <summary>
    /// 簽核後程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        string signProcess = Convert.ToString(Session["signProcess"]);
        
        if (result.Equals("同意") || signProcess.Equals("Y"))
        {
            string actName = Convert.ToString(getSession("ACTName"));
            if (actName == "財務人員")
            {
                //updateSourceStatus("Close");
            }
        }
        else if (result.Equals("不同意") || signProcess.Equals("N")) //不同意
        {
            updateSourceStatus("Terminate");
        }
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
                //SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
                //call oracle function set object workflow status to 'Reject'
                updateSourceStatus("Reject");
                //base.terminateThisProcess(si.fillerID);
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                writeLog(e);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    protected override void reGetProcedure()
    {
        //call oracle function set object workflow status to 'ReGet'
        updateSourceStatus("ReGet");
        base.reGetProcedure();
    }

    /// <summary>
    /// 撤銷程序
    /// </summary>
    protected override void withDrawProcedure()
    {
        //call oracle function set object workflow status to 'WithDraw'
        updateSourceStatus("WithDraw");
        base.withDrawProcedure();
    }

    /// <summary>
    /// 結案程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        //call oracle function set object workflow status to 'close'
        //System.IO.StreamWriter sw = null;
        try
        {
            string sourceId = currentObject.getData("SourceId");
            if (result.Equals("Y"))
            {
                updateSourceStatus(sourceId, "Close");
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            //if (sw != null) sw.Close();
        }
        base.afterApprove(engine, currentObject, result);
    }

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string status)
    {
        string result = "";
        DataObject currentObject = (DataObject)getSession("currentObject");
        string sourceId = currentObject.getData("SourceId");
        if (!sourceId.Equals(""))
        {
            result = updateSourceStatus(sourceId, status);
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
    private string updateSourceStatus(string number, string status)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_pos_workflow.upd_request_workflow";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_doc_id", OracleType.Number).Value = Convert.ToInt16(number);
            objCmd.Parameters.Add("p_workflow_status", OracleType.VarChar).Value = status;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
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
    protected void CheckByGUID_BeforeClickButton()
    {
        CheckByGUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
    }
    protected void Countersign1GUID_BeforeClickButton()
    {
        Countersign1GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
    }
	protected void Countersign2GUID_BeforeClickButton()
    {
        Countersign2GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
    }
	protected void Countersign3GUID_BeforeClickButton()
    {
        Countersign3GUID.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
    }
}