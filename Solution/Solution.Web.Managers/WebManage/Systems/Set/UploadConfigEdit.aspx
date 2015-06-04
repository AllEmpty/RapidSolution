<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadConfigEdit.aspx.cs"
    Inherits="Solution.Web.Managers.WebManage.Systems.Set.UploadConfigEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传类型编辑</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="10px" EnableCollapse="True"
        ShowHeader="False">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSave" runat="server" Text="保存" Icon="Disk" OnClick="ButtonSave_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" runat="server" EnableFrame="false" BodyPadding="5px" EnableCollapse="True"
                ShowHeader="False" ShowBorder="False">
                <Items>
                    <f:HiddenField runat="server" ID="hidId" Text="0"></f:HiddenField>
                    <f:Form ID="Form6" ShowBorder="True" BodyPadding="5px" ShowHeader="False" runat="server">
                        <Rows>
                            <f:FormRow ID="FormRow1" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="名称" ID="txtName" ShowRedStar="true" Width="300px">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="关联表名" ID="txtJoinName" ShowRedStar="true" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow2" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblUserType" Label="用户类别" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="管理员上传" Value="1" Selected="true" />
                                        <f:RadioItem Text="会员上传" Value="2" />
                                    </f:RadioButtonList>
                                    <f:DropDownList Label="上传类型" Required="true" CompareType="String" runat="server" ID="ddlUploadTypeId" Width="300px" ShowRedStar="True">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow3" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="图片上传限制" ID="txtPicSize" ShowRedStar="true" Width="300px" EmptyText="单位为KB">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="附件上传限制" ID="txtFileSize" ShowRedStar="true" Width="300px" EmptyText="单位为KB">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow4" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="存储路径" ID="txtSaveDir" ShowRedStar="true" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow19" runat="server">
                                <Items>
                                    <f:Label runat="server"></f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow9" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsPost" Label="是否启用" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="禁用" Value="0" />
                                        <f:RadioItem Text="启用" Value="1" Selected="true" />
                                    </f:RadioButtonList>
                                    <f:RadioButtonList ID="rblIsEditor" Label="类型" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="控件上传" Value="0" />
                                        <f:RadioItem Text="编辑器上传" Value="1" Selected="true" />
                                    </f:RadioButtonList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow5" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsSwf" Label="上传方式" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="Web上传" Value="0" Selected="true" />
                                        <f:RadioItem Text="Flash上传" Value="1" />
                                    </f:RadioButtonList>
                                    <f:RadioButtonList ID="brlIsChkSrcPost" Label="是否检查入口" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="否" Value="0" />
                                        <f:RadioItem Text="是" Value="1" Selected="true" />
                                    </f:RadioButtonList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow6" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsFixPic" Label="按比例生成" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="否" Value="0" />
                                        <f:RadioItem Text="是" Value="1" Selected="true" />
                                    </f:RadioButtonList>
                                     <f:DropDownList Label="生成方式" CompareType="String" runat="server" ID="ddlCutType" Width="300px">
                                         <f:ListItem Text="按比例生成宽高" Value="0" Selected="true" />
                                         <f:ListItem Text="固定图片宽高" Value="1" />
                                         <f:ListItem Text="固定背景宽高，图片按比例生成" Value="2" />
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow7" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="最大宽度" ID="txtPicWidth" Width="300px" EmptyText="超过本设置将按比例进行缩放">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="最大高度" ID="txtPicHeight" Width="300px" EmptyText="超过本设置将按比例进行缩放">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow8" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="图片质量" ID="txtPicQuality" Width="300px">
                                    </f:TextBox>
                                    <f:Label runat="server" Label="说明" Text="图片质量，0=使用默认值，>0指定质量值（指定值的情况下，范围：50-100）"></f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow20" runat="server">
                                <Items>
                                    <f:Label ID="Label1" runat="server"></f:Label>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow10" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsBigPic" Label="是否创建大图" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="否" Value="0" />
                                        <f:RadioItem Text="是" Value="1" Selected="true" />
                                    </f:RadioButtonList>
                                    <f:TextBox runat="server" Label="大图宽度" ID="txtBigWidth" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow11" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="大图高度" ID="txtBigHeight" Width="300px">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="大图压缩质量" ID="txtBigQuality" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow12" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsMidPic" Label="是否创建中图" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="否" Value="0" Selected="true" />
                                        <f:RadioItem Text="是" Value="1" />
                                    </f:RadioButtonList>
                                    <f:TextBox runat="server" Label="中图宽度" ID="txtMidWidth" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow13" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="中图高度" ID="txtMidHeight" Width="300px">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="中图压缩质量" ID="txtMidQuality" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow14" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsMinPic" Label="是否创建小图" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="否" Value="0" Selected="true" />
                                        <f:RadioItem Text="是" Value="1" />
                                    </f:RadioButtonList>
                                    <f:TextBox runat="server" Label="小图宽度" ID="txtMinWidth" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow15" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="小图高度" ID="txtMinHeight" Width="300px">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="小图压缩质量" ID="txtMinQuality" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow16" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsHotPic" Label="是否创建推荐图" ColumnNumber="2" runat="server" Required="true">
                                        <f:RadioItem Text="否" Value="0" Selected="true" />
                                        <f:RadioItem Text="是" Value="1" />
                                    </f:RadioButtonList>
                                    <f:TextBox runat="server" Label="推荐图宽度" ID="txtHotWidth" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow17" runat="server">
                                <Items>
                                    <f:TextBox runat="server" Label="推荐图高度" ID="txtHotHeight" Width="300px">
                                    </f:TextBox>
                                    <f:TextBox runat="server" Label="推荐图压缩质量" ID="txtHotQuality" Width="300px">
                                    </f:TextBox>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow18" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsWaterPic" Label="是否加水印" ColumnNumber="2" runat="server" Required="true" Width="300px">
                                        <f:RadioItem Text="否" Value="0" Selected="true" />
                                        <f:RadioItem Text="是" Value="1" />
                                    </f:RadioButtonList>
                                    <f:Label ID="Label2" runat="server" Label="说明" Text="水印图片路径在配置文件里设置"></f:Label>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                </Items>
            </f:Panel>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
