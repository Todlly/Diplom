using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float MovementSpeed { get; set; } = 9f;
    public float SprintMultiplier { get; set; } = 1.6f;
    public float ScaleSpeed { get; set; } = 5f;

    [SerializeField]
    private Camera MainCamera { get; set; }

    private Rigidbody Player { get; set; }
    private Vector3 Offset { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        MainCamera = Camera.main;
        Offset = Player.transform.position - MainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.position = Player.transform.position - Offset;
        ScaleCamera();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void ScaleCamera()
    {
        Vector3 cameraMove = MainCamera.transform.forward * ScaleSpeed * Input.mouseScrollDelta.y;
        if ((Offset - cameraMove).magnitude >= 10)
            Offset -= cameraMove;
    }

    private void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float sprint = Input.GetKey(KeyCode.LeftShift) ? SprintMultiplier : 1f;
     //   Debug.Log("Sprint: " + sprint + "\nSpeed: " + MovementSpeed * sprint + "\nSprint pressed: " + Input.GetKeyDown(KeyCode.LeftShift).ToString());
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Debug.Log("Shift pressed");

        Vector3 movement = new Vector3(h, 0, v).normalized * Time.deltaTime * (MovementSpeed * sprint);

        Vector3 destination = Player.position + movement;

        Player.position = destination;
    }

    private void RotatePlayer()
    {
        
        
        if(Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
        {
            GameObject target = hit.collider.gameObject;
            if (target.CompareTag("Ground"))
            {
                Vector3 rotationDestination = hit.point;

                Player.MoveRotation(Quaternion.LookRotation(rotationDestination - Player.position));
            }
        }
    }
}
