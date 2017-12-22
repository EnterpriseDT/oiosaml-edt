using System;
using System.Collections.Generic;
using System.Text;

namespace dk.nita.saml20.protocol
{
    public class SamlException : Exception
    {
        public SamlException(string message)
            : base(message)
        {
        }

        public SamlException()
        {
        }
    }

    public class NoIdentityProviderException : SamlException
    {
        public NoIdentityProviderException()
        {
        }
    }
}
