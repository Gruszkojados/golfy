using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource setForce;
    public AudioSource shoot;
    public AudioSource lost;
    public AudioSource win;
    public AudioSource buttonClick;
    public AudioSource changeLvl_1;
    public AudioSource changeLvl_2;

    private void Awake() {
        SoundsAction.OnSetForce += SetForceSound;
        SoundsAction.OnShoot += ShootSound;
        SoundsAction.OnLost += LostSound;
        SoundsAction.OnWin += WinSound;
        SoundsAction.OnButtonClick += ButtonClick;
        SoundsAction.OnChangePlayer += ChangePlayerSound;
    }
    private void OnDestroy() {
        SoundsAction.OnSetForce -= SetForceSound;
        SoundsAction.OnShoot -= ShootSound;
        SoundsAction.OnLost -= LostSound;
        SoundsAction.OnWin -= WinSound;
        SoundsAction.OnButtonClick -= ButtonClick;
        SoundsAction.OnChangePlayer -= ChangePlayerSound;
    }

    void SetForceSound() {
        setForce.Play();
    }
    void ShootSound() {
        shoot.Play();
    }

    void LostSound() {
        lost.Play();
    }

    void WinSound() {
        win.Play();
    }
    void ButtonClick() {
        buttonClick.Play();
    }
    void ChangePlayerSound() {
        if(Random.Range(0f, 2f) > 1) {
            changeLvl_1.Play();
        } else {
            changeLvl_2.Play();
        }
    }
}