using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public int health;
    public float speed;
    
    private float velocity;

    void Start()
    {
        velocity = 0f;
    }
	
	void Update () {

        velocity = speed * Time.deltaTime;
       // transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("Castle").transform.position, velocity);     
    }

}
