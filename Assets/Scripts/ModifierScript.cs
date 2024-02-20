using UnityEngine;
using UnityEngine.UI;

public class ModifierScript : MonoBehaviour
{
    [SerializeField] private ArmyScript _armyScript;
    [SerializeField] private SeasonUIScript _seasonUIScript;

    //объекты
    [SerializeField] private Dropdown food;
    [SerializeField] private Dropdown army;
    [SerializeField] private Dropdown build;
    [SerializeField] private Text BattleEfficiencyUI;
    [SerializeField] private Text devastationModifierUI;
    [SerializeField] private Text workEfficiencyUI;

    //модификаторы
    [HideInInspector] public float workEfficiency = 1;
    [HideInInspector] public float armyMaintenanceModifier = 1;
    [HideInInspector] public float foodMaintenanceModifier = 1;
    [HideInInspector] public float moodModifier = 1;
    [HideInInspector] public float buildMaintenanceModifier = 1;
    [HideInInspector] public float seasonFoodModifier = 1;
    [HideInInspector] public float devastationModifier = 1;

    private void FixedUpdate()
    {
        //модификаторы 
        workEfficiency = foodMaintenanceModifier * buildMaintenanceModifier;
        _armyScript.BattleEfficiency = armyMaintenanceModifier * foodMaintenanceModifier;

        switch (_seasonUIScript.StringSeason)
        {
            case "Winter":
                seasonFoodModifier = 1.5f;
                break;
            case "Spring":
                seasonFoodModifier = 1.2f;
                break;
            case "Summer":
                seasonFoodModifier = 1;
                break;
            case "Autumn":
                seasonFoodModifier = 1.2f;
                break;
        }

        devastationModifierUI.text = (100 - devastationModifier * 100).ToString() + " %";
        BattleEfficiencyUI.text = (_armyScript.BattleEfficiency*100).ToString() + " %";
        workEfficiencyUI.text = (workEfficiency * 100).ToString() + " %";

        if ((100 - devastationModifier * 100) == 0)  devastationModifierUI.color = Color.green; 
        else  devastationModifierUI.color = Color.red; 

        if (_armyScript.BattleEfficiency >= 1)  BattleEfficiencyUI.color = Color.green; 
        else BattleEfficiencyUI.color = Color.red;

        if (workEfficiency >= 1) workEfficiencyUI.color = Color.green;
        else workEfficiencyUI.color = Color.red;

    }

    //изменить множжитель потребления пищи на гнома
    private void food_investment()
    {
        switch (food.value)
        {
            case 0:
                foodMaintenanceModifier = 0.7f;
                break;
            case 1:
                foodMaintenanceModifier = 1;
                break;
            case 2:
                foodMaintenanceModifier = 1.3f;
                break;
        }
    }

    //изменить содержание армии
    private void army_investment()
    {
        switch (army.value)
        {
            case 0:
                armyMaintenanceModifier = 0.7f;
                break;
            case 1:
                armyMaintenanceModifier = 1;
                break;
            case 2:
                armyMaintenanceModifier = 1.3f;
                break;
        }
    }

    //изменить содержание армии
    private void build_investment()
    {
        switch (build.value)
        {
            case 0:
                buildMaintenanceModifier = 0.7f;
                break;
            case 1:
                buildMaintenanceModifier = 1;
                break;
            case 2:
                buildMaintenanceModifier = 1.3f;
                break;
        }
    }
}
