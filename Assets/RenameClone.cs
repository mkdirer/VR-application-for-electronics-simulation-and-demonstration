using UnityEngine;

public class RenameClone : MonoBehaviour
{
    private static int number;
    public void NumberObjects(GameObject obj) => obj.name = obj.name + ++number;
}
