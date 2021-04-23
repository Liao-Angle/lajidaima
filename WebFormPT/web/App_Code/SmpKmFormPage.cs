using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using WebServerProject;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;

using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Office.Core;
using iTextSharp.text;
using iTextSharp.text.pdf;

/// <summary>
/// BasicFormPage 的摘要描述
/// </summary>
public class SmpKmFormPage : SmpBasicFormPage
{
    //protected override DataObject readDB(AbstractEngine engine, string guid, string UIStatus)
    //{
    //    DataObject ddo = base.readDB(engine, guid, UIStatus);

    //    return ddo;
    //}

    /// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomSheetNo(AbstractEngine engine, string code)
    {
        string sheetNo = "";
        try
        {
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("FORMID", ProcessPageID);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return sheetNo;
    }

    /// <summary>
    /// Word列印成pdf
    /// </summary>
    /// <param name="sourcePath"></param>
    protected void DocConvert(string sourcePath, string destPath)
    {
        Microsoft.Office.Interop.Word.ApplicationClass application = null;
        Microsoft.Office.Interop.Word.Document document = null;
        object paramSourceDocPath = sourcePath;
        object paramMissing = Type.Missing;
        try
        {
            if (!File.Exists(destPath))
            {
                application = new Microsoft.Office.Interop.Word.ApplicationClass();
                // Open the source document.
                document = application.Documents.Open(
                    ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing);

                // Export it in the specified format.
                if (document != null)
                {
                    //object Background = true;
                    //object Range = WdPrintOutRange.wdPrintAllDocument;
                    //object Copies = 1;
                    //object PageType = WdPrintOutPages.wdPrintAllPages;
                    //object PrintToFile = false;
                    //object Collate = false;
                    //object ActivePrinterMacGX = paramMissing;
                    //object ManualDuplexPrint = false;
                    //object PrintZoomColumn = 1;
                    //object PrintZoomRow = 1;

                    //document.PrintOut(ref Background, ref paramMissing, ref Range, ref paramMissing,
                    //    ref paramMissing, ref paramMissing, ref paramMissing, ref Copies,
                    //    ref paramMissing, ref PageType, ref PrintToFile, ref Collate,
                    //    ref paramMissing, ref ManualDuplexPrint, ref PrintZoomColumn,
                    //    ref PrintZoomRow, ref paramMissing, ref paramMissing);
                    System.Object  FixedFormatExtClassPtr = null;
                    document.ExportAsFixedFormat(destPath, 
                        Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF,
                        false, 
                        WdExportOptimizeFor.wdExportOptimizeForPrint,
                        WdExportRange.wdExportAllDocument,
                        1,
                        1,WdExportItem.wdExportDocumentContent,false,false,WdExportCreateBookmarks.wdExportCreateHeadingBookmarks,false,false,false,ref FixedFormatExtClassPtr
                        );
                }
            }
        }
        catch (Exception ex)
        {
            // Respond to the error
            base.writeLog(ex);
            MessageBox(ex.StackTrace);
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Close and release the Document object.
            if (document != null)
            {
                document.Close(ref paramMissing, ref paramMissing,
                    ref paramMissing);
                Marshal.FinalReleaseComObject(document);
                document = null;
            }

            // Quit Word and release the ApplicationClass object.
            if (application != null)
            {
                application.Quit(ref paramMissing, ref paramMissing,
                    ref paramMissing);
                Marshal.FinalReleaseComObject(application);
                application = null;
            }

            GC.Collect();
            KillProcess("WINWORD");
        }
    }

    /// <summary>
    /// Excel列印成pdf
    /// </summary>
    /// <param name="sourcePath"></param>
    protected void XlsConvert(string sourcePath, string destPath)
    {
        Microsoft.Office.Interop.Excel.ApplicationClass application = null;
        Microsoft.Office.Interop.Excel.Workbook workBook = null;
        try
        {
            if (!File.Exists(destPath))
            {
                object paramMissing = Type.Missing;
                if (File.Exists(sourcePath))
                {
                    application = new Microsoft.Office.Interop.Excel.ApplicationClass();
                    //workBook = application.Workbooks.Add(sourcePath);
                    workBook = application.Workbooks.Open(
                    sourcePath, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing,
                    paramMissing, paramMissing, paramMissing);

                    if (workBook != null)
                    {
                        //workBook.Sheets.PrintOut(Type.Missing, Type.Missing, 3, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        workBook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, destPath,null,null,null,null,null,null,null);
                    }
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (workBook != null)
            {
                workBook.Close(true, Type.Missing, Type.Missing);
                Marshal.FinalReleaseComObject(workBook);
                workBook = null;
            }

            if (application != null)
            {
                application.Quit();
                Marshal.FinalReleaseComObject(application);
                application = null;
            }

            GC.Collect();
            KillProcess("EXCEL.EXE");
        }
    }

    /// <summary>
    /// PowerPoint文件列印成pdf
    /// </summary>
    /// <param name="sourcePath"></param>
    protected void PptConvert(string sourcePath, string destPath)
    {
        Microsoft.Office.Interop.PowerPoint.ApplicationClass application = null;
		  //Microsoft.Office.Interop.PowerPoint.Application application = null;
        Microsoft.Office.Interop.PowerPoint.Presentation presentation = null;
        object paramSourceDocPath = sourcePath;
        object paramMissing = Type.Missing;
        try
        {
            if (!File.Exists(destPath))
            {   
                application = new Microsoft.Office.Interop.PowerPoint.ApplicationClass();
                //application = new Microsoft.Office.Interop.PowerPoint.Application();
                application.Visible = MsoTriState.msoTrue;
                presentation = application.Presentations.Open(sourcePath, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
                if (presentation != null)
                {
                    presentation.SaveAs(destPath, PpSaveAsFileType.ppSaveAsPDF,  MsoTriState.msoTriStateMixed);
                    //PrintOptions printOptions = presentation.PrintOptions;
                    //printOptions.NumberOfCopies = 1;
                    //printOptions.Collate = MsoTriState.msoFalse;
                    //printOptions.PrintInBackground = MsoTriState.msoTrue;
                    //presentation.PrintOut(1, presentation.Slides.Count, "", 1, MsoTriState.msoTriStateMixed);
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (presentation != null)
            {
                presentation.Close();
                Marshal.FinalReleaseComObject(presentation);
                presentation = null;
            }

            if (application != null)
            {
                application.Quit();
                Marshal.FinalReleaseComObject(application);
                application = null;
            }

            GC.Collect();
            KillProcess("POWERPNT");
        }
    }

    /// <summary>
    /// 加浮水印
    /// </summary>
    /// <param name="inputfilepath"></param>
    /// <param name="outputfilepath"></param>
    /// <param name="watermarkimagepath"></param>
    protected void AddWatermark(string inputfilepath, string outputfilepath, string watermarkimagepath)
    {
        PdfReader pdfReader = null;
        FileStream outputStream = null;
        PdfStamper pdfStamper = null;
        PdfGState gstate = null;
        try
        {
            pdfReader = new PdfReader(inputfilepath);
            outputStream = new FileStream(outputfilepath, FileMode.Create);
            pdfStamper = new PdfStamper(pdfReader, outputStream);

            int numberOfPages = pdfReader.NumberOfPages;
            iTextSharp.text.Rectangle psize = pdfReader.GetPageSize(1);
            float width = psize.Width;
            float height = psize.Height;

            gstate = new PdfGState();
            gstate.FillOpacity = 0.15f;
            gstate.StrokeOpacity = 0.15f;

            PdfContentByte waterMarkContent;
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(watermarkimagepath);

            for (int i = 1; i <= numberOfPages; i++)
            {
                waterMarkContent = pdfStamper.GetOverContent(i);
                waterMarkContent.SaveState();
                waterMarkContent.SetGState(gstate);
                if (image != null)
                {
                    waterMarkContent.AddImage(image, image.Width, 0, 0, image.Height, (width - image.Width) / 2, height - (height + image.Height) / 2);
                }
            }
            pdfStamper.Close();
            pdfReader.Close();
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            if (outputStream != null)
            {
                outputStream.Close();
            }
            if (pdfReader != null)
            {
                pdfReader.Close();
            }
        }
    }

    /// <summary>
    /// 列印PDF加浮水印
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="attachmentSet"></param>
    /// <returns></returns>
    protected string printOutPdf(AbstractEngine engine, DataObjectSet attachmentSet)
    {
        string errMsg = "";
        try
        {
            for (int i = 0; i < attachmentSet.getAvailableDataObjectCount(); i++)
            {
                DataObject objAttachment = attachmentSet.getAvailableDataObject(i);
                string docGUID = objAttachment.getData("DocGUID");
                string revGUID = objAttachment.getData("RevGUID");
                string sql = "select isnull(i.External, 'N') External from SmpIndexCard i, SmpRev r where r.IndexCardGUID = i.GUID and r.GUID='" + revGUID + "'";
                //string external = (string)engine.executeScalar(sql);
                string external = objAttachment.getData("External");
                string sheetNo = objAttachment.getData("SheetNo");
                string attachmentGUID = objAttachment.getData("GUID");
                string fileType = objAttachment.getData("FILEEXT");
                string fileGUID = objAttachment.getData("FileItemGUID");
                string processed = objAttachment.getData("Processed");
                string filePath = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "filepath");
                string level1 = "EKM";
                string level2 = sheetNo;

                sql = "SELECT LEVEL1, LEVEL2 FROM FILEITEM where GUID='" + fileGUID + "'";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    level1 = ds.Tables[0].Rows[0][0].ToString();
                    level2 = ds.Tables[0].Rows[0][1].ToString();
                }
                string destpath = filePath + level1 + "\\" + level2 + "\\" + fileGUID;
                destpath = destpath.Replace("\\", "\\\\");

                if (!Convert.ToString(processed).Equals("Y"))
                {
                    bool isConvert = false;
                    string sourceFileName = destpath;
                    string pdfTempPath = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "pdfTempPath");
                    string pdfFileName = pdfTempPath + fileGUID + ".pdf";

                    if (fileType.ToLower().Equals("doc") || fileType.ToLower().Equals("docx"))
                    {
                        DocConvert(sourceFileName, pdfFileName);
                        isConvert = true;
                    }
                    else if (fileType.ToLower().Equals("xls") || fileType.ToLower().Equals("xlsx"))
                    {
                        XlsConvert(sourceFileName, pdfFileName);
                        isConvert = true;
                    }
                    else if (fileType.ToLower().Equals("ppt") || fileType.ToLower().Equals("pptx"))
                    {
                        PptConvert(sourceFileName, pdfFileName);
                        isConvert = true;
                    }
                    else if (fileType.ToLower().Equals("pdf"))
                    {
                        File.Copy(sourceFileName, pdfFileName, true);
                        isConvert = true;
                    }

                    if (isConvert)
                    {
                        string publishFileGUID = IDProcessor.getID("");
                        string destPdfFilePath = filePath + level1 + "\\" + sheetNo;
                        string destPdfFileName = destPdfFilePath + "\\" + publishFileGUID;
                        string watermarkFileName = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "watermarkFileName");
                        pdfFileName = pdfFileName.Replace("\\", "\\\\");
                        destPdfFilePath = destPdfFilePath.Replace("\\", "\\\\");
                        destPdfFileName = destPdfFileName.Replace("\\", "\\\\");
                        watermarkFileName = watermarkFileName.Replace("\\", "\\\\");

                        if (!Directory.Exists(destPdfFilePath))
                        {
                            Directory.CreateDirectory(destPdfFilePath);
                        }

                        int second = 0;
                        while (true)
                        {
                            second++;
                            //if (second % 2 == 0)
                            //{
                            //    pdfFileName = pdfTempPath + fileGUID + "1.pdf";
                            //}
                            //else
                            //{
                            //    pdfFileName = pdfTempPath + fileGUID + ".pdf";
                            //}


                            if (File.Exists(pdfFileName))
                            {
                                //Thread.Sleep(1000);
                                if (external != null && external.Equals("N"))
                                {
                                    AddWatermark(pdfFileName, destPdfFileName, watermarkFileName);
                                }
                                else
                                {
                                    File.Copy(pdfFileName, destPdfFileName);
                                }
                                string userGUID = (string)Session["UserGUID"];
                                string now = DateTimeUtility.getSystemTime2(null);

                                //insert into file
                                sql = "insert into FILEITEM (GUID, JOBID, IDENTITYID, FILENAME, FILEEXT, FILEPATH, DESCRIPTION, LEVEL1, LEVEL2, "
                                    + "D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME, UPLOADUSER, UPLOADTIME) "
                                    + "select '" + publishFileGUID + "' GUID, JOBID, '" + publishFileGUID + "' IDENTITYID, FILENAME, 'pdf' FILEEXT, FILEPATH, DESCRIPTION, '" + level1 + "' LEVEL1, '" + sheetNo + "' LEVEL2, "
                                    + "'" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME, '' UPLOADUSER, ''UPLOADTIME "
                                    + "from FILEITEM where GUID='" + fileGUID + "'";
                                if (!engine.executeSQL(sql))
                                {
                                    throw new Exception(engine.errorString);
                                }

                                //insert into SmpAttachment
                                string newAttaGUID = IDProcessor.getID("");
                                sql = "insert into SmpAttachment (GUID, DocGUID, RevGUID, FileItemGUID, AttachmentType, External, "
                                    + "IS_DISPLAY, IS_LOCK, DATA_STATUS, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME) "
                                    + "select '" + newAttaGUID + "' GUID, '" + docGUID + "' DocGUID, '" + revGUID + "' RevGUID, '" + publishFileGUID + "' FileItemGUID, 'Publish' AttachmentType, External, "
                                    + "'Y' IS_DISPLAY, 'N' IS_LOCK, 'Y' DATA_STATUS, '" + userGUID + "' D_INSERTUSER, '" + now + "' D_INSERTTIME, '' D_MODIFYUSER, '' D_MODIFYTIME "
                                    + " from SmpAttachment where GUID='" + attachmentGUID + "'";
                                if (!engine.executeSQL(sql))
                                {
                                    throw new Exception(engine.errorString);
                                }

                                attachmentSet.getAvailableDataObject(i).setData("Processed", "Y");
                                sql = "update SmpAttachment Set Processed = 'Y' where GUID='" + attachmentGUID + "'";
                                engine.executeSQL(sql);

                                if (File.Exists(pdfFileName))
                                {
                                    File.Delete(pdfFileName);
                                }
                                KillProcess("ACROTRAY");

                                break;
                            }
                            else
                            {
                                Thread.Sleep(250);
                            }

                            if (second > 120)
                            {
                                errMsg += "pdf file (" + pdfFileName + ") not found!\n";
                                break;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            errMsg = e.Message;
            engine.rollback();
            //throw new Exception(errMsg + e.StackTrace);
        }

        return errMsg;
    }

    /// <summary>
    /// 結整處理程序
    /// </summary>
    /// <param name="processname"></param>
    protected void KillProcess(string processname)
    {
        try
        {
            Process[] prs = System.Diagnostics.Process.GetProcesses();
            foreach (Process pr in prs)
            {
                if (pr.ProcessName.ToUpper().StartsWith(processname))
                {
                    if (!pr.CloseMainWindow())
                    {
                        pr.Kill();
                    }
                }
            }
        }
        catch (Exception e)
        {
            writeLog(e);
        }
    }


    /// <summary>
    /// 傳入文件類別歸屬群組資料, [0]: Kind, [1]: KindName, [2]: groupOID, [3]: id, [4]: groupName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getDocTypeBelongGroupGUID(AbstractEngine engine, string GUID)
    {
        string sql = "select c.Kind, c.KindName, c.OID, c.id, c.Name" +
                     "  from SmpDocType a, " +
                     "       SmpSubTypeBelongGroup b, " +
                     "       SmpBelongGroupV c" +
                     " where a.GUID = '" + Utility.filter(GUID) + "'" +
                     "   and b.SubTypeGUID = a.SubTypeGUID" +
                     "   and c.OID = b.BelongGroupGUID";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[5];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
            result[i][1] = ds.Tables[0].Rows[i][1].ToString();
            result[i][2] = ds.Tables[0].Rows[i][2].ToString();
            result[i][3] = ds.Tables[0].Rows[i][3].ToString();
            result[i][4] = ds.Tables[0].Rows[i][4].ToString();
        }
        return result;
    }


    /// <summary>
    /// 傳入子類別取得歸屬群組資料, [0]: Kind, [1]: KindName, [2]: groupOID, [3]: id, [4]: groupName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getSubTypeBelongGroupGUID(AbstractEngine engine, string GUID)
    {
        string sql = "select c.Kind, c.KindName, c.OID, c.id, c.Name" +
                     "  from SmpSubTypeBelongGroup b, " +
                     "       SmpBelongGroupV c" +
                     " where b.SubTypeGUID = '" + Utility.filter(GUID) + "'" +
                     "   and c.OID = b.BelongGroupGUID";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[5];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
            result[i][1] = ds.Tables[0].Rows[i][1].ToString();
            result[i][2] = ds.Tables[0].Rows[i][2].ToString();
            result[i][3] = ds.Tables[0].Rows[i][3].ToString();
            result[i][4] = ds.Tables[0].Rows[i][4].ToString();
        }
        return result;
    }


    /// <summary>
    /// 傳入文件類別取得管理者, [0]: OID, [1]: id, [2]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="GUID"></param>
    /// <returns>string[]</returns>
    protected string[][] getDocTypeAdmUserGUID(AbstractEngine engine, string GUID)
    {
        string sql = "SELECT DISTINCT c.OID, c.id, c.userName " +
                     "  FROM " +
                     "(SELECT b.OID, b.id, b.userName " +
                     "   FROM dbo.SmpMajorTypeAdm a, " +
                     "        dbo.Users b, " +
                     "     dbo.SmpDocType d " +
                     "  WHERE d.GUID = '" + Utility.filter(GUID) + "'" +
                     "    AND b.OID = a.MajorTypeAdmUserGUID " +
                     "    AND a.MajorTypeGUID = d.MajorTypeGUID " +
                     " UNION " +
                     " SELECT b.OID, b.id, b.userName " +
                     "   FROM dbo.SmpSubTypeAdm a, " +
                     "        dbo.Users b, " +
                     "     dbo.SmpDocType d " +
                     "  WHERE d.GUID = '" + Utility.filter(GUID) + "'" +
                     "    AND b.OID = a.SubTypeAdmUserGUID" +
                     "    AND a.SubTypeGUID = d.SubTypeGUID) c ";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[3];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
            result[i][1] = ds.Tables[0].Rows[i][1].ToString();
            result[i][2] = ds.Tables[0].Rows[i][2].ToString();
        }
        return result;
    }

    /// <summary>
    /// 是否為此文件的管理者
    /// </summary>
    /// <returns></returns>
    protected bool isTypeAdmin(AbstractEngine engine, string docTypeGUID, string userGUID)
    {
        bool isTypeAdm = false;
        try
        {
            string[][] values = getDocTypeAdmUserGUID(engine, docTypeGUID);
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i][0].Equals(userGUID))
                {
                    isTypeAdm = true;
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }

        return isTypeAdm;
    }

    /// <summary>
    /// 是否為此版本文件的歸屬群組
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="docTypeGUID"></param>
    /// <returns></returns>
    protected bool isBelongGroup(AbstractEngine engine, string revGUID, string userGUID)
    {
        bool isBelongGroup = false;
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string sql = "select BelongGroupType, BelongGroupGUID from SmpDocBelongGroup where RevGUID='" + revGUID + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (!engine.errorString.Equals(""))
            {
                throw new Exception(engine.errorString);
            }

            int count = ds.Tables[0].Rows.Count;
            int sqlcount = 0;
            for (int i = 0; i < count; i++)
            {
                int belongGroupType = Convert.ToInt16(ds.Tables[0].Rows[i][0].ToString());
                string belongGroupGUID = ds.Tables[0].Rows[i][1].ToString();
                switch (belongGroupType)
                {
                    case 9: //部門
                        sql = "select id from OrganizationUnit where OID = '" + belongGroupGUID + "'";
                        string orgUnitId = (string)engine.executeScalar(sql);
                        if (si.ownerOrgID.Equals(orgUnitId))
                        {
                            isBelongGroup = true;
                        }
                        break;
                    case 21: //群組
                        sql = "select count('x') cnt from Groups a, Group_User b where a.OID=b.GroupOID and a.OID='" + belongGroupGUID + "' and b.UserOID='" + userGUID + "'";
                        sqlcount = (int)engine.executeScalar(sql);
                        if (!engine.errorString.Equals(""))
                        {
                            throw new Exception(engine.errorString);
                        }
                        if (sqlcount > 0)
                        {
                            isBelongGroup = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }

        return isBelongGroup;
    }

    /// <summary>
    /// 是否為此版本文件的讀者
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="revGUID"></param>
    /// <returns></returns>
    protected bool isReader(AbstractEngine engine, string revGUID, string userGUID)
    {
        bool isReader = false;
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string sql = null;
            sql = "select DocTypeGUID from SmpRev r, SmpIndexCard i where r.GUID = '" + revGUID + "' and r.IndexCardGUID=i.GUID";
            string docTypeGUID = (string)engine.executeScalar(sql);
            //文件類別預設讀者
            sql = "select d.BelongGroupType, d.BelongGroupGUID from SmpDocTypeReader d, SmpReaderV c where d.DocTypeGUID = '" + docTypeGUID + "' and c.OID = d.BelongGroupGUID ";
            //文件預設讀者
            sql += "union select BelongGroupType, BelongGroupGUID from SmpReader where RevGUID='" + revGUID + "' and (isnull(EffectiveDate, CONVERT(VARCHAR(10), GETDATE(), 111)) >=CONVERT(VARCHAR(10), GETDATE(), 111) or isnull(ExpiryDate, CONVERT(VARCHAR(10), GETDATE(), 111)) <= CONVERT(VARCHAR(10), GETDATE(), 111))";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int count = ds.Tables[0].Rows.Count;
            int sqlcount = 0;
            for (int i = 0; i < count; i++)
            {
                int belongGroupType = Convert.ToInt16(ds.Tables[0].Rows[i][0].ToString());
                string belongGroupGUID = ds.Tables[0].Rows[i][1].ToString();
                switch (belongGroupType)
                {
                    case 1:
                        if (belongGroupGUID.Equals(userGUID))
                        {
                            isReader = true;
                        }
                        break;
                    case 9: //部門
                        sql = "select id from OrganizationUnit where OID = '" + belongGroupGUID + "'";
                        string orgUnitId = (string)engine.executeScalar(sql);
                        if (si.ownerOrgID.Equals(orgUnitId))
                        {
                            isReader = true;
                        }
                        break;
                    case 21: //群組
                        sql = "select count('x') cnt from Groups a, Group_User b where a.OID=b.GroupOID and a.OID='" + belongGroupGUID + "' and b.UserOID='" + userGUID + "'";
                        sqlcount = (int)engine.executeScalar(sql);
                        if (sqlcount > 0)
                        {
                            isReader = true;
                        }
                        break;
                    default:
                        break;
                }
                if (isReader)
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }

        return isReader;
    }

    /// <summary>
    /// 確認此群組是否為文件類別預設讀者
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="docTypeGUID"></param>
    /// <param name="belongGroupType"></param>
    /// <param name="belongGroupGUID"></param>
    /// <returns></returns>
    protected bool isDefaultReader(AbstractEngine engine, string docTypeGUID, string belongGroupGUID)
    {
        bool isReader = false;
        try
        {
            string sql = "select BelongGroupGUID from SmpDocTypeReader where DocTypeGUID='" + docTypeGUID + "' and BelongGroupGUID='" + belongGroupGUID + "'";
			
            string guid = (string)engine.executeScalar(sql);
            if (!string.IsNullOrEmpty(guid))
            {
                isReader = true;
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return isReader;
    }


    /// <summary>
    /// 是否為此版本文件的調閱人員
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="docGUID"></param>
    /// <param name="userGUID"></param>
    /// <returns></returns>
    protected bool isAccesser(AbstractEngine engine, string docGUID, string userGUID)
    {
        bool isAccesser = false;
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string sql = "select BelongGroupType, BelongGroupGUID from SmpReader where DocGUID='" + docGUID + "' and isnull(EffectiveDate, CONVERT(VARCHAR(10), GETDATE(), 111)) <=CONVERT(VARCHAR(10), GETDATE(), 111) and isnull(ExpiryDate, CONVERT(VARCHAR(10), GETDATE(), 111)) >= CONVERT(VARCHAR(10), GETDATE(), 111) and FromAccess='Y'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                int belongGroupType = Convert.ToInt16(ds.Tables[0].Rows[i][0].ToString());
                string belongGroupGUID = ds.Tables[0].Rows[i][1].ToString();
                switch (belongGroupType)
                {
                    case 1:
                        if (belongGroupGUID.Equals(userGUID))
                        {
                            isAccesser = true;
                        }
                        break;
                    default:
                        break;
                }
                if (isAccesser)
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }

        return isAccesser;
    }
}
