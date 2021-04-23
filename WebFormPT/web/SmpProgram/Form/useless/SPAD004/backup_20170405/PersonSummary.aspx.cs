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

public partial class SmpProgram_Form_SPAD004_PersonSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam("WorkFlowERPDB");
            erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
            
            //string strUserId = Request["originatorId"];
            string strUserId = (string)Session["UserID"];
			string strSheetNo = (string)Session["SPAD004_SheetNo"] ;
            if(Session["SPAD004_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["SPAD004_SheetNo"];
            }
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
            //string strStartDate = Convert.ToInt32(strYear) - 1 + "1226";
			string strStartDate = "";
            string strEndDate = "";

            
			
            string sql = null;
			
			//string strAttendDate = Request["AttendDate"];
			
            DataSet ds = null;
					

            //顯示使用者資訊
            //UserId.Text = strUserId;
            //sql = "select u.userName, o.id, o.organizationUnitName from Functions f inner join OrganizationUnit o on organizationUnitOID=o.OID inner join Users u on  f.occupantOID = u.OID where u.id='" + Utility.filter(strUserId) + "'  and isMain='1'";
			sql = "select IdNumber, Birthday,Beneficiary ,Relationship from SmpForeignTrvl where SheetNo='" + Utility.filter(strSheetNo) + "' ";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                IdNumber.Text =  ds.Tables[0].Rows[0]["IdNumber"].ToString();
                Birthday.Text = ds.Tables[0].Rows[0]["Birthday"].ToString();
				Beneficiary.Text = ds.Tables[0].Rows[0]["Beneficiary"].ToString();
				Relationship.Text = ds.Tables[0].Rows[0]["Relationship"].ToString();
            }
            
            
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