using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirDataTool
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

            if (Settings.DatabaseVersion >= 36)
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
        }

        public override string ToString()
        {
            return string.Format("Monster: {0} - {1} - {2} - {3} - {4} - {5} - {6} - {7} - {8} - {9}", MonsterIndex, Functions.PointToString(Location), Count, Spread, Delay, Direction, RandomDelay, RespawnIndex, SaveRespawnTime, RespawnTicks);
        }
    }
}
