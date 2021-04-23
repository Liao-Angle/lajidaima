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
using com.dsc.kernal.agent;
using com.dsc.kernal.databean;
using com.dsc.kernal.factory;
using com.dsc.kernal.global;
using com.dsc.kernal.utility;
using WebServerProject.flow.SMWG;

public partial class Program_DSCGPFlowService_Maintain_SMWG_Maintain : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        maintainIdentity = "SMWG";
        ApplicationID = "SYSTEM";
        ModuleID = "SMWAA";

        if (!IsPostBack)
        {
            if (!IsProcessEvent)
            {
                string[,] ids = new string[20, 2];
                for (int i = 0; i < 20; i++)
                {
                    string tagN = "SMWYAAA1" + string.Format("{0:00}", i + 1);
                    ids[i, 0] = tagN;
                    ids[i, 1] = tagN;
                }
                SMWGAAA002.setListItem(ids);

                string connectString = (string)Session["connectString"];
                string engineType = (string)Session["engineType"];

                IOFactory factory = new IOFactory();
                AbstractEngine engine = factory.getEngine(engineType, connectString);

                SMWGAgent agent = new SMWGAgent();
                agent.engine = engine;
                agent.query("");
                DataObjectSet dos = agent.defaultData;

                string[,] orderby = new string[,]{
                    {"SMWGAAA002",DataObjectConstants.ASC}
                };
                dos.sort(orderby);

                string sql = "select * from SMWKAAA";
                DataSet ds = engine.getDataSet(sql, "TEMP");

                engine.close();

                ListTable.HiddenField = new string[] { "SMWGAAA001" };
                ListTable.dataSource = dos;
                ListTable.updateTable();

                if (ds.Tables[0].Rows[0]["SMWKAAA002"].ToString().Equals("Y"))
                {
                    SMWKAAA002.Checked = true;
                }
                else
                {
                    SMWKAAA002.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA003"].ToString().Equals("Y"))
                {
                    SMWKAAA003.Checked = true;
                }
                else
                {
                    SMWKAAA003.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA004"].ToString().Equals("Y"))
                {
                    SMWKAAA004.Checked = true;
                }
                else
                {
                    SMWKAAA004.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA005"].ToString().Equals("Y"))
                {
                    SMWKAAA005.Checked = true;
                }
                else
                {
                    SMWKAAA005.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA006"].ToString().Equals("Y"))
                {
                    SMWKAAA006.Checked = true;
                }
                else
                {
                    SMWKAAA006.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA007"].ToString().Equals("Y"))
                {
                    SMWKAAA007.Checked = true;
                }
                else
                {
                    SMWKAAA007.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA008"].ToString().Equals("Y"))
                {
                    SMWKAAA008.Checked = true;
                }
                else
                {
                    SMWKAAA008.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA009"].ToString().Equals("Y"))
                {
                    SMWKAAA009.Checked = true;
                }
                else
                {
                    SMWKAAA009.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA010"].ToString().Equals("Y"))
                {
                    SMWKAAA010.Checked = true;
                }
                else
                {
                    SMWKAAA010.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA011"].ToString().Equals("Y"))
                {
                    SMWKAAA011.Checked = true;
                }
                else
                {
                    SMWKAAA011.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA012"].ToString().Equals("Y"))
                {
                    SMWKAAA012.Checked = true;
                }
                else
                {
                    SMWKAAA012.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA013"].ToString().Equals("Y"))
                {
                    SMWKAAA013.Checked = true;
                }
                else
                {
                    SMWKAAA013.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA014"].ToString().Equals("Y"))
                {
                    SMWKAAA014.Checked = true;
                }
                else
                {
                    SMWKAAA014.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA015"].ToString().Equals("Y"))
                {
                    SMWKAAA015.Checked = true;
                }
                else
                {
                    SMWKAAA015.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA016"].ToString().Equals("Y"))
                {
                    SMWKAAA016.Checked = true;
                }
                else
                {
                    SMWKAAA016.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA017"].ToString().Equals("Y"))
                {
                    SMWKAAA017.Checked = true;
                }
                else
                {
                    SMWKAAA017.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA018"].ToString().Equals("Y"))
                {
                    SMWKAAA018.Checked = true;
                }
                else
                {
                    SMWKAAA018.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA019"].ToString().Equals("Y"))
                {
                    SMWKAAA019.Checked = true;
                }
                else
                {
                    SMWKAAA019.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA020"].ToString().Equals("Y"))
                {
                    SMWKAAA020.Checked = true;
                }
                else
                {
                    SMWKAAA020.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["SMWKAAA021"].ToString().Equals("Y"))
                {
                    SMWKAAA021.Checked = true;
                }
                else
                {
                    SMWKAAA021.Checked = false;
                }

                
                DataObjectSet dos2 = new DataObjectSet();
                dos2.setAssemblyName("WebServerProject");
                dos2.setChildClassString("WebServerProject.flow.SMWY.SMWYAAA");
                dos2.setTableName("SMWYAAA");
                WebServerProject.flow.SMWY.SMWYAAA aa = (WebServerProject.flow.SMWY.SMWYAAA)dos2.create();

                string[] orList = ds.Tables[0].Rows[0]["SMWKAAA200"].ToString().Split(new char[] { ';' });
                ids = new string[orList.Length, 2];
                for (int i = 0; i < orList.Length; i++)
                {
                    string disName = orList[i];
                    for (int j = 0; j < aa.dataField.Length; j++)
                    {
                        if (orList[i].Equals(aa.dataField[j]))
                        {
                            disName = aa.displayName[j];
                            break;
                        }
                    }
                    ids[i, 0] = orList[i];
                    ids[i, 1] = disName;
                }
                FieldOrderList.setListItem(ids);
            }
        }
    }
    protected bool ListTable_SaveRowData(DataObject objects, bool isNew)
    {

        SMWGAAA aa = (SMWGAAA)objects;
        if (isNew)
        {
            aa.SMWGAAA001 = IDProcessor.getID("");
        }
        aa.SMWGAAA002 = SMWGAAA002.ValueText;
        aa.SMWGAAA003 = SMWGAAA003.ValueText;
        aa.SMWGAAA004 = SMWGAAA004.ValueText;
        return true;
    }
    protected void ListTable_ShowRowData(DataObject objects)
    {
        SMWGAAA aa = (SMWGAAA)objects;
        SMWGAAA002.ValueText = aa.SMWGAAA002;
        SMWGAAA003.ValueText = aa.SMWGAAA003;
        SMWGAAA004.ValueText = aa.SMWGAAA004;
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        string connectString = (string)Session["connectString"];
        string engineType = (string)Session["engineType"];

        IOFactory factory = new IOFactory();
        AbstractEngine engine = factory.getEngine(engineType, connectString);

        engine.startTransaction(IsolationLevel.ReadCommitted);

        SMWGAgent agent = new SMWGAgent();
        agent.engine = engine;
        agent.defaultData = ListTable.dataSource;

        if (!agent.update())
        {
            engine.rollback();
            engine.close();
            throw new Exception(engine.errorString.Replace("\n", "\\n"));
        }
        string sql = getSQL();
        if (!engine.executeSQL(sql))
        {
            engine.rollback();
            engine.close();
            throw new Exception(engine.errorString.Replace("\n", "\\n"));
        }
        engine.commit();
        engine.close();
        MessageBox(com.dsc.locale.LocaleString.getSystemMessageString("program_dscgpflowservice_maintain_smwg_maintain_aspx.language.ini", "message", "QueryError", "儲存成功"));
    }
    protected string getSQL()
    {
        string sql = "update SMWKAAA set SMWKAAA001=SMWKAAA001";
        if (SMWKAAA002.Checked)
        {
            sql += ", SMWKAAA002='Y'";
        }
        else
        {
            sql += ", SMWKAAA002='N'";
        }
        if (SMWKAAA003.Checked)
        {
            sql += ", SMWKAAA003='Y'";
        }
        else
        {
            sql += ", SMWKAAA003='N'";
        }
        if (SMWKAAA004.Checked)
        {
            sql += ", SMWKAAA004='Y'";
        }
        else
        {
            sql += ", SMWKAAA004='N'";
        }
        if (SMWKAAA005.Checked)
        {
            sql += ", SMWKAAA005='Y'";
        }
        else
        {
            sql += ", SMWKAAA005='N'";
        }
        if (SMWKAAA006.Checked)
        {
            sql += ", SMWKAAA006='Y'";
        }
        else
        {
            sql += ", SMWKAAA006='N'";
        }
        if (SMWKAAA007.Checked)
        {
            sql += ", SMWKAAA007='Y'";
        }
        else
        {
            sql += ", SMWKAAA007='N'";
        }
        if (SMWKAAA008.Checked)
        {
            sql += ", SMWKAAA008='Y'";
        }
        else
        {
            sql += ", SMWKAAA008='N'";
        }
        if (SMWKAAA009.Checked)
        {
            sql += ", SMWKAAA009='Y'";
        }
        else
        {
            sql += ", SMWKAAA009='N'";
        }
        if (SMWKAAA010.Checked)
        {
            sql += ", SMWKAAA010='Y'";
        }
        else
        {
            sql += ", SMWKAAA010='N'";
        }
        if (SMWKAAA011.Checked)
        {
            sql += ", SMWKAAA011='Y'";
        }
        else
        {
            sql += ", SMWKAAA011='N'";
        }
        if (SMWKAAA012.Checked)
        {
            sql += ", SMWKAAA012='Y'";
        }
        else
        {
            sql += ", SMWKAAA012='N'";
        }
        if (SMWKAAA013.Checked)
        {
            sql += ", SMWKAAA013='Y'";
        }
        else
        {
            sql += ", SMWKAAA013='N'";
        }
        if (SMWKAAA014.Checked)
        {
            sql += ", SMWKAAA014='Y'";
        }
        else
        {
            sql += ", SMWKAAA014='N'";
        }
        if (SMWKAAA015.Checked)
        {
            sql += ", SMWKAAA015='Y'";
        }
        else
        {
            sql += ", SMWKAAA015='N'";
        }
        if (SMWKAAA016.Checked)
        {
            sql += ", SMWKAAA016='Y'";
        }
        else
        {
            sql += ", SMWKAAA016='N'";
        }
        if (SMWKAAA017.Checked)
        {
            sql += ", SMWKAAA017='Y'";
        }
        else
        {
            sql += ", SMWKAAA017='N'";
        }
        if (SMWKAAA018.Checked)
        {
            sql += ", SMWKAAA018='Y'";
        }
        else
        {
            sql += ", SMWKAAA018='N'";
        }
        if (SMWKAAA019.Checked)
        {
            sql += ", SMWKAAA019='Y'";
        }
        else
        {
            sql += ", SMWKAAA019='N'";
        }
        if (SMWKAAA020.Checked)
        {
            sql += ", SMWKAAA020='Y'";
        }
        else
        {
            sql += ", SMWKAAA020='N'";
        }
        if (SMWKAAA021.Checked)
        {
            sql += ", SMWKAAA021='Y'";
        }
        else
        {
            sql += ", SMWKAAA021='N'";
        }
        string[,] ids = FieldOrderList.getListItem();
        string vls = "";
        for (int i = 0; i < ids.GetLength(0); i++)
        {
            vls += ids[i, 0] + ";";
        }
        vls = vls.Substring(0, vls.Length - 1);
        sql += ", SMWKAAA200='" + vls + "'";

        return sql;
    }
    protected void OrderMoveUp_Click(object sender, EventArgs e)
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

        if (getIndex == (FieldOrderList.getListItem().GetLength(0) - 1)) return;

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
