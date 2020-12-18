using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float MovementSpeed { get; set; } = 9f;
    public float ScaleSpeed { get; set; } = 9f;

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
    }

    private void ScaleCamera()
    {
        Offset += Vector3.MoveTowards(MainCamera.transform.position, Player.transform.position, 1) * ScaleSpeed * Input.mouseScrollDelta.y;
    }

    private void MovePlayer()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(h, 0, v).normalized * Time.deltaTime * MovementSpeed;

        Vector3 destination = Player.position + movement;

        Player.position = destination;
    }
}
