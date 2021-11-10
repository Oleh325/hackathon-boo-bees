using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class CatGuide : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI guideText;
    [SerializeField] private GameObject guideCat;

    private const int NumOfRows = 8;
    private int _textRowNumber;

    private readonly string[] _guideTextRows = new string[NumOfRows]
    {
        "To move use W, A, S and D keys",
        "Aim using mouse. To shoot use right-click",
        "Catch a bat and you'll be given +1 heart",
        "Collect skeleton bones to get more bullets",
        "To win - survive all 5 night waves of skeletons",
        "You lose one heart each time skeleton hits you",
        "If you lose all 3 hearts you die",
        "Good luck!"
    };

    private void Start()
    {
        guideText.text = _guideTextRows[_textRowNumber++];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_textRowNumber < NumOfRows)
            {
                guideText.text = "";
                for (int i = 0; i != _guideTextRows[_textRowNumber].Length; i++)
                {
                    guideText.text += _guideTextRows[_textRowNumber][i];
                }

                _textRowNumber++;
            }
            else
            {
                guideText.enabled = false;
                guideCat.SetActive(false);
                enabled = false;
            }
        }
    }
}