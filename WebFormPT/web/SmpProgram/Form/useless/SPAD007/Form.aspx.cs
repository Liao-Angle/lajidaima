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


public partial class SmpProgram_System_Form_SPAD007_Form : SmpAdFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPAD007"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD007.SmpForeignTrvlBillingAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>");
        sb.Append(" function submitCheck(){");
        sb.Append("     var sendBefore = \"\";");
		//sb.Append("     var sendBefore = document.getElementById('S_OriForeignForm').value;");
        sb.Append("     if(sendBefore == \"\") {");
        sb.Append("         if(confirm(\"此表單填寫完畢後，若下一關卡為董事長簽核請至原稿資料匣中列印此差旅費報銷單據，並附上差旅費支出明細後送到總經理室。 \")){");
        sb.Append("             return true;");
        sb.Append("         } else {");
        sb.Append("             return false;");
        sb.Append("         }");
        sb.Append("     } else {");
        sb.Append("         return true;");
        sb.Append("     }");
        sb.Append(" }");
        sb.Append("</script>");
        Type ctype = this.GetType();
        ClientScriptManager cm = Page.ClientScript;

        if (!cm.IsStartupScriptRegistered(ctype, "submitCheckScript"))
        {
            cm.RegisterStartupScript(ctype, "submitCheckScript", sb.ToString());
        }

        SubmitButton.BeforeClick = "submitCheck";	
	
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        bool isAddNew = base.isNew();

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;
		
        //申請單位
        DeptGUID.clientEngineType = engineType;
        DeptGUID.connectDBString = connectString;
        if (OriginatorGUID.ValueText.Equals(""))
        {
            DeptGUID.ValueText = si.submitOrgID;
            DeptGUID.doValidate();
        }
		
        //申請人員
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;            
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
		
		//公司別
		string[,] idsCompany = null;
        idsCompany = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad007_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad007_form_aspx.language.ini", "message", "tp", "中普科技")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfoById(engine,(string)Session["UserId"]);
        CompanyCode.ValueText = values[5];
        CompanyCode.ReadOnly = true;
				                
        //職務代理人
        AgentGUID.clientEngineType = engineType;
        AgentGUID.connectDBString = connectString;
        if (AgentGUID.ValueText.Equals(""))
        {
            AgentGUID.ValueText = si.fillerID; //預設帶出登入者
            AgentGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        // 職務名稱
        string[] userFunctionValues = base.getUserInfoById(engine, si.fillerID);
        if (userFunctionValues[0] != "")
        {
            Title.ValueText = userFunctionValues[3];
        }
        else
        {
            Title.ValueText = "";
        }
        
		//審核人
        CheckByGUID.clientEngineType = engineType;
        CheckByGUID.connectDBString = connectString;

        //Radio Group, 費用負擔 Charge Fee
        FeeCharge1.GroupName = "grp1";
        FeeCharge2.GroupName = "grp1";
        FeeCharge3.GroupName = "grp1";
        //FeeCharge4.GroupName = "grp1";
		FeeCharge5.GroupName = "grp1";
        FeeCharge6.GroupName = "grp1";

        //新建立表單不可維護欄位
        Title.ReadOnly = true;  
        TrvlDesc.ReadOnly = true;
        ChgTrvlDesc.ReadOnly = true;
        StartTrvlDate.ReadOnly = true;
        EndTrvlDate.ReadOnly = true;
        ChgStartTrvlDate.ReadOnly = true;
        ChgEndTrvlDate.ReadOnly = true;        
        FeeCharge1.ReadOnly = true;
        FeeCharge2.ReadOnly = true;
        FeeCharge3.ReadOnly = true;
        //FeeCharge4.ReadOnly = true;
		FeeCharge5.ReadOnly = true;
        FeeCharge6.ReadOnly = true;
        SiteUs.ReadOnly = true;
        SiteJp.ReadOnly = true;
        SiteKr.ReadOnly = true;
        SiteSub.ReadOnly = true;
        SiteOther.ReadOnly = true;
        SiteUsDesc.ReadOnly = true;
        SiteJpDesc.ReadOnly = true;
        SiteKrDesc.ReadOnly = true;
        SiteSubDesc.ReadOnly = true;
        SiteOtherDesc.ReadOnly = true;
        PrePayTwd.ReadOnly = true;
        PrePayCny.ReadOnly = true;
        PrePayUsd.ReadOnly = true;
		PrePayJpy.ReadOnly = true;
		PrePayEur.ReadOnly = true;
        PrePayOther.ReadOnly = true;
        PrePayTwdAmt.ReadOnly = true;
        PrePayCnyAmt.ReadOnly = true;
        PrePayUsdAmt.ReadOnly = true;
		PrePayJpyAmt.ReadOnly = true;
		PrePayEurAmt.ReadOnly = true;
        PrePayOtherAmt.ReadOnly = true;
                
        ActualRtnApAmt.ReadOnly = true;
        ActualRtnApDate.ReadOnly = true;
        ActualAmount.ReadOnly = true;
        ActualApAmt.ReadOnly = true;
        FinDesc.ReadOnly = true;
		
		ChgPrePayTwd.ReadOnly = true;
        ChgPrePayCny.ReadOnly = true;
        ChgPrePayUsd.ReadOnly = true;
		ChgPrePayJpy.ReadOnly = true;
		ChgPrePayEur.ReadOnly = true;
        ChgPrePayOther.ReadOnly = true;
        ChgPrePayTwdAmt.ReadOnly = true;
        ChgPrePayCnyAmt.ReadOnly = true;
        ChgPrePayUsdAmt.ReadOnly = true;
		ChgPrePayJpyAmt.ReadOnly = true;
		ChgPrePayEurAmt.ReadOnly = true;
        ChgPrePayOtherAmt.ReadOnly = true;
		PrintButton1.Enabled = true;

        //原出差單據勾稽
        OriForeignForm.clientEngineType = engineType;
        OriForeignForm.connectDBString = connectString;
		string actName = Convert.ToString(getSession("ACTName"));
		//bool isAddNew = base.isNew();
		if (isAddNew)
		{
			OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "' and SMWYAAA020='Y' and (BillFormStatus<>'Y' or  BillFormStatus<>'I' or BillFormStatus is null)   ";
		}
		//if (actName.Equals("填表人") || actName.Equals(""))
        //{
        //    OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "' and SMWYAAA020='Y' and (BillID is null or BillID='') ";
        //}
        //OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "'";              

        string[,] ids = null;

        ids = new string[,] {
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "", "")},
                    {"交通費",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "ids1", "交通費")},
                    {"住宿費",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "ids2", "住宿費")},
                    {"膳雜費",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "ids3", "膳雜費")},
                    {"簽證費",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "ids4", "簽證費")},
                    {"交際費",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "ids5", "交際費")},
                    {"電信費",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "ids6", "電信費")},
                    {"其他費用",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "ids7", "其他費用")}
        };
        PayClass.setListItem(ids);

        ids = new string[,] {
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "", "")},
                    {"TWD",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "TWD", "TWD")},
                    {"USD",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "USD", "USD")},
                    {"CNY",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "CNY", "CNY")},
                    {"CAD",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "CAD", "CAD")},
                    {"AUD",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "AUD", "AUD")},
                    {"EUR",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "EUR", "EUR")},
                    {"KRW",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "KRW", "KRW")},
                    {"HKD",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "HKD", "HKD")},
                    {"JPY",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "JPY", "JPY")},
                    {"INR",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "INR", "INR")},
                    {"PLN",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "PLN", "PLN")},
                    {"THB",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "THB", "THB")},
                    {"SGD",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "SGD", "SGD")},
                    {"MRY",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "MRY", "MRY")},
                    {"OTHER",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad007_detail_aspx.language.ini", "message", "OTHER", "OTHER")}
        };
        OccurCurrency.setListItem(ids);
        
        //由原出差單據帶出預沖銷的出差單或異動單
        if (isAddNew)
        {
            //
        }

        //差旅費報銷明細列表
        DataObjectSet requestSet = null;
        if (isAddNew)
        {
            requestSet = new DataObjectSet();
            requestSet.isNameLess = true;
            requestSet.setAssemblyName("WebServerProject");
            requestSet.setChildClassString("WebServerProject.form.SPAD007.SmpForeignTrvlBillingDetail");
            requestSet.setTableName("SmpForeignTrvlBillingDetail");
            requestSet.loadFileSchema();
            objects.setChild("SmpForeignTrvlBillingDetail", requestSet);
        }
        else
        {
            requestSet = objects.getChild("SmpForeignTrvlBillingDetail");
        }
        RequestList.dataSource = requestSet;
        RequestList.HiddenField = new string[] { "GUID", "HeaderGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        RequestList.updateTable();


        //改變工具列順序
        base.initUI(engine, objects);
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {	
        System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
        //sw.WriteLine("showData ");        
		
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        //sw.WriteLine("ActualStartTrvlDate: " + objects.getData("ActualStartTrvlDate"));
        //sw.WriteLine("ActualEndTrvlDate: " + objects.getData("ActualEndTrvlDate"));
        //sw.Close();

        //表單欄位
        SheetNo.Display = false;
        Subject.Display = false;
		
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo"); //傳入序號, 找到個資

        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID        
		
		//公司別
		string[,] idsCompany = null;
        idsCompany = new string[,]{
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad006_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad006_form_aspx.language.ini", "message", "tp", "中普科技")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
        CompanyCode.ValueText = values[2];
        CompanyCode.ReadOnly = true;

        //申請單位
        DeptGUID.ValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
                
        Title.ValueText = objects.getData("Title"); //職稱
        
        //職務代理人員
		AgentGUID.GuidValueText = objects.getData("AgentGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        AgentGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		//MessageBox("AgentGUID : " + AgentGUID.GuidValueText);

        //審核人員
        string checkByGUID = objects.getData("CheckByGUID");
        if (!checkByGUID.Equals(""))
        {
            CheckByGUID.GuidValueText = checkByGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            CheckByGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }

        //Fee Charge  費用負擔     
        string feeCharge = Convert.ToString(objects.getData("FeeCharge"));
        switch (feeCharge)
        {
            case "0":
                FeeCharge1.Checked = true;
                break;
            case "1":
                FeeCharge2.Checked = true;
                break;
            case "2":
                FeeCharge3.Checked = true;
                break;
            //case "3":
            //    FeeCharge4.Checked = true;
            //    break;
			case "4":
                FeeCharge5.Checked = true;
                break;
            case "5":
                FeeCharge6.Checked = true;
                break;	
            default:
                break;
        }

        TrvlDesc.ValueText = objects.getData("TrvlDesc");         //出差（預辦）事由
        ChgTrvlDesc.ValueText = objects.getData("ChgTrvlDesc");   //異動或取消說明
		//MessageBox("TrvlDesc : " + TrvlDesc.ValueText);
		

        //預計出差日期
        StartTrvlDate.ValueText = objects.getData("StartTrvlDate");
        EndTrvlDate.ValueText = objects.getData("EndTrvlDate");       
        //異動後出差日期
        ChgStartTrvlDate.ValueText = objects.getData("ChgStartTrvlDate");
        ChgEndTrvlDate.ValueText = objects.getData("ChgEndTrvlDate");
        //實際出差日期
        ActualStartTrvlDate.ValueText = objects.getData("ActualStartTrvlDate");
        ActualEndTrvlDate.ValueText = objects.getData("ActualEndTrvlDate");

        //勾稽出差申請單號
        //OriForeignForm.GuidValueText = objects.getData("OriForeignForm");
		//sw.WriteLine("勾稽出差申請單號:  "+  objects.getData("OriForeignForm")); 
        //string oriForeignForm = objects.getData("OriForeignForm");
        //if (!oriForeignForm.Equals(""))
        //{
        //    OriForeignForm.GuidValueText = oriForeignForm; //將值放入人員開窗元件中, 資料庫存放GUID
        //    OriForeignForm.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		//	sw.WriteLine("出差單號:  "+  OriForeignForm.ValueText); 
        //}
		
		OriForeignForm.GuidValueText = objects.getData("OriForeignForm");
        OriForeignForm.doGUIDValidate();
		//sw.WriteLine("出差單號:  "+  objects.getData("OriForeignForm")); 
		
		//OriForeignForm.GuidValueText = objects.getData("OriForeignForm"); //將值放入人員開窗元件中, 資料庫存放GUID
        //OriForeignForm.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		//MessageBox("oriForeignForm : " + objects.getData("OriForeignForm"));
		//MessageBox("oriForeignForm : " + OriForeignForm.ValueText);
		//MessageBox("oriForeignForm : " + OriForeignForm.doGUIDValidate());

        //出差地點
        if (objects.getData("SiteUs").Equals("Y")) { SiteUs.Checked = true; } else { SiteUs.Checked = false; }
        if (objects.getData("SiteJp").Equals("Y")) { SiteJp.Checked = true; } else { SiteJp.Checked = false; }
        if (objects.getData("SiteKr").Equals("Y")) { SiteKr.Checked = true; } else { SiteKr.Checked = false; }
        if (objects.getData("SiteSub").Equals("Y")) { SiteSub.Checked = true; } else { SiteSub.Checked = false; }
        if (objects.getData("SiteOther").Equals("Y")) { SiteOther.Checked = true; } else { SiteOther.Checked = false; }
        SiteUsDesc.ValueText = objects.getData("SiteUsDesc");
        SiteJpDesc.ValueText = objects.getData("SiteJpDesc");
        SiteKrDesc.ValueText = objects.getData("SiteKrDesc");
        SiteSubDesc.ValueText = objects.getData("SiteSubDesc");
        SiteOtherDesc.ValueText = objects.getData("SiteOtherDesc");

        //C 預支申請
        if (objects.getData("PrePayTwd").Equals("Y")) { PrePayTwd.Checked = true; } else { PrePayTwd.Checked = false; }
        if (objects.getData("PrePayCny").Equals("Y")) { PrePayCny.Checked = true; } else { PrePayCny.Checked = false; }
        if (objects.getData("PrePayUsd").Equals("Y")) { PrePayUsd.Checked = true; } else { PrePayUsd.Checked = false; }
		if (objects.getData("PrePayJpy").Equals("Y")) { PrePayJpy.Checked = true; } else { PrePayJpy.Checked = false; }
		if (objects.getData("PrePayEur").Equals("Y")) { PrePayEur.Checked = true; } else { PrePayEur.Checked = false; }
        if (objects.getData("PrePayOther").Equals("Y")) { PrePayOther.Checked = true; } else { PrePayOther.Checked = false; }
        PrePayTwdAmt.ValueText = objects.getData("PrePayTwdAmt");
        PrePayCnyAmt.ValueText = objects.getData("PrePayCnyAmt");
        PrePayUsdAmt.ValueText = objects.getData("PrePayUsdAmt");
		PrePayJpyAmt.ValueText = objects.getData("PrePayJpyAmt");
		PrePayEurAmt.ValueText = objects.getData("PrePayEurAmt");
        PrePayOtherAmt.ValueText = objects.getData("PrePayOtherAmt");
		
		if (objects.getData("ChgPrePayTwd").Equals("Y")) { ChgPrePayTwd.Checked = true; } else { ChgPrePayTwd.Checked = false; }
        if (objects.getData("ChgPrePayCny").Equals("Y")) { ChgPrePayCny.Checked = true; } else { ChgPrePayCny.Checked = false; }
        if (objects.getData("ChgPrePayUsd").Equals("Y")) { ChgPrePayUsd.Checked = true; } else { ChgPrePayUsd.Checked = false; }
		if (objects.getData("ChgPrePayJpy").Equals("Y")) { ChgPrePayJpy.Checked = true; } else { ChgPrePayJpy.Checked = false; }
		if (objects.getData("ChgPrePayEur").Equals("Y")) { ChgPrePayEur.Checked = true; } else { ChgPrePayEur.Checked = false; }
        if (objects.getData("ChgPrePayOther").Equals("Y")) { ChgPrePayOther.Checked = true; } else { ChgPrePayOther.Checked = false; }
        ChgPrePayTwdAmt.ValueText = objects.getData("ChgPrePayTwdAmt");
        ChgPrePayCnyAmt.ValueText = objects.getData("ChgPrePayCnyAmt");
        ChgPrePayUsdAmt.ValueText = objects.getData("ChgPrePayUsdAmt");
		ChgPrePayJpyAmt.ValueText = objects.getData("ChgPrePayJpyAmt");
		ChgPrePayEurAmt.ValueText = objects.getData("ChgPrePayEurAmt");
        ChgPrePayOtherAmt.ValueText = objects.getData("ChgPrePayOtherAmt");

        //金額
        ActualPayTw.ValueText = objects.getData("ActualPayTw");
        ActualPayTwTw.ValueText = objects.getData("ActualPayTwTw");
        ActualPayUs.ValueText = objects.getData("ActualPayUs");
        ActualPayUsTw.ValueText = objects.getData("ActualPayUsTw");
        ActualPayJp.ValueText = objects.getData("ActualPayJp");
        ActualPayJpTw.ValueText = objects.getData("ActualPayJpTw");
        ActualPayKr.ValueText = objects.getData("ActualPayKr");
        ActualPayKrTw.ValueText = objects.getData("ActualPayKrTw");
        ActualPayCn.ValueText = objects.getData("ActualPayCn");
        ActualPayCnTw.ValueText = objects.getData("ActualPayCnTw");
        ActualPayMa.ValueText = objects.getData("ActualPayMa");
        ActualPayMaTw.ValueText = objects.getData("ActualPayMaTw");
        ActualPayOu.ValueText = objects.getData("ActualPayOu");
        ActualPayOuTw.ValueText = objects.getData("ActualPayOuTw");
        ActualPayOther.ValueText = objects.getData("ActualPayOther");
        ActualPayOtherTw.ValueText = objects.getData("ActualPayOtherTw");

        ActualAmount.ValueText = objects.getData("ActualAmount");          //總金額
        ActualApAmt.ValueText = objects.getData("ActualApAmt");            //應付金額
        ActualRtnApAmt.ValueText = objects.getData("ActualRtnApAmt");      //預支還回
        ActualRtnApDate.ValueText = objects.getData("ActualRtnApDate");    //還回日期
        FinDesc.ValueText = objects.getData("FinDesc");                    // 財務註記
		
		//20140122 系統自動加總各幣別金額 start 
		//if (ActualPayTw.ValueText.Equals(""))
		//{
			string[,] ids = null;
	        string sql = null;
	        DataSet ds = null;
			decimal amt_ttl = 0;
			string currency = "";
			int count = 0;
			decimal tempOtherSum = 0;
			decimal tempCAD = 0;
			decimal tempAUD = 0;
			decimal tempHKD = 0;
			decimal tempINR = 0;
			decimal tempPLN = 0;
			decimal tempTHB = 0;
			decimal tempSGD = 0;
			decimal tempOTHER = 0;
			
			try{
				//sql = " select  CAST(sum( CONVERT(decimal (6,2), OccurAmt)) as nvarchar(30))  amt_total , OccurCurrency";
				sql = " select   sum( CONVERT(decimal (12,2), OccurAmt))  amt_total , OccurCurrency";
	            sql += " from SmpForeignTrvlBillingDetail ";
	            sql += " where HeaderGUID = (select GUID from SmpForeignTrvlBilling where SheetNo='" + objects.getData("SheetNo") + "') ";
	            sql += "group by OccurCurrency ";
					   
				ds = engine.getDataSet(sql, "TEMP");
	            count = ds.Tables[0].Rows.Count;

	            for (int i = 0; i < count; i++)
	            {                
					amt_ttl  = Convert.ToDecimal(ds.Tables[0].Rows[i]["amt_total"]);
	                currency = ds.Tables[0].Rows[i]["OccurCurrency"].ToString();
					
					if (currency.Equals("TWD")){
						ActualPayTw.ValueText = Convert.ToString(amt_ttl);
					}
					else if  (currency.Equals("USD")){
						ActualPayUs.ValueText = Convert.ToString(amt_ttl);
					}
					else if (currency.Equals("JPY")){
						ActualPayJp.ValueText = Convert.ToString(amt_ttl);
					}
					else if (currency.Equals("KRW")){
						ActualPayKr.ValueText = Convert.ToString(amt_ttl);
					}
					else if (currency.Equals("CNY")){
						ActualPayCn.ValueText = Convert.ToString(amt_ttl);
					}
					else if (currency.Equals("MRY")){
						ActualPayMa.ValueText = Convert.ToString(amt_ttl);
					}
					else if (currency.Equals("TWD")){
						ActualPayMa.ValueText = Convert.ToString(amt_ttl);
					}
					else if (currency.Equals("EUR")){
						ActualPayOu.ValueText = Convert.ToString(amt_ttl);
					}else if (currency.Equals("CAD")){
						tempCAD = amt_ttl;
					}else if (currency.Equals("AUD")){
						tempAUD = amt_ttl;
					}else if (currency.Equals("HKD")){
						tempHKD = amt_ttl;
					}else if (currency.Equals("INR")){
						tempINR = amt_ttl;
					}else if (currency.Equals("PLN")){
						tempPLN = amt_ttl;
					}else if (currency.Equals("THB")){
						tempTHB = amt_ttl;
					}else if (currency.Equals("SGD")){
						tempSGD = amt_ttl;
					}else {
						tempOTHER = amt_ttl;
					}
					tempOtherSum = tempCAD + tempAUD + tempHKD + tempINR + tempPLN + tempTHB + tempSGD + tempOTHER;				
					ActualPayOther.ValueText = Convert.ToString(tempOtherSum);
	            }               	   
			}
			catch (Exception ze)
	        {
	          MessageBox(ze.Message);
	          writeLog(ze);
	        }
		//}
		//20140122 系統自動加總各幣別金額 end 

        DataObjectSet requestSet = null;
        requestSet = objects.getChild("SmpForeignTrvlBillingDetail");
        RequestList.dataSource = requestSet;
        RequestList.updateTable();

        for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
        {
            RequestList.dataSource.getAvailableDataObject(i).setData("SmpForeignTrvlBillingDetail", objects.getData("GUID"));
        }

        //差旅費報銷明細清單非新增時, 不可維護
        if (!isAddNew)
        {
            RequestList.NoAdd = true;
            RequestList.NoDelete = true;
            RequestList.NoModify = true;
            RequestList.NoAllDelete = true;
            RequestList.IsShowCheckBox = false;
        }
        
        if (!isAddNew)
        {
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            DeptGUID.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            Title.ReadOnly = true;
            AgentGUID.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            TrvlDesc.ReadOnly = true;
            StartTrvlDate.ReadOnly = true;
            EndTrvlDate.ReadOnly = true;
            FeeCharge1.ReadOnly = true;
            FeeCharge2.ReadOnly = true;
            FeeCharge3.ReadOnly = true;
            //FeeCharge4.ReadOnly = true;
			FeeCharge5.ReadOnly = true;
            FeeCharge6.ReadOnly = true;
            SiteUs.ReadOnly = true;
            SiteJp.ReadOnly = true;
            SiteKr.ReadOnly = true;
            SiteSub.ReadOnly = true;
            SiteOther.ReadOnly = true;
            SiteUsDesc.ReadOnly = true;
            SiteJpDesc.ReadOnly = true;
            SiteKrDesc.ReadOnly = true;
            SiteSubDesc.ReadOnly = true;
            SiteOtherDesc.ReadOnly = true;
            PrePayTwd.ReadOnly = true;
            PrePayCny.ReadOnly = true;
            PrePayUsd.ReadOnly = true;
			PrePayJpy.ReadOnly = true;
			PrePayEur.ReadOnly = true;
            PrePayOther.ReadOnly = true;
            PrePayTwdAmt.ReadOnly = true;
            PrePayCnyAmt.ReadOnly = true;
            PrePayUsdAmt.ReadOnly = true;
			PrePayJpyAmt.ReadOnly = true;
			PrePayEurAmt.ReadOnly = true;
            PrePayOtherAmt.ReadOnly = true;
            OriForeignForm.ReadOnly = true;
            ChgStartTrvlDate.ReadOnly = true;
            ChgEndTrvlDate.ReadOnly = true;
            ActualStartTrvlDate.ReadOnly = true;
            ActualEndTrvlDate.ReadOnly = true;

            OccurDate.ReadOnly = true;
            OccurAmt.ReadOnly = true;
            OccurCurrency.ReadOnly = true;
            OccurDesc.ReadOnly = true;
            PayClass.ReadOnly = true;

            ActualPayCn.ReadOnly = true;
            ActualPayCnTw.ReadOnly = true;
            ActualPayJp.ReadOnly = true;
            ActualPayJpTw.ReadOnly = true;
            ActualPayKr.ReadOnly = true;
            ActualPayKrTw.ReadOnly = true;
            ActualPayMa.ReadOnly = true;
            ActualPayMaTw.ReadOnly = true;
            ActualPayOther.ReadOnly = true;
            ActualPayOtherTw.ReadOnly = true;
            ActualPayOu.ReadOnly = true;
            ActualPayOuTw.ReadOnly = true;
            ActualPayTw.ReadOnly = true;
            ActualPayTwTw.ReadOnly = true;
            ActualPayUs.ReadOnly = true;
            ActualPayUsTw.ReadOnly = true;
			
			ChgPrePayTwd.ReadOnly = true;
            ChgPrePayCny.ReadOnly = true;
            ChgPrePayUsd.ReadOnly = true;
			ChgPrePayJpy.ReadOnly = true;
			ChgPrePayEur.ReadOnly = true;
            ChgPrePayOther.ReadOnly = true;
            ChgPrePayTwdAmt.ReadOnly = true;
            ChgPrePayCnyAmt.ReadOnly = true;
            ChgPrePayUsdAmt.ReadOnly = true;
			ChgPrePayJpyAmt.ReadOnly = true;
			ChgPrePayEurAmt.ReadOnly = true;
            ChgPrePayOtherAmt.ReadOnly = true;

            //DateTime sDate = Convert.ToDateTime(StartTrvlDate.ValueText);
            //DateTime eDate = Convert.ToDateTime(EndTrvlDate.ValueText);
            //TimeSpan ts = eDate - sDate ;
            //double days = ts.TotalDays + 1;
            //lbTrvlDateSumValue.Text = "出差天數：" + Convert.ToInt32(days).ToString() + " 天";
        }

        if (isAddNew)
        {
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            TrvlDesc.ReadOnly = true;
            StartTrvlDate.ReadOnly = true;
            EndTrvlDate.ReadOnly = true;
            ChgStartTrvlDate.ReadOnly = true;
            ChgEndTrvlDate.ReadOnly = true;
            SiteUs.ReadOnly = true;
            SiteJp.ReadOnly = true;
            SiteKr.ReadOnly = true;
            SiteSub.ReadOnly = true;
            SiteOther.ReadOnly = true;
            SiteUsDesc.ReadOnly = true;
            SiteJpDesc.ReadOnly = true;
            SiteKrDesc.ReadOnly = true;
            SiteSubDesc.ReadOnly = true;
            SiteOtherDesc.ReadOnly = true;
            FeeCharge1.ReadOnly = true;
            FeeCharge2.ReadOnly = true;
            FeeCharge3.ReadOnly = true;
            //FeeCharge4.ReadOnly = true;
			FeeCharge5.ReadOnly = true;
            FeeCharge6.ReadOnly = true;
            TrvlDesc.ReadOnly = true;
            ChgTrvlDesc.ReadOnly = true;
            PrePayTwd.ReadOnly = true;
            PrePayCny.ReadOnly = true;
            PrePayUsd.ReadOnly = true;
			PrePayJpy.ReadOnly = true;
			PrePayEur.ReadOnly = true;
            PrePayOther.ReadOnly = true;
            PrePayTwdAmt.ReadOnly = true;
            PrePayCnyAmt.ReadOnly = true;
            PrePayUsdAmt.ReadOnly = true;
			PrePayJpyAmt.ReadOnly = true;
			PrePayEurAmt.ReadOnly = true;
            PrePayOtherAmt.ReadOnly = true;
            ActualAmount.ReadOnly = true;
            ActualApAmt.ReadOnly = true;
            ActualRtnApAmt.ReadOnly = true;
            ActualRtnApDate.ReadOnly = true;
            FinDesc.ReadOnly = true;
			ChgPrePayTwd.ReadOnly = true;
            ChgPrePayCny.ReadOnly = true;
            ChgPrePayUsd.ReadOnly = true;
			ChgPrePayJpy.ReadOnly = true;
			ChgPrePayEur.ReadOnly = true;
            ChgPrePayOther.ReadOnly = true;
            ChgPrePayTwdAmt.ReadOnly = true;
            ChgPrePayCnyAmt.ReadOnly = true;
            ChgPrePayUsdAmt.ReadOnly = true;
			ChgPrePayJpyAmt.ReadOnly = true;
			ChgPrePayEurAmt.ReadOnly = true;
            ChgPrePayOtherAmt.ReadOnly = true;
        }

        if (actName.Equals("財務APOwner"))
        {
            ActualAmount.ReadOnly = false;
            ActualApAmt.ReadOnly = false;
            FinDesc.ReadOnly = false;
        }
        if (actName.Equals("財務出納"))
        {
            ActualRtnApAmt.ReadOnly = false;
            ActualRtnApDate.ReadOnly = false;
        }
		
		if (actName.Equals("董事長")) {            
			AddSignButton.Display = true; //允許加簽,
        }
		PrintButton1.Enabled = true;
        //sw.Close();
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("Title", Title.ValueText);
                objects.setData("AgentGUID", AgentGUID.GuidValueText);
                objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
                //勾稽原出差單據
                objects.setData("OriForeignForm", OriForeignForm.GuidValueText);
                
                // 預計出差日期
                objects.setData("StartTrvlDate", StartTrvlDate.ValueText);
                objects.setData("EndTrvlDate", EndTrvlDate.ValueText);
                objects.setData("ChgStartTrvlDate", ChgStartTrvlDate.ValueText);
                objects.setData("ChgEndTrvlDate", ChgEndTrvlDate.ValueText);
                //實際出差日期
                objects.setData("ActualStartTrvlDate", ActualStartTrvlDate.ValueText);
                objects.setData("ActualEndTrvlDate", ActualEndTrvlDate.ValueText);

                //出差地點
                if (SiteUs.Checked) { objects.setData("SiteUs", "Y"); } else { objects.setData("SiteUs", "N"); }
                if (SiteJp.Checked) { objects.setData("SiteJp", "Y"); } else { objects.setData("SiteJp", "N"); }
                if (SiteKr.Checked) { objects.setData("SiteKr", "Y"); } else { objects.setData("SiteKr", "N"); }
                if (SiteSub.Checked) { objects.setData("SiteSub", "Y"); } else { objects.setData("SiteSub", "N"); }
                if (SiteOther.Checked) { objects.setData("SiteOther", "Y"); } else { objects.setData("SiteOther", "N"); }
                objects.setData("SiteUsDesc", SiteUsDesc.ValueText);
                objects.setData("SiteJpDesc", SiteJpDesc.ValueText);
                objects.setData("SiteKrDesc", SiteKrDesc.ValueText);
                objects.setData("SiteSubDesc", SiteSubDesc.ValueText);
                objects.setData("SiteOtherDesc", SiteOtherDesc.ValueText);

                //FeeCharge 費用負擔
                string feeCharge = "";
				string feeName = "";
                if (FeeCharge1.Checked){
                    feeCharge = "0";  // 新普
					feeName = "新普";
				}
                if (FeeCharge2.Checked){
                    feeCharge = "1";  //STCS
					feeName = "STCS";
				}
                if (FeeCharge3.Checked){
                    feeCharge = "2"; //SCQ
					feeName = "SCQ";
				}
                //if (FeeCharge4.Checked)
                //    feeCharge = "3";  //HOPE
				if (FeeCharge5.Checked){
                    feeCharge = "4";  //TP
					feeName = "中普";
				}
				if (FeeCharge6.Checked){
                    feeCharge = "5";  //STTP
					feeName = "太普";
				}	
                objects.setData("FeeCharge", feeCharge);
                //出差（預辦）事由
                objects.setData("TrvlDesc", TrvlDesc.ValueText);
                objects.setData("ChgTrvlDesc", ChgTrvlDesc.ValueText); 

                //C 原預支申請
                if (PrePayTwd.Checked) { objects.setData("PrePayTwd", "Y"); } else { objects.setData("PrePayTwd", "N"); }
                if (PrePayCny.Checked) { objects.setData("PrePayCny", "Y"); } else { objects.setData("PrePayCny", "N"); }
                if (PrePayUsd.Checked) { objects.setData("PrePayUsd", "Y"); } else { objects.setData("PrePayUsd", "N"); }
				if (PrePayJpy.Checked) { objects.setData("PrePayJpy", "Y"); } else { objects.setData("PrePayJpy", "N"); }
				if (PrePayEur.Checked) { objects.setData("PrePayEur", "Y"); } else { objects.setData("PrePayEur", "N"); }
                if (PrePayOther.Checked) { objects.setData("PrePayOther", "Y"); } else { objects.setData("PrePayOther", "N"); }
                objects.setData("PrePayTwdAmt", PrePayTwdAmt.ValueText);
                objects.setData("PrePayCnyAmt", PrePayCnyAmt.ValueText);
                objects.setData("PrePayUsdAmt", PrePayUsdAmt.ValueText);
				objects.setData("PrePayJpyAmt", PrePayJpyAmt.ValueText);
				objects.setData("PrePayEurAmt", PrePayEurAmt.ValueText);
                objects.setData("PrePayOtherAmt", PrePayOtherAmt.ValueText);
				
				//C 異動預支申請
                if (ChgPrePayTwd.Checked) { objects.setData("ChgPrePayTwd", "Y"); } else { objects.setData("ChgPrePayTwd", "N"); }
                if (ChgPrePayCny.Checked) { objects.setData("ChgPrePayCny", "Y"); } else { objects.setData("ChgPrePayCny", "N"); }
                if (ChgPrePayUsd.Checked) { objects.setData("ChgPrePayUsd", "Y"); } else { objects.setData("ChgPrePayUsd", "N"); }
				if (ChgPrePayJpy.Checked) { objects.setData("ChgPrePayJpy", "Y"); } else { objects.setData("ChgPrePayJpy", "N"); }
				if (ChgPrePayEur.Checked) { objects.setData("ChgPrePayEur", "Y"); } else { objects.setData("ChgPrePayEur", "N"); }
                if (ChgPrePayOther.Checked) { objects.setData("ChgPrePayOther", "Y"); } else { objects.setData("ChgPrePayOther", "N"); }
                objects.setData("ChgPrePayTwdAmt", ChgPrePayTwdAmt.ValueText);
                objects.setData("ChgPrePayCnyAmt", ChgPrePayCnyAmt.ValueText);
                objects.setData("ChgPrePayUsdAmt", ChgPrePayUsdAmt.ValueText);
				objects.setData("ChgPrePayJpyAmt", ChgPrePayJpyAmt.ValueText);
				objects.setData("ChgPrePayEurAmt", ChgPrePayEurAmt.ValueText);
                objects.setData("ChgPrePayOtherAmt", ChgPrePayOtherAmt.ValueText);

                //objects.setData("PrePayComment", PrePayComment.ValueText);

                objects.setData("ActualPayTw", ActualPayTw.ValueText);
                objects.setData("ActualPayTwTw", ActualPayTwTw.ValueText);
                objects.setData("ActualPayUs", ActualPayUs.ValueText);
                objects.setData("ActualPayUsTw", ActualPayUsTw.ValueText);
                objects.setData("ActualPayJp", ActualPayJp.ValueText);
                objects.setData("ActualPayJpTw", ActualPayJpTw.ValueText);
                objects.setData("ActualPayKr", ActualPayKr.ValueText);
                objects.setData("ActualPayKrTw", ActualPayKrTw.ValueText);
                objects.setData("ActualPayCn", ActualPayCn.ValueText);
                objects.setData("ActualPayCnTw", ActualPayCnTw.ValueText);
                objects.setData("ActualPayMa", ActualPayMa.ValueText);
                objects.setData("ActualPayMaTw", ActualPayMaTw.ValueText);
                objects.setData("ActualPayOu", ActualPayOu.ValueText);
                objects.setData("ActualPayOuTw", ActualPayOuTw.ValueText);
                objects.setData("ActualPayOther", ActualPayOther.ValueText);
                objects.setData("ActualPayOtherTw", ActualPayOtherTw.ValueText);

                objects.setData("ActualAmount", ActualAmount.ValueText);
                objects.setData("ActualApAmt", ActualApAmt.ValueText);
                objects.setData("ActualRtnApAmt", ActualRtnApAmt.ValueText);
                objects.setData("FinDesc", FinDesc.ValueText);
				
				string trvlSiteName = "";
				if (SiteUs.Checked){
					trvlSiteName += "美國-" + SiteUsDesc.ValueText + ";  ";
				}
				if (SiteJp.Checked){
					trvlSiteName += "日本-" + SiteJpDesc.ValueText + ";  ";
				}
				if (SiteKr.Checked){
					trvlSiteName += "韓國-" + SiteKrDesc.ValueText + ";  ";
				}
				if (SiteSub.Checked){
					trvlSiteName += "子公司-" + SiteSubDesc.ValueText + ";  ";
				}
				if (SiteOther.Checked){
					trvlSiteName += "其他-" + SiteOtherDesc.ValueText + ";  ";
				}

                				
				string prePayName = "";
				if (PrePayTwd.Checked){
					prePayName += "新台幣-" + PrePayTwdAmt.ValueText + ";  ";
				}
				if (PrePayCny.Checked){
					prePayName += "人民幣-" + PrePayCnyAmt.ValueText + ";  ";
				}
				if (PrePayUsd.Checked){
					prePayName += "美金-" + PrePayUsdAmt.ValueText + ";  ";
				}
				if (PrePayJpy.Checked){
					prePayName += "日圓-" + PrePayJpyAmt.ValueText + ";  ";
				}
				if (PrePayEur.Checked){
					prePayName += "歐元-" + PrePayEurAmt.ValueText + ";  ";
				}
				if (PrePayOther.Checked){
					//prePayName += PrePayComment.ValueText +  ";  ";
					//prePayName += PrePayComment.ValueText + "-" + PrePayOtherAmt.ValueText + ";  ";
				}
				
				string chgPrePayName = "";
				if (ChgPrePayTwd.Checked){
					chgPrePayName += "新台幣-" + ChgPrePayTwdAmt.ValueText + ";  ";
				}
				if (ChgPrePayCny.Checked){
					chgPrePayName += "人民幣-" + ChgPrePayCnyAmt.ValueText + ";  ";
				}
				if (ChgPrePayUsd.Checked){
					chgPrePayName += "美金-" + ChgPrePayUsdAmt.ValueText + ";  ";
				}
				if (ChgPrePayJpy.Checked){
					chgPrePayName += "日圓-" + ChgPrePayJpyAmt.ValueText + ";  ";
				}
				if (ChgPrePayEur.Checked){
					chgPrePayName += "歐元-" + ChgPrePayEurAmt.ValueText + ";  ";
				}
				if (ChgPrePayOther.Checked){
					chgPrePayName += ChgPrePayOtherAmt.ValueText + ";  ";
					//chgPrePayName += ChgPrePayComment.ValueText + "-" + ChgPrePayOtherAmt.ValueText + ";  ";
				}
				
				objects.setData("TrvlSiteName", trvlSiteName);
				objects.setData("FeeName", feeName);
				objects.setData("PrePayName", prePayName);
				objects.setData("ChgPrePayName", chgPrePayName);
                
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
                //sw.WriteLine("主旨含單號");
            }
			
            objects.setData("PrePayTwdAmt", PrePayTwdAmt.ValueText);
            objects.setData("PrePayCnyAmt", PrePayCnyAmt.ValueText);
            objects.setData("PrePayUsdAmt", PrePayUsdAmt.ValueText);
			objects.setData("PrePayJpyAmt", PrePayJpyAmt.ValueText);
			objects.setData("PrePayEurAmt", PrePayEurAmt.ValueText);
            objects.setData("PrePayOtherAmt", PrePayOtherAmt.ValueText);
            objects.setData("ActualPayTw", ActualPayTw.ValueText);
            objects.setData("ActualPayTwTw", ActualPayTwTw.ValueText);
            objects.setData("ActualPayUs", ActualPayUs.ValueText);
            objects.setData("ActualPayUsTw", ActualPayUsTw.ValueText);
            objects.setData("ActualPayJp", ActualPayJp.ValueText);
            objects.setData("ActualPayJpTw", ActualPayJpTw.ValueText);
            objects.setData("ActualPayKr", ActualPayKr.ValueText);
            objects.setData("ActualPayKrTw", ActualPayKrTw.ValueText);
            objects.setData("ActualPayCn", ActualPayCn.ValueText);
            objects.setData("ActualPayCnTw", ActualPayCnTw.ValueText);
            objects.setData("ActualPayMa", ActualPayMa.ValueText);
            objects.setData("ActualPayMaTw", ActualPayMaTw.ValueText);
            objects.setData("ActualPayOu", ActualPayOu.ValueText);
            objects.setData("ActualPayOuTw", ActualPayOuTw.ValueText);
            objects.setData("ActualPayOther", ActualPayOther.ValueText);
            objects.setData("ActualPayOtherTw", ActualPayOtherTw.ValueText);

            objects.setData("ActualAmount", ActualAmount.ValueText);
            objects.setData("ActualApAmt", ActualApAmt.ValueText);
            objects.setData("ActualRtnApAmt", ActualRtnApAmt.ValueText);
            objects.setData("FinDesc", FinDesc.ValueText);
            objects.setData("StartTrvlDate", StartTrvlDate.ValueText);
            objects.setData("EndTrvlDate", EndTrvlDate.ValueText);
			objects.setData("ActualRtnApDate", ActualRtnApDate.ValueText);			
            //objects.setData("PrePayComment", PrePayComment.ValueText);

            for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
            {
                RequestList.dataSource.getAvailableDataObject(i).setData("HeaderGUID", objects.getData("GUID"));
            }
            
            //beforeSetFlow
            setSession("IsSetFlow", "Y");
            
        }
        catch (Exception e)
        {
            writeLog(e);
        }        
    }

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
        //sw.WriteLine("checkFieldData ");
        
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);

        bool isAddNew = base.isNew(); //base 父類別
        if (isAddNew)
        {
            string actualStartTrvlDate = ActualStartTrvlDate.ValueText;
            string actualEndTrvlDate = ActualEndTrvlDate.ValueText;
            string chgStartTrvlDate = ChgStartTrvlDate.ValueText;
            string chgEndTrvlDate = ChgEndTrvlDate.ValueText;
            string startTrvlDate = StartTrvlDate.ValueText;
            string endTrvlDate = EndTrvlDate.ValueText;
			
			if (OriginatorGUID.ValueText.Equals(AgentGUID.ValueText))
            {
                strErrMsg += "代理人不可為申請人!\n";
            }
            if (OriginatorGUID.ValueText.Equals(CheckByGUID.ValueText))
            {
                strErrMsg += "審核人員不可為出差人員\n";
            }
            if (OriForeignForm.Equals(""))
            {
                strErrMsg += "請勾稽出差申請單號\n";
            }

            if (actualStartTrvlDate.Equals("") || actualEndTrvlDate.Equals(""))
            {
                strErrMsg += "請維護實際出差日期\n";
            }
            else 
            {
                if (!chgStartTrvlDate.Equals(""))
                {
                    if (!chgStartTrvlDate.Equals(actualStartTrvlDate) || !chgEndTrvlDate.Equals(actualEndTrvlDate))
                    {
                        strErrMsg += "實際出差日與出差異動單據出差日期不同, 請先填寫異動單, 再填寫報銷單!\n";
                    }
                }
                else {
                    if (!startTrvlDate.Equals(actualStartTrvlDate) || !endTrvlDate.Equals(actualEndTrvlDate))
                    {
                        strErrMsg += "實際出差日與出差日期不同, 請先填寫異動單, 再填寫報銷單!\n";
                    }
                }
            }
            //檢查 差旅費報銷明細清單 資料
            DataObjectSet chkType = RequestList.dataSource;
            if (chkType.getAvailableDataObjectCount().Equals(0))
            {
                strErrMsg += "請輸入差旅費明細內容(日期、類別、金額、幣別、摘要)!\n";
            }

            //設定主旨
            //if (Subject.ValueText.Equals(""))
            //{
                values = base.getUserInfoById(engine, OriginatorGUID.ValueText);
                string subject = "【國外出差差旅費報支 出差人員：" + values[1] + "】";
                //if (Subject.ValueText.Equals(""))
                //{
                    Subject.ValueText = subject;
                //}
            //}

            
        }

        //AP人員維護欄位
        if (actName.Equals("財務APOwner"))
        { 
            
        }

        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }
        //sw.Close();
        return result;        
    }

    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人, 登入者
        si.fillerName = (string)Session["UserName"]; //填表人姓名
        si.fillerOrgID = depData[0]; 
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        //si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = (string)Session["UserName"];
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
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
        //string[] depDataRealtionship = getDeptInfo(engine, (string)OriginatorGUID.GuidValueText);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
        //si.ownerID = OriginatorGUID.ValueText; //申請人id
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;  //申請人名稱
        //depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
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
		string[] values = null;
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string isChairman = ""; //是否下一關為董事長			
			//代理人, 使用表單中定義checkby2
			string agentGUID = currentObject.getData("AgentGUID");
			values = base.getUserInfo(engine, agentGUID);
			string agentId = values[0];
			
			string notifierId = agentId + ";"; //通知人員
			string deptAsstId = ""; //部門收發
	        string[] deptInfo = base.getDeptInfo(engine, currentObject.getData("OriginatorGUID"));
	        if (!deptInfo[0].Equals(""))
	        {
	            string[] userFunc = getUserRoles(engine, "部門收發", deptInfo[0]);
	            deptAsstId = userFunc[2];
	        }			

            //填表人
            string creatorId = si.fillerID;
        
            //出差人員
			string originatorGUID = currentObject.getData("OriginatorGUID");
			values = base.getUserInfo(engine, originatorGUID);
			string originatorId = values[0];
        
            //出差人員的主管
            values = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = values[0];
            
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];

            if (managerId.Equals("1356"))
            {
                isChairman = "Y";
            }
						
            //審核人員
            string checkByGUID = currentObject.getData("CheckByGUID");
			string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }              
            
            //填表人不等於請假人員則通知
            if (!creatorId.Equals(originatorId))
            {
                notifierId += originatorId + (";");
            }
            //通知部門助理
            if (!deptAsstId.Equals(""))
            {
                notifierId += deptAsstId + (";");
            }

            //string strSSP = "N";     //是否為替代役人員, 預設為N
            //select PALSMA014 from SMP_CMSMV where MV001 = '3787' 進入鼎新中取得人員是否為替代役人員
            /*
			string strPreDoc = "N";  //是否預辦證件
            if (Passport.Checked.Equals(true) || MTPs.Checked.Equals(true) || MtpsPlus.Checked.Equals(true) || USvisa.Checked.Equals(true) || Other.Checked.Equals(true))
            {
                strPreDoc = "Y";
            }
			*/
            string strNotify1 = "";  //
            string strNotify2 = "";  //通知秘書、代理人
            if (!agentGUID.Equals(""))
            {
                strNotify2 += agentId + (";2506;");
            }
            string strNotify3 = "";  //印出來給董事長簽核, 通知填表人、申請人、部門收發
            //填表人不等於出差人員則通知
            if (!creatorId.Equals(originatorId))
            {
                strNotify3 += originatorId + (";");
            }
            //通知部門助理
            if (!deptAsstId.Equals(""))
            {
                strNotify3 += deptAsstId + (";");
            }
            //string strTrvlSite = "1";//費用付擔, 判斷是否加簽各公司別最高主管 

            //sw.WriteLine("managerId=" + managerId);
            xml += "<SPAD007>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";            //填表人 
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";   //出差人員
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";          //審核人
            xml += "<checkby2 DataType=\"java.lang.String\"></checkby2>";                           //CheckBy2, 預留欄位
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";            //直屬主管
            xml += "<notify1 DataType=\"java.lang.String\">" + strNotify1 + "</notify1>";           //通知人員1
            xml += "<notify2 DataType=\"java.lang.String\">" + strNotify2 + "</notify2>";           //通知人員2
            xml += "<casher DataType=\"java.lang.String\">SMP-CASHER</casher>";                   //財務出納
            xml += "<finap DataType=\"java.lang.String\">SMP-FINAPOWNER</finap>";                   //財務AP負責人
            xml += "<chairman DataType=\"java.lang.String\">1356</chairman>";                       //董事長
            xml += "<flag1 DataType=\"java.lang.String\">" + isChairman + "</flag1>";               //flag1, 若主管為董事長, 則直屬
            xml += "</SPAD007>";
            
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        
        //表單代號
        param["SPAD007"] = xml;
        return "SPAD007";
    }

    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
        //sw.WriteLine("OriginatorGUID");

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                
        //更新代理人
        string[] substituteValues = base.getSubstituteUserInfo(engine, OriginatorGUID.GuidValueText);
        if (substituteValues[0] != "")
        {
            AgentGUID.GuidValueText = substituteValues[0];
            AgentGUID.ValueText = substituteValues[1];
            AgentGUID.ReadOnlyValueText = substituteValues[2];
        }
        else
        {
            AgentGUID.GuidValueText = "";
            AgentGUID.ValueText = "";
            AgentGUID.ReadOnlyValueText = "";
        }

        //更新職稱
        string[] userFunctionValues = base.getUserFunctions(engine, OriginatorGUID.GuidValueText);
        if (userFunctionValues[0] != "")
        {
            Title.ValueText = userFunctionValues[3];
        }
        else
        {
            Title.ValueText = "";
        }

        //更新出差人員主要部門
        string[] userDeptValues = base.getDeptInfo(engine, OriginatorGUID.GuidValueText);
        if (userDeptValues[0] != "")
        {            
            DeptGUID.ValueText = userDeptValues[0];
            DeptGUID.doValidate();
			//公司別
			string[] userValues = base.getUserInfoById(engine, OriginatorGUID.ValueText);
	        CompanyCode.ValueText = userValues[5];			
        }
        else
        {
            DeptGUID.ValueText = "";
			CompanyCode.ValueText = "";
        }

        //若只有一張單據,則由工號帶出所有對應資料
        //由原出差單據帶出預沖銷的出差單或異動單
		bool isAddNew = base.isNew();
		if (isAddNew)
		{
			OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "' and SMWYAAA020='Y' and (BillFormStatus<>'Y' or  BillFormStatus<>'I' or BillFormStatus is null)   ";
		}
        //OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "'";  
		//OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "' and SMWYAAA020='Y' and (BillID is null or BillID='') ";
		//OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "' and SMWYAAA020='Y' and (BillFormStatus<>'Y' or  BillFormStatus<>'I')   ";
        //sw.Close();
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
        string actName = Convert.ToString(getSession("ACTName").ToString());
        
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
        if (signProcess.Equals("N")) //不同意
        {
            //updateSourceStatus("Terminate");
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
        base.reGetProcedure();
    }

    /// <summary>
    /// 撤銷程序
    /// </summary>
    protected override void withDrawProcedure()
    {
        //call oracle function set object workflow status to 'WithDraw'        
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
        //string signProcess = Convert.ToString(Session["signProcess"]);
        
        base.afterApprove(engine, currentObject, result);
    }


	protected void RequestList_ShowRowData(DataObject objects)
    {
        OccurDate.ValueText = objects.getData("OccurDate");
        PayClass.ValueText = objects.getData("PayClass");
        OccurAmt.ValueText = objects.getData("OccurAmt");
        OccurCurrency.ValueText = objects.getData("OccurCurrency");
        OccurDesc.ValueText = objects.getData("OccurDesc");
    }

    protected bool RequestList_SaveRowData(DataObject objects, bool isNew)
    {        
        if (OccurDate.ValueText.Equals(""))
        {
            MessageBox("請輸入差旅費明細內容-日期");
            return false;
        }
        if (PayClass.ValueText.Equals(""))
        {
            MessageBox("請輸入差旅費明細內容-類別");
            return false;
        }
        if (OccurAmt.ValueText.Equals(""))
		{
            MessageBox("請輸入差旅費明細內容-金額");
            return false;
		}
        if (OccurCurrency.ValueText.Equals(""))
        {
            MessageBox("請輸入差旅費明細內容-幣別");
            return false;
        }
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("HeaderGUID", "TEMP");
            objects.setData("OccurDate", OccurDate.ValueText);
            objects.setData("PayClass", PayClass.ValueText);
            objects.setData("OccurAmt", OccurAmt.ValueText);
            objects.setData("OccurCurrency", OccurCurrency.ValueText);
            objects.setData("OccurDesc", OccurDesc.ValueText);

            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        return true;
    }


    /// <summary>
    /// 利用SheetNo, 取得原出差相關單據資訊
    /// </summary>
    /// <param name="values"></param>
    protected void OriForeignForm_SingleOpenWindowButtonClick(string[,] values)
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
        //sw.WriteLine("OriForeignForm_SingleOpenWindowButtonClick");

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
		string sql = "";
		string strFormId = OriForeignForm.ValueText.Substring(0, 7);
		//MessageBox("strFormId:" + strFormId);
		
		if (strFormId.Equals("SPAD004")){
			sql = " select AgentGUID, CheckByGUID, StartTrvlDate, EndTrvlDate, SiteUs, SiteJp, SiteKr, SiteSub, SiteOther, SiteUsDesc, SiteJpDesc, SiteKrDesc, SiteSubDesc, SiteOtherDesc, ";
			sql += " FeeCharge, TrvlDesc, PrePayTwd, PrePayTwdAmt, PrePayCny, PrePayCnyAmt, PrePayUsd, PrePayUsdAmt, PrePayOther, PrePayOtherAmt, SheetNo, ";			
			sql += " '' as ChgStartTrvlDate, '' as ChgEndTrvlDate, '' as ChgTrvlDesc , ";
			sql += " '' as ChgPrePayTwd, '' as ChgPrePayTwdAmt, '' as ChgPrePayCny, '' as ChgPrePayCnyAmt, '' as ChgPrePayUsd, '' as ChgPrePayUsdAmt, '' as ChgPrePayOther, '' as ChgPrePayOtherAmt";			
			sql += " , PrePayJpy, PrePayJpyAmt, PrePayEur, PrePayEurAmt, '' as ChgPrePayJpy, '' as ChgPrePayJpyAmt , '' as ChgPrePayEur, '' as ChgPrePayEurAmt From SmpForeignTrvl where GUID='" + OriForeignForm.GuidValueText + "' ";
		}
		if (strFormId.Equals("SPAD005")){
			sql = " select AgentGUID, CheckByGUID, StartTrvlDate, EndTrvlDate, SiteUs, SiteJp, SiteKr, SiteSub, SiteOther, SiteUsDesc, SiteJpDesc, SiteKrDesc, SiteSubDesc, SiteOtherDesc, ";
			sql += " FeeCharge, TrvlDesc, PrePayTwd, PrePayTwdAmt, PrePayCny, PrePayCnyAmt, PrePayUsd, PrePayUsdAmt, PrePayOther, PrePayOtherAmt, SheetNo, ";
			sql += " ChgStartTrvlDate, ChgEndTrvlDate, ChgTrvlDesc , ";
			sql += " ChgPrePayTwd,ChgPrePayTwdAmt,ChgPrePayCny,ChgPrePayCnyAmt,ChgPrePayUsd,ChgPrePayUsdAmt,ChgPrePayOther,ChgPrePayOtherAmt ";
			sql += " , PrePayJpy, PrePayJpyAmt, PrePayEur, PrePayEurAmt, ChgPrePayJpy, ChgPrePayJpyAmt , ChgPrePayEur, ChgPrePayEurAmt From SmpForeignTrvlChg where GUID='" + OriForeignForm.GuidValueText + "' ";
		}

        
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[40];
        string temp = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            AgentGUID.GuidValueText = ds.Tables[0].Rows[0]["AgentGUID"].ToString();
            AgentGUID.doGUIDValidate();
            temp = ds.Tables[0].Rows[0]["CheckByGUID"].ToString();
            if (!temp.Equals(""))
            {
                CheckByGUID.ValueText = ds.Tables[0].Rows[0]["CheckByGUID"].ToString();
                CheckByGUID.doGUIDValidate();
            }
            StartTrvlDate.ValueText = ds.Tables[0].Rows[0]["StartTrvlDate"].ToString();
            EndTrvlDate.ValueText = ds.Tables[0].Rows[0]["EndTrvlDate"].ToString();
            temp = ds.Tables[0].Rows[0]["SiteUs"].ToString();
            if (temp.Equals("Y")) { SiteUs.Checked = true; } else { SiteUs.Checked = false; }
            temp = ds.Tables[0].Rows[0]["SiteJp"].ToString();
            if (temp.Equals("Y")) { SiteJp.Checked = true; } else { SiteJp.Checked = false; }
            temp = ds.Tables[0].Rows[0]["SiteKr"].ToString();
            if (temp.Equals("Y")) { SiteKr.Checked = true; } else { SiteKr.Checked = false; }
            temp = ds.Tables[0].Rows[0]["SiteSub"].ToString();
            if (temp.Equals("Y")) { SiteSub.Checked = true; } else { SiteSub.Checked = false; }
            temp = ds.Tables[0].Rows[0]["SiteOther"].ToString();
            if (temp.Equals("Y")) { SiteOther.Checked = true; } else { SiteOther.Checked = false; }
            SiteUsDesc.ValueText = ds.Tables[0].Rows[0]["SiteUsDesc"].ToString();
            SiteJpDesc.ValueText = ds.Tables[0].Rows[0]["SiteJpDesc"].ToString();
            SiteKrDesc.ValueText = ds.Tables[0].Rows[0]["SiteKrDesc"].ToString();
            SiteSubDesc.ValueText = ds.Tables[0].Rows[0]["SiteSubDesc"].ToString();
            SiteOtherDesc.ValueText = ds.Tables[0].Rows[0]["SiteOtherDesc"].ToString();
			
			//MessageBox("SiteJpDesc.ValueText :" + SiteJpDesc.ValueText);

            string feeCharge = Convert.ToString(ds.Tables[0].Rows[0]["FeeCharge"].ToString());
            switch (feeCharge)
            {
                case "0":
                    FeeCharge1.Checked = true;
                    break;
                case "1":
                    FeeCharge2.Checked = true;
                    break;
                case "2":
                    FeeCharge3.Checked = true;
                    break;
                //case "3":
                //    FeeCharge4.Checked = true;
                //    break;
				case "4":
                    FeeCharge5.Checked = true;
                    break;
                case "5":
                    FeeCharge6.Checked = true;
                    break;	
                default:
                    break;
            }

            TrvlDesc.ValueText = ds.Tables[0].Rows[0]["TrvlDesc"].ToString();

            temp = ds.Tables[0].Rows[0]["PrePayTwd"].ToString();
            if (temp.Equals("Y")) { PrePayTwd.Checked = true; } else { PrePayTwd.Checked = false; }
            PrePayTwdAmt.ValueText = ds.Tables[0].Rows[0]["PrePayTwdAmt"].ToString();
            temp = ds.Tables[0].Rows[0]["PrePayCny"].ToString();
            if (temp.Equals("Y")) { PrePayCny.Checked = true; } else { PrePayCny.Checked = false; }
            PrePayCnyAmt.ValueText = ds.Tables[0].Rows[0]["PrePayCnyAmt"].ToString();
            temp = ds.Tables[0].Rows[0]["PrePayUsd"].ToString();
            if (temp.Equals("Y")) { PrePayUsd.Checked = true; } else { PrePayUsd.Checked = false; }
            PrePayUsdAmt.ValueText = ds.Tables[0].Rows[0]["PrePayUsdAmt"].ToString();
			
			temp = ds.Tables[0].Rows[0]["PrePayJpy"].ToString();
            if (temp.Equals("Y")) { PrePayJpy.Checked = true; } else { PrePayJpy.Checked = false; }
            PrePayJpyAmt.ValueText = ds.Tables[0].Rows[0]["PrePayJpyAmt"].ToString();
			
			temp = ds.Tables[0].Rows[0]["PrePayEur"].ToString();
            if (temp.Equals("Y")) { PrePayEur.Checked = true; } else { PrePayEur.Checked = false; }
            PrePayEurAmt.ValueText = ds.Tables[0].Rows[0]["PrePayEurAmt"].ToString();
            
			temp = ds.Tables[0].Rows[0]["PrePayOther"].ToString();
            if (temp.Equals("Y")) { PrePayOther.Checked = true; } else { PrePayOther.Checked = false; }
            PrePayOtherAmt.ValueText = ds.Tables[0].Rows[0]["PrePayOtherAmt"].ToString();
            			
			ChgStartTrvlDate.ValueText = ds.Tables[0].Rows[0]["ChgStartTrvlDate"].ToString();
            ChgEndTrvlDate.ValueText = ds.Tables[0].Rows[0]["ChgEndTrvlDate"].ToString();
			ChgTrvlDesc.ValueText = ds.Tables[0].Rows[0]["ChgTrvlDesc"].ToString();
			
			temp = ds.Tables[0].Rows[0]["ChgPrePayTwd"].ToString();
            if (temp.Equals("Y")) { ChgPrePayTwd.Checked = true; } else { ChgPrePayTwd.Checked = false; }
            ChgPrePayTwdAmt.ValueText = ds.Tables[0].Rows[0]["ChgPrePayTwdAmt"].ToString();
            temp = ds.Tables[0].Rows[0]["ChgPrePayCny"].ToString();
            if (temp.Equals("Y")) { ChgPrePayCny.Checked = true; } else { ChgPrePayCny.Checked = false; }
            ChgPrePayCnyAmt.ValueText = ds.Tables[0].Rows[0]["ChgPrePayCnyAmt"].ToString();
            temp = ds.Tables[0].Rows[0]["ChgPrePayUsd"].ToString();
            if (temp.Equals("Y")) { ChgPrePayUsd.Checked = true; } else { ChgPrePayUsd.Checked = false; }
            ChgPrePayUsdAmt.ValueText = ds.Tables[0].Rows[0]["ChgPrePayUsdAmt"].ToString();
            
			temp = ds.Tables[0].Rows[0]["ChgPrePayJpy"].ToString();
            if (temp.Equals("Y")) { ChgPrePayJpy.Checked = true; } else { ChgPrePayJpy.Checked = false; }
            ChgPrePayJpyAmt.ValueText = ds.Tables[0].Rows[0]["ChgPrePayJpyAmt"].ToString();
            
			temp = ds.Tables[0].Rows[0]["ChgPrePayEur"].ToString();
            if (temp.Equals("Y")) { ChgPrePayEur.Checked = true; } else { ChgPrePayEur.Checked = false; }
            ChgPrePayEurAmt.ValueText = ds.Tables[0].Rows[0]["ChgPrePayEurAmt"].ToString();
            
			
			temp = ds.Tables[0].Rows[0]["ChgPrePayOther"].ToString();
            if (temp.Equals("Y")) { ChgPrePayOther.Checked = true; } else { ChgPrePayOther.Checked = false; }
            ChgPrePayOtherAmt.ValueText = ds.Tables[0].Rows[0]["ChgPrePayOtherAmt"].ToString();

            //OriForeignForm..ValueText = ds.Tables[0].Rows[0]["SheetNo"].ToString();
            //OriForeignForm.doValidate();
        }
        else
        {
            AgentGUID.ValueText = "";
            StartTrvlDate.ValueText = "";
            EndTrvlDate.ValueText = "";
            SiteUs.Checked = false;
            SiteKr.Checked = false;
            SiteSub.Checked = false;
            SiteJp.Checked = false;
            SiteOther.Checked = false;
            SiteUsDesc.ValueText = "";
            SiteJpDesc.ValueText = "";
            SiteKrDesc.ValueText = "";
            SiteSubDesc.ValueText = "";
            SiteOtherDesc.ValueText = "";
            FeeCharge1.Checked = false;
            FeeCharge2.Checked = false;
            FeeCharge3.Checked = false;
            //FeeCharge4.Checked = false;
			FeeCharge5.Checked = false;
            FeeCharge6.Checked = false;
            TrvlDesc.ValueText = "";
            PrePayTwd.Checked = false;
            PrePayCny.Checked = false;
            PrePayUsd.Checked = false;
			PrePayJpy.Checked = false;
			PrePayEur.Checked = false;
            PrePayOther.Checked = false;
			PrePayTwdAmt.ValueText = "";
			PrePayCnyAmt.ValueText = "";
			PrePayUsdAmt.ValueText = "";
			PrePayJpyAmt.ValueText = "";
			PrePayEurAmt.ValueText = "";
            PrePayOtherAmt.ValueText = "";
			ChgStartTrvlDate.ValueText = "";
            ChgEndTrvlDate.ValueText = "";
			ChgTrvlDesc.ValueText = "";
			ChgPrePayTwd.Checked = false;
            ChgPrePayCny.Checked = false;
            ChgPrePayUsd.Checked = false;
			ChgPrePayJpy.Checked = false;
			ChgPrePayEur.Checked = false;
            ChgPrePayOther.Checked = false;
			ChgPrePayTwdAmt.ValueText = "";
			ChgPrePayCnyAmt.ValueText = "";
			ChgPrePayUsdAmt.ValueText = "";
			ChgPrePayJpyAmt.ValueText = "";
			ChgPrePayEurAmt.ValueText = "";
            ChgPrePayOtherAmt.ValueText = "";

        }
        //sw.Close();
    }

    /// <summary>
    /// 設定出差人員對應沖銷單據資料
    /// </summary>
    private void setOriginatorDefaultValue()
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
        //sw.WriteLine("setOriginatorDefaultValue");

        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals(""))
        {
            AbstractEngine engine = null;
            try
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                
                string sql = " select top 1 AgentGUID, CheckByGUID, StartTrvlDate, EndTrvlDate, SiteUs, SiteJp, SiteKr, SiteSub, SiteOther, SiteUsDesc, SiteJpDesc, SiteKrDesc, SiteSubDesc, SiteOtherDesc, ";
                sql += " FeeCharge, TrvlDesc, PrePayTwd, PrePayTwdAmt, PrePayCny, PrePayCnyAmt, PrePayUsd, PrePayUsdAmt, PrePayOther, PrePayOtherAmt, SheetNo ";
                sql += " From SmpForeignTrvl where OriginatorGUID='" + OriginatorGUID.GuidValueText + "' ";
                //MessageBox("Value : " + OriForeignForm.GuidValueText);
                //MessageBox("setOriginatorDefaultValue:");
                //sw.WriteLine("SQL = " + sql );

                DataSet ds = engine.getDataSet(sql, "TEMP");
                string[] result = new string[26];
                string temp = "";
                //sw.WriteLine("Count = " + ds.Tables[0].Rows.Count);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    AgentGUID.GuidValueText = ds.Tables[0].Rows[0]["AgentGUID"].ToString();
                    AgentGUID.doValidate();
                    CheckByGUID.GuidValueText = ds.Tables[0].Rows[0]["CheckByGUID"].ToString();
                    CheckByGUID.doValidate();
                    StartTrvlDate.ValueText = ds.Tables[0].Rows[0]["StartTrvlDate"].ToString();
                    EndTrvlDate.ValueText = ds.Tables[0].Rows[0]["EndTrvlDate"].ToString();

                   // sw.WriteLine("EndTrvlDate = " + EndTrvlDate.ValueText);

                    temp = ds.Tables[0].Rows[0]["SiteUs"].ToString();
                    if (temp.Equals("Y")) { SiteUs.Checked = true; } else { SiteUs.Checked = false; }
                    temp = ds.Tables[0].Rows[0]["SiteJp"].ToString();
                    if (temp.Equals("Y")) { SiteJp.Checked = true; } else { SiteJp.Checked = false; }
                    temp = ds.Tables[0].Rows[0]["SiteKr"].ToString();
                    if (temp.Equals("Y")) { SiteKr.Checked = true; } else { SiteKr.Checked = false; }
                    temp = ds.Tables[0].Rows[0]["SiteSub"].ToString();
                    if (temp.Equals("Y")) { SiteSub.Checked = true; } else { SiteSub.Checked = false; }
                    temp = ds.Tables[0].Rows[0]["SiteOther"].ToString();
                    if (temp.Equals("Y")) { SiteOther.Checked = true; } else { SiteOther.Checked = false; }
                    SiteUsDesc.ValueText = ds.Tables[0].Rows[0]["SiteUsDesc"].ToString();
                    SiteJpDesc.ValueText = ds.Tables[0].Rows[0]["SiteJpDesc"].ToString();
                    SiteKrDesc.ValueText = ds.Tables[0].Rows[0]["SiteKrDesc"].ToString();
                    SiteSubDesc.ValueText = ds.Tables[0].Rows[0]["SiteSubDesc"].ToString();
                    SiteOtherDesc.ValueText = ds.Tables[0].Rows[0]["SiteOtherDesc"].ToString();

                    string feeCharge = Convert.ToString(ds.Tables[0].Rows[0]["FeeCharge"].ToString());
                    switch (feeCharge)
                    {
                        case "0":
                            FeeCharge1.Checked = true;
                            break;
                        case "1":
                            FeeCharge2.Checked = true;
                            break;
                        case "2":
                            FeeCharge3.Checked = true;
                            break;
                        //;case "3":
                        //    FeeCharge4.Checked = true;
                        //    break;
						case "4":
                            FeeCharge5.Checked = true;
                            break;
                        case "5":
                            FeeCharge6.Checked = true;
                            break;	
                        default:
                            break;
                    }

                    TrvlDesc.ValueText = ds.Tables[0].Rows[0]["TrvlDesc"].ToString();

                    temp = ds.Tables[0].Rows[0]["PrePayTwd"].ToString();
                    if (temp.Equals("Y")) { PrePayTwd.Checked = true; } else { PrePayTwd.Checked = false; }

                    PrePayTwdAmt.ValueText = ds.Tables[0].Rows[0]["PrePayTwdAmt"].ToString();
                    temp = ds.Tables[0].Rows[0]["PrePayCny"].ToString();
                    if (temp.Equals("Y")) { PrePayCny.Checked = true; } else { PrePayCny.Checked = false; }
                    PrePayCnyAmt.ValueText = ds.Tables[0].Rows[0]["PrePayCnyAmt"].ToString();
                    temp = ds.Tables[0].Rows[0]["PrePayUsd"].ToString();
                    if (temp.Equals("Y")) { PrePayUsd.Checked = true; } else { PrePayUsd.Checked = false; }
                    PrePayUsdAmt.ValueText = ds.Tables[0].Rows[0]["PrePayUsdAmt"].ToString();
                    temp = ds.Tables[0].Rows[0]["PrePayOther"].ToString();
                    if (temp.Equals("Y")) { PrePayOther.Checked = true; } else { PrePayOther.Checked = false; }
                    PrePayOtherAmt.ValueText = ds.Tables[0].Rows[0]["PrePayOtherAmt"].ToString();
                    //OriForeignForm.ValueText = ds.Tables[0].Rows[0]["SheetNo"].ToString();
                    //OriForeignForm.doValidate();
                    //sw.WriteLine("SheetNo : " + ds.Tables[0].Rows[0]["SheetNo"].ToString());
                    //sw.WriteLine("OriForeignForm ValueText: " + OriForeignForm.ValueText);
                    //sw.WriteLine("OriForeignForm GuidValueText : " + OriForeignForm.GuidValueText);
                }
                else
                {
                    AgentGUID.ValueText = "";
                    StartTrvlDate.ValueText = "";
                    EndTrvlDate.ValueText = "";
                    SiteUs.Checked = false;
                    SiteKr.Checked = false;
                    SiteSub.Checked = false;
                    SiteJp.Checked = false;
                    SiteOther.Checked = false;
                    SiteUsDesc.ValueText = "";
                    SiteJpDesc.ValueText = "";
                    SiteKrDesc.ValueText = "";
                    SiteSubDesc.ValueText = "";
                    SiteOtherDesc.ValueText = "";
                    FeeCharge1.Checked = false;
                    FeeCharge2.Checked = false;
                    FeeCharge3.Checked = false;
                    //FeeCharge4.Checked = false;
					FeeCharge5.Checked = false;
                    FeeCharge6.Checked = false;
                    TrvlDesc.ValueText = "";
                    PrePayTwd.Checked = false;
                    PrePayCny.Checked = false;
                    PrePayUsd.Checked = false;
                    PrePayOther.Checked = false;
                    PrePayOtherAmt.ValueText = "";

                }

                
            }
            catch (Exception e)
            {
                base.writeLog(e);
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
                //if (sw != null) sw.Close();
            }
        }
    }

    protected void CheckByGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckByGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void AgentGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            AgentGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void OriginatorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriginatorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
	
	protected void PrintButton_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["SPAD007_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印國外出差差旅費報銷單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }
	
	protected void OriForeignForm_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriForeignForm.whereClause = "( BillFormStatus is null and SMWYAAA020='Y' and UserId='"+ OriginatorGUID.ValueText +"') ";
        }
    }

}