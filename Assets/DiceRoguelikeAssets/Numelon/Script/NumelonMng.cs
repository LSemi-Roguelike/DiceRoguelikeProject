using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumelonMng : MonoBehaviour
{
    private Numelon num;

    public static NumelonMng instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        num = new Numelon();
        Debug.Log(num.GetAns());
        Debug.Log(num.CheckAns(123));
        Debug.Log(num.CheckAns(456));
        Debug.Log(num.CheckAns(789));
    }

    public (int hit, int brow) Check(int n)
    {
        return num.CheckAns(n);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
