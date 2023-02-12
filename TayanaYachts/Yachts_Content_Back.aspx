<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="Yachts_Content_Back.aspx.cs" Inherits="TayanaYachts.Yachts_Content_Back" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="display: flex; justify-content: space-between">

        <div style="display: flex; flex-direction: column; justify-content: space-between; width: 30%">
            <!--Choose Yacht Model Srart-->
            <div class="container-fluid" style="margin: unset">
                <div class="card shadow mb-4">

                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Yacht OverView</h6>
                    </div>

                    <div class="card-body">
                        <div class="table-responsive">
                            <h6>Choose Yacht Model:</h6>
                            <div style="display: flex; flex-direction: column">

                                <div style="margin-bottom: 5px">
                                    <asp:DropDownList ID="ModelDropDownList" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ModelDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    <br />
                                </div>


                                <div>
                                    <asp:Image ID="Newbuilding" runat="server" src='/images/newbuilding.jpg' Style='margin-right: 10px' Height="15px" Visible="false" />
                                    <asp:Image ID="Newdesign" runat="server" src='/images/newdesign.jpg' Style='margin-left: 0px' Height="15px" Visible="false" />
                                </div>
                        <!--        <div style="margin-top: 25px; display: flex; justify-content: center">
                                    <asp:Button ID="DelModel" CssClass="redbtn" runat="server" Text="Delete" />

                                </div>-->
                            </div>




                        </div>
                    </div>
                </div>
            </div>
            <!--Choose Yacht Model End-->











            <!--Downloads File Srart-->
            <asp:Panel ID="DownloadsPanel" runat="server" Visible="false">
                <div class="container-fluid" style="margin: unset">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Downloads File</h6>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive">

                                <div style="display: flex; flex-direction: column">
                                    <div style="border-bottom: 2px solid #4e73df; padding-bottom: 20px; margin-bottom: 15px">
                                        Video Url :<asp:TextBox ID="VideoTextBox" runat="server" Width="100%" Style=""></asp:TextBox>
                                    </div>


                                    <div>
                                        Add Files :
                                        <div style="align-items: center; display: flex; flex-direction: column">

                                            <asp:FileUpload ID="AttachmentFileUpload" runat="server" AllowMultiple="True" Style="width: 200px" /><br />

                                            <asp:Button ID="AttachmentBtn" runat="server" Text="Add New" class="bluebtn" Style="margin-top: 0px" OnClick="AttachmentBtn_Click" />
                                        </div>
                                        <br />
                                        <asp:Label ID="AttachmentTip" runat="server" Text="" ForeColor="red" Style="margin-top: 20px"></asp:Label><hr />



                                        <asp:RadioButtonList ID="AttachmentRadioButtonList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AttachmentRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList>
                                        <div style="display: flex; justify-content: center">
                                            <asp:Button ID="DelAttachmentBtn" runat="server" Visible="false" Text="Delete" class="redbtn" Style="margin-top: 3px" OnClientClick="javascript:if(!window.confirm('Are you sure you want to delete？')) window.event.returnValue = false;" OnClick="DelAttachmentBtn_Click" />
                                        </div>
                                    </div>
                                </div>




                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <!--Downloads File End-->


        </div>



        <!--Overview Content Srart-->

        <div class="container-fluid" style="width: 70%; margin: unset">
            <asp:Panel ID="ContentPanel" runat="server" Visible="false">
                <div class="card shadow mb-4">

                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Overview & Dimension Content</h6>
                    </div>

                    <div class="card-body">
                        <div class="table-responsive">
                            Overview Content :<CKEditor:CKEditorControl ID="OverviewCKEditorControl" runat="server" BasePath="/Scripts/ckeditor/"
                                Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat NumberedList|
                                        BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|
                                        BidiLtr|BidiRtl
                                        /
                                        Styles|Format|Font|FontSize TextColor|BGColor Link|Image|Table|Source"
                                Width="100%" ></CKEditor:CKEditorControl><br/>
                        <!--    <asp:TextBox ID="OverviewTextBox" runat="server" Width="100%" Height="200px" Style="margin-bottom: 15px" TextMode="MultiLine"></asp:TextBox>-->
                            Dimension Content :<CKEditor:CKEditorControl ID="CKEditorControl" runat="server" BasePath="/Scripts/ckeditor/"
                                Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat NumberedList|
                                        BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|
                                        BidiLtr|BidiRtl
                                        /
                                        Styles|Format|Font|FontSize TextColor|BGColor Link|Image|Table|Source"
                                Width="100%"></CKEditor:CKEditorControl>





                            <div style="margin-top: 15px; display: flex; justify-content: center">
                                <asp:Button ID="OverviewContentBtn" CssClass="bluebtn" runat="server" Text="Update" OnClick="OverviewContentBtn_Click" />

                            </div>
                            <div style="display: flex; justify-content: center">
                                <asp:Label ID="OverviewContentBtnTip" runat="server" Text="" ForeColor="Red" Style="margin-top: 5px"></asp:Label>
                            </div>



                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>

        <!--Overview Content End-->
    </div>





    <asp:Panel ID="DownPanel" runat="server" Visible="false">

        <div style="display: flex; justify-content: space-between">


            <div style="width: 30%">
                <!--Layout & Deck Plan Srart-->

                <div class="container-fluid" style="margin: unset">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Layout & Deck Plan</h6>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive">

                                <div style="display: flex; flex-direction: column">



                                    <div>
                                        Add Images :
                                        <div style="align-items: center; display: flex; flex-direction: column">

                                            <asp:FileUpload ID="LauoutFileUpload" runat="server" AllowMultiple="True" Style="width: 200px" /><br />

                                            <asp:Button ID="LayoutUploadBtn" runat="server" Text="Add New" class="bluebtn" Style="margin-top: 0px" OnClick="LayoutUploadBtn_Click" />
                                        </div>
                                        <br />
                                        <asp:Label ID="LayoutUploadTip" runat="server" Text="" ForeColor="red" Style="margin-top: 20px"></asp:Label><hr />



                                        <asp:RadioButtonList ID="LayoutRadioButtonList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LayoutRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList>
                                        <div style="display: flex; justify-content: center">
                                            <asp:Button ID="DelLayoutBtn" runat="server" Visible="false" Text="Delete" class="redbtn" Style="margin-top: 3px" OnClientClick="javascript:if(!window.confirm('Are you sure you want to delete？')) window.event.returnValue = false;" OnClick="DelLayoutBtn_Click" />
                                        </div>
                                    </div>
                                </div>




                            </div>
                        </div>
                    </div>
                </div>

                <!--Layout & Deck Plan End-->
            </div>











            <div style="width: 70%">
                <!--Specification Srart-->

                <div class="container-fluid" style="margin: unset">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Specification</h6>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive">

                                <div class="table-responsive">
                                    Specification Content :<CKEditor:CKEditorControl ID="CKEditorControl2" runat="server" BasePath="/Scripts/ckeditor/"
                                        Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat NumberedList|
                                        BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|
                                        BidiLtr|BidiRtl
                                        /
                                        Styles|Format|Font|FontSize TextColor|BGColor Link|Image|Table|Source"
                                        Width="100%"></CKEditor:CKEditorControl>





                                    <div style="margin-top: 15px; display: flex; justify-content: center">
                                        <asp:Button ID="SpecificationContentBtn" CssClass="bluebtn" runat="server" Text="Update" OnClick="SpecificationContentBtn_Click" />

                                    </div>
                                    <div style="display: flex; justify-content: center">
                                        <asp:Label ID="SpecificationTip" runat="server" Text="" ForeColor="Red" Style="margin-top: 5px"></asp:Label>
                                    </div>




                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Specification End-->
            </div>
        </div>
    </asp:Panel>
</asp:Content>
