using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis_Talep
{
    class DbHelper
    {
        public static List<TalepC> GetUrunList()
        {
            var talepList = new List<TalepC>();
            var conn = new SQLiteConnection(Settings.ConnectionString);
            conn.Open();
            var sql = "SELECT * FROM ServisTalep order by GirisTar";
            var cmd = new SQLiteCommand(sql, conn);
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var talep = new TalepC
                {
                    ServisTalepId = Convert.ToInt32(dr["ServisTalepId"]),
                    Ad = dr["Ad"].ToString(),
                    SoyAd = dr["Soyad"].ToString(),
                    GirisTar = Convert.ToDateTime(dr["GirisTar"]),
                    Plaka = dr["Plaka"].ToString(),
                    Marka = dr["Marka"].ToString(),
                    Model = dr["Model"].ToString(),
                    Talep = dr["Talep"].ToString(),
                    Acıklama = dr["Acıklama"].ToString(),

                };

                talepList.Add(talep);
            }

            dr.Close();
            conn.Close();

            return talepList;
        }

        public static void TalepGuncelle(TalepC talep)
        {
            var conn = new SQLiteConnection(Settings.ConnectionString);
            conn.Open();

            var sql = $"UPDATE ServisTalep SET ServisTalepId = @ServisTalepId, Ad = @Ad, SoyAd = @SoyAd,GirisTar=@GirisTar,Plaka=@Plaka,Marka=@Marka,Model=@Model,Talep=@Talep, Acıklama = @Acıklama WHERE ServisTalepId = @ServisTalepId";
            var cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ServisTalepId", talep.ServisTalepId);
            cmd.Parameters.AddWithValue("@Ad", talep.Ad);
            cmd.Parameters.AddWithValue("@SoyAd", talep.SoyAd);
            cmd.Parameters.AddWithValue("@GirisTar", talep.GirisTar);
            cmd.Parameters.AddWithValue("@Plaka", talep.Plaka);
            cmd.Parameters.AddWithValue("@Marka", talep.Marka);
            cmd.Parameters.AddWithValue("@Model", talep.Model);
            cmd.Parameters.AddWithValue("@Talep", talep.Talep);
            cmd.Parameters.AddWithValue("@Acıklama", talep.Acıklama);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void TalepEkle(TalepC talep)
        {
            var conn = new SQLiteConnection(Settings.ConnectionString);
            conn.Open();
            var sql = $"INSERT INTO ServisTalep (Ad,Soyad,GirisTar,Plaka,Marka,Model,Talep,Acıklama) VALUES(@Ad,@SoyAd,@GirisTar,@Plaka,@Marka,@Model,@Talep,@Acıklama)";

            var cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Ad", talep.Ad);
            cmd.Parameters.AddWithValue("@SoyAd", talep.SoyAd);
            cmd.Parameters.AddWithValue("@GirisTar", talep.GirisTar);
            cmd.Parameters.AddWithValue("@Plaka", talep.Plaka);
            cmd.Parameters.AddWithValue("@Marka", talep.Marka);
            cmd.Parameters.AddWithValue("@Model", talep.Model);
            cmd.Parameters.AddWithValue("@Talep", talep.Talep);
            cmd.Parameters.AddWithValue("@Acıklama", talep.Acıklama);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void TalepSil(TalepC talep)
        {
            var conn = new SQLiteConnection(Settings.ConnectionString);
            conn.Open();
            var sql = $"delete from ServisTalep WHERE ServisTalepId = {talep.ServisTalepId}";
            var cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
