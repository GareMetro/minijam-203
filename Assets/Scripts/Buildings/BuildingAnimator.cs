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
                launchSequence.Append(launcher.DORotate(new Vector3(-45f, 0f, 90f), 0.25f, RotateMode.Fast));
                launchSequence.AppendInterval(0.2f);
                launchSequence.Append(launcher.DORotate(new Vector3(-90f, 0f, 90f), 0.55f, RotateMode.Fast));
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
                //Arm 1
                armBaseRotation1 = DOTween.Sequence();
                armBaseRotation1.Append(armBase1.DORotate(new Vector3(-140f, -45f, 45f), 0.3f));
                armBaseRotation1.AppendInterval(0.2f);
                armBaseRotation1.Append(armBase1.DORotate(new Vector3(-90f, -45f, 45f), 0.3f));

                armExtendTranslation1 = DOTween.Sequence();
                armExtendTranslation1.Append(armExtend1.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation1.AppendInterval(0.05f);
                armExtendTranslation1.Append(armExtend1.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));
                armExtendTranslation1.AppendInterval(0.3f);
                armExtendTranslation1.Append(armExtend1.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation1.AppendInterval(0.05f);
                armExtendTranslation1.Append(armExtend1.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));

                //Arm 2
                armBaseRotation2 = DOTween.Sequence();
                armBaseRotation2.AppendInterval(0.05f);
                armBaseRotation2.Append(armBase2.DORotate(new Vector3(-140f, 45f, -45f), 0.3f));
                armBaseRotation2.AppendInterval(0.2f);
                armBaseRotation2.Append(armBase2.DORotate(new Vector3(-90f, -45f, 45f), 0.3f));

                armExtendTranslation2 = DOTween.Sequence();
                armExtendTranslation2.AppendInterval(0.05f);
                armExtendTranslation2.Append(armExtend2.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation2.AppendInterval(0.05f);
                armExtendTranslation2.Append(armExtend2.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));
                armExtendTranslation2.AppendInterval(0.3f);
                armExtendTranslation2.Append(armExtend2.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation2.AppendInterval(0.05f);
                armExtendTranslation2.Append(armExtend2.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));

                //Arm 3
                armBaseRotation3 = DOTween.Sequence();
                armBaseRotation2.AppendInterval(0.1f);
                armBaseRotation3.Append(armBase3.DORotate(new Vector3(-140f, 45f, -45f), 0.3f));
                armBaseRotation3.AppendInterval(0.2f);
                armBaseRotation3.Append(armBase3.DORotate(new Vector3(-90f, -45f, 45f), 0.3f));

                armExtendTranslation3 = DOTween.Sequence();
                armExtendTranslation2.AppendInterval(0.1f);
                armExtendTranslation3.Append(armExtend3.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation3.AppendInterval(0.05f);
                armExtendTranslation3.Append(armExtend3.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));
                armExtendTranslation3.AppendInterval(0.3f);
                armExtendTranslation3.Append(armExtend3.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation3.AppendInterval(0.05f);
                armExtendTranslation3.Append(armExtend3.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));

                //Arm 4
                armBaseRotation4 = DOTween.Sequence();
                armBaseRotation2.AppendInterval(0.15f);
                armBaseRotation4.Append(armBase4.DORotate(new Vector3(-140f, 45f, -45f), 0.3f));
                armBaseRotation4.AppendInterval(0.2f);
                armBaseRotation4.Append(armBase4.DORotate(new Vector3(-90f, -45f, 45f), 0.3f));

                armExtendTranslation4 = DOTween.Sequence();
                armExtendTranslation2.AppendInterval(0.15f);
                armExtendTranslation4.Append(armExtend4.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation4.AppendInterval(0.05f);
                armExtendTranslation4.Append(armExtend4.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));
                armExtendTranslation4.AppendInterval(0.3f);
                armExtendTranslation4.Append(armExtend4.DOLocalMove(new Vector3(-0.00431f,-0.00422f,0.00731f), 0.125f));
                armExtendTranslation4.AppendInterval(0.05f);
                armExtendTranslation4.Append(armExtend4.DOLocalMove(new Vector3(-0.004855f,-0.004855f,0.000655f), 0.125f));
                break;
            default: break;
        }
    }

    public void PlayAnimation()
    {
        switch (buildingType)
        {
            case BuildingType.Assembler:
                armBaseRotation1.Play();
                armExtendTranslation1.Play();
                armBaseRotation2.Play();
                armExtendTranslation2.Play();
                armBaseRotation3.Play();
                armExtendTranslation3.Play();
                armBaseRotation4.Play();
                armExtendTranslation4.Play();
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
