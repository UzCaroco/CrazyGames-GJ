using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyCoin());
    }

    IEnumerator DestroyCoin()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
