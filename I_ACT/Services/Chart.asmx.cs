using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using I_ACT.DAO;
using I_ACT.Domain;
using System.Data;
namespace I_ACT.Services
{
    /// <summary>
    /// Summary description for Chart
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Chart : System.Web.Services.WebService
    {
        NotulenDAO notulensDAO = new NotulenDAO();
        Notulen notulens = new Notulen();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public class chartSummary
        {
            public string label { get; set; }
            public string value { get; set; }
            public string color { get; set; }
            public string hightlight { get; set; }
        }

        [WebMethod]
        public List<chartSummary> getchartSummaryData(List<string> gData)
        {
            List<chartSummary> t = new List<chartSummary>();
            string[] arrColor = new string[] { "#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775", "#5A69A6", "#9400D3", "#DAA520" };

            //DataSet ds1= new DataSet();
            //DataTable dt1 = new DataTable();
            //ds1 = notulensDAO.ChartSummary();
            //dt1 = ds1.Tables[0];
            //int counter = 0;
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    chartSummary sData = new chartSummary();
            //    sData.value = dr["Total"].ToString();
            //    sData.label = dr["namasumber"].ToString();
            //    sData.color = arrColor[counter];
            //    t.Add(sData);
            //    counter++;
            //}
            return t;
        }
    }
}
