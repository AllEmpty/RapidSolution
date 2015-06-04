<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvertisingPositionList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Advertisements.AdvertisingPositionList" %>
<%@ Import Namespace="DotNet.Utilities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>广告位置列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel1" runat="server" title="广告位置列表" enableframe="false" bodypadding="10px"
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
                        OnClientClick="if (!F('Panel1_Grid1').getSelectionModel().hasSelection() ) { F.alert('删除时必须选择一条将要删除的记录！'); return false; }  if (F('Panel1_Grid1').getSelectionModel().getCount() >= 2) { F.alert('只能选择一条记录进行删除！');return false; }">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <items>
             <f:SimpleForm ID="SimpleForm1" BodyPadding="5px" runat="server" EnableFrame="false" EnableCollapse="true"
                ShowBorder="True" ShowHeader="False">
                <Items>
                    <f:DropDownList Label="广告位置选择" runat="server" ID="ddlParentId" Width="250px"></f:DropDownList>
                </Items>
            </f:SimpleForm>
            <f:Grid ID="Grid1" Title="广告位置列表" EnableFrame="false" EnableCollapse="true" AllowSorting="true" Height="420px"
            PageSize="15" ShowBorder="true" ShowHeader="False" runat="server" EnableCheckBoxSelect="True" DataKeyNames="Id" EnableColumnLines="true"
            OnPageIndexChange="Grid1_PageIndexChange" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand">
                <Columns>
                    <f:BoundField DataField="Id" HeaderText="位置ID" Width="50px" />
                    <f:TemplateField HeaderText="位置图" Width="60px">
                        <ItemTemplate>
                            <%# Eval("MapImg").ToString().Length > 5 ? "<a href='" + Eval("MapImg").ToString() + "' target=\"_blank\" class='PicToolTip'><img src='" + DirFileHelper.GetFilePathPostfix(Eval("MapImg")+ "", "s") + "'></a>" : ""%>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField Width="150px" DataField="Name" DataFormatString="{0}" DataSimulateTreeLevelField="Depth" HeaderText="名称" />
                    <f:BoundField DataField="Keyword" HeaderText="关键字" Width="100px" />
                    <f:BoundField DataField="Width" HeaderText="宽" />
                    <f:BoundField DataField="Height" HeaderText="高" />
                    <f:TemplateField HeaderText="排序" Width="100px">
                        <ItemTemplate>
                            <asp:TextBox ID="tbSort" runat="server" Width="50px" Text='<%# Eval("Sort") %>' AutoPostBack="false"></asp:TextBox>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:LinkButtonField HeaderText="是否显示" Icon="BulletCross" TextAlign="Center" ToolTip="点击修改是否显示" ColumnID="IsDisplay" CommandName="IsDisplay" />
                    <f:BoundField DataField="Depth" HeaderText="级别层次" TextAlign="Center" />
                    <f:LinkButtonField HeaderText="操作" TextAlign="Center" ToolTip="点击修改当前记录" ColumnID="ButtonEdit" CommandName="ButtonEdit" />
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text=""></f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id"></f:HiddenField>
        </items>
    </f:panel>
    <f:window id="Window1" width="370px" height="425px" icon="TagBlue" title="编辑" hidden="True"
        enablemaximize="True" closeaction="HidePostBack" onclose="Window1_Close" enablecollapse="true"
        runat="server" enableresize="true" bodypadding="5px" enableframe="True" iframeurl="about:blank"
        enableiframe="true" enableclose="true" ismodal="True" enableconfirmonclose="True">
    </f:window>
    </form>
</body>
</html>
