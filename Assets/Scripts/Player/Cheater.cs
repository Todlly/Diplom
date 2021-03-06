using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Dummy;
    public GameObject Skeleton;
    public GameObject Dragon;

    private LayerMask floorMask;
    void Start()
    {
        floorMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        PlaceEnemy();
    }

    private void PlaceEnemy()
    {

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                Instantiate(Dummy, hit.point, Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Instantiate(Skeleton, hit.point, Quaternion.identity);
            }else if (Input.GetKeyDown(KeyCode.G))
            {
                Instantiate(Dragon, hit.point, Quaternion.identity);
            }
        }
    }
}
