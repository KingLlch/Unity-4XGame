using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    [HideInInspector] public UnityEvent changeHour;
    [HideInInspector] public UnityEvent changeDay;
    [HideInInspector] public UnityEvent changeMounth;
    [HideInInspector] public UnityEvent changeYear;

    //����� � ��� ����������� � ����������
    public int GameYear = 1444;
    public int GameMonth = 4;
    public int GameDay = 1;
    public int GameHour = 12;
    public float GameMinute = 1;
    public string _stringMonth = "����������";

    private float _gameTimeSpeed = 1;

    [SerializeField] private Text _textTimeUI;
    [SerializeField] private Text _textTimeYearUI;
    [SerializeField] private GameObject _buttonPauseTimeUI;
    [SerializeField] private GameObject _buttonResumeTimeUI;
    [SerializeField] private GameObject[] _gameObjectTimeSpeed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //���������� ���� �� �����
        {
            if (Time.timeScale > 0) { pause(); }
            else if (Time.timeScale == 0) { resume(); }
        }
    }

    void FixedUpdate()
    {
        GameMinute += 30 * Time.deltaTime; // 1 �������� ������� = 30 ������� �����

        //����������� ������� � UI
        _textTimeUI.text = GameDay.ToString() + " ���� " + _stringMonth + " " + GameHour.ToString() + " : " + Mathf.Round(GameMinute).ToString();
        _textTimeYearUI.text = GameYear.ToString() + " ���";

        if (GameMinute >= 60.0f) //����� ����
        {
            GameMinute = 0.0f;
            GameHour++;
            changeHour.Invoke();
        }

        if (GameHour >= 24.0f) //����� ���
        {
            GameHour = 0;
            GameDay++;
            changeDay.Invoke();
        }

        if (GameDay >= 31.0f) //����� ������
        {
            GameDay = 1;
            GameMonth++;

            //�������� �������
            switch (GameMonth)
            {
                case 1:
                    _stringMonth = ((MounthName)0).ToString();
                    break;
                case 2:
                    _stringMonth = ((MounthName)1).ToString();
                    break;
                case 3:
                    _stringMonth = ((MounthName)2).ToString();
                    break;
                case 4:
                    _stringMonth = ((MounthName)3).ToString();
                    break;
                case 5:
                    _stringMonth = ((MounthName)4).ToString();
                    break;
                case 6:
                    _stringMonth = ((MounthName)5).ToString();
                    break;
                case 7:
                    _stringMonth = ((MounthName)6).ToString();
                    break;
                case 8:
                    _stringMonth = ((MounthName)7).ToString();
                    break;
                case 9:
                    _stringMonth = ((MounthName)8).ToString();
                    break;
                case 10:
                    _stringMonth = ((MounthName)9).ToString();
                    break;
                case 11:
                    _stringMonth = ((MounthName)10).ToString();
                    break;
                case 12:
                    _stringMonth = ((MounthName)11).ToString();
                    break;

            }

            changeMounth.Invoke();
        }

        if (GameMonth >= 13.0f) //����� ����
        {
            GameMonth = 1;
            GameYear++;
            changeYear.Invoke();
        }
    }

    private enum MounthName //�������� �������
    {
        ��������,
        �������,
        ���������,
        ����������,
        ����������,
        ���������,
        ���������,
        ���������,
        ��������,
        ����������,
        ����������,
        ��������
    }

    public void time_up() //��������� �������
    {
        if ((Time.timeScale == 1) || (Time.timeScale == 0 && _gameTimeSpeed == 1)) { if (Time.timeScale == 0 && _gameTimeSpeed == 1) { _gameTimeSpeed = 2; } else { Time.timeScale = 2; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[1].SetActive(true); return; }
        if ((Time.timeScale == 2) || (Time.timeScale == 0 && _gameTimeSpeed == 2)) { if (Time.timeScale == 0 && _gameTimeSpeed == 2) { _gameTimeSpeed = 8; } else { Time.timeScale = 8; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[2].SetActive(true); return; }
        if ((Time.timeScale == 8) || (Time.timeScale == 0 && _gameTimeSpeed == 8)) { if (Time.timeScale == 0 && _gameTimeSpeed == 8) { _gameTimeSpeed = 48; } else { Time.timeScale = 48; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[3].SetActive(true); return; }
        if ((Time.timeScale == 48) || (Time.timeScale == 0 && _gameTimeSpeed == 48)) { if (Time.timeScale == 0 && _gameTimeSpeed == 48) { _gameTimeSpeed = 336; } else { Time.timeScale = 336; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[4].SetActive(true); return; }
    }

    public void time_down() //���������� �������
    {
        if ((Time.timeScale == 336) || (Time.timeScale == 0 && _gameTimeSpeed == 336)) { if (Time.timeScale == 0 && _gameTimeSpeed == 336) { _gameTimeSpeed = 48; } else { Time.timeScale = 48; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[4].SetActive(false); return; }
        if ((Time.timeScale == 48) || (Time.timeScale == 0 && _gameTimeSpeed == 48)) { if (Time.timeScale == 0 && _gameTimeSpeed == 48) { _gameTimeSpeed = 8; } else { Time.timeScale = 8; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[3].SetActive(false); return; }
        if ((Time.timeScale == 8) || (Time.timeScale == 0 && _gameTimeSpeed == 8)) { if (Time.timeScale == 0 && _gameTimeSpeed == 8) { _gameTimeSpeed = 2; } else { Time.timeScale = 2; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[2].SetActive(false); return; }
        if ((Time.timeScale == 2) || (Time.timeScale == 0 && _gameTimeSpeed == 2)) { if (Time.timeScale == 0 && _gameTimeSpeed == 2) { _gameTimeSpeed = 1; } else { Time.timeScale = 1; _gameTimeSpeed = Time.timeScale; } _gameObjectTimeSpeed[1].SetActive(false); return; }
    }

    public void pause() //�����
    {
        Time.timeScale = 0;
        _buttonPauseTimeUI.SetActive(false);
        _buttonResumeTimeUI.SetActive(true);
    }

    public void resume() //�����������
    {
        Time.timeScale = _gameTimeSpeed;
        _buttonResumeTimeUI.SetActive(false);
        _buttonPauseTimeUI.SetActive(true);
    }
}
