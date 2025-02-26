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
    [SerializeField] private SpriteRenderer cropSR;
    [SerializeField] private Sprite cropPlanted, cropGrowing1, cropGrowing2, cropRipe;
    [SerializeField] Sprite soilTilled;
    [SerializeField] Sprite soilWatered;

    private bool isWatered;

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

#if UNITY_EDITOR
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif
    }

    public void AdvanceStage()
    {
        stage++;

        int enumCount = Enum.GetValues(typeof(GrowthStage)).Length;

        if ((int)stage >= enumCount)
        {
            stage = GrowthStage.Barren;
        }

        SetSoilSprite();
    }

    void SetSoilSprite()
    {
        switch (stage)
        {
            case GrowthStage.Barren:
                sr.sprite = null;
                break;
            case GrowthStage.Ploughed:
                if (isWatered)
                    sr.sprite = soilWatered;
                else
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

    public void WaterSoil()
    {
        isWatered = true;
        SetSoilSprite();
    }

    public void PlantCrop()
    {
        if (stage == GrowthStage.Ploughed && isWatered)
        {
            stage = GrowthStage.Planted;
            UpdateCropSprite();
        }
    }

    void UpdateCropSprite()
    {
        switch (stage)
        {
            case GrowthStage.Planted:
                cropSR.sprite = cropPlanted;
                break;
            case GrowthStage.Growing1:
                cropSR.sprite = cropGrowing1;
                break;
            case GrowthStage.Growing2:
                cropSR.sprite = cropGrowing2;
                break;
            case GrowthStage.Ripe:
                cropSR.sprite = cropRipe;
                break;
        }
    }

    public void AdvanceCrop()
    {
        if (isWatered == true)
        {
            if (stage == GrowthStage.Planted || stage == GrowthStage.Growing1 || stage == GrowthStage.Growing2)
            {
                stage++;

                isWatered = false;
                SetSoilSprite();
                UpdateCropSprite();
            }
        }
    }

    public void HarvestCrop()
    {
        if (stage == GrowthStage.Ripe)
        {
            stage = GrowthStage.Ploughed;
            SetSoilSprite();
            cropSR.sprite = null;
        }
    }
}
