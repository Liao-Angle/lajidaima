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
        ItemNo.ValueText = objects.getData("ItemNo");
        ItemName.ValueText = objects.getData("ItemName");
        Content.ValueText = objects.getData("Content");
        FractionExp.ValueText = objects.getData("FractionExp");
        MinFraction.ValueText = objects.getData("MinFraction");
        MaxFraction.ValueText = objects.getData("MaxFraction");
        sfItemNum.ValueText = objects.getData("ItemNum");
        sfItemWeight.ValueText = objects.getData("ItemWeight");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        IOFactory factory = new IOFactory();
        //AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("EvaluationGUID", "temp");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("ItemNo", ItemNo.ValueText);
        objects.setData("ItemName", ItemName.ValueText);
        objects.setData("Content", Content.ValueText);
        objects.setData("FractionExp", FractionExp.ValueText);
        objects.setData("MaxFraction", MaxFraction.ValueText);
        objects.setData("MinFraction", MinFraction.ValueText);
        objects.setData("ItemNum", sfItemNum.ValueText);
        objects.setData("ItemWeight", sfItemWeight.ValueText);

        string errMsg = "";
        int number = 0;
        bool isNumeric = false;

        isNumeric = int.TryParse(objects.getData("ItemNo"), out number);
        if (!isNumeric)
        {
            errMsg += LblItemNo.Text + " 欄位必需為數字!\n";
        }

        isNumeric = int.TryParse(objects.getData("MinFraction"), out number);
        if (!isNumeric)
        {
            errMsg += LblMaxFraction.Text + " 欄位必需為數字!\n";
        }

        isNumeric = int.TryParse(objects.getData("MaxFraction"), out number);
        if (!isNumeric)
        {
            errMsg += LblMaxFraction.Text + " 欄位必需為數字!\n";
        }

        isNumeric = int.TryParse(objects.getData("ItemNum"), out number);

        if (!isNumeric)
        {
            errMsg += lblItemNum.Text + "欄位必需為數字!\n";
        }
        isNumeric = int.TryParse(objects.getData("ItemWeight"), out number);

        if (!isNumeric)
        {
            errMsg += lblItemWeight.Text + "欄位必需為數字!\n";
        }

        if (!string.IsNullOrEmpty(errMsg))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
    }
}
