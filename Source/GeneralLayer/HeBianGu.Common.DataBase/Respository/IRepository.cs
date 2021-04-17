using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Common.DataBase
{
    /// <summary>
    /// 仓储接口定义
    /// </summary>
    public interface IRepository
    {

    }
    /// <summary>
    /// 定义泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync();

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(params string[] includes);

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        Task<TEntity> GetByIDAsync(TPrimaryKey id);

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        Task<int> UpdateAsync(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        Task<int> InsertOrUpdateAsync(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        Task<int> DeleteAsync(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="autoSave">是否立即执行保存</param>
        Task<int> DeleteAsync(TPrimaryKey id, bool autoSave = true);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="autoSave">是否自动保存</param>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true);

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面条目</param>
        /// <param name="rowCount">数据总数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        Task<Tuple<List<TEntity>, int>> LoadPageList(int startPage, int pageSizet, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);

        Task<int> SaveAsync();

    }


    /// <summary>
    /// 带有日志的泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ILogRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>
    {
        ///// <summary>
        ///// 运行日志
        ///// </summary>
        ///// <param name="message"></param>
        //void LogInfo(params string[] message);

        ///// <summary>
        ///// Degbug日志
        ///// </summary>
        ///// <param name="message"></param>
        //void LogDebug(params string[] message);

        ///// <summary>
        ///// 错误日志
        ///// </summary>
        ///// <param name="exception"></param>
        ///// <param name="message"></param>
        //void LogError(Exception exception, string message);
    }
    /// <summary>
    /// 默认Guid主键类型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : ILogRepository<TEntity, Guid> where TEntity : GuidEntityBase
    {

    }

    /// <summary>
    /// 默认string主键类型仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IStringRepository<TEntity> : ILogRepository<TEntity, string> where TEntity : StringEntityBase
    {

    }
}
