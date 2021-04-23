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
using WebServerProject.flow.SMWR;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWR_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWR";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select SMWBAAA003, SMWBAAA004 from SMWBAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[,] ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
        SMWRAAB003.setListItem(ids);


        SMWRAAA obj = (SMWRAAA)objects;
        SMWRAAA022.ValueText = obj.SMWRAAA022;
        SMWRAAA023.ValueText = obj.SMWRAAA023;


        if (obj.SMWRAAA002.Equals("Y"))
        {
            SMWRAAA002.Checked = true;
        }
        else
        {
            SMWRAAA002.Checked = false;
        }
        if (obj.SMWRAAA003.Equals("Y"))
        {
            SMWRAAA003.Checked = true;
        }
        else
        {
            SMWRAAA003.Checked = false;
        }
        if (obj.SMWRAAA004.Equals("Y"))
        {
            SMWRAAA004.Checked = true;
        }
        else
        {
            SMWRAAA004.Checked = false;
        }
        if (obj.SMWRAAA005.Equals("Y"))
        {
            SMWRAAA005.Checked = true;
        }
        else
        {
            SMWRAAA005.Checked = false;
        }
        if (obj.SMWRAAA006.Equals("Y"))
        {
            SMWRAAA006.Checked = true;
        }
        else
        {
            SMWRAAA006.Checked = false;
        }
        if (obj.SMWRAAA007.Equals("Y"))
        {
            SMWRAAA007.Checked = true;
        }
        else
        {
            SMWRAAA007.Checked = false;
        }
        if (obj.SMWRAAA008.Equals("Y"))
        {
            SMWRAAA008.Checked = true;
        }
        else
        {
            SMWRAAA008.Checked = false;
        }
        if (obj.SMWRAAA009.Equals("Y"))
        {
            SMWRAAA009.Checked = true;
        }
        else
        {
            SMWRAAA009.Checked = false;
        }
        if (obj.SMWRAAA010.Equals("Y"))
        {
            SMWRAAA010.Checked = true;
        }
        else
        {
            SMWRAAA010.Checked = false;
        }
        if (obj.SMWRAAA011.Equals("Y"))
        {
            SMWRAAA011.Checked = true;
        }
        else
        {
            SMWRAAA011.Checked = false;
        }
        if (obj.SMWRAAA012.Equals("Y"))
        {
            SMWRAAA012.Checked = true;
        }
        else
        {
            SMWRAAA012.Checked = false;
        }
        if (obj.SMWRAAA013.Equals("Y"))
        {
            SMWRAAA013.Checked = true;
        }
        else
        {
            SMWRAAA013.Checked = false;
        }
        if (obj.SMWRAAA014.Equals("Y"))
        {
            SMWRAAA014.Checked = true;
        }
        else
        {
            SMWRAAA014.Checked = false;
        }
        if (obj.SMWRAAA015.Equals("Y"))
        {
            SMWRAAA015.Checked = true;
        }
        else
        {
            SMWRAAA015.Checked = false;
        }
        if (obj.SMWRAAA016.Equals("Y"))
        {
            SMWRAAA016.Checked = true;
        }
        else
        {
            SMWRAAA016.Checked = false;
        }
        if (obj.SMWRAAA017.Equals("Y"))
        {
            SMWRAAA017.Checked = true;
        }
        else
        {
            SMWRAAA017.Checked = false;
        }
        if (obj.SMWRAAA018.Equals("Y"))
        {
            SMWRAAA018.Checked = true;
        }
        else
        {
            SMWRAAA018.Checked = false;
        }
        if (obj.SMWRAAA019.Equals("Y"))
        {
            SMWRAAA019.Checked = true;
        }
        else
        {
            SMWRAAA019.Checked = false;
        }
        if (obj.SMWRAAA020.Equals("Y"))
        {
            SMWRAAA020.Checked = true;
        }
        else
        {
            SMWRAAA020.Checked = false;
        }
        if (obj.SMWRAAA021.Equals("Y"))
        {
            SMWRAAA021.Checked = true;
        }
        else
        {
            SMWRAAA021.Checked = false;
        }

        if (obj.SMWRAAA024.Equals("0"))
        {
            SMWRAAA0240.Checked = true;
        }
        else
        {
            SMWRAAA0241.Checked = true;
        }

        bool isAddNew = (bool)getSession("isNew");

        DataObjectSet dos2 = new DataObjectSet();
        dos2.setAssemblyName("WebServerProject");
        dos2.setChildClassString("WebServerProject.flow.SMWY.SMWYAAA");
        dos2.setTableName("SMWYAAA");
        WebServerProject.flow.SMWY.SMWYAAA aa = (WebServerProject.flow.SMWY.SMWYAAA)dos2.create();

        string[] orList;
        if (isAddNew)
        {
            orList = aa.dataField;
        }
        else
        {
            orList = obj.SMWRAAA200.Split(new char[] { ';' });
        }
        ids = new string[orList.Length, 2];
        for (int i = 0; i < orList.Length; i++)
        {
            string disName = orList[i];
            for (int j = 0; j < aa.dataField.Length; j++)
            {
                if (orList[i].Equals(aa.dataField[j]))
                {
                    disName = aa.displayName[j];
                    break;
                }
            }
            ids[i, 0] = orList[i];
            ids[i, 1] = disName;
        }
        FieldOrderList.setListItem(ids);


        DataObjectSet child = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.flow.SMWR.SMWRAAB");
            child.setTableName("SMWRAAB");
            obj.setChild("SMWRAAB", child);

        }
        else
        {
            child = obj.getChild("SMWRAAB");
        }
        ABTable.HiddenField = new string[] { "SMWRAAB001", "SMWRAAB002" };
        ABTable.dataSource = child;
        ABTable.updateTable();

    }
    protected override void saveData(DataObject objects)
    {
        SMWRAAA obj = (SMWRAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWRAAA001 = IDProcessor.getID("");
        }

        obj.SMWRAAA022 = SMWRAAA022.ValueText;
        obj.SMWRAAA023 = SMWRAAA023.ValueText;

        if (SMWRAAA002.Checked)
        {
            obj.SMWRAAA002 = "Y";
        }
        else
        {
            obj.SMWRAAA002 = "N";
        }
        if (SMWRAAA003.Checked)
        {
            obj.SMWRAAA003 = "Y";
        }
        else
        {
            obj.SMWRAAA003 = "N";
        }
        if (SMWRAAA004.Checked)
        {
            obj.SMWRAAA004 = "Y";
        }
        else
        {
            obj.SMWRAAA004 = "N";
        }
        if (SMWRAAA005.Checked)
        {
            obj.SMWRAAA005 = "Y";
        }
        else
        {
            obj.SMWRAAA005 = "N";
        }
        if (SMWRAAA006.Checked)
        {
            obj.SMWRAAA006 = "Y";
        }
        else
        {
            obj.SMWRAAA006 = "N";
        }
        if (SMWRAAA007.Checked)
        {
            obj.SMWRAAA007 = "Y";
        }
        else
        {
            obj.SMWRAAA007 = "N";
        }
        if (SMWRAAA008.Checked)
        {
            obj.SMWRAAA008 = "Y";
        }
        else
        {
            obj.SMWRAAA008 = "N";
        }
        if (SMWRAAA009.Checked)
        {
            obj.SMWRAAA009 = "Y";
        }
        else
        {
            obj.SMWRAAA009 = "N";
        }
        if (SMWRAAA010.Checked)
        {
            obj.SMWRAAA010 = "Y";
        }
        else
        {
            obj.SMWRAAA010 = "N";
        }
        if (SMWRAAA011.Checked)
        {
            obj.SMWRAAA011 = "Y";
        }
        else
        {
            obj.SMWRAAA011 = "N";
        }
        if (SMWRAAA012.Checked)
        {
            obj.SMWRAAA012 = "Y";
        }
        else
        {
            obj.SMWRAAA012 = "N";
        }
        if (SMWRAAA013.Checked)
        {
            obj.SMWRAAA013 = "Y";
        }
        else
        {
            obj.SMWRAAA013 = "N";
        }
        if (SMWRAAA014.Checked)
        {
            obj.SMWRAAA014 = "Y";
        }
        else
        {
            obj.SMWRAAA014 = "N";
        }
        if (SMWRAAA015.Checked)
        {
            obj.SMWRAAA015 = "Y";
        }
        else
        {
            obj.SMWRAAA015 = "N";
        }
        if (SMWRAAA016.Checked)
        {
            obj.SMWRAAA016 = "Y";
        }
        else
        {
            obj.SMWRAAA016 = "N";
        }
        if (SMWRAAA017.Checked)
        {
            obj.SMWRAAA017 = "Y";
        }
        else
        {
            obj.SMWRAAA017 = "N";
        }
        if (SMWRAAA018.Checked)
        {
            obj.SMWRAAA018 = "Y";
        }
        else
        {
            obj.SMWRAAA018 = "N";
        }
        if (SMWRAAA019.Checked)
        {
            obj.SMWRAAA019 = "Y";
        }
        else
        {
            obj.SMWRAAA019 = "N";
        }
        if (SMWRAAA020.Checked)
        {
            obj.SMWRAAA020 = "Y";
        }
        else
        {
            obj.SMWRAAA020 = "N";
        }
        if (SMWRAAA021.Checked)
        {
            obj.SMWRAAA021 = "Y";
        }
        else
        {
            obj.SMWRAAA021 = "N";
        }
        if (SMWRAAA0240.Checked)
        {
            obj.SMWRAAA024 = "0";
        }
        else
        {
            obj.SMWRAAA024 = "1";
        }

        string orderlist = "";
        for (int i = 0; i < FieldOrderList.getListItem().GetLength(0); i++)
        {
            orderlist += FieldOrderList.getListItem()[i, 0] + ";";
        }
        orderlist = orderlist.Substring(0, orderlist.Length - 1);
        obj.SMWRAAA200 = orderlist;

        DataObjectSet child = ABTable.dataSource;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMWRAAB ab = (SMWRAAB)child.getAvailableDataObject(i);
            ab.SMWRAAB002 = obj.SMWRAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWRAgent agent = new SMWRAgent();
        agent.engine = engine;
        agent.query("1=2");

        //取得資料
        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            result = agent.update();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }
    protected bool ABTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWRAAB ab = (SMWRAAB)objects;

        if (isNew)
        {
            ab.SMWRAAB001 = IDProcessor.getID("");
            ab.SMWRAAB002 = "TEMP";
        }
        ab.SMWRAAB003 = SMWRAAB003.ValueText;
        ab.SMWRAAB004 = SMWRAAB003.ReadOnlyText;
        return true;
    }
    protected void ABTable_ShowRowData(DataObject objects)
    {
        SMWRAAB ab = (SMWRAAB)objects;

        SMWRAAB003.ValueText = ab.SMWRAAB003;
    }

    protected void OrderMoveUp_Click(object sender, EventArgs e)
    {
        string[] vle = FieldOrderList.ValueText;
        if (vle.Length == 0) return;

        int getIndex = 0;
        for (int i = 0; i < FieldOrderList.getListItem().GetLength(0); i++)
        {
            if (vle[0].Equals(FieldOrderList.getListItem()[i, 0]))
            {
                getIndex = i;
                break;
            }
        }

        if (getIndex == 0) return;

        string[,] newids = new string[FieldOrderList.getListItem().GetLength(0), 2];
        for (int i = 0; i < newids.GetLength(0); i++)
        {
            if (i == (getIndex - 1))
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex, 1];
            }
            else if (i == getIndex)
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex - 1, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex - 1, 1];
            }
            else
            {
                newids[i, 0] = FieldOrderList.getListItem()[i, 0];
                newids[i, 1] = FieldOrderList.getListItem()[i, 1];
            }
        }
        FieldOrderList.setListItem(newids);
        FieldOrderList.selectedIndex(new int[] { getIndex - 1 });
    }
    protected void OrderMoveDown_Click(object sender, EventArgs e)
    {
        string[] vle = FieldOrderList.ValueText;
        if (vle.Length == 0) return;

        int getIndex = 0;
        for (int i = 0; i < FieldOrderList.getListItem().GetLength(0); i++)
        {
            if (vle[0].Equals(FieldOrderList.getListItem()[i, 0]))
            {
                getIndex = i;
                break;
            }
        }

        if (getIndex == (FieldOrderList.getListItem().GetLength(0) - 1)) return;

        string[,] newids = new string[FieldOrderList.getListItem().GetLength(0), 2];
        for (int i = 0; i < newids.GetLength(0); i++)
        {
            if (i == (getIndex + 1))
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex, 1];
            }
            else if (i == getIndex)
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex + 1, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex + 1, 1];
            }
            else
            {
                newids[i, 0] = FieldOrderList.getListItem()[i, 0];
                newids[i, 1] = FieldOrderList.getListItem()[i, 1];
            }
        }
        FieldOrderList.setListItem(newids);
        FieldOrderList.selectedIndex(new int[] { getIndex + 1 });
    }
}
