// ----------------------------------------------------------------------
// @Namespace : NewtonVgo
// @Class     : ThrowHelper
// ----------------------------------------------------------------------
#nullable enable
namespace NewtonVgo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
#if NET_STANDARD_2_1
    using System.Diagnostics.CodeAnalysis;
#endif
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Http;
#if NET_STANDARD_2_1
    using System.Runtime;
#endif
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Authentication;
    using System.Security.Cryptography;
    using System.Threading;

    /// <summary>
    /// Throw Helper
    /// </summary>
    /// <remarks>
    /// .NET Standard 2.0 - Unity 2020.3 ~ 2021.1
    /// .NET Standard 2.1 - Unity 2021.2 ~ 2023.1
    /// </remarks>
    public static class ThrowHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="AccessViolationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAccessViolationException()
        {
            throw new AccessViolationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="AggregateException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAggregateException()
        {
            throw new AggregateException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowApplicationException()
        {
            throw new ApplicationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentException()
        {
            throw new ArgumentException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentException(in string parameterName)
        {
            throw new ArgumentException(message: string.Empty, parameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentException(in string parameterName, in string message)
        {
            throw new ArgumentException(message, parameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentNullException()
        {
            throw new ArgumentNullException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentNullException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentNullException(in string parameterName)
        {
            throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterName"></param>
        /// <param name="parameter"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowExceptionIfArgumentIsNull<T>(in string parameterName, in T parameter) where T : class?
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException()
        {
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName, in string message)
        {
            throw new ArgumentOutOfRangeException(parameterName, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName, in object parameterValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName, in object parameterValue, in string message)
        {
            throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <remarks>
        /// [closed, closed]  min <= x <= max
        /// </remarks>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName, in int parameterValue, in int? min = null, in int? max = null)
        {
            if (min.HasValue && max.HasValue)
            {
                throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: $"{min} <= x <= {max}");
            }
            else if (min.HasValue)
            {
                throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: $"{min} <= x");
            }
            else if (max.HasValue)
            {
                throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: $"x <= {max}");
            }
            else
            {
                throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: string.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <remarks>
        /// [closed, closed]  min <= x <= max
        /// </remarks>
        public static void ThrowExceptionIfArgumentIsOutOfRange(in string parameterName, in int parameterValue, in int? min = null, in int? max = null)
        {
            if (min.HasValue && max.HasValue)
            {
                if ((parameterValue < min) || (parameterValue >= max))
                {
                    throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: $"{min} <= x <= {max}");
                }
            }
            else if (min.HasValue)
            {
                if (parameterValue < min)
                {
                    throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: $"{min} <= x");
                }
            }
            else if (max.HasValue)
            {
                if (parameterValue > max)
                {
                    throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: $"x <= {max}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArrayTypeMismatchException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArrayTypeMismatchException()
        {
            throw new ArrayTypeMismatchException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="AuthenticationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAuthenticationException()
        {
            throw new AuthenticationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="AuthenticationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAuthenticationException(in string message)
        {
            throw new AuthenticationException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="BadImageFormatException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowBadImageFormatException()
        {
            throw new BadImageFormatException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="BadImageFormatException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowBadImageFormatException(in string fileName)
        {
            throw new BadImageFormatException(message: string.Empty, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        /// <exception cref="BadImageFormatException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowBadImageFormatException(in string fileName, in string message)
        {
            throw new BadImageFormatException(message, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CryptographicException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicException()
        {
            throw new CryptographicException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="CryptographicException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicException(in string message)
        {
            throw new CryptographicException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CryptographicUnexpectedOperationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicUnexpectedOperationException()
        {

            throw new CryptographicUnexpectedOperationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="CryptographicUnexpectedOperationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicUnexpectedOperationException(in string message)
        {

            throw new CryptographicUnexpectedOperationException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CultureNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException()
        {
            throw new CultureNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <exception cref="CultureNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName)
        {
            throw new CultureNotFoundException(parameterName, message: string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="invalidCultureId"></param>
        /// <exception cref="CultureNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName, in int invalidCultureId)
        {
            throw new CultureNotFoundException(parameterName, invalidCultureId, message: string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <exception cref="CultureNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName, in string message)
        {
            throw new CultureNotFoundException(parameterName, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="invalidCultureId"></param>
        /// <param name="message"></param>
        /// <exception cref="CultureNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName, in int invalidCultureId, in string message)
        {
            throw new CultureNotFoundException(parameterName, invalidCultureId, message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DataException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDataException()
        {
            throw new DataException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="DataException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDataException(in string s)
        {
            throw new DataException(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DirectoryNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDirectoryNotFoundException()
        {
            throw new DirectoryNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="DirectoryNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDirectoryNotFoundException(in string message)
        {
            throw new DirectoryNotFoundException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DivideByZeroException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDivideByZeroException()
        {
            throw new DivideByZeroException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DllNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDllNotFoundException()
        {
            throw new DllNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="DuplicateWaitObjectException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDuplicateWaitObjectException()
        {
            throw new DuplicateWaitObjectException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <exception cref="DuplicateWaitObjectException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDuplicateWaitObjectException(in string parameterName)
        {
            throw new DuplicateWaitObjectException(parameterName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="EndOfStreamException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowEndOfStreamException()
        {
            throw new EndOfStreamException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="EntryPointNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowEntryPointNotFoundException()
        {
            throw new EntryPointNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowException()
        {
            throw new Exception();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="Exception"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowException(in string message)
        {
            throw new Exception(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="FileLoadException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileLoadException()
        {
            throw new FileLoadException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="FileLoadException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileLoadException(in string fileName)
        {
            throw new FileLoadException(message: string.Empty, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        /// <exception cref="FileLoadException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileLoadException(in string fileName, in string message)
        {
            throw new FileLoadException(message, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileNotFoundException()
        {
            throw new FileNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="FileNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileNotFoundException(in string fileName)
        {
            throw new FileNotFoundException(message: string.Empty, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        /// <exception cref="FileNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileNotFoundException(in string fileName, in string message)
        {
            throw new FileNotFoundException(message, fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="FormatException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFormatException()
        {
            throw new FormatException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="FormatException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFormatException(in string message)
        {
            throw new FormatException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="HttpRequestException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowHttpRequestException()
        {
            throw new HttpRequestException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="HttpRequestException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowHttpRequestException(in string message)
        {
            throw new HttpRequestException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIndexOutOfRangeException()
        {
            throw new IndexOutOfRangeException();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="message"></param>
        ///// <exception cref="IndexOutOfRangeException"></exception>
        //[DoesNotReturn]
        //public static void ThrowIndexOutOfRangeException(in string message)
        //{
        //    throw new IndexOutOfRangeException(message);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIndexOutOfRangeException(in string parameterName)
        {
            throw new IndexOutOfRangeException($"{parameterName} is out of range.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <remarks>
        /// [closed, open)  min <= x < max
        /// </remarks>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIndexOutOfRangeException(in string parameterName, in int parameterValue, in int? min = null, in int? max = null)
        {
            if (min.HasValue && max.HasValue)
            {
                throw new IndexOutOfRangeException($"{parameterName} ({parameterValue}) is out of range. {min} <= x < {max}");
            }
            else if (min.HasValue)
            {
                throw new IndexOutOfRangeException($"{parameterName} ({parameterValue}) is out of range. {min} <= x");
            }
            else if (max.HasValue)
            {
                throw new IndexOutOfRangeException($"{parameterName} ({parameterValue}) is out of range. x < {max}");
            }
            else
            {
                throw new IndexOutOfRangeException($"{parameterName} ({parameterValue}) is out of range.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <remarks>
        /// [closed, open)  min <= x < max
        /// </remarks>
        public static void ThrowExceptionIfIndexIsOutOfRange(in string parameterName, in int parameterValue, in int? min = null, in int? max = null)
        {
            if (min.HasValue && max.HasValue)
            {
                if ((parameterValue < min) || (parameterValue >= max))
                {
                    throw new IndexOutOfRangeException($"{parameterName} ({parameterValue}) is out of range. {min} <= x < {max}");
                }
            }
            else if (min.HasValue)
            {
                if (parameterValue < min)
                {
                    throw new IndexOutOfRangeException($"{parameterName} ({parameterValue}) is out of range. {min} <= x");
                }
            }
            else if (max.HasValue)
            {
                if (parameterValue >= max)
                {
                    throw new IndexOutOfRangeException($"{parameterName} ({parameterValue}) is out of range. x < {max}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InternalBufferOverflowException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInternalBufferOverflowException()
        {
            throw new InternalBufferOverflowException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidCastException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException()
        {
            throw new InvalidCastException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <exception cref="InvalidCastException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException(in int errorCode)
        {
            throw new InvalidCastException(message: string.Empty, errorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidCastException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException(in string message)
        {
            throw new InvalidCastException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <exception cref="InvalidCastException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException(in int errorCode, in string message)
        {
            throw new InvalidCastException(message, errorCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidDataException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidDataException()
        {
            throw new InvalidDataException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidDataException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidDataException(in string message)
        {
            throw new InvalidDataException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidEnumArgumentException()
        {
            throw new InvalidEnumArgumentException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidEnumArgumentException(in string message)
        {
            throw new InvalidEnumArgumentException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidExpressionException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidExpressionException()
        {

            throw new InvalidExpressionException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="InvalidExpressionException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidExpressionException(in string s)
        {

            throw new InvalidExpressionException(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidOperationException()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidOperationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidOperationException(in string message)
        {
            throw new InvalidOperationException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidProgramException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidProgramException()
        {
            throw new InvalidProgramException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidProgramException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidProgramException(in string message)
        {
            throw new InvalidProgramException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidTimeZoneException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidTimeZoneException()
        {
            throw new InvalidTimeZoneException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="InvalidTimeZoneException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidTimeZoneException(in string message)
        {
            throw new InvalidTimeZoneException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="IOException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException()
        {
            throw new IOException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hresult"></param>
        /// <exception cref="IOException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException(in int hresult)
        {
            throw new IOException(message: string.Empty, hresult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="IOException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException(in string message)
        {
            throw new IOException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hresult"></param>
        /// <param name="message"></param>
        /// <exception cref="IOException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException(in int hresult, in string message)
        {
            throw new IOException(message, hresult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="KeyNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowKeyNotFoundException()
        {
            throw new KeyNotFoundException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="KeyNotFoundException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowKeyNotFoundException(in string message)
        {
            throw new KeyNotFoundException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotFiniteNumberException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException()
        {
            throw new NotFiniteNumberException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offendingNumber"></param>
        /// <exception cref="NotFiniteNumberException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException(in double offendingNumber)
        {
            throw new NotFiniteNumberException(message: string.Empty, offendingNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotFiniteNumberException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException(in string message)
        {
            throw new NotFiniteNumberException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offendingNumber"></param>
        /// <param name="message"></param>
        /// <exception cref="NotFiniteNumberException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException(in double offendingNumber, in string message)
        {
            throw new NotFiniteNumberException(message, offendingNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotImplementedException()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotImplementedException(in string message)
        {
            throw new NotImplementedException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotSupportedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotSupportedException(in string message)
        {
            throw new NotSupportedException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNullReferenceException()
        {
            throw new NullReferenceException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NullReferenceException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNullReferenceException(in string message)
        {
            throw new NullReferenceException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectName"></param>
        /// <exception cref="ObjectDisposedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowObjectDisposedException(string objectName)
        {
            throw new ObjectDisposedException(objectName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ObjectDisposedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowObjectDisposedException(object obj)
        {
            throw new ObjectDisposedException(obj.GetType().FullName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <exception cref="ObjectDisposedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowObjectDisposedException(Type objectType)
        {
            throw new ObjectDisposedException(objectType.FullName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="OperationCanceledException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOperationCanceledException(CancellationToken token)
        {
            throw new OperationCanceledException(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="message"></param>
        /// <exception cref="OperationCanceledException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOperationCanceledException(CancellationToken token, in string message)
        {
            throw new OperationCanceledException(message, token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="OutOfMemoryException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOutOfMemoryException()
        {
            throw new OutOfMemoryException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="OutOfMemoryException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOutOfMemoryException(in string message)
        {
            throw new OutOfMemoryException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="OverflowException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOverflowException()
        {
            throw new OverflowException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="OverflowException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOverflowException(in string message)
        {
            throw new OverflowException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="PathTooLongException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPathTooLongException()
        {
            throw new PathTooLongException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="PathTooLongException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPathTooLongException(in string message)
        {
            throw new PathTooLongException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="PlatformNotSupportedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPlatformNotSupportedException()
        {
            throw new PlatformNotSupportedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="PlatformNotSupportedException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPlatformNotSupportedException(in string message)
        {
            throw new PlatformNotSupportedException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="RankException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowRankException()
        {
            throw new RankException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="RankException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowRankException(in string message)
        {
            throw new RankException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ReadOnlyException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowReadOnlyException()
        {
            throw new ReadOnlyException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <exception cref="ReadOnlyException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowReadOnlyException(in string s)
        {
            throw new ReadOnlyException(s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="SecurityException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSecurityException()
        {
            throw new SecurityException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="SecurityException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSecurityException(in string message)
        {
            throw new SecurityException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="SerializationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSerializationException()
        {
            throw new SerializationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="SerializationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSerializationException(in string message)
        {
            throw new SerializationException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="StackOverflowException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowStackOverflowException()
        {
            throw new StackOverflowException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="StackOverflowException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowStackOverflowException(in string message)
        {
            throw new StackOverflowException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="SystemException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSystemException()
        {
            throw new SystemException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="SystemException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSystemException(in string message)
        {
            throw new SystemException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="TimeoutException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowTimeoutException()
        {
            throw new TimeoutException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="TimeoutException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowTimeoutException(in string message)
        {
            throw new TimeoutException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UnauthorizedAccessException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUnauthorizedAccessException()
        {
            throw new UnauthorizedAccessException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="UnauthorizedAccessException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUnauthorizedAccessException(in string message)
        {
            throw new UnauthorizedAccessException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UriFormatException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUriFormatException()
        {
            throw new UriFormatException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textString"></param>
        /// <exception cref="UriFormatException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUriFormatException(in string textString)
        {
            throw new UriFormatException(textString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="VerificationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowVerificationException()
        {
            throw new VerificationException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="VerificationException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowVerificationException(in string message)
        {
            throw new VerificationException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="WarningException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWarningException()
        {
            throw new WarningException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="WarningException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWarningException(in string message)
        {
            throw new WarningException(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="WebException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWebException()
        {
            throw new WebException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="WebException"></exception>
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWebException(in string message)
        {
            throw new WebException(message);
        }
    }
}