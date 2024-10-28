<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeApplication.aspx.cs" Inherits="WebApplication.EmployeeApplication" %>  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  
<html xmlns="http://www.w3.org/1999/xhtml">  
<head runat="server">  
    <title></title>  
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
        .auto-style2 {
            width: 278px;
        }
    </style>
</head>  
<body>  
    <form id="form1" runat="server">  
        <div>  
            <table>  
                <tr>  
                    <td class="auto-style2">  
                        <asp:Label ID="lblName" runat="server" Text="Enter Name"></asp:Label>  
                    </td>  
                    <td>  
                        <asp:TextBox ID="txtName" runat="server" CssClass="auto-style1" Height="16px"></asp:TextBox>  
                    </td>  
                </tr>  
                <tr>  
                    <td class="auto-style2">  
                        <asp:Label ID="lblAge" runat="server" Text="Enter Age"></asp:Label>  
                    </td>  
                    <td>  
                        <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>  
                    </td>  
                </tr>  
                <tr>  
                    <td class="auto-style2">  
                        <asp:Label ID="lblPhoneNumber" runat="server" Text="Enter Phone Number"></asp:Label>  
                    </td>  
                    <td>  
                        <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>  
                    </td>  
                </tr>  
                <tr>  
                    <td class="auto-style2">  
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </td>  
                </tr>  
                <tr>  
                    <td class="auto-style2">  
                        <asp:GridView ID="gvDisplay" runat="server" AutoGenerateColumns="false"  
                            AllowPaging="false" PageSize="500" 
                            OnRowCommand="gvDisplay_RowCommand" OnRowDeleting="gvDisplay_RowDeleting"  
                            OnSelectedIndexChanging="gvDisplay_SelectedIndexChanging"  
                            OnPageIndexChanging="gvDisplay_PageIndexChanging"
                            Visible="true" DataKeyNames="Id">  
                            <Columns>  
                                <asp:BoundField DataField="Name" HeaderText="Name" />

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkName" runat="server" 
                                            CommandName="ShowAddress" 
                                            CommandArgument='<%# Eval("Id") %>' 
                                            Text='<%# Eval("Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField> 

                                <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" />  
                                <asp:BoundField DataField="Age" HeaderText="Age" />  

                                <asp:CommandField ButtonType="Button" SelectText="Edit" ShowSelectButton="True" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" 
                                            CommandName="Delete" 
                                            CommandArgument='<%# Eval("Id") %>'
                                            OnClientClick='<%# "return confirm(\"Are you sure you want to delete the record for " + Eval("Name") + "?\");" %>' 
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>  
                        </asp:GridView>  
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                    </td>  
                </tr>  

                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtLine1" runat="server" Visible="false" placeholder="Line 1* (Mandatory)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtLine2" runat="server" Visible="false" placeholder="Line 2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtLine3" runat="server" Visible="false" placeholder="Line 3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtCity" runat="server" Visible="false" placeholder="City"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPinCode" runat="server" Visible="false" placeholder="Pin Code"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:HiddenField ID="hfSelectedId" runat="server" />
                        <asp:Button ID="btnSaveAddress" runat="server" Text="Save Address" OnClick="btnSaveAddress_Click" Visible="false" />
                        <asp:Button ID="btnClearAddress" runat="server" Text="Clear Address" OnClick="btnClearAddress_Click" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false" />
                    </td>
                </tr>

                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblMessage2" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                        <br />
                        <asp:Label ID="lblEmployeeName" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                        <asp:GridView ID="gvAppEmployeeTable" runat="server" AutoGenerateColumns="false" 
                            DataKeyNames="EmpAddressId"
                            OnRowCommand="gvAppEmployeeTable_RowCommand"
                            OnSelectedIndexChanging="gvAppEmployeeTable_SelectedIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Line1" HeaderText="Line 1" />
                                <asp:BoundField DataField="Line2" HeaderText="Line 2" />
                                <asp:BoundField DataField="Line3" HeaderText="Line 3" />
                                <asp:BoundField DataField="City" HeaderText="City" />
                                <asp:BoundField DataField="PinCode" HeaderText="Pin Code" />
                                <asp:CommandField ButtonType="Button" SelectText="Edit" ShowSelectButton="True" />
                                 <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelete" runat="server" 
                                            CommandName="DeleteAddress" 
                                            CommandArgument='<%# Eval("EmpAddressId") %>' 
                                            Text="Delete" 
                                            OnClientClick="return confirm('Are you sure you want to delete this address?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hfSelectedAddressId" runat="server" />
                    </td>
                </tr>
            </table>  
        </div>  
    </form>  
</body>  
</html>