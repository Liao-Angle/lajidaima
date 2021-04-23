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

public partial class Program_DSCAuditService_Maintain_SMXE_Detail : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMXE";
        ApplicationID = "SYSTEM";
        ModuleID = "SMXAA";
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                DataSet ds = (DataSet)Session["SMXEAAADS"];
                StaticBar.TagField = "FLD";
                StaticBar.ValueField = "CCS";
                StaticBar.mode = int.Parse((string)Session["SMXEAAATYPE"]);
                StaticBar.setData(ds);

                string html = "<table width=666px border=0 cellspacing=0 id='FormHeadTable' cellpadding=2 style={border-left-style:solid;border-top-style:solid;border-width:1px} class=FormHeadBorder>";
                html += "<tr>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_detail_aspx.language.ini", "message", "str1", "項次") + "</td>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_detail_aspx.language.ini", "message", "str2", "類別") + "</td>";
                html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionHead align=right>" + com.dsc.locale.LocaleString.getSystemMessageString("program_dscauditservice_maintain_smxe_detail_aspx.language.ini", "message", "str3", "次數") + "</td>";
                html += "</tr>";

                for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                {
                    html += "<tr>";
                    html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + string.Format("{0}",i+1) + "</td>";
                    html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail>" + ds.Tables[0].Rows[i][1].ToString() + "</td>";
                    html += "<td style={border-style:solid;border-width:1px;border-top-style:none;border-left-style:none} class=OpinionDetail align=right>" + ds.Tables[0].Rows[i][0].ToString() + "</td>";
                    html += "</tr>";

                }
                html += "</table>";

                ListData.Text = html;
            }
        }
    }
}
