// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : VgoExportProcessor
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using System;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using VgoGltf;

    /// <summary>
    /// VGO Export Processor
    /// </summary>
    public static class VgoExportProcessor
    {
        /// <summary>
        /// Export VGO.
        /// </summary>
        public static void ExportVgo()
        {
            EditorApplication.isPlaying = false;
            try
            {
                string errorMessage;

                if (!Validate(out errorMessage))
                {
                    Debug.LogAssertion(errorMessage);

                    EditorUtility.DisplayDialog("Error", errorMessage, "OK");

                    return;
                }

                GameObject root = Selection.activeObject as GameObject;

                string path = EditorUtility.SaveFilePanel(title: "Save File Dialog", directory: "", defaultName: root.name + ".vgo", extension: "vgo");

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                GltfStorage gltfStorage;

                using (var exporter = new VgoExporter())
                {
                    gltfStorage = exporter.CreateGltfStorage(root);
                }

                gltfStorage.ExportGlbFile(path);

                Debug.LogFormat("Export VGO file.\nGameObject: {0}, output: {1}", root.name, path);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Perform verification.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>Returns true if validation is successful, false otherwise.</returns>
        private static bool Validate(out string errorMessage)
        {
            GameObject[] selectedGameObjects = Selection.gameObjects;

            if (selectedGameObjects.Length == 0)
            {
                errorMessage = "Select a GameObject with a VgoMeta component attached.";
                return false;
            }

            if (2 <= selectedGameObjects.Length)
            {
                errorMessage = "Please select only one GameObject.";
                return false;
            }

            GameObject selectedGameObject = selectedGameObjects[0];

            if (selectedGameObject.GetComponent<VgoMeta>() == null)
            {
                errorMessage = "Please attach a VgoMeta component to the root GameObject.";
                return false;
            }

            if (selectedGameObject.transform.position != Vector3.zero)
            {
                errorMessage = "Please set Transform Position(0, 0, 0).";
                return false;
            }

            if (selectedGameObject.transform.rotation != Quaternion.identity)
            {
                errorMessage = "Please set Transform Rotation(0, 0, 0).";
                return false;
            }

            if (selectedGameObject.transform.localScale != Vector3.one)
            {
                errorMessage = "Please set Transform Scale(1, 1, 1).";
                return false;
            }

            if (selectedGameObject.GetComponent<VgoRight>() == null)
            {
                errorMessage = "Please attach a VgoRight component to the root GameObject.";
                return false;
            }

            if (selectedGameObject.GetComponent<Rigidbody>() != null)
            {
                errorMessage = "The root GameObject is not allowed to attach the Rigidbody component.";
                return false;
            }

            if (selectedGameObject.GetComponents<Collider>().Any())
            {
                errorMessage = "The root GameObject is not allowed to attach the Collider component.";
                return false;
            }

            Collider[] colliders = selectedGameObject.GetComponentsInChildren<Collider>();

            Type boxColliderType = typeof(BoxCollider);
            Type capsuleColliderType = typeof(CapsuleCollider);
            Type sphereColliderType = typeof(SphereCollider);

            foreach (Collider collider in colliders)
            {
                Type collierType = collider.GetType();

                if ((collierType == boxColliderType) ||
                    (collierType == capsuleColliderType) ||
                    (collierType == sphereColliderType))
                {
                    continue;
                }

                errorMessage = string.Format("Not supported collier is found. GameObject: {0}", collider.gameObject.name);

                return false;
            }

            VgoRight[] vgoRights = selectedGameObject.GetComponentsInChildren<VgoRight>();

            foreach (VgoRight vgoRight in vgoRights)
            {
                if (VgoRightValidator.Validate(vgoRight, out errorMessage) == false)
                {
                    errorMessage = string.Format("There is something wrong with the VgoRight entry for '{0}' GameObject. {1}", vgoRight.gameObject.name, errorMessage);
                    return false;
                }
            }

            errorMessage = string.Empty;

            return true;
        }
    }
}