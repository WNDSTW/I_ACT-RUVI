using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using I_ACT.Domain;
using I_ACT.DAO;
using System.Data;
using System.Text;
using I_ACT.Pages;
using System.IO;
using System.DirectoryServices;
using System.Globalization;


using System.Security.Authentication;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;


using DocumentFormat.OpenXml;
using ClosedXML.Excel;
namespace I_ACT.Pages
{
    public partial class ReportAllTask : System.Web.UI.UserControl
    {
        #region Deklarasi
        //Base bases = new Base();
        string email = "";

        //string ToEmail = email;
        string From = "mk.nurochman.k@mitrakerja.pertamina.com";
        Sumber sumbers = new Sumber();
        SumberDAO sumbersDAO = new SumberDAO();
        Rekomendasi rekomendasis = new Rekomendasi();
        RekomendasiDAO rekomendasisDAO = new RekomendasiDAO();
        Notulen notulens = new Notulen();
        NotulenDAO notulensDAO = new NotulenDAO();
        Pekerja pekerjas = new Pekerja();
        PekerjaDAO pekerjasDAO = new PekerjaDAO();
        Counter counters = new Counter();
        CounterDAO countersDAO = new CounterDAO();
        Komentar komentars = new Komentar();
        KomentarDAO komentarsDAO = new KomentarDAO();
        Evidence evidences = new Evidence();
        EvidenceDAO evidencesDAO = new EvidenceDAO();
        string vKode1, vKode2, vKode3, vKode4, vKode5, vKode6, vKodeFix, vNoFix;
        Nullable<int> bulan, no;
        StringBuilder sb = new StringBuilder();
        //Log logs = new Log();
        //LogDAO logsDAO = new LogDAO();
        DataSet ds;
        public DataTable dt;
        public DataTable dtNotulenDetail = new DataTable();
        public DataTable dtFungsiDetail = new DataTable();
        public DataTable dtPICDetail = new DataTable();
        Nullable<DateTime> dueDate;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //fileUpload.Attributes["onchange"] = "UploadFile(this)";
          
            if (!IsPostBack)
            {
                dtNotulenDetail.Columns.AddRange(new DataColumn[12] { new DataColumn("idDetail"), new DataColumn("Subjek"), new DataColumn("Isi"), new DataColumn("nopekPIC"), new DataColumn("namaPIC"), new DataColumn("NoFungsi"),
                    new DataColumn("namaFungsi"), new DataColumn("TglJatuhTempo"), new DataColumn("idSeverity"),
                    new DataColumn("idProbability"), new DataColumn("idPotential"), new DataColumn("email") });

                dtFungsiDetail.Columns.AddRange(new DataColumn[3] { new DataColumn("idDetail"), new DataColumn("idFungsi"), new DataColumn("Fungsi") });
                dtPICDetail.Columns.AddRange(new DataColumn[6] { new DataColumn("idDetail"), new DataColumn("nopek"), new DataColumn("nama"), new DataColumn("jabatan"), new DataColumn("idPosition"), new DataColumn("email") });
              
                ViewState["NotulenDetail"] = dtNotulenDetail;
                ViewState["FungsiDetail"] = dtFungsiDetail;
                ViewState["PICDetail"] = dtPICDetail;

                notulensDAO.UpdateStatusOverdue();
                //addPanel.Visible = false;
                //lblJudul.Text = "TAMBAH";
                bindData();
                bindDdl();
            }
        }

      
        #region button

        protected void btnExcel_ServerClick(object sender, EventArgs e)
        {
            DataTable dtReport = new DataTable();
            notulens.idRekomendasi = null;
            notulens.idSumber = null;
            notulens.status = null;
            string vTglAkhir = null;
            string vTglAwal = null;
            if (filter_ddlRekomendasi.SelectedIndex > 0)
            {
                notulens.idRekomendasi = int.Parse(filter_ddlRekomendasi.SelectedValue.ToString());
            }

            if (filter_ddlSource.SelectedIndex > 0)
            {
                notulens.idSumber = int.Parse(filter_ddlSource.SelectedValue.ToString());
            }
            if (txTglAwal.Value != "")
            {
                vTglAwal = txTglAwal.Value;
            }
            if (txTglAkhir.Value != "")
            {
                vTglAkhir = txTglAkhir.Value;
            }

            if (txSearch.Text == "")
            {
                DataSet ds = (DataSet)Session["MyDataSet"];
                DataView dv;
                ds = notulensDAO.ListNotulenALL(Session["username"].ToString(), notulens, vTglAwal, vTglAkhir,null);

                dv = ds.Tables[0].DefaultView;
                dv.Sort = (String)ViewState["SortExpression"];
                if (ViewState["SortAscending"] == "no")
                    dv.Sort += " DESC";

                dtReport = ds.Tables[0];
            }
            else
            {
                DataSet ds = (DataSet)Session["MyDataSet"];
                DataView dv;

                ds = notulensDAO.SearchNotulenByALL(txSearch.Text, Session["username"].ToString(), notulens, vTglAwal, vTglAkhir);

                dv = ds.Tables[0].DefaultView;
                dv.Sort = (String)ViewState["SortExpression"];
                if (ViewState["SortAscending"] == "no")
                    dv.Sort += " DESC";

                dtReport = ds.Tables[0];
            }

            //while (dtReport.Columns.Count > 8)
            //{
            //    dtReport.Columns.RemoveAt(dtReport.Columns.Count - 1);
            //}

            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add(dtReport, "TASK REPORT");

            
            ws.Row(1).InsertRowsAbove(5);
            ws.Cell("E1").Value = "Action Tracking Report";

            if (filter_ddlRekomendasi.SelectedIndex >= 1)
            {
                ws.Cell("A2").Value = "Recommendation :";
                ws.Cell("B2").Value = filter_ddlRekomendasi.SelectedItem.ToString();
            }
            if (filter_ddlSource.SelectedIndex >= 1)
            {
                ws.Cell("A3").Value = "Source :";
                ws.Cell("B3").Value = filter_ddlSource.SelectedItem.ToString();
            }

            if (txTglAwal.Value != "" || txTglAkhir.Value != "")
            {
                ws.Cell("A4").Value = "Date :";
                ws.Cell("B4").Value = txTglAwal.Value + " - " + txTglAkhir.Value;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=TaskReport_IACT.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void txSearch_TextChanged(object sender, EventArgs e)
        {
            bindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindData();
        }

      
        protected void filter_ddlRekomendasi_TextChanged(object sender, EventArgs e)
        {
            if (filter_ddlRekomendasi.SelectedIndex > 0)
            {
                sumbers.idRekomendasi = int.Parse(filter_ddlRekomendasi.SelectedValue.ToString());
                filter_ddlSource.DataSource = sumbersDAO.SearchSumberbyIdRekomendasi(sumbers);
                filter_ddlSource.DataTextField = "NamaSumber";
                filter_ddlSource.DataValueField = "idSumber";
                filter_ddlSource.DataBind();
                filter_ddlSource.Items.Insert(0, "-- Select Source --");
                bindData();
            }
            else
            {
                bindData();
            }
        }

        protected void filter_ddlSource_TextChanged(object sender, EventArgs e)
        {
            bindData();
        }

        #endregion
   
        #region Gridview
     
        protected void dgObject_SortCommand(object source, DataGridSortCommandEventArgs e)
        {

            // Caches the current information
            String strSortBy = (String)ViewState["SortExpression"];
            String strSortAscending = (String)ViewState["SortAscending"];

            // Sets the new sort expression
            ViewState["SortExpression"] = e.SortExpression;

            // If you click on the sorted column, the order reverses
            if (e.SortExpression == strSortBy)
                ViewState["SortAscending"] = (strSortAscending == "yes"
                    ? "no" : "yes");
            else
                // Defaults to ascending order
                ViewState["SortAscending"] = "yes";

            bindData();
        }

        protected void dgObject_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                String strSortBy = (String)ViewState["SortExpression"];
                String strSortAscending = (String)ViewState["SortAscending"];
                String strOrder = (strSortAscending == "yes" ? "  <i class='fa fa-sort-alpha-asc fa-lg' aria-hidden='true'></i>" : " <i class='fa fa-sort-alpha-desc fa-lg' aria-hidden='true'></i>");

                for (int i = 0; i < dgObject.Columns.Count; i++)
                {
                    // Draw the glyph to reflect sorting
                    if (strSortBy == dgObject.Columns[i].SortExpression)
                    {
                        TableCell cell = e.Item.Cells[i];
                        Label lblSorted = new Label();
                        lblSorted.Font.Name = "webdings";
                        lblSorted.Font.Size = FontUnit.Small;
                        lblSorted.Attributes.Add("style", "float:right");
                        lblSorted.Text = strOrder;
                        cell.Controls.Add(lblSorted);
                    }
                }
            }
        }

        protected void dgObject_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgObject.CurrentPageIndex = e.NewPageIndex;
            bindData();
        }
        protected void dgObject_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                Label lblnamaStatus = (Label)e.Item.FindControl("lblnamaStatus");
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
                //var dataRow = dtMatrix.AsEnumerable().
                //    Where(x => x.Field<string>("initial") == "edit" &&
                //    x.Field<int>("menuid") == Int32.Parse(Request.QueryString["Id"]));
                //if (dataRow.Count() > 0)
                //{
                //    BtnEdit.Visible = true;
                //}
                //else
                //{
                //    BtnEdit.Visible = false;
                //}

                //dataRow = dtMatrix.AsEnumerable().
                //    Where(x => x.Field<string>("initial") == "delete" &&
                //    x.Field<int>("menuid") == Int32.Parse(Request.QueryString["Id"]));
                //if (dataRow.Count() > 0)
                //{
                //    BtnDelete.Visible = true;
                //}
                //else
                //{
                //    BtnDelete.Visible = false;
                //}
            }
        }

     
        #endregion

        #region Function


        protected void btnClose_ServerClick(object sender, EventArgs e)
        {

            TaskListPanel.Visible = true;
            ListPanel.Update();
        }

        protected void bindData()
        {
            
            //grups.NamaGrup = null;
            notulens.idRekomendasi = null;
            notulens.idSumber = null;
            notulens.status = null;
            string vTglAkhir = null;
            string vTglAwal = null;
            if (filter_ddlRekomendasi.SelectedIndex > 0)
            {
                notulens.idRekomendasi =int.Parse(filter_ddlRekomendasi.SelectedValue.ToString());
            }

            if (filter_ddlSource.SelectedIndex > 0)
            {
                notulens.idSumber = int.Parse(filter_ddlSource.SelectedValue.ToString());
            }
            if (txTglAwal.Value != "")
            {
                vTglAwal = txTglAwal.Value;
            }
            if (txTglAkhir.Value != "")
            {
                vTglAkhir = txTglAkhir.Value;
            }

            if (txSearch.Text == "")
            {
                DataSet ds = (DataSet)Session["MyDataSet"];
                DataView dv;
                ds = notulensDAO.ListNotulenALL(Session["username"].ToString(), notulens,vTglAwal, vTglAkhir,null);
               
                dv = ds.Tables[0].DefaultView;
                dv.Sort = (String)ViewState["SortExpression"];
                if (ViewState["SortAscending"] == "no")
                    dv.Sort += " DESC";

                // Rebind data 
                dgObject.DataSource = dv;
                dgObject.DataBind();
            }
            else
            {
                DataSet ds = (DataSet)Session["MyDataSet"];
                DataView dv;
                
                    ds = notulensDAO.SearchNotulenByALL(txSearch.Text, Session["username"].ToString(), notulens, vTglAwal, vTglAkhir);
                
                dv = ds.Tables[0].DefaultView;
                dv.Sort = (String)ViewState["SortExpression"];
                if (ViewState["SortAscending"] == "no")
                    dv.Sort += " DESC";

                // Rebind data 
                dgObject.DataSource = dv;

                if (dgObject.CurrentPageIndex >= 0)
                {
                    dgObject.CurrentPageIndex = 0;
                }
                dgObject.DataBind();
            }

            //ddlFungsi.DataSource = fungsisDAO.ListFungsi();
            //ddlFungsi.DataTextField = "Fungsi";
            //ddlFungsi.DataValueField = "NoFungsi";
            //ddlFungsi.DataBind();
            //ddlFungsi.Items.Insert(0, "-- Select Fungsi --");

            //ddlCostCenter.DataSource = costcentersDAO.ListCostCenterDdl();
            //ddlCostCenter.DataTextField = "CostCenter";
            //ddlCostCenter.DataValueField = "CostCenterID";
            //ddlCostCenter.DataBind();
            //ddlCostCenter.Items.Insert(0, "-- Select CostCenter --");
        }
        protected void bindDdl()
        {

            filter_ddlRekomendasi.DataSource = rekomendasisDAO.ListRekomendasi();
            filter_ddlRekomendasi.DataTextField = "NamaRekomendasi";
            filter_ddlRekomendasi.DataValueField = "IdRekomendasi";
            filter_ddlRekomendasi.DataBind();
            filter_ddlRekomendasi.Items.Insert(0, "-- Select Recommendation --");

        }

       
        #endregion


       
        protected void txTglAkhir_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txTglAwal_TextChanged(object sender, EventArgs e)
        {

        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //if (fileUpload.HasFile)
            //{
            //    string jenis = Path.GetExtension(fileUpload.FileName);
            //    string finalName = fileUpload.FileName + jenis;
            //    finalName = finalName.Replace(" ", "");
            //    finalName = finalName.Replace("/", "_");
            //    fileUpload.PostedFile.SaveAs(Server.MapPath("~/Doc/") + finalName);
            //    lblNamaFile.Visible = true;
            //    lblNamaFile.Text = finalName;
            //}
            
        }

        //string ToEmail = "mk.imam.nugraha@mitrakerja.pertamina.com, rizal.achmadi@pertamina.com";
   

        
      





     


       

    }
}