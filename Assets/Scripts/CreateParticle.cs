using UnityEngine;

public class CreateParticle : MonoBehaviour
{
    public GameObject effectPrefab;
    public float spawnDelay = 0.1f;

    public void CreateSpawnEffect()
    {
        Invoke("ActiveSpawnEffect", spawnDelay);
        Invoke("DeactiveSpawnEffect", 3.0f);
    }

    void ActiveSpawnEffect()
    {
        if (effectPrefab)
        {
            effectPrefab.SetActive(true);
        }
    }

    void DeactiveSpawnEffect()
    {
        if (effectPrefab)
        {
            effectPrefab.SetActive(false);
        }
    }
}