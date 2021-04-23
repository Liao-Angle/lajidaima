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

public partial class SmpProgram_Maintain_SPKM001_Detail_2_3 : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string[,] ids = null;
                ids = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "ids2", "失效")}
                };
                Enable.setListItem(ids);

                string[,] eds = null;
                eds = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "eds1", "是")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spkm001m_detail_2_3_aspx.language.ini", "message", "eds2", "否")}
                };
                External.setListItem(eds);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        Name.ValueText = objects.getData("Name");
        Desc.ValueText = objects.getData("Description");
        Enable.ValueText = objects.getData("Enable");
        External.ValueText = objects.getData("External");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SubTypeGUID", "temp");
            objects.setData("MajorTypeGUID", "temp");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("Name", Name.ValueText);
        objects.setData("Description", Desc.ValueText);
        objects.setData("Enable", Enable.ValueText);
        objects.setData("External", External.ValueText);
    }
}