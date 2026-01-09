using Godot;

public abstract class JsonUtils {
    public static bool FileExists(string filePath) {
        using FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        return file != null;
    }

    public static string GetJsonFromFile(string filePath, string errorMessage) {
        using FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        return file == null ? throw new System.Exception(errorMessage) : file.GetAsText();
    }

    public static void WriteToJsonFile(string filePath, string json) {
        string folderPath = System.IO.Path.GetDirectoryName(filePath);
        GD.Print($"Directory to write to: {folderPath}");
        if (!DirAccess.DirExistsAbsolute(folderPath)) {
            GD.Print($"Dir {folderPath} does not exist. Making it.");
            DirAccess.MakeDirAbsolute(folderPath);
        }

        using FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
        file.StoreString(json);
    }
}