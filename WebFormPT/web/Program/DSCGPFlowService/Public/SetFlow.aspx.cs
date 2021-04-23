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
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using WebServerProject.flow.SMWP;

public partial class Program_DSCGPFlowService_Public_SetFlow : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                //底下這段是取得原表單畫面相關參數設定. 若要傳其它參數, 可仿照平台基本寫法, 
                //將資料存在以原本PageUniqueID命名的Session中. 系統會自動將原本畫面的PageUniqueID傳入
                string PGID = Request.QueryString["PGID"];
                string SMWDAAA001 = Request.QueryString["SMWDAAA001"];

                OpenWin1.clientEngineType = (string)Session["engineType"];
                OpenWin1.connectDBString = (string)Session["connectString"];

                Session["TEMPSETFLOW001"] = Request.QueryString["SMWDAAA001"];
                DetailList.HiddenField = new string[] { "SMWPAAA001" };
                DetailList.InputForm = "SetFlowDetail.aspx";
                DetailList.dataSource = (DataObjectSet)getSession(Request.QueryString["PGID"], "setFlowGroup");
                DetailList.updateTable();
            }
        }
    }
    protected void SendButton_Click(object sender, EventArgs e)
    {
        if (DetailList.dataSource.getAvailableDataObjectCount()==0)
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflow_aspx.language", "message", "DetailListCountError", "「加入關卡」不可為空."));
            return;
        }
        Session["IsSetFlow"] = "Y";


        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");
                Response.Write("window.top.returnValue='YES';");
                break;
            default:
                string js = "";
                js += "window.parent.parent.$('#ecpDialog').attr('ret', 'YES');";
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        }                    
    }
    protected void PhraseButton_Click(object sender, EventArgs e)
    {
        OpenWin1.PageUniqueID = this.PageUniqueID;
        OpenWin1.identityID = "0001";
        OpenWin1.paramString = "SMVLAAA002";
        OpenWin1.whereClause = "SMVLAAA002='" + (string)Session["UserID"] + "'";
        OpenWin1.openWin("SMVLAAA", "001", false, "0001");
    }
    protected void OpenWin1_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            //SignOpinion.ValueText += values[0, 2];
        }

    }
    protected void DetailList_AddOutline(DataObject objects, bool isNew)
    {
        if (isNew)
        {
            SMWPAAA aa = (SMWPAAA)objects;
            aa.SMWPAAA002 = DetailList.dataSource.getAvailableDataObjectCount().ToString();
        }
        sort();
    }
    private void sort()
    {
        string[,] orderby = new string[,]{
            {"SMWPAAA002", DataObjectConstants.ASC}
        };
        DetailList.dataSource.sort(orderby);
        DetailList.updateTable();
    }
    protected void DetailList_DeleteData()
    {
        sort();
        rearrangeOrder();
    }
    private void rearrangeOrder()
    {
        DataObjectSet dos = DetailList.dataSource;
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            SMWPAAA obj = (SMWPAAA)dos.getAvailableDataObject(i);
            obj.SMWPAAA002 = string.Format("{0}", i + 1);
        }
        DetailList.updateTable();
    }
    protected void TOP_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = DetailList.getSelectedItem();
        if ((ddo==null) || (ddo.Length != 1))
        {
            //MessageBox("請勾選要到第一筆的資料");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflow_aspx.language", "message", "QueryError", "請勾選要到第一筆的資料"));
            return;
        }
        ddo[0].setData("SMWPAAA002", "0");
        sort();
        rearrangeOrder();
        DetailList.UnCheckAllData();
    }
    protected string DetailList_CustomDisplayField(string fieldName, string fieldData)
    {
        if (fieldName.Equals("SMWPAAA004"))
        {
            string[] tag = fieldData.Split(new char[] { '#' });
            string showValue = tag[0].Split(new char[] { ';' })[3];
            if (tag.Length > 1)
            {
                showValue += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflow_aspx.language", "message", "showName", " 等..");
            }
            return showValue;
        }
        else
        {
            return fieldData;
        }
    }
    protected void BOTTOM_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = DetailList.getSelectedItem();
        if ((ddo == null) || (ddo.Length != 1))
        {
            //MessageBox("請勾選要到第一筆的資料");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflow_aspx.language", "message", "QueryError", "請勾選要到第一筆的資料"));
            return;
        }
        ddo[0].setData("SMWPAAA002", "100");
        sort();
        rearrangeOrder();
        DetailList.UnCheckAllData();
    }
    protected void UP_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = DetailList.getSelectedItem();
        if ((ddo == null) || (ddo.Length != 1))
        {
            //MessageBox("請勾選要到第一筆的資料");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflow_aspx.language", "message", "QueryError", "請勾選要到第一筆的資料"));
            return;
        }
        ddo[0].setData("SMWPAAA002", string.Format("{0}",int.Parse(ddo[0].getData("SMWPAAA002"))-1.5));
        sort();
        rearrangeOrder();
        DetailList.UnCheckAllData();

    }
    protected void DOWN_Click(object sender, EventArgs e)
    {
        DataObject[] ddo = DetailList.getSelectedItem();
        if ((ddo == null) || (ddo.Length != 1))
        {
            //MessageBox("請勾選要到第一筆的資料");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflow_aspx.language", "message", "QueryError", "請勾選要到第一筆的資料"));
            return;
        }
        ddo[0].setData("SMWPAAA002", string.Format("{0}", int.Parse(ddo[0].getData("SMWPAAA002")) + 1.5));
        sort();
        rearrangeOrder();
        DetailList.UnCheckAllData();

    }
}
