﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MatrixPACS.ImageServer.Web.Common {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class SR {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SR() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MatrixPACS.ImageServer.Web.Common.SR", typeof(SR).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 There are images to be reconciled for this study 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_NeedReconcile {
            get {
                return ResourceManager.GetString("ActionNotAllowed_NeedReconcile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The study is stored on a research partition. 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_ResearchPartition {
            get {
                return ResourceManager.GetString("ActionNotAllowed_ResearchPartition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 There is some pending work queue item(s) for this study. 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyHasPendingWorkQueue {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyHasPendingWorkQueue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 This study is being archived 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyIsBeingArchived {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyIsBeingArchived", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Study is being processed. 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyIsBeingProcessing {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyIsBeingProcessing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Study has been locked. 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyIsLocked {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyIsLocked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Study has been archived as lossless but is currently in lossy compression state. It must be restored first. 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyIsLossyOnline {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyIsLossyOnline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Study is nearline. 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyIsNearline {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyIsNearline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Study has not been archived 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyIsNotArchived {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyIsNotArchived", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Study does not exist. 的本地化字符串。
        /// </summary>
        internal static string ActionNotAllowed_StudyNotFound {
            get {
                return ResourceManager.GetString("ActionNotAllowed_StudyNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The current session is no longer valid. 的本地化字符串。
        /// </summary>
        internal static string MessageCurrentSessionNoLongerValid {
            get {
                return ResourceManager.GetString("MessageCurrentSessionNoLongerValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 ASAP 的本地化字符串。
        /// </summary>
        internal static string Priority_Asap {
            get {
                return ResourceManager.GetString("Priority_Asap", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Callback 的本地化字符串。
        /// </summary>
        internal static string Priority_Callback {
            get {
                return ResourceManager.GetString("Priority_Callback", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Pre-Op 的本地化字符串。
        /// </summary>
        internal static string Priority_PreOp {
            get {
                return ResourceManager.GetString("Priority_PreOp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Routine 的本地化字符串。
        /// </summary>
        internal static string Priority_Routine {
            get {
                return ResourceManager.GetString("Priority_Routine", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Stat 的本地化字符串。
        /// </summary>
        internal static string Priority_Stat {
            get {
                return ResourceManager.GetString("Priority_Stat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Timing 的本地化字符串。
        /// </summary>
        internal static string Priority_Timing {
            get {
                return ResourceManager.GetString("Priority_Timing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Web (@{0},{1} {2}, User:{3}) 的本地化字符串。
        /// </summary>
        internal static string WebGUILogHeader {
            get {
                return ResourceManager.GetString("WebGUILogHeader", resourceCulture);
            }
        }
    }
}
