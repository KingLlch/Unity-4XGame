using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookInfoScript : MonoBehaviour
{
    [SerializeField] private TimeScript _timeScript;
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private FortressData _fortressData;
    [SerializeField] private MenuScript _menuScript;

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
