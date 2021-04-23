<%@ Page Language="C#" AutoEventWireup="true" Buffer="true"  CodeFile="Maintain.aspx.cs" Inherits="Program_System_Maintain_SMVA_Maintain" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系統選單設定作業</title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <link href="SMVA.css" rel="stylesheet" type="text/css" />
    <script src="../../../../JS/ShareScript.js" language="javascript" type="text/javascript"></script>
<script language=javascript>
var lsplitter='$!$!$';
var splitter='$*$*$';

//此方法會畫ActiveBar且重新讀取ActiveBar的Tree
var xmlTree="";
var spid=1;
var xmlData="";
function drawBarData()
{
    
    //取得ActiveBar的Tree
    var postData="Method=GetToolTree&"+window.location.search.substring(1, window.location.search.length);

	var xmlhttp=null;
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'Maintain.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    var tooltree=xmlhttp.responseText;
    xmlData=tooltree;
    
    drawTree();
        
    //取得所有程式清單
    postData="Method=GetProgramList&keyvalue=&"+window.location.search.substring(1, window.location.search.length);
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'Maintain.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    var programList=xmlhttp.responseText;

    //alert(xmlData);
    //return;
    
    //由tooltree畫tree
    var programTree="";
    var pAry=programList.split(lsplitter);
    for(var i=0;i<pAry.length;i++){
        var lAry=pAry[i].split(splitter);
        programTree+="<img src='item.gif' style='cursor:pointer;' ondragend='clearDrag()' ondragstart='drag_on_Item(\"ITEMSOURCE\",\""+lAry[0]+splitter+lAry[1]+"\");' GUID='"+lAry[0]+"' title='"+lAry[1]+"'> <span style='font-size:9pt;' >"+lAry[1]+"</span><br>";
    }
        
	obj=document.createElement("span");
	obj.innerHTML=programTree;
	document.getElementById("AllProgramList").innerHTML="";
    document.getElementById("AllProgramList").appendChild(obj);
    
    changeLanguage();
}
function searchProgramList()
{
    //取得所有程式清單
    postData="Method=GetProgramList&keyvalue="+document.getElementById("SearchValue").value+"&"+window.location.search.substring(1, window.location.search.length);
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'Maintain.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    var programList=xmlhttp.responseText;

    //alert(xmlData);
    //return;
    
    //由tooltree畫tree
    var programTree="";
    var pAry=programList.split(lsplitter);
    for(var i=0;i<pAry.length;i++){
        var lAry=pAry[i].split(splitter);
        programTree+="<img src='item.gif' style='cursor:pointer;' ondragend='clearDrag()' ondragstart='drag_on_Item(\"ITEMSOURCE\",\""+lAry[0]+splitter+lAry[1]+"\");' GUID='"+lAry[0]+"' title='"+lAry[1]+"'> <span style='font-size:9pt;' >"+lAry[1]+"</span><br>";
    }
        
	obj=document.createElement("span");
	obj.innerHTML=programTree;
	document.getElementById("AllProgramList").innerHTML="";
    document.getElementById("AllProgramList").appendChild(obj);
}
function searchAll()
{
    //取得所有程式清單
    postData="Method=GetProgramList&keyvalue=&"+window.location.search.substring(1, window.location.search.length);
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'Maintain.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    var programList=xmlhttp.responseText;

    //alert(xmlData);
    //return;
    
    //由tooltree畫tree
    var programTree="";
    var pAry=programList.split(lsplitter);
    for(var i=0;i<pAry.length;i++){
        var lAry=pAry[i].split(splitter);
        programTree+="<img src='item.gif' style='cursor:pointer;' ondragend='clearDrag()' ondragstart='drag_on_Item(\"ITEMSOURCE\",\""+lAry[0]+splitter+lAry[1]+"\");' GUID='"+lAry[0]+"' title='"+lAry[1]+"'> <span style='font-size:9pt;' >"+lAry[1]+"</span><br>";
    }
        
	obj=document.createElement("span");
	obj.innerHTML=programTree;
	document.getElementById("AllProgramList").innerHTML="";
    document.getElementById("AllProgramList").appendChild(obj);
}
function searchNew()
{
    //取得未加入選單程式清單
    postData="Method=GetNewProgramList&keyvalue=&"+window.location.search.substring(1, window.location.search.length);
    xmlhttp=createXMLHTTP();
    xmlhttp.open('POST', 'Maintain.aspx' , false);
    xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
    xmlhttp.send(postData);

    var programList=xmlhttp.responseText;

    //alert(xmlData);
    //return;
    
    //由tooltree畫tree
    var programTree="";
    var pAry=programList.split(lsplitter);
    for(var i=0;i<pAry.length;i++){
        var lAry=pAry[i].split(splitter);
        programTree+="<img src='item.gif' style='cursor:pointer;' ondragend='clearDrag()' ondragstart='drag_on_Item(\"ITEMSOURCE\",\""+lAry[0]+splitter+lAry[1]+"\");' GUID='"+lAry[0]+"' title='"+lAry[1]+"'> <span style='font-size:9pt;' >"+lAry[1]+"</span><br>";
    }
        
	obj=document.createElement("span");
	obj.innerHTML=programTree;
	document.getElementById("AllProgramList").innerHTML="";
    document.getElementById("AllProgramList").appendChild(obj);
}
function drawTree()
{
    //由tooltree畫tree
    var xmlDoc = createXMLDOM(xmlData);
    var x=xmlDoc.documentElement;

    xmlTree="";
    xmlTree += "<img src='o.gif' style='cursor:pointer;' onclick='ToggleTree(0)'><img src='folder.gif'  oncontextmenu='showRootMenu(\"*\");return false;'  > <span id='TREEHEAD_0'  GUID='*' style='cursor:pointer;' oncontextmenu='showRootMenu(\"*\");return false;'  ondrop='stopEvent(getEvent());drop_on_Folder();' ondragenter='stopEvent(getEvent());' ondragover='stopEvent(getEvent());'>系統選單</span><br>";
    xmlTree+="<div id='TREEBLOCK_0'>";
    
    spid=1;
    drawTreeRec(x, 1);
    
    xmlTree+="</div>";
    
	obj=document.createElement("span");
	obj.innerHTML=xmlTree;
	document.getElementById("ToolBarTree").innerHTML="";
    document.getElementById("ToolBarTree").appendChild(obj);
}
function drawTreeRec(node, level)
{
    for(var x=0;x<node.childNodes.length;x++){
        if(node.childNodes[x].nodeName=='Folder'){
            for(var i=0;i<level;i++){
                xmlTree+="　";
            }
            var fp='folder.gif';
            if(node.childNodes[x].getAttribute("FOPEN")=='N'){
                fp='folder.gif';
            }else{
                fp='open.gif';
            }
            xmlTree += "<img src='o.gif' style='cursor:pointer' onclick='ToggleTree(" + spid + ")'><img src='" + fp + "' style='cursor:pointer' oncontextmenu='showMenu(\"" + node.childNodes[x].getAttribute("GUID") + "\");return false;' ondragend='clearDrag()' ondragstart='drag_on_Folder(\"ITEMSOURCE\",\"" + node.childNodes[x].getAttribute("GUID") + "\");' > <span id='TREEHEAD_" + spid + "'  oncontextmenu='showMenu(\"" + node.childNodes[x].getAttribute("GUID") + "\");return false;' dropEffect='copy' ondrop='stopEvent(getEvent());drop_on_Folder();' ondragenter='stopEvent(getEvent());' ondragover='stopEvent(getEvent());' style='cursor:pointer'  GUID='" + node.childNodes[x].getAttribute("GUID") + "'>";
            xmlTree+=node.childNodes[x].getAttribute("title");
            xmlTree+="</span>";
            xmlTree+="<br>";

            xmlTree+="<div id='TREEBLOCK_"+spid+"'>";
            
            spid++;
            drawTreeRec(node.childNodes[x], level+1);
            
            xmlTree+="</div>";
        }
    }
    for(var x=0;x<node.childNodes.length;x++){
        if(node.childNodes[x].nodeName!='Folder'){
            for(var i=0;i<level;i++){
                xmlTree+="　";
            }
            xmlTree+="<img src='item.gif' style='cursor:pointer;' oncontextmenu='showFileMenu(\""+node.childNodes[x].getAttribute("GUID")+"\");return false;'  ondragend='clearDrag()' ondragstart='drag_on_Program(\"ITEMSOURCE\",\""+node.childNodes[x].getAttribute("GUID")+"\");' > <span id='TREEHEAD_"+spid+"'  oncontextmenu='showFileMenu(\""+node.childNodes[x].getAttribute("GUID")+"\");return false;'  style='cursor:pointer;'  GUID='"+node.childNodes[x].getAttribute("GUID")+"'>";
            xmlTree+=node.childNodes[x].getAttribute("title");
            xmlTree+="</span>";
            xmlTree+="<br>";

        }
    }
    
}
function ToggleTree(id)
{
    var evt = getEvent();
    obj=document.getElementById("TREEBLOCK_"+id);
    //obj.contentEditable=true;
    //obj.filters[0].Apply();
    if (getSRCElement(evt).tagName == 'IMG') {
        if(obj.style.display=='none'){
            getSRCElement(evt).src='o.gif';
            obj.style.display='block';
        }else{
            getSRCElement(evt).src = 'c.gif';
            obj.style.display='none';
        }
     }else{
        if(obj.style.display=='none'){
            getSRCElement(evt).previousSibling.src = 'o.gif';
            obj.style.display='block';
        }else{
            getSRCElement(evt).previousSibling.src = 'c.gif';
            obj.style.display='none';
        }
     }
     //obj.filters[0].Play();
     //obj.contentEditable=false;
}
var isDrag=0;
var cGUID="";

function drop_on_Folder() {
    var evt = getEvent();
    if (isDrag == 1) {
        var tag = evt.dataTransfer.getData('Text').split(splitter);
        var tarGUID = getSRCElement(evt).getAttribute("GUID");
        var srcGUID=tag[0];

        var xmlDoc = createXMLDOM(xmlData);
        var x=xmlDoc.documentElement;
        
        obj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+tarGUID+"']");
        //檢查有無重複項目
        var hasFound=false;
        for(var i=0;i<obj.childNodes.length;i++){
            var exTag=tarGUID+srcGUID;
            if(exTag==obj.childNodes[i].getAttribute("GUID")){
                hasFound=true;
                break;
            }
        }
        //if(!hasFound){
        if(true){
            var newNode=xmlDoc.createElement("Item");
            newNode.setAttribute("GUID",tarGUID+srcGUID);
            newNode.setAttribute("ItemGUID", srcGUID);
            newNode.setAttribute("title",tag[1]);
            obj.appendChild(newNode);
            
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }else{
            alert('<%=msg1%>');
        }
    }else if(isDrag==2){
        var xmlDoc = createXMLDOM(xmlData);
        var x=xmlDoc.documentElement;

        srcGUID = evt.dataTransfer.getData('Text');
        srcObj=selectSingleNodeFromDOM(x,"//Item[@GUID='"+srcGUID+"']");

        tarGUID = getSRCElement(evt).getAttribute("GUID");
        if(tarGUID!='*'){
            tarObj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+tarGUID+"']");
            
            pObj=srcObj.parentNode;
            pObj.removeChild(srcObj);
            
            tarObj.appendChild(srcObj);
            
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }
        
    }else if(isDrag==3){
        var xmlDoc = createXMLDOM(xmlData);
        var x=xmlDoc.documentElement;

        srcGUID = evt.dataTransfer.getData('Text');
        srcObj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+srcGUID+"']");
        
        
        pObj=srcObj.parentNode;
        pObj.removeChild(srcObj);

        tarGUID = getSRCElement(evt).getAttribute("GUID");
        var tarObj;
        if(tarGUID=='*'){
            tarObj=selectSingleNodeFromDOM(x,"//Root");
        }else{
            tarObj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+tarGUID+"']");
        }
        if(tarObj!=null){
            tarObj.appendChild(srcObj);
            
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }
    }
}

function drop_on_Trash()
{
    if((isDrag==2) || (isDrag==3)){
        if(confirm('<%=msg2%>')){
            var xmlDoc = createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
            
            var obj;
            if (isDrag == 3) {
                obj = selectSingleNodeFromDOM(x, "//Folder[@GUID='" + getEvent().dataTransfer.getData('Text') + "']");
            } else {
                obj = selectSingleNodeFromDOM(x, "//Item[@GUID='" + getEvent().dataTransfer.getData('Text') + "']");
            }
            pobj=obj.parentNode;

            pobj.removeChild(obj);
            xmlData = getFullXMLString(xmlDoc);
            drawTree();
        }
    }
           
}
function clearDrag()
{
    //isDrag=0;
}
function drag_on_Folder(tag, vle)
{
    isDrag=3;
    getEvent().dataTransfer.setData('Text', vle);
}
function drag_on_Program(tag, vle)
{
    isDrag=2;
    getEvent().dataTransfer.setData('Text',vle);
}
function drag_on_Item(tag, vle)
{
    isDrag=1;
    getEvent().dataTransfer.setData('Text', vle);
}
function truncatepx(str)
{
	str+='';
	retv=str.replace(/px/g, '');
	return retv
}
function showMenu(guid) {
    closeMenu();
    var evt = getEvent();
    document.getElementById("Menu_Layout").style.display='block';
    if (evt.pageY) {
        document.getElementById("Menu_Layout").style.top = evt.pageY;
        document.getElementById("Menu_Layout").style.left = evt.pageX;
    }
    else {
        document.getElementById("Menu_Layout").style.top = evt.clientY;
        document.getElementById("Menu_Layout").style.left = evt.clientX;
    }
    cGUID=guid;
    stopEvent(evt);
}
function showRootMenu(guid) {
    closeMenu();
    var evt = getEvent();
    document.getElementById("RootMenu").style.display = 'block';
    if (evt.pageY) {
        document.getElementById("RootMenu").style.top = evt.pageY;
        document.getElementById("RootMenu").style.left = evt.pageX;
    }
    else {
        document.getElementById("RootMenu").style.top = evt.clientY;
        document.getElementById("RootMenu").style.left = evt.clientX;
    }
    cGUID=guid;
    stopEvent(evt);
}
function showFileMenu(guid) {
    closeMenu();
    var evt = getEvent();
    document.getElementById("MenuFile").style.display = 'block';
    if (evt.pageY) {
        document.getElementById("MenuFile").style.top = evt.pageY;
        document.getElementById("MenuFile").style.left = evt.pageX;
    }
    else {
        document.getElementById("MenuFile").style.top = evt.clientY;
        document.getElementById("MenuFile").style.left = evt.clientX;
    }
    cGUID=guid;
    stopEvent(evt);
}

function closeMenu()
{
    document.getElementById("Menu_Layout").style.display='none';
    document.getElementById("RootMenu").style.display='none';
    document.getElementById("MenuFile").style.display='none';
    cGUID="";
}
function upFolder()
{
    if(cGUID!=''){
        document.getElementById("Menu_Layout").style.display='none';
        document.getElementById("RootMenu").style.display='none';
        document.getElementById("MenuFile").style.display='none';
        if(true){
            var xmlDoc=createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
            
            var obj;
            if(cGUID!='*'){
                obj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+cGUID+"']");
            }else{
                obj=selectSingleNodeFromDOM(x,"/Root");
            }
            var upobj=obj.previousSibling;
            var pobj=obj.parentNode;
            pobj.removeChild(obj);
            pobj.insertBefore(obj, upobj);

            xmlData = getFullXMLString(xmlDoc);
            drawTree();
        }
        
    }
}
function downFolder()
{
    if(cGUID!=''){
        document.getElementById("Menu_Layout").style.display='none';
        document.getElementById("RootMenu").style.display='none';
        document.getElementById("MenuFile").style.display='none';
        if(true){
            var xmlDoc = createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
            
            var obj;
            if(cGUID!='*'){
                obj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+cGUID+"']");
            }else{
                obj=selectSingleNodeFromDOM(x,"/Root");
            }
            var pobj=obj.parentNode;
            
            var upobj=obj.nextSibling;
            if(upobj==null){
                upobj=pobj.firstChild;
                pobj.removeChild(obj);
                pobj.insertBefore(obj, upobj);
            }else{
                upobj=upobj.nextSibling;
                pobj.removeChild(obj);
                pobj.insertBefore(obj, upobj);
            }
            xmlData = getFullXMLString(xmlDoc);
            drawTree();
        }
        
    }
}
function toggleFOpen()
{
    if(cGUID!=''){
        document.getElementById("Menu_Layout").style.display='none';
        document.getElementById("RootMenu").style.display='none';
        document.getElementById("MenuFile").style.display='none';
        if(true){
            var xmlDoc = createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
            
            var obj;
            if(cGUID!='*'){
                obj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+cGUID+"']");
            }else{
                obj=selectSingleNodeFromDOM(x,"/Root");
            }
            if(obj.getAttribute("FOPEN")=='Y'){
                obj.setAttribute("FOPEN","N");
            }else{
                obj.setAttribute("FOPEN","Y");
            }
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }
        
    }
}

function upItem()
{
    if(cGUID!=''){
        document.getElementById("Menu_Layout").style.display='none';
        document.getElementById("RootMenu").style.display='none';
        document.getElementById("MenuFile").style.display='none';
        if(true){
            var xmlDoc = createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
            
            var obj;
            if(cGUID!='*'){
                obj=selectSingleNodeFromDOM(x,"//Item[@GUID='"+cGUID+"']");
            }else{
                obj=selectSingleNodeFromDOM(x,"/Root");
            }
            var upobj=obj.previousSibling;
            var pobj=obj.parentNode;
            pobj.removeChild(obj);
            pobj.insertBefore(obj, upobj);
            
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }
        
    }
}
function downItem()
{
    if(cGUID!=''){
        document.getElementById("Menu_Layout").style.display='none';
        document.getElementById("RootMenu").style.display='none';
        document.getElementById("MenuFile").style.display='none';
        if(true){
            var xmlDoc = createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
            
            var obj;
            if(cGUID!='*'){
                obj=selectSingleNodeFromDOM(x,"//Item[@GUID='"+cGUID+"']");
            }else{
                obj=selectSingleNodeFromDOM(x,"/Root");
            }
            var pobj=obj.parentNode;
            
            var upobj=obj.nextSibling;
            if(upobj==null){
                upobj=pobj.firstChild;
                pobj.removeChild(obj);
                pobj.insertBefore(obj, upobj);
            }else{
                upobj=upobj.nextSibling;
                pobj.removeChild(obj);
                pobj.insertBefore(obj, upobj);
            }
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }
        
    }
}
function addFolder()
{
    if(cGUID!=''){
    //20110801 Eric Hsu : Safari Problem , cGUID 變數在showModalDialog會變空值
        var tmpcGUID = cGUID;
        document.getElementById("Menu_Layout").style.display='none';
        document.getElementById("RootMenu").style.display='none';
        document.getElementById("MenuFile").style.display='none';       
        retV=window.showModalDialog('Detail.aspx',''+splitter+''+splitter+'','dialogWidth=450px;dialogHeight=160px;status=no;resizable=no');
        if((retV!=null) && (retV!='')){
            var xmlDoc = createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
       
            var obj;
            if(tmpcGUID!='*'){
                obj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+tmpcGUID+"']");
            }else{
                obj=selectSingleNodeFromDOM(x,"/Root");
            }
            var line=retV.split(splitter);
            
            var newNode=xmlDoc.createElement("Folder");
            newNode.setAttribute("GUID",'G'+Math.floor(Math.random()*100000000));
            newNode.setAttribute("ID", line[0]);
            newNode.setAttribute("title",line[1]);
            newNode.setAttribute("ImageURL",line[2]);
            newNode.setAttribute("FOPEN","N");
            obj.appendChild(newNode);
            
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }
        
    }
}
function modifyFolder()
{
    if(cGUID!=''){
        //20110801 Eric Hsu : Safari Problem , cGUID 變數在showModalDialog會變空值
        var tmpcGUID = cGUID;
        var xmlDoc = createXMLDOM(xmlData);
        var x=xmlDoc.documentElement;
        
        obj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+tmpcGUID+"']");
        var data=obj.getAttribute("ID")+splitter+obj.getAttribute("title")+splitter+obj.getAttribute("ImageURL");
        document.getElementById("Menu_Layout").style.display='none';
        document.getElementById("RootMenu").style.display='none';
        document.getElementById("MenuFile").style.display='none';
        retV=window.showModalDialog('Detail.aspx',data,'dialogWidth=450px;dialogHeight=160px;status=no;resizable=no');
        
        if((retV!=null) && (retV!='')){
            var xmlDoc = createXMLDOM(xmlData);
            var x=xmlDoc.documentElement;
            
            var obj;
            obj=selectSingleNodeFromDOM(x,"//Folder[@GUID='"+tmpcGUID+"']");
            var line=retV.split(splitter);
            
            obj.setAttribute("ID", line[0]);
            obj.setAttribute("title",line[1]);
            obj.setAttribute("ImageURL",line[2]);
            
            xmlData=getFullXMLString(xmlDoc);
            drawTree();
        }
    }
}
function saveData()
{
    if(confirm('<%=msg3%>')){
        var postData="Method=saveData&Data="+xmlData.replace(/</g,'#lt;').replace(/>/g,'#gt;').replace(/&/g,'#AND;')+"&"+window.location.search.substring(1, window.location.search.length);;
        //alert(postData);
	    var xmlhttp=null;
        xmlhttp=createXMLHTTP();
        xmlhttp.open('POST', 'Maintain.aspx' , false);
        xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
        xmlhttp.send(postData);
        
        alert(xmlhttp.responseText);
        return true;
    }else{
        return false;
    }
}
function exportData()
{
    if(confirm('<%=msg4%>')){
        var postData="Method=exportData";
        //alert(postData);
	    var xmlhttp=null;
        xmlhttp=createXMLHTTP();
        xmlhttp.open('POST', 'Maintain.aspx' , false);
        xmlhttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
        xmlhttp.send(postData);
        
        eval(xmlhttp.responseText);
    }
}
function changeLanguage()
{
    document.getElementById("SaveButton").value='<%=msg5%>';
    document.getElementById("ExportButton").value='<%=msg6%>';
    setInnerText(document.getElementById("DSCLabel1"),'<%=msg7%>');
    document.getElementById("SearchButton").value='<%=msg8%>';
    document.getElementById("SearchNewButton").value='<%=msg9%>';
    document.getElementById("SearchAll").value='<%=msg10%>';
    setInnerText(document.getElementById("explain1"),'<%=msg11%>');
    setInnerText(document.getElementById("explain2"),'<%=msg12%>');
    setInnerText(document.getElementById("MI01"),'<%=msg13%>');
    setInnerText(document.getElementById("MI02"),'<%=msg14%>');
    setInnerText(document.getElementById("MI03"),'<%=msg15%>');
    setInnerText(document.getElementById("MI04"),'<%=msg16%>');
    setInnerText(document.getElementById("MI05"),'<%=msg17%>');
    setInnerText(document.getElementById("MI06"),'<%=msg18%>');
    setInnerText(document.getElementById("MI07"),'<%=msg19%>');
    setInnerText(document.getElementById("MI08"),'<%=msg20%>');
}
function ResetSize() {
    document.getElementById("ToolBarTree").style.height = window.frameElement.clientHeight - 100 + "px";
    document.getElementById("AllProgramList").style.height = window.frameElement.clientHeight - 100 + "px";
}
</script>

</head>
<body onload='ResetSize();drawBarData()' onclick='closeMenu()' onresize="ResetSize();">
    <form id="form1" runat="server">
    <div>
        <input id="SaveButton" type=button value='儲存設定' onclick='return saveData()' style="border:1px solid #000000;" />
        <input id="ExportButton" type=button value='匯出設定' onclick='return exportData()' style="border:1px solid #000000;" />
        <img src="delete.gif"  ondrop='stopEvent(getEvent());drop_on_Trash();' ondragenter='stopEvent(getEvent());' ondragover='stopEvent(getEvent());' />
        <span ID="DSCLabel1" runat="server" Width="122px">搜尋可使用作業: </span>
        <asp:TextBox ID="SearchValue" runat="server" CssClass="SingleField_Normal"></asp:TextBox>
        <input id="SearchButton" type=button value='搜尋' onclick='return searchProgramList()' style="border:1px solid #000000;" />
        <input id="SearchNewButton" type=button value='新項目' onclick='return searchNew()' style="border:1px solid #000000;" />
        <input id="SearchAll" type=button value='全部顯示' onclick='return searchAll()' style="border:1px solid #000000;" />
        <table border=0 cellspacing=3 cellpadding=0 width=100% height=90% >
            <tr>
                <td width=50% height=20px class='FrameTitle'>&nbsp;<span id='explain1'>將項目拉至適當位置以改變選項內容, 或是拖拉至垃圾桶刪除</span></td>
                <td width=50% height=20px class='FrameTitle'>&nbsp;<span id='explain2'>將項目拖拉至右方樹狀清單以增加選項內容</span></td>
            </tr>
            <tr>
                <td width=50%>
                <div id=ToolBarTree style="scrollbar-face-color:#EEEEEE;scrollbar-highlight-color:#CCCCFF;font-size:9pt;width:100%;height:100%;overflow:auto;border-style:solid;border-width:1px;border-color:blue"></div>
                </td>
                <td width=50%>
                <div id=AllProgramList style="scrollbar-face-color:#EEEEEE;scrollbar-highlight-color:#CCCCFF;font-size:9pt;width:100%;height:100%;overflow:auto;border-style:solid;border-width:1px;border-color:blue"></div>
                </td>
            </tr>
        </table>
    </div>
    
<div id='Menu_Layout' style="display:none;position:absolute;width:150px;height:24px;top:100px;left:200px">
<div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
<iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
<table class='MenuList' border=0 cellpadding=2 cellspacing=0 width=100% height=100%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='addFolder();return false;'><span id='MI01'>新增模組</span></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='modifyFolder();return false;'><span id='MI02'>修改模組</span></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='upFolder();return false;'><span id='MI03'>往上</span></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='downFolder();return false;'><span id='MI04'>往下</span></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='toggleFOpen();return false;'><span id='MI05'>切換預設開啟</span></a></td>
</tr>
</table>
</div>
</div>

<div id='MenuFile' style="display:none;position:absolute;width:150px;height:24px;top:100px;left:200px">
<div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
<iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
<table class='MenuList' border=0 cellpadding=2 cellspacing=0 width=100% height=100%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='upItem();return false;'><span id='MI06'>往上</span></a></td>
</tr>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='downItem();return false;'><span id='MI07'>往下</span></a></td>
</tr>
</table>
</div>
</div>
    
<div id='RootMenu' style="display:none;position:absolute;width:150px;height:24px;top:100px;left:200px">
<div style="position:absolute;top:0px;left:0px;width:100%;height:100%">
<iframe frameborder=0 style="top:0px;height:0px;position:absolute;width:100%;height:100%;z-index:-1"></iframe>
<table class='MenuList' border=0 cellpadding=2 cellspacing=0 width=100% height=100%>
<tr>
    <td width=15px class='MenuHead'>&nbsp;</td>
    <td class='MenuContent'><a href='#' onclick='addFolder();return false;'><span id='MI08'>新增模組</span></a></td>
</tr>
</table>
</div>
</div>
    </form>
</body>
</html>
