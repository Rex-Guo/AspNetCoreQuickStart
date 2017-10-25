﻿using ApplicationCore.SharedKernel;
using Newtonsoft.Json;
using System;

namespace Infrastructure.Utils
{
    public class JsonConventer : IJsonConventer
    {
        IAppLogger<JsonConventer> _logger;
        public JsonConventer(IAppLogger<JsonConventer> logger)
        {
            _logger = logger;
        }

        public string ToJson(object obj)
        {
            string json = null;
            try
            {
                json = JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                _logger.Error("对象转Json失败！", e);
            }
            return json;
        }

        public T ToObject<T>(string json)
        {
            var obj = default(T);
            try
            {
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                _logger.Error($"json转对象失败！json详情：{json}", e);
            }
            return obj;
        }
    }
}