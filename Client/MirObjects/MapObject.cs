using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirScenes;
using Client.MirSounds;
using Client.MirScenes.Dialogs;

namespace Client.MirObjects
{
    public abstract class MapObject
    {
        public static Font ChatFont = new Font(Settings.FontName, 10F);
        public static List<MirLabel> LabelList = new List<MirLabel>();

        public static UserObject User;
        public static MapObject MouseObject, TargetObject, MagicObject;
        public abstract ObjectType Race { get; }
        public abstract bool Blocking { get; }

        public uint ObjectID;
        public string Name = string.Empty;
        
        public Point CurrentLocation, MapLocation;
        public MirDirection Direction;
        public bool Dead, Hidden, SitDown, Sneaking;
        public PoisonType Poison;
        public long DeadTime;
        public byte AI;
        public bool InTrapRock;
        public bool IsHero = false;

        public bool Blend = true;

        public uint TradeGoldAmount;

        public byte PercentHealth;
        public sbyte ManaPercent = -1;
        public long HealthTime;

        public List<QueuedAction> ActionFeed = new List<QueuedAction>();
        public QueuedAction NextAction
        {
            get { return ActionFeed.Count > 0 ? ActionFeed[0] : null; }
        }

        public List<Effect> Effects = new List<Effect>();
        public List<BuffType> Buffs = new List<BuffType>();

        public MLibrary BodyLibrary;
        public Color DrawColour = Color.White, NameColour = Color.White, OutLine_Colour = Color.Black;
        public MirLabel NameLabel, ChatLabel, GuildLabel, BossLabel, BossNameLabel;
        public long ChatTime;
        public int DrawFrame, DrawWingFrame, DrawBaseFrame;
        public Point DrawLocation, Movement, FinalDrawLocation, OffSetMove;
        public Rectangle DisplayRectangle;
        public int Light, DrawY;
        public long OrigNextMotion, OrigNextMotion2, OrigNextMotion3;
        public long NextMotion, NextMotion2, NextMotion3;
        public MirAction CurrentAction;
        public bool SkipFrames;

        public Color LightColour
        {
            get
            {

                if (Race == ObjectType.Monster && Settings.MobLightEffect)
                {
                    var monsterOb = (MonsterObject)this;

                    return Color.FromName(monsterOb.GlowAura);
                }

                return Color.White;
            }
        }

        public byte LightEffect;
        
        //Sound
        public int StruckWeapon;

        public MirLabel TempLabel, QuestLabel, EventLabel;

        public static List<MirLabel> DamageLabelList = new List<MirLabel>();
        public List<Damage> Damages = new List<Damage>();

        protected Point GlobalDisplayLocationOffset
        {
            get { return new Point(0, 0); }
        }

        protected MapObject(uint objectID)
        {
            ObjectID = objectID;

            for (int i = MapControl.Objects.Count - 1; i >= 0; i--)
            {
                MapObject ob = MapControl.Objects[i];
                if (ob.ObjectID != ObjectID) continue;
                ob.Remove();
            }

            MapControl.Objects.Add(this);
        }
        public void Remove()
        {
            if (MouseObject == this) MouseObject = null;
            if (TargetObject == this) TargetObject = null;
            if (MagicObject == this) MagicObject = null;

            if (this == User.NextMagicObject)
                User.ClearMagic();

            MapControl.Objects.Remove(this);
            GameScene.Scene.MapControl.RemoveObject(this);

            if (ObjectID != GameScene.NPCID) return;

            GameScene.NPCID = 0;
            GameScene.Scene.NPCDialog.Hide();
        }

        public abstract void Process();
        public abstract void Draw();
        public abstract bool MouseOver(Point p);

        public void AddBuffEffect(BuffType type)
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                if (!(Effects[i] is BuffEffect)) continue;
                if (((BuffEffect)(Effects[i])).BuffType == type) return;
            }

            PlayerObject ob = null;
            
            if (Race == ObjectType.Player ||
                Race == ObjectType.Hero)
            {
                ob = (PlayerObject)this;
            }

            switch (type)
            {
                case BuffType.Fury:
                    Effects.Add(new BuffEffect(Libraries.Magic3, 190, 7, 1400, this, true, type) { Repeat = true });
                    break;
                case BuffType.ImmortalSkin:
                    Effects.Add(new BuffEffect(Libraries.Magic3, 570, 5, 1400, this, true, type) { Repeat = true });
                    break;
                case BuffType.SwiftFeet:
                    if (ob != null) ob.Sprint = true;
                    break;
                case BuffType.FastMove:
                    if (ob != null) ob.FastChannel = true;
                    break;
                case BuffType.MoonLight:
                case BuffType.DarkBody:
                    if (ob != null) ob.Sneaking = true;
                    break;
                case BuffType.VampireShot:
                    Effects.Add(new BuffEffect(Libraries.Magic3, 2110, 6, 1400, this, true, type) { Repeat = false });
                    break;
                case BuffType.PoisonShot:
                    Effects.Add(new BuffEffect(Libraries.Magic3, 2310, 7, 1400, this, true, type) { Repeat = false });
                    break;
                case BuffType.HeroEnergyShield:
                case BuffType.EnergyShield:
                    
                    BuffEffect effect;

                    Effects.Add(effect = new BuffEffect(Libraries.Magic2, 1880, 9, 900, this, true, type) { Repeat = false });
                    SoundManager.PlaySound(20000 + (ushort)Spell.EnergyShield * 10 + 0);

                    effect.Complete += (o, e) =>
                    {
                        Effects.Add(new BuffEffect(Libraries.Magic2, 1900, 2, 800, this, true, type) { Repeat = true });
                    };
                    break;
                case BuffType.MagicBooster:
                    Effects.Add(new BuffEffect(Libraries.Magic3, 90, 6, 1200, this, true, type) { Repeat = true });
                    break;
                case BuffType.PetEnhancer:
                    Effects.Add(new BuffEffect(Libraries.Magic3, 230, 6, 1200, this, true, type) { Repeat = true });
                    break;
            }
        }
        public void RemoveBuffEffect(BuffType type)
        {
            PlayerObject ob = null;

            if (Race == ObjectType.Player ||
                Race == ObjectType.Hero)
            {
                ob = (PlayerObject)this;
            }

            for (int i = 0; i < Effects.Count; i++)
            {
                if (!(Effects[i] is BuffEffect)) continue;
                if (((BuffEffect)(Effects[i])).BuffType != type) continue;
                Effects[i].Repeat = false;
            }

            switch (type)
            {
                case BuffType.SwiftFeet:
                    if (ob != null) ob.Sprint = false;
                    break;
                case BuffType.FastMove:
                    if (ob != null) ob.FastChannel = false;
                    break;
                case BuffType.MoonLight:
                case BuffType.DarkBody:
                    if (ob != null) ob.Sneaking = false;
                    break;
            }
        }

        public void Chat(string text)
        {
            if (ChatLabel != null && !ChatLabel.IsDisposed)
            {
                ChatLabel.Dispose();
                ChatLabel = null;
            }

            const int chatWidth = 200;
            List<string> chat = new List<string>();

            string[] output = text.Split('(', ')');
            foreach (var o in output)
            {
                if (!uint.TryParse(o, out uint id)) continue;
                var item = GameScene.ItemInfoList.FirstOrDefault(x => x.Index == id);
                if (item != null)
                {
                    text = Functions.ReplaceFirst(text, "(" + o + ")", " " + item.FriendlyName + " ");

                }
            }

            int index = 0;
            for (int i = 1; i < text.Length; i++)
                if (TextRenderer.MeasureText(CMain.Graphics, text.Substring(index, i - index), ChatFont).Width > chatWidth)
                {
                    chat.Add(text.Substring(index, i - index - 1));
                    index = i - 1;
                }
            chat.Add(text.Substring(index, text.Length - index));

            text = chat[0];
            for (int i = 1; i < chat.Count; i++)
                text += string.Format("\n{0}", chat[i]);

            ChatLabel = new MirLabel
            {
                AutoSize = true,
                BackColour = Color.Transparent,
                ForeColour = Color.White,
                OutLine = true,
                OutLineColour = Color.Black,
                DrawFormat = TextFormatFlags.HorizontalCenter,
                Text = text,
            };
            ChatTime = CMain.Time + 5000;
        }
        public virtual void DrawChat()
        {
            if (ChatLabel == null || ChatLabel.IsDisposed) return;

            if (CMain.Time > ChatTime)
            {
                ChatLabel.Dispose();
                ChatLabel = null;
                return;
            }

            ChatLabel.ForeColour = Dead ? Color.Gray : Color.White;
            ChatLabel.Location = new Point(DisplayRectangle.X + (48 - ChatLabel.Size.Width) / 2, DisplayRectangle.Y - (60 + ChatLabel.Size.Height) - (Dead ? 35 : 0));
            ChatLabel.Draw();
        }

        public virtual void CreateLabel()
        {
            NameLabel = null;
            MonsterObject tmp = null;
            if (Race == ObjectType.Monster)
            {
                tmp = (MonsterObject)this;
                if (!tmp.IsBoss)
                    tmp = null;

            }
            bool created = false;
            for (int i = 0; i < LabelList.Count; i++)
            {
                if (tmp != null)
                {
                    //  Elite Boss
                    if (tmp.IsElite)
                    {
                        //  Not the same name
                        if (LabelList[i].Text != Name ||
                            //  Text color is not white
                            LabelList[i].ForeColour != Color.White ||
                            //  Outline color is not the same as Name color
                            LabelList[i].OutLineColour != NameColour)
                            //  Discontinue
                            continue;
                        //  It's the same (I.E Created)
                        NameLabel = LabelList[i];
                    }
                    //  None Elite Boss
                    else
                    {
                        //  Not the same name
                        if (LabelList[i].Text != Name ||
                            //  Text color is not white
                            LabelList[i].ForeColour != Color.White ||
                            //  Outline color is not Red
                            LabelList[i].OutLineColour != Color.Red)
                            //  Discontinue
                            continue;
                        //  It's the same (I.E Created)
                        NameLabel = LabelList[i];
                    }
                }
                //  It wont be a boss label
                else
                {
                    if (LabelList[i].Text != Name ||
                        LabelList[i].ForeColour != NameColour) continue;
                    //  It's the same (I.E Created)
                    NameLabel = LabelList[i];
                }

                if (LabelList[i].Text == Name)
                    created = true;
            }


            if (created)
                return;
            //  Label isn't null
            if (NameLabel != null && 
            //  Label isn't disposed
                !NameLabel.IsDisposed)
            //  Already created, don't need to create any more!
                return;

            //  The label is valid now check if it by object type and their values
            if (tmp != null)
            {                
                //  It is a boss
                if (tmp.IsBoss &&
                    !tmp.IsSub &&
                    !tmp.IsPet)
                {
                    //  Elite Boss
                    if (tmp.IsElite)
                    {
                        NameLabel = new MirLabel
                        {
                            AutoSize = true,
                            BackColour = Color.Transparent,
                            ForeColour = Color.White,
                            OutLine = true,
                            OutLineColour = tmp.NameColour,
                            Text = Name,
                        };
                    }
                    //  Boss Non Elite
                    else
                    {
                        NameLabel = new MirLabel
                        {
                            AutoSize = true,
                            BackColour = Color.Transparent,
                            ForeColour = Color.White,
                            OutLine = true,
                            OutLineColour = Color.Red,
                            Text = Name,
                        };
                    }
                }
                //  Mob
                else
                {
                    NameLabel = new MirLabel
                    {
                        AutoSize = true,
                        BackColour = Color.Transparent,
                        ForeColour = NameColour,
                        OutLine = true,
                        OutLineColour = Color.Black,
                        Text = Name,
                    };
                }
            }
            //  Other
            else
            {
                NameLabel = new MirLabel
                {
                    AutoSize = true,
                    BackColour = Color.Transparent,
                    ForeColour = NameColour,
                    OutLine = true,
                    OutLineColour = OutLine_Colour,
                    Text = Name,
                };
            }
            NameLabel.Disposing += (o, e) => LabelList.Remove(NameLabel);
            LabelList.Add(NameLabel);
        }
        public virtual void DrawName()
        {
            CreateLabel();

            if (NameLabel == null) return;
            
            NameLabel.Text = Name;
            NameLabel.Location = new Point(DisplayRectangle.X + (50 - NameLabel.Size.Width) / 2, DisplayRectangle.Y - (32 - NameLabel.Size.Height / 2) + (Dead ? 35 : 8)); //was 48 -
            NameLabel.Draw();
        }
        public virtual void DrawBlend()
        {
            DXManager.SetBlend(true, 0.3F); //0.8
            Draw();
            DXManager.SetBlend(false);
        }
        public void DrawDamages()
        {
            for (int i = Damages.Count - 1; i >= 0; i--)
            {
                Damage info = Damages[i];
                if (CMain.Time > info.ExpireTime)
                {
                    Damages.RemoveAt(i);
                }
                else
                {
                    info.Draw(DisplayRectangle.Location);
                }
            }
        }

        public void DrawBossName()
        {
            //  Create the Label
            CreateBossLabel();
            //  If the label wasn't created we won't draw it
            if (BossLabel == null)
                return;

            //  Apply Text to the Label
            BossLabel.Text = string.Format("{0} - {1}%", Name, PercentHealth);
            //  Set the location of the Label
            BossLabel.Location = new Point(Settings.ScreenWidth / 2 - BossLabel.Size.Width / 2, 53); //was 48 -
            //  Draw the Label
            BossLabel.Draw();
        }

        public void CreateBossLabel()
        {
            BossLabel = null;

            for (int i = 0; i < LabelList.Count; i++)
            {
                //  Not the same name
                if (LabelList[i].Text.Contains(string.Format("{0}", Name)) || 
                //  Not the same name color
                    LabelList[i].ForeColour != NameColour)
                    continue;
                BossLabel = LabelList[i];
                break;
            }


            if (BossLabel != null && !BossLabel.IsDisposed)
                return;

            BossLabel = new MirLabel
            {
                AutoSize = true,
                BackColour = Color.Transparent,
                ForeColour = NameColour,
                OutLine = true,
                OutLineColour = Color.Black,
                Text = string.Format("{0} - {1}%", Name, PercentHealth),
            };
            BossLabel.Disposing += (o, e) => LabelList.Remove(BossLabel);
            LabelList.Add(BossLabel);
        }

        public void DrawBossHealthBar()
        {
            
            string name = Name;
            if (Name.Contains("("))
                name = Name.Substring(Name.IndexOf("(") + 1, Name.Length - Name.IndexOf("(") - 2);

            //  Object is dead, don't draw
            if (Dead)
                return;
            //  Object isn't a monster!
            if (Race != ObjectType.Monster)
                return;

            //  Check the Rev time (may not even need this)
            if (CMain.Time >= HealthTime)
            {
                if (Race == ObjectType.Monster)
                {
                    MonsterObject temp = (MonsterObject)this;
                    if (!temp.IsBoss)
                        return;

                }
            }
            //  Draw the base of the Image (I.E Empty health bar)
            Libraries.EdensEliteInter.Draw(1, new Rectangle(0, 0, 316, 33), new Point(Settings.ScreenWidth / 2 - 158, 33 + 10), Color.White, false);
            //  Draw the Health ontop of the base and shirnk it based on the Health of the object
            Libraries.EdensEliteInter.Draw(0, new Rectangle(0, 0, (int)(316 * PercentHealth / 100F), 33), new Point(Settings.ScreenWidth / 2 - 158, 33 + 10), Color.White, false);
            //  Now Draw the bosses name ontop of the Health Bar
            DrawBossName();
        }

        public void DrawHealth()
        {
            string name = Name;

            if (Name.Contains("(")) name = Name.Substring(Name.IndexOf("(") + 1, Name.Length - Name.IndexOf("(") - 2);

            if (Dead) return;
            if (Race != ObjectType.Player && Race != ObjectType.Monster && Race != ObjectType.Hero) return;

            if (CMain.Time >= HealthTime)
            {
                if (Race == ObjectType.Monster && !Name.EndsWith(string.Format("({0})", User.Name)) && !GroupDialog.GroupList.Contains(name)) return;
                if (Race == ObjectType.Player && this != User && !GroupDialog.GroupList.Contains(Name)) return;
                if (this == User && GroupDialog.GroupList.Count == 0) return;
            }


            Libraries.Prguse2.Draw(0, DisplayRectangle.X + 8, DisplayRectangle.Y - 64);
            int index = 1;

            switch (Race)
            {
                case ObjectType.Player:
                    if (GroupDialog.GroupList.Contains(name)) index = 10;
                    break;
                case ObjectType.Monster:
                case ObjectType.Hero:
                    if (GroupDialog.GroupList.Contains(name) || name == User.Name) index = 11;
                    break;
            }

            Libraries.Prguse2.Draw(index, new Rectangle(0, 0, (int)(32 * PercentHealth / 100F), 4), new Point(DisplayRectangle.X + 8, DisplayRectangle.Y - 64), Color.White, false);
            if (ManaPercent != -1)
            {
                Libraries.Prguse2.Draw(0, DisplayRectangle.X + 8, DisplayRectangle.Y - 60);
                Libraries.Prguse2.Draw(10, new Rectangle(0, 0, (int)(32 * ManaPercent / 100F), 4), new Point(DisplayRectangle.X + 8, DisplayRectangle.Y - 60), Color.White, false);
            }
            }

        public void DrawPoison()
        {
            byte poisoncount = 0;
            if (Poison != PoisonType.None)
            {
                if (Poison.HasFlag(PoisonType.Green))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.Green);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.Red))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.Red);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.Burning))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.OrangeRed);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.Bleeding))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.DarkRed);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.Slow))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.Purple);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.Stun))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.Yellow);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.Frozen))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.Blue);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.Paralysis) || Poison.HasFlag(PoisonType.LRParalysis))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.Gray);
                    poisoncount++;
                }
                if (Poison.HasFlag(PoisonType.DelayedExplosion))
                {
                    DXManager.Sprite.Draw2D(DXManager.PoisonDotBackground, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 7 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 21)), Color.Black);
                    DXManager.Sprite.Draw2D(DXManager.RadarTexture, Point.Empty, 0, new PointF((int)(DisplayRectangle.X + 8 + (poisoncount * 3)), (int)(DisplayRectangle.Y - 20)), Color.Orange);
                    poisoncount++;
                }
            }
        }

        public abstract void DrawBehindEffects(bool effectsEnabled);

        public abstract void DrawEffects(bool effectsEnabled);

    }

}
