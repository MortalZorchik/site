using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApplication1.Domain.Helpers;
using WebApplication1.Service.Implementations;

namespace WebApplication1.Controllers;

public class GoogleOAuthController : Controller
{
    private const string RedirectUrl = "https://localhost:7131/GoogleOAuth/Code";
    private const string YouTubeScope = "https://www.googleapis.com/auth/youtube";
    private const string PkceSessionKey = "codeVerifier";
    
    public IActionResult RedirectOnOAuthServer()
    {
        var codeVerifier = Guid.NewGuid().ToString();
        var codeChellange = Sha256Helper.ComputeHash(codeVerifier);

        HttpContext.Session.SetString(PkceSessionKey, codeVerifier);

        var url = GoogleOauthService.GenerateOAuthRequestUrl(YouTubeScope, RedirectUrl, codeChellange);
        return Redirect(url);
    }
    
    public async Task<ViewResult> CodeAsync(string code)
    {
        var codeVerifier = HttpContext.Session.GetString(PkceSessionKey);

        var tokenResult = await GoogleOauthService.ExchangeCodeOnTokenAsync(code, codeVerifier, RedirectUrl);
        
        var myChannelId = await YoutubeService.GetMyChannelIdAsync(tokenResult.AccessToken);

        var newDescription = "Новое описание!";
        await YoutubeService.UpdateChannelDescriptionAsync(tokenResult.AccessToken, myChannelId, newDescription);
        
        var r = await YoutubeService.GetProgrammersChannels(tokenResult.AccessToken);
        var refreshedTokenResult = await GoogleOauthService.RefreshTokenAsync(tokenResult.RefreshToken);
        return View(r);
    }
}