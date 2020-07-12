using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = transform.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps.IsAlive()) return;
        Destroy(gameObject);
    }
}