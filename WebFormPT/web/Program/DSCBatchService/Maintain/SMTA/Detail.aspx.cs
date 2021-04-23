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
using WebServerProject.maintain.SMTA;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class Program_DSCBatchService_Maintain_SMTA_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMTA";
        ApplicationID = "SYSTEM";
        ModuleID = "SMTAA";        
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {           
            connectString = (string)Session["connectString"];
            engineType = (string)Session["engineType"];

            SMTAAAA008.clientEngineType = engineType;
            SMTAAAA008.connectDBString = connectString;

            string[,] ids = new string[,]{
                {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " TIME1", "時")},
                {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " TIME2", "分")},
                {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " TIME3", "秒")}
            };
            SMTAAAA014.setListItem(ids);
            string[,] ids1 = new string[,]{
                {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " TIME1", "時")},
                {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " TIME2", "分")},
                {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " TIME3", "秒")}
            };
            SMTAAAA016.setListItem(ids);

            //string[,] ids1 = new string[,]{
            //    {"01","01"},
            //    {"02","02"},
            //    {"03","03"},
            //    {"04","04"},
            //    {"05","05"},
            //    {"06","06"},
            //    {"07","07"},
            //    {"08","08"},
            //    {"09","09"},
            //    {"10","10"}
            //};
            //SMTAAAC003.setListItem(ids1);

            

            bool isAddNew = (bool)getSession("isNew");
            DataObjectSet child = null;
            DataObjectSet cc = null;
            SMTAAAA obj = (SMTAAAA)objects;
            if (isAddNew)
            {
                SMTAAAA006.ValueText = "1";
                SMTAAAA006.ReadOnly = true;
                child = new DataObjectSet();
                child.setAssemblyName("WebServerProject");
                child.setChildClassString("WebServerProject.maintain.SMTA.SMTAAAB");
                child.setTableName("SMTAAAB");
                obj.setChild("SMTAAAB", child);

                cc = new DataObjectSet();
                cc.setAssemblyName("WebServerProject");
                cc.setChildClassString("WebServerProject.maintain.SMTA.SMTAAAC");
                cc.setTableName("SMTAAAC");
                obj.setChild("SMTAAAC", cc);
            }
            else
            {
                SMTAAAA002.ReadOnly = true;
                SMTAAAA002.ValueText = obj.SMTAAAA002;
                SMTAAAA003.ValueText = obj.SMTAAAA003;
                SMTAAAA004.ValueText = obj.SMTAAAA004;
                
                if (obj.SMTAAAA005.Equals("Y"))
                {
                    NotParaExecute.Checked = false;
                    ParaExecute.Checked = true;
                }
                else
                {
                    ParaExecute.Checked = false;
                    NotParaExecute.Checked = true;
                }
                //
                SMTAAAA006.ValueText = obj.SMTAAAA006;
                if (obj.SMTAAAA007.Equals("Y"))
                {
                    NotExecute.Checked = false;
                    ImExecute.Checked = true;
                }
                else
                {
                    ImExecute.Checked = false;
                    NotExecute.Checked = true;
                }

                SMTAAAA008.GuidValueText = obj.SMTAAAA008;
                SMTAAAA008.doGUIDValidate();
                
                SMTAAAA010.ValueText = obj.SMTAAAA010;

                if (obj.SMTAAAA012.Equals("1"))
                {
                    ExeBatch.Checked = true;
                    NtExeBatch.Checked = false;
                }
                else
                {
                    NtExeBatch.Checked = true;
                    ExeBatch.Checked = false;
                }
                SMTAAAA013.ValueText = obj.SMTAAAA013;
                SMTAAAA014.ValueText = obj.SMTAAAA014;
                SMTAAAA015.ValueText = obj.SMTAAAA015;
                SMTAAAA016.ValueText = obj.SMTAAAA016;
                SMTAAAA006.ValueText = obj.SMTAAAA006;
                child = obj.getChild("SMTAAAB");
                cc = obj.getChild("SMTAAAC");
                string[,] orderby = new string[,]{
                    {"SMTAAAC003",DataObjectConstants.ASC}
                };
                cc.sort(orderby);
                //dos.sort(orderby);
            }

            OutDataList1.HiddenField = new string[] { "SMTAAAB001", "SMTAAAB002"};
            OutDataList1.dataSource = child;
            OutDataList1.updateTable();

            ListTable.HiddenField = new string[] { "SMTAAAC001", "SMTAAAC002" };
            ListTable.dataSource = cc;
            ListTable.updateTable();
            reOrder();

            
    
        }
        catch (Exception ze)
        {
            Response.Write("alert('" + ze.Message + "');");
        }
    }
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        
        //欄位檢核
        AbstractEngine engine = null;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        engine = factory.getEngine(engineType, connectString);
        string sql = "SELECT * FROM SMTAAAA where SMTAAAA002='" + Utility.filter(SMTAAAA002.ValueText) + "'";
        DataSet ta = engine.getDataSet(sql, "TEMP");
        engine.close();

        if (isAddNew)
        {
            if (ta.Tables[0].Rows.Count > 0)
            {
                SMTAAAA002.ValueText = "";
                //throw new Exception("批次代號重複，請重新輸入");
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " QueryError1", "批次代號重複，請重新輸入"));
            }
        }
        try
        {
            decimal.Parse(SMTAAAA013.ValueText);
        }
        catch (Exception ee)
        {
            SMTAAAA013.ValueText = "";
            //throw new Exception("容許批次執行最長時間須為數值");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " QueryError2", "容許批次執行最長時間須為數值"));            
        }
        try
        {
            decimal.Parse(SMTAAAA015.ValueText);
        }
        catch (Exception ee)
        {
            SMTAAAA015.ValueText = "";
            //throw new Exception("容許度須為數值");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " QueryError3", "容許度須為數值"));
        }
        if (NotExecute.Checked)
        {
            if (SMTAAAA010.ValueText.Equals(""))
            {
                //throw new Exception("批次排程必須選擇");
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " QueryError4", "批次排程必須選擇"));
            }
        }
        if (!ListTable.dataSource.getAvailableDataObjectCount().ToString().Equals(SMTAAAA006.ValueText))
        {
            //throw new Exception("批次分段設定與分段數目不一致,請重新設定");
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", " QueryError5", "批次分段設定與分段數目不一致,請重新設定"));
        }
        


        SMTAAAA obj = (SMTAAAA)objects;
        if (isAddNew)
        {
            obj.SMTAAAA001 = IDProcessor.getID("");
            obj.SMTAAAA002 = SMTAAAA002.ValueText;
        }
        
        obj.SMTAAAA003 = SMTAAAA003.ValueText;
        obj.SMTAAAA004 = SMTAAAA004.ValueText;

        if (ParaExecute.Checked )
        {
            obj.SMTAAAA005 = "Y";
        }
        else if (NotParaExecute.Checked)
        {
            obj.SMTAAAA005 = "N";
        }
        obj.SMTAAAA006 = SMTAAAA006.ValueText;
        if (ImExecute.Checked)
        {
            obj.SMTAAAA007="Y";
        }
        else if (NotExecute.Checked)
        {
            obj.SMTAAAA007 = "N";
        }
        obj.SMTAAAA008 = SMTAAAA008.GuidValueText;
        obj.SMTAAAA009 = "2";
        obj.SMTAAAA010 = SMTAAAA010.ValueText;
        obj.SMTAAAA011 = "2";
        if (ExeBatch.Checked )
        {
            obj.SMTAAAA012="1";            
        }
        else if (NtExeBatch.Checked)
        {
            obj.SMTAAAA012 = "2";         
        }
        obj.SMTAAAA013 = SMTAAAA013.ValueText;
        obj.SMTAAAA014 = SMTAAAA014.ValueText;
        obj.SMTAAAA015 = SMTAAAA015.ValueText;
        obj.SMTAAAA016 = SMTAAAA016.ValueText;

        for (int i = 0; i < OutDataList1.dataSource.getAvailableDataObjectCount(); i++)
        {
            SMTAAAB ab = (SMTAAAB)OutDataList1.dataSource.getAvailableDataObject(i);
            ab.SMTAAAB002 = obj.SMTAAAA001;
        }
        for (int i = 0; i < ListTable.dataSource.getAvailableDataObjectCount(); i++)
        {
            SMTAAAC ac = (SMTAAAC)ListTable.dataSource.getAvailableDataObject(i);
            ac.SMTAAAC002 = obj.SMTAAAA001;
        }
        reOrder();

    }
    protected bool OutDataList1_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        SMTAAAB ab = (SMTAAAB)objects;

        if (isNew)
        {
            ab.SMTAAAB001 = IDProcessor.getID("");
            ab.SMTAAAB002 = "TEMP";
            //ab.SMTAAAB003 = "SYSTEM";
            //ab.SMTAAAB004 = "SMTAA";
        }
        ab.SMTAAAB003 = SMTAAAB003.ValueText;
        ab.SMTAAAB004 = SMTAAAB004.ValueText;
        ab.SMTAAAB005 = SMTAAAB005.ValueText;
        SMTAAAB003.ValueText = "";
        SMTAAAB004.ValueText = "";
        SMTAAAB005.ValueText = "";

        return true;
    }
    protected void OutDataList1_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
        
        SMTAAAB ab = (SMTAAAB)objects;

        SMTAAAB003.ValueText = ab.SMTAAAB003;
        SMTAAAB004.ValueText = ab.SMTAAAB004;
        SMTAAAB005.ValueText = ab.SMTAAAB005;
        
    }

    protected bool ListTable_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        SMTAAAC ac = (SMTAAAC)objects;
        
        try
        {
            decimal.Parse(SMTAAAC007.ValueText);
        }
        catch (Exception ee)
        {
            //MessageBox("錯誤嘗試次數須為數值");
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smta_detail_aspx.language.ini", "message", "QueryError6", "錯誤嘗試次數須為數值"));
            SMTAAAC007.ValueText = "";
            return false;
        }

        if (isNew)
        {
            ac.SMTAAAC001 = IDProcessor.getID("");
            ac.SMTAAAC002 = "TEMP";
            //ac.SMTAAAC003 = "1";
        }
        ac.SMTAAAC003 = SMTAAAC003.ValueText;
        ac.SMTAAAC004 = SMTAAAC004.ValueText;
        ac.SMTAAAC005 = SMTAAAC005.ValueText;
        ac.SMTAAAC006 = SMTAAAC006.ValueText;
        ac.SMTAAAC007 = SMTAAAC007.ValueText;
        SMTAAAC003.ValueText = "";
        SMTAAAC004.ValueText = "";
        SMTAAAC005.ValueText = "";
        SMTAAAC006.ValueText = "";
        SMTAAAC007.ValueText = "";

        return true;
    }
    protected void ListTable_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    {
        SMTAAAC ac = (SMTAAAC)objects;

        SMTAAAC003.ValueText = ac.SMTAAAC003;
        SMTAAAC004.ValueText = ac.SMTAAAC004;
        SMTAAAC005.ValueText = ac.SMTAAAC005;
        SMTAAAC006.ValueText = ac.SMTAAAC006;
        SMTAAAC007.ValueText = ac.SMTAAAC007;
    }
    protected override void saveDB(com.dsc.kernal.databean.DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMTAAgent agent = new SMTAAgent();
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
    protected void NotParaExecute_Click(object sender, EventArgs e)
    {
        SMTAAAA006.ReadOnly = true;
        SMTAAAA006.ValueText = "1";
    }
    protected void ParaExecute_Click(object sender, EventArgs e)
    {
        SMTAAAA006.ReadOnly = false;
    }
    private void reOrder()
    {
        DataObjectSet dos = ListTable.dataSource;
        string[,] orderby = new string[,]{
            {"SMTAAAC003",DataObjectConstants.ASC}
        };
        dos.sort(orderby);

        int ss = 1;
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            SMTAAAC ac = (SMTAAAC)dos.getAvailableDataObject(i);
            ac.SMTAAAC003 = string.Format("{0:00}", ss);

            ss++;
        }

        ListTable.updateTable();
    }
    protected void ListTable_AddOutline(com.dsc.kernal.databean.DataObject objects, bool isNew)
    {
        reOrder();
    }
    protected void RefreshOrderButton_Click(object sender, EventArgs e)
    {
        reOrder();
    }
}
