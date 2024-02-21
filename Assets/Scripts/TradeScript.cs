using UnityEngine;
using UnityEngine.UI;

public class TradeScript : MonoBehaviour
{
    [SerializeField] private ResourcesManagerScript _resourcesManagerScript;
    [SerializeField] private GameObject TradeImage;
    [SerializeField] private Sprite[] Image;
    [SerializeField] private InputField[] YouTrade;
    [SerializeField] private InputField[] TheyTrade;
    [SerializeField] private Text TradeConfirmUI;
    [SerializeField] private Text TradeText;

    private int Country = 1;
    private float tradeconfirm = 0;
    private string[] _descriptionCountry =
        { "� ������� ����������� ������� ����, �� ���������� ����� ����� ������ ������� ������.",
        "� �������� ���� �������� ���������� ����������� ����� � ������� ����, �� ��������� ������.",
        "������, ������� ������ ��������� � ����, ������ � ���, ��� �������� ����������� ��� ����� ����������.",
        "����� ����� - ����������� ������, ����� ��������� ��� ������� ���������� �������, �� � ��� ������� ���� � ���������.",
        "������, ��� ������� ��������� � ������ ��� ������ �� ��������� �������� ��������, �� ��� ����� ��������� ��� ���� � ���������.",
        "��� ������� ����� ���������� ���� ��������, ������� ����� �������! �� ���� ������ ���������!",
        "���� �����������"};
    public float[,] trade =
    {
        {0,0,0,0,0 },
        {0,0,0,0,0 },
    };

    public float[] tradeCost = { 1, 10, 20, 3, 0.25f };
    public float[,] tradeCostModif =
    { 
        // �������, ��������, �����, �����, ������
        {1, 1, 1, 1, 1 },//������
        {2, 3, 3, 1.5f, 4 },//������
        {2, 1.5f, 4, 1.5f, 3 },//����
        {1.5f, 1.5f, 2, 3, 2 },//�������
        {2, 1.5f, 2.5f, 4, 2 },//���

     };

    private void trade_confirm_value()
    {
        tradeconfirm = 0;
        if ((Country == 6) || (Country == 7))
        {
            return;
        }
        else
        {
            for (int i = 0; i <= 4; i++)
            {
                tradeconfirm += trade[0, i] * tradeCost[i] * tradeCostModif[i, Country] - trade[1, i] * tradeCost[i] * tradeCostModif[i, Country];

            }
        }

        if (tradeconfirm > 0) { TradeConfirmUI.color = Color.green; }
        else { TradeConfirmUI.color = Color.red; }

        TradeConfirmUI.text = tradeconfirm.ToString();
    }

    public void trade_confirm()
    {
        if (tradeconfirm > 0)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (_resourcesManagerScript.Resources[i] < trade[0, i])
                {
                    return;
                }
            }
            for (int i = 0; i <= 4; i++)
            {
                 _resourcesManagerScript.Resources[i] -= trade[0, i];
                 _resourcesManagerScript.Resources[i] += trade[1, i];               
            }
        }
    }

    public void selectTradeCountry(int selectedCountry)
    {
        Country = selectedCountry;
        TradeText.text = _descriptionCountry[selectedCountry];
        TradeImage.GetComponent<Image>().sprite = Image[selectedCountry];
        trade_confirm_value();
    }

    public void valueTradeYouChanged(string Resorces)
    {
        int res = int.Parse(Resorces);
        try
        {
            trade[0, res] = float.Parse(YouTrade[res].text);
            trade_confirm_value();
        }
        catch { }
    }
    public void valueTradeTheyChanged(string Resorces)
    {
        int res = int.Parse(Resorces);
        try
        {
            trade[1, res] = float.Parse(TheyTrade[res].text);
            trade_confirm_value();
        }
        catch { }
    }
}
