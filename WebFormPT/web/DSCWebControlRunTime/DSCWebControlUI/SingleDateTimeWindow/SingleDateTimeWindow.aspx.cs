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

public partial class DSCWebControlUI_SingleDateTimeWindow : System.Web.UI.Page
{
    public int DateTimeMode
    {
        get
        {
            if (ViewState["DateTimeMode"] == null)
            {
                return 0;
            }
            else
            {
                return (int)ViewState["DateTimeMode"];
            }
        }
        set
        {
            ViewState["DateTimeMode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {                
        string tmpstr = "";
        try
        {
            if (!IsPostBack)
            {
                tmpstr = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["datetimemode"]);
                DateTimeMode = int.Parse(tmpstr);
                string currentTime = "";
                if (!Request.QueryString["datetimevalue"].Equals(""))
                {
                    try
                    {
                        tmpstr = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["datetimevalue"]);
                        DateTime dt = DateTime.Parse(tmpstr);
                        currentTime = com.dsc.kernal.utility.DateTimeUtility.convertDateTimeToString(dt);
                    }
                    catch
                    {
                        currentTime = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
                    }
                }
                else
                {
                    currentTime = com.dsc.kernal.utility.DateTimeUtility.getSystemTime2(null);
                }

                HH.Text = currentTime.Substring(11, 2);
                MM.Text = currentTime.Substring(14, 2);
                SS.Text = currentTime.Substring(17, 2);

                if (DateTimeMode == 0)
                {
                    HH.Text = "00";
                    MM.Text = "00";
                    SS.Text = "00";
                    HH.Enabled = false;
                    MM.Enabled = false;
                    SS.Enabled = false;
                    HU.Enabled = false;
                    HD.Enabled = false;
                    MU.Enabled = false;
                    MD.Enabled = false;
                    SU.Enabled = false;
                    SD.Enabled = false;
                }
                Calendar1.SelectedDate = DateTime.Parse(currentTime);
                Calendar1.TodaysDate = DateTime.Parse(currentTime);
            }
        }
        catch (Exception EX)
        {
            Response.Write("<script language=javascript>");
            Response.Write("window.setTimeout(\"alert('DSCWebControlUI_SingleDateTimeWindow:" + EX.Message + "。')\",100);");
            Response.Write("</script>");

        }
    }
    protected void HH_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int test=int.Parse(HH.Text);
            if ((test < 0) || (test > 23))
            {
                HH.Text = "12";
            }
        }
        catch
        {
            HH.Text = "12";
        }
    }
    protected void MM_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int test = int.Parse(MM.Text);
            if ((test < 0) || (test > 59))
            {
                MM.Text = "00";
            }
        }
        catch
        {
            MM.Text = "00";
        }
    }
    protected void SS_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int test = int.Parse(SS.Text);
            if ((test < 0) || (test > 59))
            {
                SS.Text = "00";
            }
        }
        catch
        {
            SS.Text = "00";
        }
    }
    protected void HU_Click(object sender, EventArgs e)
    {
        int test = int.Parse(HH.Text);
        test++;
        if (test > 23)
            test = 0;
        HH.Text = string.Format("{0:00}", test);
    }
    protected void HD_Click(object sender, EventArgs e)
    {
        int test = int.Parse(HH.Text);
        test--;
        if (test <0)
            test = 23;
        HH.Text = string.Format("{0:00}", test);
    }
    protected void MU_Click(object sender, EventArgs e)
    {
        int test = int.Parse(MM.Text);
        test++;
        if (test > 59)
            test = 0;
        MM.Text = string.Format("{0:00}", test);
    }
    protected void MD_Click(object sender, EventArgs e)
    {
        int test = int.Parse(MM.Text);
        test--;
        if (test <0)
            test = 59;
        MM.Text = string.Format("{0:00}", test);
    }
    protected void SU_Click(object sender, EventArgs e)
    {
        int test = int.Parse(SS.Text);
        test++;
        if (test > 59)
            test = 0;
        SS.Text = string.Format("{0:00}", test);
    }
    protected void SD_Click(object sender, EventArgs e)
    {
        int test = int.Parse(SS.Text);
        test--;
        if (test < 0)
            test = 59;
        SS.Text = string.Format("{0:00}", test);
    }

    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        string result = "";
        
        DateTime curD = Calendar1.SelectedDate;
        if (curD.Year == 1)
        {
            curD = DateTime.Now;
        }
        string dd = com.dsc.kernal.utility.DateTimeUtility.convertDateTimeToString(curD);
        dd = dd.Substring(0, 10);
        string tt = HH.Text + ":" + MM.Text + ":" + SS.Text;

        if (DateTimeMode == 0)
        {
            result = dd;
        }
        else if (DateTimeMode == 1)
        {
            result = dd + " " + tt;
        }
        else if (DateTimeMode == 2)
        {
            result = tt;
        }
        else
        {
            result = dd + " " + HH.Text + ":" + MM.Text;
        }

        string str = "";
        str += "<script language=javascript>";
        str += "window.returnValue='" + result + "';";
        str += "window.close();";
        str += "</script>";
        Response.Write(str);
    }
}
