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

public partial class Program_SCQ_Form_EG0104_Form : SmpBasicFormPage
{
    //非Admin會議室申請
    private string Admin = "N";

    protected override void init()
    {
        ProcessPageID = "EG0104";
        AgentSchema = "WebServerProject.form.EG0104.EG0104Agent";
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
        EmpNo.DoEventWhenNoKeyIn = false;
         
        string room = sp.getParam("MeetingRoom");
        if (!room.Equals(""))
        {
            string[] strs = room.Split(new char[] { ';' });
            string[,] ids = new string[strs.Length, 2];
            for (int i = 0; i < strs.Length; i++)
            {
                ids[i, 0] = strs[i];
                ids[i, 1] = strs[i];
            }
            RoomNo.setListItem(ids);
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
        if (objects.getData("Admin").ToString() == Admin)
        {
            EmpNo.ValueText = objects.getData("EmpNo");
	    EmpNo.ReadOnlyValueText=objects.getData("EmpName");
            Telephone.ValueText = objects.getData("Telephone");
            Subject.ValueText = objects.getData("Subject");
            Participant.ValueText = objects.getData("Participant");
            RoomNo.ValueText = objects.getData("RoomNo");
            SDateTime.ValueText = objects.getData("SDateTime");
            EDateTime.ValueText = objects.getData("EDateTime");
            if (objects.getData("Projector") == "Y")
            {
                Projector.Checked = true;
            }
            if (objects.getData("Video") == "Y")
            {
                Video.Checked = true;
            }
        }
        else
        {
            MessageBox("本表單序號為Admin表單，請改用Admin開啟。");
        }
        EmpNo.ReadOnly = true;
        Participant.ReadOnly = true;
        RoomNo.ReadOnly = true;
        SDateTime.ReadOnly = true;
        EDateTime.ReadOnly = true;
        Projector.ReadOnly = true;
        Video.ReadOnly = true;
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
            objects.setData("Telephone", Telephone.ValueText);
            objects.setData("Subject", Subject.ValueText);
            objects.setData("Participant", Participant.ValueText);
            objects.setData("RoomNo", RoomNo.ValueText);
            objects.setData("SDateTime", SDateTime.ValueText);
            objects.setData("EDateTime", EDateTime.ValueText);
            if (Projector.Checked)
            {
                objects.setData("Projector", "Y");
            }
            else
            {
                objects.setData("Projector", "N");
            }
            if (Video.Checked)
            {
                objects.setData("Video", "Y");
            }
            else
            {
                objects.setData("Video", "N");
            }
            objects.setData("Admin", Admin);
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
                result = false;
            }
        }
        if (isNecessary(Subject))
        {
            if (Subject.ValueText.Equals(""))
            {
                pushErrorMessage("必須填寫會議主題");
                result = false;
            }
        }
        if (isNecessary(SDateTime))
        {
            if (SDateTime.ValueText.Equals(""))
            {
                pushErrorMessage("必須選擇起始時間");
                result = false;
            }
        }
        if (isNecessary(EDateTime))
        {
            if (EDateTime.ValueText.Equals(""))
            {
                pushErrorMessage("必須選擇終止時間");
                result = false;
            }
        }

        if (Participant.ValueText.TrimStart().TrimEnd().Equals(""))
        {
            pushErrorMessage("最低需求1人參與會議");
            result = false;
        }


        if (!SDateTime.ValueText.Equals("") && !EDateTime.ValueText.Equals(""))
        {
            DateTime dt1,dt2;
            DateTime.TryParse(SDateTime.ValueText, out dt1);
            DateTime.TryParse(EDateTime.ValueText, out dt2);
            if (dt1 < DateTime.Now)
            {
                pushErrorMessage("開會時間已過期");
                result = false;
            }
            if (dt1 >= dt2)
            {
                pushErrorMessage("開會起始時間與終止時間有誤");
                result = false;
            }
            TimeSpan ts1 = dt2.Subtract(DateTime.Now).Duration();

            ts1 = dt2.Subtract(dt1).Duration();

            string strDateStart = dt1.Year.ToString() + dt1.Month.ToString().PadLeft(2, '0') + dt1.Day.ToString().PadLeft(2, '0'), strDateEnd = dt2.Year.ToString() + dt2.Month.ToString().PadLeft(2, '0') + dt2.Day.ToString().PadLeft(2, '0');

            DataTable hst = base.getMeetingroom(engine, RoomNo.ValueText, strDateStart);
            if (hst != null&& hst.Rows.Count>0)
            {
                for (int i = 0; i < hst.Rows.Count - 1; i++)
                {
                    if (Convert.ToDateTime(hst.Rows[i]["SDateTime"]) < dt1 && Convert.ToDateTime(hst.Rows[i]["EDateTime"]) > dt1)
                    {
                        pushErrorMessage("會議安排衝突,請重新調整會議時間");
                        result = false;
                        break;
                    }
                    else if (Convert.ToDateTime(hst.Rows[i]["SDateTime"]) < dt2 && Convert.ToDateTime(hst.Rows[i]["EDateTime"]) > dt2)
                    {
                        pushErrorMessage("會議安排衝突,請重新調整會議時間");
                        result = false;
                        break;
                    }
                    else if (Convert.ToDateTime(hst.Rows[i]["SDateTime"]) > dt1 && Convert.ToDateTime(hst.Rows[i]["EDateTime"]) < dt2)
                    {
                        pushErrorMessage("會議安排衝突,請重新調整會議時間");
                        result = false;
                        break;
                    }
                }
            }

        }


        ///當日

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
        try
        {
            string isqh = "";
            if (RoomNo.ValueText.Equals("B2A"))
            {
                isqh = "1";
            }

            xml += "<EG0104>";
            xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
            xml += "<isqh DataType=\"java.lang.String\">"+isqh+"</isqh>";
            xml += "</EG0104>";
        }
        catch (Exception e)
        {
            writeLog(e);
        }

        param["EG0104"] = xml;
        return "EG0104";
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
        //        throw new Exception("更新EG0104 ForwardHR資料錯誤. 單號: " + id);
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
        
    }
}
