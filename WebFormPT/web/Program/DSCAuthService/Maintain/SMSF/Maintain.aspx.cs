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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;

public partial class Program_DSCAuthService_Maintain_SMSF_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SMSF";
        ApplicationID = "SYSTEM";
        ModuleID = "SMSAA";

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                CheckProgram.clientEngineType = engineType;
                CheckProgram.connectDBString = connectString;
                CheckProgram.clientEngineType = engineType;
                CheckProgram.connectDBString = connectString;

                UserAuthList.NoAdd = true;
                UserAuthList.NoModify = true;
                UserAuthList.NoDelete = true;

            }
        }
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (CheckProgram.GuidValueText.Equals(""))
        {
            //MessageBox("請選擇要查詢的程式名稱");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "QueryError", "請選擇要查詢的程式名稱"));
            return;
        }
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            LogonFactory lfac = new LogonFactory();
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

            string loginStr = sp.getParam("LogonClass");
            if (loginStr.Equals(""))
            {
                loginStr = "WebServerProject.system.login.GPUserLogin";
            }
            string loginAsm = loginStr.Split(new char[] { '.' })[0].Trim();

            AbstractLogon log = lfac.getLogonObject(loginAsm, loginStr);
            
            DataObjectFactory dof = new DataObjectFactory();
            dof.init();
            dof.assemblyName = "WebServerProject";
            dof.childClassString = "WebServerProject.temp.ProgramUser";
            dof.tableName = "ProgramUser";
            dof.addFieldDefinition("UserID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "UserID", "使用者代號"), "");
            dof.addFieldDefinition("UserName", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "UserName", "使用者名稱"), "");
            dof.addFieldDefinition("AUTH01", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH01", "讀取"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH02", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH02", "新增"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH03", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH03", "刪除"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH04", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH04", "修改"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH05", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH05", "列印"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH06", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH06", "報表"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH07", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH07", "擁有"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH08", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH08", "權限08"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH09", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH09", "權限09"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH10", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "AUTH10", "權限10"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("D_INSERTUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "D_INSERTUSER", "建立者"), "");
            dof.addFieldDefinition("D_INSERTTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "D_INSERTTIME", "建立時間"), "");
            dof.addFieldDefinition("D_MODIFYUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "D_MODIFYUSER", "更新者"), "");
            dof.addFieldDefinition("D_MODIFYTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsf_maintain_aspx.language.ini", "message", "D_MODIFYTIME", "更新時間"), "");
            dof.addIdentityField("UserID");
            dof.addKeyField("UserID");
            dof.allowAllFieldEmpty();

            string xml = dof.generalXML();

            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.dataObjectSchema = xml;

            Hashtable hs = null;

            WebServerProject.auth.AUTHAgent ag = new WebServerProject.auth.AUTHAgent();
            ag.engine = engine;

            DataSet userList = null;
            string sql = "";

            sql = "select E.SMSAABC003 as UserID, F.userName as UserName From SMVAAAB A left join SMSAAAA B On A.SMVAAAB009 = B.SMSAAAA002 left join SMSAABB C On B.SMSAAAA002 = C.SMSAABB003 left join SMSAABA D On C.SMSAABB002 = D.SMSAABA001 left join SMSAABC E On D.SMSAABA001 = E.SMSAABC002 left join Users	F On E.SMSAABC003 = F.id where SMVAAAB002 = '" + CheckProgram.ValueText + "'";

            userList = engine.getDataSet(sql, "TEMP");

            for (int i = 0; i < userList.Tables[0].Rows.Count; i++)
            {
                //取得使用者的所屬群組
                String LogonUser = userList.Tables[0].Rows[i][0].ToString();
                string[] groups = log.getAuth(engine, "", LogonUser, "");

                //取得該程式及該使用者的權限項目細項
                int auth = ag.getAuth(CheckProgram.ValueText, userList.Tables[0].Rows[i][0].ToString(), groups);


                //是否僅列出擁有任一權限的使用者
                bool isList = false;
                if (ListHave.Checked)
                {
                    if (auth > 0)
                    {
                        isList = true;
                    }
                    else
                    {
                        isList = false;
                    }
                }
                else
                {
                    isList = true;
                }

                if (isList)
                {
                    DataObject doo = dos.create();
                    doo.setData("UserID", userList.Tables[0].Rows[i][0].ToString());
                    doo.setData("UserName", userList.Tables[0].Rows[i][1].ToString());

                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.READ))
                    {
                        doo.setData("AUTH01", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.ADD))
                    {
                        doo.setData("AUTH02", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.DELETE))
                    {
                        doo.setData("AUTH03", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.MODIFY))
                    {
                        doo.setData("AUTH04", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.PRINT))
                    {
                        doo.setData("AUTH05", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.REPORT))
                    {
                        doo.setData("AUTH06", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.OWN))
                    {
                        doo.setData("AUTH07", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.AUTH08))
                    {
                        doo.setData("AUTH08", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.AUTH09))
                    {
                        doo.setData("AUTH09", "Y");
                    }
                    if (ag.parse(auth, WebServerProject.auth.AUTHAgent.AUTH10))
                    {
                        doo.setData("AUTH10", "Y");
                    }
                    dos.add(doo);
                }
            }

            UserAuthList.dataSource = dos;
            UserAuthList.updateTable();
            UserAuthList.Dispose();
            engine.close();

        }
        catch (Exception ze)
        {
            try
            {
                engine.close();
            }
            catch
            {
            }
            MessageBox(ze.Message);
            writeLog(ze);
        }
    }
}
