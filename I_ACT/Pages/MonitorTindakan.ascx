
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MonitorTindakan.ascx.cs" Inherits="I_ACT.Pages.MonitorTindakan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%--Tambah tindak lanjut--%>
<%--function UploadFile(fileUpload) {
        if (fileUpload.value != '') {
            document.getElementById("<%=btnUpload.ClientID %>").click();
        }
    }--%>
<script type="text/javascript">
    var errorMessage;
    window.onload = function () {
        errorMessage =document.getElementById("<%=lblMsg.ClientID %>").value;
        document.getElementById("<%=lblMsg.ClientID %>").value = "";
        AjaxControlToolkit.AsyncFileUpload.prototype._onStart = function () {
            var valid = this.raiseUploadStarted(new AjaxControlToolkit.AsyncFileUploadEventArgs(this._inputFile.value, null, null, null));
            if (typeof valid == 'undefined') {
                valid = true;
            }
            if (valid) {
                valid = Validate(this._inputFile.value);
                if (!valid) {
                    this._innerTB.value = "";
                    this._innerTB.style.backgroundColor = this._completeBackColor;
                }
            }
            return valid;
        }
    }
    var validFilesTypes = ["pdf", "rar", "zip"];
    function Validate(path) {
        $get("<%=lblMsg.ClientID%>").innerHTML = "";
        var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
        var isValidFile = false;
        for (var i = 0; i < validFilesTypes.length; i++) {
            if (ext == validFilesTypes[i]) {
                isValidFile = true;
                break;
            }
        }
        if (!isValidFile) {
            $get("<%=lblMsg.ClientID %>").innerHTML = errorMessage;
        }
        return isValidFile;
    }

    function uploadComplete() {
        swal({
            type: 'success',
            title: 'File Uploaded Succesfully !',
            width: '800px',
        })

        document.getElementById("<%=fileUpload1.ClientID%>").value = "File Ready";
    }
    // This function will execute if file upload fails
    function uploadError() {
        swal({
            type: 'error',
            title: 'File Upload Error !',
            width: '800px',
        })
    }
    

    function pilihRAM(pro, sev) {
        var potensial = pro * sev;
        document.getElementById("<%=txPotensial.ClientID%>").value = pro * sev;
        document.getElementById("<%=txProbability.ClientID%>").value = pro;
        document.getElementById("<%=txSeverity.ClientID%>").value = sev;
        if ( potensial==1 || potensial==2)
        {
            document.getElementById("<%=txnamaRisk.ClientID%>").value = "Low";
        } else if (potensial >=3  && potensial <= 6) {
            document.getElementById("<%=txnamaRisk.ClientID%>").value = "Medium";
        } else if (potensial >= 7 && potensial <= 12) {
            document.getElementById("<%=txnamaRisk.ClientID%>").value = "High";
        } else if (potensial >= 13 && potensial <= 25) {
            document.getElementById("<%=txnamaRisk.ClientID%>").value = "Extreme";
        }
    }
    function ddlRAMChanged() {

        var ddlSeverity = $(".ddlSeverity").val();
        var ddlProbability = $(".ddlProbability").val();
        var txPotensial = $(".txPotensial").val();

        txPotensial = ddlSeverity * ddlProbability;
        document.getElementById("<%=txPotensial.ClientID%>").value = txPotensial;
    }

    var validFilesTypes = ['pdf', 'rar', 'zip'];
    function validateHead() {

        //make syntax ini kok error yaa jadi terpaksa diakalin
        //var txNama_add = document.getElementById('<%=txJudul.ClientID%>').value;
        var txJudul = $(".txJudul").val();
        var txNoDokumen = $(".txNoDokumen").val();
        var ddlRekomendasi = $(".ddlRekomendasi").val();
        var ddlSumber = $(".ddlSumber").val();
        var ddlReviewer = $(".ddlReviewer").val();
        var file = $(".fileUpload1").val();
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

        //if (!isValidFile) {
        //    errCount++;
        //    errMsg += "<li><b>Attachment Must Be .pdf or .rar or .zip !</b></li>";
        //}

        if (ddlRekomendasi == "-- Select Recommendation --") {
            errCount++;
            errMsg += "<li><b>Select Recommendation !</b></li>";
        }
        if (ddlSumber == "-- Select Source --") {
            errCount++;
            errMsg += "<li><b>Select Source !</b></li>";
        }
        //if (ddlReviewer == "-- Select Reviewer --") {
        //    errCount++;
        //    errMsg += "<li><b>Select Reviewer !</b></li>";
        //}
        if (txJudul == "") {
            errCount++;
            errMsg += "<li><b>Input Title !</b></li>";
        }
        if (txNoDokumen == "") {
            errCount++;
            errMsg += "<li style='text-align:left'><b>Input Ref. Doc. Number !</b></li>";
        }

        
        if (errCount > 0) {

            swal({
                type: 'error',
                title: 'Fill All Required Fields !',
                html: "<div id='boxValidasiHead' class='alert alert-danger alert-dismissible' style='text-align:left !important;display: block;font-size:20px' role='alert'> <ul>" + errMsg + "</ul> </div>",
                width: '800px',
            })
            //$('#boxValidasi').css('display', 'block');

            return false;
        } else {
            return true;
        }
    }
 

    function validateDetail() {

        //make syntax ini kok error yaa jadi terpaksa diakalin
        //var txNama_add = document.getElementById('<%=txJudul.ClientID%>').value;
         var txSubjek = $(".txSubjek").val();
         var txTglJatuhTempo = $(".txTglJatuhTempo").val();
         var txIsiRekomendasi = $(".txIsiRekomendasi").val();
         var ddlPrioritas = $(".ddlPrioritas").val();

         var ddlSeverity = $(".ddlSeverity").val();
         var ddlProbability = $(".ddlProbability").val();
         var txPotensial = $(".txPotensial").val();
         var errMsg = "";
         var errCount = 0;

        
         if (ddlSeverity == "0") {
             errCount++;
             errMsg += "<li><b>Select Severity !</b></li>";
         }
         if (ddlProbability == "0") {
             errCount++;
             errMsg += "<li><b>Select Probability !</b></li>";
         }
         if (txIsiRekomendasi == "") {
             errCount++;
             errMsg += "<li><b>Input Recommendation !</b></li>";
         }
         if (txSubjek == "") {
             errCount++;
             errMsg += "<li><b>Input Subject !</b></li>";
         }
         if (txTglJatuhTempo == "") {
             errCount++;
             errMsg += "<li style='text-align:left'><b>Select Due Date !</b></li>";
         }


         if (errCount > 0) {

             swal({
                 type: 'error',
                 title: 'Fill All Required Fields !',
                 html: "<div id='boxValidasiDetail' class='alert alert-danger alert-dismissible' style='text-align:left !important;display: block;font-size:20px' role='alert'> <ul>" + errMsg + "</ul> </div>",
                 width: '800px',
             })
             //$('#boxValidasi').css('display', 'block');

             return false;
         } else {
             return true;
         }
    }

    function validateComment() {

        //make syntax ini kok error yaa jadi terpaksa diakalin
        //var txNama_add = document.getElementById('<%=txJudul.ClientID%>').value;
        var txKomentar = $(".txKomentar").val();
          
            var errMsg = "";
            var errCount = 0;

            if (txKomentar == "") {
                errCount++;
                errMsg += "<li><b>Input Comment !</b></li>";
            }
           
            if (errCount > 0) {

                swal({
                    type: 'error',
                    title: 'Fill All Required Fields !',
                    html: "<div id='boxValidasiHead' class='alert alert-danger alert-dismissible' style='text-align:left !important;display: block;font-size:20px' role='alert'> <ul>" + errMsg + "</ul> </div>",
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
                    <li class="active"><strong>Action Tracking</strong></li>
                    </ol>
        </div>
    </div>
</div>


        
<asp:UpdatePanel runat="server" ID="ListPanel" UpdateMode="Conditional">
    
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="dgObject" />
        <asp:AsyncPostBackTrigger ControlID="txSearch" />
        <asp:AsyncPostBackTrigger ControlID="btnSave" />
        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
    </Triggers>
    <ContentTemplate>
        <div class="row" id="TaskListPanel" runat="server" visible="true">
            <div class="col-md-12 animatedParent animateOnce z-index-50">
                <div class="panel panel-primary animated fadeInUp go">
                    <div class="panel-heading clearfix">
                        <div class="panel-title" style="padding-top: 5px"><b>TASK LIST</b></div>
                        <ul class="panel-tool-options">
                            <li>
                                <button type="button" class="btn btn-primary" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick"><i class="fa fa-plus fa-lg"></i></button>
                            </li>
                            <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                            <li><a data-rel="reload" href="#"><i class="icon-arrows-ccw"></i></a></li>

                        </ul>
                    </div>
                    <!-- panel body -->
                    <div class="panel-body">

                        <div class="row">

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

                            <div class="col-sm-8">
                                
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
                                PageSize="10" Width="100%" OnPageIndexChanged="dgObject_PageIndexChanged" OnItemCommand="dgObject_ItemCommand">
                                <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridCustomPages"></PagerStyle>
                                <HeaderStyle CssClass="GridCustomHeader"></HeaderStyle>
                                <ItemStyle Wrap="false"  CssClass="GridCustomItem" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <div style="margin-left: 2px">
                                                <asp:Label ID="lblHeaderColumn4_z" runat="server" Text='Action'></asp:Label>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div style="margin-left: 2px">
                                                <asp:LinkButton  ID="BtnEditData" CssClass="btn btn-warning btn-xs" runat="server" ToolTip="Edit" CommandName="Edit" CommandArgument='<%#Eval("NoNotulenDetail") %>'><i class="fa fa-pencil fa-lg"></i></asp:LinkButton>
                                                <asp:LinkButton  ID="BtnDelete" CssClass="btn btn-danger btn-xs" runat="server" ToolTip="Delete" OnClientClick="return confirmDelete(this, event);" CommandName="Delete" CommandArgument='<%#Eval("NoNotulenDetail") %>'><i class="fa fa-trash fa-lg"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                   <%-- <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">

                                        <ItemTemplate>
                                            <%# dgObject.PageSize * dgObject.CurrentPageIndex + Container.ItemIndex + 1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateColumn>--%>
                                     <asp:TemplateColumn SortExpression="Subjek"  HeaderStyle-Width="100px" HeaderText="Subject" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis; width:200px !important">
                                                <asp:Label ID="lblSubjek"  runat="server" Text='<%# Eval("Subjek") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NoNotulenDetail" HeaderStyle-Width="15%" HeaderText="Reg. Number" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
                                                <asp:LinkButton ID="linkNotulen" runat="server" CommandName="linkNotulen" CommandArgument='<%#Eval("NoNotulenDetail") %>' Text='<%#Eval("NoNotulenDetail")  %>'></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NoDokumen" HeaderStyle-Width="15%" HeaderText="Ref. Doc. Number" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">

                                                <asp:Label ID="lblDokumen" runat="server" Text='<%#Eval("NoDokumen")  %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="CreatedOn" HeaderText="Task Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("tglNotulen","{0:dd/MM/yyyy}") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn SortExpression="Title" HeaderStyle-Width="10%" HeaderText="Title" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("JudulNotulen") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                   
                                    <asp:TemplateColumn SortExpression="TglJatuhTempo" HeaderText="Due Date" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblTglJatuhTempo" runat="server" Text='<%# Eval("TglJatuhTempo","{0:dd/MM/yyyy}") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="NamaPIC" HeaderText="PIC" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblNamaPIC" runat="server" Text='<%# Eval("NamaPIC") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>

                                     <asp:TemplateColumn SortExpression="IdPotential" HeaderStyle-Width="5%" HeaderText="Risk Rating" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblPotential" runat="server" Text='<%# Eval("namaPotential") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>

                                    <asp:TemplateColumn SortExpression="NamaStatus" HeaderText="Status" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
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
                                            <div style="margin-left: 2px; overflow:hidden;text-overflow:ellipsis;">
                                                <asp:Label ID="lblTglClosed" runat="server" Text='<%# Eval("TglClosed","{0:dd/MM/yyyy}") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateColumn>--%>
                                    
                                </Columns>
                                <AlternatingItemStyle CssClass="GridCustomAltItem"></AlternatingItemStyle>
                            </asp:DataGrid>

                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="row" id="AddTaskPanel" runat="server" visible="false">
            <div class="col-md-12 animatedParent animateOnce z-index-50">
                <div class="panel panel-primary animated fadeInUp go">
                    <div class="panel-heading clearfix">
                        <div class="panel-title" style="padding-top: 5px;">
                            <b>
                                <asp:Label runat="server" ID="lblJudul"></asp:Label>
                                 TASK</b>
                        </div>
                        <ul class="panel-tool-options">
                            <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>

                        </ul>
                    </div>
                    <!-- panel body -->

                    <div class="panel-body">
                        <form class="form-horizontal">

                            <div class="form-group">
                                <label for="ddlRekomendasi">Recommendation</label>
                                <asp:DropDownList runat="server" ID="ddlRekomendasi" OnTextChanged="ddlRekomendasi_TextChanged" AutoPostBack="true" CssClass="form-control dropdownlist ddlRekomendasi">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="ddlSumber">Source</label>
                                <asp:DropDownList runat="server" ID="ddlSumber" CssClass="form-control dropdownlist ddlSumber" OnTextChanged="ddlSumber_TextChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txJudul">Title</label>
                                <input class="form-control txJudul" id="txJudul" runat="server" placeholder="Input Title" type="text">
                                <input class="form-control txId" id="txId" runat="server" style="display: none" type="text">
                            </div>

                            <div class="form-group">
                                <label for="txTaskDate">Task Date</label>
                                <input class="form-control txTaskDate clsDate" data-date-format="dd/mm/yyyy" id="txTaskDate" runat="server" placeholder="Select Task Date" type="text">
                            </div>
                            <div class="form-group">
                                <label for="txNoDokumen">Ref. Doc. Number</label>
                                <input class="form-control txNoDokumen" id="txNoDokumen" runat="server" placeholder="Input Ref. Doc. Number" type="text">
                            </div>
                            <div class="form-group">
                                <label for="txNoDokumen">Attachment</label>
                               <ajax:AsyncFileUpload ID="fileUpload1" CssClass="fileUpload1" OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError" 
                                CompleteBackColor="White" Width="350px" runat="server" UploaderStyle="Modern" UploadingBackColor="#CCFFFF" 
                                ThrobberID="imgLoad" OnUploadedComplete="fileUploadComplete" /><br />
                                <asp:Image ID="imgLoad" runat="server" ImageUrl="~/Images/loading.gif" />
                                <br />
                                <a style="color:navy" href="../Doc/<% =this.namaFile %>" download="<% =this.namaFile %>" target="_blank">
                                                             <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></a>
                                <br />
                             </div>
                            <div class="form-group">
                                <label for="ddlReviever">Reviewer</label>
                                <asp:DropDownList runat="server" ID="ddlReviever" CssClass="form-control dropdownlist ddlReviewer">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-12 " style="border: 3px dashed #353c42; padding-top: 10px">
                                <div class="row">
                                    <div class="col-md-6 animatedParent animateOnce ">
                                        <div class="form-group">
                                            <label for="txSubjek">Subject</label>
                                            <input class="form-control txSubjek" id="txSubjek" runat="server" placeholder="Input Subject" type="text">
                                        </div>
                                    </div>
                                    <div class="col-md-6 animatedParent animateOnce ">
                                        
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 animatedParent animateOnce ">
                                        <div class="form-group">
                                             <label for="ddlFungsi">Fungsi </label><br />
                                            <asp:DropDownList runat="server" ID="ddlFungsi" CssClass="form-control dropdownlist ddlFungsi" style="width:85% !important;display:inline !important"> 
                                            </asp:DropDownList>
                                             <button type="button" class="btn btn-primary btn-md" id="btnAddFungsi" runat="server" onserverclick="btnAddFungsi_ServerClick" style="float:right"><i class="fa fa-plus fa-lg"></i></button>
                                                <br />
                                            <br />
                                              <asp:DataGrid ID="dgFungsi" CssClass="GridBox" runat="server"
                                                DataKeyField="idDetail" AutoGenerateColumns="False"
                                                Font-Names="Calibri"
                                                PageSize="2" Width="100%" OnItemCommand="dgFungsi_ItemCommand">
                                                 <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                                <ItemStyle CssClass="GridBoxItem" />
                                                <Columns>

                                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <%# dgFungsi.PageSize * dgFungsi.CurrentPageIndex + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="idDetail" Visible="false" HeaderText="idDetail" />
                                                    <asp:BoundColumn DataField="Fungsi" Visible="True" HeaderText="Fungsi" />
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <div style="margin-left: 6px">
                                                                <asp:Label ID="lblHeaderColumn4_aksi" runat="server" Text='Act.'></asp:Label>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div style="margin-left: 6px">
                                                                <asp:LinkButton ID="dgFungsi_btnDelete" class="btn btn-danger btn-xs" runat="server" ToolTip="Delete Fungsi" OnClientClick="return confirmDelete(this, event);" CommandName="Delete" CommandArgument='<%#Eval("IdDetail") %>'><span><i class="fa fa-trash fa-lg"></i></span></asp:LinkButton>
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
                                    <div class="col-md-6 animatedParent animateOnce">
                                        <div class="form-group">
                                            <label for="ddlPIC">PIC</label><br />
                                            <asp:Label runat="server" ID="lblEmailPIC" Visible="false"></asp:Label>
                                            <button type="button" class="form-control btn btn-primary" id="addPIC" runat="server" onserverclick="addPIC_ServerClick"><i class="fa fa-user fa-lg" style="float: left !important"></i>Select PIC </button>
                                            <br />
                                            <br />
                                            <asp:DataGrid ID="dgPIC" CssClass="GridBox" runat="server"
                                                DataKeyField="idDetail" AutoGenerateColumns="False"
                                                Font-Names="Calibri"
                                                PageSize="2" Width="100%" OnItemCommand="dgPIC_ItemCommand">
                                                <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                                <ItemStyle CssClass="GridBoxItem" />
                                                <Columns>

                                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <%# dgPIC.PageSize * dgPIC.CurrentPageIndex + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="idDetail" Visible="false" HeaderText="idDetail" />
                                                    <asp:BoundColumn DataField="Nopek" Visible="True" HeaderText="Emp. No" />
                                                    <asp:BoundColumn DataField="Nama" Visible="True" HeaderText="Name" />
                                                    <asp:BoundColumn DataField="Jabatan" Visible="True" HeaderText="Position" />
                                                    <asp:BoundColumn DataField="IdPosition" Visible="false" HeaderText="IdPosition" />
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <div style="margin-left: 6px">
                                                                <asp:Label ID="lblHeaderColumn4_aksi" runat="server" Text='Act.'></asp:Label>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div style="margin-left: 6px">
                                                                <asp:LinkButton ID="dgPIC_btnDelete" class="btn btn-danger btn-xs" runat="server" ToolTip="Delete PIC" OnClientClick="return confirmDelete(this, event);" CommandName="Delete" CommandArgument='<%#Eval("IdDetail") %>'><span><i class="fa fa-trash fa-lg"></i></span></asp:LinkButton>
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
                                <div id="RAM" runat="server">
                                    
                                <div class="row">
                                
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table border="1" title="A risk matrix is a matrix that is used during risk assessment to define the level of risk by considering the category of probability or likelihood against the category of consequence severity. This is a simple mechanism to increase visibility of risks and assist management decision making">
                                                <tbody>
                                                    <tr style="height: 21px;">
                                                        <th style="width: 25%; height: 63px; text-align: center; vertical-align: middle;" colspan="3" rowspan="3">Tingkat Keparahan (Severity)</th>
                                                        <th style="height: 40px; text-align: center;" colspan="7">Konsekuensi Terhadap Objek</th>
                                                        <th style="height: 40px; text-align: center;" colspan="5">Kemungkinan Kejadian (Probability)</th>
                                                    </tr>
                                                    <tr>
                                                        <th class="tableHead" colspan="1" rowspan="1">Safety</th>
                                                        <th class="tableHead" colspan="1" rowspan="1">Health</th>
                                                        <th class="tableHead" colspan="1" rowspan="1">Produksi</th>
                                                        <th class="tableHead" colspan="1" rowspan="1">Lingkungan</th>
                                                        <th class="tableHead" colspan="1" rowspan="1">Reputasi/Media</th>
                                                        <th class="tableHead" colspan="1" rowspan="1">Legal</th>
                                                        <th class="tableHead" colspan="1" rowspan="1">Finansial</th>
                                                        <th class="tableHead">Unlikely</th>
                                                        <th class="tableHead">Rare</th>
                                                        <th class="tableHead">Moderate</th>
                                                        <th class="tableHead">Likely</th>
                                                        <th class="tableHead">Almost Certain</th>
                                                    </tr>
                                                    <tr>
                                                        <th class="tableHead"></th>
                                                        <th class="tableHead"></th>
                                                        <th class="tableHead"></th>
                                                        <th class="tableHead"></th>
                                                        <th class="tableHead"></th>
                                                        <th class="tableHead"></th>
                                                        <th class="tableHead"></th>
                                                        <th class="tableHead">1</th>
                                                        <th class="tableHead">2</th>
                                                        <th class="tableHead">3</th>
                                                        <th class="tableHead">4</th>
                                                        <th class="tableHead">5</th>
                                                    </tr>
                                                    <tr style="height: 21px;">
                                                        <th style="transform: rotate(-90deg); width: 5%; height: 0px; text-align: center; vertical-align: middle;" rowspan="6" scope="col">Probability</th>
                                                        <th  class="tableHead" style="background-color:#ff1313">5</th>
                                                        <th  class="tableHead" style="background-color:#ff1313">Catastropic</th>
                                                        <th  class="tableHead">Bencana</th>
                                                        <th  class="tableHead">Bencana</th>
                                                        <th  class="tableHead">Gangguan Masif</th>
                                                        <th  class="tableHead">Pengaruh Masif</th>
                                                        <th  class="tableHead">Dampak Masif (Dampak Besar)</th>
                                                        <th  class="tableHead">Catastropic</th>
                                                        <th  class="tableHead">Kerusakan Masif</th>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(5,1)" id="p51" style="width:100%;height:100%;background-color:#fdff24"> 5 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(5,2)" id="p52" style="width:100%;height:100%;background-color:#ff7c1a"> 10 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(5,3)" id="p53" style="width:100%;height:100%;background-color:#ff1313"> 15 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(5,4)" id="p54" style="width:100%;height:100%;background-color:#ff1313"> 20 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(5,5)" id="p55" style="width:100%;height:100%;background-color:#ff1313"> 25 </div></td>
                                                    </tr>
                                                    <tr style="height: 21px;">
                                                        <th  class="tableHead" style="background-color:#ff1313">4</th>
                                                        <th  class="tableHead" style="background-color:#ff1313">Significant</th>
                                                        <th  class="tableHead">Kejadian Fatal</th>
                                                        <th  class="tableHead">Kejadian Fatal</th>
                                                        <th  class="tableHead">Gangguan Major</th>
                                                        <th  class="tableHead">Pengaruh Major</th>
                                                        <th  class="tableHead">Dampak Major (Skala Nasional)</th>
                                                        <th  class="tableHead">Significant</th>
                                                        <th  class="tableHead">Kerusakan Major</th>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(4,1)" id="p41" style="width:100%;height:100%;background-color:#fdff24"> 4 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(4,2)" id="p42" style="width:100%;height:100%;background-color:#ff7c1a"> 8 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(4,3)" id="p43" style="width:100%;height:100%;background-color:#ff7c1a"> 12 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(4,4)" id="p44" style="width:100%;height:100%;background-color:#ff1313"> 16 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(4,5)" id="p45" style="width:100%;height:100%;background-color:#ff1313"> 20 </div></td>
                                                    </tr>
                                                    <tr style="height: 21px;">
                                                        <th  class="tableHead" style="background-color:#ff1313">3</th>
                                                        <th  class="tableHead" style="background-color:#ff1313">Moderate</th>
                                                        <th  class="tableHead">Cidera Berat</th>
                                                        <th  class="tableHead">Pengaruh Berat</th>
                                                        <th  class="tableHead">Gangguan Moderat</th>
                                                        <th  class="tableHead">Pengaruh Moderat</th>
                                                        <th  class="tableHead">Dampak Moderat (Skala Daerah)</th>
                                                        <th  class="tableHead">Moderate</th>
                                                        <th  class="tableHead">Kerusakan Moderat</th>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(3,1)" id="p31" style="width:100%;height:100%;background-color:#fdff24"> 3 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(3,2)" id="p32" style="width:100%;height:100%;background-color:#fdff24"> 6 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(3,3)" id="p33" style="width:100%;height:100%;background-color:#ff7c1a"> 9 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(3,4)" id="p34" style="width:100%;height:100%;background-color:#ff7c1a"> 12 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(3,5)" id="p35" style="width:100%;height:100%;background-color:#ff1313"> 15 </div></td>
                                                    </tr>
                                                    <tr style="height: 21px;">
                                                        <th  class="tableHead" style="background-color:#ff1313">2</th>
                                                        <th  class="tableHead" style="background-color:#ff1313">Mirror</th>
                                                        <th  class="tableHead">Cidera Sedang</th>
                                                        <th  class="tableHead">Pengaruh Sedang</th>
                                                        <th  class="tableHead">Gangguan Minor</th>
                                                        <th  class="tableHead">Pengaruh Minor</th>
                                                        <th  class="tableHead">Dampak Minor (Terbatas)</th>
                                                        <th  class="tableHead">Mirror</th>
                                                        <th  class="tableHead">Kerusakan Minor</th>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(2,1)" id="p21" style="width:100%;height:100%;background-color:#1de039"> 2 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(2,2)" id="p22" style="width:100%;height:100%;background-color:#fdff24"> 4 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(2,3)" id="p23" style="width:100%;height:100%;background-color:#fdff24"> 6 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(2,4)" id="p24" style="width:100%;height:100%;background-color:#ff7c1a"> 8 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(2,5)" id="p25" style="width:100%;height:100%;background-color:#ff7c1a"> 10 </div></td>
                                                    </tr>
                                                    <tr style="height: 21px;">
                                                        <th  class="tableHead" style="background-color:#ff1313">1</th>
                                                        <th  class="tableHead" style="background-color:#ff1313">Insignificant</th>
                                                        <th  class="tableHead">Cidera Ringan</th>
                                                        <th  class="tableHead">Pengaruh Kecil</th>
                                                        <th  class="tableHead">Gangguan Kecil</th>
                                                        <th  class="tableHead">Pengaruh Kecil</th>
                                                        <th  class="tableHead">Dampak Kecil</th>
                                                        <th  class="tableHead">Insignificant</th>
                                                        <th  class="tableHead">Kerusakan Kecil</th>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(1,1)" id="p11" style="width:100%;height:100%;background-color:#1de039"> 1 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(1,2)" id="p12" style="width:100%;height:100%;background-color:#1de039"> 2 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(1,3)" id="p13" style="width:100%;height:100%;background-color:#fdff24"> 3 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(1,4)" id="p14" style="width:100%;height:100%;background-color:#fdff24"> 4 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(1,5)" id="p15" style="width:100%;height:100%;background-color:#fdff24"> 5 </div></td>
                                                    </tr>
                                                    <tr style="height: 21px;">
                                                        <th  class="tableHead" style="background-color:#ff1313">0</th>
                                                        <th  class="tableHead" style="background-color:#ff1313">Tidak Berdampak</th>
                                                        <th  class="tableHead">Tidak Berdampak</th>
                                                        <th  class="tableHead">Tidak Berdampak</th>
                                                        <th  class="tableHead">Tidak Terjadi Keusakan/Gangguan</th>
                                                        <th  class="tableHead">Tidak Berpengaruh</th>
                                                        <th  class="tableHead">Tidak Berdampak</th>
                                                        <th  class="tableHead">Tidak Berpengaruh</th>
                                                        <th  class="tableHead">Tidak Berpengaruh</th>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(0,1)" id="Div1" style="width:100%;height:100%;background-color:#1de039"> 0 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(0,2)" id="Div2" style="width:100%;height:100%;background-color:#1de039"> 0 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(0,3)" id="Div3" style="width:100%;height:100%;background-color:#1de039"> 0 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(0,4)" id="Div4" style="width:100%;height:100%;background-color:#1de039"> 0 </div></td>
                                                        <td class="poin"><div class="divRAM" onclick="pilihRAM(0,5)" id="Div5" style="width:100%;height:100%;background-color:#1de039"> 0 </div></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                   
                                </div>
                                <div class="row">
                                    <br />
                                   <div class="col-md-6">
                                       <div class="col-md-3">
                                            <div style="background-color:#1de039;width:100% !important;text-align:center;">Low (1-2)</div> 
                                      
                                       </div>
                                       <div class="col-md-3">
                                            <div style="background-color:#fdff24;width:100% !important;text-align:center;">Medium (3-6)</div>
                                       
                                       </div>
                                       <div class="col-md-3">
                                           <div style="background-color:#ff7c1a;width:100% !important;text-align:center;">High (8-12)</div> 
                                      
                                       </div>
                                       <div class="col-md-3">
                                             <div style="background-color:#ff1313;width:100% !important;text-align:center;">Extreme (13-25)</div>
                                   
                                       </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group" style="vertical-align:middle !important;">
                                            <asp:DropDownList runat="server" Visible="false" ID="ddlSeverity" onchange="ddlRAMChanged()"   CssClass="form-control dropdownlist ddlSeverity">
                                                <asp:ListItem Text="-- Select Severity --" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:DropDownList runat="server"  Visible="false" onchange="ddlRAMChanged()" ID="ddlProbability"  CssClass="form-control dropdownlist ddlProbability">
                                                <asp:ListItem Text="-- Select Probability --" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                            <div class="col-md-1">
                                                </div>
                                            <div class="col-md-3">
                                                
                                            <label for="txPotensial">Risk Rating</label>
                                                </div>
                                            <div class="col-md-4"> 
                                                <input class="form-control txPotensial" id="txPotensial" runat="server" type="text" disabled="disabled" style="display:inline-block !Important; float:right">
                                           </div>
                                            <div class="col-md-4">
                                                 <input class="form-control txnamaRisk" id="txnamaRisk" runat="server" type="text" disabled="disabled" style="display:inline-block !Important; float:right">
</div>
                                            <input class="form-control txSeverity" id="txSeverity" runat="server" type="text" disabled="disabled" style="width:80% !important;display:none; float:right">
<input class="form-control txProbability" id="txProbability" runat="server" type="text" disabled="disabled" style="width:80% !important;display:none; float:right">

                                        </div>
                                    </div>
                                </div>
                                </div>
                                <div class="form-group">
                                           <label for="txTglJatuhTempo">Due Date</label>
                                            <input class="form-control txTglJatuhTempo clsDate" data-date-format="dd/mm/yyyy" id="txTglJatuhTempo" runat="server" placeholder="Select Due Date" type="text">
                                        
                                        </div>
                                <div class="form-group">
                                    <label>Input Recommendations</label>
                                    <textarea placeholder="Input Recommendations" runat="server" id="txIsiRekomendasi" class="form-control txIsiRekomendasi" style="width: 100%; height: 91px;"></textarea>
                                </div>
                                <div class="row">
                                    <div class="col-md-9 animatedParent animateOnce ">
                                        <div class="form-group">
                                           
                                        </div>
                                    </div>
                                    <div class="col-md-3 animatedParent animateOnce ">
                                        <div class="form-group" style="padding-top: 13%">

                                            <button type="button" class="btn btn-primary form-control" id="btnAddRekomendasi" runat="server" onclick="validateDetail();" onserverclick="btnAddRekomendasi_ServerClick">Add Recommendation</button>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive" style="margin-bottom: 17px;">
                                            <asp:DataGrid ID="dgAddRekomendasi" CssClass="GridBox" runat="server"
                                                DataKeyField="NoNotulenDetail" AutoGenerateColumns="False"
                                                Font-Names="Calibri" AllowPaging="True" OnPageIndexChanged="dgAddRekomendasi_PageIndexChanged"
                                                PageSize="2" Width="150%" OnItemCommand="dgAddRekomendasi_ItemCommand">
                                                <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridBoxPages"></PagerStyle>
                                                <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                                <ItemStyle CssClass="GridBoxItem" />
                                                <Columns>

                                                    <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <%# dgAddRekomendasi.PageSize * dgAddRekomendasi.CurrentPageIndex + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="NoNotulenDetail" Visible="false" HeaderText="ID Kompetensi" HeaderStyle-HorizontalAlign="Left" />
                                                    <asp:BoundColumn DataField="Subjek" Visible="True" HeaderText="Subject" HeaderStyle-HorizontalAlign="Left"/>
                                                    <asp:BoundColumn DataField="Isi" Visible="True" HeaderText="Recommendations" HeaderStyle-HorizontalAlign="Left"/>
                                                    <asp:BoundColumn DataField="namaPIC" Visible="True" HeaderText="PIC" HeaderStyle-HorizontalAlign="Left"/>
                                                    <asp:BoundColumn DataField="namaFungsi" Visible="True" HeaderText="Fungsi" HeaderStyle-HorizontalAlign="Left"/>
                                                    <asp:BoundColumn DataField="TglJatuhTempo" Visible="True" HeaderText="Due Date" HeaderStyle-HorizontalAlign="Left"/>
                                                    <asp:BoundColumn DataField="idPotential" Visible="True" HeaderText="Risk Rating" HeaderStyle-HorizontalAlign="Left"/>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <div style="margin-left: 6px">
                                                                <asp:Label ID="lblHeaderColumn4_aksi" runat="server" Text='Act.'></asp:Label>
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div style="margin-left: 6px">
                                                                <asp:LinkButton ID="dgAddKompetensi_btnDelete" class="btn btn-danger btn-xs" runat="server" ToolTip="Hapus Rekomendasi" OnClientClick="return confirmDelete(this, event);" CommandName="Delete" CommandArgument='<%#Eval("NoNotulenDetail") %>'><span><i class="fa fa-trash fa-lg"></i></span></asp:LinkButton>
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
                            <%--NotulenDetail--%>
                            <div class="form-group">
                                <label style="color: transparent">.</label>
                            </div>
                        </form>
                    </div>
                    <div class="panel-footer" style="text-align:right;">
                        <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" id="btnClose" onserverclick="btnClose_ServerClick">Close</button>
                        <button type="button" class="btn btn-primary" id="btnSave" validationgroup="btnSave" runat="server" onclick="validateHead();" onserverclick="btnSave_ServerClick">Submit</button>
                        <button type="button" class="btn btn-warning" id="btnDraft" validationgroup="btnDraft" runat="server" onclick="validateHead();" onserverclick="btnDraft_ServerClick">Save to Draft</button>
                        <button type="button" class="btn btn-primary" id="btnUpdate" runat="server" onclick="validateHead();" onserverclick="btnUpdate_ServerClick">Update</button>

                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
 <asp:UpdateProgress runat="server" ID="upProgress" DynamicLayout="true" AssociatedUpdatePanelID="ListPanel">
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
<div id="modalView" class="modal fade" tabindex="-2" role="dialog" style="display: none;">
  <div class="modal-dialog modal-lg">
      <div class="modal-content" style="color: #000">
          <asp:UpdatePanel runat="server" ID="viewPanel" UpdateMode="Conditional">
              <ContentTemplate>
          <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
              <h4 class="modal-title" style="color: #000">APPROVAL FORM</h4>
          </div>
                    <div class="modal-body">
                      <div class="row">
                           <div class="col-md-12">
                               <div class="table-responsive">
                                   <table class="table table-hovered">
                                        <tr style="border-bottom:1px solid #353c42 !important;border-top:0 !important">
                                           <td  >
                                               Task Number
                                           </td>
                                           <td  >
                                               <asp:Label runat="server" ID="view_lblNoNotulen"></asp:Label>
                                               <asp:Label runat="server" ID="view_lblNoNotulenDetail" Visible="false" ></asp:Label>
                                               <asp:Label runat="server" ID="view_lblDueDate" Visible="false" ></asp:Label>
                                           </td>
                                       </tr>
                                       <tr style="border-bottom:1px solid #353c42 !important">
                                           <td  >
                                               Recommendations
                                           </td>
                                           <td  >
                                               <asp:Label runat="server" ID="view_lblRekomendasi"></asp:Label>
                                           </td>
                                       </tr>
                                       <tr style="border-bottom:1px solid #353c42 !important">
                                           <td>
                                               Source
                                           </td>
                                           <td>
                                               <asp:Label runat="server" ID="view_lblSumber"></asp:Label>
                                           </td>
                                       </tr>
                                       <tr style="border-bottom:1px solid #353c42 !important">
                                           <td>
                                               Title
                                           </td>
                                           <td>
                                               <asp:Label runat="server" ID="view_lblJudulNotulen"></asp:Label>
                                           </td>
                                       </tr>
                                         <tr style="border-bottom:1px solid #353c42 !important">
                                           <td>
                                               Ref. Doc. Number
                                           </td>
                                           <td>
                                               <asp:Label runat="server" ID="view_lblNoDokumen"></asp:Label>
                                           </td>
                                       </tr>
                                         <tr style="border-bottom:1px solid #353c42 !important">
                                           <td>
                                              Task Date
                                           </td>
                                           <td>
                                               <asp:Label runat="server" ID="view_lbltglNotulen"></asp:Label>
                                           </td>
                                       </tr>
                                         <tr style="border-bottom:1px solid #353c42 !important">
                                           <td>
                                              Attachments
                                           </td>
                                           <td>
                                               <a style="color:navy" href="../Doc/<% =this.namaFile %>" download="<% =this.namaFile %>" target="_blank">
                                                            <asp:Label runat="server" ID="view_lblLampiran"></asp:Label></a>
                                           </td>
                                       </tr>
                                         <tr style="border-bottom:1px solid #353c42 !important">
                                           <td>
                                              Reviewer
                                           </td>
                                           <td>
                                               <asp:Label runat="server" ID="view_lblReviewer"></asp:Label>
                                           </td>
                                       </tr>
                                   </table>
                                   <br>
                                   </br>
                                    <asp:DataGrid ID="dgViewDetail" CssClass="GridBox" runat="server"
                                               DataKeyField="noNotulenDetail" AutoGenerateColumns="False"
                                               Font-Names="Calibri" Width="100%"  >
                                               <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridBoxPages"></PagerStyle>
                                               <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                               <ItemStyle CssClass="GridBoxItem" />
                                               <Columns>

                                                   <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                       <ItemTemplate>
                                                           <%# dgViewDetail.PageSize * dgViewDetail.CurrentPageIndex + Container.ItemIndex + 1%>
                                                       </ItemTemplate>
                                                       <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                   </asp:TemplateColumn>
                                                   <asp:BoundColumn DataField="noNotulenDetail" ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="noNotulenDetail" />
                                                   <asp:BoundColumn DataField="Subjek" ItemStyle-HorizontalAlign="Center" Visible="True" HeaderText="Subject"  />
                                                   <asp:BoundColumn DataField="Isi" ItemStyle-HorizontalAlign="Center" Visible="True" HeaderText="Recommendations" />
                                                   <asp:BoundColumn DataField="namaPIC" ItemStyle-HorizontalAlign="Center" Visible="True" HeaderText="PIC" />
                                                   <asp:BoundColumn DataField="namaFungsi" ItemStyle-HorizontalAlign="Center" Visible="True" HeaderText="Fungsi"  />
                                                   <asp:BoundColumn DataField="TglJatuhTempo" ItemStyle-HorizontalAlign="Center" Visible="True" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Due Date"  />
                                                   <asp:BoundColumn DataField="idPotential" ItemStyle-HorizontalAlign="Center" Visible="True" HeaderText="Risk Rating"  />
                                                 
                                               </Columns>
                                               <AlternatingItemStyle CssClass="GridBoxAltItem"></AlternatingItemStyle>
                                           </asp:DataGrid>
                               </div>

                           </div>

                      </div>
                        <br />
                        <div class="row" id="EvidencePanel" runat="server" style="border:3px dashed #353c42;padding-top:10px;margin:0.1%;">
                            <div class="col-md-12">
                                <h3>Evidence</h3>
                                 <div class="form-group">
                                   <label>Attachment: </label>  
                                    <a style="color:navy" href="../Doc/<% =this.fileEvidence %>" download="<% =this.fileEvidence %>" target="_blank">
                                    <asp:Label runat="server" ID="linkEvidence"></asp:Label></a>
                                             
                                 </div>
                                 <div class="form-group">
                                    <label>Evidence Note</label> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txKeteranganEvidence" ErrorMessage="*" ForeColor="Red" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                        
                                     <asp:TextBox runat="server" Enabled="false" ID="txKeteranganEvidence" TextMode="MultiLine" Width="100%" Height="91px" ForeColor="Black" CssClass="txKeteranganEvidence"></asp:TextBox> 
                                    <asp:Label runat="server" ID="lblIdEvidence" Visible="false" Text=""></asp:Label>
                              </div>
                                 <div class="form-group">
                                <label for="txKomentar">Comments</label>
                                      <input class="form-control txKomentar" id="txKomentar" runat="server" placeholder="Input Comments" type="text" >
                                  </div>
                                <br />
                                 
                                 <h3>Rejected History</h3>
                                 <asp:DataGrid ID="dgComment" CssClass="GridBox" runat="server"
                                     DataKeyField="idkomentar" AutoGenerateColumns="False"
                                     Font-Names="Calibri" Width="100%"  >
                                     <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridBoxPages"></PagerStyle>
                                     <HeaderStyle CssClass="GridBoxHeader"></HeaderStyle>
                                     <ItemStyle CssClass="GridBoxItem" />
                                     <Columns>
                                         <asp:TemplateColumn HeaderText="No." HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                             <ItemTemplate>
                                                 <%# dgComment.PageSize * dgComment.CurrentPageIndex + Container.ItemIndex + 1%>
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                         </asp:TemplateColumn>
                                         <asp:BoundColumn DataField="idkomentar" Visible="false" HeaderText="idkomentar" />
                                         <asp:BoundColumn DataField="Komentar" ItemStyle-HorizontalAlign="Center" Visible="True" HeaderText="Comments"  />
                                         <asp:BoundColumn DataField="CreatedOn" ItemStyle-HorizontalAlign="Center" Visible="True" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                         <asp:BoundColumn DataField="CreatedBy" ItemStyle-HorizontalAlign="Center" Visible="false" HeaderText="Originator" />
                                         <asp:TemplateColumn  HeaderText="Evidence" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                              <ItemTemplate>
                                                  <div style="margin-left: 2px">
                                                      <asp:LinkButton ID="linkEvidence" runat="server" CommandName="linkEvidence" CommandArgument='<%#Eval("fname") %>' Text='<%#Eval("fname")  %>'></asp:LinkButton>
                                                  </div>
                                              </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                              <HeaderStyle HorizontalAlign="Center" />
                                          </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Status" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <div style="margin-left: 2px">
                                                                <span class='label label-default'>Reject</span>
                                                               </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                     </Columns>
                                     <AlternatingItemStyle CssClass="GridBoxAltItem"></AlternatingItemStyle>
                                  </asp:DataGrid>
                                 <br />

                            </div>
                         </div>
                        <br />
                  </div>

                  <div class="modal-footer">

                      <button type="button" class="btn btn-default" data-dismiss="modal" runat="server" id="Button1" onserverclick="btnClose_ServerClick">Close</button>
                      <button type="button" class="btn btn-success" data-dismiss="modal" runat="server" id="btnApprove" onserverclick="btnApprove_ServerClick">Approve</button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal" runat="server" id="btnReject"  onclick="validateComment();" onserverclick="btnReject_ServerClick">Reject</button>
                       
                  </div>
              </ContentTemplate>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btnReject" />
                  <asp:AsyncPostBackTrigger ControlID="btnApprove" />
              </Triggers>
          </asp:UpdatePanel>
                
              
      </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div>

<div id="modalPIC" class="modal fade" tabindex="-2" role="dialog" style="display: none;">
  <div class="modal-dialog modal-lg">
      <div class="modal-content" style="color: #000">
          <asp:UpdatePanel runat="server" ID="PekerjaPanel" UpdateMode="Conditional">
              <ContentTemplate>
          <div class="modal-header">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
              <h4 class="modal-title" style="color: #000">SELECT PIC</h4>
          </div>
                    <div class="modal-body">
                      <div class="row">
                           <div class="col-md-12">
                                <div class="row" style="margin-bottom: 10px;">
                                    <div class="col-sm-7">
                                      
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
                                                       <asp:Label ID="lblHeaderColumn3Position" runat="server" Text='Position'></asp:Label>
                                                   </div>
                                               </HeaderTemplate>
                                               <ItemTemplate>
                                                   <div style="margin-left: 6px">
                                                       <asp:Label ID="lblContentHeaderColumn3Position" runat="server" Text='<%# Eval("Jabatan") %>' />
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
                                                       <asp:LinkButton ID="popPekerja_btnChoose" class="buttonGrid small-button transparent" runat="server" ToolTip="Select" CommandName="Select" 
                                                        CommandArgument='<%#Eval("NoPek") + ";" +Eval("namaPegawai") + ";" +Eval("Jabatan") + ";" +Eval("email") %>'><span><i class="fa fa-check-square fa-lg"></i></span></asp:LinkButton>
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
                  
              </Triggers>
          </asp:UpdatePanel>
                
              
      </div><!-- /.modal-content -->
  </div><!-- /.modal-dialog -->
</div>


