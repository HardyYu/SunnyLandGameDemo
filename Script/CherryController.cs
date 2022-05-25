using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<PlayerController>().CherryCount();
        Destroy(gameObject);
    }
}
