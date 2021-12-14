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

        private void SlumpaEnOmgång_Click(object sender, EventArgs e)
        {
            var slumpadOmgång = SlumpaOmgång();
            List<int> dataX = new List<int>();
            List<int> dataY = new List<int>();
            var oddsfavoritskap = RäknaUtOddsfavoritskapOchSpelvärde(slumpadOmgång, Värde.Oddsfavoritskap);
            var spelvärde = RäknaUtOddsfavoritskapOchSpelvärde(slumpadOmgång, Värde.Spelvärde);
            var komboslump = RäknaUtOddsfavoritskapOchSpelvärde(slumpadOmgång, Värde.Komboslump, oddsfavoritskap, spelvärde);
            var slump = RäknaUtOddsfavoritskapOchSpelvärde(slumpadOmgång);

            foreach (var mSystem in MSystem.List.OrderByDescending(m => m.Value).Take(30))
            {
                var oddsfavoritskapStats = new Dictionary<int, int>();
                var spelvärdeStats = new Dictionary<int, int>();
                var komboslumpStats = new Dictionary<int, int>();
                var slumpStats = new Dictionary<int, int>();
                var loops = 0;
                for (int i = 0; i <= 13; i++)
                {
                    oddsfavoritskapStats.Add(i, 0);
                    spelvärdeStats.Add(i, 0);
                    komboslumpStats.Add(i, 0);
                    slumpStats.Add(i, 0);
                }
                var slumpadeGarderingar = SlumpaGarderingarFörMSystem(mSystem.Value);

                do
                {
                    loops++;

                    var oddsfavoritskapRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                    var spelvärdeRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                    var komboslumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                    var slumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);

                    oddsfavoritskapStats[RättaRad(slumpadOmgång, oddsfavoritskapRad)]++;
                    spelvärdeStats[RättaRad(slumpadOmgång, spelvärdeRad)]++;
                    komboslumpStats[RättaRad(slumpadOmgång, komboslumpRad)]++;
                    slumpStats[RättaRad(slumpadOmgång, slumpRad)]++;
                } while (oddsfavoritskapStats[13] < 1 && spelvärdeStats[13] < 1 && komboslumpStats[13] < 1 && slumpStats[13] < 1);

                dataX.Add(mSystem.Value);
                dataY.Add(loops);

                //textBox.Text += $"{mSystem.Name}:\t";
                //textBox.Text += oddsfavoritskapStats[13] > 0 ? "Oddsfavorit" : "";
                //textBox.Text += spelvärdeStats[13] > 0 ? "Spelvärde" : "";
                //textBox.Text += komboslumpStats[13] > 0 ? "Komboslump" : "";
                //textBox.Text += slumpStats[13] > 0 ? "Slump" : "";
                //textBox.Text += Environment.NewLine;
            }

            plotterOmgångar.Plot.AddScatter(dataX.Select(x => (double)x).ToArray(), dataY.Select(x => (double)x).ToArray());
            plotterOmgångar.Refresh();
        }

        private void KörAllaOmgångar_Click(object sender, EventArgs e)
        {
            var omgångar = HämtaOmgångar();
            var dataX = new List<int>();
            var dataY = new List<int>();
            var totalStats = new Dictionary<int, Dictionary<int, int>>();

            foreach (var mSystem in MSystem.List.OrderByDescending(m => m.Value))
            {
                totalStats.Add(mSystem.Value, new Dictionary<int, int>() { { (int)Värde.Oddsfavoritskap, 0 }, { (int)Värde.Spelvärde, 0 }, { (int)Värde.Komboslump, 0 }, { (int)Värde.Slump, 0 } });
            }

            foreach (var omgång in omgångar)
            {
                var oddsfavoritskap = RäknaUtOddsfavoritskapOchSpelvärde(omgång, Värde.Oddsfavoritskap);
                var spelvärde = RäknaUtOddsfavoritskapOchSpelvärde(omgång, Värde.Spelvärde);
                var komboslump = RäknaUtOddsfavoritskapOchSpelvärde(omgång, Värde.Komboslump, oddsfavoritskap, spelvärde);
                var slump = RäknaUtOddsfavoritskapOchSpelvärde(omgång);
                dataX = new List<int>();
                dataY = new List<int>();

                foreach (var mSystem in MSystem.List.OrderByDescending(m => m.Value))
                {
                    var oddsfavoritskapStats = new Dictionary<int, int>();
                    var spelvärdeStats = new Dictionary<int, int>();
                    var komboslumpStats = new Dictionary<int, int>();
                    var slumpStats = new Dictionary<int, int>();
                    var loops = 0;
                    for (int i = 0; i <= 13; i++)
                    {
                        oddsfavoritskapStats.Add(i, 0);
                        spelvärdeStats.Add(i, 0);
                        komboslumpStats.Add(i, 0);
                        slumpStats.Add(i, 0);
                    }
                    var slumpadeGarderingar = SlumpaGarderingarFörMSystem(mSystem.Value);

                    do
                    {
                        loops++;

                        var oddsfavoritskapRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                        var spelvärdeRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                        var komboslumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);
                        var slumpRad = GenereraTipsRad(oddsfavoritskap, slumpadeGarderingar);

                        oddsfavoritskapStats[RättaRad(omgång, oddsfavoritskapRad)]++;
                        spelvärdeStats[RättaRad(omgång, spelvärdeRad)]++;
                        komboslumpStats[RättaRad(omgång, komboslumpRad)]++;
                        slumpStats[RättaRad(omgång, slumpRad)]++;
                    } while (oddsfavoritskapStats[13] < 1 && spelvärdeStats[13] < 1 && komboslumpStats[13] < 1 && slumpStats[13] < 1);

                    dataX.Add(mSystem.Value);
                    dataY.Add(loops);

                    var row = new ListViewItem($"{mSystem.Name}");

                    if (oddsfavoritskapStats[13] > 0)
                    {
                        row.SubItems.Add("Oddsfavorit");
                        totalStats[mSystem.Value][(int)Värde.Oddsfavoritskap]++;
                    }
                    else if (spelvärdeStats[13] > 0)
                    {
                        row.SubItems.Add("Spelvärde");
                        totalStats[mSystem.Value][(int)Värde.Spelvärde]++;
                    }
                    else if (komboslumpStats[13] > 0)
                    {
                        row.SubItems.Add("Komboslump");
                        totalStats[mSystem.Value][(int)Värde.Komboslump]++;
                    }
                    else if (slumpStats[13] > 0)
                    {
                        row.SubItems.Add("Slump");
                        totalStats[mSystem.Value][(int)Värde.Slump]++;
                    }
                    row.SubItems.Add(loops.ToString());

                    listView.Items.Add(row);
                }

                plotterOmgångar.Plot.AddScatter(dataX.Select(x => (double)x).ToArray(), dataY.Select(x => (double)x).ToArray());
                plotterOmgångar.Refresh();

                var listOfItems = listView.Items.Cast<ListViewItem>();
                listView.Columns[1].Text = $"Metod ({listOfItems.Count(i => i.SubItems.Cast<ListViewSubItem>().ToArray()[1].Text == "Oddsfavorit")})";
            }

            foreach (var värde in Enum.GetValues(typeof(Värde)).Cast<Värde>())
            {
                var systemX = totalStats.Keys.Select(s => (double)s).ToArray();
                var systemY = totalStats.Values.SelectMany(s => s).Where(s => s.Key == (int)värde).Select(s => (double)s.Value).ToArray();
                plotterSystem.Plot.AddScatter(systemX, systemY, label: värde.ToString());
                plotterSystem.Plot.Legend();
                plotterSystem.Refresh();
            }
        }

        private int RättaRad(Omgång slumpadOmgång, List<string> rad)
        {
            int count = 0;

            for (int i = 0; i < 13; i++)
            {
                if (rad.ElementAt(i).Contains(slumpadOmgång.RättRad[i]))
                {
                    count++;
                }
            }

            return count;
        }

        private static Dictionary<int, DynamicWeightedRandomizer<string>> RäknaUtOddsfavoritskapOchSpelvärde(Omgång omgång, Värde? värde = null, Dictionary<int, DynamicWeightedRandomizer<string>> oddsfavoritskap = null, Dictionary<int, DynamicWeightedRandomizer<string>> spelvärde = null)
        {
            var viktning = new Dictionary<int, DynamicWeightedRandomizer<string>>();

            if (värde == Värde.Komboslump)
            {
                Random random = new Random();

                for (int i = 0; i < 13; i++)
                {
                    viktning.Add(i, random.Next() % 2 == 0 ? oddsfavoritskap[i] : spelvärde[i]);
                }
            }
            else
            {
                for (int i = 0; i < omgång.Matcher.Length; i++)
                {
                    viktning.Add(i, new DynamicWeightedRandomizer<string>());

                    if (omgång.Matcher[i].OddsetEtt == 0 | omgång.Matcher[i].OddsetKryss == 0 | omgång.Matcher[i].OddsetTvå == 0)
                    {
                        värde = null;
                    }

                    switch (värde)
                    {
                        case Värde.Oddsfavoritskap:
                            viktning[i].Add(Tecken.Ett.Name, Convert.ToInt32(1 / omgång.Matcher[i].OddsetEtt * 100));
                            viktning[i].Add(Tecken.Kryss.Name, Convert.ToInt32(1 / omgång.Matcher[i].OddsetKryss * 100));
                            viktning[i].Add(Tecken.Två.Name, Convert.ToInt32(1 / omgång.Matcher[i].OddsetTvå * 100));
                            break;

                        case Värde.Spelvärde:
                            viktning[i].Add(Tecken.Ett.Name, Convert.ToInt32((1 / omgång.Matcher[i].OddsetEtt * 100) / omgång.Matcher[i].SvenskaFolketEttProcent * 100));
                            viktning[i].Add(Tecken.Kryss.Name, Convert.ToInt32((1 / omgång.Matcher[i].OddsetKryss * 100) / omgång.Matcher[i].SvenskaFolketKryssProcent * 100));
                            viktning[i].Add(Tecken.Två.Name, Convert.ToInt32((1 / omgång.Matcher[i].OddsetTvå * 100) / omgång.Matcher[i].SvenskaFolketTvåProcent * 100));
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

        private Dictionary<int, int> SlumpaGarderingarFörMSystem(int mSystem)
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

        private Omgång SlumpaOmgång()
        {
            var randomizer = new Random();
            var omgångar = HämtaOmgångar();

            return omgångar.ElementAt(randomizer.Next(omgångar.Count()));
        }

        private Omgång[] HämtaOmgångar()
        {
            return Tipsxtra.LäsInTipsxtraOmgångar();
        }
    }
}