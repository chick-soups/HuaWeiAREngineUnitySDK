///@cond ContainFaceAR
namespace HuaweiARUnitySDK
{
    using UnityEngine;
    using System;
    using HuaweiARInternal;
    /**
     * \if english
     * @brief Geomtery of a Face.
     * \else
     * @brief 人脸模型。
     * \endif
     */
    public class ARFaceGeometry
    {
        private IntPtr m_faceGeometryHandle;
        private NDKSession m_session;

        internal ARFaceGeometry(IntPtr handle, NDKSession session)
        {
            m_faceGeometryHandle = handle;
            m_session = session;
        }
        ~ARFaceGeometry()
        {
            m_session.FaceGeometryAdapter.Release(m_faceGeometryHandle);
        }
        /**
         * \if english
         * Vertices of face mesh. These vertices are under face coordinate. 
         * \else
         * 基于人脸坐标系的人脸mesh中的顶点。
         * \endif
         */
        public Vector3[] Vertices
        {
            get
            {
                return m_session.FaceGeometryAdapter.GetVertices(m_faceGeometryHandle);
            }
        }

        /**
         * \if english
         * Texture coordinate of face mesh.
         * \else
         * 纹理坐标。
         * \endif
         */
        public Vector2[] TextureCoordinates
        {
            get
            {
                return m_session.FaceGeometryAdapter.GetTexCoord(m_faceGeometryHandle);
            }
        }

        /**
         * \if english
         * Triangle count of face mesh.
         * \else
         * 三角面片个数。
         * \endif
         */
        public int TriangleCount
        {
            get
            {
                return m_session.FaceGeometryAdapter.GetTriangleCount(m_faceGeometryHandle);
            }
        }

        /**
         * \if english
         * Triangle indices of face mesh.
         * \else
         * 三角面顶点的索引。
         * \endif
         */
        public int[] TriangleIndices
        {
            get
            {
                return m_session.FaceGeometryAdapter.GetTriangleIndex(m_faceGeometryHandle);
            }
        }

        /**
         * \if english
         * Triangle label of face mesh.
         * \else
         * 三角面的标识。
         * \endif
         */
        public Label[] TriangleLabel
        {
            get
            {
                return m_session.FaceGeometryAdapter.GetTriangleLabels(m_faceGeometryHandle);
            }
        }
        
        /**
         * \if english
         * Enumeration of triangle label.
         * \else
         * 三角面标识的枚举。
         * \endif
         */
        public enum Label
        {
            /**
             * \if english
             * Indicate a triangle does not belong to face.
             * \else
             * 表明三角面片不位于人脸。
             * \endif
             */
            Label_Non_Face = -1,

            /**
             * \if english
             * Indicate a triangle belongs to other part of face, except the follwing enumeration.
             * \else
             * 表明三角面片位于人脸，但不属于下列枚举的范围内。
             * \endif
             */
            Label_Face_Other = 0,

            /**
             * \if english
             * Indicate a triangle belongs to lower lip.
             * \else
             * 表明三角面片位于人脸下嘴唇处。
             * \endif
             */
            Label_Lower_Lip = 1,

            /**
             * \if english
             * Indicate a triangle belongs to upper lip.
             * \else
             * 表明三角面片位于人脸上嘴唇处。
             * \endif
             */
            Label_Upper_Lip = 2,

            /**
             * \if english
             * Indicate a triangle belongs to left eye.
             * \else
             * 表明三角面片位于人脸左眼处。
             * \endif
             */
            Label_Left_Eye = 3,

            /**
             * \if english
             * Indicate a triangle belongs to right eye.
             * \else
             * 表明三角面片位于人脸右眼处。
             * \endif
             */
            Label_Right_Eye = 4,

            /**
             * \if english
             * Indicate a triangle belongs to left brow.
             * \else
             * 表明三角面片位于人脸左眉处。
             * \endif
             */
            Label_Left_Brow = 5,

            /**
             * \if english
             * Indicate a triangle belongs to right brow.
             * \else
             * 表明三角面片位于人脸右眉处。
             * \endif
             */
            Label_Right_Brow = 6,

            /**
             * \if english
             * Indicate a triangle belongs to center of brows.
             * \else
             * 表明三角面片位于人脸双眉间处。
             * \endif
             */
            Label_Brow_Center = 7,

            /**
             * \if english
             * Indicate a triangle belongs to nose.
             * \else
             * 表明三角面片位于人脸鼻子处。
             * \endif
             */
            Label_Nose = 8,

            /**
             * \if english
             * Not a valid value. Only used to indicate the length of label.
             * \else
             * 无效枚举，只用于表示标识的个数。
             * \endif
             */
            LABELS_LENGTH = 9
        }
    }
}
///@endcond
