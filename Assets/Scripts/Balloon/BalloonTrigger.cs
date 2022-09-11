using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonTrigger : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        BaloonSpawnManager.Instance.DestroyBalloon(this.gameObject);
    }
}
