using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExplotion : MonoBehaviour
{




    // Update is called once per frame
    void Update()
    {
        Invoke("Die",1f);
    }


    private void Die()
    {
        Destroy(gameObject);
        //
    }
}
