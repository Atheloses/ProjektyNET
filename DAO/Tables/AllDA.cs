using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Tables
{
    public class AllDA
    {
        private OracleConnection Connection { get; }

        private KomentarDA _komentarDA;
        public KomentarDA KomentarDA => _komentarDA;

        private PrioritaDA _prioritaDA;
        public PrioritaDA PrioritaDA => _prioritaDA;

        private SkupinaDA _skupinaDA;
        public SkupinaDA SkupinaDA => _skupinaDA;

        private SkupinaUkolDA _skupinaUkolDA;
        public SkupinaUkolDA SkupinaUkolDA => _skupinaUkolDA;

        private StatistikyUkoluGlobalDA _statistikyUkoluGlobalDA;
        public StatistikyUkoluGlobalDA StatistikyUkoluGlobalDA => _statistikyUkoluGlobalDA;

        private UkolDA _ukolDA;
        public UkolDA UkolDA => _ukolDA;

        private UzivatelDA _uzivatelDA;
        public UzivatelDA UzivatelDA => _uzivatelDA;

        private UzivatelSkupinaDA _uzivatelSkupinaDA;
        public UzivatelSkupinaDA UzivatelSkupinaDA => _uzivatelSkupinaDA;

        private UzivatelUkolDA _uzivatelUkolDA;
        public UzivatelUkolDA UzivatelUkolDA => _uzivatelUkolDA;

        public AllDA(OracleConnection p_Connection)
        {
            Connection = p_Connection;
            _komentarDA = new KomentarDA(Connection);
            _prioritaDA = new PrioritaDA(Connection);
            _skupinaDA = new SkupinaDA(Connection);
            _skupinaUkolDA = new SkupinaUkolDA(Connection);
            _statistikyUkoluGlobalDA = new StatistikyUkoluGlobalDA(Connection);
            _ukolDA = new UkolDA(Connection);
            _uzivatelDA = new UzivatelDA(Connection);
            _uzivatelSkupinaDA = new UzivatelSkupinaDA(Connection);
            _uzivatelUkolDA = new UzivatelUkolDA(Connection);
        }
    }
}
