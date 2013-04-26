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

public partial class PrintForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("HRLogin.aspx");
        }
        else
        {
            string username = Session["username"].ToString();
            string password = Session["password"].ToString();
        }
        string applicationID = Request["value"].ToString();

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

        string strConnection = ConfigurationManager.ConnectionStrings["CS_HumanResource"].ToString();
        SqlConnection sqlConnection = new SqlConnection(strConnection);

        sqlConnection.Open();

        SqlCommand cmdSelectParticulars = new SqlCommand("JobApplication_Select_Particulars", sqlConnection);
        cmdSelectParticulars.Parameters.Add(new SqlParameter("@id", applicationID));
        cmdSelectParticulars.CommandType = CommandType.StoredProcedure;
        SqlDataReader readerParticulars = cmdSelectParticulars.ExecuteReader();
        if (readerParticulars.HasRows)
        {
            while (readerParticulars.Read())
            {
                lblApplicationDate.Text = readerParticulars[65].ToString();
                txtPosition.Text = readerParticulars[2].ToString();
                if (readerParticulars[3].ToString() != "")
                {
                    divPositionOthers.Visible = true;
                    txtPositionOthers.Text = readerParticulars[3].ToString();
                }
                if (readerParticulars[4].ToString() != "")
                {
                    lblEnableRotation.Visible = true;
                    lblEnableRotationYesNo.Text = readerParticulars[4].ToString();
                }
                txtPreferredJobType.Text = readerParticulars[5].ToString();
                if (readerParticulars[6].ToString() != "" && readerParticulars[7].ToString() != "")
                {
                    divPeriodAvailabe.Visible = true;
                    txtAvailableStartDate.Text = readerParticulars[6].ToString() == "" ? "" : DateTime.Parse(readerParticulars[6].ToString()).ToString("dd/MM/yyyy");
                    txtAvailableEndDate.Text = readerParticulars[7].ToString() == "" ? "" : DateTime.Parse(readerParticulars[7].ToString()).ToString("dd/MM/yyyy");
                }
                txtName.Text = readerParticulars[8].ToString();
                txtAlias.Text = readerParticulars[9].ToString();
                txtNRIC.Text = readerParticulars[16].ToString();
                txtCitizenship.Text = readerParticulars[23].ToString();
                if (readerParticulars[24].ToString() != "")
                {
                    lblPRIssuedDate.Visible = true;
                    txtPRIssuedDate.Visible = true;
                    txtPRIssuedDate.Text = readerParticulars[24].ToString() == "" ? "" : DateTime.Parse(readerParticulars[24].ToString()).ToString("dd/MM/yyyy");
                }
                txtPassport.Text = readerParticulars[17].ToString();
                txtExpiryDate.Text = readerParticulars[18].ToString() == "" ? "" : DateTime.Parse(readerParticulars[18].ToString()).ToString("dd/MM/yyyy");
                txtNationality.Text = readerParticulars[25].ToString();
                txtDOB.Text = readerParticulars[19].ToString() == "" ? "" : DateTime.Parse(readerParticulars[19].ToString()).ToString("dd/MM/yyyy");
                txtRace.Text = readerParticulars[26].ToString();
                if (readerParticulars[27].ToString() != "")
                {
                    lblRaceOthers.Visible = true;
                    txtRaceOthers.Visible = true;
                    txtRaceOthers.Text = readerParticulars[27].ToString();
                }
                txtGender.Text = readerParticulars[21].ToString();
                txtReligion.Text = readerParticulars[28].ToString();
                txtMaritalStatus.Text = readerParticulars[22].ToString();
                txtResidentialAddr.Text = readerParticulars[10].ToString();
                txtHomeCountryAddr.Text = readerParticulars[11].ToString();
                txtEmailAddr.Text = readerParticulars[12].ToString();
                txtHomeNo.Text = readerParticulars[13].ToString();
                txtMobileNo.Text = readerParticulars[14].ToString();
                txtOverseasNo.Text = readerParticulars[15].ToString();
                txtContactPersonName.Text = readerParticulars[29].ToString();
                txtContactPersonRelationship.Text = readerParticulars[30].ToString();
                txtContactPersonNo.Text = readerParticulars[31].ToString();
                txtContactPersonOverseasNo.Text = readerParticulars[32].ToString();
                txtContactPersonAddr.Text = readerParticulars[33].ToString();
                if ((readerParticulars[23].ToString() == "Singaporean" && readerParticulars[21].ToString() == "Male")
                    || (readerParticulars[23].ToString() == "Singapore PR" && readerParticulars[21].ToString() == "Male"))
                {
                    divNS.Visible = true;
                }
                else
                    divNS.Visible = false;
                txtNSStatus.Text = readerParticulars[34].ToString();
                txtNSDateBegan.Text = readerParticulars[35].ToString() == "" ? "" : DateTime.Parse(readerParticulars[35].ToString()).ToString("dd/MM/yyyy");
                txtNSRank.Text = readerParticulars[37].ToString();
                txtNSDateCompletion.Text = readerParticulars[36].ToString() == "" ? "" : DateTime.Parse(readerParticulars[36].ToString()).ToString("dd/MM/yyyy");
                txtNSUnit.Text = readerParticulars[38].ToString();
                txtNSNextDate.Text = readerParticulars[39].ToString() == "" ? "" : DateTime.Parse(readerParticulars[39].ToString()).ToString("dd/MM/yyyy");
                txtNSNextLength.Text = readerParticulars[40].ToString();
                txtWordsPerMins.Text = readerParticulars[41].ToString();
                txtTotalWorkExperience.Text = readerParticulars[42].ToString();
                if (readerParticulars[42].ToString() != "0")
                {
                    chkCurrentWorking.Visible = true;
                    lblCurrentWorking.Visible = true;
                }
                chkCurrentWorking.Checked = readerParticulars[43].ToString() == "True" ? true : false;
                txtLanguagesOth.Text = readerParticulars[44].ToString();
                txtExpectedSalary.Text = readerParticulars[45].ToString();
                chkNegotiable.Checked = readerParticulars[46].ToString() == "True" ? true : false;
                txtNoticePeriod.Text = readerParticulars[47].ToString();
                if (readerParticulars[48].ToString() != "")
                {
                    divAvailableDate.Visible = true;
                    txtAvailableDate.Text = readerParticulars[48].ToString() == "" ? "" : DateTime.Parse(readerParticulars[48].ToString()).ToString("dd/MM/yyyy");
                }
                lblAttachedFiles.Text = readerParticulars[69].ToString();
                rdListKnownStaff.Text = readerParticulars[49].ToString();
                txtStaffName.Text = readerParticulars[50].ToString();
                txtStaffRelationship.Text = readerParticulars[51].ToString();
                rdListWorkTeleCentreBefore.Text = readerParticulars[52].ToString();
                txtTCStartDate.Text = readerParticulars[53].ToString() == "" ? "" : DateTime.Parse(readerParticulars[53].ToString()).ToString("dd/MM/yyyy");
                txtTCEndDate.Text = readerParticulars[54].ToString() == "" ? "" : DateTime.Parse(readerParticulars[54].ToString()).ToString("dd/MM/yyyy");
                txtTCPosition.Text = readerParticulars[55].ToString();
                rdListPhysicalImpairment.Text = readerParticulars[56].ToString();
                rdListMajorDiseaseIllness.Text = readerParticulars[57].ToString();
                rdListMinorDiseaseIllness.Text = readerParticulars[58].ToString();
                rdListLongtermMedication.Text = readerParticulars[59].ToString();
                rdListBankruptcyStatus.Text = readerParticulars[60].ToString();
                rdListConvictedOffence.Text = readerParticulars[61].ToString();
                rdListDismissedSuspended.Text = readerParticulars[62].ToString();
                txtDetailsAnswer.Text = readerParticulars[63].ToString();                
                chkDeclarationStatement.Checked = readerParticulars[64].ToString() == "True" ? true : false;
            }
        }
        readerParticulars.Close();

        SqlCommand cmdSelectSpouse = new SqlCommand("JobApplication_Select_Spouse", sqlConnection);
        cmdSelectSpouse.Parameters.Add(new SqlParameter("@id", applicationID));
        cmdSelectSpouse.CommandType = CommandType.StoredProcedure;
        SqlDataReader readerSpouse = cmdSelectSpouse.ExecuteReader();
        if (readerSpouse.HasRows)
        {
            divSpouseParticular.Visible = true;
            while (readerSpouse.Read())
            {
                txtSpouseName.Text = readerSpouse[1].ToString();
                txtSpouseNRIC.Text = readerSpouse[2].ToString();
                txtSpouseDOB.Text = readerSpouse[3].ToString() == "" ? "" : DateTime.Parse(readerSpouse[3].ToString()).ToString("dd/MM/yyyy");
                txtSpouseNationality.Text = readerSpouse[5].ToString();
                txtSpouseEmployer.Text = readerSpouse[7].ToString();
                txtSpouseOccupation.Text = readerSpouse[6].ToString();
            }
        }
        else
            divSpouseParticular.Visible = false;
        readerSpouse.Close();

        SqlDataAdapter daChildren = new SqlDataAdapter("SELECT * FROM JobApplication_Relationships WHERE (Ja_Id = '" + applicationID + "') AND (Relationship = '" + "Son" + "' OR Relationship = '" + "Daughter" + "')", sqlConnection);
        DataTable dtChildren = new DataTable();
        daChildren.Fill(dtChildren);
        gvChildren.DataSource = dtChildren;
        gvChildren.DataBind();

        SqlDataAdapter daParents = new SqlDataAdapter("SELECT * FROM JobApplication_Relationships WHERE (Ja_Id = '" + applicationID + "') AND (Relationship = '" + "Father" + "' OR Relationship = '" + "Mother" + "')", sqlConnection);
        DataTable dtParents = new DataTable();
        daParents.Fill(dtParents);
        gvParents.DataSource = dtParents;
        gvParents.DataBind();

        SqlDataAdapter daSibling = new SqlDataAdapter("SELECT * FROM JobApplication_Relationships WHERE (Ja_Id = '" + applicationID + "') AND (Relationship = '" + "Brother" + "' OR Relationship = '" + "Sister" + "')", sqlConnection);
        DataTable dtSibling = new DataTable();
        daSibling.Fill(dtSibling);
        gvSibling.DataSource = dtSibling;
        gvSibling.DataBind();

        SqlDataAdapter daEducation = new SqlDataAdapter("SELECT * FROM JobApplication_Education WHERE (Ja_Id = '" + applicationID + "')", sqlConnection);
        DataTable dtEducation = new DataTable();
        daEducation.Fill(dtEducation);
        gvEducation.DataSource = dtEducation;
        gvEducation.DataBind();

        SqlDataAdapter daCourse = new SqlDataAdapter("SELECT * FROM JobApplication_Qualification WHERE (Ja_Id = '" + applicationID + "') AND (Qualification_Type = '" + "Training" + "')", sqlConnection);
        DataTable dtCourse = new DataTable();
        daCourse.Fill(dtCourse);
        gvCourse.DataSource = dtCourse;
        gvCourse.DataBind();

        SqlDataAdapter daCertificate = new SqlDataAdapter("SELECT * FROM JobApplication_Qualification WHERE (Ja_Id = '" + applicationID + "') AND (Qualification_Type = '" + "Certificate" + "')", sqlConnection);
        DataTable dtCertificate = new DataTable();
        daCertificate.Fill(dtCertificate);
        gvCertificate.DataSource = dtCertificate;
        gvCertificate.DataBind();

        SqlDataAdapter daITKnowledge = new SqlDataAdapter("SELECT * FROM JobApplication_ITKnowledge WHERE (Ja_Id = '" + applicationID + "')", sqlConnection);
        DataTable dtITKnowledge = new DataTable();
        daITKnowledge.Fill(dtITKnowledge);
        gvITKnowledge.DataSource = dtITKnowledge;
        gvITKnowledge.DataBind();

        SqlDataAdapter daLanguage = new SqlDataAdapter("SELECT * FROM JobApplication_Languages WHERE (Ja_Id = '" + applicationID + "')", sqlConnection);
        DataTable dtLanguage = new DataTable();
        daLanguage.Fill(dtLanguage);
        gvLanguage.DataSource = dtLanguage;
        gvLanguage.DataBind();

        SqlCommand cmdSelectEmploymentHistory1 = new SqlCommand("JobApplication_Select_EmploymentHistory", sqlConnection);
        cmdSelectEmploymentHistory1.Parameters.Add(new SqlParameter("@id", applicationID));
        cmdSelectEmploymentHistory1.Parameters.Add(new SqlParameter("@Sort_Order", 1));
        cmdSelectEmploymentHistory1.CommandType = CommandType.StoredProcedure;
        SqlDataReader readerEmploymentHistory1 = cmdSelectEmploymentHistory1.ExecuteReader();
        if (readerEmploymentHistory1.HasRows)
        {
            divWorkExperience1.Visible = true;
            while (readerEmploymentHistory1.Read())
            {
                txtStartDate1.Text = readerEmploymentHistory1[1].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory1[1].ToString()).ToString("dd/MM/yyyy");
                txtEndDate1.Text = readerEmploymentHistory1[2].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory1[2].ToString()).ToString("dd/MM/yyyy");
                txtEmployer1.Text = readerEmploymentHistory1[3].ToString();
                txtLocation1.Text = readerEmploymentHistory1[4].ToString();
                txtPosition1.Text = readerEmploymentHistory1[5].ToString();
                txtJobScope1.Text = readerEmploymentHistory1[6].ToString();
                txtJobType1.Text = readerEmploymentHistory1[7].ToString();
                txtWorkHour1.Text = readerEmploymentHistory1[8].ToString();
                txtSalaryDrawn1.Text = readerEmploymentHistory1[9].ToString();
                txtLeavingReason1.Text = readerEmploymentHistory1[10].ToString();
                txtReferenceName1.Text = readerEmploymentHistory1[11].ToString();
                txtReferenceDesignation1.Text = readerEmploymentHistory1[12].ToString();
                txtReferenceContactNo1.Text = readerEmploymentHistory1[13].ToString();
                txtReferenceEmail1.Text = readerEmploymentHistory1[14].ToString();
            }
        }
        readerEmploymentHistory1.Close();

        SqlCommand cmdSelectEmploymentHistory2 = new SqlCommand("JobApplication_Select_EmploymentHistory", sqlConnection);
        cmdSelectEmploymentHistory2.Parameters.Add(new SqlParameter("@id", applicationID));
        cmdSelectEmploymentHistory2.Parameters.Add(new SqlParameter("@Sort_Order", 2));
        cmdSelectEmploymentHistory2.CommandType = CommandType.StoredProcedure;
        SqlDataReader readerEmploymentHistory2 = cmdSelectEmploymentHistory2.ExecuteReader();
        if (readerEmploymentHistory2.HasRows)
        {
            divInstruction.Visible = true;
            divWorkExperience2.Visible = true;
            while (readerEmploymentHistory2.Read())
            {
                txtStartDate2.Text = readerEmploymentHistory2[1].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory2[1].ToString()).ToString("dd/MM/yyyy");
                txtEndDate2.Text = readerEmploymentHistory2[2].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory2[2].ToString()).ToString("dd/MM/yyyy");
                txtEmployer2.Text = readerEmploymentHistory2[3].ToString();
                txtLocation2.Text = readerEmploymentHistory2[4].ToString();
                txtPosition2.Text = readerEmploymentHistory2[5].ToString();
                txtJobScope2.Text = readerEmploymentHistory2[6].ToString();
                txtJobType2.Text = readerEmploymentHistory2[7].ToString();
                txtWorkHour2.Text = readerEmploymentHistory2[8].ToString();
                txtSalaryDrawn2.Text = readerEmploymentHistory2[9].ToString();
                txtLeavingReason2.Text = readerEmploymentHistory2[10].ToString();
                txtReferenceName2.Text = readerEmploymentHistory2[11].ToString();
                txtReferenceDesignation2.Text = readerEmploymentHistory2[12].ToString();
                txtReferenceContactNo2.Text = readerEmploymentHistory2[13].ToString();
                txtReferenceEmail2.Text = readerEmploymentHistory2[14].ToString();
            }
        }
        readerEmploymentHistory2.Close();

        SqlCommand cmdSelectEmploymentHistory3 = new SqlCommand("JobApplication_Select_EmploymentHistory", sqlConnection);
        cmdSelectEmploymentHistory3.Parameters.Add(new SqlParameter("@id", applicationID));
        cmdSelectEmploymentHistory3.Parameters.Add(new SqlParameter("@Sort_Order", 3));
        cmdSelectEmploymentHistory3.CommandType = CommandType.StoredProcedure;
        SqlDataReader readerEmploymentHistory3 = cmdSelectEmploymentHistory3.ExecuteReader();
        if (readerEmploymentHistory3.HasRows)
        {
            divWorkExperience3.Visible = true;
            while (readerEmploymentHistory3.Read())
            {
                txtStartDate3.Text = readerEmploymentHistory3[1].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory3[1].ToString()).ToString("dd/MM/yyyy");
                txtEndDate3.Text = readerEmploymentHistory3[2].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory3[2].ToString()).ToString("dd/MM/yyyy");
                txtEmployer3.Text = readerEmploymentHistory3[3].ToString();
                txtLocation3.Text = readerEmploymentHistory3[4].ToString();
                txtPosition3.Text = readerEmploymentHistory3[5].ToString();
                txtJobScope3.Text = readerEmploymentHistory3[6].ToString();
                txtJobType3.Text = readerEmploymentHistory3[7].ToString();
                txtWorkHour3.Text = readerEmploymentHistory3[8].ToString();
                txtSalaryDrawn3.Text = readerEmploymentHistory3[9].ToString();
                txtLeavingReason3.Text = readerEmploymentHistory3[10].ToString();
                txtReferenceName3.Text = readerEmploymentHistory3[11].ToString();
                txtReferenceDesignation3.Text = readerEmploymentHistory3[12].ToString();
                txtReferenceContactNo3.Text = readerEmploymentHistory3[13].ToString();
                txtReferenceEmail3.Text = readerEmploymentHistory3[14].ToString();
            }
        }
        readerEmploymentHistory3.Close();

        SqlCommand cmdSelectEmploymentHistory4 = new SqlCommand("JobApplication_Select_EmploymentHistory", sqlConnection);
        cmdSelectEmploymentHistory4.Parameters.Add(new SqlParameter("@id", applicationID));
        cmdSelectEmploymentHistory4.Parameters.Add(new SqlParameter("@Sort_Order", 4));
        cmdSelectEmploymentHistory4.CommandType = CommandType.StoredProcedure;
        SqlDataReader readerEmploymentHistory4 = cmdSelectEmploymentHistory4.ExecuteReader();
        if (readerEmploymentHistory4.HasRows)
        {
            divWorkExperience4.Visible = true;
            while (readerEmploymentHistory4.Read())
            {
                txtStartDate4.Text = readerEmploymentHistory4[1].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory4[1].ToString()).ToString("dd/MM/yyyy");
                txtEndDate4.Text = readerEmploymentHistory4[2].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory4[2].ToString()).ToString("dd/MM/yyyy");
                txtEmployer4.Text = readerEmploymentHistory4[3].ToString();
                txtLocation4.Text = readerEmploymentHistory4[4].ToString();
                txtPosition4.Text = readerEmploymentHistory4[5].ToString();
                txtJobScope4.Text = readerEmploymentHistory4[6].ToString();
                txtJobType4.Text = readerEmploymentHistory4[7].ToString();
                txtWorkHour4.Text = readerEmploymentHistory4[8].ToString();
                txtSalaryDrawn4.Text = readerEmploymentHistory4[9].ToString();
                txtLeavingReason4.Text = readerEmploymentHistory4[10].ToString();
                txtReferenceName4.Text = readerEmploymentHistory4[11].ToString();
                txtReferenceDesignation4.Text = readerEmploymentHistory4[12].ToString();
                txtReferenceContactNo4.Text = readerEmploymentHistory4[13].ToString();
                txtReferenceEmail4.Text = readerEmploymentHistory4[14].ToString();
            }
        }
        readerEmploymentHistory4.Close();

        SqlCommand cmdSelectEmploymentHistory5 = new SqlCommand("JobApplication_Select_EmploymentHistory", sqlConnection);
        cmdSelectEmploymentHistory5.Parameters.Add(new SqlParameter("@id", applicationID));
        cmdSelectEmploymentHistory5.Parameters.Add(new SqlParameter("@Sort_Order", 5));
        cmdSelectEmploymentHistory5.CommandType = CommandType.StoredProcedure;
        SqlDataReader readerEmploymentHistory5 = cmdSelectEmploymentHistory5.ExecuteReader();
        if (readerEmploymentHistory5.HasRows)
        {
            divWorkExperience5.Visible = true;
            while (readerEmploymentHistory5.Read())
            {
                txtStartDate5.Text = readerEmploymentHistory5[1].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory5[1].ToString()).ToString("dd/MM/yyyy");
                txtEndDate5.Text = readerEmploymentHistory5[2].ToString() == "" ? "" : DateTime.Parse(readerEmploymentHistory5[2].ToString()).ToString("dd/MM/yyyy");
                txtEmployer5.Text = readerEmploymentHistory5[3].ToString();
                txtLocation5.Text = readerEmploymentHistory5[4].ToString();
                txtPosition5.Text = readerEmploymentHistory5[5].ToString();
                txtJobScope5.Text = readerEmploymentHistory5[6].ToString();
                txtJobType5.Text = readerEmploymentHistory5[7].ToString();
                txtWorkHour5.Text = readerEmploymentHistory5[8].ToString();
                txtSalaryDrawn5.Text = readerEmploymentHistory5[9].ToString();
                txtLeavingReason5.Text = readerEmploymentHistory5[10].ToString();
                txtReferenceName5.Text = readerEmploymentHistory5[11].ToString();
                txtReferenceDesignation5.Text = readerEmploymentHistory5[12].ToString();
                txtReferenceContactNo5.Text = readerEmploymentHistory5[13].ToString();
                txtReferenceEmail5.Text = readerEmploymentHistory5[14].ToString();
            }
        }
        readerEmploymentHistory5.Close();

        sqlConnection.Close();
        Page.DataBind();
    }
}
