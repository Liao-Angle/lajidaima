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


public partial class SmpProgram_System_Form_SPIT001_Form : SmpBasicFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPIT001"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPIT001.SmpInfoDemandAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPIT";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string userId = (string)Session["UserId"];
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        string[,] ids = null;
        DataSet ds = null;
        int count = 0;

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;

       
        try
        {
            //公司別
            ids = new string[,]{
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spit001_form_aspx.language.ini", "message", "smp", "新普科技")}
            };
            Company.setListItem(ids);
            if (Company.ValueText.Equals(""))
            {
                Company.ValueText = "SMP";
            }

            //申請部門
            OriginatorDeptGUID.clientEngineType = engineType;
            OriginatorDeptGUID.connectDBString = connectString;
            OriginatorDeptGUID.ValueText = si.submitOrgID;
            OriginatorDeptGUID.doValidate();

            OriginatorDeptGUID.ReadOnly = true;


            //申請人工號. 中文名. 英文名. 職稱
            //string[] userValues = base.getUserInfoById(engine, si.fillerID);
            //if (userValues[0] != "")
            //     {
            //    OriginatorNumber.ValueText = userValues[0];
            //    OriginatorCName.ValueText = userValues[1];
            //    OriginatorEName.ValueText = userValues[2];
            //    Title.ValueText = userValues[3];
            //}
            //else
            //{
            //    OriginatorNumber.ValueText = "";
            //    OriginatorCName.ValueText = "";
            //    OriginatorEName.ValueText = "";
            //    Title.ValueText = "";
            //}



            Hashtable ADuser = (Hashtable)base.getADUserData(engine, userId);
            
            string[] mail = ADuser["mail"].ToString().Split(new char[] { '_', '@' });
            
            Hashtable Euser = (Hashtable)base.getHRUsers(engine, userId);
           
            OriginatorNumber.ValueText = userId;
            OriginatorCName.ValueText = Euser["EmpName"].ToString();
            OriginatorEName.ValueText = mail[0] + "_" + mail[1];
            Title.ValueText = Euser["DtName"].ToString();
            Extension.ValueText = ADuser["telephonenumber"].ToString();



            OriginatorNumber.ReadOnly = true;
            OriginatorCName.ReadOnly = true;
            OriginatorEName.ReadOnly = true;
            Title.ReadOnly = true;
            //Extension.ReadOnly = true;



            MisDesc.ValueText = "";
            //需求類別


            ids = new string[4 + count, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            ids[1, 0] = "1.應用系統-賬號申請/權限異動";
            ids[1, 1] = "1.應用系統-賬號申請/權限異動";
            ids[2, 0] = "2.應用系統-程式報表修改";
            ids[2, 1] = "2.應用系統-程式報表修改";
            ids[3, 0] = "3.應用系統-系統設定與維護";
            ids[3, 1] = "3.應用系統-系統設定與維護";

              
              RequestType.setListItem(ids);

            //需求項目
            
            ids = new string[1, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            RequestItem.setListItem(ids);

      }
      catch (Exception e)
      {
            writeLog(e);
      }

        //審核人
        CheckByGUID.clientEngineType = engineType;
        CheckByGUID.connectDBString = connectString;

        //會簽人員1, 會簽人員2
        Countersign1GUID.clientEngineType = engineType;
        Countersign1GUID.connectDBString = connectString;
        Countersign2GUID.clientEngineType = engineType;
        Countersign2GUID.connectDBString = connectString;
        MisOwnerGUID.clientEngineType = engineType;
        MisOwnerGUID.connectDBString = connectString;
        		
        DataObjectSet requestSet = null;
        if (isAddNew)
        {
            requestSet = new DataObjectSet();
            requestSet.isNameLess = true;
            requestSet.setAssemblyName("WebServerProject");
            requestSet.setChildClassString("WebServerProject.form.SPIT001.SmpInfoDemandDetail");
            requestSet.setTableName("SmpInfoDemandDetail");
            requestSet.loadFileSchema();
            objects.setChild("SmpInfoDemandDetail", requestSet);

        }
        else
        {
            requestSet = objects.getChild("SmpInfoDemandDetail");
        }
        RequestList.dataSource = requestSet;
        RequestList.HiddenField = new string[] { "GUID", "HeaderGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        RequestList.updateTable();

        if (isAddNew)
        {
            Countersign1GUID.ReadOnly = true;
            Countersign2GUID.ReadOnly = true;
            MisDesc.ReadOnly = true;
            MisOwnerGUID.ReadOnly = true;
            EstimateCompleteDate.ReadOnly = true;
            Company.ReadOnly = true;
        }

        //改變工具列順序
        base.initUI(engine, objects);
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));

        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

        //表單欄位
        SheetNo.Display = false;
        Subject.Display = false;
        bool isAddNew = base.isNew();
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //單號, 主旨含單號
        base.showData(engine, objects);

        //申請人員資訊
        Company.ValueText = objects.getData("Company");
        OriginatorNumber.ValueText = objects.getData("OriginatorNumber");
        OriginatorCName.ValueText = objects.getData("OriginatorCName");
        OriginatorEName.ValueText = objects.getData("OriginatorEName");
        Title.ValueText = objects.getData("Title");
        Extension.ValueText = objects.getData("Extension");
        UserDesc.ValueText = objects.getData("UserDesc");
        MisDesc.ValueText = objects.getData("MisDesc");
		EstimateCompleteDate.ValueText = objects.getData("EstimateCompleteDate");		

        //申請單位
        OriginatorDeptGUID.ValueText = objects.getData("OriginatorDeptGUID");
        OriginatorDeptGUID.doGUIDValidate();
        
        Title.ValueText = objects.getData("Title");
               
        //審核人員
        string checkByGUID = objects.getData("CheckByGUID");
        if (!checkByGUID.Equals(""))
        {
            CheckByGUID.GuidValueText = checkByGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            CheckByGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }

        //會簽人員1
        string  countersign1GUID = objects.getData("Countersign1GUID");
        if (!countersign1GUID.Equals(""))
        {
            Countersign1GUID.GuidValueText = countersign1GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Countersign1GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }
        //會簽人員2
        string countersign2GUID = objects.getData("Countersign2GUID");
        if (!countersign2GUID.Equals(""))
        {
            Countersign2GUID.GuidValueText = countersign2GUID; //將值放入人員開窗元件中, 資料庫存放GUID
            Countersign2GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }

        string misOwnerGUID = objects.getData("MisOwnerGUID");
        if (!misOwnerGUID.Equals(""))
        {
            MisOwnerGUID.GuidValueText = misOwnerGUID; //將值放入人員開窗元件中, 資料庫存放GUID
            MisOwnerGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID
        }

        DataObjectSet requestSet = null;
        requestSet = objects.getChild("SmpInfoDemandDetail");
        RequestList.dataSource = requestSet;
        RequestList.updateTable();
		
        if (!isAddNew)
        {
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            Company.ReadOnly = true;
            OriginatorDeptGUID.ReadOnly = true;
            OriginatorNumber.ReadOnly = true;
            OriginatorCName.ReadOnly = true;
            OriginatorEName.ReadOnly = true;
            Title.ReadOnly = true;
            Extension.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            UserDesc.ReadOnly = true;
            MisDesc.ReadOnly = true;
            Countersign1GUID.ReadOnly = true;
            Countersign2GUID.ReadOnly = true;
            RequestType.ReadOnly = true;
            RequestItem.ReadOnly = true;
            RequestDesc.ReadOnly = true;
            MisOwnerGUID.ReadOnly = true;
            EstimateCompleteDate.ReadOnly = true;
			RequestList.NoAdd = true;                                                                                                                                                     
			RequestList.NoDelete = true;                                                                                                                                                  
			RequestList.NoModify = true;
			
        }
        
        for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
        {
            RequestList.dataSource.getAvailableDataObject(i).setData("SmpInfoDemandDetail", objects.getData("GUID"));
        }

        if (actName.Equals("MIS窗口")) {
            MisDesc.ReadOnly = false;
            AddSignButton.Display = true; //允許加簽, 
			EstimateCompleteDate.ReadOnly = false;
        }

        if (actName.Equals("MIS主管")) {
            Countersign1GUID.ReadOnly = false;
            Countersign2GUID.ReadOnly = false;
            MisOwnerGUID.ReadOnly = false;
        }

        if (actName.Equals("MIS承辦人員")) {            
            MisDesc.ReadOnly = false;
        }
    }
        bool isAddNew=true;
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");            
            isAddNew = base.isNew(); //base 父類別

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("Company", Company.ValueText);
                objects.setData("OriginatorDeptGUID", OriginatorDeptGUID.GuidValueText);
                objects.setData("Title", Title.ValueText);
                objects.setData("OriginatorNumber", OriginatorNumber.ValueText);
                objects.setData("OriginatorCName", OriginatorCName.ValueText);
                objects.setData("OriginatorEName", OriginatorEName.ValueText);
                objects.setData("Extension", Extension.ValueText);
                objects.setData("UserDesc", UserDesc.ValueText);
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
            }
            objects.setData("EstimateCompleteDate", EstimateCompleteDate.ValueText);
            objects.setData("MisDesc", MisDesc.ValueText);
            objects.setData("Countersign1GUID", Countersign1GUID.GuidValueText);
            objects.setData("Countersign2GUID", Countersign2GUID.GuidValueText);
            objects.setData("MisOwnerGUID", MisOwnerGUID.GuidValueText);

            for (int i = 0; i < RequestList.dataSource.getAvailableDataObjectCount(); i++)
            {
                RequestList.dataSource.getAvailableDataObject(i).setData("HeaderGUID", objects.getData("GUID"));
            }
            setSession("IsSetFlow", "Y");
        }
        catch (Exception e)
        {
            writeLog(e);
        }
    }

    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = base.getUserInfo(engine, OriginatorDeptGUID.GuidValueText);

        if (actName.Equals(""))
        {
            if (OriginatorNumber.ValueText.Equals("")) { strErrMsg += "工號不可為空值\n";  }
            if (OriginatorCName.ValueText.Equals("")) { strErrMsg += "申請人(中)不可為空值\n"; }
            if (OriginatorEName.ValueText.Equals("")) { strErrMsg += "申請人(英)不可為空值\n"; }
            if (OriginatorDeptGUID.ValueText.Equals("")) { strErrMsg += "部門不可為空值\n"; }
            if (Title.ValueText.Equals("")) { strErrMsg += "職稱不可為空值\n"; }
            if (Extension.ValueText.Equals("")) { strErrMsg += "分機不可為空值\n"; }
            if (UserDesc.ValueText.Equals("")) { strErrMsg += "使用者需求說明不可為空值\n"; }
            
			//檢查 明細清單 資料
            DataObjectSet chkType = RequestList.dataSource;
            if (chkType.getAvailableDataObjectCount().Equals(0))
            {
                strErrMsg += "請輸入資訊需求內容清單(需求類別、需求項目、需求說明)!\n";
            }

            //設定主旨
            if (Subject.ValueText.Equals(""))
            {
                //values = base.getUserInfo(engine, RequestList.ValueText);
                string subject = "【" + OriginatorCName.ValueText + " 資訊需求申請，類別：" + RequestType.ValueText + " 】";
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
            }
        
        }
        if (actName.Equals("MIS窗口"))
        {
          //  if (EstimateCompleteDate.ValueText.Equals("")) { strErrMsg += "預計完成日不可為空值\n"; }
        }
        if (actName.Equals("MIS主管"))
        {
            if (MisOwnerGUID.ValueText.Equals("")) { strErrMsg += "請指MIS承辦人員\n"; }
        }

        if (actName.Equals("MIS承辦人員"))
        {
            if (MisDesc.ValueText.Equals("")) { strErrMsg += "MIS處理說明不可為空值\n"; }
        }
        
        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
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
        si.ownerID = (string)Session["UserID"]; //表單關系人
        //si.ownerID = OriginatorGUID.ValueText;
        si.ownerName = (string)Session["UserName"];
        //si.ownerName = OriginatorGUID.ReadOnlyValueText;
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0]; //發起單位代號
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
        System.IO.StreamWriter sw = null;
        string xml = "";
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

            //填表人
            string creatorId = si.fillerID;

            //填表人員的主管
            string[] userGuidValues = base.getUserGUID(engine, creatorId);
            string[] values = base.getUserManagerInfo(engine, userGuidValues[0]);
            string managerGUID = values[0];
            
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];
            //sw.WriteLine("填表人員的主管=" + managerId);

            //審核人員
            string checkByGUID = CheckByGUID.GuidValueText;
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }

            //會簽人員１，會簽人員２            
            string countersign1GUID = Countersign1GUID.GuidValueText;
            string countersign1Id = "";
            if (!countersign1GUID.Equals(""))
            {
                values = base.getUserInfo(engine, countersign1GUID);
                countersign1Id = values[0];
            }
            string countersign2GUID = Countersign2GUID.GuidValueText;
            string countersign2Id = "";
            if (!countersign2GUID.Equals(""))
            {
                values = base.getUserInfo(engine, countersign2GUID);
                countersign2Id = values[0];
            }

            //MIS處理人員
            string misOwnerGUID = MisOwnerGUID.GuidValueText;
            string misOwnerId = "";
            if (!misOwnerGUID.Equals(""))
            {
                values = base.getUserInfo(engine, misOwnerGUID);
                misOwnerId = values[0];
            }
			
			//MIS窗口
			string misWindow = "SMP_MIS_Window";
			DataObjectSet set = currentObject.getChild("SmpInfoDemandDetail");
	        for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
	        {
	            string requestType = set.getAvailableDataObject(i).getData("RequestType");
                string requestItem = set.getAvailableDataObject(i).getData("RequestItem");

				if (requestType.Equals("電腦一般應用問題"))
	            {
	                misWindow = "SMP_MIS_OA";
	            }
                if (requestItem.Equals("鼎新ERP"))
                {
                    misWindow = "SMP_MIS_DSC";
                }
                if (requestItem.Equals("MES"))
                {
                    misWindow = "SMP_MIS_MES";
                }
	        }

            xml += "<SPIT001>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + creatorId + "</originator>";
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";
            xml += "<checkby2 DataType=\"java.lang.String\"></checkby2>";
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
            xml += "<miswindow DataType=\"java.lang.String\">" + misWindow + "</miswindow>";
            xml += "<mismgr DataType=\"java.lang.String\">SMP-MISMGR</mismgr>";
            xml += "<misowner DataType=\"java.lang.String\">" + misOwnerId + "</misowner>";
            xml += "<countersign1 DataType=\"java.lang.String\">" + countersign1Id + "</countersign1>";
            xml += "<countersign2 DataType=\"java.lang.String\">" + countersign2Id + "</countersign2>";
            xml += "<notify1 DataType=\"java.lang.String\"></notify1>";
            xml += "</SPIT001>";
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
        param["SPIT001"] = xml;
        return "SPIT001";
    }

    protected void OriginatorGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                
        //更新申請人資料
        string[] userValues = base.getUserInfoById(engine, OriginatorNumber.ValueText);
        if (userValues[0] != "")
        {
            OriginatorCName.ValueText = userValues[1];
            OriginatorEName.ValueText = userValues[2];
            Title.ValueText = userValues[3];
        }
        else
        {
            OriginatorCName.ValueText = "";
            OriginatorEName.ValueText = "";
            Title.ValueText = "";
        }

        //更新出差人員主要部門
        string[] userGuidValues = base.getUserGUID(engine, OriginatorNumber.ValueText);
        if (userGuidValues[0] != "")
        {
            string[] userDeptValues = base.getDeptInfo(engine, userGuidValues[0]);
            if (userDeptValues[0] != "")
            {
                OriginatorDeptGUID.ValueText = userDeptValues[0];
                OriginatorDeptGUID.doValidate();
            }
            else
            {
                OriginatorDeptGUID.ValueText = "";
            }
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
        base.rejectProcedure();
        if (backActID == "Creator") //流程之中, 申請人關卡的 ID 值
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


    protected void RequestList_ShowRowData(DataObject objects)
    {
        RequestType.ValueText = objects.getData("RequestType");
        RequestItem.ValueText = objects.getData("RequestItem");
        RequestDesc.ValueText = objects.getData("RequestDesc");
    }

    protected bool RequestList_SaveRowData(DataObject objects, bool isNew)
    {
        DataObjectSet chkType = RequestList.dataSource;
        for (int i = 0; i < chkType.getAvailableDataObjectCount(); i++)
        {
            string requestType = chkType.getAvailableDataObject(i).getData("RequestType");
            if (!RequestType.ValueText.Equals(requestType))
            {
                MessageBox("不同需求類別，請分開填單");
                return false;
            }

        }
        
        if (RequestType.ValueText.Equals(""))
        {
            MessageBox("必須填寫需求類別");
            return false;
        }
        if (RequestItem.ValueText.Equals(""))
        {
            MessageBox("必須填寫需求項目");
            return false;
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("HeaderGUID", "TEMP");
			objects.setData("RequestType", RequestType.ValueText);
	        objects.setData("RequestItem", RequestItem.ValueText);
	        objects.setData("RequestDesc", RequestDesc.ValueText);			
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        return true;
    }



    //笨方法:以數組形式保存RequestType/RequestItem的值

      string[] STRSReqI1 = new string[] {"","ERP Portal","EasyFlow","MES","Oracle ERP","PLM賬號權限","PLM表單權限","PLM文件權限","SRM(供應商)","SRM/GPM/ITS(內部)","LYCN","鼎新ERP"};
      string[] STRSReqI23 = new string[] {"","ERP Portal","EasyFlow","GPM","ITS","MES","Oracle ERP","PLM","SRM","鼎新ERP" };
      //
    protected void RequestType_SelectChanged(string value)
    {
          string[,] ids=null;
          //------------------------------------------------------------------------------
        string strRequestType = RequestType.ValueText;
        if (strRequestType.Trim() != "" && strRequestType.Trim()!=string.Empty)
        {
              if (strRequestType == "1.應用系統-賬號申請/權限異動")
              {
                    ids = new string[STRSReqI1.Length, 2];
                    for (int i = 0; i < STRSReqI1.Length; i++)
                    {
                          ids[i, 0] = STRSReqI1[i].ToString();
                          ids[i, 1] = STRSReqI1[i].ToString();
                    }
              }
              else
              {
                    ids = new string[STRSReqI23.Length, 2];
                    for (int i = 0; i < STRSReqI23.Length; i++)
                    {
                          ids[i, 0] = STRSReqI23[i].ToString();
                          ids[i, 1] = STRSReqI23[i].ToString();
                    }
              }


            
                //清除說明
            lbRequestTips.Text = "";
            RequestItem.setListItem(ids);            
        }
        else
        {
              ids = new string[1, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            lbRequestTips.Text = "";
            RequestItem.setListItem(ids);
        }
       
    }

    protected void RequestItem_SelectChanged(string value)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        string sql = null;
        DataSet ds = null;
        int count = 0;

        string strRequestItem = RequestItem.ValueText;
        string strRequestType = RequestType.ValueText;
        if (!strRequestItem.Equals(""))
        {
            sql = "select distinct Description From SmpInfoDemandType where RequestItem='" + value + "' and RequestType='" + strRequestType + "' ";
            ds = engine.getDataSet(sql, "TEMP");
            count = ds.Tables[0].Rows.Count;

            if (count > 0)
            {
                lbRequestTips.Text = ds.Tables[0].Rows[0][0].ToString();
               
            }
            else
            {
                lbRequestTips.Display = false;
            }
        }
    }

    protected void OriginatorNumber_SingleFieldButtonClick(object sender, EventArgs e)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        //更新基本資料
        string[] userValues = base.getUserInfoById(engine, OriginatorNumber.ValueText);
        if (userValues[0] != "")
        {
            OriginatorNumber.ValueText = userValues[0];  //申請人工號
            OriginatorCName.ValueText = userValues[1];   //中文名
            OriginatorEName.ValueText = userValues[2];   //英文名
            Title.ValueText = userValues[3];              // 職稱
        }

        //更新申請人員主要部門
        string[] userGuidValues = base.getUserGUID(engine, OriginatorNumber.ValueText);
        if (userGuidValues[0] != "")
        {
            string[] userDeptValues = base.getDeptInfo(engine, userGuidValues[0]);
            if (userDeptValues[0] != "")
            {
                OriginatorDeptGUID.ValueText = userDeptValues[0];
                OriginatorDeptGUID.doValidate();
            }
            else
            {
                OriginatorDeptGUID.ValueText = "";
            }
        }
    }

}