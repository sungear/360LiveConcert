using UnityEngine;
using System.Collections;
using System;

public interface ViewManagerIntreface {


    void SetAsNeutralEnvrionment();
    void DisplayCalibrationView();

    void SetGeneralRotationTo(Quaternion localRotation);
    void SetGeneralRotationTo(Vector3 localEulerRotation);

    void SetPanorama360To(Texture sphericalImage);
    void SetPanorama360To(Texture sphericalImage, StereoViewType stereoView );
    void SetPanorama360To(Texture sphericalImageLeft, Texture sphericalImageRight );

    void SetHorizontalPanoramaTo(Texture horizontalPanoramaImage);

    void AddInteractifElements(params GameObject [] resourcesToAdd);

    void NotifyTheEnvironementAsClear();
    void NotifyTheEnvironementAsNeutralResetted();
    void NotifyTheEnvironementAsReady();

    void Clear();
    
}
public enum StereoViewType { HorizontalLeftRigth, HorizontalRightLeft, VerticalTopBottom, VerticalBottomTop }

public class ViewManager : MonoBehaviour, ViewManagerIntreface
{



    [Header("Horizontal Panoramic Renderer")]
    public Renderer _horizontalPanoramicView;
    [Header("Spherical Renderer")]
    public Renderer _sphericalPanoramicView;

    [Header("Stereo Renderer")]
    public Renderer _stereoSphericalPanoramicLeft;
    public Renderer _stereoSphericalPanoramicRight;

    [Header("Calibration Sphere")]
    public Renderer _calibrationPanorama;

    [Header("Neutral Environment")]
    public GameObject _neutralEnvironment;

    [Header("Layer of the view")]
    public string _monoViewLayer;
    public string _leftEyeesLayer;
    public string _rightEyeesLayer;


    [Header("Roots")]
    public Transform _oriantationRoot;
    public Transform _elementAddedRoot;



    void Start() {

        _horizontalPanoramicView.gameObject.layer = LayerMask.NameToLayer( _monoViewLayer);
        _sphericalPanoramicView.gameObject.layer = LayerMask.NameToLayer(_monoViewLayer);
        _calibrationPanorama.gameObject.layer = LayerMask.NameToLayer(_monoViewLayer);
        _neutralEnvironment.gameObject.layer = LayerMask.NameToLayer(_monoViewLayer);

        _stereoSphericalPanoramicLeft.gameObject.layer = LayerMask.NameToLayer(_leftEyeesLayer);
        _stereoSphericalPanoramicRight.gameObject.layer = LayerMask.NameToLayer(_rightEyeesLayer);

        ClearAndResetAsNeutral();
    }


    public void AddInteractifElements(params GameObject[] resourcesToAdd)
    {
        for (int i = 0; i < resourcesToAdd.Length; i++)
        {
            if (resourcesToAdd[i] == null)
                continue;
            GameObject resourcesCreated = GameObject.Instantiate(  resourcesToAdd[i]  );
            resourcesCreated.transform.parent = _elementAddedRoot;
            resourcesCreated.transform.localPosition = Vector3.zero;
            resourcesCreated.transform.localRotation = Quaternion.identity;
        }
    }

    public void ClearAndResetAsNeutral() {
        Clear();
        SetAsNeutralEnvrionment();
        SetGeneralRotationTo(new Vector3(0, -90, 0));
    }
    public void Clear()
    {

        for (int childIndex = _elementAddedRoot.childCount-1; childIndex >=0 ; childIndex--)
        {
           Destroy( _elementAddedRoot.GetChild(childIndex) );
        }
         _horizontalPanoramicView.gameObject.SetActive(false);
         _sphericalPanoramicView.gameObject.SetActive(false);
         _stereoSphericalPanoramicLeft.gameObject.SetActive(false);
         _stereoSphericalPanoramicRight.gameObject.SetActive(false);
         _calibrationPanorama.gameObject.SetActive(false);
         _neutralEnvironment.SetActive(false);

    }

    public void DisplayCalibrationView()
    {
        _calibrationPanorama.transform.forward = _oriantationRoot.forward;
        _calibrationPanorama.transform.position = _oriantationRoot.position;
        _calibrationPanorama.enabled = true;
    }


    public void SetAsNeutralEnvrionment()
    {
        _neutralEnvironment.transform.forward = _oriantationRoot.forward;
        _neutralEnvironment.transform.position = _oriantationRoot.position;
        _neutralEnvironment.SetActive(true);
    }

    public void SetGeneralRotationTo(Vector3 localEulerRotation)
    {
        _oriantationRoot.localRotation = Quaternion.Euler(localEulerRotation);
    }

    public void SetGeneralRotationTo(Quaternion localRotation)
    {
        _oriantationRoot.localRotation = localRotation;
    }

    public void SetHorizontalPanoramaTo(Texture horizontalPanoramaImage)
    {
        _horizontalPanoramicView.material.SetTextureOffset("_MainTex", Vector2.zero);
        _horizontalPanoramicView.material.SetTextureScale("_MainTex", Vector2.one);
        _horizontalPanoramicView.material.SetTexture("_MainTex", horizontalPanoramaImage);
        _horizontalPanoramicView.enabled = true;
    }

    public void SetPanorama360To(Texture sphericalImage)
    {

        _sphericalPanoramicView.material.SetTextureOffset("_MainTex", Vector2.zero);
        _sphericalPanoramicView.material.SetTextureScale("_MainTex", Vector2.one);
        _sphericalPanoramicView.material.SetTexture("_MainTex", sphericalImage);
        _sphericalPanoramicView.enabled = true;
    }

    public void SetPanorama360To(Texture sphericalImageLeft, Texture sphericalImageRight)
    {
        _stereoSphericalPanoramicLeft.material.SetTextureOffset("_MainTex", Vector2.zero);
        _stereoSphericalPanoramicLeft.material.SetTextureScale("_MainTex", Vector2.one);
        _stereoSphericalPanoramicLeft.material.SetTexture("_MainTex", sphericalImageLeft);
        _stereoSphericalPanoramicLeft.enabled = true;

        _stereoSphericalPanoramicRight.material.SetTextureOffset("_MainTex", Vector2.zero);
        _stereoSphericalPanoramicRight.material.SetTextureScale("_MainTex", Vector2.one);
        _stereoSphericalPanoramicRight.material.SetTexture("_MainTex", sphericalImageRight);
        _stereoSphericalPanoramicRight.enabled = true;
    }

    public void SetPanorama360To(Texture sphericalImage, StereoViewType stereoView)
    {

        Vector2 leftOffset, rightOffset;
        Vector2 leftScale, rightScale;

        if (stereoView == StereoViewType.HorizontalLeftRigth)
        {
            leftOffset = new Vector2(0f, 0f);
            leftScale = new Vector2(0.5f, 1f);
            rightOffset = new Vector2(0.5f, 0f);
            rightScale = new Vector2(0.5f, 1f);

        }
        else if (stereoView == StereoViewType.HorizontalRightLeft)
        {
            leftOffset = new Vector2(0.5f, 0f);
            leftScale = new Vector2(0.5f, 1f);
            rightOffset = new Vector2(0f, 0f);
            rightScale = new Vector2(0.5f, 1f);

        }
        else if (stereoView == StereoViewType.VerticalTopBottom)
        {
            leftOffset = new Vector2(0f, 0.5f);
            leftScale = new Vector2(1f, 0.5f);
            rightOffset = new Vector2(0f, 0f);
            rightScale = new Vector2(1f, 0.5f);

        }
        else if (stereoView == StereoViewType.VerticalBottomTop)
        {
            leftOffset = new Vector2(0f , 0f);
            leftScale = new Vector2(1f , 0.5f);
            rightOffset = new Vector2(0f , 0.5f);
            rightScale = new Vector2(1f , 0.5f);

        }
        else {

            leftOffset = new Vector2(0f, 0f);
            leftScale = new Vector2(1f, 1f);
            rightOffset = new Vector2(0f, 0f);
            rightScale = new Vector2(1f, 1f);
        }
        
        _stereoSphericalPanoramicLeft.material.SetTextureOffset("_MainTex", leftOffset);
        _stereoSphericalPanoramicLeft.material.SetTextureScale("_MainTex", leftScale);
        _stereoSphericalPanoramicLeft.material.SetTexture("_MainTex", sphericalImage);
        _stereoSphericalPanoramicLeft.enabled = true;

        _stereoSphericalPanoramicRight.material.SetTextureOffset("_MainTex", rightOffset);
        _stereoSphericalPanoramicRight.material.SetTextureScale("_MainTex", rightScale);
        _stereoSphericalPanoramicRight.material.SetTexture("_MainTex", sphericalImage);
        _stereoSphericalPanoramicRight.enabled = true;
    }




    public void NotifyTheEnvironementAsClear()
    {
        throw new NotImplementedException();
    }

    public void NotifyTheEnvironementAsNeutralResetted()
    {
        throw new NotImplementedException();
    }

    public void NotifyTheEnvironementAsReady()
    {
        throw new NotImplementedException();
    }

}
