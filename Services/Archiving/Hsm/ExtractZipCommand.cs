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

using System.IO;
using MatrixPACS.Common;
using MatrixPACS.Dicom.Utilities.Command;
using MatrixPACS.ImageServer.Common;

namespace MatrixPACS.ImageServer.Services.Archiving.Hsm
{
	/// <summary>
	/// <see cref="CommandBase"/> for extracting a zip file containing study files to a specific directory.
	/// </summary>
	public class ExtractZipCommand : CommandBase
	{
		private readonly string _zipFile;
		private readonly string _destinationFolder;
		private readonly bool _overwrite;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="zip">The zip file to extract.</param>
		/// <param name="destinationFolder">The destination folder.</param>
		public ExtractZipCommand(string zip, string destinationFolder): base("Extract Zip File",true)
		{
			_zipFile = zip;
			_destinationFolder = destinationFolder;
			_overwrite = false;
		}

		/// <summary>
		/// Do the unzip.
		/// </summary>
		protected override void OnExecute(CommandProcessor theProcessor)
		{
		    var zipService = Platform.GetService<IZipService>();
			using (var zipReader = zipService.OpenRead(_zipFile))
			{
                zipReader.ExtractAll(_destinationFolder, _overwrite);
			}
		}

		/// <summary>
		/// Undo.  Remove the destination folder.
		/// </summary>
		protected override void OnUndo()
		{
			Directory.Delete(_destinationFolder, true);
		}
	}
}
