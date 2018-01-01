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

// This file is auto-generated by the MatrixPACS.Model.SqlServer.CodeGenerator project.

namespace MatrixPACS.ImageServer.Model
{
    using System;
    using System.Collections.Generic;
    using MatrixPACS.ImageServer.Model.EntityBrokers;
    using MatrixPACS.ImageServer.Enterprise;
    using System.Reflection;

[Serializable]
public partial class AlertCategoryEnum : ServerEnum
{
      #region Private Static Members
      private static readonly AlertCategoryEnum _System = GetEnum("System");
      private static readonly AlertCategoryEnum _Application = GetEnum("Application");
      private static readonly AlertCategoryEnum _Security = GetEnum("Security");
      private static readonly AlertCategoryEnum _User = GetEnum("User");
      #endregion

      #region Public Static Properties
      /// <summary>
      /// System alert
      /// </summary>
      public static AlertCategoryEnum System
      {
          get { return _System; }
      }
      /// <summary>
      /// Application alert
      /// </summary>
      public static AlertCategoryEnum Application
      {
          get { return _Application; }
      }
      /// <summary>
      /// Security alert
      /// </summary>
      public static AlertCategoryEnum Security
      {
          get { return _Security; }
      }
      /// <summary>
      /// User alert
      /// </summary>
      public static AlertCategoryEnum User
      {
          get { return _User; }
      }

      #endregion

      #region Constructors
      public AlertCategoryEnum():base("AlertCategoryEnum")
      {}
      #endregion
      #region Public Members
      public override void SetEnum(short val)
      {
          ServerEnumHelper<AlertCategoryEnum, IAlertCategoryEnumBroker>.SetEnum(this, val);
      }
      static public List<AlertCategoryEnum> GetAll()
      {
          return ServerEnumHelper<AlertCategoryEnum, IAlertCategoryEnumBroker>.GetAll();
      }
      static public AlertCategoryEnum GetEnum(string lookup)
      {
          return ServerEnumHelper<AlertCategoryEnum, IAlertCategoryEnumBroker>.GetEnum(lookup);
      }
      #endregion
}
}
