using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms.DataGridViewColumn;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;

public partial class SmpProgram_Form_SPTS001_PrintPage : System.Web.UI.Page
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
			string strSheetNo = "";
			strSheetNo = (string)Session["SPTS001_SheetNo"] ;
            if(Session["SPTS001_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["SPTS001_SheetNo"];
            }
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strHeaderGUID = "";
            string strSubjectNo = "";	
			string strSubjectName = "";
			string strStartDate = "";			
			string strStartTime = "";	
			string strEndTime = "";	
			string strOrgName = "";
			string strImgURL = "http://192.168.2.137/ECP/Images/";
            string sql = null;
						
            DataSet ds = null;				
			//取得單頭資料			
			sql = "select  C.CompanyCode, C.GUID, DB.SubjectNo, DB.SubjectName, C.StartDate, C.StartTime, C.EndTime  from SmpTSInHouseForm C ";
			sql = sql + "  JOIN SmpTSSchDetail S on C.SchDetailGUID = S.GUID ";
			sql = sql + "  JOIN SmpTSSubjectDetail DB on S.SubjectDetailGUID = DB.GUID ";
			sql = sql + " where C.SheetNo='"+ strSheetNo +"' ";
			//sql = sql + " where C.GUID='2916afd4-e318-4b16-aee5-ade3ca1dc4e9' ";
			ds = engine.getDataSet(sql, "TEMP");
			if (ds.Tables[0].Rows.Count > 0)
            {
                strHeaderGUID = ds.Tables[0].Rows[0]["GUID"].ToString();
				strSubjectNo = ds.Tables[0].Rows[0]["SubjectNo"].ToString();
				strSubjectName = ds.Tables[0].Rows[0]["SubjectName"].ToString();
				strStartDate = ds.Tables[0].Rows[0]["StartDate"].ToString();
				strStartTime = ds.Tables[0].Rows[0]["StartTime"].ToString();
				strEndTime = ds.Tables[0].Rows[0]["EndTime"].ToString();
				strOrgName = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                SubjectName.Text = strSubjectName;
				TrainDate.Text = strStartDate;
				TrainTime.Text = strStartTime + " ~ " + strEndTime;
            }
			
			if (strOrgName.Equals("SMP")) {
				ImgLogo.ImageUrl = strImgURL +"smplogo_form.jpg";
			}else{
				ImgLogo.ImageUrl = strImgURL +"tplogo_form.jpg";
			} 

			//顯示教育訓練學員明細
            sql = " select cast(CheckField2 as INT) as SerialNo, ISNULL( id,' ') as EmpNumber , ISNULL(userName,' ') EmpName , ' ' userSign  from SmpFlowInspect fi ";
			sql = sql + " left  join ( ";
			sql = sql + " SELECT ROW_NUMBER() OVER(ORDER BY id) AS Row, u.id, u.userName ";
 			sql = sql + " from SmpTSInHouseTrainee t ";
			sql = sql + " join Users u on u.OID=t.EmployeeGUID ";
			sql = sql + " where InHouseFormGUID='"+ strHeaderGUID +"' ";
			sql = sql + " ) aa on aa.Row=fi.CheckField2 ";
 			sql = sql + " where FormId='SPTS001' and CheckField1='PrintSerial' and Status='Y'  ";
			sql = sql + " order by 1 ";
            ds = engine.getDataSet(sql, "TEMP");

            // Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow dr;	
 			dt.Columns.Add("序　號");
            dt.Columns.Add("工　　號  ");
            dt.Columns.Add("學 員 姓 名  ");
            dt.Columns.Add("簽　　　 　名  (中文) ");
			dt.Columns.Add(" 序　號");
            dt.Columns.Add("工　　號 ");
            dt.Columns.Add("學 員 姓 名 ");
            dt.Columns.Add("簽　　　 　名  (中文)");

            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];
            for (int i = 0; i < 20; i++)
            {
                dr = dt.NewRow();
                dr[0] = ds.Tables[0].Rows[i]["SerialNo"].ToString();
                dr[1] = ds.Tables[0].Rows[i]["EmpNumber"].ToString();
                dr[2] = ds.Tables[0].Rows[i]["EmpName"].ToString();
                dr[3] = ds.Tables[0].Rows[i]["userSign"].ToString();
				dr[4] = ds.Tables[0].Rows[i+20]["SerialNo"].ToString();
                dr[5] = ds.Tables[0].Rows[i+20]["EmpNumber"].ToString();
                dr[6] = ds.Tables[0].Rows[i+20]["EmpName"].ToString();
                dr[7] = ds.Tables[0].Rows[i+20]["userSign"].ToString();
				
                dt.Rows.Add(dr);
            }
            DataView dv = new DataView(dt);
			TraineeList.DataSource = dv;
			TraineeList.DataBind();		
			TraineeList.AutoGenerateColumns=true;
			
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (engine != null) engine.close();
            if (sw != null) sw.Close();
        }
    }
	
	
	
}