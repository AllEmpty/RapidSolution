<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="InformationEdit.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Informations.InformationEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息编辑</title>
    <link rel="stylesheet" type="text/css" href="../Css/main.css" />
    <script type="text/javascript" src="../Js/jquery-1.6.4.min.js"></script>
    <script type="text/javascript" src="../Js/tools.js"></script>
    <script type="text/javascript" src="../Js/ke4/kindeditor-min.js" charset="utf-8"></script>
    <script type="text/javascript">
        var oFCKeditor1 = null;

        $(document).ready(function () {
            setTimeout(function () { New_Editor() }, 300);
        });

        function New_Editor() {
            var key = $('#txtRndKey-inputEl').val();
            if (key) {
                $('#FCKeditor1').val($('#txtText-inputEl').val());
                $('#FCKeditor1___UpFileList').val($('#txtUpload-inputEl').val());
                //下一行执行的Js函数CreateKindEditor('FCKeditor1', x, key)中，其中x代表的是上传配置表（UploadConfig）中对应的记录Id
                oFCKeditor1 = CreateKindEditor('FCKeditor1', 4, key);
            } else {
                setTimeout(function () { New_Editor() }, 3000);
            }
        }

        //编辑器提交必须调用
        function Keditor_GetHtml() {
            var html = oFCKeditor1.html();
            $('#txtText-inputEl').val(html);
            $('#txtUpload-inputEl').val($('#FCKeditor1___UpFileList').val());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--编辑器提交必须调用的控件-->
    <f:TextBox runat="server" ID="txtText" Text="" Hidden="true" />
    <f:TextBox runat="server" ID="txtUpload" Text="" Hidden="true" />
    <f:TextBox runat="server" ID="txtRndKey" Text="" Hidden="true" />
    <f:HiddenField runat="server" ID="hidId" Text="0">
    </f:HiddenField>
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="10px" EnableCollapse="True"
        ShowHeader="False">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSave" runat="server" Text="保存" Icon="Disk" OnClick="ButtonSave_Click" OnClientClick="Keditor_GetHtml();">
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
                                    <f:TextBox runat="server" ID="txtTitle" Label="文章标题" Width="300px" Text=""
                                        ShowRedStar="true" Required="true" MaxLength="100" />
                                    <f:DropDownList Label="所属栏目" AutoPostBack="true" ShowRedStar="true" Required="true"
                                        CompareType="String" EnableSimulateTree="true" runat="server" ID="ddlInformationClass_Id"
                                        OnSelectedIndexChanged="ddlInformationClassId_SelectedIndexChanged" Width="300px" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow2" runat="server">
                                <Items>
                                    <f:TextBox runat="server" ID="txtKeywords" Label="关键字" Width="300px" Text="" MaxLength="50" />
                                    <f:DatePicker ID="dpNewsTime" Label="发布时间" Width="300px" Required="true" runat="server" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow11" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsDisplay" Label="是否审核" ColumnNumber="2" runat="server" Width="300px">
                                        <f:RadioItem Text="显示" Value="1" Selected="true" />
                                        <f:RadioItem Text="不显示" Value="0" />
                                    </f:RadioButtonList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow3" runat="server">
                                <Items>
                                    <f:RadioButtonList ID="rblIsTop" Label="是否置顶" ColumnNumber="2" runat="server" Width="300px">
                                        <f:RadioItem Text="是" Value="1" />
                                        <f:RadioItem Text="否" Value="0" Selected="true" />
                                    </f:RadioButtonList>
                                    <f:RadioButtonList ID="rblIsHot" Label="是否推荐" ColumnNumber="2" runat="server" Width="300px">
                                        <f:RadioItem Text="是" Value="1" Selected="true" />
                                        <f:RadioItem Text="否" Value="0" />
                                    </f:RadioButtonList>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow10" runat="server">
                                <Items>
                                    <f:TextBox runat="server" ID="txtRedirectUrl" Label="重定向页面" Width="716px" Text=""
                                        MaxLength="50" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow4" runat="server" >
                                <Items>
                                    <f:ContentPanel ID="ContentPanel1" runat="server" Width="800px" Height="300px" BodyPadding="5px"
                                        ShowBorder="false" ShowHeader="false">
                                        <textarea name="FCKeditor1" id="FCKeditor1" style="width: 90%; height: 280px; display: none;"></textarea>
                                        <input type="hidden" id="FCKeditor1___UpFileList" name="FCKeditor1___UpFileList"
                                            value="" />
                                    </f:ContentPanel>
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow5" runat="server">
                                <Items>
                                    <f:TextBox runat="server" ID="txtSeoTitle" Label="Seo标题" Width="300px" Text="" MaxLength="100" />
                                    <f:TextBox runat="server" ID="txtSeoKey" Label="SEO关键字" Width="300px" Text="" MaxLength="100" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow6" runat="server">
                                <Items>
                                    <f:TextArea runat="server" ID="txtSeoDesc" Label="SEO说明" Width="716px" Height="50px" Text="" MaxLength="200" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow7" runat="server">
                                <Items>
                                    <f:TextArea runat="server" ID="txtNotes" Label="内容简介" Width="716px" Text=""
                                        MaxLength="200" Height="50px" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow8" runat="server">
                                <Items>
                                    <f:TextBox runat="server" ID="txtAuthor" Label="文章作者" Width="300px" Text=""
                                        MaxLength="50" />
                                    <f:TextBox runat="server" ID="txtFromName" Label="文章出处" Width="300px" Text=""
                                        MaxLength="50" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow9" runat="server">
                                <Items>
                                    <f:FileUpload runat="server" ID="filePhoto" Label="文章封面" Width="300px" />
                                </Items>
                            </f:FormRow>
                            <f:FormRow ID="FormRow12" runat="server">
                                <Items>
                                    <f:ContentPanel ID="ContentPanel3" runat="server" Width="90%" 
                                        ShowBorder="false" ShowHeader="false">
                                        <%=(p_Img != null && p_Img.Length > 5) ? "<a href='" + p_Img + "' target=\"_blank\" class='PicToolTip2'><img src='" + p_Img + "'></a>" : ""%>
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
