using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    Player pl;
    public int dameLava = 1;
    public float timeDelayDame = 0.5f;
    private float nextDamageTime = 0;
    void Awake()
    {
        pl = FindObjectOfType<Player>();
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IDameReciever dameReciever))
        {
            if (Time.time >= nextDamageTime)
            {
                ObseverManager.Instance.DamageReciever(dameLava);
                nextDamageTime = Time.time + timeDelayDame;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        float offsetYAdd = 0.2f;
        if (col.gameObject.CompareTag("Stone"))
        {
            // Lưu kích thước ban đầu và vị trí ban đầu của Lava
            Vector3 initialScale = transform.localScale;
            Vector3 initialPosition = transform.position;
            // Tăng kích thước Y của Lava
            transform.localScale = new Vector3(initialScale.x, initialScale.y + offsetYAdd, initialScale.z);
            // Di chuyển Lava lên trên để giữ điểm gốc ở dưới cùng
            transform.position = new Vector3(initialPosition.x, initialPosition.y + offsetYAdd / 2, initialPosition.z);
        }
    }
    
}
