using UnityEngine;

namespace Common.Utils
{
    public static class ColorHelper
    {
        public static Color HexToColor(string hex)
        {
          
            if (hex.StartsWith("#"))
            {
                hex = hex[1..];
            }
            
            if (hex.Length != 6 && hex.Length != 8)
            {
                Debug.LogError("Invalid HEX color format. Expected 6 or 8 characters.");
                return Color.white; 
            }

     
            var r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            var b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            var a = 255; 
            
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            }
            
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }
    }
}