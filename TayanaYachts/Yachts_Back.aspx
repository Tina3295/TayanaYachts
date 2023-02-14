<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="Yachts_Back.aspx.cs" Inherits="TayanaYachts.Yachts_Back" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Tip" runat="server" Text=""></asp:Label>
    <asp:Panel ID="AdminOnly" runat="server">



        <!-- Top start -->
        <div style="display: flex; justify-content: space-between">


            <!-- Model Start -->
            <div class="container-fluid" style="width: 40%; margin: unset">
                <div class="card shadow mb-4">

                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Yacht Model</h6>
                    </div>

                    <div class="card-body">
                        <div class="table-responsive">
                            <h6>Add Model:</h6>
                            <div style="display: flex; flex-direction: column">

                                <div>
                                    <asp:TextBox ID="AddModelTextBox" runat="server" Width="150px" placeholder="Model"></asp:TextBox>
                                    <asp:TextBox ID="AddModelNumTextBox" runat="server" Width="80px" placeholder="Length"></asp:TextBox>
                                    <br />
                                    <br />
                                </div>

                                <div style="display: flex; justify-content: space-between; align-items: center">
                                    <div>
                                        <asp:CheckBox ID="SetNewDesign" runat="server" Text="NewDesign"></asp:CheckBox>
                                        <br />
                                        <asp:CheckBox ID="SetNewBuilding" runat="server" Text="NewBuilding"></asp:CheckBox>
                                    </div>
                                    <div style="margin-bottom: 0.5rem;">
                                        <asp:Button ID="AddModel" CssClass="bluebtn" runat="server" Text="Add" OnClick="AddModel_Click" />
                                        <br />
                                    </div>
                                </div>

                            </div>
                            <div style="padding-bottom: 5px">
                                <asp:Label ID="AddModelTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>

                            <div style="padding-top: 15px; border-top: 2px solid #4e73df">
                                <asp:RadioButtonList ID="ModelRadioBtnList" runat="server" OnSelectedIndexChanged="ModelRadioBtnList_SelectedIndexChanged" AutoPostBack="True"></asp:RadioButtonList>
                            </div>





                        </div>
                    </div>
                </div>
            </div>
            <!-- Model End -->









            <div style="width: 60%; display: flex; flex-direction: column">

                <asp:Panel ID="ModelUpdatePanel" runat="server" Visible="false">


                    <!-- Model Name Start-->
                    <div class="container-fluid">
                        <div class="card shadow mb-4">

                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Yacht Model Name</h6>
                            </div>

                            <div class="card-body">
                                <div class="table-responsive">

                                    <div style="display: flex; align-items: center; justify-content: space-between; margin-bottom: 20px">
                                        <div style="">
                                            <div>
                                                Yacht Model :
                                        <asp:TextBox ID="ModelName" runat="server" Width="150px"></asp:TextBox>
                                                <asp:TextBox ID="ModelNum" runat="server" Width="80px"></asp:TextBox>
                                            </div>
                                            <div>
                                                <asp:Label ID="UpdateModelTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                        <div style="display: flex; flex-direction: column">
                                            <div style="margin-right: 40px; display: flex; align-items: center">
                                                NewDesign :&nbsp;
                                    <asp:CheckBox ID="NewDesign" runat="server"></asp:CheckBox>
                                            </div>
                                            <div style="display: flex; align-items: center">
                                                NewBuilding :&nbsp;
                                    <asp:CheckBox ID="NewBuilding" runat="server"></asp:CheckBox>
                                            </div>
                                        </div>


                                    </div>


                                    <div style="display: flex; justify-content: center; margin-bottom: 10px">

                                        <asp:Button ID="UpdateModelBtn" CssClass="bluebtn" runat="server" Text="Update" Style="margin-right: 130px" OnClick="UpdateModelBtn_Click" />
                                        <asp:Button ID="DeleteModelBtn" CssClass="redbtn" runat="server" Text="Delete" OnClick="DeleteModelBtn_Click" OnClientClick="return confirm('Are you sure you want to delete？')" />
                                    </div>



                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Model Name End-->













                    <!-- Model Banner Images Start-->
                    <div class="container-fluid">
                        <div class="card shadow mb-4">

                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">Yacht Model Banner Images</h6>
                            </div>

                            <div class="card-body">
                                <div class="table-responsive">







                                    <div style="border-bottom: 2px solid #4e73df; padding-bottom: 5px">
                                        <div style="display: flex; justify-content: space-between; align-items: center">
                                            <div>
                                                Add Images:
                                        <asp:FileUpload ID="AddImagesFileUpload" runat="server" AllowMultiple="True"></asp:FileUpload>
                                            </div>
                                            <div>
                                                <asp:Button ID="AddImagesBtn" CssClass="bluebtn" runat="server" Text="Add" OnClick="AddImagesBtn_Click" />
                                            </div>
                                        </div>



                                        <div style="margin-top: 10px">
                                            <asp:Label ID="AddImagesTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>






                                    <div style="padding-top: 10px">
                                        Banner Images List:<p style="display: inline-block; color: white; border-radius: 10px; background-color: #36b9cc; margin-left: 15px; font-size: 8px">&nbsp;* Home page banner&nbsp;</p>
                                        <br />
                                        <asp:RadioButtonList ID="BannerImagesRadioButtonList" runat="server" CellPadding="10" RepeatColumns="3" AutoPostBack="True" OnSelectedIndexChanged="BannerImagesRadioButtonList_SelectedIndexChanged" RepeatDirection="Horizontal"></asp:RadioButtonList>


                                        <div style="display: flex; justify-content: center">
                                            <asp:Button ID="SetFirst" CssClass="bluebtn" runat="server" Text="Set First Image" Style="margin-right: 100px" OnClick="SetFirst_Click" Visible="false" />
                                            <asp:Button ID="DelBannerImage" CssClass="redbtn" runat="server" Text="Delete" OnClick="BannerImageDel_Click" Visible="false" OnClientClick="return confirm('Are you sure you want to delete？')" />
                                            <br />
                                            <div>
                                                <div>
                                                    <div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <!--ModelBanner Images End-->





                </asp:Panel>

            </div>







        </div>
        <!-- Top end -->

    </asp:Panel>
</asp:Content>
