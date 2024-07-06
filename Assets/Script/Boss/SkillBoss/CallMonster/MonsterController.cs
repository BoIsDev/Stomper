using System.Collections;
using UnityEngine;
public class MonsterController : MonoBehaviour
{
    public Transform player;
    public Transform pointSkill;
    public GameObject spellMonster;
    public int count = 0;
    private Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetBool("isIdle", true);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 posPlayer = new Vector3(player.position.x, player.position.y + 1f, player.position.z);
        pointSkill.position = posPlayer;
        float distance = Vector2.Distance(player.position, transform.position);
        float posFlit = player.position.x - transform.position.x;
        if (posFlit > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (count == 0)
        {
            if (distance < 2.5f)
            {
                SetAniFalse();
                ani.SetTrigger("isAttack");
                AudioManager.Instance.PlaySFXEnviroment(AudioManager.Instance.attackCallMonster);
                count++;
            }
            else if (distance > 4)
            {
                SetAniFalse();
                ani.SetBool("isIdle", true);
                StartCoroutine(CastSkillMonster());
                count++;
            }
        }
    }
    private IEnumerator CastSkillMonster()
    {
        yield return new WaitForSeconds(2f);
        SetAniFalse();
        ani.SetTrigger("isCasting");
    }

    private IEnumerator SpellMonster()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject newSpellMonster = PoolItem.Instance.GetObjItem(spellMonster, pointSkill);
        AudioManager.Instance.PlaySFXEnviroment(AudioManager.Instance.attackSkillMonster);
        yield return new WaitForSeconds(1.5f);
        PoolItem.Instance.ReturnObjePool(newSpellMonster);
        StartCoroutine(SkipTime(1f));
    }

    public void SetAniFalse()
    {
        ani.SetBool("isIdle", false);
    }

    private IEnumerator SkipTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        ani.SetBool("isIdle", true);
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.DamageReciever(1);
        }
    }
}
