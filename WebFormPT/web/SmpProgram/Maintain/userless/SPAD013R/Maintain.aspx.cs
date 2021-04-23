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
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPAD013R";
        ApplicationID = "SYSTEM";
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
				
				//公司別			
				string[,] idsCompany = null;
		        idsCompany = new string[,]{
					{"",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad013_form_aspx.language.ini", "message", "", "")},
		            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad013_form_aspx.language.ini", "message", "smp", "新普科技")},
		            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_system_form_spad013_form_aspx.language.ini", "message", "tp", "中普科技")}
		        };
		        Company.setListItem(idsCompany);
		        Company.ReadOnly = false;

                CheckUser.clientEngineType = engineType;
                CheckUser.connectDBString = connectString;
                CheckDept.clientEngineType = engineType;
                CheckDept.connectDBString = connectString;
				
				ReqList.NoAdd = true;
                ReqList.NoModify = true;
                ReqList.NoDelete = true;
				ReqList.isShowAll = true;
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
            dof.addFieldDefinition("Company", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "Company", "公司"), "");
            dof.addFieldDefinition("ReqUser", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "ReqUser", "申請人"), "");
            dof.addFieldDefinition("ReqDept", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "ReqDept", "需求單位"), "");
			dof.addFieldDefinition("SheetNo", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "SheetNo", "申請單號"), "");            
            dof.addFieldDefinition("AttributeName", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "AttributeName", "屬性"), "");
            dof.addFieldDefinition("ProdDesc", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "ProdDesc", "品牌-型號-稱"), "");
            dof.addFieldDefinition("Price", "STRING", "10", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "Price", "參考價格"), "");
            dof.addFieldDefinition("Unit", "STRING", "10", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "Unit", "單位"), "");
            dof.addFieldDefinition("Qty", "STRING", "10", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "Qty", "數量"), "");
			dof.addFieldDefinition("CategoryType", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "CategoryType", "CategoryTpye"), "");
            dof.addFieldDefinition("D_INSERTUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "D_INSERTUSER", "建立者"), "");
            dof.addFieldDefinition("D_INSERTTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "D_INSERTTIME", "建立時間"), "");
            dof.addFieldDefinition("D_MODIFYUSER", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "D_MODIFYUSER", "更新者"), "");
            dof.addFieldDefinition("D_MODIFYTIME", "STRING", "255", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spad013r_maintain_aspx.language.ini", "message", "D_MODIFYTIME", "更新時間"), "");			
            //dof.addIdentityField("UserID");
            //dof.addKeyField("UserID");
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
			ReqList.dataSource = dos;
            ReqList.updateTable();
            ReqList.Dispose();

			string reqUser = CheckUser.GuidValueText;
            string reqDept = CheckDept.GuidValueText;
            string reqSDate = StartDate.ValueText;
            string reqEDate = EndDate.ValueText;
			string reqCompany = Company.ValueText;
			string whereCondition = "";
			
			if (!reqCompany.Equals(""))
            {
                whereCondition += " and sf.Company = '" + reqCompany + "' ";
            }
			if (!reqUser.Equals(""))
            {
                whereCondition += " and sf.OriginatorGUID = '" + reqUser + "' ";
            }
            if (!reqDept.Equals(""))
            {
                whereCondition += " and sf.DeptGUID = '" + reqDept + "' ";
            }
            if (!reqSDate.Equals("") && !reqEDate.Equals(""))
            {
                whereCondition += " and sf.DueDate >= '" + reqSDate + "' and sf.DueDate <= '" + reqEDate + "' ";
            }
			else if (!reqSDate.Equals("") && reqEDate.Equals("")) 
			{
                whereCondition += " and sf.DueDate >= '" + reqSDate + "' ";
            }
			else if (reqSDate.Equals("") && !reqEDate.Equals("")) 
			{
				whereCondition += " and sf.DueDate <= '" + reqEDate + "' ";
			}
						
			sql  = " select Company, empNumber+'-'+empName ReqUser, deptId +'-'+deptName ReqDept, ";
			sql += " sf.SheetNo as SheetNo , AttributeName, ProdDesc, Price, Unit , sd.Quantity Qty, sf.D_INSERTUSER, sf.D_INSERTTIME, sf.D_MODIFYUSER, sf.D_MODIFYTIME, CategoryName+'.'+ TypeName as CategoryType";
			//sql += " CategoryName+'.'+ TypeName as CategoryType, AttributeName, ProdDesc, Price, Unit , sd.Quantity Qty, sf.D_INSERTUSER, sf.D_INSERTTIME, sf.D_MODIFYUSER, sf.D_MODIFYTIME, sf.SheetNo ";
            sql += " from SmpStationeryForm sf, SmpStationeryDetail sd, SmpHrStationeryMt sm, EmployeeInfo us, SMWYAAA ya    ";
            sql += " where sf.GUID = sd.HeaderGUID and sm.GUID=sd.StationeryGUID ";
            sql += " and sf.OriginatorGUID = us.empGUID and sf.GUID = ya.SMWYAAA019 and ya.SMWYAAA020='Y'  ";
			sql += whereCondition;
            //sql += " group by Company, empNumber, empName, deptId,deptName,CategoryName, TypeName, AttributeName, ProdDesc, Price, Unit, sf.D_INSERTUSER, sf.D_INSERTTIME, sf.D_MODIFYUSER, sf.D_MODIFYTIME ";

            userList = engine.getDataSet(sql, "TEMP");
			//ReqList.dataSource = null;

            for (int i = 0; i < userList.Tables[0].Rows.Count; i++)
            {
				DataObject doo = dos.create();
                doo.setData("Company", userList.Tables[0].Rows[i][0].ToString());
                doo.setData("ReqUser", userList.Tables[0].Rows[i][1].ToString());
				doo.setData("ReqDept", userList.Tables[0].Rows[i][2].ToString());
				doo.setData("SheetNo", userList.Tables[0].Rows[i]["SheetNo"].ToString());
				doo.setData("AttributeName", userList.Tables[0].Rows[i][4].ToString());
				doo.setData("ProdDesc", userList.Tables[0].Rows[i][5].ToString());
				doo.setData("Price", userList.Tables[0].Rows[i][6].ToString());
				doo.setData("Unit", userList.Tables[0].Rows[i][7].ToString());
				doo.setData("Qty", userList.Tables[0].Rows[i]["Qty"].ToString());
				doo.setData("SheetNo ", userList.Tables[0].Rows[i]["SheetNo"].ToString());
				doo.setData("D_INSERTUSER", userList.Tables[0].Rows[i]["D_INSERTUSER"].ToString());
				doo.setData("D_INSERTTIME", userList.Tables[0].Rows[i]["D_INSERTTIME"].ToString());
				doo.setData("D_MODIFYUSER", userList.Tables[0].Rows[i]["D_MODIFYUSER"].ToString());
				doo.setData("D_MODIFYTIME ", userList.Tables[0].Rows[i]["D_MODIFYTIME"].ToString());									
				dos.add(doo);				
				ReqList.dataSource = dos;
				ReqList.HiddenField = new string[] { "CategoryType", "D_INSERTUSER", "D_INSERTTIME", "D_MODIFYUSER", "D_MODIFYTIME" };
                ReqList.updateTable();
                ReqList.Dispose();  
					
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
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            //string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPAD013R.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }	
}
