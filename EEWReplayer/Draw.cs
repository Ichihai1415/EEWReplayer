using EEWReplayer.Properties;
using EEWReplayer.Utils;
using Ichihai1415.GeoJSON;
using PSWaveDistance;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.Json;
using static EEWReplayer.Utils.Common;

namespace EEWReplayer
{
    internal class Draw
    {


        public static async Task DrawFlowEx(string url)
        {
            url = "https://www.data.jma.go.jp/svd/eew/data/nc/fc_hist/2024/01/20240101161010/index.html";
            var d = await GetData.GetDetail(url);
            var config = new DrawConfig()
            {
                StartTime = new DateTime(2024, 01, 01, 16, 10, 15),
                EndTime = new DateTime(2024, 01, 01, 16, 14, 00),
                DrawSpan = new TimeSpan(0, 0, 0, 0, 200)
            };
            var dClone = d.DeepCopy();
            for (DateTime drawTime = config.StartTime; drawTime < config.EndTime; drawTime = drawTime.AddMilliseconds(config.DrawSpan.TotalMilliseconds))//ms単位で正確に足し算
            {
                //var overwriteEEWList = new Data.EEWList();
                var drawEEWList = new List<Data.EEWList>();
                foreach (var (eewList, i) in dClone.EEWLists.Select((eewList, index) => (eewList, index)))
                {
                    if (eewList.EEWs.Length == 0)
                        continue;
                    var drawEEW = new Data.EEWList.EEW();
                    for (int j = eewList.EEWs.Length - 1; j >= 0; j--)
                    {
                        var eew = eewList.EEWs[j];
                        if (eew.UpdateTime > drawTime)
                            continue;
                        drawEEW = eew.DeepCopy();
                        if (j != 0)
                        {
                            var overwriteEEW = eewList.DeepCopy();
                            overwriteEEW.EEWs.ToList().RemoveRange(0, j);//[0,1,2,3] で index=jが欲しいとき0からのj個はもういらない
                            dClone.EEWLists[i] = overwriteEEW;//もうちょっといい感じにできるかも
                        }
                        break;
                    }
                    if (drawEEW != null)
                        drawEEWList.Add(new Data.EEWList()
                        {
                            ID = eewList.ID,
                            Source = eewList.Source,
                            EEWs = [drawEEW]
                        });
                }

                Console.Write(drawTime.ToString("yyyy/MM/dd HH:mm:ss.f"));
                foreach (var da in drawEEWList)
                {
                    Console.WriteLine($"  id:{da.ID}  #{da.EEWs[0].Serial}  {(da.EEWs[0].IsWarn ? "警報発表" : "")}");
                    foreach (var a in da.EEWs[0].IntensityAreas)
                    {
                        if ((int)a.MaxIntensityD.From > 10)
                            continue;
                        Console.WriteLine($"  {a.MaxIntensityD}  {a.AreaNames}");
                    }
                }
                Console.WriteLine();
                await Task.Delay(200);


            }






        }

        public static async Task DrawFlowEx()
        {
            var dir = "output\\" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Directory.CreateDirectory(dir);

            //var d = JsonSerializer.Deserialize<Data>(Resources.jma_xml_20251105095956, Form1.options);
            //var config = new DrawConfig()
            //{
            //    StartTime = new DateTime(2025, 11, 05, 09, 59, 58),
            //    EndTime = new DateTime(2025, 11, 05, 10, 02, 00),
            //    DrawSpan = new TimeSpan(0, 0, 0, 0, 100),
            //    Size = new(1080)
            //};

            var d = JsonSerializer.Deserialize<Data>(Resources._20240101161010, Form1.options);
            var config = new DrawConfig()
            {
                StartTime = new DateTime(2024, 01, 01, 16, 10, 00),
                EndTime = new DateTime(2024, 01, 01, 16, 13, 00),
                DrawSpan = new TimeSpan(0, 0, 0, 1, 000),
                Size = new(1080)
            };

            var dClone = d!.DeepCopy();
            var c = 1;
            for (var drawTime = config.StartTime; drawTime < config.EndTime; drawTime = drawTime.AddMilliseconds(config.DrawSpan.TotalMilliseconds))//ms単位で正確に足し算
            {
                //var overwriteEEWList = new Data.EEWList();
                var drawEEWList = new List<Data.EEWList>();
                foreach (var (eewList, i) in dClone.EEWLists.Select((eewList, index) => (eewList, index)))
                {
                    if (eewList.EEWs.Length == 0)
                        continue;
                    var drawEEW = new Data.EEWList.EEW();
                    for (int j = eewList.EEWs.Length - 1; j >= 0; j--)
                    {
                        var eew = eewList.EEWs[j];
                        if (eew.UpdateTime > drawTime)//drawTimeでの最新を探す
                            continue;
                        drawEEW = eew.DeepCopy();
                        if (j != 0)
                        {
                            var overwriteEEW = eewList.DeepCopy();
                            overwriteEEW.EEWs.ToList().RemoveRange(0, j);//[0,1,2,3] で index=jが欲しいとき0からのj個はもういらない
                            dClone.EEWLists[i] = overwriteEEW;//もうちょっといい感じにできるかも
                        }
                        break;
                    }
                    if (drawEEW.Serial != 0)
                        drawEEWList.Add(new Data.EEWList()
                        {
                            ID = eewList.ID,
                            EEWs = [drawEEW]
                        });
                }

                Console.WriteLine(drawTime.ToString("yyyy/MM/dd HH:mm:ss.ff"));
                Form1.f2.AddLine(drawTime.ToString("yyyy/MM/dd HH:mm:ss.ff"));
                //var areaList = new List<int>();
                //foreach (var da in drawEEWList)
                //{
                //    Console.WriteLine($"    id:{(da.ID == "" ? "(none)" : da.ID)}  #{da.EEWs[0].Serial}  {(da.EEWs[0].IsWarn ? "<警報発表>" : "")}");
                //    foreach (var a in da.EEWs[0].IntensityAreas)
                //    {
                //        if ((int)a.MaxIntensityD.From > 10)
                //            continue;
                //        var area = DataConverter.AreasInt2Sts(a.AreaCodes, ',');
                //        Console.WriteLine($"    {a.MaxIntensityD} (count:{a.AreaCodes.Length})  [{area}]");
                //        areaList.AddRange(a.AreaCodes);
                //    }
                //}
                //areaList = [.. areaList.Distinct()];
                //areaList.Sort();
                //Console.WriteLine($"    count:{areaList.Count}");
                //Console.WriteLine();
                //await Task.Delay((int)config.DrawSpan.TotalMilliseconds);

                var img = new Bitmap(config.Size.Width, config.Size.Height);
                using var g = Graphics.FromImage(img);

                g.Clear(Color.FromArgb(20, 40, 60));

                var colorConfig = new Dictionary<int, Intensity>();
                foreach (var drawEEW in drawEEWList)
                {
                    foreach (var area in drawEEW.EEWs[0].IntensityAreas)//Lgでも
                    {
                        foreach (var code in area.AreaCodes)
                        {
                            if (colorConfig.TryGetValue(code, out var existingIntensity))
                            {
                                if ((int)area.MaxIntensityD.Max > (int)existingIntensity)
                                    colorConfig[code] = area.MaxIntensityD.Max;
                            }
                            else
                            {
                                colorConfig[code] = area.MaxIntensityD.Max;
                            }
                        }
                    }
                }

                DrawMap(g, colorConfig.ToDictionary(kv => kv.Key, kv => IntN2Brush(kv.Value)));
                DrawPSWave(g, [.. drawEEWList], drawTime);
                g.DrawString("地図データ:気象庁\n" + drawTime.ToString("yyyy/MM/dd HH:mm:ss.ff"), new Font("koruri", 20), Brushes.White, 0, 0);
                var rightText = "";
                foreach (var drawEEW in drawEEWList)
                {
                    var eew = drawEEW.EEWs[0];
                    rightText += $"{drawEEW.ID} {eew.OriginTime:yyyy/MM/dd HH:mm:ss.f} {eew.HypoName} M{eew.Magnitude} {eew.MaxIntensityD}\n";
                }
                g.DrawString(rightText, new Font("koruri", 16), Brushes.White, 1080, 0);
                img.Save($"{dir}\\{c:d5}.png", ImageFormat.Png);
                Console.WriteLine($"Saved: {dir}\\{c:d5}.png");
                Form1.fd.ChangeImage(img);
                //await Task.Delay((int)config.DrawSpan.TotalMilliseconds);
                c++;
            }

            Console.WriteLine($"END----------");
            var f = 10;
            Console.WriteLine($" ffmpeg -framerate {f} -i \"%05d.png\" -vcodec libx264 -pix_fmt yuv420p -r {f} \"_output_{f}.mp4\"");
            Console.WriteLine($"END----------");



        }
        public static readonly GeoJSONScheme.GeoJSON_JMA_Map mapData = GeoJSONHelper.Deserialize<GeoJSONScheme.GeoJSON_JMA_Map>(Resources.AreaForecastLocalE_GIS_20240520_01)!;

        public static void DrawMap(Graphics g, Dictionary<int, SolidBrush> colorConfig, float latSta = 20, float latEnd = 50, float lonSta = 120, float lonEnd = 150)
        {
            if (mapData == null)
                throw new Exception("地図データが読み込まれていません。");
            var zoom = 1080f / (latEnd - latSta);

            //var bitmap = new Bitmap(1920, 1080);
            //using var g = Graphics.FromImage(bitmap);
            //g.Clear(Color.FromArgb(20, 40, 60));
            using var gp = new GraphicsPath();





            foreach (var feature in mapData.Features)
            {
                if (feature.Geometry == null)
                    continue;
                if (feature.Geometry.Type == "Polygon")
                {
                    gp.StartFigure();
                    var points = feature.Geometry.Coordinates.Objects[0].MainPoints.Select(coordinate => new PointF((coordinate.Lon - lonSta) * zoom, (latEnd - coordinate.Lat) * zoom));
                    if (points.Count() > 2)
                    {
                        gp.AddPolygon(points.ToArray());
                        if (colorConfig.TryGetValue(int.Parse((feature.Properties?.GetCode() == "" ? "-1" : feature.Properties?.GetCode()) ?? "-1"), out var brush))
                            g.FillPolygon(brush, points.ToArray());
                        else
                            g.FillPolygon(new SolidBrush(Color.FromArgb(100, 100, 150)), points.ToArray());
                    }
                }
                else
                {

                    foreach (var singleObject in feature.Geometry.Coordinates.Objects)
                    {
                        gp.StartFigure();
                        var points = singleObject.MainPoints.Select(coordinate => new PointF((coordinate.Lon - lonSta) * zoom, (latEnd - coordinate.Lat) * zoom));
                        if (points.Count() > 2)
                        {
                            gp.AddPolygon(points.ToArray());
                            if (colorConfig.TryGetValue(int.Parse(feature.Properties?.GetCode() ?? "-1"), out var brush))
                                g.FillPolygon(brush, points.ToArray());
                            else
                                g.FillPolygon(new SolidBrush(Color.FromArgb(100, 100, 150)), points.ToArray());
                        }
                    }
                }
            }
            var lineWidth = Math.Max(1f, zoom / 216f);
            //g.FillPath(new SolidBrush(Color.FromArgb(100, 100, 150)), gp);
            g.DrawPath(new Pen(Color.FromArgb(255, 200, 200, 200), lineWidth) { LineJoin = LineJoin.Round }, gp);//zoom > 200 ? 2 : 1

            g.FillRectangle(Brushes.Black, 1080, 0, 1920 - 1080, 1080);
        }

        private static readonly PSDistances psd = new();

        public static void DrawPSWave(Graphics g, Data.EEWList[] eewLists, DateTime drawTime, float latSta = 20, float latEnd = 50, float lonSta = 120, float lonEnd = 150)
        {
            foreach (var eewList in eewLists)
            {
                DrawPSWave(g, eewList.EEWs[0], drawTime, latSta, latEnd, lonSta, lonEnd);
            }
        }

        public static void DrawPSWave(Graphics g, Data.EEWList.EEW eew, DateTime drawTime, float latSta = 20, float latEnd = 50, float lonSta = 120, float lonEnd = 150)
        {
            var zoom = 1080f / (latEnd - latSta);

            var seconds = (drawTime - eew.OriginTime).TotalSeconds;

            if (seconds > 0)
            {
                var (pLatLon, sLatLon) = psd!.GetLatLonList(eew.HypoDepth, seconds, eew.HypoLat, eew.HypoLon, 45);
                if (pLatLon.Count > 2)//基本45、失敗時0か1
                {
                    var pPts = pLatLon.Select(x => new PointF(((float)x.Lon - lonSta) * zoom, (latEnd - (float)x.Lat) * zoom)).ToList()!;
                    pPts.Add(pPts[0]);
                    g.DrawPolygon(new Pen(Color.FromArgb(64, 64, 255), 2), pPts.ToArray());
                }
                if (sLatLon.Count > 2)
                {
                    var sPts = sLatLon.Select(x => new PointF(((float)x.Lon - lonSta) * zoom, (latEnd - (float)x.Lat) * zoom)).ToList()!;
                    sPts.Add(sPts[0]);
                    g.DrawPolygon(new Pen(Color.Red, 2), sPts.ToArray());
                    g.FillPolygon(new SolidBrush(Color.FromArgb(64, 255, 0, 0)), sPts.ToArray());
                }
                var hypoLength = 20;
                var hypoPt = new PointF(((float)eew.HypoLon - lonSta) * zoom, (latEnd - (float)eew.HypoLat) * zoom);
                g.DrawLine(new Pen(Color.Red, 6), hypoPt.X - hypoLength, hypoPt.Y - hypoLength, hypoPt.X + hypoLength, hypoPt.Y + hypoLength);
                g.DrawLine(new Pen(Color.Red, 6), hypoPt.X + hypoLength, hypoPt.Y - hypoLength, hypoPt.X - hypoLength, hypoPt.Y + hypoLength);
            }
        }

        /// <summary>
        /// 画像描画用に緯度・経度を補正します
        /// </summary>
        /// <param name="latSta">緯度の始点</param>
        /// <param name="latEnd">緯度の終点</param>
        /// <param name="lonSta">経度の始点</param>
        /// <param name="lonEnd">経度の終点</param>
        /// <param name="enableCorrectMax">最大値を補正するか</param>
        public static void PointCorrect(ref float latSta, ref float latEnd, ref float lonSta, ref float lonEnd, bool enableCorrectMax = false)
        {
            latSta -= (latEnd - latSta) / 20f;//差の1/20余白追加
            latEnd += (latEnd - latSta) / 20f;
            lonSta -= (lonEnd - lonSta) / 20f;
            lonEnd += (lonEnd - lonSta) / 20f;
            if (latEnd - latSta < 3f)//緯度差を最小3に
            {
                var correction = (3f - (latEnd - latSta)) / 2f;
                latSta -= correction;
                latEnd += correction;
            }
            if (latEnd - latSta < 3f)//経度差を最小3に
            {
                var correction = (3f - (lonEnd - lonSta)) / 2f;
                lonSta -= correction;
                lonEnd += correction;
            }
            if (enableCorrectMax)
            {
                if (latEnd - latSta > 10f) //緯度差を最大10に
                {
                    var correction = ((latEnd - latSta) - 10f) / 2f;
                    latSta += correction;
                    latEnd -= correction;
                }
                if (lonEnd - lonSta > 10f) //経度差を最大10に
                {
                    var correction = ((lonEnd - lonSta) - 10f) / 2f;
                    lonSta += correction;
                    lonEnd -= correction;
                }
            }

            if (lonEnd - lonSta > latEnd - latSta)//大きいほうに合わせる
            {
                var correction = ((lonEnd - lonSta) - (latEnd - latSta)) / 2f;
                latSta -= correction;
                latEnd += correction;
            }
            else// if (LonEnd - LonSta < LatEnd - LatSta)
            {
                var correction = ((latEnd - latSta) - (lonEnd - lonSta)) / 2f;
                lonSta -= correction;
                lonEnd += correction;
            }
        }

        /// <summary>
        /// 震度から震度色を返します。
        /// </summary>
        /// <param name="Int">int形式の震度</param>
        /// <returns>SolidBrush形式の震度色</returns>
        /// <remarks>配色はKiwi Monitor カラースキーム第2版を改変したものです。</remarks>
        public static SolidBrush IntN2Brush(Intensity intensity) => (int)intensity switch
        {
            0 => new SolidBrush(Color.FromArgb(80, 90, 100)),
            1 => new SolidBrush(Color.FromArgb(60, 80, 100)),
            2 => new SolidBrush(Color.FromArgb(45, 90, 180)),
            3 => new SolidBrush(Color.FromArgb(50, 175, 175)),
            4 => new SolidBrush(Color.FromArgb(240, 240, 60)),
            5 => new SolidBrush(Color.FromArgb(250, 150, 0)),
            6 => new SolidBrush(Color.FromArgb(250, 75, 0)),
            7 => new SolidBrush(Color.FromArgb(200, 0, 0)),
            8 => new SolidBrush(Color.FromArgb(100, 0, 0)),
            9 => new SolidBrush(Color.FromArgb(100, 0, 100)),
            10 => new SolidBrush(Color.FromArgb(80, 90, 100)),
            11 => new SolidBrush(Color.FromArgb(50, 130, 180)),
            12 => new SolidBrush(Color.FromArgb(250, 200, 30)),
            13 => new SolidBrush(Color.FromArgb(250, 125, 0)),
            14 => new SolidBrush(Color.FromArgb(150, 0, 0)),
            _ => new SolidBrush(Color.FromArgb(30, 60, 90)),
        };

    }
}
