// ----------------------------------------------------------------------
// @Namespace : UniVgo.Editor
// @Class     : VgoRightValidator
// ----------------------------------------------------------------------
namespace UniVgo.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UniGLTFforUniVgo;
    using UniVgo;

    /// <summary>
    /// VGO Right Validator
    /// </summary>
    public static class VgoRightValidator
    {
        /// <summary>Validation Rule</summary>
        private struct ValidationRule
        {
            /// <summary>Is Required</summary>
            public readonly bool IsRequired;

            /// <summary>Max Length</summary>
            public readonly int MaxLength;

            /// <summary>
            /// Create a new instance of ValidationRule.
            /// </summary>
            /// <param name="isRequired"></param>
            /// <param name="maxLength"></param>
            public ValidationRule(bool isRequired = false, int maxLength = 255)
            {
                IsRequired = isRequired;
                MaxLength = maxLength;
            }
        }

        /// <summary>Validation Rules</summary>
        private static readonly Dictionary<string, ValidationRule> _ValidationRules =
            new Dictionary<string, ValidationRule>
            {
                { "Title", new ValidationRule(isRequired: true) },
                { "Author", new ValidationRule(isRequired: true) },
                { "Organization", new ValidationRule(isRequired: false) },
                { "CreatedDate", new ValidationRule(isRequired: false) },
                { "UpdatedDate", new ValidationRule(isRequired: false) },
                { "Version", new ValidationRule(isRequired: false) },
                { "DistributionUrl", new ValidationRule(isRequired: false) },
                { "LicenseUrl", new ValidationRule(isRequired: false) },
            };

        /// <summary>
        /// Validates the field.
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="text"></param>
        /// <returns>error massage</returns>
        private static string ValidateField(string fieldName, string text)
        {
            var validationRule = _ValidationRules[fieldName];

            if (validationRule.IsRequired && string.IsNullOrEmpty(text))
            {
                return string.Format("{0}: Please enter a value.", fieldName);
            }

            if (text != null && validationRule.MaxLength < text.Length)
            {
                return string.Format("{0}: Please make the value within {1} characters.", fieldName, validationRule.MaxLength);
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform verification.
        /// </summary>
        /// <param name="vgoRight"></param>
        /// <param name="errorMessage"></param>
        /// <returns>Returns true if validation is successful, false otherwise.</returns>
        public static bool Validate(VgoRight vgoRight, out string errorMessage)
        {
            glTF_VGO_Right right = vgoRight.Right;

            var errorMessages = new[]
            {
                ValidateField("Title", right.title),
                ValidateField("Author", right.author),
                ValidateField("Organization", right.organization),
                ValidateField("CreatedDate", right.createdDate),
                ValidateField("UpdatedDate", right.updatedDate),
                ValidateField("Version", right.version),
                ValidateField("DistributionUrl", right.distributionUrl),
                ValidateField("LicenseUrl", right.licenseUrl),
            };

            if (errorMessages.Any(m => !string.IsNullOrEmpty(m)))
            {
                errorMessage = string.Join(Environment.NewLine, errorMessages.Where(m => !string.IsNullOrEmpty(m)).ToArray());

                return false;
            }
            else
            {
                errorMessage = string.Empty;

                return true;
            }
        }
    }
}