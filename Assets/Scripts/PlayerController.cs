using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private InputActionReference moveInput, actionInput;
    [SerializeField] private float moveSpeed = 5;

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

        block?.PloughSoil();
    }
}
