using System;

public static class SoundsAction
{
    public static event Action OnButtonClick = () => {};
    public static event Action OnSetForce = () => {};
    public static event Action OnShoot = () => {};
    public static event Action OnWin = () => {};
    public static event Action OnLost = () => {};
    public static event Action OnChangePlayer = () => {};

    public static void ButtonClick() {
        OnButtonClick.Invoke();
    }
    public static void SetForce() {
        OnSetForce.Invoke();
    }
    public static void Shoot() {
        OnShoot.Invoke();
    }
    public static void Win() {
        OnWin.Invoke();
    }
    public static void Lost() {
        OnLost.Invoke();
    }
    public static void ChangePlayer() {
        OnChangePlayer.Invoke();
    }
}
