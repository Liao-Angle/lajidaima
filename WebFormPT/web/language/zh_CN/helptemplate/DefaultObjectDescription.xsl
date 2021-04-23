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
              <xsl:value-of select="/DataObject/@childClassString"></xsl:value-of>
            </div>
          </td>
          <td align="right">
            <a href="#" onclick="window.print()">
              <img src="Images/rtg_print.gif" border="0" alt="打印本页"></img>
            </a>
          </td>
        </tr>
      </table>
      <table class="BasicFormHeadBorder" width="666px" border="0" cellspacing="0" cellpadding="5">
        <tr>
          <td class="BasicFormHeadHead" width="100px">
            组件名称
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="/DataObject/@assemblyName"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            TimeStamp
          </td>
          <td class="BasicFormHeadDetail">
            <xsl:value-of select="/DataObject/isCheckTimeStamp"/>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            识别字段
          </td>
          <td class="BasicFormHeadDetail">
            <table border="0" style="font-size:9pt">
              <tr>
                <xsl:apply-templates select="DataObject/identityField/field"></xsl:apply-templates>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            键值字段
          </td>
          <td class="BasicFormHeadDetail">
            <table border="0" style="font-size:9pt">
              <tr>
                <xsl:apply-templates select="DataObject/keyField/field"></xsl:apply-templates>
              </tr>
            </table>
          </td>
        </tr>
        <tr>
          <td class="BasicFormHeadHead">
            不更新字段
          </td>
          <td class="BasicFormHeadDetail">
            <table border="0" style="font-size:9pt">
              <tr>
                <xsl:apply-templates select="DataObject/nonUpdateField/field"></xsl:apply-templates>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <br></br>--详细信息--
      <table class="BasicFormHeadBorder" width="666px" border="0" cellspacing="0" cellpadding="5">
        <tr>
          <td class="BasicFormHeadHead" nowrap="true">字段名称</td>
          <td class="BasicFormHeadHead" nowrap="true">类型</td>
          <td class="BasicFormHeadHead" nowrap="true">长度</td>
          <td class="BasicFormHeadHead" nowrap="true">显示名称</td>
          <td class="BasicFormHeadHead" nowrap="true">内容意义</td>
        </tr>
        <xsl:apply-templates select="DataObject/fieldDefinition/field"></xsl:apply-templates>
      </table>

    </body>
  </html>
</xsl:template>
<xsl:template match="DataObject/fieldDefinition/field">
  <tr>
    <td class="BasicFormHeadDetail">
      <xsl:value-of select="@dataField"></xsl:value-of>
    </td>
    <td class="BasicFormHeadDetail">
      <xsl:value-of select="@typeField"></xsl:value-of>
    </td>
    <td class="BasicFormHeadDetail">
      <xsl:value-of select="@lengthField"></xsl:value-of>
    </td>
    <td class="BasicFormHeadDetail">
      <xsl:value-of select="@displayName"></xsl:value-of>
    </td>
    <td class="BasicFormHeadDetail" nowrap="true">　
      <xsl:value-of select="@showName"></xsl:value-of>
    </td>
  </tr>
</xsl:template>
  <xsl:template match="DataObject/allowEmptyField/field">
      <td>
        <xsl:value-of select="@dataField"></xsl:value-of>
      </td>
  </xsl:template>
  <xsl:template match="DataObject/identityField/field">
      <td>
        <xsl:value-of select="@dataField"></xsl:value-of>
      </td>
  </xsl:template>
  <xsl:template match="DataObject/keyField/field">
      <td>
        <xsl:value-of select="@dataField"></xsl:value-of>
      </td>
  </xsl:template>
  <xsl:template match="DataObject/nonUpdateField/field">
      <td>
        <xsl:value-of select="@dataField"></xsl:value-of>
      </td>
  </xsl:template>
</xsl:stylesheet> 


