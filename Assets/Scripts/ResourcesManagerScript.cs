using UnityEngine;
using UnityEngine.UI;

public class ResourcesManagerScript : MonoBehaviour
{
    [SerializeField] private ModifierScript _modifierScript;
    [SerializeField] private EventsScript _events;
    [SerializeField] private BattleScript _battleScript;
    [SerializeField] private EnemyArmyScript _enemyArmyScript;
    [SerializeField] private EnemyArmyData _enemyArmyData;
    [SerializeField] private ArmyScript _armyScript;
    [SerializeField] private ArmyData _armyData;
    [SerializeField] private TradeScript _tradeScript;
    [SerializeField] private BuildingsScript _buildingsScript;
    [SerializeField] private MenuScript _menuScript;
    [SerializeField] private TimeScript _timeScript;

    //������� � �� ����������� � ����������
    public float[] Resources = { 200, 200, 200, 200, 2000, 200 }; //�������
    public float[] BaseProduction = { 25, 1, 0, 6, 200 }; //������� ������������ ��������
    private float _gnomsReproduction; //����������� ������

    [SerializeField] private Text[] _textResoursesMainPanel; //������� � ������
    [SerializeField] private Text[] _textResoursesEconomy; //������� � ������� ���������
    [SerializeField] private Text _freeGnomsUI; //��������� �����

    //������� �� ����������
    private float[] _differenceGeneral = new float[6]; //����� �������
    private float[] _productionGeneral = new float[5];//����� ������������
    private float[] _consumingGeneral = new float[5]; //����������� 
    private float[] _buildingsProduction = new float[5]; // ������������ ��������
    private float[] _armyMaintenance = new float[5]; //���������� �����
    private float[] _armyMaintenanceResourses = new float[5]; //���������� �����

    //������������� ������ � �� ���������� 
    public float[] Buildingscellduration = { 30, 30, 30, 10, 10 }; //����� ������������� ������
    private float[] _buidingMaintenance = { 0, 0, 0, 0 }; // ��������� ���������� * ������������
    private float[] _buidingMaintenanceResourses = { 0, 0, 0, 0 }; //��������� ����������

    [HideInInspector] public float CellBuildingInWork; //������ � ������
    [HideInInspector] public float CellBuildingValue = 0;//����� ����� 
    [HideInInspector] public float[] BuildingCellTime = new float[5]; // ����� ������������� � ������ 
    [HideInInspector] public float[] BuildingsCellType = new float[5]; // ����� ��� ������ �������� 
    [HideInInspector] public float[] BuildingsCurrent = new float[5];//����� ������ �� ����
    [HideInInspector] public float[] BuildingsMax = { 10, 10, 10, 10, 10 };//����� ������ �����
    [HideInInspector] public float[] WorkerGnomsCurrent = { 4, 3, 0, 0, 7 }; // ���������� ������ �������
    [HideInInspector] public float[] WorkerGnomsMax = new float[5];// ������������ ���������� ������ �������

    //���� ����� 
    [HideInInspector] public float[] ArmyCreateTime = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ����� ����� � ������ 
    [HideInInspector] public float[] ArmyCreateType = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // ����� ��� ����� ���������� 
    [HideInInspector] public float[] ArmyCreateTimeDuration = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //����� ����� �����

    //������������ ������
    [HideInInspector] public float AllWorkerGnoms;
    [HideInInspector] public float AllArmyGnoms = 360;
    [HideInInspector] public float FreeGnoms;
    [HideInInspector] public float FortressRiches;

    //UI
    [SerializeField] private Text _cellinwork;
    [SerializeField] private Text[] _maxCellUI;
    [SerializeField] private Text[] _currentCellUI;
    [SerializeField] private Text[] _valueGnomsBuilders;
    [SerializeField] private Text[] _currentWorkerGnomsUI;
    [SerializeField] private Text[] _maxWorkerGnomsUI;
    [SerializeField] private Text[] _armyUI;
    [SerializeField] private Text[] _armyCreateUI;
    [SerializeField] private Text[] DifferenceUI;
    private string[] _sign = { "", "", "", "", "" };

    //�������
    private bool _eventWarCome = false;
    private int _guaranteedChanceCallEvent = 0;

    [HideInInspector] public int WarEventCallProgress = 200;
    [HideInInspector] public int randomEvent = 101;
    [HideInInspector] public int randomEventWar = 15;
    [HideInInspector] public int EventWarTime = 0;

    [SerializeField] private Sprite[] _flagSprite;
    [SerializeField] private GameObject[] _flagFortress;

    private void Awake()
    {
        _timeScript.changeHour.AddListener(OnChangeHour);
        _timeScript.changeDay.AddListener(OnChangeDay);
        _timeScript.changeMounth.AddListener(OnChangeMounth);
        _timeScript.changeYear.AddListener(OnChangeYear);
    }

    private void Start()
    {
        if (_menuScript.StartFortress != 0)
        {
            for (int i = 0; i < _flagFortress.Length; i++)
            {
                _flagFortress[i].GetComponent<Image>().sprite = _flagSprite[_menuScript.StartFortress - 1]; //��������� ��������
            }
            _events.start_event(); //��������� �������
        }

        for (int i = 0; i <= 4; i++)
        {
            _currentWorkerGnomsUI[i].text = WorkerGnomsCurrent[i].ToString(); //������� ����� �������
            _maxCellUI[i].text = BuildingsMax[i].ToString(); //������������ ����� ������
        }
    }

    private void FixedUpdate()
    {
        //�����
        _textResoursesMainPanel[5].text = Resources[5].ToString();
        _textResoursesEconomy[5].text = Resources[5].ToString();

        //��������� ��������
        FortressRiches = Resources[0] * _tradeScript.tradeCost[0] + Resources[1] * _tradeScript.tradeCost[1] + Resources[2] * _tradeScript.tradeCost[2] + Resources[3] * _tradeScript.tradeCost[3] + Resources[4] * _tradeScript.tradeCost[4] + Resources[5] * 10;
        _cellinwork.text = CellBuildingInWork.ToString();

        for (int i = 0; i <= 4; i++)
        {
            //��������� ������
            _textResoursesMainPanel[i].text = Mathf.Round(Resources[i]).ToString();
            _textResoursesEconomy[i].text = Resources[i].ToString();
            FreeGnoms = Resources[5] - AllWorkerGnoms - AllArmyGnoms;

            if (i < 4)
            {
                //���������� ������ � �����
                for (int j = 0; j <= 15; j++)
                {
                    if (j <= 4)
                    {
                        _buidingMaintenanceResourses[i] += _buildingsScript.Buildings[0, j] * _buildingsScript.Buildings[i + 1, j];

                    }

                    _armyMaintenanceResourses[i] += _armyData.Army[0, j] * _armyData.Army[i + 1, j];

                    _armyUI[j].text = _armyData.Army[0, j].ToString();
                    _armyCreateUI[j].text = _armyScript.CreateValue[j].ToString();
                }

                _buidingMaintenance[i] = _buidingMaintenanceResourses[i] * _modifierScript.buildMaintenanceModifier;
                _buidingMaintenanceResourses[i] = 0;

                //���������� �����
                _armyMaintenance[i] = _armyMaintenanceResourses[i] * _modifierScript.armyMaintenanceModifier;
                _armyMaintenanceResourses[i] = 0;

                //����� ��������
                _consumingGeneral[i] = _armyMaintenance[i] + _buidingMaintenance[i];
            }

            _consumingGeneral[4] = Resources[5] * _modifierScript.foodMaintenanceModifier * (1 / _modifierScript.moodModifier) * _modifierScript.seasonFoodModifier;

            //������� ����� ������
            _currentCellUI[i].text = BuildingsCurrent[i].ToString();
            _maxCellUI[i].text = BuildingsMax[i].ToString();

            //������������ ����� �������
            WorkerGnomsMax[i] = _buildingsScript.Buildings[0, i] * 5;
            _maxWorkerGnomsUI[i].text = WorkerGnomsMax[i].ToString();

            //������������ ��������
            if (WorkerGnomsMax[i] != 0)
            {
                _buildingsProduction[i] = (_buildingsScript.Buildings[0, i] * _buildingsScript.BuildingsProduct[i] * WorkerGnomsCurrent[i] / WorkerGnomsMax[i]) * _modifierScript.workEfficiency;
            }
            else { _buildingsProduction[i] = 0; }

            //������������
            _productionGeneral[i] = (BaseProduction[i] + _buildingsProduction[i]) * _modifierScript.devastationModifier;

            //�������
            _differenceGeneral[i] = _productionGeneral[i] - _consumingGeneral[i];

            //����������� �������� � ����������
            if (_differenceGeneral[i] >= 0)
            {
                DifferenceUI[i].color = Color.green; _sign[i] = "+";
            }
            else
            {
                DifferenceUI[i].color = Color.red; _sign[i] = "";
            }

            DifferenceUI[i].text = _sign[i] + _differenceGeneral[i].ToString();
            _freeGnomsUI.text = FreeGnoms.ToString();

            //�����
            if (Resources[4] < _consumingGeneral[4])
            {
                if (Resources[4] >= 0)
                {
                    _differenceGeneral[5] = -Mathf.Round((_consumingGeneral[4] - Resources[4]) / 6);
                }

                else
                {
                    _differenceGeneral[5] = -Mathf.Round(Resources[5] / 6);
                }
            }

            else
            {
                _differenceGeneral[5] = 0;
            }

            DifferenceUI[5].text = _differenceGeneral[5].ToString();

            //��������� ������ 
            _currentCellUI[i].text = BuildingsCurrent[i].ToString();
            _currentWorkerGnomsUI[i].text = WorkerGnomsCurrent[i].ToString();
            _valueGnomsBuilders[0].text = CellBuildingValue.ToString();
            _valueGnomsBuilders[1].text = CellBuildingValue.ToString();

        }

    }

    public void OnChangeHour()
    {
        
    }

    public void OnChangeDay()
    {

        if ((_eventWarCome == true) && (EventWarTime != 0) && (EventWarTime != -1))
        {
            EventWarTime -= 1;

        }
        else if ((_eventWarCome == true) && (EventWarTime == 0) && (EventWarTime != -1))
        {
            _battleScript.TheBattleIsOn = true;
            _eventWarCome = false;
            EventWarTime = -1;
        }

        //������ �������
        if (Random.Range(_guaranteedChanceCallEvent, 100) >= 90)
        {
            randomEvent = Random.Range(0, 15);

            _guaranteedChanceCallEvent = 0;
        }
        else { _guaranteedChanceCallEvent += 5; }


        WarEventCallProgress += Random.Range(0, 10); //Random.Range(0, 10);

        if (WarEventCallProgress >= 1000)//1000
        {
            EventWarTime = Random.Range(3, 6) * 30;
            randomEventWar = Random.Range(0, 14); //0 � 14
            WarEventCallProgress = 0;
            _eventWarCome = true;
        }

        for (int i = 0; i <= 5; i++)
        {
            Resources[i] += _differenceGeneral[i];
        }

        //������������� ������
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                for (int k = 0; k <= 4; k++)
                {
                    if (BuildingCellTime[i] != 0)
                    {

                        if ((BuildingsCellType[j] == k) && (BuildingCellTime[i] == Buildingscellduration[i]))
                        {
                            //Debug.Log("������������� ���������");
                            _buildingsScript.Buildings[0, k] += 1;
                            CellBuildingInWork -= 1;
                            BuildingCellTime[i] = 0;
                            Buildingscellduration[i] = 0;
                            BuildingsCellType[j] = -1;

                        }
                    }
                }
            }

            if (CellBuildingValue >= CellBuildingInWork) { Buildingscellduration[i] += 1; }

        }

        //���� ������
        for (int i = 0; i <= 19; i++)
        {
            for (int j = 0; j <= 15; j++)
            {
                for (int k = 0; k <= 15; k++)
                {
                    if (ArmyCreateTime[i] != 0)
                    {

                        if ((ArmyCreateType[j] == k) && (ArmyCreateTime[i] <= ArmyCreateTimeDuration[i]))
                        {
                            _armyData.Army[0, k] += 1;
                            ArmyCreateTime[i] = 0;
                            ArmyCreateTimeDuration[i] = 0;
                            ArmyCreateType[j] = -1;
                            _armyScript.CreateValue[k] -= 1;
                        }
                    }
                }
            }

            ArmyCreateTimeDuration[i] += 1;

        }
    }

    public void OnChangeMounth()
    {
        if (_modifierScript.devastationModifier < 1)
        {
            _modifierScript.devastationModifier += 0.1f;
        }
    }

    public void OnChangeYear()
    {
        //���������� �����������
        _gnomsReproduction = Mathf.Round(Resources[5] / 50);
        Resources[5] += _gnomsReproduction;
    }

    public void value_cell_up() //��������� ���� ����� ������������ ������
    {
        if (FreeGnoms >= 50)
        {
            AllWorkerGnoms += 50;
            switch (CellBuildingValue)
            {
                case 4:
                    CellBuildingValue = 5;
                    break;
                case 3:
                    CellBuildingValue = 4;
                    break;
                case 2:
                    CellBuildingValue = 3;
                    break;
                case 1:
                    CellBuildingValue = 2;
                    break;
                case 0:
                    CellBuildingValue = 1;
                    break;
            }
        }
    }

    public void value_cell_down()
    {
        if (CellBuildingValue >= 1)
        AllWorkerGnoms -= 50;

        switch (CellBuildingValue)
        {
            case 5:
                CellBuildingValue = 4;
                break;
            case 4:
                CellBuildingValue = 3;
                break;
            case 3:
                CellBuildingValue = 2;
                break;
            case 2:
                CellBuildingValue = 1;
                break;
            case 1:
                CellBuildingValue = 0;
                break;
        }
    }

}
