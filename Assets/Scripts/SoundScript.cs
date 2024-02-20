using UnityEngine;
using UnityEngine.UI;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private BattleScript _battleScript;

    [SerializeField] private Scrollbar VolumeSoundtrack;
    [SerializeField] private AudioSource ButtonClickSound;
    [SerializeField] private AudioSource Soundtrack;
    [SerializeField] private AudioSource BattleSounds;
    [SerializeField] private AudioClip[] SoundtrackClip; int i; bool pause = false;
    [SerializeField] private AudioClip ButtonClickResources;
    [SerializeField] private AudioClip ButtonClickBuilding;
    [SerializeField] private AudioClip ButtonClickModifSound;
    [SerializeField] private AudioClip ButtonClickUpendSound;
    [SerializeField] private AudioClip ButtonClickWarSound;
    [SerializeField] private AudioClip ButtonClickCreateArmySound;
    [SerializeField] private AudioClip ButtonClickDeCreateArmySound;
    [SerializeField] private AudioClip ButtonClickTradeSound;

    private void Start()
    {
        Soundtrack.clip = SoundtrackClip[i];
        Soundtrack.Play();
    }

    private void FixedUpdate()
    {
        if ((Soundtrack.isPlaying == false) && (pause == false))
        {
            if (i + 1 > SoundtrackClip.Length - 1)
                i = 0;
            else
                i += 1;

            Soundtrack.clip = SoundtrackClip[i]; 
        }

        if ((_battleScript.TheBattleIsOn == true) && (BattleSounds.isPlaying == false) && (_battleScript.BattleDelay == 2))
        {
            BattleSounds.Play();
        }

        else if (_battleScript.TheBattleIsOn == false) { BattleSounds.Stop(); }

    }
    public void ButtonClickUpend()
    {
        ButtonClickSound.clip = ButtonClickUpendSound;
        ButtonClickSound.Play();
    }

    public void ButtonClickNext()
    {
        if (i >= SoundtrackClip.Length-1) { i = 0; } 
        else { i++;}
        Soundtrack.clip = SoundtrackClip[i];
        Soundtrack.Play();
    }

    public void ButtonClickResourcesf()
    {
        ButtonClickSound.clip = ButtonClickResources;
        ButtonClickSound.Play();
    }

    public void ButtonClickBuildingf()
    {
        
        ButtonClickSound.clip = ButtonClickBuilding;
        ButtonClickSound.Play();

    }

    public void ButtonClickModiff()
    {
        
        ButtonClickSound.clip = ButtonClickModifSound;
        ButtonClickSound.Play();
    }

    public void ButtonClickWarf()
    {
        
        ButtonClickSound.clip = ButtonClickWarSound;
        ButtonClickSound.Play();
    }

    public void ButtonClickCreateArmyf()
    {

        ButtonClickSound.clip = ButtonClickCreateArmySound;
        ButtonClickSound.Play();
    }

    public void ButtonClickDeCreateArmyf()
    {

        ButtonClickSound.clip = ButtonClickDeCreateArmySound;
        ButtonClickSound.Play();
    }

    public void ButtonSoundtrackPause()
    {
        Soundtrack.Pause();
        pause = true;

    }

    public void ButtonSoundtrackResume()
    {
        Soundtrack.Play();
        pause = false;
    }

    public void ButtonClickClickTradef()
    {

        ButtonClickSound.clip = ButtonClickTradeSound;
        ButtonClickSound.Play();
    }

    public void SoundtrackVolume()
    {
        Soundtrack.volume = VolumeSoundtrack.value;

    }

}
