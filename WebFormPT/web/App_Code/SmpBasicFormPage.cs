using System;
using System.Collections;
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
using System.Xml;

using WebServerProject;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using System.DirectoryServices;
using System.Data.SqlClient;

/// <summary>
/// BasicFormPage 的摘要描述
/// </summary>
public class SmpBasicFormPage : BasicFormPage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    protected void setSignMode(string simMode)
    {
        if (!simMode.Equals(""))
        {
            string tempUIStatus = fixNull(Request.QueryString["UIStatus"]);
            Session["SIMMODE"] = simMode;
            setSession("SIMMODE", Session["SIMMODE"]);
            setProcessForm(tempUIStatus);
            initToolBar();
        }
    }

    private void setProcessForm(string UIStatus)
    {
        //特別處理AgreeButton & DisagreeButton
        AgreeButton.Display = false;
        DisagreeButton.Display = false;
        if ((UIStatus.Equals(ProcessNew)) || (UIStatus.Equals(ProcessModify)))
        {
            string simmode = (String)Session["SIMMODE"];
            if (simmode.Equals("2"))
            {
                SignButton.Display = false;
                AgreeButton.Display = true;
                //DisagreeButton.Display = true;
                //移除確認
                AgreeButton.ConfirmText = "";
                AgreeButton.Text = "同意";
            }
        }
    }

    private void initToolBar()
    {
        string simMode = (string)Session["SIMMODE"];

        //初始化工具列
        if (Request.QueryString["CertificateMode"] != null)
        {
        }
        else
        {
            com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
            switch (resultType)
            {
                default:
                    #region IE
                    FloatingToolBar2.Text = "</div>";
                    #endregion
                    break;
                case com.dsc.kernal.utility.BrowserProcessor.BrowserType.FireFox:
                    #region FireFox
                    FloatingToolBar2.Text = "</div><br/><br/>";
                    #endregion
                    break;
                case com.dsc.kernal.utility.BrowserProcessor.BrowserType.Chrome:
                    #region Chrome
                    FloatingToolBar2.Text = "</div><br/><br/>";
                    #endregion
                    break;
                case com.dsc.kernal.utility.BrowserProcessor.BrowserType.Safari:
                    #region Safari
                    FloatingToolBar2.Text = "</div><br/><br/>";
                    #endregion
                    break;
            }

        }
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        if (Request.QueryString["CertificateMode"] != null)
        {
            FloatingSignBar.Text = "<div id='ATB' style=\"display:none;text-alignment:bottom;position:absolute;top:25px;left:0px;height:25px;z-index:9999;width:100%;\" class='SimSignPanel'><table border=0 cellspacing=0 cellpadding=0><tr><td>";
        }
        else
        {
            if (!simMode.Equals("0"))
            {
                FloatingSignBar.Text = "<div id='ATB' style=\"text-alignment:bottom;position:absolute;top:25px;left:0px;height:25px;z-index:9999;width:100%;\" class='SimSignPanel'><table border=0 cellspacing=0 cellpadding=0><tr><td>";

            }
            else
            {
                FloatingSignBar.Text = "<div id='ATB' style=\"text-alignment:bottom;position:absolute;top:25px;left:0px;height:0px;display:none;z-index:9999;width:100%;\" class='SimSignPanel'><table border=0 cellspacing=0 cellpadding=0><tr><td>";
            }
        }

        SignLabel.Width = new System.Web.UI.WebControls.Unit("80px");
        SignLabel.Text = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "SignPanel1", "簽核意見");


        FloatingSignSep1.Text = "</td><td>";

        SignResultField.Width = new System.Web.UI.WebControls.Unit("150px");
        if (simMode.Equals("2"))
        {
            SignResultField.Display = false;
        }
        FloatingSignSep2.Text = "</td><td valign=bottom>";

        SignOpinionField.Width = new System.Web.UI.WebControls.Unit("250px");
        //SignOpinionField.MultiLine = true;
        //SignOpinionField.Height = new System.Web.UI.WebControls.Unit("25px");

        FloatingSignSep3.Text = "</td><td>";

        SignPhraseButton.Text = com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "SignPanel2", "片語");
        SignPhraseButton.Width = new System.Web.UI.WebControls.Unit("35px");

        SignPhraseOpenWin.clientEngineType = engineType;
        SignPhraseOpenWin.connectDBString = connectString;


        FloatingSignBar2.Text = "</td></tr></table></div>";

        DraftOpenWin.clientEngineType = engineType;
        DraftOpenWin.connectDBString = connectString;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        string tempUIStatus = fixNull(Request.QueryString["UIStatus"]);
        if (tempUIStatus.Equals("5")) //處理中
        { 
            this.Controls.Remove(RejectButton);
            this.Controls.AddAt(7, RejectButton);
            RejectButton.Display = true;

            AddSignButton.ImageUrl = "~/Images/GeneralAddSign.gif";
        }
        
        Session.Remove("isKM");
        
        //已保留表單不能簽核
        //if (base.isNew() == false)
        //{
        //    string objectGUID = (string)getSession("ObjectGUID");
        //    if (objectGUID != null)
        //    {
        //        string sql = "select SMWYAAA022 from SMWYAAA where SMWYAAA019='" + objectGUID + "'";
        //        string flag = (string)engine.executeScalar(sql);
        //        if (flag != null && flag.Equals("D"))
        //        {
        //            AgreeButton.Display = false;
        //            DisagreeButton.Display = false;
        //            SignButton.Display = false;
        //            AddSignButton.Display = false;
        //            RejectButton.Display = false;
        //            ForwardButton.Display = false;
        //            RedirectButton.Display = false;
        //        }
        //    }
        //}

	//-------------------------------------------------------------------------------------------------------以下方法注释为防止错误2015年3月21日10:49:19---------------------------------------
        //設定簽核完畢處理方式
       // string userGUID = (string)Session["UserGUID"];
       // string afterSignProcess = (string)engine.executeScalar("select AfterSignProcess from SmpUserConf where UserGUID='" + userGUID + "'");
       // if (afterSignProcess != null && !afterSignProcess.Equals(""))
       // {
           // setSession("AfterSignProcess", afterSignProcess);
       // }

	//----------------------------------------------------------------------------------------------------------注釋完畢以上------------------------------------------------------------------
    }
	
	/// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        string sheetNo = objects.getData("SheetNo");
        if (!sheetNo.Equals("")) //單號有值
        {
            if (Request.QueryString["IsCopyForm"] == null) //非複製表單 
            {
                string draftguid = Convert.ToString(getSession("READDRAFTGUID")); //讀取草稿
                if (draftguid.Equals(""))
                {
                    //若有單號則放入Session
                    setSession(base.PageUniqueID, "SheetNo", sheetNo);
                }
            }
        }
    }
	
	/// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        //取得Session單號
        string sheetNo = Convert.ToString(getSession(base.PageUniqueID, "SheetNo"));
        if (sheetNo.Equals(""))
        {
            string autoCodeGUID = getAutoCodeGUID(engine);
            sheetNo = base.getSheetNoProcedure(engine, autoCodeGUID);
            setSession(base.PageUniqueID, "SheetNo", sheetNo);
            try {
				objects.setData("SheetNo", sheetNo);
			} catch (Exception e) {
				base.writeLog(e);
			}
        }
    }
	
	/// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="autoCodeGUID"></param>
    /// <returns></returns>
    protected override string getSheetNoProcedure(AbstractEngine engine, string autoCodeGUID)
    {
        string sheetNo = Convert.ToString(getSession(base.PageUniqueID, "SheetNo"));
        if (sheetNo.Equals(""))
        {
            sheetNo = base.getSheetNoProcedure(engine, autoCodeGUID);
			try {
				DataObject currentObject = (DataObject)getSession("currentObject");
				currentObject.setData("SheetNo", sheetNo);
				engine.updateData(currentObject);
			} catch (Exception e) {
				base.writeLog(e);
			}
        }
        return sheetNo;
    }
	
	/// <summary>
    /// 儲存草稿
    /// </summary>
    /// <returns></returns>
    protected override string saveDraftProcedure()
    {
        setSession(base.PageUniqueID, "SheetNo", "");
        return base.saveDraftProcedure();
    }
	
	/// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        base.afterSign(engine, currentObject, result);
        refreshInbox();
    }

    /// <summary>
    /// 退回重辦程序
    /// </summary>
    protected override void rejectProcedure()
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            //代理轉派動作
            reassignmentSubstitute(engine);

            //送單程序

            string flowOID = (string)getSession("FlowGUID"); //流程實例序號
            string WorkItemOID = (string)getSession("WorkItemOID");//工作項目識別號


            string ACTID = fetchActivityIDFromWorkItemOID(engine, WorkItemOID, (string)getSession("PDID"), (string)Session["UserID"]);

            //簽核程序
            engine = factory.getEngine(engineType, connectString);

            string backActID = (string)Session["tempBackActID"];
            string backOpinion = (string)Session["tempBackOpinion"];
            string backType = (string)Session["tempBackType"];

            reexecuteActivity(engine, (string)Session["UserID"], flowOID, WorkItemOID, ACTID, backActID, backOpinion, backType);

            //儲存成功
            //Response.Write("alert('" + RejectSuccessMsg + "');");
            MessageBox(RejectSuccessMsg);

            string userGUID = (string)Session["UserGUID"];
            string afterRejectProcess = (string)engine.executeScalar("select AfterRejectProcess from SmpUserConf where UserGUID='" + userGUID + "'");
            if (afterRejectProcess != null && !afterRejectProcess.Equals(""))
            {
                if (afterRejectProcess.Equals("3"))
                {
                    closeRefresh();
                }
                else
                {
                    closeRefreshClick();
                }
            }
            else
            {
                string needCloseRefreshClick = Convert.ToString(getSession("closeRefreshClick")); //送簽後動作
                if (needCloseRefreshClick.Equals("N"))
                {
                    //closeRefreshClick();
                }
                else
                {
                    closeRefreshClick();
                }
            }

            engine.close();
            refreshInbox();
        }
        catch (Exception ue)
        {
            try
            {
                engine.close();
            }
            catch { };
            processErrorMessage(errorLevel, ue);
        }
    }

	/// <summary>
    /// 轉寄程序
    /// </summary>
    protected override void forwardProcedure()
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            //送單程序

            string workItemOID = (string)getSession("WorkItemOID"); //工作項目識別號

            //程序
            engine = factory.getEngine(engineType, connectString);

            string[] acceptorOID = (string[])Session["tempAcceptorOID"];
            string noticeType = (string)Session["tempNoticeType"];

            //客製轉寄意見 20130430 CL_Chang
            string userId = (string)Session["UserID"];
            string flowOID = (string)getSession("FlowGUID"); //流程實例序號
            string forwardComment = (string)Session["forwardComment"];//轉寄意見
            
            if (!Convert.ToString(forwardComment).Equals(""))
            {
                string[] userGUIDInfo = getUserGUID(engine, userId);
                string userGUID = userGUIDInfo[0];
                
                DataRow dsr = null;
                string sql = "select * from SmpForwardNotice where (1=2)";
                DataSet tsd = engine.getDataSet(sql, "TEMP");

                sql = "select * from SMWYAAA where SMWYAAA005='" + (string)getSession("FlowGUID") + "'";
                DataSet usd = engine.getDataSet(sql, "TEMP");
                string flowname = usd.Tables[0].Rows[0]["SMWYAAA004"].ToString();
                string subject = usd.Tables[0].Rows[0]["SMWYAAA006"].ToString();
                dsr = usd.Tables[0].Rows[0];

                for (int i = 0; i < acceptorOID.Length; i++)
                {
                    DataRow dr = tsd.Tables[0].NewRow();
                    dr["GUID"] = IDProcessor.getID("");
                    dr["ObjectGUID"] = (string)getSession("ObjectGUID");
                    dr["PDID"] = (string)getSession("PDID");
                    dr["ProcessSerialNumber"] = (string)getSession("FlowGUID");
                    dr["ViewTimes"] = "U";
                    dr["SenderId"] = (string)Session["UserID"];
                    dr["SenderName"] = (string)Session["UserName"];
                    dr["SendTime"] = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
                    dr["ReceiverId"] = acceptorOID[i];
                    dr["ProcessName"] = flowname;
                    dr["Subject"] = subject;
                    dr["ProcessType"] = "INFO";
                    dr["RelatedState"] = "";
                    dr["WorkItemOID"] = workItemOID;
                    dr["Comment"] = forwardComment;
                    dr["D_INSERTUSER"] = userGUID;
                    dr["D_INSERTTIME"] = DateTimeUtility.getSystemTime2(null);
                    dr["D_MODIFYUSER"] = "";
                    dr["D_MODIFYTIME"] = "";
                    tsd.Tables[0].Rows.Add(dr);
                }

                if (!engine.updateDataSet(tsd))
                {
                    throw new Exception(engine.errorString);
                }
            }

            if (!workItemOID.Equals(""))
            {
                forwardWorkItem(engine, (string)Session["UserID"], workItemOID, acceptorOID, noticeType);
            }
            else
            {
                //國昌20100614:自訂的通知（原稿發起）打開
                DataRow dsr = null;
                string sql = "select * from CUSTOMENOTICE where (1=2)";
                DataSet tsd = engine.getDataSet(sql, "TEMP");

                sql = "select * from SMWYAAA where SMWYAAA005='" + (string)getSession("FlowGUID") + "'";
                DataSet usd = engine.getDataSet(sql, "TEMP");
                string flowname = usd.Tables[0].Rows[0]["SMWYAAA004"].ToString();
                string subject = usd.Tables[0].Rows[0]["SMWYAAA006"].ToString();
                dsr = usd.Tables[0].Rows[0];

                for (int i = 0; i < acceptorOID.Length; i++)
                {
                    DataRow dr = tsd.Tables[0].NewRow();
                    dr["GUID"] = IDProcessor.getID("");
                    dr["OBJECTGUID"] = (string)getSession("ObjectGUID");
                    dr["PDID"] = (string)getSession("PDID");
                    dr["PROCESSSERIALNUMBER"] = (string)getSession("FlowGUID");
                    dr["VIEWTIMES"] = "U";
                    dr["SENDERID"] = (string)Session["UserID"];
                    dr["SENDERNAME"] = (string)Session["UserName"];
                    dr["SENDTIME"] = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
                    dr["RECEIVERID"] = acceptorOID[i];
                    dr["PROCESSNAME"] = flowname;
                    dr["SUBJECT"] = subject;
                    dr["PROCESSTYPE"] = "INFO";
                    dr["RELATEDSTATE"] = "";
                    tsd.Tables[0].Rows.Add(dr);
                }

                if (!engine.updateDataSet(tsd))
                {
                    throw new Exception(engine.errorString);
                }

                customAfterForward(engine, dsr, acceptorOID);
            }
            engine.close();

            //儲存成功
            //Response.Write("alert('" + ForwardSuccessMsg + "');");
            MessageBox(ForwardSuccessMsg);

            closeRefresh();
        }
        catch (Exception ue)
        {
            try
            {
                engine.close();
            }
            catch { };
            processErrorMessage(errorLevel, ue);
        }
    }
	
	/// <summary>
    /// 取得使用者資訊, [0]: id, [1]: userName, [2]: orgId, [3]: titleId
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userGUID"></param>
    /// <returns>string[]</returns>
    protected string[] getUserInfo(AbstractEngine engine, string userGUID)
    {
        string sql = "select empNumber, empName, orgId, titleId,deptId from EmployeeInfo where empGUID='" + Utility.filter(userGUID) + "'";
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
    /// 查詢使用者之相關資料,包含職位,直屬主管,所屬部門!
    /// [0]:UserID, [1]:UserName, [2]: UserADAccount, [3]:UserTitle, [4]:EMAIL, [5]:ORGID
    /// </summary>
    /// <param name="engine"></param> default engine
    /// <param name="userGUID"></param> User GUID
    /// <returns>string[]</returns> 僅回傳一筆, 值為 Array! Array[0]為人員工號, Array[1]為中文名, Array[2]為英文名,  Array[3]為職稱!
    protected string[] getUserInfoById(AbstractEngine engine, string userId)
    {
        //string sql = "select id,userName,substring(mailAddress, 1 ,( charindex('@', mailAddress)-1)), a.UserTitle,mailAddress from Users u, Functions f , UserFunctions a where  f.occupantOID = u.OID and f.isMain='1' '" + Utility.filter(userId) + "' and id= and UserOID = u.OID ";
        string sql = "select u.id,userName,substring(mailAddress, 1 ,( charindex('@', mailAddress)-1)), a.UserTitle,mailAddress, o.id from Users u, Functions f , UserFunctions a, Organization o, OrganizationUnit ou where  f.occupantOID = u.OID and f.isMain='1' and u.id='" + Utility.filter(userId) + "'  and UserOID = u.OID and f.organizationUnitOID = ou.OID and ou.organizationOID=o.OID and a.UnitOID=ou.OID "; 
				
		DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[9];
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
    /// 取得使用者主管資訊, [0]:OID, [1]: id, [2]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userGUID"></param>
    /// <returns>string[]</returns>
    protected string[] getUserManagerInfo(AbstractEngine engine, string userGUID)
    {
        string[] result = new string[3];
        string sql = "select u.OID, u.id, u.userName from Functions f, Users u where f.occupantOID = '" + Utility.filter(userGUID) + "' and f.specifiedManagerOID = u.OID and f.isMain='1'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
        }
        return result;
    }



    /// <summary>
    /// 取得使用者主管資訊, [0]:OID, [1]: id, [2]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userID">工號</param>
    /// <returns>string[]</returns>
     protected string[] getUserManagerInfoID(AbstractEngine engine, string userID)
    {
        string[] result = new string[3];
        string Sql = " select u.OID, u.id, u.userName from Functions f, Users u where f.occupantOID = (select OID from Users where id='" + userID + "') and  f.specifiedManagerOID = u.OID and f.isMain='1'";
        DataSet ds = engine.getDataSet(Sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
        }
        return result;
    }

        /// <summary>
    /// 取得User個人信息
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userID">工號</param>
    /// <returns>string[]</returns>
     protected string[] getUserforID(AbstractEngine engine, string userID)
    {
        string[] result = new string[1];
        string Sql = "select EmpTypeName from HRUSERS where EmpNo='" + userID + "'";
         DataSet ds = engine.getDataSet(Sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
              result[0] = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            result[0] = "";
        }
        return result;
    }



    /// <summary>
    /// 取得群組人員, [][]: [id][userName]
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="groupName"></param>
    /// <returns>string[][]</returns>
    protected string[][] getGroupdUser (AbstractEngine engine, string groupId)
    {
        string sql = "select u.id, userName from Groups g, Users u, Group_User gu where g.id='" + Utility.filter(groupId) + "' and gu.GroupOID = g.OID and gu.UserOID = u.OID";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[2];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();
            result[i][1] = ds.Tables[0].Rows[i][1].ToString();
        }

        return result;
    }

    /// <summary>
    /// 取得使用者OID, [0]: OID, [1]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userId"></param>
    /// <returns>string[]</returns>
    protected string[] getUserGUID(AbstractEngine engine, string userId)
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
    /// 取得群組OID, [0]: OID, [1]: groupName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="groupId"></param>
    /// <returns>string[]</returns>
    protected string[] getGroupGUID(AbstractEngine engine, string groupId)
    {
        string sql = "select OID, groupName from Groups where id='" + Utility.filter(groupId) + "'";
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
    /// 查詢使用者之相關資料,包含職位,直屬主管,所屬部門!
    /// [0]:UserID, [1]:UserOID, [2]: UserName, [3]:UserTitle, [4]:UnitName, [5]:UnitOID, [6]:ManagerID, [7]:ManagerOID, [8]:ManagerName
    /// </summary>
    /// <param name="engine"></param> default engine
    /// <param name="userGUID"></param> User GUID
    /// <returns>string[]</returns> 僅回傳一筆, 值為 Array! Array[6]為直屬主管 ID, Array[7]為主屬主管之OID, Array[8]為主管名稱
    ///                                            Array[3]為職稱, Array[4]為部門名稱!! 注意若無直屬主管,資料將不會帶出來!
    protected string[] getUserFunctions(AbstractEngine engine, string userGUID)
    {
        string sql = "select UserID,UserOID,UserName,UserTitle,UnitName,UnitOID,ManagerID,ManagerOID,ManagerName from UserFunctions where UserOID='" + Utility.filter(userGUID) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[9];
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
        }
        return result;
    }

    /// <summary>
    /// 尋找某個部門中某個角色之人員代號, 例如:尋找 ME 部門的收發人員角色!
    /// [0]:RoleName, [1]:UserOID, [2]:UserID, [3]:UserName, [4]:DetpOID, [5]:DeptID, [6]:DeptName
    /// </summary>
    /// <param name="engine"></param> default engine
    /// <param name="userRoles"></param> 角色名稱, 如: 部門收發
    /// <param name="deptID"></param> 部門代號, 如: C2200
    /// <returns>string[]</returns> 僅回傳一筆資料, 回傳值為一 Array! Array[1] 是 UserOID, Array[2]是 UserID (使用者代號)
    protected string[] getUserRoles(AbstractEngine engine, string userRoles, string deptID)
    {
        string sql = "select RoleName, UserOID, UserID, UserName, DetpOID, DeptID, DeptName from UserRoles where RoleName='" + Utility.filter(userRoles) + "' and DeptID='" + Utility.filter(deptID) + "'";
        //MessageBox(sql);
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[7];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
            result[3] = ds.Tables[0].Rows[0][3].ToString();
            result[4] = ds.Tables[0].Rows[0][4].ToString();
            result[5] = ds.Tables[0].Rows[0][5].ToString();
            result[6] = ds.Tables[0].Rows[0][6].ToString();
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
        }
        return result;
    }

    /// <summary>
    /// 取得使用者代理人(通用代理)資訊, [0]: OID, [1]: id, [2]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userGUID"></param>
    /// <returns>string[]</returns>
    protected string[] getSubstituteUserInfo(AbstractEngine engine, string userGUID)
    {
        //通用代理人
        string sql = "select Users.OID, id, userName from DefaultSubstituteDefinition inner join Users on substituteOID=Users.OID where ownerOID='" + Utility.filter(userGUID) + "' order by substitutiveOrder";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[3];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
        }
        return result;
    }

    /// <summary>
    /// 取得表單單號
    /// </summary>
    /// <param name="engine"></param> default engine
    /// <param name="flowGUID"></param> 傳入 Session 的 flowGUID getSession("FlowGUID")
    /// <returns></returns> 回傳表單單號
    protected string getSheetNo(AbstractEngine engine, string flowGUID)
    {
        string sql = "select SMWYAAA002 from SMWYAAA where SMWYAAA005='" + Utility.filter(flowGUID) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string result = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            result = ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
            result = "";
        }
        return result;
    }
	
	/// <summary>
    /// 取得AutoCodeGUID
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="ProcessPageID"></param>
    /// <returns></returns>
    protected string getAutoCodeGUID(AbstractEngine engine)
    {
        string sql = "select SMWBAAA003, SMWDAAA001, SMWDAAA011,SMWDAAA012,SMWBAAA004, SMWAAAA001,SMWDAAA018, SMWBAAA001, SMWDAAA024 from SMWAAAA inner join SMWDAAA on SMWAAAA001=SMWDAAA005 inner join SMWBAAA on SMWBAAA004=SMWDAAA003 where SMWAAAA002='" + ProcessPageID + "' and SMWDAAA006='Init'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count == 0)
        {
            throw new Exception(com.dsc.locale.LocaleString.getKernalLocaleString("BaseWebUI.dll.language.ini", "message", "QueryError7", "找不到此作業畫面所要發起的流程定義"));
        }
        else
        {
            return ds.Tables[0].Rows[0][3].ToString();
        }
    }
	
	/// <summary>
    /// 終止目前表單流程
    /// </summary>
    /// <param name="ownerID"></param>
    protected void terminateThisProcess(string ownerID)
    {
        AbstractEngine engine = null;
        try
        {
            string flowType = getGPParam("FlowAdapter");
            string con1 = getGPParam("NaNaWebService");
            string con2 = getGPParam("DotJWebService");
            string account = getGPParam("FlowAccount");
            string password = getGPParam("FlowPassword");
            string flowOID = (string)getSession("FlowGUID");

            FlowFactory ff = new FlowFactory();
            AbstractFlowAdapter adp = ff.getAdapter(flowType);
            adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
            adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

            string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
            fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
            adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, debugPage);
            adp.terminateProcess(ownerID, flowOID, "不同意  ", "自動終止流程");
            adp.logout();
        }
        catch (Exception e)
        {
            MessageBox(e.Message);

        }
        finally
        {
            if (engine != null) engine.close();
        }
    }
	
    /// <summary>
    /// 終止目前表單流程
    /// </summary>
	protected void terminateThisProcess()
    {
        AbstractEngine engine = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
			
			//代理轉派動作
            reassignmentSubstitute(engine);
				
			string signResult = "不同意";
			string backOpinion = (string)Session["tempBackOpinion"];
            //string signOpinion = (string)Session["tempSignOpinion"];
            string flowOID = (string)getSession("FlowGUID");
            base.terminateProcess(engine, flowOID, signResult, backOpinion);
			
			MessageBox("退件成功");
			refreshInbox();
			closeRefreshClick();
        }
        catch (Exception e)
        {
            MessageBox(e.Message);
        }
        finally
        {
            if (engine != null) engine.close();
        }
    }

    /// <summary>
    /// 取得部門主管人員資訊
    /// [0]: orgUnitId, [1]: orgInitName, [2]: userOID, [3]: userId, [4]: userName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="orgUnitGUID"></param>
    /// <returns></returns>
    protected string[] getOrgUnitInfo(AbstractEngine engine, string orgUnitGUID)
    {
        string sql = "select o.id, o.organizationUnitName, u.OID, u.id, u.userName ";
        sql += "from OrganizationUnit o, Users u where o.OID='" + Utility.filter(orgUnitGUID) + "' and o.managerOID = u.OID";
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
    protected string[] getOrgUnit(AbstractEngine engine, string id)
    {
        string sql = "select o.id, o.organizationUnitName, u.OID, u.id, u.userName ";
        sql += "from OrganizationUnit o, Users u where o.id='" + Utility.filter(id) + "' and o.managerOID = u.OID";
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
    /// 取得HRUser資料
    /// </summary>
    /// <param name="EmpNo">工號</param>
      protected Hashtable getHRUsers(AbstractEngine engine, string EmpNo)
    {
          Hashtable h = new Hashtable();
          try
          {
                string sql = @"SELECT * FROM [HRUSERS] WHERE [EmpNo]='" + EmpNo + "'";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                      for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                      {
                            h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                      }
                }
          }
          catch (Exception ex)
          {
                throw new Exception("查詢HRUSERS錯誤, " + ex.Message.ToString());
          }
          return h;
    }
      /// <summary>
      /// 取得出差人員資料
      /// </summary>
      /// <param name="EmpNo">工號</param>
      protected Hashtable getTrvUsers(AbstractEngine engine, string EmpNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"SELECT * FROM SCQ.dbo.SmpTrvlSubSite WHERE [Id]='" + EmpNo + "'";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢HRUSERS錯誤, " + ex.Message.ToString());
          }
          return h;
      }
      /// <summary>
      /// 取得部門助理資料
      /// </summary>
      /// <param name="EmpNo">工號</param>
      protected Hashtable getbmzl(AbstractEngine engine, string PartNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select EmpNo,MainMan from [10.3.11.92\SQL2008].SCQHRDB.DBO.PerDepart where [PartNo]='" + PartNo + "'";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢部門助理錯誤, " + ex.Message.ToString());
          }
          return h;
      }
      /// <summary>
      /// 取得離職單編號
      /// </summary>
      /// <param name="EmpNo">工號</param>
      protected Hashtable getHRcode(AbstractEngine engine, string PartNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select MAX(HRCODE) HRCODE from EH0112A";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢ID錯誤, " + ex.Message.ToString());
          }
          return h;
      }  
      
      /// <summary>
      /// 取得專業加給資料 
      /// </summary>
      /// <param name="EmpNo">單號</param>
      protected Hashtable getJC(AbstractEngine engine, string EmpNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select a.EmpNo,
case when dg is null then '0' else dg end dg,
case when xg is null then '0' else xg end xg,
case when jj is null then '0' else jj end jj,
case when dgg is null then '0' else dgg end dgg,
case when xgg is null then '0' else xgg end xgg,
case when sj is null then '0' else sj end sj from (
select EmpNo from [10.3.11.92\SQL2008].SCQHRDB.dbo.PerEmployee where InCumbency='1') a
left join (
select EmpNo,sum(a) dg,sum(b)xg,sum(c) jj,sum(d)dgg,sum(e)xgg,sum(f)sj from (
select EmpNo,CONVERT(int,a) a,CONVERT(int,b) b,CONVERT(int,c) c,CONVERT(int,d) d,CONVERT(int,e) e,CONVERT(int,f) f from (
select EmpNo,RPID,
case RPID when '0001' then '1' else '0' end a,
case RPID when '0002' then '1' else '0' end b,
case RPID when '0003' then '1' else '0' end c,
case RPID when '0004' then '1' else '0' end d,
case RPID when '0005' then '1' else '0' end e,
case RPID when '0006' then '1' else '0' end f
 from [10.3.11.92\SQL2008].SCQHRDB.dbo.PerRewOrPen1 where YYMMDD> CONVERT(nvarchar(10),GETDATE()-365,23)
 ) h) g  group by EmpNo) b on a.EmpNo=b.EmpNo where a.EmpNo='" + EmpNo + "'";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢JC錯誤, " + ex.Message.ToString());
          }
          return h;
      }


      /// <summary>
      /// 取得請假資料
      /// </summary>
      /// <param name="EmpNo">工號</param>
      protected Hashtable getLeave(AbstractEngine engine, string EmpNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select a.EmpNo,case when sj is null then '0' else sj end sj,
case when bj is null then '0' else bj end bj,
case when kg is null then '0' else kg end kg from(
select EmpNo from [10.3.11.92\SQL2008].SCQHRDB.dbo.PerEmployee where InCumbency=1) a
left join(
select EmpNo,SUM(NormalTime)sj from [10.3.11.92\SQL2008].SCQHRDB.dbo.AttLeaveData where YYMMDD between '2017-12-26' and CONVERT(nvarchar(10),GETDATE()-1,23) and LeaveType='A01'
group by EmpNo) b
on a.EmpNo=b.EmpNo
left join(
select EmpNo,SUM(NormalTime)bj from [10.3.11.92\SQL2008].SCQHRDB.dbo.AttLeaveData where YYMMDD between '2017-12-26' and CONVERT(nvarchar(10),GETDATE()-1,23) and LeaveType='A02'
group by EmpNo) c on a.EmpNo=c.EmpNo 
left join(
select EmpNo,SUM(TruancyTime1)kg from [10.3.11.92\SQL2008].SCQHRDB.dbo.AttDayData where YYMMDD between '2017-12-26' and CONVERT(nvarchar(10),GETDATE()-1,23) and ErrorWhy='曠工'
group by EmpNo
) d on d.EmpNo=a.EmpNo  WHERE a.EmpNo='" + EmpNo + "'";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢請假錯誤, " + ex.Message.ToString());
          }
          return h;
      }
      /// <summary>
      /// 取得績效資料
      /// </summary>
      /// <param name="EmpNo">工號</param>
      protected Hashtable getHRjx(AbstractEngine engine, string EmpNo)
      {
          Hashtable h = new Hashtable();
          try
          {
               StringBuilder sBuilder = new StringBuilder();
               
              sBuilder.Append(" select c.EmpNo,case when c.Field004<>'' then c.Field004 else d.Field004 end Field004, ");
              sBuilder.Append(" case when c.Field005<>'' then c.Field005 else d.Field005 end Field005 from ( ");
              sBuilder.Append(" select * from ( ");
              sBuilder.Append(" select top 2 EmpNo,Field004,Field005,ROW_NUMBER() OVER(PARTITION BY EmpNo ORDER BY EmpNo) A ");
              sBuilder.Append(" from [10.3.11.92\\SQL2008].SCQHRDB.dbo.CU001501Data a ,[10.3.11.92\\SQL2008].SCQHRDB.dbo.PerEmployee b where a.EmpID=b.EmpID and EmpNo='" + EmpNo + "' order by YY desc) ");
              sBuilder.Append(" a where a.A=1) c ");
              sBuilder.Append(" left join ");
              sBuilder.Append(" (select * from ( ");
              sBuilder.Append(" select top 2 EmpNo,Field004,Field005,ROW_NUMBER() OVER(PARTITION BY EmpNo ORDER BY EmpNo) A  ");
              sBuilder.Append(" from [10.3.11.92\\SQL2008].SCQHRDB.dbo.CU001501Data a ,[10.3.11.92\\SQL2008].SCQHRDB.dbo.PerEmployee b where a.EmpID=b.EmpID and EmpNo='" + EmpNo + "' order by YY desc)  ");
              sBuilder.Append(" a where a.A=2) d on c.EmpNo=d.EmpNo ");

              DataSet ds = engine.getDataSet(sBuilder.ToString(), "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢績效錯誤, " + ex.Message.ToString());
          }
          return h;
      }

      ///// <summary>
      ///// 取得調薪資料
      ///// </summary>
      ///// <param name="EmpNo">工號</param>
      protected Hashtable getHRtx(AbstractEngine engine, string EmpNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select * from [10.3.11.92\SQL2008].SCQHRDB.dbo.ecptx WHERE empno='" + EmpNo + "'";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢調薪錯誤, " + ex.Message.ToString());
          }
          return h;
      }
      ///// <summary>
      ///// 取得調薪資料
      ///// </summary>
      ///// <param name="EmpNo">工號</param>
      protected Hashtable getpx1(AbstractEngine engine, string EmpNo,string NDtName)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select Field002,Field018,case when Field016 is null OR Field016='' then '不合格' else '合格' end gkk from
[10.3.11.92\SQL2008].SCQHRDB.dbo.CU004901Data where Field009='公開課晉升培訓' and Field002='" + EmpNo + "'and Field018='" + NDtName + "'  group by Field002,Field018,Field016";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("培訓記錄錯誤, " + ex.Message.ToString());
          }
          return h;
      }
      protected Hashtable getpx2(AbstractEngine engine, string EmpNo,string NDtName )
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select Field002,Field018,case when Field016 is null OR Field016='' then '不合格' else '合格' end bmj from
[10.3.11.92\SQL2008].SCQHRDB.dbo.CU004901Data where Field009='部門晉升培訓' and Field002='" + EmpNo + "' and Field018='" + NDtName + "' group by Field002,Field018,Field016";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("培訓記錄錯誤, " + ex.Message.ToString());
          }
          return h;
      }
      ///// <summary>
      ///// 取得薪資解密資料
      ///// </summary>
      ///// <param name="EmpNo">工號</param>
      protected Hashtable getjiemi(AbstractEngine engine, string EmpNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select * from .dbo.EH0110A WHERE EmpNo='" + EmpNo + "' and IS_LOCK='A'";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢解密, " + ex.Message.ToString());
          }
          return h;
      }
      protected Hashtable getjiemi1(AbstractEngine engine, string EmpNo)
      {
          Hashtable h = new Hashtable();
          try
          {
              string sql = @"select * from .dbo.EH0111A WHERE EmpNo='" + EmpNo + "' and IS_LOCK='A'";
              DataSet ds = engine.getDataSet(sql, "TEMP");
              if (ds.Tables[0].Rows.Count > 0)
              {
                  for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                  {
                      h.Add(ds.Tables[0].Columns[i].ToString(), ds.Tables[0].Rows[0][i]);
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("查詢解密, " + ex.Message.ToString());
          }
          return h;
      }
    /// <summary>
    /// 取得會議室資料
    /// </summary>
    ///
    protected DataTable getMeetingroom(AbstractEngine engine, string RoomNo, string Day)
    {
        DataTable h=null;
        try
        {
            string sql = @"select * from dbo.EG0104A where RoomNo='" + RoomNo + "' and convert(nvarchar(10),SDateTime,112) like '%" + Day + "%' and IS_LOCK not in('Y','D')";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                h = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
           
        }
        return h;
    }


    //<summary>
    //取得編號資料
    //</summary>
    protected string getCustomSheetNo(AbstractEngine engine, string code)
    {
        string sheetNo = "";
        try
        {
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("FORMID", ProcessPageID);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return sheetNo;
    }




     //<summary>
     //取得AD資料
     //</summary>
    /// <param name="id"></param>
    protected Hashtable getADUserData(AbstractEngine engine, string id)
    {
          //取得AD資料
          WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
          string ldap = sp.getParam("SCQLDAPDN");
          string account = sp.getParam("SCQLDAPAccount");
          string[] sep = new string[] { "/" };
          Hashtable h = new Hashtable();
          if (account != "" && account.IndexOf("/") > 0)
          {
                try
                {
                      DirectoryEntry de = new DirectoryEntry("LDAP://" + ldap, account.Split(sep, StringSplitOptions.None)[0], account.Split(sep, StringSplitOptions.None)[1]);
                      DirectorySearcher dsc = new DirectorySearcher(de);
                      try
                      {
                            dsc.Filter = "(postalcode=" + id + ")";
                            dsc.PropertiesToLoad.Add("mail");
                            dsc.PropertiesToLoad.Add("mailnickname");
                            dsc.PropertiesToLoad.Add("telephonenumber");
                            SearchResult sr = dsc.FindOne();
                           
                            foreach (string key in sr.Properties.PropertyNames)
                            {
                                  if (key == "mail")
                                  {
                                        foreach (object obj in sr.Properties[key])
                                        {
                                              if (obj.ToString() != "")
                                              {
                                                    h.Add("mail", obj);
                                              }

                                        }
                                  }
                                  if (key == "mailnickname")
                                  {
                                        foreach (object obj in sr.Properties[key])
                                        {
                                              if (obj.ToString() != "")
                                              {
                                                    h.Add("mailnickname", obj);
                                              }

                                        }
                                  }
                                  if (key == "telephonenumber")
                                  {
                                        foreach (object obj in sr.Properties[key])
                                        {
                                              if (obj.ToString() != "")
                                              {
                                                    h.Add("telephonenumber", obj);
                                              }

                                        }
                                  }
                            }
                          
                      }
                      catch (Exception ex)
                      {
                            throw new Exception("取得LDAP郵箱錯誤, " + ex.Message.ToString());
                      }
                }
                catch (Exception ex)
                {
                      throw new Exception("連線LDAP錯誤, " + ex.Message.ToString());
                }
          }
          else
          {
                throw new Exception("請維護LDAP帳號密碼, SCQLDAPAccount=帳號/密碼.");
          }
          return h;
    }
}
