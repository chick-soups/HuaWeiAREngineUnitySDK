namespace CloudImageSample
{
    using System.Collections.Generic;
    using UnityEngine;
    using HuaweiARUnitySDK;

    public class CloudImageController : MonoBehaviour
    {
        List<ARAugmentedImage> m_Images;
        // Start is called before the first frame update
        void Start()
        {
            m_Images=new List<ARAugmentedImage>();
            AuthInfo authInfo = new AuthInfo()
            {
                appId = "102710907",
                appSecret = "ad4aa10a6dcb29523ba735d8721821665c8e6916fba225338716c6c8a5af0808",
                galleryId = "861ffae962113fed2628c662cff784b8d9dc1f304ee08d60"
            };
            ARSession.SetCloudServiceAuthInfo(authInfo);

        }
        private void Update() {
            ARFrame.GetTrackables<ARAugmentedImage>(m_Images,ARTrackableQueryFilter.UPDATED);
            foreach (var item in m_Images)
            {
                Debug.LogFormat("CloudID:{0} metaData:{1}",item.GetCloudImageId(),item.GetCloudImageMetadata());
            }
        }
    }
}
