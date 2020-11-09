using UnityEngine;

public static class PlayerProfile
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