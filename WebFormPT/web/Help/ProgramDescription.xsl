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
              <img src="Images/rtg_print.gif" border="0" alt="列印本頁"></img>
            </a>
          </td>
        </tr>
      </table>
      <table class="BasicFormHeadBorder" width="100%" border="0" cellspacing="0" cellpadding="5">
        <tr>
          <td class="BasicFormHeadHead" nowrap="true">
            程式代碼
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramID"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            作業名稱
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramName"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            使用權限
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramAuth"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            作業網址
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramURL"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            作業目的
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="Program/ProgramTarget"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            使用時機
          </td>
          <td class="BasicFormHeadDetail">
            <pre>
              <xsl:value-of select="Program/ProgramTime"/>
          </pre>
        </td>
      </tr>
      <tr>
        <td class="BasicFormHeadHead">
          操作說明
        </td>
        <td class="BasicFormHeadDetail">
          <pre>
            <xsl:value-of select="Program/ProgramOperate"/>
          </pre>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            注意事項
          </td>
          <td class="BasicFormHeadDetail">
            <pre>
              <xsl:value-of select="Program/ProgramNotice"/>
            </pre>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            參考文件
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

