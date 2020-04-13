namespace CourseSystem.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CourseSystem.Web.ViewModels.Categories;

    public class EditCourseWithCategoryViewModel<T>
    {
        public T Course { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
