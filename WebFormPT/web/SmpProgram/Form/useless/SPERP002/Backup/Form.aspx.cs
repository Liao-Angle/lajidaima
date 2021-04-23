using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;


public partial class SmpProgram_System_Form_SPERP002_Form : SmpErpFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void init()
    {	
        ProcessPageID = "SPERP002"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPERP002.SmpExpenseBillAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPERP";
        this.EnsureChildControls();		
    }

    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;
		CheckBy.Display = false;

        //採購人員
        PurMemberGUID.clientEngineType = engineType;
        PurMemberGUID.connectDBString = connectString;
        if (PurMemberGUID.ValueText.Equals(""))
        {
            PurMemberGUID.ValueText = si.fillerID; //預設帶出登入者
            PurMemberGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        //申請人員
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;

        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        //審核人員1, 2
        CheckBy1.clientEngineType = engineType;
        CheckBy1.connectDBString = connectString;
		CheckBy2.clientEngineType = engineType;
        CheckBy2.connectDBString = connectString;

		
        //採購日期
        //PoCreateDate.ValueText = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
        
        bool isAddNew = base.isNew();
        DataObjectSet detailSet = null;

        if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.SPERP002.SmpExpenseBillDetail");
            detailSet.setTableName("SmpExpenseBillDetail");
            detailSet.loadFileSchema();
            objects.setChild("SmpExpenseBillDetail", detailSet);
        }
        else
        {
            detailSet = objects.getChild("SmpExpenseBillDetail");
        }
        DetailList.dataSource = detailSet;
        DetailList.HiddenField = new string[] { "GUID", "PoNumberGUID","SourceId", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DetailList.FormTitle = "申請明細輸入畫面";
        DetailList.InputForm = "Detail.aspx";
		DetailList.reSortCondition("品名/規格", DataObjectConstants.DESC);
        DetailList.updateTable();


        //由ERP Portal拋入, 所以欄位皆設唯讀
        /*
        Subject.ReadOnly = true;
        PoNumber.ReadOnly = true;
        PoVersion.ReadOnly = true;
        PoCreateDate.ReadOnly = true;
        OriginatorGUID.ReadOnly = true;
        SupplierNum.ReadOnly = true;
        SupplierName.ReadOnly = true;
        Currency.ReadOnly = true;
        Rate.ReadOnly = true;
        PurMemberGUID.ReadOnly = true;
        Organization.ReadOnly = true;
        TaxCode.ReadOnly = true;
        PaymentTerm.ReadOnly = true;
        EnterNonTaxAmount.ReadOnly = true;
        EnterTaxAmount.ReadOnly = true;
        EnterAmount.ReadOnly = true;
        FunctionNoTaxAmount.ReadOnly = true;
        FunctionTaxAmount.ReadOnly = true;
        FunctionAmount.ReadOnly = true;
        Remark.ReadOnly = true;
        DetailList.ReadOnly = true;
        */

        //改變工具列順序
        base.initUI(engine, objects);
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        //請購單URL
        string url = sp.getParam("PortalPoUrl");
        ViewPoUrl.Value = url;
		
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"d:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
		//sw.WriteLine("showData Check By =>  " + objects.getData("CheckBy"));
		//sw.Close();		

        //表單欄位
        SheetNo.Display = false;
        Subject.Display = false;
		PoNumber.Display = false;
		//CheckBy.Display = false;
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);

        //SourceId(ERP Portal PO Header ID)
        SourceId.Value = objects.getData("SourceId");
        PoCreateDate.ValueText = objects.getData("PoCreateDate"); //採購日期
                
        PoNumber.ValueText = objects.getData("PoNumber");

        string headerId = Convert.ToString(objects.getData("PoNumber")); //Oracle PO Number

        hlPoNumber.Text = PoNumber.ValueText;
        //費用請款單Portal URI
        hlPoNumber.NavigateUrl = "javascript:onViewPo(" + headerId + ")";

        PoVersion.ValueText = objects.getData("PoVersion");
        SupplierNum.ValueText = objects.getData("SupplierNum");
        SupplierName.ValueText = objects.getData("SupplierName");
        Currency.ValueText = objects.getData("Currency");
        Rate.ValueText = objects.getData("Rate");
        
        PurMemberGUID.GuidValueText = objects.getData("PurMemberGUID");
        //PurMemberGUID.doValidate();
        PurMemberGUID.doGUIDValidate();
        
        Organization.ValueText = objects.getData("Organization");
        TaxCode.ValueText = objects.getData("TaxCode");
        PaymentTerm.ValueText = objects.getData("PaymentTerm");
        Remark.ValueText = objects.getData("Remark");

        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        //OriginatorGUID.doValidate();
                
        EnterNonTaxAmount.ValueText = objects.getData("EnterNonTaxAmount");
        EnterTaxAmount.ValueText = objects.getData("EnterTaxAmount");
        EnterAmount.ValueText = objects.getData("EnterAmount");
        FunctionNoTaxAmount.ValueText = objects.getData("FunctionNoTaxAmount");
        FunctionTaxAmount.ValueText = objects.getData("FunctionTaxAmount");
        FunctionAmount.ValueText = objects.getData("FunctionAmount");
		
		//sw.WriteLine("showData EnterAmount=>  " + objects.getData("EnterAmount"));
		//sw.WriteLine("showDataFunctionAmount=>  " + objects.getData("FunctionAmount"));
		//sw.Close();
		
		CheckBy.ValueText = objects.getData("CheckBy");
		CheckBy1.ValueText = Convert.ToString(objects.getData("CheckBy1")); //會簽人員一
        if (!CheckBy1.ValueText.Equals(""))
        {
            CheckBy1.doValidate();
        }
		CheckBy2.ValueText = Convert.ToString(objects.getData("CheckBy2")); //會簽人員二
        if (!CheckBy2.ValueText.Equals(""))
        {
            CheckBy2.doValidate();
        }
		CompanyCode.ValueText = objects.getData("CompanyCode");
		if (CompanyCode.ValueText.Equals(""))
        {
			if (Organization.ValueText.Equals("中普 LEV"))
			{
				CompanyCode.ValueText = "中普科技(股)公司";
			}
			if (Organization.ValueText.Equals("中普-Power Bank"))
			{
				CompanyCode.ValueText = "中普科技(股)公司";
			}
			if (Organization.ValueText.Equals("兆普"))
			{
				CompanyCode.ValueText = "兆普電子上海有限公司";
			}
			if (Organization.ValueText.Equals("新普-Hi-Power"))
			{
				CompanyCode.ValueText = "新普科技股份有限公司";
			}
			if (Organization.ValueText.Equals("新普-富岡廠(SMS)"))
			{
				CompanyCode.ValueText = "新普科技股份有限公司";
			}
			if (Organization.ValueText.Equals("新普(SUL)"))
			{
				CompanyCode.ValueText = "Simplo Technology USA Logistic";
			}
			if (Organization.ValueText.Equals("新普總公司"))
			{
				CompanyCode.ValueText = "新普科技股份有限公司";
			}

			if (Organization.ValueText.Equals("光電事業"))
			{
				CompanyCode.ValueText = "新普科技股份有限公司";
			}
        }
		//MessageBox(CompanyCode.ValueText);
		
        DataObjectSet detailSet = null;
        detailSet = objects.getChild("SmpExpenseBillDetail");

        DetailList.dataSource = detailSet;
        DetailList.ReadOnly = true;
        DetailList.updateTable();

        //非填表人不能修改資料
        //string actName = (string)getSession("ACTName");
        //if (actName != "填表人")
		
        if (!isAddNew)
        {
            Subject.ReadOnly = true;
            //PoNumber.ReadOnly = true;
            PoVersion.ReadOnly = true;
            PoCreateDate.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            SupplierNum.ReadOnly = true;
            SupplierName.ReadOnly = true;
            Currency.ReadOnly = true;
            Rate.ReadOnly = true;
            PurMemberGUID.ReadOnly = true;
            Organization.ReadOnly = true;
            TaxCode.ReadOnly = true;
            PaymentTerm.ReadOnly = true;
            EnterNonTaxAmount.ReadOnly = true;
            EnterTaxAmount.ReadOnly = true;
            EnterAmount.ReadOnly = true;
            FunctionNoTaxAmount.ReadOnly = true;
            FunctionTaxAmount.ReadOnly = true;
            FunctionAmount.ReadOnly = true;
            Remark.ReadOnly = true;
			CheckBy1.ReadOnly = true;
            CheckBy2.ReadOnly = true;
			CompanyCode.ReadOnly = true;
        }
        if (actName.Equals("填表人") || actName.Equals("申請人"))
        {
            CheckBy1.ReadOnly = false;
            CheckBy2.ReadOnly = false;
        }
        
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        try
        {
            //System.IO.StreamWriter sw = null;
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
            //sw.WriteLine("saveData start!!");
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            
            bool isAddNew = base.isNew(); //base 父類別
            if (isAddNew)
            {

                //if (SheetNo.ValueText.Equals(""))
                //{
                //    string autoCodeGUID = base.getAutoCodeGUID(engine);// getAutoCodeGUID(engine);
                //    string sheetNo = base.getSheetNoProcedure(engine, autoCodeGUID);
                //    SheetNo.ValueText = sheetNo;
                //    setSession(base.PageUniqueID, "SheetNo", sheetNo);
                //    objects.setData("SheetNo", sheetNo);
                //}

                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                //objects.setData("PoNumber", PoNumber.Value);
                objects.setData("PoVersion", PoVersion.ValueText);
                objects.setData("Currency", Currency.ValueText);
                objects.setData("Rate", Rate.ValueText);
                objects.setData("TaxCode", TaxCode.ValueText);
                objects.setData("PoCreateDate", PoCreateDate.ValueText);
                objects.setData("PurMemberGUID", PurMemberGUID.GuidValueText);
                objects.setData("PaymentTerm", PaymentTerm.ValueText);
                objects.setData("SupplierNum", SupplierNum.ValueText);
                objects.setData("Organization", Organization.ValueText);
                objects.setData("Remark", Remark.ValueText);
                objects.setData("SupplierName", SupplierName.ValueText);
                objects.setData("EnterNonTaxAmount", EnterNonTaxAmount.ValueText);
                objects.setData("EnterTaxAmount", EnterTaxAmount.ValueText);
                objects.setData("EnterAmount", EnterAmount.ValueText);
                objects.setData("FunctionNoTaxAmount", FunctionNoTaxAmount.ValueText);
                objects.setData("FunctionTaxAmount", FunctionTaxAmount.ValueText);
                objects.setData("FunctionAmount", FunctionAmount.ValueText);
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
				objects.setData("CompanyCode", CompanyCode.ValueText);
                objects.setData("SourceId", SourceId.Value);
				objects.setData("CheckBy", CheckBy.ValueText);
                objects.setData("CheckBy1", Convert.ToString(CheckBy1.ValueText));
                objects.setData("CheckBy2", Convert.ToString(CheckBy2.ValueText));
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
               // sw.WriteLine("saveData111");
            }
			
			objects.setData("BuyerName", PurMemberGUID.ReadOnlyValueText);
			objects.setData("OriginatorUserName", OriginatorGUID.ReadOnlyValueText);

            for (int i = 0; i < DetailList.dataSource.getAvailableDataObjectCount(); i++)
            {
                //單身與單頭串接的GUID
                DetailList.dataSource.getAvailableDataObject(i).setData("PoNumberGUID", objects.getData("GUID"));
            }
            objects.setData("CheckBy1", Convert.ToString(CheckBy1.ValueText));
            objects.setData("CheckBy2", Convert.ToString(CheckBy2.ValueText));
            //beforeSetFlow
            setSession("IsSetFlow", "Y");
            //sw.Close();
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            //if (sw != null) sw.Close();
        }
    }

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

    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人, 登入者
        si.fillerName = (string)Session["UserName"]; //填表人姓名
        si.fillerOrgID = depData[0]; 
        si.fillerOrgName = depData[1];
        //si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerID = OriginatorGUID.ValueText;
        //si.ownerName = (string)Session["UserName"];
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0]; //發起單位代號
        si.objectGUID = objects.getData("GUID");

        //MessageBox("initSubmitInfo");
        return si;
    }

    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");
        //MessageBox("getSubmitInfo");
        return si;
    }

    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID); //自動編號設定作業
        return hs;
    }

    //設定表單參數
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
		string sql = "";
		string[] values = null;
        try
        {
     		//sw = new System.IO.StreamWriter(@"d:\ECP\WebFormPT\web\LogFolder\SPERP002.log", true);		
			string actName = (string)getSession("ACTName");
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            //填表人
            string creatorId = si.fillerID;

            //申請人員
            string originatorId = si.ownerID;

            //採購人員的主管
            string purMemberGUID = currentObject.getData("PurMemberGUID");
			values = base.getUserInfo(engine, purMemberGUID);
			string buyerId = values[0];
			
            values = base.getUserManagerInfo(engine, purMemberGUID);			
            string managerGUID = values[0];
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];            
            decimal isBillAmount = decimal.Parse(FunctionAmount.ValueText);
			string checkby = currentObject.getData("CheckBy");
            string checkby1 = currentObject.getData("CheckBy1");
            string checkby2 = currentObject.getData("CheckBy2");
			
			//判斷申請人是否再次簽核 , bypass DeptId
			string flag1 = "";
			sql = "select CheckValue2 from SmpFlowInspect where FormId='SPERP002' and CheckField1='DeptId' and CheckValue1='" + si.ownerOrgID + "' and Status='Y'";
	        DataSet ds = engine.getDataSet(sql, "TEMP");
	        if (ds.Tables[0].Rows.Count > 0)
	        {
	            string byPassDept = ds.Tables[0].Rows[0][0].ToString();

	            if (byPassDept.Equals("Y"))
	            {
	                flag1 = "N";
	            }
	        }
			//string[] userDeptInfo = base.getDeptInfo(engine, purMemberGUID);
            //if (userDeptInfo[0].Equals("C2200") || userDeptInfo[0].Equals("A1300") || userDeptInfo[0].Equals("C1100") 
			//   || userDeptInfo[0].Equals("R2100") || userDeptInfo[0].Equals("R2500") || userDeptInfo[0].Equals("R2600"))
            //{
            //    flag1 = "N";
            //}
			
			//例外流程處理, 當採購人員為4239時, 由系統指定ManagerId
			sql = "select CheckValue2 from SmpFlowInspect where FormId='SPERP002' and CheckField1='UserId' and CheckValue1='" + buyerId + "'   and Status='Y'";
	        ds = engine.getDataSet(sql, "TEMP");
	        if (ds.Tables[0].Rows.Count > 0)
	        {
	            managerId = ds.Tables[0].Rows[0][0].ToString();
	        }
			
			//判斷本位幣是台幣或其他幣別, 影響核決權限簽核
			string flag2 = "TWD";
			string strCurrency = currentObject.getData("Currency");
			string strRate = currentObject.getData("Rate");
			if (strCurrency.Equals("USD") && strRate.Equals("1")){
				flag2 = "USD";
			}
			
            xml += "<SPERP002>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + buyerId + "</originator>";
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
            xml += "<finStaff DataType=\"java.lang.String\"></finStaff>";
            xml += "<billAmount DataType=\"java.lang.Integer\">" + isBillAmount + "</billAmount>";
            xml += "<checkby DataType=\"java.lang.String\">" + checkby + "</checkby>";
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkby1 + "</checkby1>";
            xml += "<checkby2 DataType=\"java.lang.String\">" + checkby2 + "</checkby2>";
			xml += "<flag1 DataType=\"java.lang.String\">" + flag1 + "</flag1>";
            xml += "<flag2 DataType=\"java.lang.String\">" + flag2 + "</flag2>";
            xml += "</SPERP002>";
            
			writeLog("xml : " + xml );
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            //if (sw != null) sw.Close();
        }
        //表單代號
        param["SPERP002"] = xml;
        return "SPERP002";
    }

    protected override string beforeSetFlow(AbstractEngine engine, string setFlowXml)
    {
        return setFlowXml;
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
        if (result.Equals("不同意") || signProcess.Equals("N")) //不同意
        {
            //updateSourceStatus("Terminate");
			updateSourceStatus("Terminate", currentObject);
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
                //base.terminateThisProcess(si.fillerID);
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
		string poNumber = currentObject.getData("PoNumber");
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"c:\ECPSite\WebFormPT\web\LogFolder\WebFormPT.log", true, System.Text.Encoding.Default);
        //sw.WriteLine("afterApprove result=>" + result +  " -- poNumber=> " + currentObject.getData("PoNumber"));
        //sw.Close();
        if (result.Equals("Y"))
        {
            updateSourceStatus(currentObject.getData("PoNumber"),"Close");
        }else
        {
            updateSourceStatus("Reject", currentObject);
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
        string poNumber = "";
        if (currentObject != null)
        {
            //poNumber = currentObject.getData("PoNumber");
			poNumber = PoNumber.ValueText;
        }
        else
        {
            poNumber = PoNumber.ValueText;
        }

        if (!poNumber.Equals(""))
        {
            result = updateSourceStatus(poNumber, status);
        }
        return result;
    }	
	
    /// <summary>
    /// 更新採購單狀態
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string status)
    {
        string result = "";
		string poNumber = currentObject.getData("PoNumber");
		//string poNumber = PoNumber.ValueText;

        if (!poNumber.Equals(""))
        {            
            result = updateSourceStatus(poNumber, status);
        }
        
        return result;
    }

    private string updateSourceStatus(string number, string status)
    {
        string result = "";
        OracleConnection conn = null;

        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "SMP_IMPORT_PO.UPDATE_WF_STATUS";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_PO_NUMBER", OracleType.Number).Value = number;
            objCmd.Parameters.Add("P_WF_STATUS", OracleType.VarChar).Value = status;
            objCmd.Parameters.Add("RETURN_VALUE", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
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


    protected void DetailList_AddOutline(DataObject objects, bool isNew)
    {
        calculateTotal();

    }
    protected void DetailList_DeleteData()
    {
        calculateTotal();
    }
    private void calculateTotal()
    {
        decimal total = 0;
        try
        {
            for (int i = 0; i < DetailList.dataSource.getAvailableDataObjectCount(); i++)
            {
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("Amount"));
            }
        }
        catch { };

        FunctionAmount.ValueText = total.ToString();
    }
    protected void CheckBy2_BeforeClickButton()
    {
        CheckBy2.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void CheckBy1_BeforeClickButton()
    {
        CheckBy1.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPERP002.log", true, System.Text.Encoding.Default);
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
}
