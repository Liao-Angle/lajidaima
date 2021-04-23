using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Collections;
using com.dsc.kernal.factory;
using System.Data;
using com.dsc.kernal.utility;


/// <summary>
/// CheckSessionID 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CheckSessionID : System.Web.Services.WebService {

    public CheckSessionID () {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
      [WebMethod]
    public string getUserID(string LDAP_ID) 
    {
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        string connectString = acs.connectString;
        string engineType = acs.engineType;
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;

        engine = factory.getEngine(engineType, connectString);
        try
        {
            string sql = "Select id from Users where lower(ldapid)=lower('"+ com.dsc.kernal.utility.Utility.filter(LDAP_ID)+"') ";
			//string sql = "Select id from Users where id='"+ com.dsc.kernal.utility.Utility.filter(User_ID)+"' ";
			//string sql = "Select id from Users where id COLLATE Chinese_Taiwan_Stroke_CI_AS like '" + com.dsc.kernal.utility.Utility.filter(User_ID) + "' COLLATE Chinese_Taiwan_Stroke_CI_AS ";
            DataSet dsResult =  engine.getDataSet(sql, "tmp");
            engine.close();
            DataTable dtResult = dsResult.Tables[0];
            if (dtResult.Rows.Count > 0)
            {
                return dtResult.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }            
        }
        catch (Exception ue) 
        {
            if (engine != null) {
                engine.close();
            }
            return "";
        }
    }
    [WebMethod]
    public string Check(string SessionID ,string Token)
    {
        if (Application["SessionLivingGroup"] != null)
        {
            Hashtable htSeCollection = (Hashtable)Application["SessionLivingGroup"];
            if (htSeCollection.ContainsKey(SessionID)
                &&
                htSeCollection[SessionID].ToString() == Token
                )
            {
                return "OK";
            }
            else
            {
                return "GONE";
            }
        }
        return "";
    }
}
