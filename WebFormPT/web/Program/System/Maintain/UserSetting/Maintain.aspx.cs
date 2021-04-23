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
using com.dsc.kernal.databean;
using com.dsc.kernal.agent;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.IO;
using com.dsc.kernal.document;
public partial class Program_System_Maintain_UserSetting_Maintain : BaseWebUI.GeneralWebPage
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                initData();
            }
        }
    }

    private void initData()
    {
        
        string engineType = (string)Session["engineType"];
        string connectString = (string)Session["connectString"];

        SUBS.clientEngineType = engineType;
        SUBS.connectDBString = connectString;
        FlowID.clientEngineType = engineType;
        FlowID.connectDBString = connectString;
        SubUser.clientEngineType = engineType;
        SubUser.connectDBString = connectString;
        InvokeDept.clientEngineType = engineType;
        InvokeDept.connectDBString = connectString;

        IOFactory factory = new IOFactory();
        AbstractEngine engine=factory.getEngine(engineType, connectString);

        string sql = "select SMVRAAA001, SMVRAAA003 from SMVRAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        string[,] ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
        VRGUID.setListItem(ids);

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "ids0", "全部由本人處理")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "ids1", "允許系統改派代理人處理")}
        };
        FlowMethod.setListItem(ids);

        sql = "select LANGUAGEID, LANGUAGENAME from SYSLANGUAGE order by ISDEFAULT desc";
        ds = engine.getDataSet(sql, "TEMP");
        ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
        USERLANGUAGE.setListItem(ids);

        sql="select * from USERSETTING where USERGUID='"+(string)Session["UserGUID"]+"'";
        ds=engine.getDataSet(sql, "TEMP");
        DataRow dr = null;
        ofuSignImage.engine = engine;
        ofuSignImage.tempFolder = Server.MapPath("~/tempFolder");
        if (ds.Tables[0].Rows.Count == 0)
        {
            dr = ds.Tables[0].NewRow();
            dr["GUID"] = IDProcessor.getID("");
            dr["USERGUID"] = (string)Session["UserGUID"];
            dr["VRGUID"] = "";
            dr["RECEIVEMSG"] = "Y";
            dr["RECEIVEMAIL"] = "Y";
            ds.Tables[0].Rows.Add(dr);
            engine.updateDataSet(ds);
            ofuSignImage.readFile("");
        }
        else
        {
            dr = ds.Tables[0].Rows[0];
            ofuSignImage.readFile(dr["VRGUID"].ToString());
        }        
        VRGUID.ValueText = dr["VRGUID"].ToString();        
        if (dr["RECEIVEMSG"].ToString().Equals("Y"))
        {
            RECEIVEMSG.Checked = true;
        }
        else
        {
            RECEIVEMSG.Checked = false;
        }
        if (dr["RECEIVEMAIL"].ToString().Equals("Y"))
        {
            RECEIVEMAIL.Checked = true;
        }
        else
        {
            RECEIVEMAIL.Checked = false;
        }

        //GP段
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string nanastr = sp.getParam("NaNaDB");
        AbstractEngine mengine = factory.getEngine(EngineConstants.SQL, nanastr);

        sql = "select * from Users where OID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        ds = engine.getDataSet(sql, "TEMP");
        FlowMethod.ValueText = ds.Tables[0].Rows[0]["enableSubstitute"].ToString();
        DateTime dt;
        if (ds.Tables[0].Rows[0]["startSubstituteTime"] != System.DBNull.Value)
        {
            dt = DateTime.Parse(ds.Tables[0].Rows[0]["startSubstituteTime"].ToString());
            StartTime.ValueText = DateTimeUtility.convertDateTimeToString(dt);
        }
        else
        {
            StartTime.ValueText = "";
        }
        if (ds.Tables[0].Rows[0]["endSubstituteTime"] != System.DBNull.Value)
        {
            dt = DateTime.Parse(ds.Tables[0].Rows[0]["endSubstituteTime"].ToString());
            EndTime.ValueText = DateTimeUtility.convertDateTimeToString(dt);
        }
        else
        {
            EndTime.ValueText = "";
        }
        USERLANGUAGE.ValueText = ds.Tables[0].Rows[0]["localeString"].ToString();

        //通用代理人
        sql = "select substituteOID, id, userName from DefaultSubstituteDefinition inner join Users on substituteOID=Users.OID where ownerOID='" + Utility.filter((string)Session["UserGUID"]) + "' order by substitutiveOrder";
        ds = mengine.getDataSet(sql, "TEMP");

        DataObjectFactory fac = new DataObjectFactory();
        fac.tableName = "SUBSTITUTION";
        fac.assemblyName = "WebServerProject";
        fac.childClassString = "WebServerProject.system.UserSetting.SUBSTITUTION";
        fac.addFieldDefinition("GUID", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac1", "識別號"), "");
        fac.addFieldDefinition("substituteOID", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac2", "代理人識別號"), "");
        fac.addFieldDefinition("id", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac3", "員工編號"), "");
        fac.addFieldDefinition("userName", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac4", "代理人姓名"), "");
        fac.addIdentityField("GUID");
        fac.addKeyField("substituteOID");
        string xml = fac.generalXML();

        DataObjectSet dos = new DataObjectSet();
        dos.setAssemblyName("WebServerProject");
        dos.setChildClassString("WebServerProject.system.UserSetting.SUBSTITUTION");
        dos.setTableName("SUBSTITUTION");
        dos.isNameLess = true;
        dos.dataObjectSchema = xml;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataObject ddo = dos.create();
            ddo.setData("GUID", IDProcessor.getID(""));
            ddo.setData("substituteOID", ds.Tables[0].Rows[i][0].ToString());
            ddo.setData("id", ds.Tables[0].Rows[i][1].ToString());
            ddo.setData("userName", ds.Tables[0].Rows[i][2].ToString());
            dos.add(ddo);
        }

        SubsList.HiddenField = new string[] { "GUID", "substituteOID" };
        SubsList.dataSource = dos;
        SubsList.updateTable();

        //流程代理人
        //20090430 Updated**** Add New Column(alwaysApply)
        sql = "select substituteOID, u.id, u.userName, isnull(invokeOrganizationUnitOID, '') as invokeOrganizationUnitOID, isnull(o.id,'') as organizationUnitID, isnull(o.organizationUnitName,'') as organizationUnitName, processPackageId, processPackageName, alwaysApply from ProcessSubstituteDefinition inner join Users u on substituteOID=u.OID left outer join OrganizationUnit o on invokeOrganizationUnitOID=o.OID where ownerOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
        ds = mengine.getDataSet(sql, "TEMP");

        fac = new DataObjectFactory();
        fac.tableName = "ProcessSubstitution";
        fac.assemblyName = "WebServerProject";
        fac.childClassString = "WebServerProject.system.UserSetting.ProcessSubstitution";
        fac.addFieldDefinition("GUID", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac1", "識別號"), "");
        fac.addFieldDefinition("substituteOID", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac2", "代理人識別號"), "");
        fac.addFieldDefinition("id", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac3", "員工編號"), "");
        fac.addFieldDefinition("userName", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac4", "代理人姓名"), "");
        fac.addFieldDefinition("invokeOrganizationUnitOID", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac5", "發起部門識別號"), "");
        fac.addFieldDefinition("organizationUnitID", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac6", "發起部門代號"), "");
        fac.addFieldDefinition("organizationUnitName", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac7", "發起部門名稱"), "");
        fac.addFieldDefinition("processPackageId", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac8", "流程編號"), "");
        fac.addFieldDefinition("processPackageName", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac9", "流程名稱"), "");
        fac.addFieldDefinition("alwaysApply", "STRING", "50", "", com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "fac10", "永久代理"), ""); //20090430 Updated**** Add New Column(alwaysApply)
        fac.addIdentityField("GUID");
        //fac.addKeyField("substituteOID");
        fac.addKeyField("invokeOrganizationUnitOID");
        fac.addKeyField("processPackageId");
        fac.addAllowEmptyField("invokeOrganizationUnitOID");
        fac.addAllowEmptyField("organizationUnitID");
        fac.addAllowEmptyField("organizationUnitName");

        xml = fac.generalXML();

        dos = new DataObjectSet();
        dos.setAssemblyName("WebServerProject");
        dos.setChildClassString("WebServerProject.system.UserSetting.ProcessSubstitution");
        dos.setTableName("ProcessSubstitution");
        dos.isNameLess = true;
        dos.dataObjectSchema = xml;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataObject ddo = dos.create();
            ddo.setData("GUID", IDProcessor.getID(""));
            ddo.setData("substituteOID", ds.Tables[0].Rows[i][0].ToString());
            ddo.setData("id", ds.Tables[0].Rows[i][1].ToString());
            ddo.setData("userName", ds.Tables[0].Rows[i][2].ToString());
            ddo.setData("invokeOrganizationUnitOID", ds.Tables[0].Rows[i][3].ToString());
            ddo.setData("organizationUnitID", ds.Tables[0].Rows[i][4].ToString());
            ddo.setData("organizationUnitName", ds.Tables[0].Rows[i][5].ToString());
            ddo.setData("processPackageId", ds.Tables[0].Rows[i][6].ToString());
            ddo.setData("processPackageName", ds.Tables[0].Rows[i][7].ToString());
            //20090430 Updated**** Add New Column(alwaysApply)
            string strTemp = "是";
            if (ds.Tables[0].Rows[i][8].ToString() == "0")
            {
                strTemp="否";
            }
            ddo.setData("alwaysApply", strTemp);
            dos.add(ddo);
        }

        ProcessSubList.HiddenField = new string[] { "GUID", "substituteOID", "invokeOrganizationUnitOID" };
        ProcessSubList.dataSource = dos;
        ProcessSubList.updateTable();

        //若開啟多重代理人機制則鎖定GP修改代理人機制區塊
        sql = "select SMVPAAA036 from SMVPAAA";
        string isSubstitute = (string)engine.executeScalar(sql);

        if (isSubstitute.Equals("Y"))
        {
            disableSubstitute();
        }

        //客製個人圖片
        ofuSignImage.engine = engine;
        ofuSignImage.readFile((string)Session["UserGUID"]);
        ofuSignImage.maxLength=100 * 1024;
        setSession("forbiddenFileType", "true");
        DataObjectSet dosSignImage = ofuSignImage.uploadedList;
        
        if(dosSignImage.getAvailableDataObjectCount()>0)
        {
            DSCWebControl.FileItem  fi = (DSCWebControl.FileItem)dosSignImage.getAvailableDataObject(0);

            string tempFolder = Server.MapPath("~/tempFolder");
            string FileAdapter = "PersonalImageFileAdapter.PersonalImageFileAdapter";
            string js = "";
            if (fi.FILEPATH.Equals(""))
            {
                com.dsc.kernal.document.DocumentAdapterFactory docFactory = new com.dsc.kernal.document.DocumentAdapterFactory();
                PersonalImageFileAdapter.PersonalImageFileAdapter adp = (PersonalImageFileAdapter.PersonalImageFileAdapter)docFactory.getDocumentAdapter(FileAdapter.Split(new char[] { '.' })[0], FileAdapter);
                
                string tempPath = tempFolder + "\\" + fi.IDENTITYID + "." + fi.FILEEXT;
                //adp.getFile(tempPath, fi.LEVEL1, fi.LEVEL2, fi.IDENTITYID);
                string urlPath = adp.getFilePath(fi.LEVEL1.ToString(), fi.LEVEL2.ToString(), fi.IDENTITYID.ToString(), fi.FILEEXT.ToString());
                urlPath = Page.ResolveClientUrl("~/Personal/") + "//"+ fi.LEVEL1 +"//" + fi.IDENTITYID +"." + fi.FILEEXT;
                string clientURL = Page.ResolveClientUrl(urlPath);

                js += "<script>var imgObj=document.getElementById('imgPreview');imgObj.src='" + clientURL + "' ; imgObj.style.display=''; </script>";           
            }
            else
            {
                string filepath = "";
                filepath = fi.FILEPATH;
                string targetFolder = Page.Server.MapPath("~/tempFolder/");
                string[] zz = com.dsc.kernal.utility.Utility.extractPath(filepath);                
                string url = Page.ResolveUrl("~/tempFolder/" + zz[zz.Length - 1]);
                js += "<script>var imgObj=document.getElementById('imgPreview');imgObj.src='" + url + "' ; imgObj.style.display=''; </script>";
            }        
            ClientScript.RegisterStartupScript(this.GetType(), "loadImage", js);
        }

        mengine.close();
        engine.close();
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        AbstractEngine mengine = null;
        try
        {
            string engineType = (string)Session["engineType"];
            string connectString = (string)Session["connectString"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            engine.startTransaction(IsolationLevel.ReadCommitted);

            WebServerProject.maintain.SMVU.SMVUAgent agent = new WebServerProject.maintain.SMVU.SMVUAgent();
            agent.engine = engine;


            //客製個人圖片
            ofuSignImage.FileAdapter = "PersonalImageFileAdapter.PersonalImageFileAdapter";
            ofuSignImage.engine = engine;
            ofuSignImage.setJobID((string)Session["UserGUID"]);
            DSCWebControl.FileItem fi = (DSCWebControl.FileItem)getSession("currentFile");
            if (fi != null) 
            {
                for (int i = 0; i < ofuSignImage.uploadedList.getAvailableDataObjectCount(); i++) 
                {
                    if (ofuSignImage.uploadedList.getAvailableDataObject(i) != fi) 
                    {
                        ofuSignImage.uploadedList.getAvailableDataObject(i).delete();
                    }
                }
                ofuSignImage.confirmSave((string)Session["UserID"], "");
                ofuSignImage.saveFile();
            }

            string sql = "select * from USERSETTING where USERGUID='" + Utility.filter((string)Session["UserGUID"]) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");

            ds.Tables[0].Rows[0]["VRGUID"] = VRGUID.ValueText;
            if (RECEIVEMAIL.Checked)
            {
                ds.Tables[0].Rows[0]["RECEIVEMAIL"] = "Y";
            }
            else
            {
                ds.Tables[0].Rows[0]["RECEIVEMAIL"] = "N";
            }
            if (RECEIVEMSG.Checked)
            {
                ds.Tables[0].Rows[0]["RECEIVEMSG"] = "Y";
            }
            else
            {
                ds.Tables[0].Rows[0]["RECEIVEMSG"] = "N";
            }

            //GP段
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string nanastr = sp.getParam("NaNaDB");
            mengine = factory.getEngine(EngineConstants.SQL, nanastr);
            mengine.startTransaction(IsolationLevel.ReadCommitted);

            sql = "select * from Users where OID='" + Utility.filter((string)Session["UserGUID"]) + "'";
            //ds = mengine.getDataSet(sql, "TEMP");
            DataSet dsGP = mengine.getDataSet(sql, "TEMP");
            if (!USERPWD.ValueText.Equals(""))
            {
                //檢查密碼
                bool isCheckPWD = false;

                string xs = "select SMVPAAA018 from SMVPAAA";
                DataSet dsz = engine.getDataSet(xs, "TEMP");

                

                if (dsz.Tables[0].Rows[0][0].ToString().Equals("N"))
                {
                    isCheckPWD=false;
                }
                else
                {
                    isCheckPWD= true;
                }

                if (isCheckPWD)
                {

                    if (!agent.checkPasswordValid((string)Session["UserID"], USERPWD.ValueText, dsGP.Tables[0].Rows[0]["password"].ToString()))
                    {
                        engine.rollback();
                        engine.close();
                        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "QueryError1", "密碼不符合安全原則"));
                        return;
                    }
                }

                string ss = "update SMVTAAA set SMVTAAA003='" + agent.getHashPWD(USERPWD.ValueText) + "' where SMVTAAA002='" + (string)Session["UserID"] + "'";
                if (!engine.executeSQL(ss))
                {
                    engine.rollback();
                    engine.close();
                    MessageBox(engine.errorString);
                    return;
                }

                ds.Tables[0].Rows[0]["LASTPWDCHANGE"] = DateTimeUtility.getSystemTime2(null).Substring(0, 10);
            }

            if (!engine.updateDataSet(ds))
            {
                engine.rollback();
                engine.close();
                MessageBox(engine.errorString);
                return;
            }


            dsGP.Tables[0].Rows[0]["enableSubstitute"] = int.Parse(FlowMethod.ValueText);
            if (!StartTime.ValueText.Equals(""))
            {
                dsGP.Tables[0].Rows[0]["startSubstituteTime"] = DateTime.Parse(StartTime.ValueText);
            }
            else
            {
                dsGP.Tables[0].Rows[0]["startSubstituteTime"] = System.DBNull.Value;
            }
            if (!EndTime.ValueText.Equals(""))
            {
                dsGP.Tables[0].Rows[0]["endSubstituteTime"] = DateTime.Parse(EndTime.ValueText);
            }
            else
            {
                dsGP.Tables[0].Rows[0]["endSubstituteTime"] = System.DBNull.Value;
            }
            dsGP.Tables[0].Rows[0]["localeString"] = USERLANGUAGE.ValueText;
            if (!USERPWD.ValueText.Equals(""))
            {
                dsGP.Tables[0].Rows[0]["password"] = agent.getHashPWD(USERPWD.ValueText);
                
            }


            if (!mengine.updateDataSet(dsGP))
            {
                mengine.rollback();
                engine.rollback();
                mengine.close();
                engine.close();
                MessageBox(mengine.errorString);
                return;
            }

            sql = "delete from DefaultSubstituteDefinition where ownerOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
            mengine.executeSQL(sql);


            DataObjectSet dos = SubsList.dataSource;
            
            if (dos.getAvailableDataObjectCount() > 0)
            {
                sql = "select * from DefaultSubstituteDefinition where (1=2)";
                ds = mengine.getDataSet(sql, "TEMP");
                for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                {
                    DataRow ddr = ds.Tables[0].NewRow();
                    ddr["OID"] = get32OID();
                    ddr["ownerOID"] = (string)Session["UserGUID"];
                    ddr["substituteOID"] = dos.getAvailableDataObject(i).getData("substituteOID");
                    ddr["substitutiveOrder"] = i;
                    ddr["objectVersion"] = 0;
                    ds.Tables[0].Rows.Add(ddr);
                }
                if (!mengine.updateDataSet(ds))
                {
                    mengine.rollback();
                    engine.rollback();
                    mengine.close();
                    engine.close();
                    MessageBox(mengine.errorString);
                    return;
                }
            }

            sql = "delete from ProcessSubstituteDefinition where ownerOID='" + Utility.filter((string)Session["UserGUID"]) + "'";
            mengine.executeSQL(sql);

            dos = ProcessSubList.dataSource;
            if (dos.getAvailableDataObjectCount() > 0)
            {
                sql = "select * from ProcessSubstituteDefinition where (1=2)";
                ds = mengine.getDataSet(sql, "TEMP");
                for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
                {
                    DataRow ddr = ds.Tables[0].NewRow();
                    ddr["OID"] = get32OID();
                    ddr["ownerOID"] = (string)Session["UserGUID"];
                    ddr["substituteOID"] = dos.getAvailableDataObject(i).getData("substituteOID");
                    if (dos.getAvailableDataObject(i).getData("invokeOrganizationUnitOID").Equals(""))
                    {
                        ddr["invokeOrganizationUnitOID"] = System.DBNull.Value;
                    }
                    else
                    {
                        ddr["invokeOrganizationUnitOID"] = dos.getAvailableDataObject(i).getData("invokeOrganizationUnitOID");
                    }
                    ddr["objectVersion"] = 1;
                    ddr["processPackageId"] = dos.getAvailableDataObject(i).getData("processPackageId");
                    ddr["processPackageName"] = dos.getAvailableDataObject(i).getData("processPackageName");
                    //20090430 Updated**** Add New Column(alwaysApply)
                    string strChecked = "0";
                    string strTemp=dos.getAvailableDataObject(i).getData("alwaysApply").ToString();
                    if (strTemp == "是")
                    {
                        strChecked = "1";
                    }
                    ddr["alwaysApply"] = strChecked; 
                    ds.Tables[0].Rows.Add(ddr);
                }
                if (!mengine.updateDataSet(ds))
                {
                    mengine.rollback();
                    engine.rollback();
                    mengine.close();
                    engine.close();
                    MessageBox(mengine.errorString);
                    return;
                }
            }

            mengine.commit();
            mengine.close();
            engine.commit();
            engine.close();
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "QueryError2", "儲存成功"));
        }
        catch (Exception te)
        {            
            try
            {
                mengine.rollback();
                engine.rollback();
                mengine.close();
                engine.close();
            }
            catch { };
            MessageBox(te.Message);
            
            return;

        }
    }
    protected void ClearButton_Click(object sender, EventArgs e)
    {
        StartTime.ValueText = "";
        EndTime.ValueText = "";
    }
    protected void SubsList_ShowRowData(DataObject objects)
    {
        SUBS.GuidValueText = objects.getData("substituteOID");
        SUBS.doGUIDValidate();
    }
    protected bool SubsList_SaveRowData(DataObject objects, bool isNew)
    {
        if (SUBS.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "QueryError3", "請選擇代理人"));
            return false;
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
        }
        objects.setData("substituteOID", SUBS.GuidValueText);
        objects.setData("id", SUBS.ValueText);
        objects.setData("userName", SUBS.ReadOnlyValueText);
        return true;
    }
    private string get32OID()
    {
        string tag = IDProcessor.getID("").Replace("-", "");
        return tag;
    }
    protected bool ProcessSubList_SaveRowData(DataObject objects, bool isNew)
    {
        if (SubUser.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "QueryError4", "必須選擇代理人"));
            return false;
        }
        if (FlowID.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_usersetting_maintain_aspx.language.ini", "message", "QueryError5", "必須選擇流程"));
            return false;
        }

        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
        }
        objects.setData("substituteOID", SubUser.GuidValueText);
        objects.setData("id", SubUser.ValueText);
        objects.setData("userName", SubUser.ReadOnlyValueText);
        if (InvokeDept.ValueText.Equals(""))
        {
            objects.setData("invokeOrganizationUnitOID", "");
            objects.setData("organizationUnitID", "");
            objects.setData("organizationUnitName", "");
        }
        else
        {
            objects.setData("invokeOrganizationUnitOID", InvokeDept.GuidValueText);
            objects.setData("organizationUnitID", InvokeDept.ValueText);
            objects.setData("organizationUnitName", InvokeDept.ReadOnlyValueText);
        }
        objects.setData("processPackageId", FlowID.ValueText);
        objects.setData("processPackageName", FlowID.ReadOnlyValueText);
        //20090430 Updated**** Add New Column(alwaysApply)
        string strChecked = "否";
        if (DSCCheckBox1.Checked)
        {
            strChecked = "是";
        }
        objects.setData("alwaysApply", strChecked);
        return true;
    }
    protected void ProcessSubList_ShowRowData(DataObject objects)
    {
        SubUser.GuidValueText = objects.getData("substituteOID");
        SubUser.doGUIDValidate();
        InvokeDept.GuidValueText = objects.getData("invokeOrganizationUnitOID");
        InvokeDept.doGUIDValidate();
        FlowID.ValueText = objects.getData("processPackageId");
        FlowID.doValidate();
        //20090430 Updated**** Add New Column(alwaysApply)
        string strTemp=objects.getData("alwaysApply");
        if (strTemp == "否")
        {
            DSCCheckBox1.Checked = false;
        }
        else if (strTemp == "是")
        {
            DSCCheckBox1.Checked = true;
        }
    }
    private void disableSubstitute()
    {
        StartTime.ReadOnly = true;
        EndTime.ReadOnly = true;
        GlassButton1.ReadOnly = true;
        SUBS.ReadOnly = true;
        SubsList.ReadOnly = true;
        FlowID.ReadOnly = true;
        SubUser.ReadOnly = true;
        InvokeDept.ReadOnly = true;
        DSCCheckBox1.ReadOnly = true;
        ProcessSubList.ReadOnly = true;
    }

    //客製個人圖片
    protected void ofuSignImage_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
        currentObject.IDENTITYID = (string)Session["UserID"];
        setSession("currentFile", currentObject);
        Response.Write("var imgObj =document.getElementById('imgPreview');imgObj.style.display=''; imgObj.src='" + ResolveClientUrl("~/tempFolder") + "//" + currentObject.FILEPATH + "' ;");
    }
    protected void gtnUploadPersonalImage_Click(object sender, EventArgs e)
    {
        ofuSignImage.openFileUploadDialog();
    }
}
