﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Program_DSCGPFlowService_Public_Rollback : BaseWebUI.GeneralWebPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //底下這段是取得原表單畫面相關參數設定. 若要傳其它參數, 可仿照平台基本寫法, 
        //將資料存在以原本PageUniqueID命名的Session中. 系統會自動將原本畫面的PageUniqueID傳入
        string PGID = Request.QueryString["PGID"];
        string SMWDAAA001 = Request.QueryString["SMWDAAA001"];


    }
    protected void SendButton_Click(object sender, EventArgs e)
    {


        Response.Write("window.top.returnValue='YES';");
        Response.Write("window.top.close();");
    }
}