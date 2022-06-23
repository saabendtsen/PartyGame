using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBigExplotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
                Invoke("Die",3f);
    }


      private void Die()
    {
        Destroy(gameObject);
        //
    }
}

