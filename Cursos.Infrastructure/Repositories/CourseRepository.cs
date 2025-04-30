using Cursos.CrossCutting;
using Cursos.Domain.Domain.Models;
using Cursos.Domain.Interfaces.Repositories;
using Cursos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Cursos.Infrastructure.Repositories
{
    public class CourseRepository : LoggingBase, ICourseRepository
    {
        private readonly DataContext _context;
        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public void Delete(Guid courseId)
        {
            Course courseToDelete = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            _context.Courses.Remove(courseToDelete);
        }

        public Course Get(string courseTitle, string knowledgePlataformName, string instructorName)
        {
            return _context.Courses
                .Include(c => c.KnowledgePlatform)
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.Instructor.CompleteName.Replace(" ", "").ToLower() == instructorName.Replace(" ", "").ToLower()
                    && c.KnowledgePlatform.Name.Replace(" ", "").ToLower() == knowledgePlataformName.Replace(" ", "").ToLower()
                    && c.Title.Replace(" ", "").ToLower() == courseTitle.Replace(" ", "").ToLower());
        }

        public Course Get(Guid courseId)
        {
            return _context.Courses
                .Include(c => c.KnowledgePlatform)
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.Id == courseId);
        }

        public List<Course> GetAll()
        {
            return _context.Courses
                .Include(c => c.KnowledgePlatform)
                .Include(c => c.Instructor).ToList();
        }

        public void Register(Course course)
        {
            Log.Info($"Persistencia de curso, Id: {course.Id}");
            _context.Courses.Add(course);
        }

        public void Update(Course course)
        {
            Course courseToUpdate = this._context.Courses.FirstOrDefault(c => c.Id == course.Id);
            courseToUpdate.KnowledgePlatformId = course.KnowledgePlatformId;
            courseToUpdate.InstructorId = course.InstructorId;
            courseToUpdate.Workload = course.Workload;
            courseToUpdate.Description = course.Description;
            courseToUpdate.Notes = course.Notes;
            courseToUpdate.Title = course.Title;
            _context.Courses.Update(courseToUpdate);
        }
    }
}