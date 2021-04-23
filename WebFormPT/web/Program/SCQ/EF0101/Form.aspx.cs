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
using System.DirectoryServices;

public partial class Program_SCQ_Form_EF0101_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EF0101";
        AgentSchema = "WebServerProject.form.EF0101.EF0101Agent";
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
        if (isNew())
        {
            Result.ReadOnly = true;
            Executer.ReadOnly = true;
            UseTime.ReadOnly = true;
        }

        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;

        EmpNo.ValueText = si.fillerID;
        EmpNo.doValidate();
        UpdateData(si.fillerID);

	EmpNo.ReadOnly = true;
	Mobile.ReadOnly = true;

            SheetNo.Display = false;
            Subject.Display = false;

        if (isNew())
        {
            string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	
                {"3","許文坤"},};
            sqzszg.setListItem(xgqhzg);
            ///
            if (si.fillerOrgID == "NQM010101")
            {
                showzg.Visible = true;
            }
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

	Subject.ValueText=objects.getData("Subject");
        RequestDate.Text = objects.getData("RequestDate");
        ExpectDate.ValueText = objects.getData("ExpectDate");
        Location.ValueText = objects.getData("Location");
        Explanation.ValueText = objects.getData("Explanation");
        Result.ValueText = objects.getData("Result");
        Executer.ValueText = objects.getData("Executer");
        UseTime.ValueText = objects.getData("UseTime");

	EmpNo.ReadOnly = true;
	Mobile.ReadOnly = true;
        ExpectDate.ReadOnly = true;
        Location.ReadOnly = true;
        Explanation.ReadOnly = true;
        Result.ReadOnly = true;
        Executer.ReadOnly = true;
        UseTime.ReadOnly = true;
            SheetNo.Display = false;
            Subject.Display = false;

        string actName = (string)getSession("ACTName");
        if (actName == "GA助理")
        {
            Result.ReadOnly = false;
            Executer.ReadOnly = false;
            UseTime.ReadOnly = false;
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
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

	    objects.setData("Subject", Subject.ValueText);
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EmpNo", si.fillerID);
            objects.setData("EmpName", si.fillerName);
            objects.setData("RequestDate", DateTime.Now.ToString("yyyy/MM/dd"));
            objects.setData("ExpectDate", ExpectDate.ValueText);
            objects.setData("Location", Location.ValueText);
            objects.setData("Explanation", Explanation.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        else
        {
            if (!Result.ReadOnly)
            {
                objects.setData("Result", Result.ValueText);
            }
            if (!Executer.ReadOnly)
            {
                objects.setData("Executer", Executer.ValueText);
            }
            if (!UseTime.ReadOnly)
            {
                objects.setData("UseTime", UseTime.ValueText);
            }
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
        if (isNecessary(ExpectDate))
        {
            if (ExpectDate.ValueText.Equals(""))
            {
                pushErrorMessage("必須選擇需完成日期");
                //pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("Program_SCQ_Form_EF0101_Form_aspx.language", "message", "QueryError1", "必須選擇請假人"));
                result = false;
            }
        }
        if (isNecessary(Location))
        {
            if (Location.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入地點");
                result = false;
            }
        }
        if (isNecessary(Explanation))
        {
            if (Explanation.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入申請說明");
                result = false;
            }
        }
        if (!Result.ReadOnly && Result.ValueText.Equals(""))
        {
            pushErrorMessage("必須輸入施工結果");
            result = false;
        }
        if (!Executer.ReadOnly && Executer.ValueText.Equals(""))
        {
            pushErrorMessage("必須輸入執行者");
            result = false;
        }
        if (!UseTime.ReadOnly && UseTime.ValueText.Equals(""))
        {
            pushErrorMessage("必須輸入施工時間");
            result = false;
        }



        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
        {
            pushErrorMessage("MFG-1 部門 請選擇簽核主管");
            result = false;
        }



        //設定主旨
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【廠務需求申請單】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
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
        string xml = "";
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            //填表人
            string creatorId = si.fillerID;
            string managerId = "";
            string FACmanager = "Q1805446";//
            string FACWom = "Q1506124";
            string[] values = base.getUserManagerInfoID(engine, si.fillerID);
            if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText != "0")
            {
                if (sqzszg.ValueText == "1")
                {
                    managerId = "Q1100135";
                }
                else if (sqzszg.ValueText == "2")
                {
                    managerId = "Q1608418";
                }
                else if (sqzszg.ValueText == "3")
                {
                    managerId = "Q1210122";
                }
		else
                {
                managerId = values[1];
                }
            }
            else
            {
                managerId = values[1];  //申請人的主管 工號
            }

            xml += "<EF0101>";
            xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
            xml += "<Manager DataType=\"java.lang.String\">" + managerId + "</Manager>";
            xml += "<FACmanager DataType=\"java.lang.String\">" + FACmanager + "</FACmanager>";
            xml += "<FACWom DataType=\"java.lang.String\">" + FACWom + "</FACWom>";
            xml += "</EF0101>";

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
        }
        //表單代號
        param["EF0101"] = xml;
        return "EF0101";
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
        //        throw new Exception("更新EF0101 ForwardHR資料錯誤. 單號: " + id);
        //    }
        //}
    }

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

        Hashtable h1 = base.getADUserData(engine, id);
        Mobile.ValueText = h1["telephonenumber"].ToString();
        engine.close();
    }
}
