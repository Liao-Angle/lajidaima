using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebServerProject;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.kernal.mail;
using smp.pms.utility;

public partial class SmpProgram_Maintain_SPPM003_Input : BaseWebUI.DataListSaveForm
{

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
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                //設定公司別
                string[,] ids = SmpPmMaintainUtil.getCompanyIds(engine);
                CompanyCode.setListItem(ids);
                //設定考核表
                ids = SmpPmMaintainUtil.getEvaluationIds(engine);
                EvaluationGUID.setListItem(ids);
                //設定狀態
                ids = SmpPmMaintainUtil.getAssessmentStatusIds(engine);
                Status.setListItem(ids);
                //設定員工自評
                ids = SmpPmMaintainUtil.getYesNoIds(engine);
                SelfEvaluation.setListItem(ids);
                if (string.IsNullOrEmpty(SelfEvaluation.ValueText))
                {
                    SelfEvaluation.ValueText = SmpPmMaintainUtil.ASSESSMENT_YES;
                }
                //設定評核主管類別
                ids = SmpPmMaintainUtil.getAssessmentCategoryIds(engine);
                AssessmentCategory.setListItem(ids);
                //階層主管
                ids = SmpPmMaintainUtil.getAssessmentLevelIds(engine);
                AssessmentLevel.setListItem(ids);
                //設定成績分配人員
                ids = SmpPmMaintainUtil.getAchievementDistWayIds(engine);
                AchievementDistWay.setListItem(ids);

                OpenWinAssessmentMembers.connectDBString = connectString;
                OpenWinAssessmentMembers.clientEngineType = engineType;
                
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
            bool isNew = (bool)getSession("isNew");
            string status = objects.getData("Status");
            if (string.IsNullOrEmpty(status))
            {
                status = "DRAFT";
            }

            CompanyCode.ValueText = objects.getData("CompanyCode");
            AssessYear.ValueText = objects.getData("AssessYear");
            AssessStartDate.ValueText = objects.getData("AssessStartDate");
            AssessEndDate.ValueText = objects.getData("AssessEndDate");
            PlanEndDate.ValueText = objects.getData("PlanEndDate");
            AssessmentName.ValueText = objects.getData("AssessmentName");
            EvaluationGUID.ValueText = objects.getData("EvaluationGUID");
            Remark.ValueText = objects.getData("Remark");
            Status.ValueText = status;
            StartDate.ValueText = objects.getData("StartDate");
            CloseDate.ValueText = objects.getData("CloseDate");

            //顯示考評設定
            string assessmentPlanGUID = objects.getData("GUID");
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmAssessmentConfigAgent");
            agent.engine = engine;
            agent.query("AssessmentPlanGUID='" + assessmentPlanGUID + "'");
            DataObjectSet set = agent.defaultData;
            DataObject obj = null;
            int count = set.getAvailableDataObjectCount();

            if (count > 0)
            {
                obj = set.getAvailableDataObject(0);
                SelfEvaluation.ValueText = obj.getData("SelfEvaluation");
                AssessmentCategory.ValueText = obj.getData("AssessmentCategory");
                AssessmentLevel.ValueText = obj.getData("AssessmentLevel");
                AchievementDistWay.ValueText = obj.getData("AchievementDistWay");
                SelfEvaluationDays.ValueText = obj.getData("SelfEvaluationDays");
                FirstAssessmentDays.ValueText = obj.getData("FirstAssessmentDays");
                SecondAssessmentDays.ValueText = obj.getData("SecondAssessmentDays");
            }

            DataObjectSet membersDraftSet = null;
            //儲存考評對象
            if (isNew)
            {
                membersDraftSet = new DataObjectSet();
                membersDraftSet.isNameLess = true;
                membersDraftSet.setAssemblyName("WebServerProject");
                membersDraftSet.setChildClassString("WebServerProject.maintain.SPPM003.SmpPmAssessmentMembersDraft");
                membersDraftSet.setTableName("SmpPmAssessmentMembersDraft");
                membersDraftSet.loadFileSchema();
                objects.setChild("SmpPmAssessmentMembersDraft", membersDraftSet);
            }
            else
            {
                membersDraftSet = objects.getChild("SmpPmAssessmentMembersDraft");
            }
            AssessmentMembersDraftList.dataSource = membersDraftSet;
            AssessmentMembersDraftList.InputForm = "AssessmentMembersDraftDetail.aspx";
            AssessmentMembersDraftList.HiddenField = new string[] { "GUID", "AssessmentPlanGUID", "UserGUID", "deptOID", "deptId", "deptManagerId", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            AssessmentMembersDraftList.updateTable();
            AssessmentMembersDraftList.Display = false;

            DataObjectSet membersSet = null;
            //儲存考評對象
            if (isNew)
            {
                membersSet = new DataObjectSet();
                membersSet.isNameLess = true;
                membersSet.setAssemblyName("WebServerProject");
                membersSet.setChildClassString("WebServerProject.maintain.SPPM003.SmpPmAssessmentMembers");
                membersSet.setTableName("SmpPmAssessmentMembers");
                membersSet.loadFileSchema();
                objects.setChild("SmpPmAssessmentMembers", membersSet);
            }
            else
            {
                membersSet = objects.getChild("SmpPmAssessmentMembers");
            }
            AssessmentMembersList.dataSource = membersSet;
            AssessmentMembersList.HiddenField = new string[] { "GUID", "AssessmentPlanGUID", "UserGUID", "deptOID", "deptId", "deptManagerId", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            AssessmentMembersList.updateTable();


            DataObjectSet managerSet = null;
            //儲存考評人
            if (isNew)
            {
                managerSet = new DataObjectSet();
                managerSet.isNameLess = true;
                managerSet.setAssemblyName("WebServerProject");
                managerSet.setChildClassString("WebServerProject.maintain.SPPM003.SmpPmAssessmentManager");
                managerSet.setTableName("SmpPmAssessmentManager");
                managerSet.loadFileSchema();
                objects.setChild("SmpPmAssessmentManager", managerSet);
            }
            else
            {
                managerSet = objects.getChild("SmpPmAssessmentManager");
            }
            AssessmentManagerList.InputForm = "AssessmentManagerDetail.aspx";
            AssessmentManagerList.dataSource = managerSet;
            AssessmentManagerList.HiddenField = new string[] { "GUID", "AssessmentPlanGUID", "UserGUID", "deptOID", "deptId", "SelfEvaluateUserGUID", "FirstAssessUserGUID", "SecondAssessUserGUID", "IS_LOCK", "IS_DISPLAY", "DATA_STATUS" };
            AssessmentManagerList.updateTable();


            if (!status.Equals(SmpPmMaintainUtil.ASSESSMENT_STATUS_DRAFT))
            {
                disableAssessmentPlan();
            }

            //setSession("AssessmentPlanGUID", "AssessmentPlanGUID", objects.getData("GUID"));
            setSession("objects", objects);
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

            bool isNew = (bool)getSession("isNew");
            if (isNew)
            {
                objects.setData("GUID", IDProcessor.getID(""));
                objects.setData("IS_DISPLAY", "Y");
                objects.setData("IS_LOCK", "N");
                objects.setData("DATA_STATUS", "Y");
            }
            objects.setData("CompanyCode", CompanyCode.ValueText);
            objects.setData("AssessYear", AssessYear.ValueText);
            objects.setData("AssessStartDate", AssessStartDate.ValueText);
            objects.setData("AssessEndDate", AssessEndDate.ValueText);
            objects.setData("PlanEndDate", PlanEndDate.ValueText);
            objects.setData("AssessmentName", AssessmentName.ValueText);
            objects.setData("EvaluationGUID", EvaluationGUID.ValueText);
            objects.setData("Remark", Remark.ValueText);
            objects.setData("Status", Status.ValueText);
            objects.setData("StartDate", StartDate.ValueText);
            objects.setData("CloseDate", CloseDate.ValueText);

            //檢查欄位資料
            errMsg = checkFieldData(objects, engine);

            if (!errMsg.Equals(""))
            {
                errMsg = errMsg.Replace("\n", "; ");
            }

            //儲存考評設定
            string assessmentPlanGUID = objects.getData("GUID");
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmAssessmentConfigAgent");
            agent.engine = engine;
            agent.query("AssessmentPlanGUID='" + assessmentPlanGUID + "'");
            DataObjectSet configSet = agent.defaultData;
            DataObject obj = null;
            int count = configSet.getAvailableDataObjectCount();
            
            if (count > 0)
            {
                obj = configSet.getAvailableDataObject(0);
            }
            else
            {
                obj = configSet.create();
            }

            obj.setData("AssessmentPlanGUID", assessmentPlanGUID);
            obj.setData("SelfEvaluation", SelfEvaluation.ValueText);
            obj.setData("AssessmentCategory", AssessmentCategory.ValueText);
            obj.setData("AchievementDistWay", AchievementDistWay.ValueText);
            obj.setData("AssessmentLevel", AssessmentLevel.ValueText);
            obj.setData("SelfEvaluationDays", SelfEvaluationDays.ValueText);
            obj.setData("FirstAssessmentDays", FirstAssessmentDays.ValueText);
            obj.setData("SecondAssessmentDays", SecondAssessmentDays.ValueText);

            if (count == 0)
            {
                obj.setData("GUID", IDProcessor.getID(""));
                obj.setData("IS_DISPLAY", "Y");
                obj.setData("IS_LOCK", "N");
                obj.setData("DATA_STATUS", "Y");
                configSet.add(obj);
            }

            agent.defaultData = configSet;

            if (errMsg.Equals(""))
            {
                if (!agent.update())
                {
                    errMsg += agent.engine.errorString;
                }

                //儲存考核對象
                DataObjectSet memberDraftSet = AssessmentMembersDraftList.dataSource;
                for (int i = 0; i < memberDraftSet.getAvailableDataObjectCount(); i++)
                {
                    DataObject data = memberDraftSet.getAvailableDataObject(i);
                    data.setData("AssessmentPlanGUID", objects.getData("GUID")); //將明細的關聯至單頭
                }

                DataObjectSet memberSet = AssessmentMembersList.dataSource;
                for (int i = 0; i < memberSet.getAvailableDataObjectCount(); i++)
                {
                    DataObject data = memberSet.getAvailableDataObject(i);
                    data.setData("AssessmentPlanGUID", objects.getData("GUID")); //將明細的關聯至單頭
                }

                //儲存考核人
                DataObjectSet managerSet = AssessmentManagerList.dataSource;
                for (int i = 0; i < managerSet.getAvailableDataObjectCount(); i++)
                {
                    DataObject data = managerSet.getAvailableDataObject(i);
                    data.setData("AssessmentPlanGUID", objects.getData("GUID")); //將明細的關聯至單頭
                }
            }

            setSession("objects", objects);
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
    /// 檢查資料
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="engine"></param>
    /// <returns></returns>
    public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        string result = "";

        //公司別
        if (string.IsNullOrEmpty(CompanyCode.ValueText))
        {
            errMsg += LblCompanyCode.Text + " 欄位必需填寫!\n";
        }

        //考評年度
        int number = 0;
        bool isNumeric = int.TryParse(AssessYear.ValueText, out number);
        if (!isNumeric)
        {
            errMsg += LblAssessYear.Text + " 欄位必需為YYYY格式!\n";
        }

        //考評期間
        if (string.IsNullOrEmpty(AssessStartDate.ValueText))
        {
            errMsg += LblAssessDate.Text + " 欄位必需填寫!\n";
        }

        if (string.IsNullOrEmpty(AssessEndDate.ValueText))
        {
            errMsg += LblAssessDate.Text + " 欄位必需填寫!\n";
        }

        //考評名稱
        if (string.IsNullOrEmpty(AssessmentName.ValueText))
        {
            errMsg += LblAssessmentName.Text + " 欄位必需填寫!\n";
        }

        //考評表
        if (string.IsNullOrEmpty(EvaluationGUID.ValueText))
        {
            errMsg += LblEvaluationGUID.Text + " 欄位必需填寫!\n";
        }

        //是否自評
        if (string.IsNullOrEmpty(SelfEvaluation.ValueText))
        {
            errMsg += LblSelfEvaluation.Text + " 欄位必需填寫!\n";
        }

        //考核人-自評人員
        DataObjectSet assessManagerListSet = AssessmentManagerList.dataSource;
        int row = assessManagerListSet.getAvailableDataObjectCount();
        if (SelfEvaluation.ValueText.Equals("Y"))
        {
            //自評天數
            if (string.IsNullOrEmpty(SelfEvaluationDays.ValueText))
            {
                errMsg += LblSelfEvaluationDays.Text + " 欄位必需填寫!\n";
            }

            for (int i = 0; i < row; i++)
            {
                DataObject data = assessManagerListSet.getAvailableDataObject(i);
                string empNumber = data.getData("empNumber");
                string empName = data.getData("empName");
                string selfEvaluateUserGUID = data.getData("SelfEvaluateUserGUID");
                string selfEvaluateUserName = data.getData("selfEvaluateUserName");
                if (string.IsNullOrEmpty(selfEvaluateUserName))
                {
                    result += empNumber + " " + empName + ";";
                }
            }

            if (!string.IsNullOrEmpty(result))
            {
                errMsg += "自評人員必需指定: " + result;
            }
        }

        //評核人員類別
        if (string.IsNullOrEmpty(AssessmentCategory.ValueText))
        {
            errMsg += LblAssessmentCategory.Text + " 欄位必需填寫!\n";
        }

        //評核階次
        if (string.IsNullOrEmpty(AssessmentLevel.ValueText))
        {
            errMsg += LblAssessmentLevel.Text + " 欄位必需填寫!\n";
        }

        //一階評核天數
        if (string.IsNullOrEmpty(FirstAssessmentDays.ValueText))
        {
            errMsg += LblFirstAssessmentDays.Text + " 欄位必需填寫!\n";
        }

        //二階評核天數
        if (string.IsNullOrEmpty(SecondAssessmentDays.ValueText))
        {
            if (AssessmentLevel.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
            {
                errMsg += LblSecondAssessmentDays.Text + " 欄位必需填寫!\n";
            }
        }


        //考核人一階主管
        result = "";
        for (int i = 0; i < row; i++)
        {
            DataObject data = assessManagerListSet.getAvailableDataObject(i);
            string empNumber = data.getData("empNumber");
            string empName = data.getData("empName");
            string firstAssessManagerName = data.getData("firstAssessManagerName");
            if (string.IsNullOrEmpty(firstAssessManagerName))
            {
                result += empNumber + " " + empName + ";";
            }
        }
        if (!string.IsNullOrEmpty(result))
        {
            errMsg += "一階主管必需指定: " + result;
        }

        //考核人二階主管
        if (AssessmentLevel.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL2))
        {
            //二階評核天數
            if (string.IsNullOrEmpty(SecondAssessmentDays.ValueText))
            {
                errMsg += LblSecondAssessmentDays.Text + " 欄位必需填寫!\n";
            }

            result = "";
            for (int i = 0; i < row; i++)
            {
                DataObject data = assessManagerListSet.getAvailableDataObject(i);
                string empNumber = data.getData("empNumber");
                string empName = data.getData("empName");
                string secondAssessManagerName = data.getData("secondAssessManagerName");
                if (string.IsNullOrEmpty(secondAssessManagerName))
                {
                    result += empNumber + " " + empName + ";";
                }
            }

            if (!string.IsNullOrEmpty(result))
            {
                errMsg += "二階主管必需指定: " + result;
            }
        }

        //成積分配主管
        if (string.IsNullOrEmpty(AchievementDistWay.ValueText))
        {
            errMsg += LblAchievementDistWay.Text + " 欄位必需填寫!\n";
        }

        return errMsg;
    }

    /// <summary>
    /// 儲存資料至資料表
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmAssessmentPlanAgent");
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
    /// 考評對象-選擇部門
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSelectOrgUnit_Click(object sender, EventArgs e)
    {
        OpenWinAssessmentMembers.identityID = "0001";
        OpenWinAssessmentMembers.paramString = "id";
        OpenWinAssessmentMembers.openWin("OrgUnit", "003", true, "0001");
    }

    /// <summary>
    /// 選擇部門依公司別過濾
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSelectOrgUnit_BeforeClicks(object sender, EventArgs e)
    {
        string companyName = CompanyCode.ReadOnlyText;
        OpenWinAssessmentMembers.whereClause = "organizationName='" + companyName + "'";
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
                    if (ls[j, 0].Equals(values[i, 2])) //判確挑選的人員是否已經選過
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (!hasFound)
                {
                    string[] tag = new string[] { values[i, 1], values[i, 2] + "(" + values[i, 3] + ")" }; //挑選結果畫面顯示
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

    /// <summary>
    /// 依部門查詢部門員工, 並將結果顯示於部門員工清單中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbAssessmentMemberSearch_Click(object sender, EventArgs e)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string allDeptName = "查詢部門: ";
            string[,] ns = OrgUnitOID.getListItem();
            string assessmentPlanGUID = "AssessmentPlanGUID";
            DataObjectSet membersDraftSet = AssessmentMembersDraftList.dataSource;
            
            int length = ns.Length / 2;
            for (int i = 0; i < length; i++)
            {
                string nsDeptId = ns[i, 0];
                string nsDeptName = ns[i, 1];
                if (!string.IsNullOrEmpty(nsDeptId))
                {
                    string[][] groupUsers = SmpPmMaintainUtil.getOrgUnitUser(engine, nsDeptId);
                    for (int j = 0; j < groupUsers.Length; j++)
                    {
                        string userOID = groupUsers[j][0];
                        string userId = groupUsers[j][1];
                        string empName = groupUsers[j][2];
                        string deptOID = groupUsers[j][3];
                        string deptName = groupUsers[j][5];
                        string companyName = groupUsers[j][9];
                        string titleName = groupUsers[j][10];
                        string functionName = groupUsers[j][11];
                        string userManagerId = groupUsers[j][6];
                        string userManagerName = groupUsers[j][7];

                        if (string.IsNullOrEmpty(userManagerId))
                        {
                            string[] result = SmpPmMaintainUtil.getOrgUnitInfoById(engine, nsDeptId);
                            userManagerId = result[3];
                            userManagerName = result[4];
                        }

                        DataObject obj = membersDraftSet.create();
                        obj.setData("GUID", IDProcessor.getID(""));
                        obj.setData("IS_DISPLAY", "Y");
                        obj.setData("IS_LOCK", "N");
                        obj.setData("DATA_STATUS", "Y");
                        obj.setData("AssessmentPlanGUID", assessmentPlanGUID);
                        obj.setData("UserGUID", userOID);
                        obj.setData("orgName", companyName);
                        obj.setData("empNumber", userId);
                        obj.setData("empName", empName);
                        obj.setData("deptOID", deptOID);
                        obj.setData("deptId", nsDeptId);
                        obj.setData("deptName", deptName);
                        obj.setData("titleName", titleName);
                        obj.setData("functionName", functionName);
                        obj.setData("deptManagerId", userManagerId);
                        obj.setData("deptManagerName", userManagerName);
                        membersDraftSet.add(obj);
                    }
                    allDeptName += nsDeptName + ";";
                }
            }
            int row = AssessmentMembersDraftList.dataSource.getAvailableDataObjectCount();
            AssessmentMembersDraftList.reSortCondition("部門名稱", DataObjectConstants.ASC);
            AssessmentMembersDraftList.updateTable();
            AssessmentMembersDraftList.Display = true;
            MessageBox(allDeptName);
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
    /// 顯示/刷新暫存部門員工清單
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbAssessmentMemberRefresh_Click(object sender, EventArgs e)
    {
        DataObjectSet membersDraftSet = AssessmentMembersDraftList.dataSource;
        AssessmentMembersDraftList.dataSource = membersDraftSet;
        AssessmentMembersDraftList.updateTable();
        AssessmentMembersDraftList.Display = true;
    }

    /// <summary>
    /// 執行匯入, 將暫存部門員工資料匯入考核對象
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbAssessmentMemberImport_Click(object sender, EventArgs e)
    {
        string errMsg = "";
        DataObjectSet sourceSet = AssessmentMembersDraftList.dataSource;
        DataObjectSet targetSet = AssessmentMembersList.dataSource;
        int row = sourceSet.getAvailableDataObjectCount();
        int into = 0;
        for (int i = 0; i < row; i++)
        {
            DataObject source = sourceSet.getAvailableDataObject(i);
            DataObject target = targetSet.create();
            target.setData("GUID", IDProcessor.getID(""));
            target.setData("IS_DISPLAY", "Y");
            target.setData("IS_LOCK", "N");
            target.setData("DATA_STATUS", "Y");
            target.setData("AssessmentPlanGUID", source.getData("AssessmentPlanGUID"));
            target.setData("UserGUID", source.getData("UserGUID"));
            target.setData("orgName", source.getData("orgName"));
            target.setData("empNumber", source.getData("empNumber"));
            target.setData("empName", source.getData("empName"));
            target.setData("deptOID", source.getData("deptOID"));
            target.setData("deptId", source.getData("deptId"));
            target.setData("deptName", source.getData("deptName"));
            target.setData("titleName", source.getData("titleName"));
            target.setData("functionName", source.getData("functionName"));
            target.setData("deptManagerId", source.getData("deptManagerId"));
            target.setData("deptManagerName", source.getData("deptManagerName"));

            string[] keys = target.keyField;
            target.keyField = new string[] { "AssessmentPlanGUID", "UserGUID" };
            if (!targetSet.checkData(target))
            {
                errMsg += target.getData("empName") + ";";
                target.keyField = keys;
            }
            else
            {
                into++;
                targetSet.add(target);
            }
        }

        if (!string.IsNullOrEmpty(errMsg))
        {
            errMsg = "資料重複: " + errMsg;
            MessageBox(errMsg);
        }
        MessageBox("匯入" + into + "筆!");
        AssessmentMembersList.PageSize = 50;
        AssessmentMembersList.dataSource = targetSet;
        AssessmentMembersList.updateTable();

        //清空暫存查詢結果
        sourceSet.removeAll();
        AssessmentMembersDraftList.dataSource = sourceSet;
        AssessmentMembersDraftList.updateTable();
    }

    /// <summary>
    /// 依設定自動產生考核人
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbGenerateAssessmentManager_Click(object sender, EventArgs e)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            
            DataObjectSet sourceSet = AssessmentMembersList.dataSource;
            DataObjectSet targetSet = AssessmentManagerList.dataSource;
            //targetSet.removeAll();
            while(true) {
                int count = targetSet.getAvailableDataObjectCount();
                if (count == 0)
                {
                    break;
                }
                else
                {
                    DataObject data = targetSet.getAvailableDataObject(0);
                    data.delete();
                }
            }
            AssessmentManagerList.dataSource = targetSet;
            AssessmentManagerList.updateTable();

            DataObject objects = (DataObject)getSession("objects");
            string errMsg = checkFieldData(objects, engine);
            if (string.IsNullOrEmpty(errMsg))
            {
                string companyCode = CompanyCode.ValueText;
                int row = sourceSet.getAvailableDataObjectCount();
                for (int i = 0; i < row; i++)
                {
                    DataObject source = sourceSet.getAvailableDataObject(i);
                    DataObject target = targetSet.create();
                    string assessmentPlanGUID = source.getData("AssessmentPlanGUID");
                    string empNumber = source.getData("empNumber");
                    string empName = source.getData("empName");
                    string userGUID = source.getData("UserGUID");
                    string orgName = source.getData("orgName");
                    string deptId = source.getData("deptId");
                    string deptOID = source.getData("deptOID");
                    string deptName = source.getData("deptName");
                    string titleName = source.getData("titleName");
                    string functionName = source.getData("functionName");

                    target.setData("GUID", IDProcessor.getID(""));
                    target.setData("IS_DISPLAY", "Y");
                    target.setData("IS_LOCK", "N");
                    target.setData("DATA_STATUS", "Y");
                    target.setData("AssessmentPlanGUID", assessmentPlanGUID);
                    target.setData("UserGUID", userGUID);
                    target.setData("orgName", orgName);
                    target.setData("empNumber", empNumber);
                    target.setData("empName", empName);
                    target.setData("deptOID", deptOID);
                    target.setData("deptId", deptId);
                    target.setData("deptName", deptName);
                    target.setData("titleName", titleName);
                    target.setData("functionName", functionName);
                    //MessageBox(i + ": " + assessmentPlanGUID + "^" + userGUID + "^" + orgName + "^" + empNumber + "^" + empName + "^" + deptOID + "^" + deptId + "^" + deptName + "^" + titleName + "^" + functionName);
                    //設定自評人員
                    if (SelfEvaluation.ValueText.Equals("Y"))
                    {
                        target.setData("SelfEvaluateUserGUID", userGUID);
                        target.setData("selfEvaluateUserName", empName);
                        string companyUserGUID = companyCode + empNumber;
                        if (userGUID.StartsWith(companyUserGUID)) //此人員沒有系統登入帳號
                        {
                            target.setData("SelfEvaluateUserGUID", "");
                            target.setData("selfEvaluateUserName", "");
                        }
                    }
                    else
                    {
                        target.setData("SelfEvaluateUserGUID", "");
                        target.setData("selfEvaluateUserName", "");
                    }

                    if (AssessmentCategory.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_CATEGORY_ORG_UNIT))
                    {
                        //設定一階評核主管
                        string firstAssessUserId = source.getData("deptManagerId");
                        string[] result = SmpPmMaintainUtil.getUserInfoById(engine, firstAssessUserId);
                        target.setData("FirstAssessUserGUID", result[0]);
                        target.setData("firstAssessManagerName", result[1]);
                        //考評對象為部門主管
                        string deptManagerId = result[9];
                        if (empNumber.Equals(deptManagerId))
                        {
                            //一階評核人員為上階部門主管
                            result = SmpPmMaintainUtil.getSuperOrgUnitInfo(engine, deptOID);
                            //將上階部門換掉
                            deptOID = result[5];
                            result = SmpPmMaintainUtil.getSuperOrgUnitInfo(engine, deptOID);
                            target.setData("FirstAssessUserGUID", result[2]);
                            target.setData("firstAssessManagerName", result[4]);
                        }

                        if (AssessmentLevel.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL2))
                        {
                            //設定二階評核主管
                            result = SmpPmMaintainUtil.getSuperOrgUnitInfo(engine, deptOID);
                            target.setData("SecondAssessUserGUID", result[2]);
                            target.setData("secondAssessManagerName", result[4]);
                        }
                    }
                    else if (AssessmentCategory.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_CATEGORY_USER_DEFINE))
                    {
                        string[] result = SmpPmMaintainUtil.getUserAssessUser(engine, userGUID);
                        //設定一階評核主管
                        target.setData("FirstAssessUserGUID", result[0]);
                        target.setData("firstAssessManagerName", result[1]);
                        //設定二階評核主管
                        if (AssessmentLevel.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL2))
                        {
                            target.setData("FirstAssessUserGUID", result[3]);
                            target.setData("firstAssessManagerName", result[4]);
                        }
                    }

                    targetSet.add(target);
                }

                AssessmentManagerList.PageSize = 50;
                AssessmentManagerList.updateTable();
                MessageBox("成功產生考核人!");
            }
            else
            {
                MessageBox(errMsg);
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
    /// 送出考核計畫
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbSendAssessmentPlan_Click(object sender, EventArgs e)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            DataObject objects = (DataObject)getSession("objects");
            string errMsg = checkFieldData(objects, engine);
            if (string.IsNullOrEmpty(errMsg))
            {
                Status.ValueText = SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN;
                objects.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);

                GbSendAssessmentPlan.ReadOnly = true;
                string dateTimeNow = DateTimeUtility.getSystemTime2(null);
                StartDate.ValueText = dateTimeNow;
                //儲存計畫
                SaveButton_Click(sender, e);
                //產生人員考核關卡資料
                createUserAssessmentStage(objects);
                //產生人員考核分數單頭
                createAssessmentUserScore(objects);
                //產生人員總結分數/等級
                createUserAchievement(objects);
            }
            else
            {
                MessageBox(errMsg);
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
    /// 考核計畫唯讀
    /// </summary>
    protected void disableAssessmentPlan()
    {
        //計劃表單
        GbSendAssessmentPlan.ReadOnly = true;
        CompanyCode.ReadOnly = true;
        AssessYear.ReadOnly = true;
        AssessStartDate.ReadOnly = true;
        AssessEndDate.ReadOnly = true;
        PlanEndDate.ReadOnly = true;
        AssessmentName.ReadOnly = true;
        EvaluationGUID.ReadOnly = true;
        Remark.ReadOnly = true;
        //考評設定
        SelfEvaluation.ReadOnly = true;
        AssessmentCategory.ReadOnly = true;
        AssessmentLevel.ReadOnly = true;
        AchievementDistWay.ReadOnly = true;
        SelfEvaluationDays.ReadOnly = true;
        FirstAssessmentDays.ReadOnly = true;
        SecondAssessmentDays.ReadOnly = true;
        //考評對象
        GbAssessmentMemberSearch.ReadOnly = true;
        GbAssessmentMemberRefresh.ReadOnly = true;
        GbAssessmentMemberImport.ReadOnly = true;
        AssessmentMembersDraftList.ReadOnly = true;
        AssessmentMembersList.ReadOnly = true;
        //考核人
        GbGenerateAssessmentManager.ReadOnly = true;
        AssessmentManagerList.ReadOnly = true;
    }

    /// <summary>
    /// 產生人員考核關卡資料
    /// </summary>
    protected void createUserAssessmentStage(DataObject objects)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        string errMsg = "";
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            //string assessmentPlanGUID = (string)getSession("AssessmentPlanGUID", "AssessmentPlanGUID");
            string assessmentPlanGUID = objects.getData("GUID");
            string evaluationGUID = EvaluationGUID.ValueText;
            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            DataObjectSet sourceSet = AssessmentManagerList.dataSource;
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAssessmentStageAgent");
            agent.engine = engine;
            agent.query("1=2");
            DataObjectSet targetSet = agent.defaultData;
            int row = sourceSet.getAvailableDataObjectCount();
            for (int i = 0; i < row; i++)
            {
                DataObject source = sourceSet.getAvailableDataObject(i);
                string assessmentManagerGUID = source.getData("GUID");
                //string assessmentPlanGUID = source.getData("AssessmentPlanGUID");
                string userGUID = source.getData("UserGUID");
                string selfEvaluateUserGUID = source.getData("SelfEvaluateUserGUID");
                if (string.IsNullOrEmpty(selfEvaluateUserGUID))
                {
                    selfEvaluateUserGUID = userGUID;
                }
                string firstAssessUserGUID = source.getData("FirstAssessUserGUID");
                string secondAssessUserGUID = source.getData("SecondAssessUserGUID");
                string startDate = DateTimeUtility.getSystemTime2(null);

                //員工自評
                if (!string.IsNullOrEmpty(selfEvaluateUserGUID))
                {
                    if (SelfEvaluation.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_YES))
                    {
                        DataObject target = targetSet.create();
                        target.setData("GUID", IDProcessor.getID(""));
                        target.setData("IS_DISPLAY", "Y");
                        target.setData("IS_LOCK", "N");
                        target.setData("DATA_STATUS", "Y");

                        target.setData("AssessmentManagerGUID", assessmentManagerGUID);
                        target.setData("AssessmentPlanGUID", assessmentPlanGUID);
                        target.setData("EvaluationGUID", evaluationGUID);
                        target.setData("UserGUID", userGUID);
                        target.setData("AssessUserGUID", selfEvaluateUserGUID);
                        target.setData("Stage", SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION);
                        target.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);
                        target.setData("StartDate", startDate);
                        target.setData("AssessmentDays", SelfEvaluationDays.ValueText);
                        targetSet.add(target);
                    }
                }

                //一階主管評核
                if (!string.IsNullOrEmpty(firstAssessUserGUID))
                {
                    DataObject target = targetSet.create();
                    target.setData("GUID", IDProcessor.getID(""));
                    target.setData("IS_DISPLAY", "Y");
                    target.setData("IS_LOCK", "N");
                    target.setData("DATA_STATUS", "Y");

                    target.setData("AssessmentManagerGUID", assessmentManagerGUID);
                    target.setData("AssessmentPlanGUID", assessmentPlanGUID);
                    target.setData("EvaluationGUID", evaluationGUID);
                    target.setData("UserGUID", userGUID);
                    target.setData("AssessUserGUID", firstAssessUserGUID);
                    target.setData("Stage", SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS);
                    target.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_DRAFT);
                    target.setData("AssessmentDays", FirstAssessmentDays.ValueText);

                    if (string.IsNullOrEmpty(selfEvaluateUserGUID))
                    {
                        target.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);
                        target.setData("StartDate", startDate);
                    }
                    targetSet.add(target);
                }

                //二階主管評核
                if (!string.IsNullOrEmpty(secondAssessUserGUID))
                {
                    if (AssessmentLevel.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
                    {
                        DataObject target = targetSet.create();
                        target.setData("GUID", IDProcessor.getID(""));
                        target.setData("IS_DISPLAY", "Y");
                        target.setData("IS_LOCK", "N");
                        target.setData("DATA_STATUS", "Y");

                        target.setData("AssessmentManagerGUID", assessmentManagerGUID);
                        target.setData("AssessmentPlanGUID", assessmentPlanGUID);
                        target.setData("EvaluationGUID", evaluationGUID);
                        target.setData("UserGUID", userGUID);
                        target.setData("AssessUserGUID", secondAssessUserGUID);
                        target.setData("Stage", SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS);
                        target.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_DRAFT);
                        target.setData("AssessmentDays", SecondAssessmentDays.ValueText);
                        targetSet.add(target);
                    }
                }
            }

            if (!agent.update())
            {
                errMsg += engine.errorString;
            }

            if (errMsg.Equals(""))
            {

            }
            else
            {
                MessageBox(errMsg);
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
    /// 產生考核對象考評分數單頭
    /// </summary>
    protected void createAssessmentUserScore(DataObject objects)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        string errMsg = "";
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            //string assessmentPlanGUID = (string)getSession("AssessmentPlanGUID", "AssessmentPlanGUID");
            string assessmentPlanGUID = objects.getData("GUID");
            string evaluationGUID = EvaluationGUID.ValueText;
            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            DataObjectSet sourceSet = AssessmentManagerList.dataSource;
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmAssessmentUserScoreAgent");
            agent.engine = engine;
            agent.query("1=2");
            DataObjectSet targetSet = agent.defaultData;
            int row = sourceSet.getAvailableDataObjectCount();
            for (int i = 0; i < row; i++)
            {
                DataObject source = sourceSet.getAvailableDataObject(i);
                string assessmentManagerGUID = source.getData("GUID");
                string userGUID = source.getData("UserGUID");
                string selfEvaluateUserGUID = source.getData("SelfEvaluateUserGUID");
                string firstAssessUserGUID = source.getData("FirstAssessUserGUID");
                string secondAssessUserGUID = source.getData("SecondAssessUserGUID");
                string startDate = DateTimeUtility.getSystemTime2(null);

                DataObject target = targetSet.create();
                target.setData("GUID", IDProcessor.getID(""));
                target.setData("IS_DISPLAY", "Y");
                target.setData("IS_LOCK", "N");
                target.setData("DATA_STATUS", "Y");

                target.setData("UserGUID", userGUID);
                target.setData("AssessmentPlanGUID", assessmentPlanGUID);
                target.setData("AssessmentManagerGUID", assessmentManagerGUID);
                target.setData("EvaluationGUID", evaluationGUID);

                targetSet.add(target);
            }

            if (!agent.update())
            {
                errMsg += engine.errorString;
            }

            if (errMsg.Equals(""))
            {
                sendAssessment(assessmentPlanGUID);
            }
            else
            {
                MessageBox(errMsg);
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
    /// 產生考核對象成績(未啟動)
    /// </summary>
    protected void createUserAchievement(DataObject objects)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;
        string errMsg = "";
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            //string assessmentPlanGUID = (string)getSession("AssessmentPlanGUID", "AssessmentPlanGUID");
            string assessmentPlanGUID = objects.getData("GUID");
            string evaluationGUID = EvaluationGUID.ValueText;
            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            DataObjectSet sourceSet = AssessmentManagerList.dataSource;
            NLAgent agent = new NLAgent();
            agent.loadSchema("WebServerProject.maintain.SPPM003.SmpPmUserAchievementAgent");
            agent.engine = engine;
            agent.query("1=2");
            DataObjectSet targetSet = agent.defaultData;
            int row = sourceSet.getAvailableDataObjectCount();
            for (int i = 0; i < row; i++)
            {
                DataObject source = sourceSet.getAvailableDataObject(i);
                string assessmentManagerGUID = source.getData("GUID");
                string userGUID = source.getData("UserGUID");
                string firstAssessUserGUID = source.getData("FirstAssessUserGUID");
                string secondAssessUserGUID = source.getData("SecondAssessUserGUID");
                string assessUserGUID = firstAssessUserGUID;
                string startDate = DateTimeUtility.getSystemTime2(null);

                if (AssessmentLevel.ValueText.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL2)) 
                {
                    assessUserGUID = secondAssessUserGUID;
                }

                DataObject target = targetSet.create();
                target.setData("GUID", IDProcessor.getID(""));
                target.setData("IS_DISPLAY", "Y");
                target.setData("IS_LOCK", "N");
                target.setData("DATA_STATUS", "Y");

                target.setData("UserGUID", userGUID);
                target.setData("AssessmentPlanGUID", assessmentPlanGUID);
                target.setData("AssessmentManagerGUID", assessmentManagerGUID);
                target.setData("EvaluationGUID", evaluationGUID);
                target.setData("AssessUserGUID", assessUserGUID);
                target.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_DRAFT);

                targetSet.add(target);
            }

            if (!agent.update())
            {
                errMsg += engine.errorString;
            }

            if (errMsg.Equals(""))
            {

            }
            else
            {
                MessageBox(errMsg);
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
    /// 送出考核通知信件
    /// </summary>
    /// <param name="assessmentPlanGUID"></param>
    /// <param name="selfEvaluation"></param>
    protected void sendAssessment(string assessmentPlanGUID)
    {
        IOFactory factory = null;
        AbstractEngine engine = null;

        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            string sql = "";
            string subject = "";
            string assessmentName = "";
            string selfEvaluation = "";
            string assessmentDays = "";
            string selfEvaluationDays = "";
            string firstAssessmentDays = "";
            string secondAssessmentDays = "";
            string uri = "";
            DataSet ds = null;

            factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            sql = "select a.AssessmentName, b.SelfEvaluation, b.SelfEvaluationDays, b.FirstAssessmentDays, b.SecondAssessmentDays from SmpPmAssessmentPlan a, SmpPmAssessmentConfig b where a.GUID='" + assessmentPlanGUID + "' and a.GUID=b.AssessmentPlanGUID";
            
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                assessmentName = ds.Tables[0].Rows[0]["AssessmentName"].ToString();
                selfEvaluation = ds.Tables[0].Rows[0]["SelfEvaluation"].ToString(); 
                selfEvaluationDays = ds.Tables[0].Rows[0]["SelfEvaluationDays"].ToString();
                firstAssessmentDays = ds.Tables[0].Rows[0]["FirstAssessmentDays"].ToString();
                secondAssessmentDays = ds.Tables[0].Rows[0]["SecondAssessmentDays"].ToString();
            }
            
            if (selfEvaluation.Equals(SmpPmMaintainUtil.ASSESSMENT_YES))
            {
                subject = "[員工自評]" + assessmentName;
                assessmentDays = selfEvaluationDays;
                uri = "?runMethod=executePmProgram&programID=SPPM004M&ObjectGUID=";
                sql = "select b.mailAddress, b.userName, c.empName, a.GUID from SmpPmUserAssessmentStage a, Users b, SmpHrEmployeeInfoV c where a.AssessmentPlanGUID='" + assessmentPlanGUID + "' and a.Stage='" + SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION + "' and a.AssessUserGUID = b.OID and a.UserGUID=c.empGUID";
            }
            else
            {
                subject = "[一階評核]" + assessmentName;
                assessmentDays = firstAssessmentDays;
                uri = "?runMethod=executePmProgram&programID=SPPM005M&ObjectGUID=";
                sql = "select b.mailAddress, b.userName, c.empName, a.GUID from SmpPmUserAssessmentStage a, Users b, SmpHrEmployeeInfoV c where a.AssessmentPlanGUID='" + assessmentPlanGUID + "' and a.Stage='" + SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS + "' and a.AssessUserGUID = b.OID and a.UserGUID=c.empGUID";
            }
            
            ds = engine.getDataSet(sql, "TEMP");
            int row = ds.Tables[0].Rows.Count;
            string[,] values = new string[row,4];
            for (int i = 0; i < row; i++)
            {
                values[i, 0] = ds.Tables[0].Rows[i]["mailAddress"].ToString();
                values[i, 1] = ds.Tables[0].Rows[i]["userName"].ToString();
                values[i, 2] = ds.Tables[0].Rows[i]["empName"].ToString();
                values[i, 3] = ds.Tables[0].Rows[i]["GUID"].ToString();
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
                string href = emailHeader + uri + objectGUID;
                content += "考評名稱: " + assessmentName + "<br />";
                content += "評核截止天數: " + assessmentDays + " 天<br />";
                content += "考核人: " + assessUserName + " <br />";
                content += "考核對象: " + userName + " <br />";
                content += "待辦事項: <a href='" + href + "'>您有一個待辦事項,請按這裡連結至您的工作</a><br />";
                
                au.sendMailHTML("", smtpServer, mailAddress, "", systemMail, subject, content);
            }
            MessageBox("成功發送考核計劃, 人數: " + row);
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
    /// 送出考評通知信件(測試)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GbReSendAssessment_Click(object sender, EventArgs e)
    {
        DataObject objects = (DataObject)getSession("Objects");
        string assessmentPlanGUID = objects.getData("GUID");
        sendAssessment(assessmentPlanGUID);
    }
}
