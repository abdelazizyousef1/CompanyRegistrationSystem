using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.DTOS.Common
{
    public class Response<TEntity>
    {
        public TEntity? Data { get; set; }

        public string? Message { get; set; }

        public bool? Status { get; set; }

        public static Response<TEntity> Success(TEntity data, string? message = null)
        {
            return new Response<TEntity>
            {
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static Response<TEntity> Fail(string message)
        {
            return new Response<TEntity>
            {
                Status = false,
                Message = message,
                Data = default
            };
        }
    }

}
