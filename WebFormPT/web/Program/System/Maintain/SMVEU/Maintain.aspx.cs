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
using WebServerProject.maintain.SMVE;

public partial class Program_System_Maintain_SMVEU_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string matchDept = "";
                getMatchDept(engine, ref matchDept);
                string mainDept = "";
                getMainDept(engine, ref mainDept);

                SMVEAgent agent = new SMVEAgent();
                agent.engine = engine;

                string whereClause = "SMVEAAA004='Y' and ((SMVEAAA007='') or ((SMVEAAA007<>'') and (SMVEAAA008='Y') and (SMVEAAA007 in " + matchDept + ")) or ((SMVEAAA007<>'') and (SMVEAAA008='N') and (SMVEAAA007 in " + mainDept + "))) and SMVEAAA002<='" + DateTimeUtility.getSystemTime2(null).Substring(0, 10) + "' and SMVEAAA003>='" + DateTimeUtility.getSystemTime2(null).Substring(0, 10) + "'";
                
                bool res = agent.query(whereClause);
                engine.close();

                if (!engine.errorString.Equals(""))
                {
                    throw new Exception(engine.errorString);
                }

                DataObjectSet dos = agent.defaultData;
                string[,] strOrderby = new string[,] { { "SMVEAAA002", DataObjectConstants.DESC } };
                dos.sort(strOrderby);

                string strHTML = "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">";
                for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                {
                    SMVEAAA dotemp = (SMVEAAA)dos.getAvailableDataObject(i);
                    
                    //start 系統公告明細視窗路徑
                    string ObjectID = com.dsc.kernal.utility.IDProcessor.getID("");
                    Page.Session[ObjectID] = dotemp;
                    string param = "ObjectID=" + ObjectID + "&isNew=false&NoAdd=1&NoDelete=1&NoModify=0";
                    string strHeight = "560px", strWidth = "600px";
                    //end 系統公告明細視窗路徑

                    strHTML += "    <tr class=\"BulletinTitle\">";
                    if (dotemp.SMVEAAA009.Equals("Y"))
                    {
                        strHTML += "    <td style=\"width:13px\" onclick=\"" + showDialog("Detail.aspx", param, strHeight, strWidth, "", "", "", "", "", "YES", "AUTO", "NO", "") + "\"><img src=\"../../../../Images/attachForSMVE.gif\" alt=\"此公告有附件\" /></td>";
                    }
                    else
                    {
                        strHeight = "510px";
                        strWidth = "600px";
                        strHTML += "    <td style=\"width:13px\"></td>";
                    }
                    strHTML += "        <td align=\"left\" style=\"padding-left:5px\"><b>" + dotemp.SMVEAAA005.ToString() + "</b></td>";
                    strHTML += "        <td style=\"width:150px\" align=\"right\">" + dotemp.SMVEAAA002.ToString() + "~" + dotemp.SMVEAAA003.ToString() + "</td>";
                    strHTML += "    </tr>";
                    string contentStr = dotemp.SMVEAAA006;
                    contentStr = DSCWebControl.DSCRichEdit.getFilterText(contentStr);

                    if (contentStr.Length > 70)
                    {
                        strHTML += "    <tr class=\"BulletinContent\" style={cursor:pointer} onclick=\"" + showDialog("Detail.aspx", param, strHeight, strWidth, "", "", "", "", "", "YES", "AUTO", "NO", "") + "\">";
                        strHTML += "        <td style=\"width:13px\"></td>";
                        strHTML += "        <td align=\"left\" style=\"padding-left:5px\">" + contentStr.Substring(0, 70).Replace("\n", "<br>") + "...</td>";
                        strHTML += "        <td style=\"width:150px\" align=\"right\"><font color=\"red\" size=\"2\"><<詳全文>></font></td>";
                    }
                    else
                    {
                        strHTML += "    <tr class=\"BulletinContent\">";
                        strHTML += "        <td style=\"width:13px\"></td>";
                        strHTML += "        <td colspan=\"2\" align=\"left\" style=\"padding-left:5px\">" + contentStr.ToString().Replace("\n", "<br>") + "</td>";
                    }
                    strHTML += "    </tr>";
                    strHTML += "    <tr style=\"height:5px\"><td colspan=\"3\" style=\"font-size:4px\">&nbsp;</td></tr>";
                }
                strHTML += "</table>";

                aspLiteral.Text = strHTML;
            }
        }
    }

    private string showDialog(string url, string GetParam, string dialogHeight, string dialogWidth, string dialogLeft, string dialogTop, string center, string dialogHide, string edge, string resizable, string scroll, string status, string unadorned)
    {
        //調整URL
        string curPath = Page.Server.MapPath(Page.Request.Path);
        string serverPath = Page.Server.MapPath("~");

        string targetPath;

        if ((url.Length >= 7) && (url.ToUpper().Substring(0, 7).Equals("HTTP://")))
        {
            //http開頭
            targetPath = url;
        }
        else if ((url.Length >= 8) && (url.ToUpper().Substring(0, 8).Equals("HTTPS://")))
        {
            //https開頭
            targetPath = url;
        }
        else if ((url.Length > 1) && (url.Substring(0, 1).Equals("/")))
        {
            //絕對路徑
            targetPath = Page.Server.MapPath("~" + url);
            targetPath = targetPath.Substring(serverPath.Length, targetPath.Length - serverPath.Length);
        }
        else if ((url.Length > 1) && (url.Substring(0, 1).Equals("~")))
        {
            //絕對路徑
            targetPath = Page.Server.MapPath(url);
            targetPath = targetPath.Substring(serverPath.Length, targetPath.Length - serverPath.Length);
        }
        else
        {
            //相對路徑
            string[] pAry = com.dsc.kernal.utility.Utility.getPathArray(Page.Request.Path);
            string pTag = "";
            for (int i = 0; i < pAry.Length - 1; i++)
            {
                pTag += "/" + pAry[i];
            }
            pTag += "/" + url;
            targetPath = Page.Server.MapPath(pTag);
            targetPath = targetPath.Substring(serverPath.Length, targetPath.Length - serverPath.Length);
        }

        //串Features
        string sFeatures = "";
        if ((dialogHeight != null) && (!dialogHeight.Equals("")))
        {
            sFeatures += "dialogHeight:" + dialogHeight + ";";
        }
        if ((dialogWidth != null) && (!dialogWidth.Equals("")))
        {
            sFeatures += "dialogWidth:" + dialogWidth + ";";
        }
        if ((dialogLeft != null) && (!dialogLeft.Equals("")))
        {
            sFeatures += "dialogLeft:" + dialogLeft + ";";
        }
        if ((dialogTop != null) && (!dialogTop.Equals("")))
        {
            sFeatures += "dialogTop:" + dialogTop + ";";
        }
        if ((center != null) && (!center.Equals("")))
        {
            sFeatures += "center:" + center + ";";
        }
        if ((dialogHide != null) && (!dialogHide.Equals("")))
        {
            sFeatures += "dialogHide:" + dialogHide + ";";
        }
        if ((edge != null) && (!edge.Equals("")))
        {
            sFeatures += "edge:" + edge + ";";
        }
        if ((resizable != null) && (!resizable.Equals("")))
        {
            sFeatures += "resizable:" + resizable + ";";
        }
        if ((scroll != null) && (!scroll.Equals("")))
        {
            sFeatures += "scroll:" + scroll + ";";
        }
        if ((status != null) && (!status.Equals("")))
        {
            sFeatures += "status:" + status + ";";
        }
        if ((unadorned != null) && (!unadorned.Equals("")))
        {
            sFeatures += "unadorned:" + unadorned + ";";
        }

        string tag = targetPath + "#####" + GetParam + "#####" + sFeatures;

        string RedirectDialogURL = getRedirectDialogUrl();
        string targetURL = RedirectDialogURL;
        targetURL += "?URL=" + Page.Server.UrlEncode(targetPath) + "&GetParam=" + Page.Server.UrlEncode(GetParam);
        return "retv=window.showModalDialog('" + targetURL + "','','" + sFeatures + "');";

    }

    //取得目前使用者所符合的上層部門列表
    private void getMatchDept(AbstractEngine engine, ref string dept)
    {

        string str = "";
        string sql = "select organizationUnitOID from Functions where occupantOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
            getMatchDeptRec(engine, ds.Tables[0].Rows[i][0].ToString(), ref str);
        }

        sql = "select organizationUnitOID from Title where occupantOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
            getMatchDeptRec(engine, ds.Tables[0].Rows[i][0].ToString(), ref str);
        }

        sql = "select organizationUnitOID from Role where actorOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
            getMatchDeptRec(engine, ds.Tables[0].Rows[i][0].ToString(), ref str);
        }
        
        if (str.Length > 0)
        {
            str = "(" + str.Substring(0, str.Length - 1) + ")";
        }
        else
        {
            str = "('')";
        }
        dept = str;
    }
    private void getMatchDeptRec(AbstractEngine engine, string oid, ref string dept)
    {
        string sql = "select superUnitOID from OrganizationUnit where OID='" + Utility.filter(oid) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ds.Tables[0].Rows[0][0] != null) && (!ds.Tables[0].Rows[0][0].ToString().Equals("")))
            {
                dept += "'" + ds.Tables[0].Rows[0][0].ToString() + "',";
                getMatchDeptRec(engine, ds.Tables[0].Rows[0][0].ToString(), ref dept);
            }
        }
    }
    //取得目前使用者所符合的部門列表
    private void getMainDept(AbstractEngine engine, ref string dept)
    {
        string str = "";
        string sql = "select organizationUnitOID from Functions where occupantOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
        }

        sql = "select organizationUnitOID from Title where occupantOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
        }

        sql = "select organizationUnitOID from Role where actorOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        ds = engine.getDataSet(sql, "TEMP");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            str += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
        }

        if (str.Length > 0)
        {
            str = "(" + str.Substring(0, str.Length - 1) + ")";
        }
        else
        {
            str = "('')";
        }
        dept = str;
    }

}
