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

public partial class DSCWebControlRunTime_DSCWebControlUI_DataListSetting_Setting : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                setSession("PGID",Request.QueryString["PGID"]);
                setSession("ListControlID",Request.QueryString["DSCWebControl"]);
                setSession("ListType", Request.QueryString["ListType"]);

                string sessionPrefix=Request.QueryString["PGID"]+"_"+Request.QueryString["DSCWebControl"];

                bool aggregateintitle = false;
                try
                {
                    aggregateintitle = (bool)Session[sessionPrefix + "_aggregateInTitle"];
                }
                catch { };
                int aggregatedigit = 2;
                try
                {
                    aggregatedigit = (int)Session[sessionPrefix + "_aggregateDigit"];
                }
                catch { };

                bool multiline = false;
                try
                {
                    multiline = (bool)Session[sessionPrefix + "_multiLine"];
                }
                catch { };
                int multilinerowcount = -1;
                try
                {
                    multilinerowcount = (int)Session[sessionPrefix + "_multiLineRowCount"];
                }
                catch { };

                string moneyfieldcolor = "#FF0000";
                try
                {
                    moneyfieldcolor = (string)Session[sessionPrefix + "_moneyFieldColor"];
                }
                catch { };
                if ((moneyfieldcolor==null) || (moneyfieldcolor.Equals("")))
                {
                    moneyfieldcolor = "#FF0000";
                }

                aggregateDigit.ValueText = aggregatedigit.ToString();
                if (aggregateintitle)
                {
                    aggregateInTitle.Checked = true;
                }
                else
                {
                    aggregateInTitle.Checked = false;
                }
                multiLineRowCount.ValueText = multilinerowcount.ToString();
                if (multiline)
                {
                    multiLine.Checked = true;
                }
                else
                {
                    multiLine.Checked = false;
                }
                moneyFieldColor.ValueText = moneyfieldcolor;

                DataObjectSet dataSource = (DataObjectSet)Session[sessionPrefix + "_dataSource"];
                string[] HiddenField = (string[])Session[sessionPrefix + "_HiddenField"];

                initHiddenField(dataSource, HiddenField);

                ArrayList GroupField = (ArrayList)Session[sessionPrefix + "_GroupField"];
                initGroupField(dataSource, GroupField);

                ArrayList SumField = (ArrayList)Session[sessionPrefix + "_SumField"];
                initSumField(dataSource, SumField);

                ArrayList AverageField = (ArrayList)Session[sessionPrefix + "_AverageField"];
                initAverageField(dataSource, AverageField);

                ArrayList STDEVField = (ArrayList)Session[sessionPrefix + "_STDEVField"];
                initSTDEVField(dataSource, STDEVField);

                ArrayList MaxField = (ArrayList)Session[sessionPrefix + "_MaxField"];
                initMaxField(dataSource, MaxField);

                ArrayList MinField = (ArrayList)Session[sessionPrefix + "_MinField"];
                initMinField(dataSource, MinField);

                ArrayList MoneyField = (ArrayList)Session[sessionPrefix + "_MoneyField"];
                initMoneyField(dataSource, MoneyField);

                DataObject temp = dataSource.create();
                string[,] ids = new string[temp.dataField.Length + 1, 2];
                ids[0, 0] = "";
                ids[0, 1] = "取消凍結";

                for (int i = 0; i < temp.dataField.Length; i++)
                {
                    ids[i + 1, 0] = temp.dataField[i];
                    ids[i + 1, 1] = temp.displayName[i];
                }
                FrozenField.setListItem(ids);

                string frozenField = (string)Session[sessionPrefix + "_FrozenField"];
                if (frozenField == null)
                {
                    FrozenField.ValueText = "";
                }
                else
                {
                    FrozenField.ValueText = frozenField;
                }

                ids = new string[temp.dataField.Length, 2];
                string oriOrder = "";
                for (int i = 0; i < temp.dataField.Length; i++)
                {
                    ids[i, 0] = temp.dataField[i];
                    ids[i, 1] = temp.displayName[i];
                    oriOrder += ids[i, 0] + ";";
                }
                FieldOrderList.setListItem(ids);
                setSession("ORIORDER", oriOrder);


                bool[] showSettingPages = (bool[])Session[sessionPrefix + "_showSettingPages"];
                if (showSettingPages == null)
                {
                    showSettingPages = new bool[] { true, true, false, false, false, false, false, false, false, false };
                }
                for (int i = 0; i < showSettingPages.Length; i++)
                {
                    DSCTabControl1.TabPages[i].Hidden = !showSettingPages[i];
                }
            }
        }
    }
    private void initGroupField(DataObjectSet dataSource, ArrayList GroupField)
    {
        if (GroupField == null)
        {
            GroupField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < GroupField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (ddo.dataField[j].Equals((string)GroupField[i]))
                {
                    aryTo.Add(new string[] { ddo.dataField[j], ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < GroupField.Count; j++)
            {
                if (fName.Equals((string)GroupField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                aryFrom.Add(tag);
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        GroupFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        GroupTo.setListItem(ids);
    }
    private void initHiddenField(DataObjectSet dataSource, string[] HiddenField)
    {
        if (HiddenField == null)
        {
            HiddenField = new string[0];
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < HiddenField.Length; j++)
            {
                if (fName.Equals(HiddenField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (hasFound)
            {
                aryTo.Add(tag);
            }
            else
            {
                aryFrom.Add(tag);
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        DisplayFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        DisplayTo.setListItem(ids);
    }
    private void initSumField(DataObjectSet dataSource, ArrayList SumField)
    {
        if (SumField == null)
        {
            SumField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < SumField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (ddo.dataField[j].Equals((string)SumField[i]))
                {
                    aryTo.Add(new string[] { ddo.dataField[j], ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < SumField.Count; j++)
            {
                if (fName.Equals((string)SumField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                if ((ddo.typeField[i].Equals(DataObjectConstants.DECIMAL)) || (ddo.typeField[i].Equals(DataObjectConstants.FLOAT)) || (ddo.typeField[i].Equals(DataObjectConstants.INTEGER)))
                {
                    aryFrom.Add(tag);
                }
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        SumFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        SumTo.setListItem(ids);
    }
    private void initAverageField(DataObjectSet dataSource, ArrayList AverageField)
    {
        if (AverageField == null)
        {
            AverageField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < AverageField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (ddo.dataField[j].Equals((string)AverageField[i]))
                {
                    aryTo.Add(new string[] { ddo.dataField[j], ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < AverageField.Count; j++)
            {
                if (fName.Equals((string)AverageField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                if ((ddo.typeField[i].Equals(DataObjectConstants.DECIMAL)) || (ddo.typeField[i].Equals(DataObjectConstants.FLOAT)) || (ddo.typeField[i].Equals(DataObjectConstants.INTEGER)))
                {
                    aryFrom.Add(tag);
                }
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        AverageFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        AverageTo.setListItem(ids);
    }
    private void initSTDEVField(DataObjectSet dataSource, ArrayList STDEVField)
    {
        if (STDEVField == null)
        {
            STDEVField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < STDEVField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (ddo.dataField[j].Equals((string)STDEVField[i]))
                {
                    aryTo.Add(new string[] { ddo.dataField[j], ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < STDEVField.Count; j++)
            {
                if (fName.Equals((string)STDEVField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                if ((ddo.typeField[i].Equals(DataObjectConstants.DECIMAL)) || (ddo.typeField[i].Equals(DataObjectConstants.FLOAT)) || (ddo.typeField[i].Equals(DataObjectConstants.INTEGER)))
                {
                    aryFrom.Add(tag);
                }
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        STDEVFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        STDEVTo.setListItem(ids);
    }
    private void initMaxField(DataObjectSet dataSource, ArrayList MaxField)
    {
        if (MaxField == null)
        {
            MaxField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < MaxField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (ddo.dataField[j].Equals((string)MaxField[i]))
                {
                    aryTo.Add(new string[] { ddo.dataField[j], ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < MaxField.Count; j++)
            {
                if (fName.Equals((string)MaxField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                if ((ddo.typeField[i].Equals(DataObjectConstants.DECIMAL)) || (ddo.typeField[i].Equals(DataObjectConstants.FLOAT)) || (ddo.typeField[i].Equals(DataObjectConstants.INTEGER)))
                {
                    aryFrom.Add(tag);
                }
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        MaxFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        MaxTo.setListItem(ids);
    }
    private void initMinField(DataObjectSet dataSource, ArrayList MinField)
    {
        if (MinField == null)
        {
            MinField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < MinField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                if (ddo.dataField[j].Equals((string)MinField[i]))
                {
                    aryTo.Add(new string[] { ddo.dataField[j], ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < MinField.Count; j++)
            {
                if (fName.Equals((string)MinField[j]))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                if ((ddo.typeField[i].Equals(DataObjectConstants.DECIMAL)) || (ddo.typeField[i].Equals(DataObjectConstants.FLOAT)) || (ddo.typeField[i].Equals(DataObjectConstants.INTEGER)))
                {
                    aryFrom.Add(tag);
                }
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        MinFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        MinTo.setListItem(ids);
    }
    private void initMoneyField(DataObjectSet dataSource, ArrayList MoneyField)
    {
        if (MoneyField == null)
        {
            MoneyField = new ArrayList();
        }

        ArrayList aryFrom = new ArrayList();
        ArrayList aryTo = new ArrayList();

        DataObject ddo = dataSource.create();

        for (int i = 0; i < MoneyField.Count; i++)
        {
            for (int j = 0; j < ddo.dataField.Length; j++)
            {
                object[] obj = (object[])MoneyField[i];
                string fn = (string)obj[0];
                if (ddo.dataField[j].Equals(fn))
                {
                    aryTo.Add(new string[] { ddo.dataField[j]+":"+((int)obj[1]).ToString()+":"+((bool)obj[2]).ToString(), ddo.displayName[j] + "(" + ddo.dataField[j] + ")" });
                }
            }
        }

        for (int i = 0; i < ddo.dataField.Length; i++)
        {
            string fName = ddo.dataField[i];
            bool hasFound = false;
            for (int j = 0; j < MoneyField.Count; j++)
            {
                object[] obj = (object[])MoneyField[j];
                string fn = (string)obj[0];
                if (fName.Equals(fn))
                {
                    hasFound = true;
                    break;
                }
            }

            string[] tag = new string[] { fName, ddo.displayName[i] + "(" + fName + ")" };
            if (!hasFound)
            {
                if ((ddo.typeField[i].Equals(DataObjectConstants.DECIMAL)) || (ddo.typeField[i].Equals(DataObjectConstants.FLOAT)) || (ddo.typeField[i].Equals(DataObjectConstants.INTEGER)))
                {
                    aryFrom.Add(tag);
                }
            }
        }

        string[,] ids;
        ids = convertArrayListToArray(aryFrom);
        MoneyFrom.setListItem(ids);
        ids = convertArrayListToArray(aryTo);
        MoneyTo.setListItem(ids);
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

    private void moveItem(DSCWebControl.MultiDropDownList sourceControl, DSCWebControl.MultiDropDownList targetControl)
    {
        string[] vle = sourceControl.ValueText;
        if (vle.Length == 0)
        {
            return;
        }

        string[,] fromList = sourceControl.getListItem();
        string[,] toList = targetControl.getListItem();

        ArrayList fromAry = new ArrayList();
        ArrayList toAry = convertArrayToArrayList(toList);
        

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

            if (hasFound)
            {
                toAry.Add(new string[] { fromList[i, 0], fromList[i, 1] });
            }
            else
            {
                fromAry.Add(new string[] { fromList[i, 0], fromList[i, 1] });
            }
        }

        sourceControl.setListItem(convertArrayListToArray(fromAry));
        targetControl.setListItem(convertArrayListToArray(toAry));
    }
    protected void DisplayRight_Click(object sender, EventArgs e)
    {
        moveItem(DisplayFrom, DisplayTo);
    }
    protected void DisplayLeft_Click(object sender, EventArgs e)
    {
        moveItem(DisplayTo, DisplayFrom);
    }
    protected void OKButton_Click(object sender, EventArgs e)
    {
        int test = 0;
        try
        {
            test = int.Parse(aggregateDigit.ValueText);
            if (test < 0)
            {
                MessageBox("彙總函數小數點位數不可小於0位");
                return;
            }
        }
        catch
        {
            MessageBox("彙總函數小數點位數輸入錯誤");
            return;
        }
        try
        {
            test = int.Parse(multiLineRowCount.ValueText);
            if (test < -1)
            {
                MessageBox("多行顯示最大行數輸入錯誤");
                return;
            }
        }
        catch
        {
            MessageBox("多行顯示最大行數輸入錯誤");
            return;
        }
        if (moneyFieldColor.ValueText.Length != 7)
        {
            MessageBox("顏色欄位輸入錯誤");
            return;
        }
        System.Text.RegularExpressions.Regex reg = null;
        string mas=@"#[0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f][0-9A-Fa-f]";
        reg=new System.Text.RegularExpressions.Regex(mas);
        bool isM=reg.IsMatch(moneyFieldColor.ValueText);
        if (!isM)
        {
            MessageBox("顏色欄位輸入錯誤");
            return;
        }

        string sessionPrefix = (string)getSession("PGID") + "_" + (string)getSession("ListControlID");

        Session[sessionPrefix + "_aggregateDigit"] = int.Parse(aggregateDigit.ValueText);
        if (aggregateInTitle.Checked)
        {
            Session[sessionPrefix + "_aggregateInTitle"] = true;
        }
        else
        {
            Session[sessionPrefix + "_aggregateInTitle"] = false;
        }
        Session[sessionPrefix + "_multiLineRowCount"] = int.Parse(multiLineRowCount.ValueText);
        if (multiLine.Checked)
        {
            Session[sessionPrefix + "_multiLine"] = true;
        }
        else
        {
            Session[sessionPrefix + "_multiLine"] = false;
        }
        Session[sessionPrefix + "_moneyFieldColor"] = moneyFieldColor.ValueText;

        string[,] ids = DisplayTo.getListItem();
        string[] HiddenField = new string[ids.GetLength(0)];

        for (int i = 0; i < ids.GetLength(0); i++)
        {
            HiddenField[i] = ids[i, 0];
        }

        Session[sessionPrefix + "_HiddenField"] = HiddenField;

        ids = GroupTo.getListItem();
        ArrayList GroupField = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            GroupField.Add(ids[i, 0]);
        }
        if (GroupField.Count == 0)
        {
            Session[sessionPrefix + "_isGrouping"] = false;
        }
        else
        {
            Session[sessionPrefix + "_isGrouping"] = true;
        }
        Session[sessionPrefix + "_GroupField"] = GroupField;

        ids = SumTo.getListItem();
        ArrayList SumField = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            SumField.Add(ids[i, 0]);
        }
        Session[sessionPrefix + "_SumField"] = SumField;

        ids = AverageTo.getListItem();
        ArrayList AverageField = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            AverageField.Add(ids[i, 0]);
        }
        Session[sessionPrefix + "_AverageField"] = AverageField;

        ids = STDEVTo.getListItem();
        ArrayList STDEVField = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            STDEVField.Add(ids[i, 0]);
        }
        Session[sessionPrefix + "_STDEVField"] = STDEVField;

        ids = MaxTo.getListItem();
        ArrayList MaxField = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            MaxField.Add(ids[i, 0]);
        }
        Session[sessionPrefix + "_MaxField"] = MaxField;

        ids = MinTo.getListItem();
        ArrayList MinField = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            MinField.Add(ids[i, 0]);
        }
        Session[sessionPrefix + "_MinField"] = MinField;

        ids = MoneyTo.getListItem();
        ArrayList MoneyField = new ArrayList();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            string[] tt=ids[i,0].Split(new char[]{':'});

            MoneyField.Add(new object[] { tt[0], int.Parse(tt[1]), bool.Parse(tt[2]) });
        }
        Session[sessionPrefix + "_MoneyField"] = MoneyField;

        if (FrozenField.ValueText.Equals(""))
        {
            Session[sessionPrefix + "_FrozenField"] = null;
        }
        else
        {
            Session[sessionPrefix + "_FrozenField"] = FrozenField.ValueText;
        }

        string testOrder = "";
        string[] orderFields = new string[FieldOrderList.getListItem().GetLength(0)];
        for (int i = 0; i < FieldOrderList.getListItem().GetLength(0); i++)
        {
            testOrder += FieldOrderList.getListItem()[i, 0] + ";";
            orderFields[i] = FieldOrderList.getListItem()[i, 0];
        }
        string oriOrder = (string)getSession("ORIORDER");
        if (!testOrder.Equals(oriOrder))
        {
            Session[sessionPrefix + "_orderField"] = orderFields;
            Session[sessionPrefix + "_hasOrderField"] = true;
        }        
        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");
                Response.Write("window.top.returnValue='YES';");
                break;
            default:
                string js = "";
                js += "window.parent.parent.$('#ecpDialog').attr('ret', 'YES');";
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        } 
    }
    protected void NOButton_Click(object sender, EventArgs e)
    {
        com.dsc.kernal.utility.BrowserProcessor.BrowserType resultType = com.dsc.kernal.utility.BrowserProcessor.detectBrowser(this.Page);
        switch (resultType)
        {
            case com.dsc.kernal.utility.BrowserProcessor.BrowserType.IE:
                Response.Write("window.top.close();");          
                break;
            default:
                string js = "";                
                js += "window.parent.parent.$('#ecpDialog').dialog('close');  ";
                Response.Write(js);
                break;
        } 

    }

    protected void GroupLeft_Click(object sender, EventArgs e)
    {
        moveItem(GroupTo, GroupFrom);
    }
    protected void GroupRight_Click(object sender, EventArgs e)
    {
        moveItem(GroupFrom, GroupTo);
    }
    protected void GroupUp_Click(object sender, EventArgs e)
    {
        if (GroupTo.ValueText.Length != 1)
        {
            return;
        }
        string curValue=GroupTo.ValueText[0];
        int index = -1;
        string[,] ids = GroupTo.getListItem();
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
        GroupTo.setListItem(ids);
    }
    protected void GroupDown_Click(object sender, EventArgs e)
    {
        if (GroupTo.ValueText.Length != 1)
        {
            return;
        }
        string curValue = GroupTo.ValueText[0];
        int index = -1;
        string[,] ids = GroupTo.getListItem();
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            if (ids[i, 0].Equals(curValue))
            {
                index = i;
                break;
            }
        }

        if (index == (ids.GetLength(0)-1))
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
        GroupTo.setListItem(ids);
    }

    protected void SumLeft_Click(object sender, EventArgs e)
    {
        moveItem(SumTo, SumFrom);
    }
    protected void SumRight_Click(object sender, EventArgs e)
    {
        moveItem(SumFrom, SumTo);
    }

    protected void AverageLeft_Click(object sender, EventArgs e)
    {
        moveItem(AverageTo, AverageFrom);
    }
    protected void AverageRight_Click(object sender, EventArgs e)
    {
        moveItem(AverageFrom, AverageTo);
    }
    protected void STDEVLeft_Click(object sender, EventArgs e)
    {
        moveItem(STDEVTo, STDEVFrom);
    }
    protected void STDEVRight_Click(object sender, EventArgs e)
    {
        moveItem(STDEVFrom, STDEVTo);
    }
    protected void MaxLeft_Click(object sender, EventArgs e)
    {
        moveItem(MaxTo, MaxFrom);
    }
    protected void MaxRight_Click(object sender, EventArgs e)
    {
        moveItem(MaxFrom, MaxTo);
    }
    protected void MinLeft_Click(object sender, EventArgs e)
    {
        moveItem(MinTo, MinFrom);
    }
    protected void MinRight_Click(object sender, EventArgs e)
    {
        moveItem(MinFrom, MinTo);
    }
    protected void MoneyRight_Click(object sender, EventArgs e)
    {
        string[] vle = MoneyFrom.ValueText;
        if (vle.Length == 0)
        {
            return;
        }

        int tts = int.Parse(DIGIT.ValueText);
        if (tts < 0)
        {
            MessageBox("小數位數不可小於0");
            return;
        }

        string[,] fromList = MoneyFrom.getListItem();
        string[,] toList = MoneyTo.getListItem();

        ArrayList fromAry = new ArrayList();
        ArrayList toAry = convertArrayToArrayList(toList);


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

            if (hasFound)
            {
                string bTag = "TRUE";
                if (ISACCOUNT.Checked)
                {
                    bTag = "TRUE";
                }
                else
                {
                    bTag = "FALSE";
                }
                toAry.Add(new string[] { fromList[i, 0]+":"+DIGIT.ValueText+":"+bTag, fromList[i, 1] });
            }
            else
            {
                fromAry.Add(new string[] { fromList[i, 0], fromList[i, 1] });
            }
        }

        MoneyFrom.setListItem(convertArrayListToArray(fromAry));
        MoneyTo.setListItem(convertArrayListToArray(toAry));
    }
    protected void MoneyLeft_Click(object sender, EventArgs e)
    {
        string[] vle = MoneyTo.ValueText;
        if (vle.Length == 0)
        {
            return;
        }

        string[,] fromList = MoneyTo.getListItem();
        string[,] toList = MoneyFrom.getListItem();

        ArrayList fromAry = new ArrayList();
        ArrayList toAry = convertArrayToArrayList(toList);


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

            if (hasFound)
            {
                toAry.Add(new string[] { fromList[i, 0].Split(new char[]{':'})[0], fromList[i, 1] });
            }
            else
            {
                fromAry.Add(new string[] { fromList[i, 0], fromList[i, 1] });
            }
        }

        MoneyTo.setListItem(convertArrayListToArray(fromAry));
        MoneyFrom.setListItem(convertArrayListToArray(toAry));
    }

    protected void OrderMoveUp_Click(object sender, EventArgs e)
    {
        string[] vle=FieldOrderList.ValueText;
        if (vle.Length == 0) return;

        int getIndex = 0;
        for (int i = 0; i < FieldOrderList.getListItem().GetLength(0); i++)
        {
            if (vle[0].Equals(FieldOrderList.getListItem()[i, 0]))
            {
                getIndex = i;
                break;
            }
        }

        if (getIndex == 0) return;

        string[,] newids = new string[FieldOrderList.getListItem().GetLength(0), 2];
        for (int i = 0; i < newids.GetLength(0); i++)
        {
            if (i == (getIndex - 1))
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex, 1];
            }
            else if (i == getIndex)
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex - 1, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex - 1, 1];
            }
            else
            {
                newids[i, 0] = FieldOrderList.getListItem()[i, 0];
                newids[i, 1] = FieldOrderList.getListItem()[i, 1];
            }
        }
        FieldOrderList.setListItem(newids);
        FieldOrderList.selectedIndex(new int[] { getIndex - 1 });
    }
    protected void OrderMoveDown_Click(object sender, EventArgs e)
    {
        string[] vle = FieldOrderList.ValueText;
        if (vle.Length == 0) return;

        int getIndex = 0;
        for (int i = 0; i < FieldOrderList.getListItem().GetLength(0); i++)
        {
            if (vle[0].Equals(FieldOrderList.getListItem()[i, 0]))
            {
                getIndex = i;
                break;
            }
        }

        if (getIndex == (FieldOrderList.getListItem().GetLength(0)-1)) return;

        string[,] newids = new string[FieldOrderList.getListItem().GetLength(0), 2];
        for (int i = 0; i < newids.GetLength(0); i++)
        {
            if (i == (getIndex + 1))
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex, 1];
            }
            else if (i == getIndex)
            {
                newids[i, 0] = FieldOrderList.getListItem()[getIndex + 1, 0];
                newids[i, 1] = FieldOrderList.getListItem()[getIndex + 1, 1];
            }
            else
            {
                newids[i, 0] = FieldOrderList.getListItem()[i, 0];
                newids[i, 1] = FieldOrderList.getListItem()[i, 1];
            }
        }
        FieldOrderList.setListItem(newids);
        FieldOrderList.selectedIndex(new int[] { getIndex + 1 });
    }
}
