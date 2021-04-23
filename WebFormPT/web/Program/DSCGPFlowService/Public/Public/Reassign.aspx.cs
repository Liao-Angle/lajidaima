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

public partial class Program_DSCGPFlowService_Public_Reassign : BaseWebUI.GeneralWebPage
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

                string manualReassignType = (string)base.getSession(PGID, "manualReassignType");

                ToUserID.clientEngineType = (string)Session["engineType"];
                ToUserID.connectDBString = (string)Session["connectString"];

                OpenWin1.clientEngineType = (string)Session["engineType"];
                OpenWin1.connectDBString = (string)Session["connectString"];
                OpenWin2.clientEngineType = (string)Session["engineType"];
                OpenWin2.connectDBString = (string)Session["connectString"];

                string[,] ids = null;

                if (manualReassignType.Equals("1"))
                {
                    ids = new string[,]{
                        {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_reassign_aspx.language.ini", "message", "ids0", "直接轉派")}
                    };
                }
                else if (manualReassignType.Equals("4"))
                {
                    ids = new string[,]{
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_reassign_aspx.language.ini", "message", "ids1", "工作轉派")}
                    };
                }
                else
                {
                    ids = new string[,]{
                        {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_reassign_aspx.language.ini", "message", "ids0", "直接轉派")},
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_reassign_aspx.language.ini", "message", "ids1", "工作轉派")}
                    };
                }
                assignmentType.setListItem(ids);
            }
        }
    }
    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        if (AssignOpinion.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_reassign_aspx.language.ini", "message", "QueryError", "必須填寫轉派意見"));
            return;
        }

        Session["tempToUserID"] = ToUserID.ValueText;
        Session["tempToUserGUID"] = ToUserID.GuidValueText;
        Session["tempAssignOpinion"] = AssignOpinion.ValueText;
        Session["tempAssignmentType"] = assignmentType.ValueText;

        Response.Write("window.top.returnValue='YES';");
        Response.Write("window.top.close();");

    }
    protected void PhraseButton_Click(object sender, EventArgs e)
    {
        OpenWin1.PageUniqueID = this.PageUniqueID;
        OpenWin1.identityID = "0001";
        OpenWin1.paramString = "SMVLAAA002";
        OpenWin1.whereClause = "SMVLAAA002='" + (string)Session["UserID"] + "'";
        OpenWin1.openWin("SMVLAAA", "001", false, "0001");

    }
    protected void OpenWin1_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            AssignOpinion.ValueText += values[0, 2];
        }
    }
    protected void OpenWin2_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            ToUserID.GuidValueText = values[0, 1];
            ToUserID.doGUIDValidate();
        }
    }
    protected void UserButton_Click(object sender, EventArgs e)
    {
        OpenWin2.PageUniqueID = this.PageUniqueID;
        OpenWin2.identityID = "0001";
        OpenWin2.paramString = "SMVMAAA002";
        OpenWin2.whereClause = "SMVMAAA002='" + (string)Session["UserID"] + "'";
        OpenWin2.openWin("SMVMAAA", "001", false, "0001");
    }
}
