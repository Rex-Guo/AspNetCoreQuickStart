﻿using System.Data.Common;

namespace MyCompany.MyProject.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// 默认连接
        /// </summary>
        /// <returns></returns>
        DbConnection Default();

        /// <summary>
        /// 默认查询连接
        /// </summary>
        /// <returns></returns>
        DbConnection DefaultQuery();
    }
}