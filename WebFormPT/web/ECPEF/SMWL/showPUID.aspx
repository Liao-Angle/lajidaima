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
        string urlQQ = Request.QueryString["url"].ToString();
        string fakePageUniqueID = Request.QueryString["fakePageUniqueID"].ToString();
        urlQQ = "../../" + urlQQ.Replace(fakePageUniqueID, QQPUID);
        //string formpath = Request.QueryString["formpath"].ToString();
        //string urlQQ = "../../" + formpath + "?ParentPanelID=" + QQCurPanelID;
        urlQQ += "&ParentPanelID=" + QQCurPanelID;
        urlQQ += "&DataListID=ListTable";
        //urlQQ += "&ParentPageUID=" + QQPUID;
        urlQQ += "&UIType=Process";
        //urlQQ += "&ObjectGUID=PEREF001_5a89ec32-63b7-4231-8e0a-b913fd899ba5";
        //urlQQ += "&HistoryGUID=";
        //urlQQ += "&FlowGUID=PEREF00100000303";
        //urlQQ += "&ACTID=";
        //urlQQ += "&PDID=PEREF001";
        //urlQQ += "&PDVer=";
        //urlQQ += "&ACTName=%u76f4%u5c6c%u4e3b%u7ba1";
        //urlQQ += "&UIStatus=5";
        //urlQQ += "&WorkItemOID=6dab9f0ecf5d1004819bfb1da96da0a2";
        urlQQ += "&TargetWorkItemOID=";
        //urlQQ += "&workAssignmentOID=6dab9f10cf5d1004819bfb1da96da0a2";
        urlQQ += "&assignmentType=0";
        //urlQQ += "&reassignmentType=";
        urlQQ += "&manualReassignType=1";
        urlQQ += "&IsAllowWithDraw=true";
        urlQQ += "&IsMaintain=N";
        urlQQ += "&CurPanelID=" + Convert.ToString(Convert.ToInt16(QQCurPanelID) + 1);

        Response.Redirect(urlQQ);
    }
    catch (Exception xe)
    {
        Response.Write(com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string031", "伺服器處理發生錯誤, 請重新登入. 其他錯誤訊息: ") + xe.Message);
    }

%>
