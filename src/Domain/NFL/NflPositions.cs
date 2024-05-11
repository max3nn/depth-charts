using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.NFL
{
    public static class NflPositions
    {
        //Note: All positions that have multiple rows with the same 'Pos', follow the naming convention of AA, AA2, etc.

        // Offense
        public static readonly string LWR = "LWR";
        public static readonly string RWR = "RWR";
        public static readonly string LT = "LT";
        public static readonly string LG = "LG";
        public static readonly string C = "C";
        public static readonly string RG = "RG";
        public static readonly string RT = "RT";
        public static readonly string TE = "TE";
        public static readonly string TE2 = "TE";
        public static readonly string QB = "QB";
        public static readonly string RB = "RB";

        // Defense
        public static readonly string DE = "DE";
        public static readonly string NT = "NT";
        public static readonly string DE1 = "DE1";  
        public static readonly string OLB = "OLB";
        public static readonly string ILB = "ILB";
        public static readonly string ILB2 = "ILB2";
        public static readonly string OLB2 = "OLB2";
        public static readonly string CB = "CB";
        public static readonly string SS = "SS";
        public static readonly string FS = "FS";
        public static readonly string RCB = "RCB";

        // Special NFLTeams
        public static readonly string PT = "PT";
        public static readonly string PK = "PK";
        public static readonly string LS = "LS";
        public static readonly string H = "H";
        public static readonly string KO = "KO";
        public static readonly string PR = "PR";
        public static readonly string KR = "KR";

        // Reserves
        public static readonly string RES = "RES";
        public static readonly string RES2 = "RES2";
        public static readonly string FUT = "FUT";
        public static readonly string FUT2 = "FUT2";
        public static readonly string FUT3 = "FUT3";
        public static readonly string FUT4 = "FUT4";


    }
}
