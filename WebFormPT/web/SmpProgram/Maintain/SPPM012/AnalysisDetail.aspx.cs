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
using smp.pms.utility;

public partial class SmpProgram_Form_SPPM006_AnalysisDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        try
            {      
            
            //string connectString = (string)Session["connectString"];
            string connectString ="USER=sa;PWD=SMPecp1;SERVER=10.3.11.83;DATABASE=WebFormPT";
            //string engineType = (string)Session["engineType"];
            string engineType = "SQL";
            string strUserGUID = Request.QueryString["strUserGUID"].ToString();
            string strAssessmentPlanName = Server.UrlDecode(Request.QueryString["strAssessmentPlanName"].ToString());
            string AchievementLevel = Server.UrlDecode(Request.QueryString["AchievementLevel"].ToString());
            string ZD = Request.QueryString["ZD"].ToString();
            string sql = null;
            DataSet ds = null;
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string lblzd = "";
            if (ZD.Equals("2"))
            {
                lblzd="二職等";
            }
            else if (ZD.Equals("5"))
            {
                lblzd = "三/四/五職等";
            }
            else if (ZD.Equals("6"))
            {
                lblzd = "六職等及以上";
            }
            Label1.Text = "等級" + "[" + AchievementLevel + "] " + lblzd + "人員明細";

            if (AchievementLevel.Equals("未評核"))
            {
                AchievementLevel="";
            }
            //統計表                 
            sql = "select a.UserGUID '工號',b.empName '姓名',b.titleName '職稱',b.ZD '職等',b.ResponsibilityFlag '責任制',b.deptName '部門' from dbo.SmpPmUserAchievement  as a left join SmpHrEmployeeInfoV as b " +
                  " on upper(a.UserGUID)=upper(b.empNumber) " +
                  " where  AssessmentPlanGUID in (select GUID from dbo.SmpPmAssessmentPlan  where AssessmentName='" + strAssessmentPlanName + "') and AssessUserGUID ='" + strUserGUID + "' "+
                  " and AchievementLevel='" + AchievementLevel + "' and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "' ";
                
            if (ZD.Equals("2"))
            {
                sql = sql + " and b.ZD='二職等'";
            }
            else if (ZD.Equals("5"))
            {
                sql = sql + " and b.ZD in ('三職等','四職等','五職等')";
            }
            else if (ZD.Equals("6"))
            {
                sql = sql + " and b.ZD not in ('二職等','三職等','四職等','五職等')";
            }

            ds = engine.getDataSet(sql, "TEMP");

            gvAchievementList.DataSource = ds;           
            gvAchievementList.DataBind();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (engine != null) engine.close();    
        }
    }

    /// <summary>
    /// chang alignment
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAchievementList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right; //人數
            //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right; //比重
        }
    }
}