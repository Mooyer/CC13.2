#region License

// Copyright (c) 2013, MatrixPACS Inc.
// All rights reserved.
// http://www.matriximage.ca
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
using System.Collections.Generic;
using MatrixPACS.Common;
using MatrixPACS.Dicom;
using MatrixPACS.Dicom.Network;
using MatrixPACS.Dicom.Network.Scp;
using MatrixPACS.Enterprise.Core;
using MatrixPACS.ImageServer.Common.WorkQueue;
using MatrixPACS.ImageServer.Core;
using MatrixPACS.ImageServer.Enterprise;
using MatrixPACS.ImageServer.Model;
using MatrixPACS.ImageServer.Model.Brokers;
using MatrixPACS.ImageServer.Model.Parameters;
using RIS4PACS;
using System.Threading;

namespace MatrixPACS.ImageServer.Services.Dicom
{
    /// <summary>
    /// Abstract class for handling Storage SCP connections.
    /// </summary>
    /// <remarks>
    /// <para>This class is an abstract base class for ImageServer plugins that process DICOM C-STORE
    /// request messages.  The class implements a number of common methods needed for SCP handlers.
    /// The class also implements the <see cref="IDicomScp{TContext}"/> interface.</para>
    /// </remarks>
    public abstract class StorageScp : BaseScp
    {
        #region Abstract Properties
        public abstract string StorageScpType { get; }
        string strAcsNbr;
        string sDefaultSCS;
        #endregion

        #region Protected Members

        /// <summary>
        /// Converts a <see cref="DicomMessage"/> instance into a <see cref="DicomFile"/>.
        /// </summary>
        /// <remarks>This routine sets the Source AE title, </remarks>
        /// <param name="message"></param>
        /// <param name="filename"></param>
        /// <param name="assocParms"></param>
        /// <returns></returns>
        protected static DicomFile ConvertToDicomFile(DicomMessage message, string filename, AssociationParameters assocParms)
        {
            // This routine sets some of the group 0x0002 elements.
            var file = new DicomFile(message, filename)
            {
                SourceApplicationEntityTitle = assocParms.CallingAE,
                TransferSyntax = message.TransferSyntax
            };

            return file;
        }

        /// <summary>
        /// Load from the database the configured transfer syntaxes
        /// </summary>
        /// <param name="read">a Read context</param>
        /// <param name="partitionKey">The partition to retrieve the transfer syntaxes for</param>
        /// <param name="encapsulated">true if searching for encapsulated syntaxes only</param>
        /// <returns>The list of syntaxes</returns>
        protected static IList<PartitionTransferSyntax> LoadTransferSyntaxes(IReadContext read, ServerEntityKey partitionKey, bool encapsulated)
        {
            var broker = read.GetBroker<IQueryServerPartitionTransferSyntaxes>();

            var criteria = new PartitionTransferSyntaxQueryParameters
            {
                ServerPartitionKey = partitionKey
            };

            IList<PartitionTransferSyntax> list = broker.Find(criteria);


            var returnList = new List<PartitionTransferSyntax>();
            foreach (PartitionTransferSyntax syntax in list)
            {
                if (!syntax.Enabled) continue;

                TransferSyntax dicomSyntax = TransferSyntax.GetTransferSyntax(syntax.Uid);
                if (dicomSyntax.Encapsulated == encapsulated)
                    returnList.Add(syntax);
            }
            return returnList;
        }
        #endregion

        #region Overridden BaseSCP methods

        protected override DicomPresContextResult OnVerifyAssociation(AssociationParameters association, byte pcid)
        {
            if (Device == null)
                return DicomPresContextResult.Accept;

            if (!Device.AllowStorage)
            {
                return DicomPresContextResult.RejectUser;
            }

            if (Device.AcceptKOPR || Context.AllowKOPR)
            {
                DicomPresContext context = association.GetPresentationContext(pcid);
                if (context.AbstractSyntax.Equals(SopClass.KeyObjectSelectionDocumentStorage)
                  || context.AbstractSyntax.Equals(SopClass.GrayscaleSoftcopyPresentationStateStorageSopClass)
                  || context.AbstractSyntax.Equals(SopClass.BlendingSoftcopyPresentationStateStorageSopClass)
                  || context.AbstractSyntax.Equals(SopClass.ColorSoftcopyPresentationStateStorageSopClass)
                  || context.AbstractSyntax.Equals(SopClass.PseudoColorSoftcopyPresentationStateStorageSopClass))
                    return DicomPresContextResult.Accept;

                return DicomPresContextResult.RejectUser;
            }

            return DicomPresContextResult.Accept;
        }

        public override bool ReceiveMessageAsFileStream(DicomServer server, ServerAssociationParameters association, byte presentationId, DicomMessage message)
        {
            var sopClassUid = message.AffectedSopClassUid;

            if (sopClassUid.Equals(SopClass.BreastTomosynthesisImageStorageUid)
                || sopClassUid.Equals(SopClass.EnhancedCtImageStorageUid)
                || sopClassUid.Equals(SopClass.EnhancedMrColorImageStorageUid)
                || sopClassUid.Equals(SopClass.EnhancedMrImageStorageUid)
                || sopClassUid.Equals(SopClass.EnhancedPetImageStorageUid)
                || sopClassUid.Equals(SopClass.EnhancedUsVolumeStorageUid)
                || sopClassUid.Equals(SopClass.EnhancedXaImageStorageUid)
                || sopClassUid.Equals(SopClass.EnhancedXrfImageStorageUid)
                || sopClassUid.Equals(SopClass.UltrasoundMultiFrameImageStorageUid)
                || sopClassUid.Equals(SopClass.MultiFrameGrayscaleByteSecondaryCaptureImageStorageUid)
                || sopClassUid.Equals(SopClass.MultiFrameGrayscaleWordSecondaryCaptureImageStorageUid)
                || sopClassUid.Equals(SopClass.MultiFrameSingleBitSecondaryCaptureImageStorageUid)
                || sopClassUid.Equals(SopClass.MultiFrameTrueColorSecondaryCaptureImageStorageUid))
            {
                server.DimseDatasetStopTag = DicomTagDictionary.GetDicomTag(DicomTags.ReconstructionIndex); // Random tag at the end of group 20
                server.StreamMessage = true;
                return true;
            }

            return false;
        }

        #endregion Overridden BaseSCP methods

        #region IDicomScp Members

        public override IDicomFilestreamHandler OnStartFilestream(DicomServer server, ServerAssociationParameters association,
                                                                  byte presentationId, DicomMessage message)
        {
            return new StorageFilestreamHandler(Context, Device, association);
        }


        //update RIS4PACS if the accession number is new
        private static void UpdateRIS(object AccessionNumber)
        {
            string tempACN = (string)AccessionNumber;
            try
            {
                RIS4PACS.RISInterface ri = new RIS4PACS.RISInterface();
                ri.SetImageArrive(tempACN);
                Platform.Log(LogLevel.Info, "RIS4PACS DB update successful.The updated accession number is {0}.", tempACN);
            }
            catch
            {
                Platform.Log(LogLevel.Info, "RIS4PACS DB update failure.");
            }
        }
   
        
        public override bool OnReceiveRequest(DicomServer server, ServerAssociationParameters association, byte presentationId, DicomMessage message)
        {
            Platform.Log(LogLevel.Info, "Received request,the message is {0}!!!", message.CommandField);
            //The following part is added for some devices won't contain the SpecificCharacterSet DicomTag.
            //The may introduce troubles when decoding the characters.
            //If there's SpecificCharacterSet DicomTag in the image, do nothing
            //If there's no SpecificCharacterSet DicomTag in the image, the configured DefaultSCS for the device will be added.
            //If there's no SpecificCharacterSet DicomTag in the image and no configured DefaultSCS, ISO_IR 100 will be used.
            if (string.IsNullOrEmpty(message.DataSet.SpecificCharacterSet))
            {
                if (string.IsNullOrEmpty(Device.DefaultSCS))
                    sDefaultSCS = "ISO_IR 100";
                else
                    sDefaultSCS = Device.DefaultSCS;

                message.DataSet[DicomTags.SpecificCharacterSet].SetStringValue(sDefaultSCS);
                message.DataSet.SpecificCharacterSet = sDefaultSCS;
            }
            try
            {
                var context = new SopInstanceImporterContext(
                    String.Format("{0}_{1}", association.CallingAE, association.TimeStamp.ToString("yyyyMMddhhmmss")),
                    association.CallingAE, association.CalledAE);

                if (Device != null && Device.DeviceTypeEnum.Equals(DeviceTypeEnum.PrimaryPacs))
                {
                    context.DuplicateProcessing = DuplicateProcessingEnum.OverwriteSopAndUpdateDatabase;
                }
                var importer = new SopInstanceImporter(context);


                DicomProcessingResult result = importer.Import(message);



                if (result.Successful)
                {
                    if (!String.IsNullOrEmpty(result.AccessionNumber))
                    {
                        ///if the accession number is fresh, write the accession number to RIS DB. This is for Dalian Yiwei

                        if (strAcsNbr != result.AccessionNumber)
                        {
                            strAcsNbr = result.AccessionNumber;
                            /// Platform.Log(LogLevel.Info, "The AccessionNumber of this received SOP Instance is:{0}", result.AccessionNumber);
                            //                               RIS4PACS.RISInterface ri = new RIS4PACS.RISInterface();
                            //                               ri.SetImageArrive(result.AccessionNumber);
                            try
                            {
                                Thread thread = new Thread(new ParameterizedThreadStart(UpdateRIS));
                                thread.Start((object)strAcsNbr);

                                //                                RIS4PACS.RISInterface ri = new RIS4PACS.RISInterface();
                                //                                ri.SetImageArrive(result.AccessionNumber);
                            }
                            catch
                            {
                                Platform.Log(LogLevel.Info, "RIS4PACS database update failure.");

                            }
                        }

                        Platform.Log(LogLevel.Info, "Received SOP Instance {0} from {1} to {2} (A#:{3} StudyUid:{4} )",
                                     result.SopInstanceUid, association.CallingAE, association.CalledAE, result.AccessionNumber,
                                     result.StudyInstanceUid);

                    }
                    else
                        Platform.Log(LogLevel.Info, "Received SOP Instance {0} from {1} to {2} (StudyUid:{3})",
                                     result.SopInstanceUid, association.CallingAE, association.CalledAE,
                                     result.StudyInstanceUid);
                }
                else
                    Platform.Log(LogLevel.Warn, "Failure importing sop: {0}", result.ErrorMessage);

                server.SendCStoreResponse(presentationId, message.MessageId, message.AffectedSopInstanceUid, result.DicomStatus);
                return true;
            }
            catch (DicomDataException ex)
            {
                Platform.Log(LogLevel.Error, ex);
                return false;  // caller will abort the association
            }
            catch (Exception ex)
            {
                Platform.Log(LogLevel.Error, ex);
                return false;  // caller will abort the association
            }
        }

        #endregion
    }
}