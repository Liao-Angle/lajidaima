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

public partial class Program_System_DSCWebControlSample_SamplePart3 : BaseWebUI.GeneralWebPage
{
    protected override void OnInit(EventArgs e)
    {
        SDTSample.FixedTime = new string[] { "03:15", "04:15", "05:15" };
        SFSample.alignRight = false;
        SOWSample.showReadOnlyField = true;
        SOWSample.FixValueTextWidth = new Unit("100px");
        SOWSample.FixReadOnlyValueTextWidth = new Unit("150px");

        base.OnInit(e);
    }
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

                SOWSample.clientEngineType = engineType;
                SOWSample.connectDBString = connectString;

                com.dsc.kernal.factory.AbstractEngine engine = null;
                try
                {
                    com.dsc.kernal.factory.IOFactory factory = new com.dsc.kernal.factory.IOFactory();
                    engine = factory.getEngine(engineType, connectString);

                    string[,] ids = new string[,]{
                        {"Value01", "選項一"},
                        {"Value02", "選項二"},
                        {"Value03", "選項三"},
                        {"Value04", "選項四"}
                    };

                    SDDLSample.setListItem(ids);

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

    #region RoutineWizard
    protected void RWValueTextButton_Click(object sender, EventArgs e)
    {
        RWResult.ValueText = RWSample.ValueText;
    }
    protected void RWReadOnlyButton_Click(object sender, EventArgs e)
    {
        RWSample.ReadOnly = RWSample.ReadOnly ^ true;
    }
    #endregion

    #region SingleDateTimeField
    protected void SDTSampleClick(string value)
    {
        try
        {
            SDTResult.ValueText = value;
        }
        catch (Exception te)
        {
            MessageBox(te.Message);
        }
    }
    protected void DateTimeModeButton_Click(object sender, EventArgs e)
    {
        if (SDTSample.DateTimeMode == 5)
        {
            SDTSample.DateTimeMode = 0;
            SDTResult.ValueText = SDTSample.ValueText;
        }
        else
        {
            SDTSample.DateTimeMode++;
            SDTResult.ValueText = SDTSample.ValueText;
        }
    }
    protected void ReadOnlyValueTextButton_Click(object sender, EventArgs e)
    {
        SDTSample.ReadOnlyValueText = SDTSample.ReadOnlyValueText ^ true;
    }
    protected void SDTReadOnlyButton_Click(object sender, EventArgs e)
    {
        SDTSample.ReadOnly = SDTSample.ReadOnly ^ true;
    }
    #endregion

    #region SingleDropDownList
    protected void SDDLChange(string value)
    {
        SDDLResult.ValueText = value;
    }
    protected void SDDLValueTextButton_Click(object sender, EventArgs e)
    {
        SDDLResult.ValueText = SDDLSample.ValueText;
    }
    protected void SDDLReadOnlyTextButton_Click(object sender, EventArgs e)
    {
        SDDLResult.ValueText = SDDLSample.ReadOnlyText;
    }
    protected void SDDLReadOnlyButton_Click(object sender, EventArgs e)
    {
        SDDLSample.ReadOnly = SDDLSample.ReadOnly ^ true;
    }
    #endregion

    #region SingleField
    protected void SFSampleChange(object sender, EventArgs e)
    {
        SFResult.ValueText = SFSample.ValueText;
    }
    protected void SFAccountChange(object sender, EventArgs e)
    {
        string tmpStr = SFSample.ValueText;
        SFResult.ValueText = tmpStr + "\n" + SFAccount.ValueText;
    }
    protected void SFValueTextButton_Click(object sender, EventArgs e)
    {
        SFResult.ValueText = SFSample.ValueText;
    }
    protected void SFCustomCSSButton_Click(object sender, EventArgs e)
    {
        if (SFSample.CustomCSS == "" && SFAccount.CustomCSS == "")
        {
            SFSample.CustomCSS = "FormHeadHead";
            SFAccount.CustomCSS = "FormHeadHead";
        }
        else
        {
            SFSample.CustomCSS = "";
            SFAccount.CustomCSS = "";
        }

    }
    protected void SFReadOnlyButton_Click(object sender, EventArgs e)
    {
        SFSample.ReadOnly = SFSample.ReadOnly ^ true;
        if (SFAccount.ReadOnly)
        {
            SFAccount.ReadOnly = false;
        }
        else
        {
            SFAccount.ReadOnly = true;
        }

    }
    protected void SFisAccountButton_Click(object sender, EventArgs e)
    {
        if (!SFAccount.isAccount)
        {
            SFAccount.isAccount = true;
            SFAccountDSCR.Text = "isAccount = true";
        }
        else
        {
            SFAccount.isAccount = false;
            SFAccountDSCR.Text = "isAccount = false";
        }
    }
    #endregion

    #region SingleOpenWindowField
    protected void SOWChangeWhere()
    {
        if (SOWTestBeforeButton.Checked)
        {
            SOWSample.whereClause = " id like '2%'";
        }
        else
        {
            SOWSample.whereClause = "";
        }
    }
    protected void SOWClick(string[,] values)
    {
        string str = "";
        if (values.GetLength(0) > 0)
        {
            for (int i = 0; i < values.GetLength(1); i++)
            {
                str += values[0, i] + "\t";
            }
        }
        SOWResult.ValueText = str;
    }
    protected void SOWPartialReadOnlyButton_Click(object sender, EventArgs e)
    {
        SOWSample.PartialReadOnly = SOWSample.PartialReadOnly ^ true;
    }
    protected void SOWHiddenTextButton_Click(object sender, EventArgs e)
    {
        SOWSample.HiddenText = SOWSample.HiddenText ^ true;
    }
    protected void SOWHiddenButtonButton_Click(object sender, EventArgs e)
    {
        SOWSample.HiddenButton = SOWSample.HiddenButton ^ true;
    }
    protected void SOWshowReadOnlyFieldButton_Click(object sender, EventArgs e)
    {
        SOWSample.showReadOnlyField = SOWSample.showReadOnlyField ^ true;
    }
    protected void SOWHiddenClearButtonButton_Click(object sender, EventArgs e)
    {
        SOWSample.HiddenClearButton = SOWSample.HiddenClearButton ^ true;
    }
    protected void SOWReadOnlyButton_Click(object sender, EventArgs e)
    {
        SOWSample.ReadOnly = SOWSample.ReadOnly ^ true;
    }
    protected void SOWGuidValueTextButton_Click(object sender, EventArgs e)
    {
        SOWResult.ValueText = SOWSample.GuidValueText;
    }
    protected void SOWValueTextButton_Click(object sender, EventArgs e)
    {
        SOWResult.ValueText = SOWSample.ValueText;
    }
    protected void SOWReadOnlyValueTextButton_Click(object sender, EventArgs e)
    {
        SOWResult.ValueText = SOWSample.ReadOnlyValueText;
    }
    #endregion
}
