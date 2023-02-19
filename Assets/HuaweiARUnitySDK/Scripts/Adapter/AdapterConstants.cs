namespace HuaweiARInternal
{
    using System;
    using System.Linq;
    using HuaweiARUnitySDK;
    internal static class AdapterConstants
    {

        public const string HuaweiARNativeApi = "huawei_arengine_ndk";
        public const string UnityPluginApi = "huawei_arengine_unity_plugin";
        public const string NDKCameraApi = "camera2ndk";
        public const string NDKMediaApi = "mediandk";
        public const string NDKAndroidHelper= "HuaWeiAREngineAndroidHelper";
        

        public static int Enum_TrackingState_MaxIntValue = Enum.GetValues(typeof(ARTrackable.TrackingState))
            .Cast<int>().Max();
        public static int Enum_TrackingState_MinIntValue = Enum.GetValues(typeof(ARTrackable.TrackingState))
            .Cast<int>().Min();

        public static int Enum_CoordSystem_MaxIntValue = Enum.GetValues(typeof(ARCoordinateSystemType))
            .Cast<int>().Max();
        public static int Enum_CoordSystem_MinIntValue = Enum.GetValues(typeof(ARCoordinateSystemType))
            .Cast<int>().Min();

        public static int Enum_FaceBlendShapeLocation_MaxIntValue = (int)ARFace.BlendShapeLocation
            .Animoji_BLENDSHAPE_LENGTH;
        public static int Enum_FaceBlendShapeLocation_MinIntValue = Enum.GetValues(typeof(ARFace.BlendShapeLocation))
            .Cast<int>().Min();

        public static int Enum_FaceLabel_MaxIntValue = (int)ARFaceGeometry.Label.LABELS_LENGTH;
        public static int Enum_FaceLabel_MinIntValue = Enum.GetValues(typeof(ARFaceGeometry.Label))
            .Cast<int>().Min();

        public static int Enum_HandType_MaxIntValue = Enum.GetValues(typeof(ARHand.HandType))
            .Cast<int>().Max();
        public static int Enum_HandType_MinIntValue = Enum.GetValues(typeof(ARHand.HandType))
            .Cast<int>().Min();

        public static int Enum_ARWorldMappingState_MaxIntValue = Enum.GetValues(typeof(ARWorldMappingState))
            .Cast<int>().Max();
        public static int Enum_ARWorldMappingState_MinIntValue = Enum.GetValues(typeof(ARWorldMappingState))
            .Cast<int>().Min();

        public static int Enum_ARAlignState_MaxIntValue = Enum.GetValues(typeof(ARAlignState))
            .Cast<int>().Max();
        public static int Enum_ARAlignState_MinIntValue = Enum.GetValues(typeof(ARAlignState))
            .Cast<int>().Min();

        public static int Enum_BodySkeletonPointName_MaxIntValue = Enum.GetValues(typeof(ARBody.SkeletonPointName))
            .Cast<int>().Max();
        public static int Enum_BodySkeletonPointName_MinIntValue = Enum.GetValues(typeof(ARBody.SkeletonPointName))
            .Cast<int>().Min();

        public static int Enum_HandSkeletonPointName_MaxIntValue = Enum.GetValues(typeof(ARHand.SkeletonPointName))
            .Cast<int>().Max();
        public static int Enum_HandSkeletonPointName_MinIntValue = Enum.GetValues(typeof(ARHand.SkeletonPointName))
            .Cast<int>().Min();
    }
}
