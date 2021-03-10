using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using I_ACT.Domain;
using I_ACT.DAO;
using System.Data;
using System.Text;
using I_ACT.Pages;
namespace I_ACT.Pages
{
    public partial class MasterSumber : System.Web.UI.UserControl
    {
        #region Deklarasi
        //Base bases = new Base();
        Sumber sumbers = new Sumber();
        SumberDAO sumbersDAO = new SumberDAO();
        Rekomendasi rekomendasis = new Rekomendasi();
        RekomendasiDAO rekomendasisDAO = new RekomendasiDAO();
        StringBuilder sb = new StringBuilder();
        //Log logs = new Log();
        //LogDAO logsDAO = new LogDAO();
        DataSet ds;
        public DataTable dt;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //addPanel.Visible = false;
                //lblJudul.Text = "TAMBAH";
                bindData();
                bindDdl();
            }
        }

        #region button

        protected void txSearch_TextChanged(object sender, EventArgs e)
        {
            bindData();
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (validasiData())
                {
                    sumbers.idRekomendasi =int.Parse(ddlRekomendasi.SelectedValue.ToString());
                    sumbers.NamaSumber = txNama.Value;
                    sumbers.SingkatanSumber = txSingkatan.Value;
                    sumbers.CreatedBy = Session["username"].ToString();
                    if (ddlRAM.SelectedValue.ToString() == "0")
                    {
                        sumbers.isRAM = false;
                    }
                    else
                    {
                        sumbers.isRAM = true;
                    }
                 
                    sumbersDAO.AddSumber(sumbers);

                    //    logs.aktifitas = "Insert";
                    //    logs.deskripsi = "Insert Grup " + txNamaGrup.Text;
                    //    logs.CreatedBy = Session["username"].ToString();
                    //    logs.menu = "Master Grup";
                    //    logsDAO.AddLog(logs);

                    clearForm();
                    bindData();
                    ListPanel.Update();
                    ScriptManager.RegisterClientScriptBlock(addPanel, this.addPanel.GetType(), "showSuccess", "successMessage();", true);


                }
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(addPanel, this.addPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (validasiData())
                {
                    sumbers.idSumber =int.Parse(txId.Value);
                    sumbers.NamaSumber = txNama.Value;
                    sumbers.idRekomendasi =int.Parse(ddlRekomendasi.SelectedValue.ToString());
                    sumbers.SingkatanSumber = txSingkatan.Value;
                    sumbers.ModifiedBy = Session["username"].ToString();
                    if (ddlRAM.SelectedValue.ToString() == "0")
                    {
                        sumbers.isRAM = false;
                    }
                    else
                    {
                        sumbers.isRAM = true;
                    }
                    sumbersDAO.EditSumber(sumbers);

                    //    logs.aktifitas = "Insert";
                    //    logs.deskripsi = "Insert Grup " + txNamaGrup.Text;
                    //    logs.CreatedBy = Session["username"].ToString();
                    //    logs.menu = "Master Grup";
                    //    logsDAO.AddLog(logs);

                    clearForm();
                    bindData();
                    ListPanel.Update();
                    ScriptManager.RegisterClientScriptBlock(addPanel, this.addPanel.GetType(), "showSuccess", "updatedMessage();", true);


                }
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(addPanel, this.addPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindData();
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            lblJudul.Text = "ADD";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            ScriptManager.RegisterClientScriptBlock(addPanel, this.addPanel.GetType(), "showModal", " $('#modalAdd').modal('show');", true);
            addPanel.Update();
        }


        #endregion
        #region Gridview
        protected void dgObject_SortCommand(object source, DataGridSortCommandEventArgs e)
        {

            // Caches the current information
            String strSortBy = (String)ViewState["SortExpression"];
            String strSortAscending = (String)ViewState["SortAscending"];

            // Sets the new sort expression
            ViewState["SortExpression"] = e.SortExpression;

            // If you click on the sorted column, the order reverses
            if (e.SortExpression == strSortBy)
                ViewState["SortAscending"] = (strSortAscending == "yes"
                    ? "no" : "yes");
            else
                // Defaults to ascending order
                ViewState["SortAscending"] = "yes";

            bindData();
        }

        protected void dgObject_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                String strSortBy = (String)ViewState["SortExpression"];
                String strSortAscending = (String)ViewState["SortAscending"];
                String strOrder = (strSortAscending == "yes" ? "  <i class='fa fa-sort-alpha-asc fa-lg' aria-hidden='true'></i>" : " <i class='fa fa-sort-alpha-desc fa-lg' aria-hidden='true'></i>");

                for (int i = 0; i < dgObject.Columns.Count; i++)
                {
                    // Draw the glyph to reflect sorting
                    if (strSortBy == dgObject.Columns[i].SortExpression)
                    {
                        TableCell cell = e.Item.Cells[i];
                        Label lblSorted = new Label();
                        lblSorted.Font.Name = "webdings";
                        lblSorted.Font.Size = FontUnit.Small;
                        lblSorted.Attributes.Add("style", "float:right");
                        lblSorted.Text = strOrder;
                        cell.Controls.Add(lblSorted);
                    }
                }
            }
        }

        protected void dgObject_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgObject.CurrentPageIndex = e.NewPageIndex;
            bindData();
        }
        protected void dgObject_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                string code = "";
                if (e.Item.ItemIndex > -1)
                {
                    code = e.CommandArgument.ToString();
                    sumbers.idSumber = int.Parse(code);
                }

                if (e.CommandName.ToString() == "Edit")
                {
                    if (sumbersDAO.GetSumberDetail(ref sumbers))
                    {
                        lblJudul.Text = "EDIT";
                        txId.Value = code.ToString();
                        txNama.Value = sumbers.NamaSumber;
                        txSingkatan.Value = sumbers.SingkatanSumber;
                        if (sumbers.idRekomendasi != null)
                        {
                            ddlRekomendasi.SelectedValue = ddlRekomendasi.Items.FindByValue("" + sumbers.idRekomendasi + "").Value;
                        }
                        else
                        {
                            ddlRekomendasi.SelectedIndex = 0;
                        }
                        btnSave.Visible = false;
                        btnUpdate.Visible = true;
                        ScriptManager.RegisterClientScriptBlock(ListPanel, this.ListPanel.GetType(), "showModal", " $('#modalAdd').modal('show');", true);
                        ListPanel.Update();
                        addPanel.Update();
                    }
                }
                else if (e.CommandName.ToString() == "Delete")
                {
                    sumbers.idSumber = int.Parse(code);
                    sumbers.ModifiedBy = Session["username"].ToString();
                    sumbersDAO.DeleteSumber(sumbers);

                    //logs.aktifitas = "Delete";
                    //logs.deskripsi = "Delete Grup " + txNamaGrup.Text;
                    //logs.CreatedBy = Session["username"].ToString();
                    //logs.menu = "Delete Grup";
                    //logsDAO.AddLog(logs);

                    if ((dgObject.Items.Count % dgObject.PageSize) == 1 & dgObject.CurrentPageIndex != 0)
                        dgObject.CurrentPageIndex = dgObject.PageCount - 2;

                    bindData();
                    ListPanel.Update();
                }
            }
            catch (Exception ex)
            {
                string eror = ex.ToString();
                ScriptManager.RegisterClientScriptBlock(addPanel, this.addPanel.GetType(), "showError", "errorMessage(" + eror + ");", true);

            }
        }
        protected void dgObject_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton BtnEdit = (LinkButton)e.Item.FindControl("BtnEdit");
                LinkButton BtnDelete = (LinkButton)e.Item.FindControl("BtnDelete");

                //var dataRow = dtMatrix.AsEnumerable().
                //    Where(x => x.Field<string>("initial") == "edit" &&
                //    x.Field<int>("menuid") == Int32.Parse(Request.QueryString["Id"]));
                //if (dataRow.Count() > 0)
                //{
                //    BtnEdit.Visible = true;
                //}
                //else
                //{
                //    BtnEdit.Visible = false;
                //}

                //dataRow = dtMatrix.AsEnumerable().
                //    Where(x => x.Field<string>("initial") == "delete" &&
                //    x.Field<int>("menuid") == Int32.Parse(Request.QueryString["Id"]));
                //if (dataRow.Count() > 0)
                //{
                //    BtnDelete.Visible = true;
                //}
                //else
                //{
                //    BtnDelete.Visible = false;
                //}
            }
        }

        #endregion
        #region Function

        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected bool validasiData()
        {
            int errCount=0;
            if (ddlRekomendasi.SelectedIndex == 0)
            {
                errCount++;
            }

            if (txNama.Value == "")
            {
                errCount++;
            }
            if (txSingkatan.Value == "") {
                errCount++;
            }

            if (errCount > 0) {
                 return false;
            }
            else
            {
                return true;
            }
        }
        protected void bindData()
        {
            
            //grups.NamaGrup = null;
            if (txSearch.Text == "")
            {
                DataSet ds = (DataSet)Session["MyDataSet"];
                DataView dv;
                ds = sumbersDAO.ListSumber();
                dv = ds.Tables[0].DefaultView;
                dv.Sort = (String)ViewState["SortExpression"];
                if (ViewState["SortAscending"] == "no")
                    dv.Sort += " DESC";

                // Rebind data 
                dgObject.DataSource = dv;
                dgObject.DataBind();
            }
            else
            {
                DataSet ds = (DataSet)Session["MyDataSet"];
                DataView dv;
                ds = sumbersDAO.SearchSumber(txSearch.Text);
                dv = ds.Tables[0].DefaultView;
                dv.Sort = (String)ViewState["SortExpression"];
                if (ViewState["SortAscending"] == "no")
                    dv.Sort += " DESC";

                // Rebind data 
                dgObject.DataSource = dv;

                if (dgObject.CurrentPageIndex >= 0)
                {
                    dgObject.CurrentPageIndex = 0;
                }
                dgObject.DataBind();
            }

            //ddlFungsi.DataSource = fungsisDAO.ListFungsi();
            //ddlFungsi.DataTextField = "Fungsi";
            //ddlFungsi.DataValueField = "NoFungsi";
            //ddlFungsi.DataBind();
            //ddlFungsi.Items.Insert(0, "-- Select Fungsi --");

            //ddlCostCenter.DataSource = costcentersDAO.ListCostCenterDdl();
            //ddlCostCenter.DataTextField = "CostCenter";
            //ddlCostCenter.DataValueField = "CostCenterID";
            //ddlCostCenter.DataBind();
            //ddlCostCenter.Items.Insert(0, "-- Select CostCenter --");
        }

        protected void bindDdl()
        {
            ddlRekomendasi.DataSource = rekomendasisDAO.ListRekomendasi();
            ddlRekomendasi.DataTextField = "NamaRekomendasi";
            ddlRekomendasi.DataValueField = "IdRekomendasi";
            ddlRekomendasi.DataBind();
            ddlRekomendasi.Items.Insert(0, "-- Select Recommendation --");
        }
        protected void clearForm()
        {
            txId.Value = "";
            txNama.Value = "";
            txSingkatan.Value = "";
            ddlRekomendasi.SelectedIndex = 0;
        }

        #endregion

       

    }
}