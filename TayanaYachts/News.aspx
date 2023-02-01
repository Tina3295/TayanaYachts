<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="TayanaYachts.News" %>

<%@ Register Src="~/Pagination.ascx" TagPrefix="uc1" TagName="Pagination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <div style="padding: 115px 20px 0 20px">
            <ul>
                <li>
                    <img src="images/newbanner.jpg" alt="Tayana Yachts" width="100%" style="border-radius: 5px"></li>
            </ul>
        </div>







        <div class="conbg">
            <!--------------------------------左邊選單開始---------------------------------------------------->
            <div class="left">
                <div class="left1">
                    <p>
                        <span>NEWS</span>
                    </p>
                    <ul>
                        <li><a href="#">News &amp; Events</a></li>
                    </ul>
                </div>
            </div>
            <!--------------------------------左邊選單結束---------------------------------------------------->
            <!--------------------------------右邊選單開始---------------------------------------------------->
            <div id="crumb">
                <a href="index.aspx" style="text-decoration: none; color: #747474;">Home</a> &gt;&gt; <a href="#" style="text-decoration: none; color: #747474;">News </a>&gt;&gt; <a href="#" style="color: #34A9D4; text-decoration: none"><span class="on1">News &amp;
                    Events</span></a>
            </div>
            <div class="right">
                <div class="right1">
                    <div class="title">
                        <span style="margin-left: 18px">News &amp; Events</span>
                    </div>
                    <!--------------------------------內容開始---------------------------------------------------->
                    <div class="box2_list">

                        <ul>





                            <asp:Literal ID="NewsList" runat="server"></asp:Literal>



                        </ul>

                    </div>
                    <div class="pagenumber">
                        <!--    <div class="pagination">共<span style="color:red">54</span>筆資料<span class="disabled">上一頁</span><span class="current">1</span><a href="new_list.aspx?page=2">2</a><a href="new_list.aspx?page=3">3</a><a href="new_list.aspx?page=4">4</a><a href="new_list.aspx?page=5">5</a><a href="new_list.aspx?page=6">6</a><a href="new_list.aspx?page=2">下一頁</a></div>-->

                        <uc1:Pagination runat="server" ID="Pagination" class="pagination" />
                    </div>



                    <!--------------------------------內容結束------------------------------------------------------>
                </div>
            </div>
            <!--------------------------------右邊選單結束---------------------------------------------------->
        </div>





    </div>
</asp:Content>
