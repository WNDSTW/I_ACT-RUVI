<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tugas.ascx.cs" Inherits="I_ACT.Pages.Tugas" %>

<script>
    $(document).ready(function () {
        var ff = '<%=Session["idRole"] %>' 
        if (ff != '1')
        {
            //document.getElementById("Filter").style.visibility = "hidden";
            $("#Filter").remove();
        }
    });

    //////////////////////////////////////////////////////////////////////////
    var validFilesTypes = ['pdf','rar','zip'];
    function validateEvidence() {

        //make syntax ini kok error yaa jadi terpaksa diakalin
         var txKeterangan = $(".txKeteranganEvidence").val();
         var file = $(".uploadEvidence").val();
         var ext = file.substring(file.lastIndexOf(".") + 1, file.length).toLowerCase();
         var errMsg = "";
         var errCount = 0;
         var isValidFile = false;

         for (var i = 0; i < validFilesTypes.length; i++) {
             if (ext == validFilesTypes[i]) {
                 isValidFile = true;
                 break;
             }
         }

         if (txKeterangan == "") {
             errCount++;
             errMsg += "<li><b>Input Evidence Note !</b></li>";
         }
        
         if (!isValidFile) {
             errCount++;
             errMsg += "<li><b>File Must Be .pdf or .rar or .zip !</b></li>";
         }

         if (errCount > 0) {

             swal({
                 type: 'error',
                 title: 'Fill Required Fields !',
                 html: "<div id='boxValidasiEvidencee' class='alert alert-danger alert-dismissible' style='text-align:left !important;display: block;font-size:20px' role='alert'> <ul>" + errMsg + "</ul> </div>",
                 width: '800px',
             })
             //$('#boxValidasi').css('display', 'block');

             return false;
         } else {
             return true;
         }
    }

    /////////////////////////////////////////////////////////////
  
</script>


<div class="row">
    <div class="col-md-12 animatedParent animateOnce z-index-50">
        <div class="breadcrumbBack">
                   <ol class="breadcrumb breadcrumb-2">
                    <li><a href="http://localhost/I_ACT/Home/index"><i class="fa fa-home"></i>Home</a></li>
                    <li class="active"><strong>My Task</strong></li>
                    </ol>
        </div>
    </div>
</div>


         
 <asp:UpdatePanel runat="server" ID="ListPanel" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dgObject" />
                    <asp:AsyncPostBackTrigger ControlID="txSearch" />
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                     <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
<div class="row" runat="server" id="listTugas">
    <div class="col-md-12 animatedParent animateOnce z-index-50">
        <div class="panel panel-primary animated fadeInUp go">
            <div class="panel-heading clearfix">
                <div class="panel-title"  style="padding-top:5px"><b>MY TASK LIST</b></div>
                <ul class="panel-tool-options">
                  
                    <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                    <li><a data-rel="reload"  href="#"><i class="icon-arrows-ccw"></i></a></li>

                </ul>
            </div>
            <!-- panel body -->
            <div class="panel-body">
                 <div id="Filter" class="row">

                            <div class="col-sm-6">
                                <div class="panel panel-primary " style="margin-bottom:10px">
                                    <div class="panel-heading clearfix" style="padding:2px 5px 2px 5px; line-height:1;">
                                        <div class="panel-title">FILTER</div>
                                        <ul class="panel-tool-options">
                                            <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                                        </ul>
                                    </div>
                                    <!-- panel body -->
                                     <%--style="display:none"--%>
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
                                             <button type="button" class="btn btn-primary btn-md" id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick" style="float:right"><i class="fa fa-search fa-lg"></i></button>
                                            
                                        </div>
                                 </div>
                            
                            </div>
                            </div>
               
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
                                DataKeyField="NoNotulenDetail" AutoGenerateColumns="False" AllowSorting="true" OnSortCommand="dgObject_SortCommand"
                                Font-Names="Calibri" OnItemDataBound="dgObject_ItemDataBound"
                                AllowPaging="True"
                                PageSize="10" Width="100%" OnPageIndexChanged="dgObject_PageIndexChanged" OnItemCommand="dgObject_ItemCommand">
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
                                    <asp:TemplateColumn SortExpression="NoNotulenDetail" HeaderStyle-Width="15%"  HeaderText="Reg. Number" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                              <%-- <asp:HyperLink ID="linkNotulen" runat="server" NavigateUrl='<%# Eval("NoNotulen","~/Pages/Traser.aspx?menu=Master Jabatan&id=3&view={0}") %>'>
				                <asp:Label ID="lblNoNotulenDetail" runat="server" Text='<%# Eval("NoNotulenDetail") %>' /> </asp:HyperLink>--%>
                                                   <asp:LinkButton ID="linkNotulen" runat="server" CommandName="linkNotulen"
                                                        CommandArgument='<%#Eval("NoNotulenDetail")+ ";" +Eval("status") + ";" +Eval("noteDelegasi") %>' Text='<%#Eval("NoNotulenDetail")  %>'></asp:LinkButton>
                                                          
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="CreatedOn" HeaderText="Task Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("CreatedOn","{0:dd/MM/yyyy}") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn SortExpression="Subjek" HeaderStyle-Width="25%"  HeaderText="Subject" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblSubjek" runat="server" Text='<%# Eval("Subjek") %>' />
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
                                    <asp:TemplateColumn HeaderText="Delegate To" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                     <asp:Label ID="lblNamaDelegasi" runat="server" Visible="false" Text='<%# Eval("namaDelegasi") %>' />
                                                    <asp:Label ID="lblTglDelegasi" runat="server" Visible="false" Text='<%# Eval("TglDelegasi","{0:dd/MM/yyyy}") %>' />
                                                
                                                   <asp:Label ID="lblBy" runat="server" Visible="false" Text="By " />
                                                   <asp:Label ID="lblNamaPIC" runat="server" Visible="false" Text='<%# Eval("namaPIC") %>' />
                                                  <asp:LinkButton ID="btnDelegate" class="btn btn-warning btn-xs" runat="server" ToolTip="Delegate Task To"  CommandName="Delegate" CommandArgument='<%#Eval("noNotulenDetail") + ";" +Eval("status") + ";" +Eval("noteDelegasi")%>'><span><i class="fa fa-share fa-lg"></i></span></asp:LinkButton>
                                              
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="PIC" HeaderStyle-Width="25%"  HeaderText="PIC" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="namaPICC" runat="server" Text='<%# Eval("namaPIC") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>


                                      <asp:TemplateColumn SortExpression="NamaStatus" HeaderText="Status" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px;overflow:hidden;text-overflow:ellipsis;">
                                           
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

                    <div class="row" runat="server" id="addEvidence">
                        <div class="col-md-12 animatedParent animateOnce z-index-50">
                            <div class="panel panel-primary animated fadeInUp go">
                                <div class="panel-heading clearfix">
                                    <div class="panel-title" style="padding-top: 5px"><b>FOLLOW UP RECOMMENDATION FORM</b></div>
                                    <ul class="panel-tool-options">

                                        <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                                        <li><a data-rel="reload" href="#"><i class="icon-arrows-ccw"></i></a></li>

                                    </ul>
                                </div>

                                <!-- panel body -->
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">
                                                <table class="table table-hovered">
                                                    <tr style="border-bottom: 1px solid #353c42 !important">
                                                        <td>Task Number
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="view_lblNoNotulen"></asp:Label>
                                                            <asp:Label runat="server" Visible="false" ID="view_lblNoNotulenDetail"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: 1px dashed #353c42 !important">
                                                        <td>Recommendation Category
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="view_lblRekomendasi"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: 1px solid #353c42 !important">
                                                        <td>Source
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="view_lblSumber"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: 1px solid #353c42 !important">
                                                        <td>Title
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="view_lblJudulNotulen"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: 1px solid #353c42 !important">
                                                        <td>Ref. Doc. No
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="view_lblNoDokumen"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: 1px solid #353c42 !important">
                                                        <td>Attachments
                                                        </td>
                                                        <td>
                                                            <a style="color:navy" href="../Doc/<% =this.namaFile %>" download="<% =this.namaFile %>" target="_blank">
                                                            <asp:Label runat="server" ID="view_lblLampiran"></asp:Label></a>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: 1px solid #353c42 !important">
                                                        <td>Reviewer
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="view_lblReviewer"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: 1px solid #353c42 !important">
                                                        <td>Originator
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="view_lblConceptor"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="trNoteDelegate" runat="server" style="border-bottom: 1px solid #353c42 !important">
                                                        <td><b>Comments From <asp:Label ID="lblNamaPIC" runat="server"></asp:Label></b>
                                                        </td>
                                                        <td>
                                                            <b><asp:Label runat="server" ID="lblNoteDelegate"></asp:Label></b>
                                                        </td>
                                                    </tr>
                                                </table>
                                                </br>
                                   
                                            </div>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="row" id="EvidencePanel" runat="server" style="border: 3px dashed #353c42; padding-top: 10px; margin: 0.1%;">
                                        <div class="col-md-12">
                                             <asp:DataGrid ID="dgViewDetail" CssClass="GridBox" runat="server"
                                        DataKeyField="noNotulenDetail" AutoGenerateColumns="False"
                                        Font-Names="Calibri" Width="100%">
                                        <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridBoxPages"></PagerStyle>
                                        <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                        <ItemStyle CssClass="GridBoxItem" />
                                        <Columns>

                                            <asp:BoundColumn DataField="noNotulenDetail" Visible="false" HeaderText="noNotulenDetail" />
                                            <asp:BoundColumn DataField="Subjek" Visible="True" HeaderText="Subject" />
                                            <asp:BoundColumn DataField="Isi" Visible="True" HeaderText="Recommendation" />
                                            <asp:BoundColumn DataField="namaPIC" Visible="True" HeaderText="PIC" />
                                            <asp:BoundColumn DataField="namaFungsi" Visible="True" HeaderText="Fungsi" />
                                            <asp:BoundColumn DataField="TglJatuhTempo" Visible="True" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundColumn DataField="idPotential" Visible="True" HeaderText="Potential" />

                                        </Columns>
                                        <AlternatingItemStyle CssClass="GridBoxAltItem"></AlternatingItemStyle>
                                    </asp:DataGrid>
                                            <hr />
                                            </br>
                                            <h3>Add Evidence</h3>
                                            <div class="form-group">
                                                <label>Attachment: </label>

                                                <a style="color:navy" href="../Doc/<% =this.fileEvidence %>" download="<% =this.fileEvidence %>" target="_blank">
                                                <asp:Label runat="server" ID="linkEvidence"></asp:Label></a>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="uploadEvidence" ErrorMessage="File Required!" ForeColor="Red" ValidationGroup="btnSubmit">
                                                </asp:RequiredFieldValidator>

                                                <asp:FileUpload ID="uploadEvidence" runat="server" CssClass="uploadEvidence" />
                                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="uploadEvidence" ForeColor="Red"
                                                    ErrorMessage="Pdf/Rar/Zip Files Only" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.rar|.zip|.RAR|.ZIP)$" />

                                            </div>
                                            <div class="form-group">
                                                <label>Evidence Notes</label>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txKeteranganEvidence" ErrorMessage="*" ForeColor="Red" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txKeteranganEvidence" TextMode="MultiLine" Width="100%" Height="91px" ForeColor="Black" CssClass="txKeteranganEvidence"></asp:TextBox>
                                                
                                                <%--<textarea placeholder="Isi Rekomendasi" runat="server" id="txKeteranganEvidence" class="form-control txKeteranganEvidence" style="width: 100%; height: 91px;"></textarea>--%>
                                            </div>

                                            <br />
                                            <h3>Rejected History</h3>
                                            <asp:DataGrid ID="dgComment" CssClass="GridBox" runat="server"
                                                DataKeyField="idkomentar" AutoGenerateColumns="False" OnItemCommand="dgComment_ItemCommand"
                                                Font-Names="Calibri" Width="100%">
                                                <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridBoxPages"></PagerStyle>
                                                <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                                <ItemStyle CssClass="GridBoxItem" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <%# dgComment.PageSize * dgComment.CurrentPageIndex + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="idkomentar" Visible="false" HeaderText="idkomentar" />
                                                     <asp:BoundColumn DataField="CreatedOn" Visible="True" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                     <asp:TemplateColumn HeaderText="Evidence" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <div style="margin-left: 2px">
                                                                <asp:LinkButton ID="linkEvidence" runat="server" CommandName="linkEvidence" CommandArgument='<%#Eval("fname") %>' Text='<%#Eval("fname")  %>'></asp:LinkButton>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                                     

                                                    <asp:BoundColumn DataField="Komentar" Visible="True" HeaderText="Comments" />
                                                    <asp:BoundColumn DataField="CreatedBy" Visible="false" HeaderText="Originator" />
                                                   
                                                </Columns>
                                                <AlternatingItemStyle CssClass="GridBoxAltItem"></AlternatingItemStyle>
                                            </asp:DataGrid>
                                        </div>
                                    </div>

                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" id="Button3" onserverclick="btnClose_ServerClick">Close</button>
                                        <%--<button type="button" class="btn btn-primary" id="btnSubmit" runat="server" onclick="validateEvidence();"  onserverclick="btnSubmit_ServerClick">Submit Evidence</button>--%>
                                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" OnClick="btnSubmit_ServerClick" CausesValidation="true" OnClientClick="validateEvidence();" Text="SAVE" ValidationGroup="btnSubmit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                     </ContentTemplate>
                </asp:UpdatePanel>

<div id="modalDelegate" class="modal fade" tabindex="-2" role="dialog" style="display: none;">
  <div class="modal-dialog modal-lg">
      <div class="modal-content" style="color: #000">
          <asp:UpdatePanel runat="server" ID="PekerjaPanel" UpdateMode="Conditional">
              <ContentTemplate>
          <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
              <h4 class="modal-title" style="color: #000">DELEGATE TASK TO</h4>
          </div>
                    <div class="modal-body">
                      <div class="row">
                           <div class="col-md-12">
                              Delegate Reg. Number : <asp:Label runat="server" ID="pop_lblNoNotulenDetail"></asp:Label>
                               <div class="row">
                                   <div class="col-md-12">
                                        <div class="form-group">
                                                <label>Comment For Delegate</label>
                                                <asp:TextBox runat="server" ID="txCommentForDelegate" TextMode="MultiLine" Width="100%" Height="91px" ForeColor="Black" CssClass="txCommentForDelegate"></asp:TextBox>
                                        </div>
                                   </div>
                               </div>
                                <div class="row" style="margin-bottom: 10px;">
                                    <div class="col-sm-7">
                                       <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="form-control dropdownlist ddlEmployee" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                            <asp:ListItem Value="Sub" Text ="Subordinate"></asp:ListItem>
                                               <asp:ListItem Value="All" Text ="All Employee"></asp:ListItem>
                                        </asp:DropDownList>
                                     </div>
                                    <div class="col-sm-2" style="text-align: left; padding-top: 5px">
                                        <asp:Label runat="server" ID="Label2" Text="Search  :"></asp:Label>
                                    </div>
                                    <div class="col-sm-3" style="text-align: left">
                                        <asp:TextBox runat="server" CssClass="Materialize" ID="txSearchPekerja" AutoPostBack="true" OnTextChanged="txSearchPekerja_TextChanged"></asp:TextBox>
                                    </div>
                                </div> 
                               <div class="table-responsive">
                                   <asp:DataGrid ID="dgPopPekerja" CssClass="GridBox" runat="server"
                                       DataKeyField="NoPek" AutoGenerateColumns="False"
                                       Font-Names="Calibri" AllowPaging="True"
                                       PageSize="10" Width="100%" OnPageIndexChanged="dgPopPekerja_PageIndexChanged" OnItemCommand="dgPopPekerja_ItemCommand">
                                       <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridBoxPages"></PagerStyle>
                                       <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                       <ItemStyle CssClass="GridBoxItem" />
                                       <Columns>
                                           <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                               <ItemTemplate>
                                                   <%# dgPopPekerja.PageSize * dgPopPekerja.CurrentPageIndex + Container.ItemIndex + 1%>
                                               </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                           </asp:TemplateColumn>
                                           <asp:TemplateColumn>
                                               <HeaderTemplate>
                                                   <div style="margin-left: 6px">
                                                       <asp:Label ID="lblHeaderColumn3_noitempopPekerja" runat="server" Text='No Employee'></asp:Label>
                                                   </div>
                                               </HeaderTemplate>
                                               <ItemTemplate>
                                                   <div style="margin-left: 6px">
                                                       <asp:Label ID="lblContentHeaderColumn3_noitempopPekerja" runat="server" Text='<%# Eval("NoPek") %>' />
                                                   </div>
                                               </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                           </asp:TemplateColumn>
                                           <asp:TemplateColumn>
                                               <HeaderTemplate>
                                                   <div style="margin-left: 6px">
                                                       <asp:Label ID="lblHeaderColumn3_namaitempopPekerja" runat="server" Text='Name'></asp:Label>
                                                   </div>
                                               </HeaderTemplate>
                                               <ItemTemplate>
                                                   <div style="margin-left: 6px">
                                                       <asp:Label ID="lblContentHeaderColumn3_namaitempopPekerja" runat="server" Text='<%# Eval("NamaPegawai") %>' />
                                                   </div>
                                               </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                           </asp:TemplateColumn>
                                           <asp:TemplateColumn>
                                               <HeaderTemplate>
                                                   <div style="margin-left: 6px">
                                                       <asp:Label ID="lblHeaderColumn4_aksipopPekerja" runat="server" Text='Action'></asp:Label>
                                                   </div>
                                               </HeaderTemplate>
                                               <ItemTemplate>
                                                   <div style="margin-left: 6px">
                                                       <asp:LinkButton ID="popPekerja_btnChoose" class="buttonGrid small-button transparent" runat="server" ToolTip="Select" CommandName="Select" CommandArgument='<%#Eval("NoPek") %>'><span><i class="fa fa-check-square fa-lg"></i></span></asp:LinkButton>
                                                   </div>
                                               </ItemTemplate>
                                               <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                               <HeaderStyle HorizontalAlign="Center" />
                                           </asp:TemplateColumn>
                                       </Columns>
                                       <AlternatingItemStyle CssClass="GridBoxAltItem"></AlternatingItemStyle>
                                   </asp:DataGrid>

                               </div>
                           </div>
                      </div>
                  </div>

                  <div class="modal-footer">

                      <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" id="Button2" onserverclick="btnClose_ServerClick">Close</button>
                      </div>
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dgPopPekerja" />
                  <asp:AsyncPostBackTrigger ControlID="txSearchPekerja" />
                  <asp:AsyncPostBackTrigger ControlID="ddlEmployee" />
                  
              </Triggers>
          </asp:UpdatePanel>
                
              
      </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div>


<div id="modalView" class="modal fade" tabindex="-1" role="dialog" style="display: none;">
  <div class="modal-dialog modal-lg">
      <div class="modal-content" style="color: #fff">
          <asp:UpdatePanel runat="server" ID="viewPanel" UpdateMode="Conditional">
              <ContentTemplate>
           <form class="form-horizontal">
          <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
              <h4 class="modal-title" style="color: #fff">VIEW TASK</h4>
          </div>
                    <div class="modal-body">
                    
                     
                  </div>

                  <div class="modal-footer">
                      <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" id="Button1" onserverclick="btnClose_ServerClick">Close</button>
                   <button type="button" class="btn btn-primary" id="btnSubmitEvidence" runat="server" onclick="validateEvidence();"  onserverclick="btnSubmit_ServerClick">Submit Evidence</button>
                    
                  </div>
                  </form>
              </ContentTemplate>
              <Triggers>
                  <asp:PostBackTrigger ControlID="btnSubmitEvidence" />
              </Triggers>
          </asp:UpdatePanel>
               
      </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div>



