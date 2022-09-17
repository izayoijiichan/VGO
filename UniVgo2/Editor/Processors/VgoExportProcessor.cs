// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoExportProcessor
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using System;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// VGO Export Processor
    /// </summary>
    public static class VgoExportProcessor
    {
        /// <summary>
        /// Export VGO.
        /// </summary>
        /// <param name="geometryCoordinate">The type of the geometry coodinates.</param>
        /// <param name="uvCoordinate">The type of the UV coodinates.</param>
        /// <param name="isBson">Whether type is BSON, otherwise JSON.</param>
        /// <param name="cryptAlgorithm">The crypt algorithm.</param>
        /// <param name="cryptKey">The crypt key.</param>
        public static void ExportVgo(
            VgoGeometryCoordinate geometryCoordinate = VgoGeometryCoordinate.RightHanded,
            VgoUVCoordinate uvCoordinate = VgoUVCoordinate.TopLeft,
            bool isBson = false,
            string? cryptAlgorithms = null,
            byte[]? cryptKey = null)
        {
            EditorApplication.isPlaying = false;
            try
            {
                if (Validate(out string errorMessage) == false)
                {
                    Debug.LogAssertion(errorMessage);

                    EditorUtility.DisplayDialog("Error", errorMessage, "OK");

                    return;
                }

                if (Selection.activeObject is GameObject root)
                {
                    //
                }
                else
                {
                    return;
                }

                string path = EditorUtility.SaveFilePanel(title: "Save File Dialog", directory: "", defaultName: root.name + ".vgo", extension: "vgo");

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                var exporter = new VgoExporter();

                IVgoStorage vgoStorage = exporter.CreateVgoStorage(root, geometryCoordinate, uvCoordinate);

                FileInfo fileInfo = new FileInfo(path);

                string binFileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + ".bin";

                if (string.IsNullOrEmpty(cryptAlgorithms))
                {
                    if (isBson)
                    {
                        vgoStorage.ExportVgoFile(
                            filePath: path,
                            assetInfoTypeId: VgoChunkTypeID.AIPB,
                            layoutTypeId: VgoChunkTypeID.LAPB,
                            resourceAccessorTypeId: VgoChunkTypeID.RAPB
                        );
                    }
                    else
                    {
                        vgoStorage.ExportVgoFile(
                            filePath: path,
                            assetInfoTypeId: VgoChunkTypeID.AIPJ,
                            layoutTypeId: VgoChunkTypeID.LAPJ,
                            resourceAccessorTypeId: VgoChunkTypeID.RAPJ
                        );
                    }
                }
                else
                {
                    if (isBson)
                    {
                        vgoStorage.ExportVgoFile(
                            filePath: path,
                            assetInfoTypeId: VgoChunkTypeID.AIPB,
                            layoutTypeId: VgoChunkTypeID.LAPB,
                            resourceAccessorTypeId: VgoChunkTypeID.RACB,
                            resourceAccessorCryptTypeId: VgoChunkTypeID.CRAB,
                            resourceAccessorCryptAlgorithm: cryptAlgorithms,
                            resourceAccessorCryptKey: cryptKey
                        );
                    }
                    else
                    {
                        vgoStorage.ExportVgoFile(
                            filePath: path,
                            assetInfoTypeId: VgoChunkTypeID.AIPJ,
                            layoutTypeId: VgoChunkTypeID.LAPJ,
                            resourceAccessorTypeId: VgoChunkTypeID.RACJ,
                            resourceAccessorCryptTypeId: VgoChunkTypeID.CRAJ,
                            resourceAccessorCryptAlgorithm: cryptAlgorithms,
                            resourceAccessorCryptKey: cryptKey
                        );
                    }
                }

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

            //if (selectedGameObject.GetComponent<VgoGenerator>() == null)
            //{
            //    errorMessage = "Please attach a VgoGenerator component to the root GameObject.";
            //    return false;
            //}

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

            //if (selectedGameObject.GetComponent<VgoRight>() == null)
            //{
            //    errorMessage = "Please attach a VgoRight component to the root GameObject.";
            //    return false;
            //}

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

            errorMessage = string.Empty;

            return true;
        }
    }
}