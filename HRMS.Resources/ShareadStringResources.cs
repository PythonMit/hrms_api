using Microsoft.Extensions.Localization;

namespace HRMS.Resources
{
    public class ShareadStringResources : IAppResourceAccessor
    {
        private IStringLocalizer<ShareadStringResources> _localizer;
        public ShareadStringResources(IStringLocalizer<ShareadStringResources> localizer)
        {
            _localizer = localizer;
        }

        public string GetResource(string key)
        {
            var data = _localizer[key];
            return data;
        }
    }
}
