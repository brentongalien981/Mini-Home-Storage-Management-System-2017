using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class EditItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.Security.MembershipUser usr = System.Web.Security.Membership.GetUser();

        hiddenField.Value = usr.ProviderUserKey.ToString();
    }


    protected void itemDetails_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        grid.DataBind();
    }

    protected void itemDetails_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
    {
        grid.DataBind();
    }

    protected void itemDetails_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        grid.DataBind();
    }
}