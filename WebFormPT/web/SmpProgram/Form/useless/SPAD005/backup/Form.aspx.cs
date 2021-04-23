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


public partial class SmpProgram_System_Form_SPAD005_Form : SmpAdFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPAD005"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD005.SmpForeignTrvlChgAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true);
		//sw.WriteLine("initUI ");
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;
        IsIncludeDateEve.Display = false;
        TempSerialNo.Display = false;
        ClassCode.Display = false;
        CategoryCode.Display = false;
        IsIncludeHoliday.Display = false;
		lbHrTrvlDesc.Display = false;
        lbHrTrvlDate.Display = false;
		HrStartTrvlDate.Display = false;
        HrEndTrvlDate.Display = false;

        //Radio Group, Change Type
        ChangeType1.GroupName = "grp2";
        ChangeType2.GroupName = "grp2";
		
		
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
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad005_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad005_form_aspx.language.ini", "message", "tp", "中普科技")}
        };
        CompanyCode.setListItem(idsCompany);
		string[] values = base.getUserInfoById(engine,(string)Session["UserID"]);
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
        //實際領取人
        GetMemberGUID.clientEngineType = engineType;
        GetMemberGUID.connectDBString = connectString;

        //原出差單據勾稽		
        OriForeignForm.clientEngineType = engineType;
        OriForeignForm.connectDBString = connectString;
        //給客製OpenWin程式位置
        //setSession((string)Session["UserID"], "OriginatorId", OriginatorGUID.ValueText);
        //OriForeignForm.customOpenWinPage = "/DSCWebControlRunTime/DSCWebControlUI/OpenWin/OpenWinOriForeignForm.aspx";
		bool isAddNew = base.isNew();
		if (isAddNew)
		{
			//OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "' and SMWYAAA020='Y' and (BillFormStatus<>'Y' or  BillFormStatus<>'I')   ";
			OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "'   and (ChgID is null or ChgID='') and SMWYAAA020='Y'    ";
		}
        
        //Radio Group, Charge Fee費用負擔
        FeeCharge1.GroupName = "grp1";
        FeeCharge2.GroupName = "grp1";
        FeeCharge3.GroupName = "grp1";
        //FeeCharge4.GroupName = "grp1";  //取消合普
		FeeCharge5.GroupName = "grp1";
		FeeCharge6.GroupName = "grp1";
        
        //班別
        ClassCode.ValueText = "SD4"; //班別 (SD4:常日班)
        //假別
        CategoryCode.ValueText = "LI04";
        //請假是否含假日, 不含
        IsIncludeHoliday.ValueText = "Y";

        //新建立表單不可維護欄位
        GetMemberGUID.ReadOnly = true;
        ActualGetDate.ReadOnly = true;
        PrePayComment.ReadOnly = true;
        StartTrvlDate.ReadOnly = true;
        EndTrvlDate.ReadOnly = true;
        TrvlDesc.ReadOnly = true;
        PrePayTwd.ReadOnly = true;
        PrePayTwdAmt.ReadOnly = true;
        PrePayCny.ReadOnly = true;
        PrePayCnyAmt.ReadOnly = true;
        PrePayUsd.ReadOnly = true;
        PrePayUsdAmt.ReadOnly = true;
		PrePayJpy.ReadOnly = true;
        PrePayJpyAmt.ReadOnly = true;
		PrePayEur.ReadOnly = true;
        PrePayEurAmt.ReadOnly = true;
        PrePayOther.ReadOnly = true;
        PrePayOtherAmt.ReadOnly = true;
        SiteUsDesc.ReadOnly = true;
        SiteUs.ReadOnly = true;
        SiteJp.ReadOnly = true;
        SiteJpDesc.ReadOnly = true;
        SiteKr.ReadOnly = true;
        SiteKrDesc.ReadOnly = true;
        SiteSub.ReadOnly = true;
        SiteSubDesc.ReadOnly = true;
        SiteOther.ReadOnly = true;
        SiteOtherDesc.ReadOnly = true;
        FeeCharge1.ReadOnly = true;
        FeeCharge2.ReadOnly = true;
        FeeCharge3.ReadOnly = true;
        //FeeCharge4.ReadOnly = true;
		FeeCharge5.ReadOnly = true;
        FeeCharge6.ReadOnly = true;

        //改變工具列順序
        base.initUI(engine, objects);
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {	
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
		//sw.WriteLine("showData ");
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        SheetNo.Display = false;
        Subject.Display = false;
        IsIncludeDateEve.Display = false;
        TempSerialNo.Display = false;
        ClassCode.Display = false;
        CategoryCode.Display = false;
        IsIncludeHoliday.Display = false;
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo"); //傳入序號, 列印傳入值
		//公司別
        CompanyCode.ValueText = objects.getData("CompanyCode");
        //異動類別     
        string changeType = Convert.ToString(objects.getData("ChangeType"));
        switch (changeType)
        {
            case "0":
                ChangeType1.Checked = true;
                break;
            case "1":
                ChangeType2.Checked = true;
                break;
            default:
                break;
        }

        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		
        //申請單位
        DeptGUID.ValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
                
        Title.ValueText = objects.getData("Title"); //職稱
        //sw.WriteLine("Title : " + Title.ValueText);
        //職務代理人員
        AgentGUID.GuidValueText = objects.getData("AgentGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        AgentGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID

        //審核人員
        string checkByGUID = objects.getData("CheckByGUID");
        if (!checkByGUID.Equals(""))
        {
            CheckByGUID.GuidValueText = checkByGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            CheckByGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		
		//sw.WriteLine("OriginatorID :  " + OriginatorGUID.ValueText );

        //原出差單據勾稽
        OriForeignForm.GuidValueText = objects.getData("OriForeignForm"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriForeignForm.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		//OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "'";
    
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

        TrvlDesc.ValueText = objects.getData("TrvlDesc");
        ChgTrvlDesc.ValueText = objects.getData("ChgTrvlDesc");

        StartTrvlDate.ValueText = objects.getData("StartTrvlDate");
        EndTrvlDate.ValueText = objects.getData("EndTrvlDate");
        ChgStartTrvlDate.ValueText = objects.getData("ChgStartTrvlDate");
        ChgEndTrvlDate.ValueText = objects.getData("ChgEndTrvlDate");
		HrStartTrvlDate.ValueText = objects.getData("HrStartTrvlDate");
        HrEndTrvlDate.ValueText = objects.getData("HrEndTrvlDate");

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
        PrePayComment.ValueText = objects.getData("PrePayComment");
        ActualGetDate.ValueText = objects.getData("ActualGetDate");
        //預支領取人
        string getMemberGUID = objects.getData("GetMemberGUID");
        if (!getMemberGUID.Equals(""))
        {
            GetMemberGUID.GuidValueText = getMemberGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            GetMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
        //請假單序號
        TempSerialNo.ValueText = objects.getData("TempSerialNo"); //寫入假單序號

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
            ChangeType1.ReadOnly = true;
            ChangeType2.ReadOnly = true;
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
            PrePayComment.ReadOnly = true;
            ActualGetDate.ReadOnly = true;
            GetMemberGUID.ReadOnly = true;

            ChgTrvlDesc.ReadOnly = true;
            ChgStartTrvlDate.ReadOnly = true;
            ChgEndTrvlDate.ReadOnly = true;
            ChgPrePayCny.ReadOnly = true;
            ChgPrePayCnyAmt.ReadOnly = true;
            ChgPrePayOther.ReadOnly = true;
            ChgPrePayOtherAmt.ReadOnly = true;
            ChgPrePayTwd.ReadOnly = true;
            ChgPrePayTwdAmt.ReadOnly = true;
            ChgPrePayUsd.ReadOnly = true;
            ChgPrePayUsdAmt.ReadOnly = true;
			ChgPrePayJpy.ReadOnly = true;
            ChgPrePayJpyAmt.ReadOnly = true;
			ChgPrePayEur.ReadOnly = true;
            ChgPrePayEurAmt.ReadOnly = true;
            OriForeignForm.ReadOnly = true;

        }

        if (isAddNew)
        {
            SiteUsDesc.ReadOnly = true;
            SiteUs.ReadOnly = true;
            SiteJp.ReadOnly = true;
            SiteJpDesc.ReadOnly = true;
            SiteKr.ReadOnly = true;
            SiteKrDesc.ReadOnly = true;
            SiteSub.ReadOnly = true;
            SiteSubDesc.ReadOnly = true;
            SiteOther.ReadOnly = true;
            SiteOtherDesc.ReadOnly = true;
            FeeCharge1.ReadOnly = true;
            FeeCharge2.ReadOnly = true;
            FeeCharge3.ReadOnly = true;
            //FeeCharge4.ReadOnly = true;
			FeeCharge5.ReadOnly = true;
            FeeCharge6.ReadOnly = true;
        }

        if (actName.Equals("財務出納"))
        {
            PrePayComment.ReadOnly = false;
            ActualGetDate.ReadOnly = false;
            GetMemberGUID.ReadOnly = false;
            ChgPrePayTwd.ReadOnly = false;
            ChgPrePayTwdAmt.ReadOnly = false;
            ChgPrePayCny.ReadOnly = false;
            ChgPrePayCnyAmt.ReadOnly = false;
            ChgPrePayUsd.ReadOnly = false;
            ChgPrePayUsdAmt.ReadOnly = false;
			ChgPrePayJpy.ReadOnly = false;
            ChgPrePayJpyAmt.ReadOnly = false;
			ChgPrePayEur.ReadOnly = false;
            ChgPrePayEurAmt.ReadOnly = false;
            ChgPrePayOther.ReadOnly = false;
            ChgPrePayOtherAmt.ReadOnly = false;            
        }
		
		HrEndTrvlDate.Display = false;
        HrStartTrvlDate.Display = false;
        lbHrTrvlDate.Display = false;
        lbHrTrvlDesc.Display = false;
        if (actName.Equals("SPAD001_HRADM") || actName.Equals("差勤負責人") || actName.Equals("差勤負責人2"))
        {
            HrEndTrvlDate.Display = true;
            HrStartTrvlDate.Display = true;
            lbHrTrvlDate.Display = true;
            lbHrTrvlDesc.Display = true;
        }

        //sw.Close();
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        try
        {
            //System.IO.StreamWriter sw = null;
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
			//sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
            //sw.WriteLine("saveData start!!");
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別
            string actName = Convert.ToString(getSession("ACTName").ToString());

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
				objects.setData("CompanyCode", CompanyCode.ValueText);
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("Title", Title.ValueText);
                objects.setData("AgentGUID", AgentGUID.GuidValueText);
                objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
                
                // 預計出差日期
                objects.setData("StartTrvlDate", StartTrvlDate.ValueText);
                objects.setData("EndTrvlDate", EndTrvlDate.ValueText);

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

                //C 預支申請
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
                objects.setData("PrePayComment", PrePayComment.ValueText);
                objects.setData("ActualGetDate", ActualGetDate.ValueText);
                objects.setData("GetMemberGUID", GetMemberGUID.GuidValueText);

                //ChangeType 異動類別
                string changeType = "";
                if (ChangeType1.Checked)
                    changeType = "0";
                if (ChangeType2.Checked)
                    changeType = "1";
                objects.setData("ChangeType", changeType);

                objects.setData("OriForeignForm", OriForeignForm.GuidValueText);
                objects.setData("ChgStartTrvlDate", ChgStartTrvlDate.ValueText);
                objects.setData("ChgEndTrvlDate", ChgEndTrvlDate.ValueText);
                objects.setData("ChgTrvlDesc", ChgTrvlDesc.ValueText);
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
					prePayName += PrePayComment.ValueText +  ";  ";
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
				
				
                
				objects.setData("OriginatorUserName", OriginatorGUID.ReadOnlyValueText);
				objects.setData("DeptName", DeptGUID.ReadOnlyValueText);
				objects.setData("AgentName", AgentGUID.ReadOnlyValueText);
				objects.setData("CheckByName", CheckByGUID.ReadOnlyValueText);
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
			
            objects.setData("StartTrvlDate", StartTrvlDate.ValueText);
            objects.setData("EndTrvlDate", EndTrvlDate.ValueText);
            objects.setData("PrePayComment", PrePayComment.ValueText);
            objects.setData("ActualGetDate", ActualGetDate.ValueText);
            objects.setData("GetMemberGUID", GetMemberGUID.GuidValueText);
            objects.setData("HrStartTrvlDate", HrStartTrvlDate.ValueText);
            objects.setData("HrEndTrvlDate", HrEndTrvlDate.ValueText);
				
			//當董事長關卡前才產生假單序號
            if (actName.Equals("差勤負責人"))
            {
                
                object[] aryTemp = callDataCenter("6");
                if (Convert.ToString(aryTemp[0]).Equals("Y"))
                {
                    string tempSerialNo = Convert.ToString(aryTemp[2]);
                    //sw.WriteLine("saveData actName : " + tempSerialNo);
                    objects.setData("TempSerialNo", tempSerialNo);
                    //TempSerialNo.ValueText = tempSerialNo; //為何要多做?
                    engine.updateData(objects);
                    //寫字串到ERP備註欄位
                    updateErpForm(objects);
                }
            }
			
            //beforeSetFlow
            setSession("IsSetFlow", "Y");
            //sw.Close();

            //beforeSign 加簽
            //string actName = Convert.ToString(getSession("ACTName").ToString());
			if (actName.Equals("SPAD001_HRADM") || actName.Equals("差勤負責人") || actName.Equals("差勤負責人2"))
            {
                setSession("IsAddSign", "AFTER");
            }

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
           // if (sw != null) sw.Close();
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

        //sw.WriteLine("actName=>> " + actName);        

        if (actName.Equals(""))
        {
            if (ChangeType1.Checked.Equals(false) && ChangeType2.Checked.Equals(false))
            {
                strErrMsg += "請點選 異動類別\n";
            }
            if (OriForeignForm.ValueText.Equals(""))
            {
                strErrMsg += "請勾稽原出差單據\n";
            }
            if (Title.ValueText.Equals(""))
            {
                strErrMsg += "請輸入 職稱\n";
            }
            if (AgentGUID.ValueText.Equals(""))
            {
                strErrMsg += "代理人員不可為空值\n";
            }
            //審核人一二不可為申請人
            if (OriginatorGUID.ValueText.Equals(AgentGUID.ValueText))
            {
                strErrMsg += "代理人不可為申請人!\n";
            }
            if (OriginatorGUID.ValueText.Equals(CheckByGUID.ValueText))
            {
                strErrMsg += "審核人員不可為出差人員\n";
            }
            if (StartTrvlDate.ValueText.Equals("") && EndTrvlDate.ValueText.Equals(""))
            {
                strErrMsg += "預計出差日期不可為空值\n";
            }
            if (ChangeType1.Checked.Equals(true) && ChgStartTrvlDate.ValueText.Equals("") && ChgEndTrvlDate.ValueText.Equals(""))
            {
                strErrMsg += "類別為出差日異動 - 異動後出差日期不可為空值\n";
            }
            
			if (!ChgStartTrvlDate.ValueText.Equals("") && !ChgEndTrvlDate.ValueText.Equals(""))
            {
				if (Convert.ToDateTime(ChgStartTrvlDate.ValueText) > Convert.ToDateTime(ChgEndTrvlDate.ValueText))
				{
					strErrMsg += "異動後出差日期起不可大於出差日期訖\n";
				}
            }
			
            //if (startDateTime.CompareTo(endDateTime) > 0)
            //{
            //    strErrMsg += "預計出差日期起 不可小於 預計出差日期訖!\n";
            //}
            if (TrvlDesc.ValueText.Equals(""))
            {
                strErrMsg += "出差（預辦）事由不可為空值\n";
            }
            if (SiteUs.Checked.Equals(false) && SiteJp.Checked.Equals(false) && SiteKr.Checked.Equals(false) && SiteSub.Checked.Equals(false) && SiteOther.Checked.Equals(false))
            {
                strErrMsg += "請點選 出差地點\n";
            }
            if (SiteUs.Checked.Equals(true))
            {
                if (SiteUsDesc.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 出差地點-美國\n";
                }
            }
            if (SiteJp.Checked.Equals(true))
            {
                if (SiteJpDesc.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 出差地點-日本\n";
                }
            }   
            if (SiteKr.Checked.Equals(true))
            {
                if (SiteKrDesc.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 出差地點-韓國\n";
                }
            }
            if (SiteSub.Checked.Equals(true))
            {
                if (SiteSubDesc.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 出差地點-子公司\n";
                }
            }
            if (SiteOther.Checked.Equals(true))
            {
                if (SiteOtherDesc.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 出差地點-其他\n";
                }
            }
            if (FeeCharge1.Checked.Equals(false) && FeeCharge2.Checked.Equals(false) && FeeCharge3.Checked.Equals(false) && FeeCharge5.Checked.Equals(false) && FeeCharge6.Checked.Equals(false))
            {
                strErrMsg += "請點選 費用負擔\n";
            }

            ////代理人員不可設為請假人員
            //if (OriginatorGUID.ValueText.Equals(AgentGUID.ValueText))
            //{
            //    strErrMsg += "代理人員不可設為出差人員!\n";
            //}

            if (PrePayTwd.Checked.Equals(true))
            {
                if (PrePayTwdAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 新台幣 金額\n";
                }
            }
            if (PrePayCny.Checked.Equals(true))
            {
                if (PrePayCnyAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 人民幣 金額\n";
                }
            }
            if (PrePayUsd.Checked.Equals(true))
            {
                if (PrePayUsdAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 美金 金額\n";
                }
            }
			if (PrePayJpy.Checked.Equals(true))
            {
                if (PrePayJpyAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 日圓 金額\n";
                }
            }
			if (PrePayEur.Checked.Equals(true))
            {
                if (PrePayEurAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 歐元 金額\n";
                }
            }
            if (PrePayOther.Checked.Equals(true))
            {
                if (PrePayOtherAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 其他幣別與金額\n";
                }
            }
			
			//審核人不可為直屬主管
            string originatorGUID = OriginatorGUID.GuidValueText;
            string[] valuesMgr = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = valuesMgr[0];
            valuesMgr = base.getUserInfo(engine, managerGUID);
            string managerId = valuesMgr[0];
            if (managerId.Equals(CheckByGUID.ValueText))
            {
                strErrMsg += "審核人不需填寫直屬主管.系統會自動判斷!\n";
            }

            ////請假日期時間有重覆!
            //string strTemp = checkDataTime();
            //if (!strTemp.Equals(""))
            //{
            //    strErrMsg += strTemp;
            //}

            //


            //設定主旨
			string subjectType = "";
            values = base.getUserInfoById(engine, OriginatorGUID.ValueText);
            string subject = "";
			if (ChangeType1.Checked.Equals(true)) // && ChangeType2.Checked.Equals(false))
            {
                subjectType = "國外出差異動";
                subject = "【" + subjectType + "人員：" + values[1] + " 異動後出差日期: " + ChgStartTrvlDate.ValueText + " ~ " + ChgEndTrvlDate.ValueText + "】";
            }else
			{
			    subjectType = "取消國外出差";
                subject = "【" + subjectType + "人員：" + values[1] + " 原出差日期: " + StartTrvlDate.ValueText + " ~ " + EndTrvlDate.ValueText + "】";
			}
            //if (Subject.ValueText.Equals(""))
            //{
                Subject.ValueText = subject;
            //}
       
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
        //si.ownerID = (string)Session["UserID"];
        //si.ownerName = (string)Session["UserName"];
        si.ownerID = OriginatorGUID.ValueText; //申請人id
        si.ownerName = OriginatorGUID.ReadOnlyValueText;  //申請人名稱
        depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
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
        System.IO.StreamWriter sw = null;
        string xml = "";
		string[] values = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            
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

            string strSSP = "N";     //是否為替代役人員, 預設為N
            //select PALSMA014 from SMP_CMSMV where MV001 = '3787' 進入鼎新中取得人員是否為替代役人員
            //string strPreDoc = "N";  //是否預辦證件
            //if (Passport.Checked.Equals(true) || MTPs.Checked.Equals(true) || MtpsPlus.Checked.Equals(true) || USvisa.Checked.Equals(true) || Other.Checked.Equals(true))
            //{
            //    strPreDoc = "Y";
            //}
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
                strNotify2 += deptAsstId + (";");
            }
            //string strTrvlSite = "1";//費用付擔, 判斷是否加簽各公司別最高主管 

            //異動類別
            string strChangeType = "";
            if (ChangeType1.Checked)
                strChangeType = "0";
            if (ChangeType2.Checked)
                strChangeType = "1";

            //日期異動
            string strDateChange = "Y";
            if (strChangeType.Equals("0"))
            {
                DateTime sDate = Convert.ToDateTime(currentObject.getData("ChgStartTrvlDate"));
                DateTime eDate = Convert.ToDateTime(currentObject.getData("ChgEndTrvlDate"));
                TimeSpan ts = eDate - sDate;
                double days = ts.TotalDays + 1;
                
                if (days.Equals(0))
                {
                    strDateChange = "N";
                }
            }
            //預支金額
            string strPreMoney = "Y";
            if (currentObject.getData("ChgPrePayTwdAmt").Equals("") && currentObject.getData("ChgPrePayCnyAmt").Equals("") && currentObject.getData("ChgPrePayUsdAmt").Equals("") &&
                currentObject.getData("ChgPrePayOtherAmt").Equals("") && currentObject.getData("PrePayTwdAmt").Equals("") && currentObject.getData("PrePayCnyAmt").Equals("") &&
                currentObject.getData("PrePayUsdAmt").Equals("") && currentObject.getData("PrePayOtherAmt").Equals(""))
            {
                strPreMoney = "N";
            }


            //sw.WriteLine("managerId=" + managerId);
            xml += "<SPAD005>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";              //填表人 
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";     //出差人員
            xml += "<ssp DataType=\"java.lang.String\">" + strSSP + "</ssp>";                         //是否為替代役人員, 預設為N
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";            //審核人
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";              //直屬主管
            xml += "<notify1 DataType=\"java.lang.String\">" + strNotify1 + "</notify1>";             //通知人員1
            xml += "<notify2 DataType=\"java.lang.String\">" + strNotify2 + "</notify2>";             //通知人員2
            xml += "<changeclass DataType=\"java.lang.String\">" + strChangeType + "</changeclass>";  //異動類別
            xml += "<datechange DataType=\"java.lang.String\">" + strDateChange + "</datechange>";    //日期異動
            xml += "<casher DataType=\"java.lang.String\">SMP-CASHER</casher>";                     //財務出納
            xml += "<hradm DataType=\"java.lang.String\">SMP-HRADM</hradm>";                          //差勤負責人            
            xml += "<secretary DataType=\"java.lang.String\">SMP-SECRETARY</secretary>";              //秘書
            xml += "<ssphr DataType=\"java.lang.String\">SPAD004-HRSSP</ssphr>";                      //通知人員(HR替代役)
            xml += "<premoney DataType=\"java.lang.String\">" + strPreMoney + "</premoney>";          //預支金額
            xml += "<checkby2 DataType=\"java.lang.String\"></checkby2>";                           //CheckBy2, 預留欄位
            xml += "</SPAD005>";
           // sw.WriteLine("xml: " + xml);
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            if (sw != null) sw.Close();
        }
        //表單代號
        param["SPAD005"] = xml;
        return "SPAD005";
    }

    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
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
		//選取對應的出差單或異動單據資料
		bool isAddNew = base.isNew();
		if (isAddNew)
		{			
			OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "'   and (ChgID is null or ChgID='') and SMWYAAA020='Y'    ";
		}
		//OriForeignForm.whereClause = " UserId='" + OriginatorGUID.ValueText + "'   and (ChgID is null or ChgID='') and SMWYAAA020='Y'     ";
        
    }

    /// <summary>
    /// 表單送出後
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        try
        {
            string strErrMsg = "";
            //在董事長關卡時,新增WFERP請假單
            string actName = Convert.ToString(getSession("ACTName"));
            if (actName.Equals("董事長"))
            {
                object[] aryTemp = callDataCenter("6");
                if (Convert.ToString(aryTemp[0]).Equals("Y"))
                {
                    string tempSerialNo = Convert.ToString(aryTemp[2]);
                    currentObject.setData("TempSerialNo", tempSerialNo);
                    TempSerialNo.ValueText = tempSerialNo;
                    engine.updateData(currentObject);
                    //寫字串到ERP備註欄位
                    updateErpForm(currentObject);
                }
                else
                {
                    strErrMsg += "送簽後續處理作業發生錯誤, 錯誤原因: " + Convert.ToString(aryTemp[1]);
                }
            }
            

            if (!strErrMsg.Equals(""))
            {
                pushErrorMessage(strErrMsg);
                //throw new Exception(strErrMsg);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        base.afterSend(engine, currentObject);
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
		string flag = "Y";
		if(StartTrvlDate.ValueText.Equals(ChgStartTrvlDate.ValueText) && (EndTrvlDate.ValueText.Equals(ChgEndTrvlDate.ValueText)))
		{
			flag = "N";
		}
		
		if(TempSerialNo.ValueText.Equals("") || (TempSerialNo == null))
		{
			flag = "N";
		}
		
		if(!TempSerialNo.ValueText.Equals(""))
		{
			flag = "Y";
		}

        //MessageBox("CheckType : " + ChangeType2.Checked);

        //MessageBox("actName : " + actName);

        if (actName.Equals("差勤負責人2"))
        {
            if (ChangeType1.Checked)
            {
                if(flag.Equals("Y"))
				{
	                //確認ERP單據                
	                //string result = "";
	                string result = approveErpForm();
					
	                if (!result.Equals(""))
	                {
	                    throw new Exception(result);
	                }
				}
				else 
				{
					MessageBox("無產生假單! ");
				}
            }
        }

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
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回關卡 ID        
        
        //先退回
        if (backActID.ToUpper().Equals("CREATOR") || backActID.ToUpper().Equals("ACT30") || backActID.ToUpper().Equals("MANAGER") 
		   || backActID.Equals("ACT33") || backActID.Equals("ACT35") || backActID.Equals("ACT31")  )
        {
			//MessageBox("rejectProcedure()");
            try
            {
                if (!TempSerialNo.ValueText.Equals(""))
                {
                   // widhDrawErpForm();
                }
                //base.rejectProcedure();
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
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        if (result.Equals("Y"))
        {

        }
        else
        {
            //widhDrawErpForm(engine, currentObject);
			widhDrawErpForm();
        }
        base.afterApprove(engine, currentObject, result);
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
	


    /// <summary>
    /// 透過元件資料相關資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private object[] callDataCenter(string id)
    {
        object[] aryTemp = null; //回傳值, object 陣列
        object[] aryCompanyData = new object[2];
        object[] aryData = null;
        System.IO.StreamWriter sw = null;
        string connectString = (string)Session["connectString"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        try
        {
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            sp = new WebServerProject.SysParam(engine);
			string companyId = CompanyCode.ValueText;
			string companyDBName = sp.getParam(companyId + "SPADCompanyDBName");
            object strERPServerIP = sp.getParam("SPADComServerIP");
            object strERPVersion = "3";
            object erpVersion = "lngERPVersion";
            object erpServerIP = "strERPServerIP";
            object progId = "strProgID";
            object pali15 = "PALI15";
            object palef12 = "PALEF12";
            object aryPara = "aryPara";
            object aryCompany = "aryCompanyData";

            aryCompanyData[0] = companyId;
            aryCompanyData[1] = companyDBName;
            object aryObjCompanyData = aryCompanyData;

            switch (id)
            {
                case "4": //把日期傳到ERP查詢當天是否為假日 等於 getIsHoliday()
                    {
                        aryData = new object[9];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //工號
                        aryData[2] = StartTrvlDate.ValueText.Replace("/", ""); //請假日期
                        aryData[3] = ClassCode.ValueText; //班別 (SD4:常日班)
                        aryData[4] = ""; //
                        aryData[5] = ""; //
                        aryData[6] = ""; //
                        aryData[7] = ""; //
                        aryData[8] = ""; //
                        object aryObjData = aryData;
                        

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion); //ERPVersion
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                        dicERPFormListTemp.Add(ref progId, ref pali15); //ERPProgID
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData); //資料參數
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);//Company ID & DBName

                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp);
                        //回傳值 [0]: 班別代號; [1]: 班別名稱; [2]: 是否為假日; 0: 非假日, 1: 假日
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "5": //ERP計算請假時數, 等於 Calculate_LeaveTime
                    {
                        //MessageBox("callDataCenter(" + id + ")");
                        aryData = new object[8];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //工號
                        aryData[2] = StartTrvlDate.ValueText.Replace("/", ""); //請假日期
                        aryData[3] = EndTrvlDate.ValueText.Replace("/", ""); //請假日期迄
                        aryData[4] = CategoryCode.ValueText; //假別
                        aryData[5] = IsIncludeHoliday.ValueText; //請假含假日
                        aryData[6] = "0800"; //請假時間起
                        aryData[7] = "1700"; //請假時間迄
                        object aryObjData = aryData;

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion); //ERPVersion
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                        dicERPFormListTemp.Add(ref progId, ref palef12); //ERPProgID
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData); //資料參數
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);//Company ID & DBName
                        
                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp);
                        //回傳值 [0]: 狀況代碼; [1]: 休假時數 (會因前端資料而計算不正確); [2]: 訊息
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "6": //6.新增WFERP請假單
                    {
						aryData = new object[8];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //員工代號
                        aryData[2] = HrStartTrvlDate.ValueText.Replace("/", ""); //起始請假日期
                        aryData[3] = HrEndTrvlDate.ValueText.Replace("/", ""); //截止請假日期
                        aryData[4] = CategoryCode.ValueText; //假別
                        aryData[5] = IsIncludeHoliday.ValueText; //請假是否含假日
                        aryData[6] = "0800"; //請假起始時分
                        aryData[7] = "1700"; //請假截止時分
                        object aryObjData = aryData;
						
						Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion);
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP);
                        dicERPFormListTemp.Add(ref progId, ref palef12);
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData);
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);
                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp); //送資料給ERP測試可不可以寫入
                        //回傳值 [0]: Y 成功; [1]: 訊息; [2]: 此次寫入ERP 回傳的資料(假單單號)
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "7": //7:寫字串到ERP備註欄位
                    {
                        aryData = new object[11];
                        string strUserID = (string)Session["UserID"];
                        string[] tempSerialNos = TempSerialNo.ValueText.Split(';');
                        object[] aryTempSerialNo = new object[tempSerialNos.Length];
                        for (int i = 0; i < aryTempSerialNo.Length; i++)
                        {
                            aryTemp = tempSerialNos[i].Split('-');
                            aryTempSerialNo[i] = aryTemp;
                        }

                        aryData[0] = "7";
                        aryData[1] = strUserID; //員工代號
                        aryData[2] = "";
                        aryData[3] = "";
                        aryData[4] = "";
                        aryData[5] = "";
                        aryData[6] = "";
                        aryData[7] = "";
                        aryData[8] = getSession(this.PageUniqueID, "SheetNo");
                        aryData[9] = Subject.ValueText.Substring(0, 100);
                        aryData[10] = aryTempSerialNo;
                        object aryObjData = aryData;

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion);
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP);
                        dicERPFormListTemp.Add(ref progId, ref palef12);
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData);
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);
                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp);
                        //回傳值 [0]: Y 成功; [1]: 訊息
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                case "9": //取回員工基本班別,與上下班/休息時間等 (作業類別9) 等於 CheckFieldData->SetUserWorkInfo
                    {
                        string startDate = StartTrvlDate.ValueText;
                        if (startDate.Equals(""))
                        {
                            startDate = DateTimeUtility.convertDateTimeToString(DateTime.Now.AddDays(1)).Substring(0, 10);
                            startDate = startDate.Replace("/", "");
                        }

                        aryData = new object[3];
                        aryData[0] = id;
                        aryData[1] = OriginatorGUID.ValueText; //工號
                        aryData[2] = startDate.Replace("/", ""); //請假日期
                        object aryObjData = aryData;

                        Scripting.Dictionary dicERPFormListTemp = new Scripting.Dictionary();
                        dicERPFormListTemp.Add(ref erpVersion, ref strERPVersion); //ERPVersion
                        dicERPFormListTemp.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                        dicERPFormListTemp.Add(ref progId, ref palef12); //ERPProgID
                        dicERPFormListTemp.Add(ref aryPara, ref aryObjData); //資料參數,本例: 作業類別/工號/請假明期
                        dicERPFormListTemp.Add(ref aryCompany, ref aryObjCompanyData);//Company ID & DBName

                        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP(); //元件初始值方法
                        aryTemp = (object[])callForERP.SaveFormInfoToERP(dicERPFormListTemp); //元件呼叫方法, dicERPFormListTemp 呼叫之參數
                        //回傳值 [0]: 班別代號; [1]: 班別名稱; [2]: 上班時間; [3]: 下班時間; [4]: 正常工作時數; [5]: 中間休息開始時間; [6]: 中間休息結束時間;
                        dicERPFormListTemp = null;
                        callForERP = null;
                        break;
                    }
                default:
                    aryTemp = new object[1];
                    break;
            }
        }
        catch (Exception e)
        {
            aryTemp = new object[1];
            base.writeLog(e);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
        }
        return aryTemp;
    }

    /// <summary>
    /// 更新ERP假單
    /// </summary>
    private void updateErpForm(DataObject currentObject)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        System.IO.StreamWriter sw = null;
        string sql = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            string connectString = (string)Session["connectString"];
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            erpEngine = base.getErpEngine();
            sp = new WebServerProject.SysParam(engine);
			string companyId = CompanyCode.ValueText;
            //string companyId = sp.getParam("SPADCompanyID");
            string tempSerialNo = currentObject.getData("TempSerialNo");
            //sw.WriteLine(tempSerialNo);
            string[] aryTempSerialNo = tempSerialNo.Split(';');
            string strUserID = (string)Session["UserID"];
            string strDateNow = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
            string strSheetNo = currentObject.getData("SheetNo");
            //sw.WriteLine(strSheetNo);
            string strComment = strSheetNo + " - " + Subject.ValueText;
            if (strComment.Length > 100)
            {
                strComment.Substring(0, 100);
            }

            erpEngine = base.getErpEngine();
            for (int i = 0; i < aryTempSerialNo.Length; i++)
            {
                string[] aryWfFormIDs = aryTempSerialNo[i].Split('-');
                for (int j = 0; j < aryWfFormIDs.Length; j++)
                {
                    string strWfFormID = aryWfFormIDs[0];
                    string strWfSerialNo = aryWfFormIDs[1];
                    sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strDateNow + "', FLAG = 1, TF010='" + strComment + "', TF904='" + strSheetNo + "' WHERE COMPANY='" + companyId + "' AND TF001='" + OriginatorGUID.ValueText + "' AND TF002='" + strWfFormID + "' AND TF003='" + strWfSerialNo + "'";
                    //sw.WriteLine(sql);
                    erpEngine.executeSQL(sql);
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
    }

    /// <summary>
    /// 核准ERP單據
    /// </summary>
    private string approveErpForm()
    {
        string strResult = "";
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
		AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        EFGateWayOfERP.Engine erpGateWayEngine = null;
        EFGateWayOfERP.CallForERP callForERP = null;
        System.IO.StreamWriter sw = null;

        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            //sw.WriteLine("call approveErpForm");
            string comExeResult = null;
            bool isError = false;
            string connectString = (string)Session["connectString"];
            engine = factory.getEngine(EngineConstants.SQL, connectString);
			erpEngine = base.getErpEngine(CompanyCode.ValueText);
            sp = new WebServerProject.SysParam(engine);
			string companyCode = CompanyCode.ValueText;
            string companyId = sp.getParam(companyCode + "SPADCompanyID");
            object strERPServerIP = sp.getParam("SPADComServerIP");
            string strEF2KWebSite = sp.getParam("EF2KWebSite");
			

            object strERPVersion = "3"; //ERP版本
            object strReturnType = "2"; //回寫狀態
            object strWfFormID = OriginatorGUID.ValueText; //表單單別（員工代號）
            object strAction = "0"; //確認狀態
            object strProgID = "PALI12"; //程式代號
            object strCompID = companyId; //公司別代號
            object strUserID = (string)Session["UserID"]; //審核人員工代號
            object strParameter4 = ""; //參數四	
            object strKeyNumber = "3"; //Key值個數
            object strComfirmObject = "TransManager.TxnManager"; //確認元
            object strComPRID = "PALI12"; //確認元程式代號
            object strComDate = ""; // DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //確認日期
            object strUserNTID = "LISA_LAI";
            string sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID.ToString()) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strUserNTID = ds.Tables[0].Rows[0][0].ToString();
            }
            string strSiteName = "EF2KWeb";
            int idx = strEF2KWebSite.LastIndexOf("/");
            if (idx > 0)
            {
                strSiteName = strEF2KWebSite.Substring(idx + 1);
            }

            object erpVersion = "lngERPVersion";
            object returnType = "lngReturnType";
            object wfFormID = "strWfFormID";
            object wfSheetNo = "strWfSheetNo";
            object action = "lngAction";
            object progID = "strProgID";
            object compID = "strCompID";
            object userID = "strUserID";
            object parameter3 = "strParameter3";
            object parameter4 = "strParameter4";
            object keyNumber = "lngKeyNumber";
            object comfirmObject = "strComfirmObject";
            object comPRID = "strComPRID";
            object comDate = "strComDate";
            object userNTID = "strUserNTID";
            object erpServerIP = "strERPServerIP";
            object debugMod = "strDebugMod";
            object y = "Y";
			
			string strDateNow = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");

            callForERP = new EFGateWayOfERP.CallForERP();
            erpGateWayEngine = new EFGateWayOfERP.Engine();
            string[] tempSerialNos = TempSerialNo.ValueText.Split(';');
            //sw.WriteLine("TempSerialNo=" + TempSerialNo.ValueText);
            //MessageBox("tempSerialNos Length: " + TempSerialNo.ValueText);
            for (int i = 0; i < tempSerialNos.Length; i++)
            {
                string[] arySerialNos = tempSerialNos[i].Split('-');
                object strWfSheetNo = arySerialNos[0]; //表單單號（請假日期）

               // MessageBox("strWfSheetNo: " + arySerialNos[0].ToString());

                object strParameter3 = arySerialNos[1]; //流水號

                //MessageBox("strParameter3: " + arySerialNos[0].ToString());

                Scripting.Dictionary dicERPInfoSet = new Scripting.Dictionary();
                dicERPInfoSet.Add(ref erpVersion, ref strERPVersion); //ERP版本
                dicERPInfoSet.Add(ref returnType, ref strReturnType); //回寫狀態
                dicERPInfoSet.Add(ref wfFormID, ref strWfFormID); //ERP程式代號
                dicERPInfoSet.Add(ref wfSheetNo, ref strWfSheetNo); //ERP單號
                dicERPInfoSet.Add(ref action, ref strAction); //Action
                dicERPInfoSet.Add(ref progID, ref strProgID); //EF表單代號
                dicERPInfoSet.Add(ref compID, ref strCompID); //ERP公司別代號
                dicERPInfoSet.Add(ref userID, ref strUserID); //EF員工代號
                dicERPInfoSet.Add(ref parameter3, ref strParameter3); //ERP 參數3
                dicERPInfoSet.Add(ref parameter4, ref strParameter4); //ERP 參數4
                dicERPInfoSet.Add(ref keyNumber, ref strKeyNumber); //Key值個數
                dicERPInfoSet.Add(ref comfirmObject, ref strComfirmObject); //ERP確認元代號
                dicERPInfoSet.Add(ref comPRID, ref strComPRID); //ERP確認元程式代號
                dicERPInfoSet.Add(ref comDate, ref strComDate); //確認日期
                dicERPInfoSet.Add(ref userNTID, ref strUserNTID); //EF使用者NT登入帳號
                dicERPInfoSet.Add(ref erpServerIP, ref strERPServerIP); //ERPServerIP
                dicERPInfoSet.Add(ref debugMod, ref y);
                //Scripting.Dictionary dicERPInfoSet = new Scripting.Dictionary();
                //dicERPInfoSet.Add("lngERPVersion", Convert.ToInt32(strERPVersion)); //ERP版本
                //dicERPInfoSet.Add("lngReturnType", Convert.ToInt32(strReturnType)); //回寫狀態
                //dicERPInfoSet.Add("strWfFormID", strWfFormID); //ERP程式代號
                //dicERPInfoSet.Add("strWfSheetNo", strWfSheetNo); //ERP單號
                //dicERPInfoSet.Add("lngAction", Convert.ToInt32(strAction)); //Action
                //dicERPInfoSet.Add("strProgID", strProgID); //EF表單代號
                //dicERPInfoSet.Add("strCompID", strCompID); //ERP公司別代號
                //dicERPInfoSet.Add("strUserID", strUserID); //EF員工代號
                //dicERPInfoSet.Add("strParameter3", strParameter3); //ERP 參數3
                //dicERPInfoSet.Add("strParameter4", strParameter4); //ERP 參數4
                //dicERPInfoSet.Add("lngKeyNumber", Convert.ToInt32(strKeyNumber)); //Key值個數
                //dicERPInfoSet.Add("strComfirmObject", strComfirmObject); //ERP確認元代號
                //dicERPInfoSet.Add("strComPRID", strComPRID); //ERP確認元程式代號
                //dicERPInfoSet.Add("strComDate", strComDate); //確認日期
                //dicERPInfoSet.Add("strUserNTID", strUserNTID); //EF使用者NT登入帳號
                //dicERPInfoSet.Add("strERPServerIP", strERPServerIP); //ERPServerIP
                //dicERPInfoSet.Add("strDebugMod", "Y");

                object[] aryTemp = (object[])callForERP.ChkERP_BeforeApprove(dicERPInfoSet);
                //sw.WriteLine("aryTemp[0]=" + Convert.ToString(aryTemp[0]));
                if (Convert.ToString(aryTemp[0]).Equals("Y"))
                {
                    comExeResult = erpGateWayEngine.saveERPInfoToTemp(dicERPInfoSet, strSiteName); //確認ERP是否可回寫
                    aryTemp = (object[])callForERP.SetERP_AfterApprove(dicERPInfoSet); //寫回審核結果到ERP
                    if (Convert.ToString(aryTemp[0]).Equals("N")) //執行有誤
                    {
                        isError = true;
                    }
					else
                    {
                        sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strDateNow + "', TF905='SPAD005' WHERE COMPANY='" + companyId + "' AND TF001='" + OriginatorGUID.ValueText + "' AND TF002='" + strWfSheetNo + "' AND TF003='" + strParameter3 + "'";
                        //erpEngine.executeSQL(sql);
                        if (!erpEngine.executeSQL(sql))
                        {
                            strResult += erpEngine.errorString;
                        }
                    }
                }
                else //執行有誤
                {
                    isError = true;
                }
                if (isError)
                {
                    string strErrReason = "執行表單簽核之前續處理作業時發生錯誤. ";
                    string strErrSource = "請假單 [確認過程] 程式代號=" + strProgID + " 單別=" + strWfFormID + " 單號=" + strWfSheetNo + " ";
                    string strErrDesc = "錯誤訊息=" + Convert.ToString(aryTemp[1]);
                    strResult += strErrReason + strErrSource + strErrDesc;
                }
                //sw.WriteLine("strResult=" + strResult);
                dicERPInfoSet = null;
            }
            callForERP = null;
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
        return strResult;
    }

    /// <summary>
    /// 作廢ERP單據
    /// </summary>
    private string widhDrawErpForm()
    {
        string strResult = "";
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        string sql = null;
        EFGateWayOfERP.Engine erpGateWayEngine = new EFGateWayOfERP.Engine();
        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            string connectString = (string)Session["connectString"];
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            erpEngine = base.getErpEngine();
            sp = new WebServerProject.SysParam(engine);
			string companyCode = CompanyCode.ValueText;
            string companyId = sp.getParam(companyCode + "SPADCompanyID");
            object strERPServerIP = sp.getParam("SPADComServerIP");
            string strEF2KWebSite = sp.getParam("EF2KWebSite");
            string strSheetNo = (string)getSession(base.PageUniqueID, "SheetNo");

            object strERPVersion = "3"; //ERP版本
            object strReturnType = "2"; //回寫狀態
            object strWfFormID = OriginatorGUID.ValueText; //表單單別（員工代號）
            object strAction = "2"; //確認狀態
            object strProgID = "PALI12"; //程式代號
            object strCompID = companyId; //公司別代號
            object strUserID = (string)Session["UserID"]; //審核人員工代號
            object strParameter4 = ""; //參數四	
            object strKeyNumber = "3"; //Key值個數
            object strComfirmObject = "TransManager.TxnManager"; //確認元
            object strComPRID = "PALI12"; //確認元程式代號
            string strComDate = "";  //DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //確認日期
            string strSiteName = "EF2KWeb";
            int idx = strEF2KWebSite.LastIndexOf("/");
            if (idx > 0)
            {
                strSiteName = strEF2KWebSite.Substring(idx + 1);
            }

            string strUserNTID = "LISA_LAI";
            sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID.ToString()) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strUserNTID = ds.Tables[0].Rows[0][0].ToString();
            }

            string[] tempSerialNos = TempSerialNo.ValueText.Split(';');
            for (int i = 0; i < tempSerialNos.Length; i++)
            {
                string[] arySerialNos = tempSerialNos[i].Split('-');
                string strWfSheetNo = arySerialNos[0]; //表單單號（請假日期）
                string strParameter3 = arySerialNos[1]; //流水號

                //Scripting.Dictionary dicERPInfoSet = new Scripting.Dictionary();
                //dicERPInfoSet.Add("lngERPVersion", Convert.ToInt32(strERPVersion)); //ERP版本
                //dicERPInfoSet.Add("lngReturnType", Convert.ToInt32(strReturnType)); //回寫狀態
                //dicERPInfoSet.Add("strWfFormID", strWfFormID); //ERP程式代號
                //dicERPInfoSet.Add("strWfSheetNo", strWfSheetNo); //ERP單號
                //dicERPInfoSet.Add("lngAction", Convert.ToInt32(strAction)); //Action
                //dicERPInfoSet.Add("strProgID", strProgID); //'EF表單代號
                //dicERPInfoSet.Add("strCompID", strCompID); //ERP公司別代號
                //dicERPInfoSet.Add("strUserID", strUserID); //EF員工代號
                //dicERPInfoSet.Add("strParameter3", strParameter3); //ERP 參數3
                //dicERPInfoSet.Add("strParameter4", strParameter4); //ERP 參數4
                //dicERPInfoSet.Add("lngKeyNumber", Convert.ToInt32(strKeyNumber)); //Key值個數
                //dicERPInfoSet.Add("strComfirmObject", strComfirmObject); //ERP確認元代號
                //dicERPInfoSet.Add("strComPRID", strComPRID); //ERP確認元程式代號
                //dicERPInfoSet.Add("strComDate", strComDate); //確認日期
                //dicERPInfoSet.Add("strUserNTID", strUserNTID); //EF使用者NT登入帳號
                //dicERPInfoSet.Add("strERPServerIP", strERPServerIP); //ERP Server IP
                //dicERPInfoSet.Add("strDebugMod", "Y");
                //string result = erpGateWayEngine.saveERPInfoToTemp(dicERPInfoSet, strSiteName);
                //object[] aryTemp = (object[])callForERP.SetERP_AfterApprove(dicERPInfoSet);

                //更新ERP該假單
                //MODIFYIER --最後修改者
                //MODI_DATE --最後修改日
                //FLAG --目前值加1
                //TF010 --備註欄
                //TF011 --Y 已確認, N 未確認, V 已作廢
                //TF012 --確認日期
                //TF013 --確認人員
                //TF904  --單號
                sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strComDate + "', FLAG = FLAG+1, TF011='V', TF012='" + strComDate + "', TF013='" + strUserID + "' WHERE COMPANY='" + strCompID + "' AND TF001='" + strWfFormID + "' AND TF002='" + strWfSheetNo + "' AND TF003='" + strParameter3 + "'";
                //sw.WriteLine(sql);
                erpEngine.executeSQL(sql);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
            if (erpEngine != null)
            {
                erpEngine.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }
        return strResult;
    }


    /// <summary>
    /// 請假日期時間是否有重覆
    /// </summary>
    /// <returns></returns>
    private string checkDataTime()
    {
        string strMessage = "";
        AbstractEngine engineErp = null;
        try
        {
            //檢查請假日期時間是否有重覆
            bool isErr = false;
            engineErp = getErpEngine();
            string originatorId = OriginatorGUID.ValueText;
            string startDate = HrStartTrvlDate.ValueText.Replace("/", "");
            string endDate = HrEndTrvlDate.ValueText.Replace("/", "");
            string startTime = "08:00";
            string endTime = "17:00";
            string startDateTime = startDate + startTime;
            string endDateTime = endDate + endTime;
            string sql = "";
            if (ChgStartTrvlDate.ValueText.Equals(ChgStartTrvlDate.ValueText))
            {
                sql = "select TF001 FROM PALTF WHERE TF001= '" + originatorId + "' AND TF011 IN ('Y','N') AND TF002='" + startDate + "' AND ((TF005 >= '" + startTime + "' AND TF006 <= '" + endTime + "') OR (TF005 < '" + startTime + "' AND TF006 > '" + startTime + "' AND TF006 <= '" + endTime + "') OR (TF005 >= '" + startTime + "' AND TF005 < '" + endTime + "' AND TF006 > '" + endTime + "'))";
                DataSet ds = engineErp.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    isErr = true;
                }
            }
            else
            {
                sql = "select TF001 FROM PALTF WHERE TF001= '" + originatorId + "' AND ((TF002+TF005 >= '" + startDateTime + "' AND TF002+TF006 <= '" + endDateTime + "') OR (TF002+TF005 < '" + startDateTime + "' AND TF002+TF006 > '" + startDateTime + "' AND TF002+TF006 <= '" + endDateTime + "') OR (TF002+TF005 >= '" + startDateTime + "' AND TF002+TF005 < '" + endDateTime + "' AND TF002+TF006 > '" + endDateTime + "')) AND TF011 IN ('Y','N')";
                DataSet ds = engineErp.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    isErr = true;
                }
            }
            if (isErr)
            {
                strMessage += "出差日期重覆可能與請假日期重覆，請確認。 若有問題請聯絡人事部門處理!\n";
            }

            
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (engineErp != null) engineErp.close();
        }

        return strMessage;
    }

    /// <summary>
    /// 利用SheetNo, 取得原出差相關單據資訊
    /// </summary>
    /// <param name="values"></param>
    protected void OriForeignForm_SingleOpenWindowButtonClick(string[,] values)
    {
        //MessageBox("OriForeignForm_SingleOpenWindowButtonClick");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
		
		string sql  = " ";
		string oriFormName = OriForeignForm.ValueText.Substring(0, 7);

        if (oriFormName.Equals("SPAD004"))
        {
	        sql  = " select AgentGUID, StartTrvlDate, EndTrvlDate, SiteUs, SiteJp, SiteKr, SiteSub, SiteOther, SiteUsDesc, SiteJpDesc, SiteKrDesc, SiteSubDesc, SiteOtherDesc, ";
	        sql += " FeeCharge, TrvlDesc, Passport, MTPs, MtpsPlus, USvisa, Other, OtherComment, DeliveryDate, CompleteDate, IdNumber, Birthday, ";
	        sql += " PrePayTwd, PrePayTwdAmt, PrePayCny, PrePayCnyAmt, PrePayUsd, PrePayUsdAmt, PrePayOther, PrePayOtherAmt, PrePayComment, ActualGetDate, GetMemberGUID ";
	        sql += " , PrePayJpy, PrePayJpyAmt, PrePayEur, PrePayEurAmt From SmpForeignTrvl where GUID='" + OriForeignForm.GuidValueText + "' ";
		}
		else
		{
	        sql  = " select AgentGUID, StartTrvlDate, EndTrvlDate, SiteUs, SiteJp, SiteKr, SiteSub, SiteOther, SiteUsDesc, SiteJpDesc, SiteKrDesc, SiteSubDesc, SiteOtherDesc, ";
	        sql += " FeeCharge, ChgTrvlDesc, Passport, MTPs, MtpsPlus, USvisa, Other, OtherComment, DeliveryDate, CompleteDate, IdNumber, Birthday, ";
	        sql += " ChgPrePayTwd, ChgPrePayTwdAmt, ChgPrePayCny, ChgPrePayCnyAmt, ChgPrePayUsd, ChgPrePayUsdAmt, ChgPrePayOther, ChgPrePayOtherAmt, PrePayComment, ActualGetDate, GetMemberGUID ";
	        sql += " , ChgPrePayJpy, ChgPrePayJpyAmt, ChgPrePayEur, ChgPrePayEurAmt From SmpForeignTrvlChg where GUID='" + OriForeignForm.GuidValueText + "' ";		
		}
        //MessageBox("Value : " + OriForeignForm.GuidValueText);
        //MessageBox("sql:" + sql);
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[36];
        string temp = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            //AgentGUID.ValueText = ds.Tables[0].Rows[0][0].ToString();
            AgentGUID.GuidValueText = ds.Tables[0].Rows[0][0].ToString();
            AgentGUID.doGUIDValidate();
            StartTrvlDate.ValueText = ds.Tables[0].Rows[0][1].ToString();
            EndTrvlDate.ValueText = ds.Tables[0].Rows[0][2].ToString();
            temp = ds.Tables[0].Rows[0][3].ToString();
            if (temp.Equals("Y")) { SiteUs.Checked = true; } else { SiteUs.Checked = false; }
            temp = ds.Tables[0].Rows[0][4].ToString();
            if (temp.Equals("Y")) { SiteJp.Checked = true; } else { SiteJp.Checked = false; }
            temp = ds.Tables[0].Rows[0][5].ToString();
            if (temp.Equals("Y")) { SiteKr.Checked = true; } else { SiteKr.Checked = false; }
            temp = ds.Tables[0].Rows[0][6].ToString();
            if (temp.Equals("Y")) { SiteSub.Checked = true; } else { SiteSub.Checked = false; }
            temp = ds.Tables[0].Rows[0][7].ToString();
            if (temp.Equals("Y")) { SiteOther.Checked = true; } else { SiteOther.Checked = false; }
            SiteUsDesc.ValueText = ds.Tables[0].Rows[0][8].ToString();
            SiteJpDesc.ValueText = ds.Tables[0].Rows[0][9].ToString();
            SiteKrDesc.ValueText = ds.Tables[0].Rows[0][10].ToString();
            SiteSubDesc.ValueText = ds.Tables[0].Rows[0][11].ToString();
            SiteOtherDesc.ValueText = ds.Tables[0].Rows[0][12].ToString();

            string feeCharge = Convert.ToString(ds.Tables[0].Rows[0][13].ToString());
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
                    FeeCharge5.Checked = true;
                    break;	
                default:
                    break;
            }

            TrvlDesc.ValueText = ds.Tables[0].Rows[0][14].ToString();
            
            temp = ds.Tables[0].Rows[0][25].ToString();
            if (temp.Equals("Y")) { PrePayTwd.Checked = true; } else { PrePayTwd.Checked = false; }
			if (temp.Equals("Y")) { ChgPrePayTwd.Checked = true; } else { ChgPrePayTwd.Checked = false; }
            PrePayTwdAmt.ValueText = ds.Tables[0].Rows[0][26].ToString();
			ChgPrePayTwdAmt.ValueText = ds.Tables[0].Rows[0][26].ToString();
			
            temp = ds.Tables[0].Rows[0][27].ToString();
            if (temp.Equals("Y")) { PrePayCny.Checked = true; } else { PrePayCny.Checked = false; }
			if (temp.Equals("Y")) { ChgPrePayCny.Checked = true; } else { ChgPrePayCny.Checked = false; }			
            PrePayCnyAmt.ValueText = ds.Tables[0].Rows[0][28].ToString();
			ChgPrePayCnyAmt.ValueText = ds.Tables[0].Rows[0][28].ToString();
			
            temp = ds.Tables[0].Rows[0][29].ToString();
            if (temp.Equals("Y")) { PrePayUsd.Checked = true; } else { PrePayUsd.Checked = false; }
			if (temp.Equals("Y")) { ChgPrePayUsd.Checked = true; } else { ChgPrePayUsd.Checked = false; }
            PrePayUsdAmt.ValueText = ds.Tables[0].Rows[0][30].ToString();
			ChgPrePayUsdAmt.ValueText = ds.Tables[0].Rows[0][30].ToString();
			
            temp = ds.Tables[0].Rows[0][31].ToString();
            if (temp.Equals("Y")) { PrePayOther.Checked = true; } else { PrePayOther.Checked = false; }
			if (temp.Equals("Y")) { ChgPrePayOther.Checked = true; } else { ChgPrePayOther.Checked = false; }
            PrePayOtherAmt.ValueText = ds.Tables[0].Rows[0][32].ToString();
			ChgPrePayOtherAmt.ValueText = ds.Tables[0].Rows[0][32].ToString();
			
            PrePayComment.ValueText = ds.Tables[0].Rows[0][33].ToString();
            ActualGetDate.ValueText = ds.Tables[0].Rows[0][34].ToString();
            GetMemberGUID.GuidValueText = ds.Tables[0].Rows[0][35].ToString();
            GetMemberGUID.doGUIDValidate();
			
			temp = ds.Tables[0].Rows[0][36].ToString();
            if (temp.Equals("Y")) { PrePayJpy.Checked = true; } else { PrePayJpy.Checked = false; }
			if (temp.Equals("Y")) { ChgPrePayJpy.Checked = true; } else { ChgPrePayJpy.Checked = false; }
            PrePayJpyAmt.ValueText = ds.Tables[0].Rows[0][37].ToString();
			ChgPrePayJpyAmt.ValueText = ds.Tables[0].Rows[0][37].ToString();
			
			temp = ds.Tables[0].Rows[0][38].ToString();
            if (temp.Equals("Y")) { PrePayEur.Checked = true; } else { PrePayEur.Checked = false; }
			if (temp.Equals("Y")) { ChgPrePayEur.Checked = true; } else { ChgPrePayEur.Checked = false; }
            PrePayEurAmt.ValueText = ds.Tables[0].Rows[0][39].ToString();
			ChgPrePayEurAmt.ValueText = ds.Tables[0].Rows[0][39].ToString();
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
            PrePayOtherAmt.ValueText = "";
            PrePayComment.ValueText = "";
            ActualGetDate.ValueText = "";
            GetMemberGUID.ValueText = "";
        }

       
    }
    protected void GetMemberGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            GetMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
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
	
	protected void PrintButton1_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["SPAD005_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印國外出差異動單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }
    protected void OriForeignForm_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriForeignForm.whereClause = " (BillID is null and SMWYAAA020 ='Y' and UserId='" + OriginatorGUID.ValueText + "' and (ChgID not in (select c.GUID from SmpForeignTrvlChg c, SMWYAAA w where w.SMWYAAA019=c.GUID and w.SMWYAAA020  in ( 'I','Y' ) )  or ChgID is null )) ";
        }
    }
}