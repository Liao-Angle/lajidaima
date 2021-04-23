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

public partial class SmpProgram_Form_SPAD005_PrintPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        System.IO.StreamWriter sw = null;
        try
        {
           // sw = new System.IO.StreamWriter(@"d:\temp\WebFormPT.log", true, System.Text.Encoding.Default);
			
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
           
            string strUserId = (string)Session["UserID"];
			string strSheetNo = (string)Session["SPAD005_SheetNo"] ;
            if(Session["SPAD005_SheetNo"] != null) 
            {
                strSheetNo = (string)Session["SPAD005_SheetNo"];
            }
			string strTripUserId = "";
            string strNow = DateTimeUtility.getSystemTime2(null);
            string strYear = strNow.Substring(0, 4);
			string strStartDate = "";
            string strEndDate = "";		
            string sql = null;
            DataSet ds = null;				


            //顯示使用者資訊
			sql = "select Subject, SheetNo, u.empNumber, u.empName, u.empEName, funcName, deptId, deptName, b.userName as agentName "; 
			sql = sql + " , (select userName from Users where OID=CheckByGUID or OID is null ) checkBy "; 
        	sql = sql + ", StartTrvlDate, EndTrvlDate, SiteUs, SiteJp, SiteKr, SiteSub, SiteOther, SiteUsDesc, SiteJpDesc, SiteKrDesc, SiteSubDesc, SiteOtherDesc "; 
        	sql = sql + ", FeeCharge, TrvlDesc, IdNumber, Birthday ";
        	sql = sql + ", PrePayTwd, PrePayTwdAmt, PrePayCny, PrePayCnyAmt, PrePayUsd, PrePayUsdAmt, PrePayJpy, PrePayJpyAmt, PrePayEur, PrePayEurAmt, PrePayOther, PrePayOtherAmt, PrePayComment "; 
        	sql = sql + ", ActualGetDate , (select userName from Users where OID=GetMemberGUID or OID is null ) getMember "; 
        	sql = sql + ", a.ChangeType,  (select top 1 SheetNo+'-'+Subject from SmpForeignTrvlV where GUID = a.OriForeignForm  ) OriFormInfo "; 
        	sql = sql + ", a.ChgStartTrvlDate, a.ChgEndTrvlDate, a.ChgTrvlDesc, ChgPrePayTwd, ChgPrePayTwdAmt, ChgPrePayCny, ChgPrePayCnyAmt "; 
        	sql = sql + ", ChgPrePayUsd, ChgPrePayUsdAmt, ChgPrePayJpy, ChgPrePayJpyAmt,ChgPrePayEur, ChgPrePayEurAmt,ChgPrePayOther, ChgPrePayOtherAmt "; 
			sql = sql + ",case u.orgId when 'SMP' then '新普科技' else '中普科技' end as OrgName ";
			sql = sql + "from SmpForeignTrvlChg a, EmployeeInfo u, Users b  "; 
			sql = sql + "where SheetNo='" + Utility.filter(strSheetNo) + "'  ";
			sql = sql + "and a.OriginatorGUID=u.empGUID  and b.OID = a.AgentGUID  "; 
            ds = engine.getDataSet(sql, "TEMP");
			//sw.WriteLine("sql :    "  + sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Subject.Text =  ds.Tables[0].Rows[0]["Subject"].ToString();
                SheetNo.Text = ds.Tables[0].Rows[0]["SheetNo"].ToString();
				CompanyCode.Text =  ds.Tables[0].Rows[0]["OrgName"].ToString();
				empNumber.Text = ds.Tables[0].Rows[0]["empNumber"].ToString();				
				empName.Text = ds.Tables[0].Rows[0]["empName"].ToString();
				empEName.Text =  ds.Tables[0].Rows[0]["empEName"].ToString();				
				empInfo.Text = empNumber.Text + '-' + empName.Text;
				strTripUserId = empNumber.Text; //簽名檔用
				
                funcName.Text = ds.Tables[0].Rows[0]["funcName"].ToString();
				deptId.Text = ds.Tables[0].Rows[0]["deptId"].ToString();
				deptName.Text = ds.Tables[0].Rows[0]["deptName"].ToString();
				deptInfo.Text = deptId.Text + '-' + deptName.Text;
				
				agentName.Text =  ds.Tables[0].Rows[0]["agentName"].ToString();
                checkBy.Text = ds.Tables[0].Rows[0]["checkBy"].ToString();
				if (checkBy.Text.Equals(""))
				{
					checkBy.Text = " - ";
				}
				StartTrvlDate.Text = ds.Tables[0].Rows[0]["StartTrvlDate"].ToString();
				EndTrvlDate.Text = ds.Tables[0].Rows[0]["EndTrvlDate"].ToString();
				
				ChgStartTrvlDate.Text = ds.Tables[0].Rows[0]["ChgStartTrvlDate"].ToString();
				ChgEndTrvlDate.Text = ds.Tables[0].Rows[0]["ChgEndTrvlDate"].ToString();
				
				if (!ChgStartTrvlDate.Text.Equals("") && !ChgEndTrvlDate.Text.Equals(""))
				{
					DateTime sDate = Convert.ToDateTime(ChgStartTrvlDate.Text);
	                DateTime eDate = Convert.ToDateTime(ChgEndTrvlDate.Text);
					TimeSpan ts = eDate - sDate ;
	                double days = ts.TotalDays + 1;
	                lbChgTrvlDateSumValue.Text = "出差天數：" + Convert.ToInt32(days).ToString() + " 天";
				}else{ lbChgTrvlDateSumValue.Text = " -- "; }
				
				if (!StartTrvlDate.Text.Equals("") && !EndTrvlDate.Text.Equals(""))
				{
					DateTime sDate = Convert.ToDateTime(StartTrvlDate.Text);
	                DateTime eDate = Convert.ToDateTime(EndTrvlDate.Text);
					TimeSpan ts = eDate - sDate ;
	                double days = ts.TotalDays + 1;
	                //lbTrvlDateSumValue.Text = "出差天數：" + Convert.ToInt32(days).ToString() + " 天";
					lbTrvlDateSumValue.Text = "   ";
				}else{ lbTrvlDateSumValue.Text = " -- "; }
				
				SiteUs.Text =  ds.Tables[0].Rows[0]["SiteUs"].ToString();
                SiteJp.Text = ds.Tables[0].Rows[0]["SiteJp"].ToString();
				SiteKr.Text = ds.Tables[0].Rows[0]["SiteKr"].ToString();
				SiteSub.Text = ds.Tables[0].Rows[0]["SiteSub"].ToString();
				SiteOther.Text =  ds.Tables[0].Rows[0]["SiteOther"].ToString();
                SiteUsDesc.Text = ds.Tables[0].Rows[0]["SiteUsDesc"].ToString();
				SiteJpDesc.Text = ds.Tables[0].Rows[0]["SiteJpDesc"].ToString();
				SiteKrDesc.Text = ds.Tables[0].Rows[0]["SiteKrDesc"].ToString();
				SiteSubDesc.Text =  ds.Tables[0].Rows[0]["SiteSubDesc"].ToString();
                SiteOtherDesc.Text = ds.Tables[0].Rows[0]["SiteOtherDesc"].ToString();
				if (SiteUs.Text.Equals("Y"))
				{
				  SiteInfo.Text = " 美國 - " + SiteUsDesc.Text;
				}
				if (SiteJp.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "日本 - " + SiteJpDesc.Text;
				}
				if (SiteKr.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "韓國 - " + SiteKrDesc.Text;
				}
				if (SiteSub.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "子公司 - " + SiteSubDesc.Text;
				}
				if (SiteOther.Text.Equals("Y"))
				{
				  SiteInfo.Text = SiteInfo.Text + "其他 - " + SiteOtherDesc.Text;
				}
				
				FeeCharge.Text = ds.Tables[0].Rows[0]["FeeCharge"].ToString();
				if (FeeCharge.Text.Equals("0"))
				{
				  FeeChargeInfo.Text = "新普";
				}
				else if (FeeCharge.Text.Equals("1"))
				{
				  FeeChargeInfo.Text = "新世";
				}
				else if (FeeCharge.Text.Equals("2"))
				{
				  FeeChargeInfo.Text = "新普(重慶) ";
				}
				else if (FeeCharge.Text.Equals("4"))
				{
				  FeeChargeInfo.Text = "中普";
				}
				else if (FeeCharge.Text.Equals("5"))
				{
				  FeeChargeInfo.Text = "太普";
				}
				else
				{
				  //FeeChargeInfo.Text = "合普";
				  FeeChargeInfo.Text = "-----";
				}
				
				TrvlDesc.Text = ds.Tables[0].Rows[0]["TrvlDesc"].ToString().Replace("\n", "<br>");
				
				ChgTrvlDesc.Text = ds.Tables[0].Rows[0]["ChgTrvlDesc"].ToString().Replace("\n", "<br>"); 
;
				//Replace( trim(rs1("spadg017")) ,chr(13), "<br>" )
				//IdNumber.Text =  ds.Tables[0].Rows[0]["IdNumber"].ToString();
				//IdNumber.Text = IdNumber.Text.Substring(0,3) + "*******";	
				
                //Birthday.Text = ds.Tables[0].Rows[0]["Birthday"].ToString();
				//Birthday.Text = "**/" + Birthday.Text.Substring(3,2) + "/**";
				
				PrePayTwd.Text =  ds.Tables[0].Rows[0]["PrePayTwd"].ToString();
                PrePayTwdAmt.Text = ds.Tables[0].Rows[0]["PrePayTwdAmt"].ToString();
				PrePayCny.Text = ds.Tables[0].Rows[0]["PrePayCny"].ToString();
				PrePayCnyAmt.Text = ds.Tables[0].Rows[0]["PrePayCnyAmt"].ToString();
				PrePayUsd.Text =  ds.Tables[0].Rows[0]["PrePayUsd"].ToString();
                PrePayUsdAmt.Text = ds.Tables[0].Rows[0]["PrePayUsdAmt"].ToString();
				PrePayJpy.Text =  ds.Tables[0].Rows[0]["PrePayJpy"].ToString();
                PrePayJpyAmt.Text = ds.Tables[0].Rows[0]["PrePayJpyAmt"].ToString();
				PrePayEur.Text =  ds.Tables[0].Rows[0]["PrePayEur"].ToString();
                PrePayEurAmt.Text = ds.Tables[0].Rows[0]["PrePayEurAmt"].ToString();
				PrePayOther.Text = ds.Tables[0].Rows[0]["PrePayOther"].ToString();
				PrePayOtherAmt.Text = ds.Tables[0].Rows[0]["PrePayOtherAmt"].ToString();
				if (PrePayTwd.Text.Equals("Y"))
				{
				  PrePayInfo.Text = "  新台幣   " + PrePayTwdAmt.Text + ";     ";
				}
				if (PrePayCny.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "    人民幣  " + PrePayCnyAmt.Text + ";     ";
				}
				if (PrePayUsd.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "     美金  " + PrePayUsdAmt.Text + ";     ";
				}
				if (PrePayJpy.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "     日圓  " + PrePayJpyAmt.Text + ";     ";
				}
				if (PrePayEur.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "     歐元  " + PrePayEurAmt.Text + ";     ";
				}
				if (PrePayOther.Text.Equals("Y"))
				{
				  PrePayInfo.Text = PrePayInfo.Text + "      其他  " + PrePayOtherAmt.Text + ";     ";
				}
				if (PrePayInfo.Text.Equals(""))
				{
				  PrePayInfo.Text = " - ";
				}
				
				PrePayComment.Text = ds.Tables[0].Rows[0]["PrePayComment"].ToString();
				if (PrePayComment.Text.Equals(""))
				{
				  PrePayComment.Text = " ---- ";
				}
				ActualGetDate.Text = ds.Tables[0].Rows[0]["ActualGetDate"].ToString();
				if (ActualGetDate.Text.Equals(""))
				{
				  ActualGetDate.Text = " -- ";
				}
				getMember.Text = ds.Tables[0].Rows[0]["getMember"].ToString();
				if (getMember.Text.Equals(""))
				{
				  getMember.Text = " -- ";
				}
				
				ChgPrePayTwd.Text =  ds.Tables[0].Rows[0]["ChgPrePayTwd"].ToString();
                ChgPrePayTwdAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayTwdAmt"].ToString();
				ChgPrePayCny.Text = ds.Tables[0].Rows[0]["ChgPrePayCny"].ToString();
				ChgPrePayCnyAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayCnyAmt"].ToString();
				ChgPrePayUsd.Text =  ds.Tables[0].Rows[0]["ChgPrePayUsd"].ToString();
                ChgPrePayUsdAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayUsdAmt"].ToString();
				ChgPrePayJpy.Text =  ds.Tables[0].Rows[0]["ChgPrePayJpy"].ToString();
                ChgPrePayJpyAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayJpyAmt"].ToString();
				ChgPrePayEur.Text =  ds.Tables[0].Rows[0]["ChgPrePayEur"].ToString();
                ChgPrePayEurAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayEurAmt"].ToString();
				ChgPrePayOther.Text = ds.Tables[0].Rows[0]["ChgPrePayOther"].ToString();
				ChgPrePayOtherAmt.Text = ds.Tables[0].Rows[0]["ChgPrePayOtherAmt"].ToString();
				if (ChgPrePayTwd.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = "  新台幣   " + ChgPrePayTwdAmt.Text + ";     ";
				}
				if (ChgPrePayCny.Text.Equals("Y"))
				{
				 ChgPrePayInfo.Text = ChgPrePayInfo.Text + "    人民幣  " + ChgPrePayCnyAmt.Text + ";     ";
				}
				if (ChgPrePayUsd.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = ChgPrePayInfo.Text + "     美金  " + ChgPrePayUsdAmt.Text + ";     ";
				}
				if (ChgPrePayJpy.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = ChgPrePayInfo.Text + "     日圓  " + ChgPrePayJpyAmt.Text + ";     ";
				}
				if (ChgPrePayEur.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = ChgPrePayInfo.Text + "     歐元  " + ChgPrePayEurAmt.Text + ";     ";
				}
				if (ChgPrePayOther.Text.Equals("Y"))
				{
				  ChgPrePayInfo.Text = ChgPrePayInfo.Text + "      其他  " + ChgPrePayOtherAmt.Text + ";     ";
				}
				if (ChgPrePayInfo.Text.Equals(""))
				{
				  ChgPrePayInfo.Text = " - ";
				}
				
				ChangeType.Text = ds.Tables[0].Rows[0]["ChangeType"].ToString();
				if (ChangeType.Text.Equals("0"))
				{
				  ChangeTypeInfo.Text = " 出差日異動 ";
				}
				else
				{
				  ChangeTypeInfo.Text = " 取消出差 ";
				}
				
				OriFormInfo.Text = ds.Tables[0].Rows[0]["OriFormInfo"].ToString();
				if (OriFormInfo.Text.Equals(""))
				{
				  OriFormInfo.Text = "  ---  ";
				}
            }
			
			try
			{
			//取得簽核人員
			string[] dr= null;
			string imgSerial = "0";
			string imgUserNo = "";
			string signItemName = "";
			string signTime = "";
			string signFileName = "";
			string url = "http://192.168.2.137/ECP/Personal/";
			string urlNa = "http://192.168.2.137/ECP/Personal/na.jpg";
			
			//預設值
			Img6.ImageUrl = urlNa;
			Img6.AlternateText = "董事長";
			Img6Time.Text = " ";
						
			Img5.ImageUrl = urlNa;
			Img5.AlternateText = "審核人5";
			Img5Time.Text = " ";
			
			Img4.ImageUrl = urlNa;
			Img4.AlternateText = "審核人4";
			Img4Time.Text = " ";
			
			Img3.ImageUrl = urlNa;
			Img3.AlternateText = "審核人3";
			Img3Time.Text = " ";
			
			Img2.ImageUrl = urlNa;
			Img2.AlternateText = "審核人2";
			Img2Time.Text = " ";
			
			Img1.ImageUrl = urlNa;
			Img1.AlternateText = "審核人1";
			Img1Time.Text = " ";
			
			sql = "select ROW_NUMBER() OVER(ORDER BY  wi.completedTime) AS ROWID, ur.id userId, ur.userName, wi.workItemName ,CONVERT(VARCHAR, wi.completedTime, 120 ) completedTime , fi.FILENAME+'.'+fi.FILEEXT imgName ";
			sql = sql + "from NaNa.dbo.WorkItem wi, NaNa.dbo.ProcessInstance pi, WebFormPT.dbo.SMWYAAA ya, NaNa.dbo.Users ur , WebFormPT.dbo.FILEITEM fi  ";
			sql = sql + "where wi.contextOID=pi.contextOID ";
			sql = sql + "and pi.serialNumber=ya.SMWYAAA005 and ur.OID = wi.performerOID and fi.LEVEL1 = ur.id ";
			sql = sql + "and wi.workItemName in ('ChargeSTCS','ChargeSCQ','審核人','直屬主管','審核人','董事長','處級','處級主管','部級') ";
			sql = sql + "and substring(SMWYAAA002,1,7) in ('SPAD004','SPAD005','SPAD006','SPAD007') ";
			sql = sql + "and ya.SMWYAAA002='" + Utility.filter(strSheetNo) + "'  ";
			ds = engine.getDataSet(sql, "TEMP");
			int rows = ds.Tables[0].Rows.Count;
			//string[][] result = new string[rows][];
           
		    for (int i = 0; i < rows; i++)
            {
				//dr = dt.NewRow();
				imgSerial = ds.Tables[0].Rows[i]["ROWID"].ToString();
				imgUserNo = ds.Tables[0].Rows[i]["userId"].ToString();
				signItemName = ds.Tables[0].Rows[i]["workItemName"].ToString();
				signTime =  ds.Tables[0].Rows[i]["completedTime"].ToString();
				signFileName = ds.Tables[0].Rows[i]["imgName"].ToString();
				if (imgSerial.Equals("1") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img1.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img1.AlternateText = signItemName;
			        Img1Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("2") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img2.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img2.AlternateText = signItemName;
			        Img2Time.Text = signTime;
				  }
				}
                if (imgSerial.Equals("3") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img3.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img3.AlternateText = signItemName;
			        Img3Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("4") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img4.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img4.AlternateText = signItemName;
			        Img4Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("5") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img5.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img5.AlternateText = signItemName;
			        Img5Time.Text = signTime;
				  }
				}
				if (imgSerial.Equals("6") )
				{
				  if (signItemName.Equals("董事長"))
				  {
					Img6.ImageUrl = url + imgUserNo + "/" + signFileName; 
					Img6.AlternateText = signItemName;
					Img6Time.Text = signTime;
				  }
				  else
				  {
				    Img6.ImageUrl = url +imgUserNo + "/" + signFileName;
			        Img6.AlternateText = signItemName;
			        Img6Time.Text = signTime;
				  }
				}
			}
			
			}
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

			//取得簽名檔與簽核時間
			//董事長
			/*
			Img6.ImageUrl = "http://192.168.2.226/ECP/Personal/1356/1356.gif";
			Img6.AlternateText = "董事長";
			Img6Time.Text = "2013/10/06 00:00:00";
			
			//string url = getPersonalImage(engine,strTripUserId);
			Img5.ImageUrl = "http://192.168.2.226/ECP/Personal/3787/3787.gif";
			Img5.AlternateText = "審核人5";
			Img5Time.Text = "2013/10/05 00:00:00";
			
			Img4.ImageUrl = "http://192.168.2.226/ECP/Personal/2556/2556.gif";
			Img4.AlternateText = "審核人4";
			Img4Time.Text = "2013/10/04 00:00:00";
			
			Img3.ImageUrl = "http://192.168.2.226/ECP/Personal/4074/4074.jpg";
			Img3.AlternateText = "審核人3";
			Img3Time.Text = "2013/10/03 00:00:00";
			
			Img2.ImageUrl = "http://192.168.2.226/ECP/Personal/3842/3842.gif";
			Img2.AlternateText = "審核人2";
			Img2Time.Text = "2013/10/02 00:00:00";
			*/
			//Img1.ImageUrl = "http://192.168.2.226/ECP/Personal/3992/3992.jpg";
			//Img1.AlternateText = "審核人1";
			//Img1Time.Text = "2013/10/01 00:00:00";
            
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
	
	
	
}