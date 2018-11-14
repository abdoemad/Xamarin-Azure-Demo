using AutoMapper;
using EShope.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShope.Services.Infra.Imp
{
    public class AutoMapper : IMapper
    {
        //public AutoMapper() {
            
        //}
        public void Initialize(Dictionary<Type, Type> mapConfiguration)
        {
            ExceptionHelper.TryCatch(() =>
            {
                Mapper.Initialize(cfg =>
                {
                    //cfg.AddCollectionMappers();
                    mapConfiguration.ToList().ForEach(mapConfig =>
                    {
                        cfg.CreateMap(mapConfig.Key, mapConfig.Value);
                    });
                });
            });
        }

        public R Map<S, R>(S input)
        {
            return Mapper.Map<R>(input);
        }
    }
}
