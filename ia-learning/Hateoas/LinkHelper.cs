using Microsoft.AspNetCore.Mvc;

namespace ia_learning.Hateoas
{
    public static class LinkHelper
    {
        public static object WithLinks<T>(T resource, IUrlHelper url, string getRoute, string updateRoute, string deleteRoute, int id)
        {
            return new
            {
                data = resource,
                _links = new
                {
                    self = url.Link(getRoute, new { id }),
                    update = url.Link(updateRoute, new { id }),
                    delete = url.Link(deleteRoute, new { id })
                }
            };
        }

        public static IEnumerable<object> WithLinks<T>(IEnumerable<T> resources, IUrlHelper url,
            string getRoute, string updateRoute, string deleteRoute, Func<T, int> selector)
        {
            return resources.Select(r =>
                WithLinks(r, url, getRoute, updateRoute, deleteRoute, selector(r))
            );
        }
    }
}
