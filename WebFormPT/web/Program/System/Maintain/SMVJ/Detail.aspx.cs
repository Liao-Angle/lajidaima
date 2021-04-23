using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.databean;
using com.dsc.kernal.agent;
using com.dsc.kernal.factory;
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using System.Xml;
using System.IO;
using WebServerProject.maintain.SMVJ;

public partial class Program_System_Maintain_SMVJ_Detail : BaseWebUI.DataListInlineForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVJ";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        this.Controls[0].Visible = false;
        this.Controls[1].Visible = false;

        SMVJAAA aa = (SMVJAAA)objects;
        string fp = (string)GlobalCache.getValue("AgentFolder") + aa.SMVJAAA002+".xml";

        XMLProcessor xp = new XMLProcessor(fp, 0);
        XmlNode xn = xp.selectSingleNode(@"/DOS");

        drawNode(xn, 0);

        string cstr = Request.QueryString["ChildClassString"];
        if ((cstr == null) || (cstr.Equals("")))
        {
            showDetail(xn.Attributes["ChildClassString"].Value);
        }
        else
        {
            showDetail(cstr);
        }
    }

    private void showDetail(string childString)
    {
        string fp = (string)GlobalCache.getValue("DataObjectFolder") + childString + ".xml";
        XMLProcessor xp = new XMLProcessor(fp, 0);
        XmlNode xn=xp.selectSingleNode("/DataObject");

        TAssemblyName.Text = xn.Attributes["assemblyName"].Value;
        TChildClassString.Text = xn.Attributes["childClassString"].Value;
        TTableName.Text = xn.Attributes["tableName"].Value;

        XmlNode xt = xp.selectSingleNode("/DataObject/isCheckTimeStamp");
        if (bool.Parse(xt.InnerText))
        {
            TTimeStamp.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvj_detail_aspx.language.ini", "message", "TextY", "是");
        }
        else
        {
            TTimeStamp.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvj_detail_aspx.language.ini", "message", "TextN", "否");
        }

        
        XmlNodeList it = xp.selectAllNodes("//DataObject/identityField/field");
        string ittag = "";
        foreach (XmlNode xts in it)
        {
            ittag += xts.Attributes["dataField"].Value + ",";
        }
        if (ittag.Length > 0)
        {
            ittag = ittag.Substring(0, ittag.Length - 1);
        }
        TIdentityField.Text = ittag;

        it = xp.selectAllNodes("//DataObject/keyField/field");
        ittag = "";
        foreach (XmlNode xts in it)
        {
            ittag += xts.Attributes["dataField"].Value + ",";
        }
        if (ittag.Length > 0)
        {
            ittag = ittag.Substring(0, ittag.Length - 1);
        }
        TKeyField.Text = ittag;

        it = xp.selectAllNodes("//DataObject/nonUpdateField/field");
        ittag = "";
        foreach (XmlNode xts in it)
        {
            ittag += xts.Attributes["dataField"].Value + ",";
        }
        if (ittag.Length > 0)
        {
            ittag = ittag.Substring(0, ittag.Length - 1);
        }
        TNonUpdateField.ValueText = ittag;

        it = xp.selectAllNodes("//DataObject/allowEmptyField/field");
        ittag = "";
        foreach (XmlNode xts in it)
        {
            ittag += xts.Attributes["dataField"].Value + ",";
        }
        if (ittag.Length > 0)
        {
            ittag = ittag.Substring(0, ittag.Length - 1);
        }
        TAllowEmptyField.ValueText = ittag;

        xt = xp.selectSingleNode("/DataObject/queryStr");
        TQueryString.ValueText = xt.InnerText;

        TAllowEmptyField.ReadOnly = true;
        TNonUpdateField.ReadOnly = true;
        TQueryString.ReadOnly = true;

        //欄位清單
        DataObjectSet dos = new DataObjectSet();
        dos.setAssemblyName("WebServerProject");
        dos.setChildClassString("WebServerProject.maintain.SMVJ.SMVJAAB");
        dos.setTableName("SMVJAAB");

        it = xp.selectAllNodes("//DataObject/fieldDefinition/field");
        foreach (XmlNode xts in it)
        {
            SMVJAAB ab = (SMVJAAB)dos.create();
            ab.SMVJAAB001 = IDProcessor.getID("");
            ab.SMVJAAB002 = IDProcessor.getID("");
            ab.SMVJAAB003 = xts.Attributes["dataField"].Value;
            ab.SMVJAAB004 = xts.Attributes["typeField"].Value;
            ab.SMVJAAB005 = xts.Attributes["lengthField"].Value;
            ab.SMVJAAB006 = xts.Attributes["defaultValue"].Value;
            ab.SMVJAAB007 = xts.Attributes["displayName"].Value;
            ab.SMVJAAB008 = xts.Attributes["showName"].Value;

            dos.add(ab);
        }
        FieldList.ReadOnly = true;
        FieldList.HiddenField = new string[] { "SMVJAAB001", "SMVJAAB002" };
        FieldList.dataSource = dos;
        FieldList.updateTable();
    }

    private void drawNode(XmlNode xn, int level)
    {
        Label lb = new Label();
        string tag = "";
        for (int i = 0; i < level; i++)
        {
            tag += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        }
        lb.Text = tag;
        TreePanel.Controls.Add(lb);

        HyperLink hl = new HyperLink();
        hl.NavigateUrl = "Detail.aspx?ChildClassString=" + xn.Attributes["ChildClassString"].Value+"&ObjectID="+(string)getSession("ObjectID")+"&isNew="+getSession("isNew").ToString();
        hl.Text = xn.Attributes["TableName"].Value + "(" + xn.Attributes["ChildClassString"].Value + ")";
        hl.Style["text-decoration"] = "none";
        TreePanel.Controls.Add(hl);

        LiteralControl lc = new LiteralControl();
        lc.Text = "<br>";
        TreePanel.Controls.Add(lc);

        for (int i = 0; i < xn.ChildNodes.Count; i++)
        {
            drawNode(xn.ChildNodes[i], level + 1);
        }
    }
}
