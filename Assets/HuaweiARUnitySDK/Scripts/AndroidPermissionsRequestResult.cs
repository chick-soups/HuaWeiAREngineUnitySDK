namespace HuaweiARUnitySDK
{
    /**
    * \if english
    * @brief The results of \link AndroidPermissionsRequest.RequestPermission() \endlink permission request.
    * \else
    * @brief \link AndroidPermissionsRequest.RequestPermission() \endlink权限请求的结果。
    * \endif
    */
    public class AndroidPermissionsRequestResult
    {

        /**
         * \if english
         * @brief The result of a permission request.
         * \else
         * @brief 权限请求的结果。
         * \endif
         */
        [System.Serializable]
        public class PermissionResult
        {
            /**
             * \if english
             * The name of permission.
             * \else
             * 权限名。
             * \endif
             */
            public string permissionName; 

            /**
             * \if english
             * \c 1 indicates  \link permissionName \endlink is granted. \c 0 indicates permissionName is denied.
             * \else
             * \link permissionName \endlink 是否被授权。值为\c 1，则表明被授权，否则，不被授权。
             * \endif
             */
            public int granted;
        }
        /**
         * \if english
         * The result array of permission request.
         * \else
         * 权限请求的结果数组。
         * \endif
         */
        public  PermissionResult[] Results
        {
            get;private set;
        }
        /**
         * \if english
         * Whether all permissions in \link Results \endlink are granted.
         * \else
         * \link Results \endlink中权限是否全部被授权。
         * \endif
         */
        public bool IsAllGranted
        {
            get
            {
                if (Results == null)
                {
                    return false;
                }

                for (int i = 0; i < Results.Length; i++)
                {
                    if (0 == Results[i].granted)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        ///@cond EXCLUDE_FROM_DOXYGEN
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="permissionResults"></param>
        public AndroidPermissionsRequestResult(PermissionResult[] permissionResults)
        {
            Results = permissionResults;
        }
        ///@endcond
    }
}
