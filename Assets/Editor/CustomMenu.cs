using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class CustomMenu
{
    [MenuItem("Tools/Clear Player Prefs")]
    static void ClearPlayerPrefs() {
        PlayerPrefs.DeleteAll();
    }
}
