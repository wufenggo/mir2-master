using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirDataTool
{
    public class MapInfo
    {
        public int Index;
        public string FileName = string.Empty, Title = string.Empty;
        public ushort MiniMap, BigMap, Music;
        public LightSetting Light;
        public byte MapDarkLight = 0, MineIndex = 0;

        public bool NoTeleport, NoReconnect, NoRandom, NoEscape, NoRecall, NoDrug, NoPosition, NoFight, SafeZone,
            NoThrowItem, NoDropPlayer, NoDropMonster, NoNames, NoMount, NeedBridle, Fight, NeedHole, Fire, Lightning, NoHero, GT;

        public string NoReconnectMap = string.Empty;
        public int FireDamage, LightningDamage;

        public List<SafeZoneInfo> SafeZones = new List<SafeZoneInfo>();
        public List<MovementInfo> Movements = new List<MovementInfo>();
        public List<RespawnInfo> Respawns = new List<RespawnInfo>();
        public List<NPCInfo> NPCs = new List<NPCInfo>();
        public List<MineZone> MineZones = new List<MineZone>();
        public List<PublicEventInfo> PublicEvents = new List<PublicEventInfo>();

        public MapInfo()
        {

        }

        public MapInfo(BinaryReader reader)
        {
            Index = reader.ReadInt32();
            FileName = reader.ReadString();
            Title = reader.ReadString();
            MiniMap = reader.ReadUInt16();
            Light = (LightSetting)reader.ReadByte();

            if (Settings.DatabaseVersion >= 3) BigMap = reader.ReadUInt16();

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                SafeZones.Add(new SafeZoneInfo(reader) { Info = this });

            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                Respawns.Add(new RespawnInfo(reader, Settings.DatabaseVersion, Settings.CustomDatabaseVersion));

            if (Settings.DatabaseVersion <= 33)
            {
                count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                    NPCs.Add(new NPCInfo(reader));
            }

            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                Movements.Add(new MovementInfo(reader));

            if (Settings.DatabaseVersion < 14) return;

            NoTeleport = reader.ReadBoolean();
            NoReconnect = reader.ReadBoolean();
            NoReconnectMap = reader.ReadString();
            NoRandom = reader.ReadBoolean();
            NoEscape = reader.ReadBoolean();
            NoRecall = reader.ReadBoolean();
            NoDrug = reader.ReadBoolean();
            NoPosition = reader.ReadBoolean();
            NoThrowItem = reader.ReadBoolean();
            NoDropPlayer = reader.ReadBoolean();
            NoDropMonster = reader.ReadBoolean();
            NoNames = reader.ReadBoolean();
            Fight = reader.ReadBoolean();
            if (Settings.DatabaseVersion == 14) NeedHole = reader.ReadBoolean();
            Fire = reader.ReadBoolean();
            FireDamage = reader.ReadInt32();
            Lightning = reader.ReadBoolean();
            LightningDamage = reader.ReadInt32();
            if (Settings.DatabaseVersion < 23) return;
            MapDarkLight = reader.ReadByte();
            if (Settings.DatabaseVersion < 26) return;
            count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                MineZones.Add(new MineZone(reader));
            if (Settings.DatabaseVersion < 27) return;
            MineIndex = reader.ReadByte();

            if (Settings.DatabaseVersion < 33) return;
            NoMount = reader.ReadBoolean();
            NeedBridle = reader.ReadBoolean();

            if (Settings.DatabaseVersion < 42) return;
            NoFight = reader.ReadBoolean();

            if (Settings.DatabaseVersion < 53) return;
            Music = reader.ReadUInt16();

            if (Settings.DatabaseVersion < 103) return;
            NoHero = reader.ReadBoolean();

            if (Settings.DatabaseVersion < 110) return;
            GT = reader.ReadBoolean();

            if (Settings.DatabaseVersion < 117) return;
            SafeZone = reader.ReadBoolean();
            if (Settings.DatabaseVersion > 135)
            {
                count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                    PublicEvents.Add(new PublicEventInfo(reader));
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Index);
            writer.Write(FileName);
            writer.Write(Title);
            writer.Write(MiniMap);
            writer.Write((byte)Light);
            writer.Write(BigMap);
            writer.Write(SafeZones.Count);
            for (int i = 0; i < SafeZones.Count; i++)
                SafeZones[i].Save(writer);

            writer.Write(Respawns.Count);
            for (int i = 0; i < Respawns.Count; i++)
                Respawns[i].Save(writer);

            writer.Write(Movements.Count);
            for (int i = 0; i < Movements.Count; i++)
                Movements[i].Save(writer);

            writer.Write(NoTeleport);
            writer.Write(NoReconnect);
            writer.Write(NoReconnectMap);
            writer.Write(NoRandom);
            writer.Write(NoEscape);
            writer.Write(NoRecall);
            writer.Write(NoDrug);
            writer.Write(NoPosition);
            writer.Write(NoThrowItem);
            writer.Write(NoDropPlayer);
            writer.Write(NoDropMonster);
            writer.Write(NoNames);
            writer.Write(Fight);
            writer.Write(Fire);
            writer.Write(FireDamage);
            writer.Write(Lightning);
            writer.Write(LightningDamage);
            writer.Write(MapDarkLight);
            writer.Write(MineZones.Count);
            for (int i = 0; i < MineZones.Count; i++)
                MineZones[i].Save(writer);
            writer.Write(MineIndex);

            writer.Write(NoMount);
            writer.Write(NeedBridle);

            writer.Write(NoFight);

            writer.Write(Music);

            writer.Write(NoHero);
            writer.Write(GT);
            writer.Write(SafeZone);
            writer.Write(PublicEvents.Count);
            for (int i = 0; i < PublicEvents.Count; i++)
                PublicEvents[i].Save(writer);
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", Index, Title);
        }

        public void CreateSafeZone()
        {
            SafeZones.Add(new SafeZoneInfo { Info = this });
        }
        public void CreateMovementInfo()
        {
            Movements.Add(new MovementInfo());
        }
    }
}
