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

public partial class Program_SCQ_Form_EM0115_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EM0115";
        AgentSchema = "WebServerProject.form.EM0115.EM0115Agent";
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

        EmpNo.clientEngineType = engineType;
        EmpNo.connectDBString = connectString;
        SheetNo.Display = false;
        Subject.Display = false;
	LbNum.ReadOnly = true;

        string sqlbm = @"select PartNo from   [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3 ='" + si.fillerID + "'";
        DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
        string SQLstr = "";
        try
        {
            if (dtbm.Rows[0][0].ToString() != "")
            {
                SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                for (int i = 1; i < dtbm.Rows.Count; i++)
                {
                    SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                } SQLstr = SQLstr + ")";
                EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


            }
            else
            {
                SQLstr = "1=2";
                EmpNo.whereClause = SQLstr;
            }

            EmpNo.DoEventWhenNoKeyIn = false;
        }
        catch
        {
            if (si.fillerOrgID != "")
            {
                SQLstr = "PartNo like '%" + si.fillerOrgID + "%'";
                EmpNo.whereClause = SQLstr;
            }
            else
            {
                SQLstr = "1=2";
                EmpNo.whereClause = SQLstr;
            }
            EmpNo.DoEventWhenNoKeyIn = false;
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
        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        LbNum.ValueText = objects.getData("LbNum");
        Owner.ValueText = objects.getData("Owner");
        CPNo.ValueText = objects.getData("CPNo");
        CPType.ValueText = objects.getData("CPType");
        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("EmpName");
        PartNo.ValueText = objects.getData("PartNo");
        Reason.ValueText = objects.getData("Reason");

        base.showData(engine, objects);


        //顯示發起資料
        CPNo.ReadOnly = true;
        CPType.ReadOnly = true;
        CPNo.ReadOnly = true;
        EmpNo.ReadOnly = true;
        PartNo.ReadOnly = true;
        Reason.ReadOnly = true;
        LbNum.ReadOnly = true;
        Owner.ReadOnly = true;

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
            objects.setData("CPNo", CPNo.ValueText);
            objects.setData("CPType", CPType.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("EmpName", EmpNo.ReadOnlyValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("Owner", Owner.ValueText);
            objects.setData("Reason", Reason.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");


            //產生單號並儲存至資料物件
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
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 筆電放行標籤申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
            }
        }

        if (Reason.ValueText.Length <20)
        {
            MessageBox("申請原因不得少於20字描述");
            result = false;
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
        //string actName = (string)getSession("ACTName");
        //bool rc = false;
        //if (base.isNew())
        //{
        //    if (actName.Equals("部門主管"))
        //    {
        //        //if (isManager(engine, RequestID.ValueText))
        //        //{
        //        //    param["check1"] = "Y";
        //        //    rc = true;
        //        //}
        //    }
        //}

        ////Return有值才會變更變數
        //if (rc)
        //    return "Y";
        //else
            return "";
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
        

        string actName = Convert.ToString(getSession("ACTName"));

        //writeLog("result  : " + result + actName);

        if (actName.Equals("總經理核准") && result.Equals("同意"))
        {
            writeLog("result1  : " + result + actName);
            UpdateLSM(engine, currentObject);
        }

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
        base.afterApprove(engine, currentObject, result);
        //"Y"代表同意
        if (result == "Y")
        {
            //開發同意簽核後自動化流程
            //UpdateLSM(engine, currentObject);
            //string sql = "update SMWYAAA set SMWYAAA021 = replace(SMWYAAA021,'<F fn=\"LbNum\"></F>','<F fn=\"LbNum\">"+15+"</F>') where SMWYAAA002 = '" + currentObject.getData("SheetNo") + "'";
            //string sql = "update SMWYAAA set SMWYAAA021 ='1' where SMWYAAA002 = '" + currentObject.getData("SheetNo") + "'";
            //engine.executeSQL(sql);
        }
        
    }
    protected void EmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        try
        {

            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
            PartNo.ValueText = h1["PartNo"].ToString();


        }
        catch (Exception ex)
        {
            throw new Exception("查詢HRUSERS錯誤, " + ex.Message.ToString());
        }
    }
    /// <summary>
    /// afterprove 更新資料
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected void UpdateLSM(AbstractEngine engine, DataObject currentObject)
    {     
        string LSM = "";
        string sql1 = @"select max(LbNum) LbNum from dbo.EM0115A";
        DataTable tTb = engine.getDataSet(sql1, "data").Tables["data"];
        string ls = tTb.Rows[0]["LbNum"].ToString().Trim();
        if (string.IsNullOrEmpty(ls))
        {
            LSM = "0001";
        }
        else
        {
            string str = Convert.ToString(Convert.ToInt32(ls) + 1);
            LSM = str.PadLeft(4, '0');
        }
        //writeLog("LSM  : " + LSM);
        string sql = "Update EM0115A set LbNum ='" + LSM + "' where SheetNo = '" + currentObject.getData("SheetNo") + "'";
        //writeLog("sql  : " + sql);
        engine.executeSQL(sql);

        //string sql2 = "update SMWYAAA set SMWYAAA021 = '' where SMWYAAA002 = '" + currentObject.getData("SheetNo") + "'";
        //engine.executeSQL(sql2);

    }

    /// <summary>
    /// 日誌
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\EM0115.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }

    protected void CPNo_TextChanged(object sender, EventArgs e)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        Hashtable hs = selectAsset(engine, CPNo.ValueText);
        Owner.ValueText = hs["Owner"].ToString();
    }
    public Hashtable selectAsset(AbstractEngine engine, string CCBH)
    {
        Hashtable hs = new Hashtable();
        hs.Add("Gwtype", "");
        hs.Add("Gwname", "");
        try
        {
            string sql = @"select FABOOK,AssetNo,EqName,Convert(nvarchar(10),YYMMDD,23)YYMMDD,PartNo,PartName,Owner,'台' dw,Counts from [10.3.11.96].MIS_DB.dbo.AssetList where AssetNo='" + CCBH + "' ";

            DataRow dr = engine.getDataSet(sql, "hrsk").Tables["hrsk"].Rows[0];

            hs["FABOOK"] = dr["FABOOK"].ToString();
            hs["AssetNo"] = dr["AssetNo"].ToString();
            hs["EqName"] = dr["EqName"].ToString();
            hs["YYMMDD"] = dr["YYMMDD"].ToString();
            hs["PartNo"] = dr["PartNo"].ToString();
            hs["PartName"] = dr["PartName"].ToString();
            hs["Owner"] = dr["Owner"].ToString();
            hs["dw"] = dr["dw"].ToString();
            hs["Counts"] = dr["Counts"].ToString();

        }
        catch { }
        return hs;
    }
}
