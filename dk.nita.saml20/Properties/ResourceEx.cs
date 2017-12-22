using System;
using System.Collections.Generic;
using System.Text;

namespace dk.nita.saml20.Properties
{
    class ResourcesEx
    {
        private static string FormatResource(string resourceName, params object[] arguments)
        {
            return string.Format(Resources.ResourceManager.GetString(resourceName), arguments);
        }

        public static string MetadataLocationNotFoundFormat(params object[] arguments)
        {
            return FormatResource(Resources.MetadataLocationNotFound, arguments);
        }

        public static string InvalidWellformedAbsoluteUriStringFormat(params object[] arguments)
        {
            return FormatResource(Resources.InvalidWellformedAbsoluteUriString, arguments);
        }

        public static string UnknownEncodingFormat(params object[] arguments)
        {
            return FormatResource(Resources.UnknownEncoding, arguments);
        }

        public static string UnsupportedRequestTypeFormat(params object[] arguments)
        {
            return FormatResource(Resources.UnsupportedRequestType, arguments);
        }

        public static string CertificateNotFoundFormat(params object[] arguments)
        {
            return FormatResource(Resources.CertificateNotFound, arguments);
        }

        public static string CertificateMoreThanOneFoundFormat(params object[] arguments)
        {
            return FormatResource(Resources.CertificateMoreThanOneFound, arguments);
        }

        public static string IdPMismatchBetweenRequestAndSessionFormat(params object[] arguments)
        {
            return FormatResource(Resources.IdPMismatchBetweenRequestAndSession, arguments);
        }
    }
}
