using UnityEngine;

public class GeneratedObjectToDelete : MonoBehaviour{

    public void DeletePointingObject(GameObject wholeCable)
    {
        Destroy(wholeCable.gameObject);
    }
}
