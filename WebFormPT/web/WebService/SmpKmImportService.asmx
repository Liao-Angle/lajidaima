<%@ WebService Language="C#" Class="SmpKmImportService" %>

using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Xml;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.kernal.webservice;
using WebServerProject;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmpKmImportService : SmpKmService
{
    public SmpKmImportService()
    {        
    }
    
    [WebMethod]
    public string HelloWorld() {
        return "Welcome to KM Import Service!";
    }

    [WebMethod]
    public string importDoc(string source, string sourceId)
    {
        string resultXml = "";
        string msg = "";
        string msgTmp = "";
        string stacktrace = "";
        string result = "N";        
        AbstractEngine engine = null;
        string sql = "";
        DataSet ds = null;  
        string docNo = ""; //km文件編號 
        string insertUserGUID = ""; 
        string docGUID = "";
        string revGUID = "";
        string indexCardGUID = ""; 
        try
        {
            
            engine = base.getEngine();            
            //透過標準AgentSchema取得資料
            //string agentSchemaSource = "WebServerProject.service.SPKM001I.SmpKmImportInterfaceHeadAgent";
            //NLAgent agentSource = new NLAgent();            
            //GET Interface data
            //agentSource.loadSchema(agentSchemaSource);           
            //agentSource.engine = engine;
            //agentSource.query(" (Source = '" + source + "' and SourceId='" +sourceId + "' and (Result is null or Result ='') ) ");                                   
            //DataObjectSet dosSourceH = agentSource.defaultData;            
            //int sourceCount = dosSourceH.getDataObjectCount();
            
            //改為finally段執行SQL UPDATE Result
            sql = " SELECT GUID, DocName FROM SmpKmImportInterfaceHead " +
                " Where Source = '" + source + "' and SourceId='" + sourceId + "' and Result ='Y' ";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                throw new Exception("Source:" + source +",Source ID :"+ sourceId + ",界面檔記錄已曾Import至KM!不可重覆匯入!");
            }
            
            //Result 無值代表未處理過
            sql = " SELECT GUID, AuthorFlag, Author, Site, DocName, MajorType, SubType, "+
                " DocType, DocProperty, ConfidentialLevel, Abstract, Keywords, EffectiveDate FROM SmpKmImportInterfaceHead "+
                " Where Source = '" + source + "' and SourceId='" + sourceId + "' and (Result is null or Result ='') ";
            ds = engine.getDataSet(sql, "TEMP");
            int sourceCount = ds.Tables[0].Rows.Count;
            if (sourceCount == 1) //界面檔資料筆數
            {
                //Interface Head
                //DataObject doSourceH = dosSourceH.getDataObject(0);
                //DataObjectSet dosSourceL =  doSourceH.getChild("SmpKmImportInterfaceLine");
                //string headGUID = doSourceH.getData("GUID");
                //string authorFlag = doSourceH.getData("AuthorFlag");
                //string author = doSourceH.getData("Author");
                //string site = doSourceH.getData("Site");
                //string docName = doSourceH.getData("DocName");
                //string majorType = doSourceH.getData("MajorType");
                //string subType = doSourceH.getData("SubType");
                //string docType = doSourceH.getData("DocType");
                //string docProperty = doSourceH.getData("DocProperty");
                //string confidentialLevel = doSourceH.getData("ConfidentialLevel");
                //string abstractD= doSourceH.getData("Abstract");
                //string keywords = doSourceH.getData("Keywords");
                //string effectiveDate = doSourceH.getData("EffectiveDate");
                //string expiryDate = "9999/12/31";

                string headGUID = ds.Tables[0].Rows[0][0].ToString();
                string authorFlag = ds.Tables[0].Rows[0][1].ToString();
                string author = ds.Tables[0].Rows[0][2].ToString();
                string site = ds.Tables[0].Rows[0][3].ToString();
                string docName = ds.Tables[0].Rows[0][4].ToString();
                string majorType = ds.Tables[0].Rows[0][5].ToString();
                string subType = ds.Tables[0].Rows[0][6].ToString();
                string docType = ds.Tables[0].Rows[0][7].ToString();
                string docProperty = ds.Tables[0].Rows[0][8].ToString();
                string confidentialLevel = ds.Tables[0].Rows[0][9].ToString();
                string abstractD = ds.Tables[0].Rows[0][10].ToString();
                string keywords = ds.Tables[0].Rows[0][11].ToString();                
                string effectiveDateTmp = ds.Tables[0].Rows[0][12].ToString();
                DateTime dt = Convert.ToDateTime(effectiveDateTmp);
                string effectiveDate = dt.ToString("yyyy/MM/dd");
                string expiryDate = "9999/12/31";                
                
                //文件必需屬性GUID初始值                
                string adminGUID = "";
                string deptGUID = "";
                string authorGUID = "";               
                string majorTypeGUID = "";
                string subTypeGUID = "";
                string docTypeGUID = "";
                string docPropertyGUID = "";

                //文件編號 SMPKMDOCExteriorSeqNo
                docNo = getSysAutoNo(engine, "SMPKMDOCExteriorSeqNo");
                if (docNo.Equals(""))
                    msgTmp += "無法取得KM外來文件編號(SMPKMDOCExteriorSeqNo)!";
                
                //取得Admin
                sql = "select u.OID,o.OID From Users u  join  OrganizationUnit o on  u.id ='administrator' and o.organizationUnitName ='資訊部(SMP)' ";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    adminGUID = ds.Tables[0].Rows[0][0].ToString();
                    insertUserGUID = adminGUID;
                    deptGUID = ds.Tables[0].Rows[0][1].ToString();
                    if (authorFlag.Equals("0"))
                        authorGUID = adminGUID;
                }
                else
                {
                    msgTmp += "無法取得管理者及其部門GUID!";
                }

                //作者及主要部門GUID
                if (!authorFlag.Equals("0")) //非admin
                {
                    sql = " SELECT TOP 1 empGUID,deptGUID  FROM EmployeeInfoAllDept " +
                      " WHERE ( " +
                      " ('1' = '" + authorFlag + "' and upper(rtrim(empNumber)) ='" + author.ToUpper() + "') or  " +
                      " ('2' = '" + authorFlag + "' and upper(rtrim(empEmail)) ='" + author.ToUpper() + "') or " +
                      " ('3' = '" + authorFlag + "' and upper(rtrim(empEName)) ='" + author.ToUpper() + "') " +
                      " ) and isMain =1 ";

                    ds = engine.getDataSet(sql, "TEMP");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        authorGUID = ds.Tables[0].Rows[0][0].ToString();
                        insertUserGUID = authorGUID;
                        deptGUID = ds.Tables[0].Rows[0][1].ToString();
                    }
                    else
                    {
                        msgTmp += author.ToUpper() +"無法取得作者及主要部門GUID!";
                    }
                }
                
                //文件類別及性質
                sql = " SELECT T.MajorTypeGUID,T.SubTypeGUID,T.DocTypeGUID,L.GUID DocPropertyGUID " +
                      " FROM SmpKmTypeV T " +
                      " JOIN SmpListValue L " +
                      " on upper(T.MajorType)='" + majorType.ToUpper() +"'  and upper(T.SubType)='" +  subType.ToUpper() +"' and " +
                      " upper(T.DocType)='" + docType.ToUpper() +"' and L.ListNameGUID='DocProperty' " +
                      " and  L.Value='" + docProperty.ToUpper() + "' ";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    majorTypeGUID = ds.Tables[0].Rows[0][0].ToString();
                    subTypeGUID = ds.Tables[0].Rows[0][1].ToString();
                    docTypeGUID = ds.Tables[0].Rows[0][2].ToString();
                    docPropertyGUID = ds.Tables[0].Rows[0][3].ToString();
                }
                else
                {
                    msgTmp += majorType + "-" + subType + "-" + docType + "無法取得文件類別或文件性質GUID!";
                }

                //取得預設的歸屬群組
                sql = "select  c.Kind, c.OID " +
                 "  from SmpSubTypeBelongGroup b, " +
                 "       SmpBelongGroupV c" +
                 " where b.SubTypeGUID = '" + Utility.filter(subTypeGUID) + "'" +
                 "   and c.OID = b.BelongGroupGUID";
                ds = engine.getDataSet(sql, "TEMP");
                int groupRows = ds.Tables[0].Rows.Count;
                string[][] groupResult = new string[groupRows][];
                if (groupRows > 0)
                {                    
                    for (int i = 0; i < groupRows; i++)
                    {
                        groupResult[i] = new string[2];
                        groupResult[i][0] = ds.Tables[0].Rows[i][0].ToString();
                        groupResult[i][1] = ds.Tables[0].Rows[i][1].ToString();
                    }
                }
                else
                    msgTmp += "無法取得文件類別歸屬群組!";
                
                if (msgTmp.Equals(""))  //所有資料齊全才新增至KM Table
                {
                    docGUID = IDProcessor.getID("");
                    revGUID = IDProcessor.getID("");                    
                    string now = DateTimeUtility.getSystemTime2(null);
                    string releaseDate = now.Substring(0, 10);
                    
                    //FILEITEM
                    string level1 = "EKM";
                    string filePath = "";
                    string fileName = ""; //包含附檔名
                    string fileRelName = "";
                    string fileExt = "";
                    string attachmentType = "";
                    string[][] sqlResults = null;
                    int lastIndex = 0;
                    string sysFilePath = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "filepath"); //settting file
                    sql = "SELECT FilePath, FileName,  FileExt, AttachmentType FROM SmpKmImportInterfaceLine " +
                        " Where HeadGUID ='" + headGUID + "' order by  FileGroupGUID, AttachmentType ";
                    ds = engine.getDataSet(sql, "TEMP");
                    DataObjectSet dosAtt = new DataObjectSet();
                    int fileRows = ds.Tables[0].Rows.Count;
                    if (fileRows > 0)
                    {
                        //SmpAttachment                        
                        dosAtt.isNameLess = true;
                        dosAtt.setAssemblyName("WebServerProject");
                        dosAtt.setChildClassString("WebServerProject.form.SPKM001.SmpAttachment");
                        dosAtt.setTableName("SmpAttachment");
                        dosAtt.loadFileSchema();
                                                
                        sqlResults = new string[fileRows][];                                                         
                        for (int i = 0; i < fileRows; i++)
                        {
                            string fileitemGUID = IDProcessor.getID("");
                            filePath = ds.Tables[0].Rows[i][0].ToString();
                            fileName = ds.Tables[0].Rows[i][1].ToString();
                            lastIndex =fileName.LastIndexOf(".");
                            fileRelName = fileName.Substring(0, lastIndex);
                            fileExt = ds.Tables[0].Rows[i][2].ToString();
                            attachmentType = ds.Tables[0].Rows[i][3].ToString();

                            string sourcePath = @filePath;
                            string targetPath = @sysFilePath + level1 + "\\" + docNo ;
                                                                                   
                            //copy file
                            if (System.IO.Directory.Exists(sourcePath))
                            {
                                //create folder
                                if (!System.IO.Directory.Exists(targetPath))
                                {
                                    System.IO.Directory.CreateDirectory(targetPath);
                                } 
                                
                                System.IO.File.Copy(filePath + fileName, targetPath +"\\" + fileitemGUID , true); 
                            }
                            else
                            {                                
                                msgTmp = sourcePath + "檔案目錄不存在!";
                                throw new Exception(msgTmp + engine.errorString);                                
                            }
                            
                            //insert into FILEITEM                           
                            sql = "insert into FILEITEM (GUID, JOBID, IDENTITYID, FILENAME, FILEEXT, FILEPATH, DESCRIPTION, LEVEL1, LEVEL2, "
                                + "D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME, UPLOADUSER, UPLOADTIME) Values("
                                + "'" + fileitemGUID + "','" + docGUID + "','" + fileitemGUID + "','" + fileRelName + "','" + fileExt + "','',''," 
                                + "'" + level1 + "' , '" + docNo + "',"
                                + "'" + insertUserGUID + "','" + now + "','','','','')";
                            if (!engine.executeSQL(sql))
                            {                                
                                msgTmp += "新增至Fileitem Table發生錯誤!";
                                throw new Exception(msgTmp + engine.errorString);
                            }
                            else
                            {
                                //SmpAttachment                               
                                DataObject doAtt = dosAtt.create();
                                string attGUID = IDProcessor.getID("");
                                doAtt.setData("IS_LOCK", "N");
                                doAtt.setData("IS_DISPLAY", "Y");
                                doAtt.setData("DATA_STATUS", "Y");
                                doAtt.setData("GUID", attGUID);
                                doAtt.setData("DocGUID", docGUID);
                                doAtt.setData("RevGUID", revGUID);
                                doAtt.setData("FileItemGUID", fileitemGUID);
                                doAtt.setData("AttachmentType", attachmentType);
                                doAtt.setData("Processed", "Y");                                
                                doAtt.setData("External","Y");
                                doAtt.setData("D_INSERTUSER", adminGUID);
                                dosAtt.add(doAtt);
                            }
                        }
                    }

                    //SmpDocument
                    string agentSchemaKm = "WebServerProject.form.SPKM001.SmpDocImportAgent";
                    NLAgent agentKm = new NLAgent();
                    agentKm.loadSchema(agentSchemaKm);
                    agentKm.engine = engine;
                    agentKm.query("1=2");
                    DataObjectSet dosDoc = agentKm.defaultData;
                    DataObject doDoc = dosDoc.create();

                    doDoc.setData("IS_LOCK", "N");
                    doDoc.setData("IS_DISPLAY", "Y");
                    doDoc.setData("DATA_STATUS", "Y");
                    doDoc.setData("GUID", docGUID);
                    doDoc.setData("DocNumber", docNo);
                    doDoc.setData("AuthorGUID", authorGUID);
                    doDoc.setData("AuthorOrgUnitGUID", deptGUID);                    
                    doDoc.setData("Site", site);
                    doDoc.setData("D_INSERTUSER", adminGUID);
                    
                    //SmpRev                   
                    DataObjectSet dosRev = new DataObjectSet();
                    dosRev.isNameLess = true;
                    dosRev.setAssemblyName("WebServerProject");
                    dosRev.setChildClassString("WebServerProject.form.SPKM001.SmpRev");
                    dosRev.setTableName("SmpRev");
                    dosRev.loadFileSchema();
                    DataObject doRev = dosRev.create();                   
                    indexCardGUID = IDProcessor.getID("");
                    doRev.setData("IS_LOCK", "N");
                    doRev.setData("IS_DISPLAY", "Y");
                    doRev.setData("DATA_STATUS", "Y");
                    doRev.setData("GUID", revGUID);
                    doRev.setData("RevNumber", "1");
                    doRev.setData("DocGUID", docGUID);
                    doRev.setData("FormGUID", docGUID);  //無表單，為空值
                    doRev.setData("IndexCardGUID", indexCardGUID);
                    doRev.setData("Released", "Y");
                    doRev.setData("LatestFlag", "Y");
                    doRev.setData("ReleaseDate", releaseDate);
                    doRev.setData("SheetNo", docNo);
                    doRev.setData("D_INSERTUSER", adminGUID);
                    dosRev.add(doRev);
                    doDoc.setChild("SmpRev", dosRev);

                    //SmpIndexCard                   
                    DataObjectSet dosIndexCard = new DataObjectSet();
                    dosIndexCard.isNameLess = true;
                    dosIndexCard.setAssemblyName("WebServerProject");
                    dosIndexCard.setChildClassString("WebServerProject.form.SPKM001.SmpIndexCard");
                    dosIndexCard.setTableName("SmpIndexCard");
                    dosIndexCard.loadFileSchema();
                    DataObject doIndexCard = dosIndexCard.create();
                    doIndexCard.setData("IS_LOCK", "N");
                    doIndexCard.setData("IS_DISPLAY", "Y");
                    doIndexCard.setData("DATA_STATUS", "Y");
                    doIndexCard.setData("GUID", indexCardGUID);
                    doIndexCard.setData("Status", "Closed");
                    doIndexCard.setData("MajorTypeGUID", majorTypeGUID);
                    doIndexCard.setData("SubTypeGUID", subTypeGUID);  
                    doIndexCard.setData("DocTypeGUID", docTypeGUID);
                    doIndexCard.setData("DocPropertyGUID", docPropertyGUID);
                    doIndexCard.setData("ConfidentialLevel", confidentialLevel);
                    doIndexCard.setData("DocGUID", docGUID);
                    doIndexCard.setData("Name", docName);
                    doIndexCard.setData("Abstract", abstractD);
                    doIndexCard.setData("KeyWords", keywords);
                    doIndexCard.setData("EffectiveDate", effectiveDate);
                    doIndexCard.setData("ExpiryDate", expiryDate);
                    doIndexCard.setData("External", "Y");
                    doIndexCard.setData("D_INSERTUSER", adminGUID);
                    dosIndexCard.add(doIndexCard);
                    doDoc.setChild("SmpIndexCard", dosIndexCard);

                    //SmpDocBelongGroup                    
                    DataObjectSet dosGroup = new DataObjectSet();
                    dosGroup.isNameLess = true;
                    dosGroup.setAssemblyName("WebServerProject");
                    dosGroup.setChildClassString("WebServerProject.form.SPKM001.SmpDocBelongGroup");
                    dosGroup.setTableName("SmpDocBelongGroup");
                    dosGroup.loadFileSchema();  
                    for (int i = 0; i < groupResult.Length; i++)
                    {
                        DataObject doGroup = dosGroup.create();
                        string groupGUID = IDProcessor.getID("");
                        doGroup.setData("IS_LOCK", "N");
                        doGroup.setData("IS_DISPLAY", "Y");
                        doGroup.setData("DATA_STATUS", "Y");
                        doGroup.setData("GUID", groupGUID);
                        doGroup.setData("DocGUID", docGUID);
                        doGroup.setData("RevGUID", revGUID);
                        doGroup.setData("BelongGroupType", groupResult[i][0]); //部門
                        doGroup.setData("BelongGroupGUID", groupResult[i][1]);
                        doGroup.setData("D_INSERTUSER", adminGUID);
                        dosGroup.add(doGroup);
                    }
                    doDoc.setChild("SmpDocBelongGroup", dosGroup);                    

                    //SmpHistory
                    DataObjectSet dosHistory = new DataObjectSet();
                    dosHistory.isNameLess = true;
                    dosHistory.setAssemblyName("WebServerProject");
                    dosHistory.setChildClassString("WebServerProject.form.SPKM001.SmpHistory");
                    dosHistory.setTableName("SmpHistory");
                    dosHistory.loadFileSchema();
                    DataObject doHistory = dosHistory.create();
                    string historyGUID = IDProcessor.getID("");
                    doHistory.setData("IS_LOCK", "N");
                    doHistory.setData("IS_DISPLAY", "Y");
                    doHistory.setData("DATA_STATUS", "Y");
                    doHistory.setData("GUID", historyGUID);
                    doHistory.setData("DocGUID", docGUID);
                    doHistory.setData("Action", "文件新增");
                    doHistory.setData("Description", "Web Service Import外來文件");
                    doHistory.setData("RevGUID", revGUID);
                    doHistory.setData("FormGUID", docGUID);
                    doHistory.setData("D_INSERTUSER", adminGUID);
                    dosHistory.add(doHistory);
                    doDoc.setChild("SmpHistory", dosHistory);
                    
                    //setChild
                    doDoc.setChild("SmpAttachment", dosAtt);
                    
                    //儲存KM Table     
                    dosDoc.add(doDoc);
                    agentKm.defaultData = dosDoc;
                    if (agentKm.update())
                    {                        
                        result="Y";
                        //刪除暫存檔
                        string tempFolder = GlobalProperty.getProperty("km", "tempFolder");
                        string delFolder = tempFolder + source + sourceId;
                        DirectoryInfo di = new DirectoryInfo(@delFolder);
                        if (fileRows>0)
                            di.Delete(true); //參數指定除了刪除目錄外，包含以下的所有檔案

                        try
                        {
                            //更新Table D_INSERTUSER , 解決ECP無法取得 D_INSERTUSER問題                     
                            sql = " UPDATE SmpDocument SET D_INSERTUSER ='" + insertUserGUID + "' WHERE GUID ='" + docGUID + "' ";
                            engine.executeSQL(sql);
                            sql = " UPDATE SmpRev SET D_INSERTUSER ='" + insertUserGUID + "' WHERE GUID ='" + revGUID + "' ";
                            engine.executeSQL(sql);
                            sql = " UPDATE SmpIndexCard SET D_INSERTUSER ='" + insertUserGUID + "' WHERE GUID ='" + indexCardGUID + "' ";
                            engine.executeSQL(sql);
                            sql = " UPDATE SmpAttachment SET D_INSERTUSER ='" + insertUserGUID + "' WHERE DocGUID ='" + docGUID + "' and RevGUID ='" + revGUID + "' ";
                            engine.executeSQL(sql);
                            sql = " UPDATE SmpDocBelongGroup SET D_INSERTUSER ='" + insertUserGUID + "' WHERE DocGUID ='" + docGUID + "' and RevGUID ='" + revGUID + "' ";
                            engine.executeSQL(sql);
                            sql = " UPDATE SmpHistory SET D_INSERTUSER ='" + insertUserGUID + "' WHERE DocGUID ='" + docGUID + "' and RevGUID ='" + revGUID + "' ";
                            engine.executeSQL(sql);
                        }
                        catch (Exception ex1)
                        {
                            writeLog(engine, 0, ex1.Message, ex1.StackTrace);
                        }
                    }
                    else
                    {
                        msgTmp += "更新KM Table 失敗!";
                        result = "N";
                    }              
             
                }
                
                //if (result.Equals("Y"))               
                    //doSourceH.setData("DocNumber", docNo);
                //doSourceH.setData("Result", result);
                   
                //for (int i = 0; i < dosSourceL.getAvailableDataObjectCount(); i++)
                //{
                //    dosSourceL.getAvailableDataObject(i).setData("AttachmentGUID", i + "");
                //}
                
                //doSourceH.setChild("SmpKmImportInterfaceLine", dosSourceL);                
                //agentSource.defaultData = dosSourceH;
                //agentSource.update();                                            
            }
            else if (sourceCount > 1)
            {                
               msgTmp += "Interface資料重覆，請檢查!";
            }
            else
            {
                msgTmp += "Interface無符合的資料，請檢查!";         
            }

            if (!msgTmp.Equals(""))
                throw new Exception(msgTmp);            
        }
        catch (Exception ex)
        {   
            //log
            result = "N";
            msg = "Source:" + source + ";Source ID:" + sourceId + " " + ex.Message;
            stacktrace = ex.StackTrace;
            writeLog(engine, 0, ex.Message, ex.StackTrace);

            //寄送mail
            string content = "Message: " + msg + "\n StackTrace: " + stacktrace;
            base.sendMail(engine, "[eKM] SmpKmImportService[importDoc] Exception", content);
        }
        finally
        {
            //update interface result            
            sql = " update SmpKmImportInterfaceHead set Result='" + result + "',";  
            if (result.Equals("Y"))
            {
                sql += " DocNumber ='" + docNo + "', " ;
            }    
            sql +=" Message = '" + msg + "' ,StackTrace = '" + stacktrace + "' " +    
               //",D_MODIFYTIME='" +  DateTimeUtility.getSystemTime2(null) + "' " +
               " where Source='" + source + "' and SourceId='" + sourceId + "' and (Result is null or Result='')";
            writeLog(engine, 0, "sql", sql);
            try
            {
                if (!engine.executeSQL(sql))
                {
                    throw new Exception(engine.errorString + ";" +sql);
                    //寄送mail
                    string content = "Message: " + msg + "\n StackTrace: " + stacktrace;
                    base.sendMail(engine, "[eKM] SmpKmImportService[importDoc] Exception", content);
                }               
            }
            catch (Exception ex1)
            {               
                writeLog(engine, 0, ex1.Message, ex1.StackTrace);
            }
            finally
            {
                if (engine != null) engine.close();
            }           
        }

        if (msg.Equals(""))
        {
            resultXml = "<ReturnValue><Result>Y</Result><Message></Message><StackTrace></StackTrace></ReturnValue>";
        }
        else
        {
            resultXml = "<ReturnValue><Result>N</Result><Message>" + msg + "</Message><StackTrace>" + stacktrace + "</StackTrace></ReturnValue>";
        }

        return resultXml;
    }

    //取得文件號碼
    protected string getSysAutoNo(AbstractEngine engine, string code)
    {
        string no = "";
        try
        {
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
           // hs.Add("Source", "E"); 
            no = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();             
        }
        catch (Exception e)
        {
            writeLog(engine, 0, e.Message, e.StackTrace);
        }
        return no;
    }
    
}