using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1 : MonoBehaviour
{
    public ParticleSystem blastEffect;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("land"))
        {
            ParticleSystem obj = Instantiate(blastEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
