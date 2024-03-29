﻿// ----------------------------------------------------------------------
// @Namespace : UniVgo2.Porters
// @Class     : VgoTextureExporter
// ----------------------------------------------------------------------
#nullable enable
namespace UniVgo2.Porters
{
    using NewtonVgo;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UniVgo2.Converters;

    /// <summary>
    /// VGO Texture Exporter
    /// </summary>
    public partial class VgoTextureExporter : IVgoTextureExporter
    {
        #region Fields

        /// <summary>The texture converter.</summary>
        protected readonly ITextureConverter _TextureConverter;

        /// <summary>The delegate to ExportTexture method.</summary>
        protected readonly ExportTextureDelegate _ExporterTextureDelegate;

        /// <summary>The image converter.</summary>
        protected IImageConverter? _ImageConverter;

        #endregion

        #region Properties

        /// <summary>A delegate of ExportTexture method.</summary>
        public ExportTextureDelegate ExportTextureDelegate => _ExporterTextureDelegate;

        /// <summary>The texture type.</summary>
        public ImageType TextureType { get; set; }

        /// <summary>The image converter.</summary>
        protected IImageConverter ImageConverter => _ImageConverter ??= new ImageConverter();

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of VgoTextureExporter.
        /// </summary>
        public VgoTextureExporter()
        {
            _TextureConverter = new TextureConverter();

            _ExporterTextureDelegate = new ExportTextureDelegate(ExportTexture);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Export a texture to vgo storage.
        /// </summary>
        /// <param name="vgoStorage">A vgo storage.</param>
        /// <param name="texture">A unity texture.</param>
        /// <param name="textureMapType">The map type of texture.</param>
        /// <param name="colorSpaceType">The color space type of image.</param>
        /// <param name="metallicRoughness">The metallic-roughness value.</param>
        /// <returns>The index of layout.texture.</returns>
        protected virtual int ExportTexture(
            IVgoStorage vgoStorage,
            Texture texture,
            in VgoTextureMapType textureMapType = VgoTextureMapType.Default,
            in VgoColorSpaceType colorSpaceType = VgoColorSpaceType.Srgb,
            in float metallicRoughness = -1.0f)
        {
            if (texture == null)
            {
                return -1;
            }

            if (!(texture is Texture2D srcTexture2d))
            {
                return -1;
            }

            if (vgoStorage.Layout.textures == null)
            {
                vgoStorage.Layout.textures = new List<VgoTexture?>();
            }

            int srcTextureInstanceId = srcTexture2d.GetInstanceID();

            VgoTexture? vgoTexture = vgoStorage.Layout.textures
                .FirstOrDefault(x => x?.id == srcTextureInstanceId);

            if (vgoTexture != null)
            {
                return vgoStorage.Layout.textures.IndexOf(vgoTexture);
            }

            float metallicSmoothness = (metallicRoughness == -1.0f) ? -1.0f : (1.0f - metallicRoughness);

            Texture2D convertedTexture2d = _TextureConverter.GetExportTexture(srcTexture2d, textureMapType, colorSpaceType, metallicSmoothness);

            int width = convertedTexture2d.width;

            int height = convertedTexture2d.height;

            string mimeType;
            byte[] imageBytes;

            if (TextureType == ImageType.WebP)
            {
                mimeType = MimeType.Image_WebP;

                byte[] webpBytes;

                try
                {
                    byte[] textureData = convertedTexture2d.GetRawTextureData();

                    byte[]? pngBytes = ImageConversion.EncodeArrayToPNG(textureData, convertedTexture2d.graphicsFormat, (uint)width, (uint)height);

                    if (pngBytes == null)
                    {
                        ThrowHelper.ThrowBadImageFormatException(srcTexture2d.name);

                        return -1;
                    }

                    // @heavy
                    webpBytes = ImageConverter.ConvertToWebp(pngBytes, ImageType.PNG, srcTexture2d.name, flipVertical: false);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);

                    return -1;
                }

                if (webpBytes.Any() == false)
                {
                    return -1;
                }

                imageBytes = webpBytes;
            }
            else if (TextureType == ImageType.JPEG)
            {
                mimeType = MimeType.Image_Jpeg;

                byte[] textureData = convertedTexture2d.GetRawTextureData();

                byte[]? jpgBytes = ImageConversion.EncodeArrayToJPG(textureData, convertedTexture2d.graphicsFormat, (uint)width, (uint)height, quality: 100);

                if (jpgBytes == null)
                {
                    return -1;
                }

                imageBytes = jpgBytes;
            }
            else
            {
                mimeType = MimeType.Image_Png;

                byte[] textureData = convertedTexture2d.GetRawTextureData();

                byte[]? pngBytes = ImageConversion.EncodeArrayToPNG(textureData, convertedTexture2d.graphicsFormat, (uint)width, (uint)height);

                if (pngBytes == null)
                {
                    return -1;
                }

                imageBytes = pngBytes;
            }

            int accessorIndex = vgoStorage.AddAccessorWithoutSparse(imageBytes, VgoResourceAccessorDataType.UnsignedByte, VgoResourceAccessorKind.ImageData);

            vgoTexture = new VgoTexture
            {
                id = srcTextureInstanceId,
                name = convertedTexture2d.name,
                source = accessorIndex,
                dimensionType = (TextureDimension)srcTexture2d.dimension,
                mapType = textureMapType,
                colorSpace = colorSpaceType,
                mimeType = mimeType,
                filterMode = (NewtonVgo.FilterMode)srcTexture2d.filterMode,
                wrapMode = (NewtonVgo.TextureWrapMode)srcTexture2d.wrapMode,
                wrapModeU = (NewtonVgo.TextureWrapMode)srcTexture2d.wrapModeU,
                wrapModeV = (NewtonVgo.TextureWrapMode)srcTexture2d.wrapModeV,
                metallicRoughness = metallicRoughness,
            };

            vgoStorage.Layout.textures.Add(vgoTexture);

            int textureIndex = vgoStorage.Layout.textures.Count - 1;

            return textureIndex;
        }

        #endregion
    }
}
