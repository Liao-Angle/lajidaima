using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RadPdf.Data.Document;
using RadPdf.Data.Document.Common;
using RadPdf.Data.Document.Objects;
using RadPdf.Data.Document.Objects.FormFields;
using RadPdf.Data.Document.Objects.Shapes;
using System.Drawing;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using com.dsc.kernal.utility;
using RadPdf.Data.Document;
using RadPdf.Data.Document.Common;
using RadPdf.Data.Document.Objects;
using RadPdf.Data.Document.Objects.Shapes;

public partial class SmpProgram_Maintain_PDFViewer_ViewDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["PDFViewerFile"] = @"c:\ECPSite\WebFormPT\web\pdfs\exception.pdf";
        //Session["PDFViewerFile"] = @"c:\ECPSite\WebFormPT\web\pdfs\ECP程(CutePdf).pdf";
        //Session["PDFViewerFile"] = @"c:\ECPSite\WebFormPT\web\pdfs\REL0023 Battery Standalone Accelerated Fatigue Test A02.pdf";
        //Session["PDFViewerFile"] = @"D:\Documents\Visual Studio 2010\Projects\ECP\WebFormPT\web\pdfs\ECP程(CutePdf).pdf";
        //Session["External"] = "Y";

        string UserID = (string)Session["UserID"];
        string UserName = (string)Session["UserName"];
        string gfp = GlobalProperty.getProperty("global", "pdfTempPath");
        string doc_file = (string)Session["PDFViewerFile"];
        string Ext_Flag = (string)Session["External"]; // Y or N 外來文件
        string strUser = UserID + UserName;
        string strDT = String.Format("{0:yyyy/MM/dd HH:mm}", DateTime.Now);
        //this.Label1.Text = Ext_Flag;
        if (string.IsNullOrEmpty(Ext_Flag))
            Ext_Flag = "N";
        if (!string.IsNullOrEmpty(doc_file))
        {
            string soureFile = doc_file;
            //string filename = @"D:\Documents\Visual Studio 2010\Projects\ECP\WebFormPT\web\pdfs\temp_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            //string filename = @"c:\ECPSite\WebFormPT\web\pdfs\temp_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";

            string filename = @"" + gfp + "viewertemp_" + UserID + "_" +DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
            if (Ext_Flag == "N")
            {
                string strFont = @"c:\windows\Fonts\kaiu.ttf";
                //string imgPath = @"~\pdfs\SIMPLO.jpg";
                BaseFont bfChinese = BaseFont.CreateFont(strFont, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath(imgPath));
                //img.SetAbsolutePosition(10, 250);
                PdfGState gstate = new PdfGState();
                gstate.FillOpacity = 0.15f;
                gstate.StrokeOpacity = 0.15f;
                PdfReader reader = new PdfReader(soureFile);
                using (PdfStamper pdfStamper = new PdfStamper(reader, new FileStream(filename, FileMode.Create)))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        iTextSharp.text.Rectangle pageSize = reader.GetPageSizeWithRotation(i);
                        PdfContentByte pdfPageContents = pdfStamper.GetOverContent(i);
                        pdfPageContents.SaveState();
                        pdfPageContents.SetGState(gstate);
                        //pdfPageContents.AddImage(img);
                        pdfPageContents.BeginText();
                        pdfPageContents.SetFontAndSize(bfChinese, 34);
                        pdfPageContents.SetRGBColorFill(0, 0, 255);
                        float textAngle = 45.0f;
                        //pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, strUser,
                        //                 pageSize.Width / 2, pageSize.Height / 2 + 140f, textAngle);
                        pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, strUser + strDT,
                                         (pageSize.Width / 2) - 130f, pageSize.Height / 2 + 70f, textAngle); // + 70f
                        pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "SIMPLO CONFIDENTIAL DOCUMENT",
                                         (pageSize.Width / 2) + 130f, pageSize.Height / 2 - 70f, textAngle);
                        pdfPageContents.EndText();
                        pdfPageContents.RestoreState();
                    }
                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();
                    reader.Close();
                }
            }
            else
                filename = soureFile;
            Session["PDFViewerFile"] = "";
            Session["External"] = "";
            byte[] pdfData = System.IO.File.ReadAllBytes(filename);
            this.PdfWebControl1.CreateDocument("Document Name", pdfData);
            if (Ext_Flag == "Y")
            {
                PdfDocumentEditor Document1 = this.PdfWebControl1.EditDocument();
                for (int i = 0; i < Document1.Pages.Count; i++)
                {
                    PdfTextShape c1 = (PdfTextShape)Document1.Pages[i].CreateObject(PdfObjectCreatable.ShapeText);
                    c1.Font.Color = new PdfColor(Color.Red);
                    c1.Text = strUser + " SIMPLO CONFIDENTIAL DOCUMENT";
                    c1.Font.Size = 20;
                    c1.Left = 30;
                    c1.Height = 100;
                    c1.Width = 800;
                    c1.Opacity = 20;

                    PdfTextShape c2 = (PdfTextShape)Document1.Pages[i].CreateObject(PdfObjectCreatable.ShapeText);
                    c2.Font.Color = new PdfColor(Color.Red);
                    c2.Text = strDT;
                    c2.Font.Size = 20;
                    c2.Left = 30;
                    c2.Top = 20;
                    c2.Height = 100;
                    c2.Width = 400;
                    c2.Opacity = 20;
                }
                Document1.Save();
            }
        }
    }

    private void alert(string msg)
    {
        string mstr = "<script language=javascript>";
        mstr += "function SMSC_Msg(){";
        mstr += "alert('" + msg + "');";
        mstr += "}";
        mstr += "window.setTimeout('SMSC_Msg()', 100);";
        mstr += "</script>";
        ClientScriptManager cm = Page.ClientScript;
        Type ctype = Page.GetType();
        cm.RegisterStartupScript(ctype, this.GetType().Name + "_" + ClientID, mstr);
    }

}