using System;
using DG.Tweening;
using UnityEngine;

public enum BuildingType
{
    Assembler,
    Cloner,
    Conveyor,
    Cutter,
    FoodSource,
    Furnace,
    Launcher,
    Mixer,
}

public class BuildingAnimator : MonoBehaviour
{
    public BuildingType buildingType;

    //Ok ce script SERA dégueu, mais nik au moins on a qu'un script, gamejam coding

    [Header("Assembler")]
    [SerializeField] Transform armBase1;
    [SerializeField] Transform armExtend1;
    [SerializeField] Transform armBase2;
    [SerializeField] Transform armExtend2;
    [SerializeField] Transform armBase3;
    [SerializeField] Transform armExtend3;
    [SerializeField] Transform armBase4;
    [SerializeField] Transform armExtend4;
    [Header("Cloner")]
    [SerializeField] ParticleSystem sparks1;
    [SerializeField] ParticleSystem sparks2;
    [SerializeField] ParticleSystem sparks3;
    [SerializeField] ParticleSystem sparks4;
    [Header("Cutter")]
    [SerializeField] Transform hatchet1;
    Sequence hatchetRotation1;
    [SerializeField] Transform hatchet2;
    Sequence hatchetRotation2;
    [SerializeField] Transform hatchet3;
    Sequence hatchetRotation3;
    [Header("Furnace")]
    [SerializeField] ParticleSystem smoke;
    [Header("Launcher")]
    [SerializeField] Transform launcher;
    Sequence launchSequence;
    [Header("Mixer")]
    [SerializeField] Transform spoon;
    [SerializeField] Mover spoonMover;

    void Awake()
    {
        switch (buildingType)
        {
            case BuildingType.Launcher:
                launchSequence = DOTween.Sequence();
                launchSequence.Append(launcher.DORotate(Vector3.forward * 45f, 0.25f, RotateMode.Fast));
                launchSequence.AppendInterval(0.2f);
                launchSequence.Append(launcher.DORotate(Vector3.zero, 0.55f, RotateMode.Fast));
                break;
            case BuildingType.Cutter:    
                hatchetRotation1 = DOTween.Sequence();
                hatchetRotation1.Append(hatchet1.DORotate(new Vector3(-160f, 0f, -180f), 0.2f));
                hatchetRotation1.Append(hatchet1.DORotate(new Vector3(-68f, 0f, -180f), 0.1f));
                hatchetRotation1.Append(hatchet1.DORotate(new Vector3(-90f, 0f, -180f), 0.55f));
                
                hatchetRotation2 = DOTween.Sequence();
                hatchetRotation2.AppendInterval(0.075f);
                hatchetRotation2.Append(hatchet1.DORotate(new Vector3(-160f, 0f, -180f), 0.2f));
                hatchetRotation2.Append(hatchet1.DORotate(new Vector3(-68f, 0f, -180f), 0.1f));
                hatchetRotation2.Append(hatchet1.DORotate(new Vector3(-90f, 0f, -180f), 0.55f));
                
                hatchetRotation3 = DOTween.Sequence();
                hatchetRotation3.AppendInterval(0.15f);
                hatchetRotation3.Append(hatchet1.DORotate(new Vector3(-160f, 0f, -180f), 0.2f));
                hatchetRotation3.Append(hatchet1.DORotate(new Vector3(-68f, 0f, -180f), 0.1f));
                hatchetRotation3.Append(hatchet1.DORotate(new Vector3(-90f, 0f, -180f), 0.55f));
                break;
            case BuildingType.Assembler:
                break;
            default: break;
        }
    }

    public void PlayAnimation()
    {
        switch (buildingType)
        {
            case BuildingType.Assembler:

                break;
            case BuildingType.Cloner: 
                sparks1.Play();
                sparks2.Play();
                sparks3.Play();
                sparks4.Play();
                break;
            case BuildingType.Conveyor: break;
            case BuildingType.Cutter: 
                hatchetRotation1.Play();
                hatchetRotation2.Play();
                hatchetRotation3.Play();
                break;
            case BuildingType.Furnace: 
                smoke.Play();
                break;
            case BuildingType.Launcher:
                launchSequence.Play();
                break;
            case BuildingType.Mixer:
                spoonMover.MoveObject(spoon, 0.8f);
                break;
            default: break;
        }
    }
}
