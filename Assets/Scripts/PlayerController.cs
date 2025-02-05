using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ToolType
{
    Plough, WateringCan, Seeds, Basket
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private InputActionReference moveInput, actionInput;
    [SerializeField] private float moveSpeed = 5;

    public ToolType currentTool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        anim.SetFloat("speed", rb.linearVelocity.magnitude);

        if (rb.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;

            int enumCount = Enum.GetValues(typeof(ToolType)).Length;

            if ((int)currentTool >= enumCount)
            {
                currentTool = ToolType.Plough;
            }
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            currentTool = ToolType.Plough;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            currentTool = ToolType.WateringCan;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            currentTool = ToolType.Seeds;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            currentTool = ToolType.Basket;

        if (actionInput.action.WasPressedThisFrame())
        {
            UseTool();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput.action.ReadValue<Vector2>() * moveSpeed;
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        switch (currentTool)
        {
            case ToolType.Plough:
                block?.PloughSoil();
                break;
            case ToolType.WateringCan:
                break;
            case ToolType.Seeds:
                break;
            case ToolType.Basket:
                break;
        }
    }
}
