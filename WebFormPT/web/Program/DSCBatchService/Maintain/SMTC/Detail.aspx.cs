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
using WebServerProject.maintain.SMTC;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;

public partial class Program_DSCBatchService_Maintain_SMTC_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMTC";
        ApplicationID = "SYSTEM";
        ModuleID = "SMTCA";        
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {           
            connectString = (string)Session["connectString"];
            engineType = (string)Session["engineType"];

            
            bool isAddNew = (bool)getSession("isNew");
            DataObjectSet child = null;
            
            SMTCAAA obj = (SMTCAAA)objects;
            if (isAddNew)
            {
                child = new DataObjectSet();
                child.setAssemblyName("WebServerProject");
                child.setChildClassString("WebServerProject.maintain.SMTC.SMTCAAB");
                child.setTableName("SMTCAAB");
                obj.setChild("SMTCAAB", child);

            }
            else
            {
                SMTCAAA002.ReadOnly = true;
                SMTCAAA003.ReadOnly = true;
                SMTCAAA004.ReadOnly = true;
                SMTCAAA005.ReadOnly = true;
                SMTCAAA006.ReadOnly = true;
                
                SMTCAAA002.ValueText = obj.SMTAAAA002;
                SMTCAAA003.ValueText = obj.SMTCAAA003;
                SMTCAAA004.ValueText = obj.SMTCAAA004;
                SMTCAAA005.ValueText = obj.SMTCAAA005;
                if (obj.SMTCAAA006.Equals("1"))
                { 
                    //SMTCAAA006.ValueText ="執行成功";
                    SMTCAAA006.ValueText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smtc_detail_aspx.language.ini", "message", " QueryError1", "執行成功");
                }
                else if (obj.SMTCAAA006.Equals("2"))
                {
                    //SMTCAAA006.ValueText = "執行失敗";
                    SMTCAAA006.ValueText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smtc_detail_aspx.language.ini", "message", " QueryError2", "執行失敗");
                }
                else if (obj.SMTCAAA006.Equals("3"))
                {
                    //SMTCAAA006.ValueText = "執行中";
                    SMTCAAA006.ValueText = com.dsc.locale.LocaleString.getSystemMessageString("program_dscbatchservice_maintain_smtc_detail_aspx.language.ini", "message", " QueryError3", "執行中");
                }
                //SMTCAAA006.ValueText = obj.SMTCAAA006;
                child = obj.getChild("SMTCAAB");                
            }

            OutDataList1.HiddenField = new string[] { "SMTCAAB001", "SMTCAAB002"};
            OutDataList1.dataSource = child;
            OutDataList1.updateTable();

            
    
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

        //IOFactory factory = new IOFactory();
        //engine = factory.getEngine(engineType, connectString);
        //string sql = "SELECT * FROM SMTCAAA where SMTCAAA002='" + SMTCAAA002.ValueText + "'";
        //DataSet ta = engine.getDataSet(sql, "TEMP");
        //engine.close();
        //if (isAddNew)
        //{
        //    if (ta.Tables[0].Rows.Count > 0)
        //    {
        //        SMTCAAA002.ValueText = "";
        //        throw new Exception("批次代號重複，請重新輸入");
        //    }
        //}
        //try
        //{
        //    decimal.Parse(SMTCAAA013.ValueText);
        //}
        //catch (Exception ee)
        //{
        //    SMTCAAA013.ValueText = "";
        //    throw new Exception("容許批次執行最長時間須為數值");
            
        //}
        //if (SMTCAAA010.ValueText.Equals(""))
        //{
        //    throw new Exception("批次排程必須選擇");
        //}
        
        //if (!ListTable.dataSource.getAvailableDataObjectCount().ToString().Equals(SMTCAAA005.ValueText))
        //{
        //    throw new Exception("批次分段設定與分段數目不一致,請重新設定");
        //}
        


        SMTCAAA obj = (SMTCAAA)objects;
        //if (isAddNew)
        //{
        //    obj.SMTCAAA001 = IDProcessor.getID("");
        //    obj.SMTCAAA002 = SMTCAAA002.ValueText;
        //}
        
        //obj.SMTCAAA003 = SMTCAAA003.ValueText;
        //obj.SMTCAAA004 = SMTCAAA004.ValueText;

        //if (ParaExecute.Checked )
        //{
        //    obj.SMTCAAA005 = "Y";
        //}
        //else if (NotParaExecute.Checked)
        //{
        //    obj.SMTCAAA005 = "N";
        //}
        //obj.SMTCAAA006 = SMTCAAA005.ValueText;
        //if (ImExecute.Checked)
        //{
        //    obj.SMTCAAA007="Y";
        //}
        //else if (NotExecute.Checked)
        //{
        //    obj.SMTCAAA007 = "N";
        //}

        //obj.SMTCAAA008 = SMTCAAA008.ValueText;
        ////SMTCAAA008.doGUIDValidate();
        //obj.SMTCAAA009 = "2";
        //obj.SMTCAAA010 = SMTCAAA010.ValueText;
        //obj.SMTCAAA011 = "2";
        //if (ExeBatch.Checked )
        //{
        //    obj.SMTCAAA012="1";            
        //}
        //else if (NtExeBatch.Checked)
        //{
        //    obj.SMTCAAA012 = "2";         
        //}
        //obj.SMTCAAA013 = SMTCAAA013.ValueText;
        //obj.SMTCAAA014 = SMTCAAA014.ValueText;

        //for (int i = 0; i < OutDataList1.dataSource.getAvailableDataObjectCount(); i++)
        //{
        //    SMTCAAB ab = (SMTCAAB)OutDataList1.dataSource.getAvailableDataObject(i);
        //    ab.SMTCAAB002 = obj.SMTCAAA001;
        //}
        

    }
    //protected bool OutDataList1_SaveRowData(com.dsc.kernal.databean.DataObject objects, bool isNew)
    //{
    //    SMTCAAB ab = (SMTCAAB)objects;

    //    if (isNew)
    //    {
    //        ab.SMTCAAB001 = IDProcessor.getID("");
    //        ab.SMTCAAB002 = "TEMP";
    //        //ab.SMTCAAB003 = "SYSTEM";
    //        //ab.SMTCAAB004 = "SMTCA";
    //    }
    //    ab.SMTCAAB003 = SMTCAAB003.ValueText;
    //    ab.SMTCAAB004 = SMTCAAB004.ValueText;
    //    ab.SMTCAAB005 = SMTCAAB005.ValueText;
    //    SMTCAAB003.ValueText = "";
    //    SMTCAAB004.ValueText = "";
    //    SMTCAAB005.ValueText = "";

    //    return true;
    //}
    //protected void OutDataList1_ShowRowData(com.dsc.kernal.databean.DataObject objects)
    //{
        
    //    SMTCAAB ab = (SMTCAAB)objects;

    //    SMTCAAB003.ValueText = ab.SMTCAAB003;
    //    SMTCAAB004.ValueText = ab.SMTCAAB004;
    //    SMTCAAB005.ValueText = ab.SMTCAAB005;
        
    //}
    protected override void saveDB(com.dsc.kernal.databean.DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMTCAgent agent = new SMTCAgent();
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

    //protected void ListTable_AddOutline(com.dsc.kernal.databean.DataObject objects, bool isNew)
    //{
    //    reOrder();
    //}

}
