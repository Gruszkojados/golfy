using System;

public static class SoundsAction
{
    public static event Action OnButtonClick = () => {};
    public static event Action OnSetForce = () => {};
    public static event Action OnShoot = () => {};
    public static event Action OnBounce = () => {};
    public static event Action OnWin = () => {};
    public static event Action OnLost = () => {};
    public static event Action OnChangePlayer = () => {};
    public static event Action OnBallInHole = () => {};
    public static event Action OnBallSwing = () => {};
    

    public static void ButtonClick() {
        if(!PlayerProfile.sounds) return;
        OnButtonClick.Invoke();
    }
    public static void SetForce() {
        if(!PlayerProfile.sounds) return;
        OnSetForce.Invoke();
    }
    public static void Shoot() {
        if(!PlayerProfile.sounds) return;
        OnShoot.Invoke();
    }
    public static void Bounce() {
        if(!PlayerProfile.sounds) return;
        OnBounce.Invoke();
    }
    public static void Win() {
        if(!PlayerProfile.sounds) return;
        OnWin.Invoke();
    }
    public static void Lost() {
        if(!PlayerProfile.sounds) return;
        OnLost.Invoke();
    }
    public static void ChangePlayer() {
        if(!PlayerProfile.sounds) return;
        OnChangePlayer.Invoke();
    }
    public static void BallInHole() {
        if(!PlayerProfile.sounds) return;
        OnBallInHole.Invoke();
    }
    public static void BallSwing() {
        if(!PlayerProfile.sounds) return;
        OnBallSwing.Invoke();
    }

}
