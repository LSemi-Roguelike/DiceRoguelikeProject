using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManage : MonoBehaviour
{
    List<GameObject> buttons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject area = GameObject.FindGameObjectWithTag("ButtonArea");
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons.Add(area.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < buttons.Count; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
