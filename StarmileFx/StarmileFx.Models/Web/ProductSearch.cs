using StarmileFx.Models.Youngo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace StarmileFx.Models.Web
{
    /// <summary>
    /// 商品搜索
    /// </summary>
    public class ProductSearch : PageData
    {
        /// <summary>
        /// 商品标识（即SKU,其他表记录的均为此字段）
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CnName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? startDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// 创建查询
        /// </summary>
        /// <returns></returns>
        public Expression<Func<Product, bool>> CreateExpression()
        {
            ParameterExpression parameter = Expression.Parameter(typeof(Product), "p");
            //选取未删除的数据  
            ConstantExpression constantPrjTypeCode = Expression.Constant(false);
            MemberExpression memberPrjTypeCode = Expression.PropertyOrField(parameter, "IsDelete");
            var query = Expression.Equal(memberPrjTypeCode, constantPrjTypeCode);
            //
            if (!string.IsNullOrEmpty(ProductID))
            {
                ConstantExpression constantProjectOfProvince = Expression.Constant(ProductID);
                MemberExpression memberProjectOfCity = Expression.PropertyOrField(parameter, "ProductID");
                query = Expression.And(query, Expression.Equal(memberProjectOfCity, constantProjectOfProvince));
            }
            //
            if (!string.IsNullOrEmpty(CnName))
            {
                ConstantExpression constantProjectOfProvince = Expression.Constant(CnName);
                MemberExpression memberProjectOfCity = Expression.PropertyOrField(parameter, "CnName");
                query = Expression.And(query, Expression.Equal(memberProjectOfCity, constantProjectOfProvince));
            }
            //
            if (Type != null)
            {
                ConstantExpression constantProjectOfProvince = Expression.Constant(Type);
                MemberExpression memberProjectOfCity = Expression.PropertyOrField(parameter, "Type");
                query = Expression.And(query, Expression.Equal(memberProjectOfCity, constantProjectOfProvince));
            }
            //开始时间  
            if (this.startDate.HasValue)
            {
                ConstantExpression constantApplyStartDate = Expression.Constant(this.startDate.Value);
                MemberExpression memberApplyStartDate = Expression.PropertyOrField(parameter, "CreatTime");
                query = Expression.And(query, Expression.GreaterThanOrEqual(memberApplyStartDate, constantApplyStartDate));
            }
            //结束时间  
            if (this.endDate.HasValue)
            {
                ConstantExpression constantApplyEndDate = Expression.Constant(this.endDate.Value);
                MemberExpression memberApplyEndDate = Expression.PropertyOrField(parameter, "CreatTime");
                query = Expression.And(query, Expression.LessThanOrEqual(memberApplyEndDate, constantApplyEndDate));
            }
            //有效性  
            ConstantExpression constantValidStatus = Expression.Constant(State);
            MemberExpression memberValidStatus = Expression.PropertyOrField(parameter, "State");
            query = Expression.And(query, Expression.Equal(memberValidStatus, constantValidStatus));

            return Expression.Lambda<Func<Product, bool>>(query, parameter);
        }
    }
}
