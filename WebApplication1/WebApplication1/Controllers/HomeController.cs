using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Domain;
using WebApplication1.Domain.Entity;
using WebApplication1.Domain.ViewModels;
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
        return Results.NotFound(new { message = "Список пользователей пуст" });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> GetUser(int id)
    {
        var response = await _userService.GetUser(id);
        if (response.StatusCode == Domain.StatusCode.OK)
        {
            return Results.Json(response.Data);
        }
        return Results.NotFound(new { message = "Пользователь не найден" });
    }
    
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> DeleteUser(int id)
    {
        var response = await _userService.DeleteUser(id);
        if (response.StatusCode == Domain.StatusCode.OK)
        {
            return Results.Json(response.Data);
        }
        return Results.NotFound(new { message = "Пользователь не найден" });
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> CreateUser(Domain.ViewModels.UserViewModel model)
    {
        var response = await _userService.CreateUser(model);
        return Results.Json(response.Data);
    }
    
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IResult> UpdateUser(Domain.ViewModels.UserViewModel model)
    {
        var response = await _userService.UpdateUser(model.Id, model);
        if (response.StatusCode == Domain.StatusCode.OK)
        {
            return Results.Json(response.Data);
        }
        return Results.NotFound(new { message = "Пользователь не найден" });
    }

    [HttpPost]
    public JsonResult GetTypes()
    {
        var types = _userService.GetTypes();
        Console.WriteLine(types+":"+types.Data);
        return Json(types.Data);
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
public record class UserViewModel(int Id, string Name, string Password, Role Role);