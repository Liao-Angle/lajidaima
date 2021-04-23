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

public partial class Program_DSCGPFlowService_Public_SmpShowOpinion : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                if (!((bool)Session["signCheckResult"]))
                {
                    Response.Write("<script language=javascript>");
                    Response.Write("window.top.close();");
                    Response.Write("</script>");
                }
                //底下這段是取得原表單畫面相關參數設定. 若要傳其它參數, 可仿照平台基本寫法, 
                //將資料存在以原本PageUniqueID命名的Session中. 系統會自動將原本畫面的PageUniqueID傳入
                string SMWDAAA001 = Request.QueryString["SMWDAAA001"];
                string PGID = Request.QueryString["PGID"];

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "select SMWDAAA009, SMWDAAA010 from SMWDAAA where SMWDAAA001='" + Utility.filter(SMWDAAA001) + "'";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                /*
                if (ds.Tables[0].Rows[0][0].ToString().Equals("0"))
                {
                    //准, 不准
                    string[,] ids = new string[,]{
                        {"YYYYYYYY","准"},
                        {"NNNNNNNN","不准"}
                    };
                    SignResult.setListItem(ids);
                }
                else
                {
                }
                */
                setSession("tempSignType", ds.Tables[0].Rows[0][0].ToString());

                //sql = "select SMWHAAB005, SMWHAAB004 from SMWHAAB where SMWHAAB002='" + Utility.filter(ds.Tables[0].Rows[0][1].ToString()) + "'";
                string backActID = Convert.ToString(Session["tempBackActID"]);
                if (backActID.ToUpper().Equals("CREATOR"))
                {
                    sql = "select SMWHAAB005, SMWHAAB004 from SMWHAAB where SMWHAAB002='" + Utility.filter(ds.Tables[0].Rows[0][1].ToString()) + "'";
                }
                else
                {
                    sql = "select SMWHAAB005, SMWHAAB004 from SMWHAAB where SMWHAAB002='" + Utility.filter(ds.Tables[0].Rows[0][1].ToString()) + "' and SMWHAAB005 = 'Y'";
                }
                
                ds = engine.getDataSet(sql, "TEMP");
                string[,] ids = new string[ds.Tables[0].Rows.Count, 2];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ids[i, 0] = ds.Tables[0].Rows[i][0].ToString()+";"+i.ToString();
                    ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
                }
                SignResult.setListItem(ids);

                engine.close();
                OpenWin1.clientEngineType = (string)Session["engineType"];
                OpenWin1.connectDBString = (string)Session["connectString"];
            }
        }
    }
    protected void SendButton_Click(object sender, EventArgs e)
    {
        string signType = (string)getSession("tempSignType");
        if (signType.Equals("0"))
        {
            if (SignResult.ValueText.Split(new char[]{';'})[0].Equals("Y"))
            {
                Session["signProcess"] = "Y";
            }
            else
            {
                Session["signProcess"] = "N";
            }
        }
        else
        {
            Session["signProcess"] = "Y";
        }

        Session["tempSignResult"] = SignResult.ReadOnlyText;
        Session["tempSignOpinion"] = SignOpinion.ValueText;

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
            SignOpinion.ValueText += values[0, 2];
        }

    }
}
