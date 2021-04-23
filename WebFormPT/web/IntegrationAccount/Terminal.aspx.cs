using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using com.dsc.kernal.factory;
using System.Data;

public partial class IssueLab_IntegrationAccount_Terminal : BaseWebUI.WebFormBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                if (Request.QueryString["url"] != null)
                {
                    string Token = "";
                    if (Application["SessionLivingGroup"] != null)
                    {
                        Hashtable htSeCollection = (Hashtable)Application["SessionLivingGroup"];
                        Token = htSeCollection[Session.SessionID].ToString();
                    }

                    string connectString = (string)Session["connectString"];
                    string engineType = (string)Session["engineType"];
                    IOFactory factory = new IOFactory();
                    AbstractEngine engine = null;

                    try
                    {
                        //write User Account into DB Table                                 
                        string FactoryID = "";
                        if (Request.QueryString["FactoryID"] != null)
                        {
                            FactoryID = com.dsc.kernal.utility.Utility.filter(Request.QueryString["FactoryID"].ToString());
                        }
                        else
                        {
                            throw new Exception("Factory ID 參數缺少");
                        }

                        engine = factory.getEngine(engineType, connectString);
                        string sql = "";
                        sql = "Select LoginID,FactoryIP.IP from AccountMap Left Join FactoryIP on AccountMap.FactoryGUID=FactoryIP.GUID where UsersGUID =  '" + (string)Session["UserGUID"] + "' ";
                        sql += " and FactoryGUID = '" + FactoryID + "' ";
                        DataSet dsResult = engine.getDataSet(sql, "tmp");
                        DataTable dtTable = dsResult.Tables[0];
                        if (dtTable.Rows.Count == 0)
                        {
                            throw new Exception("請先進行歸戶認證");
                        }
                        else
                        {		
                            string LoginID = dtTable.Rows[0][0].ToString();
                            string IP = dtTable.Rows[0][1].ToString();
                            string param = "&UserID=" + LoginID;
		            string myHost = System.Net.Dns.GetHostName();
			    int index= System.Net.Dns.GetHostEntry(myHost).AddressList.Length;
	        	    string serverIP = System.Net.Dns.GetHostEntry(myHost).AddressList[index-1].ToString();
                            param += "&SID=" + Session.SessionID;
                            param += "&Token=" + Token;
                            param += "&From=" + serverIP ;
                            string url = Request.QueryString["url"].Replace("@DNS", IP);
//Response.Write(serverIP);return;


                          Response.Redirect(url+ param);                            
                        }
                        engine.close();
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
        }
    }
        
 }
