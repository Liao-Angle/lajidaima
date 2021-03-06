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
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using WebServerProject;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class Program_SCQ_Form_EH0109_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH010902";
        AgentSchema = "WebServerProject.form.EH010902.EH010902Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "EASYFLOW";
    }
    

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");


        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        qx.ReadOnly = true;
        jbfjs.ReadOnly = true;
        jbbt.ReadOnly = true;
        bx1.ValueText = "0";

        //--------------------------部門選擇
        string sqlbm = @"select distinct PartNo from   [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart 
                        where PartNo not like '%支援%' and  PartNo not like '%駐廠%' and PartNo <>'6M0303-QS'  order by PartNo";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];

        if (dtbm.Rows.Count > 0)
        {

            int j = dtbm.Rows.Count;
            string[,] strs = new string[j + 1, 2];
            strs[0, 0] = "-1";
            strs[0, 1] = "請選擇";

            for (int i = 0; i < j; i++)
            {
                strs[i + 1, 0] = dtbm.Rows[i][0].ToString();
                strs[i + 1, 1] = dtbm.Rows[i][0].ToString();
            }
            PartName.setListItem(strs);
        }
        else
        {
            PartName.setListItem(new string[1, 2] { { "0", "未查询到部门" } });
        }

        //--------------------------職務選擇

        string[,] zw = new string[11, 2] { { "請選擇", "請選擇" }, { "作業員", "作業員" }, { "辦事員", "辦事員" }, { "領班", "領班" }, { "技術員", "技術員" }, { "組長", "組長" }, { "副工程師", "副工程師" }, { "副管理師", "副管理師" }, { "工程師", "工程師" }, { "管理師", "管理師" }, { "副主任", "副主任" } };
        DtName.setListItem(zw);

        //--------------------------類別選擇

        string[,] em = new string[4, 2] { { "請選擇", "請選擇" }, { "DL", "DL" }, { "IDL", "IDL" }, { "SDL", "SDL" } };
        EmpType.setListItem(em);
        

        //--------------------------職等選擇

        string[,] sids = new string[4, 2] { { "請選擇", "請選擇" }, { "一職等", "一職等" }, { "二職等", "二職等" }, { "三職等", "三職等" } };
        Dt.setListItem(sids);

        //--------------------------薪資默認值

        string[,] bxstr = new string[12, 2] { { "0", "0" }, { "1950", "1950" }, { "2050", "2050" }, { "2400", "2400" }, { "2600", "2600" }, { "2700", "2700" }, { "2800", "2800" }, { "3100", "3100" }, { "2150", "2150" }, { "2900", "2900" }, { "3200", "3200" }, { "3300", "3300" } };
        bx.setListItem(bxstr);
        string[,] zwstr = new string[7, 2] { { "120", "120" }, { "300", "300" }, { "500", "500" }, { "1000", "1000" }, { "1200", "1200" }, { "1500", "1500" }, { "1800", "1800" } };
        zwjg.setListItem(zwstr);
        string[,] zgstr = new string[7, 2] { { "0", "0" }, { "100", "100" }, { "300", "300" }, { "500", "500" }, { "600", "600" }, { "800", "800" },{"1200","1200"} };
        zgjg.setListItem(zgstr);

        //--------------------------專業加給
        zyjg.ValueText = "0";

        if (si.fillerID == "Q1101039")
        {
            this.Panel1.Visible = true;
        }
        else
        {
            this.Panel2.Visible = true;
        }


        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        base.showData(engine, objects);
        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        EmpName.ValueText = objects.getData("EmpName");
        PartName.ValueText = objects.getData("PartName");
        zszg.ValueText = objects.getData("zszg");
        DtName.ValueText = objects.getData("DtName");
        Dt.ValueText = objects.getData("Dt");
        EmpType.ValueText = objects.getData("EmpType");
        //bx.ValueText = objects.getData("bx");
        //zgjg.ValueText = objects.getData("zgjg");
        //zwjg.ValueText = objects.getData("zwjg");
        //zyjg.ValueText = objects.getData("zyjg");
        jbbt.ValueText = objects.getData("jbbt");
        //qx.ValueText = objects.getData("qx");
        //jbfjs.ValueText = objects.getData("jbfjs");
        ComeDateY.ValueText = objects.getData("ComeDateY");
        TryDateB.ValueText = objects.getData("TryDateB");
        TryDateE.ValueText = objects.getData("TryDateE");
        zhize.ValueText = objects.getData("zhize");
        password.ValueText = objects.getData("password");

        EmpName.ReadOnly = true;
        PartName.ReadOnly = true;
        zszg.ReadOnly = true;
        DtName.ReadOnly = true;
        EmpType.ReadOnly = true;
        bx.ReadOnly = true;
        zgjg.ReadOnly = true;
        zwjg.ReadOnly = true;
        //zyjg.ReadOnly = true;
        jbbt.ReadOnly = true;
        qx.ReadOnly = true;
        jbfjs.ReadOnly = true;
        ComeDateY.ReadOnly = true;
        TryDateB.ReadOnly = true;
        TryDateE.ReadOnly = true;
        zhize.ReadOnly = true;
        Dt.ReadOnly = true;
        DSCLabel19.ReadOnly = true;
        DSCLabel21.ReadOnly = true;
		bx1.ReadOnly = true;
        zg1.ReadOnly = true;
        zw1.ReadOnly = true;
        zy1.ReadOnly = true;
        qx1.ReadOnly = true;
        jbfjs1.ReadOnly = true;

        //if (bx1.ValueText == "0" && bx.ValueText == "0")
        //{
        //    MessageBox("請解密薪資");
        //    PrintButton2.Enabled = false;
        //}

        //顯示發起資料

    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpName", EmpName.ValueText);
            objects.setData("PartName", PartName.ValueText);
            objects.setData("zszg", zszg.ValueText);
            objects.setData("DtName", DtName.ValueText);
            objects.setData("Dt",Dt.ValueText);
            objects.setData("EmpType", EmpType.ValueText);
            objects.setData("bx", bx.ValueText);
            objects.setData("zgjg", zgjg.ValueText);
            objects.setData("zwjg", zwjg.ValueText);
            objects.setData("zyjg", zyjg.ValueText);
            objects.setData("jbbt", jbbt.ValueText);
            objects.setData("qx", qx.ValueText);
            objects.setData("jbfjs", jbfjs.ValueText);
            objects.setData("ComeDateY", ComeDateY.ValueText);
            objects.setData("TryDateB", TryDateB.ValueText);
            objects.setData("TryDateE", TryDateE.ValueText);
            objects.setData("zhize", zhize.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");



            
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

        }
        else
        {
            objects.setData("zyjg", zyjg.ValueText);
            objects.setData("qx", qx1.ValueText);
            base.saveData(engine, objects);
        }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        //新增判斷資料
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 MFG7人員任用單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }
        if(DtName.ValueText=="作業員"&& Dt.ValueText !="一職等")
        {
            MessageBox("作業員為一職等員工");
            result = false;
        }
        if (DtName.ValueText == "辦事員" && Dt.ValueText != "二職等")
        {
            MessageBox("辦事員為二職等員工");
            result = false;
        }
        if (DtName.ValueText == "技術員" && Dt.ValueText != "二職等")
        {
            MessageBox("技術員為二職等員工");
            result = false;
        }
        if (DtName.ValueText == "領班" && Dt.ValueText != "二職等")
        {
            MessageBox("領班為二職等員工");
            result = false;
        }
        if (DtName.ValueText == "組長" && Dt.ValueText != "三職等")
        {
            MessageBox("組長為三職等員工");
            result = false;
        }
        if (DtName.ValueText == "副工程師" && Dt.ValueText != "三職等")
        {
            MessageBox("副工程師為三職等員工");
            result = false;
        }
        if (DtName.ValueText == "副管理師" && Dt.ValueText != "三職等")
        {
            MessageBox("副管理師為三職等員工");
            result = false;
        }
        if (DtName.ValueText == "工程師" && Dt.ValueText != "三職等")
        {
            MessageBox("工程師為三職等員工");
            result = false;
        }
        if (DtName.ValueText == "管理師" && Dt.ValueText != "三職等")
        {
            MessageBox("管理師為三職等員工");
            result = false;
        }
        if (DtName.ValueText == "副主任" && Dt.ValueText != "三職等")
        {
            MessageBox("副主任為三職等員工");
            result = false;
        }
        if (Dt.ValueText == "一職等" && Convert.ToDouble(zyjg.ValueText) > 500)
        {
            MessageBox("一職等專業加給不得大於500");
            result = false;
        }
        if (Dt.ValueText == "二職等" && Convert.ToDouble(zyjg.ValueText) > 1050)
        {
            MessageBox("二職等專業加給不得大於1050");
            result = false;
        }
        if (Dt.ValueText == "三職等" && Convert.ToDouble(zyjg.ValueText) > 1400)
        {
            MessageBox("三職等專業加給不得大於1400");
            result = false;
        }
        if (bx1.ValueText =="0"&& bx.ValueText=="0")
        {
            MessageBox("請先解密薪資");
            result = false;
        }

        if (base.attachFile.dataSource.getAvailableDataObjectCount() < 1)
        {
            MessageBox("附件需上傳：員工資料信息,詳細資料可聯繫HR招募人員");
        }



        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        return getSubmitInfo(engine, objects, si);
    }
    
    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定自動編碼格式所需變數值。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeID"></param>
    /// <returns></returns>
    protected override Hashtable getSheetNoParam(AbstractEngine engine, string autoCodeID)
    {
        Hashtable hs = new Hashtable();
        hs.Add("FORMID", ProcessPageID);
        return hs;
    }

    /// <summary>
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string xml = "";
        string zd = Dt.ValueText;
        string iscw = "";
        string isdsz = "";
        string isqh = "";
        string bmzg = "";
        string zszg = "";
        string zjl="Q1508126";
        string creatorId = si.fillerID;  //填表人
        string[] values = base.getUserManagerInfoID(engine, creatorId);
        string[] BmInfo = base.getOrgUnit(engine, si.ownerOrgID);
        
        //if (creatorId != BmInfo[3])
        //{
        //    bmzg = values[1];
        //    isqh = "1";
        //    string a=base.getUserManagerInfoID(engine, bmzg)[1];
        //    if (a == zjl)
        //    {
        //        zszg = values[1];
        //    }
        //    else 
        //    {
        //        zszg = a;
        //    }
            
        //}
        //else
        //{
        //    bmzg=creatorId;
        //    string b=base.getUserManagerInfoID(engine, bmzg)[1];
        //    if (b == zjl)
        //    {
        //        zszg = creatorId;
        //    }
        //    else
        //    {
        //        zszg = b;
        //    }
        //}
       

        if (zd == "四職等"||zd == "五職等"||zd == "六職等")
        {
            iscw = "1";
        }

        if (zd == "六職等")
        {
            isdsz = "1";
        }


        xml += "<EH010902>";

        //xml += "<bmzg DataType=\"java.lang.String\">" + bmzg + "</bmzg>";//部門主管
        //xml += "<zszg DataType=\"java.lang.String\">" + zszg + "</zszg>";//直屬主管
        //xml += "<cwzg DataType=\"java.lang.String\">" + cwzg + "</mrgmrg>";//上級主管
        xml += "<iscw DataType=\"java.lang.String\">" + iscw + "</iscw>";//財務是否簽核
        //xml += "<dsz DataType=\"java.lang.String\">" + dsz + "</fz>";//董事長
        xml += "<isdsz DataType=\"java.lang.String\">" + isdsz + "</isdsz>";//董事長是否簽核
        //xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";//部門是否簽核
        xml += "</EH010902>";

        param["EH010902"] = xml;
        return "EH010902";


    }


    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);
        //password.ReadOnly = false;
        //password.Enabled = true;
        //xz.Enabled = true;
        //xz.ReadOnly = false;
        //PrintButton2.ReadOnly = false;
        
    }

    /// <summary>
    /// 若有加簽，送簽核前呼叫。
    /// 加簽時系統會設定Session("IsAddSign")，所以必需在saveData時執行 setSession("IsAddSign", "AFTER");
    /// AFTER 代表往後簽核
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterSign(engine, currentObject, result);
               
    }

    /// <summary>
    /// 重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID
        if (backActID.ToUpper().Equals("CREATOR"))
        {
            try
            {
                base.terminateThisProcess();
            }
            catch (Exception e)
            {
                base.writeLog(e);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }

    /// <summary>
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        //"Y"代表同意
        if (result == "Y")
        {
            //開發同意簽核後自動化流程
        }
    }
    protected void total_SelectChanged(string value)
    {
        if (DtName.ValueText == "領班" && bx.ValueText == "2400")
        {
            zgjg.setListItem(zgstr1);
        }
        else if (DtName.ValueText != "領班" && bx.ValueText == "2400")
        {
            zwjg.setListItem(zwstr2);
            zgjg.setListItem(zgstr0);
        }
        else if (DtName.ValueText == "領班"&&(bx.ValueText == "2600" || bx.ValueText == "2700"))
        {
            zwjg.setListItem(zwstr3);
            zgjg.setListItem(zgstr2);
        }
        else if (DtName.ValueText != "領班"&&(bx.ValueText == "2600" || bx.ValueText == "2700"))
        {
            zwjg.setListItem(zwstr3);
            zgjg.setListItem(zgstr0);
        }
        else if (Dt.ValueText == "二職等"&&DtName.ValueText == "領班" && (bx.ValueText == "2800" || bx.ValueText == "3100"))
        {
            zwjg.setListItem(zwstr4);
            zgjg.setListItem(zgstr6);
        }
        else if (Dt.ValueText == "二職等" && DtName.ValueText != "領班" && (bx.ValueText == "2800" || bx.ValueText == "3100"))
        {
            zwjg.setListItem(zwstr4);
            zgjg.setListItem(zgstr0);
        }
        else if (Dt.ValueText == "三職等" && bx.ValueText == "2800" && DtName.ValueText == "組長")
        {
            zwjg.setListItem(zwstr5);
            zgjg.setListItem(zgstr3);
        }
        else if (Dt.ValueText == "三職等" && bx.ValueText == "2800" && DtName.ValueText != "組長")
        {
            zwjg.setListItem(zwstr5);
            zgjg.setListItem(zgstr0);
        }
        else if (Dt.ValueText == "三職等" && DtName.ValueText == "組長" && (bx.ValueText == "2900" || bx.ValueText == "3100"))
        {
            zwjg.setListItem(zwstr6);
            zgjg.setListItem(zgstr4);
        }
        else if (Dt.ValueText == "三職等" && DtName.ValueText != "組長" && (bx.ValueText == "2900" || bx.ValueText == "3100"))
        {
            zwjg.setListItem(zwstr6);
            zgjg.setListItem(zgstr0);
        }
        else if ( DtName.ValueText == "組長"&&(bx.ValueText == "3200" || bx.ValueText == "3300"))
        {
            zwjg.setListItem(zwstr7);
            zgjg.setListItem(zgstr5);
        }
        else if (DtName.ValueText != "組長" && (bx.ValueText == "3200" || bx.ValueText == "3300"))
        {
            zwjg.setListItem(zwstr7);
            zgjg.setListItem(zgstr0);
        }
        else if (bx.ValueText == "2150")
        {
            zwjg.setListItem(zwstr5);
            zgjg.setListItem(zgstr0);
        }

        getQX();

    }
    protected void ComeDateY_DateTimeClick(string values)
    {
        TryDateB.ValueText = ComeDateY.ValueText;
        TryDateE.ValueText = DateTime.Parse(ComeDateY.ValueText).AddMonths(3).ToString();
    }

    string[,] bxstr1 = new string[1, 2] { { "1950", "1950" } };
    string[,] bxstr2 = new string[6, 2] { { "2050", "2050" }, { "2400", "2400" }, { "2600", "2600" }, { "2700", "2700" }, { "2800", "2800" }, { "3100", "3100" } };
    string[,] bxstr3 = new string[6, 2] { { "2150", "2150" }, { "2800", "2800" }, { "2900", "2900" }, { "3100", "3100" }, { "3200", "3200" }, { "3300", "3300" } };
    string[,] bxstr4 = new string[1, 2] { { "2050", "2050" } };
    string[,] bxstr5 = new string[1, 2] { { "2150", "2150" } };
    string[,] zwstr1 = new string[1, 2] { { "120", "120" } };
    string[,] zwstr2 = new string[1, 2] { { "300", "300" } };
    string[,] zwstr3 = new string[1, 2] { { "1000", "1000" } };
    string[,] zwstr4 = new string[1, 2] { { "1500", "1500" } };
    string[,] zwstr5 = new string[1, 2] { { "500", "500" } };
    string[,] zwstr6 = new string[1, 2] { { "1200", "1200" } };
    string[,] zwstr7 = new string[1, 2] { { "1800", "1800" } };
    string[,] zgstr0 = new string[1, 2] { { "0", "0" } };
    string[,] zgstr1 = new string[1, 2] { { "100", "100" } };
    string[,] zgstr2 = new string[1, 2] { { "300", "300" } };
    string[,] zgstr3 = new string[1, 2] { { "500", "500" } };
    string[,] zgstr4 = new string[1, 2] { { "800", "800" } };
    string[,] zgstr5 = new string[1, 2] { { "1200", "1200" } };
    string[,] zgstr6 = new string[1, 2] { { "600", "600" } };

    protected void Dt_SelectChanged(string value)
    {
        try
        {
            if (Dt.ValueText.Equals("一職等"))
            {
                bx.setListItem(bxstr1);
                zwjg.setListItem(zwstr1);
                zgjg.setListItem(zgstr0);
                zyjg.ValueText = "0";
            }


            if (Dt.ValueText == "二職等")
            {
                if(DtName.ValueText == "辦事員")
                {
                    bx.setListItem(bxstr4);
                    zwjg.setListItem(zwstr2);
                    zgjg.setListItem(zgstr0);
                }
                else if (DtName.ValueText == "領班") 
                {
                    bx.setListItem(bxstr2);
                    zwjg.setListItem(zwstr2);
                    //zgjg.setListItem(zgstr1);

                }
                else
                {
                    bx.setListItem(bxstr2);
                    zwjg.setListItem(zwstr2);
                    zgjg.setListItem(zgstr0);

                }

            }


            if (Dt.ValueText == "三職等")
            {
                if (DtName.ValueText == "組長")
                {
                    bx.setListItem(bxstr3);
                    zwjg.setListItem(zwstr3);
                    zgjg.setListItem(zgstr0);
                }
                else if (DtName.ValueText == "副管理師")
                {
                    bx.setListItem(bxstr5);
                    zwjg.setListItem(zwstr5);
                    zgjg.setListItem(zgstr0);
                }
                else
                {
                    bx.setListItem(bxstr3);
                    zwjg.setListItem(zwstr5);
                    zgjg.setListItem(zgstr0);
                }
            }

            if (Dt.ValueText == "請選擇")
                MessageBox("請選擇");

            getQX();
        }
        catch (Exception)
        {
            MessageBox("請選擇職等");
        }

    }

    private void getQX()
    {
        double bxNUM = 0;
        double zgjgNUM = 0;
        double zwjgNUM = 0;
        double zyjgNUM = 0;
        double jbbtNUM = 0;

        try
        {
            if (!string.IsNullOrEmpty(bx.ValueText))
            {
                bxNUM = Convert.ToDouble(bx.ValueText);
            }
            if (!string.IsNullOrEmpty(zgjg.ValueText))
            {
                zgjgNUM = Convert.ToDouble(zgjg.ValueText);
            }
            if (!string.IsNullOrEmpty(zwjg.ValueText))
            {
                zwjgNUM = Convert.ToDouble(zwjg.ValueText);
            }
            if (!string.IsNullOrEmpty(zyjg.ValueText))
            {
                zyjgNUM = Convert.ToDouble(zyjg.ValueText);
            }
            if (Dt.ValueText == "一職等" && Convert.ToDouble(zyjg.ValueText) > 500)
            {
                MessageBox("一職等專業加給不得大於500");
                zyjg.ValueText = "0";
            }
            if (Dt.ValueText == "二職等" && Convert.ToDouble(zyjg.ValueText) > 1050)
            {
                MessageBox("二職等專業加給不得大於1050");
                zyjg.ValueText = "0";

            }
            if (Dt.ValueText == "三職等" && Convert.ToDouble(zyjg.ValueText) > 1400)
            {
                MessageBox("三職等專業加給不得大於1400");
                zyjg.ValueText = "0";

            }
            

        }
        catch (Exception)
        {
            MessageBox("不是一個數字");

        }

        qx.ValueText = (bxNUM + zgjgNUM + zwjgNUM + zyjgNUM + jbbtNUM) + "";
        jbfjs.ValueText = bxNUM + "";
    }
    protected void xz_Click(object sender, EventArgs e)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        
        string b = EmpName.ValueText;

        string seqNo = getCustomSheetNo(engine, "SMPGeneralCode", "EH0109");
        string sn = getSheetNoProcedure(engine, seqNo);
        string bxsql = @"select bx,zgjg,zwjg,zyjg,qx,jbfjs from EH0109A where SheetNo ='" + sn + "'";
        DataRow bxtb = engine.getDataSet(bxsql, "data").Tables["data"].Rows[0];
        
        string b1 = "", zg = "",zw = "",zy = "",jbfj="",q = "";

        b1 = bxtb["bx"].ToString();
        zg = bxtb["zgjg"].ToString();
        zw = bxtb["zwjg"].ToString();
        zy = bxtb["zyjg"].ToString();
        q = bxtb["qx"].ToString();
        jbfj = bxtb["jbfjs"].ToString();

        engine = factory.getEngine(engineType, connectString);
        string sqltxt = "select * from SmpHRAppointmentAuthority where empno='" + Session["UserID"].ToString() + "'";
        DataSet ds = engine.getDataSet(sqltxt, "tmp");
        DataTable dt = ds.Tables["tmp"];


        if (password.ValueText == "")
        {
            MessageBox("請輸入薪資解鎖密碼");
            return;
        }
        if (dt.Rows.Count < 1)
        {
            MessageBox("未設置您權限，請聯繫HR添加權限");
            return;
        }

        if (Session["UserID"].ToString() == dt.Rows[0][0].ToString() && password.ValueText == dt.Rows[0][5].ToString())
        {
            MessageBox("已解密");
            bx1.Display = true;
            zg1.Display = true;
            zw1.Display = true;
            //zy1.Display = true;
            qx1.Display = true;
            jbfjs1.Display = true;
            bx1.ValueText = b1;
            zg1.ValueText = zg;
            zw1.ValueText = zw;
            zyjg.ValueText = zy;
            qx1.ValueText = q;
            jbfjs1.ValueText = jbfj;
            bx.Display = false;
            zgjg.Display = false;
            zwjg.Display = false;
            //zyjg.Display = false;
            qx.Display = false;
            jbfjs.Display = false;
            
        }
        else
        {
            MessageBox("密碼錯誤");
        }
    }
    protected void DSCLabel13_Click(object sender, EventArgs e)
    {
        qx1.ValueText = Convert.ToDouble(bx1.ValueText) + Convert.ToDouble(zg1.ValueText) + Convert.ToDouble(zw1.ValueText) + Convert.ToDouble(zyjg.ValueText) + "";
    }

    protected string getCustomSheetNo(AbstractEngine engine, string code, string formCode)
    {
        string sheetNo = "";
        try
        {
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("FormCode ", formCode);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();

        }
        catch (Exception e)
        {
            base.writeLog(e);
            base.writeLog(new Exception("sheetno:" + sheetNo));
        }
        return sheetNo;
    }
    //列印單據
    protected void PrintButton_OnClick(object sender, EventArgs e)
    {
        //MessageBox("SheetNo : " + SheetNo.ValueText);
        //if (bx1.ValueText == "0" && bx.ValueText == "0")
        //{
        //    MessageBox("請先解密薪資");
        //    Session["EH010902_SheetNo"] = SheetNo.ValueText;
        //    string url1 = "Form.aspx";
        //    base.showOpenWindow(url1, "任用單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
        //}
        Session["EH010902_SheetNo"] = SheetNo.ValueText;
        string url = "PrintPage.aspx";
        base.showOpenWindow(url, "列印任用單", "", "600", "", "", "", "1", "1", "", "", "", "", "750", "", true);
    }
    protected void zyjg_TextChanged(object sender, EventArgs e)
    {
        getQX();
    }

    protected void wjmm_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        AbstractEngine engine = null;
        IOFactory factory = new IOFactory();

        engine = factory.getEngine(engineType, connectString);
        string sqltxt = "SELECT empEmail FROM EmployeeInfo where empNumber='" + Session["UserID"].ToString() + "'";

        DataSet ds = engine.getDataSet(sqltxt, "T");
        if (ds.Tables["T"].Rows.Count < 1)
        {

            MessageBox("組織異常，請提供部門代碼和上級主管給MIS陳淑晴");
            return;
        }

        string Email = engine.executeScalar(sqltxt).ToString();
        string empno = Session["UserID"].ToString();
        string subject = "任用單密碼重置郵件通知";
        string connstring = (string)Session["connectString"];

        string sql = "insert into [10.200.100.38].TP.dbo.Email (email_gist,email_nr,email_fjr,isfs)values('" + subject + "','";
        sql = sql + "<p><a href= http://10.3.11.84/ecp/Program/SCQ/EH0109/ModPassword.aspx?empno=" + empno + ">任用單密碼重置</a> </p>','" + Email + "','0')";

        // MessageBox(sql);

        using (SqlConnection con = new SqlConnection(connstring))
        {
            con.Open();
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();
            MessageBox("請稍後進入郵箱點擊鏈接重置您的密碼！");
        }

    }
    protected void PartName_SelectChanged(string value)
    {
        if (PartName.ValueText == "NQM0511-AME- Fixture")
        {
            this.Panel1.Visible = true; ;
        }
        else
        {
            this.Panel2.Visible = true;
        }
    }
}
