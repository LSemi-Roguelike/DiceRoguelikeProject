using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickPoint : MonoBehaviour
{

    private Vector3 currentPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            MoveToCursor();
        }

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            currentPosition = hit.point;
            Debug.Log(currentPosition);
            currentPosition.y = 2;
            transform.LookAt(currentPosition);
        }
    }

}
