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
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.agent;
using WebServerProject.maintain.SMVP;

public partial class Program_System_Maintain_SMVP_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                readData();
            }
        }
    }

    private void readData()
    {
        SMVPAAA024.clientEngineType = (string)Session["engineType"];
        SMVPAAA024.connectDBString = (string)Session["connectString"];
        SMVPAAA025.clientEngineType = (string)Session["engineType"];
        SMVPAAA025.connectDBString = (string)Session["connectString"];
        SMVPAAA033.clientEngineType = (string)Session["engineType"];
        SMVPAAA033.connectDBString = (string)Session["connectString"];
        SMVPAAA035.clientEngineType = (string)Session["engineType"];
        SMVPAAA035.connectDBString = (string)Session["connectString"];

        string[,] ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "ListItem1", "ECP預設模式")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "ListItem2", "相容模擬SP8模式")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "ListItem3", "模擬SP8模式")}
        };
        SMVPAAA023.setListItem(ids);

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "ListItem4", "全部允許，除以下以外")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "ListItem5", "全部禁止，除以下以外")}
        };
        SMVPAAA031.setListItem(ids);

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "ListItem6", "系統管理員身份")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "ListItem7", "登入角色身份")},
        };
        SMVPAAA039.setListItem(ids);

        string connectString = (string)Session["ConnectString"];
        string engineType = (string)Session["EngineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select * from SMVPAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        if (ds.Tables[0].Rows[0]["SMVPAAA002"].ToString().Equals("Y"))
        {
            SMVPAAA002.Checked = true;
        }
        else
        {
            SMVPAAA002.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA003"].ToString().Equals("Y"))
        {
            SMVPAAA003.Checked = true;
        }
        else
        {
            SMVPAAA003.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA004"].ToString().Equals("Y"))
        {
            SMVPAAA004.Checked = true;
        }
        else
        {
            SMVPAAA004.Checked = false;
        }
        int data5 = int.Parse(ds.Tables[0].Rows[0]["SMVPAAA005"].ToString());
        if ((data5 & 1) > 0)
        {
            SMVPAAA0051.Checked = true;
        }
        else
        {
            SMVPAAA0051.Checked = false;
        }
        if ((data5 & 2) > 0)
        {
            SMVPAAA0052.Checked = true;
        }
        else
        {
            SMVPAAA0052.Checked = false;
        }
        if ((data5 & 4) > 0)
        {
            SMVPAAA0053.Checked = true;
        }
        else
        {
            SMVPAAA0053.Checked = false;
        }
        if ((data5 & 8) > 0)
        {
            SMVPAAA0054.Checked = true;
        }
        else
        {
            SMVPAAA0054.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA006"].ToString().Equals("Y"))
        {
            SMVPAAA006.Checked = true;
        }
        else
        {
            SMVPAAA006.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA007"].ToString().Equals("Y"))
        {
            SMVPAAA007.Checked = true;
        }
        else
        {
            SMVPAAA007.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA008"].ToString().Equals("Y"))
        {
            SMVPAAA008.Checked = true;
        }
        else
        {
            SMVPAAA008.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA009"].ToString().Equals("Y"))
        {
            SMVPAAA009.Checked = true;
        }
        else
        {
            SMVPAAA009.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA010"].ToString().Equals("Y"))
        {
            SMVPAAA010.Checked = true;
        }
        else
        {
            SMVPAAA010.Checked = false;
        }
        SMVPAAA011.ValueText = ds.Tables[0].Rows[0]["SMVPAAA011"].ToString();
        SMVPAAA012.ValueText = ds.Tables[0].Rows[0]["SMVPAAA012"].ToString();
        SMVPAAA013.ValueText = ds.Tables[0].Rows[0]["SMVPAAA013"].ToString();
        SMVPAAA014.ValueText = ds.Tables[0].Rows[0]["SMVPAAA014"].ToString();
        if (ds.Tables[0].Rows[0]["SMVPAAA015"].ToString().Equals("Y"))
        {
            SMVPAAA015.Checked = true;
        }
        else
        {
            SMVPAAA015.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA016"].ToString().Equals("Y"))
        {
            SMVPAAA016.Checked = true;
        }
        else
        {
            SMVPAAA016.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA017"].ToString().Equals("Y"))
        {
            SMVPAAA017.Checked = true;
        }
        else
        {
            SMVPAAA017.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA018"].ToString().Equals("Y"))
        {
            SMVPAAA018.Checked = true;
        }
        else
        {
            SMVPAAA018.Checked = false;
        }
        //if (ds.Tables[0].Rows[0]["SMVPAAA019"].ToString().Equals("Y"))
        //{
        //    SMVPAAA019.Checked = true;
        //}
        //else
        //{
        //    SMVPAAA019.Checked = false;
        //}
        if (ds.Tables[0].Rows[0]["SMVPAAA020"].ToString().Equals("Y"))
        {
            SMVPAAA020.Checked = true;
        }
        else
        {
            SMVPAAA020.Checked = false;
        }
        SMVPAAA021.ValueText = ds.Tables[0].Rows[0]["SMVPAAA021"].ToString();
        SMVPAAA022.ValueText = ds.Tables[0].Rows[0]["SMVPAAA022"].ToString();
        SMVPAAA023.ValueText = ds.Tables[0].Rows[0]["SMVPAAA023"].ToString();
        SMVPAAA024.ValueText = ds.Tables[0].Rows[0]["SMVPAAA024"].ToString();
        SMVPAAA024.doValidate();
        SMVPAAA025.ValueText = ds.Tables[0].Rows[0]["SMVPAAA025"].ToString();
        SMVPAAA025.doValidate();
        if (ds.Tables[0].Rows[0]["SMVPAAA026"].ToString().Equals("Y"))
        {
            SMVPAAA026.Checked = true;
        }
        else
        {
            SMVPAAA026.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA027"].ToString().Equals("Y"))
        {
            SMVPAAA027.Checked = true;
        }
        else
        {
            SMVPAAA027.Checked = false;
        }
        SMVPAAA028.ValueText = ds.Tables[0].Rows[0]["SMVPAAA028"].ToString();
        if (ds.Tables[0].Rows[0]["SMVPAAA029"].ToString().Equals("Y"))
        {
            SMVPAAA029.Checked = true;
        }
        else
        {
            SMVPAAA029.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA030"].ToString().Equals("Y"))
        {
            SMVPAAA030.Checked = true;
        }
        else
        {
            SMVPAAA030.Checked = false;
        }
        SMVPAAA031.ValueText = ds.Tables[0].Rows[0]["SMVPAAA031"].ToString();

        if (ds.Tables[0].Rows[0]["SMVPAAA032"].ToString().Equals("Y"))
        {
            SMVPAAA032.Checked = true;
        }
        else
        {
            SMVPAAA032.Checked = false;
        }
        SMVPAAA033.GuidValueText = ds.Tables[0].Rows[0]["SMVPAAA033"].ToString();
        SMVPAAA033.doGUIDValidate();

        if (ds.Tables[0].Rows[0]["SMVPAAA034"].ToString().Equals("Y"))
        {
            SMVPAAA034.Checked = true;
        }
        else
        {
            SMVPAAA034.Checked = false;
        }
        SMVPAAA035.ValueText = ds.Tables[0].Rows[0]["SMVPAAA035"].ToString();
        SMVPAAA035.doValidate();
        if (ds.Tables[0].Rows[0]["SMVPAAA036"].ToString().Equals("Y"))
        {
            SMVPAAA036.Checked = true;
        }
        else
        {
            SMVPAAA036.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA037"].ToString().Equals("Y"))
        {
            SMVPAAA037.Checked = true;
        }
        else
        {
            SMVPAAA037.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA038"].ToString().Equals("Y"))
        {
            SMVPAAA038.Checked = true;
        }
        else
        {
            SMVPAAA038.Checked = false;
        }
        SMVPAAA039.ValueText = ds.Tables[0].Rows[0]["SMVPAAA039"].ToString();
        if (ds.Tables[0].Rows[0]["SMVPAAA040"].ToString().Equals("Y"))
        {
            SMVPAAA040.Checked = true;
        }
        else
        {
            SMVPAAA040.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA041"].ToString().Equals("Y"))
        {
            SMVPAAA041.Checked = true;
        }
        else
        {
            SMVPAAA041.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA042"].ToString().Equals("Y"))
        {
            SMVPAAA042.Checked = true;
        }
        else
        {
            SMVPAAA042.Checked = false;
        }
        if (ds.Tables[0].Rows[0]["SMVPAAA043"].ToString().Equals("Y"))
        {
            SMVPAAA043.Checked = true;
        }
        else
        {
            SMVPAAA043.Checked = false;
        }
        //SMVPAAB
        SMVPAgent agent = new SMVPAgent();
        agent.engine = engine;
        agent.query("");

        DataObjectSet dos = agent.defaultData;
        SMVPBList.HiddenField = new string[] { "SMVPAAB001" };
        SMVPBList.dataSource = dos;
        SMVPBList.updateTable();

        engine.close();
    }    
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["ConnectString"];
        string engineType = (string)Session["EngineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select * from SMVPAAA";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        DataRow dr = ds.Tables[0].Rows[0];

        if (SMVPAAA002.Checked)
        {
            dr["SMVPAAA002"] = "Y";
        }
        else
        {
            dr["SMVPAAA002"] = "N";
        }
        if (SMVPAAA003.Checked)
        {
            dr["SMVPAAA003"] = "Y";
        }
        else
        {
            dr["SMVPAAA003"] = "N";
        }
        if (SMVPAAA004.Checked)
        {
            dr["SMVPAAA004"] = "Y";
        }
        else
        {
            dr["SMVPAAA004"] = "N";
        }
        int data5 = 0;
        if (SMVPAAA0051.Checked)
        {
            data5 += 1;
        }
        if (SMVPAAA0052.Checked)
        {
            data5 += 2;
        }
        if (SMVPAAA0053.Checked)
        {
            data5 += 4;
        }
        if (SMVPAAA0054.Checked)
        {
            data5 += 8;
        }
        dr["SMVPAAA005"] = data5;
        if (SMVPAAA006.Checked)
        {
            dr["SMVPAAA006"] = "Y";
        }
        else
        {
            dr["SMVPAAA006"] = "N";
        }
        if (SMVPAAA007.Checked)
        {
            dr["SMVPAAA007"] = "Y";
        }
        else
        {
            dr["SMVPAAA007"] = "N";
        }
        if (SMVPAAA008.Checked)
        {
            dr["SMVPAAA008"] = "Y";
        }
        else
        {
            dr["SMVPAAA008"] = "N";
        }
        if (SMVPAAA009.Checked)
        {
            dr["SMVPAAA009"] = "Y";
        }
        else
        {
            dr["SMVPAAA009"] = "N";
        }
        if (SMVPAAA010.Checked)
        {
            dr["SMVPAAA010"] = "Y";
        }
        else
        {
            dr["SMVPAAA010"] = "N";
        }
        float S11;
        if (float.TryParse(SMVPAAA011.ValueText, out S11))
        {
            dr["SMVPAAA011"] = SMVPAAA011.ValueText;
        }
        else 
        {            
            engine.close();
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "QueryError4", "左邊界請輸入數字"));
            return;
        }      
        dr["SMVPAAA012"] = SMVPAAA012.ValueText;
        dr["SMVPAAA013"] = SMVPAAA013.ValueText;
        dr["SMVPAAA014"] = SMVPAAA014.ValueText;
        if (SMVPAAA015.Checked)
        {
            dr["SMVPAAA015"] = "Y";
        }
        else
        {
            dr["SMVPAAA015"] = "N";
        }
        if (SMVPAAA016.Checked)
        {
            dr["SMVPAAA016"] = "Y";
        }
        else
        {
            dr["SMVPAAA016"] = "N";
        }
        if (SMVPAAA017.Checked)
        {
            dr["SMVPAAA017"] = "Y";
        }
        else
        {
            dr["SMVPAAA017"] = "N";
        }
        if (SMVPAAA018.Checked)
        {
            dr["SMVPAAA018"] = "Y";
        }
        else
        {
            dr["SMVPAAA018"] = "N";
        }
        //if (SMVPAAA019.Checked)
        //{
        //    dr["SMVPAAA019"] = "Y";
        //}
        //else
        //{
        //    dr["SMVPAAA019"] = "N";
        //}
        dr["SMVPAAA019"] = "N";

        if (SMVPAAA020.Checked)
        {
            dr["SMVPAAA020"] = "Y";
        }
        else
        {
            dr["SMVPAAA020"] = "N";
        }
        dr["SMVPAAA021"] = SMVPAAA021.ValueText;
        dr["SMVPAAA022"] = SMVPAAA022.ValueText;
        dr["SMVPAAA023"] = int.Parse(SMVPAAA023.ValueText);
        dr["SMVPAAA024"] = SMVPAAA024.ValueText;
        dr["SMVPAAA025"] = SMVPAAA025.ValueText;
        if (SMVPAAA026.Checked)
        {
            dr["SMVPAAA026"] = "Y";
        }
        else
        {
            dr["SMVPAAA026"] = "N";
        }
        if (SMVPAAA027.Checked)
        {
            dr["SMVPAAA027"] = "Y";
        }
        else
        {
            dr["SMVPAAA027"] = "N";
        }
        dr["SMVPAAA028"] = SMVPAAA028.ValueText;
        if (SMVPAAA029.Checked)
        {
            dr["SMVPAAA029"] = "Y";
        }
        else
        {
            dr["SMVPAAA029"] = "N";
        }
        if (SMVPAAA030.Checked)
        {
            dr["SMVPAAA030"] = "Y";
        }
        else
        {
            dr["SMVPAAA030"] = "N";
        }
        dr["SMVPAAA031"] = SMVPAAA031.ValueText;
        if (SMVPAAA032.Checked)
        {
            dr["SMVPAAA032"] = "Y";
        }
        else
        {
            dr["SMVPAAA032"] = "N";
            Session["isTesting"] = null;
        }
        dr["SMVPAAA033"] = SMVPAAA033.GuidValueText;

        if (SMVPAAA034.Checked)
        {
            dr["SMVPAAA034"] = "Y";
        }
        else
        {
            dr["SMVPAAA034"] = "N";
        }
        dr["SMVPAAA035"] = SMVPAAA035.ValueText;
        if (SMVPAAA036.Checked)
        {
            dr["SMVPAAA036"] = "Y";
        }
        else
        {
            dr["SMVPAAA036"] = "N";
        }
        if (SMVPAAA037.Checked)
        {
            dr["SMVPAAA037"] = "Y";
        }
        else
        {
            dr["SMVPAAA037"] = "N";
        }
        if (SMVPAAA038.Checked)
        {
            dr["SMVPAAA038"] = "Y";
        }
        else
        {
            dr["SMVPAAA038"] = "N";
        }
        dr["SMVPAAA039"] = int.Parse(SMVPAAA039.ValueText);
        if (SMVPAAA040.Checked)
        {
            dr["SMVPAAA040"] = "Y";
        }
        else
        {
            dr["SMVPAAA040"] = "N";
        }
        if (SMVPAAA041.Checked)
        {
            dr["SMVPAAA041"] = "Y";
        }
        else
        {
            dr["SMVPAAA041"] = "N";
        }
        if (SMVPAAA042.Checked)
        {
            dr["SMVPAAA042"] = "Y";
        }
        else
        {
            dr["SMVPAAA042"] = "N";
        }
        if (SMVPAAA043.Checked)
        {
            dr["SMVPAAA043"] = "Y";
        }
        else
        {
            dr["SMVPAAA043"] = "N";
        }
        if (!engine.updateDataSet(ds))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "QueryError1", "儲存錯誤: ") + engine.errorString.Replace("\n", "\\n"));
        }
        else
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "QueryError2", "儲存成功"));
        }

        SMVPAgent agent = new SMVPAgent();
        agent.engine = engine;
        agent.defaultData = SMVPBList.dataSource;
        if (!agent.update())
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "QueryError1", "儲存錯誤: ") + engine.errorString.Replace("\n", "\\n"));
        }
        engine.close();
    }

    protected void SMVPBList_ShowRowData(DataObject objects)
    {
        SMVPAAB ab=(SMVPAAB)objects;
        SMVPAAB002.ValueText = ab.SMVPAAB002;
    }
    protected bool SMVPBList_SaveRowData(DataObject objects, bool isNew)
    {
        if (SMVPAAB002.ValueText.Equals("..."))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_smvp_maintain_aspx.language.ini", "message", "QueryError3", "請輸入IP "));
        }

        SMVPAAB ab = (SMVPAAB)objects;
        if (isNew)
        {
            ab.SMVPAAB001 = IDProcessor.getID("");
        }
        ab.SMVPAAB002 = SMVPAAB002.ValueText;
        return true;
    }
}
