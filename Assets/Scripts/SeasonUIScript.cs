using UnityEngine;
using Zenject;

public class SeasonUIScript : MonoBehaviour
{
    [Inject] private TimeScript _timeScript;

    [HideInInspector] public float SeasonFoodModifier;
    [HideInInspector] public string StringSeason;

    [SerializeField] private GameObject circle;

    private void FixedUpdate()
    {
        circle.transform.eulerAngles = new Vector3(0, 0, 360 * ( ( ( _timeScript.GameMonth-1) * 30*24 + ((_timeScript.GameDay-1) * 24) )/ 8640) );

        if (_timeScript.GameMonth == 1) { StringSeason = "Winter"; }
        else if (_timeScript.GameMonth == 4) { StringSeason = "Spring"; }
        else if (_timeScript.GameMonth == 7) { StringSeason = "Summer"; }
        else if (_timeScript.GameMonth == 10) { StringSeason = "Autumn"; }

    }
}
