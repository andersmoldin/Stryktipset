using StryktipsetCore.Contract;
using StryktipsetCore;
using StryktipsetCoreWin;
using static StryktipsetCore.Contract.Enums;
using Weighted_Randomizer;
using static System.Windows.Forms.ListViewItem;

namespace StryktipsetStats
{
    public partial class StryktipsetStats : Form
    {
        public StryktipsetStats()
        {
            InitializeComponent();
        }

        private void SlumpaEnOmg�ng_Click(object sender, EventArgs e)
        {
            var slumpadOmg�ng = SlumpaOmg�ng();
            List<int> dataX = new List<int>();
            List<int> dataY = new List<int>();
            var oddsfavoritskap = R�knaUtOddsfavoritskapOchSpelv�rde(slumpadOmg�ng, V�rde.Oddsfavoritskap);
            var spelv�rde = R�knaUtOddsfavoritskapOchSpelv�rde(slumpadOmg�ng, V�rde.Spelv�rde);
            var komboslump = R�knaUtOddsfavoritskapOchSpelv�rde(slumpadOmg�ng, V�rde.Komboslump, oddsfavoritskap, spelv�rde);
            var slump = R�knaUtOddsfavoritskapOchSpelv�rde(slumpadOmg�ng);

            foreach (var mSystem in MSystem.List.OrderByDescending(m => m.Value).Take(30))
            {
                var oddsfavoritskapStats = new Dictionary<int, int>();
                var spelv�rdeStats = new Dictionary<int, int>();
                var komboslumpStats = new Dictionary<int, int>();
                var slumpStats = new Dictionary<int, int>();
                var loops = 0;
                for (int i = 0; i <= 13; i++)
                {
                    oddsfavoritskapStats.Add(i, 0);
                    spelv�rdeStats.Add(i, 0);
                    komboslumpStats.Add(i, 0);
                    slumpStats.Add(i, 0);
                }
                var slumpadeGarderingar = SlumpaGarderingarF�rMSystem(mSystem.Value);

                do
                {
                    loops++;

                    var oddsfavoritskapRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                    var spelv�rdeRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                    var komboslumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                    var slumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);

                    oddsfavoritskapStats[R�ttaRad(slumpadOmg�ng, oddsfavoritskapRad)]++;
                    spelv�rdeStats[R�ttaRad(slumpadOmg�ng, spelv�rdeRad)]++;
                    komboslumpStats[R�ttaRad(slumpadOmg�ng, komboslumpRad)]++;
                    slumpStats[R�ttaRad(slumpadOmg�ng, slumpRad)]++;
                } while (oddsfavoritskapStats[13] < 1 && spelv�rdeStats[13] < 1 && komboslumpStats[13] < 1 && slumpStats[13] < 1);

                dataX.Add(mSystem.Value);
                dataY.Add(loops);

                //textBox.Text += $"{mSystem.Name}:\t";
                //textBox.Text += oddsfavoritskapStats[13] > 0 ? "Oddsfavorit" : "";
                //textBox.Text += spelv�rdeStats[13] > 0 ? "Spelv�rde" : "";
                //textBox.Text += komboslumpStats[13] > 0 ? "Komboslump" : "";
                //textBox.Text += slumpStats[13] > 0 ? "Slump" : "";
                //textBox.Text += Environment.NewLine;
            }

            plotterOmg�ngar.Plot.AddScatter(dataX.Select(x => (double)x).ToArray(), dataY.Select(x => (double)x).ToArray());
            plotterOmg�ngar.Refresh();
        }

        private void K�rAllaOmg�ngar_Click(object sender, EventArgs e)
        {
            var omg�ngar = H�mtaOmg�ngar();
            var dataX = new List<int>();
            var dataY = new List<int>();
            var totalStats = new Dictionary<int, Dictionary<int, int>>();

            foreach (var mSystem in MSystem.List.OrderByDescending(m => m.Value))
            {
                totalStats.Add(mSystem.Value, new Dictionary<int, int>() { { (int)V�rde.Oddsfavoritskap, 0 }, { (int)V�rde.Spelv�rde, 0 }, { (int)V�rde.Komboslump, 0 }, { (int)V�rde.Slump, 0 } });
            }

            foreach (var omg�ng in omg�ngar)
            {
                var oddsfavoritskap = R�knaUtOddsfavoritskapOchSpelv�rde(omg�ng, V�rde.Oddsfavoritskap);
                var spelv�rde = R�knaUtOddsfavoritskapOchSpelv�rde(omg�ng, V�rde.Spelv�rde);
                var komboslump = R�knaUtOddsfavoritskapOchSpelv�rde(omg�ng, V�rde.Komboslump, oddsfavoritskap, spelv�rde);
                var slump = R�knaUtOddsfavoritskapOchSpelv�rde(omg�ng);
                dataX = new List<int>();
                dataY = new List<int>();

                foreach (var mSystem in MSystem.List.OrderByDescending(m => m.Value))
                {
                    var oddsfavoritskapStats = new Dictionary<int, int>();
                    var spelv�rdeStats = new Dictionary<int, int>();
                    var komboslumpStats = new Dictionary<int, int>();
                    var slumpStats = new Dictionary<int, int>();
                    var loops = 0;
                    for (int i = 0; i <= 13; i++)
                    {
                        oddsfavoritskapStats.Add(i, 0);
                        spelv�rdeStats.Add(i, 0);
                        komboslumpStats.Add(i, 0);
                        slumpStats.Add(i, 0);
                    }
                    var slumpadeGarderingar = SlumpaGarderingarF�rMSystem(mSystem.Value);

                    do
                    {
                        loops++;

                        var oddsfavoritskapRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                        var spelv�rdeRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                        var komboslumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                        var slumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);

                        oddsfavoritskapStats[R�ttaRad(omg�ng, oddsfavoritskapRad)]++;
                        spelv�rdeStats[R�ttaRad(omg�ng, spelv�rdeRad)]++;
                        komboslumpStats[R�ttaRad(omg�ng, komboslumpRad)]++;
                        slumpStats[R�ttaRad(omg�ng, slumpRad)]++;
                    } while (oddsfavoritskapStats[13] < 1 && spelv�rdeStats[13] < 1 && komboslumpStats[13] < 1 && slumpStats[13] < 1);

                    dataX.Add(mSystem.Value);
                    dataY.Add(loops);

                    var row = new ListViewItem($"{mSystem.Name}");

                    if (oddsfavoritskapStats[13] > 0)
                    {
                        row.SubItems.Add("Oddsfavorit");
                        totalStats[mSystem.Value][(int)V�rde.Oddsfavoritskap]++;
                    }
                    else if (spelv�rdeStats[13] > 0)
                    {
                        row.SubItems.Add("Spelv�rde");
                        totalStats[mSystem.Value][(int)V�rde.Spelv�rde]++;
                    }
                    else if (komboslumpStats[13] > 0)
                    {
                        row.SubItems.Add("Komboslump");
                        totalStats[mSystem.Value][(int)V�rde.Komboslump]++;
                    }
                    else if (slumpStats[13] > 0)
                    {
                        row.SubItems.Add("Slump");
                        totalStats[mSystem.Value][(int)V�rde.Slump]++;
                    }
                    row.SubItems.Add(loops.ToString());

                    listView.Items.Add(row);
                }

                plotterOmg�ngar.Plot.AddScatter(dataX.Select(x => (double)x).ToArray(), dataY.Select(x => (double)x).ToArray());
                plotterOmg�ngar.Refresh();

                var listOfItems = listView.Items.Cast<ListViewItem>();
                listView.Columns[1].Text = $"Metod ({listOfItems.Count(i => i.SubItems.Cast<ListViewSubItem>().ToArray()[1].Text == "Oddsfavorit")})";
            }

            foreach (var v�rde in Enum.GetValues(typeof(V�rde)).Cast<V�rde>())
            {
                var systemX = totalStats.Keys.Select(s => (double)s).ToArray();
                var systemY = totalStats.Values.SelectMany(s => s).Where(s => s.Key == (int)v�rde).Select(s => (double)s.Value).ToArray();
                plotterSystem.Plot.AddScatter(systemX, systemY, label: v�rde.ToString());
                plotterSystem.Plot.Legend();
                plotterSystem.Refresh();
            }
        }

        private int R�ttaRad(Omg�ng slumpadOmg�ng, List<string> rad)
        {
            int count = 0;

            for (int i = 0; i < 13; i++)
            {
                if (rad.ElementAt(i).Contains(slumpadOmg�ng.R�ttRad[i]))
                {
                    count++;
                }
            }

            return count;
        }

        private static Dictionary<int, DynamicWeightedRandomizer<string>> R�knaUtOddsfavoritskapOchSpelv�rde(Omg�ng omg�ng, V�rde? v�rde = null, Dictionary<int, DynamicWeightedRandomizer<string>> oddsfavoritskap = null, Dictionary<int, DynamicWeightedRandomizer<string>> spelv�rde = null)
        {
            var viktning = new Dictionary<int, DynamicWeightedRandomizer<string>>();

            if (v�rde == V�rde.Komboslump)
            {
                Random random = new Random();

                for (int i = 0; i < 13; i++)
                {
                    viktning.Add(i, random.Next() % 2 == 0 ? oddsfavoritskap[i] : spelv�rde[i]);
                }
            }
            else
            {
                for (int i = 0; i < omg�ng.Matcher.Length; i++)
                {
                    viktning.Add(i, new DynamicWeightedRandomizer<string>());

                    if (omg�ng.Matcher[i].OddsetEtt == 0 | omg�ng.Matcher[i].OddsetKryss == 0 | omg�ng.Matcher[i].OddsetTv� == 0)
                    {
                        v�rde = null;
                    }

                    switch (v�rde)
                    {
                        case V�rde.Oddsfavoritskap:
                            viktning[i].Add(Tecken.Ett.Name, Convert.ToInt32(1 / omg�ng.Matcher[i].OddsetEtt * 100));
                            viktning[i].Add(Tecken.Kryss.Name, Convert.ToInt32(1 / omg�ng.Matcher[i].OddsetKryss * 100));
                            viktning[i].Add(Tecken.Tv�.Name, Convert.ToInt32(1 / omg�ng.Matcher[i].OddsetTv� * 100));
                            break;

                        case V�rde.Spelv�rde:
                            viktning[i].Add(Tecken.Ett.Name, Convert.ToInt32((1 / omg�ng.Matcher[i].OddsetEtt * 100) / omg�ng.Matcher[i].SvenskaFolketEttProcent * 100));
                            viktning[i].Add(Tecken.Kryss.Name, Convert.ToInt32((1 / omg�ng.Matcher[i].OddsetKryss * 100) / omg�ng.Matcher[i].SvenskaFolketKryssProcent * 100));
                            viktning[i].Add(Tecken.Tv�.Name, Convert.ToInt32((1 / omg�ng.Matcher[i].OddsetTv� * 100) / omg�ng.Matcher[i].SvenskaFolketTv�Procent * 100));
                            break;

                        case null:
                            for (int j = 0; j < 3; j++)
                            {
                                viktning[i].Add(Tecken.FromValue(j + 1).Name, 1);
                            }
                            break;

                        default: throw new Exception("Nu blev det fel.");
                    }

                }
            }

            return viktning;
        }

        private static List<string> GenereraTipsRad(Dictionary<int, DynamicWeightedRandomizer<string>> viktning, Dictionary<int, int> slumpadeGarderingar)
        {
            var rad = new List<string>();

            for (int i = 0; i < 13; i++)
            {
                if (slumpadeGarderingar.ElementAt(i).Value == 3)
                {
                    rad.Add("1X2");
                }
                else
                {
                    string temp = "";

                    do
                    {
                        temp = viktning[i].NextWithReplacement();

                        if (rad.ElementAtOrDefault(i) == null)
                        {
                            rad.Add(temp);
                        }
                        else if (!rad[i].Contains(temp))
                        {
                            rad[i] += temp;
                        }
                    } while (rad.ElementAt(i).Length != slumpadeGarderingar.ElementAt(i).Value);
                }
            }

            return rad;
        }

        private Dictionary<int, int> SlumpaGarderingarF�rMSystem(int mSystem)
        {
            var randomizer = new Random();
            var rad = new Dictionary<int, int>();
            for (int i = 1; i <= 13; i++)
            {
                rad.Add(i, 1);
            }
            var faktorer = Utilities.Faktorisera(mSystem);

            for (int i = 2; i <= 3; i++)
            {
                if (faktorer.ContainsKey(i))
                {
                    for (int j = 0; j < faktorer.Single(x => x.Key == i).Value; j++)
                    {
                        var ids = rad.Where(x => x.Value == 1).Select(x => x.Key).ToArray();
                        var randomId = ids[randomizer.Next(ids.Length)];
                        rad[randomId] = i;
                    }
                }
            }

            return rad;
        }

        private Omg�ng SlumpaOmg�ng()
        {
            var randomizer = new Random();
            var omg�ngar = H�mtaOmg�ngar();

            return omg�ngar.ElementAt(randomizer.Next(omg�ngar.Count()));
        }

        private Omg�ng[] H�mtaOmg�ngar()
        {
            return Tipsxtra.L�sInTipsxtraOmg�ngar();
        }
    }
}