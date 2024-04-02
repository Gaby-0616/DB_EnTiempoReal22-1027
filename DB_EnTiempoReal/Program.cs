using Microsoft.Data.SqlClient;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DB_EnTiempoReal
{
    class Program
    {
        static void Main(string[] args)
        {
                string connectionString = "Data Source=DESKTOP-D2MMQQ1\\SQLEXPRESS;Initial Catalog=BaseDatosTiempoRea;Integrated Security=True; TrustServerCertificate=True";
                SqlConnection connection = new SqlConnection(connectionString);

                // Insertar lectura de sensor (asegúrese de reemplazar con valores reales)
                Console.WriteLine("Insertar lectura de sensor");
                int sensorId = 1; // ID del sensor real
                DateTime timestamp = DateTime.Now;
                decimal valor = 10.50m; // Valor real del sensor

                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Lectura (sensor_id, timestamp, valor) VALUES (@sensorId, @timestamp, @valor)", connection);
                command.Parameters.AddWithValue("@sensorId", sensorId);
                command.Parameters.AddWithValue("@timestamp", timestamp);
                command.Parameters.AddWithValue("@valor", valor);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Lectura insertada correctamente.");
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
                finally
                {
                    connection.Close();
                }

                // Leer lecturas recientes (filtro ajustable)
                Console.WriteLine("Leer lecturas recientes");
                connection.Open();
                command = new SqlCommand("SELECT * FROM Lectura WHERE timestamp >= DATEADD(minute, -5, GETDATE())", connection); // Últimos 5 minutos
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    sensorId = reader.GetInt32(1); // Verificar si es null
                    timestamp = reader.GetDateTime(2);
                    valor = reader.GetDecimal(3);
                    Console.WriteLine("ID: {0}, Sensor ID: {1}, Timestamp: {2}, Valor: {3}", id, sensorId, timestamp, valor);
                }
                reader.Close();
                connection.Close();

                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
}
