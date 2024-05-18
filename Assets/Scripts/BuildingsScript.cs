using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BuildingsScript : MonoBehaviour
{
    [Inject] private ResourcesManagerScript _resourcesManagerScript;

    //добывающие здания 
    public float[,] Buildings =
        {

        { 2, 1, 0, 0, 2 },
        { 10, 10, 10, 10, 10 }, //з
        { 1, 0, 1, 1, 0 }, //ж
        { 0, 0, 0, 0, 0 }, //м
        { 0, 1, 0, 0, 1 }, //д

        }; //колво зданий и их содержание

    public float[] BuildingsProduct = { 70, 15, 1.5f, 15, 200 };
    public float[,] ResourcesForBuildingMine = {
        { 0, 10, 0, 20, 0  },
        { 100, 0, 0, 10, 0},
        { 400, 20, 0, 0, 0},
        { 200, 20, 0, 0, 0},
        { 200, 0, 0, 20, 10},
    }; 

    [SerializeField] private Text[] BuildingBuildUI;
    [SerializeField] private Text[] MaxBuildingBuildUI;
    [SerializeField] private Text[] CurrentBuildingBuildUI;


    public float[] buidingduration = { 30, 30, 30, 10, 10 };//длительность постройки зданий

    public float[] ResourcesForMaxBuildingUp = { 200, 20, 10, 0, 0 };
    public float ModifierCost = 1;

    private void FixedUpdate()
    {
        for (int i = 0; i <= 4; i++)
        {
            for (int j = 0; j <= 4; j++)
            { 
                BuildingBuildUI[(j+i*5)].text = ResourcesForBuildingMine[i,j].ToString();
            }

            MaxBuildingBuildUI[i].text = (ResourcesForMaxBuildingUp[i] * ModifierCost).ToString();
            CurrentBuildingBuildUI[i].text = Buildings[0,i].ToString();
        }

    }

    public void max_buildings_up()
    {
        for (int a = 0; a <= 4; a++)
        {        
            if  (_resourcesManagerScript.Resources[a] < ResourcesForMaxBuildingUp[a] * ModifierCost) { return; }
        }

        for (int a = 0; a <= 4; a++)
        {       
            _resourcesManagerScript.BuildingsMax[a] += 5;
            _resourcesManagerScript.Resources[a] -= ResourcesForMaxBuildingUp[a] * ModifierCost;
        }
        ModifierCost += 0.5f;

    }

    public void gnomsWorkersMineUp(int typeWork) //увеличить колво гномов рабочих
    {
        if ((_resourcesManagerScript.FreeGnoms > 0) && (_resourcesManagerScript.WorkerGnomsCurrent[typeWork] < _resourcesManagerScript.WorkerGnomsMax[typeWork]))
        {
            _resourcesManagerScript.WorkerGnomsCurrent[typeWork] += 1;
            _resourcesManagerScript.AllWorkerGnoms += 1;
        }
    }

    public void gnomsWorkersMineDown(int typeWork) //уменьшить колво гномов рабочих
    {
        if (_resourcesManagerScript.WorkerGnomsCurrent[typeWork] > 0)
        {
            _resourcesManagerScript.WorkerGnomsCurrent[typeWork] -= 1;
            _resourcesManagerScript.AllWorkerGnoms -= 1;
        }
    }

    public void buildMine(int typeMine)
    {
        if ((_resourcesManagerScript.BuildingsCurrent[typeMine] < _resourcesManagerScript.BuildingsMax[typeMine]) && (_resourcesManagerScript.CellBuildingInWork < _resourcesManagerScript.CellBuildingValue))
        {
            for (int i = 0; i < 4; i++)
            {
                if (_resourcesManagerScript.Resources[i] < ResourcesForBuildingMine[typeMine, i]) { return; }
            }

            for (int i = 0; i <= 4; i++)
            {
                if ((_resourcesManagerScript.CellBuildingValue >= i + 1) && (_resourcesManagerScript.BuildingCellTime[i] == 0))
                {
                    _resourcesManagerScript.BuildingCellTime[i] = buidingduration[typeMine];
                    _resourcesManagerScript.BuildingsCellType[i] = typeMine;
                    _resourcesManagerScript.CellBuildingInWork += 1;
                    break;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                _resourcesManagerScript.Resources[i] -= ResourcesForBuildingMine[typeMine, i];
            }
            _resourcesManagerScript.BuildingsCurrent[typeMine] += 1;
        }
    }
}
