using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.EmployeeSchema;
using WebApplication.EmployeeBAL;
using WebApplication.EmployeeAddressSchema;
using WebApplication.EmployeeAddressBAL;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplication
{
	public partial class EmployeeApplication : System.Web.UI.Page
	{
		DataTable dt;

		protected void Page_Load(object sender, EventArgs e)
		{
			lblMessage.Visible = false;
			lblMessage2.Visible = false;
			if (!Page.IsPostBack)
			{
				BindGrid();
			}
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			lblMessage2.Visible = false;
			try
			{
				if (btnSubmit.Text == "Submit")
				{
					InsertData();
				}
				else if (btnSubmit.Text == "Update")
				{
					if (gvDisplay.SelectedIndex != -1)
					{
						int Id = Convert.ToInt32(gvDisplay.DataKeys[gvDisplay.SelectedIndex].Value);

						UpdateData(Id);
					}
					else
					{
						lblMessage.Visible = true;
						lblMessage.Text = "Please select a record to update.";
						lblMessage.ForeColor = System.Drawing.Color.Red;
					}
				}
			}
			catch (Exception ex)
			{
				lblMessage.Visible = true;
				lblMessage.Text = "An error occurred: " + ex.Message;
				lblMessage.ForeColor = System.Drawing.Color.Red;
			}
		}

		protected void btnClear_Click(object sender, EventArgs e)
        {
			Clear();
			//btnSubmit.Text = "Submit";
        }

		public void UpdateData(int Id)
		{
			lblMessage2.Visible = false;
			EmployeeSchema.Class1 objSchema = new EmployeeSchema.Class1();
			objSchema.Name = txtName.Text;
			objSchema.PhoneNumber = txtPhoneNumber.Text;


			if (!int.TryParse(txtAge.Text.Trim(), out int age))
			{
				lblMessage.Visible = true;
				lblMessage.Text = "Please enter a valid age (integer value).";
				lblMessage.ForeColor = System.Drawing.Color.Red;
				lblMessage.Visible = true;
				return;
			}

			objSchema.Age = age;

			EmployeeBAL.Class1 objBAL = new EmployeeBAL.Class1();
			int result = objBAL.Update(objSchema, Id);
			if (result > 0)
			{
				lblMessage.Visible = true;
				lblMessage.Text = "Data Updated Successfully!";
				lblMessage.ForeColor = System.Drawing.Color.Green;
				lblMessage.Visible = true;
			}
			btnSubmit.Text = "Submit";
			BindGrid();
			Clear();
		}

		public void InsertData()
		{
			lblMessage2.Visible = false;
			lblMessage.Visible = true;
			EmployeeSchema.Class1 objSchema = new EmployeeSchema.Class1();
			objSchema.Name = txtName.Text;
			objSchema.PhoneNumber = txtPhoneNumber.Text;
			objSchema.Age = Convert.ToInt32(txtAge.Text);
			EmployeeBAL.Class1 objBAL = new EmployeeBAL.Class1();
			int result = objBAL.Insert(objSchema);
			if (result > 0)
			{
				lblMessage.Visible = true;
				lblMessage.Text = "Data Saved Successfully!";
				lblMessage.ForeColor = System.Drawing.Color.Green;
				lblMessage.Visible = true;
			}
			BindGrid();
			Clear();
		}

		private void Clear()
		{
			lblMessage2.Visible = false;
			txtName.Text = "";
			txtAge.Text = "";
			txtPhoneNumber.Text = "";
		}

		private void BindGrid()
		{
			lblMessage2.Visible = false;
			try
			{
				EmployeeBAL.Class1 objBal = new EmployeeBAL.Class1();
				gvDisplay.Columns[0].Visible = true;
				gvDisplay.DataSource = objBal.BindGrid();
				gvDisplay.DataBind();
				gvDisplay.Columns[0].Visible = false;
				gvDisplay.Visible = true;
			}
			catch (Exception ex)
			{
				lblMessage.Visible = true;
				lblMessage.Text = "An error occurred while binding the grid: " + ex.Message;
				lblMessage.ForeColor = System.Drawing.Color.Red;
				lblMessage.Visible = true;
			}
		}

		protected void gvDisplay_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
		{
			lblMessage.Visible = false;
			lblMessage2.Visible = false;

			Clear();
			ClearAddressFields();
			btnSubmit.Text = "Submit";
			
			try
			{
				EmployeeBAL.Class1 objBAL = new EmployeeBAL.Class1();

				if (gvDisplay.DataKeys[e.NewSelectedIndex] != null)
				{
					int Id = Convert.ToInt32(gvDisplay.DataKeys[e.NewSelectedIndex].Value);

					dt = objBAL.GetById(Id);

					if (dt.Rows.Count > 0)
					{
						txtName.Text = dt.Rows[0]["Name"].ToString();
						txtPhoneNumber.Text = dt.Rows[0]["PhoneNumber"].ToString();
						txtAge.Text = dt.Rows[0]["Age"].ToString();

						btnSubmit.Text = "Update";
						ClearAddressFields();
						BindGrid();
					}
				}
				else
				{
					lblMessage.Visible = true;
					lblMessage.Text = "ID not found in DataKeys.";
					lblMessage.ForeColor = System.Drawing.Color.Red;
				}
			}
			catch (Exception ex)
			{
				lblMessage.Visible = true;
				lblMessage.Text = "An error occurred: " + ex.Message;
				lblMessage.ForeColor = System.Drawing.Color.Red;
				lblMessage.Visible = true;
			}
		}

		protected void gvDisplay_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			lblMessage.Visible = false;
			if (e.CommandName == "Delete")
			{
				int Id = Convert.ToInt32(e.CommandArgument);
				DeleteRecord(Id);
				LoadAppEmployeeTable(Id);
			}

			else if (e.CommandName.Equals("ShowAddress"))
			{
				Clear();
				ClearAddressFields();

				btnSubmit.Text = "Submit";

				int empId = Convert.ToInt32(e.CommandArgument);
				ShowAddressForm(empId);
				LoadAppEmployeeTable(empId);
			}
		}

		private void LoadAddressFields(int userId)
		{
			lblMessage.Visible = false;
			EmployeeAddressBAL.EmployeeAddressLogic objBAL = new EmployeeAddressBAL.EmployeeAddressLogic();
			var address = objBAL.GetEmployeeAddress(userId);

			if (address != null)
			{
				txtLine1.Text = address.Line1;
				txtLine2.Text = address.Line2;
				txtLine3.Text = address.Line3;
				txtCity.Text = address.City;
				txtPinCode.Text = address.PinCode;
			}
			else
			{
				txtLine1.Text = "";
				txtLine2.Text = "";
				txtLine3.Text = "";
				txtCity.Text = "";
				txtPinCode.Text = "";
			}
		}

		private void LoadAppEmployeeTable(int userId)
		{
			//lblMessage.Visible = false;
			EmployeeAddressBAL.EmployeeAddressLogic objBAL = new EmployeeAddressBAL.EmployeeAddressLogic();
			var addressList = objBAL.GetAddressDetails(userId);

			EmployeeAddressBAL.EmployeeAddressLogic myObj = new EmployeeAddressBAL.EmployeeAddressLogic();
			String userName = myObj.GetUserNameById(userId);
			//String userName = "";
			/* if (dt.Rows.Count > 0)
			{
				userName = dt.Rows[0]["Name"].ToString();
			} */
			lblEmployeeName.Visible = true;
			lblEmployeeName.Text = "Employee Name: " + userName;

			if (addressList != null && addressList.Count > 0)
			{

				gvAppEmployeeTable.DataSource = addressList;
				gvAppEmployeeTable.DataBind();
				lblMessage2.Text = "Relative data found";
				lblMessage2.ForeColor = System.Drawing.Color.Green;
				lblMessage2.Visible = true;
			}

			else
			{
				gvAppEmployeeTable.DataSource = "";
				gvAppEmployeeTable.DataBind();
				lblMessage2.Text = "No relative data found";
				lblMessage2.ForeColor = System.Drawing.Color.Red;
				lblMessage2.Visible = true;
			}
		}

		private void ShowAddressForm(int Id)
		{
			lblMessage.Visible = false;
			txtLine1.Visible = true;
			txtLine2.Visible = true;
			txtLine3.Visible = true;
			txtCity.Visible = true;
			txtPinCode.Visible = true;

			ViewState["SelectedEmpId"] = Id;

			ClearAddressFields();
			BindGrid();
		}

		private void ClearAddressFields()
		{
			lblMessage.Visible = true;
			txtLine1.Text = "";
			txtLine2.Text = "";
			txtLine3.Text = "";
			txtCity.Text = "";
			txtPinCode.Text = "";
			txtLine1.Visible = true;
			txtLine2.Visible = true;
			txtLine3.Visible = true;
			txtCity.Visible = true;
			txtPinCode.Visible = true;
			btnSaveAddress.Visible = true;
			btnClearAddress.Visible = true;
			btnCancel.Visible = true;
			btnSaveAddress.Text = "Save Address";
		}

		private void DeleteRecord(int Id)
		{
			try
			{
				EmployeeBAL.Class1 objBAL = new EmployeeBAL.Class1();
				int Result = objBAL.Delete(Id);
				if (Result == 0)
                {
					lblMessage.Text = "Can't delete parent, with a child!";
					lblMessage.ForeColor = System.Drawing.Color.Red;
					lblMessage.Visible = true;
				}
				else if (Result > 0)
				{
					lblMessage.Text = "Data Deleted Successfully!";
					lblMessage.ForeColor = System.Drawing.Color.Green;
					lblMessage.Visible = true;
				}
				LoadAppEmployeeTable(Id);
				BindGrid();
				Clear();
			}
			catch (Exception ex)
			{
				lblMessage.Text = "An error occurred: " + ex.Message;
				lblMessage.ForeColor = System.Drawing.Color.Red;
				lblMessage.Visible = true;
			}
		}

		protected void gvDisplay_RowDeleting(object sender, GridViewDeleteEventArgs e) { }

		protected void gvDisplay_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			lblMessage.Visible = false;
			ClearAddressFields();
			gvDisplay.PageIndex = e.NewPageIndex;
			BindGrid();
		}

		private void DeleteAddress(int addressId)
		{
			lblMessage.Visible = false;
			EmployeeAddressLogic objBAL = new EmployeeAddressBAL.EmployeeAddressLogic();
			int result = objBAL.DeleteAddress(addressId);

			if (result > 0)
			{
				lblMessage2.Text = "Address deleted successfully!";
				lblMessage2.ForeColor = System.Drawing.Color.Green;
			}
			else
			{
				lblMessage2.Text = "Error deleting address.";
				lblMessage2.ForeColor = System.Drawing.Color.Red;
			}

			lblMessage2.Visible = true;
		}

		private void BindAppEmployeeTable(int addressId)
		{
			lblMessage.Visible = false;
			EmployeeAddressBAL.EmployeeAddressLogic objBAL = new EmployeeAddressBAL.EmployeeAddressLogic();
			DataTable dt = objBAL.GetAllAddresses(addressId);
			gvAppEmployeeTable.DataSource = dt;
			gvAppEmployeeTable.DataBind();
		}

		protected void gvAppEmployeeTable_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
		{
			lblMessage.Visible = false;
			lblMessage2.Visible = false;


			btnSaveAddress.Text = "Save Address";
			Clear();
			ClearAddressFields();

			try
			{
				// Create instance of the business logic class
				EmployeeAddressBAL.EmployeeAddressLogic obj = new EmployeeAddressBAL.EmployeeAddressLogic();

				// Get the selected EmpAddressId from DataKeys using the new selected index
				int empAddressId = Convert.ToInt32(gvAppEmployeeTable.DataKeys[e.NewSelectedIndex].Value);
				hfSelectedAddressId.Value = empAddressId.ToString();

				// Fetch the address details by the selected EmpAddressId
				List<EmployeeAddress> addresses = obj.GetAddressesById(empAddressId);


				if (addresses.Count > 0)
				{
					// Since we're editing, we only need the first result
					EmployeeAddress address = addresses[0];

					// Fill the textboxes with the retrieved data
					txtLine1.Text = address.Line1;
					txtLine2.Text = address.Line2;
					txtLine3.Text = address.Line3;
					txtCity.Text = address.City;
					txtPinCode.Text = address.PinCode;

					// Change the button text to indicate it's an update
					btnSaveAddress.Text = "Update";

					// Optionally, bind the updated table again if needed
					//BindAppEmployeeTable(empAddressId);
				}
			}
			catch (Exception ex)
			{
				// Log or show the exception message
				lblMessage2.Text = "An error occurred: " + ex.Message;
				lblMessage2.Visible = true;
			}
		}

		protected void btnSaveAddress_Click(object sender, EventArgs e)
		{
			lblMessage.Visible = false;
			try
			{
				if (btnSaveAddress.Text == "Save Address")
				{
					if (string.IsNullOrWhiteSpace(txtLine1.Text))
					{
						lblMessage.Visible = true;
						lblMessage.Text = "Please fill all the required address fields.";
						lblMessage.ForeColor = System.Drawing.Color.Red;
						lblMessage.Visible = true;
						return;
					}

					try
					{
						/*int EmpAddressId= Convert.ToInt32(hfSelectedAddressId.Value);*/
						// Create a new EmployeeAddress object to hold the updated values
						/*int EmpAddressId = Convert.ToInt32(hfSelectedAddressId.Value);*/
						/* EmployeeAddress address = new EmployeeAddress
						{
							EmpAddressId = Convert.ToInt32(hfSelectedAddressId.Value),  // Use hidden field or other mechanism to track EmpAddressId
							Line1 = txtLine1.Text,
							Line2 = txtLine2.Text,
							Line3 = txtLine3.Text,
							City = txtCity.Text,
							PinCode = txtPinCode.Text
						}; */
						int selectedId = (int)ViewState["SelectedEmpId"];
						EmployeeAddressSchema.EmployeeAddress address = new EmployeeAddressSchema.EmployeeAddress();
						address.Id = selectedId;
						address.Line1 = txtLine1.Text;
						address.Line2 = txtLine2.Text;
						address.Line3 = txtLine3.Text;
						address.City = txtCity.Text;
						address.PinCode = txtPinCode.Text;

						// Call the update method in the business logic layer
						EmployeeAddressBAL.EmployeeAddressLogic bal = new EmployeeAddressBAL.EmployeeAddressLogic();
						/*bal.UpdateAddress(address, address.EmpAddressId);*/
						int result = bal.InsertAddress(address);

						if (result > 0)
						{
							lblMessage.Visible = true;
							lblMessage.Text = "Data Entered Successfully!";
							lblMessage.ForeColor = System.Drawing.Color.Green;
							//lblMessage.Visible = true;
						}

						else
						{
							lblMessage.Visible = true;
							lblMessage.Text = "Error entering address";
							//lblMessage.Visible = true;
						}

						// Show success message and rebind the table
						lblMessage.Text = "Address added successfully!";
						lblMessage.Visible = true;
						/*BindAppEmployeeTable(EmpAddressId);  // Refresh GridView  */
						LoadAppEmployeeTable(selectedId);
						ClearAddressFields();
					}
					catch (Exception ex)
					{
						lblMessage.Text = "Error updating address: " + ex.Message;
						lblMessage.Visible = true;
					}
				}

				else if (btnSaveAddress.Text == "Update")
				{
					lblMessage.Visible = true;
					if (gvAppEmployeeTable.SelectedIndex != -1)
					{
						int EmpAddressId = Convert.ToInt32(hfSelectedAddressId.Value);
						int selectedId = (int)ViewState["SelectedEmpId"];

						try
						{
							lblMessage2.Visible = false;
							EmployeeAddressSchema.EmployeeAddress objSchema = new EmployeeAddressSchema.EmployeeAddress();

							objSchema.Line1 = txtLine1.Text;
							objSchema.Line2 = txtLine2.Text;
							objSchema.Line3 = txtLine3.Text;
							objSchema.City = txtCity.Text;
							objSchema.PinCode = txtPinCode.Text;

							if (txtLine1.Text == "" || txtLine2.Text == "" || txtLine3.Text == "" || txtCity.Text == "" || txtPinCode.Text == "")
							{
								lblMessage.Visible = true;
								lblMessage.Visible = true;
								lblMessage.Text = "All Data Fields are compulsory!";
								lblMessage.ForeColor = System.Drawing.Color.Red;
							}

							else
							{
								EmployeeAddressBAL.EmployeeAddressLogic objBAL = new EmployeeAddressBAL.EmployeeAddressLogic();
								int result = objBAL.UpdateAddress(objSchema, EmpAddressId);

								if (result > 0)
								{
									lblMessage.Visible = true;
									lblMessage.Visible = true;
									lblMessage.Text = "Data Updated Successfully!";
									lblMessage.ForeColor = System.Drawing.Color.Green;
									//lblMessage.Visible = true;
								}

								else
								{
									lblMessage.Visible = true;
									lblMessage.Text = "Error updating address";
									//lblMessage.Visible = true;
								}
							}
						}

						catch (Exception ex)
						{
							lblMessage.Visible = true;
							lblMessage.Text = "Error: " + ex.Message;
							//lblMessage.Visible = true;
						}

						//BindAppEmployeeTable(selectedId);
						LoadAppEmployeeTable(selectedId);
						btnSaveAddress.Text = "Save Address";
						ClearAddressFields();
					}

					else
					{
						lblMessage.Visible = true;
						lblMessage.Text = "Please select a record to update.";
						lblMessage.ForeColor = System.Drawing.Color.Red;
					}
				}
			}

			catch (Exception ex)
			{
				lblMessage2.Visible = true;
				lblMessage2.Text = "An error occurred: " + ex.Message;
				lblMessage2.ForeColor = System.Drawing.Color.Red;
			}
		}

		protected void btnClearAddress_Click(object sender, EventArgs e)
        {
			ClearAddressFieldsExc();
		}

		protected void btnCancel_Click(object sender, EventArgs e)
        {
			ClearAddressFieldsExc();
			btnSaveAddress.Text = "Save Address";
		}

		private void ClearAddressFieldsExc()
        {
			lblMessage.Visible = false;
			lblMessage2.Visible = false;
			txtLine1.Text = "";
			txtLine2.Text = "";
			txtLine3.Text = "";
			txtCity.Text = "";
			txtPinCode.Text = "";
			btnCancel.Visible = true;
		}

		/*protected void UpdateAddressData(int EmpAddressId)
		{
			try
			{
				lblMessage.Visible = false;
				EmployeeAddressSchema.EmployeeAddress objSchema = new EmployeeAddressSchema.EmployeeAddress();
				objSchema.Line1 = txtLine1.Text;
				objSchema.Line2 = txtLine2.Text;
				objSchema.Line3 = txtLine3.Text;
				objSchema.City = txtCity.Text;
				objSchema.PinCode = txtPinCode.Text;

				EmployeeAddressBAL.EmployeeAddressLogic objBAL = new EmployeeAddressBAL.EmployeeAddressLogic();
				int result = objBAL.UpdateAddress(objSchema, EmpAddressId);

				if (result > 0)
				{
					lblMessage.Visible = true;
					lblMessage.Text = "Data Updated Successfully!";
					lblMessage.ForeColor = System.Drawing.Color.Green;
					lblMessage.Visible = true;
				}

				else
				{
					lblMessage.Text = "Error updating address";
					lblMessage.Visible = true;
				}
			}

			catch (Exception ex)
			{
				lblMessage.Text = "Error: " + ex.Message;
				lblMessage.Visible = true;
			}
			btnSaveAddress.Text = "Save Address";
			BindAppEmployeeTable(EmpAddressId);

		}*/

		protected void gvAppEmployeeTable_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			Clear();
			ClearAddressFields();

			lblMessage.Visible = false;
			if (e.CommandName == "DeleteAddress")
			{
				int EmpAddressId = Convert.ToInt32(e.CommandArgument);

				DeleteAddress(EmpAddressId);

				int selectedId = (int)ViewState["SelectedEmpId"];

				LoadAppEmployeeTable(selectedId);

				//LoadAppEmployeeTable2(EmpAddressId);
				//ClearAddressFields();
				//BindGrid();
			}
		}
	}
}
