using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

public static class PlayerPrefsSl
{
	public enum DataType : byte
	{
		Bool,
		Byte,
		Sbyte,
		Char,
		Decimal,
		Double,
		Float,
		Int,
		Uint,
		Long,
		Ulong,
		Short,
		Ushort,
		String,
		BoolArray,
		ByteArray,
		SbyteArray,
		CharArray,
		DecimalArray,
		DoubleArray,
		FloatArray,
		IntArray,
		UintArray,
		LongArray,
		UlongArray,
		ShortArray,
		UshortArray,
		StringArray
	}

	private const string ArraySeparator = ";;`'.+=;;";

	private const string KeySeparator = "::-%(|::";

	private static Dictionary<string, string> _registry;

	private static string _path;

	private static readonly UTF8Encoding Encoding;

	static PlayerPrefsSl()
	{
		_registry = new Dictionary<string, string>();
		Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
		_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SCP Secret Laboratory", "registry.txt");
		Refresh();
	}

	private static string Prefix(string key, DataType type)
	{
		byte b = (byte)type;
		return b.ToString("00") + key;
	}

	public static void Refresh()
	{
		_registry.Clear();
		if (!File.Exists(_path))
		{
			File.Create(_path).Close();
		}
		else
		{
			using (StreamReader streamReader = new StreamReader(_path))
			{
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					if (text.Contains("::-%(|::"))
					{
						int num = text.IndexOf("::-%(|::", StringComparison.Ordinal);
						_registry.Add(text.Substring(0, num), text.Substring(num + "::-%(|::".Length));
					}
				}
			}
		}
	}

	private static void Save()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<string, string> item in _registry)
		{
			stringBuilder.Append(item.Key);
			stringBuilder.Append("::-%(|::");
			stringBuilder.Append(item.Value);
			stringBuilder.AppendLine();
		}
		File.WriteAllText(_path, stringBuilder.ToString(), Encoding);
	}

	public static bool HasKey(string key, DataType type)
	{
		return _registry.ContainsKey(Prefix(key, type));
	}

	public static void DeleteKey(string key, DataType type)
	{
		_registry.Remove(Prefix(key, type));
		Save();
	}

	public static void DeleteAll()
	{
		File.WriteAllText(_path, "", Encoding);
	}

	private static void WriteString(string key, string value)
	{
		if (_registry.ContainsKey(key))
		{
			_registry[key] = value;
		}
		else
		{
			_registry.Add(key, value);
		}
		Save();
	}

	public static void Set(string key, bool value)
	{
		WriteString(Prefix(key, DataType.Bool), value ? "true" : "false");
	}

	public static void Set(string key, byte value)
	{
		WriteString(Prefix(key, DataType.Byte), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, sbyte value)
	{
		WriteString(Prefix(key, DataType.Sbyte), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, char value)
	{
		WriteString(Prefix(key, DataType.Char), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, decimal value)
	{
		WriteString(Prefix(key, DataType.Decimal), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, double value)
	{
		WriteString(Prefix(key, DataType.Double), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, float value)
	{
		WriteString(Prefix(key, DataType.Float), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, int value)
	{
		WriteString(Prefix(key, DataType.Int), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, uint value)
	{
		WriteString(Prefix(key, DataType.Uint), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, long value)
	{
		WriteString(Prefix(key, DataType.Long), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, ulong value)
	{
		WriteString(Prefix(key, DataType.Ulong), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, short value)
	{
		WriteString(Prefix(key, DataType.Short), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, ushort value)
	{
		WriteString(Prefix(key, DataType.Ushort), value.ToString(CultureInfo.InvariantCulture));
	}

	public static void Set(string key, string value)
	{
		WriteString(Prefix(key, DataType.String), value);
	}

	public static void Set(string key, bool[] array)
	{
		WriteString(Prefix(key, DataType.BoolArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<bool> ienumerable)
	{
		WriteString(Prefix(key, DataType.BoolArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, byte[] array)
	{
		WriteString(Prefix(key, DataType.ByteArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<byte> ienumerable)
	{
		WriteString(Prefix(key, DataType.ByteArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, sbyte[] array)
	{
		WriteString(Prefix(key, DataType.SbyteArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<sbyte> ienumerable)
	{
		WriteString(Prefix(key, DataType.SbyteArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, char[] array)
	{
		WriteString(Prefix(key, DataType.CharArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<char> ienumerable)
	{
		WriteString(Prefix(key, DataType.CharArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, decimal[] array)
	{
		WriteString(Prefix(key, DataType.DecimalArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<decimal> ienumerable)
	{
		WriteString(Prefix(key, DataType.DecimalArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, double[] array)
	{
		WriteString(Prefix(key, DataType.DoubleArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<double> ienumerable)
	{
		WriteString(Prefix(key, DataType.DoubleArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, float[] array)
	{
		WriteString(Prefix(key, DataType.FloatArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<float> ienumerable)
	{
		WriteString(Prefix(key, DataType.FloatArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, int[] array)
	{
		WriteString(Prefix(key, DataType.IntArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<int> ienumerable)
	{
		WriteString(Prefix(key, DataType.IntArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, uint[] array)
	{
		WriteString(Prefix(key, DataType.UintArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<uint> ienumerable)
	{
		WriteString(Prefix(key, DataType.UintArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, long[] array)
	{
		WriteString(Prefix(key, DataType.LongArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<long> ienumerable)
	{
		WriteString(Prefix(key, DataType.LongArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, ulong[] array)
	{
		WriteString(Prefix(key, DataType.UlongArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<ulong> ienumerable)
	{
		WriteString(Prefix(key, DataType.UlongArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, short[] array)
	{
		WriteString(Prefix(key, DataType.ShortArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<short> ienumerable)
	{
		WriteString(Prefix(key, DataType.ShortArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, ushort[] array)
	{
		WriteString(Prefix(key, DataType.UshortArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<ushort> ienumerable)
	{
		WriteString(Prefix(key, DataType.UshortArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static void Set(string key, string[] array)
	{
		WriteString(Prefix(key, DataType.StringArray), string.Join(";;`'.+=;;", array));
	}

	public static void Set(string key, IEnumerable<string> ienumerable)
	{
		WriteString(Prefix(key, DataType.StringArray), string.Join(";;`'.+=;;", ienumerable));
	}

	public static bool Get(string key, bool defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Bool), out string value) || !bool.TryParse(value, out bool result))
		{
			return defaultValue;
		}
		return result;
	}

	public static byte Get(string key, byte defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Byte), out string value) || !byte.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out byte result))
		{
			return defaultValue;
		}
		return result;
	}

	public static sbyte Get(string key, sbyte defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Sbyte), out string value) || !sbyte.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out sbyte result))
		{
			return defaultValue;
		}
		return result;
	}

	public static char Get(string key, char defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Char), out string value) || !char.TryParse(value, out char result))
		{
			return defaultValue;
		}
		return result;
	}

	public static decimal Get(string key, decimal defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Decimal), out string value) || !decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
		{
			return defaultValue;
		}
		return result;
	}

	public static double Get(string key, double defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Double), out string value) || !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
		{
			return defaultValue;
		}
		return result;
	}

	public static float Get(string key, float defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Float), out string value) || !float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
		{
			return defaultValue;
		}
		return result;
	}

	public static int Get(string key, int defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Int), out string value) || !int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
		{
			return defaultValue;
		}
		return result;
	}

	public static uint Get(string key, uint defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Uint), out string value) || !uint.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out uint result))
		{
			return defaultValue;
		}
		return result;
	}

	public static long Get(string key, long defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Long), out string value) || !long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long result))
		{
			return defaultValue;
		}
		return result;
	}

	public static ulong Get(string key, ulong defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Ulong), out string value) || !ulong.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out ulong result))
		{
			return defaultValue;
		}
		return result;
	}

	public static short Get(string key, short defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Short), out string value) || !short.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out short result))
		{
			return defaultValue;
		}
		return result;
	}

	public static ushort Get(string key, ushort defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.Int), out string value) || !ushort.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out ushort result))
		{
			return defaultValue;
		}
		return result;
	}

	public static string Get(string key, string defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.String), out string value))
		{
			return defaultValue;
		}
		return value;
	}

	public static bool[] Get(string key, bool[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.BoolArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			bool[] array2 = new bool[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!bool.TryParse(array[i], out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static byte[] Get(string key, byte[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.ByteArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!byte.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static sbyte[] Get(string key, sbyte[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.SbyteArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			sbyte[] array2 = new sbyte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!sbyte.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static char[] Get(string key, char[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.CharArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			char[] array2 = new char[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!char.TryParse(array[i], out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static decimal[] Get(string key, decimal[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.DecimalArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			decimal[] array2 = new decimal[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!decimal.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static double[] Get(string key, double[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.DoubleArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			double[] array2 = new double[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!double.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static float[] Get(string key, float[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.FloatArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			float[] array2 = new float[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!float.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static int[] Get(string key, int[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.IntArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			int[] array2 = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!int.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static uint[] Get(string key, uint[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.UintArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			uint[] array2 = new uint[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!uint.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static long[] Get(string key, long[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.LongArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			long[] array2 = new long[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!long.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static ulong[] Get(string key, ulong[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.UlongArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			ulong[] array2 = new ulong[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!ulong.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static short[] Get(string key, short[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.ShortArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			short[] array2 = new short[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!short.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static ushort[] Get(string key, ushort[] defaultValue)
	{
		if (_registry.TryGetValue(Prefix(key, DataType.UshortArray), out string value))
		{
			string[] array = value.Split(new string[1]
			{
				";;`'.+=;;"
			}, StringSplitOptions.None);
			ushort[] array2 = new ushort[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!ushort.TryParse(array[i], NumberStyles.Any, CultureInfo.InvariantCulture, out array2[i]))
				{
					return defaultValue;
				}
			}
			return array2;
		}
		return defaultValue;
	}

	public static string[] Get(string key, string[] defaultValue)
	{
		if (!_registry.TryGetValue(Prefix(key, DataType.StringArray), out string value))
		{
			return defaultValue;
		}
		return value.Split(new string[1]
		{
			";;`'.+=;;"
		}, StringSplitOptions.None);
	}
}
