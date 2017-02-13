<%@ Page Title="Add Item" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddItem.aspx.cs" Inherits="AddItem" %>

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
    <h1>Add Recipe</h1>

    <p>
        <span class="widelabel">Item Name:</span>
        <asp:textbox id="itemNameTextBox" runat="server" />
        <asp:requiredfieldvalidator id="itemNameRequiredFieldValidator" forecolor="Red" runat="server" errormessage="* Item name is required." controltovalidate="itemNameTextBox" setfocusonerror="True" display="Dynamic"></asp:requiredfieldvalidator>
        <br />

        <span class="widelabel">Item Type:</span>
        <asp:dropdownlist id="itemTypeList" runat="server" />
        <br />

        <span class="widelabel">Tags:</span>
        <asp:textbox id="tagsTextBox" runat="server" />
        <br />

        <%-- Don't need this. --%>
        <%-- 
        <span class="widelabel">Portions:</span>
        <asp:textbox id="portionsTextBox" runat="server" />
        <asp:requiredfieldvalidator id="portionsRequiredFieldValidator" forecolor="Red" runat="server" errormessage="* Portions is required." controltovalidate="portionsTextBox" display="Dynamic" setfocusonerror="True"></asp:requiredfieldvalidator>
        <asp:comparevalidator id="portionsCompareValidator" runat="server" forecolor="Red" errormessage="* Portions should be greater than 0." type="Integer" controltovalidate="portionsTextBox" valuetocompare="0" operator="GreaterThan" display="Dynamic" setfocusonerror="True"></asp:comparevalidator>
        <br />
        --%>


        <span class="widelabel">Description:</span>
        <asp:textbox id="descriptionTextBox" runat="server" />
        <br />

        <span class="widelabel">Contained by:</span>
        <asp:textbox id="containedByTextbox" runat="server" />
        <asp:comparevalidator id="containedByCompareValidator" runat="server" forecolor="Red" errormessage="* Container number should be greater than 0." type="Integer" controltovalidate="containedByTextbox" valuetocompare="0" operator="GreaterThan" display="Dynamic" setfocusonerror="True"></asp:comparevalidator>
        <br />

        <span class="widelabel">Private:</span>
        <asp:dropdownlist id="privacyList" runat="server">
            <asp:ListItem Text="Private" Value="1" />
            <asp:ListItem Text="Public" Value="0" />
        </asp:dropdownlist>
        <br />
        <br />

        <span class="widelabel">Date Added:</span>
        <asp:calendar id="Calendar1" runat="server" width="100"></asp:calendar>
    </p>

    <p>
        <asp:button id="addButton" text="Add Recipe"
            width="200" runat="server" onclick="addButton_Click" />
    </p>


    <%--For db error messages.--%>
    <p>
        <asp:label id="dbErrorLabel" forecolor="Red" runat="server" />
    </p>

    <%--For validation summary.--%>
    <asp:validationsummary id="ValidationSummary1" forecolor="Red" runat="server" />
</asp:Content>

