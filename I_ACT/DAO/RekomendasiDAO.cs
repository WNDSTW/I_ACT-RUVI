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
    public class RekomendasiDAO : Rekomendasi
    {
        #region Parameters Declaration

        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @search = new SqlParameter("@search", SqlDbType.VarChar, 50);
        protected SqlParameter @idRekomendasi = new SqlParameter("@idRekomendasi", SqlDbType.Int);
        protected SqlParameter @namaRekomendasi = new SqlParameter("@namaRekomendasi", SqlDbType.VarChar, 50);
        protected SqlParameter @singkatanRekomendasi = new SqlParameter("@singkatanRekomendasi", SqlDbType.VarChar, 50);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet ListRekomendasi()
        {
            @function.Value = 1;

            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function);
        }
        public DataSet SearchRekomendasi(string keySearch)
        {
            @function.Value = 2;
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
        public string SearchSingkatanRekomendasi(string keySearch)
        {
            @function.Value = 2222;
            if (keySearch != null)
            {
                @search.Value = keySearch;
            }
            else
            {
                @search.Value = DBNull.Value;
            }

            return Convert.ToString(SqlHelper.ExecuteScalar(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @search));
        }
        public bool AddRekomendasi(Rekomendasi Rekomendasis)
        {
            try
            {
                @function.Value = 3;

                @namaRekomendasi.Value = Rekomendasis.NamaRekomendasi;
                @singkatanRekomendasi.Value = Rekomendasis.SingkatanRekomendasi;

                @CreatedBy.Value = Rekomendasis.CreatedBy;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @singkatanRekomendasi,
                 @namaRekomendasi, @CreatedBy);
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

        public bool EditRekomendasi(Rekomendasi Rekomendasis)
        {
            try
            {
                @function.Value = 4;
                @idRekomendasi.Value = Rekomendasis.idRekomendasi;
                @namaRekomendasi.Value = Rekomendasis.NamaRekomendasi;
                @singkatanRekomendasi.Value = Rekomendasis.SingkatanRekomendasi;
                @ModifiedBy.Value = Rekomendasis.ModifiedBy;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @namaRekomendasi, @singkatanRekomendasi,
                 @idRekomendasi, @ModifiedBy);
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
        public bool GetRekomendasiDetail(ref Rekomendasi Rekomendasis)
        {
            SqlDataReader item_dr;
            @function.Value = 5;
            @idRekomendasi.Value = Rekomendasis.idRekomendasi;

            item_dr = SqlHelper.ExecuteReader(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idRekomendasi);

            if (item_dr.HasRows)
            {
                item_dr.Read();


                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NamaRekomendasi"))))
                {
                    Rekomendasis.NamaRekomendasi = item_dr["NamaRekomendasi"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("SingkatanRekomendasi"))))
                {
                    Rekomendasis.SingkatanRekomendasi = item_dr["SingkatanRekomendasi"].ToString();
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
        public void DeleteRekomendasi(Rekomendasi Rekomendasis)
        {
            @function.Value = 6;
            @idRekomendasi.Value = Rekomendasis.idRekomendasi;
            @ModifiedBy.Value = Rekomendasis.ModifiedBy;
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idRekomendasi, @ModifiedBy);
        }

        #endregion
    }
}