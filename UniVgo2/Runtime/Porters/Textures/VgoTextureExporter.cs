// ----------------------------------------------------------------------
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

            Texture srcTexture = texture;

            if (vgoStorage.Layout.textures == null)
            {
                vgoStorage.Layout.textures = new List<VgoTexture?>();
            }

            int srcTextureInstanceId = srcTexture.GetInstanceID();

            VgoTexture? vgoTexture = vgoStorage.Layout.textures
                .FirstOrDefault(x => x?.id == srcTextureInstanceId);

            if (vgoTexture != null)
            {
                return vgoStorage.Layout.textures.IndexOf(vgoTexture);
            }

            float metallicSmoothness = (metallicRoughness == -1.0f) ? -1.0f : (1.0f - metallicRoughness);

            Texture convertedTexture = _TextureConverter.GetExportTexture(srcTexture, textureMapType, colorSpaceType, metallicSmoothness);

            int width = convertedTexture.width;

            int height = convertedTexture.height;

            string mimeType;
            byte[] imageBytes;
            byte[] textureData;
            //int depth = 0;

            if (convertedTexture is Texture2D convertedTexture2d)
            {
                textureData = convertedTexture2d.GetRawTextureData();
            }
            else if (convertedTexture is Texture2DArray convertedTexture2dArray)
            {
                //depth = convertedTexture2dArray.depth;

                // @notice
                var elementDataList = new List<byte[]>(convertedTexture2dArray.depth);

                for (int elementIndex = 0; elementIndex < convertedTexture2dArray.depth; elementIndex++)
                {
                    byte[] elementData = convertedTexture2dArray.GetPixelData<byte>(mipLevel: 0, elementIndex).ToArray();

                    elementDataList.Add(elementData);
                }

                textureData = new byte[elementDataList.Sum(e => e.Length)];

                int offset = 0;

                foreach (byte[] elementData in elementDataList)
                {
                    Buffer.BlockCopy(elementData, 0, textureData, offset, elementData.Length);

                    offset += elementData.Length;
                }
            }
            else if (convertedTexture is Texture3D convertedTexture3d)
            {
                //depth = convertedTexture3d.depth;

                // @notice
                textureData = convertedTexture3d.GetPixelData<byte>(mipLevel: 0).ToArray();
            }
            else
            {
                return -1;
            }

            if (TextureType == ImageType.WebP)
            {
                mimeType = MimeType.Image_WebP;

                byte[] webpBytes;

                try
                {
                    byte[]? pngBytes = ImageConversion.EncodeArrayToPNG(textureData, convertedTexture.graphicsFormat, (uint)width, (uint)height);

                    if (pngBytes == null)
                    {
                        ThrowHelper.ThrowBadImageFormatException(srcTexture.name);

                        return -1;
                    }

                    // @heavy
                    webpBytes = ImageConverter.ConvertToWebp(pngBytes, ImageType.PNG, srcTexture.name, flipVertical: false);
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

                byte[]? jpgBytes = ImageConversion.EncodeArrayToJPG(textureData, convertedTexture.graphicsFormat, (uint)width, (uint)height, quality: 100);

                if (jpgBytes == null)
                {
                    return -1;
                }

                imageBytes = jpgBytes;
            }
            else
            {
                mimeType = MimeType.Image_Png;

                byte[]? pngBytes = ImageConversion.EncodeArrayToPNG(textureData, convertedTexture.graphicsFormat, (uint)width, (uint)height);

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
                name = convertedTexture.name,
                source = accessorIndex,
                //width = convertedTexture.width,
                //height = convertedTexture.height,
                //depth = depth,
                dimensionType = (TextureDimension)srcTexture.dimension,
                mapType = textureMapType,
                colorSpace = colorSpaceType,
                mimeType = mimeType,
                filterMode = (NewtonVgo.FilterMode)srcTexture.filterMode,
                wrapMode = (NewtonVgo.TextureWrapMode)srcTexture.wrapMode,
                wrapModeU = (NewtonVgo.TextureWrapMode)srcTexture.wrapModeU,
                wrapModeV = (NewtonVgo.TextureWrapMode)srcTexture.wrapModeV,
                metallicRoughness = metallicRoughness,
            };

            vgoStorage.Layout.textures.Add(vgoTexture);

            int textureIndex = vgoStorage.Layout.textures.Count - 1;

            return textureIndex;
        }

        #endregion
    }
}
