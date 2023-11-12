using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorBounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float Frequence = 1f;

    public float Amplitude;
 
    public Vector3 Scale = Vector3.one;
void Update()
    {
        
         
        transform.localScale = Scale * (Amplitude * Mathf.Sin(Time.time*Frequence)+ 1);
    }
}
