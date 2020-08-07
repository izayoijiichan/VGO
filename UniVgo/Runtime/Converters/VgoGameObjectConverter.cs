﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo.Converters
// @Class     : VgoGameObjectConverter
// ----------------------------------------------------------------------
namespace UniVgo.Converters
{
    using NewtonGltf;
    using UnityEngine;

    /// <summary>
    /// VGO GameObject Converter
    /// </summary>
    public class VgoGameObjectConverter
    {
        /// <summary>Default tag</summary>
        private const string Untagged = "Untagged";

        /// <summary>
        /// Create glTFNode_VGO_GameObject from GameObject.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static VGO_GameObject CreateFrom(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return null;
            }

            if ((gameObject.activeSelf == true) &&
                (gameObject.isStatic == false) &&
                (string.IsNullOrEmpty(gameObject.tag) || (gameObject.tag == Untagged)) &&
                (gameObject.layer == 0))
            {
                return null;
            }

            return new VGO_GameObject()
            {
                isActive = gameObject.activeSelf,
                isStatic = gameObject.isStatic,
                tag = gameObject.tag,
                layer = gameObject.layer,
            };
        }

        /// <summary>
        /// Set GameObject field value.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="vgoGameObject"></param>
        public static void SetGameObjectValue(GameObject gameObject, VGO_GameObject vgoGameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            if (vgoGameObject == null)
            {
                return;
            }

            gameObject.SetActive(vgoGameObject.isActive);

            gameObject.isStatic = vgoGameObject.isStatic;

            if ((string.IsNullOrEmpty(vgoGameObject.tag) == false) && (vgoGameObject.tag != Untagged))
            {
                try
                {
                    gameObject.tag = vgoGameObject.tag;
                }
                catch
                {
                    // UnityException: Tag: <vgoGameObject.tag> is not defined.
                }
            }

            if ((0 <= vgoGameObject.layer) && (vgoGameObject.layer <= 31))
            {
                try
                {
                    gameObject.layer = vgoGameObject.layer;
                }
                catch
                {
                    //
                }
            }

            gameObject.layer = vgoGameObject.layer;
        }
    }
}