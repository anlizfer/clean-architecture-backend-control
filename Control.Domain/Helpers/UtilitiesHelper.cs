using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;

namespace Control.Domain.Helpers
{
    public static class UtilitiesHelper
    {
        public static string GetClientIPAddress(HttpContext context)
        {
            string ipAddress = string.Empty;
            IPAddress ip = context.Connection.RemoteIpAddress;
            if (ip != null)
            {
                if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    ip = Dns.GetHostEntry(ip).AddressList
                            .First(x => x.AddressFamily == AddressFamily.InterNetwork);
                }
                ipAddress = ip.ToString();
            }
            return ipAddress;
        }
        public static string GetClientId(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        }
        /// <summary>
        /// Para consumir está funcion donde se llame se debe instanciar en el 
        /// constructor el private readonly IHttpContextAccessor _httpContextAccessor;
        /// </summary>
        /// <param name="context">Contexto de IHttpContextAccessor</param>
        /// <returns></returns>
        public static string GetClientName(HttpContext context)
        {
            return context.User.Identity.Name;
        }



    }
}