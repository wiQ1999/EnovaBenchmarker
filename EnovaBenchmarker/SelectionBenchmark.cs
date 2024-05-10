using Soneta.Start;
using Soneta.Business.App;
using Soneta.Towary;
using BenchmarkDotNet.Attributes;
using Soneta.Business;
using BenchmarkDotNet.Mathematics;

namespace EnovaBenchmarker
{
    [SimpleJob()]
    [MemoryDiagnoser]
    [RankColumn(NumeralSystem.Arabic)]
    [MinColumn, MaxColumn]
    public class SelectionBenchmark
    {
        private Login _login = default!;

        public SelectionBenchmark()
        {
            var loader = new Loader();

            loader.Load();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _login = BusApplication.Instance["TestLocal01"]
                .Login(false, "Administrator", "");
        }

        [Benchmark]
        public void FindMinNetPrice_linqCast()
        {
            using (var session = _login.CreateSession(false, false, nameof(FindMinNetPrice_linqCast)))
            {
                var priceDef = session
                    .GetTowary().DefinicjeCen
                    .Cast<DefinicjaCeny>()
                    .First();

                var price = session
                    .GetTowary().Ceny
                    .Cast<Cena>()
                    .Where(c => c.Definicja == priceDef)
                    .Aggregate((c1, c2) => c1.Netto > c2.Netto ? c1 : c2);
            }
        }

        [Benchmark]
        public void FindMinNetPrice_rowCondition()
        {
            using (var session = _login.CreateSession(false, false, nameof(FindMinNetPrice_rowCondition)))
            {
                var priceDef = session
                    .GetTowary().DefinicjeCen.PrimaryKey
                    .GetFirst();

                var prices = session
                    .GetTowary().Ceny.PrimaryKey
                        [new FieldCondition.Equal(nameof(Cena.Definicja), priceDef)]
                    .ToArray<Cena>();

                Array.Sort(prices, (c1, c2) => c2.Netto.Value.CompareTo(c1.Netto.Value));

                var price = prices.First();
            }
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _login?.Dispose();
        }
    }
}
