using Microsoft.AspNetCore.Mvc;

public class PricingFeature
{
    public string Name { get; set; }
    public bool Enabled { get; set; }
}

public class PricingPlan
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public string Description { get; set; }
    public List<PricingFeature> Features { get; set; }
}

[ApiController]
[Route("pricing-plans")]
public class PricingPlansController : ControllerBase
{
    private readonly List<PricingPlan> _pricingPlans = new List<PricingPlan>();

    public PricingPlansController()
    {
        _pricingPlans.Add(CreatePricingPlan("Free Plan", 0.00m, "Build and test using our set of products with up to 100 API requests", true, true, true, false, false, false, false));
        _pricingPlans.Add(CreatePricingPlan("Basic Plan", 249.00m, "Launch your project with unlimited requests and no contract minimums", true, true, true, true, true, false, false));
        _pricingPlans.Add(CreatePricingPlan("Premium Plan", 449.00m, "Get solutions, volume pricing, and dedicated support for your team", true, true, true, true, true, true, true));
    }

    private PricingPlan CreatePricingPlan(string name, decimal price, string description, params bool[] enabledFeatures)
    {
        var plan = new PricingPlan
        {
            Name = name,
            Price = price,
            Description = description,
            Features = new List<PricingFeature>()
        };

        var featureNames = new[] { "Transactions", "Auth", "Identity", "Investments", "Assets", "Liabilities", "Income" };
        for (int i = 0; i < enabledFeatures.Length && i < featureNames.Length; i++)
        {
            plan.Features.Add(new PricingFeature { Name = featureNames[i], Enabled = enabledFeatures[i] });
        }

        return plan;
    }

    [HttpGet]
    public IActionResult GetPricingPlans()
    {
        return Ok(_pricingPlans);
    }
}
