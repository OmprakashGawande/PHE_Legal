<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="EditWACaseDetails.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Legal_EditWACaseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <asp:ValidationSummary ID="VDS2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddResponder" />
    <asp:ValidationSummary ID="VDS3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDtl" />
    <asp:ValidationSummary ID="VDS4" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="CaseDispose" />
    <asp:ValidationSummary ID="VDS5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Hearing" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="EditRes" />
    <%-- Start Here Using POPUP Modal for Edit Responder --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Edit Respondent Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Respondent</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Type<span style="color:red"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfveditres_officetype" ValidationGroup="EditRes"
                                            ErrorMessage="Select Office Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficetype_EditRes" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOfficetype_EditRes" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOfficetype_EditRes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name<span style="color:red"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfveditres_office" ValidationGroup="EditRes"
                                            ErrorMessage="Select Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOffice_EditRes" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOffice_EditRes" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Name<span style="color:red"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfveditres_name" ValidationGroup="EditRes"
                                            ErrorMessage="Enter Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtResName_EditRes" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtResName_EditRes" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Designation<span style="color:red"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfveditres_desig" ValidationGroup="EditRes"
                                            ErrorMessage="Select Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDesig_EditRes" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDesig_EditRes" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Mobile No.<span style="color:red"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvEditResMobileNo" ValidationGroup="EditRes"
                                            ErrorMessage="Enter Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtMobileNo_EditRes" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtMobileNo_EditRes" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                          <asp:RegularExpressionValidator ID="RevEditResMobileNo" ValidationGroup="EditRes" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo_EditRes"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Department<span style="color:red"></span></label>
                                        <asp:RequiredFieldValidator ID="refvEditre_Dept" ValidationGroup="EditRes"
                                            ErrorMessage="Enter Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtDept_EditRes" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtDept_EditRes" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:RequiredFieldValidator ID="refvEditre_Address" ValidationGroup="EditRes"
                                            ErrorMessage="Enter Address." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAdd_EditRes" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAdd_EditRes" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Update" ID="btnYes" Style="margin-top: 20px; width: 80px;" ValidationGroup="EditRes" OnClick="btnAddResponder_Click" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Using POPUP Modal for Edit Responder --%>
    <%-- Start Here Edit Documents  ---%>
    <div class="modal fade" id="MymodalEditDocuments" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 50%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left">Edit Documents</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Documents</legend>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Documents Name </label>
                                        <asp:TextBox ID="txtEditDocumentsName" runat="server" onkeyup="javascript:capFirst(this);" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Document Upload</label>
                                        <asp:FileUpload ID="fileUpload2_EditDoc" runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Edit Doc" ID="btnEditDocuments" Style="margin-top: 20px; width: 80px;" OnClick="btnSaveDoc_Click" />
                        <asp:Button ID="Button2" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Edit Documnets --%>

    <%-- Start Here Edit Hearing Dtl --%>
    <div class="modal fade" id="ModalEditHearingDtl" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 80%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left">Edit Hearing Details</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Edit Hearing</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing Date </label>
                                        <asp:TextBox ID="txtEditHearingDate" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off" data-date-start-date="0d"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing Detail</label>
                                        <asp:DropDownList ID="ddleditHearing" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddleditHearing_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Next Hearing Date</asp:ListItem>
                                            <asp:ListItem Value="2">Not Hearing</asp:ListItem>
                                            <asp:ListItem Value="3">Instruction By Court</asp:ListItem>
                                            <asp:ListItem Value="4">Disposed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="EditHearing_InsDiv" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Instruction</label>
                                        <asp:TextBox ID="txtEditHearing_Inst" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hearing Document</label>
                                        <asp:FileUpload ID="FileUpEditHearigDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Update" ID="btnEditHearing" Style="margin-top: 20px; width: 80px;" OnClick="btnSaveHearingDtl_Click" />
                        <asp:Button ID="btnHearing" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%-- End Here Edit Hearing Dtl --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        <div class="card-title" style="display: inline; float: left;">
                            WA/RP/SLP Case Detail
                        </div>
                        <div class="float-right">
                            <a href="WPCaseList.aspx" class="btn-sm label label-primary">Home</a>
                            <asp:LinkButton ID="lnkEditCaseDtl" runat="server" CssClass="btn-sm label label-success" OnClick="lnkEditCaseDtl_Click">Edit Case Detail</asp:LinkButton>
                            <asp:LinkButton ID="lnkAddResponderDtl" runat="server" CssClass="btn-sm label-info" OnClick="lnkAddResponderDtl_Click">Add Responder</asp:LinkButton>
                            <asp:LinkButton ID="lnkBackbtn" runat="server" CssClass="btn-sm label-danger" Visible="false" OnClick="lnkBackbtn_Click">Back</asp:LinkButton>
                        </div>
                    </div>
                    <div class="card-body">
                        <%-- Start Here Bind Case && Petitioner Detail --%>
                        <fieldset id="FieldSet_CaseDetail" runat="server" visible="false">
                            <legend>Case Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <table class="table table-bordered table-hover">
                                        <tr>
                                            <th>Particular</th>
                                            <th>previous Case Detail</th>
                                            <th>Current Case Detail</th>
                                        </tr>
                                        <tr>
                                            <td>Previous/Current Case No</td>
                                            <td>
                                                <asp:Label ID="lblRefCaseNO" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblRefWPCaseNo" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                          <tr style="display:none;">
                                            <td>previous/Current Case No</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseNo" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseNo" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Case Type</td>
                                            <td>
                                                <asp:Label ID="lblWPCasetype" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWaCasetype" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Case Year</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseYear" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseYear" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Court</td>
                                            <td>
                                                <asp:Label ID="lblWPCourtType" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACourtType" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                         <tr>
                                            <td>Court Location</td>
                                            <td>
                                                <asp:Label ID="lblWPCourtLocation" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACourtLocation" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Petitioner Name</td>
                                            <td>
                                                <asp:Label ID="lblWPPetionerName" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAPetionerName" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Office Type</td>
                                            <td>
                                                <asp:Label ID="lblWPOfficeType" runat="server" CssClass=""></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOfficeType" runat="server" CssClass=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Office Name</td>
                                            <td>
                                                <asp:Label ID="lblWPOfficeName" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOfficeName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Nodal Officer Name</td>
                                            <td>
                                                <asp:Label ID="lblWPNOdalOfficerName" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWANOdalOfficerName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Nodal Officer Mobile No.</td>
                                            <td>
                                                <asp:Label ID="lblWPNOdalOfficerMObile" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWANOdalOfficerMobile" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>OIC Name</td>
                                            <td>
                                                <asp:Label ID="lblWPOICNAme" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOICNAme" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>OIC Mobile No.</td>
                                            <td>
                                                <asp:Label ID="lblWPOICMobile" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAOICMobile" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Advocate Name</td>
                                            <td>
                                                <asp:Label ID="lblWPAdvocateName" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAAdvocateName" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Advocate Mobile No.</td>
                                            <td>
                                                <asp:Label ID="lblWPAdvocateMobile" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWAAdvocateMobile" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td>Case Subject</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseSubject" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseSubject" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Case Details</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseDtl" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseDtl" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Status</td>
                                            <td>
                                                <asp:Label ID="lblWPCaseStatus" Font-Bold="true" runat="server"></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblWACaseStatus" Font-Bold="true" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Bind Case && Petitioner Detail --%>
                        <%-- Start Here Bind Documnet Detail --%>
                        <fieldset id="FieldSet_DocumentDetail" runat="server" visible="false">
                            <legend>Case Document</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Document Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RfvAddDocumnet" ValidationGroup="Save"
                                            ErrorMessage="Enter Document Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtDocumentName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>

                                        <asp:TextBox ID="txtDocumentName" runat="server" onkeyup="javascript:capFirst(this);"  CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Document Upload</label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"></asp:FileUpload>
                                        <span style="color: red; font-size:13px;font-weight:700;">Document Should be 200Kb.</span>
                                    </div>
                                </div>
                                <div class="col-md-3" style="padding-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnSaveDoc" runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" Text="Upload Doc" OnClick="btnSaveDoc_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row pt-4">
                                <div class="col-md-9">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GrdCaseDoc" runat="server" CssClass="table table-bordered text-center" AutoGenerateColumns="false" DataKeyNames="WADoc_ID" OnRowCommand="GrdCaseDoc_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        <asp:Label ID="lblWaCaseID" runat="server" Text='<%# Eval("ReAppealCase_ID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblDocumentID" runat="server" Text='<%# Eval("WADoc_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyperViewDoc" runat="server" CssClass="fa fa-eye" ToolTip="View" Target="_blank" Enabled='<%#  Eval("DocPath").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/WaCaseDoc/" +  Eval("DocPath") %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEditDoc" runat="server" CssClass="fa fa-edit" CommandArgument='<%# Eval("WADoc_ID") %>' CommandName="EditDocument" ToolTip="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Bind Document Detail --%>

                        <%-- Start Here Bind Responder Detail --%>
                        <fieldset id="FieldSet_ResponderDetail" runat="server" visible="false">
                            <legend>Responder Details</legend>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GrdResponderDtl" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="Responder_ID" AutoGenerateColumns="false" OnRowCommand="GrdResponderDtl_RowCommand" EmptyDataText="NO RECORD FOUND">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    <asp:Label ID="lblResponderID" runat="server" Text='<%# Eval("Responder_ID") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblCaseID" runat="server" Text='<%# Eval("ReAppealCase_ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Type Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeTypeName" runat="server" Text='<%# Eval("OfficeType_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblOfficeTypeID" runat="server" Text='<%# Eval("Officetype_Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Office Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOfficeName" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                    <asp:Label ID="lblOfficeID" runat="server" Text='<%# Eval("Office_Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Responder Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponderName" runat="server" Text='<%# Eval("ResponderName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignationName" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblDesignationID" runat="server" Text='<%# Eval("Designation_Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Responder Mobile No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResponderNo" runat="server" Text='<%# Eval("ResponderNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Department">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepartent" runat="server" Text='<%# Eval("ResponderDepartMent") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("ResponderAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Responder_ID") %>' CommandName="EditResponder" ToolTip="Edit" CssClass="fa fa-edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Bind Responder Detail --%>

                        <%--Start Here BInd Add add Hearing Dtl --%>
                        <fieldset id="Fieldset_HearingDtl" runat="server">
                            <legend>Hearing Detail</legend>
                            <div class="row float-right" style="margin: -2% 0 0 0;">
                                <asp:LinkButton ID="lnkbtnAddNewHering" runat="server" CssClass="btn-sm label label-success" OnClick="lnkbtnAddNewHering_Click">Add Hearing</asp:LinkButton>
                                &nbsp;&nbsp;
                                 <asp:LinkButton ID="btnHearingBack" runat="server" CssClass="btn-sm label label-danger" OnClick="btnHearingBack_Click" Visible="false">Back</asp:LinkButton>
                            </div>
                            <div class="pt-1"></div>
                            <div class="" id="AddNewHearing" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Hearing Date </label>
                                            <span style="color: red;"><b>* </b></span>
                                            <asp:RequiredFieldValidator ID="RfvHearingDate_AddHearing" ValidationGroup="Hearing"
                                                ErrorMessage="Enter Hearing Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtHearingDate_AddHearing" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                            <asp:TextBox ID="txtHearingDate_AddHearing" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off" data-date-start-date="0d"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Hearing Detail</label>
                                            <asp:DropDownList ID="ddlHearingDtl_AddHearing" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlHearingDtl_AddHearing_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Next Hearing Date</asp:ListItem>
                                                <asp:ListItem Value="2">Not Hearing</asp:ListItem>
                                                <asp:ListItem Value="3">Instruction By Court</asp:ListItem>
                                                <asp:ListItem Value="4">Disposed</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6" id="instructionDiv" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Instruction</label>
                                            <asp:TextBox ID="txtInstruc_AddHearing" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="100"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Hearing Document</label>
                                            <asp:FileUpload ID="fileDoc_AddHearing" runat="server" CssClass="form-control" />
                                            <span style="color: red; font-size:13px;font-weight:700;">Document Should be 200Kb.</span>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="padding-top: 2rem! important;">
                                        <asp:Button ID="btnAddHearing" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="Hearing" Text="Add" OnClick="btnAddHearing_Click" />
                                    </div>
                                </div>
                                <fieldset id="FieldSet_SaveHeringDtl" runat="server" visible="false">
                                    <legend>Hearing Detail</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="Grd_AddHearing" runat="server" AutoGenerateColumns="false" CssClass="table" BorderWidth="2px" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowID" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hearing Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("HearingDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hearing Detail">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHearingDetail" runat="server" Text='<%# Eval("HearingDetail") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Instruction">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinstruction" runat="server" Text='<%# Eval("Instruction") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lblHearingDoc" runat="server" Target="_blank" Enabled='<%#  Eval("HearingDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/WACaseHearingDoc/" + Eval("HearingDoc") %>' CssClass="fa fa-eye" ToolTip="View"></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row pt-3">
                                        <div class="col-md-5"></div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnSaveHearingDtl" runat="server" CssClass="btn btn-primary btn-block" Text="Save Hearing" OnClick="btnSaveHearingDtl_Click" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <fieldset id="FiledSet_HearingDBDtl" runat="server">
                                <legend>Hearings</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdHearingDtl_DB" runat="server" CssClass="table" BorderWidth="2px" AutoGenerateColumns="false" DataKeyNames="WaNxtHearing_ID" OnRowCommand="GrdHearingDtl_DB_RowCommand" EmptyDataText="NO RECORD FOUND">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            <asp:Label ID="lblHearingID" runat="server" Text='<%# Eval("WaNxtHearing_ID") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblWACaseID" runat="server" Text='<%# Eval("ReAppealCase_ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hearing Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHearingDate" runat="server" Text='<%# Eval("NxtHearingDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hearing Detail">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHearingDetail" runat="server" Text='<%# Eval("HearingDtl") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Instruction By Court">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInstructionbycourt" runat="server" Text='<%# Eval("InstructionByCourt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hplnkHearingDocument" runat="server" Enabled='<%#  Eval("HearingDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "../Legal/WACaseHearingDoc/" + Eval("HearingDoc") %>' Target="_blank" CssClass="fa fa-eye" ToolTip="View"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnEditHearing" runat="server" CssClass="fa fa-edit" CommandName="EditHearing" CommandArgument='<%# Eval("WaNxtHearing_ID") %>' ToolTip="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-3" id="CaseDipose_Div" runat="server">
                                    <div class="form-group">
                                        <label>
                                            Case Disposal</label><span style="color: red;"><b> *</b></span><br />
                                        <asp:RadioButtonList ID="rdCaseDispose" runat="server" CssClass="rbl form-control" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdCaseDispose_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="caseDisposeYes" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Disposal Type
                                        </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvDisposeType" ValidationGroup="CaseDispose"
                                            ErrorMessage="Select Case Disposal Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDisponsType" Display="Dynamic" InitialValue="0" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDisponsType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDisponsType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="CaseDisDate_Div" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Case Disposal Date</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvCaseDisposeDate" ValidationGroup="CaseDispose"
                                            ErrorMessage="Enter Case Disposal Date." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDis_Date" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDis_Date" runat="server" data-provide="datepicker" placeholder="DD/MM/YYYY" CssClass="form-control disableFuturedate" data-date-end-date="0d" data-date-format="dd/mm/yyyy" data-date-autoclose="true" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" id="CaseDisOrderTime_Div" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>
                                            Order Implement Timeline (In Days)
                                        </label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvcaseDistimeline" ValidationGroup="CaseDispose"
                                            ErrorMessage="Enter Case Disposal Timeline." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDis_OrderTimeline" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDis_OrderTimeline" runat="server" CssClass="form-control"  onkeypress="return NumberOnly();" AutoComplete="off" MaxLength="3">
                                        </asp:TextBox>

                                    </div>
                                </div>
                                <%--Authority Name Closed Due to Confirmation From Client Side. --%>
                                <div class="col-md-4" id="Div_AuthCaseDispose" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Authority Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvAuthority" ValidationGroup="CaseDispose"
                                            ErrorMessage="Select Authority Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlAutorityList" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlAutorityList" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3" id="CaseDisDoc_Div" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>Order Document</label>
                                        <asp:FileUpload ID="FielUpcaseDisposeOrderDoc" runat="server" CssClass="form-control"></asp:FileUpload>
                                        <span style="color: red; font-size: 13px; font-weight: 600;">Document Should be 200Kb.</span>
                                    </div>
                                </div>
                                <div class="col-md-3" id="HearingDtl_CaseDispose" runat="server" visible="false" style="padding-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Button ID="btnCaseDispose" runat="server" CssClass="btn btn-primary" Text="Case Disposal" OnClick="btnCaseDispose_Click" ValidationGroup="CaseDispose" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </fieldset>
                        <%--End Here BInd Add add Hearing Dtl --%>

                        <%-- Start Here Case Dispose Detail --%>
                        <fieldset id="Fieldset_CaseDispose" runat="server">
                            <legend>Case Disposal</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GrdCaseDispose" runat="server" BorderWidth="2px" CssClass="table text-center" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Case Disposal Status" DataField="CaseDisposalStatus" />
                                            <asp:BoundField HeaderText="Case Disposal Type" DataField="CaseDisposeType" />
                                            <asp:BoundField HeaderText=" Order Implement Timeline (in Days)" DataField="CaseDsiposal_timeline" />
                                            <asp:BoundField HeaderText="Case Disposal Date" DataField="CaseDisposeDate" />
                                            <asp:TemplateField HeaderText="Order Document">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperlinkOrderDoc" runat="server" Target="_blank" Enabled='<%#  Eval("CaseDisposalDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "WACaseDispose/" + Eval("CaseDisposalDoc") %>' CssClass="fa fa-eye" ToolTip="View"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </fieldset>
                        <%-- End Here Case Dispose Detail --%>

                        <%--- Start Here For Edit Case Details ---%>
                        <fieldset id="Case_EditField" runat="server" visible="false">
                            <legend>Edit WA/RP/SLP Case Details</legend>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>WP Case No.</label>
                                        <asp:Label ID="lblRefrenceCaseNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>New Case No</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvtxtWaCaseNo" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter ReAppeal CaseNo." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtReAppealCaseNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtReAppealCaseNo" runat="server" onkeyup="javascript:capFirst(this);" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Case Type</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvCasetype" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Case Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                            <%--</div>--%>
                            <%--<div class="row">--%>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Petitioner Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvPetitionerName" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Petitioner Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtPetitionerName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtPetitionerName" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Designation Name</label>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Court</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="refvddlCourttype" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Court." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCourtType" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCourtType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Court Location</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rfvCourtlocation" ValidationGroup="Save"
                                            ErrorMessage="Select Court Location." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDistrict" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            <%--</div>--%>
                            <%--<div class="row">--%>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Type</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOfficetype" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Office Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficeType" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOfficeType" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOfficeName" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtOfficeName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtOfficeName" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" MaxLength="70" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            Case Year
                                         <span style="color: red;"><b>*</b></span>
                                            <asp:RequiredFieldValidator ID="RfvCaseYear" ValidationGroup="CaseDtl"
                                                ErrorMessage="Select Case Year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCaseYear" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator></label>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            <%--</div>--%>
                            <%--<div class="row">--%>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Name</label>                                       
                                        <asp:TextBox ID="txtNOdalOfficerName" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Mobile No.</label>                                      
                                        <asp:RegularExpressionValidator ID="RexNodalOfficerMobileNo" ValidationGroup="CaseDtl" runat="server" Display="Dynamic" ControlToValidate="txtNodalOfficerMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtNodalOfficerMobileNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Nodal Officer Email-ID</label>
                                        <asp:TextBox ID="txtNodalOfficerEmail" runat="server" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            <%--</div>--%>
                            <%--<div class="row">--%>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>OIC Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvOICNAME" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select OIC Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOicName" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtOicName" runat="server" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlOicName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOicName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" id="DivOicMobile" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>OIC Mobile No.</label>
                                        <asp:RegularExpressionValidator ID="revOICMobileNo" ValidationGroup="CaseDtl" runat="server" Display="Dynamic" ControlToValidate="txtOicMobileNO"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtOicMobileNO" runat="server" CssClass="form-control" onkeypress="return NumberOnly();" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" id="DivOicEmailID" runat="server" visible="false">
                                    <div class="form-group">
                                        <label>OIC Email-ID</label>
                                        <asp:TextBox ID="txtOICEmailID" runat="server" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- </div>
                            <div class="row">--%>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dept Advocate Name</label>
                                        <asp:TextBox ID="txtDeptAdvocateName" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" MaxLength="80" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dept Advocate Mobile No.</label>
                                        <asp:RegularExpressionValidator ID="RexAdvocateMObile" ValidationGroup="CaseDtl" runat="server" Display="Dynamic" ControlToValidate="txtDeptAdvocateMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtDeptAdvocateMobileNo" runat="server" onkeypress="return NumberOnly();" AutoComplete="off" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Dept Advocate Email-ID</label>
                                        <asp:TextBox ID="txtDeptAdvocateEmail_ID" runat="server" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <%-- </div>
                            <div class="row">--%>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Justice Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:TextBox ID="txtJusticeName" runat="server" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>High Priority Case</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select High  Priority Case." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlHighPriorityCase" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlHighPriorityCase" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Case Subject</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvCaseSubject" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseSubject" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Case Sub Subject</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvSubSubject" ValidationGroup="CaseDtl"
                                            ErrorMessage="Select Case Sub Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCase_SubjSubject" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCase_SubjSubject" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-12" style="display: none;">
                                    <div class="form-group">
                                        <label>Case Subject</label>
                                        <%-- <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvtxtCaseSubject" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseSubject" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                        <asp:TextBox ID="txtCaseSubject" runat="server" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Case Details</label><span style="color: red;"><b> *</b></span><br />
                                        <asp:RequiredFieldValidator ID="RFVActionByDistrict" ValidationGroup="CaseDtl"
                                            ErrorMessage="Enter Case Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDetail" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDetail" runat="server" TextMode="MultiLine" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" CssClass="form-control" AutoComplete="off" MaxLength="250"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5"></div>
                                <div class="col-md-3" style="padding-top: 3%;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Button ID="btnUpdateWaDtl" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="CaseDtl" Text="Save" OnClick="btnUpdateWaDtl_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>       
                        </fieldset>

                        <%---End Here For Edit Case Details ---%>
                        <%---Start Here For Add Responder Details ---%>
                        <fieldset id="Field_AddResponder" runat="server" visible="false">
                            <legend>Add Respondent</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Type</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RFVResponderType" ValidationGroup="AddResponder"
                                            ErrorMessage="Select Office Type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficetype_AddRes" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOfficetype_AddRes" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlOfficetype_AddRes_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvOfficeName_Addres" ValidationGroup="AddResponder"
                                            ErrorMessage="Select Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlofficeName_AddRes" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlofficeName_AddRes" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvDesignation_AddRes" ValidationGroup="AddResponder"
                                            ErrorMessage="Select Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDesignation_AddRes" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDesignation_AddRes" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Name</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RFVtxtAddDocumentName" ValidationGroup="AddResponder"
                                            ErrorMessage="Enter Responder Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAddResponderName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAddResponderName" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Respondent Mobile No.</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvtxtAddResponderNo" ValidationGroup="AddResponder"
                                            ErrorMessage="Enter Responder No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAddResponderMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAddResponderMobileNo" runat="server" onkeypress="return NumberOnly();" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                         <asp:RegularExpressionValidator ID="rfvMobileno_AddRes" ValidationGroup="AddResponder" runat="server" Display="Dynamic" ControlToValidate="txtAddResponderMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Department</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvtxtAddResponderDepartment" ValidationGroup="AddResponder"
                                            ErrorMessage="Enter Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAddResponderDepartment" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAddResponderDepartment" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>Address</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="RfvtxtAddResponderAddress" ValidationGroup="AddResponder"
                                            ErrorMessage="Enter Address Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAddResponderAddress" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAddResponderAddress" runat="server" CssClass="form-control" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <asp:Button ID="btnAddResponder" Text="Add" runat="server" ValidationGroup="AddResponder" CssClass="btn btn-primary btn-block" OnClick="btnAddResponder_Click" />
                                </div>
                            </div>
                        </fieldset>
                        <%---End Here For Add Responder Details ---%>
                        <%---Start Here For Add && Edit Documents ---%>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Fotter" runat="Server">
    <script type="text/javascript">
        function NumberOnly() { //only Numeric required.
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return true
            return false
        }

        function capFirst(cpt) { //only Capital First.
            cpt.value = cpt.value[0].toUpperCase() + cpt.value.substring(1);
        }

        function chcode() { // Only English or Hindi Required
            var charcd = event.keyCode;
            if (charcd > 47 && charcd < 58)
                return false
            else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true
        }
        function lettersOnly() { // Only English Letter Allow.
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>

