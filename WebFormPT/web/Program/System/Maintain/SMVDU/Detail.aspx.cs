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
using WebServerProject.maintain.SMVD;

public partial class Program_System_Maintain_SMVD_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVDU";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        if (!isAddNew)
        {
            SMVDAAA003.ReadOnly = true;
            SMVDAAA004.ReadOnly = true;
            SMVDAAA007.ReadOnly = true;
            SMVDAAA008.ReadOnly = true;
            SMVDAAA009.ReadOnly = true;

        }
        string[,] ids = new string[,]{
                {"0","一般訊息"},
                {"1","重要訊息"},
                {"2","行事曆通知"}
            };
        SMVDAAA003.setListItem(ids);

        SMVDAAA obj = (SMVDAAA)objects;
        SMVDAAA003.ValueText = obj.SMVDAAA003;
        SMVDAAA004.ValueText = obj.SMVDAAA004;
        SMVDAAA007.ValueText = obj.SMVDAAA007;
        SMVDAAA008.ValueText = obj.SMVDAAA008;
        SMVDAAA009.ValueText = obj.SMVDAAA009;

        if (isAddNew)
        {
            SMVDAAA004.ValueText = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
        }

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select * from SMVDAAB where SMVDAAB002='" + Utility.filter(obj.SMVDAAA001) + "' and SMVDAAB003='" + (String)Session["UserID"] + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        if (ds.Tables[0].Rows.Count == 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            dr["SMVDAAB001"] = IDProcessor.getID("");
            dr["SMVDAAB002"] = obj.SMVDAAA001;
            dr["SMVDAAB003"] = (string)Session["UserID"];
            dr["SMVDAAB004"] = DateTimeUtility.getSystemTime2(null);
            dr["SMVDAAB005"] = "Y";
            dr["SMVDAAB006"] = DateTimeUtility.getSystemTime2(null);
            dr["D_INSERTUSER"] = (string)Session["UserID"];
            dr["D_INSERTTIME"] = DateTimeUtility.getSystemTime2(null);
            dr["D_MODIFYUSER"] = "";
            dr["D_MODIFYTIME"] = "";
            ds.Tables[0].Rows.Add(dr);
        }
        else
        {
            DataRow dr = ds.Tables[0].Rows[0];
            dr["SMVDAAB005"] = "Y";
            dr["SMVDAAB006"] = DateTimeUtility.getSystemTime2(null);

        }
        engine.updateDataSet(ds);

        engine.close();
    }

}
