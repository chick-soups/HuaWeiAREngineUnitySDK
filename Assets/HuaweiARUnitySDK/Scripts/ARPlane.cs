namespace HuaweiARUnitySDK
{
    using System;
    using HuaweiARInternal;
    using UnityEngine;
    using System.Collections.Generic;

    /**
     * \if english
     * @brief A plane in the real world detected by HUAWEI AR Engine.
     * 
     * Two or more planes may be automatially merged into a single parent plane. Assume that plane <em>Pa</em> is detected
     * earlier than plane <em>Pb</em>. Besides, <em>Pa</em> and <em>Pb</em> are actually belongs to a same plane in the real 
     * world. The result of merging <em>Pa</em> and <em>Pb</em> is that <em>Pb</em> is merged into <em>Pa</em>, namely
     * <em>Pa</em> is the parent plane of <em>Pb</em>. Calling \link GetSubsumedBy()\endlink on <em>Pb</em>'s object will 
     * returns <em>Pa</em> or <em>Pa</em>'s topmost parent. <em>Pb</em> will continue behaving as if it was independently 
     * tracked.
     * \else
     * @brief HUAWEI AR Engine检测到的真实世界中的平面。
     * 
     * 多个平面可能会自动合并成一个平面。假设平面<em>Pa</em>比平面<em>Pb</em>先被检测出来，并且两者数据真实世界中的
     * 同一个平面。那么，<em>Pb</em>将合并到<em>Pa</em>中，同时<em>Pa</em>将成为<em>Pb</em>的父平面。在<em>Pb</em>的对象上调用
     * \link GetSubsumedBy()\endlink 将返回<em>Pa</em>或者<em>Pa</em>的父平面。<em>Pb</em>在被合并后，仍然被独立地跟踪。
     * \endif
     */
    public class ARPlane:ARTrackable
    {
        /**
         * \if english
         * @brief Enumeration of detected plane type.
         * \else
         * @brief 平面类型的枚举。
         * \endif
         */
        public enum ARPlaneType
        {
            /**
             * \if english
             * horizontal upward facing plane, such as floor.
             * \else
             * 朝上的水平面，例如地面。
             * \endif
             */
            HORIZONTAL_UPWARD_FACING = 0,

            /**
             * \if english
             * horizontal downward facing plane, such as ceiling.
             * \else
             * 朝下的水平面，例如天花板。
             * \endif
             */
            HORIZONTAL_DOWNWARD_FACING = 1,

            /**
             * \if english
             * vertical facing plane.
             * \else
             * 垂直面。
             * \endif
             */
            VERTICAL_FACING = 2,

            /**
             * \if english
             * unknown facing plane.
             * \else
             * 未知朝向的平面。
             * \endif
             */
            UNKNOWN_FACING = 3,
        }

        /**
         * \if english
         * @deprecated Use \link ARPlaneType \endlink instead.
         * \else
         * @deprecated 请使用\link ARPlaneType \endlink。
         * \endif
         */
        [Obsolete] //use ARPlaneType instead
        public class PlaneType
        {
            /**
             * \if english
             * @deprecated Use \link ARPlaneType \endlink instead.
             * \else
             * @deprecated 请使用\link ARPlaneType \endlink。
             * \endif
             */
            public enum Type
            {
                HORIZONTAL_UPWARD_FACING = 0,
                HORIZONTAL_DOWNWARD_FACING = 1,
                VERTICAL_FACING = 2,
                UNKNOWN_FACING = 3,
            }
        }
        internal ARPlane(IntPtr trackableHandle, NDKSession session) : base(trackableHandle, session)
        {
        }

        /**
         * \if english
         * @brief Get the plane type.
         * @return Plane type.
         * \else
         * @brief 获取平面的类型。
         * @return 平面类型。
         * \endif
         */
        public ARPlaneType GetARPlaneType()
        {
            return m_ndkSession.PlaneAdapter.GetPlaneType(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Get the plane type.
         * @deprecated Use \link GetARPlaneType()\endlink instead.
         * \else
         * @brief 获取平面的类型。
         * @deprecated 请使用\link GetARPlaneType()\endlink。
         * \endif
         */
        [Obsolete]
        public PlaneType.Type GetPlaneType()
        {
            return (PlaneType.Type)((int)GetARPlaneType());
        }

        /**
         * \if english
         * @brief Get the parent plane of current plane.
         * 
         * If this plane has been subsumed, returns the parent plane this plane was merged into. If parent plane is 
         * also subsumed, this method will return the topmost non-subsumed plane.
         * @return the topmost non-subsumed plane which current plane has been merged into, or null if the plane
         * has not been subsumed.
         * 
         * \else
         * @brief 获取当前平面的父平面。
         * 
         * 如果当前平面被合并了，将会返回其父平面。如果该父平面也被合并，将会继续向上返回最顶层没有被合并的父平面。
         * @return 如果平面没有被合并，返回null，否则，返回最顶层没有被合并的父平面。
         * \endif
         */
        public ARPlane GetSubsumedBy()
        {
            return m_ndkSession.PlaneAdapter.AcquireSubsumedBy(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Gets the position and orientation of the plane's center in Unity world space.
         * \else
         * @brief 获取平面中心点在Unity世界坐标系的位姿。
         * \endif
         */
        public Pose GetCenterPose()
        {
            return m_ndkSession.PlaneAdapter.GetCenterPose(m_trackableHandle);
        }
        ///@cond EXCLUDE_DOXYGEN
        ///@deprecated Use GetCenterPose instead.
        [Obsolete]
        public Pose GetCenterPoseInUnity()
        {
            return GetCenterPose();
        }
        ///@endcond

        /**
         * \if english
         * @brief Gets the extent of plane in the X dimension, centered on the plane position.
         * \else
         * @brief 获取平面坐标系下当前平面在X轴的边界长度。
         * \endif
         */
        public float GetExtentX()
        {
            return m_ndkSession.PlaneAdapter.GetExtentX(m_trackableHandle);
        }
        /**
         * \if english
         * @brief Gets the extent of plane in the Z dimension, centered on the plane position.
         * \else
         * @brief 获取平面坐标系下当前平面在Z轴的边界长度。
         * \endif
         */
        public float GetExtentZ()
        {
            return m_ndkSession.PlaneAdapter.GetExtentZ(m_trackableHandle);
        }

        /**
         * \if english
         * @brief Gets a list of points (in clockwise order) in plane coordinate representing a boundary polygon for the plane.
         * 
         * The returned points are in plane coordinate and application need to transform them by \link GetCenterPose() \endlink
         * into the world coordinate.
         * @param[out] polygonList A list used to be filled with polygon points.The format is [x,0,z].
         * \else
         * @brief 获取平面坐标系下平面多边形的边界顶点。
         * 
         * 获取到的顶点在平面坐标系下，应用需要通过\link GetCenterPose()\endlink 转换到世界坐标系。
         * @param[out] polygonList 用于存放顶点的list。填充的数据格式为[x,0,z]。
         * \endif
         */
        public void GetPlanePolygon(List<Vector3> polygonList)
        {
            if (polygonList == null)
            {
                throw new ArgumentNullException();
            }
            polygonList.Clear();
            m_ndkSession.PlaneAdapter.GetPlanePolygon(m_trackableHandle, polygonList);
        }
        ///@cond EXCLUDE_DOXYGEN
        ///@deprecated use another function instead.
        [Obsolete]
        public void GetPlanePolygon(ref List<Vector3> polygonList)
        {
            GetPlanePolygon(polygonList);
        }
        ///@endcond

        /**
         * \if english
         * @brief Gets a list of points (in clockwise order) in plane coordinate representing a boundary polygon for the plane.
         * 
         * The returned points are in plane coordinate and application need to transform them by \link GetCenterPose() \endlink
         * into the world coordinate.
         * @param[out] polygonList A list used to be filled with polygon points.The format is [x,z].
         * \else
         * @brief 获取平面坐标系下平面多边形的边界顶点。
         * 
         * 获取到的顶点在平面坐标系下，应用需要通过\link GetCenterPose()\endlink 转换到世界坐标系。
         * @param[out] polygonList 用于存放顶点的list。填充的数据格式为[x,z]。
         * \endif
         */
        public void GetPlanePolygon(List<Vector2> polygonList)
        {
            if (polygonList == null)
            {
                throw new ArgumentNullException();
            }
            polygonList.Clear();
            List<Vector3> polygon3D = new List<Vector3>();
            GetPlanePolygon(polygon3D);
            foreach(Vector3 point in polygon3D)
            {
                polygonList.Add(new Vector2(point.x, point.z));
            }
        }
        ///@cond EXCLUDE_DOXYGEN
        ///@deprecated use another function instead.
        public void GetPlanePolygon(ref List<Vector2> polygonList)
        {
            GetPlanePolygon(polygonList);
        }
        ///@endcond

        /**
         * \if english
         * @brief Checks if the given pose in in the extent of the plane.
         * \else
         * @brief 检查给定的pose是否在平面的外接矩形范围内。
         * \endif
         */
        public bool IsPoseInExtents(Pose pose)
        {
            return m_ndkSession.PlaneAdapter.IsPoseInExtents(m_trackableHandle, pose);
        }

        /**
         * \if english
         * @brief Checks if the given pose in in the polygon of the plane.
         * \else
         * @brief 检查给定的pose是否在平面的多边形范围内。
         * \endif
         */
        public bool IsPoseInPolygon(Pose pose)
        {
            return m_ndkSession.PlaneAdapter.IsPoseInPolygon(m_trackableHandle,pose);
        }

        /**
         * \if english
         * @brief Enumeration of detected plane type.
         * \else
         * @brief 平面类型的枚举。
         * \endif
         */
        public enum ARPlaneSemanticLabel
        {
            /**
             * \if english
             * Other plane type.
             * \else
             * 其他类型的平面。
             * \endif
             */
            PLANE_OTHER = 0,
            /**
             * \if english
             * Wall.
             * \else
             * 墙面。
             * \endif
             */
            PLANE_WALL = 1,
            /**
             * \if english
             * Floor.
             * \else
             * 地面。
             * \endif
             */
            PLANE_FLOOR = 2,
            /**
             * \if english
             * Seat.
             * \else
             * 座位。
             * \endif
             */
            PLANE_SEAT = 3,
            /**
             * \if english
             * Table.
             * \else
             * 桌子。
             * \endif
             */
            PLANE_TABLE = 4,
            /**
             * \if english
             * Ceiling.
             * \else
             * 天花板。
             * \endif
             */
            PLANE_CEILING = 5
        }

        /**
         * \if english
         * @brief Get the plane semantic label.
         * @return Plane label.
         * \else
         * @brief 获取平面的语义类型。
         * @return 平面的语义类型。
         * \endif
         */
        public ARPlaneSemanticLabel GetARPlaneLabel()
        {
            return m_ndkSession.PlaneAdapter.GetLabel(m_trackableHandle);
        }
    }
}
