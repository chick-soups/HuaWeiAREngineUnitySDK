
namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;

    /**
     * \if english
     * @deprecated Use \link ARHitResult \endlink instead.
     * \else
     * @deprecated 请使用\link ARHitResult \endlink。
     * \endif
     */
    [Obsolete]
    public class ARPointCloudHitResult:ARHitResult
    {
        /// @cond EXCLUDE_DOXYGEN
        public ARPointCloud PointCloud { get; private set; }
        internal ARPointCloudHitResult(IntPtr hitResultHandle,NDKSession session,ARPointCloud pointCloud)
            : base(hitResultHandle, session)
        {
            PointCloud = pointCloud;
        }
        ///@endcond
    }
}
