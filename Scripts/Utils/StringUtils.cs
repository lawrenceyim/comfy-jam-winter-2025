using System.Text.RegularExpressions;

public class StringUtils {
    public static string SplitPascalCase(string input) {
        return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
    }
}