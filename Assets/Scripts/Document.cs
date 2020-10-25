using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : MonoBehaviour
{

    [SerializeField] private float waitTime;
    [SerializeField] private int points;

    private void OnEnable()
    {
        StartCoroutine(WaitThenDisable(waitTime));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator WaitThenDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    public int GetPoints()
    {
        return points;
    }
}
