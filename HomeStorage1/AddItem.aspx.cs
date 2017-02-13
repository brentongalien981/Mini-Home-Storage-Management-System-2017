using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // If it's not a POST request.
        if (!IsPostBack)
        {
            LoadLists();
        }
    }

    protected void addButton_Click(object sender, EventArgs e)
    {
        System.Web.Security.MembershipUser usr = System.Web.Security.Membership.GetUser();
        if (usr != null)
        {
            string script = "alert(\"date: " + Calendar1.SelectedDate.ToShortDateString() + "\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            if (Page.IsValid)
            {
                SqlConnection conn;
                SqlCommand comm;
                SqlDataReader reader;
                string connectionString =
                ConfigurationManager.ConnectionStrings[
                "Assignment4"].ConnectionString;
                conn = new SqlConnection(connectionString);

                comm = new SqlCommand("MyAddProcedure2", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add("@RecipeName", System.Data.SqlDbType.NVarChar, 50);
                comm.Parameters["@RecipeName"].Value = recipeNameTextBox.Text;

                comm.Parameters.Add("@CategoryId", System.Data.SqlDbType.Int);
                comm.Parameters["@CategoryId"].Value = categoryList.SelectedItem.Value;

                comm.Parameters.Add("@CuisineId", System.Data.SqlDbType.Int);
                comm.Parameters["@CuisineId"].Value = cuisineList.SelectedItem.Value;

                comm.Parameters.Add("@CookingTime", System.Data.SqlDbType.Int);
                comm.Parameters["@CookingTime"].Value = cookingTimeTextBox.Text;

                comm.Parameters.Add("@Portions", System.Data.SqlDbType.Int);
                comm.Parameters["@Portions"].Value = portionsTextBox.Text;

                comm.Parameters.Add("@Privacy", System.Data.SqlDbType.Int);
                comm.Parameters["@Privacy"].Value = privacyList.SelectedItem.Value;

                comm.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 500);
                comm.Parameters["@Description"].Value = descriptionTextBox.Text;

                comm.Parameters.Add("@SubmissionDate", System.Data.SqlDbType.NVarChar, 10);
                comm.Parameters["@SubmissionDate"].Value = Calendar1.SelectedDate.ToShortDateString();

                // Output
                comm.Parameters.Add("@AddedRecipeId", System.Data.SqlDbType.Int);
                comm.Parameters["@AddedRecipeId"].Direction = System.Data.ParameterDirection.Output;

                // This will be set to the id of the added item.
                // This will be used for the mapping table.
                int returnId = -1;

                try
                {
                    conn.Open();

                    comm.ExecuteNonQuery();

                    returnId = Convert.ToInt32(comm.Parameters["@AddedRecipeId"].Value);
                    dbErrorLabel.ForeColor = System.Drawing.Color.Green;
                    dbErrorLabel.Text = "Succes ADDING the RECIPE!<br />" +
                                        "Added Recipe Id: " + returnId;

                }
                catch (Exception ex)
                {
                    dbErrorLabel.ForeColor = System.Drawing.Color.Red;
                    dbErrorLabel.Text = "Error ADDING the RECIPE!<br /> + " + ex.ToString();
                }



                /* Query for the mapping table. */
                comm = new SqlCommand("InsertToProjectUserRecipe", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                // Add to mapping table.
                comm.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier);
                comm.Parameters["@UserId"].Value = usr.ProviderUserKey;

                comm.Parameters.Add("@RecipeId", System.Data.SqlDbType.Int);
                comm.Parameters["@RecipeId"].Value = returnId;

                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    dbErrorLabel.ForeColor = System.Drawing.Color.Red;
                    dbErrorLabel.Text = "Error ADDING to ProjectUserRecipe!<br /> + " + ex.ToString();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        else
        {
            string script = "alert(\"ERROR: usr is NULL!!\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
        }
    }

    private void LoadLists()
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader reader;
        string connectionString =
        ConfigurationManager.ConnectionStrings[
        "HomeStorage"].ConnectionString;
        conn = new SqlConnection(connectionString);

        // Command for populating the list:itemTypeList.
        comm = new SqlCommand(
        "SELECT * FROM ItemType", conn);
        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            itemTypeList.DataSource = reader;
            itemTypeList.DataValueField = "ItemTypeId";
            itemTypeList.DataTextField = "ItemTypeName";
            itemTypeList.DataBind();
            reader.Close();
        }
        catch
        {
            dbErrorLabel.Text =
            "Error loading the list of Item Types..<br />";
        }
        finally
        {
            conn.Close();
        }
        // Clear the TextBoxes.
        itemNameTextBox.Text = "";
        tagsTextBox.Text = "";
        descriptionTextBox.Text = "";
        containedByTextbox.Text = "";
    }
}