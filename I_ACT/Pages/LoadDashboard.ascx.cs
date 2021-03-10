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
    public partial class LoadDashboard : System.Web.UI.UserControl
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

        protected void bindChart()
        {
            Open = notulensDAO.cekJumlahStatusByNopek(0, "0000000",  Convert.ToInt32( idRekomendasi.Text));
            WaitingApproval = notulensDAO.cekJumlahStatusByNopek(1, "0000000",  Convert.ToInt32( idRekomendasi.Text));
            Closed = notulensDAO.cekJumlahStatusByNopek(2, "0000000",  Convert.ToInt32( idRekomendasi.Text));
            Overdue = notulensDAO.cekJumlahStatusByNopek(3, "0000000",  Convert.ToInt32( idRekomendasi.Text));
            ClosedOverdue = notulensDAO.cekJumlahStatusByNopek(4, "0000000",  Convert.ToInt32( idRekomendasi.Text));
            Reject = notulensDAO.cekJumlahStatusByNopek(-1, "0000000",  Convert.ToInt32( idRekomendasi.Text));
            dsSummary = notulensDAO.SummaryByFungsi( Convert.ToInt32( idRekomendasi.Text));

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

            //var random = new Random();
            foreach (DataRow drFungsi in dsSummary.Tables[0].Rows)
            {
                //var color = String.Format("#CC{0:X6}", "23dbef");
                NamafungsiList.Add(drFungsi["fungsiName"].ToString());
                totalList.Add(Convert.ToInt32(drFungsi["total"]));
                colorList.Add("#23dbef");
                BcolorList.Add("#FCCE56");
            }
            string namePie = "Pie" + idRekomendasi.Text;
            var nameBar = "Bar" + idRekomendasi.Text;
            NamaFungsi = new JavaScriptSerializer().Serialize(NamafungsiList.ToArray());
            total = new JavaScriptSerializer().Serialize(totalList.ToArray());
            rColor = new JavaScriptSerializer().Serialize(colorList.ToArray());
            bColor = new JavaScriptSerializer().Serialize(BcolorList.ToArray());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "appendPie('" + namePie + "');", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "loadPie(" + PersenOpen + "," + PersenWaitingApproval + "," + PersenClosed + "," + PersenOverdue + "," + PersenClosedOverdue + "," + PersenReject +",'" +namePie+"');", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "loadBar(" + NamaFungsi + "," + rColor + "," + bColor + "," + total + ");", true);
            //JavaScriptSerializer().Serialize(NamaFungsi);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //bindChart();
        }

        protected void dgSumber_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
             if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
             {
                 LinkButton lblOpen = (LinkButton)e.Item.FindControl("Open");

                 LinkButton lblWaitingApproval = (LinkButton)e.Item.FindControl("WaitingApproval");
                 LinkButton lblClosed = (LinkButton)e.Item.FindControl("Closed");
                 LinkButton lblOverdue = (LinkButton)e.Item.FindControl("Overdue");
                 LinkButton lblClosedOverdue = (LinkButton)e.Item.FindControl("ClosedOverdue");
                 LinkButton lblReject = (LinkButton)e.Item.FindControl("Reject");
                 Label lblIdSumber = (Label)e.Item.FindControl("lblIdSumber");
                 int idSource = int.Parse(lblIdSumber.Text);
                 lblOpen.Text = notulensDAO.cekJumlahStatusSumber(0, idSource).ToString();
                 lblWaitingApproval.Text = notulensDAO.cekJumlahStatusSumber(1, idSource).ToString();
                 lblClosed.Text = notulensDAO.cekJumlahStatusSumber(2, idSource).ToString();
                 lblOverdue.Text = notulensDAO.cekJumlahStatusSumber(3, idSource).ToString();
                 lblClosedOverdue.Text = notulensDAO.cekJumlahStatusSumber(4, idSource).ToString();
                 lblReject.Text = notulensDAO.cekJumlahStatusSumber(-1, idSource).ToString();

                 LinkButton lblTotalRekomendasi = (LinkButton)e.Item.FindControl("TotalRekomendasi");
                 lblTotalRekomendasi.Text = notulensDAO.cekTotalRekomendasi(idSource).ToString();

             }
        }

        protected void dgSumber_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

        }

        protected void dgSumber_ItemCommand(object source, DataGridCommandEventArgs e)
        {
             try
             {
                 int code = 0;
                 //if (e.Item.ItemIndex == 0)
                 //{
                 //    code = Convert.ToInt32(e.CommandArgument.ToString());
                 //    //notulens.noNotulenDetail = code;

                 //}
                 code = Convert.ToInt32(e.CommandArgument.ToString());
                 if (e.CommandName.ToString() == "linkSumber")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboard("", Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();
                    
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }
                 if (e.CommandName.ToString() == "TotalRekomendasi")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboard("", Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();

                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }
                 if (e.CommandName.ToString() == "Open")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboardStatus(0, Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();
                     
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }
                 if (e.CommandName.ToString() == "WaitingApproval")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboardStatus(1, Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();
                     
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }
                 if (e.CommandName.ToString() == "Closed")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboardStatus(2, Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();
                     
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }
                 if (e.CommandName.ToString() == "Overdue")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboardStatus(3, Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();
                     
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }
                 if (e.CommandName.ToString() == "ClosedOverdue")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboardStatus(4, Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();
                     
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }
                 if (e.CommandName.ToString() == "Reject")
                 {
                     //Dashboard dshb = (Dashboard)LoadControl("~/Pages/Dashboard.ascx");
                     UpdatePanel pn = (UpdatePanel)this.Parent.FindControl("viewPanel");
                     DataGrid dg = (DataGrid)this.Parent.FindControl("dgViewDetail");
                     dg.DataSource = notulensDAO.ListTaskByDashboardStatus(-1, Convert.ToInt32(idRekomendasi.Text), code);
                     dg.DataBind();
                   
                     ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "shwModal", "ShowModal();", true);
                     pn.Update();
                 }

             }
             catch (Exception ex)
             {
                 string eror = ex.ToString();
                 //ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

             }
          
        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {

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
    }
}