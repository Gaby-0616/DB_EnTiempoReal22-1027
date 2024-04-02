//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static DB_EnTiempoReal.Model;

//namespace DB_EnTiempoReal
//{
//    public class SensorService
//    {
//        public void AgregarLectura(int sensorId, double valor)
//        {
//            using (var context = new BaseDatosTiempoRealContext())
//            {
//                var lectura = new Lectura
//                {
//                    SensorId = sensorId,
//                    Timestamp = DateTime.UtcNow,
//                    Valor = valor
//                };

//                context.Lecturas.Add(lectura);
//                context.SaveChanges();

//                var evento = new SensorLecturaEvent
//                {
//                    SensorId = sensorId,
//                    Timestamp = lectura.Timestamp,
//                    Valor = lectura.Valor
//                };

//                var hub = new EventosHub();
//                hub.EnviarLectura(evento);
//            }
//        }
//    }
//}
