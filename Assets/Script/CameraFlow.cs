
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public float smoothTime;

    public Transform target;

    public Vector3 positionOffset;
    Vector3 velocity = Vector3.zero;

    [Header("Axis Limitation")]
    public Vector2 xlimit;
    public Vector2 ylimit;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + positionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x, xlimit.x, xlimit.y) , Mathf.Clamp(targetPosition.y , ylimit.x , ylimit.y),-10);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
