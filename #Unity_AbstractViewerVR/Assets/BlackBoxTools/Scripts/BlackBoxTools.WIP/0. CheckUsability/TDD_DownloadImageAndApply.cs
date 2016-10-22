//using UnityEngine;
//using System.Collections;

//public class TDD_DownloadImageAndApply : MonoBehaviour {

//    public ApplyDownloadedTexture _applyDownloadTexture;

//    public string [] _imageId;
//    public float _delayBetweenLoad= 5f;
//    public int _imageToDisplayIndex;


//    IEnumerator Start () {

//        while (true) {
//            _applyDownloadTexture.DownloadAndApplyImage(_imageId[_imageToDisplayIndex]);
//            _imageToDisplayIndex = (_imageToDisplayIndex + 1) % _imageId.Length;
//            yield return new WaitForSeconds(_delayBetweenLoad);
//        }
//    }
	
//	void Update () {
	
//	}
//}
