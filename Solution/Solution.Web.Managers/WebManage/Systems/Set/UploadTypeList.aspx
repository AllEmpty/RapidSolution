<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadTypeList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Set.UploadTypeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传类型列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel1" runat="server" title="上传类型列表" enableframe="false" bodypadding="10px"
        enablecollapse="True">
        <toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonRefresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="ButtonRefresh_Click" CssClass="inline"></f:Button>
                    <f:Button ID="ButtonAdd" runat="server" Text="添加" Icon="Add" OnClick="ButtonAdd_Click"></f:Button>
                    <f:Button ID="ButtonDelete" runat="server" Text="删除" Icon="Delete" OnClick="ButtonDelete_Click" ConfirmTitle="删除提示" ConfirmText="是否删除记录？" 
                        OnClientClick="if (!F('Panel1_Grid1').getSelectionModel().hasSelection() ) { F.alert('请选择你想要删除的记录！'); return false; } ">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <items>
            <f:Grid ID="Grid1" Title="上传类型列表" EnableFrame="false" EnableCollapse="true" AllowSorting="true" IsDatabasePaging="True"
            PageSize="15" ShowBorder="true" ShowHeader="False" runat="server" EnableCheckBoxSelect="True" DataKeyNames="Id" EnableColumnLines="true" Width="780px"
            OnPageIndexChange="Grid1_PageIndexChange" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand" OnSort="Grid1_Sort">
                <Columns>
                    <f:BoundField Width="50px" DataField="Id" SortField="Id" HeaderText="ID" TextAlign="Center" />
                    <f:BoundField Width="100px" DataField="Name" SortField="Name" HeaderText="名称" />
                    <f:BoundField Width="100px" DataField="TypeKey" SortField="TypeKey" HeaderText="关键字" />
                    <f:BoundField Width="300px" DataField="Ext" HeaderText="绑定扩展名" /> 
                    <f:CheckBoxField Width="100px" RenderAsStaticField="true" DataField="IsSys" HeaderText="是否系统默认" ToolTip="系统默认记录，不能删除" />
                    <f:LinkButtonField Width="100px" HeaderText="操作" TextAlign="Center" ToolTip="点击修改当前记录" ColumnID="ButtonEdit" CommandName="ButtonEdit" />
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text=""></f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id"></f:HiddenField>
        </items>
    </f:panel>
    <f:window id="Window1" width="380px" height="300px" icon="TagBlue" title="编辑" hidden="True"
        enablemaximize="True" closeaction="HidePostBack" onclose="Window1_Close" enablecollapse="true"
        runat="server" enableresize="true" bodypadding="5px" enableframe="True" iframeurl="about:blank"
        enableiframe="true" enableclose="true" ismodal="True" enableconfirmonclose="True">
    </f:window>
    </form>
</body>
</html>
