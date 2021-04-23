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

using System.IO;
using System.Xml;
using WebServerProject.auth;
using NPOI.HSSF.UserModel;

public partial class SmpProgram_Form_SPTS002_Form : SmpTsFormPage 
{	
    protected override void init()
    {	
		ProcessPageID = "SPTS002"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPTS002.SmpTSQuestionnaireAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPTS";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		//writeLog("initUI Start");
		string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
		CourseFormGUID.Display = false;
		TraineeGUID.Display = false;
		
		try{					
			//課程代號
            SchDetailGUID.clientEngineType = engineType;
            SchDetailGUID.connectDBString = connectString; 

            //講師            
            LecturerGUID.clientEngineType = engineType;
            LecturerGUID.connectDBString = connectString; 
			
			//Radio Group, Q11
			Q115.GroupName = "grp11";
			Q114.GroupName = "grp11";
			Q113.GroupName = "grp11";
			Q112.GroupName = "grp11";
			
			Q125.GroupName = "grp12";
			Q124.GroupName = "grp12";
			Q123.GroupName = "grp12";
			Q122.GroupName = "grp12";
			
			Q135.GroupName = "grp13";
			Q134.GroupName = "grp13";
			Q133.GroupName = "grp13";
			Q132.GroupName = "grp13";
			
			Q145.GroupName = "grp14";
			Q144.GroupName = "grp14";
			Q143.GroupName = "grp14";
			Q142.GroupName = "grp14";
			
			Q155.GroupName = "grp15";
			Q154.GroupName = "grp15";
			Q153.GroupName = "grp15";
			Q152.GroupName = "grp15";
			
			Q165.GroupName = "grp16";
			Q164.GroupName = "grp16";
			Q163.GroupName = "grp16";
			Q162.GroupName = "grp16";
			
			Q175.GroupName = "grp17";
			Q174.GroupName = "grp17";
			Q173.GroupName = "grp17";
			Q172.GroupName = "grp17";
			
			Q215.GroupName = "grp21";
			Q214.GroupName = "grp21";
			Q213.GroupName = "grp21";
			Q212.GroupName = "grp21";
			
			Q225.GroupName = "grp22";
			Q224.GroupName = "grp22";
			Q223.GroupName = "grp22";
			Q222.GroupName = "grp22";
			
			Q235.GroupName = "grp23";
			Q234.GroupName = "grp23";
			Q233.GroupName = "grp23";
			Q232.GroupName = "grp23";
			
			Q315.GroupName = "grp31";
			Q314.GroupName = "grp31";
			Q313.GroupName = "grp31";
			Q312.GroupName = "grp31";
			
			Q325.GroupName = "grp32";
			Q324.GroupName = "grp32";
			Q323.GroupName = "grp32";
			Q322.GroupName = "grp32";
			
			Q335.GroupName = "grp33";
			Q334.GroupName = "grp33";
			Q333.GroupName = "grp33";
			Q332.GroupName = "grp33";
			
			Q415.GroupName = "grp41";
			Q414.GroupName = "grp41";
			Q413.GroupName = "grp41";
			Q412.GroupName = "grp41";
			
			Q425.GroupName = "grp42";
			Q424.GroupName = "grp42";
			Q423.GroupName = "grp42";
			Q422.GroupName = "grp42";
			
			Q435.GroupName = "grp43";
			Q434.GroupName = "grp43";
			Q433.GroupName = "grp43";
			Q432.GroupName = "grp43";
			
		}
        catch (Exception e)
        {
            writeLog(e);
        }       
		
        //改變工具列順序
        base.initUI(engine, objects);
		writeLog("initUI end ");
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {
		writeLog("showData Start");
		base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);
		SheetNo.ValueText = objects.getData("SheetNo"); //傳入序號,
			
		//課程名稱
        SchDetailGUID.clientEngineType = (string)Session["engineType"];
        SchDetailGUID.connectDBString = (string)Session["connectString"]; 
		SchDetailGUID.GuidValueText = objects.getData("SchDetailGUID");      
        SchDetailGUID.doGUIDValidate();

		//講師
        LecturerGUID.clientEngineType = (string)Session["engineType"];
        LecturerGUID.connectDBString = (string)Session["connectString"]; 
		LecturerGUID.GuidValueText = objects.getData("LecturerGUID");      
        LecturerGUID.doGUIDValidate();                
		
		StartDate.ValueText = objects.getData("StartDate");
		Remark.ValueText = objects.getData("Remark");
		CourseFormGUID.ValueText = objects.getData("CourseFormGUID");
		TraineeGUID.ValueText = objects.getData("TraineeGUID");
		
		//Q11     
        string q11 = Convert.ToString(objects.getData("Q11"));
		switch (q11)
        {
            case "5":
                Q115.Checked = true;
                break;
            case "4":
                Q114.Checked = true;
                break;
            case "3":
                Q113.Checked = true;
                break;           
			case "2":
                Q112.Checked = true;
                break;		
            default:
                break;
        }
		string q12 = Convert.ToString(objects.getData("Q12"));
		switch (q12)
        {
            case "5":
                Q125.Checked = true;
                break;
            case "4":
                Q124.Checked = true;
                break;
            case "3":
                Q123.Checked = true;
                break;           
			case "2":
                Q122.Checked = true;
                break;		
            default:
                break;
        }
		string q13 = Convert.ToString(objects.getData("Q13"));
		switch (q13)
        {
            case "5":
                Q135.Checked = true;
                break;
            case "4":
                Q134.Checked = true;
                break;
            case "3":
                Q133.Checked = true;
                break;           
			case "2":
                Q132.Checked = true;
                break;		
            default:
                break;
        }
		string q14 = Convert.ToString(objects.getData("Q14"));
		switch (q14)
        {
            case "5":
                Q145.Checked = true;
                break;
            case "4":
                Q144.Checked = true;
                break;
            case "3":
                Q143.Checked = true;
                break;           
			case "2":
                Q142.Checked = true;
                break;		
            default:
                break;
        }
		string q15 = Convert.ToString(objects.getData("Q15"));
		switch (q11)
        {
            case "5":
                Q155.Checked = true;
                break;
            case "4":
                Q154.Checked = true;
                break;
            case "3":
                Q153.Checked = true;
                break;           
			case "2":
                Q152.Checked = true;
                break;		
            default:
                break;
        }
		string q16 = Convert.ToString(objects.getData("Q16"));
		switch (q16)
        {
            case "5":
                Q165.Checked = true;
                break;
            case "4":
                Q164.Checked = true;
                break;
            case "3":
                Q163.Checked = true;
                break;           
			case "2":
                Q162.Checked = true;
                break;		
            default:
                break;
        }
		string q17 = Convert.ToString(objects.getData("Q17"));
        switch (q17)
        {
            case "5":
                Q175.Checked = true;
                break;
            case "4":
                Q174.Checked = true;
                break;
            case "3":
                Q173.Checked = true;
                break;           
			case "2":
                Q172.Checked = true;
                break;		
            default:
                break;
        }
		
		//Q21     
        string q21 = Convert.ToString(objects.getData("Q21"));
		switch (q21)
        {
            case "5":
                Q215.Checked = true;
                break;
            case "4":
                Q214.Checked = true;
                break;
            case "3":
                Q213.Checked = true;
                break;           
			case "2":
                Q212.Checked = true;
                break;		
            default:
                break;
        }
		string q22 = Convert.ToString(objects.getData("Q22"));
		switch (q22)
        {
            case "5":
                Q225.Checked = true;
                break;
            case "4":
                Q224.Checked = true;
                break;
            case "3":
                Q223.Checked = true;
                break;           
			case "2":
                Q222.Checked = true;
                break;		
            default:
                break;
        }
		string q23 = Convert.ToString(objects.getData("Q23"));
		switch (q23)
        {
            case "5":
                Q235.Checked = true;
                break;
            case "4":
                Q234.Checked = true;
                break;
            case "3":
                Q233.Checked = true;
                break;           
			case "2":
                Q232.Checked = true;
                break;		
            default:
                break;
        }
		
		//Q31     
        string q31 = Convert.ToString(objects.getData("Q31"));
		switch (q31)
        {
            case "5":
                Q315.Checked = true;
                break;
            case "4":
                Q314.Checked = true;
                break;
            case "3":
                Q313.Checked = true;
                break;           
			case "2":
                Q312.Checked = true;
                break;		
            default:
                break;
        }
		string q32 = Convert.ToString(objects.getData("Q32"));
		switch (q32)
        {
            case "5":
                Q325.Checked = true;
                break;
            case "4":
                Q324.Checked = true;
                break;
            case "3":
                Q323.Checked = true;
                break;           
			case "2":
                Q322.Checked = true;
                break;		
            default:
                break;
        }
		string q33 = Convert.ToString(objects.getData("Q33"));
		switch (q33)
        {
            case "5":
                Q335.Checked = true;
                break;
            case "4":
                Q334.Checked = true;
                break;
            case "3":
                Q333.Checked = true;
                break;           
			case "2":
                Q332.Checked = true;
                break;		
            default:
                break;
        }
		
		//Q41     
        string q41 = Convert.ToString(objects.getData("Q41"));
		switch (q41)
        {
            case "5":
                Q415.Checked = true;
                break;
            case "4":
                Q414.Checked = true;
                break;
            case "3":
                Q413.Checked = true;
                break;           
			case "2":
                Q412.Checked = true;
                break;		
            default:
                break;
        }
		string q42 = Convert.ToString(objects.getData("Q42"));
		switch (q42)
        {
            case "5":
                Q425.Checked = true;
                break;
            case "4":
                Q424.Checked = true;
                break;
            case "3":
                Q423.Checked = true;
                break;           
			case "2":
                Q422.Checked = true;
                break;		
            default:
                break;
        }
		string q43 = Convert.ToString(objects.getData("Q43"));
		switch (q43)
        {
            case "5":
                Q435.Checked = true;
                break;
            case "4":
                Q434.Checked = true;
                break;
            case "3":
                Q433.Checked = true;
                break;           
			case "2":
                Q432.Checked = true;
                break;		
            default:
                break;
        }
		
		if (!isAddNew){
			Subject.ReadOnly = true;
        	SheetNo.ReadOnly = true;
			SchDetailGUID.ReadOnly = true;
			LecturerGUID.ReadOnly = true;
			StartDate.ReadOnly = true;
			CourseFormGUID.ReadOnly = true;
		}
    }
        
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
		writeLog("saveData Start");
		try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            bool isAddNew = base.isNew(); //base 父類別
			
			//CourseFormGUID.ValueText = "3f475da7-e01b-4f3b-9685-e3914c1c906d";		
			//SchDetailGUID.ValueText = "6333d114-1380-4106-89e8-b9c0e222f460";		
			//LecturerGUID.ValueText = "ddb0b18a-3fa2-4d4a-9246-2c606c1792c1";

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
				objects.setData("SchDetailGUID", SchDetailGUID.GuidValueText);
				objects.setData("LecturerGUID", LecturerGUID.GuidValueText);				
				objects.setData("StartDate", StartDate.ValueText);
				objects.setData("CourseFormGUID", CourseFormGUID.ValueText);
				
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
            }
			//其他建議及意見
			objects.setData("Remark", Remark.ValueText);
			
			//Q11 1.教師對授課時間掌握情況
            string q11 = "";
            if (Q115.Checked) q11 = "5";
            if (Q114.Checked) q11 = "4";
            if (Q113.Checked) q11 = "3";
            if (Q112.Checked) q11 = "2";
            objects.setData("Q11", q11);
			
			string q12 = "";
            if (Q125.Checked) q12 = "5";
            if (Q124.Checked) q12 = "4";
            if (Q123.Checked) q12 = "3";
            if (Q122.Checked) q12 = "2";
            objects.setData("Q12", q12);
			
			string q13 = "";
            if (Q135.Checked) q13 = "5";
            if (Q134.Checked) q13 = "4";
            if (Q133.Checked) q13 = "3";
            if (Q132.Checked) q13 = "2";
            objects.setData("Q13", q13);
			
			string q14 = "";
            if (Q145.Checked) q14 = "5";
            if (Q144.Checked) q14 = "4";
            if (Q143.Checked) q14 = "3";
            if (Q142.Checked) q14 = "2";
            objects.setData("Q14", q14);
			
			string q15 = "";
            if (Q155.Checked) q15 = "5";
            if (Q154.Checked) q15 = "4";
            if (Q153.Checked) q15 = "3";
            if (Q152.Checked) q15 = "2";
            objects.setData("Q15", q15);
			
			string q16 = "";
            if (Q165.Checked) q16 = "5";
            if (Q164.Checked) q16 = "4";
            if (Q163.Checked) q16 = "3";
            if (Q162.Checked) q16 = "2";
            objects.setData("Q16", q16);
			
			string q17 = "";
            if (Q175.Checked) q17 = "5";
            if (Q174.Checked) q17 = "4";
            if (Q173.Checked) q17 = "3";
            if (Q172.Checked) q17 = "2";
            objects.setData("Q17", q17);
			
			//Q21
            string q21 = "";
            if (Q215.Checked) q21 = "5";
            if (Q214.Checked) q21 = "4";
            if (Q213.Checked) q21 = "3";
            if (Q212.Checked) q21 = "2";
            objects.setData("Q21", q21);
			
			string q22 = "";
            if (Q225.Checked) q22 = "5";
            if (Q224.Checked) q22 = "4";
            if (Q223.Checked) q22 = "3";
            if (Q222.Checked) q22 = "2";
            objects.setData("Q22", q22);
			
			string q23 = "";
            if (Q235.Checked) q23 = "5";
            if (Q234.Checked) q23 = "4";
            if (Q233.Checked) q23 = "3";
            if (Q232.Checked) q23 = "2";
            objects.setData("Q23", q23);
			
			
			//Q31
            string q31 = "";
            if (Q315.Checked) q31 = "5";
            if (Q314.Checked) q31 = "4";
            if (Q313.Checked) q31 = "3";
            if (Q312.Checked) q31 = "2";
            objects.setData("Q31", q31);
			
			string q32 = "";
            if (Q325.Checked) q32 = "5";
            if (Q324.Checked) q32 = "4";
            if (Q323.Checked) q32 = "3";
            if (Q322.Checked) q32 = "2";
            objects.setData("Q32", q32);
			
			string q33 = "";
            if (Q335.Checked) q33 = "5";
            if (Q334.Checked) q33 = "4";
            if (Q333.Checked) q33 = "3";
            if (Q332.Checked) q33 = "2";
            objects.setData("Q33", q33);
			
			
			//Q41
            string q41 = "";
            if (Q415.Checked) q41 = "5";
            if (Q414.Checked) q41 = "4";
            if (Q413.Checked) q41 = "3";
            if (Q412.Checked) q41 = "2";
            objects.setData("Q41", q41);
			
			string q42 = "";
            if (Q425.Checked) q42 = "5";
            if (Q424.Checked) q42 = "4";
            if (Q423.Checked) q42 = "3";
            if (Q422.Checked) q42 = "2";
            objects.setData("Q42", q42);
			
			string q43 = "";
            if (Q435.Checked) q43 = "5";
            if (Q434.Checked) q43 = "4";
            if (Q433.Checked) q43 = "3";
            if (Q432.Checked) q43 = "2";
            objects.setData("Q43", q43);			
			
            setSession("IsSetFlow", "Y");		
			
        }
        catch (Exception e)
        {
            writeLog(e);
        }
		
		writeLog("saveData End");
        
    }

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
		writeLog("checkFieldData Start");
		bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string schDetailGUID = objects.getData("SchDetailGUID");
		string lecturer = objects.getData("LecturerGUID");
		string sDate = objects.getData("StartDate");
		int cnt = 0;
		bool isAddNew = base.isNew(); //base 父類別
		
       	if ((!Q115.Checked) && (!Q114.Checked) && (!Q113.Checked) && (!Q112.Checked) ) strErrMsg += "一、1.教師對授課時間掌握情況! 未填寫\n";
		if ((!Q125.Checked) && (!Q124.Checked) && (!Q123.Checked) && (!Q122.Checked) ) strErrMsg += "一、2.教師能掌握我課程的學習方向! 未填寫\n";
		if ((!Q135.Checked) && (!Q134.Checked) && (!Q133.Checked) && (!Q132.Checked) ) strErrMsg += "一、3.教師專業知識的能力! 未填寫\n";	 
		if ((!Q145.Checked) && (!Q144.Checked) && (!Q143.Checked) && (!Q142.Checked) ) strErrMsg += "一、4.教師的授課技巧與表達能力! 未填寫\n";
		if ((!Q155.Checked) && (!Q154.Checked) && (!Q153.Checked) && (!Q152.Checked) ) strErrMsg += "一、5.教師授課時的教學態度! 未填寫\n";
		if ((!Q165.Checked) && (!Q164.Checked) && (!Q163.Checked) && (!Q162.Checked) ) strErrMsg += "一、6.教師對學員課程上問題之解決能力! 未填寫\n";
		if ((!Q175.Checked) && (!Q174.Checked) && (!Q173.Checked) && (!Q172.Checked) ) strErrMsg += "一、7.教師對授課內容組織條理! 未填寫\n";
		
		if ((!Q215.Checked) && (!Q214.Checked) && (!Q213.Checked) && (!Q212.Checked) ) strErrMsg += "二、1.課程內容明確易懂! 未填寫\n";
		if ((!Q225.Checked) && (!Q224.Checked) && (!Q223.Checked) && (!Q222.Checked) ) strErrMsg += "二、2.課程內容豐富具有多樣性! 未填寫\n";
		if ((!Q235.Checked) && (!Q234.Checked) && (!Q233.Checked) && (!Q232.Checked) ) strErrMsg += "二、3.課程內容具有實用性! 未填寫\n";

		if ((!Q315.Checked) && (!Q314.Checked) && (!Q313.Checked) && (!Q312.Checked) ) strErrMsg += "三、1.我認為上完此課程對我有實質的幫助! 未填寫\n";
		if ((!Q325.Checked) && (!Q324.Checked) && (!Q323.Checked) && (!Q322.Checked) ) strErrMsg += "三、2.本次訓練有助於提昇自己的工作能力! 未填寫\n";
		if ((!Q335.Checked) && (!Q334.Checked) && (!Q333.Checked) && (!Q332.Checked) ) strErrMsg += "三、3.對工作環境中所運用所學的新知識更具信心! 未填寫\n";

		if ((!Q415.Checked) && (!Q414.Checked) && (!Q413.Checked) && (!Q412.Checked) ) strErrMsg += "四、1.對上課環境的滿意度! 未填寫\n";
		if ((!Q425.Checked) && (!Q424.Checked) && (!Q423.Checked) && (!Q422.Checked) ) strErrMsg += "四、2.課程時間安排! 未填寫\n";
		if ((!Q435.Checked) && (!Q434.Checked) && (!Q433.Checked) && (!Q432.Checked) ) strErrMsg += "四、3.對本次課程總體評估! 未填寫\n";

	    //設定主旨
        if (!Subject.ValueText.StartsWith(""))
        {
            //values = base.getUserInfo(engine, originatorGUID);
            string subject = "課程滿意度調查表：" + SchDetailGUID.GuidValueText + " ";
            Subject.ValueText = subject + Subject.ValueText;
        }
			
        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }
        writeLog("checkFieldData End");
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
		//writeLog("getSubmitInfo start");
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);
        //string[] depDataRealtionship = getDeptInfo(engine, (string)OriginatorGUID.GuidValueText);

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
		string result = "";
		string sql = "";
		int cnt = 0;
		string actName = Convert.ToString(getSession("ACTName"));
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            //填表人
            string creatorId = si.fillerID;
			//上課學員
			string trainee = creatorId;            
			xml += "<SPTS002>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<trainee DataType=\"java.lang.String\">" + trainee + "</trainee>"; 
            xml += "</SPTS002>";
            writeLog("xml: " + xml);			
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        
        //表單代號
		if (result.Equals(""))
		{
			param["SPTS002"] = xml;
			return "SPTS002";
		}else{
			return "發生錯誤! 請連絡MIS人員處理";
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
		writeLog("afterSign start : " + result);			
        string signProcess = Convert.ToString(Session["signProcess"]);
        string sql = "";
        string strSheetNo = currentObject.getData("SheetNo"); 
		string strCourseFormGuid = currentObject.getData("CourseFormGUID");
		string strTaineeGuid = currentObject.getData("TraineeGUID");
        string now = DateTimeUtility.getSystemTime2(null);
        string userGUID = (string)Session["UserGUID"];
		
		if (strTaineeGuid.Equals("")){
			strTaineeGuid = userGUID;
		}
		
		//writeLog("afterSign signProcess :" + signProcess);
		//writeLog("strSheetNo :" + strSheetNo);
		//writeLog("strCourseFormGuid :" + strCourseFormGuid);
		//writeLog("strTaineeGuid :" + strTaineeGuid);
		//writeLog("now :" + now);
				
		if (signProcess.Equals("N")) //不同意
        {
            sql = "update SmpTSCourseTrainee set GetReport = 'N', Attendance='N' where CourseFormGUID = '" + strCourseFormGuid + "' and EmployeeGUID = '" + strTaineeGuid + "' ";
			engine.executeSQL(sql);
        }else{
			sql = "update SmpTSCourseTrainee set GetReport = 'Y', Attendance='Y' where CourseFormGUID = '" + strCourseFormGuid + "' and EmployeeGUID = '" + strTaineeGuid + "' ";
			engine.executeSQL(sql);		
		} 
		writeLog("afterSign end" + signProcess);
		
        base.afterSign(engine, currentObject, result);
    }

    /// <summary>
    /// 重辦程序
    /// </summary>    
    protected override void rejectProcedure()
    {
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回關卡 ID        
        //先退回
        base.rejectProcedure();
		if (backActID.ToUpper().Equals("CREATOR") ) //流程之中, 申請人關卡的 ID 值        
        {
            //終止流程
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            base.terminateThisProcess(si.ownerID);
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
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {   
        base.afterApprove(engine, currentObject, result);
    }
 
	
	//取得課預設值
    protected void SchDetailGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, connectString);
			
    }
	


	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPTS002.log", true, System.Text.Encoding.Default);
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