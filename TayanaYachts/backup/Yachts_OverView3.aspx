<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Yachts_OverView3.aspx.cs" Inherits="TayanaYachts.Yachts_OverView3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.slideshow').cycle({
                fx: 'fade' // choose your transition type, ex: fade, scrollUp, shuffle, etc...
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $('.topbuttom').click(function () {
                $('html, body').scrollTop(0);

            });

        });
    </script>

    <script type="text/javascript" src="Scripts/jquery.ad-gallery.js"></script>
    <script src="//archive.org/includes/analytics.js?v=cf34f82" type="text/javascript"></script>
    <script type="text/javascript">window.addEventListener('DOMContentLoaded', function () { var v = archive_analytics.values; v.service = 'wb'; v.server_name = 'wwwb-app209.us.archive.org'; v.server_ms = 525; archive_analytics.send_pageview({}); });</script>

    <script type="text/javascript">
        __wm.init("https://web.archive.org/web");
        __wm.wombat("http://www.tayanaworld.com:80/Yachts_OverView.aspx?id=6d245b62-ff07-463b-95b3-277f0e5aac25", "20170920135936", "https://web.archive.org/", "web", "/_static/",
            "1505915976");
    </script>

    <!-- End Wayback Rewrite JS Include -->

    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <!--[if lt IE 7]>
<script type="text/javascript" src="javascript/iepngfix_tilebg.js"></script>
<![endif]-->


    <script type="text/javascript">
        $(function () {
            $('.topbuttom').click(function () {
                $('html, body').scrollTop(0);

            });

        });
    </script>

    <script type="text/javascript" src="Scripts/jquery.ad-gallery.js"></script>

    <script type="text/javascript">
        $(function () {

            var galleries = $('.ad-gallery').adGallery();
            galleries[0].settings.effect = 'fade';
            if ($('.banner input[type=hidden]').val() == "0") {
                $(".bannermasks").hide();
                $(".banner").hide();
                $("#crumb").css("top", "125px");
            }

        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bannermasks" >
        <img src="images/banner01_masks (1).png" alt="&quot;&quot;" />
    </div>


    <div class="banner1">
        <input type="hidden" name="ctl00$ContentPlaceHolder1$Gallery1$HiddenField1" id="ctl00_ContentPlaceHolder1_Gallery1_HiddenField1" value="1">

        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
                <div class="ad-image" style="width: 274.248px; height: 371px; top: 0px; left: 346.376px; opacity: 1;">
                    <img src="upload/Images/20120613095959.jpg" width="274" height="371">
                </div>

            </div>
            <div class="ad-controls">
            </div>

            <div class="ad-nav">
                <div class="ad-back" style="opacity: 0.6;"></div>
                <div class="ad-thumbs">

                    <ul class="ad-thumb-list" style="width: 1570px;">
                        <asp:Repeater ID="RepeaterImg" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%#Eval("ImageURL") %>'>
                                        <img src='<%#Eval("ImageURL") %>' alt="" class="image0" height="59px" />
                                    </a>
                                </li>


                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>

                </div>
                <div class="ad-forward" style="opacity: 0.6"></div>
            </div>
        </div>

    </div>


    <!---
    <div class="conbg" style="background-color:aquamarine;width:100%;height:550px;background-image: url('../images/con_bg.gif');z-index: 3;">
        </div>
        ------->






    <div class="conbg" style="margin-top:104px">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">

            <div class="left1">
                <p><span>YACHTS</span></p>

                <ul>
                    <asp:Literal ID="ModelMenu" runat="server"></asp:Literal>

                </ul>

            </div>




        </div>







        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb" style="margin-top:75px"><a href="index.aspx" style="color: #747474; text-decoration: none">Home</a> &gt;&gt; <a href="#" style="color: #747474; text-decoration: none">Yachts</a> &gt;&gt; <a href="Yachts_OverView.aspx"  style="text-decoration:none"><asp:Label ID="LittleTitle"  runat="server" Text="" Class="rightguide"></asp:Label></a></div>
        <div class="right">
            <div class="right1">
                <div class="title">
                    <asp:Label ID="ModelName" runat="server" Text="" Style="padding-left:21px"></asp:Label>
                </div>

                <!--------------------------------內容開始---------------------------------------------------->

                <!--次選單-->
                <div class="menu_y">
                    <ul>
                        <li class="menu_y00">YACHTS</li>
                        <asp:Label ID="Menu" runat="server" Text=""></asp:Label>
                    </ul>
                </div>
                <!--次選單-->
                <div class="box1">  
                    <asp:Label ID="OverviewContent" runat="server" Text=""></asp:Label>
                </div>
                <div class="box3">
                    <h4><asp:Label ID="ModelNum" runat="server" Text=""></asp:Label> DIMENSIONS</h4>
                    <div class="table02">
                       <asp:Literal ID="OverviewDimensions" runat="server"></asp:Literal>
                    </div>
                </div>


                <p class="topbuttom">
                    <img alt="top" src="images/top.gif" />
                </p>

                <!--下載開始-->
                <div id="ctl00_ContentPlaceHolder1_divDownload" class="downloads">
                    <p>
                        <img src="images/downloads.gif" alt="&quot;&quot;"/>
                    </p>

                    <ul>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        

                    </ul>

                </div>
                <!--下載結束-->


                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
