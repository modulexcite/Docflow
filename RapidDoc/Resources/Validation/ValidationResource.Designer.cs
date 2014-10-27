﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ValidationRes {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ValidationResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RapidDoc.Resources.Validation.ValidationResource", typeof(ValidationResource).Assembly);
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
        ///   Looks up a localized string similar to Не найден сотрудник у пользователя {0}.
        /// </summary>
        public static string ErrorEmplNotFound {
            get {
                return ResourceManager.GetString("ErrorEmplNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле должно быть не длиннее {1} символов.
        /// </summary>
        public static string ErrorFieldisLong {
            get {
                return ResourceManager.GetString("ErrorFieldisLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} должно быть заполнено.
        /// </summary>
        public static string ErrorFieldisNull {
            get {
                return ResourceManager.GetString("ErrorFieldisNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} должно быть не менее {2} и не более {1} символов.
        /// </summary>
        public static string ErrorFieldSize {
            get {
                return ResourceManager.GetString("ErrorFieldSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неправильный E-mail.
        /// </summary>
        public static string ErrorInvalidEmail {
            get {
                return ResourceManager.GetString("ErrorInvalidEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Минимальная длина пароля 8 символов, должен содержать цифры, заглавные и строчные буквы алфавита.
        /// </summary>
        public static string ErrorInvalidPassword {
            get {
                return ResourceManager.GetString("ErrorInvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to В документ может быть прикреплен только файл с расширение {0}, Вы добавили файл: {1}.
        /// </summary>
        public static string ErrorMandatoryFileTypes {
            get {
                return ResourceManager.GetString("ErrorMandatoryFileTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не прикреплено вложение! В документе должен(-но) быть {0} файл(-а), Вы добавили {1} файл(-а).
        /// </summary>
        public static string ErrorMandatoryNumberFiles {
            get {
                return ResourceManager.GetString("ErrorMandatoryNumberFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пароли не совпадают.
        /// </summary>
        public static string ErrorPasswordsDoNotMatch {
            get {
                return ResourceManager.GetString("ErrorPasswordsDoNotMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Необходимо указать маршрут перед утверждением.
        /// </summary>
        public static string ErrorProcessXAML {
            get {
                return ResourceManager.GetString("ErrorProcessXAML", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Размер номерной серии должен быть в пределах от 3 до 10 включительно.
        /// </summary>
        public static string ErrorRangeNumberSeqSize {
            get {
                return ResourceManager.GetString("ErrorRangeNumberSeqSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не правильно указан порт.
        /// </summary>
        public static string ErrorRangePort {
            get {
                return ResourceManager.GetString("ErrorRangePort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Документ на согласовании или исполнении нельзя отправить в архив.
        /// </summary>
        public static string ErrorToArchiveDocumentState {
            get {
                return ResourceManager.GetString("ErrorToArchiveDocumentState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не найден пользователь {0}.
        /// </summary>
        public static string ErrorUserNotFound {
            get {
                return ResourceManager.GetString("ErrorUserNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Неправильный пароль или логин.
        /// </summary>
        public static string ErrorUserOrPassword {
            get {
                return ResourceManager.GetString("ErrorUserOrPassword", resourceCulture);
            }
        }
    }
}
