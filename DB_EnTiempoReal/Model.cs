//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DB_EnTiempoReal
//{
//    public class Model
//    {
//        public class Sensor
//        {
//            public int Id { get; set; }
//            public string Nombre { get; set; }
//            public string Tipo { get; set; }
//            public string Ubicación { get; set; }

//            public ICollection<Lectura> Lecturas { get; set; }
//        }

//        public class Lectura
//        {
//            public int Id { get; set; }
//            public int SensorId { get; set; }
//            public DateTime Timestamp { get; set; }
//            public double Valor { get; set; }

//            public Sensor Sensor { get; set; }
//        }

//        public class BaseDatosTiempoRealContext : DbContext
//        {
//            public DbSet<Sensor> Sensores { get; set; }
//            public DbSet<Lectura> Lecturas { get; set; }

//            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//            {
//                optionsBuilder.UseSqlServer("Server=localhost;Database=BaseDatosTiempoReal;Integrated Security=True");
//            }
//        }
//    }
//}
