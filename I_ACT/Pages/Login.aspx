<%@ Page Title="Log in" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="I_ACT.Pages.Login" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta name="description" content="IACT, MP2 Tracking App"><title>IACT | Login</title>
<!-- Site favicon -->
<link rel="shortcut icon" type="image/x-icon" href="images/favicon.ico">
<!-- /site favicon -->
<!-- Entypo font stylesheet -->
<link href="../Content/admin_dark/css/entypo.css" rel="stylesheet"/>
<!-- /entypo font stylesheet -->

<!-- Font awesome stylesheet -->
<link href="../Content/admin_dark/css/font-awesome.min.css" rel="stylesheet"/>
<!-- /font awesome stylesheet -->

<!-- CSS3 Animate It Plugin Stylesheet -->
<link href="../Content/admin_dark/css/plugins/css3-animate-it-plugin/animations.css" rel="stylesheet"/>
<!-- /css3 animate it plugin stylesheet -->

<!-- Bootstrap stylesheet min version -->
<link href="../Content/admin_dark/css/bootstrap.min.css" rel="stylesheet"/>
<!-- /bootstrap stylesheet min version -->

<!-- Mouldifi core stylesheet -->
<link href="../Content/admin_dark/css/mouldifi-core.css" rel="stylesheet"/>
<!-- /mouldifi core stylesheet -->
<link href="../Content/datagrid.css" rel="stylesheet" />
<link href="../Content/admin_dark/css/mouldifi-forms.css" rel="stylesheet"/>
<script src="../Content/admin_dark/sweetalert/sweetalert2.min.js"></script>
<link href="../Content/admin_dark/sweetalert/sweetalert2.min.css" rel="stylesheet" />
<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
      <script src="js/html5shiv.min.js"></script>
      <script src="js/respond.min.js"></script>
<![endif]-->

    <script>
        function ValidateData() {

            var txusername = $(".txUsername").val();
            var txpassword = $(".txPassword").val();
        var errMsg = "";
        var errCount = 0;
        if (txusername == "") {
            errCount++;
            errMsg += "<li><b>Input Username !</b></li>";
        }
        if (txpassword == "") {
            errCount++;
            errMsg += "<li style='text-align:left'><b>Input Password !</b></li>";
        }

        if (errCount > 0) {

            swal({
                type: 'error',
                title: 'Input Required Fields!',
                html: "<div id='boxValidasi' class='alert alert-danger alert-dismissible' style='text-align:left !important;display: block;font-size:20px' role='alert'> <ul>" + errMsg + "</ul> </div>",
                width: '800px',
            })
            //$('#boxValidasi').css('display', 'block');

            return false;
        } else {
            return true;
        }
    }

</script>


</head>
    <body class="login-page">
	<div class="login-pag-inner">
		<div class="animatedParent animateOnce z-index-50">
			<div class="login-container animated growIn slower go">
                <div class="row">
                    <div class="login-branding">
					<img src="../Images/logo.png" style="width:200px;height:50px" />

				    </div>
                  
                </div>
				
             <div class="row">
            <div class="col-md-4">
                
            </div>
            <div class="col-md-4">
                 <div class="login-content">
                   
                            <h2 style="color: #000;opacity:1 !important"><strong>Welcome</strong>, please login</h2>
                            <form id="Form1" style="color: #fff"  runat="server">
                                <div class="form-group">
                                    <input style="color: #000 !important;opacity:1 !important" placeholder="Username" runat="server" id="txUsername" class="Materialize txUsername" type="text" />
                                </div>
                                <div class="form-group">
                                    <input style="color: #000 !important;opacity:1 !important" placeholder="Password" runat="server" id="txPassword" class="Materialize txPassword" type="password" />
                                </div>
                                <div class="form-group">
                                </div>
                                <div class="form-group">
                                    <button type="button" style="opacity:1 !important" class="btn btn-primary btn-block" id="btnLogin" runat="server" onclick="ValidateData();" onserverclick="btnLogin_ServerClick">L O G I N</button>
                                    <%--<asp:Button runat="server" ID="btnOK" cssClass="btn btn-primary btn-block" OnClick="btnLogin_ServerClick" Text="L O G I N" />--%>
                                </div>
                                  </form>
                            <a style="color:#f00"> <asp:Label  runat="server" ID="lblLoginError" Text="" ></asp:Label></a>
                        </div>
               
                    <span style="font-size: 10px; color: #fff"> Copyright © PT. Pertamina (Persero) </span>
                </div>
                <div class="col-md-4">
                   
                </div>
        </div>
		</div>
	</div>
<!--Load JQuery-->
        <script src="../Content/admin_dark/js/jquery.min.js"></script>
<script src="../Content/admin_dark/js/jquery.min.js"></script>
<!-- Load CSS3 Animate It Plugin JS -->
<script src="../Content/admin_dark/js/plugins/css3-animate-it-plugin/css3-animate-it.js"></script>
<script src="../Content/admin_dark/js/bootstrap.min.js"></script>


</body>
</html>
