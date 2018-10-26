using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EShope.Services.Infra
{
    public interface IAPIConsumer
    {
        string DefaultEndPoint { get; }
        Task<T> GetAsync<T>(string uri);

        Task<Result> PostAsync<T, Result>(string uri, T data);
    }
}
