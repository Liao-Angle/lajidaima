using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;

public partial class SmpProgram_Form_EH010301_PrintPage : BaseWebUI.GeneralWebPage  //System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

            string strUserId = (string)Session["UserId"];
			string strSheetNo = (string)Session["EH010301_SheetNo"] ;			
            if(Session["EH010301_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["EH010301_SheetNo"];				
            }
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strStartDate = "";
            string strEndDate = "";		
            string sql = null;
			
            DataSet ds = null;				

            //顯示使用者資訊
			sql = "select aa.EmpNo+'-'+aa.EmpName as EmpInfo, LeaveTypeName, Reason, SheetNo, cast (Hours as varchar)  + '   (合計:'  + hourforDay+'天 '  + hourforhour + '小時)' as showHours  ";
			sql = sql + " , substring(startDateTime,1,10) startDate, substring(startDateTime, 12, 5) startTime, substring(endDateTime, 1, 10) endDate, substring(endDateTime, 12, 5) endTime  ";
			sql = sql + " , aa.Subject, hr.PartNo as DeptInfo, hr.DtName as empTitle  ";
			sql = sql + " from (  ";
			sql = sql + " SELECT   a.EmpNo, a.EmpName, a.LeaveTypeName, a.Reason, a.SheetNo, a.Hours, min(LeaveDate+' '+BeginTime) startDateTime, max(LeaveDate+' '+EndTime) endDateTime, s.SMWYAAA006 as Subject, s.SMWYAAA008, s.SMWYAAA009, s.SMWYAAA010  ";
            sql = sql + " , cast(cast(round( CONVERT(int,a.Hours)/8,0) as int)as varchar) hourforDay, cast(cast (a.Hours%8 as int)as varchar) hourforhour FROM EH010301A a,  EH010301B b, SMWYAAA s  ";
			sql = sql + " where a.SheetNo=b.SheetNo  and s.SMWYAAA019=a.GUID ";
			sql = sql + " and a.SheetNo='"+ strSheetNo +"'  ";
			sql = sql + " group by a.EmpNo, a.EmpName, a.LeaveTypeName, a.Reason, a.SheetNo, a.Hours, s.SMWYAAA006, s.SMWYAAA008, s.SMWYAAA009, s.SMWYAAA010  ";
			sql = sql + " ) aa left join HRUSERS hr on hr.EmpNo collate Chinese_Taiwan_Stroke_CI_AS=aa.EmpNo ";

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {                
				lblSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
				lblCompany.Text = "重慶新普";
				lblDeptName.Text = ds.Tables[0].Rows[0]["DeptInfo"].ToString();
				lblEmpNumber.Text = ds.Tables[0].Rows[0]["EmpInfo"].ToString();
				lblAgent.Text = "　";
				lblFlowType.Text = "一般";
				lblClassType.Text = "　";				
				lblLeaveType.Text = ds.Tables[0].Rows[0]["LeaveTypeName"].ToString();
				lblIsHoliday.Text = "　";
				lblStartDate.Text = ds.Tables[0].Rows[0]["startDate"].ToString();
				lblEndDate.Text = ds.Tables[0].Rows[0]["endDate"].ToString();
				lblStartTime.Text = ds.Tables[0].Rows[0]["startTime"].ToString();
				lblEndTime.Text = ds.Tables[0].Rows[0]["endTime"].ToString();
				lblLeaveHours.Text = ds.Tables[0].Rows[0]["showHours"].ToString();
				lblDepty.Text = ds.Tables[0].Rows[0]["empTitle"].ToString();
				lblDescription.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
				lblCheckby1.Text = "　";
				lblCheckby2.Text = "　";
				DSCLabelBZ.Text = "**重慶新普請假單**";
            }
						
			//簽核意見
            //DataSet 
			ds = fh_sj(strSheetNo);

            #region MyRegion
            string table = "<table border='0' cellpadding='1' cellspacing='0' class='BasicFormHeadBorder'  style='width: 700px; '>";
            table += "<tr>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >類型</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >關卡名稱</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >處理者</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >處理結果</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;' >處理意見</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >處理時間</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >狀態</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >轉寄</td>";
            table += "<td class='BasicFormHeadHead' style='font-size:8pt;'  >開始時間</td>";
            //table += "<td>處理時間</td>";
            table += "</tr>";

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table += "<tr>";
                for (int j = 0; j < 9; j++)
                {
                    string sss = ds.Tables[0].Rows[i][j].ToString();
                    if (sss.Length == 0)
                    {
                        string name = "Image"+j+i+new Random().Next(100);
                        sss = "&nbsp;";//"<asp:Image ID='" + name + "' runat='server' ImageUrl='~/SmpProgram/Form/STHR003DY/123.gif' />";
                    }

                    table += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + sss + "</td>";                    
                }
                table += "</tr>";
            }

             table+="</table>";

             div2.InnerHtml=table;
            #endregion
			
			
		   //表單資訊
           ds = fh_Info(strSheetNo);
		   string value1 = "";
		   string value2 = "";
		   string value3 = "";
		   string value4 = "";
		   string value5 = "";
		   string value6 = "";
		   string value7 = "";
		   string value8 = "";
		   string value9 = "";
		   string value10 = "";
		   for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
           {
				value1 = ds.Tables[0].Rows[0][0].ToString();
				value2 = ds.Tables[0].Rows[0][1].ToString();
				value3 = ds.Tables[0].Rows[0][2].ToString();
				value4 = ds.Tables[0].Rows[0][3].ToString();
				value5 = ds.Tables[0].Rows[0][4].ToString();
				value6 = ds.Tables[0].Rows[0][5].ToString();
				value7 = ds.Tables[0].Rows[0][6].ToString();
				value8 = ds.Tables[0].Rows[0][7].ToString();
				value9 = ds.Tables[0].Rows[0][8].ToString();
				value10 = ds.Tables[0].Rows[0][9].ToString();
		   }

           string table2 = "<table border='0' cellpadding='1' cellspacing='0' class='BasicFormHeadBorder'   style='width: 700px; '>";
           table2 += "<tr>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >流程名稱</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>"+value1+"</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>單號</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + value2 + "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>填表日期</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + value3 + "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>流程狀態</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>"+value4+"</td>";           
           table2 += "</tr>";
           table2 += "<tr>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >重要性</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>"+value5+"</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >主旨</td>";
           table2 += "<td colspan='5' class='BasicFormHeadDetail' width='450px' style='font-size:8pt;' >" + value6 + "</td>";
           table2 += "</tr>";
           table2 += "<tr>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>申請人代號</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + value7+ "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >申請人姓名</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>"+value8+"</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>申請單位代號</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>"+value9+"</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>申請單位名稱</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>"+value10+"</td>";
           table2 += "</tr>";
           table2 += "</table>";
           divFormInfo.InnerHtml = table2;
			
		
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (engine != null) engine.close();
            //if (erpEngine != null) erpEngine.close();
            if (sw != null) sw.Close();
        }
    }
	

    public DataSet fh_sj(string SheetNo)
    {
        string sql = " select distinct  '一般' as lx,case when assigneeOID=(select OID from Users where id='0000' ) then '董事長' else  wi.workItemName end as workItemName,U.id+'_'+userName as clz, ";
		sql = sql +  " (substring(isnull(wi.executiveComment,''), charindex('#', isnull(wi.executiveComment,''))+2,100))  AS jg,(substring(isnull(wi.executiveComment,''),0, charindex('#',isnull(wi.executiveComment,'')))) as yj , ";
		sql = sql +  " convert(varchar,wi.completedTime, 111) + ' ' + convert(varchar, wi.completedTime, 108) as completedTime, case when executiveComment LIKE'%不同意%' then '已中止' else '已完成' END AS zt ,''as zj, ";
		sql = sql +  "  convert(varchar,wi.createdTime, 111) + ' ' + convert(varchar, wi.createdTime, 108) as createdTime  ";
		sql = sql +  " from WebFormPT.dbo.EH010301A as sr ";
		sql = sql +  " left join dbo.SMWYAAA as sy on sr.SheetNo=sy.SMWYAAA002 ";
		sql = sql +  " left join NaNa.dbo.ProcessInstance as pi  on sy.SMWYAAA005=pi.serialNumber ";
		sql = sql +  " left join dbo.WorkItem as wi on wi.contextOID=pi.contextOID ";
		sql = sql +  " left join dbo.WorkAssignment as wa on wa.workItemOID=wi.OID ";
		sql = sql +  " left join dbo.Users as U on U.OID=wi.performerOID   ";
		sql = sql +  " where  sr.SheetNo='" + SheetNo + "' ";
		sql = sql +  " order by wi.createdTime ";

        //聲明一個 IOFactory 對象YFP
        IOFactory factory = new IOFactory();
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        DataSet ds = engine2.getDataSet(sql, "TEMP");
        return ds;
    }

    /// <summary>
    /// 填表日期
    /// </summary>
    /// <param name="SheetNo"></param>
    /// <returns></returns>
    public DataSet fh_Info(string SheetNo)
    {
        string sql = "select SMWYAAA004, SMWYAAA002, SMWYAAA017, flowStauts = CASE WHEN SMWYAAA020 = 'I' THEN '進行中'  WHEN SMWYAAA020 = 'Y' ";
		sql = sql +  " THEN '已結案' WHEN SMWYAAA020 = 'N' THEN '已終止' WHEN SMWYAAA020 = 'W' THEN '已撤銷' ELSE ' ' END ";
		sql = sql +  " ,imporment =  CASE WHEN  SMWYAAA007 = '0' THEN '低'  WHEN  SMWYAAA007 = '1' ";
        sql = sql +  "                    THEN '中' WHEN  SMWYAAA007 = '2' THEN '高' ELSE ' ' END  ";
		sql = sql +  " ,  SMWYAAA006,  SMWYAAA008,  SMWYAAA009,  SMWYAAA010,  SMWYAAA011 ";
		sql = sql +  " from SMWYAAA where SMWYAAA002='" + SheetNo + "' ";
		
        //聲明一個 IOFactory 對象YFP
        IOFactory factory = new IOFactory();
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        DataSet ds = engine2.getDataSet(sql, "TEMP");
        return ds;
    }
	
}