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
//using WebServerProject.maintain.spts002.SmpTSPlanFormAgent;

public partial class SmpProgram_Maintain_SPTS002_Maintain : BaseWebUI.GeneralWebPage
{
	private string connectString = "";
	private string engineType = "";
	
    protected override void OnInit(EventArgs e)
    {
        ApplicationID = "SmpProgram";
		maintainIdentity = "SPTS002";        
        ModuleID = "SPTS";
		//AgentSchema = "WebServerProject.maintain.spts002.SmpTSPlanForm";
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
					
					//公司別			
					string[,] idsCompany = null;
			        idsCompany = new string[,]{
						{" - ",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", " - ", " - ")},
			            {"SMP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "smp", "新普科技")},
			            {"TP",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "tp", "中普科技")}
			        };
			        CompanyCode.setListItem(idsCompany);
					
					//ProduceSch
					string[,] idsProduceSch = null;
			        idsProduceSch = new string[,]{
						{" - ",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", " - ", " - ")},
			            {"Y",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "Y", "Y")},
			            {"N",com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "N", "N")}
			        };
			        ProduceSch.setListItem(idsProduceSch);
		            
		            DataObjectFactory dof = new DataObjectFactory();
		            dof.init();
		            dof.assemblyName = "WebServerProject";
		            dof.childClassString = "WebServerProject.temp.ProgramUser";
		            dof.tableName = "ProgramUser";
		            dof.addFieldDefinition("CompanyCode", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "CompanyCode", "公司別"), "");
		            dof.addFieldDefinition("PlanYear", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "PlanYear", "年度"), "");
		            dof.addFieldDefinition("ProduceSch", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "ProduceSch", "已產生年度開課計劃"), "");
		            dof.addFieldDefinition("Remark", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "Remark", "備註"), "");
		            dof.allowAllFieldEmpty();

		            string xml = dof.generalXML();

		            DataObjectSet dos = new DataObjectSet();
		            dos.isNameLess = true;
		            dos.dataObjectSchema = xml;
					
		            Hashtable hs = null;

		            WebServerProject.auth.AUTHAgent ag = new WebServerProject.auth.AUTHAgent();
		            ag.engine = engine;

		            DataSet tsList = null;
		            string sql = "";
					//string whereCondition = "";
					DataList.dataSource = dos;
		            DataList.updateTable();
		            DataList.Dispose();					

					sql  = " select  GUID,CompanyCode, PlanYear, ProduceSch, Remark from SmpTSPlanForm ";
		            sql += " where 1=2";					
		            tsList = engine.getDataSet(sql, "TEMP");

		            for (int i = 0; i < tsList.Tables[0].Rows.Count; i++)
		            {
						DataObject doo = dos.create();
						doo.setData("GUID", tsList.Tables[0].Rows[i][0].ToString());
		                doo.setData("CompanyCode", tsList.Tables[0].Rows[i][1].ToString());
		                doo.setData("PlanYear", tsList.Tables[0].Rows[i][2].ToString());
						doo.setData("ProduceSch", tsList.Tables[0].Rows[i][3].ToString());
						doo.setData("Remark", tsList.Tables[0].Rows[i][4].ToString());
						dos.add(doo);				
						DataList.dataSource = dos;
						DataList.HiddenField = new string[] { "GUID" };
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
			dof.addFieldDefinition("CompanyCode", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "CompanyCode", "公司別"), "");
            dof.addFieldDefinition("PlanYear", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "PlanYear", "年度"), "");
            dof.addFieldDefinition("ProduceSch", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "ProduceSch", "已產生年度開課計劃"), "");
            dof.addFieldDefinition("Remark", "STRING", "128", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "Remark", "備註"), "");

            dof.allowAllFieldEmpty();

            string xml = dof.generalXML();

            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.dataObjectSchema = xml;
			
            //Hashtable hs = null;

            WebServerProject.auth.AUTHAgent ag = new WebServerProject.auth.AUTHAgent();
            ag.engine = engine;

            DataSet tsList = null;
            string sql = "";
			DataList.dataSource = dos;
            DataList.updateTable();
            DataList.Dispose();

			string strCompanyCode = CompanyCode.ValueText;
			string strPlanYear = PlanYear.ValueText;
			string strProduceSch = ProduceSch.ValueText;
			string whereCondition = "";
						
			if (!strCompanyCode.Equals(" - "))
            {
                whereCondition += " and CompanyCode = '" + strCompanyCode + "' ";
            }
			if (!strPlanYear.Equals(""))
            {
                whereCondition += " and PlanYear = '" + strPlanYear + "' ";
            }
			if (!strProduceSch.Equals(" - "))
            {
                whereCondition += " and ProduceSch = '" + strProduceSch + "' ";
            }
			
			sql  = " select GUID, CompanyCode, PlanYear, ProduceSch, Remark from SmpTSPlanForm  ";
		    sql += " where 1=1";
			sql += whereCondition;
			//writeLog(" SearchButton sql :  " + sql);
            									
            tsList = engine.getDataSet(sql, "TEMP");
			//ReqList.dataSource = null;

            for (int i = 0; i < tsList.Tables[0].Rows.Count; i++)
            {
				DataObject doo = dos.create();
				doo.setData("GUID", tsList.Tables[0].Rows[i][0].ToString());
                doo.setData("CompanyCode", tsList.Tables[0].Rows[i][1].ToString());
                doo.setData("PlanYear", tsList.Tables[0].Rows[i][2].ToString());
				doo.setData("ProduceSch", tsList.Tables[0].Rows[i][3].ToString());
				doo.setData("Remark", tsList.Tables[0].Rows[i][4].ToString());
				dos.add(doo);				
				DataList.dataSource = dos;
				DataList.HiddenField = new string[] { "GUID" };
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
	
	protected void DataList_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
		CompanyCode.ValueText = objects.getData("CompanyCode");
        PlanYear.ValueText = objects.getData("PlanYear");
        ProduceSch.ValueText = objects.getData("ProduceSch");
		Remark.ValueText = objects.getData("Remark");
    }
	
    protected bool DataList_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
		if (CompanyCode.ValueText.Equals(" - "))
        {
            MessageBox("請輸入 公司別");
            return false;
        }
        if (PlanYear.ValueText.Equals(""))
        {
            MessageBox("請輸入 計劃年度");
            return false;
        }
        if (ProduceSch.ValueText.Equals(" - "))
		{
            MessageBox("請輸入 已產生年度開課計劃");
            return false;
		}
        objects.setData("GUID", IDProcessor.getID(""));
        objects.setData("CompanyCode", CompanyCode.ValueText);
        objects.setData("PlanYear", PlanYear.ValueText);
        objects.setData("ProduceSch", ProduceSch.ValueText);
        objects.setData("Remark", Remark.ValueText);		

        return true;
    }
	
	protected void SaveButton_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
			
			/*
            WebServerProject.maintain.spts002 agent = new SmpTSPlanFormAgent();
            agent.engine = engine;
            agent.defaultData = DataTable.dataSource;
            if (!agent.update())
            {
                throw new Exception(engine.errorString);
            }
			*/
            engine.close();
			MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "QueryError2", "儲存成功"));
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            writeLog(te);
            throw te;
        }
    }
	/*
    protected void SaveButton_Click(object sender, EventArgs e)
    {		
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);			
			
			DataObjectFactory dof = new DataObjectFactory();
		    dof.init();
		    dof.assemblyName = "WebServerProject";
		    dof.childClassString = "WebServerProject.temp.ProgramUser";
		    dof.tableName = "ProgramUser";
		    dof.addFieldDefinition("CompanyCode", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "CompanyCode", "公司別"), "");
		    dof.addFieldDefinition("PlanYear", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "PlanYear", "年度"), "");
		    dof.addFieldDefinition("ProduceSch", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "ProduceSch", "已產生年度開課計劃"), "");
		    dof.addFieldDefinition("Remark", "STRING", "64", "", com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "Remark", "備註"), "");
		    dof.allowAllFieldEmpty();

		    string xml = dof.generalXML();

		    DataObjectSet dos = new DataObjectSet();
		    dos.isNameLess = true;
		    dos.dataObjectSchema = xml;
					
		    Hashtable hs = null;

		    //WebServerProject.auth.AUTHAgent agent = new WebServerProject.auth.AUTHAgent();
			NLAgent agent = null;
			agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.spts002.SmpTSPlanFormAgent");
		    agent.engine = engine;   
			
            //SMVRCAgent agent = new SMVRCAgent();
            //agent.engine = engine;
            agent.defaultData = DataList.dataSource;
            if (!agent.update())
            {
                throw new Exception(engine.errorString);
            }
			
            engine.close();
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("smpprogram_maintain_spts002_maintain_aspx.language.ini", "message", "QueryError2", "儲存成功"));
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            writeLog(te);
            throw te;
        }
    }
	*/
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {            
            string line = DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\spts002.log", true, System.Text.Encoding.Default);
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
