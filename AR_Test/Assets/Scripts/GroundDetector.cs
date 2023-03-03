using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    

    private Transform camT;
    [SerializeField] private GameObject Indicator;
    [SerializeField] private GameObject Cube;

    private void Start()
    {
        camT = Camera.main.transform;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;

        Ray ray = new Ray(camT.position, camT.forward);

        if(Physics.Raycast(ray, out hitInfo))
        {
            Indicator.SetActive(true);
            Indicator.transform.position = hitInfo.point;
            if(Input.GetMouseButtonDown(0))
            {
                Cube.SetActive(true);
                Cube.transform.position = Indicator.transform.position;
            }
        }
        else
        {
            Indicator.SetActive(false);
        }
    }
}
