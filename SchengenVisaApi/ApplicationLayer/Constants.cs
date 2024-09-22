using System.Text.RegularExpressions;

namespace ApplicationLayer;

public static class Constants
{
    public readonly static Regex EnglishWordRegex = new("^[a-zA-Z']*$");

    public readonly static Regex EnglishPhraseRegex = new(@"^[a-zA-Z №0-9;,\-_+=#*']*$");

    public readonly static Regex PhoneNumRegex = new(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");
}
