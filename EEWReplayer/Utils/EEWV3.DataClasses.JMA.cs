namespace EEW_Viewer3.Utilities
{
    public partial class DataClasses
    {
        /// <summary>
        /// 気象庁データのスキーム
        /// </summary>
        public class JMA
        {
            /// <summary>
            /// 地震火山関連共通
            /// </summary>
            public class EqVol_Common
            {
                /// <summary>
                /// 【管理部】（1 回）
                /// </summary>
                /// <remarks>本情報の配信に関連する情報を記載する。</remarks>
                public required C_Control Control { get; set; }

                /// <summary>
                /// 【管理部】（1 回）
                /// </summary>
                /// <remarks>本情報の配信に関連する情報を記載する。</remarks>
                public class C_Control
                {
                    /// <summary>
                    /// 【情報名称】（1 回）
                    /// </summary>
                    /// <remarks>本要素は、「独立した情報単位」判別のキーとしても用いられる（（ⅲ）共通別紙ア．「地震火山関連XML電文の「独立した情報単位」の運用」参照）。 </remarks>
                    public required string Title { get; set; }
                    /// <summary>
                    /// 【発表時刻】（1 回）
                    /// </summary>
                    /// <remarks>ISO 8601 の規格に従い、気象庁システムからの発信時刻を記載する。この値は秒値まで有効である。</remarks>
                    public required DateTime AnnouncementDateTime { get; set; }
                    /// <summary>
                    /// 【運用種別】（1 回，値：“通常”/“訓練”/“試験”）
                    /// </summary>
                    /// <remarks>通常の運用で発表する情報には“通常”を記載する。事前に日時を定めて行う業務訓練等で発表する情報には“訓練”を記載する。定期または臨時に電文疎通確認等を目的として発表する緊急地震速報の配信テスト電文には“試験”を記載する。本要素は、「独立した情報単位」判別のキーとしても用いられる（（ⅲ）共通別紙ア．「地震火山関連XML電文の「独立した情報単位」の運用」参照）。</remarks>
                    public required string Status { get; set; }
                    /// <summary>
                    /// 【編集官署名】（1 回）
                    /// </summary>
                    /// <remarks>本要素は、「独立した情報単位」判別のキーとしても用いられるが、地震・津波に関連する情報、南海トラフ地震に関連する情報、北海道・三陸沖後発地震注意情報及び地震・津波に関するお知らせについては、システム障害発生等により一連の情報であっても編集官署が切り替わる場合があることに留意が必要である。地震・津波に関連する情報等のこうした取扱については、（ⅲ）共通別紙ア．「地震火山関連 XML 電文の「独立した情報単位」の運用」を参照すること。</remarks>
                    public required string EditorialOffice { get; set; }
                    /// <summary>
                    /// 【発表官署名】（1 回）
                    /// </summary>
                    /// <remarks>発表官署名を記載する。</remarks>
                    public required string PublishingOffice { get; set; }
                }

                /// <summary>
                /// 【ヘッダ部】（1回）
                /// </summary>
                /// <remarks>本情報の見出しを記載する。</remarks>
                public required C_Head Head { get; set; }

                /// <summary>
                /// 【ヘッダ部】（1回）
                /// </summary>
                /// <remarks>本情報の見出しを記載する。</remarks>
                public class C_Head
                {
                    /// <summary>
                    /// 【標題】（1 回）
                    /// </summary>
                    /// <remarks>情報の標題を記載する。</remarks>
                    public required string Title { get; set; }

                    /// <summary>
                    /// 【発表時刻】（1 回）
                    /// </summary>
                    /// <remarks>発表官署が本情報を発表した時刻を記載する。</remarks>
                    public required DateTime ReportDateTime { get; set; }

                    /// <summary>
                    /// 【基点時刻】（1 回）
                    /// </summary>
                    /// <remarks>情報の内容が発現・発効する基点時刻を記載する。</remarks>
                    public required DateTime TargetDateTime { get; set; }

                    /// <summary>
                    /// 【基点時刻のあいまいさ】（0 回/1回, 値：“頃”/“年頃”/“月頃”/“日頃”/“時頃”/“分頃”/“秒頃”）
                    /// </summary>
                    public string? TargetDTDubious { get; set; }
                    /// <summary>
                    /// 【失効時刻】（0 回/1 回）
                    /// </summary>
                    /// <remarks>津波警報・注意報・予報の電文及び降灰予報の電文において情報の失効時刻を記載する。</remarks>
                    public DateTime? ValidDateTime { get; set; }

                    /// <summary>
                    /// 【識別情報】（1回）
                    /// </summary>
                    /// <remarks>地震・津波に関連する情報については、ある特定の地震を識別するための地震識別番号（14 桁の数字）を記載する。津波に関連する情報では、当該警報等に寄与している地震の地震識別番号を記載するため、１つの電文に複数の地震識別番号が出現する場合もある。詳細については、（ⅲ）共通別紙イ．「地震・津波に関連する情報のEventID要素の運用」を参照。</remarks>
                    public required string EventID { get; set; }

                    /// <summary>
                    /// 【情報形態】（1回）
                    /// </summary>
                    /// <remarks>情報を発表する場合は“発表”を、「独立した情報単位」において直前の時点で発表されている Control/DateTime の最も新しい電文を訂正する場合は“訂正”を、「独立した情報単位」全体を取り消す場合は“取消”を記載する。取消電文の運用については、（ⅲ）共通別紙ウ．「取消電文の運用」を参照。</remarks>
                    public required string InfoType { get; set; }

                    /// <summary>
                    /// 【情報番号】（1 回）
                    /// </summary>
                    /// <remarks>続報を発表し、内容を更新する情報については、情報番号を記載する。続報を発表する度に情報番号を更新するが、取消報の場合は、番号は更新しない。訂正報の場合は訂正する直近の情報の情報番号を記載する。</remarks>
                    public required string Serial { get; set; }

                    /// <summary>
                    /// 【スキーマの運用種別情報】（1回）
                    /// </summary>
                    public required string InfoKind { get; set; }

                    /// <summary>
                    /// 【スキーマの運用種別情報のバージョン番号】（1回）
                    /// </summary>
                    public required string InfoKindVersion { get; set; }

                    /// <summary>
                    /// 【見出し要素】（1 回）
                    /// </summary>
                    /// <remarks>子要素にText及びInformationをもつ。</remarks>
                    public required C_Headline Headline { get; set; }

                    /// <summary>
                    /// 【見出し要素】（1 回）
                    /// </summary>
                    /// <remarks>子要素にText及びInformationをもつ。</remarks>
                    public class C_Headline
                    {
                        /// <summary>
                        /// 【見出し文】（1回）
                        /// </summary>
                        /// <remarks>見出し文を自由文形式で記載する。</remarks>
                        public required string Text { get; set; }

                        /// <summary>
                        /// 【見出し防災気象情報事項】（0回以上）
                        /// </summary>
                        /// <remarks>地震火山関連XML電文では、情報によって本要素の運用が異なる。</remarks>
                        public C_Information[]? Information { get; set; }

                        /// <summary>
                        /// 【見出し防災気象情報事項】（0回以上）
                        /// </summary>
                        /// <remarks>地震火山関連XML電文では、情報によって本要素の運用が異なる。</remarks>
                        public abstract class C_Information
                        {

                        }
                    }
                }

                /// <summary>
                /// 【内容部】（1回）
                /// </summary>
                /// <remarks>本情報の量的な詳細内容を記載する。○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」、「緊急地震速報（予報）」の場合、 「地震の諸要素」（Earthquake）  「震度の予測」（Intensity/Forecast) 「テキスト要素」（Text)  「付加文」（Comments） 「次回発表予定」（NextAdvisory） の子要素により構成される。 ○「リアルタイム震度電文」の場合、 「地震の諸要素」（Earthquake） 「リアルタイム震度（工学的基盤面の値）」（Intensity/Observation) 「次回発表予定」（NextAdvisory） の子要素により構成される。 Earthquake 要素の内容は、同じタイミングで発表した「緊急地震速報（地震動予報）」、「緊急地震速報（予報）」と同一である。 ○ヘッダ部の「情報形態」（Head/InfoType）が“取消”の場合、 テキスト要素（Text) の子要素のみからなる。 </remarks>
                public required C_Body Body { get; set; }

                /// <summary>
                /// 【内容部】（1回）
                /// </summary>
                /// <remarks>本情報の量的な詳細内容を記載する。○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」、「緊急地震速報（予報）」の場合、 「地震の諸要素」（Earthquake）  「震度の予測」（Intensity/Forecast) 「テキスト要素」（Text)  「付加文」（Comments） 「次回発表予定」（NextAdvisory） の子要素により構成される。 ○「リアルタイム震度電文」の場合、 「地震の諸要素」（Earthquake） 「リアルタイム震度（工学的基盤面の値）」（Intensity/Observation) 「次回発表予定」（NextAdvisory） の子要素により構成される。 Earthquake 要素の内容は、同じタイミングで発表した「緊急地震速報（地震動予報）」、「緊急地震速報（予報）」と同一である。 ○ヘッダ部の「情報形態」（Head/InfoType）が“取消”の場合、 テキスト要素（Text) の子要素のみからなる。 </remarks>
                public class C_Body
                {
                    /// <summary>
                    /// 【地震の諸要素】（0回/1回） 
                    /// </summary>
                    /// <remarks>地震の諸要素（発生日時、震央地名、震源要素、マグニチュード等）を記載する。○ヘッダ部の「情報形態」（Head/InfoType）が“取消”の場合、 本要素は出現しない。 ○震源とマグニチュードによる震度推定手法において、震源要素が推定できずPLUM法による震度予測のみが有効である場合は、 PLUM 法でトリガー条件を最初に満足した観測点直下の深さ 10km を震源と仮定した震源要素を記載する。（以後、この値を「仮定震源要素」という。）</remarks>
                    public C_Earthquake? Earthquake { get; set; }

                    /// <summary>
                    /// 【地震の諸要素】（0回/1回） 
                    /// </summary>
                    /// <remarks>地震の諸要素（発生日時、震央地名、震源要素、マグニチュード等）を記載する。○ヘッダ部の「情報形態」（Head/InfoType）が“取消”の場合、 本要素は出現しない。 ○震源とマグニチュードによる震度推定手法において、震源要素が推定できずPLUM法による震度予測のみが有効である場合は、 PLUM 法でトリガー条件を最初に満足した観測点直下の深さ 10km を震源と仮定した震源要素を記載する。（以後、この値を「仮定震源要素」という。）</remarks>
                    public class C_Earthquake
                    {
                        /// <summary>
                        /// 【地震発生時刻】（0回/1 回） 
                        /// </summary>
                        /// <remarks>地震の発生した時刻（発震時刻）を記載する。この値は秒値まで有効である。 ○「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」において、非常に強い揺れを検知・最大予測震度のみの場合、 本要素は出現しない。 ○「仮定震源要素」を設定する場合、 PLUM 法でトリガー条件を最初に満足した観測点における発現時刻を元に算出した地震発生時刻を記載する。</remarks>
                        public DateTime? OriginTime { get; set; }

                        /// <summary>
                        /// 【震源要素の補足情報】（0回/1回，値：“仮定震源要素”） 
                        /// </summary>
                        /// <remarks>記載されている震源要素が 「仮定震源要素」である場合、本要素が出現する。 </remarks>
                        public string? Condition { get; set; }

                        /// <summary>
                        /// 【地震発現時刻】（1 回）
                        /// </summary>
                        /// <remarks>観測点で地震を検知した時刻（発現時刻）を記載する。この値は秒値まで有効である。 </remarks>
                        public required DateTime ArrivalTime { get; set; }

                        /// <summary>
                        /// 【地震の位置要素】（1回） 
                        /// </summary>
                        /// <remarks>地震の位置に関する要素（震央地名、震源要素等）を記載する。 ○「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」において、非常に強い揺れを検知・最大予測震度のみの場合、 非常に強い揺れを検知した観測点を震央位置とみなして記載する。 （震源の深さは10kmとして扱う。） ○「仮定震源要素」を設定する場合、 PLUM 法でトリガー条件を最初に満足した観測点の座標を記載する。（震源の深さは 10kmとする。）</remarks>
                        public required C_Hypocenter Hypocenter { get; set; }

                        /// <summary>
                        /// 【地震の位置要素】（1回） 
                        /// </summary>
                        /// <remarks>地震の位置に関する要素（震央地名、震源要素等）を記載する。 ○「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」において、非常に強い揺れを検知・最大予測震度のみの場合、 非常に強い揺れを検知した観測点を震央位置とみなして記載する。 （震源の深さは10kmとして扱う。） ○「仮定震源要素」を設定する場合、 PLUM 法でトリガー条件を最初に満足した観測点の座標を記載する。（震源の深さは 10kmとする。）</remarks>
                        public class C_Hypocenter
                        {
                            /// <summary>
                            /// 【震源位置】（1 回） 
                            /// </summary>
                            /// <remarks>震源の位置に関する情報を記載する。 </remarks>
                            public required C_Area Area { get; set; }

                            /// <summary>
                            /// 【震源位置】（1 回） 
                            /// </summary>
                            /// <remarks>震源の位置に関する情報を記載する。 </remarks>
                            public class C_Area
                            {
                                /// <summary>
                                /// 【震央地名】（1 回）
                                /// </summary>
                                /// <remarks>震央地名の文字列表現を記載する。また、これに対応するコードをCodeに記載する。 </remarks>
                                public required string Name { get; set; }

                                /// <summary>
                                /// 【震央地名コード】（1回）
                                /// </summary>
                                /// <remarks>震央地名コードを示す。@typeに参照すべきコード種別“震央地名”を記載する。対応するコードについては、「コード表（震央地名）」を参照。</remarks>
                                public required string Code { get; set; }

                                /// <summary>
                                /// 【震源要素】（1 回）[jmx_eb]
                                /// </summary>
                                /// <remarks>ISO6709 の規格に従い、震源の緯度、経度を度単位（世界測地系）で、深さをメートル単位で記載し、属性@description に文字列表現を記載する。本要素に記載する深さの値は、深さ700km より浅いところでは 10,000 メートルの単位が有効であり、@description における深さは1,000 メートルの位を四捨五入して10km単位で表現する。</remarks>
                                public required string A_Coordinate_description { get; set; }

                                /// <summary>
                                /// 【震源要素】（1 回）[jmx_eb]
                                /// </summary>
                                /// <remarks>ISO6709 の規格に従い、震源の緯度、経度を度単位（世界測地系）で、深さをメートル単位で記載し、属性@description に文字列表現を記載する。本要素に記載する深さの値は、深さ700km より浅いところでは 10,000 メートルの単位が有効であり、@description における深さは1,000 メートルの位を四捨五入して10km単位で表現する。</remarks>
                                public string? Coordinate { get; set; }

                                /*
事例 
<jmx_eb:Coordinate description="北緯３９．０度 東経１４０．９度 深さ １０ｋｍ">+39.0+140.9-10000/</jmx_eb:Coordinate> 
例外表現１ （深さの例外表現） 
・震源の深さがごく浅い場合 
<jmx_eb:Coordinate description="北緯３９．０度 東経１４０．９度 ごく浅い">+39.0+140.9+0/</jmx_eb:Coordinate> 
・震源の深さがごく浅い場合（０ｋｍ） 
<jmx_eb:Coordinate description="北緯３９．０度 東経１４０．９度 ごく浅い">+39.0+140.9+0/</jmx_eb:Coordinate> 
（※「緊急地震速報（警報）」、「緊急地震速報（予報）」の場合、現行の運用では、震源の深さを「ごく浅い」とせずに、本要素の内容、属性「@description」において、震源の深さを10kmとして扱い発表する。） 
例外表現２ （全要素が不明の場合） 
<jmx_eb:Coordinate description="震源要素不明" /> 
・震源の深さが700km以上の場合 
<jmx_eb:Coordinate description="北緯３９．０度 東経１４０．９度 深さは７００ｋｍ以上">+39.0+140.9-700000/</jmx_eb:Coordinate> 
・震源の深さが不明の場合 
<jmx_eb:Coordinate description="北緯３９．０度 東経１４０．９度 深さ不明">+39.0+140.9/</jmx_eb:Coordinate> 
                                 */

                                /// <summary>
                                /// 【短縮用震央地名】（1回）
                                /// </summary>
                                /// <remarks>短縮用震央地名を記載する。また、これに対応するコードを、ReduceCodeに記載する。</remarks>
                                public required string ReduceName { get; set; }

                                /// <summary>
                                /// 【短縮用震央地名コード】（1回）
                                /// </summary>
                                /// <remarks>短縮用震央地名コードを示す。参照すべきコード種別“短縮用震央地名”を@type に記載する。対応するコードについては、「コード表（短縮用震央地名）」を参照。 </remarks>
                                public required string ReduceCode { get; set; }

                                /// <summary>
                                /// 【内陸判定】（0 回/1回，値：“内陸”/“海域”）
                                /// </summary>
                                /// <remarks>震央位置が内陸か海域かを判定する。 （※気象庁の部内システムでの利用（予告無く変更することがある）） ○「緊急地震速報（地震動予報）」、および「緊急地震速報（予報）」において、非常に強い揺れを検知・最大予測震度のみの場合、 本要素は出現しない。 ○「仮定震源要素」の場合、 本要素は出現しない。 </remarks>
                                public string? LandOrSea { get; set; }
                            }

                            /// <summary>
                            /// 【精度情報】（1 回）
                            /// </summary>
                            /// <remarks>精度情報の諸要素を示す。</remarks>
                            public required C_Accuracy Accuracy { get; set; }

                            /// <summary>
                            /// 【精度情報】（1 回）
                            /// </summary>
                            /// <remarks>精度情報の諸要素を示す。</remarks>
                            public class C_Accuracy
                            {
                                /// <summary>
                                /// 【震央位置の精度値】（1 回，値：“NaN”） 
                                /// </summary>
                                /// <remarks>震央位置の精度値を示す。内容は“NaN”固定となり、属性@rank、@rank2を持つ。</remarks>
                                public required string Epicenter { get; set; }

                                /// <summary>
                                /// 【震央位置の精度値】[属性（@rank）]（1 回，値： “0”/“1”/“2”/“3”/“4”/“5”/“6”/“7”/“8”）
                                /// </summary>
                                /// <remarks>震源位置の精度のランクを示す。</remarks>
                                public required string A_Epicenter_rank { get; set; }
                                /*
0  ：不明 
1  ：P 波／S 波レベル超え、IPF法（1 点）、または「仮定震源要素」の場合 〔気象庁データ〕 
2  ：IPF法（2 点） 〔気象庁データ〕 
3  ：IPF法（3 点／4 点） 〔気象庁データ〕 
4  ：IPF法（5 点以上）） 〔気象庁データ〕 
5  ：防災科研システム（4 点以下、または精度情報なし） 〔防災科学技術研究所データ[以下、防災科研Hi-netデータ]〕 
6  ：防災科研システム（5 点以上） 〔防災科研Hi-netデータ〕 
7  ：EPOS（海域〔観測網外〕） 
8  ：EPOS（内陸〔観測網内〕）
                                 */

                                /// <summary>
                                /// 【震央位置の精度値】[属性（@rank2）]（1 回，値： “0”/“1”/“2”/“3”/“4”/”9”）
                                /// </summary>
                                /// <remarks>震源位置の精度のランク２を示す。（※値が1,9以外については気象庁の部内システムでの利用（予告無く変更することがある））</remarks>
                                public required string A_Epicenter_rank2 { get; set; }
                                /*
0  ：不明 
1  ：P 波／S 波レベル超え、IPF法（1 点）、または「仮定震源要素」の場合 
2  ：IPF法（2 点） 
3  ：IPF法（3 点／4 点） 
4  ：IPF法（5 点以上） 
9  ：震源とマグニチュードに基づく震度予測手法での精度が最終報相当（推定震源とマグニチュードはこれ以降変化しない。ただし、PLUM法により予測震度が今後変化する可能性はある。） 
                                 */

                                /// <summary>
                                /// 【深さの精度値】（1回，値：“NaN”） 
                                /// </summary>
                                /// <remarks>深さの精度値を示す。 内容は“NaN”固定となり、属性 @rankを持つ。</remarks>
                                public required string Depth { get; set; }

                                /// <summary>
                                /// 【深さの精度値】[属性（@rank）]（1 回，値： “0”/“1”/“2”/“3”/“4”/“5”/“6”/“7”/“8”） 
                                /// </summary>
                                /// <remarks>震源深さの精度のランク</remarks>
                                public required string A_Depth_rank { get; set; }
                                /*
0  ：不明 
1  ：P 波／S 波レベル超え、IPF法（1 点）、または「仮定震源要素」の場合 〔気象庁データ〕 
2  ：IPF法（2 点） 〔気象庁データ〕 
3  ：IPF法（3 点／4 点） 〔気象庁データ〕 
4  ：IPF法（5 点以上）） 〔気象庁データ〕 
5  ：防災科研システム（4 点以下、または精度情報なし） 〔防災科学技術研究所データ[以下、防災科研Hi-netデータ]〕 
6  ：防災科研システム（5 点以上） 〔防災科研Hi-netデータ〕 
7  ：EPOS（海域〔観測網外〕） 
8  ：EPOS（内陸〔観測網内〕）
                                 */

                                /// <summary>
                                /// 【マグニチュードの精度値】（1 回，値：“NaN”） 
                                /// </summary>
                                /// <remarks>マグニチュードの精度値を示す。 内容は“NaN”固定となり、属性 @rankを持つ。 </remarks>
                                public required string MagnitudeCalculation { get; set; }

                                /// <summary>
                                /// 【マグニチュードの精度値】[属性（@rank）]（1 回，値： “0”/“2”/“3”/“4”/“5”/“6”/“8”）
                                /// </summary>
                                /// <remarks>マグニチュード精度のランク </remarks>
                                public required string A_MagnitudeCalculation_rank { get; set; }
                                /*
0  ：不明 
2  ：防災科研システム 〔防災科研Hi-netデータ〕 
3  ：全点P 相 
4  ：P 相／全相混在 
5  ：全点全相 
6  ：EPOS 
8  ：P 波／S 波レベル超え、または仮定震源要素の場合
                                 */

                                /// <summary>
                                /// 【マグニチュード計算使用観測点数】 （1 回，値： “0”/“1”/“2”/“3”/“4”/“5”）
                                /// </summary>
                                /// <remarks>マグニチュード計算使用観測点数を示す。 （※気象庁の部内システムでの利用（予告無く変更することがある）） </remarks>
                                public required string NumberOfMagnitudeCalculation { get; set; }
                                /*
0  ：不明 
1  ：1 点、P 波／S 波レベル超え、または仮定震源要素の場合 
2  ：2 点 
3  ：3 点 
4  ：4 点 
5  ：5 点以上 
                                 *//*
事例１ （震央・深さ・M：P波／S波レベル超え、1点、または「仮定震源要素」の場合による表現） 
<Accuracy> 
<Epicenter rank="1" rank2="1">NaN</Epicenter>  
<Depth rank="1">NaN</Depth>  
<MagnitudeCalculation rank="8">NaN</MagnitudeCalculation>  
<NumberOfMagnitudeCalculation>1</NumberOfMagnitudeCalculation>  
</Accuracy> 
事例２ （震央・深さ：IPF法（2点）、M：P相/全相混在、1点またはP波／S波レベル超えによる表現） 
<Accuracy> 
<Epicenter rank="2" rank2="2">NaN</Epicenter>  
<Depth rank="2">NaN</Depth>  
<MagnitudeCalculation rank="4">NaN</MagnitudeCalculation>  
<NumberOfMagnitudeCalculation>1</NumberOfMagnitudeCalculation>  
</Accuracy> 
事例３ （震央・深さ：IPF法（3点／4点）、M: P 相/全相混在、3点による表現） 
<Accuracy> 
<Epicenter rank="3" rank2="3">NaN</Epicenter>  
<Depth rank="3">NaN</Depth>  
<MagnitudeCalculation rank="4">NaN</MagnitudeCalculation>  
<NumberOfMagnitudeCalculation>3</NumberOfMagnitudeCalculation>  
</Accuracy> 
事例４ （震央・深さ：防災科研システム（5点以上）、M:全点全相、5点以上 による表現） 
<Accuracy> 
<Epicenter rank="6" rank2="4">NaN</Epicenter>  
<Depth rank="6">NaN</Depth>  
<MagnitudeCalculation rank="5">NaN</MagnitudeCalculation>  
<NumberOfMagnitudeCalculation>5</NumberOfMagnitudeCalculation>  
</Accuracy> 
事例５ （震源とマグニチュードに基づく震度予測手法での精度が最終報相当の場合の表現） 
<Accuracy> 
<Epicenter rank="6" rank2="9">NaN</Epicenter>  
<Depth rank="6">NaN</Depth>  
<MagnitudeCalculation rank="5">NaN</MagnitudeCalculation>  
<NumberOfMagnitudeCalculation>5</NumberOfMagnitudeCalculation> 
</Accuracy>
                                 */
                            }
                        }

                        /// <summary>
                        /// 【マグニチュード】（1 回） [jmx_eb]
                        /// </summary>
                        /// <remarks>地震のマグニチュードの値を記載する。 @type にはマグニチュードの種別を、@descripionには文字列表現を記載する。 マグニチュードが不明の場合、さらに@condition を追加し、マグニチュードの値が不明である旨を示す固定値“不明”を記載する。内容は“NaN”とする。 ○「仮定震源要素」の場合、 “1.0”とする。</remarks>
                        public required string Magnitude { get; set; }

                        /// <summary>
                        /// 【マグニチュード】（@type） [jmx_eb]
                        /// </summary>
                        /// <remarks>マグニチュードの種別</remarks>
                        public required string A_Magnitude_type { get; set; }

                        /// <summary>
                        /// 【マグニチュード】（@description） [jmx_eb]
                        /// </summary>
                        /// <remarks>文字列表現</remarks>
                        public required string A_Magnitude_description { get; set; }

                        /// <summary>
                        /// 【マグニチュード】（@condition） [jmx_eb]
                        /// </summary>
                        /// <remarks>マグニチュードが不明の場合、マグニチュードの値が不明である旨を示す固定値“不明”を記載する。</remarks>
                        public string? A_Magnitude_condition { get; set; }

                        /*
事例１ （気象庁マグニチュードによる表現） 
<jmx_eb:Magnitude type="Mj" description="Ｍ６．６">6.6</jmx_eb:Magnitude> 
事例２ （マグニチュードが不明の場合） 
<jmx_eb:Magnitude type="Mj" condition="不明" description="M不明">NaN</jmx_eb:Magnitude>
                         */
                    }

                    /// <summary>
                    /// 【震度・長周期地震動階級】（0回/1回）
                    /// </summary>
                    /// <remarks>震度・長周期地震動階級に関する情報を記載する。ただし、以下の場合本要素は出現しない。 ○震源の深さが150ｋmより深く推定された場合。（PLUM法による2点以上での震度予測がある場合を除く。） ○観測点1点による震度予測の場合。 ○ヘッダ部の「情報形態」（Head/InfoType要素）が“取消”の場合。 ○「リアルタイム震度電文」で、盛り込むべきリアルタイム震度（工学的基盤面の値）がない場合。</remarks>
                    public C_Intensity? Intensity { get; set; }

                    /// <summary>
                    /// 【震度・長周期地震動階級】（0回/1回）
                    /// </summary>
                    /// <remarks>震度・長周期地震動階級に関する情報を記載する。ただし、以下の場合本要素は出現しない。 ○震源の深さが150ｋmより深く推定された場合。（PLUM法による2点以上での震度予測がある場合を除く。） ○観測点1点による震度予測の場合。 ○ヘッダ部の「情報形態」（Head/InfoType要素）が“取消”の場合。 ○「リアルタイム震度電文」で、盛り込むべきリアルタイム震度（工学的基盤面の値）がない場合。</remarks>
                    public class C_Intensity
                    {
                        /// <summary>
                        /// 【震度・長周期地震動の予測】（0回/1回） 
                        /// </summary>
                        /// <remarks>震度・長周期地震動階級の予測に関する情報を記載する。 ○「リアルタイム震度電文」の場合、 本要素は出現しない。</remarks>
                        public C_Forecast? Forecast { get; set; }

                        /// <summary>
                        /// 【震度・長周期地震動の予測】（0回/1回） 
                        /// </summary>
                        /// <remarks>震度・長周期地震動階級の予測に関する情報を記載する。 ○「リアルタイム震度電文」の場合、 本要素は出現しない。</remarks>
                        public class C_Forecast
                        {
                            /// <summary>
                            /// 【コード体系の定義】（0回/1回） 
                            /// </summary>
                            /// <remarks>内容部の「震度・長周期地震動階級の予測」（Intensity/Forecast）で使用するコード体系を定義する。 ○「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 震度予測を行う府県予報区（Pref）、細分区域（Area）が１つも無い時には、 本要素は出現しない。</remarks>
                            public C_CodeDefine? CodeDefine { get; set; }

                            /// <summary>
                            /// 【コード体系の定義】（0回/1回） 
                            /// </summary>
                            /// <remarks>内容部の「震度・長周期地震動階級の予測」（Intensity/Forecast）で使用するコード体系を定義する。 ○「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 震度予測を行う府県予報区（Pref）、細分区域（Area）が１つも無い時には、 本要素は出現しない。</remarks>
                            public class C_CodeDefine
                            {
                                /// <summary>
                                /// 【コード体系の種別】（3回）
                                /// </summary>
                                /// <remarks>コード種別を記載する。 ○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 通常使用するコード体系は、“緊急地震速報／府県予報区”、“地震情報／細分区域”、及び“緊急地震速報”となる。コードについては、別途提供するコード表を参照。 [属性（@ xpath）]（1 回） 定義したコードを使用する要素の相対的な出現位置を記載する。 </remarks>
                                public required C_Type[] Type { get; set; }

                                /// <summary>
                                /// 【コード体系の種別】（3回）
                                /// </summary>
                                /// <remarks>コード種別を記載する。 ○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 通常使用するコード体系は、“緊急地震速報／府県予報区”、“地震情報／細分区域”、及び“緊急地震速報”となる。コードについては、別途提供するコード表を参照。 [属性（@ xpath）]（1 回） 定義したコードを使用する要素の相対的な出現位置を記載する。 </remarks>
                                public class C_Type
                                {
                                    /// <summary>
                                    /// [TypeのInnerText]コード種別を記載する。 ○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 通常使用するコード体系は、“緊急地震速報／府県予報区”、“地震情報／細分区域”、及び“緊急地震速報”となる。コードについては、別途提供するコード表を参照。 
                                    /// </summary>
                                    public required string Value { get; set; }

                                    /// <summary>
                                    /// [属性（@ xpath）]（1 回） 
                                    /// </summary>
                                    /// <remarks>定義したコードを使用する要素の相対的な出現位置を記載する。</remarks>
                                    public required string A_xpath { get; set; }
                                }
                                /*
事例 
<CodeDefine> 
<Type xpath="Pref/Code">緊急地震速報／府県予報区</Type>  
<Type xpath="Pref/Area/Code">地震情報／細分区域</Type>  
<Type xpath="Pref/Area/Category/Kind/Code">緊急地震速報</Type>  
</CodeDefine>
                                 */
                            }

                            /// <summary>
                            /// 【最大予測震度】（1回）
                            /// </summary>
                            /// <remarks>最大予測震度を示す。 本要素は、子要素（From、To）を持つ。通常、２つの子要素の内容には同じ値を記載する。ただし、「～程度以上」の表現を用いる場合については、To要素の内容は「over」と記載する。</remarks>
                            public required C_ForecastInt ForecastInt { get; set; }

                            /// <summary>
                            /// 【最大予測震度】（1回）
                            /// </summary>
                            /// <remarks>最大予測震度を示す。 本要素は、子要素（From、To）を持つ。通常、２つの子要素の内容には同じ値を記載する。ただし、「～程度以上」の表現を用いる場合については、To要素の内容は「over」と記載する。</remarks>
                            public class C_ForecastInt
                            {
                                /// <summary>
                                /// 【最大予測震度の下限】 （1回，値：“0”/“1”/“2”/“3”/“4”/“5-”/“5+”/“6-”/“6+”/“7”/“不明”）
                                /// </summary>
                                /// <remarks>最大予測震度の下限を示す。 </remarks>
                                public required string From { get; set; }
                                /*
0  ：震度0 
1  ：震度1 
2  ：震度2 
3  ：震度3 
4  ：震度4 
5- ：震度5弱 
5+ ：震度5強 
6- ：震度6弱 
6+ ：震度6強 
7  ：震度7 
不明：不明時 
                                 */

                                /// <summary>
                                /// 【最大予測震度の上限】 （1回，値：“0”/“1”/“2”/“3”/“4”/“5-”/“5+”/“6-”/“6+”/“7”/“over”/“不明”）
                                /// </summary>
                                /// <remarks>最大予測震度の上限を示す。 </remarks>
                                public required string To { get; set; }
                                /*
0  ：震度0 
1  ：震度1 
2  ：震度2 
3  ：震度3 
4  ：震度4 
5- ：震度5弱 
5+ ：震度5強 
6- ：震度6弱 
6+ ：震度6強 
7  ：震度7 
over:～程度以上 
不明：不明時 
                                 */
                            }
                            /*
事例１ （最大予測震度が震度5弱程度以上の場合（「程度以上」の表現）） 
<ForecastInt> 
<From>5-</From>  
<To>over</To>  
</ForecastInt> 
事例２ （最大予測震度が震度6弱の場合） 
<ForecastInt> 
<From>6-</From>  
<To>6-</To>  
</ForecastInt>
                             */

                            /// <summary>
                            /// 【最大予測長周期地震動階級】（0回/1回） 
                            /// </summary>
                            /// <remarks>最大予測長周期地震動階級を示す。 本要素は、子要素（From、To）を持つ。通常、２つの子要素の内容には同じ値を記載する。ただし、「～程度以上」の表現を用いる場合については、To要素の内容は「over」と記載する。 ○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」の場合のみ、 本要素が出現する（震源の深さが150ｋmより深く推定された場合を除く）</remarks>
                            public C_ForecastLgInt? ForecastLgInt { get; set; }

                            /// <summary>
                            /// 【最大予測長周期地震動階級】（0回/1回） 
                            /// </summary>
                            /// <remarks>最大予測長周期地震動階級を示す。 本要素は、子要素（From、To）を持つ。通常、２つの子要素の内容には同じ値を記載する。ただし、「～程度以上」の表現を用いる場合については、To要素の内容は「over」と記載する。 ○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」の場合のみ、 本要素が出現する（震源の深さが150ｋmより深く推定された場合を除く）</remarks>
                            public class C_ForecastLgInt
                            {
                                /// <summary>
                                /// 【最大予測長周期地震動階級の下限】 （1 回，値：“0”/“1”/“2”/“3”/“4”/“不明”） 
                                /// </summary>
                                /// <remarks>最大予測長周期地震動階級の下限を示す。 </remarks>
                                public required string From { get; set; }
                                /*
0  ：長周期地震動階級1未満 
1  ：長周期地震動階級1 
2  ：長周期地震動階級2 
3  ：長周期地震動階級3 
4  ：長周期地震動階級4 
不明：不明時  
                                 */

                                /// <summary>
                                /// 【最大予測長周期地震動階級の上限】 （1 回，値：“0”/“1”/“2”/“3”/“4”/“over”/“不明”） 
                                /// </summary>
                                /// <remarks>最大予測長周期地震動階級の上限を示す。 </remarks>
                                public required string To { get; set; }
                                /*
0  ：長周期地震動階級1未満 
1  ：長周期地震動階級1 
2  ：長周期地震動階級2 
3  ：長周期地震動階級3 
4  ：長周期地震動階級4 
over:～程度以上 
不明：不明時  
                                 */
                            }
                            /*
事例１ （最大予測長周期地震動階級が階級3程度以上の場合（「程度以上」の表現）） 
<ForecastLgInt> 
<From>3</From>  
<To>over</To>  
</ForecastInt> 
事例２ （最大予測長周期地震動階級が階級3の場合） 
<ForecastLgInt> 
<From>3</From>  
<To>3</To>  
</ForecastLgInt> 
                             */

                            /// <summary>
                            /// 【予測震度・予測長周期地震動階級付加要素】（0回/1回） 
                            /// </summary>
                            /// <remarks>予測震度及び予測長周期地震動階級の付加要素を示す。 ○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 震度予測及び長周期地震動階級予測をどちらも行っていないために、直前の緊急地震速報（※）と今回の緊急地震速報の間で最大予測震度及び最大予測長周期地震動階級の比較ができない場合、本要素は出現しない。 ※ここでは、直前の「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」または「緊急地震速報（予報）」が比較対象となり、警報と予報の区別はしない。</remarks>
                            public C_Appendix? Appendix { get; set; }

                            /// <summary>
                            /// 【予測震度・予測長周期地震動階級付加要素】（0回/1回） 
                            /// </summary>
                            /// <remarks>予測震度及び予測長周期地震動階級の付加要素を示す。 ○「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 震度予測及び長周期地震動階級予測をどちらも行っていないために、直前の緊急地震速報（※）と今回の緊急地震速報の間で最大予測震度及び最大予測長周期地震動階級の比較ができない場合、本要素は出現しない。 ※ここでは、直前の「緊急地震速報（警報）」、「緊急地震速報（地震動予報）」または「緊急地震速報（予報）」が比較対象となり、警報と予報の区別はしない。</remarks>
                            public class C_Appendix
                            {
                                /// <summary>
                                /// 【最大予測震度変化】（1回，値：“0”/“1”/“2”） 
                                /// </summary>
                                /// <remarks>最大予測震度変化を示す。</remarks>
                                public required string MaxIntChange { get; set; }
                                /*
0  ：ほとんど変化なし 
1  ：最大予測震度が1.0 以上大きくなった。 
2  ：最大予測震度が1.0 以上小さくなった。 
                                 */

                                /// <summary>
                                /// 【最大予測長周期地震動階級変化】（0回/1回，値：“0”/“1”/“2”）
                                /// </summary>
                                /// <remarks>最大予測長周期地震動階級変化を示す。 ○「緊急地震速報（警報）」または「緊急地震速報（地震動予報）」の場合、かつ、ForecastLgInt が出現する場合に、本要素が出現する。</remarks>
                                public string? MaxLgIntChange { get; set; }
                                /*
0  ：変化なし 
1  ：最大予測長周期地震動階級が1以上大きくなった。 
2  ：最大予測長周期地震動階級が1以上小さくなった。
                                 */

                                /// <summary>
                                /// 【最大予測値変化の理由】 （1 回, 値：“0”/“1”/“2”/“3”/“4”/”9”）
                                /// </summary>
                                /// <remarks>最大予測震度または最大予測長周期地震動階級変化の理由を示す。</remarks>
                                public required string MaxIntChangeReason { get; set; }
                                /*
0  ：変化なし 
1  ：主としてＭが変化したため(1.0 以上)。 
2  ：主として震央位置が変化したため(10.0km 以上)。 
3  ：Ｍ及び震央位置が変化したため(1 と2 の複合条件)。 
4  ：震源の深さが変化したため(上記のいずれにもあてはまらず、30.0km 以上の変化)。 
9  ：PLUM法による予測により変化したため。 
                                 */
                            }

                            public C_Pref[]? Pref { get; set; }

                            /// <summary>
                            /// 【都道府県要素】（0回以上） 
                            /// </summary>
                            /// <remarks>府県予報区の諸要素を示す。 本情報で、緊急地震速報（警報）、緊急地震速報（地震動予報）、緊急地震速報（予報）を発表している府県予報区（Pref）、細分区域（Area）について、発表状況を記載する。記載する府県予報区、細分区域の数に応じて、本要素が複数出現する。 震○「緊急地震速報（警報）」及び「緊急地震速報（予報）」の場合、 度予測・長周期地震動階級予測を行う府県予報区（Pref）、細分区域（Area）全てについて、本要素を繰り返して記載する（最大震度 4 以上または最大予測長周期地震動階級が３以上と予測された区域について、本要素を記載する）。 ○「緊急地震速報（地震動予報）」の場合、 震度予測・長周期地震動階級予測を行う府県予報区（Pref）、細分区域（Area）全てについⅡ.21－12 て、本要素を繰り返して記載する（最大震度 4 以上または最大予測長周期地震動階級が１以上と予測された区域について、本要素を記載する）。 ○「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」の場合、 震度・長周期地震動予測を行う府県予報区（Pref）、細分区域（Area）が１つも無い時には、 本要素は出現しない。 ○「緊急地震速報（地震動予報）」及び「緊急地震速報（予報）」において、非常に強い揺れを検知・最大予測震度のみの場合、 本要素は出現しない。 </remarks>
                            public class C_Pref
                            {
                                public required string Name { get; set; }
                                public required string Code { get; set; }

                                public required C_Area Area { get; set; }
                                public class C_Area
                                {
                                    public required string Name { get; set; }
                                    public required string Code { get; set; }
                                    public required C_Category Category { get; set; }
                                    public class C_Category
                                    {

                                        public required C_Kind Kind { get; set; }
                                        public class C_Kind
                                        {
                                            public required string Name { get; set; }
                                            public required string Code { get; set; }
                                        }

                                    }

                                    public required C_ForecastInt ForecastInt { get; set; }

                                    public class C_ForecastInt
                                    {
                                        /// <summary>
                                        /// 【最大予測震度の下限】 （1回，値：“0”/“1”/“2”/“3”/“4”/“5-”/“5+”/“6-”/“6+”/“7”/“不明”）
                                        /// </summary>
                                        /// <remarks>最大予測震度の下限を示す。 </remarks>
                                        public required string From { get; set; }
                                        /*
        0  ：震度0 
        1  ：震度1 
        2  ：震度2 
        3  ：震度3 
        4  ：震度4 
        5- ：震度5弱 
        5+ ：震度5強 
        6- ：震度6弱 
        6+ ：震度6強 
        7  ：震度7 
        不明：不明時 
                                         */

                                        /// <summary>
                                        /// 【最大予測震度の上限】 （1回，値：“0”/“1”/“2”/“3”/“4”/“5-”/“5+”/“6-”/“6+”/“7”/“over”/“不明”）
                                        /// </summary>
                                        /// <remarks>最大予測震度の上限を示す。 </remarks>
                                        public required string To { get; set; }
                                        /*
        0  ：震度0 
        1  ：震度1 
        2  ：震度2 
        3  ：震度3 
        4  ：震度4 
        5- ：震度5弱 
        5+ ：震度5強 
        6- ：震度6弱 
        6+ ：震度6強 
        7  ：震度7 
        over:～程度以上 
        不明：不明時 
                                         */
                                    }

                                    public C_ForecastLgInt? ForecastLgInt { get; set; }

                                    public class C_ForecastLgInt
                                    {
                                        /// <summary>
                                        /// 【最大予測長周期地震動階級の下限】 （1 回，値：“0”/“1”/“2”/“3”/“4”/“不明”） 
                                        /// </summary>
                                        /// <remarks>最大予測長周期地震動階級の下限を示す。 </remarks>
                                        public required string From { get; set; }
                                        /*
        0  ：長周期地震動階級1未満 
        1  ：長周期地震動階級1 
        2  ：長周期地震動階級2 
        3  ：長周期地震動階級3 
        4  ：長周期地震動階級4 
        不明：不明時  
                                         */

                                        /// <summary>
                                        /// 【最大予測長周期地震動階級の上限】 （1 回，値：“0”/“1”/“2”/“3”/“4”/“over”/“不明”） 
                                        /// </summary>
                                        /// <remarks>最大予測長周期地震動階級の上限を示す。 </remarks>
                                        public required string To { get; set; }
                                        /*
        0  ：長周期地震動階級1未満 
        1  ：長周期地震動階級1 
        2  ：長周期地震動階級2 
        3  ：長周期地震動階級3 
        4  ：長周期地震動階級4 
        over:～程度以上 
        不明：不明時  
                                         */
                                    }


                                    //public class C_ArrivalTime//todo:ここから

                                }
                            }


                        }




                    }



                }
            }
            /*//このソフトでは実装無し
            public class Tsunami : EqVol_Common
            {

            }*/

            /// <summary>
            /// 緊急地震速報
            /// </summary>
            public class EarthquakeEarlyWarning : EqVol_Common
            {
                /// <inheritdoc />
                public new class C_Head : EqVol_Common.C_Head
                {
                    /// <inheritdoc />
                    public new class C_Headline : EqVol_Common.C_Head.C_Headline
                    {
                        /// <summary>
                        /// 【見出し防災気象情報事項】（0回/3回）
                        /// </summary>
                        /// <remarks>本要素は緊急地震速報（警報）及び緊急地震速報（地震動予報）のみに出現し、緊急地震速報(予報)及び緊急地震速報の配信テスト電文には出現しない。また、情報形態（Head/InfoType）が“取消”の場合も出現しない。 緊急地震速報(警報)及び緊急地震速報（地震動予報）で本要素が出現する場合には、@type が“緊急地震速報(地方予報区)”、 “緊急地震速報(府県予報区)”、及び“緊急地震速報(細分区域)”である本要素が各々１回ずつ出現する。各々の Information 要素は子要素としてItem をもつ。 </remarks>
                        public new C_Information[]? Information { get; set; }

                        /// <summary>
                        /// 【見出し防災気象情報事項】（0回/3回）
                        /// </summary>
                        /// <remarks>本要素は緊急地震速報（警報）及び緊急地震速報（地震動予報）のみに出現し、緊急地震速報(予報)及び緊急地震速報の配信テスト電文には出現しない。また、情報形態（Head/InfoType）が“取消”の場合も出現しない。 緊急地震速報(警報)及び緊急地震速報（地震動予報）で本要素が出現する場合には、@type が“緊急地震速報(地方予報区)”、 “緊急地震速報(府県予報区)”、及び“緊急地震速報(細分区域)”である本要素が各々１回ずつ出現する。各々の Information 要素は子要素としてItem をもつ。 </remarks>
                        public new class C_Information
                        {
                            /// <summary>
                            /// <see cref="C_Information"/>@type
                            /// </summary>
                            /// <remarks>“緊急地震速報(地方予報区)”、 “緊急地震速報(府県予報区)”、及び“緊急地震速報(細分区域)”である本要素が各々１回ずつ出現する。</remarks>
                            public required string A_Type { get; set; }

                            /// <summary>
                            /// 【個々の防災気象情報要素】（1回/2回） 
                            /// </summary>
                            /// <remarks>緊急地震速報(警報)の発表状況を記載する。子要素として、Kind、LastKind及びAreasをもつ。警報では本要素は原則として１回のみの出現だが、新たに警報対象となる区域がある場合には、当該警報の電文において本要素は２回出現する（下表参照）。</remarks>
                            public required C_Item[] Item { get; set; }

                            /// <summary>
                            /// 【個々の防災気象情報要素】（1回/2回） 
                            /// </summary>
                            /// <remarks>緊急地震速報(警報)の発表状況を記載する。子要素として、Kind、LastKind及びAreasをもつ。警報では本要素は原則として１回のみの出現だが、新たに警報対象となる区域がある場合には、当該警報の電文において本要素は２回出現する（下表参照）。</remarks>
                            public class C_Item
                            {
                                /// <summary>
                                /// 【防災気象情報要素】（1回）
                                /// </summary>
                                /// <remarks>子要素にNameとCodeをもつ。 </remarks>
                                public required C_Kind Kind { get; set; }

                                /// <summary>
                                /// 【防災気象情報要素】（1回）
                                /// </summary>
                                /// <remarks>子要素にNameとCodeをもつ。 </remarks>
                                public class C_Kind
                                {
                                    /// <summary>
                                    /// 【防災気象情報要素名】（1回） 
                                    /// </summary>
                                    /// <remarks>防災気象情報名を記載する。現行の運用では、とりうる値としては“緊急地震速報(警報)”のみである（11-2(2)-1 の表参照）。</remarks>
                                    public required string Name { get; set; }

                                    /// <summary>
                                    /// 【防災気象情報要素コード】（1回）
                                    /// </summary>
                                    /// <remarks>上記Nameの内容に対応するコード（“警報等情報要素／緊急地震速報”）を記載する。</remarks>
                                    public required string Code { get; set; }
                                }

                                /// <summary>
                                /// 【直前の防災気象情報要素】（1回）
                                /// </summary>
                                /// <remarks>子要素にNameとCodeをもつ。</remarks>
                                public required C_LastKind LastKind { get; set; }

                                /// <summary>
                                /// 【直前の防災気象情報要素】（1回）
                                /// </summary>
                                /// <remarks>子要素にNameとCodeをもつ。</remarks>
                                public class C_LastKind
                                {
                                    /// <summary>
                                    /// 【防災気象情報要素名】（1回）
                                    /// </summary>
                                    /// <remarks>防災気象情報名を記載する。現行の運用では、とりうる値としては“緊急地震速報(警報)”又は“なし”である（11-2(2)-1の表参照）。 </remarks>
                                    public required string Name { get; set; }

                                    /// <summary>
                                    /// 【防災気象情報要素コード】（1回）
                                    /// </summary>
                                    /// <remarks>上記Nameの内容に対応するコード（“警報等情報要素／緊急地震速報”）を記載する。 </remarks>
                                    public required string Code { get; set; }
                                }

                                /// <summary>
                                /// 【対象地域・地点】（1 回）
                                /// </summary>
                                /// <remarks>Kind 及び LastKind の防災気象情報名の組み合わせに該当する区域を記載する。Information/@type の値に応じて本要素の@codeType が“緊急地震速報／地方予報区”、 “緊急地震速報／府県予報区”、又は“地震情報／細分区域”に設定される。 子要素にAreaをもつ。 </remarks>
                                public required C_Areas Areas { get; set; }

                                /// <summary>
                                /// 【対象地域・地点】（1 回）
                                /// </summary>
                                /// <remarks>Kind 及び LastKind の防災気象情報名の組み合わせに該当する区域を記載する。Information/@type の値に応じて本要素の@codeType が“緊急地震速報／地方予報区”、 “緊急地震速報／府県予報区”、又は“地震情報／細分区域”に設定される。 子要素にAreaをもつ。 </remarks>
                                public class C_Areas
                                {
                                    /// <summary>
                                    /// @codeType 
                                    /// </summary>
                                    /// <remarks>“緊急地震速報／地方予報区”、“緊急地震速報／府県予報区”、又は“地震情報／細分区域”に設定される。</remarks>
                                    public required string A_CodeType { get; set; }

                                    /// <summary>
                                    /// 【対象地域・地点】（1 回以上）  
                                    /// </summary>
                                    /// <remarks>子要素にNameとCodeをもつ。 </remarks>
                                    public required C_Area[] Areas { get; set; }

                                    /// <summary>
                                    /// 【対象地域・地点】（1 回以上）  
                                    /// </summary>
                                    /// <remarks>子要素にNameとCodeをもつ。 </remarks>
                                    public class C_Area
                                    {
                                        /// <summary>
                                        /// 【対象地域・地点名称】（1回） 
                                        /// </summary>
                                        /// <remarks>Areas/@codeType の値に応じて地方予報区、府県予報区又は細分区域のいずれかを記載する。</remarks>
                                        public required string Name { get; set; }

                                        /// <summary>
                                        /// 【対象地域・地点コード】（1回）
                                        /// </summary>
                                        /// <remarks>上記Name の内容に対応するコードを記載する。参照するコードはAreas/@codeType に記載されている。</remarks>
                                        public required string Code { get; set; }
                                    }
                                }
                            }
                        }
                    }
                }



            }




        }
    }
}
