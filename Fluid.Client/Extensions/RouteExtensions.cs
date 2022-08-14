using Fluid.Shared.Requests;

namespace Fluid.Client.Extensions;

public static class RouteExtensions
{
    public static string ToPagedRoute(this string url, PagedRequest request)
    {
        url += $"?pageNumber={request.PageNumber}&pageSize={request.PageSize}&searchString={request.SearchString}&orderBy=";
        if (request.OrderBy?.Any() == true)
        {
            foreach (var orderByPart in request.OrderBy)
            {
                url += $"{orderByPart},";
            }
            url = url[..^1];
        }
        return url;
    }
    
    public static string ToPeriodRoute(this string url, DateTime fromDate, DateTime toDate)
    {
        return url.AddDateTimeToUrl("fromDateTicks", fromDate)
            .AddDateTimeToUrl("toDateTicks", toDate);
    }

    private static string AddDateTimeToUrl(this string url, string label, DateTime dateTime)
    {
        url += $"&{label}={dateTime.Ticks}";
        return url;
    }
}
