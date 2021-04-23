using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
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

public partial class SmpProgram_Form_SPERP005_Form : SmpErpFormPage
{

    /// <summary>
    /// 初始化參數。
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPERP005";
        AgentSchema = "WebServerProject.form.SPERP005.SmpInvMmtFormAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPERP";
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

        CreatorGUID.clientEngineType = engineType;
        CreatorGUID.connectDBString = connectString;
        OriginatorGUID.clientEngineType = engineType;
        OriginatorGUID.connectDBString = connectString;

        //審核人1
        Checkby1GUID.clientEngineType = engineType;
        Checkby1GUID.connectDBString = connectString;
        //審核人2
        Checkby2GUID.clientEngineType = engineType;
        Checkby2GUID.connectDBString = connectString;
        //RD
        RdMemberGUID.clientEngineType = engineType;
        RdMemberGUID.connectDBString = connectString;
        //AME/ME
        MeMemberGUID.clientEngineType = engineType;
        MeMemberGUID.connectDBString = connectString;
        //PM    
        PmMemberGUID.clientEngineType = engineType;
        PmMemberGUID.connectDBString = connectString;
        //業務
        SaleMemberGUID.clientEngineType = engineType;
        SaleMemberGUID.connectDBString = connectString;
        //業務審核人
        SaleManagerGUID.clientEngineType = engineType;
        SaleManagerGUID.connectDBString = connectString;
        //物管
        McMemberGUID.clientEngineType = engineType;
        McMemberGUID.connectDBString = connectString;
        //品管
        QaMemberGUID.clientEngineType = engineType;
        QaMemberGUID.connectDBString = connectString;
		//SEQ
        SqeMemberGUID.clientEngineType = engineType;
        SqeMemberGUID.connectDBString = connectString;
		//採購
        PurMemberGUID.clientEngineType = engineType;
        PurMemberGUID.connectDBString = connectString;

        bool isAddNew = base.isNew();
        DataObjectSet detailSet = null;
        if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.SPERP005.SmpInvMmtDetail");
            detailSet.setTableName("SmpInvMmtDetail");
            detailSet.loadFileSchema();
            objects.setChild("SmpInvMmtDetail", detailSet);
        }
        else
        {
            detailSet = objects.getChild("SmpInvMmtDetail");
        }
        DataListLine.dataSource = detailSet;
        DataListLine.HiddenField = new string[] { "GUID", "HeaderGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        DataListLine.NoAdd = true;
        DataListLine.NoDelete = true;
        DataListLine.NoModify = true;
        DataListLine.updateTable();

        //唯讀欄位
        Subject.ReadOnly = true;
        OrgName.ReadOnly = true;
        InvMmtNum.ReadOnly = true;
        TrxTypeName.ReadOnly = true;
        Version.ReadOnly = true;
        RequestorName.ReadOnly = true;
        RequestorDept.ReadOnly = true;
        Comments.ReadOnly = true;
        LineQuantity.ReadOnly = true;
        SumQuantity.ReadOnly = true;
		CompanyCode.ReadOnly = true;

        //不顯示欄位
        CreatorGUID.Display = false;     //送簽者
        OriginatorGUID.Display = false;  //需求者
        OrgId.Display = false;           //判斷流程的org
        EcpTrxTypeId.Display = false;    //判斷流程 trx type
        HeaderId.Display = false;        //erp portal傳來的header id
        RequestorNum.Display = false;    //需求者工號
		SheetNo.Display = false;         //SheetNo
		InvMmtNum.Display = false;       //費用領退料單號

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
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        //請購單URL
        string url = sp.getParam("PortalInvMmtUrl");
        ViewInvMmtUrl.Value = url;

        //顯示單號
        base.showData(engine, objects);
        Subject.ValueText = objects.getData("Subject");
        OrgId.ValueText = objects.getData("OrgId");
        OrgName.ValueText = objects.getData("OrgName");
        EcpTrxTypeId.ValueText = objects.getData("EcpTrxTypeId");
        InvMmtNum.ValueText = objects.getData("InvMmtNum");
        HeaderId.ValueText = objects.getData("HeaderId");
        TrxTypeName.ValueText = objects.getData("TrxTypeName");
        Version.ValueText = objects.getData("Version");
        RequestorName.ValueText = objects.getData("RequestorName");
        RequestorNum.ValueText = objects.getData("RequestorNum");
        RequestorDept.ValueText = objects.getData("RequestorDept");
        Comments.ValueText = objects.getData("Comments");
        LineQuantity.ValueText = objects.getData("LineQuantity");
        SumQuantity.ValueText = objects.getData("SumQuantity");
		CompanyCode.ValueText = objects.getData("CompanyCode");
		//if (CompanyCode.ValueText.Equals(""))
        //{
			if (OrgId.ValueText.Equals("SMP"))
			{
				CompanyCode.ValueText = "新普科技股份有限公司";
			}
			if (OrgId.ValueText.Equals("SHP"))
			{
				CompanyCode.ValueText = "新普科技股份有限公司";
			}
			if (OrgId.ValueText.Equals("TP"))
			{
				CompanyCode.ValueText = "中普科技(股)公司";
			}
		//}
		//MessageBox(CompanyCode.ValueText);
        //CreatorGUID.ValueText = objects.getData("CreatorGUID");
        //OriginatorGUID.ValueText = objects.getData("OriginatorGUID");

        CreatorGUID.ValueText = Convert.ToString(objects.getData("CreatorGUID")); //審核人1
        if (!CreatorGUID.ValueText.Equals(""))
        {
        	CreatorGUID.GuidValueText = objects.getData("CreatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        	CreatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     			
        }

        OriginatorGUID.ValueText = Convert.ToString(objects.getData("OriginatorGUID")); //審核人1
        if (!OriginatorGUID.ValueText.Equals(""))
        {
        	OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        	OriginatorGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     			
        }

        hlInvMmtNum.Text = InvMmtNum.ValueText;
        string headerId = Convert.ToString(objects.getData("HeaderId")); //Oracle INV MMT Number
        //費用領退料單Portal URI
        hlInvMmtNum.NavigateUrl = "javascript:onViewInvMmt(" + headerId + ")";

        Checkby1GUID.ValueText = Convert.ToString(objects.getData("Checkby1GUID")); //審核人1
        if (!Checkby1GUID.ValueText.Equals(""))
        {
			Checkby1GUID.GuidValueText = objects.getData("Checkby1GUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			Checkby1GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     			
        }

        Checkby2GUID.ValueText = Convert.ToString(objects.getData("Checkby2GUID")); //審核人2
        if (!Checkby2GUID.ValueText.Equals(""))
        {
			Checkby2GUID.GuidValueText = objects.getData("Checkby2GUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			Checkby2GUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
		}

        RdMemberGUID.ValueText = Convert.ToString(objects.getData("RdMemberGUID")); //RD
        if (!RdMemberGUID.ValueText.Equals(""))
        {
			RdMemberGUID.GuidValueText = objects.getData("RdMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			RdMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     			
        }

        MeMemberGUID.ValueText = Convert.ToString(objects.getData("MeMemberGUID")); //AME/ME
        if (!MeMemberGUID.ValueText.Equals(""))
        {
			MeMemberGUID.GuidValueText = objects.getData("MeMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			MeMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
        }

        PmMemberGUID.ValueText = Convert.ToString(objects.getData("PmMemberGUID")); //PM
        if (!PmMemberGUID.ValueText.Equals(""))
        {
			PmMemberGUID.GuidValueText = objects.getData("PmMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			PmMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     			
        }

        SaleMemberGUID.ValueText = Convert.ToString(objects.getData("SaleMemberGUID")); //SaleMember
        if (!SaleMemberGUID.ValueText.Equals(""))
        {
			SaleMemberGUID.GuidValueText = objects.getData("SaleMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			SaleMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
        }

        SaleManagerGUID.ValueText = Convert.ToString(objects.getData("SaleManagerGUID")); //SaleManager
        if (!SaleManagerGUID.ValueText.Equals(""))
        {
			SaleManagerGUID.GuidValueText = objects.getData("SaleManagerGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			SaleManagerGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
        }

        McMemberGUID.ValueText = Convert.ToString(objects.getData("McMemberGUID")); //物管
        if (!McMemberGUID.ValueText.Equals(""))
        {
			McMemberGUID.GuidValueText = objects.getData("McMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			McMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
        }

        QaMemberGUID.ValueText = Convert.ToString(objects.getData("QaMemberGUID")); //品管
        if (!QaMemberGUID.ValueText.Equals(""))
        {
			QaMemberGUID.GuidValueText = objects.getData("QaMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			QaMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
        }
		
		SqeMemberGUID.ValueText = Convert.ToString(objects.getData("SqeMemberGUID")); //SQE
        if (!SqeMemberGUID.ValueText.Equals(""))
        {
			SqeMemberGUID.GuidValueText = objects.getData("SqeMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			SqeMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
        }
		
		PurMemberGUID.ValueText = Convert.ToString(objects.getData("PurMemberGUID")); //PUR
        if (!PurMemberGUID.ValueText.Equals(""))
        {
			PurMemberGUID.GuidValueText = objects.getData("PurMemberGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
			PurMemberGUID.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID     
        }


        if (actName == "" || actName.Equals("填表人"))
        {

        }
        else
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;
            OriginatorGUID.ReadOnly = true;
            OrgName.ReadOnly = true;
            InvMmtNum.ReadOnly = true;
            TrxTypeName.ReadOnly = true;
            Version.ReadOnly = true;
            RequestorName.ReadOnly = true;
            RequestorDept.ReadOnly = true;
            Comments.ReadOnly = true;
            LineQuantity.ReadOnly = true;
            SumQuantity.ReadOnly = true;
			QaMemberGUID.ReadOnly = true;
			McMemberGUID.ReadOnly = true;
			SaleManagerGUID.ReadOnly = true;
			SaleMemberGUID.ReadOnly = true;
			PmMemberGUID.ReadOnly = true;
			MeMemberGUID.ReadOnly = true;
			RdMemberGUID.ReadOnly = true;
			Checkby2GUID.ReadOnly = true;
			Checkby1GUID.ReadOnly = true;
			CompanyCode.ReadOnly = true;
			SqeMemberGUID.ReadOnly = true;
			PurMemberGUID.ReadOnly = true;
        }
		if (actName.Equals("申請人") || actName.Equals("填表人"))
        {
			//QaMemberGUID.ReadOnly = false;
			//McMemberGUID.ReadOnly = false;
			//SaleManagerGUID.ReadOnly = false;
			//SaleMemberGUID.ReadOnly = false;
			//PmMemberGUID.ReadOnly = false;
			//MeMemberGUID.ReadOnly = false;
			//RdMemberGUID.ReadOnly = false;
			//Checkby2GUID.ReadOnly = false;
			//Checkby1GUID.ReadOnly = false;
        }
		
        if  (actName.Equals("申請人") || actName.Equals("填表人") || actName.Equals("需求者"))
        {
            string ecpTypeId = EcpTrxTypeId.ValueText;
            string orgId = OrgId.ValueText;

            if (ecpTypeId.Equals("1") && orgId.Equals("SHP"))
            {
                MeMemberGUID.ReadOnly = false;
				RdMemberGUID.ReadOnly = false;
            }
            if (ecpTypeId.Equals("1") && orgId.Equals("SMP"))
            {
                MeMemberGUID.ReadOnly = false;
				RdMemberGUID.ReadOnly = false;
				QaMemberGUID.ReadOnly = false;
            }
			if (ecpTypeId.Equals("1") && !orgId.Equals("SHP") && !orgId.Equals("SMP"))
            {
                MeMemberGUID.ReadOnly = false;
				RdMemberGUID.ReadOnly = false;
            }
            if (ecpTypeId.Equals("2"))
            {
				Checkby1GUID.ReadOnly = false;
				Checkby2GUID.ReadOnly = false;
                QaMemberGUID.ReadOnly = false;
            }
            if (ecpTypeId.Equals("3"))
            {
				Checkby1GUID.ReadOnly = false;
				SaleMemberGUID.ReadOnly = false;
				PmMemberGUID.ReadOnly = false;
				QaMemberGUID.ReadOnly = false;                    
            }
            if (ecpTypeId.Equals("4"))
            {
				Checkby1GUID.ReadOnly = false;
				QaMemberGUID.ReadOnly = false;  
            }
            if (ecpTypeId.Equals("5") && orgId.Equals("SMP"))
            {
                Checkby1GUID.ReadOnly = false;
				SaleMemberGUID.ReadOnly = false;
				SaleManagerGUID.ReadOnly = false;
				McMemberGUID.ReadOnly = false;  
            }
            if (ecpTypeId.Equals("5") && orgId.Equals("SHP"))
            {
                Checkby1GUID.ReadOnly = false;
				McMemberGUID.ReadOnly = false;
            }
			if (ecpTypeId.Equals("5") && !orgId.Equals("SHP") && !orgId.Equals("SMP"))
            {
                Checkby1GUID.ReadOnly = false;
				McMemberGUID.ReadOnly = false;
            }
            if (ecpTypeId.Equals("6") && orgId.Equals("SMP"))
            {
                Checkby1GUID.ReadOnly = false;
				SaleMemberGUID.ReadOnly = false;
				SaleManagerGUID.ReadOnly = false;
				McMemberGUID.ReadOnly = false;  
				QaMemberGUID.ReadOnly = false; 
            }
            if (ecpTypeId.Equals("6") && orgId.Equals("SHP"))
            {
				Checkby1GUID.ReadOnly = false; 
				McMemberGUID.ReadOnly = false; 
				QaMemberGUID.ReadOnly = false; 
            }
			if (ecpTypeId.Equals("6") && !orgId.Equals("SHP") && !orgId.Equals("SMP"))
            {
				Checkby1GUID.ReadOnly = false; 
				McMemberGUID.ReadOnly = false; 
				QaMemberGUID.ReadOnly = false; 
            }
            if (ecpTypeId.Equals("7"))
            {
				Checkby1GUID.ReadOnly = false; 
				McMemberGUID.ReadOnly = false; 
				QaMemberGUID.ReadOnly = false; 
            }
            if (ecpTypeId.Equals("8"))
            {
				Checkby1GUID.ReadOnly = false; 
				McMemberGUID.ReadOnly = false; 
				QaMemberGUID.ReadOnly = false;
            }
			if (ecpTypeId.Equals("51") || ecpTypeId.Equals("59"))
            {
                Checkby1GUID.ReadOnly = false;
				McMemberGUID.ReadOnly = false;
				SaleMemberGUID.ReadOnly = false;
				PmMemberGUID.ReadOnly = false;
				SaleManagerGUID.ReadOnly = false;
            }
			if (ecpTypeId.Equals("82"))
            {
				Checkby1GUID.ReadOnly = false;
				Checkby2GUID.ReadOnly = false;
				McMemberGUID.ReadOnly = false;
				SqeMemberGUID.ReadOnly = false;
				PurMemberGUID.ReadOnly = false;                    
            }
			AddSignButton.Display = true; //允許加簽
        }
		if  (actName.Equals("物管"))
        {
			AddSignButton.Display = true; //允許加簽
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
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("CreatorGUID", CreatorGUID.GuidValueText);
            objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
            objects.setData("OrgId", OrgId.ValueText);
            objects.setData("OrgName", OrgName.ValueText);
            objects.setData("EcpTrxTypeId", EcpTrxTypeId.ValueText);
            objects.setData("InvMmtNum", InvMmtNum.ValueText);
            objects.setData("HeaderId", HeaderId.ValueText);
            objects.setData("TrxTypeName", TrxTypeName.ValueText);
            objects.setData("Version", Version.ValueText);
            objects.setData("RequestorName", RequestorName.ValueText);
            objects.setData("RequestorNum", RequestorNum.ValueText);
            objects.setData("RequestorDept", RequestorDept.ValueText);
            objects.setData("Comments", Comments.ValueText);
            objects.setData("LineQuantity", LineQuantity.ValueText);
            objects.setData("SumQuantity", SumQuantity.ValueText);
			objects.setData("CompanyCode", CompanyCode.ValueText);
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);
        }        
        objects.setData("Checkby1GUID", Checkby1GUID.GuidValueText);
        objects.setData("Checkby2GUID", Checkby2GUID.GuidValueText);
        objects.setData("RdMemberGUID", RdMemberGUID.GuidValueText);
        objects.setData("MeMemberGUID", MeMemberGUID.GuidValueText);
        objects.setData("PmMemberGUID", PmMemberGUID.GuidValueText);
        objects.setData("SaleMemberGUID", SaleMemberGUID.GuidValueText);
        objects.setData("SaleManagerGUID", SaleManagerGUID.GuidValueText);
        objects.setData("McMemberGUID", McMemberGUID.GuidValueText);
        objects.setData("QaMemberGUID", QaMemberGUID.GuidValueText);
		objects.setData("SqeMemberGUID", SqeMemberGUID.GuidValueText);
        objects.setData("PurMemberGUID", PurMemberGUID.GuidValueText);

        string actName = (String)getSession("ACTName");
        if (actName.Equals("申請人"))
        {
            AddSignButton.Display = true;
        }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {        
        bool result = true;
        string strErrMsg = "";
        string actName = Convert.ToString(getSession("ACTName"));
        if (actName.Equals("申請人") || actName.Equals("填表人"))
        {
            string ecpTypeId = EcpTrxTypeId.ValueText;
            string orgId = OrgId.ValueText;

            if (ecpTypeId.Equals("1") && orgId.Equals("SHP"))
            {
                if (RdMemberGUID.ValueText.Equals("") || MeMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護RD、AME/ME人員\n";
                }    
            }
			if (ecpTypeId.Equals("1") && !orgId.Equals("SHP") && !orgId.Equals("SMP"))
            {
                if (RdMemberGUID.ValueText.Equals("") || MeMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護RD、AME/ME人員\n";
                }    
            }
            if (ecpTypeId.Equals("1") && orgId.Equals("SMP"))
            {
                //if (RdMemberGUID.ValueText.Equals("") || MeMemberGUID.ValueText.Equals("") || QaMemberGUID.ValueText.Equals(""))
				if (QaMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護品管人員\n";
                }
            }
            if (ecpTypeId.Equals("2"))
            {
                if (Checkby1GUID.ValueText.Equals("") || QaMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1、品管人員\n";
                }
            }
            if (ecpTypeId.Equals("3"))
            {
                if (Checkby1GUID.ValueText.Equals("") || QaMemberGUID.ValueText.Equals("") || PmMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1、品管、PM人員\n";
                }
            }
            if (ecpTypeId.Equals("4"))
            {
                if (Checkby1GUID.ValueText.Equals("") )
                {
                    strErrMsg += "請維護審核人1\n";
                }
            }
            if (ecpTypeId.Equals("5") && orgId.Equals("SMP"))
            {
                if (Checkby1GUID.ValueText.Equals("") )
                {
                    strErrMsg += "請維護審核人1\n";
                }
            }
            if (ecpTypeId.Equals("5") && orgId.Equals("SHP"))
            {
                if (Checkby1GUID.ValueText.Equals("") || McMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1、物管人員\n";
                }
            }
			if (ecpTypeId.Equals("5") && !orgId.Equals("SHP") && !orgId.Equals("SMP"))
            {
                if (Checkby1GUID.ValueText.Equals("") || McMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1、物管人員\n";
                }
            }
            if (ecpTypeId.Equals("6") && orgId.Equals("SMP"))
            {
                if (Checkby1GUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1\n";
                }
            }
            if (ecpTypeId.Equals("6") && orgId.Equals("SHP"))
            {
                if (Checkby1GUID.ValueText.Equals("") || McMemberGUID.ValueText.Equals("") || QaMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1、物管、品管人員\n";
                }
            }
			if (ecpTypeId.Equals("6") && !orgId.Equals("SHP") && !orgId.Equals("SMP"))
            {
                if (Checkby1GUID.ValueText.Equals("") || McMemberGUID.ValueText.Equals("") || QaMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1、物管、品管人員\n";
                }
            }
            if (ecpTypeId.Equals("7"))
            {
                if (Checkby1GUID.ValueText.Equals("") && McMemberGUID.ValueText.Equals("") )
                {
                    strErrMsg += "請維護審核人1、物管、品管人員\n";
                }
            }
            if (ecpTypeId.Equals("8"))
            {
                if (Checkby1GUID.ValueText.Equals("") || McMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護審核人1、物管人員\n";
                }
            }
			if (ecpTypeId.Equals("82"))
            {
                if (SqeMemberGUID.ValueText.Equals("") || PurMemberGUID.ValueText.Equals("")|| McMemberGUID.ValueText.Equals(""))
                {
                    strErrMsg += "請維護SQE、採購、物管人員\n";
                }
            }
        }

        if (!strErrMsg.Equals(""))
        {
            pushErrorMessage(strErrMsg);
            result = false;
        }
        //sw.Close();
        return result;  
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"]; //填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = OriginatorGUID.ValueText; //表單關系人
        si.ownerName = OriginatorGUID.ReadOnlyValueText;
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
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        string creator = CreatorGUID.ValueText;
        string originatorId = OriginatorGUID.ValueText;
        string orgName = OrgId.ValueText;
        string trxType = EcpTrxTypeId.ValueText;
        string checkby1 = "";
        string checkby2 = "";
        string rdMember = "";
        string meMember = "";
        string pmMember = "";
        string saleMember = "";
        string saleManager = "";
        string mcMember = "";
        string qaMember = "";
        string notifyId = "";
		string sqeMember = "";
		string purMember = "";

        if (!Checkby1GUID.ValueText.Equals(""))
        {
            checkby1 = Checkby1GUID.ValueText;
        }
        if (!Checkby2GUID.ValueText.Equals(""))
        {
            checkby2 = Checkby2GUID.ValueText;
        }
        if (!RdMemberGUID.ValueText.Equals(""))
        {
            rdMember = RdMemberGUID.ValueText;
        }
        if (!MeMemberGUID.ValueText.Equals(""))
        {
            meMember = MeMemberGUID.ValueText;
        }
        if (!PmMemberGUID.ValueText.Equals(""))
        {
            pmMember = PmMemberGUID.ValueText;
        }
        if (!McMemberGUID.ValueText.Equals(""))
        {
            mcMember = McMemberGUID.ValueText;
        }
        if (!QaMemberGUID.ValueText.Equals(""))
        {
            qaMember = QaMemberGUID.ValueText;
        }
        if (!SaleMemberGUID.ValueText.Equals(""))
        {
            saleMember = SaleMemberGUID.ValueText;
        }
        if (!SaleManagerGUID.ValueText.Equals(""))
        {
            saleManager = SaleManagerGUID.ValueText;
        }
		if (!SqeMemberGUID.ValueText.Equals(""))
        {
            sqeMember = SqeMemberGUID.ValueText;
        }
		if (!PurMemberGUID.ValueText.Equals(""))
        {
            purMember = PurMemberGUID.ValueText;
        }

        string ecpTypeId = EcpTrxTypeId.ValueText;
        string orgId = OrgId.ValueText;
        if (ecpTypeId.Equals("1") && orgId.Equals("SHP"))
        {
            notifyId = "T0010";
        }


        xml += "<SPERP005>";
        xml += "<creator DataType=\"java.lang.String\">" + creator + "</creator>";
        xml += "<originator DataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<checkby1 DataType=\"java.lang.String\">" + checkby1 + "</checkby1>";
        xml += "<checkby2 DataType=\"java.lang.String\">" + checkby2 + "</checkby2>";
        xml += "<rdMember DataType=\"java.lang.String\">" + rdMember + "</rdMember>";
        xml += "<meMember DataType=\"java.lang.String\">" + meMember + "</meMember>";
        xml += "<pmMember DataType=\"java.lang.String\">" + pmMember + "</pmMember>";
        xml += "<saleMember DataType=\"java.lang.String\">" + saleMember + "</saleMember>";
        xml += "<saleManager DataType=\"java.lang.String\">" + saleManager + "</saleManager>";
        xml += "<mcMember DataType=\"java.lang.String\">" + mcMember + "</mcMember>";
        xml += "<qaMember DataType=\"java.lang.String\">" + qaMember + "</qaMember>";
		xml += "<orgName DataType=\"java.lang.String\">" + orgName + "</orgName>";
		xml += "<trxType DataType=\"java.lang.String\">" + trxType + "</trxType>";
        xml += "<notify1 DataType=\"java.lang.String\">" + notifyId + "</notify1>";
		xml += "<sqeMember DataType=\"java.lang.String\">" + sqeMember + "</sqeMember>";
		xml += "<purMember DataType=\"java.lang.String\">" + purMember + "</purMember>";
        xml += "</SPERP005>";
		//writeLog("setFlowVariables xml: " + xml );
        //表單代號
        param["SPERP005"] = xml;
        return "SPERP005";
    }

    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine"></param>
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
    /// <param name="engine"></param>
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
                //SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
                //call oracle function set object workflow status to 'Reject'
                updateSourceStatus("Reject");
                //base.terminateThisProcess(si.fillerID);
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                base.writeLog(e);
                throw new Exception(e.StackTrace);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    /// <summary>
    /// 取回程序
    /// </summary>
    protected override void reGetProcedure()
    {
        //call oracle function set object workflow status to 'ReGet'
        updateSourceStatus("ReGet");
        base.reGetProcedure();
    }

    /// <summary>
    /// 撤銷程序
    /// </summary>
    protected override void withDrawProcedure()
    {
        //call oracle function set object workflow status to 'WithDraw'
        updateSourceStatus("WithDraw");
        base.withDrawProcedure();
    }

    /// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        try
        {
            //string sourceId = currentObject.getData("FlowId");
            string sourceId = currentObject.getData("HeaderId");
			//string sheetNo = currentObject.getData("SheetNo");
            if (result.Equals("Y"))
            {
                updateSourceStatus(engine, sourceId, "Close", currentObject);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        base.afterApprove(engine, currentObject, result);
    }

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string status)
    {
        string result = "";
        DataObject currentObject = (DataObject)getSession("currentObject");
        //string sourceId = currentObject.getData("FlowId");
        string sourceId = currentObject.getData("HeaderId");
        if (!sourceId.Equals(""))
        {
            result = updateSourceStatus(sourceId, status);
        }
        return result;
    }

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="number"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string number, string status)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_update_invmmt_headers";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_im_header_id", OracleType.Number).Value = Convert.ToInt32(number);
            objCmd.Parameters.Add("p_approved_status", OracleType.VarChar).Value = status;
			objCmd.Parameters.Add("p_action_history", OracleType.VarChar).Value = "";
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
        }
        return result;
    }
	
    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="number"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(AbstractEngine engine, string number, string status, DataObject currentObject)
    {
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);
		
		//sw.WriteLine("updateSourceStatus ");
		
        string result = "";
		string strOpinion = "";
        OracleConnection conn = null;
        try
        {
//DataObject currentObject = (DataObject)getSession("currentObject");
        //string prNumber = currentObject.getData("SPPOA006");		
			string sheetNo = currentObject.getData("SheetNo");	
			strOpinion = getWorkflowOpinion(engine, sheetNo);
			
			//sw.WriteLine("strOpinion=> " + strOpinion);
			
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "smp_update_invmmt_headers";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("p_im_header_id", OracleType.Number).Value = Convert.ToInt32(number);
            objCmd.Parameters.Add("p_approved_status", OracleType.VarChar).Value = status;
			objCmd.Parameters.Add("p_action_history", OracleType.VarChar).Value = strOpinion;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
			//sw.Close();
        }
        return result;
    }
	

	

    /// <summary>
    /// 取得簽核意見
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    private string getWorkflowOpinion(AbstractEngine engine, string sheetNo)
    {	
		//System.IO.StreamWriter sw = null;
		//sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);

        string strOpinion = "";
		
        try
        {
            string strSheetNo = Convert.ToString(getSession(PageUniqueID, "SheetNo"));
            string sql = "select wi.workItemName,u.userName,wi.createdTime ";
            sql += "from [NaNa].dbo.WorkItem wi, [NaNa].dbo.ProcessInstance pi, [WebFormPT].[dbo].SMWYAAA ya, [NaNa].dbo.Users u ";
            sql += "where SMWYAAA002='" + sheetNo + "' and wi.contextOID=pi.contextOID and pi.serialNumber=ya.SMWYAAA005 and u.OID=wi.performerOID ";
            sql += "  and wi.workItemName not in ('填表人')  ";
			sql += "union ";
			sql += "select   wi.workItemName,u.userName,wi.createdTime from  [NaNa].dbo.WorkItem wi, [NaNa].dbo.ProcessInstance pi, [WebFormPT].[dbo].SMWYAAA ya , [NaNa].dbo.WorkAssignment wa , [NaNa].dbo.Users u ";
			sql += "where SMWYAAA002='" + sheetNo + "'  and  wi.contextOID=pi.contextOID and  pi.serialNumber=ya.SMWYAAA005 and wa.workItemOID=wi.OID  and u.OID=wa.assigneeOID ";
            sql += "order by wi.createdTime asc";
			
			//sw.WriteLine("getWorkflowOpinion sql => " + sql);
			
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            //bool needGotComment = false;
			//sw.WriteLine("rows QTY => " + rows);
            for (int i = 0; i < rows; i++)
            {
                string actName = ds.Tables[0].Rows[i][0].ToString();
                string userName = ds.Tables[0].Rows[i][1].ToString();
                //string comment = ds.Tables[0].Rows[i][2].ToString();
                
                strOpinion += actName + ":" + userName + " ^ ";
                //sw.WriteLine("strOpinion in  => " + strOpinion);
            }
			//sw.WriteLine("strOpinion => " + strOpinion);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
		finally
        {
            //sw.Close();
        }
		
		
        return strOpinion;
    }
    protected void Checkby1GUID_BeforeClickButton()
    {
        Checkby1GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void MeMemberGUID_BeforeClickButton()
    {
        MeMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void McMemberGUID_BeforeClickButton()
    {
        McMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void Checkby2GUID_BeforeClickButton()
    {
        Checkby2GUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void PmMemberGUID_BeforeClickButton()
    {
        PmMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void QaMemberGUID_BeforeClickButton()
    {
        QaMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void RdMemberGUID_BeforeClickButton()
    {
        RdMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void SaleMemberGUID_BeforeClickButton()
    {
        SaleMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    protected void SaleManagerGUID_BeforeClickButton()
    {
        SaleManagerGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
	protected void SqeMemberGUID_BeforeClickButton()
    {
        SqeMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
	protected void PurMemberGUID_BeforeClickButton()
    {
        PurMemberGUID.whereClause = "(leaveDate='' OR leaveDate IS NULL)"; 
    }
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPERP005.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }
}