using System;
using _13B_REW.Bancho.Packets.Objects.Serializables;

namespace _13B_REW.Bancho.Database {
    public class DatabaseUser {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime JoinDate { get; set; }

        public bool Banned { get; set; }
        public string BannedReason { get; set; }

        public string EmailAddress { get; set; }

        public string Location { get; set; }
        public string Occupation { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string Discord { get; set; }



        public long StandardRankedScore { get; set; }
        public long TaikoRankedScore { get; set; }
        public long CatchRankedScore { get; set; }

        public long StandardTotalScore { get; set; }
        public long TaikoTotalScore { get; set; }
        public long CatchTotalScore { get; set; }



        public double StandardLevel { get; set; }
        public double TaikoLevel { get; set; }
        public double CatchLevel { get; set; }

        public int StandardPlaycount { get; set; }
        public int TaikoPlaycount { get; set; }
        public int CatchPlaycount { get; set; }



        public int StandardCountSSH { get; set; }
        public int TaikoCountSSH { get; set; }
        public int CatchCountSSH { get; set; }

        public int StandardCountSS { get; set; }
        public int TaikoCountSS { get; set; }
        public int CatchCountSS { get; set; }

        public int StandardCountSH { get; set; }
        public int TaikoCountSH { get; set; }
        public int CatchCountSH { get; set; }

        public int StandardCountS { get; set; }
        public int TaikoCountS { get; set; }
        public int CatchCountS { get; set; }

        public int StandardCountA { get; set; }
        public int TaikoCountA { get; set; }
        public int CatchCountA { get; set; }

        public int StandardCountB { get; set; }
        public int TaikoCountB { get; set; }
        public int CatchCountB { get; set; }

        public int StandardCountC { get; set; }
        public int TaikoCountC { get; set; }
        public int CatchCountC { get; set; }

        public int StandardCountD { get; set; }
        public int TaikoCountD { get; set; }
        public int CatchCountD { get; set; }



        public int StandardAcc300 { get; set; }
        public int TaikoAcc300 { get; set; }
        public int CatchAcc300 { get; set; }

        public int StandardAcc100 { get; set; }
        public int TaikoAcc100 { get; set; }
        public int CatchAcc100 { get; set; }

        public int StandardAcc50 { get; set; }
        public int TaikoAcc50 { get; set; }
        public int CatchAcc50 { get; set; }

        public int StandardAccGeki { get; set; }
        public int TaikoAccGeki { get; set; }
        public int CatchAccGeki { get; set; }

        public int StandardAccKatu { get; set; }
        public int TaikoAccKatu { get; set; }
        public int CatchAccKatu { get; set; }

        public int StandardAccMiss { get; set; }
        public int TaikoAccMiss { get; set; }
        public int CatchAccMiss { get; set; }



        public int ReplaysWatched { get; set; }
        public sbyte Priviledges { get; set; }
    }
}
