using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using DG.Tweening;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rigid;

    [SerializeField]
    private float _forwardSpeed;

    

    private Vector3 _targetPos;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        Vector3 forwardVector = new Vector3(0, 0, _forwardSpeed);
       
        _targetPos = Vector3.zero;
    }

    private void FixedUpdate()
    {

        transform.Translate(_forwardSpeed * Time.deltaTime * Vector3.forward);
    }

        public void PlayerUp()
    {
        
        _targetPos.y = 1.0f;
        transform.DOBlendableLocalMoveBy(_targetPos, 0.5f);
    }

    public void PlayerDown()
    {
        
        _targetPos.y = -1.0f;
        transform.DOBlendableLocalMoveBy(_targetPos, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
