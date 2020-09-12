using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Server.MirEnvir;

namespace Server.MirDatabase
{
    public class MovementInfo
    {
        public int MapIndex;
        public Point Source, Destination;
        public bool NeedHole, NeedMove;
        public int ConquestIndex;

        public MovementInfo()
        {

        }

        public MovementInfo(BinaryReader reader)
        {
            MapIndex = reader.ReadInt32();
            Source = new Point(reader.ReadInt32(), reader.ReadInt32());
            Destination = new Point(reader.ReadInt32(), reader.ReadInt32());

            if (Envir.LoadVersion < 16) return;
            NeedHole = reader.ReadBoolean();

            if (Envir.LoadVersion < 48) return;
            NeedMove = reader.ReadBoolean();

            if (Envir.LoadVersion < 69) return;
            ConquestIndex = reader.ReadInt32();
            /*
            File.AppendAllText(Envir.exportInfo + @".\MapInfo\MovementInfo\LOAD_MovementInfo_.txt", string.Format("Map Index {0}\nSource X {1} Y {2}\nDestination X {3} Y {4}\nNeed Hole {5}\nNeed Move {6}\nConquest Index {7}\n\n\n\n",
                    MapIndex,
                    Source.X,
                    Source.Y,
                    Destination.X,
                    Destination.Y,
                    NeedHole,
                    NeedMove,
                    ConquestIndex));
                    */
        }
        public void Save(BinaryWriter writer)
        {
            writer.Write(MapIndex);
            writer.Write(Source.X);
            writer.Write(Source.Y);
            writer.Write(Destination.X);
            writer.Write(Destination.Y);
            writer.Write(NeedHole);
            writer.Write(NeedMove);
            writer.Write(ConquestIndex);
            /*
            File.AppendAllText(Envir.exportInfo + @".\MapInfo\MovementInfo\SAVE_MovementInfo_.txt", string.Format("Map Index {0}\nSource X {1} Y {2}\nDestination X {3} Y {4}\nNeed Hole {5}\nNeed Move {6}\nConquest Index {7}\n\n\n\n",
                MapIndex,
                Source.X,
                Source.Y,
                Destination.X,
                Destination.Y,
                NeedHole,
                NeedMove,
                ConquestIndex));
                */
        }


        public override string ToString()
        {
            return string.Format("{0} -> Map :{1} - {2}", Source, MapIndex, Destination);
        }
    }
}
