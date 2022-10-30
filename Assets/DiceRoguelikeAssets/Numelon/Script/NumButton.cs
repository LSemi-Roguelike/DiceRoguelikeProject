using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumButton : MonoBehaviour
{
    [SerializeField] int myNumber;
    [SerializeField] Text viewText;

    // Start is called before the first frame update
    public void OnClick()
    {
        string str = viewText.text.Replace(myNumber.ToString(), "");
        str += myNumber;
        if (str.Length > 3)
        {
            str = str.Substring(str.Length - 3);
        }
        viewText.text = str;
    }
}
