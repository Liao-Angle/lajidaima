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

public partial class MainFrame_RestoreSession : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userid = (string)Session["UserID"];
        if (userid == null)
        {
            Response.Write("<script language=javascript>");
            Response.Write("alert('" + com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string028", "您的閒置時間過久，系統已清除您的Session值。請重新登入。") + "');");
            Response.Write("parent.window.pressQuit='1';");
            Response.Write("parent.window.location.href='../Default.aspx';");
            Response.Write("</script>");
        }
        else
        {
            //檢察是否有系統訊息
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            string sql;
            DataSet ds;

            bool isShowMsg = true;

            sql = "select RECEIVEMSG from USERSETTING where USERGUID='" + Utility.filter((string)Session["UserGUID"]) + "'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString().Equals("N"))
                {
                    isShowMsg = false;
                }
            }
			
			OnlineUser.OnlineUsers olu = new OnlineUser.OnlineUsers();

            if (isShowMsg)
            {
                sql = "select top 1 * from SMVDAAA where SMVDAAA004<='" + DateTimeUtility.getSystemTime2(null) + "' and SMVDAAA001 not in (select SMVDAAB002 from SMVDAAB where SMVDAAB003='" + (String)Session["UserID"] + "')";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string GUID = ds.Tables[0].Rows[0]["SMVDAAA001"].ToString();
                    string messageType = ds.Tables[0].Rows[0]["SMVDAAA003"].ToString();
                    string alertTime = ds.Tables[0].Rows[0]["SMVDAAA004"].ToString();
                    string title = ds.Tables[0].Rows[0]["SMVDAAA007"].ToString();
                    string content = ds.Tables[0].Rows[0]["SMVDAAA008"].ToString().Replace("\n", " ").Replace("\r", " ");
                    string url = ds.Tables[0].Rows[0]["SMVDAAA009"].ToString();

                    sql = "insert into SMVDAAB(SMVDAAB001, SMVDAAB002, SMVDAAB003, SMVDAAB004, SMVDAAB005, SMVDAAB006, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) values(";
                    sql += "'" + IDProcessor.getID("") + "',";
                    sql += "'" + GUID + "',";
                    sql += "'" + (string)Session["UserID"] + "',";
                    sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                    sql += "'N',";
                    sql += "'',";
                    sql += "'System',";
                    sql += "'" + DateTimeUtility.getSystemTime2(null) + "',";
                    sql += "'',";
                    sql += "'')";
                    engine.executeSQL(sql);

                    Response.Write("<script language=javascript>");
                    Response.Write("parent.window.showAlert('SMVDAAA','" + GUID + "','" + messageType + "','" + alertTime + "','" + title + "','" + content + "','" + url + "');");
                    Response.Write("</script>");

                }

                sql = "select top 1 * from SMVCAAA where SMVCAAA002='" + Utility.filter(userid) + "' and SMVCAAA004<='" + DateTimeUtility.getSystemTime2(null) + "' and SMVCAAA006='N'";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string GUID = ds.Tables[0].Rows[0]["SMVCAAA001"].ToString();
                    string messageType = ds.Tables[0].Rows[0]["SMVCAAA003"].ToString();
                    string alertTime = ds.Tables[0].Rows[0]["SMVCAAA004"].ToString();
                    string title = ds.Tables[0].Rows[0]["SMVCAAA007"].ToString();
                    string content = ds.Tables[0].Rows[0]["SMVCAAA008"].ToString().Replace("\n", " ").Replace("\r", " ");
                    string url = ds.Tables[0].Rows[0]["SMVCAAA009"].ToString();

                    ds.Tables[0].Rows[0]["SMVCAAA006"] = "Y";
                    engine.updateDataSet(ds);

                    Response.Write("<script language=javascript>");
                    Response.Write("parent.window.showAlert('SMVCAAA','" + GUID + "','" + messageType + "','" + alertTime + "','" + title + "','" + content + "','" + url + "');");
                    Response.Write("</script>");

                }
            }
            engine.close();


        }
    }
}
