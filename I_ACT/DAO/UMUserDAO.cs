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
    public class UMUserDAO : UMUser
    {
        #region Parameters Declaration

        protected SqlParameter p_function = new SqlParameter("@function", SqlDbType.Int);
        protected SqlParameter pId = new SqlParameter("@ID", SqlDbType.VarChar, 50);
        protected SqlParameter pAppId = new SqlParameter("@AppID", SqlDbType.VarChar, 50);
        protected SqlParameter pRoleID = new SqlParameter("@RoleID", SqlDbType.Int);
        protected SqlParameter pUsername = new SqlParameter("@Username", SqlDbType.VarChar, 50);
        protected SqlParameter pRole = new SqlParameter("@Role", SqlDbType.VarChar, 50);
        protected SqlParameter pSection = new SqlParameter("@Section", SqlDbType.VarChar, 50);

        protected SqlParameter pCreatedOn = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
        protected SqlParameter pModifiedBy = new SqlParameter("@ModifiedBy", SqlDbType.VarChar, 50);
        protected SqlParameter pCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.VarChar, 50);
        protected SqlParameter pModifiedOn = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
        protected SqlParameter pisActive = new SqlParameter("@isActive", SqlDbType.Bit);

        #endregion

        #region Public Method

        public DataSet ListUser()
        {
            p_function.Value = 1 ;
            pAppId.Value = "721e2b82-4cdc-4299-bbdb-fb1e7039b0b1";
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UMUser", p_function, pAppId);
        }
        public DataSet SearchUser(UMUser users)
        {
            p_function.Value = 2;
            pAppId.Value = "721e2b82-4cdc-4299-bbdb-fb1e7039b0b1";

            if (users.username != null)
            {
                pUsername.Value = users.username;
            }
            else
            {
                pUsername.Value = DBNull.Value;
            }

            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UMUser", p_function, pUsername, pAppId);
        }

        public bool AddUser(UMUser users)
        {
            try
            {
                p_function.Value = 4;

                pUsername.Value = users.username;
                pRoleID.Value =users.roleid;
                pAppId.Value = users.appid;
                pSection.Value = users.section;
                pCreatedBy.Value = users.CreatedBy;

                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UMUser", p_function,
                pUsername, pRoleID, pAppId, pSection, pCreatedBy);
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

        public bool EditUser(UMUser users)
        {
            try
            {
                p_function.Value = 6;

                pUsername.Value = users.username;
                pRoleID.Value = users.roleid;
                pAppId.Value = users.appid;
                pSection.Value = users.section;
                pModifiedBy.Value = users.CreatedBy;
                pId.Value = users.id;
                int result = SqlHelper.ExecuteNonQuery(General.UMconnString, CommandType.StoredProcedure, "SP_UMUser", p_function,
                pUsername, pRoleID, pAppId, pSection, pModifiedBy, pId);
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
        public bool GetUserDetail(ref UMUser users)
        {
            SqlDataReader item_dr;
            p_function.Value = 3;
            pId.Value = users.id;
            pUsername.Value = users.username;
            pAppId.Value = users.appid;
            item_dr = SqlHelper.ExecuteReader(General.UMconnString, CommandType.StoredProcedure, "SP_UMUser", p_function, pId, pUsername, pAppId);

            if (item_dr.HasRows)
            {
                item_dr.Read();

                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Username"))))
                {
                    users.username = item_dr["Username"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("Rolename"))))
                {
                    users.role = item_dr["Rolename"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("idrole"))))
                {
                    users.roleid = item_dr["idrole"].ToString();
                }
                if (!(item_dr.IsDBNull(item_dr.GetOrdinal("section"))))
                {
                    users.section = item_dr["section"].ToString();
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
        public void DeleteUser(UMUser users)
        {
            p_function.Value = 5;
            pId.Value = users.id;
            pModifiedBy.Value = users.ModifiedBy;
            SqlHelper.ExecuteNonQuery(General.connString, CommandType.StoredProcedure, "SP_Inventory", p_function, pId, pModifiedBy);
        }

        public DataSet ListRole()
        {
            p_function.Value = 7;
            pAppId.Value = "721e2b82-4cdc-4299-bbdb-fb1e7039b0b1";
            return SqlHelper.ExecuteDataset(General.UMconnString, CommandType.StoredProcedure, "SP_UMUser", p_function, pAppId);
        }
        #endregion
    }
}