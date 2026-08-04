using System.IO;
using UnityEngine;

namespace PixelCross.SaveLoad
{
    public static class SaveSystem
    {
        private const string SaveFileExtension = ".json";

        private static string GetSavePath(string slot) =>
            Path.Combine(Application.persistentDataPath, $"save_{slot}{SaveFileExtension}");

        public static void Save(GameSaveData data, string slot = "default")
        {
            var json = JsonUtility.ToJson(data, prettyPrint: true);
            File.WriteAllText(GetSavePath(slot), json);
        }

        public static bool TryLoad(string slot, out GameSaveData data)
        {
            var path = GetSavePath(slot);
            if (!File.Exists(path))
            {
                data = null;
                return false;
            }

            var json = File.ReadAllText(path);
            data = JsonUtility.FromJson<GameSaveData>(json);
            return data != null;
        }

        public static bool HasSave(string slot = "default") => File.Exists(GetSavePath(slot));
    }
}
