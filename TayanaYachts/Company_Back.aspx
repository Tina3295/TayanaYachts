<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="Company_Back.aspx.cs" Inherits="TayanaYachts.Company_Back" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Tip" runat="server" Text=""></asp:Label>
    <asp:Panel ID="AdminOnly" runat="server">

        <div style="display: flex; justify-content: space-between">





            <asp:Panel ID="AboutUs" runat="server">
                <!-- About us Start -->
                <div class="container-fluid" style="width: 650px">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">About us</h6>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive" style="text-align: center">


                                <div>
                                    <CKEditor:CKEditorControl ID="CKEditorControl" runat="server" BasePath="/Scripts/ckeditor/"
                                        Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat NumberedList|
                                        BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|
                                        BidiLtr|BidiRtl
                                        /
                                        Styles|Format|Font|FontSize TextColor|BGColor Link|Image"
                                        Width="100%"></CKEditor:CKEditorControl>
                                    <br />

                                    <div style="display: flex; justify-content: center">
                                        <asp:Button ID="AboutUsBtn" CssClass="bluebtn" runat="server" Text="Upload" Style="margin-bottom: 10px" OnClick="AboutUsBtn_Click" />
                                    </div>
                                    <asp:Label ID="AboutUsTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!-- About us End -->
            </asp:Panel>



            <asp:Panel ID="Certificate" runat="server">
                <!-- Certificate Start-->
                <div class="container-fluid">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Certificate</h6>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive" style="text-align: center">


                                <div>
                                    <asp:TextBox ID="CertificateTextBox" runat="server" TextMode="MultiLine" Height="180px" Width="250px"></asp:TextBox>
                                    <br />

                                    <div style="display: flex; justify-content: center">
                                        <asp:Button ID="CertificateBtn" CssClass="bluebtn" runat="server" Text="Upload" Style="margin-top: 10px" OnClick="CertificateBtn_Click" />
                                    </div>
                                    <asp:Label ID="CertificateTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!-- Certificate End-->
            </asp:Panel>



        </div>
        <div style="display: flex; justify-content: space-between;">




            <asp:Panel ID="CertificateVerticalImg" runat="server">
                <!-- CertificateVerticalImg Start -->
                <div class="container-fluid" style="width: 500px">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Certificate Vertical Images</h6>
                        </div>

                        <div class="card-body">
                            <p>Add Images :</p>
                            <div class="table-responsive" style="margin-bottom: 0px">


                                <div style="display: flex; justify-content: space-between; text-align: center; align-items: center; padding-bottom: 5px">
                                    <asp:FileUpload ID="VerticalImgFileUpload" runat="server" AllowMultiple="True" />
                                    <br />

                                    <div>
                                        <asp:Button ID="VerticalImgButton" CssClass="bluebtn" runat="server" Text="Upload" Style="" OnClick="VerticalImgButton_Click" />
                                    </div>

                                </div>

                                <asp:Label ID="VerticalImgTip" runat="server" Text="" ForeColor="Red"></asp:Label>

                            </div>
                            <p style="border-top: 2px solid #4e73df; margin-top: 10px; padding-top: 19px; margin-bottom: 0">Certificate Vertical Images List :</p>
                            <asp:RadioButtonList ID="VerticalImgRadioButtonList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="VerticalImgRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList>

                            <div style="display: flex; justify-content: center">
                                <asp:Button ID="VerticalDelBtn" CssClass="redbtn" runat="server" Visible="false" Text="Delete" Style="margin-top: 10px" OnClick="VerticalDelBtn_Click" OnClientClick="return confirm('Are you sure you want to delete？')" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- CertificateVerticalImg End -->
            </asp:Panel>






            <asp:Panel ID="CertificateHorizontalImg" runat="server">
                <!-- CertificateHorizontalImg Start-->
                <div class="container-fluid" style="width: 500px">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Certificate Horizontal Images</h6>
                        </div>

                        <div class="card-body">
                            <p>Add Images :</p>
                            <div class="table-responsive" style="margin-bottom: 0px">


                                <div style="display: flex; justify-content: space-between; text-align: center; align-items: center; padding-bottom: 5px">
                                    <asp:FileUpload ID="HorizontalImgFileUpload" runat="server" AllowMultiple="True" />
                                    <br />

                                    <div>
                                        <asp:Button ID="HorizontalImgBtn" CssClass="bluebtn" runat="server" Text="Upload" Style="" OnClick="HorizontalImgBtn_Click" />
                                    </div>

                                </div>
                                <asp:Label ID="HorizontalImgTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            <p style="border-top: 2px solid #4e73df; margin-top: 10px; padding-top: 19px; margin-bottom: 0">Certificate Horizontal Images List :</p>
                            <asp:RadioButtonList ID="HorizontalImgRadioButtonList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="HorizontalImgRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList>
                            <div style="display: flex; justify-content: center">
                                <asp:Button ID="HorizontalDelBtn" CssClass="redbtn" runat="server" Visible="false" Text="Delete" Style="margin-top: 10px" OnClientClick="return confirm('Are you sure you want to delete？')" OnClick="HorizontalDelBtn_Click" />
                            </div>


                        </div>
                    </div>
                </div>
                <!-- CertificatHorizontalImg End-->
            </asp:Panel>
























        </div>
    </asp:Panel>
</asp:Content>
