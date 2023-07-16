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
#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAccessViolationException()
        {
            throw new AccessViolationException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAggregateException()
        {
            throw new AggregateException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowApplicationException()
        {
            throw new ApplicationException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentException()
        {
            throw new ArgumentException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentException(in string parameterName)
        {
            throw new ArgumentException(message: string.Empty, parameterName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentException(in string parameterName, in string message)
        {
            throw new ArgumentException(message, parameterName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentNullException()
        {
            throw new ArgumentNullException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentNullException(in string parameterName)
        {
            throw new ArgumentNullException(parameterName);
        }

        public static void ThrowExceptionIfArgumentIsNull<T>(in string parameterName, in T parameter) where T : class?
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException()
        {
            throw new ArgumentOutOfRangeException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName, in string message)
        {
            throw new ArgumentOutOfRangeException(parameterName, message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArgumentOutOfRangeException(in string parameterName, in object parameterValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, actualValue: parameterValue, message: string.Empty);
        }

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

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowArrayTypeMismatchException()
        {
            throw new ArrayTypeMismatchException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAuthenticationException()
        {
            throw new AuthenticationException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowAuthenticationException(in string message)
        {
            throw new AuthenticationException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowBadImageFormatException()
        {
            throw new BadImageFormatException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowBadImageFormatException(in string fileName)
        {
            throw new BadImageFormatException(message: string.Empty, fileName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowBadImageFormatException(in string fileName, in string message)
        {
            throw new BadImageFormatException(message, fileName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicException()
        {
            throw new CryptographicException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicException(in string message)
        {
            throw new CryptographicException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicUnexpectedOperationException()
        {

            throw new CryptographicUnexpectedOperationException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCryptographicUnexpectedOperationException(in string message)
        {

            throw new CryptographicUnexpectedOperationException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException()
        {
            throw new CultureNotFoundException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName)
        {
            throw new CultureNotFoundException(parameterName, message: string.Empty);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName, in int invalidCultureId)
        {
            throw new CultureNotFoundException(parameterName, invalidCultureId, message: string.Empty);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName, in string message)
        {
            throw new CultureNotFoundException(parameterName, message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowCultureNotFoundException(in string parameterName, in int invalidCultureId, in string message)
        {
            throw new CultureNotFoundException(parameterName, invalidCultureId, message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDataException()
        {
            throw new DataException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDataException(in string s)
        {
            throw new DataException(s);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDirectoryNotFoundException()
        {
            throw new DirectoryNotFoundException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDirectoryNotFoundException(in string message)
        {
            throw new DirectoryNotFoundException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDivideByZeroException()
        {
            throw new DivideByZeroException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDllNotFoundException()
        {
            throw new DllNotFoundException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDuplicateWaitObjectException()
        {
            throw new DuplicateWaitObjectException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowDuplicateWaitObjectException(in string parameterName)
        {
            throw new DuplicateWaitObjectException(parameterName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowEndOfStreamException()
        {
            throw new EndOfStreamException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowEntryPointNotFoundException()
        {
            throw new EntryPointNotFoundException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowException()
        {
            throw new Exception();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowException(in string message)
        {
            throw new Exception(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileLoadException()
        {
            throw new FileLoadException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileLoadException(in string fileName)
        {
            throw new FileLoadException(message: string.Empty, fileName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileLoadException(in string fileName, in string message)
        {
            throw new FileLoadException(message, fileName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileNotFoundException()
        {
            throw new FileNotFoundException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileNotFoundException(in string fileName)
        {
            throw new FileNotFoundException(message: string.Empty, fileName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFileNotFoundException(in string fileName, in string message)
        {
            throw new FileNotFoundException(message, fileName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFormatException()
        {
            throw new FormatException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowFormatException(in string message)
        {
            throw new FormatException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowHttpRequestException()
        {
            throw new HttpRequestException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowHttpRequestException(in string message)
        {
            throw new HttpRequestException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIndexOutOfRangeException()
        {
            throw new IndexOutOfRangeException();
        }

        //[DoesNotReturn]
        //public static void ThrowIndexOutOfRangeException(in string message)
        //{
        //    throw new IndexOutOfRangeException(message);
        //}

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

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInternalBufferOverflowException()
        {
            throw new InternalBufferOverflowException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException()
        {
            throw new InvalidCastException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException(in int errorCode)
        {
            throw new InvalidCastException(message: string.Empty, errorCode);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException(in string message)
        {
            throw new InvalidCastException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidCastException(in int errorCode, in string message)
        {
            throw new InvalidCastException(message, errorCode);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidDataException()
        {
            throw new InvalidDataException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidDataException(in string message)
        {
            throw new InvalidDataException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidEnumArgumentException()
        {
            throw new InvalidEnumArgumentException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidEnumArgumentException(in string message)
        {
            throw new InvalidEnumArgumentException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidExpressionException()
        {

            throw new InvalidExpressionException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidExpressionException(in string s)
        {

            throw new InvalidExpressionException(s);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidOperationException()
        {
            throw new InvalidOperationException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidOperationException(in string message)
        {
            throw new InvalidOperationException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidProgramException()
        {
            throw new InvalidProgramException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidProgramException(in string message)
        {
            throw new InvalidProgramException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidTimeZoneException()
        {
            throw new InvalidTimeZoneException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowInvalidTimeZoneException(in string message)
        {
            throw new InvalidTimeZoneException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException()
        {
            throw new IOException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException(in int hresult)
        {
            throw new IOException(message: string.Empty, hresult);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException(in string message)
        {
            throw new IOException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowIOException(in int hresult, in string message)
        {
            throw new IOException(message, hresult);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowKeyNotFoundException()
        {
            throw new KeyNotFoundException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowKeyNotFoundException(in string message)
        {
            throw new KeyNotFoundException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException()
        {
            throw new NotFiniteNumberException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException(in double offendingNumber)
        {
            throw new NotFiniteNumberException(message: string.Empty, offendingNumber);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException(in string message)
        {
            throw new NotFiniteNumberException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotFiniteNumberException(in double offendingNumber, in string message)
        {
            throw new NotFiniteNumberException(message, offendingNumber);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotImplementedException()
        {
            throw new NotImplementedException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotImplementedException(in string message)
        {
            throw new NotImplementedException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNotSupportedException(in string message)
        {
            throw new NotSupportedException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNullReferenceException()
        {
            throw new NullReferenceException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowNullReferenceException(in string message)
        {
            throw new NullReferenceException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowObjectDisposedException(string objectName)
        {
            throw new ObjectDisposedException(objectName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowObjectDisposedException(object obj)
        {
            throw new ObjectDisposedException(obj.GetType().FullName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowObjectDisposedException(Type objectType)
        {
            throw new ObjectDisposedException(objectType.FullName);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOperationCanceledException(CancellationToken token)
        {
            throw new OperationCanceledException(token);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOperationCanceledException(CancellationToken token, in string message)
        {
            throw new OperationCanceledException(message, token);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOutOfMemoryException()
        {
            throw new OutOfMemoryException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOutOfMemoryException(in string message)
        {
            throw new OutOfMemoryException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOverflowException()
        {
            throw new OverflowException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowOverflowException(in string message)
        {
            throw new OverflowException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPathTooLongException()
        {
            throw new PathTooLongException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPathTooLongException(in string message)
        {
            throw new PathTooLongException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPlatformNotSupportedException()
        {
            throw new PlatformNotSupportedException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowPlatformNotSupportedException(in string message)
        {
            throw new PlatformNotSupportedException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowRankException()
        {
            throw new RankException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowRankException(in string message)
        {
            throw new RankException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowReadOnlyException()
        {
            throw new ReadOnlyException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowReadOnlyException(in string s)
        {
            throw new ReadOnlyException(s);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSecurityException()
        {
            throw new SecurityException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSecurityException(in string message)
        {
            throw new SecurityException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSerializationException()
        {
            throw new SerializationException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSerializationException(in string message)
        {
            throw new SerializationException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowStackOverflowException()
        {
            throw new StackOverflowException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowStackOverflowException(in string message)
        {
            throw new StackOverflowException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSystemException()
        {
            throw new SystemException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowSystemException(in string message)
        {
            throw new SystemException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowTimeoutException()
        {
            throw new TimeoutException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowTimeoutException(in string message)
        {
            throw new TimeoutException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUnauthorizedAccessException()
        {
            throw new UnauthorizedAccessException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUnauthorizedAccessException(in string message)
        {
            throw new UnauthorizedAccessException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUriFormatException()
        {
            throw new UriFormatException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowUriFormatException(in string textString)
        {
            throw new UriFormatException(textString);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowVerificationException()
        {
            throw new VerificationException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowVerificationException(in string message)
        {
            throw new VerificationException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWarningException()
        {
            throw new WarningException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWarningException(in string message)
        {
            throw new WarningException(message);
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWebException()
        {
            throw new WebException();
        }

#if NET_STANDARD_2_1
        [DoesNotReturn]
#endif
        public static void ThrowWebException(in string message)
        {
            throw new WebException(message);
        }
    }
}