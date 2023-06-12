using Newtonsoft.Json.Linq;
using WebApplication1.Domain.Helpers;

namespace WebApplication1.Service.Implementations;

public class YoutubeService
{
    private const string YoutubeApiEndpointChannels = "https://www.googleapis.com/youtube/v3/channels";
    private const string YoutudeApiEndpointSearch = "https://www.googleapis.com/youtube/v3/search";

    public static async Task<string> GetMyChannelIdAsync(string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "mine", "true" }
        };

        var response = await HttpClientHelper.SendGetRequest<dynamic>(YoutubeApiEndpointChannels, queryParams, accessToken);

        var channelId = response.items[0].id;
        return channelId;
    }

    public static async Task UpdateChannelDescriptionAsync(string accessToken, string channelId, string newDescription)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "part", "brandingSettings" }
        };

        var body = new
        {
            id = channelId,
            brandingSettings = new
            {
                channel = new
                {
                    description = newDescription
                }
            }
        };

        await HttpClientHelper.SendPutRequest(YoutubeApiEndpointChannels, queryParams, body, accessToken);
    }

    public static async Task<Dictionary<string, string>> GetProgrammersChannels(string accessToken)
    {
        var queryParams = new Dictionary<string, string>
        { 
            { "part", "snippet" },
            {"q", "programming"}
        };

        var response = await HttpClientHelper.SendGetRequest<dynamic>(YoutudeApiEndpointSearch, queryParams, accessToken);

        var responseDict = new Dictionary<string, string>();
        for (int i = 0; i < 5; i++)
        {
            responseDict.Add($"title{i}", (string)response.items[i].snippet.title);
            responseDict.Add($"description{i}", (string)response.items[i].snippet.description);
        }
        return responseDict;
    }
}