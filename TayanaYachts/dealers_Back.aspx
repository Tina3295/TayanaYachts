<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="dealers_Back.aspx.cs" Inherits="TayanaYachts.dealers_Back" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="banner" style="margin-bottom: 20px">
            <ul>
                <li>
                    <img src="images/newbanner.jpg" alt="Tayana Yachts"></li>
            </ul>
        </div>

        <div style="margin: 0 20px">


            <div class="card mb-4 py-3 border-bottom-primary">
                <div class="bluetop">
                    <p>Add Country</p>
                </div>


                <div class="card-body">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                </div>
            </div>


            <div class="card mb-4 py-3 border-bottom-primary">
                <div class="bluetop">
                    <p>Add Country</p>
                </div>


                <div class="card-body">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="Button" />
                </div>
            </div>

        </div>
    </div>



</asp:Content>
