using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class health_control : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name.Contains("Worm")) {
            Debug.Log("worm");
            health -= 5;
        }

        if (health < 0) {
            SceneManager.LoadScene("DieMenu");
        }
    }
}
