//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DairyWeb.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Common {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Common() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DairyWeb.Resource.Common", typeof(Common).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Notes.
        /// </summary>
        public static string Notes {
            get {
                return ResourceManager.GetString("Notes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Openning Balance.
        /// </summary>
        public static string OpenningBalance {
            get {
                return ResourceManager.GetString("OpenningBalance", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Out Standing.
        /// </summary>
        public static string OutStanding {
            get {
                return ResourceManager.GetString("OutStanding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Paid Amount.
        /// </summary>
        public static string PaidAmount {
            get {
                return ResourceManager.GetString("PaidAmount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Purchased Amount.
        /// </summary>
        public static string PurchasedAmount {
            get {
                return ResourceManager.GetString("PurchasedAmount", resourceCulture);
            }
        }
    }
}
