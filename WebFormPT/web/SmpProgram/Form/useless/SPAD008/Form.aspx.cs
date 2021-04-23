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

public partial class SmpProgram_Form_SPAD008_Form : SmpAdFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 初使化
    /// </summary>
    protected override void init()
    {       
        try
        {      
            ProcessPageID = "SPAD008"; //=作業畫面代碼
            AgentSchema = "WebServerProject.form.SPAD008.SmpBusinessCardAgent";
            ApplicationID = "SMPFORM";
            ModuleID = "SPAD";
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {            
        }
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
        DataSet ds = null;
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
                {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad008_form_aspx.language.ini", "message", "smp", "新普科技")},
                {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad008_form_aspx.language.ini", "message", "tp", "中普科技")}
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

                //篩選部門
                DeptName.whereClause = " ORG = '" + orgId + "' ";
            }
            CompanyCode.ReadOnly = true;

            OriginatorGUID.clientEngineType = engineType;
            OriginatorGUID.connectDBString = connectString;
            OriginatorGUID.ValueText = si.fillerID; //預設帶出登入者
            OriginatorGUID.doValidate(); //帶出人員開窗元件中的人員名稱                      

            //申請人Email,英文名           
            string[] userValues = base.getUserInfoById(engine, si.fillerID);
           
            if (userValues[0] != "")
            {
                EngName.ValueText = userValues[2];   //英文名    
                Email.ValueText = userValues[4];   //Email   
            }
            else
            {
                EngName.ValueText = "";   //英文名    
                Email.ValueText = "";   //Email 
            }

            //部門,職稱
            DeptName.clientEngineType = engineType;
            DeptName.connectDBString = connectString;
            Title.clientEngineType = engineType;
            Title.connectDBString = connectString;

            //來自人事薪資系統的資料,部門/部門別名/預設傳傎/廠址/職稱/職稱別名
            //engineErp = getErpEngine();
            engineErp = getErpEngine(CompanyCode.ValueText);
            sql = "select DEPT,DEPTNAME,ENGDEPTNAME,FAX,ADDRESS,TITLE,ENGTITLE from SMP_TITLE WHERE ID='" + si.fillerID + "'";           
            ds = engineErp.getDataSet(sql, "TEMP");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {                
                DeptName.ValueText = ds.Tables[0].Rows[0][1].ToString(); //預設帶出部門名稱               
                DeptName.doValidate(); //帶出部門別名               
                EngDeptName.ValueText = ds.Tables[0].Rows[0][2].ToString();
                Fax.ValueText = ds.Tables[0].Rows[0][3].ToString();
                Address.ValueText = ds.Tables[0].Rows[0][4].ToString();
                Title.ValueText = ds.Tables[0].Rows[0][5].ToString();
                Title.doValidate(); //帶出職稱
                EngTitle.ValueText = ds.Tables[0].Rows[0][6].ToString();
            }

            //預設盒數
            NumberOfApply.ValueText = "1";

            //主旨不顯示於發起單據畫面
            SheetNo.Display = false;
            Subject.Display = false;
            EngDeptName.Display = false;
            EngTitle.Display = false;

            //非申請人填寫
            DeliveryDate.ReadOnly = true;
            CompleteDate.ReadOnly = true;
            OriginatorComment.ReadOnly = true;

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
        //英文姓名
        EngName.ValueText = objects.getData("EngName");
        //部門
        DeptName.ValueText = objects.getData("DeptName");
        //部門別名               
        DeptName.doGUIDValidate();
        EngDeptName.ValueText = objects.getData("EngDeptName");
        //中文職稱
        Title.ValueText = objects.getData("Title");
        //英文職稱
        Title.doGUIDValidate();
        EngTitle.ValueText = objects.getData("EngTitle");
        //email
        Email.ValueText = objects.getData("Email");
        //分機
        Ext.ValueText = objects.getData("Ext");
        //行動電話
        PhoneNumber.ValueText = objects.getData("PhoneNumber");
        //傳真
        Fax.ValueText = objects.getData("Fax");
        //盒數
        NumberOfApply.ValueText = objects.getData("NumberOfApply");
        //廠址
        Address.ValueText = objects.getData("Address");
        //名片稿送達日期
        DeliveryDate.ValueText = objects.getData("DeliveryDate");
        //名片預計送達日期
        CompleteDate.ValueText = objects.getData("CompleteDate");
        //申請人特殊要求
        SpecialRequire.ValueText = objects.getData("SpecialRequire");
        //申請人校稿意見
        OriginatorComment.ValueText = objects.getData("OriginatorComment");

        string actName = Convert.ToString(getSession("ACTName"));
        if (actName == "" || actName.Equals("申請人"))
        {
        }
        else
        {
            //表單發起後不允許修改
            EngName.ReadOnly = true;
            DeptName.ReadOnly = true;
            Title.ReadOnly = true;
            Email.ReadOnly = true;
            Ext.ReadOnly = true;
            PhoneNumber.ReadOnly = true;
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            NumberOfApply.ReadOnly = true;
            SpecialRequire.ReadOnly = true;
            OriginatorComment.ReadOnly = true;
            if (actName.Equals("人事承辦人員"))
            {
                DeliveryDate.ReadOnly = false;
                Fax.ReadOnly = false;
                Address.ReadOnly = false;
            }
            else
            {
                Fax.ReadOnly = true;
                Address.ReadOnly = true;
            }

            if (actName.Equals("校稿一"))
            {
                OriginatorComment.ReadOnly = false;
            }

            if (actName.Equals("校稿二"))
            {
                LblCompleteDate.IsNecessary = true;
                CompleteDate.ReadOnly = false;
            }
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
            objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
            objects.setData("EngName", EngName.ValueText);
            objects.setData("DeptName", DeptName.ValueText);
            objects.setData("EngDeptName", DeptName.ReadOnlyValueText);           
            objects.setData("Email", Email.ValueText);
            objects.setData("Ext", Ext.ValueText);
            objects.setData("PhoneNumber", PhoneNumber.ValueText);
            objects.setData("NumberOfApply", NumberOfApply.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            objects.setData("Title", Title.ValueText);
            objects.setData("EngTitle", Title.ReadOnlyValueText);
            objects.setData("Address", Address.ValueText);
            objects.setData("Fax", Fax.ValueText);
            objects.setData("SpecialRequire", SpecialRequire.ValueText);
            objects.setData("CompanyCode", CompanyCode.ValueText);
            base.saveData(engine, objects);
        }
        objects.setData("Title", Title.ValueText);
        objects.setData("EngTitle", Title.ReadOnlyValueText);
        objects.setData("Address", Address.ValueText);
        objects.setData("Fax", Fax.ValueText);
        objects.setData("DeliveryDate", DeliveryDate.ValueText);
        objects.setData("CompleteDate", CompleteDate.ValueText);
        objects.setData("OriginatorComment", OriginatorComment.ValueText);
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
                string subject = "【名片申請人員：" + values[1] + " 】";
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

        if (actName.Equals("人事承辦人員"))
        {
            if (Fax.ValueText.Equals(""))
            {
                strErrMsg += "請維護傳真號碼!\n";
            }

            if (Address.ValueText.Equals(""))
            {
                strErrMsg += "請維護廠區地址!\n";
            }

            if (DeliveryDate.ValueText.Equals(""))
            {
                strErrMsg += "請維護名片稿回覆日期!\n";
            }

            if (base.attachFile.dataSource.getAvailableDataObjectCount() == 0)
                strErrMsg += "請上傳名片稿附件!\n";
        }


        if (actName.Equals("校稿二"))
        {
            if (CompleteDate.ValueText.Equals(""))
            {
                strErrMsg += "請維護名片預計送達日期!\n";
            }
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
        si.ownerID = OriginatorGUID.ValueText; //申請人id
        si.ownerName = OriginatorGUID.ReadOnlyValueText;  //申請人名稱
        //si.ownerOrgID = depData[0];
        //si.ownerOrgName = depData[1];
        //si.submitOrgID = depData[0];
        depData = getDeptInfo(engine, OriginatorGUID.GuidValueText);
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

            //申請人部門主管
            string originatorGUID = OriginatorGUID.GuidValueText;            
            string[] values = base.getUserManagerInfo(engine, originatorGUID);
            string managerGUID = values[0];

            values = base.getUserInfo(engine, managerGUID);
            string managerId = values[0];            
            
            xml += "<SPAD008>";
            xml += "<hrOwner DataType=\"java.lang.String\">SPAD008-hrOwner</hrOwner>";
            xml += "<unitManager DataType=\"java.lang.String\">" + managerId + "</unitManager>";         
            // += "<unitManager DataType=\"java.lang.String\">3992</unitManager>";         
            xml += "</SPAD008>";

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
        }
        //表單代號
        param["SPAD008"] = xml;
        return "SPAD008";
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
        //sw = new System.IO.StreamWriter(@"F:\ECP\Code\WebFormPT\web\Log\spad008.log", true, System.Text.Encoding.Default);
        //sw.WriteLine("OriginatorGUID_SingleOpenWindowButtonClick()");

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

            //篩選部門
            DeptName.whereClause = " ORG = '" + CompanyCode.ValueText + "' ";

            DeptName.clientEngineType = engineType;
            DeptName.connectDBString = connectString;
            Title.clientEngineType = engineType;
            Title.connectDBString = connectString;

            //來自人事薪資系統的資料,部門/部門別名/預設傳傎/廠址/職稱/職稱別名
            //engineErp = getErpEngine();
            engineErp = getErpEngine(CompanyCode.ValueText);
            sql = "select DEPT,DEPTNAME,ENGDEPTNAME,FAX,ADDRESS,TITLE,ENGTITLE from SMP_TITLE WHERE ID='" + OriginatorGUID.ValueText + "'";

            ds = engineErp.getDataSet(sql, "TEMP");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DeptName.ValueText = ds.Tables[0].Rows[0][1].ToString(); //預設帶出部門名稱               
                DeptName.doValidate(); //帶出部門別名   
            //    EngDeptName.ValueText = ds.Tables[0].Rows[0][2].ToString();
                Fax.ValueText = ds.Tables[0].Rows[0][3].ToString();
                Address.ValueText = ds.Tables[0].Rows[0][4].ToString();
                Title.ValueText = ds.Tables[0].Rows[0][5].ToString();
                Title.doValidate();
            //    EngTitle.ValueText = ds.Tables[0].Rows[0][6].ToString();
            }
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
            //if (sw != null)
            //{
            //    sw.Close();
            //}
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
}

