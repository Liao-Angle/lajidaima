using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using com.dsc.kernal.factory;
using com.dsc.kernal.databean;

public partial class Program_System_Maintain_DBAccess_Maintain : BaseWebUI.GeneralWebPage
{
    private int fieldCount = 40;

    protected override void OnInit(EventArgs e)
    {
        base.ApplicationID = "SYSTEM";
        base.ModuleID = "SMVAA";
        base.maintainIdentity = "DBAccess";
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                string sql = "select name from sysobjects where xtype = 'U' and name <> 'dtproperties' order by name";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                string[,] ids = new string[ds.Tables[0].Rows.Count, 2];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ids[i, 0] = ds.Tables[0].Rows[i][0].ToString();
                    ids[i, 1] = ids[i, 0];
                }
                TableList.setListItem(ids);
                engine.close();
            }
        }
    }
    private void query(string where)
    {
        try
        {
            string connectString = (string)Session["connectString"];
            string engineType = (string)Session["engineType"];

            IOFactory factory = new IOFactory();
            AbstractEngine engine = factory.getEngine(engineType, connectString);

            string sql = where;

            DataSet ds = engine.getDataSet(sql, "TEMP");

            setSession("DataSet", ds);

            engine.close();

            if (ds.Tables.Count == 0)
            {
                writeLog(new Exception(com.dsc.locale.LocaleString.getSystemMessageString("program_system_maintain_dbaccess_maintain_aspx.language.ini", "message", "QueryError", "使用者執行SQL:") + System.Environment.NewLine + where));
                ShowRow_Click(null, null);
                return;
            }
            string qstr = "select ";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                qstr += ds.Tables[0].Columns[i].Caption + ", ";
            }
            qstr = qstr.Substring(0, qstr.Length - 2);
            qstr += " from " + TableList.ValueText;

            DataObjectSet dos = new DataObjectSet();
            string schema = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            schema += "<DataObject assemblyName=\"WebServerProject\" childClassString=\"WebServerProject.DBAccess\" tableName=\"" + TableList.ValueText + "\">";
            schema += "<queryStr>" + qstr + "</queryStr>";
            schema += "  <isCheckTimeStamp>true</isCheckTimeStamp>";
            schema += "  <fieldDefinition>";

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                schema += "    <field dataField=\"" + ds.Tables[0].Columns[i].Caption + "\" typeField=\"STRING\" lengthField=\"50000\" defaultValue=\"\" displayName=\"" + ds.Tables[0].Columns[i].Caption + "\" showName=\"\"/>";
            }

            schema += "  </fieldDefinition>";
            schema += "  <identityField>";
            schema += "    <field dataField=\"" + ds.Tables[0].Columns[0].Caption + "\"/>";
            schema += "  </identityField>";
            schema += "  <keyField>";
            schema += "    <field dataField=\"" + ds.Tables[0].Columns[0].Caption + "\"/>";
            schema += "  </keyField>";
            schema += "  <allowEmptyField>";
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                schema += "    <field dataField=\"" + ds.Tables[0].Columns[i].Caption + "\"/>";
            }

            schema += "  </allowEmptyField>";
            schema += "  <nonUpdateField>";
            schema += "  </nonUpdateField>";
            schema += "</DataObject>";
            dos.dataObjectSchema = schema;
            
            dos.isNameLess = true;

            bool res = true;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataObject ddo = dos.create();
                if (!dos.errorString.Equals(""))
                {
                    throw new Exception(dos.errorString);
                }

                Hashtable hs = new Hashtable();
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    hs.Add(ds.Tables[0].Columns[j].Caption, ds.Tables[0].Rows[i][j].ToString());
                }
                ddo.setHashtable(hs);
                bool res2 = dos.addDraft(ddo);
                if (!res2)
                {
                    res = false;
                }
            }

            if (res)
            {
                //closeAllField();
                //showField(ds);

                DataViewer.HiddenField = new string[0];
                DataViewer.dataSource = dos;
                DataViewer.updateTable();
            }
            else
            {
                MessageBox(dos.errorString);
            }

        }
        catch (Exception tte)
        {
            Response.Write("alert('" + tte.Message + "');");
            writeLog(tte);

        }
    }
    protected void ShowRow_Click(object sender, EventArgs e)
    {
        string sql="select * from " + TableList.ValueText;
        query(sql);
    }

    protected void Query_Click(object sender, EventArgs e)
    {
        if (SQL.ValueText.Trim().Equals(""))
        {
            ShowRow_Click(sender, e);
        }
        else
        {
            query(SQL.ValueText);
        }
    }

}
