using System;

namespace Translator.Server
{
    public class Translator: MarshalByRefObject
    {
        public String Translate(String englishString) => String.IsNullOrWhiteSpace(englishString) ? "" : Translator.Translate(englishString.Split(' '));

        public String Translate(String[] englishString)
        {
            if (englishString is null)
            {
                throw new ArgumentNullException(nameof (englishString));
            }

            String result = "";

            foreach (String word in englishString)
            {
                if (String.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                result += word.Substring(1);
                result += word.Substring(0, 1) + "ay ";
            }

            return result.Trim();
        }
    }
}
