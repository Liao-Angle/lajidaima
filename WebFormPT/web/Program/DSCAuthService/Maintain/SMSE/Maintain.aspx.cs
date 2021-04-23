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

public partial class Program_DSCAuthService_Maintain_SMSE_Maintain : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SMSE";
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

                CheckUser.clientEngineType = engineType;
                CheckUser.connectDBString = connectString;
                CheckProgram.clientEngineType = engineType;
                CheckProgram.connectDBString = connectString;
            }
        }
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        if (CheckUser.GuidValueText.Equals(""))
        {
            //MessageBox("請選擇要查詢的使用者");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "QueryError", "請選擇要查詢的使用者"));
            return;
        }
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            String LogonUser = CheckUser.ValueText;

            LogonFactory lfac = new LogonFactory();
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string loginStr = sp.getParam("LogonClass");
            if (loginStr.Equals(""))
            {
                loginStr = "WebServerProject.system.login.GPUserLogin";
            }
            string loginAsm = loginStr.Split(new char[] { '.' })[0].Trim();
            AbstractLogon log = lfac.getLogonObject(loginAsm, loginStr);

            string[] groups = log.getAuth(engine, "", LogonUser, "");


            DataObjectFactory dof = new DataObjectFactory();
            dof.init();
            dof.assemblyName = "WebServerProject";
            dof.childClassString = "WebServerProject.temp.UserGroup";
            dof.tableName = "UserGroup";
            dof.addFieldDefinition("GROUPID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "GROUPID", "群組代號"), "");
            dof.addFieldDefinition("GROUPNAME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "GROUPNAME", "群組名稱"), "");
            dof.addFieldDefinition("D_INSERTUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_INSERTUSER", "建立者"), "");
            dof.addFieldDefinition("D_INSERTTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_INSERTTIME", "建立時間"), "");
            dof.addFieldDefinition("D_MODIFYUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_MODIFYUSER", "更新者"), "");
            dof.addFieldDefinition("D_MODIFYTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_MODIFYTIME", "更新時間"), "");
            dof.addIdentityField("GROUPID");
            dof.addKeyField("GROUPID");
            dof.allowAllFieldEmpty();
            string xml = dof.generalXML();

            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.dataObjectSchema = xml;

            string tag = "'*'";
            for (int i = 0; i < groups.Length; i++)
            {
                tag += ",'" + groups[i] + "'";
            }
            string sql = "select SMSAABA002, SMSAABA003 from SMSAABA where SMSAABA002 in (" + tag + ")";
            DataSet allGroupName = engine.getDataSet(sql, "TEMP");

            for (int i = 0; i < groups.Length; i++)
            {
                DataObject doo = dos.create();
                doo.setData("GROUPID", groups[i]);
                for (int j = 0; j < allGroupName.Tables[0].Rows.Count; j++)
                {
                    if (groups[i].Equals(allGroupName.Tables[0].Rows[j][0].ToString()))
                    {
                        doo.setData("GROUPNAME", allGroupName.Tables[0].Rows[j][1].ToString());
                    }
                }

                dos.add(doo);
            }
            GroupList.NoAdd = true;
            GroupList.NoModify = true;
            GroupList.NoDelete = true;
            GroupList.dataSource = dos;
            GroupList.updateTable();
            allGroupName.Dispose();

            Hashtable hs = null;
            WebServerProject.auth.AUTHAgent ag = new WebServerProject.auth.AUTHAgent();
            ag.engine = engine;

            DataSet programList = null;
            if ((CheckProgram.GuidValueText == null) || (CheckProgram.GuidValueText.Equals("")))
            {
                sql = "select SMVAAAB002, SMVAAAB003 from SMVAAAB";
            }
            else
            {
                sql = "select SMVAAAB002, SMVAAAB003 from SMVAAAB where SMVAAAB002='" + CheckProgram.ValueText + "'";
            }
            programList = engine.getDataSet(sql, "TEMP");

            dof.init();
            dof.assemblyName = "WebServerProject";
            dof.childClassString = "WebServerProject.temp.UserProgram";
            dof.tableName = "UserProgram";
            dof.addFieldDefinition("ProgramID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "ProgramID", "程式代號"), "");
            dof.addFieldDefinition("ProgramName", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "ProgramName", "程式名稱"), "");
            dof.addFieldDefinition("AUTH01", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH01", "讀取"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH02", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH02", "新增"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH03", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH03", "刪除"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH04", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH04", "修改"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH05", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH05", "列印"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH06", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH06", "報表"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH07", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH07", "擁有"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH08", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH08", "權限08"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH09", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH09", "權限09"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("AUTH10", "STRING", "1", "N", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "AUTH10", "權限10"), com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "YN", "Y:有;N:"));
            dof.addFieldDefinition("D_INSERTUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_INSERTUSER", "建立者"), "");
            dof.addFieldDefinition("D_INSERTTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_INSERTTIME", "建立時間"), "");
            dof.addFieldDefinition("D_MODIFYUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_MODIFYUSER", "更新者"), "");
            dof.addFieldDefinition("D_MODIFYTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smse_maintain_aspx.language.ini", "message", "D_MODIFYTIME", "更新時間"), "");
            dof.addIdentityField("ProgramID");
            dof.addKeyField("ProgramID");
            dof.allowAllFieldEmpty();
            xml = dof.generalXML();

            dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.dataObjectSchema = xml;


            for (int i = 0; i < programList.Tables[0].Rows.Count; i++)
            {
                int auth = ag.getAuth(programList.Tables[0].Rows[i][0].ToString(), CheckUser.ValueText, groups);
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
                    doo.setData("ProgramID", programList.Tables[0].Rows[i][0].ToString());
                    doo.setData("ProgramName", programList.Tables[0].Rows[i][1].ToString());

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
            AuthList.NoAdd = true;
            AuthList.NoModify = true;
            AuthList.NoDelete = true;
            AuthList.dataSource = dos;
            AuthList.updateTable();
            programList.Dispose();
            engine.close();

        }
        catch (Exception ze)
        {
            try
            {
                engine.close();
            }
            catch { };
            MessageBox(ze.Message);
            writeLog(ze);
        }
    }
}
