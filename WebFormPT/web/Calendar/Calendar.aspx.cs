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
using com.dsc.kernal.utility;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;

public partial class Calendar_Calendar : System.Web.UI.Page
{
    protected string DTS = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Calendars.SelectedDate = DateTime.Now;
            queryDay();
        }
        DTS = DateTimeUtility.convertDateTimeToString(Calendars.SelectedDate).Substring(0, 10);
    }

    private void queryDay()
    {
        AbstractEngine engine = null;
        string processing = "<table border=0 cellspacing=2 cellpadding=0 width=100% class='ItemStyle'>";
        string yet = "<table border=0 cellspacing=2 cellpadding=0 width=100%  class='ItemStyle'>";
        string notyet = "<table border=0 cellspacing=2 cellpadding=0 width=100%  class='ItemStyle'>";

        try
        {
            string today = DateTimeUtility.convertDateTimeToString(Calendars.SelectedDate).Substring(0, 10);
            string curtime = DateTimeUtility.getSystemTime2(null).Substring(11, 5);
            string nowday = DateTimeUtility.getSystemTime2(null).Substring(0, 10);

            TodayLiteral.Text = today;

            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            engine = factory.getEngine(engineType, connectString);
            string sql = "";
            //取得行事曆類型
            sql = "select SMVFAAA001, SMVFAAA003, SMVFAAA005, SMVFAAA006, SMVFAAA007 from SMVFAAA";
            DataSet fds = engine.getDataSet(sql, "TEMP");

            //取得本日所有行事曆項目
            sql = "select SMVGAAA001, SMVGAAA003, SMVGAAA007, SMVGAAA008, SMVGAAA009, SMVGAAA010 from SMVGAAA where SMVGAAA002='" + Utility.filter((string)Session["UserGUID"]) + "' and SMVGAAA004='N' and SMVGAAA006='" + Utility.filter(today) + "'";
            DataSet tds = engine.getDataSet(sql, "TEMP");

            //取得所有循環行事曆項目

            engine.close();

            for (int i = 0; i < tds.Tables[0].Rows.Count; i++)
            {
                //判斷是屬於進行中, 已完成, 還是未進行
                string stime = tds.Tables[0].Rows[i][2].ToString();
                string etime = tds.Tables[0].Rows[i][3].ToString();
                int mode = 0;

                if (today.Equals(nowday))
                {
                    if ((stime.CompareTo(curtime) <= 0) && (etime.CompareTo(curtime) >= 0))
                    {
                        mode = 0; //進行中
                    }
                    else if (curtime.CompareTo(stime) < 0)
                    {
                        mode = 1; //未進行
                    }
                    else
                    {
                        mode = 2; //已進行
                    }
                }
                else if (today.CompareTo(nowday) < 0)
                {
                    mode = 2;
                }
                else
                {
                    mode = 1;
                }
                if (mode == 0)
                {
                    addItem(tds.Tables[0].Rows[i], fds, ref processing);
                }
                else if (mode == 1)
                {
                    addItem(tds.Tables[0].Rows[i], fds, ref notyet);
                }
                else
                {
                    addItem(tds.Tables[0].Rows[i], fds, ref yet);
                }
            }

            processing += "</table>";
            yet += "</table>";
            notyet += "</table>";

            fds.Dispose();
            fds = null;
            tds.Dispose();
            tds = null;
            GC.Collect();
        }
        catch
        {
            try
            {
                engine.close();
            }
            catch { };
        }
        //最後寫入畫面
        ProcessLiteral.Text = processing;
        NotYetLiteral.Text = notyet;
        YetLiteral.Text = yet;
        
    }
    private void addItem(DataRow dr, DataSet fds, ref string data)
    {
        DataRow types=null;
        for(int i=0;i<fds.Tables[0].Rows.Count;i++){
            if(dr["SMVGAAA003"].ToString().Equals(fds.Tables[0].Rows[i][0].ToString())){
                types=fds.Tables[0].Rows[i];
                break;
            }
        }
        if(types==null){
            return;
        }
        data += "<tr>";
        data += "<td width=12px bgcolor=rgb(" + types["SMVFAAA005"].ToString() + "," + types["SMVFAAA006"].ToString() + "," + types["SMVFAAA007"].ToString() + ")>&nbsp;&nbsp;&nbsp;&nbsp;</td>";
        //data += "<td></td>"; //循環
        //data+="<td></td>";//衝突
        data += "<td width=100% nowrap=true>";
        data += "<span style=\"cursor:pointer\" onclick='clickItem(\"" + dr["SMVGAAA001"].ToString() + "\");'>[" + types["SMVFAAA003"].ToString() + "] (" + dr["SMVGAAA007"].ToString() + "-" + dr["SMVGAAA008"].ToString() + ") ";
        data += dr["SMVGAAA009"].ToString()+"</span>";
        data+= "</td>";
        data += "<td width=17px><span style='cursor:pointer' onclick='deleteItem(\"" + dr["SMVGAAA001"].ToString() + "\");'><img src='Images/imgDelete.gif' alt='" + com.dsc.locale.LocaleString.getMainFrameLocaleString("Calendar.aspx.language.ini", "global", "string011", "刪除此項目") + "'></span></td>";
        data += "</tr>";
    }
    protected void Calendars_SelectionChanged(object sender, EventArgs e)
    {
        queryDay();
    }
}
