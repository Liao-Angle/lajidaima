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

public partial class SmpProgram_Detail : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string[,] ids = null;

        ids = new string[,] {
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001_detail_aspx.language.ini", "message", "", "")},
                    {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001_detail_aspx.language.ini", "message", "ids2", "簽核")},
                    {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001_detail_aspx.language.ini", "message", "ids4", "通知")}
        };
        StateType.setListItem(ids);

        ids = new string[,] {
                    {"",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001_detail_aspx.language.ini", "message", "", "")},
                    {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001_detail_aspx.language.ini", "message", "ids1", "員工")},
                    {"21",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad001_detail_aspx.language.ini", "message", "ids21", "群組")}
        };
        SignType.setListItem(ids);

        //StateValueGUID.Display = false;
        StateValueGUID.clientEngineType = (string)Session["engineType"];
        StateValueGUID.connectDBString = (string)Session["connectString"];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        StateNo.ValueText = objects.getData("StateNo");
        StateType.ValueText = objects.getData("StateType");

        StateValueGUID.Display = true;
        StateValueGUID.GuidValueText = objects.getData("StateValueGUID");
        SignType.ValueText = objects.getData("SignType");
        if (SignType.ValueText.Equals("1"))
        {
            StateValueGUID.tableName = "Users";
        }
        else if (SignType.ValueText.Equals("21"))
        {
            StateValueGUID.tableName = "OrgGroup";
        }
        StateValueGUID.doGUIDValidate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string strStateValueGUID = StateValueGUID.GuidValueText;
        string strSignType = SignType.ValueText;
        string sql = "";
        //if (strSignType.Equals("1"))
        //{
        //    sql = "select userName from Users where OID = '" + strStateValueGUID + "'";
        //}
        //else if (strSignType.Equals("21"))
        //{
        //    sql = "select groupName from Groups where OID = '" + strStateValueGUID + "'";
        //}
        //DataSet ds = engine.getDataSet(sql, "TEMP");
        //string strStateValueName = ds.Tables[0].Rows[0][0].ToString();
        string strStateValueName = StateValueGUID.ReadOnlyValueText;
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("UserFlowGUID","temp");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("StateNo", StateNo.ValueText);
        objects.setData("StateType", StateType.ValueText);
        objects.setData("SignType", SignType.ValueText);
        objects.setData("StateValueGUID", StateValueGUID.GuidValueText);
        objects.setData("StateValueId", StateValueGUID.ValueText);
        objects.setData("StateValueName", strStateValueName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    protected void SignType_SelectChanged(string value)
    {
        StateValueGUID.GuidValueText = "";
        StateValueGUID.doGUIDValidate();

        string strSignType = SignType.ValueText;
        if (strSignType.Equals("1"))
        {
            StateValueGUID.tableName = "Users";
        }
        else if (strSignType.Equals("21"))
        {
            StateValueGUID.tableName = "OrgGroup";
        }
    }
}
