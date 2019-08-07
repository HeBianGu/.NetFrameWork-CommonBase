using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.DataBase
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>
    {
        //定义数据访问上下文对象
        protected readonly DbContext _dbContext;

        public DbContext DataContext => _dbContext;

        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public async Task<TEntity> GetByIDAsync(TPrimaryKey id)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Add(entity);

            if (autoSave)
                return await SaveAsync();

            return -1;
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns> 
        public int Insert(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Add(entity);

            if (autoSave)
                return _dbContext.SaveChanges();

            return -1;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public async Task<int> UpdateAsync(TEntity entity, bool autoSave = true)
        {
            var obj = GetByIDAsync(entity.ID).Result;

            EntityToEntity(entity, obj);

            if (autoSave)
                return await SaveAsync();

            return -1;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public int Update(TEntity entity, bool autoSave = true)
        {
            var obj = GetByIDAsync(entity.ID).Result;

            EntityToEntity(entity, obj);

            if (autoSave)
                return _dbContext.SaveChanges();

            return -1;
        }

        private void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            foreach (var mItem in typeof(T).GetProperties())
            {
                mItem.SetValue(pTargetObjDest, mItem.GetValue(pTargetObjSrc, new object[] { }), null);
            }
        }
        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public async Task<int> InsertOrUpdateAsync(TEntity entity, bool autoSave = true)
        {
            if (GetByIDAsync(entity.ID) != null)
                await UpdateAsync(entity, autoSave);

            return await InsertAsync(entity, autoSave);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public async Task<int> DeleteAsync(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
                return await SaveAsync();

            return -1;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public int Delete(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);

            if (autoSave)
                return _dbContext.SaveChanges();

            return -1;
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public async Task<int> DeleteAsync(TPrimaryKey id, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(GetByIDAsync(id).Result);
            if (autoSave)
                return await SaveAsync();

            return -1;
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="autoSave">是否自动保存</param>
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(it => _dbContext.Set<TEntity>().Remove(it));
            if (autoSave)
                return await SaveAsync();

            return -1;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">页码</param>
        /// <param name="pageSize">单页数据数</param>
        /// <param name="rowCount">行数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public async Task<Tuple<List<TEntity>, int>> LoadPageList(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            int rowCount;

            var result = from p in _dbContext.Set<TEntity>()
                         select p;

            if (where != null)
                result = result.Where(where);

            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.ID);

            rowCount = await result.CountAsync();

            var skip = await result.Skip((startPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return Tuple.Create(skip, rowCount);
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }

    /// <summary>
    /// 主键为Guid类型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, string> where TEntity : StringEntityBase
    {

        public RepositoryBase(DbContext dbContext) : base(dbContext)
        {

        }

    }

}
