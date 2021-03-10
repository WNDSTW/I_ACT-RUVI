using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using I_ACT.Domain;
using I_ACT.DAO;
namespace I_ACT.Pages
{
    public partial class Home : System.Web.UI.UserControl
    {
        Notulen notulens = new Notulen();
        NotulenDAO notulensDAO = new NotulenDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            notulensDAO.UpdateStatusOverdue();
        }
    }
}