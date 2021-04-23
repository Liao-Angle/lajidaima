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
using WebServerProject;
using com.dsc.kernal.mail;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM004_Input : BaseWebUI.DataListSaveForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定階段
                string[,] ids = SmpPmMaintainUtil.getAssessmentStageIds(engine);
                Stage.setListItem(ids);
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                Status.setListItem(ids);

                engine.close();
            }
        }
    }

    /// <summary>
    /// 顯示資料
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        string errMsg = "";

        try
        {
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
            string userGUID = objects.getData("UserGUID");
            string whereClause = "AssessmentManagerGUID='" + assessmentManagerGUID + "' and UserGUID='" + userGUID + "'";
            string stage = objects.getData("Stage"); ;
            AssessmentName.ValueText = objects.getData("AssessmentName");
            Stage.ValueText = stage;
            empName.ValueText = objects.getData("empName");
            deptName.ValueText = objects.getData("deptName");
            AssessmentDays.ValueText = objects.getData("AssessmentDays");
            Status.ValueText = objects.getData("Status");

            SelfScore.ValueText = objects.getData("SelfScore");
            SelfComments.ValueText = objects.getData("SelfComments");
            FirstScore.ValueText = objects.getData("FirstScore");
            FirstComments.ValueText = objects.getData("FirstComments");
            SecondScore.ValueText = objects.getData("SecondScore");
            SecondComments.ValueText = objects.getData("SecondComments");

            displayData(objects);

            //考核表分數明細
            createScoreDetail(engine, objects);
            DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
            AssessmentScoreDetailList.dataSource = scoreDetailSet;
            //AssessmentScoreDetailList.setColumnStyle("SelfComments", 200, DSCWebControl.GridColumnStyle.LEFT);
            AssessmentScoreDetailList.InputForm = "Detail.aspx";
            AssessmentScoreDetailList.DialogHeight = 600;
            AssessmentScoreDetailList.DialogWidth = 840;
            if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
            {
                AssessmentScoreDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "Content", "MinFraction", "MaxFraction", "FirstScore", "FirstComments", "SecondScore", "SecondComments", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            }
            else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
            {
                AssessmentScoreDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "Content", "MinFraction", "MaxFraction", "SecondScore", "SecondComments", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            }
            else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            {
                AssessmentScoreDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "Content", "MinFraction", "MaxFraction", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            }
            AssessmentScoreDetailList.reSortCondition("編號", DataObjectConstants.ASC);
            AssessmentScoreDetailList.updateTable();

            setSession("objects", objects);
            setSession("Stage", "Stage", objects.getData("Stage"));
        }
        catch (Exception e)
        {
            errMsg += e.Message;
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }

        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
    }

    /// <summary>
    /// 儲存資料
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        string errMsg = "";
        IOFactory factory = null;
        AbstractEngine engine = null;

        try
        {
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            objects.setData("SelfScore", SelfScore.ValueText);
            objects.setData("SelfComments", SelfComments.ValueText);
            objects.setData("FirstScore", FirstScore.ValueText);
            objects.setData("FirstComments", FirstComments.ValueText);
            objects.setData("SecondScore", SecondScore.ValueText);
            objects.setData("SecondComments", SecondComments.ValueText);
            //計算總分
            computeTotalScore(objects);
        }
        catch (Exception e)
        {
            errMsg += e.Message;
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }

        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
    }

    /// <summary>
    /// 資料至資料表
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPPM004.SmpPmAssessmentUserScoreAgent");
        agent.engine = engine;
        agent.query("1=2");

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

    /// <summary>
    /// 控制是否顯示物件資料
    /// </summary>
    /// <param name="engine"></param>
    protected void displayData(DataObject objects)
    {
        string stage = objects.getData("Stage");
        string status = objects.getData("Status");
        if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
        {

        }
        else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
        {
            SelfComments.ReadOnly = true;

            LblFirstScore.Display = true;
            FirstScore.Display = true;
            LblFirstComments.Display = true;
            FirstComments.Display = true;
        }
        else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
        {
            SelfComments.ReadOnly = true;
            FirstComments.ReadOnly = true;
            
            LblFirstScore.Display = true;
            FirstScore.Display = true;
            LblFirstComments.Display = true;
            FirstComments.Display = true;
            LblSecondScore.Display = true;
            SecondScore.Display = true;
            LblSecondComments.Display = true;
            SecondComments.Display = true;
        }

        if (!status.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN))
        {
            GbSendAssessmentScore.Display = false;
            SelfComments.ReadOnly = true;
            FirstComments.ReadOnly = true;
            SecondComments.ReadOnly = true;
            AssessmentScoreDetailList.ReadOnly = true;
        }
    }

    /// <summary>
    /// 產生分數明細資料 (送出考核計畫時會產生單頭資料)
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected void createScoreDetail(AbstractEngine engine, DataObject objects)
    {
        DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
        if (scoreDetailSet.getAvailableDataObjectCount() == 0)
        {
            string evaluationGUID = objects.getData("EvaluationGUID");
            string sql = "select a.Name EvaluationName, b.GUID EvaluationDetailGUID, b.ItemNo, b.ItemName, b.Content, b.FractionExp, b.MinFraction, b.MaxFraction from SmpPmEvaluation a, SmpPmEvaluationDetail b where a.GUID='" + evaluationGUID + "' and a.GUID=b.EvaluationGUID";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; i++)
            {
                string itemNo = ds.Tables[0].Rows[i]["ItemNo"].ToString();
                string itemName = ds.Tables[0].Rows[i]["ItemName"].ToString();
                string content = ds.Tables[0].Rows[i]["Content"].ToString();
                string fractionExp = ds.Tables[0].Rows[i]["FractionExp"].ToString();
                string minFraction = ds.Tables[0].Rows[i]["MinFraction"].ToString();
                string maxFraction = ds.Tables[0].Rows[i]["MaxFraction"].ToString();
                string evaluationDetailGUID = ds.Tables[0].Rows[i]["EvaluationDetailGUID"].ToString();
                
                DataObject data = scoreDetailSet.create();
                data.setData("GUID", IDProcessor.getID(""));
                data.setData("IS_DISPLAY", "Y");
                data.setData("IS_LOCK", "N");
                data.setData("DATA_STATUS", "Y");
                data.setData("AssessmentUserScoreGUID", objects.getData("GUID"));
                data.setData("EvaluationDetailGUID", evaluationDetailGUID);
                data.setData("UserGUID", objects.getData("UserGUID"));
                data.setData("ItemNo", itemNo);
                data.setData("ItemName", itemName);
                data.setData("Content", content);
                data.setData("FractionExp", fractionExp);
                data.setData("MinFraction", minFraction);
                data.setData("MaxFraction", maxFraction);
                scoreDetailSet.add(data);
            }
        }
    }

    /// <summary>
    /// 計算自評/一階/二階總分
    /// </summary>
    /// <param name="objects"></param>
    protected void computeTotalScore(DataObject objects)
    {
        DataObjectSet scoreDetailSet = AssessmentScoreDetailList.dataSource;
        bool isNumeric = false;
        int number = 0;
        int totalSelfScore = 0;
        int totalFirstScore = 0;
        int totalSecondScore = 0;
        int row = scoreDetailSet.getAvailableDataObjectCount();
        for (int i = 0; i < row; i++)
        {
            DataObject data = scoreDetailSet.getAvailableDataObject(i);
            string selfScore = data.getData("SelfScore");
            string firstScore = data.getData("FirstScore");
            string secondScore = data.getData("SecondScore");

            isNumeric = int.TryParse(selfScore, out number);
            totalSelfScore += number;

            isNumeric = int.TryParse(firstScore, out number);
            totalFirstScore += number;

            isNumeric = int.TryParse(secondScore, out number);
            totalSecondScore += number;
        }

        if (totalSelfScore > 0)
        {
            SelfScore.ValueText = totalSelfScore + "";
            objects.setData("SelfScore", totalSelfScore + "");
        }

        if (totalFirstScore > 0)
        {
            FirstScore.ValueText = totalFirstScore + "";
            objects.setData("FirstScore", totalFirstScore + "");
        }

        if (totalSecondScore > 0)
        {
            SecondScore.ValueText = totalSecondScore + "";
            objects.setData("SecondScore", totalSecondScore + "");
        }
    }

    /// <summary>
    /// 送出成績
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSendAssessmentScore_Click(object sender, EventArgs e)
    {
        DataObject objects = (DataObject)getSession("objects");
        string errMsg = "";

        errMsg += sendAssessmentScoreCheck(objects);

        if (errMsg.Equals(""))
        {
            //更新狀態/完成日
            saveAssessmentStage(objects);
            //發送mail給下一關主管
            sendAssessmentToNextUser(objects);
            //檢查是否為最後一個考核對象
            isFinalStageAssessUser(objects);
            //儲存資料
            SaveButton_Click(sender, e);
        }
        else
        {
            MessageBox(errMsg);
        }
    }

    /// <summary>
    /// 送出成績時檢查資料
    /// </summary>
    /// <returns></returns>
    protected string sendAssessmentScoreCheck(DataObject objects)
    {
        string errMsg = "";
        string result = "";
        //檢查所項目均必需有分數
        DataObjectSet scoreSet = AssessmentScoreDetailList.dataSource;
        int row = scoreSet.getAvailableDataObjectCount();
        for (int i = 0; i < row; i++)
        {
            DataObject data = scoreSet.getAvailableDataObject(i);
            string itemNo = data.getData("ItemNo");
            string selfScore = data.getData("SelfScore");
            string firstScore = data.getData("FirstScore");
            string secondScore = data.getData("SecondScore");

            if (Stage.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
            {
                if (string.IsNullOrEmpty(selfScore))
                {
                    result += itemNo + ";";
                }
            }
            else if (Stage.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
            {
                if (string.IsNullOrEmpty(firstScore))
                {
                    result += itemNo + ";";
                }
            }
            else if (Stage.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            {
                if (string.IsNullOrEmpty(secondScore))
                {
                    result += itemNo + ";";
                }
            }
        }
        if (!string.IsNullOrEmpty(result))
        {
            errMsg += "評核項目必需填寫分數, 編號: " + result;
        }

        return errMsg;
    }

    /// <summary>
    /// 儲存評核階段狀態
    /// </summary>
    protected void saveAssessmentStage(DataObject objects)
    {
        string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
        string stage = objects.getData("Stage");
        string dateTimeNow = DateTimeUtility.getSystemTime2(null);

        objects.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE);
        objects.setData("CompleteDate", dateTimeNow);

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAssessmentStageAgent");
        agent.engine = engine;
        agent.query("AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage='" + stage + "'");
        DataObjectSet set = agent.defaultData;
        int row = set.getAvailableDataObjectCount();
        if (row > 0)
        {
            DataObject data = set.getAvailableDataObject(0);
            data.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE);

            data.setData("CompleteDate", dateTimeNow);
            if (!agent.update())
            {
                MessageBox(agent.engine.errorString);
            }
        }
        else
        {
            MessageBox("找不到資料更新!");
        }
    }

    /// <summary>
    /// 觸發下一階段評核
    /// </summary>
    /// <param name="assessmentManagerGUID"></param>
    /// <param name="stage"></param>
    protected void sendAssessmentToNextUser(DataObject objects)
    {
        string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
        string stage = objects.getData("Stage");
        
        //更新下一關狀態/啟始日
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql = "select Stage from SmpPmUserAssessmentStage where AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage > '" + stage + "' order by Stage";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int row = ds.Tables[0].Rows.Count;
        if (row > 0)
        {
            string minStage = ds.Tables[0].Rows[0]["Stage"].ToString();
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAssessmentStageAgent");
            agent.engine = engine;
            agent.query("AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage='" + minStage + "'");
            DataObjectSet set = agent.defaultData;
            row = set.getAvailableDataObjectCount();
            if (row > 0)
            {
                DataObject data = set.getAvailableDataObject(0);
                data.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);
                string dateTimeNow = DateTimeUtility.getSystemTime2(null);
                data.setData("StartDate", dateTimeNow);
                if (!agent.update())
                {
                    MessageBox(agent.engine.errorString);
                }
                sendAssessment(data);
            }
            else
            {
                //MessageBox("找不到資料更新!");
            }
        }
        else
        {
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAchievementAgent");
            agent.engine = engine;
            agent.query("AssessmentManagerGUID='" + assessmentManagerGUID + "'");
            DataObjectSet set = agent.defaultData;
            row = set.getAvailableDataObjectCount();
            if (row > 0)
            {
                DataObject data = set.getAvailableDataObject(0);
                data.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);
                if (!agent.update())
                {
                    MessageBox(agent.engine.errorString);
                }
            }
        }
    }

    /// <summary>
    /// 發送信件
    /// </summary>
    /// <param name="objects"></param>
    protected void sendAssessment(DataObject objects)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string assessmentPlanGUID = "";
            string subject = "";
            string assessmentName = "";
            string firstAssessmentDays = "";
            string secondAssessmentDays = "";
            string assessmentDays = "";
            string assessmentStageGUID = objects.getData("GUID");
            string stage = objects.getData("Stage");
            //string userGUID = objects.getData("UsserGUID");
            string assessUserGUID = objects.getData("AssessUserGUID");
            string sql = "select a.AssessmentPlanGUID, a.Stage, b.AssessmentName, c.FirstAssessmentDays, c.SecondAssessmentDays from SmpPmUserAssessmentStage a, SmpPmAssessmentPlan b, SmpPmAssessmentConfig c where a.GUID='" + assessmentStageGUID + "' and a.AssessmentPlanGUID=b.GUID and b.GUID = c.AssessmentPlanGUID";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int row = ds.Tables[0].Rows.Count;
            if (row > 0)
            {
                assessmentPlanGUID = ds.Tables[0].Rows[0]["AssessmentPlanGUID"].ToString();
                assessmentName = ds.Tables[0].Rows[0]["AssessmentName"].ToString();
                firstAssessmentDays = ds.Tables[0].Rows[0]["FirstAssessmentDays"].ToString();
                secondAssessmentDays = ds.Tables[0].Rows[0]["SecondAssessmentDays"].ToString();
            }

            if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
            {
                subject = "[一階評核]" + assessmentName;
                assessmentDays = firstAssessmentDays;
            }
            else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            {
                subject = "[二階評核]" + assessmentName;
                assessmentDays = secondAssessmentDays;
            }

            sql = "select b.mailAddress, b.userName, c.empName, a.GUID from SmpPmUserAssessmentStage a, Users b, SmpHrEmployeeInfoV c where a.GUID='" + assessmentStageGUID + "' and a.AssessUserGUID = b.OID and a.UserGUID=c.empGUID";
            ds = engine.getDataSet(sql, "TEMP");
            row = ds.Tables[0].Rows.Count;
            string[,] values = new string[row,4];
            for (int i = 0; i < row; i++)
            {
                values[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                values[i, 1] = ds.Tables[0].Rows[i][1].ToString();
                values[i, 2] = ds.Tables[0].Rows[i][2].ToString();
                values[i, 3] = ds.Tables[0].Rows[i][3].ToString();
            }
            
            SysParam sp = new SysParam(engine);
            string mailclass = sp.getParam("MailClass");
            string smtpServer = sp.getParam("SMTP_Server");
            string systemMail = sp.getParam("SystemMail");
            string emailHeader = sp.getParam("EmailHeader");
            MailFactory fac = new MailFactory();
            AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);
            
            for (int i = 0; i < row; i++)
            {
                string mailAddress = values[i, 0];
                string assessUserName = values[i, 1];
                string userName = values[i, 2];
                string objectGUID = values[i, 3];
                string content = "";
                string href = emailHeader + "?runMethod=executePmProgram&programID=SPPM005M&ObjectGUID=" + objectGUID;
                content += "考評名稱: " + assessmentName + "<br />";
                content += "評核截止天數: " + assessmentDays + " 天<br />";
                content += "考核人: " + assessUserName + " <br />";
                content += "考核對象: " + userName + " <br />";
                content += "待辦事項: <a href='" + href + "'>您有一個待辦事項,請按這裡連結至您的工作</a><br />";

                au.sendMailHTML("", smtpServer, mailAddress, "", systemMail, subject, content);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (engine != null)
            {
                engine.close();
            }
        }
    }

    /// <summary>
    /// 最後評核階段, 所有考核對象均已完成則提示可以進行成績分配
    /// </summary>
    /// <param name="objects"></param>
    protected void isFinalStageAssessUser(DataObject objects)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        string message = "";
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            string userGUID = (string)Session["UserGUID"];

            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            
            string assessmentPlanGUID = objects.getData("AssessmentPlanGUID");
            string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
            string stage = objects.getData("Stage");
            string sql = "select AssessmentLevel from SmpPmAssessmentConfig where AssessmentPlanGUID = '" + assessmentPlanGUID + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            string assessmentLevel = ds.Tables[0].Rows[0][0].ToString();
            bool isFinalStage = false;
            if (assessmentLevel.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL1))
            {
                if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
                {
                    isFinalStage = true;
                }
            }
            else if (assessmentLevel.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL2))
            {
                if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
                {
                    isFinalStage = true;
                }
            }
            else if(assessmentLevel.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL0)) 
            {
                sql = "select Stage from SmpPmUserAssessmentStage where AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage > '" + stage + "' order by Stage";
                ds = engine.getDataSet(sql, "TEMP");
                int row = ds.Tables[0].Rows.Count;
                if (row == 0)
                {
                    isFinalStage = true;
                }
            }

            if (isFinalStage)
            {
                sql = "select count('x') cnt from SmpPmUserAssessmentStage where AssessmentPlanGUID='" + assessmentPlanGUID + "' and AssessUserGUID='" + userGUID + "' and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "'";
                ds = engine.getDataSet(sql, "TEMP");
                int cnt = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
                if (cnt == 0)
                {
                    message = "本階段所有考評對象均已評核完畢! 請進行成績分配!";
                    MessageBox(message);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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