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
using System.Collections;
using DSCWebControl;

public partial class Program_SCQ_Form_EH0109_PrintPage : BaseWebUI.GeneralWebPage  //System.Web.UI.Page
{
    private int MaxDSC = 4;
    private string DSCName = "DSCCheckBox";
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
            string strSheetNo = (string)Session["EH0110_SheetNo"];
            if (Session["EH0110_SheetNo"] != null)
            {
                strSheetNo = (string)Session["EH0110_SheetNo"];

            }
            //strSheetNo = "EH010900000084";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);		
            string sql = null;
            DataSet ds = null;

            //顯示使用者資訊
            sql = @"SELECT   a.EmpNo, a.EmpName, a.ComeDate,a.SheetNo,Privilege,py,Obx,Ozgjg,Ozwjg,Ozyjg,Ojbbt,Oqx,Ojs,movelist,Owork,TryuseDate,
                    Nwork,NPartNo,NDtName,NDt,Nbx,Nzgjg,Nzwjg,Nzyjg,Njbbt,Nqx,Njs,PartNo,DtName,case when DtName='作業員' then '一職等'
                    when DtName='領班' then '二職等' when DtName='辦事員' then '二職等' when DtName='技術員' then '二職等' when DtName='司機' then '二職等'
                    when DtName='組長' then '三職等' when DtName='副工程師' then '三職等' when DtName='副管理師' then '三職等' else '' end Dt,
                    field004,field005,dagong,xiaogong,jiangli,daguo,xiaoguo,shenjie,shijia,bingjia,kuanggong, 
                    s.SMWYAAA006 as Subject, s.SMWYAAA008, s.SMWYAAA009, s.SMWYAAA010 
                    FROM EH0110A a, SMWYAAA s,HRUSERS b,[10.3.11.92\SQL2008].SCQHRDB.dbo.ecptx c 
                    where s.SMWYAAA019=a.GUID and a.EmpNo collate Chinese_Taiwan_Stroke_CI_As=b.EmpNo and a.EmpNo collate Chinese_Taiwan_Stroke_CI_As=c.empno and a.SheetNo='" + strSheetNo + "'";

            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {

                string check = ds.Tables[0].Rows[0]["Privilege"].ToString();
                for (int i = 1; i <= MaxDSC; i++)
                {
                    DSCCheckBox dsc = (DSCCheckBox)this.FindControl(DSCName + i.ToString());
                    if (dsc != null && i <= check.Length && check.Substring(i - 1, 1) == "Y")
                    {
                        dsc.Checked = true;
                    }
                    if (dsc != null)
                    {
                        dsc.ReadOnly = true;
                    }
                }
                EmpNo.ValueText = ds.Tables[0].Rows[0]["EmpNo"].ToString();
                EmpNo.ReadOnlyValueText = ds.Tables[0].Rows[0]["EmpName"].ToString();
                ComeDate.ValueText = ds.Tables[0].Rows[0]["ComeDate"].ToString();
                py.ValueText = ds.Tables[0].Rows[0]["py"].ToString();
                OPartNo.ValueText = ds.Tables[0].Rows[0]["PartNo"].ToString();
                ODt.ValueText = ds.Tables[0].Rows[0]["Dt"].ToString();
                ODtName.ValueText = ds.Tables[0].Rows[0]["DtName"].ToString();
                Obx.ValueText = ds.Tables[0].Rows[0]["Obx"].ToString();
                Ozgjg.ValueText = ds.Tables[0].Rows[0]["Ozgjg"].ToString();
                Ozwjg.ValueText = ds.Tables[0].Rows[0]["Ozwjg"].ToString();
                Ozyjg.ValueText = ds.Tables[0].Rows[0]["Ozyjg"].ToString(); ;
                Ojbbt.ValueText = ds.Tables[0].Rows[0]["Ojbbt"].ToString();
                Oqx.ValueText = ds.Tables[0].Rows[0]["Oqx"].ToString();
                Ojs.ValueText = ds.Tables[0].Rows[0]["Ojs"].ToString();
                movelist.ValueText = ds.Tables[0].Rows[0]["movelist"].ToString();
                Owork.ValueText = ds.Tables[0].Rows[0]["Owork"].ToString();
                Nwork.ValueText = ds.Tables[0].Rows[0]["Nwork"].ToString();
                NPartNo.ValueText = ds.Tables[0].Rows[0]["NPartNo"].ToString();
                NDtName.ValueText = ds.Tables[0].Rows[0]["NDtName"].ToString();
                NDt.ValueText = ds.Tables[0].Rows[0]["NDt"].ToString();
                Nbx.ValueText = ds.Tables[0].Rows[0]["Nbx"].ToString();
                Nzgjg.ValueText = ds.Tables[0].Rows[0]["Nzgjg"].ToString();
                Nzwjg.ValueText = ds.Tables[0].Rows[0]["Nzwjg"].ToString();
                Nzyjg.ValueText = ds.Tables[0].Rows[0]["Nzyjg"].ToString(); ;
                Njbbt.ValueText = ds.Tables[0].Rows[0]["Njbbt"].ToString();
                Nqx.ValueText = ds.Tables[0].Rows[0]["Nqx"].ToString();
                Njs.ValueText = ds.Tables[0].Rows[0]["Njs"].ToString();
                jdg.ValueText = ds.Tables[0].Rows[0]["dagong"].ToString();
                jxg.ValueText = ds.Tables[0].Rows[0]["xiaogong"].ToString();
                jj.ValueText = ds.Tables[0].Rows[0]["jiangli"].ToString();
                fdg.ValueText = ds.Tables[0].Rows[0]["daguo"].ToString();
                fxg.ValueText = ds.Tables[0].Rows[0]["xiaoguo"].ToString();
                sj.ValueText = ds.Tables[0].Rows[0]["shenjie"].ToString();
                SingleField1.ValueText = ds.Tables[0].Rows[0]["shijia"].ToString();
                SingleField2.ValueText = ds.Tables[0].Rows[0]["bingjia"].ToString();
                SingleField4.ValueText = ds.Tables[0].Rows[0]["kuanggong"].ToString();
                Mkh.ValueText = ds.Tables[0].Rows[0]["field004"].ToString();
                Ekh.ValueText = ds.Tables[0].Rows[0]["field005"].ToString();
                NZ.ValueText = ds.Tables[0].Rows[0]["TryuseDate"].ToString().Replace("上午 12:00:00", "");

               

            }
						
			//簽核意見
            //DataSet 
            //strSheetNo = "EH010900000084";
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

           string table2 = "<table border='0' cellpadding='1' cellspacing='0' class='BasicFormHeadBorder'   style='width: 660px; '>";
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
        string sql = " select distinct  '一般' as lx,wi.workItemName,U.id+'_'+userName as clz, ";
		sql = sql +  " (substring(isnull(wi.executiveComment,''), charindex('#', isnull(wi.executiveComment,''))+2,100))  AS jg,(substring(isnull(wi.executiveComment,''),0, charindex('#',isnull(wi.executiveComment,'')))) as yj , ";
		sql = sql +  " convert(varchar,wi.completedTime, 111) + ' ' + convert(varchar, wi.completedTime, 108) as completedTime, case when executiveComment LIKE'%不同意%' then '已中止' else '已完成' END AS zt ,''as zj, ";
		sql = sql +  "  convert(varchar,wi.createdTime, 111) + ' ' + convert(varchar, wi.createdTime, 108) as createdTime  ";
		sql = sql +  " from WebFormPT.dbo.EH0110A as sr ";
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