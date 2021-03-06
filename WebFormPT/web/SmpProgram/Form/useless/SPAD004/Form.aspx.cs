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
//using System.Data.OracleClient;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;


public partial class SmpProgram_System_Form_SPAD004_Form : SmpAdFormPage
{

    protected override void init()
    {	
		ProcessPageID = "SPAD004"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD004.SmpForeignTrvlAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
	}


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        //Subject.Display = false;
        IsIncludeDateEve.Display = false;
        TempSerialNo.Display = false;
        ClassCode.Display = false;
        CategoryCode.Display = false;
        IsIncludeHoliday.Display = false;
		FeeCharge.Display = false;
		//PersonButtonSearch.Display = false;
		//PersonButtonSearch.Enabled = true;

		//sw.WriteLine("line 45 ");
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
            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad004_form_aspx.language.ini", "message", "smp", "新普科技")},
            {"TE",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad004_form_aspx.language.ini", "message", "te", "嘉普科技")},
            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad004_form_aspx.language.ini", "message", "tp", "中普科技")}
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

        //Radio Group, Charge Fee
        //FeeCharge1.GroupName = "grp1";
        //FeeCharge2.GroupName = "grp1";
        //FeeCharge3.GroupName = "grp1";
        //FeeCharge4.GroupName = "grp1";  --取消合普
		//FeeCharge5.GroupName = "grp1";
		//FeeCharge6.GroupName = "grp1";

        //班別
        ClassCode.ValueText = "SD4"; //班別 (SD4:常日班)
        //假別
        CategoryCode.ValueText = "LI04";
        //請假是否含假日, 不含
        IsIncludeHoliday.ValueText = "Y";

        //新建立表單不可維護欄位
        //DeliveryDate.ReadOnly = true;
        //CompleteDate.ReadOnly = true;
        GetMemberGUID.ReadOnly = true;
        ActualGetDate.ReadOnly = true;
        PrePayComment.ReadOnly = true;
		
				
		//請出差人員出差前,務必去出差專區閱讀出差須知連結
		WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string eTripURL = sp.getParam("eTripURL"); //系統參數
        TripHyperLink.NavigateUrl = eTripURL;
		
        //改變工具列順序
        base.initUI(engine, objects);
        witeLogSMP(Convert.ToString(Session["UserID"]), OriginatorGUID.ValueText, OriginatorGUID.ReadOnlyValueText);
		//writeLog("initUI^end");

    }

    private void witeLogSMP(string UserID, string fillerID, string fillerName)
    {
        try
        {
            writeLog(new Exception(DateTime.Now.ToString("yyyy/mm/dd HH:mm:ss.ff") + " Sessin[\"UserID\"]=" + UserID + " fillerID=" + fillerID + " fillerName=" + fillerName), false);
        }
        catch { }
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {	
		//writeLog("showData^start");
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        //SheetNo.Display = false;
        //Subject.Display = false;
		TempSerialNo.Display = false;
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		//公司別
        CompanyCode.ValueText = objects.getData("CompanyCode");
		
        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID

        //申請單位
        DeptGUID.ValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
                
        Title.ValueText = objects.getData("Title"); //職稱
        
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

        //Fee Charge  費用負擔     
        string feeCharge = Convert.ToString(objects.getData("FeeCharge"));
        //switch (feeCharge)
        //{
        //    case "0":
        //        FeeCharge1.Checked = true;
        //        break;
        //    case "1":
        //        FeeCharge2.Checked = true;
        //        break;
        //    case "2":
        //        FeeCharge3.Checked = true;
        //        break;
            //case "3":
            //    FeeCharge4.Checked = true;
            //    break;
		//	case "4":
        //        FeeCharge5.Checked = true;
        //        break;
		//	case "5":
        //        FeeCharge6.Checked = true;
        //        break;		
        //    default:
        //        break;
        //}

        TrvlDesc.ValueText = objects.getData("TrvlDesc");

        StartTrvlDate.ValueText = objects.getData("StartTrvlDate");
        EndTrvlDate.ValueText = objects.getData("EndTrvlDate");

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
        SheetNo.ValueText = objects.getData("SheetNo"); //傳入序號, 找到個資
		
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
            //Passport.ReadOnly = true;
            //MTPs.ReadOnly = true;
            //MtpsPlus.ReadOnly = true;
            //USvisa.ReadOnly = true;
            //Other.ReadOnly = true;
            //DeliveryDate.ReadOnly = true;
            //CompleteDate.ReadOnly = true;
            StartTrvlDate.ReadOnly = false;
            EndTrvlDate.ReadOnly = false;
            //FeeCharge1.ReadOnly = true;
            //FeeCharge2.ReadOnly = true;
            //FeeCharge3.ReadOnly = true;
            //FeeCharge4.ReadOnly = true;
			//FeeCharge5.ReadOnly = true;
			//FeeCharge6.ReadOnly = true;
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
            //IdNumber.ReadOnly = true;
            //Birthday.ReadOnly = true;
            PrePayTwd.ReadOnly = true;
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
            //OtherComment.ReadOnly = true;
			//Beneficiary.ReadOnly = true;
            //Relationship.ReadOnly = true;

            DateTime sDate = Convert.ToDateTime(StartTrvlDate.ValueText);
            DateTime eDate = Convert.ToDateTime(EndTrvlDate.ValueText);
            TimeSpan ts = eDate - sDate ;
            double days = ts.TotalDays + 1;
            lbTrvlDateSumValue.Text = "出差天數：" + Convert.ToInt32(days).ToString() + " 天";

            //string strValue = objects.getData("IdNumber");
			//string strLoginID = (string)Session["UserID"];
			//if (strLoginID.Equals("2506")){
				///MessageBox("UserID : " + (string)Session["UserID"]);
				//IdNumber.ValueText = objects.getData("IdNumber");
	            //Birthday.ValueText = objects.getData("Birthday");
			//}else {
			//string userIDNo = objects.getData("IdNumber");
			//string tmpValue =  userIDNo.Substring(0,3) + "*******";
			//IdNumber.ValueText = tmpValue;
				//MessageBox("IdNumber : " + tmpValue);
			//string userBirthday = objects.getData("Birthday");
			//tmpValue =  "**/" + userBirthday.Substring(3,2) + "/**";
			//Birthday.ValueText = tmpValue;
				//MessageBox("Birthday : " + tmpValue);
			//}
			//userBeneficiary = objects.getData("Beneficiary"); //身故受益人
			//if (!userBeneficiary.Equals(""))
			//{
			//	tmpValue =  "*" + userBeneficiary.Substring(1,1) + "*";			
			//	Beneficiary.ValueText = tmpValue;
			//}
			
			//userRelationship = objects.getData("Relationship"); //與受益人關係;
			//if (!userRelationship.Equals(""))
			//{
			//	tmpValue =  "*****";
			//Relationship.ValueText = tmpValue;
			//}
			
        }

        if (isAddNew)
        {
            //lbTrvlDateSum.Display = false;
            lbTrvlDateSumValue.Display = false;
            //lbTrvlDateSumD.Display = false;
			ActualGetDate.ValueText = "";
			GetMemberGUID.ValueText = "";
			GetMemberGUID.ReadOnlyValueText = "";
			
			// 20161007 Eva增加 TE
			if (objects.getData("CompanyCode").Equals("TE"))
			{
				LblOtherDesc.Text = "嘉普人員至105/12/31止國外出差費請款(發票、收據等)請使用中普統一編號:54542191";
			}
        }

        //有預辦證件時, 簽到秘書關卡才可維護此兩欄位
        if (actName.Equals("Secretary"))
        {
            //DeliveryDate.ReadOnly = false;     
            //CompleteDate.ReadOnly = false;
        }

        if (actName.Equals("財務出納"))
        {
            PrePayComment.ReadOnly = false;
            ActualGetDate.ReadOnly = false;
            GetMemberGUID.ReadOnly = false;
            
            PrePayTwd.ReadOnly = false;
            PrePayTwdAmt.ReadOnly = false;
            PrePayCny.ReadOnly = false;
            PrePayCnyAmt.ReadOnly = false;
            PrePayUsd.ReadOnly = false;
            PrePayUsdAmt.ReadOnly = false;
			PrePayJpy.ReadOnly = false;
            PrePayJpyAmt.ReadOnly = false;
			PrePayEur.ReadOnly = false;
            PrePayEurAmt.ReadOnly = false;
            PrePayOther.ReadOnly = false;
            PrePayOtherAmt.ReadOnly = false;
			StartTrvlDate.ReadOnly = true;
            EndTrvlDate.ReadOnly = true;            
        }
		
		if (actName.Equals("董事長"))
        {
            PrePayTwd.ReadOnly = false;
            PrePayTwdAmt.ReadOnly = false;
            PrePayCny.ReadOnly = false;
            PrePayCnyAmt.ReadOnly = false;
            PrePayUsd.ReadOnly = false;
            PrePayUsdAmt.ReadOnly = false;
			PrePayJpy.ReadOnly = false;
            PrePayJpyAmt.ReadOnly = false;
			PrePayEur.ReadOnly = false;
            PrePayEurAmt.ReadOnly = false;
            PrePayOther.ReadOnly = false;
            PrePayOtherAmt.ReadOnly = false;
			StartTrvlDate.ReadOnly = false;
            EndTrvlDate.ReadOnly = false; 
        }
		
		if (actName.Equals("差勤負責人"))
        {
            StartTrvlDate.ReadOnly = true;
            EndTrvlDate.ReadOnly = true;
        }
		
		//當登入者為2506Melinda, 才可顯示個資按鈕
		//PersonButtonSearch.Display = false;
		
		string personButtonUser = (string)Session["UserID"];
		//MessageBox("personButtonUser : " + personButtonUser);	
		
		if (personButtonUser.Equals("2506"))
		{
			//PersonButtonSearch.Display = true;
			//PersonButtonSearch.Enabled = true;
		//	MessageBox("personButtonUser : " + personButtonUser);	
		}
        
		//writeLog("showData^end");
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        try
        {
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

                //FeeCharge 費用負擔
                //FeeCharge 費用負擔
                string feeCharge = "0";
				string feeName = CompanyCode.ValueText;
                //if (FeeCharge1.Checked){
                //    feeCharge = "0";  // 新普
				//	feeName = "新普";
				//}
                //if (FeeCharge2.Checked){
                //    feeCharge = "1";  //STCS
				//	feeName = "STCS";
				//}
                //if (FeeCharge3.Checked){
                //    feeCharge = "2"; //SCQ
				//	feeName = "SCQ";
				//}
                //if (FeeCharge4.Checked)
                //    feeCharge = "3";  //HOPE
				//if (FeeCharge5.Checked){
                //    feeCharge = "4";  //TP
				//	feeName = "中普";
				//}
				//if (FeeCharge6.Checked){
                //    feeCharge = "5";  //STTP
				//	feeName = "太普";
				//}
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
					prePayName += PrePayComment.ValueText + "-" + PrePayOtherAmt.ValueText + ";  ";
				}
                objects.setData("ActualGetDate", ActualGetDate.ValueText);
                objects.setData("GetMemberGUID", GetMemberGUID.GuidValueText);
				
				objects.setData("TrvlSiteName", trvlSiteName);
				objects.setData("FeeName", feeName);
				objects.setData("PrePayName", prePayName);

                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
                //sw.WriteLine("主旨含單號");
            }
			//C 預支申請
            if (PrePayTwd.Checked) { objects.setData("PrePayTwd", "Y"); } else { objects.setData("PrePayTwd", "N"); }
            if (PrePayCny.Checked) { objects.setData("PrePayCny", "Y"); } else { objects.setData("PrePayCny", "N"); }			
            if (PrePayUsd.Checked) { objects.setData("PrePayUsd", "Y"); } else { objects.setData("PrePayUsd", "N"); }
			if (PrePayJpy.Checked) { objects.setData("PrePayJpy", "Y"); } else { objects.setData("PrePayJpy", "N"); }
			if (PrePayEur.Checked) { objects.setData("PrePayEur", "Y"); } else { objects.setData("PrePayEur", "N"); }
            if (PrePayOther.Checked) { objects.setData("PrePayOther", "Y"); } else { objects.setData("PrePayOther", "N"); }
            objects.setData("StartTrvlDate", StartTrvlDate.ValueText);
            objects.setData("EndTrvlDate", EndTrvlDate.ValueText);
            objects.setData("PrePayComment", PrePayComment.ValueText);
            objects.setData("ActualGetDate", ActualGetDate.ValueText);
            objects.setData("GetMemberGUID", GetMemberGUID.GuidValueText);
			objects.setData("PrePayTwdAmt", PrePayTwdAmt.ValueText);
            objects.setData("PrePayCnyAmt", PrePayCnyAmt.ValueText);
            objects.setData("PrePayUsdAmt", PrePayUsdAmt.ValueText);
			objects.setData("PrePayJpyAmt", PrePayJpyAmt.ValueText);
			objects.setData("PrePayEurAmt", PrePayEurAmt.ValueText);
            objects.setData("PrePayOtherAmt", PrePayOtherAmt.ValueText);
			//objects.setData("Beneficiary", Beneficiary.ValueText);
			//objects.setData("Relationship", Relationship.ValueText);

         
            //當董事長關卡前才產生假單序號
			/*
            if (actName.Equals("董事長"))
			{                
                object[] aryTemp = callDataCenter("6", StartTrvlDate.ValueText, EndTrvlDate.ValueText);
                if (Convert.ToString(aryTemp[0]).Equals("Y"))
                {
                    string tempSerialNo = Convert.ToString(aryTemp[2]);
                    objects.setData("TempSerialNo", tempSerialNo);
                    //TempSerialNo.ValueText = tempSerialNo; //為何要多做?
                    engine.updateData(objects);
                    //寫字串到ERP備註欄位
                    updateErpForm(objects);
                }
            }
			*/

			//beforeSetFlow
	        setSession("IsSetFlow", "Y");

	        //beforeSign 加簽
	        //string actName = Convert.ToString(getSession("ACTName").ToString());
	        if (actName.Equals("SPAD001_HRADM") || actName.Equals("差勤負責人"))
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
        //sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\spad004.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
        //sw.WriteLine("checkFieldData ");
        
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);

        //sw.WriteLine("actName=>> " + actName);        

        if (actName.Equals(""))
        {
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
            //DateTime startDateTime = Convert.ToDateTime(StartTrvlDate.ValueText);
            //DateTime endDateTime = Convert.ToDateTime(EndTrvlDate.ValueText);
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
            //if (FeeCharge1.Checked.Equals(false) && FeeCharge2.Checked.Equals(false) && FeeCharge3.Checked.Equals(false) && FeeCharge5.Checked.Equals(false) && FeeCharge6.Checked.Equals(false))
            //{
            //    strErrMsg += "請點選 費用負擔\n";
            //}

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
			if (!PrePayTwdAmt.ValueText.Equals("")){
				if (PrePayTwd.Checked.Equals(false))
				{
					strErrMsg += "請勾選 預支申請 新台幣 金額\n";
				}
			}			
            if (PrePayCny.Checked.Equals(true))
            {
                if (PrePayCnyAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 人民幣 金額\n";
                }
            }
			if (!PrePayCnyAmt.ValueText.Equals("")){
				if (PrePayCny.Checked.Equals(false))
				{
					strErrMsg += "請勾選 預支申請 人民幣 \n";
				}
			}
            if (PrePayUsd.Checked.Equals(true))
            {
                if (PrePayUsdAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 美金 金額\n";
                }
            }
			if (!PrePayUsdAmt.ValueText.Equals("")){
				if (PrePayUsd.Checked.Equals(false))
				{
					strErrMsg += "請勾選 預支申請 美金\n";
				}
			}
			if (PrePayJpy.Checked.Equals(true))
            {
                if (PrePayJpyAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 日圓 金額\n";
                }
            }
			if (!PrePayJpyAmt.ValueText.Equals("")){
				if (PrePayJpy.Checked.Equals(false))
				{
					strErrMsg += "請勾選 預支申請 日圓\n";
				}
			}
			if (PrePayEur.Checked.Equals(true))
            {
                if (PrePayEurAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 歐元 金額\n";
                }
            }
			if (!PrePayEurAmt.ValueText.Equals("")){
				if (PrePayEur.Checked.Equals(false))
				{
					strErrMsg += "請勾選 預支申請 歐元\n";
				}
			}
            if (PrePayOther.Checked.Equals(true))
            {
                if (PrePayOtherAmt.ValueText.Equals(""))
                {
                    strErrMsg += "請說明 預支申請 其他幣別與金額\n";
                }
            }
			if (!PrePayOtherAmt.ValueText.Equals("")){
				if (PrePayOther.Checked.Equals(false))
				{
					strErrMsg += "請勾選 預支申請 其他幣別\n";
				}
			}
			
			if (!StartTrvlDate.ValueText.Equals("") && !EndTrvlDate.ValueText.Equals(""))
            {
				if (Convert.ToDateTime(StartTrvlDate.ValueText) > Convert.ToDateTime(EndTrvlDate.ValueText))
				{
					strErrMsg += "預計出差日期起不可大於預計出差日期訖\n";
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


            //請假日期時間有重覆!
            string strTemp = checkDataTime();
            if (!strTemp.Equals(""))
            {
                strErrMsg += strTemp;
            }


            //設定主旨由系統自動帶入
	        //if (Subject.ValueText.Equals(""))
            //{
                values = base.getUserInfoById(engine, OriginatorGUID.ValueText);
                string subject = "【出差人員：" + values[1] + " 預計出差日期: " +　StartTrvlDate.ValueText + " ~ " +　EndTrvlDate.ValueText +　"】";
                //if (Subject.ValueText.Equals(""))
                //{
                    Subject.ValueText = subject;
                //}
            //}        
        }
		
		if (actName.Equals("財務出納"))
        {
            //MessageBox("fin-casher");
			if (!PrePayTwdAmt.ValueText.Equals("") || !PrePayCnyAmt.ValueText.Equals("") || !PrePayUsdAmt.ValueText.Equals("") ||  !PrePayJpyAmt.ValueText.Equals("") || !PrePayEurAmt.ValueText.Equals("") || !PrePayOtherAmt.ValueText.Equals(""))
            {
				//strErrMsg += "12456\n";
                if (ActualGetDate.ValueText.Equals("") || GetMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護 實際領取日 與 領取人";
                }
            }
			//strErrMsg += "12456\n";
        }
        //if (actName.Equals("Secretary"))
        //{
        //    if (DeliveryDate.ValueText.Equals(""))
        //    {
        //        strErrMsg += "請維護送件日期!\n";
        //    }
        //    //DeliveryDate.ReadOnly = false;
        //}
        //if (actName.Equals("Secretary2"))
        //{
        //    if (CompleteDate.ValueText.Equals(""))
        //    {
        //        strErrMsg += "請維護完成日期!\n";
        //    }
        //}


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
        si.ownerName = (string)Session["UserName"];
		//si.ownerID = OriginatorGUID.ValueText;        
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0]; //發起單位代號
        si.objectGUID = objects.getData("GUID");
		si.important = "1"; //重要性 0:低; 1:中 2:高

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
		si.important = "1"; //重要性 0:低; 1:中 2:高
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
        
        //System.IO.StreamWriter sw = null;
        string xml = "";
		string[] values = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
            //sw.WriteLine("setFlowVariables");
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            
            //string agentId = AgentGUID.ValueText; //代理人
			//代理人, 使用表單中定義checkby2
			string agentGUID = currentObject.getData("AgentGUID");
			values = base.getUserInfo(engine, agentGUID);
			string agentId = values[0];
			
            string notifierId = agentId + ";"; //通知人員
			string deptAsstId = ""; //部門收發
            string isChairman = ""; //是否下一關為董事長
	        string[] deptInfo = base.getDeptInfo(engine, currentObject.getData("OriginatorGUID"));
	        if (!deptInfo[0].Equals(""))
	        {
	            string[] userFunc = getUserRoles(engine, "部門收發", deptInfo[0]);
	            deptAsstId = userFunc[2];
	        }			

            //填表人
            string creatorId = si.fillerID;
            //sw.WriteLine("creatorId=>" + creatorId);

            //出差人員
			string originatorGUID = currentObject.getData("OriginatorGUID");
			values = base.getUserInfo(engine, originatorGUID);
			string originatorId = values[0];

            //出差人員的主管
			values = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = values[0];
            //string originatorGUID = OriginatorGUID.GuidValueText;
            //sw.WriteLine("originatorGUID=" + originatorGUID);
           // string[] values = base.getUserManagerInfo(engine, originatorGUID);
            //string managerGUID = values[0];
            
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];

            if (managerId.Equals("1356"))
            {
                isChairman = "Y";
            }

            //sw.WriteLine("managerId=" + managerId);
			//如果為特定部門時, 加簽特助, 
			
			string deptId = DeptGUID.ValueText;
			string deptIdStr2 = deptId.Substring(0, 2);
			string actName = Convert.ToString(getSession("ACTName"));
			//sw.WriteLine("deptIdStr2=>" + deptIdStr2 + "  -- actName=>"+ actName);
			writeLog("deptIdStr2 : " + deptIdStr2 + " -deptId :"+ deptId + " - actName:" + actName);
			if (!actName.Equals("填表人") && !actName.Equals("Secretary簽核") && !actName.Equals("審核人") && !managerId.Equals("00001") && !managerId.Equals("T0001") && !managerId.Equals("1356"))
			{
				if (deptIdStr2.Equals("GZ")||deptIdStr2.Equals("EZ")||deptIdStr2.Equals("LZ")||deptIdStr2.Equals("BZ")||deptIdStr2.Equals("GJ")||deptIdStr2.Equals("EJ")||deptIdStr2.Equals("LJ")||deptIdStr2.Equals("BJ")) 
				{		
					if (!deptId.Equals("LJR6000") && !deptId.Equals("LJS1000"))
					{
						isChairman = "CM";
					}
				}
				
            }
			

            //審核人員
            string checkByGUID = currentObject.getData("CheckByGUID");
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }
            //sw.WriteLine("checkById=" + checkById);            

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
            string strPreDoc = "N";  //是否預辦證件
            
            string strNotify1 = "";  //
            string strNotify2 = "";  //通知秘書、代理人
            if (!agentGUID.Equals(""))
            {
                strNotify2 += agentId + (";2506;") + creatorId + (";") ;
	            if (!creatorId.Equals(originatorId))
				{
					strNotify2 += originatorId + (";");
				}
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
            //費用付擔
            string feeCharge = "0";
            //if (FeeCharge1.Checked) feeCharge = "0";
            //if (FeeCharge2.Checked) feeCharge = "1";
            //if (FeeCharge3.Checked) feeCharge = "2";
            //if (FeeCharge4.Checked) feeCharge = "3";
			//if (FeeCharge5.Checked) feeCharge = "4";
			//if (FeeCharge6.Checked) feeCharge = "5";	
            string strTrvlSite = feeCharge;//費用付擔, 判斷是否加簽各公司別最高主管 

            //sw.WriteLine("managerId=" + managerId);
            xml += "<SPAD004>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";            //填表人 
            xml += "<ssp DataType=\"java.lang.String\">" + strSSP + "</ssp>";                       //是否為替代役人員, 預設為N
            xml += "<secretary DataType=\"java.lang.String\">SMP-SECRETARY</secretary>";            //秘書
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";          //審核人
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";            //直屬主管
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";   //出差人員
            xml += "<trvlsite DataType=\"java.lang.String\">" + strTrvlSite + "</trvlsite>";        //TrvlSite
            xml += "<notify1 DataType=\"java.lang.String\">" + strNotify1 + "</notify1>";           //通知人員1
            xml += "<notify2 DataType=\"java.lang.String\">" + strNotify2 + "</notify2>";           //通知人員2
            xml += "<notify3 DataType=\"java.lang.String\">" + strNotify3 + "</notify3>";           //通知人員3
            xml += "<chairman DataType=\"java.lang.String\">1356</chairman>";                       //董事長
            xml += "<casher DataType=\"java.lang.String\">SMP-CASHER</casher>";                     //財務出納
            xml += "<hradm DataType=\"java.lang.String\">SMP-HRADM</hradm>";                        //差勤負責人
            xml += "<notify4 DataType=\"java.lang.String\"></notify4>";                             //審核人2, 預留欄位
            xml += "<ssphr DataType=\"java.lang.String\">SPAD004-HRSSP</ssphr>";                    //通知人員(HR替代役)
            xml += "<predoc DataType=\"java.lang.String\">" + strPreDoc + "</predoc>";              //預辦證件
            xml += "<stcsvp DataType=\"java.lang.String\">SPAD004-ChargeSTCS</stcsvp>";             //ChargeSTCS
            xml += "<scqvp DataType=\"java.lang.String\">SPAD004-ChargeSCQ</scqvp>";                //ChargeSCQ
            xml += "<hopevp DataType=\"java.lang.String\">SPAD004-ChargeHOPE</hopevp>";             //ChargeHOPE
            xml += "<checkby2 DataType=\"java.lang.String\">" + isChairman + "</checkby2>";         //CheckBy2, 若主管為董事長, 則直屬
            xml += "</SPAD004>";
            //sw.WriteLine("xml: " + xml);
			
			//string actName = Convert.ToString(getSession("ACTName").ToString());
			string strStartDate = currentObject.getData("StartTrvlDate");
			string strEndDate = currentObject.getData("EndTrvlDate");
			string strLoginUser = (string)Session["UserID"];
			writeLog("actName : " + actName);
			writeLog("strLoginUser : " + strLoginUser);
			
			//當董事長關卡前才產生假單序號
            if (actName.Equals("董事長") || strLoginUser.Equals("1356"))
			{                
				//writeLog("strStartDate : " + strStartDate + " ~ " +strEndDate);
                object[] aryTemp = callDataCenter("6", strStartDate, strEndDate, originatorId);
				writeLog("aryTemp[0] : " + Convert.ToString(aryTemp[0]));
                if (Convert.ToString(aryTemp[0]).Equals("Y"))
                {
                    string tempSerialNo = Convert.ToString(aryTemp[2]);
					writeLog("tempSerialNo : " + tempSerialNo);
                    currentObject.setData("TempSerialNo", tempSerialNo);
                    //TempSerialNo.ValueText = tempSerialNo; //為何要多做?
                    engine.updateData(currentObject);
                    //寫字串到ERP備註欄位
                    updateErpForm(currentObject);
                }
				
            }

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
        param["SPAD004"] = xml;
        return "SPAD004";
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
			
			// 20161007 Eva增加 TE
			if (userValues[5].Equals("TE"))
			{
				LblOtherDesc.Text = " ** 嘉普人員至105/12/31止國外出差費請款(發票、收據等)請使用中普統一編號:54542191 ** ";
			}else{
				LblOtherDesc.Text = "";
			}		

        }
        else
        {
            DeptGUID.ValueText = "";
			CompanyCode.ValueText = "";
			LblOtherDesc.Text = "";
        }
        
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
		System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\spad004.log", true);
		//sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
        string actName = Convert.ToString(getSession("ACTName").ToString());
        //sw.WriteLine("actName: " +actName);    
		//sw.WriteLine("TempSerialNo: " +TempSerialNo.ValueText);    
		//sw.Close();		
        if (actName.Equals("差勤負責人"))
        {
            //確認ERP單據
            string result = approveErpForm();			
            if (!result.Equals(""))
            {
                throw new Exception(result);
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
        //退回填表人終止流程				
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        
        if (backActID.ToUpper().Equals("CREATOR") )
        {
            try
            {
                if (!TempSerialNo.ValueText.Equals(""))
                {
                    //widhDrawErpForm();
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
			string strTempSerialNo = currentObject.getData("IsCustomFlow");

			if (!strTempSerialNo.Equals(""))
            {
				widhDrawErpForm(engine, currentObject);;
            }
            
        }
        base.afterApprove(engine, currentObject, result);
    }


    /// <summary>
    /// 透過元件資料相關資料
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private object[] callDataCenter(string id, string strSDate, string strEDate, string tripUser )
    {
        object[] aryTemp = null; //回傳值, object 陣列
        object[] aryCompanyData = new object[2];
        object[] aryData = null;

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
                        aryData[6] = StartTrvlDate.ValueText.Replace(":", ""); //請假時間起
                        aryData[7] = EndTrvlDate.ValueText.Replace(":", ""); //請假時間迄
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
                        aryData[1] = tripUser; //員工代號
                        aryData[2] = strSDate.Replace("/", ""); //起始請假日期
                        aryData[3] = strEDate.Replace("/", ""); //截止請假日期
                        //aryData[4] = CategoryCode.ValueText; //假別
						aryData[4] = "LI04"; //假別
                        //aryData[5] = IsIncludeHoliday.ValueText; //請假是否含假日
						aryData[5] = "Y"; //請假是否含假日
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
            sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            string connectString = (string)Session["connectString"];
            engine = factory.getEngine(EngineConstants.SQL, connectString);
            erpEngine = base.getErpEngine();
            sp = new WebServerProject.SysParam(engine);
			string companyId = CompanyCode.ValueText;
            string tempSerialNo = currentObject.getData("TempSerialNo");
            //sw.WriteLine(tempSerialNo);
            string[] aryTempSerialNo = tempSerialNo.Split(';');
            string strUserID = (string)Session["UserID"];
            string strDateNow = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
            string strSheetNo = currentObject.getData("SheetNo");
            sw.WriteLine(strSheetNo);
            string strComment = strSheetNo + " - " + Subject.ValueText;
            if (strComment.Length > 100)
            {
                strComment.Substring(0, 100);
            }
			strComment = strComment.Replace("'","''");

            erpEngine = base.getErpEngine();
            for (int i = 0; i < aryTempSerialNo.Length; i++)
            {
                string[] aryWfFormIDs = aryTempSerialNo[i].Split('-');
                for (int j = 0; j < aryWfFormIDs.Length; j++)
                {
                    string strWfFormID = aryWfFormIDs[0];
                    string strWfSerialNo = aryWfFormIDs[1];
                    sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strDateNow + "', FLAG = 1, TF010='" + strComment + "', TF904='" + strSheetNo + "' WHERE COMPANY='" + companyId + "' AND TF001='" + OriginatorGUID.ValueText + "' AND TF002='" + strWfFormID + "' AND TF003='" + strWfSerialNo + "'";
					//sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strDateNow + "', FLAG = 1, TF010='test!!TP_nodata', TF904='" + strSheetNo + "' WHERE COMPANY='" + companyId + "' AND TF001='" + OriginatorGUID.ValueText + "' AND TF002='" + strWfFormID + "' AND TF003='" + strWfSerialNo + "'";
                    sw.WriteLine(sql);
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

        try
        {            
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
            //string sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID) + "'";
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
			writeLog( "TempSerialNo : " + TempSerialNo.ValueText) ;
			
            for (int i = 0; i < tempSerialNos.Length; i++)
            {
                string[] arySerialNos = tempSerialNos[i].Split('-');
                object strWfSheetNo = arySerialNos[0]; //表單單號（請假日期）
                object strParameter3 = arySerialNos[1]; //流水號

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
                        sql = "UPDATE PALTF SET MODIFIER='" + strUserID + "', MODI_DATE='" + strDateNow + "', TF905='SPAD004' WHERE COMPANY='" + companyId + "' AND TF001='" + OriginatorGUID.ValueText + "' AND TF002='" + strWfSheetNo + "' AND TF003='" + strParameter3 + "'";
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
				writeLog("strResult=" + strResult);
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
            //if (sw != null)
            //{
            //    sw.Close();
            //}
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
            string strERPServerIP = sp.getParam("SPADComServerIP");
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
            //sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID) + "'";
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
    /// 作廢ERP單據
    /// </summary>
    private string widhDrawErpForm(AbstractEngine engine, DataObject currentObject)
    {
        string strResult = "";
        IOFactory factory = new IOFactory();
        AbstractEngine erpEngine = null;
        WebServerProject.SysParam sp = null;
        string sql = null;
        EFGateWayOfERP.Engine erpGateWayEngine = new EFGateWayOfERP.Engine();
        EFGateWayOfERP.CallForERP callForERP = new EFGateWayOfERP.CallForERP();
        System.IO.StreamWriter sw = null;
        try
        {
            string companyCode = currentObject.getData("CompanyCode");
            erpEngine = base.getErpEngine(companyCode);
            sp = new WebServerProject.SysParam(engine);
            string companyId = sp.getParam(companyCode + "SPADCompanyID");
            string strERPServerIP = sp.getParam("SPADComServerIP");
            string strEF2KWebSite = sp.getParam("EF2KWebSite");
            string strSheetNo = currentObject.getData("SheetNo");
            
            string strERPVersion = "3"; //ERP版本
            string strReturnType = "2"; //回寫狀態
            string[] values = base.getUserInfo(engine, currentObject.getData("OriginatorGUID"));
            string strWfFormID = values[0]; //表單單別（員工代號）
            string strAction = "2"; //確認狀態
            string strProgID = "PALI12"; //程式代號
            string strCompID = companyId; //公司別代號
            string strUserID = (string)Session["UserID"]; //審核人員工代號
            if (string.IsNullOrEmpty(strUserID))
            {
                string[][] users = base.getGroupdUser(engine, "SMP-HRADM");
                strUserID = users[0][0];
            }
			string strParameter4 = ""; //參數四	
            string strKeyNumber = "3"; //Key值個數
            string strComfirmObject = "TransManager.TxnManager"; //確認元
            string strComPRID = "PALI12"; //確認元程式代號
            string strComDate = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", ""); //確認日期
            string strSiteName = "EF2KWeb";
            int idx = strEF2KWebSite.LastIndexOf("/");
            if (idx > 0)
            {
                strSiteName = strEF2KWebSite.Substring(idx + 1);
            }

            string strUserNTID = "LISA_LAI";
            sql = "select left(ldapid, charindex('@', ldapid)-1) from Users where id = '" + Utility.filter(strUserID) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strUserNTID = ds.Tables[0].Rows[0][0].ToString();
            }
            
            string[] tempSerialNos = currentObject.getData("TempSerialNo").Split(';');
            for(int i=0; i<tempSerialNos.Length; i++) {
                string[] arySerialNos = tempSerialNos[i].Split('-');
                string strWfSheetNo = arySerialNos[0]; //表單單號（請假日期）
                string strParameter3 = arySerialNos[1]; //流水號

               
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
                writeLog(sql);
				
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
            string startDate = StartTrvlDate.ValueText.Replace("/", "");
            string endDate = EndTrvlDate.ValueText.Replace("/", "");
            string startTime = "0800";
            string endTime = "1700";
            string startDateTime = startDate + startTime;
            string endDateTime = endDate + endTime;
            string sql = "";
            if (StartTrvlDate.ValueText.Equals(EndTrvlDate.ValueText))
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
	
	protected void PersonButtonSearch_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["SPAD004_SheetNo"] = SheetNo.ValueText;
        string url = "PersonSummary.aspx";
        base.showOpenWindow(url, "個資查詢", "", "150", "", "", "", "1", "1", "", "", "", "", "360", "", true);
    }
	
	protected void PrintButton_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["SPAD004_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印國外出差單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }
	protected void PrintButton1_OnClick(object sender, EventArgs e)
    {
		//MessageBox("SheetNo : " + SheetNo.ValueText);
		Session["SPAD004_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印國外出差單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }	
    protected void OriginatorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriginatorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void AgentGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            AgentGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void CheckByGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckByGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
    protected void GetMemberGUID_BeforeClickButton()
    {
        GetMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";        
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPAD004.log", true, System.Text.Encoding.Default);
            //sw.WriteLine(line);
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