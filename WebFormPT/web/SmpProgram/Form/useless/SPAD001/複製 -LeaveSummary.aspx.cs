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

public partial class SmpProgram_Form_SPAD001_LeaveSummary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        AbstractEngine erpEngine = null;
        System.IO.StreamWriter sw = null;
        try
        {
            //sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            string companyCode = (string)Session["SPAD001_CompanyCode"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam(companyCode + "WorkFlowERPDB");
            erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
            
            //string strUserId = Request["originatorId"];
            string strUserGUID = (string)Session["UserGUID"];
            string strUserId = (string)Session["UserID"];
            if (Session["SPAD001_OriginatorGUID"] != null) 
            {
                strUserGUID = (string)Session["SPAD001_OriginatorGUID"];
            }
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
            string strStartDate = Convert.ToInt32(strYear) - 1 + "1226";
            string sql = null;
            DataSet ds = null;

            //顯示使用者資訊
            sql = "select u.userName, u.id, o.organizationUnitName from Functions f inner join OrganizationUnit o on organizationUnitOID=o.OID inner join Users u on  f.occupantOID = u.OID where u.OID='" + Utility.filter(strUserGUID) + "'  and isMain='1'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                strUserId = ds.Tables[0].Rows[0]["id"].ToString();
                UserId.Text = strUserId;
                UserName.Text =  ds.Tables[0].Rows[0]["userName"].ToString();
                OrgUnitName.Text = ds.Tables[0].Rows[0]["organizationUnitName"].ToString();
            }
            
            //顯示特休時數資訊
            sql = "select PALTK.*,MV021 from PALTK,CMSMV where TK001='" + strUserId + "' and TK002 ='" + strYear + "' and TK001=MV001";
            ds = erpEngine.getDataSet(sql, "TEMP");
            //TK900: 上年度未休, TK902: 上年度已休, TK007: 本年度特休, TK008: 本年度已休
            if (ds.Tables[0].Rows.Count > 0)
            {
                string yearHours = "";
                string usedHours = "";
                string notUsedHours = "";
                usedHours = ds.Tables[0].Rows[0]["TK902"].ToString();
                notUsedHours = ds.Tables[0].Rows[0]["TK900"].ToString();
                decimal reserveHours = 0;
                if (!notUsedHours.Equals("") && !usedHours.Equals(""))
                {
                    reserveHours = Convert.ToDecimal(notUsedHours) - Convert.ToDecimal(usedHours); //上年度未休時數-上年度已休時數【此值若大於56小時，最多只能計算56小時】
                    if (reserveHours > 7 * 8)
                    {
                        reserveHours = 7 * 8;
                    }
                }
                
                string arriveDate = ds.Tables[0].Rows[0]["MV021"].ToString().Substring(4);
                string strNowDate = strNow.Replace("/", "").Substring(4,4);
                if (Convert.ToDecimal(arriveDate) > Convert.ToDecimal(strNowDate))
                {
                    
                    //今天的日期小於到職日
                    if (!notUsedHours.Equals("") && !usedHours.Equals(""))
                    {
                        YearHours.Text = Convert.ToString((int)Convert.ToDecimal(notUsedHours)/1);
                        UsedHours.Text = Convert.ToString((int)Convert.ToDecimal(usedHours) / 1);
                        decimal hours = Convert.ToDecimal(notUsedHours) - Convert.ToDecimal(usedHours);
                        NotUsedHours.Text = Convert.ToString((int)hours/1);
                    }
                }
                else 
                {
                    //今天的日期大於等於到職日
                    usedHours = ds.Tables[0].Rows[0]["TK008"].ToString();
                    yearHours = ds.Tables[0].Rows[0]["TK007"].ToString();
                    
                    decimal hours = Convert.ToDecimal(yearHours) + Convert.ToDecimal(reserveHours) - Convert.ToDecimal(usedHours);
                    
                    YearHours.Text = Convert.ToString((int)(Convert.ToDecimal(yearHours) + Convert.ToDecimal(reserveHours))/1);
                    UsedHours.Text = Convert.ToString((int)Convert.ToDecimal(usedHours) / 1);
                    NotUsedHours.Text = Convert.ToString((int)hours/1);
                    
                }
                
                if (ds.Tables[0].Rows[0]["TK904"].ToString().Equals("Y"))
                {
                    YearHours.Text = "0";
                    NotUsedHours.Text = "0";
                }
            }
            
            //顯示請假明細
            sql = "select TF002,TF003,MK002,MC002,TF005,TF006,TF008,TF010,TF011 from PALTF,PALMK,PALMC where TF001='" + strUserId + "' and TF011='Y' and TF002 >='" + strStartDate + "' and PALTF.TF014=PALMK.MK001 and PALTF.TF004=PALMC.MC001 order by PALTF.TF002,PALTF.TF003";
            ds = erpEngine.getDataSet(sql, "TEMP");

            // Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow dr;
 
            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("請假日期 ", typeof(string)));
            dt.Columns.Add(new DataColumn("序號", typeof(string)));
            dt.Columns.Add(new DataColumn("班別", typeof(string)));
            dt.Columns.Add(new DataColumn("假別", typeof(string)));
            dt.Columns.Add(new DataColumn("請假起迄時間", typeof(string)));
            dt.Columns.Add(new DataColumn("請假時數", typeof(string)));
            dt.Columns.Add(new DataColumn("備註", typeof(string)));
            dt.Columns.Add(new DataColumn("確認碼", typeof(string)));

            int rows = ds.Tables[0].Rows.Count;
            string[][] result = new string[rows][];
            for (int i = 0; i < rows; i++)
            {
                dr = dt.NewRow();
                dr[0] = ds.Tables[0].Rows[i]["TF002"].ToString();
                dr[1] = ds.Tables[0].Rows[i]["TF003"].ToString();
                dr[2] = ds.Tables[0].Rows[i]["MK002"].ToString();
                dr[3] = ds.Tables[0].Rows[i]["MC002"].ToString();
                dr[4] = ds.Tables[0].Rows[i]["TF005"].ToString() + "~" + ds.Tables[0].Rows[i]["TF006"].ToString();
                dr[5] = ds.Tables[0].Rows[i]["TF008"].ToString();
                dr[6] = ds.Tables[0].Rows[i]["TF010"].ToString();
                string confirmCode = ds.Tables[0].Rows[i]["TF011"].ToString();
                string confirmName = "";
                if(confirmCode.Equals("N")) {
                    confirmName = "未確認";
                } else if(confirmCode.Equals("Y")) {
                    confirmName = "已確認";
                } else if(confirmCode.Equals("V")) {
                    confirmName = "已作廢";
                }
                dr[7] = confirmName;
                dt.Rows.Add(dr);
            }
            DataView dv = new DataView(dt);

            gvLeaveSummary.DataSource = dv;
            gvLeaveSummary.DataBind();
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
        finally
        {
            if (engine != null) engine.close();
            if (erpEngine != null) erpEngine.close();
            if (sw != null) sw.Close();
        }
    }
}