using System;
namespace ImpliciX.Language.Core
{
  public interface ILog
  {
    void Verbose(string message);
    void Verbose(string messageTemplate, params object[] propertyValues);
    void Debug(string message);
    void Debug(string messageTemplate, params object[] propertyValues);
    void Information(string message);
    void Information(string messageTemplate, params object[] propertyValues);
    void Warning(string message);
    void Warning(string messageTemplate, params object[] propertyValues);
    void Error(string message);
    void Error(string messageTemplate, params object[] propertyValues);
    void Error(Exception exception, string message);
    void Error(Exception exception, string messageTemplate, params object[] propertyValues);
    void Fatal(string message);
    void Fatal(string messageTemplate, params object[] propertyValues);
    void Fatal(Exception exception, string message);
    void Fatal(Exception exception, string messageTemplate, params object[] propertyValues);
  }
}