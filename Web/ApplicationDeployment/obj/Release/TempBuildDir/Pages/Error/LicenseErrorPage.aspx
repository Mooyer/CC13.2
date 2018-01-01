<%-- License
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LicenseErrorPage.aspx.cs" MasterPageFile="ErrorPageMaster.Master" Inherits="MatrixPACS.ImageServer.Web.Application.Pages.Error.LicenseErrorPage" %>
<%@ Import Namespace="MatrixPACS.ImageServer.Web.Common.Utilities"%>
<%@ Import Namespace="Resources"%>
<%@ Import namespace="System.Threading"%>

<asp:Content runat="server" ContentPlaceHolderID="ErrorMessagePlaceHolder">
	    <asp:label ID="ErrorMessageLabel" runat="server">
	        <%= ErrorMessages.LicenseError %>
	    </asp:label>
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="DescriptionPlaceHolder">
   <asp:Label ID = "DescriptionLabel" runat="server">
   <%= HttpContext.Current.Items.Contains(ImageServerConstants.ContextKeys.ErrorDescription)?
        HtmlUtility.Encode(HttpContext.Current.Items[ImageServerConstants.ContextKeys.ErrorDescription] as string)
           :HtmlUtility.Encode(ErrorMessages.LicenseErrorLongDescription)
   %>
    </asp:Label>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="UserEscapePlaceHolder">
    <table runat="server" width="100%" class="UserEscapeTable">
        <tr>
            <td class="UserEscapeCell"><a href="javascript:window.location.reload()" class="UserEscapeLink"><%= Labels.Refresh %></a></td>
        </tr>
    </table>
</asp:Content>