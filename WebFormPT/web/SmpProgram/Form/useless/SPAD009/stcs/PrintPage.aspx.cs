using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.factory;
using System.Data;

public partial class SmpProgram_Form_STHR003DY_Form :BaseWebUI.GeneralWebPage //System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Dictionary<string, string> dictjb = new Dictionary<string, string>();

        dictjb.Add("A01", "事假");
        dictjb.Add("K01", "曠工");
        dictjb.Add("A02", "病假");
        dictjb.Add("C01", "特休假");
        dictjb.Add("C15", "返臺假");
        dictjb.Add("B01", "婚假");
        dictjb.Add("C12", "產假");
        dictjb.Add("C03", "哺乳假");
        dictjb.Add("C14", "陪產假");
        dictjb.Add("A05", "工傷假");
        dictjb.Add("C11", "喪假");
        dictjb.Add("C17", "產檢假");
        dictjb.Add("C18", "流產假");
        dictjb.Add("C19", "出差假");
        dictjb.Add("C13", "有薪事假");
        dictjb.Add("XXX", "責任制請假");


        if(!IsPostBack)
        {
            string SheetNo = "STHR00300000137";

            if (Request.QueryString.Count == 0)
            {
                SheetNo = "";
            }
            else
            {
              SheetNo=Request.QueryString["SheetNo"].ToString(); 
            }
             //Request.QueryString["SheetNo"].ToString();

            string[] strsz = show(SheetNo);
            //主旨
            DSCLabelzhuzhi.Text = "請假日期:" + strsz[9] + "~" + strsz[10] + "  請假人員:" + strsz[2] + "-"
                + strsz[3] +"  "+ dictjb[strsz[6]];
            //公司別
            DSCLabelGSB.Text = strsz[0];
            //部門
            DSCLabelBM.Text = strsz[1];
            //請假人員
            DSCLabelNoName.Text = strsz[2] + "-" + strsz[3];
            //流程類別
            DSCLabeltype.Text = "自定";
            //假別
            DSCLabelJB.Text = dictjb[strsz[6]];
            //請假是否含節假日
            DSCLabelQJHJR.Text = "是";

            //起止日期
            DSCLabelqsrq.Text = strsz[9];
            //結束日期
            DSCLabel1jzrq.Text = strsz[10];
            //開始時間
            DSCLabelqstime.Text = strsz[11];
            //截止時間 
            DSCLabelJZRQ.Text = strsz[12]; 

            double t=0;
            double s=0;
             if(strsz[7].Length!=0)
             {
               t=Convert.ToDouble(strsz[7])/8;
                 s=Convert.ToDouble(strsz[7])%8;
             }
             int it = (int)t;
            int iss=(int)s;

            //請假時數
             DSCLabelQJSS.Text = strsz[7] + "   (合計:" + it + "天" + iss + "小時)";
            //職稱
            DSCLabelDtName.Text = strsz[4];

            //說明
            SingleFieldSM.Text = strsz[8];
            SingleFieldSM.Enabled = false;

            //備註
            DSCLabelBZ.Text = "***備註:新世電子請假申請單";
            //代理人員
            DSCLabelDL.Text = "";
            //班別
            DSCLabelbb.Text = "正常班";
            //審核人
            DSCLabelSHRY.Text = "";
            //審核人二
            DSCLabelSHRER.Text = "";


            //簽核意見
            DataSet ds = fh_sj(SheetNo);

            #region MyRegion


            string table = "<table border='0' cellpadding='1' cellspacing='0' class='BasicFormHeadBorder'  style='width: 600px; '>";
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
           string xrrq= fh_rq(SheetNo);

           string table2 = "<table border='0' cellpadding='1' cellspacing='0' class='BasicFormHeadBorder'   style='width: 600px; '>";
           table2 += "<tr>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >流程名稱</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>請假申請單</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>單號</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + SheetNo + "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>填表日期</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + xrrq + "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>流程狀態</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>已結案</td>";           
          
           table2 += "</tr>";

           table2 += "<tr>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >重要性</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>低</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >主旨</td>";
           string zz = "請假單申請單[申請人:" + strsz[3] + "](" + SheetNo + ")";
           table2 += "<td colspan='5' class='BasicFormHeadDetail' style='font-size:8pt;' >" + zz + "</td>";
        
          
           table2 += "</tr>";


           table2 += "<tr>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>申請人代號</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + strsz[2] + "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;' >申請人姓名</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + strsz[3] + "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>申請單位代號</td>";

           string[] dep= strsz[1].Split('-');

           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + dep[0] + "</td>";
           table2 += "<td class='BasicFormHeadHead' style='font-size:8pt;'>申請單位名稱</td>";
           table2 += "<td class='BasicFormHeadDetail' style='font-size:8pt;'>" + dep[1] + "</td>";

           table2 += "</tr>";

           table2 += "</table>";

           div3.InnerHtml = table2;



        }
    }




    /// <summary>
    /// strsz[0] ==公司別
    /// strsz[1] ==部門
    /// strsz[2] ==工號
    /// strsz[3] ==姓名
    /// strsz[4] ==職稱
    /// strsz[5] ==天數
    /// strsz[6] ==假別
    /// strsz[7] ==時數
    /// strsz[8] ==說明
    /// strsz[9] ==開始日期
    /// strsz[10] ==截止日期
    /// strsz[11] ==開始時間
    /// strsz[12] ==截止時間          
    /// </summary>
    /// <param name="GUID"></param>
    public string[] show(string SheetNo)
    {
        string sql = @"select Company,Dep,EmpNo,EmpName,Title,Seniority,LeaveCategory,LeaveHours,LeaveReason,
MIN(LeaveDate) as LeaveDate ,MAX(LeaveDate) as LeaveDate,
MIN(LeaveBeginTime) as LeaveBeginTime ,MAX(LeaveEndTime) as LeaveEndTime from SmpHRLeave a join SmpHRLeaveDetail b
on a.GUID=b.DetailGUID 
where a.SheetNo='" + SheetNo + "'group by Company,Dep,EmpNo,EmpName,Title,Seniority,LeaveCategory,LeaveHours,LeaveReason ";

         //聲明一個 IOFactory 對象YFP
        IOFactory factory = new IOFactory();
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        DataSet ds = engine2.getDataSet(sql, "TEMP");

        int count = 13;
        string[] strsz = new string[count];

        for (int i = 0; i < count; i++)
        {
            strsz[i] = "";
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < count; j++)
            { //數據賦值
                strsz[j] = ds.Tables[0].Rows[i][j].ToString();
            }
        }

        /*
          strsz[0] ==公司別
          strsz[1] ==部門
          strsz[2] ==工號
          strsz[3] ==姓名
          strsz[4] ==職稱
          strsz[5] ==天數
          strsz[6] ==假別
          strsz[7] ==時數
          strsz[8] ==說明
          strsz[9] ==開始日期
          strsz[10] ==截止日期
          strsz[11] ==開始時間
          strsz[12] ==截止時間        
         */
        return strsz;

    }


  // exec  [dbo].[SmpHRLeavePrint] 'STHR00300000135'


    public DataSet fh_sj(string SheetNo)
    {
        string sql = "exec  [dbo].[SmpHRLeavePrint] '" + SheetNo + "'";

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
    public string fh_rq(string SheetNo)
    {
        string sql = "select D_INSERTTIME from SmpHRLeave where SheetNo='" + SheetNo + "' ";

        //聲明一個 IOFactory 對象YFP
        IOFactory factory = new IOFactory();
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        object obj = engine2.executeScalar(sql);
        return obj.ToString();


    }





}