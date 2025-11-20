namespace ia_learning.Hateoas
{
    public class HateoasHelper
    {
        private readonly string _baseUrl;

        public HateoasHelper(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
        }

        public LinkDto Link(string endpoint, string rel, string method)
        {
            return new LinkDto($"{_baseUrl}/{endpoint}", rel, method);
        }
    }
}
