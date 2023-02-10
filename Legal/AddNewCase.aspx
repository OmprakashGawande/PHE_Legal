<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="AddNewCase.aspx.cs" Inherits="Legal_AddNewCase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        label {
            font-size: 15px;
        }

        .pt-4 {
            padding-top: 2rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Save" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Responder" />
    <div class="modal fade" id="myModal01" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 70%; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Add Respondent</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Add Respondent</legend>
                            <div class="row">
                                <div runat="server" visible="false" class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Type</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rrfvRes" ValidationGroup="Responder"
                                            ErrorMessage="Select Responder type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlRespondertype" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlRespondertype" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Type Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rfvOffice" ValidationGroup="Responder"
                                            ErrorMessage="Select Office type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficetypeName" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOfficetypeName" runat="server" OnSelectedIndexChanged="ddlOfficetypeName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Responder"
                                            ErrorMessage="Select Office Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlOfficeName" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            <%--</div>--%>
                            <%--<div class="row">--%>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Designation</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rfvDesignation" ValidationGroup="Responder"
                                            ErrorMessage="Select Designation." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlDesignation" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Name</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rfvname" ValidationGroup="Responder"
                                            ErrorMessage="Enter Responder Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtResponderName" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtResponderName" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Respondent Mobile No.</label>
                                        <span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvmobileno" ValidationGroup="Responder"
                                            ErrorMessage="Enter Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtMobileNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtMobileNo" onkeypress="return NumberOnly();" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RexNodalOfficerMobileNo" ValidationGroup="Responder" runat="server" Display="Dynamic" ControlToValidate="txtMobileNo"
                                            ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Department</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rfvDeptname" ValidationGroup="Responder"
                                            ErrorMessage="Enter Department Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtDepartment" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address</label><span style="color: red;"><b> *</b></span>
                                        <asp:RequiredFieldValidator ID="rfvAdd" ValidationGroup="Responder"
                                            ErrorMessage="Enter Address." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtAddress" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="70"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Add" ID="btnYes" ValidationGroup="Responder" Style="margin-top: 20px; width: 80px;" OnClick="btnYes_Click" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="card">
                    <div class="card-header">
                        <div class="float-left">
                            Add New Case
                        </div>
                        <div class="float-right">
                            <asp:Button ID="btnAddresponder" runat="server" CssClass="btn-sm label label-warning" Text="Add Responder" OnClick="btnAddresponder_Click" />
                        </div>

                    </div>
                    <div class="card-body">
                        <fieldset>
                            <legend>Case Detail</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Case No.</label><span style="color: red;">*</span>
                                        <asp:RequiredFieldValidator ID="RfvCaseno" ValidationGroup="Save"
                                            ErrorMessage="Enter Case No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseNo" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseNo" runat="server" placeholder="Case No." class="form-control" AutoComplete="off" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>

                                    </div>
                                </div>

                                <div runat="server" visible="false" class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Old Case No.</label>
                                            <asp:TextBox ID="txtCaseOldRefNo" runat="server" placeholder="Old Case No." AutoComplete="off" class="form-control" MaxLength="50" onkeypress="return validatenum(event);"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Case Type</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvCasetype" ValidationGroup="Save"
                                            ErrorMessage="Select Case type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCasetype" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCasetype" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Case Year</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvCaseyear" ValidationGroup="Save"
                                            ErrorMessage="Select Case year." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlCaseYear" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCaseYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <%--</div>--%>
                                <%--<div class="row">--%>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Court Type<span style="color: red;">*</span></label>
                                            <asp:RequiredFieldValidator ID="RfvCourttype" ValidationGroup="Save"
                                                ErrorMessage="Select Court type." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCourtType" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCourtType" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
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
                                <%--  <div class="col-md-3" id="DistrictCourtSelect" runat="server" visible="false">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                District<span style="color: red;">*</span></label>
                                            <asp:RequiredFieldValidator ID="RfvCourtOfDistrict" ValidationGroup="Save"
                                                ErrorMessage="Select District Of Court." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlDistrictCourt" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlDistrictCourt" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlCourtType_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Case Subject<span style="color: red;">*</span></label>
                                            <asp:RequiredFieldValidator ID="rfvCaseSubject" ValidationGroup="Save"
                                                ErrorMessage="Select Case Subject." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="ddlCaseSubject" Display="Dynamic" runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCaseSubject" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlCaseSubject_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sub Subject<span style="color: red"><b>*</b></span></label>
                                        <asp:DropDownList runat="server" ID="ddlSubSubject" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>
                                                Case Registration Date</label>
                                            <%-- <asp:RequiredFieldValidator ID="rfvCaseRegisDate" ValidationGroup="Save"
                                                ErrorMessage="Enter Date Of Case Registration." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                ControlToValidate="txtDateOfCaseReg" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                            <asp:TextBox ID="txtDateOfCaseReg" date-provide="datepicker" runat="server" data-date-end-date="0d" AutoComplete="off" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            Last Hearing Date<span style="color: red"><b>*</b></span></label>
                                        <asp:RequiredFieldValidator ID="rfvLastHearingdate" ValidationGroup="Save"
                                            ErrorMessage="Enter Date Of Last Hearing." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtDateOfLastHearing" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtDateOfLastHearing" runat="server" date-provide="datepicker" AutoComplete="off" data-date-end-date="0d" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>
                                            High Priority Case</label><span style="color: red;"><b>*</b></span>
                                        <asp:RequiredFieldValidator ID="rfvHighpriortiy" ValidationGroup="Save"
                                            ErrorMessage="Select High Priority Case." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="ddlHighprioritycase" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlHighprioritycase" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>
                                            Case Detail<span style="color: red;">*</span></label>
                                        <asp:RequiredFieldValidator ID="rfvCasedetail" ValidationGroup="Save"
                                            ErrorMessage="Enter Case Detail." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                            ControlToValidate="txtCaseDetail" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtCaseDetail" runat="server" class="form-control" onkeyup="javascript:capFirst(this);" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Appointment of Officer Incharge</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        OIC
                                                        <span style="color: red;"><b>*</b></span></label>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvOICName" ForeColor="Red" Display="Dynamic"
                                                        ControlToValidate="ddlOicName" ValidationGroup="Save" ErrorMessage="Select OIC Name."
                                                        Text="<i class='fa fa-exclamation-circle' title='Required !'></i>" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:DropDownList runat="server" ID="ddlOicName" CssClass="form-control select2"
                                                        OnSelectedIndexChanged="ddlOicName_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <%-- <asp:TextBox ID="txtOICName" runat="server"  CssClass="form-control" onkeyup="javascript:capFirst(this);"  onkeypress="return lettersOnly();" MaxLength="70" AutoComplete="off"></asp:TextBox>--%>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.</label>
                                                    <%-- <asp:RequiredFieldValidator ID="rfvoicMobile" ValidationGroup="Save"
                                                        ErrorMessage="Enter OIC Mobile No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                        ControlToValidate="txtOICMobileNo" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtOICMobileNo"
                                                        ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                        ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>--%>
                                                    <asp:TextBox ID="txtOICMobileNo" runat="server" ReadOnly="true" CssClass="form-control" MaxLength="10" AutoComplete="off" onkeypress="return NumberOnly();"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Email-ID.</label>
                                                    <asp:TextBox runat="server" ID="txtEmailID" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Appointment of Advocate</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Advocate Name</label>
                                                    <asp:TextBox ID="txtDeptAdvocateName" runat="server" AutoComplete="off" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" CssClass="form-control" MaxLength="70"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.</label>
                                                    <asp:TextBox ID="txtDeptAdvocateMobileNo" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="10" onkeypress="return NumberOnly();"></asp:TextBox>
                                                    <asp:RegularExpressionValidator runat="server" ID="revAppointmentMobileNo" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                        ControlToValidate="txtDeptAdvocateMobileNo" ValidationExpression="^([6-9]{1}[0-9]{9})$" ValidationGroup="Save" ErrorMessage="Invalid Mobile No.">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Petitioner / Applicant Details</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Name<span style="color: red"><b>*</b></span></label>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvPetitionerName" Display="Dynamic" ForeColor="Red"
                                                        SetFocusOnError="true" ControlToValidate="txtPetitionerAppName" ValidationGroup="Save" ErrorMessage="Enter Name"
                                                        Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtPetitionerAppName" runat="server" placeholder="Name" AutoComplete="off" onkeyup="javascript:capFirst(this);" CssClass="form-control" MaxLength="70" onkeypress="return validatename(event);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.<span style="color:red"><b>*</b></span></label>
                                                    <asp:RequiredFieldValidator runat="server" ID="rfvMoNo" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                        ControlToValidate="txtPetitionerAppMobileNo" ValidationGroup="Save" ErrorMessage="Enter Mobile No."
                                                        Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:TextBox ID="txtPetitionerAppMobileNo" runat="server" placeholder="Mobile No" AutoComplete="off" CssClass="form-control MobileNo1" MaxLength="10" onkeypress="return NumberOnly();"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="rfvPetiadvocatemobile" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtPetitionerAppMobileNo"
                                                        ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                        ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Petitioner Advocate Details</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Name</label>
                                                    <asp:TextBox ID="txtPetitionerAdvName" runat="server" placeholder="Name" onkeyup="javascript:capFirst(this);" onkeypress="return lettersOnly();" CssClass="form-control" MaxLength="50" AutoComplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>
                                                        Mobile No.</label>
                                                   
                                                    <asp:TextBox ID="txtPetitionerAdvMobileNo" runat="server" placeholder="Mobile" AutoComplete="off" CssClass="form-control MobileNo" onkeypress="return NumberOnly();" MaxLength="10"></asp:TextBox>
                                                     <asp:RegularExpressionValidator ID="rfv" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtPetitionerAdvMobileNo"
                                                        ErrorMessage="Invalid Mobile No." SetFocusOnError="true"
                                                        ForeColor="Red" ValidationExpression="^([6-9]{1}[0-9]{9})$"></asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="box-body">
                                    <fieldset>
                                        <legend>Document Upload</legend>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Document Name</label>
                                                            <asp:RequiredFieldValidator runat="server" ID="rfvDocumentName" ForeColor="Red" Display="Dynamic"
                                                                ControlToValidate="txtDocName" ValidationGroup="Add" ErrorMessage="Enter Document Name." Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:TextBox ID="txtDocName" placeholder="Enter Document Name" onkeyup="javascript:capFirst(this);" runat="server" MaxLength="50" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label>Documents</label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink4" Visible="false" CssClass="label label-default" runat="server" Text="View"></asp:HyperLink>
                                                                <asp:RequiredFieldValidator runat="server" ID="rfvDocument" ForeColor="Red" Display="Dynamic"
                                                                ControlToValidate="FileUpload10" ValidationGroup="Add" ErrorMessage="Select Document" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:FileUpload ID="FileUpload10" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnAddDoc" runat="server" Text="Add" ValidationGroup="Add" CssClass="btn btn-primary btn-block" OnClick="btnAddDoc_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <fieldset>
                                                    <legend>View Doc</legend>

                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="">
                                                                <asp:GridView ID="GrdViewDoc" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SNo.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSrnO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Document Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDocName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="View">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="lblDocName" runat="server" NavigateUrl='<%# "../Legal/AddNewCaseCourtDoc/" +  Eval("Document") %>' Target="_blank" CssClass="btn-sm label label-primary">View</asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <fieldset>
                                        <legend>Respondent Detail</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table table-responsive">
                                                    <asp:GridView ID="GrdRespondent" runat="server" AutoGenerateColumns="false" EmptyDataText="NO RECORD FOUND" CssClass="table table-bordered">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SNo.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSrnO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField HeaderText="Respondent Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRepondenttypeID" runat="server" Text='<%# Eval("RespondenttypeID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblrespondertypename" runat="server" Text='<%# Eval("RespondenttypeName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Office Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficetypeID" runat="server" Text='<%# Eval("OfficeTypeID") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblofficetypename" runat="server" Text='<%# Eval("OfficeTypeName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Office Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOfficeNameID" runat="server" Text='<%# Eval("OfficeNameId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblOfficename" runat="server" Text='<%# Eval("OfficeName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDesignationID" runat="server" Text='<%# Eval("DesignationId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblDesignationname" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Respondent Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrespondentName" runat="server" Text='<%# Eval("RespondentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Mobile No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("RespondentMobileNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <fieldset>
                                        <legend>Hearing Detail</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Next Hearing Date</label>
                                                    <asp:TextBox ID="txtHearingDate" runat="server" date-provide="datepicker" placeholder="DD/MM/YYYY" class="form-control" ClientIDMode="Static" autocomplete="off" onchange="checkHearingDetail();"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Hearing Document</label>
                                                    <asp:FileUpload ID="FileHearingDoc" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="Save" runat="server" Text="Save" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnClear" CssClass="btn btn-block btn-default" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                            </div>

                        </div>
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
    <script type="text/javascript">
        $('#txtDateOfCaseReg').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $('#txtDateOfLastHearing').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });

        $('#txtHearingDate').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });
        function OICDetailModal() {
            $("#OICDetailModal").modal('show');
        }
        function AdvocateDetailModal() {
            $("#AdvocateDetailModal").modal('show');
        }
        function AddRespondent() {
            $("#myModal01").modal('show');
        }
        function myModal() {
            $("#myModal").modal('show');
        }
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }


        function checkHearingDetail() {
            debugger;

            var x = document.getElementById("txtDateOfReceipt").value; //This is a STRING, not a Date
            if (x != "") {
                var dateParts = x.split("/");   //Will split in 3 parts: day, month and year
                var xday = dateParts[0];
                var xmonth = dateParts[1];
                var xyear = dateParts[2];
                var xd = new Date(xyear, parseInt(xmonth, 10) - 1, xday);
            }
            else {
                var xd = "";
            }

            var y = document.getElementById("txtHearingDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtDateOfFiling").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];
                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }

            if (xd != "" && zd != "" && yd != "" || xd != "" && yd != "" || zd != " " && yd != " ") {
                if (xd >= yd && zd >= yd) {
                    alert("Hearing Date should be greater than Date Of Receipt and Date Of Receipt ");
                    document.getElementById("txtDateOfReceipt").value = "";
                    document.getElementById("txtDateOfFiling").value = "";
                }
                else if (xd >= yd) {
                    alert("Hearing Date should be greater than Date Of Receipt");
                    document.getElementById("txtDateOfReceipt").value = "";
                }
                else if (zd >= yd) {
                    alert("Hearing Date should be greater than Date Of Receipt ");
                    document.getElementById("txtDateOfFiling").value = "";
                }
            }
        }
    </script>
    <script src="../js/ValidationJs.js"></script>
</asp:Content>

