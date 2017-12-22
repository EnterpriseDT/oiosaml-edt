//using System.Web;
using dk.nita.saml20.protocol;


namespace dk.nita.saml20.Actions
{
    /// <summary>
    /// An implementation of the IAction interface can be called during login and logoff of the 
    /// SAML Connector framework in order to perform a specific action.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Action performed during login.
        /// </summary>
        /// <param name="handler">The handler initiating the call.</param>
        /// <param name="context">The current http context.</param>
        /// <param name="assertion">The saml assertion of the currently logged in user.</param>
        void LoginAction(AbstractEndpointHandler handler, SamlHttpContext context, Saml20Assertion assertion);

        /// <summary>
        /// Action performed during logout.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="context">The context.</param>
        /// <param name="IdPInitiated">During IdP initiated logout some actions such as redirecting should not be performed</param>
        void LogoutAction(AbstractEndpointHandler handler, SamlHttpContext context, bool IdPInitiated);

        /// <summary>
        /// Action performed during SOAP logout. It is still necessary to check on each HTTP request whether or not the user has been logged out because it is not possible to log out the user from the system at the time this method is called.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="context">The context.</param>
        /// <param name="userId">The user id which the IdP requested to log out.</param>
        void SoapLogoutAction(AbstractEndpointHandler handler, SamlHttpContext context, string userId);

        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
    }
}
