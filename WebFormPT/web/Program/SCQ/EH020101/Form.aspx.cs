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
using System.DirectoryServices;
using DSCWebControl;
using WebServerProject;
using System.Data.OleDb;

public partial class Program_SCQ_Form_EH020101_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EH020101";
        AgentSchema = "WebServerProject.form.EH020101.EH020101Agent";
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
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];


        string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + si.fillerID + "'";

        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];

        string sqlga = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart";

        DataTable dtga = engine.getDataSet(sqlga, "bmE").Tables["bmE"];

        ///--------------------------帶出使用者信息
        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        EmpNo.DoEventWhenNoKeyIn = false;
        EmpNo.ValueText = si.fillerID;
        EmpNo.doValidate();
        UpdateData(si.fillerID);
        EmpNo.ReadOnly = true;
        JEmpNo.clientEngineType = engineType;
        JEmpNo.connectDBString = connectString;
        JPartNo.ReadOnly = true;
        Money.ReadOnly = true;

        string SQLstr = "";
        try
        {
            if (si.fillerOrgID == "GQC2200" || si.fillerOrgID == "GIC2200")
            {
                SQLstr = "(PartNo = '" + dtga.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtga.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtga.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")";
                JEmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


            }
            else
            {
                //SQLstr = "1=2";
                SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtbm.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")";
                JEmpNo.whereClause = SQLstr;
            }

            JEmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "GQC2200")
            {
                SQLstr = "PartNo like '%" + si.fillerOrgID + "%'";
                JEmpNo.whereClause = SQLstr;
            }
            else
            {
                SQLstr = "1=1";
                JEmpNo.whereClause = SQLstr;
            }
            JEmpNo.DoEventWhenNoKeyIn = false;
        }

        string[,] zw = new string[7, 2] { { "請選擇", "請選擇" }, { "嘉獎", "嘉獎" }, { "小功", "小功" }, { "大功", "大功" }, { "申戒", "申戒" }, { "小過", "小過" }, { "大過", "大過" } };
        JNo.setListItem(zw);
        string[,] Jcs = new string[10, 2] { { "1", "1" }, { "2", "2" }, { "3", "3" }, { "4", "4" }, { "5", "5" }, { "6", "6" }, { "7", "7" }, { "8", "8" }, { "9", "9" }, { "10", "10" } };
        CS.setListItem(Jcs);

        ///初始化上傳控件
        try
        {
            SysParam ssp = new SysParam(engine);
            // string fileAdapter = ssp.getParam("FileAdapter");
            FileUpload1.FileAdapter = "奖惩文件|*.xls";

            FileUpload1.engine = engine;
            FileUpload1.tempFolder = Server.MapPath("~/tempFolder/JC/");//獎懲臨時資料夾
            FileUpload1.readFile("");
            FileUpload1.updateTable();
        }
        catch { }

        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EH020101.EH020101B");
            dos.setTableName("EH020101B");
            dos.loadFileSchema();
            objects.setChild("EH020101B", dos);
            OutDataListJC.dataSource = dos;
            OutDataListJC.updateTable();
        }

        OutDataListJC.clientEngineType = engineType;
        OutDataListJC.connectDBString = connectString;
        OutDataListJC.HiddenField = new string[] { "GUID", "SheetNo" };


        //改變工具列順序
        base.initUI(engine, objects);
    }

    private void UpdateData(string id)
    {
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            Hashtable h1 = base.getHRUsers(engine, id);
            partNo.ValueText = h1["PartNo"].ToString();
            partNo.ReadOnly = true;
            //  Mobile.ValueText = h1["Mobile"].ToString();
            Hashtable h2 = base.getADUserData(engine, id);
            mobileuser.ValueText = h2["telephonenumber"].ToString();
            mobileuser.ReadOnly = true;
        }
        catch { }
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        Subject.ValueText = objects.getData("Subject");
        OutDataListJC.dataSource = objects.getChild("EH020101B");
        //OutDataListJC.dataSource.sort(new string[,] { { "Date", DataObjectConstants.ASC } });
        OutDataListJC.updateTable();


        OutDataListJC.IsShowCheckBox = false;
        OutDataListJC.ReadOnly = true;
        SheetNo.Display = false;
        Subject.Display = false;
        JEmpNo.ReadOnly = true;
        JPartNo.ReadOnly = true;
        Date.ReadOnly = true;
        JNo.ReadOnly = true;
        Money.ReadOnly = true;
        CS.ReadOnly = true;

        base.showData(engine, objects);


        //顯示發起資料

    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        base.saveData(engine, objects);
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        bool result = true;
        DataObject[] objs = OutDataListJC.dataSource.getAllDataObjects();

        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("Subject", Subject.ValueText);
            //objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", si.fillerName);
            objects.setData("PartNo", partNo.ValueText);
            //objects.setData("CS", CS.ValueText);
            objects.setData("ForwardHR", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            //將B表匯進來
            foreach (DataObject obj in OutDataListJC.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }
            objects.setChild("EH020101B", OutDataListJC.dataSource);


            //產生單號並儲存至資料物件


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
        DataObject[] objs = OutDataListJC.dataSource.getCurrentPageObjects();
        //新增判斷資料
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 獎懲申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }
        if (OutDataListJC.dataSource.getAvailableDataObjectCount() == 0)
        {
            pushErrorMessage("必須填寫獎懲資料");
            result = false;
        }

        string temp1String = string.Empty;
        string temp2String = string.Empty;
        for (int i = 0; i < objs.Length; i++)
        {
            if (i == 0)
            {
                temp1String = (objs[i].getData("JNo") == "小過" || objs[i].getData("JNo") == "申戒") ? "1" : "2";
            }
            else
            {
                temp2String = (objs[i].getData("JNo") == "小過" || objs[i].getData("JNo") == "申戒") ? "1" : "2";
                if (temp1String != temp2String)
                {
                    pushErrorMessage("申戒，小過不可與其他類別獎懲一同申請");
                    result = false;
                    break;
                }
            }
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
        DataObject[] objs = OutDataListJC.dataSource.getAllDataObjects();
        string xml = "";
        string isqh = "";
        string isga = "1";
        string iszjl = "";
        string tzzg = "";
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].getData("JNo") == "嘉獎" || objs[i].getData("JNo") == "小功" || objs[i].getData("JNo") == "大功" || objs[i].getData("JNo") == "大過")
            {
                iszjl = "2";
                isqh = "2";
            }
            else
            {
                isqh = "3";
                iszjl = "";
            }
        }


        if (si.fillerOrgID == "GQC2200" || si.fillerOrgID == "GIC2200")
            {
                isga = "1";
                string JPartNo = objs[0].getData("JPartNo");
                string strSql = @"select MyField7 from [10.3.11.96].MIS_DB.dbo.PerDepartMD where Status='Y' AND PartNo='" + JPartNo + "'";
                DataTable hst = engine.getDataSet(strSql, "bmxz").Tables["bmxz"];
                if (hst != null && hst.Rows.Count > 0)
                {
                    tzzg = hst.Rows[0]["MyField7"].ToString();
                }
                
            }
            else
            {
                isga = "0";
            }
        

        xml += "<EH020101>";
        xml += "<isga DataType=\"java.lang.String\">" + isga + "</isga>";//GA是否簽核
        xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";//處級是否簽核
        xml += "<iszjl DataType=\"java.lang.String\">" + iszjl + "</iszjl>";//總經理是否簽核
        xml += "<tzzg DataType=\"java.lang.String\">" + tzzg + "</tzzg>";//通知主管
        xml += "</EH020101>";

        param["EH020101"] = xml;
        return "EH020101";
    }
    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);

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

    private void UpdateDataJ(string id)
    {
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            Hashtable h1 = base.getHRUsers(engine, JEmpNo.ValueText);
            JPartNo.ValueText = h1["PartNo"].ToString();
        }
        catch { }
    }

    protected void JEmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        UpdateDataJ(JEmpNo.ValueText);
    }
    protected void JNo_SelectChanged(string value)
    {
        if (JNo.ValueText == "嘉獎")
        {
            Money.ValueText = Convert.ToString(Convert.ToInt32(CS.ValueText) * 20);
            Mark.ValueText = @"（1）技術熟練工作績效經評鑒優異者。
（2）品行端正，工作努力，負責盡職，與他人溝通良好者。
（3）拾獲現金、手機、銀行卡、證件、廠牌或其它有價物品主動歸還者。
（4）熱心服務，有具體事蹟者。
（5）對突發事件或災害預防處理得當,有助於工廠安全者。
（6）後工序發現前工序不良,降低品質風險者。
（7）發現材料不良足以影響產品品質,避免損失者。
（8）有其它功績，足為其它員工楷模者。";
        }
        if (JNo.ValueText == "小功")
        {
            Money.ValueText = Convert.ToString(Convert.ToInt32(CS.ValueText) * 60);
            Mark.ValueText = @"（1）對工作改善有特殊貢獻者。
（2）有顯著之事實，確實增進本公司業務或聲譽者。
（3）對於生產技術或管理制度建議改進，提出具體之改善方案，經採納施行確實具成效者。
（4）以身作則，領導有方，使業務發展有相當績效者。
（5）當選模範員工或者有其它重大功績者。
（6）遇有災變，勇於負責，處理得宜者。
（7）生產線連續三個月產品品質無重大不良者，且每月產能及效率皆達成者(針對班長、組長)。
（8）在非生產線、倉庫等區域拾獲無主之生產原物料、成品主動提報管理部經查屬實者。
（9）檢舉他人行為不軌,減少公司損失者。
（10）發現重大隱患,立即採取應變措施或及時提報改善者。
（11）其它應當給予記小功之事蹟者。
";
        }
        if (JNo.ValueText == "大功")
        {
            Money.ValueText = Convert.ToString(Convert.ToInt32(CS.ValueText) * 120);
            Mark.ValueText = @"（1）維護員工安全，按相關程式執行任務，確有功績者。
（2）非常事件中，在考慮自身安全下為公司效力，因而使公司得免重大損害者。
（3）挽救意外災害，在考慮自身安全下，全力以赴減少公司損害者。
（4）工作或技術上有所創新，獲致重大績效者。
（5）對公司有重大貢獻或促進榮譽者。
（6）對舞弊或有害公司權益事情,能事先舉報或防止,使工廠免受重大損失者。
（7）拾獲公司經營之重要文件、物品或工具,使公司免受損失者。
（8）具有其它重大事蹟，為其它員工表率者。
";
        }
        if (JNo.ValueText == "申戒")
        {
            Money.ValueText = Convert.ToString(Convert.ToInt32(CS.ValueText) * 20);
            Mark.ValueText = @"（1）不按規定著裝以及不按規定佩戴員工識別證及各類卡證者。
（2）工作期間做與工作無關的事，情節輕微者。
（3）隨地吐痰、違規停車、踐踏草坪(花圃)、講髒話或類似不文明行為者。
（4）未盡職責或積壓文件致延誤工作時效，情節輕微者。
（5）在工作時間打瞌睡、談天、嬉戲、閱讀無關職務工作之書報雜誌或從事規定以外之工作者。
（6）在員工餐廳內取食餐點未食用完予以丟棄，且不收拾餐盤及殘渣至指定區域者。
（7）各生產線員工依照規定，不認真填寫生產報表。
（8）工作責任心不強,粗心大意, 因過失致發生工作錯誤情節輕微者。
（9）無故不參加公司安排規定之集會、訓練或其它事項者。
（10）未按照規定關閉機（儀）器、窗戶、電源、電腦等相關設備者。
（11）不服從上級主管合理之工作安排和調配情節輕微者。
（12）人員站立或坐臥在叉車(含手拉叉車)牙叉上及小推車、台車上玩耍。
（13）未按照作業要求進行作業，情節輕微者。
（14）主管人員對所屬人員管理不當或督導不力者。
（15）侵佔公司財產納為己用，情節輕微者。（如安檢區籃框、公司文具等）
（16）將靜電服(衣、帽、鞋)穿出廠區者。
（17）在公司廠區/車間劃定之安檢管制區域內逗留，不服從保安勸離警告者。
（18）在車間安檢區域內以拋擲方式進行物品傳遞，情節輕微者。
（19）私自調換宿舍區床位者。
（20）在非指定吸煙時間吸煙者。
（21）違反其它管理規定情節輕微者。
";
        }
        if (JNo.ValueText == "小過")
        {
            Money.ValueText = Convert.ToString(Convert.ToInt32(CS.ValueText) * 60);
            Mark.ValueText = @"(1)怠忽職守或擅離工作崗位，情節嚴重者。
(2)不服從上級安排或交付之工作任務無故不完成者。
(3)工作責任心不強,粗心大意,造成工作差錯情節較嚴重者。
(4)發現、發生嚴重問題知情不報,造成損失者。
(5)擅自撕毀公文.公告或技術檔資料者。
(6)拒絕主管或權責人員對其所持物品之檢查，或出入廠區不遵守規定者。
(7)進入工位未按規定配戴勞動防護器具者。
(8)隨意在牆壁、機器設備、器具上塗寫文字、圖畫者。
(9)妨害工作場所秩序或安全、經告誡仍不改正者。
(10)從宿舍二樓以上(含)拋擲、丟棄、傾倒垃圾或其它物品到下方者。
(11)後工序發現前工序不良,後工序故意不提報者。
(12)產品不良率達5%以上檢驗未發現者。
(13)員工未盡職責或積壓文件致延誤工作時效造成公司損失者。
(14)在廠區/生活區內駕車超速行駛及違章駕駛。
(15)違反其它管理規定情節較大者。
";
        }
        if (JNo.ValueText == "大過")
        {
            Money.ValueText = Convert.ToString(Convert.ToInt32(CS.ValueText) * 120);
            Mark.ValueText = @"(1)不服從主管合理指揮，公然侮辱上司，經勸不聽者。
(2)主管階級督導不周或業務單位人員怠忽職責致延誤商機，使公司發生損害或其它災害或事故者。
(3)未經許可隨意開動或操作其它車間機器及設備者。
(4)丟棄材料、半成品或成品置之不理經查屬實者；
(5)虛報加班時數、偽造考勤記錄者。
(6)遺失經管之重要檔、機件、物件或工具者。
(7)撕毀或塗改公佈欄上之公告,或未經許可擅自張貼散發傳單者。
(8)延誤工作或擅自變更工作方法，使公司蒙受損失者。
(9) 未依作業安全標準規定/流程，造成員工受傷者。
(10)拒絕聽從主管人員合理指揮監督，經勸導仍不聽從者。
(11)在廠區/生活區內駕車(包括叉車)違規逆向行駛或超速行駛
(12)在公司劃定管制區範圍內逗留，經保安勸導離開不服從者。
(13)在廠區/車間內違規充電者。
(14)在生活區員工宿舍違規使用電器設備。
(15)未經許可，空手乘坐貨梯。
(16)非倉庫主管人員未經許可進入倉庫。
(17)騎乘電動車、機動車未戴安全頭盔者
(18)從廠區/車間拋擲、丟棄、傾倒垃圾或其它物品到廠區路面者。
(19)其它違反管理規定情節嚴重者。
";
        }
        if (JNo.ValueText == "請選擇")
        {
            MessageBox("請選擇獎懲類別");
            Money.ValueText = "";
        }
    }


    public Hashtable selectjc(AbstractEngine engine, string EmpNo)
    {
        Hashtable tb = new Hashtable();

        tb.Add("dg", "0");
        tb.Add("xg", "0");
        tb.Add("jj", "0");
        tb.Add("dgg", "0");
        tb.Add("xgg", "0");
        tb.Add("sj", "0");
        tb.Add("a", "0");
        tb.Add("b", "0");
        tb.Add("c", "0");
        tb.Add("d", "0");
        tb.Add("e", "0");
        tb.Add("f", "0");

        try
        {
            string SQL = @"select EmpNo,sum(a) dg,sum(b)xg,sum(c) jj,sum(d)dgg,sum(e)xgg,sum(f)sj from (
select EmpNo,CONVERT(int,a) a,CONVERT(int,b) b,CONVERT(int,c) c,CONVERT(int,d) d,CONVERT(int,e) e,CONVERT(int,f) f from (
select EmpNo,RPID,
case RPID when '0001' then '1' else '0' end a,
case RPID when '0002' then '1' else '0' end b,
case RPID when '0003' then '1' else '0' end c,
case RPID when '0004' then '1' else '0' end d,
case RPID when '0005' then '1' else '0' end e,
case RPID when '0006' then '1' else '0' end f
 from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerRewOrPen1 where 
YYMMDD between DATEADD(mm,-1,DATEADD(mm,DATEDIFF(mm, '1900-1-26',getdate()), '1900-1-26')) 
 and DATEADD(mm,DATEDIFF(mm, '1900-1-26',getdate()), '1900-1-25')) h
  ) g  where EmpNo='" + EmpNo + "' group by EmpNo";
            string SQL1 = @"select EmpNo,sum(a) dg,sum(b)xg,sum(c) jj,sum(d)dgg,sum(e)xgg,sum(f)sj from (
select EmpNo,CONVERT(int,a) a,CONVERT(int,b) b,CONVERT(int,c) c,CONVERT(int,d) d,CONVERT(int,e) e,CONVERT(int,f) f from (
select EmpNo,RPID,
case RPID when '0001' then '1' else '0' end a,
case RPID when '0002' then '1' else '0' end b,
case RPID when '0003' then '1' else '0' end c,
case RPID when '0004' then '1' else '0' end d,
case RPID when '0005' then '1' else '0' end e,
case RPID when '0006' then '1' else '0' end f
 from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerRewOrPen1 where substring(CONVERT(nvarchar(10),YYMMDD,23),1,4)=SUBSTRING(CONVERT(nvarchar(10),getdate(),23),1,4)
 ) h) g  where EmpNo='" + EmpNo + "' group by EmpNo";







            if (engine.getDataSet(SQL, "jczl").Tables["jczl"].Rows.Count > 0)
            {
                DataRow dr = engine.getDataSet(SQL, "jczl").Tables["jczl"].Rows[0];
                tb["dg"] = dr["dg"].ToString();
                tb["xg"] = dr["xg"].ToString();
                tb["jj"] = dr["jj"].ToString();
                tb["dgg"] = dr["dgg"].ToString();
                tb["xgg"] = dr["xgg"].ToString();
                tb["sj"] = dr["sj"].ToString();
            }

            if (engine.getDataSet(SQL1, "hrsk").Tables["hrsk"].Rows.Count > 0)
            {
                DataRow dr1 = engine.getDataSet(SQL1, "hrsk").Tables["hrsk"].Rows[0];
                tb["a"] = dr1["dg"].ToString();
                tb["b"] = dr1["xg"].ToString();
                tb["c"] = dr1["jj"].ToString();
                tb["d"] = dr1["dgg"].ToString();
                tb["e"] = dr1["xgg"].ToString();
                tb["f"] = dr1["sj"].ToString();
            }




        }
        catch (Exception e)
        {
            writeLog(e);
        }
        return tb;
    }


    protected bool OutDataListJC_SaveRowData(DataObject objects, bool isNew)
    {


        if (JEmpNo.ValueText == "")
        {
            MessageBox("必須選擇員工");
            return false; ;
        }
        if (Date.ValueText == "")
        {
            MessageBox("必須選擇日期");
            return false;
        }
        if (JNo.ValueText == "請選擇")
        {
            MessageBox("必須選擇獎懲類別");
            return false;
        }
        if (Reson.ValueText == "")
        {
            MessageBox("必須填寫申請事由");
            return false;
        }

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);


        try
        {
            DataObject[] objs = OutDataListJC.dataSource.getCurrentPageObjects();
            for (int i = 0; i < objs.Length; i++)
            {
                if (JEmpNo.ValueText == objs[i].getData("JEmpNo") && Date.ValueText == objs[i].getData("Date"))
                {
                    MessageBox("已有重複資料");
                    return false;
                }
                if (JNo.ValueText == "嘉獎" && (objs[i].getData("JNo") == "申戒" || objs[i].getData("JNo") == "小過"))
                {
                    MessageBox("獎勵與大過類不可與申戒和小過一同申請");
                    return false;
                }
                if (JNo.ValueText == "小功" && (objs[i].getData("JNo") == "申戒" || objs[i].getData("JNo") == "小過"))
                {
                    MessageBox("獎勵與大過類不可與申戒和小過一同申請");
                    return false;
                }
                if (JNo.ValueText == "大功" && (objs[i].getData("JNo") == "申戒" || objs[i].getData("JNo") == "小過"))
                {
                    MessageBox("獎勵與大過類不可與申戒和小過一同申請");
                    return false;
                }
                if (JNo.ValueText == "大過" && (objs[i].getData("JNo") == "申戒" || objs[i].getData("JNo") == "小過"))
                {
                    MessageBox("獎勵與大過類不可與申戒和小過一同申請");
                    return false;
                }
                if (JNo.ValueText == "申戒" && (objs[i].getData("JNo") == "嘉獎" || objs[i].getData("JNo") == "小功" || objs[i].getData("JNo") == "大功" || objs[i].getData("JNo") == "大過"))
                {
                    MessageBox("獎勵與大過類不可與申戒和小過一同申請");
                    return false;
                }
                if (JNo.ValueText == "小過" && (objs[i].getData("JNo") == "嘉獎" || objs[i].getData("JNo") == "小功" || objs[i].getData("JNo") == "大功" || objs[i].getData("JNo") == "大過"))
                {
                    MessageBox("獎勵與大過類不可與申戒和小過一同申請");
                    return false;
                }
            }
            if (retBool(engine, JEmpNo.ValueText, Date.ValueText))
            {
                MessageBox("資料庫已有重複資料");
                return false;
            }
            else
            {
                Hashtable hs = selectjc(engine, JEmpNo.ValueText);
                string month = "";
                string yy = "";

                if (hs["dg"].ToString() == "" && hs["xg"].ToString() == "" && hs["jj"].ToString() == "" && hs["dgg"].ToString() == "" && hs["xgg"].ToString() == "" && hs["sj"].ToString() == "")
                {
                    month = "無";
                }
                else
                {
                    month = "大功:" + hs["dg"].ToString() + "次" + ";"
                           + "小功:" + hs["xg"].ToString() + "次" + ";"
                           + "嘉獎:" + hs["jj"].ToString() + "次" + ";"
                           + "大過:" + hs["dgg"].ToString() + "次" + ";"
                           + "小過:" + hs["xgg"].ToString() + "次" + ";"
                           + "申戒:" + hs["sj"].ToString() + "次";
                }
                if (hs["a"].ToString() == "" && hs["b"].ToString() == "" && hs["c"].ToString() == "" && hs["d"].ToString() == "" && hs["e"].ToString() == "" && hs["f"].ToString() == "")
                {
                    yy = "無";
                }
                else
                {

                    yy = "大功:" + hs["a"].ToString() + "次" + ";"
                             + "小功:" + hs["b"].ToString() + "次" + ";"
                             + "嘉獎:" + hs["c"].ToString() + "次" + ";"
                             + "大過:" + hs["d"].ToString() + "次" + ";"
                             + "小過:" + hs["e"].ToString() + "次" + ";"
                             + "申戒:" + hs["f"].ToString() + "次";
                }


                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("SheetNo", base.PageUniqueID);
                objects.setData("JEmpNo", JEmpNo.ValueText);
                objects.setData("JEmpName", JEmpNo.ReadOnlyValueText);
                objects.setData("JPartNo", JPartNo.ValueText);
                objects.setData("Date", Date.ValueText);
                objects.setData("JNo", JNo.ValueText);
                objects.setData("CS", CS.ValueText);
                objects.setData("Money", Money.ValueText);
                objects.setData("Reson", Reson.ValueText);
                objects.setData("month", month);
                objects.setData("yy", yy);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        finally { OutDataListJC.updateTable(); try { engine.close(); } catch { } }

        return true;
    }

    public bool retBool(AbstractEngine engine, string JEmpNo, string datetime)
    {
        string strSql = @"select a.*,b.IS_LOCK from [EH020101B] a,[EH020101A] b where a.SheetNo=b.SheetNo and JEmpNo='" + JEmpNo + "' and Date='" + datetime + "' and IS_LOCK<>'Y'";
        DataTable hst = engine.getDataSet(strSql, "bmxz").Tables["bmxz"];
        DateTime dt1 = Convert.ToDateTime(datetime);
        if (hst != null && hst.Rows.Count > 0)
        {
            for (int j = 0; j < hst.Rows.Count; j++)
            {
                if (Convert.ToDateTime(hst.Rows[j]["Date"].ToString()) == dt1)
                {
                    return true;
                }

            }
        }
        return false;
    }
    protected void FileUpload1_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
        try
        {
            FileUpload1.CheckData(0);
            FileUpload1.updateTable();
            DSCWebControl.FileItem[] fi = FileUpload1.getSelectedItem();
            //  MessageBox(fi[0].FILEPATH);




            DataSet ds = new DataSet();
            //构建连接字符串
            string path = Server.MapPath(@"../../../tempFolder/JC/" + fi[0].FILEPATH); //@"C:\ECP\WebFormPT\web\tempFolder\OWork\c54a57e3-cc4f-402f-a19b-12264ce473ea.xls"; 

            //string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
            string ConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "; Extended Properties=\"Excel 12.0;HDR=No\""; //HDR=NO 第一行不是标题，是数据。

            //  MessageBox(ConnStr);
            OleDbConnection Conn = new OleDbConnection(ConnStr);
            Conn.Open();
            //填充数据到表
            string sql = string.Format("select * from [{0}$]", "Sheet1");
            OleDbDataAdapter da = new OleDbDataAdapter(sql, ConnStr);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];


            //刪除頁面數據
            OutDataListJC.dataSource.clear();

            //關閉鏈接刪除文件
            Conn.Close();
            System.IO.File.Delete(path);

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            int i = 0;
            ArrayList ar = new ArrayList();
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                if (retBool(engine, dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString()) || retBool1(engine, dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString()) || retBool3(dt.Rows[j][2].ToString()))
                {
                    i++;
                    continue;
                }
                else
                {
                    DataObject obj = new DataObject();
                    obj.loadFileSchema(OutDataListJC.dataSource.getChildClassString());
                    obj.setData("GUID", IDProcessor.getID(""));
                    obj.setData("SheetNo", base.PageUniqueID);
                    obj.setData("JEmpNo", dt.Rows[j][0].ToString());
                    obj.setData("Date", dt.Rows[j][1].ToString());
                    obj.setData("JNo", dt.Rows[j][2].ToString());
                    obj.setData("CS", dt.Rows[j][3].ToString());
                    obj.setData("Money", dt.Rows[j][4].ToString());
                    obj.setData("Reson", dt.Rows[j][5].ToString());
                    Hashtable h1 = selectpid(engine, dt.Rows[j][0].ToString());
                    obj.setData("JEmpName", h1["EmpName"].ToString());
                    obj.setData("JPartNo", h1["PartNo"].ToString());

                    Hashtable hs = selectjc(engine, dt.Rows[j][0].ToString());
                    string month = "";
                    string yy = "";

                    if (hs["dg"].ToString() == "" && hs["xg"].ToString() == "" && hs["jj"].ToString() == "" && hs["dgg"].ToString() == "" && hs["xgg"].ToString() == "" && hs["sj"].ToString() == "")
                    {
                        month = "無";
                    }
                    else
                    {
                        month = "大功:" + hs["dg"].ToString() + "次" + ";"
                               + "小功:" + hs["xg"].ToString() + "次" + ";"
                               + "嘉獎:" + hs["jj"].ToString() + "次" + ";"
                               + "大過:" + hs["dgg"].ToString() + "次" + ";"
                               + "小過:" + hs["xgg"].ToString() + "次" + ";"
                               + "申戒:" + hs["sj"].ToString() + "次";
                    }
                    if (hs["a"].ToString() == "" && hs["b"].ToString() == "" && hs["c"].ToString() == "" && hs["d"].ToString() == "" && hs["e"].ToString() == "" && hs["f"].ToString() == "")
                    {
                        yy = "無";
                    }
                    else
                    {

                        yy = "大功:" + hs["a"].ToString() + "次" + ";"
                                 + "小功:" + hs["b"].ToString() + "次" + ";"
                                 + "嘉獎:" + hs["c"].ToString() + "次" + ";"
                                 + "大過:" + hs["d"].ToString() + "次" + ";"
                                 + "小過:" + hs["e"].ToString() + "次" + ";"
                                 + "申戒:" + hs["f"].ToString() + "次";
                    }
                    obj.setData("month", month);
                    obj.setData("yy", yy);

                    OutDataListJC.dataSource.add(obj);
                }
            }
            OutDataListJC.dataSource.sort(new string[,] { { "JEmpNo", DataObjectConstants.ASC }, { "Date", DataObjectConstants.ASC } });
            OutDataListJC.dataSource.compact();
            OutDataListJC.dataSource.compact();
            OutDataListJC.updateTable();
            MessageBox("去除重複/異常項:" + i.ToString());
        }
        catch (Exception exc)
        {
            MessageBox(exc.Message);
        }
        FileUpload1.dataSource.clear();
        FileUpload1.updateTable();
    }

    public Hashtable selectpid(AbstractEngine engine, string EmpNo)
    {

        Hashtable h1 = new Hashtable();
        h1.Add("EmpName", "");
        h1.Add("PartNo", "");

        try
        {
            string sql = @"SELECT EmpNo,EmpName,PartNo FROM [HRUSERS] WHERE [EmpNo]='" + EmpNo + "'";

            DataRow dr = engine.getDataSet(sql, "hrsk").Tables["hrsk"].Rows[0];

            h1["EmpName"] = dr["EmpName"].ToString();
            h1["PartNo"] = dr["PartNo"].ToString();

        }
        catch { }
        return h1;
    }

    public bool retBool1(AbstractEngine engine, string EmpNo, string Day)
    {
        bool ret = false;
        DataObject[] objs = OutDataListJC.dataSource.getAllDataObjects();
        for (int i = 0; i < objs.Length; i++)
        {
            if (EmpNo == objs[i].getData("JEmpNo") && Day == objs[i].getData("Date"))
            {
                ret = true;
            }
        }

        return ret;
    }

    public bool retBool2(AbstractEngine engine, string EmpNo)
    {
       bool rest = false;
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + si.fillerID + "'";
            DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];

            string sqlbmgr = @"select PartNo from HRUSERS where EmpNo ='" + EmpNo + "'";
            DataTable dtbmgr = engine.getDataSet(sqlbmgr, "bmEgr").Tables["bmEgr"];

            if (si.fillerOrgID == "GQC2200" || si.fillerOrgID == "GIC2200" )
                {
                    rest = false;
                }
                else
                {
                    for (int i = 0; i < dtbm.Rows.Count; i++)
                    {
                        if (dtbmgr.Rows[0][0].ToString().Trim() == dtbm.Rows[i][0].ToString().Trim())
                        {
                            rest = false;
                        }
                        else
                        {
                            rest = true;
                        }
                    }
                }

        }
        catch (Exception e) { return true; }
        return rest;
    }
    public bool retBool3(string jNoString)
    {
        bool ret = false;
        string J1 = "嘉獎";
        string J2 = "小功";
        string J3 = "大功";
        string J4 = "申戒";
        string J5 = "小過";
        string J6 = "大過";

        if (jNoString != J1 && jNoString != J2 && jNoString != J3 && jNoString != J4 && jNoString != J5 && jNoString != J6)
        {
            ret = true;
        }


        return ret;
    }
}
