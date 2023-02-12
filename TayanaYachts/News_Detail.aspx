<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="News_Detail.aspx.cs" Inherits="TayanaYachts.News_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" style="border-radius: 5px;width:100%"/>

            </li>
        </ul>

    </div>
    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">

            <div class="left1">
                <p><span>NEWS</span></p>
                <ul>
                    <li><a href="#">News &amp; Events</a></li>

                </ul>



            </div>




        </div>







        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="index.aspx" style="color: #747474;text-decoration: none">Home</a> &gt;&gt; <a href="#" style="color: #747474;text-decoration: none">News </a>&gt;&gt; <a href="#" style="color:#34A9D4;text-decoration: none"><span class="on1">News &amp; Events</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title" style="padding-left:18px"><span>News &amp; Events</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box3">
                    <h4><span id="ctl00_ContentPlaceHolder1_title" ><asp:Literal ID="NewsTitle" runat="server"></asp:Literal></span></h4>
                    
                    <asp:Literal ID="NewsContent" runat="server"></asp:Literal>


                    <asp:Literal ID="NewsImages" runat="server"></asp:Literal>

                   
                    

                    <p>
                        &nbsp;
                    </p>

                </div>

                <!--下載開始-->

                <!--下載結束-->

                <div class="buttom001">
                    <a href="javascript:window.history.back();">
                        <img src="images/back.gif" alt="&quot;&quot;" width="55" height="28"/></a>
                </div>

                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
