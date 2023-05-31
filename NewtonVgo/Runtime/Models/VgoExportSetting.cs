// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : VgoExportSetting
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// VGO Export Setting
    /// </summary>
    public class VgoExportSetting
    {
        #region Fields

        /// <summary>The asset info type id.</summary>
        private VgoChunkTypeID _AssetInfoTypeId = VgoChunkTypeID.AIPJ;

        /// <summary>The layout type id.</summary>
        private VgoChunkTypeID _LayoutTypeId = VgoChunkTypeID.LAPJ;

        /// <summary>The resource accessor type id.</summary>
        private VgoChunkTypeID _ResourceAccessorTypeId = VgoChunkTypeID.RAPJ;

        /// <summary>The resource accessor crypt type id.</summary>
        private VgoChunkTypeID _ResourceAccessorCryptTypeId = VgoChunkTypeID.None;

        /// <summary>The resource accessor crypt algorithm.</summary>
        private string? _ResourceAccessorCryptAlgorithm = null;

        /// <summary>The resource accessor crypt key.</summary>
        private byte[]? _ResourceAccessorCryptKey = null;

        /// <summary>The resource type id.</summary>
        private VgoChunkTypeID _ResourceTypeId = VgoChunkTypeID.REPb;

        /// <summary>The resource URI.</summary>
        private string? _ResourceUri = null;

        /// <summary>The resource binary file name.</summary>
        private string? _BinFileName = null;

        #endregion

        #region Properties

        /// <summary>The asset info type id.</summary>
        public VgoChunkTypeID AssetInfoTypeId { get => _AssetInfoTypeId; set => _AssetInfoTypeId = value; }

        /// <summary>The layout type id.</summary>
        public VgoChunkTypeID LayoutTypeId { get => _LayoutTypeId; set => _LayoutTypeId = value; }

        /// <summary>The resource accessor type id.</summary>
        public VgoChunkTypeID ResourceAccessorTypeId { get => _ResourceAccessorTypeId; set => _ResourceAccessorTypeId = value; }

        /// <summary>The resource accessor crypt type id.</summary>
        public VgoChunkTypeID ResourceAccessorCryptTypeId { get => _ResourceAccessorCryptTypeId; set => _ResourceAccessorCryptTypeId = value; }

        /// <summary>The resource accessor crypt algorithm.</summary>
        public string? ResourceAccessorCryptAlgorithm { get => _ResourceAccessorCryptAlgorithm; set => _ResourceAccessorCryptAlgorithm = value; }

        /// <summary>The resource accessor crypt key.</summary>
        public byte[]? ResourceAccessorCryptKey { get => _ResourceAccessorCryptKey; set => _ResourceAccessorCryptKey = value; }

        /// <summary>The resource type id.</summary>
        public VgoChunkTypeID ResourceTypeId { get => _ResourceTypeId; set => _ResourceTypeId = value; }

        /// <summary>The resource URI.</summary>
        public string? ResourceUri { get => _ResourceUri; set => _ResourceUri = value; }

        /// <summary>The resource binary file name.</summary>
        public string? BinFileName { get => _BinFileName; set => _BinFileName = value; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validate export setting.
        /// </summary>
        /// <param name="errorMessages">List of validation errors.</param>
        /// <returns>Returns true if validation is successful, false otherwise.</returns>
        public bool Validate(out IReadOnlyList<string> errorMessages)
        {
            errorMessages = new List<string>(0);

            bool isValid = true;

            switch (_AssetInfoTypeId)
            {
                case VgoChunkTypeID.AIPJ:
                case VgoChunkTypeID.AIPB:
                    break;
                default:
                    errorMessages.Append($"{nameof(AssetInfoTypeId)}: {_AssetInfoTypeId}");
                    isValid = false;
                    break;
            }

            switch (_LayoutTypeId)
            {
                case VgoChunkTypeID.LAPJ:
                case VgoChunkTypeID.LAPB:
                    break;
                default:
                    errorMessages.Append($"{nameof(LayoutTypeId)}: {_LayoutTypeId}");
                    isValid = false;
                    break;
            }

            switch (_ResourceAccessorTypeId)
            {
                case VgoChunkTypeID.RAPJ:
                case VgoChunkTypeID.RAPB:
                    switch (_ResourceAccessorCryptTypeId)
                    {
                        case VgoChunkTypeID.None:
                            break;
                        case VgoChunkTypeID.CRAJ:
                        case VgoChunkTypeID.CRAB:
                        default:
                            errorMessages.Append($"{nameof(ResourceAccessorTypeId)}: {_ResourceAccessorTypeId}, {nameof(ResourceAccessorCryptTypeId)}: {_ResourceAccessorCryptTypeId}");
                            isValid = false;
                            break;
                    }
                    break;
                case VgoChunkTypeID.RACJ:
                case VgoChunkTypeID.RACB:
                    switch (_ResourceAccessorCryptTypeId)
                    {
                        case VgoChunkTypeID.CRAJ:
                        case VgoChunkTypeID.CRAB:
                            break;
                        case VgoChunkTypeID.None:
                        default:
                            errorMessages.Append($"{nameof(ResourceAccessorTypeId)}: {_ResourceAccessorTypeId}, {nameof(ResourceAccessorCryptTypeId)}: {_ResourceAccessorCryptTypeId}");
                            isValid = false;
                            break;
                    }
                    break;
                default:
                    errorMessages.Append($"{nameof(ResourceAccessorTypeId)}: {_ResourceAccessorTypeId}");
                    isValid = false;
                    break;
            }

            switch (_ResourceAccessorCryptTypeId)
            {
                case VgoChunkTypeID.None:
                    if (string.IsNullOrEmpty(_ResourceAccessorCryptAlgorithm) == false)
                    {
                        errorMessages.Append($"{nameof(ResourceAccessorCryptTypeId)}: {_ResourceAccessorCryptTypeId}, {nameof(ResourceAccessorCryptAlgorithm)}: {_ResourceAccessorCryptAlgorithm}");
                        isValid = false;
                    }
                    if (_ResourceAccessorCryptKey != null)
                    {
                        errorMessages.Append($"{nameof(ResourceAccessorCryptTypeId)}: {_ResourceAccessorCryptTypeId}, {nameof(ResourceAccessorCryptKey)}: (...)");
                        isValid = false;
                    }
                    break;
                case VgoChunkTypeID.CRAJ:
                case VgoChunkTypeID.CRAB:
                    if (_ResourceAccessorCryptAlgorithm == VgoCryptographyAlgorithms.AES)
                    {
                        //
                    }
                    else if (_ResourceAccessorCryptAlgorithm == VgoCryptographyAlgorithms.Base64)
                    {
                        if (_ResourceAccessorCryptKey != null)
                        {
                            errorMessages.Append($"{nameof(ResourceAccessorCryptTypeId)}: {_ResourceAccessorCryptTypeId}, {nameof(ResourceAccessorCryptKey)}: {_ResourceAccessorCryptKey}");
                            isValid = false;
                        }
                    }
                    else
                    {
                        errorMessages.Append($"{nameof(ResourceAccessorCryptTypeId)}: {_ResourceAccessorCryptTypeId}, {nameof(ResourceAccessorCryptAlgorithm)}: {_ResourceAccessorCryptAlgorithm}");
                        isValid = false;
                    }
                    break;
                default:
                    break;
            }

            switch (_ResourceTypeId)
            {
                case VgoChunkTypeID.REPb:
                    if (_ResourceUri != null)
                    {
                        errorMessages.Append($"{nameof(ResourceTypeId)}: {_ResourceTypeId}, {nameof(ResourceUri)}: {_ResourceUri}");
                        isValid = false;
                    }
                    if (_BinFileName != null)
                    {
                        errorMessages.Append($"{nameof(ResourceTypeId)}: {_ResourceTypeId}, {nameof(BinFileName)}: {_BinFileName}");
                        isValid = false;
                    }
                    break;
                case VgoChunkTypeID.REPJ:
                case VgoChunkTypeID.REPB:
                    if (_ResourceUri == null)
                    {
                        errorMessages.Append($"{nameof(ResourceTypeId)}: {_ResourceTypeId}, {nameof(ResourceUri)}: {_ResourceUri}");
                        isValid = false;
                    }
                    if (_BinFileName == null)
                    {
                        errorMessages.Append($"{nameof(ResourceTypeId)}: {_ResourceTypeId}, {nameof(BinFileName)}: {_BinFileName}");
                        isValid = false;
                    }
                    break;
                default:
                    errorMessages.Append($"{nameof(ResourceTypeId)}: {_ResourceTypeId}");
                    isValid = false;
                    break;
            }

            return isValid;
        }

        #endregion
    }
}
