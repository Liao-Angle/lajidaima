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
using smp.pms.utility;
using System.Drawing;

public partial class Program_SCQ_Form_Photo_Form : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ExitButton.Display = false;
        ResetButton.Display = false;
        SaveButton.Display = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        using (Bitmap bmp = new Bitmap(300, 50))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.WhiteSmoke);
                g.DrawString("字符串", new Font("隶书", 40), Brushes.Red, new PointF(50, 0));
                g.DrawEllipse(new Pen(Color.Green, 2), 10, 10, 10, 10);
            }
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                string base64 = objects.getData("Photo64");
                Image1.ImageUrl = "data:image/png;base64," + base64;
            }

        }
        //displayData(objects);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        //IOFactory factory = new IOFactory();
        //bool isNew = (bool)getSession("isNew");
        //if (isNew)
        //{
        //    objects.setData("GUID", IDProcessor.getID(""));
        //    objects.setData("EvaluationGUID", "temp");
        //    objects.setData("IS_DISPLAY", "Y");
        //    objects.setData("IS_LOCK", "N");
        //    objects.setData("DATA_STATUS", "Y");
        //}
        ////objects.setData("ItemNo", ItemNo.ValueText);
        //objects.setData("ItemName", ItemName.ValueText);
        //objects.setData("Content", Content.ValueText);
        //objects.setData("FractionExp", FractionExp.ValueText);
        //objects.setData("MinFraction", MinFraction.ValueText);
        //objects.setData("MaxFraction", MaxFraction.ValueText);

        //string errMsg = "";
        //string minFraction = MinFraction.ValueText;
        //string maxFraction = MaxFraction.ValueText;
        //string selfScore = SelfScore.ValueText;
        //string firstScore = FirstScore.ValueText;
        //string secondScore = SecondScore.ValueText;
        //bool isNumeric = false;
        //int intMinFraction = 0;
        //int intMaxFraction = 0;
        //int number = 0;

        //isNumeric = int.TryParse(minFraction, out intMinFraction);
        //isNumeric = int.TryParse(maxFraction, out intMaxFraction);


        //objects.setData("SelfScore", selfScore);
        //objects.setData("SelfComments", SelfComments.ValueText);
        //objects.setData("FirstScore", firstScore);
        //objects.setData("FirstComments", FirstComments.ValueText);
        //objects.setData("SecondScore", secondScore);
        //objects.setData("SecondComments", SecondComments.ValueText);
        
        //if (!string.IsNullOrEmpty(selfScore))
        //{
        //    isNumeric = int.TryParse(selfScore, out number);
        //    if (isNumeric)
        //    {
        //        if (number > intMaxFraction)
        //        {
        //            errMsg += LblSelfScore.Text + " " +  number + " 大於最高分數!\n";
        //        }
        //        if (number < intMinFraction)
        //        {
        //            errMsg += LblSelfScore.Text + " " + number + " 小於最低分數!\n";
        //        }
        //    }
        //    else
        //    {
        //        errMsg += LblSelfScore.Text + " 欄位必需為數字!\n";
        //    }
        //}

        //if (!string.IsNullOrEmpty(firstScore))
        //{
        //    isNumeric = int.TryParse(firstScore, out number);
        //    if (isNumeric)
        //    {
        //        if (number > intMaxFraction)
        //        {
        //            errMsg += LblFirstScore.Text + " " + number + " 大於最高分數!\n";
        //        }
        //        if (number < intMinFraction)
        //        {
        //            errMsg += LblFirstScore.Text + " " + number + " 小於最低分數!\n";
        //        }
        //    }
        //    else
        //    {
        //        errMsg += LblFirstScore.Text + " 欄位必需為數字!\n";
        //    }
        //}

        //if (!string.IsNullOrEmpty(secondScore))
        //{
        //    isNumeric = int.TryParse(secondScore, out number);
        //    if (isNumeric)
        //    {
        //        if (number > intMaxFraction)
        //        {
        //            errMsg += LblSecondScore.Text + " " + number + " 大於最高分數!\n";
        //        }
        //        if (number < intMinFraction)
        //        {
        //            errMsg += LblSecondScore.Text + " " + number + " 小於最低分數!\n";
        //        }
        //    }
        //    else
        //    {
        //        errMsg += LblSecondScore.Text + " 欄位必需為數字!\n";
        //    }
        //}

        //if (!string.IsNullOrEmpty(errMsg))
        //{
        //    errMsg = errMsg.Replace("\n", "; ");
        //    throw new Exception(errMsg);
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected void displayData(DataObject objects)
    {
        //string stage = (string)getSession("Stage", "Stage");
        //if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
        //{

        //}
        //else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
        //{
        //    SelfScore.ReadOnly = true;
        //    SelfComments.ReadOnly = true;
            
        //    LblFirstScore.Display = true;
        //    FirstScore.Display = true;
        //    LblFirstComments.Display = true;
        //    FirstComments.Display = true;
        //}
        //else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
        //{
        //    SelfScore.ReadOnly = true;
        //    SelfComments.ReadOnly = true;
        //    FirstScore.ReadOnly = true;
        //    FirstComments.ReadOnly = true;
            
        //    LblFirstScore.Display = true;
        //    FirstScore.Display = true;
        //    LblFirstComments.Display = true;
        //    FirstComments.Display = true;
        //    LblSecondScore.Display = true;
        //    SecondScore.Display = true;
        //    LblSecondComments.Display = true;
        //    SecondComments.Display = true;
        //}
    }
}
