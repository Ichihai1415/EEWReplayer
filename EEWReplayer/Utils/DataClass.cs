using static EEWReplayer.Utils.Common;

namespace EEWReplayer.Utils
{
    public class Data
    {
        /*
         * (Data) <Object("Data")>
         * |- Description <string>
         * |- Earthquakes <Array<Object(class)("Earthquake")>>
         * |  |- OriginTime <DateTime>
         * |  |- HypoName <string>
         * |  |- HypoLat <double>
         * |  |- HypoLon <double>
         * |  |- HypoDepth <double>
         * |  |- Magnitude <double>
         * |  |- MaxIntensity <enum("Intensity")>
         * |
         * |-EEWLists <Array<Object(class)("EEWList")>>
         *   |- ID <string>
         *   |- EEWs <Array<Object(class)(EEW)>>
         *      |- Serial <int>
         *      |- UpdateTime <DateTime>
         *      |- OriginTime <DateTime>
         *      |- HypoName <string>
         *      |- HypoLat <double>
         *      |- HypoLon <double>
         *      |- HypoDepth <double>
         *      |- Magnitude <double>
         *      |- IsWarn <bool>
         *      |- MaxIntensity <enum("Intensity")>
         *      |- IntensityAreas[] <Array<Object(class("IntensityArea")>>
         *         |- MaxIntensityD <Object(struct("DetailedIntensity"))>
         *         |  |- From <enum("Intensity")>
         *         |  |- To <enum("Intensity")>
         *         |  |- Max <enum("Intensity")>
         *         |
         *         |- Areas <string>
         *         |- AreaCodes <Array<int>>
         */


        public Data() { }
        public Data(Data src)
        {
            Description = src.Description;
            Earthquakes = [.. src.Earthquakes.Select(srcEq => srcEq.DeepCopy())];
            EEWLists = [.. src.EEWLists.Select(srcEEWLists => srcEEWLists)];
        }

        public Data DeepCopy() => new(this);

        public string Version { get; set; } = "";

        public DateTime Created { get; set; } = DateTime.MinValue;

        public string Description { get; set; } = "";

        public Earthquake[] Earthquakes { get; set; } = [];
        public class Earthquake
        {
            public Earthquake() { }

            public Earthquake(Earthquake src)
            {
                ID = src.ID;
                OriginTime = src.OriginTime;
                HypoName = src.HypoName;
                HypoLat = src.HypoLat;
                HypoLon = src.HypoLon;
                HypoDepth = src.HypoDepth;
                Magnitude = src.Magnitude;
                MaxIntensity = src.MaxIntensity;
            }
            public Earthquake DeepCopy() => new(this);

            public string ID { get; set; } = "";

            public DateTime OriginTime { get; set; } = DateTime.MinValue;
            public string HypoName { get; set; } = "";
            public double HypoLat { get; set; } = double.NaN;
            public double HypoLon { get; set; } = double.NaN;
            public double HypoDepth { get; set; } = double.NaN;
            public double Magnitude { get; set; } = double.NaN;
            public Intensity MaxIntensity { get; set; } = Intensity.Null;

        }

        public EEWList[] EEWLists { get; set; } = [];

        public class EEWList
        {
            public EEWList() { }
            public EEWList(EEW[] src, string id = "")
            {
                ID = id;
                EEWs = src;
            }
            public EEWList(EEWList src)
            {
                ID = src.ID;
                EEWs = [.. src.EEWs.Select(srcEEW => srcEEW.DeepCopy())];
            }
            public EEWList DeepCopy() => new(this);

            public string ID { get; set; } = "";
            public EEW[] EEWs { get; set; } = [];

            public class EEW
            {
                public EEW() { }

                public EEW(EEW src)
                {
                    Serial = src.Serial;
                    UpdateTime = src.UpdateTime;
                    OriginTime = src.OriginTime;
                    HypoName = src.HypoName;
                    HypoLat = src.HypoLat;
                    HypoLon = src.HypoLon;
                    HypoDepth = src.HypoDepth;
                    Magnitude = src.Magnitude;
                    IsWarn = src.IsWarn;
                    MaxIntensityD = src.MaxIntensityD;
                    MaxIntensityLgD = src.MaxIntensityLgD;
                    IntensityAreas = [.. src.IntensityAreas.Select(srcArea => srcArea.DeepCopy())];
                }

                public EEW DeepCopy() => new(this);

                public int Serial { get; set; } = 0;
                public DateTime UpdateTime { get; set; } = DateTime.MinValue;
                public DateTime OriginTime { get; set; } = DateTime.MinValue;
                public string HypoName { get; set; } = "";
                public double HypoLat { get; set; } = double.NaN;
                public double HypoLon { get; set; } = double.NaN;
                public double HypoDepth { get; set; } = double.NaN;
                public double Magnitude { get; set; } = double.NaN;
                public bool IsWarn { get; set; } = false;
                public DetailedIntensity MaxIntensityD { get; set; } = new DetailedIntensity();
                public DetailedIntensity MaxIntensityLgD { get; set; } = new DetailedIntensity();

                public (string[] warnAreas, int[] warnCodes) GetWarningAreas()
                {
                    var warnAreas = IntensityAreas.Where(x => (Intensity.S7 >= x.MaxIntensityD.Max && x.MaxIntensityD.Max >= Intensity.S4) || (Intensity.L4 >= x.MaxIntensityD.Max && x.MaxIntensityD.Max >= Intensity.L3));
                    var areaNames = warnAreas.SelectMany(x => x.AreaNames).Distinct().ToArray();
                    var areaCodes = warnAreas.SelectMany(x => x.AreaCodes).Distinct().ToArray();
                    if (areaNames.Length != areaCodes.Length)
                        throw new Exception($"area-code is not fully converted({areaNames.Length}-{areaCodes.Length})");
                    //return (areaNames.Length > 0 ? areaNames : null, areaCodes.Length > 0 ? areaCodes : null);
                    return (areaNames, areaCodes);
                }

                public IntensityArea[] IntensityAreas { get; set; } = [];
                public class IntensityArea
                {
                    public IntensityArea(DetailedIntensity intensity, string[]? areaNames = null, int[]? areaCodes = null)
                    {
                        MaxIntensityD = intensity.DeepClone();
                        AreaNames = areaNames ?? [];
                        AreaCodes = areaCodes ?? [];
                    }

                    public IntensityArea(KeyValuePair<DetailedIntensity, List<(string areaName, int areaCode)>> kv)
                    {
                        MaxIntensityD = kv.Key.DeepClone();
                        AreaNames = [.. kv.Value.Select(v => v.areaName)];
                        AreaCodes = [.. kv.Value.Select(v => v.areaCode)];
                    }

                    public IntensityArea(IntensityArea src)
                    {
                        MaxIntensityD = src.MaxIntensityD.DeepClone();
                        AreaNames = [.. src.AreaNames.Select(x => x)];
                        AreaCodes = [.. src.AreaCodes.Select(x => x)];
                    }

                    public IntensityArea DeepCopy() => new(this);

                    public DetailedIntensity MaxIntensityD { get; set; } = new DetailedIntensity();
                    public string[] AreaNames { get; set; } = [];

                    public int[] AreaCodes { get; set; } = [];
                }

            }
        }

    }

    public class DrawConfig
    {
        public DateTime StartTime { get; set; } = DateTime.MinValue;
        public DateTime EndTime { get; set; } = DateTime.MinValue;
        public TimeSpan DrawSpan { get; set; } = TimeSpan.Zero;

        public class Size
        {
            public Size(int height)
            {
                Width = height * 16 / 9;
                Height = height;
            }
            public Size(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public int Width { get; } = 0;
            public int Height { get; } = 0;
        }
    }
}
