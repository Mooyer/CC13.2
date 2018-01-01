﻿#region License

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

#endregion

namespace MatrixPACS.ImageServer.Core.Events
{
	/// <summary>
	/// EventArgs for when a new SOP Instance has been processed
	/// </summary>
	/// <remarks>
	/// The following example code shows how to define an extension that wil receive NewSopEventArgs events.
	/// Just create a class that implements <see cref="IEventHandler{TImageServerEventArgs}"/> and add
	/// the appropriate ExtensionOf attribute as shown below.  In this event case, it will be called each
	/// time a new SOP Instance UID is processed by the ImageServer.
	/// <code>
	/// [ExtensionOf(typeof(EventExtensionPoint{NewSopEventArgs}))]
	/// public class NewSopEventHandler : IEventHandler{NewSopEventArgs} 
	/// {
	///    public void EventHandler(object sender, NewSopEventArgs e)
	///    {
	///    }
	/// }
	/// </code>
	/// </remarks>
	[ImageServerEvent]
	public class NewSopEventArgs : SopEventArgs
	{
	}
}
