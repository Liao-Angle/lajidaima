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
using WebServerProject.flow.SMWI;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWI_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWI";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        SMWIAAA008.clientEngineType = engineType;
        SMWIAAA008.connectDBString = connectString;

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
        SMWIAAB003.setListItem(ids);
        SMWIAAD003.setListItem(ids);

        sql = "select SMWCAAA002 from SMWCAAA";
        ds = engine.getDataSet(sql, "TEMP");
        ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ids[i, 0];
        }
        SMWIAAC003.setListItem(ids);

        ids = new string[,]{
            {"W",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "message", " idsW", "待辦事項")},
            {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "message", " idsN", "通知")}
        };
        SMWIAAA007.setListItem(ids);

        SMWIAAA obj = (SMWIAAA)objects;
        SMWIAAA002.ValueText = obj.SMWIAAA002;
        SMWIAAA003.ValueText = obj.SMWIAAA003;
        if (obj.SMWIAAA004.Equals("0"))
        {
            SMWIAAA0040.Checked = true;
        }
        else
        {
            SMWIAAA0041.Checked = true;
        }
        if (obj.SMWIAAA005.Equals("0"))
        {
            SMWIAAA0050.Checked = true;
        }
        else
        {
            SMWIAAA0051.Checked = true;
        }
        SMWIAAA007.ValueText = obj.SMWIAAA007;
        SMWIAAA007_SelectChanged(obj.SMWIAAA007);
        
        if (obj.SMWIAAA006.Equals("Y"))
        {
            SMWIAAA006.Checked = true;
        }
        else
        {
            SMWIAAA006.Checked = false;
        }

        SMWIAAA006_Click(null, null);

        SMWIAAA008.GuidValueText = obj.SMWIAAA008;
        SMWIAAA008.doGUIDValidate();

        if (obj.CURRENTSTATE.Equals("Y"))
        {
            CURRENTSTATE.Checked = true;
        }
        else
        {
            CURRENTSTATE.Checked = false;
        }
        if (obj.PROCESSNAME.Equals("Y"))
        {
            PROCESSNAME.Checked = true;
        }
        else
        {
            PROCESSNAME.Checked = false;
        }
        if (obj.SHEETNO.Equals("Y"))
        {
            SHEETNO.Checked = true;
        }
        else
        {
            SHEETNO.Checked = false;
        }
        if (obj.WORKITEMNAME.Equals("Y"))
        {
            WORKITEMNAME.Checked = true;
        }
        else
        {
            WORKITEMNAME.Checked = false;
        }
        if (obj.SUBJECT.Equals("Y"))
        {
            SUBJECT.Checked = true;
        }
        else
        {
            SUBJECT.Checked = false;
        }
        if (obj.USERNAME.Equals("Y"))
        {
            USERNAME.Checked = true;
        }
        else
        {
            USERNAME.Checked = false;
        }
        if (obj.CREATETIME.Equals("Y"))
        {
            CREATETIME.Checked = true;
        }
        else
        {
            CREATETIME.Checked = false;
        }
        if (obj.VIEWTIMES.Equals("Y"))
        {
            VIEWTIMES.Checked = true;
        }
        else
        {
            VIEWTIMES.Checked = false;
        }
        if (obj.IMPORTANT.Equals("Y"))
        {
            IMPORTANT.Checked = true;
        }
        else
        {
            IMPORTANT.Checked = false;
        }
        if (obj.ATTACH.Equals("Y"))
        {
            ATTACH.Checked = true;
        }
        else
        {
            ATTACH.Checked = false;
        }
        if (obj.FIELDORDER.Equals(""))
        {
            obj.FIELDORDER = "GUID;ATTACH;SUBJECT;IMPORTANT;CURRENTSTATE;PROCESSNAME;WORKITEMNAME;CREATETIME;USERNAME;WORKTYPE;SHEETNO;VIEWTIMES;WORKASSIGNMENT;ASSIGNMENTTYPE;REASSIGNMENTTYPE;D_INSERTUSER;D_INSERTTIME;D_MODIFYUSER;D_MODIFYTIME";
        }
        string[] orList = obj.FIELDORDER.Split(new char[] { ';' });
        ids = new string[orList.Length, 2];
        for (int i = 0; i < orList.Length; i++)
        {
            ids[i, 0] = orList[i];
            //國昌2009/09/22中文化修改
            ids[i, 1] = getLanguageText(orList[i]);
        }
        FieldOrderList.setListItem(ids);

        bool isAddNew = (bool)getSession("isNew");
        DataObjectSet child = null;
        DataObjectSet childc = null;
        DataObjectSet childd = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.flow.SMWI.SMWIAAB");
            child.setTableName("SMWIAAB");
            obj.setChild("SMWIAAB", child);

            childc = new DataObjectSet();
            childc.setAssemblyName("WebServerProject");
            childc.setChildClassString("WebServerProject.flow.SMWI.SMWIAAC");
            childc.setTableName("SMWIAAC");
            obj.setChild("SMWIAAC", childc);

            childd = new DataObjectSet();
            childd.setAssemblyName("WebServerProject");
            childd.setChildClassString("WebServerProject.flow.SMWI.SMWIAAD");
            childd.setTableName("SMWIAAD");
            obj.setChild("SMWIAAD", childd);
        }
        else
        {
            child = obj.getChild("SMWIAAB");
            childc = obj.getChild("SMWIAAC");
            childd = obj.getChild("SMWIAAD");
        }
        ABTable.HiddenField = new string[] { "SMWIAAB001", "SMWIAAB002" };
        ABTable.dataSource = child;
        ABTable.updateTable();

        ACTable.HiddenField = new string[] { "SMWIAAC001", "SMWIAAC002" };
        ACTable.dataSource = childc;
        ACTable.updateTable();

        ADTable.HiddenField = new string[] { "SMWIAAD001", "SMWIAAD002" };
        ADTable.dataSource = childd;
        ADTable.updateTable();

    }
    private string getLanguageText(string fieldName)
    {
        if (fieldName.Equals("GUID"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "識別碼");
        }
        else if (fieldName.Equals("ATTACH"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "附件");
        }
        else if (fieldName.Equals("IMPORTANT"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "重要性");
        }
        else if (fieldName.Equals("CURRENTSTATE"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "狀態");
        }
        else if (fieldName.Equals("PROCESSNAME"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "流程名稱");
        }
        else if (fieldName.Equals("SHEETNO"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "單號");
        }
        else if (fieldName.Equals("WORKITEMNAME"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "流程角色");
        }
        else if (fieldName.Equals("SUBJECT"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "主旨");
        }
        else if (fieldName.Equals("USERNAME"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "發起人");
        }
        else if (fieldName.Equals("CREATETIME"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "建立時間");
        }
        else if (fieldName.Equals("WORKTYPE"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "工作性質");
        }
        else if (fieldName.Equals("VIEWTIMES"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "讀取次數");
        }
        else if (fieldName.Equals("WORKASSIGNMENT"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "工作指派識別");
        }
        else if (fieldName.Equals("ASSIGNMENTTYPE"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "工作類型");
        }
        else if (fieldName.Equals("REASSIGNMENTTYPE"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "轉派類型");
        }
        else if (fieldName.Equals("D_INSERTUSER"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "建立者");
        }
        else if (fieldName.Equals("D_INSERTTIME"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "建立時間");
        }
        else if (fieldName.Equals("D_MODIFYUSER"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "更新者");
        }
        else if (fieldName.Equals("D_MODIFYTIME"))
        {
            return com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "field", fieldName, "更新時間");
        }
        else
        {
            return "";
        }
    }
    protected override void saveData(DataObject objects)
    {
        if (SMWIAAA007.ValueText.Equals("W"))
        {
            if (SMWIAAA006.Checked)
            {
                if (SMWIAAA008.GuidValueText.Equals(""))
                {
                    //MessageBox("必須選擇意見表達類型");
                    MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwi_detail_aspx.language.ini", "message", "QueryError", "必須選擇意見表達類型"));
                    return;
                }
            }
        }
        SMWIAAA obj = (SMWIAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWIAAA001 = IDProcessor.getID("");
        }

        obj.SMWIAAA002 = SMWIAAA002.ValueText;
        obj.SMWIAAA003 = SMWIAAA003.ValueText;
        if (SMWIAAA0040.Checked)
        {
            obj.SMWIAAA004 = "0";
        }
        else
        {
            obj.SMWIAAA004 = "1";
        }
        if (SMWIAAA0050.Checked)
        {
            obj.SMWIAAA005 = "0";
        }
        else
        {
            obj.SMWIAAA005 = "1";
        }
        if (SMWIAAA006.Checked)
        {
            obj.SMWIAAA006 = "Y";
        }
        else
        {
            obj.SMWIAAA006 = "N";
        }
        obj.SMWIAAA007 = SMWIAAA007.ValueText;
        obj.SMWIAAA008 = SMWIAAA008.GuidValueText;

        if (CURRENTSTATE.Checked)
        {
            obj.CURRENTSTATE = "Y";
        }
        else
        {
            obj.CURRENTSTATE = "N";
        }
        if (PROCESSNAME.Checked)
        {
            obj.PROCESSNAME = "Y";
        }
        else
        {
            obj.PROCESSNAME = "N";
        }
        if (SHEETNO.Checked)
        {
            obj.SHEETNO = "Y";
        }
        else
        {
            obj.SHEETNO = "N";
        }
        if (WORKITEMNAME.Checked)
        {
            obj.WORKITEMNAME = "Y";
        }
        else
        {
            obj.WORKITEMNAME = "N";
        }
        if (SUBJECT.Checked)
        {
            obj.SUBJECT = "Y";
        }
        else
        {
            obj.SUBJECT = "N";
        }
        if (USERNAME.Checked)
        {
            obj.USERNAME = "Y";
        }
        else
        {
            obj.USERNAME = "N";
        }
        if (CREATETIME.Checked)
        {
            obj.CREATETIME = "Y";
        }
        else
        {
            obj.CREATETIME = "N";
        }
        if (VIEWTIMES.Checked)
        {
            obj.VIEWTIMES = "Y";
        }
        else
        {
            obj.VIEWTIMES = "N";
        }
        if (IMPORTANT.Checked)
        {
            obj.IMPORTANT = "Y";
        }
        else
        {
            obj.IMPORTANT = "N";
        }
        if (ATTACH.Checked)
        {
            obj.ATTACH = "Y";
        }
        else
        {
            obj.ATTACH = "N";
        }
        string orderlist = "";
        for (int i = 0; i < FieldOrderList.getListItem().GetLength(0); i++)
        {
            orderlist += FieldOrderList.getListItem()[i, 0] + ";";
        }
        orderlist = orderlist.Substring(0, orderlist.Length - 1);
        obj.FIELDORDER = orderlist;

        DataObjectSet child = ABTable.dataSource;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMWIAAB ab = (SMWIAAB)child.getAvailableDataObject(i);
            ab.SMWIAAB002 = obj.SMWIAAA001;
        }
        DataObjectSet childc = ACTable.dataSource;
        for (int i = 0; i < childc.getAvailableDataObjectCount(); i++)
        {
            SMWIAAC ab = (SMWIAAC)childc.getAvailableDataObject(i);
            ab.SMWIAAC002 = obj.SMWIAAA001;
        }
        DataObjectSet childd = ADTable.dataSource;
        for (int i = 0; i < childd.getAvailableDataObjectCount(); i++)
        {
            SMWIAAD ad = (SMWIAAD)childd.getAvailableDataObject(i);
            ad.SMWIAAD002 = obj.SMWIAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWIAgent agent = new SMWIAgent();
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
        SMWIAAB ab = (SMWIAAB)objects;

        if (isNew)
        {
            ab.SMWIAAB001 = IDProcessor.getID("");
            ab.SMWIAAB002 = "TEMP";
        }
        ab.SMWIAAB003 = SMWIAAB003.ValueText;
        ab.SMWIAAB004 = SMWIAAB003.ReadOnlyText;
        return true;
    }
    protected void ABTable_ShowRowData(DataObject objects)
    {
        SMWIAAB ab = (SMWIAAB)objects;

        SMWIAAB003.ValueText = ab.SMWIAAB003;
    }
    protected bool ACTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWIAAC ab = (SMWIAAC)objects;

        if (isNew)
        {
            ab.SMWIAAC001 = IDProcessor.getID("");
            ab.SMWIAAC002 = "TEMP";
        }
        ab.SMWIAAC003 = SMWIAAC003.ValueText;
        return true;
    }
    protected void ACTable_ShowRowData(DataObject objects)
    {
        SMWIAAC ab = (SMWIAAC)objects;

        SMWIAAC003.ValueText = ab.SMWIAAC003;
    }
    protected bool ADTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWIAAD ad = (SMWIAAD)objects;

        if (isNew)
        {
            ad.SMWIAAD001 = IDProcessor.getID("");
            ad.SMWIAAD002 = "TEMP";
        }
        ad.SMWIAAD003 = SMWIAAD003.ValueText;
        ad.SMWIAAD004 = SMWIAAD003.ReadOnlyText;
        return true;
    }
    protected void ADTable_ShowRowData(DataObject objects)
    {
        SMWIAAD ad = (SMWIAAD)objects;

        SMWIAAD003.ValueText = ad.SMWIAAD003;
    }
    protected void SMWIAAA007_SelectChanged(string value)
    {
        if (value.Equals("N"))
        {
            SMWIAAA006.Checked = false;
            SMWIAAA006.ReadOnly = true;
        }
        else
        {
            SMWIAAA006.ReadOnly = false;
        }
    }
    protected void SMWIAAA006_Click(object sender, EventArgs e)
    {
        if (SMWIAAA006.Checked)
        {
            SMWIAAA008.ReadOnly = false;
        }
        else
        {
            SMWIAAA008.GuidValueText = "";
            SMWIAAA008.doGUIDValidate();
            SMWIAAA008.ReadOnly = true;
        }
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
