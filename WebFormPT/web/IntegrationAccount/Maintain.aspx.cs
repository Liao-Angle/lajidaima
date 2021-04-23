using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.dsc.kernal.factory;
using System.Data;
using System.Collections;

public partial class IssueLab_IntegrationAccount_Maintain : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            if (!IsProcessEvent)
            {
                    if (Request.QueryString["url"] != null) 
                    {
                        Response.Redirect(Request.QueryString["url"].ToString());
                    }

                    string[,] items = new String[,] { { "192.168.2.33", "大陸" }, { "192.168.2.11", "台灣" }};
                    this.sddLDAP.setListItem(items);

                    //string[] Factory1 = new String[] { "192.168.71.147", "重慶" };
                    //string[] EmptyFactory = new String[] { "", "" };
                    //string[] Factory1 = new String[] { "10.3.11.81", "重慶" };
                    //string[] Factory2 = new String[] { "192.168.71.223", "新世" };
                    //string[] Factory3 = new String[] { "192.168.2.226", "台灣" };
                    //string[,] itemsFactory = new String[,] { 
                    //                                                                { EmptyFactory[0], EmptyFactory[1] }
                    //                                                                 , { Factory1[0], Factory1[1] }
                    //                                                                 , { Factory2[0], Factory2[1] }                     
                    //                                                                    , { Factory3[0], Factory3[1] }                     
                    //                                                              };


         

                    string connectString = (string)Session["connectString"];
                    string engineType = (string)Session["engineType"];
                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = null;                    

                    try
                    {
                        engine = factory.getEngine(engineType, connectString);

                        string getFactory = "Select GUID , Factory,IP from FactoryIP";
                        DataSet dsFac = engine.getDataSet(getFactory, "fac");
                        if (dsFac.Tables[0].Rows.Count > 0) 
                        {
                            string[,] itemsFactory = new String[dsFac.Tables[0].Rows.Count, 2];
                            for (int i = 0; i < dsFac.Tables[0].Rows.Count; i++) 
                            { 
                                itemsFactory[i,0] =dsFac.Tables[0].Rows [i]["GUID"].ToString()+ "@" + dsFac.Tables[0].Rows[i]["IP"].ToString();
                                itemsFactory[i, 1] = dsFac.Tables[0].Rows[i]["Factory"].ToString();
                            }
                            this.sddFactory.setListItem(itemsFactory);
                        }
                        
                        
                        //WebServerProject.SysParam sp = new WebServerProject.SysParam(engine);
                        //string domain = sp.getParam("IntegrationAccountDomain");
                        
                        //if (domain.Length > 0)
                        //{
                        //    string[] domains = domain.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        //    string[,] itemsDomain = new String[domains.Length, 2];
                        //    for (int i = 0; i < domains.Length; i++)
                        //    {
                        //        itemsDomain[i, 0] = domains[i];
                        //        itemsDomain[i, 1] = domains[i];
                        //    }
                        //    sddDomain.setListItem(itemsDomain);
                        //}
                        //else
                        //{
                        //    MessageBox("需要指定系統參數IntegrationAccountDomain");
                        //}
						engine.close();

                        
                    }
                    catch (Exception ue) 
                    {
                        MessageBox(ue.Message);
                        if (ue != null) 
                        {
                            try 
                            {
                                engine.close();
                            }catch{};
                        }
                    }                
	    }
        }        
    }
    protected void gBtnCheck_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = null;

        string adServerIP = this.sddLDAP.ValueText;
        string ldapPath = "LDAP://" + adServerIP;
        string useraccount = this.sfAccount.ValueText;
        string password = this.sfPassword.ValueText;
        string domain = this.sddDomain.ValueText;
        try
        {
            if (sddFactory.ValueText.Length == 0)
            {
                throw new Exception("請選擇廠區");
            }
            if (this.sfLocalAccountID.ValueText.Length == 0)
            {
                throw new Exception("請輸入當下待歸戶的廠區UserID");
            }
            if (sfAccount.ValueText.Length == 0)
            {
                throw new Exception("請輸入帳戶");
            }            

            //FormsAuth.LdapAuthentication la = new FormsAuth.LdapAuthentication(ldapPath);
            bool isPass = true;

            //isPass = la.IsAuthenticated(domain, useraccount, password);

            if (isPass)
            {
                //write User Account into DB Table                                      
                engine = factory.getEngine(engineType, connectString);
                string sql = "";

                sql = "Select OID from Users where id = '"+ com.dsc.kernal.utility.Utility.filter(sfLocalAccountID.ValueText)+"' ";
                Object objUserOID = engine.executeScalar(sql);
                
                string UserOID = "";
                if (objUserOID == null || ((string)objUserOID).Length == 0)
                {
                    throw new Exception("當地廠區帳號ID有誤");
                }
                else
                {
                    UserOID = (string)objUserOID;
                }

                sql = "Select Count(0) from AccountMap where UsersGUID = '" +UserOID + "' ";
                
                sql += "and FactoryGUID = '" +this.sddFactory.ValueText.Split('@')[0]+"' ";
                Object ObjectResult = engine.executeScalar(sql);
                if (ObjectResult != null) 
                {
                    if ((ObjectResult.ToString()).Equals("1")) 
                    {
                        throw new Exception("該廠區帳戶已做過歸戶");
                    }
                }

                //呼叫廠區站台，以LDAPID 取得登入ID，以做自動登入
                string FactoryIP = "";
                
                string tMethodname = "getUserID";//欲呼叫的WebService的方法名 
                object[] tArgs = new object[1];//參數列表 
                string wsURL = "http://" + this.sddFactory.ValueText.Split('@')[1] + "/ECP/WebService/CheckSessionID.asmx";
                string atWhere = "";
                //if (sddDomain.ValueText.ToUpper().Contains("CN")) 
                //{
                //    atWhere = "@CN";
                //}
                //else if (sddDomain.ValueText.ToUpper().Contains("TW"))
                //{
                //    atWhere = "@TW";
                //}                
                tArgs[0] = sfAccount.ValueText + atWhere;
                
                string loginID = "";
                try
                {
                    com.dsc.kernal.webservice.WSDLClient client = new com.dsc.kernal.webservice.WSDLClient(wsURL);
                    client.dllPath = Server.MapPath("~/tempFolder");
                    client.build(true);
                    object tReturnValue = client.Sync(tMethodname, tArgs[0].ToString());
                    
                    if (tReturnValue.ToString().Length>0)
                    {
                        loginID = tReturnValue.ToString();
                    }
		            else
			        {
		                   throw new Exception("目標廠區站台，查無對應該LDAP帳戶之帳號");
			        }
                }
                catch (Exception ue)
                {
                    throw;
                }
                
                sql = "Select GUID, UsersGUID , FactoryGUID,LoginID, D_INSERTUSER, D_INSERTTIME, D_MODIFYUSER, D_MODIFYTIME from AccountMap where 1=2 ";
                DataSet Result = engine.getDataSet(sql, "AccountMap");
                DataTable dt = Result.Tables[0];
                DataRow dr = dt.NewRow();
                dr["GUID"] = com.dsc.kernal.utility.IDProcessor.getID("");
                dr["UsersGUID"] = UserOID;
                dr["FactoryGUID"] = this.sddFactory.ValueText.Split('@')[0];
                dr["LoginID"] = loginID;
                dr["D_INSERTUSER"] = (string)Session["UserGUID"];
                dr["D_INSERTTIME"] = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
                dr["D_MODIFYUSER"] = "";
                dr["D_MODIFYTIME"] = "";
                dt.Rows.Add(dr);
                engine.updateDataSet(Result);
                engine.close();
                MessageBox("歸戶完成");
            }
            else
            {
                MessageBox("LDAP驗證失敗");
            }
        }
        catch (Exception ue) 
        {
            if (engine != null) 
            {
                engine.close();
            }            
            MessageBox(ue.Message);
        }        
    }
}