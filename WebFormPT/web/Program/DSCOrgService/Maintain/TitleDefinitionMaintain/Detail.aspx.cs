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
using WebServerProject.org.TitleDefinition;

public partial class Program_DSCOrgService_Maintain_TitleDefinitionMaintain_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        TitleDefinition obj = (TitleDefinition)objects;

        OIDF.ValueText = obj.OID;
        objectVersionF.ValueText = obj.objectVersion;
        shortNameF.ValueText = obj.shortName;
        titleDefinitionNameF.ValueText = obj.titleDefinitionName;
        organizationOIDF.ValueText = obj.organizationOID;
        descriptionF.ValueText = obj.description;
        organizationNameF.ValueText = obj.organizationName;
    }
}
