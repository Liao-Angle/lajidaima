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
using System.Drawing;
using DSCWebControl;

public partial class DSCWebControlRunTime_DSCWebControlUI_BarGraph_getGraph : Page
{
    Bitmap PaintArea=null;
    private Color BackGroundColor;
    private Unit Height;
    private Unit Width;
    private string[,] m_Data;
    private int mode;
    private Color BarColor;
    private Color[] pieColor;

    private bool IsShowValue = true;
    private decimal BarOffset = 0;
    private int barwidth = 20; //設定的長條圖寬度
    private int barxoffset = 60; //直腸條圖與左邊界距離
    private int barxend = 20; //直腸條圖與又邊界距離
    private int barspace = 10; //長條圖間隔
    private int baryoffset = 30; //直腸條圖與上邊界距離
    private int labeloffset = 100; //長條圖標籤寬度
    private int yCount = 10; //值的Grid個樹
    private bool m_isshowgrid = true;

    private string m_valueField = "";
    private string m_tagField = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string fUID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["PageUniqueID"]);
        string pID = com.dsc.kernal.utility.Utility.CheckCrossSiteScripting(Request.QueryString["ClientID"]);

        BackGroundColor = (Color)getSession(fUID, pID, "BackGroundColor");
        Height = (Unit)getSession(fUID, pID,  "Height");
        Height = new Unit(Height.Value - 10);
        Width = (Unit)getSession(fUID, pID,  "Width");
        Width = new Unit(Width.Value - 10);
        m_Data = (string[,])getSession(fUID, pID,  "m_Data");
        mode = (int)getSession(fUID, pID,  "mode");
        BarColor = (Color)getSession(fUID, pID,  "BarColor");
        labeloffset = (int)getSession(fUID, pID,  "LabelOffset");
        baryoffset = (int)getSession(fUID, pID,  "BarYOffset");
        barxoffset = (int)getSession(fUID, pID,  "BarXOffset");
        barwidth = (int)getSession(fUID, pID,  "BarWidth");
        barxend = (int)getSession(fUID, pID,  "BarXEnd");
        barspace = (int)getSession(fUID, pID,  "BarSpace");
        yCount = (int)getSession(fUID, pID,  "YCount");
        m_isshowgrid = (bool)getSession(fUID, pID,  "IsShowGrid");
        m_valueField = (string)getSession(fUID, pID,  "ValueField");
        m_tagField = (string)getSession(fUID, pID,  "TagField");
        BarOffset = (decimal)getSession(fUID, pID,  "BarOffset");
        IsShowValue = (bool)getSession(fUID, pID,  "IsShowValue");
        pieColor = (Color[])getSession(fUID, pID,  "PieColor");

        getGraph();

        if (PaintArea != null)
        {
            Response.Clear();
            PaintArea.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }
    }
    /// <summary>
    /// 取得指定PageUniqueID的Session
    /// </summary>
    /// <param name="pageUniqueID">指定的PageUniqueID</param>
    /// <param name="sessionName">Session名稱</param>
    /// <returns>Session值</returns>
    protected object getSession(string pageUniqueID, string ClientID, string sessionName)
    {
        string ptag = pageUniqueID + "_" +ClientID+"_"+ sessionName;
        return Session[ptag];
    }
    private void getGraph()
    {
        int totalx = (int)Width.Value;
        int totaly = (int)Height.Value;

        decimal totalvalue = 0;
        decimal maxvalue = 0;
        for (int i = 0; i < m_Data.GetLength(0); i++)
        {
            if (decimal.Parse(m_Data[i, 1]) > maxvalue)
            {
                maxvalue = decimal.Parse(m_Data[i, 1]);
            }
            totalvalue += decimal.Parse(m_Data[i, 1]);
        }
        if (mode == BarGraph.V_BAR)
        {

            //調整寬度
            int tempx = barxoffset + (m_Data.GetLength(0) + 1) * (barwidth + barspace) + barxend;
            if (tempx > totalx)
            {
                totalx = tempx;
                //PaintArea.Height = (int)Height.Value;
                //PaintArea.Width = totalx;
                PaintArea = new Bitmap(totalx, (int)Height.Value);
            }
            else
            {
                //PaintArea.Height = (int)Height.Value;
                //PaintArea.Width = (int)Width.Value;
                PaintArea = new Bitmap((int)Width.Value, (int)Height.Value);
            }
            Graphics g = Graphics.FromImage(PaintArea);
            g.Clear(BackGroundColor);

            
            int originx = barxoffset;
            int originy = totaly - labeloffset;

            Pen p = new Pen(Color.Black);
            Pen barp = new Pen(BarColor);

            g.DrawLine(p, new Point(originx, baryoffset), new Point(originx, originy));
            g.DrawLine(p, new Point(originx, originy), new Point(totalx - barxend, originy));


            decimal incrs = (totaly - labeloffset - baryoffset) / (maxvalue - BarOffset);

            int realspace = (int)((totalx - barxoffset - barxend - m_Data.GetLength(0) * barwidth) / (m_Data.GetLength(0) + 1));
            if (realspace < barspace)
            {
                realspace = barspace;
            }

            int inity = (int)((maxvalue - BarOffset) / yCount);
            for (int i = 0; i < yCount; i++)
            {
                int temph = (int)(incrs * inity * i);
                string tag = String.Format("{0}", BarOffset + inity * i);
                SizeF ss = g.MeasureString(tag, new Font("新細明體", 10));
                g.DrawString(tag, new Font("新細明體", 10), p.Brush, originx - ss.Width - 5, (float)originy - temph - 5);
                if (m_isshowgrid)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawLine(p, new Point(originx, originy - temph), new Point(totalx - barxend, originy - temph));
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                g.DrawLine(p, new Point(originx, originy - temph), new Point(originx + 10, originy - temph));
            }

            for (int i = 0; i < m_Data.GetLength(0); i++)
            {
                int px = originx + realspace + (realspace + barwidth) * i;
                int temph = (int)(incrs * (decimal.Parse(m_Data[i, 1]) - BarOffset));
                int py = originy - temph;
                g.FillRectangle(barp.Brush, px, py, barwidth, temph);
                g.DrawRectangle(p, px, py, barwidth, temph);

                string tag = transformTag(m_Data[i, 0]);

                g.DrawString(tag, new Font("新細明體", 10), p.Brush, (float)px, (float)originy + 10);
                if (IsShowValue)
                {
                    g.DrawString(m_Data[i, 1], new Font("新細明體", 10), p.Brush, (float)px, (float)py - 15);
                }
            }

            p.Dispose();
            barp.Dispose();
        }
        else if (mode == BarGraph.H_BAR)
        {

            //調整高度
            int tempy = baryoffset + (m_Data.GetLength(0) + 1) * (barwidth + barspace) + labeloffset;
            if (tempy > totaly)
            {
                totaly = tempy;
                //PaintArea.Height = totaly;
                //PaintArea.Width = (int)Width.Value;
                PaintArea = new Bitmap((int)Width.Value, totaly);
            }
            else
            {
                //PaintArea.Height = (int)Height.Value;
                //PaintArea.Width = (int)Width.Value;
                PaintArea = new Bitmap((int)Width.Value, (int)Height.Value);
            }
            Graphics g = Graphics.FromImage(PaintArea);
            g.Clear(BackGroundColor);


            int originx = barxoffset;
            int originy = totaly - labeloffset;

            Pen p = new Pen(Color.Black);
            Pen barp = new Pen(BarColor);

            g.DrawLine(p, new Point(originx, baryoffset), new Point(originx, originy));
            g.DrawLine(p, new Point(originx, originy), new Point(totalx - barxend, originy));


            decimal incrs = (totalx - barxoffset - barxend) / (maxvalue - BarOffset);

            int realspace = (int)((totaly - baryoffset - labeloffset - m_Data.GetLength(0) * barwidth) / (m_Data.GetLength(0) + 1));
            if (realspace < barspace)
            {
                realspace = barspace;
            }

            int initx = (int)((maxvalue - BarOffset) / yCount);
            for (int i = 0; i < yCount; i++)
            {
                int temph = (int)(incrs * initx * i);
                string tag = String.Format("{0}", BarOffset + initx * i);
                SizeF ss = g.MeasureString(tag, new Font("新細明體", 10));
                //g.DrawString(tag, new Font("新細明體", 10), p.Brush, originx - ss.Width - 5, (float)originy - temph - 5);
                g.DrawString(tag, new Font("新細明體", 10), p.Brush, originx + temph - 5, originy + 10);
                if (m_isshowgrid)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawLine(p, new Point(originx + temph, originy), new Point(originx + temph, baryoffset));
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                }
                g.DrawLine(p, new Point(originx + temph, originy), new Point(originx + temph + 10, originy));
            }

            for (int i = 0; i < m_Data.GetLength(0); i++)
            {
                int py = originy - realspace - (realspace + barwidth) * i - barwidth;
                int temph = (int)(incrs * (decimal.Parse(m_Data[i, 1]) - BarOffset));
                int px = originx;
                g.FillRectangle(barp.Brush, px, py, temph, barwidth);
                g.DrawRectangle(p, px, py, temph, barwidth);

                //string tag = transformTag(m_Data[i, 0]);
                string tag = m_Data[i, 0];
                SizeF ss = g.MeasureString(tag, new Font("新細明體", 10));

                g.DrawString(tag, new Font("新細明體", 10), p.Brush, (float)px - ss.Width - 5, (float)py);
                if (IsShowValue)
                {
                    g.DrawString(m_Data[i, 1], new Font("新細明體", 10), p.Brush, (float)px + temph + 5, (float)py);
                }
            }

            p.Dispose();
            barp.Dispose();
        }
        else if (mode == BarGraph.PIE)
        {
            //PaintArea.Width = (int)Width.Value;
            //PaintArea.Height = (int)Height.Value;
            PaintArea = new Bitmap((int)Width.Value, (int)Height.Value);
            Graphics g = Graphics.FromImage(PaintArea);
            g.Clear(BackGroundColor);


            //取得左上座標以及長寬
            int dirs = 0;

            int boundwidth = 0;
            int startx = 0;
            int starty = 0;


            if (totalx > totaly)
            {
                boundwidth = totaly - 20;
                startx = (int)((totalx - boundwidth) / 2);
                starty = 10;
                dirs = 0;
            }
            else
            {
                boundwidth = totalx - 20;
                startx = 10;
                starty = (int)((totaly - boundwidth) / 2);

                dirs = 1;
            }
            int centerx = startx + (int)(boundwidth / 2);
            int centery = starty + (int)(boundwidth / 2);
            int radius = (int)(boundwidth / 2);

            Pen p = new Pen(Color.Black);
            Pen pr = new Pen(Color.Black);

            int startdegree = 0;
            int startColor = 0;

            for (int i = 0; i < m_Data.GetLength(0); i++)
            {
                int degreeoffset = 0;
                int enddegree = 0;

                if (i == m_Data.GetLength(0) - 1)
                {
                    degreeoffset = 360 - startdegree;
                }
                else
                {
                    degreeoffset = (int)(360 * decimal.Parse(m_Data[i, 1]) / totalvalue);
                }
                pr.Color = pieColor[startColor];

                g.FillPie(pr.Brush, startx, starty, boundwidth, boundwidth, startdegree, degreeoffset);
                g.DrawPie(p, startx, starty, boundwidth, boundwidth, startdegree, degreeoffset);


                startdegree += degreeoffset;
                startColor++;
                if (startColor >= pieColor.GetLength(0))
                {
                    startColor = 0;
                }
            }

            //畫標記
            //取得扇型中心點
            startdegree = 0;

            for (int i = 0; i < m_Data.GetLength(0); i++)
            {
                int degreeoffset = 0;
                int enddegree = 0;

                if (i == m_Data.GetLength(0) - 1)
                {
                    degreeoffset = 360 - startdegree;
                }
                else
                {
                    degreeoffset = (int)(360 * decimal.Parse(m_Data[i, 1]) / totalvalue);
                }


                string tag = m_Data[i, 0] + ": " + m_Data[i, 1];
                SizeF ss = g.MeasureString(tag, new Font("新細明體", 10));

                int cdegree = startdegree + (int)(degreeoffset / 2);
                int area = 0;//象限
                int cx = 0;
                int cy = 0;
                int ex = 0;
                int ey = 0;
                if ((cdegree >= 0) && (cdegree <= 90))
                {
                    cx = centerx + (int)(radius / 2 * System.Math.Cos(2 * System.Math.PI * cdegree / 360));
                    cy = centery + (int)(radius / 2 * System.Math.Sin(2 * System.Math.PI * cdegree / 360));
                    if (dirs == 0)
                    {
                        ex = (int)(PaintArea.Width - ss.Width - 5);
                        ey = cy;
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, PaintArea.Width - ss.Width, ey);
                    }
                    else
                    {
                        ex = cx;
                        ey = (int)(PaintArea.Height - ss.Height - 5);
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, ex, PaintArea.Height - ss.Height);
                    }
                }
                else if ((cdegree > 90) && (cdegree <= 180))
                {
                    cx = centerx - (int)(radius / 2 * System.Math.Cos(2 * System.Math.PI * (180 - cdegree) / 360));
                    cy = centery + (int)(radius / 2 * System.Math.Sin(2 * System.Math.PI * (180 - cdegree) / 360));
                    if (dirs == 0)
                    {
                        ex = (int)(ss.Width + 5);
                        ey = cy;
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, 0, ey);
                    }
                    else
                    {
                        ex = cx;
                        ey = (int)(PaintArea.Height - ss.Height - 5);
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, ex, PaintArea.Height - ss.Height);
                    }
                }
                else if ((cdegree > 180) && (cdegree <= 270))
                {
                    cx = centerx - (int)(radius / 2 * System.Math.Cos(2 * System.Math.PI * (cdegree - 180) / 360));
                    cy = centery - (int)(radius / 2 * System.Math.Sin(2 * System.Math.PI * (cdegree - 180) / 360));
                    if (dirs == 0)
                    {
                        ex = (int)(ss.Width + 5);
                        ey = cy;
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, 0, ey);
                    }
                    else
                    {
                        ex = cx;
                        ey = 0;
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, ex, 0);
                    }
                }
                else
                {
                    cx = centerx + (int)(radius / 2 * System.Math.Cos(2 * System.Math.PI * (360 - cdegree) / 360));
                    cy = centery - (int)(radius / 2 * System.Math.Sin(2 * System.Math.PI * (360 - cdegree) / 360));
                    if (dirs == 0)
                    {
                        ex = (int)(PaintArea.Width - ss.Width - 5);
                        ey = cy;
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, PaintArea.Width - ss.Width, ey);
                    }
                    else
                    {
                        ex = cx;
                        ey = 0;
                        g.DrawString(tag, new Font("新細明體", 10), p.Brush, ex, 0);
                    }
                }

                g.DrawLine(p, cx, cy, ex, ey);


                startdegree += degreeoffset;
            }

            p.Dispose();
            pr.Dispose();
        }
    }
    private string transformTag(string tag)
    {
        string ret = "";
        for (int i = 0; i < tag.Length; i++)
        {
            ret += tag.Substring(i, 1) + "\n";
        }
        if (ret.Length > 0)
        {
            ret = ret.Substring(0, ret.Length - 1);
        }
        return ret;
    }

}
