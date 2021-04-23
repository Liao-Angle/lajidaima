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

public partial class Program_System_Form_STDDOC_Reassign : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //底下這段是取得原表單畫面相關參數設定. 若要傳其它參數, 可仿照平台基本寫法, 
                //將資料存在以原本PageUniqueID命名的Session中. 系統會自動將原本畫面的PageUniqueID傳入
                string PGID = Request.QueryString["PGID"];
                string SMWDAAA001 = Request.QueryString["SMWDAAA001"];

                ToUserID.clientEngineType = (string)Session["engineType"];
                ToUserID.connectDBString = (string)Session["connectString"];

            }
        }
    }
    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        if (AssignOpinion.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_reassign_aspx.language.ini", "message", "QueryError", "必須填寫原因"));
            return;
        }

        Session["tempToUserID"] = ToUserID.ValueText;
        Session["tempToUserGUID"] = ToUserID.GuidValueText;
        Session["tempAssignOpinion"] = AssignOpinion.ValueText;
        Session["tempAssignmentType"] = "0";

        Response.Write("window.returnValue='YES';");
        Response.Write("window.close();");

    }
}
