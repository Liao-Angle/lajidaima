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
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using WebServerProject.system.login;
using com.dsc.kernal.logon;
using com.dsc.kernal.agent;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM004_Input : BaseWebUI.GeneralWebPage
{
    string[] encodeFields = new string[] { "SelfScore_W", "FirstScore_W", "SecondScore_W" };
	
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPPM004M";
        ApplicationID = "SYSTEM";
        ModuleID = "SPPM";

        base.OnInit(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {		
		if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string funcId = Request.QueryString["FuncId"];
                string objectGUID = Request.QueryString["ObjectGUID"];
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                string[,] ids = null;
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定考核計畫
                ids = SmpPmMaintainUtil.getAssessmentName(engine);
                AssessmentPlanGUID.setListItem(ids);
                
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                Status.setListItem(ids);
                //if (string.IsNullOrEmpty(Status.ValueText))
                //{
                  //  Status.ValueText = SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN;
                //}
                
                string userGUID = (string)Session["UserId"];
                string whereClause = "AssessUserGUID='" + userGUID + "'";
                if (funcId.Equals("0"))
                {
                    whereClause += " and Stage='0'";
                    lblAccount.Display = true;
                    sfAccount.Display = true;
                    lblPassword.Display = true;
                    sfPassword.Display = true;
                }
                else
                {
                    whereClause += " and Stage <>'0'";                   
                    lblAccount.Display = false;
                    sfAccount.Display = false;
                    lblPassword.Display = false;
                    sfPassword.Display = false;
                }

                //whereClause += " and Status='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN + "'";
                //queryData(whereClause);
                GbSearch_Click(sender, e);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSearch_Click(object sender, EventArgs e)
    {
        string funcId = Request.QueryString["FuncId"];
        string userGUID = "";
        if (funcId.Equals("0"))
        {
            userGUID = sfAccount.ValueText;//于20170803Marcia修改以便沒有帳號人員可以績效考評
        }
        else
        {
            userGUID = (string)Session["UserId"];
        }
       
        string whereClause = "AssessUserGUID='" + userGUID + "'";

        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText))
        {
            whereClause += " and AssessmentPlanGUID in (" + SmpPmMaintainUtil.GetfilterNameplanGUID(AssessmentPlanGUID.ValueText) + ")";
        }
        if (!string.IsNullOrEmpty(Status.ValueText))
        {
            whereClause += " and Status='" + Status.ValueText + "'";
        }
        if (funcId.Equals("0"))
        {
            whereClause += " and Stage='" + SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION + "' and empNumber='"+sfAccount.ValueText+"' and '"+sfPassword.ValueText+"' =(select right(IDCardNo,6) from [10.3.11.92\\SQL2008].SCQHRDB.dbo.PerEmployee where EmpNo collate chinese_taiwan_stroke_ci_as=empNumber)";
        }
        else
        {
            whereClause += " and Stage <>'" + SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION + "' ";
        }

        queryData(whereClause);
    }

    protected string GetUserGUID(string EmpNo)
    {
        AbstractEngine engine = null;
        string EmpGUID = string.Empty;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        engine = factory.getEngine(engineType, connectString);
        string sqltxt = "select empGUID from SmpHrEmployeeInfoV where empNumber='"+EmpNo+"'";
        object str = engine.executeScalar(sqltxt);
        if (str != null)
        {
            EmpGUID = str.ToString();
        }
        return EmpGUID;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="whereClause"></param>
    protected void queryData(string whereClause)
    {
	    AbstractEngine engine = null;

        try
        {
            string funcId = Request.QueryString["FuncId"];
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM004.SmpPmAssessmentUserScoreAgent");
            agent.engine = engine;
            agent.query(whereClause);
            DataObjectSet set = agent.defaultData;
            SmpPmMaintainUtil.getDecodeValue(set, encodeFields);
            AssessmentStageList.InputForm = "Input.aspx";
            AssessmentStageList.DialogHeight = 600;
            AssessmentStageList.DialogWidth = 1000;
            if (funcId.Equals("0"))
            {
                AssessmentStageList.HiddenField = new string[] { "GUID", "UserGUID", "empNumber", "titleName", "ZD", "AssessmentPlanGUID", "AssessmentDays", "AssessmentManagerGUID", "EvaluationGUID", "EvaluationName", "SelfScore", "SelfComments", "FirstScore", "FirstScore_W", "FirstComments", "SecondScore", "SecondScore_W", "SecondComments", "FinalScore", "FinalComments", "AchievementLevel", "AssessUserGUID", "UserAssessmentStageGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            }
            else
            {
                AssessmentStageList.HiddenField = new string[] { "GUID", "UserGUID", "empNumber", "AssessmentPlanGUID", "AssessmentDays", "AssessmentManagerGUID", "EvaluationGUID", "EvaluationName", "SelfScore", "SelfScore_W", "SelfComments", "FirstComments", "FirstScore", "SecondScore", "SecondScore_W", "SecondComments", "FinalScore", "FinalComments", "AchievementLevel", "AssessUserGUID", "UserAssessmentStageGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            }
            AssessmentStageList.dataSource = set;

            for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
            {
                DataObject data = set.getAvailableDataObject(i);
                string stage = data.getData("Stage");
                if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
                {
                    AssessmentStageList.HiddenField = new string[] { "GUID", "UserGUID", "empNumber", "AssessmentPlanGUID", "AssessmentDays", "AssessmentManagerGUID", "EvaluationGUID", "EvaluationName", "SelfScore", "SelfComments", "FirstScore", "FirstComments", "SecondScore", "SecondComments", "FinalScore", "FinalComments", "AchievementLevel", "AssessUserGUID", "UserAssessmentStageGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
                    break;
                }
            }
            AssessmentStageList.updateTable();
        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
            writeLog(ex);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
    }

    public string checkScoreTotal(com.dsc.kernal.databean.DataObject objects)
    {
        //檢查大小過禁止評核等級
        IOFactory factory = null;
        AbstractEngine engine = null;
        string result = "";
        string errMsg = "";
        try
        {
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            string stage = objects.getData("Stage");
            string itemNo = objects.getData("empName");
            string firstScore = objects.getData("FirstScore_W");
            string secondScore = objects.getData("SecondScore_W");
            string MisTake1 = objects.getData("MisTake1");
            string MisTake2 = objects.getData("MisTake2");
            DataObject[] obj = AssessmentStageList.dataSource.getAllDataObjects();

            for (int i = 0; i < obj.Length; i++)
            {
                if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
                {
                    if (string.IsNullOrEmpty(firstScore))
                    {
                        result = itemNo + "未考核！";
                    }
                    else if (Convert.ToInt32(firstScore) >= 90 && (!string.IsNullOrEmpty(MisTake1) || !string.IsNullOrEmpty(MisTake2)))
                    {
                        result = itemNo + ":大小過人員不可評A;";
                    }
                    else if (Convert.ToInt32(firstScore) >= 80 && Convert.ToInt32(firstScore) < 90 && !string.IsNullOrEmpty(MisTake2))
                    {
                        result = itemNo + ":大過人員不可評B;";
                    }
                }
                else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
                {
                    if (string.IsNullOrEmpty(secondScore))
                    {
                        result = itemNo + "未考核！";
                    }
                    else if (Convert.ToInt32(secondScore) >= 90 && (!string.IsNullOrEmpty(MisTake1) || !string.IsNullOrEmpty(MisTake2)))
                    {
                        result = itemNo + ":大小過人員不可評A;";
                    }
                    else if (Convert.ToInt32(secondScore) >= 80 && Convert.ToInt32(secondScore) < 90 && !string.IsNullOrEmpty(MisTake2))
                    {
                        result = itemNo + ":大過人員不可評B;";
                    }
                }
                else
                {
                    result = "";
                }
            }
            if(!string.IsNullOrEmpty(result))
            {
                errMsg += result;
            }
        }
        catch (Exception e)
        {
            //writeLog(e);
            errMsg += e.Message;
        }
        return errMsg;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSubmit_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        string funcId = Request.QueryString["FuncId"];
        
        //判斷異常
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string userGUID1 = (string)Session["UserId"];
        string ms = string.Empty;
        //string Msg="";
        DataObject[] ddo1 = AssessmentStageList.dataSource.getAllDataObjects();
        for (int j = 0; j < ddo1.Length; j++)
        {
            string guid1 = ddo1[j].getData("GUID");
            string empName1 = ddo1[j].getData("empName");
            string assessmentPlanGUID1 = ddo1[j].getData("AssessmentPlanGUID");
            string assessmentName1 = ddo1[j].getData("AssessmentName");
            string assessmentManagerGUID1 = ddo1[j].getData("AssessmentManagerGUID");
            string stage1 = ddo1[j].getData("Stage");
            string status1 = ddo1[j].getData("Status");
            if (status1.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN))
            {
                try
                {
                    DataObject objects1 = AssessmentStageList.dataSource.getAvailableDataObject(j);
                    //根據總分判斷是否有考核
                    ms += checkScoreTotal(objects1);
                    if (ms != string.Empty)
                    {
                        ms += "\n";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox(ex.Message);
                }
                
            }
        }
        if (!string.IsNullOrEmpty(ms))
        {
           MessageBox(ms);
        }

        if (ms == string.Empty)
        {

            try
            {
                DataObject[] ddo = AssessmentStageList.dataSource.getAllDataObjects();

                if (ddo.Length > 0)
                {
                    //string connectString = (string)Session["connectString"];
                    //string engineType = (string)Session["engineType"];
                    //string userGUID = (string)Session["UserGUID"];

                    /**********************于20170803Marcia修改以便沒有帳號人員可以績效考評******************************/
                    string userGUID = "";
                    if (funcId.Equals("0"))
                    {
                        userGUID = sfAccount.ValueText;
                    }
                    else
                    {
                        userGUID = (string)Session["UserId"];
                    }
                    /**************************************************************/
                    string sendEmpName = "";
                    string errMsg = "";
                    string finalStageAssessmentName = "";
                    IOFactory factory = new IOFactory();
                    engine = factory.getEngine(engineType, connectString);


                    for (int i = 0; i < ddo.Length; i++)
                    {
                        string guid = ddo[i].getData("GUID");
                        string empName = ddo[i].getData("empName");
                        string assessmentPlanGUID = ddo[i].getData("AssessmentPlanGUID");
                        string assessmentName = ddo[i].getData("AssessmentName");
                        string assessmentManagerGUID = ddo[i].getData("AssessmentManagerGUID");
                        string stage = ddo[i].getData("Stage");
                        string status = ddo[i].getData("Status");
                        if (status.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN))
                        {
                            string whereClause = "GUID='" + guid + "' and AssessUserGUID='" + userGUID + "' and Stage='" + stage + "'";
                            NLAgent agent = new NLAgent();
                            agent.loadSchema("WebServerProject.maintain.SPPM004.SmpPmAssessmentUserScoreAgent");
                            agent.engine = engine;
                            agent.query(whereClause);
                            DataObjectSet set = agent.defaultData;
                            DataObject objects = set.getAvailableDataObject(0);
                            SmpPmMaintainUtil.getDecodeValue(objects, encodeFields);
                            //開始進行每筆成績提交
                            //errMsg += SmpPmMaintainUtil.checkScore(objects);
                            //errMsg += checkScoreTotal(objects);
                            if (errMsg.Equals(""))
                            {
                                //更新狀態/完成日
                                errMsg += SmpPmMaintainUtil.saveAssessmentStage(objects);
                                //發送mail給下一關主管
                                errMsg += SmpPmMaintainUtil.sendAssessmentToNextUser(objects);
                                //檢查是否為最後一個考核對象
                                bool finalStageComplete = false;
                                errMsg += SmpPmMaintainUtil.isFinalStageAssessUser(objects, out finalStageComplete);
                                if (finalStageComplete)
                                {
                                    errMsg += SmpPmMaintainUtil.mailAchievementAssessUser(assessmentPlanGUID, assessmentManagerGUID);
                                    finalStageAssessmentName += assessmentName + ";";
                                }
                                //儲存資料
                                bool result = agent.update();
                                if (result)
                                {
                                    sendEmpName += empName + ";";
                                }
                                else
                                {
                                    throw new Exception(engine.errorString);
                                }
                            }

                            if (!string.IsNullOrEmpty(errMsg))
                            {
                                MessageBox(empName + ": " + errMsg);
                            }
                        }
                        else
                        {
                            MessageBox(empName + ": 資料狀態不是進行中, 不能提交成績!");
                        }
                    }

                    if (!string.IsNullOrEmpty(sendEmpName))
                    {
                        MessageBox("成功提交成績: " + sendEmpName);
                    }
                    if (!string.IsNullOrEmpty(finalStageAssessmentName))
                    {
                        MessageBox(finalStageAssessmentName + " 本階段所有考評對象均已評核完畢!");
                    }

                    GbSearch_Click(sender, e);
                }
                else
                {
                    MessageBox("請勾選要送出之列!");
                }
            }
            catch (Exception ex)
            {
                writeLog(ex);
                MessageBox(ex.Message);
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
        }
    }
}
