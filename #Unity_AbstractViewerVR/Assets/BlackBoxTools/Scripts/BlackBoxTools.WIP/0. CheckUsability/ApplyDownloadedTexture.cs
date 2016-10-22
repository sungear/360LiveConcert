//using UnityEngine;
//using System.Collections;
//using BlackBox.Tools;
//using System;

//public class ApplyDownloadedTexture : MonoBehaviour {


//    public string _webSiteUrl = "http://www.jams.center/resources/360/";
//    public MeshRenderer _materialAffected;

//    public void DownloadTextureAndApplyOnMeshBasedOnUrl(string url) {

//        WebPageUnityLoader page = WebAccessor.LoadPage(url, true);
//        page.onEndPageLoading += ApplyTexture;

//    }

//    internal void DownloadAndApplyImage(string imageName)
//    {
//        DownloadTextureAndApplyOnMeshBasedOnUrl(_webSiteUrl + imageName);
//    }

//    private void ApplyTexture(string urlSent, string textReceived, string error, ref WebPageUnityLoader info)
//    {
//        Texture textureloaded = info.ImageLoad;
//        if (textureloaded != null && _materialAffected.material!=null) { 
//            _materialAffected.material.mainTexture = textureloaded;
//        }

//        info.AutoDestruction();
//    }
//}
