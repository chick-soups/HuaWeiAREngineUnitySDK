//-----------------------------------------------------------------------
// <copyright file="CameraMetadataValue.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------
namespace HuaweiARUnitySDK
{
    using System;
    using System.Runtime.InteropServices;
    using HuaweiARInternal;
    /**
     * \if english
     * @brief Struct to contain camera metadata's value.
     * \else
     * @brief 相机元数据的结构体。
     * \endif
     */
    [StructLayout(LayoutKind.Explicit)]
    public struct ARCameraMetadataValue
    {
        [FieldOffset(0)]
        private NdkCameraMetadataType m_Type;
        [FieldOffset(4)]
        private sbyte m_ByteValue;
        [FieldOffset(4)]
        private int m_IntValue;
        [FieldOffset(4)]
        private long m_LongValue;
        [FieldOffset(4)]
        private float m_FloatValue;
        [FieldOffset(4)]
        private double m_DoubleValue;
        [FieldOffset(4)]
        private ARCameraMetadataRational m_RationalValue;

        /**
         * \if english
         * @brief Construct a ARCameraMetadataValue using sbyte.
         * @param byteValue The byte value set to the struct.
         * \else
         * @brief 使用sbyte构建一个ARCameraMetadataValue。
         * @param byteValue 要使用的byte值。
         * \endif
         */
        public ARCameraMetadataValue(sbyte byteValue)
        {
            m_IntValue = 0;
            m_LongValue = 0;
            m_FloatValue = 0;
            m_DoubleValue = 0;
            m_RationalValue = new ARCameraMetadataRational();

            m_Type = NdkCameraMetadataType.Byte;
            m_ByteValue = byteValue;
        }

        /**
         * \if english
         * @brief Construct a ARCameraMetadataValue using int.
         * @param intValue The int value set to the struct.
         * \else
         * @brief 使用int构建一个ARCameraMetadataValue。
         * @param intValue 要使用的int值。
         * \endif
         */
        public ARCameraMetadataValue(int intValue)
        {
            m_ByteValue = 0;
            m_LongValue = 0;
            m_FloatValue = 0;
            m_DoubleValue = 0;
            m_RationalValue = new ARCameraMetadataRational();

            m_Type = NdkCameraMetadataType.Int32;
            m_IntValue = intValue;
        }

        /**
         * \if english
         * @brief Construct a ARCameraMetadataValue using long.
         * @param longValue The long value set to the struct.
         * \else
         * @brief 使用long构建一个ARCameraMetadataValue。
         * @param longValue 要使用的long值。
         * \endif
         */
        public ARCameraMetadataValue(long longValue)
        {
            m_ByteValue = 0;
            m_IntValue = 0;
            m_FloatValue = 0;
            m_DoubleValue = 0;
            m_RationalValue = new ARCameraMetadataRational();

            m_Type = NdkCameraMetadataType.Int64;
            m_LongValue = longValue;
        }

        /**
         * \if english
         * @brief Construct a ARCameraMetadataValue using float.
         * @param floatValue The float value set to the struct.
         * \else
         * @brief 使用float构建一个ARCameraMetadataValue。
         * @param floatValue 要使用的float值。
         * \endif
         */
        public ARCameraMetadataValue(float floatValue)
        {
            m_ByteValue = 0;
            m_IntValue = 0;
            m_LongValue = 0;
            m_DoubleValue = 0;
            m_RationalValue = new ARCameraMetadataRational();

            m_Type = NdkCameraMetadataType.Float;
            m_FloatValue = floatValue;
        }

        /**
         * \if english
         * @brief Construct a ARCameraMetadataValue using double.
         * @param doubleValue The double value set to the struct.
         * \else
         * @brief 使用double构建一个ARCameraMetadataValue。
         * @param doubleValue 要使用的double值。
         * \endif
         */
        public ARCameraMetadataValue(double doubleValue)
        {
            m_ByteValue = 0;
            m_IntValue = 0;
            m_LongValue = 0;
            m_FloatValue = 0;
            m_RationalValue = new ARCameraMetadataRational();

            m_Type = NdkCameraMetadataType.Double;
            m_DoubleValue = doubleValue;
        }

        /**
         * \if english
         * @brief Construct a ARCameraMetadataValue using \link ARCameraMetadataRational \endlink.
         * @param rationalValue The rational value set to the struct.
         * \else
         * @brief 使用\link ARCameraMetadataRational \endlink 构建一个ARCameraMetadataValue。
         * @param rationalValue 要使用的有理数值。
         * \endif
         */
        public ARCameraMetadataValue(ARCameraMetadataRational rationalValue)
        {
            m_ByteValue = 0;
            m_IntValue = 0;
            m_LongValue = 0;
            m_FloatValue = 0;
            m_DoubleValue = 0;

            m_Type = NdkCameraMetadataType.Rational;
            m_RationalValue = rationalValue;
        }

        /**
         * \if english
         * @brief Gets the Type of ARCameraMetadataValue.
         * 
         * This Type must be used to call proper query function.
         * \else
         * @brief 获取ARCameraMetadataValue的值类型。
         * 
         * 该值用于调用合理的查询函数。如果返回值为int，那么应该使用\link AsInt()\endlink 取值。
         * \endif
         */
        public Type ValueType
        {
            get
            {
                switch (m_Type)
                {
                    case NdkCameraMetadataType.Byte:
                        return typeof(Byte);
                    case NdkCameraMetadataType.Int32:
                        return typeof(int);
                    case NdkCameraMetadataType.Float:
                        return typeof(float);
                    case NdkCameraMetadataType.Int64:
                        return typeof(long);
                    case NdkCameraMetadataType.Double:
                        return typeof(double);
                    case NdkCameraMetadataType.Rational:
                        return typeof(ARCameraMetadataRational);
                    default:
                        return null;
                }
            }
        }

        /**
         * \if english
         * @brief Gets sbyte value from the struct.
         * 
         * This function checks if the querying type matches the internal type field, and logs error if the types do not match.
         * @return Returns sbyte value stored in the struct.
         * \else
         * @brief 获取结构体的sbyte值。
         * 
         * 该方法会检查内部的类型，如果不匹配，则会用日志记录error。
         * @return 结构体存储的sbyte值。
         * \endif
         */
        public sbyte AsByte()
        {
            if (m_Type != NdkCameraMetadataType.Byte)
            {
                LogError(NdkCameraMetadataType.Byte);
            }

            return m_ByteValue;
        }

        /**
         * \if english
         * @brief Gets int value from the struct.
         * 
         * This function checks if the querying type matches the internal type field, and logs error if the types do not match.
         * @return Returns int value stored in the struct.
         * \else
         * @brief 获取结构体的int值。
         * 
         * 该方法会检查内部的类型，如果不匹配，则会用日志记录error。
         * @return 结构体存储的int值。
         * \endif
         */
        public int AsInt()
        {
            if (m_Type != NdkCameraMetadataType.Int32)
            {
                LogError(NdkCameraMetadataType.Int32);
            }

            return m_IntValue;
        }

        /**
         * \if english
         * @brief Gets float value from the struct.
         * 
         * This function checks if the querying type matches the internal type field, and logs error if the types do not match.
         * @return Returns int value stored in the struct.
         * \else
         * @brief 获取结构体的float值。
         * 
         * 该方法会检查内部的类型，如果不匹配，则会用日志记录error。
         * @return 结构体存储的float值。
         * \endif
         */
        public float AsFloat()
        {
            if (m_Type != NdkCameraMetadataType.Float)
            {
                LogError(NdkCameraMetadataType.Float);
            }

            return m_FloatValue;
        }

        /**
         * \if english
         * @brief Gets long value from the struct.
         * 
         * This function checks if the querying type matches the internal type field, and logs error if the types do not match.
         * @return Returns long value stored in the struct.
         * \else
         * @brief 获取结构体的long值。
         * 
         * 该方法会检查内部的类型，如果不匹配，则会用日志记录error。
         * @return 结构体存储的long值。
         * \endif
         */
        public long AsLong()
        {
            if (m_Type != NdkCameraMetadataType.Int64)
            {
                LogError(NdkCameraMetadataType.Int64);
            }

            return m_LongValue;
        }

        /**
         * \if english
         * @brief Gets double value from the struct.
         * 
         * This function checks if the querying type matches the internal type field, and logs error if the types do not match.
         * @return Returns double value stored in the struct.
         * \else
         * @brief 获取结构体的double值。
         * 
         * 该方法会检查内部的类型，如果不匹配，则会用日志记录error。
         * @return 结构体存储的double值。
         * \endif
         */
        public double AsDouble()
        {
            if (m_Type != NdkCameraMetadataType.Double)
            {
                LogError(NdkCameraMetadataType.Double);
            }

            return m_DoubleValue;
        }

        /**
         * \if english
         * @brief Gets rational value from the struct.
         * 
         * This function checks if the querying type matches the internal type field, and logs error if the types do not match.
         * @return Returns rational value stored in the struct.
         * \else
         * @brief 获取结构体的有理值。
         * 
         * 该方法会检查内部的类型，如果不匹配，则会用日志记录error。
         * @return 结构体存储的有理值。
         * \endif
         */
        public ARCameraMetadataRational AsRational()
        {
            if (m_Type != NdkCameraMetadataType.Rational)
            {
                LogError(NdkCameraMetadataType.Rational);
            }

            return m_RationalValue;
        }

        private void LogError(NdkCameraMetadataType requestedType)
        {
            ARDebug.LogError("Error getting value from ARCameraMetadataType due to type mismatch. " +
                    "requested type = {0}, internal type = {1}\n" , requestedType, m_Type);
        }
    }

    /**
     * \if english
     * @brief A struct follows the layout of ACameraMetadata_rational struct in NDK.
     * 
     * Please refer to <a href="https://developer.android.com/ndk/reference/ndk_camera_metadata_8h.html">NdkCameraMetadata.h</a>.
     * \else
     * @brief 与NDK中ACameraMetadata_rational一致的结构体。
     * 
     * 请参考 <a href="https://developer.android.com/ndk/reference/ndk_camera_metadata_8h.html">NdkCameraMetadata.h</a>。
     * \endif
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct ARCameraMetadataRational
    {
        /**
         * \if english 
         * @brief The numerator of the metadata rational.
         * \else
         * @brief 分子。
         * \endif
         */
        public int Numerator;

        /**
         * \if english 
         * @brief The denominator of the metadata rational.
         * \else
         * @brief 分母。
         * \endif
         */
        public int Denominator;
    }
}
