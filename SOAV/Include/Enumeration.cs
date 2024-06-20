using System;

namespace SOAV.Include
{
    /// <summary>
    /// Solution Developer:
    /// SOA Enumeration
    /// </summary>
    public static class Enumeration
    {
        public enum FlagEnum
        {
            MarkRead = 1,
            KeepUnRead = 0,
            IsAttachment = 2
        }
        public enum ResponseEnum
        {
            IntializationError = -9,
            ConfigurationError = -8,
            ExternalCredentialsError = -7,
            FormatError = -6,
            AESConfigurationError = -5,
            NotAllowedURI = -4,
            FileSystemLogFailure = -3,
            UnhandledServing = -2,
            InvalidCredentials = -1,
            Success = 0,
            UnexpectedFailure = 1
        }
		/// <summary>
		/// Solution Developer:
		/// Get Integer Value of Enum Type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumValue">Object</param>
		/// <returns></returns>
		public static int GetEnumValueId<T>(T enumValue) where T : Enum
		{
			return Convert.ToInt32(enumValue);
		}
		/// <summary>
		/// Solution Developer:
		/// Get Enum Object from Integer Input
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id">int</param>
		/// <returns></returns>
		public static T GetEnumValue<T>(int id) where T : Enum
		{
			if (Enum.IsDefined(typeof(T), id))
				return (T)(object)id;
			else
				return (T)(object)-2;
		}
    }
}