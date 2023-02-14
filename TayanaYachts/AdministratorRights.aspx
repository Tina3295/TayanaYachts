<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="AdministratorRights.aspx.cs" Inherits="TayanaYachts.AdministratorRights" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="tip" runat="server" Text=""></asp:Label>
    <asp:Panel ID="AdministratorOnly" runat="server">


        <!-- Begin Page Content -->
        <div class="container-fluid">


            <!-- DataTales Example -->
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Administrator Rights</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">


                        <div style="text-align:center">
                            <asp:GridView ID="GridView1" class="table table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID"   EnableViewState="False" >
                                <Columns>
                                    <asp:TemplateField HeaderText="編號" SortExpression="Email">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email" SortExpression="Email" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Email" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Yachts" >
                                        <ItemTemplate>
                                            <asp:ImageButton ID="YachtsYes" runat="server" Visible='<%# Eval("Yachts").ToString()=="True"?Convert.ToBoolean("True"):Convert.ToBoolean("False") %>' Width="20px" ImageUrl="~/images/check.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=Yachts&admin="+Eval("Yachts")%>' />
                                            <asp:ImageButton ID="YachtsNo" runat="server" Visible='<%# Eval("Yachts").ToString()=="True"?Convert.ToBoolean("False"):Convert.ToBoolean("True") %>' Width="20px" ImageUrl="~/images/remove.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=Yachts&admin="+Eval("Yachts")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="News">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="NewsYes" runat="server" Visible='<%# Eval("News").ToString()=="True"?Convert.ToBoolean("True"):Convert.ToBoolean("False") %>' Width="20px" ImageUrl="~/images/check.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=News&admin="+Eval("News")%>' />
                                            <asp:ImageButton ID="NewsNo" runat="server" Visible='<%# Eval("News").ToString()=="True"?Convert.ToBoolean("False"):Convert.ToBoolean("True") %>' Width="20px" ImageUrl="~/images/remove.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=News&admin="+Eval("News")%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Company">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="CompanyYes" runat="server" Visible='<%# Eval("Company").ToString()=="True"?Convert.ToBoolean("True"):Convert.ToBoolean("False") %>' Width="20px" ImageUrl="~/images/check.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=Company&admin="+Eval("Company")%>' />
                                            <asp:ImageButton ID="CompanyNo" runat="server" Visible='<%# Eval("Company").ToString()=="True"?Convert.ToBoolean("False"):Convert.ToBoolean("True") %>' Width="20px" ImageUrl="~/images/remove.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=Company&admin="+Eval("Company")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dealers">
                                        <ItemTemplate>
                                             <asp:ImageButton ID="DealersYes" runat="server" Visible='<%# Eval("Dealers").ToString()=="True"?Convert.ToBoolean("True"):Convert.ToBoolean("False") %>' Width="20px" ImageUrl="~/images/check.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=Dealers&admin="+Eval("Dealers")%>' />
                                            <asp:ImageButton ID="DealersNo" runat="server" Visible='<%# Eval("Dealers").ToString()=="True"?Convert.ToBoolean("False"):Convert.ToBoolean("True") %>' Width="20px" ImageUrl="~/images/remove.png" PostBackUrl='<%# "AdministratorRights.aspx?id="+Eval("UserID")+"&system=Dealers&admin="+Eval("Dealers")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                 




                                </Columns>
                            </asp:GridView>

                        </div>




                    </div>
                </div>
            </div>

        </div>
        <!-- /.container-fluid -->
    </asp:Panel>
    <!-- End of Main Content -->
</asp:Content>
