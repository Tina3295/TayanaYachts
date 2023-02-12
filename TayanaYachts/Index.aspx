<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TayanaYachts.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/websitejquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">
        $(function () {

            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 4000;


            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式

                $(this).addClass('on').siblings('.on').removeClass('on');
                var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().eq($(this).index());
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
                if (thisLi.children('input[type=hidden]').val() == 1) {
                    thisLi.children('.new').show();
                }

            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });

            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);

            //相簿輪撥初始值設定
            $('.title ul li:eq(0)').addClass('on');
            var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0) li:eq(0)');
            thisLi.addClass('on');
            if (thisLi.children('input[type=hidden]').val() == 1) {
                thisLi.children('.new').show();
            }

            //最新消息TOP
            $('.newstop').each(function () {
                if ($(this).nextAll('input[type=hidden]').val() == 1) {
                    $(this).show();
                }
            });
        });


    </script>

    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-30943877-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://web.archive.org/web/20170716031852/https://ssl' : 'https://web.archive.org/web/20170716031852/http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bannermasks" style="top: 73px">
        <img src="images/banner00_masks2.png" alt="&quot;&quot;" />
    </div>



    <div id="abgne-block-20110111">
        <div class="bd">
            <div class="banner">

                <ul>
                    <asp:Literal ID="Banner" runat="server"></asp:Literal>











                </ul>

                <!--小圖開始-->
                <div class="bannerimg title" style="display: none">

                    <ul>
                        <asp:Literal ID="BannerNum" runat="server"></asp:Literal>






                    </ul>



                </div>
                <!--小圖結束-->
            </div>
        </div>
    </div>




















    <div class="news">
        <div class="newstitle">
            <p class="newstitlep1">
                <img src="images/news.gif" alt="news">
            </p>
            <p class="newstitlep2">
                <a href="News.aspx">More&gt;&gt;</a>
            </p>
        </div>

        <ul>




            <asp:Repeater ID="NewsRepeater" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="news01" >

                            <div class="newstop" style="display:<%#Eval("IsTop").ToString()=="True"?"block":"none"%>">
                                <img src="images/new_top01.png" alt="&quot;&quot;">
                            </div>

                            <div class="news02p1">
                                <p class="news02p1img">
                                    <img id="Repeater3_ctl02_Image1" alt="" src="images/<%#Eval("Thumbnail") %>" border="0"/>
                                </p>
                            </div>
                            <p class="news02p2">
                                <span><font color="#02a5b8"><%#Eval("Date") %></font></span>
                                <span><a href="News_Detail.aspx?id=<%# Eval("Guid").ToString().Trim()%>"><%#Eval("NewsTitle") %></a></span>
                            </p>
                            <input type="hidden" value="0">
                        </div>

                    </li>
                </ItemTemplate>
            </asp:Repeater>


        </ul>

    </div>

</asp:Content>
