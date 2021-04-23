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
using WebServerProject;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.mail;
using System.Text.RegularExpressions;

public partial class Utility_FlowGPMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        AbstractEngine engine = null;
        try
        {
            string pUserId = Request.Form["pUserId"];
            string pSubject = Request.Form["pSubject"];
            string pMessage = Request.Form["pMessage"].Replace("\t", "<");
            string pAddress = Request.Form["pAddress"];
            string pSenderAddress = Request.Form["pSenderAddress"];

            string content = pMessage;

            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(acs.engineType, acs.connectString);

            string sqlz = "select SMVPAAA009 from SMVPAAA";
            DataSet dss = engine.getDataSet(sqlz, "TEMP");
            string isDebugPage = "N";
            if (dss.Tables[0].Rows.Count == 0)
            {
                isDebugPage = "N";
            }
            else
            {
                isDebugPage = dss.Tables[0].Rows[0][0].ToString();
            }

            if (isDebugPage.Equals("Y"))
            {
                string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                fname = Server.MapPath("~/LogFolder/" + fname + "_mailback.log");
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);

                sw.WriteLine("**************");
                sw.WriteLine(pUserId);
                sw.WriteLine(pSubject);
                sw.WriteLine(pMessage);
                sw.WriteLine(pAddress);
                sw.WriteLine(pSenderAddress);
                sw.Close();

            }
            string mas = "<a href=\"http:.*performWorkFromMail\\S*\">";
            System.Text.RegularExpressions.Regex reg = null;
            MatchCollection mc = null;
            //比對簽核

            reg = new Regex(mas);
            mc = reg.Matches(content);
            if (mc.Count > 0)
            {
                //表示Mail裡面有簽核連結, 需要轉換
                string wording = "";
                IEnumerator ie = mc.GetEnumerator();
                while (ie.MoveNext())
                {
                    wording = ie.Current.ToString();
                }

                //取得字串中的hdnWorkItemOID
                int c = wording.IndexOf("hdnWorkItemOID");
                string tag = wording.Substring(c + 15, wording.Length - c - 17);

                //由GP取得資料
                SysParam sp = new SysParam(engine);
                string nanacon = sp.getParam("NaNaDB");
                string header = sp.getParam("EmailHeader");

                AbstractEngine nana = factory.getEngine(EngineConstants.SQL, nanacon);
		//20100820 Eric Hsu 避免GP DB I/O未完成,便進行資料讀取
		System.Threading.Thread.Sleep(2000);
                string sql = "select workItemName, serialNumber from WorkItem w inner join ProcessInstance p on w.contextOID=p.contextOID where w.OID='" + Utility.filter(tag) + "'";
                DataSet nds = nana.getDataSet(sql, "TEMP");

                string news = "<a target=_blank href='" + header + "?runMethod=showSignForm&workItemOID=" + tag + "&processSerialNumber=" + nds.Tables[0].Rows[0][1].ToString() + "&workItemName=" + Server.UrlEncode(nds.Tables[0].Rows[0][0].ToString()) + "'>";

                nana.close();

                content = content.Replace(wording, news);
            }

            //比對原稿
            mas = "<a href=\"http:.*traceProcessFromMail\\S*\">";
            reg = new Regex(mas);
            mc = reg.Matches(content);
            if (mc.Count > 0)
            {
                //表示Mail裡面有原稿連結, 需要轉換
                string wording = "";
                IEnumerator ie = mc.GetEnumerator();
                while (ie.MoveNext())
                {
                    wording = ie.Current.ToString();
                }

                //取得字串中的hdnWorkItemOID
                int c = wording.IndexOf("hdnProcessInstOID");
                string tag = wording.Substring(c + 18, wording.Length - c - 20);


                //由GP取得資料
                SysParam sp = new SysParam(engine);
                string nanacon = sp.getParam("NaNaDB");
                string header = sp.getParam("EmailHeader");

                AbstractEngine nana = factory.getEngine(EngineConstants.SQL, nanacon);

                string sql = "select serialNumber from ProcessInstance where OID='" + Utility.filter(tag) + "'";
                DataSet nds = nana.getDataSet(sql, "TEMP");

                string news = "<a target=_blank href='" + header + "?runMethod=showReadOnlyForm&processSerialNumber=" + nds.Tables[0].Rows[0][0].ToString() + "'>";

                nana.close();

                content = content.Replace(wording, news);
            }

            //最後的content是替代pMessage
            //Response.Write(content);

            //這裡要檢查該員是某要寄
            string spTag = "";
            if (pAddress.IndexOf(",") >= 0)
            {
                spTag = ",";
            }
            else
            {
                spTag = ";";
            }
            string sendAdr = "";
            string[] padr = pAddress.Split(new string[] { spTag }, StringSplitOptions.None);
            for (int x = 0; x < padr.Length; x++)
            {
                string ssl = "select OID from Users where mailAddress='" + padr[x].Trim() +"'";
                DataSet checkd = engine.getDataSet(ssl, "TEMP");

                bool isSend = true;
                if (checkd.Tables[0].Rows.Count > 0)
                {
                    ssl = "select RECEIVEMAIL from USERSETTING where USERGUID='" + checkd.Tables[0].Rows[0][0].ToString() + "'";
                    checkd = engine.getDataSet(ssl, "TEMP");
                    if (checkd.Tables[0].Rows.Count > 0)
                    {
                        if (checkd.Tables[0].Rows[0][0].ToString().Equals("N"))
                        {
                            isSend = false;
                        }
                    }
                }
                if (isSend)
                {
                    sendAdr += padr[x] + ";";

                }
            }
            if (sendAdr.Length>0)
            {
                sendAdr = sendAdr.Substring(0, sendAdr.Length - 1);
                SysParam sp = new SysParam(engine);
                string mailclass = sp.getParam("MailClass");
                MailFactory fac = new MailFactory();
                AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);

                au.sendMailHTML("", "127.0.0.1", sendAdr, "", pSenderAddress, pSubject, content);
                //au.sendMail("", "127.0.0.1", sendAdr, pSenderAddress, pSubject, content);
            }
            
            engine.close();

        }
        catch (Exception ze)
        {
            try
            {
                //engine.rollback();
                engine.close();
            }
            catch { };
            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
            fname = Server.MapPath("~/LogFolder/" + fname + "_callback.log");
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
            sw.WriteLine(DateTimeUtility.getSystemTime2(null) + "===============");
            sw.WriteLine(ze.StackTrace);
            sw.WriteLine(ze.Message);
            sw.Close();

            //Response.StatusCode = 500;
        }
        
    }
}
