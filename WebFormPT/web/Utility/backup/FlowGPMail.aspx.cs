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

            //by Ted 記錄是否為簽核之 email
            string tag2 = "";

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

                sw.WriteLine("");
                sw.WriteLine("******************************");
                sw.WriteLine("Log time: " + DateTimeUtility.getSystemTime2(null).Substring(0, 19));
                sw.WriteLine(pUserId);
                sw.WriteLine(pSubject);
                sw.WriteLine(pMessage);
                sw.WriteLine("To: " + pAddress);
                sw.WriteLine("From: " + pSenderAddress);
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

                //by Ted 將簽核 email 網址中的 WorkItemOID 記錄下來
                tag2 = tag;

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

            //通知 email 主旨與內容修改, 將[簽核]替換為[通知] by Ted, 2013/4/18                                   
            int ic = content.IndexOf("通知事項");            
            if (ic>0)
            {
                //表示此Mail是通知的 email 把主旨與內文有[簽核]字樣更換為 [通知]
                pSubject = pSubject.Replace("[簽核]", "[通知]");
                content = content.Replace("[簽核]", "[通知]");
                if (isDebugPage.Equals("Y"))
                {
                    string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                    fname = Server.MapPath("~/LogFolder/" + fname + "_mailback.log");
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
                    sw.WriteLine("Info Subject:" + pSubject);                    
                    sw.Close();

                }
            }

            //退回重辦的 mail 主旨與內容修改, 將[簽核]替換為[退件重辦] by Ted, 2013/4/18 
            ic = content.LastIndexOf("##");            
            if (tag2 != "" && ic > 0) //只處理簽核的 mail 並且找到最後一個 // 位置
            {
                //讀取上一關(content 最後一個) 的簽核意見,判斷是否為 "退回重辦"
                string opn = content.Substring(ic - 4, 4);

                if (opn == "退回重辦") //如果上一關是退回重辦,才更換主旨成退件重辦
                {                    
                    pSubject = pSubject.Replace("[簽核]", "[退件重辦]");
                    content = content.Replace("[簽核]", "[退件重辦]");
                    if (isDebugPage.Equals("Y"))
                    {
                        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                        fname = Server.MapPath("~/LogFolder/" + fname + "_mailback.log");
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
                        sw.WriteLine("Reject Subject:" + pSubject);
                        sw.Close();

                    }
                }
            }

            //最後的content是替代pMessage
            //Response.Write(content);            

            //通知代理人 (先考慮一層代理) by Ted, 2013/4/17
            string substAdr = "";
            if (tag2 != "")  //只處理簽核 email            
            {
                //找出代理人之 email
                string sql2 = "SELECT mailAddress FROM UserWorkAssignment WHERE SignoffType='2' and workItemOID='" + Utility.filter(tag2) + "'";
                DataSet checkd2 = engine.getDataSet(sql2, "TEMP");
                if (checkd2.Tables[0].Rows.Count > 0)
                {
                    substAdr = checkd2.Tables[0].Rows[0][0].ToString();
                    if (isDebugPage.Equals("Y"))
                    {
                        string fname2 = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                        fname2 = Server.MapPath("~/LogFolder/" + fname2 + "_mailback.log");
                        System.IO.StreamWriter sw2 = new System.IO.StreamWriter(fname2, true);
                        sw2.WriteLine("substAdr:" + substAdr);
                        //sw2.WriteLine("******************************");                                               
                        sw2.Close();
                    }
                    checkd2 = null;
                }                
            }

            //如果為改派, 並且代理人已簽核, email 就不發出 (解決代理人簽核完單據後會發出改派 email 之問題!!!) by Ted, 2013/4/24
            string reassignFlag = "N";
            ic = 0;
            ic = pSubject.IndexOf("改派通知"); //主旨為改派通知
            if (tag2 != "" && ic > 0) //只處理簽核的 mail 並且是改派的 email
            {
                //如何判斷是正常轉派,或是已代理簽核呢?
                //由GP取得資料
                SysParam sp = new SysParam(engine);
                string nanacon = sp.getParam("NaNaDB");
                string header = sp.getParam("EmailHeader");

                AbstractEngine nana = factory.getEngine(EngineConstants.SQL, nanacon);                
                //System.Threading.Thread.Sleep(2000);

                string sql = "select w.OID, w.workItemName, rd.reassignmentType from WorkItem w inner join ReassignWorkItemAuditData rd on w.OID=rd.sourceOID where w.OID='" + Utility.filter(tag2) + "'";
                DataSet nds = nana.getDataSet(sql, "TEMP");

                if (nds.Tables[0].Rows.Count > 0)
                    if (nds.Tables[0].Rows[0][2].ToString().Equals("3")) //reassignmentType- 0:人工轉派, 3:系統自動轉派
                    {
                        reassignFlag = "Y";
                        if (isDebugPage.Equals("Y"))
                        {
                            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                            fname = Server.MapPath("~/LogFolder/" + fname + "_mailback.log");
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
                            sw.WriteLine("Subject:" + pSubject);
                            sw.WriteLine("備註: 此為代理人簽核之後, 所發出之改派通知 email, 此郵件不發送給:" + pAddress);
                            sw.Close();
                        }
                    }
                /* 這個寫法最後一關有 bug!
                string sql = "select OID, workItemName, currentState from WorkItem where OID='" + Utility.filter(tag2) + "'";
                DataSet nds = nana.getDataSet(sql, "TEMP");

                if (nds.Tables[0].Rows.Count > 0)
                    if (nds.Tables[0].Rows[0][2].ToString() != "0") //currentState- 0:待簽核(人工轉派), 3:已簽核 (代理人轉派)
                    {
                        reassignFlag = "Y";
                        if (isDebugPage.Equals("Y"))
                        {
                            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                            fname = Server.MapPath("~/LogFolder/" + fname + "_mailback.log");
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
                            sw.WriteLine("Subject:" + pSubject);
                            sw.WriteLine("此為代理人簽核之後, 所發出之改派通知 email, 此郵件不發送給:" + pAddress);
                            sw.Close();
                        }
                    }
                */
                if (isDebugPage.Equals("Y"))
                {
                    string fname2 = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                    fname2 = Server.MapPath("~/LogFolder/" + fname2 + "_mailback.log");
                    System.IO.StreamWriter sw2 = new System.IO.StreamWriter(fname2, true);
                    sw2.WriteLine("OID:" + nds.Tables[0].Rows[0][0].ToString());
                    sw2.WriteLine("workItemName:" + nds.Tables[0].Rows[0][1].ToString());
                    sw2.WriteLine("State:" + nds.Tables[0].Rows[0][2].ToString());
                    sw2.WriteLine("reassignFlag:" + reassignFlag);
                    sw2.Close();
                }
                nana.close();                
            }

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

            //正常發 mail, 且不是發已簽核的轉派 email, by Ted add resassignFlag 判斷不發已簽核之轉派 email
            if (sendAdr.Length>0 && reassignFlag.Equals("N"))
            {
                sendAdr = sendAdr.Substring(0, sendAdr.Length - 1);
                SysParam sp = new SysParam(engine);
                string mailclass = sp.getParam("MailClass");
                MailFactory fac = new MailFactory();
                AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);

                au.sendMailHTML("", "127.0.0.1", sendAdr, "", pSenderAddress, pSubject, content);
                //au.sendMail("", "127.0.0.1", sendAdr, pSenderAddress, pSubject, content);

                if (isDebugPage.Equals("Y"))
                {
                    string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                    fname = Server.MapPath("~/LogFolder/" + fname + "_mailback.log");
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
                    sw.WriteLine("Mail to receipt:" + sendAdr + " Successfully!");
                    sw.Close();

                }
            }

            //單獨發 mail 給代理人, by Ted, 因為 mail 元件一次無法發給多人
            if (substAdr.Length > 0 && reassignFlag.Equals("N"))
            {
                sendAdr = sendAdr.Substring(0, sendAdr.Length - 1);
                SysParam sp = new SysParam(engine);
                string mailclass = sp.getParam("MailClass");
                MailFactory fac = new MailFactory();
                AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);

                pSubject = pSubject.Replace("[簽核]", "[代理簽核]");
                content = content.Replace("[簽核]", "[代理簽核]");

                au.sendMailHTML("", "127.0.0.1", substAdr, "", pSenderAddress, pSubject, content);
                if (isDebugPage.Equals("Y"))
                {
                    string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
                    fname = Server.MapPath("~/LogFolder/" + fname + "_mailback.log");
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
                    sw.WriteLine("Mail to receipt(sbustitute):" + substAdr + " Successfully!");
                    sw.Close();
                }
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
