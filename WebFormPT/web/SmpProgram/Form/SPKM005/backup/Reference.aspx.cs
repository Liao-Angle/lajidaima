using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

public partial class SmpProgram_Form_SPKM005_Reference : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataObject objects = (DataObject)getSession((string)Session["UserID"], "Reference");
        //string source = objects.getData("Source");
        string source = Request.QueryString["Source"];
        
        if (source.Equals("KM"))
        {
            string parentPanelID = "1";
            string dataListID = "ListTable";
            string parentPageUID = Request.QueryString["ParentPageUID"];
            string uiType = "General";
            string uiStatus = "8";
            string docGUID = Request.QueryString["Reference"];
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
            engine.close();

            string url = "Form.aspx?ParentPanelID=" + parentPanelID + "&DataListID=" + dataListID + "&ParentPageUID=" + parentPageUID + "&UIType=" + uiType + "&UIStatus=" + uiStatus + "&ObjectGUID=" + objectGUID + "&IsMaintain=" + isMaintain + "&CurPanelID=" + curPanelID;
            Response.Redirect(url);
        }
    }
}