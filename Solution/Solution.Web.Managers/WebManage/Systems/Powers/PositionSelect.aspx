<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PositionSelect.aspx.cs"
    Inherits="Solution.Web.Managers.WebManage.Systems.Powers.PositionSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>职位选择</title>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" runat="server" />
    <f:Panel ID="Panel1" runat="server" EnableFrame="false" BodyPadding="0px" EnableCollapse="True"
        ShowHeader="False">
        <Toolbars>
            <f:Toolbar ID="toolBar" runat="server">
                <Items>
                    <f:Button ID="ButtonSelect" runat="server" Text="确定选择" Icon="Disk" OnClick="ButtonSelect_Click">
                    </f:Button>
                </Items>
            </f:Toolbar>
        </Toolbars>
        <Items>
            <f:Panel ID="Panel2" Title="选择职位" runat="server" BodyPadding="0px" ShowBorder="False"
                ShowHeader="True">
                <Items>
                    <f:Form ID="Form6" ShowBorder="True" BodyPadding="1px" ShowHeader="False" runat="server">
                        <Rows>
                            <f:FormRow ID="FormRow2" runat="server">
                                <Items>
                                    <f:DropDownList Label="所属部门" AutoPostBack="true" CompareType="String"
                                        EnableSimulateTree="true" runat="server" ID="ddlBranch" Width="250px"
                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    </f:DropDownList>
                                </Items>
                            </f:FormRow>
                        </Rows>
                    </f:Form>
                    <f:Panel ID="Panel9" runat="server" BodyPadding="0px" ShowBorder="True" ShowHeader="False" Layout="Column">
                        <Items>
                            <f:Panel ID="Panel5" Width="200px" ShowHeader="false" BodyPadding="5px" ShowBorder="false"
                                runat="server">
                                <Items>
                                    <f:Grid ID="Grid1" EnableFrame="false" EnableCollapse="true" ShowBorder="true" ShowHeader="False"
                                        AllowPaging="False" runat="server" DataKeyNames="Id" Width="180px" Height="250px">
                                        <Columns>
                                            <f:BoundField Width="150px" DataField="Name" SortField="Name" HeaderText="未绑定职位列表" />
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel7" Width="50px" Height="250px" ShowHeader="false" ShowBorder="false"
                                runat="server" Layout="VBox">
                                <Items>
                                    <f:Label ID="Label1" runat="server" Height="70px">
                                    </f:Label>
                                    <f:Button ID="ButtonEmpower" runat="server" Text="&nbsp; > > &nbsp;" OnClick="ButtonEmpower_Click">
                                    </f:Button>
                                    <f:Label ID="Label2" runat="server" Height="20px">
                                    </f:Label>
                                    <f:Button ID="ButtonCancel" runat="server" Text="&nbsp; < < &nbsp;" OnClick="ButtonCancel_Click">
                                    </f:Button>
                                    <f:Label ID="Label3" runat="server" Height="20px">
                                    </f:Label>
                                    <f:Button ID="ButtonEmpty" runat="server" Text="清 空" OnClick="ButtonEmpty_Click">
                                    </f:Button>
                                </Items>
                            </f:Panel>
                            <f:Panel ID="Panel6" Width="200px" ShowHeader="false" BodyPadding="5px" ShowBorder="false"
                                runat="server">
                                <Items>
                                    <f:Grid ID="Grid2" EnableFrame="false" EnableCollapse="true" ShowBorder="true" ShowHeader="False"
                                        AllowPaging="False" runat="server" DataKeyNames="Id" Width="180px" Height="250px">
                                        <Columns>
                                            <f:BoundField Width="150px" DataField="Name" SortField="Name" HeaderText="已绑定职位列表" />
                                        </Columns>
                                    </f:Grid>
                                </Items>
                            </f:Panel>
                        </Items>
                    </f:Panel>
                </Items>
            </f:Panel>
            <f:HiddenField ID="hidPositionId" runat="server">
            </f:HiddenField>
        </Items>
    </f:Panel>
    </form>
</body>
</html>
