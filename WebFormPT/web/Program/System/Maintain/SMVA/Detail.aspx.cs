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

public partial class Program_System_Maintain_SMVA_Detail : BaseWebUI.GeneralWebPage
{
    protected string msg1 = "必須填寫模組代碼";
    protected string msg2 = "模組代碼不可超過50字元";
    protected string msg3 = "必須填寫模組名稱";
    protected string msg4 = "模組名稱不可超過50字元";

    protected override void OnPreRender(EventArgs e)
    {
        msg1 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_detail_aspx.language.ini", "message", "msg1", "必須填寫模組代碼");
        msg2 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_detail_aspx.language.ini", "message", "msg2", "模組代碼不可超過50字元");
        msg3 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_detail_aspx.language.ini", "message", "msg3", "必須填寫模組名稱");
        msg4 = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smva_detail_aspx.language.ini", "message", "msg4", "模組名稱不可超過50字元");
        base.OnPreRender(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
