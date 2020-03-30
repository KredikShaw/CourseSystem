namespace CourseSystem.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetCategories<T>();

        int GetCategoryId(string name);
    }
}
