namespace TemplateEngine.Core.Templaytor
{
    public class CustomTemplator : ICustomTemplator
    {

        // template = <h1>name</h1>
        // name = Тимерхан

        public string GetHtmlByTemplate(string template, string name)
        {
            return template.Replace("{name}", name);
        }


        public string GetHtmlByTemplate<T>(string template, T obj)
        {
            // Логика: проходится по всем свойствам T и проверять их наличие в виде переменной в template. 
            // Если нашлось, произвоить замену.

            return template;
        }
    }
}
