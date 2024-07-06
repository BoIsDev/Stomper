using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(StateManager))]
public class BossController : MonoBehaviour
{
    //GameObjhInHierarchy
    public GameObject BulletFireBall;
    public GameObject BulletIce;
    public GameObject CashIce;
    public GameObject BulletFireOn;
    public GameObject Monster;
    public GameObject BulletSuriken;
    public GameObject Hearth;
    public Text txtTimeBos;
    [Header("------TransformInHierarchy-------")]
    public Transform Player;
    public Transform PointHoldplayer;
    public Transform FireBallPos;
    public Transform IceHold;
    public Transform FireOnPos;
    public Transform MonsterHold;
    public Transform SurikenHold;//point Player
    public Transform PointSuriken;// point Suriken
    //
    public Transform[] ArrPointHeart;
    public List<Transform> lstPointIce;
    public List<Transform> lstPointMonster;
    private bool isCanCall = true;
    private bool isCanSuriken = true;
    private bool isCanHeart = true;
    private Coroutine waitingChangeSkill;
    private float distanceSkill = 7f;
    private float timeDelayStartSkill = 4f;
   
    //ManagerScripts
    public bool isStateFree = true;
    public int count = 0;
    public static BossController Instance => instance;
    private static BossController instance;
    [SerializeField] private StateManager stateManager;
    private PlayerController playerController;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        playerController = Player.gameObject.GetComponent<PlayerController>();
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
        Vector3 surikenPosition = BulletSuriken.transform.position;
        PointSuriken.position = surikenPosition;
        float posPlayerY = Player.position.y;
        float distance = Vector2.Distance(Player.position, transform.position);
        if (!playerController.isGrounded)
        {
            FireOnPos.position = new Vector3(Player.position.x, -1.45f, Player.position.z);
        }

        if (isCanCall)
        {
            isCanCall = !isCanCall;
            StartCoroutine(CallMonsterSkill());
        }
        if (isCanHeart)
        {
            isCanHeart = !isCanHeart;
            StartCoroutine(SpawnHearth());
        }
        if (isCanSuriken)
        {
            isCanSuriken = !isCanSuriken;
            StartCoroutine(SurikenSkill());
        }

        if (Time.time > timeDelayStartSkill)
        {
            if (isStateFree && count == 0)
            {
                if (distance > distanceSkill && PlayerController.Instance.isGrounded)
                {
                    isStateFree = false;
                    stateManager.ChangeState(new FireOnState(this));
                }
                else if (distance <= distanceSkill)
                {
                    isStateFree = false;
                    stateManager.ChangeState(new FireBallSkill(this));
                }
                else if (distance > distanceSkill && !PlayerController.Instance.isGrounded)
                {
                    isStateFree = false;
                    stateManager.ChangeState(new IceSkillState(this));
                }
                count++;
                waitingChangeSkill = StartCoroutine(WaitSkillChange(2.5f));
            }
        }
    }

   
    public void LoadHoldTransform()
    {
        lstPointIce = new List<Transform>();
        foreach (Transform pos in IceHold)
        {
            lstPointIce.Add(pos);
            pos.gameObject.SetActive(false);
        }
    }
    public void LoadMosterTransform()
    {
        lstPointMonster = new List<Transform>();
        foreach (Transform pos in MonsterHold)
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
    private IEnumerator CallMonsterSkill()
    {
        yield return new WaitForSeconds(12f);
        stateManager.ChangeState(new CallMonsterState(this));
        isCanCall = true;
    }
    private IEnumerator SurikenSkill()
    {
        yield return new WaitForSeconds(4);
        stateManager.ChangeState(new SurikenSkill(this));
        isCanSuriken = true;
    }
    private IEnumerator SpawnHearth()
    {
        yield return new WaitForSeconds(12f);
        Transform posSpawn = ArrPointHeart[Random.Range(0, ArrPointHeart.Length)];
        GameObject hearthPrefabs = PoolItem.Instance.GetObjItem(Hearth, posSpawn);
        hearthPrefabs.GetComponent<Rigidbody2D>().drag = 0.5f;
        isCanHeart = true;
    }
}
