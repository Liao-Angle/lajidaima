<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Outlook.aspx.cs" Inherits="ToolBar_Outlook" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="Outlook.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
<script language=javascript>
var lsplitter='#!#!#';
var splitter='#*#*#';

var toolbarHeight=27;

var toolBarAry=new Array();

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
        

function ToolBarContainer()
{
    this.GUID='';
    this.image='';
    this.title='';
    this.isActive=false;
}

function ResetSize(){
    document.getElementById("OutlookPanel").width=document.body.clientWidth;
    document.getElementById("OutlookPanel").height=document.body.clientHeight;
    if(document.getElementById("ToolBarTreeCol")!=null)
    {
        var tmpHeight=(document.body.clientHeight-22-toolbarHeight*(toolBarAry.length+1));
       if(tmpHeight>=0)
       {
            document.getElementById("ToolBarTreeCol").style.height=tmpHeight+'px';
        }
        document.getElementById("ToolBarTree").style.width=window.frameElement.clientWidth+'px';
    }
}
function initToolBar(searchValue)
{
    //以下這一段應該去伺服器讀出來
    var postData="";
    if(searchValue==''){
        postData="Method=GetToolBar";
    }else{
        postData="Method=GetSearchBar";
    }
	var xmlhttp=null;
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'Outlook.aspx'+window.location.search , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    var toolbars=xmlhttp.responseText;
    toolBarAry=new Array();
    var tary=toolbars.split(lsplitter);

    //設定Toolbar資料
    if(toolbars.length==0) return;
    var top=0;
    var hasActive=false;
    for(var i=0;i<tary.length;i++){
        var dts=tary[i].split(splitter);
        toolc=new ToolBarContainer();
        toolc.GUID=dts[0];
        toolc.image=dts[2];
        toolc.title=dts[1];
        if(dts[3]=='Y'){
            if(hasActive==false){
                toolc.isActive=true;
                hasActive=true;
            }else{
                toolc.isActive=false;
            }
        }else{
            toolc.isActive=false;
        }
        toolBarAry.push(toolc);
    }
    if(hasActive==false){
        toolBarAry[0].isActive=true;
    }
    
    //根據Toolbar數量畫上Table
    var strs="";
    strs+="<table border=0 width=100% height=100% cellspacing=0 cellpadding=0>";
    if(toolBarAry.length>0){
        strs+="<tr height=22px>";
        strs+="<td id=SearchContainer class=ToolBarContainer valign=bottom><%if(isShowSearch.Equals("Y")){ %><%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Outlook.aspx.language.ini", "global", "string001", "搜尋")%>: <input type=text align=center style='font-size:10pt;width:72px;border-style:solid;border-width:1px' id='keywd'><img src='Images/Search.gif' align=top style='cursor:pointer;' title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Outlook.aspx.language.ini", "global", "string001", "搜尋")%>' onclick='searchToolBar()'><%} %><img src='Images/Refresh.gif' align=top style='cursor:pointer;' title='<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Outlook.aspx.language.ini", "global", "string002", "重新整理")%>' onclick='initToolBar(\"\")'></td>";
        strs+="</tr>";
        
        strs+="<tr height="+toolbarHeight+"px>";
        strs+="<td id=ActiveContainer class=ToolBarContainer></td>";
        strs+="</tr>";
        
        strs+="<tr height=100%>";
        //國昌2009/10/07 0013298
        var colheight=(document.body.clientHeight-22-toolbarHeight*(toolBarAry.length+1));
        strs+="<td id='ToolBarTreeCol' style='height:"+colheight+"px;'><div id=ToolBarTree class=ToolBarTree style='width:"+(document.body.offsetWidth)+"px;height:100%;overflow:auto'></div></td>";
        strs+="</tr>";
        
        for(var i=1;i<toolBarAry.length;i++){
            strs+="<tr height="+toolbarHeight+"px>";
            strs+="<td class=ToolBarContainer id='TC_"+i+"'></td>";
            strs+="</tr>";            
        }
    }
    strs+="</table>";
    document.getElementById("OutlookPanel").innerHTML=strs;
    drawBarData(searchValue);
}
//此方法會畫ActiveBar且重新讀取ActiveBar的Tree
var xmlTree="";
var spid=0;
function drawBarData(searchData)
{
    var actBar=null;
    //先畫Active
    for(var i=0;i<toolBarAry.length;i++){
        if(toolBarAry[i].isActive){
            if(toolBarAry[i].image.length>0){
                document.getElementById("ActiveContainer").innerHTML="<img src='"+toolBarAry[i].image+"'>"+toolBarAry[i].title;
            }else{
                document.getElementById("ActiveContainer").innerHTML=toolBarAry[i].title;
            }
            actBar=toolBarAry[i];
            break;
        }
    }
    
    //取得ActiveBar的Tree
    var postData="";
    if(searchData==''){
        postData="Method=GetToolTree&GUID="+actBar.GUID;
    }else{
        postData="Method=GetToolSearchTree&SV="+escape(searchData);
    }
	var xmlhttp=null;
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'Outlook.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    var tooltree=xmlhttp.responseText;
    //由tooltree畫tree
    var xmlDoc=createXMLDOM(tooltree);
    var x=xmlDoc.documentElement;
    xmlTree="";
    spid=0;
    drawTreeRec(x, 0);
	obj=document.createElement("span");
	obj.innerHTML=xmlTree;

	document.getElementById("ToolBarTree").innerHTML="";
    document.getElementById("ToolBarTree").appendChild(obj);

    //document.getElementById("ToolBarTree").style.width=document.getElementById("ToolBarTree").offsetWidth;
    //畫其他Bar
    var curid=1;
    for(var i=0;i<toolBarAry.length;i++){
        if(!toolBarAry[i].isActive){
            curobj=document.getElementById('TC_'+curid);
            var tempstr="";

            tempstr="<span style='cursor:pointer;' onclick='showToolBar(\""+toolBarAry[i].GUID+"\");'>";
            if(toolBarAry[i].image.length>0){
                tempstr+="<img src='"+toolBarAry[i].image+"'>";
            }
            tempstr+=toolBarAry[i].title+"</span>";

            curobj.innerHTML=tempstr;
            curid++;
        }
    }

}
function drawTreeRec(node, level)
{
    for(var x=0;x<node.childNodes.length;x++){
        xmlTree+="<div>"
        var isDisplays=false;
        if(node.childNodes[x].nodeName=='Folder'){
            if(parseInt(node.childNodes[x].getAttribute("SUM"))>0){
                for(var i=0;i<level;i++){
                    xmlTree+="　";
                }
                if((node.childNodes[x].childNodes!=null) && (node.childNodes[x].childNodes.length>0)){
                    if(parseInt(node.childNodes[x].getAttribute("FOPEN"))>0){
                        isDisplays=true;
                        xmlTree+="<img src='Images/o.gif' style='cursor:pointer;' onclick='ToggleTree("+spid+")'>";
                    }else{
                        xmlTree+="<img src='Images/c.gif' style='cursor:pointer;' onclick='ToggleTree("+spid+")'>";
                    }
                }else{
                    xmlTree+="<img src='Images/e.gif'>";
                }
                xmlTree+="<span style='cursor:pointer;' onclick='ToggleTree("+spid+")'>"+node.childNodes[x].getAttribute("title")+"</span>";
            }
        }else{
            //xmlTree+="<div title='"+node.childNodes[x].getAttribute("explain")+"' onclick='clickItem(\""+node.childNodes[x].getAttribute("title")+"\",\""+node.childNodes[x].getAttribute("url")+"\",\""+node.childNodes[x].getAttribute("width")+"\",\""+node.childNodes[x].getAttribute("height")+"\",\""+node.childNodes[x].getAttribute("frameType")+"\",\""+node.childNodes[x].getAttribute("isMax")+"\")' style='cursor:pointer;white-space:nowrap;'>";
            //for(var i=0;i<level;i++){
            //    xmlTree+="　";
            //}
            //xmlTree+="<img src='Images/item.gif' border=0> ";
            //xmlTree+=node.childNodes[x].getAttribute("title");
            //xmlTree+="</div>";
			
			//CL_Chang
			var title = node.childNodes[x].getAttribute("title");
            var inbox = "收件資料匣";
            var inboxEf = "收件資料匣(EF)";
            var itemId = "";

            if(title.indexOf(inboxEf) >= 0) {
                title = title.substr(0, inboxEf.length);
                itemId = "inboxEf";
            } else if (title.indexOf(inbox) >= 0) {
                title = title.substr(0, inbox.length);
                itemId = "inbox";
            }

            xmlTree+="<div id='"+itemId+"' title='"+node.childNodes[x].getAttribute("explain")+"' onclick='clickItem(\""+title+"\",\""+node.childNodes[x].getAttribute("url")+"\",\""+node.childNodes[x].getAttribute("width")+"\",\""+node.childNodes[x].getAttribute("height")+"\",\""+node.childNodes[x].getAttribute("frameType")+"\",\""+node.childNodes[x].getAttribute("isMax")+"\")' style='cursor:pointer;white-space:nowrap;'>";
            for(var i=0;i<level;i++){
                xmlTree+="　";
            }
            xmlTree+="<img src='Images/item.gif' border=0> ";
            xmlTree+=node.childNodes[x].getAttribute("title");
            xmlTree+="</div>";
        }
        xmlTree+="</div>";

        if(isDisplays){
            xmlTree+="<div id='TREEBLOCK_"+spid+"' style='display:block'>";
        }else{
            xmlTree+="<div id='TREEBLOCK_"+spid+"' style='display:none'>";
        }
        
        spid++;
        
        if(parseInt(node.childNodes[x].getAttribute("SUM"))>0){
            drawTreeRec(node.childNodes[x], level+1);
        }
        
        xmlTree+="</div>";

    }
}
function searchToolBar()
{
    if(document.getElementById("keywd").value==''){
        alert('<%=com.dsc.locale.LocaleString.getMainFrameLocaleString("Outlook.aspx.language.ini", "global", "string003", "請填寫要搜尋的字串")%>');
        return;
    }else{
        initToolBar(document.getElementById("keywd").value);
    }
}
function showToolBar(guid)
{
    for(var i=0;i<toolBarAry.length;i++){
        if(toolBarAry[i].GUID!=guid){
            toolBarAry[i].isActive=false;
        }else{
            toolBarAry[i].isActive=true;
        }
    }
    
    drawBarData('');
}
function ToggleTree(id)
{
    var evt = getEvent();
    obj=document.getElementById('TREEBLOCK_'+id);    
    if(getSRCElement(evt).tagName=='IMG'){
        if(obj.style.display=='none'){
            getSRCElement(evt).src='Images/o.gif';
            obj.style.display='block';
        }else{
            getSRCElement(evt).src='Images/c.gif';
            obj.style.display='none';
        }
     }else{
        if(obj.style.display=='none'){
            getSRCElement(evt).previousSibling.src='Images/o.gif';
            obj.style.display='block';
        }else{
            getSRCElement(evt).previousSibling.src='Images/c.gif';
            obj.style.display='none';
        }
     }     
}
function clickItem(title, url, width, height, frameType, isMax)
{
    var ism=false;
    if(isMax=='1'){
        ism=true;
    }
    //top.window.openWindow(title, url, width, height, frameType, ism);
    if(frameType=='1'){
        parent.window.openWindowGeneral(title, url, width, height, '', ism, true);
    }else{
        var inbox = "收件資料匣";
        var inboxEf = "收件資料匣(EF)";
        if(title.indexOf(inboxEf) >= 0) {
            PageMethods.ProcessInboxEfCount(onSucessEf, onError); 
        } else if (title.indexOf(inbox) >= 0) {
            PageMethods.ProcessInboxCount(onSucess, onError); 
        }
		parent.window.openWindow(title, url, width, height, '', ism);
    }
}

function refreshInbox() {
    PageMethods.ProcessInboxCount(onSucess, onError); 
}

function onSucessEf(result) {
    var html = document.getElementById('inboxEf').innerHTML;
    var idx = html.lastIndexOf("(");
    html = html.substr(0, idx);
    html = html + "("+result+")";
    document.getElementById('inboxEf').innerHTML = html;
}

function onSucess(result) {
    var html = document.getElementById('inbox').innerHTML;
    var idx = html.lastIndexOf("(");
    html = html.substr(0, idx);
    html = html + "("+result+")";
    document.getElementById('inbox').innerHTML = html;
}
            
function onError(error) {
    alert(error.get_message());
}

function truncatepx(str)
{
	str+='';
	retv=str.replace(/px/g, '');
	return retv
}
</script>    
<script language="javascript" src="../JS/ShareScript.js"></script>
</head>
<body onload='ResetSize();initToolBar("");' onresize='ResetSize()' style='margin: 0px;'>
    <form id="form1" runat="server">
    <div style=" ">
    <!--Outlook-->
    <div id='OutlookPanel'>
    </div>
    <!--//Outlook-->
    </div>
    <div><asp:ScriptManager ID="ScriptMgr" runat="server" EnablePageMethods="true"> </asp:ScriptManager>
        <asp:HiddenField ID="inboxCount" runat="server" />
    </div>
    </form>
</body>
</html>
