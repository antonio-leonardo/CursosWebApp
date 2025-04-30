using Cursos.Application.Dto;
using Cursos.Domain.Domain.Models;
using Cursos.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Cursos.WebApp.Extensions;

namespace Cursos.WebApp.IntegrationTest
{
    public class CourseControllerTests : BaseTest, IClassFixture<TestWebApplicationFactory>
    {
        public CourseControllerTests(TestWebApplicationFactory factory) : base(factory)
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.CreateInitialDevelopmentData();
            }
        }

        [Fact]
        public async Task GetAllCourses()
        {
            HttpClient _client = _factory.CreateClient();
            var response = await _client.GetAsync("/Course");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<List<CourseDto>>();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task AddCourseWithValidData()
        {
            HttpClient _client = _factory.CreateClient();
            var newCourse = new CourseDto
            {
                CourseTitle = "BDD",
                InstructorName = "Antonio Leonardo",
                Description = "Curso sobre BDD utiilizando SpecFlow",
                KnowledgePlatform = "Alura",
                Workload = 12
            };

            var response = await _client.PostAsJsonAsync("/Course", newCourse);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Course>();
            Assert.Equal(newCourse.CourseTitle, result?.Title);
        }


        [Theory]
        [InlineData(null, null, null, null, null, HttpStatusCode.BadRequest)]
        [InlineData("", "", "", "", 20, HttpStatusCode.BadRequest)]
        [InlineData("TDD", "Antonio Leonardo", "Curso alusivo a testes de unidade automatizados", "Coursera", 30, HttpStatusCode.OK)]
        [InlineData("Testes Integrados com C#", "Antonio Leonardo", "Testando ASP.NET Core Web APIs utilizando xUnit", "Pluralsight", 20, HttpStatusCode.InternalServerError)]
        public async Task AddCoursesTupleAndCompareIfExits(string courseTitle, string instructorName, string description, string knowledggePlataform, int? workload, HttpStatusCode httpStatusCode)
        {
            CourseDto newCourse = null;
            if(!string.IsNullOrWhiteSpace(courseTitle) && !string.IsNullOrWhiteSpace(instructorName) && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(knowledggePlataform) && workload != null)
            {
                newCourse = new CourseDto
                {
                    CourseTitle = courseTitle,
                    InstructorName = instructorName,
                    Description = description,
                    KnowledgePlatform = knowledggePlataform,
                    Workload = workload.Value
                };
            }

            HttpClient _client = _factory.CreateClient();
            var response = await _client.PostAsJsonAsync("/Course", newCourse);
            string responseMessage = await response.Content.ReadAsStringAsync();
            var jsonData = (JObject)JsonConvert.DeserializeObject(responseMessage);
            Assert.Equal(response.StatusCode, httpStatusCode);
            if(response.StatusCode == HttpStatusCode.InternalServerError)
            {
                Assert.Equal("Não possível fazer inserção do curso devido ao seguinte erro: O curso já está cadastrado na base de dados", jsonData["detail"]);
            }
        }
    }
}