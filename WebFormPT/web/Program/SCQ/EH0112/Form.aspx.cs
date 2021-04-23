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
using DSCWebControl;
using System.Text.RegularExpressions;


public partial class Program_SCQ_Form_EH0112_Form : SmpBasicFormPage
{
    private int MaxDSC = 34;
    private string DSCName = "DSCCheckBox";
    protected override void init()
    {
        ProcessPageID = "EH0112";
        AgentSchema = "WebServerProject.form.EH0112.EH0112Agent";
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
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
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
        if (si.fillerID.Substring(0, 1) == "Y")
        {
            this.Panel2.Visible = true;
        }
        else
        {
            this.Panel1.Visible = true;
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
        string check = objects.getData("Privilege");
        for (int i = 1; i <= MaxDSC; i++)
        {
            DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
            if (dsc != null && i <= check.Length && check.Substring(i - 1, 1) == "Y")
            {
                dsc.Checked = true;
            }
            if (dsc != null)
            {
                dsc.ReadOnly = false;
            }
        }

        HRCODE.Text = objects.getData("ID");
        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        ComeDate.ValueText = objects.getData("ComeDate");
        RqDate.ValueText = objects.getData("RqDate");
        PartNo.ValueText = objects.getData("PartNo");
        DtName.ValueText = objects.getData("DtName");
        LeaveDate.ValueText = objects.getData("LeaveDate");
        Gwei.ValueText = objects.getData("Gwei");
        GwType.ValueText = objects.getData("GwType");
        GwNo.ValueText = objects.getData("GwNo");
        Phone.ValueText = objects.getData("Phone");
        sgz.ValueText = objects.getData("sgz");
        note1.ValueText = objects.getData("note1");
        note2.ValueText = objects.getData("note2");
        note3.ValueText = objects.getData("note3");
        note4.ValueText = objects.getData("note4");
        note5.ValueText = objects.getData("note5");
        note6.ValueText = objects.getData("note6");
        note7.ValueText = objects.getData("note7");
        note8.ValueText = objects.getData("note8");
        note9.ValueText = objects.getData("note9");
        note10.ValueText = objects.getData("note10");
        note11.ValueText = objects.getData("note11");
        note12.ValueText = objects.getData("note12");
        PayNo35.ValueText = objects.getData("PayNo35");
        PayNo29.ValueText = objects.getData("PayNo29");
        PayNo34.ValueText = objects.getData("PayNo34");
        PayNo38.ValueText = objects.getData("PayNo38");
        PayNo67.ValueText = objects.getData("PayNo67");
        PayNo78.ValueText = objects.getData("PayNo78");

        //顯示發起資料
        EmpNo.ReadOnly = true;
        PartNo.ReadOnly = true;
        ComeDate.ReadOnly = true;
        RqDate.ReadOnly = true;
        DtName.ReadOnly = true;
        Gwei.ReadOnly = true;
        Phone.ReadOnly = true;
        if (actName.ToString() == "部門主管" || actName.ToString() == "人事部")
        {
            LeaveDate.ReadOnly = false;
        }
        else { LeaveDate.ReadOnly = true; }

        if (actName.ToString() == "部門助理")
        {
            note1.ReadOnly = false;
        }
        else { note1.ReadOnly = true; }

        if (actName.ToString() == "財務部")
        {
            note2.ReadOnly = false;
        }
        else { note2.ReadOnly = true; }

        if (actName.ToString() == "資訊部")
        {
            note3.ReadOnly = false;
        }
        else { note3.ReadOnly = true; }

        //if (actName.ToString() == "工衣")
        //{
        //    note9.ReadOnly = false;
        //    PayNo38.ReadOnly = false;
        //}
        //else 
        //{
        //    note9.ReadOnly = true;
        //    PayNo38.ReadOnly = true;
        //}

        if (actName.ToString() == "管理部" || actName.ToString() == "工衣")
        {
            note9.ReadOnly = false;
            PayNo38.ReadOnly = false;
            note10.ReadOnly = false;
            note11.ReadOnly = false;
            PayNo78.ReadOnly = false;
            PayNo67.ReadOnly = false;

        }
        else
        {
            note9.ReadOnly = true;
            PayNo38.ReadOnly = true;
            note10.ReadOnly = true;
            note11.ReadOnly = true;
            PayNo78.ReadOnly = true;
            PayNo67.ReadOnly = true;
        }
        if (actName.ToString() == "人事部")
        {

            note4.ReadOnly = false;
            note5.ReadOnly = false;
            note6.ReadOnly = false;
            note7.ReadOnly = false;
            note8.ReadOnly = false;
            PayNo29.ReadOnly = false;
            PayNo34.ReadOnly = false;
            PayNo35.ReadOnly = false;

        }
        else
        {
            note4.ReadOnly = true;
            note5.ReadOnly = true;
            note6.ReadOnly = true;
            note7.ReadOnly = true;
            note8.ReadOnly = true;
            PayNo29.ReadOnly = true;
            PayNo34.ReadOnly = true;
            PayNo35.ReadOnly = true;
        }
        if (actName.ToString() == "培訓協議")
        {
            sgz.ReadOnly = true;
            note12.ReadOnly = true;
            PayNo29.ReadOnly = false;
            note7.ReadOnly = false;

        }
        else
        {
            note4.ReadOnly = true;
            note5.ReadOnly = true;
            note6.ReadOnly = true;
            note8.ReadOnly = true;
            PayNo34.ReadOnly = true;
            PayNo35.ReadOnly = true;
        }

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
            string p = string.Empty;
            for (int i = 1; i <= MaxDSC; i++)
            {
                DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
                if (dsc != null && dsc.Checked)
                {
                    p += "Y";
                }
                else
                {
                    p += "N";
                }
            }

            Hashtable H1 = base.getHRcode(engine, EmpNo.ValueText);


            string code = Convert.ToString(Convert.ToInt32(H1["HRCODE"]) + 1);
            code = code.PadLeft(5, '0');
            objects.setData("ID", "SMPHR" + code);
            objects.setData("Privilege", p);
            objects.setData("GUID", IDProcessor.getID(""));
            //objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("ComeDate", ComeDate.ValueText);
            objects.setData("RqDate", RqDate.ValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("DtName", DtName.ValueText);
            objects.setData("LeaveDate", LeaveDate.ValueText);
            objects.setData("Gwei", Gwei.ValueText);
            objects.setData("GwType", GwType.ValueText);
            objects.setData("GwNo", GwNo.ValueText);
            objects.setData("Phone", Phone.ValueText);
            objects.setData("line", line.ValueText);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");




            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

        }
        else
        {
            string p = string.Empty;
            for (int i = 1; i <= MaxDSC; i++)
            {
                DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
                if (dsc != null && dsc.Checked)
                {
                    p += "Y";
                }
                else
                {
                    p += "N";
                }
            }
            objects.setData("note1", note1.ValueText);
            objects.setData("note2", note2.ValueText);
            objects.setData("note3", note3.ValueText);
            objects.setData("note4", note4.ValueText);
            objects.setData("note5", note5.ValueText);
            objects.setData("note6", note6.ValueText);
            objects.setData("note7", note7.ValueText);
            objects.setData("note8", note8.ValueText);
            objects.setData("note9", note9.ValueText);
            objects.setData("note10", note10.ValueText);
            objects.setData("note11", note11.ValueText);
            objects.setData("note12", note11.ValueText);
            objects.setData("sgz", sgz.ValueText);
            objects.setData("PayNo35", PayNo35.ValueText);
            objects.setData("PayNo34", PayNo34.ValueText);
            objects.setData("PayNo29", PayNo29.ValueText);
            objects.setData("PayNo38", PayNo38.ValueText);
            objects.setData("PayNo67", PayNo67.ValueText);
            objects.setData("PayNo78", PayNo78.ValueText);
            objects.setData("LeaveDate", LeaveDate.ValueText);
            objects.setData("line", line.ValueText);
            objects.setData("Privilege", p);
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
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string actName = (string)getSession("ACTName");
        bool result = true;

        //新增判斷資料
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 離職申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }

        if (actName.ToString() == "部門助理" && LeaveDate.ValueText.ToString() != DateTime.Now.ToString("yyyy/MM/dd"))
        {
            MessageBox("請在申請離職日期當天簽核");
            result = false;
        }
        if (actName.ToString() == "工衣" && LeaveDate.ValueText.ToString() != DateTime.Now.ToString("yyyy/MM/dd"))
        {
            MessageBox("請在申請離職日期當天簽核");
            result = false;
        }
        //if (EmpNo.ValueText.ToString()=="Q1101039")
        //{
        //    MessageBox("不能理智");
        //    result = false;
        //}
        //Regex reg = new Regex("^[0-9]+$");
        //Match ma = reg.Match(PayNo29.ValueText.Trim().ToString());
        //if (!ma.Success && !string.IsNullOrEmpty(this.PayNo29.ValueText))
        //{
        //    MessageBox("培訓費用不是數字");
        //    result = false; 
        //}  

        //if (base.attachFile.dataSource.getAvailableDataObjectCount() < 1)
        //{
        //    MessageBox("請確認是否上傳附件資料");
        //    result = false;
        //}

        double kp = 0;
        double px = 0;
        double cp = 0;
        double wp = 0;
        double ts = 0;
        double zs = 0;

        try
        {
            if (!string.IsNullOrEmpty(PayNo35.ValueText))
            {
                kp = Convert.ToDouble(PayNo35.ValueText);
            }
            if (!string.IsNullOrEmpty(PayNo29.ValueText))
            {
                px = Convert.ToDouble(PayNo29.ValueText);
            }
            if (!string.IsNullOrEmpty(PayNo34.ValueText))
            {
                cp = Convert.ToDouble(PayNo34.ValueText);
            }
            if (!string.IsNullOrEmpty(PayNo38.ValueText))
            {
                wp = Convert.ToDouble(PayNo38.ValueText);
            }
            if (!string.IsNullOrEmpty(PayNo67.ValueText))
            {
                ts = Convert.ToDouble(PayNo67.ValueText);
            }
            if (!string.IsNullOrEmpty(PayNo78.ValueText))
            {
                zs = Convert.ToDouble(PayNo78.ValueText);
            }
        }
        catch (Exception)
        {
            MessageBox("費用欄位不是一個數字");
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
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string xml = "";
        string ismfg = "";
        string isqh = "";
        string issy = "";
        string isrs = "1";
        string bmzl = "";
        string bmzg = "";
        string issg = "";
        string ispx = "";
        string ishaw = "";
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);
        string[][] groupUsers = null;
        Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);


        if (h1["PartNo"].ToString().Substring(0, 7) == "NQM0101" || h1["PartNo"].ToString().Substring(0, 7) == "NQM0504")
        {
            groupUsers = base.getGroupdUser(engine, "LHR06");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else if (h1["PartNo"].ToString().Substring(0, 7) == "NQM0505")
        {
            groupUsers = base.getGroupdUser(engine, "LHR08");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else if (h1["PartNo"].ToString().Substring(0, 7) == "NQM0506")
        {
            groupUsers = base.getGroupdUser(engine, "LHR07");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else if (h1["PartNo"].ToString().Substring(0, 7) == "NQM0502")
        {
            groupUsers = base.getGroupdUser(engine, "LHR09");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else if (h1["PartNo"].ToString().Substring(0, 7) == "NQM0301")
        {
            groupUsers = base.getGroupdUser(engine, "LHR10");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else if (h1["PartNo"].ToString() == "GQM0201-WH")
        {
            groupUsers = base.getGroupdUser(engine, "LHR11");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else if (h1["PartNo"].ToString().Substring(0, 7) == "PIM0107")
        {
            groupUsers = base.getGroupdUser(engine, "SPYLZ005");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else if (h1["PartNo"].ToString().Substring(0, 7) == "PIM0108")
        {
            groupUsers = base.getGroupdUser(engine, "SPYLZ006");
            for (int i = 0; i < groupUsers.Length; i++)
            {
                bmzg += groupUsers[i][0] + ";";
            }
        }
        else
        {
            bmzg = values[1];
        }


        if (h1["DtTypeName"].ToString() == "A類")
        {
            issy = "1";
            ismfg = "1";
        }
        //string time =System.DateTime.Now.AddDays(-365).ToString();
        //string time2= h1["ComeDate"].ToString();

        if (h1["PerField34"].ToString() == "是")
        {
            issg = "1";
        }

        if (h1["EmpTypeName"].ToString() != "不限36小時")
        {
            isqh = "1";
        }
        else
        {
            isqh = "3";
        }
        if (DSCCheckBox35.Checked == true)
        {
            ismfg = "1";
        }

        string sql = @"select QtName,QTTypeName,QuartersNo,PerField6 from [10.3.11.96].MIS_DB.dbo.PerEmployeeMD where EmpNo='" + EmpNo.ValueText + "' AND Status='Y'";
        DataTable tTb = engine.getDataSet(sql, "data").Tables["data"];
        string gtype=tTb.Rows[0]["QTTypeName"].ToString().Trim();
        string line=tTb.Rows[0]["PerField6"].ToString().Trim();

        if (gtype != "一般崗位")
        {
            ishaw = "1";
        }
        else if (!string.IsNullOrEmpty(line) && line.Length>9
 && line.Substring(0, 10) != "M01010E-NC" )
        {
            if ((line.Substring(0, 8) == "M01010E-B" && Convert.ToInt32(line.Substring(9, 2)) > 21) || (line.Substring(0, 8) == "M01010E-N" && Convert.ToInt32(line.Substring(9, 2)) > 21) || line.Contains("PD2") || line.Contains("PD3"))
            {
                ishaw = "1";
            }
        }
        else
        {
            ishaw = "";
        }

        string sql2 = @"select * from (select COUNT(*) ct from [10.3.11.92\SQL2008].SCQHRDB.dbo.CU004401Data a,[10.3.11.92\SQL2008].SCQHRDB.dbo.PerEmployee b where a.EmpID=b.EmpID and b.EmpNo='" + EmpNo.ValueText + "') aa ";
        DataTable tTb2 = engine.getDataSet(sql2, "data").Tables["data"];
        int count = Convert.ToInt32(tTb2.Rows[0]["ct"].ToString().Trim());

        if (count > 0)
        {
            ispx = "1";
        }




        xml += "<EH0112>";
        xml += "<bmzg DataType=\"java.lang.String\">" + bmzg + "</bmzg>";//部門主管
        xml += "<bmzl DataType=\"java.lang.String\">" + bmzl + "</bmzl>";//部門助理
        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";//總經理是否簽核
        xml += "<issy DataType=\"java.lang.String\">" + issy + "</issy>";//財務資訊是否簽核
        xml += "<isrs DataType=\"java.lang.String\">" + isrs + "</isrs>";//人事是否簽核
        xml += "<ismfg DataType=\"java.lang.String\">" + ismfg + "</ismfg>";//是否含有資訊系統賬號
        xml += "<issg DataType=\"java.lang.String\">" + issg + "</issg>";//上崗證
        xml += "<ishaw DataType=\"java.lang.String\">" + ishaw + "</ishaw>";//環安衛
        xml += "<ispx DataType=\"java.lang.String\">" + ispx + "</ispx>";//培訓協議
        xml += "</EH0112>";

        //writeLog("xml: " + xml);

        param["EH0112"] = xml;
        return "EH0112";
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
    protected void JEmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        try
        {

            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);




            Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
            PartNo.ValueText = h1["PartNo"].ToString();
            ComeDate.ValueText = h1["ComeDate"].ToString().Replace("上午 12:00:00", "");
            DtName.ValueText = h1["DtName"].ToString();
            Phone.ValueText = h1["Mobile"].ToString();
            
            RqDate.ValueText = System.DateTime.Now.ToString("yyyy/MM/dd");
            line.ValueText = h1["PerField6"].ToString();

            string time3 = System.DateTime.Now.ToString("yyyy/MM/dd");
            string time4 = h1["TryuseDate1"].ToString().Replace("上午 12:00:00", "");

            if (Convert.ToDateTime(time3) > Convert.ToDateTime(time4))
            {
                LeaveDate.ValueText = System.DateTime.Now.AddDays(30).ToString();
            }
            else
            {
                LeaveDate.ValueText = System.DateTime.Now.AddDays(3).ToString();
            }

            string sql = @"select QtName,QTTypeName,QuartersNo,PerField6 from [10.3.11.96].MIS_DB.dbo.PerEmployeeMD where EmpNo='" + EmpNo.ValueText + "' AND Status='Y' ";
            DataTable tTb = engine.getDataSet(sql, "data").Tables["data"];
            GwType.ValueText = tTb.Rows[0]["QTTypeName"].ToString().Trim();
            GwNo.ValueText = tTb.Rows[0]["QuartersNo"].ToString().Trim();
            Gwei.ValueText = tTb.Rows[0]["QtName"].ToString().Trim();





        }
        catch (Exception ex)
        {
            throw new Exception("查詢HRUSERS錯誤, " + ex.Message.ToString());
        }

    }
    //列印單據
    protected void PrintButton_OnClick(object sender, EventArgs e)
    {
        //MessageBox("SheetNo : " + SheetNo.ValueText);

        Session["EH0112_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
        base.showOpenWindow(url, "列印任用單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }
    protected void GlassButton1_Click(object sender, EventArgs e)
    {
        if (EmpNo.ValueText.Equals(""))
        {
            pushErrorMessage("請選擇工號");
        }

        string aa=Convert.ToString(Convert.ToDateTime(LeaveDate.ValueText) - Convert.ToDateTime(ComeDate.ValueText));
        string weizhi = aa.Substring(0,aa.IndexOf("."));

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        try
        {
            if (EmpNo.ValueText != string.Empty)
            {
                // glasse = true;//Key裝載三輥閘數據以及提示信息

                //string mdata = "select EmpNo,UsedDays,RemainDays from [10.3.11.92\\SQL2008].SCQHRDB.dbo.AttHolidayYearDataOfDay where EmpNo ='" + EmpNo.ValueText + "' ";

                string mdata = @"select * from (
SELECT a.EmpNo,UsedDays,RemainDays,PerField5,PerField12,PerField1 FROM (
select EmpNo,convert(nvarchar(10),PerField5,23)PerField5,
case when PerField12='01' then '社招' when PerField12='02' then '學生' else '學生轉社招' end PerField12,
case when PerField1='01' then 'DL' ELSE 'IDL' END PerField1
from [10.3.11.92\SQL2008].SCQHRDB.dbo.PerEmployee) a
left join 
(select EmpNo,UsedDays,RemainDays from [10.3.11.92\SQL2008].SCQHRDB.dbo.AttHolidayYearDataOfDay 
 where YYMMDD=CONVERT(nvarchar(10),GETDATE(),23)) b on a.EmpNo=b.EmpNo) c where EmpNo='" + EmpNo.ValueText + "'";

                DataTable tTb = engine.getDataSet(mdata, "data").Tables["data"];
                string showData = @"
";
                //                for (int i = 0; i < tTb.Rows.Count; i++)
                //                {
                //                    showData = showData + tTb.Rows[i]["EmpNo"].ToString().Trim() + "  -  " + tTb.Rows[i]["UsedDays"].ToString() + "  -  " + tTb.Rows[i]["RemainDays"].ToString() + @"
                //";
                //                }
                showData = showData + tTb.Rows[0]["EmpNo"].ToString().Trim() + "\n" + "已使用年休：  " + tTb.Rows[0]["UsedDays"].ToString().Substring(0, 4) + "\n" + "剩餘年休：      " + tTb.Rows[0]["RemainDays"].ToString().Substring(0, 4) + "\n" + "年資：" + tTb.Rows[0]["PerField5"].ToString() + "\n" + "員工類別：" + tTb.Rows[0]["PerField12"].ToString() + "\n" + "直間接：" + tTb.Rows[0]["PerField1"].ToString() + "\n" + "在職天數：" + weizhi + @"
";

                MessageBox(showData);
            }
        }
        catch { }
        finally { engine.close(); }
    }
}
