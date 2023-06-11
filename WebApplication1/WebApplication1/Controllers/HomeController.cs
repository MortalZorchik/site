using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Service.Interfaces;


namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    /*[Authorize(Roles = "Admin")]*/
    public async Task<IResult> GetUsers()
    {
        var response = await _userService.GetAllUsers();
        if (response.StatusCode == Domain.StatusCode.OK)
        {
            return Results.Json(response.Data);
        }
        return Results.Json(response.Data);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}