[1mdiff --git a/Assets/NavMesh.meta b/Assets/NavMesh.meta[m
[1mnew file mode 100644[m
[1mindex 00000000..70df889a[m
[1m--- /dev/null[m
[1m+++ b/Assets/NavMesh.meta[m
[36m@@ -0,0 +1,8 @@[m
[32m+[m[32mfileFormatVersion: 2[m
[32m+[m[32mguid: e3b674cf045a2d14c97e4236a56e027e[m
[32m+[m[32mfolderAsset: yes[m
[32m+[m[32mDefaultImporter:[m
[32m+[m[32m  externalObjects: {}[m
[32m+[m[32m  userData:[m[41m [m
[32m+[m[32m  assetBundleName:[m[41m [m
[32m+[m[32m  assetBundleVariant:[m[41m [m
[1mdiff --git a/Assets/NavMesh/Editor.meta b/Assets/NavMesh/Editor.meta[m
[1mnew file mode 100644[m
[1mindex 00000000..9ca8ba65[m
[1m--- /dev/null[m
[1m+++ b/Assets/NavMesh/Editor.meta[m
[36m@@ -0,0 +1,9 @@[m
[32m+[m[32mfileFormatVersion: 2[m
[32m+[m[32mguid: 63b588f3892bb4b5eb73ad3d2791e05c[m
[32m+[m[32mfolderAsset: yes[m
[32m+[m[32mtimeCreated: 1477656493[m
[32m+[m[32mlicenseType: Pro[m
[32m+[m[32mDefaultImporter:[m
[32m+[m[32m  userData:[m[41m [m
[32m+[m[32m  assetBundleName:[m[41m [m
[32m+[m[32m  assetBundleVariant:[m[41m [m
[1mdiff --git a/Assets/NavMesh/Editor/NavMeshAssetManager.cs b/Assets/NavMesh/Editor/NavMeshAssetManager.cs[m
[1mnew file mode 100644[m
[1mindex 00000000..f372c61b[m
[1m--- /dev/null[m
[1m+++ b/Assets/NavMesh/Editor/NavMeshAssetManager.cs[m
[36m@@ -0,0 +1,334 @@[m
[32m+[m[32musing System.Collections.Generic;[m
[32m+[m[32musing System.IO;[m
[32m+[m[32musing UnityEditor.Experimental.SceneManagement;[m
[32m+[m[32musing UnityEditor.SceneManagement;[m
[32m+[m[32musing UnityEngine.AI;[m
[32m+[m[32musing UnityEngine;[m
[32m+[m
[32m+[m[32mnamespace UnityEditor.AI[m
[32m+[m[32m{[m
[32m+[m[32m    public class NavMeshAssetManager : ScriptableSingleton<NavMeshAssetManager>[m
[32m+[m[32m    {[m
[32m+[m[32m        internal struct AsyncBakeOperation[m
[32m+[m[32m        {[m
[32m+[m[32m            public NavMeshSurface surface;[m
[32m+[m[32m            public NavMeshData bakeData;[m
[32m+[m[32m            public AsyncOperation bakeOperation;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        List<AsyncBakeOperation> m_BakeOperations = new List<AsyncBakeOperation>();[m
[32m+[m[32m        internal List<AsyncBakeOperation> GetBakeOperations() { return m_BakeOperations; }[m
[32m+[m
[32m+[m[32m        struct SavedPrefabNavMeshData[m
[32m+[m[32m        {[m
[32m+[m[32m            public NavMeshSurface surface;[m
[32m+[m[32m            public NavMeshData navMeshData;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        List<SavedPrefabNavMeshData> m_PrefabNavMeshDataAssets = new List<SavedPrefabNavMeshData>();[m
[32m+[m
[32m+[m[32m        static string GetAndEnsureTargetPath(NavMeshSurface surface)[m
[32m+[m[32m        {[m
[32m+[m[32m            // Create directory for the asset if it does not exist yet.[m
[32m+[m[32m            var activeScenePath = surface.gameObject.scene.path;[m
[32m+[m
[32m+[m[32m            var targetPath = "Assets";[m
[32m+[m[32m            if (!string.IsNullOrEmpty(activeScenePath))[m
[32m+[m[32m            {[m
[32m+[m[32m                targetPath = Path.Combine(Path.GetDirectoryName(activeScenePath), Path.GetFileNameWithoutExtension(activeScenePath));[m
[32m+[m[32m            }[m
[32m+[m[32m            else[m
[32m+[m[32m            {[m
[32m+[m[32m                var prefabStage = PrefabStageUtility.GetPrefabStage(surface.gameObject);[m
[32m+[m[32m                var isPartOfPrefab = prefabStage != null && prefabStage.IsPartOfPrefabContents(surface.gameObject);[m
[32m+[m
[32m+[m[32m                if (isPartOfPrefab)[m
[32m+[m[32m                {[m
[32m+[m[32m#if UNITY_2020_1_OR_NEWER[m
[32m+[m[32m                    var assetPath = prefabStage.assetPath;[m
[32m+[m[32m#else[m
[32m+[m[32m                    var assetPath = prefabStage.prefabAssetPath;[m
[32m+[m[32m#endif[m
[32m+[m[32m                    if (!string.IsNullOrEmpty(assetPath))[m
[32m+[m[32m                    {[m
[32m+[m[32m                        var prefabDirectoryName = Path.GetDirectoryName(assetPath);[m
[32m+[m[32m                        if (!string.IsNullOrEmpty(prefabDirectoryName))[m
[32m+[m[32m                            targetPath = prefabDirectoryName;[m
[32m+[m[32m                    }[m
[32m+[m[32m                }[m
[32m+[m[32m            }[m
[32m+[m[32m            if (!Directory.Exists(targetPath))[m
[32m+[m[32m                Directory.CreateDirectory(targetPath);[m
[32m+[m[32m            return targetPath;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        static void CreateNavMeshAsset(NavMeshSurface surface)[m
[32m+[m[32m        {[m
[32m+[m[32m            var targetPath = GetAndEnsureTargetPath(surface);[m
[32m+[m
[32m+[m[32m            var combinedAssetPath = Path.Combine(targetPath, "NavMesh-" + surface.name + ".asset");[m
[32m+[m[32m            combinedAssetPath = AssetDatabase.GenerateUniqueAssetPath(combinedAssetPath);[m
[32m+[m[32m            AssetDatabase.CreateAsset(surface.navMeshData, combinedAssetPath);[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        NavMeshData GetNavMeshAssetToDelete(NavMeshSurface navSurface)[m
[32m+[m[32m        {[m
[32m+[m[32m            if (PrefabUtility.IsPartOfPrefabInstance(navSurface) && !PrefabUtility.IsPartOfModelPrefab(navSurface))[m
[32m+[m[32m            {[m
[32m+[m[32m                // Don't allow deleting the asset belonging to the prefab parent[m
[32m+[m[32m                var parentSurface = PrefabUtility.GetCorrespondingObjectFromSource(navSurface) as NavMeshSurface;[m
[32m+[m[32m                if (parentSurface && navSurface.navMeshData == parentSurface.navMeshData)[m
[32m+[m[32m                    return null;[m
[32m+[m[32m            }[m
[32m+[m
[32m+[m[32m            // Do not delete the NavMeshData asset referenced from a prefab until the prefab is saved[m
[32m+[m[32m            var prefabStage = PrefabStageUtility.GetPrefabStage(navSurface.gameObject);[m
[32m+[m[32m            var isPartOfPrefab = prefabStage != null && prefabStage.IsPartOfPrefabContents(navSurface.gameObject);[m
[32m+[m[32m            if (isPartOfPrefab && IsCurrentPrefabNavMeshDataStored(navSurface))[m
[32m+[m[32m                return null;[m
[32m+[m
[32m+[m[32m            return navSurface.navMeshData;[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        void ClearSurface(NavMeshSurface navSurface)[m
[32m+[m[32m        {[m
[32m+[m[32m            var hasNavMeshData = navSurface.navMeshData != null;[m
[32m+[m[32m            StoreNavMeshDataIfInPrefab(navSurface);[m
[32m+[m
[32m+[m[32m            var assetToDelete = GetNavMeshAssetToDelete(navSurface);[m
[32m+[m[32m            navSurface.RemoveData();[m
[32m+[m
[32m+[m[32m            if (hasNavMeshData)[m
[32m+[m[32m            {[m
[32m+[m[32m                SetNavMeshData(navSurface, null);[m
[32m+[m[32m                EditorSceneManager.MarkSceneDirty(navSurface.gameObject.scene);[m
[32m+[m[32m            }[m
[32m+[m
[32m+[m[32m            if (assetToDelete)[m
[32m+[m[32m                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(assetToDelete));[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        public void StartBakingSurfaces(UnityEngine.Object[] surfaces)[m
[32m+[m[32m        {[m
[32m+[m[32m            // Remove first to avoid double registration of the callback[m
[32m+[m[32m            EditorApplication.update -= UpdateAsyncBuildOperations;[m
[32m+[m[32m            EditorApplication.update += UpdateAsyncBuildOperations;[m
[32m+[m
[32m+[m[32m            foreach (NavMeshSurface surf in surfaces)[m
[32m+[m[32m            {[m
[32m+[m[32m                StoreNavMeshDataIfInPrefab(surf);[m
[32m+[m
[32m+[m[32m                var oper = new AsyncBakeOperation();[m
[32m+[m
[32m+[m[32m                oper.bakeData = 