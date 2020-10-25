using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace EndOfHeroes.Services
{
    /// <summary>
    ///     Used to handle exceptions to log exceptions accordingly.
    ///     
    ///     This is heavily influenced by examples written by egordorichev (https://github.com/egordorichev)
    /// </summary>
    public class CrashHandler
    {
        public static void Bind()
        {
            AppDomain.CurrentDomain.UnhandledException += ExceptionHandler;
        }

        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var e = (Exception)args.ExceptionObject;
            Report(e);
        }

        public static void Report(Exception e)
        {
            var builder = new StringBuilder();
            builder.AppendLine("----- End of Heroes has creashed -----");

            builder.AppendLine("----- Information -----");

            builder.AppendLine($"Date: {DateTime.Now:dd.MM.yyyy HH:mm}");
            builder.AppendLine($"OS: {Environment.OSVersion} {(Environment.Is64BitOperatingSystem ? 64 : 32)} bit");

            builder.AppendLine($"End of Heroes version: {GetGameVersion()}");

            builder.AppendLine("----- Error -----");
            
            builder.AppendLine(e.Message);
            builder.AppendLine();
            builder.AppendLine(e.StackTrace);

            var message = builder.ToString();
            File.AppendAllText("crash.txt", message);
        }

        private static string GetGameVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
