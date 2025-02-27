using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ToolType
{
    Plough, WateringCan, Seeds, Basket
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private InputActionReference moveInput, actionInput;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float toolWaitTime = 0.5f;
    [SerializeField] private Transform toolIndicator;
    [SerializeField] private float toolRange = 3f;

    private float toolWaitCounter;

    public ToolType currentTool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
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

        if (Vector3.Distance(toolIndicator.position, transform.position) > toolRange)
        {
            // Limita a distancia da ferramenta
            Vector2 direction = toolIndicator.position - transform.position;
            direction = direction.normalized * toolRange;
            toolIndicator.position = transform.position + new Vector3(direction.x, direction.y, 0f);
        }

        // faz o indicador ficar nos grids certinho mas ainda precisa de um offset
        toolIndicator.position = new Vector3(Mathf.FloorToInt(toolIndicator.position.x) + .5f,
            Mathf.FloorToInt(toolIndicator.position.y) + .5f, 0f);
    }

    void UseTool()
    {
        GrowBlock block = null;

        block = GridController.instance.GetBlock(toolIndicator.position.x - .5f, toolIndicator.position.y - .5f);

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
