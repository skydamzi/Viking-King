using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCorou : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeBullet()
    {
        Debug.Log("ÃÑ¾Ë¹ß»ç");
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log("ÅÁ!"); //º¼·ý -= 0.01f
        }
    }

    IEnumerator Test()
    {
        Debug.Log("ÀåÀü");

        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("¹ß»ç");
        }
    }

}
