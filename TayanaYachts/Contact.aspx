<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TayanaYachts.Contact" %>

<%@ Register assembly="Recaptcha.Web" namespace="Recaptcha.Web.UI.Controls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="padding: 115px 20px 0 20px">
            <ul>
                <li>
                    <img src="images/contact_banner.jpg" alt="Tayana Yachts" width="100%" style="border-radius: 5px"></li>
            </ul>
        </div>

        <div class="conbg">
            <!--------------------------------左邊選單開始---------------------------------------------------->
            <div class="left">

                <div class="left1">
                    <p><span>CONTACT</span></p>
                    <ul>
                        <li><a href="#">contacts</a></li>
                    </ul>



                </div>




            </div>







            <!--------------------------------左邊選單結束---------------------------------------------------->

            <!--------------------------------右邊選單開始---------------------------------------------------->
            <div id="crumb"><a href="index.aspx" style="text-decoration: none; color: #747474">Home</a> &gt;&gt; <a href="#" style="color: #34A9D4; text-decoration: none"><span class="on1">Contact</span></a></div>
            <div class="right">
                <div class="right1">
                    <div class="title"><span style="margin-left: 19px">Contact</span></div>

                    <!--------------------------------內容開始---------------------------------------------------->
                    <!--表單-->
                    <div class="from01">
                        <p>
                            Please Enter your contact information<span class="span01">*Required</span>
                        </p>
                        <br>
                        <table>
                            <tbody>
                                <tr>
                                    <td class="from01td01">Name :</td>
                                    <td><span>*</span><asp:TextBox ID="NameTextBox" runat="server" required="" aria-required="true" oninput="setCustomValidity('')" oninvalid="setCustomValidity('Please enter your name!')"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="from01td01">Email :</td>
                                    <td><span>*</span><asp:TextBox ID="EmailTextBox" runat="server" required="" aria-required="true" oninput="setCustomValidity('')" oninvalid="setCustomValidity('Please enter your email!')"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="from01td01">Phone :</td>
                                    <td><span>*</span><asp:TextBox ID="PhoneTextBox" runat="server" required="" aria-required="true" oninput="setCustomValidity('')" oninvalid="setCustomValidity('Please enter your Phone!')"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="from01td01">Country :</td>
                                    <td><span>*</span><asp:DropDownList ID="CountryDropDownList" runat="server"></asp:DropDownList>


                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"><span>*</span>Brochure of interest  *Which Brochure would you like to view?</td>
                                </tr>
                                <tr>
                                    <td class="from01td01">&nbsp;</td>
                                    <td>
                                        <asp:DropDownList ID="InterestDropDownList" runat="server"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="from01td01">Comments:</td>
                                    <td>
                                        <asp:TextBox ID="CommentsTextBox" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="from01td01">&nbsp;</td>
                                    <td class="f_right" style="padding-top:30px">
                                        <cc1:RecaptchaWidget ID="Recaptcha" runat="server"  />
                                        <asp:Label ID="RecaptchaTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="from01td01">&nbsp;</td>
                                    <td class="f_right" style="display:flex;justify-content:center;border:0">
                                        <asp:ImageButton ID="Submit" runat="server" ImageUrl="~/images/buttom03.gif" OnClick="Submit_Click" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <!--表單-->

                    <div class="box1">
                        <span class="span02">Contact with us</span><br/>
                        Thanks for your enjoying our web site as an introduction to the Tayana world and our range of yachts.
As all the designs in our range are semi-custom built, we are glad to offer a personal service to all our potential customers. 
If you have any questions about our yachts or would like to take your interest a stage further, please feel free to contact us.
                    </div>

                    <div class="list03" >
                        <p style="margin-top:15px">
                            <span>TAYANA HEAD OFFICE</span><br/>
                            NO.60 Haichien Rd. Chungmen Village Linyuan Kaohsiung Hsien 832 Taiwan R.O.C<br/>
                            tel. +886(7)641 2422<br/>
                            fax. +886(7)642 3193<br/>
                        </p>
                    </div>

                    <div class="box4">
                        <h4>Location</h4>
                        <p>
                             <iframe width="695" height="518" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://www.google.com/maps/embed?pb=!1m16!1m12!1m3!1d58974.877831635975!2d120.35885773217059!3d22.506814460527863!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!2m1!1sNO.60%20Haichien%20Rd.%20Chungmen%20Village%20Linyuan%20Kaohsiung%20Hsien%20832%20Taiwan%20R.O.C!5e0!3m2!1szh-TW!2stw!4v1675313187138!5m2!1szh-TW!2stw" width="600" height="450" style="border:0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                        </p>

                    </div>




                    <!--------------------------------內容結束------------------------------------------------------>

                </div>
            </div>

            <!--------------------------------右邊選單結束---------------------------------------------------->
        </div>




    </div>
</asp:Content>
