using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DystopiaEngine
{
    public class Settings
    {
        public static bool IsFullscreen { get; set; }
        public static int WindowWidth { get; set; }
        public static int WindowHeight { get; set; }

        // Windows only settings, to support OSX and Linux this will need adapted to set the directory accordingly
        private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public Settings()
        {
            IsFullscreen = false;
            WindowWidth = 720;
            WindowHeight = 1280;
        }

        public void Load()
        {
            var json = File.ReadAllText(_path);
            var settings = JsonSerializer.Deserialize<Settings>(json);
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(this);
            File.WriteAllText(_path, json);
        }
    }
}
