using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using WebServerProject;
using com.dsc.flow.server;
using com.dsc.flow.data;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;

/// <summary>
/// StcBasicFormPage 的摘要描述
/// </summary>
public class StcBasicFormPage : SmpBasicFormPage
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
    /// <summary>
    /// 取得使用者資訊, [0]: id, [1]: userName ,[2]:deptName
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="userGUID"></param>
    /// <returns>string[]</returns>
    /// Marcia 2015/03/27
    protected string[] getUserInfo(AbstractEngine engine, string userGUID)
    {
        string sql = "select empNumber, empName, deptName,orgId from EmployeeInfo where empGUID='" + Utility.filter(userGUID) + "'";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[4];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();
            result[1] = ds.Tables[0].Rows[0][1].ToString();
            result[2] = ds.Tables[0].Rows[0][2].ToString();
            result[3] = ds.Tables[0].Rows[0][3].ToString();
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
        }

        return result;
    }

    /*用來添加部門信息的 YFP 2015/3/27 */
    /// <summary>
    /// 查詢使用者之相關資料,包含職位,直屬主管,所屬部門!
    /// [0]:UserID, [1]:UserName, [2]: UserADAccount, [3]:UserTitle, [4]:EMAIL, [5]:ORGID
    /// </summary>
    /// <param name="engine"></param> default engine
    /// <param name="userGUID"></param> User GUID
    /// <returns>string[]</returns> 僅回傳一筆, 值為 Array! Array[0]為人員工號, Array[1]為中文名, Array[2]為英文名,  Array[3]為職稱!
    protected string[] getUserInfoById2(AbstractEngine engine, string userId)
    {
        //string sql = "select id,userName,substring(mailAddress, 1 ,( charindex('@', mailAddress)-1)), a.UserTitle,mailAddress from Users u, Functions f , UserFunctions a where  f.occupantOID = u.OID and f.isMain='1' '" + Utility.filter(userId) + "' and id= and UserOID = u.OID ";
        string sql = @"select u.id,userName,substring(mailAddress, 1 ,( charindex('@', mailAddress)-1)), a.UserTitle,mailAddress, o.id,UnitName
        from Users u, Functions f , UserFunctions a, Organization o, OrganizationUnit ou where  f.occupantOID = u.OID and f.isMain='1' and u.id='" + Utility.filter(userId) + "'  and UserOID = u.OID and f.organizationUnitOID = ou.OID and ou.organizationOID=o.OID ";

        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[] result = new string[9];
        if (ds.Tables[0].Rows.Count > 0)
        {
            result[0] = ds.Tables[0].Rows[0][0].ToString();//工號
            result[1] = ds.Tables[0].Rows[0][1].ToString();//中文名
            result[2] = ds.Tables[0].Rows[0][2].ToString();//英文名
            result[3] = ds.Tables[0].Rows[0][3].ToString();//職稱
            result[4] = ds.Tables[0].Rows[0][4].ToString();//郵件
            result[5] = ds.Tables[0].Rows[0][5].ToString();//廠別
            result[6] = ds.Tables[0].Rows[0][6].ToString();//部門
        }
        else
        {
            result[0] = "";
            result[1] = "";
            result[2] = "";
            result[3] = "";
            result[4] = "";
            result[5] = "";
            result[6] = "";
        }
        return result;
    }

    /* YFP  2015/3/27 **/
    /// <summary>
    /// 返回公司別
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    public string[,] getgsb(AbstractEngine engine)
    {
        string sql = "select id,organizationName from Organization ";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        int count = ds.Tables[0].Rows.Count;
        string[,] strcb = new string[count, 2];
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                strcb[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                strcb[i, 1] = ds.Tables[0].Rows[i][1].ToString();
            }
        }
        else
        {
            strcb[0, 0] = "";
            strcb[0, 1] = "";
        }
        return strcb;
    }

    /***教育訓練****/

    /// <summary>
    /// 連接80.18的數據連接語句
    /// </summary>
    string connectString = "Data Source=192.168.80.18;Initial Catalog=door;User ID=kitty;Password=misap3173";
    //聲明一個 IOFactory 對象
    IOFactory factory = new IOFactory();


    /// <summary>
    /// 根據人員的職稱 返回體系
    /// </summary>
    /// <param name="dtname">職稱</param>
    /// <returns></returns>
    public string[,] getfhtx(string dtname)
    {
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, connectString);
        string sql = "select systemno,systemname from stcs.dbo.PXLessonSystem where myfield1='" + dtname + "'";
        DataSet ds = engine2.getDataSet(sql, "TEMP");
        int count = ds.Tables[0].Rows.Count;

        string[,] strtx = new string[count, 2];

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            strtx[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            strtx[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }




        return strtx;
    }

    /// <summary>
    /// 返回晋升課程
    /// </summary>
    /// <param name="empno">工號</param>
    /// <param name="txno">體系號</param>
    /// <returns></returns>
    public DataSet getjyxl(string empno, string txno)
    {
        //数据库连接语句        
        AbstractEngine engine2 = factory.getEngine(EngineConstants.SQL, connectString);
        string sql = "exec stcs.kitty.jyxl_ypxkc_proc '" + empno + "','" + txno + "'";
        DataSet ds = engine2.getDataSet(sql, "TEMP");

        return ds;
    }


}
