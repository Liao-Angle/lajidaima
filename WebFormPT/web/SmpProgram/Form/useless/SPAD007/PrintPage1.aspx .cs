using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;

public partial class SmpProgram_Form_SPAD007_PrintPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        //AbstractEngine erpEngine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            //string connStr = sp.getParam("WorkFlowERPDB");
            //erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
            
            //string strUserId = Request["originatorId"];
            string strUserId = (string)Session["UserId"];
			string strSheetNo = (string)Session["SPAD007_SheetNo"] ;
            if(Session["SPAD007_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["SPAD007_SheetNo"];
            }
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strStartDate = "";
            string strEndDate = "";		
            string sql = null;
			string strHeaderGUID = "";
			
			//string strAttendDate = Request["AttendDate"];
			
            DataSet ds = null;				

            //顯示使用者資訊
			sql = " select  a.GUID, Subject, SheetNo, u.empNumber, u.empName, u.empEName, funcName, deptId, deptName, b.userName as agentName  ";
			sql = sql + " , (select userName from Users where OID=CheckByGUID or OID is null ) checkBy ";
			sql = sql + " , StartTrvlDate, EndTrvlDate, SiteUs, SiteJp, SiteKr, SiteSub, SiteOther, SiteUsDesc, SiteJpDesc, SiteKrDesc, SiteSubDesc, SiteOtherDesc ";
			sql = sql + " , FeeCharge, TrvlDesc, PrePayTwd, PrePayTwdAmt, PrePayCny, PrePayCnyAmt, PrePayUsd, PrePayUsdAmt, PrePayOther, PrePayOtherAmt, PrePayComment ";
			//sql = sql + " , (select SheetNo from SmpForeignTrvl where GUID = a.OriForeignForm )  OriForeignFormNo ";
			sql = sql + " , (select top 1 SheetNo+'-'+Subject from SmpForeignTrvlV where GUID = a.OriForeignForm  ) OriForeignFormNo ";
			sql = sql + " , ActualStartTrvlDate, ActualEndTrvlDate, ActualPayTw, ActualPayTwTw, ActualPayUs, ActualPayUsTw, ActualPayJp, ActualPayJpTw, ActualPayKr, ActualPayKrTw ";
			sql = sql + " , ActualPayCn, ActualPayCnTw, ActualPayMa, ActualPayMaTw, ActualPayOu, ActualPayOuTw,ActualPayOther, ActualPayOtherTw ";
			sql = sql + " , ActualAmount, ActualApAmt, ActualRtnApAmt, ActualRtnApDate, FinDesc ";
			sql = sql + " , ChgTrvlDesc, ChgStartTrvlDate, ChgEndTrvlDate, ChgPrePayTwd, ChgPrePayTwdAmt, ChgPrePayCny, ChgPrePayCnyAmt, ChgPrePayUsd, ChgPrePayUsdAmt, ChgPrePayOther, ChgPrePayOtherAmt  ";
			sql = sql + ",case u.orgId when 'SMP' then '新普科技' else '中普科技' end as OrgName ";
			sql = sql + "from SmpForeignTrvlBilling a, EmployeeInfo u, Users b   ";
			sql = sql + "where a.OriginatorGUID=u.empGUID  and b.OID = a.AgentGUID   ";
			sql = sql + " and  SheetNo='" + Utility.filter(strSheetNo) + "'   ";
			
			//sw.WriteLine("sql  : " + sql);    

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
				strHeaderGUID =  ds.Tables[0].Rows[0]["GUID"].ToString();
				CompanyCode.Text =  ds.Tables[0].Rows[0]["OrgName"].ToString();
                Subject.Text =  ds.Tables[0].Rows[0]["Subject"].ToString();
                SheetNo.Text = ds.Tables[0].Rows[0]["SheetNo"].ToString();
				empNumber.Text = ds.Tables[0].Rows[0]["empNumber"].ToString();				
				empName.Text = ds.Tables[0].Rows[0]["empName"].ToString();
				empEName.Text =  ds.Tables[0].Rows[0]["empEName"].ToString();				
				empInfo.Text = empNumber.Text + '-' + empName.Text;
				strTripUserId = empNumber.Text; //簽名檔用
				
                funcName.Text = ds.Tables[0].Rows[0]["funcName"].ToString();
				deptId.Text = ds.Tables[0].Rows[0]["deptId"].ToString();
				deptName.Text = ds.Tables[0].Rows[0]["deptName"].ToString();
				deptInfo.Text = deptId.Text + '-' + deptName.Text;
				
				agentName.Text =  ds.Tables[0].Rows[0]["agentName"].ToString();
                checkBy.Text = ds.Tables[0].Rows[0]["checkBy"].ToString();
				if (checkBy.Text.Equals(""))
				{
					checkBy.Text = " - ";
				}
				
				StartTrvlDate.Text = ds.Tables[0].Rows[0]["StartTrvlDate"].ToString();
				EndTrvlDate.Text = ds.Tables[0].Rows[0]["EndTrvlDate"].ToString();			
				SiteUs.Text =  ds.Tables[0].Rows[0]["SiteUs"].ToString();
                SiteJp.Text = ds.Tables[0].Rows[0]["SiteJp"].ToString();
				SiteKr.Text = ds.Tables[0].Rows[0]["SiteKr"].ToString();
				SiteSub.Text = ds.Tables[0].Rows[0]["SiteSub"].ToString();
				SiteOther.Text =  ds.Tables[0].Rows[0]["SiteOther"].ToString();
                SiteUsDesc.Text = ds.Tables[0].Rows[0]["SiteUsDesc"].ToString();
				SiteJpDesc.Text = ds.Tables[0].Rows[0]["SiteJpDesc"].ToString();
				SiteKrDesc.Text = ds.Tables[0].Rows[0]["SiteKrDesc"].ToString();
				SiteSubDesc.Text =  ds.Tables[0].Rows[0]["SiteSubDesc"].ToString();
                SiteOtherDesc.Text = ds.Tables[0].Rows[0]["SiteOtherDesc"].ToString();
				if (SiteUs.Text.Equals("Y"))
				{
				  SiteInfo.Text = " 美國 - " + SiteUsDesc.Text;
				}
				if (SiteJp.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "日本 - " + SiteJpDesc.Text;
				}
				if (SiteKr.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "韓國 - " + SiteKrDesc.Text;
				}
				if (SiteSub.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "子公司 - " + SiteSubDesc.Text;
				}
				if (SiteOther.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "其他 - " + SiteOtherDesc.Text;
				}
				
				FeeCharge.Text = ds.Tables[0].Rows[0]["FeeCharge"].ToString();
				if (FeeCharge.Text.Equals("0"))
				{
				  FeeChargeInfo.Text = "新普";
				}
				else if (FeeCharge.Text.Equals("1"))
				{
				  FeeChargeInfo.Text = "新世";
				}
				else if (FeeCharge.Text.Equals("2"))
				{
				  FeeChargeInfo.Text = "新普(重慶) ";
				}
				else if (FeeCharge.Text.Equals("4"))
				{
				  FeeChargeInfo.Text = "中普";
				}
				else if (FeeCharge.Text.Equals("5"))
				{
				  FeeChargeInfo.Text = "太普";
				}
				else
				{
				  FeeChargeInfo.Text = "合普";
				}
				
				TrvlDesc.Text = ds.Tables[0].Rows[0]["TrvlDesc"].ToString().Replace("\n", "<br>");
				
				PrePayTwd.Text =  ds.Tables[0].Rows[0]["PrePayTwd"].ToString();
                PrePayTwdAmt.Text = ds.Tables[0].Rows[0]["PrePayTwdAmt"].ToString();
				PrePayCny.Text = ds.Tables[0].Rows[0]["PrePayCny"].ToString();
				PrePayCnyAmt.Text = ds.Tables[0].Rows[0]["PrePayCnyAmt"].ToString();
				PrePayUsd.Text =  ds.Tables[0].Rows[0]["PrePayUsd"].ToString();
                PrePayUsdAmt.Text = ds.Tables[0].Rows[0]["PrePayUsdAmt"].ToString();
				PrePayOther.Text = ds.Tables[0].Rows[0]["PrePayOther"].ToString();
				PrePayOtherAmt.Text = ds.Tables[0].Rows[0]["PrePayOtherAmt"].ToString();
				if (PrePayTwd.Text.Equals("Y"))
				{
				  PrePayInfo.Text = "  新台幣   " + PrePayTwdAmt.Text;
				}
				if (PrePayCny.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "  人民幣  " + PrePayCnyAmt.Text;
				}
				if (PrePayUsd.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "  美金  " + PrePayUsdAmt.Text;
				}
				if (PrePayOther.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "  其他  " + PrePayOtherAmt.Text;
				}
				if (PrePayInfo.Text.Equals(""))
				{
				  PrePayInfo.Text = " - ";
				}
				
				OriForeignFormNo.Text = ds.Tables[0].Rows[0]["OriForeignFormNo"].ToString();
				if (OriForeignFormNo.Text.Equals(""))
				{
				  OriForeignFormNo.Text = " ---- ";
				}
				
				ActualStartTrvlDate.Text = ds.Tables[0].Rows[0]["ActualStartTrvlDate"].ToString();
				ActualEndTrvlDate.Text = ds.Tables[0].Rows[0]["ActualEndTrvlDate"].ToString();
				
				ActualPayTw.Text = ds.Tables[0].Rows[0]["ActualPayTw"].ToString();
				if (ActualPayTw.Text.Equals(""))
				{
				  ActualPayTw.Text = "-  ";
				}
				ActualPayTwTw.Text = ds.Tables[0].Rows[0]["ActualPayTwTw"].ToString();
				if (ActualPayTwTw.Text.Equals(""))
				{
				  ActualPayTwTw.Text = "- ";
				}
				ActualPayUs.Text = ds.Tables[0].Rows[0]["ActualPayUs"].ToString();
				if (ActualPayUs.Text.Equals(""))
				{
				   ActualPayUs.Text = "- ";
				}
				ActualPayUsTw.Text = ds.Tables[0].Rows[0]["ActualPayUsTw"].ToString();
				if (ActualPayUsTw.Text.Equals(""))
				{
				  ActualPayUsTw.Text = "- ";
				}
				ActualPayJp.Text = ds.Tables[0].Rows[0]["ActualPayJp"].ToString();
				if (ActualPayJp.Text.Equals(""))
				{
				  ActualPayJp.Text = "-  ";
				}
				ActualPayJpTw.Text = ds.Tables[0].Rows[0]["ActualPayJpTw"].ToString();
				if (ActualPayJpTw.Text.Equals(""))
				{
				  ActualPayJpTw.Text = " - ";
				}
				ActualPayKr.Text = ds.Tables[0].Rows[0]["ActualPayKr"].ToString();
				if (ActualPayKr.Text.Equals(""))
				{
				  ActualPayKr.Text = "-   ";
				}
				ActualPayKrTw.Text = ds.Tables[0].Rows[0]["ActualPayKrTw"].ToString();
				if (ActualPayKrTw.Text.Equals(""))
				{
				  ActualPayKrTw.Text = " -   ";
				}
				ActualPayCn.Text = ds.Tables[0].Rows[0]["ActualPayCn"].ToString();
				if (ActualPayCn.Text.Equals(""))
				{
				  ActualPayCn.Text = " -  ";
				}
				ActualPayCnTw.Text = ds.Tables[0].Rows[0]["ActualPayCnTw"].ToString();
				if (ActualPayCnTw.Text.Equals(""))
				{
				  ActualPayCnTw.Text = "  - ";
				}
				ActualPayMa.Text = ds.Tables[0].Rows[0]["ActualPayMa"].ToString();
				if (ActualPayMa.Text.Equals(""))
				{
				  ActualPayMa.Text = " - ";
				}
				ActualPayMaTw.Text = ds.Tables[0].Rows[0]["ActualPayMaTw"].ToString();
				if (ActualPayMaTw.Text.Equals(""))
				{
				  ActualPayMaTw.Text = "  -";
				}
				ActualPayOu.Text = ds.Tables[0].Rows[0]["ActualPayOu"].ToString();
				if (ActualPayOu.Text.Equals(""))
				{
				  ActualPayOu.Text = " - ";
				}
				ActualPayOuTw.Text = ds.Tables[0].Rows[0]["ActualPayOuTw"].ToString();
				if (ActualPayOuTw.Text.Equals(""))
				{
				  ActualPayOuTw.Text = "  -";
				}
				ActualPayOther.Text = ds.Tables[0].Rows[0]["ActualPayOther"].ToString();
				if (ActualPayOther.Text.Equals(""))
				{
				  ActualPayOther.Text = " - ";
				}
				ActualPayOtherTw.Text = ds.Tables[0].Rows[0]["ActualPayOtherTw"].ToString();
				if (ActualPayOtherTw.Text.Equals(""))
				{
				  ActualPayOtherTw.Text = "- ";
				}
				//ActualEnterTtlAmount.Text =  "-  ";
				//ActualFuncTtlAmount.Text =  "-  ";
				//20140122 系統自動加總各幣別金額 start 
				if (ds.Tables[0].Rows[0]["ActualPayTw"].ToString().Equals(""))
				{
					//string[,] ids = null;
			        //string sql = null;
					string sqlTtl = "";
			        DataSet dsTtl = null;
					decimal amt_ttl = 0;
					string currency = "";
					int count = 0;
					decimal tempOtherSum = 0;
					decimal tempCAD = 0;
					decimal tempAUD = 0;
					decimal tempHKD = 0;
					decimal tempINR = 0;
					decimal tempPLN = 0;
					decimal tempTHB = 0;
					decimal tempSGD = 0;
					decimal tempOTHER = 0;
					
					try{
						//sql = " select  CAST(sum( CONVERT(decimal (6,2), OccurAmt)) as nvarchar(30))  amt_total , OccurCurrency";
						sqlTtl = " select   sum( CONVERT(decimal (12,2), OccurAmt))  amt_total , OccurCurrency";
			            sqlTtl += " from SmpForeignTrvlBillingDetail ";
			            sqlTtl += " where HeaderGUID = (select GUID from SmpForeignTrvlBilling where SheetNo='" + Utility.filter(strSheetNo) + "') ";
			            sqlTtl += "group by OccurCurrency ";
							   
						dsTtl = engine.getDataSet(sqlTtl, "TEMP");
			            count = dsTtl.Tables[0].Rows.Count;

			            for (int i = 0; i < count; i++)
			            {                
							amt_ttl  = Convert.ToDecimal(dsTtl.Tables[0].Rows[i]["amt_total"]);
			                currency = dsTtl.Tables[0].Rows[i]["OccurCurrency"].ToString();
							
							if (currency.Equals("TWD")){
								ActualPayTw.Text = Convert.ToString(amt_ttl);
							}
							else if  (currency.Equals("USD")){
								ActualPayUs.Text = Convert.ToString(amt_ttl);
							}
							else if (currency.Equals("JPY")){
								ActualPayJp.Text = Convert.ToString(amt_ttl);
							}
							else if (currency.Equals("KRW")){
								ActualPayKr.Text = Convert.ToString(amt_ttl);
							}
							else if (currency.Equals("CNY")){
								ActualPayCn.Text = Convert.ToString(amt_ttl);
							}
							else if (currency.Equals("MRY")){
								ActualPayMa.Text = Convert.ToString(amt_ttl);
							}
							else if (currency.Equals("TWD")){
								ActualPayMa.Text = Convert.ToString(amt_ttl);
							}
							else if (currency.Equals("EUR")){
								ActualPayOu.Text = Convert.ToString(amt_ttl);
							}else if (currency.Equals("CAD")){
								tempCAD = amt_ttl;
							}else if (currency.Equals("AUD")){
								tempAUD = amt_ttl;
							}else if (currency.Equals("HKD")){
								tempHKD = amt_ttl;
							}else if (currency.Equals("INR")){
								tempINR = amt_ttl;
							}else if (currency.Equals("PLN")){
								tempPLN = amt_ttl;
							}else if (currency.Equals("THB")){
								tempTHB = amt_ttl;
							}else if (currency.Equals("SGD")){
								tempSGD = amt_ttl;
							}else {
								tempOTHER = amt_ttl;
							}
							tempOtherSum = tempCAD + tempAUD + tempHKD + tempINR + tempPLN + tempTHB + tempSGD + tempOTHER;				
							ActualPayOther.Text = Convert.ToString(tempOtherSum);
			            }               	   
					}
					catch (Exception ze)
			        {
			          //MessageBox(ze.Message);
			          //writeLog(ze);
			        }
				}
				//20140122 系統自動加總各幣別金額 end 
				
				ActualAmount.Text = ds.Tables[0].Rows[0]["ActualAmount"].ToString();
				if (ActualAmount.Text.Equals(""))
				{
				  ActualAmount.Text = "-  ";
				  ActualAmount.Text = "-  ";
				}
				ActualApAmt.Text = ds.Tables[0].Rows[0]["ActualApAmt"].ToString();
				if (ActualApAmt.Text.Equals(""))
				{
				  ActualApAmt.Text = " - ";
				}
				ActualRtnApAmt.Text = ds.Tables[0].Rows[0]["ActualRtnApAmt"].ToString();
				if (ActualRtnApAmt.Text.Equals(""))
				{
				  ActualRtnApAmt.Text = "  -";
				}
				ActualRtnApDate.Text = ds.Tables[0].Rows[0]["ActualRtnApDate"].ToString();
				if (ActualRtnApDate.Text.Equals(""))
				{
				  ActualRtnApDate.Text = "  -";
				}
				FinDesc.Text = ds.Tables[0].Rows[0]["FinDesc"].ToString();
				if (FinDesc.Text.Equals(""))
				{
				  FinDesc.Text = " -- ";
				}		
				ChgStartTrvlDate.Text = ds.Tables[0].Rows[0]["ChgStartTrvlDate"].ToString();
				if (ChgStartTrvlDate.Text.Equals(""))
				{
				  ChgStartTrvlDate.Text = " -- ";
				}
				ChgEndTrvlDate.Text = ds.Tables[0].Rows[0]["ChgEndTrvlDate"].ToString();				
				if (ChgEndTrvlDate.Text.Equals(""))
				{
				  ChgEndTrvlDate.Text = " -- ";
				}
				ChgTrvlDesc.Text = ds.Tables[0].Rows[0]["ChgTrvlDesc"].ToString();				
				if (ChgTrvlDesc.Text.Equals(""))
				{
				  ChgTrvlDesc.Text = " -- ";
				}
				//異動後預支申請
				ChgPrePayTwd.Text =  ds.Tables[0].Rows[0]["ChgPrePayTwd"].ToString();
                ChgPrePayTwdAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayTwdAmt"].ToString();
				ChgPrePayCny.Text = ds.Tables[0].Rows[0]["ChgPrePayCny"].ToString();
				ChgPrePayCnyAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayCnyAmt"].ToString();
				ChgPrePayUsd.Text =  ds.Tables[0].Rows[0]["ChgPrePayUsd"].ToString();
                ChgPrePayUsdAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayUsdAmt"].ToString();
				ChgPrePayOther.Text = ds.Tables[0].Rows[0]["ChgPrePayOther"].ToString();
				ChgPrePayOtherAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayOtherAmt"].ToString();
				if (ChgPrePayTwd.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = "  新台幣   " + ChgPrePayTwdAmt.Text;
				}
				if (ChgPrePayCny.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = ChgPrePayInfo.Text + "  人民幣  " + ChgPrePayCnyAmt.Text;
				}
				if (ChgPrePayUsd.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = ChgPrePayInfo.Text + "  美金  " + ChgPrePayUsdAmt.Text;
				}
				if (ChgPrePayOther.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = ChgPrePayInfo.Text + "  其他  " + ChgPrePayOtherAmt.Text;
				}
				if (ChgPrePayInfo.Text.Equals(""))
				{
				  ChgPrePayInfo.Text = " - ";
				}
            }
			
			//顯示差旅費報銷明細清單
			DataSet ds1 = null;
			sql = "select ROW_NUMBER() OVER(ORDER BY OccurDate) AS rownum, OccurDate, PayClass, OccurAmt, OccurCurrency, OccurDesc from SmpForeignTrvlBillingDetail where HeaderGUID='" + strHeaderGUID + "' ";
            ds1 = engine.getDataSet(sql, "TEMP");

            // Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow detailRec;
 
            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("序號", typeof(string)));
			dt.Columns.Add(new DataColumn("日期 ", typeof(string)));            
            dt.Columns.Add(new DataColumn("類別", typeof(string)));
            dt.Columns.Add(new DataColumn("金額", typeof(string)));
            dt.Columns.Add(new DataColumn("幣別", typeof(string)));
            dt.Columns.Add(new DataColumn("摘要", typeof(string)));

            int drows = ds1.Tables[0].Rows.Count;
            string[][] result = new string[drows][];
            for (int i = 0; i < drows; i++)
            {
                detailRec = dt.NewRow();
                detailRec[0] = ds1.Tables[0].Rows[i]["rownum"].ToString();
                detailRec[1] = ds1.Tables[0].Rows[i]["OccurDate"].ToString();
                detailRec[2] = ds1.Tables[0].Rows[i]["PayClass"].ToString();
                detailRec[3] = ds1.Tables[0].Rows[i]["OccurAmt"].ToString();
                detailRec[4] = ds1.Tables[0].Rows[i]["OccurCurrency"].ToString();
                detailRec[5] = ds1.Tables[0].Rows[i]["OccurDesc"].ToString();
                dt.Rows.Add(detailRec);
            }
            DataView dv = new DataView(dt);

            tripFeeSummary.DataSource = dv;
            tripFeeSummary.DataBind();
			
			
			try
			{
			//取得簽核人員
			string[] dr= null;
			string imgSerial = "0";
			string imgUserNo = "";
			string signItemName = "";
			string signTime = "";
			string signFileName = "";
			string url = "http://192.168.2.137/ECP/Personal/";
			string urlNa = "http://192.168.2.137/ECP/Personal/na.jpg";
			
			//預設值
			Img6.ImageUrl = urlNa;
			Img6.AlternateText = "董事長";
			Img6Time.Text = " ";
						
			Img5.ImageUrl = urlNa;
			Img5.AlternateText = "審核人5";
			Img5Time.Text = " ";
			
			Img4.ImageUrl = urlNa;
			Img4.AlternateText = "審核人4";
			Img4Time.Text = " ";
			
			Img3.ImageUrl = urlNa;
			Img3.AlternateText = "審核人3";
			Img3Time.Text = " ";
			
			Img2.ImageUrl = urlNa;
			Img2.AlternateText = "審核人2";
			Img2Time.Text = " ";
			
			Img1.ImageUrl = urlNa;
			Img1.AlternateText = "審核人1";
			Img1Time.Text = " ";
			
			sql = "select ROW_NUMBER() OVER(ORDER BY  wi.completedTime) AS ROWID, ur.id userId, ur.userName, wi.workItemName ,CONVERT(VARCHAR, wi.completedTime, 120 ) completedTime , fi.FILENAME+'.'+fi.FILEEXT imgName ";
			sql = sql + "from NaNa.dbo.WorkItem wi, NaNa.dbo.ProcessInstance pi, WebFormPT.dbo.SMWYAAA ya, NaNa.dbo.Users ur , WebFormPT.dbo.FILEITEM fi , SmpForeignTrvlBilling sb  ";
			sql = sql + "where wi.contextOID=pi.contextOID ";
			sql = sql + "and pi.serialNumber=ya.SMWYAAA005 and ur.OID = wi.performerOID and fi.LEVEL1 = ur.id ";
			sql = sql + "and wi.workItemName in ('ChargeSTCS','ChargeSCQ','審核人','直屬主管','審核人','董事長','處級','處級主管','部級','總經理','會簽','SMP特助') ";
			//sql = sql + "and wi.workItemName in ('ChargeSTCS','ChargeSCQ','審核人','直屬主管','審核人','處級','處級主管','部級','總經理','會簽') ";
			sql = sql + "and substring(SMWYAAA002,1,7) in ('SPAD007','SPAD005','SPAD006','SPAD007') ";
			sql = sql + "and sb.GUID = ya.SMWYAAA019  ";
			sql = sql + "and sb.SheetNo='" + Utility.filter(strSheetNo) + "'  ";
			//sql = sql + "and ya.SMWYAAA002='" + Utility.filter(strSheetNo) + "'  ";
			ds = engine.getDataSet(sql, "TEMP");
			int rows = ds.Tables[0].Rows.Count;
			//string[][] result = new string[rows][];
           
		    for (int i = 0; i < rows; i++)
            {
				//dr = dt.NewRow();
				imgSerial = ds.Tables[0].Rows[i]["ROWID"].ToString();
				imgUserNo = ds.Tables[0].Rows[i]["userId"].ToString();
				signItemName = ds.Tables[0].Rows[i]["workItemName"].ToString();
				signTime =  ds.Tables[0].Rows[i]["completedTime"].ToString();
				signFileName = ds.Tables[0].Rows[i]["imgName"].ToString();
				if (imgSerial.Equals("1") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img1.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img1.AlternateText = signItemName;
			        Img1Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("2") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img2.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img2.AlternateText = signItemName;
			        Img2Time.Text = signTime;
				  }
				}
                if (imgSerial.Equals("3") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img3.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img3.AlternateText = signItemName;
			        Img3Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("4") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img4.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img4.AlternateText = signItemName;
			        Img4Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("5") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img5.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img5.AlternateText = signItemName;
			        Img5Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("6") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img6.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img6.AlternateText = signItemName;
			        Img6Time.Text = signTime;
				  }
				}
			}
			
			
			
			}
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (engine != null) engine.close();
            //if (erpEngine != null) erpEngine.close();
            if (sw != null) sw.Close();
        }
    }
	
	
	
}