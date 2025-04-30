using Cursos.Application.Dto;
using Cursos.Infrastructure.Context;
using Cursos.WebApp.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

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
            Guid courseId = await response.Content.ReadFromJsonAsync<Guid>();

            var clientGet = _factory.CreateClient();
            var getResponse = await clientGet.GetAsync($"/Course/{courseId}");
            getResponse.EnsureSuccessStatusCode();
            var course = await getResponse.Content.ReadFromJsonAsync<CourseDto>();
            Assert.Equal(newCourse.CourseTitle, course?.CourseTitle);
        }

        [Fact]
        public async Task GetByIdReturnsCourse()
        {
            var clientInsert = _factory.CreateClient();
            var postResponse = await clientInsert.PostAsJsonAsync("/Course", new CourseDto
            {
                CourseTitle = "Test",
                Description = "Test Desc",
                InstructorName = "John",
                KnowledgePlatform = "Pluralsight",
                Workload = 10
            });
            postResponse.EnsureSuccessStatusCode();
            var courseId = await postResponse.Content.ReadFromJsonAsync<Guid>();

            var clientGet = _factory.CreateClient();
            var getResponse = await clientGet.GetAsync($"/Course/{courseId}");
            getResponse.EnsureSuccessStatusCode();
            var course = await getResponse.Content.ReadFromJsonAsync<CourseDto>();
            Assert.Equal("Test", course.CourseTitle);
        }

        [Fact]
        public async Task UpdateCourseSuccess()
        {
            var clientInsert = _factory.CreateClient();

            var createResponse = await clientInsert.PostAsJsonAsync("/Course", new CourseDto
            {
                CourseTitle = "Original",
                Description = "Original",
                InstructorName = "Jane",
                KnowledgePlatform = "Udemy",
                Workload = 5
            });

            var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

            var updatedCourse = new CourseDto
            {
                CourseTitle = "Updated",
                Description = "Updated Desc",
                InstructorName = "Jane",
                KnowledgePlatform = "Udemy",
                Workload = 8
            };

            var clientUpdate = _factory.CreateClient();
            var putResponse = await clientUpdate.PutAsJsonAsync($"/Course/{id}", updatedCourse);
            putResponse.EnsureSuccessStatusCode();

            var clientGet = _factory.CreateClient();
            var getResponse = await clientGet.GetAsync($"/Course/{id}");
            var updated = await getResponse.Content.ReadFromJsonAsync<CourseDto>();

            Assert.Equal("Updated", updated.CourseTitle);
            Assert.Equal(8, updated.Workload);
        }

        [Fact]
        public async Task DeleteCourseSuccess()
        {
            var clientInsert = _factory.CreateClient();

            var createResponse = await clientInsert.PostAsJsonAsync("/Course", new CourseDto
            {
                CourseTitle = "To Delete",
                Description = "To Delete Desc",
                InstructorName = "Bob",
                KnowledgePlatform = "Coursera",
                Workload = 6
            });

            var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

            var clientDelete = _factory.CreateClient();
            var deleteResponse = await clientDelete.DeleteAsync($"/Course/{id}");
            deleteResponse.EnsureSuccessStatusCode();

            var clientGet = _factory.CreateClient();
            var getResponse = await clientGet.GetAsync($"/Course/{id}");
            Assert.Equal(getResponse.StatusCode, HttpStatusCode.NoContent);
        }

        [Theory]
        [InlineData(null, null, null, null, null, HttpStatusCode.BadRequest)]
        [InlineData("", "", "", "", 20, HttpStatusCode.BadRequest)]
        [InlineData("TDD", "Antonio Leonardo", "Curso alusivo a testes de unidade automatizados", "Coursera", 30, HttpStatusCode.OK)]
        [InlineData("Testes Integrados com C#", "Antonio Leonardo", "Testando ASP.NET Core Web APIs utilizando xUnit", "Pluralsight", 20, HttpStatusCode.InternalServerError)]
        public async Task AddCoursesTuplAndCheckRequestResult(string courseTitle, string instructorName, string description, string knowledggePlataform, int? workload, HttpStatusCode httpStatusCode)
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
            Assert.Equal(response.StatusCode, httpStatusCode);
        }
    }
}