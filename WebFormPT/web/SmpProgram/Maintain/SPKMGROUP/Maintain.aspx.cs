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
using com.dsc.kernal.agent;

public partial class SmpProgram_Maintain : BaseWebUI.GeneralWebPage
{
	private string connectString = "";
	private string engineType = "";
	
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPKMGROUP";
        ApplicationID = "SYSTEM";
        ModuleID = "SPKM";

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
				
                CheckGroup.clientEngineType = engineType;
                CheckGroup.connectDBString = connectString;
				
				string groupOID = Convert.ToString(Request.QueryString["groupOID"]);
				
				if (string.IsNullOrEmpty(groupOID)) 
				{
					CheckGroup.GuidValueText = groupOID;
                    CheckGroup.doGUIDValidate();					
				}
				
				DataList.NoAdd = true;
                DataList.NoModify = true;
                DataList.NoDelete = true;
				DataList.isShowAll = true;
				
				AbstractEngine engine = null;
		
		        try
		        {
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
		            dof.addFieldDefinition("GroupName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "GroupName", "群組名稱"), "");
		            dof.addFieldDefinition("EmpNumber", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpNumber", "工號"), "");
		            dof.addFieldDefinition("EmpCName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpCName", "中文名"), "");
		            dof.addFieldDefinition("EmpEName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpEName", "英文名"), "");
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
					string whereCondition = "";
					DataList.dataSource = dos;
		            DataList.updateTable();
		            DataList.Dispose();					

					CheckGroup.GuidValueText = groupOID;
                    CheckGroup.doGUIDValidate();					
					
					if (!string.IsNullOrEmpty(groupOID)) 
		            {
		                whereCondition += " and gu.GroupOID = '" + groupOID + "' ";						
		            }else{
					    whereCondition += " and gu.GroupOID = '  '  ";
					}
					//MessageBox("SQL : " + whereCondition);

					sql  = " select  gp.groupName , ur.id as empNumber  , userName as empCName, LOWER(SUBSTRING(ur.mailAddress, 1, CHARINDEX('@', ur.mailAddress) - 1)) AS empEName, gp.groupName as GroupName ";
		            sql += " From Group_User gu, Users ur, Groups gp ";
		            sql += " where gu.UserOID=ur.OID and (ur.leaveDate='' or ur.leaveDate is null) and gu.GroupOID=gp.OID and gp.groupName like '%KM%' ";
		            //sql += "  and  (GroupOID = '"+ groupOID +"'  or GroupOID =' ' )  ";
					
					sql += whereCondition;
					
		            userList = engine.getDataSet(sql, "TEMP");

		            for (int i = 0; i < userList.Tables[0].Rows.Count; i++)
		            {
						DataObject doo = dos.create();
		                doo.setData("GroupName", userList.Tables[0].Rows[i][0].ToString());
		                doo.setData("empNumber", userList.Tables[0].Rows[i][1].ToString());
						doo.setData("empCName", userList.Tables[0].Rows[i][2].ToString());
						doo.setData("empEName", userList.Tables[0].Rows[i][3].ToString());
						dos.add(doo);				
						DataList.dataSource = dos;
		                DataList.updateTable();
		                DataList.Dispose();    
						CheckGroup.ValueText =userList.Tables[0].Rows[i][0].ToString();
					}

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
    }
	
    protected void SearchButton_Click(object sender, EventArgs e)
    {
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
            dof.addFieldDefinition("GroupName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "GroupName", "群組名稱"), "");
            dof.addFieldDefinition("EmpNumber", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpNumber", "工號"), "");
            dof.addFieldDefinition("EmpCName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpCName", "中文名"), "");
            dof.addFieldDefinition("EmpEName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpEName", "英文名"), "");

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
			DataList.dataSource = dos;
            DataList.updateTable();
            DataList.Dispose();

			string checkGroup = CheckGroup.GuidValueText;
			string whereCondition = "";
						
			if (!checkGroup.Equals(""))
            {
                whereCondition += " and gu.GroupOID = '" + checkGroup + "' ";
            }
            						
			sql  = " select  gp.groupName , ur.id as empNumber  , userName as empCName, LOWER(SUBSTRING(ur.mailAddress, 1, CHARINDEX('@', ur.mailAddress) - 1)) AS empEName, gp.groupName as GroupName ";
            sql += " From Group_User gu, Users ur, Groups gp ";
            sql += " where gu.UserOID=ur.OID and (ur.leaveDate='' or ur.leaveDate is null) and gu.GroupOID=gp.OID and gp.groupName like '%KM%' ";
			sql += whereCondition;
			
            userList = engine.getDataSet(sql, "TEMP");
			//ReqList.dataSource = null;

            for (int i = 0; i < userList.Tables[0].Rows.Count; i++)
            {
				DataObject doo = dos.create();
                doo.setData("GroupName", userList.Tables[0].Rows[i][0].ToString());
                doo.setData("empNumber", userList.Tables[0].Rows[i][1].ToString());
				doo.setData("empCName", userList.Tables[0].Rows[i][2].ToString());
				doo.setData("empEName", userList.Tables[0].Rows[i][3].ToString());
				dos.add(doo);				
				DataList.dataSource = dos;
                DataList.updateTable();
                DataList.Dispose();                
			}

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
