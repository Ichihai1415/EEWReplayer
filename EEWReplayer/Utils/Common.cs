using System.Text.Json.Serialization;
using static EEWReplayer.Utils.DataConverter;

namespace EEWReplayer.Utils
{
    public static class Common
    {
        public enum Intensity
        {
            Null = -9,
            Level = -5,
            Over = -3,//toのみ
            Unknown = -2,
            None = -1,
            S0 = 0,
            S1 = 1,
            S2 = 2,
            S3 = 3,
            S4 = 4,
            S5l = 5,
            S5u = 6,
            S6l = 7,
            S6u = 8,
            S7 = 9,
            L0 = 10,
            L1 = 11,
            L2 = 12,
            L3 = 13,
            L4 = 14,
        }

        public struct DetailedIntensity : IEquatable<DetailedIntensity>
        {
            public Intensity From { get; set; } = Intensity.Null;
            public Intensity To { get; set; } = Intensity.Null;
            public Intensity Max { get; set; } = Intensity.Null;

            [JsonIgnore]
            public readonly bool IsNull => From == Intensity.Null;

            public DetailedIntensity()
            {
                From = Intensity.Null;
                To = Intensity.Null;
                Max = Intensity.Null;
            }

            public DetailedIntensity(Intensity intensity)
            {
                From = intensity;
                To = intensity;
                Max = intensity;
            }

            public DetailedIntensity(Intensity from, Intensity to, Intensity max = Intensity.Null)
            {
                From = from;
                To = to;
                Max = max != Intensity.Null ? max : to == Intensity.Over ? from : to;
            }

            public DetailedIntensity(DetailedIntensity src)
            {
                From = src.From;
                To = src.To;
                Max = src.Max;
            }

            public readonly DetailedIntensity DeepClone() => new(this);

            public enum ToStringPattern
            {
                JMAweb = 1,
                Original = 2
            }

            public readonly string ToString(ToStringPattern mode)
            {
                if (mode == ToStringPattern.JMAweb)
                {
                    if (To == Intensity.Level)
                        return "最大震度５弱程度以上と推定";
                    else if ((int)From > 9)
                        return "長周期地震動階級" + Intensity_Enum2JMAwebString_single(From);
                    else if (To == Intensity.Over)
                        return "震度" + Intensity_Enum2JMAwebString_single(From) + "以上";
                    else if (From != To)
                        return "震度" + Intensity_Enum2JMAwebString_single(From) + "から" + Intensity_Enum2JMAwebString_single(To) + "程度";
                    else if ((int)From >= 0)
                        return "震度" + Intensity_Enum2JMAwebString_single(From) + "程度";
                    else if (From == Intensity.Unknown)
                        return "震度不明";
                    else if (From == Intensity.None)
                        return "震度なし";
                    else if (From == Intensity.Null)
                        return "(震度情報なし)";
                    else
                        throw new NotImplementedException("実装すべきですが実装されていないようです。");
                }
                else
                    throw new ArgumentException("モードが正しくありません。");
            }

            public override readonly string ToString() => ToString(ToStringPattern.JMAweb);


            public readonly bool Equals(DetailedIntensity other)
            {
                return From == other.From && To == other.To && Max == other.Max;
            }

            public override readonly bool Equals(object? obj)
            {
                if (obj is DetailedIntensity other)
                {
                    return Equals(other);
                }
                return false;
            }

            public override readonly int GetHashCode()
            {
                return HashCode.Combine(From, To, Max);
            }

            public static bool operator ==(DetailedIntensity left, DetailedIntensity right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(DetailedIntensity left, DetailedIntensity right)
            {
                return !(left == right);
            }
        }

    }

}
