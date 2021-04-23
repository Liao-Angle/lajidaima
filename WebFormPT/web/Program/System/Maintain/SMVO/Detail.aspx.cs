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
using WebServerProject.maintain.SMVO;
using com.dsc.kernal.agent;
using System.Xml;

public partial class Program_System_Maintain_SMVO_Detail : BaseWebUI.DataListSaveForm
{
    private string connectString = "";
    private string engineType = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMVO";
        ApplicationID = "SYSTEM";
        ModuleID = "SMVAA";
    }
    
    protected override void showData(com.dsc.kernal.databean.DataObject objects)
    {
        SMVOAAA003.clientEngineType = Convert.ToString(Session["engineType"]);
        SMVOAAA003.connectDBString = Convert.ToString(Session["connectString"]);
        bool isAddNew = (bool)getSession("isNew");
        if (isAddNew)
        {
            isAdd.Checked = true;
        }
        else
        {
            isAdd.Checked = false;
            DSCTabControl1.SelectedTab = 1;
            //DSCTabControl1.TabPages[0].Visible = false;
        }
        SMVOAAA002.ValueText = objects.getData("SMVOAAA002");
        if (objects.getData("SMVOAAA003") != "")
        {
            string sql = "";
            string curAgnetSchema = "";
            DataSet ds = null;
            string connectString = Convert.ToString(Session["connectString"]);
            string engineType = Convert.ToString(Session["engineType"]);
            sql = "select SMWDAAA002,SMWDAAA003,SMWDAAA013 from SMWDAAA where SMWDAAA001='" + objects.getData("SMVOAAA003") + "'";
            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);
            ds = engine.getDataSet(sql, "Tmp");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                curAgnetSchema = ds.Tables[0].Rows[0][2].ToString();
            }
            NLAgent agent = new NLAgent();
            agent.loadSchema(curAgnetSchema);
            agent.engine = engine;
            agent.query("1=2");
            engine.close();
            DataObjectSet dataSource = agent.defaultData;
            initMOField(dataSource, null);
        }
        SMVOAAA003.ValueText = objects.getData("SMVOAAA003");
        SMVOAAA003.doValidate();
        SMVOAAA004.ValueText = objects.getData("SMVOAAA004");
        if (!isAddNew && objects.getData("SMVOAAA004") != "")
        {
            setSession("SMVOAAA004",objects.getData("SMVOAAA004"));
            if (isAddNew)
            {
                moveItem(MOFrom, MOTo);
            }
        }
        if (objects.getData("SMVOAAA005").Equals("Y"))
        {
            IsProhibit.Checked = true;
        }

    }

    protected override void saveData(DataObject objects)
    {
        SMVOAAA obj = (SMVOAAA)objects;
        bool isAddNew = (bool)getSession("isNew");

        if (isAddNew)
        {
            obj.SMVOAAA001 = IDProcessor.getID("");
        }
        obj.SMVOAAA002 = SMVOAAA002.ValueText;
        obj.SMVOAAA003 = SMVOAAA003.ValueText;
        obj.SMVOAAA004 = SMVOAAA004.ValueText;
        obj.SMWDAAA002 = SMVOAAA003.ReadOnlyValueText;
        //是否禁用行動簽核
        if (IsProhibit.Checked)
        {
            obj.SMVOAAA005 = "Y";
        }
        else
        {
            obj.SMVOAAA005 = "N";
        }

       
    }
    protected override void saveDB(DataObject objects)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        //修改:設定所使用的agent
        SMVOAgent agent = new SMVOAgent();
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

    private void moveItem(DSCWebControl.MultiDropDownList sourceControl, DSCWebControl.MultiDropDownList targetControl)
    {
        string SMVOAAA004XML = Convert.ToString(getSession("SMVOAAA004"));
        string[] vle = sourceControl.ValueText;
        if (vle.Length == 0 && string.IsNullOrEmpty(SMVOAAA004XML))
        {
            return;
        }

        string[,] fromList = sourceControl.getListItem();
        string[,] toList = targetControl.getListItem();

        ArrayList fromAry = new ArrayList();
        ArrayList toAry = convertArrayToArrayList(toList);
        com.dsc.kernal.utility.XMLProcessor xp = null;
        XmlNodeList xnl = null;

        for (int i = 0; i < fromList.GetLength(0); i++)
        {
            bool hasFound = false;
            for (int j = 0; j < vle.Length; j++)
            {
                if (vle[j].Equals(fromList[i, 0]))
                {
                    hasFound = true;
                    break;
                }
            }
            if (!string.IsNullOrEmpty(SMVOAAA004XML))
            {
                xp = new XMLProcessor(SMVOAAA004XML, 1);
                xnl = xp.selectAllNodes("//FormFieldAccessControl").Item(0).ChildNodes.Item(0).ChildNodes;
                for (int k = 0; k < xnl.Count; k++)
                {
                    if (xnl.Item(k).Name.Equals(FixxmlData(fromList[i, 1])))
                    {
                        hasFound = true;
                        break;
                    }
                }
                setSession("SMVOAAA004", null);
            }


            if (hasFound)
            {
                if (sourceControl.ID != "MOTo")
                {
                    //toAry.Add(new string[] { fromList[i, 0], fromList[i, 1] });
                    toAry.Add(new string[] { com.dsc.kernal.utility.IDProcessor.getID(""), fromList[i, 1] });
                }
                if (sourceControl.ID == "MOFrom")
                {
                    fromAry.Add(new string[] { fromList[i, 0], fromList[i, 1] });
                }
            }
            else
            {
                fromAry.Add(new string[] { fromList[i, 0], fromList[i, 1] });
            }
            

        }
        sourceControl.setListItem(convertArrayListToArray(fromAry));
        targetControl.setListItem(convertArrayListToArray(toAry));
        if (sourceControl.ID != "MOTo")
        {
            setXMLField(convertArrayListToArray(fromAry), Convert.ToString(getSession("tableName")));
        }

        if (sourceControl.ID == "MOFrom")
        {
            setXMLField(convertArrayListToArray(toAry), Convert.ToString(getSession("tableName")));
        }
        else
        {
            setXMLField(convertArrayListToArray(fromAry), Convert.ToString(getSession("tableName")));
        }
    }

    private void setXMLField(string[,] fieldAry, string tableName)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<FormFieldAccessControl>");
        sb.AppendLine("    <" + tableName + ">");
        for (int i = 0; i < fieldAry.GetLength(0); i++)
        {
            sb.AppendLine("        <" + FixxmlData(fieldAry[i, 1]) + "></" + FixxmlData(fieldAry[i, 1]) + ">");
        }
        sb.AppendLine("    </" + tableName + ">");
        sb.AppendLine("</FormFieldAccessControl>");
        if (sb.ToString().Length > 0)
        {
            SMVOAAA004.ValueText = sb.ToString();
        }
    }

    private string FixxmlData(string curData)
    {
        string ret = "";
        int sindex = 0;
        int eindex = 0;
        if (!string.IsNullOrEmpty(curData))
        {
            sindex = curData.LastIndexOf("(");
            eindex = curData.LastIndexOf(")") - sindex;
            ret = curData.Substring(sindex+1, eindex-1);
        }
        return ret;
    }



    protected void MORight_Click(object sender, EventArgs e)
    {
        if (isAdd.Checked)
        {
            moveItem(MOFrom, MOTo);
        }
    }
    protected void MOLeft_Click(object sender, EventArgs e)
    {
        if (isAdd.Checked)
        {
            moveItem(MOTo, MOFrom);
        }
    }

    private void RemoveItem(DSCWebControl.MultiDropDownList MOFrom)
    {
        //for (int i = 0; i < MOTo.getListItem().GetLength(0); i++)
        //{

        //}
    }
    protected void MOUp_Click(object sender, EventArgs e)
    {
        if (MOTo.ValueText.Length != 1)
        {
            return;
        }
        string curValue = MOTo.ValueText[0];
        int index = -1;
        string[,] ids = MOTo.getListItem();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            if (ids[i, 0].Equals(curValue))
            {
                index = i;
                break;
            }
        }

        if (index == 0)
        {
            return;
        }

        int s1 = index;
        int s2 = index - 1;
        ArrayList tmp = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            if (i == s1)
            {
                tmp.Add(new string[] { ids[s2, 0], ids[s2, 1] });
            }
            else if (i == s2)
            {
                tmp.Add(new string[] { ids[s1, 0], ids[s1, 1] });
            }
            else
            {
                tmp.Add(new string[] { ids[i, 0], ids[i, 1] });
            }
        }
        ids = convertArrayListToArray(tmp);
        MOTo.setListItem(ids);
    }
    protected void MODown_Click(object sender, EventArgs e)
    {
        if (MOTo.ValueText.Length != 1)
        {
            return;
        }
        string curValue = MOTo.ValueText[0];
        int index = -1;
        string[,] ids = MOTo.getListItem();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            if (ids[i, 0].Equals(curValue))
            {
                index = i;
                break;
            }
        }

        if (index == (ids.GetLength(0) - 1))
        {
            return;
        }

        int s1 = index;
        int s2 = index + 1;
        ArrayList tmp = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            if (i == s1)
            {
                tmp.Add(new string[] { ids[s2, 0], ids[s2, 1] });
            }
            else if (i == s2)
            {
                tmp.Add(new string[] { ids[s1, 0], ids[s1, 1] });
            }
            else
            {
                tmp.Add(new string[] { ids[i, 0], ids[i, 1] });
            }
        }
        ids = convertArrayListToArray(tmp);
        MOTo.setListItem(ids);
    }
    private string[,] convertArrayListToArray(ArrayList ary)
    {
        string[,] res = new string[ary.Count, 2];
        for (int i = 0; i < ary.Count; i++)
        {
            string[] tag = (string[])ary[i];
            res[i, 0] = tag[0];
            res[i, 1] = tag[1];
        }

        return res;
    }
    private ArrayList convertArrayToArrayList(string[,] ids)
    {
        ArrayList ary = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            ary.Add(new string[] { ids[i, 0], ids[i, 1] });
        }
        return ary;
    }
    private string[,] xmlToAry(string xml)
    {
        com.dsc.kernal.utility.XMLProcessor xp = new XMLProcessor("<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + xml, 1);
        XmlNodeList it = xp.selectAllNodes("//FormFieldAccessControl/" + Convert.ToString(getSession("tableName")));
        string[,] ids = new string[it.Item(0).ChildNodes.Count, 2];
        int ccs = 0;
        foreach (XmlNode xts in it.Item(0).ChildNodes)
        {
            ids[ccs, 0] = com.dsc.kernal.utility.IDProcessor.getID("");
            ids[ccs, 1] = xts.Name;
            ccs++;
        }
        return ids;
    }

    private void initMOField(DataObjectSet dataSource, ArrayList MOField)
    {
        if (MOField == null)
        {
            MOField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();
        

        for (int i = 0; i < MOField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (ddo.dataField[j].Equals((string)MOField[i]))
                {
                    aryTo.Add(new string[] { com.dsc.kernal.utility.IDProcessor.getID(""), ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < MOField.Count; j++)
            {
                if (fName.Equals((string)MOField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { com.dsc.kernal.utility.IDProcessor.getID(""), ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                aryFrom.Add(tag);
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        MOFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        MOTo.setListItem(ids);
        setSession("tableName", ddo.tableName);
    }
    protected void GlassButton1_Click(object sender, EventArgs e)
    {
        bool run = true;
        if (SMVOAAA003.ValueText == "")
        {
            SMVOAAA003L.IsNecessary = true;
            MessageBox("請先選擇流程作業畫面關聯");
        }

        string sql = "";
        string curAgnetSchema = "";
        DataSet ds = null;
        string connectString = Convert.ToString(Session["connectString"]);
        string engineType = Convert.ToString(Session["engineType"]);
        sql = "select SMWDAAA002,SMWDAAA003,SMWDAAA013 from SMWDAAA where SMWDAAA001='" + com.dsc.kernal.utility.Utility.filter(SMVOAAA003.ValueText) + "'";
        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);
        ds = engine.getDataSet(sql, "Tmp");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            curAgnetSchema = ds.Tables[0].Rows[0][2].ToString();
        }
        NLAgent agent = new NLAgent();
        agent.loadSchema(curAgnetSchema);
        agent.engine = engine;
        agent.query("1=2");
        engine.close();
        DataObjectSet dataSource = agent.defaultData;


        if (run)
        {
            initMOField(dataSource, null);
        }
        //MessageBox(MOFrom.getListItem().GetLength(0).ToString());
        //MessageBox(MOTo.getListItem().GetLength(0).ToString());
    }
    protected void SMVOAAA003_SingleOpenWindowButtonClick(string[,] values)
    {
        if (values.GetLength(0) > 0)
        {
            SMVOAAA003L.IsNecessary = false;
        }
    }
    protected void SMVOAAA004_TextChanged(object sender, EventArgs e)
    {
        if (isAdd.Checked)
        {
            setSession("SMVOAAA004", SMVOAAA004.ValueText);
            MOTo.setListItem(new string[0, 0]);
            moveItem(MOFrom, MOTo);
        }
    }
    protected void isAdd_Click(object sender, EventArgs e)
    {
        if (isAdd.Checked)
        {
            MessageBox("若您已進行單身欄位設置，則不建議繼續使用快速設定功能，已編輯資料將會遺失");
            isAdd.Checked = true;
        }
        else
        {
            isAdd.Checked = false;
        }
    }
}
