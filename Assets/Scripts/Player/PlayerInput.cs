using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class PlayerInput : MonoBehaviour
{
    
    [SerializeField]
    private PlayerMovement _playerMovement;

    public void swipe(Vector2 swipeVector)
    {
        if(swipeVector.y > 150)
        {
            TrySpawnBalloon();
        }
        else if(swipeVector.y < 150)
        {
            TryDespawnBalloon();
        }
    }

    private void TrySpawnBalloon()
    {
        if (BaloonSpawnManager.Instance.SpawnBalloon())
        {
            _playerMovement.PlayerUp();
        }
    }

    private void TryDespawnBalloon()
    {
        if (BaloonSpawnManager.Instance.DespawnBalloon())
        {
            _playerMovement.PlayerDown();
        }
    }

  
    
}
