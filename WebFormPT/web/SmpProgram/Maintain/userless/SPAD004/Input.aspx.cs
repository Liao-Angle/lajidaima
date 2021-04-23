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

public partial class SmpProgram_maintain_SPAD004_Input : BaseWebUI.DataListSaveForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                string[,] ids = null;

                //公司別
                string userId = (string)Session["UserID"];
                //string userGuid = (string)Session["UserGUID"];
                try
                {
                    //公司別
                    ids = new string[,]{
                        {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "smp", "新普科技")},
                        {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad001_form_aspx.language.ini", "message", "tp", "中普科技")}
                    };
                    CompanyCode.setListItem(ids);
                    string orgId = "SMP";
                    string sql = "select orgId from EmployeeInfo where empNumber='" + userId + "'";
                    string value = (string)engine.executeScalar(sql);

                    if (value != null)
                    {
                        orgId = value;
                    }
                    CompanyCode.ValueText = orgId;
                    CompanyCode.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    writeLog(ex);
                }
                
                //承辦人員
                OriginatorGUID.clientEngineType = engineType;
                OriginatorGUID.connectDBString = connectString;

                CheckUser.clientEngineType = engineType;
                CheckUser.connectDBString = connectString;

                CheckDept.clientEngineType = engineType;
                CheckDept.connectDBString = connectString;

                //申請單位                
                DeptGUID.clientEngineType = engineType;
                DeptGUID.connectDBString = connectString;
                try {
                    string sql = "select organizationUnitOID  from Functions where occupantOID=(select OID from Users where id='" + userId + "')";
                    string value = (string)engine.executeScalar(sql);

                    if (value != null)
                    {
                        DeptGUID.ValueText = value;
                        DeptGUID.doValidate();
                    }
                }
                catch (Exception ex)
                {
                    writeLog(ex);
                }
                
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        //head
        CompanyCode.ValueText = objects.getData("CompanyCode");
        //CompanyName.ValueText = objects.getData("CompanyName");
        OriginatorGUID.GuidValueText = objects.getData("OriginatorGUID");
        OriginatorGUID.doGUIDValidate();
        DeptGUID.GuidValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();
        SheetNo.ValueText = objects.getData("SheetNo");
        BillingDate.ValueText = objects.getData("BillingDate");
        TotalAmount.ValueText = objects.getData("TotalAmount");       
        
        if (isNew)
        {
            BillingDate.ValueText = DateTime.Now.ToString("yyyy-MM");//取年 + 取月
            OriginatorGUID.ValueText = (string)Session["UserId"];
            OriginatorGUID.doValidate();
        }

        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPAD004.SmpTripBillSumDetail");
            detail.setTableName("SmpTripBillSumDetail");
            detail.loadFileSchema();
            objects.setChild("SmpTripBillSumDetail", detail);
        }
        else
        {
            detail = objects.getChild("SmpTripBillSumDetail");
            CompanyCode.ReadOnly = true;
            //DeleteButton.Display = Closed.ValueText.Equals("Y") ? false : true;
        }
        //detail
        DetailList.dataSource = detail;
        DetailList.NoAdd = true;
        DetailList.HiddenField = new string[] { "GUID", "SummaryGUID", "UserGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
        DetailList.reSortCondition("工號", DataObjectConstants.ASC);
        DetailList.updateTable();

        SheetNo.ReadOnly = true;
        TotalAmount.ReadOnly = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            bool isNew = (bool)getSession("isNew");

            string companyName = "";
            //objects.setData("CompanyCode", CompanyCode.ValueText);
            if (isNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                string seqNo = "";
                if (!CompanyCode.ValueText.Equals("") && !BillingDate.ValueText.Equals(""))
                    seqNo = getCustomSheetNo(engine, "SMPTripSumSeqNo", CompanyCode.ValueText + BillingDate.ValueText.Replace("-","")+"-");
                objects.setData("SheetNo", seqNo);
                objects.setData("BillingDate", BillingDate.ValueText);
                objects.setData("OriginatorGUID", OriginatorGUID.GuidValueText);
                objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("DeptNo", DeptGUID.ValueText);
                objects.setData("organizationUnitName", DeptGUID.ReadOnlyValueText);
                objects.setData("CompanyCode", CompanyCode.ValueText);
                objects.setData("EmpNumber", OriginatorGUID.ValueText);
                objects.setData("userName", OriginatorGUID.ReadOnlyValueText);
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("IS_LOCK", "N");
                objects.setData("DATA_STATUS", "Y");
            }

            if (CompanyCode.ValueText.Equals("SMP"))
            {
                companyName = "新普科技";
            }
            else if (CompanyCode.ValueText.Equals("TP"))
            {
                companyName = "中普科技";
            }
            objects.setData("organizationName", companyName);
            objects.setData("TotalAmount", TotalAmount.ValueText);
            			
	        DataObjectSet detail = DetailList.dataSource;
	        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
	        {
	            DataObject dt = detail.getAvailableDataObject(i);
                dt.setData("SummaryGUID", objects.getData("GUID"));				
	        }
            

        }
        catch (Exception e)
        {
            writeLog(e);
        }   
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPAD004.SmpTripBillSummaryAgent");
        agent.engine = engine;
        agent.query("1=2");

        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            result = agent.update();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }

    protected bool DetailList_BeforeOpenWindow(DataObject objects, bool isAddNew)
    {
        DataObjectSet set = DetailList.dataSource;
        ArrayList schInfo = new ArrayList();

        for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
        {
            DataObject obj = set.getAvailableDataObject(i);
            schInfo.Add(obj.getData("GUID") + "," + obj.getData("SheetNo"));
        }
        setSession((string)Session["UserID"], "SchInfo", schInfo);

        return true;
    }

    protected bool DetailList_BeforeDeleteData()
    {
        //string strMessage = "";
        //string closed = "";
        //string subjectName = "";
        //System.IO.StreamWriter sw1 = null;
        /*
        try
        {
            //sw1 = new System.IO.StreamWriter(@"d:\temp\SPTS002.log", true); 
            DataObject[] sch = DetailList.getSelectedItem();
            DataObjectSet setSch = DetailList.dataSource;

            for (int i = 0; i < sch.Length; i++)
            {
                DataObject schDataObject = sch[i];
                if (schDataObject != null)
                {
                    closed = schDataObject.getData("Closed");
                    subjectName = schDataObject.getData("SubjectName");
                    if (closed.Equals("Y"))
                    {
                        strMessage += "課程名稱 [" + subjectName + "] 已開班授課，不可刪除!\n";
                        DetailList.UnCheckAllData();
                        DetailList.Focus();
                    }
                }
            }

            if (!strMessage.Equals(""))
            {
                MessageBox(strMessage);
                DetailList.UnCheckAllData();
                DetailList.Focus();
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            //if (sw1 != null)
            //    sw1.Close();            
        }

        */
        return true;
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        //System.IO.StreamWriter sw = null;
        //sw = new System.IO.StreamWriter(@"d:\temp\SPERP005.log", true, System.Text.Encoding.Default);

        int count = 0;
        decimal totalAmount = 0;
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string billingUser = CheckUser.ValueText;
            string billingDept = CheckDept.ValueText;
            string billingSDate = StartDate.ValueText;
            string billingEDate = EndDate.ValueText;
            string trafficFee = "0";
            string eatFee = "0";
            string parkingFee = "0";
            string otherFee = "0";
            string etagFee = "0";
            string oilFee = "0";
            string whereCondition = "";
                       
            if (!billingUser.Equals(""))
            {
                whereCondition += " and u.empNumber = '" + billingUser + "' ";
            }
            if (!billingDept.Equals(""))
            {
                whereCondition += " and u.deptId = '" + billingDept + "' ";
            }
            if (!billingSDate.Equals("") && !billingEDate.Equals(""))
            {
                whereCondition += " and TripDate >= '" + billingSDate + "' and TripDate <= '" + billingEDate + "' ";
            }
            if (billingSDate.Equals("") && !billingEDate.Equals(""))
            {
                whereCondition += " and TripDate <= '" + billingEDate + "' ";
            }

            DataObject doo = null;
            DataObjectSet dos = DetailList.dataSource;


            string sql = " select distinct d.GUID, u.empGUID, u.empNumber as UserId, u.empName userName, d.TripDate, d.TrafficFee, d.EatFee, d.ParkingFee, d.OtherFee ";
            sql += "     , d.OriTripFormGUID as SheetNo,  u.deptId as DeptId, u.deptName as DeptName, d.StartMileage, d.EndMileage, d.EtagFee, d.MileageSum, d.StartTime, d.EndTime ";
            sql += " 	, d.OilFee, d.TripSite , d.EtcStart , d.EtcEnd , d.RefEtagFee ";
            sql += " from SmpTripBilling h, SmpTripBillingDetail d, EmployeeInfo u, SMWYAAA y ";
            sql += " where h.GUID=d.HeaderGUID  ";
            sql += " and h.GUID=y.SMWYAAA019 and y.SMWYAAA020 = 'I' ";
            sql += " and u.empGUID=d.UserGUID ";
            sql += " and d.OriTripFormGUID not in (select OriTripFormGUID from SmpTripBillSumDetail) ";
            sql += " and d.TripDate > '2014/12/31' "; 
			sql += " and d.OriTripFormGUID in (select OriTripFormGUID from SmpTripBillToFinV) ";
			sql += whereCondition;
            //sw.WriteLine("sql : " + sql);

            DataSet allBillingList = engine.getDataSet(sql, "TEMP");
            count = allBillingList.Tables[0].Rows.Count;

            for (int i = 0; i < count; i++)
            {
                doo = dos.create();

                trafficFee = allBillingList.Tables[0].Rows[i]["TrafficFee"].ToString();
                if (trafficFee.Equals("")) { trafficFee = "0"; }
                eatFee = allBillingList.Tables[0].Rows[i]["EatFee"].ToString();
                if (eatFee.Equals("")) { eatFee = "0"; }
                parkingFee = allBillingList.Tables[0].Rows[i]["ParkingFee"].ToString();
                if (parkingFee.Equals("")) { parkingFee = "0"; }
                otherFee = allBillingList.Tables[0].Rows[i]["OtherFee"].ToString();
                if (otherFee.Equals("")) { otherFee = "0"; }
                etagFee = allBillingList.Tables[0].Rows[i]["EtagFee"].ToString();
                if (etagFee.Equals("")) { etagFee = "0"; }
                oilFee = allBillingList.Tables[0].Rows[i]["OilFee"].ToString();
                if (oilFee.Equals("")) { oilFee = "0"; }

                totalAmount = totalAmount + decimal.Parse(trafficFee) + decimal.Parse(eatFee) + decimal.Parse(parkingFee) + decimal.Parse(otherFee) + decimal.Parse(etagFee) + decimal.Parse(oilFee);

                //sw.WriteLine("totalAmount : " + totalAmount);

                doo.setData("GUID", IDProcessor.getID(""));
                doo.setData("SummaryGUID", "TEMP");
                doo.setData("UserGUID", allBillingList.Tables[0].Rows[i]["empGUID"].ToString());
                doo.setData("UserId", allBillingList.Tables[0].Rows[i]["UserId"].ToString());
                doo.setData("UserName", allBillingList.Tables[0].Rows[i]["userName"].ToString());
                doo.setData("TripDate", allBillingList.Tables[0].Rows[i]["TripDate"].ToString());
                doo.setData("StartTime", allBillingList.Tables[0].Rows[i]["StartTime"].ToString());
                doo.setData("EndTime", allBillingList.Tables[0].Rows[i]["EndTime"].ToString());
                doo.setData("StartMileage", allBillingList.Tables[0].Rows[i]["StartMileage"].ToString());
                doo.setData("EndMileage", allBillingList.Tables[0].Rows[i]["EndMileage"].ToString());
                doo.setData("MileageSum", allBillingList.Tables[0].Rows[i]["MileageSum"].ToString());
                doo.setData("OilFee", oilFee);
                doo.setData("TrafficFee", trafficFee);
                doo.setData("EatFee", eatFee);
                doo.setData("ParkingFee", parkingFee);
                doo.setData("EtagFee", etagFee);
                doo.setData("OtherFee", otherFee);
                doo.setData("OriTripFormGUID", allBillingList.Tables[0].Rows[i]["SheetNo"].ToString());
                doo.setData("TripSite", allBillingList.Tables[0].Rows[i]["TripSite"].ToString());
                doo.setData("DeptId", allBillingList.Tables[0].Rows[i]["DeptId"].ToString());
                doo.setData("DeptName", allBillingList.Tables[0].Rows[i]["DeptName"].ToString());
                doo.setData("IS_LOCK", "N");
                doo.setData("IS_DISPLAY", "Y");
                doo.setData("DATA_STATUS", "Y");

                //dos.add(doo);    
                bool strReturn = true; //檢查是否重覆資料
                //資料檢查
                strReturn = tripDetailRepeatCheck(doo);
                //sw.WriteLine("回傳資料 : " + strReturn);
                if (strReturn)
                {
                    dos.add(doo);
                    DetailList.NoAdd = true;
                    DetailList.dataSource = dos;
                    DetailList.HiddenField = new string[] { "GUID", "SummaryGUID", "UserGUID", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
                    DetailList.updateTable();                    
                    calculateTotal();
                }
            }

            engine.close();
        }
        catch (Exception ze)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(ze.Message);
            writeLog(ze);
        }
        //if (sw != null) sw.Close();
    }

    protected bool tripDetailRepeatCheck(com.dsc.kernal.databean.DataObject objects)
    {
        string[] keys = objects.keyField;
        objects.keyField = new string[] { "OriTripFormGUID" };

        DataObjectSet accessSet = DetailList.dataSource;
        if (!accessSet.checkData(objects))
        {
            MessageBox(" 國內出差單請款資料重覆! ");
            objects.keyField = keys;
            return false;
        }
        keys = objects.keyField;
        objects.keyField = new string[] { "OriTripFormGUID" };
        return true;

    }


    private void calculateTotal()
    {
        decimal total = 0;
        try
        {
            for (int i = 0; i < DetailList.dataSource.getAvailableDataObjectCount(); i++)
            {
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("OilFee"));
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("TrafficFee"));
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("EatFee"));
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("ParkingFee"));
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("OtherFee"));
                total += decimal.Parse(DetailList.dataSource.getAvailableDataObject(i).getData("EtagFee"));
            }
        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
            // if (sw != null) sw.Close();
        }

        TotalAmount.ValueText = total.ToString();
    }

    /// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomSheetNo(AbstractEngine engine, string code, string companycodeYear)
    {
        string sheetNo = "";
        try
        {
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("CompanyYear", companycodeYear);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
            base.writeLog(new Exception("sheetno:" + sheetNo));
        }
        return sheetNo;
    }
    protected void DetailList_AddOutline(DataObject objects, bool isNew)
    {
        calculateTotal();
    }
    protected void DetailList_DeleteData()
    {
        calculateTotal();
    }
    //列印單據
    protected void PrintButton_OnClick(object sender, EventArgs e)
    {        
		//MessageBox("SheetNo : " + SheetNo.ValueText);
        Session["SPAD004M_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
	    base.showOpenWindow(url, "列印國內出差旅費彙總表", "", "600", "", "", "", "1", "1", "", "", "", "", "780", "", true);
    }
}
