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
        maintainIdentity = "SPAD003M";
        ApplicationID = "SMPFORM";
        ModuleID = "SPAD";

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
				
                EmployeeOID.clientEngineType = engineType;
                EmployeeOID.connectDBString = connectString;
				
				string empOID = Convert.ToString(Request.QueryString["EMPOID"]);
				
				if (string.IsNullOrEmpty(empOID)) 
				{
					EmployeeOID.GuidValueText = empOID;
                    EmployeeOID.doGUIDValidate();					
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
					dof.addFieldDefinition("OID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpNumber", "OID"), "");
				    dof.addFieldDefinition("EmpOID", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpCName", "員工OID"), "");
				    dof.addFieldDefinition("MgrOID", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpEName", "主管OID"), "");
				    dof.addFieldDefinition("EmpNumber", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpNumber", "工號"), "");
				    dof.addFieldDefinition("EmpCName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpCName", "中文名"), "");
				    dof.addFieldDefinition("EmpEName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpEName", "英文名"), "");
				    dof.addFieldDefinition("MgrNumber", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "MgrNumber", "直屬主管工號"), "");
				    dof.addFieldDefinition("MgrCName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "MgrCName", "直屬主管中文名"), "");
				    dof.addFieldDefinition("MgrEName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "MgrEName", "直屬主管英文名"), "");

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

					EmployeeOID.GuidValueText = empOID;
                    EmployeeOID.doGUIDValidate();					
					
					if (!string.IsNullOrEmpty(empOID)) 
		            {
		                //whereCondition += " and gu.GroupOID = '" + groupOID + "' ";						
		            }else{
					    //whereCondition += " and gu.GroupOID = '  '  ";
					}
					//MessageBox("SQL : " + whereCondition);

					sql  = " select f.OID, f.occupantOID EMPOID, f.specifiedManagerOID MANAGEROID, ue.empNumber EMPNUMBER, ue.empName EMPNAME, ue.empEName EMPENAEME ";
					sql += " , um.empNumber MGRNUMBER, um.empName MGRNAME, um.empEName MGRENAME from Functions f ";
					sql += " join EmployeeInfo ue on  f.occupantOID = ue.empGUID and f. isMain='1' and empLeaveDate is NULL ";
					sql += " join EmployeeInfo um on f.specifiedManagerOID=um.empGUID ";					
					
		            sql += " Where  (specifiedManagerOID =(select OID from Users where id='3992'))  ";
					
					sql += whereCondition;
					
		            userList = engine.getDataSet(sql, "TEMP");

		            for (int i = 0; i < userList.Tables[0].Rows.Count; i++)
		            {
						DataObject doo = dos.create();
		                doo.setData("OID", userList.Tables[0].Rows[i][0].ToString());
		                doo.setData("EMPOID", userList.Tables[0].Rows[i][1].ToString());
						doo.setData("MANAGEROID", userList.Tables[0].Rows[i][2].ToString());
						doo.setData("EMPNUMBER", userList.Tables[0].Rows[i][3].ToString());
						doo.setData("EMPNAME", userList.Tables[0].Rows[i][4].ToString());
						doo.setData("EMPENAEME", userList.Tables[0].Rows[i][5].ToString());
						doo.setData("MGRNUMBER", userList.Tables[0].Rows[i][6].ToString());
						doo.setData("MGRNAME", userList.Tables[0].Rows[i][7].ToString());
						doo.setData("MGRENAME", userList.Tables[0].Rows[i][8].ToString());
						dos.add(doo);				
						DataList.dataSource = dos;
		                DataList.updateTable();
		                DataList.Dispose();    

					}
					
					Master.inputForm = "Detail.aspx";
			        Master.getListTable().NoDelete = true;
			        Master.getListTable().FormTitle = com.dsc.locale.LocaleString.getSystemMessageString("program_dscorgservice_maintain_users_maintain_aspx.language.ini", "message", "Title", "使用者資料維護");
			        Master.DialogWidth = 750;
			        Master.DialogHeight = 500;
			        Master.InitData(obj);

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
            dof.addFieldDefinition("OID", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpNumber", "OID"), "");
		    dof.addFieldDefinition("EmpOID", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpCName", "員工OID"), "");
		    dof.addFieldDefinition("MgrOID", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpEName", "主管OID"), "");
		    dof.addFieldDefinition("EmpNumber", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpNumber", "工號"), "");
		    dof.addFieldDefinition("EmpCName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpCName", "中文名"), "");
		    dof.addFieldDefinition("EmpEName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "EmpEName", "英文名"), "");
		    dof.addFieldDefinition("MgrNumber", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "MgrNumber", "直屬主管工號"), "");
		    dof.addFieldDefinition("MgrCName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "MgrCName", "直屬主管中文名"), "");
		    dof.addFieldDefinition("MgrEName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spkmgroup_maintain_aspx.language.ini", "message", "MgrEName", "直屬主管英文名"), "");

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

			string checkEmployee = EmployeeOID.GuidValueText;
			string whereCondition = "";
						
			if (!checkEmployee.Equals(""))
            {
                whereCondition += " and  f.occupantOID = '" + checkEmployee + "' ";
            }
            						
			sql  = " select f.OID, f.occupantOID EMPOID, f.specifiedManagerOID MANAGEROID, ue.empNumber EMPNUMBER, ue.empName EMPNAME, ue.empEName EMPENAEME ";
			sql += " , um.empNumber MGRNUMBER, um.empName MGRNAME, um.empEName MGRENAME from Functions f ";
			sql += " join EmployeeInfo ue on  f.occupantOID = ue.empGUID and f. isMain='1' and empLeaveDate is NULL ";
			sql += " join EmployeeInfo um on f.specifiedManagerOID=um.empGUID ";
			sql += whereCondition;
			
            userList = engine.getDataSet(sql, "TEMP");
			//ReqList.dataSource = null;

            for (int i = 0; i < userList.Tables[0].Rows.Count; i++)
            {
				DataObject doo = dos.create();
                doo.setData("OID", userList.Tables[0].Rows[i][0].ToString());
		        doo.setData("EMPOID", userList.Tables[0].Rows[i][1].ToString());
				doo.setData("MANAGEROID", userList.Tables[0].Rows[i][2].ToString());
				doo.setData("EMPNUMBER", userList.Tables[0].Rows[i][3].ToString());
				doo.setData("EMPNAME", userList.Tables[0].Rows[i][4].ToString());
				doo.setData("EMPENAEME", userList.Tables[0].Rows[i][5].ToString());
				doo.setData("MGRNUMBER", userList.Tables[0].Rows[i][6].ToString());
				doo.setData("MGRNAME", userList.Tables[0].Rows[i][7].ToString());
				doo.setData("MGRENAME", userList.Tables[0].Rows[i][8].ToString());
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
