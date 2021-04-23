<%@ WebService Language="C#" Class="FlowGPCallBack" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using WebServerProject;
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.utility;
using System.Collections;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FlowGPCallBack  : System.Web.Services.WebService {

    /// <summary>
    /// GP流程結束後回呼介面
    /// </summary>
    /// <param name="flowOID">流程OID</param>
    /// <param name="result">Y:流程完成;N:流程終止;W:流程撤銷</param>
    /// <returns></returns>
    [WebMethod]
    public string afterApprove(string flowOID, string result)
    {
        AbstractEngine engine=null;

        try
        {
            string sClass = GlobalProperty.getProperty("global", "ConnectStringClass");
            com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
            com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
            acs.getConnectionString();

            
            IOFactory factory = new IOFactory();
            engine = factory.getEngine(acs.engineType, acs.connectString);
            engine.startTransaction(System.Data.IsolationLevel.ReadCommitted);
            
            DataOperate dot = new DataOperate(engine);


            dot.process(flowOID, result, "", Server);

            engine.commit();
            engine.close();

            return "Y";
        }
        catch (Exception ze)
        {
            try
            {
                engine.rollback();
                engine.close();
            }
            catch { };
            return "N";
        }
    }    
}

