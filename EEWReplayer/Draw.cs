using EEWReplayer.Properties;
using EEWReplayer.Utils;
using System.Text.Json;

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
            var d = JsonSerializer.Deserialize<Data>(Resources.eew_kunren);
            var config = new DrawConfig()
            {
                StartTime = new DateTime(2024, 11, 05, 09, 59, 58),
                EndTime = new DateTime(2024, 11, 05, 10, 01, 00),
                DrawSpan = new TimeSpan(0, 0, 0, 1, 000)
            };
            var dClone = d!.DeepCopy();
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
                            EEWs = [drawEEW]
                        });
                }

                Console.WriteLine(drawTime.ToString("yyyy/MM/dd HH:mm:ss.ff"));
                var areaList = new List<int>();
                foreach (var da in drawEEWList)
                {
                    Console.WriteLine($"    id:{(da.ID == "" ? "(none)" : da.ID)}  #{da.EEWs[0].Serial}  {(da.EEWs[0].IsWarn ? "<警報発表>" : "")}");
                    foreach (var a in da.EEWs[0].IntensityAreas)
                    {
                        if ((int)a.MaxIntensityD.From > 10)
                            continue;
                        var area = DataConverter.AreasInt2Sts(a.AreaCodes, ',');
                        Console.WriteLine($"    {a.MaxIntensityD} (count:{a.AreaCodes.Length})  [{area}]");
                        areaList.AddRange(a.AreaCodes);
                    }
                }
                areaList = areaList.Distinct().ToList();
                areaList.Sort();
                Console.WriteLine($"    count:{areaList.Count}");
                Console.WriteLine();
                await Task.Delay((int)config.DrawSpan.TotalMilliseconds);


            }






        }


    }
}
