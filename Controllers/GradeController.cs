using jegyek.Data;
using jegyek.Models;
using Microsoft.AspNetCore.Mvc;

namespace jegyek.Controllers
{
    [ApiController]
    [Route("api/grade")]
    public class GradeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<GradeDto>> GetAll()
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "select * from grades;";

            using var reader = command.ExecuteReader();
            var grades = new List<GradeDto>();
            while (reader.Read())
            {
                grades.Add(new(
                    reader.GetGuid("id"),
                    reader.GetInt32("value"),
                    reader.GetString("description"),
                    reader.GetDateTimeOffset("created_at")
                ));
            }

            return Ok(grades);
        }

        [HttpGet("{id}")]
        public ActionResult<GradeDto> GetById(Guid id)
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "select * from grades where id = @id;";
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return NotFound();
            }

            return Ok(new GradeDto(
                reader.GetGuid("id"),
                reader.GetInt32("value"),
                reader.GetString("description"),
                reader.GetDateTimeOffset("created_at")
            ));
        }

        [HttpPost]
        public ActionResult<GradeDto> Create(CreateGradeDto gradeDto)
        {
            var grade = new Grade
            {
                Value = gradeDto.Value,
                Description = gradeDto.Description
            };

            using var connection = DbConnection.GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "insert into grades (id, value, description) values (@id, @value, @description);";
            command.Parameters.AddWithValue("@id", grade.Id);
            command.Parameters.AddWithValue("@value", grade.Value);
            command.Parameters.AddWithValue("@description", grade.Description);

            command.ExecuteNonQuery();

            return CreatedAtAction(nameof(GetById), new { id = grade.Id }, grade.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, UpdateGradeDto gradeDto)
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "update grades set value = @value, description = @description where id = @id;";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@value", gradeDto.Value);
            command.Parameters.AddWithValue("@description", gradeDto.Description);

            if (command.ExecuteNonQuery() == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            using var connection = DbConnection.GetConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "delete from grades where id = @id;";
            command.Parameters.AddWithValue("@id", id);

            if (command.ExecuteNonQuery() == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}