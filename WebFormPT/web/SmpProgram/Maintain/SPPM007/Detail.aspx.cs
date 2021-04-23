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

public partial class SmpProgram_Maintain_SPPM007_Detail : BaseWebUI.DataListSaveForm
{
    string[] encodeFields = new string[] { "SelfScore", "FirstScore", "SecondScore"};

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                SaveButton.Visible = false;
                
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定等級
                string[,] ids = SmpPmMaintainUtil.getAchievementLevel(engine);
                AchievementLevel.setListItem(ids);
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                Status.setListItem(ids);

                //潘總修改成績分配分數
                if ((string)Session["UserId"] == "Q1508126")
                {
                    FinalScore.ReadOnly = false;
                    SaveButton.Visible = true;
                }

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
            if (objects.getData("Status").Equals("DRAFT"))
            {
                FinalScore.ReadOnly = true;
                SaveButton.Visible = false;
            }

            //SmpPmMaintainUtil.getDecodeValue(objects);
            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
            string userGUID = objects.getData("UserGUID");
            string whereClause = "AssessmentManagerGUID='" + assessmentManagerGUID + "' and UserGUID='" + userGUID + "'";          
            AssessmentName.ValueText = objects.getData("AssessmentName");           
            empName.ValueText = objects.getData("empName");
            deptName.ValueText = objects.getData("deptName");            
            Status.ValueText = objects.getData("Status");
            SelfScore.ValueText = objects.getData("SelfScore_W");
            SelfComments.ValueText = objects.getData("SelfComments");
            FirstScore.ValueText = objects.getData("FirstScore_W");
            FirstComments.ValueText = objects.getData("FirstComments");
            SecondScore.ValueText = objects.getData("SecondScore_W");
            SecondComments.ValueText = objects.getData("SecondComments");
            FinalScore.ValueText = objects.getData("FinalScore");
            FinalComments.ValueText = objects.getData("FinalComments");
            AchievementLevel.ValueText = objects.getData("AchievementLevel");
            sfdtName.ValueText = objects.getData("titleName");
            sfZD.ValueText = objects.getData("ZD");

            //考核表分數明細            
            DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
            SmpPmMaintainUtil.getDecodeValue(scoreDetailSet, encodeFields);
            AchievementDetailList.dataSource = scoreDetailSet;
            AchievementDetailList.setColumnStyle("Content", 360, DSCWebControl.GridColumnStyle.LEFT);          
            //AssessmentScoreDetailList.InputForm = "DetailSummary.aspx";
            //AssessmentScoreDetailList.DialogHeight = 600;
            //AssessmentScoreDetailList.DialogWidth = 840;           
            AchievementDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "SelfComments", "SelfScore", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };            
            AchievementDetailList.reSortCondition("編號", DataObjectConstants.ASC);
            AchievementDetailList.updateTable();

            //附件
            DataObjectSet attachmentSet = objects.getChild("SmpPmAttachment");
            AttachmentList.dataSource = attachmentSet;
            AttachmentList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "FileItemGUID", "Stage", "AttachmentType", "SelfComments", "SelfScore", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };

            for (int i = 0; i < attachmentSet.getAvailableDataObjectCount(); i++)
            {
                DataObject data = attachmentSet.getDataObject(i);
                //附件檔案下載連結處理
                string fileName = data.getData("FILENAME");
                if (!fileName.StartsWith("{["))
                {
                    string fileExt = data.getData("FILEEXT");
                    string fileItemGUID = data.getData("FileItemGUID");
                    string href = "../SPPM004/DownloadFile.aspx";
                    href += "?FILENAME=" + System.Web.HttpUtility.UrlPathEncode(fileName);
                    href += "&FILEEXT=" + fileExt;
                    href += "&FileItemGUID=" + fileItemGUID;
                    string fileNameUrl = "{[a href=\"" + href + "\"]}" + fileName + "{[/a]}";
                    data.setData("FILENAME", fileNameUrl);
                }
                AttachmentList.setColumnStyle("FILENAME", 130, DSCWebControl.GridColumnStyle.LEFT);
            }
            AttachmentList.updateTable();
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
    protected override void saveData(DataObject objects)
    {
        objects.setData("FinalScore", FinalScore.ValueText);
        objects.setData("AchievementLevel", AchievementLevel.ValueText);

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        AbstractEngine engine = null;
        IOFactory factory = new IOFactory();
        try
        {
            engine = factory.getEngine(engineType, connectString);
            string sqltxt = "update SmpPmUserAchievement set FinalScore='" + FinalScore.ValueText + "',AchievementLevel='" + AchievementLevel.ValueText+ "' where GUID='" + objects.getData("AchievementGUID") + "' and Status in('OPEN','COMPLETE')";
            Boolean isTrue = engine.executeSQL(sqltxt);
        
        }
        catch (Exception ex)
        {
            MessageBox(ex.Message);
        }

        base.saveData(objects);
    }
      /// <summary>
    /// 檢查總詰分數
    /// </summary>
    /// <returns></returns>
    protected string FinalScore_Check()
    {
        string errMsg = "";
        //string level = "";
        string finalScore = FinalScore.ValueText;
        int number = 0;
        float number2 = 0;
        bool isNumeric = int.TryParse(finalScore, out number);
        bool isNumeric2 = float.TryParse(finalScore, out number2);
        if (!isNumeric && !isNumeric2)
        {
            errMsg += LblFinalScore.Text + " 欄位必需為數字!\n";
        }
        else
        {
            if (number > 100 || number <0)
            {
                errMsg += LblFinalScore.Text + " 應為0~100分!\n";
            }
            else
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                string sql = " select AchievementLevel, Description from SmpPmAchievementLevel " +
                             " where convert(INT, MixFraction) <= " + FinalScore.ValueText + " and convert(INT,MaxFraction) >" + FinalScore.ValueText +
                             " and Active = 'Y' ";               
                DataSet ds = engine.getDataSet(sql, "TEMP");
                int rows = ds.Tables[0].Rows.Count;                     
                for (int i = 0; i < rows; i++)
                {
                    AchievementLevel.ValueText = ds.Tables[0].Rows[i][0].ToString();
                }
                
            }
        }
        return errMsg;
    }
    protected void FinalScore_TextChanged(object sender, EventArgs e)
    {
        string errMsg = "";
        errMsg = FinalScore_Check();
        if (!string.IsNullOrEmpty(errMsg))
        {
            errMsg = errMsg.Replace("\n", "; ");
            MessageBox(errMsg);
            FinalScore.Focus();
        }
    }
}