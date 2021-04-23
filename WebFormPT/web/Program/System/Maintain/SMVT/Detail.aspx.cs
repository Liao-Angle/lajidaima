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
using WebServerProject.maintain.SMVT;

public partial class Program_System_Maintain_SMVT_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVT";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SMVTAAA002.clientEngineType = engineType;
        SMVTAAA002.connectDBString = connectString;

        SMVTAAA obj = (SMVTAAA)objects;
        SMVTAAA002.ValueText = obj.SMVTAAA002;
        SMVTAAA002.doValidate();

    }
    protected override void saveData(DataObject objects)
    {
        if (SMVTAAA002.ValueText.Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvt_detail_aspx.language.ini", "message", "QueryError", "必需選擇使用者"));
        }

        SMVTAAA obj = (SMVTAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVTAAA001 = IDProcessor.getID("");

        }
        obj.SMVTAAA002 = SMVTAAA002.ValueText;
        obj.userName = SMVTAAA002.ReadOnlyValueText;
        string pwd = "";
        //取得密碼
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "select password from Users where id='" + Utility.filter(SMVTAAA002.ValueText) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            obj.SMVTAAA003 = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            obj.SMVTAAA003 = getHashPWD(SMVTAAA002.ValueText);
        }
        engine.close();
    }
    private string getHashPWD(string oriPWD)
    {
        System.Security.Cryptography.SHA1 md = System.Security.Cryptography.SHA1.Create();

        string oriPwd = oriPWD;
        String tSalt = "abcedefghijklmnopqrstuvwxyz";


        byte[] oriData = System.Text.Encoding.Default.GetBytes(tSalt + oriPwd);


        byte[] data = md.ComputeHash(oriData);


        string hashPwd = "";
        hashPwd = com.dsc.kernal.utility.Base64.encode(data);

        return hashPwd;

    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVTAgent agent = new SMVTAgent();
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
