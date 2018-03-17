﻿using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.IRepositories;
using ApplicationCore.Models;
using ApplicationCore.SharedKernel;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DemoRepository : IDemoRepository
    {
        readonly AppDbContext _appDbContext;
        IAppLogger<DemoRepository> _appLogger;
        ISystemDateTime _systemDateTime;

        public DemoRepository(
            AppDbContext appDbContext,
            IAppLogger<DemoRepository> appLogger,
            ISystemDateTime systemDateTime)
        {
            _appDbContext = appDbContext;
            _appLogger = appLogger;
            _systemDateTime = systemDateTime;
        }

        public async Task AddAsync(DemoModel model)
        {
            var demo = model.ToDemo();
            demo.UpdateBasicInfo(_systemDateTime); //必须执行此步骤
            _appDbContext.Add(demo);
            await _appDbContext.SaveChangesAsync();
        }

        public IEnumerable<Demo> All() => _appDbContext.Demo.ToArray();

        public void Delete(Guid id)
        {
            var demo = _appDbContext.Demo.Find(id);

            _appLogger.Warn($"尝试删除Id为{id}的Demo失败，原因为未在数据库找到");
            if (demo == null) throw new AppException("未能找到要删除的Demo");

            _appDbContext.Remove(demo);
            _appDbContext.SaveChanges();
        }

        public Demo FindByKey(Guid id) => _appDbContext.Demo.Find(id);

        public IEnumerable<Demo> GetPage(int offset, int pageSize, out int total)
        {
            total = _appDbContext.Demo.Count();
            return _appDbContext.Demo.Skip(offset).Take(pageSize);
        }

        public IEnumerable<Demo> GetTopRecords(int count)
        {
            using (var connection = _appDbContext.Database.GetDbConnection())
            {
                return connection.Query<Demo>("select top (@Count) * from Demo", new { Count = count });
            }
        }

        public void Save(DemoModel model, Guid id)
        {
            var entity = FindByKey(id);
            if (entity == null) throw new AppException("未能找到要删除的对象");

            entity.Update(model.Name);
            _appDbContext.Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}
