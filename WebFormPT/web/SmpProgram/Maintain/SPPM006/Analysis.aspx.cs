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

public partial class SmpProgram_Form_SPPM006_Analysis : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        try
        {            
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            string strUserGUID = (string)Session["UserId"];
            string strAssessmentPlanGUID = (string)Session["SPPM006_AssessmentPlanGUID"];
            string whereClause = " AssessmentPlanGUID = '" + strAssessmentPlanGUID + "' and AssessUserGUID ='" + strUserGUID + "' " +
                                 " and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "' ";
            string sql = null;
            DataSet ds = null;
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            //人數   
            Total.Text = "0";
            Complete.Text = "0";
            UnComplete.Text = "0";
          
            sql = "select count(AchievementLevel),isnull(sum(case when  AchievementLevel <>'' then 1 else 0 end ),0) complete," +
                         "isnull(sum(case when  AchievementLevel <>'' then 0  else 1  end ),0) uncomplete " +
                         "from SmpPmUserAchievement a " +
                         "join SmpHrEmployeeInfoV b on b.empNumber=a.UserGUID where " + whereClause;
            
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Total.Text = ds.Tables[0].Rows[0][0].ToString();
                Complete.Text = ds.Tables[0].Rows[0][1].ToString();
                UnComplete.Text = ds.Tables[0].Rows[0][2].ToString();
            }

            //統計表                 
            sql = "select a.AchievementLevel, Description ,COUNT(b.GUID) NOP "+
                  "from SmpPmAchievementLevel a left join SmpPmUserAchievement b on a.AchievementLevel =b.AchievementLevel and "+
                  whereClause + 
                  "GROUP by  a.AchievementLevel, a.Description "+
                  "union " +
                  "select '未評核' AchievementLevel,'未評核' Description, COUNT(*) NOP from SmpPmUserAchievement a where " +
                   whereClause + " and a.AchievementLevel ='' " +
                  "order by 1 ";
            
            ds = engine.getDataSet(sql, "TEMP");
            
            DataTable dt = new DataTable();
            DataRow dr;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("評比 ", typeof(string)));
            dt.Columns.Add(new DataColumn("分數", typeof(string)));
            dt.Columns.Add(new DataColumn("人數", typeof(double)));
            dt.Columns.Add(new DataColumn("比重", typeof(string))); 
           
            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];

            for (int i = 0; i < rows; i++)
            {
                dr = dt.NewRow();
                dr[0] = ds.Tables[0].Rows[i]["AchievementLevel"].ToString();
                dr[1] = ds.Tables[0].Rows[i]["Description"].ToString();
                dr[2] = ds.Tables[0].Rows[i]["NOP"].ToString();

                double percentage = 0;
                if (!Total.Text.Equals('0'))
                    percentage = Convert.ToDouble(dr[2]) / Convert.ToDouble(Total.Text);
                dr[3] = String.Format("{0:P}", percentage);
                
                dt.Rows.Add(dr);
            }
            DataView dv = new DataView(dt);
            gvAchievementList.DataSource = dv;           
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
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right; //人數
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right; //比重
        }
    }
}