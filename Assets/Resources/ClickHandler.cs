using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Camera m_Camera;
    public GameObject fx;
    public GameObject selector;
    private Vector3 originalPos;
    void Awake()
    {
        m_Camera = Camera.main;
        originalPos = m_Camera.transform.localPosition;

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            Debug.Log(ray);
            selector.transform.position = ray.origin;
            //if (Physics.Raycast(ray, out RaycastHit hit))
            //{
                var f = Object.Instantiate(fx);
                f.transform.localPosition = ray.origin;

            // Use the hit variable to determine what was clicked on.
            //}
            StartCoroutine("ShakeCamera");
        }
    }

    // used fields
    [SerializeField] private float cameraShakeDuration = 0.5f;
    [SerializeField] private float cameraShakeDecreaseFactor = 1f;
    [SerializeField] private float cameraShakeAmount = 1f;
    // coroutine
    IEnumerator ShakeCamera()
    {
        var currentPosition = m_Camera.transform.localPosition; 
        var duration = cameraShakeDuration;
        while (duration > 0)
        {
            m_Camera.transform.localPosition = currentPosition + Random.insideUnitSphere * cameraShakeAmount;
            duration -= Time.deltaTime * cameraShakeDecreaseFactor;
            yield return null;
        }
        m_Camera.transform.localPosition = originalPos;
    }
}
