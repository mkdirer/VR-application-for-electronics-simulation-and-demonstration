using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CheckTask : MonoBehaviour
{
    [HideInInspector] List<GameObject> Task1Objects;
    [HideInInspector] GameObject[] TaskObjects;
    [SerializeField] GameObject ButtonTask1;
    [SerializeField] Material MateralButton1;
    [SerializeField] public GameObject effectPrefab;
    [SerializeField] public float spawnDelay = 0.1f;
    [SerializeField] public List<string> CheckSelectedObjectTagStation2;

    public void CheckSelectedFirstTask()
    {
        Renderer rend = ButtonTask1.GetComponent<Renderer>();
        rend.enabled = true;
        if (PowerSupplyDC.CirciutEquipmentBlocks != null)
        {
            if (PowerSupplyDC.CirciutEquipmentBlocks.Count >= InputSignal.numberOfCircuits)
            {
                List<string> Task1Components = new List<string> { "Lamp", "And_plate", "Or_plate", "Not_plate" };

                foreach (string componentTag in Task1Components)
                {
                    if (!PowerSupplyDC.CirciutEquipmentBlocks.Any(name => GameObject.Find(name).tag == componentTag))
                    {
                        return;
                    }
                }

                TaskObjects = GameObject.FindGameObjectsWithTag("Button_for_logic");
                if(TaskObjects != null)
                {
                    Task1Objects.AddRange(collection: TaskObjects);
                    if (Task1Objects.Count >= 2)
                    {
                        rend.sharedMaterial = MateralButton1;
                        Invoke("ActiveEffect", spawnDelay);
                        Invoke("DeactiveEffect", 3.0f);

                    }
                }
            }
        }
    }

    public void CheckSelectedSecondTask()
    {
        Renderer rend = ButtonTask1.GetComponent<Renderer>();
        rend.enabled = true;
        if (PowerSupplyDC.CirciutEquipmentBlocks != null)
        {
            if (PowerSupplyDC.CirciutEquipmentBlocks.Count >= InputSignal.numberOfCircuits)
            {
                List<string> Task1Components = new List<string> { "Lamp", "And_plate", "Or_plate", "Not_plate" };

                foreach (string componentTag in Task1Components)
                {
                    if (!PowerSupplyDC.CirciutEquipmentBlocks.Any(name => GameObject.Find(name).tag == componentTag))
                    {
                        return;
                    }
                }

                TaskObjects = GameObject.FindGameObjectsWithTag("Button_for_logic");
                if (TaskObjects != null)
                {
                    Task1Objects.AddRange(collection: TaskObjects);
                    if (Task1Objects.Count >= 3)
                    {
                        rend.sharedMaterial = MateralButton1;
                        Invoke("ActiveSpawnEffect", spawnDelay);
                        Invoke("DeactiveSpawnEffect", 3.0f);
                    }
                }
            }
        }
    }

    public void CheckSelectedTaskStation2()
    {
        Renderer rend = ButtonTask1.GetComponent<Renderer>();
        rend.enabled = true;
        if (PowerSupplyDC.CirciutEquipmentBlocks != null)
        {
            if (PowerSupplyDC.CirciutEquipmentBlocks.Count >= InputSignal.numberOfCircuits)
            {
                foreach (string componentTag in CheckSelectedObjectTagStation2)
                {
                    if (!PowerSupplyDC.CirciutEquipmentBlocks.Any(name => GameObject.Find(name).tag == componentTag))
                    {
                        return;
                    }
                }

                rend.sharedMaterial = MateralButton1;
                Invoke("ActiveEffect", spawnDelay);
                Invoke("DeactiveEffect", 3.0f);

            }
        }
    }

    void ActiveEffect()
    {
        if (effectPrefab)
        {
            effectPrefab.SetActive(true);
        }
    }

    void DeactiveEffect()
    {
        if (effectPrefab)
        {
            effectPrefab.SetActive(false);
        }
    }
}