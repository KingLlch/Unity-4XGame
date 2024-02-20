using UnityEngine;

public class ArmyScript : MonoBehaviour
{
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private ArmyData _armyData;

    [HideInInspector] public float[] CreateDuration = { 10, 30, 30, 45, 45, 60, 120, 20, 20, 30, 30, 60, 30, 45, 60, 90 };//длительность найма юнита

    [HideInInspector] public float[] CreateValue = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //количество найма 

    [HideInInspector] public float BattleEfficiency = 1;

    [HideInInspector] public float[] BattleArmyStart = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //армия на начало битвы
    [HideInInspector] public float[] BattleArmyReserves = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //резерв армии

    [HideInInspector]
    public float[,] battle =
    {

        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },

    };

    [HideInInspector]
    public float[,] BattleHP =
    {

        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },

    };

    public void AddOurUnit(int unitType)
    {
        if (_resourcesManagerScript.FreeGnoms > _armyData.ArmyCreate[5, unitType])
        {
            for (int i = 1; i <= 4; i++)
            {
                if (_resourcesManagerScript.Resources[i - 1] < _armyData.ArmyCreate[i, unitType] * _armyData.ArmyCreate[0, unitType]) { return; }
            }

            for (int i = 0; i <= 15; i++)
            {
                if (_resourcesManagerScript.ArmyCreateTime[i] == 0)
                {
                    _resourcesManagerScript.ArmyCreateTime[i] = CreateDuration[unitType];
                    _resourcesManagerScript.ArmyCreateType[i] = unitType;
                    CreateValue[unitType] += 1;
                    break;

                }

            }

            for (int i = 1; i <= 4; i++)
            {
                _resourcesManagerScript.Resources[i - 1] -= _armyData.ArmyCreate[i, unitType] * _armyData.ArmyCreate[0, unitType];
            }
            _resourcesManagerScript.AllArmyGnoms += _armyData.ArmyCreate[5, unitType];
        }
    }

    public void DecreaseOurUnit(int unitType)
    {
        if (0 < _armyData.Army[0, unitType])
        {
            for (int i = 1; i < 4; i++)
            {
                _resourcesManagerScript.Resources[i] += _armyData.ArmyCreate[i, unitType];
            }
            _armyData.Army[0, unitType] -= 1;
            _resourcesManagerScript.AllArmyGnoms -= _armyData.ArmyCreate[5, unitType];
        }
    }
}
