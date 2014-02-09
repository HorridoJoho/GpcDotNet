using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Gpc
{
	public static class TextReaderExtensions
	{
		public enum NumericSignMode
		{
			AllowNone,
			AllowBoth,
			AllowMinusOnly,
			AllowPlusOnly,
			RequireOne,
			RequireMinus,
			RequirePlus
		}

		private static readonly Char[][][] _VALID_BOOLEAN_CHARS = new Char[][][] {
            new Char[][] {
                new Char[] {'0', '0'}
            },
            new Char[][] {
                new Char[] {'1', '1'}
            },
            new Char[][] {
                new Char[] { 'f', 'F' },
                new Char[] { 'a', 'A' },
                new Char[] { 'l', 'L' },
                new Char[] { 's', 'S' },
                new Char[] { 'e', 'E' }
            },
            new Char[][] {
                new Char[] { 't', 'T' },
                new Char[] { 'r', 'R' },
                new Char[] { 'u', 'U' },
                new Char[] { 'e', 'E' }
            }
        };

		public static Boolean Find(this TextReader reader, Char ch, Boolean skipFound)
		{
			while (true)
			{
				int intCh = reader.Peek();
				if (intCh == -1)
				{
					return false;
				}

				if (ch == (int)intCh)
				{
					if (skipFound)
					{
						reader.Read();
					}
					return true;
				}

				reader.Read();
			}
		}

		public static Boolean Skip(this TextReader reader, Char ch)
		{
			int intCh = reader.Peek();
			if (intCh == -1 || ch != (Char)intCh)
			{
				return false;
			}

			reader.Read();
			return true;
		}

		public static Boolean Skip(this TextReader reader, String str)
		{
			int index = 0;
			while (index < str.Length)
			{
				int intCh = reader.Peek();
				if (intCh == -1)
				{
					return false;
				}

				if (str[index] != intCh)
				{
					return false;
				}
				reader.Read();
				++index;
			}

			return true;
		}

		public static void SkipWhitespaces(this TextReader reader)
		{
			while (true)
			{
				int intCh = reader.Peek();
				if (intCh == -1 || !Char.IsWhiteSpace((Char)intCh))
				{
					return;
				}

				reader.Read();
			}
		}

		public static String ScanNumericString(this TextReader reader, NumericSignMode mode)
		{
			StringBuilder builder = new StringBuilder();
			while (true)
			{

				int intCh = reader.Peek();
				if (intCh == '-' || intCh == '+')
				{
					if (builder.Length > 0)
					{
						return builder.ToString();
					}

					switch (mode)
					{
					case NumericSignMode.AllowNone:
						return null;
					default:
						if (intCh == '-' && (mode == NumericSignMode.AllowPlusOnly || mode == NumericSignMode.RequirePlus))
						{
							return null;
						}
						else if (intCh == '+' && (mode == NumericSignMode.AllowMinusOnly || mode == NumericSignMode.RequireMinus))
						{
							return null;
						}
						break;
					}
				}
				else if (intCh == -1 || (intCh < '0' || intCh > '9'))
				{
					if (builder.Length == 0)
					{
						return null;
					}
					return builder.ToString();
				}

				if (builder.Length == 0)
				{
					switch (mode)
					{
					case NumericSignMode.RequireOne:
						if (intCh != '-' && intCh != '+')
						{
							return null;
						}
						break;
					case NumericSignMode.RequirePlus:
						if (intCh != '+')
						{
							return null;
						}
						break;
					case NumericSignMode.RequireMinus:
						if (intCh != '-')
						{
							return null;
						}
						break;
					}
				}

				builder.Append((Char)reader.Read());
			}
		}

		public static String ScanDecimalString(this TextReader reader, NumericSignMode mode, CultureInfo culture)
		{
			CultureInfo usedCulture = culture == null ? CultureInfo.CurrentCulture : culture;

			String numeric = reader.ScanNumericString(mode);
			if (numeric == null)
			{
				return null;
			}

			if (!reader.Skip(usedCulture.NumberFormat.NumberDecimalSeparator))
			{
				return numeric;
			}

			String decimals = reader.ScanNumericString(NumericSignMode.AllowNone);
			if (decimals == null)
			{
				return null;
			}

			StringBuilder builder = new StringBuilder().Append(numeric).Append(usedCulture.NumberFormat.NumberDecimalSeparator).Append(decimals);
			if (reader.Skip('e'))
			{
				String shifterNumeric = reader.ScanNumericString(NumericSignMode.RequireOne);
				if (shifterNumeric != null)
				{
					builder.Append('e').Append(shifterNumeric);
				}
			}

			return builder.ToString();
		}

		public static String ScanDecimalString(this TextReader reader, NumericSignMode mode)
		{
			return ScanDecimalString(reader, mode, null);
		}

		public static Boolean ScanBoolean(this TextReader reader, out Boolean value)
		{
			value = false;
			Char[][] boolChars = null;
			int index = -1;

			int intCh = reader.Peek();
			if (intCh == -1)
			{
				return false;
			}

			for (int iBoolChars = 0;iBoolChars < _VALID_BOOLEAN_CHARS.Length;++iBoolChars)
			{
				boolChars = _VALID_BOOLEAN_CHARS[iBoolChars];
				if (boolChars[0][0] == (Char)intCh || boolChars[0][1] == (Char)intCh)
				{
					index = 1;
					break;
				}
			}

			if (index == -1)
			{
				return false;
			}

			StringBuilder builder = new StringBuilder(boolChars.Length);
			builder.Append((Char)reader.Read());

			while (index < boolChars.Length)
			{
				intCh = reader.Peek();
				if (intCh != boolChars[index][0] || intCh != boolChars[index][1])
				{
					return false;
				}
				builder.Append((Char)reader.Read());
				++index;
			}

			if (boolChars == _VALID_BOOLEAN_CHARS[0] || boolChars == _VALID_BOOLEAN_CHARS[2])
			{
				value = false;
			}
			else
			{
				value = true;
			}

			return true;
		}

		public static Boolean ScanByte(this TextReader reader, out Byte value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowPlusOnly);
			if (str == null)
			{
				return false;
			}
			return Byte.TryParse(str, out value);
		}

		public static Boolean ScanSByte(this TextReader reader, out SByte value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowBoth);
			if (str == null)
			{
				return false;
			}
			return SByte.TryParse(str, out value);
		}

		public static Boolean ScanUInt16(this TextReader reader, out UInt16 value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowPlusOnly);
			if (str == null)
			{
				return false;
			}
			return UInt16.TryParse(str, out value);
		}

		public static Boolean ScanUInt16(this TextReader reader, out Int16 value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowBoth);
			if (str == null)
			{
				return false;
			}
			return Int16.TryParse(str, out value);
		}

		public static Boolean ScanUInt32(this TextReader reader, out UInt32 value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowPlusOnly);
			if (str == null)
			{
				return false;
			}
			return UInt32.TryParse(str, out value);
		}

		public static Boolean ScanInt32(this TextReader reader, out Int32 value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowBoth);
			if (str == null)
			{
				return false;
			}
			return Int32.TryParse(str, out value);
		}

		public static Boolean ScanUInt64(this TextReader reader, out UInt64 value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowPlusOnly);
			if (str == null)
			{
				return false;
			}
			return UInt64.TryParse(str, out value);
		}

		public static Boolean ScanInt64(this TextReader reader, out Int64 value)
		{
			value = 0;
			String str = reader.ScanNumericString(NumericSignMode.AllowBoth);
			if (str == null)
			{
				return false;
			}
			return Int64.TryParse(str, out value);
		}

		public static Boolean ScanSingle(this TextReader reader, CultureInfo culture, out Single value)
		{
			value = 0;
			String str = reader.ScanDecimalString(NumericSignMode.AllowBoth, culture);
			if (str == null)
			{
				return false;
			}
			return Single.TryParse(str, out value);
		}

		public static Boolean ScanSingle(this TextReader reader, out Single value)
		{
			return ScanSingle(reader, null, out value);
		}

		public static Boolean ScanDouble(this TextReader reader, CultureInfo culture, out Double value)
		{
			value = 0;
			String str = reader.ScanDecimalString(NumericSignMode.AllowBoth, culture);
			if (str == null)
			{
				return false;
			}
			return Double.TryParse(str, out value);
		}

		public static Boolean ScanDouble(this TextReader reader, out Double value)
		{
			return ScanDouble(reader, null, out value);
		}
	}
}
