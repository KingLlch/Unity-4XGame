using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Audio;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private TimeScript _timeScript;
    [SerializeField] private SaveAndLoadScript _saveAndLoadScript;

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject gamestart;
    [SerializeField] private GameObject gamesettings;
    [SerializeField] private VideoPlayer trailer;
    [SerializeField] private Scene currentScene;

    [SerializeField] private GameObject Settingspanel;
    [SerializeField] private GameObject HowtoPlaypanel;

    [SerializeField] private AudioMixer MainAudioMixer;
    [SerializeField] private Slider MasterVolumeValue;
    [SerializeField] private Slider MusicVolumeValue;
    [SerializeField] private Slider SoundsVolumeValue;

    [SerializeField] private AudioSource MenuMusic;

    [SerializeField] private Dropdown dropdown;
    private Resolution[] res;
    private int trailerplay = 1;

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
            else if (gamesettings.activeInHierarchy == false) 
            { 
                gamesettings.SetActive(true);
                _timeScript.pause(); 
            }

        }
    }


    private void FixedUpdate()
    {
        currentScene = SceneManager.GetActiveScene();
        if ((currentScene.buildIndex == 0) && (trailer.GetComponent<VideoPlayer>().isPlaying == false) && (trailerplay == 1))
        {
            canvas.SetActive(true);
            MenuMusic.Play();
            trailerplay = 0;
        }
        else if ((currentScene.buildIndex == 0) && (Input.anyKey))
        {
            trailer.GetComponent<VideoPlayer>().Pause();
        }
    }

    private void SetRes()
    {
        Screen.SetResolution(res[dropdown.value].width, res[dropdown.value].height, true);
    }

    private void SetMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVolume", MasterVolumeValue.value);
    }

    private void SetMusicVolume()
    {
        MainAudioMixer.SetFloat("MusicVolume", MusicVolumeValue.value);
    }

    private void SetSoundsVolume()
    {
        MainAudioMixer.SetFloat("SoundVolume", SoundsVolumeValue.value);
    }

    public void start()
    {
        gamestart.SetActive(true);
    }

    public void startnewgame()
    {
        SceneManager.LoadScene("Game");
    }

    public void startloadgame ()
    {
        SceneManager.LoadScene("Game");
        _saveAndLoadScript.loadgame();
    }
    public void startbattles()
    {
        SceneManager.LoadScene("Battle");
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

}
