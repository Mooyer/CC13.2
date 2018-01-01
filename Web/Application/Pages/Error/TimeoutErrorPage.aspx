<%--  License

// Copyright (c) 2013, MatrixPACS Inc.
// All rights reserved.
// http://www.MatrixPACS.ca
//
// This file is part of the MatrixPACS RIS/PACS open source project.
//
// The MatrixPACS RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The MatrixPACS RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the MatrixPACS RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeoutErrorPage.aspx.cs" MasterPageFile="ErrorPageMaster.Master" Inherits="MatrixPACS.ImageServer.Web.Application.Pages.Error.TimeoutErrorPage" %>
<%@ Import Namespace="Resources"%>
<%@ Import namespace="MatrixPACS.ImageServer.Web.Common.Security"%>
<%@ Import namespace="System.Threading"%>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ErrorMessagePlaceHolder">
	    <asp:label ID="ErrorMessageLabel" runat="server">
	        <%= ErrorMessages.SessionTimedout%>
	    </asp:label>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="DescriptionPlaceHolder">

    <asp:Label ID = "DescriptionLabel" runat="server">
            <%= String.Format(ErrorMessages.SessionTimedoutLongDescription,  Math.Round(SessionManager.SessionTimeout.TotalMinutes)) %>
    </asp:Label>
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="UserEscapePlaceHolder">
    <table width="100%" class="UserEscapeTable"><tr><td class="UserEscapeCell" style="width: 50%"><asp:LinkButton ID="LogoutButton" runat="server" CssClass="UserEscapeLink" OnClick="Login_Click"><%= Labels.Login %></asp:LinkButton></td><td style="width: 50%"><a href="javascript:window.close()" class="UserEscapeLink"><%=Labels.Close %></a></td></tr></table>
</asp:Content>