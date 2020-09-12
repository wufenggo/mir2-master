using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Server.MirEnvir;

namespace Server.MirDatabase
{
    public class RespawnInfo
    {
        public int MonsterIndex;
        public Point Location;
        public ushort Count, Spread, Delay, RandomDelay;
        public byte Direction;

        public string RoutePath = string.Empty;
        public int RespawnIndex;
        public bool SaveRespawnTime = false;
        public ushort RespawnTicks; //leave 0 if not using this system!

        public RespawnInfo()
        {

        }
        public RespawnInfo(BinaryReader reader, int Version, int Customversion)
        {
            MonsterIndex = reader.ReadInt32();

            Location = new Point(reader.ReadInt32(), reader.ReadInt32());

            Count = reader.ReadUInt16();
            Spread = reader.ReadUInt16();

            Delay = reader.ReadUInt16();
            Direction = reader.ReadByte();

            if (Envir.LoadVersion >= 36)
            {
                RoutePath = reader.ReadString();
            }

            if (Version > 67)
            {
                RandomDelay = reader.ReadUInt16();
                RespawnIndex = reader.ReadInt32();
                SaveRespawnTime = reader.ReadBoolean();
                RespawnTicks = reader.ReadUInt16();
            }
            else
            {
                RespawnIndex = ++SMain.Envir.RespawnIndex;
            }
        }

        public static RespawnInfo FromText(string text)
        {
            string[] data = text.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 7) return null;

            RespawnInfo info = new RespawnInfo();
            if (!int.TryParse(data[0], out info.MonsterIndex)) return null;
            if (!int.TryParse(data[1], out int x)) return null;
            if (!int.TryParse(data[2], out int y)) return null;

            info.Location = new Point(x, y);

            if (!ushort.TryParse(data[3], out info.Count)) return null;
            if (!ushort.TryParse(data[4], out info.Spread)) return null;
            if (!ushort.TryParse(data[5], out info.Delay)) return null;
            if (!byte.TryParse(data[6], out info.Direction)) return null;
            if (!ushort.TryParse(data[7], out info.RandomDelay)) return null;
            if (!int.TryParse(data[8], out info.RespawnIndex)) return null;
            if (!bool.TryParse(data[9], out info.SaveRespawnTime)) return null;
            if (!ushort.TryParse(data[10], out info.RespawnTicks)) return null;

            return info;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(MonsterIndex);

            writer.Write(Location.X);
            writer.Write(Location.Y);
            writer.Write(Count);
            writer.Write(Spread);

            writer.Write(Delay);
            writer.Write(Direction);

            writer.Write(RoutePath);

            writer.Write(RandomDelay);
            writer.Write(RespawnIndex);
            writer.Write(SaveRespawnTime);
            writer.Write(RespawnTicks);
            /*
            File.AppendAllText(Envir.exportInfo + @".\MapInfo\SafeZoneInfo\SAVE_SafeZoneInfo_.txt", string.Format("MonsterIndex {4}\nLocation X {0} Y {1}\nSpread = {2}\nCount {3}\nDelay {5}\nDirection {6}\nReoute Path {7}\nRandom Delay {8}\nRespawnIndex {9}\nSaveRespawnTime {10}\nRespawn Ticks {11}\n\n\n\n",
                    Location.X,
                    Location.Y,
                    Spread,
                    Count,
                    MonsterIndex, Delay, Direction, RoutePath, RandomDelay,
                    RespawnIndex, SaveRespawnTime, RespawnTicks));
                    */
        }

        public override string ToString()
        {
            return string.Format("Monster: {0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8} - {9}", MonsterIndex, Functions.PointToString(Location), Count, Spread, Delay, Direction, RandomDelay, RespawnIndex, SaveRespawnTime, RespawnTicks);
        }
    }

    public class RouteInfo
    {
        public Point Location;
        public int Delay;

        public static RouteInfo FromText(string text)
        {
            string[] data = text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length < 2) return null;

            RouteInfo info = new RouteInfo();
            if (!int.TryParse(data[0], out int x)) return null;
            if (!int.TryParse(data[1], out int y)) return null;

            info.Location = new Point(x, y);

            if (data.Length <= 2) return info;

            return !int.TryParse(data[2], out info.Delay) ? info : info;
        }
    }
}