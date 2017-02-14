using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // If it's not a POST request.
        if (!IsPostBack)
        {
            LoadCategoryList();
        }
    }

    private void LoadCategoryList()
    {
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader reader;
        string connectionString =
        ConfigurationManager.ConnectionStrings[
        "HomeStorage"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand(
        "SELECT * FROM ItemType", conn);
        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            itemTypeNameList.DataSource = reader;
            itemTypeNameList.DataValueField = "ItemTypeId";
            itemTypeNameList.DataTextField = "ItemTypeName";
            itemTypeNameList.DataBind();
            reader.Close();
        }
        catch
        {
            dbErrorLabel.Text =
            "Error loading the list of Item Type..<br />";
        }
        finally
        {
            conn.Close();
        }
        // Clear the TextBoxes.
        itemIdTextBox.Text = "";
        itemNameTextBox.Text = "";
        tagsTextbox.Text = "";
        descriptionTextbox.Text = "";
        containedByTextbox.Text = "";
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        System.Web.Security.MembershipUser usr = System.Web.Security.Membership.GetUser();

        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader reader;
        string connectionString =
        ConfigurationManager.ConnectionStrings[
        "HomeStorage"].ConnectionString;
        conn = new SqlConnection(connectionString);

        if (privacyList.SelectedItem.Value == "1")
        {
            //Search only the user's own items.
            comm = new SqlCommand("SelectOnlyMyOwnItems", conn);
        }
        else
        {
            // Search all public recipes plus the user's..
            comm = new SqlCommand("SelectAllPublicPlusOwnItems", conn);
        }

        comm.CommandType = System.Data.CommandType.StoredProcedure;

        comm.Parameters.Add("@ItemId", System.Data.SqlDbType.NVarChar);
        comm.Parameters["@ItemId"].Value = string.Format("%{0}%", itemIdTextBox.Text);

        comm.Parameters.Add("@ItemName", System.Data.SqlDbType.NVarChar);
        comm.Parameters["@ItemName"].Value = string.Format("%{0}%", itemNameTextBox.Text);

        comm.Parameters.Add("@ItemTypeName", System.Data.SqlDbType.NVarChar);
        comm.Parameters["@ItemTypeName"].Value = string.Format("%{0}%", itemTypeNameList.SelectedItem.Text); ;

        comm.Parameters.Add("@Tags", System.Data.SqlDbType.NVarChar);
        comm.Parameters["@Tags"].Value = string.Format("%{0}%", tagsTextbox.Text);

        comm.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar);
        comm.Parameters["@Description"].Value = string.Format("%{0}%", descriptionTextbox.Text);

        comm.Parameters.Add("@ContainedBy", System.Data.SqlDbType.NVarChar);
        comm.Parameters["@ContainedBy"].Value = string.Format("%{0}%", containedByTextbox.Text);

        comm.Parameters.Add("@Private", System.Data.SqlDbType.NVarChar);
        comm.Parameters["@Private"].Value = string.Format("%{0}%", privacyList.SelectedItem.Value);

        comm.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier);
        comm.Parameters["@UserId"].Value = usr.ProviderUserKey;

        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            myGridView.DataSource = reader;
            myGridView.DataBind();
            reader.Close();
            searchResultsLabel.Text = "Search Results:";
        }
        catch (Exception ex)
        {
            dbErrorLabel.Text =
            "Error loading the details..<br />" +
            ex.ToString() + "<br>";
        }
        finally
        {
            conn.Close();
        }
    }
}