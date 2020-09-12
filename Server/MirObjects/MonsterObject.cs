using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Server.Custom;
using Server.MirDatabase;
using Server.MirEnvir;
using Server.MirObjects.Monsters;
using S = ServerPackets;

namespace Server.MirObjects
{
    public class MonsterObject : MapObject
    {
        public static MonsterObject GetMonster(MonsterInfo info)
        {
            if (info == null) return null;

            switch (info.AI)
            {
                case 1:
                case 2:
                    return new Deer(info);
                case 3:
                    return new Tree(info);
                case 4:
                    return new SpittingSpider(info);
                case 5:
                    return new CannibalPlant(info);
                case 6:
                    return new Guard(info);
                case 7:
                    return new CaveMaggot(info);
                case 8:
                    return new AxeSkeleton(info);
                case 9:
                    return new HarvestMonster(info);
                case 10:
                    return new FlamingWooma(info);
                case 11:
                    return new WoomaTaurus(info);
                case 12:
                    return new BugBagMaggot(info);
                case 13:
                    return new RedMoonEvil(info);
                case 14:
                    return new EvilCentipede(info);
                case 15:
                    return new ZumaMonster(info);
                case 16:
                    return new RedThunderZuma(info);
                case 17:
                    return new ZumaTaurus(info);
                case 18:
                    return new Shinsu(info);
                case 19:
                    return new KingScorpion(info);
                case 20:
                    return new DarkDevil(info);
                case 21:
                    return new IncarnatedGhoul(info);
                case 22:
                    return new IncarnatedZT(info);
                case 23:
                    return new BoneFamiliar(info);
                case 24:
                    return new DigOutZombie(info);
                case 25:
                    return new RevivingZombie(info);
                case 26:
                    return new ShamanZombie(info);
                case 27:
                    return new Khazard(info);
                case 28:
                    return new ToxicGhoul(info);
                case 29:
                    return new BoneSpearman(info);
                case 30:
                    return new BoneLord(info);
                case 31:
                    return new RightGuard(info);
                case 32:
                    return new LeftGuard(info);
                case 33:
                    return new MinotaurKing(info);
                case 34:
                    return new FrostTiger(info);
                case 35:
                    return new SandWorm(info);
                case 36:
                    return new Yimoogi(info);
                case 37:
                    return new CrystalSpider(info);
                case 38:
                    return new HolyDeva(info);
                case 39:
                    return new RootSpider(info);
                case 40:
                    return new BombSpider(info);
                case 41:
                case 42:
                    return new YinDevilNode(info);
                case 43:
                    return new OmaKing(info);
                case 44:
                    return new BlackFoxman(info);
                case 45:
                    return new RedFoxman(info);
                case 46:
                    return new WhiteFoxman(info);
                case 47:
                    return new TrapRock(info);
                case 48:
                    return new GuardianRock(info);
                case 49:
                    return new ThunderElement(info);
                case 50:
                    return new GreatFoxSpirit(info);
                case 51:
                    return new HedgeKekTal(info);
                case 52:
                    return new EvilMir(info);
                case 53:
                    return new EvilMirBody(info);
                case 54:
                    return new DragonStatue(info);
                case 55:
                    return new HumanWizard(info);
                case 56:
                    return new Trainer(info);
                case 57:
                    return new TownArcher(info);
                case 58:
                    return new Guard(info);
                case 59:
                    return new HumanAssassin(info);
                case 60:
                    return new VampireSpider(info);
                case 61:
                    return new SpittingToad(info);
                case 62:
                    return new SnakeTotem(info);
                case 63:
                    return new CharmedSnake(info);
                case 64:
                    return new IntelligentCreatureObject(info);
                case 65:
                    return new MutatedManworm(info);
                case 66:
                    return new CrazyManworm(info);
                case 67:
                    return new DarkDevourer(info);
                case 68:
                    return new Football(info);
                case 69:
                    return new PoisonHugger(info);
                case 70:
                    return new Hugger(info);
                case 71:
                    return new Behemoth(info);
                case 72:
                    return new FinialTurtle(info);
                case 73:
                    return new TurtleKing(info);
                case 74:
                    return new LightTurtle(info);
                case 75:
                    return new WitchDoctor(info);
                case 76:
                    return new HellSlasher(info);
                case 77:
                    return new HellPirate(info);
                case 78:
                    return new HellCannibal(info);
                case 79:
                    return new HellKeeper(info);
                case 80:
                    return new ConquestArcher(info);
                case 81:
                    return new Gate(info);
                case 82:
                    return new Wall(info);
                case 83:
                    return new Tornado(info);
                case 84:
                    return new WingedTigerLord(info);
                case 85:
                    return new WedgeMoth(info);
                case 86:
                    return new ManectricClaw(info);
                case 87:
                    return new ManectricBlest(info);
                case 88:
                    return new ManectricKing(info);
                case 89:
                    return new IcePillar(info);
                case 90:
                    return new TrollBomber(info);
                case 91:
                    return new TrollKing(info);
                case 92:
                    return new FlameSpear(info);
                case 93:
                    return new FlameMage(info);
                case 94:
                    return new FlameScythe(info);
                case 95:
                    return new FlameAssassin(info);
                case 96:
                    return new FlameQueen(info);
                case 97:
                    return new HellKnight(info);
                case 98:
                    return new HellLord(info);
                case 99:
                    return new HellBomb(info);
                case 100:
                    return new SuperShinsu(info);
                case 101:
                    return new FrozenFighter(info);
                case 102:
                    return new FrozenMiner(info);
                case 103:
                    return new FrozenAxeman(info);
                case 104:
                    return new FrozenMagician(info);
                case 105:
                    return new OrcCommander(info);
                case 106: 
                    return new OrcMutant(info);
                case 107:
                    return new OrcGeneral(info);
                case 108:
                    return new SandDragon(info);
                case 109:
                    return new OrcWizard(info);
                case 110:
                    return new OrcWithAnimal(info);
                case 111:
                    return new TucsonMage(info);
                case 112:
                    return new TucsonWarrior(info);
                case 113:
                    return new Armadilo(info);
                case 114:
                    return new ArmadiloElder(info);
                case 115:
                    return new SandSnail(info);
                case 116:
                    return new CannibalTentacles(info);
                case 117:
                    return new TucsonGeneral(info);
                case 118:
                    return new Jar1(info);
                case 119:
                    return new Jar2(info);
                case 120:
                    return new RestlessJar(info);
                case 121:
                    return new BeastKing(info);
                case 122:
                    return new TrollStoner(info);
                case 123:
                    return new SnowFlowerQueen(info);
                case 124:
                    return new SnowFlower(info);
                case 125:
                    return new SnowMouse(info);

                case 150:
                    return new OmaSlasher(info);
                case 151:
                    return new OmaBlest(info);
                case 152:
                    return new OmaCannibal(info);
                case 153:
                    return new OmaAssassin(info);
                case 154:
                    return new OmaMage(info);
                case 155:
                //return new OmaWitchDoctor(info);
                case 156:
                    return new LightningBead(info);
                case 157:
                    return new HealingBead(info);
                case 158:
                    return new PowerUpBead(info);
                case 159:
                    return new DarkOmaKing(info);

                case 160:
                    return new Lord(info);
                case 161:
                    return new BlackDragon_Mob(info);
                case 162:
                    return new FalconLord(info);
                case 163:
                    return new BearMinotaurLrod(info);
                case 164:
                    return new Taganda(info);//MC (wave) DC (swing & Normal)
                case 165:
                    return new NumaMage(info);//MC
                case 166:
                    return new CursedCactus(info);//DC
                case 253:
                    return new FlamingMutant(info);
                case 254:
                    return new StoningStatue(info);


                case 200://custom
                    return new Runaway(info);
                case 201://custom
                    return new TalkingMonster(info);                    
                case 202:
                    return new HeroObject(info);
                case 203:
                    return new HolyDragon(info);
                case 204:
                    return new HolyDragon1(info);
                case 205:
                    return new NewIncarnatedZT(info);
                case 206:
                    return new HolyDeva1(info);

                #region Done by Pete107
                case 180:
                    return new DarkWraith(info);
                case 181:
                    return new DarkSpirit(info);
                case 182:
                    return new CrystalBeast(info);
                case 183:
                    return new FlyingStatue(info);
                case 184:
                    return new StrayCat(info);
                case 185:
                    return new BlackHammerCat(info);
                case 186:
                    return new CatShaman(info);
                case 187:
                    return new IceGuard(info);
                case 188:
                    return new ElementGuard(info);
                case 189:
                    return new KingGuard(info);
                case 190:
                    return new HumanMonster(info);
                case 191:
                    return new MirKing(info);
                //  08/12/2017

                case 240:// Test mob for testing effects etc
                    return new PlainMob(info);
                case 241:
                    return new DebuffMob0(info);
                case 244:
                    return new Mob01(info);
                #endregion
                /*
            case 255:
                return new AICustom(info);
                */
                default:
                    return new MonsterObject(info);
            }
        }

        public override ObjectType Race
        {
            get { return ObjectType.Monster; }
        }
        public bool IsRetreating = false;
        public Point OriginLocation;
        public MonsterInfo Info;
        public MapRespawn Respawn;
        public AI_Customiser CustomAI;
        public override string Name
        {
            get { return Master == null ? Info.GameName : string.Format("{0}({1})", Info.GameName, Master.Name); }
            set { throw new NotSupportedException(); }
        }

        public override int CurrentMapIndex { get; set; }
        public override Point CurrentLocation { get; set; }
        public override sealed MirDirection Direction { get; set; }
        public override ushort Level
        {
            get { return Info.Level; }
            set { throw new NotSupportedException(); }
        }

        public override sealed AttackMode AMode
        {
            get
            {
                return base.AMode;
            }
            set
            {
                base.AMode = value;
            }
        }
        public override sealed PetMode PMode
        {
            get
            {
                return base.PMode;
            }
            set
            {
                base.PMode = value;
            }
        }

        protected bool Ranged
        {
            get
            {
                return Target == null ? false : (CurrentLocation == Target.CurrentLocation || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1));
            }
        }

        public override uint Health
        {
            get { return HP; }
        }

        public override uint MaxHealth
        {
            get { return MaxHP; }
        }

        public uint HP, MaxHP;
        public ushort MoveSpeed;

        public virtual uint Experience 
        { 
            
            get { return Math.Min(uint.MaxValue, PetLevel > 0 && Info.CanBeElite && Master == null ? (uint)(Info.Experience * Settings.EliteExpBoost[PetLevel - 1]) / 100 : Info.Experience); }
        }
        public int DeadDelay
        {
            get
            {
                switch (Info.AI)
                {
                    case 81:
                    case 82:
                        return int.MaxValue;
                    case 252:
                        return 5000;
                    default:
                        return Settings.DeadTimeDelay;
                }
            }
        }

        public const int RegenDelay = 10000, EXPOwnerDelay = 3000, SearchDelay = 3000, RoamDelay = 1000, HealDelay = 600, RevivalDelay = 2000;
        public long ActionTime, MoveTime, AttackTime, RegenTime, DeadTime, SearchTime, RoamTime, HealTime;
        public long ShockTime, RageTime, HallucinationTime;
        public bool BindingShotCenter, PoisonStopRegen = true;

        public byte PetLevel;
        public uint PetExperience;
        public byte MaxPetLevel;
        public long TameTime;

        public int RoutePoint;
        public bool Waiting;

        public bool IsRaidBoss = false;
        public RaidMap Raid;

        public List<MonsterObject> SlaveList = new List<MonsterObject>();
        public List<RouteInfo> Route = new List<RouteInfo>();
        public List<PlayerObject> Contributers = new List<PlayerObject>();
        public override bool Blocking
        {
            get
            {
                return !Dead;
            }
        }
        protected virtual bool CanRegen
        {
            get { return Envir.Time >= RegenTime; }
        }
        protected virtual bool CanMove
        {
            get
            {
                return !Dead && Envir.Time > MoveTime && Envir.Time > ActionTime && Envir.Time > ShockTime &&
                       (Master == null || Master.PMode == PetMode.MoveOnly || Master.PMode == PetMode.Both) && !CurrentPoison.HasFlag(PoisonType.Paralysis)
                       && !CurrentPoison.HasFlag(PoisonType.LRParalysis) &&  !CurrentPoison.HasFlag(PoisonType.Frozen) && !CurrentPoison.HasFlag(PoisonType.Trap);
            }
        }
        protected virtual bool CanAttack
        {
            get
            {
                return !Dead && Envir.Time > AttackTime && Envir.Time > ActionTime &&
                     (Master == null || Master.PMode == PetMode.AttackOnly || Master.PMode == PetMode.Both || !CurrentMap.Info.NoFight) && !CurrentPoison.HasFlag(PoisonType.Paralysis)
                       && !CurrentPoison.HasFlag(PoisonType.LRParalysis) && !CurrentPoison.HasFlag(PoisonType.Stun) && !CurrentPoison.HasFlag(PoisonType.Frozen);
            }
        }

        protected internal MonsterObject(MonsterInfo info)
        {
            Info = info;

            Undead = Info.Undead;
            AutoRev = info.AutoRev;
            CoolEye = info.CoolEye > Envir.Random.Next(100);
            Direction = (MirDirection)Envir.Random.Next(8);

            AMode = AttackMode.All;
            PMode = PetMode.Both;            
            RegenTime = Envir.Random.Next(RegenDelay) + Envir.Time;
            SearchTime = Envir.Random.Next(SearchDelay) + Envir.Time;
            RoamTime = Envir.Random.Next(RoamDelay) + Envir.Time;
            
        }
        public virtual bool Spawn(Map temp, Point location)
        {
            if (!temp.ValidPoint(location)) return false;

            CurrentMap = temp;
            CurrentLocation = location;

            CurrentMap.AddObject(this);

            #region Elite Mobs
            if (Info.CanBeElite)
            {
                MaxPetLevel = 8;
                if (Envir.Random.Next(100) <= Settings.ChanceToBeElite)
                {
                    PetLevel = 1;
                    //8%
                    if (Envir.Random.Next(0, 10000) <= Settings.EliteChances[6])
                        PetLevel = 8;
                    else if (Envir.Random.Next(0, 10000) <= Settings.EliteChances[5])
                        PetLevel = 7;
                    else if (Envir.Random.Next(0, 10000) <= Settings.EliteChances[4])
                        PetLevel = 6;
                    else if (Envir.Random.Next(0, 10000) <= Settings.EliteChances[3])
                        PetLevel = 5;
                    else if (Envir.Random.Next(0, 10000) <= Settings.EliteChances[2])
                        PetLevel = 4;
                    else if (Envir.Random.Next(0, 10000) <= Settings.EliteChances[1])
                        PetLevel = 3;
                    else if (Envir.Random.Next(0, 10000) <= Settings.EliteChances[0])
                        PetLevel = 2;
                    short tempBoost = 0;
                    RefreshAll();
                    switch (PetLevel)
                    {
                        case 1:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel1Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                        case 2:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel2Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                        case 3:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel3Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                        case 4:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel4Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                        case 5:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel5Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                        case 6:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel6Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                        case 7:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel7Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                        case 8:
                            tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel8Bonus / 100);
                            MaxHP += (uint)tempBoost;
                            SetHP(MaxHP);
                            break;
                    }
                }
                else
                {
                    RefreshAll();
                    SetHP(MaxHP);
                }
            }
            #endregion
            else
            {
                RefreshAll();
                SetHP(MaxHP);
            }
            Spawned();
            Envir.MonsterCount++;
            CurrentMap.MonsterCount++;
            
            return true;
        }
        public bool Spawn(MapRespawn respawn)
        {
            Respawn = respawn;

            if (Respawn.Map == null) return false;

            for (int i = 0; i < 8; i++)
            {
                CurrentLocation = new Point(Respawn.Info.Location.X + Envir.Random.Next(-Respawn.Info.Spread, Respawn.Info.Spread + 1),
                                            Respawn.Info.Location.Y + Envir.Random.Next(-Respawn.Info.Spread, Respawn.Info.Spread + 1));

                if (!respawn.Map.ValidPoint(CurrentLocation)) continue;

                respawn.Map.AddObject(this);

                CurrentMap = respawn.Map;

                if (Respawn.Route.Count > 0)
                    Route.AddRange(Respawn.Route);

                #region Elite Mobs
                if (Info.CanBeElite)
                {
                    MaxPetLevel = 8;
                    if (Envir.Random.Next(100) <= Settings.ChanceToBeElite)
                    {
                        PetLevel = (byte)Envir.Random.Next(1, 7);
                        short tempBoost = 0;
                        MaxHP = Info.HP;
                        RefreshAll();
                        MaxHP = Info.HP;
                        switch (PetLevel)
                        {
                            case 1:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel1Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                            case 2:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel2Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                            case 3:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel3Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                            case 4:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel4Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                            case 5:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel5Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                            case 6:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel6Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                            case 7:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel7Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                            case 8:
                                tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel8Bonus / 100);
                                MaxHP += (uint)tempBoost;
                                SetHP(MaxHP);
                                break;
                        }
                    }
                    else
                    {
                        RefreshAll();
                        SetHP(MaxHP);
                    }
                }
                #endregion
                else
                {
                    RefreshAll();
                    SetHP(MaxHP);
                }
                Spawned();
                Respawn.Count++;
                respawn.Map.MonsterCount++;
                Envir.MonsterCount++;
                return true;
            }
            return false;
        }
        public bool IsRetreatingMob
        {
            get { return Info.IsRetreatMob || IsPublicEvenetMob; }
        }
        public bool IsPublicEvenetMob
        {
            get { return Respawn != null && Respawn.IsEventObjective && Respawn.Event != null; }
        }
        public override void Spawned()
        {
            base.Spawned();
            OriginLocation = CurrentLocation;

            ActionTime = Envir.Time + 2000;
            if (Info.HasSpawnScript && (SMain.Envir.MonsterNPC != null))
            {
                SMain.Envir.MonsterNPC.Call(this,string.Format("[@_SPAWN({0})]", Info.Index));
            }
        }

        protected virtual void RefreshBase(bool refreshHP = false)
        {
            if (refreshHP)
                MaxHP = Info.HP;
            MinAC = Info.MinAC;
            MaxAC = Info.MaxAC;
            MinMAC = Info.MinMAC;
            MaxMAC = Info.MaxMAC;
            MinDC = Info.MinDC;
            MaxDC = Info.MaxDC;
            MinMC = Info.MinMC;
            MaxMC = Info.MaxMC;
            MinSC = Info.MinSC;
            MaxSC = Info.MaxSC;
            Accuracy = Info.Accuracy;
            Agility = Info.Agility;

            MoveSpeed = Info.MoveSpeed;
            AttackSpeed = Info.AttackSpeed;
        }
        public virtual void RefreshAll()
        {
            bool refresh = true;
            if (PetLevel > 0 && Info.CanBeElite)
                refresh = false;
            else
                refresh = true;
            RefreshBase(refresh);

            #region Elite Mobs
            if (PetLevel > 0 && Info.CanBeElite)
            {
                //SMain.EnqueueDebugging(string.Format("{11} Before DC {0}-{1} MC {2}-{3} SC {4}-{5} AC {6}-{7} MAC {8}-{9} HP {10}", MinDC, MaxDC, MinMC, MaxMC, MinSC, MaxSC, MinAC, MaxAC, MinMAC, MaxMAC, MaxHP, Name));
                MaxHP = Info.HP;
                uint tempsBoost = 0;
                short tempBoost = 0;
                switch (PetLevel)
                {
                    case 1:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel1Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel1Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                    case 2:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel2Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel2Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                    case 3:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel3Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel3Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                    case 4:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel4Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel4Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                    case 5:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel5Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel5Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                    case 6:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel6Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel6Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                    case 7:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel7Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel7Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                    case 8:
                        tempBoost = (short)Math.Min(short.MaxValue, MaxHP * Settings.EliteLevel8Bonus / 100);
                        MaxHP += (uint)tempBoost;
                        //SetHP(MaxHP);
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinDC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinDC * 100 / 100);
                        MinDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxDC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxDC * 100 / 100);
                        MaxDC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMC * 100 / 100);
                        MinMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMC * 100 / 100);
                        MaxMC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinSC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinSC * 100 / 100);
                        MinSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxSC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxSC * 100 / 100);
                        MaxSC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinAC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinAC * 100 / 100);
                        MinAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxAC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxAC * 100 / 100);
                        MaxAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MinMAC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MinMAC * 100 / 100);
                        MinMAC += (ushort)tempsBoost;
                        tempsBoost = (uint)Math.Min(uint.MaxValue, MaxMAC * Settings.EliteLevel8Bonus / 100);
                        //tempsBoost = Convert.ToUInt32(MaxMAC * 100 / 100);
                        MaxMAC += (ushort)tempsBoost;
                        break;
                }
                //SMain.EnqueueDebugging(string.Format("{11} After DC {0}-{1} MC {2}-{3} SC {4}-{5} AC {6}-{7} MAC {8}-{9} HP {10}", MinDC, MaxDC, MinMC, MaxMC, MinSC, MaxSC, MinAC, MaxAC, MinMAC, MaxMAC, MaxHP, Name));

            }
            #endregion
            else
            {
                
                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + PetLevel * 20);
                MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + PetLevel * 2);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + PetLevel * 2);
                MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + PetLevel * 2);
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + PetLevel * 2);
                MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + PetLevel);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + PetLevel);
            }
            if (Info.Name == Settings.SkeletonName ||
                Info.Name == Settings.ShinsuName ||
                Info.Name == Settings.AngelName ||
                Info.Name == Settings.SuperSkeletonName1 ||
                Info.Name == Settings.SuperSkeletonName2 ||
                Info.Name == Settings.SuperSkeletonName3 ||
                Info.Name == Settings.SuperShinsuName ||
                Info.Name == Settings.SuperShinsuName2 ||
                Info.Name == Settings.SuperShinsuName4 ||
                Info.Name == Settings.DragonName ||
                Info.Name == Settings.DragonName1)
            {
                MoveSpeed = (ushort)Math.Min(ushort.MaxValue, ( Math.Max(ushort.MinValue, MoveSpeed - MaxPetLevel * 130) ));
                AttackSpeed = (ushort)Math.Min(ushort.MaxValue, ( Math.Max(ushort.MinValue, AttackSpeed - MaxPetLevel * 70) ));
            }

            if (MoveSpeed < 400)
                MoveSpeed = 400;
            if (AttackSpeed < 400)
                AttackSpeed = 400;
            RefreshBuffs();
        }
        protected virtual void RefreshBuffs()
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                if (Buffs[i] == null) return;

                Buff buff = Buffs[i];              

                if (buff.Values == null || buff.Values.Length < 1) continue;

                switch (buff.Type)
                {
                    case BuffType.Haste:
                        ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + buff.Values[0])));
                        break;
                    case BuffType.SwiftFeet:
                        MoveSpeed = (ushort)Math.Max(ushort.MinValue, MoveSpeed + 100 * buff.Values[0]);
                        break;
                    case BuffType.LightBody:
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + buff.Values[0]);
                        break;
                    case BuffType.SoulShield:
                        if (Envir.Debug)
                            SMain.Enqueue(string.Format("Max MAC Before {0} : {1}", BuffType.SoulShield.ToString(), MaxMAC));
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[0]);
                        if (Envir.Debug)
                            SMain.Enqueue(string.Format("Max MAC After {0} : {1}", BuffType.SoulShield.ToString(), MaxMAC));
                        break;
                    case BuffType.BlessedArmour:
                        if (Envir.Debug)
                            SMain.Enqueue(string.Format("Max AC Before {0} : {1}", BuffType.SoulShield.ToString(), MaxAC));
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[0]);
                        if (Envir.Debug)
                            SMain.Enqueue(string.Format("Max AC After {0} : {1}", BuffType.SoulShield.ToString(), MaxAC));
                        break;
                    case BuffType.UltimateEnhancer:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[0]);
                        break;
                    case BuffType.Exp:
                        if (Race == ObjectType.Hero)
                            ((HeroObject)this).ExpRateOffset = (float)Math.Min(float.MaxValue, ((HeroObject)this).ExpRateOffset + buff.Values[0]);
                        break;
                    case BuffType.Drop:
                        ItemDropRateOffset = (float)Math.Min(float.MaxValue, ItemDropRateOffset + buff.Values[0]);
                        break;

                    case BuffType.Curse:
                        ushort rMaxDC = (ushort)(((int)MaxDC / 100) * buff.Values[0]);
                        ushort rMaxMC = (ushort)(((int)MaxMC / 100) * buff.Values[0]);
                        ushort rMaxSC = (ushort)(((int)MaxSC / 100) * buff.Values[0]);
                        sbyte rASpeed = (sbyte)(((int)ASpeed / 100) * buff.Values[0]);
                        ushort rMSpeed = (ushort)((MoveSpeed / 100) * buff.Values[0]);

                        MaxDC = (ushort)Math.Max(ushort.MinValue, MaxDC - rMaxDC);
                        MaxMC = (ushort)Math.Max(ushort.MinValue, MaxMC - rMaxMC);
                        MaxSC = (ushort)Math.Max(ushort.MinValue, MaxSC - rMaxSC);
                        ASpeed = (sbyte)Math.Min(sbyte.MaxValue, (Math.Max(sbyte.MinValue, ASpeed - rASpeed)));
                        MoveSpeed = (ushort)Math.Max(ushort.MinValue, MoveSpeed - rMSpeed);
                        break;

                    case BuffType.PetEnhancer:                        
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + buff.Values[0]);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[0]);
                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + buff.Values[1]);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[1]);
                        break;
                    case BuffType.MobDebuff:
                        HP = (uint)Math.Min(uint.MaxValue, HP + buff.Values[0]);
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + buff.Values[1]);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[1]);

                        MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + buff.Values[2]);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + buff.Values[2]);

                        MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + buff.Values[3]);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + buff.Values[3]);

                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + buff.Values[4]);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[4]);

                        MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + buff.Values[5]);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[5]);
                        break;
                }

            }
        }
        public virtual void RefreshNameColour(bool send = true)
        {
            if (ShockTime < Envir.Time) BindingShotCenter = false;

            Color colour = Color.White;
            
            switch (PetLevel)
            {
                case 1:
                    colour = Color.Aqua;
                    break;
                case 2:
                    colour = Color.Aquamarine;
                    break;
                case 3:
                    colour = Color.LightSeaGreen;
                    break;
                case 4:
                    colour = Color.SlateBlue;
                    break;
                case 5:
                    colour = Color.SteelBlue;
                    break;
                case 6:
                    colour = Color.Blue;
                    break;
                case 7:
                    colour = Color.Navy;
                    break;
                case 8:
                    colour = Color.Red;
                    break;
            }

            if (Envir.Time < ShockTime)
                colour = Color.Peru;
            else if (Envir.Time < RageTime)
                colour = Color.Red;
            else if (Envir.Time < HallucinationTime)
                colour = Color.MediumOrchid;

            if (colour == NameColour || !send) return;

            NameColour = colour;

            Broadcast(new S.ObjectColourChanged { ObjectID = ObjectID, NameColour = NameColour });
        }

        public void SetHP(uint amount)
        {
            if (HP == amount) return;

            HP = amount <= MaxHP ? amount : MaxHP;

            if (!Dead && HP == 0) Die();

            //  HealthChanged = true;
            BroadcastHealthChange();
        }
        public virtual void ChangeHP(int amount)
        {

            uint value = (uint)Math.Max(uint.MinValue, Math.Min(MaxHP, HP + amount));

            if (value == HP) return;

            HP = value;
            /*
            if (!Dead && HP > 0)
                CurrentMap.Broadcast(new S.MobHealthChanged
                {
                    ObjectID = ObjectID,
                    HP = HP,
                    Boss = Info.IsBoss,
                    Pet = Master == null ? false : true,
                }, CurrentLocation);
                */
            if (!Dead && HP == 0) Die();

           // HealthChanged = true;
            BroadcastHealthChange();
            
        }

        //use this so you can have mobs take no/reduced poison damage
        public virtual void PoisonDamage(int amount, MapObject Attacker)
        {
            ChangeHP(amount);
        }


        public override bool Teleport(Map temp, Point location, bool effects = true, byte effectnumber = 0)
        {
            if (temp == null || !temp.ValidPoint(location)) return false;

            CurrentMap.RemoveObject(this);
            if (effects) Broadcast(new S.ObjectTeleportOut { ObjectID = ObjectID, Type = effectnumber });
            Broadcast(new S.ObjectRemove { ObjectID = ObjectID });
            
            CurrentMap.MonsterCount--;

            CurrentMap = temp;
            CurrentLocation = location;
            
            CurrentMap.MonsterCount++;

            InTrapRock = false;

            CurrentMap.AddObject(this);
            BroadcastInfo();

            if (effects) Broadcast(new S.ObjectTeleportIn { ObjectID = ObjectID, Type = effectnumber });

            BroadcastHealthChange();

            return true;
        }

        public bool hasDieScriptTriggered = false;
        public override void Die()
        {
            if (Dead) return;           
            HP = 0;
            Dead = true;
            if (IsRaidBoss && Raid != null)
            {
                if (Info.Index != Raid.Info.BossIndex)
                { 
                    MonsterInfo mInfo = Envir.GetMonsterInfo(Info.Index == Raid.Info.Sub0Index ? Raid.Info.Sub1Index : Raid.Info.Sub1Index == Info.Index ? Raid.Info.BossIndex : -1);
                    if (mInfo != null)
                    {
                        MonsterObject mob = GetMonster(mInfo);
                        if (mob != null)
                        {
                            mob.Direction = MirDirection.DownRight;
                            mob.ActionTime = Envir.Time + 300;
                            bool spawned = mob.Spawn(CurrentMap, Raid.Info.BossAreas[Info.Index == Raid.Info.Sub0Index ? 1 : 2]);
                            if (!spawned)
                            {
                                for (int x = Raid.Info.BossAreas[Info.Index == Raid.Info.Sub0Index ? 1 : 2].X - 5; x < Raid.Info.BossAreas[Info.Index == Raid.Info.Sub0Index ? 1 : 2].X + 5; x++)
                                {
                                    if (spawned)
                                        continue;
                                    for (int y = Raid.Info.BossAreas[Info.Index == Raid.Info.Sub0Index ? 1 : 2].Y - 5; y < Raid.Info.BossAreas[Info.Index == Raid.Info.Sub0Index ? 1 : 2].Y + 5; y++)
                                    {
                                        if (mob.Spawn(CurrentMap, new Point(x, y)))
                                            spawned = true;
                                    }
                                }
                            }
                            if (spawned)
                            {
                                mob.IsRaidBoss = true;
                                mob.Raid = Raid;
                                for (int i = CurrentMap.Players.Count - 1; i >= 0; i--)
                                {
                                    if (CurrentMap.Players[i].Dead)
                                        continue;
                                        CurrentMap.Players[i].ReceiveChat(
                                            string.Format("{0} has been defeated{1}", Info.GameName,
                                            Info.Index == Raid.Info.Sub0Index ?
                                            string.Format(" {0} has spawned at X:{1} Y:{2}", mob.Info.GameName, mob.CurrentLocation.X, mob.CurrentLocation.Y) :
                                            Info.Index == Raid.Info.Sub1Index ?
                                            string.Format(" {0} has spawned at X:{1} Y:{2}", mob.Info.GameName, mob.CurrentLocation.X, mob.CurrentLocation.Y) :
                                            Info.Index == Raid.Info.BossIndex ? string.Format(" all monsters have been pushed back into their pit!") : ""), ChatType.Hint);
                                }
                            }
                        }
                    }
                }
                else if (Info.Index == Raid.Info.BossIndex)
                {
                    //  finish an distibute items
                    Raid.EndRaid(true);
                }
            }
            DeadTime = Envir.Time + DeadDelay;

            Broadcast(new S.ObjectDied { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            if (Info.HasDieScript && (SMain.Envir.MonsterNPC != null) && !hasDieScriptTriggered)
            {
                hasDieScriptTriggered = Dead;
                SMain.Envir.MonsterNPC.Call(this,string.Format("[@_DIE({0})]", Info.Index));
            }

            if (EXPOwner != null && Master == null && EXPOwner.Race == ObjectType.Player)
            {
                EXPOwner.WinExp(Experience, Level);

                PlayerObject playerObj = (PlayerObject)EXPOwner;
                playerObj.CheckGroupQuestKill(Info);

                if (Info.RandomQuest.Count > 0)
                {
                    if (Info.RandomQuestChance > 0 && Envir.Random.Next(Info.RandomQuestChance) == 1)
                    {
                        playerObj.AcceptRandomQuest(Info.RandomQuest[Envir.Random.Next(Info.RandomQuest.Count)]);
                    }
                }
            }
            if (Respawn != null)
            {
                Respawn.Count--;
                if (Respawn.IsEventObjective && Respawn.Event != null)
                    Respawn.Event.EventMonsterDied(Contributers);

                Contributers.Clear();
            }

            if (Master == null && EXPOwner != null)
                 Drop();

            Master = null;

            PoisonList.Clear();
            Envir.MonsterCount--;
            if (CurrentMap != null)
                CurrentMap.MonsterCount--;
        }

        public void Revive(uint hp, bool effect)
        {
            if (!Dead) return;

            SetHP(hp);

            Dead = false;
            ActionTime = Envir.Time + RevivalDelay;

            Broadcast(new S.ObjectRevived { ObjectID = ObjectID, Effect = effect });

            if (Respawn != null)
                Respawn.Count++;

            Envir.MonsterCount++;
            CurrentMap.MonsterCount++;
        }

        public override int Pushed(MapObject pusher, MirDirection dir, int distance)
        {
            if (!Info.CanPush) return 0;
            //if (!CanMove) return 0; //stops mobs that can't move (like cannibalplants) from being pushed

            int result = 0;
            MirDirection reverse = Functions.ReverseDirection(dir);
            for (int i = 0; i < distance; i++)
            {
                Point location = Functions.PointMove(CurrentLocation, dir, 1);

                if (!CurrentMap.ValidPoint(location)) return result;

                Cell cell = CurrentMap.GetCell(location);

                bool stop = false;
                if (cell.Objects != null)
                    for (int c = 0; c < cell.Objects.Count; c++)
                    {
                        MapObject ob = cell.Objects[c];
                        if (!ob.Blocking) continue;
                        stop = true;
                    }
                if (stop) break;

                CurrentMap.GetCell(CurrentLocation).Remove(this);

                Direction = reverse;
                RemoveObjects(dir, 1);
                CurrentLocation = location;
                CurrentMap.GetCell(CurrentLocation).Add(this);
                AddObjects(dir, 1);

                Broadcast(new S.ObjectPushed { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

                result++;
            }

            ActionTime = Envir.Time + 300 * result;
            MoveTime = Envir.Time + 500 * result;

            if (result > 0)
            {
                Cell cell = CurrentMap.GetCell(CurrentLocation);

                for (int i = 0; i < cell.Objects.Count; i++)
                {
                    if (cell.Objects[i].Race != ObjectType.Spell) continue;
                    SpellObject ob = (SpellObject)cell.Objects[i];

                    ob.ProcessSpell(this);
                    //break;
                }
            }

            return result;
        }

        protected virtual void Drop()
        {
            if (!Info.CanBeElite ||
                PetLevel < 1)
            {
                for (int i = 0; i < Info.Drops.Count; i++)
                {
                    DropInfo drop = Info.Drops[i];

                    int rate = (int)(drop.Chance / (Settings.DropRate));

                    if (drop.QuestRequired && Settings.QuestDropRate > 1f)
                    {
                        rate = (int)(rate / (Settings.QuestDropRate));
                    }

                    if (EXPOwner != null && EXPOwner.ItemDropRateOffset > 0)
                        rate -= (int)(rate * (EXPOwner.ItemDropRateOffset / 100));

                    if (Envir.GlobalDrop > 0)
                        rate -= (int)(rate * ((float)(Envir.GlobalDrop) / 100f));

                    if (rate < 1) rate = 1;

                    if (Envir.Random.Next(rate) != 0) continue;

                    if (drop.Gold > 0)
                    {
                        int lowerGoldRange = (int)(drop.Gold / 2);
                        int upperGoldRange = (int)(drop.Gold + drop.Gold / 2);

                        if (EXPOwner != null && EXPOwner.GoldDropRateOffset > 0)
                            lowerGoldRange += (int)(lowerGoldRange * (EXPOwner.GoldDropRateOffset / 100));

                        if (Envir.GlobalGold > 0)
                            lowerGoldRange += (int)(lowerGoldRange * (float)(Envir.GlobalGold / 100));


                        if (lowerGoldRange > upperGoldRange) lowerGoldRange = upperGoldRange;

                        int gold = Envir.Random.Next(lowerGoldRange, upperGoldRange);

                        if (gold <= 0) continue;

                        if (!DropGold((uint)gold)) return;
                    }
                    else
                    {
                        UserItem item = Envir.CreateDropItem(drop.Item);
                        if (item == null) continue;

                        if (EXPOwner != null && EXPOwner.Race == ObjectType.Player)
                        {
                            PlayerObject ob = (PlayerObject)EXPOwner;

                            if (ob.CheckGroupQuestItem(item))
                            {
                                continue;
                            }
                        }

                        if (drop.QuestRequired) continue;
                        if (!DropItem(item)) return;
                    }
                }
            }
            #region Elite Mob Drop
            //  Check if the monster dropping items can be an elite first and then check if it is an elite.
            if (Info.CanBeElite && PetLevel > 0)    //  Same
            {
                //SMain.EnqueueDebugging(string.Format("{0} is elite - Drop Count {1}", Name, Info.EliteDrops.Count));
                //  Cycle through the elite drops.
                for (int i = 0; i < Info.EliteDrops.Count; i++)
                {
                    //  Assign the drop to a variable defined here
                    EliteDropInfo drop = Info.EliteDrops[i];
                    if (drop.EliteLevel != PetLevel)
                        continue;
                    //  Get the drop chance
                    int rate = (int)( drop.Chance / ( Settings.DropRate ) );

                    //  Get the exp owner & their drop rate increase.
                    if (EXPOwner != null && EXPOwner.ItemDropRateOffset > 0)
                        rate -= (int)( rate * ( EXPOwner.ItemDropRateOffset / 100 ) );

                    //  Elite Drop rate increase
                    int incRate = rate * Settings.EliteDropRate[PetLevel - 1] / 100;
                    rate -= incRate;

                    //  If it's less than 1 it's never gong to drop
                    if (rate < 1)
                        rate = 1;

                    //  Ensure drop rate isn't 0 (aka can't drop nothing!)
                    if (Envir.Random.Next(rate) != 0)
                    {
                        
                        continue;
                    }
                    //SMain.EnqueueDebugging(string.Format("Dropping item... "));
                    //  Check the gold is more than 0
                    if (drop.Gold > 0)
                    {

                        int lowerGoldRange = (int)( drop.Gold / 2 );
                        int upperGoldRange = (int)( drop.Gold + drop.Gold / 2 );

                        if (EXPOwner != null && EXPOwner.GoldDropRateOffset > 0)
                            lowerGoldRange += (int)( lowerGoldRange * ( EXPOwner.GoldDropRateOffset / 100 ) );

                        if (lowerGoldRange > upperGoldRange)
                            lowerGoldRange = upperGoldRange;

                        int gold = Envir.Random.Next(lowerGoldRange, upperGoldRange);

                        if (gold <= 0)
                            continue;

                        if (!DropGold((uint)gold))
                            return;
                    }
                    else
                    {
                        //SMain.EnqueueDebugging(string.Format("Checking Elite Level {0} vs drop level {1}", drop.EliteLevel, PetLevel));
                        if (drop.EliteLevel == PetLevel)
                        {
                            //SMain.EnqueueDebugging(string.Format("Elite Level {0} vs drop level {1} is correct", drop.EliteLevel, PetLevel));
                            UserItem item = Envir.CreateDropItem(drop.Item);
                            if (item == null)
                            {
                                //SMain.EnqueueDebugging(string.Format("could not create item from drop"));
                                continue;
                                
                            }

                            if (EXPOwner != null && EXPOwner.Race == ObjectType.Player)
                            {

                                
                                PlayerObject ob = (PlayerObject)EXPOwner;
                                if (Info.Index == 653 || Info.Index == 645)
                                {
                                    if (item.Info.Index == 1103 || item.Info.Index == 1301 ||
                                        item.Info.Index == 1510 || item.Info.Index == 1647 ||
                                        item.Info.Index == 1768 || item.Info.Index == 4651 ||
                                        item.Info.Index == 4646 || item.Info.Index == 4641)
                                    {
                                        byte dcInc = 0;
                                        byte mcInc = 0;
                                        byte scInc = 0;
                                        byte acInc = 0;
                                        byte macInc = 0;
                                        byte hpInc = 0;
                                        byte mpInc = 0;
                                        byte accInc = 0;
                                        byte agilInc = 0;
                                        sbyte speedInc = 0;
                                        byte critRateInc = 0;
                                        byte critDmgInc = 0;
                                        //byte reflectInc = Envir.Random.Next(6) == 0 ? (byte)Envir.Random.Next(0, 1) : (byte)0;
                                        //byte drainInc = Envir.Random.Next(6) == 0 ? (byte)Envir.Random.Next(0, 1) : (byte)0;
                                        byte poisonRecovInc = Envir.Random.Next(6) == 0 ? (byte)Envir.Random.Next(0, 1) : (byte)0;
                                        switch (ob.Class)
                                        {
                                            case MirClass.Assassin:
                                                {
                                                    switch(item.Info.Type)
                                                    {
                                                        case ItemType.Weapon:
                                                            {

                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(5, 10);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(3, 8);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(2, 6);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(1, 4);
                                                                accInc += (byte)Envir.Random.Next(0, 4);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    speedInc += (sbyte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Armour:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                acInc += (byte)Envir.Random.Next(2, 5);
                                                                macInc += (byte)Envir.Random.Next(1, 4);
                                                                hpInc += (byte)Envir.Random.Next(0, 100);
                                                                mpInc += (byte)Envir.Random.Next(0, 100);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Helmet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(1, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(0, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 1);
                                                                acInc += (byte)Envir.Random.Next(1, 5);
                                                                macInc += (byte)Envir.Random.Next(1, 3);
                                                            }
                                                            break;
                                                        case ItemType.Necklace:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    speedInc += (sbyte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Bracelet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(10) <= 5)
                                                                {
                                                                    accInc += (byte)Envir.Random.Next(0, 2);
                                                                    agilInc += (byte)Envir.Random.Next(0, 2);
                                                                }
                                                                else
                                                                {
                                                                    acInc += (byte)Envir.Random.Next(2, 4);
                                                                    macInc += (byte)Envir.Random.Next(1, 2);
                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Ring:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    speedInc += (sbyte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                {

                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Belt:
                                                            {

                                                            }
                                                            break;
                                                            {

                                                            }
                                                        case ItemType.Boots:
                                                            break;
                                                    }
                                                }
                                                break;
                                            case MirClass.Warrior:
                                                {
                                                    switch (item.Info.Type)
                                                    {
                                                        case ItemType.Weapon:
                                                            {

                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(4, 8);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(2, 6);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(1, 4);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 3);
                                                                accInc += (byte)Envir.Random.Next(0, 4);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    speedInc += (sbyte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Armour:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 1);
                                                                acInc += (byte)Envir.Random.Next(4, 8);
                                                                macInc += (byte)Envir.Random.Next(3, 5);
                                                                hpInc += (byte)Envir.Random.Next(0, 100);
                                                                mpInc += (byte)Envir.Random.Next(0, 100);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Helmet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(0, 4);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(0, 1);
                                                                acInc += (byte)Envir.Random.Next(1, 5);
                                                                macInc += (byte)Envir.Random.Next(1, 3);
                                                            }
                                                            break;
                                                        case ItemType.Necklace:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    speedInc += (sbyte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Bracelet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(10) <= 5)
                                                                {
                                                                    accInc += (byte)Envir.Random.Next(0, 2);
                                                                    agilInc += (byte)Envir.Random.Next(0, 2);
                                                                }
                                                                else
                                                                {
                                                                    acInc += (byte)Envir.Random.Next(2, 4);
                                                                    macInc += (byte)Envir.Random.Next(1, 2);
                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Ring:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    dcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    dcInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    dcInc += (byte)Envir.Random.Next(0, 2);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    speedInc += (sbyte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                {

                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Belt:
                                                            {

                                                            }
                                                            break;
                                                            {

                                                            }
                                                        case ItemType.Boots:
                                                            break;
                                                    }
                                                }
                                                break;
                                            case MirClass.Wizard:
                                                {
                                                    switch (item.Info.Type)
                                                    {
                                                        case ItemType.Weapon:
                                                            {

                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    mcInc += (byte)Envir.Random.Next(5, 10);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    mcInc += (byte)Envir.Random.Next(3, 8);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    mcInc += (byte)Envir.Random.Next(2, 6);
                                                                else
                                                                    mcInc += (byte)Envir.Random.Next(1, 4);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Armour:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    mcInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    mcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    mcInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    mcInc += (byte)Envir.Random.Next(0, 2);
                                                                acInc += (byte)Envir.Random.Next(2, 5);
                                                                macInc += (byte)Envir.Random.Next(1, 4);
                                                                hpInc += (byte)Envir.Random.Next(0, 100);
                                                                mpInc += (byte)Envir.Random.Next(0, 100);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Helmet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    dcInc += (byte)Envir.Random.Next(1, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    mcInc += (byte)Envir.Random.Next(0, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    mcInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    mcInc += (byte)Envir.Random.Next(0, 1);
                                                                acInc += (byte)Envir.Random.Next(1, 5);
                                                                macInc += (byte)Envir.Random.Next(1, 3);
                                                            }
                                                            break;
                                                        case ItemType.Necklace:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    mcInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    mcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    mcInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    mcInc += (byte)Envir.Random.Next(0, 2);
                                                            }
                                                            break;
                                                        case ItemType.Bracelet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    mcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    mcInc += (byte)Envir.Random.Next(1, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    mcInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    mcInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(10) <= 5)
                                                                {
                                                                    accInc += (byte)Envir.Random.Next(0, 2);
                                                                    agilInc += (byte)Envir.Random.Next(0, 2);
                                                                }
                                                                else
                                                                {
                                                                    acInc += (byte)Envir.Random.Next(2, 4);
                                                                    macInc += (byte)Envir.Random.Next(1, 2);
                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Ring:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    mcInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    mcInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    mcInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    mcInc += (byte)Envir.Random.Next(0, 2);
                                                                if (Envir.Random.Next(6) == 0)
                                                                {

                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Belt:
                                                            {

                                                            }
                                                            break;
                                                            {

                                                            }
                                                        case ItemType.Boots:
                                                            break;
                                                    }
                                                }
                                                break;
                                            case MirClass.Taoist:
                                                {
                                                    switch (item.Info.Type)
                                                    {
                                                        case ItemType.Weapon:
                                                            {

                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    scInc += (byte)Envir.Random.Next(5, 10);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    scInc += (byte)Envir.Random.Next(3, 8);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    scInc += (byte)Envir.Random.Next(2, 6);
                                                                else
                                                                    scInc += (byte)Envir.Random.Next(1, 4);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Armour:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    scInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    scInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    scInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    scInc += (byte)Envir.Random.Next(0, 2);
                                                                acInc += (byte)Envir.Random.Next(2, 5);
                                                                macInc += (byte)Envir.Random.Next(1, 4);
                                                                hpInc += (byte)Envir.Random.Next(0, 100);
                                                                mpInc += (byte)Envir.Random.Next(0, 100);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critRateInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(6) == 0)
                                                                    critDmgInc += (byte)Envir.Random.Next(0, 1);
                                                            }
                                                            break;
                                                        case ItemType.Helmet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    scInc += (byte)Envir.Random.Next(1, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    scInc += (byte)Envir.Random.Next(0, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    scInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    scInc += (byte)Envir.Random.Next(0, 1);
                                                                acInc += (byte)Envir.Random.Next(1, 5);
                                                                macInc += (byte)Envir.Random.Next(1, 3);
                                                            }
                                                            break;
                                                        case ItemType.Necklace:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    scInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    scInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    scInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    scInc += (byte)Envir.Random.Next(0, 2);
                                                            }
                                                            break;
                                                        case ItemType.Bracelet:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    scInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    scInc += (byte)Envir.Random.Next(1, 3);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    scInc += (byte)Envir.Random.Next(0, 2);
                                                                else
                                                                    scInc += (byte)Envir.Random.Next(0, 1);
                                                                if (Envir.Random.Next(10) <= 5)
                                                                {
                                                                    accInc += (byte)Envir.Random.Next(0, 2);
                                                                    agilInc += (byte)Envir.Random.Next(0, 2);
                                                                }
                                                                else
                                                                {
                                                                    acInc += (byte)Envir.Random.Next(2, 4);
                                                                    macInc += (byte)Envir.Random.Next(1, 2);
                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Ring:
                                                            {
                                                                int randy = Envir.Random.Next(10);
                                                                if (randy == 0)
                                                                    scInc += (byte)Envir.Random.Next(3, 5);
                                                                else if (randy >= 1 && randy <= 4)
                                                                    scInc += (byte)Envir.Random.Next(2, 4);
                                                                else if (randy >= 5 && randy <= 8)
                                                                    scInc += (byte)Envir.Random.Next(1, 3);
                                                                else
                                                                    scInc += (byte)Envir.Random.Next(0, 2);
                                                                if (Envir.Random.Next(6) == 0)
                                                                {

                                                                }
                                                            }
                                                            break;
                                                        case ItemType.Belt:
                                                            {

                                                            }
                                                            break;
                                                            {

                                                            }
                                                        case ItemType.Boots:
                                                            break;
                                                    }
                                                }
                                                break;
                                        }

                                        item.HP +=  hpInc;
                                        item.MP += mpInc;
                                        item.DC += dcInc;
                                        item.MC += mcInc;
                                        item.SC += scInc;
                                        item.AC += acInc;
                                        item.MAC += macInc;
                                        item.Accuracy += accInc;
                                        item.Agility += agilInc;
                                        item.AttackSpeed += speedInc;
                                        item.CriticalRate += critRateInc;
                                        item.CriticalDamage += critDmgInc;
                                        item.PoisonRecovery += poisonRecovInc;
                                    }
                                }
                                if (ob.CheckGroupQuestItem(item))
                                {
                                    continue;
                                }
                            }

                            if (drop.QuestRequired)
                                continue;

                            if (!DropItem(item))
                            {
                                //SMain.EnqueueDebugging(string.Format("Could not drop {0} to the floor.", item.Name));
                                return;                                
                            }
                            else
                            {
                                //SMain.EnqueueDebugging(string.Format("Dropped Item {0} to the floor Elite level {1}", item.Name, drop.EliteLevel));
                            }
                        }

                    }
                }
            }
            #endregion
        }

        protected virtual bool DropItem(UserItem item)
        {
            if (CurrentMap.Info.NoDropMonster) return false;

            ItemObject ob = new ItemObject(this, item)
            {
                Owner = EXPOwner,
                OwnerTime = Envir.Time + Settings.Minute,
            };

            return ob.Drop(Settings.DropRange);
        }

        protected virtual bool DropGold(uint gold)
        {
            if (EXPOwner != null && EXPOwner.CanGainGold(gold) && !Settings.DropGold)
            {
                EXPOwner.WinGold(gold);
                return true;
            }

            uint count = gold / Settings.MaxDropGold == 0 ? 1 : gold / Settings.MaxDropGold + 1;
            for (int i = 0; i < count; i++)
            {
                ItemObject ob = new ItemObject(this, i != count - 1 ? Settings.MaxDropGold : gold % Settings.MaxDropGold)
                {
                    Owner = EXPOwner,
                    OwnerTime = Envir.Time + Settings.Minute,
                };

                ob.Drop(Settings.DropRange);
            }

            return true;
        }

        public override void Process()
        {
            base.Process();

            RefreshNameColour();
            //  Only player, mob and hero can be a target should do I think
            if (Target != null && (Target.Race == ObjectType.Item))
                Target = null;
            if (Target != null && (Target.CurrentMap != CurrentMap || !Target.IsAttackTarget(this) || !Functions.InRange(CurrentLocation, Target.CurrentLocation, Globals.DataRange)))
                Target = null;

            for (int i = SlaveList.Count - 1; i >= 0; i--)
                if (SlaveList[i].Dead || SlaveList[i].Node == null)
                    SlaveList.RemoveAt(i);

            if (Dead && Envir.Time >= DeadTime)
            {
                CurrentMap.RemoveObject(this);
                if (Master != null && Race != ObjectType.Hero)
                {
                    Master.Pets.Remove(this);
                    Master = null;
                }
                Despawn();
                return;
            }

            if(Master != null && TameTime > 0 && Envir.Time >= TameTime)
            {
                Master.Pets.Remove(this);
                Master = null;
                Broadcast(new S.ObjectName { ObjectID = ObjectID, Name = Name });
            }


            if (IsRetreatingMob)
            {
                if (IsPublicEvenetMob)
                {
                    //  Not already retreating
                    if (!IsRetreating)
                    {
                        //  Get any public event from our location
                        PublicEvent tempEv = CurrentMap.GetPublicEvent(CurrentLocation);
                        //  We're not within the event area
                        if (tempEv == null)
                        {
                            //  Ensure our origin location is far enough away to walk to
                            if (Functions.MaxDistance(CurrentLocation, OriginLocation) > 1)
                            {
                                //  We're not in retreat
                                IsRetreating = true;
                                //  Make the first move
                                MoveTo(OriginLocation);
                            }
                        }
                    }
                    //  We're in retreat
                    else
                    {
                        //  Distance is greater than 1
                        if (Functions.MaxDistance(CurrentLocation, OriginLocation) > 1)
                            //  Move to origin location
                            MoveTo(OriginLocation);
                        //  We've hit out origin location
                        else
                        {
                            //  Stop retreating
                            IsRetreating = false;
                        }
                    }
                }
                else
                {
                    if (Functions.MaxDistance(CurrentLocation, OriginLocation) > 12)
                    {
                        IsRetreating = true;
                        Target = null;
                        MoveTo(OriginLocation);
                        return;
                    }
                    else if (IsRetreating && Functions.MaxDistance(CurrentLocation, OriginLocation) > 1)
                        MoveTo(OriginLocation);
                    else
                        IsRetreating = false;
                }
            }

            ProcessAI();

            ProcessBuffs();
            ProcessRegen();
            ProcessPoison();


         /*   if (!HealthChanged) return;

            HealthChanged = false;
            
            BroadcastHealthChange();*/
        }

        public override void SetOperateTime()
        {
            long time = Envir.Time + 2000;

            if (DeadTime < time && DeadTime > Envir.Time)
                time = DeadTime;

            if (OwnerTime < time && OwnerTime > Envir.Time)
                time = OwnerTime;

            if (ExpireTime < time && ExpireTime > Envir.Time)
                time = ExpireTime;

            if (PKPointTime < time && PKPointTime > Envir.Time)
                time = PKPointTime;

            if (LastHitTime < time && LastHitTime > Envir.Time)
                time = LastHitTime;

            if (EXPOwnerTime < time && EXPOwnerTime > Envir.Time)
                time = EXPOwnerTime;

            if (SearchTime < time && SearchTime > Envir.Time)
                time = SearchTime;

            if (RoamTime < time && RoamTime > Envir.Time)
                time = RoamTime;
            //  2           3           2       1
            //  Current < Now + 2 and Current > Now
            if (ShockTime < time && ShockTime > Envir.Time)
                //  2
                time = ShockTime;

            if (RegenTime < time && RegenTime > Envir.Time && Health < MaxHealth)
                time = RegenTime;

            if (RageTime < time && RageTime > Envir.Time)
                time = RageTime;

            if (HallucinationTime < time && HallucinationTime > Envir.Time)
                time = HallucinationTime;

            if (ActionTime < time && ActionTime > Envir.Time)
                time = ActionTime;

            if (MoveTime < time && MoveTime > Envir.Time)
                time = MoveTime;

            if (AttackTime < time && AttackTime > Envir.Time)
                time = AttackTime;

            if (HealTime < time && HealTime > Envir.Time && HealAmount > 0)
                time = HealTime;

            if (BrownTime < time && BrownTime > Envir.Time)
                time = BrownTime;

            for (int i = 0; i < ActionList.Count; i++)
            {
                if (ActionList[i].Time >= time && ActionList[i].Time > Envir.Time) continue;
                time = ActionList[i].Time;
            }

            for (int i = 0; i < PoisonList.Count; i++)
            {
                if (PoisonList[i].TickTime >= time && PoisonList[i].TickTime > Envir.Time) continue;
                time = PoisonList[i].TickTime;
            }

            for (int i = 0; i < Buffs.Count; i++)
            {
                if (Buffs[i].ExpireTime >= time && Buffs[i].ExpireTime > Envir.Time) continue;
                time = Buffs[i].ExpireTime;
            }


            if (OperateTime <= Envir.Time || time < OperateTime)
                OperateTime = time;
        }

        public override void Process(DelayedAction action)
        {
            switch (action.Type)
            {
                case DelayedType.Damage:
                    CompleteAttack(action.Params);
                    break;
                case DelayedType.RangeDamage:
                    CompleteRangeAttack(action.Params);
                    break;
                case DelayedType.Die:
                    CompleteDeath(action.Params);
                    break;
                case DelayedType.Magic:
                    CompleteMagicAttack(action.Params);
                    break;
                case DelayedType.Recall:
                    PetRecall();
                    break;
            }
        }

        public virtual void PetRecall()
        {
            if (Master == null) return;
            if (!Teleport(Master.CurrentMap, Master.Back))
                Teleport(Master.CurrentMap, Master.CurrentLocation);
        }
        protected virtual void CompleteAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            target.Attacked(this, damage, defence);
        }

        protected virtual void CompleteRangeAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];

            if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;

            target.Attacked(this, damage, defence);
        }

        protected virtual void CompleteMagicAttack(IList<object> data)
        {
            MapObject target = (MapObject)data[0];
            int damage = (int)data[1];
            DefenceType defence = (DefenceType)data[2];
            int type = (int)data[3];
            switch (type)
            {
                /// Falcon Lord - Teleport & Attack
                case 0:
                    {
                        if (target.CurrentMap != CurrentMap ||
                            !Functions.InRange(CurrentLocation, target.CurrentLocation, 12))
                            return;
                        if (target != null &&
                            target.IsAttackTarget(this))
                        {
                            //Broadcast(new S.Chat { Message = string.Format("Now Teleporting to {0}", target.Name), Type = ChatType.System });
                            Teleport(CurrentMap, target.Back, false);
                            target.Attacked(this, damage, defence);
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Location = CurrentLocation, Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation) });
                        }
                    }
                    break;
                /// Jin Lord - Multiple Range Attack
                case 1:
                    if (target != null &&
                        target.CurrentMap == CurrentMap &&
                        Functions.InRange(CurrentLocation, target.CurrentLocation, 12) &&
                        target.IsAttackTarget(this))
                    {
                        bool sendAtack = (bool)data[4];
                        if (sendAtack)
                            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Location = CurrentLocation, Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation) });
                        target.Attacked(this, damage, defence);
                    }
                    break;
            }
        }

        protected virtual void CompleteDeath(IList<object> data)
        {
            throw new NotImplementedException();
        }

        protected virtual void ProcessRegen()
        {
            if (Dead) return;

            int healthRegen = 0;

            if (CanRegen)
            {
                RegenTime = Envir.Time + RegenDelay;


                if (HP < MaxHP)
                    healthRegen += (int)(MaxHP * 0.022F) + 1;
            }


            if (Envir.Time > HealTime)
            {
                HealTime = Envir.Time + HealDelay;

                if (HealAmount > 5)
                {
                    healthRegen += 5;
                    HealAmount -= 5;
                }
                else
                {
                    healthRegen += HealAmount;
                    HealAmount = 0;
                }
                if (IsRetreatingMob &&
                IsRetreating)
                {
                    
                    int boost = 0;
                    int dist = Functions.MaxDistance(CurrentLocation, OriginLocation);
                    if (dist >= 4 &&
                        dist <= 10)
                    {
                        //Partial Retreat from a distance of 8 with a slightly larger regen
                        if (dist >= 8)
                        {
                            //  10% regen
                            healthRegen += (int)MaxHealth / 10;
                            boost = (int)MaxHealth / 10;
                        }
                        //  Slightly better regen
                        else if (dist >= 4 && dist < 8)
                        {
                            healthRegen += (int)MaxHealth / 15;
                            boost = (int)MaxHealth / 15;
                        }
                    }
                    else if (dist > 10)
                    {
                        //  1 5th of it's max health every tick
                        healthRegen += (int)MaxHealth / 5;
                        boost = (int)MaxHealth / 5;
                    }
                    
                }
            }
            
            if (healthRegen > 0) ChangeHP(healthRegen);
            if (HP == MaxHP) HealAmount = 0;
        }
        protected virtual void ProcessPoison()
        {
            PoisonType type = PoisonType.None;
            ArmourRate = 1F;
            DamageRate = 1F;
            for (int i = PoisonList.Count - 1; i >= 0; i--)
            {
                if (Dead) return;

                if (i <= -1 || i > PoisonList.Count - 1)
                    continue;
                if (PoisonList[i] == null)
                    continue;
                //  Retreating mobs will remove poisons
                if (IsRetreating)
                {
                    PoisonList.RemoveAt(i);
                    continue;
                }
                Poison poison = PoisonList[i];
                if (poison == null)
                    continue;
                if (poison.Owner != null && poison.Owner.Node == null)
                {
                    PoisonList.RemoveAt(i);
                    continue;
                }
                if (poison.Owner != null && poison.Owner.Node != null &&
                    (poison.Owner.CurrentMap != CurrentMap || Functions.MaxDistance(poison.Owner.CurrentLocation, CurrentLocation) > Globals.DataRange))
                {
                    PoisonList.RemoveAt(i);
                    continue;
                }
                if (Envir.Time > poison.TickTime)
                {
                    poison.Time++;
                    poison.TickTime = Envir.Time + poison.TickSpeed;

                    if (poison.Time >= poison.Duration)
                        PoisonList.RemoveAt(i);

                    if (poison.PType == PoisonType.Green || poison.PType == PoisonType.Bleeding || poison.PType == PoisonType.Burning)
                    {
                        //  If the tao poisoned it first it'll be the exp owner
                        if (EXPOwner == null ||
                            EXPOwner.Dead)
                        {
                            EXPOwner = poison.Owner;
                            EXPOwnerTime = Envir.Time + EXPOwnerDelay;
                        }
                        //  Exp owner is the Object that poisoned it
                        else if (EXPOwner == poison.Owner)
                            //  Reset the delay to the maximum (I.E 3 seconds)
                            EXPOwnerTime = Envir.Time + EXPOwnerDelay;

                        if (poison.PType == PoisonType.Bleeding)
                        {
                            Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.Bleeding, EffectType = 0 });
                            BroadcastDamageIndicator(DamageType.Bleeding, poison.Value);
                        }
                        else if (poison.PType == PoisonType.Burning)
                        {
                            Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.LavaBurning, EffectType = 0 });
                            if (Envir.Time > nextStruckTime)
                            {
                                nextStruckTime = Envir.Time + 800;
                                if (poison.Owner != null)
                                    Broadcast(new S.ObjectStruck { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, AttackerID = poison.Owner.ObjectID });
                            }
                            BroadcastDamageIndicator(DamageType.Burning, poison.Value);
                        }
                        else
                        {
                            BroadcastDamageIndicator(DamageType.GreenPoison, poison.Value);
                        }

                        //ChangeHP(-poison.Value);
                        PoisonDamage(-poison.Value, poison.Owner);

                        if (PoisonStopRegen)
                            RegenTime = Envir.Time + RegenDelay;
                    }

                    if (poison.PType == PoisonType.DelayedExplosion)
                    {
                        if (Envir.Time > ExplosionInflictedTime) ExplosionInflictedStage++;

                        if (!ProcessDelayedExplosion(poison))
                        {
                            ExplosionInflictedStage = 0;
                            ExplosionInflictedTime = 0;

                            if (Dead) break; //temp to stop crashing

                            PoisonList.RemoveAt(i);
                            continue;
                        }
                    }
                }

                switch (poison.PType)
                {
                    case PoisonType.Red:
                        ArmourRate -= 0.5F;
                        break;
                    case PoisonType.Stun:
                        DamageRate += 0.5F;
                        break;
                    case PoisonType.Slow:
                        MoveSpeed += 800;
                        AttackSpeed += 800;

                        if (poison.Time >= poison.Duration)
                        {
                            MoveSpeed = Info.MoveSpeed;
                            AttackSpeed = Info.AttackSpeed;
                        }
                        break;
                }
                type |= poison.PType;
                /*
                if ((int)type < (int)poison.PType)
                    type = poison.PType;
                 */
            }
            
            
            if (type == CurrentPoison) return;

            CurrentPoison = type;
            Broadcast(new S.ObjectPoisoned { ObjectID = ObjectID, Poison = type });
        }

        private bool ProcessDelayedExplosion(Poison poison)
        {
            if (Dead) return false;

            if (ExplosionInflictedStage == 0)
            {
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.DelayedExplosion, EffectType = 0 });
                return true;
            }
            if (ExplosionInflictedStage == 1)
            {
                if (Envir.Time > ExplosionInflictedTime)
                    ExplosionInflictedTime = poison.TickTime + 3000;
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.DelayedExplosion, EffectType = 1 });
                return true;
            }
            if (ExplosionInflictedStage == 2)
            {
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.DelayedExplosion, EffectType = 2 });
                if (poison.Owner != null)
                {
                    switch (poison.Owner.Race)
                    {
                        case ObjectType.Player:
                            PlayerObject caster = (PlayerObject)poison.Owner;
                            DelayedAction action = new DelayedAction(DelayedType.Magic, Envir.Time, poison.Owner, caster.GetMagic(Spell.DelayedExplosion), poison.Value, this.CurrentLocation);
                            CurrentMap.ActionList.Add(action);
                            //Attacked((PlayerObject)poison.Owner, poison.Value, DefenceType.MAC, false);
                            break;
                        case ObjectType.Monster://this is in place so it could be used by mobs if one day someone chooses to
                            Attacked((MonsterObject)poison.Owner, poison.Value, DefenceType.MAC);
                            break;
                    }
                    LastHitter = poison.Owner;
                }
                return false;
            }
            return false;
        }


        public virtual void ProcessBuffs()
        {
            bool refresh = false;
            for (int i = Buffs.Count - 1; i >= 0; i--)
            {
                Buff buff = Buffs[i];

                if (Envir.Time <= buff.ExpireTime) continue;

                Buffs.RemoveAt(i);
                Broadcast(new S.RemoveBuff { Type = buff.Type, ObjectID = ObjectID });

                switch (buff.Type)
                {
                    case BuffType.MoonLight:
                    case BuffType.Hiding:
                    case BuffType.DarkBody:
                    case BuffType.MoonMist:
                    case BuffType.Cloak:
                        Hidden = false;
                        break;
                }

                refresh = true;
            }

            if (refresh) RefreshAll();
        }
        protected virtual void ProcessAI()
        {
            if (Dead) return;

            if (Master != null)
            {
                if ((Master.PMode == PetMode.Both || Master.PMode == PetMode.MoveOnly))
                {
                    if (!Functions.InRange(CurrentLocation, Master.CurrentLocation, Globals.DataRange) || CurrentMap != Master.CurrentMap)
                        PetRecall();
                }

                if (Master.PMode == PetMode.MoveOnly || Master.PMode == PetMode.None)
                    Target = null;
            }
           
            ProcessSearch();
            ProcessRoam();
            ProcessTarget();
        }

        protected virtual bool InAttackRange(int dist)
        {
            if (Target == null) return false;
            if (Target.CurrentMap != CurrentMap) return false;

            return Target.CurrentLocation != CurrentLocation && Functions.InRange(CurrentLocation, Target.CurrentLocation, dist);
        }

        protected virtual void ProcessSearch()
        {
            if (Envir.Time < SearchTime) return;
            if (Master != null && (Master.PMode == PetMode.MoveOnly || Master.PMode == PetMode.None)) return;
            
            SearchTime = Envir.Time + SearchDelay;

            if (CurrentMap.Inactive(5)) return;

            //Stacking or Infront of master - Move
            bool stacking = CheckStacked();

            if (CanMove && ((Master != null && Master.Front == CurrentLocation) || stacking))
            {
                //Walk Randomly
                if (!Walk(Direction))
                {
                    MirDirection dir = Direction;

                    switch (Envir.Random.Next(3)) // favour Clockwise
                    {
                        case 0:
                            for (int i = 0; i < 7; i++)
                            {
                                dir = Functions.NextDir(dir);

                                if (Walk(dir))
                                    break;
                            }
                            break;
                        default:
                            for (int i = 0; i < 7; i++)
                            {
                                dir = Functions.PreviousDir(dir);

                                if (Walk(dir))
                                    break;
                            }
                            break;
                    }
                }
            }
            //  Don't search if we're retreating
            if (!IsRetreating)
                if (Target == null || Envir.Random.Next(3) == 0)
                    FindTarget();
        }
        protected virtual void ProcessRoam()
        {
            if (Target != null || Envir.Time < RoamTime) return;

            if (ProcessRoute()) return;

            if (CurrentMap.Inactive(30)) return;

            if (Master != null)
            {
                MoveTo(Master.Back);
                return;
            }

            RoamTime = Envir.Time + RoamDelay;
            if (Envir.Random.Next(10) != 0) return;

            switch (Envir.Random.Next(3)) //Face Walk
            {
                case 0:
                    Turn((MirDirection)Envir.Random.Next(8));
                    break;
                default:
                    Walk(Direction);
                    break;
            }
        }
        protected virtual void ProcessTarget()
        {
            //  Don't attack or move to the target, completely ignore them!
            if (IsRetreating)
                return;
            if (Target == null || !CanAttack) return;

            if (InAttackRange())
            {
                Attack();
                if (Target.Dead)
                    FindTarget();

                return;
            }

            if (Envir.Time < ShockTime)
            {
                Target = null;
                return;
            }

            if (Info.TeleportBack && Envir.Random.Next(7) == 0 && Target.CurrentMap == CurrentMap)
                Teleport(Target.CurrentMap,Target.Back);
            else
                MoveTo(Target.CurrentLocation);
        }
        protected virtual bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;

            return Target.CurrentLocation != CurrentLocation && Functions.InRange(CurrentLocation, Target.CurrentLocation, 1);
        }

        protected virtual void FindTarget()
        {
            if (HMode == HeroMode.DontAttack) return;
            //if (CurrentMap.Players.Count < 1) return;
            Map Current = CurrentMap;

            for (int d = 0; d <= Info.ViewRange; d++)
            {
                for (int y = CurrentLocation.Y - d; y <= CurrentLocation.Y + d; y++)
                {
                    if (y < 0) continue;
                    if (y >= Current.Height) break;

                    for (int x = CurrentLocation.X - d; x <= CurrentLocation.X + d; x += Math.Abs(y - CurrentLocation.Y) == d ? 1 : d*2)
                    {
                        if (x < 0) continue;
                        if (x >= Current.Width) break;
                        Cell cell = Current.Cells[x, y];
                        if (cell.Objects == null || !cell.Valid) continue;
                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Hero:
                                    if (!ob.IsAttackTarget(this)) continue;
                                    if (ob.Hidden && (!CoolEye || Level < ob.Level)) continue;
                                    if (this is TrapRock && ob.InTrapRock) continue;
                                    Target = ob;
                                    return;
                                case ObjectType.Player:
                                    PlayerObject playerob = (PlayerObject)ob;
                                    if (!ob.IsAttackTarget(this)) continue;
                                    if (playerob.GMGameMaster || ob.Hidden && (!CoolEye || Level < ob.Level) || Envir.Time < HallucinationTime) continue;

                                    Target = ob;

                                    if (Master != null)
                                    {
                                        for (int j = 0; j < playerob.Pets.Count; j++)
                                        {
                                            MonsterObject pet = playerob.Pets[j];

                                            if (!pet.IsAttackTarget(this)) continue;
                                            Target = pet;
                                            break;
                                        }
                                    }
                                    return;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
        }

        protected virtual bool ProcessRoute()
        {
            if (Route.Count < 1) return false;

            RoamTime = Envir.Time + 500;

            if (CurrentLocation == Route[RoutePoint].Location)
            {
                if (Route[RoutePoint].Delay > 0 && !Waiting)
                {
                    Waiting = true;
                    RoamTime = Envir.Time + RoamDelay + Route[RoutePoint].Delay;
                    return true;
                }

                Waiting = false;
                RoutePoint++;
            }

            if (RoutePoint > Route.Count - 1) RoutePoint = 0;

            if (!CurrentMap.ValidPoint(Route[RoutePoint].Location)) return true;

            MoveTo(Route[RoutePoint].Location);

            return true;
        }

        protected virtual void MoveTo(Point location)
        {
            if (CurrentLocation == location) return;

            bool inRange = Functions.InRange(location, CurrentLocation, 1);

            if (inRange)
            {
                if (!CurrentMap.ValidPoint(location)) return;
                Cell cell = CurrentMap.GetCell(location);
                if (cell.Objects != null)
                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject ob = cell.Objects[i];
                        if (!ob.Blocking) continue;
                        return;
                    }
            }

            MirDirection dir = Functions.DirectionFromPoint(CurrentLocation, location);

            if (Walk(dir)) return;

            switch (Envir.Random.Next(2)) //No favour
            {
                case 0:
                    for (int i = 0; i < 7; i++)
                    {
                        dir = Functions.NextDir(dir);

                        if (Walk(dir))
                            return;
                    }
                    break;
                default:
                    for (int i = 0; i < 7; i++)
                    {
                        dir = Functions.PreviousDir(dir);

                        if (Walk(dir))
                            return;
                    }
                    break;
            }
        }

        public virtual void Turn(MirDirection dir)
        {
            if (!CanMove) return;
            if (CurrentMap == null) return;

            Direction = dir;
                
            InSafeZone = CurrentMap.GetSafeZone(CurrentLocation) != null;


            Cell cell = CurrentMap.GetCell(CurrentLocation);
            if (cell == null || cell.Objects == null || cell.Objects.Count == 0)//strange it was missing lol
                return;
            for (int i = 0; i < cell.Objects.Count; i++)
            {
                if (cell.Objects[i].Race != ObjectType.Spell) continue;
                SpellObject ob = (SpellObject)cell.Objects[i];

                ob.ProcessSpell(this);
                //break;
            }


            Broadcast(new S.ObjectTurn { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
        }


        public virtual bool Walk(MirDirection dir) 
        {
            if (!CanMove) return false;

            Point location = Functions.PointMove(CurrentLocation, dir, 1);

            if (!CurrentMap.ValidPoint(location)) return false;

            Cell cell = CurrentMap.GetCell(location);

            if (cell.Objects != null)
            for (int i = 0; i < cell.Objects.Count; i++)
            {
                MapObject ob = cell.Objects[i];
                if (!ob.Blocking || Race == ObjectType.Creature) continue;

                return false;
            }

            CurrentMap.GetCell(CurrentLocation).Remove(this);

            Direction = dir;
            RemoveObjects(dir, 1);
            CurrentLocation = location;
            CurrentMap.GetCell(CurrentLocation).Add(this);
            AddObjects(dir, 1);

            if (Hidden)
            {
                for (int i = 0; i < Buffs.Count; i++)
                {
                    if (Buffs[i].Type != BuffType.Hiding) continue;
                    Hidden = false;
                    Buffs[i].ExpireTime = 0;
                    break;
                }
            }


            CellTime = Envir.Time + 500;
            ActionTime = Envir.Time + 300;
            MoveTime = Envir.Time + MoveSpeed;
            if (MoveTime > AttackTime)
                AttackTime = MoveTime;

            InSafeZone = CurrentMap.GetSafeZone(CurrentLocation) != null;

            Broadcast(new S.ObjectWalk { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });


            cell = CurrentMap.GetCell(CurrentLocation);

            for (int i = 0; i < cell.Objects.Count; i++)
            {
                if (cell.Objects[i].Race != ObjectType.Spell) continue;
                SpellObject ob = (SpellObject)cell.Objects[i];

                ob.ProcessSpell(this);
                //break;
            }

            return true;
        }

        public virtual bool Walk(MirDirection dir, bool br = false)
        {
            if (!CanMove) return false;

            Point location = Functions.PointMove(CurrentLocation, dir, 1);

            if (!CurrentMap.ValidPoint(location)) return false;

            Cell cell = CurrentMap.GetCell(location);

            if (cell.Objects != null)
                for (int i = 0; i < cell.Objects.Count; i++)
                {
                    MapObject ob = cell.Objects[i];
                    if (!ob.Blocking || Race == ObjectType.Creature) continue;

                    return false;
                }

            CurrentMap.GetCell(CurrentLocation).Remove(this);

            Direction = dir;
            RemoveObjects(dir, 1);
            CurrentLocation = location;
            CurrentMap.GetCell(CurrentLocation).Add(this);
            AddObjects(dir, 1);

            if (Hidden)
            {
                for (int i = 0; i < Buffs.Count; i++)
                {
                    if (Buffs[i].Type != BuffType.Hiding) continue;
                    Hidden = false;
                    Buffs[i].ExpireTime = 0;
                    break;
                }
            }


            CellTime = Envir.Time + 500;
            ActionTime = Envir.Time + 300;
            MoveTime = Envir.Time + MoveSpeed;
            if (MoveTime > AttackTime)
                AttackTime = MoveTime;

            InSafeZone = CurrentMap.GetSafeZone(CurrentLocation) != null;

            Broadcast(new S.ObjectWalk { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });


            cell = CurrentMap.GetCell(CurrentLocation);

            for (int i = 0; i < cell.Objects.Count; i++)
            {
                if (cell.Objects[i].Race != ObjectType.Spell) continue;
                SpellObject ob = (SpellObject)cell.Objects[i];

                ob.ProcessSpell(this);
                //break;
            }

            return true;
        }
        protected virtual void Attack()
        {
            if (BindingShotCenter) ReleaseBindingShot();

            ShockTime = 0;
            
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }


            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });


            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;

            int damage = GetAttackPower(MinDC, MaxDC);

            if (damage == 0) return;

            Target.Attacked(this, damage);
        }

        public void ReleaseBindingShot()
        {
            if (!BindingShotCenter) return;

            ShockTime = 0;
            Broadcast(GetInfo());//update clients in range (remove effect)
            BindingShotCenter = false;

            //the centertarget is escaped so make all shocked mobs awake (3x3 from center)
            Point place = CurrentLocation;
            for (int y = place.Y - 1; y <= place.Y + 1; y++)
            {
                if (y < 0) continue;
                if (y >= CurrentMap.Height) break;

                for (int x = place.X - 1; x <= place.X + 1; x++)
                {
                    if (x < 0) continue;
                    if (x >= CurrentMap.Width) break;

                    Cell cell = CurrentMap.GetCell(x, y);
                    if (!cell.Valid || cell.Objects == null) continue;

                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject targetob = cell.Objects[i];
                        if (targetob == null || targetob.Node == null || targetob.Race != ObjectType.Monster) continue;
                        if (((MonsterObject)targetob).ShockTime == 0) continue;

                        //each centerTarget has its own effect which needs to be cleared when no longer shocked
                        if (((MonsterObject)targetob).BindingShotCenter) ((MonsterObject)targetob).ReleaseBindingShot();
                        else ((MonsterObject)targetob).ShockTime = 0;

                        break;
                    }
                }
            }
        }

        public bool FindNearby(int distance)
        {
            for (int d = 0; d <= distance; d++)
            {
                for (int y = CurrentLocation.Y - d; y <= CurrentLocation.Y + d; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;

                    for (int x = CurrentLocation.X - d; x <= CurrentLocation.X + d; x += Math.Abs(y - CurrentLocation.Y) == d ? 1 : d * 2)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;
                        if (!CurrentMap.ValidPoint(x, y)) continue;
                        Cell cell = CurrentMap.GetCell(x, y);
                        if (cell.Objects == null) continue;

                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Player:
                                case ObjectType.Hero:
                                    if (!ob.IsAttackTarget(this)) continue;
                                    if (ob.Hidden && (!CoolEye || Level < ob.Level)) continue;
                                    if (ob.Race == ObjectType.Player)
                                    {
                                        PlayerObject player = ((PlayerObject)ob);
                                        if (player.GMGameMaster) continue;
                                    }
                                    return true;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }

            return false;
        }
        public bool FindFriendsNearby(int distance)
        {
            for (int d = 0; d <= distance; d++)
            {
                for (int y = CurrentLocation.Y - d; y <= CurrentLocation.Y + d; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;

                    for (int x = CurrentLocation.X - d; x <= CurrentLocation.X + d; x += Math.Abs(y - CurrentLocation.Y) == d ? 1 : d * 2)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;
                        if (!CurrentMap.ValidPoint(x, y)) continue;
                        Cell cell = CurrentMap.GetCell(x, y);
                        if (cell.Objects == null) continue;

                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Player:
                                case ObjectType.Hero:
                                    if (ob == this || ob.Dead) continue;
                                    if (ob.IsAttackTarget(this)) continue;
                                    if (ob.Race == ObjectType.Player)
                                    {
                                        PlayerObject player = ((PlayerObject)ob);
                                        if (player.GMGameMaster) continue;
                                    }
                                    return true;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public List<MapObject> FindAllNearby(int dist, Point location, bool needSight = true)
        {
            List<MapObject> targets = new List<MapObject>();
            for (int d = 0; d <= dist; d++)
            {
                for (int y = location.Y - d; y <= location.Y + d; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;
                    //if ((y < location.Y - d + 1) || (y > location.Y + d - 1)) continue;

                    for (int x = location.X - d; x <= location.X + d; x += Math.Abs(y - location.Y) == d ? 1 : d * 2)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;
                       // if ((x < location.Y - d + 1) || (x > location.X + d - 1)) continue;

                        Cell cell = CurrentMap.GetCell(x, y);
                        if (!cell.Valid || cell.Objects == null) continue;

                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Player:
                                case ObjectType.Hero:
                                    targets.Add(ob);
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
            return targets;
        }

        protected List<MapObject> FindAllTargets(int dist, Point location, bool needSight = true)
        {
            List<MapObject> targets = new List<MapObject>();
            for (int d = 0; d <= dist; d++)
            {
                for (int y = location.Y - d; y <= location.Y + d; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;

                    for (int x = location.X - d; x <= location.X + d; x += Math.Abs(y - location.Y) == d ? 1 : d * 2)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;                    

                        Cell cell = CurrentMap.GetCell(x, y);
                        if (!cell.Valid || cell.Objects == null) continue;

                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];
                            switch (ob.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Player:
                                case ObjectType.Hero:
                                    if (!ob.IsAttackTarget(this)) continue;
                                    if (ob.Hidden && (!CoolEye || Level < ob.Level) && needSight) continue;
                                    if (ob.Race == ObjectType.Player)
                                    {
                                        PlayerObject player = ((PlayerObject)ob);
                                        if (player.GMGameMaster) continue;
                                    }
                                    targets.Add(ob);
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
            return targets;
        }

        public override bool IsAttackTarget(PlayerObject attacker)
        {
            if (attacker == null || attacker.Node == null) return false;
            if (Dead) return false;
            if (Master == null) return true;
            if (attacker.AMode == AttackMode.Peace) return false;
            if (Master == attacker)
            {
                if (Race == ObjectType.Hero) return false;
                return attacker.AMode == AttackMode.All;
            }
            if (Master.Race == ObjectType.Player && (attacker.InSafeZone || InSafeZone)) return false;

            switch (attacker.AMode)
            {
                case AttackMode.Group:
                    return Master.GroupMembers == null || !Master.GroupMembers.Contains(attacker);
                case AttackMode.Guild:
                    {
                        if (!(Master is PlayerObject)) return false;
                        PlayerObject master = (PlayerObject)Master;
                        return master.MyGuild == null || master.MyGuild != attacker.MyGuild;
                    }
                case AttackMode.EnemyGuild:
                    {
                        if (!(Master is PlayerObject)) return false;
                        PlayerObject master = (PlayerObject)Master;
                        return (master.MyGuild != null && attacker.MyGuild != null) && master.MyGuild.IsEnemy(attacker.MyGuild);
                    }
                case AttackMode.RedBrown:
                    return Master.PKPoints >= 200 || Envir.Time < Master.BrownTime;
                default:
                    return true;
            }
        }
        public override bool IsAttackTarget(MonsterObject attacker)
        {
            if (attacker == null || attacker.Node == null) return false;

            if (Dead || attacker == this) return false;
            if (attacker.Race == ObjectType.Creature) return false;

            if (attacker.Info.AI == 6) // Guard
            {
                if (Info.AI != 1 && Info.AI != 2 && Info.AI != 3 && (Master == null || (Master.PKPoints >= 200 && Race != ObjectType.Hero)) && !Info.Ignore) //Not Dear/Hen/Tree/Pets or Red Master 
                    return true;
            }
            else if (attacker.Info.AI == 58) // Tao Guard - attacks Pets
            {
                if (Info.AI != 1 && Info.AI != 2 && Info.AI != 3 && !Info.Ignore && (Race != ObjectType.Hero && Master != null)) //Not Dear/Hen/Tree
                    return true;
            }
            else if (Master != null) //Pet Attacked
            {
                if (attacker.Master == null) //Wild Monster
                    return true;
                
                //Pet Vs Pet
                if (Master == attacker.Master)
                    return false;

                if (Envir.Time < ShockTime) //Shocked
                    return false;

                if (Master.Race == ObjectType.Player && attacker.Master.Race == ObjectType.Player && (Master.InSafeZone || attacker.Master.InSafeZone)) return false;

                if (attacker.Race == ObjectType.Hero && Race == ObjectType.Hero)
                {
                    if (PKPoints < 100 && Envir.heroConfig.NoWhite)
                        return false;

                    if (PKPoints >= 100 && PKPoints < 200 && Envir.heroConfig.NoYellow)
                        return false;

                    if (PKPoints >= 200 && Envir.heroConfig.NoRed)
                        return false;

                    if (Envir.Time < BrownTime && Envir.heroConfig.NoBrown)
                        return false;

                    if (Envir.heroConfig.NoHero)
                        return false;

                    if (Level > attacker.Level && Envir.heroConfig.NoUnderLevel)
                        return false;

                }


                switch (attacker.Master.AMode)
                {
                    case AttackMode.Group:
                        if (Master.GroupMembers != null && Master.GroupMembers.Contains((PlayerObject)attacker.Master)) return false;
                        break;
                    case AttackMode.Guild:
                        break;
                    case AttackMode.EnemyGuild:
                        break;
                    case AttackMode.RedBrown:
                        if (attacker.Master.PKPoints < 200 || Envir.Time > attacker.Master.BrownTime) return false;
                        break;
                    case AttackMode.Peace:
                        return false;
                }

                for (int i = 0; i < Master.Pets.Count; i++)
                    if (Master.Pets[i].EXPOwner == attacker.Master) return true;

                for (int i = 0; i < attacker.Master.Pets.Count; i++)
                {
                    MonsterObject ob = attacker.Master.Pets[i];
                    if (ob == Target || ob.Target == this) return true;
                }

                return Master.LastHitter == attacker.Master;
            }
            else if (attacker.Master != null) //Pet Attacking Wild Monster
            {
                if (Envir.Time < ShockTime) //Shocked
                    return false;

                for (int i = 0; i < attacker.Master.Pets.Count; i++)
                {
                    MonsterObject ob = attacker.Master.Pets[i];
                    if (ob == Target || ob.Target == this) return true;
                }

                if (attacker.Race == ObjectType.Hero)
                {
                    MonsterObject ob = attacker;
                    switch (attacker.Master.HMode)
                    {
                        case HeroMode.Defensive:
                            if (ob.Target == this) return true;
                            break;
                        case HeroMode.Offensive:
                        case HeroMode.Guard:
                            if (attacker.Master.IsAttackTarget(this)) return true;
                            break; ;

                    }

                }

                if (Target == attacker.Master)
                    return true;
            }

            if (Envir.Time < attacker.HallucinationTime) return true;
            return Envir.Time < attacker.RageTime;
        }
        public override bool IsFriendlyTarget(PlayerObject ally)
        {
            if (ally == null) return false;
            if (Master == null) return false;
            if (Master == ally) return true;

            switch (ally.AMode)
            {
                case AttackMode.Group:
                    return Master.GroupMembers != null && Master.GroupMembers.Contains(ally);
                case AttackMode.Guild:
                    return false;
                case AttackMode.EnemyGuild:
                    return true;
                case AttackMode.RedBrown:
                    return Master.PKPoints < 200 & Envir.Time > Master.BrownTime;
            }
            return true;
        }

        public override bool IsFriendlyTarget(MonsterObject ally)
        {
            if (Master != null) return false;
            if (ally.Race != ObjectType.Monster) return false;
            if (ally.Master != null) return false;

            return true;
        }

        public override int Attacked(PlayerObject attacker, int damage, DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
        {
            if (Target == null && attacker.IsAttackTarget(this))
            {
                Target = attacker;
            }

            if (attacker != null)
            {
                if (attacker.Race == ObjectType.Player)
                {
                    if (attacker.PvEDamageIncrease > 0)
                        damage += damage * attacker.PvEDamageIncrease / 100;
                }
            }
            int armour = 0;

            switch (type)
            {
                case DefenceType.ACAgility:
                    if (Envir.Random.Next(Agility + 1) > attacker.Accuracy)
                    {
                        BroadcastDamageIndicator(DamageType.Miss);
                        return 0;
                    }
                    armour = GetDefencePower(MinAC, MaxAC);
                    break;
                case DefenceType.AC:
                    armour = GetDefencePower(MinAC, MaxAC);
                    break;
                case DefenceType.MACAgility:
                    if (Envir.Random.Next(Agility + 1) > attacker.Accuracy)
                    {
                        BroadcastDamageIndicator(DamageType.Miss);
                        return 0;
                    }
                    armour = GetDefencePower(MinMAC, MaxMAC);
                    break;
                case DefenceType.MAC:
                    armour = GetDefencePower(MinMAC, MaxMAC);
                    break;
                case DefenceType.Agility:
                    if (Envir.Random.Next(Agility + 1) > attacker.Accuracy)
                    {
                        BroadcastDamageIndicator(DamageType.Miss);
                        return 0;
                    }
                    break;
            }

            armour = (int)Math.Max(int.MinValue, (Math.Min(int.MaxValue, (decimal)(armour * ArmourRate))));
            damage = (int)Math.Max(int.MinValue, (Math.Min(int.MaxValue, (decimal)(damage * DamageRate))));

            if (damageWeapon)
                attacker.DamageWeapon();
            damage += attacker.AttackBonus;

            if ((attacker.CriticalRate * Settings.CriticalRateWeight) > Envir.Random.Next(100))
            {
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.Critical });
                damage = Math.Min(int.MaxValue, damage + (int)Math.Floor(damage * (((double)attacker.CriticalDamage / (double)Settings.CriticalDamageWeight) * 10)));
                BroadcastDamageIndicator(DamageType.Critical);
            }

            if (CurrentMap.DMGIncrease > 0 && Envir.Time < CurrentMap.EXPIncreaseDuration)
            {
                int temp = damage * CurrentMap.DMGIncrease / 100;
                damage += temp;
            }

                if (armour >= damage)
            {
                BroadcastDamageIndicator(DamageType.Miss);
                return 0;
            }

            if (attacker.LifeOnHit > 0)
                attacker.ChangeHP(attacker.LifeOnHit);

            if (Target != this && attacker.IsAttackTarget(this))
            {
                if (attacker.Info.MentalState == 2)
                {
                    if (Functions.MaxDistance(CurrentLocation, attacker.CurrentLocation) < (8 - attacker.Info.MentalStateLvl))
                        Target = attacker;
                }
                else
                    Target = attacker;
            }

            if (BindingShotCenter) ReleaseBindingShot();
            ShockTime = 0;

            for (int i = PoisonList.Count - 1; i >= 0; i--)
            {
                if (PoisonList[i].PType != PoisonType.LRParalysis) continue;

                PoisonList.RemoveAt(i);
                OperateTime = 0;
            }

            if (Master != null && Master != attacker)
                if (Envir.Time > Master.BrownTime && Master.PKPoints < 200)
                    attacker.BrownTime = Envir.Time + Settings.Minute;

            if (EXPOwner == null || EXPOwner.Dead)
                EXPOwner = attacker;
            //  Does it again here
            if (EXPOwner == attacker)
                EXPOwnerTime = Envir.Time + EXPOwnerDelay;

            ushort LevelOffset = (ushort)(Level > attacker.Level ? 0 : Math.Min(10, attacker.Level - Level));

            if (attacker.HasParalysisRing && type != DefenceType.MAC && type != DefenceType.MACAgility && 1 == Envir.Random.Next(1, 15))
            {
                ApplyPoison(new Poison { PType = PoisonType.Paralysis, Duration = 5, TickSpeed = 1000 }, attacker);
            }
            
            if (attacker.Freezing > 0 && type != DefenceType.MAC && type != DefenceType.MACAgility)
            {
                if ((Envir.Random.Next(Settings.FreezingAttackWeight) < attacker.Freezing) && (Envir.Random.Next(LevelOffset) == 0))
                    ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = Math.Min(10, (3 + Envir.Random.Next(attacker.Freezing))), TickSpeed = 1000 }, attacker);
            }

            if (attacker.PoisonAttack > 0 && type != DefenceType.MAC && type != DefenceType.MACAgility)
            {
                if ((Envir.Random.Next(Settings.PoisonAttackWeight) < attacker.PoisonAttack) && (Envir.Random.Next(LevelOffset) == 0))
                    ApplyPoison(new Poison { PType = PoisonType.Green, Duration = 5, TickSpeed = 1000, Value = Math.Min(10, 3 + Envir.Random.Next(attacker.PoisonAttack)) }, attacker);
            }
            if (Envir.Time > nextStruckTime)
            {
                nextStruckTime = Envir.Time + 800;
                Broadcast(new S.ObjectStruck { ObjectID = ObjectID, AttackerID = attacker.ObjectID, Direction = Direction, Location = CurrentLocation });
            }
            if (attacker.HpDrainRate > 0)
            {
                attacker.HpDrain += Math.Max(0, ((float)(damage - armour) / 100) * attacker.HpDrainRate);
                if (attacker.HpDrain > 2)
                {
                    int HpGain = (int)Math.Floor(attacker.HpDrain);
                    attacker.ChangeHP(HpGain);
                    attacker.HpDrain -= HpGain;

                }
            }

            attacker.GatherElement();

            if (attacker.Info.Mentor != 0 && attacker.Info.isMentor)
            {
                Buff buff = attacker.Buffs.Where(e => e.Type == BuffType.Mentor).FirstOrDefault();
                if (buff != null)
                {
                    CharacterInfo Mentee = Envir.GetCharacterInfo(attacker.Info.Mentor);
                    PlayerObject player = Envir.GetPlayer(Mentee.Name);
                    if (player != null)
                    {
                        if (player.CurrentMap == attacker.CurrentMap && Functions.InRange(player.CurrentLocation, attacker.CurrentLocation, Globals.DataRange) && !player.Dead)
                        {
                            damage += ( ( damage / 100 ) * Settings.MentorDamageBoost );
                        }
                    }
                }
            }

            if (Master != null && Master != attacker && Master.Race == ObjectType.Player && Envir.Time > Master.BrownTime && Master.PKPoints < 200 && !((PlayerObject)Master).AtWar(attacker))
            {
                attacker.BrownTime = Envir.Time + Settings.Minute;
            }

            for (int i = 0; i < attacker.Pets.Count; i++)
            {
                MonsterObject ob = attacker.Pets[i];

                if (IsAttackTarget(ob) && (ob.Target == null)) ob.Target = this;
            }

            switch(type)
            {
                case DefenceType.AC:
                case DefenceType.ACAgility:
                case DefenceType.Agility:
                    BroadcastDamageIndicator(DamageType.Hit, armour - damage);
                    break;

                case DefenceType.MAC:
                case DefenceType.MACAgility:
                    BroadcastDamageIndicator(DamageType.MagicalHit, armour - damage);
                    break;

                default:
                    BroadcastDamageIndicator(DamageType.Hit, armour - damage);
                    break;

            }


            //  20% chance to trigger weapon Effect
            if (Envir.Random.Next(0, 100) >= 80)
            {
                SpellEffect tmp = SpellEffect.None;
                if (attacker != null &&
                    attacker.Info.Equipment[(int)EquipmentSlot.Weapon] != null &&
                    attacker.Info.Equipment[(int)EquipmentSlot.Weapon].Info.WeaponEffects > 0)
                {
                    int eff = attacker.Info.Equipment[(int)EquipmentSlot.Weapon].Info.WeaponEffects;
                    switch (eff)
                    {
                        //  
                        case 1:
                            tmp = SpellEffect.Cake;
                            break;
                        case 2:
                            tmp = SpellEffect.BubbleEff;
                            break;
                        case 3:
                            tmp = SpellEffect.CircleEff;
                            break;
                        case 4:
                            tmp = SpellEffect.CrystalBeastBlast;
                            break;
                        case 5:
                            tmp = SpellEffect.CrystalBeastExplosion;
                            break;
                        case 6:
                            tmp = SpellEffect.CrystalBeastSpin;
                            break;
                        case 7:
                            tmp = SpellEffect.CrystalBeastSplash;
                            break;
                        case 8:
                            tmp = SpellEffect.ElectricExplosion;
                            break;
                        case 9:
                            tmp = SpellEffect.Entrapment;
                            break;
                        case 10:
                            tmp = SpellEffect.ExplosionEff;
                            break;
                        case 11:
                            tmp = SpellEffect.EyeEff;
                            break;
                        case 12:
                            tmp = SpellEffect.GFBHit;
                            break;
                        case 13:
                            tmp = SpellEffect.GoldenBubble;
                            break;
                        case 14:
                            tmp = SpellEffect.GreatFoxSpirit;
                            break;
                        case 15:
                            tmp = SpellEffect.GreenExplosion;
                            break;
                        case 16:
                            tmp = SpellEffect.PhoenixEff;
                            break;
                        case 17:
                            tmp = SpellEffect.PsnHit;
                            break;
                        case 18:
                            tmp = SpellEffect.PurpleBlastEff;
                            break;
                        case 19:
                            tmp = SpellEffect.PurpleExplosion;
                            break;
                        case 20:
                            tmp = SpellEffect.RainbowEff;
                            break;
                        case 21:
                            tmp = SpellEffect.RedMoonEvil;
                            break;
                        case 22:
                            tmp = SpellEffect.SFBHit;
                            break;
                        case 23:
                            tmp = SpellEffect.Shocking;
                            break;
                        case 24:
                            tmp = SpellEffect.Swirling;
                            break;
                        case 25:
                            tmp = SpellEffect.Stunned;
                            break;
                        case 26:
                            tmp = SpellEffect.TDBEff0;
                            break;
                        case 27:
                            tmp = SpellEffect.TDBEff1;
                            break;
                        case 28:
                            tmp = SpellEffect.TrapEff;
                            break;
                        case 29:
                            tmp = SpellEffect.UEnhance;
                            break;
                    }
                    if (tmp != SpellEffect.None)
                    {
                        Broadcast(new S.ObjectEffect { Effect = tmp, ObjectID = ObjectID });
                    }
                }
            }
            //  Get Attackers highest attack (Positive) Element
            ElementPos selEle = (ElementPos)GetHesighestPositiveElement(attacker);
            int tempEleDmg = 0;
            if (selEle != ElementPos.None)
            {
                //  Get #% from base damage
                tempEleDmg = damage * attacker.ElementsPos[(byte)selEle] / 100;
                damage += tempEleDmg;
                int tempEleNeg = 0;
                //  Check if mob has negative stat of the same element
                if (ElementsNeg[(byte)selEle] > 0)
                {
                    //  Add an extra #% to the base damage on top
                    tempEleNeg = damage * ElementsNeg[(byte)selEle] / 100;
                    //  Add the damage to the base damage
                    
                    damage += tempEleNeg;                    
                }
                //  Get the Elemental Damage type
                DamageType tmpType = DamageType.Miss;
                switch (selEle)
                {
                    case ElementPos.Air:
                        tmpType = DamageType.ElementAir;
                        break;
                    case ElementPos.Dark:
                        tmpType = DamageType.ElementDark;
                        break;
                    case ElementPos.Earth:
                        tmpType = DamageType.ElementEarth;
                        break;
                    case ElementPos.Fire:
                        tmpType = DamageType.ElementFire;
                        break;
                    case ElementPos.Holy:
                        tmpType = DamageType.ElementHoly;
                        break;
                    case ElementPos.Phantom:
                        tmpType = DamageType.ElementPhantom;
                        break;
                    case ElementPos.Water:
                        tmpType = DamageType.ElementWater;
                        break;
                    case ElementPos.Wind:
                        tmpType = DamageType.ElementWind;
                        break;
                }
                //  Broadcast the Elemental Damage bonus
                BroadcastDamageIndicator(tmpType, tempEleDmg + tempEleNeg);
            }

            if (Respawn != null && Respawn.IsEventObjective && attacker.tempEvent != null)
            {
                if (!Contributers.Contains(attacker))
                    Contributers.Add(attacker);
            }

            if (attacker != null &&
                attacker.Race == ObjectType.Player &&
                IsRaidBoss && Raid != null)
            {
                bool hasRank = false;
                for (int i = 0; i < Raid.PlayerStats.Count; i++)
                    if (Raid.PlayerStats[i].PlayerName == attacker.Name)
                    {
                        hasRank = true;
                        Raid.PlayerStats[i].DamageDealt += damage - armour;
                    }
                if (!hasRank)
                    Raid.PlayerStats.Add(new RaidStats { PlayerName = attacker.Name, DamageDealt = damage - armour });
            }
            ChangeHP(armour - damage);
            return damage - armour;
        }

        public byte GetHesighestPositiveElement(MonsterObject attacker)
        {
            byte temp = 0;
            for (int i = 0; i < 8; i++)
                if (attacker.ElementsPos[i] > temp)
                    temp = (byte)i;
            if (temp == 0 &&
                attacker.ElementsPos[temp] == 0)
                temp = 0;
            return temp;
        }

        public byte GetHesighestPositiveElement(PlayerObject attacker)
        {
            byte temp = 0;
            for (int i = 0; i < 8; i++)
                if (attacker.ElementsPos[i] > temp)
                    temp = (byte)i;
            if (temp == 0 &&
                attacker.ElementsPos[temp] == 0)
                temp = 0;
            return temp;
        }

        public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility)
        {
            if (Target == null && attacker.IsAttackTarget(this))
                Target = attacker;

            int armour = 0;
            switch (type)
            {
                case DefenceType.ACAgility:
                    if (Envir.Random.Next(Agility + 1) > attacker.Accuracy)
                    {
                        BroadcastDamageIndicator(DamageType.Miss);
                        return 0;
                    }
                    armour = GetDefencePower(MinAC, MaxAC);
                    break;
                case DefenceType.AC:
                    armour = GetDefencePower(MinAC, MaxAC);
                    break;
                case DefenceType.MACAgility:
                    if (Envir.Random.Next(Agility + 1) > attacker.Accuracy)
                    {
                        BroadcastDamageIndicator(DamageType.Miss);
                        return 0;
                    }
                    armour = GetDefencePower(MinMAC, MaxMAC);
                    break;
                case DefenceType.MAC:
                    armour = GetDefencePower(MinMAC, MaxMAC);
                    break;
                case DefenceType.Agility:
                    if (Envir.Random.Next(Agility + 1) > attacker.Accuracy)
                    {
                        BroadcastDamageIndicator(DamageType.Miss);
                        return 0;
                    }
                    break;
            }


            if (attacker.Race == ObjectType.Hero)
            {
                ushort LevelOffset = (ushort)(Level > attacker.Level ? 0 : Math.Min(10, attacker.Level - Level));
                if (attacker.Freezing > 0 && type != DefenceType.MAC && type != DefenceType.MACAgility)
                {
                    if ((Envir.Random.Next(Settings.FreezingAttackWeight) < attacker.Freezing) && (Envir.Random.Next(LevelOffset) == 0))
                        ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = Math.Min(10, (3 + Envir.Random.Next(attacker.Freezing))), TickSpeed = 1000 }, attacker);
                }

                if (attacker.PoisonAttack > 0 && type != DefenceType.MAC && type != DefenceType.MACAgility)
                {
                    if ((Envir.Random.Next(Settings.PoisonAttackWeight) < attacker.PoisonAttack) && (Envir.Random.Next(LevelOffset) == 0))
                        ApplyPoison(new Poison { PType = PoisonType.Green, Duration = 5, TickSpeed = 1000, Value = Math.Min(10, 3 + Envir.Random.Next(attacker.PoisonAttack)) }, attacker);
                }
            }


            armour = (int)Math.Max(int.MinValue, (Math.Min(int.MaxValue, (decimal)(armour * ArmourRate))));
            damage = (int)Math.Max(int.MinValue, (Math.Min(int.MaxValue, (decimal)(damage * DamageRate))));
            
            if (armour >= damage)
            {
                BroadcastDamageIndicator(DamageType.Miss);
                return 0;
            }

            if (Target != this && attacker.IsAttackTarget(this))
                Target = attacker;

            if (BindingShotCenter) ReleaseBindingShot();
            ShockTime = 0;

            for (int i = PoisonList.Count - 1; i >= 0; i--)
            {
                if (PoisonList[i].PType != PoisonType.LRParalysis) continue;

                PoisonList.RemoveAt(i);
                OperateTime = 0;
            }

            if (attacker.Info.AI == 6 || attacker.Info.AI == 58)
                EXPOwner = null;

            else if (attacker.Master != null)
            {
                if (!Functions.InRange(attacker.CurrentLocation, attacker.Master.CurrentLocation, Globals.DataRange))
                    EXPOwner = null;
                else
                {

                    if (EXPOwner == null || EXPOwner.Dead)
                        EXPOwner = attacker.Master;
                    //  And again
                    if (EXPOwner == attacker.Master)
                        EXPOwnerTime = Envir.Time + EXPOwnerDelay;
                }

            }
            if (Envir.Time > nextStruckTime)
            {
                nextStruckTime = Envir.Time + 800;
                Broadcast(new S.ObjectStruck { ObjectID = ObjectID, AttackerID = attacker.ObjectID, Direction = Direction, Location = CurrentLocation });
            }

            switch (type)
            {
                case DefenceType.AC:
                case DefenceType.ACAgility:
                case DefenceType.Agility:
                    BroadcastDamageIndicator(DamageType.Hit, armour - damage);
                    break;

                case DefenceType.MAC:
                case DefenceType.MACAgility:
                    BroadcastDamageIndicator(DamageType.MagicalHit, armour - damage);
                    break;

                default:
                    BroadcastDamageIndicator(DamageType.Hit, armour - damage);
                    break;

            }
            //  Get Attackers highest attack (Positive) Element
            ElementPos selEle = (ElementPos)GetHesighestPositiveElement(attacker);
            int tempEleDmg = 0;
            if (selEle != ElementPos.None)
            {
                //  Get #% from base damage
                tempEleDmg = damage * attacker.ElementsPos[(byte)selEle] / 100;
                damage += tempEleDmg;
                int tempEleNeg = 0;
                //  Check if mob has negative stat of the same element
                if (ElementsNeg[(byte)selEle] > 0)
                {
                    //  Add an extra #% to the base damage on top
                    tempEleNeg = damage * ElementsNeg[(byte)selEle] / 100;
                    //  Add the damage to the base damage

                    damage += tempEleNeg;
                }
                //  Get the Elemental Damage type
                DamageType tmpType = DamageType.Miss;
                switch (selEle)
                {
                    case ElementPos.Air:
                        tmpType = DamageType.ElementAir;
                        break;
                    case ElementPos.Dark:
                        tmpType = DamageType.ElementDark;
                        break;
                    case ElementPos.Earth:
                        tmpType = DamageType.ElementEarth;
                        break;
                    case ElementPos.Fire:
                        tmpType = DamageType.ElementFire;
                        break;
                    case ElementPos.Holy:
                        tmpType = DamageType.ElementHoly;
                        break;
                    case ElementPos.Phantom:
                        tmpType = DamageType.ElementPhantom;
                        break;
                    case ElementPos.Water:
                        tmpType = DamageType.ElementWater;
                        break;
                    case ElementPos.Wind:
                        tmpType = DamageType.ElementWind;
                        break;
                }
                //  Broadcast the Elemental Damage bonus
                BroadcastDamageIndicator(tmpType, tempEleDmg + tempEleNeg);
            }
            if (Respawn != null && Respawn.IsEventObjective && attacker.Master != null && attacker.Master is PlayerObject)
            {
                var playerAttacker = (PlayerObject)attacker.Master;
                if (!Contributers.Contains(playerAttacker) && playerAttacker.tempEvent != null)
                    Contributers.Add(playerAttacker);
            }
            ChangeHP(armour - damage);
            return damage - armour;
        }
        public long nextStruckTime;
        public override int Struck(int damage, DefenceType type = DefenceType.ACAgility)
        {

            int armour = 0;

            switch (type)
            {
                case DefenceType.ACAgility:
                    armour = GetDefencePower(MinAC, MaxAC);
                    break;
                case DefenceType.AC:
                    armour = GetDefencePower(MinAC, MaxAC);
                    break;
                case DefenceType.MACAgility:
                    armour = GetDefencePower(MinMAC, MaxMAC);
                    break;
                case DefenceType.MAC:
                    armour = GetDefencePower(MinMAC, MaxMAC);
                    break;
                case DefenceType.Agility:
                    break;
            }

            armour = (int)Math.Max(int.MinValue, (Math.Min(int.MaxValue, (decimal)(armour * ArmourRate))));
            damage = (int)Math.Max(int.MinValue, (Math.Min(int.MaxValue, (decimal)(damage * DamageRate))));

            if (armour >= damage) return 0;
            if (Envir.Time > nextStruckTime)
            {
                nextStruckTime = Envir.Time + 800;
                Broadcast(new S.ObjectStruck { ObjectID = ObjectID, AttackerID = 0, Direction = Direction, Location = CurrentLocation });

            }

            ChangeHP(armour - damage);
            return damage - armour;
        }

        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true)
        {
            if (p.Owner != null && p.Owner.IsAttackTarget(this))
                Target = p.Owner;

            if (Master != null && p.Owner != null && p.Owner.Race == ObjectType.Player && p.Owner != Master)
            {
                if (Envir.Time > Master.BrownTime && Master.PKPoints < 200)
                    p.Owner.BrownTime = Envir.Time + Settings.Minute;
            }

            if (!ignoreDefence && (p.PType == PoisonType.Green))
            {
                int armour = GetDefencePower(MinMAC, MaxMAC);

                if (p.Value < armour)
                    p.PType = PoisonType.None;
                else
                    p.Value -= armour;
            }

            if (p.PType == PoisonType.None) return;

            for (int i = 0; i < PoisonList.Count; i++)
            {
                if (PoisonList[i].PType != p.PType) continue;
                if ((PoisonList[i].PType == PoisonType.Green) && (PoisonList[i].Value > p.Value)) return;//cant cast weak poison to cancel out strong poison
                if ((PoisonList[i].PType != PoisonType.Green) && ((PoisonList[i].Duration - PoisonList[i].Time) > p.Duration)) return;//cant cast 1 second poison to make a 1minute poison go away!
                if (p.PType == PoisonType.DelayedExplosion) return;
                if ((PoisonList[i].PType == PoisonType.Frozen) || (PoisonList[i].PType == PoisonType.Slow) || (PoisonList[i].PType == PoisonType.Paralysis) || (PoisonList[i].PType == PoisonType.LRParalysis)) return;//prevents mobs from being perma frozen/slowed
                if (p.PType == PoisonType.Frozen &&
                    !Info.AllowFreeze) continue;
                if (p.PType == PoisonType.Slow &&
                    !Info.AllowSlow) continue;
                if (p.PType == PoisonType.Green &&
                    !Info.AllowGreen) continue;
                if (p.PType == PoisonType.Red &&
                    !Info.AllowRed) continue;
                if ((p.PType == PoisonType.Paralysis ||
                    p.PType == PoisonType.LRParalysis) &&
                    !Info.AllowPara) continue;
                if (p.PType == PoisonType.Burning &&
                    !Info.AllowBurning) continue;
                if (p.PType == PoisonType.Bleeding &&
                    !Info.AllowBleeding) continue;
                PoisonList[i] = p;
                return;
            }

            if (p.PType == PoisonType.DelayedExplosion)
            {
                ExplosionInflictedTime = Envir.Time + 4000;
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.DelayedExplosion });
            }
            if (p.PType == PoisonType.Frozen &&
                    !Info.AllowFreeze) return;
            if (p.PType == PoisonType.Slow &&
                !Info.AllowSlow) return;
            if (p.PType == PoisonType.Green &&
                !Info.AllowGreen) return;
            if (p.PType == PoisonType.Red &&
                !Info.AllowRed) return;
            if ((p.PType == PoisonType.Paralysis ||
                p.PType == PoisonType.LRParalysis) &&
                !Info.AllowPara) return;
            if (p.PType == PoisonType.Burning &&
                !Info.AllowBurning) return;
            if (p.PType == PoisonType.Bleeding &&
                !Info.AllowBleeding) return;
            PoisonList.Add(p);
        }
        public override void AddBuff(Buff b)
        {
            if (Buffs.Any(d => d.Infinite && d.Type == b.Type)) return; //cant overwrite infinite buff with regular buff

            string caster = b.Caster != null ? b.Caster.Name : string.Empty;

            if (b.Values == null) b.Values = new int[1];

            S.AddBuff addBuff = new S.AddBuff { Type = b.Type, Caster = caster, Expire = b.ExpireTime - Envir.Time, Values = b.Values, Infinite = b.Infinite, ObjectID = ObjectID, Visible = b.Visible };

            if (b.Visible) Broadcast(addBuff);

            base.AddBuff(b);
            RefreshAll();
        }

        public Packet GetMobInfoWQ(bool hasQuest = false)
        {
            byte mobType = 0;
            if (hasQuest)
                mobType = 1;
            else if (Respawn != null && Respawn.IsEventObjective && Respawn.Event != null)
                mobType = 2;
            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = Info.Image,
                Direction = Direction,
                Effect = Info.Effect,
                AI = Info.AI,
                Light = Info.Light,
                Dead = Dead,
                Skeleton = Harvested,
                Poison = CurrentPoison,
                Hidden = Hidden,
                ShockTime = (ShockTime > 0 ? ShockTime - Envir.Time : 0),
                BindingShotCenter = BindingShotCenter,
                IsBoss = Info.IsBoss,
                GlowAura = Info.LightColar,
                IsElite = Info.CanBeElite,
                AC = MinAC,
                MAC = MinMAC,
                DC = MinDC,
                MC = MinMC,
                SC = MinSC,
                MaxAC = MaxAC,
                MaxMAC = MaxMAC,
                MaxDC = MaxDC,
                MaxMC = MaxMC,
                MaxSC = MaxSC,
                Acc = Info.Accuracy,
                Agil = Info.Agility,
                AttkSpeed = Info.AttackSpeed,
                Health = MaxHP,
                CurrentHP = Health,
                IsPet = Master == null ? false : true,
                LightEffect = Info.LightEffect,
                Level = Info.Level,
                IsTamable = Info.CanTame,
                IsPushable = Info.CanPush,
                IsUndead = Info.Undead,
                MoveSpeed = Info.MoveSpeed,
                Experience = Info.Experience,
                IsSub = Info.IsSub,
                MobType = mobType,
                PetEnhancer = HasPetEnhancer()
            };
        }
        
        public override Packet GetInfo()
        {
            byte mobType = 0;
            if (Respawn != null &&
                Respawn.IsEventObjective &&
                Respawn.Event != null)
                mobType = 2;

            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Location = CurrentLocation,
                Image = Info.Image,
                Direction = Direction,
                Effect = Info.Effect,
                AI = Info.AI,
                Light = Info.Light,
                Dead = Dead,
                Skeleton = Harvested,
                Poison = CurrentPoison,
                Hidden = Hidden,
                ShockTime = (ShockTime > 0 ? ShockTime - Envir.Time : 0),
                BindingShotCenter = BindingShotCenter,
                IsBoss = Info.IsBoss,
                GlowAura = Info.LightColar,
                IsElite = Info.CanBeElite,
                AC = MinAC,
                MAC = MinMAC,
                DC = MinDC,
                MC = MinMC,
                SC = MinSC,
                MaxAC = MaxAC,
                MaxMAC = MaxMAC,
                MaxDC = MaxDC,
                MaxMC = MaxMC,
                MaxSC = MaxSC,
                Acc = Info.Accuracy,
                Agil = Info.Agility,
                AttkSpeed = Info.AttackSpeed,
                Health = MaxHP,
                CurrentHP = HP,
                IsPet = Master == null ? false : true,
                LightEffect = Info.LightEffect,
                Level = Info.Level,
                IsTamable = Info.CanTame,
                IsPushable = Info.CanPush,
                IsUndead = Info.Undead,
                MoveSpeed = Info.MoveSpeed,
                Experience = Info.Experience,
                IsSub = Info.IsSub,
                MobType = mobType,
                PetEnhancer = HasPetEnhancer()
            };
        }

        public override void ReceiveChat(string text, ChatType type, List<UserItem> items = null)
        {
            throw new NotSupportedException();
        }

        public void RemoveObjects(MirDirection dir, int count)
        {
            switch (dir)
            {
                case MirDirection.Up:
                    //Bottom Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y + Globals.DataRange - a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
                case MirDirection.UpRight:
                    //Bottom Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y + Globals.DataRange - a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }

                    //Left Block
                    for (int a = -Globals.DataRange; a <= Globals.DataRange - count; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X - Globals.DataRange + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
                case MirDirection.Right:
                    //Left Block
                    for (int a = -Globals.DataRange; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X - Globals.DataRange + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
                case MirDirection.DownRight:
                    //Top Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y - Globals.DataRange + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }

                    //Left Block
                    for (int a = -Globals.DataRange + count; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X - Globals.DataRange + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
                case MirDirection.Down:
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y - Globals.DataRange + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
                case MirDirection.DownLeft:
                    //Top Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y - Globals.DataRange + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }

                    //Right Block
                    for (int a = -Globals.DataRange + count; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X + Globals.DataRange - b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
                case MirDirection.Left:
                    for (int a = -Globals.DataRange; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X + Globals.DataRange - b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
                case MirDirection.UpLeft:
                    //Bottom Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y + Globals.DataRange - a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }

                    //Right Block
                    for (int a = -Globals.DataRange; a <= Globals.DataRange - count; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X + Globals.DataRange - b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Remove(this);
                            }
                        }
                    }
                    break;
            }
        }
        public void AddObjects(MirDirection dir, int count)
        {
            switch (dir)
            {
                case MirDirection.Up:
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y - Globals.DataRange + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
                case MirDirection.UpRight:
                    //Top Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y - Globals.DataRange + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }

                    //Right Block
                    for (int a = -Globals.DataRange + count; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X + Globals.DataRange - b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
                case MirDirection.Right:
                    for (int a = -Globals.DataRange; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X + Globals.DataRange - b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
                case MirDirection.DownRight:
                    //Bottom Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y + Globals.DataRange - a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }

                    //Right Block
                    for (int a = -Globals.DataRange; a <= Globals.DataRange - count; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X + Globals.DataRange - b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
                case MirDirection.Down:
                    //Bottom Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y + Globals.DataRange - a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
                case MirDirection.DownLeft:
                    //Bottom Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y + Globals.DataRange - a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }

                    //Left Block
                    for (int a = -Globals.DataRange; a <= Globals.DataRange - count; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X - Globals.DataRange + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
                case MirDirection.Left:
                    //Left Block
                    for (int a = -Globals.DataRange; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X - Globals.DataRange + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
                case MirDirection.UpLeft:
                    //Top Block
                    for (int a = 0; a < count; a++)
                    {
                        int y = CurrentLocation.Y - Globals.DataRange + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = -Globals.DataRange; b <= Globals.DataRange; b++)
                        {
                            int x = CurrentLocation.X + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }

                    //Left Block
                    for (int a = -Globals.DataRange + count; a <= Globals.DataRange; a++)
                    {
                        int y = CurrentLocation.Y + a;
                        if (y < 0 || y >= CurrentMap.Height) continue;

                        for (int b = 0; b < count; b++)
                        {
                            int x = CurrentLocation.X - Globals.DataRange + b;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Player) continue;
                                ob.Add(this);
                            }
                        }
                    }
                    break;
            }
        }

        public override void Add(PlayerObject player)
        {
            bool qMob = false;
            //this...
           for(int i = 0; i < player.CurrentQuests.Count; i++)
            {
                if (player.CurrentQuests[i].KillTaskCount.Count > 0 &&
                    player.CurrentQuests[i].NeedKill(Info))
                    qMob = true;
            }
            player.Enqueue(GetMobInfoWQ(qMob));
            SendHealth(player);
        }

        public bool HasPetEnhancer()
        {
            bool petEnhanced = false;
            if (Buffs != null &&
                Buffs.Count > 0 &&
                Master != null &&
                Master.Race == ObjectType.Player)
            {
                for (int i = 0; i < Buffs.Count; i++)
                    if (Buffs[i].Type == BuffType.PetEnhancer)
                        petEnhanced = true;
            }
            return petEnhanced;
        }

        public override void SendHealth(PlayerObject player)
        {
            byte mobType = 0;
            for (int i = 0; i < player.CurrentQuests.Count; i++)
            {
                if (player.CurrentQuests[i].KillTaskCount.Count > 0 &&
                    player.CurrentQuests[i].NeedKill(Info))
                    mobType = 1;
            }
            if (mobType == 0)
            {
                if (Respawn != null && Respawn.IsEventObjective && Respawn.Event != null)
                    mobType = 2;
            }
            
            //  Is a Boss and not a Sub
            if (Info.IsBoss && !Info.IsSub)
            {
                byte time = byte.MaxValue;


                player.Enqueue(new S.ObjectHealth { ObjectID = ObjectID, Percent = PercentHealth, Expire = time, MobType = mobType, PetEnhancer = HasPetEnhancer() });
                return;
            }
            else
            {
                if (!player.IsMember(Master) && !(player.IsMember(EXPOwner) && AutoRev) && Envir.Time > RevTime)
                    return;
                byte time = Math.Min(byte.MaxValue, (byte)Math.Max(5, (RevTime - Envir.Time) / 1000));
                player.Enqueue(new S.ObjectHealth { ObjectID = ObjectID, Percent = PercentHealth, Expire = time, MobType = mobType, PetEnhancer = HasPetEnhancer() });
            }
        }

        public void PetExp(uint amount)
        {
            if (PetLevel >= MaxPetLevel) return;

            if (Info.Name == Settings.SkeletonName ||
                Info.Name == Settings.ShinsuName ||
                Info.Name == Settings.AngelName ||
                Info.Name == Settings.SuperSkeletonName1 ||
                Info.Name == Settings.SuperSkeletonName2 ||
                Info.Name == Settings.SuperSkeletonName3 ||
                Info.Name == Settings.SuperShinsuName ||
                Info.Name == Settings.SuperShinsuName2 ||
                Info.Name == Settings.SuperShinsuName4 ||
                Info.Name == Settings.DragonName ||
                Info.Name == Settings.DragonName1)
                amount *= 3;

            PetExperience += amount;

            if (PetExperience < (PetLevel + 1)*20000) return;

            PetExperience = (uint) (PetExperience - ((PetLevel + 1)*20000));
            PetLevel++;
            RefreshAll();
            OperateTime = 0;
            BroadcastHealthChange();
        }
        public override void Despawn()
        {
            SlaveList.Clear();
            base.Despawn();
        }


        public MapObject FindWeakTarget(List<MapObject> targets, MirClass targetClass = MirClass.NONE)
        {
            MapObject tmpOb = null;
            int curLowest = 100;
            int curLowestIndex = -1;
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] is PlayerObject tmppOb)
                {
                    if (targetClass != MirClass.NONE)
                    {
                        if (tmppOb.Class == targetClass)
                        {
                            if (tmppOb.PercentHealth < curLowest)
                            {
                                curLowest = tmppOb.PercentHealth;
                                curLowestIndex = i;
                            }
                        }
                    }
                    else
                    {
                        if (tmppOb.PercentHealth < curLowest)
                        {
                            curLowest = tmppOb.PercentHealth;
                            curLowestIndex = i;
                        }
                    }
                }
            }
            if (curLowest != 100 &&
                curLowestIndex != -1)
                tmpOb = targets[curLowestIndex];
            return tmpOb;
        }

        /// <summary>
        /// Find a weak target (lowest HP)
        /// </summary>
        /// <param name="location">Starting location in which to search</param>
        /// <param name="range">Range of search</param>
        /// <param name="targetClass">Default : None</param>
        /// <returns>Single Target</returns>
        public MapObject FindWeakTarget(Point location, int range, MirClass targetClass = MirClass.NONE, bool needSight = false)
        {
            MapObject tmpOb = null;
            tmpOb = FindWeakTarget(FindAllTargets(range, location), targetClass);
            return tmpOb;
        }

        /// <summary>
        /// Retrive a list of Objects (Players) of the class called
        /// </summary>
        /// <param name="location">Start search point</param>
        /// <param name="range">Range to search</param>
        /// <param name="targetClass">The Class to target (using NONE will just do the same as FindAllTargets)</param>
        /// <returns></returns>
        public List<MapObject> TargetClass(Point location, int range, MirClass targetClass, bool needSight = false)
        {
            List<MapObject> tmpL = FindAllTargets(range, location, needSight);
            for (int i = tmpL.Count - 1; i >= 0; i--)
            {
                if (tmpL[i] is PlayerObject tmpOb)
                {
                    if (tmpOb.Class != targetClass)
                        tmpL.RemoveAt(i);
                }
            }
            return tmpL;
        }
    }
}
