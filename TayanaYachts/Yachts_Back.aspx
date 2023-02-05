<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="Yachts_Back.aspx.cs" Inherits="TayanaYachts.Yachts_Back" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- Top start -->
    <div style="display: flex; justify-content: space-between">


        <!-- Country Start -->
        <div class="container-fluid" style="width: 40%; margin: unset">
            <div class="card shadow mb-4">

                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Yacht Model</h6>
                </div>

                <div class="card-body">
                    <div class="table-responsive">
                        <h6>Add Model:</h6>
                        <div style="display: flex; flex-direction:column">

                            <div>
                                <asp:TextBox ID="AddCountryTextBox" runat="server" width="150px"></asp:TextBox>
                                <asp:TextBox ID="TextBox1" runat="server" width="80px"></asp:TextBox>
                                <br />
                                <br />
                            </div>

                            <div style="display: flex; justify-content: space-between;align-items:center">
                                <div>
                                    <asp:CheckBox runat="server" Text="NewDesign"></asp:CheckBox>
                                    <br />
                                    <asp:CheckBox runat="server" Text="NewBuilding"></asp:CheckBox>
                                </div>
                                <div style="margin-bottom: 0.5rem;">
                                    <asp:Button ID="AddCountry" CssClass="bluebtn" runat="server" Text="Add" />
                                    <br />
                                </div>
                            </div>

                        </div>
                        <div Style="padding-bottom:5px">
                            <asp:Label ID="AddCountryTip" runat="server" Text="" ForeColor="Red" ></asp:Label>
                        </div>

                        <div style=" padding-top:15px;border-top:2px solid #4e73df">
                            <asp:RadioButtonList runat="server"></asp:RadioButtonList>
                        </div>





                    </div>
                </div>
            </div>
        </div>
        <!-- Country End -->









        <div style="width: 60%;display:flex;flex-direction:column">




        <!-- Area Start-->
        <div class="container-fluid" >
            <div class="card shadow mb-4">

                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Yacht Model Name</h6>
                </div>

                <div class="card-body">
                    <div class="table-responsive">

                        <div style="display: flex;align-items:center;justify-content:space-between;margin-bottom:20px">
                            <div style="">
                                <div>
                                    Yacht Model :
                                <asp:TextBox ID="TextBox2" runat="server" width="150px"></asp:TextBox>
                                    <asp:TextBox ID="TextBox3" runat="server" width="80px"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <div style="display: flex;flex-direction:column">
                                <div style="margin-right: 40px; display: flex; align-items: center">
                                    NewDesign :&nbsp;
                                    <asp:CheckBox runat="server"></asp:CheckBox>
                                </div>
                                <div style="display: flex; align-items: center">
                                    NewBuilding :&nbsp;
                                    <asp:CheckBox runat="server"></asp:CheckBox>
                                </div>
                            </div>


                        </div>


                        <div style="display:flex;justify-content:center;margin-bottom:10px">
                            
                            <asp:Button ID="Button1" CssClass="bluebtn" runat="server" Text="Update"  style="margin-right:130px"/>
                            <asp:Button ID="Button2" CssClass="redbtn" runat="server" Text="Delete" />
                        </div>

                       

                    </div>
                </div>
            </div>
        </div>
        <!--Area End-->













            <!-- Area Start-->
        <div class="container-fluid" >
            <div class="card shadow mb-4">

                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">Yacht Model Banner Images</h6>
                </div>

                <div class="card-body">
                    <div class="table-responsive">

   





                        <div style="border-bottom: 2px solid #4e73df; padding-bottom:5px">
                            <div style="display:flex;justify-content:space-between;align-items:center">
                                <div>
                                    Add Images: <asp:FileUpload runat="server"></asp:FileUpload>
                                </div>
                                <div>
                                    <asp:Button ID="Button6" CssClass="bluebtn" runat="server" Text="Add" />
                                </div>
                            </div>



                            <div style="margin-top:10px">
                                <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                        </div>






                        <div style=" padding-top: 10px">
                           Banner Images List:<br/>
                            <asp:RadioButtonList runat="server"></asp:RadioButtonList>
                            <asp:Button ID="Button7" CssClass="redbtn" runat="server" Text="Delete" /><br />                            
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <!--Area End-->







            </div>







    </div>
    <!-- Top end -->


</asp:Content>
