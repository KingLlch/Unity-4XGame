using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Zenject;

public class MenuScript : MonoBehaviour
{
    [Inject] private TimeScript _timeScript;
    [SerializeField] private SaveAndLoadScript _saveAndLoadScript;

    [SerializeField] private GameObject gamestart;
    [SerializeField] private GameObject gamesettings;
    [SerializeField] private Scene currentScene;

    [SerializeField] private GameObject Settingspanel;
    [SerializeField] private GameObject HowtoPlaypanel;

    [SerializeField] private AudioMixer MainAudioMixer;
    [SerializeField] private Slider MasterVolumeValue;
    [SerializeField] private Slider MusicVolumeValue;
    [SerializeField] private Slider SoundsVolumeValue;

    [SerializeField] private Dropdown dropdown;
    private Resolution[] res;

    [SerializeField] private GameObject _loadScreen;

    public int StartFortress = 1;

    private void Start()
    {
        Resolution[] resolution = Screen.resolutions;
        res = resolution.Distinct().ToArray();
        string[] strRes = new string[res.Length];
        
        for (int i = 0; i < res.Length; i++)
        {
            strRes[i] = res[i].width.ToString() + "x" + res[i].height.ToString();
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(strRes.ToList());
        dropdown.value = strRes.Length - 1;

        Screen.SetResolution(res[res.Length - 1].width, res[res.Length - 1].height, true);
    }

    private void Update()
    {
        if ((currentScene.buildIndex == 1) && (Input.GetKeyDown(KeyCode.Escape)))
        {
            if (gamesettings.activeInHierarchy == true) gamesettings.SetActive(false);
            else 
            { 
                gamesettings.SetActive(true);
                _timeScript.pause(); 
            }

        }

        if ((currentScene.buildIndex == 2) && (Input.GetKeyDown(KeyCode.Escape)))
        {
            if (gamesettings.activeInHierarchy == true) gamesettings.SetActive(false);
            else gamesettings.SetActive(true);
        }
    }

    public void SetRes()
    {
        Screen.SetResolution(res[dropdown.value].width, res[dropdown.value].height, true);
    }

    public void SetMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVolume", MasterVolumeValue.value);
    }

    public void SetMusicVolume()
    {
        MainAudioMixer.SetFloat("MusicVolume", MusicVolumeValue.value);
    }

    public void SetSoundsVolume()
    {
        MainAudioMixer.SetFloat("SoundVolume", SoundsVolumeValue.value);
    }

    public void start()
    {
        gamestart.SetActive(true);
    }

    public void startNewGame()
    {
        activeLoadScreen();
        SceneManager.LoadScene("Game");
    }

    public void loadGame ()
    {
        activeLoadScreen();
        SceneManager.LoadScene("Game");
        _saveAndLoadScript.loadgame();
    }
    public void startBattles()
    {
        activeLoadScreen();
        SceneManager.LoadScene("Battles");
    }
    public void settings()
    {
        Settingspanel.SetActive(true);
        HowtoPlaypanel.SetActive(false);
    }

    public void howtoplay()
    {
        Settingspanel.SetActive(false);
        HowtoPlaypanel.SetActive(true);
    }

    public void exitmenu()
    {
        SceneManager.LoadScene(0);
        _saveAndLoadScript.savegame();
    }

    public void exit()
    {
        Application.Quit();
    }

    public void start(int startFortress)
    {
        StartFortress = startFortress;
    }

    private void activeLoadScreen()
    {
        _loadScreen.SetActive(true);
    }
}
