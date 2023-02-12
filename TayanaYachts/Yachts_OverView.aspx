<%@ Page Title="" Language="C#" MasterPageFile="~/Tayana_Gallery.Master" AutoEventWireup="true" CodeBehind="Yachts_OverView.aspx.cs" Inherits="TayanaYachts.Yachts_OverView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!--次選單-->
    <div class="box1">
        <asp:Label ID="OverviewContent" runat="server" Text=""></asp:Label>
    </div>
    <div class="box3">
        <h4>
            <asp:Label ID="ModelNum" runat="server" Text=""></asp:Label>
            DIMENSIONS</h4>
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
            <img src="images/downloads.gif" alt="&quot;&quot;" />
        </p>

        <ul>
            <asp:Literal ID="Attachment" runat="server"></asp:Literal>


        </ul>

    </div>
    <!--下載結束-->



</asp:Content>
