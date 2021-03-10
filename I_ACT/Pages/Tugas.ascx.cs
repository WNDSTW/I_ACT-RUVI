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
namespace I_ACT.Pages
{
    public partial class Tugas : System.Web.UI.UserControl
    {
        #region Deklarasi
        //Base bases = new Base();
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
        Evidence evidences = new Evidence();
        EvidenceDAO evidencesDAO = new EvidenceDAO();
        Komentar komentars = new Komentar();
        KomentarDAO komentarsDAO = new KomentarDAO();

        public string namaFile = "";
        public string fileEvidence = "";
        string vKode1, vKode2, vKode3, vKode4, vKode5, vKode6, vKodeFix, vNoFix;
        Nullable<int> bulan, no;
        StringBuilder sb = new StringBuilder();
        //Log logs = new Log();
        //LogDAO logsDAO = new LogDAO();
        DataSet ds;
        public DataTable dt;
        public DataTable dtNotulenDetail = new DataTable();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["txKeterangan"] = txKeterangan.Value;
           
           
            if (!IsPostBack)
            {
                dtNotulenDetail.Columns.AddRange(new DataColumn[10] { new DataColumn("idDetail"), new DataColumn("Subjek"), new DataColumn("Isi"), new DataColumn("nopekPIC"), new DataColumn("namaPIC"), new DataColumn("NoFungsi"), new DataColumn("namaFungsi"), new DataColumn("TglJatuhTempo"), new DataColumn("idProritas"), new DataColumn("namaPrioritas") });
               
                ViewState["NotulenDetail"] = dtNotulenDetail;
                notulensDAO.UpdateStatusOverdue();
                //addPanel.Visible = false;
                //lblJudul.Text = "TAMBAH";
                bindData();
                listTugas.Visible = true;
                addEvidence.Visible = false;
                filter_ddlRekomendasi.DataSource = rekomendasisDAO.ListRekomendasi();
                filter_ddlRekomendasi.DataTextField = "NamaRekomendasi";
                filter_ddlRekomendasi.DataValueField = "IdRekomendasi";
                filter_ddlRekomendasi.DataBind();
                filter_ddlRekomendasi.Items.Insert(0, "-- Select Recommendation --");
            }
        }

      
        #region button

        protected void txSearch_TextChanged(object sender, EventArgs e)
        {
            bindData();
        }
        protected void txSearchPekerja_TextChanged(object sender, EventArgs e)
        {
            bindPekerja();
                 
        }
      
        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (txKeteranganEvidence.Text != "" && uploadEvidence.HasFile)
            {
                evidences.noNotulenDetail = view_lblNoNotulenDetail.Text;
                evidences.Keterangan = txKeteranganEvidence.Text;
                evidences.nopekSubmit = Session["nopek"].ToString();
                evidences.CreatedBy = Session["username"].ToString();


                string jenis = Path.GetExtension(uploadEvidence.FileName);
                string finalName = "Evidence-"+ uploadEvidence.FileName;
                finalName = finalName.Replace(" ", "");
                finalName = finalName.Replace("/", "_");
                uploadEvidence.PostedFile.SaveAs(Server.MapPath("~/Doc/") + finalName);
                evidences.fname = finalName;

                evidencesDAO.AddEvidence(evidences);
                kirimEmail();
                clearForm();
                addEvidence.Visible = false;
                listTugas.Visible = true;
                bindData();
            }
            else
            {
                addEvidence.Visible = true;
                listTugas.Visible = false;
            }
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindPekerja();
            PekerjaPanel.Update();
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
        protected void dgObject_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string code = "";
                string status;
                string vNoteDelegasi="";
                if (e.Item.ItemIndex > -1)
                {
                    string[] arg = new string[3];
                    arg = e.CommandArgument.ToString().Split(';');
                    code = arg[0];
                     status = arg[1];
                     vNoteDelegasi = arg[2];
                    notulens.noNotulenDetail = code;
                    evidences.noNotulenDetail = code;
                }

                if (e.CommandName.ToString() == "linkNotulen")
                {
                    if (notulensDAO.GetNotulenDetail(ref notulens))
                    {
                        if (evidencesDAO.GetEvidenceDetailByNoNotulen(ref evidences))
                        {
                            txKeteranganEvidence.Text = evidences.Keterangan;
                            linkEvidence.Text = evidences.fname;
                            fileEvidence = evidences.fname;
                            //lblIdEvidence.Text = evidences.idEvidence.ToString();
                        }
                        view_lblRekomendasi.Text = notulens.namaRekomendasi;
                        view_lblSumber.Text = notulens.namaSumber;
                        view_lblJudulNotulen.Text = notulens.judulNotulen;
                        view_lblNoDokumen.Text = notulens.noDokumen;
                        view_lblLampiran.Text = notulens.fname;
                        view_lblReviewer.Text = notulens.namaReviewer;
                        view_lblNoNotulen.Text = notulens.noNotulen;
                        view_lblNoNotulenDetail.Text = notulens.noNotulenDetail;
                        view_lblConceptor.Text = notulens.CreatedBy;
                        lblNoteDelegate.Text = vNoteDelegasi;
                        //linkEvidence.Text = notulens.fname;
                        namaFile = notulens.fname;
                        lblNamaPIC.Text = notulens.namaPIC;
                        ds = notulensDAO.SearchNotulen(code);
                        dtNotulenDetail = ds.Tables[0];
                        dgViewDetail.DataSource = ds;
                        dgViewDetail.DataBind();
                        dgComment.DataSource = komentarsDAO.SearchKomentar(code);
                        dgComment.DataBind();

                        if (Session["noLevel"].ToString() == "4")
                        {
                            trNoteDelegate.Visible = false;
                            if ( notulens.nopekDelegasi != null)
                            {
                                if (Session["idrole"].ToString() == "1")
                                {
                                    EvidencePanel.Visible = true;
                                    uploadEvidence.Enabled = true;
                                    txKeteranganEvidence.Enabled = true;
                                    btnSubmit.Visible = true;
                                    btnSubmitEvidence.Visible = true;
                                    

                                }
                                else
                                {
                                    EvidencePanel.Visible = true;
                                    btnSubmit.Visible = false;
                                    txKeteranganEvidence.Enabled = false;
                                    uploadEvidence.Enabled = false;
                                    btnSubmitEvidence.Visible = false;
                                }
                            }
                            else if (notulens.status == 2 || notulens.status == 4)
                            {
                                EvidencePanel.Visible = true;
                                btnSubmit.Visible = false;
                                txKeteranganEvidence.Enabled = false;
                                uploadEvidence.Enabled = false;
                                btnSubmitEvidence.Visible = false;
                            }
                            else
                            {
                                EvidencePanel.Visible = true;
                                uploadEvidence.Enabled = true;
                                txKeteranganEvidence.Enabled = true;
                                btnSubmit.Visible = true;
                                btnSubmitEvidence.Visible = true;

                               
                            }
                        }
                        else if (Session["noLevel"].ToString() == "5")
                        {
                            trNoteDelegate.Visible = true;
                            if (notulens.status == 2 || notulens.status == 4 )
                            {
                                EvidencePanel.Visible = true;
                                txKeteranganEvidence.Enabled = false;
                                uploadEvidence.Enabled = false;
                                btnSubmitEvidence.Visible = false;
                            }
                            else
                            {
                                
                                EvidencePanel.Visible = true;
                                txKeteranganEvidence.Enabled = true;
                                if (notulens.namaReviewer == Session["nama"].ToString())
                                {
                                    uploadEvidence.Enabled = false;
                                }
                                else
                                {
                                    uploadEvidence.Enabled = true;
                                }
                                 btnSubmitEvidence.Visible = true;
                            }
                        }

                        
                        addEvidence.Visible = true;
                        listTugas.Visible = false;
                        viewPanel.Update();
                    }
                }
                else if (e.CommandName.ToString() == "Delegate")
                {
                    pop_lblNoNotulenDetail.Text = code;
                    txCommentForDelegate.Text = vNoteDelegasi;
                    bindPekerja();
                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showModalDelegate", " $('#modalDelegate').modal('show');", true);
                    PekerjaPanel.Update();
                }
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }
        protected void dgObject_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                Label lblnamaStatus = (Label)e.Item.FindControl("lblnamaStatus");
                Label lblNamaDelegasi = (Label)e.Item.FindControl("lblNamaDelegasi");
                Label lblNamaPIC = (Label)e.Item.FindControl("lblNamaPIC");
                Label lblBy = (Label)e.Item.FindControl("lblBy");
                Label lblTglDelegasi = (Label)e.Item.FindControl("lblTglDelegasi");
                LinkButton btnDelegate = (LinkButton)e.Item.FindControl("btnDelegate");

                if (lblNamaDelegasi.Text != "" && (lblStatus.Text == "0" || lblStatus.Text == "3"))
                {
                    btnDelegate.Visible = true;
                    lblNamaDelegasi.Visible = true;
                    lblTglDelegasi.Visible = true;
                }
                else if (lblNamaDelegasi.Text != "" && (lblStatus.Text=="1" || lblStatus.Text=="2" || lblStatus.Text=="4"))
                {
                    btnDelegate.Visible = false;
                    lblNamaDelegasi.Visible = true;
                    lblTglDelegasi.Visible = true;
                }

                if (Session["nolevel"].ToString() == "5" && lblNamaDelegasi.Text != "")
                {
                    lblNamaPIC.Visible = true;
                    lblBy.Visible = true;
                }
                if (Session["nolevel"].ToString() == "5")
                {
                    btnDelegate.Visible = false;
                }

                if (lblStatus.Text == "-2")
                {
                    sb.Append("<span class='label label-default'>Draft</span>");
                    lblnamaStatus.Text = sb.ToString();
                }
                if (lblStatus.Text == "-1")
                {
                    sb.Append("<span class='label label-danger'>Rejected</span>");
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
                    sb.Append("<span class='label label-info'>Closed Overdue</span>");
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

        protected void dgPopPekerja_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgPopPekerja.CurrentPageIndex = e.NewPageIndex;
            bindPekerja();
        }

        protected void dgPopPekerja_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string code = "";
                if (e.Item.ItemIndex > -1)
                {
                    code = e.CommandArgument.ToString();
                }

                if (e.CommandName.ToString() == "Select")
                {
                    notulens.noteDelegasi = txCommentForDelegate.Text;
                    notulens.noNotulenDetail = pop_lblNoNotulenDetail.Text;
                    notulens.nopekDelegasi = code;
                    notulensDAO.DelegasiNotulen(notulens);
                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "hideModalPekerja", " $('#modalDelegate').modal('hide');", true);
                    bindData();
                    ListPanel.Update();
                }
                
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }

        protected void dgComment_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {

                string link = e.CommandArgument.ToString();

                if (e.CommandName == "linkEvidence")
                {
                    string docPdf = Server.MapPath("~/Doc/") + Path.GetFileName("~/Doc/" + link);

                    if (File.Exists(docPdf))
                    {
                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                        response.ClearContent();
                        response.Clear();
                        response.ContentType = "application/pdf";
                        response.AddHeader("Content-Disposition", "attachment; filename=" + link);
                        response.TransmitFile(Server.MapPath("~/Doc/" + link));
                        response.Flush();
                        response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        #endregion

        #region Function

        public void kirimEmail()
        {
            try
            {
                foreach (DataRow row in dtNotulenDetail.Rows)
                {
                    using (MailMessage mm = new MailMessage())
                    {
                        string email = view_lblConceptor.Text + "@pertamina.com";
                        mm.To.Add(new MailAddress(email));

                        mm.Subject = "Notifikasi IACT";
                        email = "";

                        #region ISIEMAIL
                        string IsiEmail;
                        string vSubjek = row["subjek"].ToString();
                        string vIsiRekomendasi = row["isi"].ToString();
                        string vTglJatuhTempo = row["tglJatuhTempo"].ToString();
                        string vPotensial = row["idPotential"].ToString();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<html>");
                        sb.Append("<head> <style type='text/css'> table { width=100%; } ");
                        sb.Append("th { background-color: #b5c9d8; }");
                        sb.Append("table, th, td {border: 1px solid black;  text-align:center; border-collapse: collapse;  padding: 5px;}");
                        sb.Append("p { font-size:13pt; }");
                        sb.Append("</head></style>");
                        sb.Append("<body style='font-family: Tahoma,Verdana,Segoe,sans-serif; font-size:11pt;'>");
                        //sb.Append("<font face='verdana' size='5pt'> <b>NOTIFIKASI SERTIFIKASI YANG MENDEKATI KADALUARSA</b> <font>");
                        sb.Append("<p>Dengan Hormat,</p>");
                        sb.Append("<p>PIC untuk task berikut sudah melengkapi evidence :  <b></b></p></br>");

                        sb.Append("<table style='border: 1px; text-align:left'>");
                        sb.Append("<tr><th style='border: 3px; text-align:center;background-color: #b5c9d8;'><b> Subjek </b></th>");
                        sb.Append("<th style='border: 3px; text-align:center;background-color: #b5c9d8;'> <b>Rekomendasi </b> </th>");
                        sb.Append("<th style='border: 3px; text-align:center;background-color: #b5c9d8;'> <b>Due Date </b></th>");
                        sb.Append("<th style='border: 3px; text-align:center;background-color: #b5c9d8;'> <b>Risk Rating </b></th>");
                        sb.Append("<tr><th style='border: 3px; text-align:center;'><b>" + vSubjek + " </b></th>");
                        sb.Append("<th style='border: 3px; text-align:center;'>" + vIsiRekomendasi + " </th>");
                        sb.Append("<th style='border: 3px; text-align:center;'> " + vTglJatuhTempo + "</th>");
                        sb.Append("<th style='border: 3px; text-align:center;'> " + vPotensial + "</th>");

                        sb.Append("</table> </br>");

                        sb.Append("<p>Silahkan Cek Evidence yg sudah disubmit di <a href='http://intra-ru6.pertamina.com/IACT'> <b>IACT</b></a>. Diharapkan task tersebut closed sebelum due date </p>");
                        sb.Append("<p>Terima Kasih</p>");
                        sb.Append("<footer><p><font face='verdana' size='11px'>::-- <i>Notification By</i> <a href='http://intra-ru6.pertamina.com/IACT'> <b>IACT </b></a> --::<font></P> </footer>");
                        sb.Append("</body></html>");

                        IsiEmail = sb.ToString();
                        #endregion

                        mm.Body = IsiEmail;
                        mm.IsBodyHtml = true;
                        //mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "coba.xlsx"));

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "mail.pertamina.com";
                        smtp.EnableSsl = true;
                        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential();

                        credentials.UserName = "intranet.ru6";
                        credentials.Password = "Pertamina0123";

                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = credentials;
                        //smtp.Port = 25;
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtp.Send(mm);

                    }
                }

            }
            catch (Exception ex)
            {
                clearForm();
                bindData();
                ex.ToString();
            }
        }

        protected void clearForm()
        {
            txKeteranganEvidence.Text = "";
        }
        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            listTugas.Visible = true;
            addEvidence.Visible = false;
            clearForm();
        }
     
        protected void bindData()
        {
            //grups.NamaGrup = null;
            notulens.idRekomendasi = null;
            notulens.idSumber = null;
            notulens.status = null;
            notulens.isDraft = false;
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
                DateTime TglTransaksi, TglAkhirSertifikasi, TglKadaluarsa;
                string[] formats = {"d/M/yyyy", "dd/M/yyyy", 
                         "dd/MM/yyyy", "d/MM/yyyy", "d/M/yyyy"};

                if (DateTime.TryParseExact(txTglAwal.Value, formats, CultureInfo.GetCultureInfo("en-GB").DateTimeFormat,
                                    DateTimeStyles.None, out TglTransaksi))
                {
                    vTglAwal = TglTransaksi.ToString("yyyy/MM/dd");
                }
            }
            if (txTglAkhir.Value != "")
            {
                vTglAkhir = txTglAkhir.Value;
                DateTime TglTransaksi, TglAkhirSertifikasi, TglKadaluarsa;
                string[] formats = {"d/M/yyyy", "dd/M/yyyy", 
                         "dd/MM/yyyy", "d/MM/yyyy", "d/M/yyyy"};

                if (DateTime.TryParseExact(txTglAkhir.Value, formats, CultureInfo.GetCultureInfo("en-GB").DateTimeFormat,
                                    DateTimeStyles.None, out TglTransaksi))
                {
                    vTglAkhir = TglTransaksi.ToString("yyyy/MM/dd");
                }
            }
            if (txSearch.Text == "")
            {

                DataSet ds = (DataSet)Session["MyDataSet"];
                DataView dv;
                if (Session["idrole"].ToString() == "1")
                {
                    ds = notulensDAO.ListNotulenALL(Session["username"].ToString(), notulens, vTglAwal, vTglAkhir,null);
                }
                else
                {
                    ds = notulensDAO.ListNotulenByPIC(Session["nopek"].ToString());
                }
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
                if (Session["idrole"].ToString() == "1")
                {
                    ds = notulensDAO.ListNotulenALL(Session["username"].ToString(), notulens, vTglAwal, vTglAkhir, txSearch.Text);
                }
                else
                {
                    ds = notulensDAO.SearchNotulenByPIC(txSearch.Text, Session["nopek"].ToString());
                }
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
     
        protected void bindPekerja()
        {
            if (txSearchPekerja.Text != "")
            {
                if (ddlEmployee.SelectedIndex==0)
                {
                    try
                    {
                        pekerjas.namapegawai = txSearchPekerja.Text;
                        pekerjas.NoPek = txSearchPekerja.Text;
                        pekerjas.nofungsi = int.Parse(Session["noFungsi"].ToString());
                        pekerjas.NoLevel = int.Parse(Session["noLevel"].ToString());
                        ds = pekerjasDAO.SearchStaffPekerjaByNameAndNoPek(pekerjas);
                        dgPopPekerja.DataSource = ds;
                        dgPopPekerja.DataBind();
                    }
                    catch (Exception ex)
                    {
                        dgPopPekerja.CurrentPageIndex = 0;
                        dgPopPekerja.DataBind();
                    }
                }
                else
                {
                    try
                    {
                        pekerjas.namapegawai = txSearchPekerja.Text;
                        pekerjas.NoPek = txSearchPekerja.Text;
                        ds = pekerjasDAO.SearchStaffPekerjaByNameAndNoPek(pekerjas);
                        dgPopPekerja.DataSource = ds;
                        dgPopPekerja.DataBind();
                    }
                    catch (Exception ex)
                    {
                        dgPopPekerja.CurrentPageIndex = 0;
                        dgPopPekerja.DataBind();
                    }
                }
               
            }
            else
            {
                if (ddlEmployee.SelectedIndex==1)
                {
                    try
                    {
                        ds = pekerjasDAO.ListPekerja();
                        dgPopPekerja.DataSource = ds;
                        dgPopPekerja.DataBind();
                    }
                    catch (Exception ex)
                    {
                        dgPopPekerja.CurrentPageIndex = 0;
                        dgPopPekerja.DataBind();
                    }
                }
                else
                {
                    try
                    {
                        pekerjas.nofungsi = int.Parse(Session["noFungsi"].ToString());
                        ds = pekerjasDAO.ListStaffPekerja(pekerjas);
                        dgPopPekerja.DataSource = ds;
                        dgPopPekerja.DataBind();
                    }
                    catch (Exception ex)
                    {
                        dgPopPekerja.CurrentPageIndex = 0;
                        dgPopPekerja.DataBind();
                    } 
                }
            }
        }
 
        #endregion

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
        }

        protected void filter_ddlSource_TextChanged(object sender, EventArgs e)
        {
            bindData();
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            bindData();
            ListPanel.Update();
        }

        

    

      



        



     


       

    }
}