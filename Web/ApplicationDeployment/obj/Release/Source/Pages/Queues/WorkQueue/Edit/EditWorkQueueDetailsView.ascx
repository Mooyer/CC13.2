﻿<%-- License

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
<%@ Import Namespace="MatrixPACS.ImageServer.Core.Edit" %>
<%@ Import Namespace="MatrixPACS.ImageServer.Web.Common.Utilities" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditWorkQueueDetailsView.ascx.cs"
    Inherits="MatrixPACS.ImageServer.Web.Application.Pages.Queues.WorkQueue.Edit.EditWorkQueueDetailsView" %>
<asp:DetailsView ID="EditInfoDetailsView" runat="server" AutoGenerateRows="False"
    CellPadding="2" GridLines="Horizontal" CssClass="GlobalGridView" Width="100%"
    OnDataBound="GeneralInfoDetailsView_DataBound">
    <Fields>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_Type %>">
            <ItemTemplate>
                <asp:Label ID="Type" runat="server" Text='<%# Eval("Type.Description") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_ScheduledDateTime %>">
            <ItemTemplate>
                <ccUI:DateTimeLabel ID="ScheduledDateTime" runat="server" Value='<%# Eval("ScheduledDateTime") %>'></ccUI:DateTimeLabel>
            </ItemTemplate>
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_ExpirationDateTIme %>">
            <ItemTemplate>
                <ccUI:DateTimeLabel ID="ExpirationTime" runat="server" Value='<%# Eval("ExpirationTime") %>'></ccUI:DateTimeLabel>
            </ItemTemplate>
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_InsertDateTime %>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="False" />
            <ItemTemplate>
                <ccUI:DateTimeLabel ID="InsertTime" runat="server" Value='<%# Eval("InsertTime") %>'></ccUI:DateTimeLabel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_Status %>">
            <ItemTemplate>
                <asp:Label ID="Status" runat="server" Text='<%# Eval("Status.Description") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_Priority %>">
            <ItemTemplate>
                <asp:Label ID="Priority" runat="server" Text='<%# Eval("Priority.Description") %>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
        </asp:TemplateField>
        <asp:BoundField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_ProcessingServer %>"
            HeaderStyle-CssClass="StudyDetailsViewHeader" DataField="ServerDescription" />
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, AccessionNumber %>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
            <ItemTemplate>
                <asp:Label runat="server" ID="AccessionNumber" Text='<%# Eval("Study.AccessionNumber") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, StudyDateTime %>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
            <ItemTemplate>
                <ccUI:DALabel ID="StudyDate" runat="server" Value='<%# Eval("Study.StudyDate") %>'></ccUI:DALabel>
                <ccUI:TMLabel ID="StudyTime" runat="server" Value='<%# Eval("Study.StudyTime") %>'></ccUI:TMLabel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, Modalities %>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
            <ItemTemplate>
                <asp:Label runat="server" ID="Modalities" Text='<%# Eval("Study.Modalities") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, PatientName %>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
            <ItemTemplate>
                <ccUI:PersonNameLabel ID="ReferringPhysiciansName" runat="server" PersonName='<%# Eval("Study.PatientName") %>'
                    PersonNameType="Dicom"></ccUI:PersonNameLabel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, PatientID %>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
            <ItemTemplate>
                <asp:Label runat="server" ID="PatientID" Text='<%# Eval("Study.PatientId") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_FailureCount %>"
            HeaderStyle-CssClass="StudyDetailsViewHeader" DataField="FailureCount" />
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_FailureDescription %>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="False" VerticalAlign="Top" />
            <ItemTemplate>
                <asp:Label runat="server" ID="PatientID" Text='<%# HtmlUtility.Encode(Eval("FailureDescription") as String) %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_InstancesPending %>"
            HeaderStyle-CssClass="StudyDetailsViewHeader" DataField="NumInstancesPending" />
        <asp:BoundField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_SeriesPending %>"
            HeaderStyle-CssClass="StudyDetailsViewHeader" DataField="NumSeriesPending" />
        <asp:BoundField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_StorageLocation %>"
            HeaderStyle-CssClass="StudyDetailsViewHeader" DataField="StorageLocationPath" />
        <asp:TemplateField HeaderText="<%$Resources: DetailedViewFieldLabels, WorkQueue_EditTags%>">
            <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="False" VerticalAlign="Top" />
            <ItemTemplate>
                <div style="padding: 2px;">
                    <table width="100%" cellspacing="0">
                        <tr style="color: #205F87; background: #eeeeee; padding-top: 2px;">
                            <td>
                                <b>
                                    <%= ColumnHeaders.Tag%></b>
                            </td>
                            <td>
                                <b>
                                    <%= ColumnHeaders.OldValue%></b>
                            </td>
                            <td>
                                <b>
                                    <%= ColumnHeaders.NewValue%></b>
                            </td>
                        </tr>
                        <%
                            
                        foreach (UpdateItem cmd in UpdateItems)
                        { %>
                        <tr style="background: #fefefe">
                            <td style="border-bottom: solid 1px #dddddd">
                                <pre><%= HtmlUtility.Encode(cmd.TagName) %></pre>
                            </td>
                            <td style="border-bottom: solid 1px #dddddd">
                                <pre><%= HtmlUtility.Encode(cmd.OriginalValue) %></pre>
                            </td>
                            <td style="border-bottom: solid 1px #dddddd">
                                <pre><%= HtmlUtility.Encode(cmd.Value!=null? cmd.Value: string.Empty) %></pre>
                            </td>
                        </tr>
                     <% } %>
                    </table>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Fields>
    <HeaderStyle CssClass="StudyDetailsViewHeader" Wrap="false" />
    <RowStyle CssClass="GlobalGridViewRow" />
    <AlternatingRowStyle CssClass="GlobalGridViewAlternatingRow" />
</asp:DetailsView>
