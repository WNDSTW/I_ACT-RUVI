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
    public class UMActionRoleDAO : UMActionRole
    {
        #region Parameters Declaration

        string database = System.Web.HttpContext.Current.Session["db"] as string;
        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @db = new SqlParameter("@db", SqlDbType.VarChar, 100);
        protected SqlParameter @IdRole = new SqlParameter("@IdRole", SqlDbType.Int);
        protected SqlParameter @ActId = new SqlParameter("@ActId", SqlDbType.Int);
        protected SqlParameter @MenuId = new SqlParameter("@MenuId", SqlDbType.Int);
        protected SqlParameter @recId = new SqlParameter("@recId", SqlDbType.Int);
        protected SqlParameter @RoleName = new SqlParameter("@RoleName", SqlDbType.VarChar, 100);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet ListActRole()
        {
            @function.Value = 16 ;
            @db.Value = database;
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function, @db);
        }
        public DataSet SearchActRole(UMActionRole actroles)
        {
            @function.Value = 17;
            @db.Value = database;
            if (actroles.RoleName != null)
            {
                @RoleName.Value = actroles.RoleName;
            }
            else
            {
                @RoleName.Value = DBNull.Value;
            }
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function,@db, @RoleName);
        }
        public DataSet SearchActRoleByMenu(UMActionRole actroles)
        {
            @function.Value =177;
            @db.Value = database;
            if (actroles.MenuId != null)
            {
                @MenuId.Value = actroles.MenuId;
            }
            else
            {
                @MenuId.Value = DBNull.Value;
            }
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function, @db, @MenuId);
        }
        public bool AddActRole(UMActionRole actroles)
        {
            try
            {
                @function.Value = 20;
                @db.Value = database;
                @ActId.Value = actroles.ActId;
                @IdRole.Value = actroles.IdRole;
                @CreatedBy.Value = actroles.CreatedBy;
                @CreatedOn.Value = DateTime.Now;
                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "[SP_UserManajemen]", @db, @function,
                 @ActId,@IdRole, @CreatedOn, @CreatedBy);
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
        public bool EditActRole(UMActionRole actroles)
        {
            try
            {
                @function.Value = 19;
                @db.Value = database;
                @recId.Value = actroles.recId;
                @IdRole.Value = actroles.IdRole;
                @ActId.Value = actroles.ActId;
                @ModifiedOn.Value = DateTime.Now;
                @ModifiedBy.Value = actroles.ModifiedBy;
                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen",@db, @function,
                @IdRole, @ActId,@recId, @ModifiedBy, @ModifiedOn);
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
        public bool GetActRoleDetail(ref UMActionRole actroles)
        {
            try
            {
                SqlDataReader item_dr;
                @function.Value = 211;
                @IdRole.Value = actroles.IdRole;
                @db.Value = database;
                item_dr = SqlHelper.ExecuteReader(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function, @db, @IdRole);

                if (item_dr.HasRows)
                {
                    item_dr.Read();

                    //if (!(item_dr.IsDBNull(item_dr.GetOrdinal("RoleName"))))
                    //{
                    //    actroles.RoleName = item_dr["RoleName"].ToString();
                    //}
                    //if (!(item_dr.IsDBNull(item_dr.GetOrdinal("IdRole"))))
                    //{
                    //    actroles.IdRole = int.Parse(item_dr["IdRole"].ToString());
                    //}

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
                return false;
                string error = ex.ToString();
            }
            
        }
        public void DeleteRole (UMActionRole actroles)
        {
            @function.Value = 21;
            @db.Value = database;
            @ActId.Value = actroles.ActId;
            @IdRole.Value = actroles.IdRole;
            SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @db, @function, @ActId, @IdRole);
        }
        #endregion
    }
}