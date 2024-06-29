using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateManager))]
public class BossController : MonoBehaviour
{
    [SerializeField] public StateManager stateManager;
    [SerializeField] public bool isStateFree = true;
    public Transform player;
    public float distanceSkill = 7f;
    //FireBall
    public GameObject bulletFireBall;
    public Transform fireBallPos;
    //Ice
    public GameObject bulletIce;
    public Transform iceHold;
    public GameObject cashIce;
    public List<Transform> lstPointIce;
    //FireOn
    public GameObject bulletFireOn;
    public Transform fireOnPos;
    //CallMonster
    public GameObject monster;
    public Transform monsterHold;
    public List<Transform> lstPointMonster;
    public static BossController Instance => instance;
    private static BossController instance;
    private Coroutine waitingChangeSkill;
    public int count = 0;

    void Start()
    {
        if (instance == null)
            instance = this;

        stateManager = GetComponent<StateManager>();
        if (stateManager == null)
        {
            Debug.LogError("StateManager is null in BossController.");
        }
        stateManager.ChangeState(new IdleState(this));
        LoadHoldTransform();
        LoadMosterTransform();
    }

    void Update()
    {
        if (player == null || Player.Instance == null)
        {
            Debug.LogError("Player or Player instance is null.");
            return;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            stateManager.ChangeState(new CallMonsterState(this));
        }
        Vector3 posPlayer = new Vector3(player.position.x, -1.45f, player.position.z);
        fireOnPos.position = posPlayer;
        float distance = Vector2.Distance(player.position, transform.position);
        if (isStateFree && count == 0) { 
            //{
            //    if (distance > distanceSkill && Player.Instance.isGrounded)
            //    {
            //        isStateFree = false;
            //        stateManager.ChangeState(new FireOnState(this));
            //    }
            //    else if (distance <= distanceSkill)
            //    {
            //        isStateFree = false;
            //        stateManager.ChangeState(new FireBallSkill(this));
            //    }
            //    else if (distance > distanceSkill && !Player.Instance.isGrounded)
            //    {
            //        isStateFree = false;
            //        stateManager.ChangeState(new IceSkillState(this));
            //    }

       
            count++;
            waitingChangeSkill = StartCoroutine(WaitSkillChange(2f));
            }
    }
    public void LoadHoldTransform()
    {
        lstPointIce = new List<Transform>();
        foreach (Transform pos in iceHold)
        {
            lstPointIce.Add(pos);
            pos.gameObject.SetActive(false);
        }
    }

    public void LoadMosterTransform()
    {
        lstPointMonster = new List<Transform>();
        foreach (Transform pos in monsterHold)
        {
            lstPointMonster.Add(pos);
            pos.gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitSkillChange(float delay)
    {
        yield return new WaitForSeconds(delay);
        stateManager.ChangeState(new IdleState(this));
        isStateFree = true;
        count = 0;
    }
}
