using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using BillTask.Models;

namespace BillTask.main.template
{
    public partial class invoice_create : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected List<Bill> Bills { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //load data from database when load the page
            LoadAllBills();

        }


        //take bill data and save in database
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            decimal sum = 0;
            for (int i = 1; i <= Request.Form.Count; i++)
            {
                string itemName = Request.Form["itemName" + i];
                if (itemName == null)
                    break;
                else
                {
                    int quantity = Convert.ToInt32(Request.Form["quantity" + i]);
                    decimal unitPrice = Convert.ToDecimal(Request.Form["unitPrice" + i]);
                    decimal total = quantity * unitPrice;
                    sum += total;


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("INSERT INTO Bill (ItemName, Quantity, UnitPrice, Total) VALUES (@ItemName, @Quantity, @UnitPrice, @Total)", connection))
                        {
                            command.Parameters.AddWithValue("@ItemName", itemName);
                            command.Parameters.AddWithValue("@Quantity", quantity);
                            command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                            command.Parameters.AddWithValue("@Total", total);

                            command.ExecuteNonQuery();
                        }
                    }

                }
               
            }

            LoadAllBills();
        }


        //get value of each control to save it in database
        private T FindControl<T>(Control container) where T : Control
        {
            foreach (Control control in container.Controls)
            {
                if (control is T)
                {
                    return (T)control;
                }
                else
                {
                    T nestedControl = FindControl<T>(control);
                    if (nestedControl != null)
                    {
                        return nestedControl;
                    }
                }
            }

            return null;
        }



        //display all data from database
        private void LoadAllBills()
        {
            Bills = new List<Bill>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT ItemName, Quantity, UnitPrice, Total from Bill", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bill bill = new Bill
                            {
                                ItemName = reader["ItemName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                Total = Convert.ToDecimal(reader["Total"])
                            };

                            Bills.Add(bill);
                        }
                        rptBills.DataSource = Bills;
                        rptBills.DataBind();
                    }
                }
            }
        }
    }
}

