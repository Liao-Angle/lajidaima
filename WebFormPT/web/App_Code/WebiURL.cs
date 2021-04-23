using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

/// <summary>
/// WebiLogin 的摘要描述
/// </summary>
public class WebiURL
{
    public WebiURL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //

    }

    public static string composeWebiURL(AbstractEngine engine, string userid, string password, string[] usergroup, string reportID, string RCA)
    {
        string sql = "select SMRCAAA002, SMRCAAA003, SMRCAAA004, SMRCAAA005, SMRCAAA006, SMRCAAA007 from SMRCAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        string verifyType = ds.Tables[0].Rows[0]["SMRCAAA002"].ToString();
        string trustedAuthentication = ds.Tables[0].Rows[0]["SMRCAAA007"].ToString();

        string url = "";

        if (RCA.Equals(""))
        {
            url = ds.Tables[0].Rows[0]["SMRCAAA003"].ToString();
            url += "&iDocID=" + reportID;
        }
        else
        {
            url = ds.Tables[0].Rows[0][RCA].ToString();
        }
        string verifyURL = ds.Tables[0].Rows[0]["SMRCAAA004"].ToString();

        string uid = "";
        string pwd = "";
        if (verifyType.Equals("0"))
        {
            string sts = "'*'";
            string[] gp = usergroup;
            for (int i = 0; i < gp.Length; i++)
            {
                sts += ",'" + gp[i] + "'";
            }
            sql = "select SMRDAAA003, SMRDAAA004 from SMRDAAA where SMRDAAA002 in (" + sts + ")";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                WebServerProject.SysParam sp = new WebServerProject.SysParam(null);
                uid = ds.Tables[0].Rows[0][0].ToString();
                if (!ds.Tables[0].Rows[0][1].ToString().Equals(""))
                {
                    pwd = sp.decode(ds.Tables[0].Rows[0][1].ToString());
                }
                else
                {
                    pwd = "";
                }
            }
        }
        else
        {
            uid = userid;
            pwd = password;
        }
        com.dsc.kernal.webservice.WSDLClient wc = new com.dsc.kernal.webservice.WSDLClient(verifyURL);
        wc.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
        wc.build(false);

        string token = "";
        if (trustedAuthentication.Equals("Y"))
        {
            token = (string)wc.callSync("getLogonTokenTrusted", uid);
        }
        else
        {
            token = (string)wc.callSync("getLogonToken", uid, pwd);
        }

        if (!token.Equals(""))
        {
            if (!RCA.Equals(""))
            {
                url += "?ivsLogonToken=" + token;
            }
            else
            {
                url += "&token=" + token;
            }
        }
        return url;
    }
}
