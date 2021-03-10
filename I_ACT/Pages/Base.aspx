<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Base.aspx.cs" Inherits="I_ACT.Pages.Base" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" tagPrefix="ajax" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta name="description" content="Aplikasi untuk mentracking progress pekerjaan ">
    <meta name="keywords" content="Pertamina, IACT, tracking">
    <title>Pertamina | I-Act</title>
    <!-- Site favicon -->
    <link rel='shortcut icon' type='image/x-icon' href='../Content/admin_dark/images/favicon.ico' />
    <!-- /site favicon -->
    <!-- Entypo font stylesheet -->
    <link href="../Content/admin_dark/css/entypo.css" rel="stylesheet" />
    <!-- /entypo font stylesheet -->
    <!-- Font awesome stylesheet -->
    <link href="../Content/admin_dark/css/font-awesome.min.css" rel="stylesheet" />
    <!-- /font awesome stylesheet -->
    <!-- Bootstrap stylesheet min version -->
    <link href="../Content/admin_dark/css/bootstrap.min.css" rel="stylesheet" />
    <!-- /bootstrap stylesheet min version -->
    <!-- Mouldifi core stylesheet -->
    <link href="../Content/admin_dark/css/mouldifi-core.css" rel="stylesheet" />
    <!-- /mouldifi core stylesheet -->

    <link href="../Content/admin_dark/css/plugins/morris/morris.css" rel="stylesheet" />

    <link href="../Content/admin_dark/css/mouldifi-forms.css" rel="stylesheet" />

    <link href="../Content/admin_dark/css/plugins/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="../Content/admin_dark/css/plugins/colorpicker/bootstrap-colorpicker.css" rel="stylesheet" />
    <link href="../Content/admin_dark/css/plugins/nouislider/nouislider.css" rel="stylesheet" />
    <link href="../Content/admin_dark/css/plugins/select2/select2.css" rel="stylesheet" />
    <link href="../Content/admin_dark/css/mouldifi-forms.css" rel="stylesheet" />
    <link href="../Content/admin_dark/css/plugins/datatables/jquery.dataTables.css" rel="stylesheet" />
    <link href="../Content/admin_dark/js/plugins/datatables/dataTables.bootstrap.css" rel="stylesheet" />
    <link href="../Content/admin_dark/js/plugins/datatables/extensions/Buttons/css/buttons.dataTables.css" rel="stylesheet" />
    <link href="../Content/datagrid.css" rel="stylesheet" />
    <link href="../Content/admin_dark/css/sidebar.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../Content/admin_dark/js/plugins/tooltipster/dist/css/tooltipster.bundle.min.css" />
    <link href="../Content/jquery.datepicker.css" rel="stylesheet" />
    <link href="../Content/admin_dark/sweetalert/sweetalert2.min.css" rel="stylesheet" />
    <link href="../Content/admin_dark/js/plugins/uploadify/uploadify.css" rel="stylesheet" />
            <script src="../Content/admin_dark/js/jquery.min.js"></script>
    <script src="../Content/admin_dark/js/plugins/uploadify/jquery.uploadify-3.1.js"></script>
             <script src="../Scripts/Json2.js"></script>
            <script src="../Content/admin_dark/js/plugins/datatables/jquery.dataTables.min.js"></script>
            <script src="../Content/admin_dark/js/bootstrap.min.js"></script>
            <script src="../Content/admin_dark/js/plugins/easypiechart/dist/jquery.easing.min.js"></script>
            <script src="../Content/admin_dark/js/plugins/easypiechart/dist/jquery.easypiechart.min.js"></script>
    
            <script src="../Js/Chart.js"></script>
            <script src="../Scripts/utils.js"></script>
            <script src="../Scripts/Chart.PieceLabel.js"></script>
            <script src="../Scripts/chartjs.plugin.datalabels.js"></script>

     <script>
         function pageLoad(sender, args) {
             $(function () {
                 $(".clsDate").datepicker(
                      { dateFormat: 'dd/mm/yy' }
                      );
             });
           
         }
    </script>
</head>
<body>
    
    <div class="page-container">
        

        <!-- Page Sidebar -->
        <div class="page-sidebar">
            
            <!-- Site header  -->
            <header class="site-header">
                <div class="site-logo"><img src="../Images/logoputih.png" class="img img-responsive" width="167px" height="47px"/></div>
                <div class="sidebar-collapse hidden-xs"><a class="sidebar-collapse-icon" href="#"><i class="icon-menu"></i></a></div>
                <div class="sidebar-mobile-menu visible-xs"><a data-target="#side-nav" data-toggle="collapse" class="mobile-menu-icon" href="#"><i class="icon-menu"></i></a></div>
            </header>
            <!-- /site header -->
            <!-- Main navigation -->
              <% if (Session["idrole"].ToString()=="1")
                       { %>
            <ul id="side-nav" class="main-menu navbar-collapse collapse">
                <li>
                    <a href="~/Dashboard/0" runat="server">
                        <b>
                        <i class="fa fa-tachometer"></i>
                        <span class="title">My Dashboard</span>
                        </b>
                    </a>
                </li>
                <li class="has-sub">
                    <a href="javascript:;">
                        <b>
                            <i class="fa fa-database" aria-hidden="true"></i>
                        <span class="title">Master Data</span>
                        </b>
                       
                    </a>
                    <ul class="nav collapse" id="inisiatif_nav">
                        <li>
                            <a href="~/Recommendation/1" runat="server">
                                <b>
                                    <i class="fa fa-briefcase" aria-hidden="true"></i>
                                <span class="title">Master Recommendation</span>
                                </b>
                                
                            </a>
                        </li>
                        <li>
                            <a href="~/Source/2" runat="server">
                                <b>
                                    <i class="fa fa-book" aria-hidden="true"></i>
                                <span class="title">Master Source</span>
                                </b>
                                
                            </a>
                        </li>
                       
                    </ul>
                </li>
           
                 <li>
                    <a href="~/ActTracking/3" runat="server">
                        <b>
                             <i class="fa fa-desktop " aria-hidden="true"></i>
                        <span class="title">My Action Tracking</span>
                        </b>
                       
                    </a>
                </li>
                       <li>
                    <a id="A5" href="~/MyDraft/5" runat="server">
                        <b>
                            <i class="fa fa-pencil" aria-hidden="true"></i>
                        <span class="title">My Draft</span>
                        </b>
                        
                    </a>
                </li>
                 <li>
                    <a id="A1" href="~/MyTask/4" runat="server">
                        <b>
                            <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                        <span class="title">My Task</span>
                        </b>
                        
                    </a>
                </li>
                
                 <li>
                    <a id="A4" href="~/Report/6" runat="server">
                        <b>
                            <i class="fa fa-file-text" aria-hidden="true"></i>
                        <span class="title">My Report</span>
                        </b>
                        
                    </a>
                </li>
                 <li>
                        <b>
                    <a id="A3" href="../Doc/User-Manual-iact.pdf" download>
                            <i class="fa fa-file-text-o" aria-hidden="true"></i>
                        <span class="title">My User Manual</span>
                    </a>
                     </b>
                </li>
                <li>
                    <a id="A2" href="~/Logout/7" runat="server">
                        <b>
                            <i class="fa fa-sign-out" aria-hidden="true"></i>
                        <span class="title">Log Out</span>
                        </b>
                        
                    </a>
                </li>
            </ul>
            <%}
                 else if (Session["idrole"].ToString() == "2") 
                 {%> 
                 <ul id="Ul1" class="main-menu navbar-collapse collapse">
                <li>
                    <a id="A6" href="~/Dashboard/0" runat="server">
                        <b>
                        <i class="fa fa-tachometer"></i>
                        <span class="title">My Dashboard</span>
                        </b>
                    </a>
                </li>
                 <li>
                    <a id="A9" href="~/ActTracking/3" runat="server">
                        <b>
                             <i class="fa fa-desktop " aria-hidden="true"></i>
                        <span class="title">My Action Tracking</span>
                        </b>
                       
                    </a>
                </li>
                       <li>
                    <a id="A10" href="~/MyDraft/5" runat="server">
                        <b>
                            <i class="fa fa-pencil" aria-hidden="true"></i>
                        <span class="title">My Draft</span>
                        </b>
                        
                    </a>
                </li>
                 <li>
                    <a id="A11" href="~/MyTask/4" runat="server">
                        <b>
                            <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                        <span class="title">My Task</span>
                        </b>
                        
                    </a>
                </li>
                 <li>
                        <b>
                    <a id="A13" href="../Doc/TKI-IACT.pdf" download>
                            <i class="fa fa-file-text-o" aria-hidden="true"></i>
                        <span class="title">My User Manual</span>
                    </a>
                     </b>
                </li>
                <li>
                     <a id="A7" href="~/Logout/7" runat="server">
                        <b>
                            <i class="fa fa-sign-out" aria-hidden="true"></i>
                        <span class="title">Log Out</span>
                        </b>
                        
                    </a>
                </li>
            </ul>
            <%} %>
            <!-- /main navigation -->
           
        </div>
        <!-- /page sidebar -->
        <!-- Main container -->
        <div class="main-container">

            <!-- /main header -->

            <div class="main-listheader" >
                <div class="col-sm-9">
                 <img src="../Images/logo.png" style="width:150px;height:40px" />
            
                </div>
                <div class="col-sm-3">
                   
                    <div class="col-sm-12" style="color:#fff" >
                        User    :    <% Response.Write(Session["username"]); %><br />
                        Role    :    <% Response.Write(Session["Role"]); %>
                    </div>
                </div>
            </div>
           
            <!-- Main content -->
            <div class="main-content">
                 <form id="Form1" runat="server" enctype="multipart/form-data" method="post">
                    <asp:ScriptManager runat="server" ID="ScriptManager1" EnablePageMethods="true"></asp:ScriptManager>
                    <asp:PlaceHolder runat="server" ID="phbody">
                    </asp:PlaceHolder>
                </form>
                
            </div>
            <!-- /main content -->
            <!-- Footer -->
                <div class="footer-main" style="font-size:x-small; height:auto; color:white;bottom:10px;padding:10px">
                    
                    <b> Copyright © PT. Pertamina (Persero)</b><br />
                   <%-- <b> Adopt by MP2</b><br />
                       Jl. Raya Balongan KM 9, Indramayu - Indonesia<br />
                       ADM Building, Lantai 2--%>
                   
                </div>
                <!-- /footer -->
        </div>
        <!-- /main container -->

    </div>
    <script src='<%= ResolveClientUrl("~/Js/popAlert.js") %>'></script>
    <script src='<%= ResolveClientUrl("~/Content/admin_dark/sweetalert/sweetalert2.min.js") %>'></script>
      <script src="../Content/admin_dark/js/plugins/metismenu/jquery.metisMenu.js"></script>
    <script src="../Content/admin_dark/js/plugins/blockui-master/jquery-ui.js"></script>
    <script src="../Content/admin_dark/js/plugins/blockui-master/jquery.blockUI.js"></script>
    <!--nouiSlider-->
    <script src="../Content/admin_dark/js/plugins/nouislider/nouislider.min.js"></script>
    <!-- Input Mask-->
    <script src="../Content/admin_dark/js/plugins/jasny/jasny-bootstrap.min.js"></script>
    <!-- Select2-->
    <script src="../Content/admin_dark/js/plugins/select2/select2.full.min.js"></script>
    <!--Bootstrap ColorPicker-->
    <script src="../Content/admin_dark/js/plugins/colorpicker/bootstrap-colorpicker.min.js"></script>
    <!--Bootstrap DatePicker-->
    <script src="../Content/admin_dark/js/plugins/datepicker/bootstrap-datepicker.js"></script>
    <%--Select2 Jquery--%>
    <script src="../Content/admin_dark/js/plugins/select2/select2.full.min.js"></script>
    <!-- Data tables -->
    <script src="../Content/admin_dark/js/functions.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/dataTables.bootstrap.min.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/jquery.dataTables.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/jszip.min.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/pdfmake.min.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/vfs_fonts.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/extensions/Buttons/js/dataTables.buttons.min.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/extensions/Buttons/js/buttons.html5.js"></script>
    <script src="../Content/admin_dark/js/plugins/datatables/extensions/Buttons/js/buttons.colVis.js"></script>

    
         
</body>
</html>
