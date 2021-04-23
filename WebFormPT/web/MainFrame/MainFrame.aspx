<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainFrame.aspx.cs" Inherits="MainFrame_MainFrame" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="MainFrame.css" rel="stylesheet" type="text/css" />
    <title><%=WindowTitle %></title>
    <script language=javascript>
		var pressQuit=0;
		var count=0;
		var isChangeUser=false;
        //Firefox修正處理事件寫法
        document.onkeypress=function aaa()
        { 
            //處理FireFox
            var evt = getEvent(); 
            if((evt.keyCode==8) && ((evt.target.id == '' || evt.srcElement.form == null || evt.srcElement.isTextEdit == false))){return false;}
        }  
        document.onkeydown = function handleKeyDown() 
        {
            var evt = getEvent();
            //有處理到 IE、Chrome
            if((evt.keyCode==8) && ((evt.srcElement.form == null || evt.srcElement.isTextEdit == false))){return false;} 
            if((evt.keyCode==71) && (evt.ctrlKey))
            {openCTRLG();}
        }
       
       
        
        function openCTRLG(){
            var postData=prompt('ProgramID','');
            if(postData==null) return;
            
	        var xmlhttp=null;
            //Firefox修正xmlrequest宣告
            xmlhttp=createXMLHTTP();
            xmlhttp.open('POST', 'MainFrame.aspx' , false);
            xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
            xmlhttp.send('mtd='+postData);
            
            retv=xmlhttp.responseText;
            if(retv!=''){
                retary=retv.split('$$$');
                openWindowMax(retary[0],'<%=Page.ResolveUrl("~/") %>'+retary[1]);
            }
        }
		
		function window_onbeforeunload(){
		    if(isChangeUser) return;
		    
		    if(pressQuit==0){
	            if (count == 0) {
	            <%if(((bool)Session["IsEmbeddedPortal"])==false){ %>
                    //修正事件取法
                    var evt = getEvent();
		            evt.returnValue = "<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string001", "感謝您使用本系統.  再見!")%>";		
                    //FF不支援window.event.reason
		            if (evt.reason != null && evt.reason == false){
			            cancelBubble(evt)
		            }	
		        <%} %>
	            }
	        }
        }
        function window_onunload(e){
		//跨站整合客製
		<%if(((bool)Session["crossSite"])==true){ %>
			isChangeUser=true;
		<%		Session["crossSite"] =false; } %>
            if(isChangeUser) return;
            
            if(pressQuit==0){
                count++;
	            var objHTTP = createXMLHTTP() ;
	            objHTTP.open('Get','../Logout.aspx?SessionID=<%=Session.SessionID%>',false);
	            objHTTP.send();
	            if (e == undefined) {
	                objHTTP = createXMLHTTP() ;
	                objHTTP.open('Get','../Logout.aspx?SessionID=<%=Session.SessionID%>',false);
	                objHTTP.send();
	            <%if(((bool)Session["IsEmbeddedPortal"])==false){ %>
		            window.location.href = "../Default.aspx";
		        <%} %>
	            }else{
		            window.open(e);
		            window.close();
	            }
		    }
        }
        function closeEngine(pageID){                
                var objHTTP = createXMLHTTP() ;
	            objHTTP.open('Get','../Program/DSCGPFlowService/Public/CloseEngine.aspx?pageUniqueID='+pageID,true);	            
	            objHTTP.send();
        }
        window.history.forward();

    </script>
    
    <script src="getMainFrameJS.aspx" language="javascript" type="text/javascript"></script>
    <script src="../JS/ShareScript.js" language="javascript" type="text/javascript"></script>
    <script src="../DSCWebControlRunTime/DSCWebControlUI/PageSecure.js" language="javascript" type="text/javascript"></script> 
</head>
<body  leftmargin=0 topmargin=0 style="overflow:hidden" onunload='window_onunload();' onbeforeunload='window_onbeforeunload();' onresize='Window_Resize();' onload='<%if(!SMVPAAA006){%>ToggleBanner();<%}%>Window_Resize();<%if(isUseSetting){ %>showPanelNode()<%} %>;Window_Resize();<%if(Request.QueryString["runMethod"]!=null){%>openPortalLink();<%}%>'>
<form id="form1" runat="server" method="post">
<input id='focusBug' style="position:absolute ;z-index:-9999999;width:0;height:0 " />
<div id='BannerArea'><table border=0 height=61 width=100% style="background-image:url(../Images/banner_bg.gif)" cellspacing=0 cellpadding=0><tr><td width=350px><img src='../Images/banner_left.gif' /></td><td width=99%>&nbsp;</td><td width=350px><img src='../Images/banner_right.gif' /></td></tr></table></div>
<span class='WindowToolBar' style="width:100%;height:20px; vertical-align:bottom;padding:0px" onclick='closeAllMenu()'>
<table id='WindowToolBar' class='WindowToolBar' border=0 cellspacing=0 cellpadding=0 width=100% height=10px style="padding:0px 0px 0px 0px;margin:0px 0px 0px 0px"><tr height=16px><td><%if(SMVPAAA008){ %><span onmouseover='showMainframeMenuF("Menu_Layout");' onclick='showMainframeMenu("Menu_Layout");' class='MainMenuItem'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string002", "版面排列")%><img src='Images/MenuItem.gif' /></span><%} if(SMVPAAA008){ %><span onmouseover='showMainframeMenuF("Menu_SystemMenu");' onclick='showMainframeMenu("Menu_SystemMenu");' class='MainMenuItem'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string003", "選單功能")%><img src='Images/MenuItem.gif' /></span><%} if(SMVPAAA008 && SMVPAAA003){ %><span onmouseover='showMainframeMenuF("Menu_Display");' onclick='showMainframeMenu("Menu_Display");' class='MainMenuItem'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string004", "版面設定")%><img src='Images/MenuItem.gif' /></span><%} if(SMVPAAA008){ %><span onmouseover='showAllWindowMenuF();' onclick='showAllWindowMenu();' class='MainMenuItem'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string005", "版面列表")%><img src='Images/MenuItem.gif' /></span><%} if(SMVPAAA008 && SMVPAAA007){ %><span onclick='ToggleBanner();' class='MainMenuItem'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string006", "切換標題")%><img id='ToggleBannerGraph' src='Images/MenuItemUp.gif' /></span><%} %></td><td align=right >
<%if (SMVPAAA041)
  {%>
<span id="changeLanguage"  class="LoginInfo">
<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string033", "語系切換:")%>
     <asp:dropdownlist style="font-size:7pt" ID="ddlCL" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlCL_SelectedIndexChanged">
        </asp:dropdownlist>
   </span>
   <%}%>
<span id=LoginInfo class="LoginInfo" style="text-align:right;" align=right>
<span style="cursor:pointer;" onclick='clickUser()'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string007", "登入使用者:")%></span><%=LoginInfo%>
</span>
<span onclick='logout()' style="cursor:pointer;" class=LoginInfo>&nbsp;&nbsp;[<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string008", "登出")%>]</span><span onclick='SysInfo()' style="cursor:pointer;" class=LoginInfo>&nbsp;&nbsp;<img src='Images/efHelp.gif' border=0 align=absmiddle title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string009", "系統資訊")%>' /></span><% if(SMVPAAA017&&com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page).Equals(com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE)){ %><span onclick='downloadActiveX()' class='LoginInfo' style="cursor:pointer;">&nbsp;<img src='Images/download.gif' border=0 align=absmiddle width=16 height=16 title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string032", "下載元件")%>' /></span><%}%></td></tr></table>
</span>
<div id='WindowContent' style="background-repeat:no-repeat; background-position:right" class='WorkSpaceBackground' onmouseout2='Window_Content_MouseEnd();' onmousemove='Window_Content_MouseMove();' onmouseup='Window_Content_MouseEnd();' onlosecapture='Window_Content_MouseEnd();' style="overflow:hidden;border-width:1px;top:81px;left:0px;width:900px;height:450px">
</div>
<!--版面列表-->
<div id='AllWindowPanel' onclick='closeAllMenu()' style="display:none;position:absolute;width:250px;height:24px;top:100px;left:200px;z-index:9998;">
<div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
<iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
<table class='MenuList' id='AllWindowPanel_Table' border=0 cellpadding=2 cellspacing=0 width=100% height=100%>
</table>
</div>
</div>
<!--選單：版面排列-->
<div id='Menu_Layout' onclick='closeAllMenu()' style="display:none;position:absolute;width:180px;height:24px;top:100px;left:200px;z-index:9998;">
<div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
<iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
<table class='MenuList' border=0 cellpadding=2 cellspacing=0 width=100% height=100%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='Panel_Rearrange();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string010", "層狀排列")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='Panel_VerticalArrange();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string011", "垂直並排")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='Panel_HorizontalArrange();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string012", "水平並排")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'></td>
    <td height="1px" class='MenuLine'></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='Panel_CloseNoneDock();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string013", "關閉停泊外所有視窗")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='Panel_CloseAll();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string014", "關閉所有視窗")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'></td>
    <td height="1px" class='MenuLine'></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='Panel_MinimizeNoneDock();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string015", "停泊外所有視窗最小化")%></a></td>
</tr>

</table>
</div>
</div>
<!--選單：版面排列-->
<!--選單：顯示選單-->
<div id='Menu_SystemMenu' onclick='closeAllMenu()' style="display:none;position:absolute;width:150px;height:24px;top:100px;left:200px;z-index:9998;">
<div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
<iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
<table class='MenuList' border=0 cellpadding=2 cellspacing=0 width=100% height=100%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='showSystemMenu();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string016", "顯示系統選單")%></a></td>
</tr>
<%if(SMVPAAA002){%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='showUserMenu();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string017", "顯示個人化選單")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='setUserMenu();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string018", "設定個人化選單")%></a></td>
</tr>
<%}%>
<%if(SMVPAAA004){%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='openCustomURL();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string019", "開啟外部連結視窗")%></a></td>
</tr>
<%}%>
</table>
</div>
</div>
<!--選單：顯示選單-->
<!--選單：版面設定-->
<div id='Menu_Display' onclick='closeAllMenu()' style="display:none;position:absolute;width:185px;height:24px;top:100px;left:200px;z-index:9998;">
<div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
<iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
<table class='MenuList' border=0 cellpadding=2 cellspacing=0 width=100% height=100%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='savePanelNode();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string020", "儲存個人版面設定")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='showPanelNode();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string021", "載入個人版面設定")%></a></td>
</tr>
<%if (isSaveDefault){ %>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='saveSystemNode();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string022", "儲存預設版面設定")%></a></td>
</tr>
<%} %>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='showSystemNode();'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string023", "載入預設版面設定")%></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'></td>
    <td height="1px" class='MenuLine'></td>
</tr>
<tr>
    <td width=15px class='MenuHead' align=center><input type=checkbox id='Snap_To_Grid' onclick='SnapToGrid_Click();' /></td>
    <td class='MenuContent'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string024", "貼齊格線")%>:<input type=text onmousedown='disableEvent()' onblur='enableEvent()' value='5' style="border-width:1px;border-color:black;width:25px" maxlength="2" id='SnapGridSize' />px</td>
</tr>
<tr>
    <td width=15px class='MenuHead' align=center><input type=checkbox checked id='User_New_Window' onclick='UserNewWindow_Click();' /></td>
    <td class='MenuContent'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string025", "以新視窗執行")%></td>
</tr>
<tr>
    <td width=15px class='MenuHead' align=center><input type=checkbox id='New_Window_Save' onclick='NewWindowSave_Click();' /></td>
    <td class='MenuContent'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string026", "新視窗預設可儲存")%></td>
</tr>
<tr>
    <td width=15px class='MenuHead' align=center><input type=checkbox id='New_Window_Dock' onclick='NewWindowDock_Click();' /></td>
    <td class='MenuContent'><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("MainFrame.aspx.language.ini", "global", "string027", "新視窗預設可停泊邊界")%></td>
</tr>
</table>
</div>
</div>
<!--選單：版面設定-->
<!--Portal搬移框-國昌20100318-->
<div id=MoveFrame style="display:none;position:absolute;width:200px;height:100px;left:150px;top:300px;z-index:99998;border-style:solid;border-width:3px;" onmousemove='Window_Content_MouseMove();' onmouseup='Window_Content_MouseEnd();' ondragover='cancelBubble(getEvent());stopEvent(getEvent());'></div>
<div id=AllMaskPanel style="display:none;position:absolute;width:100%;height:100%;left:0px;top:0px;z-index:99998;background-color:#AAAAAA;opacity:0;filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0)';"  onmousemove='Window_Content_MouseMove();' onmouseup='Window_Content_MouseEnd();' ondrag='cancelBubble(getEvent());stopEvent(getEvent());'></div>
<!--/Portal搬移框-國昌20100318-->
<iframe src='RestoreSession.aspx' width=0 height=0 id=RefreshFrame name=RefreshFrame></iframe>
<!--系統即時通知-->
<div id='SystemAlert' style="display:none;position:absolute;width:200px;height:116px;top:0px;left:0px;z-index:1000">
    <div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
    <iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
    <table class='AlertBorder' border=0 cellpadding=0 cellspacing=0 width=100% height=100% style="background-image:url(Images/MessageBackground.gif);">
    <tr height=22px class='AlertHead'>
        <td id='AlertTitle' width=185px></td>
        <td width=15px onclick='closeAlert();' style="cursor:pointer;">X</a></td>
    </tr>
    <tr>
        <td colspan=2 class='AlertContent'><a href='#' onclick='showAlertMessage();' id='AlertContent'></a></td>
    </tr>
    </table>
    </div>
</div>
<!--系統即時通知-->
</form>
</body>
</html>
<%=SMVPAAA016Text %>
<%=SMVPAAA005Text %>
