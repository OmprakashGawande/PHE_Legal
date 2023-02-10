using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_EditWPCases : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
        {
            if (!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString["ID"].ToString();
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                BindDetails(); // Here Bind All Page Details.
                BindDisposeType(); // Bind Case Dispose Type ddl.
                CaseDisposeStatus(); // by deafult Case Dispose on NO text.
                BindYear();
                BindOfficeType();
                HearingDatacolumn(); // Create Hearing Datatable Column.
                BindRespondertype();
                BindCasetype();
                BindCaseSubject();
                FillDesignation();
                FillOfficetype_EditRes();
                FillDesig_EditRes();
                FillOicList();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }
    protected void FillOicList()
    {
        try
        {
            ddlOicName.Items.Clear();
            ds = obj.ByDataSet("select OICName, OICMaster_ID from tblOICMaster");
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
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #region Fill Designarion
    protected void FillDesignation()
    {
        try
        {
            ddldesignation.Items.Clear();
            ddlDesignation_Res.Items.Clear();
            ds = obj.ByDataSet("select Designation_Id,Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddldesignation.DataTextField = "Designation_Name"; // for Petitioner
                ddldesignation.DataValueField = "Designation_Id";
                ddldesignation.DataSource = ds;
                ddldesignation.DataBind();

                ddlDesignation_Res.DataTextField = "Designation_Name";// for for Add_Respondent
                ddlDesignation_Res.DataValueField = "Designation_Id";
                ddlDesignation_Res.DataSource = ds;
                ddlDesignation_Res.DataBind();

                ddlDesig_EditRes.DataTextField = "Designation_Name"; // for Edit_Respondent
                ddlDesig_EditRes.DataValueField = "Designation_Id";
                ddlDesig_EditRes.DataSource = ds;
                ddlDesig_EditRes.DataBind();
            }
            ddldesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlDesig_EditRes.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void FillDesig_EditRes()
    {
        try
        {
            ddlDesig_EditRes.Items.Clear();
            ds = obj.ByDataSet("select Designation_Id,Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesig_EditRes.DataTextField = "Designation_Name"; // for Edit_Respondent
                ddlDesig_EditRes.DataValueField = "Designation_Id";
                ddlDesig_EditRes.DataSource = ds;
                ddlDesig_EditRes.DataBind();
            }
            ddlDesignation_Res.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    #endregion
    #region Fill CaseSubject
    protected void BindCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            ds = obj.ByProcedure("Sp_CaseSubject", new string[] { "flag" }, new string[] { "2" }, "dataset");
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
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Respondent
    protected void BindRespondertype()
    {
        try
        {
            ddlResponderType_Res.Items.Clear();
            ddlEditRespondertype.Items.Clear();
            ds = obj.ByProcedure("USP_Get_ResponderType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlResponderType_Res.DataTextField = "RespondertypeName";
                ddlResponderType_Res.DataValueField = "Respondertype_ID";
                ddlResponderType_Res.DataSource = ds;
                ddlResponderType_Res.DataBind();

                ddlEditRespondertype.DataTextField = "RespondertypeName";
                ddlEditRespondertype.DataValueField = "Respondertype_ID";
                ddlEditRespondertype.DataSource = ds;
                ddlEditRespondertype.DataBind();
            }
            ddlResponderType_Res.Items.Insert(0, new ListItem("Select", "0"));
            ddlEditRespondertype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void BindYear()
    {
        List<int> List = new List<int>();
        ddlCaseYear.Items.Clear();
        for (int i = 2018; i <= DateTime.Now.Year; i++)
        {
            List.Add(i);
            ddlCaseYear.DataSource = List;
            ddlCaseYear.DataBind();
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion
    #region Fill CaseDispose Status
    protected void CaseDisposeStatus() // Case Dispose By Default On NO condtiton
    {
        foreach (ListItem item in rdCaseDispose.Items)
        {
            if (item.Text.Contains("No"))
            {
                item.Selected = true;
                break;

            }
            caseDisposeYes.Visible = false;
            CaseDis_DateDiv.Visible = false;
            CaseDis_OrderDocDiv.Visible = false;
            HearingDtl_CaseDispose.Visible = false;
            CaseDis_OrderImpleDaysDiv.Visible = false;
        }
    }
    #endregion
    #region Fill Case DisposeType
    protected void BindDisposeType()
    {
        try
        {
            ddlDisposalType.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDisposalType.DataTextField = "CaseDisposeType";
                ddlDisposalType.DataValueField = "CaseDisposeType_Id";
                ddlDisposalType.DataSource = ds;
                ddlDisposalType.DataBind();
            }
            ddlDisposalType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CaseType
    protected void BindCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = obj.ByDataSet("select Casetype_ID, Casetype_Name from tbl_Legal_Casetype");
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
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Hearing Datatable
    protected void HearingDatacolumn()
    {
        DataTable dt = new DataTable();
        if (dt.Columns.Count == 0)
        {
            // dt.Columns.Add("Case_Id", typeof(int));
            dt.Columns.Add("HearingDate", typeof(string));
            dt.Columns.Add("HearingDetail", typeof(string));
            dt.Columns.Add("HearingDoc", typeof(string));
            dt.Columns.Add("Instruction", typeof(string));
        }
        ViewState["HearingDt"] = dt;
    }
    #endregion
    protected void FieldClose()
    {
        Case_EditField.Visible = false;
        FieldSet_CaseDetail.Visible = false; ;
        FieldSet_DocumentDetail.Visible = false;
        FieldSet_ResponderDetail.Visible = false;
        Field_AddResponder.Visible = false;
    }
    #region Fill OfficeType
    protected void BindOfficeType()
    {
        try
        {
            ddlOfficeType.Items.Clear();
            ddlOfficeType_Res.Items.Clear();

            ds = obj.ByDataSet("select OfficeType_Id,OfficeType_Name From tblOfficeTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType.DataTextField = "OfficeType_Name"; // for Petitioner Office
                ddlOfficeType.DataValueField = "OfficeType_Id";
                ddlOfficeType.DataSource = ds;
                ddlOfficeType.DataBind();

                ddlOfficeType_Res.DataTextField = "OfficeType_Name"; // for Add_respondent
                ddlOfficeType_Res.DataValueField = "OfficeType_Id";
                ddlOfficeType_Res.DataSource = ds;
                ddlOfficeType_Res.DataBind();
            }
            ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
            ddlOfficeType_Res.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void FillOfficetype_EditRes()
    {
        try
        {
            ddlOfficetype_EditRes.Items.Clear();
            ds = obj.ByDataSet("select OfficeType_Id,OfficeType_Name From tblOfficeTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficetype_EditRes.DataTextField = "OfficeType_Name"; // for Edit_respondent
                ddlOfficetype_EditRes.DataValueField = "OfficeType_Id";
                ddlOfficetype_EditRes.DataSource = ds;
                ddlOfficetype_EditRes.DataBind();

            }
            ddlOfficetype_EditRes.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill MainDetails
    protected void BindDetails()
    {
        try
        {
            DtlViewCaseReport.DataSource = null;
            DtlViewCaseReport.DataBind();
            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdCaseDoc_FromDB.DataSource = null;
            GrdCaseDoc_FromDB.DataBind();
            dtlCaseDispose.DataSource = null;
            dtlCaseDispose.DataBind();
            GrdHearingDtl_FromDB.DataSource = null;
            GrdHearingDtl_FromDB.DataBind();
            FieldClose();
            ds = obj.ByProcedure("USP_Legal_CaseRegis_SelectForEdit", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                // Case Dipose Dtl
                if (ds.Tables[0].Rows[0]["CaseDispose_Status"].ToString() == "Yes")
                {
                    Fieldset_CaseDispose.Visible = true;
                    dtlCaseDispose.DataSource = ds.Tables[0];
                    dtlCaseDispose.DataBind();
                    CaseDispose_Div.Visible = false;
                }
                else { Fieldset_CaseDispose.Visible = false; }
                if (ds.Tables[0].Rows[0]["CaseSubject"].ToString() != "")
                {
                    lblCaseOfSubjectDtl.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString();
                }
                FieldSet_CaseDetail.Visible = true;
                FieldSet_DocumentDetail.Visible = true;
                FieldSet_ResponderDetail.Visible = true;

                DtlViewCaseReport.DataSource = ds; // Case And Petitoner Dtl Bind In DtlView.
                DtlViewCaseReport.DataBind();

                GrdResponderDtl.DataSource = ds.Tables[1];  // Responder Dtl Bind.
                GrdResponderDtl.DataBind();

                GrdCaseDoc_FromDB.DataSource = ds.Tables[2]; // Documnets Bind.
                GrdCaseDoc_FromDB.DataBind();

                GrdHearingDtl_FromDB.DataSource = ds.Tables[3]; // Hearing Dtl Bind
                GrdHearingDtl_FromDB.DataBind();
            }
            else
            {
                DtlViewCaseReport.DataSource = null;
                DtlViewCaseReport.DataBind();
                GrdResponderDtl.DataSource = null;
                GrdResponderDtl.DataBind();
                GrdCaseDoc_FromDB.DataSource = null;
                GrdCaseDoc_FromDB.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    #endregion
    #region Fill ResponderRowCommand
    protected void GrdResponderDtl_RowCommand(object sender, GridViewCommandEventArgs e)  // Navigate on the Edit Case Detail Div.
    {
        try
        {
            if (e.CommandName == "EditResponder")
            {
                lblMsg.Text = "";
                ViewState["ResponderID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblResponderID = (Label)row.FindControl("lblResponderID");
                Label lblResponderName = (Label)row.FindControl("lblResponderName");
                Label lblResponderNo = (Label)row.FindControl("lblResponderNo");
                Label lblDepartent = (Label)row.FindControl("lblDepartent");
                Label lblAddress = (Label)row.FindControl("lblAddress");
                Label lblofficetype_ID = (Label)row.FindControl("lblofficetype_ID");
                Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
                Label lblDesignation_ID = (Label)row.FindControl("lblDesignation_ID");
                txtResponderName.Text = lblResponderName.Text;
                txtResponderNo.Text = lblResponderNo.Text;
                txtDepartment.Text = lblDepartent.Text;
                txtAddress.Text = lblAddress.Text;
                if (lblofficetype_ID.Text != "")
                {
                    ddlOfficetype_EditRes.ClearSelection();
                    ddlOfficetype_EditRes.Items.FindByValue(lblofficetype_ID.Text).Selected = true;
                }
                if (lblOffice_ID.Text != "")
                {
                    ddlOfficetype_EditRes_SelectedIndexChanged(sender, e);
                    ddlOfficename_EditRes.ClearSelection();
                    ddlOfficename_EditRes.Items.FindByValue(lblOffice_ID.Text).Selected = true;
                }
                if (lblDesignation_ID.Text != "")
                {
                    ddlDesig_EditRes.ClearSelection();
                    ddlDesig_EditRes.Items.FindByValue(lblDesignation_ID.Text).Selected = true;
                }
                ViewState["ResponderID"] = lblResponderID.Text;
                btnAddResponder.Text = "Update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }

    }
    #endregion
    #region Fill Petitioner Dtl
    protected void lnkEditCaseDtl_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
            {
                ds = obj.ByProcedure("USP_Legal_CaseRegis_SelectForEdit", new string[] { "Case_ID" }
                    , new string[] { ViewState["ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Case_EditField.Visible = true;
                    FieldSet_CaseDetail.Visible = false; ;
                    FieldSet_DocumentDetail.Visible = false;
                    FieldSet_ResponderDetail.Visible = false;
                    Field_AddResponder.Visible = false;
                    Fieldset_CaseDispose.Visible = false;
                    Fieldset_HearingDtl.Visible = false;
                    lnkEditCaseDtl.Visible = false;// btn petitioner Dtl false.
                    lnkAddResponderDtl.Visible = true;// btn respondent Dtl false.
                    lnkBack.Visible = true;// btn back true.

                    lblCaseRefNo.Text = ds.Tables[0].Rows[0]["OldCaseNo"].ToString();
                    txtPetitionerName.Text = ds.Tables[0].Rows[0]["PetitonerName"].ToString();
                    txtUpdatedCaseStatus.Text = ds.Tables[0].Rows[0]["CurrentOfficeStatus"].ToString();
                    if (ds.Tables[0].Rows[0]["CaseNo"].ToString() != null)
                    {
                        txtCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Designation_Id"].ToString() != "")
                    {
                        ddldesignation.ClearSelection();
                        ddldesignation.Items.FindByValue(ds.Tables[0].Rows[0]["Designation_Id"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["JusticeName"].ToString() != "")
                    {
                        txtJusticeName.Text = ds.Tables[0].Rows[0]["JusticeName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["NodalOfficerName"].ToString() != "")
                    {
                        txtNOdalOfficerName.Text = ds.Tables[0].Rows[0]["NodalOfficerName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["NodalOfficerMobileNo"].ToString() != "")
                    {
                        txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["NodalOfficerMobileNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["PetiAdvocateName"].ToString() != "")
                    {
                        txtPetiAdvocateName.Text = ds.Tables[0].Rows[0]["PetiAdvocateName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["PetiAdvocateMobile"].ToString() != "")
                    {
                        txtPetiAdvocateMobileNo.Text = ds.Tables[0].Rows[0]["PetiAdvocateMobile"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CaseDetail"].ToString() != "")
                    {
                        txtCaseDetail.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CaseDispose_Status"].ToString() == "Yes")
                    {
                        rdCaseDispose.ClearSelection();
                        rdCaseDispose.Items.FindByText(ds.Tables[0].Rows[0]["CaseDispose_Status"].ToString()).Selected = true;

                        rdCaseDispose_SelectedIndexChanged(sender, e);

                        ddlDisposalType.ClearSelection();
                        ddlDisposalType.Items.FindByValue(ds.Tables[0].Rows[0]["CaseDisposeType_Id"].ToString()).Selected = true;
                        ddlDisposalType_SelectedIndexChanged(sender, e);
                    }
                    if (ds.Tables[0].Rows[0]["Casetype_ID"].ToString() != "")
                    {
                        ddlCasetype.ClearSelection();
                        ddlCasetype.Items.FindByValue(ds.Tables[0].Rows[0]["Casetype_ID"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["CaseYear"].ToString() != "")
                    {
                        ddlCaseYear.ClearSelection();
                        ddlCaseYear.Items.FindByText(ds.Tables[0].Rows[0]["CaseYear"].ToString()).Selected = true;

                    }
                    if (ds.Tables[0].Rows[0]["OfficeType_Id"].ToString() != "")
                    {
                        ddlOfficeType.ClearSelection();
                        ddlOfficeType.Items.FindByValue(ds.Tables[0].Rows[0]["OfficeType_Id"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["Office_Id"].ToString() != "")
                    {
                        //ddlOfficeType_SelectedIndexChanged(sender, e);  HighPrirtyCaseSts
                        //ddlOfficeName.ClearSelection();
                        //ddlOfficeName.Items.FindByValue(ds.Tables[0].Rows[0]["Office_Id"].ToString()).Selected = true;

                    }
                    if (ds.Tables[0].Rows[0]["OfficeName"].ToString() != "")
                    {
                        txtOfficeName.Text = ds.Tables[0].Rows[0]["OfficeName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["HighPrioritiCasesStatus"].ToString() != "")
                    {
                        ddlHighPriorityCase.ClearSelection();
                        ddlHighPriorityCase.Items.FindByText(ds.Tables[0].Rows[0]["HighPrioritiCasesStatus"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["CaseSubject"].ToString() != "")
                    {
                        ddlCaseSubject.ClearSelection();
                        ddlCaseSubject.Items.FindByText(ds.Tables[0].Rows[0]["CaseSubject"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["OICMaster_ID"].ToString() != "")
                    {
                        ddlOicName.ClearSelection();
                        ddlOicName.Items.FindByValue(ds.Tables[0].Rows[0]["OICMaster_ID"].ToString()).Selected = true;
                        ddlOicName_SelectedIndexChanged(sender, e);
                    }
                    if (ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString() != "")
                    {
                        ddlCase_SubSubject.ClearSelection();
                        ddlCaseSubject_SelectedIndexChanged(sender, e);
                        ddlCase_SubSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()).Selected = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    #endregion
    #region Edit Petitioner Dtl
    protected void btnUpdate_Click(object sender, EventArgs e) // Only Case & Petitioner INformation Update.
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnUpdate.Text == "Update" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Update_LegalWPCaseReg", new string[] { "HighPrirtyCaseSts",
                        "WPCaseNo",
                        "WPCaseYear",
                        "Casetype_ID",
                        "Petitoner_Name",
                        "OfficeType_Id",
                        "OfficeName", 
                        "CurrentOfficeStatus", 
                        "CaseSubject",
                        "CaseSubSubj_Id",
                        "NodalOfficer_Name", 
                        "NodalOfficerMobileNo", 
                        "JusticeName", 
                        "PetiAdvocateName", 
                        "PetiAdvocateMobile",
                        //"OICName","OICMobileNo",
                        "OICMaster_ID",
                        "LastupdatedBy", 
                        "LastupdatedByIp", 
                        "Case_ID", 
                        "CaseDetail","UserType_Id" }
                        , new string[] { ddlHighPriorityCase.SelectedItem.Text,
                            txtCaseNo.Text.Trim(),
                            ddlCaseYear.SelectedItem.Text.Trim(),
                            ddlCasetype.SelectedValue,
                            txtPetitionerName.Text.Trim(), 
                            ddlOfficeType.SelectedValue,
                            txtOfficeName.Text.Trim(),
                            txtUpdatedCaseStatus.Text.Trim(),
                            ddlCaseSubject.SelectedValue,
                            ddlCase_SubSubject.SelectedValue,
                            txtNOdalOfficerName.Text.Trim(),
                            txtNodalOfficerMobileNo.Text.Trim(), 
                            txtJusticeName.Text.Trim(), 
                            txtPetiAdvocateName.Text.Trim(),
                            txtPetiAdvocateMobileNo.Text.Trim(),ddlOicName.SelectedValue,
                            //txtOicName.Text.Trim(),txtOicMobileNo.Text.Trim(),
                            ViewState["Emp_Id"].ToString(),
                            obj.GetLocalIPAddress(),
                            ViewState["ID"].ToString(), 
                            txtCaseDetail.Text.Trim(),
                        ddldesignation.SelectedValue}, "dataset");

                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtPetitionerName.Text = "";
                        ddlOfficeType.ClearSelection();
                        ddlCaseSubject.ClearSelection();
                        txtUpdatedCaseStatus.Text = "";
                        ddlCaseYear.ClearSelection();
                        txtNOdalOfficerName.Text = "";
                        txtNodalOfficerMobileNo.Text = "";
                        txtJusticeName.Text = "";
                        txtPetiAdvocateName.Text = "";
                        txtCaseDetail.Text = "";
                        txtPetiAdvocateMobileNo.Text = "";
                        ddlDisposalType.ClearSelection();
                        CaseDisposeStatus();

                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
                BindDetails();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void lnkAddResponderDtl_Click(object sender, EventArgs e) // Navigate on the Add Responder Div.
    {
        try
        {
            lblMsg.Text = "";
            Field_AddResponder.Visible = true;
            Case_EditField.Visible = false;
            FieldSet_CaseDetail.Visible = false; ;
            FieldSet_DocumentDetail.Visible = false;
            FieldSet_ResponderDetail.Visible = false;
            Fieldset_CaseDispose.Visible = false;
            Fieldset_HearingDtl.Visible = false;
            lnkAddResponderDtl.Visible = false; //btn respondent false.
            lnkEditCaseDtl.Visible = true; // btn petitioner Dtl true.
            lnkBack.Visible = true;// btn back true.
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #region Insert Edit Respondent Dtl
    protected void btnAddResponder_Click(object sender, EventArgs e) // Add New Responder.
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnAddResponder.Text == "Add" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    //ds = obj.ByProcedure("USP_Legal_Insert_ResponderName", new string[] { "Case_ID", "Respondertype_ID", "Respondent_Name", "RespondentNo", "Address", "Department", "CreatedBy", "CreatedByIP", "OICNAME", "OICMobileNO", "OICEailID" }
                    //    , new string[] { ViewState["ID"].ToString(), ddlResponderType_Res.SelectedValue, txtResponderName_Res.Text.Trim(), txtResponderNo_Res.Text.Trim(), txtResponderAdd_Res.Text.Trim(), txtResponderDept_Res.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), txtOicNameRespondent.Text.Trim(), txtOicMobileNoRespondent.Text.Trim(), txtOicEmailIDRespondent.Text.Trim() }, "dataset");
                    ds = obj.ByProcedure("USP_Legal_Insert_ResponderName", new string[] { "Case_ID", "Respondertype_ID", "Officetype_Id", "Office_Id", "Designation_Id", "Respondent_Name", "RespondentNo", "Address", "Department", "CreatedBy", "CreatedByIP" }
                        , new string[] { ViewState["ID"].ToString(), ddlResponderType_Res.SelectedValue, ddlOfficeType_Res.SelectedValue, ddlOfficeName_Res.SelectedValue, ddlDesignation_Res.SelectedValue, txtResponderName_Res.Text.Trim(), txtResponderNo_Res.Text.Trim(), txtResponderAdd_Res.Text.Trim(), txtResponderDept_Res.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnAddResponder.Text == "Update" && ViewState["ResponderID"].ToString() != null && ViewState["ResponderID"].ToString() != "")
                {
                    //ds = obj.ByProcedure("USP_Legal_Update_ResponderDtl", new string[] { "Respondent_ID", "Respondertype_ID", "Case_ID", "Respondent_Name", "RespondentNo", "Address", "Department", "LastupdatedBy", "LastupdatedByIp", "OICNAME", "OICMobileNO", "OICEailID" }
                    //    , new string[] { ViewState["ResponderID"].ToString(), ddlEditRespondertype.SelectedValue, ViewState["ID"].ToString(), txtResponderName.Text.Trim(), txtResponderNo.Text.Trim(), txtAddress.Text.Trim(), txtDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), txtEditRespondentOICName.Text.Trim(), txtEditRepondentOICMObile.Text.Trim(), txtEditRepondentOICEmail.Text.Trim() }, "dataset");
                    ds = obj.ByProcedure("USP_Legal_Update_ResponderDtl", new string[] { "Respondent_ID", "Respondertype_ID", "Officetype_Id", "Office_Id", "Designation_Id", "Case_ID", "Respondent_Name", "RespondentNo", "Address", "Department", "LastupdatedBy", "LastupdatedByIp" }
                      , new string[] { ViewState["ResponderID"].ToString(), ddlEditRespondertype.SelectedValue, ddlOfficetype_EditRes.SelectedValue, ddlOfficename_EditRes.SelectedValue, ddlDesig_EditRes.SelectedValue, ViewState["ID"].ToString(), txtResponderName.Text.Trim(), txtResponderNo.Text.Trim(), txtAddress.Text.Trim(), txtDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtResponderName.Text = "";
                        txtResponderNo.Text = "";
                        txtAddress.Text = "";
                        txtDepartment.Text = "";
                        txtResponderName_Res.Text = "";
                        ddlDesignation_Res.ClearSelection();
                        txtResponderNo_Res.Text = "";
                        txtResponderDept_Res.Text = "";
                        ddlOfficeType_Res.ClearSelection();
                        txtResponderAdd_Res.Text = "";
                        ddlOfficeName_Res.ClearSelection();
                        ddlResponderType_Res.ClearSelection();
                        ddlEditRespondertype.ClearSelection();
                        ddlEditRespondertype.ClearSelection();
                        ddlOfficetype_EditRes.ClearSelection();
                        ddlOfficename_EditRes.ClearSelection();
                        ddlDesig_EditRes.ClearSelection();
                        txtResponderName.Text = "";
                        txtResponderNo.Text = "";
                        txtAddress.Text = "";
                        txtDepartment.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
                BindDetails();
                btnAddResponder.Text = "Add";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region insert edit Doc
    protected void btnSaveDoc_Click(object sender, EventArgs e) // Add & Edit Document.
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                if (ViewState["CaseDoc"] == "")
                {
                    ViewState["CaseDoc"] = "";
                }
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileUpload1.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = FileUpload1.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "WPcaseDocument-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/Documents/"), strFileName);
                        FileUpload1.SaveAs(path);

                        ViewState["CaseDoc"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }

                }
                else if (fileUpload2_EditDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(fileUpload2_EditDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (fileUpload2_EditDoc.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = fileUpload2_EditDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "WPcaseDocument-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/Documents/"), strFileName);
                        fileUpload2_EditDoc.SaveAs(path);

                        ViewState["CaseDoc"] = strFileName;
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
                    if (btnSaveDoc.Text == "Upload Doc")
                    {

                        ds = obj.ByProcedure("USP_Legal_Insert_AddDocument", new string[] { "Case_ID", "Doc_Name", "Doc_Path", "CreatedBy", "CreatedByIP" }
                            , new string[] { ViewState["ID"].ToString(), txtDocumentName.Text.Trim(), ViewState["CaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSaveDoc.Text == "Edit Doc" && ViewState["DocID"].ToString().ToString() != "" && ViewState["DocID"].ToString() != null)
                    {
                        //if (ViewState["Edit_CaseDoc"] != ViewState["CaseDoc"])
                        //{
                        //    string path = Path.Combine(Server.MapPath("../Legal/Documents/"), ViewState["Edit_CaseDoc"].ToString());
                        //    if (File.Exists(path))
                        //    {
                        //        File.Delete(path);
                        //    }
                        //}
                        ds = obj.ByProcedure("USP_Legal_Update_CaseDocument", new string[] { "CaseDoc_ID", "Case_ID", "Doc_Name", "Doc_Path", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { ViewState["DocID"].ToString(), ViewState["ID"].ToString(), txtEditDocumentsName.Text.Trim(), ViewState["CaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            ViewState["CaseDoc"] = "";
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        }
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                    }
                    BindDetails();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogCls.SendErrorToText(ex);
                //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            }
        }
    }
    #endregion
    protected void GrdCaseDoc_FromDB_RowCommand(object sender, GridViewCommandEventArgs e)// on row command Event for Edit Document
    {
        try
        {
            ViewState["DocID"] = ""; ViewState["Edit_CaseDoc"] = "";
            if (e.CommandName == "EditDocument")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDocumentID = (Label)row.FindControl("lblDocumentID");
                Label lblDocName = (Label)row.FindControl("lblDocName");
                Label lblCaseDoc = (Label)row.FindControl("lblCaseDoc");
                txtEditDocumentsName.Text = lblDocName.Text;
                ViewState["DocID"] = lblDocumentID.Text;
                ViewState["CaseDoc"] = lblCaseDoc.Text;
                ViewState["Edit_CaseDoc"] = lblCaseDoc.Text;
                btnSaveDoc.Text = "Edit Doc";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#MymodalEditDocuments').modal('show')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlDisposalType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            CaseDis_DateDiv.Visible = false;
            CaseDis_OrderDocDiv.Visible = false;
            if (ddlDisposalType.SelectedIndex > 0)
            {
                CaseDis_DateDiv.Visible = true;
                CaseDis_OrderDocDiv.Visible = true;
                HearingDtl_CaseDispose.Visible = true;
                CaseDis_OrderImpleDaysDiv.Visible = true; //Order Implement Days
            }
            else
            {
                HearingDtl_CaseDispose.Visible = false;
                CaseDis_DateDiv.Visible = false;
                CaseDis_OrderDocDiv.Visible = false;
                CaseDis_OrderImpleDaysDiv.Visible = false;
                CaseDis_OrderDocDiv.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void rdCaseDispose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            caseDisposeYes.Visible = false;
            if (rdCaseDispose.SelectedValue == "1")
            {
                caseDisposeYes.Visible = true;
                ddlDisposalType.ClearSelection();
            }
            else
            {
                caseDisposeYes.Visible = false;
                CaseDis_DateDiv.Visible = false;
                CaseDis_OrderImpleDaysDiv.Visible = false;
                CaseDis_OrderDocDiv.Visible = false;
                HearingDtl_CaseDispose.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName_Res.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlOfficeType_Res.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName_Res.DataTextField = "OfficeName";
                ddlOfficeName_Res.DataValueField = "Office_Id";
                ddlOfficeName_Res.DataSource = ds;
                ddlOfficeName_Res.DataBind();

            }
            ddlOfficeName_Res.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #region Add Hearing_datatable
    protected void btnAddHearingDtl_Click(object sender, EventArgs e) //  Add Hearing Dtl.
    {
        try
        {
            //if (Page.IsValid)
            //{
            lblMsg.Text = "";
            ViewState["HearingDOC"] = "";
            int DocFailedCntExt = 0;
            int DocFailedCntSize = 0;
            string strFileName = "";
            string strExtension = "";
            string strTimeStamp = "";
            if (fileUpload_HearingDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {

                string fileExt = System.IO.Path.GetExtension(fileUpload_HearingDoc.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (fileUpload_HearingDoc.PostedFile.ContentLength > 204800) // 500 KB = 1024 * 500
                {
                    DocFailedCntSize += 1;
                }
                else
                {

                    strFileName = fileUpload_HearingDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-WPHearing-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), strFileName);
                    fileUpload_HearingDoc.SaveAs(path);

                    ViewState["HearingDOC"] = strFileName;
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
                DataTable dt1 = (DataTable)ViewState["HearingDt"];
                if (dt1.Columns.Count > 0)
                {
                    dt1.Rows.Add(Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd"), ddlHearingDtl.SelectedItem.Text.Trim(), ViewState["HearingDOC"].ToString(), txtInst_AddHearing.Text.Trim());
                }
                ds.Tables.Add(dt1);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    FieldSet_SaveHeringDtl.Visible = true;
                    btnSaveHearingDtl.Visible = true;
                    txtHearingDate.Text = "";
                    ddlHearingDtl.ClearSelection();
                    ViewState["HearingDOC"] = "";
                    GrdHearingDetail.DataSource = dt1;
                    GrdHearingDetail.DataBind();
                    ViewState["HearingDt"] = dt1;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
            }
            //}
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Save Edit Hearing
    protected void btnSaveHearingDtl_Click(object sender, EventArgs e) //Save Hearing Dtl.
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["HearingDOC"] == "")
            {
                ViewState["HearingDOC"] = "";
            }
            int DocFailedCntExt = 0;
            int DocFailedCntSize = 0;
            string strFileName = "";
            string strExtension = "";
            string strTimeStamp = "";
            if (FileUpEditHearigDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {

                string fileExt = System.IO.Path.GetExtension(FileUpEditHearigDoc.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpEditHearigDoc.PostedFile.ContentLength > 204800) // 500 KB = 1024 * 500
                {
                    DocFailedCntSize += 1;
                }
                else
                {

                    strFileName = FileUpEditHearigDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-WPHearing-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), strFileName);
                    FileUpEditHearigDoc.SaveAs(path);

                    ViewState["HearingDOC"] = strFileName;
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
                if (btnSaveHearingDtl.Text == "Save Hearing")
                {
                    DataTable dtHearing = ViewState["HearingDt"] as DataTable;
                    ds = obj.ByProcedure("USP_Legal_InsertHearingDtl", new string[] { "Case_ID", "CreatedBy", "CreatedByIP" }
                        , new string[] { ViewState["ID"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() },
                        new string[] { "type_HearingDetail" }, new DataTable[] { dtHearing }, "dataset");

                    dtHearing.Clear();
                }
                else if (btnSaveHearingDtl.Text == "Update" && ViewState["HearingID"] != "" && ViewState["HearingID"] != null)
                {
                    //if (ViewState["EditHearingDoc"] != ViewState["HearingDOC"])
                    //{
                    //    string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), ViewState["EditHearingDoc"].ToString());
                    //    if (File.Exists(path))
                    //    {
                    //        File.Delete(path);
                    //    }
                    //}
                    ds = obj.ByProcedure("USP_Legal_Update_HearingDetail", new string[] { "HearingDtl", "NextHearingDate", "HearingDoc", "LastupdatedBy", "LastupdatedByIp", "NextHearing_ID", "Case_ID", "InstructionByCourt" },
                        new string[] { ddlEditHearngDtl.SelectedItem.Text.Trim(), Convert.ToDateTime(txtEditHearingDate.Text, cult).ToString("yyy/MM/dd"), ViewState["HearingDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["HearingID"].ToString(), ViewState["ID"].ToString(), txtInst_EditHearing.Text.Trim() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrorMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrorMsg);

                        AddNewHearing.Visible = false;
                        lnkbtnAddNewHering.Visible = true;
                        btnHearingBack.Visible = false;
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrorMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
            }
            BindDetails();
            FiledSet_HearingDBDtl.Visible = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void lnkbtnAddNewHering_Click(object sender, EventArgs e)// Should Be Multiple Hearing btn
    {
        try
        {
            lblMsg.Text = "";
            AddNewHearing.Visible = true;
            FiledSet_HearingDBDtl.Visible = false;
            btnHearingBack.Visible = true;//hearing Backbtn
            lnkbtnAddNewHering.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #region Case Dispose Save
    protected void btnCaseDispose_Click(object sender, EventArgs e) // Case Dispose Event
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnCaseDispose.Text == "Case Dispose")
                {
                    ViewState["FileOrderDOC"] = "";
                    int DocFailedCntExt = 0;
                    int DocFailedCntSize = 0;
                    string strFileName = "";
                    string strExtension = "";
                    string strTimeStamp = "";
                    if (FielUpcaseDisposeOrderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                    {

                        string fileExt = System.IO.Path.GetExtension(FielUpcaseDisposeOrderDoc.FileName).Substring(1);
                        string[] supportedTypes = { "PDF", "pdf" };
                        if (!supportedTypes.Contains(fileExt))
                        {
                            DocFailedCntExt += 1;
                        }
                        else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                        {
                            DocFailedCntSize += 1;
                        }
                        else
                        {

                            strFileName = FielUpcaseDisposeOrderDoc.FileName.ToString();
                            strExtension = Path.GetExtension(strFileName);
                            strTimeStamp = DateTime.Now.ToString();
                            strTimeStamp = strTimeStamp.Replace("/", "-");
                            strTimeStamp = strTimeStamp.Replace(" ", "-");
                            strTimeStamp = strTimeStamp.Replace(":", "-");
                            string strName = Path.GetFileNameWithoutExtension(strFileName);
                            strFileName = strName + "-WPCaseDispose-" + strTimeStamp + strExtension;
                            string path = Path.Combine(Server.MapPath("../Legal/UploadOrderDoc/"), strFileName);
                            FielUpcaseDisposeOrderDoc.SaveAs(path);

                            ViewState["FileOrderDOC"] = strFileName;
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
                        ds = obj.ByProcedure("USP_Legal_CaseDispose", new string[] { "Case_ID", "CaseDisposeType_Id", "CaseDispose_Status", "CaseDispose_Date", "CaseDispose_OrderNo", "CaseDispose_OrderDoc", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { ViewState["ID"].ToString(), ddlDisposalType.SelectedValue, rdCaseDispose.SelectedItem.Text.Trim(), Convert.ToDateTime(txtCaseDisposeDate.Text, cult).ToString("yyyy/MM/dd"), txtOrderimpletimeline.Text.Trim(), ViewState["FileOrderDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                                ddlDisposalType.ClearSelection();
                                ViewState["FileOrderDOC"] = "";
                                CaseDisposeStatus();
                                txtCaseDisposeDate.Text = "";
                            }
                            else
                            {
                                lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                            }
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                        }
                        BindDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void GrdHearingDtl_FromDB_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["HearingID"] = "";
            ViewState["EditHearingDoc"] = "";
            if (e.CommandName == "EditHearing")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblCaseID = (Label)row.FindControl("lblCaseID");
                Label lblHearingDate = (Label)row.FindControl("lblHearingDate");
                Label lblHearingDetail = (Label)row.FindControl("lblHearingDetail");
                Label lblInstruction = (Label)row.FindControl("lblInstruction");
                Label lblHearingDoc = (Label)row.FindControl("lblHearingDoc");
                if (lblHearingDate.Text != "")
                {
                    txtEditHearingDate.Text = lblHearingDate.Text;
                }
                if (lblHearingDetail.Text != "")
                {
                    ddlEditHearngDtl.ClearSelection();
                    ddlEditHearngDtl.Items.FindByText(lblHearingDetail.Text).Selected = true;
                }
                if (lblInstruction.Text != "")
                {
                    ddlEditHearngDtl_SelectedIndexChanged(sender, e);
                    txtInst_EditHearing.Text = lblInstruction.Text;
                }
                ViewState["EditHearingDoc"] = lblHearingDoc.Text;
                ViewState["HearingDOC"] = lblHearingDoc.Text;
                ViewState["HearingID"] = e.CommandArgument;
                btnSaveHearingDtl.Text = "Update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#ModalEditHearingDtl').modal('show')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnHearingBack_Click(object sender, EventArgs e)// Hearing back Btn
    {
        try
        {
            lblMsg.Text = "";
            Field_AddResponder.Visible = false;
            Case_EditField.Visible = false;
            FieldSet_CaseDetail.Visible = true; ;
            FieldSet_DocumentDetail.Visible = true;
            FieldSet_ResponderDetail.Visible = true;
            Fieldset_HearingDtl.Visible = true;
            BindDetails();
            lnkbtnAddNewHering.Visible = true; // Heairng btn
            btnHearingBack.Visible = false; // Heairng back btn
            AddNewHearing.Visible = false;
            FiledSet_HearingDBDtl.Visible = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlOfficetype_EditRes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlOfficename_EditRes.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlOfficetype_EditRes.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficename_EditRes.DataTextField = "OfficeName";
                ddlOfficename_EditRes.DataValueField = "Office_Id";
                ddlOfficename_EditRes.DataSource = ds;
                ddlOfficename_EditRes.DataBind();
            }
            ddlOfficeName_Res.Items.Insert(0, new ListItem("Select", "0"));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void lnkBack_Click(object sender, EventArgs e) // For Back Button
    {
        lblMsg.Text = "";
        Field_AddResponder.Visible = false;
        Case_EditField.Visible = false;
        FieldSet_CaseDetail.Visible = true; ;
        FieldSet_DocumentDetail.Visible = true;
        FieldSet_ResponderDetail.Visible = true;
        Fieldset_HearingDtl.Visible = true;
        BindDetails();
        lnkEditCaseDtl.Visible = true;
        lnkAddResponderDtl.Visible = true;
        lnkBack.Visible = false;
    }
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOicName.SelectedValue != "0")
            {
                DataSet DSOIC = obj.ByDataSet("select OICMobileNo, OICEmailID from tblOICMaster where OICMaster_ID=" + ddlOicName.SelectedValue);
                if (DSOIC != null && DSOIC.Tables[0].Rows.Count > 0)
                {
                    txtOicMobileNo.Text = DSOIC.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    txtOicEmailID.Text = DSOIC.Tables[0].Rows[0]["OICEmailID"].ToString();
                    Div_OicMobileNo.Visible = true;
                    Div_OicEmailID.Visible = true;
                }
            }
            else
            {
                Div_OicMobileNo.Visible = false;
                Div_OicEmailID.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }

    }
    protected void ddlEditHearngDtl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlEditHearngDtl.SelectedValue == "3")
            {
                Editinst_Div.Visible = true;
            }
            else { Editinst_Div.Visible = false; }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#ModalEditHearingDtl').modal('show')", true);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlHearingDtl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlHearingDtl.SelectedValue == "3")
            {
                Inst_AddHearing.Visible = true;
            }
            else { Inst_AddHearing.Visible = false; }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlCase_SubSubject.Items.Clear();
            DataSet DsSubs = obj.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            if (DsSubs != null && DsSubs.Tables[0].Rows.Count > 0)
            {
                ddlCase_SubSubject.DataTextField = "CaseSubSubject";
                ddlCase_SubSubject.DataValueField = "CaseSubSubj_Id";
                ddlCase_SubSubject.DataSource = DsSubs;
                ddlCase_SubSubject.DataBind();
            }
            ddlCase_SubSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}