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


public partial class SmpProgram_Form_SCQD003_Q1310035_Form : SmpBasicFormPage
{
    
    /// <summary>
    /// 初使化
    /// </summary>
    protected override void init()
    {
          ProcessPageID = "SCQD003_Q1310035"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SCQD003_Q1310035.SCQD003_Q1310035Agent";
        ApplicationID = "SMPFORM";
        ModuleID = "STAD";        
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
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string userId = (string)Session["UserId"];
        try
        {            
            //申請人員 
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo"); //填表人的一些基本資訊

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
                string orgId = "SMP";
               
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
            //string[] userValues = base.getUserInfoById(engine, si.fillerID);
           
            //if (userValues[0] != "")
            //{
            //    EngName.ValueText = userValues[2];   //英文名    
            //    Email.ValueText = userValues[4];   //Email   
            //            Title.ValueText = userValues[3];
            //}
            //else
            //{
            //    EngName.ValueText = "";   //英文名    
            //    Email.ValueText = "";   //Email 
            //            Title.ValueText = "";
            //}
                  string[] mail = ((Hashtable)base.getADUserData(engine, OriginatorGUID.ValueText))["mail"].ToString().Split(new char[] { '_', '@' });
                  EngName.ValueText = mail[0] + "_" + mail[1];
                  Email.ValueText = mail[0] + "_" + mail[1] + "@" + mail[2];



			            
            //預設盒數
            NumberOfApply.ValueText = "1";

            //主旨不顯示於發起單據畫面
            SheetNo.Display = false;
            Subject.Display = false;
            
            //改變工具列順序
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
		bool isAddNew = base.isNew();
        //表單欄位
        //公司別
		CompanyCode.ValueText = objects.getData("CompanyCode");
		//主旨
        Subject.ValueText = objects.getData("Subject");
		//顯示單號
        base.showData(engine, objects);
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
        
        string actName = Convert.ToString(getSession("ACTName"));
        if (!isAddNew){
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
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        bool isAddNew = base.isNew(); //base 父類別
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));            
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
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            objects.setData("Title", Title.ValueText);
            objects.setData("EngTitle", EngTitle.ValueText);
            base.saveData(engine, objects);
        }
        objects.setData("Title", Title.ValueText);
        objects.setData("EngTitle", EngTitle.ValueText);
        //beforeSetFlow
        setSession("IsSetFlow", "Y");
    }

    /// <summary>
    /// 檢查送單資料
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = null;

        if (actName.Equals(""))
        {
            //設定主旨
            if (Subject.ValueText.Equals(""))
            {
                values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
                string subject = "【SCQ-名片申請人員：" + values[1] + " 】";
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
            pushErrorMessage(strErrMsg);
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
        hs.Add("FORMID", ProcessPageID); //自動編號設定作業
        return hs;
    }

    /// <summary>
    /// 設定流程變數
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
			string originatorId = OriginatorGUID.ValueText;
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

            xml += "<SCQAD003_Q1310035>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
            xml += "<CHECKBY DataType=\"java.lang.String\">" + checkById + "</CHECKBY>";
            xml += "<Manager DataType=\"java.lang.String\">" + managerId + "</Manager>";
            xml += "<admmanager DataType=\"java.lang.String\">ADMManager</admmanager>";
            xml += "<admowner DataType=\"java.lang.String\">ADMOwner</admowner>";
            xml += "</SCQAD003_Q1310035>";

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
        }
        //表單代號
        param["SCQAD003_Q1310035"] = xml;
        return "SCQAD003_Q1310035";
    }

    /// <summary>
    /// 重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("ORIGINATOR"))
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
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\stad001.log", true, System.Text.Encoding.Default);
        //sw.WriteLine("OriginatorGUID_SingleOpenWindowButtonClick()");

        AbstractEngine engineErp = null;
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string sql = "";
        DataSet ds = null;
      
       Hashtable hs= ((Hashtable)base.getADUserData(engine, OriginatorGUID.ValueText));
          string[] mail =hs["mail"].ToString().Split(new char[] { '_', '@' });


       EngName.ValueText = mail[0]+"_"+mail[1];
       Email.ValueText = mail[0] + "_" + mail[1] + "@" + mail[2];
       Ext.ValueText = hs["telephonenumber"].ToString();

	try{
       Hashtable users= base.getHRUsers(engine, OriginatorGUID.ValueText);
       DeptName.ValueText = users["PartNo"].ToString();
       EngDeptName.ValueText = users["PartNo"].ToString();
       Title.ValueText = users["DtName"].ToString();
       EngTitle.ValueText = users["DtName"].ToString();
       PhoneNumber.ValueText = users["Mobile"].ToString();
 	}catch (Exception es)
            {
	DeptName.ValueText=es.Message;
		}
        //CpanyCode.ValueText = mail[2];
        //try
        //{

        //    //申請人Email,英文名           
        //    string[] userValues = base.getUserInfoById(engine, OriginatorGUID.ValueText);
        //    if (userValues[0] != "")
        //    {
        //        EngName.ValueText = userValues[2];   //英文名    
        //        Email.ValueText = userValues[4];   //Email   
        //        CompanyCode.ValueText = userValues[5]; //公司別
        //    }
        //    else
        //    {
        //        EngName.ValueText = "";   //英文名    
        //        Email.ValueText = "";   //Email 
        //        CompanyCode.ValueText = ""; //公司別
        //    }
            
        //          //sw.WriteLine("userValues[2] : " + userValues[2]);
        //          //sw.WriteLine("userValues[4] : " + userValues[4]);
        //          //sw.WriteLine("userValues[5] : " + userValues[5]);
        //}
        //catch (Exception e)
        //{
        //    writeLog(e);
        //}
        //finally
        //{
        //    if (engineErp != null)
        //    {
        //        engineErp.close();
        //    }
        //    //if (sw != null)
        //    //{
        //    //    sw.Close();
        //    //}
        //}

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

    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);
    }

      protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
      {
            string xml = "";
            string[] reviewerIds = new string[] { "4019", "3787" };
            if (reviewerIds.Length > 1)
            {
                  xml += "<list>";
                  for (int i = 0; i < reviewerIds.Length; i++)
                  {
                        string[] values = base.getUserGUID(engine, reviewerIds[i]);
                        string oid = values[0];
                        string participantType = "HUMAN";
                        string performType = "NORMAL";
                        string stateValueName = "審核人" + i;

                        if (i == reviewerIds.Length - 1)
                        {
                              stateValueName = "核准人";
                        }

                        xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                        xml += "        <performers>";
                        xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "                <OID>" + oid + "</OID>";
                        xml += "                <participantType><value>" + participantType + "</value></participantType>";
                        xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "        </performers>";
                        xml += "        <multiUserMode><value>FIREST_GET_FIRST_WIN</value></multiUserMode>";
                        xml += "        <name>" + stateValueName + "</name>";
                        xml += "        <performType><value>" + performType + "</value></performType>";
                        xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                  }
                  xml += "</list>";
            }
            return xml;
            //return base.beforeSign(engine, isAfter, addSignXml);
      }






}

