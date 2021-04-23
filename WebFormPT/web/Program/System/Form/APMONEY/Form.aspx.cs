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

public partial class Program_System_Form_APMONEY_Form : BasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "APMONEY";
        AgentSchema = "WebServerProject.form.APMONEY.APMONEYAgent";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
        this.EnsureChildControls();
    }
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script language=javascript>");
        sb.Append(" function clickViewAward(){");
        sb.Append("     var url = './APMONEY/ViewRfqAward.aspx?PageUniqueID=" + PageUniqueID + "&GUID=';");
        sb.Append("     parent.window.openWindowGeneral('View RFQ Award', url,'','','',true,true);");
        sb.Append(" }");
        sb.Append("</script>");
        Type ctype = this.GetType();
        ClientScriptManager cm = Page.ClientScript;
        if (!cm.IsStartupScriptRegistered(ctype, "clickViewAwardScript"))
        {
            cm.RegisterStartupScript(ctype, "clickViewAwardScript", sb.ToString());
        }
        GbViewAward.AfterClick = "clickViewAward";
		
		GbViewAward.Display = true;
        setSession(PageUniqueID, "objects", objects);
        
        //改變工具列順序
        base.initUI(engine, objects);
		
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        APMONEY001.clientEngineType = engineType;
        APMONEY001.connectDBString = connectString;
        APMONEY004.clientEngineType = engineType;
        APMONEY004.connectDBString = connectString;

        APMONEY001.ValueText = si.fillerID;
        APMONEY001.doValidate();

        APMONEY004.ValueText = si.submitOrgID;
        APMONEY004.doValidate();

        bool isAddNew = base.isNew();
        DataObjectSet detailSet = null;
        DataObjectSet locationSet = null;
        if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.APMONEY.APDETAIL");
            detailSet.setTableName("APDETAIL");
            detailSet.loadFileSchema();
            objects.setChild("APDETAIL", detailSet);

            locationSet = new DataObjectSet();
            locationSet.isNameLess = true;
            locationSet.setAssemblyName("WebServerProject");
            locationSet.setChildClassString("WebServerProject.form.APMONEY.APLOCATION");
            locationSet.setTableName("APLOCATION");
            locationSet.loadFileSchema();
            objects.setChild("APLOCATION", locationSet);
        }
        else
        {
            detailSet = objects.getChild("APDETAIL");
            locationSet = objects.getChild("APLOCATION");
        }
        DetailList.dataSource = detailSet;
        DetailList.HiddenField = new string[] { "GUID", "APDETAIL001", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DetailList.FormTitle = "申請明細輸入畫面";
        DetailList.InputForm = "Detail.aspx";
        DetailList.updateTable();

        LocationList.dataSource = locationSet;
        LocationList.HiddenField = new string[] { "GUID", "APLOCATION001", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        LocationList.updateTable();

        GlassButtonDownload.ReadOnly = true;
    }
    protected override void showData(AbstractEngine engine, DataObject objects)
    {		
        bool isAddNew = base.isNew();
        APMONEY001.GuidValueText = objects.getData("APMONEY001");
        APMONEY001.doGUIDValidate();

        APMONEY004.ValueText = objects.getData("APMONEY004");
        APMONEY004.doValidate();

        APMONEY007.ValueText = objects.getData("APMONEY007");
        APMONEY006.ValueText = objects.getData("APMONEY006");
		
		HTMLCODE.ValueText = objects.getData("HTMLCODE");
		//內容
        string htmlContent = objects.getData("HTMLCODE");
        if (!htmlContent.Equals(""))
        {
            HtmlContentCode.InnerHtml = htmlContent;
        }
		
		string reqHeaderId = Convert.ToString(414157); //PO_REQUISITION_HEADER_ID
		string privilege = "I0WeDl8vdQE1";
		string title = "PO Requisitions-10140005033";
		ImgButtonAtta.Visible = true;
        string url = "javascript:onViewAttach('REQ_HEADERS'," + reqHeaderId + ",'" + privilege + "','" + title + "');";
        ImgButtonAtta.PostBackUrl = url;
		hlSPPOA006.NavigateUrl = "javascript:onViewPr(128678)";		
		
        REMARK1.ValueText = objects.getData("REMARK1");
		REMARK2.ValueText = objects.getData("REMARK2");
		
		if (!objects.getData("REMARK2").Equals("")){
			
			//DSCTabPage1.Enabled = false;
			DSCTabPage1.Hidden = true;
		}

        DataObjectSet detailSet = null;
        DataObjectSet locationSet = null;
        detailSet = objects.getChild("APDETAIL");
        locationSet = objects.getChild("APLOCATION");

	
        DetailList.dataSource = detailSet;
        DetailList.updateTable();
        
        for (int i = 0; i < locationSet.getAvailableDataObjectCount(); i++)
        {
            DataObject dataObject = locationSet.getAvailableDataObject(i);
            string location = "{[a href=\"Form.aspx\"]}" + dataObject.getData("APLOCATION002") + "{[/a]}";
            dataObject.setData("APLOCATION002", location);
        }
        
        LocationList.dataSource = locationSet;
        LocationList.updateTable();
    }
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("APMONEY001", APMONEY001.GuidValueText);
        objects.setData("APMONEY002", APMONEY001.ValueText);
        objects.setData("APMONEY003", APMONEY001.ReadOnlyValueText);
        objects.setData("APMONEY004", APMONEY004.ValueText);
        objects.setData("APMONEY005", APMONEY004.ReadOnlyValueText);
        objects.setData("APMONEY006", APMONEY006.ValueText);
        objects.setData("APMONEY007", APMONEY007.ValueText);
		objects.setData("HTMLCODE", HTMLCODE.ValueText);
		objects.setData("REMARK1", REMARK1.ValueText);
		objects.setData("REMARK2", REMARK2.ValueText);

        for (int i = 0; i < DetailList.dataSource.getAvailableDataObjectCount(); i++)
        {
            DetailList.dataSource.getAvailableDataObject(i).setData("APDETAIL001", objects.getData("GUID"));
        }
        for (int i = 0; i < LocationList.dataSource.getAvailableDataObjectCount(); i++)
        {
            LocationList.dataSource.getAvailableDataObject(i).setData("APLOCATION001", objects.getData("GUID"));
        }
    }
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result=true;

        if (isNecessary(APMONEY001))
        {
            if (APMONEY001.GuidValueText.Equals(""))
            {
                pushErrorMessage("必須填寫申請者");
                result = false;
            }
        }
        if (isNecessary(APMONEY007))
        {
            if (APMONEY007.ValueText.Equals("0"))
            {
                pushErrorMessage("申請金額必須大於0");
                result = false;
            }
        }
        if (isNecessary(APMONEY006))
        {
            if (APMONEY006.ValueText.Equals(""))
            {
                pushErrorMessage("必須填寫說明");
                result = false;
            }
        }


        return result;
    }
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = APMONEY001.ValueText;
        si.fillerName = APMONEY001.ReadOnlyValueText;
        si.fillerOrgID = APMONEY004.ValueText;
        si.fillerOrgName = APMONEY004.ReadOnlyValueText;
        si.ownerID = APMONEY001.ValueText;
        si.ownerName = APMONEY001.ReadOnlyValueText;
        si.ownerOrgID = APMONEY004.ValueText;
        si.ownerOrgName = APMONEY004.ReadOnlyValueText;
        si.submitOrgID = APMONEY004.ValueText;
        si.objectGUID = objects.getData("GUID");

        return si;
    }
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    protected void LocationList_ShowRowData(DataObject objects)
    {
        APLOCATION002.ValueText = objects.getData("APLOCATION002");
        GlassButtonDownload.ReadOnly = false;
		if (!objects.getData("APLOCATION002").Equals(""))
        {
            DSCTabPage1.Enabled = true;
			MessageBox("ininin");	
        }
		MessageBox("aaa");
    }

    protected bool LocationList_SaveRowData(DataObject objects, bool isNew)
    {
        if (APLOCATION002.ValueText.Equals(""))
        {
            MessageBox("必須填寫出帳單位");
            return false;
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("APLOCATION001", "TEMP");
            objects.setData("IS_LOCK", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("APLOCATION002", APLOCATION002.ValueText);
        return true;
    }

    protected void DetailList_AddOutline(DataObject objects, bool isNew)
    {
        calculateTotal();

    }

    protected void DetailList_DeleteData()
    {
        calculateTotal();
    }

    private void calculateTotal()
    {
        decimal total = 0;
        try
        {
            for (int i = 0; i < DetailList.dataSource.getAvailableDataObjectCount(); i++)
            {
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("APDETAIL003"));
            }
        }
        catch { };

        APMONEY007.ValueText = total.ToString();
    }
	
    protected void GbViewAward_Click(object sender, EventArgs e)
    {

    }	
}
