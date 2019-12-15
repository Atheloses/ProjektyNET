using DAO.Tables;
using DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public abstract class SpecialOperations
    {
        public static int LastId<T>(OracleConnection connection)
        {
            using (OracleCommand command = new OracleCommand("SELECT MAX(ID) FROM " + typeof(T).Name, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                        throw new Exception(DateTime.Now + ": Tabulka '" + typeof(T).Name + "' neobsahuje zadne zaznamy");
                    reader.Read();

                    return Convert.ToInt32(reader[0]);
                }
            }
        }

        /// <summary>
        /// 5.2.1 Vytvoření úkolu s pod úkoly
        /// </summary>
        public static async Task<bool> VytvoreniUkolu(Ukol p_Ukol, string Uzivatele, string Skupiny, OracleConnection p_Connection)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("call InsertHromadnyUkol(:nazev,:popis,:uzivatele,:skupiny,:mandaye,:prioritaID,:termin)", p_Connection))
                {
                    command.Parameters.Add(":nazev", p_Ukol.Nazev);
                    command.Parameters.Add(":popis", p_Ukol.Popis);
                    command.Parameters.Add(":uzivatele", Uzivatele);
                    command.Parameters.Add(":skupiny", Skupiny);
                    command.Parameters.Add(":mandaye", p_Ukol.Mandays);
                    command.Parameters.Add(":priorita", p_Ukol.IDPriorita);
                    command.Parameters.Add(":termin", p_Ukol.Termin);
                    await command.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 5.2.2 Splnění úkolu a jeho podúkolů
        /// </summary>
        public static async Task<bool> SplneniUkolu(int p_Ukol_ID, OracleConnection p_Connection)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("call SplnitUkol(:ukol_id)", p_Connection))
                {
                    command.Parameters.Add(":ukol_id", p_Ukol_ID);
                    await command.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 5.2.3 Hromadné splnění úkolů a podúkolů
        /// </summary>
        public static async Task<bool> HromadneSplneniUkolu(string Ukol_ID_List, string UzivatelUkol_ID_List, string SkupinaUkol_ID_List, OracleConnection p_Connection)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("call SplnitUkolyPodukoly(:ukol_list,:uzivatelukol_list,:skupinaukol_list)", p_Connection))
                {
                    command.Parameters.Add(":ukol_list", Ukol_ID_List);
                    command.Parameters.Add(":uzivatelukol_list", UzivatelUkol_ID_List);
                    command.Parameters.Add(":skupinaukol_list", SkupinaUkol_ID_List);
                    await command.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 5.2.4 Výpočet efektivity skupiny
        /// </summary>
        public static async Task<bool> EfektivitaSkupin(OracleConnection p_Connection)
        {
            using (OracleCommand command = new OracleCommand("", p_Connection)) //call EfektivitaSkupin()
            {
                OracleTransaction transaction = p_Connection.BeginTransaction(IsolationLevel.ReadCommitted);
                command.Transaction = transaction;
                int Mandays, MandaysLocal;
                DateTime monthAgo = DateTime.Now.AddDays(-30);
                try
                {
                    var skupinaDA = new SkupinaDA(p_Connection);
                    var uzivatelSkupinaDA = new UzivatelSkupinaDA(p_Connection);
                    List<Skupina> skupiny = await skupinaDA.SelectAll();
                    foreach (var skupina in skupiny)
                    {
                        Mandays = 0;
                        command.CommandText = "select " + uzivatelSkupinaDA.SQL_COLUMNS + " from uzivatelskupina uzsk where uzsk.casodpojeni is null or uzsk.casodpojeni>:month_ago and uzsk.skupina_id=:skupina_id";
                        command.Parameters.Clear();
                        command.Parameters.Add(":month_ago", monthAgo);
                        command.Parameters.Add(":skupina_id", skupina.IDSkupina);
                        using (var reader = command.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            foreach (var uzsk in uzivatelSkupinaDA.GetDTOList(dataTable))
                            {
                                if (uzsk.CasOdpojeni is null && uzsk.CasPripojeni >= monthAgo)
                                    MandaysLocal = (DateTime.Now - uzsk.CasPripojeni).Days;
                                else if (uzsk.CasOdpojeni != null && uzsk.CasPripojeni >= monthAgo)
                                    MandaysLocal = (uzsk.CasOdpojeni - uzsk.CasPripojeni).Value.Days;
                                else if (uzsk.CasOdpojeni != null)
                                    MandaysLocal = (uzsk.CasOdpojeni - monthAgo).Value.Days;
                                else
                                    MandaysLocal = 30;
                                Mandays += MandaysLocal;
                            }
                        }

                        command.CommandText = "select sum(ukol.mandays) from skupinaukol skuk " +
                            "join ukol on ukol.id = skuk.ukol_id " +
                            "where skuk.skupina_id = :skupina_id and skuk.cassplneni is not null " +
                            "and skuk.cassplneni > :month_ago";
                        command.Parameters.Clear();
                        command.Parameters.Add(":skupina_id", skupina.IDSkupina);
                        command.Parameters.Add(":month_ago", monthAgo);

                        using (var reader = command.ExecuteReader())
                        {
                            reader.Read();

                            int hotoveMandays = reader[0] == DBNull.Value ? 0 : Convert.ToInt32(reader[0]);
                            if (Mandays == 0 || hotoveMandays == 0)
                                skupina.Efektivita = "0";
                            else
                                skupina.Efektivita = (hotoveMandays * 100 / Mandays).ToString();
                            Console.WriteLine("Efektivita skupiny '" + skupina.Nazev + "': " + skupina.Efektivita);
                            await skupinaDA.Update(skupina);
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(DateTime.Now + ": " + ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// 5.2.5 Zobrazit nesplněné úkoly
        /// </summary>
        public static async Task<bool> ZobrazitNesplneneUkoly(OracleConnection p_Connection, int p_Uzivatel_ID)
        {
            try
            {
                Uzivatel u1 = await new UzivatelDA(p_Connection).SelectId(2);
                using (OracleCommand command = new OracleCommand("", p_Connection)
                {
                    CommandText = "select uk.nazev nazev_ukolu,uk.popis popis_ukolu,uzuk.popis popis_podukolu,uzuk.termin termin_podukolu,pr.nazev nazev_priority " +
                    "from uzivatelukol uzuk join ukol uk on uk.id = uzuk.ukol_id " +
                    "join priorita pr on pr.id = uk.priorita_id where uzuk.uzivatel_id = :uzivatel_id " +
                    "and uzuk.cassplneni is null order by uzuk.termin "
                })
                {
                    command.Parameters.Add(":uzivatel_id", p_Uzivatel_ID);

                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("\nNesplnene ukoly uzivatele: '" + u1.Prezdivka + "'");
                        while (reader.Read())
                        {
                            Console.WriteLine("Nazev ukolu: '" + reader["nazev_ukolu"] + "', Popis ukolu: '" + reader["popis_ukolu"] + "', " +
                                "Popis podukolu: '" + reader["popis_podukolu"] + "', Termin podukolu: '" + reader["termin_podukolu"] + "', Priorita: '" + reader["nazev_priority"] + "'");
                        }
                    }
                }

                using (OracleCommand command = new OracleCommand("", p_Connection)
                {
                    CommandText = "select sk.nazev nazev_skupiny,uk.nazev nazev_ukolu,uk.popis popis_ukolu" +
                    ",skuk.popis popis_podukolu,skuk.termin termin_podukolu,pr.nazev nazev_priorita " +
                    "from uzivatelskupina uzsk join skupina sk on sk.id = uzsk.skupina_id " +
                    "join skupinaukol skuk on skuk.skupina_id = sk.id join ukol uk on uk.id = skuk.ukol_id " +
                    "join priorita pr on pr.id = uk.priorita_id where uzsk.uzivatel_id = :uzivatel_id " +
                    "and skuk.cassplneni is null order by skuk.termin"
                })
                {
                    command.Parameters.Add(":uzivatel_id", p_Uzivatel_ID);

                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("\nNesplnene ukoly skupin uzivatele: '" + u1.Prezdivka + "'");
                        while (reader.Read())
                        {
                            Console.WriteLine("Nazev skupiny: '" + reader["nazev_skupiny"] + "', Nazev ukolu: '" + reader["nazev_ukolu"]
                                + "', Popis ukolu: '" + reader["popis_ukolu"] + "', Popis podukolu: '" + reader["popis_podukolu"]
                                + "', Termin podukolu: '" + reader["termin_podukolu"] + "', Priorita: '" + reader["nazev_priorita"] + "'");
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 5.2.6 Zobrazit statistiky skupin a úkolů
        /// </summary>
        public static async Task<bool> StatistikySkupinUkolu(OracleConnection p_Connection)
        {
            try
            {
                OracleCommand command = new OracleCommand("SELECT * FROM (select * from skupina sk where efektivita > 0 " +
                        "order by sk.efektivita desc) WHERE ROWNUM <= 1", p_Connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Nejlepsi skupina: " + reader["nazev"] + ", Efektivita: " + reader["efektivita"]);
                    }
                }

                command = new OracleCommand("SELECT * FROM (select * from skupina sk where efektivita > 0 " +
                        "order by sk.efektivita) WHERE ROWNUM <= 1", p_Connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Nejhorsi skupina: " + reader["nazev"] + ", Efektivita: " + reader["efektivita"]);
                    }
                }

                command = new OracleCommand("SELECT * FROM (select skupina.nazev,count(uzivatel_id) as pocet " +
                        "from uzivatelskupina join skupina on skupina.id = uzivatelskupina.skupina_id " +
                        "group by nazev order by pocet desc) WHERE ROWNUM <= 1", p_Connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Nejpocetnejsi skupina: " + reader["nazev"] + ", Efektivita: " + reader["pocet"]);
                    }
                }

                command = new OracleCommand("SELECT * FROM (select skupina.nazev,count(uzivatel_id) as pocet " +
                        "from uzivatelskupina join skupina on skupina.id = uzivatelskupina.skupina_id " +
                        "group by nazev order by pocet) WHERE ROWNUM <= 1", p_Connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Nejmene pocetna skupina: " + reader["nazev"] + ", Efektivita: " + reader["pocet"]);
                    }
                }

                List<StatistikyUkoluGlobal> statistiky = await new StatistikyUkoluGlobalDA(p_Connection).SelectAll();
                foreach (var stat in statistiky)
                    Console.WriteLine("Nazev statistiky: '" + stat.Kod + " - " + stat.Popis + "', Hodnota: '" + stat.Hodnota + "'");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + ": " + ex.Message);
                return false;
            }
        }
    }
}
