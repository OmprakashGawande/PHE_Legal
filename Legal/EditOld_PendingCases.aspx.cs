using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_EditOld_PendingCases : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
        {
            if (!IsPostBack)
            {
                bindDropDowns();
                string ID = Convert.ToString(Request.QueryString["ID"]);
                FindGridData(sender, e);
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    protected void FindGridData(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "flag", "UniqueNo" },
                new string[] { "2", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtFilingNo.Text = ds.Tables[0].Rows[0]["FilingNo"].ToString();
                txtCourt.Text = ds.Tables[0].Rows[0]["Court"].ToString();
                txtPetitioner.Text = ds.Tables[0].Rows[0]["Petitioner"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                if (ds.Tables[0].Rows[0]["OICId"].ToString() != "")
                {
                    ddlOICNameOpen.ClearSelection();
                    ddlOICNameOpen.Items.FindByValue(ds.Tables[0].Rows[0]["OICId"].ToString()).Selected = true;
                    txtOICMobileNoOpen.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CaseSubjectId"].ToString() != "")
                {
                    ddlCaseSubject.ClearSelection();
                    ddlCaseSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubjectId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CaseSubSubjectId"].ToString() != "")
                {
                    ddlCaseSubject_SelectedIndexChanged( sender,  e);
                    ddlCaseSubSubject.ClearSelection();
                    ddlCaseSubSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubSubjectId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["RespondentOffice"].ToString() != "")
                {
                    string ROIds = ds.Tables[0].Rows[0]["RespondentOffice"].ToString().Trim();
                    string[] RespondentOffice = ROIds.Split(',');
                    if (!string.IsNullOrEmpty(ROIds))
                    {
                        for (int i = 0; i < RespondentOffice.Length; i++)
                        {
                            ddlRespondentOffice.Items.FindByValue(RespondentOffice[i]).Selected = true;
                        }
                    }
                    else
                    {
                        ddlRespondentOffice.SelectedIndex = -1;
                    }
                }
                if (ds.Tables[0].Rows[0]["CaseType"].ToString() != "")
                {
                    txtRespondent.Text = ds.Tables[0].Rows[0]["CaseType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    ddlIsComplaince.ClearSelection();
                    ddlIsComplaince.Items.FindByText(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    private void bindDropDowns()
    {
        try
        {
            dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlOICNameOpen.DataSource = dsCase.Tables[0];
                ddlOICNameOpen.DataTextField = "OICName";
                ddlOICNameOpen.DataValueField = "OICMaster_ID";
                ddlOICNameOpen.DataBind();
                ddlOICNameOpen.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOICNameOpen.DataSource = null;
                ddlOICNameOpen.DataBind();
                ddlOICNameOpen.Items.Insert(0, new ListItem("Select", "0"));
            }

            dsCase = obj.ByDataSet("select Respondent_Office,Respondent_office_Id from tblRespondentOffice");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlRespondentOffice.DataSource = dsCase.Tables[0];
                ddlRespondentOffice.DataTextField = "Respondent_Office";
                ddlRespondentOffice.DataValueField = "Respondent_office_Id";
                ddlRespondentOffice.DataBind();
                // ddlRespondentOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRespondentOffice.DataSource = null;
                ddlRespondentOffice.DataBind();
                ddlRespondentOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlRespondentOffice.ClearSelection();
            ddlRespondentOffice.SelectedIndex = -1;

            dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {

                ddlCaseSubject.DataSource = dsCase.Tables[0];
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataBind();
                ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseSubject.DataSource = null;
                ddlCaseSubject.DataBind();
                ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
            }

            //dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubSubj_Id,CaseSubSubject from tbl_CaseSubSubjectMaster");
            //if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            //{

            //    ddlCaseSubSubject.DataSource = dsCase.Tables[0];
            //    ddlCaseSubSubject.DataTextField = "CaseSubSubject";
            //    ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
            //    ddlCaseSubSubject.DataBind();
            //    ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            //}
            //else
            //{
            //    ddlCaseSubSubject.DataSource = null;
            //    ddlCaseSubSubject.DataBind();
            //    ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            //}
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    public string convertQuotes(string str)
    {
        return str.Replace("'", "''");
    }

    protected void ddlOICNameOpen_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int OICID = Convert.ToInt32(ddlOICNameOpen.SelectedItem.Value);
        dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICID);
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            txtOICMobileNoOpen.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubSubj_Id,CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectId=" + Convert.ToInt32(ddlCaseSubject.SelectedItem.Value));
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {

                ddlCaseSubSubject.DataSource = dsCase.Tables[0];
                ddlCaseSubSubject.DataTextField = "CaseSubSubject";
                ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlCaseSubSubject.DataBind();
                ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseSubSubject.DataSource = null;
                ddlCaseSubSubject.DataBind();
                ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnUpdate.Text == "Update")
            {
                var selItems = ddlRespondentOffice.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => x.Value).ToArray();
                var result = String.Join(",", selItems);

                dsCase = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "OICId", "RespondentOffice", "CaseSubjectId", "CaseSubSubjectId", "OICMobileNo", "Remarks", "Status", "flag", "UniqueNo" },
                     new string[] { ddlOICNameOpen.SelectedValue, result, ddlCaseSubject.SelectedValue, ddlCaseSubSubject.SelectedValue, txtOICMobileNoOpen.Text.Trim(), txtRemarks.Text.Trim(), ddlIsComplaince.SelectedItem.Text, "1", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
                if (dsCase != null && dsCase.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = dsCase.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (dsCase.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        FindGridData(sender, e);
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}