using System;
using System.Collections;
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
    Sequence armBaseRotation1;
    [SerializeField] Transform armExtend1;
    Sequence armExtendTranslation1;
    [SerializeField] Transform armBase2;
    Sequence armBaseRotation2;
    [SerializeField] Transform armExtend2;
    Sequence armExtendTranslation2;
    [SerializeField] Transform armBase3;
    Sequence armBaseRotation3;
    [SerializeField] Transform armExtend3;
    Sequence armExtendTranslation3;
    [SerializeField] Transform armBase4;
    Sequence armBaseRotation4;
    [SerializeField] Transform armExtend4;
    Sequence armExtendTranslation4;
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
                launchSequence.Append(launcher.DORotate(new Vector3(0f, -45f, 0f), 0.25f, RotateMode.LocalAxisAdd));
                launchSequence.AppendInterval(0.2f);
                launchSequence.Append(launcher.DORotate(new Vector3(0f, 45f, 0f), 0.55f, RotateMode.LocalAxisAdd));
                launchSequence.SetAutoKill(false);
                break;
            case BuildingType.Cutter:    
                hatchetRotation1 = DOTween.Sequence();
                hatchetRotation1.Append(hatchet1.DORotate(new Vector3(-70f, 0f, 0f), 0.2f, RotateMode.WorldAxisAdd));
                hatchetRotation1.Append(hatchet1.DORotate(new Vector3(92f, 0f, 0f), 0.15f, RotateMode.WorldAxisAdd));
                hatchetRotation1.Append(hatchet1.DORotate(new Vector3(-22f, 0f, 0f), 0.55f, RotateMode.WorldAxisAdd));
                hatchetRotation1.SetAutoKill(false);
                
                hatchetRotation2 = DOTween.Sequence();
                hatchetRotation2.AppendInterval(0.075f);
                hatchetRotation2.Append(hatchet2.DORotate(new Vector3(70f, 0f, 0f), 0.2f, RotateMode.WorldAxisAdd));
                hatchetRotation2.Append(hatchet2.DORotate(new Vector3(-92f, 0f, 0f), 0.15f, RotateMode.WorldAxisAdd));
                hatchetRotation2.Append(hatchet2.DORotate(new Vector3(22f, 0f, 0f), 0.55f, RotateMode.WorldAxisAdd));
                hatchetRotation2.SetAutoKill(false);
                
                hatchetRotation3 = DOTween.Sequence();
                hatchetRotation3.AppendInterval(0.15f);
                hatchetRotation3.Append(hatchet3.DORotate(new Vector3(-70f, 0f, 0f), 0.2f, RotateMode.WorldAxisAdd));
                hatchetRotation3.Append(hatchet3.DORotate(new Vector3(92f, 0f, 0f), 0.15f, RotateMode.WorldAxisAdd));
                hatchetRotation3.Append(hatchet3.DORotate(new Vector3(-22f, 0f, 0f), 0.55f, RotateMode.WorldAxisAdd));
                hatchetRotation3.SetAutoKill(false);
                break;
            case BuildingType.Assembler:
                //Arm 1
                armBaseRotation1 = DOTween.Sequence();
                armBaseRotation1.AppendInterval(0.15f);
                armBaseRotation1.Append(armBase1.DORotate(new Vector3(110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation1.AppendInterval(0.2f);
                armBaseRotation1.Append(armBase1.DORotate(new Vector3(-110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation1.SetAutoKill(false);

                armExtendTranslation1 = DOTween.Sequence();
                armExtendTranslation1.Append(armExtend1.DOLocalMove(new Vector3(0f,0.61f,0.87f), 0.125f));
                armExtendTranslation1.AppendInterval(0.5f);
                armExtendTranslation1.Append(armExtend1.DOLocalMove(new Vector3(0f,0.6866f,0.0655f), 0.125f));
                armExtendTranslation1.SetAutoKill(false);

                //Arm 2
                armBaseRotation2 = DOTween.Sequence();
                armBaseRotation2.AppendInterval(0.05f);
                armBaseRotation2.AppendInterval(0.15f);
                armBaseRotation2.Append(armBase2.DORotate(new Vector3(110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation2.AppendInterval(0.2f);
                armBaseRotation2.Append(armBase2.DORotate(new Vector3(-110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation2.SetAutoKill(false);

                armExtendTranslation2 = DOTween.Sequence();
                armExtendTranslation2.AppendInterval(0.05f);
                armExtendTranslation2.Append(armExtend2.DOLocalMove(new Vector3(0f,0.61f,0.87f), 0.125f));
                armExtendTranslation2.AppendInterval(0.5f);
                armExtendTranslation2.Append(armExtend2.DOLocalMove(new Vector3(0f,0.6866f,0.0655f), 0.125f));
                armExtendTranslation2.SetAutoKill(false);

                //Arm 3
                armBaseRotation3 = DOTween.Sequence();
                armBaseRotation3.AppendInterval(0.1f);
                armBaseRotation3.AppendInterval(0.15f);
                armBaseRotation3.Append(armBase3.DORotate(new Vector3(110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation3.AppendInterval(0.2f);
                armBaseRotation3.Append(armBase3.DORotate(new Vector3(-110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation3.SetAutoKill(false);

                armExtendTranslation3 = DOTween.Sequence();
                armExtendTranslation3.AppendInterval(0.1f);
                armExtendTranslation3.Append(armExtend3.DOLocalMove(new Vector3(0f,0.61f,0.87f), 0.125f));
                armExtendTranslation3.AppendInterval(0.5f);
                armExtendTranslation3.Append(armExtend3.DOLocalMove(new Vector3(0f,0.6866f,0.0655f), 0.125f));
                armExtendTranslation3.SetAutoKill(false);

                //Arm 4
                armBaseRotation4 = DOTween.Sequence();
                armBaseRotation4.AppendInterval(0.15f);
                armBaseRotation4.AppendInterval(0.15f);
                armBaseRotation4.Append(armBase4.DORotate(new Vector3(110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation4.AppendInterval(0.2f);
                armBaseRotation4.Append(armBase4.DORotate(new Vector3(-110f, 0f, 0f), 0.15f, RotateMode.LocalAxisAdd));
                armBaseRotation4.SetAutoKill(false);

                armExtendTranslation4 = DOTween.Sequence();
                armExtendTranslation4.AppendInterval(0.15f);
                armExtendTranslation4.Append(armExtend4.DOLocalMove(new Vector3(0f,0.61f,0.87f), 0.125f));
                armExtendTranslation4.AppendInterval(0.5f);
                armExtendTranslation4.Append(armExtend4.DOLocalMove(new Vector3(0f,0.6866f,0.0655f), 0.125f));
                armExtendTranslation4.SetAutoKill(false);
                break;
            default: break;
        }
    }

    public void PlayAnimation()
    {
        switch (buildingType)
        {
            case BuildingType.Assembler:
                armBaseRotation1.Restart();
                armExtendTranslation1.Restart();
                armBaseRotation2.Restart();
                armExtendTranslation2.Restart();
                armBaseRotation3.Restart();
                armExtendTranslation3.Restart();
                armBaseRotation4.Restart();
                armExtendTranslation4.Restart();
                break;
            case BuildingType.Cloner: 
                sparks1.Play();
                sparks2.Play();
                sparks3.Play();
                sparks4.Play();
                break;
            case BuildingType.Conveyor: break;
            case BuildingType.Cutter: 
                hatchetRotation1.Restart();
                hatchetRotation2.Restart();
                hatchetRotation3.Restart();
                break;
            case BuildingType.Furnace: 
                smoke.Play();
                break;
            case BuildingType.Launcher:
                launchSequence.Restart();
                break;
            case BuildingType.Mixer:
                spoonMover.MoveObject(spoon, 0.8f);
                break;
            default: break;
        }
    }
}
