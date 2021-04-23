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
using WebServerProject.org.Users;

public partial class Program_DSCOrgService_Maintain_UsersMaintain_Detail : BaseWebUI.DataListInlineForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        Users obj = (Users)objects;

        OIDF.ValueText = obj.OID;
        objectVersionF.ValueText = obj.objectVersion;
        idF.ValueText = obj.id;
        userNameF.ValueText = obj.userName;
        passwordF.ValueText = obj.password;
        leaveDateF.ValueText = obj.leaveDate;
        referCalendarF.ValueText = obj.referCalendarOID;
        identificationTypeF.ValueText = obj.identificationType;
        languageTypeF.ValueText = obj.localeString;
        mailAddressF.ValueText = obj.mailAddress;
        phoneNumberF.ValueText = obj.phoneNumber;
        workflowServerOIDF.ValueText = obj.workflowServerOID;
        enableSubstituteF.ValueText = obj.enableSubstitute;
        startSubstituteTimeF.ValueText = obj.startSubstituteTime;
        endSubstituteTimeF.ValueText = obj.endSubstituteTime;
        costF.ValueText = obj.cost;
    }
}
