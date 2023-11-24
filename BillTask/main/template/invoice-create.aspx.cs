using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace BillTask.main.template
{
    public partial class invoice_create : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (HtmlTableRow row in myTable.Rows)
                {
                    // Skip the header row
                    if (row.Cells[0].Controls.Count > 0 && row.Cells[0].Controls[0] is CheckBox)
                        continue;

                    // Assuming the structure is consistent (checkbox, textbox, textbox, label)
                    TextBox quantityTextBox = FindControl<TextBox>(row.Cells[2]);
                    TextBox unitPriceTextBox = FindControl<TextBox>(row.Cells[3]);
                    Label resultLabel = FindControl<Label>(row.Cells[4]);

                    if (quantityTextBox != null && unitPriceTextBox != null && resultLabel != null)
                    {
                        string itemName = row.Cells[1].InnerText;
                        int quantity = Convert.ToInt32(quantityTextBox.Text);
                        decimal unitPrice = Convert.ToDecimal(unitPriceTextBox.Text);
                        decimal total = Convert.ToDecimal(resultLabel.Text);

                        // Now you have the values from each row
                        // You can use these values to insert into the database or perform other operations
                        // ...

                        // For example, you can use SqlCommand to insert into the database
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
        }

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











    }
}
