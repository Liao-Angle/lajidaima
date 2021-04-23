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

public partial class Program_DSCOrgService_Maintain_Users_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {

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
            dr["ldapid"] = u.id;
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
}
