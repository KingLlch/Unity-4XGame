using UnityEngine;
using BayatGames.SaveGameFree;
using Zenject;

public class SaveData
{
    public int GameYear;
    public int GameMonth;
    public int GameDay;
    public int GameHour;
    public float GameMinute;

    public float[] Resources;

    public float AllArmyGnoms;
    public float AllWorkerGnoms;

    public float[] Buildings;
    public float[] CurrentWorkerGnoms;
    public float[] MaxWorkerGnoms;

    public float ModifierCost;

    public float[] Army;
    public float[] EnemyArmy;
    public float foodMaintenanceModifier;
    public float armyMaintenanceModifier;
    public float devastationModifier;
}

public class SaveAndLoadScript : MonoBehaviour
{
    private static SaveData instance = new SaveData();

    [Inject] private TimeScript _timeScript;
    [Inject] private ResourcesManagerScript _resourcesManagerScript;
    [Inject] private ModifierScript _modifierScript;
    [Inject] private EnemyArmyScript _enemyArmyScript;
    [Inject] private ArmyData _armyData;
    [Inject] private BuildingsScript _buildingsScript;

    public void savegame()
    {  
        //сохранение времени
        instance.GameYear = _timeScript.GameYear;
        instance.GameMonth = _timeScript.GameMonth;
        instance.GameDay = _timeScript.GameDay;
        instance.GameHour = _timeScript.GameHour;
        instance.GameMinute = _timeScript.GameMinute;

        //ресурсы
        for (int i = 0; i <= 5; i++)
        {
            _resourcesManagerScript.Resources[i] = instance.Resources[i];
        }


        _resourcesManagerScript.AllArmyGnoms = instance.AllArmyGnoms;
        _resourcesManagerScript.AllWorkerGnoms = instance.AllWorkerGnoms;

        //здания
        for (int i = 0; i <= 4; i++)
        {
            _buildingsScript.Buildings[0, i] = instance.Buildings[i];
            _resourcesManagerScript.WorkerGnomsCurrent[i] = instance.CurrentWorkerGnoms[i];
            _resourcesManagerScript.WorkerGnomsMax[i] = instance.MaxWorkerGnoms[i];
        }
        _buildingsScript.ModifierCost = instance.ModifierCost;

        //армия
        for (int i = 0; i <= 15; i++)
        {
            _armyData.Army[0, i] = instance.Army[i];
            _enemyArmyScript.OrcsArmy[i] = instance.EnemyArmy[i];
        }

        //сохранение модификаторов
        _modifierScript.foodMaintenanceModifier = instance.foodMaintenanceModifier;
        _modifierScript.armyMaintenanceModifier = instance.armyMaintenanceModifier;
        _modifierScript.devastationModifier = instance.devastationModifier;

        SaveGame.Save("SaveData", instance);
    
    }

    public void loadgame()
    {
        
        //сохранение времени
        _timeScript.GameYear = instance.GameYear;
        _timeScript.GameMonth = instance.GameMonth;
        _timeScript.GameDay = instance.GameDay;
        _timeScript.GameHour = instance.GameHour;
        _timeScript.GameMinute = instance.GameMinute;

        //ресурсы
        for (int i = 0; i <= 5; i++)
        {
            instance.Resources[i] = _resourcesManagerScript.Resources[i];
        }


        instance.AllArmyGnoms = _resourcesManagerScript.AllArmyGnoms;
        instance.AllWorkerGnoms = _resourcesManagerScript.AllWorkerGnoms;

        //здания
        for (int i = 0; i <= 4; i++)
        {
            instance.Buildings[i] = _buildingsScript.Buildings[0, i];
            instance.CurrentWorkerGnoms[i] = _resourcesManagerScript.WorkerGnomsCurrent[i];
            instance.MaxWorkerGnoms[i] = _resourcesManagerScript.WorkerGnomsMax[i];
        }
        instance.ModifierCost = _buildingsScript.ModifierCost;

        //армия
        for (int i = 0; i <= 15; i++)
        {
            instance.Army[i] = _armyData.Army[0, i];
            instance.EnemyArmy[i] = _enemyArmyScript.OrcsArmy[i];
        }

        //сохранение модификаторов
        instance.foodMaintenanceModifier = _modifierScript.foodMaintenanceModifier;
        instance.armyMaintenanceModifier = _modifierScript.armyMaintenanceModifier;
        instance.devastationModifier = _modifierScript.devastationModifier;

        SaveGame.Load("SaveData", instance);
    
    }
}
