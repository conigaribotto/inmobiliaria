using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace WebInmobiliaria.Models
{
    public class RepositorioInquilino
    {
        string ConnectionString = "Server=localhost;Port=3307;User=root;Password=;Database=inmobiliaria;SslMode=none";

        // 🔹 Listar todos los inquilinos
        public List<Inquilinos> ObtenerInquilinos()
        {
            List<Inquilinos> inquilinos = new List<Inquilinos>();

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                var query = @"SELECT Id_Inquilinos, Documento, Apellido, Nombre, Telefono, Email FROM inquilinos;";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    inquilinos.Add(new Inquilinos
                    {
                        Id_Inquilinos = reader.GetInt32("Id_Inquilinos"),
                        Documento = reader.GetInt32("Documento"),
                        Apellido = reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email")
                    });
                }

                connection.Close();
            }

            return inquilinos;
        }

        // 🔹 Obtener un inquilino por ID
        public Inquilinos? Obtener(int id)
        {
            Inquilinos? inquilino = null;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                var query = @"SELECT Id_Inquilinos, Documento, Apellido, Nombre, Telefono, Email 
                              FROM inquilinos
                              WHERE Id_Inquilinos = @id;";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    inquilino = new Inquilinos
                    {
                        Id_Inquilinos = reader.GetInt32("Id_Inquilinos"),
                        Documento = reader.GetInt32("Documento"),
                        Apellido = reader.GetString("Apellido"),
                        Nombre = reader.GetString("Nombre"),
                        Telefono = reader.GetString("Telefono"),
                        Email = reader.GetString("Email")
                    };
                }

                connection.Close();
            }

            return inquilino;
        }

        // 🔹 Alta de inquilino
        public int Alta(Inquilinos i)
        {
            int res = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                var query = @"INSERT INTO inquilinos (Documento, Apellido, Nombre, Telefono, Email)
                              VALUES(@documento, @apellido, @nombre, @telefono, @email);
                              SELECT LAST_INSERT_ID();";

                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@documento", i.Documento);
                command.Parameters.AddWithValue("@apellido", i.Apellido);
                command.Parameters.AddWithValue("@nombre", i.Nombre);
                command.Parameters.AddWithValue("@telefono", i.Telefono);
                command.Parameters.AddWithValue("@email", i.Email);
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }

            return res;
        }

        // 🔹 Modificar inquilino
        public int Modificar(Inquilinos i)
        {
            int res = -1;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                var query = @"UPDATE inquilinos SET
                                Documento = @documento,
                                Apellido = @apellido,
                                Nombre = @nombre,
                                Telefono = @telefono,
                                Email = @email
                              WHERE Id_Inquilinos = @id;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@documento", i.Documento);
                    command.Parameters.AddWithValue("@apellido", i.Apellido);
                    command.Parameters.AddWithValue("@nombre", i.Nombre);
                    command.Parameters.AddWithValue("@telefono", i.Telefono);
                    command.Parameters.AddWithValue("@email", i.Email);
                    command.Parameters.AddWithValue("@id", i.Id_Inquilinos);

                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }

        // 🔹 Baja de inquilino
        public int Baja(int id)
        {
            int res = -1;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                var query = @"DELETE FROM inquilinos WHERE Id_Inquilinos = @id;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return res;
        }
    }
}
