

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

public partial class Program_SCQ_Form_EG0106_Form : SmpBasicFormPage
{
    private int MaxDSC = 100;
    private string DSCName = "DSCCheckBox";
    protected override void init()
    {
        ProcessPageID = "EG0106";
        AgentSchema = "WebServerProject.form.EG0106.EG0106Agent";
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
        string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + Session["UserID"].ToString() + "'";
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

        for (int i = 1; i <= MaxDSC; i++)
        {
            DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
            if (dsc.Text == "保留")
            {
                dsc.Visible = false;
            }
        }

        ///初始化上傳控件
        try
        {
            SysParam ssp = new SysParam(engine);
            // string fileAdapter = ssp.getParam("FileAdapter");
            FileUpload1.FileAdapter = "加班文件|*.xls";

            FileUpload1.engine = engine;
            FileUpload1.tempFolder = Server.MapPath("~/tempFolder/Door/");//加班臨時資料夾
            FileUpload1.readFile("");
            FileUpload1.updateTable();
        }
        catch { }

        if (base.isNew())
        {
            DataObjectSet dos = new DataObjectSet();
            dos.isNameLess = true;
            dos.setAssemblyName("WebServerProject");
            dos.setChildClassString("WebServerProject.form.EG0106.EG0106B");
            dos.setTableName("EG0106B");
            dos.loadFileSchema();
            objects.setChild("EG0106B", dos);
            ApplicationList.dataSource = dos;
            ApplicationList.updateTable();
        }

        ApplicationList.clientEngineType = engineType;
        ApplicationList.connectDBString = connectString;
        ApplicationList.HiddenField = new string[] { "GUID", "SheetNo" };

        if (isNew())
        {
            string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	
                
            };
            sqzszg.setListItem(xgqhzg);
            ///
            if (si.fillerOrgID == "NQM010101")
            {
                showzg.Visible = true;
            }
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

        string check = objects.getData("Privilege");
        for (int i = 1; i <= MaxDSC; i++)
        {
            DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
            if (dsc != null && i <= check.Length && check.Substring(i - 1, 1) == "Y")
            {
                dsc.Checked = true;
            }
            if (dsc != null)
            {
                dsc.ReadOnly = true;
            }
        }

        Note.ValueText = objects.getData("Note");
        ApplicationList.dataSource = objects.getChild("EG0106B");
        ApplicationList.updateTable();

        ApplicationList.IsShowCheckBox = false;
        ApplicationList.ReadOnly = true;
        Note.ReadOnly = true;
        EditPanel.Visible = false;
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
            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

            objects.setData("GUID", IDProcessor.getID(""));
            string p = string.Empty;
            for (int i = 1; i <= MaxDSC; i++)
            {
                DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
                if (dsc != null && dsc.Checked)
                {
                    p += "Y";
                }
                else
                {
                    p += "N";
                }
            }
            objects.setData("Privilege", p);
            objects.setData("Note", Note.ValueText);
            objects.setData("Forward", "N");
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");

            //將B表匯進來
            foreach (DataObject obj in ApplicationList.dataSource.getAllDataObjects())
            {
                obj.setData("SheetNo", objects.getData("SheetNo"));
            }

            objects.setChild("EG0106B", ApplicationList.dataSource);
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
        if (base.isNew())
        {
            if (isNecessary(Note))
            {
                if (Note.ValueText.Equals(""))
                {
                    pushErrorMessage("必須填寫申請說明");
                    result = false;
                }
            }
            if (ApplicationList.dataSource.getAvailableDataObjectCount() == 0)
            {
                pushErrorMessage("必須填寫申請人");
                result = false;
            }

            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
            {
                pushErrorMessage("MFG-1 部門 請選擇簽核主管");
                result = false;
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
        string xml = "";
        try
        {
            string isqh = "";
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            string managerId = "";
            string[] values = base.getUserManagerInfoID(engine, si.fillerID);
            if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText != "0")
            {
                if (sqzszg.ValueText == "1")
                {
                    managerId = "Q1100135";
                }
                else if (sqzszg.ValueText == "2")
                {
                    managerId = "Q1608418";
                }
                else if (sqzszg.ValueText == "3")
                {
                    managerId = "Q1210122";
                }
            }
            else
            {

                managerId = values[1];  //申請人的主管 工號

            }
            if (DSCCheckBox3.Checked || DSCCheckBox4.Checked || DSCCheckBox5.Checked || DSCCheckBox6.Checked)
            {
                isqh = "1";
            }

            xml += "<EG0109>";
            xml += "<managerID DataType=\"java.lang.String\">" + managerId + "</managerID>";//直屬主管    
            xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>"; //是否簽核  1=部級；2=處級;3=總經理；4=董事長     
            xml += "</EG0109>";

        }
        catch { }


        param["EG0109"] = xml;
        return "EG0109";
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
        if (result == "Y")
        {
            //將Forward改為R
            currentObject.setData("Forward", "R");
            string id = currentObject.getData("SheetNo");
            bool rc = engine.updateData(currentObject);
            if (rc)
            {
                throw new Exception("更新Forward資料完成. 單號: " + id);
            }
            else
            {
                throw new Exception("更新Forward資料錯誤. 單號: " + id);
            }
        }
    }

    /// <summary>
    /// 選擇請假人員
    /// </summary>
    /// <param name="values"></param>
    protected void EmpNo_SingleOpenWindowButtonClick(string[,] values)
    {
        if (values == null)
            return;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        Hashtable h1 = base.getHRUsers(engine, EmpNo.ValueText);
        PartNo.ValueText = h1["PartNo"].ToString();
        DtName.ValueText = h1["DtName"].ToString();
        Note2.ValueText = h1["PID"].ToString();
    }

    protected void AddData_Click(object sender, EventArgs e)
    {
        if (PartNo.ValueText == "")
        {
            MessageBox("必須填寫部門");
            return;
        }
        if (DtName.ValueText == "")
        {
            MessageBox("必須填寫職稱");
            return;
        }
        if (Reason.ValueText == "")
        {
            MessageBox("必須填寫申請事由");
            return;
        }
        DataObject obj = new DataObject();
        obj.loadFileSchema(ApplicationList.dataSource.getChildClassString());
        obj.setData("GUID", IDProcessor.getID(""));
        obj.setData("SheetNo", base.PageUniqueID);
        obj.setData("EmpNo", EmpNo.ValueText);
        obj.setData("EmpName", EmpNo.ReadOnlyValueText);
        obj.setData("PartNo", PartNo.ValueText);
        obj.setData("DtName", DtName.ValueText);
        obj.setData("Reason", Reason.ValueText);
        obj.setData("Note", Note2.ValueText);
        ApplicationList.dataSource.add(obj);

        ApplicationList.updateTable();
    }

    protected void ApplicationList_setClickData(string clickList)
    {
        DataObject[] objs = ApplicationList.dataSource.getAllDataObjects();
        //LeaveDateList.dataSource.clear();
        for (int i = 0; i < objs.Length; i++)
        {
            if (clickList.Substring(i, 1) == "Y")
            {
                ApplicationList.dataSource.delete(objs[i]);
            }
        }
        ApplicationList.dataSource.compact();
        ApplicationList.updateTable();
    }
    public Hashtable selectpid(AbstractEngine engine , string EmpNo)
    {

        Hashtable h1 = new Hashtable();         
        h1.Add("PID", "");
        h1.Add("EmpName", "");
        h1.Add("PartNo", "");
        h1.Add("DtName", "");
        
        try
        {
            string sql = @"SELECT EmpNo,PID,EmpName,PartNo,DtName FROM [HRUSERS] WHERE [EmpNo]='" + EmpNo + "'";

            DataRow dr = engine.getDataSet(sql, "hrsk").Tables["hrsk"].Rows[0];


            h1["PID"] = dr["PID"].ToString();
            h1["EmpName"] = dr["EmpName"].ToString();
            h1["PartNo"] = dr["PartNo"].ToString();
            h1["DtName"] = dr["DtName"].ToString();
            
        }
        catch { }
        return h1;
    }
    protected void FileUpload1_AddOutline(FileItem currentObject, bool isNew)
    {
        try
        {
            FileUpload1.CheckData(0);
            FileUpload1.updateTable();
            DSCWebControl.FileItem[] fi = FileUpload1.getSelectedItem();
            //  MessageBox(fi[0].FILEPATH);




            DataSet ds = new DataSet();
            //构建连接字符串
            string path = Server.MapPath(@"../../../tempFolder/Door/" + fi[0].FILEPATH); //@"C:\ECP\WebFormPT\web\tempFolder\OWork\c54a57e3-cc4f-402f-a19b-12264ce473ea.xls"; 

            string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";

            //  MessageBox(ConnStr);
            OleDbConnection Conn = new OleDbConnection(ConnStr);
            Conn.Open();
            //填充数据到表
            string sql = string.Format("select * from [{0}$]", "Sheet1");
            OleDbDataAdapter da = new OleDbDataAdapter(sql, ConnStr);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];


            //刪除頁面數據
            ApplicationList.dataSource.clear();

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
                 if (retBool2(engine, dt.Rows[j][0].ToString()))
                {	i++;
                    continue;
                }
                else
                {
                DataObject obj = new DataObject();
                obj.loadFileSchema(ApplicationList.dataSource.getChildClassString());
                obj.setData("GUID", IDProcessor.getID(""));
                obj.setData("SheetNo", base.PageUniqueID);
                obj.setData("EmpNo", dt.Rows[j][0].ToString());
                //obj.setData("EmpName", dt.Rows[j][1].ToString());
                //obj.setData("PartNo", dt.Rows[j][2].ToString());
                //obj.setData("DtName", dt.Rows[j][3].ToString());
                obj.setData("Reason", dt.Rows[j][4].ToString());
                Hashtable h1 = selectpid(engine,dt.Rows[j][0].ToString());
                obj.setData("Note", h1["PID"].ToString());
                obj.setData("EmpName", h1["EmpName"].ToString());
                obj.setData("PartNo", h1["PartNo"].ToString());
                obj.setData("DtName", h1["DtName"].ToString());
                //obj.setData("Note", dt.Rows[j][5].ToString());
                

                //bool acg = false;

                //string[] str = new string[4] { dt.Rows[j][0].ToString(), dt.Rows[j][2].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][4].ToString() };

                ////for (int s = 0; s < ar.Count; s++)
                ////{
                ////    if (ischeak((string[])ar[s], str))
                ////    {
                ////        acg = true;
                ////    }
                ////}



                //if (acg)
                //{
                //    i++;
                //    continue;
                //}
                ApplicationList.dataSource.add(obj);
                //ar.Add(str);
                }
            }
            ApplicationList.dataSource.sort(new string[,] { { "EmpNo", DataObjectConstants.ASC }, { "PartNo", DataObjectConstants.ASC } });
            ApplicationList.dataSource.compact();
            ApplicationList.updateTable();
            MessageBox("去除重複/異常項:" + i.ToString());
        }
        catch (Exception exc)
        {
            MessageBox(exc.Message);
        }
        FileUpload1.dataSource.clear();
        FileUpload1.updateTable();
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

            for (int i = 0; i < dtbm.Rows.Count; i++)
            {
                if (dtbmgr.Rows[0][0].ToString().Trim() == dtbm.Rows[i][0].ToString().Trim())
                {
                    return false;

                }
                else
                {
                    rest = true;
                }
            }

        }
        catch (Exception e) { return true; }
        return rest;
    }
}
