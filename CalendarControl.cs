using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CalendarControl : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        else
        {
            ArrayList alYear = new ArrayList();
            alYear.Add("*Year*");
            for (int i = DateTime.Now.Year + 5; i >= (DateTime.Now.Year - 70); i--)
            {
                alYear.Add(i);
            }
            ddlYear.DataSource = alYear;
            ddlYear.DataBind();
        }
    }

    #region Regular expressions
    private static Regex regPrevMonth = new Regex(
    @"(?<PrevMonth><a.*?>&lt;</a>)",
    RegexOptions.IgnoreCase
    | RegexOptions.Singleline
    | RegexOptions.CultureInvariant
    | RegexOptions.IgnorePatternWhitespace
    | RegexOptions.Compiled
    );

    private static Regex regNextMonth = new Regex(
    @"(?<NextMonth><a.*?>&gt;</a>)",
    RegexOptions.IgnoreCase
    | RegexOptions.Singleline
    | RegexOptions.CultureInvariant
    | RegexOptions.IgnorePatternWhitespace
    | RegexOptions.Compiled
    );
    #endregion

    protected override void Render(HtmlTextWriter writer)
    {
        // turn user control to html code
        string output = CalendarControl.RenderToString(calDate);

        MatchEvaluator mevm = new MatchEvaluator(AppendMonth);
        output = regPrevMonth.Replace(output, mevm);

        MatchEvaluator mevy = new MatchEvaluator(AppendYear);
        output = regNextMonth.Replace(output, mevy);
        // output the modified code
        writer.Write(output);
    }

    // The date displayed on the popup calendar
    public DateTime? SelectedDate
    {
        get
        {
            // null date stored or not set
            if (ViewState["SelectedDate"] == null)
            {
                return null;
            }
            return (DateTime)ViewState["SelectedDate"];
        }
        set
        {
            ViewState["SelectedDate"] = value;
            if (value != null)
            {
                calDate.SelectedDate = (DateTime)value;
                calDate.VisibleDate = (DateTime)value;
            }
            else
            {
                calDate.SelectedDate = new DateTime(0);
                calDate.VisibleDate = DateTime.Now.Date;
            }
        }
    }

    private string AppendMonth(Match m)
    {
        return CalendarControl.RenderToString(ddlMonth);
    }

    private string AppendYear(Match m)
    {
        return CalendarControl.RenderToString(ddlYear);
    }

    public static string RenderToString(Control c)
    {
        bool previousVisibility = c.Visible;
        c.Visible = true; // make visible if not

        // get html code for control
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter localWriter = new HtmlTextWriter(sw);
        c.RenderControl(localWriter);
        string output = sw.ToString();

        // restore visibility
        c.Visible = previousVisibility;

        return output;
    }

    protected void ddlYearSelectedIndexChanged(object sender, EventArgs e)
    {
        GetSelectedDate();
    }

    protected void ddlMonthSelectedIndexChanged(object sender, EventArgs e)
    {
        GetSelectedDate();
    }

    private void GetSelectedDate()
    {
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        if (ddlMonth.SelectedValue != "00")
        {
            month = Convert.ToInt32(ddlMonth.SelectedValue);
        }
        if (ddlYear.SelectedValue != "*Year*")
        {
            year = Convert.ToInt32(ddlYear.SelectedValue);
        }
        string date = DateTime.Now.Day + "/" + month + "/" + year;
        calDate.VisibleDate = Convert.ToDateTime(date);
    }

    protected void calDate_DayRender(object sender, DayRenderEventArgs e)
    {
        HyperLink hlnk = new HyperLink();
        hlnk.Text = ((LiteralControl)e.Cell.Controls[0]).Text;
        hlnk.Attributes.Add("href", "javascript:SetDate('" +
        e.Day.Date.ToString("dd/MM/yyyy") + "')");
        e.Cell.Controls.Clear();
        e.Cell.Controls.Add(hlnk);
    }

}
