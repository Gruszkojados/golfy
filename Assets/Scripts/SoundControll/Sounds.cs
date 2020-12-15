using UnityEngine;

public class Sounds : MonoBehaviour // Sounds player
{
    public AudioSource setForce;
    public AudioSource shoot;
    public AudioSource lost;
    public AudioSource win;
    public AudioSource buttonClick;
    public AudioSource startTurn;
    public AudioSource changeLvl_1;
    public AudioSource changeLvl_2;
    public AudioSource bounce_1;
    public AudioSource bounce_2;
    public AudioSource ballInHole;
    public AudioSource swing;


    private void Awake() {
        PlayerProfile.LoadSounds();
        SoundsAction.OnSetForce += SetForceSound;
        SoundsAction.OnShoot += ShootSound;
        SoundsAction.OnBounce += BounceSound;
        SoundsAction.OnLost += LostSound;
        SoundsAction.OnWin += WinSound;
        SoundsAction.OnButtonClick += ButtonClickSound;
        SoundsAction.OnChangePlayer += ChangePlayerSound;
        SoundsAction.OnBallInHole += BallInHoleSoundSound;
        SoundsAction.OnBallSwing += BallSwingSound;
    }
    private void OnDestroy() {
        SoundsAction.OnSetForce -= SetForceSound;
        SoundsAction.OnShoot -= ShootSound;
        SoundsAction.OnBounce -= BounceSound;
        SoundsAction.OnLost -= LostSound;
        SoundsAction.OnWin -= WinSound;
        SoundsAction.OnButtonClick -= ButtonClickSound;
        SoundsAction.OnChangePlayer -= ChangePlayerSound;
        SoundsAction.OnBallInHole -= BallInHoleSoundSound;
        SoundsAction.OnBallSwing -= BallSwingSound;
    }
    void SetForceSound() {
        setForce.Play();
    }
    void ShootSound() {
        shoot.Play();
    }
    void BounceSound() {
        if(Random.Range(0f, 2f) > 1) {
            bounce_1.Play();
        } else {
            bounce_2.Play();
        }
    }
    void LostSound() {
        lost.Play();
    }
    void WinSound() {
        win.Play();
    }
    void ButtonClickSound() {
        buttonClick.Play();
    }
    void ChangePlayerSound() {
        if(PlayerProfile.gameMode == Gamemode.singlePlayer) {
            startTurn.Play();
        } else {
            if(Random.Range(0f, 2f) > 1) {
                changeLvl_1.Play();
            } else {
                changeLvl_2.Play();
            }
        }
    }
    void BallInHoleSoundSound() {
        ballInHole.Play();
    }

    void BallSwingSound() {
        swing.Play();
    }
}