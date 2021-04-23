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

public partial class SmpProgram_Maintain_SPAD013_Input : BaseWebUI.DataListSaveForm
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
                //UserGUID.clientEngineType = (string)Session["engineType"];
                //UserGUID.connectDBString = (string)Session["connectString"];
				/*
				string[,] idsc = null;
				//產品類別碼	
                idsc = new string[,] {
                    {"A1總務類",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsc1", "A1總務類")}
                };
                Active.setListItem(idsc);
				
				string[,] idst = null;
				//特性碼
                idst = new string[,] {
                    {"1.文具類",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idst1", "1.文具類")}
                };
                Active.setListItem(idst);
				
				
				string[,] idsa = null;
				//屬性碼
                idsa = new string[,] {
                    {"A.報表/影印紙",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa1", "A.報表/影印紙")},
                    {"B.書寫用筆",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa2", "B.書寫用筆")},
                    {"C.印色/補充水",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa3", "C.印色/補充水")},
                    {"D.桌上文具",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa4", "D.桌上文具")},
                    {"E.膠水膠帶黏著",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa5", "E.膠水膠帶黏著")},
                    {"F.紙製用品",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa6", "F.紙製用品")},
                    {"G.檔案收納",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa7", "G.檔案收納")},
                    {"H.利貼標籤",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa8", "H.利貼標籤")},
                    {"I.電池與其他",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa9", "I.電池與其他")},
                    {"J.印表機耗材",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "idsa10", "J.印表機耗材")}
                };
                Active.setListItem(idsa);
*/
                string[,] ids = null;
				//生失效
                ids = new string[,] {
                    {"Y",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "ids1", "生效")},
                    {"N",com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_spad013m_input_aspx.language.ini", "message", "ids2", "失效")}
                };
                Active.setListItem(ids);

                //FlowGUID.clientEngineType = (string)Session["engineType"];
                //FlowGUID.connectDBString = (string)Session["connectString"];
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

        if (isNew)
        {

        }
        else
        {
            
        }
        
        //head        
		CategoryId.ValueText = objects.getData("CategoryId");
		CategoryName.ValueText = objects.getData("CategoryName");
		TypeId.ValueText = objects.getData("TypeId");
		TypeName.ValueText = objects.getData("TypeName");
		AttributeId.ValueText = objects.getData("AttributeId");
		AttributeName.ValueText = objects.getData("AttributeName");
		ProdDesc.ValueText = objects.getData("ProdDesc");
		Unit.ValueText = objects.getData("Unit");
		Price.ValueText = objects.getData("Price");
        Active.ValueText = objects.getData("Active");
		Attribute2.ValueText = objects.getData("Attribute2");
		Attribute3.ValueText = objects.getData("Attribute3");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objects"></param>
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        bool isNew = (bool)getSession("isNew");
        if (isNew)
        {
            objects.setData("GUID", IDProcessor.getID(""));
            objects.setData("IS_DISPLAY", "Y");
            objects.setData("IS_LOCK", "N");
            objects.setData("DATA_STATUS", "Y");
        }
        objects.setData("CategoryId", CategoryId.ValueText);
		objects.setData("CategoryName", CategoryName.ValueText);
		objects.setData("TypeId", TypeId.ValueText);
		objects.setData("TypeName", TypeName.ValueText);
		objects.setData("AttributeId", AttributeId.ValueText);
		objects.setData("AttributeName", AttributeName.ValueText);
		objects.setData("ProdDesc", ProdDesc.ValueText);
		objects.setData("Unit", Unit.ValueText);
		objects.setData("Price", Price.ValueText);
        objects.setData("Active", Active.ValueText);
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
        agent.loadSchema("WebServerProject.maintain.SPAD013.SmpHrStationeryMtAgent");
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
}
