namespace CanineMer
{
    public static class ExtensionMethods
    {
        public static string AddSpacesToPascalCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var newText = new System.Text.StringBuilder(text.Length * 2);
            newText.Append(text[0]);

            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) || char.IsDigit(text[i]))
                    newText.Append(' ');
                newText.Append(text[i]);
            }

            return newText.ToString();
        }
    }
}
