using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using I_ACT.DAO;
using I_ACT.Domain;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;

namespace I_ACT.Pages
{
    public partial class Dashboard : System.Web.UI.UserControl
    {
        Rekomendasi rekomendasis = new Rekomendasi();
        RekomendasiDAO rekomendasisDAO = new RekomendasiDAO();

        Sumber sumbers = new Sumber();
        SumberDAO sumbersDAO = new SumberDAO();
        Notulen notulens = new Notulen();
        NotulenDAO notulensDAO = new NotulenDAO();
        StringBuilder sb = new StringBuilder();

        public int Open = 0;
        public int WaitingApproval = 0;
        public int Closed = 0;
        public int Overdue = 0;
        public int ClosedOverdue = 0;
        public int Reject = 0;

        public double totalPerencanaan = 0;
        public double PersenOpen = 0;
        public double PersenWaitingApproval = 0;
        public double PersenClosed = 0;
        public double PersenOverdue = 0;
        public double PersenClosedOverdue = 0;
        public double PersenReject = 0;
        public DataSet dsSummary;
        List<string> NamafungsiList = new List<string>();
        List<int> totalList = new List<int>();
        public string NamaFungsi;
        public string total;
        List<string> colorList = new List<string>();
        List<string> BcolorList = new List<string>();
        public string rColor;
        public string bColor;
        DataSet dsModal;
        //public string 

        protected void bindChart(int idRekomendasi)
        {
            Open = notulensDAO.cekJumlahStatusByNopek(0, "0000000", idRekomendasi);
            WaitingApproval = notulensDAO.cekJumlahStatusByNopek(1, "0000000", idRekomendasi);
            Closed = notulensDAO.cekJumlahStatusByNopek(2, "0000000", idRekomendasi);
            Overdue = notulensDAO.cekJumlahStatusByNopek(3, "0000000", idRekomendasi);
            ClosedOverdue = notulensDAO.cekJumlahStatusByNopek(4, "0000000", idRekomendasi);
            Reject = notulensDAO.cekJumlahStatusByNopek(-1, "0000000", idRekomendasi);
            dsSummary = notulensDAO.SummaryByFungsi(idRekomendasi);

            totalPerencanaan = Open + WaitingApproval + Closed + Overdue + ClosedOverdue + Reject;
            if (totalPerencanaan == 0)
            {
                Open = 0;
                WaitingApproval = 0;
                Closed = 0;
                Overdue = 0;
                ClosedOverdue = 0;
                Reject = 0;
            }
            else if (totalPerencanaan > 0)
            {
                PersenOpen = Convert.ToDouble(Open * 100 / totalPerencanaan);
                PersenWaitingApproval = Convert.ToDouble(WaitingApproval * 100 / totalPerencanaan);
                PersenClosed = Convert.ToDouble(Closed * 100 / totalPerencanaan);
                PersenOverdue = Convert.ToDouble(Overdue * 100 / totalPerencanaan);
                PersenClosedOverdue = Convert.ToDouble(ClosedOverdue * 100 / totalPerencanaan);
                PersenReject = Convert.ToDouble(Reject * 100 / totalPerencanaan);

            }
            foreach (DataRow drFungsi in dsSummary.Tables[0].Rows)
            {
                //var color = String.Format("#CC{0:X6}", "23dbef");
                NamafungsiList.Add(drFungsi["fungsiName"].ToString());
                totalList.Add(Convert.ToInt32(drFungsi["total"]));
                colorList.Add("#23dbef");
                BcolorList.Add("#FCCE56");
            }
            NamaFungsi = new JavaScriptSerializer().Serialize(NamafungsiList.ToArray());
            total = new JavaScriptSerializer().Serialize(totalList.ToArray());
            rColor = new JavaScriptSerializer().Serialize(colorList.ToArray());
            bColor = new JavaScriptSerializer().Serialize(BcolorList.ToArray());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "loadPie(" + PersenOpen + "," + PersenWaitingApproval + "," + PersenClosed + "," + PersenOverdue + "," + PersenClosedOverdue + "," + PersenReject + ");loadBar(" + NamaFungsi + "," + rColor + "," + bColor + "," + total + ");", true);
            Persen.Update();
        }

        //protected void BindBar (int idRekomendasi)
        //{
           

          

            
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "loadBar(" + NamaFungsi + "," + rColor + "," + bColor + "," + total + ");", true);
        //}


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int x = 0;
                DataSet dsSumber;
                DataSet ds;
                ds = rekomendasisDAO.ListRekomendasi();
                DataTable table = ds.Tables[0];
                foreach (DataRow dr in table.Rows)
                {
                    AjaxControlToolkit.TabPanel tabpanel = new AjaxControlToolkit.TabPanel();
                    tabpanel.HeaderText = dr["NamaRekomendasi"].ToString();
                    tabpanel.ID = dr["idRekomendasi"].ToString();
                    sumbers.idRekomendasi = Convert.ToInt32(dr["idRekomendasi"].ToString());
                    dsSumber = sumbersDAO.SearchSumberbyIdRekomendasi(sumbers);
                    LoadDashboard ct = (LoadDashboard)LoadControl("~/Pages/LoadDashboard.ascx");
                    Label idrek = (Label)ct.FindControl("idRekomendasi");
                    DataGrid dg = (DataGrid)ct.FindControl("dgSumber");
                    dg.DataSource = dsSumber;
                    dg.DataBind();
                    idrek.Text = dr["idRekomendasi"].ToString();
                    tabpanel.Controls.Add(ct);
                    TabCtr.Tabs.Add(tabpanel);
                    if (x==0)
                    {
                        bindChart(Convert.ToInt32(dr["idRekomendasi"].ToString()));
                    }
                    x++;
                }
            }
            else
            {
                DataSet dsSumber;
                DataSet ds;
                ds = rekomendasisDAO.ListRekomendasi();
                DataTable table = ds.Tables[0];
                foreach (DataRow dr in table.Rows)
                {
                    AjaxControlToolkit.TabPanel tabpanel = new AjaxControlToolkit.TabPanel();
                    //tabpanel.HeaderTemplate = "<div style="float: right; padding-left: 5px; color: blue">Rapat Bisnis & Operasi</div>"
                    tabpanel.HeaderText = dr["NamaRekomendasi"].ToString();
                    tabpanel.ID = dr["idRekomendasi"].ToString();
                    sumbers.idRekomendasi = Convert.ToInt32(dr["idRekomendasi"].ToString());
                    dsSumber = sumbersDAO.SearchSumberbyIdRekomendasi(sumbers);
                    LoadDashboard ct = (LoadDashboard)LoadControl("~/Pages/LoadDashboard.ascx");
                    Label idrek = (Label)ct.FindControl("idRekomendasi");
                    DataGrid dg = (DataGrid)ct.FindControl("dgSumber");
                    dg.DataSource = dsSumber;
                    dg.DataBind();
                    idrek.Text = dr["idRekomendasi"].ToString();
                    tabpanel.Controls.Add(ct);
                    TabCtr.Tabs.Add(tabpanel);
                }

                //bindChart(Convert.ToInt32(TabCtr.ActiveTab.ID)); 
            }
        }

      

        #region Datagrid
        //protected void dgObject_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        //{

        //}

        //protected void dgObject_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    try
        //    {
        //        string code = "";
        //        if (e.Item.ItemIndex > -1)
        //        {
        //            code = e.CommandArgument.ToString();
        //            notulens.idRekomendasi = int.Parse(code);
        //        }

        //        if (e.CommandName.ToString() == "linkRekomendasi")
        //        {
        //            //dgObject.Visible = false;
        //            //bindDataSumber(int.Parse(code));
        //            dgSumber.Visible = true;
        //            panel1.Update();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string eror = ex.ToString();

        //    }
        //}

        //protected void dgObject_ItemDataBound(object sender, DataGridItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label lblOpen = (Label)e.Item.FindControl("lblOpen");
        //        Label lblWaitingApproval = (Label)e.Item.FindControl("lblWaitingApproval");
        //        Label lblClosed = (Label)e.Item.FindControl("lblClosed");
        //        Label lblOverdue = (Label)e.Item.FindControl("lblOverdue");
        //        Label lblClosedOverdue = (Label)e.Item.FindControl("lblClosedOverdue");
        //        Label lblReject = (Label)e.Item.FindControl("lblReject");
        //        Label lblIdRekomendasi = (Label)e.Item.FindControl("lblIdRekomendasi");
        //        int idrec = int.Parse(lblIdRekomendasi.Text);
        //        lblOpen.Text = notulensDAO.cekJumlahStatus(0, idrec).ToString();
        //        lblWaitingApproval.Text = notulensDAO.cekJumlahStatus(1, idrec).ToString();
        //        lblClosed.Text = notulensDAO.cekJumlahStatus(2, idrec).ToString();
        //        lblOverdue.Text = notulensDAO.cekJumlahStatus(3, idrec).ToString();
        //        lblClosedOverdue.Text = notulensDAO.cekJumlahStatus(4, idrec).ToString();
        //        lblReject.Text = notulensDAO.cekJumlahStatus(-1, idrec).ToString();

        //    }
        //}

        //protected void dgSumber_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        //{

        //}

        //protected void dgSumber_ItemCommand(object source, DataGridCommandEventArgs e)
        //{
        //    try
        //    {
        //        int code = 0;
        //        if (e.Item.ItemIndex > -1)
        //        {
        //            code = Convert.ToInt32( e.CommandArgument.ToString());
        //            //notulens.noNotulenDetail = code;
                   
        //        }
        //        if (e.CommandName.ToString() == "linkSumber")
        //        {
        //            dgViewDetail.DataSource = notulensDAO.ListTaskByDashboard("", 25, code);
        //            dgViewDetail.DataBind();
                    
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ShowModal();", true);
                   
        //            viewPanel.Update();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string eror = ex.ToString();
        //        ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

        //    }
          
            
        //}

        //protected void dgSumber_ItemDataBound(object sender, DataGridItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label lblOpen = (Label)e.Item.FindControl("lblOpen");
        //        Label lblWaitingApproval = (Label)e.Item.FindControl("lblWaitingApproval");
        //        Label lblClosed = (Label)e.Item.FindControl("lblClosed");
        //        Label lblOverdue = (Label)e.Item.FindControl("lblOverdue");
        //        Label lblClosedOverdue = (Label)e.Item.FindControl("lblClosedOverdue");
        //        Label lblReject = (Label)e.Item.FindControl("lblReject");
        //        Label lblIdSumber = (Label)e.Item.FindControl("lblIdSumber");
        //        int idSource = int.Parse(lblIdSumber.Text);
        //        lblOpen.Text = notulensDAO.cekJumlahStatusSumber(0, idSource).ToString();
        //        lblWaitingApproval.Text = notulensDAO.cekJumlahStatusSumber(1, idSource).ToString();
        //        lblClosed.Text = notulensDAO.cekJumlahStatusSumber(2, idSource).ToString();
        //        lblOverdue.Text = notulensDAO.cekJumlahStatusSumber(3, idSource).ToString();
        //        lblClosedOverdue.Text = notulensDAO.cekJumlahStatusSumber(4, idSource).ToString();
        //        lblReject.Text = notulensDAO.cekJumlahStatusSumber(-1, idSource).ToString();

        //    }
        //}
        #endregion
        
        //public void bindDataRekomendasi()
        //{
        //    DataSet ds;
        //    DataView dv;
        //    ds = rekomendasisDAO.ListRekomendasi();
        //    dgObject.DataSource = ds;
        //    dgObject.DataBind();
        //}

        //public void bindDataSumber(int idrec)
        //{
        //    DataSet ds;
        //    DataView dv;
        //    sumbers.idRekomendasi = idrec;
        //    ds = sumbersDAO.SearchSumberbyIdRekomendasi(sumbers);
        //    dgSumber.DataSource = ds;
        //    dgSumber.DataBind();
        //}

        //public void bindDataSumber()
        //{
        //    DataSet ds;
        //    DataView dv;
        //    sumbers.idRekomendasi = 25;
        //    ds = sumbersDAO.SearchSumberbyIdRekomendasi(sumbers);

        //    //dgSumber.DataSource = ds;
        //    //dgSumber.DataBind();
        //}

        protected void Button1_ServerClick(object sender, EventArgs e)
        {

        }

        public void ShowModal(int idRekomendation, int idSumber)
        {
            //dsModal = notulensDAO.ListTaskByDashboard("", 25, 57);
            //btnShow_Click(new object(), new EventArgs());
            //dgViewDetail.DataSource = notulensDAO.ListTaskByDashboard("", idRekomendation, idSumber);
            //dgViewDetail.DataBind();


            //////ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
            //ScriptManager.RegisterClientScriptBlock(viewPanel, this.viewPanel.GetType(), "shwModal", "ShowModal();", true);
            //viewPanel.Update();
           
            
        }
        protected void TabCtr_ActiveTabChanged(object sender, EventArgs e)
        {
            bindChart(Convert.ToInt32(TabCtr.ActiveTab.ID));
        }

        protected void dgViewDetail_ItemDataBound1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                Label lblnamaStatus = (Label)e.Item.FindControl("lblnamaStatus");
                Label lblCreatedBy = (Label)e.Item.FindControl("lblCreatedBy");

                if (lblStatus.Text == "-2")
                {
                    sb.Append("<span class='label label-default'>Draft</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                if (lblStatus.Text == "-1")
                {
                    sb.Append("<span class='label label-default'>Rejected</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                if (lblStatus.Text == "0")
                {
                    sb.Append("<span class='label label-success'>Open</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                else if (lblStatus.Text == "1")
                {
                    sb.Append("<span class='label label-warning'>Waiting Approval</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                else if (lblStatus.Text == "2")
                {
                    sb.Append("<span class='label label-primary'>Closed</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                else if (lblStatus.Text == "3")
                {
                    sb.Append("<span class='label label-danger'>Overdue</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                else if (lblStatus.Text == "4")
                {
                    sb.Append("<span class='label label-danger'>Closed Overdue</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                sb.Clear();
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            //dgViewDetail.DataSource = notulensDAO.ListTaskByDashboard("", 25, 57); 
            //dgViewDetail.DataBind();

            //viewPanel.Update();
            ////ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
            //ShowModal(25, 57);
        }

    }
}