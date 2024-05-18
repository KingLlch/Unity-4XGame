using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VisualBattleScript : MonoBehaviour
{
    [Inject] private ResourcesManagerScript _resourcesManagerScript;
    [Inject] private BattleScript _battleScript;
    [Inject] private EnemyArmyScript _enemyArmyScript;
    [Inject] private EnemyArmyData _enemyArmyData;
    [Inject] private ArmyScript _armyScript;
    [Inject] private ArmyData _armyData;

    [SerializeField] private Sprite[] UnitImage;
    [SerializeField] private Sprite[] EnemyUnitImage;

    [SerializeField] private GameObject[] VisualBattleImage;
    [SerializeField] private GameObject[] VisualEnemyBattleImage;

    [SerializeField] private Text[] VisualBattleHPUI;
    [SerializeField] private Text[] VisualEnemyBattleHPUI;

    [SerializeField] private Text[] VisualBattleNameUI;
    [SerializeField] private Text[] VisualEnemyBattleNameUI;

    private void FixedUpdate()
    {
        if (_battleScript.TheBattleIsOn == true)  VisualBattle();
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
                        VisualEnemyBattleImage[i1].GetComponent<Image>().sprite = EnemyUnitImage[j1 - 1];
                        VisualEnemyBattleNameUI[i1].text = _enemyArmyData.OrcsArmyName[j1 - 1];
                        VisualEnemyBattleHPUI[i1].text = _enemyArmyScript.enemybattleHP[i, j].ToString();
                    }
                    else if (_enemyArmyScript.enemybattle[i, j] == 0)
                    {
                        VisualEnemyBattleImage[i1].GetComponent<Image>().sprite = EnemyUnitImage[16];
                        VisualEnemyBattleNameUI[i1].text = "-";
                        VisualEnemyBattleHPUI[i1].text = _enemyArmyScript.enemybattleHP[i, j].ToString();
                    }

                    if (_armyScript.battle[i, j] == j1)
                    {
                        VisualBattleImage[i1].GetComponent<Image>().sprite = UnitImage[j1 - 1];
                        VisualBattleNameUI[i1].text = _armyData.ArmyName[j1 - 1];
                        VisualBattleHPUI[i1].text = _armyScript.BattleHP[i, j].ToString();
                    }
                    else if (_armyScript.battle[i, j] == 0)
                    {
                        VisualBattleImage[i1].GetComponent<Image>().sprite = UnitImage[16];
                        VisualBattleNameUI[i1].text = "-";
                        VisualBattleHPUI[i1].text = _armyScript.BattleHP[i, j].ToString();
                    }
                }
            }
        }
    }
}
