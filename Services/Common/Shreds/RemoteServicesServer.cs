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
using System.Threading;
using MatrixPACS.Common;
using MatrixPACS.Common.Shreds;
using MatrixPACS.ImageServer.Common;
using MatrixPACS.ImageServer.Core.Helpers;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.Server.ShredHost;

namespace MatrixPACS.ImageServer.Services.Common.Shreds
{
	/// <summary>
	/// Plugin to host ImageServer-specific web services.
	/// </summary>
	[ExtensionOf(typeof (ShredExtensionPoint))]
	[ShredIsolation(Level = ShredIsolationLevel.None)]
	public class RemoteServicesServer : WcfShred
	{

		#region Private Members

		private readonly string _className;
		private ServiceMount _serviceMount;
		private ShredStartupHelper _shredStartupHelper = null;
		#endregion

		#region Constructors

		public RemoteServicesServer()
		{
			_className = GetType().ToString();
		}

		#endregion

		#region IShred Implementation Shred Override

		public override void Start()
		{
			Platform.Log(LogLevel.Debug, "{0}[{1}]: Start invoked", _className, AppDomain.CurrentDomain.FriendlyName);
			_shredStartupHelper = new ShredStartupHelper(GetDisplayName());
			
			_shredStartupHelper.Initialize();
			if (_shredStartupHelper.StopFlag)
				return;
			_shredStartupHelper = null;
			
			try
			{
				MountWebServices();
			}
			catch (Exception e)
			{
				Platform.Log(LogLevel.Fatal, e, "Unexpected exception starting Web Services Server Shred");
			}
		}

		private void MountWebServices()
		{
			_serviceMount = new ServiceMount(new Uri(WebServicesSettings.Default.BaseUri), typeof (ServerWsHttpConfiguration).AssemblyQualifiedName)
			                	{
			                		MaxReceivedMessageSize = EnterpriseImageServerServiceSettings.Default.MaxReceivedMessageSize,
									SendTimeoutSeconds = EnterpriseImageServerServiceSettings.Default.SendTimeoutSeconds
			                	};

			_serviceMount.AddServices(new ApplicationServiceExtensionPoint());
			_serviceMount.OpenServices();
		}

		public override void Stop()
		{
			Platform.Log(LogLevel.Info, "{0}[{1}]: Stop invoked", _className, AppDomain.CurrentDomain.FriendlyName);
			
			if (_shredStartupHelper != null)
				_shredStartupHelper.StopService();

			if (_serviceMount != null)
				_serviceMount.CloseServices();
		}

		public override string GetDisplayName()
		{
			return SR.RemoteImageServerServicesServer;
		}

		public override string GetDescription()
		{
			return SR.RemoteImageServerServicesServerDescription;
		}

		#endregion
	}
}