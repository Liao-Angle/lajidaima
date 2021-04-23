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

public partial class Program_SCQ_Form_EG0107_Form : SmpBasicFormPage
{
    private string ComeInActor = "入廠確認";
    private string AwayActor = "出廠確認";

    protected override void init()
    {
        ProcessPageID = "EG0107";
        AgentSchema = "WebServerProject.form.EG0107.EG0107Agent";
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
        
        string[,] vids = new string[2, 2] { { "M", "男" }, { "F", "女" } };
        VisitorGender.setListItem(vids);

        string[,] ids = new string[4, 2] { { "ID", "身分證" }, { "DR", "駕駛證" }, { "TW", "台胞證" }, { "OT", "其他" } };
        IDType.setListItem(ids);



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

        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.doValidate();
        EmpNo.Enabled = false;
        Mobile.ValueText = objects.getData("Mobile");
        Extension.ValueText = objects.getData("Extension");
        VisitorName.ValueText = objects.getData("VisitorName");
        VisitorGender.ValueText = objects.getData("VisitorGender");
        VisitorCompany.ValueText = objects.getData("VisitorCompany");
        FollowingCount.ValueText = objects.getData("FollowingCount");
        VisitDateTime.ValueText = objects.getData("VisitDateTime");
        Reason.ValueText = objects.getData("Reason");
        IDType.ValueText = objects.getData("IDType");
        IDNumber.ValueText = objects.getData("IDNumber");
        ComeInDateTime.Text = objects.getData("ComeInDateTime");
        AwayDateTime.Text = objects.getData("AwayDateTime");
        Note.ValueText = objects.getData("Note");
        
        EmpNo.ReadOnly = true;
        Mobile.ReadOnly = true;
        Extension.ReadOnly = true;
        VisitorName.ReadOnly = true;
        VisitorGender.ReadOnly = true;
        VisitorCompany.ReadOnly = true;
        FollowingCount.ReadOnly = true;
        VisitDateTime.ReadOnly = true;
        Reason.ReadOnly = true;
        IDType.ReadOnly = true;
        IDNumber.ReadOnly = true;
        ComeInDateTime.ReadOnly = true;
        AwayDateTime.ReadOnly = true;
        Note.ReadOnly = true;

        if (actName == ComeInActor)
        {
            IDType.ReadOnly = false;
            IDNumber.ReadOnly = false;
            Note.ReadOnly = false;
        }
        else if (actName == AwayActor)
        {
            Note.ReadOnly = false;
        }

        if (actName.ToString()!="申請人"||actName.ToString()!="部門主管")
        {
            EditPanel.Visible = true;
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
        string actName = (string)getSession("ACTName");
        if (base.isNew())
        {
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("Mobile", Mobile.ValueText);
            objects.setData("Extension", Extension.ValueText);
            objects.setData("VisitorName", VisitorName.ValueText);
            objects.setData("VisitorGender", VisitorGender.ValueText);
            objects.setData("VisitorCompany", VisitorCompany.ValueText);
            objects.setData("FollowingCount", FollowingCount.ValueText);
            objects.setData("VisitDateTime", VisitDateTime.ValueText);
            objects.setData("Reason", Reason.ValueText);

            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        if (actName == ComeInActor)
        {
            objects.setData("IDType", IDType.ValueText);
            objects.setData("IDNumber", IDNumber.ValueText);
            objects.setData("ComeInDateTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            objects.setData("Note", Note.ValueText);
        }
        if (actName == AwayActor)
        {
            objects.setData("AwayDateTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            objects.setData("Note", Note.ValueText);
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
        if(base.isNew())
        {
            if (isNecessary(EmpNo))
            {
                if (EmpNo.GuidValueText.Equals(""))
                {
                    pushErrorMessage("必須選擇員工");
                    result = false;
                }
            }
            if (isNecessary(Mobile))
            {
                if (Mobile.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫聯絡電話");
                    result = false;
                }
            }
            if (isNecessary(Extension))
            {
                if (Extension.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫分機號");
                    result = false;
                }
            }
            if (isNecessary(VisitorName))
            {
                if (VisitorName.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫訪客姓名");
                    result = false;
                }
            }
            if (isNecessary(VisitorCompany))
            {
                if (VisitorCompany.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫訪客公司");
                    result = false;
                }
            }
            if (isNecessary(VisitDateTime))
            {
                if (VisitDateTime.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫訪客時間");
                    result = false;
                }
            }
            if (isNecessary(Reason))
            {
                if (Reason.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫來訪事由");
                    result = false;
                }
            }
        }
        else if (actName == ComeInActor)
        {
            if (IDNumber.ValueText.Trim().Equals(""))
            {
                pushErrorMessage("必須填寫訪客證件號碼");
                result = false;
            }
        }
        else if (actName == AwayActor)
        {
            
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
        //bool rc = false;
        
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
        //if (result == "Y")
        //{
        //    //將ForwardHR改為R
        //    currentObject.setData("ForwardHR", "R");
        //    string id = currentObject.getData("SheetNo");
        //    if (!engine.updateData(currentObject))
        //    {
        //        throw new Exception("更新EG0107 ForwardHR資料錯誤. 單號: " + id);
        //    }
        //}
    }

    /// <summary>
    /// 選擇人員
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
        Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
        Mobile.ValueText = h1["Mobile"].ToString();

        Hashtable h2 = base.getADUserData(engine, EmpNo.ValueText);
        Extension.ValueText = h2["telephonenumber"].ToString();
    }
}
