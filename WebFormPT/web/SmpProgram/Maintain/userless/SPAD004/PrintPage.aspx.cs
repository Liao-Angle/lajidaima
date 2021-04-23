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

public partial class SmpProgram_Maintain_SPAD004_PrintPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        //AbstractEngine erpEngine = null;
        //System.IO.StreamWriter sw = null;
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
			string strSheetNo = (string)Session["SPAD004_SheetNo"] ;

            if (Session["SPAD004M_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["SPAD004M_SheetNo"];
            }
						
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strHeaderGUID = "";
            string sql = null;
			DataSet ds = null;
            
            //顯示使用者資訊
            sql = "select  a.GUID, SheetNo, a.CompanyCode, u.empNumber, u.empName, u.empEName, funcName, ou.id deptId, ou.organizationUnitName as deptName ";
			sql = sql + "	 ,BillingDate, Description, TotalAmount, case u.orgId when 'SMP' then '新普科技' else '中普科技' end as OrgName  ";
            sql = sql + "from SmpTripBillSummary a, EmployeeInfo u, OrganizationUnit ou ";
            sql = sql + " where SheetNo='" + Utility.filter(strSheetNo) + "'  and a.OriginatorGUID=u.empGUID and ou.OID=a.DeptGUID ";	
			
			ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
			    strHeaderGUID =  ds.Tables[0].Rows[0]["GUID"].ToString();
                //Subject.Text =  ds.Tables[0].Rows[0]["Subject"].ToString();
                SheetNo.Text = ds.Tables[0].Rows[0]["SheetNo"].ToString();
				empNumber.Text = ds.Tables[0].Rows[0]["empNumber"].ToString();				
				empName.Text = ds.Tables[0].Rows[0]["empName"].ToString();
				empEName.Text =  ds.Tables[0].Rows[0]["empEName"].ToString();				
				empInfo.Text = empNumber.Text + '-' + empName.Text;
				strTripUserId = empNumber.Text; //簽名檔用
				
				deptId.Text = ds.Tables[0].Rows[0]["deptId"].ToString();
				deptName.Text = ds.Tables[0].Rows[0]["deptName"].ToString();
				deptInfo.Text = deptId.Text + '-' + deptName.Text;
			    
				
				totalAmount.Text = ds.Tables[0].Rows[0]["TotalAmount"].ToString();
                otherDesc.Text = ds.Tables[0].Rows[0]["Description"].ToString();
				if (otherDesc.Text.Equals(""))
				{
				  otherDesc.Text = " -- ";
				}
				CompanyCode.Text = ds.Tables[0].Rows[0]["OrgName"].ToString();
            }
			
			//顯示差旅費報銷明細清單
			DataSet ds1 = null;

            sql = "select  ROW_NUMBER() OVER(ORDER BY u.empNumber) AS rownum, u.empNumber+'-'+u.empName empData, deptId+'-'+deptName deptData ";
            sql += ", Sum(cast(OilFee as integer)) oilttl, sum(cast(TrafficFee as integer)) trafficttl, sum(cast(EatFee as integer)) eatttl ";
            sql += ", sum(cast(ParkingFee as integer)) parkttl, Sum(cast(EtagFee as integer)) etcttl, sum(cast(OtherFee as integer)) otherttl ";
            sql += ", Sum(cast(OilFee as integer)+cast(TrafficFee as integer)+cast(EatFee as integer)+cast(ParkingFee as integer)+cast(EtagFee as integer)+cast(OtherFee as integer)) personTotal ";
			sql += "from SmpTripBillSumDetail a ,  EmployeeInfo u ";
            sql += "where SummaryGUID='" + strHeaderGUID + "'  and a.UserGUID=u.empGUID ";
			sql += "group by u.empNumber, u.empName, deptId, deptName ";
            sql += " order by personTotal desc, deptData , empData ";		
            ds1 = engine.getDataSet(sql, "TEMP");
			
			// Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow detailRec;
 
            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("No", typeof(string)));
			dt.Columns.Add(new DataColumn("工號-姓名 ", typeof(string)));            
            dt.Columns.Add(new DataColumn("部門代號-名稱", typeof(string)));
            dt.Columns.Add(new DataColumn("油資加總", typeof(string)));
            dt.Columns.Add(new DataColumn("車資加總", typeof(string)));
            dt.Columns.Add(new DataColumn("繕雜費加總", typeof(string)));
            dt.Columns.Add(new DataColumn("停車費加總", typeof(string)));
            dt.Columns.Add(new DataColumn("ETC加總", typeof(string)));
            dt.Columns.Add(new DataColumn("其他費用加總", typeof(string)));
			dt.Columns.Add(new DataColumn("出差總額", typeof(string)));
			
            int drows = ds1.Tables[0].Rows.Count;
            string[][] result = new string[drows][];
            for (int i = 0; i < drows; i++)
            {
                detailRec = dt.NewRow();
                detailRec[0] = ds1.Tables[0].Rows[i]["rownum"].ToString();
                detailRec[1] = ds1.Tables[0].Rows[i]["empData"].ToString();
                detailRec[2] = ds1.Tables[0].Rows[i]["deptData"].ToString();
                detailRec[3] = ds1.Tables[0].Rows[i]["oilttl"].ToString();
                detailRec[4] = ds1.Tables[0].Rows[i]["trafficttl"].ToString();
                detailRec[5] = ds1.Tables[0].Rows[i]["eatttl"].ToString();
                detailRec[6] = ds1.Tables[0].Rows[i]["parkttl"].ToString();
                detailRec[7] = ds1.Tables[0].Rows[i]["etcttl"].ToString();
                detailRec[8] = ds1.Tables[0].Rows[i]["otherttl"].ToString();
                detailRec[9] = ds1.Tables[0].Rows[i]["personTotal"].ToString();
                dt.Rows.Add(detailRec);
            }
            DataView dv = new DataView(dt);

            tripFeeSummary.DataSource = dv;
            tripFeeSummary.DataBind();
			
			try
			{
				string urlNa = "http://192.168.2.137/ECP/Personal/na.jpg";
				
				//預設值
                Img6.ImageUrl = urlNa;
                Img5.ImageUrl = urlNa;
                Img4.ImageUrl = urlNa;
                Img3.ImageUrl = urlNa;
                Img2.ImageUrl = urlNa;
                signUser1.Text = empName.Text;
				
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
            //if (sw != null) sw.Close();
        }
    }
		
}