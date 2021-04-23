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

public partial class Program_SCQ_Form_EM0105_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EM0105";
        AgentSchema = "WebServerProject.form.EM0105.EM0105Agent";
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
        string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + Session["UserID"].ToString() + "'";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        string SQLstr = "";
        try
        {
            if (dtbm.Rows[0][0].ToString() != "")
            {
                SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtbm.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                }
                SQLstr = SQLstr + ")";
                EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


            }
            else
            {
                SQLstr = "1=2";
                EmpNo.whereClause = SQLstr;
            }

            EmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                SQLstr = "PartNo like '%" + si.fillerOrgID + "%'";
                EmpNo.whereClause = SQLstr;
            }
            else
            {
                SQLstr = "1=2";
                EmpNo.whereClause = SQLstr;
            }
            EmpNo.DoEventWhenNoKeyIn = false;
        }

        EmpNo.DoEventWhenNoKeyIn = false;

        string[,] kh = new string[3, 2] { { "請選擇", "請選擇" }, { "客戶", "客戶" }, { "員工", "員工" } };
        onlietype.setListItem(kh);

        string[,] aids = new string[4, 2] { { "請選擇", "請選擇" }, { "手機", "手機" }, { "筆電", "筆電" }, { "桌機", "桌機" } };
        Intranet.setListItem(aids);

       
        //PartNo.ReadOnly = true;

        //if (isNew() || Session["UserID"].ToString() == si.fillerID)
        //{
        //    string[,] xgqhzg = new string[,]{ {"0","請選擇"},
        //        {"1","張華謙"},
        //        {"2","田軍祥"},	
        //        {"3","許文坤"},};
        //    sqzszg.setListItem(xgqhzg);
        //    ///
        //    if (si.fillerOrgID == "NQM010101")
        //    {
        //        showzg.Visible = true;
        //    }
        //}

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
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        EmpNo.Enabled = false;
		
		//Hashtable ADuser = (Hashtable)base.getADUserData(engine, EmpNo.ValueText);
		
        if (EmpNo.ValueText != "")
        {
            Hashtable h = base.getHRUsers(engine, EmpNo.ValueText);
            PartNo.ValueText = h["PartNo"].ToString();
            DtName.ValueText = h["DtName"].ToString();
			
			Hashtable ADuser = (Hashtable)base.getADUserData(engine, EmpNo.ValueText);
			string[] mail = ADuser["mail"].ToString().Split(new char[] { '_', '@' });
			EName.ValueText = mail[0] + "_" + mail[1];
        }
		else
		{
			EName.ValueText="";
		}
		
		//if(ADuser["mail"]=="")
		//{
		//	EName.ValueText="";
		//}
		//else
		//{
		//	string[] mail = ADuser["mail"].ToString().Split(new char[] { '_', '@' });
		//	EName.ValueText = mail[0] + "_" + mail[1];
		//}
		
        
		
		

        Subject.ValueText = objects.getData("Subject");
        onlietype.ValueText = objects.getData("onlietype");
        customer.ValueText = objects.getData("customer");
        SingleDateTimeField1.ValueText = objects.getData("SingleDateTimeField1");
        SingleDateTimeField2.ValueText = objects.getData("SingleDateTimeField2");
        MAC.ValueText = objects.getData("MAC");
        Intranet.ValueText = objects.getData("Intranet");
        AreaData.ValueText = objects.getData("Area");
        Reason.ValueText = objects.getData("Reason");

        EName.ReadOnly = true;
        EmpNo.ReadOnly = true;
        PartNo.ReadOnly = true;
        DtName.ReadOnly = true;
        onlietype.ReadOnly = true;
        MAC.ReadOnly = true;
        Intranet.ReadOnly = true;
        Reason.ReadOnly = true;
        customer.ReadOnly = true;
        AreaData.ReadOnly = true;
        Reason.ReadOnly = true;
        SingleDateTimeField1.ReadOnly = true;
        SingleDateTimeField2.ReadOnly = true;
    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        if (base.isNew())
        {
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("onlietype", onlietype.ValueText);
            objects.setData("customer", customer.ValueText);
            objects.setData("SingleDateTimeField1", SingleDateTimeField1.ValueText);
            objects.setData("SingleDateTimeField2", SingleDateTimeField2.ValueText);
            objects.setData("MAC", MAC.ValueText);
            objects.setData("Intranet", Intranet.ValueText);
            objects.setData("Area", AreaData.ValueText);
            objects.setData("Reason", Reason.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
    }

    public bool getdtnamebool(string str)
    {
        if (str == "副理" || str == "經理" || str == "處長" || str == "協理" || str == "總經理" || str == "品質長" || str == "專案經理")
        {
            return true;
        }
        else
        { return false; }
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

        if (Intranet.ValueText.Equals("請選擇"))
        {
            pushErrorMessage("必須選擇上網方式");
            result = false;
        }    

        if ((onlietype.ValueText == "員工" && Intranet.ValueText == "手機") && !getdtnamebool(DtName.ValueText))
        {
            pushErrorMessage("需經副理級別以上方可申請手機上網！");
            result = false;
        }
        if (onlietype.ValueText == "員工" && Intranet.ValueText == "手機"&&MAC.ValueText.Equals(""))
        {
            pushErrorMessage("必須填寫手機MAC地址");
            result = false;
        }  

        //SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //if (Session["UserID"].ToString() == si.fillerID && si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
        //{
        //    pushErrorMessage("MFG-1 部門 請選擇簽核主管");
        //    result = false;
        //}

        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 Inter申請單 】";
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
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string managerId = "";
        string isqh = "";
        string[] values = base.getUserManagerInfoID(engine, si.fillerID);

        managerId = values[1];  //申請人的主管 工號

        if (onlietype.ValueText == "員工" && Intranet.ValueText == "手機")
        {
            isqh = "2";
        }
        else
        {
            isqh = "3";
        }

        string xml = "";
        xml += "<EM0105>";
        xml += "<CREATOR DataType=\"java.lang.String\">" + si.fillerID + "</CREATOR>";
        xml += "<ManagerID DataType=\"java.lang.String\">" + managerId + "</ManagerID>";
        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";
        xml += "</EM0105>";
        param["EM0105"] = xml;

        return "EM0105";
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
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h = base.getHRUsers(engine, EmpNo.ValueText);
        PartNo.ValueText = h["PartNo"].ToString();
        DtName.ValueText = h["DtName"].ToString();
        if (EmpNo.ValueText != "Q1708226")
        {
            Hashtable ADuser = (Hashtable)base.getADUserData(engine, EmpNo.ValueText);
            string[] mail = ADuser["mail"].ToString().Split(new char[] { '_', '@' });
            EName.ValueText = mail[0] + "_" + mail[1];

        }
        else
        {
            EName.ValueText = "";
        }
        
    }
    protected void onlietype_SelectChanged(string value)
    {
        if (onlietype.ValueText == "客戶")
        {
            EmpNo.ReadOnly = true;
            PartNo.ReadOnly = true;
            DtName.ReadOnly = true;
            EName.ReadOnly = true;
            customer.ReadOnly = false;
            MAC.ReadOnly = true;
            SingleDateTimeField1.ReadOnly = false;
            SingleDateTimeField2.ReadOnly = false;

        }
        else if (onlietype.ValueText == "員工"&&Intranet.ValueText=="手機")
        {
            MAC.ReadOnly = false;
        }
        else 
        {
            EmpNo.ReadOnly = false;
            PartNo.ReadOnly = false;
            DtName.ReadOnly = false;
            EName.ReadOnly = false;
            customer.ReadOnly = true;
            MAC.ReadOnly = true;
            SingleDateTimeField1.ReadOnly = true;
            SingleDateTimeField2.ReadOnly = true;
        }
    }

}
