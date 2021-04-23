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


public partial class SmpProgram_System_Form_SPAD003_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SmpOvertimeForm";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                UserGUID.clientEngineType = (string)Session["engineType"];
                UserGUID.connectDBString = (string)Session["connectString"];
            }
        }
    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        if (isAddNew)
        {
            string dateTimeNow = DateTimeUtility.getSystemTime2(null);
            StartDateTime.ValueText = dateTimeNow.Substring(0, 10) + " 17:20:00";
            EndDateTime.ValueText = dateTimeNow.Substring(0, 10) + " 20:20:00";
            Hours.ValueText = "3";
        }
        else
        {
            UserGUID.GuidValueText = objects.getData("UserGUID");
            UserGUID.doGUIDValidate();
            StartDateTime.ValueText = objects.getData("StartDateTime");
            EndDateTime.ValueText = objects.getData("EndDateTime");
            Hours.ValueText = objects.getData("Hours");
            Reason.ValueText = objects.getData("Reason");
            Remark.ValueText = objects.getData("Remark");
        }
    }

    protected override void saveData(DataObject objects)
    {               
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("OvertimeFormGUID", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        string strErrMsg = "";
        try
        {
            string hours = Hours.ValueText;
            DateTime restStartDateTime = Convert.ToDateTime(StartDateTime.ValueText.Substring(1, 10) + " 12:00:00");
            DateTime restEndDateTime = Convert.ToDateTime(EndDateTime.ValueText.Substring(1, 10) + " 12:00:00");

            if (!Convert.ToString(StartDateTime.ValueText).Equals(""))
            {
                DateTime startDateTime = Convert.ToDateTime(StartDateTime.ValueText);
                if (startDateTime.CompareTo(restStartDateTime) > 0 && startDateTime.CompareTo(restEndDateTime) < 0)
                {
                    strErrMsg += LblStartDateTime.Text + ": 不可落於休息時間!";
                }
            }

            if (!Convert.ToString(EndDateTime.ValueText).Equals(""))
            {
                DateTime endDateTime = Convert.ToDateTime(EndDateTime.ValueText);
                if (endDateTime.CompareTo(restStartDateTime) > 0 && endDateTime.CompareTo(restEndDateTime) < 0)
                {
                    strErrMsg += LblStartDateTime.Text + ": 不可落於休息時間!";
                }
            }

            if (Convert.ToString(Hours.ValueText).Equals(""))
            {
                strErrMsg += LblHours.Text + ": 未產生加班時數或未達加班計算標準!";
            }
            else
            {
                if (Convert.ToDecimal(hours) <= 0)
                {
                    strErrMsg += LblHours.Text + ": 加班時間選擇錯誤，加班時數為負數!";
                }
            }

            if (!strErrMsg.Equals(""))
            {
                //throw new Exception("");
            }
        }
        catch
        {
            throw new Exception(strErrMsg);
        }

        objects.setData("UserGUID", UserGUID.GuidValueText);
        objects.setData("id", UserGUID.ValueText);
        objects.setData("userName", UserGUID.ReadOnlyValueText);
        objects.setData("StartDateTime", StartDateTime.ValueText);
        objects.setData("EndDateTime", EndDateTime.ValueText);
        objects.setData("Hours", Hours.ValueText);
        objects.setData("Reason", Reason.ValueText);
        objects.setData("Remark", Remark.ValueText);
    }

    protected void DateTimeClick(string values)
    {
        DateTime startDateTime = Convert.ToDateTime(StartDateTime.ValueText);
        DateTime endDateTime = Convert.ToDateTime(EndDateTime.ValueText);
        double hours = (endDateTime - startDateTime).TotalHours;
        double hour = (int)(hours/0.5)*0.5;
        string h = "";
        if (hour > 0)
        {
            h = Convert.ToString(hour);
        }
        Hours.ValueText = h;
    }
}
