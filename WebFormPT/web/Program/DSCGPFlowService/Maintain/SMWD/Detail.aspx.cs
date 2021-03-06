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
using com.dsc.kernal.global;
using WebServerProject.flow.SMWD;
using System.Xml;
using MIL.Html;

public partial class Program_DSCGPFlowService_Maintain_SMWD_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWD";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
    }
    private void showScreen(SMWDAAA obj)
    {
        SMWDAAA002.ValueText = obj.SMWDAAA002;
        SMWDAAA003.ValueText = obj.SMWDAAA003;

        SMWDAAA004.ValueText = obj.SMWDAAA004;
        SMWDAAA004.doValidate();
        SMWDAAA005.GuidValueText = obj.SMWDAAA005;
        SMWDAAA005.doGUIDValidate();

        SMWDAAA006.ValueText = obj.SMWDAAA006;
        SMWDAAA006_SelectChanged(SMWDAAA006.ValueText);

        if (obj.SMWDAAA007.Equals("Y"))
        {
            SMWDAAA007.Checked = true;
        }
        SMWDAAA008.ValueText = obj.SMWDAAA008;

        SMWDAAA009.ValueText = obj.SMWDAAA009;
        SMWDAAA010.GuidValueText = obj.SMWDAAA010;
        SMWDAAA010.doGUIDValidate();

        SMWDAAA011.ValueText = obj.SMWDAAA011;
        SMWDAAA012.GuidValueText = obj.SMWDAAA012;
        SMWDAAA012.doGUIDValidate();

        SMWDAAA013.ValueText = obj.SMWDAAA013;
        SMWDAAA014.ValueText = obj.SMWDAAA014;
        SMWDAAA015.ValueText = obj.SMWDAAA015;

        SMWDAAA016.ValueText = obj.SMWDAAA016;
        SMWDAAA016.doValidate();

        if (obj.SMWDAAA017.Equals("Y"))
        {
            SMWDAAA017.Checked = true;
        }
        else
        {
            SMWDAAA017.Checked = false;
        }
        SMWDAAA018.ValueText = obj.SMWDAAA018;
        SMWDAAA019.ValueText = obj.SMWDAAA019;
        SMWDAAA020.ValueText = obj.SMWDAAA020;
        SMWDAAA021.ValueText = obj.SMWDAAA021;
        SMWDAAA022.ValueText = obj.SMWDAAA022;
        if (obj.SMWDAAA023.Equals("Y"))
        {
            SMWDAAA023.Checked = true;
        }
        else
        {
            SMWDAAA023.Checked = false;
        }
        if (obj.SMWDAAA024.Equals("Y"))
        {
            SMWDAAA024.Checked = true;
        }
        else
        {
            SMWDAAA024.Checked = false;
        }
        SMWDAAA025.ValueText = obj.SMWDAAA025;
        SMWDAAA026.ValueText = obj.SMWDAAA026;
        SMWDAAA027.ValueText = obj.SMWDAAA027;
        SMWDAAA028.ValueText = obj.SMWDAAA028;
        SMWDAAA029.ValueText = obj.SMWDAAA029;

        if (obj.SMWDAAA030.Equals("Y"))
        {
            SMWDAAA030.Checked = true;
        }
        else
        {
            SMWDAAA030.Checked = false;
        }
        SMWDAAA031.ValueText = obj.SMWDAAA031;
        if (obj.SMWDAAA032.Equals("Y"))
        {
            SMWDAAA032.Checked = true;
        }
        else
        {
            SMWDAAA032.Checked = false;
        }
        SMWDAAA033.ValueText = obj.SMWDAAA033;
        if (obj.SMWDAAA034.Equals("Y"))
        {
            SMWDAAA034.Checked = true;
        }
        else
        {
            SMWDAAA034.Checked = false;
        }

        SMWDAAA051.ValueText = obj.SMWDAAA051;
        SMWDAAA052.ValueText = obj.SMWDAAA052;
        SMWDAAA053.ValueText = obj.SMWDAAA053;
        SMWDAAA054.ValueText = obj.SMWDAAA054;
        SMWDAAA055.ValueText = obj.SMWDAAA055;
        SMWDAAA056.ValueText = obj.SMWDAAA056;
        if (obj.SMWDAAA057.Equals("Y"))
        {
            SMWDAAA057.Checked = true;
        }
        else
        {
            SMWDAAA057.Checked = false;
        }

        if (obj.SMWDAAA101.Equals("Y"))
        {
            SMWDAAA101.Checked = true;
        }
        else
        {
            SMWDAAA101.Checked = false;
        }
        if (obj.SMWDAAA102.Equals("Y"))
        {
            SMWDAAA102.Checked = true;
        }
        else
        {
            SMWDAAA102.Checked = false;
        }
        if (obj.SMWDAAA103.Equals("Y"))
        {
            SMWDAAA103.Checked = true;
        }
        else
        {
            SMWDAAA103.Checked = false;
        }
        if (obj.SMWDAAA104.Equals("Y"))
        {
            SMWDAAA104.Checked = true;
        }
        else
        {
            SMWDAAA104.Checked = false;
        }
        if (obj.SMWDAAA105.Equals("Y"))
        {
            SMWDAAA105.Checked = true;
        }
        else
        {
            SMWDAAA105.Checked = false;
        }
        if (obj.SMWDAAA106.Equals("Y"))
        {
            SMWDAAA106.Checked = true;
        }
        else
        {
            SMWDAAA106.Checked = false;
        }
        if (obj.SMWDAAA107.Equals("Y"))
        {
            SMWDAAA107.Checked = true;
        }
        else
        {
            SMWDAAA107.Checked = false;
        }
        if (obj.SMWDAAA108.Equals("Y"))
        {
            SMWDAAA108.Checked = true;
        }
        else
        {
            SMWDAAA108.Checked = false;
        }
        if (obj.SMWDAAA109.Equals("Y"))
        {
            SMWDAAA109.Checked = true;
        }
        else
        {
            SMWDAAA109.Checked = false;
        }
        if (obj.SMWDAAA110.Equals("Y"))
        {
            SMWDAAA110.Checked = true;
        }
        else
        {
            SMWDAAA110.Checked = false;
        }
        if (obj.SMWDAAA111.Equals("Y"))
        {
            SMWDAAA111.Checked = true;
        }
        else
        {
            SMWDAAA111.Checked = false;
        }
        if (obj.SMWDAAA112.Equals("Y"))
        {
            SMWDAAA112.Checked = true;
        }
        else
        {
            SMWDAAA112.Checked = false;
        }
        if (obj.SMWDAAA113.Equals("Y"))
        {
            SMWDAAA113.Checked = true;
        }
        else
        {
            SMWDAAA113.Checked = false;
        }
        if (obj.SMWDAAA114.Equals("Y"))
        {
            SMWDAAA114.Checked = true;
        }
        else
        {
            SMWDAAA114.Checked = false;
        }
        if (obj.SMWDAAA115.Equals("Y"))
        {
            SMWDAAA115.Checked = true;
        }
        else
        {
            SMWDAAA115.Checked = false;
        }
        if (obj.SMWDAAA116.Equals("Y"))
        {
            SMWDAAA116.Checked = true;
        }
        else
        {
            SMWDAAA116.Checked = false;
        }
        if (obj.SMWDAAA117.Equals("Y"))
        {
            SMWDAAA117.Checked = true;
        }
        else
        {
            SMWDAAA117.Checked = false;
        }
        if (obj.SMWDAAA118.Equals("Y"))
        {
            SMWDAAA118.Checked = true;
        }
        else
        {
            SMWDAAA118.Checked = false;
        }
        if (obj.SMWDAAA119.Equals("Y"))
        {
            SMWDAAA119.Checked = true;
        }
        else
        {
            SMWDAAA119.Checked = false;
        }
        if (obj.SMWDAAA120.Equals("Y"))
        {
            SMWDAAA120.Checked = true;
        }
        else
        {
            SMWDAAA120.Checked = false;
        }
        if (obj.SMWDAAA121.Equals("Y"))
        {
            SMWDAAA121.Checked = true;
        }
        else
        {
            SMWDAAA121.Checked = false;
        }
        if (obj.SMWDAAA122.Equals("Y"))
        {
            SMWDAAA122.Checked = true;
        }
        else
        {
            SMWDAAA122.Checked = false;
        }
        if (obj.SMWDAAA151.Equals("Y"))
        {
            SMWDAAA151.Checked = true;
        }
        else
        {
            SMWDAAA151.Checked = false;
        }
        if (obj.SMWDAAA152.Equals("Y"))
        {
            SMWDAAA152.Checked = true;
        }
        else
        {
            SMWDAAA152.Checked = false;
        }
        if (obj.SMWDAAA153.Equals("Y"))
        {
            SMWDAAA153.Checked = true;
        }
        else
        {
            SMWDAAA153.Checked = false;
        }
        if (obj.SMWDAAA154.Equals("Y"))
        {
            SMWDAAA154.Checked = true;
        }
        else
        {
            SMWDAAA154.Checked = false;
        }
        if (obj.SMWDAAA155.Equals("Y"))
        {
            SMWDAAA155.Checked = true;
        }
        else
        {
            SMWDAAA155.Checked = false;
        }
        if (obj.SMWDAAA156.Equals("Y"))
        {
            SMWDAAA156.Checked = true;
        }
        else
        {
            SMWDAAA156.Checked = false;
        }

        SMWDAAA201.ValueText = obj.SMWDAAA201;
        SMWDAAA202.ValueText = obj.SMWDAAA202;
        SMWDAAA203.ValueText = obj.SMWDAAA203;
        SMWDAAA204.ValueText = obj.SMWDAAA204;
        SMWDAAA205.ValueText = obj.SMWDAAA205;
        SMWDAAA206.ValueText = obj.SMWDAAA206;
        SMWDAAA207.ValueText = obj.SMWDAAA207;
        SMWDAAA208.ValueText = obj.SMWDAAA208;
        SMWDAAA209.ValueText = obj.SMWDAAA209;
        SMWDAAA210.ValueText = obj.SMWDAAA210;
        SMWDAAA211.ValueText = obj.SMWDAAA211;
        SMWDAAA212.ValueText = obj.SMWDAAA212;
        SMWDAAA213.ValueText = obj.SMWDAAA213;
        SMWDAAA214.ValueText = obj.SMWDAAA214;
        SMWDAAA215.ValueText = obj.SMWDAAA215;
        SMWDAAA216.ValueText = obj.SMWDAAA216;
        SMWDAAA217.ValueText = obj.SMWDAAA217;
        SMWDAAA218.ValueText = obj.SMWDAAA218;
        SMWDAAA219.ValueText = obj.SMWDAAA219;
        SMWDAAA220.ValueText = obj.SMWDAAA220;
        SMWDAAA221.ValueText = obj.SMWDAAA221;
        SMWDAAA222.ValueText = obj.SMWDAAA222;
        SMWDAAA251.ValueText = obj.SMWDAAA251;
        SMWDAAA252.ValueText = obj.SMWDAAA252;
        SMWDAAA253.ValueText = obj.SMWDAAA253;
        SMWDAAA254.ValueText = obj.SMWDAAA254;
        SMWDAAA255.ValueText = obj.SMWDAAA255;
        SMWDAAA256.ValueText = obj.SMWDAAA256;

        if (obj.SMWDAAA300.Equals("Y"))
        {
            SMWDAAA300.Checked = true;
        }
        else
        {
            SMWDAAA300.Checked = false;
        }
        if (obj.SMWDAAA301.Equals("Y"))
        {
            SMWDAAA301.Checked = true;
        }
        else
        {
            SMWDAAA301.Checked = false;
        }
        if (obj.SMWDAAA302.Equals("Y"))
        {
            SMWDAAA302.Checked = true;
        }
        else
        {
            SMWDAAA302.Checked = false;
        }
        if (obj.SMWDAAA303.Equals("Y"))
        {
            SMWDAAA303.Checked = true;
        }
        else
        {
            SMWDAAA303.Checked = false;
        }
        if (obj.SMWDAAA304.Equals("Y"))
        {
            SMWDAAA304.Checked = true;
        }
        else
        {
            SMWDAAA304.Checked = false;
        }
        if (obj.SMWDAAA305.Equals("Y"))
        {
            SMWDAAA305.Checked = true;
        }
        else
        {
            SMWDAAA305.Checked = false;
        }
        if (obj.SMWDAAA306.Equals("Y"))
        {
            SMWDAAA306.Checked = true;
        }
        else
        {
            SMWDAAA306.Checked = false;
        }
        if (obj.SMWDAAA307.Equals("Y"))
        {
            SMWDAAA307.Checked = true;
        }
        else
        {
            SMWDAAA307.Checked = false;
        }
        if (obj.SMWDAAA308.Equals("Y"))
        {
            SMWDAAA308.Checked = true;
        }
        else
        {
            SMWDAAA308.Checked = false;
        }
        if (obj.SMWDAAA309.Equals("Y"))
        {
            SMWDAAA309.Checked = true;
        }
        else
        {
            SMWDAAA309.Checked = false;
        }
        if (obj.SMWDAAA310.Equals("Y"))
        {
            SMWDAAA310.Checked = true;
        }
        else
        {
            SMWDAAA310.Checked = false;
        }
        if (obj.SMWDAAA311.Equals("Y"))
        {
            SMWDAAA311.Checked = true;
        }
        else
        {
            SMWDAAA311.Checked = false;
        }
        if (obj.SMWDAAA312.Equals("Y"))
        {
            SMWDAAA312.Checked = true;
        }
        else
        {
            SMWDAAA312.Checked = false;
        }
        if (obj.SMWDAAA313.Equals("Y"))
        {
            SMWDAAA313.Checked = true;
        }
        else
        {
            SMWDAAA313.Checked = false;
        }
        if (obj.SMWDAAA314.Equals("Y"))
        {
            SMWDAAA314.Checked = true;
        }
        else
        {
            SMWDAAA314.Checked = false;
        }
        SMWDAAA315.ValueText = obj.SMWDAAA315;
        SMWDAAA316.ValueText = obj.SMWDAAA316;

        //-----------------
        if (obj.SMWDAAA321.Equals("Y"))
        {
            SMWDAAA321.Checked = true;
        }
        else
        {
            SMWDAAA321.Checked = false;
        }
        if (obj.SMWDAAA322.Equals("Y"))
        {
            SMWDAAA322.Checked = true;
        }
        else
        {
            SMWDAAA322.Checked = false;
        }
        if (obj.SMWDAAA323.Equals("Y"))
        {
            SMWDAAA323.Checked = true;
        }
        else
        {
            SMWDAAA323.Checked = false;
        }
        if (obj.SMWDAAA324.Equals("Y"))
        {
            SMWDAAA324.Checked = true;
        }
        else
        {
            SMWDAAA324.Checked = false;
        }
        if (obj.SMWDAAA325.Equals("Y"))
        {
            SMWDAAA325.Checked = true;
        }
        else
        {
            SMWDAAA325.Checked = false;
        }
        if (obj.SMWDAAA326.Equals("Y"))
        {
            SMWDAAA326.Checked = true;
        }
        else
        {
            SMWDAAA326.Checked = false;
        }
        if (obj.SMWDAAA327.Equals("Y"))
        {
            SMWDAAA327.Checked = true;
        }
        else
        {
            SMWDAAA327.Checked = false;
        }
        if (obj.SMWDAAA328.Equals("Y"))
        {
            SMWDAAA328.Checked = true;
        }
        else
        {
            SMWDAAA328.Checked = false;
        }
        if (obj.SMWDAAA329.Equals("Y"))
        {
            SMWDAAA329.Checked = true;
        }
        else
        {
            SMWDAAA329.Checked = false;
        }
        if (obj.SMWDAAA330.Equals("Y"))
        {
            SMWDAAA330.Checked = true;
        }
        else
        {
            SMWDAAA330.Checked = false;
        }
        if (obj.SMWDAAA331.Equals("Y"))
        {
            SMWDAAA331.Checked = true;
        }
        else
        {
            SMWDAAA331.Checked = false;
        }
        if (obj.SMWDAAA332.Equals("Y"))
        {
            SMWDAAA332.Checked = true;
        }
        else
        {
            SMWDAAA332.Checked = false;
        }
        if (obj.SMWDAAA333.Equals("Y"))
        {
            SMWDAAA333.Checked = true;
        }
        else
        {
            SMWDAAA333.Checked = false;
        }
        if (obj.SMWDAAA334.Equals("Y"))
        {
            SMWDAAA334.Checked = true;
        }
        else
        {
            SMWDAAA334.Checked = false;
        }
        if (obj.SMWDAAA335.Equals("Y"))
        {
            SMWDAAA335.Checked = true;
        }
        else
        {
            SMWDAAA335.Checked = false;
        }
        SMWDAAA451.ValueText = obj.SMWDAAA451;
        SMWDAAA452.ValueText = obj.SMWDAAA452;
        SMWDAAA453.ValueText = obj.SMWDAAA453;
        SMWDAAA454.ValueText = obj.SMWDAAA454;
        SMWDAAA455.ValueText = obj.SMWDAAA455;
        SMWDAAA551.ValueText = obj.SMWDAAA551;
        SMWDAAA552.ValueText = obj.SMWDAAA552;
        SMWDAAA553.ValueText = obj.SMWDAAA553;
        SMWDAAA554.ValueText = obj.SMWDAAA554;
        SMWDAAA555.ValueText = obj.SMWDAAA555;

        ListTable.HiddenField = new string[] { "SMWDAAB001", "SMWDAAB002" };
        ListTable.NoAdd = true;
        ListTable.NoDelete = true;
        ListTable.dataSource = obj.getChild("SMWDAAB");
        ListTable.updateTable();

        CTable.HiddenField = new string[] { "SMWDAAC001", "SMWDAAC002" };
        CTable.dataSource = obj.getChild("SMWDAAC");
        CTable.updateTable();

        ADList.HiddenField = new string[] { "SMWDAAD001", "SMWDAAD002" };
        ADList.dataSource = obj.getChild("SMWDAAD");
        ADList.updateTable();

    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        //國昌20110713 Grid無法儲存資料問題
        //SMWDAgent agent = new SMWDAgent();
        //agent.engine = engine;
        //agent.queryChild(objects);

        string sql = "";
        DataSet ds = null;
        string[,] ids = null;

        sql = "select SMWBAAA004 from SMWBAAA";
        ds = engine.getDataSet(sql, "TEMP");
        ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ids[i, 0];
        }
        SMWDAAA003.setListItem(ids);
        SMWDAAA025.setListItem(ids);

        //sql = "select SMWCAAA002 from SMWCAAA";
        //ds = engine.getDataSet(sql, "TEMP");
        //engine.close();

        //ids = new string[ds.Tables[0].Rows.Count, 2];
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
        //    ids[i, 1] = ids[i, 0];
        //}
        //SMWDAAA004.setListItem(ids);

        ids = new string[,]{
            {"Init",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids1", "流程發起")},
            {"Display",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids2", "流程處理")},
            {"New",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids3", "流程中新增")},
            {"Origin",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids4", "原稿/通知")},
            {"Default",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids5", "預設及顯示")}
        };
        SMWDAAA006.setListItem(ids);

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids6", "禁止功能")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids7", "可閱讀")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids8", "可修改")}
        };
        SMWDAAA008.setListItem(ids);

        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids9", "標準")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids10", "意見表達")}
        };
        SMWDAAA009.setListItem(ids);

        
        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids11", "關閉視窗")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids12", "顯示流程圖")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids13", "顯示填表畫面")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids22", "原稿模式顯示")}
        };       
        SMWDAAA018.setListItem(ids);

        ids = new string[,]{
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids14", "私人轉寄")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids15", "於流程中追蹤")},
            {"A",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids16", "兩者皆可")}
        };
        SMWDAAA022.setListItem(ids);

        
        ids = new string[,]{
            {"0",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids17", "回收件夾")},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids18", "顯示流程圖")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids19", "自動開啟收件夾第一筆資料簽核")}
        };
        
        /*
        ids = new string[,]{
            {"0","回收件夾"},
            {"1","顯示流程圖"}
        };
        */
        SMWDAAA031.setListItem(ids);

        ids = new string[,]{
            {"A",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids20", "昇冪")},
            {"D",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "ids21", "降冪")}
        };
        SMWDAAA315.setListItem(ids);
        SMWDAAA316.setListItem(ids);

        int fCount = 10;
        ids = new string[fCount, 2];
        for (int i = 1; i <= fCount; i++)
        {
            string tag = "EXT" + string.Format("{0:00}", i);
            ids[i-1, 0] = tag;
            ids[i-1, 1] = tag;
        }
        SMWDAAC003.setListItem(ids);

        
        SMWDAAA004.clientEngineType = engineType;
        SMWDAAA004.connectDBString = connectString;
        SMWDAAA005.clientEngineType = engineType;
        SMWDAAA005.connectDBString = connectString;
        SMWDAAA010.clientEngineType = engineType;
        SMWDAAA010.connectDBString = connectString;
        SMWDAAA012.clientEngineType = engineType;
        SMWDAAA012.connectDBString = connectString;
        SMWDAAA016.clientEngineType = engineType;
        SMWDAAA016.connectDBString = connectString;


        SMWDAAA obj = (SMWDAAA)objects;

        bool isAddNew = (bool)getSession("isNew");

        DataObjectSet child = null;
        DataObjectSet childc = null;
        DataObjectSet childd = null;
        if (isAddNew)
        {
            child = new DataObjectSet();
            child.setAssemblyName("WebServerProject");
            child.setChildClassString("WebServerProject.flow.SMWD.SMWDAAB");
            child.setTableName("SMWDAAB");
            obj.setChild("SMWDAAB", child);

            childc = new DataObjectSet();
            childc.setAssemblyName("WebServerProject");
            childc.setChildClassString("WebServerProject.flow.SMWD.SMWDAAC");
            childc.setTableName("SMWDAAC");
            obj.setChild("SMWDAAC", childc);

            childd = new DataObjectSet();
            childd.setAssemblyName("WebServerProject");
            childd.setChildClassString("WebServerProject.flow.SMWD.SMWDAAD");
            childd.setTableName("SMWDAAD");
            obj.setChild("SMWDAAD", childd);

        }

        ADList.NoAdd = true;
        ADList.NoModify = true;

        showScreen(obj);

        if (!isAddNew)
        {
            setAC004();
        }

        //SMWDAAA005.CertificateMode = true;
    }
    protected override void saveData(DataObject objects)
    {
        if (SMWDAAA002.ValueText.Trim().Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError1", "必須填寫關聯名稱"));
        }
        if (SMWDAAA005.GuidValueText.Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError2", "必須選擇作業畫面"));
        }
        //檢查權限項目
        if (SMWDAAA016.GuidValueText.Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError3", "必須填寫權限項目"));
        }
        //檢查AgentSchema
        if (SMWDAAA013.ValueText.Equals(""))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError4", "必須填寫AgentSchema"));
        }

        if (SMWDAAA006.ValueText.Equals("Init"))
        {
            if (SMWDAAA011.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError5", "必須填寫主旨"));
            }
            //if (SMWDAAA012.GuidValueText.Equals(""))
            //{
            //    throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError6", "必須選擇表單單號格式"));
            //}
            if (SMWDAAA014.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError7", "必須填寫設定流程畫面"));
            }

        }
        else if (SMWDAAA006.ValueText.Equals("Origin"))
        {
            if (SMWDAAA020.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError8", "必須填寫轉寄設定畫面"));
            }
            if (SMWDAAA021.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError9", "必須填寫轉派設定畫面"));
            }
        }
        else
        {
            if (SMWDAAA010.GuidValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError10", "必須選擇意見表達設定"));
            }
            if (SMWDAAA015.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError11", "必須填寫加簽畫面"));
            }
            if (SMWDAAA019.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError12", "必須填寫重辦設定畫面"));
            }
            if (SMWDAAA020.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError13", "必須填寫轉寄設定畫面"));
            }
            if (SMWDAAA021.ValueText.Equals(""))
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError14", "必須填寫轉派設定畫面"));
            }

        }
        try
        {
            decimal tt = decimal.Parse(SMWDAAA033.ValueText);
            if (tt < 0)
            {
                throw new Exception("");
            }
        }
        catch
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError15", "草稿筆數限制必須大於等於0"));
        }
        //檢查是否掛載了已被掛載的流程
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string cnt = string.Empty;
        bool isProcessRepeat = false;
        try
        {
            string sql = string.Empty;
            if ("Origin,Init,Default".Contains(SMWDAAA006.ValueText))
            {
                //預設、原稿或發起直接檢查對應SMWDAAA003與SMWDAAA006
                sql = "select count(*) from SMWDAAA where SMWDAAA003 = '" + SMWDAAA003.ValueText + "' and SMWDAAA006 = '" + SMWDAAA006.ValueText + "'";
                isProcessRepeat = true;
            }
            else
            {
                //其餘檢查SMWDAAA003與SMWDAAA004並排除SMWDAAA006不應該為Default
                sql = "select count(*) from SMWDAAA where SMWDAAA003 = '" + SMWDAAA003.ValueText + "' and SMWDAAA004 = '" + SMWDAAA004.ValueText + "' and SMWDAAA006 not in ('Default','Origin','Init')";
                isProcessRepeat = false;
            }
            //若為修改則須排除自己於檢測範圍中
            if(!(bool)getSession("isNew"))
            {
                sql += " and SMWDAAA001 <> '" + ((SMWDAAA)objects).SMWDAAA001 + "'";
            }
            cnt = (string)engine.executeScalar(sql).ToString();
        }
        catch
        {
            throw new com.dsc.exception.SQLEngineException();
        }
        finally
        {
            engine.close();
        }
        if (!cnt.Equals("0"))
        {
            if (isProcessRepeat)
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError24", "該流程已被其他作業掛載，請選擇無掛載之流程"));
            }
            else
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError25", "該流程定義、流程角色之設定已存在，請選擇其他流程或角色"));
            }
        }

        SMWDAAA obj = (SMWDAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMWDAAA001 = IDProcessor.getID("");
        }

        obj.SMWDAAA002 = SMWDAAA002.ValueText;
        obj.SMWDAAA003 = SMWDAAA003.ValueText;
        obj.SMWDAAA004 = SMWDAAA004.ValueText;
        obj.SMWDAAA005 = SMWDAAA005.GuidValueText;
        obj.SMWDAAA006 = SMWDAAA006.ValueText;
        if (SMWDAAA007.Checked)
        {
            obj.SMWDAAA007 = "Y";
        }
        else
        {
            obj.SMWDAAA007 = "N";
        }
        obj.SMWDAAA008 = SMWDAAA008.ValueText;
        obj.SMWDAAA009 = SMWDAAA009.ValueText;
        obj.SMWDAAA010 = SMWDAAA010.GuidValueText;
        obj.SMWDAAA011 = SMWDAAA011.ValueText;
        obj.SMWDAAA012 = SMWDAAA012.GuidValueText;
        obj.SMWDAAA013 = SMWDAAA013.ValueText;
        obj.SMWDAAA014 = SMWDAAA014.ValueText;
        obj.SMWDAAA015 = SMWDAAA015.ValueText;
        obj.SMWDAAA016 = SMWDAAA016.ValueText;
        if (SMWDAAA017.Checked)
        {
            obj.SMWDAAA017 = "Y";
        }
        else
        {
            obj.SMWDAAA017 = "N";
        }
        obj.SMWDAAA018 = SMWDAAA018.ValueText;
        obj.SMWDAAA019 = SMWDAAA019.ValueText;
        obj.SMWDAAA020 = SMWDAAA020.ValueText;
        obj.SMWDAAA021 = SMWDAAA021.ValueText;
        obj.SMWDAAA022 = SMWDAAA022.ValueText;
        if (SMWDAAA023.Checked)
        {
            obj.SMWDAAA023 = "Y";
        }
        else
        {
            obj.SMWDAAA023 = "N";
        }
        if (SMWDAAA024.Checked)
        {
            obj.SMWDAAA024 = "Y";
        }
        else
        {
            obj.SMWDAAA024 = "N";
        }
        obj.SMWDAAA025 = SMWDAAA025.ValueText;
        obj.SMWDAAA026 = SMWDAAA026.ValueText;
        obj.SMWDAAA027 = SMWDAAA027.ValueText;
        obj.SMWDAAA028 = SMWDAAA028.ValueText;
        obj.SMWDAAA029 = SMWDAAA029.ValueText;
        if (SMWDAAA030.Checked)
        {
            obj.SMWDAAA030 = "Y";
        }
        else
        {
            obj.SMWDAAA030 = "N";
        }
        obj.SMWDAAA031 = SMWDAAA031.ValueText;
        if (SMWDAAA032.Checked)
        {
            obj.SMWDAAA032 = "Y";
        }
        else
        {
            obj.SMWDAAA032 = "N";
        }
        obj.SMWDAAA033 = SMWDAAA033.ValueText;
        if (SMWDAAA034.Checked)
        {
            obj.SMWDAAA034 = "Y";
        }
        else
        {
            obj.SMWDAAA034 = "N";
        }

        obj.SMWDAAA051 = SMWDAAA051.ValueText;
        obj.SMWDAAA052 = SMWDAAA052.ValueText;
        obj.SMWDAAA053 = SMWDAAA053.ValueText;
        obj.SMWDAAA054 = SMWDAAA054.ValueText;
        obj.SMWDAAA055 = SMWDAAA055.ValueText;
        obj.SMWDAAA056 = SMWDAAA056.ValueText;
        if (SMWDAAA057.Checked)
        {
            obj.SMWDAAA057 = "Y";
        }
        else
        {
            obj.SMWDAAA057 = "N";
        }

        if (SMWDAAA101.Checked)
        {
            obj.SMWDAAA101 = "Y";
        }
        else
        {
            obj.SMWDAAA101 = "N";
        }
        if (SMWDAAA102.Checked)
        {
            obj.SMWDAAA102 = "Y";
        }
        else
        {
            obj.SMWDAAA102 = "N";
        }
        if (SMWDAAA103.Checked)
        {
            obj.SMWDAAA103 = "Y";
        }
        else
        {
            obj.SMWDAAA103 = "N";
        }
        if (SMWDAAA104.Checked)
        {
            obj.SMWDAAA104 = "Y";
        }
        else
        {
            obj.SMWDAAA104 = "N";
        }
        if (SMWDAAA105.Checked)
        {
            obj.SMWDAAA105 = "Y";
        }
        else
        {
            obj.SMWDAAA105 = "N";
        }
        if (SMWDAAA106.Checked)
        {
            obj.SMWDAAA106 = "Y";
        }
        else
        {
            obj.SMWDAAA106 = "N";
        }
        if (SMWDAAA107.Checked)
        {
            obj.SMWDAAA107 = "Y";
        }
        else
        {
            obj.SMWDAAA107 = "N";
        }
        if (SMWDAAA108.Checked)
        {
            obj.SMWDAAA108 = "Y";
        }
        else
        {
            obj.SMWDAAA108 = "N";
        }
        if (SMWDAAA109.Checked)
        {
            obj.SMWDAAA109 = "Y";
        }
        else
        {
            obj.SMWDAAA109 = "N";
        }
        if (SMWDAAA110.Checked)
        {
            obj.SMWDAAA110 = "Y";
        }
        else
        {
            obj.SMWDAAA110 = "N";
        }
        if (SMWDAAA111.Checked)
        {
            obj.SMWDAAA111 = "Y";
        }
        else
        {
            obj.SMWDAAA111 = "N";
        }
        if (SMWDAAA112.Checked)
        {
            obj.SMWDAAA112 = "Y";
        }
        else
        {
            obj.SMWDAAA112 = "N";
        }
        if (SMWDAAA113.Checked)
        {
            obj.SMWDAAA113 = "Y";
        }
        else
        {
            obj.SMWDAAA113 = "N";
        }
        if (SMWDAAA114.Checked)
        {
            obj.SMWDAAA114 = "Y";
        }
        else
        {
            obj.SMWDAAA114 = "N";
        }
        if (SMWDAAA115.Checked)
        {
            obj.SMWDAAA115 = "Y";
        }
        else
        {
            obj.SMWDAAA115 = "N";
        }
        if (SMWDAAA116.Checked)
        {
            obj.SMWDAAA116 = "Y";
        }
        else
        {
            obj.SMWDAAA116 = "N";
        }
        if (SMWDAAA117.Checked)
        {
            obj.SMWDAAA117 = "Y";
        }
        else
        {
            obj.SMWDAAA117 = "N";
        }
        if (SMWDAAA118.Checked)
        {
            obj.SMWDAAA118 = "Y";
        }
        else
        {
            obj.SMWDAAA118 = "N";
        }
        if (SMWDAAA119.Checked)
        {
            obj.SMWDAAA119 = "Y";
        }
        else
        {
            obj.SMWDAAA119 = "N";
        }
        if (SMWDAAA120.Checked)
        {
            obj.SMWDAAA120 = "Y";
        }
        else
        {
            obj.SMWDAAA120 = "N";
        }
        if (SMWDAAA121.Checked)
        {
            obj.SMWDAAA121 = "Y";
        }
        else
        {
            obj.SMWDAAA121 = "N";
        }
        if (SMWDAAA122.Checked)
        {
            obj.SMWDAAA122 = "Y";
        }
        else
        {
            obj.SMWDAAA122 = "N";
        }
        if (SMWDAAA151.Checked)
        {
            obj.SMWDAAA151 = "Y";
        }
        else
        {
            obj.SMWDAAA151 = "N";
        }
        if (SMWDAAA152.Checked)
        {
            obj.SMWDAAA152 = "Y";
        }
        else
        {
            obj.SMWDAAA152 = "N";
        }
        if (SMWDAAA153.Checked)
        {
            obj.SMWDAAA153 = "Y";
        }
        else
        {
            obj.SMWDAAA153 = "N";
        }
        if (SMWDAAA154.Checked)
        {
            obj.SMWDAAA154 = "Y";
        }
        else
        {
            obj.SMWDAAA154 = "N";
        }
        if (SMWDAAA155.Checked)
        {
            obj.SMWDAAA155 = "Y";
        }
        else
        {
            obj.SMWDAAA155 = "N";
        }
        if (SMWDAAA156.Checked)
        {
            obj.SMWDAAA156 = "Y";
        }
        else
        {
            obj.SMWDAAA156 = "N";
        }

        obj.SMWDAAA201 = SMWDAAA201.ValueText;
        obj.SMWDAAA202 = SMWDAAA202.ValueText;
        obj.SMWDAAA203 = SMWDAAA203.ValueText;
        obj.SMWDAAA204 = SMWDAAA204.ValueText;
        obj.SMWDAAA205 = SMWDAAA205.ValueText;
        obj.SMWDAAA206 = SMWDAAA206.ValueText;
        obj.SMWDAAA207 = SMWDAAA207.ValueText;
        obj.SMWDAAA208 = SMWDAAA208.ValueText;
        obj.SMWDAAA209 = SMWDAAA209.ValueText;
        obj.SMWDAAA210 = SMWDAAA210.ValueText;
        obj.SMWDAAA211 = SMWDAAA211.ValueText;
        obj.SMWDAAA212 = SMWDAAA212.ValueText;
        obj.SMWDAAA213 = SMWDAAA213.ValueText;
        obj.SMWDAAA214 = SMWDAAA214.ValueText;
        obj.SMWDAAA215 = SMWDAAA215.ValueText;
        obj.SMWDAAA216 = SMWDAAA216.ValueText;
        obj.SMWDAAA217 = SMWDAAA217.ValueText;
        obj.SMWDAAA218 = SMWDAAA218.ValueText;
        obj.SMWDAAA219 = SMWDAAA219.ValueText;
        obj.SMWDAAA220 = SMWDAAA220.ValueText;
        obj.SMWDAAA221 = SMWDAAA221.ValueText;
        obj.SMWDAAA222 = SMWDAAA222.ValueText;
        obj.SMWDAAA251 = SMWDAAA251.ValueText;
        obj.SMWDAAA252 = SMWDAAA252.ValueText;
        obj.SMWDAAA253 = SMWDAAA253.ValueText;
        obj.SMWDAAA254 = SMWDAAA254.ValueText;
        obj.SMWDAAA255 = SMWDAAA255.ValueText;
        obj.SMWDAAA256 = SMWDAAA256.ValueText;

        if (SMWDAAA300.Checked)
        {
            obj.SMWDAAA300 = "Y";
        }
        else
        {
            obj.SMWDAAA300 = "N";
        }
        if (SMWDAAA301.Checked)
        {
            obj.SMWDAAA301 = "Y";
        }
        else
        {
            obj.SMWDAAA301 = "N";
        }
        if (SMWDAAA302.Checked)
        {
            obj.SMWDAAA302 = "Y";
        }
        else
        {
            obj.SMWDAAA302 = "N";
        }
        if (SMWDAAA303.Checked)
        {
            obj.SMWDAAA303 = "Y";
        }
        else
        {
            obj.SMWDAAA303 = "N";
        }
        if (SMWDAAA304.Checked)
        {
            obj.SMWDAAA304 = "Y";
        }
        else
        {
            obj.SMWDAAA304 = "N";
        }
        if (SMWDAAA305.Checked)
        {
            obj.SMWDAAA305 = "Y";
        }
        else
        {
            obj.SMWDAAA305 = "N";
        }
        if (SMWDAAA306.Checked)
        {
            obj.SMWDAAA306 = "Y";
        }
        else
        {
            obj.SMWDAAA306 = "N";
        }
        if (SMWDAAA307.Checked)
        {
            obj.SMWDAAA307 = "Y";
        }
        else
        {
            obj.SMWDAAA307 = "N";
        }
        if (SMWDAAA308.Checked)
        {
            obj.SMWDAAA308 = "Y";
        }
        else
        {
            obj.SMWDAAA308 = "N";
        }
        if (SMWDAAA309.Checked)
        {
            obj.SMWDAAA309 = "Y";
        }
        else
        {
            obj.SMWDAAA309 = "N";
        }
        if (SMWDAAA310.Checked)
        {
            obj.SMWDAAA310 = "Y";
        }
        else
        {
            obj.SMWDAAA310 = "N";
        }
        if (SMWDAAA311.Checked)
        {
            obj.SMWDAAA311 = "Y";
        }
        else
        {
            obj.SMWDAAA311 = "N";
        }
        if (SMWDAAA312.Checked)
        {
            obj.SMWDAAA312 = "Y";
        }
        else
        {
            obj.SMWDAAA312 = "N";
        }
        if (SMWDAAA313.Checked)
        {
            obj.SMWDAAA313 = "Y";
        }
        else
        {
            obj.SMWDAAA313 = "N";
        }
        if (SMWDAAA314.Checked)
        {
            obj.SMWDAAA314 = "Y";
        }
        else
        {
            obj.SMWDAAA314 = "N";
        }
        obj.SMWDAAA315 = SMWDAAA315.ValueText;
        obj.SMWDAAA316 = SMWDAAA316.ValueText;

        if (SMWDAAA321.Checked)
        {
            obj.SMWDAAA321 = "Y";
        }
        else
        {
            obj.SMWDAAA321 = "N";
        }
        if (SMWDAAA322.Checked)
        {
            obj.SMWDAAA322 = "Y";
        }
        else
        {
            obj.SMWDAAA322 = "N";
        }
        if (SMWDAAA323.Checked)
        {
            obj.SMWDAAA323 = "Y";
        }
        else
        {
            obj.SMWDAAA323 = "N";
        }
        if (SMWDAAA324.Checked)
        {
            obj.SMWDAAA324 = "Y";
        }
        else
        {
            obj.SMWDAAA324 = "N";
        }
        if (SMWDAAA325.Checked)
        {
            obj.SMWDAAA325 = "Y";
        }
        else
        {
            obj.SMWDAAA325 = "N";
        }
        if (SMWDAAA326.Checked)
        {
            obj.SMWDAAA326 = "Y";
        }
        else
        {
            obj.SMWDAAA326 = "N";
        }
        if (SMWDAAA327.Checked)
        {
            obj.SMWDAAA327 = "Y";
        }
        else
        {
            obj.SMWDAAA327 = "N";
        }
        if (SMWDAAA328.Checked)
        {
            obj.SMWDAAA328 = "Y";
        }
        else
        {
            obj.SMWDAAA328 = "N";
        }
        if (SMWDAAA329.Checked)
        {
            obj.SMWDAAA329 = "Y";
        }
        else
        {
            obj.SMWDAAA329 = "N";
        }
        if (SMWDAAA330.Checked)
        {
            obj.SMWDAAA330 = "Y";
        }
        else
        {
            obj.SMWDAAA330 = "N";
        }
        if (SMWDAAA331.Checked)
        {
            obj.SMWDAAA331 = "Y";
        }
        else
        {
            obj.SMWDAAA331 = "N";
        }
        if (SMWDAAA332.Checked)
        {
            obj.SMWDAAA332 = "Y";
        }
        else
        {
            obj.SMWDAAA332 = "N";
        }
        if (SMWDAAA333.Checked)
        {
            obj.SMWDAAA333 = "Y";
        }
        else
        {
            obj.SMWDAAA333 = "N";
        }
        if (SMWDAAA334.Checked)
        {
            obj.SMWDAAA334 = "Y";
        }
        else
        {
            obj.SMWDAAA334 = "N";
        }
        if (SMWDAAA335.Checked)
        {
            obj.SMWDAAA335 = "Y";
        }
        else
        {
            obj.SMWDAAA335 = "N";
        }

        obj.SMWDAAA451 = SMWDAAA451.ValueText;
        obj.SMWDAAA452 = SMWDAAA452.ValueText;
        obj.SMWDAAA453 = SMWDAAA453.ValueText;
        obj.SMWDAAA454 = SMWDAAA454.ValueText;
        obj.SMWDAAA455 = SMWDAAA455.ValueText;
        obj.SMWDAAA551 = SMWDAAA551.ValueText;
        obj.SMWDAAA552 = SMWDAAA552.ValueText;
        obj.SMWDAAA553 = SMWDAAA553.ValueText;
        obj.SMWDAAA554 = SMWDAAA554.ValueText;
        obj.SMWDAAA555 = SMWDAAA555.ValueText;


        DataObjectSet child = ListTable.dataSource;
        for (int i = 0; i < child.getAvailableDataObjectCount(); i++)
        {
            SMWDAAB ab = (SMWDAAB)child.getAvailableDataObject(i);
            ab.SMWDAAB002 = obj.SMWDAAA001;
        }
        DataObjectSet childc = CTable.dataSource;
        for (int i = 0; i < childc.getAvailableDataObjectCount(); i++)
        {
            SMWDAAC ab = (SMWDAAC)childc.getAvailableDataObject(i);
            ab.SMWDAAC002 = obj.SMWDAAA001;
        }
        DataObjectSet childd = ADList.dataSource;
        for (int i = 0; i < childd.getAvailableDataObjectCount(); i++)
        {
            SMWDAAD ab = (SMWDAAD)childd.getAvailableDataObject(i);
            ab.SMWDAAD002 = obj.SMWDAAA001;
        }
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMWDAgent agent = new SMWDAgent();
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
    protected void AnalyzeButton_Click(object sender, EventArgs e)
    {
        if (SMWDAAA005.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError16", "請填入作業畫面代號"));
            return;
        }
        if (SMWDAAA013.ValueText.Equals(""))
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError17", "請填入AgentSchema"));
            return;
        }
        AbstractEngine engine = null;
        try
        {
            //取得作業畫面URL
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string sql = "select SMWAAAA005 from SMWAAAA where SMWAAAA001='" + Utility.filter(SMWDAAA005.GuidValueText) + "'";
            DataSet dds = engine.getDataSet(sql, "TEMP");
            string iPath="";
            if (dds.Tables[0].Rows.Count > 0)
            {
                iPath = dds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError18", "找不到此作業畫面的URL"));
            }
            string curPath = Server.MapPath("~/");
            curPath += iPath;

            System.IO.StreamReader sr = new System.IO.StreamReader(curPath);
            string html = sr.ReadToEnd();
            sr.Close();

            ArrayList ary = new ArrayList();

            MIL.Html.HtmlDocument doc = MIL.Html.HtmlDocument.Create(html, false);
            fetchChild(doc.Nodes, ary, "");

            DataObjectSet dos = ListTable.dataSource;
            //for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            //{
            //    dos.getAvailableDataObject(i).delete();
            //}

            for (int i = 0; i < ary.Count; i++)
            {
                string[] data = (string[])ary[i];

                bool hasFound = false;
                SMWDAAB tab = null;
                for (int j = 0; j < dos.getAvailableDataObjectCount(); j++)
                {
                    SMWDAAB tp = (SMWDAAB)dos.getAvailableDataObject(j);
                    if (tp.SMWDAAB003.Equals(data[1]))
                    {
                        hasFound = true;
                        tab = tp;
                        break;
                    }
                }
                if (!hasFound)
                {
                    SMWDAAB ab = (SMWDAAB)dos.create();
                    ab.SMWDAAB001 = IDProcessor.getID("");
                    ab.SMWDAAB002 = "temp";
                    ab.SMWDAAB003 = data[1];
                    ab.SMWDAAB004 = data[0];
                    ab.SMWDAAB005 = "Y";
                    ab.SMWDAAB006 = "N";
                    ab.SMWDAAB007 = "N";
                    ab.SMWDAAB008 = "N";
                    dos.add(ab);
                }
                else
                {
                    tab.SMWDAAB003 = data[1];
                    tab.SMWDAAB004 = data[0];
                }
            }

            for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
            {
                SMWDAAB ab = (SMWDAAB)dos.getAvailableDataObject(i);
                bool hasFound = false;
                for (int j = 0; j < ary.Count; j++)
                {
                    string[] data = (string[])ary[j];

                    if (data[1].Equals(ab.SMWDAAB003))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    ab.delete();
                }
            }
            ListTable.dataSource = dos;
            ListTable.updateTable();

            setAC004();

            engine.close();
        }
        catch (Exception te)
        {
            try
            {
                engine.close();
            }
            catch { };
            Response.Write(te.Message);
        }
    }
    private void setAC004()
    {
        //解析AgentSchema
        string[] tgs = SMWDAAA013.ValueText.Split(new char[] { '.' });
        string fp = (string)GlobalCache.getValue("DataObjectFolder");
        for (int i = 0; i < (tgs.Length - 1); i++)
        {
            fp += tgs[i] + "\\";
        }
        fp +=SMWDAAA013.ValueText + ".xml";

        XMLProcessor xp = new XMLProcessor(fp, 0);
        //取得單頭資料物件
        XmlNode xnh = xp.selectSingleNode(@"/DOS");
        string childS = xnh.Attributes["ChildClassString"].Value;

        fp = (string)GlobalCache.getValue("DataObjectFolder");
        tgs = childS.Split(new char[] { '.' });
        for (int i = 0; i < (tgs.Length - 1); i++)
        {
            fp += tgs[i] + "\\";
        }
        fp += childS + ".xml";

        xp = new XMLProcessor(fp, 0);
        XmlNode xn = xp.selectSingleNode("/DataObject");


        //欄位清單

        XmlNodeList it = xp.selectAllNodes("//DataObject/fieldDefinition/field");
        string[,] ids = new string[it.Count, 2];
        int ccs = 0;
        foreach (XmlNode xts in it)
        {
            ids[ccs, 0] = xts.Attributes["dataField"].Value;
            ids[ccs, 1] = ids[ccs, 0];
            ccs++;
        }
        SMWDAAC004.setListItem(ids);
    }
    private void fetchChild(MIL.Html.HtmlNodeCollection xnl, ArrayList ary, string prefix)
    {
        MIL.Html.HtmlElement o = new MIL.Html.HtmlElement("HTML");
        foreach (MIL.Html.HtmlNode xn in xnl)
        {
            
            if (xn.GetType().Equals(o.GetType()))
            {
                MIL.Html.HtmlElement he = (MIL.Html.HtmlElement)xn;
                if (prefix.Equals(""))
                {
                    if (he.Name.Equals("%@"))
                    {
                        if (he.Attributes["Namespace"] != null)
                        {
                            if (he.Attributes["Namespace"].Value.Equals("DSCWebControl"))
                            {
                                prefix = he.Attributes["TagPrefix"].Value;
                            }
                        }
                    }
                }
                string tagname = he.Name;
                if (!prefix.Equals(""))
                {
                    if (tagname.IndexOf(prefix) == 0)
                    {
                        string elementName = tagname.Split(new char[] { ':' })[1];
                        string IDs = he.Attributes["ID"].Value;
                        ary.Add(new string[] { elementName, IDs });
                    }
                }
                fetchChild(((MIL.Html.HtmlElement)xn).Nodes, ary, prefix);
            }
            else
            {
                //sw.WriteLine("NotHTML:" + xn.HTML);
            }
        }
    }
    protected void ListTable_ShowRowData(DataObject objects)
    {
        SMWDAAB ab = (SMWDAAB)objects;

        SMWDAAB003.ValueText = ab.SMWDAAB003;
        SMWDAAB004.ValueText = ab.SMWDAAB004;

        if (ab.SMWDAAB005.Equals("Y"))
        {
            SMWDAAB005.Checked = true;
        }
        else
        {
            SMWDAAB005.Checked = false;
        }
        if (ab.SMWDAAB006.Equals("Y"))
        {
            SMWDAAB006.Checked = true;
        }
        else
        {
            SMWDAAB006.Checked = false;
        }
        if (ab.SMWDAAB007.Equals("Y"))
        {
            SMWDAAB007.Checked = true;
        }
        else
        {
            SMWDAAB007.Checked = false;
        }
        if (ab.SMWDAAB008.Equals("Y"))
        {
            SMWDAAB008.Checked = true;
        }
        else
        {
            SMWDAAB008.Checked = false;
        }

    }
    protected bool ListTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWDAAB ab = (SMWDAAB)objects;

        if (SMWDAAB005.Checked)
        {
            ab.SMWDAAB005 = "Y";
        }
        else
        {
            ab.SMWDAAB005 = "N";
        }
        if (SMWDAAB006.Checked)
        {
            ab.SMWDAAB006 = "Y";
        }
        else
        {
            ab.SMWDAAB006 = "N";
        }
        if (SMWDAAB007.Checked)
        {
            ab.SMWDAAB007 = "Y";
        }
        else
        {
            ab.SMWDAAB007 = "N";
        }
        if (SMWDAAB008.Checked)
        {
            ab.SMWDAAB008 = "Y";
        }
        else
        {
            ab.SMWDAAB008 = "N";
        }


        return true;
    }
    protected bool CTable_SaveRowData(DataObject objects, bool isNew)
    {
        SMWDAAC ac = (SMWDAAC)objects;
        if (isNew)
        {
            ac.SMWDAAC001 = IDProcessor.getID("");
            ac.SMWDAAC002 = "temp";
        }
        ac.SMWDAAC003 = SMWDAAC003.ValueText;
        ac.SMWDAAC004 = SMWDAAC004.ValueText;
        ac.SMWDAAC005 = SMWDAAC005.ValueText;

        return true;
    }
    protected void CTable_ShowRowData(DataObject objects)
    {
        SMWDAAC ac=(SMWDAAC)objects;
        SMWDAAC003.ValueText = ac.SMWDAAC003;
        SMWDAAC004.ValueText = ac.SMWDAAC004;
        SMWDAAC005.ValueText = ac.SMWDAAC005;
    }
    protected void SMWDAAA005_SingleOpenWindowButtonClick(string[,] values)
    {
        DataObjectSet dos = ListTable.dataSource;
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            dos.getAvailableDataObject(i).delete();
        }
        ListTable.dataSource = dos;
        ListTable.updateTable();

        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError19", "請按下解析欄位鍵重新設定畫面欄位顯示設定"));
        
    }
    protected void SMWDAAA013_TextChanged(object sender, EventArgs e)
    {
        DataObjectSet dos = CTable.dataSource;
        for (int i = 0; i < dos.getAvailableDataObjectCount(); i++)
        {
            dos.getAvailableDataObject(i).delete();
        }
        CTable.dataSource = dos;
        CTable.updateTable();

        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError20", "請按下解析欄位鍵重新設定待辦檢視資料夾設定"));

    }
    protected void HelpButton_Click(object sender, EventArgs e)
    {
        string str = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str1", "主旨變數格式: #FieldName# 或者是 %FixVar% 或者是 @UIField@")+"\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str2", "#FieldName#: 為此畫面單頭物件資料欄位名稱") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str3", "%FixVar%: 共分下列幾種固定型態:") + " \\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str4", "    1. %UIStatus% :新增 修改 刪除") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str5", "    2. %UID% :使用者代號") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str6", "    3. %UNAME% :使用者姓名") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str7", "@UIField@: 為畫面欄位, 僅能為[一般文字欄位],[單選開窗欄位],[下拉選單欄位],[日期欄位]:") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str8", "    1. 若為[一般文字欄位], 則鍵入@欄位名稱@即可") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str9", "    2. 若為[單選開窗欄位], 則鍵入@欄位名稱_ID@, 表示替換可輸入欄位部份, ") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str10", "       @欄位名稱_Name@, 表示替換不可輸入部分") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str11", "    3. 若為[下拉選單欄位], 則鍵入@欄位名稱_ID@, 表示替換資料值部份, ") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str12", "       @欄位名稱_Name@, 表示替換顯示值部分") + "\\n";
        str += com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "str13", "    4. 若為[日期欄位], 則鍵入@欄位名稱@即可") + "\\n";

        MessageBox(str);
    }
    protected void SMWDAAA006_SelectChanged(string value)
    {
        if (value.Equals("Init"))
        {
            //主要顯示畫面
            SMWDAAA007.Checked = true;
            SMWDAAA007.ReadOnly = true;

            //意見表達
            SMWDAAA009.ReadOnly = true;

            //意見表達類型
            SMWDAAA010.GuidValueText = "";
            SMWDAAA010.doGUIDValidate();
            SMWDAAA010.ReadOnly = true;

            //主旨
            SMWDAAA011.ReadOnly = false;

            //表單單號
            SMWDAAA012.ReadOnly = false;

            //設定流程畫面
            SMWDAAA014.ReadOnly = false;

            //加簽畫面
            SMWDAAA015.ReadOnly = true;

            //重辦畫面
            SMWDAAA019.ReadOnly = true;

            //轉寄畫面
            SMWDAAA020.ReadOnly = true;

            //轉派畫面
            SMWDAAA021.ReadOnly = true;
            
            //顯示簽核意見
            SMWDAAA017.Checked = false;
            SMWDAAA017.ReadOnly = true;

            //送單後控制
            SMWDAAA018.ReadOnly = false;

            //簽核後控制
            SMWDAAA031.ReadOnly = true;

            //參考流程畫面
            SMWDAAA026.ReadOnly = true;

            //簽核畫面
            SMWDAAA027.ReadOnly = true;

            //撤銷畫面
            SMWDAAA028.ReadOnly = true;

            //取回重辦畫面
            SMWDAAA029.ReadOnly = true;
        }
        else if (value.Equals("Origin"))
        {
            //主要顯示畫面
            SMWDAAA007.Checked = true;
            SMWDAAA007.ReadOnly = true;

            //意見表達
            SMWDAAA009.ReadOnly = true;

            //意見表達類型
            SMWDAAA010.GuidValueText = "";
            SMWDAAA010.doGUIDValidate();
            SMWDAAA010.ReadOnly = true;

            //主旨
            SMWDAAA011.ReadOnly = true;

            //表單單號
            SMWDAAA012.GuidValueText = "";
            SMWDAAA012.doGUIDValidate();
            SMWDAAA012.ReadOnly = true;

            //設定流程畫面
            SMWDAAA014.ReadOnly = true;

            //加簽畫面
            SMWDAAA015.ReadOnly = true;

            //重辦畫面
            SMWDAAA019.ReadOnly = true;

            //轉寄畫面
            SMWDAAA020.ReadOnly = false;

            //轉派畫面
            SMWDAAA021.ReadOnly = false;

            //顯示簽核意見
            SMWDAAA017.ReadOnly = false;

            //送單後控制
            SMWDAAA018.ReadOnly = true;

            //簽核後控制
            SMWDAAA031.ReadOnly = true;

            //參考流程畫面
            SMWDAAA026.ReadOnly = true;

            //簽核畫面
            SMWDAAA027.ReadOnly = true;

            //撤銷畫面
            SMWDAAA028.ReadOnly = true;

            //取回重辦畫面
            SMWDAAA029.ReadOnly = true;
			
			//CL_Chang 新普轉寄
            SMWDAAA020.ValueText = "Program/DSCGPFlowService/Public/SmpInfo.aspx";

        }
        else
        {
            //主要顯示畫面
            SMWDAAA007.ReadOnly = false;

            //意見表達
            SMWDAAA009.ReadOnly = false;

            //意見表達類型
            SMWDAAA010.ReadOnly = false;

            //主旨
            SMWDAAA011.ReadOnly = true;

            //表單單號
            SMWDAAA012.GuidValueText = "";
            SMWDAAA012.doGUIDValidate();
            SMWDAAA012.ReadOnly = true;

            //設定流程畫面
            SMWDAAA014.ReadOnly = true;

            //加簽畫面
            SMWDAAA015.ReadOnly = false;

            //重辦畫面
            SMWDAAA019.ReadOnly = false;

            //轉寄畫面
            SMWDAAA020.ReadOnly = false;

            //轉派畫面
            SMWDAAA021.ReadOnly = false;

            //顯示簽核意見
            SMWDAAA017.ReadOnly = false;

            //送單後控制
            SMWDAAA018.ReadOnly = true;

            //簽核後控制
            SMWDAAA031.ReadOnly = false;

            //參考流程畫面
            SMWDAAA026.ReadOnly = false;

            //簽核畫面
            SMWDAAA027.ReadOnly = false;

            //撤銷畫面
            SMWDAAA028.ReadOnly = false;

            //取回重辦畫面
            SMWDAAA029.ReadOnly = false;
			
			//CL_Chang
			//新普簽核
            SMWDAAA027.ValueText = "Program/DSCGPFlowService/Public/SmpShowOpinion.aspx";
            //新普轉寄
            SMWDAAA020.ValueText = "Program/DSCGPFlowService/Public/SmpInfo.aspx";
			//新增加簽
            SMWDAAA015.ValueText = "Program/DSCGPFlowService/Public/SmpAddSign.aspx";

        }
    }
    protected void SelectRole_Click(object sender, EventArgs e)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        RoleOpenWin.PageUniqueID = this.PageUniqueID;
        RoleOpenWin.clientEngineType = engineType;
        RoleOpenWin.connectDBString = connectString;
        RoleOpenWin.identityID = "0001";
        RoleOpenWin.paramString = "SMWCAAA002";
        RoleOpenWin.whereClause = "";
        RoleOpenWin.openWin("SMWCAAA", "001", true, "0001");

    }
    protected void RoleOpenWin_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            for (int i = 0; i < values.GetLength(0); i++)
            {
                bool hasFound = false;
                for (int j = 0; j < ADList.dataSource.getAvailableDataObjectCount(); j++)
                {
                    if (values[i, 1].Equals(ADList.dataSource.getAvailableDataObject(j).getData("SMWDAAD003")))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    SMWDAAD ad = (SMWDAAD)ADList.dataSource.create();
                    ad.SMWDAAD001 = IDProcessor.getID("");
                    ad.SMWDAAD002 = "temp";
                    ad.SMWDAAD003 = values[i, 1];

                    if (!ADList.dataSource.add(ad))
                    {
                        MessageBox(ADList.dataSource.errorString);
                    }
                }
            }
            ADList.updateTable();
        }
    }
    protected void CopySetting_Click(object sender, EventArgs e)
    {
        SMWDAAA obj = (SMWDAAA)getSession("currentObject");

        saveData(obj);

        Session["tempSMWDAAAXML"] = obj.saveXML();
        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError21", "設定已複製. 複製資料在關閉瀏覽器前都有效"));
    }
    protected void RestoreSetting_Click(object sender, EventArgs e)
    {
        if (Session["tempSMWDAAAXML"] == null)
        {
            MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError22", "沒有複製的設定"));
            return;
        }

        string xml = (string)Session["tempSMWDAAAXML"];

        SMWDAAA obj = (SMWDAAA)getSession("currentObject");
        string S001 = obj.SMWDAAA001;
        string S002 = obj.SMWDAAA002;
        string S003 = obj.SMWDAAA003;
        string S004 = obj.SMWDAAA004;
        string S005 = obj.SMWDAAA005;
        string S006 = obj.SMWDAAA006;

        obj.loadXML(xml);

        obj.SMWDAAA001 = S001;
        obj.SMWDAAA002 = S002;
        obj.SMWDAAA003 = S003;
        obj.SMWDAAA004 = S004;
        obj.SMWDAAA005 = S005;
        obj.SMWDAAA006 = S006;

        Hashtable hs = obj.child;
        IDictionaryEnumerator ie = hs.GetEnumerator();
        while (ie.MoveNext())
        {
            DataObjectSet dos = (DataObjectSet)ie.Value;
            for (int i = 0; i < dos.getDataObjectCount(); i++)
            {
                DataObject ddo = dos.getDataObject(i);
                ddo.setData(ddo.dataField[1], S001);
            }
        }

        showScreen(obj);
        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwd_detail_aspx.language.ini", "message", "QueryError23", "載入完成"));
    }
}
