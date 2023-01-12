using UnityEngine;

public class ElectronicSparksController : MonoBehaviour
{
    public ParticleSystem electronicSparksParticles;
    public GameObject target;

    void Update()
    {
        electronicSparksParticles.transform.position = target.transform.position;
        Invoke("Sparks", 5);
    }

    public void Sparks()
    {
        electronicSparksParticles.Play();
    }
}