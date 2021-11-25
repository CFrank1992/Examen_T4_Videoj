using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaDerecha1Controller : MonoBehaviour
{

    public float velocityX = 10f;

    private Rigidbody2D rb;

    private const string ENEMY_TAG = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX,rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(this.gameObject);
        
        if(other.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(this.gameObject);
            //Operación a destruir a objetivo
        }
    }
}
