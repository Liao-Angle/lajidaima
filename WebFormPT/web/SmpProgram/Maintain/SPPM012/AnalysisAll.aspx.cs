using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;
using com.dsc.kernal.agent;
using WebServerProject;
using com.dsc.kernal.mail;
using smp.pms.utility;
using System.Text;
using System.IO;

public partial class SmpProgram_Maintain_SPPM011_AnalysisNew : BaseWebUI.WebFormBasePage
{
    #region 合并行(相同值)-普通列
    /// <summary>
    /// 合并行(普通列)
    /// </summary>
    /// <param name=“gv”>所对应的GridView对象</param>
    /// <param name=“columnIndex”>所对应要合并的列的索引</param>
    public static void UnitRow(GridView gv, int columnIndex)
    {
        int i;
        string lastType;
        int lastCell;
        if (gv.Rows.Count > 0)
        {
            lastType = gv.Rows[0].Cells[columnIndex].Text;
            gv.Rows[0].Cells[columnIndex].RowSpan = 1;
            lastCell = 0;
            for (i = 1; i < gv.Rows.Count; i++)
            {
                if (gv.Rows[i].Cells[columnIndex].Text == lastType)
                {
                    gv.Rows[i].Cells[columnIndex].Visible = false;
                    gv.Rows[lastCell].Cells[columnIndex].RowSpan++;
                }
                else
                {
                    lastType = gv.Rows[i].Cells[columnIndex].Text;
                    lastCell = i;
                    gv.Rows[i].Cells[columnIndex].RowSpan = 1;
                }
            }
        }
    }

    #endregion
 

    protected void gvAchievementScore_DataBound(object sender, EventArgs e)
    {
        UnitRow(gvAchievementScore, 0);
        UnitRow(gvAchievementScore, 1);
        UnitRow(gvAchievementScore, 2);
    }
    private DataTable getDt()
    {
        AbstractEngine engine;
        DataTable dt = new DataTable();
        try
        {

            lblplanGUID.Text = (string)Session["SPPM011_sddlAssessmentPlanGUID"];

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            string whereClause = " AssessmentPlanGUID in (" + SmpPmMaintainUtil.GetfilterNameplanGUID(lblplanGUID.Text) + ") and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "' ";
            string sql = null;
            DataSet ds = null;
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);


            sql = "select 未評核,A,B,C,D,E,RatioA A1,RatioB B1,RatioC C1,RatioD D1,RatioE E1,部門,n.userName '主管',AssessUserGUID '工號' from (select SUM(未評核)'未評核',sum(A)'A',sum(B)'B',sum(C)'C',sum(D)'D',sum(E)'E',部門,AssessUserGUID from (" +
                  "select 0 '未評核',sum(A)'A',sum(B)'B',sum(C)'C',sum(D)'D',sum(E)'E',部門,AssessUserGUID from(" +
                  "select b.AssessUserGUID,case when a.AchievementLevel='A' then 1 else 0 end 'A'," +
                  "case when a.AchievementLevel='B' then 1 else 0 end 'B'," +
                  "case when a.AchievementLevel='C' then 1 else 0 end 'C'," +
                  "case when a.AchievementLevel='D' then 1 else 0 end 'D'," +
                  "case when a.AchievementLevel='E' then 1 else 0 end 'E'," +
                  "c.deptId '部門'" +
                  " from SmpPmUserAchievement b left join SmpPmAchievementLevel a  on a.AchievementLevel =b.AchievementLevel " +
                  " left join dbo.SmpHrEmployeeInfoV as c on upper(b.UserGUID)=upper(c.empNumber) " +
                  " where  " + whereClause +
                  " )z group by z.AssessUserGUID,z.部門 " +
                  " union " +
                  "select sum(未評核),sum(A),sum(B),sum(C),sum(D),sum(E),部門,AssessUserGUID from ( select 1 '未評核',0 'A',0 'B',0 'C',0 'D',0 'E',c.deptId '部門', b.AssessUserGUID from SmpPmUserAchievement as b " +
                  " left join dbo.SmpHrEmployeeInfoV as c on upper(b.UserGUID)=upper(c.empNumber) " +
                  " where c.deptId is not null and " + whereClause + " and AchievementLevel ='' )t group by AssessUserGUID,部門)k group by AssessUserGUID,部門)m " +
                  " left join dbo.Users as n on upper(m.AssessUserGUID)=upper(n.id)"+
                  " left join dbo.SmpPmRatio as v on m.部門=v.PartNo order by n.userName,m.部門";


            ds = engine.getDataSet(sql, "TMP02");

    
            DataRow dr;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("工號", typeof(string)));
            dt.Columns.Add(new DataColumn("主管", typeof(string)));
            dt.Columns.Add(new DataColumn("部門", typeof(string)));
            dt.Columns.Add(new DataColumn("說明", typeof(string)));
            dt.Columns.Add(new DataColumn("A", typeof(string)));
            dt.Columns.Add(new DataColumn("B", typeof(string)));
            dt.Columns.Add(new DataColumn("C", typeof(string)));
            dt.Columns.Add(new DataColumn("D", typeof(string)));
            dt.Columns.Add(new DataColumn("E", typeof(string)));
            dt.Columns.Add(new DataColumn("未評核", typeof(string)));

            int rows = ds.Tables["TMP02"].Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                int total = 0;
                float result = 0;
                bool isFlag = int.TryParse(ds.Tables["TMP02"].Rows[i]["A"].ToString(), out total);
                result += total;

                isFlag = int.TryParse(ds.Tables["TMP02"].Rows[i]["B"].ToString(), out total);
                result += total;

                isFlag = int.TryParse(ds.Tables["TMP02"].Rows[i]["C"].ToString(), out total);
                result += total;

                isFlag = int.TryParse(ds.Tables["TMP02"].Rows[i]["D"].ToString(), out total);
                result += total;

                isFlag = int.TryParse(ds.Tables["TMP02"].Rows[i]["E"].ToString(), out total);
                result += total;

                isFlag = int.TryParse(ds.Tables["TMP02"].Rows[i]["未評核"].ToString(), out total);
                result += total;

                dr = dt.NewRow();
                dr["工號"] = ds.Tables["TMP02"].Rows[i]["工號"].ToString();
                dr["主管"] = ds.Tables["TMP02"].Rows[i]["主管"].ToString();
                dr["部門"] = ds.Tables["TMP02"].Rows[i]["部門"].ToString();
                dr["說明"] = "應占人數";

                if (result > 0)
                {
                    dr["A"] = ds.Tables["TMP02"].Rows[i]["A1"].ToString();
                    dr["B"] = ds.Tables["TMP02"].Rows[i]["B1"].ToString();
                    dr["C"] = ds.Tables["TMP02"].Rows[i]["C1"].ToString();
                    dr["D"] = ds.Tables["TMP02"].Rows[i]["D1"].ToString();
                    dr["E"] = ds.Tables["TMP02"].Rows[i]["E1"].ToString();
                }
                else
                {
                    dr["A"] = "";
                    dr["B"] = "";
                    dr["C"] = "";
                    dr["D"] = "";
                    dr["E"] = "";
                    dr["未評核"] = "";
                }



                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["工號"] = ds.Tables["TMP02"].Rows[i]["工號"].ToString();
                dr["主管"] = ds.Tables["TMP02"].Rows[i]["主管"].ToString();
                dr["部門"] = ds.Tables["TMP02"].Rows[i]["部門"].ToString();
                dr["說明"] = "實占人數";
                dr["A"] = ds.Tables["TMP02"].Rows[i]["A"].ToString();
                dr["B"] = ds.Tables["TMP02"].Rows[i]["B"].ToString();
                dr["C"] = ds.Tables["TMP02"].Rows[i]["C"].ToString();
                dr["D"] = ds.Tables["TMP02"].Rows[i]["D"].ToString();
                dr["E"] = ds.Tables["TMP02"].Rows[i]["E"].ToString();
                dr["未評核"] = ds.Tables["TMP02"].Rows[i]["未評核"].ToString();
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["工號"] = ds.Tables["TMP02"].Rows[i]["工號"].ToString();
                dr["主管"] = ds.Tables["TMP02"].Rows[i]["主管"].ToString();
                dr["部門"] = ds.Tables["TMP02"].Rows[i]["部門"].ToString();
                dr["說明"] = "實占比例";
                if (result > 0)
                {
                    float ty = 0;
                    isFlag = float.TryParse(ds.Tables["TMP02"].Rows[i]["A"].ToString(), out ty);
                    dr["A"] = Math.Round((ty / result) * 100.0, 1).ToString("0.0") + "%";
                    isFlag = float.TryParse(ds.Tables["TMP02"].Rows[i]["B"].ToString(), out ty);
                    dr["B"] = Math.Round((ty / result) * 100.0, 1).ToString("0.0") + "%";
                    isFlag = float.TryParse(ds.Tables["TMP02"].Rows[i]["C"].ToString(), out ty);
                    dr["C"] = Math.Round((ty / result) * 100.0, 1).ToString("0.0") + "%";
                    isFlag = float.TryParse(ds.Tables["TMP02"].Rows[i]["D"].ToString(), out ty);
                    dr["D"] = Math.Round((ty / result) * 100.0, 1).ToString("0.0") + "%";
                    isFlag = float.TryParse(ds.Tables["TMP02"].Rows[i]["E"].ToString(), out ty);
                    dr["E"] = Math.Round((ty / result) * 100.0, 1).ToString("0.0") + "%";
                    isFlag = float.TryParse(ds.Tables["TMP02"].Rows[i]["未評核"].ToString(), out ty);
                    dr["未評核"] = Math.Round((ty / result) * 100.0, 1).ToString("0.0") + "%";
                }
                else
                {
                    dr["A"] = "";
                    dr["B"] = "";
                    dr["C"] = "";
                    dr["D"] = "";
                    dr["E"] = "";
                    dr["未評核"] = "";
                }
                dt.Rows.Add(dr);


               

            }
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        engine.close();
        return dt;
    }
    protected void Page_Load(object sender, EventArgs e)
    {      
        try
        {
            gvAchievementScore.DataSource = getDt();
            gvAchievementScore.DataBind();
            gvAchievementScore.Columns[0].Visible = false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        lblplanGUID.Display = false;
    }

    /// <summary>
    /// 保存 Excel 的方法
    /// </summary>
    /// <param name="ds">dataset 對象</param>
    /// <param name="filename">名稱</param>
    public static void Execl(DataTable dt, string filename)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-xls";
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        DataGrid dg = new DataGrid();
        dg.DataSource = dt;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename));
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }

    protected void btoutExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = DateTime.Today.ToString("yyyyMMdd").Substring(0, 6) + "成績分佈統計匯總.xls";
            Execl(getDt(), fileName);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void gvAchievementScore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text.Equals("實占人數"))
            {
                string ZD = "";
                if (e.Row.Cells[2].Text.Equals("二職等"))
                {
                    ZD = "2";
                }
                else if (e.Row.Cells[2].Text.Equals("三/四/五職等"))
                {
                    ZD = "5";
                }
                else if (e.Row.Cells[2].Text.Equals("六職等及以上"))
                {
                    ZD = "6";
                }
                if (Convert.ToInt32(e.Row.Cells[4].Text.ToString().Trim()) > 0)
                {
                    string url = "AnalysisDetail.aspx?strUserGUID=" + e.Row.Cells[0].Text.ToString() + "&strAssessmentPlanName=" + Server.UrlEncode(lblplanGUID.Text) + "&AchievementLevel=" + Server.UrlEncode("A") + "&ZD=" + ZD;
                    e.Row.Cells[4].Text = "<a href='" + url + "' target='_blank'>" + e.Row.Cells[4].Text + "</a>";
                }
                if (Convert.ToInt32(e.Row.Cells[5].Text.ToString().Trim()) > 0)
                {
                    string url = "AnalysisDetail.aspx?strUserGUID=" + e.Row.Cells[0].Text.ToString() + "&strAssessmentPlanName=" + Server.UrlEncode(lblplanGUID.Text) + "&AchievementLevel=" + Server.UrlEncode("B") + "&ZD=" + ZD;
                    e.Row.Cells[5].Text = "<a href='" + url + "' target='_blank'>" + e.Row.Cells[5].Text + "</a>";
                }
                if (Convert.ToInt32(e.Row.Cells[6].Text.ToString().Trim()) > 0)
                {
                    string url = "AnalysisDetail.aspx?strUserGUID=" + e.Row.Cells[0].Text.ToString() + "&strAssessmentPlanName=" + Server.UrlEncode(lblplanGUID.Text) + "&AchievementLevel=" + Server.UrlEncode("C") + "&ZD=" + ZD;
                    e.Row.Cells[6].Text = "<a href='" + url + "' target='_blank'>" + e.Row.Cells[6].Text + "</a>";
                }
                if (Convert.ToInt32(e.Row.Cells[7].Text.ToString().Trim()) > 0)
                {
                    string url = "AnalysisDetail.aspx?strUserGUID=" + e.Row.Cells[0].Text.ToString() + "&strAssessmentPlanName=" + Server.UrlEncode(lblplanGUID.Text) + "&AchievementLevel=" + Server.UrlEncode("D") + "&ZD=" + ZD;
                    e.Row.Cells[7].Text = "<a href='" + url + "' target='_blank'>" + e.Row.Cells[7].Text + "</a>";
                }
                if (Convert.ToInt32(e.Row.Cells[8].Text.ToString().Trim()) > 0)
                {
                    string url = "AnalysisDetail.aspx?strUserGUID=" + e.Row.Cells[0].Text.ToString() + "&strAssessmentPlanName=" + Server.UrlEncode(lblplanGUID.Text) + "&AchievementLevel=" + Server.UrlEncode("E") + "&ZD=" + ZD;
                    e.Row.Cells[8].Text = "<a href='" + url + "' target='_blank'>" + e.Row.Cells[8].Text + "</a>";
                }
                if (Convert.ToInt32(e.Row.Cells[9].Text.ToString().Trim()) > 0)
                {
                    string url = "AnalysisDetail.aspx?strUserGUID=" + e.Row.Cells[0].Text.ToString() + "&strAssessmentPlanName=" + Server.UrlEncode(lblplanGUID.Text) + "&AchievementLevel=" + Server.UrlEncode("未評核") + "&ZD=" + ZD;
                    e.Row.Cells[9].Text = "<a href='" + url + "' target='_blank'>" + e.Row.Cells[9].Text + "</a>";
                }
            }
        }
    }
}