namespace HuaweiARUnitySDK
{
    using HuaweiARInternal;
    using System;

    /**
     * \if english
     * @brief This class manages the status of AR Engine on this device.
     * \else
     * @brief 该类管理当前设备上AR Engine的状态。
     * \endif
     */
    public class AREnginesApk
    {
        private HuaweiArApkAdapter m_huaweiArApkAdapter;
        private static AREnginesApk m_huaweiArApk;

        private AREnginesApk()
        {
            m_huaweiArApkAdapter = new HuaweiArApkAdapter();
        }

        /**
         * \if english
         * @brief Instance of AREnginesApk.
         * \else
         * @brief AREnginesApk的实例。
         * \endif
         */
        public static AREnginesApk Instance
        {
            get
            {
                if (m_huaweiArApk == null)
                {
                    m_huaweiArApk = new AREnginesApk();
                }
                return m_huaweiArApk;
            }
        }

        /**
         * \if english
         * @brief Check the device availability asynchronously.
         * 
         * If <em>HUAWEI AR Engine.apk</em> is installed on current device, this method will return immediately.
         * Otherwise, this methods will connect with the Internet to download a supported device list of AREngine, 
         * and then compare with current device and OS. The connection timeout is set as 2 seconds.
         * <b>Note: since this connection will last for a little time, you can call it in a coroutine.</b>
         * @return The device availability. 
         * \else
         * @brief 异步检查设备能力。
         * 
         * 如果当前设备已经安装<em>HUAWEI AR Engine.apk</em>，该方法会立刻返回。否则，该方法会联网（超时2秒）下载一个AREngine的支持设备的列表，然后与当前的
         * 设备和系统进行比较。<b>注意：由于网络连接时间不确定，应用可以在协程中使用该方法。</b>
         * @return 设备的能力。
         * \endif
         */
        public ARAvailability CheckAvailability()
        {
            return m_huaweiArApkAdapter.CheckAvailability();
        }

        /**
         * \if english
         * @brief Request to insatll the <em>HUAWEI AR Engine.apk</em> synchronously.
         * 
         * We recommand you to call this method in 
         * <a href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html">OnApplictionPause(bool)</a>. 
         * When the application starts, set \c userRequestedInstall=true.
         * If <em>HUAWEI AR Engine.apk</em> is installed and compatiable, this method will return 
         * \link ARInstallStatus.INSTALLED\endlink immediately.
         * Otherwise, this function will firstly check current device availability. If the device is supported, this function 
         * show a window to prompt user. If user agree, it will jump to huawei application store. And then this function returns
         * \link ARInstallStatus.INSTALL_REQUESTED\endlink.
         * 
         * When your application resume, you should call this method again with \c userRequestedInstall=false. 
         * This will either return INSTALLED or throw an exception indicating the reason that installation could not be completed.
         * @param userRequestedInstall If set \c true, override the previous installation failure message and perform the installation again.
         * @return The Install status of HUAWEI AR Engine.
         * \else
         * @brief 同步请求安装<em>HAUWEI AR Engine.apk</em>。
         * 
         * 推荐在<a href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnApplicationPause.html">OnApplictionPause(bool)</a>
         * 中调用该方法。
         * 
         * 当应用启动时，设置 \c userRequestedInstall=true。如果已经安装<em>HUAWEI AR Engine.apk</em>并且与SDK版本兼容，该方法将直接
         * 返回\link ARInstallStatus.INSTALLED\endlink。否则，该方法将首先检查设备的兼容性。如果设备支持，该方法将弹出一个提示框，提示用户跳转
         * 到华为应用市场下载。
         * 
         * 当下载完成后，应用恢复，应用应该用\c userRequestedInstall=false 调用该方法。该方法将返回INSATLLED或者抛出异常。
         * @param userRequestedInstall 如果为\c true，将清除之前请求安装的错误信息，重新请求。
         * @return HUAWEI AR Engine的安装状态。
         * \endif
         * @exception ARUnavailableDeviceNotCompatibleException \copybrief ARUnavailableDeviceNotCompatibleException
         * @exception ARUnavailableEmuiNotCompatibleException \copybrief ARUnavailableEmuiNotCompatibleException
         * @exception ARUnavailableUserDeclinedInstallationException \copybrief ARUnavailableUserDeclinedInstallationException
         * @exception ARUnavailableConnectServerTimeOutException \copybrief ARUnavailableConnectServerTimeOutException
         */
        public ARInstallStatus RequestInstall(bool userRequestedInstall)
        {
            return m_huaweiArApkAdapter.RequestInstall(userRequestedInstall);
        }

        /**
         * \if english
         * @brief Check whether the <em>HUAWEI AR Engine.apk</em> is installed and compatible with current AR Engine SDK.
         * If <em>HUAWEI AR Engine.apk</em> is installed and compatiable, this method will return true.
         * @return Check whether HUAWEI AR Engine. APK is installed and compatible with the current AR Engine SDK.
         * \else
         * @brief 检查<em>HAUWEI AR Engine.apk</em>是否安装，并与当前的AR Engine SDK兼容。
         * 如果已经安装<em>HUAWEI AR Engine.apk</em>并且与SDK版本兼容，该方法将返回true。
         * @return HUAWEI AR Engine.apk是否安装并与当前AR Engine SDK兼容的检查结果。
         * \endif
         */
        public Boolean IsAREngineApkReady()
        {
            return m_huaweiArApkAdapter.IsAREngineApkReady();
        }
    }
}
