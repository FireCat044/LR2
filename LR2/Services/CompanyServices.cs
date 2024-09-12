using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using LR2.Models; 

public class CompanyService
{
    private readonly IConfiguration _configuration;

    public CompanyService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetCompanyWithMostEmployees()
    {
        var companies = new List<Company>();

        companies.AddRange(_configuration.GetSection("Companies").Get<List<Company>>());

        var xmlSection = _configuration.GetSection("Companies");
        foreach (var companySection in xmlSection.GetChildren())
        {
            companies.Add(new Company
            {
                Name = companySection.GetValue<string>("Name"),
                Employees = companySection.GetValue<int>("Employees")
            });
        }

        var microsoftEmployees = _configuration.GetSection("Microsoft").GetValue<int>("Employees");
        companies.Add(new Company { Name = "Microsoft", Employees = microsoftEmployees });
        var appleEmployees = _configuration.GetSection("Apple").GetValue<int>("Employees");
        companies.Add(new Company { Name = "Apple", Employees = appleEmployees });
        var googleEmployees = _configuration.GetSection("Google").GetValue<int>("Employees");
        companies.Add(new Company { Name = "Google", Employees = googleEmployees });

        var companyWithMostEmployees = companies.OrderByDescending(c => c.Employees).FirstOrDefault();

        return companyWithMostEmployees?.Name;
    }
}
