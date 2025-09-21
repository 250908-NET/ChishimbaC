namespace WebApi.EndPoints;

public static class Challenge3 {
    public static void MapNumberGamesEndpoints(this IEndpointRouteBuilder app) {
        app.MapGet("/numbers/fizzbuzz/{count}", (int count) => {
            string fizzBuzz = "";
            for(int i = 1; i <= count; i++){
                if(i % 3 == 0 && i % 5 == 0){
                    fizzBuzz += " fizzbuzz ";
                }
                else if (i % 5 == 0){
                    fizzBuzz += " buzz ";
                }
                else if (i % 3 == 0 ){
                    fizzBuzz += " fizz ";
                }
                else {
                    fizzBuzz += " " + i;
                }

            }
            return Results.Ok(new { operation = "fizzbuzz", result = fizzBuzz });
        });

           app.MapGet("/numbers/prime/{number}", (int number) => {
            bool isPrime = true;
            for(int i = 2; i < number; i++){
                if (number % i == 0){
                    isPrime = false;
                    break;
                }
            } 
            
            return Results.Ok(new { operation = "prime", result = isPrime });
        });

          app.MapGet("/numbers/fibonacci/{count}", (int count) => {
            int[] fibArray = new int[count];
            if(count == 1){
                fibArray[0] = 0;
            } 
            else {
                fibArray[0] = 0;
                fibArray[1] = 1;
                for(int i = 2;i < count; i++){
                    fibArray[i] = fibArray[i -2] + fibArray[i - 1]; 
            }}
            
            return Results.Ok(new { operation = "fibonacci", result = fibArray });
        });

        app.MapGet("/numbers/factors/{number}", (int number) => {
            List<int> factorsList = new List<int>();
            for(int i = 1; i <= number; i++){
                if(number % i == 0 ){
                    factorsList.Add(i);
                }
            }
            
            return Results.Ok(new { operation = "factors", result = factorsList});
        });



        // app.MapGet("/calculator/subtract/{a}/{b}", (double a, double b) => {
        //     return Results.Ok(new { operation = "subtract" ,result = a - b });
        // });

        // app.MapGet("/calculator/multiply/{a}/{b}", (double a, double b) => {
        //     return Results.Ok(new { operation = "multiply" , result = a * b });
        // });
        
        // app.MapGet("/calculator/divide/{a}/{b}", (double a, double b) => {
        //     if (b == 0) {
        //         return Results.BadRequest(new {error = "Cannot divide by zero"});
        //     }
            
        //     return Results.Ok(new { operation = "divide" , result = a / b });
        // });
    }
}