﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Editor
// @Class     : VgoExportProcessor
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Editor
{
    using NewtonVgo;
    using NewtonVgo.Security.Cryptography;
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
        /// <summary>The type of box collider.</summary>
        private static readonly Type _BoxColliderType = typeof(BoxCollider);

        /// <summary>The type of capsule collider.</summary>
        private static readonly Type _CapsuleColliderType = typeof(CapsuleCollider);

        /// <summary>The type of sphere collider.</summary>
        private static readonly Type _SphereColliderType = typeof(SphereCollider);

        /// <summary>
        /// Export VGO.
        /// </summary>
        /// <param name="gameObject">The target.</param>
        /// <param name="geometryCoordinate">The type of the geometry coordinates.</param>
        /// <param name="uvCoordinate">The type of the UV coordinates.</param>
        /// <param name="textureType">The type of the texture.</param>
        /// <param name="isBson">Whether type is BSON, otherwise JSON.</param>
        /// <param name="cryptAlgorithms">The crypt algorithm.</param>
        public static void ExportVgo(
            in GameObject gameObject,
            in VgoGeometryCoordinate geometryCoordinate = VgoGeometryCoordinate.RightHanded,
            in VgoUVCoordinate uvCoordinate = VgoUVCoordinate.TopLeft,
            in ImageType textureType = ImageType.PNG,
            in bool isBson = false,
            in string? cryptAlgorithms = null)
        {
            try
            {
                if (Validate(gameObject, out string errorMessage) == false)
                {
                    Debug.LogAssertion(errorMessage);

                    EditorUtility.DisplayDialog("Error", errorMessage, "OK");

                    return;
                }

                string vgoFilePath = EditorUtility.SaveFilePanel(title: "Save File Dialog", directory: "", defaultName: gameObject.name + ".vgo", extension: "vgo");

                if (string.IsNullOrEmpty(vgoFilePath))
                {
                    return;
                }

                var stopwatch = new System.Diagnostics.Stopwatch();

                stopwatch.Start();

                var exporter = new VgoExporter();

                IVgoStorage vgoStorage = exporter.CreateVgoStorage(gameObject, geometryCoordinate, uvCoordinate, textureType);

                VgoExportSetting vgoExportSetting;

                if (isBson)
                {
                    vgoExportSetting = new VgoExportSetting
                    {
                        AssetInfoTypeId = VgoChunkTypeID.AIPB,
                        LayoutTypeId = VgoChunkTypeID.LAPB,
                    };
                }
                else
                {
                    vgoExportSetting = new VgoExportSetting
                    {
                        AssetInfoTypeId = VgoChunkTypeID.AIPJ,
                        LayoutTypeId = VgoChunkTypeID.LAPJ,
                    };
                }

                if (string.IsNullOrEmpty(cryptAlgorithms))
                {
                    if (isBson)
                    {
                        vgoExportSetting.ResourceAccessorTypeId = VgoChunkTypeID.RAPB;
                    }
                    else
                    {
                        vgoExportSetting.ResourceAccessorTypeId = VgoChunkTypeID.RAPJ;
                    }
                }
                else
                {
                    byte[]? cryptKey = null;

                    if (cryptAlgorithms == VgoCryptographyAlgorithms.AES)
                    {
                        var aesCrypter = new AesCrypter();

                        cryptKey = aesCrypter.GenerateRandomKey(256);
                    }
                    else if (cryptAlgorithms == VgoCryptographyAlgorithms.Base64)
                    {
                        //
                    }

                    if (isBson)
                    {
                        vgoExportSetting.ResourceAccessorTypeId = VgoChunkTypeID.RACB;
                        vgoExportSetting.ResourceAccessorCryptTypeId = VgoChunkTypeID.CRAB;
                        vgoExportSetting.ResourceAccessorCryptAlgorithm = cryptAlgorithms;
                        vgoExportSetting.ResourceAccessorCryptKey = cryptKey;
                    }
                    else
                    {
                        vgoExportSetting.ResourceAccessorTypeId = VgoChunkTypeID.RACJ;
                        vgoExportSetting.ResourceAccessorCryptTypeId = VgoChunkTypeID.CRAJ;
                        vgoExportSetting.ResourceAccessorCryptAlgorithm = cryptAlgorithms;
                        vgoExportSetting.ResourceAccessorCryptKey = cryptKey;
                    }
                }

                vgoExportSetting.ResourceTypeId = VgoChunkTypeID.REPb;

                var fileInfo = new FileInfo(vgoFilePath);

                string binFileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length) + ".bin";

                vgoStorage.ExportVgoFile(vgoFilePath, vgoExportSetting);

                stopwatch.Stop();

                Debug.LogFormat("Export VGO file.\nGameObject: {0}, output: {1}, {2:#,0}ms", gameObject.name, vgoFilePath, stopwatch.ElapsedMilliseconds);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Perform verification.
        /// </summary>
        /// <param name="gameObject">The target.</param>
        /// <param name="errorMessage"></param>
        /// <returns>Returns true if validation is successful, false otherwise.</returns>
        private static bool Validate(in GameObject gameObject, out string errorMessage)
        {
            //if (gameObject.TryGetComponent<VgoGenerator>(out _) == false)
            //{
            //    errorMessage = "Please attach a Vgo Generator component to root GameObject.";
            //    return false;
            //}

            if (gameObject.transform.position != Vector3.zero)
            {
                errorMessage = "Please set Transform Position(0, 0, 0).";
                return false;
            }

            if (gameObject.transform.rotation != Quaternion.identity)
            {
                errorMessage = "Please set Transform Rotation(0, 0, 0).";
                return false;
            }

            if (gameObject.transform.localScale != Vector3.one)
            {
                errorMessage = "Please set Transform Scale(1, 1, 1).";
                return false;
            }

            if (gameObject.TryGetComponent<UniVgo2.VgoRight>(out _) == false)
            {
                Debug.LogWarning("Vgo Right component is not attached to root GameObject.");
                //errorMessage = "Please attach a Vgo Right component to root GameObject.";
                //return false;
            }

            if (gameObject.TryGetComponent<Rigidbody>(out _))
            {
                errorMessage = "The root GameObject is not allowed to attach the Rigidbody component.";
                return false;
            }

            if (gameObject.GetComponents<Collider>().Any())
            {
                errorMessage = "The root GameObject is not allowed to attach the Collider component.";
                return false;
            }

            Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                Type collierType = collider.GetType();

                if ((collierType == _BoxColliderType) ||
                    (collierType == _CapsuleColliderType) ||
                    (collierType == _SphereColliderType))
                {
                    continue;
                }

                errorMessage = $"Not supported collier is found. GameObject: {collider.gameObject.name}";

                return false;
            }

            errorMessage = string.Empty;

            return true;
        }
    }
}