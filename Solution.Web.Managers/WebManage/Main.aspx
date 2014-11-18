<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Solution.Web.Managers.Main" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>从零开始编写自己的C#框架——后端管理系统</title>
    <style type="text/css">
        body.f-theme-neptune .header
        {
            background-color: #005999;
            border-bottom: 1px solid #1E95EC;
        }
        
        body.f-theme-neptune .header .x-panel-body
        {
            background-color: transparent;
        }
        
        body.f-theme-neptune .header .title a
        {
            font-weight: bold;
            font-size: 24px;
            text-decoration: none;
            line-height: 50px;
            margin-left: 10px;
        }
        .label
        {
            color: #80ACCC;
        }
        .content
        {
            color: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <f:PageManager ID="PageManager1" AutoSizePanelID="regionPanel" runat="server" />
    <f:Timer ID="Timer1" Interval="120" Enabled="false" OnTick="Timer1_Tick" runat="server">
    </f:Timer>
    <f:RegionPanel ID="regionPanel" ShowBorder="false" runat="server">
        <Regions>
            <f:Region ID="regionTop" ShowBorder="false" ShowHeader="false" Position="Top" Layout="Fit"
                runat="server">
                <Toolbars>
                    <f:Toolbar ID="Toolbar1" Position="Bottom" runat="server" CssClass="topbar content"
                        CssStyle="border-bottom: 1px solid #1E95EC;background-color: #005999;">
                        <Items>
                            <f:ToolbarText ID="ToolbarText1" Text="欢迎您：" runat="server" CssClass="label">
                            </f:ToolbarText>
                            <f:ToolbarText ID="txtUser" runat="server" CssClass="content">
                            </f:ToolbarText>
                            <f:ToolbarText ID="ToolbarText2" Text="部门：" runat="server" CssClass="label">
                            </f:ToolbarText>
                            <f:ToolbarText ID="txtBranchName" runat="server" CssClass="content">
                            </f:ToolbarText>
                            <f:ToolbarText ID="ToolbarText3" Text="职位：" runat="server" CssClass="label">
                            </f:ToolbarText>
                            <f:ToolbarText ID="txtPositionInfoName" runat="server" CssClass="content">
                            </f:ToolbarText>
                            <f:ToolbarText ID="ToolbarText4" Text="在线人数：" runat="server" CssClass="label">
                            </f:ToolbarText>
                            <f:ToolbarText ID="txtOnlineUserCount" runat="server" CssClass="content">
                            </f:ToolbarText>
                            <f:ToolbarFill ID="ToolbarFill1" runat="server" />
                            <f:Button ID="btnClearCache" runat="server" Icon="controlblank" Text="清除后端缓存" OnClick="btnClearCache_Click"
                                CssStyle="background-color: transparent;background-image: none !important;border-width: 0 !important;">
                            </f:Button>
                            <f:Button ID="btnCalendar" runat="server" Icon="Calendar" Text="万年历" EnablePostBack="false"
                                CssStyle="background-color: transparent;background-image: none !important;border-width: 0 !important;">
                            </f:Button>
                            <f:Button ID="btnHelp" EnablePostBack="false" Icon="Help" Text="帮助" runat="server"
                                CssStyle="background-color: transparent;background-image: none !important;border-width: 0 !important;">
                            </f:Button>
                            <f:Button ID="btnExit" runat="server" Icon="UserRed" Text="安全退出" ConfirmText="确定退出系统？"
                                OnClick="btnExit_Click" CssStyle="background-color: transparent;background-image: none !important;border-width: 0 !important;">
                            </f:Button>
                        </Items>
                    </f:Toolbar>
                </Toolbars>
            </f:Region>
            <f:Region ID="Region2" Split="true" Width="200px" ShowHeader="true" Title="菜单" EnableCollapse="true"
                Layout="Fit" Position="Left" runat="server">
                <Items>
                    <f:Tree runat="server" ShowBorder="false" ShowHeader="false" EnableArrows="true"
                        EnableLines="true" ID="leftMenuTree">
                    </f:Tree>
                </Items>
            </f:Region>
            <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" Position="Center" runat="server">
                <Items>
                    <f:TabStrip ID="mainTabStrip" EnableTabCloseMenu="true" ShowBorder="false" runat="server">
                        <Tabs>
                            <f:Tab ID="Tab1" Title="首页" Layout="Fit" Icon="House" runat="server">
                                <Items>
                                    <f:ContentPanel ID="ContentPanel2" ShowBorder="false" BodyPadding="10px" ShowHeader="false"
                                        AutoScroll="true" runat="server">
                                        <h2>
                                            从零开始编写自己的C#框架</h2>
                                        本框架由AllEmpty原创并发布于博客园，采用Apache License v2.0软件授权许可，欢迎大家试用。大家在使用时，请在软件源码中保留本人的相关版权信息，谢谢。
                                        <br />
                                        发表本框架源码，主要是为了和大家共同学习共同进步，如果你支持本系列文章的继续发表或有更好的建议，请对相关文章回复你的看法与点击推荐，有兴趣的朋友还可以加加Q群：327360708
                                        ，大家一起探讨。
                                        <br />
                                        更多内容，敬请关注博客：<a href="http://www.cnblogs.com/EmptyFS/" target="_blank">http://www.cnblogs.com/EmptyFS/</a>
                                        <br />
                                        <br />
                                        <a href="http://www.cnblogs.com/EmptyFS/tag/%E4%BB%8E%E9%9B%B6%E5%BC%80%E5%A7%8B%E7%BC%96%E5%86%99%E8%87%AA%E5%B7%B1%E7%9A%84C%23%E6%A1%86%E6%9E%B6/"
                                            target="_blank">从零开始编写自己的C#框架章节目录</a>
                                        <br />
                                        <br />
                                        <br />
                                        <h2>
                                            使用技术</h2>
                                        <br />
                                        本框架使用ASP.NET（C#）、MsSql、SubSonic3.0、FineUI、Linq、T4模板、IIS缓存等相关技术
                                        <br />
                                        <br />
                                        <a href="https://github.com/subsonic" target="_blank">SubSonic</a>是<span style="font-size: 16px;"><a
                                            href="http://www.wekeroad.com/" target="_blank">Rob Conery</a><span style="font-size: 16px;">用c#语言写的</span></span>一
                                        个ORM开源框架，使用BSD软件授权许可（The BSD 3-Clause License）。它是一个实用的快速开发框架，通过非常简单的配置，以及附带的T4模板，就可以帮我们生成功能强大的数据访问层工具，让开发人员远离SQL语句的拼接，专注于业务逻辑的开发。
                                        <br />
                                        <a target="_blank" style="font-weight: bold;" href="http://fineui.com/">FineUI</a>是<a
                                            href="http://cnblogs.com/sanshi/" target="_blank">三生石上</a> 和 <a href="http://www.codeplex.com/site/users/view/RingoDing"
                                                target="_blank">RingoDing</a> 创建并维护。使用Apache License v2.0软件授权许可（ExtJS 库在
                                        <a target="_blank" href="http://www.sencha.com/license">GPL v3</a> 协议下发布）。它基于 jQuery
                                        / ExtJS 的 ASP.NET 控件库，创建 No JavaScript，No CSS，No UpdatePanel，No ViewState，No WebServices
                                        的网站应用程序
                                        <br />
                                        <br />
                                        <h2>
                                            支持的浏览器</h2>
                                        IE 8.0+、Chrome、Firefox、Opera、Safari
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        注：本框架不内置 ExtJS 库，请自行下载ExtJS 库后手工添加：<a target="_blank" href="http://fineui.com/bbs/forum.php?mod=viewthread&tid=3218">http://fineui.com/bbs/forum.php?mod=viewthread&tid=3218</a>
                                    </f:ContentPanel>
                                </Items>
                            </f:Tab>
                        </Tabs>
                    </f:TabStrip>
                </Items>
            </f:Region>
        </Regions>
    </f:RegionPanel>
    <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" EnableIFrame="true"
        EnableResize="true" EnableMaximize="true" IFrameUrl="about:blank" Width="650px"
        Height="450px">
    </f:Window>
    </form>
    <script>
        var menuClientID = '<%= leftMenuTree.ClientID %>';
        var tabStripClientID = '<%= mainTabStrip.ClientID %>';

        // 页面控件初始化完毕后，会调用用户自定义的onReady函数
        F.ready(function () {

            // 初始化主框架中的树(或者Accordion+Tree)和选项卡互动，以及地址栏的更新
            // treeMenu： 主框架中的树控件实例，或者内嵌树控件的手风琴控件实例
            // mainTabStrip： 选项卡实例
            // createToolbar： 创建选项卡前的回调函数（接受tabConfig参数）
            // updateLocationHash: 切换Tab时，是否更新地址栏Hash值
            // refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // refreshWhenTabChange: 切换选项卡时，是否刷新内部IFrame
            F.util.initTreeTabStrip(F(menuClientID), F(tabStripClientID), null, true, false, false);

        });
    </script>
</body>
</html>
