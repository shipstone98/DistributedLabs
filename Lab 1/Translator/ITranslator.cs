using System;

namespace Translator
{
    public interface ITranslator
    {
        String GetName();
        String GetStudentId();
        String Translate(String source);
        String Translate(String[] source);
    }
}
