using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    [SerializeField] Text inText;
    [SerializeField] Text resultText;
    [SerializeField] GameObject coverPanel;

    public void OnClick()
    {
        if(inText.text.Length < 3)
        {
            return;
        }
        (int hit, int brow) result = NumelonMng.instance.Check(int.Parse(inText.text));
        if(result.hit == 3)
        {
            resultText.text = "Correct!";
            coverPanel.SetActive(true);
        }
        else
        {
            resultText.text = "Hit : " + result.hit + "  Brow : " + result.brow; 
        }
    }
}
