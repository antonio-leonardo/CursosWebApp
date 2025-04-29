namespace Cursos.CrossCutting
{
    public class LoggingBase
    {
        protected readonly Logging Log = LoggingSingleton.GetLogging();
    }
}