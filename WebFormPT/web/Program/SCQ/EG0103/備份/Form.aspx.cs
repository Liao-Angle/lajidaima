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

public partial class Program_SCQ_Form_EG0103_Form : SmpBasicFormPage
{
    protected override void init()
    {
          ProcessPageID = "EG0103";
          AgentSchema = "WebServerProject.form.EG0103.EG0103Agent";
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

        string[,] ids = new string[3, 2] { { "0", "公司派車" }, { "1", "自駕" }, { "2", "叫車" } };
        LeaveTypeID.setListItem(ids);


        string[,] idsg = new string[3, 2] { { "0", "公司一般用車" }, { "1", "公司商務用車" }, { "2", "客戶用車" } };
        Genre.setListItem(idsg);
       
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
        CName.ValueText = objects.getData("CName");
        Mobile.ValueText = objects.getData("Mobile");
        Department.ValueText = objects.getData("Department");
        Departure.ValueText = objects.getData("Departure");
        Destination.ValueText = objects.getData("Destination");
        Peers.ValueText = objects.getData("Peers");
        DepartureTime.ValueText = objects.getData("DepartureTime");
        Reason.ValueText = objects.getData("Reason");
        Remark.ValueText = objects.getData("Remark");
        LeaveTypeID.selectedIndex(Convert.ToInt32(objects.getData("Modus")));
        Genre.selectedIndex(Convert.ToInt32(objects.getData("Genre")));
        if (objects.getData("Hitch")=="Y")
        {
            HitchY.Checked = true;
        }
        else { HitchN.Checked = true; }
        UserName.ValueText = objects.getData("UserName");
        UserMobile.ValueText = objects.getData("UserMobile");
        Plate.ValueText = objects.getData("Plate");
        StartKm.ValueText = objects.getData("StartKm");
        EndKm.ValueText = objects.getData("EndKm");
        goout.ValueText = objects.getData("Goout");
        goback.ValueText = objects.getData("Goback");

        CName.ReadOnly = true;
        Mobile.ReadOnly = true;
        Department.ReadOnly = true;
        Departure.ReadOnly = true;
        Destination.ReadOnly = true;
        Peers.ReadOnly = true;
        DepartureTime.ReadOnly = true;
        Reason.ReadOnly = true;
        HitchN.ReadOnly = true;
        HitchY.ReadOnly = true;

        string actName = (string)getSession("ACTName");
        if (actName.Equals("GA事務"))
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
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo",objects.getData("SheetNo"));
            objects.setData("CName",CName.ValueText);
            objects.setData("Mobile",Mobile.ValueText);
            objects.setData("Department",Department.ValueText);
            objects.setData("Departure",Departure.ValueText);
            objects.setData("Destination",Destination.ValueText);
            objects.setData("Peers",Peers.ValueText);
            objects.setData("DepartureTime",DepartureTime.ValueText);
            objects.setData("Reason",Reason.ValueText);
            objects.setData("Modus", LeaveTypeID.ValueText);
            objects.setData("Genre", Genre.ValueText);
            if (HitchY.Checked)
            {
                objects.setData("Hitch", "Y");
            }
            else { objects.setData("Hitch", "N"); }
            objects.setData("Usermessage", "N");
        }
        objects.setData("Remark", Remark.ValueText);
        objects.setData("UserName", UserName.ValueText);
        objects.setData("UserMobile", UserMobile.ValueText);
        objects.setData("Plate", Plate.ValueText);
        objects.setData("StartKm", StartKm.ValueText);
        objects.setData("EndKm", EndKm.ValueText);
        objects.setData("Goout", goout.ValueText);
        objects.setData("Goback", goback.ValueText);




            //產生單號並儲存至資料物件
            base.saveData(engine, objects);


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
        //新增判斷資料
      

        try{
       Convert.ToInt64(Mobile.ValueText.Trim());
        }catch(FormatException ES)
        {
            pushErrorMessage("手機號填寫有誤!");
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
        string actName = (string)getSession("ACTName");
        bool rc = false;

        if (actName.Equals("部門主管"))
        {
            //if (isManager(engine, RequestID.ValueText))
            //{
            //    param["check1"] = "Y";
            //    rc = true;
            //}
        }

        //Return有值才會變更變數
        if (rc)
            return "Y";
        else
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
			//直接終止流程
                base.terminateThisProcess();
    }


      public void EmployeeID_SingleOpenWindowButtonClick()
      {
 
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
            //開發同意簽核後自動化流程 b
            currentObject.setData("Usermessage", "R");
            string id = currentObject.getData("SheetNo");
            if (!engine.updateData(currentObject))
            {
                throw new Exception("更新EG0103 短信資料錯誤. 單號: " + id);
            }
            //或開發及時發送短信
        }
    }






      /// <summary>
      /// Genre changed
      /// </summary>
      /// <param name="value"></param>
      protected void Genre_SelectChanged(string value)
      {

      }

    
      protected void LeaveTypeID_SelectChanged(string value)
      {
          //if (value == "2")    暫未找到解決方案
          //{
          //    dds.Attributes.Add("display", "none");
          //}
          //else { Response.Write("<script>var odiv = document.getElementById('dds');odiv.style.display = 'block';</script>"); }
      }
}
