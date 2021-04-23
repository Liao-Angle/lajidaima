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

public partial class SmpProgram_Form_SPAD010_PrintPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        //AbstractEngine erpEngine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\ecp\WebFormPT\web\LogFolder\SPAD010_Report.log", true, System.Text.Encoding.Default);
			//sw.WriteLine("in page load");
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            
            string strUserId = (string)Session["UserId"];
			string strSheetNo = (string)Session["SPAD010_SheetNo"] ;
			
            if(Session["SPAD010_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["SPAD010_SheetNo"];
            }
						
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strStartDate = "";
            string strEndDate = "";		
			string strHeaderGUID = "";
            string sql = null;
			DataSet ds = null;				

            //顯示使用者資訊
			sql = "select  a.GUID, Subject, SheetNo, u.empNumber, u.empName, u.empEName, funcName, deptId, deptName ";
			sql = sql + "	 , (select userName from Users where OID=CheckByGUID or OID is null ) checkBy ";
			sql = sql + "	 , (select userName from Users where OID=PayeeGUID or OID is null ) payeeBy ";
			sql = sql + "	 , StartDate, EndDate, FinDesc, TotalAmount, case u.orgId when 'SMP' then '新普科技' else '中普科技' end as OrgName  ";
			sql = sql + " from SmpTripBilling a, EmployeeInfo u ";
			sql = sql + " where SheetNo='" + Utility.filter(strSheetNo) + "'  and a.OriginatorGUID=u.empGUID ";
			
			ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
			    strHeaderGUID =  ds.Tables[0].Rows[0]["GUID"].ToString();
                Subject.Text =  ds.Tables[0].Rows[0]["Subject"].ToString();
                SheetNo.Text = ds.Tables[0].Rows[0]["SheetNo"].ToString();
				empNumber.Text = ds.Tables[0].Rows[0]["empNumber"].ToString();				
				empName.Text = ds.Tables[0].Rows[0]["empName"].ToString();
				empEName.Text =  ds.Tables[0].Rows[0]["empEName"].ToString();				
				empInfo.Text = empNumber.Text + '-' + empName.Text;
				strTripUserId = empNumber.Text; //簽名檔用
				
				deptId.Text = ds.Tables[0].Rows[0]["deptId"].ToString();
				deptName.Text = ds.Tables[0].Rows[0]["deptName"].ToString();
				deptInfo.Text = deptId.Text + '-' + deptName.Text;
			    checkBy.Text = ds.Tables[0].Rows[0]["checkBy"].ToString();
				if (checkBy.Text.Equals(""))
				{
					checkBy.Text = " - ";
				}
				
				payeeBy.Text = ds.Tables[0].Rows[0]["payeeBy"].ToString();
				if (payeeBy.Text.Equals(""))
				{
				  payeeBy.Text = " ____________";
				}
				totalAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
				otherDesc.Text = ds.Tables[0].Rows[0]["FinDesc"].ToString();
				if (otherDesc.Text.Equals(""))
				{
				  otherDesc.Text = " -- ";
				}
				CompanyCode.Text = ds.Tables[0].Rows[0]["OrgName"].ToString();
            }
			
			//顯示差旅費報銷明細清單
			DataSet ds1 = null;
			sql = " select  ROW_NUMBER() OVER(ORDER BY UserID,TripDate) AS rownum,UserID+'-'+ UserName empData, TripDate,  StartTime+'~'+EndTime as TripTime, TrafficFee, EatFee, ParkingFee "; 
			sql += " , OtherFee, EtagFee, OilFee, TripSite, StartMileage, EndMileage, MileageSum, StartTime, EndTime, OriTripFormGUID from SmpTripBillingDetail a ";
			sql += " where HeaderGUID='" + strHeaderGUID + "' ";
			sql += " order by UserID,  TripDate";			
            ds1 = engine.getDataSet(sql, "TEMP");
			
			// Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow detailRec;
 
            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("No", typeof(string)));
			dt.Columns.Add(new DataColumn("工號-姓名 ", typeof(string)));            
            dt.Columns.Add(new DataColumn("出差日", typeof(string)));
			dt.Columns.Add(new DataColumn("時間起~訖", typeof(string)));
			dt.Columns.Add(new DataColumn("去公里", typeof(string)));
			dt.Columns.Add(new DataColumn("回公里", typeof(string)));
			dt.Columns.Add(new DataColumn("里程數", typeof(string)));
            dt.Columns.Add(new DataColumn("油資", typeof(string)));
			dt.Columns.Add(new DataColumn("車資", typeof(string)));
			dt.Columns.Add(new DataColumn("繕雜費", typeof(string)));
			dt.Columns.Add(new DataColumn("停車費", typeof(string)));
            dt.Columns.Add(new DataColumn("ETC費用", typeof(string)));
            dt.Columns.Add(new DataColumn("其他費用", typeof(string)));
			dt.Columns.Add(new DataColumn("出差地點", typeof(string)));

            int drows = ds1.Tables[0].Rows.Count;
            string[][] result = new string[drows][];
            for (int i = 0; i < drows; i++)
            {
                detailRec = dt.NewRow();
                detailRec[0] = ds1.Tables[0].Rows[i]["rownum"].ToString();
                detailRec[1] = ds1.Tables[0].Rows[i]["empData"].ToString();
                detailRec[2] = ds1.Tables[0].Rows[i]["TripDate"].ToString();
				detailRec[3] = ds1.Tables[0].Rows[i]["TripTime"].ToString();
				detailRec[4] = ds1.Tables[0].Rows[i]["StartMileage"].ToString();
				detailRec[5] = ds1.Tables[0].Rows[i]["EndMileage"].ToString();
				detailRec[6] = ds1.Tables[0].Rows[i]["MileageSum"].ToString();
                detailRec[7] = ds1.Tables[0].Rows[i]["OilFee"].ToString();
                detailRec[8] = ds1.Tables[0].Rows[i]["TrafficFee"].ToString();
                detailRec[9] = ds1.Tables[0].Rows[i]["EatFee"].ToString();
				detailRec[10] = ds1.Tables[0].Rows[i]["ParkingFee"].ToString();
                detailRec[11] = ds1.Tables[0].Rows[i]["EtagFee"].ToString();
                detailRec[12] = ds1.Tables[0].Rows[i]["OtherFee"].ToString();
                detailRec[13] = ds1.Tables[0].Rows[i]["TripSite"].ToString();
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
				sql = sql + "from NaNa.dbo.WorkItem wi, NaNa.dbo.ProcessInstance pi, WebFormPT.dbo.SMWYAAA ya, NaNa.dbo.Users ur , WebFormPT.dbo.FILEITEM fi  , SmpTripBilling stb ";
				sql = sql + "where wi.contextOID=pi.contextOID ";
				sql = sql + "and pi.serialNumber=ya.SMWYAAA005 and ur.OID = wi.performerOID and fi.LEVEL1 = ur.id ";
				sql = sql + "and wi.workItemName in ( '審核人','直屬主管','審核人','董事長','處級','處級主管','部級','總經理','部門主管','總經理室') ";
				sql = sql + "and stb.GUID = ya.SMWYAAA019 ";
				//sql = sql + "and  ya.SMWYAAA006  like = '%" + Utility.filter(strSheetNo) + "%'  ";
				//sql = sql + "and  substring(SMWYAAA002,1,7) = 'SPAD010'  "
				sql = sql + "and stb.SheetNo='" + Utility.filter(strSheetNo) + "'  ";
				ds = engine.getDataSet(sql, "TEMP");
				int rows = ds.Tables[0].Rows.Count;
				//string[][] result = new string[rows][];
				//sw.WriteLine("sql =>" + sql);
				
	           
			    for (int i = 0; i < rows; i++)
	            {
					//dr = dt.NewRow();
					imgSerial = ds.Tables[0].Rows[i]["ROWID"].ToString();
					imgUserNo = ds.Tables[0].Rows[i]["userId"].ToString();
					signItemName = ds.Tables[0].Rows[i]["workItemName"].ToString();
					signTime =  ds.Tables[0].Rows[i]["completedTime"].ToString();
					signFileName = ds.Tables[0].Rows[i]["imgName"].ToString();
					//sw.WriteLine("imgSerial =>" + imgSerial);
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
						//sw.WriteLine("Img1URL =>" + url +imgUserNo + "/" + signFileName);
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