using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.MirDatabase;
using S = ServerPackets;

namespace Server.MirObjects.Monsters
{
    public class CustomAI : MonsterObject
    {
        long debuffDuration1;
        byte DebuffDuration1;
        long stealDuration1;
        byte StealDuration1;
        byte DebuffStealAmount1;
        byte CurrentStolenAmount1;
        bool CanSteal1 = false;

        long debuffDuration2;
        byte DebuffDuration2;
        long stealDuration2;
        byte StealDuration2;
        byte DebuffStealAmount2;
        byte CurrentStolenAmount2;
        bool CanSteal2 = false;

        long debuffDuration3;
        byte DebuffDuration3;
        long stealDuration3;
        byte StealDuration3;
        byte DebuffStealAmount3;
        byte CurrentStolenAmount3;
        bool CanSteal3 = false;

        long debuffDuration4;
        byte DebuffDuration4;
        long stealDuration4;
        byte StealDuration4;
        byte DebuffStealAmount4;
        byte CurrentStolenAmount4;
        bool CanSteal4 = false;

        long debuffDuration5;
        byte DebuffDuration5;
        long stealDuration5;
        byte StealDuration5;
        byte DebuffStealAmount5;
        byte CurrentStolenAmount5;
        bool CanSteal5 = false;

        byte numberOfAttacks;
        byte HealthStage;
        long attack1Time;
        long Attack1Time;
        long attack2Time;
        long Attack2Time;
        long attack3Time;
        long Attack3Time;
        long attack4Time;
        long Attack4Time;
        long attack5Time;
        long Attack5Time;
        AIAttackType AttackType1 = AIAttackType.Default;
        AIAttackType AttackType2 = AIAttackType.Default;
        AIAttackType AttackType3 = AIAttackType.Default;
        AIAttackType AttackType4 = AIAttackType.Default;
        AIAttackType AttackType5 = AIAttackType.Default;
        AIAttackStyle AttackStyle1 = AIAttackStyle.Default;
        AIAttackStyle AttackStyle2 = AIAttackStyle.Default;
        AIAttackStyle AttackStyle3 = AIAttackStyle.Default;
        AIAttackStyle AttackStyle4 = AIAttackStyle.Default;
        AIAttackStyle AttackStyle5 = AIAttackStyle.Default;
        AIAttackEffect AttackEffect1 = AIAttackEffect.None;
        AIAttackEffect AttackEffect2 = AIAttackEffect.None;
        AIAttackEffect AttackEffect3 = AIAttackEffect.None;
        AIAttackEffect AttackEffect4 = AIAttackEffect.None;
        AIAttackEffect AttackEffect5 = AIAttackEffect.None;
        AIDebuffType Debuff1 = AIDebuffType.None;
        AIDebuffType Debuff2 = AIDebuffType.None;
        AIDebuffType Debuff3 = AIDebuffType.None;
        AIDebuffType Debuff4 = AIDebuffType.None;
        AIDebuffType Debuff5 = AIDebuffType.None;
        public CustomAI(MonsterInfo info) : base(info)
        {
            if (Info.Attacks != null &&
                Info.Attacks.Count > 0)
            {
                if (Info.Attacks.Count == 1)
                {
                    Attack1Time = info.Attacks[0].Delay;
                    AttackType1 = info.Attacks[0].AttackType;
                    AttackStyle1 = info.Attacks[0].AttackStyle;
                    AttackEffect1 = info.Attacks[0].AttackEffect;
                    Debuff1 = info.Attacks[0].Debuff;
                    CanSteal1 = info.Attacks[0].Steal;
                    DebuffDuration1 = info.Attacks[0].DebuffDuration;
                    DebuffStealAmount1 = info.Attacks[0].StealPercent;
                    StealDuration1 = info.Attacks[0].StealDuration;
                }
                if (Info.Attacks.Count == 2)
                {
                    Attack1Time = info.Attacks[0].Delay;
                    AttackType1 = info.Attacks[0].AttackType;
                    AttackStyle1 = info.Attacks[0].AttackStyle;
                    AttackEffect1 = info.Attacks[0].AttackEffect;
                    Debuff1 = info.Attacks[0].Debuff;
                    CanSteal1 = info.Attacks[0].Steal;
                    DebuffDuration1 = info.Attacks[0].DebuffDuration;
                    DebuffStealAmount1 = info.Attacks[0].StealPercent;
                    StealDuration1 = info.Attacks[0].StealDuration;
                    Attack2Time = info.Attacks[1].Delay;
                    AttackType2 = info.Attacks[1].AttackType;
                    AttackStyle2 = info.Attacks[1].AttackStyle;
                    AttackEffect2 = info.Attacks[1].AttackEffect;
                    Debuff2 = info.Attacks[1].Debuff;
                    CanSteal2 = info.Attacks[1].Steal;
                    DebuffDuration2 = info.Attacks[1].DebuffDuration;
                    DebuffStealAmount2 = info.Attacks[1].StealPercent;
                    StealDuration2 = info.Attacks[1].StealDuration;
                }
                if (Info.Attacks.Count == 3)
                {
                    Attack1Time = info.Attacks[0].Delay;
                    AttackType1 = info.Attacks[0].AttackType;
                    AttackStyle1 = info.Attacks[0].AttackStyle;
                    AttackEffect1 = info.Attacks[0].AttackEffect;
                    Debuff1 = info.Attacks[0].Debuff;
                    CanSteal1 = info.Attacks[0].Steal;
                    DebuffDuration1 = info.Attacks[0].DebuffDuration;
                    DebuffStealAmount1 = info.Attacks[0].StealPercent;
                    StealDuration1 = info.Attacks[0].StealDuration;
                    Attack2Time = info.Attacks[1].Delay;
                    AttackType2 = info.Attacks[1].AttackType;
                    AttackStyle2 = info.Attacks[1].AttackStyle;
                    AttackEffect2 = info.Attacks[1].AttackEffect;
                    Debuff2 = info.Attacks[1].Debuff;
                    CanSteal2 = info.Attacks[1].Steal;
                    DebuffDuration2 = info.Attacks[1].DebuffDuration;
                    DebuffStealAmount2 = info.Attacks[1].StealPercent;
                    StealDuration2 = info.Attacks[1].StealDuration;
                    Attack3Time = info.Attacks[2].Delay;
                    AttackType3 = info.Attacks[2].AttackType;
                    AttackStyle3 = info.Attacks[2].AttackStyle;
                    AttackEffect3 = info.Attacks[2].AttackEffect;
                    Debuff3 = info.Attacks[2].Debuff;
                    CanSteal3 = info.Attacks[2].Steal;
                    DebuffDuration3 = info.Attacks[2].DebuffDuration;
                    DebuffStealAmount3 = info.Attacks[2].StealPercent;
                    StealDuration3 = info.Attacks[2].StealDuration;
                }
                if (Info.Attacks.Count == 4)
                {
                    Attack1Time = info.Attacks[0].Delay;
                    AttackType1 = info.Attacks[0].AttackType;
                    AttackStyle1 = info.Attacks[0].AttackStyle;
                    AttackEffect1 = info.Attacks[0].AttackEffect;
                    Debuff1 = info.Attacks[0].Debuff;
                    CanSteal1 = info.Attacks[0].Steal;
                    DebuffDuration1 = info.Attacks[0].DebuffDuration;
                    DebuffStealAmount1 = info.Attacks[0].StealPercent;
                    StealDuration1 = info.Attacks[0].StealDuration;
                    Attack2Time = info.Attacks[1].Delay;
                    AttackType2 = info.Attacks[1].AttackType;
                    AttackStyle2 = info.Attacks[1].AttackStyle;
                    AttackEffect2 = info.Attacks[1].AttackEffect;
                    Debuff2 = info.Attacks[1].Debuff;
                    CanSteal2 = info.Attacks[1].Steal;
                    DebuffDuration2 = info.Attacks[1].DebuffDuration;
                    DebuffStealAmount2 = info.Attacks[1].StealPercent;
                    StealDuration2 = info.Attacks[1].StealDuration;
                    Attack3Time = info.Attacks[2].Delay;
                    AttackType3 = info.Attacks[2].AttackType;
                    AttackStyle3 = info.Attacks[2].AttackStyle;
                    AttackEffect3 = info.Attacks[2].AttackEffect;
                    Debuff3 = info.Attacks[2].Debuff;
                    CanSteal3 = info.Attacks[2].Steal;
                    DebuffDuration3 = info.Attacks[2].DebuffDuration;
                    DebuffStealAmount3 = info.Attacks[2].StealPercent;
                    StealDuration3 = info.Attacks[2].StealDuration;
                    Attack4Time = info.Attacks[3].Delay;
                    AttackType4 = info.Attacks[3].AttackType;
                    AttackStyle4 = info.Attacks[3].AttackStyle;
                    AttackEffect4 = info.Attacks[3].AttackEffect;
                    Debuff4 = info.Attacks[3].Debuff;
                    CanSteal4 = info.Attacks[3].Steal;
                    DebuffDuration4 = info.Attacks[3].DebuffDuration;
                    DebuffStealAmount4 = info.Attacks[3].StealPercent;
                    StealDuration4 = info.Attacks[3].StealDuration;
                }
                if (Info.Attacks.Count == 5)
                {
                    Attack1Time = info.Attacks[0].Delay;
                    AttackType1 = info.Attacks[0].AttackType;
                    AttackStyle1 = info.Attacks[0].AttackStyle;
                    AttackEffect1 = info.Attacks[0].AttackEffect;
                    Debuff1 = info.Attacks[0].Debuff;
                    CanSteal1 = info.Attacks[0].Steal;
                    DebuffDuration1 = info.Attacks[0].DebuffDuration;
                    DebuffStealAmount1 = info.Attacks[0].StealPercent;
                    StealDuration1 = info.Attacks[0].StealDuration;
                    Attack2Time = info.Attacks[1].Delay;
                    AttackType2 = info.Attacks[1].AttackType;
                    AttackStyle2 = info.Attacks[1].AttackStyle;
                    AttackEffect2 = info.Attacks[1].AttackEffect;
                    Debuff2 = info.Attacks[1].Debuff;
                    CanSteal2 = info.Attacks[1].Steal;
                    DebuffDuration2 = info.Attacks[1].DebuffDuration;
                    DebuffStealAmount2 = info.Attacks[1].StealPercent;
                    StealDuration2 = info.Attacks[1].StealDuration;
                    Attack3Time = info.Attacks[2].Delay;
                    AttackType3 = info.Attacks[2].AttackType;
                    AttackStyle3 = info.Attacks[2].AttackStyle;
                    AttackEffect3 = info.Attacks[2].AttackEffect;
                    Debuff3 = info.Attacks[2].Debuff;
                    CanSteal3 = info.Attacks[2].Steal;
                    DebuffDuration3 = info.Attacks[2].DebuffDuration;
                    DebuffStealAmount3 = info.Attacks[2].StealPercent;
                    StealDuration3 = info.Attacks[2].StealDuration;
                    Attack4Time = info.Attacks[3].Delay;
                    AttackType4 = info.Attacks[3].AttackType;
                    AttackStyle4 = info.Attacks[3].AttackStyle;
                    AttackEffect4 = info.Attacks[3].AttackEffect;
                    Debuff4 = info.Attacks[3].Debuff;
                    CanSteal4 = info.Attacks[3].Steal;
                    DebuffDuration4 = info.Attacks[3].DebuffDuration;
                    DebuffStealAmount4 = info.Attacks[3].StealPercent;
                    StealDuration4 = info.Attacks[3].StealDuration;
                    Attack5Time = info.Attacks[4].Delay;
                    AttackType5 = info.Attacks[4].AttackType;
                    AttackStyle5 = info.Attacks[4].AttackStyle;
                    AttackEffect5 = info.Attacks[4].AttackEffect;
                    Debuff5 = info.Attacks[4].Debuff;
                    CanSteal5 = info.Attacks[4].Steal;
                    DebuffDuration5 = info.Attacks[4].DebuffDuration;
                    DebuffStealAmount5 = info.Attacks[4].StealPercent;
                    StealDuration5 = info.Attacks[4].StealDuration;
                }


                numberOfAttacks = (byte)Info.Attacks.Count();
            }
        }


        public override void ApplyPoison(Poison p, MapObject Caster = null, bool NoResist = false, bool ignoreDefence = true)
        {
            /*
            if (!Info.AntiPoison.HasFlag(AIAntiPoison.None))
            {
                switch(p.PType)
                {
                    case PoisonType.Green:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Green))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Red:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Red))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Slow:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Slow))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Frozen:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Frozen))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Stun:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Stun))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Paralysis:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Paralysis))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.DelayedExplosion:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.DelayedExplosion))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Bleeding:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Bleeding))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.LRParalysis:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.LRParalysis))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Trap:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Trap))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    case PoisonType.Burning:
                        if (!Info.AntiPoison.HasFlag(AIAntiPoison.Burning))
                            base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                    default:
                        base.ApplyPoison(p, Caster, NoResist, ignoreDefence);
                        break;
                }
            }
            */
        }

        public void Attack1()
        {
            //  Normal hit no effect
            if ((AttackType1 == AIAttackType.Default || AttackType1 == AIAttackType.Melee) &&
                (AttackStyle1 == AIAttackStyle.Default || AttackStyle1 == AIAttackStyle.MeleeSingle) &&
                AttackEffect1 == AIAttackEffect.None)
            {

            }
            //  Normal hit with effect
            else if ((AttackType1 == AIAttackType.Default || AttackType1 == AIAttackType.Melee) &&
                    (AttackStyle1 == AIAttackStyle.Default || AttackStyle1 == AIAttackStyle.MeleeSingle) &&
                    AttackEffect1 != AIAttackEffect.None)
            {
                
            }
            //  Melee Multi hit without effect
            else if ((AttackType1 == AIAttackType.Default || AttackType1 == AIAttackType.Melee) &&
                    AttackStyle1 == AIAttackStyle.MeleeMulti && AttackEffect1 == AIAttackEffect.None)
            {

            }
            //  Melee Multi hit with effect
            else if ((AttackType1 == AIAttackType.Default || AttackType1 == AIAttackType.Melee) &&
                    AttackStyle1 == AIAttackStyle.MeleeMulti && AttackEffect1 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect1, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range normal no effect
            else if (AttackType1 == AIAttackType.Range &&
                    (AttackStyle1 == AIAttackStyle.RangeSingle || AttackStyle1 == AIAttackStyle.Default) &&
                    AttackEffect1 == AIAttackEffect.None)
            {
                
            }
            //  Range normal with effect
            else if (AttackType1 == AIAttackType.Range &&
                    (AttackStyle1 == AIAttackStyle.RangeSingle || AttackStyle1 == AIAttackStyle.Default) &&
                    AttackEffect1 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect1, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi no effect
            else if (AttackType1 == AIAttackType.Range &&
                    AttackStyle1 == AIAttackStyle.RangeMulti && AttackEffect1 == AIAttackEffect.None)
            {

            }
            //  Range multi with effect
            else if (AttackType1 == AIAttackType.Range &&
                    AttackStyle1 == AIAttackStyle.RangeMulti && AttackEffect1 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect1, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee debuff no effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    (AttackStyle1 == AIAttackStyle.Default || AttackStyle1 == AIAttackStyle.MeleeSingle) && AttackEffect1 == AIAttackEffect.None)
            {

            }
            //  Melee debuff with effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    (AttackStyle1 == AIAttackStyle.Default || AttackStyle1 == AIAttackStyle.MeleeSingle) && AttackEffect1 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect1, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee multi debuff no effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    AttackStyle1 == AIAttackStyle.MeleeMulti && AttackEffect1 == AIAttackEffect.None)
            {

            }
            //  Melee multi debuff with effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    AttackStyle1 == AIAttackStyle.MeleeMulti && AttackEffect1 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect1, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range debuff no effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    (AttackStyle1 == AIAttackStyle.Default || AttackStyle1 == AIAttackStyle.RangeSingle) && AttackEffect1 == AIAttackEffect.None)
            {

            }
            //  Range debuff wih effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    (AttackStyle1 == AIAttackStyle.Default || AttackStyle1 == AIAttackStyle.RangeSingle) && AttackEffect1 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect1, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi debuff no effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    AttackStyle1 == AIAttackStyle.RangeMulti && AttackEffect1 == AIAttackEffect.None)
            {

            }
            //  Range multi debuff wih effect
            else if (AttackType1 == AIAttackType.Debuff &&
                    AttackStyle1 == AIAttackStyle.RangeMulti && AttackEffect1 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect1, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
        }

        public void Attack2()
        {
            //  Normal hit no effect
            if ((AttackType2 == AIAttackType.Default || AttackType2 == AIAttackType.Melee) &&
                (AttackStyle2 == AIAttackStyle.Default || AttackStyle2 == AIAttackStyle.MeleeSingle) &&
                AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Normal hit with effect
            else if ((AttackType2 == AIAttackType.Default || AttackType2 == AIAttackType.Melee) &&
                    (AttackStyle2 == AIAttackStyle.Default || AttackStyle2 == AIAttackStyle.MeleeSingle) &&
                    AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee Multi hit without effect
            else if ((AttackType2 == AIAttackType.Default || AttackType2 == AIAttackType.Melee) &&
                    AttackStyle2 == AIAttackStyle.MeleeMulti && AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Melee Multi hit with effect
            else if ((AttackType2 == AIAttackType.Default || AttackType2 == AIAttackType.Melee) &&
                    AttackStyle2 == AIAttackStyle.MeleeMulti && AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range normal no effect
            else if (AttackType2 == AIAttackType.Range &&
                    (AttackStyle2 == AIAttackStyle.RangeSingle || AttackStyle2 == AIAttackStyle.Default) &&
                    AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Range normal with effect
            else if (AttackType2 == AIAttackType.Range &&
                    (AttackStyle2 == AIAttackStyle.RangeSingle || AttackStyle2 == AIAttackStyle.Default) &&
                    AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi no effect
            else if (AttackType2 == AIAttackType.Range &&
                    AttackStyle2 == AIAttackStyle.RangeMulti && AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Range multi with effect
            else if (AttackType2 == AIAttackType.Range &&
                    AttackStyle2 == AIAttackStyle.RangeMulti && AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee debuff no effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    (AttackStyle2 == AIAttackStyle.Default || AttackStyle2 == AIAttackStyle.MeleeSingle) && AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Melee debuff with effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    (AttackStyle2 == AIAttackStyle.Default || AttackStyle2 == AIAttackStyle.MeleeSingle) && AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee multi debuff no effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    AttackStyle2 == AIAttackStyle.MeleeMulti && AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Melee multi debuff with effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    AttackStyle2 == AIAttackStyle.MeleeMulti && AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range debuff no effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    (AttackStyle2 == AIAttackStyle.Default || AttackStyle2 == AIAttackStyle.RangeSingle) && AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Range debuff wih effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    (AttackStyle2 == AIAttackStyle.Default || AttackStyle2 == AIAttackStyle.RangeSingle) && AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi debuff no effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    AttackStyle2 == AIAttackStyle.RangeMulti && AttackEffect2 == AIAttackEffect.None)
            {

            }
            //  Range multi debuff wih effect
            else if (AttackType2 == AIAttackType.Debuff &&
                    AttackStyle2 == AIAttackStyle.RangeMulti && AttackEffect2 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect2, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
        }

        public void Attack3()
        {
            //  Normal hit no effect
            if ((AttackType3 == AIAttackType.Default || AttackType3 == AIAttackType.Melee) &&
                (AttackStyle3 == AIAttackStyle.Default || AttackStyle3 == AIAttackStyle.MeleeSingle) &&
                AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Normal hit with effect
            else if ((AttackType3 == AIAttackType.Default || AttackType3 == AIAttackType.Melee) &&
                    (AttackStyle3 == AIAttackStyle.Default || AttackStyle3 == AIAttackStyle.MeleeSingle) &&
                    AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee Multi hit without effect
            else if ((AttackType3 == AIAttackType.Default || AttackType3 == AIAttackType.Melee) &&
                    AttackStyle3 == AIAttackStyle.MeleeMulti && AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Melee Multi hit with effect
            else if ((AttackType3 == AIAttackType.Default || AttackType3 == AIAttackType.Melee) &&
                    AttackStyle3 == AIAttackStyle.MeleeMulti && AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range normal no effect
            else if (AttackType3 == AIAttackType.Range &&
                    (AttackStyle3 == AIAttackStyle.RangeSingle || AttackStyle3 == AIAttackStyle.Default) &&
                    AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Range normal with effect
            else if (AttackType3 == AIAttackType.Range &&
                    (AttackStyle3 == AIAttackStyle.RangeSingle || AttackStyle3 == AIAttackStyle.Default) &&
                    AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi no effect
            else if (AttackType3 == AIAttackType.Range &&
                    AttackStyle3 == AIAttackStyle.RangeMulti && AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Range multi with effect
            else if (AttackType3 == AIAttackType.Range &&
                    AttackStyle3 == AIAttackStyle.RangeMulti && AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee debuff no effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    (AttackStyle3 == AIAttackStyle.Default || AttackStyle3 == AIAttackStyle.MeleeSingle) && AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Melee debuff with effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    (AttackStyle3 == AIAttackStyle.Default || AttackStyle3 == AIAttackStyle.MeleeSingle) && AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee multi debuff no effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    AttackStyle3 == AIAttackStyle.MeleeMulti && AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Melee multi debuff with effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    AttackStyle3 == AIAttackStyle.MeleeMulti && AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range debuff no effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    (AttackStyle3 == AIAttackStyle.Default || AttackStyle3 == AIAttackStyle.RangeSingle) && AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Range debuff wih effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    (AttackStyle3 == AIAttackStyle.Default || AttackStyle3 == AIAttackStyle.RangeSingle) && AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi debuff no effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    AttackStyle3 == AIAttackStyle.RangeMulti && AttackEffect3 == AIAttackEffect.None)
            {

            }
            //  Range multi debuff wih effect
            else if (AttackType3 == AIAttackType.Debuff &&
                    AttackStyle3 == AIAttackStyle.RangeMulti && AttackEffect3 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect3, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
        }

        public void Attack4()
        {
            //  Normal hit no effect
            if ((AttackType4 == AIAttackType.Default || AttackType4 == AIAttackType.Melee) &&
                (AttackStyle4 == AIAttackStyle.Default || AttackStyle4 == AIAttackStyle.MeleeSingle) &&
                AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Normal hit with effect
            else if ((AttackType4 == AIAttackType.Default || AttackType4 == AIAttackType.Melee) &&
                    (AttackStyle4 == AIAttackStyle.Default || AttackStyle4 == AIAttackStyle.MeleeSingle) &&
                    AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee Multi hit without effect
            else if ((AttackType4 == AIAttackType.Default || AttackType4 == AIAttackType.Melee) &&
                    AttackStyle4 == AIAttackStyle.MeleeMulti && AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Melee Multi hit with effect
            else if ((AttackType4 == AIAttackType.Default || AttackType4 == AIAttackType.Melee) &&
                    AttackStyle4 == AIAttackStyle.MeleeMulti && AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range normal no effect
            else if (AttackType4 == AIAttackType.Range &&
                    (AttackStyle4 == AIAttackStyle.RangeSingle || AttackStyle4 == AIAttackStyle.Default) &&
                    AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Range normal with effect
            else if (AttackType4 == AIAttackType.Range &&
                    (AttackStyle4 == AIAttackStyle.RangeSingle || AttackStyle4 == AIAttackStyle.Default) &&
                    AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi no effect
            else if (AttackType4 == AIAttackType.Range &&
                    AttackStyle4 == AIAttackStyle.RangeMulti && AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Range multi with effect
            else if (AttackType4 == AIAttackType.Range &&
                    AttackStyle4 == AIAttackStyle.RangeMulti && AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee debuff no effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    (AttackStyle4 == AIAttackStyle.Default || AttackStyle4 == AIAttackStyle.MeleeSingle) && AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Melee debuff with effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    (AttackStyle4 == AIAttackStyle.Default || AttackStyle4 == AIAttackStyle.MeleeSingle) && AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee multi debuff no effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    AttackStyle4 == AIAttackStyle.MeleeMulti && AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Melee multi debuff with effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    AttackStyle4 == AIAttackStyle.MeleeMulti && AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range debuff no effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    (AttackStyle4 == AIAttackStyle.Default || AttackStyle4 == AIAttackStyle.RangeSingle) && AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Range debuff wih effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    (AttackStyle4 == AIAttackStyle.Default || AttackStyle4 == AIAttackStyle.RangeSingle) && AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi debuff no effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    AttackStyle4 == AIAttackStyle.RangeMulti && AttackEffect4 == AIAttackEffect.None)
            {

            }
            //  Range multi debuff wih effect
            else if (AttackType4 == AIAttackType.Debuff &&
                    AttackStyle4 == AIAttackStyle.RangeMulti && AttackEffect4 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect4, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
        }

        public void Attack5()
        {
            //  Normal hit no effect
            if ((AttackType5 == AIAttackType.Default || AttackType5 == AIAttackType.Melee) &&
                (AttackStyle5 == AIAttackStyle.Default || AttackStyle5 == AIAttackStyle.MeleeSingle) &&
                AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Normal hit with effect
            else if ((AttackType5 == AIAttackType.Default || AttackType5 == AIAttackType.Melee) &&
                    (AttackStyle5 == AIAttackStyle.Default || AttackStyle5 == AIAttackStyle.MeleeSingle) &&
                    AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee Multi hit without effect
            else if ((AttackType5 == AIAttackType.Default || AttackType5 == AIAttackType.Melee) &&
                    AttackStyle5 == AIAttackStyle.MeleeMulti && AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Melee Multi hit with effect
            else if ((AttackType5 == AIAttackType.Default || AttackType5 == AIAttackType.Melee) &&
                    AttackStyle5 == AIAttackStyle.MeleeMulti && AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range normal no effect
            else if (AttackType5 == AIAttackType.Range &&
                    (AttackStyle5 == AIAttackStyle.RangeSingle || AttackStyle5 == AIAttackStyle.Default) &&
                    AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Range normal with effect
            else if (AttackType5 == AIAttackType.Range &&
                    (AttackStyle5 == AIAttackStyle.RangeSingle || AttackStyle5 == AIAttackStyle.Default) &&
                    AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi no effect
            else if (AttackType5 == AIAttackType.Range &&
                    AttackStyle5 == AIAttackStyle.RangeMulti && AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Range multi with effect
            else if (AttackType5 == AIAttackType.Range &&
                    AttackStyle5 == AIAttackStyle.RangeMulti && AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee debuff no effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    (AttackStyle5 == AIAttackStyle.Default || AttackStyle5 == AIAttackStyle.MeleeSingle) && AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Melee debuff with effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    (AttackStyle5 == AIAttackStyle.Default || AttackStyle5 == AIAttackStyle.MeleeSingle) && AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Melee multi debuff no effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    AttackStyle5 == AIAttackStyle.MeleeMulti && AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Melee multi debuff with effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    AttackStyle5 == AIAttackStyle.MeleeMulti && AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range debuff no effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    (AttackStyle5 == AIAttackStyle.Default || AttackStyle5 == AIAttackStyle.RangeSingle) && AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Range debuff wih effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    (AttackStyle5 == AIAttackStyle.Default || AttackStyle5 == AIAttackStyle.RangeSingle) && AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
            //  Range multi debuff no effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    AttackStyle5 == AIAttackStyle.RangeMulti && AttackEffect5 == AIAttackEffect.None)
            {

            }
            //  Range multi debuff wih effect
            else if (AttackType5 == AIAttackType.Debuff &&
                    AttackStyle5 == AIAttackStyle.RangeMulti && AttackEffect5 != AIAttackEffect.None)
            {
                Broadcast(new S.CustomAIEffect { ObjectID = ObjectID, Effect = AttackEffect5, TargetID = ObjectID, DelayTime = 100, Direction = Direction, Duration = 0 });
            }
        }

        protected override void ProcessTarget()
        {
            if (Dead) return;

            if (Target == null || Target.Dead)
            {
                //  Process the target priority here
                FindTarget();
            }

            if (Target != null)
            {

                switch (numberOfAttacks)
                {
                    case 0:
                        break;
                    case 1:
                        if (Envir.Time > attack1Time)
                        {
                            attack1Time = Envir.Time + Attack1Time;
                        }
                        ActionTime = Envir.Time + 300;
                        return;
                    case 2:
                        if (Envir.Time > attack2Time)
                        {
                            attack2Time = Envir.Time + Attack2Time;
                        }
                        else if (Envir.Time > attack1Time)
                        {
                            attack1Time = Envir.Time + Attack1Time;
                        }
                        ActionTime = Envir.Time + 300;
                        return;
                    case 3:
                        if (Envir.Time > attack3Time)
                        {
                            attack3Time = Envir.Time + Attack3Time;
                        }
                        else if (Envir.Time > attack2Time)
                        {
                            attack2Time = Envir.Time + Attack2Time;
                        }
                        else if (Envir.Time > attack1Time)
                        {
                            attack1Time = Envir.Time + Attack1Time;
                        }
                        ActionTime = Envir.Time + 300;
                        return;
                    case 4:
                        if (Envir.Time > attack4Time)
                        {
                            attack4Time = Envir.Time + Attack4Time;
                        }
                        else if (Envir.Time > attack3Time)
                        {
                            attack3Time = Envir.Time + Attack3Time;
                        }
                        else if (Envir.Time > attack2Time)
                        {
                            attack2Time = Envir.Time + Attack2Time;
                        }
                        else if (Envir.Time > attack1Time)
                        {
                            attack1Time = Envir.Time + Attack1Time;
                        }
                        ActionTime = Envir.Time + 300;
                        return;
                    case 5:
                        if (Envir.Time > attack5Time)
                        {
                            attack5Time = Envir.Time + Attack5Time;
                        }
                        else if (Envir.Time > attack4Time)
                        {
                            attack4Time = Envir.Time + Attack4Time;
                        }
                        else if (Envir.Time > attack3Time)
                        {
                            attack3Time = Envir.Time + Attack3Time;
                        }
                        else if (Envir.Time > attack2Time)
                        {
                            attack2Time = Envir.Time + Attack2Time;
                        }
                        else if (Envir.Time > attack1Time)
                        {
                            attack1Time = Envir.Time + Attack1Time;
                        }
                        ActionTime = Envir.Time + 300;
                        return;
                    default:
                        break;
                }
            }

            base.ProcessTarget();
        }


    }
}
