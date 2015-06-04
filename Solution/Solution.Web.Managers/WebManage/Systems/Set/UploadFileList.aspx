<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFileList.aspx.cs" Inherits="Solution.Web.Managers.WebManage.Systems.Set.UploadFileList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>上传文件列表</title>
    <style type="text/css">
        .expander
        {
            padding: 5px;
        }
        .expander p
        {
            padding: 5px;
        }
        .expander strong
        {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:pagemanager id="PageManager1" runat="server" />
    <f:panel id="Panel1" runat="server" title="上传文件列表" enableframe="false" bodypadding="10px"
        enablecollapse="True">
        <toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonRefresh" runat="server" Text="刷新" Icon="ArrowRefresh" OnClick="ButtonRefresh_Click" CssClass="inline"></f:Button>
                    <f:Button ID="ButtonDelete" runat="server" Text="删除" Icon="Delete" OnClick="ButtonDelete_Click" ConfirmTitle="删除提示" ConfirmText="是否删除记录？" 
                        OnClientClick="if (!F('Panel1_Grid1').getSelectionModel().hasSelection() ) { F.alert('请选择你想要删除的记录！'); return false; } ">
                    </f:Button>
                    <f:Button ID="ButtonImageRegenerate" runat="server" Text="图片全部重新生成" Icon="Disk" OnClick="ButtonImageRegenerate_Click" ConfirmTitle="提示" ConfirmText="图片是否全部重新生成？"></f:Button>
                </Items>
            </f:Toolbar>
        </toolbars>
        <items>
            <f:Grid ID="Grid1" Title="上传文件列表" EnableFrame="False" EnableCollapse="true" AllowSorting="true" IsDatabasePaging="True" Width="950px"
            PageSize="15" ShowBorder="true" ShowHeader="False" runat="server" EnableCheckBoxSelect="True" DataKeyNames="Id" EnableColumnLines="true"
            OnPageIndexChange="Grid1_PageIndexChange" OnPreRowDataBound="Grid1_PreRowDataBound" OnSort="Grid1_Sort">
                <Columns>
                    <f:BoundField Width="50px" DataField="Id" SortField="Id" HeaderText="ID" TextAlign="Center" />
                    <f:TemplateField RenderAsRowExpander="true">
                        <ItemTemplate>
                            <div class="expander">
                                <p>
                                    <strong>路径：</strong><%# Eval("Path")%></p>
                                <p>
                                    <strong>文件：</strong>
                                    <a href="<%# Eval("Path") %>" target="_blank">
                                    <%# CheckPic(Eval("Ext"), Eval("Path"))%>
                                    </a>
                                </p>
                            </div>
                        </ItemTemplate>
                    </f:TemplateField>
                    <f:BoundField Width="160px" DataField="Name" SortField="Name" HeaderText="名称" />
                    <f:BoundField Width="80px" DataField="Ext" SortField="Ext" HeaderText="扩展名" /> 
                    <f:BoundField Width="80px" DataField="Size" SortField="Size" HeaderText="文件大小" /> 
                    <f:BoundField Width="80px" DataField="JoinName" SortField="JoinName" HeaderText="关联表名" /> 
                    <f:BoundField Width="50px" DataField="JoinId" SortField="JoinId" HeaderText="关联Id" /> 
                    <f:LinkButtonField HeaderText="用户类型" ColumnID="UserType" CommandName="UserType" />
                    <f:BoundField Width="80px" DataField="UserName" SortField="UserId" HeaderText="上传用户" /> 
                    <f:BoundField Width="100px" DataField="UserIp" SortField="UserIp" HeaderText="Ip" /> 
                    <f:BoundField Width="140px" DataField="AddDate" SortField="AddDate" HeaderText="上传时间" /> 
                </Columns>
            </f:Grid>
            <f:Label runat="server" ID="lblSpendingTime" Text=""></f:Label>
            <f:HiddenField runat="server" ID="SortColumn" Text="Id"></f:HiddenField>
        </items>
    </f:panel>
    </form>
</body>
</html>
