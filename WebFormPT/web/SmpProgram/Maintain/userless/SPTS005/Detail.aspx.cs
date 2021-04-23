using System;
using System.Data;
using System.Configuration;
using System.Collections;
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

public partial class SmpProgram_maintain_SPTS005_Detail : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {        
        EmployeeGUID.clientEngineType = (string)Session["engineType"];
        EmployeeGUID.connectDBString = (string)Session["connectString"];
        DeptGUID.clientEngineType = (string)Session["engineType"];
        DeptGUID.connectDBString = (string)Session["connectString"];
        
        //報名方式
        string[,] ids = null;   
        ids = new string[,]{ 
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005m_detail_aspx.language.ini", "message", "1", "調訓")},     
                        {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005m_detail_aspx.language.ini", "message", "2", "自行報名")},     
                    };
        ApplyWay.setListItem(ids);
        ApplyWay.ValueText = "1";

        //簽到        
        ids = new string[,]{ 
                        {"Y","Y"},
                        {"N","N"}       
                    };
        Attendance.setListItem(ids);
        Attendance.ValueText = "Y";

        //取得證書                 
        GetCertificate.setListItem(ids);
        GetCertificate.ValueText = "N";
        
        //費用
        Fee.ValueText = "0";

        //簽定訓練合約書
        Sign.setListItem(ids);
        Sign.ValueText = "N";

        //通過狀態
        Pass.setListItem(ids);
        Pass.ValueText = "Y";
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        EmployeeGUID.GuidValueText = objects.getData("EmployeeGUID");
        EmployeeGUID.doGUIDValidate();
        DeptGUID.GuidValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
        ApplyWay.ValueText = objects.getData("ApplyWay");
        Attendance.ValueText = objects.getData("Attendance");
        GetCertificate.ValueText = objects.getData("GetCertificate");
        CertificateNo.ValueText = objects.getData("CertificateNo");
        if (objects.getData("Fee").Equals("") )
            Fee.ValueText ="0";
        else
            Fee.ValueText = objects.getData("Fee");
        Sign.ValueText = objects.getData("Sign");
        Expire.ValueText = objects.getData("Expire");
        Pass.ValueText = objects.getData("Pass");
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        string id = EmployeeGUID.ValueText;
        Boolean hasDuplicate =false;
        //Check Data 
        //string traineeId = (string)getSession((string)Session["UserID"], "TraineeId");
        ArrayList traineeInfo = (ArrayList)getSession((string)Session["UserID"], "TraineeInfo");
        if (traineeInfo == null)
            throw new Exception("查無Session:traineeInfo，無法判斷資料是否重覆");
        else
        {
            if (isNew)
            {
                foreach (var info in traineeInfo)
                {
                    if (info.ToString().IndexOf(id) != -1)
                    {
                        hasDuplicate = true;
                        break;
                    }
                }
            }
            else
            {
                string guid = objects.getData("GUID");

                foreach (var info in traineeInfo)
                {

                    if (info.ToString().IndexOf(guid) == -1 && info.ToString().IndexOf(id) != -1)
                    {
                        hasDuplicate = true;
                        break;
                    }
                }

            }

            if (hasDuplicate)
            {
                throw new Exception("員工工號重覆!");
            }
            else
            {
                if (isNew)
                {
                    objects.setData("GUID", IDProcessor.getID(""));
                    objects.setData("CourseFormGUID", "temp");
                    objects.setData("IS_DISPLAY", "Y");
                    objects.setData("IS_LOCK", "N");
                    objects.setData("DATA_STATUS", "Y");
                }
                objects.setData("EmployeeGUID", EmployeeGUID.GuidValueText);
                objects.setData("id", id);
                objects.setData("userName", EmployeeGUID.ReadOnlyValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("deptId", DeptGUID.ValueText);
                objects.setData("deptName", DeptGUID.ReadOnlyValueText);
                objects.setData("ApplyWay", ApplyWay.ValueText);
                objects.setData("Attendance", Attendance.ValueText);
                objects.setData("GetCertificate", GetCertificate.ValueText);
                objects.setData("CertificateNo", CertificateNo.ValueText);
                objects.setData("Fee", Fee.ValueText);
                objects.setData("Sign", Sign.ValueText);
                objects.setData("Expire", Expire.ValueText);
                objects.setData("Pass", Pass.ValueText);
            }
        }
    }
        
    /// <summary>
    /// 帶出部門 
    /// </summary>
    /// <param name="values"></param>
    protected void EmployeeGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string userGUID = EmployeeGUID.GuidValueText;
        string sql = "select organizationUnitOID from Functions where occupantOID = '" + userGUID + "'";
        string orgUnitGUID = (string)engine.executeScalar(sql);
        if (!orgUnitGUID.Equals(""))
        {
            DeptGUID.GuidValueText = orgUnitGUID;
            DeptGUID.doGUIDValidate();
        }         
        engine.close();
    }
}