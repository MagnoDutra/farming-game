using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GrowthStage
{
    Barren, Ploughed, Planted, Growing1, Growing2, Ripe
}

public class GrowBlock : MonoBehaviour
{
    public GrowthStage stage;
    private SpriteRenderer sr;
    [SerializeField] Sprite soilTilled;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // if (Keyboard.current.eKey.wasPressedThisFrame)
        // {
        //     AdvanceStage();
        // }
    }

    public void AdvanceStage()
    {
        stage++;

        int enumCount = Enum.GetValues(typeof(GrowthStage)).Length;

        if ((int)stage >= enumCount)
        {
            stage = GrowthStage.Barren;
        }

        UpdateSprite();
    }

    void UpdateSprite()
    {
        switch (stage)
        {
            case GrowthStage.Barren:
                sr.sprite = null;
                break;
            case GrowthStage.Ploughed:
                sr.sprite = soilTilled;
                break;
            case GrowthStage.Planted:
                sr.sprite = soilTilled;
                break;
            case GrowthStage.Growing1:
                sr.sprite = soilTilled;
                break;
            case GrowthStage.Growing2:
                sr.sprite = soilTilled;
                break;
            case GrowthStage.Ripe:
                sr.sprite = soilTilled;
                break;
        }
    }

    public void PloughSoil()
    {
        if (stage == GrowthStage.Barren)
        {
            AdvanceStage();
        }
    }
}
