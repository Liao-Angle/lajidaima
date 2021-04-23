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
using com.dsc.flow.data;

public partial class Program_SCQ_Form_EG0108_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EG0108";
        AgentSchema = "WebServerProject.form.EG0108.EG0108Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "EASYFLOW";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        dlr.clientEngineType = engineType;
        dlr.connectDBString = connectString;

        SheetNo.Display = false;
        Subject.Display = false;

        try
        {

            string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + Session["UserID"].ToString() + "'";
            DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
            string SQLstr = "";
            if (dtbm.Rows[0][0].ToString() != "")
            {
                SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtbm.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")";
                EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'
                dlr.whereClause = "";


            }
            else
            {
                EmpNo.whereClause = "1=2";
            }

            EmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                EmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'";
            }
            else
            {
                EmpNo.whereClause = "1=2";
            }
            EmpNo.DoEventWhenNoKeyIn = false;
        }

        //取得HR假別設定
        string[,] leave = new string[3, 2] {{ "0", "無" }, { "A01", "事假" }, { "C01", "年休假" } };
        
        LeaveType.setListItem(leave);

        //取得外出類型
        string[,] outt = new string[3, 2] { { "請選擇", "請選擇" }, { "公務外出", "公務外出" }, { "私事外出", "私事外出" } };
        OutType.setListItem(outt);
        

        if (si.fillerID.Substring(0, 1) == "Y")
        {
            this.Panel2.Visible = true;
        }
        else
        {
            this.Panel1.Visible = true;
        }

        PartNo.ReadOnly = true;
        DtName.ReadOnly = true;
        OutTime.ReadOnly = true;
        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        base.showData(engine, objects);

        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        PartNo.ValueText = objects.getData("PartNo");
        LeaveType.ValueText = objects.getData("LeaveType");
        DtName.ValueText = objects.getData("DtName");
        OutType.ValueText = objects.getData("OutType");
        OutB.ValueText = objects.getData("OutB");
        OutE.ValueText = objects.getData("OutE");
        OutTime.ValueText = objects.getData("OutTime");
        OutPlace.ValueText = objects.getData("OutPlace");
        dlr.ValueText = objects.getData("dlr");
        OutReason.ValueText = objects.getData("OutReason");

        //顯示發起資料

        PartNo.ReadOnly = true;
        DtName.ReadOnly = true;
        LeaveType.ReadOnly = true;
        EmpNo.ReadOnly = true;
        OutType.ReadOnly = true;
        OutB.ReadOnly = true;
        OutE.ReadOnly = true;
        OutReason.ReadOnly = true;
        dlr.ReadOnly = true;
        OutPlace.ReadOnly = true;


    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        //產生單號並儲存至資料物件
        base.saveData(engine, objects);

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("LeaveType", LeaveType.ValueText);
            objects.setData("OutType", OutType.ValueText);
            objects.setData("OutB", OutB.ValueText);
            objects.setData("DtName", DtName.ValueText);
            objects.setData("OutE", OutE.ValueText);
            objects.setData("dlr", dlr.ValueText);
            objects.setData("OutReason", OutReason.ValueText);
            objects.setData("OutPlace", OutPlace.ValueText);
            objects.setData("OutTime", OutTime.ValueText);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

        }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        string actName = (string)getSession("ACTName");
        //新增判斷資料
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 外出申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
            }
        }

        if (OutType.ValueText == "請選擇")
        {
            pushErrorMessage("請選擇外出類型");
            result = false;
        }

        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        return getSubmitInfo(engine, objects, si);
    }
    
    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定自動編碼格式所需變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    /// <summary>
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //填表人
        string creatorId = si.fillerID;
        string zszg ="";
        string daili="";
        string[] values = base.getUserManagerInfoID(engine, creatorId);

        if(!string.IsNullOrEmpty(dlr.ValueText.ToString()))
        {
            daili=dlr.ValueText;
        }
        else
        {
            zszg=values[1];
        }



        xml += "<EG0108>";
        xml += "<daili DataType=\"java.lang.String\">" + daili + "</daili>";//代理主管簽核
        xml += "<zszg DataType=\"java.lang.String\">" + zszg + "</zszg>";//直屬主管否簽核
        //xml += "<isrs DataType=\"java.lang.String\">" + bmzg + "</isrs>";//人事是否簽核
        //xml += "<ismfg DataType=\"java.lang.String\">" + bmzg + "</ismfg>";//部門最高主管是否簽核
        xml += "</EG0108>";

        param["EG0108"] = xml;
        return "EG0108";
    }


    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);
    }

    /// <summary>
    /// 若有加簽，送簽核前呼叫。
    /// 加簽時系統會設定Session("IsAddSign")，所以必需在saveData時執行 setSession("IsAddSign", "AFTER");
    /// AFTER 代表往後簽核
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
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

    /// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        //"Y"代表同意
        if (result == "Y")
        {
            //開發同意簽核後自動化流程
        }
    }
    protected void EmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h = base.getHRUsers(engine, EmpNo.ValueText);
        PartNo.ValueText = h["PartNo"].ToString();
        DtName.ValueText = h["DtName"].ToString();
    }

    protected void ccEDate_DateTimeClick(string values)
    {
        string a = OutB.ValueText;
        string b = OutE.ValueText;

        if (Convert.ToDateTime(b) <= Convert.ToDateTime(a))
        {
            MessageBox("外出時間止必須大於外出時間起");
            OutTime.ValueText = "";
        }
        else if(a.Substring(0,10)!=b.Substring(0,10))
        {
            MessageBox("請確認起始日期為當天");
            OutTime.ValueText = "";
        }
        else if (a.Substring(14, 1) == "1" || b.Substring(14, 1) == "1" || a.Substring(14, 1) == "2" || b.Substring(14, 1) == "2" || a.Substring(14, 1) == "4" || b.Substring(14, 1) == "4" || a.Substring(14, 1) == "5" || b.Substring(14, 1) == "5" || a.Substring(15, 1) != "0" || b.Substring(15, 1) != "0")
        {
            MessageBox("請確認外出起始時間以整點或30分鐘為單位");
            OutTime.ValueText = "";
        }
        else
        {
            TimeSpan c = Convert.ToDateTime(b).Subtract(Convert.ToDateTime(a));
            string d = c.ToString().Substring(0,5);
            OutTime.ValueText = (Convert.ToInt32(d.Substring(0, 2)) * 60 + Convert.ToInt32(d.Substring(3, 2)))/60.0 + "";
        }
        
    }
    protected void OutType_SelectChanged(string value)
    {
        if (OutType.ValueText == "公務外出")
        {
            string[,] leave1 = new string[1, 2] { { "0", "無" } };
            LeaveType.setListItem(leave1);
        }
        else
        {
            string[,] leave = new string[2, 2] { { "A01", "事假" }, { "C01", "年休假" } };
            LeaveType.setListItem(leave);
        }
    }
}
