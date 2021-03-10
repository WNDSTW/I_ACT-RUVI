using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using I_ACT.Domain;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Data.SqlClient;

namespace I_ACT.DAO
{
    public class LogDAO : Log
    {
        #region Parameters Declaration

        protected SqlParameter p_function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter p_recid = new SqlParameter("@recidUnit", SqlDbType.Int);

        protected SqlParameter pMenu = new SqlParameter("@Menu", SqlDbType.VarChar, 50);
        protected SqlParameter pAktifitas = new SqlParameter("@Aktifitas", SqlDbType.VarChar, 50);
        protected SqlParameter pDeskripsi = new SqlParameter("@Deskripsi", SqlDbType.VarChar, 500);
        protected SqlParameter pCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter pCreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        #endregion

        #region Public Method

        public bool AddLog(Log logs)
        {
            try
            {
                p_function.Value = 111111;
                pMenu.Value = logs.menu;
                pAktifitas.Value = logs.aktifitas;
                pDeskripsi.Value = logs.deskripsi;
                pCreatedBy.Value = logs.CreatedBy;
                int result =   SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Base", p_function,pMenu,
                pAktifitas, pDeskripsi,pCreatedBy);
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