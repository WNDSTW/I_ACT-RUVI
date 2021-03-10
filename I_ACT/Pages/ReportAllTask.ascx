<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportAllTask.ascx.cs" Inherits="I_ACT.Pages.ReportAllTask" %>
localhost/I_ACT
<%--Tambah tindak lanjut--%>
<%--function UploadFile(fileUpload) {
        if (fileUpload.value != '') {
            document.getElementById("<%=btnUpload.ClientID %>").click();
        }
    }--%>



<div class="row">
    <div class="col-md-12 animatedParent animateOnce z-index-50">
        <div class="breadcrumbBack">
                   <ol class="breadcrumb breadcrumb-2">
                    <li><a href="http://172.20.26.5/iact/Home/index"><i class="fa fa-home"></i>Home</a></li>
                    <li class="active"><strong>Task Report</strong></li>
                    </ol>
        </div>
    </div>
</div>


         
<asp:UpdatePanel runat="server" ID="ListPanel" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="dgObject" />
        <asp:AsyncPostBackTrigger ControlID="txSearch" />
        <asp:PostBackTrigger ControlID="btnExcel" />
    </Triggers>
    <ContentTemplate>
        <div class="row" id="TaskListPanel" runat="server" visible="true">
            <div class="col-md-12 animatedParent animateOnce z-index-50">
                <div class="panel panel-primary animated fadeInUp go">
                    <div class="panel-heading clearfix">
                        <div class="panel-title" style="padding-top: 5px"><b>TASK LIST</b></div>
                        <ul class="panel-tool-options">
                            
                            <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>

                        </ul>
                    </div>
                    <!-- panel body -->
                    <div class="panel-body">

                        <div class="row">

                            <div class="col-sm-6">
                                <div class="panel panel-primary" style="margin-bottom:10px">
                                    <div class="panel-heading clearfix" style="padding:2px 5px 2px 5px; line-height:1;">
                                        <div class="panel-title">FILTER</div>
                                        <ul class="panel-tool-options">
                                            <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                                        </ul>
                                    </div>
                                    <!-- panel body -->
                                    <div class="panel-body">
                                         <div class="form-group">
                                          <b>Recommendation :</b>
                                              <asp:DropDownList runat="server" ID="filter_ddlRekomendasi" OnTextChanged="filter_ddlRekomendasi_TextChanged" AutoPostBack="true" CssClass="dropdownlist" style="width:100%">
                                </asp:DropDownList>
                                         </div>
                                         <div class="form-group">
                                          <b>Source :</b>
                                              <asp:DropDownList runat="server" ID="filter_ddlSource" OnTextChanged="filter_ddlSource_TextChanged" AutoPostBack="true" CssClass="dropdownlist" style="width:100%">
                                </asp:DropDownList>
                                         </div>
                                         <div class="form-group">
                                           <label for="txTglAwal">Date :</label>
                                             <%--<asp:TextBox ID="txTglAwal" runat="server" CssClass="clsDate" data-date-format="dd/mm/yyyy" OnTextChanged="txTglAwal_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                            <input class=" txTglAwal clsDate" data-date-format="dd/mm/yyyy" id="txTglAwal" runat="server" type="text">
                                            - 
                                             
                                             <%--<asp:TextBox ID="txTglAkhir" runat="server" CssClass="clsDate" data-date-format="dd/mm/yyyy" OnTextChanged="txTglAkhir_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                            <input class=" txTglAkhir clsDate" data-date-format="dd/mm/yyyy" id="txTglAkhir" runat="server" type="text">
                           
                                        </div>
                              
                                        </div>
                                 </div>
                            
                            </div>
                            </div>

                        <div class="row" style="margin-bottom: 10px;">

                            <div class="col-sm-8" style="text-align:left">
                                 <button type="button" class="form-control btn btn-primary" id="btnExcel" runat="server" onserverclick="btnExcel_ServerClick" style="width:160px"><i class="fa fa-file" style="float: left !important"></i>Export To Excel </button>
                                            
                            </div>
                            <div class="col-sm-1" style="text-align: left; padding-top: 5px">
                                <asp:Label runat="server" ID="Label1" Text="Search  :"></asp:Label>
                            </div>
                            <div class="col-sm-3" style="text-align: left">
                                <asp:TextBox runat="server" CssClass="Materialize" ID="txSearch" AutoPostBack="true" OnTextChanged="txSearch_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <asp:DataGrid ID="dgObject" CssClass="GridCustom" runat="server" OnItemCreated="dgObject_ItemCreated"
                                DataKeyField="NoNotulenDetail" AutoGenerateColumns="False" AllowSorting="true" OnSortCommand="dgObject_SortCommand"
                                Font-Names="Calibri" OnItemDataBound="dgObject_ItemDataBound"
                                AllowPaging="True"
                                PageSize="10" Width="100%" OnPageIndexChanged="dgObject_PageIndexChanged" >
                                <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridCustomPages"></PagerStyle>
                                <HeaderStyle CssClass="GridCustomHeader"></HeaderStyle>
                                <ItemStyle Wrap="false" CssClass="GridCustomItem" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">

                                        <ItemTemplate>
                                            <%# dgObject.PageSize * dgObject.CurrentPageIndex + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NoNotulenDetail" HeaderStyle-Width="15%" HeaderText="Reg. Number" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                         
                                                <asp:Label ID="lblNoNotulenDetail" runat="server" Text='<%#Eval("NoNotulenDetail")  %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NoDokumen" HeaderStyle-Width="15%" HeaderText="Ref. Doc. Number" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">

                                                <asp:Label ID="lblDokumen" runat="server" Text='<%#Eval("NoDokumen")  %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="CreatedOn" HeaderText="Task Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("tglNotulen","{0:dd/MM/yyyy}") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn SortExpression="Title" HeaderStyle-Width="10%" HeaderText="Title" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("JudulNotulen") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="Subjek" HeaderStyle-Width="25%" HeaderText="Subject" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblSubjek" runat="server" Text='<%# Eval("Subjek") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn SortExpression="Rekomendasi" HeaderStyle-Width="25%" HeaderText="Recommendations" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblRekomendasi" runat="server" Text='<%# Eval("Isi") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="TglJatuhTempo" HeaderText="Due Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblTglJatuhTempo" runat="server" Text='<%# Eval("TglJatuhTempo","{0:dd/MM/yyyy}") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NamaPIC" HeaderText="PIC" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblNamaPIC" runat="server" Text='<%# Eval("NamaPIC") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>

                                     <asp:TemplateColumn SortExpression="IdPotential" HeaderStyle-Width="5%" HeaderText="Potential" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblPotential" runat="server" Text='<%# Eval("idPotential") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="NamaStatus" HeaderText="Status" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
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
                                                <asp:Label ID="lblnamaStatus" runat="server" />

                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="TglClosed" HeaderText="Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblTglClosed" runat="server" Text='<%# Eval("TglClosed","{0:dd/MM/yyyy}") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <AlternatingItemStyle CssClass="GridCustomAltItem"></AlternatingItemStyle>
                            </asp:DataGrid>

                        </div>

                    </div>
                </div>
            </div>
        </div>


    </ContentTemplate>
</asp:UpdatePanel>



