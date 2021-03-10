<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoadDashboard.ascx.cs" Inherits="I_ACT.Pages.LoadDashboard" %>
<%--<%@ Register Src="~/Pages/Dashboard.ascx" TagName="dash" TagPrefix="ds" %>--%>


<div class="row">
    <asp:Label ID="idRekomendasi" runat="server" Visible="false" />
    <div class="col-md-12 animatedParent animateOnce z-index-50">
        <div class="panel panel-primary animated fadeInUp go">
            <div class="panel-heading clearfix">
                <div class="panel-title" style="padding-top: 5px"><b>Overall Task Status</b></div>
                    <ul class="panel-tool-options">
                        <li><a data-rel="collapse" href="#"><i class="icon-down-open"></i></a></li>
                    </ul>
            </div>
            <!-- panel body -->
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="panel1">
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="dgObject" />--%>
                        <asp:AsyncPostBackTrigger ControlID="dgSumber" />
                    </Triggers>
                    <ContentTemplate>
                    <div class="table-responsive">
                        <asp:DataGrid ID="dgSumber" CssClass="GridCustom" runat="server"
                            DataKeyField="idSumber" AutoGenerateColumns="False"
                            Font-Names="Calibri" OnItemDataBound="dgSumber_ItemDataBound"
                            AllowPaging="True"
                            PageSize="10" Width="100%" OnPageIndexChanged="dgSumber_PageIndexChanged" OnItemCommand="dgSumber_ItemCommand">
                            <PagerStyle Mode="NumericPages" Position="Bottom" HorizontalAlign="Left" CssClass="GridCustomPages"></PagerStyle>
                            <HeaderStyle CssClass="GridCustomHeader"></HeaderStyle>
                            <ItemStyle CssClass="GridCustomItem" />
                            <Columns>
                            <asp:TemplateColumn HeaderText="No" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <%# dgSumber.PageSize * dgSumber.CurrentPageIndex + Container.ItemIndex + 1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderStyle-Width="30%" HeaderText="Source Name" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <div style="margin-left: 2px">
                                        <%--<asp:LinkButton ID="linkSumber" runat="server" CommandName="linkSumber" CommandArgument='<%#Eval("idSumber") %>' Text='<%#Eval("namaSumber")  %>' ><a href="<%#Eval("url") %>"></asp:LinkButton>--%>
                                        <asp:LinkButton ID="linkSumber"  runat="server" CommandName="linkSumber" CommandArgument='<%# Eval("idSumber") %>' Text='<%#Eval("namaSumber")  %>'></asp:LinkButton>
                                        <%--<asp:Label ID="lblNamaSumber" runat="server"  Text='<%# Eval("namaSumber") %>' />--%>
                                        <asp:Label ID="lblidSumber" runat="server" Text='<%# Eval("idSumber") %>' Visible="false" />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Total Rekomendasi" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <div style="margin-left: 2px">
                                         <asp:LinkButton ID="TotalRekomendasi"  runat="server" CommandName="TotalRekomendasi" CommandArgument='<%# Eval("idSumber") %>' Text=""></asp:LinkButton>
                                        <%--<asp:Label ID="lblTotalRekomendasi" runat="server" Text="" />--%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Open" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <div style="margin-left: 2px">
                                        <asp:LinkButton ID="Open"  runat="server" CommandName="Open" CommandArgument='<%# Eval("idSumber") %>' Text=""></asp:LinkButton>
                                        <%--<asp:Label ID="lblOpen" runat="server" Text="" />--%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Waiting Approval" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                <ItemTemplate>
                                    <div style="margin-left: 2px">
                                        <asp:LinkButton ID="WaitingApproval"  runat="server" CommandName="WaitingApproval" CommandArgument='<%# Eval("idSumber") %>' Text=""></asp:LinkButton>
                                        <%--<asp:Label ID="lblWaitingApproval" runat="server" Text="" />--%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>



                                                                        <asp:TemplateColumn HeaderText="Closed" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                                            <ItemTemplate>
                                                                                <div style="margin-left: 2px">
                                                                                    <asp:LinkButton ID="Closed"  runat="server" CommandName="Closed"  CommandArgument='<%# Eval("idSumber") %>' Text=""></asp:LinkButton>
                                                                                    <%--<asp:Label ID="lblClosed" runat="server" Text="" />--%>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Overdue" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                                            <ItemTemplate>
                                                                                <div style="margin-left: 2px">
                                                                                    <asp:LinkButton ID="Overdue"  runat="server" CommandName="Overdue" CommandArgument='<%# Eval("idSumber") %>' Text=""></asp:LinkButton>
                                                                                    <%--<asp:Label ID="lblOverdue" runat="server" Text="" />--%>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Closed Overdue" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                                            <ItemTemplate>
                                                                                <div style="margin-left: 2px">
                                                                                    <asp:LinkButton ID="ClosedOverdue"  runat="server" CommandName="ClosedOverdue" CommandArgument='<%# Eval("idSumber") %>' Text=""></asp:LinkButton>
                                                                                    <%--<asp:Label ID="lblClosedOverdue" runat="server" Text="" />--%>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Reject" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px">
                                                                            <ItemTemplate>
                                                                                <div style="margin-left: 2px">
                                                                                    <asp:LinkButton ID="Reject"  runat="server" CommandName="Reject" CommandArgument='<%# Eval("idSumber") %>' Text=""></asp:LinkButton>
                                                                                    <%--<asp:Label ID="lblReject" runat="server" Text="" />--%>
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






<%--<script>

</script>--%>