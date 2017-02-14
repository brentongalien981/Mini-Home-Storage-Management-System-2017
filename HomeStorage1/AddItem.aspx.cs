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
            string script = "alert(\"usr.ProviderUserKey: " + usr.ProviderUserKey + "\");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);

            if (Page.IsValid)
            {
                SqlConnection conn;
                SqlCommand comm;
                string connectionString = ConfigurationManager.ConnectionStrings["HomeStorage"].ConnectionString;
                conn = new SqlConnection(connectionString);

                comm = new SqlCommand("MyProcedure_AddItem", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.Add("@ItemName", System.Data.SqlDbType.NVarChar, 80);
                comm.Parameters["@ItemName"].Value = itemNameTextBox.Text;

                comm.Parameters.Add("@TypeId", System.Data.SqlDbType.Int);
                comm.Parameters["@TypeId"].Value = itemTypeList.SelectedItem.Value;

                comm.Parameters.Add("@Tags", System.Data.SqlDbType.NVarChar, 500);
                comm.Parameters["@Tags"].Value = tagsTextBox.Text;

                comm.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 500);
                comm.Parameters["@Description"].Value = descriptionTextBox.Text;


                comm.Parameters.Add("@ContainedBy", System.Data.SqlDbType.Int);
                string containedBy = containedByTextbox.Text;
                if (containedBy == "")
                {
                    //comm.Parameters["@ContainedBy"].Value = DBNull.Value;
                    comm.Parameters["@ContainedBy"].Value = -1;
                }
                else
                {
                    comm.Parameters["@ContainedBy"].Value = containedBy;
                }
                

                comm.Parameters.Add("@Private", System.Data.SqlDbType.Int);
                comm.Parameters["@Private"].Value = privacyList.SelectedItem.Value;

                comm.Parameters.Add("@DateAdded", System.Data.SqlDbType.NVarChar, 10);
                comm.Parameters["@DateAdded"].Value = Calendar1.SelectedDate.ToShortDateString();

                // Output
                comm.Parameters.Add("@AddedItemId", System.Data.SqlDbType.Int);
                comm.Parameters["@AddedItemId"].Direction = System.Data.ParameterDirection.Output;

                // This will be set to the id of the added item.
                // This will be used for the mapping table.
                int returnId = -1;

                try
                {
                    conn.Open();

                    comm.ExecuteNonQuery();

                    returnId = Convert.ToInt32(comm.Parameters["@AddedItemId"].Value);
                    dbErrorLabel.ForeColor = System.Drawing.Color.Green;
                    dbErrorLabel.Text = "Succes adding item!<br />" +
                                        "Added Item Id: " + returnId;

                }
                catch (Exception ex)
                {
                    dbErrorLabel.ForeColor = System.Drawing.Color.Red;
                    dbErrorLabel.Text = "Error ADDING ITEM!<br /> + " + ex.ToString();
                }



                /* Query for the mapping table. */
                comm = new SqlCommand("MyProcedure_AddUserItem", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                // Add to mapping table.
                comm.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier);
                comm.Parameters["@UserId"].Value = usr.ProviderUserKey;

                comm.Parameters.Add("@ItemId", System.Data.SqlDbType.Int);
                comm.Parameters["@ItemId"].Value = returnId;

                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    dbErrorLabel.ForeColor = System.Drawing.Color.Red;
                    dbErrorLabel.Text = "Error ADDING to table:UserItem!<br /> + " + ex.ToString();
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