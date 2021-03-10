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
    public class CounterDAO
    {
        #region Parameters Declaration
        protected SqlParameter p_function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter pNo = new SqlParameter("@No", SqlDbType.Int);
        protected SqlParameter pNoDetail = new SqlParameter("@NoDetail", SqlDbType.Int);

        protected SqlParameter pKode1 = new SqlParameter("@Kode1", SqlDbType.VarChar, 50);
        protected SqlParameter pKode2 = new SqlParameter("@Kode2", SqlDbType.VarChar, 50);
        protected SqlParameter pKode3 = new SqlParameter("@Kode3", SqlDbType.VarChar, 50);
        protected SqlParameter pKode4 = new SqlParameter("@Kode4", SqlDbType.VarChar, 50);
        protected SqlParameter pKode5 = new SqlParameter("@Kode5", SqlDbType.VarChar, 50);
        protected SqlParameter pKode6 = new SqlParameter("@Kode6", SqlDbType.VarChar, 50);
        #endregion

        #region methods
        public bool GetCounter(ref Counter counters)
        {
            SqlDataReader item_dr;
            p_function.Value = 87;
            pKode1.Value = counters.Kode1;
            pKode2.Value = counters.Kode2;
            pKode3.Value = counters.Kode3;
            pKode4.Value = counters.Kode4;
            pKode5.Value = counters.Kode5;
            pKode6.Value = counters.Kode6;
            pNo.Value = counters.no;
            item_dr = SqlHelper.ExecuteReader(General.connString, CommandType.StoredProcedure, "SP_BASE", p_function,
                pKode1, pKode2, pKode3, pKode4, pKode5, pKode6, pNo);

            if (item_dr.HasRows)
            {
                item_dr.Read();

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode1"))))
                {
                    counters.Kode1 = item_dr["Kode1"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode2"))))
                {
                    counters.Kode2 = item_dr["Kode2"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode3"))))
                {
                    counters.Kode3 = item_dr["Kode3"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode4"))))
                {
                    counters.Kode4 = item_dr["Kode4"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode5"))))
                {
                    counters.Kode5 = item_dr["Kode5"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode6"))))
                {
                    counters.Kode6 = item_dr["Kode6"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("No"))))
                {
                    counters.no = int.Parse(item_dr["No"].ToString());
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
        public bool GetCounterDetail(ref Counter counters)
        {
            SqlDataReader item_dr;
            p_function.Value = 8777;
            pKode1.Value = counters.Kode1;
            pKode2.Value = counters.Kode2;
            pKode3.Value = counters.Kode3;
            pKode4.Value = counters.Kode4;
            pKode5.Value = counters.Kode5;
            pKode6.Value = counters.Kode6;
            pNo.Value = counters.no;
            pNoDetail.Value = counters.noDetail;
            item_dr = SqlHelper.ExecuteReader(General.connString, CommandType.StoredProcedure, "SP_BASE", p_function,
                pKode1, pKode2, pKode3, pKode4, pKode5, pKode6, pNo, pNoDetail);

            if (item_dr.HasRows)
            {
                item_dr.Read();

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode1"))))
                {
                    counters.Kode1 = item_dr["Kode1"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode2"))))
                {
                    counters.Kode2 = item_dr["Kode2"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode3"))))
                {
                    counters.Kode3 = item_dr["Kode3"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode4"))))
                {
                    counters.Kode4 = item_dr["Kode4"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode5"))))
                {
                    counters.Kode5 = item_dr["Kode5"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Kode6"))))
                {
                    counters.Kode6 = item_dr["Kode6"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("No"))))
                {
                    counters.no = int.Parse(item_dr["No"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NoDetail"))))
                {
                    counters.noDetail = int.Parse(item_dr["NoDetail"].ToString());
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
        public bool AddCounter(Counter counters)
        {
            try
            {
                p_function.Value = 88;

                pKode1.Value = counters.Kode1;
                pKode2.Value = counters.Kode2;
                pKode3.Value = counters.Kode3;
                pKode4.Value = counters.Kode4;
                pKode5.Value = counters.Kode5;
                pKode6.Value = counters.Kode6;
                pNo.Value = counters.no;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_BASE", p_function,
                pKode1, pKode2, pKode3, pKode4, pKode5, pKode6, pNo);
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
        public bool EditCounter(Counter counters)
        {
            try
            {
                p_function.Value = 89;
                pKode1.Value = counters.Kode1;
                pKode2.Value = counters.Kode2;
                pKode3.Value = counters.Kode3;
                pKode4.Value = counters.Kode4;
                pKode5.Value = counters.Kode5;
                pKode6.Value = counters.Kode6;
                pNo.Value = counters.no;
                int result = SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "sp_bASE", p_function,
                pKode1, pKode2, pKode3, pKode4, pKode5, pKode6, pNo);
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