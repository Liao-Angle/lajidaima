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
using WebServerProject.auth.SMSB;

public partial class Program_DSCAuthService_Maintain_SMSB_Detail: BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected override void showData(DataObject objects)
    {

        SMSAABA obj = (SMSAABA)objects;

        SMSAABA002.ValueText = obj.SMSAABA002;
        SMSAABA003.ValueText = obj.SMSAABA003;

        DataObjectSet bbset = null;
        DataObjectSet bcset = null;

        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            bbset = new DataObjectSet();
            bbset.setAssemblyName("WebServerProject");
            bbset.setChildClassString("WebServerProject.auth.SMSB.SMSAABB");
            bbset.setTableName("SMSAABB");
            obj.setChild("SMSAABB", bbset);
        }
        else
        {
            bbset = obj.getChild("SMSAABB");
        }

        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        string sql;
        DataSet ds;

        sql = "select * from SMSAAAA";
        DataSet smsa = engine.getDataSet(sql, "TEMP");

        string ins = "'*'";
        ArrayList rightAry = new ArrayList();
        if (bbset != null)
        {

            for (int i = 0; i < bbset.getDataObjectCount(); i++)
            {
                if (!bbset.getDataObject(i).isDelete())
                {
                    ins += ",'" + bbset.getDataObject(i).getData("SMSAABB003") + "'";
                    string vls = "";
                    for (int z = 0; z < smsa.Tables[0].Rows.Count; z++)
                    {
                        if (smsa.Tables[0].Rows[z]["SMSAAAA002"].ToString().Equals(bbset.getDataObject(i).getData("SMSAABB003")))
                        {
                            vls = smsa.Tables[0].Rows[z]["SMSAAAA003"].ToString();
                            break;
                        }
                    }
                    string mo = "";
                    if (bbset.getDataObject(i).getData("AUTH01").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH01", "讀取|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH02").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH02", "新增|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH03").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH03", "修改|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH04").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH04", "刪除|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH05").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH05", "列印|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH06").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH06", "報表|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH07").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH07", "擁有|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH08").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH08", "權限08|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH09").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH09", "權限09|");
                    }
                    if (bbset.getDataObject(i).getData("AUTH10").Equals("Y"))
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH10", "權限10|");
                    }
                    if (mo.Length > 0)
                    {
                        mo = mo.Substring(0, mo.Length - 1);
                    }
                    else
                    {
                        mo = "";
                    }
                    string[] li = new string[] { bbset.getDataObject(i).getData("SMSAABB003"), vls + "(" + bbset.getDataObject(i).getData("SMSAABB003") + ")-" + mo };
                    rightAry.Add(li);
                }
            }
        }
        string[,] rightItem = new string[rightAry.Count, 2];
        for (int i = 0; i < rightAry.Count; i++)
        {
            string[] tag = (string[])rightAry[i];
            rightItem[i, 0] = tag[0];
            rightItem[i, 1] = tag[1];
        }
        RightBox.setListItem(rightItem);

        
        sql = "select SMSAAAA001, SMSAAAA002, SMSAAAA003 from SMSAAAA where SMSAAAA002 not in (" + ins + ")";
        ds = engine.getDataSet(sql, "TEMP");

        ArrayList leftAry = new ArrayList();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string[] tag = new string[] { ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString() + "(" + ds.Tables[0].Rows[i][1].ToString() + ")" };
            leftAry.Add(tag);
        }
        string[,] leftItem = new string[leftAry.Count, 2];
        for (int i = 0; i < leftAry.Count; i++)
        {
            string[] tag = (string[])leftAry[i];
            leftItem[i, 0] = tag[0];
            leftItem[i, 1] = tag[1];
        }
        LeftBox.setListItem(leftItem);

        engine.close();

        if (isAddNew)
        {
            bcset = new DataObjectSet();
            bcset.setAssemblyName("WebServerProject");
            bcset.setChildClassString("WebServerProject.auth.SMSB.SMSAABC");
            bcset.setTableName("SMSAABC");
            obj.setChild("SMSAABC", bcset);
        }
        else
        {
            bcset = obj.getChild("SMSAABC");
        }
        BCList.NoAdd = true;
        BCList.NoModify = true;
        BCList.HiddenField = new string[] { "SMSAABC001", "SMSAABC002" };
        BCList.dataSource = bcset;
        BCList.updateTable();
    }
    protected override void saveData(DataObject objects)
    {
        bool isAddNew = (bool)getSession("isNew");
        SMSAABA obj = (SMSAABA)objects;
        if (isAddNew)
        {
            obj.SMSAABA001 = IDProcessor.getID("");
        }
        obj.SMSAABA002 = SMSAABA002.ValueText;
        obj.SMSAABA003 = SMSAABA003.ValueText;

        DataObjectSet bbset = obj.getChild("SMSAABB");
        bbset.clear();
        string[,] rValue=RightBox.getListItem();
        for (int i = 0; i < rValue.GetLength(0); i++)
        {
            
            SMSAABB bb = (SMSAABB)bbset.create();
            bb.SMSAABB001 = IDProcessor.getID("");
            bb.SMSAABB002 = obj.SMSAABA001;
            bb.SMSAABB003 = rValue[i, 0];            
            //string auths = rValue[i, 1].Split(new char[] { '-' })[1];
            string auths = rValue[i, 1].Substring(rValue[i, 1].LastIndexOf('-') + 1);
            bb.AUTH01 = "N";
            bb.AUTH02 = "N";
            bb.AUTH03 = "N";
            bb.AUTH04 = "N";
            bb.AUTH05 = "N";
            bb.AUTH06 = "N";
            bb.AUTH07 = "N";
            bb.AUTH08 = "N";
            bb.AUTH09 = "N";
            bb.AUTH10 = "N";
            if (auths.Length > 0)
            {
                string[] detail = auths.Split(new char[] { '|' });
                for (int x = 0; x < detail.Length; x++)
                {
                    if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH01a", "讀取")))
                    {
                        bb.AUTH01 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH02a", "新增")))
                    {
                        bb.AUTH02 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH03a", "修改")))
                    {
                        bb.AUTH03 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH04a", "刪除")))
                    {
                        bb.AUTH04 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH05a", "列印")))
                    {
                        bb.AUTH05 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH06a", "報表")))
                    {
                        bb.AUTH06 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH07a", "擁有")))
                    {
                        bb.AUTH07 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH08a", "權限08")))
                    {
                        bb.AUTH08 = "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH09a", "權限09")))
                    {
                        bb.AUTH09= "Y";
                    }
                    else if (detail[x].Equals(com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH10a", "權限10")))
                    {
                        bb.AUTH10 = "Y";
                    }
                }

            }
            bool res = bbset.add(bb);
            if (!res)
            {
                throw new Exception(bbset.errorString);
            }
        }
        obj.setChild("SMSAABB", bbset);

        DataObjectSet bcset = obj.getChild("SMSAABC");
        for (int i = 0; i < bcset.getDataObjectCount(); i++)
        {
            bcset.getDataObject(i).setData("SMSAABC002", obj.SMSAABA001);
        }

    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMSBAgent agent = new SMSBAgent();
        agent.engine = engine;
        agent.query("1=2");

        //取得資料
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
    protected void SelectButton_Click(object sender, EventArgs e)
    {
        connectString = (string)Session["connectString"];
        engineType = (string)Session["engineType"];

        openWin1.PageUniqueID = this.PageUniqueID;
        openWin1.clientEngineType = engineType;
        openWin1.connectDBString = connectString;
        openWin1.identityID = "0001";
        openWin1.paramString = "id";
        openWin1.whereClause = "";
        openWin1.openWin("Users", "001", true, "0001");
        //openWin1.openWinS("Users", "001", true, "0001", "OpenWin", "openWin1");
    }
    protected void openWin1_OpenWindowButtonClick(string identityID, string[,] values)
    {
        DataObjectSet bcset = BCList.dataSource;

        for (int i = 0; i < values.GetLength(0); i++)
        {
            SMSAABC bc = (SMSAABC)bcset.create();
            bc.SMSAABC001 = IDProcessor.getID("");
            bc.SMSAABC002 = "TEMP";
            bc.SMSAABC003 = values[i, 1];
            bc.userName = values[i, 2];

            bcset.add(bc);
        }
        BCList.dataSource = bcset;
        BCList.updateTable();
    }
    protected void ToRightButton_Click(object sender, EventArgs e)
    {
        try
        {
            string[,] vls = LeftBox.getListItem();
            ArrayList newLeft = new ArrayList();
            ArrayList newRight = new ArrayList();

            for (int i = 0; i < vls.GetLength(0); i++)
            {
                bool hasFound = false;
                for (int j = 0; j < LeftBox.ValueText.Length; j++)
                {
                    if (LeftBox.ValueText[j].Equals(vls[i, 0]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (hasFound)
                {
                    string mo = "";
                    if (AUTH01.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH01", "讀取|");
                    }
                    if (AUTH02.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH02", "新增|");
                    }
                    if (AUTH03.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH03", "修改|");
                    }
                    if (AUTH04.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH04", "刪除|");
                    }
                    if (AUTH05.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH05", "列印|");
                    }
                    if (AUTH06.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH06", "報表|");
                    }
                    if (AUTH07.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH07", "擁有|");
                    }
                    if (AUTH08.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH08", "權限08|");
                    }
                    if (AUTH09.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH09", "權限09|");
                    }
                    if (AUTH10.Checked)
                    {
                        mo += com.dsc.locale.LocaleString.getSystemMessageString("program_dscauthservice_maintain_smsb_detail_aspx.language.ini", "message", "AUTH10", "權限10|");
                    }
                    if (mo.Length > 0)
                    {
                        mo = mo.Substring(0, mo.Length - 1);
                    }
                    string[] li = new string[] { vls[i, 0], vls[i, 1] + "-" + mo };
                    newRight.Add(li);
                }
                else
                {
                    newLeft.Add(new string[] { vls[i, 0], vls[i, 1] });
                }
            }
            string[,] leftItem = new string[newLeft.Count, 2];
            for (int i = 0; i < newLeft.Count; i++)
            {
                string[] tag = (string[])newLeft[i];
                leftItem[i, 0] = tag[0];
                leftItem[i, 1] = tag[1];
            }
            LeftBox.setListItem(leftItem);

            string[,] rightItem = new string[RightBox.getListItem().GetLength(0) + newRight.Count, 2];
            for (int i = 0; i < RightBox.getListItem().GetLength(0); i++)
            {
                rightItem[i, 0] = RightBox.getListItem()[i, 0];
                rightItem[i, 1] = RightBox.getListItem()[i, 1];
            }
            for (int i = 0; i < newRight.Count; i++)
            {
                string[] tag=(string[])newRight[i];
                rightItem[RightBox.getListItem().GetLength(0) + i, 0] = tag[0];
                rightItem[RightBox.getListItem().GetLength(0) + i, 1] = tag[1];
            }
            RightBox.setListItem(rightItem);
        }
        catch (Exception te)
        {
            MessageBox(te.Message.Replace("\n","\\n"));
        }

    }
    protected void ToLeftButton_Click(object sender, EventArgs e)
    {
        try
        {
            string[,] vls = RightBox.getListItem();
            ArrayList newRight = new ArrayList();
            ArrayList newLeft = new ArrayList();

            for (int i = 0; i < vls.GetLength(0); i++)
            {
                bool hasFound = false;
                for (int j = 0; j < RightBox.ValueText.Length; j++)
                {
                    if (RightBox.ValueText[j].Equals(vls[i, 0]))
                    {
                        hasFound = true;
                        break;
                    }
                }
                if (hasFound)
                {
                    //string[] li = new string[] { vls[i, 0], vls[i, 1].Split(new char[] { '-' })[0] };
                    string[] li = new string[] { vls[i, 0], vls[i, 1].Substring(0,vls[i, 1].LastIndexOf('-'))};
                    newLeft.Add(li);
                }
                else
                {
                    newRight.Add(new string[] { vls[i, 0], vls[i, 1] });
                }
            }
            string[,] RightItem = new string[newRight.Count, 2];
            for (int i = 0; i < newRight.Count; i++)
            {
                string[] tag = (string[])newRight[i];
                RightItem[i, 0] = tag[0];
                RightItem[i, 1] = tag[1];
            }
            RightBox.setListItem(RightItem);

            string[,] LeftItem = new string[LeftBox.getListItem().GetLength(0) + newLeft.Count, 2];
            for (int i = 0; i < LeftBox.getListItem().GetLength(0); i++)
            {
                LeftItem[i, 0] = LeftBox.getListItem()[i, 0];
                LeftItem[i, 1] = LeftBox.getListItem()[i, 1];
            }
            for (int i = 0; i < newLeft.Count; i++)
            {
                string[] tag = (string[])newLeft[i];
                LeftItem[LeftBox.getListItem().GetLength(0) + i, 0] = tag[0];
                LeftItem[LeftBox.getListItem().GetLength(0) + i, 1] = tag[1];
            }
            LeftBox.setListItem(LeftItem);
        }
        catch (Exception te)
        {
            MessageBox(te.Message.Replace("\n", "\\n"));
        }
    }
}
