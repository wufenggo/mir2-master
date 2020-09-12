using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirSounds;
using Client.MirObjects;
using C = ClientPackets;
using S = ServerPackets;
using System.Threading;
using System.IO;

namespace Client.MirScenes
{
    public class SelectScene : MirScene
    {
        public MirImageControl Background;

        public MirImageControl Title;

        public MirImageControl Adorno;

        private NewCharacterDialog _character;

        public MirLabel ServerLabel;

        public MirAnimatedControl BackgroundSelect;

        public static MirAnimatedControl[] CharacterDisplay;

        public static MirAnimatedControl[] CharacterEfects;

        public MirButton StartGameButton;

        public MirButton NewCharacterButton;

        public MirButton DeleteCharacterButton;

        public MirButton CreditsButton;

        public MirButton ExitGame;

        public static CharacterButton[] CharacterButtons;

        public static List<SelectInfo> Characters = new List<SelectInfo>();

        private int _selected;

        private int IndexBack;
        public List<ItemInfo> itemInfos = new List<ItemInfo>();
        public bool _FileSaving = false;
        public DateTime fileTime = DateTime.Now;
        bool hasItemInfo = true;
        public SelectScene(List<SelectInfo> characters)
        {
            SoundManager.PlaySound(SoundList.SelectMusic, true);
            Disposing += (o, e) => SoundManager.StopSound(SoundList.SelectMusic);


            Characters = characters;
            SortList();
            int c = 0;
            KeyPress += SelectScene_KeyPress;
            IndexBack = 3;
            Background = new MirImageControl
            {
                Index = IndexBack,
                Library = Libraries.CustomLoginScene,
                Parent = this
            };
            Characters = characters;
            SortList();
            KeyPress += new KeyPressEventHandler(SelectScene_KeyPress);
            BackgroundSelect = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 1,
                AnimationDelay = 100L,
                Index = IndexBack,
                Library = Libraries.CustomLoginScene,
                Loop = true,
                Parent = Background
            };
            BackgroundSelect.AfterDraw += (o, e) =>
            {
                Libraries.CustomLoginScene.DrawBlend(c + 340, new Point(507, 287), Color.White, true, 0.5f);
                c++;
                bool flag = c == 40;
                if (flag)
                {
                    c = 0;
                }
            };
            BackgroundSelect.AfterDraw += (o, e) =>
            {
                Libraries.CustomLoginScene.DrawBlend(c + 390, new Point(554, 287), Color.White, true, 0.5f);
            };
            BackgroundSelect.AfterDraw += (o, e) =>
            {
                Libraries.CustomLoginScene.DrawBlend(c + 210, new Point(678, 310), Color.White, true, 0.5f);
            };
            BackgroundSelect.AfterDraw += (o, e) =>
            {
                Libraries.CustomLoginScene.DrawBlend(c + 260, new Point(338, 310), Color.White, true, 0.5f);
            };
            BackgroundSelect.AfterDraw += (o, e) =>
            {
                Libraries.CustomLoginScene.DrawBlend(c + 440, new Point(512, 385), Color.White, true, 0.6f);
            };
            BackgroundSelect.AfterDraw += (o, e) =>
            {
                Libraries.CustomLoginScene.DrawBlend(c + 490, new Point(515, 385), Color.White, true, 0.6f);
            };
            Adorno = new MirImageControl
            {
                Index = 48,
                Enabled = false,
                Location = new Point(3, 703),
                Library = Libraries.CustomButtons,
                Parent = Background
            };
            StartGameButton = new MirButton
            {
                Enabled = false,
                Size = new Size(140, 36),
                HoverIndex = 37,
                Index = 36,
                Library = Libraries.CustomButtons,
                Location = new Point(440, 708),
                Parent = Background,
                PressedIndex = 38,
                GrayScale = false
            };
            StartGameButton.Click += (o, e) =>
            {
                StartGame();
            };
            NewCharacterButton = new MirButton
            {
                HoverIndex = 29,
                Index = 28,
                Library = Libraries.CustomButtons,
                Location = new Point(55, 717),
                Parent = Background,
                PressedIndex = 30
            };
            NewCharacterButton.Click += (o, e) =>
            {
                _character = new NewCharacterDialog
                {
                    Parent = this
                };
                for (int i = 0; i < Characters.Count; i++)
                {
                    CharacterDisplay[i].Visible = false;
                    CharacterEfects[i].Visible = false;
                    CharacterButtons[i].Visible = false;
                }
            };
            DeleteCharacterButton = new MirButton
            {
                HoverIndex = 45,
                Index = 44,
                Library = Libraries.CustomButtons,
                Location = new Point(245, 717),
                Parent = Background,
                PressedIndex = 47
            };
            DeleteCharacterButton.Click += (o, e) =>
            {
                DeleteCharacter();
            };
            ExitGame = new MirButton
            {
                HoverIndex = 19,
                Index = 18,
                Library = Libraries.CustomButtons,
                Location = new Point(835, 717),
                Parent = Background,
                PressedIndex = 20
            };
            ExitGame.Click += (o, e) => Program.Form.Close();

            CharacterDisplay = new MirAnimatedControl[3];
            CharacterEfects = new MirAnimatedControl[3];
            CharacterButtons = new CharacterButton[3];
            CharacterDisplay[0] = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 100L,
                FadeIn = true,
                FadeInDelay = 75L,
                FadeInRate = 0.1f,
                Index = 220,
                Library = Libraries.CustomCharsel,
                Location = new Point(510, 298),
                Parent = Background,
                UseOffSet = true,
                Visible = false
            };
            CharacterDisplay[0].Click += (o, e) =>
            {
                _selected = 0;
                UpdateInterface();
            };
            CharacterEfects[0] = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 100L,
                FadeIn = true,
                FadeInDelay = 75L,
                FadeInRate = 0.1f,
                Index = 220,
                Library = Libraries.CustomCharsel,
                Location = new Point(510, 298),
                Parent = Background,
                UseOffSet = true,
                Visible = false,
                NotControl = true
            };
            CharacterButtons[0] = new CharacterButton
            {
                Location = new Point(428, 150),
                Parent = Background,
                Size = new Size(171, 342),
                Sound = SoundList.ButtonA,
                Visible = true,
            };
            CharacterButtons[0].Click += (o, e) =>
            {
                

            };
            CharacterDisplay[1] = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 100L,
                FadeIn = true,
                FadeInDelay = 75L,
                FadeInRate = 0.1f,
                Index = 220,
                Library = Libraries.CustomCharsel,
                Location = new Point(692, 397),
                Parent = Background,
                UseOffSet = true,
                Visible = false
            };
            CharacterDisplay[1].Click += (o, e) =>
            {
                _selected = 1;
                UpdateInterface();
            };
            CharacterEfects[1] = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 100L,
                FadeIn = true,
                FadeInDelay = 75L,
                FadeInRate = 0.1f,
                Index = 220,
                Library = Libraries.CustomCharsel,
                Location = new Point(692, 397),
                Parent = Background,
                UseOffSet = true,
                Visible = false,
                NotControl = true
            };
            CharacterButtons[1] = new CharacterButton
            {
                Location = new Point(605, 250),
                Parent = Background,
                Sound = SoundList.ButtonA,
                Size = new Size(171, 342),
                Visible = true,
            };
            CharacterButtons[1].Click += (o, e) =>
            {

            };
            CharacterDisplay[2] = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 100L,
                FadeIn = true,
                FadeInDelay = 75L,
                FadeInRate = 0.1f,
                Index = 220,
                Library = Libraries.CustomCharsel,
                Location = new Point(338, 397),
                Parent = Background,
                UseOffSet = true,
                Visible = false
            };
            CharacterDisplay[2].Click += (o, e) =>
            {
                _selected = 2;
                UpdateInterface();
            };
            CharacterEfects[2] = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 100L,
                FadeIn = true,
                FadeInDelay = 75L,
                FadeInRate = 0.1f,
                Index = 220,
                Library = Libraries.CustomCharsel,
                Location = new Point(338, 397),
                Parent = Background,
                UseOffSet = true,
                Visible = false,
                NotControl = true
            };
            CharacterButtons[2] = new CharacterButton
            {
                Location = new Point(250, 250),
                Parent = Background,
                Sound = SoundList.ButtonA,
                Size = new Size(171, 342),
                Visible = true,
            };
            CharacterButtons[2].Click += (o, e) =>
            {

            };
            UpdateInterfaceEfect();
            UpdateInterfaceEfect1();
            UpdateInterfaceEfect2();
            UpdateCharacterInterface();
            if (File.Exists(@"./ItemInfo.dat"))
            {
                if (itemInfos == null ||
                    itemInfos.Count == 0)
                {
                    using (FileStream stream = File.OpenRead(@"./ItemInfo.dat"))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            fileTime = DateTime.FromBinary(reader.ReadInt64());
                            int count = reader.ReadInt32();
                            if (count > 0)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    itemInfos.Add(new ItemInfo(reader));
                                }
                            }
                        }
                    }
                }
            }
            else
                hasItemInfo = false;
            UpdateInterface();
        }

        private void SelectScene_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter) return;
            if (StartGameButton.Enabled)
                StartGame();
            e.Handled = true;
        }


        public void SortList()
        {
            if (Characters != null)
                Characters.Sort((c1, c2) => c2.LastAccess.CompareTo(c1.LastAccess));
        }


        public void StartGame()
        {
            if (_FileSaving)
                return;
            if (!hasItemInfo)
            {
                MirMessageBox msgBox = new MirMessageBox("ItemInfo.dat could not be found!", MirMessageBoxButtons.OK);
                msgBox.OKButton.Click += (o, e) => Program.Form.Close();
                msgBox.Show();
                return;
            }
            if (!Libraries.Loaded)
            {
                MirMessageBox message = new MirMessageBox(string.Format("Please wait, The game is still loading... {0:##0}%", Libraries.Progress / (double)Libraries.Count * 100), MirMessageBoxButtons.Cancel);

                message.BeforeDraw += (o, e) => message.Label.Text = string.Format("Please wait, The game is still loading... {0:##0}%", Libraries.Progress / (double)Libraries.Count * 100);

                message.AfterDraw += (o, e) =>
                {
                    if (!Libraries.Loaded) return;
                    message.Dispose();
                    StartGame();
                };

                message.Show();

                return;
            }
            StartGameButton.Enabled = false;

            Network.Enqueue(new C.StartGame
            {
                CharacterIndex = Characters[_selected].Index
            });
        }

        public override void Process()
        {


        }
        public override void ProcessPacket(Packet p)
        {
            switch (p.Index)
            {
                case (short)ServerPacketIds.NewCharacter:
                    NewCharacter((S.NewCharacter)p);
                    break;
                case (short)ServerPacketIds.NewCharacterSuccess:
                    NewCharacter((S.NewCharacterSuccess)p);
                    break;
                case (short)ServerPacketIds.DeleteCharacter:
                    DeleteCharacter((S.DeleteCharacter)p);
                    break;
                case (short)ServerPacketIds.DeleteCharacterSuccess:
                    DeleteCharacter((S.DeleteCharacterSuccess)p);
                    break;
                case (short)ServerPacketIds.StartGame:
                    StartGame((S.StartGame)p);
                    break;
                case (short)ServerPacketIds.StartGameBanned:
                    StartGame((S.StartGameBanned)p);
                    break;
                case (short)ServerPacketIds.StartGameDelay:
                    StartGame((S.StartGameDelay)p);
                    break;
                case (short)ServerPacketIds.ItemInfoList:
                    RewriteItemInfoFile((S.ItemInfoList)p);
                    break;
                default:
                    base.ProcessPacket(p);
                    break;
            }
        }

        public void RewriteItemInfoFile(S.ItemInfoList p)
        {
            _FileSaving = true;
            //  Incase we only recieve one chunk.
            if (p.FileStart && p.FileEnd)
            {
                hasItemInfo = true;
                fileTime = p.fileTime;
                itemInfos.AddRange(p.ItemInfos);
                using (FileStream stream = File.Create(@".\ItemInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(fileTime.ToBinary());
                        writer.Write(itemInfos.Count);
                        for (int i = 0; i < itemInfos.Count; i++)
                            itemInfos[i].Save(writer);
                    }
                }
                _FileSaving = false;
                return;
            }
            else if (p.FileStart && !p.FileEnd) // Start of the Item Info list, (it's provided in chunks)
            {
                //  Clear the old list if it exists
                itemInfos.Clear();
                //  Not exactly true, we're just preventing them from logging in while the ItemInfo list is incomplete
                itemInfos.AddRange(p.ItemInfos);
                fileTime = p.fileTime;
            }
            else if (p.FileEnd && !p.FileStart) // End of the ItemInfo list being sent from server
            {
                itemInfos.AddRange(p.ItemInfos);
                using (FileStream stream = File.Create(@".\ItemInfo.dat"))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        writer.Write(fileTime.ToBinary());
                        writer.Write(itemInfos.Count);
                        for (int i = 0; i < itemInfos.Count; i++)
                            itemInfos[i].Save(writer);
                    }
                }
                hasItemInfo = true;
                _FileSaving = false;
                return;
            }
            else                //  Middle of the ItemInfo list being sent from server
            {
                itemInfos.AddRange(p.ItemInfos);
            }
        }

        private void NewCharacter(S.NewCharacter p)
        {
            _character.OKButton.Enabled = true;

            switch (p.Result)
            {
                case 0:
                    MirMessageBox.Show("Creating new characters is currently disabled.");
                    _character.Dispose();
                    break;
                case 1:
                    MirMessageBox.Show("Your Character Name is not acceptable.");
                    _character.NameTextBox.SetFocus();
                    break;
                case 2:
                    MirMessageBox.Show("The gender you selected does not exist.\n Contact a GM for assistance.");
                    break;
                case 3:
                    MirMessageBox.Show("The class you selected does not exist.\n Contact a GM for assistance.");
                    break;
                case 4:
                    MirMessageBox.Show("You cannot make anymore then " + Globals.MaxCharacterCount + " Characters.");
                    _character.Dispose();
                    break;
                case 5:
                    MirMessageBox.Show("A Character with this name already exists.");
                    _character.NameTextBox.SetFocus();
                    break;
            }


        }
        private void NewCharacter(S.NewCharacterSuccess p)
        {
            _character.Dispose();
            MirMessageBox.Show("Your character was created successfully.");

            Characters.Insert(0, p.CharInfo);
            _selected = 0;
            UpdateCharacterInterface();
            UpdateInterfaceEfect();
            UpdateInterfaceEfect1();
            UpdateInterfaceEfect2();
            UpdateInterface();
        }

        private void DeleteCharacter()
        {
            if (_selected < 0 || _selected >= Characters.Count) return;

            MirMessageBox message = new MirMessageBox(string.Format("Are you sure you want to Delete the character {0}?", Characters[_selected].Name), MirMessageBoxButtons.YesNo);
            int index = Characters[_selected].Index;

            message.YesButton.Click += (o, e) =>
            {
                DeleteCharacterButton.Enabled = false;
                Network.Enqueue(new C.DeleteCharacter { CharacterIndex = index });
            };

            message.Show();
        }

        private void DeleteCharacter(S.DeleteCharacter p)
        {
            DeleteCharacterButton.Enabled = true;
            switch (p.Result)
            {
                case 0:
                    MirMessageBox.Show("Deleting characters is currently disabled.");
                    break;
                case 1:
                    MirMessageBox.Show("The character you selected does not exist.\n Contact a GM for assistance.");
                    break;
            }
        }
        private void DeleteCharacter(S.DeleteCharacterSuccess p)
        {
            DeleteCharacterButton.Enabled = true;
            MirMessageBox.Show("Your character was deleted successfully.");

            for (int i = 0; i < Characters.Count; i++)
                if (Characters[i].Index == p.CharacterIndex)
                {
                    Characters.RemoveAt(i);
                    break;
                }

            UpdateInterface();
        }

        private void StartGame(S.StartGameDelay p)
        {
            StartGameButton.Enabled = true;

            long time = CMain.Time + p.Milliseconds;

            MirMessageBox message = new MirMessageBox(string.Format("You cannot log onto this character for another {0} seconds.", Math.Ceiling(p.Milliseconds / 1000M)));

            message.BeforeDraw += (o, e) => message.Label.Text = string.Format("You cannot log onto this character for another {0} seconds.", Math.Ceiling((time - CMain.Time) / 1000M));


            message.AfterDraw += (o, e) =>
            {
                if (CMain.Time <= time) return;
                message.Dispose();
                StartGame();
            };

            message.Show();
        }
        public void StartGame(S.StartGameBanned p)
        {
            StartGameButton.Enabled = true;

            TimeSpan d = p.ExpiryDate - CMain.Now;
            MirMessageBox.Show(string.Format("This account is banned.\n\nReason: {0}\nExpiryDate: {1}\nDuration: {2:#,##0} Hours, {3} Minutes, {4} Seconds", p.Reason,
                                             p.ExpiryDate, Math.Floor(d.TotalHours), d.Minutes, d.Seconds));
        }
        public void StartGame(S.StartGame p)
        {
            if (_FileSaving)
                return;
            StartGameButton.Enabled = true;

            if (p.Resolution < Settings.Resolution || Settings.Resolution == 0) Settings.Resolution = p.Resolution;

            if (p.Resolution < 1024 || Settings.Resolution < 1024) Settings.Resolution = 800;
            else if (p.Resolution < 1366 || Settings.Resolution < 1280) Settings.Resolution = 1024;
            else if (p.Resolution < 1366 || Settings.Resolution < 1366) Settings.Resolution = 1280;//not adding an extra setting for 1280 on server cause well it just depends on the aspect ratio of your screen
            else if (p.Resolution >= 1366 && Settings.Resolution >= 1366) Settings.Resolution = 1366;

            switch (p.Result)
            {
                case 0:
                    MirMessageBox.Show("Starting the game is currently disabled.");
                    break;
                case 1:
                    MirMessageBox.Show("You are not logged in.");
                    break;
                case 2:
                    MirMessageBox.Show("Your character could not be found.");
                    break;
                case 3:
                    MirMessageBox.Show("No active map and/or start point found.");
                    break;
                case 4:
                    if (Settings.Resolution == 1024)
                        CMain.SetResolution(1024, 768);
                    else if (Settings.Resolution == 1280)
                        CMain.SetResolution(1280, 800);
                    else if (Settings.Resolution == 1366)
                        CMain.SetResolution(1366, 768);// thank you
                    ActiveScene = new GameScene() { Item_Info_List = itemInfos };
                    Dispose();
                    break;
            }
        }

        public void UpdateCharacterInterface()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                bool flag = Characters[i] == null;
                if (!flag)
                {
                    CharacterDisplay[i].Visible = true;
                    CharacterButtons[i].Visible = true;
                    switch (Characters[i].Class)
                    {
                        case MirClass.Warrior:
                            CharacterDisplay[i].Index = ((Characters[i].Gender == MirGender.Male) ? 600 : 660);
                            CharacterDisplay[i].AnimationCount = ((Characters[i].Gender == MirGender.Male) ? 12 : 12);
                            break;
                        case MirClass.Wizard:
                            CharacterDisplay[i].Index = ((Characters[i].Gender == MirGender.Male) ? 720 : 780);
                            CharacterDisplay[i].AnimationCount = ((Characters[i].Gender == MirGender.Male) ? 14 : 15);
                            break;
                        case MirClass.Taoist:
                            CharacterDisplay[i].Index = ((Characters[i].Gender == MirGender.Male) ? 840 : 900);
                            CharacterDisplay[i].AnimationCount = 15;
                            break;
                        case MirClass.Assassin:
                            CharacterDisplay[i].Index = ((Characters[i].Gender == MirGender.Male) ? 960 : 1020);
                            CharacterDisplay[i].AnimationCount = 15;
                            break;
                        case MirClass.Archer:
                            CharacterDisplay[i].Index = ((Characters[i].Gender == MirGender.Male) ? 1080 : 1140);
                            CharacterDisplay[i].AnimationCount = 15;
                            break;
                    }
                }
            }
        }

        public void UpdateInterface()
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                CharacterButtons[i].Selected = (i == _selected);
                CharacterButtons[i].Update((i >= Characters.Count) ? null : Characters[i]);
            }
            for (int j = 0; j < Characters.Count; j++)
            {
                CharacterDisplay[j].Visible = true;
                CharacterEfects[j].Visible = false;
            }
            bool flag = _selected >= 0 && _selected < Characters.Count;
            if (flag)
            {
                CharacterDisplay[_selected].Visible = false;
                CharacterEfects[_selected].Visible = true;
                CharacterButtons[_selected].Visible = false;
                switch (Characters[_selected].Class)
                {
                    case MirClass.Warrior:
                        Sound = ((Characters[_selected].Gender == MirGender.Male) ? SoundList.WarMSel : SoundList.WarFSel);
                        SoundManager.PlaySound(Sound, false);
                        break;
                    case MirClass.Wizard:
                        Sound = ((Characters[_selected].Gender == MirGender.Male) ? SoundList.WizMSel : SoundList.WizFSel);
                        SoundManager.PlaySound(Sound, false);
                        break;
                    case MirClass.Taoist:
                        Sound = ((Characters[_selected].Gender == MirGender.Male) ? SoundList.TaoMSel : SoundList.TaoFSel);
                        SoundManager.PlaySound(Sound, false);
                        break;
                    case MirClass.Assassin:
                        Sound = ((Characters[_selected].Gender == MirGender.Male) ? SoundList.AssMSel : SoundList.AssFSel);
                        SoundManager.PlaySound(Sound, false);
                        break;
                }
                CharacterEfects[_selected].AfterAnimation += (o, e) =>
                {
                    CharacterDisplay[_selected].Visible = true;
                    CharacterEfects[_selected].Visible = false;
                    CharacterButtons[_selected].Visible = true;
                };
                StartGameButton.Enabled = true;
            }
            else
            {
                CharacterDisplay[_selected].Visible = false;
                StartGameButton.Enabled = false;
            }
        }


        public void UpdateInterfaceEfect()
        {
            bool flag = Characters.Count < 1;
            if (!flag)
            {
                switch (Characters[0].Class)
                {
                    case MirClass.Warrior:
                        {
                            CharacterEfects[0].Index = ((Characters[0].Gender == MirGender.Male) ? 620 : 680);
                            CharacterEfects[0].AnimationCount = ((Characters[0].Gender == MirGender.Male) ? 20 : 25);
                            int frames;
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[0].Gender == MirGender.Male) ? 610 : 613);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[0].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Wizard:
                        {
                            CharacterEfects[0].Index = ((Characters[0].Gender == MirGender.Male) ? 740 : 800);
                            CharacterEfects[0].AnimationCount = ((Characters[0].Gender == MirGender.Male) ? 24 : 25);
                            int frames;
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[0].Gender == MirGender.Male) ? 610 : 610);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[0].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Taoist:
                        {
                            CharacterEfects[0].Index = ((Characters[0].Gender == MirGender.Male) ? 860 : 920);
                            CharacterEfects[0].AnimationCount = ((Characters[0].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[0].AfterDraw += delegate (object o1, EventArgs e)
                            {
                                frames = ((Characters[0].Gender == MirGender.Male) ? 611 : 611);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[0].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Assassin:
                        {
                            CharacterEfects[0].Index = ((Characters[0].Gender == MirGender.Male) ? 980 : 1040);
                            CharacterEfects[0].AnimationCount = ((Characters[0].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[0].Gender == MirGender.Male) ? 613 : 611);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[0].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Archer:
                        {
                            CharacterEfects[0].Index = ((Characters[0].Gender == MirGender.Male) ? 1100 : 1160);
                            CharacterEfects[0].AnimationCount = ((Characters[0].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[0].Gender == MirGender.Male) ? 610 : 612);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[0].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[0].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[0].Index + frames, CharacterEfects[0].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                }
            }
        }

        public void UpdateInterfaceEfect1()
        {
            bool flag = Characters.Count > 1;
            if (flag)
            {
                switch (Characters[1].Class)
                {
                    case MirClass.Warrior:
                        {
                            CharacterEfects[1].Index = ((Characters[1].Gender == MirGender.Male) ? 620 : 680);
                            CharacterEfects[1].AnimationCount = ((Characters[1].Gender == MirGender.Male) ? 20 : 25);
                            int frames;
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[1].Gender == MirGender.Male) ? 610 : 613);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[1].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Wizard:
                        {
                            CharacterEfects[1].Index = ((Characters[1].Gender == MirGender.Male) ? 740 : 800);
                            CharacterEfects[1].AnimationCount = ((Characters[1].Gender == MirGender.Male) ? 24 : 25);
                            int frames;
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[1].Gender == MirGender.Male) ? 610 : 610);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[1].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Taoist:
                        {
                            CharacterEfects[1].Index = ((Characters[1].Gender == MirGender.Male) ? 860 : 920);
                            CharacterEfects[1].AnimationCount = ((Characters[1].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[1].AfterDraw += delegate (object o1, EventArgs e)
                            {
                                frames = ((Characters[1].Gender == MirGender.Male) ? 611 : 611);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[1].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Assassin:
                        {
                            CharacterEfects[1].Index = ((Characters[1].Gender == MirGender.Male) ? 980 : 1040);
                            CharacterEfects[1].AnimationCount = ((Characters[1].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[1].Gender == MirGender.Male) ? 613 : 611);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[1].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Archer:
                        {
                            CharacterEfects[1].Index = ((Characters[1].Gender == MirGender.Male) ? 1100 : 1160);
                            CharacterEfects[1].AnimationCount = ((Characters[1].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[1].Gender == MirGender.Male) ? 610 : 612);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[1].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[1].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[1].Index + frames, CharacterEfects[1].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                }
            }
        }

        public void UpdateInterfaceEfect2()
        {
            bool flag = Characters.Count > 2;
            if (flag)
            {
                switch (Characters[2].Class)
                {
                    case MirClass.Warrior:
                        {
                            CharacterEfects[2].Index = ((Characters[2].Gender == MirGender.Male) ? 620 : 680);
                            CharacterEfects[2].AnimationCount = ((Characters[2].Gender == MirGender.Male) ? 20 : 25);
                            int frames;
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[2].Gender == MirGender.Male) ? 610 : 613);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[2].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Wizard:
                        {
                            CharacterEfects[2].Index = ((Characters[2].Gender == MirGender.Male) ? 740 : 800);
                            CharacterEfects[2].AnimationCount = ((Characters[2].Gender == MirGender.Male) ? 24 : 25);
                            int frames;
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[2].Gender == MirGender.Male) ? 610 : 610);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[2].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Taoist:
                        {
                            CharacterEfects[2].Index = ((Characters[2].Gender == MirGender.Male) ? 860 : 920);
                            CharacterEfects[2].AnimationCount = ((Characters[2].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[2].AfterDraw += delegate (object o1, EventArgs e)
                            {
                                frames = ((Characters[2].Gender == MirGender.Male) ? 611 : 611);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[2].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Assassin:
                        {
                            CharacterEfects[2].Index = ((Characters[2].Gender == MirGender.Male) ? 980 : 1040);
                            CharacterEfects[2].AnimationCount = ((Characters[2].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[2].Gender == MirGender.Male) ? 613 : 611);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[2].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                    case MirClass.Archer:
                        {
                            CharacterEfects[2].Index = ((Characters[2].Gender == MirGender.Male) ? 1100 : 1160);
                            CharacterEfects[2].AnimationCount = ((Characters[2].Gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Characters[2].Gender == MirGender.Male) ? 610 : 612);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            CharacterEfects[2].AfterDraw += (o, e) =>
                            {
                                frames = ((Convert.ToInt32(Characters[2].Gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterEfects[2].Index + frames, CharacterEfects[2].Location, Color.White, true, 0.6f);
                            };
                            break;
                        }
                }
            }
        }

        #region Disposable
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Background = null;
                this._character = null;
                this.ServerLabel = null;
                CharacterDisplay = null;
                this.StartGameButton = null;
                this.Adorno = null;
                this.NewCharacterButton = null;
                this.DeleteCharacterButton = null;
                this.CreditsButton = null;
                this.ExitGame = null;
                CharacterButtons = null;
                Characters = null;
                this._selected = 0;
            }

            base.Dispose(disposing);
        }
        #endregion
        public sealed class NewCharacterDialog : MirImageControl
        {
            private static readonly Regex Reg = new Regex(@"^[A-Za-z0-9]{" + Globals.MinCharacterNameLength + "," + Globals.MaxCharacterNameLength + "}$");

            public MirImageControl TitleLabel;
            public MirAnimatedControl CharacterDisplay;
            public MirAnimatedControl CharacterDisplayEfect;

            public MirButton OKButton,
                             CancelButton,
                             WarriorButton,
                             WizardButton,
                             TaoistButton,
                             AssassinButton,
                             ArcherButton,
                             MaleButton,
                             FemaleButton;

            public MirTextBox NameTextBox;

            public MirLabel Description;

            private MirClass _class;
            private MirGender _gender;

            public NewCharacterDialog()
            {
                Index = 27;
                Library = Libraries.CustomButtons;
                Location = new Point(600, 160);
                Size = new Size(500, 400);

                CancelButton = new MirButton
                {
                    HoverIndex = 281,
                    Index = 280,
                    Library = Libraries.CustomTitle,
                    Location = new Point(175, 395),
                    Parent = this,
                    PressedIndex = 282
                };
                CancelButton.Click += (o, e) =>
                {
                    Dispose();

                    for (int i = 0; i < SelectScene.Characters.Count; i++)
                    {
                        SelectScene.CharacterDisplay[i].Visible = true;
                        SelectScene.CharacterEfects[i].Visible = false;
                        SelectScene.CharacterButtons[i].Visible = true;
                    }
                };


                OKButton = new MirButton
                {
                    Enabled = false,
                    HoverIndex = 361,
                    Index = 360,
                    Library = Libraries.CustomTitle,
                    Location = new Point(40, 395),
                    Parent = this,
                    PressedIndex = 362
                };
                OKButton.Click += (o, e) => CreateCharacter();

                NameTextBox = new MirTextBox
                {
                    Location = new Point(39, 356),
                    Parent = this,
                    Size = new Size(203, 20),
                    MaxLength = Globals.MaxCharacterNameLength,
                };
                NameTextBox.TextBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
                NameTextBox.TextBox.TextChanged += new EventHandler(this.CharacterNameTextBox_TextChanged);
                NameTextBox.SetFocus();

                CharacterDisplay = new MirAnimatedControl
                {
                    Animated = true,
                    AnimationCount = 13,
                    AnimationDelay = 100L,
                    FadeIn = true,
                    FadeInDelay = 75L,
                    FadeInRate = 0.1f,
                    Index = 0,
                    Library = Libraries.CustomCharsel,
                    Location = new Point(-220, 140),
                    Parent = this,
                    UseOffSet = true
                };

                CharacterDisplayEfect = new MirAnimatedControl
                {
                    Animated = true,
                    AnimationCount = 13,
                    AnimationDelay = 100L,
                    Visible = false,
                    Index = 0,
                    Library = Libraries.CustomCharsel,
                    Location = new Point(-220, 140),
                    Parent = this,
                    UseOffSet = true
                };


                WarriorButton = new MirButton
                {
                    HoverIndex = 2427,
                    Index = 2427,
                    Library = Libraries.CustomPrguse,
                    Location = new Point(56, 67),
                    Parent = this,
                    PressedIndex = 2428,
                    Sound = SoundList.ButtonA
                };
                WarriorButton.Click += (o, e) =>
                    {
                        _class = MirClass.Warrior;
                        UpdateInterface();
                    };


                WizardButton = new MirButton
                {
                    HoverIndex = 2430,
                    Index = 2429,
                    Library = Libraries.CustomPrguse,
                    Location = new Point(113, 67),
                    Parent = this,
                    PressedIndex = 2431,
                    Sound = SoundList.ButtonA
                };
                WizardButton.Click += (o, e) =>
                    {
                        _class = MirClass.Wizard;
                        UpdateInterface();
                    };


                TaoistButton = new MirButton
                {
                    HoverIndex = 2433,
                    Index = 2432,
                    Library = Libraries.CustomPrguse,
                    Location = new Point(169, 67),
                    Parent = this,
                    PressedIndex = 2434,
                    Sound = SoundList.ButtonA
                };
                TaoistButton.Click += (o, e) =>
                    {
                        _class = MirClass.Taoist;
                        UpdateInterface();
                    };

                AssassinButton = new MirButton
                {
                    HoverIndex = 2436,
                    Index = 2435,
                    Library = Libraries.CustomPrguse,
                    Location = new Point(56, 122),
                    Parent = this,
                    PressedIndex = 2437,
                    Sound = SoundList.ButtonA
                };
                AssassinButton.Click += (o, e) =>
                    {
                        _class = MirClass.Assassin;
                        UpdateInterface();
                    };

                ArcherButton = new MirButton
                {
                    HoverIndex = 2439,
                    Index = 2438,
                    Library = Libraries.CustomPrguse,
                    Location = new Point(113, 122),
                    Parent = this,
                    PressedIndex = 2440,
                    Sound = SoundList.ButtonA
                };
                ArcherButton.Click += (o, e) =>
                {
                    _class = MirClass.Archer;
                    UpdateInterface();
                };


                MaleButton = new MirButton
                {
                    HoverIndex = 2421,
                    Index = 2421,
                    Library = Libraries.CustomPrguse,
                    Location = new Point(85, 199),
                    Parent = this,
                    PressedIndex = 2422,
                    Sound = SoundList.ButtonA
                };
                MaleButton.Click += (o, e) =>
                    {
                        _gender = MirGender.Male;
                        UpdateInterface();
                    };

                FemaleButton = new MirButton
                {
                    HoverIndex = 2424,
                    Index = 2423,
                    Library = Libraries.CustomPrguse,
                    Location = new Point(142, 199),
                    Parent = this,
                    PressedIndex = 2425,
                    Sound = SoundList.ButtonA
                };
                FemaleButton.Click += (o, e) =>
                    {
                        _gender = MirGender.Female;
                        UpdateInterface();
                    };

                Description = new MirLabel
                {
                    Border = true,
                    Location = new Point(279, 70),
                    Parent = this,
                    Size = new Size(278, 170),
                    //Text = WarriorDescription,
                };
            }

            private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (sender == null) return;
                if (e.KeyChar != (char)Keys.Enter) return;
                e.Handled = true;

                if (OKButton.Enabled)
                    OKButton.InvokeMouseClick(null);
            }
            private void CharacterNameTextBox_TextChanged(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(NameTextBox.Text))
                {
                    OKButton.Enabled = false;
                    NameTextBox.Border = false;
                }
                else if (!Reg.IsMatch(NameTextBox.Text))
                {
                    OKButton.Enabled = false;
                    NameTextBox.Border = true;
                    NameTextBox.BorderColour = Color.Red;
                }
                else
                {
                    OKButton.Enabled = true;
                    NameTextBox.Border = true;
                    NameTextBox.BorderColour = Color.Green;
                }
            }

            private void CreateCharacter()
            {
                OKButton.Enabled = false;

                Network.Enqueue(new C.NewCharacter
                {
                    Name = NameTextBox.Text,
                    Class = _class,
                    Gender = _gender
                });
            }

            private void UpdateInterface()
            {
                MaleButton.Index = 2420;
                FemaleButton.Index = 2423;

                WarriorButton.Index = 2426;
                WizardButton.Index = 2429;
                TaoistButton.Index = 2432;
                AssassinButton.Index = 2435;
                ArcherButton.Index = 2438;

                switch (_gender)
                {
                    case MirGender.Male:
                        MaleButton.Index = 2421;
                        break;
                    case MirGender.Female:
                        FemaleButton.Index = 2424;
                        break;
                }

                switch (_class)
                {
                    case MirClass.Warrior:
                        {
                            CharacterDisplayEfect.Visible = true;
                            CharacterDisplay.Visible = false;
                            CharacterDisplayEfect.Index = ((_gender == MirGender.Male) ? 20 : 80);
                            CharacterDisplayEfect.AnimationCount = ((_gender == MirGender.Male) ? 20 : 25);
                            Sound = ((_gender == MirGender.Male) ? SoundList.WarMSel : SoundList.WarFSel);
                            SoundManager.PlaySound(Sound, false);
                            int frames;
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((_gender == MirGender.Male) ? 1180 : 1183);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, new Point(380, 300), Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((Convert.ToInt32(_gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, CharacterDisplay.Location, Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterAnimation += delegate (object o, EventArgs e)
                            {
                                CharacterDisplayEfect.Visible = false;
                                CharacterDisplay.Visible = true;
                                WarriorButton.Index = 2427;
                                CharacterDisplay.Index = ((_gender == MirGender.Male) ? 0 : 60);
                                CharacterDisplay.AnimationCount = ((_gender == MirGender.Male) ? 13 : 13);
                            };
                            break;
                        }
                    case MirClass.Wizard:
                        {
                            CharacterDisplayEfect.Visible = true;
                            CharacterDisplay.Visible = false;
                            CharacterDisplayEfect.Index = ((_gender == MirGender.Male) ? 140 : 200);
                            CharacterDisplayEfect.AnimationCount = ((_gender == MirGender.Male) ? 24 : 25);
                            Sound = ((_gender == MirGender.Male) ? SoundList.WizMSel : SoundList.WizFSel);
                            SoundManager.PlaySound(Sound, false);
                            int frames;
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((_gender == MirGender.Male) ? 1180 : 1180);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, new Point(380, 300), Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((Convert.ToInt32(_gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, CharacterDisplay.Location, Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterAnimation += delegate (object o, EventArgs e)
                            {
                                CharacterDisplayEfect.Visible = false;
                                CharacterDisplay.Visible = true;
                                WizardButton.Index = 2430;
                                CharacterDisplay.Index = ((_gender == MirGender.Male) ? 120 : 180);
                                CharacterDisplay.AnimationCount = 14;
                            };
                            break;
                        }
                    case MirClass.Taoist:
                        {
                            CharacterDisplayEfect.Visible = true;
                            CharacterDisplay.Visible = false;
                            CharacterDisplayEfect.Index = ((_gender == MirGender.Male) ? 260 : 320);
                            CharacterDisplayEfect.AnimationCount = ((_gender == MirGender.Male) ? 25 : 25);
                            Sound = ((_gender == MirGender.Male) ? SoundList.TaoMSel : SoundList.TaoFSel);
                            SoundManager.PlaySound(base.Sound, false);
                            int frames;
                            CharacterDisplayEfect.AfterDraw += delegate (object o1, EventArgs e)
                            {
                                frames = ((_gender == MirGender.Male) ? 1181 : 1181);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, new Point(380, 300), Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((Convert.ToInt32(_gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, CharacterDisplay.Location, Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterAnimation += delegate (object o, EventArgs e)
                            {
                                CharacterDisplayEfect.Visible = false;
                                CharacterDisplay.Visible = true;
                                TaoistButton.Index = 2433;
                                CharacterDisplay.Index = ((_gender == MirGender.Male) ? 240 : 300);
                                CharacterDisplay.AnimationCount = 15;
                            };
                            break;
                        }
                    case MirClass.Assassin:
                        {
                            CharacterDisplayEfect.Visible = true;
                            CharacterDisplay.Visible = false;
                            CharacterDisplayEfect.Index = ((_gender == MirGender.Male) ? 380 : 440);
                            CharacterDisplayEfect.AnimationCount = ((_gender == MirGender.Male) ? 25 : 25);
                            Sound = ((_gender == MirGender.Male) ? SoundList.AssMSel : SoundList.AssFSel);
                            SoundManager.PlaySound(Sound, false);
                            int frames;
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((_gender == MirGender.Male) ? 1183 : 1181);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, new Point(380, 300), Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((Convert.ToInt32(_gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, CharacterDisplay.Location, Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterAnimation += delegate (object o, EventArgs e)
                            {
                                CharacterDisplayEfect.Visible = false;
                                CharacterDisplay.Visible = true;
                                AssassinButton.Index = 2436;
                                CharacterDisplay.Index = ((_gender == MirGender.Male) ? 360 : 420);
                                CharacterDisplay.AnimationCount = 15;
                            };
                            break;
                        }
                    case MirClass.Archer:
                        {
                            CharacterDisplayEfect.Visible = true;
                            CharacterDisplay.Visible = false;
                            CharacterDisplayEfect.Index = ((_gender == MirGender.Male) ? 500 : 560);
                            CharacterDisplayEfect.AnimationCount = ((_gender == MirGender.Male) ? 25 : 25);
                            int frames;
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((_gender == MirGender.Male) ? 1180 : 1182);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, new Point(380, 300), Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterDraw += delegate (object o, EventArgs e)
                            {
                                frames = ((Convert.ToInt32(_gender) == 0) ? 1800 : 1800);
                                Libraries.CustomCharsel.DrawBlend(CharacterDisplayEfect.Index + frames, CharacterDisplay.Location, Color.White, true, 0.6f);
                            };
                            CharacterDisplayEfect.AfterAnimation += delegate (object o, EventArgs e)
                            {
                                CharacterDisplayEfect.Visible = false;
                                CharacterDisplay.Visible = true;
                                ArcherButton.Index = 2439;
                                CharacterDisplay.Index = ((_gender == MirGender.Male) ? 480 : 540);
                                CharacterDisplay.AnimationCount = 15;
                            };
                            break;
                        }
                }
                }
            }

            public sealed class CharacterButton : MirImageControl
            {
                public MirLabel NameLabel, LevelLabel, ClassLabel;
                public bool Selected;
                public MirButton SelectedButton;

                public CharacterButton()
                {
                    Index = 73;
                    Library = Libraries.CustomButtons;
                    Sound = SoundList.ButtonA;
                    Sort = true;

                    NameLabel = new MirLabel
                    {
                        Location = new Point(62, 7),
                        Parent = this,
                        NotControl = true,
                        Size = new Size(170, 18)
                    };

                    LevelLabel = new MirLabel
                    {
                        Location = new Point(136, 27),
                        Parent = this,
                        NotControl = true,
                        Size = new Size(30, 18)
                    };

                    ClassLabel = new MirLabel
                    {
                        Location = new Point(50, 27),
                        Parent = this,
                        NotControl = true,
                        Size = new Size(100, 18)
                    };
                }

                public void Update(SelectInfo info)
                {
                    Library = Libraries.CustomButtons;
                    Index = 73;
                    bool selected = this.Selected;
                    if (selected)
                    {
                        Index++;
                    }
                    NameLabel.Text = info.Name;
                    LevelLabel.Text = info.Level.ToString();
                    ClassLabel.Text = info.Class.ToString();
                    NameLabel.Location = new Point(62, 35);
                    LevelLabel.Location = new Point(136, 55);
                    ClassLabel.Location = new Point(47, 55);
                    Visible = true;
                    NameLabel.Visible = true;
                    LevelLabel.Visible = true;
                    ClassLabel.Visible = true;
                }
            }
        }
    }
