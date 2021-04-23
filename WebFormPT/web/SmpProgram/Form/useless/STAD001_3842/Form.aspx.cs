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
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;

public partial class SmpProgram_Form_STAD001_3842_Form : SmpBasicFormPage //繼承 SmpBasicFormPage
{
    
    /// <summary>
    /// 初使化
    /// </summary>
    protected override void init()
    {       
		ProcessPageID = "STAD001_3842"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.STAD001_3842.SmpAdBusinessCardAgent";
        ApplicationID = "SMPFORM";  //應用程式設定
        ModuleID = "STAD";        //模組設定
    }


    /// <summary>
    /// 初使表單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {       
		AbstractEngine engineErp = null;
        string sql = null;
        string connectString = (string)Session["connectString"];  //看GernaleWebForm有哪些session可用
        string engineType = (string)Session["engineType"];
        string userId = (string)Session["UserId"];
        try
        {            
            //申請人員 
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo"); //填表人的一些基本資訊

            //List,可多語系應用
            string[,] ids = null;
            ids = new string[,]{
                {"",""},
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stad001_form_aspx.language.ini", "message", "smp", "新普科技")},
				{"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stad001_form_aspx.language.ini", "message", "tp", "中普科技")},	
				{"STCS",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stad001_form_aspx.language.ini", "message", "stcs", "新世電子")},
				{"STHP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stad001_form_aspx.language.ini", "message", "sthp", "華普電子")},
                {"STTP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_stad001_form_aspx.language.ini", "message", "sttp", "太普電子")}
            };
            CompanyCode.setListItem(ids);
           
            if (CompanyCode.ValueText.Equals(""))
            {
                sql = "select orgId from EmployeeInfo where empNumber='" + userId + "'";
                string value = (string)engine.executeScalar(sql);
                string orgId = "SMP";  //default
               
                if (value != null)
                {
                    orgId = value;
                }
                CompanyCode.ValueText = orgId;

            }
            CompanyCode.ReadOnly = true;

            OriginatorGUID.clientEngineType = engineType;
            OriginatorGUID.connectDBString = connectString;
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱     
			
			//審核人
			CheckbyGUID.clientEngineType = engineType;
			CheckbyGUID.connectDBString = connectString;

            //申請人Email,英文名           
            string[] userValues = base.getUserInfoById(engine, si.fillerID);
           
            if (userValues[0] != "")
            {
                EngName.ValueText = userValues[2];   //英文名    
                Email.ValueText = userValues[4];   //Email   
				Title.ValueText = userValues[3];
            }
            else
            {
                EngName.ValueText = "";   //英文名    
                Email.ValueText = "";   //Email 
				Title.ValueText = "";
            }
            
			            
            //預設盒數
            NumberOfApply.ValueText = "1";

            //主旨不顯示於發起單據畫面
            SheetNo.Display = false;
            Subject.Display = false;
            
            //改變工具列順序,改變重辦位置於同意旁邊
            base.initUI(engine, objects);
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            if (engineErp != null)
            {
                engineErp.close();
            }           
        }
    }


    /// <summary>
    /// 顯示表單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
		bool isAddNew = base.isNew();  //是否填單狀態Ytrue
        //表單欄位
        //公司別
		CompanyCode.ValueText = objects.getData("CompanyCode");  //CompanyCode column name
		//主旨
        Subject.ValueText = objects.getData("Subject");
		//顯示單號
        base.showData(engine, objects);  //事先取得單號
        //申請人
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
		//審核人		
        string checkByGUID = objects.getData("CheckbyGUID");
        if (!checkByGUID.Equals(""))
        {
            CheckbyGUID.GuidValueText = checkByGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            CheckbyGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
		
        //英文姓名
        EngName.ValueText = objects.getData("EngName");
        //部門
        DeptName.ValueText = objects.getData("DeptName");
        //部門別名               
        EngDeptName.ValueText = objects.getData("EngDeptName");
        //中文職稱
        Title.ValueText = objects.getData("Title");
        //英文職稱
        EngTitle.ValueText = objects.getData("EngTitle");
        //email
        Email.ValueText = objects.getData("Email");
        //分機
        Ext.ValueText = objects.getData("Ext");
        //行動電話
        PhoneNumber.ValueText = objects.getData("PhoneNumber");
        //盒數
        NumberOfApply.ValueText = objects.getData("NumberOfApply");
        
        string actName = Convert.ToString(getSession("ACTName")); //取得目前關卡名稱
        if (!isAddNew){  //表單已送出
            //表單發起後不允許修改
            EngName.ReadOnly = true;
            DeptName.ReadOnly = true;
			EngDeptName.ReadOnly = true;
            Title.ReadOnly = true;
			EngTitle.ReadOnly = true;
            Email.ReadOnly = true;
            Ext.ReadOnly = true;
            PhoneNumber.ReadOnly = true;
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            NumberOfApply.ReadOnly = true; 
			CheckbyGUID.ReadOnly = true;			
        }
    }

    /// <summary>
    /// 儲存表單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects) //資料放到objects
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        bool isAddNew = base.isNew(); //base 父類別
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));   //IDProcessor系統標準方法         
            objects.setData("Subject", Subject.ValueText);
			objects.setData("CompanyCode", CompanyCode.ValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
			objects.setData("CheckbyGUID", CheckbyGUID.GuidValueText);
            objects.setData("EngName", EngName.ValueText);
            objects.setData("DeptName", DeptName.ValueText);
            objects.setData("EngDeptName", EngDeptName.ValueText);           
            objects.setData("Email", Email.ValueText);
            objects.setData("Ext", Ext.ValueText);
            objects.setData("PhoneNumber", PhoneNumber.ValueText);
            objects.setData("NumberOfApply", NumberOfApply.ValueText);
            objects.setData("IS_DISPLAY", "Y"); //標準動作
            objects.setData("DATA_STATUS", "Y");
            objects.setData("Title", Title.ValueText);
            objects.setData("EngTitle", EngTitle.ValueText);
            base.saveData(engine, objects); //SMP客制,讀草穚要再check
        }
        objects.setData("Title", Title.ValueText);
        objects.setData("EngTitle", EngTitle.ValueText);
        //beforeSetFlow
        setSession("IsSetFlow", "Y"); //才會beforeSetFlow
    }

    /// <summary>
    /// 檢查送單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true; //true流程往下
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = null;

        if (actName.Equals(""))
        {
            //設定主旨
            if (Subject.ValueText.Equals(""))
            {
                values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
                string subject = "【STCS-名片申請人員：" + values[1] + " 】";
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
            }
        }

        //申請盒數不可為零
        decimal numberOfApply = Convert.ToDecimal(NumberOfApply.ValueText);
        if (numberOfApply <= 0)
        {
            strErrMsg += "申請盒數必需大於零!\n";
        }
        

        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg); //丟出錯誤訊息
            result = false;
        }

        return result;
    }

    /// <summary>
    /// 初使送單資訊
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人, 登入者
        si.fillerName = (string)Session["UserName"]; //填表人姓名
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0]; //發起單位代號
        si.objectGUID = objects.getData("GUID");
        return si;
    }

    /// <summary>
    /// 取得送單資訊
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
        //si.ownerID = OriginatorGUID.ValueText; //申請人id
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;  //申請人名稱
		si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 取得單號編碼定義
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID); //自動編號設定作業, 系統管理
        return hs;
    }

    /// <summary>
    /// 設定流程變數,用哪個流程及傳參數
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        try
        {
			SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
			//填表人
            string creatorId = si.fillerID;
            
            //申請人部門主管
            string originatorGUID = OriginatorGUID.GuidValueText;
			string originatorId = OriginatorGUID.ValueText; //物件id
            string[] values = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = values[0];

            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];   
			
			//審核人
			string checkByGUID = CheckbyGUID.GuidValueText;
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }
            //對應到GP之表單設計師
            xml += "<STAD001_3842>";  //對應到流程代號_3842,表單代號
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
            xml += "<checkby DataType=\"java.lang.String\">" + checkById + "</checkby>";
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
            xml += "<admManager DataType=\"java.lang.String\">ADMManager</admManager>";
            xml += "<admowner DataType=\"java.lang.String\">ADMOwner</admowner>";
            xml += "</STAD001_3842>";

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
        }
        //表單代號
        param["STAD001_3842"] = xml;
        return "STAD001_3842";
    }

    /// <summary>
    /// 重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("ORIGINATOR"))  //是否為第一關
        {
            try
            {
                base.terminateThisProcess();
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
    /// 重新選擇申請人員
    /// </summary>
    /// <param name="values"></param>
    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        System.IO.StreamWriter sw = null;
        sw = new System.IO.StreamWriter(@"d:\temp\stad001_3842.log", true, System.Text.Encoding.Default);
        sw.WriteLine("OriginatorGUID_SingleOpenWindowButtonClick()");

        AbstractEngine engineErp = null;
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string sql = "";
        DataSet ds = null;
        try
        {

            //申請人Email,英文名           
            string[] userValues = base.getUserInfoById(engine, OriginatorGUID.ValueText);
            if (userValues[0] != "")
            {
                EngName.ValueText = userValues[2];   //英文名    
                Email.ValueText = userValues[4];   //Email   
                CompanyCode.ValueText = userValues[5]; //公司別
            }
            else
            {
                EngName.ValueText = "";   //英文名    
                Email.ValueText = "";   //Email 
                CompanyCode.ValueText = ""; //公司別
            }
            
			sw.WriteLine("userValues[2] : " + userValues[2]);
			sw.WriteLine("userValues[4] : " + userValues[4]);
			sw.WriteLine("userValues[5] : " + userValues[5]);
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            if (engineErp != null)
            {
                engineErp.close();
            }
            if (sw != null)
            {
                sw.Close();
            }
        }

    }


    /// <summary>
    /// 重新選擇申請人員前過濾資料
    /// </summary>
    /// <param name="values"></param>
    protected void OriginatorGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            OriginatorGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
	
	 /// <summary>
    /// 重新選擇審核人員前過濾資料
    /// </summary>
    /// <param name="values"></param>
    protected void CheckbyGUID_BeforeClickButton()
    {
        if (base.isNew())
        {
            CheckbyGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)";
        }
    }
}

