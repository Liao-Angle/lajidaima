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

public partial class SmpProgram_Form_SPAD002_AttendSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\SPAD002.log", true, System.Text.Encoding.Default);
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
			string strCompanyId = (string)Session["SPAD002_CompanyId"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam(strCompanyId+"WorkFlowERPDB");
            erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
            
            //string strUserId = Request["originatorId"];
            string strUserId = (string)Session["UserID"];
			
			//sw.WriteLine("strUserId:" + strUserId);
			
			string strType = "";
            if(Session["SPAD002_OriginatorId"] != null) 
            {
                strUserId = (string)Session["SPAD002_OriginatorId"];
            }
            string strNow = DateTimeUtility.getSystemTime2(null);
			// 本月的第一天           
            DateTime firstDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            //本月的最后一天
            string lastDate = firstDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            DateTime.Now.Day.ToString();
            DateTime.Now.Year.ToString();
			
			string month = DateTime.Now.AddMonths(-1).ToString("yyyyMMdd");
            string lastMonth = month.Substring(0, 6) + "26";
			
			string strStartDate = "";
            string strEndDate = "";
			string strSessionSD = "";
			string strSessionED = "";
			strSessionSD = (string)Session["SPAD002_StartDate"];
			strSessionED = (string)Session["SPAD002_EndDate"];
			
			//sw.WriteLine("strStartDate:" + strStartDate);
			//sw.WriteLine("strEndDate:" + strEndDate);

            if(Session["SPAD002_StartDate"] != null && !Session["SPAD002_StartDate"].Equals("")) 
            {
                strStartDate = (string)Session["SPAD002_StartDate"];
            }
			else
			{
				strStartDate = lastMonth;
			}
            if(Session["SPAD002_EndDate"] != null && !Session["SPAD002_EndDate"].Equals(""))
            {
                strEndDate = (string)Session["SPAD002_EndDate"];
			}
			else
			{
				strEndDate = lastDate;
			}
			
			//sw.WriteLine(" Session StartDate:" + (string)Session["SPAD002_StartDate"]);
			
            string sql = null;
			
			//sw.WriteLine("strStartDate:" + strStartDate);
			//sw.WriteLine("strEndDate:" + strEndDate);
			
			//string strAttendDate = Request["AttendDate"];
			
            DataSet ds = null;
					

            //顯示使用者資訊
            UserId.Text = strUserId;
            sql = "select u.userName, o.id, o.organizationUnitName from Functions f inner join OrganizationUnit o on organizationUnitOID=o.OID inner join Users u on  f.occupantOID = u.OID where u.id='" + Utility.filter(strUserId) + "'  and isMain='1'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserName.Text =  ds.Tables[0].Rows[0]["userName"].ToString();
                OrgUnitName.Text = ds.Tables[0].Rows[0]["organizationUnitName"].ToString();
            }
            
            //顯示出勤明細
            //sql = "select TF002,TF003,MK002,MC002,TF005,TF006,TF008,TF010,TF011 from PALTF,PALMK,PALMC where TF001='" + strUserId + "' and TF011='Y' and TF002 >='" + strStartDate + "' and PALTF.TF014=PALMK.MK001 and PALTF.TF004=PALMC.MC001 order by PALTF.TF002,PALTF.TF003";
			sql = "select distinct MC001, MC002, MC003, MC901 from AMSMC where MC001='" + strUserId + "' and MC002>='" + strStartDate + "' and MC002<='" + strEndDate + "'  ORDER BY MC002 DESC, MC003 DESC ";
						
			if(!strSessionSD.Equals("") && !strSessionED.Equals(""))
            { 
                sql = "select * from AMSMC where MC001='" + strUserId + "' and MC002>='"+strStartDate+"' and MC002<='"+strEndDate+"'   ";
            }
			if(!strSessionSD.Equals("") && strSessionED.Equals(""))			
            { 
                sql = "select * from AMSMC where MC001='" + strUserId + "' and  MC002='"+strStartDate+"'  ";
            }
			if(strSessionSD.Equals("") && !strSessionED.Equals(""))	
            { 
                sql = "select * from AMSMC where MC001='" + strUserId + "' and  MC002='"+strEndDate+"'  ";
            }
			//if (strEndDate.Equals("") && strStartDate.Equals(""))
            //{ 
            //    sql = "select * from AMSMC where MC001='" + strUserId + "' and ((MC002>='"+strStartDate+"' or MC002 is null) or (MC002<='"+strEndDate+"' or MC002 is null)) ";
            //}
			
			//sw.WriteLine("!! sql:" + sql);
			
            ds = erpEngine.getDataSet(sql, "TEMP");

            // Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow dr;
 
            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("出勤日期 ", typeof(string)));
            dt.Columns.Add(new DataColumn("出勤時間", typeof(string)));
            dt.Columns.Add(new DataColumn("備註(出勤修正單號)", typeof(string)));
            //dt.Columns.Add(new DataColumn("假別", typeof(string)));
            //dt.Columns.Add(new DataColumn("請假起迄時間", typeof(string)));
            //dt.Columns.Add(new DataColumn("請假時數", typeof(string)));
            //dt.Columns.Add(new DataColumn("備註", typeof(string)));
            //dt.Columns.Add(new DataColumn("確認碼", typeof(string)));

            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; i++)
            {
                dr = dt.NewRow();
                dr[0] = ds.Tables[0].Rows[i]["MC002"].ToString();
                dr[1] = ds.Tables[0].Rows[i]["MC003"].ToString();
                dr[2] = ds.Tables[0].Rows[i]["MC901"].ToString();
                //dr[3] = ds.Tables[0].Rows[i]["MC002"].ToString();
                //dr[4] = ds.Tables[0].Rows[i]["TF005"].ToString() + "~" + ds.Tables[0].Rows[i]["TF006"].ToString();
                //dr[5] = ds.Tables[0].Rows[i]["TF008"].ToString();
                //dr[6] = ds.Tables[0].Rows[i]["TF010"].ToString();
                //string confirmCode = ds.Tables[0].Rows[i]["TF011"].ToString();
                //string confirmName = "";
                //if(confirmCode.Equals("N")) {
                //    confirmName = "未確認";
                //} else if(confirmCode.Equals("Y")) {
                //    confirmName = "已確認";
                //} else if(confirmCode.Equals("V")) {
                //    confirmName = "已作廢";
                //}
                //dr[7] = confirmName;
                dt.Rows.Add(dr);
            }
            DataView dv = new DataView(dt);

            gvAttendSummary.DataSource = dv;
            gvAttendSummary.DataBind();
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
        finally
        {
            if (engine != null) engine.close();
            if (erpEngine != null) erpEngine.close();
            if (sw != null) sw.Close();
        }
    }
}