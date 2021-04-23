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
using com.dsc.kernal.utility;
using com.dsc.kernal.document;
using System.IO;
using System.Xml;
using NPOI.HSSF.UserModel;

/// <summary>
/// SMP Training System 共用程式
/// </summary>
public class SmpTSMaintainPage :Page
{
    public SmpTSMaintainPage()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //

    }
	
	/// <summary>
    /// 取得Excel檔案內容
    /// </summary>
    /// <param name="fName"></param>
    /// <returns></returns>
    public ArrayList getExcelData(string fName) {
        ArrayList aryData = new ArrayList();
        string filePath = string.Format(fName);
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        HSSFWorkbook wk = new HSSFWorkbook(fs); 
        HSSFSheet hst = (HSSFSheet)wk.GetSheetAt(0);
        for (int i = 0; i<=hst.LastRowNum; i++)
        {
            if (i == 0)
            {
                continue;
            }
            HSSFRow row = (HSSFRow)hst.GetRow(i);
            int cellNum = row.LastCellNum;
            string[] values = new string[cellNum];
            for (int j = 0; j<cellNum; j++)
            {
                string cellValue = row.GetCell(j) == null ? "" : row.GetCell(j).ToString();
                values[j] = cellValue;
                //MessageBox(cellValue);
            }
            aryData.Add(values);
        }
        fs.Close();
        return aryData;
    }

    /// <summary>
    /// get CompanyCode
    /// </summary>
    /// <param name="msg"></param>
    public string getCompanyCode(AbstractEngine engine, string userGUID)
    {
        string companyCode = "";
        string sql = "select h.CompanyCode from SmpTSAdmForm h, SmpTSAdmDetail l where l.AdmType='1'  and AdmTypeGUID='" + userGUID + "' and h.GUID = l.AdmFormGUID " +
           "union select h.CompanyCode from SmpTSAdmForm h, SmpTSAdmDetail l, Group_User u where  l.AdmType='21' and u.UserOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=u.GroupOID " +
           "union select h.CompanyCode from SmpTSAdmForm h, SmpTSAdmDetail l, Functions f where  l.AdmType='9' and f.occupantOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=f.organizationUnitOID";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int rows = ds.Tables[0].Rows.Count;
        string[][] result = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            result[i] = new string[1];
            result[i][0] = ds.Tables[0].Rows[i][0].ToString();            
        }

        if (result != null && result.Length > 0)
        {
            for (int i = 0; i < result.Length; i++)
                if (companyCode.Equals(""))
                    companyCode += "'" + result[i][0] + "'"; 
                else
                    companyCode += ",'" + result[i][0] + "'"; 

        }
                
        return companyCode;
    }


    /// <summary>
    /// get CompanyCode & Name
    /// </summary>
    /// <param name="msg"></param>
    public string[,] getCompanyCodeName(AbstractEngine engine, string userGUID)
    {
        string sql = "select h.CompanyCode,c.CompanyName from SmpTSAdmForm h, SmpTSAdmDetail l, SmpTSCompanyV c where l.AdmType='1'  and AdmTypeGUID='" + userGUID + "' and h.GUID = l.AdmFormGUID and h.CompanyCode = c.CompanyCode " +
            "union select h.CompanyCode,c.CompanyName from SmpTSAdmForm h, SmpTSAdmDetail l, Group_User u,  SmpTSCompanyV c where  l.AdmType='21' and u.UserOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=u.GroupOID and h.CompanyCode = c.CompanyCode " +
            "union select h.CompanyCode,c.CompanyName from SmpTSAdmForm h, SmpTSAdmDetail l, Functions f,  SmpTSCompanyV c where  l.AdmType='9' and f.occupantOID='" + userGUID + "' and h.GUID = l.AdmFormGUID and l.AdmTypeGUID=f.organizationUnitOID and h.CompanyCode = c.CompanyCode ";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int count = ds.Tables[0].Rows.Count;
        string[,] ids = null;
        ids = new string[1 + count, 2];
        ids[0, 0] = "";
        ids[0, 1] = "";
        for (int i = 0; i < count; i++)
        {
            string companyCode = ds.Tables[0].Rows[i][0].ToString();
            string companyName = ds.Tables[0].Rows[i][1].ToString();
            ids[1 + i, 0] = companyCode;            
            ids[1 + i, 1] = com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spts005_input_aspx.language.ini", "message", companyCode, companyName);
        }

        return ids;
    }


    /// <summary>
    /// 取得鼎新系統資料庫連線
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public AbstractEngine getErpEngine(AbstractEngine engine,string companyCode)
    {        
        AbstractEngine erpEngine = null;
        try
        {            
            IOFactory factory = new IOFactory();            
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
            string connStr = sp.getParam(companyCode + "WorkFlowERPDB");           
            erpEngine = factory.getEngine(EngineConstants.SQL, connStr);
        }
        catch (Exception e)
        {
            throw new Exception(e.StackTrace);
        }       
        return erpEngine;
    }
   
}
