namespace WebApi.EndPoints;

public static class Challenge1 {
    public static void MapCalculatorEndpoints1(this IEndpointRouteBuilder app) {
        app.MapGet("/calculator/add/{a}/{b}", (double a, double b) => {
            return Results.Ok(new { operation = "add", result = a + b });
        });

        app.MapGet("/calculator/subtract/{a}/{b}", (double a, double b) => {
            return Results.Ok(new { operation = "subtract" ,result = a - b });
        });

        app.MapGet("/calculator/multiply/{a}/{b}", (double a, double b) => {
            return Results.Ok(new { operation = "multiply" , result = a * b });
        });
        
        app.MapGet("/calculator/divide/{a}/{b}", (double a, double b) => {
            if (b == 0) {
                return Results.BadRequest(new {error = "Cannot divide by zero"});
            }
            
            return Results.Ok(new { operation = "divide" , result = a / b });
        });
    }
}
