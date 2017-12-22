# oiosaml-edt
EnterpriseDT's modified version of [OIOSAML.NET 1.7.13](https://www.nuget.org/packages/dk.nita.saml20/1.7.13).

# What is this?
[CompleteFTP](https://enterprisedt.com/products/completeftp) uses OIOSAML.NET for its SAML Service Provider functionality.  OIOSAML.NET is released under the [Mozilla Public License version 1.1](https://www.mozilla.org/en-US/MPL/1.1/).  This license requires users to publish any modifications made to the source code.  EnterpriseDT is publishing its modifications in this repository.

# List of Changes
The following changes were made to [OIOSAML.NET 1.7.13](https://www.nuget.org/packages/dk.nita.saml20/1.7.13):
* Removed unused classes
* Store actions in a static List so that the default actions can be managed programmatically.
* Reduced dependence on System.Web classes by:
  * Introducing SamlHttpContext and using it in place of HttpContext.
  * Introducing SamlHttpCookie and using it in place of HttpCookie.
* Removed references to Windows authentication
* Stop reading SP certificate from Windows certificate store - instead it's supplied programmatically
* Stop trying to read IDP metadata from files - instead it's supplied programmatically
* Make singletons in ConfigurationInstance ThreadStatic to support multithreading.
* Replace usage of Windows Trace with an interface (ITrace) to allow integration with external logging system
* Added code to allow validation of IDP certificates against Windows Certificate Store to be disabled programmatically
