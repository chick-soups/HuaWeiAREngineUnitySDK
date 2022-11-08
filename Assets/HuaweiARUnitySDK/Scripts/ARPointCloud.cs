namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using System.Collections.Generic;
    using UnityEngine;

    /**
     * \if english
     * @brief Contains a set of observed 3D points and confidence values.
     * \else
     * @brief 包含发现的3D点以及置信度。
     * \endif
     */
    public class ARPointCloud
    {
        private IntPtr m_pointCloudHandle;
        private NDKSession m_ndkSession;

        internal ARPointCloud(IntPtr pointcloudHandle, NDKSession session)
        {
            m_ndkSession = session;
            m_pointCloudHandle = pointcloudHandle;
        }
        
        /**
         * \if english
         * @brief Get a list of points.
         * @param[out] pointList A list to be filled with points. The returned value only contains x/y/z coordinate.
         * \else
         * @brief 获取点云中的点。
         * @param[out] pointList 点集合，包含点的x/y/z三个坐标值。
         * \endif
         */
        public void GetPoints( List<Vector3> pointList)
        {
            if(null== pointList)
            {
                throw new ArgumentNullException();
            }
            if(IntPtr.Zero == m_pointCloudHandle)
            {
                throw new ARDeadlineExceededException();
            }
            pointList.Clear();
            m_ndkSession.PointCloudAdapter.CopyPoints(m_pointCloudHandle, pointList);
        }

        /**
         * \if english
         * @brief Get a list of points.
         * @param[out] pointList A list to be filled with points. The returned value contains x/y/z coordinate and w is the 
         * confidence.
         * \else
         * @brief 获取点云中的点。
         * @param[out] pointList 点集合，包含点的x/y/z三个坐标值，以及置信度。
         * \endif
         */
        public void GetPoints(List<Vector4> pointList)
        {
            if (null == pointList)
            {
                throw new ArgumentNullException();
            }
            if (IntPtr.Zero == m_pointCloudHandle)
            {
                throw new ARDeadlineExceededException();
            }
            pointList.Clear();
            m_ndkSession.PointCloudAdapter.CopyPoints(m_pointCloudHandle, pointList);
        }

        ///@cond EXCLUDE_DOXYGEN
        ///@deprecated use another function.
        public void GetPoints(ref List<Vector3> pointList)
        {
            GetPoints(pointList);
        }
        ///@endcond

        /**
         * \if english
         * @brief Get the timestamp of the point cloud in nanoseconds.
         * \else
         * @brief 获取点云的时戳，以纳秒为单位。
         * \endif
         */
        public long GetTimestampNs()
        {
            if (IntPtr.Zero == m_pointCloudHandle)
            {
                throw new ARDeadlineExceededException();
            }
            return m_ndkSession.PointCloudAdapter.GetTimestamp(m_pointCloudHandle);
        }

        /**
         * \if english
         * @brief Release this point cloud.
         * \else
         * @brief 释放当前点云资源。
         * \endif
         */
        public void Release()
        {
            if (m_pointCloudHandle != IntPtr.Zero)
            {
                m_ndkSession.PointCloudAdapter.Release(m_pointCloudHandle);
            }
            m_pointCloudHandle = IntPtr.Zero;
        }

        ~ARPointCloud()
        {
            if (m_pointCloudHandle != IntPtr.Zero)
            {
                m_ndkSession.PointCloudAdapter.Release(m_pointCloudHandle);
            }
        }
    }
}
