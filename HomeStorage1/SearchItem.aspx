<%@ Page Title="Search Recipe" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchItem.aspx.cs" Inherits="SearchItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .widelabel {
            display: -moz-inline-block;
            display: inline-block;
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Search Item</h1>

    <p>
        <span class="widelabel">Item Id:</span>
        <asp:textbox id="itemIdTextBox" runat="server" />
        <br />

        <span class="widelabel">Item Name:</span>
        <asp:textbox id="itemNameTextBox" runat="server" />
        <br />

        <span class="widelabel">Item Type:</span>
        <asp:dropdownlist id="itemTypeNameList" runat="server" />
        <br />

        <span class="widelabel">Tags:</span>
        <asp:textbox id="tagsTextbox" runat="server" />
        <br />

        <span class="widelabel">Description:</span>
        <asp:textbox id="descriptionTextbox" runat="server" />
        <br />

        <span class="widelabel">Contained By:</span>
        <asp:textbox id="containedByTextbox" runat="server" />
        <br />

        <span class="widelabel">Item Privacy:</span>
        <asp:dropdownlist id="privacyList" runat="server">
            <asp:ListItem Text="Only My Items" Value="1" />
            <asp:ListItem Text="Public and My Own Items" Value="0" />
        </asp:dropdownlist>
        <br />
    </p>
    <p>
        <asp:button id="searchButton" text="Search Recipe"
            width="200" runat="server" onclick="searchButton_Click" />
    </p>

    <%--For db error messages.--%>
    <p>
        <asp:label id="dbErrorLabel" forecolor="Red" runat="server" />
    </p>


    <%--For displaying the results.--%>
    <div>
        <br />
        <br />
        <asp:label id="searchResultsLabel" runat="server"></asp:label>
        <br />
        <asp:gridview id="myGridView" runat="server"></asp:gridview>
    </div>


</asp:Content>

