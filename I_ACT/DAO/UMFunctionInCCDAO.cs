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
    public class UMFunctionInCCDAO : UMFunctionInCC
    {
        #region Parameters Declaration

        protected SqlParameter p_function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter p_recid = new SqlParameter("@recid", SqlDbType.Int);
      
        protected SqlParameter p_NameCostCenter = new SqlParameter("@NameCostCenter", SqlDbType.VarChar, 50);
        protected SqlParameter p_NoCostCenter = new SqlParameter("@NoCostCenter", SqlDbType.VarChar, 50);
        protected SqlParameter p_NameFunction = new SqlParameter("@NameFunction", SqlDbType.VarChar, 50);
        protected SqlParameter p_NameSection = new SqlParameter("@NameSection", SqlDbType.VarChar, 50);
        protected SqlParameter p_CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter p_CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter p_ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter p_ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter p_isActive = new SqlParameter("@isActive", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet ListUMFunctionInCC()
        {
            p_function.Value = 1 ;
            
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_CostCenter", p_function);
        }
        public DataSet SearchUMFunctionInCC(UMFunctionInCC UMfungsi)
        {
            p_function.Value = 2;
            if (UMfungsi.recid != null)
            {
                p_recid.Value = UMfungsi.recid;
            }
            else
            {
                p_recid.Value = DBNull.Value;
            }

            if (UMfungsi.NameCostCenter != null)
            {
                p_NameCostCenter.Value = UMfungsi.NameCostCenter;
            }
            else
            {
                p_NameCostCenter.Value = DBNull.Value;
            }

            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_CostCenter", p_function, p_recid, p_NameCostCenter);
        }

        public bool AddUMFunctionInCC(UMFunctionInCC UMfungsi)
        {
            try
            {
                p_function.Value = 3;
                p_NoCostCenter.Value = UMfungsi.NoCostCenter;
                p_NameFunction.Value = UMfungsi.NameFunction;
                p_NameSection.Value = UMfungsi.NameSection;
                p_CreatedBy.Value = UMfungsi.CreatedBy;
                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_CostCenter", p_function,
                p_NoCostCenter,p_NameFunction,p_NameSection, p_CreatedBy);
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

        public bool EditUMFunctionInCC(UMFunctionInCC UMfungsi)
        {
            try
            {
                p_function.Value = 4;
                p_recid.Value = UMfungsi.recid;
                p_NoCostCenter.Value = UMfungsi.NoCostCenter;
                p_NameFunction.Value = UMfungsi.NameFunction;
                p_NameSection.Value = UMfungsi.NameSection;
                p_CreatedBy.Value = UMfungsi.CreatedBy;
                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_CostCenter", p_function,
                p_NoCostCenter, p_NameFunction, p_NameSection, p_CreatedBy, p_recid);
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
        public bool GetUMFunctionInCCDetail(ref UMFunctionInCC UMfungsi)
        {
            try
            {
                SqlDataReader item_dr;
                p_function.Value = 5;
                p_recid.Value = UMfungsi.recid;

                item_dr = SqlHelper.ExecuteReader(General.UMconnString, CommandType.StoredProcedure, "SP_CostCenter", p_function, p_recid);

                if (item_dr.HasRows)
                {
                    item_dr.Read();

                    if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NoCostCenter"))))
                    {
                        UMfungsi.NoCostCenter = item_dr["NoCostCenter"].ToString();
                    }
                   
                    if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NameSection"))))
                    {
                        UMfungsi.NameSection = item_dr["NameSection"].ToString();
                    }
                    if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NameFunction"))))
                    {
                        UMfungsi.NameFunction = item_dr["NameFunction"].ToString();
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteUMFunctionInCC(UMFunctionInCC UMfungsi)
        {
            p_function.Value = 6;
            p_recid.Value = UMfungsi.recid;
            p_ModifiedBy.Value = UMfungsi.ModifiedBy;
            SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_CostCenter", p_function, p_recid, p_ModifiedBy);
        }
       
        #endregion
    }
}