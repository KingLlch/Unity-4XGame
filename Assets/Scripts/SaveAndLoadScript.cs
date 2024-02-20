using UnityEngine;

public class SaveAndLoadScript : MonoBehaviour
{
    [SerializeField] private TimeScript _timeScript;
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private ModifierScript _modifierScript;
    [SerializeField] private EnemyArmyScript _enemyArmyScript;
    [SerializeField] private EnemyArmyData _enemyArmyData;
    [SerializeField] private ArmyScript _armyScript;
    [SerializeField] private ArmyData _armyData;
    [SerializeField] private BuildingsScript _buildingsScript;

    public void savegame()
    {
        //сохранение времени
        PlayerPrefs.SetInt("GameYear", _timeScript.GameYear);
        PlayerPrefs.SetInt("GameMonth", _timeScript.GameMonth);
        PlayerPrefs.SetInt("GameDay", _timeScript.GameDay);
        PlayerPrefs.SetInt("GameHour", _timeScript.GameHour);
        PlayerPrefs.SetFloat("GameMinute", _timeScript.GameMinute);

        //ресурсы
        for (int i = 0; i <= 5; i++)
        {
            PlayerPrefs.SetFloat("Resources" + i, _resourcesManagerScript.Resources[i]);
        }

        PlayerPrefs.SetFloat("AllArmyGnoms", _resourcesManagerScript.AllArmyGnoms);
        PlayerPrefs.SetFloat("AllWorkerGnoms", _resourcesManagerScript.AllWorkerGnoms);

        //здания
        for (int i = 0; i <= 4; i++)
        {
            PlayerPrefs.SetFloat("Buildings" + i, _buildingsScript.Buildings[0, i]);
            PlayerPrefs.SetFloat("CurrentWorkerGnoms" + i, _resourcesManagerScript.WorkerGnomsCurrent[i]);
            PlayerPrefs.SetFloat("MaxWorkerGnoms" + i, _resourcesManagerScript.WorkerGnomsMax[i]);

        }

        PlayerPrefs.SetFloat("ModifierCost", _buildingsScript.ModifierCost);

        //армия
        for (int i = 0; i <= 15; i++)
        {
            PlayerPrefs.SetFloat("Army" + i, _armyData.Army[0, i]);
            PlayerPrefs.SetFloat("EnemyArmy" + i, _enemyArmyScript.OrcsArmy[i]);

        }

        //сохранение модификаторов
        PlayerPrefs.SetFloat("foodMaintenanceModifier", _modifierScript.foodMaintenanceModifier);
        PlayerPrefs.SetFloat("armyMaintenanceModifier", _modifierScript.armyMaintenanceModifier);
        PlayerPrefs.SetFloat("devastationModifier", _modifierScript.devastationModifier);

        PlayerPrefs.Save();
    }

    public void loadgame()
    {
        //загрузка времени
        _timeScript.GameYear = PlayerPrefs.GetInt("GameYear");
        _timeScript.GameMonth = PlayerPrefs.GetInt("GameMonth");
        _timeScript.GameDay = PlayerPrefs.GetInt("GameDay");
        _timeScript.GameHour = PlayerPrefs.GetInt("GameHour");
        _timeScript.GameMinute = PlayerPrefs.GetFloat("GameMinute");

        //ресурсы
        for (int i = 0; i <= 5; i++)
        {
            _resourcesManagerScript.Resources[i] = PlayerPrefs.GetFloat("Resources" + i);
        }

        _resourcesManagerScript.AllArmyGnoms = PlayerPrefs.GetFloat("AllArmyGnoms");
        _resourcesManagerScript.AllWorkerGnoms = PlayerPrefs.GetFloat("AllWorkerGnoms");

        //здания
        for (int i = 0; i <= 4; i++)
        {
            _buildingsScript.Buildings[0, i] = PlayerPrefs.GetFloat("Buildings" + i);
            _resourcesManagerScript.WorkerGnomsCurrent[i] = PlayerPrefs.GetFloat("CurrentWorkerGnoms" + i);
            _resourcesManagerScript.WorkerGnomsMax[i] = PlayerPrefs.GetFloat("MaxWorkerGnoms" + i);

        }

        _buildingsScript.ModifierCost = PlayerPrefs.GetFloat("ModifierCost");

        //армия
        for (int i = 0; i <= 15; i++)
        {
            _armyData.Army[0, i] = PlayerPrefs.GetFloat("Army" + i);
            _enemyArmyScript.OrcsArmy[i] = PlayerPrefs.GetFloat("EnemyArmy" + i);

        }

        //загрузка модификаторов
        _modifierScript.foodMaintenanceModifier = PlayerPrefs.GetFloat("foodMaintenanceModifier");
        _modifierScript.armyMaintenanceModifier = PlayerPrefs.GetFloat("armyMaintenanceModifier");
        _modifierScript.devastationModifier = PlayerPrefs.GetFloat("devastationModifier");

    }
}
