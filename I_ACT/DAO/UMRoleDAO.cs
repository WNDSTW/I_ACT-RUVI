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
    public class UMRoleDAO : UMRole
    {
        #region Parameters Declaration

        string database = System.Web.HttpContext.Current.Session["db"] as string;
        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @db = new SqlParameter("@db", SqlDbType.VarChar, 100);
        protected SqlParameter @IdRole = new SqlParameter("@IdRole", SqlDbType.Int);
        protected SqlParameter @NoLevel = new SqlParameter("@NoLevel", SqlDbType.Int);
        protected SqlParameter @RoleName = new SqlParameter("@RoleName", SqlDbType.VarChar, 100);
        protected SqlParameter @Jabatan = new SqlParameter("@Jabatan", SqlDbType.VarChar, 100);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet ListRole()
        {
            @function.Value = 1 ;
            @db.Value = database;
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function, @db);
        }
        public DataSet SearchRole(UMRole roles)
        {
            @function.Value = 2;
            @db.Value = database;
            if (roles.RoleName != null)
            {
                @RoleName.Value = roles.RoleName;
            }
            else
            {
                @RoleName.Value = DBNull.Value;
            }


            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function,@db, @RoleName);
        }
        public DataSet SearchRoleDetail(UMRole roles)
        {
            @function.Value = 222;
            @db.Value = database;
            if (roles.IdRole != null)
            {
                @IdRole.Value = roles.IdRole;
            }
            else
            {
                @IdRole.Value = DBNull.Value;
            }
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function, @db, @IdRole);
        }
        public bool GetRoleDetail_ByIdRole(ref UMRole roles)
        {
            try
            {
                SqlDataReader item_dr;
                @function.Value = 2222;
                @IdRole.Value = roles.IdRole;
                @db.Value = database;
                item_dr = SqlHelper.ExecuteReader(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function, @db, @IdRole);

                if (item_dr.HasRows)
                {
                    item_dr.Read();

                    if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Jabatan"))))
                    {
                        roles.Jabatan = item_dr["Jabatan"].ToString();
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
                return false;
                string error = ex.ToString();
            }

        }
        public void AddRole(UMRole roles)
        {
            DataTable dt = System.Web.HttpContext.Current.Session["dt"] as DataTable;
            try
            {
                @function.Value = 3;
                @db.Value = database;
                @RoleName.Value = roles.RoleName;
                @NoLevel.Value = roles.NoLevel;
                @CreatedOn.Value = DateTime.Now;
                @CreatedBy.Value = roles.CreatedBy;
                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen",@db, @function,
                @RoleName, @NoLevel, @CreatedOn, @CreatedBy);

                foreach (DataRow row in dt.Rows)
                {
                    @function.Value = 333;
                    @db.Value = database;
                    @RoleName.Value = roles.RoleName;
                    @Jabatan.Value = row[0].ToString();
                    SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function,
                    @db, @RoleName, @Jabatan);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
        public void EditRole(UMRole roles)
        {
            DataTable dt = System.Web.HttpContext.Current.Session["dt"] as DataTable;
            SqlConnection conn = null;
            SqlTransaction transact = null;
            try
            {
                conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["appconstring"].ToString());
                conn.Open();
                transact = conn.BeginTransaction();

                //update UM_role
                @function.Value = 4;
                @db.Value = database;
                @IdRole.Value = roles.IdRole;
                @RoleName.Value = roles.RoleName;
                @NoLevel.Value = roles.NoLevel;
                @ModifiedOn.Value = DateTime.Now;
                @ModifiedBy.Value = roles.ModifiedBy;
                int result = SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_UserManajemen", @db, @function,
                @IdRole, @RoleName, @ModifiedBy,@ModifiedOn, @NoLevel);


                //Insert UM_RoleDetail
                foreach (DataRow row in dt.Rows)
                {
                    @function.Value = 333;
                    @db.Value = database;
                    @RoleName.Value = roles.RoleName;
                    @Jabatan.Value = row[0].ToString();
                    result=SqlHelper.ExecuteNonQuery(transact, CommandType.StoredProcedure, "SP_UserManajemen", @function,
                    @db, @RoleName, @Jabatan);
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
        public bool GetRoleDetail(ref UMRole roles)
        {
            try
            {
                SqlDataReader item_dr;
                @function.Value = 5;
                @IdRole.Value = roles.IdRole;
                @db.Value = database;
                item_dr = SqlHelper.ExecuteReader(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function, @db, @IdRole);

                if (item_dr.HasRows)
                {
                    item_dr.Read();

                    if (!(item_dr.IsDBNull(item_dr.GetOrdinal("RoleName"))))
                    {
                        roles.RoleName = item_dr["RoleName"].ToString();
                    }
                    if (!(item_dr.IsDBNull(item_dr.GetOrdinal("NoLevel"))))
                    {
                        roles.NoLevel = int.Parse(item_dr["NoLevel"].ToString());
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
                return false;
                string error = ex.ToString();
            }
            
        }
        public void DeleteRole (UMRole roles)
        {
            @function.Value = 6;
            @db.Value = database;
            @IdRole.Value = roles.IdRole;
            @ModifiedBy.Value = roles.ModifiedBy;
            SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen",@db, @function, @IdRole, @ModifiedBy);
        }
       
        #endregion
    }
}