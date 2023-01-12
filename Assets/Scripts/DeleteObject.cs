using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    [SerializeField] public GameObject element;

    void Start()
    {
       //GameObject particle = new
    }

    public void DeletePointingObject()
    {
        if(element)
        {
            element.SetActive(true);
            Invoke("Deactivate", 3.0f);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    void Deactivate()
    {
        element.SetActive(false);
        Destroy(this.gameObject);
    }
}
