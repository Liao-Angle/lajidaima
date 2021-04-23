using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SmpProgram_Maintain_SPPMTest_Maintain :BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GlassButton1_Click(object sender, EventArgs e)
    {
        TextBox1.Text = Request.Form["TextBox1"].ToString();
        MessageBox("TextBox1值:" + TextBox1.Text + " SingleField1值:" + SingleField1.ValueText);
    }
}