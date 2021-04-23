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
using WebServerProject.org.Users;
using System.DirectoryServices;

public partial class Program_DSCOrgService_Maintain_Users_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckButton.PageUniqueID = SaveButton.PageUniqueID;
        CheckButton.ID = "CHECKBUTTON";
    }

    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {

        string engineType = (string)Session["engineType"];
        string connectString = (string)Session["connectString"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        string sql = "select LANGUAGEID, LANGUAGENAME from SYSLANGUAGE order by ISDEFAULT desc";
        DataSet ds = engine.getDataSet(sql, "TEMP");
        string[,] ids = new string[ds.Tables[0].Rows.Count, 2];
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
            ids[i, 1] = ds.Tables[0].Rows[i][1].ToString();
        }
        localeStringF.setListItem(ids);

        engine.close();

        Users obj = (Users)objects;

        //OIDF.ValueText = obj.OID;
        //objectVersionF.ValueText = obj.objectVersion;
        idF.ValueText = obj.id;
        userNameF.ValueText = obj.userName;
        //passwordF.ValueText = obj.password;
        leaveDateF.ValueText = obj.leaveDate;
        //referCalendarF.ValueText = obj.referCalendarOID;
        //identificationTypeF.ValueText = obj.identificationType;
        localeStringF.ValueText = obj.localeString;
        mailAddressF.ValueText = obj.mailAddress;
        phoneNumberF.ValueText = obj.phoneNumber;
        //workflowServerOIDF.ValueText = obj.workflowServerOID;
        //enableSubstituteF.ValueText = obj.enableSubstitute;
        //startSubstituteTimeF.ValueText = obj.startSubstituteTime;
        //endSubstituteTimeF.ValueText = obj.endSubstituteTime;
        costF.ValueText = obj.cost;
    }

    protected override void saveData(DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");

        Users u = (Users)objects;
        if (isAddNew)
        {
            u.OID = IDProcessor.getID("");
            
        }
        u.id = idF.ValueText;
        u.userName = userNameF.ValueText;
        u.leaveDate = leaveDateF.ValueText;
        u.localeString = localeStringF.ValueText;
        u.mailAddress = mailAddressF.ValueText;
        u.phoneNumber = phoneNumberF.ValueText;
        u.cost = costF.ValueText;

    }
    protected override void saveDB(DataObject objects)
    {
        Users u = (Users)objects;

        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
        string ndb = sp.getParam("NaNaDB");
        string ndt=sp.getParam("NaNaDBType");
        engine.close();

        engine = factory.getEngine(ndt, ndb);

        bool isAddNew = (bool)getSession("isNew");
        string sql = "";
        DataRow dr = null;
        DataSet ds = null;

        if (isAddNew)
        {
            sql = "select * from Users where (1=2)";
            ds = engine.getDataSet(sql, "TEMP");
            dr = ds.Tables[0].NewRow();
        }
        else
        {
            sql = "select * from Users where OID='" + objects.getData("OID") + "'";
            ds = engine.getDataSet(sql, "TEMP");
            dr = ds.Tables[0].Rows[0];
        }

        if (isAddNew)
        {
            dr["OID"] = IDProcessor.getID("").Substring(0, 32);
            dr["objectVersion"] = 1;
            dr["password"] = "mlCCiMVj8+lN5SjYg0g3bp2WzdA=";
            dr["identificationType"] = "DEFAULT";
            dr["enableSubstitute"] = "0";
            dr["mailingFrequencyType"] = "0";
            //dr["ldapid"] = u.id;
        }
        else
        {
            int oj = int.Parse(dr["objectVersion"].ToString());
            oj++;
            dr["objectVersion"] = oj;
        }

        dr["id"] = u.id;
        dr["userName"] = u.userName;
        if (!u.leaveDate.Equals(""))
        {
            dr["leaveDate"] = u.leaveDate;
        }
        else
        {
            dr["leaveDate"] = System.DBNull.Value;
        }
        dr["localeString"] = u.localeString;
        dr["mailAddress"] = u.mailAddress;
        dr["ldapid"] = u.mailAddress.Split(new char[] { '@' })[0].Trim() + "@SCQLDAPDN";
        dr["phoneNumber"] = u.phoneNumber;
        dr["cost"] = u.cost;

        if (isAddNew)
        {
            ds.Tables[0].Rows.Add(dr);
        }

        if (!engine.updateDataSet(ds))
        {
            engine.close();
            throw new Exception(engine.errorString);

        }
        engine.close();
    }
    protected void CheckButton_Click(object sender, EventArgs e)
    {
        AbstractEngine engine = null;
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        string sql = "";
        DataSet ds = null;
        msgF.Text = "";
        try
        {
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);

            sql = "select * from Users where id='" + idF.ValueText + "'";
            ds = engine.getDataSet(sql, "TEMP");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if ((bool)getSession("isNew"))
                {
                    userNameF.ValueText = "";
                    costF.ValueText = "";
                    mailAddressF.ValueText = "";
                    phoneNumberF.ValueText = "";
                    msgF.Text = "帳號" + idF.ValueText + "已經存在";
                    idF.ValueText = "";
                }
                else
                {
                    userNameF.ValueText = ds.Tables[0].Rows[0]["userName"].ToString();
                    costF.ValueText = ds.Tables[0].Rows[0]["cost"].ToString();
                    mailAddressF.ValueText = ds.Tables[0].Rows[0]["mailAddress"].ToString();
                    phoneNumberF.ValueText = ds.Tables[0].Rows[0]["phoneNumber"].ToString();
                }
            }
            else
            {
                //從HR找資料, AD找郵箱
                sql = @"SELECT *
                          FROM [WebFormPT].[dbo].[HRUSERS]
                          where [id]='" + idF.ValueText + "'";
                ds = engine.getDataSet(sql, "TEMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    userNameF.ValueText = ds.Tables[0].Rows[0]["userName"].ToString();
                    costF.ValueText = "0";
                }

                string ldap = sp.getParam("SCQLDAPDN");
                string account = sp.getParam("SCQLDAPAccount");
                string[] sep = new string[] { "/" };
                if (account != "" && account.IndexOf("/") > 0)
                {
                    try
                    {
                        DirectoryEntry de = new DirectoryEntry("LDAP://" + ldap, account.Split(sep, StringSplitOptions.None)[0], account.Split(sep, StringSplitOptions.None)[1]);
                        DirectorySearcher dsc = new DirectorySearcher(de);
                        try
                        {
                            dsc.Filter = "(postalcode=" + idF.ValueText + ")";
                            dsc.PropertiesToLoad.Add("mail");
                            SearchResult sr = dsc.FindOne();
                            foreach (string key in sr.Properties.PropertyNames)
                            {
                                if (key == "mail")
                                {
                                    foreach (object obj in sr.Properties[key])
                                    {
                                        if (obj.ToString() != "")
                                        {
                                            mailAddressF.ValueText = obj.ToString();
                                            break;
                                        }

                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("取得LDAP郵箱錯誤, " + ex.Message.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("連線LDAP錯誤, " + ex.Message.ToString());
                    }
                }
                else
                {
                    throw new Exception("請維護LDAP帳號密碼, SCQLDAPAccount=帳號/密碼.");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("連線系統資料庫錯誤, " + ex.Message.ToString());
        }
    }
}
