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
using System.Xml;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using com.dsc.flow.data;
using com.dsc.flow.server;
using com.dsc.kernal.agent;
using WebServerProject.flow.SMWY;
using WebServerProject.flow.SMWG;
using WebServerProject;
using DSCWebControl;

//public partial class SmpProgram_Maintain_SPSYS001M_Detail : BaseWebUI.GeneralWebPage
public partial class SmpProgram_Maintain_SPSYS001M_Detail : BaseWebUI.DataListSaveForm
//public partial class SmpProgram_Maintain_SPSYS001M_Detail : BaseWebUI.DataListInlineForm
{
    protected override void OnInit(EventArgs e)
    {
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";
        maintainIdentity = "SMWM";
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
    }
	
    /// <summary>
    /// 將NULL或者零長度字串調整成為&nbsp;
    /// </summary>
    /// <param name="ori">原始字串</param>
    /// <returns>調整後字串</returns>
    private string fixNbspS(string ori)
    {
        ori = fixNullS(ori);
        if (ori.Equals(""))
        {
            return "&nbsp;";
        }
        else
        {
            return ori;
        }
    }
    /// <summary>
    /// 將輸入字串為null的調整成為零長度字串
    /// </summary>
    /// <param name="ori">輸入字串</param>
    /// <returns>調整後字串</returns>
    private string fixNullS(string ori)
    {
        if (ori == null)
        {
            return "";
        }
        else
        {
            return ori;
        }
    }
    /// <summary>
    /// 取得簽核意見列表
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="processSerialNumber">流程實例序號</param>
    /// <param name="opinionType">0:顯示所有流程;1:僅顯示已簽核流程</param>
    /// <returns>意見列表</returns>
    private string getAllSignOpinion(System.Web.HttpServerUtility server, bool mDebugPage, AbstractEngine engine, string processSerialNumber, string opinionType)
    {
        //************************************************************************
        //第一步: 透過fetchFullProcInstanceWithSerialNoXML方法取得此流程的XML資訊
        //************************************************************************
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = server.MapPath("~/LogFolder/" + fname + "_flowdata.log");
        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, mDebugPage);

        string opinionXML = adp.fetchFullProcInstanceWithSerialNoXML(processSerialNumber);

        adp.logout();

        //第二步: 將XML轉換成為PerformDetail物件陣列(包含ReassignWorkItem陣列)
        ArrayList performDetailArray = new ArrayList();
        ArrayList temp2 = new ArrayList();

        XMLProcessor xp = new XMLProcessor(opinionXML, 1);
        string abortComment = "";
        bool isAbort = false;
        if (xp.selectSingleNode("com.dsc.nana.services.webservice.ProcessInfo/state").InnerText.Equals("closed.aborted"))
        {
            isAbort = true;
        }

        if (xp.selectSingleNode("com.dsc.nana.services.webservice.ProcessInfo/abortComment") != null)
        {
            abortComment = xp.selectSingleNodeText("com.dsc.nana.services.webservice.ProcessInfo/abortComment");
        }

        //處理ActInstanceInfo
        XmlNodeList xnl = xp.selectAllNodes("com.dsc.nana.services.webservice.ProcessInfo/actInstanceInfos/com.dsc.nana.services.webservice.ActInstanceInfo");
        foreach (XmlNode actInfo in xnl)
        {
            //處理performInfo
            XmlNodeList xnl2 = actInfo.SelectNodes("performInfos/com.dsc.nana.services.webservice.PerformInfo");
            foreach (XmlNode performInfo in xnl2)
            {
                //處理performDetail
                XmlNodeList xnl3 = performInfo.SelectNodes("performDetails/com.dsc.nana.services.webservice.PerformDetail");
                foreach (XmlNode performDetail in xnl3)
                {
                    PerformDetail pd = new PerformDetail();
                    pd.performType = actInfo.SelectSingleNode("performType").InnerText;
                    pd.activityName = actInfo.SelectSingleNode("activityName").InnerText;
                    pd.performerName = performDetail.SelectSingleNode("performerName").InnerText;
                    string rs = "";
                    if (performDetail.SelectSingleNode("comment") != null)
                    {
                        rs = performDetail.SelectSingleNode("comment").InnerText;
                    }
                    if (rs.Equals(""))
                    {
                        pd.executiveResult = "";
                        pd.comment = "";
                    }
                    else
                    {
                        string[] rss = rs.Split(new string[] { "##" }, StringSplitOptions.None);
                        if (rss.Length == 2)
                        {
                            pd.executiveResult = rss[0];
                            pd.comment = rss[1];
                        }
                        else
                        {
                            pd.executiveResult = "";
                            pd.comment = rs;
                        }
                    }
                    if (performDetail.SelectSingleNode("performedTime") != null)
                    {
                        if (!performDetail.SelectSingleNode("performedTime").InnerText.Equals(""))
                        {
                            pd.performedTime = DateTimeUtility.convertDateTimeToString(DateTime.Parse(performDetail.SelectSingleNode("performedTime").InnerText));
                        }
                        else
                        {
                            pd.performedTime = "";
                        }
                    }
                    else
                    {
                        pd.performedTime = "";
                    }
                    pd.state = performInfo.SelectSingleNode("state").InnerText;
                    pd.notifiedName = performDetail.SelectSingleNode("notifiedName").InnerText;
                    if (!performDetail.SelectSingleNode("createdTime").InnerText.Equals(""))
                    {
                        pd.createdTime = DateTimeUtility.convertDateTimeToString(DateTime.Parse(performDetail.SelectSingleNode("createdTime").InnerText));
                    }
                    else
                    {
                        pd.createdTime = "";
                    }

                    if (pd.createdTime.Equals(""))
                    {
                        pd.processTime = "0";
                    }
                    else
                    {
                        DateTime startdt = DateTime.Parse(pd.createdTime);
                        DateTime enddt;
                        if (pd.performedTime.Equals(""))
                        {
                            enddt = DateTime.Now;
                        }
                        else
                        {
                            enddt = DateTime.Parse(pd.performedTime);
                        }
                        TimeSpan ts = enddt.Subtract(startdt);
                        pd.processTime = Utility.Round(ts.TotalHours, 2).ToString();
                    }

                    //處理reassignment
                    temp2 = new ArrayList();
                    XmlNodeList xnl4 = performDetail.SelectNodes("reassignWorkItemRecords/com.dsc.nana.data__transfer.ReassignmentInfoForListDTO");
                    foreach (XmlNode workItemRecord in xnl4)
                    {
                        ReAssignedWorkItem rw = new ReAssignedWorkItem();
                        rw.reassignmentType = workItemRecord.SelectSingleNode("reassignmentType/value").InnerText;
                        rw.reassignFromUserId = workItemRecord.SelectSingleNode("reassignFromUserId").InnerText;
                        rw.reassignFromUserName = workItemRecord.SelectSingleNode("reassignFromUserName").InnerText;
                        rw.reassignToUserId = workItemRecord.SelectSingleNode("reassignToUserId").InnerText;
                        rw.reassignToUserName = workItemRecord.SelectSingleNode("reassignToUserName").InnerText;
                        //start 2009/04/30 hjlin
                        if (workItemRecord.SelectSingleNode("comment") == null)
                        {
                            rw.comment = "";
                        }
                        else
                        {
                            rw.comment = workItemRecord.SelectSingleNode("comment").InnerText;
                        }
                        //end 2009/04/30 hjlin
                        rw.reassignedTime = DateTimeUtility.convertDateTimeToString(DateTime.Parse(workItemRecord.SelectSingleNode("reassignedTime").InnerText));

                        temp2.Add(rw);
                    }
                    ReAssignedWorkItem[] ary = new ReAssignedWorkItem[temp2.Count];
                    for (int x = 0; x < temp2.Count; x++)
                    {
                        ary[x] = (ReAssignedWorkItem)temp2[x];
                    }
                    pd.record = ary;

                    performDetailArray.Add(pd);
                }
            }
        }

        //第三步: 排序(使用插入排序法)
        ArrayList newPerformDetailArray = new ArrayList();
        for (int i = 0; i < performDetailArray.Count; i++)
        {
            PerformDetail cur = (PerformDetail)performDetailArray[i];
            if (newPerformDetailArray.Count == 0)
            {
                newPerformDetailArray.Add(cur);
            }
            else
            {
                int index = -1;
                for (int j = 0; j < newPerformDetailArray.Count; j++)
                {
                    PerformDetail newd = (PerformDetail)newPerformDetailArray[j];
                    DateTime newdt = DateTime.Parse(newd.createdTime);
                    DateTime curdt = DateTime.Parse(cur.createdTime);
                    if (newdt.CompareTo(curdt) > 0)
                    {
                        index = j;
                        break;
                    }
                    else if (newdt.Equals(curdt))
                    {
                        if (newd.performedTime.Equals(""))
                        {
                            newdt = DateTime.MaxValue;
                        }
                        else
                        {
                            newdt = DateTime.Parse(newd.performedTime);
                        }
                        if (cur.performedTime.Equals(""))
                        {
                            curdt = DateTime.MaxValue;
                        }
                        else
                        {
                            curdt = DateTime.Parse(cur.performedTime);
                        }

                        if (newdt.CompareTo(curdt) >= 0)
                        {
                            index = j;
                            break;
                        }
                    }
                }
                if (index == -1)
                {
                    newPerformDetailArray.Add(cur);
                }
                else
                {
                    newPerformDetailArray.Insert(index, cur);
                }
            }
        }

        //第四步: 再將PerformDetail物件陣列轉換成為HTML
        string html = "";

        if (isAbort)
        {
            html += "<table width=100% border=0  cellspacing=0 cellpadding=2 style={border-left-style:solid;border-top-style:solid;border-width:1px} class=OpinionBorder>";
            html += "<tr>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead width=100px>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str1", "流程撤銷意見") + "</td>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(abortComment) + "</td>";
            html += "</tr>";
            html += "</table>";
            html += "<br>";
        }

        html += "<table width=100% border=0  cellspacing=0 cellpadding=2 style={border-left-style:solid;border-top-style:solid;border-width:1px} class=OpinionBorder>";
        html += "<tr>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>&nbsp;</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str2", "類型") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str3", "關卡名稱") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str4", "處理者") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str5", "處理結果") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str6", "處理意見") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str7", "處理時間") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str8", "狀態") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str9", "轉寄") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str10", "開始時間") + "</td>";
        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str11", "處理時數") + "</td>";
        html += "</tr>";


        for (int i = 0; i < newPerformDetailArray.Count; i++)
        {
            PerformDetail pd = (PerformDetail)newPerformDetailArray[i];
            html += "<tr>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>&nbsp;</td>";
            if (pd.performType.Equals("NORMAL"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str12", "一般") + "</td>";
            }
            else if (pd.performType.Equals("NOTICE"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str13", "通知") + "</td>";
            }
            else
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str14", "執行") + "</td>";
            }
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.activityName) + "</td>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.performerName) + "</td>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.executiveResult) + "</td>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.comment) + "</td>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.performedTime) + "</td>";
            //open.not_running.not_started:未開始; open.running:進行中; open.not_running.suspended:已暫停; closed.completed:已完成; closed.aborted:已撤銷; closed.terminated:已中止
            if (pd.state.Equals("open.not_running.not_started"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str15", "未開始") + "</td>";
            }
            else if (pd.state.Equals("open.running"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str16", "進行中") + "</td>";
            }
            else if (pd.state.Equals("open.running.not_performed"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str16", "進行中") + "</td>";
            }
            else if (pd.state.Equals("open.running.performing"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str16", "進行中") + "</td>";
            }
            else if (pd.state.Equals("open.not_running.suspended"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str17", "已暫停") + "</td>";
            }
            else if (pd.state.Equals("closed.completed"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str18", "已完成") + "</td>";
            }
            else if (pd.state.Equals("closed.aborted"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str19", "已撤銷") + "</td>";
            }
            else if (pd.state.Equals("closed.terminated"))
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str20", "已中止") + "</td>";
            }
            else
            {
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.state) + "</td>";
            }
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.notifiedName) + "</td>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.createdTime) + "</td>";
            html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(pd.processTime) + "</td>";
            html += "</tr>";

            if (pd.record.Length > 0)
            {
                html += "<tr>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none;border-right-style:none} class=OpinionDetail colspan=2>&nbsp;</td>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail colspan=9>";


                html += "<table border=0  width=90% cellspacing=0 cellpadding=2 style={border-left-style:solid;border-top-style:solid;border-width:1px} class=OpinionBorder>";
                html += "<tr>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str21", "類型") + "</td>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str22", "處理者") + "</td>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str23", "受託者") + "</td>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str24", "處理意見") + "</td>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str25", "處理時間") + "</td>";
                html += "</tr>";

                for (int j = 0; j < pd.record.Length; j++)
                {
                    ReAssignedWorkItem rw = pd.record[j];
                    html += "<tr>";
                    /// 轉派類型.
                    /// 0:直接轉派
                    /// 1:系統代理人轉派
                    /// 2:系統逾時轉派
                    /// 3:管理員代理轉派
                    /// 4:負責人代理轉派
                    /// 5:工作取回
                    /// 6:工作轉派
                    /// 7:管理員工作轉派
                    /// 8:負責人工作轉派
                    /// 9:系統永久代理轉派
                    if (rw.reassignmentType.Equals("0"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str26", "直接轉派") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("1"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str27", "系統代理人轉派") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("2"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str28", "系統逾時轉派") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("3"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str29", "管理員代理轉派") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("4"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str30", "負責人代理轉派") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("5"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str31", "工作取回") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("6"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str32", "工作轉派") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("7"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str33", "系統工作轉派") + "</td>";
                    }
                    else if (rw.reassignmentType.Equals("8"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str34", "負責人工作轉派") + "</td>";
                    }
                    //start 2009/04/30 hjlin
                    else if (rw.reassignmentType.Equals("9"))
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwm_detail_aspx.language.ini", "message", "str35", "系統永久代理轉派") + "</td>";
                    }
                    else
                    {
                        html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>&nbsp;</td>";
                    }
                    //end 2009/04/30 hjlin
                    html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(rw.reassignFromUserId + "_" + rw.reassignFromUserName) + "</td>";
                    html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(rw.reassignToUserId + "_" + rw.reassignToUserName) + "</td>";
                    html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(rw.comment) + "</td>";
                    html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + fixNbspS(rw.reassignedTime) + "</td>";
                    html += "</tr>";
                }

                html += "</table>";

                html += "</td>";
                html += "</tr>";

            }
        }

        html += "</table>";
        html += "</span>";

        return html;
    }
    
    private void bypassActivity(AbstractEngine engine, string actOID)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

        adp.bypassActivity(actOID);

        adp.logout();
    }

    private void acceptWorkItem(AbstractEngine engine, string pUserID, string pWorkItemOID)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

        adp.acceptWorkItem(pUserID, pWorkItemOID);

        adp.logout();
    }
    private void managementReassignWorkItem(AbstractEngine engine, string pAcceptorOID, string pWorkItemOID, string pReassignComment)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

        adp.managementReassignWorkItem(pAcceptorOID, pWorkItemOID, pReassignComment);

        adp.logout();
    }
    private void managementChangeWorkItemOwner(AbstractEngine engine, string pAcceptorOID, string pWorkItemOID, string pReassignComment)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

        adp.managementChangeWorkItemOwner(pAcceptorOID, pWorkItemOID, pReassignComment);

        adp.logout();
    }
    private RunningWorkItem[] fetchRunningWorkItemOIDs(AbstractEngine engine, string flowSerialNo)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

        RunningWorkItem[] rw=adp.fetchRunningWorkItemOIDs(flowSerialNo);

        adp.logout();
        return rw;
    }


    /// <summary>
    /// 取得flowSerialNo流程實例序號的流程圖, 存放於localFilePath所指定的檔案
    /// </summary>
    /// <param name="engine">資料庫連線物件</param>
    /// <param name="flowSerialNo">流程實例序號</param>
    /// <param name="localFilePath">存放暫時路徑檔案名稱</param>
    private void fetchFlowDiagram(AbstractEngine engine, string flowSerialNo, string localFilePath)
    {
        SysParam sp = new SysParam(engine);
        string flowType = sp.getParam("FlowAdapter");
        string con1 = sp.getParam("NaNaWebService");
        string con2 = sp.getParam("DotJWebService");
        string account = sp.getParam("FlowAccount");
        string password = sp.getParam("FlowPassword");

        FlowFactory ff = new FlowFactory();
        AbstractFlowAdapter adp = ff.getAdapter(flowType);
        adp.retryTimes = int.Parse(Session["FlowProcessCount"].ToString());
        adp.retryWaitingTime = int.Parse(Session["FlowProcessWaiting"].ToString());

        string fname = DateTimeUtility.getSystemTime2(null).Substring(0, 13).Replace("/", "").Replace(" ", "_");
        fname = Server.MapPath("~/LogFolder/" + fname + "_flowdata.log");

        adp.init(con1, con2, account, password, "", "127.0.0.1", "127.0.0.1:8080", "EF2KWeb", fname, (bool)Session["DebugPage"]);

        adp.fetchWorkFlowDiagram(flowSerialNo, localFilePath);

        adp.logout();
    }
    
	protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
		DataSet ds = null;
		string processSerialNumber = "";
		string opinionType = "SHOW_ALL";
		string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
		AbstractEngine engine = factory.getEngine(engineType, connectString);
		
		string strGUID = objects.getData("GUID");
		
        ds = engine.getDataSet("select SMWYAAA005 From SMWYAAA where SMWYAAA019='" + strGUID + "' ", "TEMP");
        if (ds.Tables[0].Rows.Count > 0)
        {
            processSerialNumber = ds.Tables[0].Rows[0][0].ToString();
        }
		
		string html = getAllSignOpinion(Page.Server, (bool)Session["DebugPage"], engine, processSerialNumber, opinionType);
					
		OpinionList.Text = html;
		
		string fn = IDProcessor.getID("");
        string localFilePath = Server.MapPath("~/tempFolder/" + fn + ".jpg");
        fetchFlowDiagram(engine, processSerialNumber, localFilePath);
					
        string imageURL = Request.ApplicationPath + "/tempFolder/" + fn + ".jpg";
		FlowImage.ImageUrl = imageURL;
    }
	
	private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            //string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = "SPSYS001 - " + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SPSYS001M.log", true, System.Text.Encoding.Default);
            sw.WriteLine(line);
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
            }
        }
    }
}
