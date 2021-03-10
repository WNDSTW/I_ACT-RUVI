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
using AjaxControlToolkit;
namespace I_ACT.Pages
{
    public partial class MonitorTindakan : System.Web.UI.UserControl
    {
        #region Deklarasi
        //Base bases = new Base();
        string email = "";

        //string ToEmail = email;
        string From = "intranet.ru6@pertamina.com";
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
        string vKode1, vKode2, vKode3, vKode4, vKode5, vKode6, vKodeFix, vNoFix, vNamaFile, vNoPek;
        Nullable<int> bulan, no;
        StringBuilder sb = new StringBuilder();
        //Log logs = new Log();
        //LogDAO logsDAO = new LogDAO();
        DataSet ds;
        public string namaFile = "";
        public string fileEvidence = "";
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
                dtNotulenDetail.Columns.AddRange(new DataColumn[11] { new DataColumn("NoNotulenDetail"), new DataColumn("Subjek"), new DataColumn("Isi"), new DataColumn("nopekPIC"), new DataColumn("namaPIC"), new DataColumn("NoFungsi"),
                    new DataColumn("namaFungsi"), new DataColumn("TglJatuhTempo"), new DataColumn("idSeverity"),
                    new DataColumn("idProbability"), new DataColumn("idPotential") });

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

        protected void txSearch_TextChanged(object sender, EventArgs e)
        {
            bindData();
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                dtNotulenDetail = ViewState["NotulenDetail"] as DataTable;
                if (validasiData())
                {
                    Session.Add("dtNotulenDetail", dtNotulenDetail);
                    GetCounter("Head");
                    notulens.noNotulen = vKodeFix;
                    notulens.judulNotulen = txJudul.Value;
                    notulens.idRekomendasi = int.Parse(ddlRekomendasi.SelectedValue.ToString());
                    notulens.idSumber = int.Parse(ddlSumber.SelectedValue.ToString());
                    notulens.noDokumen = txNoDokumen.Value;
                    notulens.nopekReviewer = ddlReviever.SelectedValue.ToString();
                    notulens.nopekConceptor = Session["nopek"].ToString();
                    notulens.tglNotulen = DateTime.Parse(txTaskDate.Value);
                    notulens.CreatedBy = Session["username"].ToString();
                    notulens.status = 0;
                    notulens.fname = Session["namaFile"].ToString();
                    
                    notulensDAO.AddNotulen(notulens);
                    
                    AddTaskPanel.Visible = false;
                    TaskListPanel.Visible = true;
                    clearForm();
                    bindData();
                    clearDetail();
                    ////kirimEmail();
                    //kirimEmailReviewer();
                    ListPanel.Update(); 

                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showSuccess", "successMessage();", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(Data Not Valid);", true);
                }
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }
        protected void btnDraft_ServerClick(object sender, EventArgs e)
        {
            try
            {
                dtNotulenDetail = ViewState["NotulenDetail"] as DataTable;
                if (validasiData())
                {
                    Session.Add("dtNotulenDetail", dtNotulenDetail);
                    GetCounter("Head");
                    notulens.noNotulen = vKodeFix;
                    notulens.judulNotulen = txJudul.Value;
                    notulens.idRekomendasi = int.Parse(ddlRekomendasi.SelectedValue.ToString());
                    notulens.idSumber = int.Parse(ddlSumber.SelectedValue.ToString());
                    notulens.noDokumen = txNoDokumen.Value;
                    notulens.nopekReviewer = ddlReviever.SelectedValue.ToString();
                    notulens.nopekConceptor = Session["nopek"].ToString();
                    notulens.tglNotulen = DateTime.Parse(txTaskDate.Value);
                    notulens.CreatedBy = Session["username"].ToString();
                    notulens.status = -2;
                    notulens.fname = Session["namaFile"].ToString();
                    notulens.isDraft = true;
                    notulensDAO.AddNotulen(notulens);

                    //    logs.aktifitas = "Insert";
                    //    logs.deskripsi = "Insert Grup " + txJudulGrup.Text;
                    //    logs.CreatedBy = Session["username"].ToString();
                    //    logs.menu = "Master Grup";
                    //    logsDAO.AddLog(logs);

                    AddTaskPanel.Visible = false;
                    TaskListPanel.Visible = true;
                    clearForm();
                    bindData();

                    ListPanel.Update();

                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showSuccess", "successMessage();", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(Data Not Valid);", true);
                }
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }
        public void kirimEmail()
        {
            try
            {
                foreach (DataRow row in dtNotulenDetail.Rows)
                {
                    using (MailMessage mm = new MailMessage())
                    {
                        vNoPek = row["nopekPIC"].ToString();
                        mm.From = new MailAddress(From);
                        string[] MultiplePIC = vNoPek.Split(',');
                        foreach (string nopekDetail in MultiplePIC)
                        {
                            string nopekFinal = nopekDetail.Replace(" ", "");
                            email = notulensDAO.cekEmail(nopekFinal);
                            email = email.Replace(" ", "");
                            mm.To.Add(new MailAddress(email));
                        }

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
                        sb.Append("<p>Anda ditunjuk sebagai PIC untuk task berikut :  <b></b></p></br>");

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

                        sb.Append("<p>Silahkan submit evidence ke aplikasi <a href='http://intra-ru6.pertamina.com/IACT'> <b>IACT</b></a>. Diharapkan task tersebut closed sebelum due date </p>");
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
                AddTaskPanel.Visible = false;
                TaskListPanel.Visible = true;
                clearForm();
                bindData();
                ex.ToString();
            }
        }
        public void kirimEmailReviewer()
        {
            try
            {
                foreach (DataRow row in dtNotulenDetail.Rows)
                {
                    using (MailMessage mm = new MailMessage())
                    {
                        mm.From = new MailAddress(From);
                        string emailReviewer = notulensDAO.cekEmail(ddlReviever.SelectedValue.ToString());
                        mm.To.Add(new MailAddress(emailReviewer));

                        mm.Subject = "Notifikasi IACT";
                        email = "";

                        #region ISIEMAIL
                        string IsiEmail;
                        string vSubjek = row["subjek"].ToString();
                        string vIsiRekomendasi = row["isi"].ToString();
                        string vTglJatuhTempo = row["tglJatuhTempo"].ToString();
                        string vPotensial = row["idPotential"].ToString();
                        string vnamaPIC = row["namaPIC"].ToString();
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
                        sb.Append("<p>Anda ditunjuk sebagai Reviewer untuk task berikut :  <b></b></p></br>");

                        sb.Append("<table style='border: 1px; text-align:left'>");
                        sb.Append("<tr><th style='border: 3px; text-align:center;background-color: #b5c9d8;'><b> Subjek </b></th>");
                        sb.Append("<th style='border: 3px; text-align:center;background-color: #b5c9d8;'> <b>Rekomendasi </b> </th>");
                        sb.Append("<th style='border: 3px; text-align:center;background-color: #b5c9d8;'> <b>Due Date </b></th>");
                        sb.Append("<th style='border: 3px; text-align:center;background-color: #b5c9d8;'> <b>Risk Rating </b></th>");
                        sb.Append("<th style='border: 3px; text-align:center;background-color: #b5c9d8;'> <b>PIC </b></th></tr>");
                        sb.Append("<tr><th style='border: 3px; text-align:center;'><b>" + vSubjek + " </b></th>");
                        sb.Append("<th style='border: 3px; text-align:center;'>" + vIsiRekomendasi + " </th>");
                        sb.Append("<th style='border: 3px; text-align:center;'> " + vTglJatuhTempo + "</th>");
                        sb.Append("<th style='border: 3px; text-align:center;'> " + vPotensial + "</th>");
                        sb.Append("<th style='border: 3px; text-align:center;'> " + vnamaPIC + "</th></tr>");
                        sb.Append("</table> </br>");

                        sb.Append("<p>Silahkan review task tersebut di aplikasi <a href='http://intra-ru6.pertamina.com/IACT'> <b>IACT</b></a>. Diharapkan task tersebut closed sebelum due date </p>");
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
                AddTaskPanel.Visible = false;
                TaskListPanel.Visible = true;
                clearForm();
                bindData();
                ex.ToString();
            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            try
            {
                dtNotulenDetail = ViewState["NotulenDetail"] as DataTable;
                if (validasiData())
                {
                    Session.Add("dtNotulenDetail", dtNotulenDetail);
                    notulens.noNotulenDetail = view_lblNoNotulenDetail.Text;
                    notulens.noNotulen = view_lblNoNotulen.Text;
                    notulens.judulNotulen = txJudul.Value;
                    notulens.idRekomendasi = int.Parse(ddlRekomendasi.SelectedValue.ToString());
                    notulens.idSumber = int.Parse(ddlSumber.SelectedValue.ToString());
                    notulens.noDokumen = txNoDokumen.Value;
                    notulens.nopekReviewer = ddlReviever.SelectedValue.ToString();
                    notulens.nopekConceptor = Session["nopek"].ToString();
                    notulens.tglNotulen = DateTime.Parse(txTaskDate.Value);
                    notulens.ModifiedBy = Session["username"].ToString();
                     if (Session["namaFIle"].ToString() !="")
                    {
                        notulens.fname = Session["namaFIle"].ToString();
                    }
                    else if (lblMsg.Text != "")
                    {
                        notulens.fname = lblMsg.Text;
                    }
                    //else if (Session["namaFIle"].ToString() !="")
                    //{
                    //    notulens.fname = Session["namaFIle"].ToString();
                    //}

                    notulensDAO.EditNotulen(notulens);

                    AddTaskPanel.Visible = false;
                    TaskListPanel.Visible = true;
                    clearForm();
                    bindData();
                    //Default
                    //kirimEmail();
                    ListPanel.Update();

                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showSuccess", "successMessage();", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(Data Not Valid);", true);
                }
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindData();
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            txSeverity.Value = "0";
            txProbability.Value = "0";
            lblJudul.Text = "ADD NEW";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDraft.Visible = true;
            AddTaskPanel.Visible = true;
            btnAddRekomendasi.Visible = true;
            Session["namaFile"] = "";
            TaskListPanel.Visible = false;
            clearDetail();
            clearForm();
            ListPanel.Update();
        }
        protected void btnAddRekomendasi_ServerClick(object sender, EventArgs e)
        {

            string namaFungsi = "";
            string idFungsi = "";
            string nopek = "", nama = "", jabatan = "", idPosition = "", emailPIC="";

            try
            {
                if (validasiDetail())
                {
                    //string[] arg = new string[3];
                    //arg = e.CommandArgument.ToString().Split(';');
                    //nopek = arg[0];
                    //nama = arg[1];
                    //jabatan = arg[2];
                    dtFungsiDetail = (DataTable)ViewState["FungsiDetail"];
                    foreach (DataRow row in dtFungsiDetail.Rows)
                    {
                        if (namaFungsi.Length == 0)
                        {
                            namaFungsi = row[2].ToString();
                        }
                        else
                        {
                            namaFungsi = namaFungsi + ", " + row[2].ToString();
                        }

                        if (idFungsi.Length == 0)
                        {
                            idFungsi = row[1].ToString();
                        }
                        else
                        {
                            idFungsi = idFungsi + "," + row[1].ToString();
                        }
                        
                    }
                   
                    dtPICDetail = (DataTable)ViewState["PICDetail"];
                    foreach (DataRow row in dtPICDetail.Rows)
                    {
                        if (nama.Length == 0)
                        {
                            nama = row[2].ToString();
                        }
                        else
                        {
                            nama = nama + ", " + row[2].ToString();
                        }

                        if (nopek.Length == 0)
                        {
                            nopek = row[1].ToString();
                        }
                        else
                        {
                            nopek = nopek + "," + row[1].ToString();
                        }

                     
                    }
                    

                    dtNotulenDetail = (DataTable)ViewState["NotulenDetail"];
                    dtNotulenDetail.Rows.Add(dtNotulenDetail.Rows.Count + 1, txSubjek.Value,
                    txIsiRekomendasi.Value, nopek, nama, idFungsi, namaFungsi,
                    txTglJatuhTempo.Value, txSeverity.Value, txProbability.Value, txPotensial.Value);
                    ViewState["NotulenDetail"] = dtNotulenDetail;
                    dgAddRekomendasi.DataSource = (DataTable)ViewState["NotulenDetail"];
                    dgAddRekomendasi.DataBind();
                    clearDetail();
                    ListPanel.Update();

                }
            }
            
             catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }

        protected void btnAddFungsi_ServerClick(object sender, EventArgs e)
        {
            if (ddlFungsi.SelectedIndex.ToString() != "0")
            {
                dtFungsiDetail = (DataTable)ViewState["FungsiDetail"];
                dtFungsiDetail.Rows.Add(dtFungsiDetail.Rows.Count + 1, ddlFungsi.SelectedValue.ToString(), ddlFungsi.SelectedItem.ToString());
                ViewState["FungsiDetail"] = dtFungsiDetail;
                dgFungsi.DataSource = (DataTable)ViewState["FungsiDetail"];
                dgFungsi.DataBind();
                ddlFungsi.SelectedIndex = 0;
                if (Session["namaFile"].ToString() != "")
                {
                    lblMsg.Text = Session["namaFile"].ToString(); 
                    namaFile = Session["namaFile"].ToString();
                    
                }
                ListPanel.Update();

            }
        }

        protected void addPIC_ServerClick(object sender, EventArgs e)
        {
            if (Session["namaFile"].ToString() != "")
            {
                lblMsg.Text = "File Ready";
            }
            bindPekerja();
            ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "modalPIC", " $('#modalPIC').modal('show');", true);
            PekerjaPanel.Update();
        }
        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            Nullable<DateTime> tglSekarang = DateTime.Now;
            dueDate =DateTime.Parse(view_lblDueDate.Text);
            if (tglSekarang <= dueDate)
            {
                notulens.status = 2;
            }
            else if (tglSekarang > dueDate)
            {
                notulens.status = 4;
            }
            notulens.noNotulenDetail = view_lblNoNotulenDetail.Text;
            notulensDAO.ApproveNotulen(notulens);
            bindData();
            clearForm();
            ListPanel.Update();
        }

        protected void btnReject_ServerClick(object sender, EventArgs e)
        {
            if (txKomentar.Value != "")
            {
                
                komentars.isiKomentar = txKomentar.Value;
                komentars.CreatedBy = Session["username"].ToString();
                komentars.NoNotulenDetail = view_lblNoNotulenDetail.Text;
                komentarsDAO.AddKomentar(komentars, int.Parse(lblIdEvidence.Text));
                bindData();
                clearForm();
                ListPanel.Update();
            }
        }

        protected void ddlSeverity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int vSeverity = int.Parse(ddlSeverity.SelectedValue.ToString());
            int vProbability = int.Parse(ddlProbability.SelectedValue.ToString());
            txPotensial.Value = Convert.ToString( vSeverity * vProbability);

        }

        protected void ddlProbability_SelectedIndexChanged(object sender, EventArgs e)
        {
            int vSeverity = int.Parse(ddlSeverity.SelectedValue.ToString());
            int vProbability = int.Parse(ddlProbability.SelectedValue.ToString());
            txPotensial.Value = Convert.ToString(vSeverity * vProbability);
        }
        protected void filter_ddlRekomendasi_TextChanged(object sender, EventArgs e)
        {
            if (filter_ddlRekomendasi.SelectedIndex > 0)
            {
                dgObject.CurrentPageIndex = 0;
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
            dgObject.CurrentPageIndex = 0;
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
        protected void dgObject_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string code = "";
                if (e.Item.ItemIndex > -1)
                {
                    code = e.CommandArgument.ToString();
                    notulens.noNotulenDetail = code;
                    evidences.noNotulenDetail = code;
                }

                if (e.CommandName.ToString() == "linkNotulen")
                {
                    clearForm();
                    if (notulensDAO.GetNotulenDetail(ref notulens))
                    {
                        if (evidencesDAO.GetEvidenceDetailByNoNotulen(ref evidences))
                        {
                            txKeteranganEvidence.Text = evidences.Keterangan;
                            linkEvidence.Text = evidences.fname;
                            fileEvidence = evidences.fname;
                            lblIdEvidence.Text = evidences.idEvidence.ToString();
                        }

                        if (notulens.status.ToString() == "1")
                        {
                            txKomentar.Visible = true;
                            btnReject.Visible = true;
                            btnApprove.Visible = true;
                        }
                        else
                        {
                            btnReject.Visible = false;
                            txKomentar.Visible = false;
                            btnApprove.Visible = false;
                        }
                        view_lblNoNotulenDetail.Text = code;
                        view_lblRekomendasi.Text = notulens.namaRekomendasi;
                        view_lblSumber.Text = notulens.namaSumber;
                        view_lblJudulNotulen.Text = notulens.judulNotulen;
                        view_lblNoDokumen.Text = notulens.noDokumen;
                        view_lblLampiran.Text = notulens.fname;
                        namaFile = notulens.fname;
                        view_lblReviewer.Text = notulens.namaReviewer;
                        DateTime vTaskDate =Convert.ToDateTime(notulens.tglNotulen);
                        view_lbltglNotulen.Text = vTaskDate.ToString("dd/MM/yyyy");
                        view_lblNoNotulen.Text = notulens.noNotulen;
                        view_lblDueDate.Text = notulens.tglJatuhTempo.ToString();
                        dgViewDetail.DataSource = notulensDAO.SearchNotulen(code);
                        dgViewDetail.DataBind();
                        dgComment.DataSource = komentarsDAO.SearchKomentar(code);
                        dgComment.DataBind();

                        ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showModal", " $('#modalView').modal('show');", true);
                        viewPanel.Update();
                    }
                }
                else if (e.CommandName.ToString() == "Edit")
                {
                    clearForm();

                    if (notulensDAO.GetNotulenDetail(ref notulens))
                    {
                        Session["namaFile"] = "";
                        lblJudul.Text = "EDIT ";
                        
                        view_lblNoNotulenDetail.Text = code;
                        view_lblNoNotulen.Text = notulens.noNotulen;
                        if (notulens.idRekomendasi != null)
                        {
                            ddlRekomendasi.SelectedValue = ddlRekomendasi.Items.FindByValue("" + notulens.idRekomendasi + "").Value;
                        }
                        else
                        {
                            ddlRekomendasi.SelectedIndex = 0;
                        }
                        if (notulens.idRekomendasi != null)
                        {
                            sumbers.idRekomendasi = notulens.idRekomendasi;
                            ddlSumber.DataSource = sumbersDAO.SearchSumberbyIdRekomendasi(sumbers);
                            ddlSumber.DataTextField = "NamaSumber";
                            ddlSumber.DataValueField = "idSumber";
                            ddlSumber.DataBind();
                            ddlSumber.Items.Insert(0, "-- Select Source --");
                        }
                        if (notulens.idSumber != null)
                        {
                            ddlSumber.SelectedValue = ddlSumber.Items.FindByValue("" + notulens.idSumber + "").Value;
                        }
                        else
                        {
                            ddlSumber.SelectedIndex = 0;
                        }
                        txJudul.Value = notulens.judulNotulen;
                        txNoDokumen.Value = notulens.noDokumen;
                        lblMsg.Text = notulens.fname;
                        //ddlReviever.SelectedValue = ddlReviever.Items.FindByValue(notulens.nopekReviewer).Value;
                        if (notulens.nopekReviewer != null)
                        {
                            //var item = ddlReviever.Items.FindByValue("" + notulens.nopekReviewer + "").Value;
                            //if (item != null)
                            //{
                            //    ddlReviever.SelectedValue = ddlReviever.Items.FindByValue("" + notulens.nopekReviewer + "").Value;
                            //}else
                            //{
                            //    ddlReviever.SelectedIndex = 0;
                            //}
                            //ddlSumber.SelectedValue = ddlSumber.Items.FindByValue("" + notulens.idSumber + "").Value;
                            ddlReviever.SelectedIndex = ddlReviever.Items.IndexOf(ddlReviever.Items.FindByValue(notulens.nopekReviewer));
                        }
                        else
                        {
                            ddlReviever.SelectedIndex = 0;
                        }
                        DateTime vTaskDate = Convert.ToDateTime(notulens.tglNotulen);
                        txTaskDate.Value = vTaskDate.ToString("dd/MM/yyyy");
                       
                        dgAddRekomendasi.DataSource = notulensDAO.SearchNotulen(code);
                        dgAddRekomendasi.DataBind();
                        dgComment.DataSource = komentarsDAO.SearchKomentar(code);
                        dgComment.DataBind();

                        ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showModal", " $('#modalAdd').modal('show');", true);
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        btnAddRekomendasi.Visible = true;
                        btnDraft.Visible = false;
                        AddTaskPanel.Visible = true;
                        TaskListPanel.Visible = false;
                        viewPanel.Update();
                    }
                }
                else if (e.CommandName.ToString() == "Delete")
                {
                    notulens.noNotulenDetail = code;
                    notulens.ModifiedBy = Session["username"].ToString();
                    notulensDAO.DeleteNotulen(notulens);

                    //logs.aktifitas = "Delete";
                    //logs.deskripsi = "Delete Grup " + txJudulGrup.Text;
                    //logs.CreatedBy = Session["username"].ToString();
                    //logs.menu = "Delete Grup";
                    //logsDAO.AddLog(logs);

                    if ((dgObject.Items.Count % dgObject.PageSize) == 1 & dgObject.CurrentPageIndex != 0)
                        dgObject.CurrentPageIndex = dgObject.PageCount - 2;

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
        protected void dgObject_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                Label lblnamaStatus = (Label)e.Item.FindControl("lblnamaStatus");
                Label lblCreatedBy = (Label)e.Item.FindControl("lblCreatedBy");
                LinkButton BtnEditData = (LinkButton)e.Item.FindControl("BtnEditData");
                LinkButton BtnDelete = (LinkButton)e.Item.FindControl("BtnDelete");

                if (Session["idrole"].ToString() == "1")
                {
                    BtnEditData.Visible = true;
                    BtnDelete.Visible = true;
                }
                else if (Session["idrole"].ToString() == "1" && int.Parse(lblStatus.Text) != 1 && int.Parse(lblStatus.Text) != 2 && int.Parse(lblStatus.Text) != 4 && lblCreatedBy.Text == Session["username"].ToString())
                {
                    BtnEditData.Visible = true;
                    BtnDelete.Visible = true;
                }
                else
                {
                    BtnEditData.Visible = false;
                    BtnDelete.Visible = false;
                }
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

        protected void dgAddRekomendasi_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string idDetail = Convert.ToString(e.CommandArgument);
                dtNotulenDetail = (DataTable)ViewState["NotulenDetail"];
                for (int i = dtNotulenDetail.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtNotulenDetail.Rows[i];
                    if (dr["NoNotulenDetail"].ToString() == idDetail)
                    {
                        dr.Delete();
                        dtNotulenDetail.AcceptChanges();
                    }
                }
                ViewState["NotulenDetail"] = dtNotulenDetail;
                dgAddRekomendasi.DataSource = (DataTable)ViewState["NotulenDetail"];
                dgAddRekomendasi.DataBind();
                ListPanel.Update();
            }
        }

        protected void dgAddRekomendasi_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgAddRekomendasi.CurrentPageIndex = e.NewPageIndex;
            dgAddRekomendasi.DataSource = (DataTable)ViewState["NotulenDetail"];
            dgAddRekomendasi.DataBind();
            ListPanel.Update();
        }

        protected void dgFungsi_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string idDetail = Convert.ToString(e.CommandArgument);
                dtFungsiDetail = (DataTable)ViewState["FungsiDetail"];
                for (int i = dtFungsiDetail.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtFungsiDetail.Rows[i];
                    if (dr["idDetail"].ToString() == idDetail)
                    {
                        dr.Delete();
                        dtFungsiDetail.AcceptChanges();
                    }
                }
                ViewState["FungsiDetail"] = dtFungsiDetail;
                dgFungsi.DataSource = (DataTable)ViewState["FungsiDetail"];
                dgFungsi.DataBind();
                ListPanel.Update();
            }
        }


        protected void dgPIC_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string idDetail = Convert.ToString(e.CommandArgument);
                dtPICDetail = (DataTable)ViewState["PICDetail"];
                for (int i = dtPICDetail.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtPICDetail.Rows[i];
                    if (dr["idDetail"].ToString() == idDetail)
                    {
                        dr.Delete();
                        dtPICDetail.AcceptChanges();
                    }
                }
                ViewState["PICDetail"] = dtPICDetail;
                dgPIC.DataSource = (DataTable)ViewState["PICDetail"];
                dgPIC.DataBind();
                ListPanel.Update();
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
                string nopek = "";
                string nama = "", jabatan = "", idPosition = "", emailPIC="";
                if (e.Item.ItemIndex > -1)
                {
                    string[] arg = new string[3];
                    arg = e.CommandArgument.ToString().Split(';');
                    nopek = arg[0];
                    nama = arg[1];
                    jabatan = arg[2];
                    emailPIC = arg[3];
                }

                if (e.CommandName.ToString() == "Select")
                {
                   
                    dtPICDetail = (DataTable)ViewState["PICDetail"];
                    dtPICDetail.Rows.Add(dtPICDetail.Rows.Count + 1, nopek, nama, jabatan,"0", emailPIC);
                    ViewState["PICDetail"] = dtPICDetail;
                    dgPIC.DataSource = (DataTable)ViewState["PICDetail"];
                    dgPIC.DataBind();
                    ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "hideModalPekerja", " $('#modalPIC').modal('hide');", true);
                    ListPanel.Update();
                }

            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }

        #endregion

        #region Function


        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            clearForm();
            AddTaskPanel.Visible = false;
            TaskListPanel.Visible = true;
            ListPanel.Update();
        }

        protected bool validasiData()
        {
            int errCount=0;
            if (ddlRekomendasi.SelectedIndex == 0)
            {
                errCount++;
            }

            if (txJudul.Value == "")
            {
                errCount++;
            }
            if (txNoDokumen.Value == "") {
                errCount++;
            }

            if (errCount > 0) {
                 return false;
            }
            else
            {
                return true;
            }
        }
        protected bool validasiDetail()
        {
            int errCount = 0;
            
            
            if (txSubjek.Value == "")
            {
                errCount++;
            }
            if (txTglJatuhTempo.Value == "")
            {
                errCount++;
            }
           
            if (txIsiRekomendasi.Value == "")
            {
                errCount++;
            }
            if (errCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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
                notulens.idRekomendasi =int.Parse(filter_ddlRekomendasi.SelectedValue.ToString());
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
                    ds = notulensDAO.ListNotulenALL(Session["username"].ToString(), notulens,vTglAwal, vTglAkhir,null);
                }
                else
                {
                    ds = notulensDAO.ListNotulenBySubmitter(Session["username"].ToString(), notulens, vTglAwal, vTglAkhir);
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
                    ds = notulensDAO.SearchNotulenByALL(txSearch.Text, Session["username"].ToString(), notulens, vTglAwal, vTglAkhir);
                }
                else
                {
                    ds = notulensDAO.SearchNotulenBySubmitter(txSearch.Text, Session["username"].ToString(), notulens, vTglAwal, vTglAkhir);
            
                }
                dv = ds.Tables[0].DefaultView;
                dv.Sort = (String)ViewState["SortExpression"];
                if (ViewState["SortAscending"] == "no")
                    dv.Sort += " DESC";

                // Rebind data 
                dgObject.DataSource = dv;

                //if (dgObject.CurrentPageIndex >= 0)
                //{
                //    dgObject.CurrentPageIndex = 0;
                //}
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
            ddlRekomendasi.DataSource = rekomendasisDAO.ListRekomendasi();
            ddlRekomendasi.DataTextField = "NamaRekomendasi";
            ddlRekomendasi.DataValueField = "IdRekomendasi";
            ddlRekomendasi.DataBind();
            ddlRekomendasi.Items.Insert(0, "-- Select Recommendation --");

            ddlReviever.DataSource = pekerjasDAO.ListReviewer();
            ddlReviever.DataTextField = "nama";
            ddlReviever.DataValueField = "nopek";
            ddlReviever.DataBind();
            ddlReviever.Items.Insert(0, "-- Select Reviewer --");

            ddlFungsi.DataSource = pekerjasDAO.ListFungsi();
            ddlFungsi.DataTextField = "Fungsi";
            ddlFungsi.DataValueField = "noFungsi";
            ddlFungsi.DataBind();
            ddlFungsi.Items.Insert(0, "-- Select Fungsi --");

            filter_ddlRekomendasi.DataSource = rekomendasisDAO.ListRekomendasi();
            filter_ddlRekomendasi.DataTextField = "NamaRekomendasi";
            filter_ddlRekomendasi.DataValueField = "IdRekomendasi";
            filter_ddlRekomendasi.DataBind();
            filter_ddlRekomendasi.Items.Insert(0, "-- Select Recommendation --");

        }

        protected void ddlRekomendasi_TextChanged(object sender, EventArgs e)
        {
            if (ddlRekomendasi.SelectedIndex > 0)
            {
                sumbers.idRekomendasi =int.Parse(ddlRekomendasi.SelectedValue.ToString());
                ddlSumber.DataSource = sumbersDAO.SearchSumberbyIdRekomendasi(sumbers);
                ddlSumber.DataTextField = "NamaSumber";
                ddlSumber.DataValueField = "idSumber";
                ddlSumber.DataBind();
                ddlSumber.Items.Insert(0, "-- Select Source --");
            }
        }
        protected void clearForm()
        {
            txId.Value = "";
            txJudul.Value = "";
            txNoDokumen.Value = "";
            ddlRekomendasi.SelectedIndex = 0;
            linkEvidence.Text = "";
            txKeteranganEvidence.Text = "";
            txKomentar.Value = "";
        }
        protected void clearDetail()
        {
            txSubjek.Value = "";
            txIsiRekomendasi.Value = "";
            txTglJatuhTempo.Value = "";
            ddlFungsi.SelectedIndex = 0;
            ddlProbability.SelectedIndex = 0;
            ddlSeverity.SelectedIndex = 0;
            txPotensial.Value = "";
          
            ddlFungsi.SelectedIndex = 0;

           
            dtPICDetail = (DataTable)ViewState["PICDetail"];
            for (int i = dtPICDetail.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtPICDetail.Rows[i];
                dr.Delete();
                dtPICDetail.AcceptChanges();
                
            }
            ViewState["PICDetail"] = dtPICDetail;
            dgPIC.DataSource = (DataTable)ViewState["PICDetail"];
            dgPIC.DataBind();

            dtFungsiDetail = (DataTable)ViewState["FungsiDetail"];
            for (int i = dtFungsiDetail.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtFungsiDetail.Rows[i];
                dr.Delete();
                dtFungsiDetail.AcceptChanges();

            }
            ViewState["FungsiDetail"] = dtFungsiDetail;
            dgFungsi.DataSource = (DataTable)ViewState["FungsiDetail"];
            dgFungsi.DataBind();
            ListPanel.Update();

        }

        protected void GetCounter(string tipe)
        {
            if (tipe == "Head")
            {
                string vTanggalSekarang = txTaskDate.Value; ;
                string vTahunBulan = DateTime.ParseExact(vTanggalSekarang, "dd/MM/yyyy", null).ToString("yyMM");
                vKode1 = rekomendasisDAO.SearchSingkatanRekomendasi(ddlRekomendasi.SelectedValue.ToString());
                vKode2 = sumbersDAO.SearchSingkatanSumber(ddlSumber.SelectedValue.ToString());
                vKode3 = vTahunBulan;

                counters.Kode1 = vKode1;
                counters.Kode2 = vKode2;
                counters.Kode3 = vKode3;
                counters.Kode4 = vKode4;
                counters.Kode5 = vKode5;
                counters.Kode6 = vKode6;
                if (countersDAO.GetCounter(ref counters))
                {
                    no = counters.no + 1;
                    if (no < 10)
                    {
                        vNoFix = "0" + no.ToString();
                    }
                    else
                    {
                        vNoFix = no.ToString();
                    }
                    counters.no = int.Parse(vNoFix);
                    countersDAO.EditCounter(counters);
                    vKodeFix = vKode1 + "/" + vKode2 + "/" + vTahunBulan + "/" + vNoFix;
                }
                else
                {
                    no = 1;
                    counters.no = no;
                    vNoFix = "0" + no.ToString();
                    countersDAO.AddCounter(counters);
                    vKodeFix = vKode1 + "/" + vKode2 + "/" + vTahunBulan + "/" + vNoFix;
                  
                }

            }
            else
            {
                vKode1 = vKodeFix;

                counters.Kode1 = vKode1;
                counters.Kode2 = vKode2;
                counters.Kode3 = vKode3;
                counters.Kode4 = vKode4;
                counters.Kode5 = vKode5;
                counters.Kode6 = vKode6;
                if (countersDAO.GetCounter(ref counters))
                {
                    no = counters.no + 1;

                    if (no < 10)
                    {
                        vNoFix = "0" + no.ToString();
                    }
                    else
                    {
                        vNoFix = no.ToString();
                    }
                    counters.no = int.Parse(vNoFix);
                    countersDAO.EditCounter(counters);
                    vKodeFix = vKode1 + "-"  + vNoFix;
                }
                else
                {
                    no = 1;
                    counters.no = no;
                    vNoFix = "0" + no.ToString();
                    countersDAO.AddCounter(counters);
                    vKodeFix = vKode1 + "-" + vNoFix;
                   
                }
            }
       }
        protected void bindPekerja()
        {
            string idFungsi = "";
            foreach (DataRow row in dtFungsiDetail.Rows)
            {
                if (idFungsi.Length == 0)
                {
                    idFungsi = row[1].ToString();
                }
                else
                {
                    idFungsi = idFungsi + ", " + row[1].ToString();
                }
            }

            if (txSearchPekerja.Text != "")
            {
                   try
                    {
                        pekerjas.namapegawai = txSearchPekerja.Text;
                        pekerjas.NoPek = txSearchPekerja.Text;
                        pekerjas.jabatan = txSearchPekerja.Text;
                        
                        pekerjas.NoLevel = 5;
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
                        pekerjas.nobagian = 1; 
                        ds = pekerjasDAO.ListPekerjaAtasan();
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
        #endregion


        protected void txSearchPekerja_TextChanged(object sender, EventArgs e)
        {
            bindPekerja();

        }

        protected void fileUploadComplete(object sender, AsyncFileUploadEventArgs e)
        {
            vKode2 = sumbersDAO.SearchSingkatanSumber(ddlSumber.SelectedValue.ToString());
            if (fileUpload1.HasFile)
            {
                string jenis = Path.GetExtension(fileUpload1.FileName);
                string finalName = vKode2 + "-" + fileUpload1.FileName;
                finalName = finalName.Replace(" ", "");
                finalName = finalName.Replace("/", "_");
                fileUpload1.PostedFile.SaveAs(Server.MapPath("~/Doc/") + finalName);
                lblMsg.Text = finalName;
                vNamaFile = finalName;
                Session["namaFile"] = finalName;
                ListPanel.Update();
            }
        }

        protected void ddlSumber_TextChanged(object sender, EventArgs e)
        {
            bool useRAM = false;
            if (ddlSumber.SelectedIndex > 0)
            {
                sumbers.idSumber = int.Parse(ddlSumber.SelectedValue.ToString());
                useRAM = sumbersDAO.cekRAM(sumbers);
            }

            if (useRAM)
            {
                RAM.Visible = true;
            }
            else
            {
                RAM.Visible = false;
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            dgObject.CurrentPageIndex = 0;
            bindData();
            ListPanel.Update();
            
        }

       
   

        
      





     


       

    }
}