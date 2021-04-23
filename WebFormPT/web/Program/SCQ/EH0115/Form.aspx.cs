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

public partial class Program_SCQ_Form_EH0115_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH0115";
        AgentSchema = "WebServerProject.form.EH0115.EH0115Agent";
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
        string leave = sp.getParam("LeaveType");
        if (!leave.Equals(""))
        {
            string[] strs = leave.Split(new char[] { ';' });
            string[,] ids = new string[strs.Length, 2];
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].IndexOf('/') > 0 && strs[i].IndexOf('/') < (strs[i].Length - 1))
                {
                    ids[i, 0] = strs[i].Split(new char[] { '/' })[0];
                    ids[i, 1] = strs[i].Split(new char[] { '/' })[1];
                }
            }
            LeaveType.setListItem(ids);

            string[,] tidss = new string[44, 2];
            int st = 8;
            for (int j = 0; st < 30; )
            {
                if (st < 24)
                {
                    tidss[j, 0] = st.ToString().PadLeft(2, '0') + ":00";
                    tidss[j, 1] = st.ToString().PadLeft(2, '0') + ":00";

                    tidss[j + 1, 0] = st.ToString().PadLeft(2, '0') + ":30";
                    tidss[j + 1, 1] = st.ToString().PadLeft(2, '0') + ":30";
                }
                else
                {
                    tidss[j, 0] = st.ToString().PadLeft(2, '0') + ":00";
                    tidss[j, 1] = (st - 24).ToString().PadLeft(2, '0') + ":00(+1)";

                    tidss[j + 1, 0] = st.ToString().PadLeft(2, '0') + ":30";
                    tidss[j + 1, 1] = (st - 24).ToString().PadLeft(2, '0') + ":30(+1)";
                }
                j++;
                j++;
                st++;
            }
            BeginTime.setListItem(tidss);
            BeginTime.ValueText = sp.getParam("DayBegin");
            EndTime.setListItem(tidss);
            EndTime.ValueText = sp.getParam("DayEnd");

        }
    
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
        string actName = (string)getSession("ACTName");

        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        Department.ValueText = objects.getData("Department");
        LeaveType.ValueText = objects.getData("LeaveType");
        LeaveSDate.ValueText = objects.getData("LeaveSDate");
        LeaveEDate.ValueText = objects.getData("LeaveEDate");
        DtName.ValueText = objects.getData("DtName");
        LeaveDate.ValueText = objects.getData("LeaveDate");
        BeginTime.ValueText = objects.getData("BeginTime");
        EndTime.ValueText = objects.getData("EndTime");
        BillNo.ValueText = objects.getData("BillNo");
        Reason.ValueText = objects.getData("Reason");

        if (actName.ToString() == "HR考勤")
        {
            BILL.Visible = true;
        }

        //顯示發起資料

        Department.ReadOnly = true;
        DtName.ReadOnly = true;
        LeaveType.ReadOnly = true;
        LeaveDate.ReadOnly = true;
        LeaveEDate.ReadOnly = true;
        LeaveSDate.ReadOnly = true;
        BeginTime.ReadOnly = true;
        EndTime.ReadOnly = true;
        Reason.ReadOnly = true;



    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
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
            objects.setData("Department", Department.ValueText);
            objects.setData("LeaveType", LeaveType.ValueText);
            objects.setData("LeaveSDate", LeaveSDate.ValueText);
            objects.setData("LeaveEDate", LeaveEDate.ValueText);
            objects.setData("DtName", DtName.ValueText);
            objects.setData("LeaveDate", LeaveDate.ValueText);
            objects.setData("BeginTime", BeginTime.ValueText);
            objects.setData("EndTime", EndTime.ValueText);
            objects.setData("Reason", Reason.ValueText);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

        }
        else
        {
            objects.setData("BillNo", BillNo.ValueText);
            base.saveData(engine, objects);
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
            string subject = "【 銷假申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
            }
        }
        if (Reason.ValueText == "")
        {
            MessageBox("請填寫銷假原因！");
            result = false;
        }
        if (LeaveDate.ValueText != "" && LeaveSDate.ValueText != "")
        {
            MessageBox("請假日期和請假開始日期只能填寫一種！");
            result = false;
        }

        if (actName.ToString() == "HR考勤"&&BillNo.ValueText=="")
        {
            MessageBox("請填寫東寶系統請假單號！");
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
        


        xml += "<EH0115>";
        //xml += "<issj DataType=\"java.lang.String\">" + issj + "</issj>";//上級主管是否簽核
        //xml += "<iscj DataType=\"java.lang.String\">" + iscj + "</iscj>";//處級主管是否簽核
        ////xml += "<isrs DataType=\"java.lang.String\">" + bmzg + "</isrs>";//人事是否簽核
        ////xml += "<ismfg DataType=\"java.lang.String\">" + bmzg + "</ismfg>";//部門最高主管是否簽核
        xml += "</EH0115>";

        param["EH0115"] = xml;
        return "EH0115";
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
        Department.ValueText = h["PartNo"].ToString();
        DtName.ValueText = h["DtName"].ToString();
    }
}
