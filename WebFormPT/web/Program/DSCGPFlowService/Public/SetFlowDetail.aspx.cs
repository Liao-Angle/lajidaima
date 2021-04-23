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
using WebServerProject.flow.SMWP;

public partial class Program_DSCGPFlowService_Public_SetFlowDetail :BaseWebUI.DataListInlineForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                setSession("SMWDAAA001", (string)Session["TEMPSETFLOW001"]);
            }
        }
    }
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        OpenWin1.clientEngineType = (string)Session["engineType"];
        OpenWin1.connectDBString = (string)Session["connectString"];


        string[,] ids = new string[,]{
                    {"HUMAN",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "HUMAN", "員工")},
                    {"ORGANIZATION_UNIT",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "ORGANIZATION_UNIT", "部門")},
                    {"GROUP",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "GROUP", "群組")}
                };

        participantType.setListItem(ids);

        ids = new string[,]{
            {"FOR_EACH",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "FOR_EACH", "非單一簽核")},
            {"FIREST_GET_FIRST_WIN",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "FIREST_GET_FIRST_WIN", "單一簽核")}
        };
        multiUserMode.setListItem(ids);

        ids = new string[,]{
            {"NORMAL",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "NORMAL", "一般")},
            {"NOTICE",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "NOTICE", "通知")}
        };
        performType.setListItem(ids);

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(OpenWin1.clientEngineType, OpenWin1.connectDBString);

        string sql = "select SMWCAAA002 from SMWCAAA where SMWCAAA002 in (select SMWDAAD003 from SMWDAAD where SMWDAAD002='" + Utility.filter((string)getSession("SMWDAAA001")) + "')";
        DataSet ds = engine.getDataSet(sql, "TEMP");

        if (ds.Tables[0].Rows.Count > 0)
        {
            ids = new string[ds.Tables[0].Rows.Count, 2];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                ids[i, 1] = ds.Tables[0].Rows[i][0].ToString();
            }
        }
        else
        {
            ids = new string[,]{
                {"#",com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "Msg1", "無可加簽的角色")}
            };
        }
        engine.close();

        RoleName.setListItem(ids);

        bool isAddNew = (bool)getSession("isNew");
        SMWPAAA obj=(SMWPAAA)objects;

        if (!isAddNew)
        {
            string[] tag = obj.SMWPAAA004.Split(new char[] { '#' });
            ids = new string[tag.Length, 2];
            for (int i = 0; i < tag.Length; i++)
            {
                string[] ztag = tag[i].Split(new char[] { ';' });
                ids[i, 0] = tag[i];
                ids[i, 1] = ztag[2] + ztag[3];
            }
            performers.setListItem(ids);
        }
    }
    protected override void saveData(com.dsc.kernal.databean.DataObject objects)
    {
        string[,] lt = performers.getListItem();
        if ((lt == null) || (lt.GetLength(0) == 0))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "Msg2", "請選擇參與者"));
        }
        if (RoleName.ValueText.Equals("#"))
        {
            throw new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "Msg1", "無可加簽的角色"));
        }
        bool isAddNew = (bool)getSession("isNew");
        SMWPAAA obj = (SMWPAAA)objects;

        if (isAddNew)
        {
            obj.SMWPAAA001 = IDProcessor.getID("");
            obj.SMWPAAA002 = "100";
        }
        obj.SMWPAAA003 = RoleName.ValueText;
       
        string tag = "";
        for (int i = 0; i < lt.GetLength(0); i++)
        {
            tag += lt[i, 0] + "#";
        }
        tag = tag.Substring(0, tag.Length - 1);
        obj.SMWPAAA004 = tag;
        obj.SMWPAAA005 = multiUserMode.ValueText;
        obj.SMWPAAA006 = performType.ValueText;
    }
    protected void SelectParticipant_Click(object sender, EventArgs e)
    {
        OpenWin1.PageUniqueID = this.PageUniqueID;
        OpenWin1.identityID = "0001";
        if (participantType.ValueText.Equals("HUMAN"))
        {
            OpenWin1.paramString = "id";
            OpenWin1.openWin("Users", "001", false, "0001");
        }
        else if (participantType.ValueText.Equals("ORGANIZATION_UNIT"))
        {
            OpenWin1.paramString = "id";
            OpenWin1.openWin("OrgUnit", "001", false, "0001");
        }
        else if (participantType.ValueText.Equals("GROUP"))
        {
            OpenWin1.paramString = "id";
            OpenWin1.openWin("OrgGroup", "001", false, "0001");
        }
    }
    protected void OpenWin1_OpenWindowButtonClick(string identityid, string[,] values)
    {
        if (values != null)
        {
            string[,] curValue = null;
            curValue = performers.getListItem();
            if (curValue == null)
            {
                curValue = new string[0, 2];
            }


            if (participantType.ValueText.Equals("HUMAN"))
            {
                curValue = addItem(curValue, participantType.ValueText, values[0, 0], values[0, 2]);
            }
            else if (participantType.ValueText.Equals("ORGANIZATION_UNIT"))
            {
                curValue = addItem(curValue, participantType.ValueText, values[0, 0], values[0, 2]);
            }
            else if (participantType.ValueText.Equals("GROUP"))
            {
                curValue = addItem(curValue, participantType.ValueText, values[0, 0], values[0, 2]);
            }

            performers.setListItem(curValue);
        }

    }

    private string[,] addItem(string[,] ori, string type, string OID, string uName)
    {
        string[,] newi = new string[ori.GetLength(0)+1, 2];
        for (int i = 0; i < ori.GetLength(0); i++)
        {
            newi[i, 0] = ori[i, 0];
            newi[i, 1] = ori[i, 1];
        }
        string typeStr = "";
        switch (type)
        {
            case "HUMAN":
                typeStr = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "typeStr1", "[員工]");
                break;
            case "ORGANIZATION_UNIT":
                typeStr = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "typeStr2", "[部門]");
                break;
            case "GROUP":
                typeStr = com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_public_setflowdetail_aspx.language.ini", "message", "typeStr3", "[群組]");
                break;
        }

        newi[newi.GetLength(0) - 1, 0] = type + ";" + OID+";"+typeStr+";"+uName;
        newi[newi.GetLength(0) - 1, 1] = typeStr+uName;
        return newi;
    }
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        try
        {
            string[] gids = performers.ValueText;
            ArrayList temp = new ArrayList();
            string[,] cur = performers.getListItem();
            for (int i = 0; i < cur.GetLength(0); i++)
            {
                bool hasF = false;
                for (int j = 0; j < gids.Length; j++)
                {
                    if (cur[i, 0].Equals(gids[j]))
                    {
                        hasF = true;
                        break;
                    }
                }
                if (!hasF)
                {
                    string[] tag = new string[] { cur[i, 0], cur[i, 1] };
                    temp.Add(tag);
                }
            }
            cur = new string[temp.Count, 2];
            for (int i = 0; i < temp.Count; i++)
            {
                string[] tag = (string[])temp[i];
                cur[i, 0] = tag[0];
                cur[i, 1] = tag[1];
            }
            performers.setListItem(cur);
        }
        catch { };
    }
}
