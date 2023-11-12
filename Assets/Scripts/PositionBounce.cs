using UnityEngine;
using System.Collections;

public class PositionBounce : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        origPos = transform.position;
    }
    public float Frequence = 1f;

    public float Amplitude;


    public Vector3 BouncePos= Vector3.one;

    public Vector3 origPos;

    void Update()
    {

        transform.localPosition = ((BouncePos) * ((Mathf.Sin(Time.time * Frequence)+1)/2));
    }
}
