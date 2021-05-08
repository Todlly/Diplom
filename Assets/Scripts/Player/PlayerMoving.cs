using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Enemy;
using static Interactable;
using UnityEngine.UI;
using static Menu;
using UnityEngine.Audio;

public class PlayerMoving : MonoBehaviour
{
    public float ScaleSpeed { get; set; } = 5f;
    private PlayerHealth PlayerHealth;
    public float PlayerMovementSpeed = 10f;


    public Camera MainCamera;
    public AudioClip WalkingSound;
    public AudioMixer mixer;
    private AudioSource WalkingSoundPlayer;
    private AudioMixerGroup mixerGroupSounds;
    

    private Fighting FightingScript { get; set; }
    private Vector3 Offset { get; set; }
    private Animator Animator { get; set; }
    public NavMeshAgent Navigator { get; set; }
    private GameObject WayPointer { get; set; }
    private Sprite Dummy;
    private Animator WayPointerAnimator;
    public bool isHeadingToTarget = false;

    private Interactable selection;
    public Interactable Selection
    {
        get
        {
            return selection;
        }

        set
        {
            if (selection != null)
                selection.Deselect();

            selection = value;

            if (selection != null)
                selection.Select();
        }
    }

    private Image EnemyFrame;

    private LayerMask MovingLayerMask { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
        FightingScript = GetComponent<Fighting>();
        EnemyFrame = GameObject.Find("TargetFrame").GetComponent<Image>();
        //MainCamera = Camera.current;
        Animator = GetComponent<Animator>();
        Navigator = GetComponent<NavMeshAgent>();
        WayPointer = GameObject.Find("WayPointer");
        WayPointerAnimator = WayPointer.GetComponent<Animator>();
        MovingLayerMask = LayerMask.GetMask("Ground", "Interactable");

        Dummy = Resources.Load<Sprite>("Icons/Dummy");

        Navigator.updateRotation = false;
        // Offset = Player.transform.position - MainCamera.transform.position;
        Offset = new Vector3(0.0f, -15.1f, 10.8f);
        Navigator.speed = PlayerMovementSpeed;

        mixerGroupSounds = Resources.Load<AudioMixer>("MainAudioMixer").FindMatchingGroups("Sounds")[0];
        WalkingSoundPlayer = gameObject.AddComponent<AudioSource>();
        WalkingSoundPlayer.clip = WalkingSound;
        WalkingSoundPlayer.loop = true;
        WalkingSoundPlayer.outputAudioMixerGroup = mixerGroupSounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera == null)
            MainCamera = Camera.main;

        MainCamera.transform.position = transform.position - Offset;
        MainCamera.transform.LookAt(transform);
        ScaleCamera();


        MovePlayerClick();
    }

    private void FixedUpdate()
    {
        float speed = Navigator.velocity.magnitude / Navigator.speed;
        if (speed > 0)
        {
            if (!WalkingSoundPlayer.isPlaying)
                WalkingSoundPlayer.Play();
        }
        else
        {
            WalkingSoundPlayer.Stop();
        }
        Animator.SetFloat("Speed", speed);
    }

    private void ScaleCamera()
    {
        Vector3 cameraMove = MainCamera.transform.forward * ScaleSpeed * Input.mouseScrollDelta.y;
        if ((Offset - cameraMove).magnitude >= 10 && (Offset - cameraMove).magnitude <= 400)
            Offset -= cameraMove;
    }


    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, Navigator.stoppingDistance);
        Gizmos.color = Color.green;
       // Gizmos.DrawSphere(Navigator.destination, 0.1f);
    }

    public void ClearSelection()
    {
        Selection = null;
    }

    /// <summary>
    /// Selecting moving target
    /// </summary>
    private void MovePlayerClick()
    {
        if (Input.GetMouseButtonDown(1))// Right click
        {
            if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, MovingLayerMask))
            {
                Quaternion lookTo = Quaternion.LookRotation(hit.point - transform.position);

                transform.eulerAngles = new Vector3(0, lookTo.eulerAngles.y, 0);

                Navigator.ResetPath();

                if (hit.collider.gameObject.layer == 6) // hit floor layer
                {
                    Navigator.stoppingDistance = 0f;
                    isHeadingToTarget = false;
                    WayPointer.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
                    WayPointerAnimator.Play("ShowWay");
                    Navigator.SetDestination(hit.point);
                }
                else if (hit.collider.gameObject.layer == 8) // hit interactable layer
                {
                    Selection = hit.collider.gameObject.GetComponent<Interactable>();
                    isHeadingToTarget = true;

                    if (Selection.Type == InteractableType.Minion)
                    {
                        Navigator.stoppingDistance = FightingScript.AttackRange;
                        FightingScript.TargetEnemy = Selection.GetComponent<Enemy>();
                    }

                    Navigator.SetDestination(Selection.transform.position);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0)) // Left click
        {
            if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, 1 << 8))
            {
                Selection = hit.collider.GetComponent<Interactable>();
                FightingScript.TargetEnemy = Selection.GetComponent<Enemy>();
            }
            else
            {
                FightingScript.TargetEnemy = null;
                Selection = null;
            }
        }
    }
}
