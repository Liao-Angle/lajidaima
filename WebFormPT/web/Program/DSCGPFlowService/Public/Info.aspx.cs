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
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class Program_DSCGPFlowService_Public_Info : BaseWebUI.GeneralWebPage
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

                OpenWin1.clientEngineType = (string)Session["engineType"];
                OpenWin1.connectDBString = (string)Session["connectString"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine((string)Session["engineType"], (string)Session["connectString"]);

                string sql = "select SMWDAAA022 from SMWDAAA where SMWDAAA001='" + Utility.filter(SMWDAAA001) + "'";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                string[,] ids;
                if (ds.Tables[0].Rows[0][0].ToString().Equals("1"))
                {
                    ids = new string[,]{
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_info_aspx.language", "message", "ids1", "私人轉寄")}
                    };
                }
                else if (ds.Tables[0].Rows[0][0].ToString().Equals("2"))
                {
                    ids = new string[,]{
                        {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_info_aspx.language", "message", "ids2", "於流程中追蹤")}
                    };
                }
                else
                {
                    ids = new string[,]{
                        {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_info_aspx.language", "message", "ids1", "私人轉寄")},
                        {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_info_aspx.language", "message", "ids2", "於流程中追蹤")}
                   };
                }
                engine.close();

                NoticeType.setListItem(ids);
            }
        }
    }
    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        string[] acceptorOID = new string[AcceptorOID.getListItem().GetLength(0)];
        if (acceptorOID.Length == 0)
        {
            //MessageBox("必須選擇通知人員");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_info_aspx.language", "message", "QueryError", "必須選擇通知人員"));
           return;
        }
        for (int i = 0; i < AcceptorOID.getListItem().GetLength(0); i++)
        {
            acceptorOID[i] = AcceptorOID.getListItem()[i, 0];
        }

        Session["tempAcceptorOID"] = acceptorOID;
        Session["tempNoticeType"] = NoticeType.ValueText;

        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");
                Response.Write("window.top.returnValue='YES';");
                break;
            default:
                string js = "";
                js += "window.parent.parent.$('#ecpDialog').attr('ret', 'YES');";
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        }                    

    }
    protected void SelectUser_Click(object sender, EventArgs e)
    {
        OpenWin1.PageUniqueID = this.PageUniqueID;
        OpenWin1.identityID = "0001";
        OpenWin1.paramString = "id";
        OpenWin1.whereClause = "";
        OpenWin1.openWin("Users", "001", true, "0001");

    }
    protected void OpenWin1_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            string[,] ls = AcceptorOID.getListItem();
            if (ls == null)
            {
                ls = new string[0, 2];
            }
            ArrayList ary = new ArrayList();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                bool hasFound = false;
                for (int j = 0; j < ls.GetLength(0); j++)
                {
                    if (ls[j, 0].Equals(values[i, 1]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    string[] tag = new string[] { values[i, 1], values[i, 2] + "(" + values[i, 1] + ")" };
                    ary.Add(tag);
                }
            }

            string[,] ns = new string[ls.GetLength(0) + ary.Count, 2];
            for (int i = 0; i < ls.GetLength(0); i++)
            {
                ns[i, 0] = ls[i, 0];
                ns[i, 1] = ls[i, 1];
            }
            for (int i = 0; i < ary.Count; i++)
            {
                string[] tag = (string[])ary[i];
                ns[i + ls.GetLength(0), 0] = tag[0];
                ns[i + ls.GetLength(0), 1] = tag[1];
            }
            AcceptorOID.setListItem(ns);
        }
    }
    protected void DeleteUser_Click(object sender, EventArgs e)
    {
        if (AcceptorOID.getListItem() == null)
        {
            return;
        }

        string[] OIDS = AcceptorOID.ValueText;
        string[,] alls = AcceptorOID.getListItem();
        ArrayList ary = new ArrayList();
        for (int i = 0; i < alls.GetLength(0); i++)
        {
            bool hasFound = false;
            for (int j = 0; j < OIDS.Length; j++)
            {
                if (alls[i, 0].Equals(OIDS[j]))
                {
                    hasFound = true;
                    break;
                }
            }
            if (!hasFound)
            {
                string[] tag = new string[] { alls[i, 0], alls[i, 1] };
                ary.Add(tag);
            }
        }

        string[,] ids = new string[ary.Count, 2];
        for (int i = 0; i < ary.Count; i++)
        {
            string[] tag = (string[])ary[i];
            ids[i, 0] = tag[0];
            ids[i, 1] = tag[1];
        }
        AcceptorOID.setListItem(ids);
    }
    protected void UserButton_Click(object sender, EventArgs e)
    {
        OpenWin1.PageUniqueID = this.PageUniqueID;
        OpenWin1.identityID = "0001";
        OpenWin1.paramString = "id";
        OpenWin1.whereClause = "OID in (select SMVMAAA003 from SMVMAAA where SMVMAAA002='"+(string)Session["UserID"]+"')";
        OpenWin1.openWin("Users", "001", true, "0001");

    }
}
