using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private EnemyArmyScript _enemyArmyScript;
    [SerializeField] private EnemyArmyData _enemyArmyData;
    [SerializeField] private ArmyScript _armyScript;
    [SerializeField] private ArmyData _armyData;
    [SerializeField] private TimeScript _timeScript;
    [SerializeField] private ModifierScript _modifierScript;

    [HideInInspector] public int CheckArmyStart = 0;
    [HideInInspector] public int CheckEnemyArmyStart = 0;
    [HideInInspector] public int BaseChanceAttack = 35;

    [HideInInspector] public float[] _battleArmyCheck = new float[3];
    [HideInInspector] public float[] _enemybattleArmyCheck = new float[3];

    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private GameObject _battleWinPanel;
    [SerializeField] private GameObject _battleLosePanel;
    [SerializeField] private Text[] _timeBattle;

    [HideInInspector] public int BattleDelay = 0;
    [HideInInspector] public bool TheBattleIsOn = false;
    private bool battlepanel = false;

    private void Awake()
    {
        try
        {
            _timeScript.changeHour.AddListener(OnChangeHour);
        }
        catch 
        {}
    }

    private void OnChangeHour()
    {
        if (TheBattleIsOn == true) //если проходит битва
        {
            if (battlepanel == false)
            {
                battlepanel = true;
                _battlePanel.SetActive(true);
            }

            EnemyArmyAssignment();
            ArmyAssignment();

            if (BattleDelay == 2)
            {

                Battle();

                if ((_battleArmyCheck[0] == 0) && (TheBattleIsOn == true))
                {
                    for (int r = 0; r <= 15; r++)
                    {
                        _enemyArmyScript.OrcsArmy[r] += _enemyArmyScript.enemybattleArmyStart[r];
                    }
                    TheBattleIsOn = false;
                    _battlePanel.SetActive(false);
                    _battleLosePanel.SetActive(true);
                    _timeBattle[0].text = _timeScript.GameYear.ToString() + " Года " + _timeScript._stringMonth + " " + _timeScript.GameDay.ToString() + " Дня ";
                    CheckArmyStart = 0;
                    BattleDelay = 0;
                    battlepanel = false;

                    for (int i = 0; i <= 5; i++)
                    {
                        _resourcesManagerScript.Resources[i] -= _resourcesManagerScript.Resources[i] / 2;
                        _modifierScript.devastationModifier -= 0.5f;
                    }

                }

                if ((_enemybattleArmyCheck[0] == 0) && (TheBattleIsOn == true))
                {
                    for (int r = 0; r <= 15; r++)
                    {
                        _armyData.Army[0, r] += _armyScript.BattleArmyStart[r];
                    }

                    TheBattleIsOn = false;
                    _battlePanel.SetActive(false);
                    _battleWinPanel.SetActive(true);
                    _timeBattle[1].text = _timeScript.GameYear.ToString() + " Года " + _timeScript._stringMonth + " " + _timeScript.GameDay.ToString() + " Дня ";
                    CheckEnemyArmyStart = 0;
                    BattleDelay = 0;
                    battlepanel = false;
                }
            }
        }

        if (BattleDelay != 2)
        {
            BattleDelay += 1;
        }
    }

    public void ArmyDistribution()
    {
        int _string = 0;
        for (int j = 15; j >= 0; j--) //распределение
        {
            if (j <= 7) _string = 0;
            else if (j <= 11) _string = 1;
            else if (j <= 15) _string = 2;

            for (int centerCell2 = 10, centerCell1 = 9; centerCell1 >= 0; centerCell2++, centerCell1--)
            {
                if ((_armyScript.BattleArmyReserves[j]) > 0)
                {
                    if (_armyScript.battle[_string, centerCell2] == 0)
                    {
                        _armyScript.battle[_string, centerCell2] = j + 1;
                        _armyScript.BattleArmyReserves[j] -= 1;
                        _armyScript.BattleHP[_string, centerCell2] = _armyData.ArmyCharacteristics[2, j] * _armyData.ArmyCharacteristics[1, j];
                    }
                }
                if ((_armyScript.BattleArmyReserves[j]) > 0)
                {
                    if (_armyScript.battle[_string, centerCell1] == 0)
                    {
                        _armyScript.battle[_string, centerCell1] = j + 1;
                        _armyScript.BattleArmyReserves[j] -= 1;
                        _armyScript.BattleHP[_string, centerCell1] = _armyData.ArmyCharacteristics[2, j] * _armyData.ArmyCharacteristics[1, j];
                    }
                }

            }

        }
    }

    public void EnemyArmyDistribution()
    {
        int _string = 0;
        for (int j = 15; j >= 0; j--) //распределение
        {
            if (j <= 10) _string = 0;
            else if (j <= 13) _string = 1;
            else if (j <= 15) _string = 2;

            for (int centerCell2 = 10, centerCell1 = 9; centerCell1 >= 0; centerCell2++, centerCell1--)
            {
                if (_enemyArmyScript.enemybattleArmyReserves[j] > 0)
                {
                    if (_enemyArmyScript.enemybattle[_string, centerCell2] == 0)
                    {
                        _enemyArmyScript.enemybattle[_string, centerCell2] = j + 1;
                        _enemyArmyScript.enemybattleArmyReserves[j] -= 1;
                        _enemyArmyScript.enemybattleHP[_string, centerCell2] = _enemyArmyData.OrcsArmyCharacteristics[2, j] * _enemyArmyData.OrcsArmyCharacteristics[1, j];
                    }
                }

                if (_enemyArmyScript.enemybattleArmyReserves[j] > 0)
                {
                    if (_enemyArmyScript.enemybattle[_string, centerCell1] == 0)
                    {
                        _enemyArmyScript.enemybattle[_string, centerCell1] = j + 1;
                        _enemyArmyScript.enemybattleArmyReserves[j] -= 1;
                        _enemyArmyScript.enemybattleHP[_string, centerCell1] = _enemyArmyData.OrcsArmyCharacteristics[2, j] * _enemyArmyData.OrcsArmyCharacteristics[1, j];
                    }
                }

            }

        }
    }

    public void EnemyArmyAssignment()
    {
        if (CheckEnemyArmyStart == 0)
        {
            for (int j = 0; j < 15; j++)
            {
                _enemyArmyScript.enemybattleArmyStart[j] = _enemyArmyScript.OrcsArmy[j];
                _enemyArmyScript.enemybattleArmyReserves[j] = _enemyArmyScript.OrcsArmy[j];
                _enemyArmyScript.OrcsArmy[j] = 0;
            }
            CheckEnemyArmyStart = 1;
        }

        EnemyArmyDistribution();
    }

    public void ArmyAssignment()
    {
        if (CheckArmyStart == 0)
        {
            for (int j = 0; j <= 15; j++)
            {
                _armyScript.BattleArmyStart[j] = _armyData.Army[0, j];
                _armyScript.BattleArmyReserves[j] = _armyData.Army[0, j];
                _armyData.Army[0, j] = 0;
            }

            CheckArmyStart = 1;
        }

        ArmyDistribution();

    }

    public void ourUnitDead(int rowCell, int unitType)
    {
        _armyScript.battle[0, rowCell] = 0;
        _armyScript.BattleArmyStart[unitType - 1] -= 1;

        try{_resourcesManagerScript.Resources[5] -= _armyData.ArmyCreate[5, unitType - 1];}
        catch{}
        
    }

    public void enemyUnitDead(int rowCell, int enemyUnitType)
    {
        _enemyArmyScript.enemybattle[0, rowCell] = 0;
        _enemyArmyScript.enemybattleArmyStart[enemyUnitType - 1] -= 1;
    }

    public void ourDamageCalculation(int rowCell, int enemyUnitType, int unitType, int damagetype, float damageModifier = 1)
    {
        _armyScript.BattleHP[0, rowCell] -= damageModifier * _enemyArmyData.OrcsArmyCharacteristics[damagetype, enemyUnitType - 1] * _enemyArmyData.OrcsArmyCharacteristics[1, enemyUnitType - 1] * (1 - ((_armyData.ArmyCharacteristics[3, unitType - 1]) / 100)); // расчет урона
    }

    public void enemyDamageCalculation(int rowCell, int enemyUnitType, int unitType, int damagetype, float damageModifier = 1)
    {
        _enemyArmyScript.enemybattleHP[0, rowCell] -= damageModifier * _armyData.ArmyCharacteristics[damagetype, unitType - 1] * _armyScript.BattleEfficiency * _armyData.ArmyCharacteristics[1, unitType - 1] * (1 - ((_enemyArmyData.OrcsArmyCharacteristics[3, enemyUnitType - 1]) / 100)); // расчет урона
    }

    public void enemyOffset(int _string, int rowCell, int direction)
    {
        _enemyArmyScript.enemybattle[_string, rowCell + direction] = _enemyArmyScript.enemybattle[_string, rowCell];
        _enemyArmyScript.enemybattleHP[_string, rowCell + direction] = _enemyArmyScript.enemybattleHP[_string, rowCell];
        _enemyArmyScript.enemybattle[_string, rowCell] = 0;
        _enemyArmyScript.enemybattleHP[_string, rowCell] = 0;
    }

    public void ourOffset(int _string, int rowCell, int direction)
    {
        _armyScript.battle[_string, rowCell + direction] = _armyScript.battle[_string, rowCell];
        _armyScript.BattleHP[_string, rowCell + direction] = _armyScript.BattleHP[_string, rowCell];
        _armyScript.battle[_string, rowCell] = 0;
        _armyScript.BattleHP[_string, rowCell] = 0;
    }

    public void OurOffsetUp(int _string, int rowCell)
    {
        _armyScript.battle[_string, rowCell] = _armyScript.battle[_string + 1, rowCell];
        _armyScript.BattleHP[_string, rowCell] = _armyScript.BattleHP[_string + 1, rowCell];
        _armyScript.battle[_string + 1, rowCell] = 0;
        _armyScript.BattleHP[_string + 1, rowCell] = 0;
    }

    public void EnemyOffsetUp(int _string, int rowCell)
    {
        _enemyArmyScript.enemybattle[_string, rowCell] = _enemyArmyScript.enemybattle[_string + 1, rowCell];
        _enemyArmyScript.enemybattleHP[_string, rowCell] = _enemyArmyScript.enemybattle[_string + 1, rowCell];
        _enemyArmyScript.enemybattle[_string + 1, rowCell] = 0;
        _enemyArmyScript.enemybattleHP[_string + 1, rowCell] = 0;
    }

    public float calculateEnemyMeleeHitChance(int enemyUnitType, int unitType)
    {
        return (BaseChanceAttack + _enemyArmyData.OrcsArmyCharacteristics[4, enemyUnitType - 1] - (_armyData.ArmyCharacteristics[5, unitType - 1] * _armyScript.BattleEfficiency));
    }

    public float calculateEnemyRangeHitChance(int enemyUnitType)
    {
        return _enemyArmyData.OrcsArmyCharacteristics[7, enemyUnitType - 1];
    }

    public float calculateOurMeleeHitChance(int enemyUnitType, int unitType)
    {
        return (BaseChanceAttack + (_armyData.ArmyCharacteristics[4, unitType - 1] * _armyScript.BattleEfficiency) - _enemyArmyData.OrcsArmyCharacteristics[5, enemyUnitType - 1]);
    }

    public float calculateOurRangeHitChance(int unitType)
    {
        return _armyData.ArmyCharacteristics[7, unitType - 1];
    }

    public void enemyUnitAttack(int rowCell, int value, string attackType, float damageModifier = 1)
    {
        for (int enemyUnitType = 1; enemyUnitType <= 16; enemyUnitType++)
        {
            if (_enemyArmyScript.enemybattle[0, rowCell] == enemyUnitType)
            {
                for (int unitType = 1; unitType <= 16; unitType++)
                {

                    if (_armyScript.battle[0, rowCell + value] == unitType)
                    {
                        if (attackType == "Melee")
                        {
                            if (calculateEnemyMeleeHitChance(enemyUnitType, unitType) >= Random.Range(0, 100)) // расчет попадания
                            {
                                ourDamageCalculation(rowCell + value, enemyUnitType, unitType, 6, damageModifier);
                                if (_armyScript.BattleHP[0, rowCell + value] <= 0) ourUnitDead(rowCell + value, unitType);

                            }

                        }

                        else
                        {
                            if (calculateEnemyRangeHitChance(enemyUnitType) >= Random.Range(0, 100)) // расчет попадания
                            {
                                ourDamageCalculation(rowCell + value, enemyUnitType, unitType, 8, damageModifier);
                                if (_armyScript.BattleHP[0, rowCell + value] <= 0) ourUnitDead(rowCell + value, unitType);

                            }

                        }

                    }
                }
            }
        }
    }

    public void OurUnitAttack(int rowCell, int value, string attackType, float damageModifier = 1)
    {
        for (int c = 1; c <= 16; c++)
        {
            if (_enemyArmyScript.enemybattle[0, rowCell + value] == c)
            {
                for (int d = 1; d <= 16; d++)
                {
                    if (_armyScript.battle[0, rowCell ] == d)
                    {
                        if (attackType == "Melee")
                        {
                            if (calculateOurMeleeHitChance(c, d) >= Random.Range(0, 100)) // расчет попадания
                            {
                                enemyDamageCalculation(rowCell + value, c, d, 6, damageModifier);
                                if (_enemyArmyScript.enemybattleHP[0, rowCell + value] <= 0) enemyUnitDead(rowCell + value, c);

                            }
                        }
                        else
                        {
                            if (calculateOurRangeHitChance(d) >= Random.Range(0, 100)) // расчет попадания
                            {
                                enemyDamageCalculation(rowCell + value, c, d, 8, damageModifier);
                                if (_enemyArmyScript.enemybattleHP[0, rowCell + value] <= 0) enemyUnitDead(rowCell + value, c);
                            }
                        }
                    }
                }
            }
        }
    }

    public void Battle()
    {
        for (int rowCell = 0; rowCell <= 19; rowCell++)
        {
            //битва

            if (_enemyArmyScript.enemybattle[0, rowCell] != 0)  //атака 1 ряда врага
            {
                if (_armyScript.battle[0, rowCell] != 0)
                {
                    enemyUnitAttack(rowCell, 0, "Melee");
                }

                else if ((rowCell + 1 <= 19) && (rowCell - 1 >= 0) && (_armyScript.battle[0, rowCell + 1] != 0) && (_armyScript.battle[0, rowCell - 1] != 0))
                {
                    enemyUnitAttack(rowCell, 1, "Melee", 0.5f);
                    enemyUnitAttack(rowCell, -1, "Melee", 0.5f);
                }

                else if ((rowCell + 1 <= 19) && ((_armyScript.battle[0, rowCell + 1] != 0)))
                {
                    enemyUnitAttack(rowCell, 1, "Melee");
                }

                else if ((rowCell - 1 >= 0) && ((_armyScript.battle[0, rowCell - 1] != 0)))
                {
                    enemyUnitAttack(rowCell, -1, "Melee");
                }

                else if (((rowCell + 1) <= 9) && (_enemyArmyScript.enemybattle[0, rowCell + 1] == 0)) //смещение вправо
                {
                    enemyOffset(0, rowCell, 1);
                }

                else if (((rowCell - 1) >= 10) && (_enemyArmyScript.enemybattle[0, rowCell - 1] == 0)) //смешение влево
                {
                    enemyOffset(0, rowCell, -1);
                }
            }

            if (_armyScript.battle[0, rowCell] != 0) //атака нашего 1 ряда
            {

                if (_enemyArmyScript.enemybattle[0, rowCell] != 0)
                {
                    OurUnitAttack(rowCell, 0, "Melee");
                }

                else if ((rowCell + 1 <= 19) && (rowCell - 1 >= 0) && (_enemyArmyScript.enemybattle[0, rowCell + 1] != 0) && (_enemyArmyScript.enemybattle[0, rowCell - 1] != 0))
                {
                    OurUnitAttack(rowCell, 1, "Melee", 0.5f);
                    OurUnitAttack(rowCell, -1, "Melee", 0.5f);
                }

                else if ((rowCell + 1 <= 19) && ((_enemyArmyScript.enemybattle[0, rowCell + 1] != 0)))
                {
                    OurUnitAttack(rowCell, 1, "Melee");
                }

                else if ((rowCell - 1 >= 0) && ((_enemyArmyScript.enemybattle[0, rowCell - 1] != 0)))
                {
                    OurUnitAttack(rowCell, -1, "Melee");
                }

                else if (((rowCell + 1) <= 9) && (_armyScript.battle[0, rowCell + 1] == 0)) //смещение вправо
                {
                    ourOffset(0, rowCell, 1);

                }

                else if (((rowCell - 1) >= 10) && (_armyScript.battle[0, rowCell - 1] == 0)) //смешение влево
                {
                    ourOffset(0, rowCell, -1);

                }
            }

            if (_enemyArmyScript.enemybattle[1, rowCell] != 0) //атака 2 ряда врага
            {
                if (_armyScript.battle[0, rowCell] != 0)
                {
                    enemyUnitAttack(rowCell, 0, "Range");
                }

                else if ((rowCell + 1 <= 19) && (rowCell - 1 >= 0) && (_armyScript.battle[0, rowCell + 1] != 0) && (_armyScript.battle[0, rowCell - 1] != 0))
                {
                    enemyUnitAttack(rowCell, 1, "Range", 0.5f);
                    enemyUnitAttack(rowCell, -1, "Range", 0.5f);
                }

                else if ((rowCell + 1 <= 19) && ((_armyScript.battle[0, rowCell + 1] != 0)))
                {
                    enemyUnitAttack(rowCell, 1, "Range");
                }

                else if ((rowCell - 1 >= 0) && ((_armyScript.battle[0, rowCell - 1] != 0)))
                {
                    enemyUnitAttack(rowCell, -1, "Range");
                }

                else if (((rowCell + 1) <= 9) && (_enemyArmyScript.enemybattle[1, rowCell + 1] == 0)) //смещение вправо
                {
                    enemyOffset(1, rowCell, 1);
                }

                else if (((rowCell - 1) >= 10) && (_enemyArmyScript.enemybattle[1, rowCell - 1] == 0)) //смешение влево
                {
                    enemyOffset(1, rowCell, -1);

                }
            }

            if (_armyScript.battle[1, rowCell] != 0) //атака нашего 2 ряда
            {
                if (_enemyArmyScript.enemybattle[0, rowCell] != 0)
                {
                    OurUnitAttack(rowCell, 0, "Range");
                }

                else if ((rowCell + 1 <= 19) && (rowCell - 1 >= 0) && (_enemyArmyScript.enemybattle[0, rowCell + 1] != 0) && (_enemyArmyScript.enemybattle[0, rowCell - 1] != 0))
                {
                    OurUnitAttack(rowCell, 1, "Range", 0.5f);
                    OurUnitAttack(rowCell, -1, "Range", 0.5f);
                }

                else if ((rowCell + 1 <= 19) && ((_enemyArmyScript.enemybattle[0, rowCell + 1] != 0)))
                {
                    OurUnitAttack(rowCell, 1, "Range");
                }

                else if ((rowCell - 1 >= 0) && ((_enemyArmyScript.enemybattle[0, rowCell - 1] != 0)))
                {
                    OurUnitAttack(rowCell, -1, "Range");
                }

                else if (((rowCell + 1) <= 9) && (_armyScript.battle[1, rowCell + 1] == 0)) //смещение вправо
                {
                    ourOffset(1, rowCell, 1);

                }

                else if (((rowCell - 1) >= 10) && (_armyScript.battle[1, rowCell - 1] == 0)) //смешение влево
                {
                    ourOffset(1, rowCell, -1);

                }
            }

            if (_enemyArmyScript.enemybattle[2, rowCell] != 0) //атака 3 ряда врага
            {
                if (_armyScript.battle[0, rowCell] != 0)
                {
                    enemyUnitAttack(rowCell, 0, "Range");
                }

                else if ((rowCell + 1 <= 19) && (rowCell - 1 >= 0) && (_armyScript.battle[0, rowCell + 1] != 0) && (_armyScript.battle[0, rowCell - 1] != 0))
                {
                    enemyUnitAttack(rowCell, 1, "Range", 0.5f);
                    enemyUnitAttack(rowCell, -1, "Range", 0.5f);
                }

                else if ((rowCell + 1 <= 19) && ((_armyScript.battle[0, rowCell + 1] != 0)))
                {
                    enemyUnitAttack(rowCell, 1, "Range");
                }

                else if ((rowCell - 1 >= 0) && ((_armyScript.battle[0, rowCell - 1] != 0)))
                {
                    enemyUnitAttack(rowCell, -1, "Range");
                }

                else if (((rowCell + 1) <= 9) && (_enemyArmyScript.enemybattle[2, rowCell + 1] == 0)) //смещение вправо
                {
                    enemyOffset(2, rowCell, 1);
                }

                else if (((rowCell - 1) >= 10) && (_enemyArmyScript.enemybattle[2, rowCell - 1] == 0)) //смешение влево
                {
                    enemyOffset(2, rowCell, -1);

                }
            }

            if (_armyScript.battle[2, rowCell] != 0) //атака нашего 3 ряда
            {
                if (_enemyArmyScript.enemybattle[0, rowCell] != 0)
                {
                    OurUnitAttack(rowCell, 0, "Range");
                }

                else if ((rowCell + 1 <= 19) && (rowCell - 1 >= 0) && (_enemyArmyScript.enemybattle[0, rowCell + 1] != 0) && (_enemyArmyScript.enemybattle[0, rowCell - 1] != 0))
                {
                    OurUnitAttack(rowCell, 1, "Range", 0.5f);
                    OurUnitAttack(rowCell, -1, "Range", 0.5f);
                }

                else if ((rowCell + 1 <= 19) && ((_enemyArmyScript.enemybattle[0, rowCell + 1] != 0)))
                {
                    OurUnitAttack(rowCell, 1, "Range");
                }

                else if ((rowCell - 1 >= 0) && ((_enemyArmyScript.enemybattle[0, rowCell - 1] != 0)))
                {
                    OurUnitAttack(rowCell, -1, "Range");
                }


                else if (((rowCell + 1) <= 9) && (_armyScript.battle[2, rowCell + 1] == 0)) //смещение вправо
                {
                    ourOffset(2, rowCell, 1);

                }

                else if (((rowCell - 1) >= 10) && (_armyScript.battle[2, rowCell - 1] == 0)) //смешение влево
                {
                    ourOffset(2, rowCell, -1);

                }
            }

            for (int i = 0; i < _battleArmyCheck.Length; i++)
            {
                _battleArmyCheck[i] = 0;
                _enemybattleArmyCheck[i] = 0;
            }

            for (int r = 0; r <= 19; r++) //подсчет общего колва армии
            {
                _battleArmyCheck[1] += _armyScript.battle[0, r];
                _battleArmyCheck[2] += _armyScript.battle[1, r];

                for (int r1 = 0; r1 <= 2; r1++)
                {
                    _battleArmyCheck[0] += _armyScript.battle[r1, r];
                }
            }

            for (int r = 0; r <= 19; r++)
            {
                _enemybattleArmyCheck[1] += _enemyArmyScript.enemybattle[0, r];
                _enemybattleArmyCheck[2] += _enemyArmyScript.enemybattle[1, r];

                for (int r1 = 0; r1 <= 2; r1++)
                {
                    _enemybattleArmyCheck[0] += _enemyArmyScript.enemybattle[r1, r];
                }
            }

            if (_battleArmyCheck[1] == 0)
            {
                OurOffsetUp(0, rowCell);
            }

            if (_battleArmyCheck[2] == 0 && _battleArmyCheck[1] == 0)
            {
                OurOffsetUp(1, rowCell);
            }

            for (int r = 0; r <= 15; r++)
            {
                _battleArmyCheck[0] += _armyScript.BattleArmyReserves[r];
            }

            if (_enemybattleArmyCheck[1] == 0)
            {
                EnemyOffsetUp(0, rowCell);
            }

            if (_enemybattleArmyCheck[2] == 0 && _enemybattleArmyCheck[1] == 0)
            {
                EnemyOffsetUp(1, rowCell);
            }

            for (int r = 0; r <= 15; r++)
            {
                _enemybattleArmyCheck[0] += _enemyArmyScript.enemybattleArmyReserves[r];
            }
        }

    }
}
