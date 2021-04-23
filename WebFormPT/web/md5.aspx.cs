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
using System.IO;
using System.Text.RegularExpressions;
using WebServerProject;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Runtime.Remoting;
public partial class md5 : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
                com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
                com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
                acs.getConnectionString();

                BYWin1.connectDBString = acs.connectString;
                BYWin1.clientEngineType = acs.engineType;
                BYWin1.HiddenIndex = new int[] { 0, 3 };

            }
        }
        /*
        Assembly asm = Assembly.LoadFile(Server.MapPath("~/Bin/WebServerProject.dll"));
        Type[] type = asm.GetTypes();
        for (int i = 0; i < type.Length; i++)
        {
            if (type[i].BaseType.Name.Equals("DataObject"))
            {
                Response.Write(type[i].Name + ":" + type[i].BaseType.Name + "<br>");
            }
        }
        */
        /*
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(acs.engineType, acs.connectString);

        string sql = "select count(*) as CCS, UserName from LoginData group by UserName";
        DataSet data = engine.getDataSet(sql, "TEMP");
        engine.close();

        Bars.TagField = "UserName";
        Bars.ValueField = "CCS";
        
        Bars.setData(data);
        //Bars.updateGraph();
        */
        /*
        System.IO.StreamReader sr = new StreamReader(@"D:\z.txt");
        string content = sr.ReadToEnd();
        sr.Close();

        string header="http://127.0.0.1/WebFormPT/";
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(acs.engineType, acs.connectString);

        string mas = "<a href=\"http:.*performWorkFromMail\\S*\">";
        System.Text.RegularExpressions.Regex reg = null;
        MatchCollection mc = null;
        //比對簽核
        
        reg = new Regex(mas);
        mc=reg.Matches(content);
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
            int c=wording.IndexOf("hdnWorkItemOID");
            string tag = wording.Substring(c + 15, wording.Length-c-17);
            
            //由GP取得資料
            SysParam sp = new SysParam(engine);
            string nanacon = sp.getParam("NaNaDB");

            AbstractEngine nana = factory.getEngine(EngineConstants.SQL, nanacon);

            string sql = "select workItemName, serialNumber from WorkItem w inner join ProcessInstance p on w.contextOID=p.contextOID where w.OID='" + tag + "'";
            DataSet nds = nana.getDataSet(sql, "TEMP");

            string news = "<a href='"+header+"?runMethod=showSignForm&workItemOID=" + tag + "&processSerialNumber=" + nds.Tables[0].Rows[0][1].ToString() + "&workItemName=" + nds.Tables[0].Rows[0].ToString()+"'>";

            nana.close();

            content=content.Replace(wording, news);
        }
        
        //比對原稿
        mas = "<a href=\"http:.*traceProcessFromExternalWeb\\S*\">";
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

            AbstractEngine nana = factory.getEngine(EngineConstants.SQL, nanacon);

            string sql = "select serialNumber from ProcessInstance where OID='"+tag+"'";
            DataSet nds = nana.getDataSet(sql, "TEMP");

            string news = "<a href='" + header + "?runMethod=showReadOnlyForm&processSerialNumber=" + nds.Tables[0].Rows[0][0].ToString() + "'>";

            nana.close();

            content = content.Replace(wording, news);
        }

        Response.Write(content);

        engine.close();
        */

        //Response.Write(Request.QueryString);

        //Response.Write(Server.HtmlEncode("<"));
        /*
        Response.Write(Request.UserAgent);

        //System.Security.Cryptography.SHA1 md = (System.Security.Cryptography.SHA1)System.Security.Cryptography.MD5.Create("SHA1");

        System.Security.Cryptography.SHA1 md = System.Security.Cryptography.SHA1.Create();
        
        string oriPwd = "1234";
        String tSalt = "abcedefghijklmnopqrstuvwxyz";

        //StringReader sr = new StringReader(oriPwd);
        byte[] oriData = System.Text.Encoding.Default.GetBytes(tSalt+oriPwd);
        //byte[] saltData = System.Text.Encoding.Default.GetBytes(tSalt);

        //byte[] cmbData = combineByteArray(oriData, saltData);

        byte[] data = md.ComputeHash(oriData);

        //byte[] cData = combineByteArray(data, oriData);

        string hashPwd = "";
        hashPwd = com.dsc.kernal.utility.Base64.encode(data);

        Response.Write(hashPwd);
        */
    }

    private byte[] combineByteArray(byte[] bt1, byte[] bt2)
    {
        byte[] ret = new byte[bt1.Length + bt2.Length];
        for (int i = 0; i < bt1.Length; i++)
        {
            ret[i] = bt1[i];
        }
        for (int i = 0; i < bt2.Length; i++)
        {
            ret[i + bt1.Length] = bt2[i];
        }
        return ret;
    }
}
