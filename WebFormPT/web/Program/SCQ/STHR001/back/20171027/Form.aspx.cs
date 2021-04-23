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
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Collections.Generic;


/*
// 新增日期: 2015/1/24
// 作者：FangPeng_Yan
// 內容說明： 人員需求申請單
 */

public partial class Program_System_Form_STAD003_Form : StcBasicFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            DSCLabelDateTime.Text = time;//為申請日期賦值


               string[,] strlb= new string[,] {
                                     {"請選擇","請選擇"},
                                      {"逅補直接員工","逅補直接員工"},
                                      {"新增直接員工","新增直接員工"},
                                       {"逅補間接員工","逅補間接員工"},                                      
                                       {"新增間接員工","新增間接員工"}
                                      };

               SingleDropDownListLb.setListItem(strlb); //新增類別 


                   string[,] strxl= new string[,] {
                                     {"請選擇","請選擇"},
                                      {"中專","中專"},
                                       {"高中","高中"},
                                      {"大專","大專"},
                                       {"本科","本科"},
                                    {"碩士","碩士"},
                                    {"博士","博士"},
                                     {"其它","其它"}
                                      };

            SingleDropDownListXl.setListItem(strxl);//學歷


           
            
        } 

    }

    /// <summary>
    /// 初始化參數。此方法需設定四個系統變數
    /// </summary>
    protected override void init()
    {
        ProcessPageID = "STHR001";
        AgentSchema = "WebServerProject.form.STHR001.SmpHrRequirementAgent";
        ApplicationID = "SYSTEM";
        ModuleID = "SAMPLE";
        //this.EnsureChildControls();
    }
    /// <summary>
    /// 初始化畫面元件
    /// 初始化資料物件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void initUI(com.dsc.kernal.factory.AbstractEngine engine, com.dsc.kernal.databean.DataObject objects)
    {
        try
        {
            #region 初始化控件

            SingleFieldAgeA.ValueText = "18";//年齡
            SingleFieldAgeB.ValueText = "45";//年齡
            SingleFieldRs.ValueText = "1";//申請人數
            #region 部門添加
        
           // string[,] strgs = base.getgsb(engine);
            string[,] strgs = new string[,]{
                {"SMP","重慶新普"},
                {"TP", "中普科技"},	
                {"STCS","新世電子"},
                {"STHP","華普電子"},
                {"STTP","太普電子"}
            };
            SingleDropDownListGS.setListItem(strgs);//添加公司

            sqzszg.setListItem(strgs);
            SingleDropDownListGS.ReadOnly = true;//不能選擇
            #endregion
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];
            string userId = (string)Session["UserId"];

            //申請人員
            APMONEY00SQR.clientEngineType = engineType;
            APMONEY00SQR.connectDBString = connectString;
            APMONEY00SQR.ValueText = si.fillerID; //預設帶出登入者
            APMONEY00SQR.doValidate(); //帶出人員開窗元件中的人員名稱  

            /*/申請人Email,英文名  (查詢部門)         
            string[] userValues = base.getUserInfoById2(engine, si.fillerID);

            if (userValues[0] != "")
            {
                SingleFieldBMMC.ValueText = userValues[6];//部門
            }
*/
            SingleFieldBMMC.ValueText = si.fillerOrgName;

            SingleFieldBMMC.ReadOnly = true;//部門不能寫


            //string[] orgUnitInfo = base.getOrgUnitInfo(engine, OriginatorDeptGUID.GuidValueText);// 查詢部門所有主管
            //if (!orgUnitInfo[3].Equals(""))
            //{

            //}
            ///-------------------------------------MFG1
            if (isNew())
            {
                string[,] xgqhzg = new string[,]{ {"0","請選擇"},
                {"1","張華謙"},
                {"2","田軍祥"},	
                //{"3","許文坤"},
            };
                sqzszg.setListItem(xgqhzg);
                ///
                if (si.fillerOrgID == "NQM010101")
                {
                    showzg.Visible = true;
                }
            }


            //主旨不顯示於發起單據畫面
            SheetNo.Display = false;
            Subject.Display = false;

            //改變工具列順序
            base.initUI(engine, objects);

            #endregion
        }
        catch (Exception e)
        {
            writeLog(e);
        }

    }

    /// <summary>
    /// 取 SubmitInfo 值得
    /// 初始化SubmitInfo 資料結構。利用系統預設狀況給予欄位初始值。
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
    /// 取得送單資訊
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
     
        si.ownerID = (string)Session["UserID"]; //表單關系人
        si.ownerName = (string)Session["UserName"];
        si.ownerOrgID = depData[0];
        si.ownerOrgName = depData[1];
        si.submitOrgID = depData[0];
        si.objectGUID = objects.getData("GUID");

        return si;
    }

    /// <summary>
    /// 發起第一步
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    /// <returns></returns>
    protected override bool checkFieldData(AbstractEngine engine, DataObject objects)
    {
        bool result = true;

        string actName = Convert.ToString(getSession("ACTName"));
        string[] values = null;


        if (actName.Equals(""))
        {
            //設定主旨
            if (Subject.ValueText.Equals(""))
            {
                values = base.getUserInfo(engine, APMONEY00SQR.GuidValueText);
                string subject = "申请人:"+values[1];
                if (Subject.ValueText.Equals(""))
                {
                    Subject.ValueText = subject;
                }
            }
        }
        if (!SingleFieldXyrs.ValueText.Equals(""))
        {// 現有編制 不等於空
            try
            {
                int i = Convert.ToInt32(SingleFieldXyrs.ValueText);
            }
            catch 
            {
                pushErrorMessage("現有編制 應為數字");
                result = false;
            }
        }

        if (!SingleFieldRs.ValueText.Equals(""))
        {// 需求人數 不等於空
            try
            {
                int i = Convert.ToInt32(SingleFieldRs.ValueText);
            }
            catch
            {
                pushErrorMessage("需求人數 應為數字");
                result = false;
            }
        }


        if (!SingleFieldAgeA.ValueText.Equals(""))
        {// 年齡 不等於空
            try
            {
                int i = Convert.ToInt32(SingleFieldAgeA.ValueText);
            }
            catch
            {
                pushErrorMessage("年齡 應為數字");
                result = false;
            }
        }

	    SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText == "0")
            {
 		pushErrorMessage("MFG-1 部門 請選擇簽核主管");
                result = false;
            }

	
        if (!SingleFieldAgeB.ValueText.Equals(""))
        {// 年齡 不等於空
            try
            {
                int i = Convert.ToInt32(SingleFieldAgeB.ValueText);
            }
            catch
            {
                pushErrorMessage("年齡 應為數字");
                result = false;
            }
        }
        //類別不能為請選擇
        if (SingleDropDownListLb.ValueText == "請選擇")
        {
            pushErrorMessage("請選擇類別");
            result = false;            
        }
        if (SingleDropDownListXl.ValueText == "請選擇")
        {
            pushErrorMessage("請選擇學歷");
            result = false; 
        }
        if (SingleDateTimeFieldYdateTime.ValueText.Length == 0)
        {
            pushErrorMessage("請選擇預錄用日期");
            result = false;
        }
        else
        {

            DateTime dt = Convert.ToDateTime(SingleDateTimeFieldYdateTime.ValueText);//預錄用日期
            DateTime dtsqrq=Convert.ToDateTime(DSCLabelDateTime.Text);//申請日期
            if (dtsqrq > dt)
            {
                pushErrorMessage("預錄用日期不能小於當前日期");
                result = false;
            }
        }

        return result;
    }

    public bool getpartname(string str)
    {
        if (str.Length < 7)
        { return false; }
        str = str.Substring(0, 7);
        if (str == "NQM0301" || str == "NQM0302" || str == "NQM0304" || str == "NQM0305" || str == "NQM0308" || str == "NQM0309" || str == "NQMMM03" || str == "NQM0101" || str == "NQM0501" || str == "NQM0502" || str == "NQM0503" || str == "NQM0504" || str == "NQM0505" || str == "NQM0506")
        {
            return true;
        }
        else
        { return false; }
    }


    /// <summary>
    /// 設定流程變數
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="param"></param>
    /// <param name="currentObject"></param>
    /// <returns></returns>
    protected override string setFlowVariables(AbstractEngine engine, Hashtable param, DataObject currentObject)
    {
        string xml = "";
        try
        {
            SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
            //填表人
            string creatorId = si.fillerID;

            //申請人
            string originatorGUID = APMONEY00SQR.GuidValueText;
            string originatorId = APMONEY00SQR.ValueText;//工號
             string managerId="";
             string[] values = base.getUserManagerInfo(engine, originatorGUID);
            if (si.fillerOrgID == "NQM010101" && sqzszg.ValueText != "0")
            {
 		if (sqzszg.ValueText == "1")
                {
                    managerId = "Q1100135";
                }
		else if (sqzszg.ValueText == "2")
                {
                    managerId = "Q1608418";
                }
                else if (sqzszg.ValueText == "3"){
                    managerId = "Q1210122";
                }
            }
            else {
               
                managerId = values[1];  //申請人的主管 工號
            
            }

            //string[] strgr = FH_GRXX(originatorId);//獲取個人信息
            //string[] zc ={ "副理", "專案經理", "經理", "資深經理", "處長", "特助", "副總經理", "總經理" };

            //bool isb= fh_sziscz(zc, strgr[2]);
            //是否簽核  1=部級；2=處級;3=總經理；4=董事長   
            /*新增直接員工 逅補直接員工 簽到總經理  其他的簽到董事長 */
             /*逅補間接員工 新增間接員工 簽到董事長 */

            string sjzg = "";

            sjzg = base.getUserManagerInfo(engine, values[0])[1];

            string isfz = "";
            if ((getpartname(si.fillerOrgID)))
            {

                isfz = "1";
            }

		 int isqh=3;
	     //string isqh="Q1406670";
             if (SingleDropDownListLb.ValueText == "逅補間接員工"
                || SingleDropDownListLb.ValueText == "新增間接員工")
            {
	        	isqh =4;
                sjzg = "0";
                //isqh = "Q1300037";
            }            
           
            xml += "<STAD001_C163277>";
            xml += "<txtTBR DataType=\"java.lang.String\">" + creatorId + "</txtTBR>";//填表人
            xml += "<txtSqr DataType=\"java.lang.String\">" + originatorId + "</txtSqr>";//申請人
            xml += "<txtShr DataType=\"java.lang.String\">" + sjzg + "</txtShr>";//上一級主管
            xml += "<txtZszg DataType=\"java.lang.String\">" + managerId + "</txtZszg>";//直屬主管
	      xml += "<isfz DataType=\"java.lang.String\">" + isfz + "</isfz>";
            xml += "<txtCbr DataType=\"java.lang.String\">Q1100494</txtCbr>";//承办人
            xml += "<txtCbrzg DataType=\"java.lang.String\">Q1100103</txtCbrzg>"; //承办人主管      
            xml += "<txtisqh DataType=\"java.lang.String\">"+isqh+"</txtisqh>"; //是否簽核  1=部級；2=處級;3=總經理；4=董事長     
            xml += "</STAD001_C163277>";

        }
        catch (Exception e)
        {
            writeLog(e);
        }
        finally
        {
        }
        //表單代號
        param["STAD001_C163277"] = xml;
        return "STAD001_C163277";
    }
    /// <summary>
    /// 查看字符串數組中是否有這邊記錄
    /// </summary>
    /// <param name="sz">數組</param>
    /// <param name="str">字符串</param>
    /// <returns></returns>
    public bool fh_sziscz(string[] sz,string str)
    {
        bool isb=false;
        foreach (string var in sz)
        {
            if (str == var)
                isb = true;
        }
       return isb;
 
    }


    /// <summary>
    /// 返回個人信息
    /// </summary>
    /// <param name="empno">工號</param>
    /// <returns></returns>
    public string[] FH_GRXX(string empno)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string sql = "select EmpNo,EmpName,DtName,PartName,InCumbency,EmpTypeName from EmployeeHR where EmpNo='"+empno+"'";

       DataSet ds=  engine.getDataSet(sql,"1");
        string[] strsz=new string [6];
        if(ds.Tables[0].Rows.Count>0)
        {
            for (int i = 0; i < 6; i++)
            {
                strsz[i] = ds.Tables[0].Rows[0][i].ToString();
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                strsz[i] = "";
            }
        }

        return strsz;


 
    }


    /// <summary>
    /// 顯示控件
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void showData(AbstractEngine engine, DataObject objects)
    {
        bool isAddNew = base.isNew();
        //顯示單號
        base.showData(engine, objects);
        //单号
        SheetNo.ValueText = objects.getData("SheetNo");
        //主旨
        Subject.ValueText = objects.getData("Subject");
        //申請人
        APMONEY00SQR.GuidValueText = objects.getData("ApplicationGUID"); //將值放入人員開窗元件中, 資料庫存放GUID
        APMONEY00SQR.doGUIDValidate(); //顯示開窗元件的人員名稱, 因人員為GUID

        //表單欄位
        //公司別
        SingleDropDownListGS.ValueText = objects.getData("TheCompany");
      
    

        SingleFieldBMMC.ValueText = objects.getData("ApplicationPartname");//部門

        DSCLabelDateTime.Text = objects.getData("ApplicationDatetime");//申請日期
        SingleFieldXyrs.ValueText = objects.getData("NowCount");//現有人數
        SingleDateTimeFieldYdateTime.ValueText = objects.getData("RecruitmentDate");//預錄用日期
        SingleDropDownListLb.ValueText = objects.getData("PersonnelType");//人員類型
        SingleFieldRs.ValueText = objects.getData("PersonnelCount");//需求人數

        string[]  strage=objects.getData("DemandAge").Split('~');

        SingleFieldAgeA.ValueText = strage[0];//年齡
        SingleFieldAgeB.ValueText=strage[1];


        SingleFieldXqyy.ValueText = objects.getData("DemandReason");//需求原因
        SingleFieldZwxq.ValueText = objects.getData("PositionDemand");//職位需求
        SingleDropDownListXl.ValueText = objects.getData("Schooling");//學歷

        string sex= objects.getData("DemandSex");//性別
        switch (sex)
        {
            case "男":
                DSCRadioButtonSexMen.Checked = true;
                break;
            case "女":
                DSCRadioButtonSexWoman.Checked = true;
                break;
            case "不拘":
                DSCRadioButtoSexUnlimited.Checked = true;
                break;
        }

       string by=objects.getData("IsMilitaryservice");//是否兵役
        if(by=="役畢")
            DSCRadioButton1.Checked=true;
        else
            DSCRadioButton2.Checked = true;

        SingleFieldKx.ValueText = objects.getData("DemandDepartment");//科系
        SingleFieldJtjnrgtz.ValueText = objects.getData("DemandSkill");//技能
        SingleFieldGzzwsm.ValueText = objects.getData("DemandPosition");//職務說明

        string actName = Convert.ToString(getSession("ACTName"));
        　　

        if (!isAddNew)
        {
            //表單發起後不允許修改
            Subject.ReadOnly = true;

            APMONEY00SQR.ReadOnly = true;
            SingleDropDownListGS.ReadOnly = true;
            SingleFieldBMMC.ReadOnly = true;
            DSCLabelDateTime.ReadOnly = true;
            SingleFieldXyrs.ReadOnly = true;
            SingleDateTimeFieldYdateTime.ReadOnly = true;
            SingleDropDownListLb.ReadOnly = true;
            SingleFieldRs.ReadOnly = true;
            SingleFieldAgeA.ReadOnly = true;
            SingleFieldAgeB.ReadOnly = true;
            SingleFieldXqyy.ReadOnly = true;//需求原因
            SingleFieldZwxq.ReadOnly = true;//職位需求
            SingleDropDownListXl.ReadOnly = true;//學歷

            DSCRadioButtonSexMen.ReadOnly = true;

            DSCRadioButtonSexWoman.ReadOnly = true;

            DSCRadioButtoSexUnlimited.ReadOnly = true;
            DSCRadioButton1.ReadOnly=true;
            DSCRadioButton2.ReadOnly = true;

            SingleFieldKx.ReadOnly = true;//科系
            SingleFieldJtjnrgtz.ReadOnly = true;//技能
            SingleFieldGzzwsm.ReadOnly = true;//技能 

        } 
    } 
    /// <summary>
    /// 發起后第二步執行
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.factory.AbstractEngine engine, DataObject objects)
    {
        SubmitInfo si = (SubmitInfo)getSession("SubmitInfo");
        bool isAddNew = base.isNew();
        if (isAddNew)
        {
            base.saveData(engine, objects);//保存單號
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("DATA_STATUS", "Y");
            #region 表單數據
            objects.setData("Subject", Subject.ValueText);
            //申請人GUID
            objects.setData("ApplicationGUID", APMONEY00SQR.GuidValueText);
            //填表人工號
            objects.setData("ApplicationEmpno", APMONEY00SQR.ValueText);
            //填表人姓名
            objects.setData("ApplicationEmpname", APMONEY00SQR.ReadOnlyValueText);
            //填表人部門ID
            objects.setData("ApplicationPartno", SingleFieldBMMC.ValueText);
            //填表人部門名稱
            objects.setData("ApplicationPartname", SingleFieldBMMC.ValueText);
            //公司名稱
             objects.setData("TheCompany", SingleDropDownListGS.ValueText);


            //申請日期
            objects.setData("ApplicationDatetime", DSCLabelDateTime.Text);
            //現在有人數
            objects.setData("NowCount", SingleFieldXyrs.ValueText);
            //預錄用日期 
            objects.setData("RecruitmentDate", SingleDateTimeFieldYdateTime.ValueText);
            //人員類型
            objects.setData("PersonnelType", SingleDropDownListLb.ValueText);
            //申請人員個數
            objects.setData("PersonnelCount", SingleFieldRs.ValueText);
            //需求原因
            objects.setData("DemandReason", SingleFieldXqyy.ValueText);
            //職位需求
            objects.setData("PositionDemand", SingleFieldZwxq.ValueText);
            //學歷
            objects.setData("Schooling", SingleDropDownListXl.ValueText);
            //需求性別
            string strsex = "";
            if (DSCRadioButtonSexMen.Checked)//男
                strsex = DSCRadioButtonSexMen.Text;
            else if (DSCRadioButtonSexWoman.Checked)//女
                strsex = DSCRadioButtonSexWoman.Text;
            else if (DSCRadioButtoSexUnlimited.Checked)//不拘
                strsex = DSCRadioButtoSexUnlimited.Text;
            objects.setData("DemandSex", strsex);
            //需求年齡
            objects.setData("DemandAge", SingleFieldAgeA.ValueText + "~" + SingleFieldAgeB.ValueText);
            //是否兵役
            string strby = "";
            if (DSCRadioButton1.Checked)//役畢
                strby = DSCRadioButton1.Text;
            else
                strby = DSCRadioButton2.Text;
            objects.setData("IsMilitaryservice", strby);
            //DemandDepartment  需求科系
            objects.setData("DemandDepartment", SingleFieldKx.ValueText);
            //需求技能
            objects.setData("DemandSkill", SingleFieldJtjnrgtz.ValueText);
            //工作職務說明
            objects.setData("DemandPosition", SingleFieldGzzwsm.ValueText);

            #endregion
        } 
        setSession("IsSetFlow", "Y");
        //如果要调用 签核前的方法 必须要有的
        setSession("IsAddSign","AFTER");
    }

    /// <summary>
    /// 第四步
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
    /// 退回填表人那裡
    /// </summary>
    protected override void rejectProcedure()
    {
        //退回填表人終止流程
        String backActID = Convert.ToString(Session["tempBackActID"]); //退回後關卡ID

        if (backActID.ToUpper().Equals("CREATOR"))//CREATOR填表人
        {
            try
            {
                base.terminateThisProcess();

            }
            catch (Exception e)
            {
                base.writeLog(e);
                throw new Exception(e.StackTrace);
            }
        }
        else
        {
            base.rejectProcedure();
        }
    }


    /// <summary>
    /// APMONEY00SQR  選擇事件
    /// </summary>
    /// <param name="values"></param>
    protected void APMONEY00SQR_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        //string connectString = (string)Session["connectString"];
        //string engineType = (string)Session["engineType"];
        try
        {
            //申請人Email,英文名           
            string[] userValues = base.getUserInfoById2(engine, APMONEY00SQR.ValueText);
            if (userValues[0] != "")
            {
                SingleDropDownListGS.ValueText = userValues[5]; //公司別
                SingleFieldBMMC.ValueText = userValues[6];//部門

              //  MessageBox(userValues[5]);
            }
            else
            {
                SingleFieldBMMC.ValueText = "";//部門
            }
        }
        catch (Exception e)
        {
            writeLog(e);
        }
       
    }

    /// <summary>
    /// 發起 送單后處理 (相對于填表人)
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="currentObject"></param>
    protected override void afterSend(AbstractEngine engine, DataObject currentObject)
    {
        try
        {
            //MessageBox("送單后處理");
        }
        catch (Exception ex)
        {
           // MessageBox(ex.Message);
            //throw;
        }
       base.afterSend(engine, currentObject);
    }
    /// <summary>
    /// 簽核前的方法處理
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="isAfter"></param>
    /// <param name="addSignXml"></param>
    /// <returns></returns>
    protected override string beforeSign(AbstractEngine engine, bool isAfter, string addSignXml)
    {
        //获取签核 关卡
        string actName = Convert.ToString(getSession("ACTName"));
      
       //MessageBox(actName);
        /*可以在直属主管操作内容*/
        //if (actName == "直屬主管")
        //{
        //    //
        //    string connectString = "Data Source=192.168.80.18;Initial Catalog=door;User ID=kitty;Password=misap3173";//(string)Session["connectString"];//数据库连接语句
        //   // engineType = (string)Session["engineType"];
        //    IOFactory factory = new IOFactory();
        //    AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, connectString);
        //    string sql = "select count(*) from zlcx.dbo.zL_accounts";//sql语句
        //    object obj = engine2.executeScalar(sql);
        //   MessageBox(obj.ToString());
        //}
        return base.beforeSign(engine, isAfter, addSignXml);
    }
 


}
