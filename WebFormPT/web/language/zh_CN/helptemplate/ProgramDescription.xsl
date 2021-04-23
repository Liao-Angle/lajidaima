<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
    <html>
      <head>
        <link href="ProgramDescription.css" rel="stylesheet" type="text/css" />
      </head>
      <body style="overflow:scroll">
      <table border="0" width="100%" >
        <tr>
          <td width="100%">
            <div class="h">
              <xsl:value-of select="Program/ProgramName"></xsl:value-of>
            </div>
          </td>
          <td align="right">
            <a href="#" onclick="window.print()">
              <img src="Images/rtg_print.gif" border="0" alt="打印本页"></img>
            </a>
          </td>
        </tr>
      </table>
      <table class="BasicFormHeadBorder" width="100%" border="0" cellspacing="0" cellpadding="5">
        <tr>
          <td class="BasicFormHeadHead">
            程序代码
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramID"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            作业名称
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramName"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            使用权限
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramAuth"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            作业网址
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramURL"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            作业目的
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramTarget"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            使用时机
          </td>
          <td class="BasicFormHeadDetail">
            <pre>
              <xsl:value-of select="Program/ProgramTime"/>
          </pre>
        </td>
      </tr>
      <tr>
        <td class="BasicFormHeadHead">
          操作说明
        </td>
        <td class="BasicFormHeadDetail">
          <pre>
            <xsl:value-of select="Program/ProgramOperate"/>
          </pre>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            注意事项
          </td>
          <td class="BasicFormHeadDetail">
            <pre>
              <xsl:value-of select="Program/ProgramNotice"/>
            </pre>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            参考文件
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramFile"/>
          </td>
        </tr>
      </table>
    </body>
    </html>
</xsl:template>

</xsl:stylesheet> 

