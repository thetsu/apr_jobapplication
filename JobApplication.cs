using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class JobApplication : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblDate.Text = DateTime.Now.ToLongDateString();

        string maxDOB = DateTime.Today.AddYears(-14).ToShortDateString();
        string minDOB = DateTime.Today.AddYears(-80).ToShortDateString();
        string maxEducation = DateTime.Today.ToShortDateString();
        string minEducation = DateTime.Today.AddYears(-60).ToShortDateString();
        string maxWorkExperience = DateTime.Today.ToShortDateString();
        string minWorkExperience = DateTime.Today.AddYears(-60).ToShortDateString();
        rvAvailableStartDate.MaximumValue = DateTime.Today.AddYears(5).ToShortDateString();
        rvAvailableStartDate.MinimumValue = DateTime.Today.ToShortDateString();
        rvAvailableEndDate.MaximumValue = DateTime.Today.AddYears(5).ToShortDateString();
        rvAvailableEndDate.MinimumValue = DateTime.Today.ToShortDateString();
        rvPRIssuedDate.MaximumValue = DateTime.Today.ToShortDateString();
        rvPRIssuedDate.MinimumValue = DateTime.Today.AddYears(-80).ToShortDateString();
        rvExpiryDate.MaximumValue = DateTime.Today.AddYears(5).ToShortDateString();
        rvExpiryDate.MinimumValue = DateTime.Today.ToShortDateString();
        rvDOB.MaximumValue = maxDOB;
        rvDOB.MinimumValue = minDOB;
        rvSpouseDOB.MaximumValue = maxDOB;
        rvSpouseDOB.MinimumValue = minDOB;
        rvFatherDOB.MaximumValue = maxDOB;
        rvFatherDOB.MinimumValue = minDOB;
        rvMotherDOB.MaximumValue = maxDOB;
        rvMotherDOB.MinimumValue = minDOB;
        rvNSDateBegan.MaximumValue = DateTime.Today.ToShortDateString();
        rvNSDateBegan.MinimumValue = DateTime.Today.AddYears(-80).ToShortDateString();
        rvNSDateCompletion.MaximumValue = DateTime.Today.ToShortDateString();
        rvNSDateCompletion.MinimumValue = DateTime.Today.AddYears(-80).ToShortDateString();
        rvNSNextDate.MaximumValue = DateTime.Today.AddYears(5).ToShortDateString();
        rvNSNextDate.MinimumValue = DateTime.Today.ToShortDateString();        
        rvStartDate1.MaximumValue = maxWorkExperience;
        rvStartDate1.MinimumValue = minWorkExperience;
        rvEndDate1.MaximumValue = DateTime.Today.AddYears(1).ToShortDateString();
        rvEndDate1.MinimumValue = minWorkExperience;
        rvStartDate2.MaximumValue = maxWorkExperience;
        rvStartDate2.MinimumValue = minWorkExperience;
        rvEndDate2.MaximumValue = maxWorkExperience;
        rvEndDate2.MinimumValue = minWorkExperience;
        rvStartDate3.MaximumValue = maxWorkExperience;
        rvStartDate3.MinimumValue = minWorkExperience;
        rvEndDate3.MaximumValue = maxWorkExperience;
        rvEndDate3.MinimumValue = minWorkExperience;
        rvStartDate4.MaximumValue = maxWorkExperience;
        rvStartDate4.MinimumValue = minWorkExperience;
        rvEndDate4.MaximumValue = maxWorkExperience;
        rvEndDate4.MinimumValue = minWorkExperience;
        rvStartDate5.MaximumValue = maxWorkExperience;
        rvStartDate5.MinimumValue = minWorkExperience;
        rvEndDate5.MaximumValue = maxWorkExperience;
        rvEndDate5.MinimumValue = minWorkExperience;
        rvAvailableDate.MaximumValue = DateTime.Today.AddYears(1).ToShortDateString();
        rvAvailableDate.MinimumValue = DateTime.Today.ToShortDateString();
        rvTCStartDate.MaximumValue = DateTime.Today.ToShortDateString();
        rvTCStartDate.MinimumValue = DateTime.Today.AddYears(-80).ToShortDateString();
        rvTCEndDate.MaximumValue = DateTime.Today.ToShortDateString();
        rvTCEndDate.MinimumValue = DateTime.Today.AddYears(-80).ToShortDateString();

        if (!IsPostBack)
        {
            divPersonalInformation.Visible = true;  //Set PersonalInfo by default
            divEducationQualification.Visible = false;
            divWorkExperience.Visible = false;
            divLanguage.Visible = false;
            divDeclaration.Visible = false;
            btnPersonalInformation.CssClass = "tab_current";
            btnEducationQualification.CssClass = "tab";
            btnWorkExperience.CssClass = "tab";
            btnLanguage.CssClass = "tab";
            btnDeclaration.CssClass = "tab";
            SetInitialChildrenRow();
            SetInitialSiblingRow();
            SetInitialEducationRow();
            SetInitialCourseRow();
            SetInitialCertificateRow();
            SetInitialITKnowledgeRow();
            SetInitialLanguageRow();
        }

        divPositionOthers.Visible = false;
        divAvailableDate.Visible = false;
        divPeriodAvailabe.Visible = false;
        divSpouseParticular.Visible = false;
        divNS.Visible = false;
        divInstruction.Visible = false;
        divWorkExperience1.Visible = false;
        divWorkExperience2.Visible = false;
        divWorkExperience3.Visible = false;
        divWorkExperience4.Visible = false;
        divWorkExperience5.Visible = false;

        ShowPositionRelatedInfo();
        ShowPeriodAvailableInfo();
        ShowNSInfo();        
        ShowRaceRelatedInfo();
        ShowMaritalStatusRelatedInfo();
        ShowCitizenshipRelatedInfo();
        ShowWorkExperienceInfo();
    }

    protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPositionRelatedInfo();        
    }

    protected void ddlPreferredJobType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowPeriodAvailableInfo();
    }

    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowNSInfo();        
    }

    protected void ddlRace_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowRaceRelatedInfo();
    }

    protected void ddlMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowMaritalStatusRelatedInfo();        
    }

    protected void ddlCitizenship_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowCitizenshipRelatedInfo();
    }

    protected void ddlTotalWorkExperience_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowWorkExperienceInfo();
    }

    private void ShowPositionRelatedInfo()
    {
        if (ddlPosition.Text == "Customer Care Officer")
        {
            lblEnableRotation.Visible = true;
            rdEnableRotation.Visible = true;
        }
        else
        {
            lblEnableRotation.Visible = false;
            rdEnableRotation.Visible = false;
        }

        if (ddlPosition.Text == "Others")
        {
            divPositionOthers.Visible = true;
        }
        else
        {
            divPositionOthers.Visible = false;
            txtPositionOthers.Text = "";
        }
    }
    
    private void ShowPeriodAvailableInfo()
    {
        if (ddlPreferredJobType.Text == "Part Time" || ddlPreferredJobType.Text == "Contract")
        {
            divPeriodAvailabe.Visible = true;
        }
        else
        {
            divPeriodAvailabe.Visible = false;
            txtAvailableStartDate.Text = "";
            txtAvailableEndDate.Text = "";
        }
        
        if (ddlPreferredJobType.Text == "Full Time")
        {
            divAvailableDate.Visible = true;
        }
        else
        {
            divAvailableDate.Visible = false;
            txtAvailableDate.Text = "";
        }
    }

    private void ShowNSInfo()
    {
        if ((ddlCitizenship.Text == "Singaporean" && ddlGender.Text == "Male")
            || (ddlCitizenship.Text == "Singapore PR" && ddlGender.Text == "Male"))
        {
            divNS.Visible = true;
        }
        else
            divNS.Visible = false;
    }

    private void ShowRaceRelatedInfo()
    {
        if (ddlRace.Text == "Others")
        {
            lblRaceOthers.Visible = true;
            txtRaceOthers.Visible = true;
        }
        else
        {
            lblRaceOthers.Visible = false;
            txtRaceOthers.Text = "";
            txtRaceOthers.Visible = false;
        }
    }

    private void ShowMaritalStatusRelatedInfo()
    {
        if (ddlMaritalStatus.Text == "Married")
        {
            divSpouseParticular.Visible = true;
        }
        else
        {
            divSpouseParticular.Visible = false;
        }
    }

    private void ShowCitizenshipRelatedInfo()
    {
        ShowNSInfo();
        if (ddlCitizenship.Text == "Singapore PR")
        {
            lblPRIssuedDate.Visible = true;
            txtPRIssuedDate.Visible = true;
            cldPRIssuedDate.Visible = true;
        }
        else
        {
            lblPRIssuedDate.Visible = false;
            txtPRIssuedDate.Text = "";
            txtPRIssuedDate.Visible = false;
            cldPRIssuedDate.Visible = false;
        }
    }

    private void ShowWorkExperienceInfo()
    {
        if (ddlTotalWorkExperience.Text == "Select Option" || ddlTotalWorkExperience.Text == "0")
        {
            chkCurrentWorking.Visible = false;
            chkCurrentWorking.Checked = false;
            lblCurrentWorking.Visible = false;
            divInstruction.Visible = false;
            divWorkExperience1.Visible = false;
            divWorkExperience2.Visible = false;
            divWorkExperience3.Visible = false;
            divWorkExperience4.Visible = false;
            divWorkExperience5.Visible = false;
        }
        else
        {
            chkCurrentWorking.Visible = true;
            lblCurrentWorking.Visible = true;
            divInstruction.Visible = true;
            if (ddlTotalWorkExperience.Text == "1")
            {
                divInstruction.Visible = false;
                divWorkExperience1.Visible = true;
                divWorkExperience2.Visible = false;
                divWorkExperience3.Visible = false;
                divWorkExperience4.Visible = false;
                divWorkExperience5.Visible = false;
            }
            else if (ddlTotalWorkExperience.Text == "2")
            {
                divWorkExperience1.Visible = true;
                divWorkExperience2.Visible = true;
                divWorkExperience3.Visible = false;
                divWorkExperience4.Visible = false;
                divWorkExperience5.Visible = false;
            }
            else if (ddlTotalWorkExperience.Text == "3")
            {
                divWorkExperience1.Visible = true;
                divWorkExperience2.Visible = true;
                divWorkExperience3.Visible = true;
                divWorkExperience4.Visible = false;
                divWorkExperience5.Visible = false;
            }
            else if (ddlTotalWorkExperience.Text == "4")
            {
                divWorkExperience1.Visible = true;
                divWorkExperience2.Visible = true;
                divWorkExperience3.Visible = true;
                divWorkExperience4.Visible = true;
                divWorkExperience5.Visible = false;
            }
            else
            {
                divWorkExperience1.Visible = true;
                divWorkExperience2.Visible = true;
                divWorkExperience3.Visible = true;
                divWorkExperience4.Visible = true;
                divWorkExperience5.Visible = true;
            }
        }
    }

    protected void btnPersonalInformation_Click(object sender, EventArgs e)
    {
        divPersonalInformation.Visible = true;
        divEducationQualification.Visible = false;
        divWorkExperience.Visible = false;
        divLanguage.Visible = false;
        divDeclaration.Visible = false;
        btnPersonalInformation.CssClass = "tab_current";
        btnEducationQualification.CssClass = "tab";
        btnWorkExperience.CssClass = "tab";
        btnLanguage.CssClass = "tab";
        btnDeclaration.CssClass = "tab";
    }

    private void ShowPersonalInformation()
    {
        divPersonalInformation.Visible = true;
        btnPersonalInformation.CssClass = "tab_current";
    }

    protected void btnEducationQualification_Click(object sender, EventArgs e)
    {
        divPersonalInformation.Visible = false;
        divEducationQualification.Visible = true;
        divWorkExperience.Visible = false;
        divLanguage.Visible = false;
        divDeclaration.Visible = false;
        btnPersonalInformation.CssClass = "tab";
        btnEducationQualification.CssClass = "tab_current";
        btnWorkExperience.CssClass = "tab";
        btnLanguage.CssClass = "tab";
        btnDeclaration.CssClass = "tab";
    }

    private void ShowEducationQualification()
    {
        divEducationQualification.Visible = true;
        btnEducationQualification.CssClass = "tab_current";
    }

    protected void btnWorkExperience_Click(object sender, EventArgs e)
    {
        divPersonalInformation.Visible = false;
        divEducationQualification.Visible = false;
        divWorkExperience.Visible = true;
        divLanguage.Visible = false;
        divDeclaration.Visible = false;
        btnPersonalInformation.CssClass = "tab";
        btnEducationQualification.CssClass = "tab";
        btnWorkExperience.CssClass = "tab_current";
        btnLanguage.CssClass = "tab";
        btnDeclaration.CssClass = "tab";
    }

    private void ShowWorkExperience()
    {
        divWorkExperience.Visible = true;
        btnWorkExperience.CssClass = "tab_current";
    }

    protected void btnLanguage_Click(object sender, EventArgs e)
    {
        divPersonalInformation.Visible = false;
        divEducationQualification.Visible = false;
        divWorkExperience.Visible = false;
        divLanguage.Visible = true;
        divDeclaration.Visible = false;
        btnPersonalInformation.CssClass = "tab";
        btnEducationQualification.CssClass = "tab";
        btnWorkExperience.CssClass = "tab";
        btnLanguage.CssClass = "tab_current";
        btnDeclaration.CssClass = "tab";
    }

    private void ShowLanguage()
    {
        divLanguage.Visible = true;
        btnLanguage.CssClass = "tab_current";
    }

    protected void btnDeclaration_Click(object sender, EventArgs e)
    {
        Boolean hasError = true;
        hasError = CheckLanguage();
        if (!hasError)
        {
            Boolean isAcceptable = false;
            isAcceptable = CheckLanguageProficiency();

            if (isAcceptable)
            {
                divPersonalInformation.Visible = false;
                divEducationQualification.Visible = false;
                divWorkExperience.Visible = false;
                divLanguage.Visible = false;
                divDeclaration.Visible = true;
                btnPersonalInformation.CssClass = "tab";
                btnEducationQualification.CssClass = "tab";
                btnWorkExperience.CssClass = "tab";
                btnLanguage.CssClass = "tab";
                btnDeclaration.CssClass = "tab_current";
            }
        }
    }

    private void ShowDeclaration()
    {
        divDeclaration.Visible = true;
        btnDeclaration.CssClass = "tab_current";
    }

    private void SetInitialChildrenRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dr = dt.NewRow();
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentChildrenTable"] = dt;

        gvChildren.DataSource = dt;
        gvChildren.DataBind();

    }

    protected void gvChildren_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_RowCreated("CurrentChildrenTable", "lbtnChildren_RowRemoved", e);
    }

    protected void btnChildren_RowAdded_Click(object sender, EventArgs e)
    {
        AddNewRowToChildrenGrid();
    }

    private void AddNewRowToChildrenGrid()
    {
        if (ViewState["CurrentChildrenTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentChildrenTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                ExtractChildren(dtCurrentTable, "add");
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        ShowPersonalInformation();
        //Set Previous Data on Postbacks
        SetPreviousChildrenData("add");
    }

    protected void lbtnChildren_RowRemoved_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentChildrenTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentChildrenTable"];
            if (dt.Rows.Count > 1)
            {
                ExtractChildren(dt, "remove");

                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[gvRow.RowIndex]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentChildrenTable"] = dt;
            //Re bind the GridView for the updated data
            gvChildren.DataSource = dt;
            gvChildren.DataBind();
        }

        ShowPersonalInformation();
        //Set Previous Data on Postbacks
        SetPreviousChildrenData("remove");
    }

    private void ExtractChildren(DataTable dt, string status)
    {
        int rowIndex = 0;
        DataRow dr = null;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            //extract the values
            DropDownList ddl1 = (DropDownList)gvChildren.Rows[rowIndex].Cells[0].FindControl("ddlChildren");
            TextBox text1 = (TextBox)gvChildren.Rows[rowIndex].Cells[1].FindControl("txtChildrenName");
            TextBox text2 = (TextBox)gvChildren.Rows[rowIndex].Cells[2].FindControl("txtChildrenDOB");
            TextBox text3 = (TextBox)gvChildren.Rows[rowIndex].Cells[3].FindControl("txtChildrenEmployer");
            TextBox text4 = (TextBox)gvChildren.Rows[rowIndex].Cells[4].FindControl("txtChildrenOccupation");

            if (status == "add")
            {
                dr = dt.NewRow();
            }

            dt.Rows[i - 1]["Column1"] = ddl1.SelectedItem.Text;
            dt.Rows[i - 1]["Column2"] = text1.Text;
            dt.Rows[i - 1]["Column3"] = text2.Text;
            dt.Rows[i - 1]["Column4"] = text3.Text;
            dt.Rows[i - 1]["Column5"] = text4.Text;

            rowIndex++;
        }
        if (status == "add")
        {
            dt.Rows.Add(dr);
            //Store the current data in ViewState for future reference
            ViewState["CurrentChildrenTable"] = dt;
            //Re bind the GridView for the updated data
            gvChildren.DataSource = dt;
            gvChildren.DataBind();
        }

    }

    private void SetPreviousChildrenData(string status)
    {
        int rowIndex = 0;
        if (ViewState["CurrentChildrenTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentChildrenTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList ddl1 = (DropDownList)gvChildren.Rows[rowIndex].Cells[0].FindControl("ddlChildren");
                    TextBox text1 = (TextBox)gvChildren.Rows[rowIndex].Cells[1].FindControl("txtChildrenName");
                    TextBox text2 = (TextBox)gvChildren.Rows[rowIndex].Cells[2].FindControl("txtChildrenDOB");
                    TextBox text3 = (TextBox)gvChildren.Rows[rowIndex].Cells[3].FindControl("txtChildrenEmployer");
                    TextBox text4 = (TextBox)gvChildren.Rows[rowIndex].Cells[4].FindControl("txtChildrenOccupation");

                    text1.Text = dt.Rows[i]["Column2"].ToString();
                    text2.Text = dt.Rows[i]["Column3"].ToString();
                    text3.Text = dt.Rows[i]["Column4"].ToString();
                    text4.Text = dt.Rows[i]["Column5"].ToString();

                    if (status == "add")
                    {
                        if (i < dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                        }
                    }
                    else if (status == "remove")
                    {
                        if (i <= dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                        }
                    }
                    rowIndex++;
                }
            }
        }
    }

    private void SetInitialSiblingRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dr = dt.NewRow();
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentSiblingTable"] = dt;

        gvSibling.DataSource = dt;
        gvSibling.DataBind();

    }

    protected void gvSibling_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_RowCreated("CurrentSiblingTable", "lbtnSibling_RowRemoved", e);
    }

    protected void btnSibling_RowAdded_Click(object sender, EventArgs e)
    {
        AddNewRowToSiblingGrid();
    }

    private void AddNewRowToSiblingGrid()
    {

        if (ViewState["CurrentSiblingTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentSiblingTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                ExtractSibling(dtCurrentTable, "add");
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        ShowPersonalInformation();
        //Set Previous Data on Postbacks
        SetPreviousSiblingData("add");
    }

    protected void lbtnSibling_RowRemoved_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentSiblingTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentSiblingTable"];
            if (dt.Rows.Count > 1)
            {
                ExtractSibling(dt, "remove");

                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[gvRow.RowIndex]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentSiblingTable"] = dt;
            //Re bind the GridView for the updated data
            gvSibling.DataSource = dt;
            gvSibling.DataBind();
        }

        ShowPersonalInformation();
        //Set Previous Data on Postbacks
        SetPreviousSiblingData("remove");
    }

    private void ExtractSibling(DataTable dt, string status)
    {
        int rowIndex = 0;
        DataRow dr = null;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            //extract the values
            DropDownList ddl1 = (DropDownList)gvSibling.Rows[rowIndex].Cells[0].FindControl("ddlSibling");
            TextBox text1 = (TextBox)gvSibling.Rows[rowIndex].Cells[1].FindControl("txtSiblingName");
            TextBox text2 = (TextBox)gvSibling.Rows[rowIndex].Cells[2].FindControl("txtSiblingDOB");
            TextBox text3 = (TextBox)gvSibling.Rows[rowIndex].Cells[3].FindControl("txtSiblingEmployer");
            TextBox text4 = (TextBox)gvSibling.Rows[rowIndex].Cells[4].FindControl("txtSiblingOccupation");

            if (status == "add")
            {
                dr = dt.NewRow();
            }

            dt.Rows[i - 1]["Column1"] = ddl1.SelectedItem.Text;
            dt.Rows[i - 1]["Column2"] = text1.Text;
            dt.Rows[i - 1]["Column3"] = text2.Text;
            dt.Rows[i - 1]["Column4"] = text3.Text;
            dt.Rows[i - 1]["Column5"] = text4.Text;

            rowIndex++;
        }
        if (status == "add")
        {
            dt.Rows.Add(dr);
            //Store the current data in ViewState for future reference
            ViewState["CurrentSiblingTable"] = dt;
            //Re bind the GridView for the updated data
            gvSibling.DataSource = dt;
            gvSibling.DataBind();
        }
    }

    private void SetPreviousSiblingData(string status)
    {
        int rowIndex = 0;
        if (ViewState["CurrentSiblingTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentSiblingTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList ddl1 = (DropDownList)gvSibling.Rows[rowIndex].Cells[0].FindControl("ddlSibling");
                    TextBox text1 = (TextBox)gvSibling.Rows[rowIndex].Cells[1].FindControl("txtSiblingName");
                    TextBox text2 = (TextBox)gvSibling.Rows[rowIndex].Cells[2].FindControl("txtSiblingDOB");
                    TextBox text3 = (TextBox)gvSibling.Rows[rowIndex].Cells[3].FindControl("txtSiblingEmployer");
                    TextBox text4 = (TextBox)gvSibling.Rows[rowIndex].Cells[4].FindControl("txtSiblingOccupation");

                    text1.Text = dt.Rows[i]["Column2"].ToString();
                    text2.Text = dt.Rows[i]["Column3"].ToString();
                    text3.Text = dt.Rows[i]["Column4"].ToString();
                    text4.Text = dt.Rows[i]["Column5"].ToString();

                    if (status == "add")
                    {
                        if (i < dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                        }
                    }
                    else if (status == "remove")
                    {
                        if (i <= dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                        }
                    }
                    rowIndex++;
                }
            }
        }
    }

    private void SetInitialEducationRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentEducationTable"] = dt;

        gvEducation.DataSource = dt;
        gvEducation.DataBind();

    }

    protected void gvEducation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_RowCreated("CurrentEducationTable", "lbtnEducation_RowRemoved", e);
    }

    protected void btnEducation_RowAdded_Click(object sender, EventArgs e)
    {
        AddNewRowToEducationGrid();
    }

    private void AddNewRowToEducationGrid()
    {
        if (ViewState["CurrentEducationTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentEducationTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                ExtractEducation(dtCurrentTable, "add");
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousEducationData("add");
    }

    protected void lbtnEducation_RowRemoved_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentEducationTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentEducationTable"];
            if (dt.Rows.Count > 1)
            {
                ExtractEducation(dt, "remove");

                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[gvRow.RowIndex]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentEducationTable"] = dt;
            //Re bind the GridView for the updated data
            gvEducation.DataSource = dt;
            gvEducation.DataBind();
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousEducationData("remove");
    }

    private void ExtractEducation(DataTable dt, string status)
    {
        int rowIndex = 0;
        DataRow dr = null;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            //extract the values
            TextBox text1 = (TextBox)gvEducation.Rows[rowIndex].Cells[0].FindControl("txtEducationAttained");
            TextBox text2 = (TextBox)gvEducation.Rows[rowIndex].Cells[1].FindControl("txtEducationSchoolName");
            TextBox text3 = (TextBox)gvEducation.Rows[rowIndex].Cells[2].FindControl("txtEducationStartDate");
            TextBox text4 = (TextBox)gvEducation.Rows[rowIndex].Cells[3].FindControl("txtEducationEndDate");
            TextBox text5 = (TextBox)gvEducation.Rows[rowIndex].Cells[4].FindControl("txtEducationMajor");

            if (status == "add")
            {
                dr = dt.NewRow();
            }

            dt.Rows[i - 1]["Column1"] = text1.Text;
            dt.Rows[i - 1]["Column2"] = text2.Text;
            dt.Rows[i - 1]["Column3"] = text3.Text;
            dt.Rows[i - 1]["Column4"] = text4.Text;
            dt.Rows[i - 1]["Column5"] = text5.Text;

            rowIndex++;
        }
        if (status == "add")
        {
            dt.Rows.Add(dr);
            //Store the current data in ViewState for future reference
            ViewState["CurrentEducationTable"] = dt;
            //Re bind the GridView for the updated data
            gvEducation.DataSource = dt;
            gvEducation.DataBind();
        }

    }

    private void SetPreviousEducationData(string status)
    {
        int rowIndex = 0;
        if (ViewState["CurrentEducationTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentEducationTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox text1 = (TextBox)gvEducation.Rows[rowIndex].Cells[0].FindControl("txtEducationAttained");
                    TextBox text2 = (TextBox)gvEducation.Rows[rowIndex].Cells[1].FindControl("txtEducationSchoolName");
                    TextBox text3 = (TextBox)gvEducation.Rows[rowIndex].Cells[2].FindControl("txtEducationStartDate");
                    TextBox text4 = (TextBox)gvEducation.Rows[rowIndex].Cells[3].FindControl("txtEducationEndDate");
                    TextBox text5 = (TextBox)gvEducation.Rows[rowIndex].Cells[4].FindControl("txtEducationMajor");

                    text1.Text = dt.Rows[i]["Column1"].ToString();
                    text2.Text = dt.Rows[i]["Column2"].ToString();
                    text3.Text = dt.Rows[i]["Column3"].ToString();
                    text4.Text = dt.Rows[i]["Column4"].ToString();
                    text5.Text = dt.Rows[i]["Column5"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetInitialCourseRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentCourseTable"] = dt;

        gvCourse.DataSource = dt;
        gvCourse.DataBind();

    }

    protected void gvCourse_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_RowCreated("CurrentCourseTable", "lbtnCourse_RowRemoved", e);
    }

    protected void btnCourse_RowAdded_Click(object sender, EventArgs e)
    {
        AddNewRowToCourseGrid();
    }

    private void AddNewRowToCourseGrid()
    {
        if (ViewState["CurrentCourseTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentCourseTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                ExtractCourse(dtCurrentTable, "add");
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousCourseData("add");
    }

    protected void lbtnCourse_RowRemoved_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentCourseTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentCourseTable"];
            if (dt.Rows.Count > 1)
            {
                ExtractCourse(dt, "remove");

                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[gvRow.RowIndex]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentCourseTable"] = dt;
            //Re bind the GridView for the updated data
            gvCourse.DataSource = dt;
            gvCourse.DataBind();
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousCourseData("remove");
    }

    private void ExtractCourse(DataTable dt, string status)
    {
        int rowIndex = 0;
        DataRow dr = null;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            //extract the values
            TextBox text1 = (TextBox)gvCourse.Rows[rowIndex].Cells[0].FindControl("txtCourseSchoolName");
            TextBox text2 = (TextBox)gvCourse.Rows[rowIndex].Cells[1].FindControl("txtCourseTitle");
            TextBox text3 = (TextBox)gvCourse.Rows[rowIndex].Cells[2].FindControl("txtCourseStartDate");
            TextBox text4 = (TextBox)gvCourse.Rows[rowIndex].Cells[3].FindControl("txtCourseEndDate");

            if (status == "add")
            {
                dr = dt.NewRow();
            }

            dt.Rows[i - 1]["Column1"] = text1.Text;
            dt.Rows[i - 1]["Column2"] = text2.Text;
            dt.Rows[i - 1]["Column3"] = text3.Text;
            dt.Rows[i - 1]["Column4"] = text4.Text;

            rowIndex++;
        }
        if (status == "add")
        {
            dt.Rows.Add(dr);
            //Store the current data in ViewState for future reference
            ViewState["CurrentCourseTable"] = dt;
            //Re bind the GridView for the updated data
            gvCourse.DataSource = dt;
            gvCourse.DataBind();
        }

    }

    private void SetPreviousCourseData(string status)
    {
        int rowIndex = 0;
        if (ViewState["CurrentCourseTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentCourseTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox text1 = (TextBox)gvCourse.Rows[rowIndex].Cells[0].FindControl("txtCourseSchoolName");
                    TextBox text2 = (TextBox)gvCourse.Rows[rowIndex].Cells[1].FindControl("txtCourseTitle");
                    TextBox text3 = (TextBox)gvCourse.Rows[rowIndex].Cells[2].FindControl("txtCourseStartDate");
                    TextBox text4 = (TextBox)gvCourse.Rows[rowIndex].Cells[3].FindControl("txtCourseEndDate");

                    text1.Text = dt.Rows[i]["Column1"].ToString();
                    text2.Text = dt.Rows[i]["Column2"].ToString();
                    text3.Text = dt.Rows[i]["Column3"].ToString();
                    text4.Text = dt.Rows[i]["Column4"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetInitialCertificateRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dt.Columns.Add(new DataColumn("Column5", typeof(string)));
        dt.Columns.Add(new DataColumn("Column6", typeof(string)));
        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dr["Column2"] = string.Empty;
        dr["Column3"] = string.Empty;
        dr["Column4"] = string.Empty;
        dr["Column5"] = string.Empty;
        dr["Column6"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentCertificateTable"] = dt;

        gvCertificate.DataSource = dt;
        gvCertificate.DataBind();

    }

    protected void gvCertificate_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_RowCreated("CurrentCertificateTable", "lbtnCertificate_RowRemoved", e);
    }

    protected void btnCertificate_RowAdded_Click(object sender, EventArgs e)
    {
        AddNewRowToCertificateGrid();
    }

    private void AddNewRowToCertificateGrid()
    {
        if (ViewState["CurrentCertificateTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentCertificateTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                ExtractCertificate(dtCurrentTable, "add");
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousCertificateData("add");
    }

    protected void lbtnCertificate_RowRemoved_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentCertificateTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentCertificateTable"];
            if (dt.Rows.Count > 1)
            {
                ExtractCertificate(dt, "remove");

                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[gvRow.RowIndex]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentCertificateTable"] = dt;
            //Re bind the GridView for the updated data
            gvCertificate.DataSource = dt;
            gvCertificate.DataBind();
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousCertificateData("remove");
    }

    private void ExtractCertificate(DataTable dt, string status)
    {
        int rowIndex = 0;
        DataRow dr = null;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            //extract the values
            TextBox text1 = (TextBox)gvCertificate.Rows[rowIndex].Cells[0].FindControl("txtCertificateSchoolName");
            TextBox text2 = (TextBox)gvCertificate.Rows[rowIndex].Cells[1].FindControl("txtCertificateTitle");
            TextBox text3 = (TextBox)gvCertificate.Rows[rowIndex].Cells[2].FindControl("txtCertificateStartDate");
            TextBox text4 = (TextBox)gvCertificate.Rows[rowIndex].Cells[3].FindControl("txtCertificateEndDate");
            TextBox text5 = (TextBox)gvCertificate.Rows[rowIndex].Cells[4].FindControl("txtCertificateDate");
            TextBox text6 = (TextBox)gvCertificate.Rows[rowIndex].Cells[5].FindControl("txtCertificateEarned");

            if (status == "add")
            {
                dr = dt.NewRow();
            }

            dt.Rows[i - 1]["Column1"] = text1.Text;
            dt.Rows[i - 1]["Column2"] = text2.Text;
            dt.Rows[i - 1]["Column3"] = text3.Text;
            dt.Rows[i - 1]["Column4"] = text4.Text;
            dt.Rows[i - 1]["Column5"] = text5.Text;
            dt.Rows[i - 1]["Column6"] = text6.Text;

            rowIndex++;
        }
        if (status == "add")
        {
            dt.Rows.Add(dr);
            //Store the current data in ViewState for future reference
            ViewState["CurrentCertificateTable"] = dt;
            //Re bind the GridView for the updated data
            gvCertificate.DataSource = dt;
            gvCertificate.DataBind();
        }

    }

    private void SetPreviousCertificateData(string status)
    {
        int rowIndex = 0;
        if (ViewState["CurrentCertificateTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentCertificateTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox text1 = (TextBox)gvCertificate.Rows[rowIndex].Cells[0].FindControl("txtCertificateSchoolName");
                    TextBox text2 = (TextBox)gvCertificate.Rows[rowIndex].Cells[1].FindControl("txtCertificateTitle");
                    TextBox text3 = (TextBox)gvCertificate.Rows[rowIndex].Cells[2].FindControl("txtCertificateStartDate");
                    TextBox text4 = (TextBox)gvCertificate.Rows[rowIndex].Cells[3].FindControl("txtCertificateEndDate");
                    TextBox text5 = (TextBox)gvCertificate.Rows[rowIndex].Cells[4].FindControl("txtCertificateDate");
                    TextBox text6 = (TextBox)gvCertificate.Rows[rowIndex].Cells[5].FindControl("txtCertificateEarned");

                    text1.Text = dt.Rows[i]["Column1"].ToString();
                    text2.Text = dt.Rows[i]["Column2"].ToString();
                    text3.Text = dt.Rows[i]["Column3"].ToString();
                    text4.Text = dt.Rows[i]["Column4"].ToString();
                    text5.Text = dt.Rows[i]["Column5"].ToString();
                    text6.Text = dt.Rows[i]["Column6"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    private void SetInitialITKnowledgeRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dr = dt.NewRow();

        dr["Column1"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentITKnowledgeTable"] = dt;

        gvITKnowledge.DataSource = dt;
        gvITKnowledge.DataBind();

    }

    protected void gvITKnowledge_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_RowCreated("CurrentITKnowledgeTable", "lbtnITKnowledge_RowRemoved", e);
    }

    protected void btnITKnowledge_RowAdded_Click(object sender, EventArgs e)
    {
        AddNewRowToITKnowledgeGrid();
    }

    private void AddNewRowToITKnowledgeGrid()
    {
        if (ViewState["CurrentITKnowledgeTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentITKnowledgeTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                ExtractITKnowledge(dtCurrentTable, "add");
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousITKnowledgeData("add");
    }

    protected void lbtnITKnowledge_RowRemoved_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentITKnowledgeTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentITKnowledgeTable"];
            if (dt.Rows.Count > 1)
            {
                ExtractITKnowledge(dt, "remove");

                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[gvRow.RowIndex]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentITKnowledgeTable"] = dt;
            //Re bind the GridView for the updated data
            gvITKnowledge.DataSource = dt;
            gvITKnowledge.DataBind();
        }

        ShowEducationQualification();
        //Set Previous Data on Postbacks
        SetPreviousITKnowledgeData("remove");
    }

    private void ExtractITKnowledge(DataTable dt, string status)
    {
        int rowIndex = 0;
        DataRow dr = null;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            //extract the values
            TextBox text1 = (TextBox)gvITKnowledge.Rows[rowIndex].Cells[0].FindControl("txtSoftwareName");
            DropDownList ddl1 = (DropDownList)gvITKnowledge.Rows[rowIndex].Cells[1].FindControl("ddlITKnowledge");

            if (status == "add")
            {
                dr = dt.NewRow();
            }

            dt.Rows[i - 1]["Column1"] = text1.Text;
            dt.Rows[i - 1]["Column2"] = ddl1.SelectedItem.Text;

            rowIndex++;
        }
        if (status == "add")
        {
            dt.Rows.Add(dr);
            //Store the current data in ViewState for future reference
            ViewState["CurrentITKnowledgeTable"] = dt;
            //Re bind the GridView for the updated data
            gvITKnowledge.DataSource = dt;
            gvITKnowledge.DataBind();
        }

    }

    private void SetPreviousITKnowledgeData(string status)
    {
        int rowIndex = 0;
        if (ViewState["CurrentITKnowledgeTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentITKnowledgeTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox text1 = (TextBox)gvITKnowledge.Rows[rowIndex].Cells[0].FindControl("txtSoftwareName");
                    DropDownList ddl1 = (DropDownList)gvITKnowledge.Rows[rowIndex].Cells[1].FindControl("ddlITKnowledge");

                    text1.Text = dt.Rows[i]["Column1"].ToString();

                    if (status == "add")
                    {
                        if (i < dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column2"].ToString()).Selected = true;
                        }
                    }
                    else if (status == "remove")
                    {
                        if (i <= dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column2"].ToString()).Selected = true;
                        }
                    }
                    rowIndex++;
                }
            }
        }
    }

    private void SetInitialLanguageRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dt.Columns.Add(new DataColumn("Column2", typeof(string)));
        dt.Columns.Add(new DataColumn("Column3", typeof(string)));
        dt.Columns.Add(new DataColumn("Column4", typeof(string)));
        dr = dt.NewRow();

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentLanguageTable"] = dt;

        gvLanguage.DataSource = dt;
        gvLanguage.DataBind();

    }

    protected void gvLanguage_RowCreated(object sender, GridViewRowEventArgs e)
    {
        gv_RowCreated("CurrentLanguageTable", "lbtnLanguage_RowRemoved", e);
    }

    protected void btnLanguage_RowAdded_Click(object sender, EventArgs e)
    {  
        Boolean hasError = true;
        hasError = CheckLanguage();
        if (!hasError)
        {
            Boolean isAcceptable = false;
            isAcceptable = CheckLanguageProficiency();

            if (isAcceptable)
            {
                AddNewRowToLanguageGrid();
            }
        }
    }

    private void AddNewRowToLanguageGrid()
    {
        if (ViewState["CurrentLanguageTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentLanguageTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                ExtractLanguage(dtCurrentTable, "add");
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        ShowLanguage();
        //Set Previous Data on Postbacks
        SetPreviousLanguageData("add");
    }

    protected void lbtnLanguage_RowRemoved_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentLanguageTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentLanguageTable"];
            if (dt.Rows.Count > 1)
            {
                ExtractLanguage(dt, "remove");

                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data
                    dt.Rows.Remove(dt.Rows[gvRow.RowIndex]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentLanguageTable"] = dt;
            //Re bind the GridView for the updated data
            gvLanguage.DataSource = dt;
            gvLanguage.DataBind();
        }

        ShowLanguage();
        //Set Previous Data on Postbacks
        SetPreviousLanguageData("remove");        
    }

    private void ExtractLanguage(DataTable dt, string status)
    {
        int rowIndex = 0;
        DataRow dr = null;
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            //extract the values
            DropDownList ddl1 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[0].FindControl("ddlLanguage");
            DropDownList ddl2 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[1].FindControl("ddlSpokenSkill");
            DropDownList ddl3 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[2].FindControl("ddlWrittenSkill");
            DropDownList ddl4 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[3].FindControl("ddlReadingSkill");

            if (status == "add")
            {
                dr = dt.NewRow();
            }

            dt.Rows[i - 1]["Column1"] = ddl1.SelectedItem.Text;
            dt.Rows[i - 1]["Column2"] = ddl2.SelectedItem.Text;
            dt.Rows[i - 1]["Column3"] = ddl3.SelectedItem.Text;
            dt.Rows[i - 1]["Column4"] = ddl4.SelectedItem.Text;

            rowIndex++;
        }
        if (status == "add")
        {
            dt.Rows.Add(dr);
            //Store the current data in ViewState for future reference
            ViewState["CurrentLanguageTable"] = dt;
            //Re bind the GridView for the updated data
            gvLanguage.DataSource = dt;
            gvLanguage.DataBind();
        }

    }

    private void SetPreviousLanguageData(string status)
    {
        int rowIndex = 0;
        if (ViewState["CurrentLanguageTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentLanguageTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList ddl1 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[0].FindControl("ddlLanguage");
                    DropDownList ddl2 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[1].FindControl("ddlSpokenSkill");
                    DropDownList ddl3 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[2].FindControl("ddlWrittenSkill");
                    DropDownList ddl4 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[3].FindControl("ddlReadingSkill");

                    if (status == "add")
                    {
                        if (i < dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                            ddl2.ClearSelection();
                            ddl2.Items.FindByText(dt.Rows[i]["Column2"].ToString()).Selected = true;
                            ddl3.ClearSelection();
                            ddl3.Items.FindByText(dt.Rows[i]["Column3"].ToString()).Selected = true;
                            ddl4.ClearSelection();
                            ddl4.Items.FindByText(dt.Rows[i]["Column4"].ToString()).Selected = true;
                        }
                    }
                    else if (status == "remove")
                    {
                        if (i <= dt.Rows.Count - 1)
                        {
                            ddl1.ClearSelection();
                            ddl1.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;
                            ddl2.ClearSelection();
                            ddl2.Items.FindByText(dt.Rows[i]["Column2"].ToString()).Selected = true;
                            ddl3.ClearSelection();
                            ddl3.Items.FindByText(dt.Rows[i]["Column3"].ToString()).Selected = true;
                            ddl4.ClearSelection();
                            ddl4.Items.FindByText(dt.Rows[i]["Column4"].ToString()).Selected = true;
                        }
                    }
                    rowIndex++;
                }
            }
        }
        CheckLanguage();
    }

    private bool CheckLanguage()
    {
        int rows = gvLanguage.Rows.Count;
        Boolean hasError = false;

        for (int i = 0; i < rows; i++)
        {
            DropDownList ddl = (DropDownList)gvLanguage.Rows[i].Cells[0].FindControl("ddlLanguage");
            ddl.Enabled = false;

            if (ddl.Text != "Select Language")
            {
                if (rows >= 2)
                {
                    //Compare all selected languages each others whether Duplicate or Not
                    for (int k = 0; k < rows; k++)
                    {
                        for (int j = 0; j < k; j++)
                        {
                            DropDownList ddl1 = (DropDownList)gvLanguage.Rows[k].Cells[0].FindControl("ddlLanguage");
                            DropDownList ddl2 = (DropDownList)gvLanguage.Rows[j].Cells[0].FindControl("ddlLanguage");
                            if (ddl1.Text == ddl2.Text)
                            {
                                j = k;
                                k = rows;
                                lblLanguageDupMsg.Text = "Language should not be duplicated!";
                                hasError = true;
                                ddl1.Enabled = true;
                            }
                            else
                            {
                                lblLanguageDupMsg.Text = "";
                                hasError = false;
                            }
                        }
                    }
                }
                else if (rows < 2)
                {
                    lblLanguageDupMsg.Text = "";                    
                    hasError = false;
                    ddl.Enabled = true;
                }
            }
            else
            {
                i = rows;
                lblLanguageDupMsg.Text = "";
                hasError = true;
                ddl.Enabled = true;
            }
        }
        return hasError;
    }

    private bool CheckLanguageProficiency()
    {
        Boolean isAcceptable = true;
        lblProficiencyMsg.Text = "";

        if (ViewState["CurrentLanguageTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentLanguageTable"];
            if (dt.Rows.Count > 0)
            {
                int rowIndex = 0;
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    DropDownList ddl1 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[1].FindControl("ddlSpokenSkill");
                    DropDownList ddl2 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[2].FindControl("ddlWrittenSkill");
                    DropDownList ddl3 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[3].FindControl("ddlReadingSkill");

                    if (ddl1.Text == "Not Applicable" && ddl2.Text == "Not Applicable" && ddl3.Text == "Not Applicable")
                    {                        
                        isAcceptable = false;
                        lblProficiencyMsg.Text = "At least one proficiency should not be 'Not Applicable' for each Language.";
                    }
                    rowIndex++;
                }
            }
        }
        return isAcceptable;
    }

    private void gv_RowCreated(string dataTable, string linkButton, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)ViewState[dataTable];
            LinkButton lb = (LinkButton)e.Row.FindControl(linkButton);
            if (lb != null)
            {
                if (dt.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == 0)
                    {
                        lb.Visible = false;
                    }
                }
                else
                {
                    lb.Visible = false;
                }
            }
        }
    }

    protected void btnUploadPhoto_Click(object sender, EventArgs e)
    {
        string applicationID = string.IsNullOrEmpty(txtNRIC.Text) ? txtPassport.Text : txtNRIC.Text;
        applicationID = applicationID.ToUpper().Replace(" ", "");
        applicationID = Regex.Replace(applicationID, @"[\/:*!?<>|]", "");
        string path = Server.MapPath("Temp/" + applicationID + "_Photo/");
        //lblResumeUploadMsg.Text = "";
        lblResumeUploadErrMsg.Text = "";

        //Photo Upload to Temp folder
        if (filUploadPhoto.HasFile)
        {            
            try
            {
                string fileExt = Path.GetExtension(filUploadPhoto.FileName);
                fileExt = fileExt.ToLower();
                if (fileExt != ".bmp" && fileExt != ".gif" && fileExt != ".jpg" && fileExt != ".jpeg" && fileExt != ".png")
                {
                    DeleteFiles(path);                    
                    chkUploadPhoto.Visible = false;
                    chkUploadPhoto.Checked = false;
                    lblUploadPhoto.Text = "";
                    //lblPhotoUploadMsg.Text = "Only .bmp, .gif, .jpg, .jpeg, .png file types are acceptable!";
                    lblPhotoUploadErrMsg.Text = "";
                }
                else
                {
                    if (filUploadPhoto.PostedFile.ContentLength <= 153600)  //Check file size
                    {                       
                        Bitmap img = new Bitmap(filUploadPhoto.PostedFile.InputStream, false);
                        int imgWidth = img.Width;
                        int imgHeight = img.Height;
                        if (imgWidth <= 400 && imgHeight <= 514)    //Check file size by pixels
                        {
                            DeleteFiles(path);
                            string fileName = Path.GetFileName(filUploadPhoto.FileName);
                            Directory.CreateDirectory(Server.MapPath("Temp/" + applicationID + "_Photo"));   //Create dynamic folder for each applicant
                            filUploadPhoto.SaveAs(Server.MapPath("Temp/" + applicationID + "_Photo/" + fileName));  //Upload file                        
                            chkUploadPhoto.Visible = true;
                            chkUploadPhoto.Checked = true;
                            lblUploadPhoto.Text = fileName;
                            //lblPhotoUploadMsg.Text = "";
                            lblPhotoUploadErrMsg.Text = "";
                        }
                        else
                        {
                            DeleteFiles(path);
                            chkUploadPhoto.Visible = false;
                            chkUploadPhoto.Checked = false;
                            lblUploadPhoto.Text = "";
                            //lblPhotoUploadMsg.Text = "The file should not exceed 150 kb!";
                            lblPhotoUploadErrMsg.Text = "";
                        }
                    }
                    else
                    {
                        DeleteFiles(path);
                        chkUploadPhoto.Visible = false;
                        chkUploadPhoto.Checked = false;
                        lblUploadPhoto.Text = "";
                        //lblPhotoUploadMsg.Text = "The file should not exceed 150 kb!";
                        lblPhotoUploadErrMsg.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                DeleteFiles(path);                
                chkUploadPhoto.Visible = false;
                chkUploadPhoto.Checked = false;
                lblUploadPhoto.Text = "";
                lblPhotoUploadMsg.Text = "The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
        else
        {
            if (lblUploadPhoto.Text != "")
            {
                chkUploadPhoto.Checked = true;
                //lblPhotoUploadMsg.Text = "";
                lblPhotoUploadErrMsg.Text = "";
            }
            else
            {
                //lblPhotoUploadMsg.Text = "No file is selected! ";
                lblPhotoUploadErrMsg.Text = "No file is selected! ";
            }
        }
    }

    protected void btnUploadResume_Click(object sender, EventArgs e)
    {
        string applicationID = string.IsNullOrEmpty(txtNRIC.Text) ? txtPassport.Text : txtNRIC.Text;
        applicationID = applicationID.ToUpper().Replace(" ", "");
        applicationID = Regex.Replace(applicationID, @"[\/:*!?<>|]", "");
        string path = Server.MapPath("Temp/" + applicationID + "_Resume/");
        //lblPhotoUploadMsg.Text = "";
        lblPhotoUploadErrMsg.Text = "";

        //Resume Upload to Temp folder
        if (filUploadResume.HasFile)
        {
            try
            {
                string fileExt = Path.GetExtension(filUploadResume.FileName);
                fileExt = fileExt.ToLower();
                if (fileExt != ".doc" && fileExt != ".docx" && fileExt != ".xls" && fileExt != ".xlsx" && fileExt != ".pdf")
                {
                    DeleteFiles(path);                    
                    chkUploadResume.Visible = false;
                    chkUploadResume.Checked = false;
                    lblUploadResume.Text = "";
                    //lblResumeUploadMsg.Text = "Only .doc, .docx, .xls, .xlsx, .pdf file types are acceptable!";
                    lblResumeUploadErrMsg.Text = "";
                }
                else
                {
                    if (filUploadResume.PostedFile.ContentLength <= 512000)  //Check file size
                    {
                        DeleteFiles(path);
                        string fileName = Path.GetFileName(filUploadResume.FileName);
                        Directory.CreateDirectory(Server.MapPath("Temp/" + applicationID + "_Resume"));   //Create dynamic folder for each applicant
                        filUploadResume.SaveAs(Server.MapPath("Temp/" + applicationID + "_Resume/" + fileName));  //Upload file                        
                        chkUploadResume.Visible = true;
                        chkUploadResume.Checked = true;
                        lblUploadResume.Text = fileName;
                        //lblResumeUploadMsg.Text = "";
                        lblResumeUploadErrMsg.Text = "";
                    }
                    else
                    {
                        DeleteFiles(path);                        
                        chkUploadResume.Visible = false;
                        chkUploadResume.Checked = false;
                        lblUploadResume.Text = "";
                        //lblResumeUploadMsg.Text = "The file should not exceed 500 kb!";
                        lblResumeUploadErrMsg.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                DeleteFiles(path);                
                chkUploadResume.Visible = false;
                chkUploadResume.Checked = false;
                lblUploadResume.Text = "";
                lblResumeUploadMsg.Text = "The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
        else
        {
            if (lblUploadResume.Text != "")
            {
                chkUploadResume.Checked = true;
                //lblResumeUploadMsg.Text = "";
                lblResumeUploadErrMsg.Text = "";
            }
            else
            {
                //lblResumeUploadMsg.Text = "No file is selected!";
                lblResumeUploadErrMsg.Text = "No file is selected!";
            }
        }
    }

    private void DeleteFiles(string path)
    {
        if (Directory.Exists(path))
        {
            string[] files = Directory.GetFiles(path, "*.*");
            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string applicationID = string.IsNullOrEmpty(txtNRIC.Text) ? txtPassport.Text : txtNRIC.Text;
        applicationID = applicationID.ToUpper().Replace(" ", "");
        applicationID = Regex.Replace(applicationID, @"[\/:*!?<>|]", "");
        Boolean HasError = false;
        //lblPhotoUploadMsg.Text = "";

        //Photo Upload to Attachments folder / remove from Temp folder
        if ((chkUploadPhoto.Checked) && (lblUploadPhoto.Text != ""))
        {
            try
            {
                Directory.CreateDirectory(Server.MapPath("Attachments/" + applicationID));   //Create dynamic folder for each applicant
                string photoSource = Server.MapPath("Temp/" + applicationID + "_Photo/");
                string photoDestination = Server.MapPath("Attachments/" + applicationID + "/");
                File.Copy(photoSource + lblUploadPhoto.Text, photoDestination + lblUploadPhoto.Text, true);
                DeleteFiles(photoSource);
                Directory.Delete(photoSource, true);                 
            }
            catch (Exception ex)
            {
                lblPhotoUploadMsg.Text = "The file could not be uploaded. The following error occured: " + ex.Message;
                HasError = true;
            }
        }
        else
        {
            //lblPhotoUploadMsg.Text = "Please select photo to upload!";
            lblPhotoUploadErrMsg.Text = "";
        }

        //Resume Upload to Attachments folder / remove from Temp folder
        if ((chkUploadResume.Checked) && (lblUploadResume.Text != ""))
        {
            try
            {
                Directory.CreateDirectory(Server.MapPath("Attachments/" + applicationID));   //Create dynamic folder for each applicant
                string resumeSource = Server.MapPath("Temp/" + applicationID + "_Resume/");
                string resumeDestination = Server.MapPath("Attachments/" + applicationID + "/");
                File.Copy(resumeSource + lblUploadResume.Text, resumeDestination + lblUploadResume.Text, true);
                DeleteFiles(resumeSource);
                Directory.Delete(resumeSource, true);
            }
            catch (Exception ex)
            {
                lblResumeUploadMsg.Text = "The file could not be uploaded. The following error occured: " + ex.Message;
                HasError = true;
            }
        }
        else
        {
            //lblResumeUploadMsg.Text = "";
            lblResumeUploadErrMsg.Text = "";
        }
        //FILE UPLOAD END

        //SAVE DATA START
        if (!HasError)
        {
            string strConnection = ConfigurationManager.ConnectionStrings["CS_HumanResource"].ToString();
            SqlConnection sqlConnection = new SqlConnection(strConnection);

            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            string timestamp = month + "/" + day + "/" + year + " " + DateTime.Now.ToShortTimeString();            
            string rotation = (ddlPosition.Text != "Customer Care Officer") ? "" : rdEnableRotation.Text;
            string availablestartdate = string.IsNullOrEmpty(txtAvailableStartDate.Text) ? "" : Convert.ToDateTime(txtAvailableStartDate.Text).Month + "/" + Convert.ToDateTime(txtAvailableStartDate.Text).Day + "/" + Convert.ToDateTime(txtAvailableStartDate.Text).Year;
            string availableenddate = string.IsNullOrEmpty(txtAvailableEndDate.Text) ? "" : Convert.ToDateTime(txtAvailableEndDate.Text).Month + "/" + Convert.ToDateTime(txtAvailableEndDate.Text).Day + "/" + Convert.ToDateTime(txtAvailableEndDate.Text).Year;
            string dob = string.IsNullOrEmpty(txtDOB.Text) ? "" : Convert.ToDateTime(txtDOB.Text).Month + "/" + Convert.ToDateTime(txtDOB.Text).Day + "/" + Convert.ToDateTime(txtDOB.Text).Year;
            int age = string.IsNullOrEmpty(txtDOB.Text) ? 0 : year - Convert.ToDateTime(txtDOB.Text).Year;
            string pr_issueddate = string.IsNullOrEmpty(txtPRIssuedDate.Text) ? "" : Convert.ToDateTime(txtPRIssuedDate.Text).Month + "/" + Convert.ToDateTime(txtPRIssuedDate.Text).Day + "/" + Convert.ToDateTime(txtPRIssuedDate.Text).Year;
            string pp_expirydate = string.IsNullOrEmpty(txtExpiryDate.Text) ? "" : Convert.ToDateTime(txtExpiryDate.Text).Month + "/" + Convert.ToDateTime(txtExpiryDate.Text).Day + "/" + Convert.ToDateTime(txtExpiryDate.Text).Year;
            string religion = (ddlReligion.Text == "Select Religion") ? "" : ddlReligion.Text;
            string ns_status = (ddlNSStatus.Text == "Select NS Status") ? "" : ddlNSStatus.Text;
            string ns_datebegan = string.IsNullOrEmpty(txtNSDateBegan.Text) ? "" : Convert.ToDateTime(txtNSDateBegan.Text).Month + "/" + Convert.ToDateTime(txtNSDateBegan.Text).Day + "/" + Convert.ToDateTime(txtNSDateBegan.Text).Year;
            string ns_datecompletion = string.IsNullOrEmpty(txtNSDateCompletion.Text) ? "" : Convert.ToDateTime(txtNSDateCompletion.Text).Month + "/" + Convert.ToDateTime(txtNSDateCompletion.Text).Day + "/" + Convert.ToDateTime(txtNSDateCompletion.Text).Year;
            string ns_nextdate = string.IsNullOrEmpty(txtNSNextDate.Text) ? "" : Convert.ToDateTime(txtNSNextDate.Text).Month + "/" + Convert.ToDateTime(txtNSNextDate.Text).Day + "/" + Convert.ToDateTime(txtNSNextDate.Text).Year;
            string availabledate = string.IsNullOrEmpty(txtAvailableDate.Text) ? "" : Convert.ToDateTime(txtAvailableDate.Text).Month + "/" + Convert.ToDateTime(txtAvailableDate.Text).Day + "/" + Convert.ToDateTime(txtAvailableDate.Text).Year;
            string startdate = string.IsNullOrEmpty(txtTCStartDate.Text) ? "" : Convert.ToDateTime(txtTCStartDate.Text).Month + "/" + Convert.ToDateTime(txtTCStartDate.Text).Day + "/" + Convert.ToDateTime(txtTCStartDate.Text).Year;
            string enddate = string.IsNullOrEmpty(txtTCEndDate.Text) ? "" : Convert.ToDateTime(txtTCEndDate.Text).Month + "/" + Convert.ToDateTime(txtTCEndDate.Text).Day + "/" + Convert.ToDateTime(txtTCEndDate.Text).Year;

            string link = "";
            if ((chkUploadPhoto.Checked && lblUploadPhoto.Text != "") || (chkUploadResume.Checked && lblUploadResume.Text != ""))
                link = Server.MapPath("Attachments/" + applicationID);

            string spouse_dob = string.IsNullOrEmpty(txtSpouseDOB.Text) ? "" : Convert.ToDateTime(txtSpouseDOB.Text).Month + "/" + Convert.ToDateTime(txtSpouseDOB.Text).Day + "/" + Convert.ToDateTime(txtSpouseDOB.Text).Year;
            int spouse_age = string.IsNullOrEmpty(txtSpouseDOB.Text) ? 0 : year - Convert.ToDateTime(txtSpouseDOB.Text).Year;
            string spouse_nationality = (ddlSpouseNationality.Text == "Select Nationality") ? "" : ddlSpouseNationality.Text;

            sqlConnection.Open();
            //Get Application ID
            SqlCommand cmdGetID = new SqlCommand("JobApplication_GetID", sqlConnection);
            cmdGetID.Parameters.Add(new SqlParameter("@id", applicationID));
            cmdGetID.CommandType = CommandType.StoredProcedure;            
            object id = cmdGetID.ExecuteScalar();

            //Get Application Status
            SqlCommand cmdGetStatus = new SqlCommand("JobApplication_GetApplicationStatus", sqlConnection);
            cmdGetStatus.Parameters.Add(new SqlParameter("@id", applicationID));
            cmdGetStatus.CommandType = CommandType.StoredProcedure;
            object status = cmdGetStatus.ExecuteScalar();
            
            //Get Remark
            SqlCommand cmdGetRemark = new SqlCommand("JobApplication_GetRemark", sqlConnection);
            cmdGetRemark.Parameters.Add(new SqlParameter("@id", applicationID));
            cmdGetRemark.CommandType = CommandType.StoredProcedure;
            object remark = cmdGetRemark.ExecuteScalar();

            //SAVE PARTICULARS INFO
            SqlCommand cmdParticulars = new SqlCommand("JobApplication_InsertUpdate_Particulars", sqlConnection);
            cmdParticulars.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdParticulars.Parameters.Add(new SqlParameter("@Emp_Id", ""));
            cmdParticulars.Parameters.Add(new SqlParameter("@Position_Applied", ddlPosition.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Position_Applied_Oth", txtPositionOthers.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Enable_Rotation", rotation));
            cmdParticulars.Parameters.Add(new SqlParameter("@Preferred_JobType", ddlPreferredJobType.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Available_StartDate", availablestartdate));
            cmdParticulars.Parameters.Add(new SqlParameter("@Available_EndDate", availableenddate));
            cmdParticulars.Parameters.Add(new SqlParameter("@Name", txtName.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Alias", txtAlias.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Residential_Addr", txtResidentialAddr.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@HomeCountry_Addr", txtHomeCountryAddr.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Email_Addr", txtEmailAddr.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Home_N", txtHomeNo.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Mobile_N", txtMobileNo.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Overseas_N", txtOverseasNo.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@NRIC_FIN", txtNRIC.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Passport", txtPassport.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@DOB", dob));
            cmdParticulars.Parameters.Add(new SqlParameter("@Age", age));
            cmdParticulars.Parameters.Add(new SqlParameter("@Gender", ddlGender.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Marital_Status", ddlMaritalStatus.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Citizenship", ddlCitizenship.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@PR_IssuedDate", pr_issueddate));
            cmdParticulars.Parameters.Add(new SqlParameter("@Passport_ExpiryDate", pp_expirydate));
            cmdParticulars.Parameters.Add(new SqlParameter("@Nationality", ddlNationality.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Race", ddlRace.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Race_Oth", txtRaceOthers.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Religion", religion));
            cmdParticulars.Parameters.Add(new SqlParameter("@Contact_Person_Name", txtContactPersonName.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Contact_Person_Relationship", txtContactPersonRelationship.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Contact_Person_N", txtContactPersonNo.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Contact_Person_Overseas_N", txtContactPersonOverseasNo.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Contact_Person_Addr", txtContactPersonAddr.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@NS_Status", ns_status));
            cmdParticulars.Parameters.Add(new SqlParameter("@NS_Date_Began", ns_datebegan));
            cmdParticulars.Parameters.Add(new SqlParameter("@NS_Date_Completion", ns_datecompletion));
            cmdParticulars.Parameters.Add(new SqlParameter("@NS_Rank", txtNSRank.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@NS_Unit", txtNSUnit.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@NS_Next_Date", ns_nextdate));
            cmdParticulars.Parameters.Add(new SqlParameter("@NS_Next_Length", txtNSNextLength.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Typing_Skill", txtWordsPerMins.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Total_Work_Experience", ddlTotalWorkExperience.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Current_Working", chkCurrentWorking.Checked ? 1 : 0));
            cmdParticulars.Parameters.Add(new SqlParameter("@Languages_Oth", txtLanguagesOth.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Expected_Salary", txtExpectedSalary.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Negotiable", chkNegotiable.Checked ? 1 : 0));
            cmdParticulars.Parameters.Add(new SqlParameter("@Notice_Period", ddlNoticePeriod.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Earliest_Start_Date", availabledate));
            cmdParticulars.Parameters.Add(new SqlParameter("@Known_Staff", rdListKnownStaff.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Staff_Name", txtStaffName.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Staff_Relationship", txtStaffRelationship.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Work_TeleCentre_Before", rdListWorkTeleCentreBefore.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@TeleCentre_Start_Date", startdate));
            cmdParticulars.Parameters.Add(new SqlParameter("@TeleCentre_End_Date", enddate));
            cmdParticulars.Parameters.Add(new SqlParameter("@TeleCentre_Position", txtTCPosition.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Physical_Impairment", rdListPhysicalImpairment.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Major_Disease_Illness", rdListMajorDiseaseIllness.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Minor_Disease_Illness", rdListMinorDiseaseIllness.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Longterm_Medication", rdListLongtermMedication.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Bankruptcy_Status", rdListBankruptcyStatus.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Convicted_Offence", rdListConvictedOffence.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Dismissed_Suspended", rdListDismissedSuspended.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Details_Answer", txtDetailsAnswer.Text));
            cmdParticulars.Parameters.Add(new SqlParameter("@Read_Form", chkDeclarationStatement.Checked ? 1 : 0));
            cmdParticulars.Parameters.Add(new SqlParameter("@Timestamp_Applied", timestamp));
            cmdParticulars.Parameters.Add(new SqlParameter("@Timestamp_Update", ""));
            cmdParticulars.Parameters.Add(new SqlParameter("@Author_Update", ""));
            cmdParticulars.Parameters.Add(new SqlParameter("@Application_Status", "New"));
            cmdParticulars.Parameters.Add(new SqlParameter("@Folder_Link", link));
            cmdParticulars.CommandType = CommandType.StoredProcedure;
            if (id == null)
            {
                //cmdParticulars.Parameters.Add(new SqlParameter("@Application_Status", "New"));
                cmdParticulars.Parameters.Add(new SqlParameter("@Remark", ""));
                cmdParticulars.Parameters.Add(new SqlParameter("@TransactionType", "Insert"));
            }
            else
            {
                //cmdParticulars.Parameters.Add(new SqlParameter("@Application_Status", status));
                cmdParticulars.Parameters.Add(new SqlParameter("@Remark", remark));
                cmdParticulars.Parameters.Add(new SqlParameter("@TransactionType", "Update"));
            }
            cmdParticulars.ExecuteNonQuery();
            //SAVE SPOUSE INFO IF ANY
            SqlCommand cmdSpouseDelete = new SqlCommand("DELETE FROM JobApplication_Spouse WHERE Ja_Id = '" + applicationID + "'", sqlConnection);
            cmdSpouseDelete.ExecuteNonQuery();
            SqlCommand cmdSpouseInsert = new SqlCommand("JobApplication_Insert_Spouse", sqlConnection);
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@Name", txtSpouseName.Text));
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@NRIC", txtSpouseNRIC.Text));
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@DOB", spouse_dob));
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@Age", spouse_age));
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@Nationality", spouse_nationality));
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@Occupation", txtSpouseOccupation.Text));
            cmdSpouseInsert.Parameters.Add(new SqlParameter("@Employer", txtSpouseEmployer.Text));
            cmdSpouseInsert.CommandType = CommandType.StoredProcedure;
            if (txtSpouseName.Text != "" || txtSpouseNRIC.Text != "" || spouse_dob != ""
                || spouse_nationality != "" || txtSpouseOccupation.Text != "" || txtSpouseEmployer.Text != "")
                cmdSpouseInsert.ExecuteNonQuery();

            SqlCommand cmdRelationshipDelete = new SqlCommand("DELETE FROM JobApplication_Relationships WHERE Ja_Id = '" + applicationID + "'", sqlConnection);
            cmdRelationshipDelete.ExecuteNonQuery();
            //SAVE CHILDREN INFO IF ANY
            //Children - Dynamic Start
            int rowIndex = 0;
            string relationship = "";
            string gender = "";
            string relationship_dob = "";
            int relationship_age = 0;            
            if (ViewState["CurrentChildrenTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentChildrenTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the values
                        DropDownList ddl1 = (DropDownList)gvChildren.Rows[rowIndex].Cells[0].FindControl("ddlChildren");
                        TextBox text1 = (TextBox)gvChildren.Rows[rowIndex].Cells[1].FindControl("txtChildrenName");
                        TextBox text2 = (TextBox)gvChildren.Rows[rowIndex].Cells[2].FindControl("txtChildrenDOB");
                        TextBox text3 = (TextBox)gvChildren.Rows[rowIndex].Cells[3].FindControl("txtChildrenEmployer");
                        TextBox text4 = (TextBox)gvChildren.Rows[rowIndex].Cells[4].FindControl("txtChildrenOccupation");

                        dtCurrentTable.Rows[i - 1]["Column1"] = ddl1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = text1.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = text2.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = text3.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = text4.Text;

                        relationship = (ddl1.Text == "Select Gender") ? "" : ((ddl1.Text == "Male") ? "Son" : "Daughter");
                        gender = (ddl1.Text == "Select Gender") ? "" : ddl1.Text;
                        relationship_dob = string.IsNullOrEmpty(text2.Text) ? "" : Convert.ToDateTime(text2.Text).Month + "/" + Convert.ToDateTime(text2.Text).Day + "/" + Convert.ToDateTime(text2.Text).Year;
                        relationship_age = string.IsNullOrEmpty(text2.Text) ? 0 : year - Convert.ToDateTime(text2.Text).Year;

                        SqlCommand cmdChildrenInsert = new SqlCommand("JobApplication_Insert_Relationships", sqlConnection);
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Relationship", relationship));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Gender", gender));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Name", text1.Text));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@DOB", relationship_dob));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Age", relationship_age));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Occupation", text4.Text));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Employer", text3.Text));
                        cmdChildrenInsert.Parameters.Add(new SqlParameter("@Sort_Order", i));
                        cmdChildrenInsert.CommandType = CommandType.StoredProcedure;

                        if (relationship != "" && text1.Text != "")
                            cmdChildrenInsert.ExecuteNonQuery();

                        rowIndex++;
                    }
                }
            }
            //Children - Dynamic End

            //SAVE PARENT INFO
            relationship_dob = string.IsNullOrEmpty(txtFatherDOB.Text) ? "" : Convert.ToDateTime(txtFatherDOB.Text).Month + "/" + Convert.ToDateTime(txtFatherDOB.Text).Day + "/" + Convert.ToDateTime(txtFatherDOB.Text).Year;
            relationship_age = string.IsNullOrEmpty(txtFatherDOB.Text) ? 0 : year - Convert.ToDateTime(txtFatherDOB.Text).Year;
            SqlCommand cmdFatherInsert = new SqlCommand("JobApplication_Insert_Relationships", sqlConnection);
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Relationship", "Father"));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Gender", "Male"));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Name", txtFatherName.Text));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@DOB", relationship_dob));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Age", relationship_age));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Occupation", txtFatherOccupation.Text));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Employer", txtFatherEmployer.Text));
            cmdFatherInsert.Parameters.Add(new SqlParameter("@Sort_Order", "1"));
            cmdFatherInsert.CommandType = CommandType.StoredProcedure;
            cmdFatherInsert.ExecuteNonQuery();

            relationship_dob = string.IsNullOrEmpty(txtMotherDOB.Text) ? "" : Convert.ToDateTime(txtMotherDOB.Text).Month + "/" + Convert.ToDateTime(txtMotherDOB.Text).Day + "/" + Convert.ToDateTime(txtMotherDOB.Text).Year;
            relationship_age = string.IsNullOrEmpty(txtMotherDOB.Text) ? 0 : year - Convert.ToDateTime(txtMotherDOB.Text).Year;
            SqlCommand cmdMotherInsert = new SqlCommand("JobApplication_Insert_Relationships", sqlConnection);
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Relationship", "Mother"));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Gender", "Female"));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Name", txtMotherName.Text));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@DOB", relationship_dob));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Age", relationship_age));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Occupation", txtMotherOccupation.Text));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Employer", txtMotherEmployer.Text));
            cmdMotherInsert.Parameters.Add(new SqlParameter("@Sort_Order", "2"));
            cmdMotherInsert.CommandType = CommandType.StoredProcedure;
            cmdMotherInsert.ExecuteNonQuery();

            //SAVE SIBLING INFO IF ANY
            //Sibling - Dynamic Start
            rowIndex = 0;
            relationship = "";
            gender = "";
            relationship_dob = "";
            relationship_age = 0;
            if (ViewState["CurrentSiblingTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentSiblingTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the values
                        DropDownList ddl1 = (DropDownList)gvSibling.Rows[rowIndex].Cells[0].FindControl("ddlSibling");
                        TextBox text1 = (TextBox)gvSibling.Rows[rowIndex].Cells[1].FindControl("txtSiblingName");
                        TextBox text2 = (TextBox)gvSibling.Rows[rowIndex].Cells[2].FindControl("txtSiblingDOB");
                        TextBox text3 = (TextBox)gvSibling.Rows[rowIndex].Cells[3].FindControl("txtSiblingEmployer");
                        TextBox text4 = (TextBox)gvSibling.Rows[rowIndex].Cells[4].FindControl("txtSiblingOccupation");

                        dtCurrentTable.Rows[i - 1]["Column1"] = ddl1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = text1.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = text2.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = text3.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = text4.Text;

                        relationship = (ddl1.Text == "Select Gender") ? "" : ((ddl1.Text == "Male") ? "Brother" : "Sister");
                        gender = (ddl1.Text == "Select Gender") ? "" : ddl1.Text;
                        relationship_dob = string.IsNullOrEmpty(text2.Text) ? "" : Convert.ToDateTime(text2.Text).Month + "/" + Convert.ToDateTime(text2.Text).Day + "/" + Convert.ToDateTime(text2.Text).Year;
                        relationship_age = string.IsNullOrEmpty(text2.Text) ? 0 : year - Convert.ToDateTime(text2.Text).Year;

                        SqlCommand cmdSiblingInsert = new SqlCommand("JobApplication_Insert_Relationships", sqlConnection);
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Relationship", relationship));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Gender", gender));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Name", text1.Text));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@DOB", relationship_dob));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Age", relationship_age));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Occupation", text4.Text));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Employer", text3.Text));
                        cmdSiblingInsert.Parameters.Add(new SqlParameter("@Sort_Order", i));
                        cmdSiblingInsert.CommandType = CommandType.StoredProcedure;

                        if (relationship != "" && text1.Text != "")                            
                            cmdSiblingInsert.ExecuteNonQuery();

                        rowIndex++;
                    }
                }
            }
            //Sibling - Dynamic End

            SqlCommand cmdEducationDelete = new SqlCommand("DELETE FROM JobApplication_Education WHERE Ja_Id = '" + applicationID + "'", sqlConnection);
            cmdEducationDelete.ExecuteNonQuery();
            //SAVE EDUCATION INFO
            //Education - Dynamic Start
            rowIndex = 0;
            string start_date = "";
            string end_date = "";
            if (ViewState["CurrentEducationTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentEducationTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the values
                        TextBox text1 = (TextBox)gvEducation.Rows[rowIndex].Cells[1].FindControl("txtEducationAttained");
                        TextBox text2 = (TextBox)gvEducation.Rows[rowIndex].Cells[2].FindControl("txtEducationSchoolName");
                        TextBox text3 = (TextBox)gvEducation.Rows[rowIndex].Cells[3].FindControl("txtEducationStartDate");
                        TextBox text4 = (TextBox)gvEducation.Rows[rowIndex].Cells[4].FindControl("txtEducationEndDate");
                        TextBox text5 = (TextBox)gvEducation.Rows[rowIndex].Cells[5].FindControl("txtEducationMajor");

                        dtCurrentTable.Rows[i - 1]["Column1"] = text1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = text2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = text3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = text4.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = text5.Text;

                        start_date = string.IsNullOrEmpty(text3.Text) ? "" : Convert.ToDateTime(text3.Text).Month + "/" + Convert.ToDateTime(text3.Text).Day + "/" + Convert.ToDateTime(text3.Text).Year;
                        end_date = string.IsNullOrEmpty(text4.Text) ? "" : Convert.ToDateTime(text4.Text).Month + "/" + Convert.ToDateTime(text4.Text).Day + "/" + Convert.ToDateTime(text4.Text).Year;

                        SqlCommand cmdEducationInsert = new SqlCommand("JobApplication_Insert_Education", sqlConnection);
                        cmdEducationInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
                        cmdEducationInsert.Parameters.Add(new SqlParameter("@Institution_Name", text2.Text));
                        cmdEducationInsert.Parameters.Add(new SqlParameter("@Start_Date", start_date));
                        cmdEducationInsert.Parameters.Add(new SqlParameter("@End_Date", end_date));
                        cmdEducationInsert.Parameters.Add(new SqlParameter("@Qualification_Attained", text1.Text));
                        cmdEducationInsert.Parameters.Add(new SqlParameter("@Course_Major", text5.Text));
                        cmdEducationInsert.Parameters.Add(new SqlParameter("@Sort_Order", i));
                        cmdEducationInsert.CommandType = CommandType.StoredProcedure;
                        cmdEducationInsert.ExecuteNonQuery();

                        rowIndex++;
                    }
                }
            }
            //Education - Dynamic End

            SqlCommand cmdQualificationDelete = new SqlCommand("DELETE FROM JobApplication_Qualification WHERE Ja_Id = '" + applicationID + "'", sqlConnection);
            cmdQualificationDelete.ExecuteNonQuery();
            //SAVE TRAINING COURSES INFO IF ANY
            //Training - Dynamic Start
            rowIndex = 0;
            string qualification_start_date = "";
            string qualification_end_date = "";
            string qualification_earned_date = "";
            if (ViewState["CurrentCourseTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentCourseTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the values
                        TextBox text1 = (TextBox)gvCourse.Rows[rowIndex].Cells[1].FindControl("txtCourseSchoolName");
                        TextBox text2 = (TextBox)gvCourse.Rows[rowIndex].Cells[2].FindControl("txtCourseTitle");
                        TextBox text3 = (TextBox)gvCourse.Rows[rowIndex].Cells[3].FindControl("txtCourseStartDate");
                        TextBox text4 = (TextBox)gvCourse.Rows[rowIndex].Cells[4].FindControl("txtCourseEndDate");

                        dtCurrentTable.Rows[i - 1]["Column1"] = text1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = text2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = text3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = text4.Text;

                        qualification_start_date = string.IsNullOrEmpty(text3.Text) ? "" : Convert.ToDateTime(text3.Text).Month + "/" + Convert.ToDateTime(text3.Text).Day + "/" + Convert.ToDateTime(text3.Text).Year;
                        qualification_end_date = string.IsNullOrEmpty(text4.Text) ? "" : Convert.ToDateTime(text4.Text).Month + "/" + Convert.ToDateTime(text4.Text).Day + "/" + Convert.ToDateTime(text4.Text).Year;

                        SqlCommand cmdTrainingInsert = new SqlCommand("JobApplication_Insert_Qualification", sqlConnection);
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Institution_Name", text1.Text));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Start_Date", qualification_start_date));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@End_Date", qualification_end_date));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Certificate_Title", text2.Text));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Certificate_Date", ""));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Certificate_Earned", ""));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Qualification_Type", "Training"));
                        cmdTrainingInsert.Parameters.Add(new SqlParameter("@Sort_Order", i));
                        cmdTrainingInsert.CommandType = CommandType.StoredProcedure;

                        if (text1.Text != "")
                            cmdTrainingInsert.ExecuteNonQuery();

                        rowIndex++;
                    }
                }
            }
            //Training - Dynamic End

            //SAVE CERTIFICATES INFO IF ANY
            //Certificate - Dynamic Start
            rowIndex = 0;
            if (ViewState["CurrentCertificateTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentCertificateTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the values
                        TextBox text1 = (TextBox)gvCertificate.Rows[rowIndex].Cells[1].FindControl("txtCertificateSchoolName");
                        TextBox text2 = (TextBox)gvCertificate.Rows[rowIndex].Cells[2].FindControl("txtCertificateTitle");
                        TextBox text3 = (TextBox)gvCertificate.Rows[rowIndex].Cells[3].FindControl("txtCertificateStartDate");
                        TextBox text4 = (TextBox)gvCertificate.Rows[rowIndex].Cells[4].FindControl("txtCertificateEndDate");
                        TextBox text5 = (TextBox)gvCertificate.Rows[rowIndex].Cells[5].FindControl("txtCertificateDate");
                        TextBox text6 = (TextBox)gvCertificate.Rows[rowIndex].Cells[6].FindControl("txtCertificateEarned");

                        dtCurrentTable.Rows[i - 1]["Column1"] = text1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = text2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = text3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = text4.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = text5.Text;
                        dtCurrentTable.Rows[i - 1]["Column6"] = text6.Text;

                        qualification_start_date = string.IsNullOrEmpty(text3.Text) ? "" : Convert.ToDateTime(text3.Text).Month + "/" + Convert.ToDateTime(text3.Text).Day + "/" + Convert.ToDateTime(text3.Text).Year;
                        qualification_end_date = string.IsNullOrEmpty(text4.Text) ? "" : Convert.ToDateTime(text4.Text).Month + "/" + Convert.ToDateTime(text4.Text).Day + "/" + Convert.ToDateTime(text4.Text).Year;
                        qualification_earned_date = string.IsNullOrEmpty(text5.Text) ? "" : Convert.ToDateTime(text5.Text).Month + "/" + Convert.ToDateTime(text5.Text).Day + "/" + Convert.ToDateTime(text5.Text).Year;

                        SqlCommand cmdCertificateInsert = new SqlCommand("JobApplication_Insert_Qualification", sqlConnection);
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Institution_Name", text1.Text));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Start_Date", qualification_start_date));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@End_Date", qualification_end_date));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Certificate_Title", text2.Text));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Certificate_Date", qualification_earned_date));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Certificate_Earned", text6.Text));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Qualification_Type", "Certificate"));
                        cmdCertificateInsert.Parameters.Add(new SqlParameter("@Sort_Order", i));
                        cmdCertificateInsert.CommandType = CommandType.StoredProcedure;

                        if (text1.Text != "")
                            cmdCertificateInsert.ExecuteNonQuery();

                        rowIndex++;
                    }
                }
            }
            //Certificate - Dynamic End

            SqlCommand cmdITKnowledgeDelete = new SqlCommand("DELETE FROM JobApplication_ITKnowledge WHERE Ja_Id = '" + applicationID + "'", sqlConnection);
            cmdITKnowledgeDelete.ExecuteNonQuery();
            //SAVE IT KNOWLEDGE
            SqlCommand cmdMSWordInsert = new SqlCommand("JobApplication_Insert_ITKnowledge", sqlConnection);
            cmdMSWordInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdMSWordInsert.Parameters.Add(new SqlParameter("@Software_Name", "Microsoft Word"));
            cmdMSWordInsert.Parameters.Add(new SqlParameter("@Proficiency", ddlMSWord.Text));
            cmdMSWordInsert.Parameters.Add(new SqlParameter("@Sort_Order", "0"));
            cmdMSWordInsert.CommandType = CommandType.StoredProcedure;
            cmdMSWordInsert.ExecuteNonQuery();

            SqlCommand cmdMSExcelInsert = new SqlCommand("JobApplication_Insert_ITKnowledge", sqlConnection);
            cmdMSExcelInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdMSExcelInsert.Parameters.Add(new SqlParameter("@Software_Name", "Microsoft Excel"));
            cmdMSExcelInsert.Parameters.Add(new SqlParameter("@Proficiency", ddlMSExcel.Text));
            cmdMSExcelInsert.Parameters.Add(new SqlParameter("@Sort_Order", "0"));
            cmdMSExcelInsert.CommandType = CommandType.StoredProcedure;
            cmdMSExcelInsert.ExecuteNonQuery();

            SqlCommand cmdMSPowerpointInsert = new SqlCommand("JobApplication_Insert_ITKnowledge", sqlConnection);
            cmdMSPowerpointInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdMSPowerpointInsert.Parameters.Add(new SqlParameter("@Software_Name", "Microsoft Powerpoint"));
            cmdMSPowerpointInsert.Parameters.Add(new SqlParameter("@Proficiency", ddlMSPowerpoint.Text));
            cmdMSPowerpointInsert.Parameters.Add(new SqlParameter("@Sort_Order", "0"));
            cmdMSPowerpointInsert.CommandType = CommandType.StoredProcedure;
            cmdMSPowerpointInsert.ExecuteNonQuery();

            SqlCommand cmdInternetInsert = new SqlCommand("JobApplication_Insert_ITKnowledge", sqlConnection);
            cmdInternetInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdInternetInsert.Parameters.Add(new SqlParameter("@Software_Name", "Internet"));
            cmdInternetInsert.Parameters.Add(new SqlParameter("@Proficiency", ddlInternet.Text));
            cmdInternetInsert.Parameters.Add(new SqlParameter("@Sort_Order", "0"));
            cmdInternetInsert.CommandType = CommandType.StoredProcedure;
            cmdInternetInsert.ExecuteNonQuery();

            //IT Knowledge - Dynamic Start
            rowIndex = 0;
            string proficiency = "";
            if (ViewState["CurrentITKnowledgeTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentITKnowledgeTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the values
                        TextBox text1 = (TextBox)gvITKnowledge.Rows[rowIndex].Cells[1].FindControl("txtSoftwareName");
                        DropDownList ddl1 = (DropDownList)gvITKnowledge.Rows[rowIndex].Cells[2].FindControl("ddlITKnowledge");

                        dtCurrentTable.Rows[i - 1]["Column1"] = text1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = ddl1.Text;

                        proficiency = (ddl1.Text == "Select Proficiency") ? "" : ddl1.Text;

                        SqlCommand cmdITKnowledgeInsert = new SqlCommand("JobApplication_Insert_ITKnowledge", sqlConnection);
                        cmdITKnowledgeInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
                        cmdITKnowledgeInsert.Parameters.Add(new SqlParameter("@Software_Name", text1.Text));
                        cmdITKnowledgeInsert.Parameters.Add(new SqlParameter("@Proficiency", ddl1.Text));
                        cmdITKnowledgeInsert.Parameters.Add(new SqlParameter("@Sort_Order", i));
                        cmdITKnowledgeInsert.CommandType = CommandType.StoredProcedure;

                        if (proficiency != "" && text1.Text != "")
                            cmdITKnowledgeInsert.ExecuteNonQuery();

                        rowIndex++;
                    }
                }
            }
            //IT Knowledge - Dynamic End

            SqlCommand cmdEmploymentDelete = new SqlCommand("DELETE FROM JobApplication_EmploymentHistory WHERE Ja_Id = '" + applicationID + "'", sqlConnection);
            cmdEmploymentDelete.ExecuteNonQuery();
            //SAVE EMPLOYMENT HISTORY INFO IF ANY
            string Employment_start_date = "";
            string Employment_end_date = "";
            string job_type = "";
            Employment_start_date = string.IsNullOrEmpty(txtStartDate1.Text) ? "" : Convert.ToDateTime(txtStartDate1.Text).Month + "/" + Convert.ToDateTime(txtStartDate1.Text).Day + "/" + Convert.ToDateTime(txtStartDate1.Text).Year;
            Employment_end_date = string.IsNullOrEmpty(txtEndDate1.Text) ? "" : Convert.ToDateTime(txtEndDate1.Text).Month + "/" + Convert.ToDateTime(txtEndDate1.Text).Day + "/" + Convert.ToDateTime(txtEndDate1.Text).Year;
            job_type = (ddlJobType1.Text == "Select Job Type") ? "" : ddlJobType1.Text;
            SqlCommand cmdEmployment1Insert = new SqlCommand("JobApplication_Insert_EmploymentHistory", sqlConnection);
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Start_Date", Employment_start_date));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@End_Date", Employment_end_date));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Employer", txtEmployer1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Location", txtLocation1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Position", txtPosition1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Job_Scope", txtJobScope1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Job_Type", job_type));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Work_Hours", txtWorkHour1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Salary_Drawn", txtSalaryDrawn1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Leaving_Reason", txtLeavingReason1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Reference_Name", txtReferenceName1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Reference_Designation", txtReferenceDesignation1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Reference_ContactNo", txtReferenceContactNo1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Reference_Email", txtReferenceEmail1.Text));
            cmdEmployment1Insert.Parameters.Add(new SqlParameter("@Sort_Order", "1"));
            cmdEmployment1Insert.CommandType = CommandType.StoredProcedure;
            if (ddlTotalWorkExperience.Text == "1" || ddlTotalWorkExperience.Text == "2" || ddlTotalWorkExperience.Text == "3"
                || ddlTotalWorkExperience.Text == "4" || ddlTotalWorkExperience.Text == "5")
                cmdEmployment1Insert.ExecuteNonQuery();

            Employment_start_date = string.IsNullOrEmpty(txtStartDate2.Text) ? "" : Convert.ToDateTime(txtStartDate2.Text).Month + "/" + Convert.ToDateTime(txtStartDate2.Text).Day + "/" + Convert.ToDateTime(txtStartDate2.Text).Year;
            Employment_end_date = string.IsNullOrEmpty(txtEndDate2.Text) ? "" : Convert.ToDateTime(txtEndDate2.Text).Month + "/" + Convert.ToDateTime(txtEndDate2.Text).Day + "/" + Convert.ToDateTime(txtEndDate2.Text).Year;
            job_type = (ddlJobType2.Text == "Select Job Type") ? "" : ddlJobType2.Text;
            SqlCommand cmdEmployment2Insert = new SqlCommand("JobApplication_Insert_EmploymentHistory", sqlConnection);
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Start_Date", Employment_start_date));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@End_Date", Employment_end_date));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Employer", txtEmployer2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Location", txtLocation2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Position", txtPosition2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Job_Scope", txtJobScope2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Job_Type", job_type));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Work_Hours", txtWorkHour2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Salary_Drawn", txtSalaryDrawn2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Leaving_Reason", txtLeavingReason2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Reference_Name", txtReferenceName2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Reference_Designation", txtReferenceDesignation2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Reference_ContactNo", txtReferenceContactNo2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Reference_Email", txtReferenceEmail2.Text));
            cmdEmployment2Insert.Parameters.Add(new SqlParameter("@Sort_Order", "2"));
            cmdEmployment2Insert.CommandType = CommandType.StoredProcedure;
            if (ddlTotalWorkExperience.Text == "2" || ddlTotalWorkExperience.Text == "3"
                || ddlTotalWorkExperience.Text == "4" || ddlTotalWorkExperience.Text == "5")
                cmdEmployment2Insert.ExecuteNonQuery();

            Employment_start_date = string.IsNullOrEmpty(txtStartDate3.Text) ? "" : Convert.ToDateTime(txtStartDate3.Text).Month + "/" + Convert.ToDateTime(txtStartDate3.Text).Day + "/" + Convert.ToDateTime(txtStartDate3.Text).Year;
            Employment_end_date = string.IsNullOrEmpty(txtEndDate3.Text) ? "" : Convert.ToDateTime(txtEndDate3.Text).Month + "/" + Convert.ToDateTime(txtEndDate3.Text).Day + "/" + Convert.ToDateTime(txtEndDate3.Text).Year;
            job_type = (ddlJobType3.Text == "Select Job Type") ? "" : ddlJobType3.Text;
            SqlCommand cmdEmployment3Insert = new SqlCommand("JobApplication_Insert_EmploymentHistory", sqlConnection);
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Start_Date", Employment_start_date));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@End_Date", Employment_end_date));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Employer", txtEmployer3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Location", txtLocation3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Position", txtPosition3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Job_Scope", txtJobScope3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Job_Type", job_type));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Work_Hours", txtWorkHour3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Salary_Drawn", txtSalaryDrawn3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Leaving_Reason", txtLeavingReason3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Reference_Name", txtReferenceName3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Reference_Designation", txtReferenceDesignation3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Reference_ContactNo", txtReferenceContactNo3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Reference_Email", txtReferenceEmail3.Text));
            cmdEmployment3Insert.Parameters.Add(new SqlParameter("@Sort_Order", "3"));
            cmdEmployment3Insert.CommandType = CommandType.StoredProcedure;
            if (ddlTotalWorkExperience.Text == "3" || ddlTotalWorkExperience.Text == "4" || ddlTotalWorkExperience.Text == "5")
                cmdEmployment3Insert.ExecuteNonQuery();

            Employment_start_date = string.IsNullOrEmpty(txtStartDate4.Text) ? "" : Convert.ToDateTime(txtStartDate4.Text).Month + "/" + Convert.ToDateTime(txtStartDate4.Text).Day + "/" + Convert.ToDateTime(txtStartDate4.Text).Year;
            Employment_end_date = string.IsNullOrEmpty(txtEndDate4.Text) ? "" : Convert.ToDateTime(txtEndDate4.Text).Month + "/" + Convert.ToDateTime(txtEndDate4.Text).Day + "/" + Convert.ToDateTime(txtEndDate4.Text).Year;
            job_type = (ddlJobType4.Text == "Select Job Type") ? "" : ddlJobType4.Text;
            SqlCommand cmdEmployment4Insert = new SqlCommand("JobApplication_Insert_EmploymentHistory", sqlConnection);
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Start_Date", Employment_start_date));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@End_Date", Employment_end_date));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Employer", txtEmployer4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Location", txtLocation4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Position", txtPosition4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Job_Scope", txtJobScope4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Job_Type", job_type));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Work_Hours", txtWorkHour4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Salary_Drawn", txtSalaryDrawn4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Leaving_Reason", txtLeavingReason4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Reference_Name", txtReferenceName4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Reference_Designation", txtReferenceDesignation4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Reference_ContactNo", txtReferenceContactNo4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Reference_Email", txtReferenceEmail4.Text));
            cmdEmployment4Insert.Parameters.Add(new SqlParameter("@Sort_Order", "4"));
            cmdEmployment4Insert.CommandType = CommandType.StoredProcedure;
            if (ddlTotalWorkExperience.Text == "4" || ddlTotalWorkExperience.Text == "5")
                cmdEmployment4Insert.ExecuteNonQuery();

            Employment_start_date = string.IsNullOrEmpty(txtStartDate5.Text) ? "" : Convert.ToDateTime(txtStartDate5.Text).Month + "/" + Convert.ToDateTime(txtStartDate5.Text).Day + "/" + Convert.ToDateTime(txtStartDate5.Text).Year;
            Employment_end_date = string.IsNullOrEmpty(txtEndDate5.Text) ? "" : Convert.ToDateTime(txtEndDate5.Text).Month + "/" + Convert.ToDateTime(txtEndDate5.Text).Day + "/" + Convert.ToDateTime(txtEndDate5.Text).Year;
            job_type = (ddlJobType5.Text == "Select Job Type") ? "" : ddlJobType5.Text;
            SqlCommand cmdEmployment5Insert = new SqlCommand("JobApplication_Insert_EmploymentHistory", sqlConnection);
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Start_Date", Employment_start_date));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@End_Date", Employment_end_date));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Employer", txtEmployer5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Location", txtLocation5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Position", txtPosition5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Job_Scope", txtJobScope5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Job_Type", job_type));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Work_Hours", txtWorkHour5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Salary_Drawn", txtSalaryDrawn5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Leaving_Reason", txtLeavingReason5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Reference_Name", txtReferenceName5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Reference_Designation", txtReferenceDesignation5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Reference_ContactNo", txtReferenceContactNo5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Reference_Email", txtReferenceEmail5.Text));
            cmdEmployment5Insert.Parameters.Add(new SqlParameter("@Sort_Order", "5"));
            cmdEmployment5Insert.CommandType = CommandType.StoredProcedure;
            if (ddlTotalWorkExperience.Text == "5")
                cmdEmployment5Insert.ExecuteNonQuery();

            SqlCommand cmdLanguageDelete = new SqlCommand("DELETE FROM JobApplication_Languages WHERE Ja_Id = '" + applicationID + "'", sqlConnection);
            cmdLanguageDelete.ExecuteNonQuery();
            //SAVE LANGUAGE INFO
            //Language - Dynamic Start
            rowIndex = 0;
            string language = "";
            string spoken = "";
            string written = "";
            string reading = "";
            if (ViewState["CurrentLanguageTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentLanguageTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the values
                        DropDownList ddl1 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[0].FindControl("ddlLanguage");
                        DropDownList ddl2 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[1].FindControl("ddlSpokenSkill");
                        DropDownList ddl3 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[2].FindControl("ddlWrittenSkill");
                        DropDownList ddl4 = (DropDownList)gvLanguage.Rows[rowIndex].Cells[3].FindControl("ddlReadingSkill");

                        dtCurrentTable.Rows[i - 1]["Column1"] = ddl1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = ddl2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = ddl3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = ddl4.Text;

                        language = (ddl1.Text == "Select Language") ? "" : ddl1.Text;
                        spoken = (ddl2.Text == "Select Proficiency") ? "" : ddl2.Text;
                        written = (ddl3.Text == "Select Proficiency") ? "" : ddl3.Text;
                        reading = (ddl4.Text == "Select Proficiency") ? "" : ddl4.Text;

                        SqlCommand cmdLanguageInsert = new SqlCommand("JobApplication_Insert_Languages", sqlConnection);
                        cmdLanguageInsert.Parameters.Add(new SqlParameter("@Ja_Id", applicationID));
                        cmdLanguageInsert.Parameters.Add(new SqlParameter("@Language", language));
                        cmdLanguageInsert.Parameters.Add(new SqlParameter("@Spoken", spoken));
                        cmdLanguageInsert.Parameters.Add(new SqlParameter("@Written", written));
                        cmdLanguageInsert.Parameters.Add(new SqlParameter("@Reading", reading));
                        cmdLanguageInsert.Parameters.Add(new SqlParameter("@Sort_Order", i));
                        cmdLanguageInsert.CommandType = CommandType.StoredProcedure;
                        cmdLanguageInsert.ExecuteNonQuery();

                        rowIndex++;
                    }
                }
            }
            //Language - Dynamic End

            sqlConnection.Close();

            Response.Redirect("ThankYou.htm");
        }
        //SAVE DATA END

    }

    //CUSTOMVALIDATOR:SERVER START
    protected void ServerValidate_Spouse(object sender, ServerValidateEventArgs e)
    {
        if (ddlMaritalStatus.SelectedValue == "Married")
        {
            if (txtSpouseName.Text != "" && txtSpouseNRIC.Text != "" && txtSpouseDOB.Text != ""
                && ddlSpouseNationality.SelectedIndex != 0 && txtSpouseEmployer.Text != "" && txtSpouseOccupation.Text != "")
            {
                e.IsValid = true;
            }
            else
                e.IsValid = false;
        }
        else
            e.IsValid = true;
    }
    //CUSTOMVALIDATOR:SERVER END   
}
