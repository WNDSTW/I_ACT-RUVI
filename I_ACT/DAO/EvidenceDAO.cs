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
    public class EvidenceDAO : Evidence
    {
        #region Parameters Declaration

        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @search = new SqlParameter("@search", SqlDbType.VarChar, 50);
        protected SqlParameter @idEvidence = new SqlParameter("@idEvidence", SqlDbType.Int);
        protected SqlParameter @noNotulenDetail = new SqlParameter("@noNotulenDetail", SqlDbType.VarChar, 50);
        protected SqlParameter @keterangan = new SqlParameter("@keterangan", SqlDbType.VarChar, 200);
        protected SqlParameter @fname = new SqlParameter("@fname", SqlDbType.VarChar, 100);
        protected SqlParameter @nopekSubmit = new SqlParameter("@nopekSubmit", SqlDbType.VarChar, 50);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet ListEvidence()
        {
            @function.Value = 23;

            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function);
        }
        public DataSet SearchEvidence(string keySearch)
        {
            @function.Value = 24;
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

        public bool AddEvidence(Evidence Evidences)
        {
            try
            {
                @function.Value = 25;

                @noNotulenDetail.Value = Evidences.noNotulenDetail;
                @keterangan.Value = Evidences.Keterangan;
                @nopekSubmit.Value = Evidences.nopekSubmit;
                @fname.Value = Evidences.fname;
                @CreatedBy.Value = Evidences.CreatedBy;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @nopekSubmit, @keterangan,
                 @noNotulenDetail,@fname, @CreatedBy);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool EditEvidence(Evidence Evidences)
        {
            try
            {
                @function.Value = 26;
                @idEvidence.Value = Evidences.idEvidence;
                @noNotulenDetail.Value = Evidences.noNotulenDetail;
                @keterangan.Value = Evidences.Keterangan;
                @nopekSubmit.Value = Evidences.nopekSubmit;
                @fname.Value = Evidences.fname;
                @ModifiedBy.Value = Evidences.ModifiedBy;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @nopekSubmit, @keterangan,
                 @nopekSubmit, @ModifiedBy, @idEvidence);
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool GetEvidenceDetail(ref Evidence Evidences)
        {
            SqlDataReader item_dr;
            @function.Value = 27;
            @idEvidence.Value = Evidences.idEvidence;

            item_dr = SqlHelper.ExecuteReader(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idEvidence);

            if (item_dr.HasRows)
            {
                item_dr.Read();


                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("noNotulenDetail"))))
                {
                    Evidences.noNotulenDetail = item_dr["noNotulenDetail"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("nopekSubmit"))))
                {
                    Evidences.nopekSubmit = item_dr["nopekSubmit"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("keterangan"))))
                {
                    Evidences.Keterangan = item_dr["keterangan"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("fname"))))
                {
                    Evidences.fname = item_dr["fname"].ToString();
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

        public bool GetEvidenceDetailByNoNotulen(ref Evidence Evidences)
        {
            SqlDataReader item_dr;
            @function.Value = 2777;
            @noNotulenDetail.Value = Evidences.noNotulenDetail;

            item_dr = SqlHelper.ExecuteReader(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @noNotulenDetail);

            if (item_dr.HasRows)
            {
                item_dr.Read();


                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("noNotulenDetail"))))
                {
                    Evidences.noNotulenDetail = item_dr["noNotulenDetail"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("nopekSubmit"))))
                {
                    Evidences.nopekSubmit = item_dr["nopekSubmit"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("keterangan"))))
                {
                    Evidences.Keterangan = item_dr["keterangan"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idEvidence"))))
                {
                    Evidences.idEvidence =int.Parse(item_dr["idEvidence"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("fname"))))
                {
                    Evidences.fname = item_dr["fname"].ToString();
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
        public void DeleteEvidence(Evidence Evidences)
        {
            @function.Value = 28;
            @idEvidence.Value = Evidences.idEvidence;
            @ModifiedBy.Value = Evidences.ModifiedBy;
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idEvidence, @ModifiedBy);
        }

        #endregion
    }
}