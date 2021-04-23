<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input.aspx.cs" Inherits="SmpProgram_maintain_SPTS005_Input" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>
<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">-->

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>開課記錄</title>
    <style type="text/css">
        .style1
        {
            height: 25px;
        }
        .style2
        {
            height: 18px;
        }
        .style3
        {
            height: 22px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 865px">
        <legend>開課記錄</legend>       
        <table style="margin-left:4px; width: 860px; height: 189px;" border=0 cellspacing=0 cellpadding=1 >
            <tr><td colspan="8" class="style2"> 
                <asp:Label ID="Label1" runat="server" Text="＊請先輸入公司別/開課年度/課程名稱/上課日期!" 
                    Font-Size="10pt" ForeColor="Red"></asp:Label>
                </td>
            </tr>         
            <tr><td align="right">
                <cc1:DSCLabel ID="LblReocrdSource" runat="server" Text="登錄來源" Width="76px" 
                IsNecessary="True" TextAlign="2" Height="16px" /></td>            
                <td><cc1:SingleDropDownList ID="RecordSource" runat="server" Width="113px" 
                        ReadOnly="True" /></td>
                <td align="right">
                    <cc1:DSCLabel ID="LblResourceNo" runat="server" IsNecessary="False" Text="登錄單號" 
                        TextAlign="2" Width="79px" style="margin-left: 0px" /></td>
	            <td><cc1:SingleField ID="RecordNo" runat="server"  Width="113px" /></td>
                <td align="right">
                     <cc1:DSCLabel ID="LblCourseSource" runat="server" Text="課程來源" TextAlign="2" 
                         Width="75px" IsNecessary="True" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="SchSource" runat="server" Width="90px" /> </td>  
                <td align="right" >
                      <cc1:DSCLabel ID="LblSchNo" runat="server" IsNecessary="False" Text="計劃代號" 
                        TextAlign="2" Width="79px" style="margin-left: 0px" /></td>
	            <td><cc1:SingleField ID="SchNo" runat="server"  Width="113px" /></td> 
            </tr>
            <tr><td align="right" class="style1">
                    <cc1:DSCLabel ID="LblCompanyCode" runat="server" Text="公司別" Width="70px" 
                        TextAlign="2" IsNecessary="True" /></td>
	            <td class="style1">
                    <cc1:SingleDropDownList ID="CompanyCode" 
                        runat="server" Width="113px" /> </td>
                <td align="right" class="style1">
                    <cc1:DSCLabel ID="LblCourseYear" runat="server" IsNecessary="True" Text="開課年度" 
                        TextAlign="2" width="70px" /></td>
	            <td class="style1"><cc1:SingleField ID="CourseYear" 
                        runat="server" Width="113px" /></td>
                 <td align="right" class="style1">
                   <cc1:DSCLabel ID="LblCourseName" runat="server" Text="課程名稱" Width="70px" 
                         TextAlign="2" IsNecessary="True" /> </td>
	            <td colspan="3" class="style1">
                    <cc1:SingleOpenWindowField ID="SchDetailGUID" runat="server" Width="379px" 
                            showReadOnlyField="True" guidField="GUID" keyField="SubjectNo" serialNum="001" 
                            tableName="TSSchForm" FixReadOnlyValueTextWidth="250px" 
                        FixValueTextWidth="95px" 
                        onbeforeclickbutton="SchDetailGUID_BeforeClickButton" 
                         onsingleopenwindowbuttonclick="SchDetailGUID_SingleOpenWindowButtonClick" 
                        IgnoreCase="True" dialogWidth="600" idIndex="3" valueIndex="4" /></td>             
            </tr>
            <tr><td align="right">
                   <cc1:DSCLabel ID="LblBriefIntro" runat="server" Text="課程簡介" TextAlign="2" 
                       Width="70px" />  </td>
                <td colspan="7"> <cc1:SingleField ID="BriefIntro" runat="server" Width="680px" /> </td>                 
            </tr>
            <tr>
                <td align="right">
                   <cc1:DSCLabel ID="LblCourseType" runat="server" Text="課程類別" Width="70px" 
                        TextAlign="2" IsNecessary="True" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="SubjectType" runat="server" Width="90px" /> </td> 
                <td align="right">
                   <cc1:DSCLabel ID="LblInOut" runat="server" Text="內外訓" TextAlign="2" 
                       Width="66px" IsNecessary="True" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="InOut" runat="server" Width="90px" /> </td> 
                <td align="right">                
                   <cc1:DSCLabel ID="LblDept" runat="server" Text="開課單位" TextAlign="2" 
                       Width="70px" IsNecessary="True" /> </td>
	            <td colspan="3">
                    <cc1:SingleOpenWindowField ID="DeptGUID" runat="server" Width="375px" 
                            showReadOnlyField="True" guidField="OID" keyField="id" serialNum="001" 
                            tableName="OrgUnit" FixReadOnlyValueTextWidth="250px" 
                        FixValueTextWidth="95px" IgnoreCase="True" /> </td>                           
            </tr>                   
            <tr>  
                <td align="right">
                   <cc1:DSCLabel ID="LblDateS" runat="server" Text="上課日期" TextAlign="2" 
                       Width="83px" IsNecessary="True" /> </td>
	            <td>
                    <cc1:SingleDateTimeField ID="StartDate" runat="server" Width="116px" />   </td>  
                <td align="right">
                     <cc1:DSCLabel ID="LblHours" runat="server" Text="上課時數" TextAlign="2" 
                         Width="70px" IsNecessary="True" /> </td>
	            <td>
                    <cc1:SingleField ID="Hours" runat="server" Width="90px" Fractor="1" 
                        ontextchanged="Hours_TextChanged" />  </td>  
                <td align="right">
                   <cc1:DSCLabel ID="LblLecturer" runat="server" Text="講師" TextAlign="2" 
                       Width="70px" IsNecessary="True" />  </td>
	            <td colspan="3"> 
                    <cc1:SingleOpenWindowField ID="LecturerGUID" runat="server" Width="388px" 
                            showReadOnlyField="True" guidField="GUID" keyField="GUID" serialNum="001" 
                            tableName="TSLecturer" FixReadOnlyValueTextWidth="250px" 
                        FixValueTextWidth="95px" 
                        onbeforeclickbutton="LecturerGUID_BeforeClickButton" IgnoreCase="True" /></td>     
            </tr>
            <tr>
                <td align="right">
                   <cc1:DSCLabel ID="LblWay" runat="server" Text="教授方式" TextAlign="2" 
                        Width="70px" IsNecessary="True" /> </td>
	            <td>
                    <cc1:SingleDropDownList ID="Way" runat="server" Width="90px" />  </td>
                <td align="right">
                     <cc1:DSCLabel ID="LblTTQS" runat="server" Text="TTQS課程" TextAlign="2" 
                         Width="84px" IsNecessary="True"  /></td>
                <td> 
                    <cc1:SingleDropDownList ID="TTQS" runat="server" Width="90px" /> </td>
                <td align="right">
                     <cc1:DSCLabel ID="LblCloseDate" runat="server" Text="結案日期" Width="70px" 
                         TextAlign="2" />  </td>
	            <td>
                    <cc1:SingleDateTimeField ID="CloseDate" runat="server" Width="100px"                       
                        ondatetimeclick="CloseDate_DateTimeClick" /></td>                                  
                <td align="right">
                    <cc1:DSCLabel ID="LBLStatus" runat="server" Text="課程狀態" Width="64px" 
                        IsNecessary="True" TextAlign="2" /></td>
                <td>
                    <cc1:SingleDropDownList ID="Status" runat="server" 
                        Width="90px" ReadOnly="False" /></td>            
               
            </tr> 
            <tr><td align="right">
                   <cc1:DSCLabel ID="LblAssessmentI" runat="server" Text="內訓評核方式" Width="94px" 
                       TextAlign="2" />  </td>
	            <td colspan="7">                                      
                    <cc1:DSCCheckBox ID="CbWrittenTest" runat="server" Text="筆試" Width="49px" />
                    <cc1:SingleField ID="WrittenTest" runat="server" Width="60px" Display="false" />
                    <cc1:DSCCheckBox ID="CbImplement" runat="server" Text="實作" Width="52px" />
                    <cc1:SingleField ID="Implement" runat="server" Width="60px" Display="false" />
                    <cc1:DSCCheckBox ID="CbSatisfaction" runat="server" Text="課程滿意度調查" Width="119px" />
                    <cc1:SingleField ID="Satisfaction" runat="server" Width="60px" Display="false" /> 
                    <cc1:DSCCheckBox ID="CbInOther" runat="server" Text="其他" Width="50px" 
                        onclick="CbInOther_Click" />
                    <cc1:SingleField ID="InOther" runat="server" Width="60px" Display="false" />
                    <cc1:SingleField ID="InOtherDesc" runat="server" Width="125px" />   </td>                       
           </tr>
           <tr><td align="right">
                <cc1:DSCLabel ID="LblAssessmentO" runat="server" Text="外訓評核方式" Width="94px" 
                       TextAlign="2" />  </td>
	            <td colspan="7" >                       
                    <cc1:DSCCheckBox ID="CbPresentation" runat="server" Text="發表開課" Width="81px" />
                    <cc1:SingleField ID="Presentation" runat="server" Width="60px" Display="false" />
                    <cc1:DSCCheckBox ID="CbCertificate" runat="server" Text="證書" Width="55px" />
                    <cc1:SingleField ID="Certificate" runat="server" Width="60px" Display="false" />
                    <cc1:DSCCheckBox ID="CbReport" runat="server" Text="受訓心得報告" Width="110px" />
                    <cc1:SingleField ID="Report" runat="server" Width="60px" Display="false" />
                    <cc1:DSCCheckBox ID="CbOJT" runat="server" Text="OJT(工作應用計畫書)" Width="155px" />
                    <cc1:SingleField ID="OJT" runat="server" Width="60px" Display="false" />
                    <cc1:DSCCheckBox ID="CbOutOther" runat="server" Text="其他" Width="50px" 
                        onclick="CbOutOther_Click" />
                    <cc1:SingleField ID="OutOther" runat="server" Width="60px" Display="false" />
                    <cc1:SingleField ID="OutOtherDesc" runat="server" Width="125px" />   </td>          
            </tr>             
            <tr>
                <td align="right" class="style3">
                   <cc1:DSCLabel ID="LblRemark" runat="server" Text="備註" Width="70px" 
                        TextAlign="2" />  </td>
	            <td class="style3" colspan="7">
                    <cc1:SingleField ID="Remark" runat="server" Width="680
                        px" /></td> 
            </tr> 
        </table>
    </fieldset>  

    <table>        
        <tr><td >
            <cc1:DSCTabControl ID="TabTrainee" runat="server" Height="120px"   
                Width="883px" PageColor="White">
            <TabPages>
                <cc1:DSCTabPage runat="server" Title="學員資料" Enabled="True" ImageURL="" Hidden="False" ID="DSCTabPage1">
                    <TabBody>
                        <table width="100%">
                            <tr>                                                               
                                <td>
                                    <%--<asp:FileUpload ID="FuPath" runat="server" Width="396px" />--%>
                                    &nbsp;
                                   <cc1:DSCLabel ID="lblImportFile" runat="server" style="text-align: left" 
                                       Text="檔案名稱" TextAlign="2" Width="72px" />                                     
                                   <cc1:SingleField ID="TraineeFileName" runat="server" Width="600px" />
                                   <cc1:OpenFileUpload ID="FileUploadTrainee" runat="server" onaddoutline="Trainee_AddOutline" Display="True" />
                                   <cc1:GlassButton ID="ButtonImport" runat="server" Height="20px" 
                                        onclick="TraineeImport_Click" Text="匯入" Width="70px" />
                                        <asp:HyperLink ID="HyperLink1" runat="server" 
                                        NavigateUrl="/ECP/SmpProgram/Maintain/SPTS005/學員匯入範例.xls">取得匯入格式</asp:HyperLink>
                                </td>
                                                              
                            </tr>
                            <tr><td>                        
                                <cc1:DataList ID="DataListTrainee" runat="server" Height="220px" 
                                    Width="860px" excelFileName="c5cd1bb8-f030-44cf-ade1-8a23b0e52f0c" 
                                    onbeforeopenwindow="DataListTrainee_BeforeOpenWindow"  />  </td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="TabMaterial" runat="server" Enabled="True" Hidden="False" 
                    ImageURL="" Title="課程教材">
                    <TabBody>
                        <table width="100%">
                            <tr><td align="right" Width="70px">
                                    <cc1:DSCLabel ID="LblSource" runat="server" Width="70px" Text="來源" TextAlign="2" /></td>
                                <td><cc1:SingleDropDownList ID="Source" runat="server" Width="70px" /></td>
                                <td align="right" Width="70px">
                                    <cc1:DSCLabel ID="LblMaterialGUID" runat="server" Width="70px" Text="文件號碼" 
                                            TextAlign="2" /></td>
                                <td><cc1:SingleOpenWindowField ID="MaterialGUID" runat="server" 
                                    width="470px" showReadOnlyField="True" guidField="GUID" keyField="DocNumber" 
                                    serialNum="001" tableName="Document" FixReadOnlyValueTextWidth="300px" 
                                    FixValueTextWidth="120px" 
                                    onbeforeclickbutton="MaterialGUID_BeforeClickButton" IgnoreCase="True" /></td>
                            </tr>
                            <tr><td colspan="4">
                                    <cc1:OutDataList ID="DataListMaterial" runat="server" height="200px" width="860px"
                                        OnSaveRowData="DataListMaterial_SaveRowData" 
                                        OnShowRowData="DataListMaterial_ShowRowData" EnableTheming="True" /></td>
                            </tr>
                        </table>
                    </TabBody>
                </cc1:DSCTabPage>
                <cc1:DSCTabPage ID="TabAttach" runat="server" Enabled="True" Hidden="False" 
                    ImageURL="" Title="相關附件">
                    <TabBody>
                        <table width="100%">
                            <tr>                                
                                <td><cc1:DSCLabel ID="LblAttaFileName" runat="server" style="text-align: left" 
                                        Text="檔案名稱" TextAlign="2" Width="72px" />                                     
                               <cc1:SingleField ID="AttaFileName" runat="server" Width="600px" />
                               <cc1:OpenFileUpload ID="FileUploadAtta" runat="server" onaddoutline="Atta_AddOutline" Display="True" />
                                        <cc1:GlassButton ID="ButtonUpload" runat="server" Height="20px" 
                                            onclick="AttaUpload_Click" Text="上傳檔案" Width="70px" /></td>
                            </tr>
                            <tr>
                                <td >
                                    <cc1:OutDataList ID="DataListAttachment" runat="server" Height="200px" 
                                            onbeforedeletedata="Atta_BeforeDeleteData"                                            
                                            onshowrowdata="DataListAttachment_ShowRowData" Width="860px" /></td>
                            </tr>
                        </table></TabBody>

                </cc1:DSCTabPage>
            </TabPages>
            </cc1:DSCTabControl>            
            </td></tr>           
    </table>
     
    </form>
</body>
</html>
