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

public partial class SmpProgram_maintain_SPTS002_Detail : BaseWebUI.DataListInlineForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
		string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string[,] ids = null;

        DeptGUID.clientEngineType = (string)Session["engineType"];
        DeptGUID.connectDBString = (string)Session["connectString"];
				
		//內外訓
        ids = new string[,] {
            {"",""},
            {"I",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_detail_aspx.language.ini", "message", "I", "內訓")},
            {"O",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_detail_aspx.language.ini", "message", "O", "外訓")}
        };
        InOut.setListItem(ids);
        //InOut.ReadOnly = true;

        //課程類別
        ids = new string[,]{ 
            {"",""},
            {"1",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_detail_aspx.language.ini", "message", "1", "新人訓練")},
            {"2",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_detail_aspx.language.ini", "message", "2", "專業職能")},
            {"3",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_detail_aspx.language.ini", "message", "3", "管理職能")},
            {"4",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_detail_aspx.language.ini", "message", "4", "品質管理")},
            {"5",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts002_detail_aspx.language.ini", "message", "5", "安全衛生")} 
        };
        SubjectType.setListItem(ids);

        SubjectNo.ReadOnly = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
		bool isNew = (bool)getSession("isNew");

        SubjectNo.ValueText = objects.getData("SubjectNo");
        SubjectName.ValueText = objects.getData("SubjectName");
				
		DeptGUID.GuidValueText = objects.getData("DeptGUID");
        DeptGUID.doGUIDValidate();

		TrainingHours.ValueText = objects.getData("TrainingHours");
        SubjectType.ValueText = objects.getData("SubjectType");
		InOut.ValueText = objects.getData("InOut");
        ExpiryDate.ValueText = objects.getData("ExpiryDate");

        string sessionCompCode = (string)Session["SPTS002_ICompCode"];
        CompanyCode.ValueText = sessionCompCode; //產生流水號用
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
		IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        //string sql = null;
		
		bool isNew = (bool)getSession("isNew");
        string id = SubjectName.ValueText;
        string sessionCompCode = (string)Session["SPTS002_ICompCode"];
        Boolean hasDuplicate =false;
        //Check Data 
        //string traineeId = (string)getSession((string)Session["UserID"], "TraineeId");
        ArrayList subjectInfo = (ArrayList)getSession((string)Session["UserID"], "SubjectInfo");

        if (subjectInfo == null)
            throw new Exception("查無Session:planInfo，無法判斷資料是否重覆");
        else
        {
            if (isNew)
            {
                foreach (var info in subjectInfo)
                {
                    if (info.ToString().IndexOf(id) != -1)
                    {
                        hasDuplicate = true;
                        break;
                    }
                }
            }
            else
            {
                string guid = objects.getData("GUID");

                foreach (var info in subjectInfo)
                {

                    if (info.ToString().IndexOf(guid) == -1 && info.ToString().IndexOf(id) != -1)
                    {
                        hasDuplicate = true;
                        break;
                    }
                }

            }

            if (hasDuplicate)
            {
                throw new Exception("課程名稱重覆!");
            }
            else
            {	
		        if (isNew)
		        {
		            objects.setData("GUID", IDProcessor.getID(""));
		            objects.setData("SubjectFormGUID", "temp");
                    string seqNo = getCustomCourseNo(engine, "SMPTSSubjectNo", sessionCompCode);
		            //Page.Response.Write("alert('seqNO:" + seqNo + "');");
		            objects.setData("SubjectNo", seqNo);
		            objects.setData("IS_DISPLAY", "Y");
		            objects.setData("IS_LOCK", "N");
		            objects.setData("DATA_STATUS", "Y");
		        }

                //objects.setData("SubjectNo", SubjectNo.ValueText);
                objects.setData("SubjectName", SubjectName.ValueText);
		        objects.setData("DeptGUID", DeptGUID.GuidValueText);
                objects.setData("deptId", DeptGUID.ValueText);
                objects.setData("deptName", DeptGUID.ReadOnlyValueText);
				objects.setData("TrainingHours", TrainingHours.ValueText);
		        objects.setData("InOut", InOut.ValueText);
                objects.setData("SubjectType", SubjectType.ValueText);
                objects.setData("ExpiryDate", ExpiryDate.ValueText);
                objects.setData("CompanyCode", CompanyCode.ValueText);
				
				//檢查欄位資料
		        string errMsg = checkFieldData(objects, engine);
		        engine.close();
		        if (!errMsg.Equals(""))
		        {
		            errMsg = errMsg.Replace("\n", "; ");
		            throw new Exception(errMsg);
		        }
			}
		}

    }
	
	/// <summary>
    /// 取得編號
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    protected string getCustomCourseNo(AbstractEngine engine, string code, string strCompanyID)
    {
        string sheetNo = "";
        try
        {                    
            object codeId = engine.executeScalar("select SMVIAAA002 from SMVIAAA where SMVIAAA002='" + code + "'");
            WebServerProject.AutoCode ac = new WebServerProject.AutoCode();
            ac.engine = engine;
            Hashtable hs = new Hashtable();
            hs.Add("CompanyID", strCompanyID);
            sheetNo = ac.getAutoCode(Convert.ToString(codeId), hs).ToString();
        }
        catch (Exception e)
        {
            base.writeLog(e);
        }
        return sheetNo;
    }
	
	 public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        bool isNew = (bool)getSession("isNew");
        string sql = null;
		int cnt = 0;
        string guid = objects.getData("GUID");
        string companyCode = objects.getData("CompanyCode");
		string deptGUID = objects.getData("DeptGUID");
        string subjectName = objects.getData("SubjectName");
        string subjectNo = objects.getData("SubjectNo");
		string inOut = objects.getData("InOut");
        string subjectType = objects.getData("SubjectType");
		
		if (deptGUID.Equals(""))
        {
            errMsg += "請選擇[" + lblDeptGUID.Text + "]! \n";
        }
        if (subjectName.Equals(""))
        {
            errMsg += "請維護[" + lblSubjectName.Text + "]! \n";
        }        
		if (inOut.Equals(""))
        {
            errMsg += "請選擇[" + lblInOut.Text + "]!\n";
        }
        if (subjectType.Equals(""))
        {
            errMsg += "請選擇[" + lblSubjectType.Text + "]!\n";
        }
		
        //課程名稱+開課部門 不可重覆
        if (isNew)
        {
            sql = "select count(*) from SmpTSSubjectDetail where DeptGUID+SubjectName='" + deptGUID + subjectName + "' ";
            cnt = (int)engine.executeScalar(sql);
            if (cnt > 0)
            {
                errMsg += "課程名稱+開課部門不可重覆! \n";
            }
        }
				
		decimal hours = 0;
		string chkTrainingHours = objects.getData("TrainingHours");
        //訓練時數不可為零
        if (!chkTrainingHours.Equals(""))
        {
            hours = Convert.ToDecimal(chkTrainingHours);
        }
        if (hours <= 0)
        {
            errMsg += "訓練時數必需大於零! \n";
        }
		
        return errMsg;
    }
	
	//訓練時數為數字
    protected void TrainingHours_TextChanged(object sender, EventArgs e)
    {
        double n;
        if (!double.TryParse(TrainingHours.ValueText, out n)) 
        {
            MessageBox("訓練時數需為數字格式");
            TrainingHours.ValueText = "";
            TrainingHours.Focus();
        }
    }


    protected void DeptGUID_BeforeClickButton()
    {
        bool isNew = (bool)getSession("isNew");

        if (isNew)
        {
            string companyCode = CompanyCode.ValueText;
            string companyName = "";
            if (companyCode.Equals("SMP"))
            {
                companyName = "新普科技";
            }
            else if (companyCode.Equals("TP"))
            {
                companyName = "中普科技";
            }
            DeptGUID.whereClause = " ( organizationName='" + companyName + "' ) ";
        }
    }
}
