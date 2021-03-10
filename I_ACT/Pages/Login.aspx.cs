using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Net;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
                           
using System.Web.Security;
using System.Web.Script.Serialization;
using I_ACT.Domain;
using I_ACT.DAO;
using System.Data;
using System.Data.SqlClient;

namespace I_ACT.Pages
{
    public partial class Login : Page
    {
        #region Deklarasi
        string displayname = "";
        string department = "";
        int vdepartmentid;
        string dominName = string.Empty;
        string adPath = string.Empty;
        string userName = string.Empty;
        string pass = string.Empty;
        string strError = string.Empty;
        string roles = string.Empty;

        private UMUserInRole users = new UMUserInRole();
        private UMUserInRoleDAO usersDAO = new UMUserInRoleDAO();

        private Log logs = new Log();
        private LogDAO logsDAO = new LogDAO();

        private Pekerja pekerjas = new Pekerja();
        private PekerjaDAO pekerjasDAO = new PekerjaDAO();

        DataTable dtUser = new DataTable();
        DataSet dsUser = new DataSet();
        DataTable dtPekerja = new DataTable();
        DataSet dsPekerja = new DataSet();
        bool loginSukses = false;
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            userName = txUsername.Value;
            pass = txPassword.Value;
            #region LDAP
            try
            {
                Session["db"] = "IACT";
                foreach (string key in ConfigurationManager.AppSettings.Keys)
                {
                    dominName = key.Contains("DirectoryDomain") ? ConfigurationManager.AppSettings[key] : dominName;
                    adPath = key.Contains("DirectoryPath") ? ConfigurationManager.AppSettings[key] : adPath;
                    if (!String.IsNullOrEmpty(dominName) && !String.IsNullOrEmpty(adPath))
                    {
                        if (true == AuthenticateUser(dominName, userName, pass, adPath, out strError))
                        {
                            dtUser.Clear();
                            dsUser.Clear();
                            users.username = userName;
                            dsUser = usersDAO.SearchUser(users);
                            dtUser = dsUser.Tables[0];
                            if (dtUser.Rows.Count > 0)
                            {
                                bool requested = (from DataRow dr in dtUser.Rows
                                                  select (bool)dr["requested"]).FirstOrDefault();

                                bool isbanned = (from DataRow dr in dtUser.Rows
                                                 select (bool)dr["isbanned"]).FirstOrDefault();
                                if (!requested && !isbanned)
                                {
                                    users.username = userName;
                                    Session["username"] = userName;
                                    Session["role"] = (from DataRow dr in dtUser.Rows
                                                       select (string)dr["Rolename"]).FirstOrDefault();
                                    Session["idrole"] = (from DataRow dr in dtUser.Rows
                                                         select (int)dr["idrole"]).FirstOrDefault();
                                    Session["section"] = (from DataRow dr in dtUser.Rows
                                                          select (string)dr["section"]).FirstOrDefault();



                                    Session["aplikasi"] = "IACT";
                                    Session["nama"] = displayname;
                                    Session["dept"] = department;
                                    roles = Session["role"].ToString();
                                    Session["username"] = userName;
                                     ambilDataPekerja();
                                    //Production
                                    //Session["role"] = "Originator";
                                    //Session["idrole"] = 2;
                                     //Session["aplikasi"] = "IACT_Pusat";
                                    //Session["nama"] = "Nana Kanan";
                                    //Session["nopek"] = "724831";
                                    //Session["NoLevel"] = "4";
                                    //Session["NoBagian"] = "39";
                                    //Session["NoFungsi"] = "11";
                                    //Session["username"] = "nana.kanan";
                                    //aktLog.createLog(Session["aplikasi"].ToString(), "Log In", "Login", "Login sebagai user yg sudah terdaftar", Session["username"].ToString(), DateTime.Now);

                                    //Session["username"] = "agung.darmawan";
                                    //Session["nama"] = "Agung Darmawan";
                                    //Session["nopek"] = "747563";
                                    //Session["role"] = "Originator";
                                    //Session["NoLevel"] = "4";
                                    //Session["NoBagian"] = "11";
                                    //Session["NoFungsi"] = "41";

                                    loginSukses = true;

                                }
                                else if (requested)
                                {
                                    lblLoginError.Text = "Your Account must be approved first, please contact IT admin";
                                }

                                else if (isbanned)
                                {
                                    lblLoginError.Text = "Your Account was inactive, please contact IT admin";
                                }
                            }
                            else
                            {
                                //userDomain.WrongLogin = 0;
                                users.username = userName;
                                users.CreatedBy = "System";
                                users.CreatedOn = DateTime.Now;
                                users.requested = false;
                                users.idRole = 2;
                                usersDAO.AddUser(users);

                                Session["username"] = userName;
                                Session["role"] = "Originator";
                                Session["idrole"] = 2;
                                ambilDataPekerja();


                                logs.aktifitas = "Login";
                                logs.deskripsi = "Login sebagai user baru";
                                logs.CreatedBy = Session["username"].ToString();
                                logs.menu = "Login";
                                logsDAO.AddLog(logs);

                                loginSukses = true;
                            }
                        }
                        else
                        {
                            
                            //OFLINE MODE (PRODUCTION MODE)
                            //Session["username"] = userName;
                            //Session["role"] = "Administrator";
                            //Session["idrole"] = 1;
                            //Session["aplikasi"] = "IACT_Pusat";
                            //Session["nama"] = "Rudi Hermawan Catur Putra";
                            //Session["nopek"] = "744735";
                            //Session["NoLevel"] = "4";
                            //Session["NoBagian"] = "58";
                            //Session["NoFungsi"] = "16";
                            //loginSukses = true;

                            //Session["username"] = "rosnamora";
                            //Session["nama"] = "Rosnamora Harahap";
                            //Session["nopek"] = "682355";
                            //Session["role"] = "Employee";
                            //Session["NoLevel"] = "4";
                            //Session["NoBagian"] = "59";
                            //Session["NoFungsi"] = "16";

                            ////PUBLISH MODE
                            lblLoginError.Text = "Invalid username or Password! ";
                            dominName = string.Empty;
                            adPath = string.Empty;
                            if (String.IsNullOrEmpty(strError)) break;

                        }
                    }


                }
                if (!string.IsNullOrEmpty(strError))
                {
                    lblLoginError.Text = strError;
                }

                if (loginSukses)
                {
                    logs.aktifitas = "Login";
                    logs.deskripsi = "Login Sebagai " + Session["role"].ToString();
                    logs.CreatedBy = Session["username"].ToString();
                    logs.menu = "Login";
                    logsDAO.AddLog(logs);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, txUsername.Value, DateTime.Now, DateTime.Now.AddMinutes(50), true, roles, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(txUsername.Value, false), false);
                    FormsAuthentication.SetAuthCookie(txUsername.Value, true);
                    //Response.Redirect("~/Traser.aspx?menu=Home", false);
                }
            }
            catch (Exception ex)
            {
                lblLoginError.Text = ex.Message;
            }
            finally
            {

            }
            #endregion

        }

        public void ambilDataPekerja()
        {
            pekerjas.email = userName + "@pertamina.com";
            dsPekerja = pekerjasDAO.SearchPekerja(pekerjas);
            dtPekerja = dsPekerja.Tables[0];
            if (dtPekerja.Rows.Count > 0)
            {
                Session["nopek"] = (from DataRow dr in dtPekerja.Rows
                                    select (string)dr["nopek"]).FirstOrDefault();
                Session["NoLevel"] = (from DataRow dr in dtPekerja.Rows
                                      select (Int32)dr["NoLevel"]).FirstOrDefault();
                //Session["NoBagian"] = (from DataRow dr in dtPekerja.Rows
                //                       select (Int32)dr["NoBagian"]).FirstOrDefault();
                //Session["NoFungsi"] = (from DataRow dr in dtPekerja.Rows
                //                       select (Int32)dr["NoFungsi"]).FirstOrDefault();
                Session["NoBagian"] = "0";
                Session["NoFungsi"] = "0";
            }
            else
            {
                Session["nopek"] = "0000000";
                Session["NoLevel"] = "5";
                Session["NoBagian"] = "0";
                Session["NoFungsi"] = "0";
                Session["username"]= userName;
                //Session["role"] = "Administrator";
                //Session["idrole"] = 1;
                //Session["aplikasi"] = "IACT_Pusat";
                //Session["nama"] = "Rudi";
                //Session["nopek"] = "748665";
                //Session["NoLevel"] = "4";
                //Session["NoBagian"] = "58";
                //Session["NoFungsi"] = "16";
            }
        }

        public bool AuthenticateUser(string domain, string username, string password, string LdapPath, out string Errmsg)
        {
            Errmsg = "";
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(LdapPath, domainAndUsername, password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("displayname");
                search.PropertiesToLoad.Add("title");
                search.PropertiesToLoad.Add("department");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
               
                    return true;
            }
            catch (Exception ex)
            {
                Errmsg = ex.Message;
                return false;
                throw new Exception("Error authenticating user.");
            }
        }

    }
    }
