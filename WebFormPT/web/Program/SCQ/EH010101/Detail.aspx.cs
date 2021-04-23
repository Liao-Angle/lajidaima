using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class SmpProgram_Maintain_SPPM001_Detail : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string EmpNo = objects.getData("EmpNo");
        string WorkDate = objects.getData("WorkDate");
        //Content.ValueText = objects.getData("Content");
        //FractionExp.ValueText = objects.getData("FractionExp");
        //MinFraction.ValueText = objects.getData("MinFraction");
        //MaxFraction.ValueText = objects.getData("MaxFraction");
        //sfItemNum.ValueText = objects.getData("ItemNum");
        //sfItemWeight.ValueText = objects.getData("ItemWeight");
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            //string EmpNo = (string)Session["EmpNo"];
            //string WorkDate = (string)Session["WorkDate"];

            string sql = null;
            DataSet ds = null;
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);


            //統計表                 
            sql = @"select STAFF_NUM,STAFF_NAME,MACHINE_ID,R_DATE,R_TIME,MS from [10.3.11.68].SCQHR_DB.DBO.sgzshuju where STAFF_NUM='" + EmpNo + "' and R_DATE='" + WorkDate.Replace("/", "") + "' order by R_TIME ";

            ds = engine.getDataSet(sql, "TEMP");

            DataTable dt = new DataTable();
            DataRow dr;

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("工號 ", typeof(string)));
            dt.Columns.Add(new DataColumn("姓名", typeof(string)));
            dt.Columns.Add(new DataColumn("日期", typeof(double)));
            dt.Columns.Add(new DataColumn("時間", typeof(string)));
            dt.Columns.Add(new DataColumn("進/出", typeof(string)));
            dt.Columns.Add(new DataColumn("卡機號", typeof(string)));

            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];

            for (int i = 0; i < rows; i++)
            {
                dr = dt.NewRow();
                dr[0] = ds.Tables[0].Rows[i]["STAFF_NUM"].ToString();
                dr[1] = ds.Tables[0].Rows[i]["STAFF_NAME"].ToString();
                dr[2] = ds.Tables[0].Rows[i]["R_DATE"].ToString();
                dr[3] = ds.Tables[0].Rows[i]["R_TIME"].ToString();
                dr[4] = ds.Tables[0].Rows[i]["MS"].ToString();
                dr[5] = ds.Tables[0].Rows[i]["MACHINE_ID"].ToString();

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
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        //IOFactory factory = new IOFactory();
        ////AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        //bool isNew = (bool)getSession("isNew");
        //if (isNew)
        //{
        //    objects.setData("GUID", IDProcessor.getID(""));
        //    objects.setData("EvaluationGUID", "temp");
        //    objects.setData("IS_DISPLAY", "Y");
        //    objects.setData("IS_LOCK", "N");
        //    objects.setData("DATA_STATUS", "Y");
        //}
        //objects.setData("ItemNo", ItemNo.ValueText);
        //objects.setData("ItemName", ItemName.ValueText);
        //objects.setData("Content", Content.ValueText);
        //objects.setData("FractionExp", FractionExp.ValueText);
        //objects.setData("MaxFraction", MaxFraction.ValueText);
        //objects.setData("MinFraction", MinFraction.ValueText);
        //objects.setData("ItemNum", sfItemNum.ValueText);
        //objects.setData("ItemWeight", sfItemWeight.ValueText);

        //string errMsg = "";
        //int number = 0;
        //bool isNumeric = false;

        //isNumeric = int.TryParse(objects.getData("ItemNo"), out number);
        //if (!isNumeric)
        //{
        //    errMsg += LblItemNo.Text + " 欄位必需為數字!\n";
        //}

        //isNumeric = int.TryParse(objects.getData("MinFraction"), out number);
        //if (!isNumeric)
        //{
        //    errMsg += LblMaxFraction.Text + " 欄位必需為數字!\n";
        //}

        //isNumeric = int.TryParse(objects.getData("MaxFraction"), out number);
        //if (!isNumeric)
        //{
        //    errMsg += LblMaxFraction.Text + " 欄位必需為數字!\n";
        //}

        //isNumeric = int.TryParse(objects.getData("ItemNum"), out number);

        //if (!isNumeric)
        //{
        //    errMsg += lblItemNum.Text + "欄位必需為數字!\n";
        //}
        //isNumeric = int.TryParse(objects.getData("ItemWeight"), out number);

        //if (!isNumeric)
        //{
        //    errMsg += lblItemWeight.Text + "欄位必需為數字!\n";
        //}

        //if (!string.IsNullOrEmpty(errMsg))
        //{
        //    errMsg = errMsg.Replace("\n", "; ");
        //    throw new Exception(errMsg);
        //}
    }
    protected void gvAchievementList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            //保持列不变形
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                //方法一：
                //e.Row.Cells[i].Text = "&nbsp;" + e.Row.Cells[i].Text + "&nbsp;";  此行代碼會將list第一列失效,
                e.Row.Cells[i].Wrap = false;
                //方法二：
                //e.Row.Cells[i].Text = "<nobr>&nbsp;" + e.Row.Cells[i].Text + "&nbsp;</nobr>";            
            }
        }
    }
}
