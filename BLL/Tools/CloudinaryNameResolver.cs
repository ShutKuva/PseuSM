using BLL.Abstractions.Tools;
using BLL.Parameters;
using Tools.Abstractions;

namespace BLL.Tools
{
    public class CloudinaryNameResolver : IParamsResolver<string, CloudinaryParameters>
    {
        private readonly ITimeService _timeService;

        public CloudinaryNameResolver(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public async Task<string> CreateParamsAsync(CloudinaryParameters parameters)
        {
            DateTime now = await _timeService.GetCurrentDateTime();

            var guid = new Guid();

            return $"{(parameters.User == null ? parameters.Post == null ? "placeholder" : $"{parameters.Post.Id}" : $"{parameters.User.Id}_{parameters.User.Login}")}_{now.Year}_{now.Day}_{now.Minute}_{guid}";
        }
    }
}
