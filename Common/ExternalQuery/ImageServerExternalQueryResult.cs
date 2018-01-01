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

using System.Runtime.Serialization;
using MatrixPACS.Common.Serialization;

namespace MatrixPACS.ImageServer.Common.ExternalQuery
{
	/// <summary>
	/// Base class for defining results from external query requests
	/// </summary>
	/// <remarks>
	/// <para>
	///  This base class forms the foundation of a polymorphic plugin based 
	/// mechanism for performing queries against the ImageServer.  The 
	/// class is used for relaying results back to the client.
	/// </para>
	/// <para>
	/// The intent is for the data contracts to be usable both for WCF 
	/// style communication with the ImageServer and REST based services.
	/// </para>
	/// </remarks>
	[DataContract(Namespace = ImageServerExternalQueryNamespace.Value)]
	[ImageServerExternalQueryType("9F65E8F8-593A-47A9-BB90-001EF9F5D25F")]
	public abstract class ImageServerExternalQueryResult : DataContractBase
	{
	}
}
