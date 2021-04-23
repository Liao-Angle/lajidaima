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
using WebServerProject.maintain.SMVH;

public partial class Program_System_Maintain_SMVH_Detail : BaseWebUI.DataListSaveForm
{
    private string groupName = "DEFAULT";

    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVH";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        SMVHAAA obj = (SMVHAAA)objects;
        SMVHAAA002.ValueText = obj.SMVHAAA002;
        SMVHAAA003.ValueText = obj.SMVHAAA003;
        SMVHAAA004.ValueText = obj.SMVHAAA004;
        SMVHAAA005.ValueText = obj.SMVHAAA005;
        if (obj.SMVHAAA006.Equals("Y"))
        {
            setSession("IsAlreadyEntryption", true);
            SMVHAAA006.Checked = true;
            SMVHAAA006.ReadOnly = true;
            string keyClass = obj.SMVHAAA008;
            if (keyClass.Trim().Length > 0)
            {
                SMVHAAA008.ValueText = keyClass;
                drrCustomKey.Checked = true;
                drrDefaultKey.Checked = false;
            }
            else
            {
                drrDefaultKey.Checked = true;
                drrCustomKey.Checked = false;
            }
            drrCustomKey.ReadOnly = true;
            drrDefaultKey.ReadOnly = true;
            SMVHAAA004.ReadOnly = true;
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smvh_detail_aspx.language.ini", "message", "ChageNotAllowed", "已加密的參數內容與加密方式；不允許修改"));
        }
        else
        {
            setSession("IsAlreadyEntryption", false);
            SMVHAAA006.Checked = false;
        }        
    }
    protected override void saveData(DataObject objects)
    {
        SMVHAAA obj = (SMVHAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVHAAA001 = IDProcessor.getID("");
        }
        obj.SMVHAAA002 = SMVHAAA002.ValueText;
        obj.SMVHAAA003 = SMVHAAA003.ValueText;
        obj.SMVHAAA005 = SMVHAAA005.ValueText;
        if (SMVHAAA006.Checked)
        {
            obj.SMVHAAA006 = "Y";
        }
        else
        {
            obj.SMVHAAA006 = "N";
        }
        if (!SMVHAAA006.Checked)
        {
            obj.SMVHAAA004 = SMVHAAA004.ValueText;
        }
        else
        {
            WebServerProject.SysParam sp = new WebServerProject.SysParam(null);                       

            
            if ((bool)getSession("IsAlreadyEntryption"))
            {
                obj.SMVHAAA004 = SMVHAAA004.ValueText;
            }
            else
            {
                if (drrCustomKey.Checked)
                {
                    if (SMVHAAA008.ValueText.Trim().Length == 0)
                    {
                        string ExMsg = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvh_detail_aspx.language.ini", "message", "NameSpaceError", "必需輸入加密類別NameSpace");
                        throw new Exception(ExMsg);
                    }
                    string[] className = null ;
                    com.dsc.kernal.agent.AbstractEncryptionKeyAgent  KeyAgent=null;
                    string customKey = "";
                    try
                    {
                        className = SMVHAAA008.ValueText.Split('.');
                        KeyAgent = (com.dsc.kernal.agent.AbstractEncryptionKeyAgent)Utility.getClasses(className[0], SMVHAAA008.ValueText);
                        customKey = KeyAgent.getEntryptionKey();
                    }
                    catch (Exception ue)
                    {                        
                        string ExMsg =com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvh_detail_aspx.language.ini", "message", "InstanceError", "加密類別實體化錯誤，請檢查自訂類別名稱與元件是否存在");
                        throw new Exception(ExMsg);
                    }      
                        sp.setEncryptionKey(customKey);
                        obj.SMVHAAA008 = SMVHAAA008.ValueText;
                }
                obj.SMVHAAA004 = sp.encode(SMVHAAA004.ValueText);
            }
        }
        obj.SMVHAAA007 = groupName;
        
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVHAgent agent = new SMVHAgent();
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

    protected void isReplaceKey_Click(object sender, EventArgs e)
    {
        if (drrCustomKey.Checked)
        {
            this.SMVHAAA008.ReadOnly = false;
        }
        else
        {
            this.SMVHAAA008.ValueText = "";
            this.SMVHAAA008.ReadOnly = true;
        }
    }

    protected void drrCustomKey_Click(object sender, EventArgs e)
    {        
        if (drrCustomKey.Checked)
        {
            this.SMVHAAA008.ReadOnly = false;
        }
        else
        {
            this.SMVHAAA008.ValueText = "";
            this.SMVHAAA008.ReadOnly = true;
        }
    }
    protected void SMVHAAA006_Click(object sender, EventArgs e)
    {
        if (SMVHAAA006.Checked)
        {
            if (SMVHAAA008.ValueText.Trim().Length > 0)
            {
                
                drrCustomKey.Checked = true;
                drrDefaultKey.Checked = false;
            }
            else
            {                
                drrCustomKey.Checked = false;
                drrDefaultKey.Checked = true;                                
            }
            drrDefaultKey.ReadOnly = false;
            drrCustomKey.ReadOnly = false;
        }
        else
        {
            drrCustomKey.Checked = false;
            drrDefaultKey.Checked = false;
            SMVHAAA008.ValueText = "";
            SMVHAAA008.ReadOnly = true;

            drrDefaultKey.ReadOnly = true;
            drrCustomKey.ReadOnly = true;
        }
    }
    protected void drrDefaultKey_Click(object sender, EventArgs e)
    {
        SMVHAAA006.Checked = true;
        SMVHAAA008.ValueText = "";
        SMVHAAA008.ReadOnly = true;
    }
}
