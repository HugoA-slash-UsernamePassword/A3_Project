using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadein : MonoBehaviour
{
    public AudioSource intro;
    public GameObject player;
    public GameObject[] ghosts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!intro.isPlaying) //bit of a hacky solution but it works for now
        {
            Instantiate(player);
            foreach (GameObject item in ghosts)
            {
                Instantiate(item);
            }
            Destroy(gameObject);
        }
    }
}
