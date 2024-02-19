using UnityEngine;
using TMPro;

public class BookInfoScript : MonoBehaviour
{
    [SerializeField] private TimeScript _timeScript;
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private TextMeshPro _textData;
    [SerializeField] private TextMeshPro _textFortressRiches;
    [SerializeField] private TextMeshPro _textFreeGnoms;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _textData.text = _timeScript.GameDay.ToString() + " " + _timeScript._stringMonth + " " + _timeScript.GameYear.ToString();
        _textFortressRiches.text = _resourcesManagerScript.FortressRiches.ToString();
        _textFreeGnoms.text = _resourcesManagerScript.FreeGnoms.ToString();

    }
}
