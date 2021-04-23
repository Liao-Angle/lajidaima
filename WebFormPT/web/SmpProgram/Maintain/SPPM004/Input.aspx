<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_Maintain_SPPM004_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>考核表評核作業</title>
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
        .style2
        {
            height: 18px;
        }
        .style3
        {
            height: 20px;
        }
    </style>
    <script type="text/javascript">
        function SelfScore_xx() {
            var myID = new Array();
            var inputs = document.getElementsByTagName('input');
            for (var i = 1; i < inputs.length; i++) {
                var input = inputs[i];
                var str = input.id.toString().substring(0, input.id.lastIndexOf('_'));
                if (input.id.indexOf(str) >= 0 && input.id.indexOf("DSCTabAssessment_GridViewEvaluationDetail") >= 0) {

                    var Flag = 0;
                    if (myID.length == 0) {
                        myID.push(str);
                    }
                    else {
                        for (var j = 0; j < myID.length; j++) {
                            if (myID[j] == str) {
                                Flag = 1;
                            }
                        }
                        if (Flag == 0) {
                            myID.push(str);
                        }
                    }
                }
            }
            var total = 0;
            for (var k = 0; k < myID.length; k++) {
                var lbl4 = myID[k] + "_tbItemWeight";
                var weight = document.getElementById(lbl4).value;
                var ItemNum = document.getElementById(myID[k] + "_tbItemNum").value;
                var MaxFraction = document.getElementById(myID[k] + "_tbMaxFraction").value;
                var tbSelfScore = document.getElementById(myID[k] + "_tbSelfScore").value;
                if (tbSelfScore != "" && MaxFraction != "" && ItemNum != "" && weight != "") {
                    total += parseFloat((weight / (ItemNum * MaxFraction)) * tbSelfScore);
                }
            }
            //alert(total.toFixed(2).toString());
            document.getElementById("S_DSCTabAssessment_SelfScore_W").value = total.toFixed(2).toString();
        }
        function firstScore_xx() {
            var myID = new Array();
            var inputs = document.getElementsByTagName('input');
            for (var i = 1; i < inputs.length; i++) {
                var input = inputs[i];
                var str = input.id.toString().substring(0, input.id.lastIndexOf('_'));
                if (input.id.indexOf(str) >= 0 && input.id.indexOf("DSCTabAssessment_GridViewEvaluationDetail") >= 0) {

                    var Flag = 0;
                    if (myID.length == 0) {
                        myID.push(str);
                    }
                    else {
                        for (var j = 0; j < myID.length; j++) {
                            if (myID[j] == str) {
                                Flag = 1;
                            }
                        }
                        if (Flag == 0) {
                            myID.push(str);
                        }
                    }
                }
            }
            var total = 0;
            for (var k = 0; k < myID.length; k++) {
                var lbl4 = myID[k] + "_tbItemWeight"; 
                var weight = document.getElementById(lbl4).value;
                var ItemNum = document.getElementById(myID[k] + "_tbItemNum").value;
                var MaxFraction =document.getElementById(myID[k] + "_tbMaxFraction").value;
                var tbFirstScore =document.getElementById(myID[k] + "_tbFirstScore").value;
                if (tbFirstScore != "" && MaxFraction!="" && ItemNum!="" && weight!="") {
                    total += parseFloat((weight / (ItemNum * MaxFraction)) * tbFirstScore);
                }
             }
           //alert(total.toFixed(2).toString());
            document.getElementById("S_DSCTabAssessment_FirstScore_W").value = total.toFixed(2).toString();
        }
        function SecondScore_xx() {
            var myID = new Array();
            var inputs = document.getElementsByTagName('input');
            for (var i = 1; i < inputs.length; i++) {
                var input = inputs[i];
                var str = input.id.toString().substring(0, input.id.lastIndexOf('_'));
                if (input.id.indexOf(str) >= 0 && input.id.indexOf("DSCTabAssessment_GridViewEvaluationDetail") >= 0) {

                    var Flag = 0;
                    if (myID.length == 0) {
                        myID.push(str);
                    }
                    else {
                        for (var j = 0; j < myID.length; j++) {
                            if (myID[j] == str) {
                                Flag = 1;
                            }
                        }
                        if (Flag == 0) {
                            myID.push(str);
                        }
                    }
                }
            }
            var total = 0;
            for (var k = 0; k < myID.length; k++) {
                var lbl4 = myID[k] + "_tbItemWeight";
                var weight = document.getElementById(lbl4).value;
                var ItemNum = document.getElementById(myID[k] + "_tbItemNum").value;
                var MaxFraction = document.getElementById(myID[k] + "_tbMaxFraction").value;
                var tbSecondScore = document.getElementById(myID[k] + "_tbSecondScore").value;
                if (tbSecondScore != "" && MaxFraction != "" && ItemNum != "" && weight != "") {
                    total += parseFloat((weight / (ItemNum * MaxFraction)) * tbSecondScore);
                }
            }
            //alert(total.toFixed(2).toString());
            document.getElementById("S_DSCTabAssessment_SecondScore_W").value = total.toFixed(2).toString();
        }
    </script>
</head>
<body>
    <table width="100%">
        <tr><td>
            <cc1:GlassButton ID="GbSendAssessmentScore" runat="server" Height="16px" 
                        Text="送出成績" Width="100px" ConfirmText="送出成績後將不能再異動資料, 請確認?" 
                onclick="GbSendAssessmentScore_Click" Visible="False" />
             <cc1:GlassButton ID="computer" runat="server" Height="16px" Text="匯總" 
                onclick="computer_Click" Width="42px" /> 
                <cc1:SingleField ID="total" runat="server" Width="80px" Height="16px" ReadOnly="True" />  
                </td>
        </tr>
    </table>
    <form id="form1" runat="server">
    <cc1:DSCTabControl ID="DSCTabAssessment" runat="server" Height="120px" Width="960px" PageColor="White">
    <TabPages>
        <cc1:DSCTabPage runat="server" Title="評核作業維護" Enabled="True" ImageURL="" Hidden="False" ID="TabAssessMaintain">
        <TabBody>
            <table width="780px" border=0 cellspacing=0 cellpadding=1 > 
                <tr><td><cc1:DSCLabel ID="LblAssessmentName" runat="server" Width="90px" 
                        Text="考評名稱" TextAlign="2" /></td>
                        <td colspan=5>
                            <cc1:SingleField ID="AssessmentName" runat="server" Width="314px" 
                                ReadOnly="True" /></td>
                        <td><cc1:DSCLabel ID="LblStage" runat="server" Width="90px" Text="評核階段" 
                                TextAlign="2" /></td>
                        <td><cc1:SingleDropDownList ID="Stage" runat="server" Width="100px" ReadOnly="True" /></td>
                </tr>
                <tr><td class="style1"><cc1:DSCLabel ID="LblEmpName" runat="server" Width="90px" Text="姓名" 
                        TextAlign="2" /></td>
                    <td class="style1"><cc1:SingleField ID="empName" runat="server" Width="80px" ReadOnly="True" /></td>
                    <td class="style1"><cc1:DSCLabel ID="LblDeptName" runat="server" Width="80px" Text="部門" 
                            TextAlign="2" Height="16px" /></td>
                    <td class="style1"><cc1:SingleField ID="deptName" runat="server" Width="120px" ReadOnly="True" /></td>
                    <td class="style1"><cc1:DSCLabel ID="LblAssessmentDays" runat="server" Width="80px" Text="評核天數" TextAlign="2" /></td>
                    <td class="style1"><cc1:SingleField ID="AssessmentDays" runat="server" Width="40px" 
                            ReadOnly="True" /></td>
                    <td class="style1"><cc1:DSCLabel ID="LblStatus" runat="server" Width="90px" Text="評核狀態" 
                            TextAlign="2" /></td>
                    <td class="style1"><cc1:SingleDropDownList ID="Status" runat="server" Width="100px" 
                            ReadOnly="True" /></td>
                </tr>
                <tr><td class="style2">
                    <cc1:DSCLabel ID="LblSelfComments" runat="server" Width="90px" Text="自評意見" 
                        TextAlign="2" Display="False" /></td>
                    <td colspan=5 class="style2">
                        <cc1:SingleField ID="SelfComments" runat="server" Width="450px" 
                            MultiLine="True" Display="False" /></td>
                    <td class="style2"><cc1:DSCLabel ID="LblSelfScore" runat="server" Width="90px" Text="自評總分" 
                            TextAlign="2" Height="16px" Display="False" /></td>
                    <td class="style2"><cc1:SingleField ID="SelfScore" runat="server" Width="80px" ReadOnly="True" 
                            Display="False" /><cc1:SingleField ID="SelfScore_W" runat="server"  Width="80px" ReadOnly="True" Display="False" />
                    </td>
                </tr>
                <tr><td class="style3">
                    <cc1:DSCLabel ID="LblFirstComments" runat="server" Width="90px" 
                        Text="一階評核意見" TextAlign="2" Display="False" /></td>
                    <td colspan=5 class="style3">
                        <cc1:SingleField ID="FirstComments" runat="server" Width="450px" 
                            MultiLine="True" Display="False" /></td>
                    <td class="style3"><cc1:DSCLabel ID="LblFirstScore" runat="server" Width="90px" Text="一階評核總分" 
                            TextAlign="2" Display="False" /></td>
                    <td class="style3"><cc1:SingleField ID="FirstScore" runat="server" Width="80px" ReadOnly="True" 
                            Display="False" />
                        <cc1:SingleField ID="FirstScore_W" runat="server" Width="80px" ReadOnly="True" 
                            Display="False"/>
                    </td>
                </tr>
                <tr><td>
                    <cc1:DSCLabel ID="LblSecondComments" runat="server" Width="90px" 
                        Text="二階評核意見" TextAlign="2" Display="False" /></td>
                    <td colspan=5>
                        <cc1:SingleField ID="SecondComments" runat="server" Width="450px" 
                            MultiLine="True" Display="False" /></td>
                    <td><cc1:DSCLabel ID="LblSecondScore" runat="server" Width="90px" Text="二階評核總分" 
                            TextAlign="2" Display="False" /></td>
                    <td><cc1:SingleField ID="SecondScore" runat="server" Width="80px" ReadOnly="True" 
                            Display="False" />
                        <cc1:SingleField ID="SecondScore_W" runat="server" Width="80px" ReadOnly="True" 
                            Display="False" />
                    </td>
                </tr>
            </table>
            <table style="width: 960px">
                <tr><td width="100%" height="100%">
                        <cc1:DataList ID="AssessmentScoreDetailList" runat="server" Height="295px" 
                            Width="100%" showExcel="True" ReadOnly="False" NoAdd="True" 
                            NoDelete="True" IsHideSelectAllButton="True" IsShowCheckBox="False" />
                        <br />
                    </td>
                </tr>
            </table>

            <table style="width: 960px">
                <tr><td>
                        <asp:GridView ID="GridViewEvaluationDetail" runat="server" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
                            style="font-size: x-small" AllowSorting="False">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="ItemNo" HeaderText="編號" ReadOnly="True" 
                                    SortExpression="ItemNo" />
                                <asp:BoundField DataField="ItemName" HeaderText="項目" ReadOnly="True" 
                                    SortExpression="ItemName" />
                                <asp:BoundField DataField="Content" HeaderText="考核內容" ReadOnly="True" 
                                    SortExpression="Content" />
                                 <asp:BoundField DataField="FractionExp" HeaderText="分數說明" ReadOnly="True" 
                                    SortExpression="FractionExp" />
                                <asp:BoundField DataField="MinFraction" HeaderText="最低分數" 
                                    SortExpression="MinFraction" Visible="False" />
                                <asp:BoundField DataField="MaxFraction" HeaderText="最高分數" ReadOnly="True" 
                                    SortExpression="MaxFraction" Visible="False" />
                                <asp:TemplateField HeaderText="自評分數" SortExpression="SelfScore">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="SelfScore" runat="server" Text='<%# Bind("SelfScore") %>'></asp:TextBox>
                                        <asp:TextBox ID="ItemNum" runat="server" Text='<%# Bind("ItemNum") %>'></asp:TextBox>
                                        <asp:TextBox ID="ItemWeight" runat="server" Text='<%# Bind("ItemWeight") %>'></asp:TextBox>
                                        <asp:TextBox ID="MaxFraction" runat="server" Text='<%# Bind("MaxFraction") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbSelfScore" runat="server" Height="20px" Text='<%# Bind("SelfScore") %>' Width="60px"></asp:TextBox>
                                        <asp:TextBox ID="tbItemNum" runat="server" ReadOnly="true" Width="0px"  Text='<%# Bind("ItemNum") %>'></asp:TextBox>
                                        <asp:TextBox ID="tbItemWeight" runat="server" ReadOnly="true" Width="0px"  Text='<%# Bind("ItemWeight") %>'></asp:TextBox>
                                        <asp:TextBox ID="tbMaxFraction" runat="server" ReadOnly="true" Width="0px" Text='<%# Bind("MaxFraction") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="一階分數" SortExpression="FirstScore">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="FirstScore" runat="server" Text='<%# Bind("FirstScore") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbFirstScore" runat="server" Height="20px" 
                                            Text='<%# Bind("FirstScore") %>' Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="二階分數" SortExpression="SecondScore">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="SecondScore" runat="server" Text='<%# Bind("SecondScore") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbSecondScore" runat="server" Height="20px" 
                                            Text='<%# Bind("SecondScore") %>' Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="自評意見" SortExpression="SelfComments">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="SelfComments" runat="server" Text='<%# Bind("SelfComments") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbSelfComments" runat="server" MaxLength="250" 
                                            Text='<%# Bind("SelfComments") %>' TextMode="MultiLine" Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="一階評核意見" SortExpression="FirstComments">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="FirstComments" runat="server" Text='<%# Bind("FirstComments") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbFirstComments" runat="server" MaxLength="250" 
                                            Text='<%# Bind("FirstComments") %>' TextMode="MultiLine" Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="二階評核意見" SortExpression="SecondComments">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="SecondComments" runat="server" Text='<%# Bind("SecondComments") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbSecondComments" runat="server" MaxLength="250" 
                                            Text='<%# Bind("SecondComments") %>' TextMode="MultiLine" Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="識別碼" SortExpression="GUID" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="GUID" runat="server" Text='<%# Bind("GUID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="tbGUID" runat="server" MaxLength="250" 
                                            Text='<%# Bind("GUID") %>' TextMode="SingleLine" Width="100px" 
                                            Visible="False"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </TabBody>
        </cc1:DSCTabPage>
        <cc1:DSCTabPage ID="TabAttachment" runat="server" Enabled="True" Hidden="False" ImageURL="" Title="附件">
        <TabBody>
            <table width="780px">
                <tr><td><cc1:OpenFileUpload ID="FileUpload" runat="server" onaddoutline="Atta_AddOutline" />
                        <cc1:GlassButton ID="ButtonUpload" runat="server" Height="20px" Text="上傳檔案" Width="70px" 
                            onclick="Upload_Click"/>
                    </td>
                </tr>
                <tr><td>
                        <cc1:DataList ID="AttachmentList" runat="server" height="300px" width="780px" 
                            onbeforedeletedata="Atta_BeforeDeleteData" NoAdd="True" />
                    </td>
                </tr>
            </table>
        </TabBody>
        </cc1:DSCTabPage>
    </TabPages>
    </cc1:DSCTabControl>
    
    
    </form>
</body>
</html>
