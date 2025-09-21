



using System;

namespace _6_FlowControl
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// This method gets a valid temperature between -40 asnd 135 inclusive from the user
        /// and returns the valid int. 
        /// </summary>
        /// <returns></returns>
        public static int GetValidTemperature()
        {

            throw new NotImplementedException($"GetValidTemperature() has not been implemented.");
        }

        /// <summary>
        /// This method has one int parameter
        /// It prints outdoor activity advice and temperature opinion to the console 
        /// based on 20 degree increments starting at -20 and ending at 135 
        /// n < -20, Console.Write("hella cold");
        /// -20 <= n < 0, Console.Write("pretty cold");
        ///  0 <= n < 20, Console.Write("cold");
        /// 20 <= n < 40, Console.Write("thawed out");
        /// 40 <= n < 60, Console.Write("feels like Autumn");
        /// 60 <= n < 80, Console.Write("perfect outdoor workout temperature");
        /// 80 <= n < 90, Console.Write("niiice");
        /// 90 <= n < 100, Console.Write("hella hot");
        /// 100 <= n < 135, Console.Write("hottest");
        /// </summary>
        /// <param name="temp"></param>
        public static void GiveActivityAdvice(int temp)
        {
            switch (temp)
            {
                case int n when n < -20:
                Console.WriteLine("hella cold");
                break;
                case int n when -20 <= n < 0:
                Console.WriteLine("pretty cold");
                break;
                case int n when 0 <= n < 20:
                Console.WriteLine("cold");
                break;
                case int n when 20 <= n < 40:
                Console.WriteLine("thawed out");
                break;
                case int n when 40 <= n < 60:
                Console.WriteLine("feels like Autumn");
                break;
                case int n when 60 <= n < 80:
                Console.WriteLine("perfect outdoor workout temperature");
                break;
                case int n when 80 <= n < 90:
                Console.WriteLine("niiice");
                break;
                case int n when 90 <= n < 100:
                Console.WriteLine("hella hot");
                break;
                case int n when 100 <= n < 135:
                Console.WriteLine("hottest");
                break;
                
            }
            throw new NotImplementedException($"GiveActivityAdvice() has not been implemented.");
        }

        /// <summary>
        /// This method gets a username and password from the user
        /// and stores that data in the global variables of the 
        /// names in the method.
        /// </summary>
        public static void Register()
        {
            //if not as parameters how am I getting the username and password from the user??
            new String userName = "";
            new String password = "";
            throw new NotImplementedException($"Register() has not been implemented.");
        }

        /// <summary>
        /// This method gets username and password from the user and
        /// compares them with the username and password names provided in Register().
        /// If the password and username match, the method returns true. 
        /// If they do not match, the user is reprompted for the username and password
        /// until the exact matches are inputted.
        /// </summary>
        /// <returns></returns>
        public static bool Login()
        {
            // Create a new username and password variable..  Compare them to the variables created in Register() and return true if they both match
            throw new NotImplementedException($"Login() has not been implemented.");
        }

        /// <summary>
        /// This method has one int parameter.
        /// It checks if the int is <=42, Console.WriteLine($"{temp} is too cold!");
        /// between 43 and 78 inclusive, Console.WriteLine($"{temp} is an ok temperature");
        /// or > 78, Console.WriteLine($"{temp} is too hot!");
        /// For each temperature range, a different advice is given. 
        /// </summary>
        /// <param name="temp"></param>
        public static void GetTemperatureTernary(int temp)
        {
            switch (temp){
                int n when n <= 42:
                Console.WriteLine($"{temp} is too cold!");
                break;
                int n when n >= 43 && n <= 78:
                Console.WriteLine($"{temp} is an ok temperature");
                break;
                int n when n > 78:
                Console.WriteLine($"{temp} is too hot!");
                break;
                
                
            }
            throw new NotImplementedException($"GetTemperatureTernary() has not been implemented.");
        }
    }//EoP
}//EoN
