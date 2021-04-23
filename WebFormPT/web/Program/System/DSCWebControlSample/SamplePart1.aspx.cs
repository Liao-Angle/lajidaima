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

public partial class Program_System_DSCWebControlSample_SamplePart1 : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string sClass = com.dsc.kernal.utility.GlobalProperty.getProperty("global", "ConnectStringClass");
                com.dsc.kernal.db.ConnectStringFactory cs = new com.dsc.kernal.db.ConnectStringFactory();
                com.dsc.kernal.db.AbstractConnectString acs = cs.getConnectStringObject(sClass.Split(new char[] { '.' })[0], sClass);
                acs.getConnectionString();
                string connectString = acs.connectString;
                string engineType = acs.engineType;

                BYWinSample.clientEngineType = engineType;
                BYWinSample.connectDBString = connectString;

                com.dsc.kernal.factory.AbstractEngine engine = null;
                try
                {
                    com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
                    engine = factory.getEngine(engineType, connectString);

                    WebServerProject.maintain.SMVB.SMVBAgent agent = new WebServerProject.maintain.SMVB.SMVBAgent();
                    agent.engine = engine;
                    bool res=agent.query("");

                    if (!res)
                    {
                        throw new Exception(engine.errorString);
                    }
                    if (!agent.defaultData.errorString.Equals(""))
                    {
                        throw new Exception(agent.defaultData.errorString);
                    }

                    com.dsc.kernal.databean.DataObjectSet dos = agent.defaultData;
                    DataListSample.dataSource = dos;
                    DataListSample.showSettingPages = new bool[] { true, true, true, true, true, true, true, true, true, true };

                    engine.close();
                }
                catch (Exception te)
                {
                    try
                    {
                        engine.close();
                    }
                    catch { };
                    MessageBox("測試頁面載入有誤: "+te.Message);
                    return;
                }
            }
        }
    }

    #region BYWin
    protected void ReadOnlyButton_Click(object sender, EventArgs e)
    {
        BYWinSample.ReadOnly = BYWinSample.ReadOnly ^ true;
    }
    protected void HiddenFieldButton_Click(object sender, EventArgs e)
    {
        if (BYWinSample.HiddenIndex.Length == 0)
        {
            BYWinSample.HiddenIndex = new int[] { 0 };
        }
        else
        {
            BYWinSample.HiddenIndex = new int[0];
        }
    }
    protected void OutputIndexButton_Click(object sender, EventArgs e)
    {
        if (BYWinSample.outputIndex == 1)
        {
            BYWinSample.outputIndex = 0;
        }
        else
        {
            BYWinSample.outputIndex = 1;
        }
        GetWhereClauseButton_Click(null, null);
    }
    protected void GetWhereClauseButton_Click(object sender, EventArgs e)
    {
        SQLClause.ValueText = BYWinSample.getWhereClause();
    }
    protected void OutputTypeButton_Click(object sender, EventArgs e)
    {
        if (BYWinSample.outputType == "STRING")
        {
            BYWinSample.outputType = "NUMBER";
        }
        else
        {
            BYWinSample.outputType = "STRING";
        }
        GetWhereClauseButton_Click(null, null);
    }
    protected void SepFieldButton_Click(object sender, EventArgs e)
    {
        if (BYWinSample.sepField == "-")
        {
            BYWinSample.sepField = "=";
        }
        else
        {
            BYWinSample.sepField = "-";
        }
    }
    protected void GetSelectedDataButton_Click(object sender, EventArgs e)
    {
        string[,] temp = BYWinSample.getSelectedDataArray();
        string outs = "";
        for (int i = 0; i < temp.GetLength(0); i++)
        {
            for (int j = 0; j < temp.GetLength(1); j++)
            {
                outs += temp[i, j] + "\t";
            }
            outs += "\n";
        }
        SQLClause.ValueText = outs;
    }
    #endregion

    #region DataList/OutDataList
    protected void DataListHiddenFieldButton_Click(object sender, EventArgs e)
    {
        if (DataListSample.HiddenField.Length == 0)
        {
            DataListSample.HiddenField = new string[] { "SMVAAAB001" };
        }
        else
        {
            DataListSample.HiddenField = new string[0];
        }
        DataListSample.updateTable();
    }
    protected void ShowSerialButton_Click(object sender, EventArgs e)
    {
        DataListSample.showSerial = DataListSample.showSerial ^ true;
        DataListSample.updateTable();
    }
    protected void IsShowCheckBoxButton_Click(object sender, EventArgs e)
    {
        DataListSample.IsShowCheckBox = DataListSample.IsShowCheckBox ^ true;
        DataListSample.updateTable();
    }
    protected void showTotalRowCountButton_Click(object sender, EventArgs e)
    {
        DataListSample.showTotalRowCount = DataListSample.showTotalRowCount ^ true;
        DataListSample.updateTable();
    }
    protected void showExcelButton_Click(object sender, EventArgs e)
    {
        DataListSample.showExcel = DataListSample.showExcel ^ true;
    }
    protected void showPrintButton_Click(object sender, EventArgs e)
    {
        DataListSample.showPrint = DataListSample.showPrint ^ true;
    }
    protected void showSetupButton_Click(object sender, EventArgs e)
    {
        DataListSample.showSetup = DataListSample.showSetup ^ true;
    }
    protected void MultiLineButton_Click(object sender, EventArgs e)
    {
        if (DataListSample.multiLine)
        {
            DataListSample.multiLine = false;
        }
        else
        {
            DataListSample.multiLine = true;
            DataListSample.multiLineRowCount = 2;
        }
        DataListSample.updateTable();
    }
    protected void IsHideSelectAllButton_Click(object sender, EventArgs e)
    {
        DataListSample.IsHideSelectAllButton = DataListSample.IsHideSelectAllButton ^ true;
        DataListSample.updateTable();
    }
    protected void showAdminColumnButton_Click(object sender, EventArgs e)
    {
        if (DataListSample.showAdminColumn == "N,N")
        {
            DataListSample.showAdminColumn = "Y,Y";
        }
        else
        {
            DataListSample.showAdminColumn = "N,N";
        }
        DataListSample.updateTable();
    }
    protected void CheckDataButton_Click(object sender, EventArgs e)
    {
        DataListSample.CheckData(2, 1);
    }
    protected void UnCheckDataButton_Click(object sender, EventArgs e)
    {
        DataListSample.UnCheckData(2, 1);
    }
    protected void UnCheckAllDataButton_Click(object sender, EventArgs e)
    {
        DataListSample.UnCheckAllData();
    }
    protected void getSelectedItemButton_Click(object sender, EventArgs e)
    {
        string outstr = "";
        com.dsc.kernal.databean.DataObject[] ddo = DataListSample.getSelectedItem();
        for (int i = 0; i < ddo.Length; i++)
        {
            outstr += ddo[i].getData("SMVAAAB002") + ";";
        }
        DataListOutput.ValueText = outstr;
    }
    protected void reOrderFieldButton_Click(object sender, EventArgs e)
    {
        com.dsc.kernal.databean.DataObject ddo = DataListSample.dataSource.getAvailableDataObject(0);
        string[] curs = new string[ddo.dataField.Length];
        for (int i = 0; i < curs.Length; i++)
        {
            if (i == 1)
            {
                curs[i] = ddo.dataField[2];
            }
            else if (i == 2)
            {
                curs[i] = ddo.dataField[1];
            }
            else
            {
                curs[i] = ddo.dataField[i];
            }
        }
        DataListSample.reOrderField(curs);
    }
    #endregion

    #region DSCCheckBox
    protected void CheckBoxTextButton_Click(object sender, EventArgs e)
    {
        if (DSCCheckBoxSample.Text == "這是DSCCheckBox")
        {
            DSCCheckBoxSample.Text = "點了Text";
        }
        else
        {
            DSCCheckBoxSample.Text = "這是DSCCheckBox";
        }
    }
    protected void CheckBoxReadOnlyButton_Click(object sender, EventArgs e)
    {
        DSCCheckBoxSample.ReadOnly = DSCCheckBoxSample.ReadOnly ^ true;
    }
    protected void CheckBoxCheckButton_Click(object sender, EventArgs e)
    {
        DSCCheckBoxSample.Checked = DSCCheckBoxSample.Checked ^ true;
        this.DSCCheckBoxSample_Click(sender, e);
    }
    protected void DSCCheckBoxSample_Click(object sender, EventArgs e)
    {
        if (DSCCheckBoxSample.Checked)
        {
            DSCCheckBoxResult.ValueText = "有勾選";
        }
        else
        {
            DSCCheckBoxResult.ValueText = "未勾選";
        }
    }
    #endregion

    #region DSCRichEdit
    protected void RichEditValueTextButton_Click(object sender, EventArgs e)
    {
        RichEditResult.ValueText = DSCRichEditSample.ValueText;
    }
    protected void isShowStandardBarButton_Click(object sender, EventArgs e)
    {
        DSCRichEditSample.isShowStandardBar = DSCRichEditSample.isShowStandardBar ^ true;
    }
    protected void isShowMarkBarButton_Click(object sender, EventArgs e)
    {
        DSCRichEditSample.isShowMarkBar = DSCRichEditSample.isShowMarkBar ^ true;
    }
    protected void isShowMarkBar2Button_Click(object sender, EventArgs e)
    {
        DSCRichEditSample.isShowMarkBar2 = DSCRichEditSample.isShowMarkBar2 ^ true;
    }
    protected void RichEditReadOnlyButton_Click(object sender, EventArgs e)
    {
        DSCRichEditSample.ReadOnly = DSCRichEditSample.ReadOnly ^ true;
    }
    protected void addListItemSettingButton_Click(object sender, EventArgs e)
    {
        DSCRichEditSample.addListItemSetting("【", "】", "1;2;3;4;5;6;7");
    }
    protected void setStandardButtonControlButton_Click(object sender, EventArgs e)
    {
        DSCRichEditSample.setStandardButtonControl(true, true, true, true, true, true, true, true, false, false, false, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false);
    }
    protected void addFontArrayButton_Click(object sender, EventArgs e)
    {
        DSCRichEditSample.addFontArray("MS Gothic");
    }
    protected void getInnerValueTextButton_Click(object sender, EventArgs e)
    {
        RichEditResult.ValueText = DSCRichEditSample.getInnerValueText();
    }
    protected void getDIValueTextButton_Click(object sender, EventArgs e)
    {
        RichEditResult.ValueText = DSCRichEditSample.getDIValueText();
    }
    #endregion

    #region DSCLabel
    protected void CustomCSSButton_Click(object sender, EventArgs e)
    {
        if (DSCLabelSample.CustomCSS == "")
        {
            DSCLabelSample.CustomCSS = "FormHeadHead";
        }
        else
        {
            DSCLabelSample.CustomCSS = "";
        }
    }
    protected void TextAlignButton_Click(object sender, EventArgs e)
    {
        if (DSCLabelSample.TextAlign == DSCWebControl.DSCLabel.LEFT)
        {
            DSCLabelSample.TextAlign = DSCWebControl.DSCLabel.CENTER;
        }
        else if (DSCLabelSample.TextAlign == DSCWebControl.DSCLabel.CENTER)
        {
            DSCLabelSample.TextAlign = DSCWebControl.DSCLabel.RIGHT;
        }
        else
        {
            DSCLabelSample.TextAlign = DSCWebControl.DSCLabel.LEFT;
        }
    }
    protected void IsNecessaryButton_Click(Object sender, EventArgs e)
    {
        DSCLabelSample.IsNecessary = DSCLabelSample.IsNecessary ^ true;
    }
    #endregion

    #region DSCRadioButton
    protected void RadioTextButton_Click(object sender, EventArgs e)
    {
        if (DSCR12.Text == "Change")
        {
            DSCR12.Text = "群組1, 選項2";
        }
        else
        {
            DSCR12.Text = "Change";
        }
    }
    protected void RadioCheckedButton_Click(object sender, EventArgs e)
    {
        if (DSCR11.Checked)
        {
            DSCR11.Checked = false;
            DSCR12.Checked = true;
        }
        else
        {
            DSCR11.Checked = true;
            DSCR12.Checked = false;
        }
    }
    protected void DSCR_Click(object sender, EventArgs e)
    {
        if (DSCR11.Checked)
        {
            DSCRadioResult.ValueText = "群組1,選項2 被選取\n";
        }
        else
        {
            DSCRadioResult.ValueText = "群組1,選項2 被選取\n";
        } 
        if (DSCR21.Checked)
        {
            DSCRadioResult.ValueText += "群組2,選項1 被選取";
        }
        else
        {
            DSCRadioResult.ValueText += "群組2,選項2 被選取";
        }
    }
    protected void RadioReadOnlyButton_Click(object sender, EventArgs e)
    {
        if (DSCR11.ReadOnly)
        {
            DSCR11.ReadOnly = false;
            DSCR12.ReadOnly = false;
            DSCR21.ReadOnly = false;
            DSCR22.ReadOnly = false;
        }
        else
        {
            DSCR11.ReadOnly = true;
            DSCR12.ReadOnly = true;
            DSCR21.ReadOnly = true;
            DSCR22.ReadOnly = true;
        }
    }
    #endregion

}
