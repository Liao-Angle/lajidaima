using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class SmpProgram_Maintain_LeaveJob_ModPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbPassword.Focus();
        }
    }

    protected void btMod_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["empno"] != null)
        {
            string empno = Request.QueryString["empno"].ToString();
            if (!empno.Equals(""))
            {
                if (tbPassword.Text.Equals(tbPassword2.Text) && !tbPassword.Text.Equals(""))
                {
                    string sqltxt = "update dbo.SmpHRAppointmentAuthority set password='" + tbPassword.Text + "' where empno='" + empno + "'";
                    using (SqlConnection con = new SqlConnection("USER=sa;PWD=SMPecp1;SERVER=10.3.11.83;DATABASE=WebFormPT"))
                    {
                        con.Open();
                        SqlCommand com = new SqlCommand(sqltxt, con);
                        if (com.ExecuteNonQuery() > 0)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "alert('密碼修改成功！');", true);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "alert('請確認是否是經副級以上人員！');", true);
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "alert('兩次密碼輸入不一致或新密碼不能為空，請重新輸入！');", true);
                    tbPassword2.Text = "";
                    tbPassword2.Focus();

                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "提示", "alert('無效的傳參數！請聯繫MIS人員。');", true);
            }
        }
    }
}