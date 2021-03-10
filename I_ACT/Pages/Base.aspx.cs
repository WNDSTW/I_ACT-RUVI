using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace I_ACT.Pages
{
    public partial class Base : System.Web.UI.Page
    {
        UserControl ucbodypage = null;
        string bodypage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplaySessionValue();
            //Session["username"] = "subantarjo";
            //Session["nama"] = "Subantarjo";
            //Session["role"] = "Employee";
            //Session["nopek"] = "677285";
            //Session["NoLevel"] = "4";
            //Session["NoBagian"] = "51";
            //Session["NoFungsi"] = "14";

            //Session["username"] = "rianto.hardani";
            //Session["nama"] = "Rianto Rachmawan Hardani";
            //Session["role"] = "Employee";
            //Session["nopek"] = "752145";
            //Session["NoLevel"] = "5";
            //Session["NoBagian"] = "51";
            //Session["NoFungsi"] = "14";

            //Session["username"] = "rudi.putra";
            //Session["nama"] = "Rudi Hermawan Catur Putra";
            //Session["nopek"] = "748665";
            //Session["role"] = "Conceptor";
            //Session["NoLevel"] = "5";
            //Session["NoBagian"] = "41";
            //Session["NoFungsi"] = "11";

            //
            //Session["nama"] = "Rosnamora Harahap";
            //Session["username"] = "rosnamora";
            //Session["nopek"] = "682355";
            //Session["role"] = "Employee";
            //Session["NoLevel"] = "4";
            //Session["NoBagian"] = "59";
            //Session["NoFungsi"] = "16";

            //Session["username"] = "Banu.pradipta";
            //Session["nama"] = "Banu Pradipta";
            //Session["nopek"] = "750279";
            //Session["role"] = "Employee";
            //Session["NoLevel"] = "5";
            //Session["NoBagian"] = "58";
            //Session["NoFungsi"] = "16";

            if (this.Page.RouteData.Values["Menu"] != null && this.Page.RouteData.Values["idMenu"] != null)
            {
                GetPages();
            }
            else
            {
                ucbodypage = (UserControl)LoadControl("Home.ascx");
                phbody.Controls.Add(ucbodypage);
            }
        }
        private void DisplaySessionValue()
        {
            if (Session["Username"] != null)
            {
                string sesi = Session["username"].ToString();
                string role = Session["role"].ToString();
            }
            else
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
        }
        private void GetPages()
        {
            bodypage = this.Page.RouteData.Values["Menu"].ToString();

            switch (bodypage)
            {

                case "Dashboard":
                    ucbodypage = (UserControl)LoadControl("Dashboard.ascx");
                    phbody.Controls.Add(ucbodypage);
                    Session["menu"] = "Dashboard";
                    //home.Attributes.Add("style", "background-color:#017ee9;color:#fff");
                    break;
                case "Recommendation":
                    ucbodypage = (UserControl)LoadControl("MasterRekomendasi.ascx");
                    phbody.Controls.Add(ucbodypage);
                    Session["menu"] = "Master Recommendation";
                    //masterData.Attributes.Add("style", "background-color:#017ee9;color:#fff");
                    break;
                case "Source":
                    ucbodypage = (UserControl)LoadControl("MasterSumber.ascx");
                    phbody.Controls.Add(ucbodypage);
                    Session["menu"] = "Master Source";
                    //masterData.Attributes.Add("style", "background-color:#017ee9;color:#fff");
                    break;
                case "ActTracking":
                    ucbodypage = (UserControl)LoadControl("MonitorTindakan.ascx");
                    phbody.Controls.Add(ucbodypage);
                    Session["menu"] = "Action Tracking";
                    //masterData.Attributes.Add("style", "background-color:#017ee9;color:#fff");
                    break;
                case "MyTask":
                    ucbodypage = (UserControl)LoadControl("Tugas.ascx");
                    phbody.Controls.Add(ucbodypage);
                    Session["menu"] = "My Task";
                    //masterData.Attributes.Add("style", "background-color:#017ee9;color:#fff");
                    break;
                case "MyDraft":
                    ucbodypage = (UserControl)LoadControl("DraftTindakan.ascx");
                    phbody.Controls.Add(ucbodypage);
                    Session["menu"] = "My Draft";
                    //masterData.Attributes.Add("style", "background-color:#017ee9;color:#fff");
                    break;
                case "Report":
                    ucbodypage = (UserControl)LoadControl("ReportAllTask.ascx");
                    phbody.Controls.Add(ucbodypage);
                    Session["menu"] = "My Report";
                    //masterData.Attributes.Add("style", "background-color:#017ee9;color:#fff");
                    break;
                case "Logout": 
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    Response.Redirect("~/Pages/Login.aspx");
                    break;
                default:
                    ucbodypage = (UserControl)LoadControl("Home.ascx");
                    phbody.Controls.Add(ucbodypage);
                    break;
            }
        }
    }
}