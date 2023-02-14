<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="TayanaYachts.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/loading.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="AdminTip" runat="server" Text=""></asp:Label>
    <asp:Panel ID="AdministratorOnly" runat="server">





        <div class="container-fluid">
            <!-- DataTales Example -->
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Add User</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive"style="display:flex;justify-content:space-between;align-items:center">


                        <div>
                            Email :
                            <asp:TextBox ID="Email" runat="server" Width="450px" placeholder="Email"></asp:TextBox>
                            Password :
                            <asp:TextBox ID="Password" runat="server" placeholder="Password" ></asp:TextBox><br/><br/>
                            Name :
                            <asp:TextBox ID="FirstName" runat="server" placeholder="First Name"></asp:TextBox>
                            <asp:TextBox ID="LastName" runat="server" placeholder="Last Name"></asp:TextBox>
                            <asp:Label ID="Tip" runat="server" Text="" ForeColor="Red"></asp:Label>
  
                        </div>

                        <asp:Button ID="RegisterAccount" runat="server" Text="Register" class="bluebtn" OnClick="RegisterAccount_Click" OnClientClick="$('#Load').show();" />

                    </div>



                    <div id="Load" class="preloader" style="margin-top: 15px;display:none">
                        Loading<div class="circ1"></div>
                        <div class="circ2"></div>
                        <div class="circ3"></div>
                        <div class="circ4"></div>
                    </div>






                </div>
            </div>

        </div>




















        <!-- Begin Page Content -->
        <div class="container-fluid">
            <!-- DataTales Example -->
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">User Management</h6>
                </div>
                <div class="card-body">
                    <div class="table-responsive">


                        <div>
                            <asp:GridView ID="GridView1" class="table table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" EnableViewState="False" OnDataBound="GridView1_DataBound" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="編號" SortExpression="Email">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email" SortExpression="Email">
                                        <EditItemTemplate>
                                            <asp:Label ID="EmailNoEdit" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Email" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="FirstNameEdit" runat="server" Text='<%# Bind("FirstName") %>' Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="FirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="LastName" SortExpression="LastName">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="LastNameEdit" runat="server" Text='<%# Bind("LastName") %>' Width="80px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UserIdentity" SortExpression="UserIdentity">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="UserIdentityEdit" runat="server">
                                                <asp:ListItem Value="Top">Top Administrator</asp:ListItem>
                                                <asp:ListItem Value="Middle">Middle Administrator</asp:ListItem>
                                                <asp:ListItem Value="General">General Administrator</asp:ListItem>
                                            </asp:DropDownList><br />
                                            <br />
                                            <asp:Label ID="tip" runat="server" Text=""></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="UserIdentity" runat="server" Text='<%# Bind("Permission") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UserIdentity" SortExpression="UserIdentity">
                                        <ItemTemplate>
                                            <asp:Label ID="InitDate" runat="server" Text='<%# Bind("InitDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="Renew" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton><br />
                                            <br />
                                            <asp:LinkButton ID="Scrap" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" ForeColor="Red" OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;"></asp:LinkButton>
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
