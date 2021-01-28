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

    private Fighting FightingScript { get; set; }
    private Vector3 Offset { get; set; }
    private Animator Animator { get; set; }
    public NavMeshAgent Navigator { get; set; }
    private GameObject WayPointer { get; set; }
    private Vector3 Destination { get; set; }

    private LayerMask MovingLayerMask { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        FightingScript = GetComponent<Fighting>();
        MainCamera = Camera.main;
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        WayPointer = GameObject.Find("WayPointer");
        MovingLayerMask = LayerMask.GetMask("Ground", "Enemy");

        Navigator.updateRotation = false;
        // Offset = Player.transform.position - MainCamera.transform.position;
        Offset = new Vector3(0.0f, -15.1f, 10.8f);

    }

    // Update is called once per frame
    void Update()
    {
        MainCamera.transform.position = transform.position - Offset;
        MainCamera.transform.LookAt(transform);
        ScaleCamera();

        if (Navigator.remainingDistance <= Navigator.stoppingDistance)
        {
            WayPointer.SetActive(false);
        }
        else
        {
            if (FightingScript.SelectedEnemy == null)
                WayPointer.SetActive(true);
        }

        MovePlayerClick();
        // Debug.Log("Has path: " + Navigator.hasPath + ", status: " + Navigator.pathStatus);
    }

    private void FixedUpdate()
    {
        float speed = Navigator.velocity.magnitude / Navigator.speed;
        Animator.SetFloat("Speed", speed);
    }

    private void ScaleCamera()
    {
        Vector3 cameraMove = MainCamera.transform.forward * ScaleSpeed * Input.mouseScrollDelta.y;
        if ((Offset - cameraMove).magnitude >= 10)
            Offset -= cameraMove;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, Navigator.stoppingDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Navigator.destination, 0.1f);
    }
    private void MovePlayerClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, MovingLayerMask))
            {
                Quaternion lookTo = Quaternion.LookRotation(hit.point - transform.position);

                transform.eulerAngles = new Vector3(0, lookTo.eulerAngles.y, 0);

                Navigator.ResetPath();

                if (hit.collider.gameObject.layer == 6) // hit floor layer
                {
                  //  FightingScript.SelectedEnemy = null;
                    Navigator.stoppingDistance = 0f;

                    WayPointer.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
                    WayPointer.SetActive(true);
                    Navigator.SetDestination(hit.point);

                }
                else if (hit.collider.gameObject.layer == 7) // hit enemy layer
                {

                    WayPointer.SetActive(false);
                    Navigator.stoppingDistance = FightingScript.AttackRange;
                    Navigator.SetDestination(hit.collider.gameObject.transform.position);
                    FightingScript.SelectedEnemy = hit.transform.GetComponentInParent<Enemy>();
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.layer != 7)
                {
                    FightingScript.SelectedEnemy = null;
                }else{
                    FightingScript.SelectedEnemy = hit.collider.gameObject.GetComponent<Enemy>();
                    Debug.Log("left clicked enemy");
                }
            }
            else
            {
                FightingScript.SelectedEnemy = null;
            }
        }
    }
}
