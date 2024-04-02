using Microsoft.Data.SqlClient;
using System;
using System.Transactions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DB_EnTiempoReal
{
    class Program
    {

        static void Main(string[] args)
        {

            // Monitor de sensores anormales
            SensorAnormalMonitor sensorAnormalMonitor = new SensorAnormalMonitor();

            sensorAnormalMonitor.SensorAnormal += (sender, e) =>
            {
                Console.WriteLine("¡Sensor anómalo detectado! Sensor ID: {0}, Valor: {1}", e.SensorId, e.Valor);
                // Enviar notificación, registrar en un log, etc.
            };

            while (true)
            {
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Menú principal:");
                Console.WriteLine("1. Insertar lectura de sensor");
                Console.WriteLine("2. Leer lecturas recientes");
                Console.WriteLine("3. Salir");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        InsertarLecturaSensor(sensorAnormalMonitor);
                        break;
                    case 2:
                        LeerLecturasRecientes();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
        }

        static void InsertarLecturaSensor(SensorAnormalMonitor sensorAnormalMonitor)
        {
            string connectionString = "Data Source=LAPTOP-0I5E9G7D;Initial Catalog=BaseDatosTiempoRea;Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Lectura (sensor_id, timestamp, valor) VALUES (@sensorId, @timestamp, @valor)", connection))
                {
                    int sensorId = PedirSensorId();
                    DateTime timestamp = DateTime.Now;
                    decimal valor = PedirValorSensor();

                    command.Parameters.AddWithValue("@sensorId", sensorId);
                    command.Parameters.AddWithValue("@timestamp", timestamp);
                    command.Parameters.AddWithValue("@valor", valor);

                    // Bloquear la tabla Lectura antes de insertar
                    command.CommandText = "BEGIN TRANSACTION; " + command.CommandText + "; COMMIT TRANSACTION";

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Lectura insertada correctamente.");

                        // Detectar sensor anómalo
                        sensorAnormalMonitor.DetectarSensorAnormal(sensorId, valor);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Message.Contains("Cannot insert the value NULL into column"))
                        {
                            Console.WriteLine("Error: No se puede insertar NULL en la columna 'id'.");
                        }
                        else
                        {
                            Console.WriteLine("Error al insertar la lectura: " + ex.Message);
                        }
                    }
                }
            }
        }

        static void LeerLecturasRecientes()
        {
            string connectionString = "Data Source=LAPTOP-0I5E9G7D;Initial Catalog=BaseDatosTiempoRea;Integrated Security=True; TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Lectura WHERE timestamp >= DATEADD(minute, -5, GETDATE())", connection)) // Últimos 5 minutos
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int sensorId = reader.GetInt32(1); // Verificar si es null
                        DateTime timestamp = reader.GetDateTime(2);
                        decimal valor = reader.GetDecimal(3);
                        Console.WriteLine("ID: {0}, Sensor ID: {1}, Timestamp: {2}, Valor: {3}", id, sensorId, timestamp, valor);
                    }
                    reader.Close();
                }
            }
        }

        static int PedirSensorId()
        {
            Console.Write("Ingrese el ID del sensor: ");
            int sensorId = int.Parse(Console.ReadLine());
            return sensorId;
        }

        static decimal PedirValorSensor()
        {
            Console.Write("Ingrese el valor del sensor: ");
            decimal valor = decimal.Parse(Console.ReadLine());

            return valor;
        }

    }


    public class SensorAnormalEventArgs : EventArgs
    {
        public int SensorId { get; private set; }
        public decimal Valor { get; private set; }

        public SensorAnormalEventArgs(int sensorId, decimal valor)
        {
            SensorId = sensorId;
            Valor = valor;
        }
    }

    public delegate void SensorAnormalEventHandler(object sender, SensorAnormalEventArgs e);

    // Crear una clase para gestionar eventos de sensores anormales
    public class SensorAnormalMonitor
    {
        private event SensorAnormalEventHandler OnSensorAnormal;

        public event SensorAnormalEventHandler SensorAnormal
        {
            add { OnSensorAnormal += value; }
            remove { OnSensorAnormal -= value; }
        }

        public void DetectarSensorAnormal(int sensorId, decimal valor)
        {
            if (valor > 100) // Valor umbral arbitrario
            {
                OnSensorAnormal?.Invoke(this, new SensorAnormalEventArgs(sensorId, valor));
            }
        }
    }

}
