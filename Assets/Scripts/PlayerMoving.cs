using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoving : MonoBehaviour
{
    public float MovementSpeed { get; set; } = 9f;
    public float SprintMultiplier { get; set; } = 1.6f;
    public float ScaleSpeed { get; set; } = 5f;
    public bool IsMoving { get; set; }

    [SerializeField]
    private Camera MainCamera { get; set; }

    private Rigidbody Player { get; set; }
    private Fighting FightingScript { get; set; }
    private Vector3 Offset { get; set; }
    private Animator Animator { get; set; }
    private NavMeshAgent Navigator { get; set; }
    private GameObject WayPointer { get; set; }
    private Vector3 Destination { get; set; }

    private LayerMask MovingLayerMask { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        FightingScript = GetComponent<Fighting>();
        MainCamera = Camera.main;
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        WayPointer = GameObject.Find("WayPointer");
        MovingLayerMask = LayerMask.GetMask("Ground", "Enemy");

        // Offset = Player.transform.position - MainCamera.transform.position;
        Offset = new Vector3(0.0f, -15.1f, 10.8f);

    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.position = Player.transform.position - Offset;
        MainCamera.transform.LookAt(Player.transform);
        ScaleCamera();

        if (Navigator.remainingDistance <= Navigator.stoppingDistance)
        {
            Animator.SetBool("IsWalking", false);
            WayPointer.SetActive(false);
        }
        else
        {
            Animator.SetBool("IsWalking", true);
            if (FightingScript.SelectedEnemy == null)
                WayPointer.SetActive(true);
        }

        MovePlayerClick();
    }

    private void FixedUpdate()
    {
        if (Navigator.velocity.magnitude > 0f)
            Player.rotation = Quaternion.LookRotation(Navigator.destination - Player.position);
        // MovePlayer();
        // RotatePlayer();
    }

    private void ScaleCamera()
    {
        Vector3 cameraMove = MainCamera.transform.forward * ScaleSpeed * Input.mouseScrollDelta.y;
        if ((Offset - cameraMove).magnitude >= 10)
            Offset -= cameraMove;
    }

    private void GettingMouse()
    {

    }

    private void MovePlayerClick()
    {
        if (!Input.GetMouseButtonDown(1))
            return;

        if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, MovingLayerMask))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                Navigator.ResetPath();
                FightingScript.SelectedEnemy = null;
                FightingScript.IsAttacking = false;
                Navigator.stoppingDistance = 0.5f;

                WayPointer.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);

                Navigator.SetDestination(hit.point);
                // Navigator.SetDestination(new Vector3(hit.point.x, Player.position.y, hit.point.z));

                Player.rotation = Quaternion.LookRotation(Navigator.destination - Player.position);
            }
            else if (hit.collider.gameObject.layer == 7)
            {
                Navigator.ResetPath();
                FightingScript.SelectedEnemy = hit.collider.gameObject.GetComponent<Enemy>();
            }
        }
    }
}
