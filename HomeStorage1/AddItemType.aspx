<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddItemType.aspx.cs" Inherits="AddItemType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:SqlDataSource ID="itemTypesDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>" SelectCommand="SELECT [ItemTypeId], [ItemTypeName] FROM [ItemType]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="itemTypeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:LocalSqlServer %>"
        DeleteCommand="DELETE FROM [ItemType] WHERE [ItemTypeId] = @ItemTypeId"
        InsertCommand="INSERT INTO [ItemType] ([ItemTypeName]) VALUES (@ItemTypeName)"
        SelectCommand="SELECT [ItemTypeId], [ItemTypeName] FROM [ItemType] WHERE ([ItemTypeId] = @ItemTypeId)"
        UpdateCommand="UPDATE [ItemType] SET [ItemTypeName] = @ItemTypeName WHERE [ItemTypeId] = @ItemTypeId">

        <DeleteParameters>
            <asp:Parameter Name="ItemTypeId" Type="Int32" />
        </DeleteParameters>

        <InsertParameters>
            <asp:Parameter Name="ItemTypeName" Type="String" />
        </InsertParameters>

        <SelectParameters>
            <asp:ControlParameter ControlID="grid" Name="ItemTypeId" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>

        <UpdateParameters>
            <asp:Parameter Name="ItemTypeId" Type="Int32" />
        </UpdateParameters>

    </asp:SqlDataSource>






    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="itemTypesDataSource" DataKeyNames="ItemTypeId">
        <Columns>
            <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeId" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="ItemTypeName" />
            <asp:ButtonField CommandName="Select" Text="Select" />
        </Columns>
    </asp:GridView>

    <br />
    <br />



    <asp:DetailsView ID="itemTypeDetailsView" runat="server" AutoGenerateRows="False" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" DataSourceID="itemTypeDataSource" DataKeyNames="ItemTypeId"
        OnItemDeleted="itemTypeDetails_ItemDeleted"
        OnItemInserted="itemTypeDetails_ItemInserted"
        OnItemUpdated="itemTypeDetails_ItemUpdated">

        <HeaderTemplate>
            <%#Eval("ItemTypeName") == null ? "Adding New Item Type Name..." : "Details of Item Type: " + Eval("ItemTypeName")%>
        </HeaderTemplate>

        <Fields>
            <asp:BoundField DataField="ItemTypeId" HeaderText="ItemTypeId" ReadOnly="true" InsertVisible="False" />
            <asp:BoundField DataField="ItemTypeName" HeaderText="ItemTypeName" />
        </Fields>
    </asp:DetailsView>




    <p>
        <asp:LinkButton ID="button_addItemType" runat="server"
            Text="* Add New Item Type Here" OnClick="button_addItemType_Click" />
    </p>
</asp:Content>


