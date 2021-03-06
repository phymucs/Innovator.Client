﻿#if HTTPSYNC
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Innovator.Client
{
  internal class HttpResponseMsg : HttpResponseMessage, IHttpResponse
  {
    private Dictionary<string, string> _headers;

    public Stream AsStream { get { return ((ResponseContent)Content).Stream; } }

    IDictionary<string, string> IHttpResponse.Headers { get { return _headers; } }

    public HttpResponseMsg(HttpStatusCode code) : base(code) { }

    public void FlushHeaders()
    {
      _headers = new Dictionary<string, string>();
      foreach (var header in Headers)
      {
        _headers[header.Key] = header.Value.GroupConcat(", ");
      }
      foreach (var header in Content.Headers)
      {
        _headers[header.Key] = header.Value.GroupConcat(", ");
      }
    }
  }
}
#endif
