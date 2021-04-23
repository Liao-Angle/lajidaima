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

public partial class SmpProgram_Input : BaseWebUI.DataListSaveForm
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
                string[,] ids = null;
                //公司別
                string userGUID = (string)Session["UserGUID"];
                string sql = "select h.CompanyCode, c.CompanyName from SmpTSAdmForm h, SmpTSAdmDetail l, SmpTSCompanyV c where l.AdmType='1'  and AdmTypeGUID='" + userGUID + "' and h.GUID = l.AdmFormGUID and h.CompanyCode = c.CompanyCode "
                            + "union select h.CompanyCode, c.CompanyName from SmpTSAdmForm h, SmpTSAdmDetail l, Group_User u, SmpTSCompanyV c where  l.AdmType='21' and u.UserOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=u.GroupOID and h.CompanyCode = c.CompanyCode "
                            + "union select h.CompanyCode, c.CompanyName from SmpTSAdmForm h, SmpTSAdmDetail l,  Functions f, SmpTSCompanyV c where  l.AdmType='9' and f.occupantOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=f.organizationUnitOID and h.CompanyCode = c.CompanyCode ";
                DataSet ds = engine.getDataSet(sql, "TEMP");
                int count = ds.Tables[0].Rows.Count;
                ids = new string[1 + count, 2];
                ids[0, 0] = "";
                ids[0, 1] = "";

                for (int i = 0; i < count; i++)
                {
                    string companyCode = ds.Tables[0].Rows[i][0].ToString();
                    string companyName = ds.Tables[0].Rows[i][1].ToString();
                    ids[1 + i, 0] = companyCode;
                    ids[1 + i, 1] = companyName;
                }
                CompanyCode.setListItem(ids);
                //講師來源
                ids = new string[,] {
                    {"",""},
                    {"I",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts004m_input_aspx.language.ini", "message", "I", "內部")},
                    {"O",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts004m_input_aspx.language.ini", "message", "O", "外部")}
                };
                LecturerSource.setListItem(ids);
                //內部講師
                InLecturerGUID.clientEngineType = (string)Session["engineType"];
                InLecturerGUID.connectDBString = (string)Session["connectString"];
                //部門
                InLecturerDeptGUID.clientEngineType = (string)Session["engineType"];
                InLecturerDeptGUID.connectDBString = (string)Session["connectString"];

                //唯讀欄位
                CompanyCode.ReadOnly = true;
                LecturerSource.ReadOnly = true;
                InLecturerGUID.ReadOnly = true;
                InLecturerDeptGUID.ReadOnly = true;
                ExtLecturer.ReadOnly = true;
                ExtCompany.ReadOnly = true;
                Email.ReadOnly = true;
                StartDate.ReadOnly = true;
                EndDate.ReadOnly = true;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        //公司別
        CompanyCode.ValueText = objects.getData("CompanyCode");
        //講師來源
        string lecturerSource = objects.getData("LecturerSource"); ;
        LecturerSource.ValueText = lecturerSource;
        //內部講師
        string inLecturerGUID = objects.getData("InLecturerGUID");
        InLecturerGUID.GuidValueText = inLecturerGUID;
        if (!inLecturerGUID.Equals("")) 
        {
            InLecturerGUID.doGUIDValidate();
        }
        //部門
        string inLecturerDeptGUID = objects.getData("InLecturerDeptGUID");
        InLecturerDeptGUID.GuidValueText = inLecturerDeptGUID;
        if (!inLecturerDeptGUID.Equals(""))
        {
            InLecturerDeptGUID.doGUIDValidate();
        }
        //外部講師
        ExtLecturer.ValueText = objects.getData("ExtLecturer");
        //公司/機構
        ExtCompany.ValueText = objects.getData("ExtCompany");
        //信箱
        Email.ValueText = objects.getData("Email");
        //電話
        Telephone.ValueText = objects.getData("Telephone");
        //專長
        SpecialSkill.ValueText = objects.getData("SpecialSkill");
        //經歷
        Experience.ValueText = objects.getData("Experience");
        //有效期間
        StartDate.ValueText = objects.getData("StartDate");
        EndDate.ValueText = objects.getData("EndDate");
        //新人訓練
        string value = objects.getData("Orientation");
        Orientation.ValueText = value;
        if(value.Equals("Y")) 
        {
            CbxOrientation.Checked = true;
        }
        //專業職能
        value = objects.getData("Professional");
        Professional.ValueText = value;
        if (value.Equals("Y"))
        {
            CbxProfessional.Checked = true;
        }
        //管理職能
        value = objects.getData("Management");
        Management.ValueText = value;
        if (value.Equals("Y"))
        {
            CbxManagement.Checked = true;
        }
        //品質管理
        value = objects.getData("Quality");
        Quality.ValueText = value;
        if (value.Equals("Y"))
        {
            CbxQuality.Checked = true;
        }
        //環安衛
        value = objects.getData("EHS");
        EHS.ValueText = value;
        if (value.Equals("Y"))
        {
            CbxEHS.Checked = true;
        }

        if (isNew)
        {
            CompanyCode.ReadOnly = false;
            LecturerSource.ReadOnly = false;
        }
        else
        {
            if (lecturerSource.Equals("I"))
            {
                StartDate.ReadOnly = false;
                EndDate.ReadOnly = false;
            }
            else if (lecturerSource.Equals("O"))
            {
                ExtCompany.ReadOnly = false;
                ExtLecturer.ReadOnly = false;
                Email.ReadOnly = false;
                StartDate.ReadOnly = true;
                EndDate.ReadOnly = true;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string sql = null;

        objects.setData("CompanyCode", CompanyCode.ValueText);
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        string companyCode = CompanyCode.ValueText;
        objects.setData("CompanyCode", companyCode);
        sql = "select CompanyName from SmpTSCompanyV where CompanyCode = '" + companyCode + "'";
        string companyName = (string)engine.executeScalar(sql);
        objects.setData("CompanyName", companyName);
        string lecturerSource = LecturerSource.ValueText;
        objects.setData("LecturerSource", lecturerSource);
        sql = "select LecturerSourceValue from SmpTSLecturerSourceV where LecturerSourceCode = '" + lecturerSource + "'";
        string lecturerSourceValue = (string)engine.executeScalar(sql);
        objects.setData("LecturerSourceValue", lecturerSourceValue);
        objects.setData("InLecturerGUID", InLecturerGUID.GuidValueText);
        objects.setData("id", InLecturerGUID.ValueText);
        objects.setData("userName", InLecturerGUID.ReadOnlyValueText);
        objects.setData("InLecturerDeptGUID", InLecturerDeptGUID.GuidValueText);
        objects.setData("ExtLecturer", ExtLecturer.ValueText);
        objects.setData("ExtCompany", ExtCompany.ValueText);
        objects.setData("Email", Email.ValueText);
        objects.setData("Telephone", Telephone.ValueText);
        objects.setData("SpecialSkill", SpecialSkill.ValueText);
        objects.setData("Experience", Experience.ValueText);
        objects.setData("StartDate", StartDate.ValueText);
        objects.setData("EndDate", EndDate.ValueText);

        if (CbxOrientation.Checked)
        {
            Orientation.ValueText = "Y";
        }
        else
        {
            Orientation.ValueText = "N";
        }
        if (CbxProfessional.Checked)
        {
            Professional.ValueText = "Y";
        }
        else
        {
            Professional.ValueText = "N";
        }
        if (CbxManagement.Checked)
        {
            Management.ValueText = "Y";
        }
        else
        {
            Management.ValueText = "N";
        }
        if (CbxQuality.Checked)
        {
            Quality.ValueText = "Y";
        }
        else
        {
            Quality.ValueText = "N";
        }
        if (CbxEHS.Checked)
        {
            EHS.ValueText = "Y";
        }
        else
        {
            EHS.ValueText = "N";
        }
        objects.setData("Orientation", Orientation.ValueText);
        objects.setData("Professional", Professional.ValueText);
        objects.setData("Management", Management.ValueText);
        objects.setData("Quality", Quality.ValueText);
        objects.setData("EHS", EHS.ValueText);

        //檢查欄位資料
        string errMsg = checkFieldData(objects, engine);
        engine.close();
        if (!errMsg.Equals(""))
        {
            errMsg = errMsg.Replace("\n", "; ");
            throw new Exception(errMsg);
        }

    }

    public string checkFieldData(com.dsc.kernal.databean.DataObject objects, AbstractEngine engine)
    {
        string errMsg = "";
        
        string sql = null;
        string guid = objects.getData("GUID");
        string companyCode = objects.getData("CompanyCode");
        string lecturerSource = objects.getData("LecturerSource");
        string startDate = objects.getData("StartDate");
        string endDate = objects.getData("EndDate");

        if (endDate.Equals(""))
        {
            endDate = "9999/12/31";
        }

        if (companyCode.Equals(""))
        {
            errMsg += "請選擇[" + LblCompany.Text + "]!\n";
        }

        if (lecturerSource.Equals(""))
        {
            errMsg += "請選擇[" + LblLecturerSource.Text + "]!\n";
        }

        if (!startDate.Equals("") && !endDate.Equals(""))
        {
            DateTime startTime = Convert.ToDateTime(startDate);
            DateTime endTime = Convert.ToDateTime(endDate);
            //檢查日期
            if (startTime.CompareTo(endTime) >= 0)
            {
                errMsg += "有效期間(迄)需大於有效期間(起)!\n";
            }
        }

        //內部-需選擇內部講師
        if (lecturerSource.Equals("I"))
        {
            if (startDate.Equals(""))
            {
                errMsg += "請選擇[" + LblStartEndDate.Text + "(起)]!\n";
            }

            string inLecturerGUID = InLecturerGUID.GuidValueText;
            if (inLecturerGUID.Equals(""))
            {
                errMsg += "請輸入[" + LblInLecturerGUID.Text + "]!\n";
            }

            //在同一有效期間內內部講師(員工工號)不可重覆
            sql = "select count(*) cnt from SmpTSLecturer where InLecturerGUID='" + inLecturerGUID + "' " +
                    "and GUID != '" + guid + "' " +
                    "and ((StartDate <= '" + startDate + "' and (EndDate >= '" + startDate + "' or EndDate = '')) " +
                        "or (StartDate <= '" + endDate + "' and (EndDate >= '" + endDate + "' or EndDate = '')))";
            int cnt = (int)engine.executeScalar(sql);
            if (cnt > 0)
            {
                errMsg += "在同一有效期間[" + LblInLecturerGUID.Text + "]不可重覆!\n";
            }
        }
        //外部-需輸入外部講師、公司/機構、信箱、電話
        else if (lecturerSource.Equals("O"))
        {
            if (ExtLecturer.ValueText.Equals(""))
            {
                errMsg += "請輸入[" + LblExtLecturer.Text + "]!\n";
            }
            if (ExtCompany.ValueText.Equals(""))
            {
                errMsg += "請輸入[" + LblExtCompany.Text + "]!\n";
            }
            if (Email.ValueText.Equals(""))
            {
                //errMsg += "請輸入[" + LblEmail.Text + "]!\n";
            }
            if (Telephone.ValueText.Equals(""))
            {
                errMsg += "請輸入[" + LblTelephone.Text + "]!\n";
            }

            //在同一有效期間內外部講師姓名不可重覆
            string extLecturer = objects.getData("ExtLecturer");
            string extCompany = objects.getData("ExtCompany");
            sql = "select count(*) cnt from SmpTSLecturer where ExtLecturer='" + extLecturer + "' and ExtCompany='" + extCompany + "' " +
                    "and GUID != '" + guid + "'";
                    //"and ((StartDate <= '" + startDate + "' and (EndDate >= '" + startDate + "' or EndDate = '')) " +
                    //    "or (StartDate <= '" + endDate + "' and (EndDate >= '" + endDate + "' or EndDate = '')))";
            int cnt = (int)engine.executeScalar(sql);
            if (cnt > 0)
            {
                //errMsg += "在同一有效期間[" + LblExtLecturer.Text + "]不可重覆!\n";
                errMsg += "同一公司外部講師[" + LblExtLecturer.Text + "]不可重覆!\n";
            }
        }

        //教授課程類至少選一項
        if (!CbxOrientation.Checked && !CbxProfessional.Checked && !CbxManagement.Checked && !CbxQuality.Checked && !CbxEHS.Checked)
        {
            errMsg += "請選擇[" + LblCalssType.Text + "]!\n";
        }

        return errMsg;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        NLAgent agent = new NLAgent();
        agent.loadSchema("WebServerProject.maintain.SPTS004.SmpTSLecturerAgent");
        agent.engine = engine;
        agent.query("1=2");

        bool result = agent.defaultData.add(objects);
        if (!result)
        {
            engine.close();
            throw new Exception(agent.defaultData.errorString);
        }
        else
        {
            result = agent.update();
            engine.close();
            if (!result)
            {
                throw new Exception(engine.errorString);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    protected void LecturerSource_SelectChanged(string value)
    {
        if (LecturerSource.ValueText.Equals("I"))
        {
            InLecturerGUID.ReadOnly = false;
            ExtLecturer.ReadOnly = true;
            ExtLecturer.ValueText = "";
            ExtCompany.ReadOnly = true;
            ExtCompany.ValueText = "";
            StartDate.ReadOnly = false;
            EndDate.ReadOnly = false;
        }
        else if (LecturerSource.ValueText.Equals("O"))
        {
            InLecturerGUID.ReadOnly = true;
            InLecturerGUID.GuidValueText = "";
            InLecturerGUID.ValueText = "";
            InLecturerGUID.ReadOnlyValueText = "";
            InLecturerDeptGUID.GuidValueText = "";
            InLecturerDeptGUID.ValueText = "";
            InLecturerDeptGUID.ReadOnlyValueText = "";
            ExtCompany.ReadOnly = false;
            ExtLecturer.ReadOnly = false;
            Email.ReadOnly = false;
            Telephone.ReadOnly = false;
            StartDate.ReadOnly = true;
            EndDate.ReadOnly = true;
            StartDate.ValueText = "";
            EndDate.ValueText = "";
        }
    }

    /// <summary>
    /// 帶出部門與信箱
    /// </summary>
    /// <param name="values"></param>
    protected void InLecturerGUID_SingleOpenWindowButtonClick(string[,] values)
    {
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(EngineConstants.SQL, (string)Session["connectString"]);
        string sql = null;
        string userGUID = InLecturerGUID.GuidValueText;
        if (string.IsNullOrEmpty(InLecturerDeptGUID.GuidValueText))
        {
            sql = "select organizationUnitOID from Functions where occupantOID = '" + userGUID + "'";
            string orgUnitGUID = (string)engine.executeScalar(sql);
            if (!orgUnitGUID.Equals(""))
            {
                InLecturerDeptGUID.GuidValueText = orgUnitGUID;
                InLecturerDeptGUID.doGUIDValidate();
            }
        }
        sql = "select mailAddress from Users where OID = '" + userGUID + "'";
        string emailAddress = (string)engine.executeScalar(sql);
        Email.ValueText = emailAddress;
        engine.close();
    }
}
