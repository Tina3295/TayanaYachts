<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dealers_Back.aspx.cs" Inherits="TayanaYachts.dealers_Back" %>

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

        <div style="margin: 0 20px;display: flex; padding: 0 10px">


            <div class="card mb-4 py-3 border-bottom-primary" style="width: 48%">
                <div class="bluetop">
                    <p>Country</p>
                </div>
                <div class="card-body">
                    新增國家:<asp:TextBox ID="AddCountryTextBox" runat="server"></asp:TextBox>
                    <asp:Button ID="AddCountry" CssClass="bluebtn" runat="server" Text="Add" OnClick="AddCountry_Click" /><br />
                    <asp:Label ID="AddCountryTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div style="padding-left: 20px; padding-bottom: 20px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CountryID " OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" EnableViewState="False">
                        <Columns>
                            <asp:TemplateField HeaderText="編號" SortExpression="Country">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="國家" SortExpression="Country">
                                <EditItemTemplate>
                                    <asp:TextBox ID="CountryEdit" runat="server" Text='<%# Bind("Country") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Country") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="新增日期" SortExpression="InitDate">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("InitDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="區域數">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("區域數") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="Renew" runat="server" CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                                    &nbsp;<asp:LinkButton ID="Scrap" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Edit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>




            <div class="card mb-4 py-3 border-bottom-primary" style="width: 48%">
                <div class="bluetop">
                    <p>Area</p>
                </div>

                <div class="card-body">
                    <asp:DropDownList ID="CountryDropDownList" runat="server" AppendDataBoundItems="True" DataTextField="Country" DataValueField="CountryID" AutoPostBack="True" OnSelectedIndexChanged="CountryDropDownList_SelectedIndexChanged">
                    </asp:DropDownList><br />
                    <br />
                    <asp:RadioButtonList ID="DealerRadioButtonList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DealerRadioButtonList_SelectedIndexChanged"></asp:RadioButtonList><br />

                    <div style="border-top: 2px solid rgb(52, 169, 212); padding-top: 15px">
                        Add agent area:<asp:TextBox ID="AddAreaTextBox" runat="server"></asp:TextBox>
                        <asp:Button ID="AddArea" CssClass="bluebtn" runat="server" Text="Add" OnClick="AddArea_Click" /><br />
                        <asp:Label ID="AddAreaTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>

                </div>

            </div>
        </div>

        <asp:Panel ID="DealerInfoHide" runat="server">
            <div style="margin: 0 20px;padding: 0 10px">
                <div class="card mb-4 py-3 border-bottom-primary" style="width: 98%;margin-bottom:15px">
                    <div class="bluetop">
                        <p>Area Dealer Info</p>
                    </div>

                    <div class="card-body">

                        <table class="dealerinfor" style="width: 100%; margin: auto">
                            <tr style="text-align: left;">
                                <th style="width: 200px">項目</th>
                                <th>資訊</th>
                            </tr>
                            <tr>
                                <td>Agentimg:</td>
                                <td>
                                    <asp:Image ID="Agentimg" Width="209px" Height="148px" runat="server" />
                                    &nbsp;&nbsp;<asp:FileUpload ID="AgentimgUpload" runat="server" />
                                    <asp:Button ID="AgentimgUploadBtn" Class="bluebtn" runat="server" Text="Upload" OnClick="AgentimgUploadBtn_Click"/>
                                    <asp:Label ID="AgentimgUploadTip" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Country:</td>
                                <td>
                                    <asp:TextBox ID="Country" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Area:</td>
                                <td>
                                    <asp:TextBox ID="Area" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Name:</td>
                                <td>
                                    <asp:TextBox ID="Name" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Contact:</td>
                                <td>
                                    <asp:TextBox ID="Contact" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Address:</td>
                                <td>
                                    <asp:TextBox ID="Address" runat="server" Width="500px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>TEL:</td>
                                <td>
                                    <asp:TextBox ID="TEL" runat="server" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Fax:</td>
                                <td>
                                    <asp:TextBox ID="Fax" runat="server" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Email:</td>
                                <td>
                                    <asp:TextBox ID="Email" runat="server" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Link:</td>
                                <td>
                                    <asp:TextBox ID="Link" runat="server" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>InitDate:</td>
                                <td>
                                    <asp:Label ID="InitDate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>



                    </div>
                    <div style="display: flex; justify-content: space-between; padding: 0 250px 15px 250px">
                        <div style="display: inline-block">
                            <asp:Button ID="UpdateAgent" Class="bluebtn" runat="server" Text="Update" OnClick="UpdateAgent_Click" />
                        </div>
                        <div style="display: inline-block">
                            <asp:Button ID="DeleteAgent" Class="redbtn" runat="server" Text="Delete" OnClick="DeleteAgent_Click" OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) window.event.returnValue = false;"/>
                        </div>
                    </div>

                </div>
            </div>
        </asp:Panel>
    </div>




</asp:Content>
