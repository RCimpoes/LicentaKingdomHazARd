using UnityEngine;

public class CollisionCstl : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "NavAgent")
        {
            // Reaction
            Debug.Log("Facut accident cu:" + collision.gameObject.name);
        }
    }

  
}