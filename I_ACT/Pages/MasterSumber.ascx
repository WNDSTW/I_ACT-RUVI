<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MasterSumber.ascx.cs" Inherits="I_ACT.Pages.MasterSumber" %>

<script>
    function validateData() {

        //make syntax ini kok error yaa jadi terpaksa diakalin
        //var txNama_add = document.getElementById('<%=txNama.ClientID%>').value;
        var txNama = $(".txNama").val();
        var txSingkatan = $(".txSingkatan").val();
        var ddlRekomendasi = $(".ddlRekomendasi").val();
        var errMsg = "";
        var errCount = 0;

        if (ddlRekomendasi == "-- Select Recommendation --") {
            errCount++;
            errMsg += "<li><b>Choose Recommendation !</b></li>";
        }

        if (txNama == "") {
            errCount++;
            errMsg += "<li><b>Input Source Name !</b></li>";
        }
        if (txSingkatan == "") {
            errCount++;
            errMsg += "<li style='text-align:left'><b>Input Source Code !</b></li>";
        }

        
        if (errCount > 0) {

            swal({
                type: 'error',
                title: 'Input All Required Fields!',
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


<div class="row">
    <div class="col-md-12 animatedParent animateOnce z-index-50">
        <div class="breadcrumbBack">
                   <ol class="breadcrumb breadcrumb-2">
                    <li><a href="http://172.20.26.5/iact/Home/index"><i class="fa fa-home"></i>Home</a></li>
                    <li><a href="#">Master Data</a></li>
                    <li class="active"><strong>Master Source</strong></li>
                    </ol>
        </div>
    </div>
</div>


         

<div class="row">
    <div class="col-md-12 animatedParent animateOnce z-index-50">
        <div class="panel panel-primary animated fadeInUp go">
            <div class="panel-heading clearfix">
                <div class="panel-title"  style="padding-top:5px"><b>LIST SOURCE</b></div>
                <ul class="panel-tool-options">
                    <li>
                        <button type="button" class="btn btn-primary"   id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick"><i class="fa fa-plus fa-lg"></i></button>
                    </li>
                    <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                    <li><a data-rel="reload"  href="#"><i class="icon-arrows-ccw"></i></a></li>

                </ul>
            </div>
            <!-- panel body -->
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="ListPanel" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dgObject" />
                    <asp:AsyncPostBackTrigger ControlID="txSearch" />
                </Triggers>
                <ContentTemplate>
                    <div class="row" style="margin-bottom: 10px;">
                        
                        <div class="col-sm-8"></div>
                        <div class="col-sm-1" style="text-align: left; padding-top: 5px">
                            <asp:Label runat="server" ID="Label1" Text="Search  :"></asp:Label>
                        </div>
                        <div class="col-sm-3" style="text-align: left">
                            <asp:TextBox runat="server" CssClass="Materialize" ID="txSearch" AutoPostBack="true" OnTextChanged="txSearch_TextChanged"></asp:TextBox>
                        </div>
                    </div> 
                <div class="table-responsive">
					 <asp:DataGrid ID="dgObject" CssClass="GridCustom" runat="server" OnItemCreated="dgObject_ItemCreated"
                                DataKeyField="idSumber" AutoGenerateColumns="False" AllowSorting="true" OnSortCommand="dgObject_SortCommand"
                                Font-Names="Calibri" OnItemDataBound="dgObject_ItemDataBound"
                                AllowPaging="True"
                                PageSize="10" Width="100%" OnPageIndexChanged="dgObject_PageIndexChanged" OnItemCommand="dgObject_ItemCommand">
                                <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridCustomPages"></PagerStyle>
                                <HeaderStyle CssClass="GridCustomHeader"></HeaderStyle>
                                <ItemStyle CssClass="GridCustomItem" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">

                                        <ItemTemplate>
                                            <%# dgObject.PageSize * dgObject.CurrentPageIndex + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderStyle-Width="40%" SortExpression="NamaSumber" HeaderText="Source Name" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px">
                                                <asp:Label ID="lblContentNamaSumber" runat="server" Text='<%# Eval("NamaSumber") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderStyle-Width="40%" SortExpression="SingkatanSumber" HeaderText="Source Code" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">

                                        <ItemTemplate>
                                            <div style="margin-left: 2px">
                                                <asp:Label ID="lblContentSingkatanSumber" runat="server" Text='<%# Eval("SingkatanSumber") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                       <asp:TemplateColumn HeaderStyle-Width="40%" SortExpression="isRAM" HeaderText="Use RAM" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">

                                        <ItemTemplate>
                                            <div style="margin-left: 2px">
                                                <asp:CheckBox runat="server" Enabled="false" ID="ckRAM" Checked='<%# Eval("isRAM") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <div style="margin-left: 2px">
                                                <asp:Label ID="lblHeaderColumn4_z" runat="server" Text='Action'></asp:Label>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="margin-left: 2px">
                                                <asp:LinkButton  ID="BtnEdit" CssClass="btn btn-warning btn-xs" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%#Eval("idSumber") %>'><i class="fa fa-pencil fa-lg"></i></asp:LinkButton>
                                                <asp:LinkButton  ID="BtnDelete" CssClass="btn btn-danger btn-xs" runat="server" ToolTip="Delete" OnClientClick="return confirmDelete(this, event);" CommandName="Delete" CommandArgument='<%#Eval("idSumber") %>'><i class="fa fa-trash fa-lg"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <AlternatingItemStyle CssClass="GridCustomAltItem"></AlternatingItemStyle>
                            </asp:DataGrid>
                  
				</div>    
                    
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</div>


<div id="modalAdd" class="modal fade" tabindex="-1" role="dialog" style="display: none;">
  <div class="modal-dialog modal-lg">
      <div class="modal-content" style="color: #000">
          <asp:UpdatePanel runat="server" ID="addPanel" UpdateMode="Conditional">
              <ContentTemplate>
          <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
              <h4 class="modal-title" style="color: #000"><asp:Label runat="server" ID="lblJudul"></asp:Label> SOURCE</h4>
          </div>
                    <div class="modal-body">
                      <form class="form-horizontal">
                          <div class="form-group">
                              <label for="txNama">Recommendation Name</label>
                             <asp:DropDownList runat="server" ID="ddlRekomendasi" CssClass="form-control dropdownlist ddlRekomendasi">
                               
                             </asp:DropDownList>
                          </div>
                          <div class="form-group">
                              <label for="txNama">Source Name</label>
                              <input class="form-control txNama" id="txNama" runat="server" placeholder="Input Source Name" type="text">
                               <input class="form-control txId" id="txId" runat="server" style="display:none" type="text">
                               
                          </div>
                          <div class="form-group">
                              <label for="txSingkatan">Source Code</label>
                              <input class="form-control txSingkatan" id="txSingkatan" runat="server" placeholder="Input Source Code" type="text">
                          </div>
                           <div class="form-group">
                              <label for="txSingkatan">Use RAM</label>
                             <asp:DropDownList runat="server" ID="ddlRAM" CssClass="form-control dropdownlist ddlRAM">
                               <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                 <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                             </asp:DropDownList></div>
                      </form>
                  </div>

                  <div class="modal-footer">

                      <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" id="btnClose" onserverclick="btnClose_ServerClick">Close</button>
                      <button type="button" class="btn btn-primary" id="btnSave" runat="server" onclick="validateData();" onserverclick="btnSave_ServerClick">Save</button>
                        <button type="button" class="btn btn-primary" id="btnUpdate" runat="server" onclick="validateData();" onserverclick="btnUpdate_ServerClick">Update</button>
                  </div>
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btnSave" />
                  <asp:AsyncPostBackTrigger ControlID="btnAdd" />
              </Triggers>
          </asp:UpdatePanel>
                
              
      </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div>
