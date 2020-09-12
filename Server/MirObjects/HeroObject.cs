
using Server.MirDatabase;
using System;
using System.Collections.Generic;
using Server.MirEnvir;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S = ServerPackets;
using System.Drawing;
using System.IO;
using Server.MirObjects.Monsters;

namespace Server.MirObjects
{
    public class GuardSpot
    {
        public Point GuardLocation = Point.Empty;
        public Map GuardMap = null;
    }

    public class AutoPotSystem
    {
        public bool isEnabled;

        public List<AutoPot> AutoPotList = new List<AutoPot>();

        public AutoPotSystem()
        {
            var HPPot = new AutoPot()
            {
                AutoPotType = 1,
            };
            AutoPotList.Add(HPPot);

            var MPPot = new AutoPot()
            {
                AutoPotType = 2,
            };
            AutoPotList.Add(MPPot);         

        }
        public void AllowAutoPot()
        {
            isEnabled = true;
        }

    }

    public class AutoPot
    {
        public byte AutoPotType = 0;
        public int ItemIndex = -1;
        public byte PercentTrigger = 20;
        public long Delay = 5000;
        public long Timer = SMain.Envir.Time;
    }

    public class HeroObject : MonsterObject
    {
        public UserItem[] Equipment = new UserItem[18];
        public UserItem[] Inventory = new UserItem[10];
        public UserItem[] QuestInventory = new UserItem[40];
        public List<UserMagic> Magics = new List<UserMagic>();
        public List<ItemSets> ItemSets = new List<ItemSets>();
        public List<EquipmentSlot> MirSet = new List<EquipmentSlot>();
        public UserItem HP_PotionToUse = null;
        public UserItem MP_PotionToUse = null;
        public UserItem InstanPotionToUse = null;
        public AutoPotSystem autoPotSystem = new AutoPotSystem();
        public int HeroLevel = 0;
        public long HeroCurrentEXP = 0;
        public long NeedExp = 0;
        public string HeroName = "Hero";
        public MirClass HeroClass;
        public MirGender HeroGender;
        public byte HeroHair;
        public bool Summoned;
        public uint HeroCurrentHealth = 0;
        public int HeroCurrenMana = 0;
        public int Version
        { get { return PlayerMaster.Info.HeroLoadVersion; }
        }
        public int customVersion = int.MaxValue;
        public uint MaxMP, MP;
        public uint OriginalMP, OriginalHP;
        public bool isSpawn, AutoSpawn = true;
        public long MaxExperience;
        public long HeroExperience = 0;
        public long fearTime;
        public bool isLocked = false;

        public Spell HeroNextSpell;
        public MapObject HeroNextTarget;
        public Point HeroNextTargetLocation;
        public HeroBehaviour HeroBehaviour = HeroBehaviour.None;

        public Spell comboSpell;
        public Point comboLocation = Point.Empty;
        public MapObject comboTarget = null;


        public MapObject HarvestTarget = null;
        public long HarvestTime = SMain.Envir.Time;
        public long HarvestDelay = SMain.Envir.Time;


        public new List<PlayerObject> GroupMembers
        {
            get { return PlayerMaster.GroupMembers; }
        }

        public sbyte PercentMana
        {
            get { return (sbyte)(MP / (float)MaxMP * 100); }

        }

        public bool CanUsePercentPot
        {
            get { return Envir.Time >= PercentCoolTime; }
        }

        public long PercentCoolTime;

        public short Weapon;
        public short Armour;
        public byte WingEffect;
        public byte WeaponEffect;
        private long PotTime;
        public bool MagicShieldUp = false;
        public long MagicShieldTime;
        public long SoulShieldDelay = 0;
        public long ArmourShield = 0;

        public bool MonsterAttacked = false;

        public Point GoToLocation = Point.Empty;
        public byte GoToAttempts = 0;
        public const byte GoToMax = 10;

        public bool HalfMoonOn = false, CrossHalfMoonOn = false, ThrustingOn = false, SlayingOn = false, DoubleSlashOn = false, Hemorrhage, LavaKingCasting;
        public int HemorrhageAttackCount;
        public byte HeroStage = 0;

        public GuardSpot guardSpot;
        private const long DuraDelay = 10000;

        public PlayerObject PlayerMaster
        {
            get
            {
                return Master != null ? ((PlayerObject)Master) : null;
            }
        }

        /// <summary>
        /// We'll use a new race to make it easier to call upon futher down the line
        /// </summary>
        public override ObjectType Race
        {
            get { return ObjectType.Hero; }
        }
        /// <summary>
        /// Override the Naming method in order for us to be able to allow players to give them their own name.
        /// </summary>
        public override string Name
        {
            get { return Master == null ? HeroName : ( Dead ? HeroName : string.Format("{0}_{1}'s Hero", HeroName, Master.Name) ); }
            set { throw new NotSupportedException(); }
        }

        protected override bool CanMove
        {
            get
            {
                return !Dead && Envir.Time > MoveTime && Envir.Time > ActionTime && Envir.Time > ShockTime &&
                       (Master == null || Master.HMode != HeroMode.DontMove) && !CurrentPoison.HasFlag(PoisonType.Paralysis) && !CurrentPoison.HasFlag(PoisonType.Trap)
                       && !CurrentPoison.HasFlag(PoisonType.LRParalysis)  && !CurrentPoison.HasFlag(PoisonType.Frozen);
            }
        }
        protected override bool CanAttack
        {
            get
            {
                return !Dead && Envir.Time > AttackTime && Envir.Time > ActionTime &&
                     (Master == null || Master.HMode != HeroMode.DontAttack || !InSafeZone || !CurrentMap.Info.NoFight) && !CurrentPoison.HasFlag(PoisonType.Paralysis)
                       && !CurrentPoison.HasFlag(PoisonType.LRParalysis) && !CurrentPoison.HasFlag(PoisonType.Stun) && !CurrentPoison.HasFlag(PoisonType.Frozen);
            }
        }

        public long TorchTime { get; private set; }
        public float ExpRateOffset { get; set; }
        public long DuraTime { get; private set; }
        public bool NoDuraLoss { get; private set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        /// <summary>
        /// Initialize the Hero
        /// </summary>
        /// <param name="info"></param>
        public HeroObject(MonsterInfo info)
            :base(info)
        {
            
            //  Apply the Versions for Load/Save
            customVersion = Envir.CustomVersion;

        }

        public override int Attacked(MonsterObject attacker, int damage, DefenceType type = DefenceType.ACAgility)
        {
            if (attacker.Master != null && attacker.Master.Race == ObjectType.Player && Envir.Time > BrownTime && PKPoints < 200 && attacker.Race == ObjectType.Hero)
                attacker.BrownTime = Envir.Time + Settings.Minute;

            MonsterAttacked = true;

            DamageDura();
            if (MagicShieldUp)
            {
                int temp = damage * 15 / 100;
                damage -= temp;
            }
            return base.Attacked(attacker, damage, type);
        }

        public override int Attacked(PlayerObject attacker, int damage, DefenceType type = DefenceType.ACAgility, bool damageWeapon = true)
        {
            if (Envir.Time > BrownTime && PKPoints < 200 && !PlayerMaster.AtWar(attacker))
                attacker.BrownTime = Envir.Time + Settings.Minute;

            DamageDura();

            if (attacker != null)
                attacker.DamageWeapon();


            if (MagicShieldUp)
            {
                int temp = damage * 15 / 100;
                damage -= temp;
            }
            return base.Attacked(attacker, damage, type, damageWeapon);
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
                case DelayedType.Recall:
                    PetRecall();
                    break;
                case DelayedType.Magic:
                    CompleteMagic(action.Params);
                    break;
            }
        }

        private void CompleteMagic(IList<object> data)
        {
            Spell magic = (Spell)data[0];
            int value = 0;
            int magicLevel = 0;
            //int value;
            MapObject target;
            //Point location;
            //MonsterObject monster;

            UserMagic usrMagic = Magics.FirstOrDefault(x => x.Spell == magic);

            if (usrMagic == null) return;

            switch (magic)
            {
                case Spell.FireBall:
                    value = (int)data[1];
                    target = (MapObject)data[2];

                    if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;
                    if (target.Attacked(this, value, DefenceType.MAC) > 0) LevelMagic(usrMagic);
                    break;
                case Spell.FrostCrunch:
                    value = (int)data[1];
                    target = (MapObject)data[2];

                    if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;
                    if (target.Attacked(this, value, DefenceType.MAC) > 0)
                    {
                        if (Level + (target.Race == ObjectType.Player ? 2 : 10) >= target.Level && Envir.Random.Next(target.Race == ObjectType.Player ? 100 : 20) <= usrMagic.Level * 2)
                        {
                            target.ApplyPoison(new Poison
                            {
                                Owner = this,
                                Duration = target.Race == ObjectType.Player ? 4 : 5 + Envir.Random.Next(5),
                                PType = PoisonType.Slow,
                                TickSpeed = 1000,
                            }, this);
                            target.OperateTime = 0;
                        }

                        if (Level + (target.Race == ObjectType.Player ? 2 : 10) >= target.Level && Envir.Random.Next(target.Race == ObjectType.Player ? 100 : 40) <= usrMagic.Level * 2)
                        {
                            target.ApplyPoison(new Poison
                            {
                                Owner = this,
                                Duration = target.Race == ObjectType.Player ? 2 : 5 + Envir.Random.Next(Freezing),
                                PType = PoisonType.Frozen,
                                TickSpeed = 1000,
                            }, this);
                            target.OperateTime = 0;
                        }
                    }
                    break;
                #region DragonFlames  Wizard

                case Spell.DragonFlames:

                    Broadcast(new S.MapEffect { Effect = SpellEffect.DragonFlamesEffect, Location = comboLocation, Value = (byte)Direction });

                    for (int y = comboLocation.Y - 2; y <= comboLocation.Y + 2; y++)
                    {
                        if (y < 0) continue;
                        if (y >= CurrentMap.Height) break;

                        for (int x = comboLocation.X - 2; x <= comboLocation.X + 2; x++)
                        {
                            if (x < 0) continue;
                            if (x >= CurrentMap.Width) break;

                            var cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                target = cell.Objects[i];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:
                                        //Only targets
                                        if (target.IsAttackTarget(this))
                                        {
                                            target.Attacked(this, usrMagic.GetDamage(GetAttackPower(MinMC, MaxMC)) ,DefenceType.MAC);

                                        }
                                        break;
                                }
                            }

                        }

                    }
                    LevelMagic(usrMagic);
                    break;

                #endregion

                #region BrokenSoulCut Warrior

                case Spell.BrokenSoulCut:

                    Broadcast(new S.MapEffect { Effect = SpellEffect.BrokenSoulCutEffect, Location = CurrentLocation, Value = (byte)Direction });
                    var newLocation = CurrentLocation;

                    for (int i = 0; i < 6; i++)
                    {
                        newLocation = Functions.PointMove(newLocation, Direction, 1);

                        if (!CurrentMap.ValidPoint(newLocation)) continue;

                        var cell = CurrentMap.GetCell(newLocation);

                        if (cell.Objects == null) continue;

                        for (int o = 0; o < cell.Objects.Count; o++)
                        {
                            target = cell.Objects[o];
                            if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;

                            if (!target.IsAttackTarget(this)) continue;
                            target.Attacked(this, usrMagic.GetDamage(GetAttackPower(MinDC, MaxDC)), DefenceType.MAC);
                        }
                    }

                    LevelMagic(usrMagic);
                    break;

                #endregion

                #region LastJudgement Taoist

                case Spell.LastJudgement:

                    Broadcast(new S.MapEffect { Effect = SpellEffect.LastJudgementEffect, Location = comboLocation, Value = (byte)Direction });

                    for (int y = comboLocation.Y - 2; y <= comboLocation.Y + 2; y++)
                    {
                        if (y < 0) continue;
                        if (y >= CurrentMap.Height) break;

                        for (int x = comboLocation.X - 2; x <= comboLocation.X + 2; x++)
                        {
                            if (x < 0) continue;
                            if (x >= CurrentMap.Width) break;

                            var cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                target = cell.Objects[i];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:
                                        //Only targets
                                        if (target.IsAttackTarget(this))
                                        {
                                            target.Attacked(this, usrMagic.GetDamage(GetAttackPower(MinSC, MaxSC)), DefenceType.MAC);

                                            if (Envir.Random.Next(100) > 25)
                                                Target.ApplyPoison(new Poison
                                                {
                                                    Owner = this,
                                                    Duration = 3 + usrMagic.Level,
                                                    PType = PoisonType.Stun,
                                                    TickSpeed = 1000,
                                                }, this);
                                            Target.OperateTime = 0;

                                        }
                                        break;
                                }
                            }

                        }

                    }
                    LevelMagic(usrMagic);
                    break;

                #endregion

                #region ThunderClap Wizard
                case Spell.ThunderClap:

                    Broadcast(new S.MapEffect { Effect = SpellEffect.ThunderClapEffect, Location = comboLocation, Value = (byte)Direction });

                    for (int y = comboLocation.Y - 1; y <= comboLocation.Y + 1; y++)
                    {
                        if (y < 0) continue;
                        if (y >= CurrentMap.Height) break;

                        for (int x = comboLocation.X - 1; x <= comboLocation.X + 1; x++)
                        {
                            if (x < 0) continue;
                            if (x >= CurrentMap.Width) break;

                            var cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                target = cell.Objects[i];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:
                                        //Only targets
                                        if (target.IsAttackTarget(this))
                                        {
                                            if (x == 0 && y == 0)
                                                target.Attacked(this, (int)(GetAttackPower(MinMC, MaxMC) * 1.5f), DefenceType.MAC);
                                            else
                                                target.Attacked(this, usrMagic.GetDamage(GetAttackPower(MinMC, MaxMC)), DefenceType.MAC);

                                        }
                                        break;
                                }
                            }

                        }

                    }
                    LevelMagic(usrMagic);
                    break;
                #endregion

                #region ChopChopStar Warrior
                case Spell.ChopChopStar:

                    if (comboTarget != null && !comboTarget.Dead && comboTarget.IsAttackTarget(this))
                    {
                        Broadcast(new S.ObjectEffect { ObjectID = comboTarget.ObjectID, Effect = SpellEffect.ChopChopStarEffect });

                        comboTarget.Attacked(this, GetAttackPower(MinDC, MaxDC), DefenceType.MAC);
                        var action = new DelayedAction(DelayedType.Damage, Envir.Time + 1000, comboTarget, usrMagic.GetDamage(GetAttackPower(MinDC, MaxDC)), DefenceType.MAC);
                        ActionList.Add(action);
                    }



                    LevelMagic(usrMagic);
                    break;

                #endregion

                #region SoulEaterSwamp Taoist
                case Spell.SoulEaterSwamp:

                    if (comboTarget != null && !comboTarget.Dead && comboTarget.IsAttackTarget(this))
                    {
                        Broadcast(new S.ObjectEffect { ObjectID = ObjectID, TargetID = comboTarget.ObjectID, Effect = SpellEffect.SoulEaterSwampEffect });


                        for (int y = comboLocation.Y - 2; y <= comboLocation.Y + 2; y++)
                        {
                            if (y < 0) continue;
                            if (y >= CurrentMap.Height) break;

                            for (int x = comboLocation.X - 2; x <= comboLocation.X + 2; x++)
                            {
                                if (x < 0) continue;
                                if (x >= CurrentMap.Width) break;

                                var cell = CurrentMap.GetCell(x, y);

                                if (!cell.Valid || cell.Objects == null) continue;

                                for (int i = 0; i < cell.Objects.Count; i++)
                                {
                                    target = cell.Objects[i];
                                    switch (target.Race)
                                    {
                                        case ObjectType.Monster:
                                        case ObjectType.Player:
                                        case ObjectType.Hero:
                                            //Only targets
                                            if (target.IsAttackTarget(this))
                                            {
                                                target.Attacked(this, GetAttackPower(MinSC, MaxSC), DefenceType.MAC);

                                            }
                                            break;
                                    }
                                }

                            }

                        }

                    }

                    LevelMagic(usrMagic);
                    break;

                #endregion

                #region HandOfGod Assassin
                case Spell.HandOfGod:

                    if (comboTarget != null && !comboTarget.Dead && comboTarget.IsAttackTarget(this))
                    {
                        Broadcast(new S.ObjectEffect { ObjectID = comboTarget.ObjectID, TargetID = comboTarget.ObjectID, Effect = SpellEffect.HandOfGodEffect });

                        var action = new DelayedAction(DelayedType.Damage, Envir.Time +  500, comboTarget, GetAttackPower(MinDC, MaxDC), DefenceType.MAC);
                        ActionList.Add(action);


                    }

                    LevelMagic(usrMagic);
                    break;

                #endregion

                #region SoulReaper Assassin
                case Spell.SoulReaper:

                   var show = true;

                    for (int y = comboLocation.Y - 2; y <= comboLocation.Y + 2; y++)
                    {
                        if (y < 0)
                            continue;
                        if (y >= CurrentMap.Height)
                            break;

                        for (int x = comboLocation.X - 2; x <= comboLocation.X + 2; x++)
                        {
                            if (x < 0)
                                continue;
                            if (x >= CurrentMap.Width)
                                break;

                           var cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid)
                                continue;

                            bool cast = true;
                            if (cell.Objects != null)
                                for (int o = 0; o < cell.Objects.Count; o++)
                                {
                                    target = cell.Objects[o];
                                    if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.MobMeteorStrike)
                                        continue;

                                    cast = false;
                                    break;
                                }

                            if (!cast)
                                continue;

                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.SoulReaper,
                                Value = GetAttackPower(MinDC, MaxDC),
                                ExpireTime = Envir.Time + 4000  + (2000 * usrMagic.Level),
                                TickSpeed = 1000,
                                MobCaster = this,
                                CurrentLocation = new Point(x, y),
                                CastLocation = comboLocation,
                                Show = show,
                                CurrentMap = CurrentMap,
                                StartTime = Envir.Time + 500,
                            };

                            show = false;

                            CurrentMap.AddObject(ob);
                            ob.Spawned();
                        }
                    }

                    break;
                #endregion

                #region Plague
                case Spell.Plague:
                    {

                        value = (int)data[1];
                        target = (MapObject)data[2];
                        PoisonType tmp = (PoisonType)data[3];
                        magicLevel = (int)data[4];
                        if (target.IsAttackTarget(this))
                        {
                            
                            PoisonType poison;
                            int chance = Envir.Random.Next(6000);
                            if (chance >= 0 && chance < 1000)
                                poison = PoisonType.Slow;
                            else if (chance >= 1000 && chance < 2000)
                                poison = PoisonType.Frozen;
                            else if (chance >= 2000 && chance < 4000)
                                poison = tmp;
                            else
                                poison = PoisonType.None;

                            int tempValue = 0;

                            if (poison == PoisonType.Green)
                            {
                                tempValue = value / 15 + magicLevel + 1;
                            }
                            else
                            {
                                tempValue = value + (magicLevel + 1) * 2;
                            }

                            if (poison != PoisonType.None)
                            {
                                target.ApplyPoison(new Poison { PType = poison, Duration = (2 * (magicLevel  + 1)) + (value / 10), TickSpeed = 1000, Value = tempValue, Owner = this }, this, false, false);
                            }

                            if (target.Race == ObjectType.Player)
                            {
                                PlayerObject tempOb = (PlayerObject)target;

                                tempOb.ChangeMP(-tempValue);
                            }
                        }
                    }
                    break;
                #endregion

                #region HeadShot
                case Spell.HeadShot:
                    value = (int)data[1];
                    target = (MapObject)data[2];
                    magicLevel = (int)data[3];
                    if (target == null || !target.IsAttackTarget(this) || target.CurrentMap != CurrentMap || target.Node == null) return;
                    if (target.Attacked(this, value, DefenceType.MAC) > 0)

                    if (Envir.Random.Next(10 - magicLevel) < 2)
                    {
                        target.ApplyPoison(new Poison
                        {
                            Duration = 5 + (magicLevel + 1),
                            Owner = this,
                            PType = PoisonType.Bleeding,
                            TickSpeed = 1000,
                            Value = 0
                        }, this);
                    }
                    break;
            }
            #endregion
        }
        public long RefreshInfoTime;
        protected override void ProcessAI()
        {
            if (Dead || !isSpawn) return;
            if (MagicShieldUp && Envir.Time > MagicShieldTime)
            {
                Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.MagicShieldDown });
                MagicShieldUp = false;
            }

            if (Master != null)
            {
                if (Master.HMode != HeroMode.DontMove)
                {
                    if (!Functions.InRange(CurrentLocation, Master.CurrentLocation, Globals.DataRange) || CurrentMap != Master.CurrentMap)
                        PetRecall();
                }

                if (Master.HMode == HeroMode.DontAttack)
                    Target = null;
            }

            UserItem item;
            if (Envir.Time > TorchTime)
            {
                TorchTime = Envir.Time + 10000;
                item = Equipment[(int)EquipmentSlot.Torch];
                if (item != null)
                {
                    DamageItem(item, 5);

                    if (item.CurrentDura == 0)
                    {
                        Equipment[(int)EquipmentSlot.Torch] = null;
                        PlayerMaster.Enqueue(new S.DeleteItem { UniqueID = item.UniqueID, Count = item.Count });
                        RefreshStats();
                    }
                }
            }
            if (Envir.Time > RefreshInfoTime)
            {
                RefreshInfoTime = Envir.Time + 500;// 500 = half a second

                for (int y = CurrentLocation.Y - Globals.DataRange; y <= CurrentLocation.Y + Globals.DataRange; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;

                    for (int x = CurrentLocation.X - Globals.DataRange; x <= CurrentLocation.X + Globals.DataRange; x++)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;
                        if (x < 0 || x >= CurrentMap.Width) continue;

                        Cell cell = CurrentMap.GetCell(x, y);

                        if (!cell.Valid || cell.Objects == null) continue;

                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject ob = cell.Objects[i];

                            if (ob is PlayerObject obj)
                                obj.Enqueue(GetInfo());

                        }
                    }
                }
            }

            if (Envir.Time > DuraTime)
            {
                DuraTime = Envir.Time + DuraDelay;

                for (int i = 0; i < Equipment.Length; i++)
                {
                    item = Equipment[i];
                    if (item == null || !item.DuraChanged) continue; // || item.Info.Type == ItemType.Mount
                    item.DuraChanged = false;
                    PlayerMaster.Enqueue(new S.DuraChanged { UniqueID = item.UniqueID, CurrentDura = item.CurrentDura });
                }
            }

            FindHarvest();
            ProcessHarvest();
            ProcessSearch();
            ProcessRoam();
            ProcessCombo();
            ProcessFriendTarget();
            ProcessTarget();
            ProcessAutoPot();
            ProcessBuffs();
            RefreshNameColour();

            MonsterAttacked = false;
        }

        public void ProcessHarvest()
        {
            if (HarvestTime < SMain.Envir.Time)
                HarvestTarget = null;

            if (HarvestTarget == null) return;

            if (Functions.MaxDistance(CurrentLocation,HarvestTarget.CurrentLocation) <= 1)
            {
                Broadcast(new S.ObjectHarvest { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                ActionTime = Envir.Time + 300;
                AttackTime = Envir.Time + 500;

                foreach (var d in ((HarvestMonster)HarvestTarget).Info.Drops)
                {
                    if (!d.QuestRequired) continue;

                    UserItem item = Envir.CreateDropItem(d.Item);
                    if (item == null) continue;

                    PlayerMaster.CheckHeroNeedQuestItem(item, true);

                    var harvestMob = ((HarvestMonster)HarvestTarget);
                    harvestMob.Harvested = true;
                    harvestMob._drops = null;
                    harvestMob.Broadcast(new S.ObjectHarvested { ObjectID = harvestMob.ObjectID, Direction = harvestMob.Direction, Location = harvestMob.CurrentLocation });
                }
                HarvestTarget = null;
            }
            else
            {
                MoveTo(HarvestTarget.CurrentLocation);
            }
        }

        public void FindHarvest()
        {
            if (HarvestTarget != null) return;
            if (HarvestDelay > SMain.Envir.Time) return;

            HarvestDelay = SMain.Envir.Time + 500;

            List<MapObject> targets = FindAllNearby(10, CurrentLocation, false);

            foreach(var t in targets)
            {
                if (t is HarvestMonster && !t.Harvested )
                {
                    var harvest = (HarvestMonster)t;
                    if (!harvest.Dead || harvest.Harvested) continue;
                    if (harvest.Info == null || harvest.Info.Drops == null) continue;

                    foreach(var d in harvest.Info.Drops)
                    {
                        if (!d.QuestRequired) continue;

                        UserItem item = Envir.CreateDropItem(d.Item);
                        if (item == null) continue;

                        if (PlayerMaster.CheckHeroNeedQuestItem(item,false))
                        {
                            HarvestTarget = t;
                            HarvestTime = SMain.Envir.Time + 10 * Settings.Second;
                        }

                    }
                }
            }
        }

        private int CountItem(UserItem[] grid, int idx)
        {
            var counter = 0;
            foreach(var g in grid)
            {
                if (g == null) continue;

                if (g.Info.Index == idx)
                    counter++;
            }

            return counter;
        }

        #region ProcessAutoPot System
        private void ProcessAutoPot()
        {
            if (!autoPotSystem.isEnabled) return;

            foreach(var pot in autoPotSystem.AutoPotList)
            {
                if (pot.PercentTrigger < 0 || pot.PercentTrigger > 100) continue;
                if (pot.Timer > Envir.Time) continue;

                pot.Timer = Envir.Time + pot.Delay;

                if (pot.AutoPotType == 1 && HP < ((MaxHP * pot.PercentTrigger) / 100))
                {
                    bool hasEnough = true;
                    string fName = string.Empty;
                    foreach (var b in Inventory)
                    {
                        if (b != null && b.Info.Index == pot.ItemIndex)
                        {
                            if (!CanUseItem(b)) continue;

                            if (CountItem(Inventory,pot.ItemIndex) == 1)
                            {
                                hasEnough = false;
                                fName = b.FriendlyName;
                            }

                            UseItem(b.UniqueID);
                            break;
                        }
                    }
                    if (!hasEnough)
                        PlayerMaster.ReceiveChat("Hero has run out of : " + fName, ChatType.System);  //This spams your chat when using pots , should warm when 1 pot left

                }

                if (pot.AutoPotType == 2 && MP < ((MaxMP * pot.PercentTrigger) / 100))
                {
                    bool hasEnough = true;
                    string fName = string.Empty;
                    foreach (var b in Inventory)
                    {
                        if (b != null && b.Info.Index == pot.ItemIndex)
                        {
                            if (!CanUseItem(b)) continue;

                            if (CountItem(Inventory, pot.ItemIndex) == 1)
                            {
                                hasEnough = false;
                                fName = b.FriendlyName;
                            }

                            UseItem(b.UniqueID);
                            break;
                        }
                    }
                    if (!hasEnough)
                        PlayerMaster.ReceiveChat("Hero has run out of : " + fName, ChatType.System);  //This spams your chat when using pots , should warm when 1 pot left

                }
            }
        }
        #endregion

        #region ProcessCombo Skills
        private void ProcessCombo()
        {
            if (Master == null) return;
            if (comboSpell == Spell.None) return;


            Direction = Functions.DirectionFromPoint(CurrentLocation, comboLocation);

            Point loc;
            if (comboSpell.In(Spell.ChopChopStar,Spell.SoulEaterSwamp, Spell.SoulEaterSwamp) && comboTarget != null)
            {
                loc = comboTarget.CurrentLocation;
            }
            else
                loc = comboLocation;

            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.None, TargetID = 0, Target = loc, Cast = true, Level = 0 });


            if (comboSpell == Spell.DragonFlames && comboLocation != Point.Empty)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 5000 });
                Broadcast(new S.ComboStance { Time = 5000 , ObjectID = ObjectID , spell = Spell.DragonFlames});
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 5000, Spell.DragonFlames));

                ActionTime = Envir.Time + 5000;
                AttackTime = Envir.Time + 5000;
            }

            if (comboSpell == Spell.BrokenSoulCut && comboLocation != Point.Empty)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 2000 });
                Broadcast(new S.ComboStance { Time = 2000, ObjectID = ObjectID, spell = Spell.BrokenSoulCut });
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 2000, Spell.BrokenSoulCut));

                ActionTime = Envir.Time + 2000;
                AttackTime = Envir.Time + 2000;
            }

            if (comboSpell == Spell.SoulReaper && comboLocation != Point.Empty)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 5000 });
                Broadcast(new S.ComboStance { Time = 5000, ObjectID = ObjectID, spell = Spell.SoulReaper });
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 5000, Spell.SoulReaper));

                ActionTime = Envir.Time + 5000;
                AttackTime = Envir.Time + 5000;
            }

            if (comboSpell == Spell.LastJudgement && comboLocation != Point.Empty)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 5000 });
                Broadcast(new S.ComboStance { Time = 5000, ObjectID = ObjectID, spell = Spell.LastJudgement });
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 5000, Spell.LastJudgement));

                ActionTime = Envir.Time + 5000;
                AttackTime = Envir.Time + 5000;
            }

            if (comboSpell == Spell.ThunderClap && comboLocation != Point.Empty)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 5000 });
                Broadcast(new S.ComboStance { Time = 5000, ObjectID = ObjectID, spell = Spell.ThunderClap });
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 5000, Spell.ThunderClap));

                ActionTime = Envir.Time + 5000;
                AttackTime = Envir.Time + 5000;
            }

            if (comboSpell == Spell.ChopChopStar && comboTarget != null)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 5000 });
                Broadcast(new S.ComboStance { Time = 5000, ObjectID = ObjectID, spell = Spell.ChopChopStar });
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 5000, Spell.ChopChopStar));

                ActionTime = Envir.Time + 5000;
                AttackTime = Envir.Time + 5000;
            }

            if (comboSpell == Spell.SoulEaterSwamp && comboTarget != null)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 5000 });
                Broadcast(new S.ComboStance { Time = 5000, ObjectID = ObjectID, spell = Spell.SoulEaterSwamp });
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 5000, Spell.SoulEaterSwamp));

                ActionTime = Envir.Time + 5000;
                AttackTime = Envir.Time + 5000;
            }

            if (comboSpell == Spell.HandOfGod && comboTarget != null)
            {
                PlayerMaster.Enqueue(new S.ComboHero { Time = 5000 });
                Broadcast(new S.ComboStance { Time = 5000, ObjectID = ObjectID, spell = Spell.HandOfGod });
                ActionList.Add(new DelayedAction(DelayedType.Magic, Envir.Time + 5000, Spell.HandOfGod));

                ActionTime = Envir.Time + 5000;
                AttackTime = Envir.Time + 5000;
            }

            comboSpell = Spell.None;
        }
        #endregion

        protected void ProcessFriendTarget()
        {
            if (Master == null) return;

            switch (HeroClass)
            {
                case MirClass.Taoist:
                        TaoHelp();
                    break;
                case MirClass.Warrior:
                    ProcessWarriorBuff();
                    break;
            }
        }

        public void ProcessWarriorBuff()
        {
            UserMagic magic = null;

            if (Dead || Envir.Time < AttackTime || Envir.Time < ActionTime) return;
            #region Warrior Rage
            
            magic = Magics.FirstOrDefault(x => x.Spell == Spell.Rage && x.Key > 0 && MP > x.ManaCost);

            if (magic != null && MonsterAttacked && (Envir.Random.Next((1 + magic.Level) * 2) == 1) && (!Buffs.Any(x => x.Type == BuffType.Rage)))
            {
                MapObject target = this;

                int value = MaxDC >= 5 ? Math.Min(8, MaxDC / 5) : 1;
                if (target != null)
                {

                    int duration = 0;
                    switch (magic.Level)
                    {
                        case 0:
                            duration = 60;
                            break;
                        case 1:
                            duration = 120;
                            break;
                        case 2:
                            duration = 180;
                            break;
                        case 3:
                            duration = 240;
                            break;
                        case 4:
                            duration = 300;
                            break;
                    }
                    int val = (int)Math.Round(MaxDC * (0.12 + (0.03 * magic.Level)));

                    AddBuff(new Buff { Type = BuffType.Rage, Caster = this, ExpireTime = Envir.Time + duration * 1000, Values = new int[] { val } });
                    Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                    LevelMagic(magic);

                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;

                    ChangeMP(-magic.ManaCost);

                    return;
                }


            }
            #endregion
            #region Warrior Pro Field
            magic = Magics.FirstOrDefault(x => x.Spell == Spell.ProtectionField && x.Key > 0 && MP > x.ManaCost);

            if (magic != null && MonsterAttacked && (Envir.Random.Next((1 + magic.Level) * 2) == 1) && (!Buffs.Any(x => x.Type == BuffType.ProtectionField)))
            {
                MapObject target = this;

                int value = MaxDC >= 5 ? Math.Min(8, MaxDC / 5) : 1;
                if (target != null)
                {                   
                    int duration = 0;
                    switch (magic.Level)
                    {
                        case 0:
                            duration = 60;
                            break;
                        case 1:
                            duration = 120;
                            break;
                        case 2:
                            duration = 180;
                            break;
                        case 3:
                            duration = 240;
                            break;
                        case 4:
                            duration = 300;
                            break;
                    }
                    int val = (int)Math.Round(MaxAC * (0.2 + (0.03 * magic.Level)));

                    AddBuff(new Buff { Type = BuffType.ProtectionField, Caster = this, ExpireTime = Envir.Time + duration * 1000, Values = new int[] { val } });
                    Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                    LevelMagic(magic);

                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;

                    ChangeMP(-magic.ManaCost);

                    return;
                }


            }
            #endregion
            #region Hate
            magic = Magics.FirstOrDefault(x => x.Spell == Spell.Haste && x.Key > 0 && MP > x.ManaCost);

            if (magic != null && Envir.Random.Next(5 - magic.Level) == 1 && !Buffs.Any(x => x.Type == BuffType.Haste))
            {
                AddBuff(new Buff { Type = BuffType.Haste, Caster = PlayerMaster, ExpireTime = Envir.Time + ((magic.Level + 3) * 10) * 1000, ObjectID = ObjectID, Values = new int[] { (magic.Level + 1) * 2 } });
                LevelMagic(magic);

                ChangeMP(-magic.ManaCost);
                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = ObjectID, Target = CurrentLocation, Cast = true, Level = magic.Level });

                return;
            }
            #endregion
        }

        public void ResizeInventory()
        {
            if (Inventory.Length >= 42) return;
            Array.Resize(ref Inventory, Inventory.Length + 8);

            PlayerMaster.Enqueue(new S.ResizeHeroInventory { Size = Inventory.Length });
        }
        #region Hero F Key usage Pete107
        public void UseMagic(Spell spell, MirDirection dir, Point location, MapObject target = null)
        {
            if (Envir.Time < AttackTime)
                return;
            bool cast = false;
            UserMagic magic = null;
            bool hasSkill = false;
            for (int i = 0; i < Magics.Count; i++)
            {
                if (Magics[i].Spell == spell && ! hasSkill)
                {
                    magic = Magics[i];
                    hasSkill = true;
                }
            }
            long delay = magic.GetDelay();

            if (magic != null && Envir.Time < (magic.CastTime + delay) && magic.CastTime > 0)
            {
                HeroNextSpell = Spell.None;
                return;
            }
            
            bool inRange = true;
            for (int i = 0; i < Envir.MagicInfoList.Count; i++)
                if (Envir.MagicInfoList[i].Spell == magic.Spell)
                    if (!InAttackRange(Envir.MagicInfoList[i].Range))
                        inRange = false;
            if (!inRange)
            {
                MoveTo(target != null ? target.CurrentLocation : Target != null ? Target.CurrentLocation : PlayerMaster.CurrentLocation);
                return;
            }
            
            if (MP < magic.ManaCost)
                return;

            int damage = 0;

            switch(spell)
            {
                #region Attack Targets
                case Spell.SoulFireBall:
                    {
                        if (target != null)
                        {
                            if (target.IsAttackTarget(this))
                            {
                                damage = GetAttackPower(MinSC, MaxSC);
                                if (damage == 0)
                                    return;
                                PerformSoulFireBall(magic, target, damage);
                                cast = true;
                            }                            
                        }
                    }
                    break;
                case Spell.Poisoning:
                    {
                        if (target != null)
                        {
                            if (target.IsAttackTarget(this))
                            {
                                PerformPoisoning(magic, target);
                                cast = true;
                            }
                        }
                    }
                    break;
                case Spell.FireBall:
                case Spell.FrostCrunch:
                    {
                        if (target != null)
                        {
                            if (target.IsAttackTarget(this))
                            {
                                damage = GetAttackPower(MinMC, MaxMC);
                                if (damage == 0) return;
                                if (spell == Spell.FrostCrunch)
                                    PerformFrostCrunch(magic, target, damage);
                                else if (spell == Spell.FireBall)
                                    PerformFireBall(magic, target, damage);
                                cast = true;
                            }
                        }
                    }
                    break;
                case Spell.Vampirism:
                case Spell.ThunderBolt:
                case Spell.TurnUndead:
                case Spell.FlameDisruptor:                
                    {
                        if (target != null)
                        {
                            if (target.IsAttackTarget(this))
                            {
                                damage = GetAttackPower(MinMC, MaxMC);
                                if (damage == 0) return;
                                if (spell == Spell.Vampirism)
                                    PerformVampirism(magic, target, damage);
                                if (spell == Spell.TurnUndead)
                                    PerformTurnUndead(magic, target, damage);
                                if (spell == Spell.ThunderBolt)
                                    PerformThunderBolt(magic, target, damage);
                                cast = true;
                            }
                        }
                    }
                    break;
                case Spell.Revelation:
                    break;
                #endregion
                #region Friendly Targets
                case Spell.Healing:
                    {
                        if (target != null)
                        {
                            if (target.Race == ObjectType.Player ||
                                target.Race == ObjectType.Hero ||
                                (target.Race == ObjectType.Monster && target.Master != null && target.Master.Race == ObjectType.Player))
                                if (target.IsFriendlyTarget(this))
                                {
                                    PerformHealing(magic, target);
                                    cast = true;
                                }
                        }
                    }
                    break;                
                case Spell.UltimateEnhancer:
                    {
                        if (target != null)
                        {
                            if (target.Race == ObjectType.Player ||
                                target.Race == ObjectType.Hero ||
                                (target.Race == ObjectType.Monster && target.Master != null && target.Master.Race == ObjectType.Player))
                                if (target.IsFriendlyTarget(this))
                                {
                                    PerformUltimateEnhancer(magic, target, damage);
                                    cast = true;
                                }
                        }
                    }
                    break;
                case Spell.Purification:
                    {
                        if (target != null)
                        {
                            if (target.Race == ObjectType.Player ||
                                target.Race == ObjectType.Hero ||
                                (target.Race == ObjectType.Monster && target.Master != null && target.Master.Race == ObjectType.Player))
                                if (target.IsFriendlyTarget(this))
                                {
                                    PerformPurification(magic, target);
                                    cast = true;
                                }
                        }
                    }
                    break;
                #endregion
                #region Location Targets
                case Spell.SoulShield:
                case Spell.BlessedArmour:
                    {
                        damage = GetAttackPower(MinSC, MaxSC);
                        if (damage == 0) return;
                        PerformFourByFourLocationBuff(magic, location, damage);
                        cast = true;
                    }
                    break;
                case Spell.MassHealing:
                    {
                        damage = GetAttackPower(MinSC, MaxSC);
                        if (damage == 0) return;
                        PerformMassHealing(magic, location, damage);
                        cast = true;
                    }
                    break;
                case Spell.IceStorm:
                case Spell.FireBang:
                    {
                        damage = GetAttackPower(MinMC, MaxMC);
                        if (damage == 0) return;
                        PerformThreeByThreeLocation(magic, location, damage);
                        cast = true;
                    }
                    break;
                case Spell.FireWall:
                    {
                        damage = GetAttackPower(MinMC, MaxMC);
                        if (damage == 0) return;
                        PerformFireWall(magic, location, damage);
                        cast = true;
                    }
                    break;
                case Spell.Lightning:
                case Spell.HellFire:
                    {
                        damage = GetAttackPower(MinMC, MaxMC);
                        if (damage == 0) return;
                        PerformLineAttack(magic, dir, location, damage);
                        cast = true;
                    }
                    break;
                #endregion
                #region Own Location Targets
                case Spell.ThunderStorm:
                case Spell.FlameField:
                case Spell.MagicShield:
                case Spell.Haste:
                case Spell.LightBody:
                    {
                        if (spell == Spell.MagicShield)
                            PerformMagicShield(magic);
                        if (spell == Spell.FlameField ||
                            spell == Spell.ThunderStorm)
                        {
                            damage = GetAttackPower(MinMC, MaxMC);
                            if (damage == 0) return;
                            PerformFourByFour(magic, location, damage);
                        }
                        if (spell == Spell.Haste)
                            PerformHaste(magic);
                        if (spell == Spell.LightBody)
                            PerformLightBody(magic);
                        cast = true;
                    }
                    break;
                #endregion

            }
            if (cast)
            {
                magic.CastTime = Envir.Time;
                HeroNextSpell = Spell.None;
            }
            
            ChangeMP(-magic.ManaCost);
            LevelMagic(magic);
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
        }
        #region Wiz
        public void PerformFireBall(UserMagic magic, MapObject target, int damage)
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, target, magic.GetDamage(damage), DefenceType.MAC);
            ActionList.Add(action);
        }

        public void PerformMagicShield(UserMagic magic)
        {
            #region Magic Shield
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = Target.ObjectID*/, Target = CurrentLocation, Cast = true, Level = magic.Level });
            MagicShieldTime = Envir.Time + Settings.Second * (15 + magic.Level * 5);
            AddBuff(new Buff { Type = BuffType.MagicShield, Caster = this, ObjectID = ObjectID, ExpireTime = MagicShieldTime, Values = new int[] { 15 } });
            MagicShieldUp = true;
            Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.MagicShieldUp });
            return;
            #endregion
        }

        public void PerformFourByFour(UserMagic magic, Point location, int damage)
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, location);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = Target.ObjectID*/, Target = location, Cast = true, Level = magic.Level });


            for (int y = location.Y - 2; y <= location.Y + 2; y++)
            {
                if (y < 0) continue;
                if (y >= CurrentMap.Height) break;

                for (int x = location.X - 2; x <= location.X + 2; x++)
                {
                    if (x < 0) continue;
                    if (x >= CurrentMap.Width) break;

                    var cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid || cell.Objects == null) continue;

                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject target = cell.Objects[i];
                        switch (target.Race)
                        {
                            case ObjectType.Monster:
                            case ObjectType.Player:
                            case ObjectType.Hero:
                                //Only targets
                                if (!target.IsAttackTarget(this)) break;

                                if (target.Attacked(this, magic.Spell == Spell.ThunderStorm && !target.Undead ? magic.GetDamage(damage) / 10 : magic.GetDamage(damage), DefenceType.MAC) <= 0)
                                {
                                    if (target.Undead)
                                    {
                                        target.ApplyPoison(new Poison { PType = PoisonType.Stun, Duration = magic.Level + 2, TickSpeed = 1000 }, this);
                                    }
                                    break;
                                }
                                break;
                        }
                    }

                }
            }
        }

        public void PerformVampirism(UserMagic magic, MapObject target, int damage)
        {
            var VampAmount = (ushort)(damage * (magic.Level + 1) * 0.25F);
            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, target, magic.GetDamage(damage), DefenceType.MAC);
            ActionList.Add(action);
        }

        public void PerformFrostCrunch(UserMagic magic, MapObject target, int damage)
        {

            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, target, magic.GetDamage(damage), DefenceType.MAC);
            ActionList.Add(action);

            if (HeroLevel + (target.Race == ObjectType.Player ? 2 : 10) >= target.Level && Envir.Random.Next(target.Race == ObjectType.Player ? 100 : 20) <= magic.Level)
            {
                target.ApplyPoison(new Poison
                {
                    Owner = this,
                    Duration = target.Race == ObjectType.Player ? 4 : 5 + Envir.Random.Next(5),
                    PType = PoisonType.Slow,
                    TickSpeed = 1000,
                }, this);
                target.OperateTime = 0;
            }

            if (HeroLevel + (target.Race == ObjectType.Player ? 2 : 10) >= target.Level && Envir.Random.Next(target.Race == ObjectType.Player ? 100 : 40) <= magic.Level)
            {
                target.ApplyPoison(new Poison
                {
                    Owner = this,
                    Duration = target.Race == ObjectType.Player ? 2 : 5 + Envir.Random.Next(Freezing),
                    PType = PoisonType.Frozen,
                    TickSpeed = 1000,
                }, this);
                target.OperateTime = 0;
            }
            
        }

        public void PerformThreeByThreeLocation(UserMagic magic, Point location, int damage)
        {
            var value = damage;

            Direction = Functions.DirectionFromPoint(CurrentLocation, location);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = Target.ObjectID*/, Target = location, Cast = true, Level = magic.Level });

            for (int y = location.Y - 1; y <= location.Y + 1; y++)
            {
                if (y < 0) continue;
                if (y >= CurrentMap.Height) break;

                for (int x = location.X - 1; x <= location.X + 1; x++)
                {
                    if (x < 0) continue;
                    if (x >= CurrentMap.Width) break;

                    var cell = CurrentMap.GetCell(x, y);

                    if (!cell.Valid || cell.Objects == null) continue;

                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject target = cell.Objects[i];
                        switch (target.Race)
                        {
                            case ObjectType.Monster:
                            case ObjectType.Player:
                            case ObjectType.Hero:
                                //Only targets
                                if (target.IsAttackTarget(this))
                                {
                                    target.Attacked(this, magic.GetDamage(value), DefenceType.MAC);
                                }
                                break;
                        }
                    }

                }
            }
        }

        public void PerformFireWall(UserMagic magic, Point location, int damage)
        {
            int value = damage;
            Direction = Functions.DirectionFromPoint(CurrentLocation, location);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = Target.ObjectID*/, Target = location, Cast = true, Level = magic.Level });


            if (CurrentMap.ValidPoint(location))
            {
                var cell = CurrentMap.GetCell(location);

                bool cast = true;
                if (cell.Objects != null)
                    for (int o = 0; o < cell.Objects.Count; o++)
                    {
                        MapObject target = cell.Objects[o];
                        if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.FireWall) continue;

                        cast = false;
                        break;
                    }

                if (cast)
                {
                    SpellObject ob = new SpellObject
                    {
                        Spell = Spell.FireWall,
                        Value = magic.GetDamage(value),
                        ExpireTime = Envir.Time + (10 + value / 2) * 1000,
                        TickSpeed = 2000,
                        Caster = PlayerMaster,
                        CurrentLocation = location,
                        CurrentMap = CurrentMap,
                    };
                    CurrentMap.AddObject(ob);
                    ob.Spawned();
                }
            }

            var dir = MirDirection.Up;
            for (int i = 0; i < 4; i++)
            {
                location = Functions.PointMove(location, dir, 1);
                dir += 2;

                if (!CurrentMap.ValidPoint(location)) continue;

                var cell = CurrentMap.GetCell(location);
                bool cast = true;

                if (cell.Objects != null)
                    for (int o = 0; o < cell.Objects.Count; o++)
                    {
                        MapObject target = cell.Objects[o];
                        if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.FireWall) continue;

                        cast = false;
                        break;
                    }

                if (!cast) continue;

                SpellObject ob = new SpellObject
                {
                    Spell = Spell.FireWall,
                    Value = magic.GetDamage(value),
                    ExpireTime = Envir.Time + (10 + value / 2) * 1000,
                    TickSpeed = 2000,
                    Caster = PlayerMaster,
                    CurrentLocation = location,
                    CurrentMap = CurrentMap,
                };
                CurrentMap.AddObject(ob);
                ob.Spawned();
            }
        }

        public void PerformLineAttack(UserMagic magic, MirDirection dir, Point location, int damage)
        {
            var dist = (magic.Spell == Spell.HellFire ? 4 : 7);

            if (!Functions.InRange(CurrentLocation, location, dist)) return;
            var newLocation = CurrentLocation;
            var tempDirection = dir;
            for (int i = 0; i < dist; i++)
            {
                newLocation = Functions.PointMove(newLocation, tempDirection, 1);

                if (!CurrentMap.ValidPoint(newLocation)) continue;

                var cell = CurrentMap.GetCell(newLocation);

                if (cell.Objects == null) continue;

                for (int o = 0; o < cell.Objects.Count; o++)
                {
                    MapObject target = cell.Objects[o];
                    if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;

                    if (!target.IsAttackTarget(this)) continue;
                    if (target.Attacked(this, magic.GetDamage(damage), DefenceType.MAC) > 0)
                    break;
                }
            }
            Direction = dir;
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = Target.ObjectID*/, Target = location, Cast = true, Level = magic.Level });

        }

        public void PerformTurnUndead(UserMagic magic, MapObject target, int damage)
        {
            if (target.Race != ObjectType.Monster || !target.Undead)
            {
                return;
            }

            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });


            if (Envir.Random.Next(2) + HeroLevel - 1 <= target.Level)
            {
                target.Target = this;
                return;
            }

            int dif = HeroLevel - target.Level + 3;  //Tu Ice

            if (Envir.Random.Next(100) >= (target.Level + 1 << 3) + dif)
            {
                target.Target = this;
                return;
            }

            target.LastHitter = this.Master;
            target.LastHitTime = Envir.Time + 5000;
            target.EXPOwner = this.Master;
            target.EXPOwnerTime = Envir.Time + 5000;
            target.Die();
        }
        #endregion
        #region Tao
        public void PerformMassHealing(UserMagic magic, Point location, int damage)
        {
            if (Functions.MaxDistance(CurrentLocation, location) > 14)
                return;
            if (location != Point.Empty)
            {

                Direction = Functions.DirectionFromPoint(CurrentLocation, location);
                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = target.ObjectID*/, Target = location, Cast = true, Level = magic.Level });

                for (int y = location.Y - 1; y <= location.Y + 1; y++)
                {
                    if (y < 0) continue;
                    if (y >= CurrentMap.Height) break;

                    for (int x = location.X - 1; x <= location.X + 1; x++)
                    {
                        if (x < 0) continue;
                        if (x >= CurrentMap.Width) break;

                        var cell = CurrentMap.GetCell(x, y);

                        if (!cell.Valid || cell.Objects == null) continue;

                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            MapObject t = cell.Objects[i];
                            switch (t.Race)
                            {
                                case ObjectType.Monster:
                                case ObjectType.Player:
                                case ObjectType.Hero:
                                    //Only targets
                                    if (t.IsFriendlyTarget(PlayerMaster))
                                    {
                                        if (t.Health >= t.MaxHealth) continue;
                                        t.HealAmount = (ushort)Math.Min(ushort.MaxValue, t.HealAmount + magic.GetDamage(GetAttackPower(MinSC, MaxSC)));
                                        t.OperateTime = 0;
                                    }
                                    break;
                            }
                        }

                    }

                }
            }           
        }

        public void PerformFourByFourLocationBuff(UserMagic magic, Point location, int damage)
        {
            switch(magic.Spell)
            {
                case Spell.SoulShield:
                    {
                        var item = GetAmulet(3);

                        if (item != null && (Functions.MaxDistance(CurrentLocation, location) <= 14))
                        {
                            List<MapObject> targets = FindAllNearby(4, location, false);
                            for (int i = 0; i < targets.Count; i++)
                                if (targets[i].Race.In(ObjectType.Hero, ObjectType.Player) && targets[i].IsFriendlyTarget(PlayerMaster))
                                    targets[i].AddBuff(new Buff { Type = BuffType.SoulShield, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = targets[i].ObjectID, Values = new int[] { targets[i].Level / 7 + 4 } });
                            Direction = Functions.DirectionFromPoint(CurrentLocation, location);
                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = trg.ObjectID*/, Target = location, Cast = true, Level = magic.Level });
                            ConsumeItem(item, 3);
                            return;
                        }
                    }
                    break;
                case Spell.BlessedArmour:
                    {
                        ArmourShield = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000;
                        var item = GetAmulet(3);
                        if (item != null && (Functions.MaxDistance(CurrentLocation, location) <= 14))
                        {
                            List<MapObject> targets = FindAllNearby(4, location, false);
                            for (int i = 0; i < targets.Count; i++)
                                if (targets[i].Race.In(ObjectType.Hero, ObjectType.Player) && targets[i].IsFriendlyTarget(PlayerMaster))
                                    targets[i].AddBuff(new Buff { Type = BuffType.BlessedArmour, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = targets[i].ObjectID, Values = new int[] { targets[i].Level / 7 + 4 } });
                            Direction = Functions.DirectionFromPoint(CurrentLocation, location);
                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell/*, TargetID = trg.ObjectID*/, Target = location, Cast = true, Level = magic.Level });
                            ConsumeItem(item, 3);
                        }
                        return;
                    }
            }
        }
        
        public void PerformUltimateEnhancer(UserMagic magic, MapObject target, int damage)
        {
            int value = MaxSC >= 5 ? Math.Min(8, MaxSC / 5) : 1;
            if (target != null)
            {
                target.AddBuff(new Buff { Type = BuffType.UltimateEnhancer, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = target.ObjectID, Values = new int[] { value } });
                Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                Broadcast(new S.ObjectEffect { Effect = SpellEffect.UEnhance, ObjectID = target.ObjectID });
                return;
            }
        }

        public void PerformPurification(UserMagic magic, MapObject target)
        {
            
            if (target.PoisonList.Count > 0)
            {
                if (target != null)
                {
                    if (Envir.Random.Next(5 - magic.Level) == 1)
                    {
                        target.PoisonList.Clear();
                        target.OperateTime = 0;

                    }
                    Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                    return;
                }

            }
        }

        public void PerformHealing(UserMagic magic, MapObject target)
        {
            int health = magic.GetDamage(GetAttackPower(MinSC, MaxSC) * 2) + Level;
            if (!target.Dead && InHelpRange(target) && target.Node != null)
            {
                if (target.Health < target.MaxHealth)
                {

                    Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });


                    target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + health);
                    target.OperateTime = 0;
                    LevelMagic(magic);
                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;
                    ChangeMP(-magic.ManaCost);
                    return;
                }
            }
        }

        public void PerformPoisoning(UserMagic magic, MapObject target)
        {
            var item = GetPoison(1);

            if (item != null)
            {
                if ((item.Info.Shape == 1 && !target.PoisonList.Any(x => x.PType == PoisonType.Green)) || (item.Info.Shape == 2 && !target.PoisonList.Any(x => x.PType == PoisonType.Red)))
                {
                    Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

                    int power = magic.GetDamage(GetAttackPower(MinSC, MaxSC));
                    switch (item.Info.Shape)
                    {
                        case 1:
                            target.ApplyPoison(new Poison
                            {
                                Duration = (power * 2) + ((magic.Level + 1) * 7),
                                Owner = PlayerMaster,
                                PType = PoisonType.Green,
                                TickSpeed = 2000,
                                Value = power / 15 + magic.Level + 1 + Envir.Random.Next(PoisonAttack)
                            }, this);
                            break;
                        case 2:
                            target.ApplyPoison(new Poison
                            {
                                Duration = (power * 2) + (magic.Level + 1) * 7,
                                Owner = PlayerMaster,
                                PType = PoisonType.Red,
                                TickSpeed = 2000,
                            }, this);
                            break;
                    }
                    target.OperateTime = 0;
                    ConsumeItem(item, 1);
                    return;
                }
            }
        }

        public void PerformSoulFireBall(UserMagic magic, MapObject target, int damage)
        {
            var item = GetAmulet(1);

            if (item != null)
            {
                Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

                DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, target, magic.GetDamage(damage), DefenceType.MAC);
                ActionList.Add(action);
                return;
            }
        }

        public void PerformRevelation(UserMagic magic, MapObject target)
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

            target.RevTime = Envir.Time + ((1 + magic.Level) * 10) * 1000;
            target.OperateTime = 0;
            target.BroadcastHealthChange();
        }
        #endregion
        #region Warrior
        public void PerformTwinDrakeBlade(UserMagic magic, int damage)
        {
            #region Twin Drake Blade
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.TwinDrakeBlade });
            Target.Attacked(this, (int)(damage));
            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.AC);
            ActionList.Add(action);
            ChangeMP(-magic.ManaCost);
            LevelMagic(magic);
            return;
            #endregion
        }

        public void PerformThunderBolt(UserMagic magic, MapObject target, int damage)
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
            DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, target, magic.GetDamage(damage), DefenceType.MAC);
            ActionList.Add(action);
            return;
        }

        public void PerformFlamingSword(UserMagic magic, int damage)
        {

            if (magic != null && Envir.Random.Next(26 - (magic.Level * 2)) == 1 && MP > magic.ManaCost)
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.FlamingSword });
                switch (magic.Level)
                {
                    case 0:
                        Target.Attacked(this, (int)(damage * 1.4));
                        break;
                    case 1:
                        Target.Attacked(this, (int)(damage * 1.8));
                        break;
                    case 2:
                        Target.Attacked(this, (int)(damage * 2.2));
                        break;
                    case 3:
                        Target.Attacked(this, (int)(damage * 2.6));
                        break;
                }
                ChangeMP(-magic.ManaCost);
                LevelMagic(magic);
                return;
            }
        }
        #endregion
        #region Sin
        public void PerformHaste(UserMagic magic)
        {
            AddBuff(new Buff { Type = BuffType.Haste, Caster = PlayerMaster, ExpireTime = Envir.Time + ((magic.Level + 3) * 10) * 1000, ObjectID = ObjectID, Values = new int[] { (magic.Level + 1) * 2 } });
            LevelMagic(magic);

            ChangeMP(-magic.ManaCost);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = ObjectID, Target = CurrentLocation, Cast = true, Level = magic.Level });

            return;
        }

        public void PerformLightBody(UserMagic magic)
        {
            AddBuff(new Buff { Type = BuffType.LightBody, Caster = PlayerMaster, ExpireTime = Envir.Time + ((magic.Level + 3) * 10) * 1000, ObjectID = ObjectID, Values = new int[] { (magic.Level + 1) } });
            LevelMagic(magic);

            ChangeMP(-magic.ManaCost);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = ObjectID, Target = CurrentLocation, Cast = true, Level = magic.Level });

            return;
        }

        public void PerformHeavenlySword(UserMagic magic, int damage)
        {
            var train = false;
            var location = CurrentLocation;
            for (int i = 0; i < 3; i++)
            {
                location = Functions.PointMove(location, Direction, 1);

                if (!CurrentMap.ValidPoint(location)) continue;

                var cell = CurrentMap.GetCell(location);

                if (cell.Objects == null) continue;

                for (int o = 0; o < cell.Objects.Count; o++)
                {
                    MapObject target = cell.Objects[o];
                    if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;

                    if (!target.IsAttackTarget(this)) continue;
                    if (target.Attacked(this, damage, DefenceType.MAC) > 0)
                        train = true;
                    break;
                }
            }
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });


            ChangeMP(-magic.ManaCost);
            if (train)
                LevelMagic(magic);

            return;
        }

        public void PerformPoisonSword(UserMagic magic, int damage)
        {
            Point hitPoint;
            Cell cell;
            MirDirection dir = Functions.PreviousDir(Direction);
            int power = magic.GetDamage(GetAttackPower(MinDC, MaxDC));

            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.PoisonSword });

            for (int i = 0; i < 5; i++)
            {
                hitPoint = Functions.PointMove(CurrentLocation, dir, 1);
                dir = Functions.NextDir(dir);

                if (!CurrentMap.ValidPoint(hitPoint)) continue;
                cell = CurrentMap.GetCell(hitPoint);

                if (cell.Objects == null) continue;

                for (int o = 0; o < cell.Objects.Count; o++)
                {
                    MapObject target = cell.Objects[o];
                    if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;
                    if (target == null || !target.IsAttackTarget(this) || target.Node == null) continue;

                    target.ApplyPoison(new Poison
                    {
                        Duration = 3 + power / 10 + magic.Level * 3,
                        Owner = this,
                        PType = PoisonType.Green,
                        TickSpeed = 1000,
                        Value = power / 10 + magic.Level + 1 + Envir.Random.Next(PoisonAttack)
                    }, this);

                    target.OperateTime = 0;
                    break;
                }
            }

            ChangeMP(-magic.ManaCost);
            LevelMagic(magic);
            return;
        }
        #endregion
        #endregion
        private void TaoHelp()
        {
            UserMagic magic = null;

            if (Dead || Envir.Time < AttackTime || Envir.Time < ActionTime) return;

            
            if (HeroNextSpell != Spell.None)
            {
                if (HeroNextTarget != null &&
                    (//Only valid targets
                    (HeroNextTarget.Race == ObjectType.Player) ||
                    (HeroNextTarget.Race == ObjectType.Hero) ||
                    (HeroNextTarget.Race == ObjectType.Monster)
                    )
                   )
                {
                    magic = Magics.FirstOrDefault(x => x.Spell == HeroNextSpell && x.Key > 0 && MP > x.ManaCost);
                    if (magic != null)
                    {
                        switch (HeroNextSpell)
                        {
                            case Spell.Hiding:
                                {
                                    MapObject target = this;

                                    int value = MaxSC >= 5 ? Math.Min(8, MaxSC / 5) : 1;
                                    if (target != null)
                                    {
                                        target.AddBuff(new Buff { Type = BuffType.Hiding, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = target.ObjectID, Values = new int[] { value } });
                                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                                        LevelMagic(magic);

                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;
                                        ChangeMP(-magic.ManaCost);
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            case Spell.UltimateEnhancer:
                                {
                                    MapObject target = null;
                                    if (!PlayerMaster.Buffs.Any(x => x.Type == BuffType.UltimateEnhancer) && (InHelpRange(PlayerMaster)))
                                    {
                                        target = PlayerMaster;
                                    }
                                    else if (!Buffs.Any(x => x.Type == BuffType.UltimateEnhancer))
                                    {
                                        target = this;
                                    }
                                    else if (PlayerMaster.GroupMembers != null)
                                    {
                                        var p = PlayerMaster.GroupMembers.FirstOrDefault(x => !x.Buffs.Any(y => y.Type == BuffType.UltimateEnhancer) && (InHelpRange(x)));
                                        target = p;
                                    }
                                    if (HeroNextTarget != null && HeroNextTarget.IsFriendlyTarget(this))
                                        target = HeroNextTarget;
                                    int value = MaxSC >= 5 ? Math.Min(8, MaxSC / 5) : 1;
                                    var item = GetAmulet(1);
                                    if (item == null) return;
                                    if (target != null)
                                    {
                                        target.AddBuff(new Buff { Type = BuffType.UltimateEnhancer, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = target.ObjectID, Values = new int[] { value } });
                                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                                        Broadcast(new S.ObjectEffect { Effect = SpellEffect.UEnhance, ObjectID = target.ObjectID });
                                        LevelMagic(magic);

                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;

                                        ChangeMP(-magic.ManaCost);
                                        ConsumeItem(item, 1);

                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            case Spell.Purification:
                                {
                                    if (PlayerMaster.PoisonList.Count > 0 || PoisonList.Count > 0 || (PlayerMaster.GroupMembers != null && PlayerMaster.GroupMembers.Any(x => x.PoisonList.Count > 0)))
                                    {

                                        MapObject target = null;
                                        if (PlayerMaster.PoisonList.Count > 0 && (InHelpRange(PlayerMaster)))
                                        {
                                            target = PlayerMaster;
                                        }
                                        else if (PoisonList.Count > 0)
                                        {
                                            target = this;
                                        }
                                        else if (PlayerMaster.GroupMembers != null)
                                        {
                                            var p = PlayerMaster.GroupMembers.FirstOrDefault(x => x.PoisonList.Count > 0 && (InHelpRange(x)));
                                            target = p;
                                        }
                                        if (HeroNextTarget != null && HeroNextTarget.IsFriendlyTarget(this))
                                            target = HeroNextTarget;
                                        if (target != null)
                                        {
                                            if (Envir.Random.Next(5 - magic.Level) == 1)
                                            {
                                                target.PoisonList.Clear();
                                                target.OperateTime = 0;

                                            }
                                            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

                                            LevelMagic(magic);

                                            ActionTime = Envir.Time + 300;
                                            AttackTime = Envir.Time + AttackSpeed;

                                            ChangeMP(-magic.ManaCost);
                                        }
                                        HeroNextSpell = Spell.None;
                                        HeroNextTarget = null;
                                        HeroNextTargetLocation = Point.Empty;
                                        return;
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            case Spell.SoulShield:
                                {
                                    SoulShieldDelay = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000;

                                    Point loc;
                                    MapObject trg;
                                    if (Envir.Random.Next(10) > 5)
                                    {
                                        loc = CurrentLocation;
                                        trg = this;
                                    }
                                    else
                                    {
                                        loc = PlayerMaster.CurrentLocation;
                                        trg = PlayerMaster;
                                    }
                                    if (HeroNextTarget != null && HeroNextTarget.CurrentLocation != Point.Empty)
                                    {
                                        trg = HeroNextTarget;
                                        loc = HeroNextTarget.CurrentLocation;
                                    }

                                    var item = GetAmulet(3);

                                    if (item != null && (InHelpRange(trg)))
                                    {
                                        List<MapObject> targets = FindAllNearby(4, loc, false);
                                        for (int i = 0; i < targets.Count; i++)
                                        {
                                            if (targets[i].Race.In(ObjectType.Hero, ObjectType.Player) && targets[i].IsFriendlyTarget(PlayerMaster))
                                            {
                                                int[] tmp;
                                                tmp = new int[1];
                                                tmp[0] = 
                                                    //  Level 0 = 4
                                                    magic.Level == 0 ? 4 : 
                                                    //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half) 1 (whole)
                                                    4 * (magic.Level + 1) / (targets[i] == this ? 2 : 1);

                                                targets[i].AddBuff(new Buff { Type = BuffType.SoulShield, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = targets[i].ObjectID, Values = tmp });
                                                targets[i].OperateTime = 0;
                                            }
                                        }
                                        Direction = Functions.DirectionFromPoint(CurrentLocation, loc);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = trg.ObjectID, Target = loc, Cast = true, Level = magic.Level });

                                        LevelMagic(magic);

                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;

                                        ChangeMP(-magic.ManaCost);

                                        ConsumeItem(item, 3);
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;

                                }
                            case Spell.BlessedArmour:
                                {
                                    ArmourShield = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000;
                                    Point loc;
                                    MapObject trg;
                                    if (Envir.Random.Next(10) > 5)
                                    {
                                        loc = CurrentLocation;
                                        trg = this;
                                    }
                                    else
                                    {
                                        loc = PlayerMaster.CurrentLocation;
                                        trg = PlayerMaster;
                                    }


                                    var item = GetAmulet(3);

                                    if (item != null && (InHelpRange(trg)))
                                    {
                                        List<MapObject> targets = FindAllNearby(4, loc, false);
                                        for (int i = 0; i < targets.Count; i++)
                                        {

                                            if (targets[i].Race.In(ObjectType.Hero, ObjectType.Player) && targets[i].IsFriendlyTarget(PlayerMaster))
                                            {
                                                int[] tmp;
                                                tmp = new int[1];
                                                tmp[0] =
                                                    //  Level 0 = 4
                                                    magic.Level == 0 ? 4 :
                                                    //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half) 1 (whole)
                                                    4 * (magic.Level + 1) / (targets[i] == this ? 2 : 1);

                                                targets[i].AddBuff(new Buff { Type = BuffType.BlessedArmour, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = targets[i].ObjectID, Values = tmp });
                                                targets[i].OperateTime = 0;
                                            }
                                        }
                                        Direction = Functions.DirectionFromPoint(CurrentLocation, loc);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = trg.ObjectID, Target = loc, Cast = true, Level = magic.Level });


                                        LevelMagic(magic);

                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;

                                        ChangeMP(-magic.ManaCost);
                                        ConsumeItem(item, 3);
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            case Spell.MassHealing:
                                {
                                    MapObject target = HeroNextTarget;
                                    Point location = HeroNextTarget.CurrentLocation;
                                    if (location != Point.Empty)
                                    {

                                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = location, Cast = true, Level = magic.Level });

                                        LevelMagic(magic);

                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;

                                        ChangeMP(-magic.ManaCost);

                                        for (int y = location.Y - 1; y <= location.Y + 1; y++)
                                        {
                                            if (y < 0) continue;
                                            if (y >= CurrentMap.Height) break;

                                            for (int x = location.X - 1; x <= location.X + 1; x++)
                                            {
                                                if (x < 0) continue;
                                                if (x >= CurrentMap.Width) break;

                                                var cell = CurrentMap.GetCell(x, y);

                                                if (!cell.Valid || cell.Objects == null) continue;

                                                for (int i = 0; i < cell.Objects.Count; i++)
                                                {
                                                    MapObject t = cell.Objects[i];
                                                    switch (t.Race)
                                                    {
                                                        case ObjectType.Monster:
                                                        case ObjectType.Player:
                                                        case ObjectType.Hero:
                                                            //Only targets
                                                            if (t.IsFriendlyTarget(PlayerMaster))
                                                            {
                                                                if (t.Health >= t.MaxHealth) continue;
                                                                t.HealAmount = (ushort)Math.Min(ushort.MaxValue, t.HealAmount + magic.GetDamage(GetAttackPower(MinSC, MaxSC)));
                                                                t.OperateTime = 0;
                                                            }
                                                            break;
                                                    }
                                                }

                                            }

                                        }
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            case Spell.Healing:
                                {
                                    int health = magic.GetDamage(GetAttackPower(MinSC, MaxSC) * 2) + Level;
                                    //Master
                                    var target = HeroNextTarget;
                                    if (InHelpRange(target) && target.Node != null)
                                    {
                                        if (target.Health < target.MaxHealth)
                                        {
                                            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });


                                            target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + health);
                                            target.OperateTime = 0;
                                            LevelMagic(magic);

                                            ActionTime = Envir.Time + 300;
                                            AttackTime = Envir.Time + AttackSpeed;

                                            ChangeMP(-magic.ManaCost);

                                        }
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            default:
                                HeroNextSpell = Spell.None;
                                HeroNextTarget = null;
                                HeroNextTargetLocation = Point.Empty;
                                return;
                        }
                    }
                }
                //  Not targeting anything so cast on self
                else if (HeroNextSpell == Spell.Hiding ||
                    HeroNextSpell == Spell.Healing)
                {
                    magic = Magics.FirstOrDefault(x => x.Spell == HeroNextSpell && x.Key > 0 && MP > x.ManaCost);
                    if (magic != null)
                    {
                        switch (HeroNextSpell)
                        {
                            case Spell.Hiding:
                                {
                                    MapObject target = this;

                                    int value = MaxSC >= 5 ? Math.Min(8, MaxSC / 5) : 1;
                                    if (target != null)
                                    {
                                        target.AddBuff(new Buff { Type = BuffType.Hiding, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = target.ObjectID, Values = new int[] { value } });
                                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                                        LevelMagic(magic);

                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;
                                        ChangeMP(-magic.ManaCost);
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            case Spell.Healing:
                                {
                                    int health = magic.GetDamage(GetAttackPower(MinSC, MaxSC) * 2) + Level;
                                    //Master
                                    //MapObject target = this;
                                    if (InHelpRange(this) && Node != null)
                                    {
                                        if (Health < MaxHealth)
                                        {
                                            Direction = Functions.DirectionFromPoint(CurrentLocation, CurrentLocation);
                                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = ObjectID, Target = CurrentLocation, Cast = true, Level = magic.Level });


                                            HealAmount = (ushort)Math.Min(ushort.MaxValue, HealAmount + health);
                                            OperateTime = 0;
                                            LevelMagic(magic);

                                            ActionTime = Envir.Time + 300;
                                            AttackTime = Envir.Time + AttackSpeed;

                                            ChangeMP(-magic.ManaCost);

                                        }
                                    }
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                        }
                    }
                }
                else
                    HeroNextTarget = null;
            }
            else
            {
                #region Taoist Hiding
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.Hiding && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && MonsterAttacked && (Envir.Random.Next(10 - (1 + magic.Level) * 2) == 1) && (!Buffs.Any(x => x.Type == BuffType.Hiding)))
                {
                    MapObject target = this;

                    int value = MaxSC >= 5 ? Math.Min(8, MaxSC / 5) : 1;
                    if (target != null)
                    {
                        target.AddBuff(new Buff { Type = BuffType.Hiding, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = target.ObjectID, Values = new int[] { value } });
                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                        LevelMagic(magic);

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        ChangeMP(-magic.ManaCost);

                        return;
                    }


                }
                #endregion

                #region Taoist UltimateEnhancer
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.UltimateEnhancer && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && (Envir.Random.Next(13 - magic.Level * 2) == 1))
                {
                    MapObject target = null;
                    if (!PlayerMaster.Buffs.Any(x => x.Type == BuffType.UltimateEnhancer) && (InHelpRange(PlayerMaster)))
                    {
                        target = PlayerMaster;
                    }
                    else if (!Buffs.Any(x => x.Type == BuffType.UltimateEnhancer))
                    {
                        target = this;
                    }
                    else if (PlayerMaster.GroupMembers != null)
                    {
                        var p = PlayerMaster.GroupMembers.FirstOrDefault(x => !x.Buffs.Any(y => y.Type == BuffType.UltimateEnhancer) && (InHelpRange(x)));
                        target = p;
                    }

                    var item = GetAmulet(1);//lmao no check if it's null? -,-
                    if (item == null)
                        return;
                    int value = MaxSC >= 5 ? Math.Min(8, MaxSC / 5) : 1;
                    if (target != null)
                    {
                        target.AddBuff(new Buff { Type = BuffType.UltimateEnhancer, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = target.ObjectID, Values = new int[] { value } });
                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });
                        Broadcast(new S.ObjectEffect { Effect = SpellEffect.UEnhance, ObjectID = target.ObjectID });
                        LevelMagic(magic);

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        ChangeMP(-magic.ManaCost);
                        ConsumeItem(item, 1);

                        return;
                    }


                }
                #endregion

                #region Taoist Purification
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.Purification && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && (Envir.Random.Next(10 - magic.Level * 2) == 1))
                {
                    if (PlayerMaster.PoisonList.Count > 0 || PoisonList.Count > 0 || (PlayerMaster.GroupMembers != null && PlayerMaster.GroupMembers.Any(x => x.PoisonList.Count > 0)))
                    {

                        MapObject target = null;
                        if (PlayerMaster.PoisonList.Count > 0 && (InHelpRange(PlayerMaster)))
                        {
                            target = PlayerMaster;
                        }
                        else if (PoisonList.Count > 0)
                        {
                            target = this;
                        }
                        else if (PlayerMaster.GroupMembers != null)
                        {
                            var p = PlayerMaster.GroupMembers.FirstOrDefault(x => x.PoisonList.Count > 0 && (InHelpRange(x)));
                            target = p;
                        }

                        if (target != null)
                        {
                            if (Envir.Random.Next(5 - magic.Level) == 1)
                            {
                                target.PoisonList.Clear();
                                target.OperateTime = 0;

                            }
                            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });

                            LevelMagic(magic);

                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;

                            ChangeMP(-magic.ManaCost);

                            return;
                        }

                    }



                }
                #endregion

                #region Taoist SoulShield
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.SoulShield && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && (Envir.Random.Next(5 - magic.Level) == 1) && SoulShieldDelay <= Envir.Time)
                {
                    SoulShieldDelay = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000;

                    Point loc;
                    MapObject trg;
                    if (Envir.Random.Next(10) > 5)
                    {
                        loc = CurrentLocation;
                        trg = this;
                    }
                    else
                    {
                        loc = PlayerMaster.CurrentLocation;
                        trg = PlayerMaster;
                    }


                    var item = GetAmulet(3);

                    if (item != null && (InHelpRange(trg)))
                    {
                        List<MapObject> targets = FindAllNearby(4, loc, false);
                        for (int i = 0; i < targets.Count; i++)
                        {
                            if (targets[i].Race.In(ObjectType.Hero, ObjectType.Player) && targets[i].IsFriendlyTarget(PlayerMaster))
                            {
                                int[] tmp;
                                tmp = new int[1];
                                tmp[0] =
                                    //  Level 0 = 4
                                    magic.Level == 0 ? 4 :
                                    //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half)
                                    4 * (magic.Level + 1) / (targets[i] == this ? 2 : 1);

                                targets[i].AddBuff(new Buff { Type = BuffType.SoulShield, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = targets[i].ObjectID, Values = tmp });
                                targets[i].OperateTime = 0;
                            }
                        }
                        Direction = Functions.DirectionFromPoint(CurrentLocation, loc);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = trg.ObjectID, Target = loc, Cast = true, Level = magic.Level });

                        LevelMagic(magic);

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        ChangeMP(-magic.ManaCost);

                        ConsumeItem(item, 3);
                        return;
                    }
                }
                #endregion

                #region Taoist BlessedArmour
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.BlessedArmour && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && (Envir.Random.Next(5 - magic.Level) == 1) && ArmourShield <= Envir.Time)
                {
                    ArmourShield = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000;
                    Point loc;
                    MapObject trg;
                    if (Envir.Random.Next(10) > 5)
                    {
                        loc = CurrentLocation;
                        trg = this;
                    }
                    else
                    {
                        loc = PlayerMaster.CurrentLocation;
                        trg = PlayerMaster;
                    }


                    var item = GetAmulet(3);

                    if (item != null && (InHelpRange(trg)))
                    {
                        List<MapObject> targets = FindAllNearby(4, loc, false);
                        for (int i = 0; i < targets.Count; i++)
                        {

                            if (targets[i].Race.In(ObjectType.Hero, ObjectType.Player) && targets[i].IsFriendlyTarget(PlayerMaster))
                            {
                                int[] tmp;
                                tmp = new int[1];
                                tmp[0] =
                                    //  Level 0 = 4
                                    magic.Level == 0 ? 4 :
                                    //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half)
                                    4 * (magic.Level + 1) / (targets[i] == this ? 2 : 1);

                                targets[i].AddBuff(new Buff { Type = BuffType.BlessedArmour, Caster = this, ExpireTime = Envir.Time + (GetAttackPower(MinSC, MaxSC) * 2 + (magic.Level + 1) * 10) * 1000, ObjectID = targets[i].ObjectID, Values = tmp });
                                targets[i].OperateTime = 0;
                            }
                        }
                        Direction = Functions.DirectionFromPoint(CurrentLocation, loc);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = trg.ObjectID, Target = loc, Cast = true, Level = magic.Level });


                        LevelMagic(magic);

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        ChangeMP(-magic.ManaCost);
                        ConsumeItem(item, 3);
                    }
                    return;
                }
                #endregion

                #region Taoist MassHealing
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.MassHealing && x.Key > 0 && MP > x.ManaCost);
                if (magic != null && (Envir.Random.Next(6 - magic.Level) == 1))
                {

                    Point location = Point.Empty;

                    //Master
                    var target = Master;
                    if (InHelpRange(target) && target.Node != null)
                    {
                        if (target.Health < target.MaxHealth)
                        {
                            location = target.CurrentLocation;
                        }
                    }

                    //Self
                    if (HP < MaxHP)
                    {
                        location = CurrentLocation;
                        target = this;
                    }

                    //Group pets
                    if (PlayerMaster.GroupMembers != null)
                    {
                        foreach (var g in GroupMembers)
                        {
                            foreach (var p in g.Pets)
                            {

                                target = p;
                                if (InHelpRange(target) && target.Node != null)
                                {
                                    if (target.Health < target.MaxHealth)
                                    {
                                        location = target.CurrentLocation;
                                        break;
                                    }
                                }

                            }
                        }
                    }


                    //Group
                    if (PlayerMaster.GroupMembers != null)
                    {
                        foreach (var g in GroupMembers)
                        {
                            target = g;
                            if (InHelpRange(target) && target.Node != null)
                            {
                                if (target.Health < target.MaxHealth)
                                {
                                    location = target.CurrentLocation;
                                    break;
                                }
                            }
                        }
                    }

                    if (location != Point.Empty)
                    {

                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = location, Cast = true, Level = magic.Level });

                        LevelMagic(magic);

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        ChangeMP(-magic.ManaCost);

                        for (int y = location.Y - 1; y <= location.Y + 1; y++)
                        {
                            if (y < 0) continue;
                            if (y >= CurrentMap.Height) break;

                            for (int x = location.X - 1; x <= location.X + 1; x++)
                            {
                                if (x < 0) continue;
                                if (x >= CurrentMap.Width) break;

                                var cell = CurrentMap.GetCell(x, y);

                                if (!cell.Valid || cell.Objects == null) continue;

                                for (int i = 0; i < cell.Objects.Count; i++)
                                {
                                    MapObject t = cell.Objects[i];
                                    switch (t.Race)
                                    {
                                        case ObjectType.Monster:
                                        case ObjectType.Player:
                                        case ObjectType.Hero:
                                            //Only targets
                                            if (t.IsFriendlyTarget(PlayerMaster))
                                            {
                                                if (t.Health >= t.MaxHealth) continue;
                                                t.HealAmount = (ushort)Math.Min(ushort.MaxValue, t.HealAmount + magic.GetDamage(GetAttackPower(MinSC, MaxSC)));
                                                t.OperateTime = 0;
                                            }
                                            break;
                                    }
                                }

                            }

                        }
                    }
                }
                #endregion

                #region Taoist Healing
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.Healing && x.Key > 0 && MP > x.ManaCost);
                if (magic != null && (Envir.Random.Next(5 - magic.Level) == 1))
                {

                    int health = magic.GetDamage(GetAttackPower(MinSC, MaxSC) * 2) + Level;
                    //Master
                    var target = Master;
                    if (InHelpRange(target) && target.Node != null)
                    {
                        if (target.Health < target.MaxHealth)
                        {
                            Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });


                            target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + health);
                            target.OperateTime = 0;
                            LevelMagic(magic);

                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;

                            ChangeMP(-magic.ManaCost);
                            return;
                        }
                    }
                    //Self
                    if (HP < MaxHP)
                    {
                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = ObjectID, Target = CurrentLocation, Cast = true, Level = magic.Level });


                        HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + health);
                        OperateTime = 0;
                        LevelMagic(magic);

                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        ChangeMP(-magic.ManaCost);
                        return;
                    }
                    //Group
                    if (PlayerMaster.GroupMembers != null)
                    {
                        foreach (var g in GroupMembers)
                        {
                            target = g;
                            if (InHelpRange(target) && target.Node != null)
                            {
                                if (!target.Dead && target.Health < target.MaxHealth)
                                {
                                    Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });


                                    target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + health);
                                    target.OperateTime = 0;
                                    LevelMagic(magic);
                                    ActionTime = Envir.Time + 300;
                                    AttackTime = Envir.Time + AttackSpeed;
                                    ChangeMP(-magic.ManaCost);
                                    return;
                                }
                            }
                        }
                    }
                    //Master Pets
                    foreach (var p in PlayerMaster.Pets)
                    {
                        target = p;
                        if (!target.Dead && InHelpRange(target) && target.Node != null)
                        {
                            if (target.Health < target.MaxHealth)
                            {

                                Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });


                                target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + health);
                                target.OperateTime = 0;
                                LevelMagic(magic);
                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;
                                ChangeMP(-magic.ManaCost);
                                return;
                            }
                        }
                    }
                    //Group pets
                    if (PlayerMaster.GroupMembers != null)
                    {
                        foreach (var g in GroupMembers)
                        {
                            foreach (var p in g.Pets)
                            {
                                target = p;
                                if (!target.Dead && InHelpRange(target) && target.Node != null)
                                {
                                    if (target.Health < target.MaxHealth)
                                    {
                                        Direction = Functions.DirectionFromPoint(CurrentLocation, target.CurrentLocation);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = target.ObjectID, Target = target.CurrentLocation, Cast = true, Level = magic.Level });


                                        target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + health);
                                        target.OperateTime = 0;
                                        LevelMagic(magic);
                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;
                                        ChangeMP(-magic.ManaCost);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                }
                #endregion
            }
        }


        protected virtual bool InHelpRange(MapObject target)
        {
            if (target.CurrentMap != CurrentMap) return false;

            return target.CurrentLocation != CurrentLocation && Functions.InRange(CurrentLocation, target.CurrentLocation, 15);
        }

        protected bool InMasterAttackRange(int dist)
        {
            if (Target == null) return false;
            if (Target.CurrentMap != CurrentMap) return false;

            return Target.CurrentLocation != Master.CurrentLocation && Functions.InRange(Master.CurrentLocation, Target.CurrentLocation, dist);
        }

        protected override void ProcessTarget()
        {
            if (Target == null || !CanAttack) return;

            if (Master.HMode == HeroMode.DontAttack || (Master.HMode == HeroMode.Offensive && !InMasterAttackRange(Envir.heroConfig.MasterAttackRange)))
            {
                Target = null;
                return;
            }
            //  Check the behaviour
            if (HeroBehaviour == HeroBehaviour.CloseMaster)
            {
                //  Check Both on the same map
                if (Master.CurrentMap == CurrentMap)
                {
                    //  Ensure we're 1 step away from the master
                    if (Functions.MaxDistance(CurrentLocation, Master.CurrentLocation) > 1)
                    {
                        //  Move back towards master
                        MoveTo(Master.CurrentLocation);
                        return;
                    }
                }
            }

            switch (HeroClass)
            {
                case MirClass.Wizard:
                    if (InAttackRange(HasRangeSkill() ? 9 : 1)) // 10 or 1
                    {
                        
                        if (Target == null || Target.Dead)
                            FindTarget();
                        WizardAttack();
                    }

                    if (Envir.Time < ShockTime)
                    {
                        Target = null;
                        return;
                    }

                    if (HasSpellActive(Spell.ThunderStorm) || HasSpellActive(Spell.FlameField))//sec
                        MoveTo(Target.CurrentLocation);
                    else
                        MoveTo(Master.CurrentLocation);
                    break;
                case MirClass.Warrior:
                    if (InAttackRange())
                    {
                        WarriorAttack();
                        if (Target == null || Target.Dead)
                            FindTarget();
                    }

                    if (Envir.Time < ShockTime)
                    {
                        Target = null;
                        return;
                    }
                    if (HeroBehaviour == HeroBehaviour.CloseMaster &&
                        //  Distance from Hero to Master
                        Functions.MaxDistance(CurrentLocation, Master.CurrentLocation) == 1 && // In range of Master
                        //  Distance from Master to Target
                        Functions.MaxDistance(Master.CurrentLocation, Target.CurrentLocation) == 1 && // In hit range of Master
                        //  Distance from Hero to Target
                        Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) > 1) //  Not within hit range
                    {
                        //  Move to the Target
                        MoveTo(Target.CurrentLocation);
                        return;
                    }
                    else if (HeroBehaviour == HeroBehaviour.CloseMaster &&
                        Functions.MaxDistance(CurrentLocation, Master.CurrentLocation) == 1)
                        return;
                    else
                    {
                        MoveTo(Target.CurrentLocation);
                        return;
                    }
                case MirClass.Assassin:
                    if (InAttackRange())
                    {
                        AssassinAttack();
                        if (Target == null || Target.Dead)
                            FindTarget();
                    }

                    if (Envir.Time < ShockTime)
                    {
                        Target = null;
                        return;
                    }

                    if (HeroBehaviour == HeroBehaviour.CloseMaster &&
                        Functions.MaxDistance(CurrentLocation, Master.CurrentLocation) == 1 &&
                        Functions.MaxDistance(Master.CurrentLocation, Target.CurrentLocation) == 1 &&
                        Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) > 1)
                    {
                        MoveTo(Target.CurrentLocation);
                        return;
                    }
                    else
                    {
                        MoveTo(Target.CurrentLocation);
                        return;
                    }
                case MirClass.Taoist:
                    if (InAttackRange(
                        HasRangeSkill() ? 
                        (Envir.Random.Next(3) == 1 ? 9 : 1)    // 10 or 1
                        : 1))// 1
                    {
                        if (HeroNextSpell == Spell.None)
                            TaoistAttack();
                        if (Target == null || Target.Dead)
                            FindTarget();
                    }

                    if (Envir.Time < ShockTime)
                    {
                        Target = null;
                        return;
                    }

                    if (HasSpellActive(Spell.SpiritSword))
                    {
                        if (Target != null)
                            MoveTo(Target.CurrentLocation);
                        return;
                    }
                    else
                    {
                        MoveTo(Master.CurrentLocation);
                        return;
                    }
            }
        }

        protected override void FindTarget()
        {
            
            base.FindTarget();

            if (Target == null ||
                (HeroNextTarget != null && Target != HeroNextTarget))
                Target = HeroNextTarget;
        }

        protected virtual void WizardAttack()
        {
            if (BindingShotCenter) ReleaseBindingShot();

            int damage;
            ShockTime = 0;

            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }
            if (LavaKingCasting) return;
            var Spells = GetAvailableSpells();

            if (Spells == null || Spells.Count == 0)
            {
                damage = GetAttackPower(MinDC, MaxDC);
                if (damage == 0) return;

                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

                Target.Attacked(this, damage);
                ActionTime = Envir.Time + 300;
                AttackTime = Envir.Time + AttackSpeed;

                return;
            }
            damage = GetAttackPower(MinMC, MaxMC);
            #region Wizard Kite behaviour
            if (HeroBehaviour == HeroBehaviour.Kite)
            {
                if (Envir.Time < fearTime && !HasSpellActive(Spell.ThunderStorm) && !HasSpell(Spell.FlameField))
                {
                    int dist = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation);

                    if (dist > 3)
                    {
                        MirDirection dir = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                        bool run = Functions.MaxDistance(CurrentLocation, CurrentLocation) > 3 ? false : true;

                        if (Envir.Random.Next(8) != 1 && Walk(dir, run)) return;

                        switch (Envir.Random.Next(2)) //No favour
                        {
                            case 0:
                                for (int i = 0; i < 7; i++)
                                {
                                    dir = Functions.NextDir(dir);

                                    if (Walk(dir, run))
                                        return;
                                }
                                break;
                            default:
                                for (int i = 0; i < 7; i++)
                                {
                                    dir = Functions.PreviousDir(dir);

                                    if (Walk(dir, run))
                                        return;
                                }
                                break;
                        }
                    }

                    return;
                }
                fearTime = Envir.Time + Envir.Random.Next(1000, 3000);
            }
            #endregion

            if (damage == 0) return;

            var manaSpells = Spells.Where(x => MP > x.ManaCost).ToList();

            if (manaSpells == null || manaSpells.Count == 0) return;
            #region Wizard Lighting Choosing
            if (manaSpells.Any(x => x.Spell == Spell.HellFire || x.Spell == Spell.ThunderStorm || x.Spell == Spell.FlameField))
            {
                var dist = 3;

                if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, dist)) return;
                bool targetFound = false;
                byte targetCount = 1;
                List<MapObject> objs = FindAllTargets(3, CurrentLocation, false);
                for (int i = 0; i < objs.Count; i++)
                    if (objs[i].IsAttackTarget(this))
                    {
                        targetFound = true;
                        targetCount++;
                    }
                if (!targetFound || targetCount == 1)
                {
                    manaSpells.RemoveAll(x => x.Spell == Spell.HellFire || x.Spell == Spell.ThunderStorm || x.Spell == Spell.FlameField);
                }
            }
            
            if (manaSpells.Any(x => x.Spell == Spell.Lightning))
            {
                var dist = 7;

                if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, dist)) return;

                var newLocation = CurrentLocation;
                var targetFound = false;
                var tempDir = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                for (int i = 0; i < dist; i++)
                {
                    newLocation = Functions.PointMove(newLocation, tempDir, 1);

                    if (!CurrentMap.ValidPoint(newLocation)) continue;

                    var cell = CurrentMap.GetCell(newLocation);

                    if (cell.Objects == null) continue;

                    for (int o = 0; o < cell.Objects.Count; o++)
                    {
                        MapObject target = cell.Objects[o];
                        if (target == Target)
                        {
                            targetFound = true;
                        }
                    }
                }

                if (!targetFound)
                {
                    manaSpells.RemoveAll(x => x.Spell == Spell.Lightning);
                }
            }
            #endregion


            if (manaSpells == null || manaSpells.Count == 0) return;
            #region Magic Shield
            var magic = manaSpells.FirstOrDefault(x => x.Spell == Spell.MagicShield && x.Key > 0 && MP > x.ManaCost);
            if (magic != null && ((Envir.Random.Next(5 - magic.Level) == 1) || HeroNextSpell == Spell.MagicShield))
            {
                if (HeroNextSpell != Spell.None)
                    HeroNextSpell = Spell.None;
                if (!MagicShieldUp)
                {
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });
                    
                    MagicShieldTime = Envir.Time + Settings.Second * (15 + magic.Level * 5);
                    //duh
                    AddBuff(new Buff { Type = BuffType.MagicShield, Caster = this, ObjectID = ObjectID, ExpireTime = MagicShieldTime, Values = new int[] { 15 } });
                    MagicShieldUp = true;
                    Broadcast(new S.ObjectEffect { ObjectID = ObjectID, Effect = SpellEffect.MagicShieldUp });

                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;

                    ChangeMP(-magic.ManaCost);
                    LevelMagic(magic);
                    return;
                }
            }


            manaSpells.RemoveAll(x => x.Spell == Spell.MagicShield);
            #endregion
            if (manaSpells.Count == 0) return;
            var chosen = manaSpells[Envir.Random.Next(manaSpells.Count)];
            UserMagic temp = null;
            if (HeroNextSpell != Spell.None)
            {
                bool hasSkill = false;
                for (int i = 0; i < Magics.Count; i++)
                    if (HeroNextSpell == Magics[i].Spell && !hasSkill)
                    {
                        hasSkill = true;
                        temp = Magics[i];
                    }
                        
                if (!hasSkill && temp == null)
                {
                    HeroNextSpell = Spell.None;
                }

            }
            if (temp != null)
                chosen = temp;
            if (chosen.Spell == Spell.IceThrust && Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) > 1) return;
            int delay = 0;
            DelayedAction action;
            ChangeMP(-chosen.ManaCost);
            switch (chosen.Spell)
            {
                #region Vampirism
                case Spell.Vampirism:
                    var VampAmount = (ushort)(damage * (chosen.Level + 1) * 0.25F);
                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });

                    action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, chosen.GetDamage(damage), DefenceType.MAC);
                    ActionList.Add(action);

                    ChangeHP(VampAmount);

                    break;
                #endregion
                #region Frost Crunch
                case Spell.FrostCrunch:
                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });
                    delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step
                    action = new DelayedAction(DelayedType.Magic, Envir.Time + delay, chosen.Spell, chosen.GetDamage(damage), Target);
                    ActionList.Add(action);
                    break;
                #endregion
                #region Thunder Storm
                case Spell.ThunderStorm:
                case Spell.FlameField:
                    var location = CurrentLocation;
                    bool train = false;

                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });


                    for (int y = location.Y - 2; y <= location.Y + 2; y++)
                    {
                        if (y < 0) continue;
                        if (y >= CurrentMap.Height) break;

                        for (int x = location.X - 2; x <= location.X + 2; x++)
                        {
                            if (x < 0) continue;
                            if (x >= CurrentMap.Width) break;

                            var cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject target = cell.Objects[i];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:
                                        //Only targets
                                        if (!target.IsAttackTarget(this)) break;

                                        if (target.Attacked(this, chosen.Spell == Spell.ThunderStorm && !target.Undead ? chosen.GetDamage(damage) / 10 : chosen.GetDamage(damage), DefenceType.MAC) <= 0)
                                        {
                                            if (chosen.Spell != Spell.FlameField &&
                                                target.Undead)
                                            {
                                                target.ApplyPoison(new Poison { PType = PoisonType.Stun, Duration = chosen.Level + 2, TickSpeed = 1000 }, this);
                                            }
                                            break;
                                        }

                                        train = true;
                                        break;
                                }
                            }

                        }
                    }

                    ChangeMP(-chosen.ManaCost);
                    if (train)
                        LevelMagic(chosen);

                    break;
                #endregion
                #region Fire Bang
                case Spell.FireBang:

                    var value = damage;
                    location = Target.CurrentLocation;

                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });


                    train = false;
                    for (int y = location.Y - 1; y <= location.Y + 1; y++)
                    {
                        if (y < 0) continue;
                        if (y >= CurrentMap.Height) break;

                        for (int x = location.X - 1; x <= location.X + 1; x++)
                        {
                            if (x < 0) continue;
                            if (x >= CurrentMap.Width) break;

                            var cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject target = cell.Objects[i];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:
                                        //Only targets
                                        if (target.IsAttackTarget(this))
                                        {
                                            if (target.Attacked(this, chosen.GetDamage(value), DefenceType.MAC) > 0)
                                                train = true;
                                        }
                                        break;
                                }
                            }

                        }
                        if (train)
                            LevelMagic(chosen);
                        ChangeMP(-chosen.ManaCost);
                    }

                    break;
                #endregion
                #region Fire Wall
                case Spell.FireWall:

                    value = damage;
                    location = Target.CurrentLocation;

                    LevelMagic(chosen);
                    ChangeMP(-chosen.ManaCost);

                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });


                    if (CurrentMap.ValidPoint(location))
                    {
                        var cell = CurrentMap.GetCell(location);

                        bool cast = true;
                        if (cell.Objects != null)
                            for (int o = 0; o < cell.Objects.Count; o++)
                            {
                                MapObject target = cell.Objects[o];
                                if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.FireWall) continue;

                                cast = false;
                                break;
                            }

                        if (cast)
                        {
                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.FireWall,
                                Value = chosen.GetDamage(value),
                                ExpireTime = Envir.Time + (10 + value / 2) * 1000,
                                TickSpeed = 2000,
                                Caster = PlayerMaster,
                                CurrentLocation = location,
                                CurrentMap = CurrentMap,
                            };
                            CurrentMap.AddObject(ob);
                            ob.Spawned();
                        }
                    }

                    var dir = MirDirection.Up;
                    for (int i = 0; i < 4; i++)
                    {
                        location = Functions.PointMove(Target.CurrentLocation, dir, 1);
                        dir += 2;

                        if (!CurrentMap.ValidPoint(location)) continue;

                        var cell = CurrentMap.GetCell(location);
                        bool cast = true;

                        if (cell.Objects != null)
                            for (int o = 0; o < cell.Objects.Count; o++)
                            {
                                MapObject target = cell.Objects[o];
                                if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.FireWall) continue;

                                cast = false;
                                break;
                            }

                        if (!cast) continue;

                        SpellObject ob = new SpellObject
                        {
                            Spell = Spell.FireWall,
                            Value = chosen.GetDamage(value),
                            ExpireTime = Envir.Time + (10 + value / 2) * 1000,
                            TickSpeed = 2000,
                            Caster = PlayerMaster,
                            CurrentLocation = location,
                            CurrentMap = CurrentMap,
                        };
                        CurrentMap.AddObject(ob);
                        ob.Spawned();
                    }

                    break;
                #endregion
                #region Hell Fire & Lightning
                case Spell.HellFire:
                case Spell.Lightning:

                    var dist = (chosen.Spell == Spell.HellFire ? 4 : 7);

                    if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, dist)) return;

                    train = false;
                    var newLocation = CurrentLocation;
                    var tempDirection = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    for (int i = 0; i < dist; i++)
                    {
                        newLocation = Functions.PointMove(newLocation, tempDirection, 1);

                        if (!CurrentMap.ValidPoint(newLocation)) continue;

                        var cell = CurrentMap.GetCell(newLocation);

                        if (cell.Objects == null) continue;

                        for (int o = 0; o < cell.Objects.Count; o++)
                        {
                            MapObject target = cell.Objects[o];
                            if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;

                            if (!target.IsAttackTarget(this)) continue;
                            if (target.Attacked(this, chosen.GetDamage(damage), DefenceType.MAC) > 0)
                                train = true;
                            break;
                        }
                    }
                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });


                    ChangeMP(-chosen.ManaCost);
                    if (train)
                        LevelMagic(chosen);
                    break;
                #endregion
                #region Turn Undead
                case Spell.TurnUndead:

                    if (Target.Race != ObjectType.Monster || !Target.Undead)
                    {
                        return;
                    }

                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });


                    if (Envir.Random.Next(2) + HeroLevel - 1 <= Target.Level)
                    {
                        Target.Target = this;
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;
                        return;
                    }

                    int dif = HeroLevel - Target.Level + 3;  //Tu Ice

                    if (Envir.Random.Next(100) >= (Target.Level + 1 << 3) + dif)
                    {
                        Target.Target = this;
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;
                        return;
                    }

                    Target.LastHitter = this.Master;
                    Target.LastHitTime = Envir.Time + 5000;
                    Target.EXPOwner = this.Master;
                    Target.EXPOwnerTime = Envir.Time + 5000;
                    Target.Die();

                    break;
                #endregion
                #region IceThrust / LavaKing
                case Spell.IceThrust:
                    {
                        
                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });
                        action = new DelayedAction(DelayedType.MonsterMagic, Envir.Time + 1500, this, chosen.Spell, CurrentLocation, Direction, chosen.GetDamage(damage), (int)(chosen.GetDamage(damage) * 0.6), (int)chosen.Level);
                        CurrentMap.ActionList.Add(action);
                    }
                    break;
                case Spell.LavaKing:
                    {
                        LavaKingCasting = true;
                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });
                        action = new DelayedAction(DelayedType.MonsterMagic, Envir.Time + 500, this, chosen.Spell, damage, Target.CurrentLocation, (int)chosen.Level);
                        CurrentMap.ActionList.Add(action);
                    }
                    break;
                default:
                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = chosen.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = chosen.Level });
                    delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step

                    action = new DelayedAction(DelayedType.Magic, Envir.Time + delay, chosen.Spell, damage, Target);
                    ActionList.Add(action);
                    break;
                    #endregion
            }
            HeroNextSpell = Spell.None;
            HeroNextTarget = null;
            HeroNextTargetLocation = Point.Empty;
            LevelMagic(chosen);
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;
        }

        protected virtual void TaoistAttack()
        {
            if (BindingShotCenter) ReleaseBindingShot();

            ShockTime = 0;
            DelayedAction action;
            if (Target.Race != ObjectType.Player && Target.Race != ObjectType.Monster && Target.Race != ObjectType.Hero)
                return;
            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            int damage;

            damage = GetAttackPower(MinSC, MaxSC);
            if (damage != 0)
            {
                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                UserMagic magic = null;


                if (HeroNextSpell != Spell.None)
                {
                    bool hasSpell = false;
                    for (int i = 0; i < Magics.Count; i++)
                    {
                        if (Magics[i].Spell == HeroNextSpell && !hasSpell && Magics[i].Key > 0)
                        {
                            hasSpell = true;
                            magic = Magics[i];
                        }
                    }
                    if (!hasSpell || magic.ManaCost > MP)
                    {
                        HeroNextSpell = Spell.None;
                        HeroNextTarget = null;
                        HeroNextTargetLocation = Point.Empty;
                        return;
                    }

                    switch(magic.Spell)
                    {
                        case Spell.Poisoning:
                            {
                                var item = GetPoison(1);

                                if (item != null)
                                {
                                    if ((item.Info.Shape == 1 && !Target.PoisonList.Any(x => x.PType == PoisonType.Green)) || (item.Info.Shape == 2 && !Target.PoisonList.Any(x => x.PType == PoisonType.Red)))
                                    {
                                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                                        int power = magic.GetDamage(GetAttackPower(MinSC, MaxSC));

                                        switch (item.Info.Shape)
                                        {
                                            case 1:
                                                Target.ApplyPoison(new Poison
                                                {
                                                    Duration = (power * 2) + ((magic.Level + 1) * 7),
                                                    Owner = PlayerMaster,
                                                    PType = PoisonType.Green,
                                                    TickSpeed = 2000,
                                                    Value = power / 15 + magic.Level + 1 + Envir.Random.Next(PoisonAttack)
                                                }, this);
                                                break;
                                            case 2:
                                                Target.ApplyPoison(new Poison
                                                {
                                                    Duration = (power * 2) + (magic.Level + 1) * 7,
                                                    Owner = PlayerMaster,
                                                    PType = PoisonType.Red,
                                                    TickSpeed = 2000,
                                                }, this);
                                                break;
                                        }
                                        Target.OperateTime = 0;

                                        LevelMagic(magic);
                                        ChangeMP(-magic.ManaCost);

                                        ConsumeItem(item, 1);

                                        ActionTime = Envir.Time + 300;
                                        AttackTime = Envir.Time + AttackSpeed;
                                        HeroNextSpell = Spell.None;
                                        HeroNextTarget = null;
                                        HeroNextTargetLocation = Point.Empty;
                                        return;
                                    }
                                }
                            }
                            break;
                        case Spell.SoulFireBall:
                            {
                                var item = GetAmulet(1);

                                if (item != null)
                                {
                                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                                    action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, magic.GetDamage(damage), DefenceType.MAC);
                                    ActionList.Add(action);

                                    LevelMagic(magic);
                                    ChangeMP(-magic.ManaCost);

                                    ConsumeItem(item, 1);

                                    ActionTime = Envir.Time + 300;
                                    AttackTime = Envir.Time + AttackSpeed;
                                    HeroNextSpell = Spell.None;
                                    HeroNextTarget = null;
                                    HeroNextTargetLocation = Point.Empty;
                                    return;
                                }
                            }
                            break;
                        case Spell.Revelation:
                            {
                                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                                Target.RevTime = Envir.Time + ((1 + magic.Level) * 10) * 1000;
                                Target.OperateTime = 0;
                                Target.BroadcastHealthChange();

                                LevelMagic(magic);
                                ChangeMP(-magic.ManaCost);
                                HeroNextSpell = Spell.None;
                                HeroNextTarget = null;
                                HeroNextTargetLocation = Point.Empty;
                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;
                                return;
                            }
                        case Spell.Plague:
                            {
                                if (Target == null)
                                    return;
                                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                                UserItem item = GetAmulet(1);
                                if (item == null) return;

                                int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step


                                PoisonType pType = PoisonType.None;

                                UserItem itemp = GetPoison(1, 2);

                                if (itemp != null)
                                    pType = PoisonType.Red;
                                else
                                {
                                    itemp = GetPoison(1, 1);

                                    if (itemp != null)
                                        pType = PoisonType.Green;
                                }
                                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });
                                action = new DelayedAction(DelayedType.Magic, delay, magic.Spell, damage, Target, pType, (int)magic.Level);

                                ActionList.Add(action);
                                ConsumeItem(item, 1);
                                if (itemp != null) ConsumeItem(itemp, 1);
                                HeroNextSpell = Spell.None;
                                HeroNextTarget = null;
                                HeroNextTargetLocation = Point.Empty;
                                return;
                            }
                        case Spell.HeadShot:
                            {
                                if (Target == null)
                                    return;
                                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                                UserItem item = GetAmulet(1);
                                if (item == null) return;

                                ConsumeItem(item, 1);
                                if (Target == null || !Target.IsAttackTarget(this) || !CanFly(Target.CurrentLocation)) return;

                                damage = magic.GetDamage(GetAttackPower(MinSC, MaxSC));

                                int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step
                                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });
                                action = new DelayedAction(DelayedType.Magic, Envir.Time + delay, magic.Spell, damage, Target, (int)magic.Level);
                                HeroNextSpell = Spell.None;
                                HeroNextTarget = null;
                                HeroNextTargetLocation = Point.Empty;
                                ActionList.Add(action);
                                return;
                            }
                        default:
                            {
                                HeroNextSpell = Spell.None;
                                HeroNextTarget = null;
                                HeroNextTargetLocation = Point.Empty;
                                return;
                            }
                    }
                    return;
                }
                else
                {
                    #region Poisoning
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.Poisoning && x.Key > 0 && MP > x.ManaCost);

                    if (magic != null && (Envir.Random.Next(6 - magic.Level) == 1))
                    {
                        var item = GetPoison(1);

                        if (item != null)
                        {
                            if ((item.Info.Shape == 1 && !Target.PoisonList.Any(x => x.PType == PoisonType.Green)) || (item.Info.Shape == 2 && !Target.PoisonList.Any(x => x.PType == PoisonType.Red)))
                            {
                                Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                                int power = magic.GetDamage(GetAttackPower(MinSC, MaxSC));

                                switch (item.Info.Shape)
                                {
                                    case 1:
                                        Target.ApplyPoison(new Poison
                                        {
                                            Duration = (power * 2) + ((magic.Level + 1) * 7),
                                            Owner = PlayerMaster,
                                            PType = PoisonType.Green,
                                            TickSpeed = 2000,
                                            Value = power / 15 + magic.Level + 1 + Envir.Random.Next(PoisonAttack)
                                        }, this);
                                        break;
                                    case 2:
                                        Target.ApplyPoison(new Poison
                                        {
                                            Duration = (power * 2) + (magic.Level + 1) * 7,
                                            Owner = PlayerMaster,
                                            PType = PoisonType.Red,
                                            TickSpeed = 2000,
                                        }, this);
                                        break;
                                }
                                Target.OperateTime = 0;

                                LevelMagic(magic);
                                ChangeMP(-magic.ManaCost);

                                ConsumeItem(item, 1);

                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;



                                return;
                            }
                        }
                    }
                    #endregion
                    #region Soul Fire Ball
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.SoulFireBall && x.Key > 0 && MP > x.ManaCost);

                    if (magic != null && (Envir.Random.Next(3) == 1 || !Functions.InRange(CurrentLocation, Target.CurrentLocation, 1)))
                    {
                        var item = GetAmulet(1);

                        if (item != null)
                        {
                            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                            action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, magic.GetDamage(damage), DefenceType.MAC);
                            ActionList.Add(action);

                            LevelMagic(magic);
                            ChangeMP(-magic.ManaCost);

                            ConsumeItem(item, 1);

                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;



                            return;
                        }
                    }
                    #endregion
                    #region Revelation
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.Revelation && x.Key > 0 && MP > x.ManaCost);

                    if (magic != null && (Envir.Random.Next(15 - magic.Level * 2) == 1))
                    {
                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                        Target.RevTime = Envir.Time + ((1 + magic.Level) * 10) * 1000;
                        Target.OperateTime = 0;
                        Target.BroadcastHealthChange();

                        LevelMagic(magic);
                        ChangeMP(-magic.ManaCost);


                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;



                        return;

                    }
                    #endregion
                    #region Plague
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.Plague && x.Key > 0 && MP > x.ManaCost);
                    if (magic != null && (Envir.Random.Next(15 - magic.Level * 2) == 1))
                    {
                        if (Target == null)
                            return;
                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                        UserItem item = GetAmulet(1);
                        if (item == null) return;

                        int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step


                        PoisonType pType = PoisonType.None;

                        UserItem itemp = GetPoison(1, 2);

                        if (itemp != null)
                            pType = PoisonType.Red;
                        else
                        {
                            itemp = GetPoison(1, 1);

                            if (itemp != null)
                                pType = PoisonType.Green;
                        }
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });
                        action = new DelayedAction(DelayedType.Magic, delay, magic.Spell, damage, Target, pType, (int)magic.Level);

                        ActionList.Add(action);
                        ConsumeItem(item, 1);
                        if (itemp != null) ConsumeItem(itemp, 1);
                        return;
                    }
                    #endregion
                    #region HeadShot
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.HeadShot && x.Key > 0 && MP > x.ManaCost);
                    if (magic != null && (Envir.Random.Next(15 - magic.Level * 2) == 1))
                    {
                        if (Target == null)
                            return;
                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

                        UserItem item = GetAmulet(1);
                        if (item == null) return;

                        ConsumeItem(item, 1);
                        if (Target == null || !Target.IsAttackTarget(this) || !CanFly(Target.CurrentLocation)) return;

                        damage = magic.GetDamage(GetAttackPower(MinSC, MaxSC));

                        int delay = Functions.MaxDistance(CurrentLocation, Target.CurrentLocation) * 50 + 500; //50 MS per Step
                        Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });
                        action = new DelayedAction(DelayedType.Magic, Envir.Time + delay, magic.Spell, damage, Target, (int)magic.Level);

                        ActionList.Add(action);
                        return;
                    }
                    damage = GetAttackPower(MinDC, MaxDC);
                    if (damage == 0) return;
                    if (InAttackRange(1))
                    {
                        Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                        Target.Attacked(this, damage);
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;

                        var tempSpell = Magics.FirstOrDefault(x => x.Spell == Spell.SpiritSword && x.Key > 0);
                        if (tempSpell != null)
                        {
                            LevelMagic(tempSpell);
                        }
                    }
                    #endregion
                }
            }
        }

        private UserItem GetAmulet(int count, int shape = 0)
        {
            for (int i = 0; i < Equipment.Length; i++)
            {
                UserItem item = Equipment[i];
                if (item != null && item.Info.Type == ItemType.Amulet && item.Info.Shape == shape && item.Count >= count)
                    return item;
            }

            return null;
        }
        private UserItem GetPoison(int count, byte shape = 0)
        {
            for (int i = 0; i < Equipment.Length; i++)
            {
                UserItem item = Equipment[i];
                if (item != null && item.Info.Type == ItemType.Poison && item.Count >= count)
                {
                    if (shape == 0)
                    {
                        if (item.Info.Shape == 1 || item.Info.Shape == 2)
                            return item;
                    }
                    else
                    {
                        if (item.Info.Shape == shape)
                            return item;
                    }
                }
            }

            return null;
        }


        protected virtual void AssassinAttack()
        {
            if (BindingShotCenter) ReleaseBindingShot();

            ShockTime = 0;

            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            int damage;
            ActionTime = Envir.Time + 300;
            AttackTime = Envir.Time + AttackSpeed;


            damage = GetAttackPower(MinDC, MaxDC);
            if (damage == 0) return;


            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            if (HeroNextSpell != Spell.None)
            {
                UserMagic tmpMagic = null;
                for (int i = 0; i < Magics.Count; i++)
                    if (Magics[i].Spell == HeroNextSpell)
                        tmpMagic = Magics[i];
                if (tmpMagic == null)
                {
                    HeroNextSpell = Spell.None;
                    return;
                }
                switch (HeroNextSpell)
                {
                    case Spell.HeavenlySword:
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;
                        PerformHeavenlySword(tmpMagic, damage);
                        HeroNextSpell = Spell.None;
                        return;
                    case Spell.PoisonSword:
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;
                        PerformPoisonSword(tmpMagic, damage);
                        HeroNextSpell = Spell.None;
                        return;
                }
            }
            else
            {
                UserMagic magic = null;
                #region Haste
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.Haste && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && Envir.Random.Next(5 - magic.Level) == 1 && !Buffs.Any(x => x.Type == BuffType.Haste))
                {
                    AddBuff(new Buff { Type = BuffType.Haste, Caster = PlayerMaster, ExpireTime = Envir.Time + ((magic.Level + 3) * 10) * 1000, ObjectID = ObjectID, Values = new int[] { (magic.Level + 1) * 2 } });
                    LevelMagic(magic);

                    ChangeMP(-magic.ManaCost);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                    return;
                }
                #endregion
                #region Light Body
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.LightBody && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && Envir.Random.Next(5 - magic.Level) == 1 && !Buffs.Any(x => x.Type == BuffType.LightBody))
                {
                    AddBuff(new Buff { Type = BuffType.LightBody, Caster = PlayerMaster, ExpireTime = Envir.Time + ((magic.Level + 3) * 10) * 1000, ObjectID = ObjectID, Values = new int[] { (magic.Level + 1) } });
                    LevelMagic(magic);

                    ChangeMP(-magic.ManaCost);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });

                    return;
                }

                #endregion
                #region Heavenly Sword
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.HeavenlySword && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && Envir.Random.Next(10 - magic.Level) == 1)
                {
                    var train = false;
                    var location = CurrentLocation;
                    for (int i = 0; i < 3; i++)
                    {
                        location = Functions.PointMove(location, Direction, 1);

                        if (!CurrentMap.ValidPoint(location)) continue;

                        var cell = CurrentMap.GetCell(location);

                        if (cell.Objects == null) continue;

                        for (int o = 0; o < cell.Objects.Count; o++)
                        {
                            MapObject target = cell.Objects[o];
                            if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;

                            if (!target.IsAttackTarget(this)) continue;
                            if (target.Attacked(this, damage, DefenceType.MAC) > 0)
                                train = true;
                            break;
                        }
                    }
                    Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });


                    ChangeMP(-magic.ManaCost);
                    if (train)
                        LevelMagic(magic);

                    return;
                }
                #endregion
                #region Fatal Sword
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.FatalSword && x.Key > 0);

                if (magic != null && Envir.Random.Next(10 - magic.Level) == 1)
                {
                    damage = (int)((damage + 1 + magic.Level) * 1.10f);
                    S.ObjectEffect p = new S.ObjectEffect { ObjectID = Target.ObjectID, Effect = SpellEffect.FatalSword };
                    PlayerMaster.Enqueue(p);
                    LevelMagic(magic);
                }
                #endregion
                #region Poison Sword
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.PoisonSword && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && Envir.Random.Next(5 - ((magic.Level))) == 1)
                {
                    Point hitPoint;
                    Cell cell;
                    MirDirection dir = Functions.PreviousDir(Direction);
                    int power = magic.GetDamage(GetAttackPower(MinDC, MaxDC));

                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.PoisonSword });
                    for (int i = 0; i < 5; i++)
                    {
                        hitPoint = Functions.PointMove(CurrentLocation, dir, 1);
                        dir = Functions.NextDir(dir);

                        if (!CurrentMap.ValidPoint(hitPoint)) continue;
                        cell = CurrentMap.GetCell(hitPoint);

                        if (cell.Objects == null) continue;

                        for (int o = 0; o < cell.Objects.Count; o++)
                        {
                            MapObject target = cell.Objects[o];
                            if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;
                            if (target == null || !target.IsAttackTarget(this) || target.Node == null) continue;

                            target.ApplyPoison(new Poison
                            {
                                Duration = 3 + power / 10 + magic.Level * 3,
                                Owner = this,
                                PType = PoisonType.Green,
                                TickSpeed = 1000,
                                Value = power / 10 + magic.Level + 1 + Envir.Random.Next(PoisonAttack)
                            }, this);

                            target.OperateTime = 0;
                            break;
                        }
                    }

                    
                }
                #endregion
                #region Double Slash
                if (DoubleSlashOn)
                {
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.DoubleSlash && x.Key > 0 && MP > x.ManaCost);

                    if (magic != null)
                    {
                        switch (magic.Level)
                        {
                            case 0:
                                Target.Attacked(this, (int)(damage * 0.6));
                                break;
                            case 1:
                                Target.Attacked(this, (int)(damage * 0.7));
                                break;
                            case 2:
                                Target.Attacked(this, (int)(damage * 0.8));
                                break;
                            case 3:
                                Target.Attacked(this, (int)(damage * 0.9));
                                break;


                        }

                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.DoubleSlash });
                        DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, (int)(damage * 0.9f), DefenceType.AC);
                        ActionList.Add(action);

                        ChangeMP(-magic.ManaCost);
                        LevelMagic(magic);
                        return;
                    }
                }
                #endregion
                #region Hemorrhage
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.Hemorrhage && x.Key > 0 && MP > x.ManaCost);

                if (magic != null && Envir.Random.Next(5 - ((magic.Level))) == 1)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.Hemorrhage });
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });
                    HemorrhageAttackCount += Envir.Random.Next(1, 1 + magic.Level * 2);
                    if (Hemorrhage)
                    {
                        damage = magic.GetDamage(damage);
                        LevelMagic(magic);
                        S.ObjectEffect ef = new S.ObjectEffect { ObjectID = Target.ObjectID, Effect = SpellEffect.Hemorrhage };

                        CurrentMap.Broadcast(ef, Target.CurrentLocation);

                        long calcDuration = magic.Level * 2 + Luck / 6;

                        Target.ApplyPoison(new Poison
                        {
                            Duration = (calcDuration <= 0) ? 1 : calcDuration,
                            Owner = this,
                            PType = PoisonType.Bleeding,
                            TickSpeed = 1500,
                            Value = MinDC + 1
                        }, this);

                        Target.OperateTime = 0;
                        HemorrhageAttackCount = 0;
                        Hemorrhage = false;
                    }
                    else if (!Hemorrhage && 55 <= HemorrhageAttackCount) Hemorrhage = true;
                    ChangeMP(-magic.ManaCost);
                    LevelMagic(magic);
                    return;
                }
                #endregion
                #region CrescentSlash
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.CrescentSlash && x.Key > 0 && MP > x.ManaCost);
                if (magic != null && Envir.Random.Next(30 - ((magic.Level + 1) * 3)) == 1)
                {
                    int damageBase = GetAttackPower(MinDC, MaxDC);
                    if (Envir.Random.Next(0, 100) <= Accuracy)
                        damageBase += damageBase;
                    int damageFinal = magic.GetDamage(damageBase);

                    int col = 5;
                    int row = 3;

                    List<MapObject> TargetsList = new List<MapObject>();
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.CrescentSlash });
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = magic.Spell, TargetID = Target.ObjectID, Target = Target.CurrentLocation, Cast = true, Level = magic.Level });
                    Point[] loc = new Point[col]; //0 = left 1 = center 2 = right
                    loc[0] = Functions.PointMove(CurrentLocation, Functions.PreviousDir(Direction), 1);
                    loc[1] = Functions.PointMove(CurrentLocation, Direction, 1);
                    loc[2] = Functions.PointMove(CurrentLocation, Functions.NextDir(Direction), 1);
                    loc[3] = Functions.PointMove(loc[0], Functions.PreviousDir(Direction), 1);
                    loc[4] = Functions.PointMove(loc[2], Functions.NextDir(Direction), 1);
                    
                    for (int i = 0; i < col; i++)
                    {
                        Point startPoint = loc[i];
                        for (int j = 0; j < row; j++)
                        {
                            Point hitPoint = Functions.PointMove(startPoint, Direction, j);

                            if (!CurrentMap.ValidPoint(hitPoint)) continue;

                            Cell cell = CurrentMap.GetCell(hitPoint);

                            if (cell.Objects == null) continue;

                            for (int k = 0; k < cell.Objects.Count; k++)
                            {
                                MapObject target = cell.Objects[k];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:
                                        //Only targets
                                        if (target.IsAttackTarget(this))
                                        {
                                            if (target.Attacked(this, damageFinal, DefenceType.MAC) > 0)
                                                LevelMagic(magic);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                Target.Attacked(this, damage);
            }
            #endregion
        }

        protected virtual void WarriorAttack()
        {
            if (BindingShotCenter) ReleaseBindingShot();

            ShockTime = 0;

            if (!Target.IsAttackTarget(this))
            {
                Target = null;
                return;
            }

            int damage = GetAttackPower(MinDC, MaxDC);
            if (damage == 0) return;
            
            
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            if (HeroNextSpell != Spell.None)
            {
                UserMagic tmpMagic = null;
                for (int i = 0; i < Magics.Count; i++)
                    if (Magics[i].Spell == HeroNextSpell)
                        tmpMagic = Magics[i];
                if (tmpMagic == null)
                {
                    HeroNextSpell = Spell.None;
                    return;
                }
                switch (HeroNextSpell)
                {
                    case Spell.TwinDrakeBlade:
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;
                        PerformTwinDrakeBlade(tmpMagic, damage);
                        HeroNextSpell = Spell.None;
                        return;
                    case Spell.FlamingSword:
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;
                        PerformFlamingSword(tmpMagic, damage);
                        HeroNextSpell = Spell.None;
                        return;
                }
            }
            else
            {
                UserMagic magic = null;
                #region Flaming Sword
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.FlamingSword && x.Key > 0);
                if (magic != null && Envir.Random.Next(26 - (magic.Level * 2)) == 1 && MP > magic.ManaCost)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.FlamingSword });
                    switch (magic.Level)
                    {
                        case 0:
                            Target.Attacked(this, (int)(damage * 1.4));
                            break;
                        case 1:
                            Target.Attacked(this, (int)(damage * 1.8));
                            break;
                        case 2:
                            Target.Attacked(this, (int)(damage * 2.2));
                            break;
                        case 3:
                            Target.Attacked(this, (int)(damage * 2.6));
                            break;
                    }
                    ChangeMP(-magic.ManaCost);
                    LevelMagic(magic);
                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;
                    return;
                }
                #endregion
                #region Twin Drake Blade
                magic = Magics.FirstOrDefault(x => x.Spell == Spell.TwinDrakeBlade && x.Key > 0);

                if (magic != null && Envir.Random.Next(15 - ((2 + magic.Level) * 2)) == 1 && MP > magic.ManaCost)
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.TwinDrakeBlade });
                    Target.Attacked(this, (int)(damage));
                    DelayedAction action = new DelayedAction(DelayedType.Damage, Envir.Time + 500, Target, damage, DefenceType.AC);
                    ActionList.Add(action);

                    ChangeMP(-magic.ManaCost);
                    LevelMagic(magic);
                    ActionTime = Envir.Time + 300;
                    AttackTime = Envir.Time + AttackSpeed;
                    return;
                }
                #endregion
                #region Slaying
                if (SlayingOn)
                {
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.Slaying && x.Key > 0);

                    if (magic != null && Envir.Random.Next(8 - magic.Level) == 1)
                    {
                        Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.Slaying });
                        Target.Attacked(this, (int)((damage + magic.Level + 1) * 1.10f));
                        LevelMagic(magic);
                        ActionTime = Envir.Time + 300;
                        AttackTime = Envir.Time + AttackSpeed;
                        return;
                    }
                }
                #endregion
                #region Thrusting
                if (ThrustingOn)
                {
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.Thrusting && x.Key > 0);

                    if (magic != null)
                    {
                        Point target = Functions.PointMove(CurrentLocation, Direction, 2);
                        bool hitAny = false;
                        if (CurrentMap.ValidPoint(target))
                        {
                            Cell cell = CurrentMap.GetCell(target);

                            if (cell.Objects != null)
                            {

                                LevelMagic(magic);

                                for (int i = 0; i < cell.Objects.Count; i++)
                                {
                                    MapObject ob = cell.Objects[i];
                                    if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) continue;
                                    if (!ob.IsAttackTarget(this)) continue;

                                    if (ob.Attacked(this, damage, DefenceType.ACAgility) > 0)
                                        hitAny = true;
                                }
                            }
                        }
                        if (hitAny)
                        {
                            LevelMagic(magic);
                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.Thrusting });
                        }
                        else
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                            Target.Attacked(this, damage);

                            var tmp = Magics.FirstOrDefault(x => x.Spell == Spell.Fencing && x.Key > 0);
                            if (tmp != null)
                            {
                                LevelMagic(tmp);
                            }
                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;
                        }
                        return;
                    }
                }
                #endregion
                #region Cross Halfmoon
                if (CrossHalfMoonOn)
                {
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.CrossHalfMoon && x.Key > 0 && MP >= x.ManaCost);
                    if (magic != null)
                    {
                        bool hitAny = false;
                        List<MapObject> Targets = FindAllTargets(1, CurrentLocation, false);
                        if (Targets.Count > 0)
                        {
                            for (int i = 0; i < Targets.Count; i++)
                            {
                                Targets[i].Attacked(this, damage, DefenceType.ACAgility);
                                hitAny = true;
                            }
                            if (hitAny)
                            {
                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.CrossHalfMoon });
                                LevelMagic(magic);
                                ChangeMP(-magic.ManaCost);
                            }
                            else
                            {
                                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                                Target.Attacked(this, damage);

                                var tmp = Magics.FirstOrDefault(x => x.Spell == Spell.Fencing && x.Key > 0);
                                if (tmp != null)
                                {
                                    LevelMagic(tmp);
                                }
                                ActionTime = Envir.Time + 300;
                                AttackTime = Envir.Time + AttackSpeed;
                            }
                            return;
                        }
                    }
                }
                #endregion
                #region Halfmoon
                if (HalfMoonOn)
                {
                    magic = Magics.FirstOrDefault(x => x.Spell == Spell.HalfMoon && x.Key > 0 && MP > x.ManaCost);

                    if (magic != null)
                    {
                        MirDirection dir = Functions.PreviousDir(Direction);
                        bool hitAny = false;
                        for (int i = 0; i < 4; i++)
                        {
                            Point target = Functions.PointMove(CurrentLocation, dir, 1);
                            dir = Functions.NextDir(dir);
                            if (target == Front) continue;
                            if (!CurrentMap.ValidPoint(target)) continue;
                            Cell cell = CurrentMap.GetCell(target);
                            if (cell.Objects == null) continue;
                            for (int o = 0; o < cell.Objects.Count; o++)
                            {
                                MapObject ob = cell.Objects[o];
                                if (ob.Race != ObjectType.Player && ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) continue;
                                if (!ob.IsAttackTarget(this)) continue;

                                if (ob.Attacked(this, damage, DefenceType.ACAgility) > 0)
                                    hitAny = true;
                            }
                        }
                        if (hitAny)
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Spell = Spell.HalfMoon });
                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;
                            LevelMagic(magic);
                            ChangeMP(-magic.ManaCost);
                        }
                        else
                        {
                            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                            Target.Attacked(this, damage);

                            var tmp = Magics.FirstOrDefault(x => x.Spell == Spell.Fencing && x.Key > 0);
                            if (tmp != null)
                            {
                                LevelMagic(tmp);
                            }
                            ActionTime = Envir.Time + 300;
                            AttackTime = Envir.Time + AttackSpeed;
                        }
                        return;
                    }
                }
                #endregion
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
                Target.Attacked(this, damage);

                var tempSpell = Magics.FirstOrDefault(x => x.Spell == Spell.Fencing && x.Key > 0);
                if (tempSpell != null)
                {
                    LevelMagic(tempSpell);
                }
                ActionTime = Envir.Time + 300;
                AttackTime = Envir.Time + AttackSpeed;
            }
        }

        private bool HasSpell(Spell s)
        {
            if (Magics.FirstOrDefault(x => x.Spell == s) != null)
                return true;
            else
                return false;
        }

        private bool HasSpellActive(Spell s)
        {
            if (Magics == null)
                return false;
            if (Magics.FirstOrDefault(x => x.Spell == s && x.Key > 0) != null)
                return true;
            else
                return false;
        }

        public void LevelMagic(UserMagic magic)
        {
            byte exp = (byte)(Envir.Random.Next(3) + 1);

            exp *= SkillNeckBoost;

            if (Level == 65535) exp = byte.MaxValue;

            switch (magic.Level)
            {
                case 0:
                    if (HeroLevel < magic.Info.Level1)
                        return;

                    magic.Experience += exp;
                    if (magic.Experience >= magic.Info.Need1)
                    {
                        magic.Level++;
                        magic.Experience = (ushort)(magic.Experience - magic.Info.Need1);
                        RefreshStats();
                    }
                    break;
                case 1:
                    if (HeroLevel < magic.Info.Level2)
                        return;

                    magic.Experience += exp;
                    if (magic.Experience >= magic.Info.Need2)
                    {
                        magic.Level++;
                        magic.Experience = (ushort)(magic.Experience - magic.Info.Need2);
                        RefreshStats();
                    }
                    break;
                case 2:
                    if (HeroLevel < magic.Info.Level3)
                        return;

                    magic.Experience += exp;
                    if (magic.Experience >= magic.Info.Need3)
                    {
                        magic.Level++;
                        magic.Experience = 0;
                        RefreshStats();
                    }
                    break;
                        
                default:
                    return;
            }

            PlayerMaster.Enqueue(new S.HeroMagicLeveled { Spell = magic.Spell, Level = magic.Level, Experience = magic.Experience });        
        }


        private List<UserMagic> GetAvailableSpells()
        {
            return Magics.Where(x => x.Key >= 1 && !x.Info.Spell.In(Spell.DragonFlames,Spell.ThunderClap,Spell.LastJudgement,Spell.ChopChopStar,Spell.SoulEaterSwamp,Spell.SoulReaper,Spell.BrokenSoulCut,Spell.HandOfGod)).ToList();
        }


        private bool HasRangeSkill()
        {
            if (Magics.FirstOrDefault(x => x.Spell.In(Spell.Plague, Spell.HeadShot, Spell.IceThrust, Spell.LavaKing, Spell.FireBall, Spell.GreatFireBall, Spell.ThunderBolt, Spell.FireBang, Spell.Vampirism, Spell.FrostCrunch,Spell.SoulFireBall,Spell.Poisoning,Spell.TurnUndead,Spell.FireWall,Spell.HellFire,Spell.Lightning) && x.Key > 0) != null)
                return true;
            else
                return false;
        }

        public override void SendHealth(PlayerObject player)
        {
            if (player == null) return;

            if (!player.IsMember(Master) && !(player.IsMember(EXPOwner) && AutoRev) && Envir.Time > RevTime) return;
            byte time = Math.Min(byte.MaxValue, (byte)Math.Max(5, (RevTime - Envir.Time) / 1000));
            player.Enqueue(new S.ObjectHealth { ObjectID = ObjectID, Percent = PercentHealth, Expire = time,MP = MP, ManaPercent = PercentMana });
        }


        protected override void ProcessSearch()
        {
            if (Envir.Time < SearchTime) return;

            SendHealth(PlayerMaster);

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
            if (PlayerMaster != null && Master.HMode == HeroMode.Defensive)
            {
                if (PlayerMaster.Target != null && PlayerMaster.Target.CurrentMap == CurrentMap)
                    Target = Master.Target;
                else
                    Target = null;
            }
            else if (HeroNextTarget != null)
                Target = HeroNextTarget;
            else if (Target == null)
                FindTarget();
        }


        public override void PetRecall()
        {
            if (Master == null || (Race == ObjectType.Hero && PlayerMaster.HMode == HeroMode.Guard)) return;
            if (!Teleport(Master.CurrentMap, Master.Back))
                Teleport(Master.CurrentMap, Master.CurrentLocation);
        }

        protected override void ProcessRoam()
        {
            if (Target != null || Envir.Time < RoamTime) return;

            if (ProcessRoute()) return;

            if (CurrentMap.Inactive(30)) return;

            if (GoToLocation != Point.Empty)
            {
                MoveTo(GoToLocation);
                GoToAttempts++;
            }
            if (Master != null)
            {
                if (guardSpot != null)
                    MoveTo(guardSpot.GuardLocation);
                else
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


        protected override void MoveTo(Point location)
        {
            if (CurrentLocation == GoToLocation || GoToAttempts == HeroObject.GoToMax)
            {
                GoToLocation = Point.Empty;

                if (PlayerMaster != null && PlayerMaster.HMode == HeroMode.Guard)
                {
                    var guardPos = new GuardSpot()
                    {
                        GuardMap = CurrentMap,
                        GuardLocation = CurrentLocation,
                    };

                    guardSpot = guardPos;

                }
            }
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

            bool run = Functions.MaxDistance(CurrentLocation,location) > 2 ? false : true;

            if (Walk(dir, run)) return;

            switch (Envir.Random.Next(2)) //No favour
            {
                case 0:
                    for (int i = 0; i < 7; i++)
                    {
                        dir = Functions.NextDir(dir);

                        if (Walk(dir, run))
                            return;
                    }
                    break;
                default:
                    for (int i = 0; i < 7; i++)
                    {
                        dir = Functions.PreviousDir(dir);

                        if (Walk(dir, run))
                            return;
                    }
                    break;
            }
        }

        private void RefreshMagicStats()
        {
            var spell = Magics.FirstOrDefault(x => x.Spell == Spell.Fencing);
            Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + (spell != null ? spell.Level * 3 : 0));

            spell = Magics.FirstOrDefault(x => x.Spell == Spell.Slaying && x.Key > 0);
            Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + (spell != null ?1 * spell.Level : 0));


            spell = Magics.FirstOrDefault(x => x.Spell == Spell.SpiritSword);
            Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + (spell != null ? 2 * spell.Level : 0));
            MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + (spell != null ? MaxSC * (spell.Level + 1) * 0.1F : 0));
        }


        public void RefreshStats()
        {
            MaxHP = 0; MaxMP = 0;
            MinAC = 0; MaxAC = 0;
            MinMAC = 0; MaxMAC = 0;
            MinDC = 0; MaxDC = 0;
            MinMC = 0; MaxMC = 0;
            MinSC = 0; MaxSC = 0;
            Info.HP = 0;
            MaxBagWeight = 0;
            MaxWearWeight = 0;
            MaxHandWeight = 0;
            ASpeed = 0;
            Luck = 0;
            MagicResist = 0;
            PoisonResist = 0;
            HealthRecovery = 0;
            SpellRecovery = 0;
            PoisonRecovery = 0;
            Holy = 0;
            Freezing = 0;
            PoisonAttack = 0;
            ExpRateOffset = 0;
            SkillNeckBoost = 1;
            ItemSets.Clear();
            MirSet.Clear();
            //Somebody didn't put the clear lists lol
            RefreshLevelStats();
            RefreshBagWeight();
            RefreshEquipmentStats();
            RefreshMagicStats();
            RefreshBuffs();
            RefreshItemSetStats();
            RefreshMirSetStats();
            RefreshGuildBuffs();
            //Location Stats ?

            if (HP > MaxHP) SetHP(MaxHP);
            if (MP > MaxMP) SetMP(MaxMP);

            AttackSpeed = 1400 - ((ASpeed * 30) + Math.Min(370, (HeroLevel * 14)));

            if (AttackSpeed < 500) AttackSpeed = 500;
            if (MoveSpeed < 700) MoveSpeed = 700;

            if (PlayerMaster != null)
                PlayerMaster.Enqueue(GetHeroStats());
        }

        private void RefreshBagWeight()
        {
            CurrentBagWeight = 0;

            for (int i = 0; i < Inventory.Length; i++)
            {
                UserItem item = Inventory[i];
                if (item != null)
                    CurrentBagWeight = (ushort)Math.Min(ushort.MaxValue, CurrentBagWeight + item.Weight);
            }
        }

        public override void ProcessBuffs()
        {
            bool refresh = false;
            for (int i = Buffs.Count - 1; i >= 0; i--)
            {
                Buff buff = Buffs[i];
                if (Envir.Time <= buff.ExpireTime || buff.Infinite || buff.Paused) continue;


                //Enqueue(new S.RemoveBuff { Type = buff.Type, ObjectID = ObjectID });
                PlayerMaster.Enqueue(new S.RemoveBuff { Type = buff.Type, ObjectID = ObjectID });
                if (buff.Visible) Broadcast(new S.RemoveBuff { Type = buff.Type, ObjectID = ObjectID });
                Buffs.RemoveAt(i);
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

            if (refresh) RefreshStats();
        }
        public byte SkillNeckBoost;
        private void RefreshEquipmentStats()
        {
            CurrentWearWeight = 0;
            CurrentHandWeight = 0;
            NoDuraLoss = false;

            var skillsToAdd = new List<string>();
            var skillsToRemove = new List<string> { Settings.HealRing, Settings.FireRing };
            short Macrate = 0, Acrate = 0, HPrate = 0, MPrate = 0;

            for (int i = 0; i < Equipment.Length; i++)
            {
                UserItem temp = Equipment[i];
                if (temp == null) continue;
                ItemInfo RealItem = Functions.GetRealItem(temp.Info,(ushort)HeroLevel, HeroClass, Envir.ItemInfoList);
                if (RealItem.Type == ItemType.Weapon || RealItem.Type == ItemType.Torch)
                    CurrentHandWeight = (ushort)Math.Min(byte.MaxValue, CurrentHandWeight + temp.Weight);
                else
                    CurrentWearWeight = (ushort)Math.Min(byte.MaxValue, CurrentWearWeight + temp.Weight);

                if (temp.CurrentDura == 0 && temp.Info.Durability > 0) continue;


                MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + RealItem.MinAC + temp.Awake.getAC());
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + RealItem.MaxAC + temp.AC + temp.Awake.getAC());
                MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + RealItem.MinMAC + temp.Awake.getMAC());
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + RealItem.MaxMAC + temp.MAC + temp.Awake.getMAC());

                MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + RealItem.MinDC + temp.Awake.DC);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + RealItem.MaxDC + temp.DC + temp.Awake.DC);
                MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + RealItem.MinMC + temp.Awake.getMC());
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + RealItem.MaxMC + temp.MC + temp.Awake.getMC());
                MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + RealItem.MinSC + temp.Awake.getSC());
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + RealItem.MaxSC + temp.SC + temp.Awake.getSC());

                Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + RealItem.Accuracy + temp.Accuracy);
                Agility = (byte)Math.Min(byte.MaxValue, Agility + RealItem.Agility + temp.Agility);

                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + RealItem.HP + temp.HP + temp.Awake.getHPMP());
                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + RealItem.MP + temp.MP + temp.Awake.getHPMP());

                ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + temp.AttackSpeed + RealItem.AttackSpeed)));
                Luck = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, Luck + temp.Luck + RealItem.Luck)));

                MaxBagWeight = (ushort)Math.Max(ushort.MinValue, (Math.Min(ushort.MaxValue, MaxBagWeight + RealItem.BagWeight)));
                MaxWearWeight = (ushort)Math.Max(ushort.MinValue, (Math.Min(byte.MaxValue, MaxWearWeight + RealItem.WearWeight)));
                MaxHandWeight = (ushort)Math.Max(ushort.MinValue, (Math.Min(byte.MaxValue, MaxHandWeight + RealItem.HandWeight)));
                HPrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, HPrate + RealItem.HPrate));
                MPrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, MPrate + RealItem.MPrate));
                Acrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, Acrate + RealItem.MaxAcRate));
                Macrate = (short)Math.Max(short.MinValue, Math.Min(short.MaxValue, Macrate + RealItem.MaxMacRate));
                MagicResist = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, MagicResist + temp.MagicResist + RealItem.MagicResist)));
                PoisonResist = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, PoisonResist + temp.PoisonResist + RealItem.PoisonResist)));
                HealthRecovery = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, HealthRecovery + temp.HealthRecovery + RealItem.HealthRecovery)));
                SpellRecovery = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, SpellRecovery + temp.ManaRecovery + RealItem.SpellRecovery)));
                PoisonRecovery = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, PoisonRecovery + temp.PoisonRecovery + RealItem.PoisonRecovery)));
                CriticalRate = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalRate + temp.CriticalRate + RealItem.CriticalRate)));
                CriticalDamage = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, CriticalDamage + temp.CriticalDamage + RealItem.CriticalDamage)));
                Holy = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, Holy + RealItem.Holy)));
                Freezing = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, Freezing + temp.Freezing + RealItem.Freezing)));
                PoisonAttack = (byte)Math.Max(byte.MinValue, (Math.Min(byte.MaxValue, PoisonAttack + temp.PoisonAttack + RealItem.PoisonAttack)));

                if (RealItem.Light > Light) Light = RealItem.Light;

                if (RealItem.Unique.HasFlag(SpecialItemMode.NoDuraLoss)) NoDuraLoss = true;
                if (RealItem.Unique.HasFlag(SpecialItemMode.Skill)) SkillNeckBoost = 3;


                if (RealItem.Set == ItemSet.None) continue;

                ItemSets itemSet = ItemSets.Where(set => set.Set == RealItem.Set && !set.Type.Contains(RealItem.Type) && !set.SetComplete).FirstOrDefault();

                if (itemSet != null)
                {
                    itemSet.Type.Add(RealItem.Type);
                    itemSet.Count++;
                }
                else
                {
                    ItemSets.Add(new ItemSets { Count = 1, Set = RealItem.Set, Type = new List<ItemType> { RealItem.Type } });
                }

                //Mir Set
                if (RealItem.Set == ItemSet.Mir)
                {
                    if (!MirSet.Contains((EquipmentSlot)i))
                        MirSet.Add((EquipmentSlot)i);
                }
            }
            
            MaxHP = (ushort)Math.Min(ushort.MaxValue, (((double)HPrate / 100) + 1) * MaxHP);
            MaxMP = (ushort)Math.Min(ushort.MaxValue, (((double)MPrate / 100) + 1) * MaxMP);
            MaxAC = (ushort)Math.Min(ushort.MaxValue, (((double)Acrate / 100) + 1) * MaxAC);
            MaxMAC = (ushort)Math.Min(ushort.MaxValue, (((double)Macrate / 100) + 1) * MaxMAC);
         
        }

        private void RefreshItemSetStats()
        {
            foreach (var s in ItemSets)
            {
                if ((s.Set == ItemSet.Purity) && (s.Type.Contains(ItemType.Ring)) && (s.Type.Contains(ItemType.Bracelet)))
                    Holy = Math.Min(byte.MaxValue, (byte)(Holy + 3));
                if ((s.Set == ItemSet.HwanDevil) && (s.Type.Contains(ItemType.Ring)) && (s.Type.Contains(ItemType.Bracelet)))
                {
                    MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 5);
                    MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                }

                if (!s.SetComplete) continue;
                switch (s.Set)
                {
                    case ItemSet.Mundane:
                        MaxHP = Math.Min(uint.MaxValue, MaxHP + 50);
                        break;
                    case ItemSet.NokChi:
                        MaxMP = Math.Min(uint.MaxValue, MaxMP + 50);
                        break;
                    case ItemSet.TaoProtect:
                        MaxHP = Math.Min(uint.MaxValue, MaxHP + 30);
                        MaxMP = Math.Min(uint.MaxValue, MaxMP + 30);
                        break;
                    case ItemSet.RedOrchid:
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 2);
                        break;
                    case ItemSet.RedFlower:
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 50);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP - 25);
                        break;
                    case ItemSet.Smash:
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 1);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 3);
                        ASpeed = (sbyte)Math.Min(sbyte.MaxValue, ASpeed + 2);
                        break;
                    case ItemSet.HwanDevil:
                        MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + 1);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                        MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 5);
                        break;
                    case ItemSet.Purity:
                        MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + 1);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        Holy = (byte)Math.Min(ushort.MaxValue, Holy + 3);
                        break;
                    case ItemSet.FiveString:
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + (((double)MaxHP / 100) * 30));
                        MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + 2);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 2);
                        break;
                    case ItemSet.Spirit:
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 2);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 5);
                        ASpeed = (sbyte)Math.Min(sbyte.MaxValue, ASpeed + 2);
                        break;
                    case ItemSet.Bone:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 2);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                        break;
                    case ItemSet.Bug:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                        PoisonResist = (byte)Math.Min(byte.MaxValue, PoisonResist + 1);
                        break;
                    case ItemSet.WhiteGold:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 2);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 2);
                        break;
                    case ItemSet.WhiteGoldH:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 3);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 30);
                        ASpeed = (sbyte)Math.Min(int.MaxValue, ASpeed + 2);
                        break;
                    case ItemSet.RedJade:
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 2);
                        break;
                    case ItemSet.RedJadeH:
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 40);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + 2);
                        break;
                    case ItemSet.Nephrite:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                        break;
                    case ItemSet.NephriteH:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 15);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 20);
                        Holy = (byte)Math.Min(byte.MaxValue, Holy + 1);
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 1);
                        break;
                    case ItemSet.Whisker1:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 25);
                        break;
                    case ItemSet.Whisker2:
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 17);
                        break;
                    case ItemSet.Whisker3:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 17);
                        break;
                    case ItemSet.Whisker4:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                        break;
                    case ItemSet.Whisker5:
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                        MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 17);
                        break;
                    case ItemSet.Hyeolryong:
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 2);
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 15);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 20);
                        Holy = (byte)Math.Min(byte.MaxValue, Holy + 1);
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 1);
                        break;
                    case ItemSet.Monitor:
                        MagicResist = (byte)Math.Min(byte.MaxValue, MagicResist + 1);
                        PoisonResist = (byte)Math.Min(byte.MaxValue, PoisonResist + 1);
                        break;
                    case ItemSet.Oppressive:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + 1);
                        break;
                    case ItemSet.BlueFrost:
                        MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 1);
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 2);
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 2);
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 50);
                        Accuracy = (byte)Math.Min(byte.MaxValue, Accuracy + 1);
                        Agility = (byte)Math.Min(byte.MaxValue, Agility + 1);
                        break;
                }
            }
        }

        private void RefreshMirSetStats()
        {
            if (MirSet.Count() == 10)
            {
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 70);
                Luck = (sbyte)Math.Min(sbyte.MaxValue, Luck + 2);
                ASpeed = (sbyte)Math.Min(int.MaxValue, ASpeed + 2);
                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + 70);
                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + 80);
                MagicResist = (byte)Math.Min(byte.MaxValue, MagicResist + 6);
                PoisonResist = (byte)Math.Min(byte.MaxValue, PoisonResist + 6);
            }

            if (MirSet.Contains(EquipmentSlot.RingL) && MirSet.Contains(EquipmentSlot.RingR))
            {
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
            }
            if (MirSet.Contains(EquipmentSlot.BraceletL) && MirSet.Contains(EquipmentSlot.BraceletR))
            {
                MinAC = (ushort)Math.Min(ushort.MaxValue, MinAC + 1);
                MinMAC = (ushort)Math.Min(ushort.MaxValue, MinMAC + 1);
            }
            if ((MirSet.Contains(EquipmentSlot.RingL) | MirSet.Contains(EquipmentSlot.RingR)) && (MirSet.Contains(EquipmentSlot.BraceletL) | MirSet.Contains(EquipmentSlot.BraceletR)) && MirSet.Contains(EquipmentSlot.Necklace))
            {
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 30);
                MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 17);
            }
            if (MirSet.Contains(EquipmentSlot.RingL) && MirSet.Contains(EquipmentSlot.RingR) && MirSet.Contains(EquipmentSlot.BraceletL) && MirSet.Contains(EquipmentSlot.BraceletR) && MirSet.Contains(EquipmentSlot.Necklace))
            {
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + 1);
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + 1);
                MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, MaxBagWeight + 20);
                MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, MaxWearWeight + 10);
            }
            if (MirSet.Contains(EquipmentSlot.Armour) && MirSet.Contains(EquipmentSlot.Helmet) && MirSet.Contains(EquipmentSlot.Weapon))
            {
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 2);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                Agility = (byte)Math.Min(byte.MaxValue, Agility + 1);
            }
            if (MirSet.Contains(EquipmentSlot.Armour) && MirSet.Contains(EquipmentSlot.Boots) && MirSet.Contains(EquipmentSlot.Belt))
            {
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                MaxHandWeight = (ushort)Math.Min(ushort.MaxValue, MaxHandWeight + 17);
            }
            if (MirSet.Contains(EquipmentSlot.Armour) && MirSet.Contains(EquipmentSlot.Boots) && MirSet.Contains(EquipmentSlot.Belt) && MirSet.Contains(EquipmentSlot.Helmet) && MirSet.Contains(EquipmentSlot.Weapon))
            {
                MinDC = (ushort)Math.Min(ushort.MaxValue, MinDC + 1);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + 1);
                MinMC = (ushort)Math.Min(ushort.MaxValue, MinMC + 1);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + 1);
                MinSC = (ushort)Math.Min(ushort.MaxValue, MinSC + 1);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + 1);
                MaxHandWeight = (ushort)Math.Min(ushort.MaxValue, MaxHandWeight + 17);
            }
        }

        public void RefreshLevelStats()
        {
            if (HeroLevel > 0)
                MaxExperience = HeroLevel < Envir.heroConfig.HeroExpRequired.Length ? Envir.heroConfig.HeroExpRequired[HeroLevel - 1] : 0;
            
            Accuracy = Settings.ClassBaseStats[(byte)HeroClass].StartAccuracy;
            Agility = Settings.ClassBaseStats[(byte)HeroClass].StartAgility;
            CriticalRate = Settings.ClassBaseStats[(byte)HeroClass].StartCriticalRate;
            CriticalDamage = Settings.ClassBaseStats[(byte)HeroClass].StartCriticalDamage;
            //Other Stats;
            


            Info.HP = MaxHP = (ushort)Math.Min(ushort.MaxValue, 14 + (HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].HpGain + Settings.ClassBaseStats[(byte)HeroClass].HpGainRate) * HeroLevel);

            MinAC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MinAc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MinAc : 0);
            MaxAC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MaxAc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MaxAc : 0);
            MinMAC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MinMac > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MinMac : 0);
            MaxMAC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MaxMac > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MaxMac : 0);
            MinDC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MinDc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MinDc : 0);
            MaxDC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MaxDc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MaxDc : 0);
            MinMC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MinMc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MinMc : 0);
            MaxMC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MaxMc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MaxMc : 0);
            MinSC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MinSc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MinSc : 0);
            MaxSC = (ushort)Math.Min(ushort.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].MaxSc > 0 ? HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].MaxSc : 0);
            CriticalRate = (byte)Math.Min(byte.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].CritialRateGain > 0 ? CriticalRate + (HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].CritialRateGain) : CriticalRate);
            CriticalDamage = (byte)Math.Min(byte.MaxValue, Settings.ClassBaseStats[(byte)HeroClass].CriticalDamageGain > 0 ? CriticalDamage + (HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].CriticalDamageGain) : CriticalDamage);

            MaxBagWeight = (ushort)Math.Min(ushort.MaxValue, (50 + HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].BagWeightGain * HeroLevel));
            MaxWearWeight = (ushort)Math.Min(ushort.MaxValue, (15 + HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].WearWeightGain * HeroLevel));
            MaxHandWeight = (ushort)Math.Min(ushort.MaxValue, (12 + HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].HandWeightGain * HeroLevel));
            switch (HeroClass)
            {
                case MirClass.Warrior:
                    Info.HP = MaxHP = (ushort)Math.Min(ushort.MaxValue, 14 + (HeroLevel / Settings.ClassBaseStats[(byte)HeroClass].HpGain + Settings.ClassBaseStats[(byte)HeroClass].HpGainRate + HeroLevel / 20F) * HeroLevel);
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, 11 + (HeroLevel * 3.5F) + (HeroLevel * Settings.ClassBaseStats[(byte)HeroClass].MpGainRate));
                    break;
                case MirClass.Wizard:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, 13 + ((HeroLevel / 5F + 2F) * 2.2F * HeroLevel) + (HeroLevel * Settings.ClassBaseStats[(byte)HeroClass].MpGainRate));
                    break;
                case MirClass.Taoist:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, (13 + HeroLevel / 8F * 2.2F * HeroLevel) + (HeroLevel * Settings.ClassBaseStats[(byte)HeroClass].MpGainRate));
                    break;
                case MirClass.Assassin:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, (11 + HeroLevel * 5F) + (HeroLevel * Settings.ClassBaseStats[(byte)HeroClass].MpGainRate));
                    break;
                case MirClass.Archer:
                    MaxMP = (ushort)Math.Min(ushort.MaxValue, (11 + HeroLevel * 4F) + (HeroLevel * Settings.ClassBaseStats[(byte)HeroClass].MpGainRate));
                    break;
            }

            OriginalMP = MaxMP;
            OriginalHP = MaxHP;

         
        }

        /// <summary>
        /// Upon spawning the Hero, it'll have been Summoned.
        /// </summary>
        public override void Spawned()
        {
            isSpawn = true;
            base.Spawned();
            Summoned = true;

            if (Master != null && Master.Race == ObjectType.Player)
                PlayerMaster.Enqueue(GetUserInfo());

            RefreshStats();
            for (int i = 0; i < Buffs.Count; i++)
            {
                if ((Buffs[i].Type == BuffType.Exp ||
                    Buffs[i].Type == BuffType.Drop) && Buffs[i].Paused)
                    UnpauseBuff(Buffs[i]);
            }
            UpdateClientSpawn(HeroState.Spawned);

            if (PlayerMaster != null && PlayerMaster.HMode == HeroMode.Guard)
            {
                var guardPos = new GuardSpot()
                {
                    GuardMap = CurrentMap,
                    GuardLocation = CurrentLocation,
                };

                guardSpot = guardPos;

            }

            if (PlayerMaster != null)
            {
                bool foundHero = false;
                for (int i = 0; i < Envir.HeroRankings.Count; i++)
                {
                    if (Envir.HeroRankings[i].Owner == PlayerMaster.Info.Index &&
                        Envir.HeroRankings[i].Name == HeroName)
                        foundHero = true;
                    else
                        continue;
                    if (Envir.HeroRankings[i].Level < HeroLevel)
                        Envir.HeroRankings[i].Level = HeroLevel;
                    if (Envir.HeroRankings[i].Experience < HeroExperience)
                        Envir.HeroRankings[i].Experience = HeroExperience;
                }
                if (!foundHero)
                {
                    HeroRank tmp = new HeroRank()
                    {
                        Owner = PlayerMaster.Info.Index,
                        Name = HeroName,
                        OwnerName = PlayerMaster.Info.Name,
                        Level = HeroLevel,
                        Experience = Experience,
                        HeroesClass = HeroClass,
                        HeroesGender = HeroGender,
                        LastUpdateTime = Envir.Time
                    };
                    Envir.HeroRankings.Add(tmp);
                }
            }
            for (int i = 0; i < Buffs.Count; i++)
            {

            }
        }

        public override void ChangeHP(int amount)
        {
            base.ChangeHP(amount);

            if (Master != null && Master.Race == ObjectType.Player)
                ((PlayerObject)Master).Enqueue(new S.HeroHealthChanged() { HP = (ushort)HP, MP = (ushort)MP });
        }

        public void GainExp(uint amount)
        {
            if (!isSpawn)
                return;
            if (HeroLevel >= Envir.heroConfig.LevelCaps[HeroStage]) return;
            if (amount == 0) return;
            if (Dead) return;
            if (!Functions.InRange(PlayerMaster.CurrentLocation, CurrentLocation, Globals.DataRange) || CurrentMap != PlayerMaster.CurrentMap) return;

            if (ExpRateOffset > 0)
                amount += (uint)(amount * (ExpRateOffset / 100));
            if (Equipment != null)
            {
                if (Equipment[(int)EquipmentSlot.Shield] != null)
                {
                    int tmp = (int)amount / 10;
                    int shieldEXP = (tmp / Settings.ShieldEXPDivision);
                    if (shieldEXP > 0)
                    {
                        PlayerMaster.UpgradeHeroShield(shieldEXP);
                    }
                    PlayerMaster.Enqueue(new S.GainShieldEXP { Amount = (uint)shieldEXP });
                }
            }
            if (isSpawn)
            {
                foreach (var i in Equipment)
                {
                    if (i == null) continue;
                    if (i.Info.LvlableBy == WearType.Player) continue;

                    var exp = ((int)amount / 10);

                    if (exp > 0)
                    {
                        if (i.ItemGainExp(exp))
                        {
                            i.ItemLevelUp((ushort)HeroLevel, HeroClass, HeroGender, Envir.ItemInfoList);
                            ReceiveChat(i.FriendlyName + " has leveled up!", ChatType.System);
                        }

                        RefreshStats();
                        PlayerMaster.Enqueue(new S.RefreshItem { Item = i });
                    }
                }
            }
            HeroExperience += amount;

            PlayerMaster.Enqueue(new S.HeroGainExperience { Amount = amount });
 
            if (HeroExperience < MaxExperience) return;
            if (HeroLevel >= ushort.MaxValue) return;

            //Calculate increased levels
            var experience = HeroExperience;
            while (experience >= MaxExperience)
            {
                HeroLevel++;
                experience -= MaxExperience;

                if (HeroLevel == Envir.heroConfig.BagLock1 || HeroLevel == Envir.heroConfig.BagLock2 || HeroLevel == Envir.heroConfig.BagLock3 || HeroLevel == Envir.heroConfig.BagLock4)
                    ResizeInventory();

                RefreshStats();
                if (HeroLevel >= ushort.MaxValue) break;
                if (HeroLevel >= Envir.heroConfig.LevelCaps[HeroStage]) break; ;
            }

            HeroExperience = experience;
            LevelUp();
        }
        public bool CanUpgradeHeroShield()
        {
            if (Equipment[(int)EquipmentSlot.Shield] != null)
            {
                UserItem item = Equipment[(int)EquipmentSlot.Shield];
                if (item != null)
                {
                    if (item.ShieldLevel >= Settings.MaxShieldLevel)
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }
        public void UpgradeHeroShield(int amount, bool useRandom = false)
        {
            if (CanUpgradeHeroShield())
            {
                UserItem item = Equipment[(int)EquipmentSlot.Shield];
                //  Ensure it's valid
                if (item != null)
                {
                    int random = 0;
                    //  Randomise the EXP
                    if (useRandom)
                        random = Envir.Random.Next(amount);
                    else
                        random = amount;
                    //  Switch between the shields grade
                    switch (item.Info.Grade)
                    {
                        case ItemGrade.Common:
                            //  if EXP is more or equal to the need'd exp
                            if (item.ShieldExp + random >= Settings.CommonShieldEXP[item.ShieldLevel])
                            {
                                item.ShieldExp -= Settings.CommonShieldEXP[item.ShieldLevel];
                                //  if EXP is equal to 0 (ensure we're not going in to minus)
                                if (item.ShieldExp < 0)
                                    item.ShieldExp = 0;
                                //  Increase the Shields level.
                                item.ShieldLevel++;
                                if (item.ShieldLevel < Settings.MaxShieldLevel)
                                    item.NeedShieldExp = Settings.CommonShieldEXP[item.ShieldLevel];
                                else
                                    item.NeedShieldExp = 0;
                                item = PlayerMaster.UpgradeRandomShieldStat(item);
                            }
                            else
                                //  We can't level it, so increase the exp.           
                                item.ShieldExp += random;
                            break;
                        case ItemGrade.Rare:
                            if (item.ShieldExp + random >= Settings.RareShieldEXP[item.ShieldLevel])
                            {
                                item.ShieldExp -= Settings.RareShieldEXP[item.ShieldLevel];
                                if (item.ShieldExp < 0)
                                    item.ShieldExp = 0;
                                //  Increase the Shields level.
                                item.ShieldLevel++;
                                if (item.ShieldLevel < Settings.MaxShieldLevel)
                                    item.NeedShieldExp = Settings.RareShieldEXP[item.ShieldLevel];
                                else
                                    item.NeedShieldExp = 0;
                                item = PlayerMaster.UpgradeRandomShieldStat(item);
                            }
                            //  if EXP is equal to 0 (ensure we're not going in to minus)
                            else
                                //  We can't level it, so increase the exp.           
                                item.ShieldExp += random;
                            break;
                        case ItemGrade.Legendary:
                            if (item.ShieldExp + random >= Settings.LegendaryShieldEXP[item.ShieldLevel])
                            {
                                item.ShieldExp -= Settings.LegendaryShieldEXP[item.ShieldLevel];
                                if (item.ShieldExp < 0)
                                    item.ShieldExp = 0;
                                //  Increase the Shields level.
                                item.ShieldLevel++;
                                if (item.ShieldLevel < Settings.MaxShieldLevel)
                                    item.NeedShieldExp = Settings.LegendaryShieldEXP[item.ShieldLevel];
                                else
                                    item.NeedShieldExp = 0;
                                item = PlayerMaster.UpgradeRandomShieldStat(item);
                            }
                            //  if EXP is equal to 0 (ensure we're not going in to minus)
                            else
                                //  We can't level it, so increase the exp.           
                                item.ShieldExp += random;
                            break;
                        case ItemGrade.Mythical:
                            if (item.ShieldExp + random >= Settings.MythicalShieldEXP[item.ShieldLevel])
                            {
                                item.ShieldExp -= Settings.MythicalShieldEXP[item.ShieldLevel];
                                if (item.ShieldExp < 0)
                                    item.ShieldExp = 0;
                                item.ShieldLevel++;
                                if (item.ShieldLevel < Settings.MaxShieldLevel)
                                    item.NeedShieldExp = Settings.MythicalShieldEXP[item.ShieldLevel];
                                else
                                    item.NeedShieldExp = 0;
                                item = PlayerMaster.UpgradeRandomShieldStat(item);
                            }
                            //  if EXP is equal to 0 (ensure we're not going in to minus)
                            else
                                //  We can't level it, so increase the exp.           
                                item.ShieldExp += random;
                            break;
                        case ItemGrade.Quest:
                            if (item.ShieldExp + random >= Settings.QuestShieldEXP[item.ShieldLevel])
                            {
                                item.ShieldExp -= Settings.QuestShieldEXP[item.ShieldLevel];
                                if (item.ShieldExp < 0)
                                    item.ShieldExp = 0;
                                item.ShieldLevel++;
                                if (item.ShieldLevel < Settings.MaxShieldLevel)
                                    item.NeedShieldExp = Settings.QuestShieldEXP[item.ShieldLevel];
                                else
                                    item.NeedShieldExp = 0;
                                item = PlayerMaster.UpgradeRandomShieldStat(item);
                            }
                            else
                                item.ShieldExp += random;// you missed this :P
                            break;
                    }
                    Equipment[(int)EquipmentSlot.Shield] = item;
                    PlayerMaster.Enqueue(new S.RefreshItem { Item = item });
                }
            }
            else
            {
                return;
            }
            SetShieldEXPS();
        }

        public void SetShieldEXPS()
        {
            #region Shields in Heroes Inventory
            if (Inventory != null)
            {
                for (int i = 0; i < Inventory.Length; i++)
                {
                    if (Inventory[i] == null)
                        continue;
                    if (Inventory[i].Info.Type == ItemType.Shield)
                    {
                        switch (Inventory[i].Info.Grade)
                        {
                            case ItemGrade.Common:
                                if (Inventory[i].ShieldLevel < Settings.MaxShieldLevel)
                                    Inventory[i].NeedShieldExp = Settings.CommonShieldEXP[Inventory[i].ShieldLevel];
                                else
                                    Inventory[i].NeedShieldExp = 0;
                                break;
                            case ItemGrade.Rare:
                                if (Inventory[i].ShieldLevel < Settings.MaxShieldLevel)
                                    Inventory[i].NeedShieldExp = Settings.RareShieldEXP[Inventory[i].ShieldLevel];
                                else
                                    Inventory[i].NeedShieldExp = 0;
                                break;
                            case ItemGrade.Legendary:
                                if (Inventory[i].ShieldLevel < Settings.MaxShieldLevel)
                                    Inventory[i].NeedShieldExp = Settings.LegendaryShieldEXP[Inventory[i].ShieldLevel];
                                else
                                    Inventory[i].NeedShieldExp = 0;
                                break;
                            case ItemGrade.Mythical:
                                if (Inventory[i].ShieldLevel < Settings.MaxShieldLevel)
                                    Inventory[i].NeedShieldExp = Settings.MythicalShieldEXP[Inventory[i].ShieldLevel];
                                else
                                    Inventory[i].NeedShieldExp = 0;
                                break;
                            case ItemGrade.Quest:
                                if (Inventory[i].ShieldLevel < Settings.MaxShieldLevel)
                                    Inventory[i].NeedShieldExp = Settings.QuestShieldEXP[Inventory[i].ShieldLevel];
                                else
                                    Inventory[i].NeedShieldExp = 0;
                                break;
                        }
                    }
                }
            }
            #endregion
            #region Hero Equipped Shield
            if (Equipment != null)
            {
                if (Equipment[(int)EquipmentSlot.Shield] != null)
                {
                    switch (Equipment[(int)EquipmentSlot.Shield].Info.Grade)
                    {
                        case ItemGrade.Common:
                            if (Equipment[(int)EquipmentSlot.Shield].ShieldLevel < Settings.MaxShieldLevel)
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = Settings.CommonShieldEXP[Equipment[(int)EquipmentSlot.Shield].ShieldLevel];
                            else
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = 0;
                            break;
                        case ItemGrade.Rare:
                            if (Equipment[(int)EquipmentSlot.Shield].ShieldLevel < Settings.MaxShieldLevel)
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = Settings.RareShieldEXP[Equipment[(int)EquipmentSlot.Shield].ShieldLevel];
                            else
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = 0;
                            break;
                        case ItemGrade.Legendary:
                            if (Equipment[(int)EquipmentSlot.Shield].ShieldLevel < Settings.MaxShieldLevel)
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = Settings.LegendaryShieldEXP[Equipment[(int)EquipmentSlot.Shield].ShieldLevel];
                            else
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = 0;
                            break;
                        case ItemGrade.Mythical:
                            if (Equipment[(int)EquipmentSlot.Shield].ShieldLevel < Settings.MaxShieldLevel)
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = Settings.MythicalShieldEXP[Equipment[(int)EquipmentSlot.Shield].ShieldLevel];
                            else
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = 0;
                            break;
                        case ItemGrade.Quest:
                            if (Equipment[(int)EquipmentSlot.Shield].ShieldLevel < Settings.MaxShieldLevel)
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = Settings.QuestShieldEXP[Equipment[(int)EquipmentSlot.Shield].ShieldLevel];
                            else
                                Equipment[(int)EquipmentSlot.Shield].NeedShieldExp = 0;
                            break;
                    }
                }
            }
            #endregion
        }

        public void LevelUp()
        {
            if (HeroLevel >= Envir.heroConfig.LevelCaps[HeroStage])
            {
                PlayerMaster.ReceiveChat("Hero stage reached: " + (HeroStage + 1).ToString(),ChatType.System);
                isLocked = true;

                var p = new S.SetHeroLocked()
                {
                    isLocked = isLocked,
                };

                PlayerMaster.Enqueue(p);
            }

            PlayerMaster.Enqueue(new S.HeroStashInfo { Name = HeroName, Class = HeroClass, Active = Active, Gender = HeroGender, Level = (ushort)HeroLevel });
            SetHP(MaxHP);
            SetMP(MaxMP);

            PlayerMaster.Enqueue(new S.HeroLevelChanged { Level = (ushort)HeroLevel, Experience = HeroExperience, MaxExperience = MaxExperience });
            Broadcast(new S.ObjectLeveled { ObjectID = ObjectID });

            if (Master != null && Master.Race == ObjectType.Player)
                PlayerMaster.Enqueue(new S.HeroStatsChanged() { HP = (ushort)HP, MP = (ushort)MP, MaxHP = (ushort)MaxHP, MaxMP = (ushort)MaxMP });
        }

        private void UpdateClientSpawn(HeroState s)
        {
            if (PlayerMaster == null) return;

            var p = new S.SetHeroSpawned()
            {
                setHeroSpawned = s,
            };

            PlayerMaster.Enqueue(p);
        }

        public void Realive(PlayerObject p)
        {
            Dead = false;
            Spawn(Master.CurrentMap, Master.CurrentLocation);
        }

        #region Inventory Stuff
        private static int FreeSpace(IList<UserItem> array)
        {
            int count = 0;

            for (int i = 0; i < array.Count; i++)
                if (array[i] == null)
                    count++;

            return count;
        }

        private void AddItem(UserItem item)
        {
            if (item.Info.StackSize > 1) //Stackable
            {
                for (int i = 0; i < Inventory.Length; i++)
                {
                    UserItem temp = Inventory[i];
                    if (temp == null || item.Info != temp.Info || temp.Count >= temp.Info.StackSize)
                        continue;

                    if (item.Count + temp.Count <= temp.Info.StackSize)
                    {
                        temp.Count += item.Count;
                        return;
                    }
                    item.Count -= temp.Info.StackSize - temp.Count;
                    temp.Count = temp.Info.StackSize;
                }
            }

            if (item.Info.Type == ItemType.Potion || item.Info.Type == ItemType.Scroll || ( item.Info.Type == ItemType.Script && item.Info.Effect == 1 ))
            {
                for (int i = 0; i < 2; i++)
                {
                    if (Inventory[i] != null)
                        continue;
                    Inventory[i] = item;
                    return;
                }
            }
            else
            {
                for (int i = 2; i < Inventory.Length; i++)
                {
                    if (Inventory[i] != null)
                        continue;
                    Inventory[i] = item;
                    return;
                }
            }

            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] != null)
                    continue;
                Inventory[i] = item;
                return;
            }
        }

        
        public void CheckItemInfo(ItemInfo info, bool dontLoop = false)
        {
            if (( dontLoop == false ) && ( info.ClassBased | info.LevelBased )) //send all potential data so client can display it
            {
                for (int i = 0; i < Envir.ItemInfoList.Count; i++)
                {
                    if (( Envir.ItemInfoList[i] != info ) && ( Envir.ItemInfoList[i].Name.StartsWith(info.Name) ))
                        CheckItemInfo(Envir.ItemInfoList[i], true);
                }
            }

            if (PlayerMaster == null) return;

            if (PlayerMaster.Connection.SentItemInfo.Contains(info))
                return;
            PlayerMaster.Enqueue(new S.NewItemInfo { Info = info });
            PlayerMaster.Connection.SentItemInfo.Add(info);
            
        }
        
        public void CheckItem(UserItem item)
        {
            CheckItemInfo(item.Info);

            for (int i = 0; i < item.Slots.Length; i++)
            {
                if (item.Slots[i] == null)
                    continue;

                CheckItemInfo(item.Slots[i].Info);
            }
        }
        

        public void GetItemInfo()
        {
            UserItem item;
            for (int i = 0; i < Inventory.Length; i++)
            {
                item = Inventory[i];
                if (item == null)
                    continue;

                CheckItem(item);
            }

            for (int i = 0; i < Equipment.Length; i++)
            {
                item = Equipment[i];

                if (item == null)
                    continue;

                CheckItem(item);
            }
        }

        private void ConsumeItem(UserItem item, uint cost)
        {
            item.Count -= cost;
            if (Master != null && Master.Race == ObjectType.Player)
            {
                PlayerObject ob = (PlayerObject)Master;
                ob.Enqueue(new S.DeleteItem { UniqueID = item.UniqueID, Count = cost });
            }

            if (item.Count != 0)
                return;

            for (int i = 0; i < Equipment.Length; i++)
            {
                if (Equipment[i] != null && Equipment[i].Slots.Length > 0)
                {
                    for (int j = 0; j < Equipment[i].Slots.Length; j++)
                    {
                        if (Equipment[i].Slots[j] != item)
                            continue;
                        Equipment[i].Slots[j] = null;

                        RefreshStats();
                        return;
                    }
                }

                if (Equipment[i] != item)
                    continue;
                Equipment[i] = null;

                RefreshStats();
                return;
            }

            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i] != item)
                    continue;
                Inventory[i] = null;
                RefreshStats();
                return;
            }
            //Item not found
        }

        private void DamageDura()
        {
            if (!NoDuraLoss)
                for (int i = 0; i < Equipment.Length; i++)
                    DamageItem(Equipment[i], Envir.Random.Next(1) + 1);
        }
        public void DamageWeapon()
        {
            if (!NoDuraLoss)
                if (Equipment[(int)EquipmentSlot.Torch] != null && Equipment[(int)EquipmentSlot.Torch].Info.Shape == 10)
                return;
        }
        private void DamageItem(UserItem item, int amount, bool isChanged = false)
        {
            if (item == null || item.CurrentDura == 0 || item.Info.Type == ItemType.Amulet)
                return;

            if (item.Info.Strong > 0)
                amount = Math.Max(1, amount - item.Info.Strong);
            item.CurrentDura = (ushort)Math.Max(ushort.MinValue, item.CurrentDura - amount);
            item.DuraChanged = true;

            if (item.CurrentDura > 0 && isChanged != true)
                return;
            if (Master != null && Master.Race == ObjectType.Player)
            {
                PlayerObject ob = (PlayerObject)Master;
                ob.Enqueue(new S.DuraChanged { UniqueID = item.UniqueID, CurrentDura = item.CurrentDura });
            }

            item.DuraChanged = false;
            RefreshStats();
        }

        public override void ReceiveChat(string text, ChatType type, List<UserItem> items = null)
        {
            if (PlayerMaster == null) return;

            PlayerMaster.Enqueue(new S.Chat { Message = text, Type = type });

            PlayerMaster.Report.ChatMessage(text);
        }

        public bool CanRemoveItem(MirGridType grid, UserItem item)
        {
            //Item  Stuck

            UserItem[] array;
            switch (grid)
            {
                case MirGridType.HeroInventory:
                    array = PlayerMaster.Info.Inventory;
                    break;
                case MirGridType.Inventory:
                    array = Inventory;
                    break;
                default:
                    return false;
            }

            return FreeSpace(array) > 0;
        }

        public bool CanEquipItem(UserItem item, int slot)
        {
            switch (item.Info.WearType)
            {
                case WearType.Player:
                    ReceiveChat("Item is restricted for Players.", ChatType.System);
                    return false;
            }

            switch ((EquipmentSlot)slot)
            {
                case EquipmentSlot.Weapon:
                    if (item.Info.Type != ItemType.Weapon)
                        return false;
                    break;
                case EquipmentSlot.Armour:
                    if (item.Info.Type != ItemType.Armour)
                        return false;
                    break;
                case EquipmentSlot.Helmet:
                    if (item.Info.Type != ItemType.Helmet)
                        return false;
                    break;
                case EquipmentSlot.Torch:
                    if (item.Info.Type != ItemType.Torch)
                        return false;
                    break;
                case EquipmentSlot.Necklace:
                    if (item.Info.Type != ItemType.Necklace)
                        return false;
                    break;
                case EquipmentSlot.BraceletL:
                    if (item.Info.Type != ItemType.Bracelet)
                        return false;
                    break;
                case EquipmentSlot.BraceletR:
                    if (item.Info.Type != ItemType.Bracelet && item.Info.Type != ItemType.Amulet)
                        return false;
                    break;
                case EquipmentSlot.RingL:
                case EquipmentSlot.RingR:
                    if (item.Info.Type != ItemType.Ring)
                        return false;
                    break;
                case EquipmentSlot.Amulet:
                    if (item.Info.Type != ItemType.Amulet)
                        return false;
                    break;
                case EquipmentSlot.Boots:
                    if (item.Info.Type != ItemType.Boots)
                        return false;
                    break;
                case EquipmentSlot.Belt:
                    if (item.Info.Type != ItemType.Belt)
                        return false;
                    break;
                case EquipmentSlot.Stone:
                    if (item.Info.Type != ItemType.Stone)
                        return false;
                    break;
                case EquipmentSlot.Mount:
                    if (item.Info.Type != ItemType.Mount)
                        return false;
                    break;
                case EquipmentSlot.Poison:
                    if (item.Info.Type != ItemType.Poison)
                        return false;
                    break;
                case EquipmentSlot.Medals:
                    if (item.Info.Type != ItemType.Medals)
                        return false;
                    break;
                case EquipmentSlot.Shield:
                    if (item.Info.Type != ItemType.Shield)
                        return false;
                    break;
                case EquipmentSlot.Pads:
                    if (item.Info.Type != ItemType.Pads)
                        return false;
                    break;
                default:
                    return false;
            }

            switch (HeroGender)
            {
                case MirGender.Male:
                    if (!item.Info.RequiredGender.HasFlag(RequiredGender.Male))
                        return false;
                    break;
                case MirGender.Female:
                    if (!item.Info.RequiredGender.HasFlag(RequiredGender.Female))
                        return false;
                    break;
            }
  
            switch (HeroClass)
            {
                case MirClass.Warrior:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Warrior))
                        return false;
                    break;
                case MirClass.Wizard:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Wizard))
                        return false;
                    break;
                case MirClass.Taoist:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Taoist))
                        return false;
                    break;
                case MirClass.Assassin:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Assassin))
                        return false;
                    break;
            }

            switch (item.Info.RequiredType)
            {
                case RequiredType.Level:
                    if (HeroLevel < item.Info.RequiredAmount)
                        return false;
                    break;
                case RequiredType.AC:
                    if (MaxAC < item.Info.RequiredAmount)
                        return false;
                    break;
                case RequiredType.MAC:
                    if (MaxMAC < item.Info.RequiredAmount)
                        return false;
                    break;
                case RequiredType.DC:
                    if (MaxDC < item.Info.RequiredAmount)
                        return false;
                    break;
                case RequiredType.MC:
                    if (MaxMC < item.Info.RequiredAmount)
                        return false;
                    break;
                case RequiredType.SC:
                    if (MaxSC < item.Info.RequiredAmount)
                        return false;
                    break;
            };

            if (item.Info.Type == ItemType.Weapon || item.Info.Type == ItemType.Torch)
            {
                if (item.Weight - ( Equipment[slot] != null ? Equipment[slot].Weight : 0 ) + CurrentHandWeight > MaxHandWeight)
                    return false;
            }
            else
                if (item.Weight - ( Equipment[slot] != null ? Equipment[slot].Weight : 0 ) + CurrentWearWeight > MaxWearWeight)
                return false;
            return true;
        }

        private bool CanUseItem(UserItem item)
        {
            if (item == null)
                return false;

            switch (item.Info.WearType)
            {
                case WearType.Player:
                    ReceiveChat("Item is restricted for Players.", ChatType.System);
                    return false;
            }

            switch (HeroGender)
            {
                case MirGender.Male:
                    if (!item.Info.RequiredGender.HasFlag(RequiredGender.Male))
                    {
                        ReceiveChat("You are not Female.", ChatType.System);
                        return false;
                    }
                    break;
                case MirGender.Female:
                    if (!item.Info.RequiredGender.HasFlag(RequiredGender.Female))
                    {
                        ReceiveChat("You are not Male.", ChatType.System);
                        return false;
                    }
                    break;
            }

            switch (HeroClass)
            {
                case MirClass.Warrior:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Warrior))
                    {
                        ReceiveChat("Warriors cannot use this item.", ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.Wizard:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Wizard))
                    {
                        ReceiveChat("Wizards cannot use this item.", ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.Taoist:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Taoist))
                    {
                        ReceiveChat("Taoists cannot use this item.", ChatType.System);
                        return false;
                    }
                    break;
                case MirClass.Assassin:
                    if (!item.Info.RequiredClass.HasFlag(RequiredClass.Assassin))
                    {
                        ReceiveChat("Assassins cannot use this item.", ChatType.System);
                        return false;
                    }
                    break;
            }

            switch (item.Info.RequiredType)
            {
                case RequiredType.Level:
                    if (HeroLevel < item.Info.RequiredAmount)
                    {
                        ReceiveChat("You are not a high enough level.", ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.AC:
                    if (MaxAC < item.Info.RequiredAmount)
                    {
                        ReceiveChat("You do not have enough AC.", ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MAC:
                    if (MaxMAC < item.Info.RequiredAmount)
                    {
                        ReceiveChat("You do not have enough MAC.", ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.DC:
                    if (MaxDC < item.Info.RequiredAmount)
                    {
                        ReceiveChat("You do not have enough DC.", ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.MC:
                    if (MaxMC < item.Info.RequiredAmount)
                    {
                        ReceiveChat("You do not have enough MC.", ChatType.System);
                        return false;
                    }
                    break;
                case RequiredType.SC:
                    if (MaxSC < item.Info.RequiredAmount)
                    {
                        ReceiveChat("You do not have enough SC.", ChatType.System);
                        return false;
                    }
                    break;
            }
            switch (item.Info.Type)
            {
                case ItemType.Potion:
                    if (CurrentMap.Info.NoDrug)
                    {
                        ReceiveChat("You cannot use Potions here", ChatType.System);
                        return false;
                    }
                    break;

                case ItemType.Book:
                    if (Magics.Any(t => t.Spell == (Spell)item.Info.Shape))
                    {
                        return false;
                    }
                    break;
                case ItemType.Saddle:
                case ItemType.Ribbon:
                case ItemType.Bells:
                case ItemType.Mask:
                case ItemType.Reins:
                    if (Equipment[(int)EquipmentSlot.Mount] == null)
                    {
                        ReceiveChat("Can only be used with a mount", ChatType.System);
                        return false;
                    }
                    break;
                case ItemType.Hook:
                case ItemType.Float:
                case ItemType.Bait:
                case ItemType.Finder:
                case ItemType.Reel:
                    if (Equipment[(int)EquipmentSlot.Weapon] == null ||
                        ( Equipment[(int)EquipmentSlot.Weapon].Info.Shape != 49 && Equipment[(int)EquipmentSlot.Weapon].Info.Shape != 50 ))
                    {
                        ReceiveChat("Can only be used with a fishing rod", ChatType.System);
                        return false;
                    }
                    break;
                case ItemType.Pets:
                    return false;
            }

            //if (item.Info.Type == ItemType.Book)
            //    for (int i = 0; i < Info.Magics.Count; i++)
            //        if (Info.Magics[i].Spell == (Spell)item.Info.Shape) return false;

            return true;
        }

        public Packet GetUpdateInfo()
        {
            return new S.PlayerUpdate
            {
                ObjectID = ObjectID,
                Weapon = Equipment[(int)EquipmentSlot.Weapon] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Weapon].Info,(ushort)HeroLevel,HeroClass,Envir.ItemInfoList).Shape : (short)-1,
                Armour = Armour = Equipment[(int)EquipmentSlot.Armour] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Armour].Info, (ushort)HeroLevel, HeroClass, Envir.ItemInfoList).Shape : (byte)0,
                Light = Light,
                WingEffect = Equipment[(int)EquipmentSlot.Armour] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Armour].Info, (ushort)HeroLevel, HeroClass, Envir.ItemInfoList).Effect : (byte)0,
                WeaponEffect = Equipment[(int)EquipmentSlot.Weapon] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Weapon].Info, (ushort)HeroLevel, HeroClass, Envir.ItemInfoList).Effect : (byte)0
            };
        }

        public override void AddBuff(Buff b)
        {
            if (Buffs.Any(d => d.Infinite && d.Type == b.Type)) return; //cant overwrite infinite buff with regular buff

            string caster = b.Caster != null ? b.Caster.Name : string.Empty;

            if (b.Values == null) b.Values = new int[1];
            S.AddBuff addBuff = null;
            if (b.Type == BuffType.HeroEnergyShield)
                addBuff = new S.AddBuff { Type = b.Type, Caster = caster, Expire = b.ExpireTime - Envir.Time, Values = b.Values, Infinite = b.Infinite, ObjectID = ObjectID, Visible = b.Visible, Hero = true };
            else
                addBuff = new S.AddBuff { Type = b.Type, Caster = caster, Expire = b.ExpireTime - Envir.Time, Values = b.Values, Infinite = b.Infinite, ObjectID = PlayerMaster.ObjectID, Visible = b.Visible, Hero = true };

            PlayerMaster.Enqueue(addBuff);
            if (b.Visible) Broadcast(addBuff);

            AddHeroBuff(b);
            RefreshStats();
        }

        public void AddHeroBuff(Buff b)
        {
            switch (b.Type)
            {
                case BuffType.MoonLight:
                case BuffType.Hiding:
                case BuffType.DarkBody:
                case BuffType.MoonMist:
                case BuffType.Cloak:

                    if (CurrentMap == null) break;

                    Hidden = true;

                    if (b.Type == BuffType.MoonLight || b.Type == BuffType.DarkBody || b.Type == BuffType.MoonMist) Sneaking = true;

                    for (int y = CurrentLocation.Y - Globals.DataRange; y <= CurrentLocation.Y + Globals.DataRange; y++)
                    {
                        if (y < 0) continue;
                        if (y >= CurrentMap.Height) break;

                        for (int x = CurrentLocation.X - Globals.DataRange; x <= CurrentLocation.X + Globals.DataRange; x++)
                        {
                            if (x < 0) continue;
                            if (x >= CurrentMap.Width) break;
                            if (x < 0 || x >= CurrentMap.Width) continue;

                            Cell cell = CurrentMap.GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject ob = cell.Objects[i];
                                if (ob.Race != ObjectType.Monster && ob.Race != ObjectType.Hero) continue;

                                if (ob.Target == this && (!ob.CoolEye || ob.Level < Level)) ob.Target = null;
                            }
                        }
                    }
                    break;
            }


            for (int i = 0; i < Buffs.Count; i++)
            {
                if (Buffs[i].Type != b.Type) continue;

                Buffs[i] = b;
                Buffs[i].Paused = false;
                return;
            }

            Buffs.Add(b);
        }

        public override bool Walk(MirDirection dir, bool br = false)
        {
            if (!CanMove) return false;

            var temploc = Functions.PointMove(CurrentLocation, dir, 1);

            if (!CurrentMap.ValidPoint(temploc)) return false;

            var cell = CurrentMap.GetCell(temploc);

            if (cell.Objects != null)
                for (int i = 0; i < cell.Objects.Count; i++)
                {
                    MapObject ob = cell.Objects[i];
                    if (!ob.Blocking) continue;
                    return false;
                }



            Point location = Functions.PointMove(CurrentLocation, dir, 2);

            if (!CurrentMap.ValidPoint(location)) return false;

            cell = CurrentMap.GetCell(location);

            bool isBreak = br;

      

            if (cell.Objects != null)
                for (int i = 0; i < cell.Objects.Count; i++)
                {
                    MapObject ob = cell.Objects[i];
                    if (!ob.Blocking) continue;
                    isBreak = true;
                    break;
                }

            if (isBreak)
            {
                location = Functions.PointMove(CurrentLocation, dir, 1);

                if (!CurrentMap.ValidPoint(location)) return false;

                cell = CurrentMap.GetCell(location);

                if (cell.Objects != null)
                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject ob = cell.Objects[i];
                        if (!ob.Blocking) continue;
                        return false;
                    }
            }

            CurrentMap.GetCell(CurrentLocation).Remove(this);

            Direction = dir;
            RemoveObjects(dir, 1);
            CurrentLocation = location;
            CurrentMap.GetCell(CurrentLocation).Add(this);
            AddObjects(dir, 1);

            if (Hidden)
            {
                Hidden = false;

                for (int i = 0; i < Buffs.Count; i++)
                {
                    if (Buffs[i].Type != BuffType.Hiding) continue;

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

            if (isBreak)
                Broadcast(new S.ObjectWalk { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
            else
                Broadcast(new S.ObjectRun { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });


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

        private bool DropItem(UserItem item, int range = 1, bool DeathDrop = false)
        {
            ItemObject ob = new ItemObject(this, item, DeathDrop);

            if (!ob.Drop(range))
                return false;

            if (item.Info.Type == ItemType.Meat)
                item.CurrentDura = (ushort)Math.Max(0, item.CurrentDura - 2000);

            return true;
        }

        public void GainItem(UserItem item)
        {
            //CheckItemInfo(item.Info);
            CheckItem(item);

            UserItem clonedItem = item.Clone();
          
            if (Master != null
                && Master.Race == ObjectType.Player)
            {
                PlayerObject ob = (PlayerObject)Master;
                ob.Enqueue(new S.GainedItem { Item = clonedItem, hero = true, }); //Cloned because we are probably going to change the amount.
            }

            AddItem(item);
        }

        public bool CanGainItems(UserItem[] items)
        {
            int itemCount = items.Count(e => e != null);
            uint itemWeight = 0;
            uint stackOffset = 0;

            if (itemCount < 1)
                return true;

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                    continue;

                itemWeight += items[i].Weight;

                if (items[i].Info.StackSize > 1)
                {
                    uint count = items[i].Count;

                    for (int u = 0; u < Inventory.Length; u++)
                    {
                        UserItem bagItem = Inventory[u];

                        if (bagItem == null || bagItem.Info != items[i].Info)
                            continue;

                        if (bagItem.Count + count > bagItem.Info.StackSize)
                            stackOffset++;

                        break;
                    }
                }
            }

            if (CurrentBagWeight + ( itemWeight ) > MaxBagWeight)
                return false;
            if (FreeSpace(Inventory) < itemCount + stackOffset)
                return false;

            return true;
        }

        public bool CanGainItem(UserItem item, bool useWeight = true)
        {
            if (item.Info.Type == ItemType.Amulet)
            {
                if (FreeSpace(Inventory) > 0 && ( CurrentBagWeight + item.Weight <= MaxBagWeight || !useWeight ))
                    return true;

                uint count = item.Count;

                for (int i = 0; i < Inventory.Length; i++)
                {
                    UserItem bagItem = Inventory[i];

                    if (bagItem == null || bagItem.Info != item.Info)
                        continue;

                    if (bagItem.Count + count <= bagItem.Info.StackSize)
                        return true;

                    count -= bagItem.Info.StackSize - bagItem.Count;
                }

                return false;
            }

            if (useWeight && CurrentBagWeight + ( item.Weight ) > MaxBagWeight)
                return false;

            if (FreeSpace(Inventory) > 0)
                return true;

            if (item.Info.StackSize > 1)
            {
                uint count = item.Count;

                for (int i = 0; i < Inventory.Length; i++)
                {
                    UserItem bagItem = Inventory[i];

                    if (bagItem.Info != item.Info)
                        continue;

                    if (bagItem.Count + count <= bagItem.Info.StackSize)
                        return true;

                    count -= bagItem.Info.StackSize - bagItem.Count;
                }
            }

            return false;
        }

        protected override void ProcessRegen()
        {
            if (Dead) return;
            int healthRegen = 0 , manaRegen = 0;

            if (CanRegen)
            {
                RegenTime = Envir.Time + RegenDelay;


                if (HP < MaxHP)
                    healthRegen += (int)(MaxHP * 0.022F) + 1;


                if (MP < MaxMP)
                    manaRegen += (int)(MaxMP * 0.022F) + 1;
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
            }

            if (Envir.Time > PotTime)
            {
                //PotTime = Envir.Time + Math.Max(50,Math.Min(PotDelay, 600 - (Level * 10)));
                PotTime = Envir.Time + (PlayerMaster != null ? PlayerObject.PotDelay : 200);
                int PerTickRegen = Settings.PerTickRegen + (HeroLevel / 10);

                if (PotHealthAmount > PerTickRegen)
                {
                    healthRegen += (PerTickRegen * (PotHPBoost + 100)) / 100;
                    PotHealthAmount -= (ushort)PerTickRegen;
                }
                else
                {
                    healthRegen += PotHealthAmount;
                    PotHealthAmount = 0;
                }

                if (PotManaAmount > PerTickRegen)
                {
                    manaRegen += (PerTickRegen * (PotMPBoost + 100)) / 100;
                    PotManaAmount -= (ushort)PerTickRegen;
                }
                else
                {
                    manaRegen += PotManaAmount;
                    PotManaAmount = 0;
                }
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
            }

            if (healthRegen > 0)
            {
                BroadcastDamageIndicator(DamageType.Hp, healthRegen);
                ChangeHP(healthRegen);
            }
            if (HP == MaxHP) HealAmount = 0;

            if (manaRegen > 0)
            {
                BroadcastDamageIndicator(DamageType.Mp, manaRegen);
                ChangeMP(manaRegen);
            }


        }

        protected override void RefreshBuffs()
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
                        MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + buff.Values[0]);
                        break;
                    case BuffType.BlessedArmour:
                        MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + buff.Values[0]);
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

                    case BuffType.Impact:
                        if (buff.Values.Length < 1) return;
                        MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + buff.Values[0]);
                        break;
                    case BuffType.Magic:
                        if (buff.Values.Length < 1) return;
                        MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + buff.Values[0]);
                        break;
                    case BuffType.Taoist:
                        if (buff.Values.Length < 1) return;
                        MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + buff.Values[0]);
                        break;
                    case BuffType.Storm:
                        if (buff.Values.Length < 1) return;
                        ASpeed = (sbyte)Math.Max(sbyte.MinValue, (Math.Min(sbyte.MaxValue, ASpeed + buff.Values[0])));
                        break;
                    case BuffType.Accuracy:
                        if (buff.Values.Length < 1) return;
                        Accuracy = (byte)Math.Max(sbyte.MinValue, (Math.Min(byte.MaxValue, Accuracy + buff.Values[0])));
                        break;
                    case BuffType.Agility:
                        if (buff.Values.Length < 1) return;
                        Agility = (byte)Math.Max(sbyte.MinValue, (Math.Min(byte.MaxValue, Agility + buff.Values[0])));
                        break;
                    case BuffType.HealthAid:
                        if (buff.Values.Length < 1) return;
                        MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + buff.Values[0]);
                        break;
                    case BuffType.ManaAid:
                        if (buff.Values.Length < 1) return;
                        MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + buff.Values[0]);
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
                }

            }
        }
        public void RefreshGuildBuffs()
        {
            if (PlayerMaster == null) return;
            if (PlayerMaster.MyGuild == null)
                return;
            if (PlayerMaster.MyGuild.BuffList.Count == 0) return;
            for (int i = 0; i < PlayerMaster.MyGuild.BuffList.Count; i++)
            {
                GuildBuff Buff = PlayerMaster.MyGuild.BuffList[i];
                if ((Buff.Info == null) || (!Buff.Active)) continue;
                MaxAC = (ushort)Math.Min(ushort.MaxValue, MaxAC + Buff.Info.BuffAc);
                MaxMAC = (ushort)Math.Min(ushort.MaxValue, MaxMAC + Buff.Info.BuffMac);
                MaxDC = (ushort)Math.Min(ushort.MaxValue, MaxDC + Buff.Info.BuffDc);
                MaxMC = (ushort)Math.Min(ushort.MaxValue, MaxMC + Buff.Info.BuffMc);
                MaxSC = (ushort)Math.Min(ushort.MaxValue, MaxSC + Buff.Info.BuffSc);
                //AttackBonus = (byte)Math.Min(byte.MaxValue, AttackBonus + Buff.Info.BuffAttack);
                MaxHP = (ushort)Math.Min(ushort.MaxValue, MaxHP + Buff.Info.BuffMaxHp);
                MaxMP = (ushort)Math.Min(ushort.MaxValue, MaxMP + Buff.Info.BuffMaxMp);
                //MineRate = (byte)Math.Min(byte.MaxValue, MineRate + Buff.Info.BuffMineRate);
                //GemRate = (byte)Math.Min(byte.MaxValue, GemRate + Buff.Info.BuffGemRate);
                //FishRate = (byte)Math.Min(byte.MaxValue, FishRate + Buff.Info.BuffFishRate);
                ExpRateOffset = (float)Math.Min(float.MaxValue, ExpRateOffset + Buff.Info.BuffExpRate);
                //CraftRate = (byte)Math.Min(byte.MaxValue, CraftRate + Buff.Info.BuffCraftRate); //needs coding
                SkillNeckBoost = (byte)Math.Min(byte.MaxValue, SkillNeckBoost + Buff.Info.BuffSkillRate);
                HealthRecovery = (byte)Math.Min(byte.MaxValue, HealthRecovery + Buff.Info.BuffHpRegen);
                SpellRecovery = (byte)Math.Min(byte.MaxValue, SpellRecovery + Buff.Info.BuffMPRegen);
                ItemDropRateOffset = (float)Math.Min(float.MaxValue, ItemDropRateOffset + Buff.Info.BuffDropRate);
                GoldDropRateOffset = (float)Math.Min(float.MaxValue, GoldDropRateOffset + Buff.Info.BuffGoldRate);
            }
        }
        public void UseItem(ulong id)
        {
            S.HeroUseItem p = new S.HeroUseItem { UniqueID = id, Success = false };

            UserItem item = null;
            int index = -1;

            for (int i = 0; i < Inventory.Length; i++)
            {
                item = Inventory[i];
                if (item == null || item.UniqueID != id)
                    continue;
                index = i;
                break;
            }
            
            if (item == null || index == -1 || !CanUseItem(item))
            {
                if (Master != null &&
                                                Master.Race == ObjectType.Player)
                {
                    PlayerObject ob = (PlayerObject)Master;
                    ob.Enqueue(p);
                    return;
                }
                return;
            }

            if (Dead && !( item.Info.Type == ItemType.Scroll && item.Info.Shape == 6 ))
            {
                if (Master != null &&
                                                Master.Race == ObjectType.Player)
                {
                    PlayerObject ob = (PlayerObject)Master;
                    ob.Enqueue(p);
                    return;
                }
                return;
            }

            switch (item.Info.Type)
            {
                case ItemType.Potion:
                    switch (item.Info.Shape)
                    {
                        case 0: //NormalPotion
                            PotHealthAmount = (ushort)Math.Min(ushort.MaxValue, PotHealthAmount + item.Info.HP);
                            PotManaAmount = (ushort)Math.Min(ushort.MaxValue, PotManaAmount + item.Info.MP);

                            PotHPBoost = item.Info.HPrate;
                            PotMPBoost = item.Info.MPrate;
                            break;
                        case 1: //SunPotion
                            ChangeHP(item.Info.HP);
                            ChangeMP(item.Info.MP);
                            break;
                        case 2: //MysteryWater
                            if (Master != null &&
                                                            Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                ob.Enqueue(p);
                                return;
                            }
                            return;
                        case 3: //Buff
                            int time = item.Info.Durability;

                            if (( item.Info.MaxDC + item.DC ) > 0)
                                AddBuff(new Buff { Type = BuffType.Impact, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.MaxDC + item.DC } });

                            if (( item.Info.MaxMC + item.MC ) > 0)
                                AddBuff(new Buff { Type = BuffType.Magic, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.MaxMC + item.MC } });

                            if (( item.Info.MaxSC + item.SC ) > 0)
                                AddBuff(new Buff { Type = BuffType.Taoist, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.MaxSC + item.SC } });

                            if (( item.Info.AttackSpeed + item.AttackSpeed ) > 0)
                                AddBuff(new Buff { Type = BuffType.Storm, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.AttackSpeed + item.AttackSpeed } });

                            if (( item.Info.HP + item.HP ) > 0)
                                AddBuff(new Buff { Type = BuffType.HealthAid, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.HP + item.HP } });

                            if (( item.Info.MP + item.MP ) > 0)
                                AddBuff(new Buff { Type = BuffType.ManaAid, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.MP + item.MP } });

                            if (( item.Info.MaxAC + item.AC ) > 0)
                                AddBuff(new Buff { Type = BuffType.Defence, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.MaxAC + item.AC } });

                            if (( item.Info.MaxMAC + item.MAC ) > 0)
                                AddBuff(new Buff { Type = BuffType.MagicDefence, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.MaxMAC + item.MAC } });


                            if (item.Info.HPrate > 0)
                                AddBuff(new Buff { Type = BuffType.HealthAid, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { (int)(item.Info.HPrate * OriginalHP) / 100 } });

                            if (item.Info.MPrate > 0)
                                AddBuff(new Buff { Type = BuffType.ManaAid, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { (int)(item.Info.MPrate * OriginalMP) / 100 } });

                            break;
                        case 4: //Exp
                            time = item.Info.Durability;

                            AddBuff(new Buff { Type = BuffType.Exp, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.Luck + item.Luck } });
                            break;
                        case 5://Drop
                            {
                                Buff tmp = Buffs.Where(o => o.Type == BuffType.Drop).FirstOrDefault();
                                if (tmp == null)
                                {
                                    time = item.Info.Durability;

                                    AddBuff(new Buff { Type = BuffType.Drop, Caster = this, ExpireTime = Envir.Time + time * Settings.Minute, Values = new int[] { item.Info.Luck + item.Luck } });
                                }
                                else
                                {
                                    PlayerMaster.Enqueue(p);
                                    return;
                                }
                            }
                            break;

                        case 6: //Hero % pot //New Ice

                            {

                                if (CanUsePercentPot)

                                {

                                    if (item.Info.HP > 0)

                                    {

                                        PotHealthAmount = (ushort)Math.Min(ushort.MaxValue, PotHealthAmount + (int)((MaxMP * item.Info.HP) / 100));

                                    }

                                    if (item.Info.MP > 0)

                                    {
                                        PotManaAmount = (ushort)Math.Min(ushort.MaxValue, PotManaAmount + (int)((MaxMP * item.Info.MP) / 100));

                                    }

                                    PercentCoolTime = Envir.Time + Settings.Second * Settings.PercentPotionDelay;

                                }

                                else

                                {

                                    ReceiveChat(string.Format("{0} seconds until you can use again.", (PercentCoolTime - Envir.Time) / 1000), ChatType.System);

                                    PlayerObject ob = (PlayerObject)Master;
                                    ob.Enqueue(p);

                                    return;

                                }

                            }

                            break;
                    }
                    break;
                case ItemType.Scroll:
                    UserItem temp;
                    switch (item.Info.Shape)
                    {
                        case 0: //DE
                            if (Master != null &&
                                                            Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                ob.Enqueue(p);
                                return;
                            }
                            return;
                        case 1: //TT
                            if (Master != null &&
                                                            Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                ob.Enqueue(p);
                                return;
                            }
                            return;
                        case 2: //RT
                            if (Master != null &&
                                Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                ob.Enqueue(p);
                                return;
                            }
                            return;
                        case 3: //BenedictionOil
                            if (Master != null &&
                                Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                ob.Enqueue(p);

                                return;
                            }
                                return;
                        case 4: //RepairOil
                            if (Master != null &&
                                Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                temp = Equipment[(int)EquipmentSlot.Weapon];
                                if (temp == null || temp.MaxDura == temp.CurrentDura)
                                {
                                    ob.Enqueue(p);
                                    return;
                                }
                                if (temp.Info.Bind.HasFlag(BindMode.DontRepair))
                                {
                                    ob.Enqueue(p);
                                    return;
                                }
                                temp.MaxDura = (ushort)Math.Max(0, temp.MaxDura - Math.Min(5000, temp.MaxDura - temp.CurrentDura) / 30);

                                temp.CurrentDura = (ushort)Math.Min(temp.MaxDura, temp.CurrentDura + 5000);
                                temp.DuraChanged = false;

                                ReceiveChat("Your weapon has been partially repaired", ChatType.Hint);
                                ob.Enqueue(new S.ItemRepaired { UniqueID = temp.UniqueID, MaxDura = temp.MaxDura, CurrentDura = temp.CurrentDura });
                            }

                            break;
                        case 5: //WarGodOil
                            if (Master != null &&
                                Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                temp = Equipment[(int)EquipmentSlot.Weapon];
                                if (temp == null || temp.MaxDura == temp.CurrentDura)
                                {
                                    ob.Enqueue(p);
                                    return;
                                }
                                if (temp.Info.Bind.HasFlag(BindMode.DontRepair) || ( temp.Info.Bind.HasFlag(BindMode.NoSRepair) ))
                                {
                                    ob.Enqueue(p);
                                    return;
                                }
                                temp.CurrentDura = temp.MaxDura;
                                temp.DuraChanged = false;

                                ReceiveChat("Your weapon has been completely repaired", ChatType.Hint);
                                ob.Enqueue(new S.ItemRepaired { UniqueID = temp.UniqueID, MaxDura = temp.MaxDura, CurrentDura = temp.CurrentDura });
                                break;
                            }
                            else
                                break;
                        case 20: //ReleaseScroll
                            if (Master != null &&
                                Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                if (HeroLevel < Envir.heroConfig.LevelCaps[HeroStage])
                                {
                                    ReceiveChat("You haven't reached stage level yet!", ChatType.Hint);
                                    ob.Enqueue(p);
                                    return;
                                }
                                if (item.Info.RequiredType == RequiredType.Level && item.Info.RequiredAmount != HeroStage)
                                {
                                    ReceiveChat("Wrong Stage Release scroll!", ChatType.Hint);
                                    ob.Enqueue(p);
                                    return;
                                }
                                if (HeroStage == Envir.heroConfig.LevelCaps.Length)
                                {
                                    ReceiveChat("Maximum Hero stage reached!!", ChatType.Hint);
                                    ob.Enqueue(p);
                                    return;
                                }

                                if (Envir.Random.Next(100) < item.Info.Durability)
                                {
                                    HeroStage++;
                                    isLocked = false;

                                    var s = new S.SetHeroLocked()
                                    {
                                        isLocked = isLocked,
                                    };

                                    PlayerMaster.Enqueue(s);


                                    ReceiveChat("Release worked!, you reached stage :" + (HeroStage + 1).ToString(), ChatType.Hint);
                                }
                                else if (item.Info.MaxAC > 0 && Envir.Random.Next(100) < item.Info.MinAC)
                                {


                                    HeroLevel -= item.Info.MaxAC;
                                    LevelUp();


                                    isLocked = false;
                                   var s = new S.SetHeroLocked()
                                    {
                                        isLocked = isLocked,
                                    };
                                    PlayerMaster.Enqueue(s);
                                }
                                else
                                {
                                    ReceiveChat("Release failed!, better luck next time", ChatType.Hint);
                                }

                                break;
                            }
                            else
                                break;

                        case 21: //AutoPot
                            if (Master != null &&
                                Master.Race == ObjectType.Player)
                            {

                                if (autoPotSystem.isEnabled)
                                {
                                    ReceiveChat("Autopot is already enabled!", ChatType.Hint);
                                    PlayerMaster.Enqueue(p);
                                    return;
                                }

                                autoPotSystem.AllowAutoPot();
                                PlayerMaster.Enqueue(new S.SetAutoPot { AutoPot = PlayerMaster.Hero.autoPotSystem.isEnabled });
                                break;
                            }
                            break;
                           case 27: //SupeerBenedictionOil
                            if (Master != null &&
                                Master.Race == ObjectType.Player)
                            {
                                PlayerObject ob = (PlayerObject)Master;
                                ob.Enqueue(p);

                                return;
                            }
                            break;
                    }
                    break;
                case ItemType.Book:
                    UserMagic magic = new UserMagic((Spell)item.Info.Shape);
                    if (magic.Info == null)
                    {
                        if (Master != null && Master.Race == ObjectType.Player)
                        {
                            PlayerObject ob = (PlayerObject)Master;
                            ob.Enqueue(p);
                        }
                        return;
                    }

                    Magics.Add(magic);
                    if (Master != null && Master.Race == ObjectType.Player)
                    {
                        PlayerObject ob = (PlayerObject)Master;
                        ob.Enqueue(magic.GetHeroInfo());
                    }
                    break;
                case ItemType.Transform: //Transforms
                    int tTime = item.Info.Durability;
                    int tType = item.Info.Shape;

                    AddBuff(new Buff { Type = BuffType.Transform, Caster = this, ExpireTime = Envir.Time + tTime * 1000, Values = new int[] { tType } });
                    break;
                default:
                    return;
            }
            int idx = -1;
            if (item.Count > 1)
                item.Count--;
            else
            {
                if (item.Info.Type == ItemType.Potion)
                {

                    for (int i = 0; i < Inventory.Length; i++)
                    {
                        if (Inventory[i] != null)
                        {
                            if (idx != -1)
                                continue;
                            if (Inventory[i].Info == item.Info &&
                                item.UniqueID != Inventory[i].UniqueID)
                                idx = i;
                        }
                    }
                    Inventory[index] = null;
                }
                else
                    Inventory[index] = null;
            }
            if (Master != null &&
                                Master.Race == ObjectType.Player)
            {
                PlayerObject ob = (PlayerObject)Master;
                ob.Report.ItemChanged("HERO UseItem", item, 1, 1);

                p.Success = true;
                ob.Enqueue(p);
                if (idx != -1)
                    MoveItem(MirGridType.HeroInventory, idx, index);
                return;
            }
            return;
        }

        public override bool IsFriendlyTarget(PlayerObject ally)
        {
            if (PlayerMaster == null) return false;
            if (ally == PlayerMaster) return true;
            if (ally.Info == null) return false;

            switch (ally.AMode)
            {
                case AttackMode.Group:
                    return GroupMembers != null && GroupMembers.Contains(ally);
                case AttackMode.RedBrown:
                    return PlayerMaster.PKPoints < 200 & Envir.Time > PlayerMaster.BrownTime;
                case AttackMode.Guild:
                    return false;
                case AttackMode.EnemyGuild:
                    return true;
            }
            return true;
        }

        public void EquipItem(MirGridType grid, ulong id, int to)
        {
            S.EquipItem p = new S.EquipItem { Grid = grid, UniqueID = id, To = to, Success = false };
            PlayerObject ob = null;
            if (Master != null &&
                Master.Race == ObjectType.Player)
                ob = (PlayerObject)Master;
            if (to < 0 || to >= Equipment.Length)
            {
                if (ob != null)
                    ob.Enqueue(p);
                return;
            }

            UserItem[] array;
            switch (grid)
            {
                case MirGridType.HeroInventory:
                    array = Inventory;
                    break;
                default:
                    if (ob != null)
                        ob.Enqueue(p);
                    return;
            }


            int index = -1;
            UserItem temp = null;

            for (int i = 0; i < array.Length; i++)
            {
                temp = array[i];
                if (temp == null || temp.UniqueID != id)
                    continue;
                index = i;
                break;
            }

            if (temp == null || index == -1)
            {
                if (ob != null)
                    ob.Enqueue(p);
                return;
            }

            if (( temp.SoulBoundId != -1 ) && ( temp.SoulBoundId != Info.Index ))
            {
                if (ob != null)
                    ob.Enqueue(p);
                return;
            }

            if (Equipment[to] != null)
                if (Equipment[to].WeddingRing != -1)
                {
                    if (ob != null)
                        ob.Enqueue(p);
                    return;
                }


            if (CanEquipItem(temp, to))
            {
                if (temp.Info.NeedIdentify && !temp.Identified)
                {
                    temp.Identified = true;
                    if (ob != null)
                    ob.Enqueue(new S.RefreshItem { Item = temp });
                }
                if (( temp.Info.Bind.HasFlag(BindMode.BindOnEquip) ) && ( temp.SoulBoundId == -1 ))
                {
                    temp.SoulBoundId = Info.Index;
                    if (ob != null)
                        ob.Enqueue(new S.RefreshItem { Item = temp });
                }


                array[index] = Equipment[to];

                if (ob != null)
                    ob.Report.ItemMoved("HERO RemoveItem", temp, MirGridType.HeroEquipment, grid, to, index);

                Equipment[to] = temp;

                if (ob != null)
                    ob.Report.ItemMoved("HERO EquipItem", temp, grid, MirGridType.HeroEquipment, index, to);

                p.Success = true;
                if (ob != null)
                    ob.Enqueue(p);
                RefreshStats();

                //Broadcast(GetUpdateInfo());
                return;
            }
            if (ob != null)
                ob.Enqueue(p);
        }

        public void MoveItem(MirGridType grid, int from, int to)
        {
            S.MoveItem p = new S.MoveItem { Grid = grid, From = from, To = to, Success = false };
            UserItem[] array;
            if (PlayerMaster != null)
            {
                switch (grid)
                {
                    case MirGridType.HeroInventory:
                        array = Inventory;
                        break;
                    default:
                        if (PlayerMaster != null)
                            PlayerMaster.Enqueue(p);
                        return;
                }

                if (from >= 0 && 
                    to >= 0 && 
                    from < array.Length && 
                    to < array.Length)
                {
                    if (array[from] == null)
                    {
                        if (PlayerMaster != null)
                            PlayerMaster.Report.ItemError("MoveItem", grid, grid, from, to);
                        if (PlayerMaster != null)
                            PlayerMaster.ReceiveChat("Item Move Error - Please report the item you tried to move and the time", ChatType.System);
                        if (PlayerMaster != null)
                            PlayerMaster.Enqueue(p);
                        return;
                    }

                    UserItem i = array[to];
                    array[to] = array[from];

                    if (PlayerMaster != null)
                        PlayerMaster.Report.ItemMoved("MoveItem", array[to], grid, grid, from, to);

                    array[from] = i;

                    if (PlayerMaster != null)
                        PlayerMaster.Report.ItemMoved("MoveItem", array[from], grid, grid, to, from);

                    p.Success = true;
                    if (PlayerMaster != null)
                        PlayerMaster.Enqueue(p);
                    return;
                }

                if (PlayerMaster != null)
                    PlayerMaster.Enqueue(p);
            }
        }

        public override bool Spawn(Map temp, Point location)
        {

            if (!temp.ValidPoint(location)) return false;
            if (isSpawn) return false;
            if (Dead) return false;

            AutoSpawn = true;
            CurrentMap = temp;
            CurrentLocation = location;

            CurrentMap.AddObject(this);

            Spawned();
            Envir.MonsterCount++;
            CurrentMap.MonsterCount++;

            return true;
        }

        public void SetMP(uint amount)
        {
            if (MP == amount) return;

            MP = amount <= MaxMP ? amount : MaxMP;

            if (!Dead && HP == 0) Die();

        }


        #endregion
        public virtual void ChangeMP(int amount)
        {

            uint value = (uint)Math.Max(uint.MinValue, Math.Min(MaxMP, MP + amount));

            if (value == MP)
                return;

            MP = value;

            if (Master != null && Master.Race == ObjectType.Player)
                ((PlayerObject)Master).Enqueue(new S.HeroHealthChanged() { HP = (ushort)HP, MP = (ushort)MP });

        }

        /// <summary>
        /// Override the Die method to desummon the hero
        /// </summary>
        public override void Die()
        {
            if (Dead)
                return;

            HP = 0;
            Dead = true;
            //DeadTime = Envir.Time + DeadDelay;
            DeadTime = 0;

            Broadcast(new S.ObjectDied { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });
            /*
            if (EXPOwner != null && Master == null && EXPOwner.Race == ObjectType.Player)
                EXPOwner.WinExp(Experience);
            */
            if (Respawn != null)
                Respawn.Count--;

            PoisonList.Clear();
            Envir.MonsterCount--;
            CurrentMap.MonsterCount--;

            DeathDrop(LastHitter);

            UpdateClientSpawn(HeroState.Dead);

        }

        private void DeathDrop(MapObject killer)
        {
            bool pkbodydrop = true;

            if (Envir.heroConfig.allowDeathDrop)
            {
                if ((killer == null) || ((pkbodydrop) || (killer.Race != ObjectType.Player)))
                {
                    UserItem temp;

                    for (int i = 0; i < Equipment.Length; i++)
                    {
                        temp = Equipment[i];

                        if (temp == null) continue;
                        if (temp.Info.Bind.HasFlag(BindMode.DontDeathdrop)) continue;
                        if ((temp.WeddingRing != -1) && (Equipment[(int)EquipmentSlot.RingL].UniqueID == temp.UniqueID)) continue; //CHECK THIS

                        if ((temp != null) && ((killer == null) || ((killer != null) && (killer.Race != ObjectType.Player))))
                        {
                            SMain.Enqueue(temp.Name);
                            if (temp.Info.Bind.HasFlag(BindMode.BreakOnDeath))
                            {
                                Equipment[i] = null;
                                PlayerMaster.Enqueue(new S.DeleteItem { UniqueID = temp.UniqueID, Count = temp.Count });
                                ReceiveChat(string.Format("Hero {0} shattered upon death.", temp.FriendlyName), ChatType.System2);
                                PlayerMaster.Report.ItemChanged("HeroDeathDrop", temp, temp.Count, 1);
                            }
                        }
                        if (ItemSets.Any(set => set.Set == ItemSet.Spirit && !set.SetComplete))
                        {
                            if (temp.Info.Set == ItemSet.Spirit)
                            {
                                Equipment[i] = null;
                                PlayerMaster.Enqueue(new S.DeleteItem { UniqueID = temp.UniqueID, Count = temp.Count });

                                PlayerMaster.Report.ItemChanged("HeroDeathDrop", temp, temp.Count, 1);
                            }
                        }

                        if (temp.Count > 1)
                        {
                            int percent = Envir.RandomomRange(10, 8);

                            uint count = (uint)Math.Ceiling(temp.Count / 10F * percent);

                            if (count > temp.Count)
                                throw new ArgumentOutOfRangeException();

                            UserItem temp2 = Envir.CreateFreshItem(temp.Info);
                            temp2.Count = count;

                            if (DropItem(temp2, Settings.DropRange, true))
                            {
                                if (count == temp.Count)
                                    Equipment[i] = null;

                                PlayerMaster.Enqueue(new S.DeleteItem { UniqueID = temp.UniqueID, Count = count });
                                temp.Count -= count;

                                PlayerMaster.Report.ItemChanged("HeroDeathDrop", temp, count, 1);
                            }
                        }
                        else if (Envir.Random.Next(30) == 0)
                        {
                            if (DropItem(temp, Settings.DropRange, true))
                            {
                                Equipment[i] = null;
                                PlayerMaster.Enqueue(new S.DeleteItem { UniqueID = temp.UniqueID, Count = temp.Count });

                                PlayerMaster.Report.ItemChanged("HeroDeathDrop", temp, temp.Count, 1);
                            }
                        }
                    }

                }
            }

            if (Envir.heroConfig.allowInvetoryDeathDrop)
            {
                for (int i = 0; i < Inventory.Length; i++)
                {
                    UserItem temp = Inventory[i];

                    if (temp == null) continue;
                    if (temp.Info.Bind.HasFlag(BindMode.DontDeathdrop)) continue;
                    if (temp.WeddingRing != -1) continue;

                    if (temp.Count > 1)
                    {
                        int percent = Envir.RandomomRange(10, 8);
                        if (percent == 0) continue;

                        uint count = (uint)Math.Ceiling(temp.Count / 10F * percent);

                        if (count > temp.Count)
                            throw new ArgumentOutOfRangeException();

                        UserItem temp2 = Envir.CreateFreshItem(temp.Info);
                        temp2.Count = count;

                        if (DropItem(temp2, Settings.DropRange, true))
                        {
                            if (count == temp.Count)
                                Inventory[i] = null;

                            PlayerMaster.Enqueue(new S.DeleteItem { UniqueID = temp.UniqueID, Count = count });
                            temp.Count -= count;

                            PlayerMaster.Report.ItemChanged("HeroDeathDrop", temp, count, 1);
                        }
                    }
                    else if (Envir.Random.Next(10) == 0)
                    {
                        if (DropItem(temp, Settings.DropRange, true))
                        {
                            Inventory[i] = null;
                            PlayerMaster.Enqueue(new S.DeleteItem { UniqueID = temp.UniqueID, Count = temp.Count });

                            PlayerMaster.Report.ItemChanged("HeroDeathDrop", temp, temp.Count, 1);
                        }
                    }
                }
            }

            RefreshStats();
        }

        /// <summary>
        /// Saving the Hero to it's own contained .info file.
        /// </summary>
        /// <returns></returns>
        public bool SaveHero(int Hcount)
        {
            try
            {
                using (FileStream stream = new FileStream(@"./Heroes/" + Master.Name + (Hcount == 0 ? "" : Hcount.ToString()) + ".data", FileMode.OpenOrCreate))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(HeroName);
                        writer.Write((byte)HeroClass);
                        writer.Write((byte)HeroGender);
                        writer.Write(HeroHair);
                        writer.Write(Summoned);
                        //Save each Equipment slot
                        writer.Write(HP_PotionToUse != null);
                        if (HP_PotionToUse != null)
                            HP_PotionToUse.Save(writer);
                        writer.Write(MP_PotionToUse != null);
                        if (MP_PotionToUse != null)
                            MP_PotionToUse.Save(writer);
                        writer.Write(InstanPotionToUse != null);
                        if (InstanPotionToUse != null)
                            InstanPotionToUse.Save(writer);
                        //Save the Inventory length
                        writer.Write(Inventory.Length);
                        for (int i = 0; i < Inventory.Length; i++)
                        {
                            writer.Write(Inventory[i] != null);
                            if (Inventory[i] == null)
                                continue;
                            //Save each Inventory slot.
                            Inventory[i].Save(writer);
                        }

                        writer.Write(Equipment.Length);
                        for (int i = 0; i < Equipment.Length; i++)
                        {
                            writer.Write(Equipment[i] != null);
                            if (Equipment[i] == null)
                                continue;
                            Equipment[i].Save(writer);
                        }

                        //Save the Magic Count
                        writer.Write(Magics.Count);
                        for (int i = 0; i < Magics.Count; i++)
                            //Save each Magic
                            Magics[i].Save(writer);

                        writer.Write(Dead);
                        writer.Write(HP);
                        writer.Write(MP);
                        writer.Write(HeroLevel);
                        writer.Write(HeroExperience);

                        writer.Write(HeroStage);

                        writer.Write(isLocked);

                        writer.Write(PlayerMaster.CurrentQuests.Count(x => x.Info.HeroQuest));
                        for (int i = 0; i < PlayerMaster.Info.CurrentQuests.Count; i++)
                        {
                            if (PlayerMaster.Info.CurrentQuests[i].Info.HeroQuest)
                                PlayerMaster.Info.CurrentQuests[i].Save(writer);
                        }


                        //Save the QuestInventory length
                        writer.Write(QuestInventory.Length);
                        for (int i = 0; i < QuestInventory.Length; i++)
                        {
                            writer.Write(QuestInventory[i] != null);
                            if (QuestInventory[i] == null)
                                continue;
                            //Save each QuestInventory slot.
                            QuestInventory[i].Save(writer);
                        }

                        writer.Write(AutoSpawn);
                        writer.Write(PKPoints);

                        writer.Write(autoPotSystem.AutoPotList.Count);
                        foreach(var p in autoPotSystem.AutoPotList)
                        {
                            writer.Write(p.ItemIndex);
                            writer.Write(p.PercentTrigger);
                            writer.Write(p.Delay);
                        }

                        writer.Write(autoPotSystem.isEnabled);
                        
                        writer.Write(ExpRateOffset);
                        
                        writer.Write(Buffs.Count);
                        for (int i = 0; i < Buffs.Count; i++)
                        {
                            Buff buff = Buffs[i];
                            if (!buff.Infinite)
                            {
                                if (buff.Type != BuffType.Curse)
                                {
                                    buff.Caster = null;
                                    if (!buff.Paused) buff.ExpireTime -= Envir.Time;
                                }
                            }

                            Buffs[i].Save(writer);
                        }

                        writer.Write(Active);
                        writer.Write(Deleted);
                    }
                }

                PlayerMaster.Info.HeroLoadVersion = Envir.LoadVersion;

                if (!Directory.Exists(@".\Back Up\" + Master.Name + (Hcount == 0 ? "" : Hcount.ToString()))) Directory.CreateDirectory(@".\Back Up\" + Master.Name + (Hcount == 0 ? "" : Hcount.ToString()));

                File.Copy(@".\Heroes\" + Master.Name + (Hcount == 0 ? "" : Hcount.ToString()) + ".data", @".\Back Up\" + Master.Name + (Hcount == 0 ? "" : Hcount.ToString()) + @"\" + string.Format("{6} {0:0000}-{1:00}-{2:00} {3:00}-{4:00}-{5:00}.bak", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second,Name));

                return true;
            }
            //  Here to display any errors to do with the saving of the hero.
            catch (Exception ex)
            {
                SMain.Enqueue(ex);
                return false;
            }
        }


        public void PauseBuff(Buff b)
        {
            if (b.Paused) return;

            b.ExpireTime = b.ExpireTime - Envir.Time;
            b.Paused = true;
            PlayerMaster.Enqueue(new S.RemoveBuff { Type = b.Type, ObjectID = ObjectID, Hero = true });
        }
        public void UnpauseBuff(Buff b)
        {
            if (!b.Paused) return;

            b.ExpireTime = b.ExpireTime + Envir.Time;
            b.Paused = false;
            PlayerMaster.Enqueue(new S.AddBuff { Type = b.Type, Caster = Name, Expire = b.ExpireTime - Envir.Time, Values = b.Values, Infinite = b.Infinite, ObjectID = ObjectID, Visible = b.Visible , Hero = true});
        }

        /// <summary>
        /// Load the Players Hero data.
        /// </summary>
        /// <returns></returns>
        public bool LoadHero(int Hcount)
        {
            if (Master != null)
            {
                if (!File.Exists(@".\Heroes\" + Master.Name + (Hcount == 0 ? "" : Hcount.ToString()) + ".data"))
                {
                    return false;
                }
                try
                {
                    //  We'll load the Hero from a players own save file.
                    using (FileStream stream = new FileStream(@".\Heroes\" + Master.Name + (Hcount == 0 ? "" : Hcount.ToString()) + ".data", FileMode.OpenOrCreate))
                    {
                        //  Start a binary reader.
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            HeroName = reader.ReadString();
                            HeroClass = (MirClass)reader.ReadByte();
                            HeroGender = (MirGender)reader.ReadByte();
                            HeroHair = reader.ReadByte();
                            Summoned = reader.ReadBoolean();
                            if (reader.ReadBoolean())
                                HP_PotionToUse = new UserItem(reader, Version, customVersion);
                            if (reader.ReadBoolean())
                                MP_PotionToUse = new UserItem(reader, Version, customVersion);
                            if (reader.ReadBoolean())
                                InstanPotionToUse = new UserItem(reader, Version, customVersion);
                            int count = reader.ReadInt32();
                            Array.Resize(ref Inventory, count);
                            for (int i = 0; i < Inventory.Length; i++)
                            {
                                if (!reader.ReadBoolean())
                                    continue;
                                UserItem item = new UserItem(reader, Version, customVersion);
                                if (SMain.Envir.BindItem(item) && i < Inventory.Length)
                                    Inventory[i] = item;
                            }

                            count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                if (!reader.ReadBoolean())
                                    continue;
                                UserItem item = new UserItem(reader, Version, customVersion);
                                if (SMain.Envir.BindItem(item) && i < Equipment.Length)
                                    Equipment[i] = item;
                            }
                            count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                UserMagic magic = new UserMagic(reader);
                                if (magic.Info == null)
                                    continue;
                                Magics.Add(magic);
                            }


                            Dead = reader.ReadBoolean();
                            HP = reader.ReadUInt32();
                            MP = reader.ReadUInt32();
                            HeroLevel = reader.ReadInt32();
                            HeroExperience = reader.ReadInt64();

                            HeroStage = reader.ReadByte();

                            isLocked = reader.ReadBoolean();

                            count = reader.ReadInt32();

                            for (int i = 0; i < count; i++)
                            {
                                QuestProgressInfo quest = new QuestProgressInfo(reader);

                                if (!PlayerMaster.CurrentQuests.Any(x => x.Index == quest.Index))
                                {
                                    if (SMain.Envir.BindQuest(quest))
                                        PlayerMaster.CurrentQuests.Add(quest);
                                }
                            }

                            count = reader.ReadInt32();
                            for (int i = 0; i < count; i++)
                            {
                                if (!reader.ReadBoolean())
                                    continue;
                                UserItem item = new UserItem(reader, Version, customVersion);
                                if (SMain.Envir.BindItem(item) && i < QuestInventory.Length)
                                    QuestInventory[i] = item;
                            }


                            if (reader.PeekChar() != -1)
                            {
                                AutoSpawn = reader.ReadBoolean();
                            }

                            if (reader.PeekChar() != -1)
                            {
                                PKPoints = reader.ReadInt32();

                                count = reader.ReadInt32();
                                for (int i = 0; i < count; i++)
                                {
                                    var pot = autoPotSystem.AutoPotList[i];

                                    pot.ItemIndex = reader.ReadInt32();
                                    pot.PercentTrigger = reader.ReadByte();
                                    pot.Delay = reader.ReadInt64();


                                }
                            }

                            if (reader.PeekChar() != -1)
                            {
                                autoPotSystem.isEnabled = reader.ReadBoolean();
                            }

                            if (reader.PeekChar() != -1)
                            {
                                ExpRateOffset= reader.ReadSingle();
                            }

                            if (Version > 115)
                            {
                                count = reader.ReadInt32();
                                for (int i = 0; i < count; i++)
                                {
                                    Buff buff = new Buff(reader);           

                                    buff.ExpireTime += Envir.Time;
                                    buff.Paused = false;

                                    AddBuff(buff);

                                }
                            }

                            if (Version > 119)
                            {
                                Active = reader.ReadBoolean();                              
                            }

                            if (Version > 120)
                            {
                                Deleted = reader.ReadBoolean();
                            }


                        }
                    }

                    for (int i = 0; i < PlayerMaster.CurrentQuests.Count; i++)
                    {
                        if (PlayerMaster.CurrentQuests[i].Info.HeroQuest)
                        {
                            PlayerMaster.CurrentQuests[i].ResyncTasks();
                            PlayerMaster.SendUpdateQuest(PlayerMaster.CurrentQuests[i], QuestState.Add);
                        }
                    }
                    /*
                    foreach(var i in QuestInventory)
                    {
                        if (i != null)
                        PlayerMaster.CheckItem(i);
                    }
                    */
                    /*
                    foreach(var i in autoPotSystem.AutoPotList)
                    {
                        var item = Envir.GetItemInfo(i.ItemIndex);
                        if (item != null)
                            PlayerMaster.CheckItemInfo(item);
                    }
                    */
                                
                    if (HeroLevel > 0)
                        MaxExperience = HeroLevel < Envir.heroConfig.HeroExpRequired.Length ? Envir.heroConfig.HeroExpRequired[HeroLevel - 1] : 0;
                    return true;
                }
                catch (Exception ex)
                {
                    SMain.Enqueue(ex);
                    return false;
                }
            }
            else
                return false;
        }

        public override void Despawn()
        {
            isSpawn = false;
            if (!Dead)
                UpdateClientSpawn(HeroState.Unspawned);
            base.Despawn();

        }

        public S.HeroInformation GetUserInfo()
        {
            AutoPot HpAuto = null, MpAuto = null;
            foreach (var pot in autoPotSystem.AutoPotList)
            {
                if (pot.AutoPotType == 1)
                {
                    HpAuto = pot;
                }
                if (pot.AutoPotType == 2)
                {
                    MpAuto = pot;
                }
            }


            S.HeroInformation packet = new S.HeroInformation
            {
                ObjectID = ObjectID,
                RealId = (uint)Info.Index,
                Name = HeroName,
                NameColour = NameColour,
                Class = HeroClass,
                Gender = HeroGender,
                Level = (ushort)HeroLevel,
                Location = CurrentLocation,
                Direction = Direction,
                Hair = HeroHair,
                HP = (ushort)HP,
                MP = (ushort)MP,
                MaxHP = (ushort)MaxHP,
                MaxMP = (ushort)MaxMP,

                Experience = HeroExperience,
                MaxExperience = MaxExperience,
                isLocked = isLocked,
                AllowAutoPot = autoPotSystem.isEnabled,

                Inventory = new UserItem[Inventory.Length],
                Equipment = new UserItem[Equipment.Length],
                QuestInventory = new UserItem[QuestInventory.Length],

                HpRate = HpAuto.PercentTrigger,
                MpRate = MpAuto.PercentTrigger,

                HpInfo = HpAuto.ItemIndex,
                MpInfo = MpAuto.ItemIndex,

            };

            //Copy this method to prevent modification before sending packet information.
            for (int i = 0; i < Magics.Count; i++)
                packet.Magics.Add(Magics[i].CreateClientMagic());

            Inventory.CopyTo(packet.Inventory, 0);
            Equipment.CopyTo(packet.Equipment, 0);
            QuestInventory.CopyTo(packet.QuestInventory, 0);
                


            return packet;
        }

        public override void RefreshNameColour(bool send = true)
        {
            var prevColor = NameColour;
            Color colour = Color.MediumPurple;

            if (PKPoints >= 200)
                colour = Color.Red;
            else if (Envir.Time < BrownTime)
                colour = Color.SaddleBrown;
            else if (PKPoints >= 100)
                colour = Color.Yellow;

            NameColour = colour;

            if (prevColor == NameColour) return;

            BroadcastColourChange();
        }

        public void BroadcastColourChange()
        {
            if (CurrentMap == null) return;

            for (int i = CurrentMap.Players.Count - 1; i >= 0; i--)
            {
                PlayerObject player = CurrentMap.Players[i];

                if (Functions.InRange(CurrentLocation, player.CurrentLocation, Globals.DataRange))
                    player.Enqueue(new S.ObjectColourChanged { ObjectID = ObjectID, NameColour = NameColour});
            }
        }

        public S.HeroStats GetHeroStats()
        {
            S.HeroStats packet = new S.HeroStats
            {
                MinAC = MinAC,
                MaxAC = MaxAC,
                MinMAC = MinMAC,
                MaxMAC = MaxMAC,
                MinDC = MinDC,
                MaxDC = MaxDC,
                MinMC = MinMC,
                MaxMC = MaxMC,
                Agility = Agility,
                Accuracy = Accuracy,
                ASpeed = ASpeed,
                Luck = Luck,
                MinSC = MinSC,
                MaxSC = MaxSC,
                MaxHP = MaxHP,
                MaxMP = MaxMP,
                CriticalDamage = CriticalDamage,
                CriticalRate = CriticalRate,
                CurrentBagWeight = CurrentBagWeight,
                CurrentHandWeight = CurrentHandWeight,
                CurrentWearWeight = CurrentWearWeight,
                MaxBagWeight = MaxBagWeight,
                MaxHandWeight = MaxHandWeight,
                MaxWearWeight = MaxWearWeight,
                MagicResist = MagicResist,
                PoisonAttack = PoisonAttack,
                PoisonRecovery = PoisonRecovery,
                PoisonResist = PoisonResist,
                Freezing = Freezing,
                HealthRecovery = HealthRecovery,
                SpellRecovery = SpellRecovery,
                Holy = Holy,
            };


            return packet;
        }



        public override Packet GetInfo()
        {
            if (Name.Contains("Icey"))
            {

            }
            if (Race != ObjectType.Hero)
                return null;
            return new S.ObjectPlayer
            {
                ObjectID = ObjectID,
                Name = Name,
                NameColour = NameColour,
                Class = HeroClass,
                Gender = HeroGender,
                Location = CurrentLocation,
                Direction = Direction,
                Hair = HeroHair,
                Weapon = Equipment[(int)EquipmentSlot.Weapon] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Weapon].Info, (ushort)HeroLevel, HeroClass, Envir.ItemInfoList).Shape : (short)-1,
                Armour = Armour = Equipment[(int)EquipmentSlot.Armour] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Armour].Info, (ushort)HeroLevel, HeroClass, Envir.ItemInfoList).Shape : (byte)0,
                Light = Light,
                WingEffect = Equipment[(int)EquipmentSlot.Armour] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Armour].Info, (ushort)HeroLevel, HeroClass, Envir.ItemInfoList).Effect : (byte)0,
                WeaponEffect = Equipment[(int)EquipmentSlot.Weapon] != null ? Functions.GetRealItem(Equipment[(int)EquipmentSlot.Weapon].Info, (ushort)HeroLevel, HeroClass, Envir.ItemInfoList).Effect : (byte)0,
                Poison = CurrentPoison,
                Dead = Dead,
                Hidden = Hidden,
                Buffs = Buffs.Where(d => d.Visible).Select(e => e.Type).ToList(),
                Effect = SpellEffect.None,
                Extra = false,
                TransformType = -1,
                IsHero = true,
            };
        }
    }
}
