
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Web;

namespace GroupHub.Core
{
    public static class Helper
    {

        public static string GenerateSecretKey(int bytes)
        {

            // Create a random key using a random number generator. This would be the
            //  secret key shared by sender and receiver.
            byte[] secretkeybytes = new Byte[bytes];
            string secretkey = null;
            Crypto crypto = new Crypto();
            //RNGCryptoServiceProvider is an implementation of a random number generator.
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                // The array is now filled with cryptographically strong random bytes.
                rng.GetBytes(secretkeybytes);
                secretkey = HttpServerUtility.UrlTokenEncode(secretkeybytes);

            }

            return secretkey;
        }


        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }


    }
}
