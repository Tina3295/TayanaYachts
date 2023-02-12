<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Company1.aspx.cs" Inherits="TayanaYachts.Company1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" style="border-radius: 5px; width: 100%" />
            </li>
        </ul>
    </div>





    <div class="conbg">
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">

            <div class="left1">
                <p><span>COMPANY </span></p>

                <ul>

                    <li><a href="Company.aspx" target="_self">About Us</a></li>

                    <li><a href="Company1.aspx" target="_self">Certificate</a></li>

                </ul>


            </div>

        </div>


        <!--------------------------------左邊選單結束---------------------------------------------------->

        <!--------------------------------右邊選單開始---------------------------------------------------->
        <div id="crumb"><a href="index.aspx" style="color: #747474; text-decoration: none">Home</a> &gt;&gt; <a href="#" style="color: #747474; text-decoration: none">Company  </a>&gt;&gt; <a href="#" style="color: #34A9D4; text-decoration: none"><span class="on1">Certificate</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title" ><span style="padding-left: 18px">Certificate</span></div>

                <!--------------------------------內容開始---------------------------------------------------->
                <div class="box3">
                    <asp:Literal ID="CertificateHTML" runat="server"></asp:Literal>


                    <div class="pit">
                        <ul style="display:flex;flex-wrap:wrap;justify-content:space-between;margin-top:20px">
                            
                            <asp:Literal ID="VerticalImg" runat="server"></asp:Literal>
                            <asp:Literal ID="HorizontalImg" runat="server"></asp:Literal>
                            
                        </ul>
                    </div>
                </div>




                <!--------------------------------內容結束------------------------------------------------------>
            </div>
        </div>

        <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>
</asp:Content>
