<%@ Page Title="" Language="C#" MasterPageFile="~/Tayana_Gallery.Master" AutoEventWireup="true" CodeBehind="Yachts_Layout.aspx.cs" Inherits="TayanaYachts.Yachts_Layout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box6">
        <p>Layout &amp; deck plan</p>
        <ul>
            <asp:Literal ID="LayoutImg" runat="server"></asp:Literal>
        </ul>
    </div>
</asp:Content>
