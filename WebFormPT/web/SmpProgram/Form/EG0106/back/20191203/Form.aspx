<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Program_SCQ_Form_EG0106_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>未命名頁面</title>
    <style type="text/css">
        .style1
        {
            width: 189px;
        }
        .style2
        {
            width: 183px;
        }
        .style3
        {
            width: 176px;
        }
        .style4
        {
            width: 136px;
        }
        .style5
        {
            width: 166px;
        }
    </style>
    </head>
<body leftmargin=0 topmargin=0>
    <form id="form1" runat="server">
    <div>

         <cc1:SingleField id="SheetNo" runat="server" Display="False"></cc1:SingleField>
     <cc1:SingleField id="Subject" runat="server" Display="False"></cc1:SingleField>
     <table style="margin-left:4px; width: 874px;" border="0" cellspacing="0" 
             cellpadding="1">
     <tr>
      <td align="center" height="30" style="width: 800px">
            <font style="font-family: 標楷體; font-size: large;"><b>門禁權限申請單</b></font></td>
     </tr>
     </table>
        <table style="margin-left:4px; width: 877px;" border=0 cellspacing=0 
            cellpadding=1 class=BasicFormHeadBorder>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBPrivilege" runat="server" Text="門禁權限"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                 <table width="100%">
                    <tr>
                        <td class=BasicFormHeadDetail>                    
                            <cc1:DSCCheckBox ID="DSCCheckBox1" runat="server" Text="主機房進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>                    
                            <cc1:DSCCheckBox ID="DSCCheckBox2" runat="server" Text="主機房出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox3" runat="server" Text="一號樓梯進大辦公室"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox4" runat="server" Text="一號樓梯出大辦公室"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox5" runat="server" Text="大廳玻璃門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox6" runat="server" Text="大廳玻璃門出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox7" runat="server" Text="大辦公室玻璃門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox8" runat="server" Text="大辦公室玻璃門出"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox9" runat="server" Text="大廳機房進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox10" runat="server" Text="大廳機房出"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox11" runat="server" Text="大廳到一樓西側大辦公室進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox12" runat="server" Text="大廳到一樓西側大辦公室出"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox13" runat="server" Text="HR到大廳進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox14" runat="server" Text="HR到大廳出"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox15" runat="server" Text="D100D100 500半成品倉進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox16" runat="server" Text="監控室進"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox17" runat="server" Text="D100 215 倉進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox18" runat="server" Text="MIS C機房進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox19" runat="server" Text="ME  設備間進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox20" runat="server" Text="SMT鋼板房"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox21" runat="server" Text="貴重物品倉出"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox22" runat="server" Text="貴重物品倉進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox23" runat="server" Text="L100 電子材料倉進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox24" runat="server" Text="MIS  D機房進"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox25" runat="server" Text="1號電梯1樓進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox26" runat="server" Text="1號電梯2樓進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox27" runat="server" Text="2號電梯1樓進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox28" runat="server" Text="2號電梯2樓進"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox29" runat="server" Text="SMT物料房進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox30" runat="server" Text="SMT物料房出"></cc1:DSCCheckBox>
                        
                        </td>
			            <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox31" runat="server" Text="關務資料室"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox32" runat="server" Text="財務資料室"></cc1:DSCCheckBox>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox33" runat="server" Text="TE設備間進"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox34" runat="server" Text="SMT  ICT治具室"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox35" runat="server" Text="量測室"></cc1:DSCCheckBox>
                        
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox36" runat="server" Text="實驗室進"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox37" runat="server" Text="三廠移印室"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox38" runat="server" Text="三廠MIS主機房"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox39" runat="server" Text="三廠MIS維修間"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox40" runat="server" Text="三廠TE設備間"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox41" runat="server" Text="SMT備品房"></cc1:DSCCheckBox>
                        </td>
			<td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox42" runat="server" Text="一廠一層四號門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox43" runat="server" Text="一廠一層四號門出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox44" runat="server" Text="一廠二層TE設備間"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox45" runat="server" Text="IQC Rohs測試區"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox46" runat="server" Text="一廠二樓500倉"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox47" runat="server" Text="一廠進三廠天橋處"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox48" runat="server" Text="一廠碼頭進出"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox49" runat="server" Text="品保樣品室"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox50" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox51" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox52" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox53" runat="server" Text="三廠東側機房1"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox54" runat="server" Text="三廠東側機房2"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox55" runat="server" Text="三厂分类间出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox56" runat="server" Text="三厂分类间进"></cc1:DSCCheckBox>
                        </td>
                    </tr>

                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox57" runat="server" Text="三廠二樓3號門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox58" runat="server" Text="三廠二樓3號門出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox59" runat="server" Text="三廠二樓受電室"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox60" runat="server" Text="三廠海關監管倉"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox61" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox62" runat="server" Text="三廠電梯2"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox63" runat="server" Text="三廠一樓3號門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox64" runat="server" Text="三廠一樓3號門出"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox65" runat="server" Text="二廠樓頂進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox66" runat="server" Text="二廠樓頂出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox67" runat="server" Text="二廠大辦公室進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox68" runat="server" Text="貽百量測室進"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox69" runat="server" Text="停車場進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox70" runat="server" Text="停車場出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox71" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox72" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox73" runat="server" Text="三廠一樓倉庫東卷閘門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox74" runat="server" Text="三廠一樓倉庫東卷閘門出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox75" runat="server" Text="三廠一樓實驗室進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox76" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox77" runat="server" Text="TE設備間"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox78" runat="server" Text="TE燒校保"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox79" runat="server" Text="二樓電子倉旁進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox80" runat="server" Text="二樓電子倉旁出"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox81" runat="server" Text="一廠5號門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox82" runat="server" Text="一廠5號門出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox83" runat="server" Text="IE治具室"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox84" runat="server" Text="一廠二樓ME設備間"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox85" runat="server" Text="停車刷卡南大門進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox86" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox87" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox88" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox89" runat="server" Text="停車刷卡南大門出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox90" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox91" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox92" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox93" runat="server" Text="IE維修間"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox94" runat="server" Text="三廠電梯1"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox95" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox96" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox97" runat="server" Text="鐳射間1"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox98" runat="server" Text="鐳射間2"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox99" runat="server" Text="鐳射間3"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox100" runat="server" Text="鐳射間3"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox101" runat="server" Text="三廠二樓東側至機房進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox102" runat="server" Text="三廠二樓東側至機房出"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox103" runat="server" Text="三廠大廳至二樓進"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox104" runat="server" Text="三廠大廳至二樓出"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox105" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox106" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox107" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox108" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox109" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox110" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox111" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox112" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox113" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox114" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox115" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox116" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:DSCCheckBox ID="DSCCheckBox117" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox118" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox119" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                        <td class=BasicFormHeadDetail>   
                            <cc1:DSCCheckBox ID="DSCCheckBox120" runat="server" Text="待啟用"></cc1:DSCCheckBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>



  <tr>
            <td align=right class=BasicFormHeadHead valign="top" rowspan=2 width=80px>
                <cc1:DSCLabel ID="LBApplication" runat="server" Text="申請人資料"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>                
                <Panel ID=EditPanel runat=server>
                <table width="100%">
                    <tr>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead1" runat="server" Text="申請人" Width="70px"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead3" runat="server" Text="部門" Width="71px"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead4" runat="server" Text="職稱"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead5" runat="server" Text="申請事由"></cc1:DSCLabel>
                        </td>
                        <td class=BasicFormHeadHead>
                            <cc1:DSCLabel ID="LBHead6" runat="server" Text="物理卡號"></cc1:DSCLabel>
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleOpenWindowField ID="EmpNo" runat="server"
                                showReadOnlyField="True" guidField="EmpNo" keyField="EmpNo" serialNum="001" 
                                tableName="hrusers" 
                                OnSingleOpenWindowButtonClick="EmpNo_SingleOpenWindowButtonClick" 
                                valueIndex="1" idIndex="0" Width="104px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="PartNo" runat="server" Width="100px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="DtName" runat="server" Width="60px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="Reason" runat="server" Width="100px" />
                        </td>
                        <td class=BasicFormHeadDetail>
                            <cc1:SingleField ID="Note2" runat="server" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class=BasicFormHeadDetail colspan=5>
                            <cc1:GlassButton ID="AddData" runat="server" BackColor="White"
                                onclick="AddData_Click" ImageUrl="~\Images\lock.png"
                                 />
                        </td>
                    </tr>
                </table>
                </Panel>
            </td>
        </tr>
      
        <tr>
             <td class=BasicFormHeadDetail>
                <cc1:DataList ID="ApplicationList" runat="server" Height="200px" Width="96%" 
                    isShowAll="True" NoModify="True" 
                    showTotalRowCount="True" NoAllDelete="True" NoAdd="True" 
                    IsHideSelectAllButton="True"  showExcel="True"
                    IsOpenWinUse="True" NoDelete="True" />

                    <span style="margin-top:0px; padding-top:0px"> 
                 <cc1:FileUpload ID="FileUpload1" runat="server" NoDelete="True" 
                             onaddoutline="FileUpload1_AddOutline" Width="18px" 
                     BorderStyle="None" ViewStateMode="Disabled" Height="200px" /></span>  
            </td>
        </tr>
        <tr>
            <td valign=top align=right class=BasicFormHeadHead>
                <cc1:DSCLabel ID="LBNote" runat="server" Text="備註說明"></cc1:DSCLabel>
            </td>
            <td class=BasicFormHeadDetail>
                <cc1:SingleField ID="Note" runat="server" Width="100%" Height="64px" MultiLine="True" />
            </td>
        </tr>
           
        <tr runat="server" id="showzg" visible="false">
            <td class="BasicFormHeadHead" style="height: 17px">
          <cc1:DSCLabel ID="DSCLabel2" runat="server" Text="請選擇簽核主管" Width="150px" Height="20px"></cc1:DSCLabel>
             </td>
              <td class="BasicFormHeadHead" style="height: 17px">
                  <cc1:SingleDropDownList ID="sqzszg" runat="server" />
             </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
