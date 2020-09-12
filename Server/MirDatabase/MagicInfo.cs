using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Server.MirEnvir;
using S = ServerPackets;

namespace Server.MirDatabase
{
    public class MagicInfo
    {
        public string Name;
        public Spell Spell;
        public byte BaseCost, LevelCost, Icon;
        public byte Level1, Level2, Level3 , Level4  = 0, Level5 = 0;
        public ushort Need1, Need2, Need3 , Need4 = 0, Need5 = 0;
        public uint DelayBase = 1800, DelayReduction;
        public ushort PowerBase, PowerBonus;
        public ushort MPowerBase, MPowerBonus;
        public float MultiplierBase = 1.0f, MultiplierBonus;
        public byte Range = 9;

        #region PvP Damage
        public ushort PvPPowerBase, PvPPowerBonus;
        public ushort PvPMPowerBase, PvPMPowerBonus;
        public float PvPMultiplierBase = 1.0f, PvPMultiplierBonus;
        #endregion

        public bool HumUpEnable;
        public bool OverrideHumUp;

        public override string ToString()
        {
            return Name;
        }

        public MagicInfo()
        {

        }

        public void Copy(MagicInfo info)
        {
            Name = info.Name;
            Spell = info.Spell;
            BaseCost = info.BaseCost;
            LevelCost = info.LevelCost;
            Icon = info.Icon;

            Level1 = info.Level1;
            Level2 = info.Level2;
            Level3 = info.Level3;
            Level4 = info.Level4;
            Level5 = info.Level5;

            Need1 = info.Need1;
            Need2 = info.Need2;
            Need3 = info.Need3;
            Need4 = info.Need4;
            Need5 = info.Need5;

            DelayBase = info.DelayBase;
            DelayReduction = info.DelayReduction;

            PowerBase = info.PowerBase;
            PowerBonus = info.PowerBonus;

            MPowerBase = info.MPowerBase;
            MPowerBonus = info.MPowerBonus;

            MultiplierBase = info.MultiplierBase;
            MultiplierBonus = info.MultiplierBonus;

            Range = info.Range;

            HumUpEnable = info.HumUpEnable;
            PvPPowerBase = info.PvPPowerBase;
            PvPPowerBonus = info.PvPPowerBonus;
            PvPMPowerBase = info.PvPMPowerBase;
            PvPMPowerBonus = info.PvPMPowerBonus;
            PvPMultiplierBase = info.PvPMultiplierBase;
            PvPMultiplierBonus = info.PvPMultiplierBonus;
        }

        public MagicInfo (BinaryReader reader, int version = int.MaxValue, int Customversion = int.MaxValue)
        {
            Name = reader.ReadString();
            Spell = (Spell)reader.ReadByte();
            BaseCost = reader.ReadByte();
            LevelCost = reader.ReadByte();
            Icon = reader.ReadByte();
            Level1 = reader.ReadByte();
            Level2 = reader.ReadByte();
            Level3 = reader.ReadByte();
            Need1 = reader.ReadUInt16();
            Need2 = reader.ReadUInt16();
            Need3 = reader.ReadUInt16();
            DelayBase = reader.ReadUInt32();
            DelayReduction = reader.ReadUInt32();
            PowerBase = reader.ReadUInt16();
            PowerBonus = reader.ReadUInt16();
            MPowerBase = reader.ReadUInt16();
            MPowerBonus = reader.ReadUInt16();

            if (version > 66)
                Range = reader.ReadByte();
            if (version > 70)
            {
                MultiplierBase = reader.ReadSingle();
                MultiplierBonus = reader.ReadSingle();
            }

            if (version > 82)
            {
                Need4 = reader.ReadUInt16();
                Need5= reader.ReadUInt16();
                Level4 = reader.ReadByte();
                Level5 = reader.ReadByte();
                HumUpEnable = reader.ReadBoolean();
            }
            if (version > 126)
                OverrideHumUp = reader.ReadBoolean();

            if (version > 136)
            {
                PvPMPowerBase = reader.ReadUInt16();
                PvPMPowerBonus = reader.ReadUInt16();
                PvPMultiplierBase = reader.ReadSingle();
                PvPMultiplierBonus = reader.ReadSingle();
                PvPPowerBase = reader.ReadUInt16();
                PvPPowerBonus = reader.ReadUInt16();
            }
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write((byte)Spell);
            writer.Write(BaseCost);
            writer.Write(LevelCost);
            writer.Write(Icon);
            writer.Write(Level1);
            writer.Write(Level2);
            writer.Write(Level3);
            writer.Write(Need1);
            writer.Write(Need2);
            writer.Write(Need3);
            writer.Write(DelayBase);
            writer.Write(DelayReduction);
            writer.Write(PowerBase);
            writer.Write(PowerBonus);
            writer.Write(MPowerBase);
            writer.Write(MPowerBonus);
            writer.Write(Range);
            writer.Write(MultiplierBase);
            writer.Write(MultiplierBonus);

            writer.Write(Need4);
            writer.Write(Need5);
            writer.Write(Level4);
            writer.Write(Level5);
            writer.Write(HumUpEnable);
            writer.Write(OverrideHumUp);
            
            writer.Write(PvPMPowerBase);
            writer.Write(PvPMPowerBonus);
            writer.Write(PvPMultiplierBase);
            writer.Write(PvPMultiplierBonus);
            writer.Write(PvPPowerBase);
            writer.Write(PvPPowerBonus);
            
        }
    }

    public class UserMagic
    {
        public Spell Spell;
        public MagicInfo Info;

        public byte Level, Key;
        public ushort Experience;
        public bool IsTempSpell;
        public long CastTime;

        public int ManaCost
        {
            get
            {
                return Info.BaseCost + Level * Info.LevelCost;
            }
        }

        private MagicInfo GetMagicInfo(Spell spell)
        {
            for (int i = 0; i < SMain.Envir.MagicInfoList.Count; i++)
            {
                MagicInfo info = SMain.Envir.MagicInfoList[i];
                if (info.Spell != spell) continue;
                return info;
            }
            return null;
        }

        public UserMagic(Spell spell)
        {
            Spell = spell;
            
            Info = GetMagicInfo(Spell);
        }
        public UserMagic(BinaryReader reader)
        {
            Spell = (Spell) reader.ReadByte();
            Info = GetMagicInfo(Spell);

            Level = reader.ReadByte();
            Key = reader.ReadByte();
            Experience = reader.ReadUInt16();

            if (Envir.LoadVersion < 15) return;
            IsTempSpell = reader.ReadBoolean();

            if (Envir.LoadVersion < 65) return;
            CastTime = reader.ReadInt64();
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write((byte) Spell);

            writer.Write(Level);
            writer.Write(Key);
            writer.Write(Experience);
            writer.Write(IsTempSpell);
            writer.Write(CastTime);
        }

        public Packet GetInfo()
        {
            return new S.NewMagic
                {
                    Magic = CreateClientMagic()
                };
        }

        public Packet GetHeroInfo()
        {
            return new S.HeroNewMagic
            {
                Magic = CreateClientMagic()
            };
        }

        public ClientMagic CreateClientMagic() //
        {
            return new ClientMagic
            {
                Spell = Spell,
                BaseCost = Info.BaseCost,
                LevelCost = Info.LevelCost,
                Icon = Info.Icon,
                Level1 = Info.Level1,
                Level2 = Info.Level2,
                Level3 = Info.Level3,
                Level4 = Info.Level4,
                Level5 = Info.Level5,
                Need1 = Info.Need1,
                Need2 = Info.Need2,
                Need3 = Info.Need3,
                Need4 = Info.Need4,
                Need5 = Info.Need5,
                Level = Level,
                HumUpEnable = Info.HumUpEnable,
                OverrideHumUp = Info.OverrideHumUp,
                Key = Key,
                Experience = Experience,
                IsTempSpell = IsTempSpell,
                Delay = GetDelay(),
                Range = Info.Range,
                CastTime = (CastTime != 0) && (SMain.Envir.Time > CastTime) ? SMain.Envir.Time - CastTime : 0,
                PowerBase = Info.PowerBase,
                PowerBonus = Info.PowerBonus,
                MPowerBase = Info.MPowerBase,
                MPowerBonus = Info.MPowerBonus,
                MultiplierBase = Info.MultiplierBase,
                MultiplierBonus = Info.MultiplierBonus,
                PvPPowerBase = Info.PvPPowerBase,
                PvPPowerBonus = Info.PvPPowerBonus,
                PvPMPowerBase = Info.PvPMPowerBase,
                PvPMPowerBonus = Info.PvPMPowerBonus,
                PvPMultiplierBase = Info.PvPMultiplierBase,
                PvPMultiplierBonus = Info.PvPMultiplierBonus,
            };
        }

        public int GetDamage(int DamageBase)
        {
            return (int)((DamageBase + GetPower()) * GetMultiplier());
        }

        public int GetPvPDamage(int DamageBase)
        {
            return (int)((DamageBase + GetPvPPower()) * GetPvPMultiplier());
        }


        public float GetMultiplier()
        {
            return (Info.MultiplierBase + (Level * Info.MultiplierBonus));
        }

        public float GetPvPMultiplier()
        {
            return (Info.PvPMultiplierBase + (Level * Info.PvPMultiplierBonus));
        }

        public int GetPower()
        {
            return (int)Math.Round((MPower() / 4F) * (Level + 1) + DefPower());
        }

        public int GetPvPPower()
        {
            return (int)Math.Round((MPvPPower() / 4F) * (Level + 1) + DefPvPPower());
        }

        public int MPower()
        {
            if (Info.MPowerBonus > 0)
            {
                return SMain.Envir.Random.Next(Info.MPowerBase, Info.MPowerBonus + Info.MPowerBase);
            }
            else
                return Info.MPowerBase;
        }

        public int MPvPPower()
        {
            if (Info.PvPPowerBonus > 0)
            {
                return SMain.Envir.Random.Next(Info.PvPMPowerBase, Info.PvPMPowerBonus + Info.PvPMPowerBase);
            }
            else
                return Info.PvPMPowerBase;
        }

        public int DefPower()
        {
            if (Info.PowerBonus > 0)
            {
                return SMain.Envir.Random.Next(Info.PowerBase, Info.PowerBonus + Info.PowerBase);
            }
            else
                return Info.PowerBase;
        }


        public int DefPvPPower()
        {
            if (Info.PvPPowerBonus > 0)
            {
                return SMain.Envir.Random.Next(Info.PvPPowerBase, Info.PvPPowerBonus + Info.PvPPowerBase);
            }
            else
                return Info.PvPPowerBase;
        }

        public int GetPower(int power)
        {
            return (int)Math.Round(power / 4F * (Level + 1) + DefPower());
        }

        public int GetPvPPower(int power)
        {
            return (int)Math.Round(power / 4F * (Level + 1) + DefPvPPower());
        }

        public long GetDelay()
        {
            return Info.DelayBase - (Level * Info.DelayReduction);
        }
    }
}
