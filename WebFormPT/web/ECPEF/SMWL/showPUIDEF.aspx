<%@ Page Language="c#" ValidateRequest="false" %>

<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="com.dsc.kernal.utility" %>
<%@ Import Namespace="com.dsc.kernal.agent" %>
<%@ Import Namespace="com.dsc.kernal.factory" %>
<%@ Import Namespace="com.dsc.kernal.global" %>
<%@ Import Namespace="com.dsc.kernal.databean" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Runtime.Remoting" %>
<%
    // 在這裡放置使用者程式碼以初始化網頁
    Response.Clear();
    Response.Cache.SetExpires(DateTime.Now);
    Response.ContentType = "text/html";

    try
    {
        string QQPUID = (string)Session["QQPUID"];
        string QQCurPanelID = (string)Session["QQCurPanelID"];
        string urlQQ = Utility.UrlDecode(Request.QueryString["url"].ToString());
        string fakePageUniqueID = Request.QueryString["fakePageUniqueID"].ToString();
        urlQQ = urlQQ.Replace(fakePageUniqueID, QQPUID);
        urlQQ += "&ParentPanelID=" + QQCurPanelID;
        urlQQ += "&CurPanelID=" + Convert.ToString(Convert.ToInt16(QQCurPanelID) + 1);
        Response.Redirect(urlQQ);
        //Response.Write(urlQQ);
    }
    catch (Exception xe)
    {
        Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string031", "伺服器處理發生錯誤, 請重新登入. 其他錯誤訊息: ") + xe.Message);
    }

%>
