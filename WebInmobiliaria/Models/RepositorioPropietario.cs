using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace WebInmobiliaria.Models
{
  public class RepositorioPropietario
  {
    string ConnectionString = "Server=localhost;Port=3307;User=root;Password=;Database=inmobiliaria;SslMode=none";

    // 🔹 Listado de propietarios
    public List<Propietarios> ObtenerPropietarios()
    {
      List<Propietarios> propietarios = new List<Propietarios>();

      using (MySqlConnection connection = new MySqlConnection(ConnectionString))
      {
        var query = $@"SELECT {nameof(Propietarios.IdPropietario)}, 
                                      {nameof(Propietarios.Documento)},
                                      {nameof(Propietarios.Apellido)}, 
                                      {nameof(Propietarios.Nombre)},
                                      {nameof(Propietarios.Telefono)}, 
                                      {nameof(Propietarios.Email)}
                               FROM propietarios;";

        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
          propietarios.Add(new Propietarios
          {
            IdPropietario = reader.GetInt32(nameof(Propietarios.IdPropietario)),
            Documento = reader.GetInt32(nameof(Propietarios.Documento)),
            Apellido = reader.GetString(nameof(Propietarios.Apellido)),
            Nombre = reader.GetString(nameof(Propietarios.Nombre)),
            Telefono = reader.GetString(nameof(Propietarios.Telefono)),
            Email = reader.GetString(nameof(Propietarios.Email))
          });
        }

        connection.Close();
      }

      return propietarios;
    }

    // 🔹 Obtener un propietario por ID
    public Propietarios? Obtener(int id)
    {
      Propietarios? propietario = null; // ✅ Declaramos bien

      using (MySqlConnection connection = new MySqlConnection(ConnectionString))
      {
        var query = $@"SELECT {nameof(Propietarios.IdPropietario)}, 
                                      {nameof(Propietarios.Documento)},
                                      {nameof(Propietarios.Apellido)}, 
                                      {nameof(Propietarios.Nombre)},
                                      {nameof(Propietarios.Telefono)}, 
                                      {nameof(Propietarios.Email)}
                               FROM propietarios
                               WHERE {nameof(Propietarios.IdPropietario)} = @id;";

        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);
        var reader = command.ExecuteReader();

        if (reader.Read())
        {
          propietario = new Propietarios
          {
            IdPropietario = reader.GetInt32(nameof(Propietarios.IdPropietario)),
            Documento = reader.GetInt32(nameof(Propietarios.Documento)),
            Apellido = reader.GetString(nameof(Propietarios.Apellido)),
            Nombre = reader.GetString(nameof(Propietarios.Nombre)),
            Telefono = reader.GetString(nameof(Propietarios.Telefono)),
            Email = reader.GetString(nameof(Propietarios.Email))
          };
        }

        connection.Close();
      }

      return propietario;
    }

    public int Alta(Propietarios p)
    {
      int res = 0;

      using (MySqlConnection connection = new MySqlConnection(ConnectionString))
      {
        var query = $@"INSERT INTO propietarios
                                      ({nameof(Propietarios.Documento)},
                                      {nameof(Propietarios.Apellido)}, 
                                      {nameof(Propietarios.Nombre)},
                                      {nameof(Propietarios.Telefono)}, 
                                      {nameof(Propietarios.Email)})
                               VALUES(@documento, @apellido, @nombre, @telefono, @email);
                               SELECT LAST_INSERT_ID();";

        connection.Open();
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@documento", p.Documento);
        command.Parameters.AddWithValue("@apellido", p.Apellido);
        command.Parameters.AddWithValue("@nombre", p.Nombre);
        command.Parameters.AddWithValue("@telefono", p.Telefono);
        command.Parameters.AddWithValue("@email", p.Email);
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();
      }

      return res;

    }

    public int Modificar(Propietarios p)
    {
      int res = -1;

      using (MySqlConnection connection = new MySqlConnection(ConnectionString))
      {
        var query = $@"UPDATE propietarios SET
                                      {nameof(Propietarios.Documento)} = @documento,
                                      {nameof(Propietarios.Apellido)} = @apellido,
                                      {nameof(Propietarios.Nombre)} = @nombre,
                                      {nameof(Propietarios.Telefono)} = @telefono,
                                      {nameof(Propietarios.Email)} = @email
                               WHERE {nameof(Propietarios.IdPropietario)} = @id;";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
          command.Parameters.AddWithValue("@documento", p.Documento);
          command.Parameters.AddWithValue("@apellido", p.Apellido);
          command.Parameters.AddWithValue("@nombre", p.Nombre);
          command.Parameters.AddWithValue("@telefono", p.Telefono);
          command.Parameters.AddWithValue("@email", p.Email);
          command.Parameters.AddWithValue("@id", p.IdPropietario);

          connection.Open();
          res = command.ExecuteNonQuery(); // devuelve número de filas afectadas
          connection.Close();
        }
      }

      return res;
    }
    public int Baja(int id)
    {
      int res = -1;

      using (MySqlConnection connection = new MySqlConnection(ConnectionString))
      {
        var query = $@"DELETE FROM propietarios 
                               WHERE {nameof(Propietarios.IdPropietario)} = @id;";

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
