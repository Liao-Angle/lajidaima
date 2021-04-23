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


public partial class SmpProgram_Form_SPAD009_PrintPage3 : BaseWebUI.GeneralWebPage //System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        //AbstractEngine erpEngine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
			//sw.WriteLine("in page load")'
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            //string connStr = sp.getParam("WorkFlowERPDB");
            //erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
            
            //string strUserId = Request["originatorId"];
            string strUserId = (string)Session["UserId"];
			string strSheetNo = (string)Session["SPAD009_SheetNo"] ;
            if(Session["SPAD009_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["SPAD009_SheetNo"];
            }
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strStartDate = "";
            string strEndDate = "";		
            string sql = null;
			
			//string strAttendDate = Request["AttendDate"];
			
            DataSet ds = null;				

            //顯示使用者資訊
			sql = "select Subject, SheetNo, u.empNumber, u.empName, u.empEName, deptId, deptName ";
			sql = sql + " , (select userName from Users where OID=CheckByGUID or OID is null ) checkBy ";
 			sql = sql + ", TripDate, StartTime, EndTime, IsTripFee, TripSite, Notes, TrafficFee, EatFee, ParkingFee, OtherFee ";
			sql = sql + ", MeetingMinute, StartMileage, EndMileage, MileageSum, EtagFee, MileageSum*6 'PayeeFee'  ";
 			sql = sql + ", case orgId when 'SMP' then '新普科技' else '中普科技' end as OrgName ";
			sql = sql + "from SmpTripForm a, EmployeeInfo u ";
			sql = sql + "where SheetNo='" + Utility.filter(strSheetNo) + "'  ";
			//sql = sql + "where SheetNo='SPAD00900000064' ";
			sql = sql + "and a.OriginatorGUID=u.empGUID ";

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Subject.Text =  ds.Tables[0].Rows[0]["Subject"].ToString();
                SheetNo.Text = ds.Tables[0].Rows[0]["SheetNo"].ToString();
				empNumber.Text = ds.Tables[0].Rows[0]["empNumber"].ToString();				
				empName.Text = ds.Tables[0].Rows[0]["empName"].ToString();
				empEName.Text =  ds.Tables[0].Rows[0]["empEName"].ToString();				
				empInfo.Text = empNumber.Text + '-' + empName.Text;
				strTripUserId = empNumber.Text; //簽名檔用
		
				deptId.Text = ds.Tables[0].Rows[0]["deptId"].ToString();
				deptName.Text = ds.Tables[0].Rows[0]["deptName"].ToString();
				deptInfo.Text = deptId.Text + '-' + deptName.Text;
				
                checkBy.Text = ds.Tables[0].Rows[0]["checkBy"].ToString();
				if (checkBy.Text.Equals(""))
				{
					checkBy.Text = " -- ";
				}
				TripDate.Text = ds.Tables[0].Rows[0]["TripDate"].ToString();
				StartTime.Text = ds.Tables[0].Rows[0]["StartTime"].ToString();
				EndTime.Text = ds.Tables[0].Rows[0]["EndTime"].ToString();
				
				IsTripFee.Text =  ds.Tables[0].Rows[0]["IsTripFee"].ToString();
				if (IsTripFee.Text.Equals("Y"))
				{
				  IsTripFee.Text = "Y - 需請出差費用 ";
				}
				else
				{
				  IsTripFee.Text = "N - 不申請出差費用 ";
				}
				
				TripSite.Text = ds.Tables[0].Rows[0]["TripSite"].ToString();
				if (TripSite.Text.Equals(""))
				{
					TripSite.Text = " -- ";
				}

                Notes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
				if (Notes.Text.Equals(""))
				{
					Notes.Text = " -- ";
				}
				TrafficFee.Text = ds.Tables[0].Rows[0]["TrafficFee"].ToString();
				if (TrafficFee.Text.Equals(""))
				{
					TrafficFee.Text = " -- ";
				}
				EatFee.Text = ds.Tables[0].Rows[0]["EatFee"].ToString();
				if (EatFee.Text.Equals(""))
				{
					EatFee.Text = " -- ";
				}
				ParkingFee.Text =  ds.Tables[0].Rows[0]["ParkingFee"].ToString();
				if (ParkingFee.Text.Equals(""))
				{
					ParkingFee.Text = " -- ";
				}
                OtherFee.Text = ds.Tables[0].Rows[0]["OtherFee"].ToString();
				if (OtherFee.Text.Equals(""))
				{
					OtherFee.Text = " -- ";
				}
				OrgName.Text = ds.Tables[0].Rows[0]["OrgName"].ToString();
				if (OrgName.Text.Equals(""))
				{
					OrgName.Text = " -- ";
				}
				
				StartMileage.Text = ds.Tables[0].Rows[0]["StartMileage"].ToString();
				if (StartMileage.Text.Equals(""))
				{
					StartMileage.Text = " -- ";
				}
				EndMileage.Text = ds.Tables[0].Rows[0]["EndMileage"].ToString();
				if (EndMileage.Text.Equals(""))
				{
					EndMileage.Text = " -- ";
				}
				MileageSum.Text =  ds.Tables[0].Rows[0]["MileageSum"].ToString();
				if (MileageSum.Text.Equals(""))
				{
					MileageSum.Text = " -- ";
				}
                EtagFee.Text = ds.Tables[0].Rows[0]["EtagFee"].ToString();
				if (EtagFee.Text.Equals(""))
				{
					EtagFee.Text = " -- ";
				}
				PayeeFee.Text = ds.Tables[0].Rows[0]["PayeeFee"].ToString();
				if (PayeeFee.Text.Equals(""))
				{
					PayeeFee.Text = " -- ";
				}

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
        //string sql = "exec  [dbo].[SmpHRLeavePrint] '" + SheetNo + "'";
		string sql = " select distinct  '一般' as lx,wi.workItemName,U.id+'_'+userName as clz, ";
		sql = sql +  " (substring(isnull(wi.executiveComment,''), charindex('#', isnull(wi.executiveComment,''))+2,100))  AS jg,(substring(isnull(wi.executiveComment,''),0, charindex('#',isnull(wi.executiveComment,'')))) as yj ,wi.completedTime,case when executiveComment LIKE'%不同意%' then '已中止' else '已完成' END AS zt ,''as zj, ";
		sql = sql +  " wi.createdTime  ";
		sql = sql +  " from dbo.SmpTripForm as sr ";
		sql = sql +  " left join dbo.SMWYAAA as sy on sr.SheetNo=sy.SMWYAAA002 ";
		sql = sql +  " left join dbo.ProcessInstance as pi  on sy.SMWYAAA005=pi.serialNumber ";
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