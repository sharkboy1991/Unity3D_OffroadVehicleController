using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    public float dur = 1;

    void Start()
    {
        Destroy(gameObject, dur);
    }
}
