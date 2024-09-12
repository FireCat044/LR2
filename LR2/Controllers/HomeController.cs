using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly CompanyService _companyService;
    private readonly IConfiguration _configuration;

    public HomeController(CompanyService companyService, IConfiguration configuration)
    {
        _companyService = companyService;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        var company = _companyService.GetCompanyWithMostEmployees();

        ViewBag.Company = company;

        var name = _configuration["Name"];
        var age = _configuration["Age"];
        var location = _configuration["Location"];
        var occupation = _configuration["Occupation"];

        ViewBag.Name = name;
        ViewBag.Age = age;
        ViewBag.Location = location;
        ViewBag.Occupation = occupation;

        return View();
    }
}
