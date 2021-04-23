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
using com.dsc.flow.server;
using DSCWebControl;
using WebServerProject;

public partial class Program_SCQ_Form_EM0110_Form : SmpBasicFormPage
{
    private string Open = "O";
    private string Ongoing = "G";
    private string Pending = "P";
    private string Closed = "C";
    //Dispach,Finished,EditStatus1,EditStatus2,Satisfy1,Satisfy2,Status
    private bool[] DEF_GUI_CONTROL = new bool[7] { false, false, false, false, false, false, false };
    private bool[] ACT2_GUI_CONTROL = new bool[7] { true, false, false, false, false, false, true };
    private bool[] ACT4_GUI_CONTROL = new bool[7] { false, true, true, true, false, false, true };
    private bool[] ACT5_GUI_CONTROL = new bool[7] { false, false, false, true, false, false, true };
    private bool[] ACT6_GUI_CONTROL = new bool[7] { false, true, true, true, true, true, true };
    private double radio = 15;

    protected override void init()
    {
        ProcessPageID = "EM0110";
        AgentSchema = "WebServerProject.form.EM0110.EM0110Agent";
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
        MISEmpNo.clientEngineType = engineType;
        MISEmpNo.connectDBString = connectString;
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
                            }SQLstr =SQLstr +")";
                            MISEmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


                      }
                      else
                      {
                            MISEmpNo.whereClause = "1=2";
                      }

                     MISEmpNo.DoEventWhenNoKeyIn = false;
                }
                catch {
                      if (si.fillerOrgID != "")
                      {
                            MISEmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'";
                      }
                      else
                      {
                            MISEmpNo.whereClause = "1=2";
                      }
                      MISEmpNo.DoEventWhenNoKeyIn = false;
                }
        EmpNo.DoEventWhenNoKeyIn = false;

        DSCRadioButton[] SatisfyValue = new DSCRadioButton[5] { SatisfyValue1, SatisfyValue2, SatisfyValue3, SatisfyValue4, SatisfyValue5 };
        foreach (DSCRadioButton sv in SatisfyValue)
        {
            sv.ReadOnly = false;
        }
        SatisfyText.ReadOnly = false;
        
        string[,] ids = new string[4, 2] { { "AP", "AP" }, { "MES", "MES" }, { "OA", "OA" }, { "弱電", "弱電" } };
        IssueType.setListItem(ids);

        string[,] fids = new string[2, 2] { { Pending, "未完成" }, { Closed, "已完成" } };
        Finished.setListItem(fids);

        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EM0110.EM0110B");
            dos.setTableName("EM0110B");
            dos.loadFileSchema();
            objects.setChild("EM0110B", dos);
            ApplicationList.dataSource = dos;
            ApplicationList.updateTable();
        }

        ApplicationList.clientEngineType = engineType;
        ApplicationList.connectDBString = connectString;
        ApplicationList.HiddenField = new string[] { "GUID", "SheetNo", "UpdateTime" };

        Table_Control(DEF_GUI_CONTROL);
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
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.doValidate();
        EmpNo.Enabled = false;
        if(EmpNo.ValueText != "")
        {
            UpdateData(EmpNo.ValueText);
        }
        MISEmpNo.ValueText = objects.getData("MISEmpNo");
        MISEmpNo.doValidate();
        MISEmpNo.Enabled = false;
        Mobile.ValueText = objects.getData("Mobile");
        IssueType.ValueText = objects.getData("IssueType");
        Issue.ValueText = objects.getData("Issue");
        MISEmpNo.ValueText = objects.getData("MISEmpNo");        
        PendingReason.ValueText = objects.getData("PendingReason");
        Solution.ValueText = objects.getData("Solution");
        if (objects.getData("IssueStatus") == Closed)
        {
            Finished.ValueText = Closed;
        }
        else
        {
            //預設值
            Finished.ValueText = Pending;
        }
        
        int i = 0;
        DSCRadioButton[] SatisfyValue = new DSCRadioButton[5] { SatisfyValue1, SatisfyValue2, SatisfyValue3, SatisfyValue4, SatisfyValue5 };
        int.TryParse(objects.getData("SatisfyValue"), out i);
        if (i > 0)
        {
            SatisfyValue[i-1].Checked = true;
        }
        SatisfyText.ValueText = objects.getData("SatisfyText");

        EmpNo.ReadOnly = true;
        Mobile.ReadOnly = true;
        IssueType.ReadOnly = true;
        Issue.ReadOnly = true;
        MISEmpNo.ReadOnly = true;
        PendingReason.ReadOnly = true;
        Solution.ReadOnly = true;
        Finished.ReadOnly = true;

        Status.Visible = true;
        string ACTID = fetchActivityIDFromWorkItemOID(engine, (string)getSession("WorkItemOID"), (string)getSession("PDID"), (string)Session["UserID"]);
        if (ACTID == "ACT2")
        {
            Table_Control(ACT2_GUI_CONTROL);
            IssueType.ReadOnly = false;
            Issue.ReadOnly = false;
            MISEmpNo.ReadOnly = false;
        }
        else if (ACTID == "ACT4")
        {
            Table_Control(ACT4_GUI_CONTROL);
            Finished.ReadOnly = false;
            PendingReason.ReadOnly = false;
            Solution.ReadOnly = true;
        }
        else if (ACTID == "ACT5")
        {
            Table_Control(ACT5_GUI_CONTROL);
            Solution.ReadOnly = false;
        }
        else if (ACTID == "ACT6")
        {
            Table_Control(ACT6_GUI_CONTROL);
            foreach (DSCRadioButton sv in SatisfyValue)
            {
                sv.ReadOnly = false;
            }
            SatisfyText.ReadOnly = false;
        }

        ApplicationList.dataSource = objects.getChild("EM0110B");
        ApplicationList.updateTable();
    }

    private void Table_Control(bool[] b)
    {
        if (b.Length == 7)
        {
            Dispach.Visible = b[0];
            Finished.Visible = b[1];
            EditStatus1.Visible = b[2];
            EditStatus2.Visible = b[3];
            Satisfy1.Visible = b[4];
            Satisfy2.Visible = b[5];
            Status.Visible = b[6];
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
        DataObject obj = new DataObject();
        DSCRadioButton[] SatisfyValue = new DSCRadioButton[5] { SatisfyValue1, SatisfyValue2, SatisfyValue3, SatisfyValue4, SatisfyValue5 };

        obj.loadFileSchema(ApplicationList.dataSource.getChildClassString());
        if (base.isNew())
        {
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("Mobile", Mobile.ValueText);
            objects.setData("MISEmpNo", si.fillerID);
            objects.setData("MISEmpName", si.fillerName);
            objects.setData("IssueType", IssueType.ValueText);
            objects.setData("Issue", Issue.ValueText);
            objects.setData("IssueStatus", Open);    //Open
            objects.setData("SatisfyCheck", "0");   //不抽樣

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            obj.setData("EmpNo", EmpNo.ValueText);
            obj.setData("EmpName", EmpNo.ReadOnlyValueText);
            obj.setData("Context", Issue.ValueText);
        }
        else
        {
            string ACTID = fetchActivityIDFromWorkItemOID(engine, (string)getSession("WorkItemOID"), (string)getSession("PDID"), (string)Session["UserID"]);
            if (ACTID == "ACT2")
            {
                objects.setData("MISEmpNo", MISEmpNo.ValueText);
                objects.setData("MISEmpName", MISEmpNo.ReadOnlyValueText);
                objects.setData("IssueType", IssueType.ValueText);
                objects.setData("Issue", Issue.ValueText);
                objects.setData("IssueStatus", Ongoing);    //Ongoing
                //寫入指派誰
                obj.setData("EmpNo", MISEmpNo.ValueText);
                obj.setData("EmpName", MISEmpNo.ReadOnlyValueText);
                obj.setData("Context", si.fillerName + "指派" + MISEmpNo.ReadOnlyValueText);
            }
            else if (ACTID == "ACT4")
            {
                objects.setData("IssueStatus", Finished.ValueText);
                if (Finished.ValueText == Pending)
                {
                    objects.setData("PendingReason", PendingReason.ValueText);

                    obj.setData("EmpNo", MISEmpNo.ValueText);
                    obj.setData("EmpName", MISEmpNo.ReadOnlyValueText);
                    obj.setData("Context", PendingReason.ValueText);
                }
                else if (Finished.ValueText == Closed)
                {
                    objects.setData("Solution", Solution.ValueText);

                    obj.setData("EmpNo", MISEmpNo.ValueText);
                    obj.setData("EmpName", MISEmpNo.ReadOnlyValueText);
                    obj.setData("Context", Solution.ValueText);
                }
            }
            else if (ACTID == "ACT5")
            {
                objects.setData("Solution", Solution.ValueText);
                objects.setData("IssueStatus", Closed);

                obj.setData("EmpNo", MISEmpNo.ValueText);
                obj.setData("EmpName", MISEmpNo.ReadOnlyValueText);
                obj.setData("Context", Solution.ValueText);
            }
            else if (ACTID == "ACT6")
            {
                for(int i=1; i<=SatisfyValue.Length;i++)
                {
                    if (SatisfyValue[i - 1].Checked)
                    {
                        objects.setData("SatisfyCheck", i.ToString());
                    }
                }
                objects.setData("SatisfyValue", "1");
                objects.setData("SatisfyText", SatisfyText.ValueText);

                obj.setData("EmpNo", MISEmpNo.ValueText);
                obj.setData("EmpName", MISEmpNo.ReadOnlyValueText);
                obj.setData("Context", SatisfyText.ValueText);
            }
        }
        obj.setData("GUID", IDProcessor.getID(""));
        obj.setData("SheetNo", objects.getData("SheetNo"));
        obj.setData("IssueStatus", objects.getData("IssueStatus"));
        obj.setData("UpdateTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        ApplicationList.dataSource.add(obj);
        ApplicationList.dataSource.sort(new string[,] { { "UpdateTime", DataObjectConstants.ASC } });
        ApplicationList.updateTable();
        objects.setChild("EM0110B", ApplicationList.dataSource);
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
        if (!base.isNew())
        {
            string ACTID = fetchActivityIDFromWorkItemOID(engine, (string)getSession("WorkItemOID"), (string)getSession("PDID"), (string)Session["UserID"]);
            if (ACTID == "ACT2")
            {
                if (MISEmpNo.GuidValueText.Equals(""))
                {
                    pushErrorMessage("必須選擇" + LBMISEmpNo.Text);
                    result = false;
                }
            }
            else if (ACTID == "ACT4")
            {
                if (Finished.ValueText == Closed)
                {
                    if (Solution.ValueText.Trim() == "")
                    {
                        pushErrorMessage("必須填寫" + LBSolution.Text);
                        result = false;
                    }
                }
                else if (Finished.ValueText == Pending)
                {
                    if (PendingReason.ValueText.Trim() == "")
                    {
                        pushErrorMessage("必須填寫" + LBPendingReason.Text);
                        result = false;
                    }
                }
            }
            else if (ACTID == "ACT5")
            {
                if (Finished.ValueText == Closed)
                {
                    if (Solution.ValueText.Trim() == "")
                    {
                        pushErrorMessage("必須填寫" + LBSolution.Text);
                        result = false;
                    }
                }
            }
            else if (ACTID == "ACT6")
            {
                if (SatisfyText.ValueText.Trim() == "")
                {
                    pushErrorMessage("必須填寫" + LBSatisfyText.Text);
                    result = false;
                }
            }
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
        string str = "<MIS><EmpNo dataType=\"java.lang.String\">" + currentObject.getData("MISEmpNo") + "</EmpNo></MIS>";

        if (base.isNew())
        {   
            param["MIS"] = str;
            return "MIS";
        }
        else
        {
            string ACTID = fetchActivityIDFromWorkItemOID(engine, (string)getSession("WorkItemOID"), (string)getSession("PDID"), (string)Session["UserID"]);
            if (ACTID.Equals("ACT2"))
            {
                param["MIS"] = str;
                return "MIS";
            }
            else if (ACTID.Equals("ACT4") && currentObject.getData("IssueStatus").Equals(Pending))
            {
                param["MIS"] = str;
                param["check1"] = "Y";
                return "Y";
            }
            else if (currentObject.getData("IssueStatus").Equals(Closed))
            {
                if (DateTime.Now.Millisecond <= radio)
                {
                    param["MIS"] = str;
                    param["check2"] = "Y";
                    return "Y";
                }
            }
        }
        return "";
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
    }

    /// <summary>
    /// 選擇請假人員
    /// </summary>
    /// <param name="values"></param>
    protected void EmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        if (values == null)
            return;
        UpdateData(EmpNo.ValueText);
    }

    private void UpdateData(string id)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h1 = base.getHRUsers(engine, id);
        Mobile.ValueText = h1["Mobile"].ToString();
    }
    protected void Finished_SelectChanged(string value)
    {
        if (Finished.ValueText == Pending)
        {
            PendingReason.ReadOnly = false;
            Solution.ReadOnly = true;
        }
        else if (Finished.ValueText == Closed)
        {
            PendingReason.ReadOnly = true;
            Solution.ReadOnly = false;
        }
    }
}
