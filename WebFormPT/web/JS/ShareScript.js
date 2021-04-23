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

function createXMLDOM(xml) {
    var xmlDoc =null;
    if (window.ActiveXObject) {
        try {
            var xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.loadXML(xml)
        } catch (e) { }
    } else if (window.XMLHttpRequest) {
        var parser = new DOMParser();
        xmlDoc = parser.parseFromString(xml, "text/xml")
    }
    return xmlDoc;
}
function selectSingleNodeFromDOM(xmlDocument, XPath) {
    //alert('selectSingleNodeFromDOM inseide' + XPath); 
    var node = null;
    if (window.ActiveXObject) {
        node= xmlDocument.selectSingleNode(XPath);
    } else if (window.XMLHttpRequest) {        
        var xpe = new XPathEvaluator();
        var result = xpe.evaluate(XPath, xmlDocument, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);
        //alert('result is ' + result);
        if (result != null) {
            //alert('xmlDocument is ' + xmlDocument) ; 
            //alert('XPath is ' + XPath) ;  
            node = result.singleNodeValue;           
            
        }
    }
    return node;
}

function getFullXMLString(xmlDocument) {
    if (xmlDocument.xml) {
        return xmlDocument.xml;
    }
    else {
        var oSerializer = new XMLSerializer();
        return oSerializer.serializeToString(xmlDocument);
    }
}

function getEvent() {
    if (window.event == null) {
        var func = getEvent.caller;
        while (func != null) {
            var arg0 = func.arguments[0];
            if (arg0) {
                if ((arg0.constructor == Event || arg0.constructor == MouseEvent)
            || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                    return arg0;
                }
            }
            func = func.caller;
        }
        return null;
    }
    else 
        return window.event;
}

function getSRCElement(event) {
    if (event.srcElement == null) 
        return event.target;
    else
        return event.srcElement;
}

function cancelBubble(event) {
    if (event.cancelBubble == null)
        event.stopPropagation();
    else
        event.cancelBubble = true;
}

function stopEvent(event) {
    if (event.srcElement == null)
        event.preventDefault();
    else {
        //20150908 matt IE11 不支援event.returnValue
        if (window.Event) {
            event.stopPropagation();
            event.preventDefault();
        }else
            event.returnValue = false;
    }
}

function setInnerText(target, wording) {
    if (target.innerText == null)
        target.textContent = wording;
    else
        target.innerText = wording;
}