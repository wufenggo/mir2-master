﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Server.Custom;
using Server.MirDatabase;
using Server.MirObjects;
using S = ServerPackets;

namespace Server.MirEnvir
{
    public class Map
    {
        private static Envir Envir
        {
            get { return SMain.Envir; }
        }

        public MapInfo Info;

        public int Thread = 0;

        public int Width, Height;
        public Cell[,] Cells;
        public List<Point> WalkableCells;
        public Door[,] DoorIndex;
        public List<Door> Doors = new List<Door>();
        public MineSpot[,] Mine;
        public long LightningTime, FireTime, InactiveTime;
        public int MonsterCount, InactiveCount;

        public List<LMS_BR> BR_Matches = new List<LMS_BR>();

        public List<NPCObject> NPCs = new List<NPCObject>();
        public List<PlayerObject> Players = new List<PlayerObject>();
        public List<MapRespawn> Respawns = new List<MapRespawn>();
        public List<DelayedAction> ActionList = new List<DelayedAction>();
        public List<PublicEvent> Events = new List<PublicEvent>();
        public List<ConquestObject> Conquest = new List<ConquestObject>();
        public List<RaidMap> Raids = new List<RaidMap>();
        public ConquestObject tempConquest;
        public byte EXPIncrease = 0;
        public long EXPIncreaseDuration = 0;
        public int DMGIncrease = 0;
        public Map(MapInfo info)
        {
            Info = info;
            foreach (var publicEventInfo in info.PublicEvents)
                Events.Add(new PublicEvent(publicEventInfo, this));
            Thread = Envir.Random.Next(Settings.ThreadLimit);
            foreach (var br in info.LMS_BR)
                BR_Matches.Add(new LMS_BR(br, this));
        }

        public Door AddDoor(byte DoorIndex, Point location)
        {
            DoorIndex = (byte)(DoorIndex & 0x7F);
            for (int i = 0; i < Doors.Count; i++)
                if (Doors[i].index == DoorIndex)
                    return Doors[i];
            Door DoorInfo = new Door() { index = DoorIndex, Location = location };
            Doors.Add(DoorInfo);
            return DoorInfo;
        }
        
        public bool OpenDoor(byte DoorIndex)
        {
            for (int i = 0; i < Doors.Count; i++)
                if (Doors[i].index == DoorIndex)
                {
                    Doors[i].DoorState = 2;
                    Doors[i].LastTick = Envir.Time;
                    return true;
                }
            return false;
        }

        public void SetMapEXPIncrease(byte inc, long duration,int dmg)
        {
            EXPIncrease = inc;
            EXPIncreaseDuration = Envir.Time +  duration * 1000;
            DMGIncrease = dmg;
        }

        private byte FindType(byte[] input)
        {
            //c# custom map format
            if ((input[2] == 0x43) && (input[3] == 0x23))
            {
                return 100;
            }
            //wemade mir3 maps have no title they just start with blank bytes
            if (input[0] == 0)
                return 5;
            //shanda mir3 maps start with title: (C) SNDA, MIR3.
            if ((input[0] == 0x0F) && (input[5] == 0x53) && (input[14] == 0x33))
                return 6;

            //wemades antihack map (laby maps) title start with: Mir2 AntiHack
            if ((input[0] == 0x15) && (input[4] == 0x32) && (input[6] == 0x41) && (input[19] == 0x31))
                return 4;

            //wemades 2010 map format i guess title starts with: Map 2010 Ver 1.0
            if ((input[0] == 0x10) && (input[2] == 0x61) && (input[7] == 0x31) && (input[14] == 0x31))
                return 1;

            //shanda's 2012 format and one of shandas(wemades) older formats share same header info, only difference is the filesize
            if ((input[4] == 0x0F) && (input[18] == 0x0D) && (input[19] == 0x0A))
            {
                int W = input[0] + (input[1] << 8);
                int H = input[2] + (input[3] << 8);
                if (input.Length > (52 + (W * H * 14)))
                    return 3;
                else
                    return 2;
            }

            //3/4 heroes map format (myth/lifcos i guess)
            if ((input[0] == 0x0D) && (input[1] == 0x4C) && (input[7] == 0x20) && (input[11] == 0x6D))
                return 7;
            return 0;
        }

        private void LoadMapCellsv0(byte[] fileBytes)
        {
            int offSet = 0;
            Width = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            Height = BitConverter.ToInt16(fileBytes, offSet);
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 52;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {//total 12
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //Can Fire Over.

                    offSet += 2;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.LowWall; //Can't Fire Over.

                    offSet += 2;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //No Floor Tile.

                    if (Cells[x, y] == null) Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };

                    if (fileBytes[offSet] > 0)
                        DoorIndex[x, y] = AddDoor(fileBytes[offSet], new Point(x, y));

                    offSet += 3;

                    byte light = fileBytes[offSet++];

                    if (light >= 100 && light <= 119)
                        Cells[x, y].FishingAttribute = (sbyte)(light - 100);
                }
        }

        private void LoadMapCellsv1(byte[] fileBytes)
        {
            int offSet = 21;

            int w = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            int xor = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            int h = BitConverter.ToInt16(fileBytes, offSet);
            Width = w ^ xor;
            Height = h ^ xor;
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 54;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    if (((BitConverter.ToInt32(fileBytes, offSet) ^ 0xAA38AA38) & 0x20000000) != 0)
                        Cells[x, y] = Cell.HighWall; //Can Fire Over.

                    offSet += 6;
                    if (((BitConverter.ToInt16(fileBytes, offSet) ^ xor) & 0x8000) != 0)
                        Cells[x, y] = Cell.LowWall; //No Floor Tile.

                    if (Cells[x, y] == null) Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };
                    offSet += 2;
                    if (fileBytes[offSet] > 0)
                        DoorIndex[x, y] = AddDoor(fileBytes[offSet], new Point(x, y));
                    offSet += 5;

                    byte light = fileBytes[offSet++];

                    if (light >= 100 && light <= 119)
                        Cells[x, y].FishingAttribute = (sbyte)(light - 100);

                    offSet += 1;
                }
        }

        private void LoadMapCellsv2(byte[] fileBytes)
        {
            int offSet = 0;
            Width = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            Height = BitConverter.ToInt16(fileBytes, offSet);
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 52;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {//total 14
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //Can Fire Over.

                    offSet += 2;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.LowWall; //Can't Fire Over.

                    offSet += 2;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //No Floor Tile.

                    if (Cells[x, y] == null) Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };

                    offSet += 2;
                    if (fileBytes[offSet] > 0)
                        DoorIndex[x, y] = AddDoor(fileBytes[offSet], new Point(x, y));
                    offSet += 5;

                    byte light = fileBytes[offSet++];

                    if (light >= 100 && light <= 119)
                        Cells[x, y].FishingAttribute = (sbyte)(light - 100);

                    offSet += 2;
                }
        }

        private void LoadMapCellsv3(byte[] fileBytes)
        {
            int offSet = 0;
            Width = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            Height = BitConverter.ToInt16(fileBytes, offSet);
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 52;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {//total 36
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //Can Fire Over.

                    offSet += 2;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.LowWall; //Can't Fire Over.

                    offSet += 2;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //No Floor Tile.

                    if (Cells[x, y] == null) Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };
                    offSet += 2;
                    if (fileBytes[offSet] > 0)
                        DoorIndex[x, y] = AddDoor(fileBytes[offSet], new Point(x, y));
                    offSet += 12;

                    byte light = fileBytes[offSet++];

                    if (light >= 100 && light <= 119)
                        Cells[x, y].FishingAttribute = (sbyte)(light - 100);

                    offSet += 17;
                }
        }

        private void LoadMapCellsv4(byte[] fileBytes)
        {
            int offSet = 31;
            int w = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            int xor = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            int h = BitConverter.ToInt16(fileBytes, offSet);
            Width = w ^ xor;
            Height = h ^ xor;
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 64;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {//total 12
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //Can Fire Over.

                    offSet += 2;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.LowWall; //Can't Fire Over.

                    if (Cells[x, y] == null) Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };
                    offSet += 4;
                    if (fileBytes[offSet] > 0)
                        DoorIndex[x, y] = AddDoor(fileBytes[offSet], new Point(x, y));
                    offSet += 6;
                }
        }

        private void LoadMapCellsv5(byte[] fileBytes)
        {
            int offSet = 22;
            Width = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            Height = BitConverter.ToInt16(fileBytes, offSet);
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 28 + (3 * ((Width / 2) + (Width % 2)) * (Height / 2));
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {//total 14
                    if ((fileBytes[offSet] & 0x01) != 1)
                        Cells[x, y] = Cell.HighWall;
                    else if ((fileBytes[offSet] & 0x02) != 2)
                        Cells[x, y] = Cell.LowWall;
                    else
                        Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };
                    offSet += 13;

                    byte light = fileBytes[offSet++];

                    if (light >= 100 && light <= 119)
                        Cells[x, y].FishingAttribute = (sbyte)(light - 100);
                }
        }

        private void LoadMapCellsv6(byte[] fileBytes)
        {
            int offSet = 16;
            Width = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 2;
            Height = BitConverter.ToInt16(fileBytes, offSet);
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 40;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {//total 20
                    if ((fileBytes[offSet] & 0x01) != 1)
                        Cells[x, y] = Cell.HighWall;
                    else if ((fileBytes[offSet] & 0x02) != 2)
                        Cells[x, y] = Cell.LowWall;
                    else
                        Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };
                    offSet += 20;
                }
        }

        private void LoadMapCellsv7(byte[] fileBytes)
        {
            int offSet = 21;
            Width = BitConverter.ToInt16(fileBytes, offSet);
            offSet += 4;
            Height = BitConverter.ToInt16(fileBytes, offSet);
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offSet = 54;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {//total 15
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.HighWall; //Can Fire Over.
                    offSet += 6;
                    if ((BitConverter.ToInt16(fileBytes, offSet) & 0x8000) != 0)
                        Cells[x, y] = Cell.LowWall; //Can't Fire Over.
                    //offSet += 2;
                    if (Cells[x, y] == null) Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };
                    offSet += 2;
                    if (fileBytes[offSet] > 0)
                        DoorIndex[x, y] = AddDoor(fileBytes[offSet], new Point(x, y));
                    offSet += 4;

                    byte light = fileBytes[offSet++];

                    if (light >= 100 && light <= 119)
                        Cells[x, y].FishingAttribute = (sbyte)(light - 100);

                    offSet += 2;
                }
        }

        private void LoadMapCellsV100(byte[] Bytes)
        {
            int offset = 4;
            if ((Bytes[0] != 1) || (Bytes[1] != 0)) return;//only support version 1 atm
            Width = BitConverter.ToInt16(Bytes, offset);
            offset += 2;
            Height = BitConverter.ToInt16(Bytes, offset);
            Cells = new Cell[Width, Height];
            DoorIndex = new Door[Width, Height];

            offset = 8;

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    offset += 2;
                    if ((BitConverter.ToInt32(Bytes, offset) & 0x20000000) != 0)
                        Cells[x, y] = Cell.HighWall; //Can Fire Over.
                    offset += 10;
                    if ((BitConverter.ToInt16(Bytes, offset) & 0x8000) != 0)
                        Cells[x, y] = Cell.LowWall; //Can't Fire Over.

                    if (Cells[x, y] == null) Cells[x, y] = new Cell { Attribute = CellAttribute.Walk };
                    offset += 2;
                    if (Bytes[offset] > 0)
                        DoorIndex[x, y] = AddDoor(Bytes[offset], new Point(x, y));
                    offset += 11;

                    byte light = Bytes[offset++];

                    if (light >= 100 && light <= 119)
                        Cells[x, y].FishingAttribute = (sbyte)(light - 100);
                }
                
        }

        public bool Load()
        {
            try
            {
                string fileName = Path.Combine(Settings.MapPath, Info.FileName + ".map");
                if (File.Exists(fileName))
                {
                    byte[] fileBytes = File.ReadAllBytes(fileName);
                    switch(FindType(fileBytes))
                    {
                        case 0:
                            LoadMapCellsv0(fileBytes);
                            break;
                        case 1:
                            LoadMapCellsv1(fileBytes);
                            break;
                        case 2:
                            LoadMapCellsv2(fileBytes);
                            break;
                        case 3:
                            LoadMapCellsv3(fileBytes);
                            break;
                        case 4:
                            LoadMapCellsv4(fileBytes);
                            break;
                        case 5:
                            LoadMapCellsv5(fileBytes);
                            break;
                        case 6:
                            LoadMapCellsv6(fileBytes);
                            break;
                        case 7:
                            LoadMapCellsv7(fileBytes);
                            break;
                        case 100:
                            LoadMapCellsV100(fileBytes);
                            break;
                    }
                    

                    for (int i = 0; i < Info.Respawns.Count; i++)
                    {
                        MapRespawn info = new MapRespawn(Info.Respawns[i]);
                        if (info.Monster == null) continue;
                        info.Map = this;
                        Respawns.Add(info);

                        if ((info.Info.SaveRespawnTime) && (info.Info.RespawnTicks != 0))
                            SMain.Envir.SavedSpawns.Add(info);
                    }


                    for (int i = 0; i < Info.NPCs.Count; i++)
                    {
                        NPCInfo info = Info.NPCs[i];
                        if (!ValidPoint(info.Location)) continue;

                        AddObject(new NPCObject(info) {CurrentMap = this});
                    }

                    for (int i = 0; i < Info.SafeZones.Count; i++)
                        CreateSafeZone(Info.SafeZones[i]);
                    CreateMine();
                    return true;
                }
            }
            catch (Exception ex)
            {
                SMain.Enqueue(ex);
            }

            SMain.Enqueue("Failed to Load Map: " + Info.FileName);
            return false;
        }

        public PublicEvent GetPublicEvent(Point location)
        {
            for (int i = 0; i < Events.Count; i++)
            {
                PublicEvent publicEvent = Events[i];
                if (!publicEvent.IsActive)
                    continue;

                if (Functions.InRange(publicEvent.CurrentLocation, location, publicEvent.Info.EventSize))
                    return publicEvent;
            }
            return null;
        }

        private void CreateSafeZone(SafeZoneInfo info)
        {
            if (Settings.SafeZoneBorder)
            {
                for (int y = info.Location.Y - info.Size; y <= info.Location.Y + info.Size; y++)
                {
                    if (y < 0) continue;
                    if (y >= Height) break;
                    for (int x = info.Location.X - info.Size; x <= info.Location.X + info.Size; x += Math.Abs(y - info.Location.Y) == info.Size ? 1 : info.Size * 2)
                    {
                        if (x < 0) continue;
                        if (x >= Width) break;
                        if (!Cells[x, y].Valid) continue;

                        SpellObject spell = new SpellObject
                        {
                            ExpireTime = long.MaxValue,
                            Spell = Spell.TrapHexagon,
                            TickSpeed = int.MaxValue,
                            CurrentLocation = new Point(x, y),
                            CurrentMap = this,
                            Decoration = true
                        };

                        Cells[x, y].Add(spell);

                        spell.Spawned();
                    }
                }
            }

            if (Settings.SafeZoneHealing)
            {
                for (int y = info.Location.Y - info.Size; y <= info.Location.Y + info.Size; y++)
                {
                    if (y < 0) continue;
                    if (y >= Height) break;
                    for (int x = info.Location.X - info.Size; x <= info.Location.X + info.Size; x++)
                    {
                        if (x < 0) continue;
                        if (x >= Width) break;
                        if (!Cells[x, y].Valid) continue;

                        SpellObject spell = new SpellObject
                            {
                                ExpireTime = long.MaxValue,
                                Value = 25,
                                TickSpeed = 2000,
                                Spell = Spell.Healing,
                                CurrentLocation = new Point(x, y),
                                CurrentMap = this
                            };

                        Cells[x, y].Add(spell);

                        spell.Spawned();
                    }
                }
            }


        }

        private void CreateMine()
        {
            if ((Info.MineIndex == 0) && (Info.MineZones.Count == 0)) return;
            Mine = new MineSpot[Width, Height];
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    Mine[i, j] = new MineSpot();
            if ((Info.MineIndex != 0) && (Settings.MineSetList.Count > Info.MineIndex - 1))
            {
                Settings.MineSetList[Info.MineIndex - 1].SetDrops(Envir.ItemInfoList);
                for (int i = 0; i < Width; i++)
                    for (int j = 0; j < Height; j++)
                        Mine[i,j].Mine = Settings.MineSetList[Info.MineIndex - 1];
            }
            if (Info.MineZones.Count > 0)
            {
                for (int i = 0; i < Info.MineZones.Count; i++)
                {
                    MineZone Zone = Info.MineZones[i];
                    if (Zone.Mine != 0)
                        Settings.MineSetList[Zone.Mine - 1].SetDrops(Envir.ItemInfoList);
                    if (Settings.MineSetList.Count < Zone.Mine) continue;
                    for (int x =  Zone.Location.X - Zone.Size; x < Zone.Location.X + Zone.Size; x++)
                        for (int y = Zone.Location.Y - Zone.Size; y < Zone.Location.Y + Zone.Size; y++)
                        {
                            if ((x < 0) || (x >= Width) || (y < 0) || (y >= Height)) continue;
                            if (Zone.Mine == 0)
                                Mine[x, y].Mine = null;
                            else
                                Mine[x, y].Mine = Settings.MineSetList[Zone.Mine - 1];
                        }
                }
            }
        }

        public Cell GetCell(Point location)
        {
            return Cells[location.X, location.Y];
        }

        public bool EmptyCell(Point location)
        {
            return Cells[location.X, location.Y].Objects == null || Cells[location.X, location.Y].Objects.Count == 0;
        }

        public Cell GetCell(int x, int y)
        {
            return Cells[x, y];
        }

        public bool ValidPoint(Point location)
        {
            return location.X >= 0 && location.X < Width && location.Y >= 0 && location.Y < Height && GetCell(location).Valid;
        }
        public bool ValidPoint(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && GetCell(x, y).Valid;
        }

        public bool CheckDoorOpen(Point location)
        {
            if (DoorIndex[location.X, location.Y] == null) return true;
            if (DoorIndex[location.X, location.Y].DoorState != 2) return false;
            return true;
        }
        public long lmsDelay;
        public void Process_LMS_BR()
        {
            if (Envir.Time > lmsDelay)
            {
                lmsDelay = Envir.Time + Settings.Second * 1;
                for (int i = 0; i < BR_Matches.Count; i++)
                {
                    BR_Matches[i].Process();
                }
            }
        }
        public long raidDelay;
        public void ProcessRaids()
        {
            if (Envir.Time > raidDelay)
            {
                raidDelay = Envir.Time + Settings.Second * 1;
                for (int i = 0; i < Raids.Count; i++)
                {
                    Raids[i].Process();
                }
            }
        }

        public void Process()
        {
            //Process_LMS_BR();
            ProcessRespawns();
            ProcessRaids();
            //process doors
            for (int i = 0; i < Doors.Count; i++)
            {
                if ((Doors[i].DoorState == 2) && (Doors[i].LastTick + 5000 < Envir.Time))
                {
                    Doors[i].DoorState = 0;
                    //broadcast that door is closed
                    Broadcast(new S.Opendoor() { DoorIndex = Doors[i].index, Close = true }, Doors[i].Location);

                }
            }

            if ((Info.Lightning) && Envir.Time > LightningTime)
            {
                LightningTime = Envir.Time + Envir.Random.Next(3000, 15000);
                for (int i = Players.Count - 1; i >= 0; i--)
                {
                    PlayerObject player = Players[i];
                    Point Location;
                    if (Envir.Random.Next(4) == 0)
                    {
                        Location = player.CurrentLocation;
                        
                    }
                    else
                        Location = new Point(player.CurrentLocation.X - 10 + Envir.Random.Next(20), player.CurrentLocation.Y - 10 + Envir.Random.Next(20));

                    if (!ValidPoint(Location)) continue;

                    SpellObject Lightning = null;
                    Lightning = new SpellObject
                    {
                        Spell = Spell.MapLightning,
                        Value = Envir.Random.Next(Info.LightningDamage),
                        ExpireTime = Envir.Time + (1000),
                        TickSpeed = 500,
                        Caster = null,
                        CurrentLocation = Location,
                        CurrentMap = this,
                        Direction = MirDirection.Up
                    };
                    AddObject(Lightning);
                    Lightning.Spawned();
                }
            }
            if ((Info.Fire) && Envir.Time > FireTime)
            {
                FireTime = Envir.Time + Envir.Random.Next(3000, 15000);
                for (int i = Players.Count - 1; i >= 0; i--)
                {
                    PlayerObject player = Players[i];
                    Point Location;
                    if (Envir.Random.Next(4) == 0)
                    {
                        Location = player.CurrentLocation;

                    }
                    else
                        Location = new Point(player.CurrentLocation.X - 10 + Envir.Random.Next(20), player.CurrentLocation.Y - 10 + Envir.Random.Next(20));

                    if (!ValidPoint(Location)) continue;

                    SpellObject Lightning = new SpellObject
                    {
                        Spell = Spell.MapLava,
                        Value = Envir.Random.Next(Info.FireDamage),
                        ExpireTime = Envir.Time + (1000),
                        TickSpeed = 500,
                        Caster = null,
                        CurrentLocation = Location,
                        CurrentMap = this,
                        Direction = MirDirection.Up
                    };
                    AddObject(Lightning);
                    Lightning.Spawned();
                }
            }
            
            for (int i = 0; i < ActionList.Count; i++)
            {
                if (Envir.Time < ActionList[i].Time) continue;
                Process(ActionList[i]);
                ActionList.RemoveAt(i);
            }

            if (InactiveTime < Envir.Time)
            {
                if (!Players.Any())
                {
                    InactiveTime = Envir.Time + Settings.Minute;
                    InactiveCount++;
                }
                else
                {
                    InactiveCount = 0;
                }
            }
            if (Events.Count > 0)
                for (int i = 0; i < Events.Count; i++)
                    Events[i].Process();
            
            if (EXPIncrease > 0 && Envir.Time > EXPIncreaseDuration)
            {
                EXPIncrease = 0;
                EXPIncreaseDuration = 0;
                DMGIncrease = 0;
            }
        }

        private void ProcessRespawns()
        {
            bool Success = true;
            for (int i = 0; i < Respawns.Count; i++)
            {
                MapRespawn respawn = Respawns[i];
                if ((respawn.Info.RespawnTicks != 0) && (Envir.RespawnTick.CurrentTickcounter < respawn.NextSpawnTick)) continue;
                if ((respawn.Info.RespawnTicks == 0) && (Envir.Time < respawn.RespawnTime)) continue;

                if (respawn.Count < (respawn.Info.Count * Envir.spawnmultiplyer))
                {
                    int count = (respawn.Info.Count * Envir.spawnmultiplyer) - respawn.Count;

                    for (int c = 0; c < count; c++)
                        Success = respawn.Spawn();
                }
                if (Success)
                {
                    respawn.ErrorCount = 0;
                    long delay = Math.Max(1, respawn.Info.Delay - respawn.Info.RandomDelay + Envir.Random.Next(respawn.Info.RandomDelay * 2));
                    respawn.RespawnTime = Envir.Time + (delay * Settings.Minute);
                    if (respawn.Info.RespawnTicks != 0)
                    {
                        respawn.NextSpawnTick = Envir.RespawnTick.CurrentTickcounter + (ulong)respawn.Info.RespawnTicks;
                        if (respawn.NextSpawnTick > long.MaxValue)//since nextspawntick is ulong this simple thing allows an easy way of preventing the counter from overflowing
                            respawn.NextSpawnTick -= long.MaxValue;
                    }
                }
                else
                {
                    respawn.RespawnTime = Envir.Time + 1 * Settings.Minute; // each time it fails to spawn, give it a 1 minute cooldown
                    if (respawn.ErrorCount < 5)
                        respawn.ErrorCount++;
                    else
                    {
                        if (respawn.ErrorCount == 5)
                        {
                            respawn.ErrorCount++;

                            File.AppendAllText(@".\SpawnErrors.txt",
                                String.Format("[{5}]Failed to spawn: mapindex: {0} ,mob info: index: {1} spawncoords ({2}:{3}) range {4}", respawn.Map.Info.Index, respawn.Info.MonsterIndex, respawn.Info.Location.X, respawn.Info.Location.Y, respawn.Info.Spread, DateTime.Now)
                                       + Environment.NewLine);
                            //*/
                        }

                    }
                }
            }
        }

        public void Process(DelayedAction action)
        {
            switch (action.Type)
            {
                case DelayedType.Magic:
                    CompleteMagic(action.Params);
                    break;
                case DelayedType.MonsterMagic:
                    CompleteMonsterMagic(action.Params);
                    break;
                case DelayedType.Spawn:
                    MapObject obj = (MapObject)action.Params[0];

                    switch(obj.Race)
                    {
                        case ObjectType.Monster:
                            {
                                MonsterObject mob = (MonsterObject)action.Params[0];
                                mob.Spawn(this, (Point)action.Params[1]);
                                if (action.Params.Length > 2) ((MonsterObject)action.Params[2]).SlaveList.Add(mob);
                            }
                            break;
                        case ObjectType.Spell:
                            {
                                SpellObject spell = (SpellObject)action.Params[0];
                                AddObject(spell);
                                spell.Spawned();
                            }
                            break;
                    }
                    break;
            }
        }

        private void CompleteMonsterMagic(IList<object> data)
        {
            MonsterObject mob = (MonsterObject)data[0];
            Spell spell = (Spell)data[1];
            if (mob == null || mob.Node == null || mob.Info == null || mob.Dead)
                return;
            int value;
            int magicLevel;
            Point location;
            Cell cell;
            MirDirection dir;
            bool show = false;
            switch (spell)
            {
                #region Crystal Beast
                case Spell.CrystalBeastBlizz:
                    value = (int)data[2];
                    location = (Point)data[3];

                    for (int y = location.Y - 2; y <= location.Y + 2; y++)
                    {
                        if (y < 0)
                            continue;
                        if (y >= Height)
                            break;

                        for (int x = location.X - 2; x <= location.X + 2; x++)
                        {
                            if (x < 0)
                                continue;
                            if (x >= Width)
                                break;

                            cell = GetCell(x, y);

                            if (!cell.Valid)
                                continue;

                            bool cast = true;
                            if (cell.Objects != null)
                                for (int o = 0; o < cell.Objects.Count; o++)
                                {
                                    MapObject target = cell.Objects[o];
                                    if (target.Race != ObjectType.Spell || ( (SpellObject)target ).Spell != Spell.CrystalBeastBlizz)
                                        continue;

                                    cast = false;
                                    break;
                                }

                            if (!cast)
                                continue;

                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.CrystalBeastBlizz,
                                Value = value,
                                ExpireTime = Envir.Time + 3000,
                                TickSpeed = 440,
                                MobCaster = mob,
                                CurrentLocation = new Point(x, y),
                                CastLocation = location,
                                CurrentMap = this,
                                StartTime = Envir.Time + 800,
                            };
                            AddObject(ob);
                            ob.Spawned();
                        }
                    }
                    break;
                #endregion

                #region Mob Firewall
                case Spell.MobFireWall:
                    value = (int)data[2];
                    location = (Point)data[3];
                    int type = (int)data[4];
                    int stage = (int)data[5];
                    if (type != 1)
                    {
                        if (ValidPoint(location))
                        {
                            cell = GetCell(location);

                            bool cast = true;
                            if (cell.Objects != null)
                                for (int o = 0; o < cell.Objects.Count; o++)
                                {
                                    MapObject target = cell.Objects[o];
                                    if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.MobFireWall)
                                        continue;

                                    cast = false;
                                    break;
                                }

                            if (cast)
                            {
                                SpellObject ob = new SpellObject
                                {
                                    Spell = Spell.MobFireWall,
                                    Value = value,
                                    ExpireTime = Envir.Time + (10 + value / 2) * 1000,
                                    TickSpeed = 2000,
                                    MobCaster = mob,
                                    CurrentLocation = location,
                                    CurrentMap = this,
                                };
                                AddObject(ob);
                                ob.Spawned();
                            }
                        }

                        dir = MirDirection.Up;
                        for (int i = 0; i < 4; i++)
                        {
                            location = Functions.PointMove((Point)data[3], dir, 1);
                            dir += 2;

                            if (!ValidPoint(location))
                                continue;

                            cell = GetCell(location);
                            bool cast = true;

                            if (cell.Objects != null)
                                for (int o = 0; o < cell.Objects.Count; o++)
                                {
                                    MapObject target = cell.Objects[o];
                                    if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.MobFireWall)
                                        continue;

                                    cast = false;
                                    break;
                                }

                            if (!cast)
                                continue;

                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.MobFireWall,
                                Value = value,
                                ExpireTime = Envir.Time + (10 + value / 2) * 1000,
                                TickSpeed = 2000,
                                MobCaster = mob,
                                CurrentLocation = location,
                                CurrentMap = this,
                            };
                            AddObject(ob);
                            ob.Spawned();
                        }
                    }
                    else if (type == 1)
                    {
                        if (ValidPoint(location))
                        {
                            List<Point> points = Functions.GetCircleLocations(location, stage + 1);
                            if (points != null && points.Count > 0)
                            {
                                for (int i = 0; i < points.Count; i++)
                                {
                                    if (!ValidPoint(points[i]))
                                        continue;
                                    cell = GetCell(location);
                                    bool cast = true;
                                    if (cell != null &&
                                        cell.Objects != null)
                                    {
                                        for (int o = 0; o < cell.Objects.Count; o++)
                                        {
                                            MapObject target = cell.Objects[o];
                                            if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.MobFireWall)
                                                continue;
                                            cast = false;
                                            break;
                                        }
                                    }
                                    if (!cast)
                                        continue;
                                    SpellObject ob = new SpellObject
                                    {
                                        Spell = Spell.MobFireWall,
                                        Value = value,
                                        ExpireTime = Envir.Time + 1500,
                                        TickSpeed = 1000,
                                        MobCaster = mob,
                                        CurrentLocation = points[i],
                                        CurrentMap = this
                                    };
                                    AddObject(ob);
                                    ob.Spawned();
                                }
                            }
                        }
                    }
                    break;
                #endregion

                #region Special Mob
                case Spell.SpecialMob:
                    value = (int)data[2];
                    location = (Point)data[3];
                    long duration = (long)data[4];
                    if (ValidPoint(location))
                    {
                        SpellObject ob = new SpellObject
                        {
                            Spell = Spell.SpecialMob,
                            Value = value,
                            ExpireTime = Envir.Time + duration,
                            TickSpeed = 1200,
                            MobCaster = mob,
                            CurrentLocation = location,
                            CurrentMap = this
                        };
                        AddObject(ob);
                        ob.Spawned();
                    }
                    break;
                #endregion

                #region Mob Poison Cloud
                case Spell.MobPoisonCloud:
                    value = (int)data[2];
                    location = (Point)data[3];
                    byte bonusdmg = (byte)data[4];
                    show = true;

                    for (int y = location.Y - 1; y <= location.Y + 1; y++)
                    {
                        if (y < 0)
                            continue;
                        if (y >= Height)
                            break;

                        for (int x = location.X - 1; x <= location.X + 1; x++)
                        {
                            if (x < 0)
                                continue;
                            if (x >= Width)
                                break;

                            cell = GetCell(x, y);

                            if (!cell.Valid)
                                continue;

                            bool cast = true;
                            if (cell.Objects != null)
                                for (int o = 0; o < cell.Objects.Count; o++)
                                {
                                    MapObject target = cell.Objects[o];
                                    if (target.Race != ObjectType.Spell || ( (SpellObject)target ).Spell != Spell.MobPoisonCloud)
                                        continue;

                                    cast = false;
                                    break;
                                }

                            if (!cast)
                                continue;

                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.MobPoisonCloud,
                                Value = value + bonusdmg,
                                ExpireTime = Envir.Time + 6000,
                                TickSpeed = 1000,
                                MobCaster = mob,
                                CurrentLocation = new Point(x, y),
                                CastLocation = location,
                                Show = show,
                                CurrentMap = this,
                            };

                            show = false;

                            AddObject(ob);
                            ob.Spawned();
                        }
                    }

                    break;
                #endregion

                #region Blizzard

                case Spell.MobBlizzard:
                    value = (int)data[2];
                    location = (Point)data[3];
                    show = true;

                    for (int y = location.Y - 2; y <= location.Y + 2; y++)
                    {
                        if (y < 0)
                            continue;
                        if (y >= Height)
                            break;

                        for (int x = location.X - 2; x <= location.X + 2; x++)
                        {
                            if (x < 0)
                                continue;
                            if (x >= Width)
                                break;

                            cell = GetCell(x, y);

                            if (!cell.Valid)
                                continue;

                            bool cast = true;
                            if (cell.Objects != null)
                                for (int o = 0; o < cell.Objects.Count; o++)
                                {
                                    MapObject target = cell.Objects[o];
                                    if (target.Race != ObjectType.Spell || ( (SpellObject)target ).Spell != Spell.MobBlizzard)
                                        continue;

                                    cast = false;
                                    break;
                                }

                            if (!cast)
                                continue;

                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.MobBlizzard,
                                Value = value,
                                ExpireTime = Envir.Time + 3000,
                                TickSpeed = 440,
                                MobCaster = mob,
                                CurrentLocation = new Point(x, y),
                                CastLocation = location,
                                Show = show,
                                CurrentMap = this,
                                StartTime = Envir.Time + 800,
                            };

                            show = false;

                            AddObject(ob);
                            ob.Spawned();
                        }
                    }

                    break;

                #endregion

                #region MeteorStrike

                case Spell.MobMeteorStrike:
                    value = (int)data[2];
                    location = (Point)data[3];
                    show = true;

                    for (int y = location.Y - 2; y <= location.Y + 2; y++)
                    {
                        if (y < 0)
                            continue;
                        if (y >= Height)
                            break;

                        for (int x = location.X - 2; x <= location.X + 2; x++)
                        {
                            if (x < 0)
                                continue;
                            if (x >= Width)
                                break;

                            cell = GetCell(x, y);

                            if (!cell.Valid)
                                continue;

                            bool cast = true;
                            if (cell.Objects != null)
                                for (int o = 0; o < cell.Objects.Count; o++)
                                {
                                    MapObject target = cell.Objects[o];
                                    if (target.Race != ObjectType.Spell || ( (SpellObject)target ).Spell != Spell.MobMeteorStrike)
                                        continue;

                                    cast = false;
                                    break;
                                }

                            if (!cast)
                                continue;

                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.MobMeteorStrike,
                                Value = value,
                                ExpireTime = Envir.Time + 3000,
                                TickSpeed = 440,
                                MobCaster = mob,
                                CurrentLocation = new Point(x, y),
                                CastLocation = location,
                                Show = show,
                                CurrentMap = this,
                                StartTime = Envir.Time + 800,
                            };

                            show = false;

                            AddObject(ob);
                            ob.Spawned();
                        }
                    }

                    break;

                #endregion

                #region IceThrust

                case Spell.IceThrust:
                    {
                        location = (Point)data[2];
                        MirDirection direction = (MirDirection)data[3];

                        int nearDamage = (int)data[4];
                        int farDamage = (int)data[5];
                        magicLevel = (int)data[6];
                        
                        int col = 3;
                        int row = 3;

                        Point[] loc = new Point[col]; //0 = left 1 = center 2 = right
                        loc[0] = Functions.PointMove(location, Functions.PreviousDir(direction), 1);
                        loc[1] = Functions.PointMove(location, direction, 1);
                        loc[2] = Functions.PointMove(location, Functions.NextDir(direction), 1);

                        for (int i = 0; i < col; i++)
                        {
                            Point startPoint = loc[i];
                            for (int j = 0; j < row; j++)
                            {
                                Point hitPoint = Functions.PointMove(startPoint, direction, j);

                                if (!ValidPoint(hitPoint)) continue;

                                cell = GetCell(hitPoint);

                                if (cell.Objects == null) continue;

                                for (int k = 0; k < cell.Objects.Count; k++)
                                {
                                    MapObject target = cell.Objects[k];
                                    switch (target.Race)
                                    {
                                        case ObjectType.Monster:
                                        case ObjectType.Player:
                                        case ObjectType.Hero:
                                            if (target.IsAttackTarget(mob))
                                            {
                                                //Only targets
                                                if (target.Attacked(mob, farDamage, DefenceType.MAC) > 0)
                                                {
                                                    if (mob.Level + (target.Race == ObjectType.Player ? 2 : 10) >= target.Level && Envir.Random.Next(target.Race == ObjectType.Player ? 20 : 15) <= magicLevel)
                                                    {
                                                        target.ApplyPoison(new Poison
                                                        {
                                                            Owner = mob,
                                                            Duration = mob.Race == ObjectType.Player ? 4 : 5 + Envir.Random.Next(5),
                                                            PType = PoisonType.Slow,
                                                            TickSpeed = 1000,
                                                        }, mob);
                                                        target.OperateTime = 0;
                                                    }

                                                    if (mob.Level + (target.Race == ObjectType.Player ? 2 : 10) >= target.Level && Envir.Random.Next(target.Race == ObjectType.Player ? 40 : 25) <= magicLevel)
                                                    {
                                                        target.ApplyPoison(new Poison
                                                        {
                                                            Owner = mob,
                                                            Duration = target.Race == ObjectType.Player ? 2 : 5 + Envir.Random.Next(mob.Freezing),
                                                            PType = PoisonType.Frozen,
                                                            TickSpeed = 1000,
                                                        }, mob);
                                                        target.OperateTime = 0;
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    break;

                #endregion

                #region Lava King
                case Spell.LavaKing:
                    value = (int)data[2];
                    location = (Point)data[3];
                    {
                        show = true;

                        for (int y = location.Y - 2; y <= location.Y + 2; y++)
                        {
                            if (y < 0)
                                continue;
                            if (y >= Height)
                                break;

                            for (int x = location.X - 2; x <= location.X + 2; x++)
                            {
                                if (x < 0)
                                    continue;
                                if (x >= Width)
                                    break;

                                cell = GetCell(x, y);

                                if (!cell.Valid)
                                    continue;

                                bool cast = true;
                                if (cell.Objects != null)
                                    for (int o = 0; o < cell.Objects.Count; o++)
                                    {
                                        MapObject target = cell.Objects[o];

                                        if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.LavaKing)
                                            continue;

                                        cast = false;
                                        break;
                                    }
                                

                                if (!cast)
                                    continue;

                                SpellObject ob = new SpellObject
                                {
                                    Spell = Spell.LavaKing,
                                    Value = value,
                                    ExpireTime = Envir.Time + 3000,
                                    TickSpeed = 520,
                                    MobCaster = mob,
                                    CurrentLocation = new Point(x, y),
                                    CastLocation = location,
                                    Show = show,
                                    CurrentMap = this,
                                    StartTime = Envir.Time + 800,
                                };

                                show = false;

                                AddObject(ob);
                                ob.Spawned();
                            }
                        }
                    }

                    break;
                    #endregion
            }
        }

        private void CompleteMagic(IList<object> data)
        {
            bool train = false;
            PlayerObject player = (PlayerObject)data[0];
            UserMagic magic = (UserMagic)data[1];

            if (player == null || player.Info == null) return;

            int value, value2, pvpDamage;
            Point location;
            Cell cell;
            MirDirection dir;
            MonsterObject monster;
            Point front;
            switch (magic.Spell)
            {

                #region HellFire

                case Spell.HellFire:
                    value = (int)data[2];
                    dir = (MirDirection)data[4];
                    location = Functions.PointMove((Point)data[3], dir, 1);
                    int count = (int)data[5] - 1;
                    pvpDamage = (int)data[6];

                    if (!ValidPoint(location)) return;

                    if (count > 0)
                    {
                        DelayedAction action = new DelayedAction(DelayedType.Magic, Envir.Time + 100, player, magic, value, location, dir, count, pvpDamage);
                        ActionList.Add(action);
                    }

                    cell = GetCell(location);

                    if (cell.Objects == null) return;


                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject target = cell.Objects[i];
                        switch (target.Race)
                        {
                            case ObjectType.Monster:
                            case ObjectType.Hero:
                            case ObjectType.Player:
                                //Only targets
                                if (target.IsAttackTarget(player))
                                {
                                    if (target.Race == ObjectType.Player &&
                                        target.Attacked(player, pvpDamage, DefenceType.MAC, false) > 0)
                                        player.LevelMagic(magic);
                                    else if (target.Race != ObjectType.Player &&
                                        target.Attacked(player, value, DefenceType.MAC, false) > 0)
                                        player.LevelMagic(magic);
                                    return;
                                }
                                break;
                        }
                    }
                    break;

                #endregion

                #region SummonSkeleton, SummonShinsu, SummonHolyDeva, ArcherSummons , SummonHolyDragon

                case Spell.SummonSkeleton:
                case Spell.SummonShinsu:
                case Spell.SummonHolyDeva:
                case Spell.SummonVampire:
                case Spell.SummonToad:
                case Spell.SummonSnakes:
                case Spell.SummonHolyDragon:
                    monster = (MonsterObject)data[2];
                    front = (Point)data[3];

                    if (monster.Master.Dead) return;

                    if (ValidPoint(front))
                        monster.Spawn(this, front);
                    else
                        monster.Spawn(player.CurrentMap, player.CurrentLocation);

                    monster.Master.Pets.Add(monster);
                    break;

                #endregion

                #region FireBang, IceStorm, DragNet

                case Spell.IceStorm:
                case Spell.FireBang:
                case Spell.DragNet:
                    value = (int)data[2];
                    location = (Point)data[3];
                    pvpDamage = (int)data[4];
                    for (int y = location.Y - 1; y <= location.Y + 1; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 1; x <= location.X + 1; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            if (magic.Spell == Spell.FireBang)
                            {
                                for (int i = 0; i < cell.Objects.Count; i++)
                                {
                                    MapObject target = cell.Objects[i];
                                    switch (target.Race)
                                    {
                                        case ObjectType.Monster:
                                        case ObjectType.Player:
                                        case ObjectType.Hero:
                                            //Only targets
                                            if (target.IsAttackTarget(player))
                                            {
                                                if (target.Race == ObjectType.Player &&
                                                    target.Attacked(player, pvpDamage, DefenceType.MAC, false) > 0)
                                                    train = true;
                                                else if (target.Race != ObjectType.Player &&
                                                target.Attacked(player, value, DefenceType.MAC, false) > 0)
                                                    train = true;
                                            }
                                            break;
                                    }
                                }
                            }
                            else if (magic.Spell == Spell.DragNet)
                            {
                                for (int i = 0; i < cell.Objects.Count; i++)
                                {

                                    MapObject target = cell.Objects[i];
                                    switch (target.Race)
                                    {
                                        case ObjectType.Monster:
                                        case ObjectType.Player:
                                        case ObjectType.Hero:
                                            //Only targets
                                            if (target.IsAttackTarget(player))
                                            {
                                                int dmg = 0;

                                                if (target.Race == ObjectType.Monster && target.Undead)
                                                    dmg = value;
                                                else
                                                    dmg = value / 3;
                                                if (target.Race == ObjectType.Player &&
                                                    target.Attacked(player, dmg, DefenceType.MAC, false) > 0)
                                                {
                                                    if (target.Undead && Envir.Random.Next(5) == 0)
                                                        target.ApplyPoison(new Poison { PType = PoisonType.Stun, Duration = magic.Level + 2, TickSpeed = 1000 }, player);
                                                    train = true;
                                                }
                                                else if (target.Race != ObjectType.Player &&
                                                    target.Attacked(player, dmg, DefenceType.MAC, false) > 0)
                                                {
                                                    if (target.Undead && Envir.Random.Next(5) == 0)
                                                        target.ApplyPoison(new Poison { PType = PoisonType.Stun, Duration = magic.Level + 2, TickSpeed = 1000 }, player);

                                                    train = true;
                                                }
                                                break;
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            if (cell.Objects != null)
                                for (int i = 0; i < cell.Objects.Count; i++)
                                {

                                    MapObject target = cell.Objects[i];
                                    switch (target.Race)
                                    {
                                        case ObjectType.Monster:
                                        case ObjectType.Player:
                                        case ObjectType.Hero:
                                            //Only targets
                                            if (target.IsAttackTarget(player))
                                            {
                                                if (target.Attacked(player, pvpDamage, DefenceType.MAC, false) > 0)
                                                {
                                                    if (Envir.Random.Next(30) == 0)
                                                    {
                                                        target.ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = Envir.Random.Next(2, 6 + magic.Level), Owner = player, TickSpeed = 1000 }, player);
                                                    }
                                                    else if (Envir.Random.Next(60) == 0)
                                                    {
                                                        target.ApplyPoison(new Poison { PType = PoisonType.Frozen, Duration = Envir.Random.Next(1, 2 + magic.Level), Owner = player, TickSpeed = 1000 }, player);
                                                    }
                                                    train = true;
                                                }
                                            }
                                            break;
                                    }
                                }
                        }
                    }

                    break;

                #endregion

                #region MassHiding

                case Spell.MassHiding:
                    value = (int)data[2];
                    location = (Point)data[3];

                    for (int y = location.Y - 1; y <= location.Y + 1; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 1; x <= location.X + 1; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

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
                                        if (target.IsFriendlyTarget(player))
                                        {
                                            for (int b = 0; b < target.Buffs.Count; b++)
                                                if (target.Buffs[b].Type == BuffType.Hiding) return;

                                            target.AddBuff(new Buff { Type = BuffType.Hiding, Caster = player, ExpireTime = Envir.Time + value * 1000 });
                                            target.OperateTime = 0;
                                            train = true;
                                        }
                                        break;
                                }
                            }

                        }

                    }

                    break;

                #endregion

                #region SoulShield, BlessedArmour HolyShield
                case Spell.HolyShield:
                case Spell.SoulShield:
                case Spell.BlessedArmour:
                    value = (int)data[2];
                    location = (Point)data[3];
                    BuffType type = magic.Spell == Spell.SoulShield ? BuffType.SoulShield : magic.Spell == Spell.BlessedArmour ? BuffType.BlessedArmour : BuffType.CombinedBuff;

                    for (int y = location.Y - 3; y <= location.Y + 3; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 3; x <= location.X + 3; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

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
                                        int[] tmp;
                                        if (type == BuffType.CombinedBuff)
                                        {
                                            tmp = new int[3];
                                            tmp[0] =
                                                //  Level 0 = 4
                                                magic.Level == 0 ? 4 :
                                                //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half)
                                                4 * (magic.Level + 1) / (target.Race == ObjectType.Hero ? 2 : 1);
                                            tmp[1] =
                                                //  Level 0 = 4
                                                magic.Level == 0 ? 4 :
                                                //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half)
                                                4 * (magic.Level + 1) / (target.Race == ObjectType.Hero ? 2 : 1);
                                            tmp[2] =
                                                //  Level 0 = 4
                                                magic.Level == 0 ? 4 :
                                                //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half)
                                                4 * (magic.Level + 1) / (target.Race == ObjectType.Hero ? 2 : 1);
                                        }
                                        else
                                        {
                                            tmp = new int[1];
                                            tmp[0] =
                                                //  Level 0 = 4
                                                magic.Level == 0 ? 4 :
                                                //  Level 1 = 8, 2 = 12, 3 = 16 Hero ? / 2 (half)
                                                4 * (magic.Level + 1) / (target.Race == ObjectType.Hero ? 2 : 1);

                                            //tmp[0] = target.Level / 7 + 4;
                                        }
                                        if (target.IsFriendlyTarget(player))
                                        {
                                            target.AddBuff(new Buff { Type = type, Caster = player, ExpireTime = Envir.Time + value * 1000, Values = tmp });
                                            target.OperateTime = 0;
                                            train = true;
                                        }
                                        break;
                                }
                            }

                        }

                    }

                    break;

                #endregion

                #region MoonMist

                case Spell.MoonMist:
                    {
                        value = (int)data[2];
                        location = (Point)data[3];
                        pvpDamage = (int)data[4];
                        //if (!player.ActiveBlizzard) return;
                        cell = GetCell(location);

                        if (!cell.Valid || cell.Objects == null) return;

                        bool cast = true;
                        for (int i = 0; i < cell.Objects.Count; i++)
                        {
                            if (cell.Objects[i].Race != ObjectType.Spell || ((SpellObject)cell.Objects[i]).Spell != Spell.MoonMist) continue;
                            cast = false;
                            break;
                        }
                        if (cast)
                        {
                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.MoonMist,
                                Value = value,
                                ExpireTime = Envir.Time + 7000,
                                TickSpeed = 1000,
                                Caster = player,
                                CurrentLocation = location,
                                CurrentMap = this,
                                PvPValue = pvpDamage,
                                Show = true
                            };
                            AddObject(ob);
                            ob.Spawned();
                        }
                        else
                            return;
                        for (int y = location.Y - 2; y <= location.Y + 2; y++)
                        {
                            if (y < 0) continue;
                            if (y >= Height) break;

                            for (int x = location.X - 2; x <= location.X + 2; x++)
                            {
                                if (location.X == x &&
                                    location.Y == y)
                                    continue;
                                if (x < 0) continue;
                                if (x >= Width) break;

                                cell = GetCell(x, y);

                                if (!cell.Valid || cell.Objects == null) continue;

                                cast = true;
                                for (int i = 0; i < cell.Objects.Count; i++)
                                {
                                    if (cell.Objects[i].Race != ObjectType.Spell || ((SpellObject)cell.Objects[i]).Spell != Spell.MoonMist) continue;
                                    cast = false;
                                    break;
                                }
                                if (cast)
                                {
                                    SpellObject ob = new SpellObject
                                    {
                                        Spell = Spell.MoonMist,
                                        Value = value,
                                        ExpireTime = Envir.Time + 7000,
                                        TickSpeed = 1000,
                                        Caster = player,
                                        CurrentLocation = new Point(x, y),
                                        CurrentMap = this,
                                        PvPValue = pvpDamage,
                                        Show = false
                                    };
                                    AddObject(ob);
                                    ob.Spawned();
                                }
                            }
                        }
                    }

                    break;

                #endregion

                #region FireWall

                case Spell.FireWall:
                    value = (int)data[2];
                    location = (Point)data[3];
                    long dura = (long)data[4];
                    pvpDamage = (int)data[5];
                    player.LevelMagic(magic);

                    if (ValidPoint(location))
                    {
                        cell = GetCell(location);

                        bool cast = true;
                        if (cell.Objects != null)
                            for (int o = 0; o < cell.Objects.Count; o++)
                            {
                                MapObject target = cell.Objects[o];
                                if (target.Race != ObjectType.Spell || ( (SpellObject)target ).Spell != Spell.FireWall) continue;

                                cast = false;
                                break;
                            }

                        //  This tells the client to draw the effect and the server to do a map event (I.E the damage)
                        if (cast)
                        {
                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.FireWall,
                                Value = value,
                                ExpireTime = Envir.Time + dura,
                                //ExpireTime = Envir.Time + ( 10 + value / 2 ) * 1000,
                                TickSpeed = 2000,
                                Caster = player,
                                CurrentLocation = location,
                                CurrentMap = this,
                                PvPValue = pvpDamage
                            };
                            AddObject(ob);
                            ob.Spawned();
                        }
                    }

                    dir = MirDirection.Up;
                    for (int i = 0; i < 4; i++)
                    {
                        location = Functions.PointMove((Point)data[3], dir, 1);
                        dir += 2;

                        if (!ValidPoint(location)) continue;

                        cell = GetCell(location);
                        bool cast = true;

                        if (cell.Objects != null)
                            for (int o = 0; o < cell.Objects.Count; o++)
                            {
                                MapObject target = cell.Objects[o];
                                if (target.Race != ObjectType.Spell || ( (SpellObject)target ).Spell != Spell.FireWall) continue;

                                cast = false;
                                break;
                            }

                        if (!cast) continue;

                        SpellObject ob = new SpellObject
                        {
                            Spell = Spell.FireWall,
                            Value = value,
                            ExpireTime = Envir.Time + dura,
                            //ExpireTime = Envir.Time + ( 10 + value / 2 ) * 1000,
                            TickSpeed = 2000,
                            Caster = player,
                            CurrentLocation = location,
                            CurrentMap = this,
                            PvPValue = pvpDamage
                        };
                        AddObject(ob);
                        ob.Spawned();
                    }

                    break;

                #endregion

                #region Lightning

                case Spell.Lightning:
                    value = (int)data[2];
                    location = (Point)data[3];
                    dir = (MirDirection)data[4];
                    pvpDamage = (int)data[5];
                    for (int i = 0; i < 6; i++)
                    {
                        location = Functions.PointMove(location, dir, 1);

                        if (!ValidPoint(location)) continue;

                        cell = GetCell(location);

                        if (cell.Objects == null) continue;

                        for (int o = 0; o < cell.Objects.Count; o++)
                        {
                            MapObject target = cell.Objects[o];
                            if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;

                            if (!target.IsAttackTarget(player)) continue;
                            if (target.Race == ObjectType.Player && target.Attacked(player, pvpDamage, DefenceType.MAC, false) > 0)
                                train = true;
                            else if (target.Race != ObjectType.Player && target.Attacked(player, value, DefenceType.MAC, false) > 0)
                                train = true;
                            break;
                        }
                    }

                    break;

                #endregion

                #region HeavenlySword

                case Spell.HeavenlySword:
                    value = (int)data[2];
                    location = (Point)data[3];
                    dir = (MirDirection)data[4];
                    pvpDamage = (int)data[5];
                    for (int i = 0; i < 3; i++)
                    {
                        location = Functions.PointMove(location, dir, 1);

                        if (!ValidPoint(location)) continue;

                        cell = GetCell(location);

                        if (cell.Objects == null) continue;

                        for (int o = 0; o < cell.Objects.Count; o++)
                        {
                            MapObject target = cell.Objects[o];
                            if (target.Race != ObjectType.Player && target.Race != ObjectType.Monster && target.Race != ObjectType.Hero) continue;

                            if (!target.IsAttackTarget(player)) continue;
                            if (target.Race == ObjectType.Player &&
                                target.Attacked(player, pvpDamage, DefenceType.AC, false) > 0)
                            {
                                train = true;
                            }
                            else if (target.Race != ObjectType.Player &&
                                target.Attacked(player, value, DefenceType.AC, false) > 0)
                            {
                                train = true;
                            }
                            break;
                        }
                    }

                    break;

                #endregion

                #region MassHealing

                case Spell.MassHealing:
                    value = (int)data[2];
                    location = (Point)data[3];

                    for (int y = location.Y - 1; y <= location.Y + 1; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 1; x <= location.X + 1; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

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
                                        if (target.IsFriendlyTarget(player))
                                        {
                                            if (target.Health >= target.MaxHealth) continue;
                                            target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + value);
                                            target.OperateTime = 0;
                                            train = true;
                                        }
                                        break;
                                }
                            }

                        }

                    }

                    break;

                #endregion

                #region HealingCircle
                case Spell.HealingCircle:
                    value = (int)data[2];
                    location = (Point)data[3];
                    long duration = (long)data[4];
                    pvpDamage = (int)data[5];
                    train = true;
                    {
                        bool show = true;

                        for (int y = location.Y - 2; y <= location.Y + 2; y++)
                        {
                            if (y < 0)
                                continue;
                            if (y >= Height)
                                break;

                            for (int x = location.X - 2; x <= location.X + 2; x++)
                            {
                                if (x < 0)
                                    continue;
                                if (x >= Width)
                                    break;

                                cell = GetCell(x, y);

                                if (!cell.Valid)
                                    continue;

                                bool cast = true;
                                if (cell.Objects != null)
                                    for (int o = 0; o < cell.Objects.Count; o++)
                                    {
                                        MapObject target = cell.Objects[o];
                                        if (target.Race == ObjectType.Player ||
                                            target.Race == ObjectType.Monster ||
                                            target.Race == ObjectType.Hero)
                                        {
                                            if (target.IsFriendlyTarget(player))
                                            {
                                                if (target.Health >= target.MaxHealth) continue;
                                                target.HealAmount = (ushort)Math.Min(ushort.MaxValue, target.HealAmount + value);
                                                target.OperateTime = 0;
                                            }
                                            else if (target.IsAttackTarget(player))
                                            {
                                                if (target.Race == ObjectType.Player)
                                                    target.Attacked(player, pvpDamage, DefenceType.MAC);
                                                else
                                                    target.Attacked(player, value, DefenceType.MAC);
                                            }
                                        }
                                        if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.HealingCircle)
                                            continue;

                                        cast = false;
                                        break;
                                    }
                                if (!cast)
                                    continue;
                                SpellObject ob = new SpellObject
                                {
                                    Spell = magic.Spell,
                                    Value = value,
                                    ExpireTime = Envir.Time + duration,
                                    TickSpeed = 2000,
                                    Caster = player,
                                    CurrentLocation = new Point(x, y),
                                    CastLocation = location,
                                    Show = show,
                                    CurrentMap = this,
                                    StartTime = Envir.Time + 800,
                                    PvPValue = pvpDamage
                                };

                                show = false;

                                AddObject(ob);
                                ob.Spawned();
                            }
                        }
                    }
                    break;

                #endregion

                #region ThunderStorm

                case Spell.ThunderStorm:
                case Spell.FlameField:
                case Spell.NapalmShot:
                case Spell.StormEscape:
                    value = (int)data[2];
                    location = (Point)data[3];
                    pvpDamage = (int)data[4];
                    for (int y = location.Y - 2; y <= location.Y + 2; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 2; x <= location.X + 2; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

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
                                        if (!target.IsAttackTarget(player)) break;
                                        if (target.Race == ObjectType.Player)
                                            target.Attacked(player, magic.Spell == Spell.ThunderStorm ? pvpDamage / 10 : pvpDamage, DefenceType.MAC, false);
                                        else if (target.Race != ObjectType.Player &&
                                            target.Attacked(player, magic.Spell == Spell.ThunderStorm && !target.Undead ? value / 10 : value, DefenceType.MAC, false) <= 0)
                                        {
                                            if (target.Undead)
                                            {
                                                target.ApplyPoison(new Poison { PType = PoisonType.Stun, Duration = magic.Level + 2, TickSpeed = 1000 }, player);
                                            }
                                            break;
                                        }

                                        train = true;
                                        break;
                                }
                            }

                        }
                    }

                    break;

                #endregion

                #region LionRoar

                case Spell.LionRoar:
                    location = (Point)data[2];

                    for (int y = location.Y - 2; y <= location.Y + 2; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 2; x <= location.X + 2; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject target = cell.Objects[i];
                                if (target.Race != ObjectType.Monster) continue;
                                //Only targets
                                if (!target.IsAttackTarget(player) || player.Level + 3 < target.Level) continue;
                                target.ApplyPoison(new Poison { PType = PoisonType.LRParalysis, Duration = magic.Level + 2, TickSpeed = 1000 }, player);
                                target.OperateTime = 0;
                                train = true;
                            }

                        }

                    }

                    break;

                #endregion

                #region PoisonCloud

                case Spell.PoisonCloud:
                    value = (int)data[2];
                    location = (Point)data[3];
                    byte bonusdmg = (byte)data[4];
                    pvpDamage = (int)data[5];
                    train = true;
                    {
                        bool show = true;

                        for (int y = location.Y - 1; y <= location.Y + 1; y++)
                        {
                            if (y < 0)
                                continue;
                            if (y >= Height)
                                break;

                            for (int x = location.X - 1; x <= location.X + 1; x++)
                            {
                                if (x < 0)
                                    continue;
                                if (x >= Width)
                                    break;

                                cell = GetCell(x, y);

                                if (!cell.Valid)
                                    continue;

                                bool cast = true;
                                if (cell.Objects != null)
                                    for (int o = 0; o < cell.Objects.Count; o++)
                                    {
                                        MapObject target = cell.Objects[o];
                                        if (target.Race != ObjectType.Spell || ( (SpellObject)target ).Spell != Spell.PoisonCloud)
                                            continue;

                                        cast = false;
                                        break;
                                    }

                                if (!cast)
                                    continue;

                                SpellObject ob = new SpellObject
                                {
                                    Spell = Spell.PoisonCloud,
                                    Value = value,
                                    ExpireTime = Envir.Time + 6000,
                                    TickSpeed = 1000,
                                    Caster = player,
                                    CurrentLocation = new Point(x, y),
                                    CastLocation = location,
                                    Show = show,
                                    CurrentMap = this,
                                    PvPValue = pvpDamage
                                };

                                show = false;

                                AddObject(ob);
                                ob.Spawned();
                            }
                        }
                    }

                    break;
                    

                #endregion

                #region IceThrust

                case Spell.IceThrust:
                    {
                        location = (Point)data[2];
                        MirDirection direction = (MirDirection)data[3];
                        MirDirection _dir = direction;
                        int nearDamage = (int)data[4];
                        int farDamage = (int)data[5];
                        pvpDamage = (int)data[6];
                        Point[] loc = new Point[12]; //0 = left 1 = center 2 = right
                        // Row 1
                        loc[0] = Functions.PointMove(location, direction, 1);
                        // Row 2
                        loc[1] = Functions.PointMove(location, direction, 2);
                        _dir = Functions.PreviousDir(_dir);
                        _dir = Functions.PreviousDir(_dir);
                        loc[2] = Functions.PointMove(loc[1], _dir, 1);
                        _dir = direction;
                        _dir = Functions.NextDir(_dir);
                        _dir = Functions.NextDir(_dir);
                        loc[3] = Functions.PointMove(loc[1], _dir, 1);

                        // Row 3
                        loc[4] = Functions.PointMove(location, direction, 3);
                        _dir = direction;
                        _dir = Functions.PreviousDir(_dir);
                        _dir = Functions.PreviousDir(_dir);
                        loc[5] = Functions.PointMove(loc[4], _dir, 1);
                        loc[6] = Functions.PointMove(loc[4], _dir, 2);
                        _dir = direction;
                        _dir = Functions.NextDir(_dir);
                        _dir = Functions.NextDir(_dir);
                        loc[7] = Functions.PointMove(loc[4], _dir, 1);
                        loc[8] = Functions.PointMove(loc[4], _dir, 2);
                        // Row 4
                        loc[9] = Functions.PointMove(location, direction, 4);
                        _dir = direction;
                        _dir = Functions.PreviousDir(_dir);
                        _dir = Functions.PreviousDir(_dir);
                        loc[10] = Functions.PointMove(loc[9], _dir, 2);
                        _dir = direction;
                        _dir = Functions.NextDir(_dir);
                        _dir = Functions.NextDir(_dir);
                        loc[11] = Functions.PointMove(loc[9], _dir, 2);
                        
                        for (int i = 0; i <= 11; i++)
                        {
                            if (!ValidPoint(loc[i])) continue;
                            cell = GetCell(loc[i]);
                            if (cell.Objects == null) continue;
                            for (int k = 0; k < cell.Objects.Count; k++)
                            {
                                MapObject target = cell.Objects[k];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:
                                        if (target.IsAttackTarget(player))
                                        {
                                            //Only targets
                                            if (target.Race == ObjectType.Player &&
                                                target.Attacked(player, pvpDamage, DefenceType.MAC, false) > 0)
                                            {
                                                if (player.Level + 2  >= target.Level && Envir.Random.Next(30) <= magic.Level)
                                                {
                                                    target.ApplyPoison(new Poison
                                                    {
                                                        Owner = player,
                                                        Duration = 4,
                                                        PType = PoisonType.Slow,
                                                        TickSpeed = 1000,
                                                    }, player);
                                                    target.OperateTime = 0;
                                                }

                                                if (player.Level + 2 >= target.Level && Envir.Random.Next(60) <= magic.Level)
                                                {
                                                    target.ApplyPoison(new Poison
                                                    {
                                                        Owner = player,
                                                        Duration = 2,
                                                        PType = PoisonType.Frozen,
                                                        TickSpeed = 1000,
                                                    }, player);
                                                    target.OperateTime = 0;
                                                }

                                                train = true;
                                            }
                                            else if (target.Race != ObjectType.Player &&
                                                target.Attacked(player, farDamage, DefenceType.MAC, false) > 0)
                                            {
                                                if (player.Level + 10 >= target.Level && Envir.Random.Next(20) <= magic.Level)
                                                {
                                                    target.ApplyPoison(new Poison
                                                    {
                                                        Owner = player,
                                                        Duration = 5 + Envir.Random.Next(5),
                                                        PType = PoisonType.Slow,
                                                        TickSpeed = 1000,
                                                    }, player);
                                                    target.OperateTime = 0;
                                                }

                                                if (player.Level + 10 >= target.Level && Envir.Random.Next(40) <= magic.Level)
                                                {
                                                    target.ApplyPoison(new Poison
                                                    {
                                                        Owner = player,
                                                        Duration = 5 + Envir.Random.Next(player.Freezing),
                                                        PType = PoisonType.Frozen,
                                                        TickSpeed = 1000,
                                                    }, player);
                                                    target.OperateTime = 0;
                                                }

                                                train = true;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    break;

                #endregion

                

                #region Mirroring

                case Spell.Mirroring:
                    monster = (MonsterObject)data[2];
                    front = (Point)data[3];
                    bool finish = (bool)data[4];

                    if (finish)
                    {
                        monster.Die();
                        return;
                    };

                    if (ValidPoint(front))
                        monster.Spawn(this, front);
                    else
                        monster.Spawn(player.CurrentMap, player.CurrentLocation);
                    break;

                #endregion

                #region Blizzard    Frozen Rains

                case Spell.Blizzard:
                case Spell.FrozenRains:
                    value = (int)data[2];
                    location = (Point)data[3];
                    pvpDamage = (int)data[4];
                    train = true;
                    {
                       bool show = true;

                        for (int y = location.Y - 2; y <= location.Y + 2; y++)
                        {
                            if (y < 0)
                                continue;
                            if (y >= Height)
                                break;

                            for (int x = location.X - 2; x <= location.X + 2; x++)
                            {
                                if (x < 0)
                                    continue;
                                if (x >= Width)
                                    break;

                                cell = GetCell(x, y);

                                if (!cell.Valid)
                                    continue;

                                bool cast = true;
                                if (magic.Spell == Spell.Blizzard)
                                {
                                    if (cell.Objects != null)
                                        for (int o = 0; o < cell.Objects.Count; o++)
                                        {
                                            MapObject target = cell.Objects[o];
                                            if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.Blizzard)
                                                continue;

                                            cast = false;
                                            break;
                                        }
                                }
                                else
                                {
                                    if (cell.Objects != null)
                                        for (int o = 0; o < cell.Objects.Count; o++)
                                        {
                                            MapObject target = cell.Objects[o];
                                            if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.FrozenRains)
                                                continue;

                                            cast = false;
                                            break;
                                        }
                                }
                                if (!cast)
                                    continue;
                                //  Blizzard doesn't do an initial hit because it uses
                                //  Map event to do the damage
                                SpellObject ob = new SpellObject
                                {
                                    Spell = magic.Spell,
                                    Value = value,
                                    ExpireTime = Envir.Time + 3000,
                                    TickSpeed = 520,
                                    Caster = player,
                                    CurrentLocation = new Point(x, y),
                                    CastLocation = location,
                                    Show = show,
                                    CurrentMap = this,
                                    StartTime = Envir.Time + 800,
                                    PvPValue = pvpDamage
                                };

                                show = false;

                                AddObject(ob);
                                ob.Spawned();
                            }
                        }
                    }

                    break;

                #endregion     

                #region MeteorStrike    Lava King

                case Spell.MeteorStrike:
                case Spell.LavaKing:
                    value = (int)data[2];
                    location = (Point)data[3];
                    pvpDamage = (int)data[4];
                    train = true;
                    {
                        bool show = true;

                        for (int y = location.Y - 2; y <= location.Y + 2; y++)
                        {
                            if (y < 0)
                                continue;
                            if (y >= Height)
                                break;

                            for (int x = location.X - 2; x <= location.X + 2; x++)
                            {
                                if (x < 0)
                                    continue;
                                if (x >= Width)
                                    break;

                                cell = GetCell(x, y);

                                if (!cell.Valid)
                                    continue;

                                bool cast = true;
                                if (magic.Spell == Spell.MeteorStrike)
                                {
                                    if (cell.Objects != null)
                                        for (int o = 0; o < cell.Objects.Count; o++)
                                        {
                                            MapObject target = cell.Objects[o];

                                            if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.MeteorStrike)
                                                continue;

                                            cast = false;
                                            break;
                                        }
                                }
                                else
                                {
                                    if (cell.Objects != null)
                                        for (int o = 0; o < cell.Objects.Count; o++)
                                        {
                                            MapObject target = cell.Objects[o];

                                            if (target.Race != ObjectType.Spell || ((SpellObject)target).Spell != Spell.LavaKing)
                                                continue;

                                            cast = false;
                                            break;
                                        }
                                }

                                if (!cast)
                                    continue;

                                SpellObject ob = new SpellObject
                                {
                                    Spell = magic.Spell,
                                    Value = value,
                                    ExpireTime = Envir.Time + 3000,
                                    TickSpeed = 520,
                                    Caster = player,
                                    CurrentLocation = new Point(x, y),
                                    CastLocation = location,
                                    Show = show,
                                    CurrentMap = this,
                                    StartTime = Envir.Time + 800,
                                    PvPValue = pvpDamage
                                };

                                show = false;

                                AddObject(ob);
                                ob.Spawned();
                            }
                        }
                    }

                    break;

                #endregion

                #region TrapHexagon

                case Spell.TrapHexagon:
                    value = (int)data[2];
                    location = (Point)data[3];

                    MonsterObject centerTarget = null;

                    for (int y = location.Y - 1; y <= location.Y + 1; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 1; x <= location.X + 1; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject target = cell.Objects[i];

                                if (y == location.Y && x == location.X && target.Race == ObjectType.Monster)
                                {
                                    centerTarget = (MonsterObject)target;
                                }
                                
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                        if (target == null || !target.IsAttackTarget(player) || target.Node == null || target.Level > player.Level + 2) continue;

                                        MonsterObject mobTarget = (MonsterObject)target;

                                        if (centerTarget == null) centerTarget = mobTarget;

                                        mobTarget.ShockTime = Envir.Time + value;
                                        mobTarget.Target = null;
                                        break;
                                }
                            }

                        }
                    }

                    if (centerTarget == null) return;

                    for (byte i = 0; i < 8; i += 2)
                    {
                        Point startpoint = Functions.PointMove(location, (MirDirection)i, 2);
                        for (byte j = 0; j <= 4; j += 4)
                        {
                            MirDirection spawndirection = i == 0 || i == 4 ? MirDirection.Right : MirDirection.Up;
                            Point spawnpoint = Functions.PointMove(startpoint, spawndirection + j, 1);
                            if (spawnpoint.X <= 0 || spawnpoint.X > centerTarget.CurrentMap.Width) continue;
                            if (spawnpoint.Y <= 0 || spawnpoint.Y > centerTarget.CurrentMap.Height) continue;
                            SpellObject ob = new SpellObject
                            {
                                Spell = Spell.TrapHexagon,
                                ExpireTime = Envir.Time + value,
                                TickSpeed = 100,
                                Caster = player,
                                CurrentLocation = spawnpoint,
                                CastLocation = location,
                                CurrentMap = centerTarget.CurrentMap,
                                Target = centerTarget,
                            };

                            centerTarget.CurrentMap.AddObject(ob);
                            ob.Spawned();
                        }
                    }

                    train = true;

                    break;

                #endregion

                #region Curse

                case Spell.Curse:
                    value = (int)data[2];
                    location = (Point)data[3];
                    value2 = (int)data[4];
                    type = BuffType.Curse;

                    for (int y = location.Y - 3; y <= location.Y + 3; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 3; x <= location.X + 3; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

                            if (!cell.Valid || cell.Objects == null) continue;

                            for (int i = 0; i < cell.Objects.Count; i++)
                            {
                                MapObject target = cell.Objects[i];
                                switch (target.Race)
                                {
                                    case ObjectType.Monster:
                                    case ObjectType.Player:
                                    case ObjectType.Hero:

                                        if (Envir.Random.Next(10) >= 4) continue;

                                        //Only targets
                                        if (target.IsAttackTarget(player))
                                        {
                                            duration = magic.Level == 0 ? 1 :
                                                magic.Level == 1 ? 2 :
                                                magic.Level == 2 ? 3 : 4;

                                            target.ApplyPoison(new Poison { PType = PoisonType.Slow, Duration = target.Race == ObjectType.Player ? duration : value, TickSpeed = 1000, Value = value2 }, player);
                                            target.AddBuff(new Buff { Type = type, Caster = player, ExpireTime = Envir.Time + (target.Race == ObjectType.Player ? duration : value) * 1000, Values = new int[] { value2 } });
                                            target.OperateTime = 0;
                                            train = true;
                                        }
                                        break;
                                }
                            }

                        }

                    }

                    break;

                #endregion

                #region ExplosiveTrap

                case Spell.ExplosiveTrap:
                    value = (int)data[2];
                    front = (Point)data[3];
                    int trapID = (int)data[4];

                    if (ValidPoint(front))
                    {
                        cell = GetCell(front);

                        bool cast = true;
                        if (cell.Objects != null)
                            for (int o = 0; o < cell.Objects.Count; o++)
                            {
                                MapObject target = cell.Objects[o];
                                if (target.Race != ObjectType.Spell || (((SpellObject)target).Spell != Spell.FireWall && ((SpellObject)target).Spell != Spell.ExplosiveTrap)) continue;

                                cast = false;
                                break;
                            }

                        if (cast)
                        {
                            player.LevelMagic(magic);
                            System.Drawing.Point[] Traps = new Point[3];
                            Traps[0] = front;
                            Traps[1] = Functions.Left(front, player.Direction);
                            Traps[2] = Functions.Right(front, player.Direction);
                            for (int i = 0; i <= 2; i++)
                            {
                                SpellObject ob = new SpellObject
                                {
                                    Spell = Spell.ExplosiveTrap,
                                    Value = value,
                                    ExpireTime = Envir.Time + (10 + value / 2) * 1000,
                                    TickSpeed = 500,
                                    Caster = player,
                                    CurrentLocation = Traps[i],
                                    CurrentMap = this,
                                    ExplosiveTrapID = trapID,
                                    ExplosiveTrapCount = i
                                };
                                AddObject(ob);
                                ob.Spawned();
                                player.ArcherTrapObjectsArray[trapID, i] = ob;
                            }
                        }
                    }
                    break;

                #endregion

                #region Trap

                case Spell.Trap:
                    value = (int)data[2];
                    location = (Point)data[3];
                    MonsterObject selectTarget = null;

                    if (!ValidPoint(location)) break;

                    cell = GetCell(location);

                    if (!cell.Valid || cell.Objects == null) break;

                    for (int i = 0; i < cell.Objects.Count; i++)
                    {
                        MapObject target = cell.Objects[i];
                        if (target.Race == ObjectType.Monster)
                        {
                            selectTarget = (MonsterObject)target;

                            if (selectTarget == null || !selectTarget.IsAttackTarget(player) || selectTarget.Node == null || selectTarget.Level >= player.Level + 2) continue;
                            selectTarget.ShockTime = Envir.Time + value;
                            selectTarget.Target = null;
                            break;
                        }
                    }

                    if (selectTarget == null) return;

                    if (location.X <= 0 || location.X > selectTarget.CurrentMap.Width) break;
                    if (location.Y <= 0 || location.Y > selectTarget.CurrentMap.Height) break;
                    SpellObject spellOb = new SpellObject
                    {
                        Spell = Spell.Trap,
                        ExpireTime = Envir.Time + value,
                        TickSpeed = 100,
                        Caster = player,
                        CurrentLocation = location,
                        CastLocation = location,
                        CurrentMap = selectTarget.CurrentMap,
                        Target = selectTarget,
                    };

                    selectTarget.CurrentMap.AddObject(spellOb);
                    spellOb.Spawned();

                    train = true;
                    break;

                #endregion

                #region OneWithNature

                case Spell.OneWithNature:
                    value = (int)data[2];
                    location = (Point)data[3];

                    bool hasVampBuff = (player.Buffs.Where(ex => ex.Type == BuffType.VampireShot).ToList().Count() > 0);
                    bool hasPoisonBuff = (player.Buffs.Where(ex => ex.Type == BuffType.PoisonShot).ToList().Count() > 0);

                    for (int y = location.Y - 2; y <= location.Y + 2; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 2; x <= location.X + 2; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

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
                                        if (!target.IsAttackTarget(player) || target.Dead) break;

                                        //knockback
                                        //int distance = 1 + Math.Max(0, magic.Level - 1) + Envir.Random.Next(2);
                                        //dir = Functions.DirectionFromPoint(location, target.CurrentLocation);
                                        //if(target.Level < player.Level)
                                        //    target.Pushed(player, dir, distance);// <--crashes server somehow?

                                        if (target.Attacked(player, value, DefenceType.MAC, false) <= 0) break;

                                        if (hasVampBuff)//Vampire Effect
                                        {
                                            if (player.VampAmount == 0) player.VampTime = Envir.Time + 1000;
                                            player.VampAmount += (ushort)(value * (magic.Level + 1) * 0.25F);
                                        }
                                        if (hasPoisonBuff)//Poison Effect
                                        {
                                            target.ApplyPoison(new Poison
                                            {
                                                Duration = (value * 2) + (magic.Level + 1) * 7,
                                                Owner = player,
                                                PType = PoisonType.Green,
                                                TickSpeed = 2000,
                                                Value = value / 15 + magic.Level + 1 + Envir.Random.Next(player.PoisonAttack)
                                            }, player);
                                            target.OperateTime = 0;
                                        }
                                        train = true;
                                        break;
                                }
                            }

                        }
                    }

                    if (hasVampBuff)//Vampire Effect
                    {
                        //cancel out buff
                        player.AddBuff(new Buff { Type = BuffType.VampireShot, Caster = player, ExpireTime = Envir.Time + 1000, Values = new int[]{ value }, Visible = true, ObjectID = player.ObjectID });
                    }
                    if (hasPoisonBuff)//Poison Effect
                    {
                        //cancel out buff
                        player.AddBuff(new Buff { Type = BuffType.PoisonShot, Caster = player, ExpireTime = Envir.Time + 1000, Values = new int[]{ value }, Visible = true, ObjectID = player.ObjectID });
                    }
                    break;

                #endregion

                #region Portal

                case Spell.Portal:                  
                    value = (int)data[2];
                    location = (Point)data[3];
                    value2 = (int)data[4];

                    spellOb = new SpellObject
                    {
                        Spell = Spell.Portal,
                        Value = value2,
                        ExpireTime = Envir.Time + value * 1000,
                        TickSpeed = 2000,
                        Caster = player,
                        CurrentLocation = location,
                        CurrentMap = this,
                    };

                    if (player.PortalObjectsArray[0] == null)
                    {
                        player.PortalObjectsArray[0] = spellOb;
                    }
                    else
                    {
                        player.PortalObjectsArray[1] = spellOb;
                        player.PortalObjectsArray[1].ExitMap = player.PortalObjectsArray[0].CurrentMap;
                        player.PortalObjectsArray[1].ExitCoord = player.PortalObjectsArray[0].CurrentLocation;

                        player.PortalObjectsArray[0].ExitMap = player.PortalObjectsArray[1].CurrentMap;
                        player.PortalObjectsArray[0].ExitCoord = player.PortalObjectsArray[1].CurrentLocation;
                    }

                    AddObject(spellOb);
                    spellOb.Spawned();
                    train = true;
                    break;

                #endregion

                #region DelayedExplosion

                case Spell.DelayedExplosion:
                    value = (int)data[2];
                    location = (Point)data[3];

                    for (int y = location.Y - 1; y <= location.Y + 1; y++)
                    {
                        if (y < 0) continue;
                        if (y >= Height) break;

                        for (int x = location.X - 1; x <= location.X + 1; x++)
                        {
                            if (x < 0) continue;
                            if (x >= Width) break;

                            cell = GetCell(x, y);

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
                                        if (target.IsAttackTarget(player))
                                        {
                                            if (target.Attacked(player, value, DefenceType.MAC, false) > 0)
                                                train = false;//wouldnt want to make the skill give twice the points
                                        }
                                        break;
                                }
                            }

                        }

                    }

                    break;

                #endregion
            }

            if (train)
                player.LevelMagic(magic);

        }

        public void AddObject(MapObject ob)
        {
            if (ob.Race == ObjectType.Player)
            {
                Players.Add((PlayerObject)ob);
                InactiveTime = Envir.Time;
            }
            if (ob.Race == ObjectType.Merchant)
                NPCs.Add((NPCObject)ob);

            GetCell(ob.CurrentLocation).Add(ob);
        }

        public void RemoveObject(MapObject ob)
        {
            if (ob.Race == ObjectType.Player) Players.Remove((PlayerObject)ob);
            if (ob.Race == ObjectType.Merchant) NPCs.Remove((NPCObject)ob);

            GetCell(ob.CurrentLocation).Remove(ob);
        }




        public SafeZoneInfo GetSafeZone(Point location)
        {
            for (int i = 0; i < Info.SafeZones.Count; i++)
            {
                SafeZoneInfo szi = Info.SafeZones[i];
                if (Functions.InRange(szi.Location, location, szi.Size))
                    return szi;
            }
            return null;
        }

        public ConquestObject GetConquest(Point location)
        {
            for (int i = 0; i < Conquest.Count; i++)
            {
                ConquestObject swi = Conquest[i];
                if ((swi.Info.FullMap || Functions.InRange(swi.Info.Location, location, swi.Info.Size)) && swi.WarIsOn)
                    return swi;
            }
            return null;
        }

  

        public void Broadcast(Packet p, Point location)
        {
            if (p == null) return;

            for (int i = Players.Count - 1; i >= 0; i--)
            {
                PlayerObject player = Players[i];

                if (Functions.InRange(location, player.CurrentLocation, Globals.DataRange))
                    player.Enqueue(p);
                    
            }
        }

        public void BroadcastNPC(Packet p, Point location)
        {
            if (p == null) return;

            for (int i = Players.Count - 1; i >= 0; i--)
            {
                PlayerObject player = Players[i];

                if (Functions.InRange(location, player.CurrentLocation, Globals.DataRange))
                    player.Enqueue(p);

            }
        }


        public void Broadcast(Packet p, Point location, PlayerObject Player)
        {
            if (p == null) return;

            if (Functions.InRange(location, Player.CurrentLocation, Globals.DataRange))
            {
                Player.Enqueue(p);
            }    
        }

        public bool Inactive(int count = 5)
        {
            //temporary test for server speed. Stop certain processes if no players.
            if (InactiveCount > count) return true;

            return false;
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", Info.Index, Info.Title);
            //return string.Format("{0}", Name);
        }
    }
    public class Cell
    {
        public static readonly Cell HighWall = new Cell { Attribute = CellAttribute.HighWall };
        public static readonly Cell LowWall = new Cell { Attribute = CellAttribute.LowWall };

        public bool HasWall
        {
            get { return Attribute == CellAttribute.HighWall || Attribute == CellAttribute.LowWall; }
        }

        public bool Valid
        {
            get { return Attribute == CellAttribute.Walk; }
        }

        public List<MapObject> Objects;
        public CellAttribute Attribute;
        public sbyte FishingAttribute = -1;

        public void Add(MapObject mapObject)
        {
            if (Objects == null) Objects = new List<MapObject>();

            Objects.Add(mapObject);
        }
        public void Remove(MapObject mapObject)
        {
            if (mapObject == null || Objects == null)
                return;
            Objects.Remove(mapObject);
            if (Objects.Count == 0) Objects = null;
        }

        public override string ToString()
        {
            if (Objects == null)
            {
                return string.Format("0 Objects");
            }
            else
                return string.Format("{0} Objects", Objects.Count);
        }
    }
    public class MapRespawn
    {
        public RespawnInfo Info;
        public MonsterInfo Monster;
        public Map Map;
        public int Count;
        public long RespawnTime;
        public ulong NextSpawnTick;
        public byte ErrorCount = 0;

        public List<RouteInfo> Route;
        public bool IsEventObjective = false;
        public PublicEvent Event;
        public MapRespawn(RespawnInfo info)
        {
            Info = info;
            Monster = SMain.Envir.GetMonsterInfo(info.MonsterIndex);

            LoadRoutes();
        }
        public bool Spawn()
        {
            MonsterObject ob = MonsterObject.GetMonster(Monster);
            if (ob == null) return true;
            return ob.Spawn(this);
        }

        public void LoadRoutes()
        {
            Route = new List<RouteInfo>();

            if (string.IsNullOrEmpty(Info.RoutePath)) return;

            string fileName = Path.Combine(Settings.RoutePath, Info.RoutePath + ".txt");

            if (!File.Exists(fileName)) return;

            List<string> lines = File.ReadAllLines(fileName).ToList();

            foreach (string line in lines)
            {
                RouteInfo info = RouteInfo.FromText(line);

                if (info == null) continue;

                Route.Add(info);
            }
        }
    }
}
