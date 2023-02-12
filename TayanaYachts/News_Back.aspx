<%@ Page Title="" Language="C#" MasterPageFile="~/Back.Master" AutoEventWireup="true" CodeBehind="News_Back.aspx.cs" Inherits="TayanaYachts.News_Back" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- Top start -->
    <div style="display: flex; justify-content: space-between">


        <!-- ReleaseDate Start -->
        <div class="container-fluid" style="width: 500px;margin:unset">
            <div class="card shadow mb-4">

                <div class="card-header py-3">
                    <h6 class="m-0 font-weight-bold text-primary">ReleaseDate</h6>
                </div>

                <div class="card-body">
                    <div class="table-responsive">


                        <div style="padding: 20px">

                            <h6>Date :</h6>
                            <div style="display: flex; justify-content: end">

                                <asp:DropDownList ID="CanlendarYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CanlendarYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <br />
                            <div Style="text-align:center">
                            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="100%"   OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged">
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#3399FF" ForeColor="White" Font-Bold="True" />
                                <TitleStyle BackColor="White" BorderColor="#3399FF" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="#3399FF" />
                                <TodayDayStyle BackColor="#CCCCCC" />
                            </asp:Calendar>
                                </div>
                            <br />
                            <hr />
                            <h6>New Title :</h6>
                            <asp:TextBox ID="AddNewsTitle" runat="server" type="text" MaxLength="75" Width="229px"></asp:TextBox>
                            <asp:CheckBox ID="AddIsTop" runat="server" Text="Top" Style="margin-left: 5px" />
                            <asp:Button ID="AddTitle" runat="server" Text="Add" class="bluebtn" OnClick="AddTitle_Click" Style="margin-top: 10px" />
                            <asp:Label ID="AddTitleTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <!-- ReleaseDate End -->











        <div style="display: flex; flex-direction: column; justify-content: space-between">



            <asp:Panel ID="NewsTitlePanel" Visible="false" runat="server">
                <!-- 新聞列表 Start-->
                <div class="container-fluid" style="width:466px;padding:unset">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">News</h6>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive" >


                                <div style="display: flex">
                                    <h6 style="display: inline-block; margin-right: 10px">Title :</h6>
                                    <br />
                                    <asp:RadioButtonList ID="NewsTitleRadioBtnList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="NewsTitleRadioBtnList_SelectedIndexChanged"></asp:RadioButtonList>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <!-- 新聞列表 End-->
            </asp:Panel>







            <asp:Panel ID="NewsAttachmentPanel" Visible="false" runat="server">
                <!-- 新聞附件 Start-->
                <div class="container-fluid">
                    <div class="card shadow mb-4">

                        <div class="card-header py-3">
                            <h6 class="m-0 font-weight-bold text-primary">Attachments</h6>
                        </div>

                        <div class="card-body">
                            <div class="table-responsive">


                                <div>
                                    <div style="display: flex; align-items: center">
                                        <asp:FileUpload ID="AttachmentFileUpload" runat="server" AllowMultiple="True" />

                                        <asp:Button ID="AttachmentBtn" runat="server" Text="Add New" class="bluebtn" OnClick="AttachmentBtn_Click" /><br />
                                    </div>
                                    <asp:Label ID="AttachmentTip" runat="server" Text="" ForeColor="red" Style="margin-top: 15px"></asp:Label><hr />



                                    <asp:RadioButtonList ID="AttachmentRadioButtonList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="AttachmentRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList>
                                    <asp:Button ID="DelAttachmentBtn" runat="server" Visible="false" Text="Delete" class="redbtn" Style="margin-top: 3px" OnClick="DelAttachmentBtn_Click" OnClientClick="javascript:if(!window.confirm('Are you sure you want to delete？')) window.event.returnValue = false;" />
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
                <!-- 新聞附件 End-->
            </asp:Panel>






        </div>
    </div>
    <!-- Top end -->
























    <asp:Panel ID="NewsDetailPanel" Visible="false" runat="server">

        <div>
            <!--新聞詳細資訊 Start-->
            <div class="container-fluid">
                <div class="card shadow mb-4">

                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">News Detail</h6>
                    </div>

                    <div class="card-body">
                        <div class="table-responsive" >


                            <table class="dealerinfor newscontent" style="width: 100%; margin: auto">

                                <tr>
                                    <td>
                                        <h6>ReleaseDate:</h6>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="Years" runat="server"></asp:DropDownList>
                                        <asp:DropDownList ID="Months" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Months_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:DropDownList ID="Days" runat="server"></asp:DropDownList>
                                    </td>

                                    <td style="border-left: 2px solid #4e73df; padding-left: 20px; width: 50%">
                                        <h6>IsTop:</h6>
                                        <asp:CheckBox ID="IsTop" runat="server" Style="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h6>Thumbnail:</h6>
                                    </td>
                                    <td colspan="2">
                                        <asp:Image ID="Thumbnailimg" Width="209px" Height="148px" runat="server" /><br />
                                        &nbsp;&nbsp;<asp:FileUpload ID="ThumbnailUpload" runat="server" />
                                        <asp:Button ID="ThumbnailUploadBtn" Class="bluebtn" runat="server" Text="Upload" OnClick="ThumbnailUploadBtn_Click" />
                                        <asp:Label ID="ThumbnailUploadTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h6>Title:</h6>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="NewsTitle" runat="server" Width="400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h6>Summary:</h6>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="Summary" runat="server" TextMode="MultiLine" Width="400px" Height="60px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <h6>NewsContent:</h6>
                                    </td>
                                    <td colspan="2">
                                        <CKEditor:CKEditorControl ID="CKEditorControl" runat="server" BasePath="/Scripts/ckeditor/"
                                            Toolbar="Bold|Italic|Underline|Strike|Subscript|Superscript|-|RemoveFormat NumberedList|
                                        BulletedList|-|Outdent|Indent|-|JustifyLeft|JustifyCenter|JustifyRight|JustifyBlock|-|
                                        BidiLtr|BidiRtl
                                        /
                                        Styles|Format|Font|FontSize TextColor|BGColor Link|Image"
                                            Width="100%"></CKEditor:CKEditorControl>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <h6>InitDate:</h6>
                                    </td>
                                    <td colspan="2">

                                        <asp:Label ID="InitDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>


                            </table>






                            <div style="display: flex; justify-content: space-between; padding: 30px 250px 15px 250px">
                                <div style="display: inline-block">
                                    <asp:Button ID="UpdateNews" Class="bluebtn" runat="server" Text="Update" OnClick="UpdateNews_Click" />
                                </div>
                                <div style="display: inline-block">
                                    <asp:Button ID="DeleteNews" Class="redbtn" runat="server" Text="Delete" OnClientClick="javascript:if(!window.confirm('Are you sure you want to delete？')) window.event.returnValue = false;" OnClick="DeleteNews_Click" />
                                </div>
                            </div>
                            <div Style="text-align:center">
                            <asp:Label ID="UpdateNewsTip" runat="server" Text="" ForeColor="red" ></asp:Label>
                            </div>





                        </div>
                    </div>
                </div>
            </div>
            <!-- 新聞詳細資訊 End-->
        </div>

























        <div>

            <!-- 多圖 Start-->
            <div class="container-fluid">
                <div class="card shadow mb-4">

                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">News Images</h6>
                    </div>

                    <div class="card-body">
                        <div class="table-responsive" >


                            <h6>Upload Horizontal Group Image :</h6>
                            <div>
                                <asp:FileUpload ID="NewsImageUpload" runat="server" AllowMultiple="True" />
                                <asp:Button ID="NewsImageBtn" runat="server" Text="Upload" class="bluebtn" OnClick="NewsImageBtn_Click" /><br />
                                <asp:Label ID="NewsImageTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div>
                            <hr />
                            <h6>News Image List :</h6>
                            <asp:RadioButtonList ID="NewsImageRadioButtonList" runat="server" CellPadding="10" RepeatColumns="3" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="NewsImageRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList>
                            <asp:Button ID="DelNewsImage" runat="server" Text="Delete Image" type="button" class="redbtn" Style="margin-top: 10px" OnClientClick="return confirm('Are you sure you want to delete？')" OnClick="DelNewsImage_Click" Visible="False" />


                        </div>
                    </div>
                </div>
            </div>
            <!-- 多圖 End-->

        </div>


    </asp:Panel>




</asp:Content>
