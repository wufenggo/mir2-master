using System.IO;
using System;
using Client.MirSounds;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Client
{

    public enum KeybindOptions : int
    {
        None,
        Bar1Skill1,
        Bar1Skill2,
        Bar1Skill3,
        Bar1Skill4,
        Bar1Skill5,
        Bar1Skill6,
        Bar1Skill7,
        Bar1Skill8,
        Bar2Skill1,
        Bar2Skill2,
        Bar2Skill3,
        Bar2Skill4,
        Bar2Skill5,
        Bar2Skill6,
        Bar2Skill7,
        Bar2Skill8,
        Inventory,
        Inventory2,
        KeySettings,
        Equipment,
        Equipment2,
        Skills,
        Skills2,
        Creature,
        MountWindow,
        Mount,
        Fishing,
        Skillbar,
        Mentor,
        Relationship,
        Friends,
        Guilds,
        GameShop,
        Quests,
        Closeall,
        Options,
        Group,
        Belt,
        Pickup,
        Belt1,
        Belt1Alt,
        Belt2,
        Belt2Alt,
        Belt3,
        Belt3Alt,
        Belt4,
        Belt4Alt,
        Belt5,
        Belt5Alt,
        Belt6,
        Belt6Alt,
        Logout,
        Exit,
        CreaturePickup,
        CreatureAutoPickup,
        Minimap,
        Bigmap,
        Trade,
        ChangeAttackmode,
        AttackmodePeace,
        AttackmodeGroup,
        AttackmodeGuild,
        AttackmodeEnemyguild,
        AttackmodeRedbrown,
        AttackmodeAll,
        ChangePetmode,
        PetmodeBoth,
        PetmodeMoveonly,
        PetmodeAttackonly,
        PetmodeNone,
        Help,
        Autorun,
        SummonHero,
        Cameramode,
        Screenshot,
        DropView,
        TargetDead,
        Ranking,
        AddGroupMember,
        Crafting,
        Rental,
        SkillTilde,
        Mail,
        HeroEquipment,
        HeroInventory,
        HeroSkills,
        HeroBelt,
        HeroBeltFlip,
        HeroBelt1,
        HeroBelt2,
        HeroBelt1Alt,
        HeroBelt2Alt,
        Bar3Skill1,
        Bar3Skill2,
        Bar3Skill3,
        Bar3Skill4,
        Bar3Skill5,
        Bar3Skill6,
        Bar3Skill7,
        Bar3Skill8,

    }

    public class KeyBind
    {
        public string Text;

        public Keys Key = 0;

        public KeybindOptions function = KeybindOptions.None;

        private KeyInfo _defaultKey;

        private KeyInfo _customkey;

        public KeyInfo DefaultKey
        {
            get
            {
                return _defaultKey;
            }
            set
            {
                _defaultKey = value;
                bool flag = _customkey == null;
                if (flag)
                {
                    _customkey = new KeyInfo(_defaultKey);
                }
            }
        }

        public KeyInfo CutomKey
        {
            get
            {
                bool flag = _customkey == null;
                if (flag)
                {
                    _customkey = new KeyInfo(_defaultKey);
                }
                return _customkey;
            }
            set
            {
                _customkey = value;
            }
        }

        public bool CanOverlap
        {
            get
            {
                KeybindOptions keybindOptions = function;
                bool result;
                switch (keybindOptions)
                {
                    case KeybindOptions.Bar1Skill1:
                    case KeybindOptions.Bar1Skill2:
                    case KeybindOptions.Bar1Skill3:
                    case KeybindOptions.Bar1Skill4:
                    case KeybindOptions.Bar1Skill5:
                    case KeybindOptions.Bar1Skill6:
                    case KeybindOptions.Bar1Skill7:
                    case KeybindOptions.Bar1Skill8:
                    case KeybindOptions.Bar2Skill1:
                    case KeybindOptions.Bar2Skill2:
                    case KeybindOptions.Bar2Skill3:
                    case KeybindOptions.Bar2Skill4:
                    case KeybindOptions.Bar2Skill5:
                    case KeybindOptions.Bar2Skill6:
                    case KeybindOptions.Bar2Skill7:
                    case KeybindOptions.Bar2Skill8:
                        break;
                    default:
                        switch (keybindOptions)
                        {
                            case KeybindOptions.Belt1Alt:
                            case KeybindOptions.Belt2Alt:
                            case KeybindOptions.Belt3Alt:
                            case KeybindOptions.Belt4Alt:
                            case KeybindOptions.Belt5Alt:
                            case KeybindOptions.Belt6Alt:
                                goto IL_C0;
                            case KeybindOptions.Belt2:
                            case KeybindOptions.Belt3:
                            case KeybindOptions.Belt4:
                            case KeybindOptions.Belt5:
                            case KeybindOptions.Belt6:
                                break;
                            default:
                                switch (keybindOptions)
                                {
                                    case KeybindOptions.Bar3Skill1:
                                    case KeybindOptions.Bar3Skill2:
                                    case KeybindOptions.Bar3Skill3:
                                    case KeybindOptions.Bar3Skill4:
                                    case KeybindOptions.Bar3Skill5:
                                    case KeybindOptions.Bar3Skill6:
                                    case KeybindOptions.Bar3Skill7:
                                        goto IL_C0;
                                }
                                break;
                        }
                        result = false;
                        return result;
                }
                IL_C0:
                result = true;
                return result;
            }
        }
    }


    public class KeyBindSettings
    {
        private static InIReader Reader = new InIReader(".\\KeySetting.ini");

        public List<KeyBind> Keylist = new List<KeyBind>();

        public KeyBindSettings()
        {
            New();
            bool flag = !File.Exists(".\\KeySetting.ini");
            if (flag)
            {
                Save();
            }
            else
            {
                Load();
            }
        }

        public void Load()
        {
            foreach (KeyBind current in Keylist)
            {
                current.CutomKey.RequireAlt = KeyBindSettings.Reader.ReadByte(current.function.ToString(), "RequireAlt", current.CutomKey.RequireAlt);
                current.CutomKey.RequireShift = KeyBindSettings.Reader.ReadByte(current.function.ToString(), "RequireShift", current.CutomKey.RequireShift);
                current.CutomKey.RequireTilde = KeyBindSettings.Reader.ReadByte(current.function.ToString(), "RequireTilde", current.CutomKey.RequireTilde);
                current.CutomKey.RequireCtrl = KeyBindSettings.Reader.ReadByte(current.function.ToString(), "RequireCtrl", current.CutomKey.RequireCtrl);
                string value = KeyBindSettings.Reader.ReadString(current.function.ToString(), "RequireKey", current.CutomKey.Key.ToString());
                Enum.TryParse<Keys>(value, out current.CutomKey.Key);
            }
        }

        public void Save()
        {
            KeyBindSettings.Reader.Write("Guide", "01", "RequireAlt,RequireShift,RequireTilde,RequireCtrl");
            KeyBindSettings.Reader.Write("Guide", "02", "have 3 options: 0/1/2");
            KeyBindSettings.Reader.Write("Guide", "03", "0 < you cannot have this key pressed to use the function");
            KeyBindSettings.Reader.Write("Guide", "04", "1 < you have to have this key pressed to use this function");
            KeyBindSettings.Reader.Write("Guide", "05", "2 < it doesnt matter if you press this key to use this function");
            KeyBindSettings.Reader.Write("Guide", "06", "by default just use 2, unless you have 2 functions on the same key");
            KeyBindSettings.Reader.Write("Guide", "07", "example: change attack mode (ctrl+h) and help (h)");
            KeyBindSettings.Reader.Write("Guide", "08", "if you set either of those to requireshift 2, then they wil both work at the same time or not work");
            KeyBindSettings.Reader.Write("Guide", "09", "");
            KeyBindSettings.Reader.Write("Guide", "10", "To get the value for RequireKey look at:");
            KeyBindSettings.Reader.Write("Guide", "11", "https://msdn.microsoft.com/en-us/library/system.windows.forms.keys(v=vs.110).aspx");
            foreach (KeyBind current in this.Keylist)
            {
                KeyBindSettings.Reader.Write(current.function.ToString(), "RequireAlt", current.CutomKey.RequireAlt);
                KeyBindSettings.Reader.Write(current.function.ToString(), "RequireShift", current.CutomKey.RequireShift);
                KeyBindSettings.Reader.Write(current.function.ToString(), "RequireTilde", current.CutomKey.RequireTilde);
                KeyBindSettings.Reader.Write(current.function.ToString(), "RequireCtrl", current.CutomKey.RequireCtrl);
                KeyBindSettings.Reader.Write(current.function.ToString(), "RequireKey", current.CutomKey.Key.ToString());
            }
        }

        public void New()
        {
            KeyBind item = new KeyBind
            {
                Text = "Auto Run on/off",
                function = KeybindOptions.Autorun,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Attack mode switch key",
                function = KeybindOptions.ChangeAttackmode,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 1,
                    Key = Keys.H
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Summoner Break/Attack",
                function = KeybindOptions.ChangePetmode,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 1,
                    Key = Keys.A
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Riding",
                function = KeybindOptions.Mount,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D0
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Key settings",
                function = KeybindOptions.KeySettings,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.F12
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Mob Info Veiw, Alt and Point",
                function = KeybindOptions.TargetDead,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.Alt
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Mini-Map Open/close",
                function = KeybindOptions.Minimap,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.V
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "View full map",
                function = KeybindOptions.Bigmap,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.B
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Help Open/Close",
                function = KeybindOptions.Help,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.H
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Option Open/close",
                function = KeybindOptions.Options,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.O
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Drop Item Picking",
                function = KeybindOptions.Pickup,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Tab
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "View Drop Item",
                function = KeybindOptions.DropView,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Tab
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Change camera mode",
                function = KeybindOptions.Cameramode,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Insert
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Screenshot key",
                function = KeybindOptions.Screenshot,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Pause
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Close all menus",
                function = KeybindOptions.Closeall,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Escape
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Belt Open/Close",
                function = KeybindOptions.Belt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Z
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #1",
                function = KeybindOptions.Belt1,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D1
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #1",
                function = KeybindOptions.Belt1Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad1
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #2",
                function = KeybindOptions.Belt2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D2
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #2",
                function = KeybindOptions.Belt2Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad2
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #3",
                function = KeybindOptions.Belt3,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D3
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #3",
                function = KeybindOptions.Belt3Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad3
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #4",
                function = KeybindOptions.Belt4,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D4
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #4",
                function = KeybindOptions.Belt4Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad4
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #5",
                function = KeybindOptions.Belt5,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D5
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #5",
                function = KeybindOptions.Belt5Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad5
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #6",
                function = KeybindOptions.Belt6,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D6
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Use belt #6",
                function = KeybindOptions.Belt6Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad6
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill bar change",
                function = KeybindOptions.SkillTilde,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Oemtilde
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "View skill bar",
                function = KeybindOptions.Skillbar,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.R
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #1",
                function = KeybindOptions.Bar1Skill1,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F1
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #2",
                function = KeybindOptions.Bar1Skill2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F2
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #3",
                function = KeybindOptions.Bar1Skill3,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F3
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #4",
                function = KeybindOptions.Bar1Skill4,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F4
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #5",
                function = KeybindOptions.Bar1Skill5,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F5
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #6",
                function = KeybindOptions.Bar1Skill6,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F6
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #7",
                function = KeybindOptions.Bar1Skill7,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F7
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut #8",
                function = KeybindOptions.Bar1Skill8,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F8
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #1",
                function = KeybindOptions.Bar2Skill1,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F1
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #2",
                function = KeybindOptions.Bar2Skill2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F2
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #3",
                function = KeybindOptions.Bar2Skill3,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F3
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #4",
                function = KeybindOptions.Bar2Skill4,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F4
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #5",
                function = KeybindOptions.Bar2Skill5,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F5
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #6",
                function = KeybindOptions.Bar2Skill6,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F6
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #7",
                function = KeybindOptions.Bar2Skill7,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F7
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Shortcut2 #8",
                function = KeybindOptions.Bar2Skill8,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 1,
                    Key = Keys.F8
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Inventory Open/Close",
                function = KeybindOptions.Inventory,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.F9
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Inventory Open/Close2",
                function = KeybindOptions.Inventory2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.I
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Character Status Open/Close",
                function = KeybindOptions.Equipment,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.F10
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Character Status Open/Close2",
                function = KeybindOptions.Equipment2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.C
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Status Open/Close",
                function = KeybindOptions.Skills,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.F11
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Skill Status Open/Close2",
                function = KeybindOptions.Skills2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.S
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Quest Open/Close",
                function = KeybindOptions.Quests,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Q
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Crafting Open/Close",
                function = KeybindOptions.Crafting,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.U
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Mount Open/Close",
                function = KeybindOptions.MountWindow,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.J
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Fishing Open/Close",
                function = KeybindOptions.Fishing,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.N
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "GameShop Open/Close",
                function = KeybindOptions.GameShop,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Y
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Trade Menu Open/Close",
                function = KeybindOptions.Trade,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.T
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Ranking Menu Open/Close",
                function = KeybindOptions.Ranking,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.D9
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Group Menu Open/Close",
                function = KeybindOptions.Group,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.P
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Guild Menu Open/Close",
                function = KeybindOptions.Guilds,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.G
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Mentor Open/Close",
                function = KeybindOptions.Mentor,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.W
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Relationship Open/Close",
                function = KeybindOptions.Relationship,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.L
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Friends Open/Close",
                function = KeybindOptions.Friends,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.F
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Rental Open/Close",
                function = KeybindOptions.Rental,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.K
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Mail Open/Close",
                function = KeybindOptions.Mail,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.M
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Summon/UnSummon",
                function = KeybindOptions.SummonHero,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Scroll
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Bag",
                function = KeybindOptions.HeroInventory,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 1,
                    Key = Keys.I
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Character Menu",
                function = KeybindOptions.HeroEquipment,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 1,
                    Key = Keys.C
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] skill Menu",
                function = KeybindOptions.HeroSkills,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 1,
                    Key = Keys.S
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Belt Open/Close",
                function = KeybindOptions.HeroBelt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 1,
                    Key = Keys.Z
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Use belt #1",
                function = KeybindOptions.HeroBelt1,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.D7
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Use belt #1",
                function = KeybindOptions.HeroBelt1Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad7
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Use belt #2",
                function = KeybindOptions.HeroBelt2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.D8
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Use belt #2",
                function = KeybindOptions.HeroBelt2Alt,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 2,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.NumPad8
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Skill Shortcut #1",
                function = KeybindOptions.Bar3Skill1,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 1,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F1
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Skill Shortcut #2",
                function = KeybindOptions.Bar3Skill2,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 1,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F2
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Skill Shortcut #3",
                function = KeybindOptions.Bar3Skill3,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 1,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F3
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Skill Shortcut #4",
                function = KeybindOptions.Bar3Skill4,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 1,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F4
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Skill Shortcut #5",
                function = KeybindOptions.Bar3Skill5,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 1,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F5
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Skill Shortcut #6",
                function = KeybindOptions.Bar3Skill6,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 1,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F6
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "[Hero] Skill Shortcut #7",
                function = KeybindOptions.Bar3Skill7,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 2,
                    RequireShift = 1,
                    RequireTilde = 2,
                    RequireCtrl = 2,
                    Key = Keys.F7
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Log out",
                function = KeybindOptions.Logout,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 1,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.X
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Exit",
                function = KeybindOptions.Exit,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 1,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.Q
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Pet Menu Open/Close",
                function = KeybindOptions.Creature,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.E
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Automatic picking of pet items",
                function = KeybindOptions.CreatureAutoPickup,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 1,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.A
                }
            };
            Keylist.Add(item);
            item = new KeyBind
            {
                Text = "Pick up pet items",
                function = KeybindOptions.CreaturePickup,
                DefaultKey = new KeyInfo
                {
                    RequireAlt = 0,
                    RequireShift = 0,
                    RequireTilde = 0,
                    RequireCtrl = 0,
                    Key = Keys.A
                }
            };
            Keylist.Add(item);
        }

        public void ResetKey()
        {
            foreach (KeyBind current in Keylist)
            {
                current.CutomKey = new KeyInfo(current.DefaultKey);
            }
        }

        public KeyBind HasKey(KeyBind keyBind)
        {
            KeyBind result;
            foreach (KeyBind current in Keylist)
            {
                bool flag = keyBind == current;
                if (!flag)
                {
                    bool flag2 = current.CutomKey.ToString() == keyBind.CutomKey.ToString();
                    if (flag2)
                    {
                        result = current;
                        return result;
                    }
                }
            }
            result = null;
            return result;
        }

        public Keys GetKey(KeybindOptions op)
        {
            Keys result;
            foreach (KeyBind current in Keylist)
            {
                bool flag = current.function == op;
                if (flag)
                {
                    result = current.CutomKey.Key;
                    return result;
                }
            }
            result = Keys.None;
            return result;
        }
    }


}
