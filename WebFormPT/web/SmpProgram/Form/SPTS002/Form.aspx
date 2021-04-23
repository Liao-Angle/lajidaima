<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="SmpProgram_Form_SPTS002_Form" %>

<%@ Register Assembly="DSCWebControl" Namespace="DSCWebControl" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../../StyleSheet/WebFormPT.css" rel="stylesheet" type="text/css" />
    <title>教育訓練滿意度調查表</title>
    <style type="text/css">
    </style>
</head>

<body leftmargin=0 topmargin=0>

<form id="form1" runat="server">
	<cc1:SingleField ID="CourseFormGUID" runat="server" Display="False" />
	<cc1:SingleField ID="TraineeGUID" runat="server" Display="False" />
	
	<div>	
	<table style="margin-left:4px; width: 760px;" border=0 cellspacing=0 cellpadding=1 >
		<tr height="40"> 
			<td align=center colSpan="5">
				<b><font style="font-family: 標楷體; font-size: large;">教育訓練滿意度調查表</font></b> 
            </td>
		</tr>
	</table>
	<table style="margin-left:0px; width: 730px;" border=0 cellspacing=0 cellpadding=0 >
		<tr>
			<td colSpan="5" ><font size="2">			    
				各位親愛的同仁：<br>
				首先感謝您參與此次的訓練課程，在接近尾聲的同時，希望您能抽出幾分鐘來填寫這份調查表，以作為日後安排訓練課程相關事宜改進之依據。謝謝!<br>
				<font color="#ff0000"> *** 如您未參加本課程，請退件處理 ***</font>
				</font>
			</td>
		</tr>
	</table>
	<br>
	<table style="margin-left:4px; width: 660px;" border=0 cellspacing=0 cellpadding=1 class=BasicFormHeadBorder>
		<tr>
			<td align="right" class="BasicFormHeadHead" title="單號>
				<cc1:DSCLabel ID="lblSheetNo" runat="server" Text="單號" Width="85px" TextAlign="2" />
			</td>
			<td class=BasicFormHeadDetail  colSpan="3">
				<cc1:SingleField ID="SheetNo" runat="server" Width="120px" ReadOnly="True" />
			</td>
		</tr>
		<tr>
			<td align="right" class="BasicFormHeadHead" title="主旨">
				<cc1:DSCLabel ID="lblSubject" runat="server" Text="主旨" Width="85px" IsNecessary="False" TextAlign="2" />
			</td>
			<td class=BasicFormHeadDetail  colSpan="3">
				<cc1:SingleField ID="Subject" runat="server" Width="635px" />
			</td>
		</tr>
		<tr>
			<td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="lblDateS" runat="server" Text="上課日期" TextAlign="2" Width="83px" IsNecessary="True" /> 
			</td>
            <td class=BasicFormHeadDetail colSpan="3">
                <cc1:SingleDateTimeField ID="StartDate" runat="server" Width="116px" />		
			</td> 
		</tr>
		
		<tr>
			<td align="right" class="BasicFormHeadHead">
                 <cc1:DSCLabel ID="lblCourseName" runat="server" Text="課程名稱" Width="70px" TextAlign="2" IsNecessary="True" /> 
			</td>
            <td class=BasicFormHeadDetail colSpan="3">
                   <cc1:SingleOpenWindowField ID="SchDetailGUID" runat="server" Width="379px" 
                           showReadOnlyField="True" guidField="GUID" keyField="SubjectNo" serialNum="001" 
                           tableName="TSSchForm" FixReadOnlyValueTextWidth="250px" 
                       FixValueTextWidth="95px" IgnoreCase="True" dialogWidth="600" idIndex="3" valueIndex="4" />
			</td>  
		</tr>
			<td align="right" class="BasicFormHeadHead">
                  <cc1:DSCLabel ID="lblLecturer" runat="server" Text="講師" TextAlign="2" 
                      Width="70px" IsNecessary="False" />  </td>
            <td class=BasicFormHeadDetail colSpan="3"> 
                   <cc1:SingleOpenWindowField ID="LecturerGUID" runat="server" Width="388px" 
                           showReadOnlyField="True" guidField="GUID" keyField="GUID" serialNum="001" 
                           tableName="TSLecturer" FixReadOnlyValueTextWidth="250px" 
                       FixValueTextWidth="95px" OnClick="LecturerGUID_Click" IgnoreCase="True" />
			</td> 
		</tr>
		<tr>
		  <td class="BasicFormHeadHead" colspan="4">
		    <table>
				<tr>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQDesc" runat="server" Text="內　　　　　　　　　　容" TextAlign="1" />  
					</td>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ5" runat="server" Text="很滿意" Width="100px" TextAlign="1" />  
					</td>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ4" runat="server" Text="滿意" Width="100px" TextAlign="1" />  
					</td>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ3" runat="server" Text="普通" Width="100px" TextAlign="1" />  
					</td>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ2" runat="server" Text="不滿意" Width="100px" TextAlign="1" />  
					</td>
				</tr>
				<tr>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ1Desc" runat="server" Text="一、教師授課的整體表現" width="300px" TextAlign="0" />  
					</td>
					<td align="right" class="BasicFormHeadHead" colspan="4">
					  <cc1:DSCLabel ID="LblQ1Zero" runat="server" Text="" Width="70px" TextAlign="2" />  
					</td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ11" runat="server" Text="　1.教師對授課時間掌握情況" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q115" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q114" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q113" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q112" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ12" runat="server" Text="　2.教師能掌握我課程的學習方向" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q125" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q124" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q123" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q122" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ13" runat="server" Text="　3.教師專業知識的能力" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q135" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q134" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q133" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q132" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ14" runat="server" Text="　4.教師的授課技巧與表達能力" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q145" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q144" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q143" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q142" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ15" runat="server" Text="　5.教師授課時的教學態度" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q155" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q154" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q153" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q152" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ16" runat="server" Text="　6.教師對學員課程上問題之解決能力" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q165" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q164" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q163" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q162" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ17" runat="server" Text="　7.教師對授課內容組織條理" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q175" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q174" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q173" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q172" runat="server" /></td>
				</tr>
				<tr>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ2Desc" runat="server" Text="二、課程設計" TextAlign="0" />  
					</td>
					<td align="right" class="BasicFormHeadHead" colspan="4">
					  <cc1:DSCLabel ID="LblQ2Zero" runat="server" Text="" Width="200px" TextAlign="2" />  
					</td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ21" runat="server" Text="　1.課程內容明確易懂" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q215" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q214" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q213" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q212" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ22" runat="server" Text="　2.課程內容豐富具有多樣性" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q225" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q224" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q223" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q222" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ23" runat="server" Text="　3.課程內容具有實用性" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q235" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q234" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q233" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q232" runat="server" /></td>
				</tr>
				<tr>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ3Desc" runat="server" Text="三、自我評量" TextAlign="0" />  
					</td>
					<td align="right" class="BasicFormHeadHead" colspan="4">
					  <cc1:DSCLabel ID="LblQ3Zero" runat="server" Text="" Width="200px" TextAlign="2" />  
					</td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ31" runat="server" Text="　1.我認為上完此課程對我有實質的幫助" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q315" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q314" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q313" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q312" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ32" runat="server" Text="　2.本次訓練有助於提昇自己的工作能力" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q325" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q324" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q323" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q322" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ33" runat="server" Text="　3.對工作環境中所運用所學的新知識更具信心" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q335" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q334" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q333" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q332" runat="server" /></td>
				</tr>
				<tr>
					<td align="right" class="BasicFormHeadHead">
					  <cc1:DSCLabel ID="LblQ4Desc" runat="server" Text="四、上課環境與服務品質" TextAlign="0" />  
					</td>
					<td align="right" class="BasicFormHeadHead" colspan="4">
					  <cc1:DSCLabel ID="LblQ4Zero" runat="server" Text="" Width="200px" TextAlign="2" />  
					</td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ41" runat="server" Text="　1.對上課環境的滿意度" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q415" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q414" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q413" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q412" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ42" runat="server" Text="　2.課程時間安排" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q425" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q424" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q423" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q422" runat="server" /></td>
				</tr>
				<tr>
					<td width="70px"  align="right" class="BasicFormHeadHead">
						<cc1:DSCLabel ID="lbQ43" runat="server" Text="　3.對本次課程總體評估" Width="300px" TextAlign="0"/></cc1:DSCLabel>
					</td>        
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q435" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q434" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q433" runat="server" /></td>
					<td class="BasicFormHeadDetail" ><cc1:DSCRadioButton ID="Q432" runat="server" /></td>
				</tr>	
		    </table>
		  </td>
		</tr>

		         
        <tr>
            <td align="LEFT" class="BasicFormHeadHead" colspan="4">
              <cc1:DSCLabel ID="LblRemark" runat="server" Text="五、其他建議及意見： "  TextAlign="0" />  
			</td>
		</tr>
		<tr>
            <td class="BasicFormHeadDetail" colspan="5" TextAlign="1">
			　　<cc1:SingleField ID="Remark" runat="server" Height="50px" Width="690px" MultiLine="True" />
			</td> 
        </tr> 			
    </table>
    
	</div>
</form>

</body>
</html>
