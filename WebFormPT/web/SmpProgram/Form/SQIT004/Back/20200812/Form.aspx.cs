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
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.agent;
using com.dsc.flow.data;
using System.Text;
using System.IO;
using System.Net;

public partial class Program_SCQ_Form_SQIT004_Form : SmpBasicFormPage
{
    protected override void init()
    {
        ProcessPageID = "SQIT004";
        AgentSchema = "WebServerProject.form.SQIT004.SQIT004Agent";
        ApplicationID = "SYSTEM";
        ModuleID = "EASYFLOW";
    }

    /// <summary>
    /// 初始化畫面元件。初始化資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

        SheetNo.Display = false;
        Subject.Display = false;
        //改變工具列順序
        base.initUI(engine, objects);
    }

    /// <summary>
    /// 將資料由資料物件填入畫面元件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        //顯示單號
        base.showData(engine, objects);

        SheetNo.ValueText = objects.getData("SheetNo");
        Subject.ValueText = objects.getData("Subject");
        RqNO.ValueText = objects.getData("RqNO");
        version.ValueText = objects.getData("version");
        Creator.ValueText = objects.getData("Creator");
        PartNo.ValueText = objects.getData("PartNo");
        RqDate.ValueText = objects.getData("RqDate");
        RqPlanDate.ValueText = objects.getData("RqPlanDate");
        RqOwner.ValueText = objects.getData("RqOwner");
        BU.ValueText = objects.getData("BU");
        RqType.ValueText = objects.getData("RqType");
        Subject1.ValueText = objects.getData("Subject1");
        RqConent.ValueText = objects.getData("RqConent");
        RqLv.ValueText = objects.getData("RqLv");
        DevlopOwner.ValueText = objects.getData("DevlopOwner");
        RqMISDate.ValueText = objects.getData("RqMISDate");
        PlanHour.ValueText = objects.getData("PlanHour");
        DevlopHour.ValueText = objects.getData("DevlopHour");
        PlanCost.ValueText = objects.getData("PlanCost");

        //顯示發起資料


        ShowHistory(RqNO.ValueText);

    }

    /// <summary>
    /// 資料由畫面元件填入資料物件。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            //顯示要Save的資料
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("SheetNo", IDProcessor.getID("SheetNo"));
            objects.setData("Subject", Subject.ValueText);
            objects.setData("RqNO", RqNO.ValueText);
            objects.setData("version", version.ValueText);
            objects.setData("Creator", Creator.ValueText);
            objects.setData("PartNo", PartNo.ValueText);
            objects.setData("RqDate", RqDate.ValueText);
            objects.setData("RqPlanDate", RqPlanDate.ValueText);
            objects.setData("RqOwner", RqOwner.ValueText);
            objects.setData("BU", BU.ValueText);
            objects.setData("RqType", RqType.ValueText);
            objects.setData("Subject1", Subject1.ValueText);
            objects.setData("RqConent", RqConent.ValueText);
            objects.setData("RqLv", RqLv.ValueText);
            objects.setData("DevlopOwner", DevlopOwner.ValueText);
            objects.setData("RqMISDate", RqMISDate.ValueText);
            objects.setData("PlanHour", PlanHour.ValueText);
            objects.setData("DevlopHour", DevlopHour.ValueText);
            objects.setData("PlanCost", PlanCost.ValueText);
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");


            //產生單號並儲存至資料物件
            base.saveData(engine, objects);

        }
    }

    /// <summary>
    /// 畫面資料稽核。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;
        //新增判斷資料

        if (Subject.ValueText.Equals(""))
        {
            //values = base.getUserInfo(engine, RequestList.ValueText);
            string subject = "【 資訊需求申請單 】";
            if (Subject.ValueText.Equals(""))
            {
                Subject.ValueText = subject;
                // objects.setData("Subject", subject);
            }
        }

        return result;
    }

    /// <summary>
    /// 初始化SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo initSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        return getSubmitInfo(engine, objects, si);
    }
    
    /// <summary>
    /// 設定SubmitInfo資料結構。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="objects"></param>
    /// <param name="si"></param>
    /// <returns></returns>
    protected override SubmitInfo getSubmitInfo(AbstractEngine engine, DataObject objects, SubmitInfo si)
    {
        string[] depData = getDeptInfo(engine, (string)Session["UserGUID"]);

        si.fillerID = (string)Session["UserID"];//填表人
        si.fillerName = (string)Session["UserName"];
        si.fillerOrgID = depData[0];
        si.fillerOrgName = depData[1];
        si.ownerID = (string)Session["UserID"];//表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 設定自動編碼格式所需變數值。
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
    /// 設定流程變數。目前主要是用來傳遞流程所需要的變數值。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        string qhcj = RqLv.ValueText;
        //writeLog("qhcj:"+qhcj);


        xml += "<SQIT004>";
        xml += "<qhcj DataType=\"java.lang.String\">" + qhcj + "</qhcj>";//簽核層級
        xml += "</SQIT004>";

        param["SQIT004"] = xml;
        return "SQIT004";
    }


    /// <summary>
    /// 發起流程後呼叫此方法
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        base.afterSend(engine, currentObject);
    }

    /// <summary>
    /// 若有加簽，送簽核前呼叫。
    /// 加簽時系統會設定Session("IsAddSign")，所以必需在saveData時執行 setSession("IsAddSign", "AFTER");
    /// AFTER 代表往後簽核
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        return base.beforeSign(engine, isAfter, addSignXml);
    }

    /// <summary>
    /// 按下送簽按鈕後呼叫此方法。
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterSign(AbstractEngine engine, DataObject currentObject, string result)
    {
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
    /// 流程結束後會執行此方法，系統會傳入result代表結果，其中Y：同意結案；N：不同意結案；W：撤銷（抽單）。
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    protected override void afterApprove(AbstractEngine engine, DataObject currentObject, string result)
    {
        //獲取資訊系統API地址
        string url = "http://10.3.11.102/SimploWebAPI/api/IRS_Requirements/CloseECPStep2";
        bool resultBool = false;

        string sheetNo = currentObject.getData("SheetNo");

        string sql = @"select RqNO from SQIT004A where SheetNo ='" + sheetNo + "'";
        DataRow bxtb = engine.getDataSet(sql, "data").Tables["data"].Rows[0];

        string number = "";

        number = bxtb["RqNO"].ToString();

        StringBuilder Json = new StringBuilder();
        Json.AppendFormat("{0}", "{");
        Json.AppendFormat("\"number\":\"{0}\",", number);
        //"Y"代表同意
        if (result == "Y")
        {
            Json.AppendFormat("\"type\":\"{0}\"", "1");
        }
        else
        {
            Json.AppendFormat("\"type\":\"{0}\"", "0");
        }

        Json.AppendFormat("{0}", "}");
        try
        {
            resultBool = Send(url, Json);
            writeLog("result:" + result);
            writeLog("number:" + number);
            writeLog("Json:" + Json);
			writeLog("resultBool:" + resultBool + url);
        }
        catch (Exception ex)
        { }
        base.showData(engine, currentObject);
    }

    /// <summary>
    /// 調用API
    /// </summary>
    /// <param name="urlString"></param>
    /// <param name="resultJson"></param>
    /// <returns></returns>
    public bool Send(string urlString, StringBuilder resultJson)
    {
        bool resultBool = false;
        //messageString = "OK";

        try
        {
            Stream dataStream = null;

            Encoding encoding = Encoding.UTF8;

            //处理HttpWebRequest访问https有安全证书的问题（ 请求被中止: 未能创建 SSL/TLS 安全通道。）
            //ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlString);
            request.Method = "POST";
            //request.Headers.Add("Authorization", string.Format("Bearer {0}", tokenString));
            request.ContentType = "application/json";
            request.KeepAlive = false; //关闭"持久连接"
            request.Timeout = 36000;
            request.ProtocolVersion = HttpVersion.Version11;

            byte[] buffer = encoding.GetBytes(resultJson.ToString());
            request.ContentLength = buffer.Length;
            using (dataStream = request.GetRequestStream())
            {
                dataStream.Write(buffer, 0, buffer.Length);
            }
            //request.GetRequestStream().Write(buffer, 0, buffer.Length);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            //{
            //    resultString = reader.ReadToEnd();
            //}
            resultBool = true;
        }
        catch (Exception ex)
        {
            resultBool = false;
            //messageString = ex.Message;
        }

        return resultBool;
    }
    //客製簽核記錄
    protected void ShowHistory(string RqNO)
    {
        AbstractEngine engine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);

            string sql = @"select SheetNo from SQIT003A where RqNO ='" + RqNO + "'";
            DataRow bxtb = engine.getDataSet(sql, "data").Tables["data"].Rows[0];
            string number = "";
            number = bxtb["SheetNo"].ToString();

            string sql1 = @"select SheetNo from SQIT004A where RqNO ='" + RqNO + "'";
            DataRow bxtb1 = engine.getDataSet(sql1, "data").Tables["data"].Rows[0];
            string number1 = "";
            number1 = bxtb1["SheetNo"].ToString();
            

            
            // "SQIT00400000096";

            string strSheetNo = number;
            string strSheetNo1 = number1;
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
            DataSet ds = null;
            DataSet ds1 = null;
            DataSet ds2 = null;

            //簽核意見
            //DataSet 
            ds = fh_sj(strSheetNo);
            ds1 = fh_sj1(strSheetNo1);
            ds2 = fp_sj(RqNO);

            #region MyRegion
            string table = "<table border='0' cellpadding='0' cellspacing='0' class='BasicFormHeadBorder'  style='width: 660px; '>";

            table += "<tr>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >&nbsp;&nbsp;</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >類型</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >關卡名稱</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >處理者</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >處理結果</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;' >處理意見</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >處理時間</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >狀態</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >轉寄</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >開始時間</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:9pt;'  >處理時間</td>";
            table += "</tr>";

            //立案階段
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table += "<tr>";
                table += "<td class='BasicFormHeadDetail' style='font-size:9pt;'>&nbsp;&nbsp;</td>";
                for (int j = 0; j < 10; j++)
                {
                    string sss = ds.Tables[0].Rows[i][j].ToString();
                    if (sss.Length == 0)
                    {
                        string name = "Image" + j + i + new Random().Next(100);
                        sss = "&nbsp;";//"<asp:Image ID='" + name + "' runat='server' ImageUrl='~/SmpProgram/Form/STHR003DY/123.gif' />";
                    }
                    table += "<td class='BasicFormHeadDetail' style='font-size:9pt;'>" + sss + "</td>";
                }
                table += "</tr>";
            }
            //分配階段
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                table += "<tr>";
                table += "<td class='BasicFormHeadDetail' style='font-size:9pt;'>&nbsp;&nbsp;</td>";
                for (int j = 0; j < 10; j++)
                {
                    string sss = ds2.Tables[0].Rows[i][j].ToString();
                    if (sss.Length == 0)
                    {
                        string name = "Image" + j + i + new Random().Next(100);
                        sss = "&nbsp;";//"<asp:Image ID='" + name + "' runat='server' ImageUrl='~/SmpProgram/Form/STHR003DY/123.gif' />";
                    }
                    table += "<td class='BasicFormHeadDetail' style='font-size:9pt;'>" + sss + "</td>";
                }
                table += "</tr>";
            }
            //申請階段
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                table += "<tr>";
                table += "<td class='BasicFormHeadDetail' style='font-size:9pt;'>&nbsp;&nbsp;</td>";
                for (int j = 0; j < 10; j++)
                {
                    string sss = ds1.Tables[0].Rows[i][j].ToString();
                    if (sss.Length == 0)
                    {
                        string name = "Image" + j + i + new Random().Next(100);
                        sss = "&nbsp;";//"<asp:Image ID='" + name + "' runat='server' ImageUrl='~/SmpProgram/Form/STHR003DY/123.gif' />";
                    }
                    table += "<td class='BasicFormHeadDetail' style='font-size:9pt;'>" + sss + "</td>";
                }
                table += "</tr>";
            }

            table += "</table>";

            div2.InnerHtml = table;
            #endregion

        }
        catch (Exception ex)
        {
            MessageBox(ex.ToString());
        }
        finally
        {
            if (engine != null) engine.close();
            //if (erpEngine != null) erpEngine.close();
            if (sw != null) sw.Close();
        }
    }
    //立案簽核記錄
    public DataSet fh_sj(string SheetNo)
    {
        string sql = " select distinct  '立案' as lx,case when assigneeOID=(select OID from Users where id='0000' ) then '董事長' else  wi.workItemName end as workItemName,U.id+'_'+userName as clz, ";
        sql = sql + " (substring(isnull(wi.executiveComment,''), charindex('#', isnull(wi.executiveComment,''))+2,100))  AS jg,(substring(isnull(wi.executiveComment,''),0, charindex('#',isnull(wi.executiveComment,'')))) as yj , ";
        sql = sql + " convert(varchar,wi.completedTime, 111) + ' ' + convert(varchar, wi.completedTime, 108) as completedTime, case when executiveComment LIKE'%不同意%' then '已中止' else '已完成' END AS zt ,''as zj, ";
        sql = sql + "  convert(varchar,wi.createdTime, 111) + ' ' + convert(varchar, wi.createdTime, 108) as createdTime,Convert(decimal(10,2),DATEDIFF(ss,wi.createdTime,wi.completedTime)/60.0/60,1) tm  ";
        sql = sql + " from WebFormPT.dbo.SQIT003A as sr ";
        sql = sql + " left join dbo.SMWYAAA as sy on sr.SheetNo=sy.SMWYAAA002 ";
        sql = sql + " left join NaNa.dbo.ProcessInstance as pi  on sy.SMWYAAA005=pi.serialNumber ";
        sql = sql + " left join dbo.WorkItem as wi on wi.contextOID=pi.contextOID ";
        sql = sql + " left join dbo.WorkAssignment as wa on wa.workItemOID=wi.OID ";
        sql = sql + " left join dbo.Users as U on U.OID=wi.performerOID   ";
        sql = sql + " where  sr.SheetNo='" + SheetNo + "' ";
        sql = sql + " order by wi.createdTime ";

        //聲明一個 IOFactory 對象YFP
        IOFactory factory = new IOFactory();
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        DataSet ds = engine2.getDataSet(sql, "TEMP");
        return ds;
    }
    //立案簽核記錄
    public DataSet fp_sj(string SheetNo)
    {
        string sql = "select 'SA' lx,case when ActionCode='Transfer' THEN '轉單' else '分配' end workItemName,Operator collate Chinese_Taiwan_Stroke_CI_AS+'_'+EmpName clz, ";
        sql = sql + " Action,Result,Replace(Convert(nvarchar(20),OperatingTime,120),'-','/')OperatingTime,Status,'' zj,Replace(Convert(nvarchar(20),StartingTime,120),'-','/')StartingTime,OperatingHours   ";
        sql = sql + " from [10.3.11.102].ITPlatform_DB.dbo.LOG_Resume_Requirements a,dbo.HRUSERS b  ";
        sql = sql + " where a.Operator collate Chinese_Taiwan_Stroke_CI_AS=b.EmpNo and ActionCode in('Transfer','Asign') and  ";
        sql = sql + " BillNumber='" + SheetNo + "' ";
        sql = sql + " order by CreateTime ";

        //聲明一個 IOFactory 對象YFP
        IOFactory factory = new IOFactory();
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        DataSet ds = engine2.getDataSet(sql, "TEMP");
        return ds;
    }
    //申請簽核記錄
    public DataSet fh_sj1(string SheetNo)
    {
        string sql = " select distinct  '申請' as lx,case when assigneeOID=(select OID from Users where id='0000' ) then '董事長' else  wi.workItemName end as workItemName,isnull(U.id+'_'+U.userName,'')+isnull(o.id+'_'+o.userName,'') as clz, ";
        sql = sql + " (substring(isnull(wi.executiveComment,''), charindex('#', isnull(wi.executiveComment,''))+2,100))  AS jg,(substring(isnull(wi.executiveComment,''),0, charindex('#',isnull(wi.executiveComment,'')))) as yj , ";
        sql = sql + " convert(varchar,wi.completedTime, 111) + ' ' + convert(varchar, wi.completedTime, 108) as completedTime, case when executiveComment LIKE'%不同意%' then '已中止' else '已完成' END AS zt ,''as zj, ";
        sql = sql + "  convert(varchar,wi.createdTime, 111) + ' ' + convert(varchar, wi.createdTime, 108) as createdTime ,Convert(decimal(10,2),DATEDIFF(ss,wi.createdTime,wi.completedTime)/60.0/60,1) tm ";
        sql = sql + " from WebFormPT.dbo.SQIT004A as sr ";
        sql = sql + " left join dbo.SMWYAAA as sy on sr.SheetNo=sy.SMWYAAA002 ";
        sql = sql + " left join NaNa.dbo.ProcessInstance as pi  on sy.SMWYAAA005=pi.serialNumber ";
        sql = sql + " left join dbo.WorkItem as wi on wi.contextOID=pi.contextOID ";
        sql = sql + " left join dbo.WorkAssignment as wa on wa.workItemOID=wi.OID ";
        sql = sql + " left join dbo.Users as U on U.OID=wi.performerOID   ";
        sql = sql + " left join dbo.Users as o on o.OID=wa.assigneeOID    ";
        sql = sql + " where  sr.SheetNo='" + SheetNo + "' ";
        sql = sql + " order by wi.createdTime ";

        //聲明一個 IOFactory 對象YFP
        IOFactory factory = new IOFactory();
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        DataSet ds = engine2.getDataSet(sql, "TEMP");
        return ds;
    }
    /// <summary>
    /// 日誌
    /// </summary>
    /// <param name="engine">WebFormPT</param>
    /// <param name="currentObject"></param>
    /// <param name="result"></param>
    private void writeLog(string message)
    {
        System.IO.StreamWriter sw = null;
        try
        {
            string sheetNo = (string)getSession(this.PageUniqueID, "SheetNo");
            string line = ProcessPageID + "^" + sheetNo + "^" + DateTime.Now + "^" + message;
            string serverPath = Server.MapPath("/ECP/LogFolder");
            sw = new System.IO.StreamWriter(serverPath + @"\SQIT004.log", true, System.Text.Encoding.Default);
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
