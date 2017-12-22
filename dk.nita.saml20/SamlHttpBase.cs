#region Using

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

#endregion
  
namespace dk.nita.saml20
{
    /*
     * INSTRUCTIONS:
     * 1. Implement SamlHttpRequest
     * 2. Initialise SamlHttpRequest.Cookies
     * 3. Implement SamlHttpResponse
     * 4. Use values of SamlHttpResponse.Cookies after calling SAML code
     * 5. Handle SamlHttpContext.LoggedOut
     * 6. Implement SamlCache
     * 7. Set SamlRuntime.Cache
     */

    #region SamlHttpRequest

    public abstract class SamlHttpRequest
    {
        public abstract NameValueCollection QueryString { get; }
        public abstract NameValueCollection Params { get; }
        public abstract NameValueCollection Headers { get; }
        public abstract Stream InputStream { get; }
        public abstract string RawUrl { get; }
        public abstract string RequestType { get; }
        public abstract Uri Url { get; }
        public abstract string UserHostAddress { get; }

        public abstract SamlHttpCookieCollection Cookies { get; }
    }
    
    #endregion

    #region SamlHttpResponse

    public abstract class SamlHttpResponse
    {
        private SamlHttpCookieCollection cookies;

        protected SamlHttpResponse(SamlHttpRequest request)
        {
            cookies = new SamlHttpCookieCollection(request.Cookies);
        }

        public abstract string ContentType { get; set; }
        public abstract Stream OutputStream { get; }
        public abstract Encoding ContentEncoding { get; set; }
        public abstract void Redirect(string redirectUrl);
        public abstract void End();
        public abstract void AddHeader(string name, string value);
        public abstract void Write(string text);
        public abstract void Redirect(string redirectUrl, bool stopProcessing);

        public SamlHttpCookieCollection Cookies { get { return cookies; } }
    }
    
    #endregion

    #region SamlCache

    public abstract class SamlCache
    {
        public static readonly DateTime NoAbsoluteExpiration;
        public static readonly TimeSpan NoSlidingExpiration;

        public abstract object this[string name] { get; }

        public abstract void Insert(string key, object value, object dummy, DateTime absoluteExpiration, TimeSpan slidingExpiration);
        public abstract object Get(string p);
        public abstract void Remove(string p);
    }

    #endregion

    #region SamlRuntime

    public class SamlRuntime
    {
        private static SamlCache cache = null;

        public static SamlCache Cache
        {
            get
            {
                if (cache == null)
                    throw new NotImplementedException("SamlCache not initialized");
                return cache;
            }
            set
            {
                cache = value;
            }
        }
    }

    #endregion

    #region SamlHttpContext

    public class SamlHttpContext
    {
        private SamlHttpRequest request;
        private SamlHttpResponse response;
        private SamlCache cache;

        public SamlHttpContext(SamlHttpRequest request, SamlHttpResponse response, SamlCache cache)
        {
            this.request = request;
            this.response = response;
            this.cache = cache;

            Current = this;
        }

        public event EventHandler LoggedOut;

        [ThreadStatic]
        internal static SamlHttpContext Current;

        internal SamlHttpRequest Request { get { return request; } }
        internal SamlHttpResponse Response { get { return response; } }
        internal SamlCache Cache { get { return cache; } }

        internal void Logout()
        {
            if (LoggedOut == null)
                throw new NotImplementedException("SamlHttpContext.LoggedOut event not handled");
            LoggedOut(this, new EventArgs());
        }
    }
    
    #endregion

    #region SamlHttpCookie

    public class SamlHttpCookie
    {
        private string name;
        private string val;
        private bool secure;
        private bool httpOnly;

        public SamlHttpCookie(string name, string value)
        {
            this.name = name;
            this.val = value;
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        public string Value
        {
            get { return val; }
            set { this.val = value; }
        }

        public bool Secure
        {
            get { return secure; }
            set { this.secure = value; }
        }

        public bool HttpOnly
        {
            get { return httpOnly; }
            set { this.httpOnly = value; }
        }
    }
    
    #endregion

    #region SamlHttpCookieCollection

    public class SamlHttpCookieCollection : IEnumerable<SamlHttpCookie>
    {
        private Dictionary<string, SamlHttpCookie> cookies = new Dictionary<string, SamlHttpCookie>();
        private SamlHttpCookieCollection companion;

        public SamlHttpCookieCollection()
        {
        }

        public SamlHttpCookieCollection(SamlHttpCookieCollection companion)
        {
            this.companion = companion;
        }

        public SamlHttpCookie this[string name] 
        { 
            get 
            {
                SamlHttpCookie cookie = null;
                cookies.TryGetValue(name, out cookie);
                return cookie; 
            } 
        }

        internal void Remove(string name)
        {
            cookies.Remove(name);
            if (companion != null)
                companion.Remove(name);
        }

        public void Add(SamlHttpCookie cookie)
        {
            cookies[cookie.Name] = cookie;
            if (companion != null)
                companion.Add(cookie);
        }

        public IEnumerator<SamlHttpCookie> GetEnumerator()
        {
            return cookies.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return cookies.Values.GetEnumerator();
        }
    }
    
    #endregion
}
