namespace WebApi.EndPoints;
using System;

public static class Challenge2 {
    public static void MapStringManipulatorEndpoints(this IEndpointRouteBuilder app) {
        app.MapGet("/text/reverse/{text}", (string text) => {
            // new char[] input = text.ToCharArray();
            return Results.Ok(new { operation = "reverse" , result = string.Concat(text.Reverse()) });

            //var reversed = new string(text.Reverse().ToArray());
            //return new {operation = "reverse", rev = reversed};
        });
        app.MapGet("/text/uppercase/{text}", (string text) =>{
            return Results.Ok(new { operation = "uppercase" , result = text.ToUpper()});
        });

        app.MapGet("/text/lowercase/{text}", (string text) =>
        {
         var lowercase = text.ToLower();
        return new { operation = "lowercase" , input = text, lowercase };
        });

         app.MapGet("/text/count/{text}", (string text) =>
        {
            var charCount = text.Length;
            string[] words = text.Split(new[]{' ', '\t', '\r', '\n'},StringSplitOptions.RemoveEmptyEntries);
            var wordCount = words.Length;
            int vowelCount = text.Count(c => "aeiouAEIOU".Contains(c));

         
        return new {operation = "count" , input = charCount , wordCount , vowelCount};
        });

         app.MapGet("/text/palindrome/{text}", (string text) =>
        {
             string cleanedWord = text.ToLower();
             bool isPalindrome =false;
            // char[] wordArray = cleanedWord.ToCharArray();
            // char[] reversedWordArray = Array.Reverse(wordArray);
            // string reversedWord = new string(reversedWordArray);
            int start =0;
            int end =cleanedWord.Length -1;

            while(start<end)

            {

                if(cleanedWord[start] != cleanedWord[end]) 
                {
                    isPalindrome =false;
                    
                } else{
                    
                    isPalindrome = true;

                }

                start++;
                end--;
            }

            return new { operation = "palindrome" , input = isPalindrome };
        });



        
        // app.MapGet("/calculator/divide/{a}/{b}", (double a, double b) => {
        //     if (b == 0) {
        //         return Results.BadRequest(new {error = "Cannot divide by zero"});
        //     }
            
        //     return Results.Ok(new { operation = "divide" , result = a / b });
        // });
    }
}