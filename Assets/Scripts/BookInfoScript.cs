using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class BookInfoScript : MonoBehaviour
{
    [Inject] private TimeScript _timeScript;
    [Inject] private ResourcesManagerScript _resourcesManagerScript;
    [Inject] private FortressData _fortressData;
    [Inject] private MenuScript _menuScript;

    [SerializeField] private GameObject[] _button;

    [SerializeField] private GameObject[] _page; 
    [SerializeField] private Slider _warEventPage;
    [SerializeField] private TextMeshProUGUI[] _text;

    private int _currentPage;


    void Awake()
    {
        _text[0].text = _fortressData.FortressDescription[_menuScript.StartFortress];
        _timeScript.changeDay.AddListener(OnChangeDay);
    }

    void OnChangeDay()
    {
        _warEventPage.value = _resourcesManagerScript.WarEventCallProgress / 10;
    }

    public void Page(int value) 
    {
        _page[_currentPage].SetActive(false);
        _currentPage += value;
        _page[_currentPage].SetActive(true);

        if (_currentPage == _page.Length) _button[0].SetActive(false);
        else _button[1].SetActive(true);

        if (_currentPage == 0) _button[1].SetActive(false);
        else _button[1].SetActive(true);
    }
}
