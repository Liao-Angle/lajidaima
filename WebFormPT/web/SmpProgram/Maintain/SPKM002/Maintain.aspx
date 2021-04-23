<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Maintain.aspx.cs" Inherits="SmpProgram_Maintain_SPKM001_Maintain" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未命名頁面</title>
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
        var lsplitter = "#!#!#";
        var splitter = "#*#*#";

        function getMajorType() {
            var postData = "Method=GetMajorType";
            var xmlhttp = null;
            xmlhttp = createXMLHTTP();
            xmlhttp.open('POST', 'Maintain.aspx', false);
            xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xmlhttp.send(postData);
            var tooltree = xmlhttp.responseText;
            var strs = "";
            var data = tooltree.split(lsplitter);
            for (var i = 0; i < data.length; i++) {
                var line = data[i].split(splitter);
                strs += "<div nowrap=true id='O_" + line[0] + "'><img src='c.gif' border=0 id='I_" + line[0] + "' guid='" + line[0] + "' oid='" + line[0] + "' state='N' level=0 onclick='toggleSubType(\"" + line[0] + "\");'><span onclick='showMajorType(\"" + line[0] + "\");' style=\"cursor:pointer\">" + line[1] + " " + line[2] + "</span></div>";
                strs += "<div nowrap=true id='D_" + line[0] + "' style=\"display:none\"></div>";
            }
            document.getElementById('MajorSubTypeTree').innerHTML = strs;
        }

        function toggleSubType(superOID) {
            obj = getEventSource();
            if (obj.getAttribute('state') == 'N') {
                var curLevel = obj.getAttribute('level');
                curLevel++;
                //要去取資料
                //var postData = "Method=GetSubType&O=" + obj.getAttribute('guid') + "&P=" + superOID;
                var postData = "Method=GetSubType&P=" + superOID;
                var xmlhttp = null;
                xmlhttp = createXMLHTTP();
                xmlhttp.open('POST', 'Maintain.aspx', false);
                xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                xmlhttp.send(postData);
                var tooltree = xmlhttp.responseText;
                var data = tooltree.split(lsplitter);
                if (tooltree.length > 0) {
                    var strs = "";
                    var data = tooltree.split(lsplitter);
                    for (var i = 0; i < data.length; i++) {
                        var line = data[i].split(splitter);
                        strs += "<div nowrap=true id='O_" + line[0] + "'>";
                        for (var j = 0; j < curLevel; j++) {
                            strs += "　";
                        }
                        //strs += "<img src='c.gif' border=0 id='I_" + line[0] + "' guid='" + obj.getAttribute('guid') + "' oid='" + line[0] + "' state='N' level=" + curLevel + " onclick='toggleSubType(\"" + line[0] + "\");'><span onclick='showSubType(\"" + line[0] + "\");' style=\"cursor:pointer\">" + line[1] + " " + line[2] + "</span></div>";
                        strs += "<img src='c.gif' border=0 id='I_" + line[0] + "' guid='" + obj.getAttribute('guid') + "' oid='" + line[0] + "' state='N' level=" + curLevel +
                                " onclick='toggleDocType(\"" + line[0] + "\");'><span onclick='showSubType(\"" + line[0] + "\");' style=\"cursor:pointer\">" + line[1] + " " + line[2] + "</span></div>";
                        strs += "<div nowrap=true id='D_" + line[0] + "' style=\"display:none\"></div>";
                    }
                    dobj = document.getElementById('D_' + obj.getAttribute('oid'));
                    dobj.innerHTML = strs;
                    dobj.style.display = 'block';

                    iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                    iobj.src = 'o.gif';
                    iobj.setAttribute('state', 'E');
                } else {
                    iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                    iobj.src = 'e.gif';
                }

            } else if (obj.getAttribute('state') == 'E') {
                //要關起來
                dobj = document.getElementById('D_' + obj.getAttribute('oid'));
                dobj.style.display = 'none';
                iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                iobj.src = 'c.gif';
                iobj.setAttribute('state', 'C');
            } else {
                //要打開
                dobj = document.getElementById('D_' + obj.getAttribute('oid'));
                dobj.style.display = 'block';
                iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                iobj.src = 'o.gif';
                iobj.setAttribute('state', 'E');
            }
        }

        function toggleDocType(superOID) {
            obj = getEventSource();
            if (obj.getAttribute('state') == 'N') {
                var curLevel = obj.getAttribute('level');
                curLevel++;
                //要去取資料
                var postData = "Method=GetDocType&P=" + superOID;
                var xmlhttp = null;
                xmlhttp = createXMLHTTP();
                xmlhttp.open('POST', 'Maintain.aspx', false);
                xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                xmlhttp.send(postData);
                var tooltree = xmlhttp.responseText;
                var data = tooltree.split(lsplitter);
                if (tooltree.length > 0) {
                    var strs = "";
                    var data = tooltree.split(lsplitter);
                    for (var i = 0; i < data.length; i++) {
                        var line = data[i].split(splitter);
                        strs += "<div nowrap=true id='O_" + line[0] + "'>";
                        for (var j = 0; j < curLevel; j++) {
                            strs += "　";
                        }
                        strs += "<img src='e.gif' border=0 id='I_" + line[0] + "' guid='" + obj.getAttribute('guid') + "' oid='" + line[0] + "' state='N' level=" + curLevel +
                                " ><span onclick='showDocType(\"" + line[0] + "\");' style=\"cursor:pointer\">" + line[1] + " " + line[2] + "</span></div>";
                        strs += "<div nowrap=true id='D_" + line[0] + "' style=\"display:none\"></div>";
                    }
                    dobj = document.getElementById('D_' + obj.getAttribute('oid'));
                    dobj.innerHTML = strs;
                    dobj.style.display = 'block';

                    iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                    iobj.src = 'o.gif';
                    iobj.setAttribute('state', 'E');
                } else {
                    iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                    iobj.src = 'e.gif';
                }
            } else if (obj.getAttribute('state') == 'E') {
                //要關起來
                dobj = document.getElementById('D_' + obj.getAttribute('oid'));
                dobj.style.display = 'none';
                iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                iobj.src = 'c.gif';
                iobj.setAttribute('state', 'C');
            } else {
                //要打開
                dobj = document.getElementById('D_' + obj.getAttribute('oid'));
                dobj.style.display = 'block';
                iobj = document.getElementById('I_' + obj.getAttribute('oid'));
                iobj.src = 'o.gif';
                iobj.setAttribute('state', 'E');
            }
        }

        function showMajorType(OID) {
            document.getElementById('DetailView').src = 'MajorTypeDetail.aspx?OID=' + OID;
        }

        function showSubType(OID) {
            document.getElementById('DetailView').src = 'SubTypeDetail.aspx?OID=' + OID;
        }

        function showDocType(OID) {
            document.getElementById('DetailView').src = 'DocTypeDetail.aspx?OID=' + OID;
        }

        function checkSession(OID) {
            var postData = "Method=checkSession";
            var xmlhttp = null;
            xmlhttp = createXMLHTTP();
            xmlhttp.open('POST', 'Maintain.aspx', false);
            xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xmlhttp.send(postData);
            var msg = xmlhttp.responseText;
            return msg;
        }

        function createXMLHTTP() {
            if (window.ActiveXObject) {
                try {
                    return new ActiveXObject("Msxml2.XMLHTTP");
                } catch (e) {
                    try {
                        return new ActiveXObject("Microsoft.XMLHTTP");
                    } catch (e2) {
                        return null;
                    }
                }
            } else if (window.XMLHttpRequest) {
                return new XMLHttpRequest();
            } else { return null; }
        }

        function getEventSource() {
            if (window.ActiveXObject) {
                return window.event.srcElement; //for ie
            }
            var func = getEventSource.caller;
            while (func != null) {
                var arg0 = func.arguments[0];
                if (arg0) {
                    if ((arg0.constructor == Event || arg0.constructor == MouseEvent)
                || (typeof (arg0) == 'object' && arg0.preventDefault && arg0.stopPropagation)) {

                        return arg0.target;
                    }
                }
                func = func.caller;
            }
            return null;
        }
    </script>
</head>
<body topmargin="0" leftmargin="0" onload='getMajorType()'>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellspacing="2" cellpadding="0" width="100%" height="100%" style="font-size: 9pt">
                <tr>
                    <td width="210px">
                        <div id="MajorSubTypeTree" style="padding-top: 2px; padding-left: 2px; scrollbar-face-color: #EEEEEE; scrollbar-highlight-color: #CCCCFF; font-size: 9pt; width: 100%; height: 100%; overflow: auto; border-style: solid; border-width: 1px; border-color: blue">
                        </div>
                    </td>
                    <td>
                        <div id="MajorSubTypeDetail" style="scrollbar-face-color: #EEEEEE; scrollbar-highlight-color: #CCCCFF; font-size: 9pt; width: 100%; height: 100%; overflow: auto; border-style: solid; border-width: 1px; border-color: blue">
                            <iframe id='DetailView' src='' frameborder="0" style="width: 100%; height: 100%"></iframe>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
