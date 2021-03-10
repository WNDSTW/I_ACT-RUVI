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
    public class KomentarDAO : Komentar
    {
        #region Parameters Declaration

        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @search = new SqlParameter("@search", SqlDbType.VarChar, 50);
        protected SqlParameter @idKomentar = new SqlParameter("@idKomentar", SqlDbType.Int);
        protected SqlParameter @idEvidence = new SqlParameter("@idEvidence", SqlDbType.Int);
        protected SqlParameter @Komentar = new SqlParameter("@Komentar", SqlDbType.VarChar, 200);
        protected SqlParameter @NoNotulenDetail = new SqlParameter("@NoNotulenDetail", SqlDbType.VarChar, 50);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);

        #endregion

        #region Public Method

       
        public DataSet SearchKomentar(string keySearch)
        {
            @function.Value = 29;
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

   
        public bool AddKomentar(Komentar komentars, int idEvidences)
        {
            try
            {
                @function.Value = 30;
                @NoNotulenDetail.Value = komentars.@NoNotulenDetail;
                @Komentar.Value = komentars.isiKomentar;
                @idEvidence.Value = idEvidences;
                @CreatedBy.Value = komentars.CreatedBy;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", @function,@idEvidence, @NoNotulenDetail,
                 @Komentar, @CreatedBy);
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

        #endregion
    }
}