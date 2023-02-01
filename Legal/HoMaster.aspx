﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Legal/MainMaster.master" AutoEventWireup="true" CodeFile="HoMaster.aspx.cs" Inherits="Legal_HoMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp;
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
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
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header">
                        Ho Master
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Enter Details</legend>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Ho Name<span style="color: red;"><b>*</b></span> </label>
                                                <asp:RequiredFieldValidator ID="Rfvdate" ValidationGroup="Save"
                                                    ErrorMessage="Enter Ho Name." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required !'></i>"
                                                    ControlToValidate="txtHoName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtHoName" Placeholder="Enter Ho Name" runat="server" CssClass="form-control" autocomplete="off" onkeyup="javascript:capFirst(this);" onkeypress="return chcode();"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button runat="server" ValidationGroup="Save" CssClass="btn btn-primary btn-block" ID="btnSave" OnClick="btnSave_Click" Text="Save" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <a href="HoMaster.aspx" class="btn btn-default btn-block">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-md-6">
                                <fieldset>
                                    <legend>Details</legend>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" PageSize="50" runat="server" DataKeyNames="Ho_Id" AutoGenerateColumns="False" CssClass="table-bordered table-striped" OnRowCommand="GridView1_RowCommand"  OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="true"  EmptyDataText="NO RECORD FOUND">
                                                    <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblSno" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblHoId" runat="server" Visible="false" Text='<%#Eval("Ho_Id").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ho Name" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHoName" Text='<%# Eval("HoName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditView" runat="server" CommandArgument='<%# Eval("Ho_Id") %>' CommandName="EditDetails" ToolTip="Edit" CssClass="btn btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
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
          function chcode() { // Only English or Hindi Required
              var charcd = event.keyCode;
              if (charcd > 47 && charcd < 58)
                  return false
              else if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)
                  return true
              }
      </script>
   </asp:Content>

