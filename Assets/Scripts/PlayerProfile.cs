public static class PlayerProfile
{
    public static int levelIndex;
    public static Gamemode gameMode;
    public static bool music;
    public static bool sounds;
}

public enum Gamemode
{
    singlePlayer = 1,
    bot = 0
}
