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


public partial class SmpProgram_System_Form_SPAD006_Form : SmpAdFormPage
{

    protected override void init()
    {	
        ProcessPageID = "SPAD006"; //=作業畫面代碼
        AgentSchema = "WebServerProject.form.SPAD006.SmpForeignTrvlPreDocAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";		
    }


    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
		//sw.WriteLine("initUI ");
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        //主旨不顯示於發起單據畫面
        SheetNo.Display = false;
        Subject.Display = false;
		//sw.WriteLine("line 45 ");
        //申請單位
        DeptGUID.clientEngineType = engineType;
        DeptGUID.connectDBString = connectString;
        DeptGUID.ValueText = si.submitOrgID;
        DeptGUID.doValidate();
		
		//sw.WriteLine("DeptGUID>>  " + DeptGUID.ValueText);
		
        //申請人員
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;            
        if (OriginatorGUID.ValueText.Equals(""))
        {
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }
        
        //職務代理人
        AgentGUID.clientEngineType = engineType;
        AgentGUID.connectDBString = connectString;
        if (AgentGUID.ValueText.Equals(""))
        {
            AgentGUID.ValueText = si.fillerID; //預設帶出登入者
            AgentGUID.doValidate(); //帶出人員開窗元件中的人員名稱
        }

        // 職務名稱
        //sw.WriteLine("si.fillerID >>  " + si.fillerID);
        string[] userFunctionValues = base.getUserInfoById(engine, si.fillerID);
        //string[] userFunctionValues = base.getUserFunctionsId(engine, si.fillerID);
        if (userFunctionValues[0] != "")
        {
            Title.ValueText = userFunctionValues[3];
        }
        else
        {
            Title.ValueText = "";
        }
        //sw.WriteLine("Title.ValueText >>  " + Title.ValueText);
		
		//審核人
        CheckByGUID.clientEngineType = engineType;
        CheckByGUID.connectDBString = connectString;

        //DeliveryDate.ValueText = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
        DeliveryDate.ReadOnly = true;
        CompleteDate.ReadOnly = true;
        //DeliveryDate.AllowEmpty = true;
        //CompleteDate.AllowEmpty = true;
		
		//sw.Close();

        //改變工具列順序
        base.initUI(engine, objects);
    }

    protected override void showData(AbstractEngine engine, DataObject objects)
    {
	
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"C:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
        //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
		//sw.WriteLine("showData ");
		
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

        //申請人員
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID

        //申請單位
        DeptGUID.ValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
        
        Title.ValueText = objects.getData("Title");

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
        

        Comment.ValueText = objects.getData("Comment");

        //Passport.Checked = objects.getData("Password");
        if (objects.getData("Passport").Equals("Y")) { Passport.Checked = true; } else { Passport.Checked = false; }
        if (objects.getData("MTPs").Equals("Y")){ MTPs.Checked = true; } else { MTPs.Checked = false; }
        if (objects.getData("MtpsPlus").Equals("Y")){ MtpsPlus.Checked = true; } else { MtpsPlus.Checked = false; }
        if (objects.getData("USvisa").Equals("Y")) { USvisa.Checked = true; } else { USvisa.Checked = false; }
        if (objects.getData("Other").Equals("Y")) { Other.Checked = true; } else { Other.Checked = false; }
		DeliveryDate.ValueText = objects.getData("DeliveryDate");
        CompleteDate.ValueText = objects.getData("CompleteDate");
        //sw.WriteLine("isAddNew ");		
        if (!isAddNew)
        {
            Subject.ReadOnly = true;
            SheetNo.ReadOnly = true;
            DeptGUID.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            Title.ReadOnly = true;
            AgentGUID.ReadOnly = true;
            CheckByGUID.ReadOnly = true;
            Comment.ReadOnly = true;
            Passport.ReadOnly = true;
            MTPs.ReadOnly = true;
            MtpsPlus.ReadOnly = true;
            USvisa.ReadOnly = true;
            Other.ReadOnly = true;
            DeliveryDate.ReadOnly = true;
            CompleteDate.ReadOnly = true;
        }
        //sw.WriteLine("actName=> " + actName);
        if (actName.Equals("Secretary"))
        {
            DeliveryDate.ReadOnly = false;     
        }
        if (actName.Equals("Secretary2"))
        {
            CompleteDate.ReadOnly = false;
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

            if (isAddNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("Subject", Subject.ValueText);
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("Title", Title.ValueText);
                objects.setData("AgentGUID", AgentGUID.GuidValueText);
                objects.setData("CheckByGUID", CheckByGUID.GuidValueText);
                objects.setData("Comment", Comment.ValueText);
                if (Passport.Checked)  {objects.setData("Passport", "Y");} else {objects.setData("Passport", "N");}
                if (MTPs.Checked) { objects.setData("MTPs", "Y"); } else { objects.setData("MTPs", "N"); }
                if (MtpsPlus.Checked) { objects.setData("MtpsPlus", "Y"); } else { objects.setData("MtpsPlus", "N"); }
                if (USvisa.Checked) { objects.setData("USvisa", "Y"); } else { objects.setData("USvisa", "N"); }
                if (Other.Checked) { objects.setData("Other", "Y"); } else { objects.setData("Other", "N"); }
                objects.setData("OtherComment", OtherComment.ValueText);
                //objects.setData("DeliveryDate", DeliveryDate.ValueText);
                //objects.setData("CompleteDate", CompleteDate.ValueText);
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("DATA_STATUS", "Y");
                //主旨含單號
                base.saveData(engine, objects);
                //sw.WriteLine("主旨含單號");
            }
			//sw.WriteLine("saveData Line217==> " + DeliveryDate.ValueText);
            objects.setData("DeliveryDate", DeliveryDate.ValueText);
            objects.setData("CompleteDate", CompleteDate.ValueText);
			//sw.WriteLine("saveData Line220==> " + CompleteDate.ValueText);
            
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
            if (AgentGUID.ValueText.Equals(""))
            {
                strErrMsg += "代理人員不可為空值\n";
            }

            if (Comment.ValueText.Equals(""))
            {
                strErrMsg += "出差（預辦）事由不可為空值\n";
            }
            if (Passport.Checked.Equals(false) && MTPs.Checked.Equals(false) && MtpsPlus.Checked.Equals(false) && USvisa.Checked.Equals(false) && Other.Checked.Equals(false))
            {
                strErrMsg += "請至少勾選一簽證申請 / 護照申請\n";
            }
            if (Other.Checked.Equals(true))
            {
                if (OtherComment.ValueText.Equals(""))
                {
                    strErrMsg += "請填寫簽證申請 / 護照申請-其他欄位\n";
                }
            }

            //代理人員不可設為請假人員
            if (OriginatorGUID.ValueText.Equals(AgentGUID.ValueText))
            {
                strErrMsg += "代理人員不可設為出差人員!\n";
            }

            //設定主旨
            //if (Subject.ValueText.Equals(""))
            //{
                values = base.getUserInfo(engine, OriginatorGUID.GuidValueText);
                string subject = "【預辦證件人員：" + values[1] + " 】";
                //if (Subject.ValueText.Equals(""))
                //{
                    Subject.ValueText = subject;
                //}
            //}
        
        }
        if (actName.Equals("Secretary"))
        {
            if (DeliveryDate.ValueText.Equals(""))
            {
                strErrMsg += "請維護送件日期!\n";
            }
            //DeliveryDate.ReadOnly = false;
        }
        if (actName.Equals("Secretary2"))
        {
            if (CompleteDate.ValueText.Equals(""))
            {
                strErrMsg += "請維護完成日期!\n";
            }
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
        try
        {
           // sw = new System.IO.StreamWriter(@"D:\ECP\WebFormPT\web\LogFolder\WebService.log", true);
			//sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\WebFormPT.log", true, System.Text.Encoding.Default);
            //sw = new System.IO.StreamWriter(@"F:\ECP\WebFormPT\web\log\spad006.log", true);
            //sw.WriteLine("setFlowVariables");
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

            
            string agentId = AgentGUID.ValueText; //代理人
            string notifierId = si.fillerID + ";"; //通知人員
			string deptAsstId = ""; //部門收發
	        string[] deptInfo = base.getDeptInfo(engine, OriginatorGUID.GuidValueText);
	        if (!deptInfo[0].Equals(""))
	        {
	            string[] userFunc = getUserRoles(engine, "部門收發", deptInfo[0]);
	            deptAsstId = userFunc[2];
	        }			

            //填表人
            string creatorId = si.fillerID;
            //sw.WriteLine("creatorId=>" + creatorId);

            //出差人員
            string originatorId = OriginatorGUID.ValueText;
            //sw.WriteLine("originatorId=>" + originatorId);

            //出差人員的主管
            string originatorGUID = OriginatorGUID.GuidValueText;
            //sw.WriteLine("originatorGUID=" + originatorGUID);
            string[] values = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = values[0];
            
            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];
            //sw.WriteLine("managerId=" + managerId);

            //審核人員
            string checkByGUID = CheckByGUID.GuidValueText;
            string checkById = "";
            if (!checkByGUID.Equals(""))
            {
                values = base.getUserInfo(engine, checkByGUID);
                checkById = values[0];
            }
            //sw.WriteLine("checkById=" + checkById);
            

            //代理人
            string agentGUID = AgentGUID.GuidValueText; //使用表單中定義checkby2

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

            //sw.WriteLine("notifierId=" + notifierId);
            xml += "<SPAD006>";
            xml += "<creator DataType=\"java.lang.String\">" + creatorId + "</creator>";
            xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
            xml += "<checkby1 DataType=\"java.lang.String\">" + checkById + "</checkby1>";
            xml += "<checkby2 DataType=\"java.lang.String\">" + agentId + "</checkby2>";
            xml += "<manager DataType=\"java.lang.String\">" + managerId + "</manager>";
            xml += "<secretary DataType=\"java.lang.String\">SMP-SECRETARY</secretary>";
            xml += "<secretary2 DataType=\"java.lang.String\">SMP-SECRETARY</secretary2>";
            xml += "<agnet DataType=\"java.lang.String\">" + agentId + "</agnet>";
            xml += "<notifier DataType=\"java.lang.String\">" + notifierId + "</notifier>";
            xml += "</SPAD006>";
            //sw.WriteLine("xml: " + xml);
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
           // if (sw != null) sw.Close();
        }
        //表單代號
        param["SPAD006"] = xml;
        return "SPAD006";
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
        }
        else
        {
            DeptGUID.ValueText = "";
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
        if (backActID == "CREATOR") //流程之中, 申請人關卡的 ID 值
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

}