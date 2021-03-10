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
    public class SumberDAO : Sumber
    {
        #region Parameters Declaration

        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @search = new SqlParameter("@search", SqlDbType.VarChar, 50);
        protected SqlParameter @idSumber = new SqlParameter("@idSumber", SqlDbType.Int);
        protected SqlParameter @idRekomendasi = new SqlParameter("@idRekomendasi", SqlDbType.Int);
        protected SqlParameter @namaSumber = new SqlParameter("@namaSumber", SqlDbType.VarChar, 50);
        protected SqlParameter @singkatanSumber = new SqlParameter("@singkatanSumber", SqlDbType.VarChar, 50);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);
        protected SqlParameter @isRAM = new SqlParameter("@isRAM", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet ListSumber()
        {
            @function.Value = 7;

            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function);
        }
        public DataSet SearchSumber(string keySearch)
        {
            @function.Value = 8;
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

        public string SearchSingkatanSumber(string keySearch)
        {
            @function.Value = 8888;
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
        public DataSet SearchSumberbyIdRekomendasi(Sumber sumbers)
        {
            @function.Value = 888;
            if (sumbers.idRekomendasi != null)
            {
                @idRekomendasi.Value = sumbers.idRekomendasi;
            }
            else
            {
                @idRekomendasi.Value = DBNull.Value;
            }

            return SqlHelper.ExecuteDataset(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idRekomendasi);
        }

        public bool AddSumber(Sumber Sumbers)
        {
            try
            {
                @function.Value = 9;
                @idRekomendasi.Value = Sumbers.idRekomendasi;
                @namaSumber.Value = Sumbers.NamaSumber;
                @singkatanSumber.Value = Sumbers.SingkatanSumber;
                @isRAM.Value = Sumbers.isRAM;
                @CreatedBy.Value = Sumbers.CreatedBy;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function,@idRekomendasi, @singkatanSumber,
                 @namaSumber, @CreatedBy, @isRAM);
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

        public bool EditSumber(Sumber Sumbers)
        {
            try
            {
                @function.Value = 10;
                @idSumber.Value = Sumbers.idSumber;
                @namaSumber.Value = Sumbers.NamaSumber;
                @singkatanSumber.Value = Sumbers.SingkatanSumber;
                @idRekomendasi.Value = Sumbers.idRekomendasi;
                @isRAM.Value = Sumbers.isRAM;
                @ModifiedBy.Value = Sumbers.ModifiedBy;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @namaSumber, @singkatanSumber,
                 @idSumber, @idRekomendasi, @ModifiedBy, @isRAM);
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
        public bool GetSumberDetail(ref Sumber Sumbers)
        {
            SqlDataReader item_dr;
            @function.Value = 11;
            @idSumber.Value = Sumbers.idSumber;

            item_dr = SqlHelper.ExecuteReader(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idSumber);

            if (item_dr.HasRows)
            {
                item_dr.Read();


                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NamaSumber"))))
                {
                    Sumbers.NamaSumber = item_dr["NamaSumber"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("SingkatanSumber"))))
                {
                    Sumbers.SingkatanSumber = item_dr["SingkatanSumber"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idRekomendasi"))))
                {
                    Sumbers.idRekomendasi =int.Parse(item_dr["idRekomendasi"].ToString());
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
        public void DeleteSumber(Sumber Sumbers)
        {
            @function.Value = 12;
            @idSumber.Value = Sumbers.idSumber;
            @ModifiedBy.Value = Sumbers.ModifiedBy;
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idSumber, @ModifiedBy);
        }

        public bool cekRAM(Sumber Sumbers)
        {
            @function.Value = 777;
            @idSumber.Value = Sumbers.idSumber;
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(General.connString, CommandType.StoredProcedure, "SP_Base", @function, @idSumber));
        }
        #endregion
    }
}