using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;
using I_ACT.Domain;

namespace I_ACT.DAO
{
    public class NotulenDAO : Notulen
    {
        #region Parameters Declaration

        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @search = new SqlParameter("@search", SqlDbType.VarChar, 50);
        protected SqlParameter @idNotulen = new SqlParameter("@idNotulen", SqlDbType.Int);
        protected SqlParameter @status = new SqlParameter("@status", SqlDbType.Int);
        protected SqlParameter @prioritas = new SqlParameter("@prioritas", SqlDbType.Int);
        protected SqlParameter @idfungsi = new SqlParameter("@idfungsi", SqlDbType.VarChar);
        protected SqlParameter @idSeverity = new SqlParameter("@idSeverity", SqlDbType.Int);
        protected SqlParameter @idProbability = new SqlParameter("@idProbability", SqlDbType.Int);
        protected SqlParameter @idPotensial = new SqlParameter("@idPotensial", SqlDbType.Int);
        protected SqlParameter @idRekomendasi = new SqlParameter("@idRekomendasi", SqlDbType.Int);
        protected SqlParameter @idSumber = new SqlParameter("@idSumber", SqlDbType.Int);
        protected SqlParameter @namaNotulen = new SqlParameter("@namaNotulen", SqlDbType.VarChar, 50);
        protected SqlParameter @singkatanNotulen = new SqlParameter("@singkatanNotulen", SqlDbType.VarChar, 50);
        protected SqlParameter @noNotulen = new SqlParameter("@noNotulen", SqlDbType.VarChar, 50);
        protected SqlParameter @noNotulenDetail = new SqlParameter("@noNotulenDetail", SqlDbType.VarChar, 50);
        protected SqlParameter @nopekConceptor = new SqlParameter("@nopekConceptor", SqlDbType.VarChar, 50);
        protected SqlParameter @judulNotulen = new SqlParameter("@judulNotulen", SqlDbType.VarChar, 1000);
        protected SqlParameter @noDokumen = new SqlParameter("@noDokumen", SqlDbType.VarChar, 100);
        protected SqlParameter @fname = new SqlParameter("@fname", SqlDbType.VarChar, 100);
        protected SqlParameter @subjek = new SqlParameter("@subjek", SqlDbType.VarChar, 2000);
        protected SqlParameter @isi = new SqlParameter("@isi", SqlDbType.VarChar, 2000);
        protected SqlParameter @noteDelegasi = new SqlParameter("@noteDelegasi", SqlDbType.VarChar, 200);

        protected SqlParameter @namaFungsi = new SqlParameter("@namaFungsi", SqlDbType.VarChar, 200);

        protected SqlParameter @nopekPIC = new SqlParameter("@nopekPIC", SqlDbType.VarChar, 50);
        protected SqlParameter @namaPIC = new SqlParameter("@namaPIC", SqlDbType.VarChar, 1000);
        protected SqlParameter @nopekDelegasi = new SqlParameter("@nopekDelegasi", SqlDbType.VarChar, 50);
        protected SqlParameter @namaDelegasi = new SqlParameter("@namaDelegasi", SqlDbType.VarChar, 50);
        protected SqlParameter @nopekReviewer = new SqlParameter("@nopekReviewer", SqlDbType.VarChar, 50);
        protected SqlParameter @namaStatus = new SqlParameter("@namaStatus", SqlDbType.VarChar, 50);
        protected SqlParameter @tglJatuhTempo = new SqlParameter("@tglJatuhTempo", SqlDbType.DateTime);
        protected SqlParameter @tglClosed = new SqlParameter("@tglClosed", SqlDbType.DateTime);
        protected SqlParameter @tglDelegasi = new SqlParameter("@tglDelegasi", SqlDbType.DateTime);
        protected SqlParameter @tglNotulen = new SqlParameter("@tglNotulen", SqlDbType.DateTime);
        protected SqlParameter @tglAwal = new SqlParameter("@tglAwal", SqlDbType.DateTime);
        protected SqlParameter @tglAkhir = new SqlParameter("@tglAkhir", SqlDbType.DateTime);
        //protected SqlParameter @status = new SqlParameter("@status", SqlDbType.Int);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);
        protected SqlParameter @isDraft = new SqlParameter("@isDraft", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet SearchNotulen(string keySearch)
        {
            @function.Value = 14444;
            if (keySearch != null)
            {
                @search.Value = keySearch;
            }
            else
            {
                @search.Value = DBNull.Value;
            }
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @search);
        }
        public DataSet SearchNotulenDetail(string keySearch)
        {
            @function.Value = 1444411;
            if (keySearch != null)
            {
                @search.Value = keySearch;
            }
            else
            {
                @search.Value = DBNull.Value;
            }
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @search);
        }
        public DataSet SearchNotulenDetailAll(string keySearch)
        {
            @function.Value = 1444400;
            if (keySearch != null)
            {
                @search.Value = keySearch;
            }
            else
            {
                @search.Value = DBNull.Value;
            }
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @search);
        }
        public DataSet ListNotulenALL(string userSubmitter, Notulen notulens,string vTglAwal, string vTglAkhir , string keysearch)
        {
            @function.Value = 13;
            @idRekomendasi.Value=notulens.idRekomendasi;
            @idSumber.Value=notulens.idSumber;
            @tglAwal.Value=vTglAwal;
            @tglAkhir.Value=vTglAkhir;
            @status.Value=notulens.status;
            @isDraft.Value = notulens.isDraft;
            @search.Value = keysearch;
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @CreatedBy,
                @status, @isDraft, @idRekomendasi, @idSumber, @tglAwal, @tglAkhir, @search);
        }

        

        public DataSet ListNotulenBySubmitter(string userSubmitter, Notulen notulens, string vTglAkhir, string vTglAwal)
        {
            @function.Value = 1333;
            @CreatedBy.Value = userSubmitter;
            @idRekomendasi.Value = notulens.idRekomendasi;
            @idSumber.Value = notulens.idSumber;
            @tglAwal.Value = vTglAwal;
            @tglAkhir.Value = vTglAkhir;
            @status.Value = notulens.status;
            @isDraft.Value = notulens.isDraft;
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @CreatedBy,
                @status, @idRekomendasi, @idSumber, @tglAwal, @tglAkhir, @isDraft);
        }
        public DataSet SearchNotulenByALL(string keySearch, string userSubmitter, Notulen notulens, string vTglAkhir, string vTglAwal)
        {
            @function.Value = 14;
            if (keySearch != null)
            {
                @search.Value = keySearch;
            }
            else
            {
                @search.Value = DBNull.Value;
            }
            @idRekomendasi.Value = notulens.idRekomendasi;
            @idSumber.Value = notulens.idSumber;
            @tglAwal.Value = vTglAwal;
            @tglAkhir.Value = vTglAkhir;
            @status.Value = notulens.status;

            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @search,
                @status, @idRekomendasi, @idSumber, @tglAwal, @tglAkhir);
        }

        public DataSet SearchNotulenBySubmitter(string keySearch, string userSubmitter, Notulen notulens, string vTglAkhir, string vTglAwal)
        {
            @function.Value = 1444;
            if (keySearch != null)
            {
                @search.Value = keySearch;
            }
            else
            {
                @search.Value = DBNull.Value;
            }
            @CreatedBy.Value = userSubmitter;
            @idRekomendasi.Value = notulens.idRekomendasi;
            @idSumber.Value = notulens.idSumber;
            @tglAwal.Value = vTglAwal;
            @tglAkhir.Value = vTglAkhir;
            @status.Value = notulens.status;
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @search, @CreatedBy,
                @status, @idRekomendasi, @idSumber, @tglAwal, @tglAkhir);
        }

        public DataSet ListNotulenByPIC(string PIC)
        {
            @function.Value = 13333;
            @nopekPIC.Value = PIC;
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @nopekPIC);
        }
        public DataSet SearchNotulenByPIC(string keySearch, string PIC)
        {
            @function.Value = 14444;
            if (keySearch != null)
            {
                @search.Value = keySearch;
            }
            else
            {
                @search.Value = DBNull.Value;
            }
            @nopekPIC.Value = PIC;
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @search, @nopekPIC);
        }

      
        public void AddNotulen(Notulen notulens)
        {

            DataTable dtNotulenDetail = System.Web.HttpContext.Current.Session["dtNotulenDetail"] as DataTable;
            SqlConnection conn = null;
            SqlTransaction transact = null;
            int noDetail = 0;
            string noHead = notulens.noNotulen;
            string vNoFix = "", vKodeFix="";
            try
            {
                conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IACTconstring"].ToString());
                conn.Open();
                transact = conn.BeginTransaction();

                //Add Plan_Perencanaan
                @function.Value = 15;
                @noNotulen.Value = notulens.noNotulen;
                @idRekomendasi.Value = notulens.idRekomendasi;
                @idSumber.Value = notulens.idSumber;
                @nopekConceptor.Value = notulens.nopekConceptor;
                @judulNotulen.Value = notulens.judulNotulen;
                @nopekReviewer.Value = notulens.nopekReviewer;
                @noDokumen.Value = notulens.noDokumen;
                @fname.Value = notulens.fname;
                @tglNotulen.Value = notulens.tglNotulen;
                @CreatedBy.Value = notulens.CreatedBy;
                @isDraft.Value = notulens.isDraft;
                int result = SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_BASE", @function,
                 @noNotulen,@isDraft, @idRekomendasi, @idSumber,@tglNotulen, @nopekConceptor, @judulNotulen, @nopekReviewer, @noDokumen, @fname, @CreatedBy);

                foreach (DataRow row in dtNotulenDetail.Rows)
                {
                    noDetail += 1;
                    if (noDetail < 10)
                    {
                        vNoFix = "0" + noDetail.ToString();
                    }
                    else
                    {
                        vNoFix = noDetail.ToString();
                    }
                    vKodeFix = noHead + "-" + vNoFix;
                    @function.Value = 21;
                    @noNotulen.Value = notulens.noNotulen;
                    @noNotulenDetail.Value = vKodeFix;
                    @subjek.Value = row[1].ToString();
                    @isi.Value = row[2].ToString();
                    @nopekPIC.Value = row[3].ToString();
                    @idfungsi.Value = row[5].ToString();
                    @tglJatuhTempo.Value = row[7].ToString();
                    @idSeverity.Value =int.Parse(row[8].ToString());
                    @idProbability.Value =int.Parse(row[9].ToString());
                    @status.Value = notulens.status;
                    @CreatedBy.Value = notulens.CreatedBy;
                    SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_BASE", @function,
                    @noNotulen, @noNotulenDetail, @subjek, @isi, @nopekPIC,@idfungsi, @prioritas, @tglJatuhTempo,@status,@idProbability,@idSeverity, @CreatedBy);
                }
                transact.Commit();
            }
            catch (Exception ex)
            {
                if (transact != null)
                {
                    transact.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public void EditNotulenDraft(Notulen notulens)
        {

            DataTable dtNotulenDetail = System.Web.HttpContext.Current.Session["dtNotulenDetail"] as DataTable;
            SqlConnection conn = null;
            SqlTransaction transact = null;
            int noDetail = 0;
            string noHead = notulens.noNotulen;
            string vNoFix = "", vKodeFix = "";
            try
            {
                conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IACTconstring"].ToString());
                conn.Open();
                transact = conn.BeginTransaction();

                //Add Plan_Perencanaan
                @function.Value = 16;

                @noNotulen.Value = notulens.noNotulen;
                @idRekomendasi.Value = notulens.idRekomendasi;
                @idSumber.Value = notulens.idSumber;
                @nopekConceptor.Value = notulens.nopekConceptor;
                @judulNotulen.Value = notulens.judulNotulen;
                @nopekReviewer.Value = notulens.nopekReviewer;
                @noDokumen.Value = notulens.noDokumen;
                @fname.Value = notulens.fname;
                @tglNotulen.Value = notulens.tglNotulen;
                @ModifiedBy.Value = notulens.ModifiedBy;
                @CreatedBy.Value = notulens.CreatedBy;
                @isDraft.Value = notulens.isDraft;
                int result = SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_BASE", @function,
                 @noNotulen,@CreatedBy, @isDraft, @idRekomendasi, @idSumber, @tglNotulen, @nopekConceptor, @judulNotulen, @nopekReviewer, @noDokumen, @fname, @ModifiedBy);

                 @function.Value = 21100;
                 int result2 = SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_BASE", @function, @noNotulen);
             
                foreach (DataRow row in dtNotulenDetail.Rows)
                {
                    noDetail += 1;
                    if (noDetail < 10)
                    {
                        vNoFix = "0" + noDetail.ToString();
                    }
                    else
                    {
                        vNoFix = noDetail.ToString();
                    }
                    vKodeFix = noHead + "-" + vNoFix;
                    @function.Value = 211;
                    @noNotulen.Value = notulens.noNotulen;
                    @noNotulenDetail.Value = vKodeFix;

                    //@subjek.Value = row[2].ToString();
                    //@isi.Value = row[3].ToString();
                    //@nopekPIC.Value = row[4].ToString();
                    //@idfungsi.Value = row[6].ToString();
                    //@tglJatuhTempo.Value = row[8].ToString();
                    //@idSeverity.Value = int.Parse(row[9].ToString());
                    //@idProbability.Value = int.Parse(row[10].ToString());

                    @subjek.Value = row[2].ToString();
                    @isi.Value = row[3].ToString();
                    @nopekPIC.Value = row[4].ToString();
                    @idfungsi.Value = row[6].ToString();
                    @tglJatuhTempo.Value = row[8].ToString();
                    @idSeverity.Value = int.Parse(row[9].ToString());
                    @idProbability.Value = int.Parse(row[10].ToString());

                    @status.Value = notulens.status;
                    @CreatedBy.Value = notulens.CreatedBy;
                    SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_BASE", @function,
                    @noNotulen, @noNotulenDetail, @subjek, @isi, @nopekPIC, @idfungsi, @prioritas, @tglJatuhTempo, @status, @idProbability, @idSeverity, @CreatedBy);
                }
                transact.Commit();
            }
            catch (Exception ex)
            {
                if (transact != null)
                {
                    transact.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }
        public void EditNotulen(Notulen notulens)
        {

            DataTable dtNotulenDetail = System.Web.HttpContext.Current.Session["dtNotulenDetail"] as DataTable;
            SqlConnection conn = null;
            SqlTransaction transact = null;
            int noDetail = 0;
            string noHead = notulens.noNotulen;
            string vNoFix = "", vKodeFix = "";
            try
            {
                conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["IACTconstring"].ToString());
                conn.Open();
                transact = conn.BeginTransaction();

                //Add Plan_Perencanaan
                @function.Value = 16;
                
                @noNotulen.Value = notulens.noNotulen;
                @idRekomendasi.Value = notulens.idRekomendasi;
                @idSumber.Value = notulens.idSumber;
                @nopekConceptor.Value = notulens.nopekConceptor;
                @judulNotulen.Value = notulens.judulNotulen;
                @nopekReviewer.Value = notulens.nopekReviewer;
                @noDokumen.Value = notulens.noDokumen;
                @fname.Value = notulens.fname;
                @tglNotulen.Value = notulens.tglNotulen;
                @ModifiedBy.Value = notulens.ModifiedBy;
                @isDraft.Value = notulens.isDraft;
                int result = SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_BASE", @function,
                 @noNotulen,@isDraft, @idRekomendasi, @idSumber, @tglNotulen, @nopekConceptor, @judulNotulen, @nopekReviewer, @noDokumen, @fname, @ModifiedBy);

                foreach (DataRow row in dtNotulenDetail.Rows)
                {
                    noDetail += 1;
                    if (noDetail < 10)
                    {
                        vNoFix = "0" + noDetail.ToString();
                    }
                    else
                    {
                        vNoFix = noDetail.ToString();
                    }
                    vKodeFix = noHead + "-" + vNoFix;
                    @function.Value = 2111;
                    @noNotulen.Value = notulens.noNotulen;
                    @noNotulenDetail.Value = notulens.noNotulenDetail;
                    @subjek.Value = row[1].ToString();
                    @isi.Value = row[2].ToString();
                    @nopekPIC.Value = row[3].ToString();
                    @idfungsi.Value = row[5].ToString();
                    @tglJatuhTempo.Value = row[7].ToString();
                    @idSeverity.Value = int.Parse(row[8].ToString());
                    @idProbability.Value = int.Parse(row[9].ToString());
                    @status.Value = notulens.status;
                    @ModifiedBy.Value = notulens.ModifiedBy;
                    SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_BASE", @function,
                    @noNotulen, @noNotulenDetail, @subjek, @isi, @nopekPIC, @idfungsi, @prioritas, @tglJatuhTempo, @status, @idProbability, @idSeverity, @ModifiedBy);
                }
                transact.Commit();
            }
            catch (Exception ex)
            {
                if (transact != null)
                {
                    transact.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public bool GetNotulenDetail(ref Notulen Notulens)
        {
            SqlDataReader item_dr;
            @function.Value = 17;
            @noNotulenDetail.Value = Notulens.noNotulenDetail;

            item_dr = SqlHelper.ExecuteReader(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @noNotulenDetail);

            if (item_dr.HasRows)
            {
                item_dr.Read();
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("namaRekomendasi"))))
                {
                    Notulens.namaRekomendasi = item_dr["namaRekomendasi"].ToString();
                } 
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idRekomendasi"))))
                {
                    Notulens.idRekomendasi =int.Parse(item_dr["idRekomendasi"].ToString());
                } 
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idSumber"))))
                {
                    Notulens.idSumber = int.Parse(item_dr["idSumber"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NoNotulen"))))
                {
                    Notulens.noNotulen = item_dr["NoNotulen"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NoNotulenDetail"))))
                {
                    Notulens.noNotulenDetail= item_dr["NoNotulenDetail"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("namaSumber"))))
                {
                    Notulens.namaSumber = item_dr["namaSumber"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("namaPIC"))))
                {
                    Notulens.namaPIC = item_dr["namaPIC"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("judulNotulen"))))
                {
                    Notulens.judulNotulen = item_dr["judulNotulen"].ToString();
                }

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("noDokumen"))))
                {
                    Notulens.noDokumen = item_dr["noDokumen"].ToString();
                }

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("namaReviewer"))))
                {
                    Notulens.namaReviewer = item_dr["namaReviewer"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("nopekReviewer"))))
                {
                    Notulens.nopekReviewer = item_dr["nopekReviewer"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("fname"))))
                {
                    Notulens.fname = item_dr["fname"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("status"))))
                {
                    Notulens.status =int.Parse(item_dr["status"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("nopekDelegasi"))))
                {
                    Notulens.nopekDelegasi = item_dr["nopekDelegasi"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("TglJatuhTempo"))))
                {
                    Notulens.tglJatuhTempo =DateTime.Parse(item_dr["TglJatuhTempo"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("TglNotulen"))))
                {
                    Notulens.tglNotulen = DateTime.Parse(item_dr["TglNotulen"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("createdBy"))))
                {
                    Notulens.CreatedBy = item_dr["createdBy"].ToString();
                }

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("subjek"))))
                {
                    Notulens.subjek = item_dr["subjek"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("isi"))))
                {
                    Notulens.isi = item_dr["isi"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idSeverity"))))
                {
                    Notulens.idSeverity =int.Parse(item_dr["idSeverity"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idProbability"))))
                {
                    Notulens.idProbability =int.Parse(item_dr["idProbability"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("namaPotential"))))
                {
                    Notulens.namaPotential = item_dr["namaPotential"].ToString();
                }

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idfungsi"))))
                {
                    Notulens.idfungsi = item_dr["idfungsi"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("nopekPIC"))))
                {
                    Notulens.nopekPIC = item_dr["nopekPIC"].ToString();
                }
                item_dr.Close();
                item_dr.Dispose();
                return true;
            }
            else
            {
                item_dr.Close();
                item_dr.Dispose();
                return false;
            }
        }

        public void DeleteNotulen(Notulen Notulens)
        {
            @function.Value = 18;
            @noNotulenDetail.Value = Notulens.noNotulenDetail;
            @ModifiedBy.Value = Notulens.ModifiedBy;
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @noNotulenDetail, @ModifiedBy);
        }

        public void DelegasiNotulen(Notulen Notulens)
        {
            @function.Value = 22;
            @noNotulenDetail.Value = Notulens.noNotulenDetail;
            @nopekDelegasi.Value = Notulens.nopekDelegasi;
            @tglDelegasi.Value = Notulens.tglDelegasi;
            @noteDelegasi.Value = Notulens.noteDelegasi;
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function,@noteDelegasi, @noNotulenDetail, @nopekDelegasi, @tglDelegasi);
        }

        public void ApproveNotulen(Notulen Notulens)
        {
            @function.Value = 31;
            @noNotulenDetail.Value = Notulens.noNotulenDetail;
            @status.Value = Notulens.status;
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @noNotulenDetail, @status);
        }

        public void UpdateStatusOverdue()
        {
            @function.Value = 32;
           
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function);
        }


        ////////////-------------- DASHBOARD ---------------------////////////
        public int cekJumlahStatus (int idStatus, int idrec)
        {
            @function.Value = 33;
            @status.Value = idStatus;
            @idRekomendasi.Value = idrec;
            return Convert.ToInt16(SqlHelper.ExecuteScalar(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @status, @idRekomendasi));
        }

        public int cekJumlahStatusSumber(int idStatus, int idSource)
        {
            @function.Value = 34;
            @status.Value = idStatus;
            @idSumber.Value = idSource;
            return Convert.ToInt16(SqlHelper.ExecuteScalar(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @status, @idSumber));
        }
        public int cekTotalRekomendasi(int idSource)
        {
            @function.Value = 344;
            @idSumber.Value = idSource;
            return Convert.ToInt16(SqlHelper.ExecuteScalar(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idSumber));
        }

        public int cekJumlahStatusByNopek(int idStatus, string nopek, int rekom)
        {
            @function.Value = 3444;
            @status.Value = idStatus;
            @nopekPIC.Value = nopek;
            @idRekomendasi.Value = rekom;
            return Convert.ToInt16(SqlHelper.ExecuteScalar(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @status, @nopekPIC, @idRekomendasi));
        }
        public DataSet ChartSummary()
        {
            @function.Value = 35;
            
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function);
        }
        public string cekEmail(string nopek)
        {
            @function.Value = 36;
            @nopekPIC.Value = nopek;
            return Convert.ToString(SqlHelper.ExecuteScalar(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @nopekPIC));
        }

        public DataSet ListTaskByDashboard(string nFungsi, int idRek, int idSrc)
        {
            @function.Value = 1455;
            //if (nFungsi != null)
            //{
            //    @namaFungsi.Value = nFungsi;
            //}
            //else
            //{
            //    @namaFungsi.Value = DBNull.Value;
            //}
            @idRekomendasi.Value = idRek;
            @idSumber.Value = idSrc;

            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, 
                 @idRekomendasi, @idSumber);
        }

        public DataSet ListTaskByDashboardStatus(int stats, int idRek, int idSrc)
        {
            @function.Value = 1455;
            //if (nFungsi != null)
            //{
            //    @namaFungsi.Value = nFungsi;
            //}
            //else
            //{
            //    @namaFungsi.Value = DBNull.Value;
            //}
            @status.Value = stats;
            @idRekomendasi.Value = idRek;
            @idSumber.Value = idSrc;

            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function,
                 @idRekomendasi, @idSumber,@status);
        }

        public DataSet SummaryByFungsi(int PIC)
        {
            @function.Value = 333;
            @idRekomendasi.Value = PIC;
            //@nopekPIC.Value = PIC;
            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idRekomendasi);
        }
        #endregion
    }
}