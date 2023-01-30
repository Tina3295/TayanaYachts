<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="News_Back.aspx.cs" Inherits="TayanaYachts.NewsBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="margin-bottom: 20px">
            <ul>
                <li>
                    <img src="images/DealersCover.jpg" alt="Tayana Yachts"></li>
            </ul>
        </div>

        <div style="margin: 0 20px; display: flex; padding: 0 10px">


            <div class="card mb-4 py-3 border-bottom-primary" style="width: 48%">
                <div class="bluetop">
                    <p>ReleaseDate</p>
                </div>





                <div style="padding: 20px">

                    <h6>Date :</h6>
                    <div style="display: flex; justify-content: end">

                        <asp:DropDownList ID="CanlendarYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CanlendarYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <br />

                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="100%" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender">
                        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#3399FF" ForeColor="White" Font-Bold="True" />
                        <TitleStyle BackColor="White" BorderColor="#3399FF" BorderWidth="3px" Font-Bold="True" Font-Size="12pt" ForeColor="#3399FF" />
                        <TodayDayStyle BackColor="#CCCCCC" />
                    </asp:Calendar>
                    <hr />
                    <h6 style="display: inline-block">New Title :</h6>
                    <asp:TextBox ID="AddNewsTitle" runat="server" type="text" class="form-control" MaxLength="75" Width="223px"></asp:TextBox>
                    <asp:CheckBox ID="AddIsTop" runat="server" Text="Top" Style="margin-left: 5px" />
                    <asp:Button ID="AddTitle" runat="server" Text="Add" class="bluebtn" OnClick="AddTitle_Click" Style="margin-top: 10px" />

                </div>
            </div>




            <div class="card mb-4 py-3 border-bottom-primary" style="width: 48%">
                <div class="bluetop">
                    <p>News</p>
                </div>

                <div class="card-body">

                    <div>
                        <h6>Title :</h6>
                        <br />
                        <asp:RadioButtonList ID="NewsTitleRadioBtnList" runat="server" class="my-3" AutoPostBack="True" OnSelectedIndexChanged="NewsTitleRadioBtnList_SelectedIndexChanged"></asp:RadioButtonList>


                    </div>
                </div>

            </div>
        </div>

        <asp:Panel ID="NewsInfoHide" runat="server">
            <div style="margin: 0 20px; padding: 0 10px">
                <div class="card mb-4 py-3 border-bottom-primary" style="width: 98%; margin-bottom: 15px">
                    <div class="bluetop">
                        <p>News Detail</p>
                    </div>

                    <div class="card-body">

                        <table class="dealerinfor newscontent" style="width: 100%; margin: auto">

                            <tr>
                                <td>
                                    <h6>ReleaseDate:</h6>
                                </td>
                                <td>
                                    <asp:DropDownList ID="Years" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Years_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:DropDownList ID="Months" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Months_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:DropDownList ID="Days" runat="server"></asp:DropDownList>
                                </td>

                                <td style="border-left: 2px solid rgb(52, 169, 212); padding-left: 20px; width: 50%">
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
                                    <asp:Label ID="ThumbnailUploadTip" runat="server" Text=""></asp:Label>
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
                                    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="400px" Height="60px"></asp:TextBox>
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



                    </div>
                    <div style="display: flex; justify-content: space-between; padding: 0 250px 15px 250px">
                        <div style="display: inline-block">
                            <asp:Button ID="UpdateAgent" Class="bluebtn" runat="server" Text="Update" />
                        </div>
                        <div style="display: inline-block">
                            <asp:Button ID="DeleteAgent" Class="redbtn" runat="server" Text="Delete" OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;" />
                        </div>
                    </div>

                </div>
            </div>
        </asp:Panel>





        <div style="margin: 0 20px; padding: 0 10px">
            <div class="card mb-4 py-3 border-bottom-primary" style="width: 98%; margin-bottom: 15px">
                <div class="bluetop">
                    <p>News Images</p>
                </div>
                <div class="card-body">

                    
                            <h6>Upload Horizontal Group Image :</h6>
                            <div >
                                <asp:FileUpload ID="NewsImageUpload" runat="server" AllowMultiple="True" />
                                <asp:Button ID="NewsImageBtn" runat="server" Text="Upload" class="bluebtn" OnClick="NewsImageBtn_Click"  /><br/>
                                <asp:Label ID="NewsImageTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </div><hr/>
                            <h6>News Image List :</h6>
                            <asp:RadioButtonList ID="NewsImageRadioButtonList" runat="server"   CellPadding="10" RepeatColumns="3" RepeatDirection="Horizontal"></asp:RadioButtonList>
                            <asp:Button ID="DelNewsImage" runat="server" Text="Delete Image" type="button" class="redbtn" Style="margin-top:10px" OnClientClick="return confirm('Are you sure you want to delete？')" OnClick="DelNewsImage_Click"  />
          

                </div>
            </div>
        </div>


    </div>
</asp:Content>
