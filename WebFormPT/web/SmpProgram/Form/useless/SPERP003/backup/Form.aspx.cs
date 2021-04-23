using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
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

public partial class Program_System_Form_SPPO_Form : SmpErpFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "SPERP003";
        AgentSchema = "WebServerProject.form.SPERP003.SPPOAAgent";
        ApplicationID = "SMPFORM";
        ModuleID = "SPERP";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        SPPOA004.clientEngineType = engineType;
        SPPOA004.connectDBString = connectString;
        SPPOA005.clientEngineType = engineType;
        SPPOA005.connectDBString = connectString;
        SPPOA012.clientEngineType = engineType;
        SPPOA012.connectDBString = connectString;
        SPPOA013.clientEngineType = engineType;
        SPPOA013.connectDBString = connectString;
        SPPOA019.clientEngineType = engineType;
        SPPOA019.connectDBString = connectString;

        SPPOA003.ValueText = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
        SPPOA004.ValueText = si.fillerID;
        SPPOA004.doValidate();
        SPPOA005.ValueText = si.fillerOrgID;
        SPPOA005.doValidate();

        bool isAddNew = base.isNew();
        DataObjectSet detailSet = null;
        if (isAddNew)
        {
            detailSet = new DataObjectSet();
            detailSet.isNameLess = true;
            detailSet.setAssemblyName("WebServerProject");
            detailSet.setChildClassString("WebServerProject.form.SPERP003.SPPOB");
            detailSet.setTableName("SPPOB");
            detailSet.loadFileSchema();
            objects.setChild("SPPOB", detailSet);
        }
        else
        {
            detailSet = objects.getChild("SPPOB");
        }
        //detailSet.reOrderField(new string[] { "SPPOB020" });
        string[,] orderby = new string[,]{{"SPPOB020",DataObjectConstants.ASC}};
        detailSet.sort(orderby);
        DataListLine.dataSource = detailSet;
        DataListLine.HiddenField = new string[] { "GUID", "HeadGUID", "SPPOB001", "SPPOB002", "SPPOB003", "SPPOB011", "SPPOB013", "SPPOB014", "SPPOB015", "SPPOB016", "SPPOB017", "SPPOB018", "SPPOB019", "SPPOB020", "SPPOB021", "SPPOB022", "IS_LOCK", "DATA_STATUS", "IS_DISPLAY" };
        //DataListLine.NoAdd = true;                                                                                                                                                     
        //DataListLine.NoDelete = true;                                                                                                                                                  
        //DataListLine.NoModify = true;
        //DataListLine.reSortCondition("LINE_ID", DataObjectConstants.ASC);                                                                                                                                    
        DataListLine.updateTable();
        DataListLine.ReadOnly = true;


        //唯讀欄位
        Subject.ReadOnly = true;
        SPPOA003.ReadOnly = true;
        SPPOA004.ReadOnly = true;
        SPPOA005.ReadOnly = true;
        SPPOA006.ReadOnly = true;
        SPPOA008.ReadOnly = true;
        SPPOA009.ReadOnly = true;
        SPPOA010.ReadOnly = true;
        SPPOA012.ReadOnly = true;
        SPPOA013.ReadOnly = true;
        SPPOA015.ReadOnly = true;
        SPPOA018A.ReadOnly = true;
        SPPOA018B.ReadOnly = true;
        SPPOA018C.ReadOnly = true;
        SPPOA019.ReadOnly = true;
        //DataListLine.ReadOnly = true;

        //改變工具列順序
        base.initUI(engine, objects);                                                                                                                                                                                                                                                                                                                      
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
                                                                                                                                                                            
    protected override void showData(AbstractEngine engine, DataObject objects)                                                                                     
    {
        base.showData(engine, objects);
        string actName = Convert.ToString(getSession("ACTName"));
        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        //請購單URL
        string url = sp.getParam("PortalPrUrl");
        ViewPrUrl.Value = url;

        Subject.ValueText = objects.getData("Subject");
        SetOfBookName.ValueText = objects.getData("SetOfBookName");
        string headerId = Convert.ToString(objects.getData("SPPOA016")); //HEADER_ID
        string reqHeaderId = Convert.ToString(objects.getData("SPPOA017")); //PO_REQUISITION_HEADER_ID

        
        SPPOA003.ValueText = Convert.ToString(objects.getData("SPPOA003")); //申請日期
        SPPOA004.ValueText = Convert.ToString(objects.getData("SPPOA004")); //送審人員
        SPPOA004.doValidate();
        SPPOA005.ValueText = Convert.ToString(objects.getData("SPPOA005")); //部門代號
        SPPOA005.doValidate();
        SPPOA006.ValueText = Convert.ToString(objects.getData("SPPOA006")); //請購單單號
        SPPOA006.Display = false;
        hlSPPOA006.Text = SPPOA006.ValueText;
        //請購單Portal URI
        hlSPPOA006.NavigateUrl = "javascript:onViewPr(" + headerId + ")";
        SPPOA007.Value = Convert.ToString(objects.getData("SPPOA007")); //是否有附件
        
        SPPOA008.ValueText = Convert.ToString(objects.getData("SPPOA008")); //幣別
        SPPOA009.ValueText = Convert.ToString(objects.getData("SPPOA009")); //預估金額
        SPPOA010.ValueText = Convert.ToString(objects.getData("SPPOA010")); //說明
        SPPOA011.Value = Convert.ToString(objects.getData("SPPOA011")); //狀態
        SPPOA012.ValueText = Convert.ToString(objects.getData("SPPOA012")); //審核人員
        if (!SPPOA012.ValueText.Equals(""))
        {
            SPPOA012.doValidate();
        }
        SPPOA013.ValueText = Convert.ToString(objects.getData("SPPOA013")); //會簽人員一
        if (!SPPOA013.ValueText.Equals(""))
        {
            SPPOA013.doValidate();
        }
        SPPOA014.Value = Convert.ToString(objects.getData("SPPOA014")); //會簽人員二
        SPPOA015.ValueText = Convert.ToString(objects.getData("SPPOA015")); //會簽人員意見
        SPPOA016.Value = headerId;
        SPPOA017.Value = reqHeaderId;
        if (Convert.ToString(objects.getData("SPPOA018")).Equals("0"))
        {
            SPPOA018A.Checked = true;
        }
        else if (Convert.ToString(objects.getData("SPPOA018")).Equals("1"))
        {
            SPPOA018B.Checked = true;
        }
        else if (Convert.ToString(objects.getData("SPPOA018")).Equals("2"))
        {
            SPPOA018C.Checked = true;
        }
        else
        {
            
        }
        SPPOA019.ValueText = Convert.ToString(objects.getData("SPPOA019")); //通知人員
        if (!SPPOA019.ValueText.Equals(""))
        {
            SPPOA019.doValidate();
        }
        
        DataObjectSet detailSet = null;
        detailSet = objects.getChild("SPPOB");

        DataListLine.dataSource = detailSet;
        DataListLine.updateTable();

        bool isCanAppendAtta = false;
        if (actName.Equals("填表人") || actName.Equals("申請人"))
        {
            SPPOA012.ReadOnly = false;
            SPPOA013.ReadOnly = false;
            SPPOA018A.ReadOnly = false;
            SPPOA018B.ReadOnly = false;
            SPPOA018C.ReadOnly = false;
            SPPOA019.ReadOnly = false;
            if (actName.Equals("申請人"))
            {
                AddSignButton.Display = true;
            }
        }
        else if (actName.Equals("主管"))
        {
            AddSignButton.Display = true;
        }
        else if (actName.Equals("承辦人"))
        {
            AddSignButton.Display = true;
            isCanAppendAtta = true;
            SPPOA015.ReadOnly = false;
        }
        else if (actName.Equals("需求者"))
        {
            AddSignButton.Display = true;
        }

        if (SPPOA007.Value.Equals("Y") || actName.Equals("承辦人") || actName.Equals("承辦人主管"))
        {
            //附件URL
            url = sp.getParam("SMPReqDocUrl");
            ViewAttachUrl.Value = url;

            string title = "PO Requisitions-" + SPPOA006.ValueText;
            string privilege = "BeoALu2UGAI1";
            if (isCanAppendAtta)
            {
                privilege = "I0WeDl8vdQE1"; //有權限
            }
            ImgButtonAtta.Visible = true;
            url = "javascript:onViewAttach('REQ_HEADERS'," + reqHeaderId + ",'" + privilege + "','" + title + "');";
            ImgButtonAtta.PostBackUrl = url;
        }
        else
        {
            ImgButtonAtta.Visible = false;
        }

        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        string actName = Convert.ToString(getSession("ACTName"));
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            base.saveData(engine, objects);
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
        }

        objects.setData("SPPOA012", Convert.ToString(SPPOA012.ValueText));
        objects.setData("SPPOA013", Convert.ToString(SPPOA013.ValueText));
        objects.setData("SPPOA014", Convert.ToString(SPPOA014.Value));
        objects.setData("SPPOA015", Convert.ToString(SPPOA015.ValueText));
        
        string requisitionCategory = "";
        if (SPPOA018A.Checked)
        {
            objects.setData("SPPOA018", "0");
            requisitionCategory = "資訊類";
        }
        else if (SPPOA018B.Checked)
        {
            objects.setData("SPPOA018", "1");
            requisitionCategory = "其他類";
        }
        else if (SPPOA018C.Checked)
        {
            objects.setData("SPPOA018", "2");
            requisitionCategory = "工安類";
        }
        else
        {

        }
        //For MCloud
        //objects.setData("OriginatorUserName", SPPOA004.ReadOnlyValueText);
        //objects.setData("OriginatorDeptName", SPPOA005.ReadOnlyValueText);
        objects.setData("RequisitionCategory", requisitionCategory);
        //objects.setData("ReviewerUserName", SPPOA012.ReadOnlyValueText);
        //objects.setData("CcbUserName", SPPOA013.ReadOnlyValueText);
        //objects.setData("NotifierUserName", SPPOA019.ReadOnlyValueText);

        objects.setData("SPPOA019", Convert.ToString(SPPOA019.ValueText));

        if (actName.Equals("主管") || actName.Equals("承辦人") || actName.Equals("申請人2"))
        {
            setSession("IsAddSign", "AFTER");
        }

        //beforeSetFlow
        setSession("IsSetFlow", "Y");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result=true;
        try
        {
            string actName = Convert.ToString(getSession("ACTName"));
            string buyers = getBuyers(engine, objects);

            if (buyers.Equals(""))
            {
                if (SPPOA013.ValueText.Equals(""))
                {
                    //pushErrorMessage(SPPO013lb.Text + "欄位不可為空，會簽人員請選擇採購員!");
                    //result = false;
                }
            }

            if (actName.Equals("填表人") || actName.Equals("申請人"))
            {
                if (!SPPOA018A.Checked && !SPPOA018B.Checked && !SPPOA018C.Checked)
                {
                    pushErrorMessage(lblSPPOA018.Text + "欄位不可為空!");
                    result = false;
                }
            }
        }
        catch (Exception ex)
        {
            pushErrorMessage(ex.Message);
            result = false;
        }

        return result;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        //si.ownerID = (string)Session["UserID"];
        si.ownerID = SPPOA004.ValueText;
        //si.ownerName = (string)Session["UserName"];
        si.ownerName = SPPOA004.ReadOnlyValueText;
        depData = getDeptInfo(engine, SPPOA004.GuidValueText);
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        string sql = "";
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        //填表人
        string creatorId = si.fillerID;
        //申請人員
        string originatorId = currentObject.getData("SPPOA004");

        if (string.IsNullOrEmpty(creatorId))
        {
            creatorId = originatorId;
        }

        //需求者
        string requestors = "";
        //主管
        string managers = "";
        DataObjectSet set = currentObject.getChild("SPPOB");
        for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
        {
            string requestorId = set.getAvailableDataObject(i).getData("SPPOB012");
            if (!requestorId.Equals(originatorId))
            {
                if (requestors.IndexOf(requestorId) == -1)
                {
                    requestors += requestorId + ";";
                }
            }

            if (managers.Equals(""))
            {
                string[] results = base.getUserGUID(engine, requestorId);
                string requestorGUID = results[0];
                results = base.getUserManagerInfo(engine, requestorGUID);
                string managerId = results[1];
                managers = managerId;
            }
        }

        if (managers.Equals(""))
        {
            string[] results = base.getUserGUID(engine, creatorId);
            string creatorGUID = results[0];
            results = base.getUserManagerInfo(engine, creatorGUID);
            string managerId = results[1];
            managers = managerId;
        }

        //bypass originator, requestor
        sql = "select CheckValue3 from SmpFlowInspect where FormId='" + ProcessPageID + "' and CheckField1='DeptId' and CheckValue1='" + si.ownerOrgID + "' and Status='Y'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            string byPassRequestor = ds.Tables[0].Rows[0][0].ToString();

            if (byPassRequestor.Equals("Y"))
            {
                requestors = "";
            }
        }


        //審核人
        string checkby = currentObject.getData("SPPOA012");
        
        //會簽
        string ccb = currentObject.getData("SPPOA013");
        string undertaker = "";
        string deptId = si.ownerOrgID;
        string category = currentObject.getData("SPPOA018");
        if (!deptId.Equals("C2200") && !deptId.Equals("GSC2200"))
        {
            //if (SPPOA018A.Checked)
            if (category.Equals("0"))
            {
                string[][] groupUsers = base.getGroupdUser(engine, "SPERP003-IT");
                string userId = groupUsers[0][0];
                undertaker = userId;
            }
        }
        //if (SPPOA018C.Checked)
        if (category.Equals("2"))
        {
            string[][] groupUsers = base.getGroupdUser(engine, "SPERP003-工安");
            string userId = groupUsers[0][0];
            undertaker = userId;
        }
        //通知
        string notifiers = "SPERP003-NOTI";
        xml = "<SPERP003>";
        xml += "<creator dataType=\"java.lang.String\">" + creatorId + "</creator>";
        xml += "<originator dataType=\"java.lang.String\">" + originatorId + "</originator>";
        xml += "<requestors dataType=\"java.lang.String\">" + requestors + "</requestors>";
        xml += "<checkby dataType=\"java.lang.String\">" + checkby + "</checkby>";
        xml += "<managers dataType=\"java.lang.String\">" + managers + "</managers>";
        xml += "<ccb dataType=\"java.lang.String\">" + ccb + "</ccb>";
        xml += "<undertaker dataType=\"java.lang.String\">" + undertaker + "</undertaker>";
        xml += "<notifiers dataType=\"java.lang.String\">" + notifiers + "</notifiers>";
        xml += "</SPERP003>";

        param["SPERP003"] = xml;
        return "SPERP003";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="setFlowXml"></param>
    /// <returns></returns>
    protected override string beforeSetFlow(AbstractEngine engine, string setFlowXml)
    {
        return setFlowXml;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void beforeSend(AbstractEngine engine, DataObject currentObject)
    {
        base.beforeSend(engine, currentObject);
    }

    /// <summary>
    /// 簽核前程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        string signProcess = Convert.ToString(Session["signProcess"]);
        //System.IO.StreamWriter sw = null;
        try
        {
            string actName = Convert.ToString(getSession("ACTName"));
            string xml = "";
            if (signProcess.Equals("Y"))
            {
                if (actName.Equals("主管"))
                {
                    DataObject currentObject = (DataObject)getSession("currentObject");
                    DataObjectSet set = currentObject.getChild("SPPOB");
                    for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
                    {
                        string requestorId = set.getAvailableDataObject(i).getData("SPPOB012");
                    }
                    //if (deptIds.IndexOf("R5500") >= 0 || deptIds.IndexOf("R5700") >= 0)
                    //{
                    //    string userId = "4327";
                    //    string[] values = base.getUserGUID(engine, userId);
                    //    xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    //    xml += "       <performers>";
                    //    xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    //    xml += "                <OID>" + values[0] + "</OID>";
                    //    xml += "                <participantType><value>HUMAN</value></participantType>";
                    //    xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    //    xml += "        </performers>";
                    //    xml += "        <multiUserMode><value>FOR_EACH</value></multiUserMode>";
                    //    xml += "        <name>處長</name>";
                    //    xml += "        <performType><value>NORMAL</value></performType>";
                    //    xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    //}

                    //if (deptIds.IndexOf("R5500") >= 0 || deptIds.IndexOf("R5700") >= 0
                    //    || deptIds.IndexOf("R5600") >= 0 || deptIds.IndexOf("RH520") >= 0 || deptIds.IndexOf("RH540") >= 0 || deptIds.IndexOf("RH510") >= 0 || deptIds.IndexOf("RH500") >= 0)
                    //{
                    //    string userId = "2365";
                    //    string[] values = base.getUserGUID(engine, userId);
                    //    xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    //    xml += "       <performers>";
                    //    xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    //    xml += "                <OID>" + values[0] + "</OID>";
                    //    xml += "                <participantType><value>HUMAN</value></participantType>";
                    //    xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                    //    xml += "        </performers>";
                    //    xml += "        <multiUserMode><value>FOR_EACH</value></multiUserMode>";
                    //    xml += "        <name>副總</name>";
                    //    xml += "        <performType><value>NORMAL</value></performType>";
                    //    xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                    //}

                    //if (!xml.Equals(""))
                    //{
                    //    xml = "<list>" + xml + "</list>";
                    //    return xml;
                    //}
                }
                else if (actName.Equals("承辦人"))
                {
                    string managerGUID = "";
                    if (SPPOA018A.Checked)
                    {
                        string[][] groupUsers = base.getGroupdUser(engine, "SPERP003-IT");
                        string userId = groupUsers[0][0];
                        string[] userValues = base.getUserGUID(engine, userId);
                        string[] managerValues = base.getUserManagerInfo(engine, userValues[0]);
                        managerGUID = managerValues[0];
                    }
                    if (!managerGUID.Equals(""))
                    {
                        xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                        xml += "       <performers>";
                        xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "                <OID>" + managerGUID + "</OID>";
                        xml += "                <participantType><value>HUMAN</value></participantType>";
                        xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "        </performers>";
                        xml += "        <multiUserMode><value>FOR_EACH</value></multiUserMode>";
                        xml += "        <name>承辦人主管</name>";
                        xml += "        <performType><value>NORMAL</value></performType>";
                        xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                        if (!xml.Equals(""))
                        {
                            xml = "<list>" + xml + "</list>";
                            return xml;
                        }
                    }
                }
                else if (actName.Equals("申請人2")) //核准請購單
                {
                    string prNumber = SPPOA006.ValueText;
                    DataObject currentObject = (DataObject)getSession("currentObject");
                    string status = currentObject.getData("SPPOA011");
                    string headerId = currentObject.getData("SPPOA016");
                    string notifier = currentObject.getData("SPPOA019");


                    string strOpinion = getWorkflowOpinion(engine);
                    if (status.Equals("IMPORTED"))
                    {
                        approvePr(prNumber, strOpinion);
                    }
                    else
                    {
                        reApprovePr(headerId, strOpinion);
                    }

                    if (!Convert.ToString(notifier).Equals(""))
                    {
                        string[] values = base.getUserGUID(engine, notifier);

                        xml += "    <com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";
                        xml += "       <performers>";
                        xml += "            <com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "                <OID>" + values[0] + "</OID>";
                        xml += "                <participantType><value>HUMAN</value></participantType>";
                        xml += "            </com.dsc.nana.data_transfer.ActivityDefPerformerForClientListDTO>";
                        xml += "        </performers>";
                        xml += "        <multiUserMode><value>FOR_EACH</value></multiUserMode>";
                        xml += "        <name>通知</name>";
                        xml += "        <performType><value>NOTICE</value></performType>";
                        xml += "    </com.dsc.nana.data_transfer.ActivityDefinitionForClientListDTO>";

                        if (!xml.Equals(""))
                        {
                            xml = "<list>" + xml + "</list>";
                            return xml;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            
        }
        return addSignXml;
    }

    /// <summary>
    /// 簽核後程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
        string signProcess = Convert.ToString(Session["signProcess"]);
        try
        {
            if (result.Equals("同意") || signProcess.Equals("Y"))
            {
                string actName = Convert.ToString(getSession("ACTName"));
                if (actName == "申請人2")
                {
                    if (result.Equals("Y"))
                    {
                        //updateSourceStatus(prNumber, "Close");
                        //approvePr(prNumber, signNote);
                    }
                }
            }
            else if (result.Equals("不同意") || signProcess.Equals("N")) //不同意
            {
                updateSourceStatus("Terminate", currentObject);
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }

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
                //SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
                //call oracle function set object workflow status to 'Reject'
                //base.terminateThisProcess(si.fillerID);
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
    /// 結案程序
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        //call oracle function set object workflow status to 'close'
        //string signProcess = Convert.ToString(Session["signProcess"]);
        if (result.Equals("Y"))
        {
            //updateSourceStatus(prNumber, "Close");
            //approvePr(prNumber, signNote);
        }
        else
        {
            updateSourceStatus("Reject", currentObject);
        }
        base.afterApprove(engine, currentObject, result);
    }

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string status, DataObject currentObject)
    {
        string result = "";
        string prNumber = "";
        if (currentObject != null)
        {
            prNumber = currentObject.getData("SPPOA006");
        }
        else
        {
            prNumber = SPPOA006.ValueText;
        }

        if (!prNumber.Equals(""))
        {
            result = updateSourceStatus(prNumber, status);
        }
        return result;
    }

    /// <summary>
    /// 更新需求單狀態
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="number"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private string updateSourceStatus(string number, string status)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "SMP_IMPORT_PR.UPDATE_WF_STATUS";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_PR_NUMBER", OracleType.VarChar).Value = number;
            objCmd.Parameters.Add("P_WF_STATUS", OracleType.VarChar).Value = status;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    /// <summary>
    /// 核准PR
    /// </summary>
    /// <param name="number"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    private string approvePr(string number, string note)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "SMP_IMPORT_PR.PR_APPROVE";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_PR_NUMBER", OracleType.VarChar).Value = number;
            objCmd.Parameters.Add("P_NOTE", OracleType.VarChar).Value = note;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    /// <summary>
    /// 重新核准PR
    /// </summary>
    /// <param name="number"></param>
    /// <param name="note"></param>
    /// <returns></returns>
    private string reApprovePr(string number, string note)
    {
        string result = "";
        OracleConnection conn = null;
        try
        {
            conn = base.getErpPortalConn();
            OracleCommand objCmd = new OracleCommand();
            objCmd.Connection = conn;
            objCmd.CommandText = "SMP_IMPORT_PR.PR_REAPPROVE";
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("P_HEADER_ID", OracleType.VarChar).Value = number;
            objCmd.Parameters.Add("P_NOTE", OracleType.VarChar).Value = note;
            objCmd.Parameters.Add("return_value", OracleType.VarChar, 1024).Direction = ParameterDirection.ReturnValue;
            conn.Open();
            objCmd.ExecuteNonQuery();
            result = Convert.ToString(objCmd.Parameters["return_value"].Value);
        }
        catch (Exception e)
        {
            base.writeLog(e);
            throw new Exception(e.StackTrace);
        }
        finally
        {
            conn.Close();
        }
        return result;
    }

    /// <summary>
    /// 取得簽核意見
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    private string getWorkflowOpinion(AbstractEngine engine) 
    {
        string strOpinion = "";
        try
        {
            string sheetNo = Convert.ToString(getSession(PageUniqueID, "SheetNo"));
            string sql = "select wi.workItemName,u.userName,wi.executiveComment ";
            sql += "from [NaNa].dbo.WorkItem wi, [NaNa].dbo.ProcessInstance pi, [WebFormPT].[dbo].SMWYAAA ya, [NaNa].dbo.Users u ";
            sql += "where SMWYAAA002='" + sheetNo + "' and wi.contextOID=pi.contextOID and pi.serialNumber=ya.SMWYAAA005 and u.OID=wi.performerOID ";
            sql += "order by wi.createdTime asc";
            DataSet ds = engine.getDataSet(sql, "TEMP");
            int rows = ds.Tables[0].Rows.Count;
            bool needGotComment = false;
            for (int i = 0; i < rows; i++)
            {
                string actName = ds.Tables[0].Rows[i][0].ToString();
                string userName = ds.Tables[0].Rows[i][1].ToString();
                string comment = ds.Tables[0].Rows[i][2].ToString();
                if (actName.Equals("主管") || actName.Equals("處級主管") || actName.Equals("會簽"))
                {
                    needGotComment = true;
                }
                if (needGotComment)
                {
                    if (actName.Equals("會簽"))
                    {
                        strOpinion += actName + "(" + userName + "): " + comment.Replace("\n", "");
                    }
                    else
                    {
                        strOpinion += userName + ": " + comment.Replace("\n", "");
                    }
                }
            }
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return strOpinion;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected string getBuyers(AbstractEngine engine, DataObject currentObject)
    {
        string buyers = "";
        DataObjectSet set = currentObject.getChild("SPPOB");
        for (int i = 0; i < set.getAvailableDataObjectCount(); i++)
        {
            string buyerId = set.getAvailableDataObject(i).getData("SPPOB023");
            if (buyers.IndexOf(buyerId) == -1)
            {
                buyers += buyerId + ";";
            }
        }
        return buyers;
    }
    protected void SPPOA012_BeforeClickButton()
    {
        SPPOA012.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
    }
    protected void SPPOA013_BeforeClickButton()
    {
        SPPOA013.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
    }
    protected void SPPOA019_BeforeClickButton()
    {
        SPPOA019.whereClause = "(leaveDate >= getDate() or leaveDate='' or leaveDate is null)";
    }
}
