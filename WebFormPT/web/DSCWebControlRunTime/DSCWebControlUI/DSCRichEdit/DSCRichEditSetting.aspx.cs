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
using com.dsc.kernal.factory;
using com.dsc.kernal.databean;
using com.dsc.kernal.utility;

public partial class DSCWebControlRunTime_DSCWebControlUI_DSCRichEdit_DSCRichEditSetting : BaseWebUI.WebFormBasePage
{
    protected override void OnPreRenderComplete(EventArgs e)
    {
        DSCLabel1.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string46", "縮排間隔(px)");
        DSCGroupBox1.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string47", "清單項目設定");
        DSCLabel2.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string48", "前置字元");
        DSCLabel3.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string49", "後置字元");
        DSCLabel4.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string50", "序號清單");
        OKButton.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string51", "確定");
        CancelButton.Text = com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string52", "取消");
        base.OnPreRenderComplete(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string parentPageUniqueID = Request.QueryString["PUID"];
                setSession("ParentPageUniqueID", parentPageUniqueID);
                string DSCRichEditID = Request.QueryString["SOURCE"];
                setSession("DSCRichEditID", DSCRichEditID);

                ArrayList ItemArray = (ArrayList)Session[parentPageUniqueID + "_" + DSCRichEditID + "_ItemArray"];
                DataObjectFactory dof = new DataObjectFactory();
                dof.assemblyName = "DSCWebControl";
                dof.childClassString = "DSCWebControl.DSCRichEdit.ItemArray";
                dof.tableName = "ItemArray";
                dof.addFieldDefinition("GUID", "STRING", "50", "", com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string53", "識別號"), "");
                dof.addFieldDefinition("NAME", "STRING", "100", "", com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string54", "顯示字樣"), "");
                dof.addFieldDefinition("PREFIX", "STRING", "100", "", com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string48", "前置字元"), "");
                dof.addFieldDefinition("POSTFIX", "STRING", "100", "", com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string49", "後置字元"), "");
                dof.addFieldDefinition("LISTORDER", "STRING", "255", "", com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string50", "序號清單"), "");
                dof.addFieldDefinition("ORDER", "DECIMAL", "10", "0", com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string55", "順序"),"");

                dof.addIdentityField("GUID");
                dof.addKeyField("NAME");
                dof.addAllowEmptyField("PREFIX");
                dof.addAllowEmptyField("POSTFIX");

                string xml=dof.generalXML();
                DataObjectSet dos = new DataObjectSet();
                dos.dataObjectSchema = xml;
                dos.isNameLess = true;

                for (int i = 0; i < ItemArray.Count; i++)
                {
                    string sets=(string)ItemArray[i];
                    string[] li = sets.Split(new char[]{';'});
                    DataObject ddo = dos.create();
                    ddo.setData("GUID", IDProcessor.getID(""));
                    ddo.setData("NAME", li[0] + li[2] + li[1]);
                    ddo.setData("PREFIX", li[0]);
                    ddo.setData("POSTFIX", li[1]);
                    ddo.setData("LISTORDER", sets.Substring(li[0].Length + li[1].Length + 2, sets.Length - li[0].Length - li[1].Length - 2));
                    ddo.setData("ORDER", (i+1).ToString());

                    if (!dos.add(ddo))
                    {
                        MessageBox(dos.errorString);
                    }
                }
                dos.sort(new string[,] { { "ORDER", "ASC" } });

                LiList.dataSource = dos;
                LiList.HiddenField = new string[] { "GUID", "ORDER"};
                LiList.updateTable();

                int indentV = (int)Session[parentPageUniqueID + "_" + DSCRichEditID + "_indentValue"];
                IndentValue.ValueText = indentV.ToString();
            }
        }
    }
    protected void LiList_ShowRowData(DataObject objects)
    {
        PrefixChar.ValueText = objects.getData("PREFIX");
        PostfixChar.ValueText = objects.getData("POSTFIX");
        ListOrder.ValueText = objects.getData("LISTORDER");
    }
    protected bool LiList_SaveRowData(DataObject objects, bool isNew)
    {
        if (ListOrder.ValueText.Trim().Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string56", "序號必須填寫"));
            return false;
        }
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("ORDER", "10000");
        }
        objects.setData("NAME", PrefixChar.ValueText + ListOrder.ValueText.Split(new char[] { ';' })[0] + PostfixChar.ValueText);
        objects.setData("PREFIX", PrefixChar.ValueText);
        objects.setData("POSTFIX", PostfixChar.ValueText);
        objects.setData("LISTORDER", ListOrder.ValueText);

        return true;
    }
    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Write("window.close();");

    }
    protected void OKButton_Click(object sender, EventArgs e)
    {
        if (int.Parse(IndentValue.ValueText) <= 0)
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string57", "縮排距離必須大於0"));
            return;
        }
        string parentPageUniqueID=(string)getSession("ParentPageUniqueID");
        string DSCRichEditID=(string)getSession("DSCRichEditID");

        Session[parentPageUniqueID + "_" + DSCRichEditID + "_indentValue"] = int.Parse(IndentValue.ValueText);

        DataObjectSet dos = LiList.dataSource;
        ArrayList ItemArray = (ArrayList)Session[parentPageUniqueID + "_" + DSCRichEditID + "_ItemArray"];
        ItemArray.Clear();
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            DataObject ddo = dos.getAvailableDataObject(i);
            ItemArray.Add(ddo.getData("PREFIX") + ";" + ddo.getData("POSTFIX") + ";" + ddo.getData("LISTORDER"));
        }
        Session[parentPageUniqueID + "_" + DSCRichEditID + "_ItemArray"] = ItemArray;

        Response.Write("window.returnValue='YES';");
        Response.Write("window.close();");

    }
    protected void LiList_AddOutline(DataObject objects, bool isNew)
    {
        reOrder();
    }
    private void reOrder()
    {
        DataObjectSet dos = LiList.dataSource;
        dos.sort(new string[,] { { "ORDER", DataObjectConstants.ASC } });
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            dos.getAvailableDataObject(i).setData("ORDER", (i + 1).ToString());
        }
        LiList.updateTable();
    }
    protected void LiList_DeleteData()
    {
        reOrder();
    }

    protected void TOP_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = LiList.getSelectedItem();
        if ((ddo == null) || (ddo.Length != 1))
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string58", "請勾選要到第一筆的資料"));
            return;
        }
        ddo[0].setData("ORDER", "0");
        reOrder();
        LiList.UnCheckAllData();
    }
    protected void BOTTOM_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = LiList.getSelectedItem();
        if ((ddo == null) || (ddo.Length != 1))
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string59", "請勾選要到最後一筆的資料"));
            return;
        }
        ddo[0].setData("ORDER", "100000000");
        reOrder();
        LiList.UnCheckAllData();
    }
    protected void UP_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = LiList.getSelectedItem();
        if ((ddo == null) || (ddo.Length != 1))
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string60", "請勾選要往前一筆的資料"));
            return;
        }
        ddo[0].setData("ORDER", string.Format("{0}", int.Parse(ddo[0].getData("ORDER")) - 1.5));
        reOrder();
        LiList.UnCheckAllData();
    }
    protected void DOWN_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = LiList.getSelectedItem();
        if ((ddo == null) || (ddo.Length != 1))
        {
            MessageBox(com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "DSCRichEdit", "string61", "請勾選要往後一筆的資料"));
            return;
        }
        ddo[0].setData("ORDER", string.Format("{0}", int.Parse(ddo[0].getData("ORDER")) + 1.5));
        reOrder();
        LiList.UnCheckAllData();

    }

}
