using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log($"{name} is at Z localposition {gameObject.transform.localPosition.z}");
        Debug.Log($"{name} is at Z worldposition {gameObject.transform.position.z}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
