using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MirDatabase
{
    public class AI_Customiser
    {
        public int MobIndex;
        public List<AI_Type> AttackTypes = new List<AI_Type>();
        public bool IgnorePetDamage;
        public string AIPath = @".\AI.dat";
        public AI_Customiser()
        {

        }

        public List<AI_Customiser> LoadAI_Customiser()
        {
            if (!File.Exists(AIPath))
                SaveAI();
            List<AI_Customiser> types = new List<AI_Customiser>();
            using (FileStream stream = File.OpenRead(AIPath))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int Mobcount = reader.ReadInt32();
                for (int x = 0; x < Mobcount; x++)
                {
                    AI_Customiser tmp = new AI_Customiser
                    {
                        MobIndex = reader.ReadInt32(),
                        IgnorePetDamage = reader.ReadBoolean()
                    };
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        tmp.AttackTypes.Add(new AI_Type(reader));
                    }
                    types.Add(tmp);
                }
            }
            return types;
        }

        public void SaveAI()
        {
            using (FileStream stream = File.Create(AIPath))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write((int)0);
            }
        }

        public void SaveAI(List<AI_Customiser> types)
        {
            if (types == null)
                return;
            using (FileStream stream = File.Create(AIPath))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(types.Count);
                for (int x = 0; x < types.Count; x++)
                {
                    writer.Write(types[x].MobIndex);
                    writer.Write(types[x].AttackTypes.Count);
                    for (int i = 0; i < AttackTypes.Count; i++)
                        types[x].AttackTypes[i].SaveAI(writer);
                }             
            }
        }
    }

    public class AI_Type
    {
        public bool Melee, Range;
        public bool SingleHit, MultipleHit, AOEHit;
        public byte HitRange;
        public bool IgnorePet;
        public MirClass TargetClass;
        public MirClass ClassDamageBonus;
        public byte DamageBonusPet, DamageBonusClass;
        public byte EffectType;
        public bool AnnounceDrop;
        public string DropMessage;
        public long AttkTime;

        public AI_Type() { }

        public AI_Type(BinaryReader reader)
        {
            Melee = reader.ReadBoolean();
            Range = reader.ReadBoolean();
            SingleHit = reader.ReadBoolean();
            MultipleHit = reader.ReadBoolean();
            AOEHit = reader.ReadBoolean();
            HitRange = reader.ReadByte();
            IgnorePet = reader.ReadBoolean();
            TargetClass = (MirClass)reader.ReadByte();
            ClassDamageBonus = (MirClass)reader.ReadByte();
            DamageBonusPet = reader.ReadByte();
            DamageBonusClass = reader.ReadByte();
            EffectType = reader.ReadByte();
            AnnounceDrop = reader.ReadBoolean();
            DropMessage = reader.ReadString();
            AttkTime = reader.ReadInt64();
        }

        public void SaveAI(BinaryWriter writer)
        {
            writer.Write(Melee);
            writer.Write(Range);
            writer.Write(SingleHit);
            writer.Write(MultipleHit);
            writer.Write(AOEHit);
            writer.Write(HitRange);
            writer.Write(IgnorePet);
            writer.Write((byte)TargetClass);
            writer.Write((byte)ClassDamageBonus);
            writer.Write(DamageBonusPet);
            writer.Write(DamageBonusClass);
            writer.Write(EffectType);
            writer.Write(AnnounceDrop);
            writer.Write(DropMessage);
            writer.Write(AttkTime);
        }
    }
}
