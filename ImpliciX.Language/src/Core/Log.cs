using System;
namespace ImpliciX.Language.Core
{
  public static class Log
  {
    public static ILog Logger { get; set; }

    static Log()
    {
      Logger = new DefaultLogger();
    }

    public static void Verbose(string messageTemplate)
    {
      Logger.Verbose(messageTemplate);
    }

    public static void Verbose(string messageTemplate, params object[] propertyValues)
    {
      Logger.Verbose(messageTemplate, propertyValues);
    }

    public static void Debug(string messageTemplate)
    {
      Logger.Debug(messageTemplate);
    }

    public static void Debug(string messageTemplate, params object[] propertyValues)
    {
      Logger.Debug(messageTemplate, propertyValues);
    }

    public static void Information(string messageTemplate)
    {
      Logger.Information(messageTemplate);
    }

    public static void Information(string messageTemplate, params object[] propertyValues)
    {
      Logger.Information(messageTemplate, propertyValues);
    }

    public static void Warning(string messageTemplate)
    {
      Logger.Warning(messageTemplate);
    }

    public static void Warning(string messageTemplate, params object[] propertyValues)
    {
      Logger.Warning(messageTemplate, propertyValues);
    }

    public static void Error(string messageTemplate)
    {
      Logger.Error(messageTemplate);
    }

    public static void Error(string messageTemplate, params object[] propertyValues)
    {
      Logger.Error(messageTemplate, propertyValues);
    }

    public static void Error(Exception exception, string messageTemplate)
    {
      Logger.Error(exception, messageTemplate);
    }

    public static void Error(Exception exception, string messageTemplate, params object[] propertyValues)
    {
      Logger.Error(exception, messageTemplate, propertyValues);
    }

    public static void Fatal(string messageTemplate)
    {
      Logger.Fatal(messageTemplate);
    }

    public static void Fatal(string messageTemplate, params object[] propertyValues)
    {
      Logger.Fatal(messageTemplate, propertyValues);
    }

    public static void Fatal(Exception exception, string messageTemplate)
    {
      Logger.Fatal(exception, messageTemplate);
    }

    public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
    {
      Logger.Fatal(exception, messageTemplate, propertyValues);
    }

    class DefaultLogger : ILog
    {
      private void Default() => Console.WriteLine("No logger defined for language");
      public void Verbose(string message) => Default();
      public void Verbose(string messageTemplate, params object[] propertyValues) => Default();
      public void Debug(string message) => Default();
      public void Debug(string messageTemplate, params object[] propertyValues) => Default();
      public void Information(string message) => Default();
      public void Information(string messageTemplate, params object[] propertyValues) => Default();
      public void Warning(string message) => Default();
      public void Warning(string messageTemplate, params object[] propertyValues) => Default();
      public void Error(string message) => Default();
      public void Error(string messageTemplate, params object[] propertyValues) => Default();
      public void Error(Exception exception, string message) => Default();
      public void Error(Exception exception, string messageTemplate, params object[] propertyValues) => Default();
      public void Fatal(string message) => Default();
      public void Fatal(string messageTemplate, params object[] propertyValues) => Default();
      public void Fatal(Exception exception, string message) => Default();
      public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues) => Default();
    }
  }
}