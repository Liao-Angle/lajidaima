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
using BarCodes;
using BarCodes.generator;
using System.Drawing;

public partial class DSCWebControlRunTime_DSCWebControlUI_BarCode_getBarCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string BarCodeType = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["BarCodeType"]);
        string codes = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["codes"].ToUpper());

        BarCodeUtility bu = new BarCodeUtility(BarCodeType, "");
        Bitmap bt = bu.getLabelBitmap(new string[] { codes });
        if (bt != null)
        {
            Response.Clear();
            bt.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }

    }
}
