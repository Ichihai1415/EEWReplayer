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

            public DetailedIntensity(string eewShindo)
            {
                eewShindo = eewShindo.Replace(" ", "").Replace("最大震度５弱程度以上と推定", "<level>").Replace("最大", "").Replace("震度", "").Replace("程度", "").Replace("と推定", "");
                //この時点で（震度）以上か<level>に　「と推定」は初期のみ
                if (eewShindo.Contains("以上"))
                {
                    var iInt = ConvertSource.Shindo_StringEnum[eewShindo.Replace("以上", "")];
                    this = new DetailedIntensity(iInt, Intensity.Over, iInt);
                }
                else if (eewShindo.Contains("から"))
                {
                    var sInts = eewShindo.Split("から");
                    var fInt = ConvertSource.Shindo_StringEnum[sInts[0]];
                    var tInt = ConvertSource.Shindo_StringEnum[sInts[1]];
                    this = new DetailedIntensity(fInt, tInt, tInt);
                }
                else
                {
                    var iInt = ConvertSource.Shindo_StringEnum[eewShindo];
                    this = new DetailedIntensity(iInt, iInt, iInt);
                }
            }

            public readonly DetailedIntensity DeepClone() => new(this);

            public enum ToStringPattern
            {
                JMA = 1,
                Original = 2
            }

            public readonly string ToString(ToStringPattern mode)
            {
                if (mode == ToStringPattern.JMA)
                {
                    if (To == Intensity.Level)
                        return "最大震度５弱程度以上と推定";
                    else if ((int)From > 10)
                        return ConvertSource.Shindo_EnumString[From];
                    else if (To == Intensity.Over)
                        return "震度" + ConvertSource.Shindo_EnumString[From] + "以上";
                    else if (From != To)
                        return "震度" + ConvertSource.Shindo_EnumString[From] + "から" + ConvertSource.Shindo_EnumString[To] + "程度";
                    else if ((int)From >= 0)
                        return "震度" + ConvertSource.Shindo_EnumString[From] + "程度";
                    else if (ConvertSource.Shindo_EnumString.TryGetValue(From, out string? value))
                        return value;
                    else
                        return "";
                }
                else
                    throw new NotImplementedException();
            }

            public override readonly string ToString() => ToString(ToStringPattern.JMA);


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
