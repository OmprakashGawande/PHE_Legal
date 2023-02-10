using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_EditWACaseDetails : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds, ds1, ds2 = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
        {
            if (Session["Emp_Id"] != null && Session["Emp_Id"] != "")
            {
                if (!IsPostBack)
                {
                    ViewState["ID"] = Request.QueryString["ID"].ToString();
                    ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                    ViewState["Office_Id"] = Session["Office_Id"].ToString();
                    Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    BindDetails();
                    FillDesignation();
                    BindDisposeType();
                    CaseDisposeStatus();
                    BindOfficeType();
                    BindYear();
                    HearingDatacolumn(); // Create Hearing Datatable Column.
                    BindRespondertype();
                    BindCasetype();
                    BindCaseSubject();
                    FillCourtName();
                    FillOicList();
                    FillAuthorityList();
                    FillLoaction();
                }
            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }

    }
    #region Fill CourtName
    protected void FillCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear();
            ds = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }
           , new string[] { }, "dataset");
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
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Designarion
    protected void FillDesignation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            ddlDesignation_AddRes.Items.Clear();
            ddlDesig_EditRes.Items.Clear();
            ds = obj.ByDataSet("select Designation_Id,Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                // Here For Petitioner Info.
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataValueField = "Designation_Id";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();

                // Here For Add Responder
                ddlDesignation_AddRes.DataTextField = "Designation_Name";
                ddlDesignation_AddRes.DataValueField = "Designation_Id";
                ddlDesignation_AddRes.DataSource = ds;
                ddlDesignation_AddRes.DataBind();

                // Here For Edi Responder 
                ddlDesig_EditRes.DataTextField = "Designation_Name";
                ddlDesig_EditRes.DataValueField = "Designation_Id";
                ddlDesig_EditRes.DataSource = ds;
                ddlDesig_EditRes.DataBind();
            }
            ddlDesig_EditRes.Items.Insert(0, new ListItem("Select", "0"));
            ddlDesignation_AddRes.Items.Insert(0, new ListItem("Select", "0"));
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Loaction
    protected void FillLoaction()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_District", new string[] { }, new string[] { }, "dataset");
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
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
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
    protected void FillAuthorityList()
    {
        try
        {
            ddlAutorityList.Items.Clear();
            ds = obj.ByDataSet("select IAuthority_ID, IAuthority_Name from tblImplementation_AuthMst");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlAutorityList.DataTextField = "IAuthority_Name";
                ddlAutorityList.DataValueField = "IAuthority_ID";
                ddlAutorityList.DataSource = ds;
                ddlAutorityList.DataBind();
            }
            ddlAutorityList.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void HearingDatacolumn()
    {
        DataTable dt = new DataTable();
        if (dt.Columns.Count == 0)
        {
            dt.Columns.Add("HearingDate", typeof(string));
            dt.Columns.Add("HearingDetail", typeof(string));
            dt.Columns.Add("HearingDoc", typeof(string));
            dt.Columns.Add("Instruction", typeof(string));
        }
        ViewState["HearingDt"] = dt;
    }
    protected void BindOfficeType()
    {
        try
        {

            ddlOfficeType.Items.Clear();
            ds1 = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType.DataTextField = "OfficeType_Name";
                ddlOfficeType.DataValueField = "OfficeType_Id";
                ddlOfficeType.DataSource = ds1;
                ddlOfficeType.DataBind();
            }
            ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }
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
            CaseDisDate_Div.Visible = false;
            CaseDisDoc_Div.Visible = false;
        }
    }
    protected void BindDisposeType()
    {
        try
        {
            ddlDisponsType.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDisponsType.DataTextField = "CaseDisposeType";
                ddlDisponsType.DataValueField = "CaseDisposeType_Id";
                ddlDisponsType.DataSource = ds;
                ddlDisponsType.DataBind();
            }
            ddlDisponsType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void UploadOrderDoc() // when Case Dispose Order Doc Filled.
    {
        try
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
                string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
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
                    strFileName = strName + "-Supplier-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/UploadOrderDoc/"), strFileName);
                    FielUpcaseDisposeOrderDoc.SaveAs(path);

                    ViewState["FileOrderDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BindYear()
    {
        List<int> List = new List<int>();
        ddlCaseYear.Items.Clear();
        for (int i = 2019; i <= DateTime.Now.Year; i++)
        {
            List.Add(i);
            ddlCaseYear.DataSource = List;
            ddlCaseYear.DataBind();
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void FieldClose()
    {
        Case_EditField.Visible = false;
        FieldSet_CaseDetail.Visible = false; ;
        FieldSet_DocumentDetail.Visible = false;
        FieldSet_ResponderDetail.Visible = false;
        Field_AddResponder.Visible = false;
    }
    protected void BindDetails()
    {
        try
        {
            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdCaseDoc.DataSource = null;
            GrdCaseDoc.DataBind();
            GrdCaseDispose.DataSource = null;
            GrdCaseDispose.DataBind();
            GrdHearingDtl_DB.DataSource = null;
            GrdHearingDtl_DB.DataBind();
            FieldClose();
            ds = obj.ByProcedure("USP_Legal_Select_ForWACaseDtl", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                FieldSet_CaseDetail.Visible = true;
                FieldSet_DocumentDetail.Visible = true;
                FieldSet_ResponderDetail.Visible = true;
                FieldSet_SaveHeringDtl.Visible = false;
                FiledSet_HearingDBDtl.Visible = true;
                // WPCase Dtl.
                lblRefCaseNO.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                lblWPCasetype.Text = ds.Tables[0].Rows[0]["Casetype_Name"].ToString();
                lblWPCaseYear.Text = ds.Tables[0].Rows[0]["CaseYear"].ToString();
                lblWPCourtType.Text = ds.Tables[0].Rows[0]["CourtTypeName"].ToString();
                lblWPCourtLocation.Text = ds.Tables[0].Rows[0]["CourtLocation"].ToString();
                lblWPPetionerName.Text = ds.Tables[0].Rows[0]["PetitonerName"].ToString();
                lblWPOfficeType.Text = ds.Tables[0].Rows[0]["OfficeType_Name"].ToString();
                lblWPOfficeName.Text = ds.Tables[0].Rows[0]["OfficeName"].ToString();
                lblWPCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                lblWPNOdalOfficerName.Text = ds.Tables[0].Rows[0]["NodalOfficerName"].ToString();
                lblWPNOdalOfficerMObile.Text = ds.Tables[0].Rows[0]["NodalOfficerMobileNo"].ToString();
                lblWPOICNAme.Text = ds.Tables[0].Rows[0]["OICName"].ToString();
                lblWPOICMobile.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                lblWPAdvocateName.Text = ds.Tables[0].Rows[0]["petiAdvocateName"].ToString();
                lblWPAdvocateMobile.Text = ds.Tables[0].Rows[0]["petiAdvocateMobile"].ToString();
                lblWPCaseSubject.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString();
                lblWPCaseDtl.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString();
                if (ds.Tables[0].Rows[0]["CaseStatus"].ToString() == "Pending")
                {
                    lblWPCaseStatus.Text = ds.Tables[0].Rows[0]["CaseStatus"].ToString();
                    lblWPCaseStatus.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lblWPCaseStatus.Text = ds.Tables[0].Rows[0]["CaseStatus"].ToString();
                    lblWPCaseStatus.ForeColor = System.Drawing.Color.Green;
                }
                GrdResponderDtl.DataSource = ds.Tables[1];  // Responder Dtl Bind.
                GrdResponderDtl.DataBind();



                // WACase And Petitoner Dtl.
                if (ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    lblRefWPCaseNo.Text = ds.Tables[2].Rows[0]["ReAppeal_CaseNo"].ToString();
                    lblWaCasetype.Text = ds.Tables[2].Rows[0]["Casetype_Name"].ToString();
                    lblWACaseYear.Text = ds.Tables[2].Rows[0]["CaseYear"].ToString();
                    lblWACourtLocation.Text = ds.Tables[2].Rows[0]["CourtLocation"].ToString();
                    lblWACourtType.Text = ds.Tables[2].Rows[0]["CourtTypeName"].ToString();
                    lblWAPetionerName.Text = ds.Tables[2].Rows[0]["PetitionerName"].ToString();
                    lblWAOfficeType.Text = ds.Tables[2].Rows[0]["OfficeType_Name"].ToString();
                    lblWAOfficeName.Text = ds.Tables[2].Rows[0]["OfficeName"].ToString();
                    lblWACaseNo.Text = ds.Tables[2].Rows[0]["ReAppeal_CaseNo"].ToString();
                    lblWANOdalOfficerName.Text = ds.Tables[2].Rows[0]["NodalofficerName"].ToString();
                    lblWANOdalOfficerMobile.Text = ds.Tables[2].Rows[0]["NodalOfficerMobileNo"].ToString();
                    lblWAOICNAme.Text = ds.Tables[2].Rows[0]["OICName"].ToString();
                    lblWAOICMobile.Text = ds.Tables[2].Rows[0]["OICMobileNo"].ToString();
                    lblWAAdvocateName.Text = ds.Tables[2].Rows[0]["DeptAdvocateName"].ToString();
                    lblWAAdvocateMobile.Text = ds.Tables[2].Rows[0]["DeptAdvocateMobileNo"].ToString();
                    lblWACaseSubject.Text = ds.Tables[2].Rows[0]["CaseSubject"].ToString();
                    lblWACaseDtl.Text = ds.Tables[2].Rows[0]["ReAppeal_CaseDetail"].ToString();

                    if (ds.Tables[2].Rows[0]["CaseStatus"].ToString() == "Pending")
                    {
                        lblWACaseStatus.Text = ds.Tables[2].Rows[0]["CaseStatus"].ToString();
                        lblWACaseStatus.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblWACaseStatus.Text = ds.Tables[2].Rows[0]["CaseStatus"].ToString();
                        lblWACaseStatus.ForeColor = System.Drawing.Color.Green;
                    }

                    if (ds.Tables[3].Rows[0]["CaseDisposalStatus"].ToString() == "Yes")
                    {
                        Fieldset_CaseDispose.Visible = true;
                        GrdCaseDispose.DataSource = ds.Tables[3];
                        GrdCaseDispose.DataBind();
                        CaseDipose_Div.Visible = false;
                    }
                    else { Fieldset_CaseDispose.Visible = false; }
                    GrdHearingDtl_DB.DataSource = ds.Tables[4]; // Hearing Dtl Bind.
                    GrdHearingDtl_DB.DataBind();
                    GrdCaseDoc.DataSource = ds.Tables[5]; // Documnets Bind.
                    GrdCaseDoc.DataBind();
                }
            }
            else
            {
                GrdResponderDtl.DataSource = null;
                GrdResponderDtl.DataBind();
                GrdCaseDoc.DataSource = null;
                GrdCaseDoc.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    protected void BindRespondertype()
    {
        try
        {
            ddlOfficetype_AddRes.Items.Clear();
            ddlOfficetype_EditRes.Items.Clear();
            ds = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficetype_AddRes.DataTextField = "OfficeType_Name";
                ddlOfficetype_AddRes.DataValueField = "OfficeType_Id";
                ddlOfficetype_AddRes.DataSource = ds;
                ddlOfficetype_AddRes.DataBind();

                ddlOfficetype_EditRes.DataTextField = "OfficeType_Name";
                ddlOfficetype_EditRes.DataValueField = "OfficeType_Id";
                ddlOfficetype_EditRes.DataSource = ds;
                ddlOfficetype_EditRes.DataBind();
            }
            ddlOfficetype_AddRes.Items.Insert(0, new ListItem("Select", "0"));
            ddlOfficetype_EditRes.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void BindCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = obj.ByProcedure("USP_Legal_GetCaseType", new string[] { }
           , new string[] { }, "dataset");
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
    protected void GrdResponderDtl_RowCommand(object sender, GridViewCommandEventArgs e)  // Navigate on the Edit Case Detail Div.
    {
        try
        {
            if (e.CommandName == "EditResponder")
            {
                lblMsg.Text = "";
                ViewState["ResponderID"] = "";
                ViewState["lblCaseID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblResponderID = (Label)row.FindControl("lblResponderID");
                Label lblCaseID = (Label)row.FindControl("lblCaseID");
                Label lblResponderName = (Label)row.FindControl("lblResponderName");
                Label lblResponderNo = (Label)row.FindControl("lblResponderNo");
                Label lblDepartent = (Label)row.FindControl("lblDepartent");
                Label lblAddress = (Label)row.FindControl("lblAddress");
                Label lblOfficeTypeID = (Label)row.FindControl("lblOfficeTypeID");
                Label lblOfficeID = (Label)row.FindControl("lblOfficeID");
                Label lblDesignationID = (Label)row.FindControl("lblDesignationID");

                txtResName_EditRes.Text = lblResponderName.Text;
                txtMobileNo_EditRes.Text = lblResponderNo.Text;
                txtDept_EditRes.Text = lblDepartent.Text;
                txtAdd_EditRes.Text = lblAddress.Text;
                ViewState["ResponderID"] = lblResponderID.Text;
                ViewState["lblCaseID"] = lblCaseID.Text;
                if (lblOfficeTypeID.Text != "")
                {
                    ddlOfficetype_EditRes.ClearSelection();
                    ddlOfficetype_EditRes.Items.FindByValue(lblOfficeTypeID.Text).Selected = true;
                }
                if (lblOfficeID.Text != "")
                {
                    ddlOfficetype_EditRes_SelectedIndexChanged(sender, e);
                    ddlOffice_EditRes.ClearSelection();
                    ddlOffice_EditRes.Items.FindByValue(lblOfficeID.Text).Selected = true;
                }
                if (lblDesignationID.Text != "")
                {
                    ddlDesig_EditRes.ClearSelection();
                    ddlDesig_EditRes.Items.FindByValue(lblDesignationID.Text).Selected = true;
                }
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
    protected void lnkEditCaseDtl_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
            {
                Case_EditField.Visible = true;
                FieldSet_CaseDetail.Visible = false; ;
                FieldSet_DocumentDetail.Visible = false;
                FieldSet_ResponderDetail.Visible = false;
                Field_AddResponder.Visible = false;
                Fieldset_CaseDispose.Visible = false;
                Fieldset_HearingDtl.Visible = false;
                lnkAddResponderDtl.Visible = true;
                lnkEditCaseDtl.Visible = false;
                lnkBackbtn.Visible = true;
                ViewState["WACaseID"] = "";
                ds = obj.ByProcedure("USP_Legal_Select_ForEditWACaseDtl", new string[] { "WPCase_ID" }
                    , new string[] { ViewState["ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblRefrenceCaseNo.Text = ds.Tables[0].Rows[0]["WPCaseNo"].ToString();
                    txtPetitionerName.Text = ds.Tables[0].Rows[0]["WAPetitionerName"].ToString();
                    txtJusticeName.Text = ds.Tables[0].Rows[0]["JusticeName"].ToString();
                    if (ds.Tables[0].Rows[0]["ReAppeal_CaseNo"].ToString() != "")
                    {
                        txtReAppealCaseNo.Text = ds.Tables[0].Rows[0]["ReAppeal_CaseNo"].ToString();
                        btnUpdateWaDtl.Text = "Update";
                        ViewState["WACaseID"] = ds.Tables[0].Rows[0]["ReAppealCase_ID"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Designation_Id"].ToString() != "")
                    {
                        ddlDesignation.ClearSelection();
                        ddlDesignation.Items.FindByValue(ds.Tables[0].Rows[0]["Designation_Id"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["CourtType_Id"].ToString() != "")
                    {
                        ddlCourtType.ClearSelection();
                        ddlCourtType.Items.FindByValue(ds.Tables[0].Rows[0]["CourtType_Id"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["CaseSubject_ID"].ToString() != "")
                    {
                        ddlCaseSubject.ClearSelection();
                        ddlCaseSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubject_ID"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["NodalofficerName"].ToString() != "")
                    {
                        txtNOdalOfficerName.Text = ds.Tables[0].Rows[0]["NodalofficerName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["NodalofficerMobileNo"].ToString() != "")
                    {
                        txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["NodalofficerMobileNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["OICName"].ToString() != "")
                    {
                        txtOicName.Text = ds.Tables[0].Rows[0]["OICName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["OICMobileNo"].ToString() != "")
                    {
                        txtOicMobileNO.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["ReAppeal_CaseDetail"].ToString() != "")
                    {
                        txtCaseDetail.Text = ds.Tables[0].Rows[0]["ReAppeal_CaseDetail"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DeptAdvocateName"].ToString() != "")
                    {
                        txtDeptAdvocateName.Text = ds.Tables[0].Rows[0]["DeptAdvocateName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DeptAdvocateMobileNo"].ToString() != "")
                    {
                        txtDeptAdvocateMobileNo.Text = ds.Tables[0].Rows[0]["DeptAdvocateMobileNo"].ToString();
                        txtDeptAdvocateEmail_ID.Text = ds.Tables[0].Rows[0]["DeptAdvocateEmail_ID"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CaseDisposalStatus"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[0]["CaseDisposalStatus"].ToString() == "Yes")
                        {
                            rdCaseDispose.ClearSelection();
                            rdCaseDispose.Items.FindByText(ds.Tables[0].Rows[0]["CaseDisposalStatus"].ToString()).Selected = true;
                            rdCaseDispose_SelectedIndexChanged(sender, e);
                            ddlDisponsType.ClearSelection();
                            ddlDisponsType.Items.FindByValue(ds.Tables[0].Rows[0]["CaseDisposalType_Id"].ToString()).Selected = true;
                            ddlDisponsType_SelectedIndexChanged(sender, e);
                            txtCaseDis_OrderTimeline.Text = ds.Tables[0].Rows[0]["CaseDisposal_timeline"].ToString();

                        }
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
                        ddlOfficeType.Items.Clear();
                        BindOfficeType();
                        ddlOfficeType.Items.FindByValue(ds.Tables[0].Rows[0]["OfficeType_Id"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["OfficeName"].ToString() != "")
                    {
                        txtOfficeName.Text = ds.Tables[0].Rows[0]["OfficeName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["HighpriorityCaseStatus"].ToString() != "")
                    {
                        ddlHighPriorityCase.ClearSelection();
                        ddlHighPriorityCase.Items.FindByText(ds.Tables[0].Rows[0]["HighpriorityCaseStatus"].ToString()).Selected = true;
                    }
                    if (ds.Tables[0].Rows[0]["OICMaster_ID"].ToString() != "")
                    {
                        ddlOicName.ClearSelection();
                        ddlOicName.Items.FindByValue(ds.Tables[0].Rows[0]["OICMaster_ID"].ToString()).Selected = true;
                        ddlOicName_SelectedIndexChanged(sender, e);
                    }
                    if (ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString() != "")
                    {
                        ddlCaseSubject_SelectedIndexChanged(sender, e);
                        ddlCase_SubjSubject.ClearSelection();
                        ddlCase_SubjSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()).Selected = true;
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
            lnkEditCaseDtl.Visible = true;
            lnkAddResponderDtl.Visible = false;
            lnkBackbtn.Visible = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnAddResponder_Click(object sender, EventArgs e) // Add New Responder.
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnAddResponder.Text == "Add" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_Insert_WACaseRespoderDtl", new string[] { "WPCase_ID", "Officetype_Id", "Office_Id", "Designation_Id", "ResponderName", "ResponderNo", "ResponderAddress", "ResponderDepartMent", "CreatedBy", "CreatedByIP" }
                        , new string[] { ViewState["ID"].ToString(), ddlOfficetype_AddRes.SelectedValue, ddlOfficetype_AddRes.SelectedValue, ddlDesignation_AddRes.SelectedValue, txtAddResponderName.Text.Trim(), txtAddResponderMobileNo.Text.Trim(), txtAddResponderAddress.Text.Trim(), txtAddResponderDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnAddResponder.Text == "Update" && ViewState["ResponderID"].ToString() != null && ViewState["ResponderID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_UpdateWAResponderDtl", new string[] { "Responder_ID", "Officetype_Id", "Office_Id", "Designation_Id", "WACase_ID", "ResponderName", "ResponderNo", "ResponderAddress", "ResponderDepartMent", "LastupdatedBy", "LastupdatedByIP" }
                        , new string[] { ViewState["ResponderID"].ToString(), ddlOfficetype_EditRes.SelectedValue, ddlOffice_EditRes.SelectedValue, ddlDesig_EditRes.SelectedValue, ViewState["lblCaseID"].ToString(), txtResName_EditRes.Text.Trim(), txtMobileNo_EditRes.Text.Trim(), txtAdd_EditRes.Text.Trim(), txtDept_EditRes.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtResName_EditRes.Text = "";
                        txtMobileNo_EditRes.Text = "";
                        txtAdd_EditRes.Text = "";
                        txtDept_EditRes.Text = "";
                        ddlofficeName_AddRes.ClearSelection();
                        ddlDesignation_AddRes.ClearSelection();
                        txtAddResponderName.Text = "";
                        txtAddResponderMobileNo.Text = "";
                        txtAddResponderDepartment.Text = "";
                        txtAddResponderAddress.Text = "";
                        ddlOfficetype_EditRes.ClearSelection();
                        ddlOfficetype_AddRes.ClearSelection();
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
    protected void lnkBackbtn_Click(object sender, EventArgs e) // For Back Button
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
            lnkAddResponderDtl.Visible = true;
            lnkEditCaseDtl.Visible = true;
            lnkBackbtn.Visible = false;
            BindDetails();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnSaveDoc_Click(object sender, EventArgs e) // Add & Edit Document.
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                ViewState["FileUploadDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                    string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileUpload1.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
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
                        strFileName = strName + "-WaCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/WaCaseDoc/"), strFileName);
                        FileUpload1.SaveAs(path);

                        ViewState["FileUploadDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }

                }
                else if (fileUpload2_EditDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(fileUpload2_EditDoc.FileName).Substring(1);
                    string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (fileUpload2_EditDoc.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
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
                        strFileName = strName + "-WaCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/WaCaseDoc/"), strFileName);
                        fileUpload2_EditDoc.SaveAs(path);

                        ViewState["FileUploadDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in(png, jpg, jpeg, pdf, JPG, JPEG, PNG, PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 500 KB \\n"; }

                if (errormsg == "")
                {
                    if (btnSaveDoc.Text == "Upload Doc")
                    {
                        ds = obj.ByProcedure("USP_Legal_Insert_WACaseDocDtl", new string[] { "WPCase_ID", "DocName", "DocPath", "CreatedBy", "CreatedByIP" }
                            , new string[] { ViewState["ID"].ToString(), txtDocumentName.Text.Trim(), ViewState["FileUploadDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSaveDoc.Text == "Edit Doc" && ViewState["DocID"].ToString().ToString() != "" && ViewState["DocID"].ToString() != null)
                    {
                        ds = obj.ByProcedure("USP_Legal_Update_WACaseDocDtl", new string[] { "WADoc_ID", "WACase_Id", "DocName", "DocPath", "LastupdatedBy", "LastupdatedbyIP" }
                            , new string[] { ViewState["DocID"].ToString(), ViewState["WaCaseID"].ToString(), txtEditDocumentsName.Text.Trim(), ViewState["FileUploadDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            ViewState["FileUploadDOC"] = "";
                            FiledSet_HearingDBDtl.Visible = true;
                            FieldSet_SaveHeringDtl.Visible = true;
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
    protected void GrdCaseDoc_RowCommand(object sender, GridViewCommandEventArgs e)// on row command Event for Edit Document
    {
        try
        {
            ViewState["DocID"] = "";
            ViewState["WaCaseID"] = "";
            if (e.CommandName == "EditDocument")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDocumentID = (Label)row.FindControl("lblDocumentID");
                Label lblWaCaseID = (Label)row.FindControl("lblWaCaseID");
                Label lblDocName = (Label)row.FindControl("lblDocName");
                txtEditDocumentsName.Text = lblDocName.Text;
                ViewState["DocID"] = lblDocumentID.Text;
                ViewState["WaCaseID"] = lblWaCaseID.Text;
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
    protected void ddlDisponsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            CaseDisDate_Div.Visible = false;
            CaseDisDoc_Div.Visible = false;
            if (ddlDisponsType.SelectedIndex > 0)
            {
                CaseDisDate_Div.Visible = true;
                CaseDisDoc_Div.Visible = true;
                HearingDtl_CaseDispose.Visible = true;
                CaseDisOrderTime_Div.Visible = true;
                // Div_AuthCaseDispose.Visible = true;
            }
            else
            {
                HearingDtl_CaseDispose.Visible = false;
                CaseDisDate_Div.Visible = false;
                CaseDisDoc_Div.Visible = false;
                //   Div_AuthCaseDispose.Visible = false;
                CaseDisOrderTime_Div.Visible = false;
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
                ddlDisponsType.ClearSelection();
            }
            else
            {
                caseDisposeYes.Visible = false;
                HearingDtl_CaseDispose.Visible = false;
                CaseDisDate_Div.Visible = false;
                CaseDisDoc_Div.Visible = false;
                //Div_AuthCaseDispose.Visible = false;
                CaseDisOrderTime_Div.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void BindCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            ds = obj.ByDataSet("SELECT CaseSubject, CaseSubjectID FROM tbl_LegalMstCaseSubject");
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
    protected void btnUpdateWaDtl_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnUpdateWaDtl.Text == "Save" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Insert_WaCaseDetail", new string[] { "Case_ID", "ReAppeal_CaseNo", "Casetype_ID", "PetitionerName", 
                        "Designation_Id", "CourtType_ID","CourtLocatinDistrict_ID", "OfficeType_Id", "OfficeName", "CaseYear", "NodalofficerName",
                        "NodalofficerMobileNo", "NodalofficerEmail_ID", "OICMaster_ID", "DeptAdvocateName", "DeptAdvocateMobileNo", "DeptAdvocateEmail_ID", "JustriceName",
                        "HighpriorityCaseStatus", "ReAppeal_CaseDetail", "CaseSubject_ID", "CaseSubSubj_Id", "CreatedBy", "CreatedByIP" },
                         new string[] { ViewState["ID"].ToString(), txtReAppealCaseNo.Text.Trim(), ddlCasetype.SelectedValue, txtPetitionerName.Text.Trim(),
                             ddlDesignation.SelectedValue, ddlCourtType.SelectedValue,ddlDistrict.SelectedValue, ddlOfficeType.SelectedValue,  txtOfficeName.Text.Trim(),ddlCaseYear.SelectedItem.Text.Trim(),txtNOdalOfficerName.Text.Trim(),
                             txtNodalOfficerMobileNo.Text.Trim(), txtNodalOfficerEmail.Text.Trim(),ddlOicName.SelectedValue, txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), txtDeptAdvocateEmail_ID.Text.Trim(),txtJusticeName.Text.Trim(),
                         ddlHighPriorityCase.SelectedItem.Text,txtCaseDetail.Text.Trim(),ddlCaseSubject.SelectedValue,ddlCase_SubjSubject.SelectedValue,ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress()}, "dataset");
                }
                else if (btnUpdateWaDtl.Text == "Update")
                {
                    ds = obj.ByProcedure("USP_Update_WACaseDetail", new string[] {"Case_ID", "ReAppeal_CaseNo", "Casetype_ID", "PetitionerName", 
                        "Designation_Id", "CourtType_ID","CourtLocatinDistrict_ID", "OfficeType_Id", "OfficeName", "CaseYear", "NodalofficerName",
                        "NodalofficerMobileNo", "NodalofficerEmail_ID", "OICMaster_ID", "DeptAdvocateName", "DeptAdvocateMobileNo", "DeptAdvocateEmail_ID", "JustriceName",
                        "HighpriorityCaseStatus", "ReAppeal_CaseDetail", "CaseSubject_ID", "CaseSubSubj_Id", "LastUpdatedBy", "LastUpdatedByIP", "ReAppealCase_ID" }
                        , new string[] { ViewState["ID"].ToString(), txtReAppealCaseNo.Text.Trim(), ddlCasetype.SelectedValue, txtPetitionerName.Text.Trim(),
                             ddlDesignation.SelectedValue, ddlCourtType.SelectedValue,ddlDistrict.SelectedValue, ddlOfficeType.SelectedValue,  txtOfficeName.Text.Trim(),ddlCaseYear.SelectedItem.Text.Trim(),txtNOdalOfficerName.Text.Trim(),
                             txtNodalOfficerMobileNo.Text.Trim(), txtNodalOfficerEmail.Text.Trim(),ddlOicName.SelectedValue, txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), txtDeptAdvocateEmail_ID.Text.Trim(),txtJusticeName.Text.Trim(),
                         ddlHighPriorityCase.SelectedItem.Text,txtCaseDetail.Text.Trim(),ddlCaseSubject.SelectedValue,ddlCase_SubjSubject.SelectedValue,ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["WACaseID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtPetitionerName.Text = "";
                        ddlCasetype.ClearSelection();
                        txtCaseSubject.Text = "";
                        txtReAppealCaseNo.Text = "";
                        txtNOdalOfficerName.Text = "";
                        txtNodalOfficerMobileNo.Text = "";
                        txtOicName.Text = "";
                        txtOicMobileNO.Text = "";
                        ddlDistrict.ClearSelection();
                        txtCaseDetail.Text = "";
                        ddlDisponsType.ClearSelection();
                        CaseDisposeStatus();
                        ViewState["FileOrderDOC"] = "";
                        lnkEditCaseDtl.Visible = true;
                        lnkBackbtn.Visible = false;
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
    protected void lnkbtnAddNewHering_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            AddNewHearing.Visible = true;
            FiledSet_HearingDBDtl.Visible = false;
            btnHearingBack.Visible = true;
            lnkbtnAddNewHering.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnAddHearing_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ViewState["HearingDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (fileDoc_AddHearing.HasFile)// CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(fileDoc_AddHearing.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (fileDoc_AddHearing.PostedFile.ContentLength > 204800) // 500 KB = 1024 * 500
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = fileDoc_AddHearing.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-WAHearing-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/WACaseHearingDoc/"), strFileName);
                        fileDoc_AddHearing.SaveAs(path);

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
                        dt1.Rows.Add(Convert.ToDateTime(txtHearingDate_AddHearing.Text, cult).ToString("yyyy/MM/dd"), ddlHearingDtl_AddHearing.SelectedItem.Text.Trim(), ViewState["HearingDOC"].ToString(), txtInstruc_AddHearing.Text.Trim());
                    }

                    if (dt1.Rows.Count > 0)
                    {
                        FieldSet_SaveHeringDtl.Visible = true;
                        btnSaveHearingDtl.Visible = true;
                        txtHearingDate_AddHearing.Text = "";
                        ddlHearingDtl_AddHearing.ClearSelection();
                        ViewState["HearingDOC"] = "";
                        Grd_AddHearing.DataSource = dt1;
                        Grd_AddHearing.DataBind();
                        ViewState["HearingDt"] = dt1;

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnSaveHearingDtl_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            ViewState["EditHearingDOC"] = "";
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
                    strFileName = strName + "-WAHearing-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/WACaseHearingDoc/"), strFileName);
                    FileUpEditHearigDoc.SaveAs(path);

                    ViewState["EditHearingDOC"] = strFileName;
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
                    ds = obj.ByProcedure("USP_Legal_Insert_WACaseHearingDetail", new string[] { "WPCase_ID", "CreatedBy", "CreatedByIP" }
                        , new string[] { ViewState["ID"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() },
                        new string[] { "type_HearingDetail" }, new DataTable[] { dtHearing }, "dataset");
                    dtHearing.Clear();
                }
                else if (btnSaveHearingDtl.Text == "Update" && ViewState["HearingID"] != "" && ViewState["HearingID"] != null)
                {
                    ds = obj.ByProcedure("USP_Legal_Update_WACaseHearingDtl", new string[] { "WACase_ID", "NxtHearingDate", "HearingDtl", "InstructionByCourt", "HearingDoc", "LastupdatedBy", "LastupdatedByIP", "WaNxtHearing_ID" }
                        , new string[] { ViewState["WaCaseIDHearing"].ToString(), Convert.ToDateTime(txtEditHearingDate.Text, cult).ToString("yyyy/MM/dd"), ddleditHearing.SelectedItem.Text.Trim(), txtEditHearing_Inst.Text.Trim(), ViewState["EditHearingDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["HearingID"].ToString() }
                       , "dataset");
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
    protected void GrdHearingDtl_DB_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["WaCaseIDHearing"] = "";
            ViewState["HearingID"] = "";
            if (e.CommandName == "EditHearing")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblWACaseID = (Label)row.FindControl("lblWACaseID");
                Label lblHearingID = (Label)row.FindControl("lblHearingID");
                Label lblHearingDate = (Label)row.FindControl("lblHearingDate");
                Label lblHearingDetail = (Label)row.FindControl("lblHearingDetail");
                Label lblInstructionbycourt = (Label)row.FindControl("lblInstructionbycourt");

                txtEditHearingDate.Text = lblHearingDate.Text;
                ViewState["WaCaseIDHearing"] = lblWACaseID.Text;
                ViewState["HearingID"] = lblHearingID.Text;
                ddleditHearing.ClearSelection();
                ddleditHearing.Items.FindByText(lblHearingDetail.Text).Selected = true;
                if (lblInstructionbycourt.Text != "")
                {
                    ddleditHearing_SelectedIndexChanged(sender, e);
                    txtEditHearing_Inst.Text = lblInstructionbycourt.Text;
                }
                btnSaveHearingDtl.Text = "Update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#ModalEditHearingDtl').modal('show')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnCaseDispose_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnCaseDispose.Text == "Case Disposal")
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
                            strFileName = strName + "-WACaseDispose-" + strTimeStamp + strExtension;
                            string path = Path.Combine(Server.MapPath("../Legal/WACaseDispose/"), strFileName);
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
                        ds = obj.ByProcedure("USP_Legal_UpdateWACaseDisposeDtl", new string[] { "WPCase_ID", "CaseDisposeType_Id", "CaseDisposeDate", "CaseDisposeStatus", "CaseDsiposeOrderNo", "CaseDisposeOrderDoc", "LastIsactiveBy", "LastIsactiveByIP", "IAuthority_ID" }
                            , new string[] { ViewState["ID"].ToString(), ddlDisponsType.SelectedValue, Convert.ToDateTime(txtCaseDis_Date.Text, cult).ToString("yyyy/MM/dd"), rdCaseDispose.SelectedItem.Text.Trim(), txtCaseDis_OrderTimeline.Text.Trim(), ViewState["FileOrderDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ddlAutorityList.SelectedValue }, "dataset");

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                                ddlDisponsType.ClearSelection();
                                ViewState["FileOrderDOC"] = "";
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
    protected void btnHearingBack_Click(object sender, EventArgs e)
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
            btnHearingBack.Visible = false;
            lnkbtnAddNewHering.Visible = true;
            BindDetails();

            AddNewHearing.Visible = false;
            FiledSet_HearingDBDtl.Visible = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtOicMobileNO.Text = ""; txtOICEmailID.Text = "";
            if (ddlOicName.SelectedIndex > 0)
            {
                DataSet ds1 = obj.ByDataSet("select OICName, OICMobileNo,OICEmailID from tblOICMaster where OICMaster_ID =" + ddlOicName.SelectedValue);
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    txtOICEmailID.Text = ds1.Tables[0].Rows[0]["OICEmailID"].ToString();
                    txtOicMobileNO.Text = ds1.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    DivOicMobile.Visible = true;
                    DivOicEmailID.Visible = true;
                }
            }
            else
            {
                DivOicMobile.Visible = false;
                DivOicEmailID.Visible = false;
            }
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
            ddlCase_SubjSubject.Items.Clear();
            if (ddlCaseSubject.SelectedIndex > 0)
            {
                ds = obj.ByDataSet("SELECT CaseSubSubj_Id, CaseSubSubject FROM tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlCase_SubjSubject.DataTextField = "CaseSubSubject";
                    ddlCase_SubjSubject.DataValueField = "CaseSubSubj_Id";
                    ddlCase_SubjSubject.DataSource = ds;
                    ddlCase_SubjSubject.DataBind();
                }
            }
            ddlCase_SubjSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlOfficetype_AddRes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlOfficetype_AddRes.SelectedIndex > 0)
            {
                ddlofficeName_AddRes.Items.Clear();
                ds = obj.ByDataSet("select Office_Id, OfficeName from tblOfficeMaster where OfficeType_Id=" + ddlOfficetype_AddRes.SelectedValue);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlofficeName_AddRes.DataTextField = "OfficeName";
                    ddlofficeName_AddRes.DataValueField = "Office_Id";
                    ddlofficeName_AddRes.DataSource = ds;
                    ddlofficeName_AddRes.DataBind();
                }
            }
            ddlofficeName_AddRes.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlHearingDtl_AddHearing_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlHearingDtl_AddHearing.SelectedValue == "3")
            {
                instructionDiv.Visible = true;
            }
            else { instructionDiv.Visible = false; }
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
            lblMsg.Text = "";
            if (ddlOfficetype_EditRes.SelectedIndex > 0)
            {
                ddlOffice_EditRes.Items.Clear();
                ds = obj.ByDataSet("select Office_Id, OfficeName from tblOfficeMaster where OfficeType_Id=" + ddlOfficetype_EditRes.SelectedValue);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice_EditRes.DataTextField = "OfficeName";
                    ddlOffice_EditRes.DataValueField = "Office_Id";
                    ddlOffice_EditRes.DataSource = ds;
                    ddlOffice_EditRes.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
                }
            }
            ddlOffice_EditRes.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddleditHearing_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddleditHearing.SelectedValue == "3")
            {
                EditHearing_InsDiv.Visible = true;
            }
            else
            {
                EditHearing_InsDiv.Visible = false;
                txtEditHearing_Inst.Text = "";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#ModalEditHearingDtl').modal('show')", true);

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}