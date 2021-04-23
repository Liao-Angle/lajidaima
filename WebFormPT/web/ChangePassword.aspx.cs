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
using WebServerProject.maintain.SMVU;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Button1_Click(object sender, System.EventArgs e)
    {
        if (!PWD.Text.Equals(PWD2.Text))
        {
            showMessage("密碼與再次輸入密碼不同");
            return;
        }

        AbstractEngine engine = null;
        AbstractEngine mengine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            SMVUAgent agent = new SMVUAgent();
            agent.engine = engine;

            string pwd = PWD.Text;

            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string nanastr = sp.getParam("NaNaDB");
            mengine = factory.getEngine(EngineConstants.SQL, nanastr);
            string sql = "select * from Users where OID='" + Utility.filter((string)Session["UserGUID"]) + "'";            
            DataSet dsGP = mengine.getDataSet(sql, "TEMP");
             
            if (!agent.checkPasswordValid((string)Session["UserID"], pwd, dsGP.Tables[0].Rows[0]["password"].ToString()))
            {
                throw new Exception("密碼不符合安全原則");                
            }

            //這裡要將密碼寫回GP
            string hash = agent.getHashPWD(pwd);

           

            sql = "update Users set password='" + Utility.filter(hash) + "' where id='" + (String)Session["UserID"] + "'";
            if (!mengine.executeSQL(sql))
            {
                throw new Exception(mengine.errorString);
            }

            //寫回SMVT
            string ss = "update SMVTAAA set SMVTAAA003='" + Utility.filter(hash) + "' where SMVTAAA002='" + (String)Session["UserID"] + "'";
            if (!engine.executeSQL(ss))
            {
                throw new Exception(engine.errorString);
            }

            //寫回UserSetting
            ss = "update USERSETTING set LASTPWDCHANGE='" + DateTimeUtility.getSystemTime2(null).Substring(0,10) + "' where USERGUID='" + (string)Session["UserGUID"] + "'";
            if (!engine.executeSQL(ss))
            {
                throw new Exception(engine.errorString);
            }


            mengine.close();
            engine.close();

            Response.Write("<script language=javascript>");
            Response.Write("alert('密碼變更完成, 請重新登入');");
            Response.Write("window.location.href='Login.aspx';");
            Response.Write("</script>");
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            try
            {
                mengine.close();
            }
            catch { };
            showMessage(te.Message);
            return;
        }
    }
    private void showMessage(string msg)
    {
        Response.Write("<script language=javascript>");
        Response.Write("alert('" + msg.Replace(System.Environment.NewLine, "\\n") + "');");
        Response.Write("</script>");
    }
}
