using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AutoPatcherAdmin
{
    public class InIReader
    {
        #region Fields
        private readonly List<string> _contents;
        private readonly string _fileName;
        #endregion

        #region Constructor
        public InIReader(string fileName)
        {
            _fileName = fileName;

            _contents = new List<string>();
            try
            {
                if (File.Exists(_fileName))
                    _contents.AddRange(File.ReadAllLines(_fileName));
            }
            catch
            {
            }
        }
        #endregion

        #region Functions
        private string FindValue(string section, string key)
        {
            for (int a = 0; a < _contents.Count; a++)
                if (String.CompareOrdinal(_contents[a], "[" + section + "]") == 0)
                    for (int b = a + 1; b < _contents.Count; b++)
                        if (String.CompareOrdinal(_contents[b].Split('=')[0], key) == 0)
                            return _contents[b].Split('=')[1];
                        else if (_contents[b].StartsWith("[") && _contents[b].EndsWith("]"))
                            return null;
            return null;
        }

        private int FindIndex(string section, string key)
        {
            for (int a = 0; a < _contents.Count; a++)
                if (String.CompareOrdinal(_contents[a], "[" + section + "]") == 0)
                    for (int b = a + 1; b < _contents.Count; b++)
                        if (String.CompareOrdinal(_contents[b].Split('=')[0], key) == 0)
                            return b;
                        else if (_contents[b].StartsWith("[") && _contents[b].EndsWith("]"))
                        {
                            _contents.Insert(b - 1, key + "=");
                            return b - 1;
                        }
                        else if (_contents.Count - 1 == b)
                        {
                            _contents.Add(key + "=");
                            return _contents.Count - 1;
                        }
            if (_contents.Count > 0)
                _contents.Add("");

            _contents.Add("[" + section + "]");
            _contents.Add(key + "=");
            return _contents.Count - 1;
        }

        public void Save()
        {
            try
            {
                File.WriteAllLines(_fileName, _contents.ToArray());
            }
            catch
            {
            }
        }
        #endregion

        #region Read
        public bool ReadBoolean(string section, string key, bool Default)
        {

            if (!bool.TryParse(FindValue(section, key), out bool result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public byte ReadByte(string section, string key, byte Default)
        {

            if (!byte.TryParse(FindValue(section, key), out byte result))
            {
                result = Default;
                Write(section, key, Default);
            }


            return result;
        }

        public sbyte ReadSByte(string section, string key, sbyte Default)
        {

            if (!sbyte.TryParse(FindValue(section, key), out sbyte result))
            {
                result = Default;
                Write(section, key, Default);
            }


            return result;
        }

        public ushort ReadUInt16(string section, string key, ushort Default)
        {

            if (!ushort.TryParse(FindValue(section, key), out ushort result))
            {
                result = Default;
                Write(section, key, Default);
            }


            return result;
        }

        public short ReadInt16(string section, string key, short Default)
        {

            if (!short.TryParse(FindValue(section, key), out short result))
            {
                result = Default;
                Write(section, key, Default);
            }


            return result;
        }

        public uint ReadUInt32(string section, string key, uint Default)
        {

            if (!uint.TryParse(FindValue(section, key), out uint result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public int ReadInt32(string section, string key, int Default)
        {

            if (!int.TryParse(FindValue(section, key), out int result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public ulong ReadUInt64(string section, string key, ulong Default)
        {

            if (!ulong.TryParse(FindValue(section, key), out ulong result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public long ReadInt64(string section, string key, long Default)
        {

            if (!long.TryParse(FindValue(section, key), out long result))
            {
                result = Default;
                Write(section, key, Default);
            }


            return result;
        }

        public float ReadSingle(string section, string key, float Default)
        {

            if (!float.TryParse(FindValue(section, key), out float result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public double ReadDouble(string section, string key, double Default)
        {

            if (!double.TryParse(FindValue(section, key), out double result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public decimal ReadDecimal(string section, string key, decimal Default)
        {

            if (!decimal.TryParse(FindValue(section, key), out decimal result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public string ReadString(string section, string key, string Default)
        {
            string result = FindValue(section, key);

            if (string.IsNullOrEmpty(result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public char ReadChar(string section, string key, char Default)
        {

            if (!char.TryParse(FindValue(section, key), out char result))
            {
                result = Default;
                Write(section, key, Default);
            }

            return result;
        }

        public Point ReadPoint(string section, string key, Point Default)
        {
            string temp = FindValue(section, key);
            if (temp == null || !int.TryParse(temp.Split(',')[0], out int tempX))
            {
                Write(section, key, Default);
                return Default;
            }
            if (!int.TryParse(temp.Split(',')[1], out int tempY))
            {
                Write(section, key, Default);
                return Default;
            }

            return new Point(tempX, tempY);
        }

        public Size ReadSize(string section, string key, Size Default)
        {
            string temp = FindValue(section, key);
            if (!int.TryParse(temp.Split(',')[0], out int tempX))
            {
                Write(section, key, Default);
                return Default;
            }
            if (!int.TryParse(temp.Split(',')[1], out int tempY))
            {
                Write(section, key, Default);
                return Default;
            }

            return new Size(tempX, tempY);
        }

        public TimeSpan ReadTimeSpan(string section, string key, TimeSpan Default)
        {

            if (!TimeSpan.TryParse(FindValue(section, key), out TimeSpan result))
            {
                result = Default;
                Write(section, key, Default);
            }


            return result;
        }
        #endregion

        #region Write
        public void Write(string section, string key, bool value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, byte value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, sbyte value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, ushort value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, short value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, uint value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, int value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, ulong value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, long value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, float value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, double value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, decimal value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, string value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, char value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }

        public void Write(string section, string key, Point value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value.X + "," + value.Y;
            Save();
        }

        public void Write(string section, string key, Size value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value.Width + "," + value.Height;
            Save();
        }

        public void Write(string section, string key, TimeSpan value)
        {
            _contents[FindIndex(section, key)] = key + "=" + value;
            Save();
        }
        #endregion
    }
}
