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

public partial class SmpProgram_Maintain_SPPM011_Analysis : BaseWebUI.DataListSaveForm
{

    protected override void showData(DataObject objects)
    {
        AbstractEngine engine;
        try
        {
            if (objects.getData("Status").Equals("COMPLETE"))
            {
                cbSubmitStatus.ReadOnly = true;
            }
            lblplanGUID.Text = objects.getData("AssessmentPlanGUID");
            lblAssessUserGUID.Text = objects.getData("AssessUserGUID");

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            cbSubmitStatus.Checked = objects.getData("SubmitStatus") == "Y" ? true : false;

            string whereClause = " AssessmentPlanGUID = '" + objects.getData("AssessmentPlanGUID") + "' and AssessUserGUID ='" + objects.getData("AssessUserGUID") + "' " +
                                 " and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "' ";
            string sql = null;
            DataSet ds = null;
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            //人數   
            sfyphNum.ValueText = "0";
            sfCompleteNum.ValueText = "0";
            sfNoCompleteNum.ValueText = "0";

            sql = "select count(AchievementLevel),isnull(sum(case when  AchievementLevel <>'' then 1 else 0 end ),0) complete," +
                         "isnull(sum(case when  AchievementLevel <>'' then 0  else 1  end ),0) uncomplete " +
                         "from SmpPmUserAchievement a " +
                         "join SmpHrEmployeeInfoV b on b.empNumber=a.UserGUID where " + whereClause;

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                sfyphNum.ValueText = ds.Tables[0].Rows[0][0].ToString();
                sfCompleteNum.ValueText = ds.Tables[0].Rows[0][1].ToString();
                sfNoCompleteNum.ValueText = ds.Tables[0].Rows[0][2].ToString();
            }

            //統計表                 
            sql = "select a.AchievementLevel, Description ,COUNT(b.GUID) NOP " +
                  "from SmpPmAchievementLevel a left join SmpPmUserAchievement b on a.AchievementLevel =b.AchievementLevel and " +
                  whereClause +
                  "GROUP by  a.AchievementLevel, a.Description " +
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
                if (!sfyphNum.ValueText.Equals('0'))
                    percentage = Convert.ToDouble(dr[2]) / Convert.ToDouble(sfyphNum.ValueText);
                dr[3] = String.Format("{0:P}", percentage);

                dt.Rows.Add(dr);
            }
            DataView dv = new DataView(dt);
            gvAchievementScore.DataSource = dv;
            gvAchievementScore.DataBind();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        engine.close();
        sfyphNum.ReadOnly = true;
        sfCompleteNum.ReadOnly = true;
        sfNoCompleteNum.ReadOnly = true;
        lblplanGUID.Display = false;
        lblAssessUserGUID.Display = false;

        base.showData(objects);
    }
    protected override void saveData(DataObject objects)
    {
        objects.setData("SubmitStatus", cbSubmitStatus.Checked == true ? "Y" : "N");
        base.saveData(objects);
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sqltxt = "update dbo.SmpPmUserAchievement set SubmitStatus='" + objects.getData("SubmitStatus") + "' where AssessmentPlanGUID='" + objects.getData("AssessmentPlanGUID") + "' and AssessUserGUID='" + objects.getData("AssessUserGUID") + "'";
        bool result = engine.executeSQL(sqltxt);
        if (!result)
        {
            throw new Exception(engine.errorString);
        }
        
        engine.close();

        base.saveDB(objects);
    }

    protected void gbSendNotice_Click(object sender, EventArgs e)
    {
        try
        {
            SmpPmMaintainUtil.SendSupviorNotice(lblplanGUID.Text, lblAssessUserGUID.Text);
            MessageBox("郵件發送成功");
        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
        }
    }
}