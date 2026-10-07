
namespace UnitConverter.Services
{
    public class UnitConverterService
    {
    
    #region Length conversion
        static  readonly Dictionary<string,float> LENGTH_UNITS = new Dictionary<string, float>()
        {
          {"mm", 0.001f},
          {"cm", 0.01f},
          {"m", 1.0f},
          {"km", 1000.0f},
          {"in", 0.0254f},
          {"ft",0.3048f},
          {"yd",0.9144f},
          {"mi",1609.34f}
        };

        public static bool IsValidLengthUnit(string unit)
        {
            return LENGTH_UNITS.ContainsKey(unit);
        }

        public static string ValidLengthUnits()
        {
            string units = string.Empty;
            foreach(var unit in LENGTH_UNITS.Keys)
            {
                units += unit + " ";
            }

            return units;
        }

        public static float ConvertLengthUnit(string from, string to, float originalValue)
        {
            float valueInMeters = originalValue * LENGTH_UNITS[from];
            return valueInMeters / LENGTH_UNITS[to];
        }
#endregion

    #region  Weight conversion
        static readonly Dictionary<string, float> WEIGHT_UNITS = new Dictionary<string, float>()
        {
            {"mg",0.001f},
            {"g",1.0f},
            {"kg",1_000.0f},
            {"oz",28.3495f},
            {"lb",453.592f},
        };

        public static bool IsValidWeightUnit(string unit)
        {
            return WEIGHT_UNITS.ContainsKey(unit);
        }

        public static string ValidWeightUnits()
        {
            string units = string.Empty;
            foreach(var unit in WEIGHT_UNITS.Keys)
            {
                units += unit + " ";
            }

            return units;
        }

        public static float ConvertWeightUnit(string from, string to, float originalValue)
        {
            float valueInGrams = originalValue * WEIGHT_UNITS[from];
            return valueInGrams / WEIGHT_UNITS[to];
        } 
    #endregion

    #region Temperature conversion
        static readonly List<string> TEMPERATURE_UNITS = new List<string>(){"C","F","K"};
        public static bool IsValidTemperatureUnit(string temperatureUnit)
        {
            return TEMPERATURE_UNITS.Contains(temperatureUnit);
        }

        public static string ValidTemperatureUnits()
        {
            string units = string.Empty;
            foreach(var unit in TEMPERATURE_UNITS)
            {
                units += unit + " ";
            }

            return units;
        }

        public static float ConvertTemperatureUnits(string from, string to, float originalValue)
        {
            float convertedValue = 0;

            switch (from)
            {
                case "C":
                    switch (to)
                    {
                        case "C":
                            convertedValue = originalValue;
                            break;
                        case "F":
                            convertedValue = (originalValue * 1.8f) + 32;
                            break;
                        case "K":
                            convertedValue += 273.15f;
                            break;
                    }
                    break;
                case "F":
                    switch (to)
                    {
                        case "C":
                            convertedValue = (originalValue - 32) * 1.8f;
                            break;
                        case "F":
                            convertedValue = originalValue;
                            break;
                        case "K":
                            convertedValue = (originalValue - 32) * 1.8f + 273.15f;
                            break;
                    }
                    break;
                case "K":
                    switch (to)
                    {
                        case "C":
                            convertedValue = originalValue - 273.15f;
                            break;
                        case "F":
                            convertedValue = (originalValue - 273.15f) * 1.8f + 32;
                            break;
                        case "K":
                            convertedValue = originalValue;
                            break;
                    }
                    break;
                default:
                    convertedValue = 0;
                    break;
            }

            return convertedValue;
        }
        #endregion
    }
}