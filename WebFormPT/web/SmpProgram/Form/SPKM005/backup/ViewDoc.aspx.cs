using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using com.dsc.kernal.utility;
using com.dsc.kernal.document;

public partial class SmpProgram_Form_SPKM005_ViewDoc : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string docGUID = (string)getSession((string)Session["UserID"], "ViewDocGUID");
        string parentPanelID = "1";
        string dataListID = "ListTable";
        string parentPageUID = Request.QueryString["ParentPageUID"];
        string uiType = "General";
        string uiStatus = "8";
        string objectGUID = "";
        string isMaintain = "Y";
        string curPanelID = Request.QueryString["CurPanelID"];
        //string sql = "select a.SMWYAAA001 from SmpDocument d, SmpRev r, SMWYAAA a where d.GUID='" + docGUID + "' and d.GUID=r.DocGUID and r.Released='Y' and r.LatestFlag='Y' and r.FormGUID=a.SMWYAAA019";
        string sql = "select r.GUID from SmpDocument d, SmpRev r where d.GUID='" + docGUID + "' and d.GUID=r.DocGUID and r.Released='Y' and r.LatestFlag='Y'";
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        objectGUID = (string)engine.executeScalar(sql);

        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        //string vieweDocUrl = sp.getParam("eKMViewDocUrl");
        //string url = vieweDocUrl + "?ParentPanelID=" + parentPanelID + "&DataListID=" + dataListID + "&ParentPageUID=" + parentPageUID + "&UIType=" + uiType + "&UIStatus=" + uiStatus + "&ObjectGUID=" + objectGUID + "&IsMaintain=" + isMaintain + "&CurPanelID=" + curPanelID;
        string eKMUrl = sp.getParam("eKMViewDocSite");
        string url = eKMUrl + "?runMethod=showeKMForm&ObjectGUID=" + objectGUID + "&CloseTitle=1&CloseToolBar=1&CloseSetting=1&ShowBackList=N";
        
        engine.close();
        Response.Redirect(url);
    }
}