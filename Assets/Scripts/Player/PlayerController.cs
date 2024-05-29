using System;
using System.Collections;
using System.Collections.Generic;
using Keraz.Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("基本参数")]
    public float normalSpeed;
    public float runSpeed;
    public float speed;
    public Vector2 faceDir;
    
    [Header("基本组件")]
    private Rigidbody2D rd;
    private BoxCollider2D coll;
    //[SerializeField]private InventoryUI inventoryUI;

    [Header("新输入系统")]
    private Simulation inputSystem;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        inputSystem = new Simulation();
        speed = normalSpeed;
    }

    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void Update()
    {
        faceDir = inputSystem.Player.Move.ReadValue<Vector2>();
       
    }
    

    private void FixedUpdate()
    {
        if (faceDir.x <=0.5f || faceDir.y <0.5f)
        {
            Move();
        }
        
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }

    private void Move()
    {
        // 移动
        rd.MovePosition(transform.localPosition+(Vector3)faceDir*(speed*Time.deltaTime));
    }
}
