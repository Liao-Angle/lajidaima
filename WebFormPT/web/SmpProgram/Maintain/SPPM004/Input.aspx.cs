using System;
using System.Data;
using System.Drawing;
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
    string[] encodeFields = new string[] { "SelfScore", "FirstScore", "SecondScore"};
    string[] encodeFields2 = new string[] { "SelfScore", "FirstScore", "SecondScore", "SelfScore_W", "FirstScore_W", "SecondScore_W" };
    
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

                //改用gridview處理, 原datalist不顯示
                AssessmentScoreDetailList.Display = false; 
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

            string guid = objects.getData("GUID");
            string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
            string userGUID = objects.getData("UserGUID");
            string whereClause = "AssessmentManagerGUID='" + assessmentManagerGUID + "' and UserGUID='" + userGUID + "'";
            string stage = objects.getData("Stage"); ;
            //string status = objects.getData("Status");
            //string selfEvaluation = objects.getData("SelfEvaluation");
            AssessmentName.ValueText = objects.getData("AssessmentName");
            Stage.ValueText = stage;
            empName.ValueText = objects.getData("empName");
            deptName.ValueText = objects.getData("deptName");
            AssessmentDays.ValueText = objects.getData("AssessmentDays");
            Status.ValueText = objects.getData("Status");

            SelfScore.ValueText = objects.getData("SelfScore");
            SelfScore_W.ValueText = objects.getData("SelfScore_W");
            SelfComments.ValueText = objects.getData("SelfComments");
            FirstScore.ValueText = objects.getData("FirstScore");
            FirstScore_W.ValueText = objects.getData("FirstScore_W");
            FirstComments.ValueText = objects.getData("FirstComments");
            SecondScore.ValueText = objects.getData("SecondScore");
            SecondScore_W.ValueText = objects.getData("SecondScore_W");
            SecondComments.ValueText = objects.getData("SecondComments");

            displayData(objects);

            //考核表分數明細
            createScoreDetail(engine, objects);
            DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
            SmpPmMaintainUtil.getDecodeValue(scoreDetailSet, encodeFields);
            AssessmentScoreDetailList.dataSource = scoreDetailSet;
            //AssessmentScoreDetailList.setColumnStyle("SelfComments", 200, DSCWebControl.GridColumnStyle.LEFT);
            AssessmentScoreDetailList.InputForm = "Detail.aspx";
            AssessmentScoreDetailList.DialogHeight = 600;
            AssessmentScoreDetailList.DialogWidth = 840;
            if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
            {
                AssessmentScoreDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "Content", "MinFraction", "MaxFraction", "FirstScore", "FirstComments", "SecondScore", "SecondComments", "ItemNum", "ItemWeight", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
                ButtonUpload.ReadOnly = true;
                AttachmentList.ReadOnly = true;
            }
            else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
            {
                AssessmentScoreDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "Content", "MinFraction", "MaxFraction", "SecondScore", "SecondComments", "ItemNum", "ItemWeight", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            }
            else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            {
                AssessmentScoreDetailList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "EvaluationDetailGUID", "UserGUID", "Content", "MinFraction", "MaxFraction", "ItemNum", "ItemWeight", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            }
            AssessmentScoreDetailList.reSortCondition("編號", DataObjectConstants.ASC);
            AssessmentScoreDetailList.updateTable();

            //附件
            AbstractEngine fileEngine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
            SysParam sp = new SysParam(fileEngine);
            string fileAdapter = sp.getParam("FileAdapter");
            FileUpload.FileAdapter = fileAdapter;
            FileUpload.engine = fileEngine;
            FileUpload.tempFolder = Server.MapPath("~/tempFolder/");
            FileUpload.readFile("");

            //附件
            DataObjectSet attachmentSet = objects.getChild("SmpPmAttachment");
            AttachmentList.dataSource = attachmentSet;
            AttachmentList.HiddenField = new string[] { "GUID", "AssessmentUserScoreGUID", "FileItemGUID", "Stage", "AttachmentType", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };

            for (int i = 0; i < attachmentSet.getAvailableDataObjectCount(); i++)
            {
                DataObject data = attachmentSet.getDataObject(i);
                //附件檔案下載連結處理
                string fileName = data.getData("FILENAME");
                if (!fileName.StartsWith("{["))
                {
                    string fileExt = data.getData("FILEEXT");
                    string fileItemGUID = data.getData("FileItemGUID");
                    string href = "DownloadFile.aspx";
                    href += "?FILENAME=" + System.Web.HttpUtility.UrlPathEncode(fileName);
                    href += "&FILEEXT=" + fileExt;
                    href += "&FileItemGUID=" + fileItemGUID;
                    string fileNameUrl = "{[a href=\"" + href + "\"]}" + fileName + "{[/a]}";
                    data.setData("FILENAME", fileNameUrl);
                }
                AttachmentList.setColumnStyle("FILENAME", 130, DSCWebControl.GridColumnStyle.LEFT);
            }
            AttachmentList.updateTable();

            setSession("objects", objects);
            setSession("Stage", "Stage", objects.getData("Stage"));

            showGridViewData(objects);
        }
        catch (Exception e)
        {
            errMsg += e.Message;
            errMsg = errMsg.Replace("\n", "; ");
            writeLog(e);
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
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected void showGridViewData(com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            //AssessmentScoreDetailList.Display = true;
            DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
            string stage = objects.getData("Stage"); ;
            string status = objects.getData("Status");
            string selfEvaluation = objects.getData("SelfEvaluation");
            //gridview display
            DataTable dt = scoreDetailSet.getDataTable(); //懷疑DataObjectSet有bug, 執行[離開]後再次點選列資料, dataObjectSet.getDataTable()會產生null exception
            dt.DefaultView.Sort = "ItemNo";
            //二階默認顯示一階成績
            //if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        dr["SecondScore"] = dr["FirstScore"];
            //    }
            //}

            dt = dt.DefaultView.ToTable();
            GridViewEvaluationDetail.DataMember = "考核表明細分數";
            GridViewEvaluationDetail.DataSource = dt;
            GridViewEvaluationDetail.DataBind();
            //GridViewEvaluationDetail.Sort("ItemNo", SortDirection.Ascending);
            //gridview control
            foreach (GridViewRow r in GridViewEvaluationDetail.Rows)
            {
                TextBox tbGUID = ((TextBox)r.FindControl("tbGUID"));
                TextBox tbSelfScore = ((TextBox)r.FindControl("tbSelfScore"));
                TextBox tbSelfComments = ((TextBox)r.FindControl("tbSelfComments"));
                TextBox tbFirstScore = ((TextBox)r.FindControl("tbFirstScore"));
                TextBox tbFirstComments = ((TextBox)r.FindControl("tbFirstComments"));
                TextBox tbSecondScore = ((TextBox)r.FindControl("tbSecondScore"));
                TextBox tbSecondComments = ((TextBox)r.FindControl("tbSecondComments"));

                tbGUID.Visible = false;


                if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
                {
                    tbFirstScore.ReadOnly = true;
                    tbFirstComments.ReadOnly = true;
                    tbSecondScore.ReadOnly = true;
                    tbSecondComments.ReadOnly = true;

                    tbFirstScore.BackColor = Color.LightGray;
                    tbFirstComments.BackColor = Color.LightGray;
                    tbSecondScore.BackColor = Color.LightGray;
                    tbSecondComments.BackColor = Color.LightGray;

                    tbSelfComments.Width = Unit.Pixel((int)tbSelfComments.Width.Value * 2);

                    //hide 一階
                    if (GridViewEvaluationDetail.Columns.Count > 10)
                    {
                        GridViewEvaluationDetail.Columns[7].Visible = false;
                        GridViewEvaluationDetail.Columns[10].Visible = false;
                    }
                    //hide 二階
                    if (GridViewEvaluationDetail.Columns.Count > 11)
                    {
                        GridViewEvaluationDetail.Columns[8].Visible = false;
                        GridViewEvaluationDetail.Columns[11].Visible = false;
                    }
                }
                else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
                {
                    tbSelfScore.ReadOnly = true;
                    tbSelfComments.ReadOnly = true;
                    tbSecondScore.ReadOnly = true;
                    tbSecondComments.ReadOnly = true;

                    tbSelfScore.BackColor = Color.LightGray;
                    tbSelfComments.BackColor = Color.LightGray;
                    tbSecondScore.BackColor = Color.LightGray;
                    tbSecondComments.BackColor = Color.LightGray;

                    tbFirstComments.Width = Unit.Pixel((int)tbFirstComments.Width.Value * 2);

                    //hide自評
                    if (selfEvaluation.Equals(SmpPmMaintainUtil.ASSESSMENT_NO))
                    {
                        if (GridViewEvaluationDetail.Columns.Count > 7)
                        {
                            GridViewEvaluationDetail.Columns[6].Visible = false;
                            GridViewEvaluationDetail.Columns[9].Visible = false;
                        }
                    }

                    //hide 2階
                    if (GridViewEvaluationDetail.Columns.Count > 11)
                    {
                        GridViewEvaluationDetail.Columns[8].Visible = false;
                        GridViewEvaluationDetail.Columns[11].Visible = false;
                    }
                }
                else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
                {
                    tbSelfScore.ReadOnly = true;
                    tbSelfComments.ReadOnly = true;
                    tbFirstScore.ReadOnly = true;
                    tbFirstComments.ReadOnly = true;

                    tbSelfScore.BackColor = Color.LightGray;
                    tbSelfComments.BackColor = Color.LightGray;
                    tbFirstScore.BackColor = Color.LightGray;
                    tbFirstComments.BackColor = Color.LightGray;

                    tbSecondComments.Width = Unit.Pixel((int)tbSecondComments.Width.Value * 2);

                    //hide自評
                    if (selfEvaluation.Equals(SmpPmMaintainUtil.ASSESSMENT_NO))
                    {
                        if (GridViewEvaluationDetail.Columns.Count > 7)
                        {
                            GridViewEvaluationDetail.Columns[6].Visible = false;
                            GridViewEvaluationDetail.Columns[9].Visible = false;
                        }
                    }
                }

                if (!status.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN))
                {
                    tbSelfScore.ReadOnly = true;
                    tbSelfComments.ReadOnly = true;
                    tbFirstScore.ReadOnly = true;
                    tbFirstComments.ReadOnly = true;
                    tbSecondScore.ReadOnly = true;
                    tbSecondComments.ReadOnly = true;
                    ButtonUpload.ReadOnly = true;
                }
            }

            addGridViewEvent();
        }
        catch (Exception e)
        {
            writeLog(e);
            throw new Exception(e.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected void addGridViewEvent()
    {
        foreach (GridViewRow r in GridViewEvaluationDetail.Rows)
        {
            TextBox tbSelfScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex].FindControl("tbSelfScore");
            if (r.DataItemIndex == 0)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox tbNextSelfScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbSelfScore");
                    tbSelfScore.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextSelfScore.ClientID + "').focus();}");
                    tbSelfScore.Attributes.Add("onchange", "SelfScore_xx();");
                }
            }
            if (r.DataItemIndex < this.GridViewEvaluationDetail.Rows.Count - 1 && r.DataItemIndex > 0)
            {
                TextBox txtPrevSelfScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbSelfScore");
                TextBox tbNextSelfScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbSelfScore");
                tbSelfScore.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextSelfScore.ClientID + "').focus();}else if(event.keyCode == 38){document.getElementById('" + txtPrevSelfScore.ClientID + "').focus();}");
                tbSelfScore.Attributes.Add("onchange", "SelfScore_xx();");
            }
            if (r.DataItemIndex == this.GridViewEvaluationDetail.Rows.Count - 1)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox txtPrevSelfScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbSelfScore");
                    tbSelfScore.Attributes.Add("onkeydown", "if(event.keyCode == 38){document.getElementById('" + txtPrevSelfScore.ClientID + "').focus();}");
                    tbSelfScore.Attributes.Add("onchange", "SelfScore_xx();");
                }
            }

            //add FirstScore onkeydown event
            TextBox tbFirstScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex].FindControl("tbFirstScore");
            if (r.DataItemIndex == 0)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox tbNextFirstScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbFirstScore");
                    tbFirstScore.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextFirstScore.ClientID + "').focus();}");
                    tbFirstScore.Attributes.Add("onchange", "firstScore_xx();");     
                }
            }
            if (r.DataItemIndex < this.GridViewEvaluationDetail.Rows.Count - 1 && r.DataItemIndex > 0)
            {
                TextBox txtPrevFirstScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbFirstScore");
                TextBox tbNextFirstScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbFirstScore");
                tbFirstScore.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextFirstScore.ClientID + "').focus();}else if(event.keyCode == 38){document.getElementById('" + txtPrevFirstScore.ClientID + "').focus();}");
                tbFirstScore.Attributes.Add("onchange", "firstScore_xx();");    
            }
            if (r.DataItemIndex == this.GridViewEvaluationDetail.Rows.Count - 1)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox txtPrevFirstScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbFirstScore");
                    tbFirstScore.Attributes.Add("onkeydown", "if(event.keyCode == 38){document.getElementById('" + txtPrevFirstScore.ClientID + "').focus();}");
                    tbFirstScore.Attributes.Add("onchange", "firstScore_xx();");    
                }
            }
            
            //add Firstcomment onkeydown event
            TextBox tbFirstComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex].FindControl("tbFirstComments");
            if (r.DataItemIndex == 0)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox tbNextFirstComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbFirstComments");
                    tbFirstComments.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextFirstComments.ClientID + "').focus();}");
                }
            }
            if (r.DataItemIndex < this.GridViewEvaluationDetail.Rows.Count - 1 && r.DataItemIndex > 0)
            {
                TextBox txtPrevFirstComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbFirstComments");
                TextBox tbNextFirstComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbFirstComments");
                tbFirstComments.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextFirstComments.ClientID + "').focus();}else if(event.keyCode == 38){document.getElementById('" + txtPrevFirstComments.ClientID + "').focus();}");
            }
            if (r.DataItemIndex == this.GridViewEvaluationDetail.Rows.Count - 1)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox txtPrevFirstComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbFirstComments");
                    tbFirstComments.Attributes.Add("onkeydown", "if(event.keyCode == 38){document.getElementById('" + txtPrevFirstComments.ClientID + "').focus();}");
                }
            }

            //add SecondScore onkeydown event
            TextBox tbSecondScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex].FindControl("tbSecondScore");
            if (r.DataItemIndex == 0)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox tbNextSecondScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbSecondScore");
                    tbSecondScore.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextSecondScore.ClientID + "').focus();}");
                    tbSecondScore.Attributes.Add("onchange", "SecondScore_xx();");  
                }
            }
            if (r.DataItemIndex < this.GridViewEvaluationDetail.Rows.Count - 1 && r.DataItemIndex > 0)
            {
                TextBox txtPrevSecondScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbSecondScore");
                TextBox tbNextSecondScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbSecondScore");
                tbSecondScore.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextSecondScore.ClientID + "').focus();}else if(event.keyCode == 38){document.getElementById('" + txtPrevSecondScore.ClientID + "').focus();}");
                tbSecondScore.Attributes.Add("onchange", "SecondScore_xx();");  
            }
            if (r.DataItemIndex == this.GridViewEvaluationDetail.Rows.Count - 1)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox txtPrevSecondScore = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbSecondScore");
                    tbSecondScore.Attributes.Add("onkeydown", "if(event.keyCode == 38){document.getElementById('" + txtPrevSecondScore.ClientID + "').focus();}");
                    tbSecondScore.Attributes.Add("onchange", "SecondScore_xx();");  
                }
               
            }

            //add SecondComments onkeydown event
            TextBox tbSecondComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex].FindControl("tbSecondComments");
            if (r.DataItemIndex == 0)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox tbNextSecondComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbSecondComments");
                    tbSecondComments.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextSecondComments.ClientID + "').focus();}");
                }
            }
            if (r.DataItemIndex < this.GridViewEvaluationDetail.Rows.Count - 1 && r.DataItemIndex > 0)
            {
                TextBox txtPrevSecondComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbSecondComments");
                TextBox tbNextSecondComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex + 1].FindControl("tbSecondComments");
                tbSecondComments.Attributes.Add("onkeydown", "if(event.keyCode == 40){document.getElementById('" + tbNextSecondComments.ClientID + "').focus();}else if(event.keyCode == 38){document.getElementById('" + txtPrevSecondComments.ClientID + "').focus();}");
            }
            if (r.DataItemIndex == this.GridViewEvaluationDetail.Rows.Count - 1)
            {
                if (this.GridViewEvaluationDetail.Rows.Count > 1)
                {
                    TextBox txtPrevSecondComments = (TextBox)this.GridViewEvaluationDetail.Rows[r.DataItemIndex - 1].FindControl("tbSecondComments");
                    tbSecondComments.Attributes.Add("onkeydown", "if(event.keyCode == 38){document.getElementById('" + txtPrevSecondComments.ClientID + "').focus();}");
                }
            }
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
            string guid = objects.getData("GUID");

            factory = new IOFactory();
            engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

            objects.setData("SelfScore", SelfScore.ValueText);
            objects.setData("SelfScore_W", SelfScore_W.ValueText);
            objects.setData("SelfComments", SelfComments.ValueText);
            objects.setData("FirstScore", FirstScore.ValueText);
            objects.setData("FirstScore_W", FirstScore_W.ValueText);
            objects.setData("FirstComments", FirstComments.ValueText);
            objects.setData("SecondScore", SecondScore.ValueText);
            objects.setData("SecondScore_W", SecondScore_W.ValueText);
            objects.setData("SecondComments", SecondComments.ValueText);
            //儲存Grid資料
            errMsg += saveGrid(objects);
            //計算總分
            computeTotalScore(objects);

            SmpPmMaintainUtil.getEncodeValue(objects, encodeFields2);
            DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
            SmpPmMaintainUtil.getEncodeValue(scoreDetailSet, encodeFields);

            //儲存附件
            for (int i = 0; i < AttachmentList.dataSource.getAvailableDataObjectCount(); i++)
            {
                DataObject data = AttachmentList.dataSource.getDataObject(i);
                data.setData("AssessmentUserScoreGUID", guid);
                string fileName = data.getData("FILENAME");
                int idxs = fileName.IndexOf("]}");
                int idxe = fileName.LastIndexOf("{[");
                if (idxs > 0 && idxe > 0)
                {
                    fileName = fileName.Substring(idxs + 2, idxe - (idxs + 2));
                }
                data.setData("FILENAME", fileName); //將filename換回, 避免url過長, 造成saveDB報錯! 
            }
            FileUpload.setJobID(guid);
            FileUpload.confirmSave("PMS", objects.getData("empNumber"));
            FileUpload.saveFile();
            FileUpload.engine.close();
        }
        catch (Exception e)
        {
            errMsg += e.Message;
            errMsg = errMsg.Replace("\n", "; ");
            writeLog(e);
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
    /// 
    /// </summary>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected string saveGrid(com.dsc.kernal.databean.DataObject objects)
    {
        string errMsg = "";
        try
        {
            //gridview save
            DataObjectSet scoreDetailSet = objects.getChild("SmpPmAssessmentScoreDetail");
            string stage = objects.getData("Stage");
            foreach (GridViewRow r in GridViewEvaluationDetail.Rows)
            {
                string tbGuid;
                string tbSelfScore;
                string tbSelfComments;
                string tbFirstScore;
                string tbFirstComments;
                string tbSecondScore;
                string tbSecondComments;

                tbGuid = ((TextBox)r.FindControl("tbGUID")).Text;
                tbSelfScore = ((TextBox)r.FindControl("tbSelfScore")).Text;
                tbSelfComments = ((TextBox)r.FindControl("tbSelfComments")).Text;
                tbFirstScore = ((TextBox)r.FindControl("tbFirstScore")).Text;
                tbFirstComments = ((TextBox)r.FindControl("tbFirstComments")).Text;
                tbSecondScore = ((TextBox)r.FindControl("tbSecondScore")).Text;
                tbSecondComments = ((TextBox)r.FindControl("tbSecondComments")).Text;

                for (int i = 0; i < scoreDetailSet.getAvailableDataObjectCount(); i++)
                {
                    DataObject dataObject = scoreDetailSet.getAvailableDataObject(i);
                    string guid = dataObject.getData("GUID");
                    string itemNo = dataObject.getData("ItemNo");
                    string minFraction = dataObject.getData("MinFraction");
                    string maxFraction = dataObject.getData("MaxFraction");
                    string selfScore = dataObject.getData("SelfScore");
                    string firstScore = dataObject.getData("FirstScore");
                    string secondScore = dataObject.getData("SecondScore");
                    int intMinFraction;
                    int intMaxFraction;
                    bool isNumeric;
                    isNumeric = int.TryParse(minFraction, out intMinFraction);
                    isNumeric = int.TryParse(maxFraction, out intMaxFraction);

                    if (guid.Equals(tbGuid))
                    {
                        dataObject.setData("SelfScore", tbSelfScore);
                        dataObject.setData("SelfComments", tbSelfComments);
                        
                        //二階默認儲存一階成績
                        if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
                        {
                            dataObject.setData("FirstScore", tbFirstScore);
                            dataObject.setData("FirstComments", tbFirstComments);
                            dataObject.setData("SecondScore", tbFirstScore);
                            dataObject.setData("SecondComments", tbFirstComments);
                        }
                        else
                        {
                            dataObject.setData("SecondScore", tbSecondScore);
                            dataObject.setData("SecondComments", tbSecondComments);
                        }
                        
                        int number;

                        if (!string.IsNullOrEmpty(tbSelfScore))
                        {
                            isNumeric = int.TryParse(tbSelfScore, out number);
                            if (isNumeric)
                            {
                                if (number > intMaxFraction)
                                {
                                    errMsg += "編號 " + itemNo + " 分數 " + number + " 大於最高分數!\n";
                                }
                                if (number < intMinFraction)
                                {
                                    errMsg += "編號 " + itemNo + " 分數 " + number + " 小於最低分數!\n";
                                }
                            }
                            else
                            {
                                errMsg += "編號 " + itemNo + " 分數欄位必需為數值!";
                            }
                        }

                        if (!string.IsNullOrEmpty(tbFirstScore))
                        {
                            isNumeric = int.TryParse(tbFirstScore, out number);
                            if (isNumeric)
                            {
                                if (number > intMaxFraction)
                                {
                                    errMsg += "編號 " + itemNo + " 分數 " + number + " 大於最高分數!\n";
                                }
                                if (number < intMinFraction)
                                {
                                    errMsg += "編號 " + itemNo + " 分數 " + number + " 小於最低分數!\n";
                                }
                            }
                            else
                            {
                                errMsg += "編號 " + itemNo + " 分數欄位必需為數值!";
                            }
                        }

                        if (!string.IsNullOrEmpty(tbSecondScore))
                        {
                            isNumeric = int.TryParse(tbSecondScore, out number);
                            if (isNumeric)
                            {
                                if (number > intMaxFraction)
                                {
                                    errMsg += "編號 " + itemNo + " 分數 " + number + " 大於最高分數!\n";
                                }
                                if (number < intMinFraction)
                                {
                                    errMsg += "編號 " + itemNo + " 分數 " + number + " 小於最低分數!\n";
                                }
                            }
                            else
                            {
                                errMsg += "編號 " + itemNo + " 分數欄位必需為數值!";
                            }
                        }

                        

                        break;
                    }
                }
            }
            errMsg += SmpPmMaintainUtil.checkScore(objects);
        }
        catch (Exception e)
        {
            writeLog(e);
            errMsg += e.Message;
        }
        return errMsg;
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
            SmpPmMaintainUtil.getDecodeValue(objects, encodeFields2);
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
        string selfEvaluation = objects.getData("SelfEvaluation");

        if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
        {
            LblSelfScore.Display = true;
            SelfScore.Display = false;
            SelfScore_W.Display = true;
            LblSelfComments.Display = true;
            SelfComments.Display = true;
        }
        else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
        {
            SelfComments.ReadOnly = true;

            LblFirstScore.Display = true;
            FirstScore.Display = false;
            FirstScore_W.Display = true;
            LblFirstComments.Display = true;
            FirstComments.Display = true;

            if (selfEvaluation.Equals(SmpPmMaintainUtil.ASSESSMENT_YES))
            {
                LblSelfScore.Display = true;
                SelfScore.Display = false;
                SelfScore_W.Display = true;
                LblSelfComments.Display = true;
                SelfComments.Display = true;
            }
        }
        else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
        {
            SelfComments.ReadOnly = true;
            FirstComments.ReadOnly = true;
            
            LblFirstScore.Display = true;
            FirstScore.Display = false;
            FirstScore_W.Display = true;
            LblFirstComments.Display = true;
            FirstComments.Display = true;
            LblSecondScore.Display = true;
            SecondScore.Display = false;
            SecondScore_W.Display = true;
            LblSecondComments.Display = true;
            SecondComments.Display = true;

            if (selfEvaluation.Equals(SmpPmMaintainUtil.ASSESSMENT_YES))
            {
                LblSelfScore.Display = true;
                SelfScore.Display = false;
                SelfScore_W.Display = true;
                LblSelfComments.Display = true;
                SelfComments.Display = true;
            }
        }

        if (!status.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN))
        {
            GbSendAssessmentScore.Display = false;
            SelfComments.ReadOnly = true;
            FirstComments.ReadOnly = true;
            SecondComments.ReadOnly = true;
            AssessmentScoreDetailList.ReadOnly = true;
            SaveButton.ReadOnly = true;
            FileUpload.ReadOnly = true;
            AttachmentList.ReadOnly = true;
        }
        ExitButton.Display = false;
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
            string sql = "select a.Name EvaluationName, b.GUID EvaluationDetailGUID, b.ItemNo, b.ItemName, b.Content, b.FractionExp, b.MinFraction, b.MaxFraction,b.ItemNum,b.ItemWeight from SmpPmEvaluation a, SmpPmEvaluationDetail b where a.GUID='" + evaluationGUID + "' and a.GUID=b.EvaluationGUID ";
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
                string ItemNum = ds.Tables[0].Rows[i]["ItemNum"].ToString();
                string ItemWeight = ds.Tables[0].Rows[i]["ItemWeight"].ToString();
                
                DataObject data = scoreDetailSet.create();
                data.setData("GUID", IDProcessor.getID(""));
                data.setData("IS_DISPLAY", "Y");
                data.setData("IS_LOCK", "N");
                data.setData("DATA_STATUS", "Y");
                data.setData("D_INSERTUSER", "");
                data.setData("D_INSERTTIME", "");
                data.setData("D_MODIFYUSER", "");
                data.setData("D_MODIFYTIME", "");
                data.setData("AssessmentUserScoreGUID", objects.getData("GUID"));
                data.setData("EvaluationDetailGUID", evaluationDetailGUID);
                data.setData("UserGUID", objects.getData("UserGUID"));
                data.setData("ItemNo", itemNo);
                data.setData("ItemName", itemName);
                data.setData("Content", content);
                data.setData("FractionExp", fractionExp);
                data.setData("MinFraction", minFraction);
                data.setData("MaxFraction", maxFraction);
                data.setData("ItemNum", ItemNum);
                data.setData("ItemWeight", ItemWeight);
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
        double totalSelfScore = 0;
        double totalFirstScore = 0;
        double totalSecondScore = 0;
        double totalSelfScore_W = 0;
        double totalFirstScore_W = 0;
        double totalSecondScore_W = 0;
        int row = scoreDetailSet.getAvailableDataObjectCount();
        for (int i = 0; i < row; i++)
        {
            DataObject data = scoreDetailSet.getAvailableDataObject(i);
            string selfScore = data.getData("SelfScore");
            string firstScore = data.getData("FirstScore");
            string secondScore = data.getData("SecondScore");
            string ItemNum = data.getData("ItemNum");
            string ItemWeight = data.getData("ItemWeight");
            string MaxFraction = data.getData("MaxFraction");

            int Num = 0;
            int weight = 0;
            int MaxFraction_d = 0;

            isNumeric = int.TryParse(ItemNum, out Num);

            isNumeric = int.TryParse(ItemWeight, out weight);

            isNumeric = int.TryParse(MaxFraction, out MaxFraction_d);

            isNumeric = int.TryParse(selfScore, out number);
            totalSelfScore += number;
            totalSelfScore_W += (Convert.ToDouble(weight) / (Convert.ToDouble(Num) * Convert.ToDouble(MaxFraction_d))) * Convert.ToDouble(number);

            isNumeric = int.TryParse(firstScore, out number);
            totalFirstScore += number;
            totalFirstScore_W += (Convert.ToDouble(weight) / (Convert.ToDouble(Num) * Convert.ToDouble(MaxFraction_d))) * Convert.ToDouble(number);

            isNumeric = int.TryParse(secondScore, out number);
            totalSecondScore += number;
            totalSecondScore_W += (Convert.ToDouble(weight) / (Convert.ToDouble(Num) * Convert.ToDouble(MaxFraction_d))) * Convert.ToDouble(number);
        }

        if (totalSelfScore > 0)
        {
            SelfScore.ValueText = totalSelfScore + "";
            objects.setData("SelfScore", totalSelfScore + "");            
            objects.setData("SelfScore_W", Math.Round(totalSelfScore_W, 2).ToString() + "");
        }

        if (totalFirstScore > 0)
        {
            FirstScore.ValueText = totalFirstScore + "";
            objects.setData("FirstScore", totalFirstScore + "");         
            objects.setData("FirstScore_W", Math.Round(totalFirstScore_W, 2) + "");
        }

        if (totalSecondScore > 0)
        {
            SecondScore.ValueText = totalSecondScore + "";
            objects.setData("SecondScore", totalSecondScore + "");           
            objects.setData("SecondScore_W", Math.Round(totalSecondScore_W, 2) + "");
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
            errMsg += SmpPmMaintainUtil.saveAssessmentStage(objects);
            //發送mail給下一關主管
            errMsg += SmpPmMaintainUtil.sendAssessmentToNextUser(objects);
            //檢查是否為最後一個考核對象
            bool finalStageComplete = false;
            errMsg += SmpPmMaintainUtil.isFinalStageAssessUser(objects, out finalStageComplete);
            if (finalStageComplete)
            {
                string assessmentPlanGUID = objects.getData("AssessmentPlanGUID");
                string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
                errMsg += SmpPmMaintainUtil.mailAchievementAssessUser(assessmentPlanGUID, assessmentManagerGUID);
                MessageBox("本階段所有考評對象均已評核完畢!");
            }
            //儲存資料
            SaveButton_Click(sender, e);
        }

        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            //throw new Exception(errMsg);
            MessageBox(errMsg);
        }
        
    }
    public string checkScoreTotal(com.dsc.kernal.databean.DataObject objects)
    {
        //檢查大小過禁止評核等級
        IOFactory factory = null;
        AbstractEngine engine = null;
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

            if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
            {
                if(string.IsNullOrEmpty(firstScore))
                {
                    errMsg += "請先儲存后再送出成績！";
                }
                if (Convert.ToInt32(firstScore) >= 90 && (!string.IsNullOrEmpty(MisTake1) || !string.IsNullOrEmpty(MisTake2)))
                {
                    errMsg += itemNo + ":大小過人員不可評A;";
                }
                if (Convert.ToInt32(firstScore) >= 80 && Convert.ToInt32(firstScore) < 90 && !string.IsNullOrEmpty(MisTake2))
                {
                    errMsg += itemNo + ":大過人員不可評B;";
                }
            }
            else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            {
                if (string.IsNullOrEmpty(secondScore))
                {
                    errMsg += "請先儲存后再送出成績！";
                }
                if (Convert.ToInt32(secondScore) >= 90 && (!string.IsNullOrEmpty(MisTake1) || !string.IsNullOrEmpty(MisTake2)))
                {
                    errMsg += itemNo + ":大小過人員不可評A;";
                }
                if (Convert.ToInt32(secondScore) >= 80 && Convert.ToInt32(secondScore) < 90 && !string.IsNullOrEmpty(MisTake2))
                {
                    errMsg += itemNo + ":大過人員不可評B;";
                }
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
    /// 送出成績時檢查資料
    /// </summary>
    /// <returns></returns>
    protected string sendAssessmentScoreCheck(DataObject objects)
    {
        string errMsg = "";
        //string result = "";
        //檢查所項目均必需有分數
        errMsg += saveGrid(objects);
        errMsg += SmpPmMaintainUtil.checkScore(objects);
        errMsg += checkScoreTotal(objects);
        //DataObjectSet scoreSet = AssessmentScoreDetailList.dataSource;
        //int row = scoreSet.getAvailableDataObjectCount();
        //for (int i = 0; i < row; i++)
        //{
        //    DataObject data = scoreSet.getAvailableDataObject(i);
        //    string itemNo = data.getData("ItemNo");
        //    string selfScore = data.getData("SelfScore");
        //    string firstScore = data.getData("FirstScore");
        //    string secondScore = data.getData("SecondScore");

        //    if (Stage.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
        //    {
        //        if (string.IsNullOrEmpty(selfScore))
        //        {
        //            result += itemNo + ";";
        //        }
        //    }
        //    else if (Stage.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
        //    {
        //        if (string.IsNullOrEmpty(firstScore))
        //        {
        //            result += itemNo + ";";
        //        }
        //    }
        //    else if (Stage.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
        //    {
        //        if (string.IsNullOrEmpty(secondScore))
        //        {
        //            result += itemNo + ";";
        //        }
        //    }
        //}
        //if (!string.IsNullOrEmpty(result))
        //{
        //    errMsg += "評核項目必需填寫分數, 編號: " + result;
        //}

        return errMsg;
    }

    //protected string checkScore(DataObject objects)
    //{
    //    string errMsg = "";
    //    string result = "";
    //    //檢查所項目均必需有分數
    //    string stage = objects.getData("Stage");
    //    DataObjectSet scoreSet = objects.getChild("SmpPmAssessmentScoreDetail");
    //    int row = scoreSet.getAvailableDataObjectCount();
    //    for (int i = 0; i < row; i++)
    //    {
    //        DataObject data = scoreSet.getAvailableDataObject(i);
    //        string itemNo = data.getData("ItemNo");
    //        string selfScore = data.getData("SelfScore");
    //        string firstScore = data.getData("FirstScore");
    //        string secondScore = data.getData("SecondScore");

    //        if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
    //        {
    //            if (string.IsNullOrEmpty(selfScore))
    //            {
    //                result += itemNo + ";";
    //            }
    //        }
    //        else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
    //        {
    //            if (string.IsNullOrEmpty(firstScore))
    //            {
    //                result += itemNo + ";";
    //            }
    //        }
    //        else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
    //        {
    //            if (string.IsNullOrEmpty(secondScore))
    //            {
    //                result += itemNo + ";";
    //            }
    //        }
    //    }
    //    if (!string.IsNullOrEmpty(result))
    //    {
    //        errMsg += "評核項目必需填寫分數, 編號: " + result;
    //    }
        
    //    return errMsg;
    //}

    /// <summary>
    /// 儲存評核階段狀態
    /// </summary>
    //protected string saveAssessmentStage(DataObject objects)
    //{
    //    string errMsg = "";
    //    AbstractEngine engine = null;
    //    try
    //    {
    //        string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
    //        string stage = objects.getData("Stage");
    //        string dateTimeNow = DateTimeUtility.getSystemTime2(null);
    //        objects.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE);
    //        objects.setData("CompleteDate", dateTimeNow);
    //        string connectString = (string)Session["connectString"];
    //        string engineType = (string)Session["engineType"];
    //        IOFactory factory = new IOFactory();
    //        engine = factory.getEngine(engineType, connectString);
    //        NLAgent agent = new NLAgent();
    //        agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAssessmentStageAgent");
    //        agent.engine = engine;
    //        agent.query("AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage='" + stage + "'");
    //        DataObjectSet set = agent.defaultData;
    //        int row = set.getAvailableDataObjectCount();
    //        if (row > 0)
    //        {
    //            DataObject data = set.getAvailableDataObject(0);
    //            data.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE);
    //            data.setData("CompleteDate", dateTimeNow);
    //            if (!agent.update())
    //            {
    //                errMsg += agent.engine.errorString;
    //            }
    //        }
    //        else
    //        {
    //            errMsg += "找不到資料更新!";
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        errMsg += e.Message;
    //        writeLog(e);
    //    }
    //    finally
    //    {
    //        if (engine != null)
    //        {
    //            engine.close();
    //        }
    //    }
    //    return errMsg;
    //}

    /// <summary>
    /// 觸發下一階段評核
    /// </summary>
    /// <param name="assessmentManagerGUID"></param>
    /// <param name="stage"></param>
    //protected string sendAssessmentToNextUser(DataObject objects)
    //{
    //    string errMsg = "";
    //    AbstractEngine engine = null;
    //    try
    //    {
    //        string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
    //        string stage = objects.getData("Stage");
    //        string connectString = (string)HttpContext.Current.Session["connectString"];
    //        string engineType = (string)HttpContext.Current.Session["engineType"];
    //        IOFactory factory = new IOFactory();
    //        engine = factory.getEngine(engineType, connectString);

    //        //更新下一關狀態/啟始日
    //        string sql = "select Stage from SmpPmUserAssessmentStage where AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage > '" + stage + "' order by Stage";
    //        DataSet ds = engine.getDataSet(sql, "TEMP");
    //        int row = ds.Tables[0].Rows.Count;
    //        if (row > 0)
    //        {
    //            string minStage = ds.Tables[0].Rows[0]["Stage"].ToString();
    //            NLAgent agent = new NLAgent();
    //            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAssessmentStageAgent");
    //            agent.engine = engine;
    //            agent.query("AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage='" + minStage + "'");
    //            DataObjectSet set = agent.defaultData;
    //            row = set.getAvailableDataObjectCount();
    //            if (row > 0)
    //            {
    //                DataObject data = set.getAvailableDataObject(0);
    //                data.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);
    //                string dateTimeNow = DateTimeUtility.getSystemTime2(null);
    //                data.setData("StartDate", dateTimeNow);
    //                if (!agent.update())
    //                {
    //                    errMsg += agent.engine.errorString;
    //                }
    //                errMsg += SmpPmMaintainUtil.sendAssessment(data);
    //            }
    //            else
    //            {
    //                //MessageBox("找不到資料更新!");
    //            }
    //        }
    //        else
    //        {
    //            NLAgent agent = new NLAgent();
    //            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAchievementAgent");
    //            agent.engine = engine;
    //            agent.query("AssessmentManagerGUID='" + assessmentManagerGUID + "'");
    //            DataObjectSet set = agent.defaultData;
    //            row = set.getAvailableDataObjectCount();
    //            if (row > 0)
    //            {
    //                int score = 0;
    //                int number = 0;
    //                bool isNumeric = false;
    //                string firstScore = objects.getData("FirstScore");
    //                string secondScore = objects.getData("SecondScore");
    //                MessageBox("firstScore=" + firstScore);
    //                MessageBox("secondScore=" + secondScore);
    //                isNumeric = int.TryParse(firstScore, out number);
    //                if (isNumeric)
    //                {
    //                    score = number;
    //                }
    //                isNumeric = int.TryParse(secondScore, out number);
    //                if (isNumeric)
    //                {
    //                    score = number;
    //                }
    //                MessageBox("score=" + score + "");
    //                DataObject data = set.getAvailableDataObject(0);
    //                data.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);
    //                if (score > 0)
    //                {
    //                    data.setData("FinalScore", score + "");
    //                    string achievementLevel = SmpPmMaintainUtil.getAchievementLevel(engine, data);
    //                    data.setData("AchievementLevel", achievementLevel);
    //                }
    //                if (!agent.update())
    //                {
    //                    errMsg += agent.engine.errorString;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        errMsg += e.Message;
    //    }
    //    return errMsg;
    //}

    /// <summary>
    /// 發送信件
    /// </summary>
    /// <param name="objects"></param>
    //protected void sendAssessment(DataObject objects)
    //{
    //    AbstractEngine engine = null;

    //    try
    //    {
    //        string connectString = (string)Session["connectString"];
    //        string engineType = (string)Session["engineType"];

    //        IOFactory factory = new IOFactory();
    //        engine = factory.getEngine(engineType, connectString);
    //        string assessmentPlanGUID = "";
    //        string subject = "";
    //        string assessmentName = "";
    //        string firstAssessmentDays = "";
    //        string secondAssessmentDays = "";
    //        string assessmentDays = "";
    //        string assessmentStageGUID = objects.getData("GUID");
    //        string stage = objects.getData("Stage");
    //        //string userGUID = objects.getData("UsserGUID");
    //        string assessUserGUID = objects.getData("AssessUserGUID");
    //        string sql = "select a.AssessmentPlanGUID, a.Stage, b.AssessmentName, c.FirstAssessmentDays, c.SecondAssessmentDays from SmpPmUserAssessmentStage a, SmpPmAssessmentPlan b, SmpPmAssessmentConfig c where a.GUID='" + assessmentStageGUID + "' and a.AssessmentPlanGUID=b.GUID and b.GUID = c.AssessmentPlanGUID";
    //        DataSet ds = engine.getDataSet(sql, "TEMP");
    //        int row = ds.Tables[0].Rows.Count;
    //        if (row > 0)
    //        {
    //            assessmentPlanGUID = ds.Tables[0].Rows[0]["AssessmentPlanGUID"].ToString();
    //            assessmentName = ds.Tables[0].Rows[0]["AssessmentName"].ToString();
    //            firstAssessmentDays = ds.Tables[0].Rows[0]["FirstAssessmentDays"].ToString();
    //            secondAssessmentDays = ds.Tables[0].Rows[0]["SecondAssessmentDays"].ToString();
    //        }

    //        if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
    //        {
    //            subject = "[一階評核]" + assessmentName;
    //            assessmentDays = firstAssessmentDays;
    //        }
    //        else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
    //        {
    //            subject = "[二階評核]" + assessmentName;
    //            assessmentDays = secondAssessmentDays;
    //        }

    //        sql = "select b.mailAddress, b.userName, c.empName, a.GUID from SmpPmUserAssessmentStage a, Users b, SmpHrEmployeeInfoV c where a.GUID='" + assessmentStageGUID + "' and a.AssessUserGUID = b.OID and a.UserGUID=c.empGUID";
    //        ds = engine.getDataSet(sql, "TEMP");
    //        row = ds.Tables[0].Rows.Count;
    //        string[,] values = new string[row,4];
    //        for (int i = 0; i < row; i++)
    //        {
    //            values[i, 0] = ds.Tables[0].Rows[i][0].ToString();
    //            values[i, 1] = ds.Tables[0].Rows[i][1].ToString();
    //            values[i, 2] = ds.Tables[0].Rows[i][2].ToString();
    //            values[i, 3] = ds.Tables[0].Rows[i][3].ToString();
    //        }
            
    //        SysParam sp = new SysParam(engine);
    //        string mailclass = sp.getParam("MailClass");
    //        string smtpServer = sp.getParam("SMTP_Server");
    //        string systemMail = sp.getParam("EcpPmsMail");
    //        string emailHeader = sp.getParam("EmailHeader");
    //        MailFactory fac = new MailFactory();
    //        AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);
            
    //        for (int i = 0; i < row; i++)
    //        {
    //            string mailAddress = values[i, 0];
    //            string assessUserName = values[i, 1];
    //            string userName = values[i, 2];
    //            string objectGUID = values[i, 3];
    //            string content = "";
    //            string href = emailHeader + "?runMethod=executePmProgram&programID=SPPM005M&ObjectGUID=" + objectGUID;
    //            content += "考評名稱: " + assessmentName + "<br />";
    //            content += "評核截止天數: " + assessmentDays + " 天<br />";
    //            content += "考核人: " + assessUserName + " <br />";
    //            content += "考核對象: " + userName + " <br />";
    //            content += "待辦事項: <a href='" + href + "'>您有一個待辦事項,請按這裡連結至您的工作</a><br />";

    //            au.sendMailHTML("", smtpServer, mailAddress, "", systemMail, subject, content);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //    finally
    //    {
    //        if (engine != null)
    //        {
    //            engine.close();
    //        }
    //    }
    //}

    /// <summary>
    /// 最後評核階段, 所有考核對象均已完成則提示可以進行成績分配
    /// </summary>
    /// <param name="objects"></param>
    //protected string isFinalStageAssessUser(DataObject objects)
    //{
    //    string errMsg = "";
    //    string message = "";
    //    AbstractEngine engine = null;
        
    //    try
    //    {
    //        string connectString = (string)Session["connectString"];
    //        string engineType = (string)Session["engineType"];
    //        string userGUID = (string)Session["UserGUID"];

    //        IOFactory factory = new IOFactory();
    //        engine = factory.getEngine(engineType, connectString);
            
    //        string assessmentPlanGUID = objects.getData("AssessmentPlanGUID");
    //        string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
    //        string stage = objects.getData("Stage");
    //        string sql = "select AssessmentLevel from SmpPmAssessmentConfig where AssessmentPlanGUID = '" + assessmentPlanGUID + "'";
    //        DataSet ds = engine.getDataSet(sql, "TEMP");
    //        string assessmentLevel = ds.Tables[0].Rows[0][0].ToString();
    //        bool isFinalStage = false;
    //        if (assessmentLevel.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL1))
    //        {
    //            if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
    //            {
    //                isFinalStage = true;
    //            }
    //        }
    //        else if (assessmentLevel.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL2))
    //        {
    //            if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
    //            {
    //                isFinalStage = true;
    //            }
    //        }
    //        else if(assessmentLevel.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL0)) 
    //        {
    //            sql = "select Stage from SmpPmUserAssessmentStage where AssessmentManagerGUID='" + assessmentManagerGUID + "' and Stage > '" + stage + "' order by Stage";
    //            ds = engine.getDataSet(sql, "TEMP");
    //            int row = ds.Tables[0].Rows.Count;
    //            if (row == 0)
    //            {
    //                isFinalStage = true;
    //            }
    //        }

    //        if (isFinalStage)
    //        {
    //            sql = "select count('x') cnt from SmpPmUserAssessmentStage where AssessmentPlanGUID='" + assessmentPlanGUID + "' and AssessUserGUID='" + userGUID + "' and Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "'";
    //            ds = engine.getDataSet(sql, "TEMP");
    //            int cnt = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
    //            if (cnt == 0)
    //            {
    //                message = "本階段所有考評對象均已評核完畢! 請進行成績分配!";
    //                MessageBox(message);
    //            }
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        errMsg += e.Message;
    //        writeLog(e);
    //    }
    //    finally
    //    {
    //        if (engine != null)
    //        {
    //            engine.close();
    //        }
    //    }
    //    return errMsg;
    //}

    /// <summary>
    /// 上傳檔案後更新附件清單
    /// </summary>
    /// <param name="currentObject"></param>
    /// <param name="isNew"></param>
    protected void Atta_AddOutline(DSCWebControl.FileItem currentObject, bool isNew)
    {
        try
        {
            setSession("currentFile", currentObject);
            DataObject objects = (DataObject)getSession("objects");

            string assessmentUserScoreGUID = objects.getData("GUID");
            string stage = objects.getData("Stage");
            DataObjectSet detailSet = AttachmentList.dataSource;
            DataObject obj = detailSet.create();
            obj.setData("GUID", IDProcessor.getID(""));
            obj.setData("AssessmentUserScoreGUID", assessmentUserScoreGUID);
            obj.setData("Stage", stage);
            obj.setData("AttachmentType", "");
            obj.setData("FileItemGUID", currentObject.GUID);
            obj.setData("FILENAME", currentObject.FILENAME);
            obj.setData("FILEEXT", currentObject.FILEEXT);
            obj.setData("DESCRIPTION", currentObject.DESCRIPTION);
            obj.setData("UPLOADUSER", currentObject.UPLOADUSER);
            obj.setData("UPLOADTIME", currentObject.UPLOADTIME);
            obj.setData("IS_LOCK", "N");
            obj.setData("IS_DISPLAY", "Y");
            obj.setData("DATA_STATUS", "Y");
            detailSet.add(obj);
            AttachmentList.dataSource = detailSet;
            AttachmentList.updateTable();
        }
        catch (Exception e)
        {
            base.writeLog(e);
            MessageBox(e.StackTrace);
            throw new Exception(e.StackTrace);
        }
    }

    /// <summary>
    /// 刪除附件清單則同步移除檔案清單
    /// </summary>
    /// <returns></returns>
    protected bool Atta_BeforeDeleteData()
    {
        try
        {
            DataObject objects = (DataObject)getSession("objects");
            DataObject[] atta = AttachmentList.getSelectedItem();
            DataObjectSet setAtta = AttachmentList.dataSource;
            DataObjectSet setFile = FileUpload.uploadedList;

            string stage = objects.getData("Stage");
            bool isDelete = true;
            for (int i = 0; i < atta.Length; i++)
            {
                DataObject attaDataObject = atta[i];
                if (attaDataObject != null)
                {
                    string rowStage = attaDataObject.getData("Stage");
                    string fileName = attaDataObject.getData("FILENAME");
                    if (rowStage.Equals(stage))
                    {
                        string attaFileGUID = attaDataObject.getData("FileItemGUID");
                        for (int j = 0; j < setFile.getAvailableDataObjectCount(); j++)
                        {
                            DataObject fileDataObject = setFile.getAvailableDataObject(j);
                            string fileGUID = fileDataObject.getData("GUID");
                            if (fileGUID.Equals(attaFileGUID))
                            {
                                setFile.delete(fileDataObject);
                                break;
                            }
                        }
                    }
                    else
                    {
                        isDelete = false;
                        MessageBox("刪除清單含有其他階段附件不允許刪除!");
                    }
                }
            }

            FileUpload.uploadedList = setFile;
            return isDelete;
        }
        catch (Exception e)
        {
            base.writeLog(e);
            MessageBox(e.StackTrace);
            throw new Exception(e.StackTrace);
        }
    }

    /// <summary>
    /// 點選上傳檔案開啟上傳檔案元件對話窗
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Upload_Click(object sender, EventArgs e)
    {
        FileUpload.openFileUploadDialog();
    }
    protected void computer_Click(object sender, EventArgs e)
    {

        DataObject objects = (DataObject)getSession("objects");
        IOFactory factory = null;
        AbstractEngine engine = null;
        double number = 0;

        factory = new IOFactory();
        engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);

        string stage = objects.getData("Stage");

        foreach (GridViewRow row in GridViewEvaluationDetail.Rows)
        {
            if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
            {
                string temp = ((TextBox)row.FindControl("tbFirstScore")).Text.Trim();
                if (string.IsNullOrEmpty(temp))
                {
                    number += 0;
                }
                else
                {
                    number += Convert.ToDouble(((TextBox)row.FindControl("tbFirstScore")).Text);
                }

            }
            else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            {
                string temp = ((TextBox)row.FindControl("tbSecondScore")).Text.Trim();
                if (string.IsNullOrEmpty(temp))
                {
                    number += 0;
                }
                else
                {
                    number += Convert.ToDouble(((TextBox)row.FindControl("tbSecondScore")).Text);
                }
            }


        }
        //TextBox tbFirstScore = ((TextBox)r.FindControl("tbFirstScore"));
        total.ValueText = number.ToString();
    }
}