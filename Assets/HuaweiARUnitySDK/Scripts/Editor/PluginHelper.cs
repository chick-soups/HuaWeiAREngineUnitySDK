namespace HuaweiARInternal
{
    using UnityEditor;
    using System.IO;
    using UnityEditor.Build;
    class PluginHelper
    {
        public static PluginImporter GetImporterByPluginName(string name)
        {
            string[] guids = AssetDatabase.FindAssets(Path.GetFileNameWithoutExtension(name));

            PluginImporter importer = null;
            int count = 0;
            foreach(string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (Path.GetFileName(path) == name)
                {
                    importer = AssetImporter.GetAtPath(path) as PluginImporter;
                    count++;
                }
            }

            if(0==count)
            {
                throw new BuildFailedException(string.Format("plugin {0} is not found!",name));
            }
            else if (count>1)
            {
                throw new BuildFailedException(string.Format("multiple plugin {0} are found",name));
            }
            else if(null==importer)
            {
                throw new BuildFailedException(string.Format("{0} is found, however it is not a plugin.",name));
            }
            return importer;
        }
    }
}
