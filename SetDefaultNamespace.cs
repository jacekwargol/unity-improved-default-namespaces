using System.IO;
using System.Text;
using UnityEditor;

public class SetDefaultNamespace : UnityEditor.AssetModificationProcessor {
    public static void OnWillCreateAsset(string metaFilePath) {
        var subfoldersToOmit = 2;

        var namespaceBeginToReplace = "#NAMESPACE#";
        var namespaceEndToReplace = "#NAMESPACEEND#";

        var filename = Path.GetFileNameWithoutExtension(metaFilePath);

        if (!filename.EndsWith(".cs")) {
            return;
        }

        var isDefaultNamespace = EditorSettings.projectGenerationRootNamespace.Length > 0;
        var file = $"{Path.GetDirectoryName(metaFilePath)}\\{filename}";
        var segmentedPath = $"{Path.GetDirectoryName(metaFilePath)}".Split('\\');
        var namespaceBegin = isDefaultNamespace ?
             "namespace " + EditorSettings.projectGenerationRootNamespace :
             "namespace ";
        var namespaceEnd = "";
        StringBuilder namespaceName = new StringBuilder();


        // Skip desired number of subfolders(e.g. Assets/Scripts)
        for (int i = subfoldersToOmit; i < segmentedPath.Length; i++) {
            namespaceName.Append("." + segmentedPath[i]);
        }
        // If there is no default namespace set for project, we must remove starting dot
        if(!isDefaultNamespace && namespaceName.Length > 0) {
            namespaceName.Remove(0, 1);
        }
             
        if (namespaceName.Length > 0 || isDefaultNamespace) {
            namespaceName.Insert(0, namespaceBegin);
            namespaceName.Append(" {");
            namespaceEnd += '}';
        }


        var fileContent = File.ReadAllText(file);
        var newContent = fileContent.Replace(namespaceBeginToReplace, namespaceName.ToString())
            .Replace(namespaceEndToReplace, namespaceEnd);

        File.WriteAllText(file, newContent);
        AssetDatabase.Refresh();
    }
}
