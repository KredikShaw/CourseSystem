namespace CourseSystem.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using CourseSystem.Web.ViewModels.Categories;

    public class EditCourseWithCategoryViewModel<T>
    {
        public T Course { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
