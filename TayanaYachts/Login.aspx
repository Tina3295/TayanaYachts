<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TayanaYachts.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Login</title>

    <!-- Custom fonts for this template-->
    <link href="vendor/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css" />
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet" />

    <!-- Custom styles for this template-->

    <link href="css/sb-admin-2.css" rel="stylesheet" type="text/css" />
    <link href="css/loading.css" rel="stylesheet" />
</head>
<body class="bg-gradient-primary">
    <form id="form1" runat="server">
        <div class="container">

            <!-- Outer Row -->
            <div class="row justify-content-center">

                <div class="col-xl-10 col-lg-12 col-md-9">

                    <div class="card o-hidden border-0 shadow-lg my-5">
                        <div class="card-body p-0">
                            <!-- Nested Row within Card Body -->
                            <div class="row">

                                <img src="images/306271799.jpg" style="width: 444px; height: 557px" />
                                <div class="col-lg-6">
                                    <div class="p-5" style="margin-top: 100px">
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">Welcome Back!</h1>

                                        </div>
                                        <div class="user">
                                            <div class="form-group">
                                                <asp:TextBox ID="Email" runat="server" placeholder="Enter Email Address..." class="form-control form-control-user" required="" aria-required="true" oninput="setCustomValidity('')" oninvalid="setCustomValidity('Required')"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="Password" runat="server" placeholder="Password" class="form-control form-control-user" required="" aria-required="true" oninput="setCustomValidity('')" oninvalid="setCustomValidity('Required')"></asp:TextBox>
                                            </div>
                                            <div class="form-group" style="margin-left: -24px">
                                                <div class="custom-control custom-checkbox small">

                                                    <asp:CheckBox ID="Check" runat="server" Text="&nbsp;&nbsp;Remember Me" />

                                                </div>
                                                <asp:Label ID="Tip" runat="server" Text="" Style="color: #FF0000"></asp:Label>


                                            </div>
                                            <!--登入-->
                                            <asp:Button ID="LoginBtn" runat="server" Text="Login" class="btn btn-primary btn-user btn-block" OnClick="LoginBtn_Click" OnClientClick="$('#Load').show();" />




                                            <!--  <hr/>
                                            <a href="index.html" class="btn btn-google btn-user btn-block">
                                                <i class="fab fa-google fa-fw"></i>Login with Google
                                            </a>
                                            <a href="index.html" class="btn btn-facebook btn-user btn-block">
                                                <i class="fab fa-facebook-f fa-fw"></i>Login with Facebook
                                            </a>
                                        </div>
                                        <hr/>
                                        <div class="text-center">
                                            <a class="small" href="forgot-password.html">Forgot Password?</a>
                                        </div>
                                       <div class="text-center">
                                            <a class="small" href="Register.aspx">Create an Account!</a>
                                        </div>-->
                                        </div>
                                    </div>



                  
                                        <div id="Load" class="preloader" style="display:none;margin-bottom:5.9px;margin-top:-30px">
                                            Loading<div class="circ1"></div>
                                            <div class="circ2"></div>
                                            <div class="circ3"></div>
                                            <div class="circ4"></div>
                                        </div>
              



                                    <div style="display: flex; justify-content: end">
                                        <a href="Index.aspx">返回前台</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>




    <!-- Bootstrap core JavaScript-->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>

    <!-- Custom scripts for all pages
    <script src="javascript/sb-admin-2.min.js"></script>-->
</body>
</html>
