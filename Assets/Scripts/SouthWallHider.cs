using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouthWallHider : MonoBehaviour
{
    [SerializeField]
    private GameObject SouthWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SouthWall.transform.localScale = new Vector3(SouthWall.transform.localScale.x, SouthWall.transform.localScale.y / 3, SouthWall.transform.localScale.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SouthWall.transform.localScale = new Vector3(SouthWall.transform.localScale.x, SouthWall.transform.localScale.y * 3, SouthWall.transform.localScale.z);
        }
    }

}
