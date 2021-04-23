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

public partial class Program_SCQ_Form_EH010403_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH010403";
        AgentSchema = "WebServerProject.form.EH010403.EH010403Agent";
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
                            EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


                      }
                      else
                      {
                            EmpNo.whereClause = "1=2";
                      }

                      EmpNo.DoEventWhenNoKeyIn = false;
                }
                catch {
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
        string leave = sp.getParam("AwardType");
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
            RewardID.setListItem(ids);
        }

        string[,] tids = new string[2, 2] { { "1", "ㄧ次" }, { "2", "二次" } };
        Quantity.setListItem(tids);
        
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
        if (EmpNo.ValueText!="")
        {
            Hashtable h = base.getHRUsers(engine, EmpNo.ValueText);
            TryUseDate.Text = DateTime.Parse(h["TryuseDate"].ToString()).ToString("yyyy/MM/dd");
            DtName.Text = h["DtName"].ToString();
        }
        RewardID.ValueText = objects.getData("RewardID");
        Quantity.ValueText = objects.getData("Quantity");
        Reason.ValueText = objects.getData("Reason");

        EmpNo.ReadOnly = true;
        Reason.ReadOnly = true;
        RewardID.ReadOnly = true;
        Quantity.ReadOnly = true;
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
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("RewardID", RewardID.ValueText);
            objects.setData("RewardName", RewardID.ReadOnlyText);
            objects.setData("Quantity", Quantity.ValueText);
            objects.setData("Reason", Reason.ValueText);
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
        if (isNecessary(EmpNo))
        {
            if (EmpNo.GuidValueText.Equals(""))
            {
                pushErrorMessage("必須選擇員工");
                //pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("Program_SCQ_Form_EH010403_Form_aspx.language", "message", "QueryError1", "必須選擇請假人"));
                result = false;
            }
        }
        if (isNecessary(Reason))
        {
            if (Reason.ValueText.Equals(""))
            {
                pushErrorMessage("必須填寫獎勵事由");
                //pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("Program_SCQ_Form_EH010403_Form_aspx.language", "message", "QueryError4", "必須填寫請假事由"));
                result = false;
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
        //string actName = (string)getSession("ACTName");
        //bool rc = false;

        //if (actName.Equals("部門主管"))
        //{
        //    double hours = 0;
        //    double.TryParse(currentObject.getData("Hours"), out hours);
        //    if (hours >= 40.0)
        //    {
        //        param["check1"] = "Y";
        //        rc = true;
        //    }
        //}
        //if (rc)
        //    return "Y";
        //else
        //    return "";
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
        if (result == "Y")
        {
            //將ForwardHR改為R
            currentObject.setData("ForwardHR", "R");
            string id = currentObject.getData("SheetNo");
            if (!engine.updateData(currentObject))
            {
                throw new Exception("更新EH010403 ForwardHR資料錯誤. 單號: " + id);
            }
        }
    }

    /// <summary>
    /// 選擇請假人員
    /// </summary>
    /// <param name="values"></param>
    protected void RequestID_SingleOpenWindowButtonClick(string[,] values)
    {
        if (values == null)
            return;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        Hashtable h = base.getHRUsers(engine, EmpNo.ValueText);
        TryUseDate.Text = DateTime.Parse(h["TryuseDate"].ToString()).ToString("yyyy/MM/dd");
        DtName.Text = h["DtName"].ToString();
    }
}
