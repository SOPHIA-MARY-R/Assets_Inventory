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
}
