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

public partial class SmpProgram_Maintain_SPPM007_Input : BaseWebUI.GeneralWebPage
{
    string[] encodeFields = new string[] { "SelfScore_W", "FirstScore_W", "SecondScore_W", "FinalScore" };
	
    protected override void OnInit(EventArgs e)
    {
        maintainIdentity = "SPPM007M";
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
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                string[,] ids = null;
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);
                //設定考核計畫
                ids = SmpPmMaintainUtil.getAssessmentName(engine);
                AssessmentPlanGUID.setListItem(ids);
                

                //姓名查詢
                sowfEmpNo.connectDBString = connectString;
                sowfEmpNo.clientEngineType = engineType;



                //設定狀態
                ids = SmpPmMaintainUtil.getAchievementLevel(engine);
                AchievementLevel.setListItem(ids);

                //部門
                //CheckDept.clientEngineType = engineType;
                //CheckDept.connectDBString = connectString;

                //是否顯示部門
                if (funcId.Equals("0")) //個人
                {
                //    lblCheckDept.Visible = false;
                //    CheckDept.Display = false;
                    LblAssessmentMemberGroupGUID.Visible = false;
                    OrgUnitOID.Visible = false;
                    OrgUnitOID.Height = 20;
                    GbSelectOrgUnit.Visible = false;
                    GbDeleteOrgUnit.Visible = false;
                    OpenWinAssessmentMembers.Visible = false;
                }
                //else
                //{
                //    lblCheckDept.Visible = true;
                //    CheckDept.Display = true;
                //}

                OpenWinAssessmentMembers.connectDBString = connectString;
                OpenWinAssessmentMembers.clientEngineType = engineType;

                lblAccount.Display = false;
                sfAccount.Display = false;
                lblPassword.Display = false;
                sfPassword.Display = false;

                string userGUID = (string)Session["UserId"];
                string whereClause = "1=2";                
                queryData(whereClause);
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
        string userGUID ="";
        string whereClause = "";

        if (funcId.Equals("0"))
        {
            userGUID = GetUserGUID(sfAccount.ValueText);//于20170803Marcia修改以便沒有帳號人員可以績效考評
        }
        else
        {
            userGUID = (string)Session["UserId"];
        }

        if (funcId.Equals("0")) //個人，只可查有自核且狀態為COMPLETE或CLOSE
        {
            whereClause += " (UserGUID='" + userGUID + "' and (Status = '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE + "' OR  " +
                "Status ='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CLOSE + "') and SelfEvaluation ='Y' )" + " and empNumber='"+sfAccount.ValueText+"' and '"+sfPassword.ValueText+"'=(select right(IDCardNo,6) from [10.3.11.92\\SQL2008].SCQHRDB.dbo.PerEmployee where EmpNo collate chinese_taiwan_stroke_ci_as=empNumber)";
        }
        else
        {
            string userIdInfo = SmpPmMaintainUtil.GetEmpNoCollect(userGUID, AssessmentPlanGUID.ValueText);
            if (!userIdInfo.Equals(""))
            {
                whereClause += " UserGUID in (" + userIdInfo + ") and ";
            }
            else
            {
                whereClause += " 1=2 and ";
            }
            if ((string)Session["UserId"] == "Q1508126" || (string)Session["UserId"] == "Y1701199")
            {
                whereClause = " 1=1 and ";
            }
            whereClause += "  Status <>'" + SmpPmMaintainUtil.ASSESSMENT_STATUS_CANCEL + "' ";
        }

        if (!string.IsNullOrEmpty(AssessmentPlanGUID.ValueText))
        {
            whereClause += " and AssessmentPlanGUID in(" + SmpPmMaintainUtil.GetfilterNameplanGUID(AssessmentPlanGUID.ValueText) + ")";
        }

        if (!string.IsNullOrEmpty(AchievementLevel.ValueText))
        {
            whereClause += " and AchievementLevel='" + AchievementLevel.ValueText + "' ";
        }
        if (!string.IsNullOrEmpty(sowfEmpNo.ValueText))
        {
            whereClause += " and empNumber='" + sowfEmpNo.ValueText + "' ";
        }
        //if (!string.IsNullOrEmpty(CheckDept.ValueText))
        //{
        //    whereClause += " and deptId='" + CheckDept.ValueText + "'";
        //}
        try
        {
            string depts = "";
            string[,] ns = OrgUnitOID.getListItem();
            if (ns != null)
            {
                int length = ns.Length / 2;
                for (int i = 0; i < length; i++)
                {
                    string nsDeptId = ns[i, 0];
                    string nsDeptName = ns[i, 1];
                    if (!string.IsNullOrEmpty(nsDeptId))
                    {
                        if (i == 0)
                        {
                            depts += "'" + nsDeptId + "'";
                        }
                        else
                        {
                            depts += ",'" + nsDeptId + "'";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(depts.Trim()))
                {
                    whereClause += " and deptId in (" + depts + ")";
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox(ex.StackTrace);
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
        string sqltxt = "select empGUID from SmpHrEmployeeInfoV where empNumber='" + EmpNo + "'";
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
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM007.SmpPmAssessmentUserScoreAgent");
            agent.engine = engine;
            agent.query(whereClause);
            DataObjectSet set = agent.defaultData;
           
            AchievementList.InputForm = "Detail.aspx";
            AchievementList.DialogHeight = 600;
            AchievementList.DialogWidth = 1000;
            AchievementList.HiddenField = new string[] { "GUID", "UserGUID", "AssessmentPlanGUID", "AssessmentManagerGUID", "EvaluationGUID", "EvaluationName", "Status", "SelfEvaluation", "SelfScore_W","SelfComments", "AchievementGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            AchievementList.dataSource = set;
            SmpPmMaintainUtil.getDecodeValue(set, encodeFields);
            AchievementList.updateTable();

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

    /// <summary>
    /// 考評對象-選擇部門
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSelectOrgUnit_Click(object sender, EventArgs e)
    {
        OpenWinAssessmentMembers.identityID = "001";
        OpenWinAssessmentMembers.paramString = "ME001";
        OpenWinAssessmentMembers.openWin("hrbumen", "001", true, "001");
    }

    /// <summary>
    /// 選擇部門依公司別過濾
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSelectOrgUnit_BeforeClicks(object sender, EventArgs e)
    {
        //string companyName = CompanyCode.ReadOnlyText;
        //OpenWinAssessmentMembers.whereClause = "organizationName='" + companyName + "'";
    }

    /// <summary>
    /// 觸發開窗元件
    /// </summary>
    /// <param name="identityid"></param>
    /// <param name="values"></param>
    protected void OpenWinAssessmentMembers_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            string[,] ls = OrgUnitOID.getListItem(); //已選擇人員, 0:代號, 1:名稱
            if (ls == null)
            {
                ls = new string[0, 2];
            }
            ArrayList ary = new ArrayList();
            for (int i = 0; i < values.GetLength(0); i++)
            {
                bool hasFound = false;
                for (int j = 0; j < ls.GetLength(0); j++)
                {
                    if (ls[j, 0].Equals(values[i, 1])) //判確挑選的人員是否已經選過
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    string[] tag = new string[] { values[i, 1], values[i, 0] + "(" + values[i, 1] + ")" }; //挑選結果畫面顯示
                    ary.Add(tag);
                }
            }

            string[,] ns = new string[ls.GetLength(0) + ary.Count, 2];
            for (int i = 0; i < ls.GetLength(0); i++)
            {
                ns[i, 0] = ls[i, 0];
                ns[i, 1] = ls[i, 1];
            }
            for (int i = 0; i < ary.Count; i++)
            {
                string[] tag = (string[])ary[i];
                ns[i + ls.GetLength(0), 0] = tag[0];
                ns[i + ls.GetLength(0), 1] = tag[1];
            }

            OrgUnitOID.setListItem(ns);
        }
    }

    /// <summary>
    /// 刪除所選擇的部門清單
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbDeleteOrgUnit_Click(object sender, EventArgs e)
    {
        if (OrgUnitOID.getListItem() == null)
        {
            return;
        }

        string[] OIDS = OrgUnitOID.ValueText;
        string[,] alls = OrgUnitOID.getListItem();
        ArrayList ary = new ArrayList();
        for (int i = 0; i < alls.GetLength(0); i++)
        {
            bool hasFound = false;
            for (int j = 0; j < OIDS.Length; j++)
            {
                if (alls[i, 0].Equals(OIDS[j]))
                {
                    hasFound = true;
                    break;
                }
            }
            if (!hasFound)
            {
                string[] tag = new string[] { alls[i, 0], alls[i, 1] };
                ary.Add(tag);
            }
        }

        string[,] ids = new string[ary.Count, 2];
        for (int i = 0; i < ary.Count; i++)
        {
            string[] tag = (string[])ary[i];
            ids[i, 0] = tag[0];
            ids[i, 1] = tag[1];
        }
        OrgUnitOID.setListItem(ids);
    } 
}
