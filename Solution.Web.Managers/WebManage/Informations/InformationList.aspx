<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Informations.InformationList" %>
<%@ Import Namespace="DotNet.Utilities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel1" runat="server" title="信息列表" enableframe="false" bodypadding="10px"
        enablecollapse="True">
        <toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonRefresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="ButtonRefresh_Click" CssClass="inline"></f:Button>
                    <f:Button ID="ButtonSearch" runat="server" Text="查询" Icon="Magnifier" OnClick="ButtonSearch_Click"></f:Button>
                    <f:Button ID="ButtonAdd" runat="server" Text="添加" Icon="Add" OnClick="ButtonAdd_Click"></f:Button>
                    <f:Button ID="ButtonSaveAutoSort" runat="server" Text="自动排序" Icon="ArrowJoin" OnClick="ButtonSaveAutoSort_Click" ConfirmTitle="自动排序提示" ConfirmText="是否对所有数据进行自动排序？"></f:Button>
                    <f:Button ID="ButtonSaveSort" runat="server" Text="保存排序" Icon="Disk" OnClick="ButtonSaveSort_Click"></f:Button>
                    
                    <f:Button ID="ButtonDelete" runat="server" Text="删除" Icon="Delete" OnClick="ButtonDelete_Click" ConfirmTitle="删除提示" ConfirmText="是否删除记录？" 
                        OnClientClick="if (!F('Panel1_Grid1').getSelectionModel().hasSelection() ) { F.alert('请选择你想要删除的记录！'); return false; } ">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <items>
            <f:Form ID="Form6" ShowBorder="True" BodyPadding="5px" ShowHeader="False" runat="server">
                <Rows>
                    <f:FormRow ID="FormRow1" runat="server">
                        <Items>
                            <f:TextBox runat="server" ID="txtKey" Label="标题或Key" Width="260px" Text="" MaxLength="20"  />
                            <f:DropDownList CompareType="String" Label="所属栏目" EnableSimulateTree="true" runat="server" ID="dllInformationClass" Width="260px" />
                            </Items>
                    </f:FormRow>
                    <f:FormRow ID="FormRow3" runat="server">
                        <Items>
                            <f:DropDownList CompareType="String" Label="审批状态"
                                runat="server" ID="ddlIsDisplay" Width="260px" >
                                <f:ListItem Text="==全部==" Value="" />
                                <f:ListItem Text="已审批" Value="1" />
                                <f:ListItem Text="未审批" Value="0" />
                            </f:DropDownList>
                            <f:DropDownList CompareType="String" Label="推荐状态"
                                runat="server" ID="ddlIsHot" Width="260px" >
                                <f:ListItem Text="==全部==" Value="" />
                                <f:ListItem Text="已推荐" Value="1" />
                                <f:ListItem Text="未推荐" Value="0" />
                            </f:DropDownList>
                        </Items>
                    </f:FormRow>
                    <f:FormRow ID="FormRow2" runat="server">
                        <Items>
                            <f:DatePicker runat="server" Label="起始日期" ID="dpStart" DateFormatString="yyyy-M-d" Width="260px" />
                            <f:DatePicker runat="server" Label="终止日期" ID="dpEnd" DateFormatString="yyyy-M-d" Width="260px" />
                        </Items>
                    </f:FormRow>
                </Rows>
            </f:Form>
            <f:Grid ID="Grid1" Title="信息列表" EnableFrame="false" EnableCollapse="true" AllowSorting="true" IsDatabasePaging="True" Height="420px"
            PageSize="15" ShowBorder="true" ShowHeader="False" AllowPaging="true" runat="server" EnableCheckBoxSelect="True" DataKeyNames="Id" EnableColumnLines="true"
            OnPageIndexChange="Grid1_PageIndexChange" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand" OnSort="Grid1_Sort">
                <Columns>
                    <f:BoundField DataField="Id" SortField="Id" HeaderText="ID" Width="50px" />
                    <f:TemplateField HeaderText="封面" Width="100px">
                        <ItemTemplate>
                            <%# Eval("FrontCoverImg").ToString().Length > 5 ? "<a href='" + Eval("FrontCoverImg") + "' target=\"_blank\" class='PicToolTip'><img src='" + DirFileHelper.GetFilePathPostfix(Eval("FrontCoverImg")+ "", "s") + "'></a>" : ""%>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField DataField="InformationClass_Name" SortField="InformationClass_Id"  HeaderText="分类" Width="80px" />
                    <f:BoundField DataField="Title" SortField="Title" HeaderText="文章标题" Width="250px" />
                    <f:BoundField DataField="Keywords" SortField="Keywords" HeaderText="关键字" Width="80px" />
                    <f:BoundField DataField="ViewCount" SortField="ViewCount" HeaderText="浏览数" Width="60px" />
                    <f:BoundField DataField="CommentCount" SortField="CommentCount" HeaderText="评论数" Width="60px" />
                    <f:TemplateField HeaderText="排序" Width="100px">
                        <ItemTemplate>
                            <asp:TextBox ID="tbSort" runat="server" Width="50px" Text='<%# Eval("Sort") %>' AutoPostBack="false"></asp:TextBox>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:LinkButtonField ColumnID="IsDisplay" SortField="IsDisplay" HeaderText="审核" TextAlign="Center" CommandName="IsDisplay" Width="40px"  />
                    <f:LinkButtonField ColumnID="IsTop" SortField="IsTop" HeaderText="置顶" TextAlign="Center" CommandName="IsTop" Width="40px"  />
                    <f:LinkButtonField ColumnID="IsHot" SortField="IsHot" HeaderText="推荐" TextAlign="Center" CommandName="IsHot" Width="40px"  />
                    <f:BoundField DataField="UpdateDate" SortField="UpdateDate" HeaderText="更新时间" TextAlign="left" Width="130px" />
                    <f:BoundField DataField="Manager_CName" SortField="Manager_CName" HeaderText="更新人" TextAlign="left" Width="80px" />
                    <f:LinkButtonField Width="60px" HeaderText="操作" TextAlign="Center" ToolTip="点击修改当前记录" ColumnID="ButtonEdit" CommandName="ButtonEdit" />
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text=""></f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id"></f:HiddenField>
        </items>
    </f:panel>
    <f:window id="Window1" width="800px" height="620px" icon="TagBlue" title="编辑" hidden="True"
        enablemaximize="True" closeaction="HidePostBack" onclose="Window1_Close" enablecollapse="true"
        runat="server" enableresize="true" bodypadding="5px" enableframe="True" iframeurl="about:blank"
        enableiframe="true" enableclose="true" ismodal="True" enableconfirmonclose="True">
    </f:window>
    </form>
</body>
</html>
