#region License

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

using System;
using MatrixPACS.Common;
using MatrixPACS.Common.Shreds;

namespace MatrixPACS.ImageServer.Services.ServiceLock.Shreds
{
	/// <summary>
	/// Plugin to handle ServiceLock processing for the ImageServer.
	/// </summary>
	[ExtensionOf(typeof(ShredExtensionPoint))]
	[ShredIsolationAttribute(Level = ShredIsolationLevel.None)]
	public class ServiceLockServerExtension : Shred
	{
		private readonly string _className;
      
		public ServiceLockServerExtension()
		{
			_className = GetType().ToString();
		}
		public override void Start()
		{
			Platform.Log(LogLevel.Info, "{0}[{1}]: Start invoked", _className, AppDomain.CurrentDomain.FriendlyName);

			ServiceLockServerManager.Instance.StartService();
		}

		public override void Stop()
		{
			ServiceLockServerManager.Instance.StopService();

			Platform.Log(LogLevel.Info, "{0}[{1}]: Stop invoked", _className, AppDomain.CurrentDomain.FriendlyName);
		}

		public override string GetDisplayName()
		{
			return SR.ServiceLockServer;
		}

		public override string GetDescription()
		{
			return SR.ServiceLockServerDescription;
		}
	}
}