<%@ WebService Language="C#" Class="DSCBatchService" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Data;

/// <summary>
/// WebService 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DSCBatchService : System.Web.Services.WebService
{


    public DSCBatchService()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SelectBatchMainInfo(string BatchID)
    {
        string strxml = "";

        //try
        //{
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine;
        engine = factory.getEngine(acs.engineType, acs.connectString);
        
        string whereclause = "";
            
        if (!BatchID.Equals(""))
        {
            whereclause = "where SMTAAAA002='" + Utility.filter(BatchID) + "'";
        }
        string sql = "select SMTAAAA001,SMTAAAA002,SMTAAAA003,SMTAAAA004,SMTAAAA005,SMTAAAA006,SMTAAAA007,SMTAAAA008,SMTAAAA009,SMTAAAA010,SMTAAAA011,SMTAAAA012,SMTAAAA013,SMTAAAA014,SMTAAAB001,SMTAAAB002,SMTAAAB003,SMTAAAB004,SMTAAAB005,SMTAAAC001,SMTAAAC002,SMTAAAC003,SMTAAAC004,SMTAAAC005,SMTAAAC006,SMTAAAC007 from SMTAAAA inner join SMTAAAB on SMTAAAA001=SMTAAAB002 inner join SMTAAAC on SMTAAAA001=SMTAAAC002 " + whereclause;
        DataSet ds = engine.getDataSet(sql, "TEMP");

        engine.close();

        if (ds.Tables[0].Rows.Count > 0)
        {
            /*
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //strxml = strxml + "<SMTAAAA>";
                if (i == 0)
                {
                    strxml = "<SMTA>" + ds.Tables[0].Rows[i]["SMTAAAA001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA006"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA007"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA008"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA009"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA010"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA011"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA012"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA013"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA014"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC006"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC007"].ToString() + "$$";
                    strxml = strxml + "</SMTA>";
                }
                else
                {
                    strxml = strxml + "<SMTA>;" + ds.Tables[0].Rows[i]["SMTAAAA001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA006"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA007"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA008"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA009"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA010"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA011"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA012"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA013"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAA014"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAB005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC006"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTAAAC007"].ToString() + "$$";
                    strxml = strxml + "</SMTA>";
                }
                //strxml = strxml + "<SMTAAAA002>" + ds.Tables[0].Rows[i][1].ToString() +"</SMTAAAA002>";
                //strxml = strxml + "</SMTAAAA>";
            }

            return strxml;
            */
            strxml = ds.GetXml();
            strxml = System.Web.HttpUtility.UrlEncode(strxml);
            return strxml;
        }
        else
        {
            strxml = "<error>找不到符合的資料</error>";
            strxml = System.Web.HttpUtility.UrlEncode(strxml);
            return strxml;
        }    

        
    }

    public string SelectBatchDetailInfo(string BatchID)
    {
        string strxml = "";

        //try
        //{
        string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
        com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
        com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
        acs.getConnectionString();

        IOFactory factory = new IOFactory();
        AbstractEngine engine;
        engine = factory.getEngine(acs.engineType, acs.connectString);

        string whereclause = "";

        if (!BatchID.Equals(""))
        {
            BatchID = "where SMTCAAA002 in (select SMTAAAA001 from SMTAAAA where SMTAAAA002='" + Utility.filter(BatchID) + "')";
        }
        string sql = "select SMTCAAA001,SMTCAAA002,SMTCAAA003,SMTCAAA004,SMTCAAA005,SMTCAAA006,SMTCAAA007,SMTCAAB001,SMTCAAB002,SMTCAAB003,SMTCAAB004 from SMTCAAA left join SMTCAAB ON SMTCAAA001=SMTCAAB002 " + whereclause;
        DataSet ds = engine.getDataSet(sql, "TEMP");

        engine.close();

        if (ds.Tables[0].Rows.Count > 0)
        {
            /*
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    strxml = "<SMTC>" + ds.Tables[0].Rows[i]["SMTCAAA001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA006"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA007"].ToString() + "$$";                    
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB004"].ToString() + "$$";                    
                    strxml = strxml + "</SMTC>";
                }
                else
                {
                    strxml = "<SMTC>" + ds.Tables[0].Rows[i]["SMTCAAA001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA004"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA005"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA006"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAA007"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB001"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB002"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB003"].ToString() + "$$";
                    strxml = strxml + ds.Tables[0].Rows[i]["SMTCAAB004"].ToString() + "$$";
                    strxml = strxml + "</SMTC>";
                }
            }

            strxml = System.Web.HttpUtility.UrlEncode(strxml);
            return strxml;
            */
            strxml = ds.GetXml();
            strxml = System.Web.HttpUtility.UrlEncode(strxml);
            return strxml;
        }
        else
        {
            strxml = "<error>找不到符合的資料</error>";
            strxml = System.Web.HttpUtility.UrlEncode(strxml);
            return strxml;
        }


    }

}

