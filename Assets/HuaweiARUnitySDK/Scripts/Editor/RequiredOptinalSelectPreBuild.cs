namespace HuaweiARInternal
{
    using UnityEditor.Build;
    using UnityEditor;
    using UnityEngine;
    internal class RequiredOptinalSelectPreBuild : IPreprocessBuild
    {
        public int callbackOrder
        {
            get
            {
                return 0;
            }
        }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            bool isHuaweiARReruired = HuaweiARProjectSettings.Instance.IsHuaweiARRequired;

            Debug.LogFormat("Building application with {0} HuaweiAR support", isHuaweiARReruired ? "Required" : "Optional");
            PluginHelper.GetImporterByPluginName("HUAWEI AR Engine Plugin_Required.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, isHuaweiARReruired);
            PluginHelper.GetImporterByPluginName("HUAWEI AR Engine Plugin_Optional.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, !isHuaweiARReruired);
        }
    }
}
