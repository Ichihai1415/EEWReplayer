using static EEWReplayer.Utils.Common;
using static EEWReplayer.Utils.Common.DetailedIntensity;

namespace EEWReplayer.Utils
{
    /// <summary>
    /// データ格納用クラス
    /// </summary>
    public class Data
    {
        /*
         * Data <Object(class("Data"))>
         * |- Version <string>
         * |- Created <DateTime>
         * |- Description <string>
         * |- Comment <string>
         * |- ID <string>
         * |- Earthquakes <Array<Object(class("Earthquake"))>>
         * |  |- ID <string>
         * |  |- Source <string>
         * |  |- OriginTime <DateTime>
         * |  |- HypoName <string>
         * |  |- HypoLat <double>
         * |  |- HypoLon <double>
         * |  |- HypoDepth <double>
         * |  |- Magnitude <double>
         * |  |- MaxIntensity <enum("Intensity")>
         * |  |- MaxIntensityLg <enum("Intensity")>
         * |
         * |-EEWLists <Array<Object(class("EEWList"))>>
         *   |- ID <string>
         *   |- Source <string>
         *   |- EEWs <Array<Object(class("EEW"))>
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
         *      |- IntensityAreas <Array<Object(class("IntensityArea")>>
         *      |  |- MaxIntensityD <Object(struct("DetailedIntensity"))>
         *      |  |  |- From <enum("Intensity")>
         *      |  |  |- To <enum("Intensity")>
         *      |  |  |- Max <enum("Intensity")>
         *      |  |
         *      |  |- AreaNames <Array<string>>
         *      |  |- AreaCodes <Array<int>>
         *      |- IntensityLgAreas <Array<Object(class("IntensityArea")>>
         *         |- MaxIntensityD <Object(struct("DetailedIntensity"))>
         *         |  |- From <enum("Intensity")>
         *         |  |- To <enum("Intensity")>
         *         |  |- Max <enum("Intensity")>
         *         |
         *         |- AreaNames <Array<string>>
         *         |- AreaCodes <Array<int>>
         */

        /// <summary>
        /// 初期化コンストラクト
        /// </summary>
        public Data() { }

        /// <summary>
        /// 初期化コンストラクト（ディープコピー）
        /// </summary>
        /// <param name="src">コピー元</param>
        public Data(Data src)
        {
            Version = src.Version;
            Created = src.Created;
            Description = src.Description;
            Comment = src.Comment;
            ID = src.ID;
            Earthquakes = [.. src.Earthquakes.Select(srcEq => srcEq.DeepCopy())];
            EEWLists = [.. src.EEWLists.Select(srcEEWLists => srcEEWLists)];
        }

        /// <summary>
        /// バージョン
        /// </summary>
        public string Version { get; set; } = "";

        /// <summary>
        /// 生成日時
        /// </summary>
        public DateTime Created { get; set; } = DateTime.MinValue;

        /// <summary>
        /// （固定）説明
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 自由記述
        /// </summary>
        public string Comment { get; set; } = "(自由に記述できます)";

        /// <summary>
        /// データID
        /// </summary>
        public string ID { get; set; } = "";

        /// <summary>
        /// 地震情報リスト
        /// </summary>
        public Earthquake[] Earthquakes { get; set; } = [];

        /// <summary>
        /// 地震情報
        /// </summary>
        public class Earthquake
        {
            /// <summary>
            /// 初期化コンストラクト
            /// </summary>
            public Earthquake() { }

            /// <summary>
            /// 初期化コンストラクト（ディープコピー）
            /// </summary>
            /// <param name="src">コピー元</param>
            public Earthquake(Earthquake src)
            {
                ID = src.ID;
                Source = src.Source;
                OriginTime = src.OriginTime;
                HypoName = src.HypoName;
                HypoLat = src.HypoLat;
                HypoLon = src.HypoLon;
                HypoDepth = src.HypoDepth;
                Magnitude = src.Magnitude;
                MaxIntensity = src.MaxIntensity;
                MaxIntensityLg = src.MaxIntensityLg;
            }

            /// <summary>
            /// 地震ID
            /// </summary>
            public string ID { get; set; } = "";

            /// <summary>
            /// 情報ソース
            /// </summary>
            public string Source { get; set; } = "";

            /// <summary>
            /// 発生日時
            /// </summary>
            public DateTime OriginTime { get; set; } = DateTime.MinValue;

            /// <summary>
            /// 震央名
            /// </summary>
            public string HypoName { get; set; } = "";

            /// <summary>
            /// 震源緯度
            /// </summary>
            public double HypoLat { get; set; } = double.NaN;

            /// <summary>
            /// 震源経度
            /// </summary>
            public double HypoLon { get; set; } = double.NaN;

            /// <summary>
            /// 震源深さ
            /// </summary>
            public double HypoDepth { get; set; } = double.NaN;

            /// <summary>
            /// マグニチュード
            /// </summary>
            public double Magnitude { get; set; } = double.NaN;

            /// <summary>
            /// 最大震度
            /// </summary>
            public Intensity MaxIntensity { get; set; } = Intensity.Null;

            /// <summary>
            /// 最大長周期地震動階級
            /// </summary>
            public Intensity MaxIntensityLg { get; set; } = Intensity.Null;

            /// <summary>
            /// ディープコピーします。
            /// </summary>
            /// <returns>コピーされたインスタンス</returns>
            public Earthquake DeepCopy() => new(this);
        }

        /// <summary>
        /// 一連（1地震）の緊急地震速報のリスト
        /// </summary>
        public EEWList[] EEWLists { get; set; } = [];

        /// <summary>
        /// 一連（1地震）の緊急地震速報
        /// </summary>
        public class EEWList
        {
            /// <summary>
            /// 初期化コンストラクト
            /// </summary>
            public EEWList() { }

            /// <summary>
            /// 初期化コンストラクト（緊急地震速報リストから）
            /// </summary>
            /// <param name="eews">緊急地震速報リスト</param>
            /// <param name="source">情報ソース</param>
            /// <param name="id">地震ID</param>
            public EEWList(EEW[] eews, string source, string id = "")
            {
                ID = id;
                Source = source;
                EEWs = eews;
            }

            /// <summary>
            /// 初期化コンストラクト（ディープコピー）
            /// </summary>
            /// <param name="src">コピー元</param>
            public EEWList(EEWList src)
            {
                ID = src.ID;
                Source = src.Source;
                EEWs = [.. src.EEWs.Select(srcEEW => srcEEW.DeepCopy())];
            }

            /// <summary>
            /// 地震ID
            /// </summary>
            public string ID { get; set; } = "";

            /// <summary>
            /// 情報ソース
            /// </summary>
            public string Source { get; set; } = "";

            /// <summary>
            /// 緊急地震速報リスト
            /// </summary>
            public EEW[] EEWs { get; set; } = [];

            /// <summary>
            /// 緊急地震速報
            /// </summary>
            public class EEW
            {
                /// <summary>
                /// 初期化コンストラクト
                /// </summary>
                public EEW() { }

                /// <summary>
                /// 初期化コンストラクト（ディープコピー）
                /// </summary>
                /// <param name="src">コピー元</param>
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
                    IntensityLgAreas = [.. src.IntensityLgAreas.Select(srcArea => srcArea.DeepCopy())];
                }

                /// <summary>
                /// 報数
                /// </summary>
                public int Serial { get; set; } = 0;

                /// <summary>
                /// 更新（発表）日時
                /// </summary>
                public DateTime UpdateTime { get; set; } = DateTime.MinValue;

                /// <summary>
                /// 発生日時
                /// </summary>
                public DateTime OriginTime { get; set; } = DateTime.MinValue;

                /// <summary>
                /// 震央名
                /// </summary>
                public string HypoName { get; set; } = "";

                /// <summary>
                /// 震源緯度
                /// </summary>
                public double HypoLat { get; set; } = double.NaN;

                /// <summary>
                /// 震源経度
                /// </summary>
                public double HypoLon { get; set; } = double.NaN;

                /// <summary>
                /// 震源深さ
                /// </summary>
                public double HypoDepth { get; set; } = double.NaN;

                /// <summary>
                /// マグニチュード
                /// </summary>
                public double Magnitude { get; set; } = double.NaN;

                /// <summary>
                /// 警報発表か（初報/対象地域追加）
                /// </summary>
                public bool IsWarn { get; set; } = false;

                /// <summary>
                /// 最大震度（詳細）
                /// </summary>
                public DetailedIntensity MaxIntensityD { get; set; } = new DetailedIntensity();

                /// <summary>
                /// 最大長周期地震動階級（詳細）
                /// </summary>
                public DetailedIntensity MaxIntensityLgD { get; set; } = new DetailedIntensity();

                /// <summary>
                /// 地域別推定震度情報リスト
                /// </summary>
                public IntensityArea[] IntensityAreas { get; set; } = [];

                /// <summary>
                /// 地域別推定長周期地震動階級情報リスト
                /// </summary>
                public IntensityArea[] IntensityLgAreas { get; set; } = [];

                /// <summary>
                /// 地域別推定震度情報
                /// </summary>
                /// <remarks><see cref="DetailedIntensity"/>で地域名と地域コードのリストが含まれます。</remarks>
                public class IntensityArea
                {
                    /// <summary>
                    /// 初期化コンストラクト
                    /// </summary>
                    public IntensityArea() { }

                    /// <summary>
                    /// 初期化コンストラクト（通常追加）
                    /// </summary>
                    /// <param name="intensity">震度（詳細）</param>
                    /// <param name="areaNames">地域名のリスト</param>
                    /// <param name="areaCodes">地域コードのリスト</param>
                    public IntensityArea(DetailedIntensity intensity, string[]? areaNames = null, int[]? areaCodes = null)
                    {
                        MaxIntensityD = intensity.DeepClone();
                        AreaNames = areaNames ?? [];
                        AreaCodes = areaCodes ?? [];
                    }

                    /// <summary>
                    /// 初期化コンストラクト（Dictionary.Select用）
                    /// </summary>
                    /// <param name="kv">震度、(地域名, 地域コード)のリスト</param>
                    public IntensityArea(KeyValuePair<DetailedIntensity, List<(string areaName, int areaCode)>> kv)
                    {
                        MaxIntensityD = kv.Key.DeepClone();
                        AreaNames = [.. kv.Value.Select(v => v.areaName)];
                        AreaCodes = [.. kv.Value.Select(v => v.areaCode)];
                    }

                    /// <summary>
                    /// 初期化コンストラクト（ディープコピー）
                    /// </summary>
                    /// <param name="src">コピー元</param>
                    public IntensityArea(IntensityArea src)
                    {
                        MaxIntensityD = src.MaxIntensityD.DeepClone();
                        AreaNames = [.. src.AreaNames.Select(x => x)];
                        AreaCodes = [.. src.AreaCodes.Select(x => x)];
                    }

                    /// <summary>
                    /// 震度（詳細）
                    /// </summary>
                    public DetailedIntensity MaxIntensityD { get; set; } = new DetailedIntensity();

                    /// <summary>
                    /// 地域名のリスト
                    /// </summary>
                    public string[] AreaNames { get; set; } = [];

                    /// <summary>
                    /// 地域コードのリスト
                    /// </summary>
                    public int[] AreaCodes { get; set; } = [];

                    /// <summary>
                    /// ディープコピーします。
                    /// </summary>
                    /// <returns>コピーされたインスタンス</returns>
                    public IntensityArea DeepCopy() => new(this);


                    public override string ToString()
                    {
                        return "【" + MaxIntensityD.ToString() + "】" + string.Join('、', AreaNames);
                    }

                    public string ToString(ToStringPattern mode, string separator, char areaNameSeparator)
                    {
                        return MaxIntensityD.ToString(mode) + separator + string.Join(areaNameSeparator, AreaNames);
                    }
                }

                /// <summary>
                /// 警報対象（相当）地域を取得します。
                /// </summary>
                /// <remarks>この情報での警報対象のみで、<see cref="IsWarn"/>が<see cref="false"/>の場合実際の対象地域と異なる場合があります。また、警報2報以降の場合警報発表されたものの予測が基準以下になった場合入りません。<see cref="EEWList.GetAllWarningAreas()"/>で正確な地域を取得できます。</remarks>
                /// <returns>推定震度4以上または推定長周期地震動階級3以上の地域</returns>
                public (string[] warnAreas, int[] warnCodes) GetWarningAreas()
                {
                    var warnAreas = IntensityAreas.Where(x => Intensity.S7 >= x.MaxIntensityD.Max && x.MaxIntensityD.Max >= Intensity.S4)
                        .Concat(IntensityLgAreas.Where(x => Intensity.L4 >= x.MaxIntensityD.Max && x.MaxIntensityD.Max >= Intensity.L3));
                    var areaNames = warnAreas.SelectMany(x => x.AreaNames).Distinct().ToArray();
                    var areaCodes = warnAreas.SelectMany(x => x.AreaCodes).Distinct().ToArray();
                    //if (areaNames.Length != areaCodes.Length)
                    //    throw new Exception($"area-code is not fully converted({areaNames.Length}-{areaCodes.Length})");
                    //return (areaNames.Length > 0 ? areaNames : null, areaCodes.Length > 0 ? areaCodes : null);
                    return (areaNames, areaCodes);
                }//支庁は修正　レベル法だと数が合わない  最大震度５弱程度以上と推定	石川県珠洲市付近 https://www.data.jma.go.jp/eew/data/nc/pub_hist/2024/06/20240603063142/fc/index.html

                /// <summary>
                /// ディープコピーします。
                /// </summary>
                /// <returns>コピーされたインスタンス</returns>
                public EEW DeepCopy() => new(this);
            }

            /// <summary>
            /// ディープコピーします。
            /// </summary>
            /// <returns>コピーされたインスタンス</returns>
            public EEWList DeepCopy() => new(this);

            /// <summary>
            /// 警報発表毎の対象地域を取得します。
            /// </summary>
            /// <returns>（警報地域名リスト、地域コードリスト）のリスト</returns>
            public (string[] areaNames, int[] areaCodes)[] GetAllWarningAreas() => GetAllWarningAreas(int.MaxValue);

            /// <summary>
            /// 警報発表毎の対象地域を取得します。
            /// </summary>
            /// <param name="onlyNewArea">追加地域のみにするか</param>
            /// <returns>（警報地域名リスト、地域コードリスト）のリスト</returns>
            public (string[] areaNames, int[] areaCodes)[] GetAllWarningAreas(bool onlyNewArea) => GetAllWarningAreas(int.MaxValue, onlyNewArea);

            /// <summary>
            /// 警報発表毎の対象地域を取得します。
            /// </summary>
            /// <param name="endSerial">予報報数の制限（n報までに発表されたものに）</param>
            /// <returns>（警報地域名リスト、地域コードリスト）のリスト</returns>
            public (string[] areaNames, int[] areaCodes)[] GetAllWarningAreas(int endSerial) => GetAllWarningAreas(endSerial, false);

            /// <summary>
            /// 警報発表毎の対象地域を取得します。
            /// </summary>
            /// <param name="endSerial">予報報数の制限（n報までに発表されたものに）</param>
            /// <param name="onlyNewArea">追加地域のみにするか</param>
            /// <returns>（警報地域名リスト、地域コードリスト）のリスト</returns>
            public (string[] areaNames, int[] areaCodes)[] GetAllWarningAreas(int endSerial, bool onlyNewArea)
            {
                var eew_warn = EEWs.Where(eew => eew.Serial <= endSerial).Where(eew => eew.IsWarn);
                var result = new List<(string[] areaNames, int[] areaCodes)>();
                var i = 0;
                foreach (var eew in eew_warn)
                {
                    var (areaNames, areaCodes) = eew.GetWarningAreas();
                    if (i == 0)
                        result.Add((areaNames, areaCodes));
                    else if (onlyNewArea)
                        result.Add((areaNames.Except(result[i - 1].areaNames).ToArray(), areaCodes.Except(result[i - 1].areaCodes).ToArray()));
                    else
                        result.Add((result[i - 1].areaNames.Concat(areaNames).Distinct().ToArray(), result[i - 1].areaCodes.Concat(areaCodes).Distinct().ToArray()));
                    i++;
                }
                return [.. result];
            }

            /// <summary>
            /// 緊急地震速報を追加します。
            /// </summary>
            /// <param name="eew">緊急地震速報</param>
            public void AddEEW(EEW eew) => EEWs = [.. EEWs, eew];

            /// <summary>
            /// 緊急地震速報のリストを追加します。
            /// </summary>
            /// <param name="eews">緊急地震速報のリスト</param>
            public void AddEEW(EEW[] eews) => EEWs = [.. EEWs, .. eews];
        }

        /// <summary>
        /// ディープコピーします。
        /// </summary>
        /// <returns>コピーされたインスタンス</returns>
        public Data DeepCopy() => new(this);

        /// <summary>
        /// 地震情報のリストを追加します。
        /// </summary>
        /// <param name="data"></param>
        public void AddEarthquake(Data data) => AddEarthquake(data.Earthquakes);

        /// <summary>
        /// 地震情報を追加します。
        /// </summary>
        /// <param name="earthquake"></param>
        public void AddEarthquake(Earthquake earthquake) => Earthquakes = [.. Earthquakes, earthquake];

        /// <summary>
        /// 地震情報のリストを追加します。
        /// </summary>
        /// <param name="earthquakes"></param>
        public void AddEarthquake(Earthquake[] earthquakes) => Earthquakes = [.. Earthquakes, .. earthquakes];

        /// <summary>
        /// 緊急地震速報リストを追加します。
        /// </summary>
        /// <param name="data"></param>
        public void AddEEWList(Data data) => AddEEWList(data.EEWLists);

        /// <summary>
        /// 一連の緊急地震速報を追加します。
        /// </summary>
        /// <param name="eewList"></param>
        public void AddEEWList(EEWList eewList) => EEWLists = [.. EEWLists, eewList];

        /// <summary>
        /// 一連の緊急地震速報のリストを追加します。
        /// </summary>
        /// <param name="eewLists"></param>
        public void AddEEWList(EEWList[] eewLists) => EEWLists = [.. EEWLists, .. eewLists];

        /// <summary>
        /// 地震情報のリストと一連の緊急地震速報のリストを追加します。
        /// </summary>
        /// <param name="data"></param>
        public void AddEarthquakeEEW(Data data)
        {
            AddEarthquake(data.Earthquakes);
            AddEEWList(data.EEWLists);
        }
    }

    public class DrawConfig
    {
        public DrawConfig() { }

        public DateTime StartTime { get; init; } = DateTime.MinValue;
        public DateTime EndTime { get; init; } = DateTime.MinValue;
        public TimeSpan DrawSpan { get; init; } = TimeSpan.MinValue;

        public required C_Size Size { get; init; }

        public required float LatSta { get; init; }
        public required float LatEnd { get; init; }
        public required float LonSta { get; init; }
        public required float LonEnd { get; init; }
        //いるかわからないが再計算回避策
        private float _zoom = -1;
        public float Zoom
        {
            get
            {
                if (_zoom == -1)
                    _zoom = Size.Height / (LatEnd - LatSta);
                return _zoom;
            }
        }

        private (float zw, float zh) _zoomWH = (-1, -1);
        public (float zw, float zh) ZoomWH
        {
            get
            {
                if (_zoomWH.zw == -1)
                    _zoomWH = (Size.Width / (LonEnd - LonSta), Size.Height / (LatEnd - LatSta));
                return _zoomWH;
            }
        }

        public Size GetDrawSize() => Size.ToDrawingSize();

        public required C_Colors Colors { get; set; }


        public DrawConfig DeepCopy() => new()
        {
            StartTime = StartTime,
            EndTime = EndTime,
            DrawSpan = DrawSpan,
            Size = Size.DeepCopy(),
            LatSta = LatSta,
            LatEnd = LatEnd,
            LonSta = LonSta,
            LonEnd = LonEnd,
            _zoom = _zoom,
            _zoomWH = _zoomWH,
            Colors = Colors.DeepCopy()
        };
        public DrawConfig DeepCopyWithoutColor(C_Colors color) => new()
        {
            StartTime = StartTime,
            EndTime = EndTime,
            DrawSpan = DrawSpan,
            Size = Size.DeepCopy(),
            LatSta = LatSta,
            LatEnd = LatEnd,
            LonSta = LonSta,
            LonEnd = LonEnd,
            _zoom = _zoom,
            _zoomWH = _zoomWH,
            Colors = color
        };

        public class C_Size
        {
            public C_Size(int height)
            {
                Width = height * 16 / 9;
                Height = height;
            }
            public C_Size(int height, double ratio)
            {
                Width = (int)(height * ratio);
                Height = height;
            }
            public C_Size(int height, (int rw, int rh) ratio)
            {
                Width = height * ratio.rw / ratio.rh;
                Height = height;
            }
            public C_Size(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public int Width { get; }
            public int Height { get; }

            public Size ToDrawingSize() => new(Width, Height);

            public C_Size DeepCopy() => new(Width, Height);
        }
        public class C_Colors
        {
            public Color LineColor { get; init; } = Color.White;

            public Color BackgroundColor { get; init; } = Color.FromArgb(20, 40, 60);

            public SolidBrush DefaultFillColor { get; init; } = new(Color.FromArgb(100, 100, 150));

            public Dictionary<int, SolidBrush> FillColors { get; set; } = [];

            public C_Colors DeepCopy() => new()
            {
                LineColor = LineColor,
                BackgroundColor = BackgroundColor,
                DefaultFillColor = DefaultFillColor,
                FillColors = FillColors.ToDictionary(x => x.Key, x => x.Value)//SolidBrushのDeepCopy怪しいけど保留
            };
        }
    }
}
