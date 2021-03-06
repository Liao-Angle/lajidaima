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
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class Program_DSCGPFlowService_Public_DraftOpenWin : BaseWebUI.GeneralWebPage
{
    public String[] sptag = new String[] { "^&$*%)(&%($&" };

    protected override void OnPreRenderComplete(EventArgs e)
    {
        SearchButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_draftopenwin_aspx.language.ini", "global", "SearchButton.Text", "重新搜尋");
        SendButton.Text = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_draftopenwin_aspx.language.ini", "global", "SendButton.Text", "刪除所選草稿");
        SendButton.ConfirmText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_draftopenwin_aspx.language.ini", "global", "SendButton.ConfirmText", "確定刪除所選草稿?");
        base.OnPreRenderComplete(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        ArrayList selectData=new ArrayList();
        setSession("selectData", selectData);

        Response.Expires = -1;

        //string pmdata = Request.QueryString["PM"];
        string pmdata = (string)Session["OPENWINPM"];
        string[] pmd = pmdata.Split(new string[] { "$$$$$" }, StringSplitOptions.None);

        string connectString = pmd[0];
        setSession("connectString", connectString);

        bool isMulti = false;
        if (pmd[3].Equals("TRUE"))
        {
            isMulti = true;
        }
        else
        {
            isMulti = false;
        }
        isMulti = true;

        setSession("isMulti", isMulti);

        string clientEngineType = pmd[6];
        setSession("clientEngineType", clientEngineType);

        string userEngineType = pmd[7];
        setSession("userEngineType", userEngineType);

        string userDBString = pmd[8];
        setSession("userDBString", userDBString);

        GeneralOpenWinAgent agent=new GeneralOpenWinAgent();
        
        String pm = pmd[4];
        string whereClause = pmd[5];
        String[,] param;
        string[] paramtag;
        if ((pm != null) && (!pm.Equals("")))
        {
            paramtag = pm.Split(new char[] { ',' });

            param = new String[paramtag.Length, 3];
            for (int q = 0; q < paramtag.Length; q++)
            {
                param[q, 0] = "@" + paramtag[q].Trim();
            }
        }
        else
        {
            paramtag = null;
            param = null;
        }
        setSession("paramtag", paramtag);

        if (whereClause != null)
        {
            whereClause = whereClause.Replace("|", "'");
        }

        setSession("whereClause", whereClause);

        ArrayList ret = agent.queryData(clientEngineType, connectString, userEngineType, userDBString, pmd[1], pmd[2], param, whereClause, true, 1, 10);

        string[,] ids = null;
        string tableName = pmd[1];
        string serialNum = pmd[2];
        setSession("tableName", tableName);
        setSession("serialNum", serialNum);

        ArrayList hf = new ArrayList();
        ArrayList df = new ArrayList();
        ArrayList vf = new ArrayList();

        string[] typeList = (string[])ret[2];
        string[] dataField = (string[])ret[4];
        String[] displayName = (String[])ret[1];


        string[] hfs;
        string[] dfs;
        string[] vfs;
        DataObjectSet dos = (DataObjectSet)ret[0];
        if (dos.getAvailableDataObjectCount() == 0)
        {
            DataGrids.DesignTitle = (string[])ret[1];
            for (int i = 0; i < typeList.Length; i++)
            {
                if (typeList[i].ToLower().Equals("hide"))
                {
                    hf.Add(displayName[i]);
                }
                else
                {
                    vf.Add(displayName[i]);
                    df.Add(displayName[i]);

                }
            }
            hfs = new string[hf.Count];
            for (int i = 0; i < hf.Count; i++)
            {
                hfs[i] = (string)hf[i];
            }
            dfs = new string[df.Count];
            for (int i = 0; i < df.Count; i++)
            {
                dfs[i] = (string)df[i];
            }
            vfs = new string[vf.Count];
            for (int i = 0; i < vf.Count; i++)
            {
                vfs[i] = (string)vf[i];
            }
        }
        else
        {
            DataGrids.DesignTitle = null;
            for (int i = 0; i < typeList.Length; i++)
            {
                if (typeList[i].ToLower().Equals("hide"))
                {
                    hf.Add(dataField[i]);
                }
                else
                {
                    vf.Add(dataField[i]);
                    df.Add(displayName[i]);
                }
            }

            hfs = new string[hf.Count];
            for (int i = 0; i < hf.Count; i++)
            {
                hfs[i] = (string)hf[i];
            }
            dfs = new string[df.Count];
            for (int i = 0; i < df.Count; i++)
            {
                dfs[i] = (string)df[i];
            }
            vfs = new string[vf.Count];
            for (int i = 0; i < vf.Count; i++)
            {
                vfs[i] = (string)vf[i];
            }
        }
        DataGrids.dataSource = (DataObjectSet)ret[0];
        DataGrids.HiddenField = hfs;
        DataGrids.updateTable();
        for (int i = 0; i < DataGrids.dataSource.getPageSize(); i++)
        {
            DataGrids.UnCheckData(i);
        }

        /*
        OpenWindowListPanel.iniFilePath = Page.MapPath("/" + GlobalProperty.getProperty("global", "apname") + "/SettingFiles/openwin.ini");
        OpenWindowListPanel.isMulti = isMulti;
        OpenWindowListPanel.displayName = (String[])ret[1];
        OpenWindowListPanel.typeList = (String[])ret[2];
        OpenWindowListPanel.subtypeList = (String[])ret[3];
        OpenWindowListPanel.dataSource = (DataObjectSet)ret[0];
        OpenWindowListPanel.dataField = (String[])ret[4];
        */
        ids = new string[vfs.Length, 2];
        for (int i = 0; i < vfs.Length; i++)
        {
            ids[i, 0] = vfs[i];
            ids[i, 1] = dfs[i];
        }

        FieldSelect.setListItem(ids);

        ids = new string[,]{
            {" like ",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "OpenWin", "string3", "包含")},
            {" = ",com.dsc.locale.LocaleString.getKernalLocaleString("DSCWebControl.dll.language.ini", "OpenWin", "string4", "等於")}
        };
        OperationList.setListItem(ids);

        if (!isMulti)
        {
            SendButton.Display = false;
            DataGrids.IsShowCheckBox = false;
        }
        else
        {
            DataGrids.IsShowCheckBox = true;
            DataGrids.IsHideSelectAllButton = true;
        }

        setSession("page", 1);
        setSession("pagesize", 10);
    }
    private void showData(int page, int pagesize)
    {
        String[] fieldName = new String[] { FieldSelect.ValueText };
        String[] operation = new String[] { OperationList.ValueText };
        String[] valueText = new String[] { ValueText.ValueText };

        String connectString = (string)getSession("connectString");
        string clientEngineType = (string)getSession("clientEngineType");
        string userEngineType = (string)getSession("userEngineType");
        string userDBString = (string)getSession("userDBString");

        GeneralOpenWinAgent agent = new GeneralOpenWinAgent();

        String[,] param;
        string[] paramtag = (string[])getSession("paramtag");

        if ((paramtag != null) && (!paramtag.Equals("")))
        {
            param = new String[paramtag.Length, 3];
            for (int q = 0; q < paramtag.Length; q++)
            {
                param[q, 0] = "@" + paramtag[q].Trim();
            }
        }
        else
        {
            param = null;
        }
        string curWhere = (string)getSession("whereClause");
        if (curWhere.Trim().Equals(""))
        {
            if (OperationList.ValueText.Equals(DataObjectConstants.LIKE))
            {
                curWhere += " " + FieldSelect.ValueText + " like '%" + ValueText.ValueText + "%'";
            }
            else
            {
                curWhere += " " + FieldSelect.ValueText + " = '" + ValueText.ValueText + "'";
            }
        }
        else
        {
            if (OperationList.ValueText.Equals(DataObjectConstants.LIKE))
            {
                curWhere += " and " + FieldSelect.ValueText + " like '%" + ValueText.ValueText + "%'";
            }
            else
            {
                curWhere += " and " + FieldSelect.ValueText + " = '" + ValueText.ValueText + "'";
            }
        }
        ArrayList ret = agent.queryData(clientEngineType, connectString, userEngineType, userDBString, (string)getSession("tableName"), (string)getSession("serialNum"), param, curWhere, true, page, pagesize);

        string[] typeList = (string[])ret[2];
        string[] dataField = (string[])ret[4];
        String[] displayName = (String[])ret[1];


        DataObjectSet qdos = (DataObjectSet)ret[0];
        /*
        DataObject[] fobj = qdos.getFilteredObjects(fieldName, operation, valueText);
        DataObjectSet dos = new DataObjectSet();
        for (int x = 0; x < fobj.Length; x++)
        {
            dos.add(fobj[x]);
        }
        */
        DataObjectSet dos = qdos;

        ArrayList hf = new ArrayList();
        string[] hfs;
        if (dos.getAvailableDataObjectCount() == 0)
        {
            DataGrids.DesignTitle = (string[])ret[1];
            for (int i = 0; i < typeList.Length; i++)
            {
                if (typeList[i].ToLower().Equals("hide"))
                {
                    hf.Add(displayName[i]);
                }
            }
            hfs = new string[hf.Count];
            for (int i = 0; i < hf.Count; i++)
            {
                hfs[i] = (string)hf[i];
            }
        }
        else
        {
            DataGrids.DesignTitle = null;
            for (int i = 0; i < typeList.Length; i++)
            {
                if (typeList[i].ToLower().Equals("hide"))
                {
                    hf.Add(dataField[i]);
                }
            }

            hfs = new string[hf.Count];
            for (int i = 0; i < hf.Count; i++)
            {
                hfs[i] = (string)hf[i];
            }
            DataGrids.dataSource = (DataObjectSet)ret[0];
        }

        DataGrids.HiddenField = hfs;
        DataGrids.dataSource = dos;
        DataGrids.updateTable();

        for (int i = 0; i < dos.getDataObjectCount(); i++)
        {
            DataObject cur = dos.getDataObject(i);
            if (checkEqual(cur))
            {
                DataGrids.CheckData(i);
            }
        }

    }
    private bool checkEqual(DataObject cur)
    {
        ArrayList selectData = (ArrayList)getSession("selectData");
        for (int i = 0; i < selectData.Count; i++)
        {
            bool isequal = true;
            DataObject ddo = (DataObject)selectData[i];
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (!ddo.getData(ddo.dataField[j]).Equals(cur.getData(cur.dataField[j])))
                {
                    isequal = false;
                    break;
                }
            }
            if (isequal)
            {
                return true;
            }
        }
        return false;
    }
    protected void SearchButton_Click(object sender, EventArgs e)
    {
        setSession("page", 1);
        int page = (int)getSession("page");
        int pagesize = (int)getSession("pagesize");

        ArrayList selectData = new ArrayList();
        setSession("selectData", selectData);
        showData(page, pagesize);
        //DataGrids.dataField = (String[])ret[4];

        //String[] displayName = (String[])ret[1];
        //String[] dataField = (String[])ret[4];

    }
    protected void DataGrids_ShowRowData(DataObject objects)
    {
        bool isMulti = (bool)getSession("isMulti");
        if (true)
        {
            //單選, 點擊之後就要回傳資料
            string retV = "";
            for (int i = 0; i < objects.displayName.Length; i++)
            {
                retV += objects.getData(objects.dataField[i]) + "\t";
            }
            if (retV.Length > 0) retV = retV.Substring(0, retV.Length - 1);
            Response.Write("window.top.close();");
            Response.Write("window.top.returnValue='" + Utility.UrlEncode(retV) + "';");
        }
    }
    protected void SendButton_Click(object sender, EventArgs e)
    {
        ArrayList dor = (ArrayList)getSession("selectData");

        if (dor.Count > 0)
        {
            string utag = "'*'";
            for (int i = 0; i < dor.Count; i++)
            {
                DataObject ddo = (DataObject)dor[i];
                utag += ",'" + ddo.getData("GUID") + "'";
            }

            String connectString = (string)getSession("connectString");
            string clientEngineType = (string)getSession("clientEngineType");

            IOFactory factory = new IOFactory();
            AbstractEngine engine=null;

            try
            {
                engine = factory.getEngine(clientEngineType, connectString);

                string sql = "delete from EFORMDRAFT where GUID in (" + utag + ")";
                engine.executeSQL(sql);

                engine.close();

                DataGrids_FirstPageClick();
            }
            catch (Exception te)
            {
                try
                {
                    engine.close();
                }
                catch { };
                MessageBox(te.Message);
            }
        }
        /*
        string retV = "";
        if (dor.Count > 0)
        {
            for (int i = 0; i < dor.Count; i++)
            {
                DataObject ddo = (DataObject)dor[i];
                for (int j = 0; j < ddo.dataField.Length; j++)
                {
                    retV += Utility.UrlEncode(ddo.getData(ddo.dataField[j])) + "\t";
                }
                retV = retV.Substring(0, retV.Length - 1);
                retV += Server.UrlEncode(sptag[0]);
            }
            retV = retV.Substring(0, retV.Length - Server.UrlEncode(sptag[0]).Length);
        }
        
        Response.Write("window.close();");
        Response.Write("window.returnValue='" + retV + "';");
        */
    }
    protected bool DataGrids_ChangePageSizeClick(int pagesize)
    {
        int page = (int)getSession("page");
        setSession("pagesize", pagesize);

        showData(page, pagesize);

        return true;
    }
    protected bool DataGrids_FirstPageClick()
    {
        setSession("page", 1);
        int pagesize = (int)getSession("pagesize");

        showData(1, pagesize);
        return true;
    }
    protected bool DataGrids_NextPageClick()
    {
        int page = (int)getSession("page");
        int pagesize = (int)getSession("pagesize");
        page++;
        setSession("page", page);

        showData(page, pagesize);
        return true;
    }
    protected bool DataGrids_PrevPageClick()
    {
        int page = (int)getSession("page");
        int pagesize = (int)getSession("pagesize");

        if (page > 1)
        {
            page--;
            setSession("page", page);

            showData(page, pagesize);
            return true;

        }
        else
        {
            return false;
        }
    }
    protected void DataGrids_setClickData(string clickList)
    {
        ArrayList selectData = (ArrayList)getSession("selectData");

        for (int i = 0; i < DataGrids.dataSource.getDataObjectCount(); i++)
        {
            string tag = clickList.Substring(i, 1);
            bool inSelectData = false;
            inSelectData = checkEqual(DataGrids.dataSource.getDataObject(i));
            if ((tag.Equals("N")) && (inSelectData))
            {
                removeSelectData(DataGrids.dataSource.getDataObject(i));
            }
            else if ((tag.Equals("Y")) && (!inSelectData))
            {
                selectData.Add(DataGrids.dataSource.getDataObject(i).clone());
            }
        }
        setSession("selectData", selectData);
    }
    private void removeSelectData(DataObject cur)
    {
        ArrayList selectData = (ArrayList)getSession("selectData");

        for (int i = 0; i < selectData.Count; i++)
        {
            bool isequal = true;
            DataObject ddo = (DataObject)selectData[i];
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (!ddo.getData(ddo.dataField[j]).Equals(cur.getData(cur.dataField[j])))
                {
                    isequal = false;
                    break;
                }
            }
            if (isequal)
            {
                selectData.Remove(ddo);
            }
        }
        setSession("selectData", selectData);
    }
}
