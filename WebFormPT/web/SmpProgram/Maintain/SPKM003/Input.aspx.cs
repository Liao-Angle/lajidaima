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

public partial class SmpProgram_Maintain_SPKM003_Input : BaseWebUI.DataListSaveForm
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
                UserOID.clientEngineType = (string)Session["engineType"];
                UserOID.connectDBString = (string)Session["connectString"];
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
        //head                    
		OID.ValueText = objects.getData("OID"); 
		objectVersion.ValueText = objects.getData("objectVersion"); 		
		id.ValueText = objects.getData("id"); 
		groupName.ValueText = objects.getData("groupName"); 
		//organizationOID.ValueText = objects.getData("organizationOID"); 
		description.ValueText = objects.getData("description"); 
		CompanyName.ValueText = objects.getData("CompanyName"); 
				
        DataObjectSet detail = null;
        if (isNew)
        {
            detail = new DataObjectSet();
            detail.isNameLess = true;
            detail.setAssemblyName("WebServerProject");
            detail.setChildClassString("WebServerProject.maintain.SPKM003.Group_User");
            detail.setTableName("Group_User");
            detail.loadFileSchema();
            objects.setChild("Group_User", detail);
        }
        else
        {
            detail = objects.getChild("Group_User");			
        }
		CompanyName.ReadOnly = true;
		id.ReadOnly = true;
		groupName.ReadOnly = true;

        //detail
        GroupDetailList.dataSource = detail;
        GroupDetailList.HiddenField = new string[] { "GroupOID", "UserOID"};
        GroupDetailList.reSortCondition("群組人員工號", DataObjectConstants.ASC);
        GroupDetailList.updateTable();
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
			objects.setData("OID", IDProcessor.getID(""));   
        }
        objects.setData("objectVersion", objectVersion.ValueText);
		objects.setData("id", id.ValueText);
		objects.setData("groupName", groupName.ValueText);
		//objects.setData("organizationOID", organizationOID.ValueText);
		objects.setData("description", description.ValueText);
		objects.setData("CompanyName", CompanyName.ValueText);

        DataObjectSet detail = GroupDetailList.dataSource;
        for (int i = 0; i < detail.getAvailableDataObjectCount(); i++)
        {
            DataObject dt = detail.getAvailableDataObject(i);
            dt.setData("GroupOID", objects.getData("OID"));
        }

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
        agent.loadSchema("WebServerProject.maintain.SPKM003.GroupAgent");
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

    /// <summary>
    ///Group_SaveRow
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    protected bool GroupDetailList_SaveRowData(DataObject objects, bool isNew)
    {
        string strErrMsg = "";		
		
        if (UserOID.ValueText.Equals(""))
        {
            strErrMsg = "請選擇群組人員!\n";
        }

        if (!strErrMsg.Equals(""))
        {
            MessageBox(strErrMsg);
            return false;
        }
		
        if (isNew)
        {
            objects.setData("GroupOID", "TEMP");                  
        }
		//MessageBox("GroupOID guid : " + GroupOID.GuidValueText);
				
		objects.setData("UserOID", UserOID.GuidValueText);
		objects.setData("id", UserOID.ValueText);
        objects.setData("userName", UserOID.ReadOnlyValueText);       

        return true;
    }

    /// <summary>
    ///Group_ShowRow
    /// </summary>
    /// <param name="objects"></param>
    /// <param name="isNew"></param>
    protected void GroupDetailList_ShowRowData(DataObject objects)
    {
        UserOID.tableName = "Users";
        UserOID.serialNum = "003";
        UserOID.idIndex = 2;
        UserOID.valueIndex = 3;
        
        UserOID.GuidValueText = objects.getData("UserOID");
        UserOID.doGUIDValidate();
    }
}
