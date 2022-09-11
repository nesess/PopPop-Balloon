using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaloonSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Lean.Pool.LeanGameObjectPool _balloonLean;

    [SerializeField]
    private Lean.Pool.LeanGameObjectPool _confettiLean;

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _balloonTargetPos;

    [SerializeField]
    private Transform _characterBalloonConnection;

    [SerializeField]
    private Material[] _balloonMat;

    [SerializeField]
    private PlayerMovement _playerMovement;

    private AudioSource _audioSource;
    private int _balloonCount = 0;

    public static BaloonSpawnManager Instance { get; private set; }
    private void Awake()
    {
       

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnStartBalloons());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetRandomUpVector()
    {
        return new Vector3(Random.Range(-0.5f, 0.5f), _balloonTargetPos.localPosition.y, Random.Range(-0.5f, 0.5f));
    }

    public bool SpawnBalloon()
    {
        if(_balloonCount >19)
        {
            return false;
        }
        GameObject TempGameObject = _balloonLean.Spawn(_player, false);
        GameObject balloon = TempGameObject.transform.GetChild(1).gameObject;
        TempGameObject.transform.GetChild(0).GetComponent<RopeMinikit.RopeConnection>().transformSettings.transform = _characterBalloonConnection;
        balloon.GetComponent<MeshRenderer>().material = _balloonMat[Random.Range(0, _balloonMat.Length)];
        balloon.transform.localScale = Vector3.zero;
        TempGameObject.transform.localPosition = Vector3.zero;
        TempGameObject.transform.DOBlendableLocalMoveBy(GetRandomUpVector(), 0.5f);
        balloon.transform.DOScale(new Vector3(1.4f,1.4f,1.4f), 1.0f);
        _balloonCount++;
        return true;
    }

    public bool DespawnBalloon()
    {
        if( _balloonCount <= 1)
        {
            return false;
        }
        _balloonCount--;
        _balloonLean.DespawnOldest();
        GameObject confetti = _confettiLean.Spawn(_player.GetChild(2).transform.position, Quaternion.identity, _confettiLean.transform);
        _confettiLean.Despawn(confetti, 1.5f);
        _audioSource.Play();
        return true;
    }

    private IEnumerator SpawnStartBalloons()
    {
        for(int i=0;i<4;i++)
        {
            yield return new WaitForSeconds(0.2f);
            SpawnBalloon();
            _playerMovement.PlayerUp();
        }
    }

    public void DestroyBalloon(GameObject balloon)
    {
        if(_balloonCount > 1)
        {
            
            _balloonCount--;
            GameObject confetti = _confettiLean.Spawn(balloon.transform.position,Quaternion.identity,_confettiLean.transform);
            _confettiLean.Despawn(confetti, 1.5f);
            _balloonLean.Despawn(balloon.transform.parent.transform.parent.gameObject);
            _audioSource.Play();
            _playerMovement.PlayerDown();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }

}
