<%@ WebService Language="C#" Class="SmpKmFileuploadService" %>

using System;  
using System.Web;  
using System.Collections;
using System.Collections.Generic;
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
public class SmpKmFileuploadService : SmpKmService
{
    ArrayList aryOffice = new ArrayList();

    public SmpKmFileuploadService()
    {
        
    }

    [WebMethod]
    public string createDocByByte(byte[] bxml)
    {
        string result = "";
        string xml = System.Text.Encoding.Default.GetString(bxml);
        result = createDocByXml(xml);
        return result;
    }

    [WebMethod]
    public string createDocByXml(string xml)
    {
        string resultXml = "";
        string message = "";
        string stacktrace = "";
        XMLProcessor xp = null;
        AbstractEngine engine = null;
        XmlDocument xmlDoc = null;
        XmlNode xmlNode = null;
        try
        {
            engine = base.getEngine();
            string tempFolder = GlobalProperty.getProperty("km", "tempFolder");
            string allowFileExt = GlobalProperty.getProperty("km", "allowFileExt");
            string[] fileExts = allowFileExt.Split(new char[] { ',' });
            aryOffice.AddRange(fileExts);
            xp = new XMLProcessor(xml, 1);
            //取得文件資訊
            XmlNode xn = xp.selectSingleNode("eKM/Document");
            string source = xn.SelectSingleNode("Source").InnerText;
            string sourceId = xn.SelectSingleNode("SourceId").InnerText;
            string authorFlag = xn.SelectSingleNode("AuthorFlag").InnerText;
            string author = xn.SelectSingleNode("Author").InnerText;
            string site = xn.SelectSingleNode("Site").InnerText;
            string docName = xn.SelectSingleNode("IndexCard/DocName").InnerText;
            string majorType = xn.SelectSingleNode("IndexCard/MajorType").InnerText;
            string subType = xn.SelectSingleNode("IndexCard/SubType").InnerText;
            string docType = xn.SelectSingleNode("IndexCard/DocType").InnerText;
            string docProperty = xn.SelectSingleNode("IndexCard/DocProperty").InnerText;
            string confidentialLevel = xn.SelectSingleNode("IndexCard/ConfidentialLevel").InnerText;
            string abstractText = xn.SelectSingleNode("IndexCard/Abstract").InnerText;
            string keywords = xn.SelectSingleNode("IndexCard/Keywords").InnerText;
            string effectiveDate = xn.SelectSingleNode("IndexCard/EffectiveDate").InnerText;

            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlNode = xmlDoc.SelectSingleNode("eKM/Document/IndexCard/Abstract");
            abstractText = xmlNode.InnerText;
            xmlNode = xmlDoc.SelectSingleNode("eKM/Document/IndexCard/Keywords");
            keywords = xmlNode.InnerText;
            
            Dictionary<string, string> document = new Dictionary<string, string>();
            document.Add("Source", source);
            document.Add("SourceId", sourceId);
            document.Add("AuthorFlag", authorFlag);
            document.Add("Author", author);
            document.Add("Site", site);
            document.Add("IndexCard/DocName", docName);
            document.Add("IndexCard/MajorType", majorType);
            document.Add("IndexCard/SubType", subType);
            document.Add("IndexCard/DocType", docType);
            document.Add("IndexCard/DocProperty", docProperty);
            document.Add("IndexCard/ConfidentialLevel", confidentialLevel);
            document.Add("IndexCard/Abstract", abstractText);
            document.Add("IndexCard/Keywords", keywords);
            document.Add("IndexCard/EffectiveDate", effectiveDate);

            //處理檔案
            string destFolder = tempFolder + source + sourceId + @"\";
            XmlNodeList xnl = xp.selectAllNodes("eKM/Attachments/Attachment");
            ArrayList aryAttas = new ArrayList();
            foreach (XmlNode node in xnl)
            {
                Dictionary<string, string> attachment = new Dictionary<string, string>();
                string attaflag = node.SelectSingleNode("AttachmentFlag").InnerText;
                string fileExt = node.SelectSingleNode("OriginalFile/FileExt").InnerText;
                if (aryOffice.IndexOf(fileExt) != -1)
                {
                    string fileName = node.SelectSingleNode("OriginalFile/FileName").InnerText;
                    string attachmentType = node.SelectSingleNode("OriginalFile/AttachmentType").InnerText;
                    string base64String = node.SelectSingleNode("OriginalFile/FileContent").InnerText;
                    if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);
                    string outputFileName = destFolder + fileName;
                    string fileGroupGUID = IDProcessor.getID("");
                    attachment.Add("FilePath", destFolder);
                    attachment.Add("FileGroupGUID", fileGroupGUID);
                    attachment.Add("AttachmentFlag", attaflag);
                    attachment.Add("OriginalFile/FileExt", fileExt);
                    attachment.Add("OriginalFile/FileName", fileName);
                    attachment.Add("OriginalFile/AttachmentType", attachmentType);

                    try
                    {
                        base64StringtoFile(base64String, outputFileName);
                        if (attaflag.Equals("0")) //需傳入原始檔與發佈檔
                        {
                            fileExt = node.SelectSingleNode("PublishFile/FileExt").InnerText;
                            if (fileExt.ToLower().Equals("pdf")) //發佈檔
                            {
                                fileName = node.SelectSingleNode("PublishFile/FileName").InnerText;
                                attachmentType = node.SelectSingleNode("PublishFile/AttachmentType").InnerText;
                                base64String = node.SelectSingleNode("PublishFile/FileContent").InnerText;
                                outputFileName = destFolder + fileName;
                                base64StringtoFile(base64String, outputFileName);

                                attachment.Add("PublishFile/FileExt", fileExt);
                                attachment.Add("PublishFile/FileName", fileName);
                                attachment.Add("PublishFile/AttachmentType", attachmentType);
                            }
                        }
                    }
                    catch (ArgumentNullException ane)
                    {
                        message += "Base 64 string is null. \n" + ane.Message;
                        stacktrace += ane.StackTrace;
                    }
                    catch (FormatException fe)
                    {
                        message += "Base 64 string length is not 4 or is not an even multiple of 4. \n" + fe.Message;
                        stacktrace += fe.StackTrace;
                    }
                    catch (Exception exp)
                    {
                        message += exp.Message;
                        stacktrace += exp.StackTrace;
                    }
                    //暫存附件資訊
                    aryAttas.Add(attachment);
                }
                else
                {
                    message += "file extension [" + fileExt + "] is not match.\n";
                }
            }

            //儲存至介面檔
            message += importDoc(engine, document, aryAttas);
        }
        catch (Exception ex)
        {
            message = ex.Message;
            stacktrace += ex.StackTrace;
            writeLog(engine, 0, ex.Message, ex.StackTrace);
            
            //寄送mail
            string content = "Message: " + message + "\nStackTrace: " + stacktrace;
            base.sendMail(engine, "[eKM] SmpKmFileuploadService Exception", content);
        }
        finally
        {
            if (engine != null) engine.close();
        }

        if (message.Equals(""))
        {
            resultXml = "<ReturnValue><Result>Y</Result></ReturnValue>";
        }
        else
        {
            resultXml = "<ReturnValue><Result>N</Result><Message>" + message + "</Message><StackTrace>" + stacktrace + "</StackTrace></ReturnValue>";
        }

        return resultXml;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="base64String"></param>
    /// <param name="outputFileName"></param>
    protected void base64StringtoFile(string base64String, string outputFileName)
    {
        FileStream outFile = null;
        try
        {
            // Convert the Base64 UUEncoded input into binary output.
            byte[] binaryData =
               Convert.FromBase64String(base64String);
            // Write out the decoded data
            outFile = new System.IO.FileStream(outputFileName,
                                       System.IO.FileMode.Create,
                                       System.IO.FileAccess.Write);
            outFile.Write(binaryData, 0, binaryData.Length);

        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (outFile != null) outFile.Close();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="document"></param>
    /// <param name="aryAttas"></param>
    /// <returns></returns>
    protected string importDoc(AbstractEngine engine, Dictionary<string, string> document, ArrayList aryAttas)
    {
        string result = "";
        string agentSchema = "WebServerProject.service.SPKM001.SmpKmImportInterfaceHeadAgent";
        NLAgent agent = new NLAgent();
        try
        {
            agent.loadSchema(agentSchema);
            //單頭
            agent.engine = engine;
            agent.query("1=2");
            DataObjectSet set = agent.defaultData;
            DataObject head = set.create();
            string headGUID = IDProcessor.getID("");
            head.setData("IS_LOCK", "N");
            head.setData("IS_DISPLAY", "Y");
            head.setData("DATA_STATUS", "Y");
            head.setData("GUID", headGUID);
            head.setData("Source", document["Source"]);
            head.setData("SourceId", document["SourceId"]);
            head.setData("AuthorFlag", document["AuthorFlag"]);
            head.setData("Author", document["Author"]);
            head.setData("Site", document["Site"]);
            head.setData("DocName", document["IndexCard/DocName"]);
            head.setData("MajorType", document["IndexCard/MajorType"]);
            head.setData("SubType", document["IndexCard/SubType"]);
            head.setData("DocType", document["IndexCard/DocType"]);
            head.setData("DocProperty", document["IndexCard/DocProperty"]);
            head.setData("ConfidentialLevel", document["IndexCard/ConfidentialLevel"]);
            head.setData("Abstract", document["IndexCard/Abstract"]);
            head.setData("Keywords", document["IndexCard/Keywords"]);
            head.setData("EffectiveDate", document["IndexCard/EffectiveDate"]);
            int count = aryAttas.Count;
            //writeLog(engine, 0, "count", count+"");
            if (count > 0)
            {
                DataObjectSet lineSet = new DataObjectSet();
                lineSet.isNameLess = true;
                lineSet.setAssemblyName("WebServerProject");
                lineSet.setChildClassString("WebServerProject.service.SPKM001.SmpKmImportInterfaceLine");
                lineSet.setTableName("SmpKmImportInterfaceLine");
                lineSet.loadFileSchema();
                for (int i = 0; i < aryAttas.Count; i++)
                {
                    Dictionary<string, string> atta = (Dictionary<string, string>)aryAttas[i];
                    string attaflag = atta["AttachmentFlag"];
                    DataObject original = lineSet.create();
                    DataObject publish = lineSet.create();
                    string fileGroupGUID = IDProcessor.getID("");
                    //原始檔
                    original.setData("IS_LOCK", "N");
                    original.setData("IS_DISPLAY", "Y");
                    original.setData("DATA_STATUS", "Y");
                    original.setData("GUID", IDProcessor.getID(""));
                    original.setData("HeadGUID", headGUID);
                    original.setData("FilePath", atta["FilePath"]);
                    original.setData("FileName", atta["OriginalFile/FileName"]);
                    original.setData("FileExt", atta["OriginalFile/FileExt"]);
                    original.setData("AttachmentType", atta["OriginalFile/AttachmentType"]);
                    original.setData("FileGroupGUID", fileGroupGUID);
                    lineSet.add(original);
                    //發佈檔
                    publish.setData("IS_LOCK", "N");
                    publish.setData("IS_DISPLAY", "Y");
                    publish.setData("DATA_STATUS", "Y");
                    publish.setData("GUID", IDProcessor.getID(""));
                    publish.setData("HeadGUID", headGUID);
                    publish.setData("FilePath", atta["FilePath"]);
                    publish.setData("FileGroupGUID", fileGroupGUID);
                    publish.setData("AttachmentType", "Publish");
                    if (attaflag.Equals("0"))
                    {
                        publish.setData("FileName", atta["PublishFile/FileName"]);
                        publish.setData("FileExt", atta["PublishFile/FileExt"]);
                    }
                    else //同原始檔
                    {
                        publish.setData("FileName", atta["OriginalFile/FileName"]);
                        publish.setData("FileExt", atta["OriginalFile/FileExt"]);
                    }
                    lineSet.add(publish);
                }
                head.setChild("SmpKmImportInterfaceLine", lineSet);
                set.add(head);
                
                agent.defaultData = set;
                if (agent.update())
                {
                    //call import service
                    result += importDoc(engine, document["Source"], document["SourceId"]);
                }
                else
                {
                    result += engine.errorString;
                }
            }
        }
        catch (Exception e)
        {
            result = e.Message;
            writeLog(engine, 0, e.Message, e.StackTrace);
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="source"></param>
    /// <param name="sourceId"></param>
    /// <returns></returns>
    protected string importDoc(AbstractEngine engine, string source, string sourceId)
    {
        string result = null;
        try
        {
            SysParam sp = new SysParam(engine);
            string service = sp.getParam("SmpKmImportService");
            WSDLClient wc = new WSDLClient(service);
            wc.dllPath = com.dsc.kernal.utility.Utility.G_GetTempPath();
            wc.build(true);
            string message = Convert.ToString(wc.callSync("importDoc", source, sourceId));

            XMLProcessor xp = new XMLProcessor(message, 1);
            XmlNode node = xp.selectSingleNode("ReturnValue");
            string flag = node["Result"].InnerText;
            if (flag.Equals("Y"))
            {
                result = "";
            }
            else
            {
                result = node["Message"].InnerText;
            }
        }
        catch (Exception e)
        {
            result = e.Message;
        }
        return result;
    }
}