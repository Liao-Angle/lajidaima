<!--20090527 Modify Eric : PanelID-->
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getMainFrameJS.aspx.cs" Inherits="MainFrame_getMainFrameJS" %>
<%@ Import namespace="System.IO"%>
<%@ Import namespace="System.Text"%>
<%@ Import namespace="System.Web"%>
<%@ Import namespace="System.Data"%>
<%@ Import namespace="System.Collections"%>

var urlHead="../";
var splitter="#!#!#!#";
var lsplitter="#*#*#*#";
var oriTopBannerHeight=79;
var topBannerHeight=79;
var toolBarHeight=20;
var systemMenuTitle='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string001", "系統選單")%>';
var userMenuTitle='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string002", "個人選單")%>';
var curProcessID='<%=Server.UrlEncode((string)Session["processid"]) %>';
var isAuthorizationMode = '<%=(string)((new com.dsc.kernal.factory.IOFactory()).getEngine((string)Session["engineType"], (string)Session["connectString"])).executeScalar("select SMVPAAA038 from SMVPAAA")%>';

var menuFix=false;
var isNewWindow=true;
var isSnapToGrid=false;
var SnapGridSize=5;
var isNewWindowSave=false;
var isNewWindowDock=false;
var panelAry=new Array();


var panelDownIndex=-1;
var panelDownStartx=-1;
var panelDownStarty=-1;

var panelResizeIndex=-1;
var panelWEResizeIndex=-1;
var panelNSResizeIndex=-1;

//檢查雙點flag for Chrome Safari Opera
var checkDblClickFlag = 0;

//相容瀏覽器用取基礎點函式
function getBasePoint()
{
    if (<%=com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page).Equals(com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE).ToString().ToLower() %>)
    {   return 0;}
    else
    {   return document.getElementById('WindowContent').offsetTop;}
}

function changeMenuFix(vle)
{
    menuFix=vle;
}
function openWindowGeneral(title, url, bWidth, bHeight, frameType, isMax, newWindow)
{
     if(title.length>1)
    {
        if(title.substring(0,1)=='\t'){
            var isFoundSP7=false;
		    for(var i=0;i<panelAry.length;i++){
		        if(panelAry[i].title.length>1){
		            if(panelAry[i].title.substring(0,1)=='\t'){
		                isFoundSP7=true;
		                Panel_Close_Silence(panelAry[i].panelID);
		                break;
		            }
		        }
		    }           
		    if(isFoundSP7){
		        //alert('已經開啟一張SP7表單, 請先關閉該張表單');
		        //return;
		    }
        }
    }
    if(isMax){
        bWidth=100;
        bHeight=100;
    }
    var oldWindow=isNewWindow;
    isNewWindow=newWindow;
    var curPanelID=openWindow2(title, url, bWidth, bHeight, frameType);
    isNewWindow=oldWindow;
    
    if(isMax){
        getAvailabeMaxSize(curPanelID);
    }
    return curPanelID;
}
function openWindow(title, url, bWidth, bHeight, frameType, isMax)
{
    
    if(isMax){
        bWidth=100;
        bHeight=100;
    }
    var curPanelID=openWindow2(title, url, bWidth, bHeight, frameType);
    if(isMax){
        getAvailabeMaxSize(curPanelID);
    }
    return curPanelID;
}
function openWindowMax(title, url)
{
    
    var curPanelID=openWindow2(title, url, 300, 100,'');   
    getAvailabeMaxSize(curPanelID);
    return curPanelID;
}
function openWindow1(title, url, bWidth, bHeight)
{
    
    return openWindow2(title, url, bWidth, bHeight, '');  
}
function openWindow2(title, url, bWidth, bHeight, frameType)
{
	//國昌20100715-mantis0016825
    var oriURL=url;
        
	var curPanel=null;
	var isAddNew=false;
	if((url.length>7) && (url.substring(0,7).toUpperCase()=='HTTP://')){
	}else if((url.length>8) && (url.substring(0,8).toUpperCase()=='HTTPS://')){
	}else if((url.length>0) && (url.substring(0,1)=='/')){
	}else{
	    url=urlHead+url;
	}

	if(!isNewWindow)
	{
		//find proper window
		for(var i=0;i<panelAry.length;i++){
		    if(panelAry[i].title==title){
		        curPanel=panelAry[i];
		        break;
		    }
		}
		if(curPanel==null){
		    for(var i=0;i<panelAry.length;i++){
			    if((panelAry[i].title!=systemMenuTitle) && (panelAry[i].title!=userMenuTitle)){
			        if(panelAry[i].dockside=='NONE'){
				        curPanel=panelAry[i];
				        break;
				    }
			    }
		    }
		}
	}else{

		for(var i=0;i<panelAry.length;i++){
		    if(panelAry[i].title==title){
		        curPanel=panelAry[i];
		        break;
		    }
		}
	}
	//國昌20100617:mantis0017614
	showWaitingIcon();
	
	if(curPanel==null){
		isAddNew=true;
		curPanel=new PanelNode();
		//open a new window
	
		//find max ID
		//find max zIndex
		maxID=-1;
		maxIndex=-1;
		for(var i=0;i<panelAry.length;i++){
			if(eval(panelAry[i].panelID)>maxID){
				maxID=panelAry[i].panelID;
			}
			if(panelAry[i].zIndex>maxIndex){
				maxIndex=panelAry[i].zIndex;
			}
		}
		maxID++;
		maxIndex++;

		//find proper postion and size
		var mLeft=0;
		for(var i=0;i<panelAry.length;i++){
			if(panelAry[i].dockside=='WEST'){
				if((eval(panelAry[i].left)+eval(panelAry[i].width))>mLeft){
					mLeft=eval(panelAry[i].left)+eval(panelAry[i].width);
				}
			}
		}
		
	
		var mTop=0;
		for(var i=0;i<panelAry.length;i++){
			if(panelAry[i].dockside=='NORTH'){
				if((eval(panelAry[i].top)+eval(panelAry[i].height))>mTop){
					mTop=eval(panelAry[i].top)+eval(panelAry[i].height);
				}
			}
		}
		curPanel.panelID=maxID;
		curPanel.zIndex=maxIndex;
		curPanel.frameType=frameType;
		curPanel.top=mTop;
		curPanel.left=mLeft;
		curPanel.gWidth=bWidth;
		curPanel.gHeight=bHeight;
		curPanel.width=curPanel.gWidth;
		curPanel.height=curPanel.gHeight;
		curPanel.preWidth=curPanel.width;
		curPanel.preHeight=curPanel.height;

		if(isNewWindowSave){
		    curPanel.isSave=true;
		}else{
		    curPanel.isSave=false;
		}
		if(isNewWindowDock){
		    curPanel.dockable=true;
		}else{
		    curPanel.dockable=false;
		}
		curPanel.newPanel();
	}
    curPanel.frameType=frameType;
    
	if((url.length>7) && (url.substring(0,7).toUpperCase()=='HTTP://')){
	    if(isAuthorizationMode=='Y'){
            if(url.indexOf('?')!=-1){
                url+="&processID="+curProcessID;
            }else{
                url+="?processID="+curProcessID;
            }
	    }
	}else if((url.length>8) && (url.substring(0,8).toUpperCase()=='HTTPS://')){
	    if(isAuthorizationMode=='Y'){
            if(url.indexOf('?')!=-1){
                url+="&processID="+curProcessID;
            }else{
                url+="?processID="+curProcessID;
            }
        }
	}else{
        if(url.indexOf('?')!=-1){
            url+="&CurPanelID="+curPanel.panelID;
        }else{
            url+="?CurPanelID="+curPanel.panelID;
        }
    }
	curPanel.url=url;
	curPanel.title=title;
	
	//國昌20100715-mantis0016825
    var postData="";
    postData="Method=GetProgramID&SV="+escape(oriURL);
	var xmlhttp=null;
    //FireFox修正
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'getMainFrameJS.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);
    curPanel.programID=xmlhttp.responseText;
    
	if(isAddNew){
		panelAry.push(curPanel);
	}

    try{
	curPanel.navigate();
	}catch(e){
	    alert(e);
	}
	curPanel.drawPanel();
	setZIndex(curPanel.panelID);
	return curPanel.panelID;
}


function PanelNode()
{
	this.top=0;
	this.left=0;
	this.width=400;
	this.height=300;
	this.gWidth=300;
	this.gHeight=200;
	
	this.preWidth=0;
	this.preHeight=0;
	
	this.dockable=false;
	this.dockside="NONE";
	this.state="NORMAL";
	this.isSave=false;
	this.isFix=false;
	this.title="<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string003", "未命名標題")%>";
	this.url="";
	this.zIndex=-1;
	this.panelID="0";
	this.frameType="";
	
	//國昌20100715-mantis0016825
	this.programID="";
}
PanelNode.prototype.drawPanel=function(){

  //這裡要畫方塊
  obj=document.getElementById('WindowPanel_Div_'+this.panelID);
  obj.style.top=getBasePoint()+this.top+'px';
  obj.style.left=this.left+'px';
  obj.style.height=this.height+'px';
  obj.style.width=this.width+'px';

  obj=document.getElementById('WindowPanel_Table_'+this.panelID);
  obj.style.height=this.height+'px';
  obj.style.width=this.width+'px';
  
  obj=document.getElementById('WindowPanel_Content_'+this.panelID);
  //Firefox排版關係，需改為24px，height不能為負數，因此做判斷
  if (this.height > 24)
    obj.style.height=(this.height-24)+'px';
  else
    obj.style.height='0px';

  obj=document.getElementById('WindowPanel_Title_'+this.panelID);
  obj.style.width=this.width-82+'px';
  obj.childNodes[0].style.width=this.width-82+'px';
  setInnerText(obj.childNodes[0],this.title);
  
  //國昌20100715-mantis0016825
  obj.childNodes[0].title=this.programID;

  obj=document.getElementById('PanelMask_'+this.panelID);
  obj.style.top='20px';
  obj.style.left='0px';
  obj.style.height=(this.height-20)+'px';
  obj.style.width=this.width+'px';
  
  obj=document.getElementById('WindowPanel_State_'+this.panelID);
	if(this.state=='NORMAL'){
		obj.src='Images/Min.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string004", "最小化")%>';
		
	}else{
		obj.src='Images/Max.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string005", "還原")%>';
	}
  obj=document.getElementById('WindowPanel_Dock_'+this.panelID);
	if(this.dockable){
		obj.src='Images/dock.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string006", "目前可停泊邊界")%>';
		
	}else{
		obj.src='Images/dock2.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string007", "目前不可停泊邊界")%>';
	}
  obj=document.getElementById('WindowPanel_Save_'+this.panelID);
	if(this.isSave){
		obj.src='Images/save.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string008", "目前可儲存視窗狀態")%>';
		
	}else{
		obj.src='Images/save2.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string009", "目前不可儲存視窗狀態")%>';
	}
    //記得要畫fix的扭
  obj=document.getElementById('WindowPanel_Fix_'+this.panelID);
	if(this.isFix){
		obj.src='Images/nail_lock.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string010", "取消鎖定")%>';
		
	}else{
		obj.src='Images/nail_free.gif';
		obj.title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string011", "鎖定位置")%>';
	}
   obj=document.getElementById('WindowPanel_IFrame_'+this.panelID);
   //非IE瀏覽器 iframe背景預設透明
   obj.style.backgroundColor = '#FFFFFF';
   if(this.frameType==''){
        //obj.frameType='scrolling:auto';
	if((this.url.length>7) && (this.url.substring(0,7).toUpperCase()=='HTTP://')){
	}else if((this.url.length>8) && (this.url.substring(0,8).toUpperCase()=='HTTPS://')){
	}else if((this.url.length>0) && (this.url.substring(0,3)=='../')){
	}else{
        try{
        	obj.scrolling='auto';
        }catch(e){}
	}
   }else{
        //obj.frameType='scrolling:no';
        try{
            obj.scrolling='auto';
        }catch(e){}
   }
   
}
PanelNode.prototype.setPreValue=function(){
	this.preHeight=this.height;
	this.preWidth=this.width;
}
PanelNode.prototype.navigate=function(){
  obj=document.getElementById('WindowPanel_IFrame_'+this.panelID);
   obj.contentWindow.location.href= this.url;  
}
PanelNode.prototype.newPanel=function(){
	var strs="";

    var dblclickevent="";
    //IE使用ondblclick事件處理最大化，其餘於Panel_Title_MouseDown處理
	var isIE=<%=com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page).Equals(com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE).ToString().ToLower() %>;
    if (isIE)
    {   dblclickevent=" ondblclick='getAvailabeMaxSize("+this.panelID+")' ";}
	
	//strs+="<div id='WindowPanel_Div_"+this.panelID+"' style='position:absolute;top:0px;left:0px;width:450px;height:300px;z-index:0;'>";
	strs+="<iframe frameborder=0 style='top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1;'></iframe>";
	strs+="<table class='WindowContentBorder' id='WindowPanel_Table_"+this.panelID+"' border=0 width='450px' height='300px' cellspacing='0' cellpadding='0'>";
	strs+="<tr height=20px class='WindowTitlePanel'>";
	strs+="	<td width=16px align=center valign=center><"+"img title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string011", "鎖定位置")%>' id='WindowPanel_Fix_"+this.panelID+"' src='Images/nail_free.gif' border=0 style='cursor:pointer;' onclick='Panel_ToggleFix("+this.panelID+")'></td>";
	//國昌20100715-mantis0016825	
	strs+="	<td width=368px id='WindowPanel_Title_"+this.panelID+"' onmousedown='Panel_Title_MouseDown("+this.panelID+")'"+dblclickevent+"><div style='overflow:hidden;white-space:nowrap;' title='"+this.programID+"'>"+this.title+"</div></td>";
	strs+="	<td width=16px align=center valign=center><"+"img title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string008", "目前可儲存視窗狀態")%>' id='WindowPanel_Save_"+this.panelID+"' src='Images/save.gif' border=0 style='cursor:pointer' onclick='Panel_ToggleSave("+this.panelID+")'></td>";
	strs+="	<td width=16px align=center valign=center><"+"img title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string006", "目前可停泊邊界")%>' id='WindowPanel_Dock_"+this.panelID+"' src='Images/dock.gif' border=0 style='cursor:pointer' onclick='Panel_ToggleDock("+this.panelID+")'></td>";
	strs+="	<td width=16px align=center valign=center><"+"img title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string004", "最小化")%>' id='WindowPanel_State_"+this.panelID+"' src='Images/min.gif' border=0 style='cursor:pointer' onclick='Panel_ToggleSize("+this.panelID+")'></td>";
	strs+="	<td width=16px align=center valign=center><"+"img title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string012", "關閉此視窗")%>' id='WindowPanel_Close_"+this.panelID+"' src='Images/cancel.gif' border=0 style='cursor:pointer' onclick='Panel_Close("+this.panelID+")'></td>";
	strs+="	<td width=2px align=center valign=center rowspan=2 style='cursor:E-resize' onmousedown='Panel_WEResize("+this.panelID+")' onlosecapture='Window_Content_MouseEnd();'>&nbsp;</td>";
	strs+="</tr>";
	strs+="<tr height=278px id='WindowPanel_Content_"+this.panelID+"'>";
	strs+="	<td class='WindowContentPanel' colspan=6>";
    //國昌20100617:mantis0017614 新增onload event
	
    if (isIE)//20120814 Eric Hsu : if ecp panel open asp page including frameset tag, error will happen
    {
        strs+="		<iframe onload='hideWaitingIcon();' id='WindowPanel_IFrame_"+this.panelID+"'  frameborder=0 width=100% height=100% style='scrollbar-face-color:white;scrollbar-highlight-color:#CCCCFF;overflow:scrolling;'  src='"+this.url+"'></iframe>";    
    }
    else
    {
        strs+="		<iframe onload='hideWaitingIcon();' id='WindowPanel_IFrame_"+this.panelID+"'  frameborder=0 width=100% height=100% style='scrollbar-face-color:white;scrollbar-highlight-color:#CCCCFF;overflow:auto;'  src='"+this.url+"'></iframe>";            
    }
	strs+="	</td>";
	//strs+="	<td width=2px align=center valign=center></td>";
	strs+="</tr>";
	strs+="<tr height=2px>";
	strs+="	<td colspan=6 style='cursor:S-resize' onmousedown='Panel_NSResize("+this.panelID+")'></td>";
	strs+="	<td id='WindowPanel_Resize_"+this.panelID+"' class='WindowResizeCell' onmousedown='Panel_Resize("+this.panelID+")' style='cursor:SE-resize'></td>";
	strs+="</tr>";
	strs+="</table>";
	strs+="<div id='PanelMask_"+this.panelID+"' style='background-color:#D3D3D3;position:absolute;z-index:-1000;opacity:0;filter:progid:DXImageTransform.Microsoft.Alpha(opacity=0)';'></div>";
	//strs+="</div>";

	obj=document.createElement("div");
	obj.id='WindowPanel_Div_'+this.panelID;
	obj.style.position='absolute';
    obj.style.overflow='hidden';
	obj.innerHTML=strs;
	document.getElementById('WindowContent').appendChild(obj);
}


function Window_Resize()
{
	totalHeight=truncatepx(document.body.clientHeight);
	cHeight=totalHeight-topBannerHeight;
	cWidth=truncatepx(document.body.clientWidth);
	document.getElementById('WindowContent').style.width=cWidth+'px';
    document.getElementById('BannerArea').style.width=cWidth+'px';
    document.getElementById('WindowToolBar').style.width=cWidth+'px';
    if(cHeight>=0)
    {
        document.getElementById('WindowContent').style.height=cHeight+'px';
    }	
	refreshPanel();
}
function getPanelNode(ind)
{
    for(var i=0;i<panelAry.length;i++)
    {
	    if(panelAry[i].panelID==ind)
	    {
		    return panelAry[i];
	    }
    }
}
function getPanelWindowObject(ind)
{
    objs=document.getElementById('WindowPanel_IFrame_'+ind);    	    
    var retWindow;
    if (objs.window) 
    {
        retWindow = objs.window;
    }
    else 
    {
        retWindow = objs.contentWindow;
    }	
    return retWindow;
}
function getPanelNodeIndex(ind)
{
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].panelID==ind){
			return i;
		}
	}
}
function Panel_ToggleSize(ind)
{
	var curPanel=getPanelNode(ind);
	if(menuFix){
	    if((curPanel.title==systemMenuTitle) || (curPanel.title==userMenuTitle)){
	        return;
	    }
	}
	if(curPanel.state=='NORMAL'){
		//縮小
		curPanel.state='MINIMIZE';
		curPanel.height=22;
	}else{
		//放到正常大小
		curPanel.state='NORMAL';
		curPanel.height=eval(curPanel.preHeight);
	}
	curPanel.drawPanel();
}
function Panel_ToggleSave(ind)
{
	var curPanel=getPanelNode(ind);
	if(curPanel.isSave){
		//改不可儲存
		curPanel.isSave=false;
	}else{
		//改可以儲存
		curPanel.isSave=true;
	}
	curPanel.drawPanel();
}
function Panel_ToggleDock(ind)
{
	var curPanel=getPanelNode(ind);
	if(curPanel.dockable){
		//改不可停泊
		curPanel.dockable=false;
	}else{
		//改可以停泊
		curPanel.dockable=true;
	}
	curPanel.drawPanel();
}
function Panel_ToggleFix(ind)
{
	var curPanel=getPanelNode(ind);
	if(curPanel.isFix){
		//改浮動
		curPanel.isFix=false;
	}else{
		//改可以停泊
		curPanel.isFix=true;
	}
	curPanel.drawPanel();
}
function Panel_Close_Silence(ind)
{
		var curPanelIndex=getPanelNodeIndex(ind);
		panelAry.splice(curPanelIndex,1);
		curobj=document.getElementById('WindowPanel_Div_'+ind);
		obj=document.getElementById('WindowContent');
		obj.removeChild(curobj);
		document.getElementById('focusBug').focus();
}
function Panel_Close(ind)
{
    hideWaitingIcon();
	var curPanelx=getPanelNode(ind);
	document.getElementById('focusBug').focus();
	if(menuFix){
	    if((curPanelx.title==systemMenuTitle) || (curPanelx.title==userMenuTitle)){
	        return;
	    }
	}
	if(confirm('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string013", "你確定要關閉此視窗嗎?")%>')){
	    //20090911 Modified By Eric : Recheck Engine to close
	    try{	        
		    var pageUniqueID= document.getElementById('WindowPanel_IFrame_'+ind).getPuid();		    
		    //document.getElementById('WindowPanel_IFrame_'+ind).kickAllXmlhttp();
		    closeEngine(pageUniqueID);		    
		}		
		catch(e){
		    //alert('terrors:'+e.message);
		}			
		var curPanelIndex=getPanelNodeIndex(ind);
		panelAry.splice(curPanelIndex, 1);
		curobj=document.getElementById('WindowPanel_Div_'+ind);
		obj=document.getElementById('WindowContent');		
		obj.removeChild(curobj);		    
	}
}
function Panel_ShowMask()
{
    for(var i=0;i<panelAry.length;i++){
        obj=document.getElementById('PanelMask_'+panelAry[i].panelID);
        obj.style.zIndex=1000;
    }
}
function Panel_CloseMask()
{
    for(var i=0;i<panelAry.length;i++){
        obj=document.getElementById('PanelMask_'+panelAry[i].panelID);
        obj.style.zIndex=-1000;
    }
}
function Panel_WEResize(ind)
{
    var evt = getEvent();
	var curPanel=getPanelNode(ind);
	if(menuFix){
	    if((curPanel.title==systemMenuTitle) || (curPanel.title==userMenuTitle)){
	        return;
	    }
	}
	if(!curPanel.isFix){
	    if(curPanel.state='NORMAL'){
		    panelWEResizeIndex=ind;
	        Panel_ShowMask();
	        cancelBubble(evt)
	        //國昌:20100318 MoveFrame
	        document.getElementById('AllMaskPanel').style.display='block';
	        showMoveFrame(curPanel.left+'px', curPanel.top+'px', curPanel.width+'px', curPanel.height+'px');
	    }
	}
	setZIndex(ind);
}
function Panel_NSResize(ind)
{
    var evt = getEvent();
	var curPanel=getPanelNode(ind);
	if(menuFix){
	    if((curPanel.title==systemMenuTitle) || (curPanel.title==userMenuTitle)){
	        return;
	    }
	}
	if(!curPanel.isFix){
	    if(curPanel.state='NORMAL'){
		    panelNSResizeIndex=ind;
	        Panel_ShowMask();
	        cancelBubble(evt)
	        //國昌:20100318 MoveFrame
	        document.getElementById('AllMaskPanel').style.display='block';
	        showMoveFrame(curPanel.left+'px', curPanel.top+'px', curPanel.width+'px', curPanel.height+'px');
	    }
	}
	setZIndex(ind);
}
function Panel_Resize(ind)
{
    var evt = getEvent();
	var curPanel=getPanelNode(ind);
	if(menuFix){
	    if((curPanel.title==systemMenuTitle) || (curPanel.title==userMenuTitle)){
	        return;
	    }
	}
	if(!curPanel.isFix){
	    if(curPanel.state='NORMAL'){
		    panelResizeIndex=ind;
	        Panel_ShowMask();
	        cancelBubble(evt)
	        //國昌:20100318 MoveFrame
	        document.getElementById('AllMaskPanel').style.display='block';
	        showMoveFrame(curPanel.left+'px', curPanel.top+'px', curPanel.width+'px', curPanel.height+'px');
	    }
	}
	setZIndex(ind);
}
var count = 0;
function Panel_Title_MouseDown(ind)
{
    //檢查雙點區 for Chrome Safari Opera
    var nowTimeFlag = (new Date()).getTime();
    if (nowTimeFlag-checkDblClickFlag <= 800)
    {
        getAvailabeMaxSize(ind);
        checkDblClickFlag = 0;
    }
    else
    {
        if (!<%=com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page).Equals(com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE).ToString().ToLower() %>)
        {checkDblClickFlag = nowTimeFlag;}

        var evt=getEvent();
	    curPanel=getPanelNode(ind);
	    if(menuFix){
	        if((curPanel.title==systemMenuTitle) || (curPanel.title==userMenuTitle)){
	            return;
	        }
	    }
	
        if(!curPanel.isFix){
	        mousex=evt.clientX;
	        mousey=evt.clientY;
    	
    	
	        panelDownIndex=ind;
	        panelDownStartx=mousex;
	        panelDownStarty=mousey;
	    
	        Panel_ShowMask();
	        cancelBubble(evt)

	        //國昌:20100318 MoveFrame
	        document.getElementById('AllMaskPanel').style.display='block';
	        showMoveFrame(curPanel.left+'px', curPanel.top+'px', curPanel.width+'px', curPanel.height+'px');
	    }
	    setZIndex(ind);
    }
}
function showMoveFrame(left, top, width, height)
{
    document.getElementById('MoveFrame').style.left=left;
    document.getElementById('MoveFrame').style.top=document.getElementById('WindowContent').offsetTop+parseInt(top.substring(0, top.length-2),0)+'px';
    document.getElementById('MoveFrame').style.width=width;
    document.getElementById('MoveFrame').style.height=height;
    if(document.getElementById('MoveFrame').style.display=='none'){
        document.getElementById('MoveFrame').style.display='block';
    }
}
function moveMoveFrame(x, y, w, h)
{
    obj=document.getElementById('MoveFrame');
    obj.style.left=parseInt(obj.style.left.substring(0, obj.style.left.length-2),0)+x+'px';
    obj.style.top=parseInt(obj.style.top.substring(0, obj.style.top.length-2),0)+y+'px';
    obj.style.width=parseInt(obj.style.width.substring(0, obj.style.width.length-2),0)+w+'px';
    obj.style.height=parseInt(obj.style.height.substring(0, obj.style.height.length-2),0)+h+'px';
}
function setZIndex(ind)
{
	//move current panel to top
	curobj=document.getElementById('WindowPanel_Div_'+ind);
	curZIndex=curobj.style.zIndex;
	curPanel=getPanelNode(ind);
	
	var maxZIndex=-1000;
	for(var i=0;i<panelAry.length;i++){
		obj=document.getElementById('WindowPanel_Div_'+panelAry[i].panelID);
		if(obj != curobj && obj.style.zIndex>maxZIndex){
			maxZIndex=obj.style.zIndex;
		}
	}
	if(maxZIndex==-1000){
	    maxZIndex=0;
	}else{
	    maxZIndex++;
	}
	curobj.style.zIndex=maxZIndex;
	curPanel.zIndex=maxZIndex;
}
function Window_Content_MouseMove()
{
    var evt = getEvent();
	if(panelDownIndex!=-1)
	{
        //event.srcElement.setCapture();
        try
        {  
		mousex=evt.clientX;
		mousey=evt.clientY;

		offsetx=mousex-panelDownStartx;
		offsety=mousey-panelDownStarty;

		window.stats=offsetx;

		curobj=document.getElementById('WindowPanel_Div_'+panelDownIndex);
      
		curPanel=getPanelNode(panelDownIndex);
		
	    //國昌:20100318 MoveFrame
		//curPanel.top+=offsety;
		//curobj.style.top=curPanel.top+'px';
		//curPanel.left+=offsetx;
		//curobj.style.left=curPanel.left+'px';

		moveMoveFrame(offsetx, offsety, 0, 0);
		
		panelDownStartx=mousex;
		panelDownStarty=mousey;
		}
		catch(e)
		{
		    Window_Content_MouseEnd();
		}

	}else if(panelResizeIndex!=-1){
		mousex=evt.clientX;
		mousey=evt.clientY;
		
		curPanel=getPanelNode(panelResizeIndex);
		if(((mousex-curPanel.left)<100) || ((mousey-curPanel.top)<50))
		{
		}else{
		    
	        //國昌:20100318 MoveFrame
	        var obj=document.getElementById('MoveFrame');
	        moveMoveFrame(0,0,mousex-curPanel.left-parseInt(obj.style.width.substring(0, obj.style.width.length-2),0), mousey-document.getElementById('WindowContent').offsetTop-curPanel.top-parseInt(obj.style.height.substring(0, obj.style.height.length-2),0));
	        cancelBubble(evt)
			//curPanel.width=mousex-curPanel.left;
			//curPanel.height=mousey-curPanel.top;
			//curPanel.setPreValue();
			//curPanel.drawPanel();
		}
		
	}else if(panelWEResizeIndex!=-1){
		mousex=evt.clientX;
		mousey=evt.clientY;
		
		curPanel=getPanelNode(panelWEResizeIndex);
		if(((mousex-curPanel.left)<100) || ((mousey-curPanel.top)<50))
		{
		}else{
		
	        //國昌:20100318 MoveFrame
	        var obj=document.getElementById('MoveFrame');
	        moveMoveFrame(0,0,mousex-curPanel.left-parseInt(obj.style.width.substring(0, obj.style.width.length-2),0), 0);
	        cancelBubble(evt)

			//curPanel.width=mousex-curPanel.left;
			//curPanel.setPreValue();
			//curPanel.drawPanel();
		}
	}else if(panelNSResizeIndex!=-1){
		mousex=evt.clientX;
		mousey=evt.clientY;
		
		curPanel=getPanelNode(panelNSResizeIndex);
		if(((mousex-curPanel.left)<100) || ((mousey-curPanel.top)<50))
		{
		}else{
		
	        //國昌:20100318 MoveFrame
	        var obj=document.getElementById('MoveFrame');
	        moveMoveFrame(0,0,0, mousey-document.getElementById('WindowContent').offsetTop-curPanel.top-parseInt(obj.style.height.substring(0, obj.style.height.length-2),0));
	        cancelBubble(evt)
			//curPanel.height=mousey-curPanel.top;
			//curPanel.setPreValue();
			//curPanel.drawPanel();
		}
		
	}
}

function Window_Content_MouseEnd()
{
	if(panelDownIndex!=-1)
	{	
        var evt = getEvent();
		curobj=document.getElementById('WindowPanel_Div_'+panelDownIndex);
		curPanel=getPanelNode(panelDownIndex);
				
	    //國昌:20100318 MoveFrame
		var obj=document.getElementById('MoveFrame');
		curPanel.top=parseInt(obj.style.top.substring(0, obj.style.top.length-2),0)-document.getElementById('WindowContent').offsetTop;
		if(curPanel.top<0){
		    curPanel.top=0;
		}
		curobj.style.top=getBasePoint()+curPanel.top+'px';
		curPanel.left=parseInt(obj.style.left.substring(0, obj.style.left.length-2),0);
		curobj.style.left=curPanel.left+'px';

		if(isSnapToGrid)
		{
		    var intX=Math.floor(curPanel.left / SnapGridSize);
		    var resX=curPanel.left % SnapGridSize;
		    var intY=Math.floor(curPanel.top / SnapGridSize);
		    var resY=curPanel.top % SnapGridSize;
		    if(resX>(SnapGridSize/2)){
		        curPanel.left=(intX+1)*SnapGridSize;
		    }else{
		        curPanel.left=intX*SnapGridSize;
		    }
		    if(resY>(SnapGridSize/2)){
		        curPanel.top=(intY+1)*SnapGridSize;
		    }else{
		        curPanel.top=intY*SnapGridSize;
		    }
		    //window.status=curPanel.left;
		}
		curobj.style.top=getBasePoint()+curPanel.top+'px';
		curobj.style.left=curPanel.left+'px';
				
		panelDownStartx=-1;
		panelDownStarty=-1;
		panelDownIndex=-1;
		
		Panel_CloseMask();
		//judge if dockable
		judgeDockable(curPanel, evt.clientX, evt.clientY-getBoundaryTop());
		//getSRCElement(evt).setCapture();
		//getSRCElement(evt).releaseCapture();
	}else if(panelResizeIndex!=-1){
		curobj=document.getElementById('WindowPanel_Div_'+panelResizeIndex);
		curPanel=getPanelNode(panelResizeIndex);
		
	    //國昌:20100318 MoveFrame
		var obj=document.getElementById('MoveFrame');
        curPanel.width=parseInt(obj.style.width.substring(0, obj.style.width.length-2),0);
        curPanel.height=parseInt(obj.style.height.substring(0, obj.style.height.length-2),0);
        
		if(isSnapToGrid)
		{
		    var intX=Math.floor(curPanel.width / SnapGridSize);
		    var resX=curPanel.width % SnapGridSize;
		    var intY=Math.floor(curPanel.height / SnapGridSize);
		    var resY=curPanel.height % SnapGridSize;
		    if(resX>(SnapGridSize/2)){
		        curPanel.width=(intX+1)*SnapGridSize;
		    }else{
		        curPanel.width=intX*SnapGridSize;
		    }
		    if(resY>(SnapGridSize/2)){
		        curPanel.height=(intY+1)*SnapGridSize;
		    }else{
		        curPanel.height=intY*SnapGridSize;
		    }
		    //window.status=curPanel.width;
		}
		curobj.style.height=curPanel.height+'px';
		curobj.style.width=curPanel.width+'px';
	    curPanel.drawPanel();
	    
		Panel_CloseMask();
		panelResizeIndex=-1;
	}else if(panelWEResizeIndex!=-1){
		curobj=document.getElementById('WindowPanel_Div_'+panelWEResizeIndex);
		curPanel=getPanelNode(panelWEResizeIndex);
		
		//國昌:20100318 MoveFrame
		var obj=document.getElementById('MoveFrame');
        curPanel.width=parseInt(obj.style.width.substring(0, obj.style.width.length-2),0);
	
		if(isSnapToGrid)
		{
		    var intX=Math.floor(curPanel.width / SnapGridSize);
		    var resX=curPanel.width % SnapGridSize;
		    if(resX>(SnapGridSize/2)){
		        curPanel.width=(intX+1)*SnapGridSize;
		    }else{
		        curPanel.width=intX*SnapGridSize;
		    }
		}
		curobj.style.width=curPanel.width+'px';
	    curPanel.drawPanel();
	    
		Panel_CloseMask();
		panelWEResizeIndex=-1;
	}else if(panelNSResizeIndex!=-1){
		curobj=document.getElementById('WindowPanel_Div_'+panelNSResizeIndex);
		curPanel=getPanelNode(panelNSResizeIndex);
		
	    //國昌:20100318 MoveFrame
		var obj=document.getElementById('MoveFrame');
        curPanel.height=parseInt(obj.style.height.substring(0, obj.style.height.length-2),0);

		if(isSnapToGrid)
		{
		    var intY=Math.floor(curPanel.height / SnapGridSize);
		    var resY=curPanel.height % SnapGridSize;
		    if(resY>(SnapGridSize/2)){
		        curPanel.height=(intY+1)*SnapGridSize;
		    }else{
		        curPanel.height=intY*SnapGridSize;
		    }
		    //window.status=curPanel.width;
		}
		curobj.style.height=curPanel.height+'px';
	    curPanel.drawPanel();
	    
		Panel_CloseMask();
		panelNSResizeIndex=-1;
	}
	//國昌:20100318 MoveFrame
	document.getElementById('MoveFrame').style.display='none';
	document.getElementById('AllMaskPanel').style.display='none';
}

function getAvailabeMaxSize(ind)
{
	curobj=document.getElementById('WindowPanel_Div_'+ind);
	curPanel=getPanelNode(ind);
		
	curPanel.dockside='NONE';
	
	var mLeft=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='WEST'){
			if((eval(panelAry[i].left)+eval(panelAry[i].width))>mLeft){
				mLeft=eval(panelAry[i].left)+eval(panelAry[i].width);
			}
		}
	}
	
	var mRight=getBoundaryWidth();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='EAST'){
			if(panelAry[i].left<mRight){
				mRight=panelAry[i].left;
			}
		}
	}
	
	var mTop=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='NORTH'){
			if((eval(panelAry[i].top)+eval(panelAry[i].height))>mTop){
				mTop=eval(panelAry[i].top)+eval(panelAry[i].height);
			}
		}
	}
	
	var mBottom=getBoundaryHeight();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='SOUTH'){
			if(panelAry[i].top<mBottom){
				mBottom=panelAry[i].top;
			}
		}
	}
	curPanel.state='NORMAL';
	
	curPanel.top=mTop;
	curPanel.left=mLeft;
	curPanel.width=mRight-mLeft;
	curPanel.height=mBottom-mTop;
	curPanel.setPreValue();
	
	curPanel.drawPanel();
}

function judgeDockable(curPanel, x, y)
{
	if((curPanel.dockable) && (curPanel.state='NORMAL'))
	{
		//dockable
		var offs=10;
		docks="NONE";
		if(x<offs){
			docks="WEST";
		}else if(x>(getBoundaryWidth()-offs)){
			docks="EAST";
		}else if(y<offs){
			docks="NORTH";
		}else if(y>(getBoundaryHeight()-offs)){
			docks="SOUTH";
		}
		curPanel.dockside=docks;
		if(docks!='NONE'){
			refreshPanel();
		}
	}
}

function refreshPanel()
{
	//start from west
	var offxs=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockable){
			if(panelAry[i].dockside=='WEST'){
				panelAry[i].left=offxs;
				panelAry[i].top=0;
				panelAry[i].height=getBoundaryHeight();
				panelAry[i].width=panelAry[i].gWidth;
				offxs+=panelAry[i].width;
				panelAry[i].setPreValue();
			}
		}
	}
	
	//process East
	var offxe=getBoundaryWidth();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockable){
			if(panelAry[i].dockside=='EAST'){
				panelAry[i].width=panelAry[i].gWidth;
				panelAry[i].left=offxe-panelAry[i].width;
				panelAry[i].top=0;
				panelAry[i].height=getBoundaryHeight();
				offxe=panelAry[i].left;
				panelAry[i].setPreValue();
			}
		}
	}
	
	//process North
	var offys=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockable){
			if(panelAry[i].dockside=='NORTH'){
				panelAry[i].left=offxs;
				panelAry[i].top=offys;
				panelAry[i].width=offxe-offxs;
				panelAry[i].height=panelAry[i].gHeight;
				offys+=panelAry[i].height;
				panelAry[i].setPreValue();
			}
		}
	}

	//process South
	var offye=getBoundaryHeight();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockable){
			if(panelAry[i].dockside=='SOUTH'){
				panelAry[i].left=offxs;
				panelAry[i].height=panelAry[i].gHeight;
				panelAry[i].top=offye-panelAry[i].height;
				panelAry[i].width=offxe-offxs;
				offye=panelAry[i].top;
				panelAry[i].setPreValue();
			}
		}
	}
	
	resizePanel();
}
function resizePanel()
{
	//resize all panels
	for(var i=0;i<panelAry.length;i++){
		if (i!=0){
       	        panelAry[i].height=getBoundaryHeight();
		} 
		panelAry[i].drawPanel();
	}
}

function getBoundaryWidth()
{
	return truncatepx(document.getElementById('WindowContent').style.width);
}
function getBoundaryHeight()
{
	return truncatepx(document.getElementById('WindowContent').style.height);
}
function getBoundaryTop()
{
	return topBannerHeight;
}

function truncatepx(str)
{
	str+='';
	retv=str.replace(/px/g, '');
	return retv
}
//------------------------------底下為選單功能-----------------------
var isOnMenu=false;

function closeAllMenu()
{
    if(!isOnMenu){
        menuids=new Array();
        menuids.push("Menu_Layout");
        menuids.push("Menu_SystemMenu");
        menuids.push("Menu_Display");
        menuids.push("AllWindowPanel");
        
        for(var i=0;i<menuids.length;i++){
            var obj=document.getElementById(menuids[i]);
            obj.style.display='none';
        }
    }
    isOnMenu=false;
}
function showMainframeMenu(menuid)
{
    var evt = getEvent();
    isOnMenu=false;
    var obj=document.getElementById(menuid);
    if(obj.style.display=='none')
    {    
        closeAllMenu();
        obj.style.display='block';
        obj.style.left=getSRCElement(evt).offsetLeft;
        obj.style.top=topBannerHeight;
        isOnMenu=true;
    }else{
        closeAllMenu();
        isOnMenu=false;
    }
    cancelBubble(evt)
}
function showMainframeMenuF(menuid)
{
    var evt = getEvent();
    if(isOnMenu){
        var obj=document.getElementById(menuid);
        if(obj.style.display=='none')
        {    
            isOnMenu=false;
            closeAllMenu();
            isOnMenu=true;
            obj.style.display='block';
            obj.style.left=getSRCElement(evt).offsetLeft;
            obj.style.top=topBannerHeight;
        }
        isOnMenu=true;
    }
}
function openCustomURL()
{
    closeAllMenu();
    var url=(prompt('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string014", "請輸入網址?")%>',''));
    if(url!=null){
        openWindow(url, url,300,200,'',true);
    }
}
function showSystemMenu()
{
    closeAllMenu();
    var hasFound=false;
    for(var i=0;i<panelAry.length;i++){
        if(panelAry[i].title=='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string001", "系統選單")%>'){
            hasFound=true;
            break;
        }
    }
    if(!hasFound){
        oldIsNewWindow=isNewWindow;
        isNewWindow=true;
        openWindow('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string001", "系統選單")%>','ToolBar/Outlook.aspx?ShowSys=1',165,300,'scrolling=no',false);
        isNewWindow=oldIsNewWindow;
    }
}
function showUserMenu()
{
    closeAllMenu();
    var hasFound=false;
    for(var i=0;i<panelAry.length;i++){
        if(panelAry[i].title=='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string002", "個人選單")%>'){
            hasFound=true;
            break;
        }
    }
    if(!hasFound){
        oldIsNewWindow=isNewWindow;
        isNewWindow=true;
        openWindow('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string002", "個人選單")%>','ToolBar/Outlook.aspx?ShowSys=0',165,300,'scrolling=no',false);
        isNewWindow=oldIsNewWindow;
    }
}
function setUserMenu()
{
    closeAllMenu();
    oldIsNewWindow=isNewWindow;
    isNewWindow=true;
    openWindow('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string015", "個人選單設定")%>','Program/System/Maintain/SMVA/Maintain.aspx?ShowSys=0',165,300,'scrolling=no',true);
    isNewWindow=oldIsNewWindow;
}
function showNodeValue(data)
{

    var strary=data.split(lsplitter);
    Panel_CloseAll();

    for(var i=0;i<strary.length-5;i++){
            var line=strary[i].split(splitter);
            curPanel=new PanelNode();
		    curPanel.panelID=line[15];
		    curPanel.frameType=line[16];
		    curPanel.zIndex=eval(line[14]);
		    curPanel.top=eval(line[0]);
		    curPanel.left=eval(line[1]);
		    curPanel.gWidth=eval(line[4]);
		    curPanel.gHeight=eval(line[5]);
		    curPanel.width=eval(line[2]);
		    curPanel.height=eval(line[3]);
		    curPanel.preWidth=eval(line[6]);
		    curPanel.preHeight=eval(line[7]);
		    if(line[8]=='1'){
		        curPanel.dockable=true;
		    }else{
		        curPanel.dockable=false;
		    }
		    curPanel.dockside=line[9];
		    curPanel.isSave=true;
		    curPanel.state=line[10];
		    if(line[11]=='1'){
		        curPanel.isFix=true;
		    }else{
		        curPanel.isFix=false;
		    }
	        curPanel.title=line[12];
	       <!--20090527 Modify Eric : PanelID-->
	       if((line[13].length>7) && (line[13].substring(0,7).toUpperCase()=='HTTP://')){
	        }else if((line[13].length>8) && (line[13].substring(0,8).toUpperCase()=='HTTPS://')){
	        }else{
                if(line[13].indexOf('?')!=-1){
                    line[13]+="&CurPanelID="+curPanel.panelID;
                }else{
                    line[13]+="?CurPanelID="+curPanel.panelID;
                }
            }
	        curPanel.url=line[13];    	    
	        panelAry.push(curPanel);
	        curPanel.newPanel();
	        curPanel.drawPanel();
	        curPanel.navigate();
    }
    if(strary[strary.length-5]=='1'){
        document.getElementById('User_New_Window').checked=true;
    }else{
        document.getElementById('User_New_Window').checked=false;
    }
    if(strary[strary.length-4]=='1'){
        document.getElementById('Snap_To_Grid').checked=true;
    }else{
        document.getElementById('Snap_To_Grid').checked=false;
    }
    document.getElementById('SnapGridSize').value=strary[strary.length-3];
    if(strary[strary.length-2]=='1'){
        document.getElementById('New_Window_Save').checked=true;
    }else{
        document.getElementById('New_Window_Save').checked=false;
    }
    if(strary[strary.length-1]=='1'){
        document.getElementById('New_Window_Dock').checked=true;
    }else{
        document.getElementById('New_Window_Dock').checked=false;
    }
    
    UserNewWindow_Click();
    SnapToGrid_Click();
    if(isNaN(document.getElementById('SnapGridSize').value)){
        document.getElementById('SnapGridSize').value='0';
    }
    SnapGridSize=document.getElementById('SnapGridSize').value;
    NewWindowSave_Click();
    NewWindowDock_Click();
    
    refreshPanel();
}
function showSystemNode()
{
    closeAllMenu();
    var postData="USERID=";
	var xmlhttp=null;
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'showDefaultSetting.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    data=xmlhttp.responseText;
    
    showNodeValue(data);
}
function showPanelNode()
{
    closeAllMenu();
    var postData="USERID=";
	var xmlhttp=null;
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'showUserSetting.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    data=xmlhttp.responseText;

    showNodeValue(data);
}
function Panel_VerticalArrange()
{
	//重新排視窗
	//邏輯: 已經Dock的不排, 非Dock但是state=MINIMIZE的不排
	//依照可以排的視窗的zIndex排列
	
	closeAllMenu();
	//先取得起始位置
	var mLeft=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='WEST'){
			if((eval(panelAry[i].left)+eval(panelAry[i].width))>mLeft){
				mLeft=eval(panelAry[i].left)+eval(panelAry[i].width);
			}
		}
	}
		
	var mTop=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='NORTH'){
			if((eval(panelAry[i].top)+eval(panelAry[i].height))>mTop){
				mTop=eval(panelAry[i].top)+eval(panelAry[i].height);
			}
		}
	}

	var mRight=getBoundaryWidth();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='EAST'){
			if(panelAry[i].left<mRight){
				mRight=panelAry[i].left;
			}
		}
	}
	
	var mBottom=getBoundaryHeight();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='SOUTH'){
			if(panelAry[i].top<mBottom){
				mBottom=panelAry[i].top;
			}
		}
	}
	//計算符合條件的元素個數
	var matchCount=0;
	for(var i=0;i<panelAry.length;i++){
		if((panelAry[i].dockside=='NONE') && (panelAry[i].state='NORMAL')){
			matchCount++;
		}
    }
    if(matchCount==0) return;
    
    //計算垂直高度寬度
    var newHeight=Math.floor((mBottom-mTop)/matchCount);
    var newWidth=mRight-mLeft;                	
	
	//根據angAry陣列開始重新調整每個元素的top & left
	for(var i=0;i<panelAry.length;i++){
		if((panelAry[i].dockside=='NONE') && (panelAry[i].state='NORMAL')){
		    var curPanel=panelAry[i];		
		    curPanel.top=mTop;
		    curPanel.left=mLeft;
		    curPanel.height=newHeight;
		    curPanel.width=newWidth;

		    curPanel.drawPanel();
		    mTop+=newHeight;
		}
	}
}
function Panel_HorizontalArrange()
{
	//重新排視窗
	//邏輯: 已經Dock的不排, 非Dock但是state=MINIMIZE的不排
	//依照可以排的視窗的zIndex排列
	
	closeAllMenu();
	//先取得起始位置
	var mLeft=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='WEST'){
			if((eval(panelAry[i].left)+eval(panelAry[i].width))>mLeft){
				mLeft=eval(panelAry[i].left)+eval(panelAry[i].width);
			}
		}
	}
		
	var mTop=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='NORTH'){
			if((eval(panelAry[i].top)+eval(panelAry[i].height))>mTop){
				mTop=eval(panelAry[i].top)+eval(panelAry[i].height);
			}
		}
	}

	var mRight=getBoundaryWidth();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='EAST'){
			if(panelAry[i].left<mRight){
				mRight=panelAry[i].left;
			}
		}
	}
	
	var mBottom=getBoundaryHeight();
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='SOUTH'){
			if(panelAry[i].top<mBottom){
				mBottom=panelAry[i].top;
			}
		}
	}
	//計算符合條件的元素個數
	var matchCount=0;
	for(var i=0;i<panelAry.length;i++){
		if((panelAry[i].dockside=='NONE') && (panelAry[i].state='NORMAL')){
			matchCount++;
		}
    }
    if(matchCount==0) return;
    
    //計算垂直高度寬度
    var newHeight=mBottom-mTop;
    var newWidth=Math.floor((mRight-mLeft)/matchCount);           	
	
	//根據angAry陣列開始重新調整每個元素的top & left
	for(var i=0;i<panelAry.length;i++){
		if((panelAry[i].dockside=='NONE') && (panelAry[i].state='NORMAL')){
		    var curPanel=panelAry[i];		
		    curPanel.top=mTop;
		    curPanel.left=mLeft;
		    curPanel.height=newHeight;
		    curPanel.width=newWidth;

		    curPanel.drawPanel();
		    mLeft+=newWidth;
		}
	}
}
function Panel_Rearrange()
{
	//重新排視窗
	//邏輯: 已經Dock的不排, 非Dock但是state=MINIMIZE的不排
	//依照可以排的視窗的zIndex排列
	//畫面大小依照目前大小不調整, 若破表也無所謂
	
	closeAllMenu();
	//先取得起始位置
	var mLeft=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='WEST'){
			if((eval(panelAry[i].left)+eval(panelAry[i].width))>mLeft){
				mLeft=eval(panelAry[i].left)+eval(panelAry[i].width);
			}
		}
	}
		
	var mTop=0;
	for(var i=0;i<panelAry.length;i++){
		if(panelAry[i].dockside=='NORTH'){
			if((eval(panelAry[i].top)+eval(panelAry[i].height))>mTop){
				mTop=eval(panelAry[i].top)+eval(panelAry[i].height);
			}
		}
	}

	//挑選符合條件的元素並且依照大小填入angAry
	var angAry=new Array();
	for(var i=0;i<panelAry.length;i++){
		if((panelAry[i].dockside=='NONE') && (panelAry[i].state='NORMAL')){
			if(angAry.length==0){
				angAry.push(panelAry[i]);
			}else{
				//根據每一個元素判斷zIndex大小
				var cindx=panelAry[i].zIndex;
				var hasInsert=false;
				for(var j=0;j<angAry.length;j++){
					if(cindx<angAry[j].zIndex){
						angAry.splice(j,0,panelAry[i]);
						hasInsert=true;
						break;
					}
				}
				if(!hasInsert){
					angAry.push(panelAry[i]);
				}
			}
		}
	}
	
	//根據angAry陣列開始重新調整每個元素的top & left
	for(var i=0;i<angAry.length;i++){
		var curPanel=angAry[i];
		curPanel.top=mTop;
		curPanel.left=mLeft;
		obj=document.getElementById('WindowPanel_Div_'+curPanel.panelID);
		obj.style.top=getBasePoint()+mTop+'px';
		obj.style.left=mLeft+'px';
		mTop+=25;
		mLeft+=25;
	}
}
function UserNewWindow_Click()
{
	//closeAllMenu();
    if(document.getElementById('User_New_Window').checked){
        isNewWindow=true;
    }else{
        isNewWindow=false;
    }
}
function SnapToGrid_Click()
{
	//closeAllMenu();
    if(document.getElementById('Snap_To_Grid').checked){
        if (document.getElementById('SnapGridSize').value=='0'){
            document.getElementById('Snap_To_Grid').checked = false;
            alert('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string025", "貼齊線格不可設定為0")%>'); 
        }   
        isSnapToGrid=true;
    }else{
        isSnapToGrid=false;
    }
}
function NewWindowSave_Click()
{
	//closeAllMenu();
    if(document.getElementById('New_Window_Save').checked){
        isNewWindowSave=true;
    }else{
        isNewWindowSave=false;
    }
}
function NewWindowDock_Click()
{
	//closeAllMenu();
    if(document.getElementById('New_Window_Dock').checked){
        isNewWindowDock=true;
    }else{
        isNewWindowDock=false;
    }
}

function disableEvent()
{
    isOnMenu=true;
}
function enableEvent()
{
    if(isNaN(document.getElementById('SnapGridSize').value)){
        document.getElementById('SnapGridSize').value='0';
    }
    SnapGridSize=document.getElementById('SnapGridSize').value;
    isOnMenu=false;
}
function splitCurPanelID(strURL)
{
    var returnURL=strURL.substring(0,strURL.indexOf("?"));
    if(strURL.indexOf("CurPanelID")>-1){
        var strTemp=strURL.substr(strURL.indexOf("?"));
        strTemp=strTemp.split("&");
        for(var i=0;i<strTemp.length;i++){
            if(strTemp[i].indexOf("CurPanelID")==-1){
                if(i==0){
                    returnURL=returnURL+strTemp[i];}
                else {
                    returnURL=returnURL+"&"+strTemp[i];}
            }
        }
    }
    return returnURL;
}

function getSaveNode()
{
	var savestr="";
	for(var i=0;i<panelAry.length;i++){
		//只有可以儲存者才儲存
		if(panelAry[i].isSave){
			savestr+=panelAry[i].top+splitter;
			savestr+=panelAry[i].left+splitter;
			savestr+=panelAry[i].width+splitter;
			savestr+=panelAry[i].height+splitter;
			savestr+=panelAry[i].gWidth+splitter;
			savestr+=panelAry[i].gHeight+splitter;
			savestr+=panelAry[i].preWidth+splitter;
			savestr+=panelAry[i].preHeight+splitter;
			if(panelAry[i].dockable){
				savestr+='1'+splitter;
			}else{
				savestr+='0'+splitter;
			}
			savestr+=panelAry[i].dockside+splitter;
			savestr+=panelAry[i].state+splitter;
			if(panelAry[i].isFix){
				savestr+='1'+splitter;
			}else{
				savestr+='0'+splitter;
			}
			savestr+=panelAry[i].title+splitter;
			savestr+=splitCurPanelID(panelAry[i].url)+splitter;
			savestr+=panelAry[i].zIndex+splitter;
			savestr+=panelAry[i].panelID+splitter;
			savestr+=panelAry[i].frameType;
			savestr+=lsplitter;
		}
	}

	//儲存是否開新視窗設定
	if(isNewWindow){
		savestr+="1";
	}else{
		savestr+="0";
	}
	//儲存是否貼齊設定
	if(isSnapToGrid){
		savestr+=lsplitter+"1";
	}else{
		savestr+=lsplitter+"0";
	}
    //儲存貼齊大小
    savestr+=lsplitter+SnapGridSize;
 	//儲存是否儲存
	if(isNewWindowSave){
		savestr+=lsplitter+"1";
	}else{
		savestr+=lsplitter+"0";
	}
   	//儲存是否停泊
	if(isNewWindowDock){
		savestr+=lsplitter+"1";
	}else{
		savestr+=lsplitter+"0";
	}

	return savestr;
}
function saveSystemNode()
{
    if(!confirm('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string016", "你確定要儲存版面設定嗎？系統僅會儲存該視窗狀態允許儲存者")%>')){
        return;
    }
	closeAllMenu();

    var savestr=getSaveNode();
    
    var postData="DATA="+escape(savestr);
	var xmlhttp=null;
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'saveDefaultSetting.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    alert(xmlhttp.responseText);
}

function savePanelNode()
{
    if(!confirm('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string016", "你確定要儲存版面設定嗎？系統僅會儲存該視窗狀態允許儲存者")%>')){
        return;
    }
	closeAllMenu();

    var savestr=getSaveNode();

    var postData="DATA="+escape(savestr);
	var xmlhttp=null;
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'saveUserSetting.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    alert(xmlhttp.responseText);
}
function Panel_CloseAll()
{
    for(var i=0;i<panelAry.length;i++){
		var curPanel=panelAry[i];
		curobj=document.getElementById('WindowPanel_Div_'+curPanel.panelID);
		obj=document.getElementById('WindowContent');
		obj.removeChild(curobj);
	}
	panelAry=new Array();
}
function Panel_CloseNoneDock()
{
    tempAry=new Array();
    for(var i=0;i<panelAry.length;i++){
		var curPanel=panelAry[i];
		if(curPanel.dockside=='NONE'){
		    curobj=document.getElementById('WindowPanel_Div_'+curPanel.panelID);
		    obj=document.getElementById('WindowContent');
		    obj.removeChild(curobj);
		}else{
		    tempAry.push(curPanel);
		}
	}
	panelAry=tempAry;
}
function Panel_MinimizeNoneDock()
{
    for(var i=0;i<panelAry.length;i++){
        if((panelAry[i].dockside=='NONE') && (panelAry[i].state=='NORMAL')){
            panelAry[i].state='MINIMIZE';
            panelAry[i].height=22;
            panelAry[i].drawPanel();
        }
    }
}
function showAllWindowMenu()
{
    var evt = getEvent();
    isOnMenu=false;
    obj=document.getElementById('AllWindowPanel');
    if(obj.style.display=='none')
    {   
        tableObj=document.getElementById('AllWindowPanel_Table');
        while(tableObj.rows.length>0){
            tableObj.deleteRow(0);
        }

        if(panelAry.length>0){
            for(var i=0;i<panelAry.length;i++){
                rowObj=tableObj.insertRow(-1);
                cellObj=rowObj.insertCell(-1);
                cellObj.className='MenuHead';
                cellObj.width='15px';
                cellObj=rowObj.insertCell(-1);
                cellObj.className='MenuContent';
                cellObj.innerHTML="<a href='#' onclick='setZIndex("+panelAry[i].panelID+");closeAllMenu();'>"+panelAry[i].title+"</a>";
            }
        }else{
            rowObj=tableObj.insertRow(-1);
            cellObj=rowObj.insertCell(-1);
            cellObj.className='MenuHead';
            cellObj.width='15px';
            cellObj=rowObj.insertCell(-1);
            cellObj.className='MenuContent';
            cellObj.innerHTML="<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string017", "無任何開啟面版")%>";
       }
        closeAllMenu();
        obj.style.display='block';
        obj.style.left=getSRCElement(evt).offsetLeft;
        obj.style.top=topBannerHeight;
        isOnMenu=true;
    }else{
        closeAllMenu();
        isOnMenu=false;
    }
    cancelBubble(evt)
}
function showAllWindowMenuF()
{
    var evt = getEvent();
    if(isOnMenu){
        obj=document.getElementById('AllWindowPanel');
        if(obj.style.display=='none')
        {   
            tableObj=document.getElementById('AllWindowPanel_Table');
            while(tableObj.rows.length>0){
                tableObj.deleteRow(0);
            }

            if(panelAry.length>0){
                for(var i=0;i<panelAry.length;i++){
                    rowObj=tableObj.insertRow(-1);
                    cellObj=rowObj.insertCell(-1);
                    cellObj.className='MenuHead';
                    cellObj.width='15px';
                    cellObj=rowObj.insertCell(-1);
                    cellObj.className='MenuContent';
                    cellObj.innerHTML="<a href='#' onclick='setZIndex("+panelAry[i].panelID+");closeAllMenu();'>"+panelAry[i].title+"</a>";
                }
            }else{
                rowObj=tableObj.insertRow(-1);
                cellObj=rowObj.insertCell(-1);
                cellObj.className='MenuHead';
                cellObj.width='15px';
                cellObj=rowObj.insertCell(-1);
                cellObj.className='MenuContent';
                cellObj.innerHTML="<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string017", "無任何開啟面版")%>";
           }
           isOnMenu=false;
            closeAllMenu();
            isOnMenu=true;
            obj.style.display='block';
            obj.style.left=getSRCElement(evt).offsetLeft;
            obj.style.top=topBannerHeight;
        }
        isOnMenu=true;
    }
}
function ToggleBanner()
{
    closeAllMenu();
    if(document.getElementById('BannerArea').style.display!='none'){
        document.getElementById('BannerArea').style.display='none';
        topBannerHeight=toolBarHeight;
        if(document.getElementById('ToggleBannerGraph')!=null)
            document.getElementById('ToggleBannerGraph').src='Images/MenuItem.gif';
    }else{
        document.getElementById('BannerArea').style.display='block';
        topBannerHeight=oriTopBannerHeight;
        if(document.getElementById('ToggleBannerGraph')!=null)
            document.getElementById('ToggleBannerGraph').src='Images/MenuItemUp.gif';
    }
    Window_Resize();
}

var waitLoop=0;
var loopStep=10;
var startx;
var starty;
var endy;
var isEnd;
var loopHandle;
var alertGUID;
var alertType;
function showAlert(alerttype, guid, messageType, alertTime, title, content, url)
{
    alertGUID=guid;
    alertType=alerttype;
    
    startx=truncatepx(document.body.clientWidth)-truncatepx(document.getElementById('SystemAlert').style.width);
    starty=truncatepx(document.body.clientHeight);
    endy=starty-truncatepx(document.getElementById('SystemAlert').style.height);

    var mType;
    if(messageType=='0'){
        mType='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string018", "一般訊息")%>';
    }else if(messageType=='1'){
        mType='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string019", "重要訊息")%>';
    }else{
        mType='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string020", "行事曆通知")%>';
    }
    document.getElementById('AlertTitle').contentText=' '+mType;
    var str="<font color=red><b>&nbsp;"+title+"</b></font><br>";
    str+='&nbsp;'+content.substring(0,10)+"...";
    document.getElementById('AlertContent').innerHTML=str;
    
    waitLoop=0;
    isEnd=false;
    loopHandle=window.setInterval('moveSystemAlert()', 100);
}
function moveSystemAlert()
{
    if(!isEnd){
        starty-=loopStep;
        if(starty<endy){
            starty=endy;
            isEnd=true;
        }
        document.getElementById('SystemAlert').style.left=startx+'px';
        document.getElementById('SystemAlert').style.top=starty+'px';
        document.getElementById('SystemAlert').style.display='block';
    }else{
        waitLoop++;
        if(waitLoop==100){
            document.getElementById('SystemAlert').style.display='none';
            window.clearInterval(loopHandle);
            loopHandle=null;
        }
    }
}
function showAlertMessage()
{
    openWindowGeneral("<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string021", "系統訊息檢視")%>", "MainFrame/SystemMessage.aspx?alertType="+alertType+"&GUID="+alertGUID, 350, 200, "", false, true);
    document.getElementById('SystemAlert').style.display='none';
    window.clearInterval(loopHandle);
    loopHandle=null;
    
}
function closeAlert()
{
    document.getElementById('SystemAlert').style.display='none';
    window.clearInterval(loopHandle);
    loopHandle=null;
}
function clickUser()
{
    openWindowGeneral("<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string022", "線上使用者")%>","MainFrame/showLogonUser.aspx",700, 450, "", false, true);
    //window.showModalDialog("ShowLogonUser.aspx",'',"dialogHeight:350px;dialogWidth:550px");
}
function logout()
{
    if(confirm('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string023", "你確定要登出嗎？")%>')){
        pressQuit=1;
		 window.location.href = "../Default.aspx";
    }
}
function SysInfo()
{
    openWindowGeneral("<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("getMainFrameJS.aspx.language.ini", "global", "string024", "系統資訊")%>","MainFrame/SysInfo.aspx",600, 400, "", false, true);
}
function downloadActiveX()
{
    window.open("../ActiveX.aspx");
}

//國昌20100617:mantis0017614	
function showWaitingIcon(){
    if(document.getElementById('WaitingPanel')!=null) return;
	wpanel=document.createElement('div');
	wpanel.id='WaitingPanel';
	wpanel.style.display='none';
	wpanel.style.zIndex=99999;
	wpanel.style.position='absolute';
	wpanel.style.top=0;
	wpanel.style.left=0;
	wpanel.style.width=250;
	wpanel.style.height=40;

	fpanel=document.createElement('iframe');
	fpanel.frameBorder=0;
	fpanel.scrolling='no';
	fpanel.style.zIndex=100000;
	fpanel.style.position='relative';
	fpanel.style.top=0;
	fpanel.style.left=0;
	fpanel.style.width=250;
	fpanel.style.height=44;
	fpanel.src='../DSCWebControlRunTime/WaitingPanel.aspx';
	wpanel.appendChild(fpanel);
	document.body.appendChild(wpanel);
	cw=document.body.clientWidth/2-125;
	ch=document.body.clientHeight/2-22;
	try{
	    document.getElementById('WaitingPanel').style.left=cw;
	    document.getElementById('WaitingPanel').style.top=ch;
	    document.getElementById('WaitingPanel').style.display='inline';
	}catch(e){};
}
function hideWaitingIcon(){
    try{
	    document.getElementById('WaitingPanel').style.display='none';
	    document.body.removeChild(document.getElementById('WaitingPanel'));
	}catch(e){};
}
