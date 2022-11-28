using SchoolMangment.Database;

namespace SchoolMangment.Repostories
{
    public class CommonRepository<T> where T : class, new()
    {
        private readonly SqlSugarContext context;

        public CommonRepository(SqlSugarContext context)
        {
            this.context = context;
        }

        public Task<int> AddT(T entity)
        {
            return context.db.Insertable<T>(entity).ExecuteCommandAsync();
        }
        public Task<List<T>> GetPagingListT(int page, int pageSize, ref int totalCount)
        {
            return context.db.Queryable<T>().ToPageListAsync(page, pageSize, totalCount);
        }
    }
}
