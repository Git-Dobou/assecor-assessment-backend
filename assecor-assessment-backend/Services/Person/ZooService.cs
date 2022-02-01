using System;
using System.Collections.Generic;
using System.Linq;
using web_api.Models;
using web_api.Services.Db;
using web_api.Services.Logger;

namespace web_api.Services.Zoo
{
    public class ZooService : IZooService
    {
        private readonly List<Models.Zoo> _zoos;
        private readonly ILoggerManager _logger;
        private readonly DatabaseInteractor _databaseInteractor;

        public ZooService(ILoggerManager loggerManager,
            DatabaseInteractor databaseInteractor)
        {
            _logger = loggerManager;
            _databaseInteractor = databaseInteractor;

            _databaseInteractor.Database.EnsureCreated();
            _zoos = LoadFromDataBase();
        }

        private List<Models.Zoo> LoadFromDataBase()
        {
            try
            {
                return _databaseInteractor.Zoo.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }
        }

        public IEnumerable<Models.Zoo> GetAll()
        {
            return _zoos;
        }

        public bool Add(in Models.Zoo zoo)
        {
            try
            {
                if (zoo == null)
                    return false;

                zoo.Id = Guid.NewGuid().ToString();
                _zoos.Add(zoo);
                _databaseInteractor.Zoo.Add(zoo);
                _databaseInteractor.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }

            return true;
        }

        public IEnumerable<Models.Zoo> GetByName(string name)
        {
            try
            {
                return _zoos.Where(zoo => zoo.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                var index = _zoos?.FindIndex(zoo => zoo.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (index == null || index < 0)
                    return false;

                _zoos.RemoveAt(index.Value);
                _databaseInteractor.Zoo.Remove(_zoos[index.Value]);
                _databaseInteractor.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                throw ex;
            }
        }
    }
}