using Server.MirEnvir;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server.MirDatabase
{
    public class SafeZoneInfo
    {
        public Point Location;
        public ushort Size;
        public bool StartPoint;

        public MapInfo Info;

        public SafeZoneInfo()
        {

        }

        public SafeZoneInfo(BinaryReader reader)
        {
            Location = new Point(reader.ReadInt32(), reader.ReadInt32());
            Size = reader.ReadUInt16();
            StartPoint = reader.ReadBoolean();
            /*
            File.AppendAllText(Envir.exportInfo + @".\MapInfo\SafeZoneInfo\LOAD_SafeZoneInfo_.txt", string.Format("Location X {0} Y {1}\nSize = {2}\nStartPoint {3}\n\n\n\n",
                Location.X,
                Location.Y,
                Size,
                StartPoint));
                */
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Location.X);
            writer.Write(Location.Y);
            writer.Write(Size);
            writer.Write(StartPoint);
            /*
            File.AppendAllText(Envir.exportInfo + @".\MapInfo\SafeZoneInfo\SAVE_SafeZoneInfo_.txt", string.Format("Location X {0} Y {1}\nSize = {2}\nStartPoint {3}\n\n\n\n",
                Location.X,
                Location.Y,
                Size,
                StartPoint));
                */
        }

        public override string ToString()
        {
            return string.Format("Map: {0}- {1}", Functions.PointToString(Location), StartPoint);
        }
    }
}
