using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour

{
    public bool canBePressed;

    public string keyToPress;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    
    void Update()
    {
        if(Input.GetButtonDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if(Mathf.Abs( transform.position.y) > 1.1f )
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                } 
                else if(Mathf.Abs(transform.position.y) > 1.05f)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                } else if(Mathf.Abs(transform.position.y) > 0.82f)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.7f)
                {
                    Debug.Log("Good");
                    GameManager.instance.PerfectHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Hit");
                    GameManager.instance.PerfectHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)

    {
        if (gameObject.activeInHierarchy)
        {

            if (collision.tag == "Activator")
            {
                canBePressed = false;

                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
