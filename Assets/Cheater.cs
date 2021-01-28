using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Dummy;

    private LayerMask floorMask;
    void Start()
    {
        floorMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        PlaceDummy();
    }

    private void PlaceDummy()
    {
        if (!Input.GetKeyDown(KeyCode.D))
            return;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            GameObject.Instantiate(Dummy, hit.point, Quaternion.identity);
        }
    }
}
