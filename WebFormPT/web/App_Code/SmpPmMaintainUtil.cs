using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebServerProject;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.mail;
using com.dsc.kernal.security;
using com.dsc.kernal.utility;

namespace smp.pms.utility
{
    /// <summary>
    /// BasicFormPage 的摘要描述
    /// </summary>
    public class SmpPmMaintainUtil
    {
        public const string ASSESSMENT_STATUS_DRAFT = "DRAFT";
        public const string ASSESSMENT_STATUS_OPEN = "OPEN";
        public const string ASSESSMENT_STATUS_COMPLETE = "COMPLETE";
        public const string ASSESSMENT_STATUS_CLOSE = "CLOSE";
        public const string ASSESSMENT_STATUS_CANCEL = "CANCEL";
        public const string ASSESSMENT_CATEGORY_ORG_UNIT = "0";
        public const string ASSESSMENT_CATEGORY_USER_DEFINE = "1";
        public const string ASSESSMENT_LEVEL_LEVEL1 = "1";
        public const string ASSESSMENT_LEVEL_LEVEL2 = "2";
        public const string ASSESSMENT_LEVEL_LEVEL0 = "0";
        public const string ACHIEVEMENT_DIST_WAY_LAST_ASSESS = "0";
        public const string ACHIEVEMENT_DIST_WAY_USER_DEFINE = "1";
        public const string ACHIEVEMENT_MEMBER_TYPE_GROUP = "0";
        public const string ACHIEVEMENT_MEMBER_TYPE_USER = "1";
        public const string ASSESSMENT_YES = "Y";
        public const string ASSESSMENT_NO = "N";
        public const string ASSESSMENT_STAGE_SELF_EVALUATION = "0";
        public const string ASSESSMENT_STAGE_FIRST_ASSESS = "1";
        public const string ASSESSMENT_STAGE_SECOND_ASSESS = "2";

        /// <summary>
        /// 查詢使用者之相關資料,包含職位,直屬主管,所屬部門!
        /// [0]:OID, [1]:姓名, [2]: AD帳號, [3]:職稱, [4]:email, [5]:公司別, [6]: 部門代號, [7]: 部門名稱, [8] 公司別名稱, [9] 部門主管工號, [10] 部門主管姓名, [11] 身份別
        /// </summary>
        /// <param name="engine"></param> default engine
        /// <param name="userGUID"></param> User GUID
        /// <returns>string[]</returns> 僅回傳一筆
        public static string[] getUserInfoById(AbstractEngine engine, string userId)
        {
            string sql = "select empGUID, empName, empEName, titleName, empEmail, orgId, deptId, deptName, orgName, deptManagerId, deptManagerName, functionName,empNumber from SmpHrEmployeeInfoV where empNumber='" + Utility.filter(userId) + "' ";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            string[] result = new string[13];
            if (ds.Tables[0].Rows.Count > 0)
            {
                result[0] = ds.Tables[0].Rows[0][0].ToString();
                result[1] = ds.Tables[0].Rows[0][1].ToString();
                result[2] = ds.Tables[0].Rows[0][2].ToString();
                result[3] = ds.Tables[0].Rows[0][3].ToString();
                result[4] = ds.Tables[0].Rows[0][4].ToString();
                result[5] = ds.Tables[0].Rows[0][5].ToString();
                result[6] = ds.Tables[0].Rows[0][6].ToString();
                result[7] = ds.Tables[0].Rows[0][7].ToString();
                result[8] = ds.Tables[0].Rows[0][8].ToString();
                result[9] = ds.Tables[0].Rows[0][9].ToString();
                result[10] = ds.Tables[0].Rows[0][10].ToString();
                result[11] = ds.Tables[0].Rows[0][11].ToString();
                result[12] = ds.Tables[0].Rows[0][12].ToString();
            }
            else
            {
                result[0] = "";
                result[1] = "";
                result[2] = "";
                result[3] = "";
                result[4] = "";
                result[5] = "";
                result[6] = "";
                result[7] = "";
                result[8] = "";
                result[9] = "";
                result[10] = "";
                result[11] = "";
                result[12] = "";
            }
            return result;
        }

        /// <summary>
        /// 取得部門人員, [][]: [][0]帳號識別碼, [][1]工號, [][2]姓名, [][3]部門識別碼, [][4]部門代號, [][5]部門名稱, [][6]部門主管工號, [][7]部門主管名稱, [][8]公司別, [][9]公司別名稱, [][10]職稱, [][11]身份別
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="groupName"></param>
        /// <returns>string[][]</returns>
        public static string[][] getOrgUnitUser(AbstractEngine engine, string deptId)
        {
            string sql = "select empGUID, empNumber, empName, deptOID, deptId, deptName, deptManagerId, deptManagerName, orgId, orgName, titleName, functionName, OnBoardDate from SmpHrEmployeeInfoV where deptId='" + Utility.filter(deptId) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; i++)
            {
                result[i] = new string[13];
                result[i][0] = ds.Tables[0].Rows[i][0].ToString();
                result[i][1] = ds.Tables[0].Rows[i][1].ToString();
                result[i][2] = ds.Tables[0].Rows[i][2].ToString();
                result[i][3] = ds.Tables[0].Rows[i][3].ToString();
                result[i][4] = ds.Tables[0].Rows[i][4].ToString();
                result[i][5] = ds.Tables[0].Rows[i][5].ToString();
                result[i][6] = ds.Tables[0].Rows[i][6].ToString();
                result[i][7] = ds.Tables[0].Rows[i][7].ToString();
                result[i][8] = ds.Tables[0].Rows[i][8].ToString();
                result[i][9] = ds.Tables[0].Rows[i][9].ToString();
                result[i][10] = ds.Tables[0].Rows[i][10].ToString();
                result[i][11] = ds.Tables[0].Rows[i][11].ToString();
                result[i][12] = ds.Tables[0].Rows[i][12].ToString();
            }

            return result;
        }
        /// <summary>
        /// 取得部門人員, [][]: [][0]帳號識別碼, [][1]工號, [][2]姓名, [][3]部門識別碼, [][4]部門代號, [][5]部門名稱, [][6]部門主管工號, [][7]部門主管名稱, [][8]公司別, [][9]公司別名稱, [][10]職稱, [][11]身份別
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="groupName"></param>
        /// <returns>string[][]</returns>
        public static string[][] getOrgUnitUser(AbstractEngine engine, string deptId,string FormName)
        {
            string sql = "select empGUID, empNumber, empName, deptOID, deptId, deptName, deptManagerId, deptManagerName, orgId, orgName, titleName, functionName, OnBoardDate from SmpHrEmployeeInfoV where 1=1 ";
            if (!deptId.Equals(""))
            {
                sql += " and deptId='" + Utility.filter(deptId) + "'";
            }
            if (!FormName.Equals(""))
            {
                sql += " and FormName='"+FormName+"'";
            }
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; i++)
            {
                result[i] = new string[13];
                result[i][0] = ds.Tables[0].Rows[i][0].ToString();
                result[i][1] = ds.Tables[0].Rows[i][1].ToString();
                result[i][2] = ds.Tables[0].Rows[i][2].ToString();
                result[i][3] = ds.Tables[0].Rows[i][3].ToString();
                result[i][4] = ds.Tables[0].Rows[i][4].ToString();
                result[i][5] = ds.Tables[0].Rows[i][5].ToString();
                result[i][6] = ds.Tables[0].Rows[i][6].ToString();
                result[i][7] = ds.Tables[0].Rows[i][7].ToString();
                result[i][8] = ds.Tables[0].Rows[i][8].ToString();
                result[i][9] = ds.Tables[0].Rows[i][9].ToString();
                result[i][10] = ds.Tables[0].Rows[i][10].ToString();
                result[i][11] = ds.Tables[0].Rows[i][11].ToString();
                result[i][12] = ds.Tables[0].Rows[i][12].ToString();
            }

            return result;
        }
        /// <summary>
        /// 取得使用者OID, [0]: OID, [1]: userName
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="userId"></param>
        /// <returns>string[]</returns>
        public static string[] getUserGUID(AbstractEngine engine, string userId)
        {
            string sql = "select OID, userName from Users where id='" + Utility.filter(userId) + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            string[] result = new string[2];
            if (ds.Tables[0].Rows.Count > 0)
            {
                result[0] = ds.Tables[0].Rows[0][0].ToString();
                result[1] = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                result[0] = "";
                result[1] = "";
            }

            return result;
        }
                
        /// <summary>
        /// 取得部門主管人員資訊
        /// [0]: 部門代號, [1]: 部門名稱, [2]: 部門主管識別號, [3]: 部門主管工號, [4]: 部門主管姓名
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="orgUnitGUID"></param>
        /// <returns></returns>
        public static string[] getOrgUnitInfo(AbstractEngine engine, string orgUnitGUID)
        {
            string sql = "select o.id, o.organizationUnitName, u.OID, u.id, u.userName from OrganizationUnit o, Users u where o.OID='" + Utility.filter(orgUnitGUID) + "' and o.managerOID = u.OID";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            string[] result = new string[5];
            if (ds.Tables[0].Rows.Count > 0)
            {
                result[0] = ds.Tables[0].Rows[0][0].ToString();
                result[1] = ds.Tables[0].Rows[0][1].ToString();
                result[2] = ds.Tables[0].Rows[0][2].ToString();
                result[3] = ds.Tables[0].Rows[0][3].ToString();
                result[4] = ds.Tables[0].Rows[0][4].ToString();
            }
            else
            {
                result[0] = "";
                result[1] = "";
                result[2] = "";
                result[3] = "";
                result[4] = "";
            }

            return result;
        }

        /// <summary>
        /// 取得部門主管人員資訊
        /// [0]: 部門識別碼, [1]: 部門名稱, [2]: 部門主管識別號, [3]: 部門主管工號, [4]: 部門主管姓名
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="orgUnitGUID"></param>
        /// <returns></returns>
        public static string[] getOrgUnitInfoById(AbstractEngine engine, string orgUnitId)
        {
            string sql = "select o.OID, o.organizationUnitName, u.OID, u.id, u.userName from OrganizationUnit o, Users u where o.id='" + Utility.filter(orgUnitId) + "' and o.managerOID = u.OID";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            string[] result = new string[5];
            if (ds.Tables[0].Rows.Count > 0)
            {
                result[0] = ds.Tables[0].Rows[0][0].ToString();
                result[1] = ds.Tables[0].Rows[0][1].ToString();
                result[2] = ds.Tables[0].Rows[0][2].ToString();
                result[3] = ds.Tables[0].Rows[0][3].ToString();
                result[4] = ds.Tables[0].Rows[0][4].ToString();
            }
            else
            {
                result[0] = "";
                result[1] = "";
                result[2] = "";
                result[3] = "";
                result[4] = "";
            }

            return result;
        }

        /// <summary>
        /// 取得上層部門主管人員資訊
        /// [0]: 部門代號, [1]: 部門名稱, [2]: 部門主管識別號, [3]: 部門主管工號, [4]: 部門主管姓名
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="orgUnitGUID"></param>
        /// <returns></returns>
        public static string[] getSuperOrgUnitInfo(AbstractEngine engine, string orgUnitGUID)
        {
            string sql = "select s.id, s.organizationUnitName, u.OID, u.id, u.userName, s.OID from OrganizationUnit o, Users u, OrganizationUnit s where o.OID='" + Utility.filter(orgUnitGUID) + "' and o.superUnitOID = s.OID and s.managerOID = u.OID";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            string[] result = new string[6];
            if (ds.Tables[0].Rows.Count > 0)
            {
                result[0] = ds.Tables[0].Rows[0][0].ToString();
                result[1] = ds.Tables[0].Rows[0][1].ToString();
                result[2] = ds.Tables[0].Rows[0][2].ToString();
                result[3] = ds.Tables[0].Rows[0][3].ToString();
                result[4] = ds.Tables[0].Rows[0][4].ToString();
                result[5] = ds.Tables[0].Rows[0][5].ToString();
            }
            else
            {
                result[0] = "";
                result[1] = "";
                result[2] = "";
                result[3] = "";
                result[4] = "";
                result[5] = "";
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="userGUID"></param>
        /// <returns></returns>
        public static string[] getUserAssessUser(AbstractEngine engine, string userGUID)
        {
            string sql = "select a.FirstAssessUserGUID, b.userName FirstAssessUserName, a.SecondAssessUserGUID, c.userName SecondAssessUserName, a.AchievementUserGUID, d.userName AchievementUserName from SmpPmUserAssessment a left join Users b on b.id=a.FirstAssessUserGUID left join Users c on c.id=a.SecondAssessUserGUID left join Users d on d.id=a.AchievementUserGUID where UserGUID='" + userGUID + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[] result = new string[6];
            if (ds.Tables[0].Rows.Count > 0)
            {
                result[0] = ds.Tables[0].Rows[0][0].ToString();
                result[1] = ds.Tables[0].Rows[0][1].ToString();
                result[2] = ds.Tables[0].Rows[0][2].ToString();
                result[3] = ds.Tables[0].Rows[0][3].ToString();
                result[4] = ds.Tables[0].Rows[0][4].ToString();
                result[5] = ds.Tables[0].Rows[0][5].ToString();
            }
            else
            {
                result[0] = "";
                result[1] = "";
                result[2] = "";
                result[3] = "";
                result[4] = "";
                result[5] = "";
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="evaluationGUID"></param>
        /// <returns></returns>
        public static string[,] getCompanyIds(AbstractEngine engine)
        {
            string sql = "select CompanyCode, CompanyName from SmpTSCompanyV";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[,] ids = new string[1 + rows, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            for (int i = 0; i < rows; i++)
            {
                ids[i+1, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i+1, 1] = ds.Tables[0].Rows[i][1].ToString();
            }

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="evaluationGUID"></param>
        /// <returns></returns>
        public static string[,] getEvaluationIds(AbstractEngine engine)
        {
            string sql = "select GUID, Name from SmpPmEvaluation where Active = 'Y'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[,] ids = new string[1 + rows,2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            for (int i = 0; i < rows; i++)
            {
                ids[i+1, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i+1, 1] = ds.Tables[0].Rows[i][1].ToString();
            }

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAssessmentStatusIds(AbstractEngine engine)
        {
            string[,] ids = new string[6, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            ids[1, 0] = ASSESSMENT_STATUS_DRAFT;
            ids[1, 1] = "未開始";
            ids[2, 0] = ASSESSMENT_STATUS_OPEN;
            ids[2, 1] = "進行中";
            ids[3, 0] = ASSESSMENT_STATUS_COMPLETE;
            ids[3, 1] = "已完成";
            ids[4, 0] = ASSESSMENT_STATUS_CLOSE;
            ids[4, 1] = "已結案";
            ids[5, 0] = ASSESSMENT_STATUS_CANCEL;
            ids[5, 1] = "取消";

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getYesNoIds(AbstractEngine engine)
        {
            string[,] ids = new string[3, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            ids[1, 0] = ASSESSMENT_YES;
            ids[1, 1] = "是";
            ids[2, 0] = ASSESSMENT_NO;
            ids[2, 1] = "否";

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAssessmentCategoryIds(AbstractEngine engine)
        {
            string[,] ids = new string[3, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            ids[1, 0] = ASSESSMENT_CATEGORY_ORG_UNIT;
            ids[1, 1] = "部門組織";
            ids[2, 0] = ASSESSMENT_CATEGORY_USER_DEFINE;
            ids[2, 1] = "自定評核人員";

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAssessmentLevelIds(AbstractEngine engine)
        {
            string[,] ids = new string[4, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            ids[1, 0] = ASSESSMENT_LEVEL_LEVEL1;
            ids[1, 1] = "一階";
            ids[2, 0] = ASSESSMENT_LEVEL_LEVEL2;
            ids[2, 1] = "二階";
            ids[3, 0] = "0";
            ids[3, 1] = "不限";

            return ids;
        }

        public static string[,] getAssessmentStageIds(AbstractEngine engine)
        {
            string[,] ids = new string[3, 2];
            ids[0, 0] = ASSESSMENT_STAGE_SELF_EVALUATION;
            ids[0, 1] = "員工自評";
            ids[1, 0] = ASSESSMENT_STAGE_FIRST_ASSESS;
            ids[1, 1] = "一階主管";
            ids[2, 0] = ASSESSMENT_STAGE_SECOND_ASSESS;
            ids[2, 1] = "二階主管";

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAchievementDistWayIds(AbstractEngine engine)
        {
            string[,] ids = new string[3, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            ids[1, 0] = ACHIEVEMENT_DIST_WAY_LAST_ASSESS;
            ids[1, 1] = "最後評核人員";
            ids[2, 0] = ACHIEVEMENT_DIST_WAY_USER_DEFINE;
            ids[2, 1] = "自定評核人員";
            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAssessmentMemberTypeIds(AbstractEngine engine)
        {
            string[,] ids = new string[2, 2];
            ids[0, 0] = ACHIEVEMENT_MEMBER_TYPE_GROUP;
            ids[0, 1] = "部門";
            ids[1, 0] = ACHIEVEMENT_MEMBER_TYPE_USER;
            ids[1, 1] = "人員";

            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAchievementLevel(AbstractEngine engine)
        {
            string sql = "select AchievementLevel, Description from SmpPmAchievementLevel where Active = 'Y' order by AchievementLevel";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[,] ids = new string[1 + rows, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            for (int i = 0; i < rows; i++)
            {
                ids[i + 1, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i + 1, 1] = ds.Tables[0].Rows[i][1].ToString();
            }
            return ids;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAssessment(AbstractEngine engine)
        {
            string sql = "select GUID, AssessmentName from SmpPmAssessmentPlan where Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_DRAFT + "' order by D_INSERTTIME desc";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[,] ids = new string[1 + rows, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            for (int i = 0; i < rows; i++)
            {
                ids[i + 1, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i + 1, 1] = ds.Tables[0].Rows[i][1].ToString();
            }

            return ids;
        }
        /// <summary>
        /// 取得考核計劃名稱
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string[,] getAssessmentName(AbstractEngine engine)
        {
            string sql = "select distinct AssessmentName from SmpPmAssessmentPlan where Status <> '" + SmpPmMaintainUtil.ASSESSMENT_STATUS_DRAFT + "'";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            string[,] ids = new string[1 + rows, 2];
            ids[0, 0] = "";
            ids[0, 1] = "";
            for (int i = 0; i < rows; i++)
            {
                ids[i + 1, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i + 1, 1] = ds.Tables[0].Rows[i][0].ToString();
            }

            return ids;
        }
        /// <summary>
        /// 觸發下一階段評核
        /// </summary>
        /// <param name="assessmentManagerGUID"></param>
        /// <param name="stage"></param>
        public static string sendAssessmentToNextUser(DataObject objects)
        {
            string errMsg = "";
            AbstractEngine engine = null;
            try
            {
                string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
                string stage = objects.getData("Stage");
                string connectString = (string)HttpContext.Current.Session["connectString"];
                string engineType = (string)HttpContext.Current.Session["engineType"];
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);

                //更新下一關狀態/啟始日
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
                            errMsg += agent.engine.errorString;
                        }
                        errMsg += sendAssessment(data);
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
                        int score = 0;
                        int number = 0;
                        bool isNumeric = false;
                        string firstScore = objects.getData("FirstScore_W");
                        string secondScore = objects.getData("SecondScore_W");
                        isNumeric = int.TryParse(firstScore, out number);
                        if (isNumeric)
                        {
                            score = number;
                        }
                        isNumeric = int.TryParse(secondScore, out number);
                        if (isNumeric)
                        {
                            score = number;
                        }

                        DataObject data = set.getAvailableDataObject(0);
                        data.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN);
                        if (score > 0)
                        {
                            data.setData("FinalScore", score + "");
                            string achievementLevel = getAchievementLevel(engine, data);
                            data.setData("AchievementLevel", achievementLevel);
                            getEncodeValue(data, new string[] { "FinalScore" });
                        }
                        if (!agent.update())
                        {
                            errMsg += agent.engine.errorString;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errMsg += e.Message;
            }
            return errMsg;
        }

        /// <summary>
        /// 發送信件
        /// </summary>
        /// <param name="objects"></param>
        public static string sendAssessment(DataObject objects)
        {
            string errMsg = "";
            IOFactory factory = null;
            AbstractEngine engine = null;

            try
            {
                string connectString = (string)HttpContext.Current.Session["connectString"];
                string engineType = (string)HttpContext.Current.Session["engineType"];

                factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                string assessmentPlanGUID = "";
                string subject = "";
                string assessmentName = "";
                string firstAssessmentDays = "";
                string secondAssessmentDays = "";
                string assessmentDays = "";
                string assessmentStageGUID = objects.getData("GUID");
                //errMsg += assessmentStageGUID;
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

                sql = "select b.mailAddress, b.userName, c.empName, a.GUID from SmpPmUserAssessmentStage a, Users b, SmpHrEmployeeInfoV c where a.GUID='" + assessmentStageGUID + "' and a.AssessUserGUID = b.id and a.UserGUID=c.empNumber";
                ds = engine.getDataSet(sql, "TEMP");
                row = ds.Tables[0].Rows.Count;
                string[,] values = new string[row, 4];
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
                string systemMail = sp.getParam("SystemMail");//20170810 Marcia 需要添加EcpPmsMail發送郵件地址
                string emailHeader = sp.getParam("EmailHeader");
                MailFactory fac = new MailFactory();
                AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);
                //errMsg += "mailclass:{" + mailclass+"}";

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
                    //errMsg += "smtpServer{" + smtpServer + "}mailAddress{" + mailAddress + "systemMail{" + systemMail + "subject{" + subject + "}"; 
                    au.sendMailHTML("", smtpServer, mailAddress, "", systemMail, subject, content);
                }

            }
            catch (Exception ex)
            {
                errMsg += ex.Message;
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
            return errMsg;
        }

        /// <summary>
        /// 儲存評核階段狀態
        /// </summary>
        public static string saveAssessmentStage(DataObject objects)
        {
            string errMsg = "";
            AbstractEngine engine = null;
            try
            {
                string assessmentManagerGUID = objects.getData("AssessmentManagerGUID");
                string stage = objects.getData("Stage");
                string dateTimeNow = DateTimeUtility.getSystemTime2(null);
                objects.setData("Status", SmpPmMaintainUtil.ASSESSMENT_STATUS_COMPLETE);
                objects.setData("CompleteDate", dateTimeNow);
                string connectString = (string)HttpContext.Current.Session["connectString"];
                string engineType = (string)HttpContext.Current.Session["engineType"];
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
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
                        errMsg += agent.engine.errorString;
                    }
                }
                else
                {
                    errMsg += "找不到資料更新!";
                }
            }
            catch (Exception e)
            {
                errMsg += e.Message;
                //writeLog(e);
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
            return errMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static string isFinalStageAssessUser(DataObject objects, out bool finalStageComplete)
        {
            string errMsg = "";
            AbstractEngine engine = null;
            finalStageComplete = false;
            try
            {
                string connectString = (string)HttpContext.Current.Session["connectString"];
                string engineType = (string)HttpContext.Current.Session["engineType"];
                string userGUID = (string)HttpContext.Current.Session["UserGUID"];

                IOFactory factory = new IOFactory();
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
                else if (assessmentLevel.Equals(SmpPmMaintainUtil.ASSESSMENT_LEVEL_LEVEL0))
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
                        //message = "本階段所有考評對象均已評核完畢! 請進行成績分配!";
                        //MessageBox(message);
                        finalStageComplete = true;
                    }
                }
            }
            catch (Exception e)
            {
                errMsg += e.Message;
                //writeLog(e);
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }

            return errMsg;
        }

        /// <summary>
        /// 檢查成績
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static string checkScore(DataObject objects)
        {
            string errMsg = "";
            string result = "";
            //檢查所項目均必需有分數
            string stage = objects.getData("Stage");
            DataObjectSet scoreSet = objects.getChild("SmpPmAssessmentScoreDetail");
            int row = scoreSet.getAvailableDataObjectCount();
            for (int i = 0; i < row; i++)
            {
                DataObject data = scoreSet.getAvailableDataObject(i);
                string itemNo = data.getData("ItemNo");
                string selfScore = data.getData("SelfScore");
                string firstScore = data.getData("FirstScore");
                string secondScore = data.getData("SecondScore");

                if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SELF_EVALUATION))
                {
                    if (string.IsNullOrEmpty(selfScore))
                    {
                        result += itemNo + ";";
                    }
                }
                else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_FIRST_ASSESS))
                {
                    if (string.IsNullOrEmpty(firstScore))
                    {
                        result += itemNo + ";";
                    }
                }
                else if (stage.Equals(SmpPmMaintainUtil.ASSESSMENT_STAGE_SECOND_ASSESS))
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
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static string getAchievementLevel(AbstractEngine engine, DataObject objects) 
        {
            string achievementLevel = "";
            string finalScore = objects.getData("FinalScore");
            string sql = " select AchievementLevel, Description from SmpPmAchievementLevel " +
                             " where convert(INT, MixFraction) <= " + finalScore + " and convert(INT,MaxFraction) >" + finalScore +
                             " and Active = 'Y' ";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                achievementLevel = ds.Tables[0].Rows[i][0].ToString();
            }
            return achievementLevel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        public static void getDecodeValue(DataObjectSet set, string[] fields) 
        {
            for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
            {
                DataObject data = set.getAvailableDataObject(i);
                getDecodeValue(data, fields);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="set"></param>
        public static void getEncodeValue(DataObjectSet set, string[] fields)
        {
            for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
            {
                DataObject data = set.getAvailableDataObject(i);
                getEncodeValue(data, fields);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public static void getDecodeValue(DataObject data, string[] fields)
        {
            try
            {
                WebServerProject.SysParam sp = new WebServerProject.SysParam(null);
                for (int i = 0; i < fields.Length; i++)
                {
                    string fieldValue = data.getData(fields[i]);
                    int number = 0;
                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        bool isNumeric = int.TryParse(fieldValue, out number);
                        if (!isNumeric)
                        {
                            string decodeValue = sp.decode(fieldValue);
                            data.setData(fields[i], decodeValue);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public static void getEncodeValue(DataObject data, string[] fields)
        {
            try
            {
                WebServerProject.SysParam sp = new WebServerProject.SysParam(null);
                for (int i = 0; i < fields.Length; i++)
                {
                    string fieldValue = data.getData(fields[i]);
                    int number = 0;
                    float number2=0;
                    if (!string.IsNullOrEmpty(fieldValue))
                    {
                        bool isNumeric = int.TryParse(fieldValue, out number);
                        bool  isNumeric2 = float.TryParse(fieldValue, out number2);
                        if (isNumeric || isNumeric2)
                        {
                            string encodeValue = sp.encode(fieldValue);
                            data.setData(fields[i], encodeValue);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="userGUID"></param>
        /// <returns></returns>
        public static string[] getLastAchievementAssessmentPlan(AbstractEngine engine, string userGUID)
        {
            string sql = "select distinct b.GUID, b.AssessmentName, b.D_INSERTTIME from SmpPmUserAchievement a, SmpPmAssessmentPlan b "
                + "where a.AssessUserGUID = '"+userGUID+"' "
                + "and a.Status='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN + "' and a.AssessmentPlanGUID=b.GUID order by b.D_INSERTTIME desc";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int row = ds.Tables[0].Rows.Count;
            string[] values = new string[2];
            if (row > 0)
            {
                values[0] = ds.Tables[0].Rows[0][0].ToString();
                values[1] = ds.Tables[0].Rows[0][1].ToString();
            }
            else
            {
                values[0] = "";
                values[1] = "";
            }
            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="assessmentPlanGUID"></param>
        public static string mailAchievementAssessUser(string assessmentPlanGUID, string assessmentManagerGUID)
        {
            string errMsg = "";
            AbstractEngine engine = null;
            try
            {
                string connectString = (string)HttpContext.Current.Session["connectString"];
                string engineType = (string)HttpContext.Current.Session["engineType"];
                
                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);

                string sql = "select distinct c.mailAddress,c.userName,b.AssessmentName,b.GUID "
                    + "from SmpPmUserAchievement a, SmpPmAssessmentPlan b, Users c "
                    + "where a.AssessmentPlanGUID='" + assessmentPlanGUID + "' "
                    + "and a.Status='" + SmpPmMaintainUtil.ASSESSMENT_STATUS_OPEN + "' "
                    + "and a.AssessUserGUID=(select m.AchievementUserGUID from SmpPmAssessmentManager m where m.GUID='" + assessmentManagerGUID + "') "
                    + "and a.AssessmentPlanGUID=b.GUID "
                    + "and a.AssessUserGUID=c.id";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                int row = ds.Tables[0].Rows.Count;
                string[,] values = new string[row, 4];
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
                    string assessmentName = values[i, 2];
                    string objectGUID = values[i, 3];
                    string subject = "[成績分配] " + assessmentName;
                    string content = "";
                    string href = emailHeader + "?runMethod=executePmProgram&programID=SPPM006M&ObjectGUID=" + objectGUID;
                    content += "考評名稱: " + assessmentName + "<br />";
                    content += "考核人: " + assessUserName + " <br />";
                    content += "待辦事項: <a href='" + href + "'>您有一個待辦事項,請按這裡連結至您的工作</a><br />";

                    au.sendMailHTML("", smtpServer, mailAddress, "", systemMail, subject, content);
                }
            }
            catch (Exception e)
            {
                errMsg = "通知成績分配人員時發生錯誤: " + e.Message;
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
            return errMsg;
        }

      public static bool GetSubmitStatus(string AssessmentPlanGUID, string AssessUserGUID)
        { 
            bool isTrue =false;
            AbstractEngine engine = null;
            try
            {
                string connectString = (string)HttpContext.Current.Session["connectString"];
                string engineType = (string)HttpContext.Current.Session["engineType"];

                IOFactory factory = new IOFactory();
                engine = factory.getEngine(engineType, connectString);
                string sqltxt = "select distinct SubmitStatus from dbo.SmpPmUserAchievement where  AssessmentPlanGUID in(select GUID from dbo.SmpPmAssessmentPlan where AssessmentName='" + AssessmentPlanGUID + "') and AssessUserGUID='" + AssessUserGUID + "'";
                object k = engine.executeScalar(sqltxt);
                if (k != null)
                {
                    if (k.ToString().Equals("Y") || k.ToString().Equals("A"))
                    {
                        isTrue = true;
                    }
                    else
                    {
                        isTrue = false;
                    }
                }
                else
                {
                    isTrue = false;
                }

            }
            catch
            {
                isTrue = false;
            }
            finally
            {
                if (engine != null)
                {
                    engine.close();
                }
            }
            return isTrue;
        }
       public static string SendSupviorNotice(string AssessmentPlanName, string AssessUserGUID)
        {
            string errMsg = "";
            string connectString = (string)HttpContext.Current.Session["connectString"];
            string engineType = (string)HttpContext.Current.Session["engineType"];

            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            SysParam sp = new SysParam(engine);
            string mailclass = sp.getParam("MailClass");
            string smtpServer = sp.getParam("SMTP_Server");
            string systemMail = sp.getParam("SystemMail");//20170810 Marcia 需要添加EcpPmsMail發送郵件地址
            string emailHeader = sp.getParam("EmailHeader");
            MailFactory fac = new MailFactory();
            AbstractMailUtility au = fac.getMailUtility(mailclass.Split(new char[] { '.' })[0], mailclass);

            string sqltxt = "select top 1 c.GUID,c.AssessmentName,b.mailAddress from dbo.SmpPmUserAchievement as a left join dbo.Users as b on a.AssessUserGUID=b.id left join dbo.SmpPmAssessmentPlan as c on a.AssessmentPlanGUID=c.GUID where  b.mailAddress is not null and c.AssessmentName='" + AssessmentPlanName + "' and a.AssessUserGUID='" + AssessUserGUID + "'";
            DataSet ds = engine.getDataSet(sqltxt, "tmp");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    string mailAddress = ds.Tables[0].Rows[0]["mailAddress"].ToString(); //"jack_xiao@simplo.com.cn";
                    string assessUserName = ds.Tables[0].Rows[0]["AssessmentName"].ToString();
                    string objectGUID = ds.Tables[0].Rows[0]["GUID"].ToString();
                    string subject = "績效考核重新Review郵件通知";
                    string content = "";
                    string href = emailHeader + "?runMethod=executePmProgram&programID=SPPM005M&ObjectGUID=" + objectGUID;
                    content += "考評名稱: " + assessUserName + "<br />";
                    content += "待辦事項: <a href='" + href + "'>請重新Review您部門績效等級分配比例</a><br />";

                    au.sendMailHTML("", smtpServer, mailAddress, "", systemMail, subject, content);
                }
                catch (Exception ex)
                {
                    errMsg = "重新Review時郵件發生錯誤: " + ex.Message;
                }
            }
            return errMsg;
        }
        /// <summary>
        /// 取得下屬成員工號
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="PlanGUID"></param>
        /// <returns></returns>
        public static string GetEmpNoCollect(string managerId, string PlanGUID)
        {
            string connectString = (string)HttpContext.Current.Session["connectString"];
            string engineType = (string)HttpContext.Current.Session["engineType"];
            string ReturnStr = "";
            
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            try
            {
                string sqltxt = " select distinct UserGUID from(select a.AssessmentPlanGUID,a.UserGUID,ISNULL(b.id,'') managerId,ISNULL(d.id,'') SencondManagerId,ISNULL(c.id,'') AssessmentManagerId from dbo.SmpPmAssessmentManager as a " +
                                " left join Users as b on a.FirstAssessUserGUID=b.id left join Users as c on a.AchievementUserGUID=c.id " +
                                " left join Users as d on a.SecondAssessUserGUID=d.id )z where AssessmentPlanGUID in(" + GetfilterNameplanGUID(PlanGUID) + ")  and AssessmentManagerId='" + managerId + "'";
                DataSet ds = engine.getDataSet(sqltxt, "temp");
                if (ds.Tables["temp"] != null && ds.Tables["temp"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["temp"].Rows.Count; i++)
                    {
                        if (ReturnStr.Equals(""))
                        {
                            ReturnStr = "'" + ds.Tables["temp"].Rows[i]["UserGUID"].ToString() + "'";
                        }
                        else
                        {
                            if (!ReturnStr.Contains(ds.Tables["temp"].Rows[i]["UserGUID"].ToString()))
                            {
                                ReturnStr += ",'" + ds.Tables["temp"].Rows[i]["UserGUID"].ToString() + "'";
                            }
                        }
                    }
                }

                sqltxt = " select distinct UserGUID from(select a.AssessmentPlanGUID,a.UserGUID,ISNULL(b.id,'') managerId,ISNULL(d.id,'') SencondManagerId,ISNULL(c.id,'') AssessmentManagerId from dbo.SmpPmAssessmentManager as a " +
                               " left join Users as b on a.FirstAssessUserGUID=b.id left join Users as c on a.AchievementUserGUID=c.id " +
                               " left join Users as d on a.SecondAssessUserGUID=d.id )z where AssessmentPlanGUID in(" + GetfilterNameplanGUID(PlanGUID) + ")  and SencondManagerId='" + managerId + "'";


                ds = engine.getDataSet(sqltxt, "temp2");
                if (ds.Tables["temp2"] != null && ds.Tables["temp2"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["temp2"].Rows.Count; i++)
                    {
                        if (ReturnStr.Equals(""))
                        {
                            ReturnStr = "'" + ds.Tables["temp2"].Rows[i]["UserGUID"].ToString() + "'";
                        }
                        else
                        {
                            if (!ReturnStr.Contains(ds.Tables["temp2"].Rows[i]["UserGUID"].ToString()))
                            {
                                ReturnStr += ",'" + ds.Tables["temp2"].Rows[i]["UserGUID"].ToString() + "'";
                            }
                        }
                    }
                }

                sqltxt = " select distinct UserGUID from(select a.AssessmentPlanGUID,a.UserGUID,ISNULL(b.id,'') managerId,ISNULL(d.id,'') SencondManagerId,ISNULL(c.id,'') AssessmentManagerId from dbo.SmpPmAssessmentManager as a " +
                   " left join Users as b on a.FirstAssessUserGUID=b.id left join Users as c on a.AchievementUserGUID=c.id " +
                   " left join Users as d on a.SecondAssessUserGUID=d.id )z where AssessmentPlanGUID in(" + GetfilterNameplanGUID(PlanGUID) + ")  and managerId='" + managerId + "'";

                ds = engine.getDataSet(sqltxt, "temp3");
                if (ds.Tables["temp3"] != null && ds.Tables["temp3"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["temp3"].Rows.Count; i++)
                    {
                        if (ReturnStr.Equals(""))
                        {
                            ReturnStr = "'" + ds.Tables["temp3"].Rows[i]["UserGUID"].ToString() + "'";
                        }
                        else
                        {
                            if (!ReturnStr.Contains(ds.Tables["temp3"].Rows[i]["UserGUID"].ToString()))
                            {
                                ReturnStr += ",'" + ds.Tables["temp3"].Rows[i]["UserGUID"].ToString() + "'";
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.StackTrace);
            }
            finally
            {
                engine.close();
            }
            return ReturnStr;

        }

        public static string GetPlanGUID(string PlanGUID)
        {
            string PlanStr = "";
            string sqltxt = " select distinct GUID from dbo.SmpPmAssessmentPlan where AssessmentName=(select AssessmentName from dbo.SmpPmAssessmentPlan where GUID='"+PlanGUID+"')";
            string connectString = (string)HttpContext.Current.Session["connectString"];
            string engineType = (string)HttpContext.Current.Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            DataSet ds = engine.getDataSet(sqltxt, "tmp");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (PlanStr.Equals(""))
                    {
                        PlanStr += "'" + ds.Tables[0].Rows[i]["GUID"].ToString() + "'";
                    }
                    else
                    {
                        PlanStr += ",'" + ds.Tables[0].Rows[i]["GUID"].ToString() + "'";
                    }
                }
            }
            return PlanStr;        
        }
        /// <summary>
        /// 依據考核計劃名稱取得GUID
        /// </summary>
        /// <param name="PlanGUID"></param>
        /// <returns></returns>
        public static string GetfilterNameplanGUID(string planName)
        {
            string PlanStr = "";
            string sqltxt = " select distinct GUID from dbo.SmpPmAssessmentPlan where AssessmentName='" + planName + "'";
            string connectString = (string)HttpContext.Current.Session["connectString"];
            string engineType = (string)HttpContext.Current.Session["engineType"];
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            DataSet ds = engine.getDataSet(sqltxt, "tmp");
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (PlanStr.Equals(""))
                    {
                        PlanStr += "'" + ds.Tables[0].Rows[i]["GUID"].ToString() + "'";
                    }
                    else
                    {
                        PlanStr += ",'" + ds.Tables[0].Rows[i]["GUID"].ToString() + "'";
                    }
                }
            }
            return PlanStr;
        }
    }
}