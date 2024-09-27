using System.Globalization;

namespace EcommerceAspNet.Api.Middlewares
{
    public class CultureInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureInfoMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var acceptedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);
            var requestLanguage = context.Request.Headers.AcceptLanguage;
            var cultureLanguage = new CultureInfo("us");

            if(string.IsNullOrEmpty(requestLanguage) == false && acceptedLanguages.Any(d => d.Equals(requestLanguage)))
            {
                cultureLanguage = new CultureInfo(requestLanguage!);
            }

            CultureInfo.CurrentUICulture = cultureLanguage;
            CultureInfo.CurrentCulture = cultureLanguage;

            await _next(context);
        }
    }
}
