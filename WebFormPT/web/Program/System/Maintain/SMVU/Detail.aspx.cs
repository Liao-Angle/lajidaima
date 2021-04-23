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
using WebServerProject.maintain.SMVU;

public partial class Program_System_Maintain_SMVU_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVU";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SMVUAAA002.clientEngineType = engineType;
        SMVUAAA002.connectDBString = connectString;

        SMVUAAA obj = (SMVUAAA)objects;
        SMVUAAA002.ValueText = obj.SMVUAAA002;
        SMVUAAA002.doValidate();

        SMVUAAA003.ValueText = obj.SMVUAAA003;
        SMVUAAA004.ValueText = obj.SMVUAAA004;
        SMVUAAA005.ValueText = obj.SMVUAAA005;
        if (obj.SMVUAAA006.Equals("Y"))
        {
            SMVUAAA006.Checked = true;
        }
        else
        {
            SMVUAAA006.Checked = false;
        }
        if (obj.SMVUAAA007.Equals("Y"))
        {
            SMVUAAA007.Checked = true;
        }
        else
        {
            SMVUAAA007.Checked = false;
        }
        if (obj.SMVUAAA008.Equals("Y"))
        {
            SMVUAAA008.Checked = true;
        }
        else
        {
            SMVUAAA008.Checked = false;
        }
        SMVUAAA009.ValueText = obj.SMVUAAA009;
        if (obj.SMVUAAA010.Equals("Y"))
        {
            SMVUAAA010.Checked = true;
        }
        else
        {
            SMVUAAA010.Checked = false;
        }
    }
    protected override void saveData(DataObject objects)
    {
        if (!isCheckNum(SMVUAAA003.ValueText, true, 0))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvu_detail_aspx.language.ini", "message", "QueryError1", "帳戶比對長度不可小於0"));
        }
        if (!isCheckNum(SMVUAAA004.ValueText, true, 4))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvu_detail_aspx.language.ini", "message", "QueryError2", "密碼最小長度必須大於4"));
        }
        if (!isCheckNum(SMVUAAA005.ValueText, false, 20))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvu_detail_aspx.language.ini", "message", "QueryError3", "密碼最大長度必須小於等於20"));
        }
        if (!isCheckNum(SMVUAAA009.ValueText, true, 0))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvu_detail_aspx.language.ini", "message", "QueryError4", "密碼有效天數不可小於0"));
        }
        SMVUAAA obj = (SMVUAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVUAAA001 = IDProcessor.getID("");

        }
        obj.SMVUAAA002 = SMVUAAA002.ValueText;
        obj.userName = SMVUAAA002.ReadOnlyValueText;
        obj.SMVUAAA003 = SMVUAAA003.ValueText;
        obj.SMVUAAA004 = SMVUAAA004.ValueText;
        obj.SMVUAAA005 = SMVUAAA005.ValueText;
        if (SMVUAAA006.Checked)
        {
            obj.SMVUAAA006 = "Y";
        }
        else
        {
            obj.SMVUAAA006 = "N";
        }
        if (SMVUAAA007.Checked)
        {
            obj.SMVUAAA007 = "Y";
        }
        else
        {
            obj.SMVUAAA007 = "N";
        }
        if (SMVUAAA008.Checked)
        {
            obj.SMVUAAA008 = "Y";
        }
        else
        {
            obj.SMVUAAA008 = "N";
        }
        obj.SMVUAAA009 = SMVUAAA009.ValueText;
        if (SMVUAAA010.Checked)
        {
            obj.SMVUAAA010 = "Y";
        }
        else
        {
            obj.SMVUAAA010 = "N";
        }
    }
    private bool isCheckNum(string num, bool isLarge, int limit)
    {
        try
        {
            int d = int.Parse(num);
            if (isLarge)
            {
                if (d >= limit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (d <= limit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch
        {
            return false;
        }

    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVUAgent agent = new SMVUAgent();
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
}
