using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System;

public class BattlesScriptUI : MonoBehaviour
{
    [SerializeField] private ArmyScript _armyScript;
    [SerializeField] private ArmyData _armyData;
    [SerializeField] private EnemyArmyScript _enemyArmyScript;
    [SerializeField] private EnemyArmyData _enemyArmyData;
    [SerializeField] private BattleScript _battleScript;

    //битва
    [HideInInspector] public bool TheBattleIsOn = false;

    private float[] _battleArmyCheck = new float[3];
    private float[] _enemybattleArmyCheck = new float[3];

    private float GameMinute;
    [SerializeField] private Text[] _ourUnits;
    [SerializeField] private Text[] _enemyUnits;

    //визуал
    [SerializeField] private Sprite[] UnitImage;
    [SerializeField] private Sprite[] EnemyUnitImage;

    [SerializeField] private GameObject[] VisualBattleImage;
    [SerializeField] private GameObject[] VisualEnemyBattleImage;

    [SerializeField] private Text[] VisualBattleHPUI;
    [SerializeField] private Text[] VisualEnemyBattleHPUI;

    [SerializeField] private Text[] VisualBattleNameUI;
    [SerializeField] private Text[] VisualEnemyBattleNameUI;

    [SerializeField] private TextMeshProUGUI BattleWinnerUI;
    [SerializeField] private TextMeshProUGUI ValueRoundsUI;

    private float[] _startBattleArmy = new float[16];
    private float[] _startBattleEnemyArmy = new float[16];



    void Start()
    {
        VisualBattle();
    }

    void FixedUpdate()
    {

        if (TheBattleIsOn == true)
        {
            GameMinute = GameMinute + 30 * Time.deltaTime;
            BattleWinnerUI.text = "";

            if (GameMinute >= 60.0f)
            {
                ValueRoundsUI.text += 1;
                GameMinute = 0.0f;
                _battleScript.Battle();

                if ((_enemybattleArmyCheck[0] == 0) || (_battleArmyCheck[0] == 0))
                {
                    if ((_enemybattleArmyCheck[0] == 0) && (_battleArmyCheck[0] == 0))
                    {
                        BattleWinnerUI.text = "Ничья";
                    }

                    else if (_enemybattleArmyCheck[0] == 0)
                    {
                        BattleWinnerUI.text = "Победа гномов";
                    }

                    else if (_battleArmyCheck[0] == 0)
                    {
                        BattleWinnerUI.text = "Победа орков";
                    }

                    TheBattleIsOn = false;

                    _enemyArmyScript.enemybattleArmyReserves = _startBattleEnemyArmy;
                    _armyScript.BattleArmyReserves = _startBattleArmy;

                    _battleScript.EnemyArmyDistribution();
                    _battleScript.ArmyDistribution();
                }
            }

            VisualBattle();
        }
    }

    public void Clear()
    {
        TheBattleIsOn = false;
        BattleWinnerUI.text = "";

        Array.Clear(_startBattleArmy, 0, _startBattleArmy.Length);
        Array.Clear(_startBattleEnemyArmy, 0, _startBattleEnemyArmy.Length);
        Array.Clear(_enemyArmyScript.enemybattleArmyReserves, 0,_enemyArmyScript.enemybattleArmyReserves.Length);
        Array.Clear(_armyScript.BattleArmyReserves, 0, _armyScript.BattleArmyReserves.Length);
        Array.Clear(_enemyArmyScript.enemybattle, 0, _enemyArmyScript.enemybattle.Length);
        Array.Clear(_armyScript.battle, 0, _armyScript.battle.Length);

        ValueRoundsUI.text = "0";

        for (int unitType = 0; unitType < 16; unitType++)
        {
            UpdateValueUnits(unitType);

        }

        _battleScript.EnemyArmyDistribution();
        _battleScript.ArmyDistribution();

        VisualBattle();
    }

    public void Distribution()
    {
        _startBattleEnemyArmy = _enemyArmyScript.enemybattleArmyReserves;
        _startBattleArmy = _armyScript.BattleArmyReserves;

        _battleScript.EnemyArmyDistribution();
        _battleScript.ArmyDistribution();

        VisualBattle();
    }

    public void startBattle()
    {
        TheBattleIsOn = true;
        ValueRoundsUI.text = "0";
    }

    public void AddEnemyUnit(int unitType)
    {
        _enemyArmyScript.enemybattleArmyReserves[unitType] += 1;
        UpdateValueUnits(unitType);
    }

    public void AddOurUnit(int unitType)
    {
        _armyScript.BattleArmyReserves[unitType] += 1;
        UpdateValueUnits(unitType);
    }

    public void DecreaseEnemyUnit(int unitType)
    {
        if (_enemyArmyScript.enemybattleArmyReserves[unitType] > 0)
        {
            _enemyArmyScript.enemybattleArmyReserves[unitType] -= 1;
            UpdateValueUnits(unitType);
        }

    }

    public void DecreaseOurUnit(int unitType)
    {
        if (_armyScript.BattleArmyReserves[unitType] > 0)
        {
            _armyScript.BattleArmyReserves[unitType] -= 1;
            UpdateValueUnits(unitType);
        }

    }

    private void UpdateValueUnits(int unitType)
    {
        _ourUnits[unitType].text = _armyScript.BattleArmyReserves[unitType].ToString();
        _enemyUnits[unitType].text = _enemyArmyScript.enemybattleArmyReserves[unitType].ToString();
    }
    public void VisualBattle()
    {
        for (int i = 0, i1 = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 19; j++, i1++)
            {
                for (int j1 = 1; j1 <= 16; j1++)
                {
                    if (_enemyArmyScript.enemybattle[i, j] == j1)
                    {
                        VisualEnemyBattleImage[i1].GetComponent<UnityEngine.UI.Image>().sprite = EnemyUnitImage[j1 - 1];
                        VisualEnemyBattleNameUI[i1].text = _enemyArmyData.OrcsArmyName[j1 - 1];
                        VisualEnemyBattleHPUI[i1].text = _enemyArmyScript.enemybattleHP[i, j].ToString();
                    }
                    else if (_enemyArmyScript.enemybattle[i, j] == 0)
                    {
                        VisualEnemyBattleImage[i1].GetComponent<UnityEngine.UI.Image>().sprite = EnemyUnitImage[16];
                        VisualEnemyBattleNameUI[i1].text = "-";
                        VisualEnemyBattleHPUI[i1].text = _enemyArmyScript.enemybattleHP[i, j].ToString();
                    }

                    if (_armyScript.battle[i, j] == j1)
                    {
                        VisualBattleImage[i1].GetComponent<UnityEngine.UI.Image>().sprite = UnitImage[j1 - 1];
                        VisualBattleNameUI[i1].text = _armyData.ArmyName[j1 - 1];
                        VisualBattleHPUI[i1].text = _armyScript.BattleHP[i, j].ToString();
                    }
                    else if (_armyScript.battle[i, j] == 0)
                    {
                        VisualBattleImage[i1].GetComponent<UnityEngine.UI.Image>().sprite = UnitImage[16];
                        VisualBattleNameUI[i1].text = "-";
                        VisualBattleHPUI[i1].text = _armyScript.BattleHP[i, j].ToString();
                    }
                }
            }
        }
    }
}
