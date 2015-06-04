<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="AdvertisementEdit.aspx.cs"
    Inherits="Solution.Web.Managers.WebManage.Advertisements.AdvertisementEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>广告编辑</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:HiddenField runat="server" ID="hidId" Text="0">
    </f:HiddenField>
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="10px" EnableCollapse="True"
        ShowHeader="False">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSave" runat="server" Text="保存" Icon="Disk" OnClick="ButtonSave_Click">
                    </f:Button>
                    <f:Button ID="ButtonDeleteImage" runat="server" Text="删除图片" Icon="Delete" OnClick="ButtonDeleteImage_Click"
                        ConfirmTitle="删除提示" ConfirmText="是否删除图片？" />
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" runat="server" EnableFrame="false" BodyPadding="5px" EnableCollapse="True"
                ShowHeader="False" ShowBorder="False">
                <Items>
                    <f:Form ID="extForm1" ShowBorder="false" ShowHeader="false" BodyPadding="5px" runat="server">
                        <Rows>
                            <f:FormRow ID="FormRow1" runat="server">
                                <Items>
                                    <f:TextBox runat="server" ID="txtName" Label="广告名称" Width="300px" Text="" ShowRedStar="true"
                                        MaxLength="50" />
                                    <f:TextBox runat="server" ID="txtKeyword" Label="Key(非中文)" Width="300px" Text=""
                                        MaxLength="50" ShowRedStar="true" Readonly="True" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow2" runat="server">
                                <Items>
                                    <f:DropDownList Label="广告位置" AutoPostBack="true" CompareType="String" EnableSimulateTree="true"
                                        runat="server" ID="ddlAdvertisingPosition" Width="300px" ShowRedStar="true" OnSelectedIndexChanged="ddlAdvertisingPosition_SelectedIndexChanged">
                                    </f:DropDownList>
                                    <f:RadioButtonList ID="rblIsDisplay" Label="是否审核" ColumnNumber="2" runat="server"
                                        Width="300px">
                                        <f:RadioItem Text="显示" Value="1" Selected="true" />
                                        <f:RadioItem Text="不显示" Value="0" />
                                    </f:RadioButtonList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow4" runat="server">
                                <Items>
                                    <f:DatePicker ID="dpStartTime" Label="开始时间" Width="300px" Required="true" runat="server"
                                        ShowRedStar="true" />
                                    <f:DatePicker ID="dpEndTime" Label="结束时间" Width="300px" Required="true" runat="server"
                                        ShowRedStar="true" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow11" runat="server">
                                <Items>
                                    <f:TextBox runat="server" ID="txtUrl" Label="广告链接地址" Width="610px" Text="" MaxLength="200" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow3" runat="server">
                                <Items>
                                    <f:TextArea runat="server" Label="说明" ID="txtContent" Width="610px" MaxLength="100"
                                        Height="50px">
                                    </f:TextArea>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow10" runat="server">
                                <Items>
                                    <f:FileUpload runat="server" ID="filePhoto" Label="广告图片" Width="300px" />
                                    <f:TextBox runat="server" ID="txtSort" Label="排序" Width="300px" Text="0" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="ShowImage" runat="server">
                                <Items>
                                    <f:ContentPanel ID="ContentPanel3" runat="server" Width="90%" ShowBorder="false"
                                        ShowHeader="false">
                                        <%=(p_Img != null && p_Img.Length > 5) ? "<a href='" + p_Img + "' target=\"_blank\"><img src='" + p_Img + "'></a>" : ""%>
                                    </f:ContentPanel>
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
