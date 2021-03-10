using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 using System.IO;

namespace I_ACT.Services
{
    /// <summary>
    /// Summary description for Upload
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;

            try
            {
                HttpPostedFile postedFile = context.Request.Files["Filedata"];
                string savepath = "";
                string tempPath = "";
                //tempPath = System.Configuration.ConfigurationManager.AppSettings["FolderPath"];
                tempPath = "Doc";
                savepath = context.Server.MapPath(tempPath);
                string filename = postedFile.FileName;
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);

                postedFile.SaveAs(savepath + @"\" + filename);
                context.Response.Write(tempPath + "/" + filename);
                context.Response.StatusCode = 200;

            }

            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }

        }



        public bool IsReusable
        {

            get
            {

                return false;

            }

        }

    }
}