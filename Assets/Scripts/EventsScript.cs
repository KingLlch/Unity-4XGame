using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EventsScript : MonoBehaviour
{
    [Inject] private ResourcesManagerScript _resourcesManagerScript;
    [Inject] private TimeScript _timeScript;
    [Inject] private EnemyArmyScript _enemyArmyScript;
    [Inject] private EnemyArmyData _enemyArmyData;
    [Inject] private ArmyScript _armyScript;
    [Inject] private ArmyData _armyData;
    [Inject] private MenuScript _menuScript;

    //события
    [HideInInspector]
    public float[,] ResourcesEvent =
    {
        { 0,0,0,0,0,0 },
        { 0,0,0,0,0,0 },
        { 0,0,0,0,0,0 },
        { 0,0,0,0,0,0 },
    };
    [HideInInspector]
    public float[,] ArmyEvent =
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    };

    [HideInInspector] public float[] EnemyArmyEvent = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private string[] OrcsClanName =
    { "Ломаные топоры"  , "Кривой месяц", "Кровавые руки"       , "Костетрясы"      , "Глазочесы", "Кровавые пыряла", "Жубодеры"                , "Гнилозубы",
      "Кабанье рыло"    , "Стреляющие"  , "Почитающие предков"  , "Кривая ухмылка"  , "Троллебои", "Зеленый идол"   , "Жосткие ребята Гримгора"};

    [SerializeField] private Sprite[] OrcsClanImage;
    [SerializeField] private GameObject OrcsClan;
    [SerializeField] private Text OrcsClanNameUI;

    [HideInInspector] public string TextMainEvent = " "; //текст ивента
    [HideInInspector] public string TextMainEventName = " "; //название ивента

    [SerializeField] private GameObject EventPanel;
    [SerializeField] private Text[] Event_UI;
    [SerializeField] private Text[] Event_UI_Text;
    [SerializeField] private Text Event_UI_Text_Main;
    [SerializeField] private Text Event_UI_Text_Main_Name;

    [SerializeField] private GameObject WarEventTime;
    [SerializeField] private GameObject War_Time;
    [SerializeField] private Text[] War_Event_Time;
    [SerializeField] private Text WarTime;

    [SerializeField] private GameObject image1;
    [SerializeField] private GameObject image2;

    [SerializeField] private AudioSource EventSound;
    [SerializeField] private AudioClip[] EconomicEventSound;
    [SerializeField] private AudioClip WarEventSound;

    [HideInInspector] public System.Action[] Event;
    [HideInInspector] public System.Action[] WarEvent;

    private int eventRandom;

    private void Start()
    {
        Event = new System.Action[] { event_0, event_1 };
        WarEvent = new System.Action[] { warevent };
    }

    private void FixedUpdate()
    {
        if (_resourcesManagerScript.randomEventWar != 15)
        {
            WarEvent[0].Invoke();
            EventSound.Play();
            _timeScript.pause();
            OrcsClan.GetComponent<Image>().sprite = OrcsClanImage[_resourcesManagerScript.randomEventWar];
            OrcsClanNameUI.text = OrcsClanName[_resourcesManagerScript.randomEventWar];
            WarEventTime.SetActive(true);
            OrcsClan.SetActive(true);

            War_Event_Time[0].text = (_resourcesManagerScript.EventWarTime / 30).ToString();

            if (_resourcesManagerScript.EventWarTime / 30 == 1)
            {
                War_Event_Time[1].text = "месяц";
            }

            else if ((_resourcesManagerScript.EventWarTime / 30 == 2) || (_resourcesManagerScript.EventWarTime / 30 == 3) || (_resourcesManagerScript.EventWarTime / 30 == 4))
            {
                War_Event_Time[1].text = "месяца";
            }

            else
            {
                War_Event_Time[1].text = "месяцев";
            }

            for (int j = 0; j <= 3; j++)
            {
                Event_UI_Text_Main.text = TextMainEvent;
                Event_UI_Text_Main_Name.text = TextMainEventName;
            }

            for (int y = 0, k = 0, j = 0; y <= 23; y++)
            {
                Event_UI[y].text = ResourcesEvent[j, k].ToString();
                if (k <= 4) { k++; } else { k = 0; j++; }
            }

            EventPanel.SetActive(true);

            _resourcesManagerScript.randomEventWar = 15;
        }


        if (_resourcesManagerScript.EventWarTime != 0)
        {
            War_Time.SetActive(true);
            WarTime.text = (_resourcesManagerScript.EventWarTime / 30).ToString() + " " + War_Event_Time[1].text;
        }
        if (_resourcesManagerScript.EventWarTime == -1) War_Time.SetActive(false);


        for (int i = 0; i <= 1; i++)
        {
            if (_resourcesManagerScript.randomEvent == i)
            {
                Event[i].Invoke();
                EventSound.Play();
                _timeScript.pause();
                OrcsClan.SetActive(false);
                WarEventTime.SetActive(false);

                for (int j = 0; j <= 3; j++)
                {
                    Event_UI_Text_Main.text = TextMainEvent;
                    Event_UI_Text_Main_Name.text = TextMainEventName;
                }

                for (int y = 0, k = 0, j = 0; y <= 23; y++)
                {
                    Event_UI[y].text = ResourcesEvent[j, k].ToString();
                    if (k <= 4) { k++; } else { k = 0; j++; }
                }

                EventPanel.SetActive(true);
                _resourcesManagerScript.randomEvent = 101;
            }
        }
    }

    public void start_event() //различия крепостей
    {
        switch (_menuScript.StartFortress)
        {
            case 1:
                _resourcesManagerScript.BaseProduction[2] += 5; _resourcesManagerScript.Resources[2] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
            case 2:
                _resourcesManagerScript.BaseProduction[0] += 10; _resourcesManagerScript.Resources[0] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
            case 3:
                _resourcesManagerScript.BaseProduction[0] += 10; _resourcesManagerScript.Resources[0] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
            case 4:
                _resourcesManagerScript.BaseProduction[0] += 10; _resourcesManagerScript.Resources[0] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
            case 5:
                _resourcesManagerScript.BaseProduction[0] += 10; _resourcesManagerScript.Resources[0] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
            case 6:
                _resourcesManagerScript.BaseProduction[0] += 10; _resourcesManagerScript.Resources[0] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
            case 7:
                _resourcesManagerScript.BaseProduction[0] += 10; _resourcesManagerScript.Resources[0] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
            case 8:
                _resourcesManagerScript.BaseProduction[0] += 10; _resourcesManagerScript.Resources[0] += 1; _armyData.ArmyCreate[0, 0] = 2;
                break;
        }
        _menuScript.StartFortress = 0;
    }

    public void event_0()
    {
        randomEventCharacteristics(true);

        TextMainEvent = "Мой король, плохие новости, нам нужны дополнительные ресурсы для починки крепости";
        TextMainEventName = "Непредвиденные траты";

        image1.SetActive(true);
        image2.SetActive(false);

        EventSound.clip = EconomicEventSound[0];

    }

    public void event_1()
    {
        randomEventCharacteristics(false);

        TextMainEvent = "Мой король, хорошие новости, мы добыли больше ресурсов чем предполагалось";
        TextMainEventName = "Неожиданная прибыль";

        image1.SetActive(true);
        image2.SetActive(false);

        EventSound.clip = EconomicEventSound[1];

    }

    public void warevent()
    {
        ResourcesEvent[0, 0] = 0;
        ResourcesEvent[0, 1] = 0;
        ResourcesEvent[0, 2] = 0;
        ResourcesEvent[0, 3] = 0;
        ResourcesEvent[0, 4] = 0;
        ResourcesEvent[0, 5] = 0;

        ResourcesEvent[1, 0] = 0;
        ResourcesEvent[1, 1] = 0;
        ResourcesEvent[1, 2] = 0;
        ResourcesEvent[1, 3] = 0;
        ResourcesEvent[1, 4] = 0;
        ResourcesEvent[1, 5] = 0;

        ResourcesEvent[2, 0] = 0;
        ResourcesEvent[2, 1] = 0;
        ResourcesEvent[2, 2] = 0;
        ResourcesEvent[2, 3] = 0;
        ResourcesEvent[2, 4] = 0;
        ResourcesEvent[2, 5] = 0;

        ResourcesEvent[3, 0] = 0;
        ResourcesEvent[3, 1] = 0;
        ResourcesEvent[3, 2] = 0;
        ResourcesEvent[3, 3] = 0;
        ResourcesEvent[3, 4] = 0;
        ResourcesEvent[3, 5] = 0;

        TextMainEvent = "Мой король, до нас дошли сведения что орки объявили ваагх на нашу крепость, нужно подготовится к обороне.";
        TextMainEventName = "Ваагх объявлен на нас";

        for (int i = 0; i <= 15; i++)
        {
            EnemyArmyEvent[i] = Mathf.Round((_resourcesManagerScript.FortressRiches / 4000) * _enemyArmyData.enemyclans[_resourcesManagerScript.randomEventWar, i]);
        }

        image1.SetActive(true);
        image2.SetActive(false);

        EventSound.clip = WarEventSound;
    }

    public void randomEventCharacteristics(bool typeEventBad)
    {
        float _eventBalance = 0;
        if (typeEventBad == true)
        {
            for (int i = 0; i < 4; i++)
            {
                _eventBalance = -50;
                for (int j = 0; j < 5; j++)
                {
                    ResourcesEvent[i, j] = Random.Range(_eventBalance, 0);
                    _eventBalance -= ResourcesEvent[i, j];
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                _eventBalance = 50;
                for (int j = 0; j < 5; j++)
                {
                    ResourcesEvent[i, j] = Random.Range(0, _eventBalance);
                    _eventBalance -= ResourcesEvent[i, j];
                }
            }
        }
    }

    public void SelectEvent(int selectedVariant)
    {
        for (int i = 0; i < 5; i++)
        {
            if ((_resourcesManagerScript.Resources[i] <= 0) && (ResourcesEvent[selectedVariant, i] * -1 <= 0)) { }
            else
            {
                if ((_resourcesManagerScript.Resources[i] < ResourcesEvent[selectedVariant, i] * -1) && (_resourcesManagerScript.randomEvent <= 7)) { return; }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            _resourcesManagerScript.Resources[i] += ResourcesEvent[selectedVariant, i];
        }

        for (int i = 0; i <= 15; i++)
        {
            _armyData.Army[0, i] += ArmyEvent[selectedVariant, i];
            _enemyArmyScript.OrcsArmy[i] += EnemyArmyEvent[i];

        }

        EventPanel.SetActive(false);
        _timeScript.resume();

    }
}
