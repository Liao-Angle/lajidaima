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

public partial class Program_SCQ_Form_EG0101_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "EG0101";
        AgentSchema = "WebServerProject.form.EG0101.EG0101Agent";
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
        






  try
                {

                      string sqlbm = @"select PartNo from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where MyField3='" + Session["UserID"].ToString() + "'";
                      DataTable dtbm = engine.getDataSet(sqlbm, "bmE").Tables["bmE"];
                      string SQLstr = "";
                      if (dtbm.Rows[0][0].ToString() != "")
                      {
                            SQLstr = "(PartNo = '" + dtbm.Rows[0][0].ToString() + "' ";
                            for (int i = 1; i < dtbm.Rows.Count; i++)
                            {
                                  SQLstr = SQLstr + " or PartNo = '" + dtbm.Rows[i][0].ToString() + "' ";
                            }
SQLstr =SQLstr +")";
                            EmpNo.whereClause = SQLstr;//and EmpTypeName like '不限%'


                      }
                      else
                      {
                            EmpNo.whereClause = "1=2";
                      }

                      EmpNo.DoEventWhenNoKeyIn = false;
                }
                catch {
                      if (si.fillerOrgID != "")
                      {
                            EmpNo.whereClause = "PartNo like '%" + si.fillerOrgID + "%'";
                      }
                      else
                      {
                            EmpNo.whereClause = "1=2";
                      }
                      EmpNo.DoEventWhenNoKeyIn = false;
                }

        string[,] ids = new string[9, 2];
        for (int i = 1; i < 10; i++)
        {
            ids[i - 1, 0] = i.ToString();
            ids[i - 1, 1] = i.ToString();
        }
        Boxes.setListItem(ids);




        if (isNew())
        {
            string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	
                {"3","許文坤"},};
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
  

        EmpNo.ValueText = objects.getData("EmpNo");
        EmpNo.ReadOnlyValueText = objects.getData("CName");
        EName.ValueText = objects.getData("EName");
        CDepartment.ValueText = objects.getData("CDepartment");
        EDepartment.ValueText = objects.getData("EDepartment");
        CTitle.ValueText = objects.getData("CTitle");
        ETitle.ValueText = objects.getData("ETitle");
        Extension.ValueText = objects.getData("Extension");
        Mobile.ValueText = objects.getData("Mobile");
        EMail.ValueText = objects.getData("EMail");
        Boxes.ValueText = objects.getData("Boxes");
        Subject.ValueText = objects.getData("Subject");
        //顯示單號
        base.showData(engine, objects);

        EmpNo.ReadOnly = true;
        EName.ReadOnly = true;
        CDepartment.ReadOnly = true;
        EDepartment.ReadOnly = true;
        CTitle.ReadOnly = true;
        ETitle.ReadOnly = true;
        Extension.ReadOnly = true;
        Mobile.ReadOnly = true;
        EMail.ReadOnly = true;
        Boxes.ReadOnly = true;
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
            objects.setData("Subject", Subject.ValueText);
            objects.setData("EmpNo", EmpNo.ValueText);
            objects.setData("CName", EmpNo.ReadOnlyValueText);
            objects.setData("EName", EName.ValueText);
            objects.setData("CDepartment", CDepartment.ValueText);
            objects.setData("EDepartment", EDepartment.ValueText);
            objects.setData("CTitle", CTitle.ValueText);
            objects.setData("ETitle", ETitle.ValueText);
            objects.setData("Extension", Extension.ValueText);
            objects.setData("Mobile", Mobile.ValueText);
            objects.setData("EMail", EMail.ValueText);
            objects.setData("Boxes", Boxes.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");            
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
        if (isNecessary(EmpNo))
        {
            if (EmpNo.GuidValueText.Equals(""))
            {
                pushErrorMessage("必須選擇員工");
                //pushErrorMessage(com.dsc.locale.LocaleString.getSystemMessageString("Program_SCQ_Form_EG0101_Form_aspx.language", "message", "QueryError1", "必須選擇請假人"));
                result = false;
            }
        }
        if (isNecessary(EName))
        {
            if (EName.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入英文名字");
                result = false;
            }
        }
        if (isNecessary(CDepartment))
        {
            if (CDepartment.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入中文部門名稱");
                result = false;
            }
        }
        if (isNecessary(EDepartment))
        {
            if (EDepartment.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入英文部門名稱");
                result = false;
            }
        }
        if (isNecessary(CTitle))
        {
            if (CTitle.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入中文職稱");
                result = false;
            }
        }
        if (isNecessary(ETitle))
        {
            if (ETitle.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入英文職稱");
                result = false;
            }
        }
        if (isNecessary(Extension))
        {
            if (Extension.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入分機號碼");
                result = false;
            }
        }
        if (isNecessary(Mobile))
        {
            if (Mobile.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入行動電話");
                result = false;
            }
        }
        if (isNecessary(EMail))
        {
            if (EMail.ValueText.Equals(""))
            {
                pushErrorMessage("必須輸入電子郵件");
                result = false;
            }
        }


        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
        {
            pushErrorMessage("MFG-1 部門 請選擇簽核主管");
            result = false;
        }
     }

        //設定主旨
        //設定主旨
        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【名片申請單】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
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
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            //填表人
            string creatorId = si.fillerID;
            string isqh = "1";
            string managerId = "";
            string mrgmrgId = "Q1508126";//固定需要簽核到CC
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

            xml += "<EG0101>";
            xml += "<managerID DataType=\"java.lang.String\">" + managerId + "</managerID>";
            xml += "<mrgmrgID DataType=\"java.lang.String\">" + mrgmrgId + "</mrgmrgID>";
            xml += "<isqh DataType=\"java.lang.String\">" + isqh + "</isqh>";
            xml += "</EG0101>";

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
        }
        //表單代號
        param["EG0101"] = xml;
        return "EG0101";
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
            //將ForwardHR改為R
            currentObject.setData("ForwardHR", "R");
            string id = currentObject.getData("SheetNo");
            if (!engine.updateData(currentObject))
            {
                throw new Exception("更新EG0101 ForwardHR資料錯誤. 單號: " + id);
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
        UpdateData(EmpNo.ValueText);
    }

    private void UpdateData(string id)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        Hashtable h1 = base.getHRUsers(engine, id);
        CDepartment.ValueText = h1["PartNo"].ToString();
        EDepartment.ValueText = h1["PartNo"].ToString();
        CTitle.ValueText = h1["DtName"].ToString();
        ETitle.ValueText = h1["DtName"].ToString();
        Mobile.ValueText = h1["Mobile"].ToString();

        Hashtable h2 = base.getADUserData(engine, id);
        EMail.ValueText = h2["mail"].ToString();
        EName.ValueText = h2["mailnickname"].ToString();
        Extension.ValueText = h2["telephonenumber"].ToString();
    }
}
