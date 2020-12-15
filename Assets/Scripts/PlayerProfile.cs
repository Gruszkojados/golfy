using UnityEngine;

public static class PlayerProfile // Class for read/write info about sounds and holding actual gamemode with level index.
{
    public static int levelIndex;
    public static Gamemode gameMode;
    public static bool sounds;
    const string audio = "isSound";

    public static void ChangeSounds() {
        if(sounds) {
            sounds = false;
        } else {
            sounds = true;
        }
        SaveSounds();
    }
    static void SaveSounds() {
        int tmp;
        if(sounds) {
            tmp = 1;
        } else {
            tmp = 0;
        }
        PlayerPrefs.SetInt(audio, tmp);
    }
    public static void ClearPrefs() {
        Debug.Log("Clear data ");
        PlayerPrefs.DeleteKey("allScores");
    }
    public static void LoadSounds() {
        if(!PlayerPrefs.HasKey(audio)) {
            sounds = true;
            SaveSounds();
        };
        int tmp = PlayerPrefs.GetInt(audio);
        if(tmp.Equals(0)) {
            sounds = false;
        } else {
            sounds = true;
        }
    }
}
public enum Gamemode
{
    singlePlayer = 1,
    bot = 0
}