using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class RetrieveInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("HRLogin.aspx");
        }
        else
        {
            Boolean IsAuthenticated = false;
            IsAuthenticated = CheckAuthentication(Session["username"].ToString(), Session["password"].ToString());
            //Session.Remove("username");
            //Session.Remove("password");

            if (IsAuthenticated == true)
            {
                string strConnection = ConfigurationManager.ConnectionStrings["CS_HumanResource"].ToString();
                SqlConnection sqlConnection = new SqlConnection(strConnection);

                sqlConnection.Open();

                //SqlDataAdapter daApplicantInfo = new SqlDataAdapter("SELECT * FROM JobApplication_Particulars ORDER BY Timestamp_Applied DESC", sqlConnection);
                SqlDataAdapter daApplicantInfo = new SqlDataAdapter("SELECT * FROM JobApplication_Particulars", sqlConnection);
                DataTable dtApplicantInfo = new DataTable();
                daApplicantInfo.Fill(dtApplicantInfo);
                gvApplicantInfo.DataSource = dtApplicantInfo;
                gvApplicantInfo.DataBind();

                sqlConnection.Close();
                //Page.DataBind();
            }
            else
            {
                Response.Redirect("HRLogin.aspx");
            }
        }
    }

    private bool CheckAuthentication(string UserName, string Password)
    {
        Boolean HasRow = false;

        string strConnection = ConfigurationManager.ConnectionStrings["CS_HumanResource"].ToString();
        SqlConnection sqlConnection = new SqlConnection(strConnection);

        sqlConnection.Open();

        SqlCommand cmdHRAccess = new SqlCommand("Select * From JobApplication_Access", sqlConnection);
        SqlDataReader drHRAccess = cmdHRAccess.ExecuteReader();
        while (drHRAccess.Read())
        {
            if ((UserName == drHRAccess["Emp_Id"].ToString()) && (Password == drHRAccess["Password"].ToString()))
            {
                HasRow = true;
            }
        }
        drHRAccess.Close();

        sqlConnection.Close();
        return HasRow;
    }

    protected void gvApplicantInfo_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            SortGridView(sortExpression, " DESC");
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            SortGridView(sortExpression, " ASC");
        }
    }

    private void SortGridView(string sortExpression, string direction)
    {
        DataTable dt = gvApplicantInfo.DataSource as DataTable;
        DataView dv = new DataView(dt);
        dv.Sort = sortExpression + direction;

        gvApplicantInfo.DataSource = dv;
        gvApplicantInfo.DataBind();
    }
   
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;
            return (SortDirection)ViewState["sortDirection"];
        }
        set
        {
            ViewState["sortDirection"] = value;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strConnection = ConfigurationManager.ConnectionStrings["CS_HumanResource"].ToString();
        SqlConnection sqlConnection = new SqlConnection(strConnection);

        sqlConnection.Open();

        string id = (txtID.Text == "") ? "" : txtID.Text;
        string position = (ddlPosition.Text == "All") ? "" : ddlPosition.Text;
        string name = (txtName.Text == "") ? "" : txtName.Text;
        string status = (ddlApplicationStatus.Text == "All") ? "" : ddlApplicationStatus.Text;
        string strSearch = "SELECT * FROM JobApplication_Particulars";
        string strSQL = "";

        if (id != "")
            strSQL = strSQL + AddOperatorAnd(strSQL) + "(Ja_Id = '" + id + "')";
        if (position != "")
            strSQL = strSQL + AddOperatorAnd(strSQL) + "(Position_Applied = '" + position + "')";
        if (name != "")
            strSQL = strSQL + AddOperatorAnd(strSQL) + "(Name = '" + name + "')";
        if (status != "")
            strSQL = strSQL + AddOperatorAnd(strSQL) + "(Application_Status = '" + status + "')";

        if (strSQL != "")
            strSearch = strSearch + " WHERE " + strSQL;

        strSearch = strSearch + " ORDER BY Timestamp_Applied DESC";

        SqlDataAdapter daSearch = new SqlDataAdapter(strSearch, sqlConnection);
        DataTable dtSearch = new DataTable();
        daSearch.Fill(dtSearch);
        gvApplicantInfo.DataSource = dtSearch;
        gvApplicantInfo.DataBind();

        if (dtSearch.Rows.Count > 0)
            lblMsg.Visible = false;
        else
            lblMsg.Visible = true;

        sqlConnection.Close();

    }

    private string AddOperatorAnd(string strSql)
    {
        if (strSql != "")
            return " AND ";
        else
            return "";
    }

    //protected void gvApplicantInfo_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvApplicantInfo.EditIndex = e.NewEditIndex;
    //    gvApplicantInfo.DataBind();
    //}

    //protected void gvApplicantInfo_RowCanceling(object sender, GridViewCancelEditEventArgs e)
    //{
    //    gvApplicantInfo.EditIndex = -1;
    //    gvApplicantInfo.DataBind();
    //}

    //protected void gvApplicantInfo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    gvApplicantInfo.DataBind();
    //    string id = ((HyperLink)gvApplicantInfo.Rows[e.RowIndex].Cells[0].Controls[0]).Text;
    //    //string status = ((TextBox)gvApplicantInfo.Rows[e.RowIndex].Cells[11].Controls[0]).Text;
    //    string status = ddlNewApplicationStatus.Text;
    //    string timestamp = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year + " " + DateTime.Now.ToShortTimeString();
    //    string username = Session["username"].ToString();

    //    UpdateApplicationStatus(id, status, timestamp, username);
    //    gvApplicantInfo.EditIndex = -1;
    //    gvApplicantInfo.DataBind();
    //}

    //private void UpdateApplicationStatus(string id, string status, string timestamp, string username)
    //{
    //    string strConnection = ConfigurationManager.ConnectionStrings["CS_HumanResource"].ToString();
    //    SqlConnection sqlConnection = new SqlConnection(strConnection);

    //    sqlConnection.Open();

    //    SqlCommand cmdUpdate = new SqlCommand("JobApplication_Update_ApplicationStatus", sqlConnection);
    //    cmdUpdate.Parameters.Add(new SqlParameter("@Ja_Id", id));
    //    cmdUpdate.Parameters.Add(new SqlParameter("@Timestamp_Update", timestamp));
    //    cmdUpdate.Parameters.Add(new SqlParameter("@Application_Status", status));
    //    cmdUpdate.Parameters.Add(new SqlParameter("@Author_Update", username));
    //    cmdUpdate.CommandType = CommandType.StoredProcedure;
    //    if (status != "Select Option")
    //        cmdUpdate.ExecuteNonQuery();

    //    sqlConnection.Close();
    //}

}
