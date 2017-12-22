using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using dk.nita.saml20.Properties;

namespace dk.nita.saml20.config
{
    /// <summary>
    /// Common implementation of X509 certificate references used in configuration files. 
    /// Specializations are free to provide the xml namespace that fit the best (ie the namespace of the containing element)
    /// </summary>
    public class Certificate
    {
        private X509Certificate2 certificate;

        /// <summary>
        /// Opens the certificate from its store.
        /// </summary>
        /// <returns></returns>
        public X509Certificate2 GetCertificate()
        {
            return certificate;
            //X509Store store = new X509Store( storeName, storeLocation);
            //try
            //{
            //    store.Open(OpenFlags.ReadOnly);
            //    X509Certificate2Collection found = store.Certificates.Find( x509FindType, findValue, validOnly);
            //    if (found.Count == 0)
            //        throw new ConfigurationErrorsException(ResourcesEx.CertificateNotFoundFormat(findValue) );
            //    if (found.Count > 1)
            //        throw new ConfigurationErrorsException(ResourcesEx.CertificateMoreThanOneFoundFormat(findValue) );
            //    return found[0];
            //}
            //finally
            //{
            //    store.Close();
            //}
        }

        public void SetCertificate(X509Certificate2 cert)
        {
            certificate = cert;
        }

        /// <summary>
        /// Find value
        /// </summary>
        [XmlAttribute]
        public string findValue;

        /// <summary>
        /// Store location
        /// </summary>
        [XmlAttribute]
        public StoreLocation storeLocation;

        /// <summary>
        /// Store name
        /// </summary>
        [XmlAttribute]
        public StoreName storeName;

        /// <summary>
        /// find type
        /// </summary>
        [XmlAttribute]
        public X509FindType x509FindType;

        /// <summary>
        /// Determines if only valid certificates are found
        /// </summary>
        [XmlAttribute]
        public bool validOnly = false;
    }
}