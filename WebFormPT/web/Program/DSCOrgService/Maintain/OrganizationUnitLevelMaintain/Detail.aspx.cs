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
using WebServerProject.org.OrganizationUnitLevel;

public partial class Program_DSCOrgService_Maintain_OrganizationUnitLevelMaintain_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        OrganizationUnitLevel obj = (OrganizationUnitLevel)objects;

        OIDF.ValueText = obj.OID;
        objectVersionF.ValueText = obj.objectVersion;
        levelValueF.ValueText = obj.levelValue;
        organizationUnitLevelNameF.ValueText = obj.organizationUnitLevelName;
        organizationOIDF.ValueText = obj.organizationOID;
        descriptionF.ValueText = obj.description;
        organizationNameF.ValueText = obj.organizationName;
    }
}
