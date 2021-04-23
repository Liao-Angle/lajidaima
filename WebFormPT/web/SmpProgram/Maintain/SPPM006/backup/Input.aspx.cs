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

public partial class SmpProgram_Maintain_SPPM006_Input : BaseWebUI.DataListSaveForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                string[,] ids = null;
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);               
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                Status.setListItem(ids);
                //設定等級
                ids = SmpPmMaintainUtil.getAchievementLevel(engine);
                AchievementLevel.setListItem(ids);
                engine.close();
            }
        }
    }

    /// <summary>
    /// showData
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
            setSession("objects", objects);

            deptName.ValueText = objects.getData("deptName");
            empName.ValueText = objects.getData("empName");
            AssessmentName.ValueText = objects.getData("AssessmentName");
            SelfScore.ValueText = objects.getData("SelfScore");          
            FirstScore.ValueText = objects.getData("FirstScore");            
            SecondScore.ValueText = objects.getData("SecondScore");
            FinalScore.ValueText = objects.getData("FinalScore");
			SelfComments.ValueText = objects.getData("SelfComments");
            FirstComments.ValueText = objects.getData("FirstComments");
            SecondComments.ValueText = objects.getData("SecondComments");
            AchievementLevel.ValueText = objects.getData("AchievementLevel");  
            FinalComments.ValueText = objects.getData("FinalComments");
            Status.ValueText = objects.getData("Status");
            string score = FirstScore.ValueText;
            if (!string.IsNullOrEmpty(SecondScore.ValueText))
            {
                score = SecondScore.ValueText;
            }
            if (string.IsNullOrEmpty(FinalScore.ValueText))
            {
                FinalScore.ValueText = score;
            }

            FinalScore_Check();

            displayData(objects);
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
    /// 編輯欄位條件
    /// </summary>
    /// <param name="objects"></param>
    protected void displayData(DataObject objects)
    {        
        string status = objects.getData("Status");
        if (!status.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN))
        {
            FinalScore.ReadOnly = true;
            AchievementLevel.ReadOnly = true;
            FinalComments.ReadOnly = true;
            SaveButton.Visible = false;
        } 
    }

    /// <summary>
    /// saveData
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        string errMsg = "";
        errMsg = FinalScore_Check();

        if (!string.IsNullOrEmpty(errMsg))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }
        else
        {
            objects.setData("FinalScore", FinalScore.ValueText);
            objects.setData("FinalComments", FinalComments.ValueText);
            objects.setData("AchievementLevel", AchievementLevel.ValueText);
            objects.setData("Description", AchievementLevel.ReadOnlyText);            
            if (!string.IsNullOrEmpty(AchievementLevel.ValueText))
                objects.setData("Results","OPEN");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPPM006.SmpPmUserAchievementAgent");
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
    /// 檢查總詰分數
    /// </summary>
    /// <returns></returns>
    protected string FinalScore_Check()
    {
        string errMsg = "";
        //string level = "";
        string finalScore = FinalScore.ValueText;
        int number = 0;
        bool isNumeric = int.TryParse(finalScore, out number);
        if (!isNumeric)
        {
            errMsg += LblFinalScore.Text + " 欄位必需為整數!\n";
        }
        else
        {
            if (number > 100 || number <0)
            {
                errMsg += LblFinalScore.Text + " 應為0~100分!\n";
            }
            else
            {
                //改以table 值為主
                //switch (Convert.ToInt32(FinalScore.ValueText) / 10)
                //{
                //    case 10:
                //    case 9:
                //        level = "A";
                //        break;
                //    case 8:
                //        level = "B";
                //        break;
                //    case 7:
                //        level = "C";
                //        break;
                //    case 6:
                //        level = "D";
                //        break;
                //    default:
                //        level = "E";
                //        break;
                //}
                //AchievementLevel.ValueText = level;
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                string sql = " select AchievementLevel, Description from SmpPmAchievementLevel " +
                             " where convert(INT, MixFraction) <= " + FinalScore.ValueText + " and convert(INT,MaxFraction) >=" + FinalScore.ValueText +
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

    /// <summary>
    /// 總詰分數
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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