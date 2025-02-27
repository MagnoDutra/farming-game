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
    [SerializeField] private float toolWaitTime = 0.5f;
    [SerializeField] private Transform toolIndicator;
    private float toolWaitCounter;

    public ToolType currentTool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        UIController.Instance.SwitchTool((int)currentTool);
    }

    void Update()
    {
        anim.SetFloat(Constants.Animations.SPEED, rb.linearVelocity.magnitude);

        FlipCharacter();

        SwitchToolOsPressKey();

        if (actionInput.action.WasPressedThisFrame() && toolWaitCounter <= 0)
        {
            UseTool();
        }


    }

    private void FixedUpdate()
    {
        if (toolWaitCounter > 0)
        {
            rb.linearVelocity = Vector2.zero;
            toolWaitCounter -= Time.deltaTime;
            return;
        }
        rb.linearVelocity = moveInput.action.ReadValue<Vector2>() * moveSpeed;
    }

    void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        toolIndicator.position = mousePos;
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = FindFirstObjectByType<GrowBlock>();

        toolWaitCounter = toolWaitTime;

        switch (currentTool)
        {
            case ToolType.Plough:
                block?.PloughSoil();
                anim.SetTrigger(Constants.Animations.PLOUGH);
                break;
            case ToolType.WateringCan:
                block?.WaterSoil();
                anim.SetTrigger(Constants.Animations.WATERING);
                break;
            case ToolType.Seeds:
                block.PlantCrop();
                break;
            case ToolType.Basket:
                block.HarvestCrop();
                break;
        }
    }

    void FlipCharacter()
    {
        if (rb.linearVelocity.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.linearVelocity.x > 0f)
        {
            transform.localScale = Vector3.one;
        }
    }

    void SwitchToolOsPressKey()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentTool++;

            int enumCount = Enum.GetValues(typeof(ToolType)).Length;

            if ((int)currentTool >= enumCount)
            {
                currentTool = ToolType.Plough;
            }
            UIController.Instance.SwitchTool((int)currentTool);
        }

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentTool = ToolType.Plough;
            UIController.Instance.SwitchTool((int)currentTool);
        }

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentTool = ToolType.WateringCan;
            UIController.Instance.SwitchTool((int)currentTool);
        }

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentTool = ToolType.Seeds;
            UIController.Instance.SwitchTool((int)currentTool);
        }

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {

            currentTool = ToolType.Basket;
            UIController.Instance.SwitchTool((int)currentTool);
        }
    }



}
