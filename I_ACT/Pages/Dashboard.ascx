<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="I_ACT.Pages.Dashboard" %>

<%@ Register Src="~/Pages/LoadDashboard.ascx" TagName="loadDashboard" TagPrefix="uc2" %>

<div class="row">
    <div class="col-md-12 animatedParent animateOnce z-index-50">
        <div class="breadcrumbBack">
                   <ol class="breadcrumb breadcrumb-2">
                    <li><a href="http://intra-ru6.pertamina.com/iact/Dashboard/0"><i class="fa fa-home"></i>Home</a></li>
                    <li class="active"><strong>Dashboard</strong></li>
                    </ol>
        </div>
    </div>
</div>
<%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
<asp:UpdatePanel runat="server" ID="contentPanel" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="TabCtr" />
    </Triggers>
    <ContentTemplate>
        <%--<div class="row">
            <asp:Button runat="server" ID="btnShow" OnClick="btnShow_Click" />
        </div>--%>
        <div class="row">
            <div class="col-md-12 animatedParent animateOnce z-index-50">
                  <ajax:TabContainer ID="TabCtr" runat="server"  OnActiveTabChanged="TabCtr_ActiveTabChanged" CssClass="MyTab" AutoPostBack="true">
                      
                   </ajax:TabContainer>   
                </div>
        </div>
        
    </ContentTemplate>
</asp:UpdatePanel>

 <asp:UpdateProgress runat="server" ID="upProgress" DynamicLayout="true" AssociatedUpdatePanelID="contentPanel">
             <ProgressTemplate>
                 <div class="progress">
                    <div class="center">
                         <img alt="" src="../Images/loading.gif" />
                        <br />
                        Please Wait ...
                    </div>
                 </div>
             </ProgressTemplate>

         </asp:UpdateProgress>


<div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-primary animated fadeInUp go">
                                                <div class="panel-heading clearfix">
                                                    <div class="panel-title" style="padding-top: 5px"><b>Task Status</b></div>
                                                    <ul class="panel-tool-options">
                                                        <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                                                    </ul>
                                                </div>
                                                <!-- panel body -->
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div id="colChart" class="col-md-4">

                                                            <canvas id="PieChart" width='100' height='100'></canvas>
                                                        </div>
                                                        <asp:UpdatePanel runat="server" ID="Persen" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="col-md-6">
                                                            <div class="row">
                                                                <table class="table bordered" style="height: auto">
                                                                    <tr>
                                                                        <td>Open
                                                                        </td>
                                                                        <td>
                                                                            <%  =this.PersenOpen.ToString("F") %> %
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Waiting Approval
                                                                        </td>
                                                                        <td>
                                                                            <% =this.PersenWaitingApproval.ToString("F") %> %
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Closed
                                                                        </td>
                                                                        <td>
                                                                            <% =this.PersenClosed.ToString("F") %> %
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Overdue
                                                                        </td>
                                                                        <td>
                                                                            <% =this.PersenOverdue.ToString("F") %> %
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Closed Overdue
                                                                        </td>
                                                                        <td>
                                                                            <% =this.PersenClosedOverdue.ToString("F") %> %
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Reject
                                                                        </td>
                                                                        <td>
                                                                            <% =this.Reject.ToString("F") %> %
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>


                                                        </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        
                                                    </div>
                                                    <div id="RowChartFungsi" class="row">
                                                        <div class="row">
                                                            <canvas id="FungsiChart" width="50" height="10"></canvas>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                        </div>
                                    </div>

<div id="modalView" class="modal fade" tabindex="-2" role="dialog" style="display: none;">
        <div class="modal-dialog modal-lg" style="width:1500px;">
            <div class="modal-content" style="color: #000">
                <asp:UpdatePanel runat="server" ID="viewPanel" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dgViewDetail" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-body" >
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <br></br>
                                        <asp:DataGrid ID="dgViewDetail" CssClass="GridBox" runat="server"
                                            DataKeyField="noNotulenDetail" AutoGenerateColumns="False"
                                            Font-Names="Calibri" Width="100%" OnItemDataBound="dgViewDetail_ItemDataBound1">
                                            <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridBoxPages"></PagerStyle>
                                            <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                            <ItemStyle CssClass="GridBoxItem" />
                                            <Columns>
                                               <%-- <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        <div style="margin-left: 2px">
                                                            <asp:Label ID="lblHeaderColumn4_z" runat="server" Text='Action'></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px">
                                                            <asp:LinkButton ID="BtnEditData" CssClass="btn btn-warning btn-xs" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%#Eval("NoNotulenDetail") %>'><i class="fa fa-pencil fa-lg"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="BtnDelete" CssClass="btn btn-danger btn-xs" runat="server" ToolTip="Delete" OnClientClick="return confirmDelete(this, event);" CommandName="Delete" CommandArgument='<%#Eval("NoNotulenDetail") %>'><i class="fa fa-trash fa-lg"></i></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>--%>
                                                 <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">

                                        <ItemTemplate>
                                            <%# dgViewDetail.PageSize * dgViewDetail.CurrentPageIndex + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateColumn>
                                                 <asp:TemplateColumn SortExpression="Title" HeaderStyle-Width="10%" HeaderText="Title" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("JudulNotulen") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Subjek" HeaderStyle-Width="100px" HeaderText="Subject" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis; width: 200px !important">
                                                            <asp:Label ID="lblSubjek" runat="server" Text='<%# Eval("Subjek") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                               <%-- <asp:TemplateColumn SortExpression="NoNotulenDetail" HeaderStyle-Width="15%" HeaderText="Reg. Number" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <asp:LinkButton ID="linkNotulen" runat="server" CommandName="linkNotulen" CommandArgument='<%#Eval("NoNotulenDetail") %>' Text='<%#Eval("NoNotulenDetail")  %>'></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>--%>
                                                <asp:TemplateColumn SortExpression="NoDokumen" HeaderStyle-Width="15%" HeaderText="Tindak Lanjut" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemStyle Wrap="true" />
                                                    <ItemTemplate>
                                                        <div style="word-wrap: ; width : 200px; margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">

                                                            <asp:Label ID="lblisi" runat="server" Text='<%#Eval("Isi")  %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                 
                                                <asp:TemplateColumn SortExpression="NoDokumen" HeaderStyle-Width="15%" HeaderText="Refference" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">

                                                            <asp:Label ID="lblDokumen" runat="server" Text='<%#Eval("NoDokumen")  %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <%--<asp:TemplateColumn SortExpression="CreatedOn" HeaderText="Task Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("tglNotulen","{0:dd/MM/yyyy}") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>--%>
                                              

                                                <asp:TemplateColumn SortExpression="TglJatuhTempo" HeaderText="Due Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <asp:Label ID="lblTglJatuhTempo" runat="server" Text='<%# Eval("TglJatuhTempo","{0:dd/MM/yyyy}") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="NamaPIC" HeaderText="PIC" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <asp:Label ID="lblNamaPIC" runat="server" Text='<%# Eval("NamaPIC") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn SortExpression="IdPotential" HeaderStyle-Width="5%" HeaderText="Risk Rating" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <asp:Label ID="lblPotential" runat="server" Text='<%# Eval("namaPotential") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>

                                                <asp:TemplateColumn SortExpression="NamaStatus" HeaderText="Status" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <%--<%if ( Eval("Status") == "0") { %>
                                                    <span class="label label-success">Open</span>
                                                <%} %>
                                                    <%else if (DataBinder.Eval("Status") == "1")
                                                  { %>
                                                    <span class="label label-info">Waiting Approval</span>
                                                    <%}
                                                  else if (Eval("Status") == "2")
                                                  { %>
                                                    <span class="label label-primary">Closed</span>
                                                    <%}
                                                  else if (Eval("Status") == "3")
                                                  { %>
                                                    <span class="label label-danger">Overdue</span>
                                                    <%}
                                                  else if (Eval("Status") == "4")
                                                  { %>
                                                    <span class="label label-warning">Closed Overdue</span>
                                                    <%} %>--%>
                                                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status") %>' />
                                                            <asp:Label ID="lblCreatedBy" runat="server" Visible="false" Text='<%# Eval("CreatedBy") %>' />
                                                            <asp:Label ID="lblnamaStatus" runat="server" />

                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                           <%--     <asp:TemplateColumn SortExpression="TglClosed" HeaderText="Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <div style="margin-left: 2px; overflow: hidden; text-overflow: ellipsis;">
                                                            <asp:Label ID="lblTglClosed" runat="server" Text='<%# Eval("TglClosed","{0:dd/MM/yyyy}") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>--%>

                                            </Columns>
                                            <AlternatingItemStyle CssClass="GridBoxAltItem"></AlternatingItemStyle>
                                        </asp:DataGrid>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">

                            <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" id="Button1" onserverclick="Button1_ServerClick">Close</button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

<style type="text/css">

     .MyTab a
     {
         color :blue !important;
     }
</style>
   <script type="text/javascript" >
      
       function ShowModal() {
           $('#modalView').modal('show');
       }
       var chartbar;
       var ChartPie;

       function loadPie(open, wApprove, Closed, Overdue, cOverdue, Reject, pieName) {
           //$('#colChart').html("<canvas id='" + pieName + "' width='100' height='100'></canvas>");
           var configPie = {
               type: 'pie',
               data: {
                   datasets: [{
                       data: [open, wApprove, Closed, Overdue, cOverdue, Reject],
                       backgroundColor: [
                          "#99c24d",
                          "#c128a1",
                          "#048ba8",
                          "#fb4d3d",
                          "#f0a202",
                          "#4f544e"
                       ], datalabels: {
                           display: false
                       }
                   }],

                   labels: [
                       "Open",
                       "Waiting Approval",
                       "Closed",
                       "Overdue",
                       "Closed Overdue",
                       "Reject"
                   ],

                   percentage: [
                       open,
                       wApprove,
                       Closed,
                       Overdue,
                       cOverdue,
                       Reject
                   ]
               },
               options: {
                   responsive: true,
                   animation: {
                       duration: 2000,
                       easing: 'easeInQuart'
                   },
                   legend: {
                       position: 'bottom',
                       display: true,
                   },
                   pieceLabel: {
                       render: 'percentage',
                       fontColor: ['black', 'black'],
                       fontSize: 15,
                       fontStyle: 'bold',
                       precision: 1
                   },
                   title: {
                       display: true,
                       text: 'Status Task',
                       position: 'top'
                   }


               }
           };
           if (ChartPie) {
               ChartPie.destroy();
           }
           var ctxO = document.getElementById('PieChart').getContext('2d');
           ChartPie = new Chart(ctxO, configPie);
       }

       function loadBar(fungsi, rColor, bColor, total) {
           var speedData = {
               labels: fungsi,
               fillOpacity: .3,
               datasets: [{
                   label: "Outstanding",
                   backgroundColor: rColor, datalabels: {
                       display: true
                   },
                   //hoverBackgroundColor: bColor,
                   //borderWidth: 1,
                   data: total,
               }]
           };
           var chartOptions1 = {
               tooltips: {
                   enabled: false
               },
               responsive: true,
               title: {
                   display: true,
                   text: 'Outstanding Tindak Lanjut (Open & Overdue)'
               },
               legend: {
                   display: false,
                   position: 'top',
                   labels: {
                       boxWidth: 80,
                       fontColor: 'black'
                   }
               },
               scales: {
                   xAxes: [{
                       beginAtZero: true,
                       ticks: {
                           autoSkip: false
                       }
                   }],
                   yAxes: [{
                       ticks: {
                           beginAtZero: true
                       }
                   }]
               },
               //animation : {
               //    onComplete: function () {
               //        var chartInstance = this.chart,
               //         ctx = chartInstance.ctx;

               //        ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
               //        ctx.textAlign = 'center';
               //        ctx.textBaseline = 'bottom';

               //        this.data.datasets.forEach(function (dataset, i) {
               //            var meta = chartInstance.controller.getDatasetMeta(i);
               //            meta.data.forEach(function (bar, index) {
               //                var data = dataset.data[index];
               //                ctx.fillText(data, bar._model.x, bar._model.y - 5);
               //            });
               //        });
               //    }
               //}
           };
           if (chartbar) {
               chartbar.destroy();
           };
          
           var ctxO0 = document.getElementById('FungsiChart').getContext('2d');
           chartbar = new Chart(ctxO0, {
               type: 'bar',
               data: speedData,
               responsive: true,
               options: chartOptions1
            
           });
       }
   </script>