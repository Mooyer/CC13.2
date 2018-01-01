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

using System.Xml.Serialization;
using MatrixPACS.Dicom;

namespace MatrixPACS.ImageServer.Core.Data
{
	/// <summary>
	/// Represents the serializable detailed information of an image set.
	/// </summary>
	[XmlRoot("Details")]
	public class ImageSetDetails
	{
		#region Constructors

		public ImageSetDetails()
		{
		    StudyInfo = new StudyInformation();
		}

		public ImageSetDetails(IDicomAttributeProvider attributeProvider)
		{
			StudyInfo = new StudyInformation(attributeProvider);
		}

		#endregion

		#region Public Properties

		public int SopInstanceCount { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="StudyInformation"/> of the image set.
		/// </summary>
		public StudyInformation StudyInfo { get; set; }

		#endregion

		#region Public Methods
		/// <summary>
		/// Inserts a <see cref="DicomMessageBase"/> into the set.
		/// </summary>
		/// <param name="message"></param>
		public void InsertFile(DicomMessageBase message)
		{
			StudyInfo.Add(message);
		}
		#endregion
	}
}