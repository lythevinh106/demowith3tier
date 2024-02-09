using MydemoFirst.Errors.Exceptions;

namespace MydemoFirst.View.MailLayout
{
    public class MailLayoutHelper
    {

        public static string GetMailLayout1(string name)
        {
            var contentsPath = Directory.GetCurrentDirectory() + $"/View/MailLayout/MailLayout1.html";


            string fileContent = ReadAllTextMethod(contentsPath);

            fileContent = fileContent.Replace("{{$name}}", name);

            return fileContent;

        }

        private static string ReadAllTextMethod(string contentsPath)
        {
            if (System.IO.File.Exists(contentsPath))
            {
                string fileContent = System.IO.File.ReadAllText(contentsPath);

                return fileContent;

            }

            throw new ExceptionNotFound("html file not exist");



        }
    }
}
