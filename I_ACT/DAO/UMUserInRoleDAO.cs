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
    public class UMUserInRoleDAO : UMUserInRole
    {
        #region Parameters Declaration

       
        protected SqlParameter @function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter @db = new SqlParameter("@db", SqlDbType.VarChar, 100);
        
        protected SqlParameter @idRole = new SqlParameter("@idRole", SqlDbType.Int);
        protected SqlParameter @username = new SqlParameter("@username", SqlDbType.VarChar, 50);
        protected SqlParameter @RoleName = new SqlParameter("@RoleName", SqlDbType.VarChar, 50);
        protected SqlParameter @section = new SqlParameter("@section", SqlDbType.VarChar, 100);

        protected SqlParameter @CreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @CreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter @ModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter @ModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter @isActive = new SqlParameter("@isActive", SqlDbType.Bit);
        protected SqlParameter @isBanned = new SqlParameter("@isBanned", SqlDbType.Bit);
        protected SqlParameter @requested = new SqlParameter("@requested", SqlDbType.Bit);
        

        #endregion

        #region Public Method

        public DataSet ListUser()
        {
            @function.Value = 7 ;
            @db.Value = System.Web.HttpContext.Current.Session["db"] as string; 
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function,@db);
        }
        public DataSet SearchUser(UMUserInRole users)
        {
            @function.Value = 8;
            @db.Value = System.Web.HttpContext.Current.Session["db"] as string; 
            if (users.username != null)
            {
                @username.Value = users.username;
            }
            else
            {
                @username.Value = DBNull.Value;
            }
            if (users.RoleName != null)
            {
                @RoleName.Value = users.RoleName;
            }
            else
            {
                @RoleName.Value = DBNull.Value;
            }
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function,@RoleName, @username, @db);
        }
        public bool AddUser(UMUserInRole users)
        {
            try
            {
                @function.Value = 9;
                @db.Value = System.Web.HttpContext.Current.Session["db"] as string; 
                @username.Value = users.username;
                @idRole.Value = users.idRole;
                @requested.Value = users.requested;
                @section.Value = users.section;
             
                @CreatedBy.Value = users.CreatedBy;
                @CreatedOn.Value = DateTime.Now;
                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function,
                @username, @idRole, @requested, @section, @CreatedBy,@CreatedOn,@db);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
        public bool EditUser(UMUserInRole users)
        {
            try
            {
                @function.Value = 10;
                @db.Value = System.Web.HttpContext.Current.Session["db"] as string; 
                @username.Value = users.username;
                @idRole.Value = users.idRole;
                @requested.Value = users.requested;
                @section.Value = users.section;
                @ModifiedBy.Value = users.ModifiedBy;
                @ModifiedOn.Value = DateTime.Now;
                @isBanned.Value = users.isBanned;

                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @function,
                @username, @idRole, @requested, @section, @ModifiedBy,@ModifiedOn, @isBanned, @db);
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool GetUserDetail(ref UMUserInRole users)
        {
            SqlDataReader item_dr;
            @function.Value = 11;
            @db.Value = System.Web.HttpContext.Current.Session["db"] as string; 
            @username.Value = users.username;

            item_dr = SqlHelper.ExecuteReader(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen",@db, @function, @username);

            if (item_dr.HasRows)
            {
                item_dr.Read();

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idRole"))))
                {
                    users.idRole = int.Parse(item_dr["idRole"].ToString());
                }
                else
                {
                    users.idRole = null;
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Section"))))
                {
                    users.section = item_dr["Section"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("isBanned"))))
                {
                    users.isBanned =bool.Parse(item_dr["isBanned"].ToString());
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("requested"))))
                {
                    users.requested = bool.Parse(item_dr["requested"].ToString());
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
        public void DeleteUser(UMUserInRole users)
        {
            @function.Value = 12;
            @db.Value = System.Web.HttpContext.Current.Session["db"] as string; 
            @username.Value = users.username;
            @ModifiedBy.Value = users.ModifiedBy;
            @ModifiedOn.Value = DateTime.Now;
            SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen",@db, @function, @username,@ModifiedOn, @ModifiedBy);
        }
        public void BannedUser(UMUserInRole users)
        {
            @function.Value = 13;
            @db.Value = System.Web.HttpContext.Current.Session["db"] as string;
            @username.Value = users.username;
            @ModifiedBy.Value = users.ModifiedBy;
            @ModifiedOn.Value = DateTime.Now;
            SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @db, @function, @username, @ModifiedOn, @ModifiedBy);
        }
        public void OpenBannedUser(UMUserInRole users)
        {
            @function.Value = 14;
            @db.Value = System.Web.HttpContext.Current.Session["db"] as string;
            @username.Value = users.username;
            @ModifiedBy.Value = users.ModifiedBy;
            @ModifiedOn.Value = DateTime.Now;
            SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @db, @function, @username, @ModifiedOn, @ModifiedBy);
        }
        public void ApproveUser(UMUserInRole users)
        {
            @function.Value = 15;
            @db.Value = System.Web.HttpContext.Current.Session["db"] as string;
            @username.Value = users.username;
            @ModifiedBy.Value = users.ModifiedBy;
            @ModifiedOn.Value = DateTime.Now;
            SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UserManajemen", @db, @function, @username, @ModifiedOn, @ModifiedBy);
        }
        #endregion
    }
}