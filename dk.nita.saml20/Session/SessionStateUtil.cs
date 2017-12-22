﻿using System;
//using System.Web;

namespace dk.nita.saml20.session
{
    /// <summary>
    /// Contains the logic about how to create and store the SessionId
    /// </summary>
    class SessionStateUtil
    {
        internal static Guid? SessionId
        {
            get
            {
                SamlHttpCookie httpCookie = SamlHttpContext.Current.Request.Cookies[SessionConstants.SessionCookieName];
                if (httpCookie != null)
                    return new Guid(httpCookie.Value);

                return null;
            }
        }

        internal static void CreateSessionId(Guid sessionId)
        {
            SamlHttpContext.Current.Request.Cookies.Remove(SessionConstants.SessionCookieName); // Remove cookie from request when creating a new session id. This is necessary because adding a cookie with the same name does not override cookies in the request.
            SamlHttpCookie httpCookie = new SamlHttpCookie(SessionConstants.SessionCookieName, sessionId.ToString());
            httpCookie.Secure = true;
            httpCookie.HttpOnly = true;
            SamlHttpContext.Current.Response.Cookies.Add(httpCookie); // When a cookie is added to the response it is automatically added to the request. Thus, SessionId is available immeditly when reading cookies from the request.
        }
    }
}