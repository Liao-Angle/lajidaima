using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Net;
using com.dsc.kernal.utility;
using System.IO;
using com.dsc.kernal.factory;
using System.Data;
using System.Xml;
using MOfficeLibrary;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class MOfficeService : System.Web.Services.WebService
{
    public MOfficeService() 
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() 
    {
        return "HelloWorld";
    }

    /// <summary>
    /// 取得表單資料
    /// </summary>
    /// <param name="pRequestXML">
    /// 
    ///     <Request>
    ///        <MCLOUDXmlTag>fetchToDoMOfficeWorkItem</MCLOUDXmlTag>
    ///        <UserID></UserID>      登入者ID
    ///        <WorkitemOID></WorkitemOID> 
    ///        <TargetFormKind></TargetFormKind>  顯示類型
    ///        <Locale></Locale>  語系
    ///    </Request>
    /// 
    /// 
    /// </param>
    /// <returns>
    /// 
    ///     <Response>
    ///     <ReturnInfo>
    ///       <ReturnStatus>Y</ReturnStatus> 是否成功 (Y/N) 
    ///       <ReturnDescribe>No Error</ReturnDescribe>	 開立動作敘述 
    ///     </ReturnInfo> 
    ///     <userInfo>
    ///      <UserOID></UserOID> //使用者OID
    ///      <UserID></UserID> //使用者ID
    ///     </userInfo>
    ///     <action>
    ///      <IsAbleToAbortProcess>False</IsAbleToAbortProcess> //可撤銷
    ///      <IsApprovel>True</IsApproval> //可簽核
    ///      <IsStop>True</Istop>  //可終止
    ///      <IsBack>0</IsBack>  //可退回重辦(0~4)
    ///      <IsBeforAddInvoke>False</IsBeforAddInvoke> //可向前加簽
    ///      <IsAfterAddInvoke>False</IsAfterAddInvoke> //可向後加簽
    ///      <IsNotice></IsNotice>//可寄送通知
    ///      <IsAttachement>True</IsAttachement> //是否有附件元件(有附件不一定有可下載附件) 
    ///     </action>
    ///     <Form FromInstanceOID="表單OID" WorkItemOID="閞卡OID" SerialNo="流程序號" ProcessInstanceOID="流程OID" FromName="表單名稱"> 
    ///         <Head>
    ///            <Field id='欄位名稱' Sequence="橫列呈現順序編號" title='此欄位Label顯示內容'>欄位值</Field>
    ///            <Field id='欄位名稱' Sequence="橫列呈現順序編號" title='此欄位Label顯示內容'>欄位值</Field>
    ///         </Head>   
    ///         <Body>
    ///           <GridID>單身(Grid)代號§單身名稱</GridID>
    ///         </Body> 
    ///     </Form>
    ///     </Response>
    /// 
    /// 
    /// 
    /// </returns>
    [WebMethod]
    public string getMOfficeForm(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "getMOfficeForm";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1; 
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {
            
            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //getMOfficeForm 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string UserID = getRData("UserID", RData);
                string WorkitemOID = getRData("WorkitemOID", RData);
                string TargetFormKind = getRData("TargetFormKind", RData);
                string Locale = getRData("Locale", RData);

                if (!string.IsNullOrEmpty(WorkitemOID))
                {
                    ProcessserialNumber = MC.findProcessserialNumber(WorkitemOID);
                }

                string[] tmp1 = MC.getuserInfo(UserID);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                ret.AppendLine(getuserInfo(tmp1));

                ret.AppendLine(getaction(tmp1, WorkitemOID, ProcessserialNumber, MC));

                ret.Append(getForm(tmp1, ProcessserialNumber, WorkitemOID, Locale, this.Context, MC));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string getMOfficeGrid(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "getMOfficeGrid";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //getMOfficeGrid 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string FormInstanceOID = getRData("FormInstanceOID", RData);
                string GridID = getRData("GridID", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                ret.Append(getDetailForm(FormInstanceOID, GridID, MC));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string getAttachementFormInstanceOID(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "getAttachementFormInstanceOID";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //getAttachementFormInstanceOID 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string WorkItmeOID = getRData("WorkItmeOID", RData);
                string FormInstanceOID = getRData("FormInstanceOID", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                ret.Append(getAttachement(FormInstanceOID, WorkItmeOID, MC, this.Context));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string fetchToDoMOfficeProcessTitle(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "fetchToDoMOfficeProcessTitle";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //fetchToDoMOfficeProcessTitle 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string UserID = getRData("UserID", RData);
                string TargetFormKind = getRData("TargetFormKind", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                string[] tmp1 = MC.getuserInfo(UserID);


                ret.Append(getProcess(UserID, TargetFormKind, tmp1, MC, this.Context));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string fetchToDoMOfficeWorkItem(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "fetchToDoMOfficeWorkItem";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //fetchToDoMOfficeWorkItem 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string UserID = getRData("UserID", RData);
                string ProcessIds = getRData("ProcessIds", RData);
                string TargetFormKind = getRData("TargetFormKind", RData);
                string pageSize = getRData("pageSize", RData);
                string pageNo = getRData("pageNo", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                string[] tmp1 = MC.getuserInfo(UserID);

                ret.Append(getlistSimpleWorkItem(tmp1, ProcessIds, TargetFormKind, pageSize, pageNo, MC));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string doFormApprovalAgree(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "doFormApprovalAgree";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //doFormApprovalAgree 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string UserID = getRData("UserID", RData);
                string WorkitemOID = getRData("WorkitemOID", RData);
                string SerialNo = getRData("SerialNo", RData);
                string Comments = getRData("Comments", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                string[] tmp1 = MC.getuserInfo(UserID);

                ret.Append(getContentText(tmp1, WorkitemOID, SerialNo, Comments, WebMethodName, MC, this.Context));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string doFormTerminated(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "doFormTerminated";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //doFormApprovalAgree 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string UserID = getRData("UserID", RData);
                string WorkitemOID = getRData("WorkitemOID", RData);
                string SerialNo = getRData("SerialNo", RData);
                string Comments = getRData("Comments", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                string[] tmp1 = MC.getuserInfo(UserID);

                ret.Append(getContentText(tmp1, WorkitemOID, SerialNo, Comments, WebMethodName, MC, this.Context));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string getUserOpinitionUserOID(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "getUserOpinitionUserOID";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //getUserOpinitionUserOID 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string UserOID = getRData("UserOID", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                ret.Append(getPhrase(UserOID, MC, this.Context));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }


    [WebMethod]
    public string getApproveLog(String pRequestXML)
    {
        System.Text.StringBuilder ret = new System.Text.StringBuilder();
        string WebMethodName = "getApproveLog";
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        XmlNode node = null;
        DateTime dt1;
        DateTime dt2;
        MOfficeLibrary.MOfficeLibrary MC = null;
        dt1 = DateTime.Now;
        try
        {

            MC = new MOfficeLibrary.MOfficeLibrary();
            if (MC.MOfficeIntegration())
            {
                string ProcessserialNumber = "";
                string[,] RData = getRequestData(pRequestXML);
                //getApproveLog 必要Request
                string MCLOUDXmlTag = getRData("MCLOUDXmlTag", RData);
                string ProcessInstanceOID = getRData("ProcessInstanceOID", RData);
                string Locale = getRData("Locale", RData);

                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>Y</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>No Error</ReturnDescribe>");//開立動作敘述 

                ret.Append(getList(ProcessInstanceOID, MC, this.Context, Locale));

            }
            else
            {
                ret.AppendLine("<Response>");
                ret.AppendLine("    <ReturnInfo>");
                ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
                ret.AppendLine("        <ReturnDescribe>尚未開啟M-Office整合功能</ReturnDescribe>");//開立動作敘述 
            }
        }
        catch (Exception ex)
        {
            ret.Remove(0, ret.Length);
            ret.AppendLine("<Response>");
            ret.AppendLine("    <ReturnInfo>");
            ret.AppendLine("        <ReturnStatus>N</ReturnStatus>");//是否成功 (Y/N)
            ret.AppendLine("        <ReturnDescribe>" + ex.Message + ";" + ex.StackTrace + "</ReturnDescribe>");//開立動作敘述 
        }
        ret.AppendLine("    </ReturnInfo>");
        ret.AppendLine("</Response>");

        dt2 = DateTime.Now;
        TimeSpan ts = dt2.Subtract(dt1);
        writeLog(WebMethodName, dt1, dt2, ts, pRequestXML, ret.ToString());
        return ret.ToString();
    }



    /// <summary>
    /// 取得List區塊 表單聯絡人資料 / 簽核歷程
    /// </summary>
    /// <param name="ProcessInstanceOID">流程實例序號</param>
    /// <param name="MC"></param>
    /// <param name="httpContext"></param>
    /// <param name="Locale"></param>
    /// <returns></returns>
    private string getList(string ProcessInstanceOID, MOfficeLibrary.MOfficeLibrary MO, HttpContext httpContext, string Locale)
    {
        try
        {
            return MO.getList(ProcessInstanceOID, httpContext, Locale).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得 簽核意見 區塊
    /// </summary>
    /// <param name="UserOID"></param>
    /// <param name="MC"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private string getPhrase(string UserOID, MOfficeLibrary.MOfficeLibrary MO, HttpContext httpContext)
    {
        try
        {
            return MO.getPhrase(UserOID, httpContext).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得 ContentText 區塊
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="WorkitemOID"></param>
    /// <param name="SerialNo"></param>
    /// <param name="Comments"></param>
    /// <param name="WebMethodName"></param>
    /// <param name="MO"></param>
    /// <returns></returns>
    private string getContentText(string[] userInfo, string WorkitemOID, string SerialNo, string Comments, string WebMethodName, MOfficeLibrary.MOfficeLibrary MO, System.Web.HttpContext httpContext)
    {
        try
        {
            return MO.getContentText(userInfo[1].ToString(), userInfo[0].ToString(), WorkitemOID, SerialNo, Comments, WebMethodName, httpContext).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得 list 跟 SimpleWorkItem 區塊
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="ProcessIds"></param>
    /// <param name="TargetFormKind"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNo"></param>
    /// <param name="MO"></param>
    /// <returns></returns>
    private string getlistSimpleWorkItem(string[] userInfo, string ProcessIds, string TargetFormKind, string pageSize, string pageNo, MOfficeLibrary.MOfficeLibrary MO)
    {
        try
        {
            return MO.getlistSimpleWorkItem(TargetFormKind, userInfo[1].ToString(), userInfo[0].ToString(), ProcessIds, pageSize, pageNo, this.Context).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得Process區塊
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="TargetFormKind"></param>
    /// <param name="userInfo"></param>
    /// <param name="MO"></param>
    /// <returns></returns>
    private string getProcess(string UserID, string TargetFormKind, string[] userInfo, MOfficeLibrary.MOfficeLibrary MO, System.Web.HttpContext httpContext)
    {
        try
        {

            return MO.getProcess(TargetFormKind, userInfo[1].ToString(), userInfo[0].ToString(), httpContext).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得Attachement區塊
    /// </summary>
    /// <param name="FormInstanceOID"></param>
    /// <param name="WorkItmeOID"></param>
    /// <param name="MC"></param>
    /// <returns></returns>
    private string getAttachement(string FormInstanceOID, string WorkItmeOID, MOfficeLibrary.MOfficeLibrary MO, System.Web.HttpContext httpContext)
    {
        try
        {

            return MO.getAttachement(FormInstanceOID, WorkItmeOID, httpContext).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得DetailForm區塊
    /// </summary>
    /// <param name="FormInstanceOID"></param>
    /// <param name="GridID"></param>
    /// <param name="MO"></param>
    /// <returns></returns>
    private string getDetailForm(string FormInstanceOID, string GridID, MOfficeLibrary.MOfficeLibrary MO)
    {
        try
        {
            return MO.getDetailForm(FormInstanceOID, GridID, this.Context).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得Form區塊
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="ProcessserialNumber"></param>
    /// <param name="WorkitemOID"></param>
    /// <param name="MO"></param>
    /// <returns></returns>
    private string getForm(string[] userInfo, string ProcessserialNumber, string WorkitemOID, string Locale, System.Web.HttpContext httpContext, MOfficeLibrary.MOfficeLibrary MO)
    {
        try
        {
            return MO.getForm(ProcessserialNumber, WorkitemOID, Convert.ToString(userInfo[1]), Convert.ToString(userInfo[0]), Locale, httpContext).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 取得action區塊
    /// </summary>
    /// <param name="userInfo"></param>
    /// <param name="WorkitemOID"></param>
    /// <param name="ProcessserialNumber"></param>
    /// <param name="MO"></param>
    /// <returns></returns>
    private string getaction(string[] userInfo, string WorkitemOID, string ProcessserialNumber, MOfficeLibrary.MOfficeLibrary MO)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string[] tmp1 = userInfo;
        string[] tmp = MO.getaction(ProcessserialNumber, WorkitemOID, Convert.ToString(tmp1[1]), Convert.ToString(tmp1[0]));
        sb.AppendLine("    <action>");
        sb.AppendLine("        <IsAbleToAbortProcess>" + tmp[0] + "</IsAbleToAbortProcess>");
        sb.AppendLine("        <IsApprovel>" + tmp[1] + "</IsApprovel>");
        sb.AppendLine("        <IsStop>" + tmp[2] + "</IsStop>");
        sb.AppendLine("        <IsBack>" + tmp[3] + "</IsBack>");
        sb.AppendLine("        <IsBeforAddInvoke>" + tmp[4] + "</IsBeforAddInvoke>");
        sb.AppendLine("        <IsAfterAddInvoke>" + tmp[5] + "</IsAfterAddInvoke>");
        sb.AppendLine("        <IsNotice>" + tmp[6] + "</IsNotice>");
        sb.AppendLine("        <IsAttachement>" + tmp[7] + "</IsAttachement>");
        sb.AppendLine("    </action>");
        return sb.ToString();
    }
    /// <summary>
    /// 取得userInfo區塊
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    private string getuserInfo(string[] userInfo)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string[] tmp = userInfo;
        sb.AppendLine("    <userInfo>");
        sb.AppendLine("        <UserOID>" + tmp[0] + "</UserOID>");
        sb.AppendLine("        <UserID>" + tmp[1] + "</UserID>");
        sb.AppendLine("    </userInfo>");

        return sb.ToString();
    }

    private string getRData(string Tag, string[,] RData)
    {
        string ret = "";
        for (int i = 0; i < RData.GetLength(0); i++)
        {
            if (RData[i, 0].Equals(Tag))
            {
                if (string.IsNullOrEmpty(RData[i, 1]))
                {
                    ret = "";
                }
                else
                {
                    ret = RData[i, 1];
                }
            }
        }
        return ret;
    }

    private string[,] getRequestData(String RequestXML)
    {
        string[,] ret = null;
        XMLProcessor xp = null;
        XmlNodeList Nlist = null;
        //XmlNode node = null;
        try
        {
            if (!string.IsNullOrEmpty(RequestXML))
            {
                xp = new XMLProcessor(RequestXML, 1);
                Nlist = xp.selectAllNodes("//Request").Item(0).ChildNodes;
                ret = new string[Nlist.Count, 2];
                for (int i = 0; i < Nlist.Count; i++)
                {
                    ret[i, 0] = Nlist.Item(i).Name;
                    ret[i, 1] = Convert.ToString(Nlist.Item(i).InnerText);
                }
            }
        }
        catch (Exception ex)
        {
            ret = new string[,] { 
                {"ERR",ex.Message}
            };
            return ret;
        }

        return ret;
    }
    /// <summary>
    /// 寫入Log紀錄(LogFolder) 配合啟用偵錯記錄檔設定
    /// </summary>
    /// <param name="WebMethodName">WebMethod名稱 WebService名稱</param>
    /// <param name="start">開始時間起</param>
    /// <param name="end">結束時間迄</param>
    /// <param name="executionTime">執行時間</param>
    /// <param name="pRequestXML">所收到之要求</param>
    private void writeLog(string WebMethodName, DateTime start, DateTime end, TimeSpan executionTime, string pRequestXML, string pResponseXML)
    {
        //******************************紀錄處理時間
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();
        string connectString = acs.connectString;
        string engineType = acs.engineType;
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sqlz = "select SMVPAAA009 from SMVPAAA";
        DataSet dss = engine.getDataSet(sqlz, "TEMP");
        engine.close();
        string isDebugPage = "N";
        if (dss.Tables[0].Rows.Count == 0)
        {
            isDebugPage = "N";
        }
        else
        {
            isDebugPage = dss.Tables[0].Rows[0][0].ToString();
        }

        if (isDebugPage.Equals("Y"))
        {
            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 10).Replace("/", "");
            fname = Server.MapPath("~/LogFolder/" + fname + "_MOfficeIntegration.log");
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fname, true);
            sw.WriteLine("**************");
            sw.WriteLine("WebMethodName:" + WebMethodName);
            sw.WriteLine("pRequestXML:" + pRequestXML);
            sw.WriteLine("pResponseXML:" + pResponseXML);
            sw.WriteLine("startTime:" + start.ToString("yyyy/MM/dd HH:mm:ss.fffffff"));
            sw.WriteLine("completeTime:" + end.ToString("yyyy/MM/dd HH:mm:ss.fffffff"));
            sw.WriteLine("runTime:" + executionTime.ToString());
            sw.Close();

        }

    }

}