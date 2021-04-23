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

public partial class Program_SCQ_Form_EH010301_PrintPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        //AbstractEngine erpEngine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
			//sw.WriteLine("in page load")'
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            //string connStr = sp.getParam("WorkFlowERPDB");
            //erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
            
            //string strUserId = Request["originatorId"];
            string strUserId = (string)Session["UserId"];
			string strSheetNo = (string)Session["EH010301_SheetNo"] ;
            if(Session["EH010301_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["EH010301_SheetNo"];
            }
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strStartDate = "";
            string strEndDate = "";		
            string sql = null;
			
			//string strAttendDate = Request["AttendDate"];
			
            DataSet ds = null;				

            //顯示使用者資訊
			sql = "select Subject, SheetNo, u.empNumber, u.empName, u.empEName, deptId, deptName ";
			sql = sql + " , (select userName from Users where OID=CheckByGUID or OID is null ) checkBy ";
 			sql = sql + ", TripDate, StartTime, EndTime, IsTripFee, TripSite, Notes, TrafficFee, EatFee, ParkingFee, OtherFee ";
			sql = sql + ", MeetingMinute, StartMileage, EndMileage, MileageSum, EtagFee, MileageSum*6 'PayeeFee'  ";
 			sql = sql + ", case orgId when 'SMP' then '新普科技' else '中普科技' end as OrgName ";
			sql = sql + "from SmpTripForm a, EmployeeInfo u ";
			sql = sql + "where SheetNo='" + Utility.filter(strSheetNo) + "'  ";
			//sql = sql + "where SheetNo='SPAD00900000064' ";
			sql = sql + "and a.OriginatorGUID=u.empGUID ";

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
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
					checkBy.Text = " -- ";
				}
				TripDate.Text = ds.Tables[0].Rows[0]["TripDate"].ToString();
				StartTime.Text = ds.Tables[0].Rows[0]["StartTime"].ToString();
				EndTime.Text = ds.Tables[0].Rows[0]["EndTime"].ToString();
				
				IsTripFee.Text =  ds.Tables[0].Rows[0]["IsTripFee"].ToString();
				if (IsTripFee.Text.Equals("Y"))
				{
				  IsTripFee.Text = "Y - 需請出差費用 ";
				}
				else
				{
				  IsTripFee.Text = "N - 不申請出差費用 ";
				}
				
				TripSite.Text = ds.Tables[0].Rows[0]["TripSite"].ToString();
				if (TripSite.Text.Equals(""))
				{
					TripSite.Text = " -- ";
				}

                Notes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
				if (Notes.Text.Equals(""))
				{
					Notes.Text = " -- ";
				}
				TrafficFee.Text = ds.Tables[0].Rows[0]["TrafficFee"].ToString();
				if (TrafficFee.Text.Equals(""))
				{
					TrafficFee.Text = " -- ";
				}
				EatFee.Text = ds.Tables[0].Rows[0]["EatFee"].ToString();
				if (EatFee.Text.Equals(""))
				{
					EatFee.Text = " -- ";
				}
				ParkingFee.Text =  ds.Tables[0].Rows[0]["ParkingFee"].ToString();
				if (ParkingFee.Text.Equals(""))
				{
					ParkingFee.Text = " -- ";
				}
                OtherFee.Text = ds.Tables[0].Rows[0]["OtherFee"].ToString();
				if (OtherFee.Text.Equals(""))
				{
					OtherFee.Text = " -- ";
				}
				OrgName.Text = ds.Tables[0].Rows[0]["OrgName"].ToString();
				if (OrgName.Text.Equals(""))
				{
					OrgName.Text = " -- ";
				}
				
				StartMileage.Text = ds.Tables[0].Rows[0]["StartMileage"].ToString();
				if (StartMileage.Text.Equals(""))
				{
					StartMileage.Text = " -- ";
				}
				EndMileage.Text = ds.Tables[0].Rows[0]["EndMileage"].ToString();
				if (EndMileage.Text.Equals(""))
				{
					EndMileage.Text = " -- ";
				}
				MileageSum.Text =  ds.Tables[0].Rows[0]["MileageSum"].ToString();
				if (MileageSum.Text.Equals(""))
				{
					MileageSum.Text = " -- ";
				}
                EtagFee.Text = ds.Tables[0].Rows[0]["EtagFee"].ToString();
				if (EtagFee.Text.Equals(""))
				{
					EtagFee.Text = " -- ";
				}
				PayeeFee.Text = ds.Tables[0].Rows[0]["PayeeFee"].ToString();
				if (PayeeFee.Text.Equals(""))
				{
					PayeeFee.Text = " -- ";
				}

            }
			
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
			sql = sql + "from NaNa.dbo.WorkItem wi, NaNa.dbo.ProcessInstance pi, WebFormPT.dbo.SMWYAAA ya, NaNa.dbo.Users ur , WebFormPT.dbo.FILEITEM fi   , SmpTripForm stf ";
			sql = sql + "where wi.contextOID=pi.contextOID ";
			sql = sql + "and pi.serialNumber=ya.SMWYAAA005 and ur.OID = wi.performerOID and fi.LEVEL1 = ur.id ";
			sql = sql + "and wi.workItemName in ('ChargeSTCS','ChargeSCQ','審核人','直屬主管','審核人','董事長','處級','處級主管','部級') ";
			sql = sql + "and stf.GUID = ya.SMWYAAA019 ";
			sql = sql + "and stf.SheetNo='" + Utility.filter(strSheetNo) + "'  ";
			//sql = sql + "and substring(SMWYAAA002,1,7) in ('SPAD009') ";
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

			//取得簽名檔與簽核時間
			//董事長
			/*
			Img6.ImageUrl = "http://192.168.2.226/ECP/Personal/1356/1356.gif";
			Img6.AlternateText = "董事長";
			Img6Time.Text = "2013/10/06 00:00:00";
			
			//string url = getPersonalImage(engine,strTripUserId);
			Img5.ImageUrl = "http://192.168.2.226/ECP/Personal/3787/3787.gif";
			Img5.AlternateText = "審核人5";
			Img5Time.Text = "2013/10/05 00:00:00";
			
			Img4.ImageUrl = "http://192.168.2.226/ECP/Personal/2556/2556.gif";
			Img4.AlternateText = "審核人4";
			Img4Time.Text = "2013/10/04 00:00:00";
			
			Img3.ImageUrl = "http://192.168.2.226/ECP/Personal/4074/4074.jpg";
			Img3.AlternateText = "審核人3";
			Img3Time.Text = "2013/10/03 00:00:00";
			
			Img2.ImageUrl = "http://192.168.2.226/ECP/Personal/3842/3842.gif";
			Img2.AlternateText = "審核人2";
			Img2Time.Text = "2013/10/02 00:00:00";
			*/
			//Img1.ImageUrl = "http://192.168.2.226/ECP/Personal/3992/3992.jpg";
			//Img1.AlternateText = "審核人1";
			//Img1Time.Text = "2013/10/01 00:00:00";
            
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