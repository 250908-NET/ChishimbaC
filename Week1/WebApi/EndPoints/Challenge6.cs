namespace WebApi.EndPoints;

public static class Challenge6
{
    enum TemperatureUnit
    {
        Kelvin,
        Celsius,
        Fahrenheit,
        Rankine,
    }

    static string ToString(this TemperatureUnit unit)
    {
        return unit switch
        {
            TemperatureUnit.Kelvin => "Kelvin",
            TemperatureUnit.Celsius => "Celsius",
            TemperatureUnit.Fahrenheit => "Fahrenheit",
            TemperatureUnit.Rankine => "Rankine",
            _ => "",
        };
    }

    static TemperatureUnit ParseUnit(string input)
    {
        return input.Trim().ToLower() switch
        {
            "k" or "kelvin" => TemperatureUnit.Kelvin,
            "c" or "celsius" => TemperatureUnit.Celsius,
            "f" or "fahrenheit" => TemperatureUnit.Fahrenheit,
            "r" or "rankine" => TemperatureUnit.Celsius,
            _ => throw new ArgumentException("Invalid temperature unit.")
        };
    }

    static double ToKelvin(double temperature, TemperatureUnit unit)
    {
        return unit switch
        {
            TemperatureUnit.Kelvin => temperature,
            TemperatureUnit.Celsius => temperature + 273.15,
            TemperatureUnit.Fahrenheit => (temperature - 32.0) * 5.0 / 9.0 + 273.15,
            TemperatureUnit.Rankine => temperature * 5.0 / 9.0,
            _ => throw new ArgumentException("Invalid temperature unit.")
        };
    }

    static double FromKelvin(double temperature, TemperatureUnit unit)
    {
        return unit switch
        {
            TemperatureUnit.Kelvin => temperature,
            TemperatureUnit.Celsius => temperature - 273.15,
            TemperatureUnit.Fahrenheit => (temperature - 273.15) * 9.0 / 5.0 + 32.0,
            TemperatureUnit.Rankine => temperature * 9.0 / 5.0,
            _ => throw new ArgumentException("Invalid temperature unit.")
        };
    }

    public static void MapTemperatureEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/temp/{from}-to-{to}/{temperature}", (string from, string to, double temperature) =>
        {
            try
            {
                TemperatureUnit fromUnit = ParseUnit(from);
                TemperatureUnit toUnit = ParseUnit(to);

                double result = 0.0;
                if (fromUnit != TemperatureUnit.Kelvin)
                    result = ToKelvin(temperature, fromUnit);
                if (toUnit != TemperatureUnit.Kelvin)
                    result = FromKelvin(result, toUnit);

                return Results.Ok(new { temperature = result, unit = toUnit.ToString() });
            }
            catch (Exception e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        });

        app.MapGet("/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}", (double temp1, string unit1, double temp2, string unit2) =>
        {
            try
            {
                TemperatureUnit unit1Enum = ParseUnit(unit1);
                TemperatureUnit unit2Enum = ParseUnit(unit2);

                double unit1Kelvin = ToKelvin(temp1, unit1Enum);
                double unit2Kelvin = ToKelvin(temp2, unit2Enum);

                return Results.Ok((unit2Kelvin - unit1Kelvin) switch
                {
                    < 0 => new { result = -1, relationship = "less than" },   // temp1 > temp2
                    0 => new { result = 0, relationship = "equal to" },     // temp1 == temp2
                    > 0 => new { result = 1, relationship = "greater than" }, // temp1 < temp2
                    _ => throw new ArgumentException("Invalid temperature.")
                });
            }
            catch (Exception e)
            {
                return Results.BadRequest(new { error = e.Message });
            }
        });
    }
}
