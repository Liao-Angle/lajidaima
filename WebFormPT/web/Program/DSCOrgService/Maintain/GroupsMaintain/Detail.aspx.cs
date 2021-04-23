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
using WebServerProject.org.Groups;

public partial class Program_DSCOrgService_Maintain_GroupsMaintain_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        Groups obj = (Groups)objects;

        OIDF.ValueText = obj.OID;
        objectVersionF.ValueText = obj.objectVersion;
        idF.ValueText = obj.id;
        groupNameF.ValueText = obj.groupName;
        organizationOIDF.ValueText = obj.organizationOID;
        descriptionF.ValueText = obj.description;
        organizationNameF.ValueText = obj.organizationName;

        DataObjectSet dos = obj.getChild("GroupUser");
        UserList.ReadOnly = true;
        UserList.InputForm = null;
        UserList.HiddenField = new string[] { "OID", "GroupOID", "UserOID" };
        UserList.dataSource = dos;
        UserList.updateTable();
    }
}
