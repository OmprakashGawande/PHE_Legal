using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;

public partial class Legal_AddNewCase : System.Web.UI.Page
{
    DataSet ds;
    DataSet dsRecord;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Office_ID"] = Session["Office_ID"];
                    FillDistrict();
                    BindCourtName();
                    BindRespondertype();
                    BindOfficeType();
                    FillCasetype();
                    BindCaseSubject();
                    DtColumn();
                    FillYear();
                    FillColumn();
                    FillDesignation();
                    BiunOicName();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    #region Fill Designation
    protected void FillDesignation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            ds = objdb.ByDataSet("select Designation_Id, Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataValueField = "Designation_Id";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();
            }
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 2018; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    #endregion
    #region Fill Respondent
    protected void BindRespondertype()
    {
        try
        {
            ddlRespondertype.Items.Clear();
            ds = objdb.ByProcedure("USP_Get_ResponderType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRespondertype.DataTextField = "RespondertypeName";
                ddlRespondertype.DataValueField = "Respondertype_ID";
                ddlRespondertype.DataSource = ds;
                ddlRespondertype.DataBind();
            }
            ddlRespondertype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    # region Doc Datatable
    protected void DtColumn()
    {
        DataTable dtcol = new DataTable();
        if (dtcol.Columns.Count == 0)
        {
            dtcol.Columns.Add("DocName", typeof(string));
            dtcol.Columns.Add("Document", typeof(string));
        }
        ViewState["dtcol"] = dtcol;
    }
    #endregion
    #region Fill OICName
    protected void BiunOicName()
    {
        try
        {
            ddlOicName.Items.Clear();
            ds = objdb.ByProcedure("Usp_Oic_Name", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOicName.DataTextField = "OICName";
                ddlOicName.DataValueField = "OICMaster_ID";
                ddlOicName.DataSource = ds;
                ddlOicName.DataBind();
            }
            ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CourtName
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear();
            ds = objdb.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = ds;
                ddlCourtType.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill OfficeType
    protected void BindOfficeType()
    {
        ddlOfficetypeName.Items.Clear();
        ds = objdb.ByProcedure("USP_Select_Officetype", new string[] { }, new string[] { }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlOfficetypeName.DataTextField = "OfficeType_Name";
            ddlOfficetypeName.DataValueField = "OfficeType_Id";
            ddlOfficetypeName.DataSource = ds;
            ddlOfficetypeName.DataBind();
        }
        ddlOfficetypeName.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion
    #region Fill District as Loaction
    protected void FillDistrict()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Select_District", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();

                //ddlDistrictCourt.DataSource = ds;
                //ddlDistrictCourt.DataTextField = "District_Name";
                //ddlDistrictCourt.DataValueField = "District_ID";
                //ddlDistrictCourt.DataBind();
            }

            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            // ddlDistrictCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CaseStatus
    protected void BindCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            ds = objdb.ByDataSet("SELECT CaseSubject, CaseSubjectID FROM tbl_LegalMstCaseSubject");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataSource = ds;
                ddlCaseSubject.DataBind();
            }
            ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region SubmitButton
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";
                if (txtCaseNo.Text == "")
                {
                    msg += "Enter Case Number.\\n";
                }
                if (ddlCourtType.SelectedIndex < 0)
                {
                    msg += "Select Court Type.\\n";
                }
                if (ddlCaseSubject.SelectedIndex < 0)
                {
                    msg += "Select Case Subject.\\n";
                }
                if (ddlHighprioritycase.SelectedIndex < 0)
                {
                    msg += "Select High Priority Case.\\n";
                }
                if (txtCaseDetail.Text == "")
                {
                    msg += "Enter Subject Of Case.\\n";
                }
                if (msg == "")
                {
                    string Hearing_Date = "";
                    string DateofReceipt = "";
                    string LastHearingDate = "";
                    if (txtDateOfCaseReg.Text != "")
                    {
                        DateofReceipt = Convert.ToDateTime(txtDateOfCaseReg.Text, cult).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        DateofReceipt = "";
                    }
                    if (txtDateOfLastHearing.Text != "")
                    {
                        LastHearingDate = Convert.ToDateTime(txtDateOfLastHearing.Text, cult).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        LastHearingDate = "";
                    }
                    lblMsg.Text = "";
                    if (btnSubmit.Text == "Save")
                    {
                        DataTable DtNew = ViewState["DocData"] as DataTable;
                        DataTable dtresponder = ViewState["dt"] as DataTable;

                        ds = objdb.ByProcedure("USP_Insert_AddNewCaseReg", new string[] {"OldCaseNo", "CaseNo", "Casetype_ID", "CourtType_Id", "CourtDistrictLocation_ID", "CaseSubject_ID","CaseSubSubj_Id", "CaseRegDate",
                            "LastHearingDate", "HighPrioritiCaseSts", "CaseDetail","PetitonerName", 
                            "PetitionerMobileNo", "PetiAdvocateName", "PetiAdvocateMobile", "OICName", "DeptAdvocateName",
                            "DeptAdvocateMobileNo",  "CaseYear", "CreatedBy", "CreatedByIP"},
                            new string[] { txtCaseOldRefNo.Text.Trim(), txtCaseNo.Text.Trim(), ddlCasetype.SelectedValue, ddlCourtType.SelectedValue,ddlDistrict.SelectedValue, ddlCaseSubject.SelectedValue,ddlSubSubject.SelectedValue, DateofReceipt,
                                LastHearingDate,ddlHighprioritycase.SelectedItem.Text,txtCaseDetail.Text.Trim(), txtPetitionerAppName.Text.Trim(),
                            txtPetitionerAppMobileNo.Text.Trim(),txtPetitionerAdvName.Text.Trim(),txtPetitionerAdvMobileNo.Text.Trim(),ddlOicName.SelectedValue, txtDeptAdvocateName.Text.Trim(),txtDeptAdvocateMobileNo.Text.Trim(),
                           ddlCaseYear.SelectedItem.Text.Trim(),ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress()}, new string[] { "type_AddNewCaseReponderDtl", "type_LegalCaseDocDetail" }, new DataTable[] { dtresponder, DtNew }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks!", ErrMsg);
                            ClearText();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ErrMsg);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                    }
                }
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                //}

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region ClearButton
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();

    }
    #endregion
    #region ClearAllControl
    protected void ClearText()
    {
        txtCaseNo.Text = "";
        txtCaseOldRefNo.Text = "";
        ddlCourtType.ClearSelection();
        ddlCaseSubject.ClearSelection();
        txtDateOfCaseReg.Text = "";
        txtDateOfLastHearing.Text = "";
        txtCaseDetail.Text = "";
        txtDepartment.Text = "";
        txtOICMobileNo.Text = "";
        txtDeptAdvocateName.Text = "";
        txtDeptAdvocateMobileNo.Text = "";
        txtPetitionerAppName.Text = "";
        txtPetitionerAppMobileNo.Text = "";
        txtPetitionerAdvName.Text = "";
        txtPetitionerAdvMobileNo.Text = "";
        txtHearingDate.Text = "";
        GrdViewDoc.DataSource = null;
        GrdViewDoc.DataBind();
        ddlCasetype.ClearSelection();
        ddlCaseYear.ClearSelection();
        ddlDistrict.ClearSelection();
        ddlHighprioritycase.ClearSelection();
        ddlOicName.ClearSelection();
        GrdRespondent.DataSource = null;
        GrdRespondent.DataBind();
    }
    #endregion
    #region AddResponder Button
    protected void btnAddresponder_Click(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddRespondent()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region CaseType
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = objdb.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region AddDoc
    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        try
        {


            ViewState["AddNewCaseDoc"] = "";
            int DocFailedCntExt = 0;
            int DocFailedCntSize = 0;
            string strFileName = "";
            string strExtension = "";
            string strTimeStamp = "";
            if (FileUpload10.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {

                string fileExt = System.IO.Path.GetExtension(FileUpload10.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpload10.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                {
                    DocFailedCntSize += 1;
                }
                else
                {
                    strFileName = FileUpload10.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), strFileName);
                    FileUpload10.SaveAs(path);

                    ViewState["AddNewCaseDoc"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            string errormsg = "";
            if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
            if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

            if (errormsg == "")
            {
                DataTable dt = ViewState["dtcol"] as DataTable;
                if (dt.Columns.Count > 0)
                {
                    dt.Rows.Add(txtDocName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString());
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["DocData"] = dt;
                    GrdViewDoc.DataSource = dt;
                    GrdViewDoc.DataBind();
                    txtDocName.Text = "";
                    ViewState["AddNewCaseDoc"] = "";

                }
                else
                {
                    GrdViewDoc.DataSource = null;
                    GrdViewDoc.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region  Respondent Column
    protected void FillColumn()
    {
        DataTable dtCol = new DataTable();
        if (dtCol.Columns.Count == 0)
        {
            dtCol.Columns.Add("RespondenttypeID", typeof(int));
            dtCol.Columns.Add("OfficeTypeID", typeof(int));
            dtCol.Columns.Add("OfficeNameId", typeof(int));
            dtCol.Columns.Add("DesignationId", typeof(int));
            dtCol.Columns.Add("DesignationName", typeof(string));
            dtCol.Columns.Add("RespondentName", typeof(string));
            dtCol.Columns.Add("RespondentMobileNo", typeof(string));
            dtCol.Columns.Add("Department", typeof(string));
            dtCol.Columns.Add("Address", typeof(string));
            dtCol.Columns.Add("RespondenttypeName", typeof(string));
            dtCol.Columns.Add("OfficeTypeName", typeof(string));
            dtCol.Columns.Add("OfficeName", typeof(string));
        }
        ViewState["Responder"] = dtCol;
    }
    #endregion
    //protected void ddlCourtType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        try
    //        {
    //            if (ddlCourtType.SelectedValue == "5")
    //            {
    //                DistrictCourtSelect.Visible = true;
    //            }
    //            else
    //            {
    //                DistrictCourtSelect.Visible = false;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    #region Bind OfficeName
    protected void ddlOfficetypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName.Items.Clear();
            ds = objdb.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlOfficetypeName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataTextField = "OfficeName";
                ddlOfficeName.DataValueField = "Office_Id";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
            }
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddRespondent()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Save Add ResponderDtl
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnYes.Text == "Add")
                {
                    DataTable dt = ViewState["Responder"] as DataTable;
                    if (dt.Columns.Count > 0)
                    {
                        dt.Rows.Add(ddlRespondertype.SelectedValue, ddlOfficetypeName.SelectedValue, ddlOfficeName.SelectedValue, ddlDesignation.SelectedValue, ddlDesignation.SelectedItem.Text, txtResponderName.Text.Trim(),
                            txtMobileNo.Text.Trim(), txtDepartment.Text.Trim(), txtAddress.Text.Trim(), ddlRespondertype.SelectedItem.Text.Trim(), ddlOfficetypeName.SelectedItem.Text.Trim(), ddlOfficeName.SelectedItem.Text.Trim());
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdRespondent.DataSource = dt;
                        GrdRespondent.DataBind();
                        ViewState["dt"] = dt;

                        ddlOfficeName.ClearSelection();
                        ddlDesignation.ClearSelection();
                        ddlRespondertype.ClearSelection();
                        ddlOfficetypeName.ClearSelection();
                        txtResponderName.Text = "";
                        txtMobileNo.Text = "";
                        txtDepartment.Text = "";
                        txtAddress.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #endregion
    #region Bind MobileNo&Email
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("Usp_Oic_MobileEmailGet", new string[] { "OICMaster_ID" }, new string[] { ddlOicName.SelectedValue }, "dataset");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtOICMobileNo.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    txtEmailID.Text = ds.Tables[0].Rows[0]["OICEmailID"].ToString();
                }
                else
                {
                    txtOICMobileNo.Text = "";
                    txtEmailID.Text = "";

                }
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlSubSubject.Items.Clear();
            DataSet DsSubs = objdb.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            if (DsSubs != null && DsSubs.Tables[0].Rows.Count > 0)
            {
                ddlSubSubject.DataTextField = "CaseSubSubject";
                ddlSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlSubSubject.DataSource = DsSubs;
                ddlSubSubject.DataBind();
            }
            ddlSubSubject.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}

