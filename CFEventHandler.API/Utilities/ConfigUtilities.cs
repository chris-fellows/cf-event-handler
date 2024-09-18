using CFUtilities.Encryption;
using System.Text;

namespace CFEventHandler.API.Utilities
{
    /// <summary>
    /// Config utilities
    /// </summary>
    internal class ConfigUtilities
    {
        private const string Key = "k8Bv29sHy0aYvAq56a";    // TODO: Move elsewhere (Environment variable etc)
        private const string EncryptedPrefix = "\tEncrypted\t";

        /// <summary>
        /// Decrypts setting if encrypted else returns original setting
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecryptSetting(string value)
        {
            return value.StartsWith(EncryptedPrefix) ?
                    Encoding.UTF8.GetString(TripleDESEncryption.DecryptByteArray(System.Convert.FromBase64String(value.Substring(EncryptedPrefix.Length)), Key)) :
                    value;        
        }

        ///// <summary>
        ///// Encrypts setting
        ///// </summary>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static string EncyptSetting(string value)
        //{
        //    return value.StartsWith(EncryptedPrefix) ?
        //            value :  // Already encrypted
        //            EncryptedPrefix + System.Convert.ToBase64String(TripleDESEncryption.EncryptByteArray(Encoding.UTF8.GetBytes(value), Key));
        //}
    }
}
