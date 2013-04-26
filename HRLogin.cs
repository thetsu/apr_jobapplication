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

public partial class HRLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void liHR_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Boolean IsAuthenticated = false;
        IsAuthenticated = CheckAuthentication(liHR.UserName, liHR.Password);
        e.Authenticated = IsAuthenticated;
        if (IsAuthenticated == true)
        {           
            Session["username"] = liHR.UserName;
            Session["password"] = liHR.Password;
            //Server.Transfer("RetrieveInfo.aspx");
            Response.Redirect("RetrieveInfo.aspx");
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
}
